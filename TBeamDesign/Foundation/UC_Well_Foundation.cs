using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
//using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.PSC_I_Girder;


//using LimitStateMethod.RCC_T_Girder;
//using LimitStateMethod.LS_Progress;
//using LimitStateMethod.DeckSlab;


using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;




namespace BridgeAnalysisDesign.Foundation
{
    public partial class UC_Well_Foundation : UserControl
    {
        public event EventHandler OnProcees;
        public IApplication iApp;

        public UC_Well_Foundation()
        {
            InitializeComponent();
            pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.Rcc_Well_Foundation1;
            Project_Name = "";
            user_path = "";
        }
        public void Load_Data()
        {
            #region SITE DATA

            List<string> list = new List<string>();

            list.Add(string.Format("SITE DATA$$"));

            list.Add(string.Format("Span of Bridge$70$m"));
            list.Add(string.Format("Deck Level$98.5$m"));
            list.Add(string.Format("HFL $80.67$m"));
            list.Add(string.Format("LWL $78.37$m"));
            list.Add(string.Format("Well Cap Bottom Level$89.8$m"));
            #endregion SITE DATA

            #region BACKFILL SOIL PROPERTIES

            list.Add(string.Format("BACKFILL SOIL PROPERTIES$$"));

            list.Add(string.Format("Ka (Active Earth Pressure Coefficient) $0.297$"));
            list.Add(string.Format("Bulk Density of soil $2$t/cum."));
            list.Add(string.Format("Submerged Density of  Soil$1$t/cu.m."));
            list.Add(string.Format("Angle of internal friction$30$degree"));
            list.Add(string.Format("Angle of wall friction$21$degree"));

            #endregion BACKFILL SOIL PROPERTIES

            #region DESIGN DATA

            list.Add(string.Format("DESIGN DATA$$"));

            list.Add(string.Format("Width of Well Cap$7$m"));
            list.Add(string.Format("Length of abutment$13$m"));
            list.Add(string.Format("Length of Abutment Cap$14$m"));
            list.Add(string.Format("Abutment Cap top Level$97.000$m"));
            list.Add(string.Format("Abutment Cap bottom Level$96.000$m"));
            list.Add(string.Format("Thickness of dirt wall$0.3$m"));
            list.Add(string.Format("Thickness  of Well Cap$1.8$m"));
            list.Add(string.Format("Thickness of  abutment shaft$1.5$m"));
            list.Add(string.Format("Thickness of side wall$0.4$m"));
            list.Add(string.Format("Seismic Zone $V$"));
            list.Add(string.Format("Seismic Coefficient $0.27$"));
            list.Add(string.Format("Overhang of Superstructure from C/L of bearing =$0.6$m"));
            list.Add(string.Format("Expansion Gap$0.05$m"));
            list.Add(string.Format("Length of approach slab$4$m"));
            list.Add(string.Format("Thickness of bearing beam (Rect.)$0.6$m"));
            list.Add(string.Format("Thickness of bearing beam (Trapz.)$0.4$m"));
            list.Add(string.Format("Abutment cap width$1.9$m"));
            list.Add(string.Format("Max. Scour Level$89.8$m"));
            list.Add(string.Format("Top Level of earthfilling in well$89.5$m"));
            list.Add(string.Format("Average Ground Level$89.8$m"));
            list.Add(string.Format("Thickness of bearings inclluding pedestals$0.5$m"));
            list.Add(string.Format("Outer Dia of Well$7$m"));
            list.Add(string.Format("Internal dia of well$5$m"));
            list.Add(string.Format("Thickness of Top Plug$0.3$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Dia of Well Curb$7.15$m"));
            list.Add(string.Format("Foundation Level$72$m"));
            list.Add(string.Format("Thickness of steining$1$m"));

            #endregion DESIGN DATA

            #region DL SUPERSTRUCTURE


            //list.Add(string.Format("Dead Load calculation for RCC Deck Slab $ $ "));
            list.Add(string.Format("DL SUPERSTRUCTURE$$"));


            list.Add(string.Format("c/c Length Of the span$70.000 $m"));
            list.Add(string.Format("Total Width of Superstructure$13.000 $m"));
            list.Add(string.Format("Width of Carriageway$7.500 $m"));
            list.Add(string.Format("Projection beyond cL of bearing $0.600 $m"));
            list.Add(string.Format("Expansion gap$0.050 $m"));
            list.Add(string.Format("Av. thickness of deck slab    $0.250 $m"));
            list.Add(string.Format("Thickness of wearing coat$0.075 $m"));
            list.Add(string.Format("Width of footpath$1.500 $m"));
            list.Add(string.Format("Width of member$0.5$m"));
            list.Add(string.Format("Width of crash barrier$0.45$m"));
            list.Add(string.Format("Width of railing kerb$0.3$m"));
            list.Add(string.Format("Total Dead load of steel superstructure$225.00$t"));



            #endregion DL SUPERSTRUCTURE

            #region SIDL DATA


            //list.Add(string.Format("Dead Load calculation for RCC Deck Slab $ $ "));
            list.Add(string.Format("SIDL DATA$$"));


            list.Add(string.Format("Crash barrier$0.700X2$t/m"));
            list.Add(string.Format("Weight of Railing$0.300X2$t/m"));
            list.Add(string.Format("Service Load$0.750X2$t/m"));
            list.Add(string.Format("Railing kerb$0.300X0.300X2.5X2$t/m"));

           //Crash barrier = 0.700 x Nos. = 2
//Wearing Coat = OK (To be checked)
//Weight of Railing = 0.300 x Nos. = 2
//Service load = 0.750 x Nos. = 2
//Railing kerb = 0.300 x 0.300


            #endregion SIDL DATA

            #region WELL CAP DATA


            list.Add(string.Format("WELL CAP DATA$$"));


            list.Add(string.Format("Grade of Concrete$M35$"));
            list.Add(string.Format("Grade of Steel$Fe 500$"));

            list.Add(string.Format("Permissible compressive stress of concrete$11.5$N/mm^2"));
            list.Add(string.Format("Permissible tensile stress of steel$240$N/mm^2"));
            list.Add(string.Format("Modular Ratio$10$"));


            list.Add(string.Format("Bottom Bar Diameter (Long.)$28$mm"));
            list.Add(string.Format("Bottom Bar Spacing (Long.)$100$mm"));

            list.Add(string.Format("Top Bar Diameter (Long.)$20$mm"));
            list.Add(string.Format("Top Bar Spacing (Long.)$150$mm"));



            list.Add(string.Format("Bottom Bar Diameter (Trans.)$20$mm"));
            list.Add(string.Format("Bottom Bar Spacing (Trans.)$140$mm"));

            list.Add(string.Format("Top Bar Diameter (Trans.)$16$mm"));
            list.Add(string.Format("Top Bar Spacing (Trans.)$200$mm"));

            list.Add(string.Format("Top Bar Diameter (Cantilever)$28$mm"));
            list.Add(string.Format("Top Bar Spacing (Cantilever)$100$mm"));

            #endregion WELL CAP DATA

            #region STEINING DATA

            list.Add(string.Format("STEINING DATA$$"));

            //Hoop bars: Diameter 10 mm.  Spacing 250 mm.


            list.Add(string.Format("Hoop bar Diameter$10$mm"));
            list.Add(string.Format("Hoop bar Spacing$250$mm"));


            #endregion STEINING DATA

            #region  ABUTMENT SHAFT

            list.Add(string.Format(" ABUTMENT SHAFT DATA$$"));

            //Hoop bars: Diameter 10 mm.  Spacing 250 mm.


            list.Add(string.Format("Dia. of bars on Compression side (Longer Face)$1.6$cm"));
            list.Add(string.Format("No. of bars on Compression side (Longer Face)$102$nos"));

            list.Add(string.Format("Dia. of bars on Tension Side (Longer Face)$2.8$cm"));
            list.Add(string.Format("No. of bars on Compression side (Longer Face)$110$nos"));


            list.Add(string.Format("Dia. of bars on Tension Side (Shorter Face)$1.6$cm"));
            list.Add(string.Format("No. of bars on Compression side (Shorter Face)$6$nos"));

            list.Add(string.Format("Dia. of bars on Compression side (Shorter Face)$1.6$cm"));
            list.Add(string.Format("No. of bars on Compression side (Shorter Face)$6$nos"));


            #endregion  ABUTMENT SHAFT DATA

            #region SIDEWALL DATA

            list.Add(string.Format("SIDEWALL DATA$$"));

            //Hoop bars: Diameter 10 mm.  Spacing 250 mm.


            list.Add(string.Format("Assuming Cap thickness$300$mm"));
            list.Add(string.Format("Longitudnal Clear Cover$40$mm"));


            list.Add(string.Format("Nos. of Longitudnal steel bar$14$nos"));
            list.Add(string.Format("Dia. of Longitudnal steel bar$20$mm"));

            list.Add(string.Format("Dia. of Transverse steel bar$12$mm"));
            list.Add(string.Format("Transverse Clear Cover$40$mm"));

            list.Add(string.Format("Spacing of Stirrups$150$mm"));



            list.Add(string.Format("Earth face horizontally Distribution steel bar dia$20$mm"));
            list.Add(string.Format("Earth face horizontally Distribution steel bar spacing$90$mm"));


            list.Add(string.Format("Earth face vertically Distribution steel bar dia$12$mm"));
            list.Add(string.Format("Earth face vertically Distribution steel bar spacing$200$mm"));


            list.Add(string.Format("Outer face vertically Distribution steel bar dia$12$mm"));
            list.Add(string.Format("Outer face vertically Distribution steel bar spacing$200$mm"));

            list.Add(string.Format("Outer face horizontally Distribution steel bar dia$10$mm"));
            list.Add(string.Format("Outer face horizontally Distribution steel bar spacing$250$mm"));




            list.Add(string.Format("Cantilever return wall Top steel bar dia$12$mm"));
            list.Add(string.Format("Cantilever return wall Top steel bar Spacing$120$mm"));


            list.Add(string.Format("Cantilever return wall Bottom steel bar dia$10$mm"));
            list.Add(string.Format("Cantilever return wall Bottom steel bar Spacing$200$mm"));




            #endregion WELL CURB DATA

            #region WELL CURB DATA

            list.Add(string.Format("WELL CURB DATA$$"));


            list.Add(string.Format("Min. reinforcement in terms of wt.$72$kg/cu.m"));

            list.Add(string.Format("Nos. of Top bar$14$nos"));
            list.Add(string.Format("Top bar Diameter$25$mm"));


            list.Add(string.Format("Nos. of Bottom bar$92$nos"));
            list.Add(string.Format("Bottom bar Diameter$16$mm"));

            list.Add(string.Format("Nos. of Links$12$nos"));
            list.Add(string.Format("Links Diameter$12$mm"));

            list.Add(string.Format("Clear Cover$75$mm"));

            list.Add(string.Format("Unit wt. of steel$7850.0$kg/cu.m"));

            #endregion WELL CURB DATA

            btn_process_new.Enabled = true;

            MyList.Fill_List_to_Grid(dgv_user_input, list, '$');
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                txt_LL_title1.Text = "Moving Wheel Load Data 1";
                txt_LL_title2.Text = "Moving Wheel Load Data 2";
                txt_LL_title3.Text = "Moving Tracked Load Data";
            }
            Modified_Cell();
        }

