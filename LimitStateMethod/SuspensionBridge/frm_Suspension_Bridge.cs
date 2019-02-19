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

using BridgeAnalysisDesign;


using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace LimitStateMethod.SuspensionBridge
{
    
    public partial class frm_Suspension_Bridge : Form
    {
        IApplication iApp = null;


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



        public frm_Suspension_Bridge(IApplication iapp)
        {
            InitializeComponent();
            this.iApp = iapp;
        }
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "CABLE SUSPENSION BRIDGE [BS]";
                return "CABLE SUSPENSION BRIDGE [IRC]";
            }
        }
          public bool IsCreateData = false;

        private void frm_Suspension_Bridge_Load(object sender, EventArgs e)
        {
            Set_Project_Name();
            Load_Default_Data();


            //Modified_Cell(dgv_cable_suspension);
            //Modified_Cell(dgv_wind_guy);
            Modified_Cell(dgv_est_steel);
            Calculate_Estimate_Steel();


            //Load_Beam_Sections(cmb_sec_1_name);
            //Load_Beam_Sections(cmb_sec_2_name);
            //Load_Angle_Sections(cmb_sec_3_name);
            //Load_Angle_Sections(cmb_sec_4_name);
            //Load_Angle_Sections(cmb_sec_5_name);
            //Load_Angle_Sections(cmb_sec_6_name);

            //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            //{

            //    cmb_sec_1_code.SelectedItem = "250";
            //    cmb_sec_2_code.SelectedItem = "450";
            //}
            //else
            //{
            //    //cmb_sec_1_code.Items
            //    cmb_sec_1_code.SelectedItem = "254X146X37";
            //    cmb_sec_2_code.SelectedItem = "457X152X74";
            //}
            Loads_Suspension_Sections();

            Calculate_Load();



            #region Bearings

            //Chiranjit [2016 03 1]
            uC_BRD1.iApp = iApp;
            uC_BRD1.Load_Default_Data();
            iApp.user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title); ;


            #endregion Bearings




        }
        void Loads_Suspension_Sections()
        {
            #region Tower Analysis

            uC_Sections1.iApp = iApp;
            uC_Sections2.iApp = iApp;
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                uC_Sections2.Section_Name = "ISA";
                uC_Sections2.Section_Size = "65X65X6";

                uC_Sections1.Section_Name = "ISA";
                uC_Sections1.Section_Size = "200X200X18";
            }
            else
            {
                uC_Sections2.Convert_IS_to_BS("ISA", "65X65X6");
                uC_Sections1.Convert_IS_to_BS("ISA", "200X200X18");
            }

            #endregion Tower Analysis



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

        void Load_Default_Data()
        {
            List<string> list = new List<string>();

            #region Estimate for Steel

            list.Clear();
            list.Add(string.Format("1$TOP/BOTTOM CHORD$$$$"));
            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$2UKA200*200*25$322.5x1$73.9$8$190662.00"));
            else
                list.Add(string.Format("a)$2ISA200*200*25$322.5x1$73.9$8$190662.00"));
            list.Add(string.Format("b)$Packing Plt.25Thk.$0.15x0.42$196.25$176$2176.02"));
            list.Add(string.Format("2$VERTICAL$$$$"));

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$2UKA75*75*8$2.8x1$8.9$520$12958.40"));
            else
                list.Add(string.Format("a)$2ISA75*75*8$2.8x1$8.9$520$12958.40"));

            list.Add(string.Format("b)$Packing 10Thk.$0.075x0.075$78.50$1040$459.23"));
            list.Add(string.Format("3$DIAGONALS$$$$"));


            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$2UKA75*75*8$3.52x1$8.9$516$16165.25"));
            else 
                list.Add(string.Format("a)$2ISA75*75*8$3.52x1$8.9$516$16165.25"));

            list.Add(string.Format("b)$Packing 10Thk.$0.075x0.16$774.00$1806$16774.13"));
            list.Add(string.Format("4$SIDE STRUT$$$$"));
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$2UKA100*100*8$3.24x1$12.1$520$20386.08"));
            else
                list.Add(string.Format("a)$2ISA100*100*8$3.24x1$12.1$520$20386.08"));
            list.Add(string.Format("b)$Packing  12Thk.$0.1x0.05$94.20$520$244.92"));
            list.Add(string.Format("c)$Side Strut Connecting Bottom Plt.12Thk.$0.325x0.15$94.20$260$1193.99"));
            list.Add(string.Format("d)$Side Strut Gusset Bottom Plt.12Thk.$0.315x0.242$94.20$260$1867.03"));
            list.Add(string.Format("e)$Side Strut Connecting Top Plt.12Thk.$0.36x0.175$94.20$172$1020.75"));
            list.Add(string.Format("f)$Side Strut Gusset Top Plt.12Thk.$0.227x0.179$94.20$172$658.35"));
            list.Add(string.Format("g)$Side Strut Connecting Top Plt.12Thk.$0.27x0.175$94.20$88$391.68"));
            list.Add(string.Format("h)$Side Strut Gusset Top Plt.12Thk.$0.227x0.154$94.20$88$289.79"));
            list.Add(string.Format("5$ROAD BEARER$$$$"));


            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("i)$UKB 254x146x37$2.5x1$37.3$645$60146.25"));
            else
                list.Add(string.Format("i)$MB250$2.5x1$37.3$645$60146.25"));

            list.Add(string.Format("6$TRANSOM$$$$"));


            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$UKB 457x152x74 (Avg.)$6.025x1$72.4$130$56707.30"));
            else
                list.Add(string.Format("a)$MB450 (Avg.)$6.025x1$72.4$130$56707.30"));
            


            list.Add(string.Format("b)$Packing Plt.8Thk.$0.15x0.125$62.80$520$612.30"));
            list.Add(string.Format("c)$10Thk.Plt. For U Bolt $0.32x0.225$78.50$260$1469.52"));
            list.Add(string.Format("d)$Wind Guy Plt. 8Thk. $0.1x0.1$62.80$520$326.56"));
            list.Add(string.Format("e)$Bearing Plate 20Thk.$0.3x0.3$157.00$4$56.52"));
            list.Add(string.Format("7$BOTTOM BRACING$$$$"));
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                list.Add(string.Format("a)$2UKA65*65*8$4.8x1$7.7$258$9535.68"));
            else
                list.Add(string.Format("a)$2ISA65*65*8$4.8x1$7.7$258$9535.68"));
            list.Add(string.Format("b)$Packing In Bottom Bracing Member 8Thk.$0.16x0.075$62.80$910$685.78"));
            list.Add(string.Format("8$SPLICE PLATE$$$$"));
            list.Add(string.Format("a)$Top Splice 25Thk.$1.01x0.42$196.25$88$7325.93"));
            list.Add(string.Format("b)$Inner Splice 25Thk.$1.01x0.175$196.25$176$6104.95"));
            list.Add(string.Format("c)$Bottom Splice 25Thk.$1.17x0.42$196.25$84$8100.73"));
            list.Add(string.Format("d)$Inner Splice 25Thk.$1.01x0.175$196.25$168$5827.45"));
            list.Add(string.Format("9$GUSSET PLATE$$$$"));
            list.Add(string.Format("a)$Bottom Bracing Gusset Plt. 8Thk.$0.482x0.15$62.80$130$590.26"));
            list.Add(string.Format("b)$Top Gusset Plate (T-Type)$0.36x0.335$78.50$86$814.17"));
            list.Add(string.Format("c)$Bottom Gusset Plate (R-Type)$0.24x0.335$78.50$84$530.16"));
            list.Add(string.Format("d)$Top Gusset Plate (S-Type)$0.574x0.417$78.50$86$1615.91"));
            list.Add(string.Format("e)$Bottom Gusset Plate (Q-Type)$0.55x0.422$78.50$84$1530.47"));
            list.Add(string.Format("f)$Top Gusset Plate (U-Type)$1.01x0.437$78.50$88$3048.98"));
            list.Add(string.Format("g)$Bottom Gusset Plate (P-Type)$1.01x0.422$78.50$20$669.17"));
            list.Add(string.Format("h)$Bottom Gusset Plate (O-Type)$1.01x0.355$78.50$36$1013.26"));
            list.Add(string.Format("i)$Gusset Plate (Center)$0.52x0.17$78.50$10$69.39"));
            list.Add(string.Format("10$FLOORING PLATE$$$$"));
            list.Add(string.Format("a)$CHEQ.PLT.8Thk. $1.798x1.248$68.98$516$79868.80"));
            list.Add(string.Format("b)$M.S.FLAT(150x8) @ 180 c/c$1.765x0.15$62.80$4128$68633.37"));
            list.Add(string.Format("c)$Bottom Angle ISA50*50*6$322.5x1$4.50$4$5805.00"));
            list.Add(string.Format("d)$Flat 50*6Thk.$322.5x0.05$47.10$1$759.49"));
            list.Add(string.Format("11$WHEEL GUARD$$$$"));
            list.Add(string.Format("a)$Built up section$322.5x0.31$23.55$2$4708.82"));
            list.Add(string.Format("b)$Cleat MC125$0.1x1$13.10$1040$1362.40"));
            list.Add(string.Format("12$FIXER$$$$"));
            list.Add(string.Format("a)$Spacer Plate 16Th.$1x0.1$125.6$1040$13062.40"));
            list.Add(string.Format("b)$U-Clamp With Transom Dia22mm$0.32x1$2.99$520$497.536"));
            list.Add(string.Format("c)$U-Clamp With Main Rope Dia22mm$0.9x1$2.99$520$1399.32"));
            list.Add(string.Format("13$RAILING$$$$"));
            list.Add(string.Format("a)$Pipe  25mm Dia$325x1$2.01$6$3919.50"));
            list.Add(string.Format("b)$Hanger$0.175x0.075$62.8$780$642.92"));
            list.Add(string.Format(" $Total Weight in Kg$$$$"));
            list.Add(string.Format(" $Total Weight in Ton$$$$"));


            MyList.Fill_List_to_Grid(dgv_est_steel, list, '$');
            #endregion Estimate for Steel

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
                    //if (s1 != "" && s2 == "")
                    if (s3 == "")
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
            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, txt_project_name.Text);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
        }

        private void Design_Cable_Suspension_Bridge()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design of Cable Suspension Bridge.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\Cable Suspension Bridge Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            iApp.Excel_Open_Message();

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


            myExcelWorksheet.get_Range("J1054").Formula = uC_moto_sec_11.Section_Name ;
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

            iApp.Excel_Close_Message();
        }

        private void Design_BS_Cable_Suspension_Bridge()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design of Cable Suspension Bridge BS.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\BS Cable Suspension Bridge Design.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            iApp.Excel_Open_Message();

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
            myExcelWorksheet.get_Range("B" + cl_no).Formula = uC_moto_sec_1.Section_Name + " " +  uC_moto_sec_1.Section_Size;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_4.Area;
            myExcelWorksheet.get_Range("H" +  (cl_no + 1)).Formula = uC_moto_sec_4.tf;
            myExcelWorksheet.get_Range("K" +  (cl_no + 1)).Formula = uC_moto_sec_4.tw;
            myExcelWorksheet.get_Range("N" +  (cl_no + 1)).Formula = uC_moto_sec_4.Ixx;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_5.Area;
            myExcelWorksheet.get_Range("H" +  (cl_no + 1)).Formula = uC_moto_sec_5.tf;
            myExcelWorksheet.get_Range("K" +  (cl_no + 1)).Formula = uC_moto_sec_5.tw;
            myExcelWorksheet.get_Range("N" +  (cl_no + 1)).Formula = uC_moto_sec_5.Ixx;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_6.Area;
            myExcelWorksheet.get_Range("H" +  (cl_no + 1)).Formula = uC_moto_sec_6.tf;
            myExcelWorksheet.get_Range("K" +  (cl_no + 1)).Formula = uC_moto_sec_6.tw;
            myExcelWorksheet.get_Range("N" +  (cl_no + 1)).Formula = uC_moto_sec_6.Ixx;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_7.Area;
            myExcelWorksheet.get_Range("H" +  (cl_no + 1)).Formula = uC_moto_sec_7.tf;
            myExcelWorksheet.get_Range("K" +  (cl_no + 1)).Formula = uC_moto_sec_7.tw;
            myExcelWorksheet.get_Range("N" +  (cl_no + 1)).Formula = uC_moto_sec_7.Ixx;
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
            myExcelWorksheet.get_Range("E75" +  (cl_no + 1)).Formula = uC_moto_sec_8.Area;
            myExcelWorksheet.get_Range("H75" +  (cl_no + 1)).Formula = uC_moto_sec_8.tf;
            myExcelWorksheet.get_Range("K75" +  (cl_no + 1)).Formula = uC_moto_sec_8.tw;
            myExcelWorksheet.get_Range("N75" +  (cl_no + 1)).Formula = uC_moto_sec_8.Ixx;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_9.Area;

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
            myExcelWorksheet.get_Range("E75" +  (cl_no + 1)).Formula = uC_moto_sec_10.Area;
            myExcelWorksheet.get_Range("H75" +  (cl_no + 1)).Formula = uC_moto_sec_10.tf;
            myExcelWorksheet.get_Range("K75" +  (cl_no + 1)).Formula = uC_moto_sec_10.tw;
            myExcelWorksheet.get_Range("N75" +  (cl_no + 1)).Formula = uC_moto_sec_10.Ixx;
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
            myExcelWorksheet.get_Range("E" +  (cl_no + 1)).Formula = uC_moto_sec_11.Area;

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
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_1.Area;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_1.Ixx;

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
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_2.Area;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_2.Ixx;

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
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_3.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_3.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_3.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_3.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_3.Iyy;

            #endregion Section 3

            #region Section 4

            cl_no = cl_no + 3;

            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_4.Section_Name + " " + uC_foot_sec_4.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_4.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_4.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_4.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_4.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_4.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_4.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_4.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_4.Iyy;

            #endregion Section 4


            #region Section 5

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_5.Section_Name + " " + uC_foot_sec_5.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_5.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_5.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_5.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_5.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_5.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_5.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_5.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_5.Iyy;

            #endregion Section 5


            #region Section 6

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_6.Section_Name + " " + uC_foot_sec_6.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_6.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_6.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_6.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_6.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_6.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_6.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_6.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_6.Iyy;

            #endregion Section 6

            #region Section 7

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_7.Section_Name + " " + uC_foot_sec_7.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_7.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_7.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_7.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_7.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_7.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_7.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_7.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_7.Iyy;

            #endregion Section 7

            #region Section 8

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_8.Section_Name + " " + uC_foot_sec_8.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_8.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_8.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_8.tw;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_8.Area;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_8.Ixx;

            #endregion Section 8

            #region Section 9

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_9.Section_Name + " " + uC_foot_sec_9.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_9.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_9.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_9.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_9.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_9.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_9.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_9.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_9.Iyy;

            #endregion Section 9

            #region Section 10

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_10.Section_Name + " " + uC_foot_sec_10.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_10.h;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_10.b;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_10.Cyy;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_10.Area;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_10.tf;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_10.tw;
            myExcelWorksheet.get_Range("L" +  (cl_no + 1)).Formula = uC_foot_sec_10.Ixx;
            myExcelWorksheet.get_Range("O" +  (cl_no + 1)).Formula = uC_foot_sec_10.Iyy;

            #endregion Section 10

            #region Section 11

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_11.Section_Name + " " + uC_foot_sec_11.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_11.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_11.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_11.tw;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_11.Area;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_11.Ixx;

            #endregion Section 11

            #region Section 12

            cl_no = cl_no + 3;


            myExcelWorksheet.get_Range("C" + cl_no).Formula = uC_foot_sec_12.Section_Name + " " + uC_foot_sec_12.Section_Size;
            myExcelWorksheet.get_Range("F" + cl_no).Formula = uC_foot_sec_12.D;
            myExcelWorksheet.get_Range("I" + cl_no).Formula = uC_foot_sec_12.Zxx;
            myExcelWorksheet.get_Range("L" + cl_no).Formula = uC_foot_sec_12.tw;
            myExcelWorksheet.get_Range("F" +  (cl_no + 1)).Formula = uC_foot_sec_12.Area;
            myExcelWorksheet.get_Range("I" +  (cl_no + 1)).Formula = uC_foot_sec_12.Ixx;

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

            iApp.Excel_Close_Message();
        }

        private void Design_Wind_Guy_Analysis()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Design of Wind Guy.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\Wind Guy Analysis.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["WindGuy"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);



            #region Section Input

            myExcelWorksheet.get_Range("E11").Formula = txt_wind_guy_E11.Text;
            myExcelWorksheet.get_Range("E3").Formula = txt_wind_guy_E3.Text;
            myExcelWorksheet.get_Range("E5").Formula = txt_wind_guy_E5.Text;
            myExcelWorksheet.get_Range("E7").Formula = txt_wind_guy_E7.Text;
            myExcelWorksheet.get_Range("E9").Formula = txt_wind_guy_E9.Text;

            myExcelWorksheet.get_Range("G11").Formula = txt_wind_guy_G11.Text;
            myExcelWorksheet.get_Range("G3").Formula = txt_wind_guy_G3.Text;
            myExcelWorksheet.get_Range("G5").Formula = txt_wind_guy_G5.Text;
            myExcelWorksheet.get_Range("G7").Formula = txt_wind_guy_G7.Text;
            myExcelWorksheet.get_Range("G9").Formula = txt_wind_guy_G9.Text;
      

            #endregion Section Input

            //DataGridView dgv = dgv_sidl;
            int rindx = 0;


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }


        private void Design_Estimate_for_Steel()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "Estimate for Steel.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\Estimate for Steel.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Estimate Steel"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();


            DataGridView dgv = dgv_est_steel;
            int rindx = 0;


            rindx = 0;

            #region Input

            MyList mlist = null;
            for (int i = 0; i < dgv.RowCount - 2; i++)
            {
                myExcelWorksheet.get_Range("A" + (i + 3)).Formula = dgv[0, i].Value.ToString();
                myExcelWorksheet.get_Range("B" + (i + 3)).Formula = dgv[1, i].Value.ToString();
                //myExcelWorksheet.get_Range("C" + (i + 3)).Formula = dgv[2, i].Value.ToString();



                mlist = new MyList(dgv[2, i].Value.ToString().Replace("*", "x"), 'x');

                if (mlist.Count > 1)
                {
                    myExcelWorksheet.get_Range("C" + (i + 3)).Formula = mlist.StringList[0];
                    myExcelWorksheet.get_Range("D" + (i + 3)).Formula = mlist.StringList[1];
                }
                myExcelWorksheet.get_Range("E" + (i + 3)).Formula = dgv[3, i].Value.ToString();
                myExcelWorksheet.get_Range("F" + (i + 3)).Formula = dgv[4, i].Value.ToString();

            }

            #endregion Input





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


        public string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
            }
        }
        //{
        //    get
        //    {
        //        return Get_Project_Folder();
        //    }
        //}

        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Construction Drawings"), "SUSPENSION_BRIDGE_IS");
            }
            else
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Construction Drawings"), "SUSPENSION_BRIDGE_BS");
            }

        }

        private void btn_wind_guy_proceed_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Design_Wind_Guy_Analysis();
        }

        private void btn_cable_proceed_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                Design_Cable_Suspension_Bridge();
            else
                Design_BS_Cable_Suspension_Bridge();
        }

        private void btn_est_steel_proceed_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Design_Estimate_for_Steel();
        }

        private void dgv_est_steel_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Calculate_Estimate_Steel();
        }
        void Calculate_Estimate_Steel()
        {
            double s, u, q, w;


            DataGridView dgv = dgv_est_steel;

            double total = 0.0;
            for (int i = 0; i < dgv.RowCount-1; i++)
            {
                //s = MyList.Get_Expression_Result(dgv[2, i].Value.ToString());
                //u = MyList.StringToDouble(dgv[3, i].Value.ToString(),0.0);
                //q = MyList.StringToDouble(dgv[4, i].Value.ToString(),0.0);
                //w = s * u * q;
                try
                {

                    s = MyList.Get_Expression_Result(dgv[2, i].Value.ToString());
                    u = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                    q = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);
                    w = s * u * q;
                    dgv[5, i].Value = w.ToString("f3");

                    if(w == 0)
                        dgv[5, i].Value = "";

                    total += w;
                }
                catch (Exception ee) { }
            }
            if (dgv.RowCount > 2)
            {
                dgv[5, dgv.RowCount - 2].Value = total.ToString("f1");
                dgv[5, dgv.RowCount - 1].Value = (total / 1000.0).ToString("f3");
            }


        }

        private void btn_cable_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string file_path = "";

         
            //file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            file_path = Get_Project_Folder();


            if ((btn.Name == btn_cable_open.Name) 
                || (btn.Name == btn_wind_guy_open.Name) 
                || (btn.Name == btn_est_steel_open.Name) )
            {
                iApp.Open_ASTRA_Worksheet_Dialog();
                return;
            }
            else if (btn.Name == btn_cable_open_des.Name)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    file_path = Path.Combine(file_path, "Design of Cable Suspension Bridge.xlsx");
                else
                    file_path = Path.Combine(file_path, "Design of Cable Suspension Bridge BS.xlsx");

            }
            else if (btn.Name == btn_wind_guy_open_des.Name)
            {
                file_path = Path.Combine(file_path, "Design of Wind Guy.xlsx");
                //file_path = Path.Combine(file_path, "Analysis for Wind Guy.xlsx");
            }
            else if (btn.Name == btn_est_steel_open_des.Name)
            {
                file_path = Path.Combine(file_path, "Estimate for Steel.xlsx");
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

        private void btn_open_ana_inp_browse_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files(*.txt)|*.txt";

                if(ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_ana_input_data.Text = ofd.FileName;
                }
            }

            //if(File.Exists(txt_ana_input_data.Text))
            //{
            //    iApp.Form_ASTRA_TEXT_Data(txt_ana_input_data.Text, false).Show();
            //}
        }


        public string Working_Folder
        {
            get
            {
                //return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return user_path;
            }
        }
        public string Analysis_File
        {
            get
            {

                if (!Directory.Exists(Working_Folder)) Directory.CreateDirectory(Working_Folder);

                string fpath = Path.Combine(Working_Folder, "Tower Analysis");

                if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

                return Path.Combine(fpath, "Tower_Analysis.txt");


            }
        }

        public string Seismic_File
        {
            get
            {
                string seis_inp_file = Path.Combine(Path.GetDirectoryName(Analysis_File), @"Seismic_Analysis\Seismic_Analysis.txt");
                return seis_inp_file;
            }
        }


        private void btn_load_example_data_Click(object sender, EventArgs e)
        {
            Load_Example();

        }

        private void Load_Example()
        {

            string tower_file = Analysis_File;


            string ss = Path.Combine(Application.StartupPath, @"DESIGN\Suspension Bridge\Tower Analysis\Tower_Inp.txt");

            if (File.Exists(ss))
            {
                File.WriteAllLines(tower_file, File.ReadAllLines(ss));
            }

            if (File.Exists(tower_file))
            {
                txt_ana_input_data.Text = tower_file;
                //MessageBox.Show("Example Data Loaded in File \"" + tower_file + "\"");
                MessageBox.Show("Data file Created as file " + Analysis_File);
            }
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (File.Exists(Analysis_File))
            {
                iApp.Form_ASTRA_TEXT_Data(Analysis_File, false).ShowDialog();
            }
            else
            {
                MessageBox.Show("Analysis Input Data file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            string rep_file = MyList.Get_Analysis_Report_File(Analysis_File);
            string seis_rep_file = MyList.Get_Analysis_Report_File(Seismic_File);

            BridgeMemberAnalysis dbd = null;

            if (File.Exists(seis_rep_file) && File.Exists(rep_file))
            {
                frm_OpenInput fin = new frm_OpenInput();
                if (fin.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                switch (fin.Option)
                {
                    case 1:
                        if (File.Exists(rep_file)) dbd = new BridgeMemberAnalysis(iApp, rep_file);
                        break;
                    case 2:
                        if (File.Exists(seis_rep_file)) dbd = new BridgeMemberAnalysis(iApp, seis_rep_file);
                        break;
                }
            }
            else
            {
                if (File.Exists(rep_file)) dbd = new BridgeMemberAnalysis(iApp, rep_file);
            }

            if (sps.Vertical_Members == "")
                Create_data();
            if (dbd != null)
            {

               
                CMember cm = new CMember(iApp);
                cm.Group.MemberNosText = sps.Vertical_Members;
                cm.Group.SetMemNos();
                cm.MemberType = eMemberType.VerticalMember;

                double ax = dbd.Get_Max_Axial_Force(cm.Group.MemberNos);
                //dbd.GetForce(ref cm);

                txt_vertical_force.Text = ax.ToString();

                txt_inp_moto_H21.Text = ax.ToString();

                cm.Group.MemberNosText = sps.Diagonal_Members;
                cm.Group.SetMemNos();
                cm.MemberType = eMemberType.VerticalMember;
                ax = dbd.Get_Max_Axial_Force(cm.Group.MemberNos);
                txt_diagonal_force.Text = ax.ToString();
                txt_inp_moto_H22.Text = ax.ToString();
            }
        }

        private void btn_open_analysis_Click(object sender, EventArgs e)
        {

            string rep_file =  MyList.Get_Analysis_Report_File(Analysis_File);
            string seis_rep_file =  MyList.Get_Analysis_Report_File(Seismic_File);

            int opt = 0;
            frm_OpenInput fin = new frm_OpenInput();


            Button btn = sender as Button;

            if (btn.Name == btn_open_analysis_report.Name)
            {
                if (File.Exists(rep_file) && File.Exists(seis_rep_file))
                {
                    if (fin.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) opt = fin.Option;

                    if (opt == 1) System.Diagnostics.Process.Start(rep_file);
                    if (opt == 2) System.Diagnostics.Process.Start(seis_rep_file);
                }
                else
                {
                    if (File.Exists(rep_file))
                        System.Diagnostics.Process.Start(rep_file);
                }
            }
            else if (btn.Name == btn_view_data.Name)
            {
                if (File.Exists(Analysis_File) && File.Exists(Seismic_File))
                {
                    if (fin.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) opt = fin.Option;

                    if (opt == 1) System.Diagnostics.Process.Start(Analysis_File);
                    if (opt == 2) System.Diagnostics.Process.Start(Seismic_File);
                }
                else
                {
                    if (File.Exists(Analysis_File))
                        System.Diagnostics.Process.Start(Analysis_File);
                }
            }
            else
            {
                MessageBox.Show("Analysis Input Data file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            };


        }

        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {

            if (showMessage) DemoCheck();

            iApp.Save_Form_Record(this, user_path);
        }
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_TWA_hgt.Text = "30.0";
                txt_TWA_brc_hgt.Text = "2.0";
                txt_TWA_base_wd.Text = "3.0";


                txt_inp_moto_B3.Text = "325.0";
                txt_inp_moto_B4.Text = "5.0";
                txt_inp_moto_F4.Text = "3.6";



                txt_inp_foot_1.Text = "325.0";

                txt_wind_guy_G3.Text = "325.0";
                txt_wind_guy_G5.Text = "325.0";
                txt_wind_guy_G7.Text = "325.0";
                txt_wind_guy_G9.Text = "325.0";



                txt_inp_moto_B3.Text = "325.0";
                txt_inp_moto_B4.Text = "5.0";
                txt_inp_moto_F4.Text = "3.6";



                txt_inp_foot_1.Text = "325.0";
                txt_inp_foot_2.Text = "0.8";
                txt_inp_foot_3.Text = "0.75";
                
                Loads_Suspension_Sections();
                
            }
        }


        private bool Check_Project_Folder()
        {

            if (Path.GetFileName(user_path) != Project_Name)
            {
                MessageBox.Show(this, "New Project is not created. Please create New Project.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }
        Suspension_Bridge sps = new Suspension_Bridge();

        bool Create_data()
        {

            sps.Tower_Height = MyList.StringToDouble(txt_TWA_hgt.Text, 0.0);
            sps.Bracing_Panel_Height = MyList.StringToDouble(txt_TWA_brc_hgt.Text, 0.0);
            sps.Tower_Base_Width = MyList.StringToDouble(txt_TWA_base_wd.Text, 0.0);
            sps.Tower_Top_Width = MyList.StringToDouble(txt_TWA_top_wd.Text, 0.0);
            sps.Tower_Lower_Connector_Width = MyList.StringToDouble(txt_TWA_lower_cntr.Text, 0.0);
            sps.Tower_Upper_Connector_Width = MyList.StringToDouble(txt_TWA_upper_cntr.Text, 0.0);
            sps.Tower_Clear_Distance = MyList.StringToDouble(txt_TWA_clear_distance.Text, 0.0);


            sps.Tower_Dead_Load = MyList.StringToDouble(txt_TWA_dead_load.Text, 0.0);
            sps.Tower_Live_Load = MyList.StringToDouble(txt_TWA_live_load.Text, 0.0);
            sps.Tower_Seismic_Coefficient = MyList.StringToDouble(txt_TWA_seismic_coefficient.Text, 0.0);

            sps.Tower_SEC_VS_AX = MyList.StringToDouble(txt_TWA_sec_vs_A.Text, 0.0);
            sps.Tower_SEC_VS_IX = MyList.StringToDouble(txt_TWA_sec_vs_IX.Text, 0.0);
            sps.Tower_SEC_VS_IZ = MyList.StringToDouble(txt_TWA_sec_vs_IZ.Text, 0.0);


            sps.Tower_SEC_BS_AX = MyList.StringToDouble(txt_TWA_sec_bs_AX.Text, 0.0);
            sps.Tower_SEC_BS_IX = MyList.StringToDouble(txt_TWA_sec_bs_IX.Text, 0.0);
            sps.Tower_SEC_BS_IZ = MyList.StringToDouble(txt_TWA_sec_bs_IZ.Text, 0.0);

            //if (sps.Tower_Height == 30.0 &&

            //    sps.Bracing_Panel_Height == 2.0 &&
            //    sps.Tower_Base_Width == 3.0 &&
            //    sps.Tower_Top_Width == 1.5 &&
            //    sps.Tower_Lower_Connector_Width == 10.0 &&
            //    sps.Tower_Upper_Connector_Width == 24.0 &&
            //    sps.Tower_Clear_Distance == 4.5

            //    )
            //{
            //    Load_Example();
            //}
            //else
            //{
            return (sps.Create_Data(Analysis_File));
        }
        private void btn_create_data_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;


            if (!Check_Project_Folder()) return;

            if (btn.Name == btn_create_data.Name)
            {


                if (Path.GetFileName(user_path) != Project_Name) Create_Project();


                Write_All_Data();


                if (Create_data())
                {

                    MessageBox.Show("Data file Created as file " + Analysis_File);
                }
                //}
            }
            else if (btn.Name == btn_view_data.Name)
            {
                if (File.Exists(Analysis_File))
                    System.Diagnostics.Process.Start(Analysis_File);
                else
                {
                    MessageBox.Show("Analysis Input Data file not found.");
                }
            }

        }

        private void cmb_sec_1_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            //if (cmb.Name == cmb_sec_1_name.Name) Load_sections_code(cmb.Text, cmb_sec_1_code);
            //else if (cmb.Name == cmb_sec_2_name.Name) Load_sections_code(cmb.Text, cmb_sec_2_code);
            //else if (cmb.Name == cmb_sec_3_name.Name) Load_sections_code(cmb.Text, cmb_sec_3_code);
            //else if (cmb.Name == cmb_sec_4_name.Name) Load_sections_code(cmb.Text, cmb_sec_4_code);
            //else if (cmb.Name == cmb_sec_5_name.Name) Load_sections_code(cmb.Text, cmb_sec_5_code);
            //else if (cmb.Name == cmb_sec_6_name.Name) Load_sections_code(cmb.Text, cmb_sec_6_code);
        }
        private void Load_sections(ComboBox cmb_name)
        {

            List<string> sec_names = new List<string>();

            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            foreach (var item in tbl_rolledSteelChannels.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }

            //cmb_section_1
        }
        private void Load_Channel_Sections(ComboBox cmb_name)
        {
            foreach (var item in tbl_rolledSteelChannels.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }
        private void Load_Angle_Sections(ComboBox cmb_name)
        {
            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }
        private void Load_Beam_Sections(ComboBox cmb_name)
        {
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }
        private void Load_sections_code(string name, ComboBox cmb_code)
        {


            List<string> sec_names = new List<string>();

            //ComboBox cmb = cmb_sec_1_code;



            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionSize + "X" + item.Thickness); 
                }
            }
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionCode); 
                }
            }
            foreach (var item in tbl_rolledSteelChannels.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionCode); 
                }
            }

            //cmb_section_1
        }

        private void Load_Angle_Sections_code(string name, ComboBox cmb_code)
        {
            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionSize + "X" + item.Thickness);
                }
            }
        }
        private void Load_Beam_Sections_code(string name, ComboBox cmb_code)
        {
            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionCode);
                }
            }
        }
        private void Load_Channel_Sections_code(string name, ComboBox cmb_code)
        {

            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelChannels.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionCode);
                }
            }
        }

        private void Load_sections_details(string name, string code, int opt)
        {


            List<string> sec_names = new List<string>();

            double Zxx = 0.0, Ixx = 0.0, D = 0.0, tw = 0.0;


            MyList ml = new MyList(code, 'X');


            double ts = 0.0;

            if (name.EndsWith("A"))
            {
                ts = ml.GetDouble(2);
                code = ml[0] + "X" + ml[1];
            }
            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if ((item.SectionName == name) && item.SectionSize == code && item.Thickness == ts)
                {
                    //propertyGrid1.SelectedObject = item; break;
                }
            }
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if ((item.SectionName == name) && item.SectionCode == code)
                {
                    //propertyGrid1.SelectedObject = item; 
                    D = item.Depth;
                    tw = item.WebThickness;
                    Ixx = item.Ixx;
                    break;
                }
            }
            foreach (var item in tbl_rolledSteelChannels.List_Table)
            {
                if ((item.SectionName == name) && item.SectionCode == code)
                {
                    //propertyGrid1.SelectedObject = item;
                    D = item.Depth;
                    tw = item.WebThickness;
                    Ixx = item.Ixx;
                    break;
                }
            }
            //int opt = 1;

           D =  D/10.0;
           Zxx = Ixx / (D / 2.0);

            //if (opt == 1)
            //{
            //    txt_inp_moto_D19.Text = (D).ToString("f3");
            //    txt_inp_moto_G19.Text = Zxx.ToString("f3");
            //    txt_inp_moto_J19.Text = tw.ToString("f3");
            //}
            //else if (opt == 2)
            //{
            //    txt_inp_moto_I27.Text = (D).ToString("f3");
            //    txt_inp_moto_C27.Text = Zxx.ToString("f3");
            //    txt_inp_moto_F27.Text = tw.ToString("f3");
            //}
            //else if (opt == 3)
            //{
            //    //txt_inp_moto_I27.Text = (D).ToString("f3");
            //    //txt_inp_moto_C27.Text = Zxx.ToString("f3");
            //    //txt_inp_moto_F27.Text = tw.ToString("f3");
            //}
            //else if (opt == 4)
            //{
            //    txt_inp_foot_E6.Text = (D * 10).ToString("f3");
            //}
            //else if (opt == 5)
            //{
            //    txt_inp_foot_E7.Text = (D * 10).ToString("f3");
            //}
            //else if (opt == 6)
            //{
            //    txt_inp_foot_E12.Text = (D).ToString("f3");
            //    txt_inp_foot_H12.Text = Zxx.ToString("f3");
            //    txt_inp_foot_J12.Text = tw.ToString("f3");
            //}
        }
        private void cmb_sec_1_code_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = sender as ComboBox;

            //if (cmb.Name == cmb_sec_1_code.Name) Load_sections_details(cmb_sec_1_name.Text, cmb.Text, 1);
            //else if (cmb.Name == cmb_sec_2_code.Name) Load_sections_details(cmb_sec_2_name.Text, cmb.Text, 2);
            //else if (cmb.Name == cmb_sec_3_code.Name) Load_sections_details(cmb_sec_3_name.Text, cmb.Text, 3);
            //else if (cmb.Name == cmb_sec_4_code.Name) Load_sections_details(cmb_sec_4_name.Text, cmb.Text, 4);
            //else if (cmb.Name == cmb_sec_5_code.Name) Load_sections_details(cmb_sec_5_name.Text, cmb.Text, 5);
            //else if (cmb.Name == cmb_sec_6_code.Name) Load_sections_details(cmb_sec_6_name.Text, cmb.Text, 6);
        }

        private void uC_Sections1_Changed(object sender, EventArgs e)
        {
            UserControl uc = sender as UserControl;
            if (uc == null) return;

            if (uc.Name == uC_Sections1.Name)
            {
                txt_TWA_sec_vs_A.Text = uC_Sections1.Area.ToString("f3");
                txt_TWA_sec_vs_IX.Text = uC_Sections1.Ixx.ToString("f3");
                txt_TWA_sec_vs_IZ.Text = uC_Sections1.Izz.ToString("f3");
            }

            else if (uc.Name == uC_Sections2.Name)
            {
                txt_TWA_sec_bs_AX.Text = uC_Sections2.Area.ToString("f3");
                txt_TWA_sec_bs_IX.Text = uC_Sections2.Ixx.ToString("f3");
                txt_TWA_sec_bs_IZ.Text = uC_Sections2.Izz.ToString("f3");
            }

        }

        private void btn_susp_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_susp_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //user_path = frm.Example_Path;
                    //user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);


                    user_path = frm.Example_Path;

                    //Open_Project();



                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As



                    //Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);
                    iApp.user_path = user_path;

                    Write_All_Data();
                }
            }
            else if (btn.Name == btn_susp_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreateData = true;
                //}
                IsCreateData = true;
                Create_Project();
            }
            //Button_Enable_Disable();
            //Button();
        }
        #region Chiranjit [2016 09 07]
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }

        public void All_Button_Enable(bool flag)
        {
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Cable_Suspension_Bridge;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]

        private void txt_TWA_L_TextChanged(object sender, EventArgs e)
        {
            Calculate_Load();

        }

        private void Calculate_Load()
        {
            double L = MyList.StringToDouble(txt_TWA_L);
            double B = MyList.StringToDouble(txt_TWA_B);
            double W = 0.0;

            W = (L / 325.0) * (B / 6.0) * 1200;

            txt_TWA_applied_load.Text = W.ToString("f2");
            txt_TWA_dead_load.Text = (W / 16).ToString("f2");
            txt_inp_moto_B3.Text = L.ToString();
            txt_inp_moto_B4.Text  = B.ToString();
        }

        private void txt_TWA_applied_load_TextChanged(object sender, EventArgs e)
        {
            double W = MyList.StringToDouble(txt_TWA_applied_load);
            txt_TWA_dead_load.Text = (W / 16).ToString("f2");
        }

    }


    public class Suspension_Bridge
    {

        //Height of Tower = 30.0 m
        //Each Bracing Panel height = 2.0m
        //Width of Each Tower at Base = 3.0m
        //Width of each Tower at Top = 1.5m
        //Lower Tower Connector at height = 10.0m (check)
        //Upper Tower Connector at height = 20.0m (check)
        #region Properties
        public double Tower_Height { get; set; }
        public double Bracing_Panel_Height { get; set; }
        public double Tower_Base_Width { get; set; }
        public double Tower_Top_Width { get; set; }
        public double Tower_Lower_Connector_Width { get; set; }
        public double Tower_Upper_Connector_Width { get; set; }
        public double Tower_Clear_Distance { get; set; }

        public double Tower_Dead_Load { get; set; }
        public double Tower_Live_Load { get; set; }
        public double Tower_Seismic_Coefficient { get; set; }

        public double Tower_SEC_VS_AX { get; set; }
        public double Tower_SEC_VS_IX { get; set; }
        public double Tower_SEC_VS_IZ { get; set; }

        public double Tower_SEC_BS_AX { get; set; }
        public double Tower_SEC_BS_IX { get; set; }
        public double Tower_SEC_BS_IZ { get; set; }

        public string Vertical_Members { get; set; }
        public string Diagonal_Members { get; set; }
        #endregion Properties

        public Suspension_Bridge()
        {
            Tower_Height = 30.0;
            Bracing_Panel_Height = 2.0;
            Tower_Base_Width = 3.0;
            Tower_Top_Width = 1.5;
            Tower_Lower_Connector_Width = 10.0;
            Tower_Upper_Connector_Width = 20.0;
            Tower_Clear_Distance = 4.5;

            Vertical_Members = "";
            Diagonal_Members = "";


            Tower_Dead_Load = 55.0;
            Tower_Live_Load = 55.0;
            Tower_Seismic_Coefficient = 55.0;

            Tower_SEC_VS_AX = 68.81;
            Tower_SEC_VS_IX = 1046.5;
            Tower_SEC_VS_IZ = 1046.5;

            Tower_SEC_BS_AX = 7.44;
            Tower_SEC_BS_IX = 11.7;
            Tower_SEC_BS_IZ = 11.7;

        }

        public bool Create_Data(string file_name)
        {
            List<string> list = new List<string>();

            
            //con
            int Upper_Connector_Index = -1;
            int Lower_Connector_Index = -1;


            int indx = 12;

           

            int pnl_nos = (int)(Tower_Height / Bracing_Panel_Height);
            #region X Points


            double x_ps = (Tower_Base_Width - Tower_Top_Width) / 2;
            double X_Incr = (x_ps) / (pnl_nos);



            List<double> x_side_1 = new List<double>();
            List<double> x_side_2 = new List<double>();

            List<double> y_side = new List<double>();
            List<double> z_side_1 = new List<double>();
            List<double> z_side_2 = new List<double>();


            double x = 0, y = 0, z = 0;


            int i = 0;


            for (i = 0; i <= pnl_nos; i++)
            {
                x = i * X_Incr;
                y = i * Bracing_Panel_Height;

                x_side_1.Add(x);
                z_side_1.Add(x);

                x_side_2.Add(Tower_Base_Width - x);
                z_side_2.Add(Tower_Base_Width - x);


                y_side.Add(y);

            }


            i = 0;

            #endregion X Points


            //     Tower_Lower_Connector_Width = 10.0;
            //Tower_Upper_Connector_Width = 20.0;

            if (Tower_Upper_Connector_Width != 0.0)
            {
                for (i = 0; i < y_side.Count; i++)
                {
                    if (Tower_Upper_Connector_Width.ToString("f3") == y_side[i].ToString("f3"))
                    {
                        Upper_Connector_Index = i; break;
                    }
                }
            }

            if(Tower_Lower_Connector_Width != 0.0)
            {
                for (i = 0; i < y_side.Count; i++)
                {
                    if (Tower_Lower_Connector_Width.ToString("f3") == y_side[i].ToString("f3"))
                    {
                        Lower_Connector_Index = i; break;
                    }
                }
            }

            JointNodeCollection jnc_1, jnc_2, jnc_3, jnc_4; // First Tower


            JointNodeCollection jnc_5, jnc_6, jnc_7, jnc_8;// Second Tower

            jnc_1 = new JointNodeCollection();
            jnc_2 = new JointNodeCollection();
            jnc_3 = new JointNodeCollection();
            jnc_4 = new JointNodeCollection();

            jnc_5 = new JointNodeCollection();
            jnc_6 = new JointNodeCollection();
            jnc_7 = new JointNodeCollection();
            jnc_8 = new JointNodeCollection();


            double x_dist = Tower_Base_Width + Tower_Clear_Distance;



            JointNode jn = null;

            #region Side 1
            for (i = 0; i < y_side.Count; i++)
            {
                #region First Tower
                jn = new JointNode();

                jn.X = x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_1.Add(jn);
                #endregion First Tower


                #region Second Tower
                jn = new JointNode();

                jn.X = x_dist + x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_6.Add(jn);
                #endregion Second Tower

            }

            #endregion Side 1


            #region Side 2
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_2.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_5.Add(jn);
            }

            #endregion Side 2


            #region Side 3
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_3.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_8.Add(jn);
            }

            #endregion Side 3


            #region Side 4
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_4.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_7.Add(jn);
            }

            #endregion Side 4


            JointNodeCollection con_jnt_1 = new JointNodeCollection();


            //indx = 12;

            indx = 12;



            double mid_x = (x_dist + Tower_Base_Width) / 2.0;

            if (Upper_Connector_Index != -1)
            {
                indx = Upper_Connector_Index;

                #region  Connector 1

                #region Connector Joint 1

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 1

                #region Connetor Joint 2

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 2


                indx++;
                #region Connetor Joint 3

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 3

                #region Connetor Joint 4

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 4

                #endregion  Connector 1
            }
            indx = 5;

            JointNodeCollection con_jnt_2 = new JointNodeCollection();

            if (Lower_Connector_Index != -1)
            {
                indx = Lower_Connector_Index;

                #region  Connector 2

                #region Connector Joint 1

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 1

                #region Connetor Joint 2

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 2


                indx++;
                #region Connetor Joint 3

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 3

                #region Connetor Joint 4

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 4

                #endregion  Connector 2
            }
            list.Add(string.Format(""));
            list.Add(string.Format("ASTRA SPACE TOWER"));
            list.Add(string.Format("UNIT MTON METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            int jnt_no = 1;

            #region Joint Coordinates
            foreach (var item in jnc_1)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_2)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_3)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_4)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_5)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_6)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_7)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_8)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }


            foreach (var item in con_jnt_1)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in con_jnt_2)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            #endregion Joint Coordinates



            int mem_no = 1;


            Member mbr = new Member();

            MemberCollection m_side_1 = new MemberCollection();



            List<int> sec_1 = new List<int>();
            List<int> sec_2 = new List<int>();

            #region Tower 1
            #region Member Side 1 [1-15]
            for (i = 1; i < jnc_1.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i - 1];
                mbr.EndNode = jnc_1[i];

                m_side_1.Add(mbr);


                sec_1.Add(mem_no++);
            }

            #endregion Member Side 1

            #region Member Side 2 [16-30]

            for (i = 1; i < jnc_2.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_2[i - 1];
                mbr.EndNode = jnc_2[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 2

            #region Member Side 3 [31-45]


            for (i = 1; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_3[i - 1];
                mbr.EndNode = jnc_3[i];

                m_side_1.Add(mbr);

                sec_1.Add(mem_no++);
            }

            #endregion Member Side 3

            #region Member Side 4 [46-60]


            for (i = 1; i < jnc_4.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_4[i - 1];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 4

            #region Member 61-76


            for (i = 0; i < jnc_1.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i];
                mbr.EndNode = jnc_2[i];

                m_side_1.Add(mbr);

                sec_2.Add(mem_no++);

            }

            #endregion Member 61-76

            #region Member 77-92


            for (i = 0; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_3[i];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 93-108


            for (i = 0; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i];
                mbr.EndNode = jnc_3[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 109-124


            for (i = 0; i < jnc_2.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_2[i];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 109-124

            #region Member 125-139


            for (i = 1; i < jnc_1.Count; i += 2)
            {
                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_3[i - 1];
                    mbr.EndNode = jnc_1[i];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_1[i];
                    mbr.EndNode = jnc_3[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                }
                catch (Exception ex) { }
            }

            #endregion Member 109-124

            #region Member 140-154


            for (i = 1; i < jnc_2.Count; i += 2)
            {
                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_2[i - 1];
                    mbr.EndNode = jnc_4[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_4[i];
                    mbr.EndNode = jnc_2[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                }
                catch (Exception exx) { }
            }

            #endregion Member 140-154

            #region Member 155-169


            for (i = 1; i < jnc_3.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_4[i - 1];
                    mbr.EndNode = jnc_3[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);



                    mbr = new Member();
                    mbr.StartNode = jnc_3[i];
                    mbr.EndNode = jnc_4[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169

            #region Member 170-184

            for (i = 1; i < jnc_1.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_1[i - 1];
                    mbr.EndNode = jnc_2[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_2[i];
                    mbr.EndNode = jnc_1[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169
            #endregion Tower 1



            #region Tower 2

            #region Member Side 1 [185-199]

            JointNodeCollection jnc = jnc_5;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 1

            #region Member Side 2 [200-214]

            jnc = jnc_6;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 2

            #region Member Side 3 [215-229]


            jnc = jnc_7;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 3

            #region Member Side 4 [230-244]

            jnc = jnc_8;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 4

            #region Member 245-260


            for (i = 0; i < jnc_5.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_5[i];
                mbr.EndNode = jnc_6[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 61-76

            #region Member 261-276


            for (i = 0; i < jnc_7.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_7[i];
                mbr.EndNode = jnc_8[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 277-292


            for (i = 0; i < jnc_7.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_5[i];
                mbr.EndNode = jnc_7[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 293-308


            for (i = 0; i < jnc_6.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_6[i];
                mbr.EndNode = jnc_8[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 109-124

            #region Member 309-323


            for (i = 1; i < jnc_5.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_7[i - 1];
                    mbr.EndNode = jnc_5[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_5[i];
                    mbr.EndNode = jnc_7[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 109-124

            #region Member 324-338


            for (i = 1; i < jnc_6.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_6[i - 1];
                    mbr.EndNode = jnc_8[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_8[i];
                    mbr.EndNode = jnc_6[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 140-154

            #region Member 339-353


            for (i = 1; i < jnc_7.Count; i += 2)
            {

                try
                {
                    mbr = new Member();
                    mbr.StartNode = jnc_8[i - 1];
                    mbr.EndNode = jnc_7[i];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);

                    mbr = new Member();
                    mbr.StartNode = jnc_7[i];
                    mbr.EndNode = jnc_8[i + 1];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 155-169

            #region Member 354-368

            for (i = 1; i < jnc_5.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_5[i - 1];
                    mbr.EndNode = jnc_6[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                    mbr = new Member();
                    mbr.StartNode = jnc_6[i];
                    mbr.EndNode = jnc_5[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169

            #endregion Tower 2



            MemberCollection m_conn = new MemberCollection();


            if (Upper_Connector_Index != -1)
            {
                #region Connector 1


                indx = Upper_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);



                //indx = 12;
                indx = Upper_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_1[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[0];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[1];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[0];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[2];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);







                //indx = 12;
                indx = Upper_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[3];
                mbr.EndNode = jnc_8[indx];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx + 1];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[1];
                mbr.EndNode = jnc_6[indx + 1];
                m_conn.Add(mbr);


                #endregion Connector
            }


            if (Lower_Connector_Index != -1)
            {
                #region Connector 2

                //MemberCollection m_conn = new MemberCollection();

                //indx = 5;
                indx = Lower_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);



                //indx = 5;
                indx = Lower_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_2[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[0];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[1];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[0];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[2];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);



                //indx = 5;
                indx = Lower_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[3];
                mbr.EndNode = jnc_8[indx];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx + 1];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[1];
                mbr.EndNode = jnc_6[indx + 1];
                m_conn.Add(mbr);


                #endregion Connector
            }
            mem_no = 1;


            #region MEMBER INCIDENCES
            list.Add("MEMBER INCIDENCES");
            foreach (var item in m_side_1)
            {
                item.MemberNo = mem_no++;
                list.Add(item.ToString());
                
            }
            foreach (var item in m_conn)
            {
                sec_2.Add(mem_no);

                item.MemberNo = mem_no++;
                list.Add(item.ToString());

            }

            #endregion MEMBER INCIDENCES

            #region MEMBER PROPERTY


            list.Add(string.Format("UNIT KG CM"));
            list.Add(string.Format("MEMBER PROPERTY"));
            //list.Add(string.Format("{0} PRISMATIC AX 7.44 IX 11.7 IY 11.7 IZ 11.7", MyList.Get_Array_Text(sec_2)));
            //list.Add(string.Format("{0} PRISMATIC AX 68.81 IX 1046.5 IY 1046.5 IZ 1046.5", MyList.Get_Array_Text(sec_1)));

            //list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", MyList.Get_Array_Text(sec_1), Tower_SEC_VS_AX, Tower_SEC_VS_IX, Tower_SEC_VS_IX, Tower_SEC_VS_IZ));
            //list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", MyList.Get_Array_Text(sec_2), Tower_SEC_BS_AX, Tower_SEC_BS_IX, Tower_SEC_BS_IX, Tower_SEC_BS_IZ));
            //list.Add(string.Format("{0} PRISMATIC AX 68.81 IX 1046.5 IY 1046.5 IZ 1046.5", MyList.Get_Array_Text(sec_1)));

            Vertical_Members = MyList.Get_Array_Text(sec_1);
            Diagonal_Members = MyList.Get_Array_Text(sec_2);
            list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IZ {3}", Vertical_Members, Tower_SEC_VS_AX, Tower_SEC_VS_IX, Tower_SEC_VS_IZ));
            list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IZ {3}", Diagonal_Members, Tower_SEC_BS_AX, Tower_SEC_BS_IX, Tower_SEC_BS_IZ));
          
            #endregion MEMBER PROPERTY


            #region CONSTANTS & Others

            List<int> supp = new List<int>();

            supp.Add(jnc_1[0].NodeNo);
            supp.Add(jnc_2[0].NodeNo);
            supp.Add(jnc_3[0].NodeNo);
            supp.Add(jnc_4[0].NodeNo);
            supp.Add(jnc_5[0].NodeNo);
            supp.Add(jnc_6[0].NodeNo);
            supp.Add(jnc_7[0].NodeNo);
            supp.Add(jnc_8[0].NodeNo);

            string supp_jnts = MyList.Get_Array_Text(supp);

            List<int> dl = new List<int>();
            indx = jnc_1.Count-1;
            dl.Add(jnc_1[indx].NodeNo);
            dl.Add(jnc_2[indx].NodeNo);
            dl.Add(jnc_3[indx].NodeNo);
            dl.Add(jnc_4[indx].NodeNo);
            dl.Add(jnc_5[indx].NodeNo);
            dl.Add(jnc_6[indx].NodeNo);
            dl.Add(jnc_7[indx].NodeNo);
            dl.Add(jnc_8[indx].NodeNo);


            string dl_jnts = MyList.Get_Array_Text(dl);



            list.Add(string.Format("UNIT KG CM"));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 2000000 ALL"));
            list.Add(string.Format("DEN 78 ALL"));
            list.Add(string.Format("PR STEEL ALL"));
            list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 17 33 49 65 81 97 113 FIXED"));
            list.Add(string.Format("{0} FIXED", supp_jnts));
            //list.Add(string.Format("SELFWEIGHT Y -1.4"));
            list.Add(string.Format("UNIT MTON M"));

            //list.Add(string.Format("LOAD 1 LIVE LOAD"));
            //list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Live_Load));
            //list.Add(string.Format("LOAD 2 DEAD LOAD"));
            //list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Dead_Load));


            list.Add(string.Format("LOAD 1  DEAD LOAD + LIVE LOAD"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Dead_Load));
            list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Live_Load));

            list.Add(string.Format("SEISMIC COEEFICIENT {0}", Tower_Seismic_Coefficient));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));

            #endregion CONSTANTS


            File.WriteAllLines(file_name, list.ToArray());

            return true;
        }




    }
}
