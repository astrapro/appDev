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
    public partial class UC_Abut_Cant : UserControl
    {

        public IApplication iApp = null;

        public UC_Abut_Cant()
        {
            InitializeComponent();
        }

        public string Dead_Load_Reactions
        {
            get
            {
                if (dgv_abutment_input.RowCount > 10) return dgv_abutment_input[1, 15].Value.ToString();

                return "";
            }
            set
            {
                if (dgv_abutment_input.RowCount > 10) dgv_abutment_input[1, 15].Value = value;
            }
        }
        public string Length
        {
            get
            {
                if (dgv_abutment_input.RowCount > 1) return dgv_abutment_input[1, 1].Value.ToString();

                return "";
            }
            set
            {
                if (dgv_abutment_input.RowCount > 1) dgv_abutment_input[1, 1].Value = value;
            }
        }
        public void Load_Abutment_Inputs()
        {
            List<string> list = new List<string>();

            #region Inputs 1
            list.Add(string.Format("Span Arrangement$$"));
            //list.Add(string.Format("Span Arrangement (c/c of bearing)$5$m"));
            list.Add(string.Format("Span Arrangement (c/c of Exp. Joint)$7.900 $m"));
            list.Add(string.Format("Skew Angle$34.00 $Deg"));
            list.Add(string.Format("No. of Lane loading for design$3 $nos"));
            ////list.Add(string.Format("$$"));
            list.Add(string.Format("Reduced Levels $$"));
            ////list.Add(string.Format("$$"));
            list.Add(string.Format("FRL near crash barrier / median edge$596.136 $m"));
            list.Add(string.Format("Cross Camber$2.500 $%"));
            list.Add(string.Format("Depth of the superstructure at End beam$1.200 $m"));
            list.Add(string.Format("Thickness of wearing course$0.065 $m"));
            list.Add(string.Format("Thickness of bearing & pedestal$0.321 $m"));
            list.Add(string.Format("HFL$594.360 $m"));
            list.Add(string.Format("Bed Level at A1$591.980 $m"));
            list.Add(string.Format("Max Scour Level$589.140 $m"));
            list.Add(string.Format("Actual Foundation Level                  $589.540 $m"));
            //list.Add(string.Format("$$"));



            //list.Add(string.Format("$$"));
            list.Add(string.Format("Loads and Forces from Superstructure$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Dead Load Reaction$1635.12 $kN"));
            list.Add(string.Format("SIDL (Super Imposed Dead Load) Reaction $0.00 $kN"));
            list.Add(string.Format("Height of Hand Rail / Crash Barrier$1.00 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Trans. Ecc. of DL$0.000 $m"));
            list.Add(string.Format("Trans. Ecc. of SIDL$0.000 $m"));
            list.Add(string.Format("Trans. Ecc. of FPLL$0.000 $m"));
            //list.Add(string.Format("$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Earth Pressure  Parameters$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Angle of Repose of Soil$30.00$Degree"));
            list.Add(string.Format("Inclination of Backfill with horizontal$0.00$Degree"));
            list.Add(string.Format("Unit weight of concrete for stability check$24.00$kN/m^3"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Unit weight of concrete for Abutment design$22.50$kN/m^3"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Allowable Bearing Capacity (Normal Case)$250.00$kN/m^2"));
            //list.Add(string.Format("$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Material properties$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of Concrete$M 35$"));
            list.Add(string.Format("Grade of steel$Fe 500$"));



            DataGridView dgv = dgv_abutment_input;


            MyList.Fill_List_to_Grid(dgv, list, '$');


            #endregion Inputs



            #region Dimensional details

            //list.Add(string.Format("Dimensional details of structural components$$"));
            //list.Add(string.Format("$$"));
            list.Clear();
            list.Add(string.Format("Heel (7, 7', 7'', 10f, 10f')$$"));
            list.Add(string.Format("Width$2.300 $m "));
            list.Add(string.Format("Thickness  at root$1.200 $m"));
            list.Add(string.Format("Tip Thickness $0.500 $m"));
            list.Add(string.Format("Length of constant depth of heel$0.000 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Toe (8, 8', 8'', 9, 9', 9'')$ $"));
            list.Add(string.Format(" Width                       $2.300 $m "));
            list.Add(string.Format("Thickness  at root        $1.200 $m"));
            list.Add(string.Format("Tip Thickness $0.500 $m"));
            list.Add(string.Format("Length of constant depth of toe$0.000 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Shaft (4, 5, 6)$$"));
            list.Add(string.Format("Width at top                      $0.900 $m"));
            list.Add(string.Format("Width at bottom$0.900 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Cap (2, 3)$$"));
            list.Add(string.Format("Width  at top of Rect. part$1.200 $m"));
            list.Add(string.Format("Width  at bottom of Trap. Part$0.900 $m"));
            list.Add(string.Format("Thickness of rect part at top$0.300 $m"));
            list.Add(string.Format("Thickness of trap. part at bot.$0.300 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Solid Return  Wall (10a, 10b, 10c, 10d, 10e)$$"));
            list.Add(string.Format("Thickness at top$0.500 $m"));
            list.Add(string.Format("Thickness at bottom$0.500 $m"));
            list.Add(string.Format("No of return walls provided$1 $nos"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Cantilever  Wall (11a, 11b)$$"));
            list.Add(string.Format("Thickness$0.500 $m"));
            list.Add(string.Format("Height at tip (free end)$0.500 $m"));
            //list.Add(string.Format("$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Dirt Wall (1)$$"));
            list.Add(string.Format("Thickness$0.300 $m"));
            list.Add(string.Format("Radius of Curvature$0 $m"));
            list.Add(string.Format("Superelevation$0.0 $%"));
            list.Add(string.Format("Number of girders at abutment$4 $nos"));
            list.Add(string.Format("Total Base Length$12.000 $m "));
            list.Add(string.Format("Design speed of the vehicle$100 $kmph"));
            #endregion Dimensional details

            dgv = dgv_abut_2;


            MyList.Fill_List_to_Grid(dgv, list, '$');

            Modified_Cells();

        }

        public void Modified_Cells()
        {
            DataGridView dgv = dgv_abutment_input;
            #region Format Input Data
            //dgv = dgv_girder_input_data;

            string s1, s2, s3;

            s1 = "";
            s2 = "";
            s3 = "";
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s1 != "" && s2 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                    }
                    else
                    {
                        //if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

            dgv = dgv_abut_2;



            #region Format Input Data
            //dgv = dgv_girder_input_data;


            s1 = "";
            s2 = "";
            s3 = "";
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s1 != "" && s2 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                    }
                    else
                    {
                        //if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

        }

        string user_path = "";

        private void btn_abutment_process_Click(object sender, EventArgs e)
        {
            //DemoCheck();
                //iApp.Save_Form_Record(this, user_path);
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                Abutment_Process_Design_IS();
            }
            else
            {
                Abutment_Process_Design_BS();
            }
        }
        private void Abutment_Process_Design_IS()
        {

            //string file_path = "";
            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            

            if(iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "RCC_Cantilever_Abutment_Design_IRC.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design Cantilever\IRC_Abutment_Design_Cant.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["data"];

            DataGridView dgv = dgv_abutment_input;


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;



            List<double> data = new List<double>();
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

                int c = 0;
                //if (rindx < dgv.RowCount - 1)
                //{

                //}
                for (int i = 21; i <= 148; i++)
                {
                    if (rindx == 12)
                    {

                    }
                    if (i == 223
                        || i >= 22 && i <= 26
                        || i >= 22 && i <= 26
                        || i == 28
                        || i >= 30 && i <= 32
                        || i == 35
                        || i == 38
                        || i >= 40 && i <= 41
                        || i == 43
                        || i == 45
                        || i >= 48 && i <= 87
                        || i >= 90 && i <= 94
                        || i >= 97 && i <= 103
                        || i >= 105 && i <= 111
                        || i >= 113 && i <= 119
                        || i == 121
                        || i == 123
                        || i >= 125 && i <= 146
                        )
                    {
                        continue;
                    }

                    if (i == 21)
                    {
                        myExcelWorksheet.get_Range("G" + (i)).Formula = data[rindx++].ToString();
                    }
                    else if (i == 96)
                    {
                        myExcelWorksheet.get_Range("K88").Formula = data[rindx++].ToString();
                        myExcelWorksheet.get_Range("K89").Formula = data[rindx++].ToString();
                        myExcelWorksheet.get_Range("K90").Formula = data[rindx++].ToString();
                    }
                    else if (i > 103 && i < 125)
                    {
                        myExcelWorksheet.get_Range("H" + (i)).Formula = data[rindx++].ToString();

                    }
                    else if (i > 146 && i < 149)
                    {
                        myExcelWorksheet.get_Range("G" + (i)).Formula = data[rindx++].ToString();
                    }
                    else
                    {
                        myExcelWorksheet.get_Range("F" + (i)).Formula = data[rindx++].ToString();

                    }
                }

                #region Input 2
                dgv = dgv_abut_2;
                data.Clear();
                rindx = 0;

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv[1, i].Value.ToString() != "")
                    {
                        data.Add(MyList.StringToDouble(dgv[1, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", ""), 0.0));
                    }
                }

                myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E54").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E55").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E56").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K53").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K54").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K55").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K56").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("E59").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E60").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K59").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K60").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K61").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K62").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("E66").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E67").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E71").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K66").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K69").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("E74").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E81").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E82").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("F84").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("K78").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();




                #endregion Input 2


            }
            catch (Exception exx) { }




            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }
        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Cantilever Abutment Design";
            }
        }

        public void Load_Live_Loads()
        {
            //cmb_bs_ll_2
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_1);
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_2);
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                cmb_bs_ll_1.SelectedIndex = cmb_bs_ll_1.Items.Count - 1;
                cmb_bs_ll_2.SelectedIndex = 12;
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
    
            }
        }

        private void Abutment_Process_Design_BS()
        {


            string file_path = "";
            //string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);
            else
               file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Abutment_Design_BS.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design Cantilever\BS_Abutment_Design_Cant.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["data"];

            DataGridView dgv = dgv_abutment_input;


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;



            List<double> data = new List<double>();
            try
            {
                double val = 0.0;

                #region data
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv[1, i].Value.ToString() != "")
                    {
                        data.Add(MyList.StringToDouble(dgv[1, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", ""), 0.0));
                    }
                }

                int c = 0;
                //if (rindx < dgv.RowCount - 1)
                //{

                //}
                for (int i = 21; i <= 148; i++)
                {
                    if (rindx == 12)
                    {

                    }
                    if (i == 223
                        || i >= 22 && i <= 26
                        || i >= 22 && i <= 26
                        || i == 28
                        || i >= 30 && i <= 32
                        || i == 35
                        || i == 38
                        || i >= 40 && i <= 41
                        || i == 43
                        || i == 45
                        || i >= 48 && i <= 87
                        || i >= 90 && i <= 94
                        || i >= 97 && i <= 103
                        || i >= 105 && i <= 111
                        || i >= 113 && i <= 119
                        || i == 121
                        || i == 123
                        || i >= 125 && i <= 146
                        )
                    {
                        continue;
                    }

                    if (i == 21)
                    {
                        myExcelWorksheet.get_Range("G" + (i)).Formula = data[rindx++].ToString();
                    }
                    else if (i == 96)
                    {
                        myExcelWorksheet.get_Range("K88").Formula = data[rindx++].ToString();
                        myExcelWorksheet.get_Range("K89").Formula = data[rindx++].ToString();
                        myExcelWorksheet.get_Range("K90").Formula = data[rindx++].ToString();
                    }
                    else if (i > 103 && i < 125)
                    {
                        myExcelWorksheet.get_Range("H" + (i)).Formula = data[rindx++].ToString();

                    }
                    else if (i > 146 && i < 149)
                    {
                        myExcelWorksheet.get_Range("G" + (i)).Formula = data[rindx++].ToString();
                    }
                    else
                    {
                        myExcelWorksheet.get_Range("F" + (i)).Formula = data[rindx++].ToString();

                    }
                }

                #region Input 2
                dgv = dgv_abut_2;
                data.Clear();
                rindx = 0;

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv[1, i].Value.ToString() != "")
                    {
                        data.Add(MyList.StringToDouble(dgv[1, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", ""), 0.0));
                    }
                }

                myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E54").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E55").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E56").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K53").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K54").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K55").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K56").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("E59").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E60").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K59").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K60").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K61").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K62").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("E66").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E67").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E71").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("K66").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K69").Formula = data[rindx++].ToString();


                myExcelWorksheet.get_Range("E74").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E81").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("E82").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("F84").Formula = data[rindx++].ToString();

                myExcelWorksheet.get_Range("K78").Formula = data[rindx++].ToString();
                myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();




                #endregion Input 2

                #endregion data

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["cwll"];

                myExcelWorksheet.get_Range("B21").Formula = txt_bs_left_bl.Text;
                myExcelWorksheet.get_Range("J21").Formula = txt_bs_right_bl.Text;

                myExcelWorksheet.get_Range("B32").Formula = cmb_bs_ll_1.Text;

                myExcelWorksheet.get_Range("E34").Formula = txt_bs_total_load1.Text;
                myExcelWorksheet.get_Range("D36").Formula = txt_bs_load_dist1.Text;



                myExcelWorksheet.get_Range("B65").Formula = cmb_bs_ll_2.Text;

                myExcelWorksheet.get_Range("E66").Formula = txt_bs_total_load2.Text;
                myExcelWorksheet.get_Range("D68").Formula = txt_bs_load_dist2.Text;


            }
            catch (Exception exx) { }




            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

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

        private void btn_abutment_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void cmb_bs_ll_1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmd = sender as ComboBox;
            DataGridView dgv = dgv_bs_ll_1;
            TextBox txtb = txt_bs_total_load1;


            if (cmd.Name == cmb_bs_ll_2.Name)
            {
                dgv = dgv_bs_ll_2;
                txtb = txt_bs_total_load2;
            }

            //string ss = cmd.Text;

            //MyList mlst = new MyList(ss, ':');
            string code = cmd.Text;

            LoadData ld = iApp.LiveLoads.Get_LoadData(code);


            dgv.Rows.Clear();
            for (int i = 0; i < ld.Loads_In_KN.StringList.Count; i++)
            {
                if (i == 0)
                {
                    dgv.Rows.Add((i + 1), ld.Loads_In_KN.StringList[0], "");

                    dgv[2, 0].ReadOnly = true;
                    dgv[2, 0].Style.BackColor = Color.Gray;
                }
                else
                {
                    dgv.Rows.Add((i + 1), ld.Loads_In_KN.StringList[i], ld.Distances.StringList[i - 1]);
                }
            }
            txtb.Text = (ld.Total_Loads * 10).ToString("f3");

        }

        private void UC_Abut_Cant_Load(object sender, EventArgs e)
        {
            Load_Abutment_Inputs();
        }

    }
}
