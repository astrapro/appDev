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


namespace BridgeAnalysisDesign.RCC_Culvert
{
    public partial class UC_BoxCulvert : UserControl
    {
         IApplication iApp;

        public UC_BoxCulvert()
        {
            InitializeComponent();
            Load_Default_Data_Single_Cell();
            Load_Default_Data_Multi_Cell();
        }
        public void SetIApplication(IApplication app)
        {
            iApp = app;
        }
        void Load_Default_Data_Single_Cell()
        {
            //DataGridView dgv = dgv_input_data;

            List<string> list = new List<string>();
            MyList mlist = null;


            #region Base Pressure
            list.Add(string.Format("Number of cell in culvert$1$"));
            list.Add(string.Format("Clear Span$8.8$m"));
            list.Add(string.Format("Clear Height$7.6$m"));
            list.Add(string.Format("Overall width$12$m"));
            list.Add(string.Format("Carriageway width$11$m"));
            list.Add(string.Format("Crash barrier width$0.5$m"));
            list.Add(string.Format("Footpath width/ Safety kerb$0$m"));
            list.Add(string.Format("Minimum thickness of wearing coat$0.075$m"));
            list.Add(string.Format("Thickness of soil fill over Bottom slab$0$m"));
            list.Add(string.Format("lean concrete thickness over deck$0.000$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Width of Haunch$0.3$m"));
            list.Add(string.Format("Depth of Haunch$0.3$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Top slab thickness$0.70$m"));
            list.Add(string.Format("Bottom slab thickness$0.70$m"));
            list.Add(string.Format("Side wall thickness $0.65$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Safe Bearing Capacity$150$kN/m²"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Density of concrete$25$kN/m³"));
            list.Add(string.Format("unit weight of soil$18$kN/m³"));
            list.Add(string.Format("unit weight of submerged soil$10$kN/m³"));
            list.Add(string.Format("unit weight of wearing coat$22$kN/m³"));
            list.Add(string.Format("Grade of concrete$35$N/mm2"));
            list.Add(string.Format("Grade of reinforcement$500$N/mm2"));
            list.Add(string.Format("Length of the box considered for design$1$m"));
            list.Add(string.Format("Modulus of elasticity of concret$2.96E+04$N/m2"));
            list.Add(string.Format("Modulus of elasticity of Steel$2.00E+05$N/m2"));
            list.Add(string.Format("Permissible stresses$$"));
            list.Add(string.Format("Permissible Compressive stress in Concrete$11.5$Mpa"));
            list.Add(string.Format("Permissible Tensile stress in Steel$240$Mpa"));
            list.Add(string.Format("Modular ratio, m$6.76$"));
            list.Add(string.Format("factor, k$0.245$"));
            list.Add(string.Format("Lever arm factor, j$0.92$"));
            list.Add(string.Format("Moment resistance factor, R$1.29$Mpa"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of shear reinforcement$500$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Design Levels$$"));
            list.Add(string.Format("Formation road level$812.043$m"));
            list.Add(string.Format("Highest Flood level$810.56$m"));
            list.Add(string.Format("Lowest Water level$803$m"));
            list.Add(string.Format("Maximum Scour level$803$m"));
            list.Add(string.Format("Foundation level$802.899$m"));

            //dgv = dgv_input_data;



            MyList.Fill_List_to_Grid(dgv_design_data_single, list, '$');
            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure

            //Modified_Cell(dgv_input_data);

            //if (dgv[2, i].Value == "") dgv[2, i].Value = "";

        }


        void Load_Default_Data_Multi_Cell()
        {
            //DataGridView dgv = dgv_input_data;

            List<string> list = new List<string>();
            MyList mlist = null;


            #region Base Pressure
            list.Add(string.Format("Design Data$$"));
            list.Add(string.Format("Number of cells in culvert$2$"));
            list.Add(string.Format("Clear Span$11.90$m"));
            list.Add(string.Format("$11.90$m"));
            list.Add(string.Format("$0.00$m"));
            list.Add(string.Format("Effective Span$12.6$m(skew)"));
            list.Add(string.Format("$12.6$m(skew)"));
            list.Add(string.Format("$0$m"));
            list.Add(string.Format("Total span$25.2$m"));
            list.Add(string.Format("Clear Height$4.5$m"));
            list.Add(string.Format("Effective Height$5.35$m"));
            list.Add(string.Format("Overall width$28$m"));
            list.Add(string.Format("Carriageway width$8.5$m"));
            list.Add(string.Format("Crash barrier width$0.50$m"));
            list.Add(string.Format("Crash barrier Height$0.75$m"));
            list.Add(string.Format("Safety kerb$0.00$m"));
            list.Add(string.Format("Ht. of crash barrier$1.00$m"));
            list.Add(string.Format("Ht. of kerb$0.00$m"));
            list.Add(string.Format("Width of railing$0.50$m"));
            list.Add(string.Format("Footpath width$1.50$m"));
            list.Add(string.Format("Minimum thickness of wearing coat$0.065$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Thickness of soil fill over Bottom slab$0.00$m"));
            list.Add(string.Format("lean concrete thickness bot. slab$0.00$m"));
            list.Add(string.Format("Length of the box considered for design$1.00$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Width of Haunch$1.5$m"));
            list.Add(string.Format("Depth of Haunch$0.5$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Top slab thickness$0.80$m"));
            list.Add(string.Format("Bottom slab thickness$0.90$m"));
            list.Add(string.Format("Side wall thickness $0.80$m"));
            list.Add(string.Format("Mid wall thickness$0.60$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Safe Bearing Capacity$150.00$kN/m²"));
            list.Add(string.Format("Density of concrete$25.00$kN/m³"));
            list.Add(string.Format("unit weight of soil$18.00$kN/m³"));
            list.Add(string.Format("unit weight of submerged soil$10.00$kN/m³"));
            list.Add(string.Format("unit weight of wearing coat$22.00$kN/m³"));
            list.Add(string.Format("unit weight of water$10.00$kN/m³"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of concrete$30$N/mm2"));
            list.Add(string.Format("$2.50$N/mm2"));
            list.Add(string.Format("$13.40$N/mm2"));
            list.Add(string.Format("Grade of reinforcement$500$N/mm2"));
            list.Add(string.Format("Grade of shear reinforcement$500$N/mm2"));
            list.Add(string.Format("Modulus of elasticity of concret$3.10E+07$N/m2"));
            list.Add(string.Format("Modulus of elasticity of Steel$200000000$N/m2"));
            list.Add(string.Format("Modulur Ratio$6.451612903$"));
            list.Add(string.Format("Design Levels$$"));
            list.Add(string.Format("Formation road level$263.335$m"));
            list.Add(string.Format("Highest Flood level$260.660$m"));
            list.Add(string.Format("Lowest Water level$256.259$m"));
            list.Add(string.Format("Maximum Scour level$256.259$m"));
            list.Add(string.Format("Foundation level$258.259$m"));
            //dgv = dgv_input_data;



            MyList.Fill_List_to_Grid(dgv_design_data_multi, list, '$');
            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure

            //Modified_Cell(dgv_input_data);

            //if (dgv[2, i].Value == "") dgv[2, i].Value = "";

        }

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "RCC Box Culvert Design in LSM";
            }
        }
        private void Box_Culvert_Multi_Cell()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Box Culvert Multi Cell.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Box Culvert Multi Cell.xlsx");

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



            List<double> data = new List<double>();
            try
            {

                if (false)
                {
                    List<string> list = new List<string>();



                    DataGridView dgv = dgv_design_data_single;
                    int rindx = 0;
                    int i = 0;


                    #region Design Data
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

                    rindx = 0;

                    dgv = dgv_design_data_single;



                    DataInputCollections dips = new DataInputCollections();

                    dips.Load_Data_from_Grid(dgv);
                    List<string> ldbl = new List<string>();

                    rindx = 0;

                    for (i = 6; i < 68; i++)
                    {
                        if ((i >= 12 && i <= 26) ||
                            (i >= 22 && i <= 30) ||
                            (i >= 30 && i <= 31) ||
                            (i >= 36 && i <= 37) ||
                            (i >= 40 && i <= 41) ||
                            (i >= 47 && i <= 48) ||
                            (i >= 52 && i <= 53) ||
                            (i >= 58 && i <= 59) ||
                            (i >= 68 && i <= 70) ||
                            (i >= 72 && i <= 74) ||
                            (i >= 88 && i <= 90) ||
                            (i >= 92 && i <= 94) ||
                            (i >= 98 && i <= 100) ||
                            (i >= 103 && i <= 105) ||
                            (i >= 110 && i <= 116)
                           )
                        {
                            continue;
                        }
                        else
                        {

                            if (rindx == 60)
                            {
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("M", "").Trim().ToString();

                                //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                            }
                            else if (rindx == 61)
                            {
                                //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("FE", "").Trim().ToString();
                            }
                            else
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();

                        }
                    }
                    #endregion Design Data


                    rindx = 0;

                }

            }
            catch (Exception exx) { }

            iApp.Excel_Close_Message();

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        private void Box_Culvert_Single_Cell()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Single cell box.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Single cell box.xlsx");

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



            List<double> data = new List<double>();
            try
            {

                if (false)
                {
                    List<string> list = new List<string>();



                    DataGridView dgv = dgv_design_data_single;
                    int rindx = 0;
                    int i = 0;


                    #region Design Data
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

                    rindx = 0;

                    dgv = dgv_design_data_single;



                    DataInputCollections dips = new DataInputCollections();

                    dips.Load_Data_from_Grid(dgv);
                    List<string> ldbl = new List<string>();

                    rindx = 0;

                    for (i = 6; i < 68; i++)
                    {
                        if ((i >= 12 && i <= 26) ||
                            (i >= 22 && i <= 30) ||
                            (i >= 30 && i <= 31) ||
                            (i >= 36 && i <= 37) ||
                            (i >= 40 && i <= 41) ||
                            (i >= 47 && i <= 48) ||
                            (i >= 52 && i <= 53) ||
                            (i >= 58 && i <= 59) ||
                            (i >= 68 && i <= 70) ||
                            (i >= 72 && i <= 74) ||
                            (i >= 88 && i <= 90) ||
                            (i >= 92 && i <= 94) ||
                            (i >= 98 && i <= 100) ||
                            (i >= 103 && i <= 105) ||
                            (i >= 110 && i <= 116)
                           )
                        {
                            continue;
                        }
                        else
                        {

                            if (rindx == 60)
                            {
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("M", "").Trim().ToString();

                                //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                            }
                            else if (rindx == 61)
                            {
                                //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("FE", "").Trim().ToString();
                            }
                            else
                                myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();

                        }
                    }
                    #endregion Design Data


                    rindx = 0;

                }

            }
            catch (Exception exx) { }

            iApp.Excel_Close_Message();

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


        private void btn_single_cell_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_process)
            {
                if (rbtn_multi.Checked)
                {
                    Box_Culvert_Multi_Cell();
                }
                else
                {
                    Box_Culvert_Single_Cell();
                }
            }
            else if(btn == btn_open)
            {
                iApp.Open_ASTRA_Worksheet_Dialog();
            }
        }

        public bool Is_Multi_Cell
        {
            get
            {
                return rbtn_multi.Checked;
            }
            set
            {
                rbtn_multi.Checked = value;
                rbtn_single.Checked = !value;
            }
        }
        private void rbtn_single_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_multi.Checked) sc1.Panel1Collapsed = true;
            sc1.Panel2Collapsed = true;
        }

    }
}