        public void Modified_Cell()
        {

            #region Modified Cell
            string s1, s2, s3, s4;
            int sl_no = 1;


            DataGridView dgv = dgv_user_input;




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
                    //if (s1 != "" && s2 == "")
                    if (s3 == "" && s2 == "")
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
            #endregion Modified cells
           btn_open_report.Enabled = File.Exists(excel_file);
        }

        private void Design_Well_Foundation()
        {

            string file_path = excel_file;

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");

            if(iApp.DesignStandard == eDesignStandard.IndianStandard)
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Well Foundation\Design of Well foundation_IRC_45.xlsx");
            else
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Well Foundation\Design of Well foundation_BS.xlsx");


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
            Excel.Worksheet des_data = (Excel.Worksheet)myExcelWorkbook.Sheets["Design data"];
            Excel.Worksheet dl_supp = (Excel.Worksheet)myExcelWorkbook.Sheets["DL SUP"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            DataInputCollections dinp = new DataInputCollections();

            dinp.Load_Data_from_Grid(dgv_user_input);


            List<string> list = new List<string>();


            DataGridView dgv = dgv_user_input;
            int rindx = 0;


            rindx = 0;

            #region Input

            MyList mlist = null;
            int col;


            Excel.Worksheet curr_sheet = des_data;

            //dinp.Headings


            #region Chiranjit [2016 07 04]

            #region Site Data

            List<DataInput> dta = dinp.Get_Data(0);


            int i = 0;


            col = 2;

            for (i = 0; i < dta.Count; i++)
            {
                curr_sheet.get_Range("C" + (col++)).Formula = dta[i].DATA.ToString();
            }



            dta = dinp.Get_Data(1);



            col = 8;

            for (i = 0; i < dta.Count; i++)
            {
                curr_sheet.get_Range("C" + (col++)).Formula = dta[i].DATA.ToString();
            }


            dta = dinp.Get_Data(2);

            col = 13;

            for (i = 0; i < dta.Count; i++)
            {
                curr_sheet.get_Range("C" + (col++)).Formula = dta[i].DATA.ToString();
                if (col == 37) col++;
            }






            #endregion Site Data


            #region DL SUP


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DL SUP"];


            dta = dinp.Get_Data(3);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                curr_sheet.get_Range("H" + (col++)).Formula = dta[i].DATA.ToString();
                if(i == 11)
                {
                    curr_sheet.get_Range("H" + (17)).Formula = dta[i].DATA.ToString();
                    break;

                }
            }


            dta = dinp.Get_Data(4);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                mlist = new MyList(dta[i].DATA, 'X');
                if (mlist.Count >= 2)
                {
                    if (i == 0)
                    {
                        curr_sheet.get_Range("E55").Formula = mlist[0].ToString();
                        curr_sheet.get_Range("F55").Formula = mlist[1].ToString();
                    }
                    else if (i == 1)
                    {
                        curr_sheet.get_Range("E57").Formula = mlist[0].ToString();
                        curr_sheet.get_Range("F57").Formula = mlist[1].ToString();
                    }
                    else if (i == 2)
                    {
                        curr_sheet.get_Range("E58").Formula = mlist[0].ToString();
                        curr_sheet.get_Range("F58").Formula = mlist[1].ToString();
                    }
                    else if (i == 3)
                    {
                        curr_sheet.get_Range("E59").Formula = mlist[0].ToString();
                        curr_sheet.get_Range("F59").Formula = mlist[1].ToString();
                    }
                }
            }


