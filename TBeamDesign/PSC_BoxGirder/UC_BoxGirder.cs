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

using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class UC_BoxGirder : UserControl
    {

        public double Span
        {
            get
            {
                if (dgv_user_input.RowCount > 0)
                {
                    return MyList.StringToDouble(dgv_user_input[1, 0].Value.ToString(), 0.0);
                }
                return 0.0;
            }
            set
            {
                if(dgv_user_input.RowCount > 0)
                {
                    dgv_user_input[1, 0].Value = value.ToString("f3");
                }
            }
        }

        public double Width
        {
            get
            {
                if (dgv_user_input.RowCount > 0)
                {
                    return MyList.StringToDouble(dgv_user_input[1, 3].Value.ToString(), 0.0);
                }
                return 0.0;
            }
            set
            {
                if (dgv_user_input.RowCount > 0)
                {
                    dgv_user_input[1, 3].Value = value.ToString("f3");
                }
            }
        }
        public IApplication iApp { get; set; }
        public UC_BoxGirder()
        {
            InitializeComponent();
            //Load_Data();
        }
        private void Design_BS_PSC_Box_Girder_Bridge()
        {

            iApp.Excel_Open_Message();
            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design Of PSC Box Girder Bridge.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\PSC Box Girder\Design Of PSC Box Girder Bridge.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet) myExcelWorkbook.Sheets["Input"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            List<string> list = new List<string>();


            #region Section Input



            //myExcelWorksheet.get_Range("A9").Formula = txt_sec_B1.Text;
            //myExcelWorksheet.get_Range("B9").Formula = txt_sec_B2.Text;
            //myExcelWorksheet.get_Range("C9").Formula = txt_sec_B3.Text;
            //myExcelWorksheet.get_Range("E9").Formula = txt_sec_B4.Text;
            //myExcelWorksheet.get_Range("D15").Formula = txt_sec_B5.Text;
            //myExcelWorksheet.get_Range("G17").Formula = txt_sec_B6.Text;



            //myExcelWorksheet.get_Range("A17").Formula = txt_sec_H1.Text;
            //myExcelWorksheet.get_Range("E12").Formula = txt_sec_H2.Text;
            //myExcelWorksheet.get_Range("G12").Formula = txt_sec_H3.Text;
            //myExcelWorksheet.get_Range("I12").Formula = txt_sec_H4.Text;
            //myExcelWorksheet.get_Range("D13").Formula = txt_sec_H5.Text;
            //myExcelWorksheet.get_Range("E20").Formula = txt_sec_H6.Text;





            #endregion Section Input

            int rindx = 0;

            #region Section Input

            rindx = 0;
            for (int i = 6; i <= 20; i++)
            {
                if (i == 7
                   || i == 8
                   || i == 16
                    )
                {
                    continue;
                }
                try
                {
                    myExcelWorksheet.get_Range("H" + i).Formula = dgv_user_input[1, rindx++].Value.ToString();
                }
                catch (Exception ex) { }

            }
            #endregion Section Input

            #region Properties


            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Prop"];

            rindx = 0;

            DataGridView dgv = dgv_seg_tab3;

            for (int i = 23; i <= 44; i++)
            {
                if (i == 35
                   || i == 36
                   || i == 37
                    )
                {
                    rindx++;
                    continue;
                }
                try
                {
                    myExcelWorksheet.get_Range("D" + i).Formula = dgv[1, rindx].Value.ToString();
                    myExcelWorksheet.get_Range("E" + i).Formula = dgv[2, rindx].Value.ToString();
                    myExcelWorksheet.get_Range("F" + i).Formula = dgv[3, rindx].Value.ToString();
                    myExcelWorksheet.get_Range("G" + i).Formula = dgv[4, rindx].Value.ToString();
                    myExcelWorksheet.get_Range("H" + i).Formula = dgv[5, rindx].Value.ToString();
                    myExcelWorksheet.get_Range("I" + i).Formula = dgv[6, rindx].Value.ToString();

                    rindx++;
                }
                catch (Exception ex) { }

            }
            #endregion Properties

            #region BM-SF

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["bm-sf"];

            try
            {

                #region Dead Load

                rindx = 21;

                myExcelWorksheet.get_Range("D" + rindx).Formula = txt_BM_DL_Supp.Text;
                myExcelWorksheet.get_Range("E" + rindx).Formula = txt_BM_DL_Deff.Text;
                myExcelWorksheet.get_Range("F" + rindx).Formula = txt_BM_DL_L8.Text;
                myExcelWorksheet.get_Range("G" + rindx).Formula = txt_BM_DL_L4.Text;
                myExcelWorksheet.get_Range("H" + rindx).Formula = txt_BM_DL_3L8.Text;
                myExcelWorksheet.get_Range("I" + rindx).Formula = txt_BM_DL_Mid.Text;


                rindx = 22;

                myExcelWorksheet.get_Range("D" + rindx).Formula = txt_SF_DL_Supp.Text;
                myExcelWorksheet.get_Range("E" + rindx).Formula = txt_SF_DL_Deff.Text;
                myExcelWorksheet.get_Range("F" + rindx).Formula = txt_SF_DL_L8.Text;
                myExcelWorksheet.get_Range("G" + rindx).Formula = txt_SF_DL_L4.Text;
                myExcelWorksheet.get_Range("H" + rindx).Formula = txt_SF_DL_3L8.Text;
                myExcelWorksheet.get_Range("I" + rindx).Formula = txt_SF_DL_Mid.Text;

                #endregion Dead Load


                #region Live Load

                rindx = 49;

                myExcelWorksheet.get_Range("E" + rindx).Formula = txt_BM_LL_Supp.Text;
                myExcelWorksheet.get_Range("F" + rindx).Formula = txt_BM_LL_Deff.Text;
                myExcelWorksheet.get_Range("G" + rindx).Formula = txt_BM_LL_L8.Text;
                myExcelWorksheet.get_Range("H" + rindx).Formula = txt_BM_LL_L4.Text;
                myExcelWorksheet.get_Range("I" + rindx).Formula = txt_BM_LL_3L8.Text;
                myExcelWorksheet.get_Range("J" + rindx).Formula = txt_BM_LL_Mid.Text;


                rindx = 56;

                myExcelWorksheet.get_Range("E" + rindx).Formula = txt_SF_LL_Supp.Text;
                myExcelWorksheet.get_Range("F" + rindx).Formula = txt_SF_LL_Deff.Text;
                myExcelWorksheet.get_Range("G" + rindx).Formula = txt_SF_LL_L8.Text;
                myExcelWorksheet.get_Range("H" + rindx).Formula = txt_SF_LL_L4.Text;
                myExcelWorksheet.get_Range("I" + rindx).Formula = txt_SF_LL_3L8.Text;
                myExcelWorksheet.get_Range("J" + rindx).Formula = txt_SF_LL_Mid.Text;

                rindx++;

                #endregion Live Load


            }
            catch (Exception ex) { }


            #endregion BM-SF

            #region Prestress
            myExcelWorksheet = (Excel.Worksheet) myExcelWorkbook.Sheets["Prestress"];

            try
            {

                #region Inputs

                rindx = 0;

                dgv = dgv_prestress_1;

                for (int i = 11; i <= 25; i++)
                {
                    if (i == 12
                       || (i >= 12 && i <= 16)
                       || i == 24
                        )
                    {
                        continue;
                    }
                    try
                    {
                        myExcelWorksheet.get_Range("G" + i).Formula = dgv[1, rindx].Value.ToString();

                        rindx++;
                    }
                    catch (Exception ex) { }

                }

                #endregion Inputs

                #region Cable Profile

                rindx = 0;

                dgv = dgv_prestress_2;

                for (int i = 34; i <= 47; i++)
                {
                    //if (i == 35
                    //   || i == 36
                    //   || i == 37
                    //    )
                    //{
                    //    rindx++;
                    //    continue;
                    //}
                    try
                    {
                        dgv = dgv_prestress_2;

                        myExcelWorksheet.get_Range("C" + i).Formula = dgv[1, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + i).Formula = dgv[2, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("G" + i).Formula = dgv[3, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + i).Formula = dgv[4, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("I" + i).Formula = dgv[5, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("J" + i).Formula = dgv[6, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("K" + i).Formula = dgv[7, rindx].Value.ToString();

                        rindx++;

                        i++;

                        dgv = dgv_prestress_3;

                        myExcelWorksheet.get_Range("F" + i).Formula = dgv[1, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("G" + i).Formula = dgv[2, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + i).Formula = dgv[3, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("I" + i).Formula = dgv[4, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("J" + i).Formula = dgv[5, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("K" + i).Formula = dgv[6, rindx].Value.ToString();

                        rindx++;
                    }
                    catch (Exception ex) { }

                }

                #endregion Cable Profile

            }
            catch (Exception ex) { }


            #endregion Prestress

            #region Temp
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Temp"];

            try
            {

                #region Inputs

                rindx = 0;

                dgv = dgv_temp;

                for (int i = 4; i <= 9; i++)
                {
                    if (i == 12
                       || (i >= 12 && i <= 16)
                       || i == 24
                        )
                    {
                        continue;
                    }
                    try
                    {
                        myExcelWorksheet.get_Range("I" + i).Formula = dgv[1, rindx].Value.ToString();

                        rindx++;
                    }
                    catch (Exception ex) { }

                }

                #endregion Inputs

            }
            catch (Exception ex) { }


            #endregion Temp




            #region Ultimate
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Ultimate"];

            try
            {

                #region Inputs


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    myExcelWorksheet.get_Range("A10").Formula = "Ultimate Shear Capacity of Section uncracked in Flexure (As per Relevant Code)";
                    myExcelWorksheet.get_Range("A23").Formula = "Ultimate Shear Capacity of Section cracked in Flexure (As per Relevant Code)";
                    myExcelWorksheet.get_Range("A36").Formula = "Check for Limiting Shear for Outer Girder (As per Relevant Code)";
                    myExcelWorksheet.get_Range("A42").Formula = "Provision of Shear Reinforcement (As per Relevant Code)";
                    myExcelWorksheet.get_Range("A42").Formula = "Provision of Shear Reinforcement (As per Relevant Code) ";

                    myExcelWorksheet.get_Range("F4").Formula = "(As per Relevant Code) ";
                    myExcelWorksheet.get_Range("F5").Formula = "(As per Relevant Code) ";
                    myExcelWorksheet.get_Range("F6").Formula = "(As per Relevant Code) ";
                }


                #endregion Inputs

            }
            catch (Exception ex) { }


            #endregion Ultimate

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

        }

        public void Load_Data()
        {

            List<string> list = new List<string>();
            list.Add(string.Format("Overall Span (C/C spacing of exp. joint)$48.750$m"));
            //list.Add(string.Format("Effective Span (C/C spacing of Bearing)$47.750$m"));
            list.Add(string.Format("Girder end to bearing centre line$0.500$m"));
            list.Add(string.Format("Expansion gap $0.040$m"));
            list.Add(string.Format("Width of deck$9.750$m"));
            list.Add(string.Format("Depth of Box Girder$2.500$m"));
            list.Add(string.Format("Grade of Concrete of Girder$40$Mpa"));
            list.Add(string.Format("Age of concrete for at transfer$14$days"));
            list.Add(string.Format("Maturity of concrete for at transfer$87$%"));
            list.Add(string.Format("Age of girder at the time of casting of SIDL$56$days"));
            list.Add(string.Format("Maturity of girder at the time of casting of SIDL$100$%"));
            list.Add(string.Format("Extra time dependent loss to be considered$20.0$%"));
            list.Add(string.Format("Wearing coat thickness $0.065$m"));

            MyList.Fill_List_to_Grid(dgv_user_input, list, '$');

            Load_Tab2_Tab3_Box_Segment_Data();




            list.Clear();
            list.Add(string.Format("Nominal Diameter=D=$15.2$mm"));
            list.Add(string.Format("Young's Modulus of Elasticity=Eps=$195$Gpa"));
            list.Add(string.Format("Jacking Force at Transfer (% of Breaking Load)=Pj=$76.5$%"));
            list.Add(string.Format("Slip at Jacking end=s=$6$mm"));
            list.Add(string.Format("Coefficient of Friction=m=$0.17$per radian"));
            list.Add(string.Format("Wobble Friction Coefficient=k=$0.002$per metre"));
            list.Add(string.Format("Relaxation of prestressing steel at 70% uts=Re1=$35.0$Mpa "));
            list.Add(string.Format("Relaxation of prestressing steel at 50% uts=Re2=$0$MPa"));
            list.Add(string.Format("Dia of Prestressing Duct=qd=$110$mm"));

            MyList.Fill_List_to_Grid(dgv_prestress_1, list, '$');


            list.Clear();

            list.Add(string.Format("1$2$1.672$1.505$1.274$0.876$0.478$0.130"));
            list.Add(string.Format("2$2$1.104$0.970$0.783$0.462$0.147$0.130"));
            list.Add(string.Format("3$1.052$0.000$0.000$0.000$0.000$0.167$0.130"));
            list.Add(string.Format("4$2$0.000$0.000$0.000$0.279$0.130$0.130"));
            list.Add(string.Format("5$2$0.522$0.312$0.130$0.130$0.130$0.130"));
            list.Add(string.Format("6$2$0.248$0.130$0.130$0.130$0.130$0.130"));
            list.Add(string.Format("7$1.052$0.248$0.130$0.130$0.130$0.130$0.130"));


            MyList.Fill_List_to_Grid(dgv_prestress_2, list, '$');


            list.Clear();
            list.Add(string.Format("1$326.6$335.9$342.6$345.5$348.3$345.4"));
            list.Add(string.Format("2$326.5$335.8$343.1$347.4$350.7$333.6"));
            list.Add(string.Format("3$0.0$0.0$0.0$0.0$316.9$334.2"));
            list.Add(string.Format("4$0.0$0.0$0.0$307.3$338.6$339.5"));
            list.Add(string.Format("5$328.7$330.6$338.4$341.4$339.7$335.6"));
            list.Add(string.Format("6$317.0$325.0$336.5$343.1$342.0$340.9"));
            list.Add(string.Format("7$338.2$345.3$347.0$350.0$353.0$356.0"));


            MyList.Fill_List_to_Grid(dgv_prestress_3, list, '$');



            list.Clear();
            list.Add(string.Format("Total Height of the girder=h=$2.5$m"));
            list.Add(string.Format("C.G. of Girder from bottom=Y=$1.525$m"));
            list.Add(string.Format("M.O.I. of the Section=I=$4.6758$m^4"));
            list.Add(string.Format("Area of the Section=A=$4.9663$m^2"));
            list.Add(string.Format("Modulus of Elasticity of Concrete=Ec=$3.16E+07$KN/m^2 "));
            list.Add(string.Format("Coefficient of thermal expansion of concrete=a=$0.0000117$oC"));

            MyList.Fill_List_to_Grid(dgv_temp, list, '$');

            //MyList ml = null;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    ml = new MyList
            //}

            Update_Tab3_Data();
        }



        public void Load_Tab2_Tab3_Box_Segment_Data()
        {

            List<string> list = new List<string>();
            list.Add(string.Format("D 2.5 2.5 2.5 2.5 2.5 2.5"));
            list.Add(string.Format("Dw 9.75 9.75 9.75 9.75 9.75 9.75"));
            list.Add(string.Format("Td 0.225 0.225 0.225 0.225 0.225 0.225"));
            list.Add(string.Format("C1 1.925 1.925 1.925 1.925 1.925 1.925"));
            list.Add(string.Format("C2 0 0 0 0 0 0"));
            list.Add(string.Format("Tip 0.2 0.2 0.2 0.2 0.2 0.2"));
            list.Add(string.Format("Tf 0.3 0.3 0.3 0.3 0.3 0.3"));
            list.Add(string.Format("Iw 0.7 0.7 0.7 0.7 0.7 0.7"));
            list.Add(string.Format("D1 2.2 2.2 2.2 2.2 2.2 2.2"));
            list.Add(string.Format("Tw 0.6 0.579 0.48 0.31 0.31 0.31"));
            list.Add(string.Format("SW 4.5 4.5 4.5 4.5 4.5 4.5"));
            list.Add(string.Format("Ts 0.55 0.26 0.26 0.26 0.26 0.26"));
            list.Add(string.Format("D2 0 0 0 0 0 0"));
            list.Add(string.Format("K1 0 0 0 0 0 0"));
            list.Add(string.Format("K2 0.175 0.0827 0.0827 0.0827 0.0827 0.0827"));
            list.Add(string.Format("HW1 0.409 0.485 0.582 0.75 0.75 0.75"));
            list.Add(string.Format("HH1 0.083 0.097 0.116 0.15 0.15 0.15"));
            list.Add(string.Format("CH1 1.85 1.85 1.85 1.85 1.85 1.85"));
            list.Add(string.Format("HW2 0 0 0 0 0 0"));
            list.Add(string.Format("HH2 0 0 0 0 0 0"));
            list.Add(string.Format("HW3 0 0 0.088 0.3 0.3 0.3"));
            list.Add(string.Format("HH3 0 0 0.044 0.15 0.15 0.15"));


            MyList mlist = null;


            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ' ');
                dgv_seg_tab3.Rows.Add(mlist.StringList.ToArray());
            }

            Set_Segment_Data();

        }

        private void Set_Segment_Data()
        {


            for (int i = 0; i < dgv_seg_tab3.Rows.Count; i++)
            {

                if (i == 0)
                    dgv_seg_tab3.Rows[i].Height = 26;
                else if (i > 13)
                    dgv_seg_tab3.Rows[i].Height = 16;
                else
                    dgv_seg_tab3.Rows[i].Height = 15;

                if (dgv_seg_tab3[0, i].Value.ToString() == "D2" ||
                    dgv_seg_tab3[0, i].Value.ToString() == "K1" ||
                    dgv_seg_tab3[0, i].Value.ToString() == "K2")
                {
                    dgv_seg_tab3.Rows[i].ReadOnly = true;
                    dgv_seg_tab3.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    dgv_seg_tab3.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
        }
        private void dgv_seg_tab3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Update_Tab3_Data();
        }

        public void Update_Tab3_Data()
        {
            try
            {
                double theta = 0.0;
                double D2 = 0.0;
                double K1 = 0.0;
                double K2 = 0.0;
                double va = 5;
                for (int i = 1; i < dgv_seg_tab3.ColumnCount; i++)
                {
                    theta = Math.Atan((MyList.StringToDouble(dgv_seg_tab3[i, 7].Value.ToString(), 0.0) /
                            MyList.StringToDouble(dgv_seg_tab3[i, 8].Value.ToString(), 0.0)));

                    va = (180.0 / Math.PI) * theta;

                    //Web Inclination = θ (degrees) = atan (Iw / D1) = atan (0.7 / 2.200) = atan(0.3182) = 17.650
                    //and  D2 = D – Tf – D1 = 2.500 – 0.300 – 2.200 = 0.0
                    D2 = (MyList.StringToDouble(dgv_seg_tab3[i, 0].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3[i, 6].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3[i, 8].Value.ToString(), 0.0));

                    K1 = D2 * Math.Tan(theta);
                    K2 = MyList.StringToDouble(dgv_seg_tab3[i, 11].Value.ToString(), 0.0) * Math.Tan(theta);


                    dgv_seg_tab3[i, 12].Value = D2.ToString("f3");
                    dgv_seg_tab3[i, 13].Value = K1.ToString("f3");
                    dgv_seg_tab3[i, 14].Value = K2.ToString("f3");
                    //K1 = D2 x tan(θ) = 0.0 x tan(17.6501) = 0.0
                    //K2 = Ts x tan(θ) = 0.550 x tan(17.6501) = 0.1750
                    theta = (180.0 / Math.PI) * theta;
                    if (i == 1) txt_tab3_support.Text = theta.ToString("f3");
                    if (i == 2) txt_tab3_d.Text = theta.ToString("f3");
                    if (i == 3) txt_tab3_L_8.Text = theta.ToString("f3");
                    if (i == 4) txt_tab3_L_4.Text = theta.ToString("f3");
                    if (i == 5) txt_tab3_3L_8.Text = theta.ToString("f3");
                    if (i == 6) txt_tab3_L_2.Text = theta.ToString("f3");



                    //txt_tab1_ds.Text = dgv_seg_tab3[i, 2].Value.ToString();

                }
            }
            catch (Exception ex) { }
        }

        public string Get_Project_Folder()
        {
            return iApp.user_path;
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

        private void UC_BoxGirder_Load(object sender, EventArgs e)
        {
            if (dgv_user_input.RowCount == 0)
                Load_Data();
        }

        private void btn_psc_box_Click(object sender, EventArgs e)
        {
            Design_BS_PSC_Box_Girder_Bridge();
        }

        private void tab_Prestressed_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_open_design)
            {
                string file_path = Get_Project_Folder();
                file_path = Path.Combine(file_path, "Design Of PSC Box Girder Bridge.xlsx");
                if (File.Exists(file_path)) iApp.OpenExcelFile(file_path, "2011ap");
            }
            else
            {

                iApp.Open_WorkSheet_Design();
            }
        }

      
    }
}
