using System;
using System.Collections.Generic;
using System.Collections;
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

        public event EventHandler OnButtonProceed;


        //Box_Culvert_Model box = new Box_Culvert_Model(5, 8, 7);
        Box_Culvert_Model MultiCell_Box_Model { get; set; }
        Box_Culvert_Model SingleCell_Box_Model { get; set; }

        public UC_BoxCulvert()
        {
            InitializeComponent();
            sc1.Panel2Collapsed = true;


            Load_Default_Data_Single_Cell();
            Load_Default_Data_Multi_Cell();
            Button_Enable_Disable();
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
            list.Add(string.Format("Design Data$$"));
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
            list.Add(string.Format("$$"));
            list.Add(string.Format("Permissible stresses$$"));
            list.Add(string.Format("Permissible Compressive stress in Concrete$11.5$Mpa"));
            list.Add(string.Format("Permissible Tensile stress in Steel$240$Mpa"));
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
            MyList.Modified_Cell(dgv_design_data_single);


            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure


            MyList.Fill_List_to_Grid(dgv_design_data_single, list, '$');
            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            MyList.Modified_Cell(dgv_design_data_single);



        }


        void Load_Default_Data_Multi_Cell()
        {
            //DataGridView dgv = dgv_input_data;

            List<string> list = new List<string>();
            MyList mlist = null;


            #region Base Pressure
            list.Add(string.Format("Design Data$$"));
            list.Add(string.Format("Number of cells in culvert$2$"));
            list.Add(string.Format("Effective Span (L1)$12.6$m(skew)"));
            list.Add(string.Format("Effective Span (L2)$12.6$m(skew)"));
            list.Add(string.Format("Clear Height$4.5$m"));
            list.Add(string.Format("$$"));
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
            list.Add(string.Format("Grade of concrete$30$N/mm²"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of reinforcement$500$N/mm²"));
            list.Add(string.Format("Grade of shear reinforcement$500$N/mm²"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Modulus of elasticity of Steel$200000000$N/m²"));
            list.Add(string.Format("Design Levels$$"));
            list.Add(string.Format("Formation road level$263.335$m"));
            list.Add(string.Format("Highest Flood level$260.660$m"));
            list.Add(string.Format("Foundation level$258.259$m"));
            //dgv = dgv_input_data;



            MyList.Fill_List_to_Grid(dgv_design_data_multi, list, '$');
            MyList.Modified_Cell(dgv_design_data_multi);


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

            //file_path = Path.Combine(file_path, "Box Culvert Multi Cell.xlsx");
            file_path = Excel_File();

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Box Culvert Multi Cell LSM.xlsx");

            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Box Culvert Multi Cell LSM BS.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];



            List<double> data = new List<double>();
            try
            {

                //if (false)
                if (true)
                {
                    List<string> list = new List<string>();



                    DataGridView dgv = dgv_design_data_multi;
                    int rindx = 0;
                    int i = 0;


                    #region Design Data
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

                    rindx = 0;

                    dgv = dgv_design_data_multi;



                    DataInputCollections dips = new DataInputCollections();

                    dips.Load_Data_from_Grid(dgv);
                    List<string> ldbl = new List<string>();

                    rindx = 0;

                    for (i = 6; i < 68; i++)
                    {
                        if ((i == 7) ||
                            (i == 8) ||
                            (i == 9) ||
                            (i == 12) ||
                            (i == 13) ||
                            (i == 15) ||
                            (i == 26) ||
                            (i == 30) ||
                            (i == 33) ||
                            (i == 38) ||
                            (i == 45) ||
                            (i == 47) ||
                            (i == 48) ||
                            (i == 51) ||
                            (i == 53) ||
                            (i == 65) ||
                            (i == 66) ||
                            (i >= 53 && i <= 62)
                           )
                        {
                            continue;
                        }
                        else
                        {

                            //if (rindx == 60)
                            //{
                            //    myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("M", "").Trim().ToString();

                            //    //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                            //}
                            //else if (rindx == 61)
                            //{
                            //    //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                            //    myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("FE", "").Trim().ToString();
                            //}
                            //else
                            myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();

                        }
                        if (rindx < dips.Count)
                            while (dips[rindx].DATA == "") rindx++;
                    }
                    #endregion Design Data


                    if (true)
                    {
                        #region Analysis Results
                        rindx = 0;


                        myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Summary of loads"];



                        BC_Results BR = new BC_Results(Result_File);




                        BC_Table bt = BR[0];

                        List<string> cells = new List<string>();



                        cells.Add("C");
                        cells.Add("D");
                        cells.Add("E");
                        cells.Add("F");
                        cells.Add("G");
                        cells.Add("H");
                        cells.Add("I");
                        cells.Add("J");
                        cells.Add("K");
                        cells.Add("L");
                        cells.Add("M");
                        cells.Add("N");
                        cells.Add("O");


                        #region Top Slab
                        bt = BR[0];

                        #region Top Slab 1 Bending Moment
                        rindx = 0;
                        for (i = 9; i <= 19; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment

                        bt = BR[1];


                        #region Top Slab 2 Bending Moment
                        rindx = 0;
                        for (i = 24; i <= 34; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 2 Bending Moment


                        bt = BR[2];

                        #region Top Slab 1 Shear Force
                        rindx = 0;

                        for (i = 55; i <= 65; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion  Top Slab 1 Shear Force

                        bt = BR[3];

                        #region Top Slab 2 Shear Force
                        rindx = 0;

                        for (i = 70; i <= 80; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion  Top Slab 1 Shear Force
                        #endregion Top Slab


                        #region Side Wall

                        bt = BR[4];

                        #region Side Wall 1 Bending Moment
                        rindx = 0;
                        for (i = 861; i <= 871; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment

                        bt = BR[5];

                        #region Side Wall 2 Bending Moment
                        rindx = 0;
                        for (i = 906; i <= 916; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment


                        bt = BR[6];

                        #region Side Wall 1 Shear Force
                        rindx = 0;
                        for (i = 922; i <= 932; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment

                        bt = BR[7];

                        #region Side Wall 2 Shear Force
                        rindx = 0;
                        for (i = 968; i <= 978; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment

                        #endregion Side Wall

                        #region Inner Wall

                        bt = BR[8];

                        #region Inner Wall 1 Bending Moment
                        rindx = 0;
                        for (i = 876; i <= 886; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment


                        bt = BR[9];

                        #region Inner Wall 1 Shear Force
                        rindx = 0;
                        for (i = 937; i <= 947; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment
                        #endregion Inner Wall

                        bt = BR[10];

                        #region Bottom Slab 1 Bending Moment
                        rindx = 0;
                        for (i = 1990; i <= 1992; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment

                        bt = BR[11];

                        #region Bottom Slab 2 Bending Moment
                        rindx = 0;
                        for (i = 1997; i <= 1999; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Top Slab 1 Bending Moment
                        bt = BR[12];

                        #region Bottom Slab 1 Shear Force
                        rindx = 0;
                        for (i = 2012; i <= 2014; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Bottom Slab 1 Shear Force

                        bt = BR[13];

                        #region Bottom Slab 2 Shear Force
                        rindx = 0;
                        for (i = 2019; i <= 2021; i++)
                        {
                            //foreach (var item in cells)
                            //{
                            //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                            //}

                            for (int c = 0; c < cells.Count; c++)
                            {
                                myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                            }
                            //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                            rindx++;
                        }
                        #endregion Bottom Slab 1 Shear Force

                        #endregion Analysis Results
                    }
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

            //file_path = Path.Combine(file_path, "Single cell box.xlsx");
            file_path = Excel_File();

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Box Culvert Single Cell LSM.xlsx");

            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Box Culvert\Box Culvert Single Cell LSM BS.xlsx");
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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];



            List<double> data = new List<double>();
            try
            {

                //if (false)
                if (true)
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

                    for (i = 3; i < 48; i++)
                    {
                        if ((i == 13) ||
                            (i == 16) ||
                            (i == 20) ||
                            (i == 22) ||
                            (i >= 30 && i <= 32) ||
                            (i >= 35 && i <= 39) ||

                            (i == 41) ||
                            (i == 42)
                           )
                        {
                            continue;
                        }
                        else
                        {
                            myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();

                            if (rindx < dips.Count)
                            {
                                while (dips[rindx].DATA == "")
                                    rindx++;
                            }
                        }
                    }


                    #endregion Design Data

                    #region Analysis Report



                    #region Analysis Results
                    rindx = 0;


                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Summary of loads"];




                    BC_Results BR = new BC_Results(Result_File);




                    BC_Table bt = BR[0];

                    List<string> cells = new List<string>();



                    cells.Add("C");
                    cells.Add("D");
                    cells.Add("E");
                    cells.Add("F");
                    cells.Add("G");
                    cells.Add("H");
                    cells.Add("I");
                    cells.Add("J");
                    cells.Add("K");
                    cells.Add("L");
                    cells.Add("M");
                    cells.Add("N");
                    cells.Add("O");


                    #region Top Slab


                    #region Top Slab 1 Bending Moment
                    rindx = 0;
                    for (i = 6; i <= 16; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment


                    bt = BR[1];

                    #region Top Slab 1 Shear Force
                    rindx = 0;

                    for (i = 22; i <= 32; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion  Top Slab 1 Shear Force


                    #endregion Top Slab


                    #region Side Wall

                    bt = BR[2];

                    #region Side Wall 1 Bending Moment
                    rindx = 0;
                    for (i = 223; i <= 233; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment

                    bt = BR[3];

                    #region Side Wall 2 Bending Moment
                    rindx = 0;
                    for (i = 437; i <= 447; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment



                    #region Side Wall 1 Shear Force

                    bt = BR[4];



                    rindx = 0;
                    for (i = 239; i <= 249; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment

                    #region Side Wall 2 Shear Force

                    bt = BR[5];

                    rindx = 0;
                    for (i = 453; i <= 463; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment

                    #endregion Side Wall

                    bt = BR[6];

                    #region Bottom Slab 1 Bending Moment
                    rindx = 0;
                    for (i = 654; i <= 664; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Top Slab 1 Bending Moment


                    bt = BR[7];

                    #region Bottom Slab 1 Shear Force
                    rindx = 0;
                    for (i = 670; i <= 680; i++)
                    {
                        //foreach (var item in cells)
                        //{
                        //    //myExcelWorksheet.get_Range(item + i).Formula = bt[i][rindx++];
                        //}

                        for (int c = 0; c < cells.Count; c++)
                        {
                            myExcelWorksheet.get_Range(cells[c] + i).Formula = bt[rindx][c];
                        }
                        //myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();
                        rindx++;
                    }
                    #endregion Bottom Slab 1 Shear Force

                    #endregion Analysis Results


                    #endregion Analysis Report
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

        public void Create_Data_Multicell_DeadLoad()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("ASTRA SPACE MULTICELL BOX CULVERT"));
            list.Add(string.Format("UNIT METER KN"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1        0.0000        0.0000        0.0000"));
            list.Add(string.Format("2        12.7500        0.0000        0.0000"));
            list.Add(string.Format("3        25.5000        0.0000        0.0000"));
            list.Add(string.Format("4        0.0000        5.0700        0.0000"));
            list.Add(string.Format("5        12.7500        5.0700        0.0000"));
            list.Add(string.Format("6        25.5000        5.0700        0.0000"));
            list.Add(string.Format("7        12.7400        0.0000        0.0000"));
            list.Add(string.Format("8        0.1400        0.0000        0.0000"));
            list.Add(string.Format("9        0.2800        0.0000        0.0000"));
            list.Add(string.Format("10        0.4200        0.0000        0.0000"));
            list.Add(string.Format("11        0.5600        0.0000        0.0000"));
            list.Add(string.Format("12        0.7000        0.0000        0.0000"));
            list.Add(string.Format("13        0.8400        0.0000        0.0000"));
            list.Add(string.Format("14        0.9800        0.0000        0.0000"));
            list.Add(string.Format("15        1.1200        0.0000        0.0000"));
            list.Add(string.Format("16        1.2600        0.0000        0.0000"));
            list.Add(string.Format("17        1.4000        0.0000        0.0000"));
            list.Add(string.Format("18        1.5400        0.0000        0.0000"));
            list.Add(string.Format("19        1.6800        0.0000        0.0000"));
            list.Add(string.Format("20        1.8200        0.0000        0.0000"));
            list.Add(string.Format("21        1.9600        0.0000        0.0000"));
            list.Add(string.Format("22        2.1000        0.0000        0.0000"));
            list.Add(string.Format("23        2.2400        0.0000        0.0000"));
            list.Add(string.Format("24        2.3800        0.0000        0.0000"));
            list.Add(string.Format("25        2.5200        0.0000        0.0000"));
            list.Add(string.Format("26        2.6600        0.0000        0.0000"));
            list.Add(string.Format("27        2.8000        0.0000        0.0000"));
            list.Add(string.Format("28        2.9400        0.0000        0.0000"));
            list.Add(string.Format("29        3.0800        0.0000        0.0000"));
            list.Add(string.Format("30        3.2200        0.0000        0.0000"));
            list.Add(string.Format("31        3.3600        0.0000        0.0000"));
            list.Add(string.Format("32        3.5000        0.0000        0.0000"));
            list.Add(string.Format("33        3.6400        0.0000        0.0000"));
            list.Add(string.Format("34        3.7800        0.0000        0.0000"));
            list.Add(string.Format("35        3.9200        0.0000        0.0000"));
            list.Add(string.Format("36        4.0600        0.0000        0.0000"));
            list.Add(string.Format("37        4.2000        0.0000        0.0000"));
            list.Add(string.Format("38        4.3400        0.0000        0.0000"));
            list.Add(string.Format("39        4.4800        0.0000        0.0000"));
            list.Add(string.Format("40        4.6200        0.0000        0.0000"));
            list.Add(string.Format("41        4.7600        0.0000        0.0000"));
            list.Add(string.Format("42        4.9000        0.0000        0.0000"));
            list.Add(string.Format("43        5.0400        0.0000        0.0000"));
            list.Add(string.Format("44        5.1800        0.0000        0.0000"));
            list.Add(string.Format("45        5.3200        0.0000        0.0000"));
            list.Add(string.Format("46        5.4600        0.0000        0.0000"));
            list.Add(string.Format("47        5.6000        0.0000        0.0000"));
            list.Add(string.Format("48        5.7400        0.0000        0.0000"));
            list.Add(string.Format("49        5.8800        0.0000        0.0000"));
            list.Add(string.Format("50        6.0200        0.0000        0.0000"));
            list.Add(string.Format("51        6.1600        0.0000        0.0000"));
            list.Add(string.Format("52        6.3000        0.0000        0.0000"));
            list.Add(string.Format("53        6.4400        0.0000        0.0000"));
            list.Add(string.Format("54        6.5800        0.0000        0.0000"));
            list.Add(string.Format("55        6.7200        0.0000        0.0000"));
            list.Add(string.Format("56        6.8600        0.0000        0.0000"));
            list.Add(string.Format("57        7.0000        0.0000        0.0000"));
            list.Add(string.Format("58        7.1400        0.0000        0.0000"));
            list.Add(string.Format("59        7.2800        0.0000        0.0000"));
            list.Add(string.Format("60        7.4200        0.0000        0.0000"));
            list.Add(string.Format("61        7.5600        0.0000        0.0000"));
            list.Add(string.Format("62        7.7000        0.0000        0.0000"));
            list.Add(string.Format("63        7.8400        0.0000        0.0000"));
            list.Add(string.Format("64        7.9800        0.0000        0.0000"));
            list.Add(string.Format("65        8.1200        0.0000        0.0000"));
            list.Add(string.Format("66        8.2600        0.0000        0.0000"));
            list.Add(string.Format("67        8.4000        0.0000        0.0000"));
            list.Add(string.Format("68        8.5400        0.0000        0.0000"));
            list.Add(string.Format("69        8.6800        0.0000        0.0000"));
            list.Add(string.Format("70        8.8200        0.0000        0.0000"));
            list.Add(string.Format("71        8.9600        0.0000        0.0000"));
            list.Add(string.Format("72        9.1000        0.0000        0.0000"));
            list.Add(string.Format("73        9.2400        0.0000        0.0000"));
            list.Add(string.Format("74        9.3800        0.0000        0.0000"));
            list.Add(string.Format("75        9.5200        0.0000        0.0000"));
            list.Add(string.Format("76        9.6600        0.0000        0.0000"));
            list.Add(string.Format("77        9.8000        0.0000        0.0000"));
            list.Add(string.Format("78        9.9400        0.0000        0.0000"));
            list.Add(string.Format("79        10.0800        0.0000        0.0000"));
            list.Add(string.Format("80        10.2200        0.0000        0.0000"));
            list.Add(string.Format("81        10.3600        0.0000        0.0000"));
            list.Add(string.Format("82        10.5000        0.0000        0.0000"));
            list.Add(string.Format("83        10.6400        0.0000        0.0000"));
            list.Add(string.Format("84        10.7800        0.0000        0.0000"));
            list.Add(string.Format("85        10.9200        0.0000        0.0000"));
            list.Add(string.Format("86        11.0600        0.0000        0.0000"));
            list.Add(string.Format("87        11.2000        0.0000        0.0000"));
            list.Add(string.Format("88        11.3400        0.0000        0.0000"));
            list.Add(string.Format("89        11.4800        0.0000        0.0000"));
            list.Add(string.Format("90        11.6200        0.0000        0.0000"));
            list.Add(string.Format("91        11.7600        0.0000        0.0000"));
            list.Add(string.Format("92        11.9000        0.0000        0.0000"));
            list.Add(string.Format("93        12.0400        0.0000        0.0000"));
            list.Add(string.Format("94        12.1800        0.0000        0.0000"));
            list.Add(string.Format("95        12.3200        0.0000        0.0000"));
            list.Add(string.Format("96        12.4600        0.0000        0.0000"));
            list.Add(string.Format("97        12.6000        0.0000        0.0000"));
            list.Add(string.Format("98        12.8901        0.0000        0.0000"));
            list.Add(string.Format("99        13.0302        0.0000        0.0000"));
            list.Add(string.Format("100        13.1703        0.0000        0.0000"));
            list.Add(string.Format("101        13.3104        0.0000        0.0000"));
            list.Add(string.Format("102        13.4505        0.0000        0.0000"));
            list.Add(string.Format("103        13.5907        0.0000        0.0000"));
            list.Add(string.Format("104        13.7308        0.0000        0.0000"));
            list.Add(string.Format("105        13.8709        0.0000        0.0000"));
            list.Add(string.Format("106        14.0110        0.0000        0.0000"));
            list.Add(string.Format("107        14.1511        0.0000        0.0000"));
            list.Add(string.Format("108        14.2912        0.0000        0.0000"));
            list.Add(string.Format("109        14.4313        0.0000        0.0000"));
            list.Add(string.Format("110        14.5714        0.0000        0.0000"));
            list.Add(string.Format("111        14.7115        0.0000        0.0000"));
            list.Add(string.Format("112        14.8516        0.0000        0.0000"));
            list.Add(string.Format("113        14.9918        0.0000        0.0000"));
            list.Add(string.Format("114        15.1319        0.0000        0.0000"));
            list.Add(string.Format("115        15.2720        0.0000        0.0000"));
            list.Add(string.Format("116        15.4121        0.0000        0.0000"));
            list.Add(string.Format("117        15.5522        0.0000        0.0000"));
            list.Add(string.Format("118        15.6923        0.0000        0.0000"));
            list.Add(string.Format("119        15.8324        0.0000        0.0000"));
            list.Add(string.Format("120        15.9725        0.0000        0.0000"));
            list.Add(string.Format("121        16.1126        0.0000        0.0000"));
            list.Add(string.Format("122        16.2527        0.0000        0.0000"));
            list.Add(string.Format("123        16.3929        0.0000        0.0000"));
            list.Add(string.Format("124        16.5330        0.0000        0.0000"));
            list.Add(string.Format("125        16.6731        0.0000        0.0000"));
            list.Add(string.Format("126        16.8132        0.0000        0.0000"));
            list.Add(string.Format("127        16.9533        0.0000        0.0000"));
            list.Add(string.Format("128        17.0934        0.0000        0.0000"));
            list.Add(string.Format("129        17.2335        0.0000        0.0000"));
            list.Add(string.Format("130        17.3736        0.0000        0.0000"));
            list.Add(string.Format("131        17.5137        0.0000        0.0000"));
            list.Add(string.Format("132        17.6538        0.0000        0.0000"));
            list.Add(string.Format("133        17.7940        0.0000        0.0000"));
            list.Add(string.Format("134        17.9341        0.0000        0.0000"));
            list.Add(string.Format("135        18.0742        0.0000        0.0000"));
            list.Add(string.Format("136        18.2143        0.0000        0.0000"));
            list.Add(string.Format("137        18.3544        0.0000        0.0000"));
            list.Add(string.Format("138        18.4945        0.0000        0.0000"));
            list.Add(string.Format("139        18.6346        0.0000        0.0000"));
            list.Add(string.Format("140        18.7747        0.0000        0.0000"));
            list.Add(string.Format("141        18.9148        0.0000        0.0000"));
            list.Add(string.Format("142        19.0549        0.0000        0.0000"));
            list.Add(string.Format("143        19.1951        0.0000        0.0000"));
            list.Add(string.Format("144        19.3352        0.0000        0.0000"));
            list.Add(string.Format("145        19.4753        0.0000        0.0000"));
            list.Add(string.Format("146        19.6154        0.0000        0.0000"));
            list.Add(string.Format("147        19.7555        0.0000        0.0000"));
            list.Add(string.Format("148        19.8956        0.0000        0.0000"));
            list.Add(string.Format("149        20.0357        0.0000        0.0000"));
            list.Add(string.Format("150        20.1758        0.0000        0.0000"));
            list.Add(string.Format("151        20.3159        0.0000        0.0000"));
            list.Add(string.Format("152        20.4560        0.0000        0.0000"));
            list.Add(string.Format("153        20.5962        0.0000        0.0000"));
            list.Add(string.Format("154        20.7363        0.0000        0.0000"));
            list.Add(string.Format("155        20.8764        0.0000        0.0000"));
            list.Add(string.Format("156        21.0165        0.0000        0.0000"));
            list.Add(string.Format("157        21.1566        0.0000        0.0000"));
            list.Add(string.Format("158        21.2967        0.0000        0.0000"));
            list.Add(string.Format("159        21.4368        0.0000        0.0000"));
            list.Add(string.Format("160        21.5769        0.0000        0.0000"));
            list.Add(string.Format("161        21.7170        0.0000        0.0000"));
            list.Add(string.Format("162        21.8571        0.0000        0.0000"));
            list.Add(string.Format("163        21.9973        0.0000        0.0000"));
            list.Add(string.Format("164        22.1374        0.0000        0.0000"));
            list.Add(string.Format("165        22.2775        0.0000        0.0000"));
            list.Add(string.Format("166        22.4176        0.0000        0.0000"));
            list.Add(string.Format("167        22.5577        0.0000        0.0000"));
            list.Add(string.Format("168        22.6978        0.0000        0.0000"));
            list.Add(string.Format("169        22.8379        0.0000        0.0000"));
            list.Add(string.Format("170        22.9780        0.0000        0.0000"));
            list.Add(string.Format("171        23.1181        0.0000        0.0000"));
            list.Add(string.Format("172        23.2582        0.0000        0.0000"));
            list.Add(string.Format("173        23.3984        0.0000        0.0000"));
            list.Add(string.Format("174        23.5385        0.0000        0.0000"));
            list.Add(string.Format("175        23.6786        0.0000        0.0000"));
            list.Add(string.Format("176        23.8187        0.0000        0.0000"));
            list.Add(string.Format("177        23.9588        0.0000        0.0000"));
            list.Add(string.Format("178        24.0989        0.0000        0.0000"));
            list.Add(string.Format("179        24.2390        0.0000        0.0000"));
            list.Add(string.Format("180        24.3791        0.0000        0.0000"));
            list.Add(string.Format("181        24.5192        0.0000        0.0000"));
            list.Add(string.Format("182        24.6593        0.0000        0.0000"));
            list.Add(string.Format("183        24.7994        0.0000        0.0000"));
            list.Add(string.Format("184        24.9396        0.0000        0.0000"));
            list.Add(string.Format("185        25.0797        0.0000        0.0000"));
            list.Add(string.Format("186        25.2198        0.0000        0.0000"));
            list.Add(string.Format("187        25.3599        0.0000        0.0000"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1        4        5"));
            list.Add(string.Format("2        5        6"));
            list.Add(string.Format("3        1        4"));
            list.Add(string.Format("4        2        5"));
            list.Add(string.Format("5        2        7"));
            list.Add(string.Format("6        3        6"));
            list.Add(string.Format("7        1        8"));
            list.Add(string.Format("8        2        98"));
            list.Add(string.Format("9        8        9"));
            list.Add(string.Format("10        9        10"));
            list.Add(string.Format("11        10        11"));
            list.Add(string.Format("12        11        12"));
            list.Add(string.Format("13        12        13"));
            list.Add(string.Format("14        13        14"));
            list.Add(string.Format("15        14        15"));
            list.Add(string.Format("16        15        16"));
            list.Add(string.Format("17        16        17"));
            list.Add(string.Format("18        17        18"));
            list.Add(string.Format("19        18        19"));
            list.Add(string.Format("20        19        20"));
            list.Add(string.Format("21        20        21"));
            list.Add(string.Format("22        21        22"));
            list.Add(string.Format("23        22        23"));
            list.Add(string.Format("24        23        24"));
            list.Add(string.Format("25        24        25"));
            list.Add(string.Format("26        25        26"));
            list.Add(string.Format("27        26        27"));
            list.Add(string.Format("28        27        28"));
            list.Add(string.Format("29        28        29"));
            list.Add(string.Format("30        29        30"));
            list.Add(string.Format("31        30        31"));
            list.Add(string.Format("32        31        32"));
            list.Add(string.Format("33        32        33"));
            list.Add(string.Format("34        33        34"));
            list.Add(string.Format("35        34        35"));
            list.Add(string.Format("36        35        36"));
            list.Add(string.Format("37        36        37"));
            list.Add(string.Format("38        37        38"));
            list.Add(string.Format("39        38        39"));
            list.Add(string.Format("40        39        40"));
            list.Add(string.Format("41        40        41"));
            list.Add(string.Format("42        41        42"));
            list.Add(string.Format("43        42        43"));
            list.Add(string.Format("44        43        44"));
            list.Add(string.Format("45        44        45"));
            list.Add(string.Format("46        45        46"));
            list.Add(string.Format("47        46        47"));
            list.Add(string.Format("48        47        48"));
            list.Add(string.Format("49        48        49"));
            list.Add(string.Format("50        49        50"));
            list.Add(string.Format("51        50        51"));
            list.Add(string.Format("52        51        52"));
            list.Add(string.Format("53        52        53"));
            list.Add(string.Format("54        53        54"));
            list.Add(string.Format("55        54        55"));
            list.Add(string.Format("56        55        56"));
            list.Add(string.Format("57        56        57"));
            list.Add(string.Format("58        57        58"));
            list.Add(string.Format("59        58        59"));
            list.Add(string.Format("60        59        60"));
            list.Add(string.Format("61        60        61"));
            list.Add(string.Format("62        61        62"));
            list.Add(string.Format("63        62        63"));
            list.Add(string.Format("64        63        64"));
            list.Add(string.Format("65        64        65"));
            list.Add(string.Format("66        65        66"));
            list.Add(string.Format("67        66        67"));
            list.Add(string.Format("68        67        68"));
            list.Add(string.Format("69        68        69"));
            list.Add(string.Format("70        69        70"));
            list.Add(string.Format("71        70        71"));
            list.Add(string.Format("72        71        72"));
            list.Add(string.Format("73        72        73"));
            list.Add(string.Format("74        73        74"));
            list.Add(string.Format("75        74        75"));
            list.Add(string.Format("76        75        76"));
            list.Add(string.Format("77        76        77"));
            list.Add(string.Format("78        77        78"));
            list.Add(string.Format("79        78        79"));
            list.Add(string.Format("80        79        80"));
            list.Add(string.Format("81        80        81"));
            list.Add(string.Format("82        81        82"));
            list.Add(string.Format("83        82        83"));
            list.Add(string.Format("84        83        84"));
            list.Add(string.Format("85        84        85"));
            list.Add(string.Format("86        85        86"));
            list.Add(string.Format("87        86        87"));
            list.Add(string.Format("88        87        88"));
            list.Add(string.Format("89        88        89"));
            list.Add(string.Format("90        89        90"));
            list.Add(string.Format("91        90        91"));
            list.Add(string.Format("92        91        92"));
            list.Add(string.Format("93        92        93"));
            list.Add(string.Format("94        93        94"));
            list.Add(string.Format("95        94        95"));
            list.Add(string.Format("96        95        96"));
            list.Add(string.Format("97        96        97"));
            list.Add(string.Format("98        97        98"));
            list.Add(string.Format("99        98        99"));
            list.Add(string.Format("100        99        100"));
            list.Add(string.Format("101        100        101"));
            list.Add(string.Format("102        101        102"));
            list.Add(string.Format("103        102        103"));
            list.Add(string.Format("104        103        104"));
            list.Add(string.Format("105        104        105"));
            list.Add(string.Format("106        105        106"));
            list.Add(string.Format("107        106        107"));
            list.Add(string.Format("108        107        108"));
            list.Add(string.Format("109        108        109"));
            list.Add(string.Format("110        109        110"));
            list.Add(string.Format("111        110        111"));
            list.Add(string.Format("112        111        112"));
            list.Add(string.Format("113        112        113"));
            list.Add(string.Format("114        113        114"));
            list.Add(string.Format("115        114        115"));
            list.Add(string.Format("116        115        116"));
            list.Add(string.Format("117        116        117"));
            list.Add(string.Format("118        117        118"));
            list.Add(string.Format("119        118        119"));
            list.Add(string.Format("120        119        120"));
            list.Add(string.Format("121        120        121"));
            list.Add(string.Format("122        121        122"));
            list.Add(string.Format("123        122        123"));
            list.Add(string.Format("124        123        124"));
            list.Add(string.Format("125        124        125"));
            list.Add(string.Format("126        125        126"));
            list.Add(string.Format("127        126        127"));
            list.Add(string.Format("128        127        128"));
            list.Add(string.Format("129        128        129"));
            list.Add(string.Format("130        129        130"));
            list.Add(string.Format("131        130        131"));
            list.Add(string.Format("132        131        132"));
            list.Add(string.Format("133        132        133"));
            list.Add(string.Format("134        133        134"));
            list.Add(string.Format("135        134        135"));
            list.Add(string.Format("136        135        136"));
            list.Add(string.Format("137        136        137"));
            list.Add(string.Format("138        137        138"));
            list.Add(string.Format("139        138        139"));
            list.Add(string.Format("140        139        140"));
            list.Add(string.Format("141        140        141"));
            list.Add(string.Format("142        141        142"));
            list.Add(string.Format("143        142        143"));
            list.Add(string.Format("144        143        144"));
            list.Add(string.Format("145        144        145"));
            list.Add(string.Format("146        145        146"));
            list.Add(string.Format("147        146        147"));
            list.Add(string.Format("148        147        148"));
            list.Add(string.Format("149        148        149"));
            list.Add(string.Format("150        149        150"));
            list.Add(string.Format("151        150        151"));
            list.Add(string.Format("152        151        152"));
            list.Add(string.Format("153        152        153"));
            list.Add(string.Format("154        153        154"));
            list.Add(string.Format("155        154        155"));
            list.Add(string.Format("156        155        156"));
            list.Add(string.Format("157        156        157"));
            list.Add(string.Format("158        157        158"));
            list.Add(string.Format("159        158        159"));
            list.Add(string.Format("160        159        160"));
            list.Add(string.Format("161        160        161"));
            list.Add(string.Format("162        161        162"));
            list.Add(string.Format("163        162        163"));
            list.Add(string.Format("164        163        164"));
            list.Add(string.Format("165        164        165"));
            list.Add(string.Format("166        165        166"));
            list.Add(string.Format("167        166        167"));
            list.Add(string.Format("168        167        168"));
            list.Add(string.Format("169        168        169"));
            list.Add(string.Format("170        169        170"));
            list.Add(string.Format("171        170        171"));
            list.Add(string.Format("172        171        172"));
            list.Add(string.Format("173        172        173"));
            list.Add(string.Format("174        173        174"));
            list.Add(string.Format("175        174        175"));
            list.Add(string.Format("176        175        176"));
            list.Add(string.Format("177        176        177"));
            list.Add(string.Format("178        177        178"));
            list.Add(string.Format("179        178        179"));
            list.Add(string.Format("180        179        180"));
            list.Add(string.Format("181        180        181"));
            list.Add(string.Format("182        181        182"));
            list.Add(string.Format("183        182        183"));
            list.Add(string.Format("184        183        184"));
            list.Add(string.Format("185        184        185"));
            list.Add(string.Format("186        185        186"));
            list.Add(string.Format("187        186        187"));
            list.Add(string.Format("188        187        3"));
            list.Add(string.Format("MEMBER PROPERTY"));
            list.Add(string.Format("5 7 TO 188 PRIS YD 0.8 ZD 1"));
            list.Add(string.Format("1 2 3 6 PRIS YD 0.7 ZD 1"));
            list.Add(string.Format("4 PRIS YD 0.5 ZD 1"));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 23.5616 ALL"));
            list.Add(string.Format("ALPHA 1e-005 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("1 TO 3 7 TO 187 FIXED BUT FX FZ MX MY MZ KFY 2093"));
            list.Add(string.Format("MEMBER RELEASE"));
            list.Add(string.Format("8 98 START FX FZ MX MY"));
            list.Add(string.Format("8 98 END FX FZ MX MY"));
            list.Add(string.Format("LOAD 1 LOADTYPE Dead  TITLE DL+SIDL"));
            list.Add(string.Format("SELFWEIGHT Y -1 "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 2 UNI GY -12.5"));
            list.Add(string.Format("1 2 UNI GY -10"));
            list.Add(string.Format("LOAD 2 LOADTYPE Dead  TITLE WATER LOAD"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 4 TRAP GX -20 0"));
            list.Add(string.Format("4 6 TRAP GX 20 0"));
            list.Add(string.Format("5 7 TO 196 UNI GY -20"));
            list.Add(string.Format("LOAD 3 LOADTYPE Dead  TITLE SURCHARGE LOAD"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("6 UNI GX -10.8"));
            list.Add(string.Format("3 UNI GX 10.8"));
            list.Add(string.Format("LOAD 4 LOADTYPE Dead  TITLE EARTH PRESSURE BS."));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 TRAP GX 55.8 0"));
            list.Add(string.Format("6 TRAP GX -55.8 0"));
            list.Add(string.Format("LOAD 5 LOADTYPE Dead  TITLE EARTH PRESSURE OS."));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 TRAP GX 55.8 0"));
            list.Add(string.Format("LOAD 6 LOADTYPE Dead  TITLE BRAKING LOAD"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("4 6 FX -23.53"));
            list.Add(string.Format("LOAD 7 LOADTYPE Dead  TITLE EARTH CUSHION"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 2 UNI GY -0.2"));
            list.Add(string.Format("LOAD 8 LOADTYPE None  TITLE TEMPERATURE RISE"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("4 FX 845 MZ -217"));
            list.Add(string.Format("6 FX -845 MZ 217"));
            list.Add(string.Format("LOAD 9 LOADTYPE None  TITLE TEMPERATURE FALL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("4 FX -612 MZ 39"));
            list.Add(string.Format("6 FX 612 MZ -39"));
            list.Add(string.Format("LOAD 10 LOADTYPE Dead  TITLE SIDL WC"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 2 UNI GY -1.65"));
            list.Add(string.Format("LOAD COMB 11 COMBINATION LOAD CASE 11"));
            list.Add(string.Format("1 1.35 10 1.75 "));
            list.Add(string.Format("LOAD COMB 12 COMBINATION LOAD CASE 12"));
            list.Add(string.Format("1 1.35 10 1.75 5 1.5 "));
            list.Add(string.Format("LOAD COMB 13 COMBINATION LOAD CASE 13"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.5 "));
            list.Add(string.Format("LOAD COMB 14 COMBINATION LOAD CASE 14"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.5 7 1.35 "));
            list.Add(string.Format("LOAD COMB 15 COMBINATION LOAD CASE 15"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.5 7 1.35 6 1.5 "));
            list.Add(string.Format("LOAD COMB 16 COMBINATION LOAD CASE 16"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.5 7 1.35 3 1.2 6 1.5 "));
            list.Add(string.Format("LOAD COMB 17 REACTION"));
            list.Add(string.Format("1 1.0 2 1.0 4 1.0 7 1.0 10 1.0 "));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));

            //string frm = "Multicell Dead Loads.txt";

            File.WriteAllLines(Input_File_DL, list.ToArray());
        }
        public void Create_Data_MulticellLiveLoad()
        {

            List<string> list = new List<string>();
            list.Add(string.Format("ASTRA SPACE MULTICELL BOX CULVERT"));
            list.Add(string.Format("UNIT METER KN"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1        0.0000        0.0000        0.0000"));
            list.Add(string.Format("2        12.7500        0.0000        0.0000"));
            list.Add(string.Format("3        25.5000        0.0000        0.0000"));
            list.Add(string.Format("4        0.0000        5.0700        0.0000"));
            list.Add(string.Format("5        12.7500        5.0700        0.0000"));
            list.Add(string.Format("6        25.5000        5.0700        0.0000"));
            list.Add(string.Format("7        12.7400        0.0000        0.0000"));
            list.Add(string.Format("8        0.1400        0.0000        0.0000"));
            list.Add(string.Format("9        0.2800        0.0000        0.0000"));
            list.Add(string.Format("10        0.4200        0.0000        0.0000"));
            list.Add(string.Format("11        0.5600        0.0000        0.0000"));
            list.Add(string.Format("12        0.7000        0.0000        0.0000"));
            list.Add(string.Format("13        0.8400        0.0000        0.0000"));
            list.Add(string.Format("14        0.9800        0.0000        0.0000"));
            list.Add(string.Format("15        1.1200        0.0000        0.0000"));
            list.Add(string.Format("16        1.2600        0.0000        0.0000"));
            list.Add(string.Format("17        1.4000        0.0000        0.0000"));
            list.Add(string.Format("18        1.5400        0.0000        0.0000"));
            list.Add(string.Format("19        1.6800        0.0000        0.0000"));
            list.Add(string.Format("20        1.8200        0.0000        0.0000"));
            list.Add(string.Format("21        1.9600        0.0000        0.0000"));
            list.Add(string.Format("22        2.1000        0.0000        0.0000"));
            list.Add(string.Format("23        2.2400        0.0000        0.0000"));
            list.Add(string.Format("24        2.3800        0.0000        0.0000"));
            list.Add(string.Format("25        2.5200        0.0000        0.0000"));
            list.Add(string.Format("26        2.6600        0.0000        0.0000"));
            list.Add(string.Format("27        2.8000        0.0000        0.0000"));
            list.Add(string.Format("28        2.9400        0.0000        0.0000"));
            list.Add(string.Format("29        3.0800        0.0000        0.0000"));
            list.Add(string.Format("30        3.2200        0.0000        0.0000"));
            list.Add(string.Format("31        3.3600        0.0000        0.0000"));
            list.Add(string.Format("32        3.5000        0.0000        0.0000"));
            list.Add(string.Format("33        3.6400        0.0000        0.0000"));
            list.Add(string.Format("34        3.7800        0.0000        0.0000"));
            list.Add(string.Format("35        3.9200        0.0000        0.0000"));
            list.Add(string.Format("36        4.0600        0.0000        0.0000"));
            list.Add(string.Format("37        4.2000        0.0000        0.0000"));
            list.Add(string.Format("38        4.3400        0.0000        0.0000"));
            list.Add(string.Format("39        4.4800        0.0000        0.0000"));
            list.Add(string.Format("40        4.6200        0.0000        0.0000"));
            list.Add(string.Format("41        4.7600        0.0000        0.0000"));
            list.Add(string.Format("42        4.9000        0.0000        0.0000"));
            list.Add(string.Format("43        5.0400        0.0000        0.0000"));
            list.Add(string.Format("44        5.1800        0.0000        0.0000"));
            list.Add(string.Format("45        5.3200        0.0000        0.0000"));
            list.Add(string.Format("46        5.4600        0.0000        0.0000"));
            list.Add(string.Format("47        5.6000        0.0000        0.0000"));
            list.Add(string.Format("48        5.7400        0.0000        0.0000"));
            list.Add(string.Format("49        5.8800        0.0000        0.0000"));
            list.Add(string.Format("50        6.0200        0.0000        0.0000"));
            list.Add(string.Format("51        6.1600        0.0000        0.0000"));
            list.Add(string.Format("52        6.3000        0.0000        0.0000"));
            list.Add(string.Format("53        6.4400        0.0000        0.0000"));
            list.Add(string.Format("54        6.5800        0.0000        0.0000"));
            list.Add(string.Format("55        6.7200        0.0000        0.0000"));
            list.Add(string.Format("56        6.8600        0.0000        0.0000"));
            list.Add(string.Format("57        7.0000        0.0000        0.0000"));
            list.Add(string.Format("58        7.1400        0.0000        0.0000"));
            list.Add(string.Format("59        7.2800        0.0000        0.0000"));
            list.Add(string.Format("60        7.4200        0.0000        0.0000"));
            list.Add(string.Format("61        7.5600        0.0000        0.0000"));
            list.Add(string.Format("62        7.7000        0.0000        0.0000"));
            list.Add(string.Format("63        7.8400        0.0000        0.0000"));
            list.Add(string.Format("64        7.9800        0.0000        0.0000"));
            list.Add(string.Format("65        8.1200        0.0000        0.0000"));
            list.Add(string.Format("66        8.2600        0.0000        0.0000"));
            list.Add(string.Format("67        8.4000        0.0000        0.0000"));
            list.Add(string.Format("68        8.5400        0.0000        0.0000"));
            list.Add(string.Format("69        8.6800        0.0000        0.0000"));
            list.Add(string.Format("70        8.8200        0.0000        0.0000"));
            list.Add(string.Format("71        8.9600        0.0000        0.0000"));
            list.Add(string.Format("72        9.1000        0.0000        0.0000"));
            list.Add(string.Format("73        9.2400        0.0000        0.0000"));
            list.Add(string.Format("74        9.3800        0.0000        0.0000"));
            list.Add(string.Format("75        9.5200        0.0000        0.0000"));
            list.Add(string.Format("76        9.6600        0.0000        0.0000"));
            list.Add(string.Format("77        9.8000        0.0000        0.0000"));
            list.Add(string.Format("78        9.9400        0.0000        0.0000"));
            list.Add(string.Format("79        10.0800        0.0000        0.0000"));
            list.Add(string.Format("80        10.2200        0.0000        0.0000"));
            list.Add(string.Format("81        10.3600        0.0000        0.0000"));
            list.Add(string.Format("82        10.5000        0.0000        0.0000"));
            list.Add(string.Format("83        10.6400        0.0000        0.0000"));
            list.Add(string.Format("84        10.7800        0.0000        0.0000"));
            list.Add(string.Format("85        10.9200        0.0000        0.0000"));
            list.Add(string.Format("86        11.0600        0.0000        0.0000"));
            list.Add(string.Format("87        11.2000        0.0000        0.0000"));
            list.Add(string.Format("88        11.3400        0.0000        0.0000"));
            list.Add(string.Format("89        11.4800        0.0000        0.0000"));
            list.Add(string.Format("90        11.6200        0.0000        0.0000"));
            list.Add(string.Format("91        11.7600        0.0000        0.0000"));
            list.Add(string.Format("92        11.9000        0.0000        0.0000"));
            list.Add(string.Format("93        12.0400        0.0000        0.0000"));
            list.Add(string.Format("94        12.1800        0.0000        0.0000"));
            list.Add(string.Format("95        12.3200        0.0000        0.0000"));
            list.Add(string.Format("96        12.4600        0.0000        0.0000"));
            list.Add(string.Format("97        12.6000        0.0000        0.0000"));
            list.Add(string.Format("98        12.8901        0.0000        0.0000"));
            list.Add(string.Format("99        13.0302        0.0000        0.0000"));
            list.Add(string.Format("100        13.1703        0.0000        0.0000"));
            list.Add(string.Format("101        13.3104        0.0000        0.0000"));
            list.Add(string.Format("102        13.4505        0.0000        0.0000"));
            list.Add(string.Format("103        13.5907        0.0000        0.0000"));
            list.Add(string.Format("104        13.7308        0.0000        0.0000"));
            list.Add(string.Format("105        13.8709        0.0000        0.0000"));
            list.Add(string.Format("106        14.0110        0.0000        0.0000"));
            list.Add(string.Format("107        14.1511        0.0000        0.0000"));
            list.Add(string.Format("108        14.2912        0.0000        0.0000"));
            list.Add(string.Format("109        14.4313        0.0000        0.0000"));
            list.Add(string.Format("110        14.5714        0.0000        0.0000"));
            list.Add(string.Format("111        14.7115        0.0000        0.0000"));
            list.Add(string.Format("112        14.8516        0.0000        0.0000"));
            list.Add(string.Format("113        14.9918        0.0000        0.0000"));
            list.Add(string.Format("114        15.1319        0.0000        0.0000"));
            list.Add(string.Format("115        15.2720        0.0000        0.0000"));
            list.Add(string.Format("116        15.4121        0.0000        0.0000"));
            list.Add(string.Format("117        15.5522        0.0000        0.0000"));
            list.Add(string.Format("118        15.6923        0.0000        0.0000"));
            list.Add(string.Format("119        15.8324        0.0000        0.0000"));
            list.Add(string.Format("120        15.9725        0.0000        0.0000"));
            list.Add(string.Format("121        16.1126        0.0000        0.0000"));
            list.Add(string.Format("122        16.2527        0.0000        0.0000"));
            list.Add(string.Format("123        16.3929        0.0000        0.0000"));
            list.Add(string.Format("124        16.5330        0.0000        0.0000"));
            list.Add(string.Format("125        16.6731        0.0000        0.0000"));
            list.Add(string.Format("126        16.8132        0.0000        0.0000"));
            list.Add(string.Format("127        16.9533        0.0000        0.0000"));
            list.Add(string.Format("128        17.0934        0.0000        0.0000"));
            list.Add(string.Format("129        17.2335        0.0000        0.0000"));
            list.Add(string.Format("130        17.3736        0.0000        0.0000"));
            list.Add(string.Format("131        17.5137        0.0000        0.0000"));
            list.Add(string.Format("132        17.6538        0.0000        0.0000"));
            list.Add(string.Format("133        17.7940        0.0000        0.0000"));
            list.Add(string.Format("134        17.9341        0.0000        0.0000"));
            list.Add(string.Format("135        18.0742        0.0000        0.0000"));
            list.Add(string.Format("136        18.2143        0.0000        0.0000"));
            list.Add(string.Format("137        18.3544        0.0000        0.0000"));
            list.Add(string.Format("138        18.4945        0.0000        0.0000"));
            list.Add(string.Format("139        18.6346        0.0000        0.0000"));
            list.Add(string.Format("140        18.7747        0.0000        0.0000"));
            list.Add(string.Format("141        18.9148        0.0000        0.0000"));
            list.Add(string.Format("142        19.0549        0.0000        0.0000"));
            list.Add(string.Format("143        19.1951        0.0000        0.0000"));
            list.Add(string.Format("144        19.3352        0.0000        0.0000"));
            list.Add(string.Format("145        19.4753        0.0000        0.0000"));
            list.Add(string.Format("146        19.6154        0.0000        0.0000"));
            list.Add(string.Format("147        19.7555        0.0000        0.0000"));
            list.Add(string.Format("148        19.8956        0.0000        0.0000"));
            list.Add(string.Format("149        20.0357        0.0000        0.0000"));
            list.Add(string.Format("150        20.1758        0.0000        0.0000"));
            list.Add(string.Format("151        20.3159        0.0000        0.0000"));
            list.Add(string.Format("152        20.4560        0.0000        0.0000"));
            list.Add(string.Format("153        20.5962        0.0000        0.0000"));
            list.Add(string.Format("154        20.7363        0.0000        0.0000"));
            list.Add(string.Format("155        20.8764        0.0000        0.0000"));
            list.Add(string.Format("156        21.0165        0.0000        0.0000"));
            list.Add(string.Format("157        21.1566        0.0000        0.0000"));
            list.Add(string.Format("158        21.2967        0.0000        0.0000"));
            list.Add(string.Format("159        21.4368        0.0000        0.0000"));
            list.Add(string.Format("160        21.5769        0.0000        0.0000"));
            list.Add(string.Format("161        21.7170        0.0000        0.0000"));
            list.Add(string.Format("162        21.8571        0.0000        0.0000"));
            list.Add(string.Format("163        21.9973        0.0000        0.0000"));
            list.Add(string.Format("164        22.1374        0.0000        0.0000"));
            list.Add(string.Format("165        22.2775        0.0000        0.0000"));
            list.Add(string.Format("166        22.4176        0.0000        0.0000"));
            list.Add(string.Format("167        22.5577        0.0000        0.0000"));
            list.Add(string.Format("168        22.6978        0.0000        0.0000"));
            list.Add(string.Format("169        22.8379        0.0000        0.0000"));
            list.Add(string.Format("170        22.9780        0.0000        0.0000"));
            list.Add(string.Format("171        23.1181        0.0000        0.0000"));
            list.Add(string.Format("172        23.2582        0.0000        0.0000"));
            list.Add(string.Format("173        23.3984        0.0000        0.0000"));
            list.Add(string.Format("174        23.5385        0.0000        0.0000"));
            list.Add(string.Format("175        23.6786        0.0000        0.0000"));
            list.Add(string.Format("176        23.8187        0.0000        0.0000"));
            list.Add(string.Format("177        23.9588        0.0000        0.0000"));
            list.Add(string.Format("178        24.0989        0.0000        0.0000"));
            list.Add(string.Format("179        24.2390        0.0000        0.0000"));
            list.Add(string.Format("180        24.3791        0.0000        0.0000"));
            list.Add(string.Format("181        24.5192        0.0000        0.0000"));
            list.Add(string.Format("182        24.6593        0.0000        0.0000"));
            list.Add(string.Format("183        24.7994        0.0000        0.0000"));
            list.Add(string.Format("184        24.9396        0.0000        0.0000"));
            list.Add(string.Format("185        25.0797        0.0000        0.0000"));
            list.Add(string.Format("186        25.2198        0.0000        0.0000"));
            list.Add(string.Format("187        25.3599        0.0000        0.0000"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1        4        5"));
            list.Add(string.Format("2        5        6"));
            list.Add(string.Format("3        1        4"));
            list.Add(string.Format("4        2        5"));
            list.Add(string.Format("5        2        7"));
            list.Add(string.Format("6        3        6"));
            list.Add(string.Format("7        1        8"));
            list.Add(string.Format("8        2        98"));
            list.Add(string.Format("9        8        9"));
            list.Add(string.Format("10        9        10"));
            list.Add(string.Format("11        10        11"));
            list.Add(string.Format("12        11        12"));
            list.Add(string.Format("13        12        13"));
            list.Add(string.Format("14        13        14"));
            list.Add(string.Format("15        14        15"));
            list.Add(string.Format("16        15        16"));
            list.Add(string.Format("17        16        17"));
            list.Add(string.Format("18        17        18"));
            list.Add(string.Format("19        18        19"));
            list.Add(string.Format("20        19        20"));
            list.Add(string.Format("21        20        21"));
            list.Add(string.Format("22        21        22"));
            list.Add(string.Format("23        22        23"));
            list.Add(string.Format("24        23        24"));
            list.Add(string.Format("25        24        25"));
            list.Add(string.Format("26        25        26"));
            list.Add(string.Format("27        26        27"));
            list.Add(string.Format("28        27        28"));
            list.Add(string.Format("29        28        29"));
            list.Add(string.Format("30        29        30"));
            list.Add(string.Format("31        30        31"));
            list.Add(string.Format("32        31        32"));
            list.Add(string.Format("33        32        33"));
            list.Add(string.Format("34        33        34"));
            list.Add(string.Format("35        34        35"));
            list.Add(string.Format("36        35        36"));
            list.Add(string.Format("37        36        37"));
            list.Add(string.Format("38        37        38"));
            list.Add(string.Format("39        38        39"));
            list.Add(string.Format("40        39        40"));
            list.Add(string.Format("41        40        41"));
            list.Add(string.Format("42        41        42"));
            list.Add(string.Format("43        42        43"));
            list.Add(string.Format("44        43        44"));
            list.Add(string.Format("45        44        45"));
            list.Add(string.Format("46        45        46"));
            list.Add(string.Format("47        46        47"));
            list.Add(string.Format("48        47        48"));
            list.Add(string.Format("49        48        49"));
            list.Add(string.Format("50        49        50"));
            list.Add(string.Format("51        50        51"));
            list.Add(string.Format("52        51        52"));
            list.Add(string.Format("53        52        53"));
            list.Add(string.Format("54        53        54"));
            list.Add(string.Format("55        54        55"));
            list.Add(string.Format("56        55        56"));
            list.Add(string.Format("57        56        57"));
            list.Add(string.Format("58        57        58"));
            list.Add(string.Format("59        58        59"));
            list.Add(string.Format("60        59        60"));
            list.Add(string.Format("61        60        61"));
            list.Add(string.Format("62        61        62"));
            list.Add(string.Format("63        62        63"));
            list.Add(string.Format("64        63        64"));
            list.Add(string.Format("65        64        65"));
            list.Add(string.Format("66        65        66"));
            list.Add(string.Format("67        66        67"));
            list.Add(string.Format("68        67        68"));
            list.Add(string.Format("69        68        69"));
            list.Add(string.Format("70        69        70"));
            list.Add(string.Format("71        70        71"));
            list.Add(string.Format("72        71        72"));
            list.Add(string.Format("73        72        73"));
            list.Add(string.Format("74        73        74"));
            list.Add(string.Format("75        74        75"));
            list.Add(string.Format("76        75        76"));
            list.Add(string.Format("77        76        77"));
            list.Add(string.Format("78        77        78"));
            list.Add(string.Format("79        78        79"));
            list.Add(string.Format("80        79        80"));
            list.Add(string.Format("81        80        81"));
            list.Add(string.Format("82        81        82"));
            list.Add(string.Format("83        82        83"));
            list.Add(string.Format("84        83        84"));
            list.Add(string.Format("85        84        85"));
            list.Add(string.Format("86        85        86"));
            list.Add(string.Format("87        86        87"));
            list.Add(string.Format("88        87        88"));
            list.Add(string.Format("89        88        89"));
            list.Add(string.Format("90        89        90"));
            list.Add(string.Format("91        90        91"));
            list.Add(string.Format("92        91        92"));
            list.Add(string.Format("93        92        93"));
            list.Add(string.Format("94        93        94"));
            list.Add(string.Format("95        94        95"));
            list.Add(string.Format("96        95        96"));
            list.Add(string.Format("97        96        97"));
            list.Add(string.Format("98        97        7"));
            list.Add(string.Format("99        98        99"));
            list.Add(string.Format("100        99        100"));
            list.Add(string.Format("101        100        101"));
            list.Add(string.Format("102        101        102"));
            list.Add(string.Format("103        102        103"));
            list.Add(string.Format("104        103        104"));
            list.Add(string.Format("105        104        105"));
            list.Add(string.Format("106        105        106"));
            list.Add(string.Format("107        106        107"));
            list.Add(string.Format("108        107        108"));
            list.Add(string.Format("109        108        109"));
            list.Add(string.Format("110        109        110"));
            list.Add(string.Format("111        110        111"));
            list.Add(string.Format("112        111        112"));
            list.Add(string.Format("113        112        113"));
            list.Add(string.Format("114        113        114"));
            list.Add(string.Format("115        114        115"));
            list.Add(string.Format("116        115        116"));
            list.Add(string.Format("117        116        117"));
            list.Add(string.Format("118        117        118"));
            list.Add(string.Format("119        118        119"));
            list.Add(string.Format("120        119        120"));
            list.Add(string.Format("121        120        121"));
            list.Add(string.Format("122        121        122"));
            list.Add(string.Format("123        122        123"));
            list.Add(string.Format("124        123        124"));
            list.Add(string.Format("125        124        125"));
            list.Add(string.Format("126        125        126"));
            list.Add(string.Format("127        126        127"));
            list.Add(string.Format("128        127        128"));
            list.Add(string.Format("129        128        129"));
            list.Add(string.Format("130        129        130"));
            list.Add(string.Format("131        130        131"));
            list.Add(string.Format("132        131        132"));
            list.Add(string.Format("133        132        133"));
            list.Add(string.Format("134        133        134"));
            list.Add(string.Format("135        134        135"));
            list.Add(string.Format("136        135        136"));
            list.Add(string.Format("137        136        137"));
            list.Add(string.Format("138        137        138"));
            list.Add(string.Format("139        138        139"));
            list.Add(string.Format("140        139        140"));
            list.Add(string.Format("141        140        141"));
            list.Add(string.Format("142        141        142"));
            list.Add(string.Format("143        142        143"));
            list.Add(string.Format("144        143        144"));
            list.Add(string.Format("145        144        145"));
            list.Add(string.Format("146        145        146"));
            list.Add(string.Format("147        146        147"));
            list.Add(string.Format("148        147        148"));
            list.Add(string.Format("149        148        149"));
            list.Add(string.Format("150        149        150"));
            list.Add(string.Format("151        150        151"));
            list.Add(string.Format("152        151        152"));
            list.Add(string.Format("153        152        153"));
            list.Add(string.Format("154        153        154"));
            list.Add(string.Format("155        154        155"));
            list.Add(string.Format("156        155        156"));
            list.Add(string.Format("157        156        157"));
            list.Add(string.Format("158        157        158"));
            list.Add(string.Format("159        158        159"));
            list.Add(string.Format("160        159        160"));
            list.Add(string.Format("161        160        161"));
            list.Add(string.Format("162        161        162"));
            list.Add(string.Format("163        162        163"));
            list.Add(string.Format("164        163        164"));
            list.Add(string.Format("165        164        165"));
            list.Add(string.Format("166        165        166"));
            list.Add(string.Format("167        166        167"));
            list.Add(string.Format("168        167        168"));
            list.Add(string.Format("169        168        169"));
            list.Add(string.Format("170        169        170"));
            list.Add(string.Format("171        170        171"));
            list.Add(string.Format("172        171        172"));
            list.Add(string.Format("173        172        173"));
            list.Add(string.Format("174        173        174"));
            list.Add(string.Format("175        174        175"));
            list.Add(string.Format("176        175        176"));
            list.Add(string.Format("177        176        177"));
            list.Add(string.Format("178        177        178"));
            list.Add(string.Format("179        178        179"));
            list.Add(string.Format("180        179        180"));
            list.Add(string.Format("181        180        181"));
            list.Add(string.Format("182        181        182"));
            list.Add(string.Format("183        182        183"));
            list.Add(string.Format("184        183        184"));
            list.Add(string.Format("185        184        185"));
            list.Add(string.Format("186        185        186"));
            list.Add(string.Format("187        186        187"));
            list.Add(string.Format("188        187        3"));
            list.Add(string.Format("MEMBER PROPERTY"));
            list.Add(string.Format("5 7 TO 196 PRIS YD 0.9 ZD 1"));
            list.Add(string.Format("1 2 3 6 PRIS YD 0.8 ZD 1"));
            list.Add(string.Format("4 PRIS YD 0.6 ZD 1"));
            list.Add(string.Format("CONSTANTS "));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 23.5616  ALL"));
            list.Add(string.Format("ALPHA 1e-005 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("1 TO 3 7 TO 187 FIXED BUT FX FZ MX MY MZ KFY 2900"));
            list.Add(string.Format("*MEMBER RELEASE"));
            list.Add(string.Format("LOAD 1 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -46.37        0.00        0.38"));
            list.Add(string.Format("LOAD 2 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -29.91        0.00        0.63"));
            list.Add(string.Format("LOAD 3 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -22.32        0.12        0.88"));
            list.Add(string.Format("LOAD 4 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -19.88        0.37        1.13"));
            list.Add(string.Format("LOAD 5 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -18.02        0.62        1.38"));
            list.Add(string.Format("1 UNI GY        -30.34        0.00        0.62"));
            list.Add(string.Format("LOAD 6 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -16.54        0.87        1.63"));
            list.Add(string.Format("1 UNI GY        -22.54        0.11        0.87"));
            list.Add(string.Format("LOAD 7 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -15.34        1.12        1.88"));
            list.Add(string.Format("1 UNI GY        -19.97        0.36        1.12"));
            list.Add(string.Format("LOAD 8 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        1.37        2.13"));
            list.Add(string.Format("1 UNI GY        -18.08        0.61        1.37"));
            list.Add(string.Format("1 UNI GY        -30.9        0        0.609"));
            list.Add(string.Format("LOAD 9 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        1.62        2.38"));
            list.Add(string.Format("1 UNI GY        -16.59        0.86        1.62"));
            list.Add(string.Format("1 UNI GY        -22.8        0.097        0.859"));
            list.Add(string.Format("LOAD 10 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        1.87        2.63"));
            list.Add(string.Format("1 UNI GY        -15.38        1.11        1.87"));
            list.Add(string.Format("1 UNI GY        -20.1        0.347        1.109"));
            list.Add(string.Format("LOAD 11 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        2.12        2.88"));
            list.Add(string.Format("1 UNI GY        -14.57        1.36        2.12"));
            list.Add(string.Format("1 UNI GY        -18.16        0.60        1.36"));
            list.Add(string.Format("1 UNI GY        -31.4        0.0        0.6"));
            list.Add(string.Format("LOAD 12 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        2.37        3.13"));
            list.Add(string.Format("1 UNI GY        -14.57        1.61        2.37"));
            list.Add(string.Format("1 UNI GY        -16.65        0.85        1.61"));
            list.Add(string.Format("1 UNI GY        -23.1        0.1        0.8"));
            list.Add(string.Format("LOAD 13 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        2.62        3.38"));
            list.Add(string.Format("1 UNI GY        -14.57        1.86        2.62"));
            list.Add(string.Format("1 UNI GY        -15.43        1.10        1.86"));
            list.Add(string.Format("1 UNI GY        -20.2        0.3        1.1"));
            list.Add(string.Format("LOAD 14 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        2.87        3.63"));
            list.Add(string.Format("1 UNI GY        -14.57        2.11        2.87"));
            list.Add(string.Format("1 UNI GY        -14.57        1.35        2.11"));
            list.Add(string.Format("1 UNI GY        -18.2        0.6        1.3"));
            list.Add(string.Format("1 UNI GY        -32.0        0.0        0.6"));
            list.Add(string.Format("LOAD 15 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        3.12        3.88"));
            list.Add(string.Format("1 UNI GY        -14.57        2.36        3.12"));
            list.Add(string.Format("1 UNI GY        -14.57        1.60        2.36"));
            list.Add(string.Format("1 UNI GY        -16.7        0.8        1.6"));
            list.Add(string.Format("1 UNI GY        -23.4        0.1        0.8"));
            list.Add(string.Format("LOAD 16 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        3.37        4.13"));
            list.Add(string.Format("1 UNI GY        -14.57        2.61        3.37"));
            list.Add(string.Format("1 UNI GY        -14.57        1.85        2.61"));
            list.Add(string.Format("1 UNI GY        -15.5        1.1        1.8"));
            list.Add(string.Format("1 UNI GY        -20.3        0.3        1.1"));
            list.Add(string.Format("LOAD 17 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        3.62        4.38"));
            list.Add(string.Format("1 UNI GY        -14.57        2.86        3.62"));
            list.Add(string.Format("1 UNI GY        -14.57        2.10        2.86"));
            list.Add(string.Format("1 UNI GY        -14.6        1.3        2.1"));
            list.Add(string.Format("1 UNI GY        -18.3        0.6        1.3"));
            list.Add(string.Format("1 UNI GY        -32.6        0.0        0.6"));
            list.Add(string.Format("LOAD 18 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        3.87        4.63"));
            list.Add(string.Format("1 UNI GY        -14.57        3.11        3.87"));
            list.Add(string.Format("1 UNI GY        -14.57        2.35        3.11"));
            list.Add(string.Format("1 UNI GY        -14.6        1.6        2.3"));
            list.Add(string.Format("1 UNI GY        -16.8        0.8        1.6"));
            list.Add(string.Format("1 UNI GY        -23.7        0.1        0.8"));
            list.Add(string.Format("LOAD 19 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        4.12        4.88"));
            list.Add(string.Format("1 UNI GY        -14.57        3.36        4.12"));
            list.Add(string.Format("1 UNI GY        -14.57        2.60        3.36"));
            list.Add(string.Format("1 UNI GY        -14.6        1.8        2.6"));
            list.Add(string.Format("1 UNI GY        -15.5        1.1        1.8"));
            list.Add(string.Format("1 UNI GY        -20.4        0.3        1.1"));
            list.Add(string.Format("LOAD 20 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        4.37        5.13"));
            list.Add(string.Format("1 UNI GY        -14.57        3.61        4.37"));
            list.Add(string.Format("1 UNI GY        -14.57        2.85        3.61"));
            list.Add(string.Format("1 UNI GY        -14.6        2.1        2.8"));
            list.Add(string.Format("1 UNI GY        -14.6        1.3        2.1"));
            list.Add(string.Format("1 UNI GY        -18.4        0.6        1.3"));
            list.Add(string.Format("1 UNI GY        -33.2        -0.2        0.6"));
            list.Add(string.Format("LOAD 21 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        4.62        5.38"));
            list.Add(string.Format("1 UNI GY        -14.57        3.86        4.62"));
            list.Add(string.Format("1 UNI GY        -14.57        3.10        3.86"));
            list.Add(string.Format("1 UNI GY        -14.6        2.3        3.1"));
            list.Add(string.Format("1 UNI GY        -14.6        1.6        2.3"));
            list.Add(string.Format("1 UNI GY        -16.9        0.8        1.6"));
            list.Add(string.Format("1 UNI GY        -24.0        0.0        0.8"));
            list.Add(string.Format("LOAD 22 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        4.87        5.63"));
            list.Add(string.Format("1 UNI GY        -14.57        4.11        4.87"));
            list.Add(string.Format("1 UNI GY        -14.57        3.35        4.11"));
            list.Add(string.Format("1 UNI GY        -14.6        2.6        3.3"));
            list.Add(string.Format("1 UNI GY        -14.6        1.8        2.6"));
            list.Add(string.Format("1 UNI GY        -15.6        1.1        1.8"));
            list.Add(string.Format("1 UNI GY        -20.5        0.3        1.1"));
            list.Add(string.Format("LOAD 23 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        5.12        5.88"));
            list.Add(string.Format("1 UNI GY        -14.57        4.36        5.12"));
            list.Add(string.Format("1 UNI GY        -14.57        3.60        4.36"));
            list.Add(string.Format("1 UNI GY        -14.6        2.8        3.6"));
            list.Add(string.Format("1 UNI GY        -14.6        2.1        2.8"));
            list.Add(string.Format("1 UNI GY        -14.6        1.3        2.1"));
            list.Add(string.Format("1 UNI GY        -18.5        0.5        1.3"));
            list.Add(string.Format("LOAD 24 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        5.37        6.13"));
            list.Add(string.Format("1 UNI GY        -14.57        4.61        5.37"));
            list.Add(string.Format("1 UNI GY        -14.57        3.85        4.61"));
            list.Add(string.Format("1 UNI GY        -14.6        3.1        3.8"));
            list.Add(string.Format("1 UNI GY        -14.6        2.3        3.1"));
            list.Add(string.Format("1 UNI GY        -14.6        1.6        2.3"));
            list.Add(string.Format("1 UNI GY        -16.9        0.8        1.6"));
            list.Add(string.Format("LOAD 25 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        5.62        6.38"));
            list.Add(string.Format("1 UNI GY        -14.57        4.86        5.62"));
            list.Add(string.Format("1 UNI GY        -14.57        4.10        4.86"));
            list.Add(string.Format("1 UNI GY        -14.6        3.3        4.1"));
            list.Add(string.Format("1 UNI GY        -14.6        2.6        3.3"));
            list.Add(string.Format("1 UNI GY        -14.6        1.8        2.6"));
            list.Add(string.Format("1 UNI GY        -15.6        1.0        1.8"));
            list.Add(string.Format("LOAD 26 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        5.87        6.63"));
            list.Add(string.Format("1 UNI GY        -14.57        5.11        5.87"));
            list.Add(string.Format("1 UNI GY        -14.57        4.35        5.11"));
            list.Add(string.Format("1 UNI GY        -14.6        3.6        4.3"));
            list.Add(string.Format("1 UNI GY        -14.6        2.8        3.6"));
            list.Add(string.Format("1 UNI GY        -14.6        2.1        2.8"));
            list.Add(string.Format("1 UNI GY        -14.6        1.3        2.1"));
            list.Add(string.Format("LOAD 27 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        6.12        6.88"));
            list.Add(string.Format("1 UNI GY        -14.57        5.36        6.12"));
            list.Add(string.Format("1 UNI GY        -14.57        4.60        5.36"));
            list.Add(string.Format("1 UNI GY        -14.6        3.8        4.6"));
            list.Add(string.Format("1 UNI GY        -14.6        3.1        3.8"));
            list.Add(string.Format("1 UNI GY        -14.6        2.3        3.1"));
            list.Add(string.Format("1 UNI GY        -14.6        1.5        2.3"));
            list.Add(string.Format("LOAD 28 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        6.37        7.13"));
            list.Add(string.Format("1 UNI GY        -14.57        5.61        6.37"));
            list.Add(string.Format("1 UNI GY        -14.57        4.85        5.61"));
            list.Add(string.Format("1 UNI GY        -14.6        4.1        4.8"));
            list.Add(string.Format("1 UNI GY        -14.6        3.3        4.1"));
            list.Add(string.Format("1 UNI GY        -14.6        2.6        3.3"));
            list.Add(string.Format("1 UNI GY        -14.6        1.8        2.6"));
            list.Add(string.Format("LOAD 29 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        6.62        7.38"));
            list.Add(string.Format("1 UNI GY        -14.57        5.86        6.62"));
            list.Add(string.Format("1 UNI GY        -14.57        5.10        5.86"));
            list.Add(string.Format("1 UNI GY        -14.6        4.3        5.1"));
            list.Add(string.Format("1 UNI GY        -14.6        3.6        4.3"));
            list.Add(string.Format("1 UNI GY        -14.6        2.8        3.6"));
            list.Add(string.Format("1 UNI GY        -14.6        2.0        2.8"));
            list.Add(string.Format("LOAD 30 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.57        6.11        6.87"));
            list.Add(string.Format("1 UNI GY        -14.57        5.35        6.11"));
            list.Add(string.Format("1 UNI GY        -14.6        4.6        5.3"));
            list.Add(string.Format("1 UNI GY        -14.6        3.8        4.6"));
            list.Add(string.Format("1 UNI GY        -14.6        3.1        3.8"));
            list.Add(string.Format("1 UNI GY        -14.6        2.3        3.1"));
            list.Add(string.Format("LOAD 31 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.57        6.36        7.12"));
            list.Add(string.Format("1 UNI GY        -14.57        5.60        6.36"));
            list.Add(string.Format("1 UNI GY        -14.6        4.8        5.6"));
            list.Add(string.Format("1 UNI GY        -14.6        4.1        4.8"));
            list.Add(string.Format("1 UNI GY        -14.6        3.3        4.1"));
            list.Add(string.Format("1 UNI GY        -14.6        2.5        3.3"));
            list.Add(string.Format("LOAD 32 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.57        6.61        7.37"));
            list.Add(string.Format("1 UNI GY        -14.57        5.85        6.61"));
            list.Add(string.Format("1 UNI GY        -14.6        5.1        5.8"));
            list.Add(string.Format("1 UNI GY        -14.6        4.3        5.1"));
            list.Add(string.Format("1 UNI GY        -14.6        3.6        4.3"));
            list.Add(string.Format("1 UNI GY        -14.6        2.8        3.6"));
            list.Add(string.Format("LOAD 33 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.57        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.57        6.10        6.86"));
            list.Add(string.Format("1 UNI GY        -14.6        5.3        6.1"));
            list.Add(string.Format("1 UNI GY        -14.6        4.6        5.3"));
            list.Add(string.Format("1 UNI GY        -14.6        3.8        4.6"));
            list.Add(string.Format("1 UNI GY        -14.6        3.0        3.8"));
            list.Add(string.Format("LOAD 34 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.57        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.57        6.35        7.11"));
            list.Add(string.Format("1 UNI GY        -14.6        5.6        6.3"));
            list.Add(string.Format("1 UNI GY        -14.6        4.8        5.6"));
            list.Add(string.Format("1 UNI GY        -14.6        4.1        4.8"));
            list.Add(string.Format("1 UNI GY        -14.6        3.3        4.1"));
            list.Add(string.Format("LOAD 35 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.57        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.57        6.60        7.36"));
            list.Add(string.Format("1 UNI GY        -14.6        5.8        6.6"));
            list.Add(string.Format("1 UNI GY        -14.6        5.1        5.8"));
            list.Add(string.Format("1 UNI GY        -14.6        4.3        5.1"));
            list.Add(string.Format("1 UNI GY        -14.6        3.5        4.3"));
            list.Add(string.Format("LOAD 36 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.57        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.57        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.6        6.1        6.8"));
            list.Add(string.Format("1 UNI GY        -14.6        5.3        6.1"));
            list.Add(string.Format("1 UNI GY        -14.6        4.6        5.3"));
            list.Add(string.Format("1 UNI GY        -14.6        3.8        4.6"));
            list.Add(string.Format("LOAD 37 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        8.62        9.38"));
            list.Add(string.Format("1 UNI GY        -14.57        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.57        6.99        7.76"));
            list.Add(string.Format("1 UNI GY        -14.6        6.3        7.1"));
            list.Add(string.Format("1 UNI GY        -14.6        5.6        6.3"));
            list.Add(string.Format("1 UNI GY        -14.6        4.8        5.6"));
            list.Add(string.Format("1 UNI GY        -14.6        4.0        4.8"));
            list.Add(string.Format("LOAD 38 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        8.87        9.63"));
            list.Add(string.Format("1 UNI GY        -14.57        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.57        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.6        6.6        7.3"));
            list.Add(string.Format("1 UNI GY        -14.6        5.8        6.6"));
            list.Add(string.Format("1 UNI GY        -14.6        5.1        5.8"));
            list.Add(string.Format("1 UNI GY        -14.6        4.3        5.1"));
            list.Add(string.Format("LOAD 39 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        9.12        9.88"));
            list.Add(string.Format("1 UNI GY        -14.57        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.57        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.6        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.6        6.1        6.8"));
            list.Add(string.Format("1 UNI GY        -14.6        5.3        6.1"));
            list.Add(string.Format("1 UNI GY        -14.6        4.5        5.3"));
            list.Add(string.Format("LOAD 40 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        9.37        10.13"));
            list.Add(string.Format("1 UNI GY        -14.57        8.62        9.38"));
            list.Add(string.Format("1 UNI GY        -14.57        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.6        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.6        6.3        7.1"));
            list.Add(string.Format("1 UNI GY        -14.6        5.6        6.3"));
            list.Add(string.Format("1 UNI GY        -14.6        4.8        5.6"));
            list.Add(string.Format("LOAD 41 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        9.62        10.38"));
            list.Add(string.Format("1 UNI GY        -14.57        8.86        9.62"));
            list.Add(string.Format("1 UNI GY        -14.57        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.6        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.6        6.6        7.3"));
            list.Add(string.Format("1 UNI GY        -14.6        5.8        6.6"));
            list.Add(string.Format("1 UNI GY        -14.6        5.0        5.8"));
            list.Add(string.Format("LOAD 42 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        9.87        10.63"));
            list.Add(string.Format("1 UNI GY        -14.57        9.11        9.87"));
            list.Add(string.Format("1 UNI GY        -14.57        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.6        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.6        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.6        6.1        6.8"));
            list.Add(string.Format("1 UNI GY        -14.6        5.3        6.1"));
            list.Add(string.Format("LOAD 43 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        10.12        10.88"));
            list.Add(string.Format("1 UNI GY        -14.57        9.36        10.12"));
            list.Add(string.Format("1 UNI GY        -14.57        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.6        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.6        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.6        6.3        7.1"));
            list.Add(string.Format("1 UNI GY        -14.6        5.5        6.3"));
            list.Add(string.Format("LOAD 44 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        10.37        11.13"));
            list.Add(string.Format("1 UNI GY        -14.57        9.61        10.37"));
            list.Add(string.Format("1 UNI GY        -14.57        8.85        9.61"));
            list.Add(string.Format("1 UNI GY        -14.6        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.6        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.6        6.6        7.3"));
            list.Add(string.Format("1 UNI GY        -14.6        5.8        6.6"));
            list.Add(string.Format("LOAD 45 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -14.57        10.62        11.38"));
            list.Add(string.Format("1 UNI GY        -14.57        9.86        10.62"));
            list.Add(string.Format("1 UNI GY        -14.57        9.10        9.86"));
            list.Add(string.Format("1 UNI GY        -14.6        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.6        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.6        6.87        7.63"));
            list.Add(string.Format("1 UNI GY        -14.6        6.0        6.8"));
            list.Add(string.Format("LOAD 46 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -15.34        10.87        11.63"));
            list.Add(string.Format("1 UNI GY        -14.57        10.11        10.87"));
            list.Add(string.Format("1 UNI GY        -14.57        9.35        10.11"));
            list.Add(string.Format("1 UNI GY        -14.6        8.4        9.4"));
            list.Add(string.Format("1 UNI GY        -14.6        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.6        7.12        7.88"));
            list.Add(string.Format("1 UNI GY        -14.6        6.3        7.1"));
            list.Add(string.Format("LOAD 47 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -16.54        11.12        11.88"));
            list.Add(string.Format("1 UNI GY        -14.57        10.36        11.12"));
            list.Add(string.Format("1 UNI GY        -14.57        9.60        10.36"));
            list.Add(string.Format("1 UNI GY        -14.57        8.84        9.60"));
            list.Add(string.Format("1 UNI GY        -14.6        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.6        7.37        8.13"));
            list.Add(string.Format("1 UNI GY        -14.6        6.5        7.3"));
            list.Add(string.Format("LOAD 48 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -18.02        11.37        12.13"));
            list.Add(string.Format("1 UNI GY        -14.57        10.61        11.37"));
            list.Add(string.Format("1 UNI GY        -14.57        9.85        10.61"));
            list.Add(string.Format("1 UNI GY        -14.57        9.09        9.85"));
            list.Add(string.Format("1 UNI GY        -14.6        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.6        7.62        8.38"));
            list.Add(string.Format("1 UNI GY        -14.57        6.87        7.63"));
            list.Add(string.Format("LOAD 49 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -19.88        11.62        12.38"));
            list.Add(string.Format("1 UNI GY        -15.29        10.86        11.62"));
            list.Add(string.Format("1 UNI GY        -14.57        10.10        10.86"));
            list.Add(string.Format("1 UNI GY        -14.57        9.34        10.10"));
            list.Add(string.Format("1 UNI GY        -14.6        8.62        9"));
            list.Add(string.Format("1 UNI GY        -14.6        7.87        8.63"));
            list.Add(string.Format("1 UNI GY        -14.57        7.12        7.88"));
            list.Add(string.Format("LOAD 50 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -22.32        11.87        12.63"));
            list.Add(string.Format("1 UNI GY        -16.48        11.11        11.87"));
            list.Add(string.Format("1 UNI GY        -14.57        10.35        11.11"));
            list.Add(string.Format("1 UNI GY        -14.57        9.59        10.35"));
            list.Add(string.Format("1 UNI GY        -14.57        8.82        9.59"));
            list.Add(string.Format("1 UNI GY        -14.6        8.12        8.88"));
            list.Add(string.Format("1 UNI GY        -14.57        7.37        8.13"));
            list.Add(string.Format("LOAD 51 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY        -29.91        12.12        12.88"));
            list.Add(string.Format("1 UNI GY        -17.95        11.36        12.12"));
            list.Add(string.Format("1 UNI GY        -14.57        10.60        11.36"));
            list.Add(string.Format("1 UNI GY        -14.57        9.84        10.60"));
            list.Add(string.Format("1 UNI GY        -14.57        9.07        9.84"));
            list.Add(string.Format("1 UNI GY        -14.6        8.37        9.13"));
            list.Add(string.Format("1 UNI GY        -14.57        7.62        8.38"));
            list.Add(string.Format("LOAD 52 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -46.37        -0.38        0.38"));
            list.Add(string.Format("1 UNI GY        -19.80        11.61        12.37"));
            list.Add(string.Format("1 UNI GY        -15.24        10.85        11.61"));
            list.Add(string.Format("1 UNI GY        -14.57        10.09        10.85"));
            list.Add(string.Format("1 UNI GY        -14.57        9.32        10.09"));
            list.Add(string.Format("1 UNI GY        -14.6        8.62        9.38"));
            list.Add(string.Format("1 UNI GY        -14.57        7.87        8.63"));
            list.Add(string.Format("LOAD 53 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -29.91        -0.13        0.63"));
            list.Add(string.Format("1 UNI GY        -22.19        11.86        12.62"));
            list.Add(string.Format("1 UNI GY        -16.42        11.10        11.86"));
            list.Add(string.Format("1 UNI GY        -14.57        10.34        11.10"));
            list.Add(string.Format("1 UNI GY        -14.57        9.57        10.34"));
            list.Add(string.Format("1 UNI GY        -14.57        8.56        9.32"));
            list.Add(string.Format("1 UNI GY        -14.57        8.12        8.88"));
            list.Add(string.Format("LOAD 54 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -22.32        0.12        0.88"));
            list.Add(string.Format("1 UNI GY        -29.50        12.11        12.87"));
            list.Add(string.Format("1 UNI GY        -17.87        11.35        12.11"));
            list.Add(string.Format("1 UNI GY        -14.57        10.59        11.35"));
            list.Add(string.Format("1 UNI GY        -14.57        9.82        10.59"));
            list.Add(string.Format("1 UNI GY        -14.57        8.81        9.57"));
            list.Add(string.Format("1 UNI GY        -14.57        8.37        9.13"));
            list.Add(string.Format("LOAD 55 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -19.88        0.37        1.13"));
            list.Add(string.Format("1 UNI GY        -45.35        12.36        13.12"));
            list.Add(string.Format("1 UNI GY        -19.70        11.60        12.36"));
            list.Add(string.Format("1 UNI GY        -15.19        10.84        11.60"));
            list.Add(string.Format("1 UNI GY        -14.57        10.07        10.84"));
            list.Add(string.Format("1 UNI GY        -14.57        9.06        9.82"));
            list.Add(string.Format("1 UNI GY        -14.57        8.6        9.0"));
            list.Add(string.Format("LOAD 56 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -18.02        0.62        1.38"));
            list.Add(string.Format("2 UNI GY        -30.34        -0.14        0.62"));
            list.Add(string.Format("1 UNI GY        -22.06        11.85        12.61"));
            list.Add(string.Format("1 UNI GY        -16.36        11.09        11.85"));
            list.Add(string.Format("1 UNI GY        -14.57        10.32        11.09"));
            list.Add(string.Format("1 UNI GY        -14.57        9.31        10.07"));
            list.Add(string.Format("1 UNI GY        -14.57        8.80        9.56"));
            list.Add(string.Format("LOAD 57 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -16.54        0.87        1.63"));
            list.Add(string.Format("2 UNI GY        -22.54        0.11        0.87"));
            list.Add(string.Format("1 UNI GY        -29.03        12.10        12.86"));
            list.Add(string.Format("1 UNI GY        -17.80        11.34        12.10"));
            list.Add(string.Format("1 UNI GY        -14.57        10.57        11.34"));
            list.Add(string.Format("1 UNI GY        -14.57        9.56        10.32"));
            list.Add(string.Format("1 UNI GY        -14.57        9.05        9.81"));
            list.Add(string.Format("LOAD 58 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -15.34        1.12        1.88"));
            list.Add(string.Format("2 UNI GY        -19.97        0.36        1.12"));
            list.Add(string.Format("1 UNI GY        -44.19        12.35        13.11"));
            list.Add(string.Format("1 UNI GY        -19.60        11.59        12.35"));
            list.Add(string.Format("1 UNI GY        -15.14        10.82        11.59"));
            list.Add(string.Format("1 UNI GY        -14.57        9.81        10.57"));
            list.Add(string.Format("1 UNI GY        -14.57        9.30        10.06"));
            list.Add(string.Format("LOAD 59 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        1.37        2.13"));
            list.Add(string.Format("2 UNI GY        -18.08        0.61        1.37"));
            list.Add(string.Format("2 UNI GY        -30.86        -0.15        0.61"));
            list.Add(string.Format("1 UNI GY        -21.93        11.84        12.60"));
            list.Add(string.Format("1 UNI GY        -16.30        11.07        11.84"));
            list.Add(string.Format("1 UNI GY        -14.57        10.06        10.82"));
            list.Add(string.Format("1 UNI GY        -14.57        9.55        10.31"));
            list.Add(string.Format("LOAD 60 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        1.62        2.38"));
            list.Add(string.Format("2 UNI GY        -16.59        0.86        1.62"));
            list.Add(string.Format("2 UNI GY        -22.82        0.10        0.86"));
            list.Add(string.Format("1 UNI GY        -28.57        12.09        12.85"));
            list.Add(string.Format("1 UNI GY        -17.72        11.32        12.09"));
            list.Add(string.Format("1 UNI GY        -14.57        10.31        11.07"));
            list.Add(string.Format("1 UNI GY        -14.57        9.80        10.56"));
            list.Add(string.Format("LOAD 61 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        1.87        2.63"));
            list.Add(string.Format("2 UNI GY        -15.38        1.11        1.87"));
            list.Add(string.Format("2 UNI GY        -20.07        0.35        1.11"));
            list.Add(string.Format("1 UNI GY        -43.09        12.34        13.10"));
            list.Add(string.Format("1 UNI GY        -19.50        11.57        12.34"));
            list.Add(string.Format("1 UNI GY        -14.57        10.56        11.32"));
            list.Add(string.Format("1 UNI GY        -14.57        10.05        10.81"));
            list.Add(string.Format("LOAD 62 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        2.12        2.88"));
            list.Add(string.Format("2 UNI GY        -14.57        1.36        2.12"));
            list.Add(string.Format("2 UNI GY        -18.16        0.60        1.36"));
            list.Add(string.Format("2 UNI GY        -31.40        -0.17        0.60"));
            list.Add(string.Format("1 UNI GY        -21.80        11.82        12.59"));
            list.Add(string.Format("1 UNI GY        -15.09        10.81        11.57"));
            list.Add(string.Format("1 UNI GY        -14.57        10.30        11.06"));
            list.Add(string.Format("LOAD 63 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        2.37        3.13"));
            list.Add(string.Format("2 UNI GY        -14.57        1.61        2.37"));
            list.Add(string.Format("2 UNI GY        -16.65        0.85        1.61"));
            list.Add(string.Format("2 UNI GY        -23.10        0.08        0.85"));
            list.Add(string.Format("1 UNI GY        -28.12        12.07        12.84"));
            list.Add(string.Format("1 UNI GY        -16.24        11.06        11.82"));
            list.Add(string.Format("1 UNI GY        -14.57        10.55        11.31"));
            list.Add(string.Format("LOAD 64 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        2.62        3.38"));
            list.Add(string.Format("2 UNI GY        -14.57        1.86        2.62"));
            list.Add(string.Format("2 UNI GY        -15.43        1.10        1.86"));
            list.Add(string.Format("2 UNI GY        -20.18        0.33        1.10"));
            list.Add(string.Format("1 UNI GY        -42.04        12.32        13.09"));
            list.Add(string.Format("1 UNI GY        -17.64        11.31        12.07"));
            list.Add(string.Format("1 UNI GY        -15.04        10.80        11.56"));
            list.Add(string.Format("LOAD 65 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        2.87        3.63"));
            list.Add(string.Format("2 UNI GY        -14.57        2.11        2.87"));
            list.Add(string.Format("2 UNI GY        -14.57        1.35        2.11"));
            list.Add(string.Format("2 UNI GY        -18.25        0.58        1.35"));
            list.Add(string.Format("2 UNI GY        -31.97        -0.18        0.59"));
            list.Add(string.Format("1 UNI GY        -19.41        11.56        12.32"));
            list.Add(string.Format("1 UNI GY        -16.18        11.05        11.81"));
            list.Add(string.Format("LOAD 66 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        3.12        3.88"));
            list.Add(string.Format("2 UNI GY        -14.57        2.36        3.12"));
            list.Add(string.Format("2 UNI GY        -14.57        1.60        2.36"));
            list.Add(string.Format("2 UNI GY        -16.72        0.83        1.60"));
            list.Add(string.Format("2 UNI GY        -23.39        0.07        0.84"));
            list.Add(string.Format("1 UNI GY        -21.68        11.81        12.57"));
            list.Add(string.Format("1 UNI GY        -17.57        11.30        12.06"));
            list.Add(string.Format("LOAD 67 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        3.37        4.13"));
            list.Add(string.Format("2 UNI GY        -14.57        2.61        3.37"));
            list.Add(string.Format("2 UNI GY        -14.57        1.85        2.61"));
            list.Add(string.Format("2 UNI GY        -15.49        1.09        1.85"));
            list.Add(string.Format("2 UNI GY        -20.28        0.32        1.09"));
            list.Add(string.Format("1 UNI GY        -27.69        12.06        12.82"));
            list.Add(string.Format("1 UNI GY        -19.31        11.55        12.31"));
            list.Add(string.Format("LOAD 68 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        3.62        4.38"));
            list.Add(string.Format("2 UNI GY        -14.57        2.86        3.62"));
            list.Add(string.Format("2 UNI GY        -14.57        2.10        2.86"));
            list.Add(string.Format("2 UNI GY        -14.57        1.34        2.10"));
            list.Add(string.Format("2 UNI GY        -18.33        0.57        1.34"));
            list.Add(string.Format("1 UNI GY        -41.05        12.31        13.07"));
            list.Add(string.Format("1 UNI GY        -21.56        11.80        12.56"));
            list.Add(string.Format("LOAD 69 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        3.87        4.63"));
            list.Add(string.Format("2 UNI GY        -14.57        3.11        3.87"));
            list.Add(string.Format("2 UNI GY        -14.57        2.35        3.11"));
            list.Add(string.Format("2 UNI GY        -14.57        1.59        2.35"));
            list.Add(string.Format("2 UNI GY        -16.78        0.82        1.59"));
            list.Add(string.Format("2 UNI GY        -32.55        -0.19        0.57"));
            list.Add(string.Format("1 UNI GY        -27.28        12.05        12.81"));
            list.Add(string.Format("LOAD 70 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        4.12        4.88"));
            list.Add(string.Format("2 UNI GY        -14.57        3.36        4.12"));
            list.Add(string.Format("2 UNI GY        -14.57        2.60        3.36"));
            list.Add(string.Format("2 UNI GY        -14.57        1.84        2.60"));
            list.Add(string.Format("2 UNI GY        -15.54        1.07        1.84"));
            list.Add(string.Format("2 UNI GY        -23.69        0.06        0.82"));
            list.Add(string.Format("1 UNI GY        -40.10        12.30        13.06"));
            list.Add(string.Format("LOAD 71 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        4.37        5.13"));
            list.Add(string.Format("2 UNI GY        -14.57        3.61        4.37"));
            list.Add(string.Format("2 UNI GY        -14.57        2.85        3.61"));
            list.Add(string.Format("2 UNI GY        -14.57        2.09        2.85"));
            list.Add(string.Format("2 UNI GY        -14.57        1.32        2.09"));
            list.Add(string.Format("2 UNI GY        -20.39        0.31        1.07"));
            list.Add(string.Format("2 UNI GY        -33.16        -0.20        0.56"));
            list.Add(string.Format("LOAD 72 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        4.62        5.38"));
            list.Add(string.Format("2 UNI GY        -14.57        3.86        4.62"));
            list.Add(string.Format("2 UNI GY        -14.57        3.10        3.86"));
            list.Add(string.Format("2 UNI GY        -14.57        2.34        3.10"));
            list.Add(string.Format("2 UNI GY        -14.57        1.57        2.34"));
            list.Add(string.Format("2 UNI GY        -18.41        0.56        1.32"));
            list.Add(string.Format("2 UNI GY        -24.00        0.05        0.81"));
            list.Add(string.Format("LOAD 73 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        4.87        5.63"));
            list.Add(string.Format("2 UNI GY        -14.57        4.11        4.87"));
            list.Add(string.Format("2 UNI GY        -14.57        3.35        4.11"));
            list.Add(string.Format("2 UNI GY        -14.57        2.59        3.35"));
            list.Add(string.Format("2 UNI GY        -14.57        1.82        2.59"));
            list.Add(string.Format("2 UNI GY        -16.85        0.81        1.57"));
            list.Add(string.Format("2 UNI GY        -20.50        0.30        1.06"));
            list.Add(string.Format("LOAD 74 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        5.12        5.88"));
            list.Add(string.Format("2 UNI GY        -14.57        4.36        5.12"));
            list.Add(string.Format("2 UNI GY        -14.57        3.60        4.36"));
            list.Add(string.Format("2 UNI GY        -14.57        2.84        3.60"));
            list.Add(string.Format("2 UNI GY        -14.57        2.07        2.84"));
            list.Add(string.Format("2 UNI GY        -15.59        1.06        1.82"));
            list.Add(string.Format("2 UNI GY        -18.49        0.55        1.31"));
            list.Add(string.Format("LOAD 75 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        5.37        6.13"));
            list.Add(string.Format("2 UNI GY        -14.57        4.61        5.37"));
            list.Add(string.Format("2 UNI GY        -14.57        3.85        4.61"));
            list.Add(string.Format("2 UNI GY        -14.57        3.09        3.85"));
            list.Add(string.Format("2 UNI GY        -14.57        2.32        3.09"));
            list.Add(string.Format("2 UNI GY        -14.57        1.56        2.32"));
            list.Add(string.Format("2 UNI GY        -16.92        0.80        1.56"));
            list.Add(string.Format("LOAD 76 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        5.62        6.38"));
            list.Add(string.Format("2 UNI GY        -14.57        4.86        5.62"));
            list.Add(string.Format("2 UNI GY        -14.57        4.10        4.86"));
            list.Add(string.Format("2 UNI GY        -14.57        3.34        4.10"));
            list.Add(string.Format("2 UNI GY        -14.57        2.57        3.34"));
            list.Add(string.Format("2 UNI GY        -14.57        1.31        2.07"));
            list.Add(string.Format("2 UNI GY        -15.65        1.05        1.81"));
            list.Add(string.Format("LOAD 77 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        5.87        6.63"));
            list.Add(string.Format("2 UNI GY        -14.57        5.11        5.87"));
            list.Add(string.Format("2 UNI GY        -14.57        4.35        5.11"));
            list.Add(string.Format("2 UNI GY        -14.57        3.59        4.35"));
            list.Add(string.Format("2 UNI GY        -14.57        2.82        3.59"));
            list.Add(string.Format("2 UNI GY        -14.57        1.81        2.57"));
            list.Add(string.Format("2 UNI GY        -14.61        1.30        2.06"));
            list.Add(string.Format("LOAD 78 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        6.12        6.88"));
            list.Add(string.Format("2 UNI GY        -14.57        5.36        6.12"));
            list.Add(string.Format("2 UNI GY        -14.57        4.60        5.36"));
            list.Add(string.Format("2 UNI GY        -14.57        3.84        4.60"));
            list.Add(string.Format("2 UNI GY        -14.57        3.07        3.84"));
            list.Add(string.Format("2 UNI GY        -14.57        2.06        2.82"));
            list.Add(string.Format("2 UNI GY        -14.57        1.55        2.31"));
            list.Add(string.Format("LOAD 79 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        6.37        7.13"));
            list.Add(string.Format("2 UNI GY        -14.57        5.61        6.37"));
            list.Add(string.Format("2 UNI GY        -14.57        4.85        5.61"));
            list.Add(string.Format("2 UNI GY        -14.57        4.09        4.85"));
            list.Add(string.Format("2 UNI GY        -14.57        3.32        4.09"));
            list.Add(string.Format("2 UNI GY        -14.57        2.31        3.07"));
            list.Add(string.Format("2 UNI GY        -14.57        1.80        2.56"));
            list.Add(string.Format("LOAD 80 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        6.62        7.38"));
            list.Add(string.Format("2 UNI GY        -14.57        5.86        6.62"));
            list.Add(string.Format("2 UNI GY        -14.57        5.10        5.86"));
            list.Add(string.Format("2 UNI GY        -14.57        4.34        5.10"));
            list.Add(string.Format("2 UNI GY        -14.57        3.57        4.34"));
            list.Add(string.Format("2 UNI GY        -14.57        2.56        3.32"));
            list.Add(string.Format("2 UNI GY        -14.57        2.05        2.81"));
            list.Add(string.Format("LOAD 81 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        6.87        7.63"));
            list.Add(string.Format("2 UNI GY        -14.57        6.11        6.87"));
            list.Add(string.Format("2 UNI GY        -14.57        5.35        6.11"));
            list.Add(string.Format("2 UNI GY        -14.57        4.59        5.35"));
            list.Add(string.Format("2 UNI GY        -14.57        3.82        4.59"));
            list.Add(string.Format("2 UNI GY        -14.57        2.81        3.57"));
            list.Add(string.Format("2 UNI GY        -14.57        2.30        3.06"));
            list.Add(string.Format("LOAD 82 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        7.12        7.88"));
            list.Add(string.Format("2 UNI GY        -14.57        6.36        7.12"));
            list.Add(string.Format("2 UNI GY        -14.57        5.60        6.36"));
            list.Add(string.Format("2 UNI GY        -14.57        4.84        5.60"));
            list.Add(string.Format("2 UNI GY        -14.57        4.07        4.84"));
            list.Add(string.Format("2 UNI GY        -14.57        3.06        3.82"));
            list.Add(string.Format("2 UNI GY        -14.57        2.55        3.31"));
            list.Add(string.Format("LOAD 83 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        7.37        8.13"));
            list.Add(string.Format("2 UNI GY        -14.57        6.61        7.37"));
            list.Add(string.Format("2 UNI GY        -14.57        5.85        6.61"));
            list.Add(string.Format("2 UNI GY        -14.57        5.09        5.85"));
            list.Add(string.Format("2 UNI GY        -14.57        4.32        5.09"));
            list.Add(string.Format("2 UNI GY        -14.57        3.31        4.07"));
            list.Add(string.Format("2 UNI GY        -14.57        2.80        3.56"));
            list.Add(string.Format("LOAD 84 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        7.62        8.38"));
            list.Add(string.Format("2 UNI GY        -14.57        6.86        7.62"));
            list.Add(string.Format("2 UNI GY        -14.57        6.10        6.86"));
            list.Add(string.Format("2 UNI GY        -14.57        5.34        6.10"));
            list.Add(string.Format("2 UNI GY        -14.57        4.57        5.34"));
            list.Add(string.Format("2 UNI GY        -14.57        3.56        4.32"));
            list.Add(string.Format("2 UNI GY        -14.57        3.05        3.81"));
            list.Add(string.Format("LOAD 85 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        7.87        8.63"));
            list.Add(string.Format("2 UNI GY        -14.57        7.11        7.87"));
            list.Add(string.Format("2 UNI GY        -14.57        6.35        7.11"));
            list.Add(string.Format("2 UNI GY        -14.57        5.59        6.35"));
            list.Add(string.Format("2 UNI GY        -14.57        4.82        5.59"));
            list.Add(string.Format("2 UNI GY        -14.57        3.81        4.57"));
            list.Add(string.Format("2 UNI GY        -14.57        3.30        4.06"));
            list.Add(string.Format("LOAD 86 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        8.12        8.88"));
            list.Add(string.Format("2 UNI GY        -14.57        7.36        8.12"));
            list.Add(string.Format("2 UNI GY        -14.57        6.60        7.36"));
            list.Add(string.Format("2 UNI GY        -14.57        5.84        6.60"));
            list.Add(string.Format("2 UNI GY        -14.57        5.07        5.84"));
            list.Add(string.Format("2 UNI GY        -14.57        4.06        4.82"));
            list.Add(string.Format("2 UNI GY        -14.57        3.55        4.31"));
            list.Add(string.Format("LOAD 87 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        8.37        9.13"));
            list.Add(string.Format("2 UNI GY        -14.57        7.61        8.37"));
            list.Add(string.Format("2 UNI GY        -14.57        6.85        7.61"));
            list.Add(string.Format("2 UNI GY        -14.57        5.32        6.09"));
            list.Add(string.Format("2 UNI GY        -14.57        4.31        5.07"));
            list.Add(string.Format("2 UNI GY        -14.57        3.80        4.56"));
            list.Add(string.Format("LOAD 88 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        8.62        9.38"));
            list.Add(string.Format("2 UNI GY        -14.57        7.86        8.62"));
            list.Add(string.Format("2 UNI GY        -14.57        7.10        7.86"));
            list.Add(string.Format("2 UNI GY        -14.57        6.09        6.85"));
            list.Add(string.Format("2 UNI GY        -14.57        5.57        6.34"));
            list.Add(string.Format("2 UNI GY        -14.57        4.56        5.32"));
            list.Add(string.Format("2 UNI GY    -14.57        4.05        4.81"));
            list.Add(string.Format("LOAD 89 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        8.87        9.63"));
            list.Add(string.Format("2 UNI GY        -14.57        8.11        8.87"));
            list.Add(string.Format("2 UNI GY        -14.57        7.35        8.11"));
            list.Add(string.Format("2 UNI GY        -14.57        6.34        7.10"));
            list.Add(string.Format("2 UNI GY        -14.57        5.82        6.59"));
            list.Add(string.Format("2 UNI GY        -14.57        4.81        5.57"));
            list.Add(string.Format("2 UNI GY        -14.57        4.30        5.06"));
            list.Add(string.Format("LOAD 90 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        9.12        9.88"));
            list.Add(string.Format("2 UNI GY        -14.57        8.36        9.12"));
            list.Add(string.Format("2 UNI GY        -14.57        7.60        8.36"));
            list.Add(string.Format("2 UNI GY        -14.57        6.59        7.35"));
            list.Add(string.Format("2 UNI GY        -14.57        6.07        6.84"));
            list.Add(string.Format("2 UNI GY        -14.57        5.06        5.82"));
            list.Add(string.Format("2 UNI GY        -14.57        4.55        5.31"));
            list.Add(string.Format("LOAD 91 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        9.37        10.13"));
            list.Add(string.Format("2 UNI GY        -14.57        8.61        9.37"));
            list.Add(string.Format("2 UNI GY        -14.57        7.85        8.61"));
            list.Add(string.Format("2 UNI GY        -14.57        6.84        7.60"));
            list.Add(string.Format("2 UNI GY        -14.57        6.32        7.09"));
            list.Add(string.Format("2 UNI GY        -14.57        5.31        6.07"));
            list.Add(string.Format("2 UNI GY        -14.57        4.80        5.56"));
            list.Add(string.Format("LOAD 92 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        9.62        10.38"));
            list.Add(string.Format("2 UNI GY        -14.57        8.86        9.62"));
            list.Add(string.Format("2 UNI GY        -14.57        8.10        8.86"));
            list.Add(string.Format("2 UNI GY        -14.57        7.09        7.85"));
            list.Add(string.Format("2 UNI GY        -14.57        6.57        7.34"));
            list.Add(string.Format("2 UNI GY        -14.57        5.56        6.32"));
            list.Add(string.Format("2 UNI GY        -14.57        5.05        5.81"));
            list.Add(string.Format("LOAD 93 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        9.87        10.63"));
            list.Add(string.Format("2 UNI GY        -14.57        9.11        9.87"));
            list.Add(string.Format("2 UNI GY        -14.57        8.35        9.11"));
            list.Add(string.Format("2 UNI GY        -14.57        7.34        8.10"));
            list.Add(string.Format("2 UNI GY        -14.57        6.82        7.59"));
            list.Add(string.Format("2 UNI GY        -14.57        5.81        6.57"));
            list.Add(string.Format("2 UNI GY        -14.57        5.30        6.06"));
            list.Add(string.Format("LOAD 94 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        10.12        10.88"));
            list.Add(string.Format("2 UNI GY        -14.57        9.36        10.12"));
            list.Add(string.Format("2 UNI GY        -14.57        8.60        9.36"));
            list.Add(string.Format("2 UNI GY        -14.57        7.59        8.35"));
            list.Add(string.Format("2 UNI GY        -14.57        7.07        7.84"));
            list.Add(string.Format("2 UNI GY        -14.57        6.06        6.82"));
            list.Add(string.Format("2 UNI GY        -14.57        5.55        6.31"));
            list.Add(string.Format("LOAD 95 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        10.37        11.13"));
            list.Add(string.Format("2 UNI GY        -14.57        9.61        10.37"));
            list.Add(string.Format("2 UNI GY        -14.57        8.85        9.61"));
            list.Add(string.Format("2 UNI GY        -14.57        7.84        8.60"));
            list.Add(string.Format("2 UNI GY        -14.57        7.32        8.09"));
            list.Add(string.Format("2 UNI GY        -14.57        6.31        7.07"));
            list.Add(string.Format("2 UNI GY        -14.57        5.80        6.56"));
            list.Add(string.Format("LOAD 96 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -14.57        10.62        11.38"));
            list.Add(string.Format("2 UNI GY        -14.57        9.86        10.62"));
            list.Add(string.Format("2 UNI GY        -14.57        9.10        9.86"));
            list.Add(string.Format("2 UNI GY        -14.57        8.09        8.85"));
            list.Add(string.Format("2 UNI GY        -14.57        7.57        8.34"));
            list.Add(string.Format("2 UNI GY        -14.57        6.56        7.32"));
            list.Add(string.Format("2 UNI GY        -14.57        6.05        6.81"));
            list.Add(string.Format("LOAD 97 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -15.34        10.87        11.63"));
            list.Add(string.Format("2 UNI GY        -14.57        10.11        10.87"));
            list.Add(string.Format("2 UNI GY        -14.57        9.35        10.11"));
            list.Add(string.Format("2 UNI GY        -14.57        8.34        9.10"));
            list.Add(string.Format("2 UNI GY        -14.57        7.82        8.59"));
            list.Add(string.Format("2 UNI GY        -14.57        6.81        7.57"));
            list.Add(string.Format("2 UNI GY        -14.57        6.30        7.06"));
            list.Add(string.Format("LOAD 98 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -16.54        11.12        11.88"));
            list.Add(string.Format("2 UNI GY        -14.57        10.36        11.12"));
            list.Add(string.Format("2 UNI GY        -14.57        9.60        10.36"));
            list.Add(string.Format("2 UNI GY        -14.57        8.59        9.35"));
            list.Add(string.Format("2 UNI GY        -14.57        8.07        8.84"));
            list.Add(string.Format("2 UNI GY        -14.57        7.06        7.82"));
            list.Add(string.Format("2 UNI GY        -14.57        6.55        7.31"));
            list.Add(string.Format("LOAD 99 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -18.02        11.37        12.13"));
            list.Add(string.Format("2 UNI GY        -14.57        10.61        11.37"));
            list.Add(string.Format("2 UNI GY        -14.57        9.85        10.61"));
            list.Add(string.Format("2 UNI GY        -14.57        8.84        9.60"));
            list.Add(string.Format("2 UNI GY        -14.57        8.32        9.09"));
            list.Add(string.Format("2 UNI GY        -14.57        7.31        8.07"));
            list.Add(string.Format("2 UNI GY        -14.57        6.80        7.56"));
            list.Add(string.Format("LOAD 100 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -19.88        11.62        12.38"));
            list.Add(string.Format("2 UNI GY        -15.29        10.86        11.62"));
            list.Add(string.Format("2 UNI GY        -14.57        10.10        10.86"));
            list.Add(string.Format("2 UNI GY        -14.57        9.09        9.85"));
            list.Add(string.Format("2 UNI GY        -14.57        8.57        9.34"));
            list.Add(string.Format("2 UNI GY        -14.57        7.56        8.32"));
            list.Add(string.Format("2 UNI GY        -14.57        7.05        7.81"));
            list.Add(string.Format("LOAD 101 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -22.32        11.87        12.63"));
            list.Add(string.Format("2 UNI GY        -16.48        11.11        11.87"));
            list.Add(string.Format("2 UNI GY        -14.57        10.35        11.11"));
            list.Add(string.Format("2 UNI GY        -14.57        9.34        10.10"));
            list.Add(string.Format("2 UNI GY        -14.57        8.82        9.59"));
            list.Add(string.Format("2 UNI GY        -14.57        7.81        8.57"));
            list.Add(string.Format("2 UNI GY        -14.57        7.30        8.06"));
            list.Add(string.Format("LOAD 102 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -29.91        12.12        12.88"));
            list.Add(string.Format("2 UNI GY        -17.95        11.36        12.12"));
            list.Add(string.Format("2 UNI GY        -14.57        10.60        11.36"));
            list.Add(string.Format("2 UNI GY        -14.57        9.59        10.35"));
            list.Add(string.Format("2 UNI GY        -14.57        9.07        9.84"));
            list.Add(string.Format("2 UNI GY        -14.57        8.06        8.82"));
            list.Add(string.Format("2 UNI GY        -14.57        7.55        8.31"));
            list.Add(string.Format("LOAD 103 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -19.80        11.61        12.37"));
            list.Add(string.Format("2 UNI GY        -15.24        10.85        11.61"));
            list.Add(string.Format("2 UNI GY        -14.57        9.84        10.60"));
            list.Add(string.Format("2 UNI GY        -14.57        9.32        10.09"));
            list.Add(string.Format("2 UNI GY        -14.57        8.31        9.07"));
            list.Add(string.Format("2 UNI GY        -14.57        7.80        8.56"));
            list.Add(string.Format("LOAD 104 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -22.19        11.86        12.62"));
            list.Add(string.Format("2 UNI GY        -16.42        11.10        11.86"));
            list.Add(string.Format("2 UNI GY        -14.57        10.09        10.85"));
            list.Add(string.Format("2 UNI GY        -14.57        9.57        10.34"));
            list.Add(string.Format("2 UNI GY        -14.57        8.56        9.32"));
            list.Add(string.Format("2 UNI GY        -14.57        8.05        8.81"));
            list.Add(string.Format("LOAD 105 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -29.50        12.11        12.87"));
            list.Add(string.Format("2 UNI GY        -17.87        11.35        12.11"));
            list.Add(string.Format("2 UNI GY        -14.57        10.34        11.10"));
            list.Add(string.Format("2 UNI GY        -14.57        9.82        10.59"));
            list.Add(string.Format("2 UNI GY        -14.57        8.81        9.57"));
            list.Add(string.Format("2 UNI GY        -14.57        8.30        9.06"));
            list.Add(string.Format("LOAD 106 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -45.35        12.36        13.12"));
            list.Add(string.Format("2 UNI GY        -19.70        11.60        12.36"));
            list.Add(string.Format("2 UNI GY        -14.57        10.59        11.35"));
            list.Add(string.Format("2 UNI GY        -14.57        10.07        10.84"));
            list.Add(string.Format("2 UNI GY        -14.57        9.06        9.82"));
            list.Add(string.Format("2 UNI GY        -14.57        8.55        9.31"));
            list.Add(string.Format("LOAD 107 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -22.06        11.85        12.61"));
            list.Add(string.Format("2 UNI GY        -15.19        10.84        11.60"));
            list.Add(string.Format("2 UNI GY        -14.57        10.32        11.09"));
            list.Add(string.Format("2 UNI GY        -14.57        9.31        10.07"));
            list.Add(string.Format("2 UNI GY        -14.57        8.80        9.56"));
            list.Add(string.Format("LOAD 108 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -29.03        12.10        12.86"));
            list.Add(string.Format("2 UNI GY        -16.36        11.09        11.85"));
            list.Add(string.Format("2 UNI GY        -14.57        10.57        11.34"));
            list.Add(string.Format("2 UNI GY        -14.57        9.56        10.32"));
            list.Add(string.Format("2 UNI GY        -14.57        9.05        9.81"));
            list.Add(string.Format("LOAD 109 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -44.19        12.35        13.11"));
            list.Add(string.Format("2 UNI GY        -17.80        11.34        12.10"));
            list.Add(string.Format("2 UNI GY        -15.14        10.82        11.59"));
            list.Add(string.Format("2 UNI GY        -14.57        9.81        10.57"));
            list.Add(string.Format("2 UNI GY        -14.57        9.30        10.06"));
            list.Add(string.Format("LOAD 110 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -19.60        11.59        12.35"));
            list.Add(string.Format("2 UNI GY        -16.30        11.07        11.84"));
            list.Add(string.Format("2 UNI GY        -14.57        10.06        10.82"));
            list.Add(string.Format("2 UNI GY        -14.57        9.55        10.31"));
            list.Add(string.Format("LOAD 111 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -21.93        11.84        12.60"));
            list.Add(string.Format("2 UNI GY        -17.72        11.32        12.09"));
            list.Add(string.Format("2 UNI GY        -14.57        10.31        11.07"));
            list.Add(string.Format("2 UNI GY        -14.57        9.80        10.56"));
            list.Add(string.Format("LOAD 112 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -28.57        12.09        12.85"));
            list.Add(string.Format("2 UNI GY        -19.50        11.57        12.34"));
            list.Add(string.Format("2 UNI GY        -14.57        10.56        11.32"));
            list.Add(string.Format("2 UNI GY        -14.57        10.05        10.81"));
            list.Add(string.Format("LOAD 113 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -43.09        12.34        13.10"));
            list.Add(string.Format("2 UNI GY        -21.80        11.82        12.59"));
            list.Add(string.Format("2 UNI GY        -15.09        10.81        11.57"));
            list.Add(string.Format("2 UNI GY        -14.57        10.30        11.06"));
            list.Add(string.Format("LOAD 114 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -28.12        12.07        12.84"));
            list.Add(string.Format("2 UNI GY        -16.24        11.06        11.82"));
            list.Add(string.Format("2 UNI GY        -14.57        10.55        11.31"));
            list.Add(string.Format("LOAD 115 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -42.04        12.70        13.09"));
            list.Add(string.Format("2 UNI GY        -17.64        11.31        12.07"));
            list.Add(string.Format("2 UNI GY        -15.04        10.80        11.56"));
            list.Add(string.Format("LOAD 116 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -19.41        11.56        12.32"));
            list.Add(string.Format("2 UNI GY        -16.18        11.05        11.81"));
            list.Add(string.Format("LOAD 117 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -21.68        11.81        12.57"));
            list.Add(string.Format("2 UNI GY        -17.57        11.30        12.06"));
            list.Add(string.Format("LOAD 118 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -27.69        12.06        12.82"));
            list.Add(string.Format("2 UNI GY        -19.31        11.55        12.31"));
            list.Add(string.Format("LOAD 119 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -41.05        12.31        13.07"));
            list.Add(string.Format("2 UNI GY        -21.56        11.80        12.56"));
            list.Add(string.Format("LOAD 120 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -27.28        12.05        12.81"));
            list.Add(string.Format("LOAD 121 LOADTYPE Live  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY        -40.10        12.30        13.06"));
            list.Add(string.Format(""));
            list.Add(string.Format("LOAD 501 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -63.1188 0 0.8"));
            list.Add(string.Format("LOAD 502 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -41.0091 0 1.05"));
            list.Add(string.Format("LOAD 503 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -31.8192 0 1.3"));
            list.Add(string.Format("LOAD 504 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -28.2864 0 1.55"));
            list.Add(string.Format("LOAD 505 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -25.5723 0.2 1.8"));
            list.Add(string.Format("LOAD 506 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -23.428 0.45 2.05"));
            list.Add(string.Format("LOAD 507 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.6966 0.7 2.3"));
            list.Add(string.Format("1 UNI GY -49.1933 0 0.93"));
            list.Add(string.Format("LOAD 508 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 0.95 2.55"));
            list.Add(string.Format("1 UNI GY -34.8555 0 1.18"));
            list.Add(string.Format("LOAD 509 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 1.2 2.8"));
            list.Add(string.Format("1 UNI GY -29.8595 0 1.43"));
            list.Add(string.Format("LOAD 510 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 1.45 3.05"));
            list.Add(string.Format("1 UNI GY -26.7915 0.08 1.68"));
            list.Add(string.Format("LOAD 511 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 1.7 3.3"));
            list.Add(string.Format("1 UNI GY -24.3977 0.33 1.93"));
            list.Add(string.Format("LOAD 512 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 1.95 3.55"));
            list.Add(string.Format("1 UNI GY -22.4836 0.58 2.18"));
            list.Add(string.Format("LOAD 513 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 2.2 3.8"));
            list.Add(string.Format("1 UNI GY -20.9235 0.83 2.43 12.5"));
            list.Add(string.Format("LOAD 514 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 2.45 4.05"));
            list.Add(string.Format("1 UNI GY -20.5977 1.08 2.68"));
            list.Add(string.Format("LOAD 515 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 2.7 4.3"));
            list.Add(string.Format("1 UNI GY -20.5977 1.33 2.93"));
            list.Add(string.Format("LOAD 516 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 2.95 4.55"));
            list.Add(string.Format("1 UNI GY -20.5977 1.58 3.18"));
            list.Add(string.Format("LOAD 517 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 3.2 4.8"));
            list.Add(string.Format("1 UNI GY -20.5977 1.83 3.43"));
            list.Add(string.Format("LOAD 518 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 3.45 5.05"));
            list.Add(string.Format("1 UNI GY -20.5977 2.08 3.68"));
            list.Add(string.Format("LOAD 519 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 3.7 5.3"));
            list.Add(string.Format("1 UNI GY -20.5977 2.33 3.93"));
            list.Add(string.Format("1 UNI GY -53.72 0 0.88"));
            list.Add(string.Format("LOAD 520 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 3.95 5.55"));
            list.Add(string.Format("1 UNI GY -20.5977 2.58 4.18"));
            list.Add(string.Format("1 UNI GY -36.98 0 1.13"));
            list.Add(string.Format("LOAD 521 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 4.2 5.8"));
            list.Add(string.Format("1 UNI GY -20.5977 2.83 4.43"));
            list.Add(string.Format("1 UNI GY -30.58 0 1.38"));
            list.Add(string.Format("LOAD 522 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 4.45 6.05"));
            list.Add(string.Format("1 UNI GY -20.5977 3.08 4.68"));
            list.Add(string.Format("1 UNI GY -27.34 0.03 1.63"));
            list.Add(string.Format("LOAD 523 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 4.7 6.3"));
            list.Add(string.Format("1 UNI GY -20.5977 3.33 4.93"));
            list.Add(string.Format("1 UNI GY -24.83 0.28 1.88"));
            list.Add(string.Format("LOAD 524 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 4.95 6.55"));
            list.Add(string.Format("1 UNI GY -20.5977 3.58 5.18"));
            list.Add(string.Format("1 UNI GY -22.83 0.53 2.13"));
            list.Add(string.Format("LOAD 525 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 5.2 6.8"));
            list.Add(string.Format("1 UNI GY -20.5977 3.83 5.43"));
            list.Add(string.Format("1 UNI GY -21.21 0.78 2.38"));
            list.Add(string.Format("1 UNI GY -43.4 0 1.01"));
            list.Add(string.Format("LOAD 526 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 5.45 7.05"));
            list.Add(string.Format("1 UNI GY -20.5977 4.08 5.68"));
            list.Add(string.Format("1 UNI GY -20.6 1.03 2.63"));
            list.Add(string.Format("1 UNI GY -32.48 0 1.26"));
            list.Add(string.Format("LOAD 527 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 5.7 7.3"));
            list.Add(string.Format("1 UNI GY -20.5977 4.33 5.93"));
            list.Add(string.Format("1 UNI GY -20.6 1.28 2.88"));
            list.Add(string.Format("1 UNI GY -28.79 0 1.51"));
            list.Add(string.Format("LOAD 528 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 5.95 7.55"));
            list.Add(string.Format("1 UNI GY -20.5977 4.58 6.18"));
            list.Add(string.Format("1 UNI GY -20.6 1.53 3.13"));
            list.Add(string.Format("1 UNI GY -25.96 0.16 1.76"));
            list.Add(string.Format("LOAD 529 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 6.2 7.8"));
            list.Add(string.Format("1 UNI GY -20.5977 4.83 6.43"));
            list.Add(string.Format("1 UNI GY -20.6 1.78 3.38"));
            list.Add(string.Format("1 UNI GY -23.74 0.41 2.01"));
            list.Add(string.Format("LOAD 530 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 6.45 8.05"));
            list.Add(string.Format("1 UNI GY -20.5977 5.08 6.68"));
            list.Add(string.Format("1 UNI GY -20.6 2.03 3.63"));
            list.Add(string.Format("1 UNI GY -21.95 0.66 2.26"));
            list.Add(string.Format("LOAD 531 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 6.7 8.3"));
            list.Add(string.Format("1 UNI GY -20.5977 5.33 6.93"));
            list.Add(string.Format("1 UNI GY -20.6 2.28 3.88"));
            list.Add(string.Format("1 UNI GY -20.6 0.91 2.51"));
            list.Add(string.Format("LOAD 532 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 6.95 8.55"));
            list.Add(string.Format("1 UNI GY -20.5977 5.58 7.18"));
            list.Add(string.Format("1 UNI GY -20.6 2.53 4.13"));
            list.Add(string.Format("1 UNI GY -20.6 1.16 2.76"));
            list.Add(string.Format("LOAD 533 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 7.2 8.8"));
            list.Add(string.Format("1 UNI GY -20.5977 5.83 7.43"));
            list.Add(string.Format("1 UNI GY -20.6 2.78 4.38"));
            list.Add(string.Format("1 UNI GY -20.6 1.41 3.01"));
            list.Add(string.Format("1 UNI GY -37.92 0 0.88"));
            list.Add(string.Format("LOAD 534 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 7.45 9.05"));
            list.Add(string.Format("1 UNI GY -20.5977 6.08 7.68"));
            list.Add(string.Format("1 UNI GY -20.6 3.03 4.63"));
            list.Add(string.Format("1 UNI GY -20.6 1.66 3.26"));
            list.Add(string.Format("LOAD 535 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 7.7 9.3"));
            list.Add(string.Format("1 UNI GY -20.6 0 0.93"));
            list.Add(string.Format("1 UNI GY -20.6 6.33 7.93"));
            list.Add(string.Format("1 UNI GY -20.6 1.91 3.51"));
            list.Add(string.Format("1 UNI GY -26.1 0 1.13"));
            list.Add(string.Format("LOAD 536 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.5977 7.95 9.55"));
            list.Add(string.Format("1 UNI GY -20.6 6.58 8.18"));
            list.Add(string.Format("1 UNI GY -20.6 3.53 5.13"));
            list.Add(string.Format("1 UNI GY -20.6 2.16 3.76"));
            list.Add(string.Format("1 UNI GY -21.59 0 1.38"));
            list.Add(string.Format("LOAD 537 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 8.2 9.8"));
            list.Add(string.Format("1 UNI GY -20.6 6.83 8.43"));
            list.Add(string.Format("1 UNI GY -20.6 3.78 5.38"));
            list.Add(string.Format("1 UNI GY -20.6 2.41 4.01"));
            list.Add(string.Format("1 UNI GY -19.3 0.03 1.63"));
            list.Add(string.Format("LOAD 538 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 8.45 10.05"));
            list.Add(string.Format("1 UNI GY -20.6 7.08 8.68"));
            list.Add(string.Format("1 UNI GY -20.6 4.03 5.63"));
            list.Add(string.Format("1 UNI GY -20.6 2.66 4.26"));
            list.Add(string.Format("1 UNI GY -17.53 0.28 1.88"));
            list.Add(string.Format("1 UNI GY -39.38 0 0.86"));
            list.Add(string.Format("LOAD 539 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 8.7 10.3"));
            list.Add(string.Format("1 UNI GY -20.6 7.33 8.93"));
            list.Add(string.Format("1 UNI GY -20.6 4.28 5.88"));
            list.Add(string.Format("1 UNI GY -20.6 2.91 4.51"));
            list.Add(string.Format("1 UNI GY -16.12 0.53 2.13"));
            list.Add(string.Format("1 UNI GY -26.75 0 1.11"));
            list.Add(string.Format("LOAD 540 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 8.95 10.55"));
            list.Add(string.Format("1 UNI GY -20.6 7.58 9.18"));
            list.Add(string.Format("1 UNI GY -20.6 4.53 6.13"));
            list.Add(string.Format("1 UNI GY -20.6 3.16 4.76"));
            list.Add(string.Format("1 UNI GY -14.97 0.78 2.38"));
            list.Add(string.Format("1 UNI GY -21.8 0 1.36"));
            list.Add(string.Format("LOAD 541 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 9.2 10.8"));
            list.Add(string.Format("1 UNI GY -20.6 7.83 9.43"));
            list.Add(string.Format("1 UNI GY -20.6 4.78 6.38"));
            list.Add(string.Format("1 UNI GY -20.6 3.41 5.01"));
            list.Add(string.Format("1 UNI GY -14.54 1.03 2.63"));
            list.Add(string.Format("1 UNI GY -19.46 0.01 1.61"));
            list.Add(string.Format("LOAD 542 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 9.45 11.05"));
            list.Add(string.Format("1 UNI GY -20.6 8.08 9.68"));
            list.Add(string.Format("1 UNI GY -20.6 5.03 6.63"));
            list.Add(string.Format("1 UNI GY -20.6 3.66 5.26"));
            list.Add(string.Format("1 UNI GY -14.54 1.28 2.88"));
            list.Add(string.Format("1 UNI GY -17.66 0.26 1.86"));
            list.Add(string.Format("LOAD 543 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 9.7 11.3"));
            list.Add(string.Format("1 UNI GY -20.6 8.33 9.93"));
            list.Add(string.Format("1 UNI GY -20.6 5.28 6.88"));
            list.Add(string.Format("1 UNI GY -20.6 3.91 5.51"));
            list.Add(string.Format("1 UNI GY -14.54 1.53 3.13"));
            list.Add(string.Format("1 UNI GY -16.22 0.51 2.11"));
            list.Add(string.Format("LOAD 544 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 9.95 11.55"));
            list.Add(string.Format("1 UNI GY -20.6 8.58 10.18"));
            list.Add(string.Format("1 UNI GY -20.6 5.53 7.13"));
            list.Add(string.Format("1 UNI GY -20.6 4.16 5.76"));
            list.Add(string.Format("1 UNI GY -14.54 1.78 3.38"));
            list.Add(string.Format("1 UNI GY -15.06 0.76 2.36"));
            list.Add(string.Format("LOAD 545 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.6 10.2 11.8"));
            list.Add(string.Format("1 UNI GY -20.6 8.83 10.43"));
            list.Add(string.Format("1 UNI GY -20.6 5.78 7.38"));
            list.Add(string.Format("1 UNI GY -20.6 4.41 6.01"));
            list.Add(string.Format("1 UNI GY -14.54 2.03 3.63"));
            list.Add(string.Format("1 UNI GY -14.54 1.01 2.61"));
            list.Add(string.Format("LOAD 546 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.7 10.45 12.05"));
            list.Add(string.Format("1 UNI GY -20.6 9.08 10.68"));
            list.Add(string.Format("1 UNI GY -20.6 6.03 7.63"));
            list.Add(string.Format("1 UNI GY -20.6 4.66 6.26"));
            list.Add(string.Format("1 UNI GY -14.54 2.28 3.88"));
            list.Add(string.Format("1 UNI GY -14.54 1.26 2.86"));
            list.Add(string.Format("LOAD 547 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -23.43 10.7 12.3"));
            list.Add(string.Format("1 UNI GY -20.6 9.33 10.93"));
            list.Add(string.Format("1 UNI GY -20.6 6.28 7.88"));
            list.Add(string.Format("1 UNI GY -20.6 4.91 6.51"));
            list.Add(string.Format("1 UNI GY -14.54 2.53 4.13"));
            list.Add(string.Format("1 UNI GY -14.54 1.51 3.11"));
            list.Add(string.Format("LOAD 548 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -25.57 10.95 12.55"));
            list.Add(string.Format("1 UNI GY -20.6 9.58 11.18"));
            list.Add(string.Format("1 UNI GY -20.6 6.53 8.13"));
            list.Add(string.Format("1 UNI GY -20.6 5.16 6.76"));
            list.Add(string.Format("1 UNI GY -14.54 2.78 4.38"));
            list.Add(string.Format("1 UNI GY -14.54 1.76 3.36"));
            list.Add(string.Format("LOAD 549 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -28.29 11.2 12.8"));
            list.Add(string.Format("1 UNI GY -20.6 9.83 11.43"));
            list.Add(string.Format("1 UNI GY -20.6 6.78 8.38"));
            list.Add(string.Format("1 UNI GY -20.6 5.41 7.01"));
            list.Add(string.Format("1 UNI GY -14.54 3.03 4.63"));
            list.Add(string.Format("1 UNI GY -14.54 2.01 3.61"));
            list.Add(string.Format("LOAD 550 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -31.82 11.45 13.05"));
            list.Add(string.Format("1 UNI GY -20.6 10.08 11.68"));
            list.Add(string.Format("1 UNI GY -20.6 7.03 8.63"));
            list.Add(string.Format("1 UNI GY -20.6 5.66 7.26"));
            list.Add(string.Format("1 UNI GY -14.54 3.28 4.88"));
            list.Add(string.Format("1 UNI GY -14.54 2.26 3.86"));
            list.Add(string.Format("LOAD 551 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -41.01 11.7 13.3"));
            list.Add(string.Format("1 UNI GY -20.98 10.33 11.93"));
            list.Add(string.Format("1 UNI GY -20.6 7.28 8.88"));
            list.Add(string.Format("1 UNI GY -20.6 5.91 7.51"));
            list.Add(string.Format("1 UNI GY -14.54 3.53 5.13"));
            list.Add(string.Format("1 UNI GY -14.54 2.51 4.11"));
            list.Add(string.Format("LOAD 552 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -63.12 11.95 13.55"));
            list.Add(string.Format("1 UNI GY -22.55 10.58 12.18"));
            list.Add(string.Format("1 UNI GY -20.6 7.53 9.13"));
            list.Add(string.Format("1 UNI GY -20.6 6.16 7.76"));
            list.Add(string.Format("1 UNI GY -14.54 3.78 5.38"));
            list.Add(string.Format("1 UNI GY -14.54 2.76 4.36"));
            list.Add(string.Format("LOAD 553 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -41.01 -0.3 1.3"));
            list.Add(string.Format("1 UNI GY -24.48 10.83 12.43"));
            list.Add(string.Format("1 UNI GY -20.6 7.78 9.38"));
            list.Add(string.Format("1 UNI GY -20.6 6.41 8.01"));
            list.Add(string.Format("1 UNI GY -14.54 4.03 5.63"));
            list.Add(string.Format("1 UNI GY -14.54 3.01 4.61"));
            list.Add(string.Format("LOAD 554 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -31.82 -0.05 1.55"));
            list.Add(string.Format("1 UNI GY -26.9 11.08 12.68"));
            list.Add(string.Format("1 UNI GY -20.6 8.03 9.63"));
            list.Add(string.Format("1 UNI GY -20.6 6.66 8.26"));
            list.Add(string.Format("1 UNI GY -14.54 4.28 5.88"));
            list.Add(string.Format("1 UNI GY -14.54 3.26 4.86"));
            list.Add(string.Format("LOAD 555 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -28.29 0.2 1.8"));
            list.Add(string.Format("1 UNI GY -30 11.33 12.93"));
            list.Add(string.Format("1 UNI GY -20.6 8.28 9.88"));
            list.Add(string.Format("1 UNI GY -20.6 6.91 8.51"));
            list.Add(string.Format("1 UNI GY -14.54 4.53 6.13"));
            list.Add(string.Format("1 UNI GY -14.54 3.51 5.11"));
            list.Add(string.Format("1 UNI GY -36.57 0 0.9"));
            list.Add(string.Format("LOAD 556 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -25.57 0.45 2.05"));
            list.Add(string.Format("1 UNI GY -35.26 11.58 13.18"));
            list.Add(string.Format("1 UNI GY -20.6 8.53 10.13"));
            list.Add(string.Format("1 UNI GY -20.6 7.16 8.76"));
            list.Add(string.Format("1 UNI GY -14.54 4.78 6.38"));
            list.Add(string.Format("1 UNI GY -14.54 3.76 5.36"));
            list.Add(string.Format("1 UNI GY -25.48 0 1.15"));
            list.Add(string.Format("LOAD 557 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -23.43 0.7 2.3"));
            list.Add(string.Format("1 UNI GY -50.03 11.83 13.43"));
            list.Add(string.Format("1 UNI GY -20.6 8.78 10.38"));
            list.Add(string.Format("1 UNI GY -20.6 7.41 9.01"));
            list.Add(string.Format("1 UNI GY -14.54 5.03 6.63"));
            list.Add(string.Format("1 UNI GY -14.54 4.01 5.61"));
            list.Add(string.Format("1 UNI GY -21.38 0 1.4"));
            list.Add(string.Format("LOAD 558 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -21.7 0.95 2.55"));
            list.Add(string.Format("1 UNI GY -49.19 12.08 13.68"));
            list.Add(string.Format("1 UNI GY -20.6 9.03 10.63"));
            list.Add(string.Format("1 UNI GY -20.6 7.66 9.26"));
            list.Add(string.Format("1 UNI GY -14.54 5.28 6.88"));
            list.Add(string.Format("1 UNI GY -14.54 4.26 5.86"));
            list.Add(string.Format("1 UNI GY -19.14 0.05 1.65"));
            list.Add(string.Format("LOAD 559 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 1.2 2.8"));
            list.Add(string.Format("2 UNI GY -34.86 -0.17 1.43"));
            list.Add(string.Format("1 UNI GY -20.6 9.28 10.88"));
            list.Add(string.Format("1 UNI GY -20.6 7.91 9.51"));
            list.Add(string.Format("1 UNI GY -14.54 5.53 7.13"));
            list.Add(string.Format("1 UNI GY -14.54 4.51 6.11"));
            list.Add(string.Format("1 UNI GY -17.4 0.3 1.9"));
            list.Add(string.Format("LOAD 560 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 1.45 3.05"));
            list.Add(string.Format("2 UNI GY -29.86 0.08 1.68"));
            list.Add(string.Format("1 UNI GY -20.6 9.53 11.13"));
            list.Add(string.Format("1 UNI GY -20.6 8.16 9.76"));
            list.Add(string.Format("1 UNI GY -14.54 5.78 7.38"));
            list.Add(string.Format("1 UNI GY -14.54 4.76 6.36"));
            list.Add(string.Format("1 UNI GY -16.02 0.55 2.15"));
            list.Add(string.Format("LOAD 561 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 1.7 3.3"));
            list.Add(string.Format("2 UNI GY -26.79 0.33 1.93"));
            list.Add(string.Format("1 UNI GY -20.6 9.78 11.38"));
            list.Add(string.Format("1 UNI GY -20.6 8.41 10.01"));
            list.Add(string.Format("1 UNI GY -14.54 6.03 7.63"));
            list.Add(string.Format("1 UNI GY -14.54 5.01 6.61"));
            list.Add(string.Format("1 UNI GY -14.89 0.8 2.4"));
            list.Add(string.Format("LOAD 562 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 1.95 3.55"));
            list.Add(string.Format("2 UNI GY -24.4 0.58 2.18"));
            list.Add(string.Format("1 UNI GY -20.6 10.03 11.63"));
            list.Add(string.Format("1 UNI GY -20.6 8.66 10.26"));
            list.Add(string.Format("1 UNI GY -14.54 6.28 7.88"));
            list.Add(string.Format("1 UNI GY -14.54 5.26 6.86"));
            list.Add(string.Format("1 UNI GY -14.54 1.05 2.65"));
            list.Add(string.Format("LOAD 563 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 2.2 3.8"));
            list.Add(string.Format("2 UNI GY -22.48 0.83 2.43"));
            list.Add(string.Format("1 UNI GY -20.7 10.28 11.88"));
            list.Add(string.Format("1 UNI GY -20.6 8.91 10.51"));
            list.Add(string.Format("1 UNI GY -14.54 6.53 8.13"));
            list.Add(string.Format("1 UNI GY -14.54 5.51 7.11"));
            list.Add(string.Format("1 UNI GY -14.54 1.3 2.9"));
            list.Add(string.Format("LOAD 564 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 2.45 4.05"));
            list.Add(string.Format("2 UNI GY -20.92 1.08 2.68"));
            list.Add(string.Format("1 UNI GY -22.21 10.53 12.13"));
            list.Add(string.Format("2 UNI GY -20.6 9.16 10.76"));
            list.Add(string.Format("1 UNI GY -14.54 6.78 8.38"));
            list.Add(string.Format("1 UNI GY -14.54 5.76 7.36"));
            list.Add(string.Format("1 UNI GY -14.54 1.55 3.15"));
            list.Add(string.Format("LOAD 565 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 2.7 4.3"));
            list.Add(string.Format("2 UNI GY -20.6 1.33 2.93"));
            list.Add(string.Format("1 UNI GY -24.06 10.78 12.38"));
            list.Add(string.Format("1 UNI GY -20.6 9.41 11.01"));
            list.Add(string.Format("1 UNI GY -14.54 7.03 8.63"));
            list.Add(string.Format("1 UNI GY -14.54 6.01 7.61"));
            list.Add(string.Format("1 UNI GY -14.54 1.8 3.4"));
            list.Add(string.Format("LOAD 566 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 2.95 4.55"));
            list.Add(string.Format("2 UNI GY -20.6 1.58 3.18"));
            list.Add(string.Format("1 UNI GY -26.37 11.03 12.63"));
            list.Add(string.Format("1 UNI GY -20.6 9.66 11.26"));
            list.Add(string.Format("1 UNI GY -14.54 7.28 8.88"));
            list.Add(string.Format("1 UNI GY -14.54 6.28 7.88"));
            list.Add(string.Format("1 UNI GY -14.54 2.05 3.65"));
            list.Add(string.Format("LOAD 567 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 3.2 4.8"));
            list.Add(string.Format("2 UNI GY -20.6 1.83 3.43"));
            list.Add(string.Format("1 UNI GY -29.31 11.28 12.88"));
            list.Add(string.Format("1 UNI GY -20.6 9.91 11.51"));
            list.Add(string.Format("1 UNI GY -14.54 7.53 9.13"));
            list.Add(string.Format("1 UNI GY -14.54 6.53 8.13"));
            list.Add(string.Format("1 UNI GY -14.54 2.3 3.9"));
            list.Add(string.Format("LOAD 568 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 3.45 5.05"));
            list.Add(string.Format("2 UNI GY -20.6 2.08 3.68"));
            list.Add(string.Format("1 UNI GY -33.34 11.53 13.13"));
            list.Add(string.Format("1 UNI GY -20.6 10.16 11.76"));
            list.Add(string.Format("1 UNI GY -14.54 7.78 9.38"));
            list.Add(string.Format("1 UNI GY -14.54 6.78 8.38"));
            list.Add(string.Format("1 UNI GY -14.54 2.55 4.15"));
            list.Add(string.Format("LOAD 569 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 3.7 5.3"));
            list.Add(string.Format("2 UNI GY -20.6 2.33 3.93"));
            list.Add(string.Format("1 UNI GY -46.11 11.78 13.38"));
            list.Add(string.Format("1 UNI GY -21.45 10.41 12.01"));
            list.Add(string.Format("1 UNI GY -14.54 8.03 9.63"));
            list.Add(string.Format("1 UNI GY -14.54 7.03 8.63"));
            list.Add(string.Format("1 UNI GY -14.54 2.8 4.4"));
            list.Add(string.Format("LOAD 570 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 3.95 5.55"));
            list.Add(string.Format("2 UNI GY -20.6 2.58 4.18"));
            list.Add(string.Format("1 UNI GY -53.72 12.03 13.63"));
            list.Add(string.Format("1 UNI GY -23.13 10.66 12.26"));
            list.Add(string.Format("1 UNI GY -14.54 8.28 9.88"));
            list.Add(string.Format("1 UNI GY -14.54 7.28 8.88"));
            list.Add(string.Format("1 UNI GY -14.54 3.05 4.65"));
            list.Add(string.Format("LOAD 571 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 4.2 5.8"));
            list.Add(string.Format("2 UNI GY -20.6 2.83 4.43"));
            list.Add(string.Format("2 UNI GY -36.98 -0.22 1.38"));
            list.Add(string.Format("1 UNI GY -25.2 10.91 12.51"));
            list.Add(string.Format("1 UNI GY -14.54 8.53 10.13"));
            list.Add(string.Format("1 UNI GY -14.54 7.53 9.13"));
            list.Add(string.Format("1 UNI GY -14.54 3.3 4.9"));
            list.Add(string.Format("LOAD 572 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 4.45 6.05"));
            list.Add(string.Format("2 UNI GY -20.6 3.08 4.68"));
            list.Add(string.Format("2 UNI GY -30.58 0.03 1.63"));
            list.Add(string.Format("1 UNI GY -27.81 11.16 12.76"));
            list.Add(string.Format("1 UNI GY -14.54 8.78 10.38"));
            list.Add(string.Format("1 UNI GY -14.54 7.78 9.38"));
            list.Add(string.Format("1 UNI GY -14.54 3.55 5.15"));
            list.Add(string.Format("LOAD 573 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 4.7 6.3"));
            list.Add(string.Format("2 UNI GY -20.6 3.33 4.93"));
            list.Add(string.Format("2 UNI GY -27.34 0.28 1.88"));
            list.Add(string.Format("1 UNI GY -31.18 11.41 13.01"));
            list.Add(string.Format("1 UNI GY -14.54 9.03 10.63"));
            list.Add(string.Format("1 UNI GY -14.54 8.03 9.63"));
            list.Add(string.Format("1 UNI GY -14.54 3.8 5.4"));
            list.Add(string.Format("LOAD 574 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 4.95 6.55"));
            list.Add(string.Format("2 UNI GY -20.6 3.58 5.18"));
            list.Add(string.Format("2 UNI GY -24.83 0.53 2.13"));
            list.Add(string.Format("1 UNI GY -38.88 11.66 13.26"));
            list.Add(string.Format("1 UNI GY -14.54 9.28 10.88"));
            list.Add(string.Format("1 UNI GY -14.54 8.26 9.86"));
            list.Add(string.Format("1 UNI GY -14.54 4.05 5.65"));
            list.Add(string.Format("LOAD 575 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 5.2 6.8"));
            list.Add(string.Format("2 UNI GY -20.6 3.83 5.43"));
            list.Add(string.Format("2 UNI GY -22.83 0.78 2.38"));
            list.Add(string.Format("1 UNI GY -58.03 11.91 13.51"));
            list.Add(string.Format("1 UNI GY -14.54 9.53 11.13"));
            list.Add(string.Format("1 UNI GY -14.54 8.51 10.11"));
            list.Add(string.Format("1 UNI GY -14.54 4.3 5.9"));
            list.Add(string.Format("LOAD 576 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 5.45 7.05"));
            list.Add(string.Format("2 UNI GY -20.6 4.08 5.68"));
            list.Add(string.Format("2 UNI GY -21.21 1.03 2.63"));
            list.Add(string.Format("1 UNI GY -43.4 12.16 13.76"));
            list.Add(string.Format("1 UNI GY -14.54 9.78 11.38"));
            list.Add(string.Format("1 UNI GY -14.54 8.76 10.36"));
            list.Add(string.Format("1 UNI GY -14.54 4.55 6.15"));
            list.Add(string.Format("LOAD 577 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 5.7 7.3"));
            list.Add(string.Format("2 UNI GY -20.6 4.33 5.93"));
            list.Add(string.Format("2 UNI GY -20.6 1.28 2.88"));
            list.Add(string.Format("2 UNI GY -32.48 -0.09 1.51"));
            list.Add(string.Format("1 UNI GY -14.54 10.03 11.63"));
            list.Add(string.Format("1 UNI GY -14.54 9.01 10.61"));
            list.Add(string.Format("1 UNI GY -14.54 4.8 6.4"));
            list.Add(string.Format("LOAD 578 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 5.95 7.55"));
            list.Add(string.Format("2 UNI GY -20.6 4.58 6.18"));
            list.Add(string.Format("2 UNI GY -20.6 1.53 3.13"));
            list.Add(string.Format("2 UNI GY -28.79 0.16 1.76"));
            list.Add(string.Format("1 UNI GY -14.61 10.28 11.88"));
            list.Add(string.Format("1 UNI GY -14.54 9.26 10.86"));
            list.Add(string.Format("1 UNI GY -14.54 5.05 6.65"));
            list.Add(string.Format("LOAD 579 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 6.2 7.8"));
            list.Add(string.Format("2 UNI GY -20.6 4.83 6.43"));
            list.Add(string.Format("2 UNI GY -20.6 1.78 3.38"));
            list.Add(string.Format("2 UNI GY -25.96 0.41 2.01"));
            list.Add(string.Format("1 UNI GY -15.68 10.53 12.13"));
            list.Add(string.Format("1 UNI GY -14.54 9.51 11.11"));
            list.Add(string.Format("1 UNI GY -14.54 5.3 6.9"));
            list.Add(string.Format("LOAD 580 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 6.45 8.05"));
            list.Add(string.Format("2 UNI GY -20.6 5.08 6.68"));
            list.Add(string.Format("2 UNI GY -20.6 2.03 3.63"));
            list.Add(string.Format("2 UNI GY -23.74 0.66 2.26"));
            list.Add(string.Format("1 UNI GY -16.99 10.78 12.38"));
            list.Add(string.Format("1 UNI GY -14.54 9.76 11.36"));
            list.Add(string.Format("1 UNI GY -14.54 5.55 7.15"));
            list.Add(string.Format("LOAD 581 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 6.7 8.3"));
            list.Add(string.Format("2 UNI GY -20.6 5.33 6.93"));
            list.Add(string.Format("2 UNI GY -20.6 2.28 3.88"));
            list.Add(string.Format("2 UNI GY -21.95 0.91 2.51"));
            list.Add(string.Format("1 UNI GY -18.61 11.03 12.63"));
            list.Add(string.Format("1 UNI GY -14.54 10.01 11.61"));
            list.Add(string.Format("1 UNI GY -14.54 5.8 7.4"));
            list.Add(string.Format("LOAD 582 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 6.95 8.55"));
            list.Add(string.Format("2 UNI GY -20.6 5.58 7.18"));
            list.Add(string.Format("2 UNI GY -20.6 2.53 4.13"));
            list.Add(string.Format("2 UNI GY -20.6 1.16 2.76"));
            list.Add(string.Format("1 UNI GY -20.69 11.28 12.88"));
            list.Add(string.Format("1 UNI GY -14.54 10.26 11.86"));
            list.Add(string.Format("1 UNI GY -14.54 6.05 7.65"));
            list.Add(string.Format("LOAD 583 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 7.2 8.8"));
            list.Add(string.Format("2 UNI GY -20.6 5.83 7.43"));
            list.Add(string.Format("2 UNI GY -20.6 2.78 4.38"));
            list.Add(string.Format("2 UNI GY -20.6 1.41 3.01"));
            list.Add(string.Format("1 UNI GY -23.53 11.53 13.13"));
            list.Add(string.Format("1 UNI GY -15.59 10.51 12.11"));
            list.Add(string.Format("LOAD 584 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 7.45 9.05"));
            list.Add(string.Format("2 UNI GY -20.6 6.08 7.68"));
            list.Add(string.Format("2 UNI GY -20.6 3.03 4.63"));
            list.Add(string.Format("2 UNI GY -20.6 1.66 3.26"));
            list.Add(string.Format("1 UNI GY -32.55 11.78 13.38"));
            list.Add(string.Format("1 UNI GY -16.87 10.76 12.36"));
            list.Add(string.Format("1 UNI GY -14.54 6.55 8.15"));
            list.Add(string.Format("LOAD 585 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 7.7 9.3"));
            list.Add(string.Format("2 UNI GY -20.6 6.33 7.93"));
            list.Add(string.Format("2 UNI GY -20.6 3.28 4.88"));
            list.Add(string.Format("2 UNI GY -20.6 1.91 3.51"));
            list.Add(string.Format("1 UNI GY -37.92 12.03 13.63"));
            list.Add(string.Format("1 UNI GY -18.47 11.01 12.61"));
            list.Add(string.Format("1 UNI GY -14.54 6.8 8.4"));
            list.Add(string.Format("LOAD 586 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 7.95 9.55"));
            list.Add(string.Format("2 UNI GY -20.6 6.58 8.18"));
            list.Add(string.Format("2 UNI GY -20.6 3.53 5.13"));
            list.Add(string.Format("2 UNI GY -20.6 2.16 3.76"));
            list.Add(string.Format("2 UNI GY -26.1 -0.22 1.38"));
            list.Add(string.Format("1 UNI GY -20.5 11.26 12.86"));
            list.Add(string.Format("LOAD 587 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 8.2 9.8"));
            list.Add(string.Format("2 UNI GY -20.6 6.83 8.43"));
            list.Add(string.Format("2 UNI GY -20.6 3.78 5.38"));
            list.Add(string.Format("2 UNI GY -20.6 2.41 4.01"));
            list.Add(string.Format("2 UNI GY -21.59 0.03 1.63"));
            list.Add(string.Format("1 UNI GY -23.17 11.51 13.11"));
            list.Add(string.Format("LOAD 588 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 8.45 10.05"));
            list.Add(string.Format("2 UNI GY -20.6 7.08 8.68"));
            list.Add(string.Format("2 UNI GY -20.6 4.03 5.63"));
            list.Add(string.Format("2 UNI GY -19.3 0.28 1.88"));
            list.Add(string.Format("1 UNI GY -31.56 11.76 13.36"));
            list.Add(string.Format("1 UNI GY -14.54 7.55 9.15"));
            list.Add(string.Format("LOAD 589 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 8.7 10.3"));
            list.Add(string.Format("2 UNI GY -20.6 7.33 8.93"));
            list.Add(string.Format("2 UNI GY -20.6 4.28 5.88"));
            list.Add(string.Format("2 UNI GY -20.6 2.91 4.51"));
            list.Add(string.Format("2 UNI GY -17.53 0.53 2.13"));
            list.Add(string.Format("1 UNI GY -39.38 12.01 13.61"));
            list.Add(string.Format("LOAD 590 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 8.95 10.55"));
            list.Add(string.Format("2 UNI GY -20.6 7.58 9.18"));
            list.Add(string.Format("2 UNI GY -20.6 4.53 6.13"));
            list.Add(string.Format("2 UNI GY -20.6 3.16 4.76"));
            list.Add(string.Format("2 UNI GY -26.75 -0.24 1.36"));
            list.Add(string.Format("1 UNI GY -14.54 8.05 9.65"));
            list.Add(string.Format("LOAD 591 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.2 10.8"));
            list.Add(string.Format("2 UNI GY -20.6 7.83 9.43"));
            list.Add(string.Format("2 UNI GY -20.6 3.41 5.01"));
            list.Add(string.Format("2 UNI GY -16.12 0.78 2.38"));
            list.Add(string.Format("2 UNI GY -21.8 0.01 1.61"));
            list.Add(string.Format("LOAD 592 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.45 11.05"));
            list.Add(string.Format("2 UNI GY -20.6 8.08 9.68"));
            list.Add(string.Format("2 UNI GY -20.6 5.03 6.63"));
            list.Add(string.Format("2 UNI GY -20.6 3.66 5.26"));
            list.Add(string.Format("2 UNI GY -14.97 1.03 2.63"));
            list.Add(string.Format("2 UNI GY -19.46 0.26 1.86"));
            list.Add(string.Format("1 UNI GY -14.54 8.3 9.9"));
            list.Add(string.Format("LOAD 593 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.7 11.3"));
            list.Add(string.Format("2 UNI GY -20.6 8.33 9.93"));
            list.Add(string.Format("2 UNI GY -20.6 5.28 6.88"));
            list.Add(string.Format("2 UNI GY -20.6 3.91 5.51"));
            list.Add(string.Format("2 UNI GY -14.54 1.28 2.88"));
            list.Add(string.Format("2 UNI GY -17.66 0.51 2.11"));
            list.Add(string.Format("1 UNI GY -14.54 8.55 10.15"));
            list.Add(string.Format("LOAD 594 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.95 11.55"));
            list.Add(string.Format("2 UNI GY -20.6 8.58 10.18"));
            list.Add(string.Format("2 UNI GY -20.6 5.53 7.13"));
            list.Add(string.Format("2 UNI GY -20.6 4.16 5.76"));
            list.Add(string.Format("2 UNI GY -14.54 1.53 3.13"));
            list.Add(string.Format("2 UNI GY -16.22 0.76 2.36"));
            list.Add(string.Format("1 UNI GY -14.54 8.8 10.4"));
            list.Add(string.Format("LOAD 595 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 10.2 11.8"));
            list.Add(string.Format("2 UNI GY -20.6 8.83 10.43"));
            list.Add(string.Format("2 UNI GY -20.6 5.78 7.38"));
            list.Add(string.Format("2 UNI GY -20.6 4.41 6.01"));
            list.Add(string.Format("2 UNI GY -14.54 1.78 3.38"));
            list.Add(string.Format("2 UNI GY -15.06 1.01 2.61"));
            list.Add(string.Format("1 UNI GY -14.54 9.05 10.65"));
            list.Add(string.Format("LOAD 596 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 10.45 12.05"));
            list.Add(string.Format("2 UNI GY -20.6 9.08 10.68"));
            list.Add(string.Format("2 UNI GY -20.6 6.03 7.63"));
            list.Add(string.Format("2 UNI GY -20.6 4.66 6.26"));
            list.Add(string.Format("2 UNI GY -14.54 2.03 3.63"));
            list.Add(string.Format("2 UNI GY -14.54 1.26 2.86"));
            list.Add(string.Format("1 UNI GY -14.54 9.3 10.9"));
            list.Add(string.Format("LOAD 597 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -21.7 10.7 12.3"));
            list.Add(string.Format("2 UNI GY -20.6 9.33 10.93"));
            list.Add(string.Format("2 UNI GY -20.6 6.28 7.88"));
            list.Add(string.Format("2 UNI GY -20.6 4.91 6.51"));
            list.Add(string.Format("2 UNI GY -14.54 2.28 3.88"));
            list.Add(string.Format("2 UNI GY -14.54 1.51 3.11"));
            list.Add(string.Format("1 UNI GY -14.54 9.55 11.15"));
            list.Add(string.Format("LOAD 598 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -23.43 10.95 12.55"));
            list.Add(string.Format("2 UNI GY -20.6 9.58 11.18"));
            list.Add(string.Format("2 UNI GY -20.6 6.53 8.13"));
            list.Add(string.Format("2 UNI GY -20.6 5.16 6.76"));
            list.Add(string.Format("2 UNI GY -14.54 2.53 4.13"));
            list.Add(string.Format("2 UNI GY -14.54 1.76 3.36"));
            list.Add(string.Format("1 UNI GY -14.54 9.8 11.4"));
            list.Add(string.Format("LOAD 599 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -25.57 11.2 12.8"));
            list.Add(string.Format("2 UNI GY -20.6 9.83 11.43"));
            list.Add(string.Format("2 UNI GY -20.6 6.78 8.38"));
            list.Add(string.Format("2 UNI GY -20.6 5.41 7.01"));
            list.Add(string.Format("2 UNI GY -14.54 2.78 4.38"));
            list.Add(string.Format("2 UNI GY -14.54 2.01 3.61"));
            list.Add(string.Format("1 UNI GY -14.54 10.05 11.65"));
            list.Add(string.Format("LOAD 600 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -28.29 11.45 13.05"));
            list.Add(string.Format("2 UNI GY -20.6 10.08 11.68"));
            list.Add(string.Format("2 UNI GY -20.6 7.03 8.63"));
            list.Add(string.Format("2 UNI GY -20.6 5.66 7.26"));
            list.Add(string.Format("2 UNI GY -14.54 3.03 4.63"));
            list.Add(string.Format("2 UNI GY -14.54 2.26 3.86"));
            list.Add(string.Format("1 UNI GY -14.69 10.3 11.9"));
            list.Add(string.Format("LOAD 601 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -31.82 11.7 13.3"));
            list.Add(string.Format("2 UNI GY -20.6 10.33 11.93"));
            list.Add(string.Format("2 UNI GY -20.6 7.28 8.88"));
            list.Add(string.Format("2 UNI GY -20.6 5.91 7.51"));
            list.Add(string.Format("2 UNI GY -14.54 3.28 4.88"));
            list.Add(string.Format("2 UNI GY -14.54 2.51 4.11"));
            list.Add(string.Format("1 UNI GY -15.77 10.55 12.15"));
            list.Add(string.Format("LOAD 602 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -41.01 11.95 13.55"));
            list.Add(string.Format("2 UNI GY -20.98 10.58 12.18"));
            list.Add(string.Format("2 UNI GY -20.6 7.53 9.13"));
            list.Add(string.Format("2 UNI GY -20.6 6.16 7.76"));
            list.Add(string.Format("2 UNI GY -14.54 3.53 5.13"));
            list.Add(string.Format("2 UNI GY -14.54 2.76 4.36"));
            list.Add(string.Format("1 UNI GY -17.1 10.8 12.4"));
            list.Add(string.Format("LOAD 603 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -63.12 12.2 13.8"));
            list.Add(string.Format("2 UNI GY -22.55 10.83 12.43"));
            list.Add(string.Format("2 UNI GY -20.6 7.78 9.38"));
            list.Add(string.Format("2 UNI GY -20.6 6.41 8.01"));
            list.Add(string.Format("2 UNI GY -14.54 3.78 5.38"));
            list.Add(string.Format("2 UNI GY -14.54 3.01 4.61"));
            list.Add(string.Format("1 UNI GY -18.76 11.05 12.65"));
            list.Add(string.Format("LOAD 604 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -24.48 11.08 12.68"));
            list.Add(string.Format("2 UNI GY -20.6 8.03 9.63"));
            list.Add(string.Format("2 UNI GY -20.6 6.66 8.26"));
            list.Add(string.Format("2 UNI GY -14.54 4.03 5.63"));
            list.Add(string.Format("2 UNI GY -14.54 3.26 4.86"));
            list.Add(string.Format("1 UNI GY -20.88 11.3 12.9"));
            list.Add(string.Format("LOAD 605 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -26.9 11.33 12.93"));
            list.Add(string.Format("2 UNI GY -20.6 8.28 9.88"));
            list.Add(string.Format("2 UNI GY -20.6 6.91 8.51"));
            list.Add(string.Format("2 UNI GY -14.54 4.28 5.88"));
            list.Add(string.Format("2 UNI GY -14.54 3.51 5.11"));
            list.Add(string.Format("1 UNI GY -24.06 11.55 13.15"));
            list.Add(string.Format("LOAD 606 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -30 11.58 13.18"));
            list.Add(string.Format("2 UNI GY -20.6 8.53 10.13"));
            list.Add(string.Format("2 UNI GY -20.6 7.16 8.76"));
            list.Add(string.Format("2 UNI GY -14.54 4.53 6.13"));
            list.Add(string.Format("2 UNI GY -14.54 3.76 5.36"));
            list.Add(string.Format("1 UNI GY -33.6 11.8 13.4"));
            list.Add(string.Format("LOAD 607 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -35.26 11.83 13.43"));
            list.Add(string.Format("2 UNI GY -20.6 8.78 10.38"));
            list.Add(string.Format("2 UNI GY -20.6 7.41 9.01"));
            list.Add(string.Format("2 UNI GY -14.54 4.78 6.38"));
            list.Add(string.Format("2 UNI GY -14.54 4.01 5.61"));
            list.Add(string.Format("1 UNI GY -36.57 12.05 13.65"));
            list.Add(string.Format("LOAD 608 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -50.03 12.08 13.68"));
            list.Add(string.Format("2 UNI GY -20.6 9.03 10.63"));
            list.Add(string.Format("2 UNI GY -20.6 7.66 9.26"));
            list.Add(string.Format("2 UNI GY -14.54 5.03 6.63"));
            list.Add(string.Format("2 UNI GY -14.54 4.26 5.86"));
            list.Add(string.Format("2 UNI GY -25.48 -0.2 1.4"));
            list.Add(string.Format("LOAD 609 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.28 10.88"));
            list.Add(string.Format("2 UNI GY -20.6 7.91 9.51"));
            list.Add(string.Format("2 UNI GY -14.54 5.28 6.88"));
            list.Add(string.Format("2 UNI GY -14.54 4.51 6.11"));
            list.Add(string.Format("2 UNI GY -21.38 0.05 1.65"));
            list.Add(string.Format("LOAD 610 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.53 11.13"));
            list.Add(string.Format("2 UNI GY -20.6 8.16 9.76"));
            list.Add(string.Format("2 UNI GY -14.54 5.53 7.13"));
            list.Add(string.Format("2 UNI GY -14.54 4.76 6.36"));
            list.Add(string.Format("2 UNI GY -19.14 0.3 1.9"));
            list.Add(string.Format("LOAD 611 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 9.78 11.38"));
            list.Add(string.Format("2 UNI GY -20.6 8.41 10.01"));
            list.Add(string.Format("2 UNI GY -14.54 5.78 7.38"));
            list.Add(string.Format("2 UNI GY -14.54 5.01 6.61"));
            list.Add(string.Format("2 UNI GY -17.4 0.55 2.15"));
            list.Add(string.Format("LOAD 612 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 10.03 11.63"));
            list.Add(string.Format("2 UNI GY -20.6 8.66 10.26"));
            list.Add(string.Format("2 UNI GY -14.54 6.03 7.63"));
            list.Add(string.Format("2 UNI GY -14.54 5.26 6.86"));
            list.Add(string.Format("2 UNI GY -16.02 0.8 2.4"));
            list.Add(string.Format("LOAD 613 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.6 10.28 11.88"));
            list.Add(string.Format("2 UNI GY -20.6 8.91 10.51"));
            list.Add(string.Format("2 UNI GY -14.54 6.28 7.88"));
            list.Add(string.Format("2 UNI GY -14.54 5.51 7.11"));
            list.Add(string.Format("2 UNI GY -14.89 1.05 2.65"));
            list.Add(string.Format("LOAD 614 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.7 10.53 12.13"));
            list.Add(string.Format("2 UNI GY -20.6 9.16 10.76"));
            list.Add(string.Format("2 UNI GY -14.54 6.53 8.13"));
            list.Add(string.Format("2 UNI GY -14.54 5.76 7.36"));
            list.Add(string.Format("2 UNI GY -14.54 1.3 2.9"));
            list.Add(string.Format("LOAD 615 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -22.21 10.78 12.38"));
            list.Add(string.Format("2 UNI GY -20.6 9.41 11.01"));
            list.Add(string.Format("2 UNI GY -14.54 6.78 8.38"));
            list.Add(string.Format("2 UNI GY -14.54 6.01 7.61"));
            list.Add(string.Format("2 UNI GY -14.54 1.55 3.15"));
            list.Add(string.Format("LOAD 616 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -24.06 11.03 12.63"));
            list.Add(string.Format("2 UNI GY -20.6 9.66 11.26"));
            list.Add(string.Format("2 UNI GY -14.54 7.03 8.63"));
            list.Add(string.Format("2 UNI GY -14.54 6.26 7.86"));
            list.Add(string.Format("2 UNI GY -14.54 1.8 3.4"));
            list.Add(string.Format("LOAD 617 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -26.37 11.28 12.88"));
            list.Add(string.Format("2 UNI GY -20.6 9.91 11.51"));
            list.Add(string.Format("2 UNI GY -14.54 7.28 8.88"));
            list.Add(string.Format("2 UNI GY -14.54 6.51 8.11"));
            list.Add(string.Format("2 UNI GY -14.54 2.05 3.65"));
            list.Add(string.Format("LOAD 618 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -29.31 11.53 13.13"));
            list.Add(string.Format("2 UNI GY -20.6 10.16 11.76"));
            list.Add(string.Format("2 UNI GY -14.54 7.53 9.13"));
            list.Add(string.Format("2 UNI GY -14.54 6.76 8.36"));
            list.Add(string.Format("2 UNI GY -14.54 2.3 3.9"));
            list.Add(string.Format("LOAD 619 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -33.34 11.78 13.38"));
            list.Add(string.Format("2 UNI GY -20.6 10.41 12.01"));
            list.Add(string.Format("2 UNI GY -14.54 7.78 9.38"));
            list.Add(string.Format("2 UNI GY -14.54 7.01 8.61"));
            list.Add(string.Format("2 UNI GY -14.54 2.55 4.15"));
            list.Add(string.Format("LOAD 620 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -46.11 12.03 13.63"));
            list.Add(string.Format("2 UNI GY -21.45 10.66 12.26"));
            list.Add(string.Format("2 UNI GY -14.54 8.03 9.63"));
            list.Add(string.Format("2 UNI GY -14.54 7.26 8.86"));
            list.Add(string.Format("2 UNI GY -14.54 2.8 4.4"));
            list.Add(string.Format("LOAD 621 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -23.13 10.91 12.51"));
            list.Add(string.Format("2 UNI GY -14.54 8.28 9.88"));
            list.Add(string.Format("2 UNI GY -14.54 7.51 9.11"));
            list.Add(string.Format("2 UNI GY -14.54 3.05 4.65"));
            list.Add(string.Format("LOAD 622 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -25.2 11.16 12.76"));
            list.Add(string.Format("2 UNI GY -14.54 8.53 10.13"));
            list.Add(string.Format("2 UNI GY -14.54 7.76 9.36"));
            list.Add(string.Format("2 UNI GY -14.54 3.3 4.9"));
            list.Add(string.Format("LOAD 623 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -27.81 11.41 13.01"));
            list.Add(string.Format("2 UNI GY -14.54 8.78 10.38"));
            list.Add(string.Format("2 UNI GY -14.54 8.01 9.61"));
            list.Add(string.Format("2 UNI GY -14.54 3.55 5.15"));
            list.Add(string.Format("LOAD 624 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -31.18 11.66 13.26"));
            list.Add(string.Format("2 UNI GY -14.54 9.03 10.63"));
            list.Add(string.Format("2 UNI GY -14.54 8.26 9.86"));
            list.Add(string.Format("2 UNI GY -14.54 3.8 5.4"));
            list.Add(string.Format("LOAD 625 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -38.88 11.91 13.51"));
            list.Add(string.Format("2 UNI GY -14.54 9.28 10.88"));
            list.Add(string.Format("2 UNI GY -14.54 8.51 10.11"));
            list.Add(string.Format("2 UNI GY -14.54 4.05 5.65"));
            list.Add(string.Format("LOAD 626 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -58.03 12.16 13.76"));
            list.Add(string.Format("2 UNI GY -14.54 9.53 11.13"));
            list.Add(string.Format("2 UNI GY -14.54 8.76 10.36"));
            list.Add(string.Format("2 UNI GY -14.54 4.3 5.9"));
            list.Add(string.Format("LOAD 627 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 9.78 11.38"));
            list.Add(string.Format("2 UNI GY -14.54 9.01 10.61"));
            list.Add(string.Format("2 UNI GY -14.54 4.55 6.15"));
            list.Add(string.Format("LOAD 628 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 10.03 11.63"));
            list.Add(string.Format("2 UNI GY -14.54 9.26 10.86"));
            list.Add(string.Format("2 UNI GY -14.54 4.8 6.4"));
            list.Add(string.Format("LOAD 629 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 10.28 11.88"));
            list.Add(string.Format("2 UNI GY -14.54 9.51 11.11"));
            list.Add(string.Format("2 UNI GY -14.54 5.05 6.65"));
            list.Add(string.Format("LOAD 630 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.61 10.53 12.13"));
            list.Add(string.Format("2 UNI GY -14.54 9.76 11.36"));
            list.Add(string.Format("2 UNI GY -14.54 5.3 6.9"));
            list.Add(string.Format("LOAD 631 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -15.68 10.78 12.38"));
            list.Add(string.Format("2 UNI GY -14.54 10.01 11.61"));
            list.Add(string.Format("2 UNI GY -14.54 5.55 7.15"));
            list.Add(string.Format("LOAD 632 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -16.99 11.03 12.63"));
            list.Add(string.Format("2 UNI GY -14.54 10.26 11.86"));
            list.Add(string.Format("2 UNI GY -14.54 5.8 7.4"));
            list.Add(string.Format("LOAD 633 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -18.61 11.28 12.88"));
            list.Add(string.Format("2 UNI GY -14.54 10.51 12.11"));
            list.Add(string.Format("2 UNI GY -14.54 6.05 7.65"));
            list.Add(string.Format("LOAD 634 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.69 11.53 13.13"));
            list.Add(string.Format("2 UNI GY -15.59 10.76 12.36"));
            list.Add(string.Format("2 UNI GY -14.54 6.3 7.9"));
            list.Add(string.Format("LOAD 635 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -23.53 11.78 13.38"));
            list.Add(string.Format("2 UNI GY -16.87 11.01 12.61"));
            list.Add(string.Format("2 UNI GY -14.54 6.55 8.15"));
            list.Add(string.Format("LOAD 636 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -32.55 12.03 13.63"));
            list.Add(string.Format("2 UNI GY -18.47 11.26 12.86"));
            list.Add(string.Format("2 UNI GY -14.54 6.8 8.4"));
            list.Add(string.Format("LOAD 637 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 5.8 7.4"));
            list.Add(string.Format("2 UNI GY -20.5 11.51 13.11"));
            list.Add(string.Format("2 UNI GY -14.54 7.05 8.65"));
            list.Add(string.Format("LOAD 638 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 6.05 7.65"));
            list.Add(string.Format("2 UNI GY -23.17 11.76 13.36"));
            list.Add(string.Format("2 UNI GY -14.54 7.3 8.9"));
            list.Add(string.Format("LOAD 639 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -31.56 12.01 13.61"));
            list.Add(string.Format("2 UNI GY -14.54 7.55 9.15"));
            list.Add(string.Format("LOAD 640 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 7.8 9.4"));
            list.Add(string.Format("LOAD 641 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 8.05 9.65"));
            list.Add(string.Format("LOAD 642 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 8.3 9.9"));
            list.Add(string.Format("LOAD 643 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 8.55 10.15"));
            list.Add(string.Format("LOAD 644 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 8.8 10.4"));
            list.Add(string.Format("LOAD 645 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 9.05 10.65"));
            list.Add(string.Format("LOAD 646 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 9.3 10.9"));
            list.Add(string.Format("LOAD 647 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 9.55 11.15"));
            list.Add(string.Format("LOAD 648 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 9.8 11.4"));
            list.Add(string.Format("LOAD 649 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 10.05 11.65"));
            list.Add(string.Format("LOAD 650 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.54 10.3 11.9"));
            list.Add(string.Format("LOAD 651 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -14.69 10.55 12.15"));
            list.Add(string.Format("LOAD 652 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -15.77 10.8 12.4"));
            list.Add(string.Format("LOAD 653 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -17.1 11.05 12.65"));
            list.Add(string.Format("LOAD 654 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -18.76 11.3 12.9"));
            list.Add(string.Format("LOAD 655 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -20.88 11.55 13.15"));
            list.Add(string.Format("LOAD 656 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -24.06 11.8 13.4"));
            list.Add(string.Format("LOAD 657 LOADTYPE Live  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -33.6 12.05 13.65"));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));
            File.WriteAllLines(Input_File_LL, list.ToArray());
        }

        public void Create_Data_Singlecell_DeadLoad()
        {
            List<string> list = new List<string>();
            #region  Singlecell DeadLoad
            list.Add(string.Format("ASTRA PLANE SINGLE CELL BOX CULVERT ANALYSIS"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1        0        -3.625        0"));
            list.Add(string.Format("2        9.3        -3.625        0"));
            list.Add(string.Format("3        9.3        4.75        0"));
            list.Add(string.Format("4        0        4.75        0"));
            list.Add(string.Format("5        0.1        -3.625        0"));
            list.Add(string.Format("6        0.2        -3.625        0"));
            list.Add(string.Format("7        0.3        -3.625        0"));
            list.Add(string.Format("8        0.4        -3.625        0"));
            list.Add(string.Format("9        0.5        -3.625        0"));
            list.Add(string.Format("10        0.6        -3.625        0"));
            list.Add(string.Format("11        0.7        -3.625        0"));
            list.Add(string.Format("12        0.8        -3.625        0"));
            list.Add(string.Format("13        0.9        -3.625        0"));
            list.Add(string.Format("14        1        -3.625        0"));
            list.Add(string.Format("15        1.1        -3.625        0"));
            list.Add(string.Format("16        1.2        -3.625        0"));
            list.Add(string.Format("17        1.4        -3.625        0"));
            list.Add(string.Format("18        1.5        -3.625        0"));
            list.Add(string.Format("19        1.6        -3.625        0"));
            list.Add(string.Format("20        1.7        -3.625        0"));
            list.Add(string.Format("21        1.8        -3.625        0"));
            list.Add(string.Format("22        1.9        -3.625        0"));
            list.Add(string.Format("23        2        -3.625        0"));
            list.Add(string.Format("24        2.1        -3.625        0"));
            list.Add(string.Format("25        2.2        -3.625        0"));
            list.Add(string.Format("26        2.3        -3.625        0"));
            list.Add(string.Format("27        2.4        -3.625        0"));
            list.Add(string.Format("28        2.5        -3.625        0"));
            list.Add(string.Format("29        2.6        -3.625        0"));
            list.Add(string.Format("30        2.7        -3.625        0"));
            list.Add(string.Format("31        2.8        -3.625        0"));
            list.Add(string.Format("32        2.9        -3.625        0"));
            list.Add(string.Format("33        3        -3.625        0"));
            list.Add(string.Format("34        3.1        -3.625        0"));
            list.Add(string.Format("35        3.2        -3.625        0"));
            list.Add(string.Format("36        3.3        -3.625        0"));
            list.Add(string.Format("37        3.4        -3.625        0"));
            list.Add(string.Format("38        3.5        -3.625        0"));
            list.Add(string.Format("39        3.6        -3.625        0"));
            list.Add(string.Format("40        3.7        -3.625        0"));
            list.Add(string.Format("41        3.8        -3.625        0"));
            list.Add(string.Format("42        3.9        -3.625        0"));
            list.Add(string.Format("43        4        -3.625        0"));
            list.Add(string.Format("44        4.1        -3.625        0"));
            list.Add(string.Format("45        4.2        -3.625        0"));
            list.Add(string.Format("46        4.3        -3.625        0"));
            list.Add(string.Format("47        4.4        -3.625        0"));
            list.Add(string.Format("48        4.5        -3.625        0"));
            list.Add(string.Format("49        4.6        -3.625        0"));
            list.Add(string.Format("50        4.7        -3.625        0"));
            list.Add(string.Format("51        4.8        -3.625        0"));
            list.Add(string.Format("52        4.9        -3.625        0"));
            list.Add(string.Format("53        5        -3.625        0"));
            list.Add(string.Format("54        5.1        -3.625        0"));
            list.Add(string.Format("55        5.2        -3.625        0"));
            list.Add(string.Format("56        5.3        -3.625        0"));
            list.Add(string.Format("57        5.4        -3.625        0"));
            list.Add(string.Format("58        5.5        -3.625        0"));
            list.Add(string.Format("59        5.6        -3.625        0"));
            list.Add(string.Format("60        5.7        -3.625        0"));
            list.Add(string.Format("61        5.8        -3.625        0"));
            list.Add(string.Format("62        5.9        -3.625        0"));
            list.Add(string.Format("63        6        -3.625        0"));
            list.Add(string.Format("64        6.1        -3.625        0"));
            list.Add(string.Format("65        6.2        -3.625        0"));
            list.Add(string.Format("66        6.3        -3.625        0"));
            list.Add(string.Format("67        6.4        -3.625        0"));
            list.Add(string.Format("68        6.5        -3.625        0"));
            list.Add(string.Format("69        6.6        -3.625        0"));
            list.Add(string.Format("70        6.7        -3.625        0"));
            list.Add(string.Format("71        6.8        -3.625        0"));
            list.Add(string.Format("72        6.9        -3.625        0"));
            list.Add(string.Format("73        7        -3.625        0"));
            list.Add(string.Format("74        7.1        -3.625        0"));
            list.Add(string.Format("75        7.2        -3.625        0"));
            list.Add(string.Format("76        7.3        -3.625        0"));
            list.Add(string.Format("77        7.4        -3.625        0"));
            list.Add(string.Format("78        7.5        -3.625        0"));
            list.Add(string.Format("79        7.6        -3.625        0"));
            list.Add(string.Format("80        7.7        -3.625        0"));
            list.Add(string.Format("81        7.8        -3.625        0"));
            list.Add(string.Format("82        7.9        -3.625        0"));
            list.Add(string.Format("83        8        -3.625        0"));
            list.Add(string.Format("84        8.1        -3.625        0"));
            list.Add(string.Format("85        8.2        -3.625        0"));
            list.Add(string.Format("86        8.3        -3.625        0"));
            list.Add(string.Format("87        8.4        -3.625        0"));
            list.Add(string.Format("88        8.5        -3.625        0"));
            list.Add(string.Format("89        8.6        -3.625        0"));
            list.Add(string.Format("90        8.7        -3.625        0"));
            list.Add(string.Format("91        8.8        -3.625        0"));
            list.Add(string.Format("92        8.9        -3.625        0"));
            list.Add(string.Format("93        9        -3.625        0"));
            list.Add(string.Format("94        9.1        -3.625        0"));
            list.Add(string.Format("95        9.2        -3.625        0"));
            list.Add(string.Format("96        1.3        -3.625        0"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1        2        3"));
            list.Add(string.Format("2        3        4"));
            list.Add(string.Format("3        4        1"));
            list.Add(string.Format("4        1        5"));
            list.Add(string.Format("5        5        6"));
            list.Add(string.Format("6        6        7"));
            list.Add(string.Format("7        7        8"));
            list.Add(string.Format("8        8        9"));
            list.Add(string.Format("9        9        10"));
            list.Add(string.Format("10        10        11"));
            list.Add(string.Format("11        11        12"));
            list.Add(string.Format("12        12        13"));
            list.Add(string.Format("13        13        14"));
            list.Add(string.Format("14        14        15"));
            list.Add(string.Format("15        15        16"));
            list.Add(string.Format("16        17        18"));
            list.Add(string.Format("17        18        19"));
            list.Add(string.Format("18        19        20"));
            list.Add(string.Format("19        20        21"));
            list.Add(string.Format("20        21        22"));
            list.Add(string.Format("21        22        23"));
            list.Add(string.Format("22        23        24"));
            list.Add(string.Format("23        24        25"));
            list.Add(string.Format("24        25        26"));
            list.Add(string.Format("25        26        27"));
            list.Add(string.Format("26        27        28"));
            list.Add(string.Format("27        28        29"));
            list.Add(string.Format("28        29        30"));
            list.Add(string.Format("29        30        31"));
            list.Add(string.Format("30        31        32"));
            list.Add(string.Format("31        32        33"));
            list.Add(string.Format("32        33        34"));
            list.Add(string.Format("33        34        35"));
            list.Add(string.Format("34        35        36"));
            list.Add(string.Format("35        36        37"));
            list.Add(string.Format("36        37        38"));
            list.Add(string.Format("37        38        39"));
            list.Add(string.Format("38        39        40"));
            list.Add(string.Format("39        40        41"));
            list.Add(string.Format("40        41        42"));
            list.Add(string.Format("41        42        43"));
            list.Add(string.Format("42        43        44"));
            list.Add(string.Format("43        44        45"));
            list.Add(string.Format("44        45        46"));
            list.Add(string.Format("45        46        47"));
            list.Add(string.Format("46        47        48"));
            list.Add(string.Format("47        48        49"));
            list.Add(string.Format("48        49        50"));
            list.Add(string.Format("49        50        51"));
            list.Add(string.Format("50        51        52"));
            list.Add(string.Format("51        52        53"));
            list.Add(string.Format("52        53        54"));
            list.Add(string.Format("53        54        55"));
            list.Add(string.Format("54        55        56"));
            list.Add(string.Format("55        56        57"));
            list.Add(string.Format("56        57        58"));
            list.Add(string.Format("57        58        59"));
            list.Add(string.Format("58        59        60"));
            list.Add(string.Format("59        60        61"));
            list.Add(string.Format("60        61        62"));
            list.Add(string.Format("61        62        63"));
            list.Add(string.Format("62        63        64"));
            list.Add(string.Format("63        64        65"));
            list.Add(string.Format("64        65        66"));
            list.Add(string.Format("65        66        67"));
            list.Add(string.Format("66        67        68"));
            list.Add(string.Format("67        68        69"));
            list.Add(string.Format("68        69        70"));
            list.Add(string.Format("69        70        71"));
            list.Add(string.Format("70        71        72"));
            list.Add(string.Format("71        72        73"));
            list.Add(string.Format("72        73        74"));
            list.Add(string.Format("73        74        75"));
            list.Add(string.Format("74        75        76"));
            list.Add(string.Format("75        76        77"));
            list.Add(string.Format("76        77        78"));
            list.Add(string.Format("77        78        79"));
            list.Add(string.Format("78        79        80"));
            list.Add(string.Format("79        80        81"));
            list.Add(string.Format("80        81        82"));
            list.Add(string.Format("81        82        83"));
            list.Add(string.Format("82        83        84"));
            list.Add(string.Format("83        84        85"));
            list.Add(string.Format("84        85        86"));
            list.Add(string.Format("85        86        87"));
            list.Add(string.Format("86        87        88"));
            list.Add(string.Format("87        88        89"));
            list.Add(string.Format("88        89        90"));
            list.Add(string.Format("89        90        91"));
            list.Add(string.Format("90        91        92"));
            list.Add(string.Format("91        92        93"));
            list.Add(string.Format("92        93        94"));
            list.Add(string.Format("93        94        95"));
            list.Add(string.Format("94        95        2"));
            list.Add(string.Format("95        16        96"));
            list.Add(string.Format("96        96        17"));
            list.Add(string.Format("MEMBER PROPERTY"));
            list.Add(string.Format("2 PRIS YD 0.7 ZD 1"));
            list.Add(string.Format("1 3 PRIS YD 0.65 ZD 1"));
            list.Add(string.Format("4 TO 96 PRIS YD 0.8 ZD 1"));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 23.5616 ALL"));
            list.Add(string.Format("ALPHA 1e-005 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("1 2 5 TO 96 FIXED BUT FX FZ MX MY MZ KFY 3613.81"));
            list.Add(string.Format("SELFWEIGHT Y -1"));
            list.Add(string.Format("LOAD 1 LOADTYPE NONE TITLE DL + SIDL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -13 0 9.3"));
            list.Add(string.Format("2 UNI GY -8.75"));
            list.Add(string.Format("LOAD 2 LOADTYPE NONE TITLE WATER LOAD"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TRAP GX 18.98 0 0 6.84"));
            list.Add(string.Format("3 TRAP GX 0 -18.98 1.5 8.37"));
            list.Add(string.Format("4 TO 94 UNI GY -18.98 0 0.1"));
            list.Add(string.Format("LOAD 3 LOADTYPE NONE TITLE SURCHARGE LOAD"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GX -10.8 0 8.37"));
            list.Add(string.Format("3 UNI GX 10.8 0 8.37"));
            list.Add(string.Format("LOAD 4 LOADTYPE NONE TITLE EARTH PRESSURE(BOTH SIDE)"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TRAP GX -82.35 0 0 8.37"));
            list.Add(string.Format("3 TRAP GX 0 81.35 0 8.37"));
            list.Add(string.Format("LOAD 5 LOADTYPE NONE TITLE EARTH PRESSURE(ONE SIDE)"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 TRAP GX 0 82.35 0 8.37"));
            list.Add(string.Format("LOAD 6 LOADTYPE LIVE TITLE BRAKING - 70RW"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("2 3 FX -26.67"));
            list.Add(string.Format("LOAD 7 LOADTYPE NONE TITLE EARTH CUSION"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("2 UNI GY -0.1"));
            list.Add(string.Format("LOAD 8 LOADTYPE TEMPERATURE TITLE TEMPERATURE RISE"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("4 FX 796"));
            list.Add(string.Format("3 FX -796"));
            list.Add(string.Format("3 MZ 189"));
            list.Add(string.Format("4 MZ -189"));
            list.Add(string.Format("LOAD 9 LOADTYPE TEMPERATURE TITLE TEMPERATURE FALL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("3 FX 574"));
            list.Add(string.Format("4 FX -547"));
            list.Add(string.Format("3 MZ -33.3"));
            list.Add(string.Format("4 MZ -33.3"));
            list.Add(string.Format("LOAD 10 LOADTYPE DEAD TITLE SIDL WITH WEARING COAT"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 UNI GY -1.65"));
            list.Add(string.Format("LOAD COMB 11 COMBINATION LOAD CASE 11"));
            list.Add(string.Format("1 1.35 10 1.75 "));
            list.Add(string.Format("LOAD COMB 12 COMBINATION LOAD CASE 12"));
            list.Add(string.Format("1 1.35 10 1.75 5 1.50 "));
            list.Add(string.Format("LOAD COMB 13 COMBINATION LOAD CASE 13"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.50 "));
            list.Add(string.Format("LOAD COMB 14 COMBINATION LOAD CASE 14"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.50 7 1.35 "));
            list.Add(string.Format("LOAD COMB 15 COMBINATION LOAD CASE 15"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.50 7 1.35 6 1.50 "));
            list.Add(string.Format("LOAD COMB 16 COMBINATION LOAD CASE 16"));
            list.Add(string.Format("1 1.35 10 1.75 4 1.50 7 1.35 3 1.20 6 1.50 "));
            list.Add(string.Format("LOAD COMB 17 REACTION"));
            list.Add(string.Format("1 1.00 2 1.00 4 1.00 7 1.00 10 1.00 "));
            list.Add(string.Format("FINISH"));

            #endregion  Singlecell DeadLoad


            File.WriteAllLines(Input_File_DL, list.ToArray());
        }
        public void Create_Data_Singlecell_LiveLoad()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("ASTRA PLANE Input LIVE   LOAD "));
            list.Add(string.Format("UNIT METER KN"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1      9.3      4.75      0"));
            list.Add(string.Format("2      0      4.75      0"));
            list.Add(string.Format("3      9.3      -6.46      0"));
            list.Add(string.Format("4      0      -6.46      0"));
            list.Add(string.Format("5      0.1      -6.46      0"));
            list.Add(string.Format("6      0.6      -6.46      0"));
            list.Add(string.Format("7      0.7      -6.46      0"));
            list.Add(string.Format("8      0.8      -6.46      0"));
            list.Add(string.Format("9      0.9      -6.46      0"));
            list.Add(string.Format("10      1      -6.46      0"));
            list.Add(string.Format("11      1.1      -6.46      0"));
            list.Add(string.Format("12      1.2      -6.46      0"));
            list.Add(string.Format("13      1.3      -6.46      0"));
            list.Add(string.Format("14      1.4      -6.46      0"));
            list.Add(string.Format("15      1.5      -6.46      0"));
            list.Add(string.Format("16      1.6      -6.46      0"));
            list.Add(string.Format("17      1.7      -6.46      0"));
            list.Add(string.Format("18      1.8      -6.46      0"));
            list.Add(string.Format("19      1.9      -6.46      0"));
            list.Add(string.Format("20      2      -6.46      0"));
            list.Add(string.Format("21      2.1      -6.46      0"));
            list.Add(string.Format("22      2.2      -6.46      0"));
            list.Add(string.Format("23      2.3      -6.46      0"));
            list.Add(string.Format("24      2.4      -6.46      0"));
            list.Add(string.Format("25      2.5      -6.46      0"));
            list.Add(string.Format("26      2.6      -6.46      0"));
            list.Add(string.Format("27      2.7      -6.46      0"));
            list.Add(string.Format("28      2.8      -6.46      0"));
            list.Add(string.Format("29      2.9      -6.46      0"));
            list.Add(string.Format("30      3      -6.46      0"));
            list.Add(string.Format("31      3.1      -6.46      0"));
            list.Add(string.Format("32      3.2      -6.46      0"));
            list.Add(string.Format("33      3.3      -6.46      0"));
            list.Add(string.Format("34      3.4      -6.46      0"));
            list.Add(string.Format("35      3.5      -6.46      0"));
            list.Add(string.Format("36      3.6      -6.46      0"));
            list.Add(string.Format("37      3.7      -6.46      0"));
            list.Add(string.Format("38      3.8      -6.46      0"));
            list.Add(string.Format("39      3.9      -6.46      0"));
            list.Add(string.Format("40      4      -6.46      0"));
            list.Add(string.Format("41      4.1      -6.46      0"));
            list.Add(string.Format("42      4.2      -6.46      0"));
            list.Add(string.Format("43      4.3      -6.46      0"));
            list.Add(string.Format("44      4.4      -6.46      0"));
            list.Add(string.Format("45      4.5      -6.46      0"));
            list.Add(string.Format("46      4.6      -6.46      0"));
            list.Add(string.Format("47      4.7      -6.46      0"));
            list.Add(string.Format("48      4.8      -6.46      0"));
            list.Add(string.Format("49      4.9      -6.46      0"));
            list.Add(string.Format("50      5      -6.46      0"));
            list.Add(string.Format("51      5.1      -6.46      0"));
            list.Add(string.Format("52      5.2      -6.46      0"));
            list.Add(string.Format("53      5.3      -6.46      0"));
            list.Add(string.Format("54      5.4      -6.46      0"));
            list.Add(string.Format("55      5.5      -6.46      0"));
            list.Add(string.Format("56      5.6      -6.46      0"));
            list.Add(string.Format("57      5.7      -6.46      0"));
            list.Add(string.Format("58      5.8      -6.46      0"));
            list.Add(string.Format("59      5.9      -6.46      0"));
            list.Add(string.Format("60      6      -6.46      0"));
            list.Add(string.Format("61      6.1      -6.46      0"));
            list.Add(string.Format("62      6.2      -6.46      0"));
            list.Add(string.Format("63      6.3      -6.46      0"));
            list.Add(string.Format("64      6.4      -6.46      0"));
            list.Add(string.Format("65      6.5      -6.46      0"));
            list.Add(string.Format("66      6.6      -6.46      0"));
            list.Add(string.Format("67      6.7      -6.46      0"));
            list.Add(string.Format("68      6.8      -6.46      0"));
            list.Add(string.Format("69      6.9      -6.46      0"));
            list.Add(string.Format("70      7      -6.46      0"));
            list.Add(string.Format("71      7.1      -6.46      0"));
            list.Add(string.Format("72      7.2      -6.46      0"));
            list.Add(string.Format("73      7.3      -6.46      0"));
            list.Add(string.Format("74      7.4      -6.46      0"));
            list.Add(string.Format("75      7.5      -6.46      0"));
            list.Add(string.Format("76      7.6      -6.46      0"));
            list.Add(string.Format("77      7.7      -6.46      0"));
            list.Add(string.Format("78      7.8      -6.46      0"));
            list.Add(string.Format("79      7.9      -6.46      0"));
            list.Add(string.Format("80      8      -6.46      0"));
            list.Add(string.Format("81      8.1      -6.46      0"));
            list.Add(string.Format("82      8.2      -6.46      0"));
            list.Add(string.Format("83      8.3      -6.46      0"));
            list.Add(string.Format("84      8.4      -6.46      0"));
            list.Add(string.Format("85      8.5      -6.46      0"));
            list.Add(string.Format("86      8.6      -6.46      0"));
            list.Add(string.Format("87      8.7      -6.46      0"));
            list.Add(string.Format("88      8.8      -6.46      0"));
            list.Add(string.Format("89      8.9      -6.46      0"));
            list.Add(string.Format("90      9      -6.46      0"));
            list.Add(string.Format("91      9.1      -6.46      0"));
            list.Add(string.Format("92      9.2      -6.46      0"));
            list.Add(string.Format("MEMBER      INCIDENCES"));
            list.Add(string.Format("1      2      1"));
            list.Add(string.Format("2      2      4"));
            list.Add(string.Format("3      1      3"));
            list.Add(string.Format("4      4      5"));
            list.Add(string.Format("5      5      6"));
            list.Add(string.Format("6      6      7"));
            list.Add(string.Format("7      7      8"));
            list.Add(string.Format("8      8      9"));
            list.Add(string.Format("9      9      10"));
            list.Add(string.Format("10      10      11"));
            list.Add(string.Format("11      11      12"));
            list.Add(string.Format("12      12      13"));
            list.Add(string.Format("13      13      14"));
            list.Add(string.Format("14      14      15"));
            list.Add(string.Format("15      15      16"));
            list.Add(string.Format("16      16      17"));
            list.Add(string.Format("17      17      18"));
            list.Add(string.Format("18      18      19"));
            list.Add(string.Format("19      19      20"));
            list.Add(string.Format("20      20      21"));
            list.Add(string.Format("21      21      22"));
            list.Add(string.Format("22      22      23"));
            list.Add(string.Format("23      23      24"));
            list.Add(string.Format("24      24      25"));
            list.Add(string.Format("25      25      26"));
            list.Add(string.Format("26      26      27"));
            list.Add(string.Format("27      27      28"));
            list.Add(string.Format("28      28      29"));
            list.Add(string.Format("29      29      30"));
            list.Add(string.Format("30      30      31"));
            list.Add(string.Format("31      31      32"));
            list.Add(string.Format("32      32      33"));
            list.Add(string.Format("33      33      34"));
            list.Add(string.Format("34      34      35"));
            list.Add(string.Format("35      35      36"));
            list.Add(string.Format("36      36      37"));
            list.Add(string.Format("37      37      38"));
            list.Add(string.Format("38      38      39"));
            list.Add(string.Format("39      39      40"));
            list.Add(string.Format("40      40      41"));
            list.Add(string.Format("41      41      42"));
            list.Add(string.Format("42      42      43"));
            list.Add(string.Format("43      43      44"));
            list.Add(string.Format("44      44      45"));
            list.Add(string.Format("45      45      46"));
            list.Add(string.Format("46      46      47"));
            list.Add(string.Format("47      47      48"));
            list.Add(string.Format("48      48      49"));
            list.Add(string.Format("49      49      50"));
            list.Add(string.Format("50      50      51"));
            list.Add(string.Format("51      51      52"));
            list.Add(string.Format("52      52      53"));
            list.Add(string.Format("53      53      54"));
            list.Add(string.Format("54      54      55"));
            list.Add(string.Format("55      55      56"));
            list.Add(string.Format("56      56      57"));
            list.Add(string.Format("57      57      58"));
            list.Add(string.Format("58      58      59"));
            list.Add(string.Format("59      59      60"));
            list.Add(string.Format("60      60      61"));
            list.Add(string.Format("61      61      62"));
            list.Add(string.Format("62      62      63"));
            list.Add(string.Format("63      63      64"));
            list.Add(string.Format("64      64      65"));
            list.Add(string.Format("65      65      66"));
            list.Add(string.Format("66      66      67"));
            list.Add(string.Format("67      67      68"));
            list.Add(string.Format("68      68      69"));
            list.Add(string.Format("69      69      70"));
            list.Add(string.Format("70      70      71"));
            list.Add(string.Format("71      71      72"));
            list.Add(string.Format("72      72      73"));
            list.Add(string.Format("73      73      74"));
            list.Add(string.Format("74      74      75"));
            list.Add(string.Format("75      75      76"));
            list.Add(string.Format("76      76      77"));
            list.Add(string.Format("77      77      78"));
            list.Add(string.Format("78      78      79"));
            list.Add(string.Format("79      79      80"));
            list.Add(string.Format("80      80      81"));
            list.Add(string.Format("81      81      82"));
            list.Add(string.Format("82      82      83"));
            list.Add(string.Format("83      83      84"));
            list.Add(string.Format("84      84      85"));
            list.Add(string.Format("85      85      86"));
            list.Add(string.Format("86      86      87"));
            list.Add(string.Format("87      87      88"));
            list.Add(string.Format("88      88      89"));
            list.Add(string.Format("89      89      90"));
            list.Add(string.Format("90      90      91"));
            list.Add(string.Format("91      91      92"));
            list.Add(string.Format("92      92      3"));
            list.Add(string.Format("MEMBER PROPERTY  "));
            list.Add(string.Format("1 PRIS YD 0.7 ZD 1"));
            list.Add(string.Format("2 3 PRIS YD 0.8 ZD 1"));
            list.Add(string.Format("4 TO 92 PRIS YD 0.8 ZD 1"));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 23.5616 ALL"));
            list.Add(string.Format("ALPHA 1e-005 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("3 TO 96 FIXED BUT FX FZ MX MY MZ KFY 4131.29"));
            list.Add(string.Format("LOAD 1 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -51.9 0 0.9"));
            list.Add(string.Format("LOAD 2 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -30.6 0 1.15"));
            list.Add(string.Format("LOAD 3 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -22.1 0 1.4"));
            list.Add(string.Format("LOAD 4 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.5 0 1.65"));
            list.Add(string.Format("LOAD 5 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.7 0.1 1.9"));
            list.Add(string.Format("LOAD 6 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.8 0.35 2.15"));
            list.Add(string.Format("LOAD 7 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.4 0.6 2.4"));
            list.Add(string.Format("1 UNI GY -37.99 0 1.03"));
            list.Add(string.Format("LOAD 8 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.4 0.85 2.65"));
            list.Add(string.Format("1 UNI GY -25.45 0 1.28"));
            list.Add(string.Format("LOAD 9 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.6 1.1 2.9"));
            list.Add(string.Format("1 UNI GY -19.43 0 1.53"));
            list.Add(string.Format("LOAD 10 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9 1.35 3.15"));
            list.Add(string.Format("1 UNI GY -15.9 0 1.78"));
            list.Add(string.Format("LOAD 11 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.5 1.6 3.4"));
            list.Add(string.Format("1 UNI GY -13.61 0.23 2.03"));
            list.Add(string.Format("LOAD 12 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.1 1.85 3.65"));
            list.Add(string.Format("1 UNI GY -12 0.48 2.28"));
            list.Add(string.Format("LOAD 13 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.8 2.1 3.9"));
            list.Add(string.Format("1 UNI GY -10.82 0.73 2.53"));
            list.Add(string.Format("LOAD 14 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.6 2.35 4.15"));
            list.Add(string.Format("1 UNI GY -9.93 0.98 2.78"));
            list.Add(string.Format("LOAD 15 LOADTYPE Live  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.4 2.6 4.4"));
            list.Add(string.Format("1 UNI GY -9.25 1.23 3.03"));
            list.Add(string.Format("LOAD 16 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.3 2.85 4.65"));
            list.Add(string.Format("1 UNI GY -8.71 1.48 3.28"));
            list.Add(string.Format("LOAD 17 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.2 3.1 4.9"));
            list.Add(string.Format("1 UNI GY -8.28 1.73 3.53"));
            list.Add(string.Format("LOAD 18 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.1 3.35 5.15"));
            list.Add(string.Format("1 UNI GY -7.94 1.98 3.78"));
            list.Add(string.Format("LOAD 19 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.1 3.6 5.4"));
            list.Add(string.Format("1 UNI GY -7.67 2.23 4.03"));
            list.Add(string.Format("1 UNI GY -42.31 -0.82 0.98"));
            list.Add(string.Format("LOAD 20 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.2 3.85 5.65"));
            list.Add(string.Format("1 UNI GY -7.47 2.48 4.28"));
            list.Add(string.Format("1 UNI GY -27.2 0 1.23"));
            list.Add(string.Format("LOAD 21 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.2 4.1 5.9"));
            list.Add(string.Format("1 UNI GY -7.32 2.73 4.53"));
            list.Add(string.Format("1 UNI GY -20.37 0 1.48"));
            list.Add(string.Format("LOAD 22 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.2 4.35 6.15"));
            list.Add(string.Format("1 UNI GY -7.21 2.98 4.78"));
            list.Add(string.Format("1 UNI GY -16.49 0 1.73"));
            list.Add(string.Format("LOAD 23 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.1 4.6 6.4"));
            list.Add(string.Format("1 UNI GY -7.15 3.23 5.03"));
            list.Add(string.Format("1 UNI GY -14 0.18 1.98"));
            list.Add(string.Format("LOAD 24 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.1 4.85 6.65"));
            list.Add(string.Format("1 UNI GY -7.12 3.48 5.28"));
            list.Add(string.Format("1 UNI GY -12.28 0.43 2.23"));
            list.Add(string.Format("LOAD 25 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.2 5.1 6.9"));
            list.Add(string.Format("1 UNI GY -7.14 3.73 5.53"));
            list.Add(string.Format("1 UNI GY -11.03 0.68 2.48"));
            list.Add(string.Format("1 UNI GY -32.7 0 1.11"));
            list.Add(string.Format("LOAD 26 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.3 5.35 7.15"));
            list.Add(string.Format("1 UNI GY -7.2 3.98 5.78"));
            list.Add(string.Format("1 UNI GY -10.09 0.93 2.73"));
            list.Add(string.Format("1 UNI GY -23.1 0 1.36"));
            list.Add(string.Format("LOAD 27 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.4 5.6 7.4"));
            list.Add(string.Format("1 UNI GY -7.19 4.23 6.03"));
            list.Add(string.Format("1 UNI GY -9.37 1.18 2.98"));
            list.Add(string.Format("1 UNI GY -18.1 0 1.61"));
            list.Add(string.Format("LOAD 28 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.6 5.85 7.65"));
            list.Add(string.Format("1 UNI GY -7.14 4.48 6.28"));
            list.Add(string.Format("1 UNI GY -8.8 1.43 3.23"));
            list.Add(string.Format("1 UNI GY -15.1 0.06 1.86"));
            list.Add(string.Format("LOAD 29 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.8 6.1 7.9"));
            list.Add(string.Format("1 UNI GY -7.12 4.73 6.53"));
            list.Add(string.Format("1 UNI GY -8.36 1.68 3.48"));
            list.Add(string.Format("1 UNI GY -13 0.31 2.11"));
            list.Add(string.Format("LOAD 30 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.1 6.35 8.15"));
            list.Add(string.Format("1 UNI GY -7.15 4.98 6.78"));
            list.Add(string.Format("1 UNI GY -8 1.93 3.73"));
            list.Add(string.Format("1 UNI GY -11.6 0.56 2.36"));
            list.Add(string.Format("LOAD 31 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.5 6.6 8.4"));
            list.Add(string.Format("1 UNI GY -7.21 5.23 7.03"));
            list.Add(string.Format("1 UNI GY -7.72 2.18 3.98"));
            list.Add(string.Format("1 UNI GY -10.5 0.81 2.61"));
            list.Add(string.Format("LOAD 32 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9 6.85 8.65"));
            list.Add(string.Format("1 UNI GY -7.32 5.48 7.28"));
            list.Add(string.Format("1 UNI GY -7.51 2.43 4.23"));
            list.Add(string.Format("1 UNI GY -9.7 1.06 2.86"));
            list.Add(string.Format("LOAD 33 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.6 7.1 8.9"));
            list.Add(string.Format("1 UNI GY -7.48 5.73 7.53"));
            list.Add(string.Format("1 UNI GY -7.34 2.68 4.48"));
            list.Add(string.Format("1 UNI GY -9.1 1.31 3.11"));
            list.Add(string.Format("1 UNI GY -29.87 0 0.98"));
            list.Add(string.Format("LOAD 34 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.4 7.35 9.15"));
            list.Add(string.Format("1 UNI GY -7.68 5.98 7.78"));
            list.Add(string.Format("1 UNI GY -7.23 2.93 4.73"));
            list.Add(string.Format("1 UNI GY -8.6 1.56 3.36"));
            list.Add(string.Format("1 UNI GY -19.2 0 1.23"));
            list.Add(string.Format("LOAD 35 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.4 7.6 9.36"));
            list.Add(string.Format("1 UNI GY -7.95 6.23 8.03"));
            list.Add(string.Format("1 UNI GY -7.16 3.18 4.98"));
            list.Add(string.Format("1 UNI GY -8.2 1.81 3.61"));
            list.Add(string.Format("1 UNI GY -14.38 0 1.48"));
            list.Add(string.Format("LOAD 36 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.8 7.85 9.36"));
            list.Add(string.Format("1 UNI GY -8.29 6.48 8.28"));
            list.Add(string.Format("1 UNI GY -7.12 3.43 5.23"));
            list.Add(string.Format("1 UNI GY -7.8 2.06 3.86"));
            list.Add(string.Format("1 UNI GY -11.64 0 1.73"));
            list.Add(string.Format("LOAD 37 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.7 8.1 9.36"));
            list.Add(string.Format("1 UNI GY -8.72 6.73 8.53"));
            list.Add(string.Format("1 UNI GY -7.13 3.68 5.48"));
            list.Add(string.Format("1 UNI GY -7.6 2.31 4.11"));
            list.Add(string.Format("1 UNI GY -9.88 0.08 1.98"));
            list.Add(string.Format("LOAD 38 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.5 8.35 9.36"));
            list.Add(string.Format("1 UNI GY -9.27 6.98 8.78"));
            list.Add(string.Format("1 UNI GY -7.18 3.93 5.73"));
            list.Add(string.Format("1 UNI GY -7.4 2.56 4.36"));
            list.Add(string.Format("1 UNI GY -8.67 0.33 2.23"));
            list.Add(string.Format("LOAD 39 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.96 7.23 9.03"));
            list.Add(string.Format("1 UNI GY -7.21 4.18 5.98"));
            list.Add(string.Format("1 UNI GY -7.3 2.81 4.61"));
            list.Add(string.Format("1 UNI GY -7.79 0.58 2.48"));
            list.Add(string.Format("1 UNI GY -31.3 0 0.96"));
            list.Add(string.Format("LOAD 40 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.86 7.48 9.28"));
            list.Add(string.Format("1 UNI GY -7.15 4.43 6.23"));
            list.Add(string.Format("1 UNI GY -7.2 3.06 4.86"));
            list.Add(string.Format("1 UNI GY -7.12 0.83 2.73"));
            list.Add(string.Format("1 UNI GY -19.75 0 1.21"));
            list.Add(string.Format("LOAD 41 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.05 7.73 9.36"));
            list.Add(string.Format("1 UNI GY -7.12 4.68 6.48"));
            list.Add(string.Format("1 UNI GY -7.1 3.31 5.11"));
            list.Add(string.Format("1 UNI GY -6.61 1.08 2.98"));
            list.Add(string.Format("1 UNI GY -14.66 0 1.46"));
            list.Add(string.Format("LOAD 42 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.68 7.98 9.36"));
            list.Add(string.Format("1 UNI GY -7.14 4.93 6.73"));
            list.Add(string.Format("1 UNI GY -7.1 3.56 5.36"));
            list.Add(string.Format("1 UNI GY -6.21 1.33 3.23"));
            list.Add(string.Format("1 UNI GY -11.81 0 1.71"));
            list.Add(string.Format("LOAD 43 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.02 8.23 9.36"));
            list.Add(string.Format("1 UNI GY -7.2 5.18 6.98"));
            list.Add(string.Format("1 UNI GY -7.2 3.81 5.61"));
            list.Add(string.Format("1 UNI GY -5.9 1.58 3.48"));
            list.Add(string.Format("1 UNI GY -10 0.16 1.96"));
            list.Add(string.Format("LOAD 44 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -19.61 8.48 9.36"));
            list.Add(string.Format("1 UNI GY -7.3 5.43 7.23"));
            list.Add(string.Format("1 UNI GY -7.2 4.06 5.86"));
            list.Add(string.Format("1 UNI GY -5.65 1.83 3.73"));
            list.Add(string.Format("1 UNI GY -8.75 0.41 2.21"));
            list.Add(string.Format("LOAD 45 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.44 5.68 7.48"));
            list.Add(string.Format("1 UNI GY -7.2 4.31 6.11"));
            list.Add(string.Format("1 UNI GY -5.45 2.08 3.98"));
            list.Add(string.Format("1 UNI GY -7.85 0.66 2.46"));
            list.Add(string.Format("1 UNI GY -7.17 0.91 2.71"));
            list.Add(string.Format("LOAD 46 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.64 5.93 7.73"));
            list.Add(string.Format("1 UNI GY -7.1 4.56 6.36"));
            list.Add(string.Format("1 UNI GY -5.3 2.33 4.23"));
            list.Add(string.Format("1 UNI GY -6.65 1.16 2.96"));
            list.Add(string.Format("LOAD 47 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.89 6.18 7.98"));
            list.Add(string.Format("1 UNI GY -7.1 4.81 6.61"));
            list.Add(string.Format("1 UNI GY -5.18 2.58 4.48"));
            list.Add(string.Format("1 UNI GY -6.24 1.41 3.21"));
            list.Add(string.Format("LOAD 48 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.22 6.43 8.23"));
            list.Add(string.Format("1 UNI GY -7.2 5.06 6.86"));
            list.Add(string.Format("1 UNI GY -5.1 2.83 4.73"));
            list.Add(string.Format("1 UNI GY -5.92 1.66 3.46"));
            list.Add(string.Format("LOAD 49 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.63 6.68 8.48"));
            list.Add(string.Format("1 UNI GY -7.2 5.31 7.11"));
            list.Add(string.Format("1 UNI GY -5.05 3.08 4.98"));
            list.Add(string.Format("1 UNI GY -5.67 1.91 3.71"));
            list.Add(string.Format("LOAD 50 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.15 6.93 8.73"));
            list.Add(string.Format("1 UNI GY -7.4 5.56 7.36"));
            list.Add(string.Format("1 UNI GY -5.03 3.33 5.23"));
            list.Add(string.Format("1 UNI GY -5.46 2.16 3.96"));
            list.Add(string.Format("LOAD 51 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.81 7.18 8.98"));
            list.Add(string.Format("1 UNI GY -7.5 5.81 7.61"));
            list.Add(string.Format("1 UNI GY -5.03 3.58 5.48"));
            list.Add(string.Format("1 UNI GY -5.31 2.41 4.21"));
            list.Add(string.Format("LOAD 52 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.66 7.43 9.23"));
            list.Add(string.Format("1 UNI GY -7.8 6.06 7.86"));
            list.Add(string.Format("1 UNI GY -5.07 3.83 5.73"));
            list.Add(string.Format("1 UNI GY -5.19 2.66 4.46"));
            list.Add(string.Format("LOAD 53 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.79 7.68 9.36"));
            list.Add(string.Format("1 UNI GY -8.1 6.31 8.11"));
            list.Add(string.Format("1 UNI GY -5.09 4.08 5.98"));
            list.Add(string.Format("1 UNI GY -5.11 2.91 4.71"));
            list.Add(string.Format("LOAD 54 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.31 7.93 9.36"));
            list.Add(string.Format("1 UNI GY -8.4 6.56 8.36"));
            list.Add(string.Format("1 UNI GY -5.04 4.33 6.23"));
            list.Add(string.Format("1 UNI GY -5.05 3.16 4.96"));
            list.Add(string.Format("LOAD 55 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.47 8.18 9.36"));
            list.Add(string.Format("1 UNI GY -8.9 6.81 8.61"));
            list.Add(string.Format("1 UNI GY -5.03 4.58 6.48"));
            list.Add(string.Format("1 UNI GY -5.03 3.41 5.21"));
            list.Add(string.Format("1 UNI GY -19.04 0 1"));
            list.Add(string.Format("LOAD 56 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -18.74 8.43 9.36"));
            list.Add(string.Format("1 UNI GY -9.5 7.06 8.86"));
            list.Add(string.Format("1 UNI GY -5.04 4.83 6.73"));
            list.Add(string.Format("1 UNI GY -5.03 3.66 5.46"));
            list.Add(string.Format("1 UNI GY -12.46 0 1.25"));
            list.Add(string.Format("LOAD 57 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.2 7.31 9.11"));
            list.Add(string.Format("1 UNI GY -5.08 5.08 6.98"));
            list.Add(string.Format("1 UNI GY -5.07 3.91 5.71"));
            list.Add(string.Format("1 UNI GY -9.4 0 1.5"));
            list.Add(string.Format("LOAD 58 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.2 7.56 9.36"));
            list.Add(string.Format("1 UNI GY -5.15 5.33 7.23"));
            list.Add(string.Format("1 UNI GY -5.09 4.16 5.96"));
            list.Add(string.Format("1 UNI GY -7.65 0 1.75"));
            list.Add(string.Format("LOAD 59 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.5 7.81 9.36"));
            list.Add(string.Format("1 UNI GY -5.25 5.58 7.48"));
            list.Add(string.Format("1 UNI GY -5.05 4.41 6.21"));
            list.Add(string.Format("1 UNI GY -6.51 0.2 2"));
            list.Add(string.Format("LOAD 60 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.3 8.06 9.36"));
            list.Add(string.Format("1 UNI GY -5.39 5.83 7.73"));
            list.Add(string.Format("1 UNI GY -5.03 4.66 6.46"));
            list.Add(string.Format("1 UNI GY -5.73 0.45 2.25"));
            list.Add(string.Format("LOAD 61 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.57 6.08 7.98"));
            list.Add(string.Format("1 UNI GY -5.04 4.91 6.71"));
            list.Add(string.Format("1 UNI GY -5.15 0.7 2.5"));
            list.Add(string.Format("LOAD 62 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.8 6.33 8.23"));
            list.Add(string.Format("1 UNI GY -5.08 5.16 6.96"));
            list.Add(string.Format("1 UNI GY -4.72 0.95 2.75"));
            list.Add(string.Format("LOAD 63 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.09 6.58 8.48"));
            list.Add(string.Format("1 UNI GY -5.14 5.41 7.21"));
            list.Add(string.Format("1 UNI GY -4.39 1.2 3"));
            list.Add(string.Format("LOAD 64 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.46 6.83 8.73"));
            list.Add(string.Format("1 UNI GY -5.24 5.66 7.46"));
            list.Add(string.Format("1 UNI GY -4.12 1.45 3.25"));
            list.Add(string.Format("LOAD 65 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.93 7.08 8.98"));
            list.Add(string.Format("1 UNI GY -5.38 5.91 7.71"));
            list.Add(string.Format("1 UNI GY -3.92 1.7 3.5"));
            list.Add(string.Format("LOAD 66 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.53 7.33 9.23"));
            list.Add(string.Format("1 UNI GY -5.56 6.16 7.96"));
            list.Add(string.Format("1 UNI GY -3.75 1.95 3.75"));
            list.Add(string.Format("LOAD 67 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.32 7.58 9.36"));
            list.Add(string.Format("1 UNI GY -5.78 6.41 8.21"));
            list.Add(string.Format("1 UNI GY -3.62 2.2 4"));
            list.Add(string.Format("LOAD 68 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.4 7.83 9.36"));
            list.Add(string.Format("1 UNI GY -6.07 6.66 8.46"));
            list.Add(string.Format("1 UNI GY -3.52 2.45 4.25"));
            list.Add(string.Format("LOAD 69 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.92 8.08 9.36"));
            list.Add(string.Format("1 UNI GY -6.43 6.91 8.71"));
            list.Add(string.Format("1 UNI GY -3.45 2.7 4.5"));
            list.Add(string.Format("LOAD 70 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.23 8.33 9.36"));
            list.Add(string.Format("1 UNI GY -6.88 7.16 8.96"));
            list.Add(string.Format("1 UNI GY -3.4 2.95 4.75"));
            list.Add(string.Format("LOAD 71 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.47 7.41 9.21"));
            list.Add(string.Format("1 UNI GY -3.37 3.2 5"));
            list.Add(string.Format("LOAD 72 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.25 7.66 9.36"));
            list.Add(string.Format("1 UNI GY -3.35 3.45 5.25"));
            list.Add(string.Format("LOAD 73 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.3 7.91 9.36"));
            list.Add(string.Format("1 UNI GY -3.36 3.7 5.5"));
            list.Add(string.Format("LOAD 74 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.78 8.16 9.36"));
            list.Add(string.Format("1 UNI GY -3.38 3.95 5.75"));
            list.Add(string.Format("LOAD 75 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13 8.41 9.36"));
            list.Add(string.Format("1 UNI GY -3.39 4.2 6"));
            list.Add(string.Format("LOAD 76 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.36 4.45 6.25"));
            list.Add(string.Format("LOAD 77 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.35 4.7 6.5"));
            list.Add(string.Format("LOAD 78 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.36 4.95 6.75"));
            list.Add(string.Format("LOAD 79 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.39 5.2 7"));
            list.Add(string.Format("LOAD 80 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.44 5.45 7.25"));
            list.Add(string.Format("LOAD 81 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.51 5.7 7.5"));
            list.Add(string.Format("LOAD 82 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.6 5.95 7.75"));
            list.Add(string.Format("LOAD 83 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.73 6.2 8"));
            list.Add(string.Format("LOAD 84 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.88 6.45 8.25"));
            list.Add(string.Format("LOAD 85 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.08 6.7 8.5"));
            list.Add(string.Format("LOAD 86 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.33 6.95 8.75"));
            list.Add(string.Format("LOAD 87 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.65 7.2 9"));
            list.Add(string.Format("LOAD 88 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.06 7.45 9.25"));
            list.Add(string.Format("LOAD 89 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.6 7.7 9.36"));
            list.Add(string.Format("LOAD 90 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.33 7.95 9.36"));
            list.Add(string.Format("LOAD 91 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.38 8.2 9.36"));
            list.Add(string.Format("LOAD 92 LOADTYPE Dead  TITLE 70R WHEEL  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.98 8.45 9.36"));
            list.Add(string.Format("LOAD 101 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.59 0 0.965"));
            list.Add(string.Format("LOAD 102 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.79 0 1.215"));
            list.Add(string.Format("LOAD 103 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.07 0 1.465"));
            list.Add(string.Format("LOAD 104 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.6 0 1.715"));
            list.Add(string.Format("LOAD 105 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.7 0.035 1.965"));
            list.Add(string.Format("1 UNI GY -16.64 0 1.78"));
            list.Add(string.Format("LOAD 106 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.08 0.285 2.215"));
            list.Add(string.Format("1 UNI GY -11.95 0 2.03"));
            list.Add(string.Format("LOAD 107 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.64 0.535 2.465"));
            list.Add(string.Format("1 UNI GY -9.45 0 2.28"));
            list.Add(string.Format("LOAD 108 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.31 0.785 2.715"));
            list.Add(string.Format("1 UNI GY -7.9 0 2.53"));
            list.Add(string.Format("1 UNI GY -16.97 0 1.78"));
            list.Add(string.Format("LOAD 109 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.87 1.285 3.215"));
            list.Add(string.Format("1 UNI GY -6.11 0.33 3.03"));
            list.Add(string.Format("1 UNI GY -9.54 0 2.28"));
            list.Add(string.Format("LOAD 110 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.71 1.535 3.465"));
            list.Add(string.Format("1 UNI GY -5.56 0.58 3.28"));
            list.Add(string.Format("1 UNI GY -7.96 0 2.53"));
            list.Add(string.Format("LOAD 111 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.59 1.785 3.715"));
            list.Add(string.Format("1 UNI GY -5.14 0.83 3.53"));
            list.Add(string.Format("1 UNI GY -6.9 0.08 2.78"));
            list.Add(string.Format("1 UNI GY -17.31 0 1.77"));
            list.Add(string.Format("LOAD 112 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.5 2.035 3.965"));
            list.Add(string.Format("1 UNI GY -4.81 1.08 3.78"));
            list.Add(string.Format("1 UNI GY -6.15 0.33 3.03"));
            list.Add(string.Format("1 UNI GY -12.27 0 2.02"));
            list.Add(string.Format("LOAD 113 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.42 2.285 4.215"));
            list.Add(string.Format("1 UNI GY -4.55 1.33 4.03"));
            list.Add(string.Format("1 UNI GY -5.58 0.58 3.28"));
            list.Add(string.Format("1 UNI GY -9.63 0 2.27"));
            list.Add(string.Format("LOAD 114 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.36 2.535 4.465"));
            list.Add(string.Format("1 UNI GY -4.34 1.58 4.28"));
            list.Add(string.Format("1 UNI GY -5.15 0.83 3.53"));
            list.Add(string.Format("1 UNI GY -8.02 0 2.52"));
            list.Add(string.Format("1 UNI GY -17.67 0 1.75"));
            list.Add(string.Format("LOAD 115 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.32 2.785 4.715"));
            list.Add(string.Format("1 UNI GY -4.18 1.83 4.53"));
            list.Add(string.Format("1 UNI GY -4.82 1.08 3.78"));
            list.Add(string.Format("1 UNI GY -6.94 0 2.77"));
            list.Add(string.Format("1 UNI GY -12.44 0 2"));
            list.Add(string.Format("LOAD 116 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.29 3.035 4.965"));
            list.Add(string.Format("1 UNI GY -4.05 2.08 4.78"));
            list.Add(string.Format("1 UNI GY -4.56 1.33 4.03"));
            list.Add(string.Format("1 UNI GY -6.18 0.32 3.02"));
            list.Add(string.Format("1 UNI GY -9.73 0 2.25"));
            list.Add(string.Format("LOAD 117 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 3.285 5.215"));
            list.Add(string.Format("1 UNI GY -3.95 2.33 5.03"));
            list.Add(string.Format("1 UNI GY -4.35 1.58 4.28"));
            list.Add(string.Format("1 UNI GY -5.61 0.57 3.27"));
            list.Add(string.Format("1 UNI GY -8.09 0 2.5"));
            list.Add(string.Format("1 UNI GY -18.05 0 1.74"));
            list.Add(string.Format("LOAD 118 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 3.535 5.465"));
            list.Add(string.Format("1 UNI GY -3.88 2.58 5.28"));
            list.Add(string.Format("1 UNI GY -4.18 1.83 4.53"));
            list.Add(string.Format("1 UNI GY -5.17 0.82 3.52"));
            list.Add(string.Format("1 UNI GY -6.99 0.05 2.75"));
            list.Add(string.Format("1 UNI GY -12.61 0 1.99"));
            list.Add(string.Format("LOAD 119 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.29 3.785 5.715"));
            list.Add(string.Format("1 UNI GY -3.84 2.83 5.53"));
            list.Add(string.Format("1 UNI GY -4.06 2.08 4.78"));
            list.Add(string.Format("1 UNI GY -4.83 1.07 3.77"));
            list.Add(string.Format("1 UNI GY -6.21 0.3 3"));
            list.Add(string.Format("1 UNI GY -9.83 0 2.24"));
            list.Add(string.Format("LOAD 120 LOADTYPE Dead  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.31 4.035 5.965"));
            list.Add(string.Format("1 UNI GY -3.81 3.08 5.78"));
            list.Add(string.Format("1 UNI GY -3.96 2.33 5.03"));
            list.Add(string.Format("1 UNI GY -4.57 1.32 4.02"));
            list.Add(string.Format("1 UNI GY -5.63 0.55 3.25"));
            list.Add(string.Format("1 UNI GY -8.15 0 2.49"));
            list.Add(string.Format("1 UNI GY -11.03 0 1.48"));
            list.Add(string.Format("LOAD 121 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.29 4.285 6.215"));
            list.Add(string.Format("1 UNI GY -3.81 3.33 6.03"));
            list.Add(string.Format("1 UNI GY -3.89 2.58 5.28"));
            list.Add(string.Format("1 UNI GY -4.36 1.57 4.27"));
            list.Add(string.Format("1 UNI GY -5.19 0.8 3.5"));
            list.Add(string.Format("1 UNI GY -7.03 0.04 2.74"));
            list.Add(string.Format("1 UNI GY -7.65 0 1.73"));
            list.Add(string.Format("LOAD 122 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 4.535 6.465"));
            list.Add(string.Format("1 UNI GY -3.83 3.58 6.28"));
            list.Add(string.Format("1 UNI GY -3.84 2.83 5.53"));
            list.Add(string.Format("1 UNI GY -4.19 1.82 4.52"));
            list.Add(string.Format("1 UNI GY -4.85 1.05 3.75"));
            list.Add(string.Format("1 UNI GY -6.24 0.29 2.99"));
            list.Add(string.Format("1 UNI GY -5.94 0 1.98"));
            list.Add(string.Format("LOAD 123 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 4.785 6.715"));
            list.Add(string.Format("1 UNI GY -3.87 3.83 6.53"));
            list.Add(string.Format("1 UNI GY -3.81 3.08 5.78"));
            list.Add(string.Format("1 UNI GY -4.06 2.07 4.77"));
            list.Add(string.Format("1 UNI GY -4.58 1.3 4"));
            list.Add(string.Format("1 UNI GY -5.66 0.54 3.24"));
            list.Add(string.Format("1 UNI GY -4.91 0 2.23"));
            list.Add(string.Format("LOAD 124 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.29 5.035 6.965"));
            list.Add(string.Format("1 UNI GY -3.83 4.08 6.78"));
            list.Add(string.Format("1 UNI GY -3.81 3.33 6.03"));
            list.Add(string.Format("1 UNI GY -3.96 2.32 5.02"));
            list.Add(string.Format("1 UNI GY -4.37 1.55 4.25"));
            list.Add(string.Format("1 UNI GY -5.21 0.79 3.49"));
            list.Add(string.Format("1 UNI GY -4.23 0 2.48"));
            list.Add(string.Format("LOAD 125 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.32 5.285 7.215"));
            list.Add(string.Format("1 UNI GY -3.81 4.33 7.03"));
            list.Add(string.Format("1 UNI GY -3.83 3.58 6.28"));
            list.Add(string.Format("1 UNI GY -3.84 2.82 5.52"));
            list.Add(string.Format("1 UNI GY -4.07 2.05 4.75"));
            list.Add(string.Format("1 UNI GY -4.59 1.29 3.99"));
            list.Add(string.Format("1 UNI GY -3.75 0.03 2.73"));
            list.Add(string.Format("LOAD 126 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.36 5.535 7.465"));
            list.Add(string.Format("1 UNI GY -3.81 4.58 7.28"));
            list.Add(string.Format("1 UNI GY -3.86 3.83 6.53"));
            list.Add(string.Format("1 UNI GY -3.81 3.07 5.77"));
            list.Add(string.Format("1 UNI GY -3.97 2.3 5"));
            list.Add(string.Format("1 UNI GY -4.38 1.54 4.24"));
            list.Add(string.Format("1 UNI GY -3.4 0.28 2.98"));
            list.Add(string.Format("LOAD 127 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.42 5.785 7.715"));
            list.Add(string.Format("1 UNI GY -3.83 4.83 7.53"));
            list.Add(string.Format("1 UNI GY -3.83 4.08 6.78"));
            list.Add(string.Format("1 UNI GY -3.81 3.32 6.02"));
            list.Add(string.Format("1 UNI GY -3.89 2.55 5.25"));
            list.Add(string.Format("1 UNI GY -4.21 1.79 4.49"));
            list.Add(string.Format("1 UNI GY -3.13 0.53 3.23"));
            list.Add(string.Format("LOAD 128 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.5 6.035 7.965"));
            list.Add(string.Format("1 UNI GY -3.88 5.08 7.78"));
            list.Add(string.Format("1 UNI GY -3.81 4.33 7.03"));
            list.Add(string.Format("1 UNI GY -3.82 3.57 6.27"));
            list.Add(string.Format("1 UNI GY -3.84 2.8 5.5"));
            list.Add(string.Format("1 UNI GY -4.07 2.04 4.74"));
            list.Add(string.Format("1 UNI GY -2.92 0.78 3.48"));
            list.Add(string.Format("LOAD 129 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.59 6.285 8.215"));
            list.Add(string.Format("1 UNI GY -3.95 5.33 8.03"));
            list.Add(string.Format("1 UNI GY -3.81 4.58 7.28"));
            list.Add(string.Format("1 UNI GY -2.75 1.03 3.73"));
            list.Add(string.Format("LOAD 130 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.71 6.535 8.465"));
            list.Add(string.Format("1 UNI GY -4.04 5.58 8.28"));
            list.Add(string.Format("1 UNI GY -3.83 4.83 7.53"));
            list.Add(string.Format("1 UNI GY -3.86 3.82 6.52"));
            list.Add(string.Format("1 UNI GY -3.81 3.05 5.75"));
            list.Add(string.Format("1 UNI GY -3.97 2.29 4.99"));
            list.Add(string.Format("1 UNI GY -2.62 1.28 3.98"));
            list.Add(string.Format("LOAD 131 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.87 6.785 8.715"));
            list.Add(string.Format("1 UNI GY -4.17 5.83 8.53"));
            list.Add(string.Format("1 UNI GY -3.88 5.08 7.78"));
            list.Add(string.Format("1 UNI GY -2.52 1.53 4.23"));
            list.Add(string.Format("LOAD 132 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.06 7.035 8.965"));
            list.Add(string.Format("1 UNI GY -4.33 6.08 8.78"));
            list.Add(string.Format("1 UNI GY -3.94 5.33 8.03"));
            list.Add(string.Format("1 UNI GY -3.83 4.07 6.77"));
            list.Add(string.Format("1 UNI GY -3.81 3.3 6"));
            list.Add(string.Format("1 UNI GY -3.89 2.54 5.24"));
            list.Add(string.Format("1 UNI GY -2.44 1.78 4.48"));
            list.Add(string.Format("LOAD 133 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.31 7.285 9.215"));
            list.Add(string.Format("1 UNI GY -4.53 6.33 9.03"));
            list.Add(string.Format("1 UNI GY -4.04 5.58 8.28"));
            list.Add(string.Format("1 UNI GY -3.81 4.57 7.27"));
            list.Add(string.Format("1 UNI GY -3.86 3.8 6.5"));
            list.Add(string.Format("1 UNI GY -3.81 3.04 5.74"));
            list.Add(string.Format("1 UNI GY -2.38 2.03 4.73"));
            list.Add(string.Format("LOAD 134 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.64 7.535 9.36"));
            list.Add(string.Format("1 UNI GY -4.78 6.58 9.28"));
            list.Add(string.Format("1 UNI GY -4.16 5.83 8.53"));
            list.Add(string.Format("1 UNI GY -3.83 4.82 7.52"));
            list.Add(string.Format("1 UNI GY -3.83 4.05 6.75"));
            list.Add(string.Format("1 UNI GY -3.81 3.29 5.99"));
            list.Add(string.Format("1 UNI GY -2.33 2.28 4.98"));
            list.Add(string.Format("LOAD 135 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.08 7.785 9.36"));
            list.Add(string.Format("1 UNI GY -5.11 6.83 9.36"));
            list.Add(string.Format("1 UNI GY -4.32 6.08 8.78"));
            list.Add(string.Format("1 UNI GY -3.87 5.07 7.77"));
            list.Add(string.Format("1 UNI GY -3.81 4.3 7"));
            list.Add(string.Format("1 UNI GY -3.82 3.54 6.24"));
            list.Add(string.Format("1 UNI GY -2.3 2.53 5.23"));
            list.Add(string.Format("LOAD 136 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.7 8.035 9.36"));
            list.Add(string.Format("1 UNI GY -5.52 7.08 9.36"));
            list.Add(string.Format("1 UNI GY -4.52 6.33 9.03"));
            list.Add(string.Format("1 UNI GY -3.94 5.32 8.02"));
            list.Add(string.Format("1 UNI GY -3.81 4.55 7.25"));
            list.Add(string.Format("1 UNI GY -3.86 3.79 6.49"));
            list.Add(string.Format("1 UNI GY -2.28 2.78 5.48"));
            list.Add(string.Format("LOAD 137 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.6 8.285 9.36"));
            list.Add(string.Format("1 UNI GY -6.06 7.33 9.36"));
            list.Add(string.Format("1 UNI GY -4.77 6.58 9.28"));
            list.Add(string.Format("1 UNI GY -4.03 5.57 8.27"));
            list.Add(string.Format("1 UNI GY -3.83 4.8 7.5"));
            list.Add(string.Format("1 UNI GY -3.84 4.04 6.74"));
            list.Add(string.Format("1 UNI GY -2.28 3.03 5.73"));
            list.Add(string.Format("LOAD 138 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.07 8.535 9.36"));
            list.Add(string.Format("1 UNI GY -6.79 7.58 9.36"));
            list.Add(string.Format("1 UNI GY -5.09 6.83 9.36"));
            list.Add(string.Format("1 UNI GY -4.15 5.82 8.52"));
            list.Add(string.Format("1 UNI GY -3.87 5.05 7.75"));
            list.Add(string.Format("1 UNI GY -3.81 4.29 6.99"));
            list.Add(string.Format("1 UNI GY -2.29 3.28 5.98"));
            list.Add(string.Format("LOAD 139 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.79 8.785 9.36"));
            list.Add(string.Format("1 UNI GY -7.8 7.83 9.36"));
            list.Add(string.Format("1 UNI GY -5.5 7.08 9.36"));
            list.Add(string.Format("1 UNI GY -4.31 6.07 8.77"));
            list.Add(string.Format("1 UNI GY -3.94 5.3 8"));
            list.Add(string.Format("1 UNI GY -3.81 4.54 7.24"));
            list.Add(string.Format("1 UNI GY -2.31 3.53 6.23"));
            list.Add(string.Format("LOAD 140 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.3 8.08 9.36"));
            list.Add(string.Format("1 UNI GY -6.03 7.33 9.36"));
            list.Add(string.Format("1 UNI GY -4.51 6.32 9.02"));
            list.Add(string.Format("1 UNI GY -4.03 5.55 8.25"));
            list.Add(string.Format("1 UNI GY -3.83 4.79 7.49"));
            list.Add(string.Format("1 UNI GY -2.3 3.78 6.48"));
            list.Add(string.Format("LOAD 141 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.69 8.33 9.36"));
            list.Add(string.Format("1 UNI GY -6.75 7.58 9.36"));
            list.Add(string.Format("1 UNI GY -4.76 6.57 9.27"));
            list.Add(string.Format("1 UNI GY -4.15 5.8 8.5"));
            list.Add(string.Format("1 UNI GY -3.87 5.04 7.74"));
            list.Add(string.Format("1 UNI GY -2.28 4.03 6.73"));
            list.Add(string.Format("LOAD 142 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.12 8.58 9.36"));
            list.Add(string.Format("1 UNI GY -7.75 7.83 9.36"));
            list.Add(string.Format("1 UNI GY -5.07 6.82 9.36"));
            list.Add(string.Format("1 UNI GY -4.3 6.05 8.75"));
            list.Add(string.Format("1 UNI GY -3.93 5.29 7.99"));
            list.Add(string.Format("1 UNI GY -2.28 4.28 6.98"));
            list.Add(string.Format("LOAD 143 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.97 8.83 9.36"));
            list.Add(string.Format("1 UNI GY -9.21 8.08 9.36"));
            list.Add(string.Format("1 UNI GY -5.48 7.07 9.36"));
            list.Add(string.Format("1 UNI GY -4.5 6.3 9"));
            list.Add(string.Format("1 UNI GY -4.02 5.54 8.24"));
            list.Add(string.Format("1 UNI GY -2.29 4.53 7.23"));
            list.Add(string.Format("LOAD 144 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.54 8.33 9.36"));
            list.Add(string.Format("1 UNI GY -6.01 7.32 9.36"));
            list.Add(string.Format("1 UNI GY -4.74 6.55 9.25"));
            list.Add(string.Format("1 UNI GY -4.14 5.79 8.49"));
            list.Add(string.Format("1 UNI GY -2.31 4.78 7.48"));
            list.Add(string.Format("LOAD 145 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.82 8.58 9.36"));
            list.Add(string.Format("1 UNI GY -6.71 7.57 9.36"));
            list.Add(string.Format("1 UNI GY -5.06 6.8 9.36"));
            list.Add(string.Format("1 UNI GY -4.29 6.04 8.74"));
            list.Add(string.Format("1 UNI GY -2.35 5.03 7.73"));
            list.Add(string.Format("LOAD 146 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.1 8.83 9.36"));
            list.Add(string.Format("1 UNI GY -7.69 7.82 9.36"));
            list.Add(string.Format("1 UNI GY -5.46 7.05 9.36"));
            list.Add(string.Format("1 UNI GY -4.49 6.29 8.99"));
            list.Add(string.Format("1 UNI GY -2.4 5.28 7.98"));
            list.Add(string.Format("LOAD 147 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.12 8.07 9.36"));
            list.Add(string.Format("1 UNI GY -5.98 7.3 9.36"));
            list.Add(string.Format("1 UNI GY -4.73 6.54 9.24"));
            list.Add(string.Format("1 UNI GY -2.47 5.53 8.23"));
            list.Add(string.Format("LOAD 148 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.4 8.32 9.36"));
            list.Add(string.Format("1 UNI GY -6.67 7.55 9.36"));
            list.Add(string.Format("1 UNI GY -5.04 6.79 9.36"));
            list.Add(string.Format("1 UNI GY -2.56 5.78 8.48"));
            list.Add(string.Format("LOAD 149 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.54 8.57 9.36"));
            list.Add(string.Format("1 UNI GY -7.63 7.8 9.36"));
            list.Add(string.Format("1 UNI GY -5.43 7.04 9.36"));
            list.Add(string.Format("1 UNI GY -2.68 6.03 8.73"));
            list.Add(string.Format("LOAD 150 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -25.29 8.82 9.36"));
            list.Add(string.Format("1 UNI GY -9.04 8.05 9.36"));
            list.Add(string.Format("1 UNI GY -5.95 7.29 9.36"));
            list.Add(string.Format("1 UNI GY -2.82 6.28 8.98"));
            list.Add(string.Format("LOAD 151 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.26 8.3 9.36"));
            list.Add(string.Format("1 UNI GY -6.63 7.54 9.36"));
            list.Add(string.Format("1 UNI GY -3 6.53 9.23"));
            list.Add(string.Format("LOAD 152 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.26 8.55 9.36"));
            list.Add(string.Format("1 UNI GY -7.58 7.79 9.36"));
            list.Add(string.Format("1 UNI GY -3.24 6.78 9.36"));
            list.Add(string.Format("LOAD 153 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -24.53 8.8 9.36"));
            list.Add(string.Format("1 UNI GY -8.96 8.04 9.36"));
            list.Add(string.Format("1 UNI GY -3.54 7.03 9.36"));
            list.Add(string.Format("LOAD 154 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.13 8.29 9.36"));
            list.Add(string.Format("1 UNI GY -3.94 7.28 9.36"));
            list.Add(string.Format("LOAD 155 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15 8.54 9.36"));
            list.Add(string.Format("1 UNI GY -4.5 7.53 9.36"));
            list.Add(string.Format("LOAD 156 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -23.82 8.79 9.36"));
            list.Add(string.Format("1 UNI GY -5.31 7.78 9.36"));
            list.Add(string.Format("LOAD 157 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.58 8.03 9.36"));
            list.Add(string.Format("LOAD 158 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.82 8.28 9.36"));
            list.Add(string.Format("LOAD 159 LOADTYPE Dead  TITLE 70R TRACK  "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.84 8.53 9.36"));
            list.Add(string.Format("LOAD 201 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.4 0 0.4"));
            list.Add(string.Format("LOAD 202 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.5 0 0.6"));
            list.Add(string.Format("LOAD 203 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.1 0.1 0.9"));
            list.Add(string.Format("LOAD 204 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.9 0.4 1.1"));
            list.Add(string.Format("LOAD 205 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.1 0.6 1.4"));
            list.Add(string.Format("LOAD 206 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.6 0.9 1.6"));
            list.Add(string.Format("1 UNI GY -13.3 0 0.4"));
            list.Add(string.Format("LOAD 207 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.2 1.1 1.9"));
            list.Add(string.Format("1 UNI GY -8.1 0 0.7"));
            list.Add(string.Format("LOAD 208 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.9 1.4 2.1"));
            list.Add(string.Format("1 UNI GY -6 0.1 0.9"));
            list.Add(string.Format("LOAD 209 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.7 1.6 2.4"));
            list.Add(string.Format("1 UNI GY -4.8 0.4 1.2"));
            list.Add(string.Format("LOAD 210 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.5 1.9 2.6"));
            list.Add(string.Format("1 UNI GY -4 0.6 1.4"));
            list.Add(string.Format("LOAD 211 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.4 2.1 2.9"));
            list.Add(string.Format("1 UNI GY -3.5 0.9 1.7"));
            list.Add(string.Format("LOAD 212 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.3 2.4 3.1"));
            list.Add(string.Format("1 UNI GY -3.2 1.1 1.9"));
            list.Add(string.Format("LOAD 213 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.2 2.6 3.4"));
            list.Add(string.Format("1 UNI GY -2.9 1.4 2.2"));
            list.Add(string.Format("LOAD 214 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.2 2.9 3.6"));
            list.Add(string.Format("1 UNI GY -2.7 1.6 2.4"));
            list.Add(string.Format("LOAD 215 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 3.1 3.9"));
            list.Add(string.Format("1 UNI GY -2.5 1.9 2.7"));
            list.Add(string.Format("LOAD 216 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 3.4 4.1"));
            list.Add(string.Format("1 UNI GY -2.4 2.1 2.9"));
            list.Add(string.Format("LOAD 217 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 3.6 4.4"));
            list.Add(string.Format("1 UNI GY -2.3 2.4 3.2"));
            list.Add(string.Format("LOAD 218 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 3.9 4.6"));
            list.Add(string.Format("1 UNI GY -2.2 2.6 3.4"));
            list.Add(string.Format("LOAD 219 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 4.1 4.9"));
            list.Add(string.Format("1 UNI GY -2.2 2.9 3.7"));
            list.Add(string.Format("LOAD 220 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 4.4 5.1"));
            list.Add(string.Format("1 UNI GY -2.1 3.1 3.9"));
            list.Add(string.Format("LOAD 221 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 4.6 5.4"));
            list.Add(string.Format("1 UNI GY -2.1 3.4 4.2"));
            list.Add(string.Format("LOAD 222 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 4.9 5.6"));
            list.Add(string.Format("1 UNI GY -2.1 3.6 4.4"));
            list.Add(string.Format("LOAD 223 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 5.1 5.9"));
            list.Add(string.Format("1 UNI GY -2.1 3.9 4.7"));
            list.Add(string.Format("LOAD 224 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 5.4 6.1"));
            list.Add(string.Format("1 UNI GY -2.1 4.1 4.9"));
            list.Add(string.Format("LOAD 225 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 5.6 6.4"));
            list.Add(string.Format("1 UNI GY -2.1 4.4 5.2"));
            list.Add(string.Format("LOAD 226 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 5.9 6.6"));
            list.Add(string.Format("1 UNI GY -2.1 4.6 5.4"));
            list.Add(string.Format("LOAD 227 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.1 6.1 6.9"));
            list.Add(string.Format("1 UNI GY -2.1 4.9 5.7"));
            list.Add(string.Format("LOAD 228 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.2 6.4 7.1"));
            list.Add(string.Format("1 UNI GY -2.1 5.1 5.9"));
            list.Add(string.Format("LOAD 229 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.2 6.6 7.4"));
            list.Add(string.Format("1 UNI GY -2.1 5.4 6.2"));
            list.Add(string.Format("LOAD 230 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.3 6.9 7.6"));
            list.Add(string.Format("1 UNI GY -2.1 5.6 6.4"));
            list.Add(string.Format("LOAD 231 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.4 7.1 7.9"));
            list.Add(string.Format("1 UNI GY -2.1 5.9 6.7"));
            list.Add(string.Format("LOAD 232 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.5 7.4 8.1"));
            list.Add(string.Format("1 UNI GY -2.1 6.1 6.9"));
            list.Add(string.Format("LOAD 233 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.7 7.6 8.4"));
            list.Add(string.Format("1 UNI GY -2.2 6.4 7.2"));
            list.Add(string.Format("LOAD 234 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.9 7.9 8.6"));
            list.Add(string.Format("1 UNI GY -2.2 6.6 7.4"));
            list.Add(string.Format("LOAD 235 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.2 8.1 8.9"));
            list.Add(string.Format("1 UNI GY -2.3 6.9 7.7"));
            list.Add(string.Format("LOAD 236 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.6 8.4 9.1"));
            list.Add(string.Format("1 UNI GY -2.4 7.1 7.9"));
            list.Add(string.Format("LOAD 237 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.1 8.6 9.4"));
            list.Add(string.Format("1 UNI GY -2.5 7.4 8.2"));
            list.Add(string.Format("LOAD 238 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.9 8.9 9.36"));
            list.Add(string.Format("1 UNI GY -2.7 7.6 8.4"));
            list.Add(string.Format("LOAD 239 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.9 7.9 8.7"));
            list.Add(string.Format("LOAD 240 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.2 8.1 8.9"));
            list.Add(string.Format("LOAD 241 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -3.6 8.4 9.2"));
            list.Add(string.Format("LOAD 242 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.2 8.6 9.4"));
            list.Add(string.Format("LOAD 243 LIVE TITLE 70R BOGGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5 8.9 9.4"));
            list.Add(string.Format("PERFORM ANALYSIS PRINT ALL"));
            list.Add(string.Format("FINISH"));
         
            File.WriteAllLines(Input_File_LL, list.ToArray());
        }
        public string Excel_File()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            if (Is_Multi_Cell)
                file_path = Path.Combine(file_path, "Box Culvert Multi Cell LSM.xlsx");
            else
                file_path = Path.Combine(file_path, "Box Culvert Single Cell LSM.xlsx");

            return file_path;
        }
        private void btn_single_cell_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_process_design)
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
            else if (btn == btn_create_data)
            {
                Create_Data();
            }
            else if (btn == btn_process_data)
            {
                Process_Data();
            }
            else if (btn == btn_open_design)
            {
                string file_path = Excel_File();
                if (File.Exists(file_path)) iApp.OpenExcelFile(file_path, "2011ap");
                else
                {
                    iApp.Open_ASTRA_Worksheet_Dialog();
                }
            }
            else if (btn == btn_DL_input)
            {
                if (File.Exists(Input_File_DL))
                {
                    iApp.Form_ASTRA_TEXT_Data(Input_File_DL, false).Show();
                    //System.Diagnostics.Process.Start(Input_File_DL);
                }
            }
            else if (btn == btn_DL_report)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(Input_File_DL))) System.Diagnostics.Process.Start(MyList.Get_Analysis_Report_File(Input_File_DL));
            }
            else if (btn == btn_open_result_summary)
            {
                if (File.Exists(Result_File)) System.Diagnostics.Process.Start(Result_File);
            }
            else if (btn == btn_LL_input)
            {
                if (File.Exists(Input_File_LL))
                {
                    iApp.Form_ASTRA_TEXT_Data(Input_File_LL, false).Show();
                    //System.Diagnostics.Process.Start(Input_File_LL);
                }
            }
            else if (btn == btn_LL_report)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(Input_File_LL))) System.Diagnostics.Process.Start(MyList.Get_Analysis_Report_File(Input_File_LL));
            }
            Button_Enable_Disable();

            if(OnButtonProceed != null)
            {
                OnButtonProceed(sender, e);
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
        string Input_File_DL = "";
        string Input_File_LL = "";


        string Result_File
        {
            get
            {
                if (rbtn_multi.Checked) return Path.Combine(iApp.user_path, "Multiple_Cell_Result_Summary.txt");
                return Path.Combine(iApp.user_path, "Single_Cell_Result_Summary.txt");
            }
        }

        private void rbtn_single_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_multi.Checked)
            {
                sc1.Panel1Collapsed = true;
                sc1.Panel2Collapsed = false;
                if(MultiCell_Box_Model!= null)
                {
                    rtb_load_data.Lines = MultiCell_Box_Model.DL_Load_Data.ToArray();
                }
                else
                {
                    rtb_load_data.Text = "";
                }
            }
            else
            {
                sc1.Panel1Collapsed = false;
                sc1.Panel2Collapsed = true;
                if (SingleCell_Box_Model != null)
                {
                    rtb_load_data.Lines = SingleCell_Box_Model.DL_Load_Data.ToArray();
                }
                else
                {
                    rtb_load_data.Text = "";
                }
            }

            if(File.Exists(Result_File))
            {
                rtb_results.Lines = File.ReadAllLines(Result_File);
            }
            else
            {
                rtb_results.Text = "";
            }

            Button_Enable_Disable();
            this.Refresh();
        }
        public void Create_Data()
        {

            string Working_Folder = "";

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            Working_Folder = file_path;

            string pd = "";
            if (Directory.Exists(Working_Folder))
            {
                if (rbtn_multi.Checked)
                {
                    pd = Path.Combine(Working_Folder, "Multicell Box Culvert Dead Load");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                    Input_File_DL = Path.Combine(pd, "Multicell_DL_Input_File.txt");

                    pd = Path.Combine(Working_Folder, "Multicell Box Culvert Live Load");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                    Input_File_LL = Path.Combine(pd, "Multicell_LL_Input_File.txt");
                }
                else
                {
                    pd = Path.Combine(Working_Folder, "Single cell Box Culvert Dead Load");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                    Input_File_DL = Path.Combine(pd, "Single_DL_Input_File.txt");

                    pd = Path.Combine(Working_Folder, "Single cell Box Culvert Live Load");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                    Input_File_LL = Path.Combine(pd, "Single_LL_Input_File.txt");
                }
            }

            if (Is_Multi_Cell)
            {
                //Create_Data_Multicell_DeadLoad();
                //Create_Data_MulticellLiveLoad();

                MultiCell_Box_Model = new Box_Culvert_Model();
                DataGridView dgv = dgv_design_data_multi;

                //box.No_of_Cells = dgv_design_data_multi

                DataInputCollections dips = new DataInputCollections();

                dips.Load_Data_from_Grid(dgv);

                MultiCell_Box_Model.No_of_Cells = dips[0].ToInt;

                MultiCell_Box_Model.Width = dips[1].ToDouble;
                MultiCell_Box_Model.Height = dips[3].ToDouble;

                MultiCell_Box_Model.Top_slab_thickness = dips[23].ToDouble;
                MultiCell_Box_Model.Bottom_slab_thickness = dips[24].ToDouble;
                MultiCell_Box_Model.Side_wall_thickness = dips[25].ToDouble;
                MultiCell_Box_Model.Mid_wall_thickness = dips[26].ToDouble;

                MultiCell_Box_Model.Create_Data_Multicell();
                MultiCell_Box_Model.Write_Data(Input_File_DL, false);
                MultiCell_Box_Model.Write_Data(Input_File_LL, true);

                rtb_load_data.Lines = MultiCell_Box_Model.DL_Load_Data.ToArray();
                //string fl_path = Path.Combine(iApp.user_path, "Analysis_Input_Data.TXT");
                //File.WriteAllLines(Input_File_DL, MultiCell_Box_Model.Input_Data.ToArray());
                //File.WriteAllLines(Input_File_LL, MultiCell_Box_Model.Input_Data.ToArray());
            }
            else
            {
                //Create_Data_Singlecell_DeadLoad();
                //Create_Data_Singlecell_LiveLoad();


                SingleCell_Box_Model = new Box_Culvert_Model();
                DataGridView dgv = dgv_design_data_single;

                //box.No_of_Cells = dgv_design_data_multi

                DataInputCollections dips = new DataInputCollections();

                dips.Load_Data_from_Grid(dgv);


                SingleCell_Box_Model.No_of_Cells = dips[0].ToInt;

                SingleCell_Box_Model.Width = dips[1].ToDouble;
                SingleCell_Box_Model.Height = dips[2].ToDouble;


                SingleCell_Box_Model.Top_slab_thickness = dips[14].ToDouble;
                SingleCell_Box_Model.Bottom_slab_thickness = dips[15].ToDouble;
                SingleCell_Box_Model.Side_wall_thickness = dips[16].ToDouble;


                SingleCell_Box_Model.Create_Data_Singlecell();
                SingleCell_Box_Model.Write_Data(Input_File_DL, false);
                SingleCell_Box_Model.Write_Data(Input_File_LL, true);
                rtb_load_data.Lines = SingleCell_Box_Model.DL_Load_Data.ToArray();

                //string fl_path = Path.Combine(iApp.user_path, "Analysis_Input_Data.TXT");
                //File.WriteAllLines(Input_File_DL, SingleCell_Box_Model.Input_Data.ToArray());
                //File.WriteAllLines(Input_File_LL, SingleCell_Box_Model.Input_Data.ToArray());
            }
            MessageBox.Show(this, "Analysis Input Data files are created as \n\n" + Input_File_DL + "\n\n and \n\n" + Input_File_LL, "ASTRA", MessageBoxButtons.OK);
        }
        public void Process_Data()
        {
            try
            {
                //DECKSLAB_LL_TXT();
                #region Process
                int i = 1;
                //Write_All_Data(true);

                string flPath = Input_File_DL;

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                do
                {
                    if (i == 1)
                    {
                        flPath = Input_File_DL;
                    }
                    else if (i == 2)
                    {
                        flPath = Input_File_LL;
                    }
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    i++;
                }
                while (i <= 2);

                //frm_LS_Process ff = new frm_LS_Process(pcol);
                //ff.Owner = this;
                ////ff.ShowDialog();
                //if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                //if (!iApp.Show_and_Run_Process_List(pcol)) return;


                //while (i < 3) ;

                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                //string ana_rep_file = Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File);

                if (iApp.Show_and_Run_Process_List(pcol))
                {
                    iApp.Progress_Works.Clear();


                    iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");
                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");

                    //Deck_Analysis.LiveLoad_1_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File));

                    //Deck_Analysis.LiveLoad_2_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_2_Input_File));

                    //Deck_Analysis.LiveLoad_3_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_3_Input_File));

                    //Deck_Analysis.LiveLoad_4_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_4_Input_File));

                    //Deck_Analysis.LiveLoad_5_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_5_Input_File));

                    //Deck_Analysis.LiveLoad_6_Analysis = new BridgeMemberAnalysis(iApp,
                    //    Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_6_Input_File));

                    //Deck_Analysis.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis.DeadLoad_Analysis_Report);

                    if (!iApp.Is_Progress_Cancel)
                    {                        
                        //Show_Deckslab_Moment_Shear();

                    }
                    else
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;
                    }
                }
                if (rbtn_multi.Checked)
                {
                    Read_Multicell_Bending_Moment_Shear_Force();
                }
                else
                {
                    Read_Singlecell_Bending_Moment_Shear_Force();
                }
                ////grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                ////grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

                //Button_Enable_Disable();
                //Write_All_Data(false);
                iApp.Progress_Works.Clear();
                Button_Enable_Disable();

                #endregion Process
                //Write_All_Data(false);
            }
            catch (Exception ex) { }
        }

        private void Read_Multicell_Bending_Moment_Shear_Force()
        {
            BridgeMemberAnalysis brd = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_DL));
            BridgeMemberAnalysis brd1 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_LL));

            List<int> top_arr = new List<int>();
            int c = 0;
            //for (int i = 0; i < 11; i++)
            //{
            //    top_arr.Add(Box_Model.jnTop[i].NodeNo);
            //}
            List<double> lst_frcs = new List<double>();

            MaxForce mf;


            List<List<double>> All_Frcs = new List<List<double>>();


            List<string> list = new List<string>();

            #region Top Slab

            #region Top Slab 1 Bending Moment

            List<double> dst = MultiCell_Box_Model.list_Top_X;

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[i].NodeNo);
                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);

                All_Frcs.Add(lst_frcs);
            }

            list.Add("");
            list.Add("");
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 1 : Summary of Bending Moments  for Unfactored Loads (Top Slab 1)"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1
            list.Add("");
            list.Add("");

            #region Top Slab 2  Bending Moment


            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[10 + i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[10 + i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[10 + i].NodeNo);
                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);



                All_Frcs.Add(lst_frcs);
            }



            dst = MultiCell_Box_Model.list_Top_X;

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 2 : Summary of Bending Moments  for Unfactored Loads (Top Slab 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 2
            list.Add("");
            list.Add("");

            #region Top Slab 1 Shear Force
            All_Frcs.Clear();


            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 3 : Summary of Shear Force  for Unfactored Loads (Top Slab 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1
            list.Add("");
            list.Add("");

            #region Top Slab 2 Shear Force


            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[10 + i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }



                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnTop[10 + i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                lst_frcs.Add(sf.MaxShearForce);
                 
                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                lst_frcs.Add(sf.MaxShearForce);



                All_Frcs.Add(lst_frcs);
            }




            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 4 : Summary of Shear Force  for Unfactored Loads (Top Slab 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 2
            #endregion Top Slab

            #region Side Wall

            List<int> mm = MultiCell_Box_Model.ht_side_mems[0] as List<int>;

            //mm = Box_Model.ht_mid_mems[0] as List<int>;

            List<int> sd = new List<int>();

            for (int i = 0; i < mm.Count; i++)
            {
                var mb = brd.Analysis.Members.GetMember(mm[i]);
                if (!sd.Contains(mb.StartNode.NodeNo)) sd.Add(mb.StartNode.NodeNo);
                if (!sd.Contains(mb.EndNode.NodeNo)) sd.Add(mb.EndNode.NodeNo);
            }

            mm = MultiCell_Box_Model.ht_side_mems[MultiCell_Box_Model.No_of_Cells] as List<int>;

            //mm = Box_Model.ht_mid_mems[0] as List<int>;

            List<int> sd2 = new List<int>();

            for (int i = 0; i < mm.Count; i++)
            {
                var mb = brd.Analysis.Members.GetMember(mm[i]);
                if (!sd2.Contains(mb.StartNode.NodeNo)) sd2.Add(mb.StartNode.NodeNo);
                if (!sd2.Contains(mb.EndNode.NodeNo)) sd2.Add(mb.EndNode.NodeNo);
            }


            #region Side Wall
            list.Add("");
            list.Add("");

            #region Side Wall 1 Bending Moment

            dst = MultiCell_Box_Model.list_Y;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd[i]);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }



                top_arr.Clear();
                top_arr.Add(sd[i]);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);


                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);



                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 5 : Summary of Bending Moments  for Unfactored Loads (Side Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Side Wall 1

            list.Add("");
            list.Add("");

            #region Side Wall 2  Bending Moment




            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd2[i]);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(sd2[i]);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);


                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 6 : Summary of Bending Moments  for Unfactored Loads (Side Wall 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));

            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Side Wall 2

            list.Add("");
            list.Add("");

            #region Side Wall 1 Shear Force
            All_Frcs.Clear();


            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd[i]);

                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(sd[i]);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);



                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 7 : Summary of Shear Force  for Unfactored Loads (Side Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1

            list.Add("");
            list.Add("");

            #region Side Wall 2 Shear Force


            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd2[i]);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(sd2[i]);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);



                All_Frcs.Add(lst_frcs);
            }





            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 8 : Summary of Shear Force  for Unfactored Loads (Side Wall 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 2

            #endregion Side Wall



            #endregion Side Wall


            #region Inner Wall

            mm = MultiCell_Box_Model.ht_mid_mems[1] as List<int>;

            //mm = Box_Model.ht_mid_mems[0] as List<int>;

            List<int> inner = new List<int>();

            for (int i = 0; i < mm.Count; i++)
            {
                var mb = brd.Analysis.Members.GetMember(mm[i]);
                if (!inner.Contains(mb.StartNode.NodeNo)) inner.Add(mb.StartNode.NodeNo);
                if (!inner.Contains(mb.EndNode.NodeNo)) inner.Add(mb.EndNode.NodeNo);
            }

            mm = MultiCell_Box_Model.ht_side_mems[MultiCell_Box_Model.No_of_Cells] as List<int>;

            #region Inner Wall 1
            list.Add("");
            list.Add("");

            #region Inner Wall 1 Bending Moment

            dst = MultiCell_Box_Model.list_Y;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(inner[i]);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(inner[i]);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);


                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);


                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 9 :  Summary of Bending Moments  for Unfactored Loads (Inner Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Side Wall 1
            list.Add("");
            list.Add("");

            #region Inner Wall 1 Shear Force
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(inner[i]);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(inner[i]);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 10 :  Summary of Shear Force  for Unfactored Loads (Inner Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1

            #endregion Inner Wall 1

            #endregion Inner Wall

            #region Bottom Slab
            list.Add("");
            list.Add("");

            #region Bottom Slab 1 Bending Moment

            dst = MultiCell_Box_Model.list_Bottom_X;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnBots[i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnBots[i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);


                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 11 : Summary of Bending Moments  for Unfactored Loads (Bottom Slab 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
 
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 1

            list.Add("");
            list.Add("");

            #region Bottom Slab 2  Bending Moment


            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[dst.Count - 1 + i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnBots[dst.Count - 1 + i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);


                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 12 : Summary of Bending Moments  for Unfactored Loads (Bottom Slab 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 2

            list.Add("");
            list.Add("");

            #region Bottom Slab 1 Shear Force

            All_Frcs.Clear();


            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnBots[i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnBots[i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 13 : Summary of Shear Force  for Unfactored Loads (Bottom Slab 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 2

            list.Add("");
            list.Add("");

            #region Bottom Slab 2 Shear Force


            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(MultiCell_Box_Model.jnTop[dst.Count - 1 + i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(MultiCell_Box_Model.jnBots[dst.Count - 1 + i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 14 : Summary of Shear Force  for Unfactored Loads (Bottom Slab 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));

            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 2
            #endregion Bottom Slab


            rtb_results.Lines = list.ToArray();
            File.WriteAllLines(Result_File, list.ToArray());
            System.Diagnostics.Process.Start(Result_File);
        }

        private void Read_Singlecell_Bending_Moment_Shear_Force()
        {
            BridgeMemberAnalysis brd = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_DL));
            BridgeMemberAnalysis brd1 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_LL));

            List<int> top_arr = new List<int>();
            int c = 0;
            //for (int i = 0; i < 11; i++)
            //{
            //    top_arr.Add(Box_Model.jnTop[i].NodeNo);
            //}
            List<double> lst_frcs = new List<double>();

            MaxForce mf;


            List<List<double>> All_Frcs = new List<List<double>>();


            List<string> list = new List<string>();

            #region Top Slab

            #region Top Slab 1 Bending Moment

            List<double> dst = SingleCell_Box_Model.list_Top_X;

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(SingleCell_Box_Model.jnTop[i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(SingleCell_Box_Model.jnTop[i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                top_arr.Clear();
                top_arr.Add(SingleCell_Box_Model.jnTop[i].NodeNo);
                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);


                All_Frcs.Add(lst_frcs);
            }

            list.Add("");
            list.Add("");
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 1 : Summary of Bending Moments  for Unfactored Loads (Top Slab)"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1
            list.Add("");
            list.Add("");

            #region Top Slab 1 Shear Force
            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(SingleCell_Box_Model.jnTop[i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(SingleCell_Box_Model.jnTop[i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 2 : Summary of Shear Force  for Unfactored Loads (Top Slab)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1
            list.Add("");
            list.Add("");

            #endregion Top Slab

            #region Side Wall

            List<int> mm = SingleCell_Box_Model.ht_side_mems[0] as List<int>;

            //mm = Box_Model.ht_mid_mems[0] as List<int>;

            List<int> sd = new List<int>();

            for (int i = 0; i < mm.Count; i++)
            {
                var mb = brd.Analysis.Members.GetMember(mm[i]);
                if (!sd.Contains(mb.StartNode.NodeNo)) sd.Add(mb.StartNode.NodeNo);
                if (!sd.Contains(mb.EndNode.NodeNo)) sd.Add(mb.EndNode.NodeNo);
            }

            mm = SingleCell_Box_Model.ht_side_mems[SingleCell_Box_Model.No_of_Cells] as List<int>;

            //mm = Box_Model.ht_mid_mems[0] as List<int>;

            List<int> sd2 = new List<int>();

            for (int i = 0; i < mm.Count; i++)
            {
                var mb = brd.Analysis.Members.GetMember(mm[i]);
                if (!sd2.Contains(mb.StartNode.NodeNo)) sd2.Add(mb.StartNode.NodeNo);
                if (!sd2.Contains(mb.EndNode.NodeNo)) sd2.Add(mb.EndNode.NodeNo);
            }

            #region Side Wall
            list.Add("");
            list.Add("");

            #region Side Wall 1 Bending Moment

            dst = SingleCell_Box_Model.list_Y;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd[i]);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }



                top_arr.Clear();
                top_arr.Add(sd[i]);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);


                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);



                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 3 : Summary of Bending Moments  for Unfactored Loads (Side Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Side Wall 1

            list.Add("");
            list.Add("");

            #region Side Wall 2  Bending Moment




            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd2[i]);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(sd2[i]);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);


                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 4 : Summary of Bending Moments  for Unfactored Loads (Side Wall 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));

            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Side Wall 2

            list.Add("");
            list.Add("");

            #region Side Wall 1 Shear Force
            All_Frcs.Clear();


            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd[i]);

                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(sd[i]);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);



                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 5 : Summary of Shear Force  for Unfactored Loads (Side Wall 1)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 1

            list.Add("");
            list.Add("");

            #region Side Wall 2 Shear Force


            All_Frcs.Clear();

            for (int i = 0; i < 11; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(sd2[i]);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(sd2[i]);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);



                All_Frcs.Add(lst_frcs);
            }





            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 6 : Summary of Shear Force  for Unfactored Loads (Side Wall 2)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Top Slab 2

            #endregion Side Wall



            #endregion Side Wall



            #region Bottom Slab
            list.Add("");
            list.Add("");

            #region Bottom Slab 1 Bending Moment

            dst = SingleCell_Box_Model.list_Bottom_X;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(SingleCell_Box_Model.jnBots[i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                top_arr.Clear();
                top_arr.Add(SingleCell_Box_Model.jnBots[i].NodeNo);
                mf = brd1.GetJoint_Max_Hogging(top_arr, true);
                lst_frcs.Add(mf.Force);

                mf = brd1.GetJoint_Max_Sagging(top_arr, true);
                lst_frcs.Add(mf.Force);


                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 7 : Summary of Bending Moments  for Unfactored Loads (Bottom Slab)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp        Live Load               "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load       Load      Cushion     load       Rise        Fall      Bending Moment "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                Hogging     Sagging   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));

            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 1

            list.Add("");
            list.Add("");


            #region Bottom Slab 1 Shear Force

            All_Frcs.Clear();


            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 11; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(SingleCell_Box_Model.jnBots[i].NodeNo);
                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }


                top_arr.Clear();
                top_arr.Add(SingleCell_Box_Model.jnBots[i].NodeNo);
                var sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, true);
                lst_frcs.Add(sf.MaxShearForce);

                sf = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, false, false);
                lst_frcs.Add(sf.MaxShearForce);

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 8 : Summary of Shear Force  for Unfactored Loads (Bottom Slab)"));

            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Distance   Dead Load   SIDL (w.c)  Live Load   Earth      Earth      Braking    Braking    Earth       Water      Temp        Temp         Live Load "));
            list.Add(string.Format("              +                    Surcharge   Pressure   Pressure    Load        Load     Cushion     load       Rise        Fall        Shear Force "));
            list.Add(string.Format("           SIDL(w/o)               Load        Both side  one side    (LHS)      (RHS)                                                 Maximum    Minimum   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------- "));


            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                   , itm[6]
                   , itm[7]
                   , itm[8]
                   , itm[9]
                   , itm[10]
                   , itm[11]
                   , itm[12]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------"));

            #endregion Bottom Slab 2

            list.Add("");
            list.Add("");

            #endregion Bottom Slab




            rtb_results.Lines = list.ToArray();
            File.WriteAllLines(Result_File, list.ToArray());
            System.Diagnostics.Process.Start(Result_File);
        }

        public void Button_Enable_Disable()
        {
            if (iApp == null) return;
            string Working_Folder = "";


            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            Working_Folder = file_path;

            string pd = "";
            if (Directory.Exists(Working_Folder))
            {
                if (rbtn_multi.Checked)
                {
                    pd = Path.Combine(Working_Folder, "Multicell Box Culvert Dead Load");
                    Input_File_DL = Path.Combine(pd, "Multicell_DL_Input_File.txt");

                    pd = Path.Combine(Working_Folder, "Multicell Box Culvert Live Load");
                    Input_File_LL = Path.Combine(pd, "Multicell_LL_Input_File.txt");
                }
                else
                {
                    pd = Path.Combine(Working_Folder, "Single cell Box Culvert Dead Load");
                    Input_File_DL = Path.Combine(pd, "Single_DL_Input_File.txt");

                    pd = Path.Combine(Working_Folder, "Single cell Box Culvert Live Load");
                    Input_File_LL = Path.Combine(pd, "Single_LL_Input_File.txt");
                }
            }

            btn_create_data.Enabled = Directory.Exists(Working_Folder);
            btn_process_data.Enabled = File.Exists(Input_File_DL);
            btn_DL_input.Enabled = File.Exists(Input_File_DL);
            btn_LL_input.Enabled = File.Exists(Input_File_LL);
            btn_DL_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Input_File_DL));
            btn_LL_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Input_File_LL));

            //btn_process_design.Enabled = (btn_DL_report.Enabled && btn_LL_report.Enabled);
            //btn_process_design.Enabled = Directory.Exists(Working_Folder);
            //btn_.Enabled = File.Exists(Input_File_DL);
        }

        private void btn_save_load_data_Click(object sender, EventArgs e)
        {
            if (rbtn_multi.Checked)
            {
                MultiCell_Box_Model.DL_Load_Data = new List<string>(rtb_load_data.Lines);
                MultiCell_Box_Model.Write_Data(Input_File_DL, false);
                MultiCell_Box_Model.Write_Data(Input_File_LL, true);
            }
            else
            {
                SingleCell_Box_Model.DL_Load_Data = new List<string>(rtb_load_data.Lines);
                SingleCell_Box_Model.Write_Data(Input_File_DL, false);
                SingleCell_Box_Model.Write_Data(Input_File_LL, true);
            }
        }


    }

    public class Box_Culvert_Model
    {
        public int No_of_Cells { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top_slab_thickness { get; set; }
        public double Bottom_slab_thickness { get; set; }
        public double Side_wall_thickness { get; set; }
        public double Mid_wall_thickness { get; set; }

        public List<string> Input_Data { get; set; }

        public Hashtable ht_top_mems { get; set; }
        public Hashtable ht_bot_mems { get; set; }
        public Hashtable ht_side_mems { get; set; }
        public Hashtable ht_mid_mems { get; set; }


        public JointNodeCollection jnBots { get; set; }
        public JointNodeCollection jnSide { get; set; }
        public JointNodeCollection jnTop { get; set; }


        public double LL_70R_Track { get; set; }
        public double LL_70R_Wheel { get; set; }
        public double LL_Class_A { get; set; }
        public double LL_Boggie { get; set; }

        public Box_Culvert_Model()
        {
            No_of_Cells = 2;
            Width = 8.0;
            Height = 7.0;

            Top_slab_thickness = 0.80;
            Bottom_slab_thickness = 0.90;
            Side_wall_thickness = 0.80;
            Mid_wall_thickness = 0.70;

            ht_top_mems = new Hashtable();
            ht_bot_mems = new Hashtable();
            ht_side_mems = new Hashtable();
            ht_mid_mems = new Hashtable();


            jnBots = new JointNodeCollection();
            jnSide = new JointNodeCollection();
            jnTop = new JointNodeCollection();

            LL_70R_Track = 700;
            LL_70R_Wheel = 700;
            LL_Class_A = 554;
            LL_Boggie = 40;

        }


        public List<double> list_Top_X { get; set; }
        public List<double> list_Bottom_X { get; set; }
        public List<double> list_Y { get; set; }

        JointNodeCollection All_Joints { get; set; }

        MemberCollection mbrCols { get; set; }

        public void Create_Data_Multicell()
        {
            double org_bot = 12.60;
            double org_side = 5.07;
            double org_top = 12.75;

            #region List Bottom
            List<double> list_bottom = new List<double>();
            //list_bottom(Bottom Slab);
            list_bottom.Add(0.00);
            list_bottom.Add(6.30);
            list_bottom.Add(12.60);

            #endregion List Bottom

            #region List Side

            List<double> list_side = new List<double>();
            list_side.Add(0.00);
            list_side.Add(0.51);
            list_side.Add(1.01);
            list_side.Add(1.52);
            list_side.Add(2.03);
            list_side.Add(2.54);
            list_side.Add(3.04);
            list_side.Add(3.55);
            list_side.Add(4.06);
            list_side.Add(4.56);
            list_side.Add(5.07);

            #endregion List Side

            #region List Top


            List<double> list_top = new List<double>();
            list_top.Add(0.00);
            list_top.Add(1.28);
            list_top.Add(2.55);
            list_top.Add(3.83);
            list_top.Add(5.10);
            list_top.Add(6.38);
            list_top.Add(7.65);
            list_top.Add(8.93);
            list_top.Add(10.20);
            list_top.Add(11.48);
            list_top.Add(12.75);
            #endregion List Top

            List<int> arr = new List<int>();

            #region Generate Coordinates
            list_Top_X = new List<double>();
            list_Bottom_X = new List<double>();
            list_Y = new List<double>();


            double _x = 0.0, _y = 0.0;



            for (int i = 0; i < list_top.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                _x = Width * list_top[i] / org_top;
                if (!list_Top_X.Contains(_x)) list_Top_X.Add(_x);
            }

            for (int i = 0; i < list_bottom.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                _x = Width * list_bottom[i] / org_bot;
                if (!list_Bottom_X.Contains(_x)) list_Bottom_X.Add(_x);
            }

            _y = 0.0;
            for (int i = 0; i < list_side.Count; i++)
            {
                _y = Height * list_side[i] / org_side;
                list_Y.Add(_y);
            }

            //for (int i = 0; i < No_of_Cells; i++)
            //{
            //    for (int j = 0; j < list_top.Count; j++)
            //    {
            //        //_x = _x + Width * list_top[j] / org_top;
            //        _x = Width * list_top[j] / org_top;
            //        if (!list_Top_X.Contains(_x)) list_Top_X.Add(_x);
            //    }
            //}
            _x = 0.0;
            //for (int i = 0; i < No_of_Cells; i++)
            //{
            //    for (int j = 0; j < list_bottom.Count; j++)
            //    {
            //        _x = _x + Width * list_bottom[j] / org_bot;
            //        list_Bottom_X.Add(_x);
            //    }
            //}



            JointNode jn = new JointNode();


            jnBots = new JointNodeCollection();
            jnSide = new JointNodeCollection();
            jnTop = new JointNodeCollection();


            _x = 0.0;
            for (int i = 0; i < No_of_Cells; i++)
            {
                if (i == 0)
                {
                    jn = new JointNode();
                    _x += list_Bottom_X[0];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
                for (int j = 1; j < list_Bottom_X.Count; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width) + list_Bottom_X[j];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
            }

            _x = 0.0;
            for (int i = 0; i < No_of_Cells; i++)
            {
                if (i == 0)
                {
                    jn = new JointNode();
                    _x += list_Top_X[0];
                    jn.X = _x;
                    jn.Y = Height;
                    jnTop.Add(jn);
                }
                for (int j = 1; j < list_Top_X.Count; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width) + list_Top_X[j];
                    jn.X = _x;
                    jn.Y = Height;
                    jnTop.Add(jn);
                }
            }


            _x = 0.0;
            _y = 0.0;
            jnSide.Clear();

            for (int i = 0; i <= No_of_Cells; i++)
            {
                _y = 0.0;
                for (int j = 1; j < list_Y.Count - 1; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width);
                    jn.X = _x;
                    jn.Y = list_Y[j];
                    jnSide.Add(jn);
                }
            }

            _x = 0.0;

             All_Joints = new JointNodeCollection();

            int count = 1;

            foreach (var item in jnBots)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }
            foreach (var item in jnTop)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }
            foreach (var item in jnSide)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }

            Member mbr = new Member();

             mbrCols = new MemberCollection();

            count = 1;

            #region Bottom Members
            mbrCols.Clear();
            for (int i = 1; i < jnBots.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = count++;
                mbr.StartNode = jnBots[i - 1];
                mbr.EndNode = jnBots[i];
                mbrCols.Add(mbr);

                arr.Add(mbr.MemberNo);
            }


            ht_bot_mems.Add(1, arr);

            #endregion Bottom Members


            #region Top Members
            arr = new List<int>();

            for (int i = 1; i < jnTop.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = count++;
                mbr.StartNode = jnTop[i - 1];
                mbr.EndNode = jnTop[i];
                mbrCols.Add(mbr);
                arr.Add(mbr.MemberNo);
            }

            ht_top_mems.Add(1, arr);

            #endregion Top Members

            #region Side Members

            for (int i = 0; i <= No_of_Cells; i++)
            {
                _y = 0.0;
                arr = new List<int>();

                for (int j = 1; j < list_Y.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = count++;

                    _x = (i * Width);

                    //mbr.StartNode = new JointNode(_x, list_Y[i - 1], 0.0);
                    //mbr.EndNode = new JointNode(_x, list_Y[i], 0.0);

                    mbr.StartNode = All_Joints.GetJoints(_x, list_Y[j - 1], 0.0, 0.1);
                    mbr.EndNode = All_Joints.GetJoints(_x, list_Y[j], 0.0, 0.1);


                    mbrCols.Add(mbr);

                    arr.Add(mbr.MemberNo);

                }
                if (i == 0 || i == No_of_Cells)
                {
                    ht_side_mems.Add(i, arr);
                }
                else
                {
                    ht_mid_mems.Add(i, arr);
                }

            }
            #endregion Side Members


            #endregion Generate Coordinates


            List<string> list = new List<string>();

            #region Live Load

            arr = ht_top_mems[1] as List<int>;

            LL_Load_Data = new List<string>();

            LL_Load_Data.Add(string.Format("LOAD 1 LOADTYPE 70R TRACK"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            double ll = LL_70R_Track / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));


            LL_Load_Data.Add(string.Format("LOAD 2 LOADTYPE 70R Wheel"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_70R_Wheel / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));

            LL_Load_Data.Add(string.Format("LOAD 3 LOADTYPE CLASS A"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_Class_A / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));



            LL_Load_Data.Add(string.Format("LOAD 4 LOADTYPE Boggie"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_Boggie / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));

            #endregion Live Load

            list.Add(string.Format("LOAD 1 DEAD LOAD"));
            //list.Add(string.Format("SELFWEIGHT Y -1 "));
            //list.Add(string.Format("SELFWEIGHT Y -1 "));

            double _dl = 25 * Height * Width / 11;

            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(arr), _dl));

            list.Add(string.Format("LOAD 2 SIDL (W/O)"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            list.Add(string.Format("{0} UNI GY -18.75", MyList.Get_Array_Text(arr)));
            list.Add(string.Format("LOAD 3 LIVE REDUCIBLE TITLE SIDL(WC)"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -1.43"));
            list.Add(string.Format("{0} UNI GY -1.43", MyList.Get_Array_Text(arr)));
            list.Add(string.Format("LOAD 4 EARTH PRESSURE BOTH SIDE"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("3 LIN Y -2 0"));
            //list.Add(string.Format("2 LIN Y -40.7 -2"));
            //list.Add(string.Format("1 LIN Y -43.2 -40.2"));
            //list.Add(string.Format("24 LIN Y 2 0"));
            //list.Add(string.Format("23 LIN Y 40.7 2"));
            //list.Add(string.Format("22 LIN Y 43.2 40.2"));

            arr = ht_side_mems[0] as List<int>;

            double ld = 0.0;
            double ld2 = 0.0;
            double Ka = 0.33;
            double Gama_d = 18.0;
            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX {1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }

            arr = ht_side_mems[No_of_Cells] as List<int>;

            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX -{1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }


            list.Add(string.Format("LOAD 5 EARTH PRESSURE ONE SIDE"));
            list.Add(string.Format("MEMBER LOAD"));
            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX -{1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }



            list.Add(string.Format("LOAD 6 SURCHARGE PRESSURE BS"));
            list.Add(string.Format("MEMBER LOAD"));

            arr = ht_side_mems[0] as List<int>;
            //list.Add(string.Format("1 TO 3 UNI GX 10.8"));
            list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));

            arr = ht_side_mems[No_of_Cells] as List<int>;
            list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));



            list.Add(string.Format("LOAD 7 SURCHARGE PRESSURE RS"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("22 TO 24 UNI GX -10.8"));
            list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));


            arr = ht_side_mems[0] as List<int>;
            list.Add(string.Format("LOAD 8 SURCHARGE PRESSURE LS"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));



            arr = ht_top_mems[1] as List<int>;

            list.Add(string.Format("LOAD 9 BREAKING LOAD RS"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FX -21.03", arr[arr.Count - 1]));



            list.Add(string.Format("LOAD 10 BREAKING LOAD LS"));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("101 FX 21.03"));
            list.Add(string.Format("{0} FX 21.03", arr[0]));
            list.Add(string.Format("LOAD 11 TEMPERATURE GRADIENT RISE"));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("101 FX 550.66"));
            //list.Add(string.Format("110 FX -550.66"));
            //list.Add(string.Format("101 MZ -68.9"));
            //list.Add(string.Format("110 MZ 68.9"));


            list.Add(string.Format("{0} FX 550.66", arr[0]));
            list.Add(string.Format("{0} FX -550.66", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ -68.9", arr[0]));
            list.Add(string.Format("{0} MZ 68.9", arr[arr.Count - 1]));


            list.Add(string.Format("LOAD 12 TEMPERATURE GRADIENT FALL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FX -287.23", arr[0]));
            list.Add(string.Format("{0} FX 287.23", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ -9.1", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ 9.1", arr[0]));


            DL_Load_Data = list;


            if (false)
            {
                #region Comment


                list.Add(string.Format("ASTRA SPACE MULTIPLE CELL INPUT DATA FILE"));
                list.Add(string.Format("UNIT KN METRES"));
                list.Add(string.Format("JOINT COORDINATES"));
                for (int i = 0; i < All_Joints.Count; i++)
                {
                    list.Add(string.Format("{0}", All_Joints[i].ToString()));
                }

                list.Add(string.Format("MEMBER INCIDENCES"));
                for (int i = 0; i < mbrCols.Count; i++)
                {
                    list.Add(string.Format("{0}", mbrCols[i].ToString()));
                }

                list.Add(string.Format("MEMBER PROPERTY"));


                arr = new List<int>();

                arr = ht_bot_mems[1] as List<int>;

                //list.Add(string.Format("210 TO 212 222 TO 350 PRIS YD 0.5 ZD 1"));
                list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Bottom_slab_thickness));
                //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));



                arr = ht_top_mems[1] as List<int>;


                //list.Add(string.Format("1 TO 3 22 TO 24 201 TO 209 PRIS YD 0.4 ZD 1"));
                list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Top_slab_thickness));



                for (int i = 0; i <= No_of_Cells; i++)
                {
                    if (i == 0 || i == No_of_Cells)
                    {
                        arr = ht_side_mems[i] as List<int>;
                        //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));
                        list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Side_wall_thickness));
                    }
                    else
                    {
                        arr = ht_mid_mems[i] as List<int>;
                        //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));
                        list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Mid_wall_thickness));
                    }
                }


                list.Add(string.Format("CONSTANTS"));
                list.Add(string.Format("E 2.17185e+007 ALL"));
                list.Add(string.Format("POISSON 0.17 ALL"));
                list.Add(string.Format("DENSITY 25 ALL"));
                list.Add(string.Format("DAMP 0.05 ALL"));

                arr = new List<int>();
                foreach (var item in jnBots)
                {
                    arr.Add(item.NodeNo);
                }

                list.Add(string.Format("SUPPORTS"));
                //list.Add(string.Format("1 5 9 13 169 TO 297 FIXED BUT FX FZ MX MY MZ KFY 1210.3"));
                list.Add(string.Format("{0} FIXED BUT FX FZ MX MY MZ KFY 1210.3", MyList.Get_Array_Text(arr)));
                //list.Add(string.Format("*1 5 9 13 169 TO 297 FIXED"));

                arr = ht_top_mems[1] as List<int>;

                list.Add(string.Format("LOAD 1 LOADTYPE Dead  TITLE DL"));
                list.Add(string.Format("SELFWEIGHT Y -1 "));
                list.Add(string.Format("LOAD 2 LOADTYPE Live REDUCIBLE TITLE SIDL"));
                list.Add(string.Format("MEMBER LOAD"));
                //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
                list.Add(string.Format("{0} UNI GY -18.75", MyList.Get_Array_Text(arr)));
                list.Add(string.Format("LOAD 3 LOADTYPE Live REDUCIBLE TITLE SIDL(WC)"));
                list.Add(string.Format("MEMBER LOAD"));
                //list.Add(string.Format("201 TO 209 UNI GY -1.43"));
                list.Add(string.Format("{0} UNI GY -1.43", MyList.Get_Array_Text(arr)));


                list.Add(string.Format("LOAD 4 LOADTYPE Live REDUCIBLE TITLE EARTH PRESSURE"));
                list.Add(string.Format("MEMBER LOAD"));
                //list.Add(string.Format("3 LIN Y -2 0"));
                //list.Add(string.Format("2 LIN Y -40.7 -2"));
                //list.Add(string.Format("1 LIN Y -43.2 -40.2"));
                //list.Add(string.Format("24 LIN Y 2 0"));
                //list.Add(string.Format("23 LIN Y 40.7 2"));
                //list.Add(string.Format("22 LIN Y 43.2 40.2"));



                arr = ht_side_mems[0] as List<int>;

                 ld = 0.0;
                 ld2 = 0.0;
                 Ka = 0.33;
                 Gama_d = 18.0;
                foreach (var item in arr)
                {
                    var mb = mbrCols.GetMember(item);

                    //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                    ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                    //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                    if (ld2 != 0.0)
                        list.Add(string.Format("{0} UNI GX {1:f3}", mb.MemberNo, ld2));
                    //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
                }

                arr = ht_side_mems[No_of_Cells] as List<int>;

                foreach (var item in arr)
                {
                    var mb = mbrCols.GetMember(item);

                    //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                    ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                    //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                    if (ld2 != 0.0)
                        list.Add(string.Format("{0} UNI GX -{1:f3}", mb.MemberNo, ld2));
                    //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
                }





                list.Add(string.Format("LOAD 5 LOADTYPE Live REDUCIBLE TITLE SURCHARGE PRESSURE(BS)"));
                list.Add(string.Format("MEMBER LOAD"));



                arr = ht_side_mems[0] as List<int>;
                //list.Add(string.Format("1 TO 3 UNI GX 10.8"));
                list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));

                arr = ht_side_mems[No_of_Cells] as List<int>;
                list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));



                list.Add(string.Format("LOAD 6 LOADTYPE None  TITLE SURCHARGE PRESSURE RS"));
                list.Add(string.Format("MEMBER LOAD"));
                //list.Add(string.Format("22 TO 24 UNI GX -10.8"));
                list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));


                arr = ht_side_mems[0] as List<int>;
                list.Add(string.Format("LOAD 7 LOADTYPE None  TITLE SURCHARGE PRESSURE LS"));
                list.Add(string.Format("MEMBER LOAD"));
                list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));



                arr = ht_top_mems[1] as List<int>;

                list.Add(string.Format("LOAD 8 LOADTYPE None  TITLE BREAKING LOAD RS"));
                list.Add(string.Format("JOINT LOAD"));
                list.Add(string.Format("{0} FX -21.03", arr[arr.Count - 1]));



                list.Add(string.Format("LOAD 9 LOADTYPE None  TITLE BREAKING LOAD LS"));
                list.Add(string.Format("JOINT LOAD"));
                //list.Add(string.Format("101 FX 21.03"));
                list.Add(string.Format("{0} FX 21.03", arr[0]));
                list.Add(string.Format("LOAD 10 LOADTYPE None  TITLE TEMPERATURE GRADIENT RISE"));
                list.Add(string.Format("JOINT LOAD"));
                //list.Add(string.Format("101 FX 550.66"));
                //list.Add(string.Format("110 FX -550.66"));
                //list.Add(string.Format("101 MZ -68.9"));
                //list.Add(string.Format("110 MZ 68.9"));


                list.Add(string.Format("{0} FX 550.66", arr[0]));
                list.Add(string.Format("{0} FX -550.66", arr[arr.Count - 1]));
                list.Add(string.Format("{0} MZ -68.9", arr[0]));
                list.Add(string.Format("{0} MZ 68.9", arr[arr.Count - 1]));


                list.Add(string.Format("LOAD 11 LOADTYPE None  TITLE TEMPERATURE GRADIENT FALL"));
                list.Add(string.Format("JOINT LOAD"));
                list.Add(string.Format("{0} FX -287.23", arr[0]));
                list.Add(string.Format("{0} FX 287.23", arr[arr.Count - 1]));
                list.Add(string.Format("{0} MZ -9.1", arr[arr.Count - 1]));
                list.Add(string.Format("{0} MZ 9.1", arr[0]));

                arr = ht_top_mems[1] as List<int>;

                //list.Add(string.Format("1 TO 3 UNI GX 10.8"));
                //list.Add(string.Format("22 TO 24 UNI GX -10.8"));

                list.Add(string.Format("PERFORM ANALYSIS PRINT ALL"));
                //list.Add(string.Format("FINISH"));
                list.Add(string.Format("FINISH"));


                #endregion Comment
            }

        }
        public void Create_Data_Singlecell()
        {

            No_of_Cells = 1;
            List<double> lst_fact = new List<double>();

            #region List Factors

            lst_fact.Add(0.0);
            lst_fact.Add(0.1);
            lst_fact.Add(0.2);
            lst_fact.Add(0.3);
            lst_fact.Add(0.4);
            lst_fact.Add(0.5);
            lst_fact.Add(0.6);
            lst_fact.Add(0.7);
            lst_fact.Add(0.8);
            lst_fact.Add(0.9);
            lst_fact.Add(1.0);

            #endregion List Factors




            List<int> arr = new List<int>();



            #region Generate Coordinates
            list_Top_X = new List<double>();
            list_Bottom_X = new List<double>();
            list_Y = new List<double>();


            double _x = 0.0, _y = 0.0;



            for (int i = 0; i < lst_fact.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                _x = Width * lst_fact[i];
                if (!list_Top_X.Contains(_x)) list_Top_X.Add(_x);
            }

            for (int i = 0; i < lst_fact.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                _x = Width * lst_fact[i];
                if (!list_Bottom_X.Contains(_x)) list_Bottom_X.Add(_x);
            }

            _y = 0.0;
            for (int i = 0; i < lst_fact.Count; i++)
            {
                _y = Height * lst_fact[i];
                list_Y.Add(_y);
            }

            _x = 0.0;

            JointNode jn = new JointNode();


            jnBots = new JointNodeCollection();
            jnSide = new JointNodeCollection();
            jnTop = new JointNodeCollection();


            _x = 0.0;
            for (int i = 0; i < No_of_Cells; i++)
            {
                if (i == 0)
                {
                    jn = new JointNode();
                    _x += list_Bottom_X[0];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
                for (int j = 1; j < list_Bottom_X.Count; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width) + list_Bottom_X[j];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
            }

            _x = 0.0;
            for (int i = 0; i < No_of_Cells; i++)
            {
                if (i == 0)
                {
                    jn = new JointNode();
                    _x += list_Top_X[0];
                    jn.X = _x;
                    jn.Y = Height;
                    jnTop.Add(jn);
                }
                for (int j = 1; j < list_Top_X.Count; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width) + list_Top_X[j];
                    jn.X = _x;
                    jn.Y = Height;
                    jnTop.Add(jn);
                }
            }


            _x = 0.0;
            _y = 0.0;
            jnSide.Clear();

            for (int i = 0; i <= No_of_Cells; i++)
            {
                _y = 0.0;
                for (int j = 1; j < list_Y.Count - 1; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width);
                    jn.X = _x;
                    jn.Y = list_Y[j];
                    jnSide.Add(jn);
                }
            }

            _x = 0.0;

            All_Joints = new JointNodeCollection();

            int count = 1;

            foreach (var item in jnBots)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }
            foreach (var item in jnTop)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }
            foreach (var item in jnSide)
            {
                item.NodeNo = count++;
                All_Joints.Add(item);
            }

            Member mbr = new Member();

            mbrCols = new MemberCollection();

            count = 1;

            #region Bottom Members
            mbrCols.Clear();
            for (int i = 1; i < jnBots.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = count++;
                mbr.StartNode = jnBots[i - 1];
                mbr.EndNode = jnBots[i];
                mbrCols.Add(mbr);
                arr.Add(mbr.MemberNo);
            }
            ht_bot_mems.Add(1, arr);

            #endregion Bottom Members

            #region Top Members
            arr = new List<int>();

            for (int i = 1; i < jnTop.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = count++;
                mbr.StartNode = jnTop[i - 1];
                mbr.EndNode = jnTop[i];
                mbrCols.Add(mbr);
                arr.Add(mbr.MemberNo);
            }

            ht_top_mems.Add(1, arr);

            #endregion Top Members

            #region Side Members

            for (int i = 0; i <= No_of_Cells; i++)
            {
                _y = 0.0;
                arr = new List<int>();

                for (int j = 1; j < list_Y.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = count++;

                    _x = (i * Width);

                    //mbr.StartNode = new JointNode(_x, list_Y[i - 1], 0.0);
                    //mbr.EndNode = new JointNode(_x, list_Y[i], 0.0);

                    mbr.StartNode = All_Joints.GetJoints(_x, list_Y[j - 1], 0.0, 0.1);
                    mbr.EndNode = All_Joints.GetJoints(_x, list_Y[j], 0.0, 0.1);


                    mbrCols.Add(mbr);

                    arr.Add(mbr.MemberNo);

                }
                if (i == 0 || i == No_of_Cells)
                {
                    ht_side_mems.Add(i, arr);
                }
                else
                {
                    ht_mid_mems.Add(i, arr);
                }

            }
            #endregion Side Members


            #endregion Generate Coordinates




            #region Live Load

            arr = ht_top_mems[1] as List<int>;

            LL_Load_Data = new List<string>();

            LL_Load_Data.Add(string.Format("LOAD 1 LOADTYPE 70R TRACK"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            double ll = LL_70R_Track / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));


            LL_Load_Data.Add(string.Format("LOAD 2 LOADTYPE 70R Wheel"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_70R_Wheel / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));

            LL_Load_Data.Add(string.Format("LOAD 3 LOADTYPE CLASS A"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_Class_A / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));



            LL_Load_Data.Add(string.Format("LOAD 4 LOADTYPE Boggie"));
            LL_Load_Data.Add(string.Format("MEMBER LOAD"));

            ll = LL_Boggie / Width;
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            LL_Load_Data.Add(string.Format("{0} UNI GY -{1:f2}", MyList.Get_Array_Text(arr), ll));

            #endregion Live Load

            List<string> list = new List<string>();

            list.Add(string.Format("LOAD 1 DEAD LOAD"));
            //list.Add(string.Format("SELFWEIGHT Y -1 "));
            //list.Add(string.Format("SELFWEIGHT Y -1 "));

            double _dl = 25 * Height * Width / 11;

            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(arr), _dl));

            list.Add(string.Format("LOAD 2 SIDL (W/O)"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            list.Add(string.Format("{0} UNI GY -18.75", MyList.Get_Array_Text(arr)));
            list.Add(string.Format("LOAD 3 LIVE REDUCIBLE TITLE SIDL(WC)"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("201 TO 209 UNI GY -1.43"));
            list.Add(string.Format("{0} UNI GY -1.43", MyList.Get_Array_Text(arr)));
            list.Add(string.Format("LOAD 4 EARTH PRESSURE BOTH SIDE"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("3 LIN Y -2 0"));
            //list.Add(string.Format("2 LIN Y -40.7 -2"));
            //list.Add(string.Format("1 LIN Y -43.2 -40.2"));
            //list.Add(string.Format("24 LIN Y 2 0"));
            //list.Add(string.Format("23 LIN Y 40.7 2"));
            //list.Add(string.Format("22 LIN Y 43.2 40.2"));

            arr = ht_side_mems[0] as List<int>;

            double ld = 0.0;
            double ld2 = 0.0;
            double Ka = 0.33;
            double Gama_d = 18.0;
            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX {1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }

            arr = ht_side_mems[No_of_Cells] as List<int>;

            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX -{1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }


            list.Add(string.Format("LOAD 5 EARTH PRESSURE ONE SIDE"));
            list.Add(string.Format("MEMBER LOAD"));
            foreach (var item in arr)
            {
                var mb = mbrCols.GetMember(item);

                //ld = 0.5 * Ka * Gama_d * mb.StartNode.Y * mb.StartNode.Y;
                ld2 = 0.5 * Ka * Gama_d * Math.Pow(Height - mb.EndNode.Y, 2.0); ;

                //list.Add(string.Format("{0} LIN Y -{1:f3} -{2:f3}", mb.MemberNo, ld2, ld));
                if (ld2 != 0.0)
                    list.Add(string.Format("{0} UNI GX -{1:f3}", mb.MemberNo, ld2));
                //list.Add(string.Format("{0} UNI GX -{1:f3}", MyList.Get_Array_Text(arr)));
            }



            list.Add(string.Format("LOAD 6 SURCHARGE PRESSURE BS"));
            list.Add(string.Format("MEMBER LOAD"));

            arr = ht_side_mems[0] as List<int>;
            //list.Add(string.Format("1 TO 3 UNI GX 10.8"));
            list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));

            arr = ht_side_mems[No_of_Cells] as List<int>;
            list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));



            list.Add(string.Format("LOAD 7 SURCHARGE PRESSURE RS"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("22 TO 24 UNI GX -10.8"));
            list.Add(string.Format("{0} UNI GX -10.8", MyList.Get_Array_Text(arr)));


            arr = ht_side_mems[0] as List<int>;
            list.Add(string.Format("LOAD 8 SURCHARGE PRESSURE LS"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("{0} UNI GX 10.8", MyList.Get_Array_Text(arr)));



            arr = ht_top_mems[1] as List<int>;

            list.Add(string.Format("LOAD 9 BREAKING LOAD RS"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FX -21.03", arr[arr.Count - 1]));



            list.Add(string.Format("LOAD 10 BREAKING LOAD LS"));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("101 FX 21.03"));
            list.Add(string.Format("{0} FX 21.03", arr[0]));
            list.Add(string.Format("LOAD 11 TEMPERATURE GRADIENT RISE"));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("101 FX 550.66"));
            //list.Add(string.Format("110 FX -550.66"));
            //list.Add(string.Format("101 MZ -68.9"));
            //list.Add(string.Format("110 MZ 68.9"));


            list.Add(string.Format("{0} FX 550.66", arr[0]));
            list.Add(string.Format("{0} FX -550.66", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ -68.9", arr[0]));
            list.Add(string.Format("{0} MZ 68.9", arr[arr.Count - 1]));


            list.Add(string.Format("LOAD 12 TEMPERATURE GRADIENT FALL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FX -287.23", arr[0]));
            list.Add(string.Format("{0} FX 287.23", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ -9.1", arr[arr.Count - 1]));
            list.Add(string.Format("{0} MZ 9.1", arr[0]));


            DL_Load_Data = list;
        }


        public List<string> DL_Load_Data { get; set; }

        public List<string> LL_Load_Data { get; set; }

        public void Write_Data(string fileName, bool liveLoad)
        {
            List<string> list = new List<string>();


            if(No_of_Cells == 1)
                list.Add(string.Format("ASTRA SPACE SINGLE CELL INPUT DATA FILE"));
            else
                list.Add(string.Format("ASTRA SPACE MULTIPLE CELL INPUT DATA FILE"));


            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            for (int i = 0; i < All_Joints.Count; i++)
            {
                list.Add(string.Format("{0}", All_Joints[i].ToString()));
            }

            list.Add(string.Format("MEMBER INCIDENCES"));
            for (int i = 0; i < mbrCols.Count; i++)
            {
                list.Add(string.Format("{0}", mbrCols[i].ToString()));
            }

            list.Add(string.Format("MEMBER PROPERTY"));

            List<int> arr = new List<int>();

            arr = ht_bot_mems[1] as List<int>;

            //list.Add(string.Format("210 TO 212 222 TO 350 PRIS YD 0.5 ZD 1"));
            list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Bottom_slab_thickness));
            //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));



            arr = ht_top_mems[1] as List<int>;


            //list.Add(string.Format("1 TO 3 22 TO 24 201 TO 209 PRIS YD 0.4 ZD 1"));
            list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Top_slab_thickness));



            for (int i = 0; i <= No_of_Cells; i++)
            {
                if (i == 0 || i == No_of_Cells)
                {
                    arr = ht_side_mems[i] as List<int>;
                    //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));
                    list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Side_wall_thickness));
                }
                else
                {
                    arr = ht_mid_mems[i] as List<int>;
                    //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));
                    list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Mid_wall_thickness));
                }
            }


            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 25 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));

            arr = new List<int>();
            //arr = ht_bot_mems[1] as List<int>;
            foreach (var item in jnBots)
            {
                arr.Add(item.NodeNo);
            }

            list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 5 9 13 169 TO 297 FIXED BUT FX FZ MX MY MZ KFY 1210.3"));
            list.Add(string.Format("{0} FIXED BUT FX MX MY MZ KFY 1210.3", MyList.Get_Array_Text(arr)));
            //list.Add(string.Format("*1 5 9 13 169 TO 297 FIXED"));

            if (liveLoad)
            {
                list.AddRange(LL_Load_Data.ToArray());
            }
            else
            {
                list.AddRange(DL_Load_Data.ToArray());
            }

            list.Add(string.Format("PERFORM ANALYSIS PRINT ALL"));
            list.Add(string.Format("FINISH"));

            Input_Data = list;

            File.WriteAllLines(fileName, list.ToArray());
        }
        void STAAD_Data()
        {
            List<string> list = new List<string>();

            #region STAAD LOADS
            list.Add(string.Format("LOAD 1 LOADTYPE Dead  TITLE DL"));
            list.Add(string.Format("SELFWEIGHT Y -1 "));
            list.Add(string.Format("LOAD 2 LOADTYPE Live REDUCIBLE TITLE SIDL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("201 TO 209 UNI GY -18.75"));
            list.Add(string.Format("LOAD 3 LOADTYPE Live REDUCIBLE TITLE SIDL(WC)"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("201 TO 209 UNI GY -1.43"));
            list.Add(string.Format("LOAD 4 LOADTYPE Live REDUCIBLE TITLE EARTH PRESSURE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("3 LIN Y -2 0"));
            list.Add(string.Format("2 LIN Y -40.7 -2"));
            list.Add(string.Format("1 LIN Y -43.2 -40.2"));
            list.Add(string.Format("24 LIN Y 2 0"));
            list.Add(string.Format("23 LIN Y 40.7 2"));
            list.Add(string.Format("22 LIN Y 43.2 40.2"));
            list.Add(string.Format("LOAD 5 LOADTYPE Live REDUCIBLE TITLE SURCHARGE PRESSURE(BS)"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TO 3 UNI GX 10.8"));
            list.Add(string.Format("22 TO 24 UNI GX -10.8"));
            list.Add(string.Format("LOAD 6 LOADTYPE None  TITLE SURCHARGE PRESSURE RS"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("22 TO 24 UNI GX -10.8"));
            list.Add(string.Format("LOAD 7 LOADTYPE None  TITLE SURCHARGE PRESSURE LS"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TO 3 UNI GX 10.8"));
            list.Add(string.Format("LOAD 8 LOADTYPE None  TITLE BREAKING LOAD RS"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("110 FX -21.03"));
            list.Add(string.Format("LOAD 9 LOADTYPE None  TITLE BREAKING LOAD LS"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("101 FX 21.03"));
            list.Add(string.Format("LOAD 10 LOADTYPE None  TITLE TEMPERATURE GRADIENT RISE"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("101 FX 550.66"));
            list.Add(string.Format("110 FX -550.66"));
            list.Add(string.Format("101 MZ -68.9"));
            list.Add(string.Format("110 MZ 68.9"));
            list.Add(string.Format("LOAD 11 LOADTYPE None  TITLE TEMPERATURE GRADIENT FALL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("101 FX -287.23"));
            list.Add(string.Format("110 FX 287.23"));
            list.Add(string.Format("110 MZ -9.1"));
            list.Add(string.Format("101 MZ 9.1"));

            #endregion STAAD LOADS

        }
    }
    public class BC_Results : List<BC_Table>
    {
        string _flName = "";
        public BC_Results(string fileName)
        {
            _flName = fileName;
            Read_From_Result();
        }
        void Read_From_Result()
        {
            if (!File.Exists(_flName)) return;
            List<string> list = new List<string>(File.ReadAllLines(_flName));

            string kStr = "";


            BC_Table tab = null;
            BC_Rows br = null;

            bool flag = false;
            for (int i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i].ToUpper());


                if (kStr == "") continue;
                if (kStr.StartsWith("-------------------"))
                {
                    if (flag) flag = false;
                    continue;
                }



                if(kStr.StartsWith("TABLE"))
                {
                    tab = new BC_Table(kStr);
                    this.Add(tab);
                    flag = true;
                    i += 5;
                    continue;
                }
                
                if(flag)
                {
                    br = BC_Rows.Parse(kStr);
                    if(br != null)
                    {
                        tab.Add(br);
                    }
                }

            }


        }

    }
    public class BC_Table : List<BC_Rows>
    {
        public string Title { get; set; }
        public BC_Table(string title):base()
        {
            Title = title;
        }
    }
    public class BC_Rows : List<string>
    {
        public BC_Rows():base()
        {
        }
        public static BC_Rows Parse(string txt)
        {
            try
            {
                MyList m = new MyList(MyList.RemoveAllSpaces(txt.ToUpper()), ' ');
                BC_Rows bc = new BC_Rows();
                bc.AddRange(m.StringList);
                return bc;
            }
            catch (Exception exx) { }

            return null;

        }
    }

}