            #endregion DL SUP



            #region Well Cap


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Well Cap"];


            dta = dinp.Get_Data(5);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                if (i == 0)
                {
                    curr_sheet.get_Range("D262").Formula = dta[i].DATA.ToString();
                }
                else if (i == 1)
                {
                    curr_sheet.get_Range("D263").Formula = dta[i].DATA.ToString();
                }
                else if (i == 2)
                {
                    curr_sheet.get_Range("F264").Formula = dta[i].DATA.ToString();
                }
                else if (i == 3)
                {
                    curr_sheet.get_Range("F265").Formula = dta[i].DATA.ToString();
                }
                else if (i == 4)
                {
                    curr_sheet.get_Range("F266").Formula = dta[i].DATA.ToString();
                }
                else if (i == 5)
                {
                    curr_sheet.get_Range("B292").Formula = dta[i].DATA.ToString();
                }
                else if (i == 6)
                {
                    curr_sheet.get_Range("D292").Formula = dta[i].DATA.ToString();
                }
                else if (i == 7)
                {
                    curr_sheet.get_Range("B299").Formula = dta[i].DATA.ToString();
                }
                else if (i == 8)
                {
                    curr_sheet.get_Range("D299").Formula = dta[i].DATA.ToString();
                }


                else if (i == 9)
                {
                    curr_sheet.get_Range("B315").Formula = dta[i].DATA.ToString();
                }
                else if (i == 10)
                {
                    curr_sheet.get_Range("D315").Formula = dta[i].DATA.ToString();
                }



