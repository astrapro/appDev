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
using BridgeAnalysisDesign;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace LimitStateMethod.SlabBridge
{
    public partial class frmSlabBridge : Form
    {
        IApplication iApp;


        string Input_File_DL = "";


        string Input_File_LL = "";

        public string Get_LL_File(int LoadNo)
        {
            if (Input_File_LL == "") return "";
            string fl_name = Path.Combine(Path.GetDirectoryName(Input_File_LL), "LL_ANALYSIS_" + LoadNo);
            if (!Directory.Exists(fl_name)) Directory.CreateDirectory(fl_name);

            fl_name = Path.Combine(fl_name, "LL_ANALYSIS_" + LoadNo + ".txt");

            return fl_name;
        }

        public frmSlabBridge(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            Load_Default_Data();
            Load_LL();
        }
        public void Load_Default_Data()
        {
            List<string> list = new List<string>();
            #region Default Data
            list.Add(string.Format("Design Data$$"));
            list.Add(string.Format("Number of span$1$"));
            list.Add(string.Format("Clear Span$11.5$m"));
            list.Add(string.Format("Effective span$12$m"));
            list.Add(string.Format("Overall width$16$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Crash barrier width$0.5$m"));
            list.Add(string.Format("Footpath width/ Safety kerb$0.25$m"));
            list.Add(string.Format("Height of Crash barrier$1$m"));
            list.Add(string.Format("Footpath width$1.500$m"));
            list.Add(string.Format("Height of footpath slab$0.3$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Width of Railing Kerb $0.5$m"));
            list.Add(string.Format("height of rail kerb $0.25$m"));
            list.Add(string.Format("Height of railing $1$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Slab Thickness $1$m"));
            list.Add(string.Format("Length of cantilever portion $2$m"));
            list.Add(string.Format("Cantilever Tip thickness $0.2$m"));
            list.Add(string.Format("Cantilever root thickness $0.3$m"));
            list.Add(string.Format("Lean concrete over Deck Slab $0.2$m"));
            list.Add(string.Format("Minimum Thickness of Wearing coat $0.075$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Unit Weights$$"));
            list.Add(string.Format("Density of concrete$25$kN/m³"));
            list.Add(string.Format("unit weight of soil$18$kN/m³"));
            list.Add(string.Format("unit weight of submerged soil$10$kN/m³"));
            list.Add(string.Format("unit weight of wearing coat$22$kN/m³"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Material Properties$$"));
            list.Add(string.Format("Grade of concrete$35$N/mm2"));
            list.Add(string.Format("Grade of reinforcement$500$N/mm2"));
            list.Add(string.Format("Grade of shear reinforcement$500$N/mm2"));
            list.Add(string.Format("Length of the Slab considered for design$1$m"));
            list.Add(string.Format("Partial safety factor for concrete $1.5$"));
            list.Add(string.Format("Partial safety factor for steel$1.15$"));
            list.Add(string.Format("Modulus of elasticity of concrete$3.20E+04$N/mm2"));
            list.Add(string.Format("Modulus of elasticity of Steel$2.00E+05$N/mm2"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Tensile strenght of concrete$2.8$Mpa"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Moment due to self wt.$0.00$"));
            list.Add(string.Format("Moment due to SIDL$0.00$"));
            list.Add(string.Format("Total moment due to sustained loading$0.00$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("K3(for simply supported members)$0.125$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Gamma_m$1.5$"));
            list.Add(string.Format("alpha$0.67$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Creep cofficient (taking long term of creep)$1.48$"));
            list.Add(string.Format("cofficient to consider influence of concrete strength$0.67$"));

            #endregion Default Data
            MyList.Fill_List_to_Grid(dgv_design_data, list, '$');
            dgv_design_data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            MyList.Modified_Cell(dgv_design_data);
        }

        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            iApp.Save_Form_Record(this, user_path);
        }

        private void btn_TGirder_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_TGirder_browse.Name)
            {
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //user_path = Path.Combine(user_path, Project_Name);
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                        Write_All_Data();
                    }
                    #endregion Save As
                }
            }
            else if (btn.Name == btn_TGirder_new_design.Name)
            {
                Create_Project();
            }

            grb_Analysis.Enabled = (Directory.Exists(user_path));
            Button_Enable_Disable();
        }

        #region Chiranjit [2016 09 07]

        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.BOX_CULVERT_LSM;
            }
        }

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


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "RCC SLAB BRIDGE DESIGN LIMIT STATE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "RCC SLAB BRIDGE DESIGN LIMIT STATE [LRFD]";

                return "RCC SLAB BRIDGE DESIGN LIMIT STATE [IRC]";
            }
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

        #endregion

        private void Slab_Bridge_Design()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, "Worksheet Design");

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "Single cell box.xlsx");
            file_path = Excel_File();

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Solid Slab Bridge.xlsx");


            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Solid Slab Bridge BS.xlsx");
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
                    DataGridView dgv = dgv_design_data;
                    int rindx = 0;
                    int i = 0;

                    #region Design Data
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];

                    rindx = 0;


                    DataInputCollections dips = new DataInputCollections();

                    dips.Load_Data_from_Grid(dgv);

                    List<string> lst_vals = new List<string>();
                    foreach (var item in dips)
                    {
                        if (item.DATA != "")
                        {
                            //lst_vals.Add(MyList.StringToDouble(item.DATA));
                            lst_vals.Add(item.DATA);
                        }
                    }

                    List<string> ldbl = new List<string>();

                    rindx = 0;

                    //8

                    //14

                    //18

                    //25
                    //26
                    //31
                    //32

                    //41
                    //43
                    //47-49
                    //51-54
                    //57-62
                    for (i = 4; i < 65; i++)
                    {
                        if ((i == 8) ||
                            (i == 14) ||
                            (i == 18) ||
                            (i == 25) ||
                            (i == 26) ||
                            (i == 31) ||
                            (i == 32) ||
                            (i == 41) ||
                            (i == 43) ||
                            (i >= 47 && i <= 49) ||
                            (i >= 51 && i <= 54) ||
                            (i >= 57 && i <= 62)
                           )
                        {
                            continue;
                        }
                        else
                        {
                            myExcelWorksheet.get_Range("G" + i).Formula = lst_vals[rindx++].ToUpper().Replace("M", "").Replace("FE", "").Trim().ToString();

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
                            //    myExcelWorksheet.get_Range("G" + i).Formula = dips[rindx++].DATA.ToString();

                            //while (dips[rindx].DATA == "")
                            //    rindx++;
                        }
                    }
                    #endregion Design Data

                    rindx = 0;


                    if (true)
                    {
                        #region Analysis Results
                        rindx = 0;


                        myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Summary"];



                        SB_Results BR = new SB_Results(Result_File);




                        SB_Table bt = BR[0];

                        List<string> cells = new List<string>();



                        cells.Add("B");
                        cells.Add("C");
                        cells.Add("D");
                        cells.Add("E");
                        cells.Add("F");
                        cells.Add("G");
                        cells.Add("H");
                        //cells.Add("I");
                        //cells.Add("J");
                        //cells.Add("K");
                        //cells.Add("L");
                        //cells.Add("M");
                        //cells.Add("N");
                        //cells.Add("O");


                        bt = BR[0];

                        #region Top Slab 1 Bending Moment
                        rindx = 0;
                        for (i = 4; i <= 13; i++)
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

                        rindx = 0;

                        bt = BR[1];


                        cells.Clear();
                        cells.Add("B");
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
                        cells.Add("P");
                        cells.Add("Q");
                        cells.Add("R");


                        #region Top Slab 2 Bending Moment
                        rindx = 0;
                        for (i = 19; i <= 28; i++)
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
                        #endregion


                    }
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



        Slab_Bridge_Model Slab_Model;

        private void Read_Singlecell_Bending_Moment_Shear_Force()
        {

            BridgeMemberAnalysis brd = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_DL));
            //BridgeMemberAnalysis brd1 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Input_File_LL));
            BridgeMemberAnalysis brd1 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Get_LL_File(1)));
            BridgeMemberAnalysis brd2 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Get_LL_File(2)));
            BridgeMemberAnalysis brd3 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Get_LL_File(3)));
            BridgeMemberAnalysis brd4 = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(Get_LL_File(4)));

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

            List<double> dst = Slab_Model.list_X;


            #region Bottom Slab
            list.Add("");
            list.Add("");

            #region Bottom Slab 1 Bending Moment

            dst = Slab_Model.list_X;
            All_Frcs.Clear();

            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                for (int l = 1; l <= 3; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(Slab_Model.jnBots[i].NodeNo);
                    mf = brd.GetJoint_MomentForce(top_arr, l);
                    lst_frcs.Add(mf.Force);

                    mf = brd.GetJoint_ShearForce(top_arr, l);
                    lst_frcs.Add(mf.Force);
                }

                All_Frcs.Add(lst_frcs);
            }

            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 1 : SUMMARY OF FORCES"));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("  Distance        Dead Load         SIDL w/o surfacing   SIDL with surfacing"));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("                BM          SF         BM          SF        BM          SF"));
            list.Add(string.Format("              (kN-m)       (kN)      (kN-m)       (kN)     (kN-m)       (kN)"));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2}",
                     dst[i]
                   , itm[0]
                   , itm[1]
                   , itm[2]
                   , itm[3]
                   , itm[4]
                   , itm[5]
                    ));
            }
            list.Add(string.Format("------------------------------------------------------------------------------"));

            #endregion Bottom Slab 1

            All_Frcs.Clear();


            for (int i = 0; i < dst.Count; i++)
            {
                lst_frcs = new List<double>();
                //for (int l = 1; l <= 3; l++)
                {
                    top_arr.Clear();
                    top_arr.Add(Slab_Model.jnBots[i].NodeNo);
                    var bm = brd1.GetJoint_MaxBendingMoment_Corrs_ShearForce(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd1.GetJoint_MinimumBendingMoment_Corrs_ShearForce(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);


                    bm = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd1.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);



                    bm = brd2.GetJoint_MaxBendingMoment_Corrs_ShearForce(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd2.GetJoint_MinimumBendingMoment_Corrs_ShearForce(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);


                    bm = brd2.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd2.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);


                    bm = brd3.GetJoint_MaxBendingMoment_Corrs_ShearForce(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd3.GetJoint_MinimumBendingMoment_Corrs_ShearForce(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);


                    bm = brd3.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd3.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd4.GetJoint_MaxBendingMoment_Corrs_ShearForce(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd4.GetJoint_MinimumBendingMoment_Corrs_ShearForce(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);


                    bm = brd4.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, false);
                    lst_frcs.Add(bm.MaxBendingMoment);

                    bm = brd4.GetJoint_MaxShearForce_Corrs_BendingMoment(top_arr, true, true);
                    lst_frcs.Add(bm.MaxBendingMoment);

                }

                All_Frcs.Add(lst_frcs);
            }
            list.Add("");
            list.Add("");

            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("TABLE 2 : SUMMARY OF FORCES DUE TO LIVE LOADS"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                list.Add(string.Format("  Distance                     70R Track                                    70R Wheel                                 70R Boggie                              CLASS A"));
            }
            else
            {
                list.Add(string.Format("  Distance                  LL ANALYSIS 1                             LL ANALYSIS 2                                LL ANALYSIS 3                                 LL ANALYSIS 4"));
            }
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                       BM                    SF                    BM                   SF                   BM                       SF                    BM                  SF"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                 MAX       MIN         MAX         MIN       MAX        MIN        MAX        MIN        MAX        MIN        MAX        MIN         MAX        MIN       MAX         MIN"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));

            for (int i = 0; i < All_Frcs.Count; i++)
            {
                var itm = All_Frcs[i];
                list.Add(string.Format("{0,10:f2} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2} {14,10:f2} {15,10:f2} {16,10:f2}",
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
                   , itm[13]
                   , itm[14]
                   , itm[15]
                    ));
            }
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));


            #endregion Bottom Slab




            rtb_results.Lines = list.ToArray();
            File.WriteAllLines(Result_File, list.ToArray());
            System.Diagnostics.Process.Start(Result_File);
        }

        string Result_File
        {
            get
            {
                return Path.Combine(iApp.user_path, "Slab_Bridge_Result_Summary.txt");
            }
        }
        public void Create_Data_Singlecell_LiveLoad()
        {
            List<string> list = new List<string>();
            //list.Add(string.Format("ASTRA SPACE SLAB ANALYSIS LIVE LOAD"));
            //list.Add(string.Format("UNIT METER KN"));
            //list.Add(string.Format("JOINT COORDINATES"));
            //list.Add(string.Format("1 0 0 0; 2 12 0 0;"));
            //list.Add(string.Format("MEMBER INCIDENCES"));
            //list.Add(string.Format("1 1 2;"));
            //list.Add(string.Format("MEMBER PROPERTY"));
            //list.Add(string.Format("1 PRIS YD 1 ZD 1"));
            //list.Add(string.Format("CONSTANTS"));
            //list.Add(string.Format("E 2.17185e+007 ALL"));
            //list.Add(string.Format("POISSON 0.17 ALL"));
            //list.Add(string.Format("DENSITY 23.5616 ALL"));
            //list.Add(string.Format("ALPHA 1e-005 ALL"));
            //list.Add(string.Format("DAMP 0.05 ALL"));
            //list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 FIXED BUT FX FZ MX MY MZ"));
            //list.Add(string.Format("2 FIXED BUT FZ MX MY MZ"));

            list.Add(string.Format("ASTRA SPACE SLAB ANALYSIS LIVE LOAD"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         0.0000     0.0000     0.0000"));
            list.Add(string.Format("2        12.0000     0.0000     0.0000"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("MEMBER PROPERTY"));
            list.Add(string.Format("1 PRISMATIC YD 1 ZD 1"));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E CONCRETE ALL"));
            list.Add(string.Format("DEN CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("1 FIXED BUT FX MZ MY"));
            list.Add(string.Format("2 PINNED"));
            list.Add(string.Format("LOAD 1 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -38.65 0 0.381"));
            list.Add(string.Format("LOAD 2 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -23.11 0 0.631"));
            list.Add(string.Format("LOAD 3 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.58 0.119 0.881"));
            list.Add(string.Format("LOAD 4 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.42 0.369 1.131"));
            list.Add(string.Format("LOAD 5 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.81 0.619 1.381"));
            list.Add(string.Format("1 UNI GY -47.14 0 0.619"));
            list.Add(string.Format("LOAD 6 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.56 0.869 1.631"));
            list.Add(string.Format("1 UNI GY -35.43 0.107 0.869"));
            list.Add(string.Format("LOAD 7 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 1.119 1.881"));
            list.Add(string.Format("1 UNI GY -31.05 0.357 1.119"));
            list.Add(string.Format("LOAD 8 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 1.369 2.131"));
            list.Add(string.Format("1 UNI GY -27.77 0.607 1.369"));
            list.Add(string.Format("1 UNI GY -48.07 0 0.607"));
            list.Add(string.Format("LOAD 9 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 1.619 2.381"));
            list.Add(string.Format("1 UNI GY -25.24 0.857 1.619"));
            list.Add(string.Format("1 UNI GY -35.68 0.095 0.857"));
            list.Add(string.Format("LOAD 10 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 1.869 2.631"));
            list.Add(string.Format("1 UNI GY -24.31 1.107 1.869"));
            list.Add(string.Format("1 UNI GY -31.23 0.345 1.107"));
            list.Add(string.Format("LOAD 11 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 2.119 2.881"));
            list.Add(string.Format("1 UNI GY -24.31 1.357 2.119"));
            list.Add(string.Format("1 UNI GY -27.91 0.595 1.357"));
            list.Add(string.Format("1 UNI GY -49.04 0 0.595"));
            list.Add(string.Format("LOAD 12 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 2.369 3.131"));
            list.Add(string.Format("1 UNI GY -24.31 1.607 2.369"));
            list.Add(string.Format("1 UNI GY -25.34 0.845 1.607"));
            list.Add(string.Format("1 UNI GY -35.93 0.083 0.845"));
            list.Add(string.Format("LOAD 13 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 2.619 3.381"));
            list.Add(string.Format("1 UNI GY -24.31 1.857 2.619"));
            list.Add(string.Format("1 UNI GY -24.31 1.095 1.857"));
            list.Add(string.Format("1 UNI GY -31.41 0.333 1.095"));
            list.Add(string.Format("LOAD 14 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 2.869 3.631"));
            list.Add(string.Format("1 UNI GY -24.31 2.107 2.869"));
            list.Add(string.Format("1 UNI GY -24.31 1.345 2.107"));
            list.Add(string.Format("1 UNI GY -28.05 0.583 1.345"));
            list.Add(string.Format("1 UNI GY -50.05 0 0.583"));
            list.Add(string.Format("LOAD 15 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 3.119 3.881"));
            list.Add(string.Format("1 UNI GY -24.31 2.357 3.119"));
            list.Add(string.Format("1 UNI GY -24.31 1.595 2.357"));
            list.Add(string.Format("1 UNI GY -25.45 0.833 1.595"));
            list.Add(string.Format("1 UNI GY -36.18 0.071 0.833"));
            list.Add(string.Format("LOAD 16 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 3.369 4.131"));
            list.Add(string.Format("1 UNI GY -24.31 2.607 3.369"));
            list.Add(string.Format("1 UNI GY -24.31 1.845 2.607"));
            list.Add(string.Format("1 UNI GY -24.31 1.083 1.845"));
            list.Add(string.Format("1 UNI GY -31.6 0.321 1.083"));
            list.Add(string.Format("LOAD 17 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 3.619 4.381"));
            list.Add(string.Format("1 UNI GY -24.31 2.857 3.619"));
            list.Add(string.Format("1 UNI GY -24.31 2.095 2.857"));
            list.Add(string.Format("1 UNI GY -24.31 1.333 2.095"));
            list.Add(string.Format("1 UNI GY -28.19 0.571 1.333"));
            list.Add(string.Format("1 UNI GY -51.1 0 0.571"));
            list.Add(string.Format("LOAD 18 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 3.869 4.631"));
            list.Add(string.Format("1 UNI GY -24.31 3.107 3.869"));
            list.Add(string.Format("1 UNI GY -24.31 2.345 3.107"));
            list.Add(string.Format("1 UNI GY -24.31 1.583 2.345"));
            list.Add(string.Format("1 UNI GY -25.56 0.821 1.583"));
            list.Add(string.Format("1 UNI GY -36.44 0.059 0.821"));
            list.Add(string.Format("LOAD 19 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 4.119 4.881"));
            list.Add(string.Format("1 UNI GY -24.31 3.357 4.119"));
            list.Add(string.Format("1 UNI GY -24.31 2.595 3.357"));
            list.Add(string.Format("1 UNI GY -24.31 1.833 2.595"));
            list.Add(string.Format("1 UNI GY -24.31 1.071 1.833"));
            list.Add(string.Format("1 UNI GY -31.79 0.309 1.071"));
            list.Add(string.Format("LOAD 20 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 4.369 5.131"));
            list.Add(string.Format("1 UNI GY -24.31 3.607 4.369"));
            list.Add(string.Format("1 UNI GY -24.31 2.845 3.607"));
            list.Add(string.Format("1 UNI GY -24.31 2.083 2.845"));
            list.Add(string.Format("1 UNI GY -24.31 1.321 2.083"));
            list.Add(string.Format("1 UNI GY -28.33 0.559 1.321"));
            list.Add(string.Format("1 UNI GY -26.08 0 0.559"));
            list.Add(string.Format("LOAD 21 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 4.619 5.381"));
            list.Add(string.Format("1 UNI GY -24.31 3.857 4.619"));
            list.Add(string.Format("1 UNI GY -24.31 3.095 3.857"));
            list.Add(string.Format("1 UNI GY -24.31 2.333 3.095"));
            list.Add(string.Format("1 UNI GY -24.31 1.571 2.333"));
            list.Add(string.Format("1 UNI GY -25.68 0.809 1.571"));
            list.Add(string.Format("1 UNI GY -18.34 0.047 0.809"));
            list.Add(string.Format("LOAD 22 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 4.869 5.631"));
            list.Add(string.Format("1 UNI GY -24.31 4.107 4.869"));
            list.Add(string.Format("1 UNI GY -24.31 3.345 4.107"));
            list.Add(string.Format("1 UNI GY -24.31 2.583 3.345"));
            list.Add(string.Format("1 UNI GY -24.31 1.821 2.583"));
            list.Add(string.Format("1 UNI GY -24.31 1.059 1.821"));
            list.Add(string.Format("1 UNI GY -15.98 0.297 1.059"));
            list.Add(string.Format("LOAD 23 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 5.119 5.881"));
            list.Add(string.Format("1 UNI GY -24.31 4.357 5.119"));
            list.Add(string.Format("1 UNI GY -24.31 3.595 4.357"));
            list.Add(string.Format("1 UNI GY -24.31 2.833 3.595"));
            list.Add(string.Format("1 UNI GY -24.31 2.071 2.833"));
            list.Add(string.Format("1 UNI GY -24.31 1.309 2.071"));
            list.Add(string.Format("1 UNI GY -14.23 0.547 1.309"));
            list.Add(string.Format("LOAD 24 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 5.369 6.131"));
            list.Add(string.Format("1 UNI GY -24.31 4.607 5.369"));
            list.Add(string.Format("1 UNI GY -24.31 3.845 4.607"));
            list.Add(string.Format("1 UNI GY -24.31 3.083 3.845"));
            list.Add(string.Format("1 UNI GY -24.31 2.321 3.083"));
            list.Add(string.Format("1 UNI GY -24.31 1.559 2.321"));
            list.Add(string.Format("1 UNI GY -12.89 0.797 1.559"));
            list.Add(string.Format("LOAD 25 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 5.619 6.381"));
            list.Add(string.Format("1 UNI GY -24.31 4.857 5.619"));
            list.Add(string.Format("1 UNI GY -24.31 4.095 4.857"));
            list.Add(string.Format("1 UNI GY -24.31 3.333 4.095"));
            list.Add(string.Format("1 UNI GY -24.31 2.571 3.333"));
            list.Add(string.Format("1 UNI GY -24.31 1.809 2.571"));
            list.Add(string.Format("1 UNI GY -12.15 1.047 1.809"));
            list.Add(string.Format("LOAD 26 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 5.869 6.631"));
            list.Add(string.Format("1 UNI GY -24.31 5.107 5.869"));
            list.Add(string.Format("1 UNI GY -24.31 4.345 5.107"));
            list.Add(string.Format("1 UNI GY -24.31 3.583 4.345"));
            list.Add(string.Format("1 UNI GY -24.31 2.821 3.583"));
            list.Add(string.Format("1 UNI GY -24.31 2.059 2.821"));
            list.Add(string.Format("1 UNI GY -12.15 1.297 2.059"));
            list.Add(string.Format("LOAD 27 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 6.119 6.881"));
            list.Add(string.Format("1 UNI GY -24.31 5.357 6.119"));
            list.Add(string.Format("1 UNI GY -24.31 4.595 5.357"));
            list.Add(string.Format("1 UNI GY -24.31 3.833 4.595"));
            list.Add(string.Format("1 UNI GY -24.31 3.071 3.833"));
            list.Add(string.Format("1 UNI GY -24.31 2.309 3.071"));
            list.Add(string.Format("1 UNI GY -12.15 1.547 2.309"));
            list.Add(string.Format("LOAD 28 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 6.369 7.131"));
            list.Add(string.Format("1 UNI GY -24.31 5.607 6.369"));
            list.Add(string.Format("1 UNI GY -24.31 4.845 5.607"));
            list.Add(string.Format("1 UNI GY -24.31 4.083 4.845"));
            list.Add(string.Format("1 UNI GY -24.31 3.321 4.083"));
            list.Add(string.Format("1 UNI GY -24.31 2.559 3.321"));
            list.Add(string.Format("1 UNI GY -12.15 1.797 2.559"));
            list.Add(string.Format("LOAD 29 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 6.619 7.381"));
            list.Add(string.Format("1 UNI GY -24.31 5.857 6.619"));
            list.Add(string.Format("1 UNI GY -24.31 5.095 5.857"));
            list.Add(string.Format("1 UNI GY -24.31 4.333 5.095"));
            list.Add(string.Format("1 UNI GY -24.31 3.571 4.333"));
            list.Add(string.Format("1 UNI GY -24.31 2.809 3.571"));
            list.Add(string.Format("1 UNI GY -12.15 2.047 2.809"));
            list.Add(string.Format("LOAD 30 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 6.869 7.631"));
            list.Add(string.Format("1 UNI GY -24.31 6.107 6.869"));
            list.Add(string.Format("1 UNI GY -24.31 5.345 6.107"));
            list.Add(string.Format("1 UNI GY -24.31 4.583 5.345"));
            list.Add(string.Format("1 UNI GY -24.31 3.821 4.583"));
            list.Add(string.Format("1 UNI GY -24.31 3.059 3.821"));
            list.Add(string.Format("1 UNI GY -12.15 2.297 3.059"));
            list.Add(string.Format("LOAD 31 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 7.119 7.881"));
            list.Add(string.Format("1 UNI GY -24.31 6.357 7.119"));
            list.Add(string.Format("1 UNI GY -24.31 5.595 6.357"));
            list.Add(string.Format("1 UNI GY -24.31 4.833 5.595"));
            list.Add(string.Format("1 UNI GY -24.31 4.071 4.833"));
            list.Add(string.Format("1 UNI GY -24.31 3.309 4.071"));
            list.Add(string.Format("1 UNI GY -12.15 2.547 3.309"));
            list.Add(string.Format("LOAD 32 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 7.369 8.131"));
            list.Add(string.Format("1 UNI GY -24.31 6.607 7.369"));
            list.Add(string.Format("1 UNI GY -24.31 5.845 6.607"));
            list.Add(string.Format("1 UNI GY -24.31 5.083 5.845"));
            list.Add(string.Format("1 UNI GY -24.31 4.321 5.083"));
            list.Add(string.Format("1 UNI GY -24.31 3.559 4.321"));
            list.Add(string.Format("1 UNI GY -12.15 2.797 3.559"));
            list.Add(string.Format("LOAD 33 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 7.619 8.381"));
            list.Add(string.Format("1 UNI GY -24.31 6.857 7.619"));
            list.Add(string.Format("1 UNI GY -24.31 6.095 6.857"));
            list.Add(string.Format("1 UNI GY -24.31 5.333 6.095"));
            list.Add(string.Format("1 UNI GY -24.31 4.571 5.333"));
            list.Add(string.Format("1 UNI GY -24.31 3.809 4.571"));
            list.Add(string.Format("1 UNI GY -12.15 3.047 3.809"));
            list.Add(string.Format("LOAD 34 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 7.869 8.631"));
            list.Add(string.Format("1 UNI GY -24.31 7.107 7.869"));
            list.Add(string.Format("1 UNI GY -24.31 6.345 7.107"));
            list.Add(string.Format("1 UNI GY -24.31 5.583 6.345"));
            list.Add(string.Format("1 UNI GY -24.31 4.821 5.583"));
            list.Add(string.Format("1 UNI GY -24.31 4.059 4.821"));
            list.Add(string.Format("1 UNI GY -12.15 3.297 4.059"));
            list.Add(string.Format("LOAD 35 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 8.119 8.881"));
            list.Add(string.Format("1 UNI GY -24.31 7.357 8.119"));
            list.Add(string.Format("1 UNI GY -24.31 6.595 7.357"));
            list.Add(string.Format("1 UNI GY -24.31 5.833 6.595"));
            list.Add(string.Format("1 UNI GY -24.31 5.071 5.833"));
            list.Add(string.Format("1 UNI GY -24.31 4.309 5.071"));
            list.Add(string.Format("1 UNI GY -12.15 3.547 4.309"));
            list.Add(string.Format("LOAD 36 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 8.369 9.131"));
            list.Add(string.Format("1 UNI GY -24.31 7.607 8.369"));
            list.Add(string.Format("1 UNI GY -24.31 6.845 7.607"));
            list.Add(string.Format("1 UNI GY -24.31 6.083 6.845"));
            list.Add(string.Format("1 UNI GY -24.31 5.321 6.083"));
            list.Add(string.Format("1 UNI GY -24.31 4.559 5.321"));
            list.Add(string.Format("1 UNI GY -12.15 3.797 4.559"));
            list.Add(string.Format("LOAD 37 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 8.619 9.381"));
            list.Add(string.Format("1 UNI GY -24.31 7.857 8.619"));
            list.Add(string.Format("1 UNI GY -24.31 7.095 7.857"));
            list.Add(string.Format("1 UNI GY -24.31 6.333 7.095"));
            list.Add(string.Format("1 UNI GY -24.31 5.571 6.333"));
            list.Add(string.Format("1 UNI GY -24.31 4.809 5.571"));
            list.Add(string.Format("1 UNI GY -12.15 4.047 4.809"));
            list.Add(string.Format("LOAD 38 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 8.869 9.631"));
            list.Add(string.Format("1 UNI GY -24.31 8.107 8.869"));
            list.Add(string.Format("1 UNI GY -24.31 7.345 8.107"));
            list.Add(string.Format("1 UNI GY -24.31 6.583 7.345"));
            list.Add(string.Format("1 UNI GY -24.31 5.821 6.583"));
            list.Add(string.Format("1 UNI GY -24.31 5.059 5.821"));
            list.Add(string.Format("1 UNI GY -12.15 4.297 5.059"));
            list.Add(string.Format("LOAD 39 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 9.119 9.881"));
            list.Add(string.Format("1 UNI GY -24.31 8.357 9.119"));
            list.Add(string.Format("1 UNI GY -24.31 7.595 8.357"));
            list.Add(string.Format("1 UNI GY -24.31 6.833 7.595"));
            list.Add(string.Format("1 UNI GY -24.31 6.071 6.833"));
            list.Add(string.Format("1 UNI GY -24.31 5.309 6.071"));
            list.Add(string.Format("1 UNI GY -12.15 4.547 5.309"));
            list.Add(string.Format("LOAD 40 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 9.369 10.131"));
            list.Add(string.Format("1 UNI GY -24.31 8.607 9.369"));
            list.Add(string.Format("1 UNI GY -24.31 7.845 8.607"));
            list.Add(string.Format("1 UNI GY -24.31 7.083 7.845"));
            list.Add(string.Format("1 UNI GY -24.31 6.321 7.083"));
            list.Add(string.Format("1 UNI GY -24.31 5.559 6.321"));
            list.Add(string.Format("1 UNI GY -12.15 4.797 5.559"));
            list.Add(string.Format("LOAD 41 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 9.619 10.381"));
            list.Add(string.Format("1 UNI GY -24.31 8.857 9.619"));
            list.Add(string.Format("1 UNI GY -24.31 8.095 8.857"));
            list.Add(string.Format("1 UNI GY -24.31 7.333 8.095"));
            list.Add(string.Format("1 UNI GY -24.31 6.571 7.333"));
            list.Add(string.Format("1 UNI GY -24.31 5.809 6.571"));
            list.Add(string.Format("1 UNI GY -12.15 5.047 5.809"));
            list.Add(string.Format("LOAD 42 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 9.869 10.631"));
            list.Add(string.Format("1 UNI GY -24.31 9.107 9.869"));
            list.Add(string.Format("1 UNI GY -24.31 8.345 9.107"));
            list.Add(string.Format("1 UNI GY -24.31 7.583 8.345"));
            list.Add(string.Format("1 UNI GY -24.31 6.821 7.583"));
            list.Add(string.Format("1 UNI GY -24.31 6.059 6.821"));
            list.Add(string.Format("1 UNI GY -12.15 5.297 6.059"));
            list.Add(string.Format("LOAD 43 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.15 10.119 10.881"));
            list.Add(string.Format("1 UNI GY -24.31 9.357 10.119"));
            list.Add(string.Format("1 UNI GY -24.31 8.595 9.357"));
            list.Add(string.Format("1 UNI GY -24.31 7.833 8.595"));
            list.Add(string.Format("1 UNI GY -24.31 7.071 7.833"));
            list.Add(string.Format("1 UNI GY -24.31 6.309 7.071"));
            list.Add(string.Format("1 UNI GY -12.15 5.547 6.309"));
            list.Add(string.Format("LOAD 44 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.56 10.369 11.131"));
            list.Add(string.Format("1 UNI GY -24.31 9.607 10.369"));
            list.Add(string.Format("1 UNI GY -24.31 8.845 9.607"));
            list.Add(string.Format("1 UNI GY -24.31 8.083 8.845"));
            list.Add(string.Format("1 UNI GY -24.31 7.321 8.083"));
            list.Add(string.Format("1 UNI GY -24.31 6.559 7.321"));
            list.Add(string.Format("1 UNI GY -12.15 5.797 6.559"));
            list.Add(string.Format("LOAD 45 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.81 10.619 11.381"));
            list.Add(string.Format("1 UNI GY -24.31 9.857 10.619"));
            list.Add(string.Format("1 UNI GY -24.31 9.095 9.857"));
            list.Add(string.Format("1 UNI GY -24.31 8.333 9.095"));
            list.Add(string.Format("1 UNI GY -24.31 7.571 8.333"));
            list.Add(string.Format("1 UNI GY -24.31 6.809 7.571"));
            list.Add(string.Format("1 UNI GY -12.15 6.047 6.809"));
            list.Add(string.Format("LOAD 46 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.42 10.869 11.631"));
            list.Add(string.Format("1 UNI GY -24.31 10.107 10.869"));
            list.Add(string.Format("1 UNI GY -24.31 9.345 10.107"));
            list.Add(string.Format("1 UNI GY -24.31 8.583 9.345"));
            list.Add(string.Format("1 UNI GY -24.31 7.821 8.583"));
            list.Add(string.Format("1 UNI GY -24.31 7.059 7.821"));
            list.Add(string.Format("1 UNI GY -12.15 6.297 7.059"));
            list.Add(string.Format("LOAD 47 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.58 11.119 11.881"));
            list.Add(string.Format("1 UNI GY -25.02 10.357 11.119"));
            list.Add(string.Format("1 UNI GY -24.31 9.595 10.357"));
            list.Add(string.Format("1 UNI GY -24.31 8.833 9.595"));
            list.Add(string.Format("1 UNI GY -24.31 8.071 8.833"));
            list.Add(string.Format("1 UNI GY -24.31 7.309 8.071"));
            list.Add(string.Format("1 UNI GY -12.15 6.547 7.309"));
            list.Add(string.Format("LOAD 48 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -23.11 11.369 12.131"));
            list.Add(string.Format("1 UNI GY -27.5 10.607 11.369"));
            list.Add(string.Format("1 UNI GY -24.31 9.845 10.607"));
            list.Add(string.Format("1 UNI GY -24.31 9.083 9.845"));
            list.Add(string.Format("1 UNI GY -24.31 8.321 9.083"));
            list.Add(string.Format("1 UNI GY -24.31 7.559 8.321"));
            list.Add(string.Format("1 UNI GY -12.15 6.797 7.559"));
            list.Add(string.Format("LOAD 49 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -38.65 11.619 12.381"));
            list.Add(string.Format("1 UNI GY -30.69 10.857 11.619"));
            list.Add(string.Format("1 UNI GY -24.31 10.095 10.857"));
            list.Add(string.Format("1 UNI GY -24.31 9.333 10.095"));
            list.Add(string.Format("1 UNI GY -24.31 8.571 9.333"));
            list.Add(string.Format("1 UNI GY -24.31 7.809 8.571"));
            list.Add(string.Format("1 UNI GY -12.15 7.047 7.809"));
            list.Add(string.Format("LOAD 50 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -34.94 11.107 11.869"));
            list.Add(string.Format("1 UNI GY -24.92 10.345 11.107"));
            list.Add(string.Format("1 UNI GY -24.31 9.583 10.345"));
            list.Add(string.Format("1 UNI GY -24.31 8.821 9.583"));
            list.Add(string.Format("1 UNI GY -24.31 8.059 8.821"));
            list.Add(string.Format("1 UNI GY -12.15 7.297 8.059"));
            list.Add(string.Format("LOAD 51 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -45.39 11.357 12.119"));
            list.Add(string.Format("1 UNI GY -27.36 10.595 11.357"));
            list.Add(string.Format("1 UNI GY -24.31 9.833 10.595"));
            list.Add(string.Format("1 UNI GY -24.31 9.071 9.833"));
            list.Add(string.Format("1 UNI GY -24.31 8.309 9.071"));
            list.Add(string.Format("1 UNI GY -12.15 7.547 8.309"));
            list.Add(string.Format("LOAD 52 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -74.89 11.607 12.369"));
            list.Add(string.Format("1 UNI GY -30.52 10.845 11.607"));
            list.Add(string.Format("1 UNI GY -24.31 10.083 10.845"));
            list.Add(string.Format("1 UNI GY -24.31 9.321 10.083"));
            list.Add(string.Format("1 UNI GY -24.31 8.559 9.321"));
            list.Add(string.Format("1 UNI GY -12.15 7.797 8.559"));
            list.Add(string.Format("LOAD 53 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -34.71 11.095 11.857"));
            list.Add(string.Format("1 UNI GY -24.81 10.333 11.095"));
            list.Add(string.Format("1 UNI GY -24.31 9.571 10.333"));
            list.Add(string.Format("1 UNI GY -24.31 8.809 9.571"));
            list.Add(string.Format("1 UNI GY -12.15 8.047 8.809"));
            list.Add(string.Format("LOAD 54 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -44.57 11.345 12.107"));
            list.Add(string.Format("1 UNI GY -27.23 10.583 11.345"));
            list.Add(string.Format("1 UNI GY -24.31 9.821 10.583"));
            list.Add(string.Format("1 UNI GY -24.31 9.059 9.821"));
            list.Add(string.Format("1 UNI GY -12.15 8.297 9.059"));
            list.Add(string.Format("LOAD 55 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -72.58 11.595 12.357"));
            list.Add(string.Format("1 UNI GY -30.34 10.833 11.595"));
            list.Add(string.Format("1 UNI GY -24.31 10.071 10.833"));
            list.Add(string.Format("1 UNI GY -24.31 9.309 10.071"));
            list.Add(string.Format("1 UNI GY -12.15 8.547 9.309"));
            list.Add(string.Format("LOAD 56 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -34.48 11.083 11.845"));
            list.Add(string.Format("1 UNI GY -24.71 10.321 11.083"));
            list.Add(string.Format("1 UNI GY -24.31 9.559 10.321"));
            list.Add(string.Format("1 UNI GY -12.15 8.797 9.559"));
            list.Add(string.Format("LOAD 57 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -43.78 11.333 12.095"));
            list.Add(string.Format("1 UNI GY -27.1 10.571 11.333"));
            list.Add(string.Format("1 UNI GY -24.31 9.809 10.571"));
            list.Add(string.Format("1 UNI GY -12.15 9.047 9.809"));
            list.Add(string.Format("LOAD 58 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -70.41 11.583 12.345"));
            list.Add(string.Format("1 UNI GY -30.18 10.821 11.583"));
            list.Add(string.Format("1 UNI GY -24.31 10.059 10.821"));
            list.Add(string.Format("1 UNI GY -12.15 9.297 10.059"));
            list.Add(string.Format("LOAD 59 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -34.25 11.071 11.833"));
            list.Add(string.Format("1 UNI GY -24.61 10.309 11.071"));
            list.Add(string.Format("1 UNI GY -12.15 9.547 10.309"));
            list.Add(string.Format("LOAD 60 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -43.01 11.321 12.083"));
            list.Add(string.Format("1 UNI GY -26.97 10.559 11.321"));
            list.Add(string.Format("1 UNI GY -12.15 9.797 10.559"));
            list.Add(string.Format("LOAD 62 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -68.37 11.571 12.333"));
            list.Add(string.Format("1 UNI GY -30.01 10.809 11.571"));
            list.Add(string.Format("1 UNI GY -12.15 10.047 10.809"));
            list.Add(string.Format("LOAD 63 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -34.02 11.059 11.821"));
            list.Add(string.Format("1 UNI GY -12.25 10.297 11.059"));
            list.Add(string.Format("LOAD 64 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -42.28 11.309 12"));
            list.Add(string.Format("1 UNI GY -13.42 10.547 11.309"));
            list.Add(string.Format("LOAD 65 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -66.45 11.559 12"));
            list.Add(string.Format("1 UNI GY -14.91 10.797 11.559"));
            list.Add(string.Format("LOAD 66 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.89 11.047 11.809"));
            list.Add(string.Format("LOAD 67 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.77 11.297 12"));
            list.Add(string.Format("LOAD 68 LOADTYPE None  TITLE 70R TRACK"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -32.3 11.547 12"));
            list.Add(string.Format("LOAD 101 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -44.65 0 0.9425"));
            list.Add(string.Format("LOAD 102 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.91 0 1.1925"));
            list.Add(string.Format("LOAD 103 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.26 0 1.4425"));
            list.Add(string.Format("LOAD 104 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -18.59 0 1.6925"));
            list.Add(string.Format("LOAD 105 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.6 0.0575 1.9425"));
            list.Add(string.Format("LOAD 106 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.07 0.3075 2.1925"));
            list.Add(string.Format("LOAD 107 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 0.5575 2.4425"));
            list.Add(string.Format("1 UNI GY -40.53 -0.64125 0.90125"));
            list.Add(string.Format("LOAD 108 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 0.8075 2.6925"));
            list.Add(string.Format("1 UNI GY -27.97 0 1.15125"));
            list.Add(string.Format("LOAD 109 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 1.0575 2.9425"));
            list.Add(string.Format("1 UNI GY -24.15 0 1.40125"));
            list.Add(string.Format("LOAD 110 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 1.3075 3.1925"));
            list.Add(string.Format("1 UNI GY -21.37 0.11 1.65125"));
            list.Add(string.Format("LOAD 111 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 1.5575 3.4425"));
            list.Add(string.Format("1 UNI GY -19.26 0.35875 1.90125"));
            list.Add(string.Format("LOAD 112 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 1.8075 3.6925"));
            list.Add(string.Format("1 UNI GY -17.81 0.60875 2.15125"));
            list.Add(string.Format("LOAD 113 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 2.0575 3.9425"));
            list.Add(string.Format("1 UNI GY -17.81 0.85875 2.40125"));
            list.Add(string.Format("LOAD 114 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 2.3075 4.1925"));
            list.Add(string.Format("1 UNI GY -17.81 1.10875 2.65125"));
            list.Add(string.Format("LOAD 115 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 2.5575 4.4425"));
            list.Add(string.Format("1 UNI GY -17.81 1.35875 2.90125"));
            list.Add(string.Format("LOAD 116 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 2.8075 4.6925"));
            list.Add(string.Format("1 UNI GY -17.81 1.60875 3.15125"));
            list.Add(string.Format("LOAD 117 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 3.0575 4.9425"));
            list.Add(string.Format("1 UNI GY -17.81 1.85875 3.40125"));
            list.Add(string.Format("LOAD 118 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 3.3075 5.1925"));
            list.Add(string.Format("1 UNI GY -17.81 2.10875 3.65125"));
            list.Add(string.Format("LOAD 119 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 3.5575 5.4425"));
            list.Add(string.Format("1 UNI GY -17.81 2.35875 3.90125"));
            list.Add(string.Format("1 UNI GY -36.78 0 1.0225"));
            list.Add(string.Format("LOAD 120 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 3.8075 5.6925"));
            list.Add(string.Format("1 UNI GY -17.81 2.60875 4.15125"));
            list.Add(string.Format("1 UNI GY -23.95 0 1.2725"));
            list.Add(string.Format("LOAD 121 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 4.0575 5.9425"));
            list.Add(string.Format("1 UNI GY -17.81 2.85875 4.40125"));
            list.Add(string.Format("1 UNI GY -20.31 0 1.5225"));
            list.Add(string.Format("LOAD 122 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 4.3075 6.1925"));
            list.Add(string.Format("1 UNI GY -17.81 3.10875 4.65125"));
            list.Add(string.Format("1 UNI GY -17.89 0 1.7725"));
            list.Add(string.Format("LOAD 123 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 4.5575 6.4425"));
            list.Add(string.Format("1 UNI GY -17.81 3.35875 4.90125"));
            list.Add(string.Format("1 UNI GY -16.07 0.1375 2.0225"));
            list.Add(string.Format("LOAD 124 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 4.8075 6.6925"));
            list.Add(string.Format("1 UNI GY -17.81 3.60875 5.15125"));
            list.Add(string.Format("1 UNI GY -14.65 0.3875 2.2725"));
            list.Add(string.Format("LOAD 125 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 5.0575 6.9425"));
            list.Add(string.Format("1 UNI GY -17.81 3.85875 5.40125"));
            list.Add(string.Format("1 UNI GY -14.57 0.6375 2.5225"));
            list.Add(string.Format("1 UNI GY -38.44 0 0.91375"));
            list.Add(string.Format("LOAD 126 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 5.3075 7.1925"));
            list.Add(string.Format("1 UNI GY -17.81 4.10875 5.65125"));
            list.Add(string.Format("1 UNI GY -14.57 0.8875 2.7725"));
            list.Add(string.Format("1 UNI GY -29.16 0 1.16375"));
            list.Add(string.Format("LOAD 127 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 5.5575 7.4425"));
            list.Add(string.Format("1 UNI GY -17.81 4.35875 5.90125"));
            list.Add(string.Format("1 UNI GY -14.57 1.1375 3.0225"));
            list.Add(string.Format("1 UNI GY -25.4 0.01 1.41375"));
            list.Add(string.Format("LOAD 128 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 5.8075 7.6925"));
            list.Add(string.Format("1 UNI GY -17.81 4.60875 6.15125"));
            list.Add(string.Format("1 UNI GY -14.57 1.3875 3.2725"));
            list.Add(string.Format("1 UNI GY -22.61 0.25625 1.66375"));
            list.Add(string.Format("LOAD 129 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 6.0575 7.9425"));
            list.Add(string.Format("1 UNI GY -17.81 4.85875 6.40125"));
            list.Add(string.Format("1 UNI GY -14.57 1.6375 3.5225"));
            list.Add(string.Format("1 UNI GY -20.48 0.50625 1.91375"));
            list.Add(string.Format("LOAD 130 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 6.3075 8.1925"));
            list.Add(string.Format("1 UNI GY -17.81 5.10875 6.65125"));
            list.Add(string.Format("1 UNI GY -14.57 1.8875 3.7725"));
            list.Add(string.Format("1 UNI GY -19.52 0.75625 2.16375"));
            list.Add(string.Format("LOAD 131 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 6.5575 8.4425"));
            list.Add(string.Format("1 UNI GY -17.81 5.35875 6.90125"));
            list.Add(string.Format("1 UNI GY -14.57 2.1375 4.0225"));
            list.Add(string.Format("1 UNI GY -19.52 1.00625 2.41375"));
            list.Add(string.Format("LOAD 132 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 6.8075 8.6925"));
            list.Add(string.Format("1 UNI GY -17.81 5.60875 7.15125"));
            list.Add(string.Format("1 UNI GY -14.57 2.3875 4.2725"));
            list.Add(string.Format("1 UNI GY -19.52 1.25625 2.66375"));
            list.Add(string.Format("LOAD 133 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 7.0575 8.9425"));
            list.Add(string.Format("1 UNI GY -17.81 5.85875 7.40125"));
            list.Add(string.Format("1 UNI GY -14.57 2.6375 4.5225"));
            list.Add(string.Format("1 UNI GY -19.52 1.50625 2.91375"));
            list.Add(string.Format("1 UNI GY -26.82 0 0.9925"));
            list.Add(string.Format("LOAD 134 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 7.3075 9.1925"));
            list.Add(string.Format("1 UNI GY -17.81 6.10875 7.65125"));
            list.Add(string.Format("1 UNI GY -14.57 2.8875 4.7725"));
            list.Add(string.Format("1 UNI GY -19.52 1.75625 3.16375"));
            list.Add(string.Format("1 UNI GY -17.47 0 1.2425"));
            list.Add(string.Format("LOAD 135 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 7.5575 9.4425"));
            list.Add(string.Format("1 UNI GY -17.81 6.35875 7.90125"));
            list.Add(string.Format("1 UNI GY -14.57 3.1375 5.0225"));
            list.Add(string.Format("1 UNI GY -19.52 2.00625 3.41375"));
            list.Add(string.Format("1 UNI GY -14.81 0 1.4925"));
            list.Add(string.Format("LOAD 136 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 7.8075 9.6925"));
            list.Add(string.Format("1 UNI GY -17.81 6.60875 8.15125"));
            list.Add(string.Format("1 UNI GY -14.57 3.3875 5.2725"));
            list.Add(string.Format("1 UNI GY -19.52 2.25625 3.66375"));
            list.Add(string.Format("1 UNI GY -13.05 0 1.7425"));
            list.Add(string.Format("LOAD 137 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.0575 9.9425"));
            list.Add(string.Format("1 UNI GY -17.81 6.85875 8.40125"));
            list.Add(string.Format("1 UNI GY -14.57 3.6375 5.5225"));
            list.Add(string.Format("1 UNI GY -19.52 2.50625 3.91375"));
            list.Add(string.Format("1 UNI GY -11.72 0.1675 1.9925"));
            list.Add(string.Format("LOAD 138 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.3075 10.1925"));
            list.Add(string.Format("1 UNI GY -17.81 7.10875 8.65125"));
            list.Add(string.Format("1 UNI GY -14.57 3.8875 5.7725"));
            list.Add(string.Format("1 UNI GY -19.52 2.75625 4.16375"));
            list.Add(string.Format("1 UNI GY -10.69 0.4175 2.2425"));
            list.Add(string.Format("LOAD 139 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.5575 10.4425"));
            list.Add(string.Format("1 UNI GY -17.81 7.35875 8.90125"));
            list.Add(string.Format("1 UNI GY -14.57 4.1375 6.0225"));
            list.Add(string.Format("1 UNI GY -19.52 3.00625 4.41375"));
            list.Add(string.Format("1 UNI GY -10.63 0.6675 2.4925"));
            list.Add(string.Format("1 UNI GY -26.12 0 1.04"));
            list.Add(string.Format("LOAD 140 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.8075 10.6925"));
            list.Add(string.Format("1 UNI GY -17.81 7.60875 9.15125"));
            list.Add(string.Format("1 UNI GY -14.57 4.3875 6.2725"));
            list.Add(string.Format("1 UNI GY -19.52 3.25625 4.66375"));
            list.Add(string.Format("1 UNI GY -10.63 0.9175 2.7425"));
            list.Add(string.Format("1 UNI GY -16.72 0 1.29"));
            list.Add(string.Format("LOAD 141 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.0575 10.9425"));
            list.Add(string.Format("1 UNI GY -17.81 7.85875 9.40125"));
            list.Add(string.Format("1 UNI GY -14.57 4.6375 6.5225"));
            list.Add(string.Format("1 UNI GY -19.52 3.50625 4.91375"));
            list.Add(string.Format("1 UNI GY -10.63 1.1675 2.9925"));
            list.Add(string.Format("1 UNI GY -13.94 -0.42 1.54"));
            list.Add(string.Format("LOAD 142 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.3075 11.1925"));
            list.Add(string.Format("1 UNI GY -17.81 8.10875 9.65125"));
            list.Add(string.Format("1 UNI GY -14.57 4.8875 6.7725"));
            list.Add(string.Format("1 UNI GY -19.52 3.75625 5.16375"));
            list.Add(string.Format("1 UNI GY -10.63 1.4175 3.2425"));
            list.Add(string.Format("1 UNI GY -12.26 -0.17 1.79"));
            list.Add(string.Format("LOAD 143 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.5575 11.4425"));
            list.Add(string.Format("1 UNI GY -17.81 8.35875 9.90125"));
            list.Add(string.Format("1 UNI GY -14.57 5.1375 7.0225"));
            list.Add(string.Format("1 UNI GY -19.52 4.00625 5.41375"));
            list.Add(string.Format("1 UNI GY -10.63 1.6675 3.4925"));
            list.Add(string.Format("1 UNI GY -11 0.09 2.04"));
            list.Add(string.Format("LOAD 144 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.07 9.8075 11.6925"));
            list.Add(string.Format("1 UNI GY -17.81 8.60875 10.1512"));
            list.Add(string.Format("1 UNI GY -14.57 5.3875 7.2725"));
            list.Add(string.Format("1 UNI GY -19.52 4.25625 5.66375"));
            list.Add(string.Format("1 UNI GY -10.63 1.9175 3.7425"));
            list.Add(string.Format("1 UNI GY -10.02 0.34 2.29"));
            list.Add(string.Format("LOAD 145 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.6 10.0575 11.9425"));
            list.Add(string.Format("1 UNI GY -17.81 8.85875 10.4012"));
            list.Add(string.Format("1 UNI GY -14.57 5.6375 7.5225"));
            list.Add(string.Format("1 UNI GY -19.52 4.50625 5.91375"));
            list.Add(string.Format("1 UNI GY -10.63 2.1675 3.9925"));
            list.Add(string.Format("1 UNI GY -9.9 0.59 2.54"));
            list.Add(string.Format("LOAD 146 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -18.59 10.3075 12.1925"));
            list.Add(string.Format("1 UNI GY -17.81 9.10875 10.6512"));
            list.Add(string.Format("1 UNI GY -14.57 5.8875 7.7725"));
            list.Add(string.Format("1 UNI GY -19.52 4.75625 6.16375"));
            list.Add(string.Format("1 UNI GY -10.63 2.4175 4.2425"));
            list.Add(string.Format("1 UNI GY -9.9 0.84 2.79"));
            list.Add(string.Format("LOAD 147 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.26 10.5575 12.4425"));
            list.Add(string.Format("1 UNI GY -17.81 9.35875 10.9012"));
            list.Add(string.Format("1 UNI GY -14.57 6.1375 8.0225"));
            list.Add(string.Format("1 UNI GY -19.52 5.00625 6.41375"));
            list.Add(string.Format("1 UNI GY -10.63 2.6675 4.4925"));
            list.Add(string.Format("1 UNI GY -9.9 1.08 3.04"));
            list.Add(string.Format("LOAD 148 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.91 10.8075 12.6925"));
            list.Add(string.Format("1 UNI GY -17.81 9.60875 11.1512"));
            list.Add(string.Format("1 UNI GY -14.57 6.3875 8.2725"));
            list.Add(string.Format("1 UNI GY -19.52 5.25625 6.66375"));
            list.Add(string.Format("1 UNI GY -10.63 2.9175 4.7425"));
            list.Add(string.Format("1 UNI GY -9.9 1.33 3.29"));
            list.Add(string.Format("LOAD 149 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -44.65 11.0575 12.9425"));
            list.Add(string.Format("1 UNI GY -17.81 9.85875 11.4012"));
            list.Add(string.Format("1 UNI GY -14.57 6.6375 8.5225"));
            list.Add(string.Format("1 UNI GY -19.52 5.50625 6.91375"));
            list.Add(string.Format("1 UNI GY -10.63 3.1675 4.9925"));
            list.Add(string.Format("1 UNI GY -9.9 1.58 3.54"));
            list.Add(string.Format("LOAD 150 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -19.33 10.1088 11.6512"));
            list.Add(string.Format("1 UNI GY -14.57 6.8875 8.7725"));
            list.Add(string.Format("1 UNI GY -19.52 5.75625 7.16375"));
            list.Add(string.Format("1 UNI GY -10.63 3.4175 5.2425"));
            list.Add(string.Format("1 UNI GY -9.9 1.83 3.79"));
            list.Add(string.Format("LOAD 151 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.47 10.3588 11.9012"));
            list.Add(string.Format("1 UNI GY -14.57 7.1375 9.0225"));
            list.Add(string.Format("1 UNI GY -19.52 6.00625 7.41375"));
            list.Add(string.Format("1 UNI GY -10.63 3.6675 5.4925"));
            list.Add(string.Format("1 UNI GY -9.9 2.08 4.04"));
            list.Add(string.Format("LOAD 152 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -24.28 10.6088 12"));
            list.Add(string.Format("1 UNI GY -14.57 7.3875 9.2725"));
            list.Add(string.Format("1 UNI GY -19.52 6.25625 7.66375"));
            list.Add(string.Format("1 UNI GY -10.63 3.9175 5.7425"));
            list.Add(string.Format("1 UNI GY -9.9 2.33 4.29"));
            list.Add(string.Format("LOAD 153 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -28.15 10.8588 12"));
            list.Add(string.Format("1 UNI GY -14.57 7.6375 9.5225"));
            list.Add(string.Format("1 UNI GY -19.52 6.50625 7.91375"));
            list.Add(string.Format("1 UNI GY -10.63 4.1675 5.9925"));
            list.Add(string.Format("1 UNI GY -9.9 2.58 4.54"));
            list.Add(string.Format("LOAD 154 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -41.34 11.1088 12"));
            list.Add(string.Format("1 UNI GY -14.57 7.8875 9.7725"));
            list.Add(string.Format("1 UNI GY -19.52 6.75625 8.16375"));
            list.Add(string.Format("1 UNI GY -10.63 4.4175 6.2425"));
            list.Add(string.Format("1 UNI GY -9.9 2.83 4.79"));
            list.Add(string.Format("LOAD 155 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.1375 10.0225"));
            list.Add(string.Format("1 UNI GY -19.52 7.00625 8.41375"));
            list.Add(string.Format("1 UNI GY -10.63 4.6675 6.4925"));
            list.Add(string.Format("1 UNI GY -9.9 3.08 5.04"));
            list.Add(string.Format("1 UNI GY -13.03 0 1.3"));
            list.Add(string.Format("LOAD 156 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.3875 10.2725"));
            list.Add(string.Format("1 UNI GY -19.52 7.25625 8.66375"));
            list.Add(string.Format("1 UNI GY -10.63 4.9175 6.7425"));
            list.Add(string.Format("1 UNI GY -9.9 3.33 5.29"));
            list.Add(string.Format("1 UNI GY -8.63 0 1.55"));
            list.Add(string.Format("LOAD 157 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.6375 10.5225"));
            list.Add(string.Format("1 UNI GY -19.52 7.50625 8.91375"));
            list.Add(string.Format("1 UNI GY -10.63 5.1675 6.9925"));
            list.Add(string.Format("1 UNI GY -9.9 3.58 5.54"));
            list.Add(string.Format("1 UNI GY -7.43 0 1.8"));
            list.Add(string.Format("LOAD 158 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 8.8875 10.7725"));
            list.Add(string.Format("1 UNI GY -19.52 7.75625 9.16375"));
            list.Add(string.Format("1 UNI GY -10.63 5.4175 7.2425"));
            list.Add(string.Format("1 UNI GY -9.9 3.83 5.79"));
            list.Add(string.Format("1 UNI GY -6.56 0 2.05"));
            list.Add(string.Format("LOAD 159 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.1375 11.0225"));
            list.Add(string.Format("1 UNI GY -19.52 8.00625 9.41375"));
            list.Add(string.Format("1 UNI GY -10.63 5.6675 7.4925"));
            list.Add(string.Format("1 UNI GY -9.9 4.08 6.04"));
            list.Add(string.Format("1 UNI GY -5.9 -0.1 2.3"));
            list.Add(string.Format("LOAD 160 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.3875 11.2725"));
            list.Add(string.Format("1 UNI GY -19.52 8.25625 9.66375"));
            list.Add(string.Format("1 UNI GY -10.63 5.9175 7.7425"));
            list.Add(string.Format("1 UNI GY -9.9 4.33 6.29"));
            list.Add(string.Format("1 UNI GY -5.39 0.15 2.55"));
            list.Add(string.Format("LOAD 161 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.57 9.6375 11.5225"));
            list.Add(string.Format("1 UNI GY -19.52 8.50625 9.91375"));
            list.Add(string.Format("1 UNI GY -10.63 6.1675 7.9925"));
            list.Add(string.Format("1 UNI GY -9.9 4.58 6.54"));
            list.Add(string.Format("1 UNI GY -5.39 0.4 2.8"));
            list.Add(string.Format("LOAD 162 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -15.52 9.8875 11.7725"));
            list.Add(string.Format("1 UNI GY -19.52 8.75625 10.1637"));
            list.Add(string.Format("1 UNI GY -10.63 6.4175 8.2425"));
            list.Add(string.Format("1 UNI GY -9.9 4.83 6.79"));
            list.Add(string.Format("1 UNI GY -5.39 0.65 3.05"));
            list.Add(string.Format("LOAD 163 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.18 10.1375 12"));
            list.Add(string.Format("1 UNI GY -19.52 9.00625 10.4137"));
            list.Add(string.Format("1 UNI GY -10.63 6.6675 8.4925"));
            list.Add(string.Format("1 UNI GY -9.9 5.08 7.04"));
            list.Add(string.Format("1 UNI GY -5.39 0.9 3.3"));
            list.Add(string.Format("LOAD 164 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -19.36 10.3875 12"));
            list.Add(string.Format("1 UNI GY -19.52 9.25625 10.6637"));
            list.Add(string.Format("1 UNI GY -10.63 6.9175 8.7425"));
            list.Add(string.Format("1 UNI GY -9.9 5.33 7.29"));
            list.Add(string.Format("1 UNI GY -5.39 1.15 3.55"));
            list.Add(string.Format("LOAD 165 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -22.31 10.6375 12"));
            list.Add(string.Format("1 UNI GY -19.52 9.50625 10.9137"));
            list.Add(string.Format("1 UNI GY -10.63 7.1675 8.9925"));
            list.Add(string.Format("1 UNI GY -9.9 5.58 7.54"));
            list.Add(string.Format("1 UNI GY -5.39 1.4 3.8"));
            list.Add(string.Format("LOAD 166 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -30.77 10.8875 12"));
            list.Add(string.Format("1 UNI GY -19.52 9.75625 11.1637"));
            list.Add(string.Format("1 UNI GY -10.63 7.4175 9.2425"));
            list.Add(string.Format("1 UNI GY -9.9 5.83 7.79"));
            list.Add(string.Format("1 UNI GY -5.39 1.65 4.05"));
            list.Add(string.Format("LOAD 167 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -19.9 10.0063 11.4137"));
            list.Add(string.Format("1 UNI GY -10.63 7.6675 9.4925"));
            list.Add(string.Format("1 UNI GY -9.9 6.08 8.04"));
            list.Add(string.Format("1 UNI GY -5.39 1.9 4.3"));
            list.Add(string.Format("LOAD 168 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.87 10.2563 11.6637"));
            list.Add(string.Format("1 UNI GY -10.63 7.9175 9.7425"));
            list.Add(string.Format("1 UNI GY -9.9 6.33 8.29"));
            list.Add(string.Format("1 UNI GY -5.39 2.15 4.55"));
            list.Add(string.Format("LOAD 169 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -24.42 10.5063 11.9137"));
            list.Add(string.Format("1 UNI GY -10.63 8.1675 9.9925"));
            list.Add(string.Format("1 UNI GY -9.9 6.58 8.54"));
            list.Add(string.Format("1 UNI GY -5.39 2.4 4.8"));
            list.Add(string.Format("LOAD 170 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -27.82 10.7563 12"));
            list.Add(string.Format("1 UNI GY -10.63 8.4175 10.2425"));
            list.Add(string.Format("1 UNI GY -9.9 6.83 8.79"));
            list.Add(string.Format("1 UNI GY -5.39 2.65 5.05"));
            list.Add(string.Format("LOAD 171 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -33.94 11.0063 12"));
            list.Add(string.Format("1 UNI GY -10.63 8.6675 10.4925"));
            list.Add(string.Format("1 UNI GY -9.9 7.08 9.04"));
            list.Add(string.Format("1 UNI GY -5.39 2.9 5.3"));
            list.Add(string.Format("LOAD 172 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -54 11.2563 12"));
            list.Add(string.Format("1 UNI GY -10.63 8.9175 10.7425"));
            list.Add(string.Format("1 UNI GY -9.9 7.33 9.29"));
            list.Add(string.Format("1 UNI GY -5.39 3.15 5.55"));
            list.Add(string.Format("LOAD 173 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.63 9.1675 10.9925"));
            list.Add(string.Format("1 UNI GY -9.9 7.58 9.54"));
            list.Add(string.Format("1 UNI GY -5.39 3.4 5.8"));
            list.Add(string.Format("LOAD 174 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.63 9.4175 11.2425"));
            list.Add(string.Format("1 UNI GY -9.9 7.83 9.79"));
            list.Add(string.Format("1 UNI GY -5.39 3.65 6.05"));
            list.Add(string.Format("LOAD 175 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.63 9.6675 11.4925"));
            list.Add(string.Format("1 UNI GY -9.9 8.08 10.04"));
            list.Add(string.Format("1 UNI GY -5.39 3.9 6.3"));
            list.Add(string.Format("LOAD 176 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.32 9.9175 11.7425"));
            list.Add(string.Format("1 UNI GY -9.9 8.33 10.29"));
            list.Add(string.Format("1 UNI GY -5.39 4.15 6.55"));
            list.Add(string.Format("LOAD 177 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -12.53 10.1675 11.9925"));
            list.Add(string.Format("1 UNI GY -9.9 8.58 10.54"));
            list.Add(string.Format("1 UNI GY -5.39 4.4 6.8"));
            list.Add(string.Format("LOAD 178 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.11 10.4175 12.2425"));
            list.Add(string.Format("1 UNI GY -9.9 8.83 10.79"));
            list.Add(string.Format("1 UNI GY -5.39 4.65 7.05"));
            list.Add(string.Format("LOAD 179 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -16.27 10.6675 12.4925"));
            list.Add(string.Format("1 UNI GY -9.9 9.08 11.04"));
            list.Add(string.Format("1 UNI GY -5.39 4.9 7.3"));
            list.Add(string.Format("LOAD 180 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -22.43 10.9175 12.7425"));
            list.Add(string.Format("1 UNI GY -9.9 9.33 11.29"));
            list.Add(string.Format("1 UNI GY -5.39 5.15 7.55"));
            list.Add(string.Format("LOAD 181 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -9.9 9.58 11.54"));
            list.Add(string.Format("1 UNI GY -5.39 5.4 7.8"));
            list.Add(string.Format("LOAD 182 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -10.46 9.83 11.79"));
            list.Add(string.Format("1 UNI GY -5.39 5.65 8.05"));
            list.Add(string.Format("LOAD 183 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.56 10.08 12"));
            list.Add(string.Format("1 UNI GY -5.39 5.9 8.3"));
            list.Add(string.Format("LOAD 184 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -13.01 10.33 12"));
            list.Add(string.Format("1 UNI GY -5.39 6.15 8.55"));
            list.Add(string.Format("LOAD 185 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -14.96 10.58 12"));
            list.Add(string.Format("1 UNI GY -5.39 6.4 8.8"));
            list.Add(string.Format("LOAD 186 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -20.16 10.83 12"));
            list.Add(string.Format("1 UNI GY -5.39 6.65 9.05"));
            list.Add(string.Format("LOAD 187 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 6.9 9.3"));
            list.Add(string.Format("LOAD 188 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 7.15 9.55"));
            list.Add(string.Format("LOAD 189 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 7.4 9.8"));
            list.Add(string.Format("LOAD 190 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 7.65 10.05"));
            list.Add(string.Format("LOAD 191 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 7.9 10.3"));
            list.Add(string.Format("LOAD 193 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 8.15 10.55"));
            list.Add(string.Format("LOAD 194 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 8.4 10.8"));
            list.Add(string.Format("LOAD 195 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 8.65 11.05"));
            list.Add(string.Format("LOAD 196 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 8.9 11.3"));
            list.Add(string.Format("LOAD 197 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 9.15 11.55"));
            list.Add(string.Format("LOAD 198 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.39 9.4 11.8"));
            list.Add(string.Format("LOAD 199 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.78 9.65 12"));
            list.Add(string.Format("LOAD 200 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -6.41 9.9 12"));
            list.Add(string.Format("LOAD 201 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.23 10.15 12"));
            list.Add(string.Format("LOAD 202 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.36 10.4 12"));
            list.Add(string.Format("LOAD 203 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.8 10.65 12"));
            list.Add(string.Format("LOAD 301 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.26 0 0.6"));
            list.Add(string.Format("LOAD 302 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.12 0 0.975"));
            list.Add(string.Format("LOAD 303 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.78 0 1.35"));
            list.Add(string.Format("LOAD 304 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 0 1.655"));
            list.Add(string.Format("LOAD 305 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.04 0.1 1.905"));
            list.Add(string.Format("LOAD 306 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.85 0.345 2.155"));
            list.Add(string.Format("1 UNI GY -7.45 0 0.645"));
            list.Add(string.Format("LOAD 307 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 0.595 2.405"));
            list.Add(string.Format("1 UNI GY -3.86 0 1.02"));
            list.Add(string.Format("LOAD 308 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 0.845 2.655"));
            list.Add(string.Format("1 UNI GY -2.68 0 1.395"));
            list.Add(string.Format("LOAD 309 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 1.095 2.905"));
            list.Add(string.Format("1 UNI GY -2.25 0 1.685"));
            list.Add(string.Format("LOAD 310 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 1.345 3.155"));
            list.Add(string.Format("1 UNI GY -2.01 0.125 1.935"));
            list.Add(string.Format("LOAD 311 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 1.595 3.405"));
            list.Add(string.Format("1 UNI GY -1.83 0.375 2.185"));
            list.Add(string.Format("LOAD 312 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 1.845 3.655"));
            list.Add(string.Format("1 UNI GY -1.79 0.625 2.435"));
            list.Add(string.Format("LOAD 313 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 2.095 3.905"));
            list.Add(string.Format("1 UNI GY -1.79 0.875 2.685"));
            list.Add(string.Format("LOAD 314 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 2.345 4.155"));
            list.Add(string.Format("1 UNI GY -1.79 1.125 2.935"));
            list.Add(string.Format("LOAD 315 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 2.595 4.405"));
            list.Add(string.Format("1 UNI GY -1.79 1.375 3.185"));
            list.Add(string.Format("LOAD 316 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 2.845 4.655"));
            list.Add(string.Format("1 UNI GY -1.79 1.625 3.435"));
            list.Add(string.Format("LOAD 317 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 3.095 4.905"));
            list.Add(string.Format("1 UNI GY -1.79 1.875 3.685"));
            list.Add(string.Format("LOAD 318 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 3.345 5.155"));
            list.Add(string.Format("1 UNI GY -1.79 2.125 3.935"));
            list.Add(string.Format("LOAD 319 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 3.595 5.405"));
            list.Add(string.Format("1 UNI GY -1.79 2.375 4.185"));
            list.Add(string.Format("LOAD 320 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 3.845 5.655"));
            list.Add(string.Format("1 UNI GY -1.79 2.625 4.435"));
            list.Add(string.Format("LOAD 321 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 4.095 5.905"));
            list.Add(string.Format("1 UNI GY -1.79 2.875 4.685"));
            list.Add(string.Format("LOAD 322 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 4.345 6.155"));
            list.Add(string.Format("1 UNI GY -1.79 3.125 4.935"));
            list.Add(string.Format("LOAD 323 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 4.595 6.405"));
            list.Add(string.Format("1 UNI GY -1.79 3.375 5.185"));
            list.Add(string.Format("LOAD 324 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 4.845 6.655"));
            list.Add(string.Format("1 UNI GY -1.79 3.625 5.435"));
            list.Add(string.Format("LOAD 325 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 5.095 6.905"));
            list.Add(string.Format("1 UNI GY -1.79 3.875 5.685"));
            list.Add(string.Format("LOAD 326 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 5.345 7.155"));
            list.Add(string.Format("1 UNI GY -1.79 4.125 5.935"));
            list.Add(string.Format("LOAD 327 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 5.595 7.405"));
            list.Add(string.Format("1 UNI GY -1.79 4.375 6.185"));
            list.Add(string.Format("LOAD 328 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 5.845 7.655"));
            list.Add(string.Format("1 UNI GY -1.79 4.625 6.435"));
            list.Add(string.Format("LOAD 329 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 6.095 7.905"));
            list.Add(string.Format("1 UNI GY -1.79 4.875 6.685"));
            list.Add(string.Format("LOAD 330 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 6.345 8.155"));
            list.Add(string.Format("1 UNI GY -1.79 5.125 6.935"));
            list.Add(string.Format("LOAD 331 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 6.595 8.405"));
            list.Add(string.Format("1 UNI GY -1.79 5.375 7.185"));
            list.Add(string.Format("LOAD 332 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 6.845 8.655"));
            list.Add(string.Format("1 UNI GY -1.79 5.625 7.435"));
            list.Add(string.Format("LOAD 333 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 7.095 8.905"));
            list.Add(string.Format("1 UNI GY -1.79 5.875 7.685"));
            list.Add(string.Format("LOAD 334 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 7.345 9.155"));
            list.Add(string.Format("1 UNI GY -1.79 6.125 7.935"));
            list.Add(string.Format("LOAD 335 LOADTYPE None  TITLE 70R WHEEL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 7.595 9.405"));
            list.Add(string.Format("1 UNI GY -1.79 6.375 8.185"));
            list.Add(string.Format("LOAD 336 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 7.845 9.655"));
            list.Add(string.Format("1 UNI GY -1.79 6.625 8.435"));
            list.Add(string.Format("LOAD 337 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 8.095 9.905"));
            list.Add(string.Format("1 UNI GY -1.79 6.875 8.685"));
            list.Add(string.Format("LOAD 338 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 8.345 10.155"));
            list.Add(string.Format("1 UNI GY -1.79 7.125 8.935"));
            list.Add(string.Format("LOAD 339 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 8.595 10.405"));
            list.Add(string.Format("1 UNI GY -1.79 7.375 9.185"));
            list.Add(string.Format("LOAD 340 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 8.845 10.655"));
            list.Add(string.Format("1 UNI GY -1.79 7.625 9.435"));
            list.Add(string.Format("LOAD 341 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 9.095 10.905"));
            list.Add(string.Format("1 UNI GY -1.79 7.875 9.685"));
            list.Add(string.Format("LOAD 342 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 9.345 11.155"));
            list.Add(string.Format("1 UNI GY -1.79 8.125 9.935"));
            list.Add(string.Format("LOAD 343 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.79 9.595 11.405"));
            list.Add(string.Format("1 UNI GY -1.79 8.375 10.185"));
            list.Add(string.Format("LOAD 344 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -1.85 9.845 11.655"));
            list.Add(string.Format("1 UNI GY -1.79 8.625 10.435"));
            list.Add(string.Format("LOAD 345 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.04 10.095 11.905"));
            list.Add(string.Format("1 UNI GY -1.79 8.875 10.685"));
            list.Add(string.Format("LOAD 346 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.28 10.345 12"));
            list.Add(string.Format("1 UNI GY -1.79 9.125 10.935"));
            list.Add(string.Format("LOAD 347 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.78 10.65 12"));
            list.Add(string.Format("1 UNI GY -1.79 9.375 11.185"));
            list.Add(string.Format("LOAD 348 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.12 11.025 12"));
            list.Add(string.Format("1 UNI GY -1.79 9.625 11.435"));
            list.Add(string.Format("LOAD 349 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.26 11.4 12"));
            list.Add(string.Format("1 UNI GY -1.87 9.875 11.685"));
            list.Add(string.Format("LOAD 350 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.06 10.125 11.935"));
            list.Add(string.Format("LOAD 351 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.32 10.375 12"));
            list.Add(string.Format("LOAD 352 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -2.88 10.695 12"));
            list.Add(string.Format("LOAD 353 LOADTYPE None  TITLE 70R BOGIE"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.41 11.07 12"));
            list.Add(string.Format("LOAD 401 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.73 0 1.2"));
            list.Add(string.Format("LOAD 402 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.85 0 1.45"));
            list.Add(string.Format("LOAD 403 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.7"));
            list.Add(string.Format("LOAD 404 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.95"));
            list.Add(string.Format("LOAD 405 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0 2.2"));
            list.Add(string.Format("LOAD 406 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0.05 2.45"));
            list.Add(string.Format("LOAD 407 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0.3 2.7"));
            list.Add(string.Format("LOAD 408 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0.55 2.95"));
            list.Add(string.Format("LOAD 409 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 0.8 3.2"));
            list.Add(string.Format("LOAD 410 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 1.05 3.45"));
            list.Add(string.Format("LOAD 411 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 1.3 3.7"));
            list.Add(string.Format("LOAD 412 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 1.55 3.95"));
            list.Add(string.Format("LOAD 413 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 1.8 4.2"));
            list.Add(string.Format("1 UNI GY -26.73 0 1.2"));
            list.Add(string.Format("LOAD 414 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 2.05 4.45"));
            list.Add(string.Format("1 UNI GY -11.85 0 1.15"));
            list.Add(string.Format("LOAD 415 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 2.3 4.7"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.4"));
            list.Add(string.Format("LOAD 416 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 2.55 4.95"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.65"));
            list.Add(string.Format("LOAD 417 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 2.8 5.2"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.9"));
            list.Add(string.Format("LOAD 418 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 3.05 5.45"));
            list.Add(string.Format("1 UNI GY -7.88 0.05 2.15"));
            list.Add(string.Format("LOAD 419 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 3.3 5.7"));
            list.Add(string.Format("1 UNI GY -7.88 0.3 2.4"));
            list.Add(string.Format("LOAD 420 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 3.55 5.95"));
            list.Add(string.Format("1 UNI GY -7.88 0.55 2.65"));
            list.Add(string.Format("LOAD 421 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 3.8 6.2"));
            list.Add(string.Format("1 UNI GY -7.88 0.8 2.9"));
            list.Add(string.Format("LOAD 422 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 4.05 6.45"));
            list.Add(string.Format("1 UNI GY -7.88 1.05 3.15"));
            list.Add(string.Format("LOAD 423 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 4.3 6.7"));
            list.Add(string.Format("1 UNI GY -7.88 1.3 3.4"));
            list.Add(string.Format("LOAD 424 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 4.55 6.95"));
            list.Add(string.Format("1 UNI GY -7.88 1.55 3.65"));
            list.Add(string.Format("LOAD 425 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 4.8 7.2"));
            list.Add(string.Format("1 UNI GY -7.88 1.8 3.9"));
            list.Add(string.Format("1 UNI GY -26.73 0 1.2"));
            list.Add(string.Format("LOAD 426 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 5.05 7.45"));
            list.Add(string.Format("1 UNI GY -7.88 2.05 4.15"));
            list.Add(string.Format("1 UNI GY -11.85 0 1.45"));
            list.Add(string.Format("LOAD 427 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 5.3 7.7"));
            list.Add(string.Format("1 UNI GY -7.88 2.3 4.4"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.7"));
            list.Add(string.Format("LOAD 428 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 5.55 7.95"));
            list.Add(string.Format("1 UNI GY -7.88 2.55 4.65"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.95"));
            list.Add(string.Format("LOAD 429 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 5.8 8.2"));
            list.Add(string.Format("1 UNI GY -7.88 2.8 4.9"));
            list.Add(string.Format("1 UNI GY -7.88 0 2.2"));
            list.Add(string.Format("LOAD 430 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 6.05 8.45"));
            list.Add(string.Format("1 UNI GY -7.88 3.05 5.15"));
            list.Add(string.Format("1 UNI GY -7.88 0.05 2.45"));
            list.Add(string.Format("LOAD 431 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 6.3 8.7"));
            list.Add(string.Format("1 UNI GY -7.88 3.3 5.4"));
            list.Add(string.Format("1 UNI GY -7.88 0.3 2.7"));
            list.Add(string.Format("LOAD 432 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 6.55 8.95"));
            list.Add(string.Format("1 UNI GY -7.88 3.55 5.65"));
            list.Add(string.Format("1 UNI GY -7.88 0.55 2.95"));
            list.Add(string.Format("LOAD 433 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 6.8 9.2"));
            list.Add(string.Format("1 UNI GY -7.88 3.8 5.9"));
            list.Add(string.Format("1 UNI GY -7.88 0.8 3.2"));
            list.Add(string.Format("LOAD 434 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.05 9.45"));
            list.Add(string.Format("1 UNI GY -7.88 4.05 6.15"));
            list.Add(string.Format("1 UNI GY -7.88 1.05 3.45"));
            list.Add(string.Format("LOAD 435 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.3 9.7"));
            list.Add(string.Format("1 UNI GY -7.88 4.3 6.4"));
            list.Add(string.Format("1 UNI GY -7.88 1.3 3.7"));
            list.Add(string.Format("LOAD 436 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.55 9.95"));
            list.Add(string.Format("1 UNI GY -7.88 4.55 6.65"));
            list.Add(string.Format("1 UNI GY -7.88 1.55 3.95"));
            list.Add(string.Format("LOAD 437 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.8 10.2"));
            list.Add(string.Format("1 UNI GY -7.88 4.8 6.9"));
            list.Add(string.Format("1 UNI GY -7.88 1.8 4.2"));
            list.Add(string.Format("1 UNI GY -26.73 0 0.9"));
            list.Add(string.Format("LOAD 438 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.05 10.45"));
            list.Add(string.Format("1 UNI GY -7.88 5.05 7.15"));
            list.Add(string.Format("1 UNI GY -7.88 2.05 4.45"));
            list.Add(string.Format("1 UNI GY -11.85 0 1.15"));
            list.Add(string.Format("LOAD 439 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.3 10.7"));
            list.Add(string.Format("1 UNI GY -7.88 5.3 7.4"));
            list.Add(string.Format("1 UNI GY -7.88 2.3 4.7"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.4"));
            list.Add(string.Format("LOAD 440 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.55 10.95"));
            list.Add(string.Format("1 UNI GY -7.88 5.55 7.65"));
            list.Add(string.Format("1 UNI GY -7.88 2.55 4.95"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.65"));
            list.Add(string.Format("LOAD 441 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.8 11.2"));
            list.Add(string.Format("1 UNI GY -7.88 5.8 7.9"));
            list.Add(string.Format("1 UNI GY -7.88 2.8 5.2"));
            list.Add(string.Format("1 UNI GY -7.88 0 1.9"));
            list.Add(string.Format("LOAD 442 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.05 11.45"));
            list.Add(string.Format("1 UNI GY -7.88 6.05 8.15"));
            list.Add(string.Format("1 UNI GY -7.88 3.05 5.45"));
            list.Add(string.Format("1 UNI GY -7.88 0.05 2.15"));
            list.Add(string.Format("LOAD 443 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.3 11.7"));
            list.Add(string.Format("1 UNI GY -7.88 6.3 8.4"));
            list.Add(string.Format("1 UNI GY -7.88 3.3 5.7"));
            list.Add(string.Format("1 UNI GY -7.88 0.3 2.4"));
            list.Add(string.Format("LOAD 444 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.55 11.95"));
            list.Add(string.Format("1 UNI GY -7.88 6.55 8.65"));
            list.Add(string.Format("1 UNI GY -7.88 3.55 5.95"));
            list.Add(string.Format("1 UNI GY -7.88 0.55 2.65"));
            list.Add(string.Format("LOAD 445 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.8 12"));
            list.Add(string.Format("1 UNI GY -7.88 6.8 8.9"));
            list.Add(string.Format("1 UNI GY -7.88 3.8 6.2"));
            list.Add(string.Format("1 UNI GY -7.88 0.8 2.9"));
            list.Add(string.Format("LOAD 446 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.05 12"));
            list.Add(string.Format("1 UNI GY -7.88 7.05 9.15"));
            list.Add(string.Format("1 UNI GY -7.88 4.05 6.45"));
            list.Add(string.Format("1 UNI GY -7.88 1.05 3.15"));
            list.Add(string.Format("LOAD 447 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.3 12"));
            list.Add(string.Format("1 UNI GY -7.88 7.3 9.4"));
            list.Add(string.Format("1 UNI GY -7.88 4.3 6.7"));
            list.Add(string.Format("1 UNI GY -7.88 1.3 3.4"));
            list.Add(string.Format("LOAD 448 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.85 10.55 12"));
            list.Add(string.Format("1 UNI GY -7.88 7.55 9.65"));
            list.Add(string.Format("1 UNI GY -7.88 4.55 6.95"));
            list.Add(string.Format("1 UNI GY -7.88 1.55 3.65"));
            list.Add(string.Format("LOAD 449 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -26.73 10.8 13.2"));
            list.Add(string.Format("1 UNI GY -7.88 7.8 9.9"));
            list.Add(string.Format("1 UNI GY -7.88 4.8 7.2"));
            list.Add(string.Format("1 UNI GY -7.88 1.8 3.9"));
            list.Add(string.Format("LOAD 450 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.05 10.15"));
            list.Add(string.Format("1 UNI GY -7.88 5.05 7.45"));
            list.Add(string.Format("1 UNI GY -7.88 2.05 4.15"));
            list.Add(string.Format("LOAD 451 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.3 10.4"));
            list.Add(string.Format("1 UNI GY -7.88 5.3 7.7"));
            list.Add(string.Format("1 UNI GY -7.88 2.3 4.4"));
            list.Add(string.Format("LOAD 452 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.55 10.65"));
            list.Add(string.Format("1 UNI GY -7.88 5.55 7.95"));
            list.Add(string.Format("1 UNI GY -7.88 2.55 4.65"));
            list.Add(string.Format("LOAD 453 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.8 10.9"));
            list.Add(string.Format("1 UNI GY -7.88 5.8 8.2"));
            list.Add(string.Format("1 UNI GY -7.88 2.8 4.9"));
            list.Add(string.Format("LOAD 454 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.05 11.15"));
            list.Add(string.Format("1 UNI GY -7.88 6.05 8.45"));
            list.Add(string.Format("1 UNI GY -7.88 3.05 5.15"));
            list.Add(string.Format("LOAD 455 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.3 11.4"));
            list.Add(string.Format("1 UNI GY -7.88 6.3 8.7"));
            list.Add(string.Format("1 UNI GY -7.88 3.3 5.4"));
            list.Add(string.Format("1 UNI GY -26.73 0 1.1"));
            list.Add(string.Format("LOAD 456 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.55 11.65"));
            list.Add(string.Format("1 UNI GY -7.88 6.55 8.95"));
            list.Add(string.Format("1 UNI GY -7.88 3.55 5.65"));
            list.Add(string.Format("1 UNI GY -17.6 0 1.35"));
            list.Add(string.Format("LOAD 457 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.8 11.9"));
            list.Add(string.Format("1 UNI GY -7.88 6.8 9.2"));
            list.Add(string.Format("1 UNI GY -7.88 3.8 5.9"));
            list.Add(string.Format("1 UNI GY -17.6 0 1.6"));
            list.Add(string.Format("LOAD 458 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.05 12.15"));
            list.Add(string.Format("1 UNI GY -7.88 7.05 9.45"));
            list.Add(string.Format("1 UNI GY -7.88 4.05 6.15"));
            list.Add(string.Format("1 UNI GY -17.6 0.05 1.85"));
            list.Add(string.Format("LOAD 459 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.3 12.4"));
            list.Add(string.Format("1 UNI GY -7.88 7.3 9.7"));
            list.Add(string.Format("1 UNI GY -7.88 4.3 6.4"));
            list.Add(string.Format("1 UNI GY -17.6 0.3 2.1"));
            list.Add(string.Format("1 UNI GY -48.72 0 0.9"));
            list.Add(string.Format("LOAD 460 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.85 10.55 12.65"));
            list.Add(string.Format("1 UNI GY -7.88 7.8 10.2"));
            list.Add(string.Format("1 UNI GY -7.88 4.55 6.65"));
            list.Add(string.Format("1 UNI GY -17.6 0.55 2.35"));
            list.Add(string.Format("1 UNI GY -24.07 0 1.15"));
            list.Add(string.Format("LOAD 461 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.05 10.45"));
            list.Add(string.Format("1 UNI GY -7.88 4.8 6.9"));
            list.Add(string.Format("1 UNI GY -17.6 0.8 2.6"));
            list.Add(string.Format("1 UNI GY -17.6 0 1.4"));
            list.Add(string.Format("LOAD 462 LOADTYPE None   "));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.3 10.7"));
            list.Add(string.Format("1 UNI GY -7.88 5.05 7.15"));
            list.Add(string.Format("1 UNI GY -17.6 1.05 2.85"));
            list.Add(string.Format("1 UNI GY -17.6 0 1.65"));
            list.Add(string.Format("LOAD 463 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.55 10.95"));
            list.Add(string.Format("1 UNI GY -7.88 5.3 7.4"));
            list.Add(string.Format("1 UNI GY -17.6 1.3 3.1"));
            list.Add(string.Format("1 UNI GY -17.6 0.1 1.9"));
            list.Add(string.Format("LOAD 464 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.8 11.2"));
            list.Add(string.Format("1 UNI GY -7.88 5.55 7.65"));
            list.Add(string.Format("1 UNI GY -17.6 1.55 3.35"));
            list.Add(string.Format("1 UNI GY -17.6 0.35 2.15"));
            list.Add(string.Format("LOAD 465 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.05 11.45"));
            list.Add(string.Format("1 UNI GY -7.88 5.8 7.9"));
            list.Add(string.Format("1 UNI GY -17.6 1.8 3.6"));
            list.Add(string.Format("1 UNI GY -17.6 0.6 2.4"));
            list.Add(string.Format("LOAD 466 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.3 11.7"));
            list.Add(string.Format("1 UNI GY -7.88 6.05 8.15"));
            list.Add(string.Format("1 UNI GY -17.6 2.05 3.85"));
            list.Add(string.Format("1 UNI GY -17.6 0.85 2.65"));
            list.Add(string.Format("LOAD 467 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.55 11.95"));
            list.Add(string.Format("1 UNI GY -7.88 6.3 8.4"));
            list.Add(string.Format("1 UNI GY -17.6 2.3 4.1"));
            list.Add(string.Format("1 UNI GY -17.6 1.1 2.9"));
            list.Add(string.Format("LOAD 468 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.8 12"));
            list.Add(string.Format("1 UNI GY -7.88 6.55 8.65"));
            list.Add(string.Format("1 UNI GY -17.6 2.55 4.35"));
            list.Add(string.Format("1 UNI GY -17.6 1.35 3.15"));
            list.Add(string.Format("LOAD 469 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.05 12"));
            list.Add(string.Format("1 UNI GY -7.88 6.8 8.9"));
            list.Add(string.Format("1 UNI GY -17.6 2.8 4.6"));
            list.Add(string.Format("1 UNI GY -17.6 1.6 3.4"));
            list.Add(string.Format("LOAD 470 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.3 12"));
            list.Add(string.Format("1 UNI GY -7.88 7.05 9.15"));
            list.Add(string.Format("1 UNI GY -17.6 3.05 4.85"));
            list.Add(string.Format("1 UNI GY -17.6 1.85 3.65"));
            list.Add(string.Format("LOAD 471 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.85 10.55 12"));
            list.Add(string.Format("1 UNI GY -7.88 7.3 9.4"));
            list.Add(string.Format("1 UNI GY -17.6 3.3 5.1"));
            list.Add(string.Format("1 UNI GY -17.6 2.1 3.9"));
            list.Add(string.Format("LOAD 472 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.55 9.65"));
            list.Add(string.Format("1 UNI GY -17.6 3.55 5.35"));
            list.Add(string.Format("1 UNI GY -17.6 2.35 4.15"));
            list.Add(string.Format("1 UNI GY -15.9 0 0.925"));
            list.Add(string.Format("1 UNI GY -10.9 0 0.9"));
            list.Add(string.Format("LOAD 473 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 7.8 9.9"));
            list.Add(string.Format("1 UNI GY -7.88 7.8 9.9"));
            list.Add(string.Format("1 UNI GY -17.6 3.8 5.6"));
            list.Add(string.Format("1 UNI GY -17.6 2.6 4.4"));
            list.Add(string.Format("1 UNI GY -6.74 0 1.175"));
            list.Add(string.Format("1 UNI GY -5.36 0 1.28"));
            list.Add(string.Format("LOAD 474 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.05 10.15"));
            list.Add(string.Format("1 UNI GY -17.6 4.05 5.85"));
            list.Add(string.Format("1 UNI GY -17.6 2.85 4.65"));
            list.Add(string.Format("1 UNI GY -4.35 0 1.425"));
            list.Add(string.Format("1 UNI GY -4.29 0 1.58"));
            list.Add(string.Format("LOAD 475 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.3 10.4"));
            list.Add(string.Format("1 UNI GY -17.6 4.3 6.1"));
            list.Add(string.Format("1 UNI GY -17.6 3.1 4.9"));
            list.Add(string.Format("1 UNI GY -4.29 0 1.675"));
            list.Add(string.Format("1 UNI GY -4.29 0.08 1.83"));
            list.Add(string.Format("LOAD 476 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.55 10.65"));
            list.Add(string.Format("1 UNI GY -17.6 4.55 6.35"));
            list.Add(string.Format("1 UNI GY -17.6 3.35 5.15"));
            list.Add(string.Format("1 UNI GY -4.29 0.18 1.925"));
            list.Add(string.Format("1 UNI GY -4.29 0.33 2.08"));
            list.Add(string.Format("LOAD 477 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 8.8 10.9"));
            list.Add(string.Format("1 UNI GY -17.6 4.8 6.6"));
            list.Add(string.Format("1 UNI GY -17.6 3.6 5.4"));
            list.Add(string.Format("1 UNI GY -4.29 0.43 2.175"));
            list.Add(string.Format("1 UNI GY -10.9 0 0.9"));
            list.Add(string.Format("LOAD 478 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.05 11.15"));
            list.Add(string.Format("1 UNI GY -17.6 5.05 6.85"));
            list.Add(string.Format("1 UNI GY -17.6 3.85 5.65"));
            list.Add(string.Format("1 UNI GY -4.29 0.68 2.425"));
            list.Add(string.Format("1 UNI GY -5.36 0 1.28"));
            list.Add(string.Format("LOAD 479 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.3 11.4"));
            list.Add(string.Format("1 UNI GY -17.6 5.3 7.1"));
            list.Add(string.Format("1 UNI GY -17.6 4.1 5.9"));
            list.Add(string.Format("1 UNI GY -4.29 0.93 2.675"));
            list.Add(string.Format("1 UNI GY -4.29 0 1.58"));
            list.Add(string.Format("LOAD 480 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.55 11.65"));
            list.Add(string.Format("1 UNI GY -17.6 5.55 7.35"));
            list.Add(string.Format("1 UNI GY -17.6 4.35 6.15"));
            list.Add(string.Format("1 UNI GY -4.29 1.175 2.925"));
            list.Add(string.Format("1 UNI GY -4.29 0.08 1.83"));
            list.Add(string.Format("LOAD 481 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 9.8 11.9"));
            list.Add(string.Format("1 UNI GY -17.6 5.8 7.6"));
            list.Add(string.Format("1 UNI GY -17.6 4.6 6.4"));
            list.Add(string.Format("1 UNI GY -4.29 1.425 3.175"));
            list.Add(string.Format("1 UNI GY -4.29 0.33 2.08"));
            list.Add(string.Format("LOAD 482 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.05 12"));
            list.Add(string.Format("1 UNI GY -17.6 6.05 7.85"));
            list.Add(string.Format("1 UNI GY -17.6 4.85 6.65"));
            list.Add(string.Format("1 UNI GY -4.29 1.675 3.425"));
            list.Add(string.Format("1 UNI GY -4.29 0.58 2.33"));
            list.Add(string.Format("LOAD 483 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.88 10.3 12"));
            list.Add(string.Format("1 UNI GY -17.6 6.3 8.1"));
            list.Add(string.Format("1 UNI GY -17.6 5.1 6.9"));
            list.Add(string.Format("1 UNI GY -4.29 1.925 3.675"));
            list.Add(string.Format("1 UNI GY -4.29 0.83 2.58"));
            list.Add(string.Format("LOAD 484 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -11.85 10.55 12"));
            list.Add(string.Format("1 UNI GY -17.6 6.55 8.35"));
            list.Add(string.Format("1 UNI GY -17.6 5.35 7.15"));
            list.Add(string.Format("1 UNI GY -4.29 2.175 3.925"));
            list.Add(string.Format("1 UNI GY -4.29 1.08 2.83"));
            list.Add(string.Format("LOAD 485 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 6.8 8.6"));
            list.Add(string.Format("1 UNI GY -17.6 5.6 7.4"));
            list.Add(string.Format("1 UNI GY -4.29 2.425 4.175"));
            list.Add(string.Format("1 UNI GY -4.29 1.33 3.08"));
            list.Add(string.Format("LOAD 486 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 7.05 8.85"));
            list.Add(string.Format("1 UNI GY -17.6 5.85 7.65"));
            list.Add(string.Format("1 UNI GY -4.29 2.675 4.425"));
            list.Add(string.Format("1 UNI GY -4.29 1.58 3.33"));
            list.Add(string.Format("LOAD 487 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 7.3 9.1"));
            list.Add(string.Format("1 UNI GY -17.6 6.1 7.9"));
            list.Add(string.Format("1 UNI GY -4.29 2.925 4.675"));
            list.Add(string.Format("1 UNI GY -4.29 1.83 3.58"));
            list.Add(string.Format("LOAD 488 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 7.55 9.35"));
            list.Add(string.Format("1 UNI GY -17.6 6.35 8.15"));
            list.Add(string.Format("1 UNI GY -4.29 3.175 4.925"));
            list.Add(string.Format("1 UNI GY -4.29 2.08 3.83"));
            list.Add(string.Format("LOAD 489 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 7.8 9.6"));
            list.Add(string.Format("1 UNI GY -17.6 6.6 8.4"));
            list.Add(string.Format("1 UNI GY -4.29 3.425 5.175"));
            list.Add(string.Format("1 UNI GY -4.29 2.33 4.08"));
            list.Add(string.Format("LOAD 490 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 8.05 9.85"));
            list.Add(string.Format("1 UNI GY -17.6 6.85 8.65"));
            list.Add(string.Format("1 UNI GY -4.29 3.675 5.425"));
            list.Add(string.Format("1 UNI GY -4.29 2.58 4.33"));
            list.Add(string.Format("LOAD 491 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 8.3 10.1"));
            list.Add(string.Format("1 UNI GY -17.6 7.1 8.9"));
            list.Add(string.Format("1 UNI GY -4.29 3.925 5.675"));
            list.Add(string.Format("1 UNI GY -4.29 2.83 4.58"));
            list.Add(string.Format("LOAD 492 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 8.55 10.35"));
            list.Add(string.Format("1 UNI GY -17.6 7.35 9.15"));
            list.Add(string.Format("1 UNI GY -4.29 4.175 5.925"));
            list.Add(string.Format("1 UNI GY -4.29 3.08 4.83"));
            list.Add(string.Format("LOAD 493 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 8.8 10.6"));
            list.Add(string.Format("1 UNI GY -17.6 7.6 9.4"));
            list.Add(string.Format("1 UNI GY -4.29 4.425 6.175"));
            list.Add(string.Format("1 UNI GY -4.29 3.33 5.08"));
            list.Add(string.Format("LOAD 494 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 9.05 10.85"));
            list.Add(string.Format("1 UNI GY -17.6 7.85 9.65"));
            list.Add(string.Format("1 UNI GY -4.29 4.675 6.425"));
            list.Add(string.Format("1 UNI GY -4.29 3.58 5.33"));
            list.Add(string.Format("LOAD 495 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 9.3 11.1"));
            list.Add(string.Format("1 UNI GY -17.6 8.1 9.9"));
            list.Add(string.Format("1 UNI GY -4.29 4.925 6.675"));
            list.Add(string.Format("1 UNI GY -4.29 3.83 5.58"));
            list.Add(string.Format("LOAD 496 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 9.55 11.35"));
            list.Add(string.Format("1 UNI GY -17.6 8.35 10.15"));
            list.Add(string.Format("1 UNI GY -4.29 5.175 6.925"));
            list.Add(string.Format("1 UNI GY -4.29 4.08 5.83"));
            list.Add(string.Format("LOAD 497 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 9.8 11.6"));
            list.Add(string.Format("1 UNI GY -17.6 8.6 10.4"));
            list.Add(string.Format("1 UNI GY -4.29 5.425 7.175"));
            list.Add(string.Format("1 UNI GY -4.29 4.33 6.08"));
            list.Add(string.Format("LOAD 498 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.05 11.85"));
            list.Add(string.Format("1 UNI GY -17.6 8.85 10.65"));
            list.Add(string.Format("1 UNI GY -4.29 5.675 7.425"));
            list.Add(string.Format("1 UNI GY -4.29 4.58 6.33"));
            list.Add(string.Format("LOAD 499 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.3 12"));
            list.Add(string.Format("1 UNI GY -17.6 9.1 10.9"));
            list.Add(string.Format("1 UNI GY -4.29 5.925 7.675"));
            list.Add(string.Format("1 UNI GY -4.29 4.83 6.58"));
            list.Add(string.Format("LOAD 500 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.55 12"));
            list.Add(string.Format("1 UNI GY -17.6 9.35 11.15"));
            list.Add(string.Format("1 UNI GY -4.29 6.175 7.925"));
            list.Add(string.Format("1 UNI GY -4.29 5.08 6.83"));
            list.Add(string.Format("LOAD 501 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -21.91 10.8 12"));
            list.Add(string.Format("1 UNI GY -17.6 9.6 11.4"));
            list.Add(string.Format("1 UNI GY -4.29 6.425 8.175"));
            list.Add(string.Format("1 UNI GY -4.29 5.33 7.08"));
            list.Add(string.Format("LOAD 502 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -40.32 11.05 12"));
            list.Add(string.Format("1 UNI GY -17.6 9.85 11.65"));
            list.Add(string.Format("1 UNI GY -4.29 6.675 8.425"));
            list.Add(string.Format("1 UNI GY -4.29 5.58 7.33"));
            list.Add(string.Format("LOAD 503 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.1 11.9"));
            list.Add(string.Format("1 UNI GY -4.29 6.925 8.675"));
            list.Add(string.Format("1 UNI GY -4.29 5.83 7.58"));
            list.Add(string.Format("LOAD 504 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.35 12"));
            list.Add(string.Format("1 UNI GY -4.29 7.175 8.925"));
            list.Add(string.Format("1 UNI GY -4.29 6.08 7.83"));
            list.Add(string.Format("LOAD 505 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -17.6 10.6 12"));
            list.Add(string.Format("1 UNI GY -4.29 7.425 9.175"));
            list.Add(string.Format("1 UNI GY -4.29 6.33 8.08"));
            list.Add(string.Format("LOAD 506 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -24.07 10.85 12"));
            list.Add(string.Format("1 UNI GY -4.29 7.675 9.425"));
            list.Add(string.Format("1 UNI GY -4.29 6.58 8.33"));
            list.Add(string.Format("LOAD 507 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 7.925 9.675"));
            list.Add(string.Format("1 UNI GY -4.29 6.83 8.58"));
            list.Add(string.Format("LOAD 508 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 8.175 9.925"));
            list.Add(string.Format("1 UNI GY -4.29 7.08 8.83"));
            list.Add(string.Format("LOAD 509 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 8.425 10.175"));
            list.Add(string.Format("1 UNI GY -4.29 7.33 9.08"));
            list.Add(string.Format("LOAD 510 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 8.675 10.425"));
            list.Add(string.Format("1 UNI GY -4.29 7.58 9.33"));
            list.Add(string.Format("LOAD 511 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 8.925 10.675"));
            list.Add(string.Format("1 UNI GY -4.29 7.83 9.58"));
            list.Add(string.Format("1 UNI GY -4.29 9.175 10.925"));
            list.Add(string.Format("1 UNI GY -4.29 8.08 9.83"));
            list.Add(string.Format("LOAD 513 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 9.425 11.175"));
            list.Add(string.Format("1 UNI GY -4.29 8.33 10.08"));
            list.Add(string.Format("LOAD 514 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 9.675 11.425"));
            list.Add(string.Format("1 UNI GY -4.29 8.58 10.33"));
            list.Add(string.Format("LOAD 515 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 9.925 11.675"));
            list.Add(string.Format("1 UNI GY -4.29 8.83 10.58"));
            list.Add(string.Format("LOAD 516 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 10.175 11.925"));
            list.Add(string.Format("1 UNI GY -4.29 9.08 10.83"));
            list.Add(string.Format("LOAD 517 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 10.425 12.175"));
            list.Add(string.Format("1 UNI GY -4.29 9.33 11.08"));
            list.Add(string.Format("LOAD 518 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -5.05 10.675 12.425"));
            list.Add(string.Format("1 UNI GY -4.29 9.58 11.33"));
            list.Add(string.Format("LOAD 519 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -8.72 10.925 12.675"));
            list.Add(string.Format("1 UNI GY -4.29 9.83 11.58"));
            list.Add(string.Format("LOAD 520 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 10.08 11.83"));
            list.Add(string.Format("LOAD 521 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.29 10.33 12"));
            list.Add(string.Format("LOAD 522 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -4.35 10.58 12"));
            list.Add(string.Format("LOAD 523 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -7.86 10.95 12"));
            list.Add(string.Format("LOAD 524 LOADTYPE None  TITLE CLASS A"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 UNI GY -22.25 11.33 12"));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));

            File.WriteAllLines(Input_File_LL, list.ToArray());
        }
        public string Excel_File()
        {

            //string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            //if (iApp.user_path != "")
            //    file_path = Path.Combine(iApp.user_path, Title);

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);


            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, "Worksheet Design");

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Slab Bridge Design in LSM.xlsx");

            return file_path;
        }
        private void btn_single_cell_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_process_design)
            {
                Slab_Bridge_Design();
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
                    //iApp.Form_ASTRA_Input_Data(Input_File_DL, false).Show();
                    //System.Diagnostics.Process.Start(Input_File_DL);
                    iApp.Form_ASTRA_TEXT_Data(Input_File_DL, false).Show();
                }
            }
            else if (btn == btn_DL_report)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(Input_File_DL))) System.Diagnostics.Process.Start(MyList.Get_Analysis_Report_File(Input_File_DL));
            }
            else if (btn == btn_LL_input)
            {
                string ff = Get_LL_File(1);
                if (File.Exists(ff))
                {
                    // iApp.Form_ASTRA_Input_Data(Input_File_LL, false).Show();
                    //System.Diagnostics.Process.Start(Input_File_LL);
                    iApp.Form_ASTRA_TEXT_Data(ff, false).Show();
                }
            }
            else if (btn == btn_LL_report)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(Input_File_LL))) System.Diagnostics.Process.Start(MyList.Get_Analysis_Report_File(Input_File_LL));
            }
            else if (btn == btn_result_summary)
            {
                if (File.Exists(MyList.Get_Analysis_Report_File(Result_File))) System.Diagnostics.Process.Start(MyList.Get_Analysis_Report_File(Result_File));

                if (File.Exists((Result_File))) System.Diagnostics.Process.Start((Result_File));

            }
            Button_Enable_Disable();
            Write_All_Data(false);
        }

        public void Create_Data()
        {

            string Working_Folder = "";


            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
            {
                //file_path = Path.Combine(iApp.user_path, Title);
                Working_Folder = iApp.user_path;
            }



            string pd = "";
            if (Directory.Exists(Working_Folder))
            {
                pd = Path.Combine(Working_Folder, "Slab Analysis Dead Load");
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                Input_File_DL = Path.Combine(pd, "Slab_DL_Input_File.txt");

                pd = Path.Combine(Working_Folder, "Slab Analysis Live Load");
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);

                Input_File_LL = Path.Combine(pd, "Slab_LL_Input_File.txt");
            }


            //List<string> list = new List<string>();

            Slab_Model = new Slab_Bridge_Model();
            DataGridView dgv = dgv_design_data;

            //box.No_of_Cells = dgv_design_data_multi

            DataInputCollections dips = new DataInputCollections();

            dips.Load_Data_from_Grid(dgv);

            //Slab_Model.No_of_Cells = dips[0].ToInt;

            Slab_Model.Width = dips[1].ToDouble;
            Slab_Model.Length = dips[3].ToDouble;

            Slab_Model.Slab_Thickness = dips[24].ToDouble;




            Slab_Model.Create_Data_Live_Load();

            Slab_Model.LL_TXT_Data = new List<string>(rtb_LL_TXT.Lines);

            Slab_Model.Write_Data(Input_File_LL, true);

            Slab_Model.LL_Load_Data = new List<string>(rtb_LL_1.Lines);

            Slab_Model.Write_Data(Get_LL_File(1), true);

            Slab_Model.LL_Load_Data = new List<string>(rtb_LL_2.Lines);

            Slab_Model.Write_Data(Get_LL_File(2), true);

            Slab_Model.LL_Load_Data = new List<string>(rtb_LL_3.Lines);

            Slab_Model.Write_Data(Get_LL_File(3), true);


            Slab_Model.LL_Load_Data = new List<string>(rtb_LL_4.Lines);

            Slab_Model.Write_Data(Get_LL_File(4), true);


            Slab_Model.Create_Data_Dead_Load();
            Slab_Model.Write_Data(Input_File_DL, false);




            rtb_load_data.Lines = Slab_Model.DL_Load_Data.ToArray();





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
                    //else if (i == 2)
                    else
                    {
                        flPath = Get_LL_File(i - 1);
                    }
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    i++;
                }
                while (i <= 5);

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
                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");

                    if (!iApp.Is_Progress_Cancel)
                    {                        //Show_Deckslab_Moment_Shear();
                    }
                    else
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;

                    }

                }


                Read_Singlecell_Bending_Moment_Shear_Force();

                Button_Enable_Disable();
                //Write_All_Data(false);
                iApp.Progress_Works.Clear();
                Button_Enable_Disable();

                #endregion Process
                //Write_All_Data(false);
            }
            catch (Exception ex) { }
        }

        public void Button_Enable_Disable()
        {
            if (iApp == null) return;
            string Working_Folder = "";


            Working_Folder = user_path;
            //Input_File_LL = Get_LL_File(1);
            string ll_file = Get_LL_File(1);

            string pd = "";
            if (Directory.Exists(Working_Folder))
            {

                pd = Path.Combine(Working_Folder, "Slab Analysis Dead Load");
                Input_File_DL = Path.Combine(pd, "Slab_DL_Input_File.txt");

                pd = Path.Combine(Working_Folder, "Slab Analysis Live Load");
                Input_File_LL = Path.Combine(pd, "Slab_LL_Input_File.txt");
            }


            //btn_process_design.Enabled = Directory.Exists(Working_Folder);

            btn_create_data.Enabled = Directory.Exists(iApp.user_path);
            btn_process_data.Enabled = File.Exists(Input_File_DL);
            btn_DL_input.Enabled = File.Exists(Input_File_DL);
            btn_LL_input.Enabled = File.Exists(ll_file);
            btn_DL_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(Input_File_DL));
            btn_LL_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(ll_file));


            btn_process_design.Enabled = File.Exists(Result_File);

            //grb_Design.Enabled = (btn_DL_report.Enabled && btn_LL_report.Enabled);
            grb_Design.Enabled = File.Exists(Result_File);

            //btn_.Enabled = File.Exists(Input_File_DL);
        }

        private void frmSlabBridge_Load(object sender, EventArgs e)
        {
            grb_Analysis.Enabled = false;
            grb_Design.Enabled = false;

            btn_process_design.Enabled = false;

            Set_Project_Name();

            uC_AbutmentWallType1.SetIApplication(iApp);
            uC_PierWallType1.SetIApplication(iApp);
        }

        public void Load_LL()
        {
            List<string> list = new List<string>();


            //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                if (iApp.DesignStandard != eDesignStandard.IndianStandard)
                {
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 1 HB_45_6"));
                list.Add(string.Format("45.00 45.00 45.00 45.00 "));
                list.Add(string.Format("1.8 6.0 1.8 "));
                list.Add(string.Format("1.000"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 2 HB_30_6"));
                list.Add(string.Format("30.00 30.00 30.00 30.00 "));
                list.Add(string.Format("1.8 6.0 1.8 "));
                list.Add(string.Format("1.000"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 3 HB_25_6"));
                list.Add(string.Format("25.00 25.00 25.00 25.00 "));
                list.Add(string.Format("1.8 6.0 1.8 "));
                list.Add(string.Format("1.000"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 4 HB_UNIT_6"));
                list.Add(string.Format("1.00 1.00 1.00 1.00 "));
                list.Add(string.Format("1.8 6.0 1.8 "));
                list.Add(string.Format("1.000"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 1 IRCCLASSA "));
                list.Add(string.Format("2.7 2.7 11.4 11.4 6.8 6.8 6.8 6.8 "));
                list.Add(string.Format("1.10 3.20 1.20 4.30 3.00 3.00 3.00 "));
                list.Add(string.Format("1.800 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 2 IRC70RTRACK "));
                list.Add(string.Format("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 "));
                list.Add(string.Format("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 "));
                list.Add(string.Format("2.900 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 3 IRC70RWHEEL "));
                list.Add(string.Format("17.0 17.0 17.0 17.0 12.0 12.0 8.0 "));
                list.Add(string.Format("1.37 3.05 1.37 2.13 1.52 3.96 "));
                list.Add(string.Format("2.900 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 4 IRC70RW40TBL "));
                list.Add(string.Format("10.0 10.0 "));
                list.Add(string.Format("1.93 "));
                list.Add(string.Format("2.790 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 5 IRC70RW40TBM "));
                list.Add(string.Format("5.0 5.0 5.0 5.0 "));
                list.Add(string.Format("0.795 0.38 0.795 "));
                list.Add(string.Format("2.790 "));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            rtb_LL_TXT.Lines = list.ToArray();

            list.Clear();


            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 3 IRC70RWHEEL  1.179"));
                list.Add(string.Format("LOAD GENERATION 39"));
                list.Add(string.Format("TYPE 3 -18.800 0 1.500 XINC 0.5"));

                rtb_LL_1.Lines = list.ToArray();


                list.Clear();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 2 IRC70RTRACK  1.179"));
                list.Add(string.Format("LOAD GENERATION 39"));
                list.Add(string.Format("TYPE 2 0.0 0 1.500 XINC 0.5"));


                rtb_LL_2.Lines = list.ToArray();


                list.Clear();

                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 4 IRC70RW40TBL  1.179"));
                list.Add(string.Format("LOAD GENERATION 39"));
                list.Add(string.Format("TYPE 4 0.0 0 1.500 XINC 0.5"));

                rtb_LL_3.Lines = list.ToArray();

                list.Clear();

                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 1 IRCCLASSA  1.179"));
                list.Add(string.Format("LOAD GENERATION 39"));
                list.Add(string.Format("TYPE 1 0.0 0 1.500 XINC 0.5"));

                rtb_LL_4.Lines = list.ToArray();
            }
            else
            {
                list.Clear();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 1 HB_45_6  1.0"));
                list.Add(string.Format("LOAD GENERATION 40"));
                list.Add(string.Format("TYPE 1 0.0 0 2.250 XINC 0.5"));

                rtb_LL_1.Lines = list.ToArray();

                list.Clear();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 2 HB_30_6  1.0"));
                list.Add(string.Format("LOAD GENERATION 40"));
                list.Add(string.Format("TYPE 2 -14.600 0 2.250 XINC 0.5"));

                rtb_LL_2.Lines = list.ToArray();

                list.Clear();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 3 HB_25_6  1.0"));
                list.Add(string.Format("LOAD GENERATION 40"));
                list.Add(string.Format("TYPE 3 0.00 0 2.250 XINC 0.5"));


                rtb_LL_3.Lines = list.ToArray();

                list.Clear();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                list.Add(string.Format("TYPE 4 HB_UNIT_6  1.0"));
                list.Add(string.Format("LOAD GENERATION 40"));
                list.Add(string.Format("TYPE 4 0.00 0 2.250 XINC 0.5"));

                rtb_LL_4.Lines = list.ToArray();
            }

        }

        private void btn_drawings_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(user_path, "Drawings"), "SLAB_DRAWING_LSM");
        }
    }


    public class Slab_Bridge_Model
    {
        public int No_of_Spans { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Slab_Thickness { get; set; }
        public List<string> Input_Data_DL { get; set; }
        public List<string> Input_Data_LL { get; set; }

        public Hashtable ht_bot_mems { get; set; }
        public Hashtable ht_coords { get; set; }


        public JointNodeCollection jnBots { get; set; }

        public double LL_70R_Track { get; set; }
        public double LL_70R_Wheel { get; set; }
        public double LL_Class_A { get; set; }
        public double LL_Boggie { get; set; }

        public Slab_Bridge_Model()
        {
            No_of_Spans = 1;
            Width = 16;
            Length = 12;

            Slab_Thickness = 0.90;
            ht_bot_mems = new Hashtable();
            ht_coords = new Hashtable();

            jnBots = new JointNodeCollection();

            LL_70R_Track = 700;
            LL_70R_Wheel = 1000;
            LL_Class_A = 554;
            LL_Boggie = 40;


            Input_Data_DL = Input_Data_LL = new List<string>();
        }


        public List<double> list_X { get; set; }
        public List<double> list_Z { get; set; }

        JointNodeCollection All_Joints_DL { get; set; }
        JointNodeCollection All_Joints_LL { get; set; }

        MemberCollection mbrCols_DL { get; set; }
        MemberCollection mbrCols_LL { get; set; }

        public void Create_Data_Dead_Load()
        {
            #region List Bottom
            List<double> list_bottom = new List<double>();
            //list_bottom(Bottom Slab);
            list_bottom.Add(0);
            list_bottom.Add(0.111083333);
            list_bottom.Add(0.22225);
            list_bottom.Add(0.333333333);
            list_bottom.Add(0.444416667);
            list_bottom.Add(0.555583333);
            list_bottom.Add(0.666666667);
            list_bottom.Add(0.77775);
            list_bottom.Add(0.888916667);
            list_bottom.Add(1);

            #endregion List Bottom

            List<int> arr = new List<int>();

            #region Generate Coordinates
            list_X = new List<double>();


            double _x = 0.0, _y = 0.0;


            for (int i = 0; i < list_bottom.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                _x = Width * list_bottom[i];
                if (!list_X.Contains(_x)) list_X.Add(_x);
            }

            _x = 0.0;


            JointNode jn = new JointNode();


            jnBots = new JointNodeCollection();


            _x = 0.0;
            for (int i = 0; i < No_of_Spans; i++)
            {
                if (i == 0)
                {
                    jn = new JointNode();
                    _x += list_X[0];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
                for (int j = 1; j < list_X.Count; j++)
                {
                    jn = new JointNode();
                    _x = (i * Width) + list_X[j];
                    jn.X = _x;
                    jnBots.Add(jn);
                }
            }

            All_Joints_DL = new JointNodeCollection();
            int count = 1;
            foreach (var item in jnBots)
            {
                item.NodeNo = count++;
                All_Joints_DL.Add(item);
            }

            Member mbr = new Member();

            mbrCols_DL = new MemberCollection();

            count = 1;

            #region Bottom Members
            mbrCols_DL.Clear();
            for (int i = 1; i < jnBots.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = count++;
                mbr.StartNode = jnBots[i - 1];
                mbr.EndNode = jnBots[i];
                mbrCols_DL.Add(mbr);

                arr.Add(mbr.MemberNo);
            }


            ht_bot_mems.Add(1, arr);

            #endregion Bottom Members


            #endregion Generate Coordinates


            List<string> list = new List<string>();

            #region Live Load

            arr = ht_bot_mems[1] as List<int>;

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

            double _dl = 25 * Length * Width / 110;

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
          
            //list.Add(string.Format("LOAD 11 TEMPERATURE GRADIENT RISE"));
            //list.Add(string.Format("JOINT LOAD"));
            ////list.Add(string.Format("101 FX 550.66"));
            ////list.Add(string.Format("110 FX -550.66"));
            ////list.Add(string.Format("101 MZ -68.9"));
            ////list.Add(string.Format("110 MZ 68.9"));


            //list.Add(string.Format("{0} FX 550.66", arr[0]));
            //list.Add(string.Format("{0} FX -550.66", arr[arr.Count - 1]));
            //list.Add(string.Format("{0} MZ -68.9", arr[0]));
            //list.Add(string.Format("{0} MZ 68.9", arr[arr.Count - 1]));


            //list.Add(string.Format("LOAD 12 TEMPERATURE GRADIENT FALL"));
            //list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("{0} FX -287.23", arr[0]));
            //list.Add(string.Format("{0} FX 287.23", arr[arr.Count - 1]));
            //list.Add(string.Format("{0} MZ -9.1", arr[arr.Count - 1]));
            //list.Add(string.Format("{0} MZ 9.1", arr[0]));

            DL_Load_Data = list;
        }


        public void Create_Data_Live_Load()
        {

            #region List Bottom
            List<double> list_bottom = new List<double>();
            //list_bottom(Bottom Slab);
            list_bottom.Add(0);
            list_bottom.Add(0.111083333);
            list_bottom.Add(0.22225);
            list_bottom.Add(0.333333333);
            list_bottom.Add(0.444416667);
            list_bottom.Add(0.555583333);
            list_bottom.Add(0.666666667);
            list_bottom.Add(0.77775);
            list_bottom.Add(0.888916667);
            list_bottom.Add(1);

            #endregion List Bottom

            List<int> arr = new List<int>();

            #region Generate Coordinates
            list_X = new List<double>();
            list_Z = new List<double>();


            double _x = 0.0, _y = 0.0;


            for (int i = 0; i < list_bottom.Count; i++)
            {
                //_x = _x + Width * list_top[j] / org_top;
                //_x = Width * list_bottom[i];
                _x = Length * list_bottom[i];
                if (!list_X.Contains(_x)) list_X.Add(_x);
            }

            _x = 0.0;


            double CL = 2.0;
            list_Z.Add(0.0);
            list_Z.Add(CL);
            list_Z.Add(Width - CL);
            list_Z.Add(Width / 2);
            list_Z.Add(Width);

            list_Z.Sort();










            JointNode jn = new JointNode();


            jnBots = new JointNodeCollection();


            _x = 0.0;



            foreach (var _z in list_Z)
            {
                jnBots = new JointNodeCollection();
                _x = 0.0;
                for (int i = 0; i < No_of_Spans; i++)
                {
                    if (i == 0)
                    {
                        jn = new JointNode();
                        _x += list_X[0];
                        jn.X = _x;
                        jn.Z = _z;
                        jnBots.Add(jn);
                    }
                    for (int j = 1; j < list_X.Count; j++)
                    {
                        jn = new JointNode();
                        _x = (i * Width) + list_X[j];
                        jn.X = _x;
                        jn.Z = _z;
                        jnBots.Add(jn);
                    }
                }
                ht_coords.Add(_z, jnBots);
            }


            All_Joints_LL = new JointNodeCollection();
            int count = 1;

            foreach (var _z in list_Z)
            {
                jnBots = ht_coords[_z] as JointNodeCollection;
                foreach (var item in jnBots)
                {
                    item.NodeNo = count++;


                    All_Joints_LL.Add(item);
                }
            }


            Member mbr = new Member();

            mbrCols_LL = new MemberCollection();

            count = 1;



            #region Bottom Members

            #region Long Members

            foreach (var _z in list_Z)
            {
                jnBots = ht_coords[_z] as JointNodeCollection;
                arr = new List<int>();
                for (int i = 1; i < jnBots.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = count++;
                    mbr.StartNode = jnBots[i - 1];
                    mbr.EndNode = jnBots[i];
                    mbrCols_LL.Add(mbr);
                    arr.Add(mbr.MemberNo);
                }
                ht_bot_mems.Add(_z, arr);
            }


            #region Cross Girders
            for (int c = 1; c < list_Z.Count; c++)
            {

                var m1 = ht_coords[list_Z[c-1]] as JointNodeCollection;
                var m2 = ht_coords[list_Z[c]] as JointNodeCollection;

                for (int i = 0; i < m1.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = count++;
                    mbr.StartNode = m1[i];
                    mbr.EndNode = m2[i];
                    mbrCols_LL.Add(mbr);
                    arr.Add(mbr.MemberNo);
                }
                //ht_bot_mems.Add(_z, arr);
            }
            #endregion Cross Girders


            //arr.Add(mbr.MemberNo);

            #endregion Long Members


            #endregion Bottom Members


            #endregion Generate Coordinates


            List<string> list = new List<string>();

            #region Live Load
          
            #endregion Live Load
        }


        public List<string> DL_Load_Data { get; set; }

        public List<string> LL_Load_Data { get; set; }


        public List<string> LL_TXT_Data { get; set; }


        public void Write_Data(string fileName, bool liveLoad)
        {
            List<string> list = new List<string>();

            list.Add(string.Format("ASTRA SPACE SLAB BRIDGE INPUT DATA FILE"));


            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            for (int i = 0; i < All_Joints_LL.Count; i++)
            {
                list.Add(string.Format("{0}", All_Joints_LL[i].ToString()));
            }

            list.Add(string.Format("MEMBER INCIDENCES"));
            for (int i = 0; i < mbrCols_LL.Count; i++)
            {
                list.Add(string.Format("{0}", mbrCols_LL[i].ToString()));
            }

            list.Add(string.Format("MEMBER PROPERTY"));

            List<int> arr = new List<int>();

            //arr = ht_bot_mems[1] as List<int>;
            //arr = ht_bot_mems[1] as List<int>;


            foreach (var _z in list_Z)
            {
                arr.AddRange((ht_bot_mems[_z] as List<int>).ToArray());
            }


            //list.Add(string.Format("210 TO 212 222 TO 350 PRIS YD 0.5 ZD 1"));
            //list.Add(string.Format("{0} PRIS YD {1:f3} ZD 1", MyList.Get_Array_Text(arr), Slab_Thickness));
            list.Add(string.Format("{0} TO {1} PRIS YD {2:f3} ZD 1", mbrCols_LL[0].MemberNo, mbrCols_LL[mbrCols_LL.Count - 1].MemberNo, Slab_Thickness));
            //list.Add(string.Format("8 TO 10 15 TO 17 PRIS YD 0.3 ZD 1"));



            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 2.17185e+007 ALL"));
            list.Add(string.Format("POISSON 0.17 ALL"));
            list.Add(string.Format("DENSITY 25 ALL"));
            list.Add(string.Format("DAMP 0.05 ALL"));

            arr = new List<int>();
            //arr = ht_bot_mems[1] as List<int>;
            List<int> L_Supp = new List<int>();
            List<int> R_Supp = new List<int>();
            foreach (var _z in list_Z)
            {
                var jnts = ht_coords[_z] as JointNodeCollection;
                L_Supp.Add(jnts[0].NodeNo);
                R_Supp.Add(jnts[jnts.Count - 1].NodeNo);
            }

            list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 5 9 13 169 TO 297 FIXED BUT FX FZ MX MY MZ KFY 1210.3"));
            list.Add(string.Format("{0} FIXED BUT FX MZ", MyList.Get_Array_Text(L_Supp)));
            list.Add(string.Format("{0} PINNED", MyList.Get_Array_Text(R_Supp)));
            //list.Add(string.Format("*1 5 9 13 169 TO 297 FIXED"));

            if (liveLoad)
            {
                if (LL_Load_Data != null)
                {
                    LL_Load_Data.Remove("");
                    list.AddRange(LL_Load_Data.ToArray());
                }
            }
            else
            {
                list.AddRange(DL_Load_Data.ToArray());
            }

            list.Add(string.Format("PERFORM ANALYSIS PRINT ALL"));
            list.Add(string.Format("FINISH"));

            if (liveLoad) Input_Data_LL = list;
            else Input_Data_DL = list;

            File.WriteAllLines(fileName, list.ToArray());

            if(LL_TXT_Data != null && liveLoad)
            {
                File.WriteAllLines(MyList.Get_LL_TXT_File(fileName), LL_TXT_Data.ToArray());

            }


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
    public class SB_Results : List<SB_Table>
    {
        string _flName = "";
        public SB_Results(string fileName)
        {
            _flName = fileName;
            Read_From_Result();
        }
        void Read_From_Result()
        {
            if (!File.Exists(_flName)) return;
            List<string> list = new List<string>(File.ReadAllLines(_flName));

            string kStr = "";


            SB_Table tab = null;
            SB_Rows br = null;

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



                if (kStr.StartsWith("TABLE"))
                {
                    tab = new SB_Table(kStr);
                    this.Add(tab);
                    flag = true;
                    if (Count == 1) i += 6;
                    if (Count == 2) i += 7;
                    continue;
                }

                if (flag)
                {
                    br = SB_Rows.Parse(kStr);
                    if (br != null)
                    {
                        tab.Add(br);
                    }
                }

            }


        }

    }
    public class SB_Table : List<SB_Rows>
    {
        public string Title { get; set; }
        public SB_Table(string title)
            : base()
        {
            Title = title;
        }
    }
    public class SB_Rows : List<string>
    {
        public SB_Rows()
            : base()
        {
        }
        public static SB_Rows Parse(string txt)
        {
            try
            {
                MyList m = new MyList(MyList.RemoveAllSpaces(txt.ToUpper()), ' ');
                SB_Rows bc = new SB_Rows();
                bc.AddRange(m.StringList);
                return bc;
            }
            catch (Exception exx) { }

            return null;

        }
    }

    public class RCC_Slab_Bridge_Analysis
    {

        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;

        public List<BridgeMemberAnalysis> All_LL_Analysis = null;





        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        string input_file, user_path;
        public RCC_Slab_Bridge_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
        }

        #region Properties

        public double Length { get; set; }
        public double Ds { get; set; }



        /// <summary>
        /// width of crash barrier
        /// </summary>
        public double Wc { get; set; }
        public double Wf_left { get; set; }
        public double Wf_right { get; set; }
        /// <summary>
        /// width of footpath
        /// </summary>
        public double Wk_left { get; set; }
        public double Wk_right { get; set; }

        /// <summary>
        /// width of railing
        /// </summary>
        public double Wr { get; set; }
        /// <summary>
        /// Overhang of girder off the bearing [og]
        /// </summary>
        public double og { get; set; }
        /// <summary>
        /// Overhang of slab off the bearing [os]
        /// </summary>
        public double os { get; set; }
        /// <summary>
        /// Expansion Gap [eg]
        /// </summary>
        public double eg { get; set; }
        /// <summary>
        /// Length of varring portion
        /// </summary>
        public double Lvp { get; set; }
        /// <summary>
        /// Length of Solid portion
        /// </summary>
        public double Lsp { get; set; }
        /// <summary>
        /// Effective Length
        /// </summary>
        public double Leff { get; set; }


        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }
        public int Total_Rows
        {
            get
            {
                return 11;
            }
        }
        public int Total_Columns
        {
            get
            {
                return 11;
            }
        }
        public double Skew_Angle { get; set; }
        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }

        public double Spacing_Long_Girder
        {

            get
            {
                //Chiranjit [2013 05 02]
                //return MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / 6.0).ToString("0.000"), 0.0);

                double val = ((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (NMG - 1));
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //chiranji [2013 05 03]
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                double val = (Length - 2 * Effective_Depth) / (NCG - 1);
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Working_Folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }
        #region Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                if (File.Exists(value))
                    user_path = Path.GetDirectoryName(input_file);
            }
        }

        //Chiranjit [2013 09 24]
        public string LL_Analysis_1_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 1");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_1_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_2_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 2");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_2_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_3_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 3");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_3_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_4_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 4");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_4_Input_File.txt");
                }
                return "";
            }
        }
        public string LL_Analysis_5_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 5");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_5_Input_File.txt");
                }
                return "";
            }
        }
        public string LL_Analysis_6_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 6");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_6_Input_File.txt");
                    //return Path.Combine(pd, "LL_Type_6_Input_File.txt");
                }
                return "";
            }
        }


        //Chiranjit [2012 05 27]
        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(Working_Folder)) return "";
                return Path.Combine(Working_Folder, "ASTRA_DATA_FILE.TXT");

            }
        }


        public string Get_Analysis_Report_File(string input_path)
        {

            if (!File.Exists(input_path)) return "";

            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");


        }
        public string Working_Folder
        {
            get
            {
                if (File.Exists(Input_File))
                    return Path.GetDirectoryName(Input_File);
                return "";
            }
        }
        public int NoOfInsideJoints
        {
            get
            {
                return 1;
            }
        }
        #endregion Analysis Input File
        public int NCG { get; set; }
        public int NMG { get; set; }


        #endregion Properties



        //Chiranjit [2013 05 02]
        string support_left_joints = "";
        string support_right_joints = "";

        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();


        //Chiranjit [2011 08 01]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
        public void CreateData()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            //double val1 = 12.1;
            double val1 = WidthBridge;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            //Effective_Depth = Lvp;
            Effective_Depth = Lsp;

            list_x.Clear();
            list_x.Add(0.0);

            list_x.Add(og);

            list_x.Add(Lsp);


            //last_x = (Lsp + Lvp);
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            //last_x = 3 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            //last_x = 5 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = 3 * Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            //last_x = 7 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            int i = 0;
            for (i = list_x.Count - 2; i >= 0; i--)
            {
                last_x = Length - list_x[i];
                list_x.Add(last_x);
            }
            MyList.Array_Format_With(ref list_x, "F3");
            last_x = x_incr + Effective_Depth;

            bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);

            }
            while (last_x <= Length);
            list_x.Sort();


            list_z.Clear();
            list_z.Add(0);

            if (Wc != 0.0)
            {
                last_z = Wc;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);

                last_z = WidthBridge - Wc;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left != 0.0 && Wf_right != 0.0)
            {
                last_z = Wf_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wf_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left != 0.0 && Wf_right == 0.0)
            {
                last_z = Wf_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wk_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left == 0.0 && Wf_right != 0.0)
            {
                last_z = Wk_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wf_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            if (Width_LeftCantilever != 0.0)
            {
                last_z = Width_LeftCantilever;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            if (Width_RightCantilever != 0.0)
            {
                last_z = WidthBridge - Width_RightCantilever;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = Width_LeftCantilever + z_incr;
            do
            {
                flag = false;
                for (i = 0; i < list_z.Count; i++)
                {
                    if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                    list_z.Add(last_z);

                last_z += z_incr;

                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);

            list_z.Sort();
            #endregion Chiranjit [2011 09 23] Correct Create Data



            _Columns = list_x.Count;
            _Rows = list_z.Count;

            //int i = 0;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];


            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    //nd.X = list_x[iCols] + (skew_length * list_z[iRows]);
                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }
            int nodeNo = 0;
            Joints.Clear();

            //support_left_joints = Joints_Array[0, 0].NodeNo + " TO " + Joints_Array[0, iCols - 1].NodeNo;
            //support_right_joints = Joints_Array[iRows - 1, 0].NodeNo + " TO " + Joints_Array[iRows - 1, iCols - 1].NodeNo;

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();





            //string str_joints = "";

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                    if (iCols == 1 && iRows > 1 && iRows < _Rows - 2)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 2 && iRows > 1 && iRows < _Rows - 2)
                        support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else
                    {
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                }
                if (list_nodes.Count > 0)
                {
                    joints_list_for_load.Add(MyList.Get_Array_Text(list_nodes));
                    list_nodes.Clear();
                }
            }

            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();


            #region Chiranjit [2013 05 30]
            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                }
            }
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                }
            }
            #endregion Chiranjit [2013 05 30]


            #region Chiranjit [2013 06 06]

            #endregion Chiranjit [2013 06 06]
        }

        public string _DeckSlab { get; set; }
        public string _Inner_Girder_Mid { get; set; }
        public string _Inner_Girder_Support { get; set; }
        public string _Outer_Girder_Mid { get; set; }
        public string _Outer_Girder_Support { get; set; }
        public string _Cross_Girder_Inter { get; set; }
        public string _Cross_Girder_End { get; set; }

        void Set_Inner_Outer_Cross_Girders()
        {

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();

            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
                    (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    Inner_Girder.Add(MemColls[i].MemberNo);
                }
            }
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();


            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);

        }



        #region Chiranjit [2014 09 02] For British Standard

        public List<int> HA_Lanes;

        public string HA_Loading_Members;
        public void WriteData_Total_Analysis(string file_name, bool is_british)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> DeckSlab = new List<int>();

            List<int> Inner_Girder_Mid = new List<int>();
            List<int> Inner_Girder_Support = new List<int>();

            List<int> Outer_Girder_Mid = new List<int>();
            List<int> Outer_Girder_Support = new List<int>();

            List<int> Cross_Girder_Inter = new List<int>();
            List<int> Cross_Girder_End = new List<int>();


            List<int> HA_Members = new List<int>();

            List<double> HA_Dists = new List<double>();
            HA_Dists = new List<double>();
            if (HA_Lanes != null)
            {
                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
                }
            }

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            int index = 2;

            for (int c = 0; c < _Rows; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (i <= 1 || i >= (_Columns - 3))
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {
                            var item = Long_Girder_Members_Array[c, i];

                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Support.Add(item.MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {

                            var item = Long_Girder_Members_Array[c, i];

                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Mid.Add(item.MemberNo);


                            //Inner_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                }
            }

            Outer_Girder_Mid.Sort();
            Outer_Girder_Support.Sort();


            Inner_Girder_Mid.Sort();
            Inner_Girder_Support.Sort();
            DeckSlab.Sort();
            index = 2;
            List<int> lst_index = new List<int>();
            for (int n = 1; n <= NCG - 2; n++)
            {
                for (i = 0; i < _Columns; i++)
                {
                    if (Cross_Girder_Members_Array[0, i].StartNode.X.ToString("0.00") == (Spacing_Cross_Girder * n + Effective_Depth).ToString("0.00"))
                    {
                        index = i;
                        lst_index.Add(i);
                    }
                }
            }
            for (int c = 0; c < _Rows - 1; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (lst_index.Contains(i))
                        Cross_Girder_Inter.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    else if (i == 1 || i == _Columns - 2)
                    {
                        Cross_Girder_End.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                        DeckSlab.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                }
            }

            DeckSlab.Sort();
            Cross_Girder_Inter.Sort();
            Cross_Girder_End.Sort();


            //_Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            //_Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            //_Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);




            //string _DeckSlab = "";
            //string _Inner_Girder_Mid = "";
            //string _Inner_Girder_Support = "";
            //string _Outer_Girder_Mid = "";
            //string _Outer_Girder_Support = "";
            //string _Cross_Girder_Inter = "";
            //string _Cross_Girder_End = "";




            _DeckSlab = MyList.Get_Array_Text(DeckSlab);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder_Mid);
            _Inner_Girder_Support = MyList.Get_Array_Text(Inner_Girder_Support);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder_Mid);
            _Outer_Girder_Support = MyList.Get_Array_Text(Outer_Girder_Support);
            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder_Inter);
            _Cross_Girder_End = MyList.Get_Array_Text(Cross_Girder_End);



            HA_Loading_Members = MyList.Get_Array_Text(HA_Members);

            list.Add("SECTION PROPERTIES");
            //if (Long_Inner_Mid_Section != null)
            //{
            //    Set_Section_Properties(list);
            //}
            //else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANTS");
            list.Add("E 2.85E6 ALL");
            //list.Add("E " + Ecm * 100 + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");

            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));


            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }

        #endregion Chiranjit [2014 09 02] For British Standard

        public void WriteData_Total_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> DeckSlab = new List<int>();

            List<int> Inner_Girder_Mid = new List<int>();
            List<int> Inner_Girder_Support = new List<int>();

            List<int> Outer_Girder_Mid = new List<int>();
            List<int> Outer_Girder_Support = new List<int>();

            List<int> Cross_Girder_Inter = new List<int>();
            List<int> Cross_Girder_End = new List<int>();






            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            int index = 2;

            for (int c = 0; c < _Rows; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (i <= 1 || i >= (_Columns - 3))
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {
                            Inner_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {
                            Inner_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                }
            }

            Outer_Girder_Mid.Sort();
            Outer_Girder_Support.Sort();


            Inner_Girder_Mid.Sort();
            Inner_Girder_Support.Sort();
            DeckSlab.Sort();
            index = 2;
            List<int> lst_index = new List<int>();
            for (int n = 1; n <= NCG - 2; n++)
            {
                for (i = 0; i < _Columns; i++)
                {
                    if (Cross_Girder_Members_Array[0, i].StartNode.X.ToString("0.00") == (Spacing_Cross_Girder * n + Effective_Depth).ToString("0.00"))
                    {
                        index = i;
                        lst_index.Add(i);
                    }
                }
            }
            for (int c = 0; c < _Rows - 1; c++)
            {
                for (i = 0; i < _Columns; i++)
                {
                    if (lst_index.Contains(i))
                        Cross_Girder_Inter.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    else if (i == 1 || i == _Columns - 2)
                    {
                        Cross_Girder_End.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                        DeckSlab.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                }
            }

            DeckSlab.Sort();
            Cross_Girder_Inter.Sort();
            Cross_Girder_End.Sort();


            //_Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            //_Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            //_Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);




            //string _DeckSlab = "";
            //string _Inner_Girder_Mid = "";
            //string _Inner_Girder_Support = "";
            //string _Outer_Girder_Mid = "";
            //string _Outer_Girder_Support = "";
            //string _Cross_Girder_Inter = "";
            //string _Cross_Girder_End = "";




            _DeckSlab = MyList.Get_Array_Text(DeckSlab);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder_Mid);
            _Inner_Girder_Support = MyList.Get_Array_Text(Inner_Girder_Support);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder_Mid);
            _Outer_Girder_Support = MyList.Get_Array_Text(Outer_Girder_Support);
            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder_Inter);
            _Cross_Girder_End = MyList.Get_Array_Text(Cross_Girder_End);


            list.Add("SECTION PROPERTIES");

            //if (Long_Inner_Mid_Section != null)
            //{
            //    Set_Section_Properties(list);
            //}
            //else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));



            //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }

        public void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
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

            //if (Long_Inner_Mid_Section != null)
            //{
            //    Set_Section_Properties(list);
            //}
            //else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));


            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("1 TO 220 UNI GY -0.0001");

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);

            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");

            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            string fn = Path.GetDirectoryName(file_name);
            fn = Path.Combine(fn, "LL.TXT");
            File.WriteAllLines(fn, ll_data.ToArray());

            list.Clear();
        }

        public void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
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
            //if (Long_Inner_Mid_Section != null)
            //{
            //    Set_Section_Properties(list);
            //}
            //else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));


            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            //list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }

        public void Clear()
        {
            Joints_Array = null;
            Long_Girder_Members_Array = null;
            Cross_Girder_Members_Array = null;
            MemColls.Clear();
            MemColls = null;
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList_1 = new List<LoadData>();
            //LoadList.Clear();
            MyList mlist = null;
            for (i = 0; i < dgv_live_load.RowCount; i++)
            {
                try
                {
                    ld = new LoadData();
                    mlist = new MyList(MyList.RemoveAllSpaces(dgv_live_load[0, i].Value.ToString().ToUpper()), ':');
                    ld.TypeNo = mlist.StringList[0];
                    ld.Code = mlist.StringList[1];
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    for (int j = 0; j < Live_Load_List.Count; j++)
                    {
                        if (Live_Load_List[j].TypeNo == ld.TypeNo)
                        {
                            ld.LoadWidth = Live_Load_List[j].LoadWidth;
                            break;
                        }
                    }
                    if ((ld.Z + ld.LoadWidth) > WidthBridge)
                    {
                        throw new Exception("Width of Bridge Deck is insufficient to accommodate \ngiven numbers of Lanes of Vehicle Load. \n\nBridge Width = " + WidthBridge + " <  Load Width (" + ld.Z + " + " + ld.LoadWidth + ") = " + (ld.Z + ld.LoadWidth));
                    }
                    else
                    {
                        ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                        ld.ImpactFactor = MyList.StringToDouble(dgv_live_load[5, i].Value.ToString(), 0.5);
                        LoadList_1.Add(ld);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #region Set Loads

            List<LoadData> LoadList_tmp = new List<LoadData>(LoadList_1.ToArray());

            LoadList_2 = new List<LoadData>();
            LoadList_3 = new List<LoadData>();
            LoadList_4 = new List<LoadData>();
            LoadList_5 = new List<LoadData>();
            LoadList_6 = new List<LoadData>();

            LoadList_1.Clear();

            //70 R Wheel
            ld = new LoadData(Live_Load_List[2]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_1.Add(ld);


            //1 Lane Class A
            ld = new LoadData(Live_Load_List[0]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_2.Add(ld);



            //2 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);



            //3 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[2].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);


            //1 Lane Class A + 70 RW
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);

            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            #endregion Set Loads
        }
    }
}
