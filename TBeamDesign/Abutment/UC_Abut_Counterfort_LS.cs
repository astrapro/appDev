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
    public partial class UC_Abut_Counterfort_LS : UserControl
    {
        public IApplication iApp;

        public IApplication iapp
        {

            get
            {
                return iApp;
            }
            set
            {
                iApp = value;

                Load_Live_Loads();

            }
        }
        public UC_Abut_Counterfort_LS()
        {
            InitializeComponent();
            Load_Default_Data();
        }

        public bool Is_Individual
        {
            get
            {
                return !rbtn_dead_load.Visible;
            }
            set
            {
                rbtn_dead_load.Visible = !value;
            }
        }

        public double Length
        {
            get
            {
                if (dgv_input_data.RowCount > 1)
                    return MyList.StringToDouble(dgv_input_data[1, 1].Value.ToString(), 0.0);
                return 0.0;
            }
            set
            {
                if (dgv_input_data.RowCount > 1)
                    dgv_input_data[1, 1].Value = value.ToString("f3");
            }
        }
        public string Reaction_A
        {
            get
            {
                return txt_Reaction_A.Text;

            }
            set
            {
                txt_Reaction_A.Text = value;

            }
        }
        public string Reaction_B
        {
            get
            {
                return txt_Reaction_B.Text;

            }
            set
            {
                txt_Reaction_B.Text = value;

            }
        }
        void Load_Default_Data()
        {
            DataGridView dgv = dgv_input_data;

            List<string> list = new List<string>();
            MyList mlist = null;

            #region Base Pressure
            ////list.Add(string.Format("Span Arrangement$$"));
            list.Add(string.Format("Span Arrangement$$"));
            //list.Add(string.Format("$$"));
            //list.Add(string.Format("Span Arrangement (c/c of Exp. Joint)$51.250 $m."));
            list.Add(string.Format("Span Arrangement (c/c of Exp. Joint)$51.250 $m."));
            list.Add(string.Format("Dist. Betn c/l of exp jt to c/l of brg$0.625 $m."));
            list.Add(string.Format("Projection of deck beyond c/l of brg$0.600 $m."));
            list.Add(string.Format("Width of Expansion Gap$0.050 $m."));
            list.Add(string.Format("No. of Lane loading for design$2 $Nos."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Reduced Levels$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("FRL at C/L of carriageway$103.400 $m."));
            list.Add(string.Format("Cross Camber$2.500 $%"));
            list.Add(string.Format("Depth of the superstructure $3.4000 $m."));
            list.Add(string.Format("Thickness of wearing course$0.075 $m."));
            list.Add(string.Format("HFL$96.900 $m."));
            list.Add(string.Format("Bed Level at A1$86.000 $m."));
            list.Add(string.Format("Actual Foundation Level     $84.000 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Dimensional details of structural components$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Heel$$"));
            list.Add(string.Format("Width                       $8.000 $m."));
            list.Add(string.Format("Thickness  at root     $0.600 $m."));
            list.Add(string.Format("Tip Thickness $0.5999 $m."));
            list.Add(string.Format("Length of constant depth of heel$0.000 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Toe$$"));
            list.Add(string.Format(" Width                       $6.500 $m."));
            list.Add(string.Format("Thickness  at root       $0.600 $m."));
            list.Add(string.Format("Tip Thickness $0.5999 $m."));
            list.Add(string.Format("Length of constant depth of toe$0.000 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Shaft$$"));
            list.Add(string.Format("Width at top                      $0.600 $m."));
            list.Add(string.Format("Width at bottom$0.600 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Cap$$"));
            list.Add(string.Format(" Width  at top of Rect. part$2.000 $m."));
            list.Add(string.Format(" Width  at bottom of Trap. Part$0.600 $m."));
            list.Add(string.Format("Thickness of rect part at top$0.500 $m."));
            list.Add(string.Format("Thickness of trap. part at bot.$0.500 $m."));
            list.Add(string.Format("Length$12.000 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Solid Return  Wall$$"));
            list.Add(string.Format("Thickness at top$0.300 $m."));
            list.Add(string.Format("Thickness at bottom$1.400 $m."));
            list.Add(string.Format("No of return walls provided$2 $Nos."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Cantilever  Wall$$"));
            list.Add(string.Format("Thickness$0.600 $m."));
            list.Add(string.Format("Height at tip (free end)$0.500 $m."));
            list.Add(string.Format("No of cantilever walls provided$2 $Nos."));
            list.Add(string.Format("Thickness of counterfort$0.400$m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Dirt Wall$$"));
            list.Add(string.Format("Thickness$0.400 $m."));
            list.Add(string.Format("Length$12.000 $m."));
            list.Add(string.Format("Max. span of the toe/heel slab   =$2.875 $m."));
            list.Add(string.Format("Radius of Curvature$1000000 $m."));
            list.Add(string.Format("Superelevation$0.001 $m."));
            list.Add(string.Format("Number of bearings at abutment$2 $Nos."));
            list.Add(string.Format("Total Base Length$12.000 $m."));
            list.Add(string.Format("Design speed of the vehicle$100 $KMPH"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Loads and Forces from Superstructure$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Height of crash barrier$1.00 $m."));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Earth Pressure  Parameters$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Angle of Repose of Soil$32.00$Degrees"));
            list.Add(string.Format("Cohesion$0.00$kN/m^2"));
            list.Add(string.Format("Cohesion for sliding check$0.00$kN/m^2"));
            list.Add(string.Format("Coeff. of friction at base between soil & conc.$0.500 $"));
            list.Add(string.Format("Inclination of Backfill with horizontal$0.00$Degrees."));
            list.Add(string.Format("Angle of Wall friction$20.00$Degrees."));
            list.Add(string.Format("Equiv. Ht. of backfill for Live load surcharge$1.20$m."));
            list.Add(string.Format("Equiv. Ht. of earth for L. L surcharge, Slip road Side$0.00$m."));
            list.Add(string.Format("Unit weight of Soil (dry)$20.00 $kN/m^3"));
            list.Add(string.Format("Unit weight of Soil (submerged)$10.00 $kN/m^3"));
            list.Add(string.Format("Unit weight of concrete for stability check$25.00$kN/m^3"));
            list.Add(string.Format("Allowable Bearing Capacity (Normal Case)$500.00$kN/m^2"));
            list.Add(string.Format("Passive Earth pressure Coefficient for Stability$6.470 $"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Wind Load$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Wind speed$47.00$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Seismic Load$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Seismic Zone (i, ii, iii, iv or v)$v$"));
            list.Add(string.Format("Response Reduction factor$2.5$"));
            list.Add(string.Format("Type of founding strata$HS$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Material properties$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of Concrete$M 35$"));
            list.Add(string.Format("Grade of steel$Fe 500$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Design Constants:$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Partial Safety Factor Material of Concrete$1.50$"));
            list.Add(string.Format("Partial Safety Factor Material of Steel$1.15$"));
            list.Add(string.Format("Modulus of elasticity of reinforcing of steel $200000$"));
            list.Add(string.Format("Ultimate Compressive Strain in Concrete$0.0035$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("SIDL Loads:$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Unit weight of RCC$25.00$"));
            list.Add(string.Format("Width of Footpath$1.500$"));
            list.Add(string.Format("Avg. Height of raised Footpath $0.000$"));
            list.Add(string.Format("width of kerb$0.300$"));
            list.Add(string.Format("Height of Kerb$0.250$"));
            list.Add(string.Format("Wt of Railing / m$1.000$"));

            dgv = dgv_input_data;
            //dgv = dgv_input_data;
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure



            Modified_Cell(dgv_input_data);

            //if (dgv[2, i].Value == "") dgv[2, i].Value = "";

        }

        public void Modified_Cell()
        {
            Modified_Cell(dgv_input_data);
        }
        public void Modified_Cell(DataGridView dgv)
        {
            string s1, s2, s3, s4;
            int sl_no = 1;




            #region Format Input Data
            //dgv = dgv_base_pressure;

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
                        sl_no = 1;

                        dgv.Rows[i].ReadOnly = true;
                    }

                    //else
                    //{
                    //if (s2 != "") dgv[0, i].Value = sl_no++;
                    //}
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

            //if (dgv[1, 0].Value == "") dgv[2, i].Value = "";

        }

        public string Get_Project_Folder()
        {

            //string file_path = Path.Combine(iapp.user_path, "Counterfort Abutment Design");
            string file_path = Path.Combine(iapp.user_path, "Design of RCC Abutment with Open Foundation");



            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, cmb_boq_item.Text);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
        }

        private void Design_IRC_Abutment_Bridges_Box_Type_IRC()
        {


            string file_path = Get_Design_Report();
            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");


            //file_path = Path.Combine(file_path, "Counterfort Abutment Limit State Design [IRC].xlsm");
            //file_path = Path.Combine(file_path, "Abutment Design with Open Foundation in LSM [IRC].xlsm");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Counterfort Abutment LS\Counterfort Abutment Limit State Design [IRC].xlsm");
            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Counterfort Abutment LS\Counterfort Abutment Limit State Design IRC.xlsm");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            iapp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            myExcelWorkbooks = myExcelApp.Workbooks;


            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DLSUP"];


            if (rbtn_calculated.Checked == false)
            {
                //myExcelWorksheet.get_Range("F179").Formula = Reaction_A;
                //myExcelWorksheet.get_Range("F181").Formula = Reaction_B;

                myExcelWorksheet.get_Range("F179").Formula = Reaction_A;
                myExcelWorksheet.get_Range("F181").Formula = Reaction_A;



                myExcelWorksheet.get_Range("G179").Formula = "(Values Taken from Analysis / User)";
                myExcelWorksheet.get_Range("G181").Formula = "(Values Taken from Analysis / User)";


            }


            List<string> list = new List<string>();



            DataGridView dgv = dgv_input_data;
            int rindx = 0;


            #region Design Data
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

            rindx = 0;

            dgv = dgv_input_data;



            DataInputCollections dips = new DataInputCollections();

            dips.Load_Data_from_Grid(dgv);
            List<string> ldbl = new List<string>();

            //for (int i = 0; i < dgv.RowCount; i++)
            //{
            //    try
            //    {
            //        if (dgv[1, i].Value.ToString() != "")
            //            ldbl.Add(dgv[1, i].Value.ToString());
            //    }
            //    catch (Exception exx) { }
            //}


            rindx = 0;

            for (int i = 7; i < 123; i++)
            {
                if ((i >= 12 && i <= 14) ||
                    (i >= 22 && i <= 25) ||
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
                    //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToString();

                    if (rindx == 60)
                    {
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("M", "").Trim().ToString();

                        //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();

                    }
                    else if (rindx == 61)
                    {
                        //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("FE", "").Trim().ToString();
                    }
                    else
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToString();

                }
            }
            #endregion Design Data


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);
            iapp.Excel_Close_Message();

        }

        string Get_Design_Report()
        {
            string file_path = Get_Project_Folder();

            if(iapp.DesignStandard == eDesignStandard.IndianStandard) 
                file_path = Path.Combine(file_path, "Abutment Design with Open Foundation in LSM [IRC].xlsm");
            else
                file_path = Path.Combine(file_path, "Abutment Design with Open Foundation in LSM [BS].xlsm");
            return file_path;
        }

        private void Design_IRC_Abutment_Bridges_Box_Type_BS()
        {


            string file_path = Get_Design_Report();

            ////file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");


            //file_path = Path.Combine(file_path, "Counterfort Abutment Limit State Design [BS].xlsm");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Counterfort Abutment LS\Counterfort Abutment Limit State Design BS.xlsm");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Counterfort Abutment LS\Counterfort Abutment Limit State Design [BS].xlsm");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
                return;

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            myExcelWorkbooks = myExcelApp.Workbooks;

            iapp.Excel_Open_Message();


            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DLSUP"];


            if (rbtn_calculated.Checked == false)
            {
                myExcelWorksheet.get_Range("F179").Formula = Reaction_A;
                myExcelWorksheet.get_Range("F181").Formula = Reaction_B;
            }


            List<string> list = new List<string>();



            DataGridView dgv = dgv_input_data;
            int rindx = 0;
            int i = 0;


            #region Design Data
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

            rindx = 0;

            dgv = dgv_input_data;



            DataInputCollections dips = new DataInputCollections();

            dips.Load_Data_from_Grid(dgv);
            List<string> ldbl = new List<string>();

            //for (int i = 0; i < dgv.RowCount; i++)
            //{
            //    try
            //    {
            //        if (dgv[1, i].Value.ToString() != "")
            //            ldbl.Add(dgv[1, i].Value.ToString());
            //    }
            //    catch (Exception exx) { }
            //}


            rindx = 0;

            for ( i = 7; i < 123; i++)
            {
                if ((i >= 12 && i <= 14) ||
                    (i >= 22 && i <= 25) ||
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
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("M", "").Trim().ToString();

                        //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();

                    }
                    else if (rindx == 61)
                    {
                        //myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx].DATA.ToUpper().Replace("M", "").Trim().ToString();
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToUpper().Replace("FE", "").Trim().ToString();
                    }
                    else
                        myExcelWorksheet.get_Range("E" + i).Formula = dips[rindx++].DATA.ToString();

                }
            }
            #endregion Design Data

            #region cwll

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["cwll"];


            List<string> xls_cell = new List<string>();
            xls_cell.Add("Q");
            xls_cell.Add("R");
            xls_cell.Add("S");
            xls_cell.Add("T");
            xls_cell.Add("U");
            xls_cell.Add("V");
            xls_cell.Add("W");


            List<double> load1 = new List<double>();
            List<double> dist1 = new List<double>();
            
            List<double> load2 = new List<double>();
            List<double> dist2 = new List<double>();


            i = 0;

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




            #region HB Load 1

            int cell = 26;
            string kStr = "";



            //myExcelWorksheet.get_Range("Q25").Formula = cmb_bs_ll_1.Text + " Loading";
            myExcelWorksheet.get_Range("Q25").Formula = cmb_bs_ll_1.Text;

            for (i = 0; i < xls_cell.Count; i++)
            {
                kStr = xls_cell[i] + cell;

                if (i < load1.Count)
                    myExcelWorksheet.get_Range(kStr).Formula = load1[i].ToString();
                else
                    myExcelWorksheet.get_Range(kStr).Formula = "";
            }
            cell = 27;
            for (i = 1; i < xls_cell.Count; i++)
            {
                kStr = xls_cell[i] + cell;


                if ((i-1) < dist1.Count)
                    myExcelWorksheet.get_Range(kStr).Formula = dist1[i-1].ToString();
                else
                    myExcelWorksheet.get_Range(kStr).Formula = "";
            }


            #endregion HA Load 1

            #region HB Load 2

            xls_cell.Insert(0, "P");

             cell = 64;
             kStr = "";
             //myExcelWorksheet.get_Range("P63").Formula = cmb_bs_ll_2.Text + " Loading";
             myExcelWorksheet.get_Range("P63").Formula = cmb_bs_ll_2.Text; 

            for (i = 0; i < xls_cell.Count; i++)
            {
                kStr = xls_cell[i] + cell;

                if (i < load1.Count)
                    myExcelWorksheet.get_Range(kStr).Formula = load1[i].ToString();
                else
                    myExcelWorksheet.get_Range(kStr).Formula = "";
            }
            cell = 65;
            for (i = 1; i < xls_cell.Count; i++)
            {
                kStr = xls_cell[i] + cell;


                if ((i - 1) < dist1.Count)
                    myExcelWorksheet.get_Range(kStr).Formula = dist1[i-1].ToString();
                else
                    myExcelWorksheet.get_Range(kStr).Formula = "";
            }


            #endregion HA Load 2



            #endregion cwll


            rindx = 0;

            myExcelWorkbook.Save();
            iApp.Excel_Close_Message();
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

        public event EventHandler OnButtonClick;
        private void btn_new_design_Click(object sender, EventArgs e)
        {
            if (OnButtonClick != null)
            {
                OnButtonClick(sender, e);
            }

            if (iApp == null) return;
            if (iapp.DesignStandard == eDesignStandard.BritishStandard)
            {
                Design_IRC_Abutment_Bridges_Box_Type_BS();
            }
            else
            {
                Design_IRC_Abutment_Bridges_Box_Type_IRC();
            }
        }

        public event EventHandler dead_load_CheckedChanged;
        private void rbtn_dead_load_CheckedChanged(object sender, EventArgs e)
        {
            if (dead_load_CheckedChanged != null) dead_load_CheckedChanged(sender, e);

            if (rbtn_calculated.Checked)
            {
                txt_Reaction_A.Enabled = false;
                txt_Reaction_B.Enabled = false;


                txt_Reaction_A.Text = txt_Reaction_A.Tag.ToString();
                txt_Reaction_B.Text = txt_Reaction_B.Tag.ToString();


                if (txt_Reaction_A.Text == "") txt_Reaction_A.Text = "4417.59";
                if (txt_Reaction_B.Text == "") txt_Reaction_B.Text = "4417.59";

            }
            else if (rbtn_user_given.Checked)
            {
                txt_Reaction_A.Enabled = true;
                txt_Reaction_B.Enabled = true;



                txt_Reaction_A.Tag = txt_Reaction_A.Text;
                txt_Reaction_B.Tag = txt_Reaction_B.Text;

                txt_Reaction_A.Text = "";
                txt_Reaction_B.Text = "";

            }
            else
            {
                txt_Reaction_A.Enabled = true;
                txt_Reaction_B.Enabled = true;
            }
        }

        private void btn_open_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_open_worksheet)
            {
                iapp.Open_WorkSheet_Design();
            }

            else if (btn == btn_open_design)
            {
                string file_path = Get_Design_Report();

                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                //{
                //    file_path = Path.Combine(file_path, "Counterfort Abutment Limit State Design [IRC].xlsm");
                //}
                //else
                //{
                //    file_path = Path.Combine(file_path, "Counterfort Abutment Limit State Design [BS].xlsm");
                //}

                if (File.Exists(file_path))
                {
                    iApp.OpenExcelFile(file_path, "2011ap");
                }
                else
                {
                    MessageBox.Show(file_path + " file not found.");
                    return;
                }

            }

        }


        public void Load_Live_Loads()
        {
            if (iApp == null) return;
            //cmb_bs_ll_2
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_1);
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_2);
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
               
                //cmb_bs_ll_1.SelectedIndex = 8;
                //cmb_bs_ll_2.SelectedIndex = 13;
                cmb_bs_ll_2.SelectedIndex = 8;
                cmb_bs_ll_1.SelectedIndex = 13;
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

                tc_design.TabPages.Remove(tab_liveloads);
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
            for (int i = 0; i < ld.Loads_In_KN.StringList.Count; i++)
            //for (int i = 0; i < ld.Loads.StringList.Count; i++)
            {
                if (i == 0)
                {
                    //dgv.Rows.Add((i + 1), ld.Loads.StringList[0], "");
                    dgv.Rows.Add((i + 1), ld.Loads_In_KN.StringList[0], "");

                    dgv[2, 0].ReadOnly = true;
                    dgv[2, 0].Style.BackColor = Color.Gray;
                }
                else
                {
                    //dgv.Rows.Add((i + 1), ld.Loads.StringList[i], ld.Distances.StringList[i - 1]);
                    dgv.Rows.Add((i + 1), ld.Loads_In_KN.StringList[i], ld.Distances.StringList[i - 1]);
                }
            }
            txtb.Text = (ld.Total_Loads * 10).ToString("f3");
            //txtb.Text = (ld.Total_Loads).ToString("f3");

            //txtd.Text = (ld.LoadWidth).ToString("f3");
            txtd.Text = (ld.Distances.SUM).ToString("f3");

        }

        private void btn_Excel_Notes_Click(object sender, EventArgs e)
        {
            if (iApp != null) iApp.Open_Excel_Macro_Notes();
        }


    }
}
