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
    public partial class frm_IRC_Abutment : Form
    {
        IApplication iapp;
        public frm_IRC_Abutment(IApplication iapp)
        {
            InitializeComponent();
            this.iapp = iapp;
        }

        private void frm_IRC_Abutment_Load(object sender, EventArgs e)
        {
            Load_Default_Data();
            cmb_abut_type.SelectedIndex = 0;

            SIDL_Calculation();
        }

        void Load_Default_Data()
        {
            DataGridView dgv = dgv_sidl;

            List<string> list = new List<string>();
            MyList mlist = null;
            

            #region SIDL
            list.Clear();

            list.Add(string.Format("(i)$Railing (2 Nos.)$4$0.175$0.200$25.000$3.500"));
            list.Add(string.Format("(ii)$Railing post ( 1 nos)$2$0.275$0.900$25.000$1.238"));
            list.Add(string.Format("(iii)$Railing Kerb (1 nos.)$2$0.300$0.275$25.000$4.125"));
            list.Add(string.Format("(iv)$Crash Barrier$2$$$$16.000"));
            list.Add(string.Format("(v)$Foot path slab$2$0.000$0.000$25.000$0.000"));
            list.Add(string.Format("(vi)$Wearing Coat$$7.500$0.075$25.000$14.063"));
            list.Add(string.Format("(vii)$Serice load$$$$$20.00"));

            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion SIDL

            #region Earth Pressure
            list.Clear();
            list.Add(string.Format("Angle of internal Friction $32$deg"));
            list.Add(string.Format("Angle of Friction between soil$21$deg"));
            list.Add(string.Format("Surcharge angle$0$deg"));
            list.Add(string.Format("Angle of wall face with horizontal$90$deg"));
            list.Add(string.Format("Bulk Density of earth$2$"));
            list.Add(string.Format("Submerged Density of Earth$1$t/m3"));

            dgv = dgv_earth_pressure;
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion Earth Pressure

            #region Base Pressure
            list.Clear();
            list.Add(string.Format("Design data$$"));
            list.Add(string.Format("Safe Bearing Capacity at founding level$65$t/m2"));
            list.Add(string.Format("Bulk Density of earth g$2$t/m3"));
            list.Add(string.Format("Submerged Density of Earth gsub$1$t/m3"));
            list.Add(string.Format("Angle of internal Friction f$32$deg"));
            list.Add(string.Format("Angle of Friction between soil and conc d$21$deg"));
            list.Add(string.Format("Angle of wall face with horizontal, i$90$deg"));
            list.Add(string.Format("Grade of Concrete$M35$"));
            list.Add(string.Format("Length of abutment$12$m"));
            list.Add(string.Format("Density of RCC$2.5$t/m3"));
            list.Add(string.Format("Live Load Surcharge$1.2$m"));
            list.Add(string.Format("Thickness of approach slab$0.3$m"));
            list.Add(string.Format("Summary of Levels$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Formation level$123.5$m"));
            list.Add(string.Format("Founding Level$95.17$m"));
            list.Add(string.Format("Max. Scour level in normal case$98.81$m"));
            list.Add(string.Format("Thickness of Wearing coat$0.075$m"));
            list.Add(string.Format("Superstructure Depth$4$m"));
            list.Add(string.Format("HFL$117.79$m"));
            list.Add(string.Format("LWL$102.58$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Dimensions of Abutment$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Length of base slab$22.2$m"));
            list.Add(string.Format("Width of base slab$12$m"));
            list.Add(string.Format("Thickness of base slab$0.8$m"));
            list.Add(string.Format("Length of toe slab$8.0$m"));
            list.Add(string.Format("Thickness of Screen wall$0.8$m"));
            list.Add(string.Format("Thickness of dirt wall$0.4$m"));
            list.Add(string.Format("Width of cap (Excluding dirt wall)$1.8$m"));
            list.Add(string.Format("Thickness of cap$0.9$m"));
            list.Add(string.Format("No  of back counterforts$3$nos"));
            list.Add(string.Format("No of front counterforts$5$nos"));
            list.Add(string.Format("Thickness of internal walls$0.4$m"));
            list.Add(string.Format("Thickness of external walls$0.4$m"));
            list.Add(string.Format("Thickness of top relief slab$0.5$m"));
            list.Add(string.Format("Thickness of bottom relief slab$0.3$m"));
            list.Add(string.Format("Overhang of superstructure from C/l of bearing$0.65$m"));
            list.Add(string.Format("Expansion gap$0.05$m"));
            list.Add(string.Format("Height of bearing+pedestal$0.5$m"));
            list.Add(string.Format("Length of Heel slab beyond internal walls$0.3$m"));
            list.Add(string.Format("Length of toe slab beyond front counterfort$0.3$m"));
            list.Add(string.Format("Max. Span of the toe slab$2.925$m"));
            list.Add(string.Format("Min. Span of the toe slab$2.875$m"));


            dgv = dgv_base_pressure;
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure





            #region DEAD LOAD
            list.Clear();

            list.Add(string.Format("1$Outer Girder$2$1.325$35.000$92.750$25.000$2318.750"));
            list.Add(string.Format("2$Inner Girder$2$1.152$35.000$80.640$25.000$2016.000"));
            list.Add(string.Format("3$End Cross Girder$2$0.900$12.000$21.600$25.000$540.000"));
            list.Add(string.Format("4$Intermediate Cross Girder$1$0.650$12.000$7.800$25.000$195.000"));
            list.Add(string.Format("5$Deck Slab$1$4.500$35.000$157.500$25.000$3937.500"));
            //list.Add(string.Format(""));

            dgv = dgv_weight;
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                dgv.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion DEAD LOAD




            Modified_Cell(dgv_earth_pressure);
            Modified_Cell(dgv_base_pressure);

        }
       
        public void Calculate_Weight()
        {




            //dgv
            DataGridView dgv = dgv_weight;


            double n, a, l, u, q, w;

            double total_wgt = 0.0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    n = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0);
                    a = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                    l = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);

                    q = n * a * l;


                    dgv[5, i].Value = q.ToString("f3");

                    u = MyList.StringToDouble(dgv[6, i].Value.ToString(), 0.0);

                    w = q * u;
                    dgv[7, i].Value = w.ToString("f3");

                    total_wgt += w;
                }
                catch (Exception eer) { }
            }

            txt_total_weight.Text = total_wgt.ToString("f3");

        }
        private void btn_process_Click(object sender, EventArgs e)
        {
            //Analysis_Abutment();
            iapp.Excel_Open_Message();
            if (cmb_abut_type.SelectedIndex == 0) Design_IRC_Abutment_Bridges_Box_Type();
            else if(cmb_abut_type.SelectedIndex == 1)  Design_IRC_Abutment_Bridges_Girder_Type();
        }

        public void Analysis_Abutment()
        {
            string ana_file = Path.Combine(Get_Project_Folder(), "Abutment_Analysis_Input.txt");

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;


            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");

            List<JointNode> Joints = new List<JointNode>();
            List<Member> MemColls = new List<Member>();

            double L = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double og = MyList.StringToDouble(txt_Ana_og.Text, 0.0);
            double inc = (L - og * 2) / 4.0;

            double ws = 2.0 - og;



            JointNode jn = new JointNode();

            jn.NodeNo = 1;
            jn.X = 0.0;

            Joints.Add(jn);

            jn = new JointNode();

            jn.NodeNo = 2;
            jn.X = og + og;

            Joints.Add(jn);

            jn = new JointNode();
            jn.NodeNo = 3;
            jn.X = og + ws;

            Joints.Add(jn);

            jn = new JointNode();
            jn.NodeNo = 4;
            jn.X = og + inc;

            Joints.Add(jn);

            jn = new JointNode();
            jn.NodeNo = 5;
            jn.X = og + 2 * inc;

            Joints.Add(jn);


            jn = new JointNode();
            jn.NodeNo = 6;
            jn.X = og + 3 * inc;

            Joints.Add(jn);


            jn = new JointNode();
            jn.NodeNo = 7;
            jn.X = L - (og + ws);

            Joints.Add(jn);

            jn = new JointNode();
            jn.NodeNo = 8;
            jn.X = og + 4 * inc;

            Joints.Add(jn);

            jn = new JointNode();
            jn.NodeNo = 9;
            jn.X = L;

            Joints.Add(jn);




            Member mbr = new Member();
            mbr.MemberNo = 1;
            mbr.StartNode = Joints[0];
            mbr.EndNode = Joints[1];

            MemColls.Add(mbr);
            for (i = 2; i < Joints.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = i;
                mbr.StartNode = Joints[i - 1];
                mbr.EndNode = Joints[i];
                MemColls.Add(mbr);
            }



            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }
            list.Add("SECTION PROPERTIES");

            ////list.Add("1 2 5 6 AX");
            ////list.Add(string.Format("1 2 6 7 PRIS AX 0.036212 IX 0.00001 IY 0.000697 IZ 0.001",
            //list.Add(string.Format("1 2 7 8 PRIS AX {0} IX 0.00001 IY {1} IZ 0.001",
            //    txt_smp_i_a_sup.Text,
            //    txt_smp_i_Ix_sup.Text
            //    )); 
            
            //list.Add(string.Format("3 TO 6 PRIS AX {0} IX 0.00001 IY {1} IZ 0.001",
            //     txt_smp_i_a_mid.Text,
            //     txt_smp_i_ix_mid.Text
            //     ));


            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 2.85E6 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));
            list.Add(string.Format("SUPPORT"));
            //list.Add(string.Format("2 6   PINNED"));
            list.Add(string.Format("2 8 FIXED BUT FX MZ"));
            //list.Add(string.Format("99 100 101 102   FIXED BUT FX MZ"));






            //list.Add(string.Format("LOAD 1 DEAD LOAD"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 2 7 8 UNI GY -{0:f4}", MyList.StringToDouble(txt_Ana_DL_supp.Text, 0.0)/10.0));
            //list.Add(string.Format("3 TO 6 UNI GY -{0}", MyList.StringToDouble(txt_Ana_DL_mid.Text, 0.0)/10.0));

            //list.Add(string.Format("LOAD 2 SIDL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 8 UNI GY -{0}", MyList.StringToDouble(txt_Ana_SIDL.Text, 0.0) / 10.0));

            //list.Add(string.Format("LOAD 3 FPLL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 8 UNI GY -{0}", MyList.StringToDouble(txt_Ana_FPLL.Text, 0.0)/10.0));

            //list.Add(string.Format("PRINT SUPPORT REACTIONS"));
            //list.Add(string.Format("PERFORM ANALYSIS"));
            //list.Add(string.Format("FINISH"));


            File.WriteAllLines(ana_file, list.ToArray());

            MessageBox.Show("Input Data Created as " + ana_file, "ASTRA");

            #region Process
            try
            {
                #region Process
                //int i = 0;


                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = ana_file;
                iapp.Progress_Works.Clear();

                    if (File.Exists(flPath))
                    {
                        pd = new ProcessData();
                        pd.Process_File_Name = flPath;
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);
                        iapp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                    }


                    i++;
                    string ana_rep_file = Path.Combine(Path.GetDirectoryName(flPath), "ANALYSIS_REP");
                if (iapp.Show_and_Run_Process_List(pcol))
                {


                    BridgeMemberAnalysis DeadLoad_Analysis = new BridgeMemberAnalysis(iapp,ana_rep_file);
                }

                #endregion Process
            }
            catch (Exception ex) { }
            #endregion Process
        }
        private void Design_IRC_Abutment_Bridges_Box_Type()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "IRC Abutment Design Box Type.xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC Abutment Design.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC ABUTMENT Design_Box_Type.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DLSUP"];

 
            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();


            #region Section Input



            myExcelWorksheet.get_Range("A9").Formula = txt_sec_B1.Text;
            myExcelWorksheet.get_Range("B9").Formula = txt_sec_B2.Text;
            myExcelWorksheet.get_Range("C9").Formula = txt_sec_B3.Text;
            myExcelWorksheet.get_Range("E9").Formula = txt_sec_B4.Text;
            myExcelWorksheet.get_Range("D15").Formula = txt_sec_B5.Text;
            myExcelWorksheet.get_Range("G17").Formula = txt_sec_B6.Text;



            myExcelWorksheet.get_Range("A17").Formula = txt_sec_H1.Text;
            myExcelWorksheet.get_Range("E12").Formula = txt_sec_H2.Text;
            myExcelWorksheet.get_Range("G12").Formula = txt_sec_H3.Text;
            myExcelWorksheet.get_Range("I12").Formula = txt_sec_H4.Text;
            myExcelWorksheet.get_Range("D13").Formula = txt_sec_H5.Text;
            myExcelWorksheet.get_Range("E20").Formula = txt_sec_H6.Text;







            #endregion Section Input

            DataGridView dgv = dgv_sidl;
            int rindx = 0;

            #region SIDL Input

            string v1 = "";



            double n, w, d, uu;


            myExcelWorksheet.get_Range("L118").Formula = txt_sidl_spc1.Text;
            myExcelWorksheet.get_Range("M118").Formula = txt_sidl_spc2.Text;


            for (int i = 0; i < dgv.RowCount; i++)
            {

                n = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0);
                w = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                d = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);
                uu = MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0);

                myExcelWorksheet.get_Range("D" + (i + 117)).Formula = dgv[2, i].Value.ToString();
                myExcelWorksheet.get_Range("E" + (i + 117)).Formula = dgv[3, i].Value.ToString();
                myExcelWorksheet.get_Range("F" + (i + 117)).Formula = dgv[4, i].Value.ToString();
                myExcelWorksheet.get_Range("G" + (i + 117)).Formula = dgv[5, i].Value.ToString();


                try
                {
                    if ((n * w * d * uu) == 0.0) 
                        myExcelWorksheet.get_Range("H" + (i + 117)).Formula = dgv[6, i].Value.ToString();
                }
                catch (Exception ex0)
                {

                    //throw;
                }
            }

            #endregion SIDL Input

            double L = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double og = MyList.StringToDouble(txt_Ana_og.Text, 0.0);
            double eff_L = (L - 2 * og);


            myExcelWorksheet.get_Range("B153").Formula = txt_Ana_og.Text;
            myExcelWorksheet.get_Range("C152").Formula = (eff_L / 4.0).ToString("f3");
            myExcelWorksheet.get_Range("D160").Formula = eff_L.ToString("f3");



            #region LL
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["LL"];


            myExcelWorksheet.get_Range("A9").Formula = og.ToString();
            myExcelWorksheet.get_Range("I10").Formula = eff_L.ToString();
            myExcelWorksheet.get_Range("S9").Formula = og.ToString();
            

            #endregion LL


            #region Earth pr
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Earth pr"];

            dgv = dgv_earth_pressure;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                myExcelWorksheet.get_Range("F" + (i + 7)).Formula = dgv[1, i].Value.ToString();
            }

            #endregion Earth pr



            #region Base Pressure
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Base Pressure"];

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
                catch(Exception exx){}
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

            //iapp.Excel_Open_Message();
        }

        private void Design_IRC_Abutment_Bridges_Girder_Type()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "IRC Abutment Design Girder Type.xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Design IRC\IRC ABUTMENT Design_Girder_Type.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["DLSUP"];


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);




            List<string> list = new List<string>();


            #region Section Input



            myExcelWorksheet.get_Range("B4").Formula = txt_Ana_L.Text;
            myExcelWorksheet.get_Range("B5").Formula = txt_Ana_og.Text;
            myExcelWorksheet.get_Range("B6").Formula = txt_total_weight.Text;
            myExcelWorksheet.get_Range("B7").Formula = txt_total_sidl.Text;
           






            #endregion Section Input

            DataGridView dgv = dgv_sidl;
            int rindx = 0;

            #region SIDL Input
            //for (int i = 0; i < dgv.RowCount; i++)
            //{
            //    myExcelWorksheet.get_Range("E" + (i + 117)).Formula = dgv[2, i].Value.ToString();
            //    myExcelWorksheet.get_Range("F" + (i + 117)).Formula = dgv[3, i].Value.ToString();
            //    myExcelWorksheet.get_Range("G" + (i + 117)).Formula = dgv[4, i].Value.ToString();
            //}

            #endregion SIDL Input


            #region Earth pr
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Earth pr"];

            dgv = dgv_earth_pressure;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                myExcelWorksheet.get_Range("F" + (i + 7)).Formula = dgv[1, i].Value.ToString();
            }








            #endregion Earth pr



            #region Base Pressure
            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Base Pressure"];

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

        public string Get_Project_Folder()
        {

            string file_path = Path.Combine(iapp.LastDesignWorkingFolder, "IRC Abutment Design");

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, cmb_boq_item.Text);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
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

        private void dgv_sidl_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SIDL_Calculation();
        }
        public void SIDL_Calculation()
        {
            DataGridView dgv = dgv_sidl;

            double n, w, d, u_wt, wt;


            double tot_wt = 0.0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                n = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0);
                w = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                d = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);
                u_wt = MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0);

                wt = n * w * d * u_wt;

                if(wt == 0.0)  wt = w * d * u_wt;


                if(i == 1)
                {
                    wt = wt / MyList.StringToDouble(txt_sidl_spc1.Text, 0.0) * MyList.StringToDouble(txt_sidl_spc2.Text, 0.0);
                }
                //tot_wt += wt;

                if(wt != 0.0)
                {
                    dgv[2, i].Value = n.ToString("f0");
                    dgv[3, i].Value = w.ToString("f3");
                    dgv[4, i].Value = d.ToString("f3");
                    dgv[5, i].Value = u_wt.ToString("f3");
                    dgv[6, i].Value = wt.ToString("f3");
                }

                if (n == 0.0) dgv[2, i].Value = "";
                if (w == 0.0) dgv[3, i].Value = "";
                if (d == 0.0) dgv[4, i].Value = "";
                if (u_wt == 0.0) dgv[5, i].Value = "";

                tot_wt += MyList.StringToDouble(dgv[6, i].Value.ToString(), 0.0);
            }

            txt_total_sidl.Text = tot_wt.ToString("f3");

        }
        public void Modified_Cell( DataGridView dgv)
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

        private void dgv_weight_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Calculate_Weight();
        }

        List<string> list_weight = new List<string>();
        private void rbtn_wgt_direct_CheckedChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if(rbtn_wgt_direct.Checked)
            {

                //list.Add(string.Format("1$Outer Girder$2$1.325$35.000$92.750$25.000$2318.750"));
                //list.Add(string.Format("2$Inner Girder$2$1.152$35.000$80.640$25.000$2016.000"));
                //list.Add(string.Format("3$End Cross Girder$2$0.900$12.000$21.600$25.000$540.000"));
                //list.Add(string.Format("4$Intermediate Cross Girder$1$0.650$12.000$7.800$25.000$195.000"));
                //list.Add(string.Format("5$Deck Slab$1$4.500$35.000$157.500$25.000$3937.500"));


                string kStr = "";



                DataGridView dgv = dgv_weight;

                try
                {

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    kStr = dgv[0, i].Value.ToString();
                    for (int c = 1; c < dgv.ColumnCount; c++)
                    {
                        if (dgv[c, i].Value == null)
                            dgv[c, i].Value = "";
                        kStr = kStr + "$" + dgv[c, i].Value.ToString();
                    }
                    list.Add(kStr);
                }


                }
                catch (Exception exx) { }

                if (list.Count > 0)
                {
                    list_weight = list;
                    dgv.Rows.Clear();
                }
                
            }
            else if(rbtn_wgt_compt_sec_props.Checked)
            {

                DataGridView dgv = dgv_weight;

                MyList mlist = null;
                list = list_weight;


                if (list.Count > 0)
                {
                    dgv.Rows.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        mlist = new MyList(list[i], '$');
                        dgv.Rows.Add(mlist.StringList.ToArray());
                    }
                }

                Calculate_Weight();
            }
        }

        List<string> list_sidl = new List<string>();
        private void rbtn_sidl_direct_CheckedChanged(object sender, EventArgs e)
        {

            List<string> list = new List<string>();
            if (rbtn_sidl_direct.Checked)
            {
                string kStr = "";

                DataGridView dgv = dgv_sidl;

                try
                {

                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        kStr = dgv[0, i].Value.ToString();
                        for (int c = 1; c < dgv.ColumnCount; c++)
                        {
                            if (dgv[c, i].Value == null)
                                dgv[c, i].Value = "";
                            kStr = kStr + "$" + dgv[c, i].Value.ToString();
                        }
                        list.Add(kStr);
                    }


                }
                catch (Exception exx) { }

                if (list.Count > 0)
                {
                    list_sidl = list;
                    dgv.Rows.Clear();
                }

            }
            else if (rbtn_sidl_compt_sec_props.Checked)
            {

                DataGridView dgv = dgv_sidl;

                MyList mlist = null;
                list = list_sidl;


                if (list.Count > 0)
                {
                    dgv.Rows.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        mlist = new MyList(list[i], '$');
                        dgv.Rows.Add(mlist.StringList.ToArray());
                    }
                }

                SIDL_Calculation();
            }
            //MyList
        }

        private void cmb_abut_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            tc_main.TabPages.Clear();

            if (cmb_abut_type.SelectedIndex == 0)
            {
                tc_main.TabPages.Add(tab_Section);
                tc_main.TabPages.Add(tab_process);
            }
            else if (cmb_abut_type.SelectedIndex == 1)
            {
                tc_main.TabPages.Add(tab_DL);
                tc_main.TabPages.Add(tab_process);
            }
        }

        private void txt_sidl_spc2_TextChanged(object sender, EventArgs e)
        {
            SIDL_Calculation();
        }

        private void txt_wgt_L_TextChanged(object sender, EventArgs e)
        {
            double L = MyList.StringToDouble(txt_wgt_L.Text, 0.0);
            double W = MyList.StringToDouble(txt_wgt_W.Text, 0.0);

            DataGridView dgv = dgv_weight;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i == 0 ||
                    i == 1 ||
                    i == 4)
                {
                    dgv[4, i].Value = L.ToString("f3");
                }
                else
                {
                    dgv[4, i].Value = W.ToString("f3");
                }

            }

            txt_Ana_L.Text = L.ToString("f3");

            Calculate_Weight();
        }

    }
}