                else if (i == 11)
                {
                    curr_sheet.get_Range("B322").Formula = dta[i].DATA.ToString();
                }
                else if (i == 12)
                {
                    curr_sheet.get_Range("D322").Formula = dta[i].DATA.ToString();
                }

                else if (i == 13)
                {
                    curr_sheet.get_Range("B387").Formula = dta[i].DATA.ToString();
                }
                else if (i == 14)
                {
                    curr_sheet.get_Range("D387").Formula = dta[i].DATA.ToString();
                }
            }



            #endregion Well Cap


            #region STEINING


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["STEINING"];


            dta = dinp.Get_Data(6);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                if (i == 0)
                {
                    curr_sheet.get_Range("C254").Formula = dta[i].DATA.ToString();
                }
                else if (i == 1)
                {
                    curr_sheet.get_Range("E261").Formula = dta[i].DATA.ToString();
                } 
            }



            #endregion STEINING


            #region ABTSHAFT


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["ABTSHAFT"];
            
            
            dta = dinp.Get_Data(7);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                if (i == 0)
                {
                    curr_sheet.get_Range("F26").Formula = dta[i].DATA.ToString();
                }
                else if (i == 1)
                {
                    curr_sheet.get_Range("F27").Formula = dta[i].DATA.ToString();
                }
                else if (i == 2)
                {
                    curr_sheet.get_Range("F29").Formula = dta[i].DATA.ToString();
                }
                else if (i == 3)
                {
                    curr_sheet.get_Range("F30").Formula = dta[i].DATA.ToString();
                }
                else if (i == 4)
                {
                    curr_sheet.get_Range("F34").Formula = dta[i].DATA.ToString();
                }
                else if (i == 5)
                {
                    curr_sheet.get_Range("F35").Formula = dta[i].DATA.ToString();
                }
                else if (i == 6)
                {
                    curr_sheet.get_Range("F37").Formula = dta[i].DATA.ToString();
                }
                else if (i == 7)
                {
                    curr_sheet.get_Range("F38").Formula = dta[i].DATA.ToString();
                }
            }



            #endregion ABTSHAFT


            #region SIDEWALL


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SIDEWALL"];


            dta = dinp.Get_Data(8);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                if (i == 0)
                {
                    curr_sheet.get_Range("D6").Formula = dta[i].DATA.ToString();
                }
                else if (i == 1)
                {
                    curr_sheet.get_Range("F27").Formula = dta[i].DATA.ToString();
                }
                else if (i == 2)
                {
                    curr_sheet.get_Range("B34").Formula = dta[i].DATA.ToString();
                }
                else if (i == 3)
                {
                    curr_sheet.get_Range("D34").Formula = dta[i].DATA.ToString();
                }
                else if (i == 4)
                {
                    curr_sheet.get_Range("B43").Formula = dta[i].DATA.ToString();
                }
                else if (i == 5)
                {
                    curr_sheet.get_Range("F43").Formula = dta[i].DATA.ToString();
                }
                else if (i == 6)
                {
                    curr_sheet.get_Range("E55").Formula = dta[i].DATA.ToString();
                }
                else if (i == 7)
                {
                    curr_sheet.get_Range("B149").Formula = dta[i].DATA.ToString();
                }
                else if (i == 8)
                {
                    curr_sheet.get_Range("E149").Formula = dta[i].DATA.ToString();
                }

                else if (i == 9)
                {
                    curr_sheet.get_Range("B156").Formula = dta[i].DATA.ToString();
                }

                else if (i == 10)
                {
                    curr_sheet.get_Range("E156").Formula = dta[i].DATA.ToString();
                }
                else if (i == 11)
                {
                    curr_sheet.get_Range("B157").Formula = dta[i].DATA.ToString();
                }
                else if (i == 12)
                {
                    curr_sheet.get_Range("E157").Formula = dta[i].DATA.ToString();
                }
                else if (i == 13)
                {
                    curr_sheet.get_Range("B158").Formula = dta[i].DATA.ToString();
                }
                else if (i == 14)
                {
                    curr_sheet.get_Range("E158").Formula = dta[i].DATA.ToString();
                }
                else if (i == 15)
                {
                    curr_sheet.get_Range("B176").Formula = dta[i].DATA.ToString();
                }
                else if (i == 16)
                {
                    curr_sheet.get_Range("D176").Formula = dta[i].DATA.ToString();
                }
                else if (i == 17)
                {
                    curr_sheet.get_Range("B179").Formula = dta[i].DATA.ToString();
                }
                else if (i == 18)
                {
                    curr_sheet.get_Range("D179").Formula = dta[i].DATA.ToString();
                }
            }



            #endregion SIDEWALL


            #region SIDEWALL


            curr_sheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Well Curb"];


            dta = dinp.Get_Data(9);

            col = 3;

            for (i = 0; i < dta.Count; i++)
            {
                if (i == 0)
                {
                    curr_sheet.get_Range("F11").Formula = dta[i].DATA.ToString();
                }
                else if (i == 1)
                {
                    curr_sheet.get_Range("C13").Formula = dta[i].DATA.ToString();
                }
                else if (i == 2)
                {
                    curr_sheet.get_Range("F13").Formula = dta[i].DATA.ToString();
                }
                else if (i == 3)
                {
                    curr_sheet.get_Range("C14").Formula = dta[i].DATA.ToString();
                }
                else if (i == 4)
                {
                    curr_sheet.get_Range("F14").Formula = dta[i].DATA.ToString();
                }
                else if (i == 5)
                {
                    curr_sheet.get_Range("C15").Formula = dta[i].DATA.ToString();
                }
                else if (i == 6)
                {
                    curr_sheet.get_Range("F15").Formula = dta[i].DATA.ToString();
                }
                else if (i == 7)
                {
                    curr_sheet.get_Range("D16").Formula = dta[i].DATA.ToString();
                }
                else if (i == 8)
                {
                    curr_sheet.get_Range("D17").Formula = dta[i].DATA.ToString();
                }
            }



            #endregion Well Curb


            #endregion Chiranjit [2016 07 04]

            #endregion Input

            #region Live Loads

            Excel.Worksheet LL = (Excel.Worksheet)myExcelWorkbook.Sheets["LL"];


            List<string> axle_loads = new List<string>();
            List<string> axle_dists = new List<string>();
            int indx = 0;

            MyList ml;

            string kStr = "";



            #region Type 1

            LL.get_Range("A4").Formula = txt_LL_title1.Text + ", 2 Lane";

            #region Loads

            kStr = txt_LL_axle_loads1.Text.Replace(",", " ");
            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_loads.AddRange(ml.StringList);


            if (axle_loads.Count < 8)
            {
                for ( i = axle_loads.Count; i <= 8; i++)
                {
                    axle_loads.Add("0.0");
                }
            }
            else
            {
                while (axle_loads.Count > 8) axle_loads.RemoveAt(axle_loads.Count - 1);
            }




            LL.get_Range("B7").Formula = axle_loads[indx++];
            LL.get_Range("C7").Formula = axle_loads[indx++];
            LL.get_Range("D7").Formula = axle_loads[indx++];
            LL.get_Range("E7").Formula = axle_loads[indx++];
            LL.get_Range("F7").Formula = axle_loads[indx++];
            LL.get_Range("G7").Formula = axle_loads[indx++];
            LL.get_Range("H7").Formula = axle_loads[indx++];
            LL.get_Range("I7").Formula = axle_loads[indx++];

            indx = 0;
            LL.get_Range("J7").Formula = axle_loads[indx++];
            LL.get_Range("K7").Formula = axle_loads[indx++];
            LL.get_Range("L7").Formula = axle_loads[indx++];
            LL.get_Range("M7").Formula = axle_loads[indx++];
            LL.get_Range("N7").Formula = axle_loads[indx++];
            LL.get_Range("O7").Formula = axle_loads[indx++];

            #endregion Loads


            #region Distances

            kStr = txt_LL_load_distances1.Text.Replace(",", " ");

            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_dists.AddRange(ml.StringList);





            if (axle_dists.Count < 7)
            {
                for ( i = axle_dists.Count; i <= 7; i++)
                {
                    axle_dists.Add("0.0");
                }
            }
            else
            {
                while (axle_dists.Count > 7) axle_dists.RemoveAt(axle_loads.Count - 1);
            }

            indx = 0;
            LL.get_Range("B9").Formula = axle_dists[indx++];
            LL.get_Range("C9").Formula = axle_dists[indx++];
            LL.get_Range("D9").Formula = axle_dists[indx++];
            LL.get_Range("E9").Formula = axle_dists[indx++];
            LL.get_Range("F9").Formula = axle_dists[indx++];
            LL.get_Range("G9").Formula = axle_dists[indx++];
            LL.get_Range("H9").Formula = axle_dists[indx++];



            LL.get_Range("I9").Formula = txt_LL_separate_distance1.Text;

            indx = 0;

            LL.get_Range("J9").Formula = axle_dists[indx++];
            LL.get_Range("K9").Formula = axle_dists[indx++];
            LL.get_Range("L9").Formula = axle_dists[indx++];
            LL.get_Range("M9").Formula = axle_dists[indx++];
            LL.get_Range("N9").Formula = axle_dists[indx++];



            #endregion Distances


            LL.get_Range("A42").Formula = txt_LL_title1.Text + ", 1 Lane";


            #region Loads

             
            indx = 0;

            LL.get_Range("H43").Formula = axle_loads[indx++];
            LL.get_Range("I43").Formula = axle_loads[indx++];

            LL.get_Range("J43").Formula = axle_loads[indx++];
            LL.get_Range("K43").Formula = axle_loads[indx++];
            LL.get_Range("L43").Formula = axle_loads[indx++];
            LL.get_Range("M43").Formula = axle_loads[indx++];


            #endregion Loads


            #region Distances

            indx = 0;

            LL.get_Range("H45").Formula = axle_dists[indx++];
            LL.get_Range("I45").Formula = axle_dists[indx++]; 
            LL.get_Range("J45").Formula = axle_loads[indx++];
            LL.get_Range("K45").Formula = axle_loads[indx++];
            LL.get_Range("L45").Formula = axle_loads[indx++];

            #endregion Distances

            #endregion Type 1



            #region Type 2



            LL.get_Range("A75").Formula = txt_LL_title2.Text;


            #region Loads

            kStr = txt_LL_axle_loads2.Text.Replace(",", " ");
            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_loads.Clear();

            axle_loads.AddRange(ml.StringList);

            indx = 0;



            if (axle_loads.Count < 7)
            {
                for ( i = axle_loads.Count; i <= 7; i++)
                {
                    axle_loads.Add("0.0");
                }
            }
            else
            {
                while (axle_loads.Count > 7) axle_loads.RemoveAt(axle_loads.Count - 1);
            }



            LL.get_Range("B77").Formula = axle_loads[indx++];
            LL.get_Range("C77").Formula = axle_loads[indx++];
            LL.get_Range("D77").Formula = axle_loads[indx++];
            LL.get_Range("E77").Formula = axle_loads[indx++];
            LL.get_Range("F77").Formula = axle_loads[indx++];
            LL.get_Range("G77").Formula = axle_loads[indx++];
            LL.get_Range("H77").Formula = axle_loads[indx++];

            indx = 0;
            LL.get_Range("J77").Formula = axle_loads[indx++];
            LL.get_Range("K77").Formula = axle_loads[indx++];
            LL.get_Range("L77").Formula = axle_loads[indx++];
            LL.get_Range("M77").Formula = axle_loads[indx++];
            LL.get_Range("N77").Formula = axle_loads[indx++];
            LL.get_Range("O77").Formula = axle_loads[indx++];
            LL.get_Range("P77").Formula = axle_loads[indx++];

            #endregion Loads


            #region Distances

            kStr = txt_LL_load_distances2.Text.Replace(",", " ");

            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_dists.Clear();
            axle_dists.AddRange(ml.StringList);


            indx = 0;




            if (axle_dists.Count < 6)
            {
                for ( i = axle_dists.Count; i <= 6; i++)
                {
                    axle_dists.Add("0.0");
                }
            }
            else
            {
                while (axle_dists.Count > 6) axle_dists.RemoveAt(axle_loads.Count - 1);
            }

            LL.get_Range("B79").Formula = axle_dists[indx++];
            LL.get_Range("C79").Formula = axle_dists[indx++];
            LL.get_Range("D79").Formula = axle_dists[indx++];
            LL.get_Range("E79").Formula = axle_dists[indx++];
            LL.get_Range("F79").Formula = axle_dists[indx++];
            //LL.get_Range("G79").Formula = axle_dists[indx++];
            LL.get_Range("H79").Formula = axle_dists[indx++];



            LL.get_Range("I79").Formula = txt_LL_separate_distance2.Text;

            indx = 0;

            LL.get_Range("J79").Formula = axle_dists[indx++];
            LL.get_Range("K79").Formula = axle_dists[indx++];
            LL.get_Range("L79").Formula = axle_dists[indx++];
            LL.get_Range("M79").Formula = axle_dists[indx++];
            LL.get_Range("N79").Formula = axle_dists[indx++];
            //LL.get_Range("O79").Formula = axle_dists[indx++];
            LL.get_Range("P79").Formula = axle_dists[indx++];



            #endregion Distances



            #endregion Type 2



            #region Type 3


            LL.get_Range("A108").Formula = txt_LL_title3.Text;

            LL.get_Range("C109").Formula = txt_LL_load_distances3.Text;
            LL.get_Range("E110").Formula = txt_LL_separate_distance3.Text;

            LL.get_Range("D116").Formula = "=2*" + txt_LL_axle_loads3.Text;
            LL.get_Range("F118").Formula = txt_LL_axle_loads3.Text;


            #endregion Type 3

            #endregion Live Loads



            myExcelWorkbook.Save();

            btn_open_report.Enabled = true;
            
            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void Design_Well_Foundation_2016_07_04()
        {

            string file_path = excel_file;

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Well Foundation\Design of Well foundation_IRC_45.xlsx");
            else
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Well Foundation\Design of Well foundation_BS.xlsx");


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
            Excel.Worksheet des_data = (Excel.Worksheet)myExcelWorkbook.Sheets["Design data"];
            Excel.Worksheet dl_supp = (Excel.Worksheet)myExcelWorkbook.Sheets["DL SUP"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();


            DataGridView dgv = dgv_user_input;
            int rindx = 0;


            rindx = 0;

            #region Input

            MyList mlist = null;
            int col, rw;

            col = 2;

            Excel.Worksheet curr_sheet = des_data;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i <= 40)
                {
                    des_data.get_Range("C" + (col++)).Formula = dgv[1, i].Value.ToString();
                }
                else if (i > 40)
                {
                    dl_supp.get_Range("H" + (col++)).Formula = dgv[1, i].Value.ToString();
                }


                if (i == 41)
                    col = 3;

            }

            #endregion Input

            #region Live Loads

            Excel.Worksheet LL = (Excel.Worksheet)myExcelWorkbook.Sheets["LL"];


            List<string> axle_loads = new List<string>();
            List<string> axle_dists = new List<string>();
            int indx = 0;

            MyList ml;

            string kStr = "";








            #region Type 1

            LL.get_Range("A4").Formula = txt_LL_title1.Text + ", 2 Lane";

            #region Loads

            kStr = txt_LL_axle_loads1.Text.Replace(",", " ");
            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_loads.AddRange(ml.StringList);


            if (axle_loads.Count < 8)
            {
                for (int i = axle_loads.Count; i <= 8; i++)
                {
                    axle_loads.Add("0.0");
                }
            }
            else
            {
                while (axle_loads.Count > 8) axle_loads.RemoveAt(axle_loads.Count - 1);
            }




            LL.get_Range("B7").Formula = axle_loads[indx++];
            LL.get_Range("C7").Formula = axle_loads[indx++];
            LL.get_Range("D7").Formula = axle_loads[indx++];
            LL.get_Range("E7").Formula = axle_loads[indx++];
            LL.get_Range("F7").Formula = axle_loads[indx++];
            LL.get_Range("G7").Formula = axle_loads[indx++];
            LL.get_Range("H7").Formula = axle_loads[indx++];
            LL.get_Range("I7").Formula = axle_loads[indx++];

            indx = 0;
            LL.get_Range("J7").Formula = axle_loads[indx++];
            LL.get_Range("K7").Formula = axle_loads[indx++];
            LL.get_Range("L7").Formula = axle_loads[indx++];
            LL.get_Range("M7").Formula = axle_loads[indx++];
            LL.get_Range("N7").Formula = axle_loads[indx++];
            LL.get_Range("O7").Formula = axle_loads[indx++];

            #endregion Loads


            #region Distances

            kStr = txt_LL_load_distances1.Text.Replace(",", " ");

            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_dists.AddRange(ml.StringList);





            if (axle_dists.Count < 7)
            {
                for (int i = axle_dists.Count; i <= 7; i++)
                {
                    axle_dists.Add("0.0");
                }
            }
            else
            {
                while (axle_dists.Count > 7) axle_dists.RemoveAt(axle_loads.Count - 1);
            }

            indx = 0;
            LL.get_Range("B9").Formula = axle_dists[indx++];
            LL.get_Range("C9").Formula = axle_dists[indx++];
            LL.get_Range("D9").Formula = axle_dists[indx++];
            LL.get_Range("E9").Formula = axle_dists[indx++];
            LL.get_Range("F9").Formula = axle_dists[indx++];
            LL.get_Range("G9").Formula = axle_dists[indx++];
            LL.get_Range("H9").Formula = axle_dists[indx++];



            LL.get_Range("I9").Formula = txt_LL_separate_distance1.Text;

            indx = 0;

            LL.get_Range("J9").Formula = axle_dists[indx++];
            LL.get_Range("K9").Formula = axle_dists[indx++];
            LL.get_Range("L9").Formula = axle_dists[indx++];
            LL.get_Range("M9").Formula = axle_dists[indx++];
            LL.get_Range("N9").Formula = axle_dists[indx++];



            #endregion Distances


            LL.get_Range("A42").Formula = txt_LL_title1.Text + ", 1 Lane";


            #region Loads


            indx = 0;

            LL.get_Range("H43").Formula = axle_loads[indx++];
            LL.get_Range("I43").Formula = axle_loads[indx++];

            LL.get_Range("J43").Formula = axle_loads[indx++];
            LL.get_Range("K43").Formula = axle_loads[indx++];
            LL.get_Range("L43").Formula = axle_loads[indx++];
            LL.get_Range("M43").Formula = axle_loads[indx++];


            #endregion Loads


            #region Distances

            indx = 0;

            LL.get_Range("H45").Formula = axle_dists[indx++];
            LL.get_Range("I45").Formula = axle_dists[indx++];
            LL.get_Range("J45").Formula = axle_loads[indx++];
            LL.get_Range("K45").Formula = axle_loads[indx++];
            LL.get_Range("L45").Formula = axle_loads[indx++];

            #endregion Distances

            #endregion Type 1

            #region Type 2



            LL.get_Range("A75").Formula = txt_LL_title2.Text;


            #region Loads

            kStr = txt_LL_axle_loads2.Text.Replace(",", " ");
            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_loads.Clear();

            axle_loads.AddRange(ml.StringList);

            indx = 0;



            if (axle_loads.Count < 7)
            {
                for (int i = axle_loads.Count; i <= 7; i++)
                {
                    axle_loads.Add("0.0");
                }
            }
            else
            {
                while (axle_loads.Count > 7) axle_loads.RemoveAt(axle_loads.Count - 1);
            }



            LL.get_Range("B77").Formula = axle_loads[indx++];
            LL.get_Range("C77").Formula = axle_loads[indx++];
            LL.get_Range("D77").Formula = axle_loads[indx++];
            LL.get_Range("E77").Formula = axle_loads[indx++];
            LL.get_Range("F77").Formula = axle_loads[indx++];
            LL.get_Range("G77").Formula = axle_loads[indx++];
            LL.get_Range("H77").Formula = axle_loads[indx++];

            indx = 0;
            LL.get_Range("J77").Formula = axle_loads[indx++];
            LL.get_Range("K77").Formula = axle_loads[indx++];
            LL.get_Range("L77").Formula = axle_loads[indx++];
            LL.get_Range("M77").Formula = axle_loads[indx++];
            LL.get_Range("N77").Formula = axle_loads[indx++];
            LL.get_Range("O77").Formula = axle_loads[indx++];
            LL.get_Range("P77").Formula = axle_loads[indx++];

            #endregion Loads


            #region Distances

            kStr = txt_LL_load_distances2.Text.Replace(",", " ");

            kStr = MyList.RemoveAllSpaces(kStr);
            ml = new MyList(kStr, ' ');

            axle_dists.Clear();
            axle_dists.AddRange(ml.StringList);


            indx = 0;




            if (axle_dists.Count < 6)
            {
                for (int i = axle_dists.Count; i <= 6; i++)
                {
                    axle_dists.Add("0.0");
                }
            }
            else
            {
                while (axle_dists.Count > 6) axle_dists.RemoveAt(axle_loads.Count - 1);
            }

            LL.get_Range("B79").Formula = axle_dists[indx++];
            LL.get_Range("C79").Formula = axle_dists[indx++];
            LL.get_Range("D79").Formula = axle_dists[indx++];
            LL.get_Range("E79").Formula = axle_dists[indx++];
            LL.get_Range("F79").Formula = axle_dists[indx++];
            //LL.get_Range("G79").Formula = axle_dists[indx++];
            LL.get_Range("H79").Formula = axle_dists[indx++];



            LL.get_Range("I79").Formula = txt_LL_separate_distance2.Text;

            indx = 0;

            LL.get_Range("J79").Formula = axle_dists[indx++];
            LL.get_Range("K79").Formula = axle_dists[indx++];
            LL.get_Range("L79").Formula = axle_dists[indx++];
            LL.get_Range("M79").Formula = axle_dists[indx++];
            LL.get_Range("N79").Formula = axle_dists[indx++];
            //LL.get_Range("O79").Formula = axle_dists[indx++];
            LL.get_Range("P79").Formula = axle_dists[indx++];



            #endregion Distances



            #endregion Type 2

            #region Type 3

            LL.get_Range("A108").Formula = txt_LL_title3.Text;

            LL.get_Range("C109").Formula = txt_LL_load_distances3.Text;
            LL.get_Range("E110").Formula = txt_LL_separate_distance3.Text;

            LL.get_Range("D116").Formula = "=2*" + txt_LL_axle_loads3.Text;
            LL.get_Range("F118").Formula = txt_LL_axle_loads3.Text;


            #endregion Type 3

            #endregion Live Loads



            myExcelWorkbook.Save();

            btn_open_report.Enabled = true;

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
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


        public string user_path { get; set; }

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF WELL FOUNDATION [BS]";
                return "DESIGN OF WELL FOUNDATION [IS]";
            }
        }

        public string Project_Name { get; set; }



        public string Get_Project_Folder()
        {
            string file_path = "";
            //string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            //if (user_path != "")
            //{
            //    if (Project_Name != "")
            //    {
            //        file_path = Path.Combine(user_path, Project_Name);
            //    }
            //    else
            //        file_path = Path.Combine(iApp.user_path, Title);
            //}

            //if (file_path == "" || Project_Name == "") return "";

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);
            file_path = user_path;
            return file_path;
        }

        public event EventHandler OnButtonClick;

        private void btn_process_new_Click(object sender, EventArgs e)
        {
            if (OnProcees != null)
                OnProcees(sender, e);
            Design_Well_Foundation();
        }


        public string excel_file
        {
            get
            {

                string file_path = Get_Project_Folder();

                //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
                file_path = Path.Combine(file_path, "Design of Well foundation.xlsx");
                return file_path;
            }
        }
        private void btn_open_report_Click(object sender, EventArgs e)
        {

            string file_path = excel_file;

            if(File.Exists( file_path))
            {
                iApp.OpenExcelFile(file_path, "2011ap");
            }

        }

        private void btn_open_worksheet_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //for (int i = 0; i < dgv_user_input.RowCount; i++)
            //{
            //    if(dgv_user_input[1, i].Value != "")
            //    dgv_user_input[1, i].Value = "-999";
            //}
        }

    }

}
