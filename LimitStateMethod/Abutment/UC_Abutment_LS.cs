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



namespace LimitStateMethod.Abutment
{
    public partial class UC_Abutment_LS : UserControl
    {
        public IApplication iapp;

        public UC_Abutment_LS()
        {
            InitializeComponent();
            Load_Default_Data();
        }

        void Load_Default_Data()
        {
            DataGridView dgv = dgv_base_pressure;

            List<string> list = new List<string>();
            MyList mlist = null;

            #region Base Pressure
            list.Add(string.Format("Span Arrangement$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Span Arrangement (c/c of Exp. Joint)$51.250 $m."));
            list.Add(string.Format("Dist. Betn c/l of exp jt to c/l of brg$0.625 $m."));
            list.Add(string.Format("Projection of deck beyond c/l of brg$0.600 $m."));
            list.Add(string.Format("Width of Expansion Gap$0.050 $m."));
            list.Add(string.Format("No. of Lane loading for design$2 $Nos."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Reduced Levels$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("FRL at C/L of carriageway$103.400 $m."));
            list.Add(string.Format("Cross Camber$2.500 $%"));
            list.Add(string.Format("Depth of the superstructure $3.4000 $m."));
            list.Add(string.Format("Thickness of wearing course$0.075 $m."));
            list.Add(string.Format("HFL$96.900 $m."));
            list.Add(string.Format("Bed Level at A1$86.000 $m."));
            list.Add(string.Format("Actual Foundation Level     $84.000 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Dimensional details of structural components$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Heel$$"));
            list.Add(string.Format("Width                       $8.000 $m."));
            list.Add(string.Format("Thickness  at root     $0.600 $m."));
            list.Add(string.Format("Tip Thickness $0.5999 $m."));
            list.Add(string.Format("Length of constant depth of heel$0.000 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Toe$$"));
            list.Add(string.Format(" Width                       $6.500 $m."));
            list.Add(string.Format("Thickness  at root       $0.600 $m."));
            list.Add(string.Format("Tip Thickness $0.5999 $m."));
            list.Add(string.Format("Length of constant depth of toe$0.000 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Shaft$$"));
            list.Add(string.Format("Width at top                      $0.600 $m."));
            list.Add(string.Format("Width at bottom$0.600 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment Cap$$"));
            list.Add(string.Format(" Width  at top of Rect. part$2.000 $m."));
            list.Add(string.Format(" Width  at bottom of Trap. Part$0.600 $m."));
            list.Add(string.Format("Thickness of rect part at top$0.500 $m."));
            list.Add(string.Format("Thickness of trap. part at bot.$0.500 $m."));
            list.Add(string.Format("Length$12.000 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Solid Return  Wall$$"));
            list.Add(string.Format("Thickness at top$0.300 $m."));
            list.Add(string.Format("Thickness at bottom$1.400 $m."));
            list.Add(string.Format("No of return walls provided$2 $Nos."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Cantilever  Wall$$"));
            list.Add(string.Format("Thickness$0.600 $m."));
            list.Add(string.Format("Height at tip (free end)$0.500 $m."));
            list.Add(string.Format("No of cantilever walls provided$2 $Nos."));
            list.Add(string.Format("Thickness of counterfort$0.400$m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Dirt Wall$$"));
            list.Add(string.Format("Thickness$0.400 $m."));
            list.Add(string.Format("Length$12.000 $m."));
            list.Add(string.Format("Max. span of the toe/heel slab   =$2.875 $m."));
            list.Add(string.Format("Radius of Curvature$1000000 $m."));
            list.Add(string.Format("Superelevation$0.001 $m."));
            list.Add(string.Format("Number of bearings at abutment$2 $Nos."));
            list.Add(string.Format("Total Base Length$12.000 $m."));
            list.Add(string.Format("Design speed of the vehicle$100 $KMPH"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Loads and Forces from Superstructure$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Height of crash barrier$1.00 $m."));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Earth Pressure  Parameters$$"));
            list.Add(string.Format("$$"));
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
            list.Add(string.Format("$$"));
            list.Add(string.Format("Wind Load$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Wind speed$47.00$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Seismic Load$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Seismic Zone (i, ii, iii, iv or v)$v$"));
            list.Add(string.Format("Response Reduction factor$2.5$"));
            list.Add(string.Format("Type of founding strata$HS$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Material properties$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of Concrete$M 35$"));
            list.Add(string.Format("Grade of steel$Fe 500$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Design Constants:$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Partial Safety Factor Material of Concrete$1.50$"));
            list.Add(string.Format("Partial Safety Factor Material of Steel$1.15$"));
            list.Add(string.Format("Modulus of elasticity of reinforcing of steel $200000$"));
            list.Add(string.Format("Ultimate Compressive Strain in Concrete$0.0035$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Page$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("SIDL Loads:$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Unit weight of RCC$25.00$"));
            list.Add(string.Format("Width of Footpath$1.500$"));
            list.Add(string.Format("Avg. Height of raised Footpath $0.000$"));
            list.Add(string.Format("width of kerb$0.300$"));
            list.Add(string.Format("Height of Kerb$0.250$"));
            list.Add(string.Format("Wt of Railing / m$1.000$"));

            dgv = dgv_base_pressure;
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure



            Modified_Cell(dgv_base_pressure);

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
                    }
                    //else
                    //{
                    //if (s2 != "") dgv[0, i].Value = sl_no++;
                    //}
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data
        }

        public string Get_Project_Folder()
        {

            string file_path = Path.Combine(iapp.user_path, "Counterfort Abutment Design IRC");

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, cmb_boq_item.Text);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
        }

        private void Design_IRC_Abutment_Bridges_Box_Type()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Counterfort Abutment Limit State Design [IRC].xlsm");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Counterfort Abutment LS\Counterfort Abutment Limit State Design [IRC].xlsm");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            myExcelWorkbooks = myExcelApp.Workbooks;


            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DLSUP"];



            List<string> list = new List<string>();



            DataGridView dgv = dgv_base_pressure;
            int rindx = 0;


            #region Base Pressure
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

            rindx = 0;

            dgv = dgv_base_pressure;


            List<string> ldbl = new List<string>();

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[1, i].Value.ToString() != "")
                        ldbl.Add(dgv[1, i].Value.ToString());
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            for (int i = 7; i < 55; i++)
            {
                if (i == 18 ||
                    i == 19 ||
                    i == 25 ||
                    (i >= 28 && i <= 30) ||
                    i == 35 ||
                    i == 44 ||
                    i == 47)
                {
                    continue;
                }
                else
                {
                    myExcelWorksheet.get_Range("F" + i).Formula = ldbl[rindx++].ToString();
                }
            }
            #endregion Earth pr


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iapp.Excel_Open_Message();
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

    }
}
