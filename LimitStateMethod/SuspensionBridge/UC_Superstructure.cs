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


namespace LimitStateMethod.SuspensionBridge
{
    public partial class UC_Superstructure : UserControl
    {

       public IApplication iApp = null;


        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelBeams;
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelChannels;
                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelAngles;
                return iApp.Tables.IS_SteelAngles;
            }
        }


        public double Length
        {
            set
            {
                txt_inp_foot_1.Text = value.ToString();
                txt_inp_moto_B3.Text = value.ToString();
            }
            get
            {
                return MyList.StringToDouble(txt_inp_moto_B3.Text, 0.0);
            }
        }


        public double Width
        {
            set
            {
                txt_inp_moto_B4.Text = value.ToString();
            }
            get
            {
                return MyList.StringToDouble(txt_inp_moto_B4.Text, 0.0);
            }
        }

        public double Carriageway_Width
        {
            set
            {
                txt_inp_moto_F4.Text = value.ToString();
            }
            get
            {
                return MyList.StringToDouble(txt_inp_moto_F4.Text, 0.0);
            }
        }

        public double Tower_Height
        {
            set
            {
                txt_inp_moto_F18.Text = value.ToString();
            }
            get
            {
                return MyList.StringToDouble(txt_inp_moto_F18.Text, 0.0);
            }
        }

        public string Span_Text
        {
            get
            {
                return label250.Text;
            }
            set
            {
                label250.Text = value;
                label34.Text = value;
            }
        }

        public string user_path
        {
            get
            {
                return Get_Project_Folder();
            }
        }

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DECK STRUCTURE DESIGN [BS]";
                return "DECK STRUCTURE DESIGN [IRC 45]";
            }
        }

        public string Get_Project_Folder()
        {
            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
        }


        public UC_Superstructure()
        {
            InitializeComponent();
        }

       public void Loads_Suspension_Sections(IApplication app)
        {
            iApp = app;
            #region Motorable
            uC_moto_sec_1.iApp = iApp;
            uC_moto_sec_2.iApp = iApp;
            uC_moto_sec_3.iApp = iApp;
            uC_moto_sec_4.iApp = iApp;
            uC_moto_sec_5.iApp = iApp;
            uC_moto_sec_6.iApp = iApp;
            uC_moto_sec_7.iApp = iApp;
            uC_moto_sec_8.iApp = iApp;
            uC_moto_sec_9.iApp = iApp;
            uC_moto_sec_10.iApp = iApp;
            uC_moto_sec_11.iApp = iApp;
            uC_moto_sec_12.iApp = iApp;

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                uC_moto_sec_1.Section_Name = "ISMC";
                uC_moto_sec_1.Section_Size = "100";


                uC_moto_sec_2.Section_Name = "ISMB";
                uC_moto_sec_2.Section_Size = "250";

                uC_moto_sec_3.Section_Name = "ISMB";
                uC_moto_sec_3.Section_Size = "450";

                uC_moto_sec_4.Section_Name = "ISA";
                uC_moto_sec_4.Section_Size = "200X200X25";


                uC_moto_sec_5.Section_Name = "ISA";
                uC_moto_sec_5.Section_Size = "75X75X8";


                uC_moto_sec_6.Section_Name = "ISA";
                uC_moto_sec_6.Section_Size = "75X75X8";


                uC_moto_sec_7.Section_Name = "ISA";
                uC_moto_sec_7.Section_Size = "100X100X8";


                uC_moto_sec_8.Section_Name = "ISA";
                uC_moto_sec_8.Section_Size = "200X200X18";


                uC_moto_sec_9.Section_Name = "ISMB";
                uC_moto_sec_9.Section_Size = "500";

                uC_moto_sec_10.Section_Name = "ISA";
                uC_moto_sec_10.Section_Size = "65X65X6";

                uC_moto_sec_11.Section_Name = "ISMB";
                uC_moto_sec_11.Section_Size = "450";

                uC_moto_sec_12.Section_Name = "ISMC";
                uC_moto_sec_12.Section_Size = "400";

            }
            else
            {


                uC_moto_sec_1.Convert_IS_to_BS("ISMC", "100");
                //uC_moto_sec_1.Section_Size = "100";


                uC_moto_sec_2.Convert_IS_to_BS("ISMB", "250");

                //uC_moto_sec_2.Section_Name = "ISMB";
                //uC_moto_sec_2.Section_Size = "250";

                uC_moto_sec_3.Convert_IS_to_BS("ISMB", "450");
                //uC_moto_sec_3.Section_Name = "ISMB";
                //uC_moto_sec_3.Section_Size = "450";

                uC_moto_sec_4.Convert_IS_to_BS("ISA", "200X200X25");
                //uC_moto_sec_4.Section_Name = "ISA";
                //uC_moto_sec_4.Section_Size = "200X200X25";


                uC_moto_sec_5.Convert_IS_to_BS("ISA", "75X75X8");
                //uC_moto_sec_5.Section_Name = "ISA";
                //uC_moto_sec_5.Section_Size = "75X75X8";


                uC_moto_sec_6.Convert_IS_to_BS("ISA", "75X75X8");
                //uC_moto_sec_6.Section_Name = "ISA";
                //uC_moto_sec_6.Section_Size = "75X75X8";


                uC_moto_sec_7.Convert_IS_to_BS("ISA", "100X100X8");
                //uC_moto_sec_7.Section_Name = "ISA";
                //uC_moto_sec_7.Section_Size = "100X100X8";


                uC_moto_sec_8.Convert_IS_to_BS("ISA", "200X200X18");
                //uC_moto_sec_8.Section_Name = "ISA";
                //uC_moto_sec_8.Section_Size = "200X200X18";


                uC_moto_sec_9.Convert_IS_to_BS("ISMB", "500");
                //uC_moto_sec_9.Section_Name = "ISMB";
                //uC_moto_sec_9.Section_Size = "500";

                uC_moto_sec_10.Convert_IS_to_BS("ISA", "65X65X6");
                //uC_moto_sec_10.Section_Name = "ISA";
                //uC_moto_sec_10.Section_Size = "65X65X6";

                uC_moto_sec_11.Convert_IS_to_BS("ISMB", "450");
                //uC_moto_sec_11.Section_Name = "ISMB";
                //uC_moto_sec_11.Section_Size = "450";

            }

            #endregion Motorable

            #region Foot Bridge

            uC_foot_sec_1.iApp = iApp;
            uC_foot_sec_2.iApp = iApp;
            uC_foot_sec_3.iApp = iApp;
            uC_foot_sec_4.iApp = iApp;
            uC_foot_sec_5.iApp = iApp;
            uC_foot_sec_6.iApp = iApp;
            uC_foot_sec_7.iApp = iApp;
            uC_foot_sec_8.iApp = iApp;
            uC_foot_sec_9.iApp = iApp;
            uC_foot_sec_10.iApp = iApp;
            uC_foot_sec_11.iApp = iApp;
            uC_foot_sec_12.iApp = iApp;


            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {

                uC_foot_sec_1.Section_Name = "ISMC";
                uC_foot_sec_1.Section_Size = "125";


                uC_foot_sec_2.Section_Name = "ISMB";
                uC_foot_sec_2.Section_Size = "175";


                uC_foot_sec_3.Section_Name = "ISA";
                uC_foot_sec_3.Section_Size = "150X150X15";


                uC_foot_sec_4.Section_Name = "ISA";
                uC_foot_sec_4.Section_Size = "55X55X8";


                uC_foot_sec_5.Section_Name = "ISA";
                uC_foot_sec_5.Section_Size = "80X80X8";


                uC_foot_sec_6.Section_Name = "ISA";
                uC_foot_sec_6.Section_Size = "75X75X8";


                uC_foot_sec_7.Section_Name = "ISA";
                uC_foot_sec_7.Section_Size = "200X200X15";


                uC_foot_sec_8.Section_Name = "ISMB";
                uC_foot_sec_8.Section_Size = "300";


                uC_foot_sec_9.Section_Name = "ISA";
                uC_foot_sec_9.Section_Size = "75X75X8";


                uC_foot_sec_10.Section_Name = "ISA";
                uC_foot_sec_10.Section_Size = "65X65X8";

                uC_foot_sec_11.Section_Name = "ISMC";
                uC_foot_sec_11.Section_Size = "400";


                uC_foot_sec_12.Section_Name = "ISMB";
                uC_foot_sec_12.Section_Size = "300";

            }
            else
            {
                uC_foot_sec_1.Convert_IS_to_BS("ISMC", "125");
                uC_foot_sec_2.Convert_IS_to_BS("ISMB", "175");
                uC_foot_sec_3.Convert_IS_to_BS("ISA", "150X150X15");
                uC_foot_sec_4.Convert_IS_to_BS("ISA", "55X55X8");
                uC_foot_sec_5.Convert_IS_to_BS("ISA", "80X80X8");
                uC_foot_sec_6.Convert_IS_to_BS("ISA", "75X75X8");

                uC_foot_sec_7.Convert_IS_to_BS("ISA", "200X200X15");


                uC_foot_sec_8.Convert_IS_to_BS("ISMB", "300");
                uC_foot_sec_9.Convert_IS_to_BS("ISA", "75X75X8");

                uC_foot_sec_10.Convert_IS_to_BS("ISA", "65X65X8");


                uC_foot_sec_11.Convert_IS_to_BS("ISMC", "400");
                uC_foot_sec_12.Convert_IS_to_BS("ISMB", "300");
            }
            #endregion Foot Bridge
        }

        private void btn_cable_new_Click(object sender, EventArgs e)
        {

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                Design_Cable_Suspension_Bridge();
            else
                Design_BS_Cable_Suspension_Bridge();
        }


        private void Design_Cable_Suspension_Bridge()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design of Deck Structure.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cable Stayed Bridge\Design of Deck Structure_IS.xlsx");

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
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Motorable Superstructure"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();




            //myExcelWorksheet.get_Range("B153").Formula = txt_Ana_og.Text;
            //myExcelWorksheet.get_Range("C152").Formula = (eff_L / 4.0).ToString("f3");
            //myExcelWorksheet.get_Range("D160").Formula = eff_L.ToString("f3");
            #region Motorable Superstructure
            //myExcelWorksheet.get_Range("A27").Formula = txt_inp_moto_A27.Text;
            myExcelWorksheet.get_Range("B10").Formula = txt_inp_moto_B10.Text;
            myExcelWorksheet.get_Range("B11").Formula = txt_inp_moto_B11.Text;
            myExcelWorksheet.get_Range("B12").Formula = txt_inp_moto_B12.Text;
            //myExcelWorksheet.get_Range("B13").Formula = txt_inp_moto_B13.Text;
            myExcelWorksheet.get_Range("B14").Formula = txt_inp_moto_B14.Text;
            myExcelWorksheet.get_Range("B15").Formula = txt_inp_moto_B15.Text;
            myExcelWorksheet.get_Range("B16").Formula = txt_inp_moto_B16.Text;
            myExcelWorksheet.get_Range("B17").Formula = txt_inp_moto_B17.Text;
            myExcelWorksheet.get_Range("B18").Formula = txt_inp_moto_B18.Text;
            //myExcelWorksheet.get_Range("B19").Formula = txt_inp_moto_B19.Text;



            myExcelWorksheet.get_Range("B20").Formula = txt_inp_moto_B20.Text;
            myExcelWorksheet.get_Range("B21").Formula = txt_inp_moto_B21.Text;
            myExcelWorksheet.get_Range("B22").Formula = txt_inp_moto_B22.Text;
            myExcelWorksheet.get_Range("B23").Formula = txt_inp_moto_B23.Text;
            myExcelWorksheet.get_Range("B24").Formula = txt_inp_moto_B24.Text;
            myExcelWorksheet.get_Range("B25").Formula = txt_inp_moto_B25.Text;
            myExcelWorksheet.get_Range("B26").Formula = txt_inp_moto_B26.Text;
            myExcelWorksheet.get_Range("B29").Formula = txt_inp_moto_B29.Text;
            myExcelWorksheet.get_Range("B3").Formula = txt_inp_moto_B3.Text;
            myExcelWorksheet.get_Range("B31").Formula = txt_inp_moto_B31.Text;
            myExcelWorksheet.get_Range("B32").Formula = txt_inp_moto_B32.Text;





            myExcelWorksheet.get_Range("B33").Formula = txt_inp_moto_B33.Text;
            myExcelWorksheet.get_Range("B34").Formula = txt_inp_moto_B34.Text;
            myExcelWorksheet.get_Range("B35").Formula = txt_inp_moto_B35.Text;
            myExcelWorksheet.get_Range("B36").Formula = txt_inp_moto_B36.Text;
            myExcelWorksheet.get_Range("B37").Formula = txt_inp_moto_B37.Text;
            myExcelWorksheet.get_Range("B38").Formula = txt_inp_moto_B38.Text;
            myExcelWorksheet.get_Range("B39").Formula = txt_inp_moto_B39.Text;
            myExcelWorksheet.get_Range("B4").Formula = txt_inp_moto_B4.Text;
            myExcelWorksheet.get_Range("B40").Formula = txt_inp_moto_B40.Text;
            myExcelWorksheet.get_Range("B41").Formula = txt_inp_moto_B41.Text;
            myExcelWorksheet.get_Range("B42").Formula = txt_inp_moto_B42.Text;





            myExcelWorksheet.get_Range("B43").Formula = txt_inp_moto_B43.Text;
            myExcelWorksheet.get_Range("B44").Formula = txt_inp_moto_B44.Text;
            myExcelWorksheet.get_Range("B45").Formula = txt_inp_moto_B45.Text;
            myExcelWorksheet.get_Range("B46").Formula = txt_inp_moto_B46.Text;
            myExcelWorksheet.get_Range("B47").Formula = txt_inp_moto_B47.Text;
            myExcelWorksheet.get_Range("B48").Formula = txt_inp_moto_B48.Text;
            myExcelWorksheet.get_Range("B49").Formula = txt_inp_moto_B49.Text;
            myExcelWorksheet.get_Range("B5").Formula = txt_inp_moto_B5.Text;
            myExcelWorksheet.get_Range("B50").Formula = txt_inp_moto_B50.Text;
            myExcelWorksheet.get_Range("B51").Formula = txt_inp_moto_B51.Text;
            myExcelWorksheet.get_Range("B52").Formula = txt_inp_moto_B52.Text;
            myExcelWorksheet.get_Range("B53").Formula = txt_inp_moto_B53.Text;
            myExcelWorksheet.get_Range("B54").Formula = txt_inp_moto_B54.Text;
            myExcelWorksheet.get_Range("B6").Formula = txt_inp_moto_B6.Text;
            myExcelWorksheet.get_Range("B7").Formula = txt_inp_moto_B7.Text;
            myExcelWorksheet.get_Range("B8").Formula = txt_inp_moto_B8.Text;


            //myExcelWorksheet.get_Range("B9").Formula = txt_inp_moto_B9.Text;

            //Applied Sidl
            myExcelWorksheet.get_Range("B9").Formula = (MyList.StringToDouble(txt_inp_moto_B9.Text, 0.0) + MyList.StringToDouble(txt_inp_moto_B9_sidl.Text, 0.0)).ToString();



            //myExcelWorksheet.get_Range("C27").Formula = txt_inp_moto_C27.Text;
            //myExcelWorksheet.get_Range("D19").Formula = txt_inp_moto_D19.Text;
            myExcelWorksheet.get_Range("D29").Formula = txt_inp_moto_D29.Text;
            myExcelWorksheet.get_Range("D31").Formula = txt_inp_moto_D31.Text;
            myExcelWorksheet.get_Range("D47").Formula = txt_inp_moto_D47.Text;
            myExcelWorksheet.get_Range("D53").Formula = txt_inp_moto_D53.Text;
            myExcelWorksheet.get_Range("D6").Formula = txt_inp_moto_D6.Text;
            myExcelWorksheet.get_Range("E23").Formula = txt_inp_moto_E23.Text;
            myExcelWorksheet.get_Range("E50").Formula = txt_inp_moto_E50.Text;
            myExcelWorksheet.get_Range("F13").Formula = txt_inp_moto_F13.Text;
            myExcelWorksheet.get_Range("F14").Formula = txt_inp_moto_F14.Text;
            myExcelWorksheet.get_Range("F16").Formula = txt_inp_moto_F16.Text;
            myExcelWorksheet.get_Range("F17").Formula = txt_inp_moto_F17.Text;
            myExcelWorksheet.get_Range("F18").Formula = txt_inp_moto_F18.Text;
            //myExcelWorksheet.get_Range("F27").Formula = txt_inp_moto_F27.Text;
            myExcelWorksheet.get_Range("F28").Formula = txt_inp_moto_F28.Text;




            myExcelWorksheet.get_Range("F29").Formula = txt_inp_moto_F29.Text;
            myExcelWorksheet.get_Range("F30").Formula = txt_inp_moto_F30.Text;
            myExcelWorksheet.get_Range("F31").Formula = txt_inp_moto_F31.Text;
            myExcelWorksheet.get_Range("F33").Formula = txt_inp_moto_F33.Text;
            myExcelWorksheet.get_Range("F36").Formula = txt_inp_moto_F36.Text;
            myExcelWorksheet.get_Range("F37").Formula = txt_inp_moto_F37.Text;
            myExcelWorksheet.get_Range("F38").Formula = txt_inp_moto_F38.Text;
            myExcelWorksheet.get_Range("F39").Formula = txt_inp_moto_F39.Text;
            myExcelWorksheet.get_Range("F4").Formula = txt_inp_moto_F4.Text;
            myExcelWorksheet.get_Range("F46").Formula = txt_inp_moto_F46.Text;
            myExcelWorksheet.get_Range("F47").Formula = txt_inp_moto_F47.Text;
            myExcelWorksheet.get_Range("F49").Formula = txt_inp_moto_F49.Text;
            myExcelWorksheet.get_Range("F52").Formula = txt_inp_moto_F52.Text;
            myExcelWorksheet.get_Range("F53").Formula = txt_inp_moto_F53.Text;
            myExcelWorksheet.get_Range("F9").Formula = txt_inp_moto_F9.Text;
            //myExcelWorksheet.get_Range("G19").Formula = txt_inp_moto_G19.Text;
            myExcelWorksheet.get_Range("G23").Formula = txt_inp_moto_G23.Text;
            myExcelWorksheet.get_Range("H10").Formula = txt_inp_moto_H10.Text;
            myExcelWorksheet.get_Range("H11").Formula = txt_inp_moto_H11.Text;
            myExcelWorksheet.get_Range("H12").Formula = txt_inp_moto_H12.Text;
            myExcelWorksheet.get_Range("H13").Formula = txt_inp_moto_H13.Text;
            myExcelWorksheet.get_Range("H14").Formula = txt_inp_moto_H14.Text;
            myExcelWorksheet.get_Range("H16").Formula = txt_inp_moto_H16.Text;
            myExcelWorksheet.get_Range("H17").Formula = txt_inp_moto_H17.Text;
            myExcelWorksheet.get_Range("H21").Formula = txt_inp_moto_H21.Text;
            myExcelWorksheet.get_Range("H22").Formula = txt_inp_moto_H22.Text;
            myExcelWorksheet.get_Range("H29").Formula = txt_inp_moto_H29.Text;
            myExcelWorksheet.get_Range("H31").Formula = txt_inp_moto_H31.Text;
            myExcelWorksheet.get_Range("H34").Formula = txt_inp_moto_H34.Text;
            myExcelWorksheet.get_Range("H35").Formula = txt_inp_moto_H35.Text;
            myExcelWorksheet.get_Range("H50").Formula = txt_inp_moto_H50.Text;
            myExcelWorksheet.get_Range("I23").Formula = txt_inp_moto_I23.Text;
            //myExcelWorksheet.get_Range("I27").Formula = txt_inp_moto_I27.Text;
            myExcelWorksheet.get_Range("I33").Formula = txt_inp_moto_I33.Text;
            myExcelWorksheet.get_Range("I49").Formula = txt_inp_moto_I49.Text;
            myExcelWorksheet.get_Range("I52").Formula = txt_inp_moto_I52.Text;
            //myExcelWorksheet.get_Range("J19").Formula = txt_inp_moto_J19.Text;
            myExcelWorksheet.get_Range("J29").Formula = txt_inp_moto_J29.Text;
            myExcelWorksheet.get_Range("J31").Formula = txt_inp_moto_J31.Text;
            myExcelWorksheet.get_Range("K38").Formula = txt_inp_moto_K38.Text;
            myExcelWorksheet.get_Range("K39").Formula = txt_inp_moto_K39.Text;



            #region Section 1
            myExcelWorksheet.get_Range("J139").Formula = uC_moto_sec_1.Section_Name;
            myExcelWorksheet.get_Range("K139").Formula = uC_moto_sec_1.Section_Size;
            myExcelWorksheet.get_Range("K140").Formula = uC_moto_sec_1.Area;
            myExcelWorksheet.get_Range("K141").Formula = uC_moto_sec_1.Ixx;

            #endregion Section 1

            #region Section 2


            myExcelWorksheet.get_Range("J213").Formula = uC_moto_sec_2.Section_Name + " " + uC_moto_sec_2.Section_Size;
            myExcelWorksheet.get_Range("J214").Formula = uC_moto_sec_2.Zxx;
            myExcelWorksheet.get_Range("J215").Formula = uC_moto_sec_2.tw;
            myExcelWorksheet.get_Range("J216").Formula = uC_moto_sec_2.D;
            myExcelWorksheet.get_Range("J228").Formula = uC_moto_sec_2.Ixx;

            #endregion Section 2

            #region Section 3


            myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("J283").Formula = uC_moto_sec_3.Zxx;
            myExcelWorksheet.get_Range("J284").Formula = uC_moto_sec_3.tw;
            myExcelWorksheet.get_Range("J285").Formula = uC_moto_sec_3.D;
            myExcelWorksheet.get_Range("J297").Formula = uC_moto_sec_3.Ixx;

            #endregion Section 2

            #region Section 4


            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("J581").Formula = uC_moto_sec_4.h;
            myExcelWorksheet.get_Range("J582").Formula = uC_moto_sec_4.b;
            myExcelWorksheet.get_Range("J584").Formula = uC_moto_sec_4.Cyy;
            myExcelWorksheet.get_Range("J585").Formula = uC_moto_sec_4.Area;
            myExcelWorksheet.get_Range("J586").Formula = uC_moto_sec_4.tf;
            myExcelWorksheet.get_Range("J587").Formula = uC_moto_sec_4.tw;
            myExcelWorksheet.get_Range("J589").Formula = uC_moto_sec_4.Ixx;
            myExcelWorksheet.get_Range("J590").Formula = uC_moto_sec_4.Iyy;

            #endregion Section 4

            #region Section 5


            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("J652").Formula = uC_moto_sec_5.h;
            myExcelWorksheet.get_Range("J653").Formula = uC_moto_sec_5.b;
            myExcelWorksheet.get_Range("J655").Formula = uC_moto_sec_5.Cyy;
            myExcelWorksheet.get_Range("J656").Formula = uC_moto_sec_5.Area;
            myExcelWorksheet.get_Range("J657").Formula = uC_moto_sec_5.tf;
            myExcelWorksheet.get_Range("J658").Formula = uC_moto_sec_5.tw;
            myExcelWorksheet.get_Range("J659").Formula = uC_moto_sec_5.Ixx;
            myExcelWorksheet.get_Range("J660").Formula = uC_moto_sec_5.Iyy;

            #endregion Section 5

            #region Section 6


            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("J682").Formula = uC_moto_sec_6.h;
            myExcelWorksheet.get_Range("J683").Formula = uC_moto_sec_6.b;
            myExcelWorksheet.get_Range("J685").Formula = uC_moto_sec_6.Cyy;
            myExcelWorksheet.get_Range("J686").Formula = uC_moto_sec_6.Area;
            myExcelWorksheet.get_Range("J687").Formula = uC_moto_sec_6.tf;
            myExcelWorksheet.get_Range("J688").Formula = uC_moto_sec_6.tw;
            myExcelWorksheet.get_Range("J689").Formula = uC_moto_sec_6.Ixx;
            myExcelWorksheet.get_Range("J690").Formula = uC_moto_sec_6.Iyy;

            #endregion Section 6

            #region Section 7


            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("J719").Formula = uC_moto_sec_7.h;
            myExcelWorksheet.get_Range("J720").Formula = uC_moto_sec_7.b;
            myExcelWorksheet.get_Range("J722").Formula = uC_moto_sec_7.Cyy;
            myExcelWorksheet.get_Range("J723").Formula = uC_moto_sec_7.Area;
            myExcelWorksheet.get_Range("J724").Formula = uC_moto_sec_7.tf;
            myExcelWorksheet.get_Range("J725").Formula = uC_moto_sec_7.tw;
            myExcelWorksheet.get_Range("J726").Formula = uC_moto_sec_7.Ixx;
            myExcelWorksheet.get_Range("J727").Formula = uC_moto_sec_7.Iyy;

            #endregion Section 7

            #region Section 8

            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_8.Section_Name + " " + uC_moto_sec_8.Section_Size;
            myExcelWorksheet.get_Range("J719").Formula = uC_moto_sec_8.h;
            myExcelWorksheet.get_Range("J720").Formula = uC_moto_sec_8.b;
            myExcelWorksheet.get_Range("J722").Formula = uC_moto_sec_8.Cyy;
            myExcelWorksheet.get_Range("J723").Formula = uC_moto_sec_8.Area;
            myExcelWorksheet.get_Range("J724").Formula = uC_moto_sec_8.tf;
            myExcelWorksheet.get_Range("J725").Formula = uC_moto_sec_8.tw;
            myExcelWorksheet.get_Range("J726").Formula = uC_moto_sec_8.Ixx;
            myExcelWorksheet.get_Range("J727").Formula = uC_moto_sec_8.Iyy;

            #endregion Section 8

            #region Section 9


            myExcelWorksheet.get_Range("J883").Formula = uC_moto_sec_9.Section_Name + " " + uC_moto_sec_9.Section_Size;
            myExcelWorksheet.get_Range("J892").Formula = uC_moto_sec_9.Zxx;
            myExcelWorksheet.get_Range("J893").Formula = uC_moto_sec_9.tw;
            myExcelWorksheet.get_Range("J895").Formula = uC_moto_sec_9.D;
            //myExcelWorksheet.get_Range("J883").Formula = uC_moto_sec_9.Ixx;

            #endregion Section 9

            #region Section 10

            //myExcelWorksheet.get_Range("J282").Formula = uC_moto_sec_8.Section_Name + " " + uC_moto_sec_8.Section_Size;
            myExcelWorksheet.get_Range("J980").Formula = uC_moto_sec_10.h;
            myExcelWorksheet.get_Range("J981").Formula = uC_moto_sec_10.b;
            myExcelWorksheet.get_Range("J983").Formula = uC_moto_sec_10.Cyy;
            myExcelWorksheet.get_Range("J984").Formula = uC_moto_sec_10.Area;
            myExcelWorksheet.get_Range("J985").Formula = uC_moto_sec_10.tf;
            myExcelWorksheet.get_Range("J986").Formula = uC_moto_sec_10.tw;
            myExcelWorksheet.get_Range("J987").Formula = uC_moto_sec_10.Ixx;
            myExcelWorksheet.get_Range("J988").Formula = uC_moto_sec_10.Iyy;

            #endregion Section 10

            #region Section 11


            myExcelWorksheet.get_Range("J1054").Formula = uC_moto_sec_11.Section_Name;
            myExcelWorksheet.get_Range("K1054").Formula = uC_moto_sec_11.Section_Size;
            myExcelWorksheet.get_Range("J1055").Formula = uC_moto_sec_11.Zxx;
            myExcelWorksheet.get_Range("J1056").Formula = uC_moto_sec_11.tw;
            myExcelWorksheet.get_Range("J1058").Formula = uC_moto_sec_11.D;
            myExcelWorksheet.get_Range("J1124").Formula = uC_moto_sec_11.Area;
            myExcelWorksheet.get_Range("J1125").Formula = uC_moto_sec_11.Ixx;

            #endregion Section 11

            #region Section 12


            myExcelWorksheet.get_Range("J1540").Formula = uC_moto_sec_12.Section_Name + " " + uC_moto_sec_12.Section_Size;
            //myExcelWorksheet.get_Range("K1054").Formula = uC_moto_sec_12.Section_Size;
            myExcelWorksheet.get_Range("J1550").Formula = uC_moto_sec_12.tw;

            #endregion Section 12


            #endregion Motorable Superstructure


            #region footbridge superstructure

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["footbridge superstructure"];

            myExcelWorksheet.get_Range("E3").Formula = txt_inp_foot_1.Text;
            myExcelWorksheet.get_Range("E4").Formula = txt_inp_foot_2.Text;
            myExcelWorksheet.get_Range("E5").Formula = txt_inp_foot_3.Text;

            myExcelWorksheet.get_Range("E8").Formula = txt_inp_foot_4.Text;
            myExcelWorksheet.get_Range("E9").Formula = txt_inp_foot_5.Text;
            myExcelWorksheet.get_Range("E10").Formula = txt_inp_foot_6.Text;
            myExcelWorksheet.get_Range("E11").Formula = txt_inp_foot_7.Text;
            myExcelWorksheet.get_Range("E13").Formula = txt_inp_foot_8.Text;
            myExcelWorksheet.get_Range("E14").Formula = txt_inp_foot_9.Text;
            myExcelWorksheet.get_Range("E15").Formula = txt_inp_foot_10.Text;



            #region Section 1
            myExcelWorksheet.get_Range("J149").Formula = uC_foot_sec_1.Section_Name;
            myExcelWorksheet.get_Range("K149").Formula = uC_foot_sec_1.Section_Size;
            myExcelWorksheet.get_Range("J179").Formula = uC_foot_sec_1.D;
            myExcelWorksheet.get_Range("J176").Formula = uC_foot_sec_1.Zxx;
            myExcelWorksheet.get_Range("J177").Formula = uC_foot_sec_1.tw;
            myExcelWorksheet.get_Range("J191").Formula = uC_foot_sec_1.Ixx;

            #endregion Section 1

            #region Section 2

            myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_2.Section_Name;
            myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_2.Section_Size;
            myExcelWorksheet.get_Range("J239").Formula = uC_foot_sec_2.D;
            myExcelWorksheet.get_Range("J236").Formula = uC_foot_sec_2.Zxx;
            myExcelWorksheet.get_Range("J237").Formula = uC_foot_sec_2.tw;
            myExcelWorksheet.get_Range("J255").Formula = uC_foot_sec_2.Ixx;

            #endregion Section 2

            #region Section 3

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_3.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_3.Section_Size;

            myExcelWorksheet.get_Range("J542").Formula = uC_foot_sec_3.h;
            myExcelWorksheet.get_Range("J543").Formula = uC_foot_sec_3.b;
            myExcelWorksheet.get_Range("J545").Formula = uC_foot_sec_3.Cyy;
            myExcelWorksheet.get_Range("J546").Formula = uC_foot_sec_3.Area;
            myExcelWorksheet.get_Range("J547").Formula = uC_foot_sec_3.tf;
            myExcelWorksheet.get_Range("J548").Formula = uC_foot_sec_3.tw;
            myExcelWorksheet.get_Range("J549").Formula = uC_foot_sec_3.Ixx;
            myExcelWorksheet.get_Range("J550").Formula = uC_foot_sec_3.Iyy;

            #endregion Section 3

            #region Section 4

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_3.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_3.Section_Size;

            myExcelWorksheet.get_Range("J687").Formula = uC_foot_sec_4.h;
            myExcelWorksheet.get_Range("J688").Formula = uC_foot_sec_4.b;
            myExcelWorksheet.get_Range("J690").Formula = uC_foot_sec_4.Cyy;
            myExcelWorksheet.get_Range("J691").Formula = uC_foot_sec_4.Area;
            myExcelWorksheet.get_Range("J692").Formula = uC_foot_sec_4.tf;
            myExcelWorksheet.get_Range("J693").Formula = uC_foot_sec_4.tw;
            myExcelWorksheet.get_Range("J694").Formula = uC_foot_sec_4.Ixx;
            myExcelWorksheet.get_Range("J695").Formula = uC_foot_sec_4.Iyy;

            #endregion Section 4

            #region Section 5

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_3.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_3.Section_Size;

            myExcelWorksheet.get_Range("J718").Formula = uC_foot_sec_5.h;
            myExcelWorksheet.get_Range("J719").Formula = uC_foot_sec_5.b;
            myExcelWorksheet.get_Range("J721").Formula = uC_foot_sec_5.Cyy;
            myExcelWorksheet.get_Range("J722").Formula = uC_foot_sec_5.Area;
            myExcelWorksheet.get_Range("J723").Formula = uC_foot_sec_5.tf;
            myExcelWorksheet.get_Range("J724").Formula = uC_foot_sec_5.tw;
            myExcelWorksheet.get_Range("J725").Formula = uC_foot_sec_5.Ixx;
            myExcelWorksheet.get_Range("J726").Formula = uC_foot_sec_5.Iyy;

            #endregion Section 5


            #region Section 6

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_3.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_3.Section_Size;

            myExcelWorksheet.get_Range("J753").Formula = uC_foot_sec_6.h;
            myExcelWorksheet.get_Range("J754").Formula = uC_foot_sec_6.b;
            myExcelWorksheet.get_Range("J756").Formula = uC_foot_sec_6.Cyy;
            myExcelWorksheet.get_Range("J757").Formula = uC_foot_sec_6.Area;
            myExcelWorksheet.get_Range("J758").Formula = uC_foot_sec_6.tf;
            myExcelWorksheet.get_Range("J759").Formula = uC_foot_sec_6.tw;
            myExcelWorksheet.get_Range("J760").Formula = uC_foot_sec_6.Ixx;
            myExcelWorksheet.get_Range("J761").Formula = uC_foot_sec_6.Iyy;

            #endregion Section 6

            #region Section 7

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_3.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_3.Section_Size;

            myExcelWorksheet.get_Range("J845").Formula = uC_foot_sec_7.h;
            myExcelWorksheet.get_Range("K845").Formula = uC_foot_sec_7.b;
            //myExcelWorksheet.get_Range("J756").Formula = uC_foot_sec_7.Cyy;
            //myExcelWorksheet.get_Range("J757").Formula = uC_foot_sec_7.Area;
            myExcelWorksheet.get_Range("L845").Formula = uC_foot_sec_7.tf;
            //myExcelWorksheet.get_Range("J759").Formula = uC_foot_sec_7.tw;
            //myExcelWorksheet.get_Range("J760").Formula = uC_foot_sec_7.Ixx;
            //myExcelWorksheet.get_Range("J761").Formula = uC_foot_sec_7.Iyy;

            #endregion Section 7

            #region Section 8

            myExcelWorksheet.get_Range("C890").Formula = uC_foot_sec_8.Section_Name + " " + uC_foot_sec_8.Section_Size;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_8.Section_Size;
            myExcelWorksheet.get_Range("J902").Formula = uC_foot_sec_8.D;
            myExcelWorksheet.get_Range("J899").Formula = uC_foot_sec_8.Zxx;
            myExcelWorksheet.get_Range("J900").Formula = uC_foot_sec_8.tw;
            //myExcelWorksheet.get_Range("J255").Formula = uC_foot_sec_8.Ixx;

            #endregion Section 8

            #region Section 9

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_9.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_9.Section_Size;

            myExcelWorksheet.get_Range("J907").Formula = uC_foot_sec_9.h;
            myExcelWorksheet.get_Range("K907").Formula = uC_foot_sec_9.b;
            //myExcelWorksheet.get_Range("J756").Formula = uC_foot_sec_9.Cyy;
            //myExcelWorksheet.get_Range("J757").Formula = uC_foot_sec_9.Area;
            myExcelWorksheet.get_Range("L907").Formula = uC_foot_sec_9.tf;
            //myExcelWorksheet.get_Range("J759").Formula = uC_foot_sec_9.tw;
            //myExcelWorksheet.get_Range("J760").Formula = uC_foot_sec_9.Ixx;
            //myExcelWorksheet.get_Range("J761").Formula = uC_foot_sec_9.Iyy;

            #endregion Section 9

            #region Section 10

            //myExcelWorksheet.get_Range("J200").Formula = uC_foot_sec_10.Section_Name;
            //myExcelWorksheet.get_Range("K200").Formula = uC_foot_sec_10.Section_Size;

            myExcelWorksheet.get_Range("J991").Formula = uC_foot_sec_10.h;
            myExcelWorksheet.get_Range("J992").Formula = uC_foot_sec_10.b;
            myExcelWorksheet.get_Range("J994").Formula = uC_foot_sec_10.Cyy;
            myExcelWorksheet.get_Range("J995").Formula = uC_foot_sec_10.Area;
            myExcelWorksheet.get_Range("J996").Formula = uC_foot_sec_10.tf;
            myExcelWorksheet.get_Range("J997").Formula = uC_foot_sec_10.tw;
            myExcelWorksheet.get_Range("J998").Formula = uC_foot_sec_10.Ixx;
            myExcelWorksheet.get_Range("J999").Formula = uC_foot_sec_10.Iyy;

            #endregion Section 10

            #region Section 11
            //myExcelWorksheet.get_Range("J149").Formula = uC_foot_sec_1.Section_Name;
            //myExcelWorksheet.get_Range("K149").Formula = uC_foot_sec_1.Section_Size;
            //myExcelWorksheet.get_Range("J179").Formula = uC_foot_sec_1.D;
            //myExcelWorksheet.get_Range("J176").Formula = uC_foot_sec_1.Zxx;
            //myExcelWorksheet.get_Range("J177").Formula = uC_foot_sec_1.tw;
            //myExcelWorksheet.get_Range("J191").Formula = uC_foot_sec_1.Ixx;

            #endregion Section 11

            #region Section 12

            myExcelWorksheet.get_Range("J1112").Formula = uC_foot_sec_2.Section_Name;
            myExcelWorksheet.get_Range("K1112").Formula = uC_foot_sec_2.Section_Size;
            myExcelWorksheet.get_Range("J1119").Formula = uC_foot_sec_2.Area;
            myExcelWorksheet.get_Range("J1121").Formula = uC_foot_sec_2.D;
            //myExcelWorksheet.get_Range("J1112").Formula = uC_foot_sec_2.Zxx;
            myExcelWorksheet.get_Range("L1121").Formula = uC_foot_sec_2.tw;
            myExcelWorksheet.get_Range("J1120").Formula = uC_foot_sec_2.Ixx;

            #endregion Section 12


            #endregion footbridge superstructure


            #region abutment design

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["abutment design"];

            myExcelWorksheet.get_Range("C9").Formula = txt_susp_abut_H1.Text;
            myExcelWorksheet.get_Range("C22").Formula = txt_susp_abut_H3.Text;
            myExcelWorksheet.get_Range("C24").Formula = txt_susp_abut_H4.Text;

            double h1, h2, h3, h4;


            h1 = MyList.StringToDouble(txt_susp_abut_H1.Text, 0.0);
            h2 = MyList.StringToDouble(txt_susp_abut_H2.Text, 0.0);
            h3 = MyList.StringToDouble(txt_susp_abut_H3.Text, 0.0);
            h4 = MyList.StringToDouble(txt_susp_abut_H4.Text, 0.0);
            myExcelWorksheet.get_Range("B15").Formula = (h1 + h2 + h3 + h4).ToString("f3");




            myExcelWorksheet.get_Range("C25").Formula = txt_susp_abut_B1.Text;
            myExcelWorksheet.get_Range("D26").Formula = txt_susp_abut_B2.Text;
            //myExcelWorksheet.get_Range("D25").Formula = txt_susp_abut_B3.Text;
            myExcelWorksheet.get_Range("E25").Formula = txt_susp_abut_B4.Text;
            //myExcelWorksheet.get_Range("F25").Formula = txt_susp_abut_B5.Text;
            myExcelWorksheet.get_Range("G25").Formula = txt_susp_abut_B6.Text;
            myExcelWorksheet.get_Range("H25").Formula = txt_susp_abut_B7.Text;


            myExcelWorksheet.get_Range("I12").Formula = txt_susp_abut_RL1.Text;
            myExcelWorksheet.get_Range("I15").Formula = txt_susp_abut_RL2.Text;
            myExcelWorksheet.get_Range("I17").Formula = txt_susp_abut_RL3.Text;
            myExcelWorksheet.get_Range("I20").Formula = txt_susp_abut_RL4.Text;
            myExcelWorksheet.get_Range("B24").Formula = txt_susp_abut_RL5.Text;


            #endregion abutment design


            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void Design_BS_Cable_Suspension_Bridge()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design of Deck Structure BS.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\BS Cable Suspension Bridge Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cable Stayed Bridge\Design of Deck Structure_BS.xlsx");

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
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Motorable Superstructure"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();




            //myExcelWorksheet.get_Range("B153").Formula = txt_Ana_og.Text;
            //myExcelWorksheet.get_Range("C152").Formula = (eff_L / 4.0).ToString("f3");
            //myExcelWorksheet.get_Range("D160").Formula = eff_L.ToString("f3");
            #region Motorable Superstructure
            //myExcelWorksheet.get_Range("A27").Formula = txt_inp_moto_A27.Text;
            myExcelWorksheet.get_Range("B10").Formula = txt_inp_moto_B10.Text;
            myExcelWorksheet.get_Range("B11").Formula = txt_inp_moto_B11.Text;
            myExcelWorksheet.get_Range("B12").Formula = txt_inp_moto_B12.Text;
            //myExcelWorksheet.get_Range("B13").Formula = txt_inp_moto_B13.Text;
            myExcelWorksheet.get_Range("B14").Formula = txt_inp_moto_B14.Text;
            myExcelWorksheet.get_Range("B15").Formula = txt_inp_moto_B15.Text;
            myExcelWorksheet.get_Range("B16").Formula = txt_inp_moto_B16.Text;
            myExcelWorksheet.get_Range("B17").Formula = txt_inp_moto_B17.Text;
            myExcelWorksheet.get_Range("B18").Formula = txt_inp_moto_B18.Text;
            //myExcelWorksheet.get_Range("B19").Formula = txt_inp_moto_B19.Text;



            myExcelWorksheet.get_Range("B20").Formula = txt_inp_moto_B20.Text;
            myExcelWorksheet.get_Range("B21").Formula = txt_inp_moto_B21.Text;
            myExcelWorksheet.get_Range("B22").Formula = txt_inp_moto_B22.Text;
            myExcelWorksheet.get_Range("B23").Formula = txt_inp_moto_B23.Text;
            myExcelWorksheet.get_Range("B24").Formula = txt_inp_moto_B24.Text;
            myExcelWorksheet.get_Range("B25").Formula = txt_inp_moto_B25.Text;
            myExcelWorksheet.get_Range("B26").Formula = txt_inp_moto_B26.Text;
            myExcelWorksheet.get_Range("B29").Formula = txt_inp_moto_B29.Text;
            myExcelWorksheet.get_Range("B3").Formula = txt_inp_moto_B3.Text;
            myExcelWorksheet.get_Range("B31").Formula = txt_inp_moto_B31.Text;
            myExcelWorksheet.get_Range("B32").Formula = txt_inp_moto_B32.Text;





            myExcelWorksheet.get_Range("B33").Formula = txt_inp_moto_B33.Text;
            myExcelWorksheet.get_Range("B34").Formula = txt_inp_moto_B34.Text;
            myExcelWorksheet.get_Range("B35").Formula = txt_inp_moto_B35.Text;
            myExcelWorksheet.get_Range("B36").Formula = txt_inp_moto_B36.Text;
            myExcelWorksheet.get_Range("B37").Formula = txt_inp_moto_B37.Text;
            myExcelWorksheet.get_Range("B38").Formula = txt_inp_moto_B38.Text;
            myExcelWorksheet.get_Range("B39").Formula = txt_inp_moto_B39.Text;
            myExcelWorksheet.get_Range("B4").Formula = txt_inp_moto_B4.Text;
            myExcelWorksheet.get_Range("B40").Formula = txt_inp_moto_B40.Text;
            myExcelWorksheet.get_Range("B41").Formula = txt_inp_moto_B41.Text;
            myExcelWorksheet.get_Range("B42").Formula = txt_inp_moto_B42.Text;





            myExcelWorksheet.get_Range("B43").Formula = txt_inp_moto_B43.Text;
            myExcelWorksheet.get_Range("B44").Formula = txt_inp_moto_B44.Text;
            myExcelWorksheet.get_Range("B45").Formula = txt_inp_moto_B45.Text;
            myExcelWorksheet.get_Range("B46").Formula = txt_inp_moto_B46.Text;
            myExcelWorksheet.get_Range("B47").Formula = txt_inp_moto_B47.Text;
            myExcelWorksheet.get_Range("B48").Formula = txt_inp_moto_B48.Text;
            myExcelWorksheet.get_Range("B49").Formula = txt_inp_moto_B49.Text;
            myExcelWorksheet.get_Range("B5").Formula = txt_inp_moto_B5.Text;
            myExcelWorksheet.get_Range("B50").Formula = txt_inp_moto_B50.Text;
            myExcelWorksheet.get_Range("B51").Formula = txt_inp_moto_B51.Text;
            myExcelWorksheet.get_Range("B52").Formula = txt_inp_moto_B52.Text;
            myExcelWorksheet.get_Range("B53").Formula = txt_inp_moto_B53.Text;
            myExcelWorksheet.get_Range("B54").Formula = txt_inp_moto_B54.Text;
            myExcelWorksheet.get_Range("B6").Formula = txt_inp_moto_B6.Text;
            myExcelWorksheet.get_Range("B7").Formula = txt_inp_moto_B7.Text;
            myExcelWorksheet.get_Range("B8").Formula = txt_inp_moto_B8.Text;
            myExcelWorksheet.get_Range("B9").Formula = txt_inp_moto_B9.Text;
            //myExcelWorksheet.get_Range("C27").Formula = txt_inp_moto_C27.Text;
            //myExcelWorksheet.get_Range("D19").Formula = txt_inp_moto_D19.Text;
            myExcelWorksheet.get_Range("D29").Formula = txt_inp_moto_D29.Text;
            myExcelWorksheet.get_Range("D31").Formula = txt_inp_moto_D31.Text;
            myExcelWorksheet.get_Range("D47").Formula = txt_inp_moto_D47.Text;
            myExcelWorksheet.get_Range("D53").Formula = txt_inp_moto_D53.Text;
            myExcelWorksheet.get_Range("D6").Formula = txt_inp_moto_D6.Text;
            myExcelWorksheet.get_Range("E23").Formula = txt_inp_moto_E23.Text;
            myExcelWorksheet.get_Range("E50").Formula = txt_inp_moto_E50.Text;
            myExcelWorksheet.get_Range("F13").Formula = txt_inp_moto_F13.Text;
            myExcelWorksheet.get_Range("F14").Formula = txt_inp_moto_F14.Text;
            myExcelWorksheet.get_Range("F16").Formula = txt_inp_moto_F16.Text;
            myExcelWorksheet.get_Range("F17").Formula = txt_inp_moto_F17.Text;
            myExcelWorksheet.get_Range("F18").Formula = txt_inp_moto_F18.Text;
            //myExcelWorksheet.get_Range("F27").Formula = txt_inp_moto_F27.Text;
            myExcelWorksheet.get_Range("F28").Formula = txt_inp_moto_F28.Text;




            myExcelWorksheet.get_Range("F29").Formula = txt_inp_moto_F29.Text;
            myExcelWorksheet.get_Range("F30").Formula = txt_inp_moto_F30.Text;
            myExcelWorksheet.get_Range("F31").Formula = txt_inp_moto_F31.Text;
            myExcelWorksheet.get_Range("F33").Formula = txt_inp_moto_F33.Text;
            myExcelWorksheet.get_Range("F36").Formula = txt_inp_moto_F36.Text;
            myExcelWorksheet.get_Range("F37").Formula = txt_inp_moto_F37.Text;
            myExcelWorksheet.get_Range("F38").Formula = txt_inp_moto_F38.Text;
            myExcelWorksheet.get_Range("F39").Formula = txt_inp_moto_F39.Text;
            myExcelWorksheet.get_Range("F4").Formula = txt_inp_moto_F4.Text;
            myExcelWorksheet.get_Range("F46").Formula = txt_inp_moto_F46.Text;
            myExcelWorksheet.get_Range("F47").Formula = txt_inp_moto_F47.Text;
            myExcelWorksheet.get_Range("F49").Formula = txt_inp_moto_F49.Text;
            myExcelWorksheet.get_Range("F52").Formula = txt_inp_moto_F52.Text;
            myExcelWorksheet.get_Range("F53").Formula = txt_inp_moto_F53.Text;
            myExcelWorksheet.get_Range("F9").Formula = txt_inp_moto_F9.Text;
            //myExcelWorksheet.get_Range("G19").Formula = txt_inp_moto_G19.Text;
            myExcelWorksheet.get_Range("G23").Formula = txt_inp_moto_G23.Text;
            myExcelWorksheet.get_Range("H10").Formula = txt_inp_moto_H10.Text;
            myExcelWorksheet.get_Range("H11").Formula = txt_inp_moto_H11.Text;
            myExcelWorksheet.get_Range("H12").Formula = txt_inp_moto_H12.Text;
            myExcelWorksheet.get_Range("H13").Formula = txt_inp_moto_H13.Text;
            myExcelWorksheet.get_Range("H14").Formula = txt_inp_moto_H14.Text;
            myExcelWorksheet.get_Range("H16").Formula = txt_inp_moto_H16.Text;
            myExcelWorksheet.get_Range("H17").Formula = txt_inp_moto_H17.Text;
            myExcelWorksheet.get_Range("H21").Formula = txt_inp_moto_H21.Text;
            myExcelWorksheet.get_Range("H22").Formula = txt_inp_moto_H22.Text;
            myExcelWorksheet.get_Range("H29").Formula = txt_inp_moto_H29.Text;
            myExcelWorksheet.get_Range("H31").Formula = txt_inp_moto_H31.Text;
            myExcelWorksheet.get_Range("H34").Formula = txt_inp_moto_H34.Text;
            myExcelWorksheet.get_Range("H35").Formula = txt_inp_moto_H35.Text;
            myExcelWorksheet.get_Range("H50").Formula = txt_inp_moto_H50.Text;
            myExcelWorksheet.get_Range("I23").Formula = txt_inp_moto_I23.Text;
            //myExcelWorksheet.get_Range("I27").Formula = txt_inp_moto_I27.Text;
            myExcelWorksheet.get_Range("I33").Formula = txt_inp_moto_I33.Text;
            myExcelWorksheet.get_Range("I49").Formula = txt_inp_moto_I49.Text;
            myExcelWorksheet.get_Range("I52").Formula = txt_inp_moto_I52.Text;
            //myExcelWorksheet.get_Range("J19").Formula = txt_inp_moto_J19.Text;
            myExcelWorksheet.get_Range("J29").Formula = txt_inp_moto_J29.Text;
            myExcelWorksheet.get_Range("J31").Formula = txt_inp_moto_J31.Text;
            myExcelWorksheet.get_Range("K38").Formula = txt_inp_moto_K38.Text;
            myExcelWorksheet.get_Range("K39").Formula = txt_inp_moto_K39.Text;




            int cl_no = 0;


            #region Section 1

            //B56
            //E56
            //H56
            //K56
            //N56
            //E57
            cl_no = 56;
            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_1.Section_Name + " " + uC_moto_sec_1.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_1.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_1.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_1.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_1.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_1.Area;

            #endregion Section 1

            #region Section 2

            //B59
            //E59
            //H59
            //K59
            //N59
            //E60
            cl_no = 59;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_2.Section_Name + " " + uC_moto_sec_2.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_2.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_2.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_2.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_2.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_2.Area;

            #endregion Section 2

            #region Section 3
            //B62
            //E62
            //H62
            //K62
            //N62
            //E63
            cl_no = 62;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_3.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_3.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_3.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_3.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_3.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_3.Area;

            #endregion Section 2

            #region Section 4


            //B65
            //E65
            //H65
            //K65
            //E66
            //H66
            //K66
            //N66
            cl_no = 65;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_4.Section_Name + " " + uC_moto_sec_3.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_4.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_4.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_4.Cyy;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_4.Area;
            myExcelWorksheet.get_Range("H" + (cl_no + 1)).Formula = uC_moto_sec_4.tf;
            myExcelWorksheet.get_Range("K" + (cl_no + 1)).Formula = uC_moto_sec_4.tw;
            myExcelWorksheet.get_Range("N" + (cl_no + 1)).Formula = uC_moto_sec_4.Ixx;
            //myExcelWorksheet.get_Range("N66").Formula = uC_moto_sec_4.Iyy;

            #endregion Section 4

            #region Section 5


            //B68
            //E68
            //H68
            //K68
            //E69
            //H69
            //K69
            //N69

            cl_no = 68;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_5.Section_Name + " " + uC_moto_sec_5.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_5.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_5.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_5.Cyy;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_5.Area;
            myExcelWorksheet.get_Range("H" + (cl_no + 1)).Formula = uC_moto_sec_5.tf;
            myExcelWorksheet.get_Range("K" + (cl_no + 1)).Formula = uC_moto_sec_5.tw;
            myExcelWorksheet.get_Range("N" + (cl_no + 1)).Formula = uC_moto_sec_5.Ixx;
            //myExcelWorksheet.get_Range("N66").Formula = uC_moto_sec_4.Iyy;

            #endregion Section 5

            #region Section 6

            //            B71
            //E71
            //H71
            //K71
            //E72
            //H72
            //K72
            //N72


            cl_no = 71;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_6.Section_Name + " " + uC_moto_sec_6.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_6.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_6.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_6.Cyy;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_6.Area;
            myExcelWorksheet.get_Range("H" + (cl_no + 1)).Formula = uC_moto_sec_6.tf;
            myExcelWorksheet.get_Range("K" + (cl_no + 1)).Formula = uC_moto_sec_6.tw;
            myExcelWorksheet.get_Range("N" + (cl_no + 1)).Formula = uC_moto_sec_6.Ixx;
            //myExcelWorksheet.get_Range("N66").Formula = uC_moto_sec_4.Iyy;

            #endregion Section 6

            #region Section 7

            //B74
            //E74
            //H74
            //K74
            //E75
            //H75
            //K75
            //N75


            cl_no = 74;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_7.Section_Name + " " + uC_moto_sec_7.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_7.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_7.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_7.Cyy;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_7.Area;
            myExcelWorksheet.get_Range("H" + (cl_no + 1)).Formula = uC_moto_sec_7.tf;
            myExcelWorksheet.get_Range("K" + (cl_no + 1)).Formula = uC_moto_sec_7.tw;
            myExcelWorksheet.get_Range("N" + (cl_no + 1)).Formula = uC_moto_sec_7.Ixx;
            //myExcelWorksheet.get_Range("N66").Formula = uC_moto_sec_7.Iyy;

            #endregion Section 7

            #region Section 8

            //B77
            //E77
            //H77
            //K77
            //E78
            //H78
            //K78
            //N78
            cl_no = 77;
            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_8.Section_Name + " " + uC_moto_sec_8.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_8.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_8.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_8.Cyy;
            myExcelWorksheet.get_Range("E75" + (cl_no + 1)).Formula = uC_moto_sec_8.Area;
            myExcelWorksheet.get_Range("H75" + (cl_no + 1)).Formula = uC_moto_sec_8.tf;
            myExcelWorksheet.get_Range("K75" + (cl_no + 1)).Formula = uC_moto_sec_8.tw;
            myExcelWorksheet.get_Range("N75" + (cl_no + 1)).Formula = uC_moto_sec_8.Ixx;
            //myExcelWorksheet.get_Range("N75").Formula = uC_moto_sec_8.Iyy;

            #endregion Section 8

            #region Section 9

            //B80
            //E80
            //H80
            //K80
            //N80
            //E81
            cl_no = 80;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_9.Section_Name + " " + uC_moto_sec_9.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_9.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_9.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_9.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_9.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_9.Area;

            #endregion Section 9

            #region Section 10

            //B83
            //E83
            //H83
            //K83
            //E84
            //H84
            //K84
            //N84

            cl_no = 83;
            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_10.Section_Name + " " + uC_moto_sec_10.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_10.h;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_10.b;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_10.Cyy;
            myExcelWorksheet.get_Range("E75" + (cl_no + 1)).Formula = uC_moto_sec_10.Area;
            myExcelWorksheet.get_Range("H75" + (cl_no + 1)).Formula = uC_moto_sec_10.tf;
            myExcelWorksheet.get_Range("K75" + (cl_no + 1)).Formula = uC_moto_sec_10.tw;
            myExcelWorksheet.get_Range("N75" + (cl_no + 1)).Formula = uC_moto_sec_10.Ixx;
            //myExcelWorksheet.get_Range("N75").Formula = uC_moto_sec_8.Iyy;

            #endregion Section 10

            #region Section 11

            //B86
            //E86
            //H86
            //K86
            //N86
            //E87

            cl_no = 86;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_11.Section_Name + " " + uC_moto_sec_11.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_11.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_11.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_11.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_11.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_11.Area;

            #endregion Section 11


            #region Section 12

            //B86
            //E86
            //H86
            //K86
            //N86
            //E87

            cl_no = 89;

            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_12.Section_Name + " " + uC_moto_sec_12.Section_Size;
            myExcelWorksheet.get_Range("E" + cl_no).Formula = uC_moto_sec_12.D;
            myExcelWorksheet.get_Range("H" + cl_no).Formula = uC_moto_sec_12.Zxx;
            myExcelWorksheet.get_Range("K" + cl_no).Formula = uC_moto_sec_12.tw;
            myExcelWorksheet.get_Range("N" + cl_no).Formula = uC_moto_sec_12.Ixx;
            myExcelWorksheet.get_Range("E" + (cl_no + 1)).Formula = uC_moto_sec_12.Area;

            #endregion Section 12


            #endregion Motorable Superstructure


            #region footbridge superstructure

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["footbridge superstructure"];



            myExcelWorksheet.get_Range("C3").Formula = txt_inp_foot_1.Text;
            myExcelWorksheet.get_Range("C4").Formula = txt_inp_foot_2.Text;
            myExcelWorksheet.get_Range("C5").Formula = txt_inp_foot_3.Text;

            myExcelWorksheet.get_Range("C19").Formula = txt_inp_foot_4.Text;
            myExcelWorksheet.get_Range("C9").Formula = txt_inp_foot_5.Text;
            myExcelWorksheet.get_Range("C10").Formula = txt_inp_foot_6.Text;
            myExcelWorksheet.get_Range("C11").Formula = txt_inp_foot_7.Text;
            myExcelWorksheet.get_Range("C23").Formula = txt_inp_foot_8.Text;
            myExcelWorksheet.get_Range("C24").Formula = txt_inp_foot_9.Text;
            myExcelWorksheet.get_Range("C25").Formula = txt_inp_foot_10.Text;



            //int cl_no = 0;


            #region Section 1

            //C85
            //F85
            //I85
            //L85
            //F86
            //I86

            cl_no = 52;
            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_1.Section_Name + " " + uC_foot_sec_1.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_1.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_1.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_1.tw;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_1.Area;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_1.Ixx;

            #endregion Section 1

            #region Section 2

            //C88
            //F88
            //I88
            //L88
            //F89
            //I89


            cl_no = cl_no + 3;
            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_2.Section_Name + " " + uC_foot_sec_2.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_2.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_2.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_2.tw;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_2.Area;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_2.Ixx;

            #endregion Section 2


            #region Section 3

            //C91
            //F91
            //I91
            //L91
            //O91
            //F92
            //I92
            //L92
            //O92
            cl_no = cl_no + 3;

            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_3.Section_Name + " " + uC_foot_sec_3.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_3.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_3.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_3.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_3.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_3.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_3.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_3.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_3.Iyy;

            #endregion Section 3

            #region Section 4

            cl_no = cl_no + 3;

            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_4.Section_Name + " " + uC_foot_sec_4.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_4.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_4.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_4.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_4.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_4.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_4.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_4.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_4.Iyy;

            #endregion Section 4


            #region Section 5

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_5.Section_Name + " " + uC_foot_sec_5.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_5.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_5.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_5.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_5.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_5.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_5.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_5.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_5.Iyy;

            #endregion Section 5


            #region Section 6

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_6.Section_Name + " " + uC_foot_sec_6.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_6.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_6.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_6.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_6.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_6.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_6.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_6.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_6.Iyy;

            #endregion Section 6

            #region Section 7

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_7.Section_Name + " " + uC_foot_sec_7.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_7.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_7.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_7.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_7.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_7.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_7.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_7.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_7.Iyy;

            #endregion Section 7

            #region Section 8

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_8.Section_Name + " " + uC_foot_sec_8.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_8.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_8.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_8.tw;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_8.Area;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_8.Ixx;

            #endregion Section 8

            #region Section 9

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_9.Section_Name + " " + uC_foot_sec_9.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_9.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_9.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_9.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_9.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_9.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_9.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_9.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_9.Iyy;

            #endregion Section 9

            #region Section 10

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_10.Section_Name + " " + uC_foot_sec_10.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_10.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_10.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_10.Cyy;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_10.Area;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_10.tf;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_10.tw;
            myExcelWorksheet.get_Range("L" + (cl_no + 1)).Formula = uC_foot_sec_10.Ixx;
            myExcelWorksheet.get_Range("O" + (cl_no + 1)).Formula = uC_foot_sec_10.Iyy;

            #endregion Section 10

            #region Section 11

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_11.Section_Name + " " + uC_foot_sec_11.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_11.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_11.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_11.tw;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_11.Area;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_11.Ixx;

            #endregion Section 11

            #region Section 12

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_12.Section_Name + " " + uC_foot_sec_12.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_12.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_12.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_12.tw;
            myExcelWorksheet.get_Range("F" + (cl_no + 1)).Formula = uC_foot_sec_12.Area;
            myExcelWorksheet.get_Range("I" + (cl_no + 1)).Formula = uC_foot_sec_12.Ixx;

            #endregion Section 12

            #endregion footbridge superstructure


            #region abutment design

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["abutment design"];

            myExcelWorksheet.get_Range("C9").Formula = txt_susp_abut_H1.Text;
            myExcelWorksheet.get_Range("C22").Formula = txt_susp_abut_H3.Text;
            myExcelWorksheet.get_Range("C24").Formula = txt_susp_abut_H4.Text;

            double h1, h2, h3, h4;


            h1 = MyList.StringToDouble(txt_susp_abut_H1.Text, 0.0);
            h2 = MyList.StringToDouble(txt_susp_abut_H2.Text, 0.0);
            h3 = MyList.StringToDouble(txt_susp_abut_H3.Text, 0.0);
            h4 = MyList.StringToDouble(txt_susp_abut_H4.Text, 0.0);
            myExcelWorksheet.get_Range("B15").Formula = (h1 + h2 + h3 + h4).ToString("f3");




            myExcelWorksheet.get_Range("C25").Formula = txt_susp_abut_B1.Text;
            myExcelWorksheet.get_Range("D26").Formula = txt_susp_abut_B2.Text;
            //myExcelWorksheet.get_Range("D25").Formula = txt_susp_abut_B3.Text;
            myExcelWorksheet.get_Range("E25").Formula = txt_susp_abut_B4.Text;
            //myExcelWorksheet.get_Range("F25").Formula = txt_susp_abut_B5.Text;
            myExcelWorksheet.get_Range("G25").Formula = txt_susp_abut_B6.Text;
            myExcelWorksheet.get_Range("H25").Formula = txt_susp_abut_B7.Text;


            myExcelWorksheet.get_Range("I12").Formula = txt_susp_abut_RL1.Text;
            myExcelWorksheet.get_Range("I15").Formula = txt_susp_abut_RL2.Text;
            myExcelWorksheet.get_Range("I17").Formula = txt_susp_abut_RL3.Text;
            myExcelWorksheet.get_Range("I20").Formula = txt_susp_abut_RL4.Text;
            myExcelWorksheet.get_Range("B24").Formula = txt_susp_abut_RL5.Text;


            #endregion abutment design


            myExcelWorkbook.Save();

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

        private void btn_cable_open_des_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string file_path = "";


            //file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            file_path = Get_Project_Folder();

            if (btn.Name == btn_cable_open_des.Name)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    file_path = Path.Combine(file_path, "Design of Deck Structure.xlsx");
                else
                    file_path = Path.Combine(file_path, "Design of Deck Structure BS.xlsx");

            }

            else if ((btn.Name == btn_cable_open.Name))
            {
                iApp.Open_ASTRA_Worksheet_Dialog();
                return;
            }
            if (File.Exists(file_path))
            {
                iApp.OpenExcelFile(file_path, "2011ap");
            }
            else
            {
                MessageBox.Show("File not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
