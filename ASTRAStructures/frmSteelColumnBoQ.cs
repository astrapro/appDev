using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;
using AstraInterface.Interface;

namespace ASTRAStructures
{
    public partial class frmSteelColumnBoQ : Form
    {
        IApplication iApp;
        Thread thd;

        public sAdd_Column_BOQ Add_Column_BOQ;


        ASTRADoc AST_DOC { get; set; }
        public StructureMemberAnalysis StructureAnalysis { get; set; }
        public double Main_Bar_Dia { get; set; }
        public double Tie_Bar_Dia { get; set; }

        public SteelColumnDesign col_design { get; set; }


        //Chiranjit [2015 04 01]
        public TreeView TRV { get; set; }


        public List<ColumnData> All_Column_Data { get; set; }

        public frmSteelColumnBoQ(IApplication app, ASTRADoc ast_doc)
        {
            InitializeComponent();
            iApp = app;
            uC_SteelSections1.SetIApplication(iApp);
            AST_DOC = ast_doc;
        }

        private void frmSteelColumnDesign_Load(object sender, EventArgs e)
        {
            Select_Members();
            Load_Column_Data();
            chk_sele_all.Checked = true;
        }


        private void btn_get_beams_Click(object sender, EventArgs e)
        {
            Select_Members();
        }

        private void Select_Members()
        {
            dgv_columns.Rows.Clear();

            chk_sele_all.Checked = true;

            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.X == item.StartNode.X &&
                    item.EndNode.Y != item.StartNode.Y &&
                    item.EndNode.Z == item.StartNode.Z)
                {
                    list_mem.Add(item);
                }
            }

            bool flag = false;


            List<int> lst_jnt = new List<int>();
            List<List<int>> all_jnt = new List<List<int>>();

            MovingLoadAnalysis.frm_ProgressBar.ON("Reading Members......");
            for (int i = 0; i < list_mem.Count; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, list_mem.Count); 
                flag = false;
                foreach (var item in list_conts)
                {
                    if (item.Contains(list_mem[i].MemberNo))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    JointCoordinateCollection cont_jcc = new JointCoordinateCollection();


                    list_mem1 = Get_Continuous_Members(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);

                    lst_jnt = new List<int>();
                    foreach (var item in cont_jcc)
                    {
                        lst_jnt.Add(item.NodeNo);
                    }
                    all_jnt.Add(lst_jnt);
                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();

            
            MemberIncidence mi = null;


            double Pu = 0.0;
            double Mux = 0.0;
            double Muy = 0.0;
            for (int i = 0; i < list_conts.Count; i++)
            {
                var item = list_conts[i];
                mi = AST_DOC.Members.Get_Member(item[0]);

                //Pu = StructureAnalysis.GetJoint_R1_Axial(all_jnt[i]);
                //Mux = StructureAnalysis.GetJoint_M2_Bending(all_jnt[i]);
                //Muy = StructureAnalysis.GetJoint_M3_Bending(all_jnt[i]);


                Pu = StructureAnalysis.GetJoint_R1_Axial(all_jnt[i]);
                //Mux = StructureAnalysis.GetJoint_M2_Bending(all_jnt[i]);
                Mux = StructureAnalysis.GetJoint_ShearForce(all_jnt[i]);
                Muy = StructureAnalysis.GetJoint_M3_Bending(all_jnt[i]);


                dgv_columns.Rows.Add(true, "C" + (i + 1), MyStrings.Get_Array_Text(item), col_design.Section_Name, col_design.h, Main_Bar_Dia, col_design.bar_nos, Tie_Bar_Dia, Pu, Mux, Muy, "");
            }
        }

        public List<int> Get_Continuous_Members(MemberIncidence b1, ref  JointCoordinateCollection cont_jcc)
        {
            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);
            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }



            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }

        private void cmb_sele_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                dgv_columns[0, i].Value = chk_sele_all.Checked;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //All_Column_Data = new List<ColumnData>();
            //int c = 0;
            //for (int i = 0; i < dgv_columns.RowCount; i++)
            //{
            //    try
            //    {
            //        c = 0;
            //        ColumnData cd = new ColumnData();
            //        cd.IsSelect = (bool)dgv_columns[c++, i].Value;
            //        if (cd.IsSelect)
            //        {
            //            cd.ColumnNos = dgv_columns[c++, i].Value.ToString();
            //            cd.Continuous_ColumnMembers = dgv_columns[c++, i].Value.ToString();
            //            cd.Breadth = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Depth = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Main_Bar_dia = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Tie_Bar_Dia = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Pu = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Mux = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Muy = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            All_Column_Data.Add(cd);
            //        }

            //    }
            //    catch (Exception exx) { }
            //}

            //this.DialogResult = DialogResult.OK;
            //this.Close();
            RunThread();
            //thd = new Thread(new ThreadStart(RunThread));
            //thd.Start();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        int lr = 0;

        public int Last_Row
        {
            set
            {
                bool chk = (bool)dgv_columns[0, value].Value;

                if (lr != value)
                {
                    if (chk)
                    {
                        dgv_columns[0, lr].Value = false;
                        lr = value;
                        dgv_columns[0, lr].Value = true;
                    }
                }

            }
            get
            {
                return lr;
            }
        }

        private void dgv_columns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            return;
            bool chk = (bool)dgv_columns[0, e.RowIndex].Value;

            if (chk)
            {
                Last_Row = e.RowIndex;
            }
            //for (int i = 0; i < dgv_columns.RowCount; i++)
            //{
            //    if(i == e.RowIndex) continue;

            //    if ((bool)dgv_columns[0, i].Value)
            //        dgv_columns[0, i].Value = false;
            //}

            dgv_columns.Refresh();

        }

        #region Thread Functions


        public List<string> Design_Summary { get; set; }

        public void RunThread()
        {
            List<string> list = new List<string>();

            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*           DESIGN OF RCC COLUMN             *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add("");

            #endregion

          

            int c = 0;
            int step = 1;


            string ColumnNos = "";

            Design_Summary = new List<string>();
            //int c = 0;
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                try
                {
                    c = 0;
                    //ColumnData cd = new ColumnData();
                    //SteelColumnDesign cd = new SteelColumnDesign(iApp);
                    SteelColumnDesign cd = col_design;


                    //cd.IsSelect = (bool)dgv_columns[c++, i].Value;
                    bool IsSelect = (bool)dgv_columns[c++, i].Value;
                    if (IsSelect)
                    {


                        var item = MyList.Get_Array_Intiger(dgv_columns[2, i].Value.ToString());
                        double l = 0.0;

                        //foreach (var mn in item)
                        //{
                        //    var mi = AST_DOC.Members.Get_Member(mn);
                        //    l += mi.Length;
                        //}

                        Set_Grid_Color(i);

                        foreach (var mn in item)
                        {
                            var mi = AST_DOC.Members.Get_Member(mn);
                            l += mi.Length;
                        }

                        #region User Input

                        //cd.AST_DOC = AST_DOC;

                        //if (AST_DOC_ORG != null)
                        //    cd.AST_DOC = AST_DOC_ORG;
                        //else
                        //    cd.AST_DOC = AST_DOC;

                        //cd.l = MyList.StringToDouble(txt_steel_column_l.Text, 0.0);
                        cd.ColumnNo = dgv_columns[1, i].Value.ToString();
                        cd.l = l;
                        cd.a = MyList.StringToDouble(txt_steel_column_a.Text, 0.0);


                        //Pu = StructureAnalysis.GetJoint_R1_Axial(item);
                        //Mux = StructureAnalysis.GetJoint_M2_Bending(all_jnt[i]);
                        //Mux = StructureAnalysis.GetJoint_ShearForce(all_jnt[i]);
                        //Muy = StructureAnalysis.GetJoint_M3_Bending(all_jnt[i]);

                        //cd.P = StructureAnalysis.GetJoint_R1_Axial(item);
                        //cd.M = StructureAnalysis.GetJoint_M3_Bending(item);
                        //cd.V = StructureAnalysis.GetJoint_ShearForce(item);


                        cd.P = MyList.StringToDouble(dgv_columns[8, i].Value.ToString(), 0.0);
                        cd.M = MyList.StringToDouble(dgv_columns[9, i].Value.ToString(), 0.0);
                        cd.V = MyList.StringToDouble(dgv_columns[10, i].Value.ToString(), 0.0);


                        cd.e = MyList.StringToDouble(txt_steel_column_e.Text, 0.0);
                        cd.Pms = MyList.StringToDouble(txt_steel_column_Pms.Text, 0.0);
                        cd.fy = MyList.StringToDouble(txt_steel_column_fy.Text, 0.0);
                        cd.fs = MyList.StringToDouble(txt_steel_column_fs.Text, 0.0);
                        cd.fb = MyList.StringToDouble(txt_steel_column_fb.Text, 0.0);
                        cd.Pcs = MyList.StringToDouble(txt_steel_column_Pcs.Text, 0.0);
                        cd.Ps = MyList.StringToDouble(txt_steel_column_Ps.Text, 0.0);
                        cd.n = MyList.StringToDouble(txt_steel_column_n.Text, 0.0);
                        cd.tb = MyList.StringToDouble(txt_steel_column_tb.Text, 0.0);
                        cd.Dr = MyList.StringToDouble(txt_steel_column_Dr.Text, 0.0);
                        cd.Nr = MyList.StringToDouble(txt_steel_column_Nr.Text, 0.0);


                        cd.Section_Name = dgv_columns[3, i].Value.ToString();

                        var dta = uC_SteelSections1.Get_BeamSection(cd.Section_Name);

                        //cd.A = MyList.StringToDouble(uC_SteelSections1.txt_a.Text, 0.0);
                        //cd.h = MyList.StringToDouble(uC_SteelSections1.txt_h.Text, 0.0);
                        //cd.Bf = MyList.StringToDouble(uC_SteelSections1.txt_Bf.Text, 0.0);
                        //cd.tw = MyList.StringToDouble(uC_SteelSections1.txt_tw.Text, 0.0);
                        //cd.Ixx = MyList.StringToDouble(uC_SteelSections1.txt_Ixx.Text, 0.0);
                        //cd.Iyy = MyList.StringToDouble(uC_SteelSections1.txt_Iyy.Text, 0.0);
                        //cd.rxx = MyList.StringToDouble(uC_SteelSections1.txt_rxx.Text, 0.0);
                        //cd.ryy = MyList.StringToDouble(uC_SteelSections1.txt_ryy.Text, 0.0);



                        cd.A = dta.Area * 100;
                        cd.h = dta.Depth;
                        cd.Bf = dta.FlangeWidth;
                        cd.tw = dta.WebThickness;
                        cd.Ixx = dta.Ixx * 10000;
                        cd.Iyy = dta.Iyy * 10000;
                        cd.rxx = dta.Rxx * 10;
                        cd.ryy = dta.Ryy * 10;



                        //txt_a.Text = (dta.Area * 100).ToString();

                        ////txt_h.Text = dta.Depth.ToString("f3");
                        //txt_h.Text = dta.Depth.ToString();
                        ////txt_h1.Text = dta.h.ToString("f3");

                        //txt_Ixx.Text = (dta.Ixx * 10000).ToString();
                        //txt_Iyy.Text = (dta.Iyy * 10000).ToString();
                        //txt_rxx.Text = (dta.Rxx * 10).ToString();
                        //txt_ryy.Text = (dta.Ryy * 10).ToString();

                        //txt_Z.Text = (dta.Zxx * 1000).ToString();
                        //txt_tw.Text = dta.WebThickness.ToString();
                        ////txt_tw.Text = dta..ToString("f3");

                        //txt_w.Text = (dta.Weight / 9.81).ToString("f3");
                        //txt_Bf.Text = (dta.FlangeWidth).ToString();

                        //if (lst.Count > 20)
                        //{
                        //    txt_h1.Text = lst[17].ToString();
                        //    txt_h2.Text = lst[18].ToString();
                        //}


                        #endregion User Input

                        //cd.Report_File = ;


                        //string rep_file = Path.Combine(Path.GetDirectoryName(AST_DOC.AnalysisFileName), "Structure Design");

                        //if (!Directory.Exists(rep_file))
                        //    Directory.CreateDirectory(rep_file);

                        //rep_file = Path.Combine(rep_file, "Column Design");

                        //if (!Directory.Exists(rep_file))
                        //    Directory.CreateDirectory(rep_file);

                        ////rep_file = Path.Combine(rep_file, "Column_Design_Report.txt");
                        //rep_file = Path.Combine(rep_file, "Column_" +dgv_columns[1, i].Value.ToString() + ".txt");

                        //cd.Report_File = rep_file;

                        cd.Calculate_Program();


                        if (Design_Summary == null)
                            Design_Summary = new List<string>();

                        Design_Summary.Add(string.Format(""));


                        list.Add(string.Format("------------------------------------------------"));
                        list.Add(string.Format("COLUMN = {0}", cd.ColumnNo));
                        list.Add(string.Format("CONTINUOUS MEMBERS = {0}", dgv_columns[2,i].Value.ToString()));
                        list.Add(string.Format("------------------------------------------------"));

                        list.AddRange(cd.Design_Individual_Program().ToArray());

                        //Design_Summary.Add(string.Format("------------------------------------------------"));
                        //Design_Summary.Add(string.Format("COLUMN = {0}", cd.ColumnNos));
                        //Design_Summary.Add(string.Format("CONTINUOUS MEMBERS = {0}", cd.Continuous_ColumnMembers));
                        //Design_Summary.AddRange(col_design.Design_Summary.ToArray());
                        //Design_Summary.Add(string.Format("------------------------------------------------"));
                       
 
                        Set_Grid_Color(i, true, cd.IS_DESIGN_OK);

                        dgv_columns[11, i].Value = cd.IS_DESIGN_OK ? " OK " : "NOT OK";

                        //Add_Column_BOQ(col_design.BOQ);

                    }

                }
                catch (Exception exx) { }
            }



           
            #region End of Report
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            #endregion End of Report

            Save_Data();

            File.WriteAllLines(col_design.Report_File, list.ToArray());


            string des_sum = Path.Combine(Path.GetDirectoryName(col_design.Get_Report_File()), "COLUMN_DESIGN_SUMMARY.TXT");
            File.WriteAllLines(des_sum, Design_Summary.ToArray());



            
            MessageBox.Show(this, "Report file created in file " + col_design.Report_File);

            frmASTRAReport fap = new frmASTRAReport(col_design.Report_File);
            fap.Owner = this;
            fap.ShowDialog();

        }

        public void Set_Grid_Color(int row_index, bool isdone, bool Is_Design_OK)
        {
            if (isdone)
            {
                if (Is_Design_OK)
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LawnGreen;
                else
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else
                dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.Yellow;

            if(row_index > 3 )
                dgv_columns.FirstDisplayedScrollingRowIndex = row_index - 3;
            else
                dgv_columns.FirstDisplayedScrollingRowIndex = 0;
        }

        public void Set_Grid_Color(int row_index)
        {
            Set_Grid_Color(row_index, false, false);
        }

        #endregion Thread Functions

        private void btn_oprn_report_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_columns;

            int r = -1;
            if (dgv.SelectedCells.Count == 0) return;

            List<int> list_rows = new List<int>();

            int i = 0;
            bool fl = false;
            for (i = 0; i < dgv.RowCount; i++)
            {
                fl = (bool)dgv[0, i].Value;
                if (fl)
                    list_rows.Add(i);
            }



            list_rows.Sort();
            string f = "";


            List<string> list = new List<string>();
            //if (list_rows.Count > 0)
            //    list.AddRange(ColumnDesign.Get_Banner());

            foreach (var item in list_rows)
            {
                r = item;
                col_design.ColumnNo = dgv[1, r].Value.ToString();
                f = col_design.Get_Report_File();

                if (File.Exists(f))
                {
                    list.AddRange(File.ReadAllLines(f));
                }
                else
                {
                    MessageBox.Show(this, string.Format("Design is not done for the selected Column {0}.", dgv[1, r].Value));
                    return;
                }
            }
            if (list.Count > 0)
            {
                File.WriteAllLines(col_design.Report_File, list.ToArray());

                f = col_design.Report_File;

                if (File.Exists(f))
                {
                    frmASTRAReport.OpenReport(f, this);
                }
                else
                {
                    MessageBox.Show(this, string.Format("Design is not done for the selected Column(s).", dgv[1, r].Value));
                }
            }
        }

        #region Save Data Function
        //Hashtable Beam_Data = new Hashtable();
        public void Save_Data()
        {
            //string fname = col_design.Get_Report_File(1);
            string fname = col_design.Get_Report_File();

            fname = AST_DOC.AnalysisFileName ;

            fname = Path.Combine(Path.GetDirectoryName(fname), "STEEL_COLUMN_DATA.TXT");

            List<string> list = new List<string>();

            string kStr = "";
            int i = 0;
            for (i = 0; i < dgv_columns.RowCount; i++)
            {
                kStr = "";
                for (int c = 0; c < dgv_columns.ColumnCount; c++)
                {
                    if (dgv_columns[c, i].Value == null)
                        kStr += string.Format("{0}$", "");
                    else
                        kStr += string.Format("{0}$", dgv_columns[c, i].Value.ToString());
                }
                list.Add(kStr);
            }

            File.WriteAllLines(fname, list.ToArray());
        }

        void Load_Column_Data()
        {
            string fname = col_design.Get_Report_File();
            if (fname == "") return;
            fname = Path.Combine(Path.GetDirectoryName(fname), "STEEL_COLUMN_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                int c = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], '$');
                    try
                    {
                        c = 3;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        c++;
                        dgv_columns[c, i].Value = mli.StringList[c];

                        if (mli.StringList[c] != "") Set_Grid_Color(i, true, !mli.StringList[c].Contains("NOT"));

                    }
                    catch (Exception exc)
                    {

                    }
                }
            }

        }
        #endregion Save Data Function

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;

            if (btn.Name == btn_update_ana_input.Name)
            {
                //thd = new Thread(new ThreadStart(Update_Data));
                //thd.Start();
                //thd.Join();
                Update_Data();
            }
            else if (btn.Name == btn_Modify.Name)
            {
                frm_Modify_Column_BOQ fmboq = new frm_Modify_Column_BOQ();
                fmboq.DGV_INPUTS = dgv_columns;
                fmboq.ShowDialog();
            }
        }

        private void Update_Data()
        {
            MovingLoadAnalysis.frm_ProgressBar.ON("Update Analysis Input Data....");
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, dgv_columns.RowCount);
                //Set_Grid_Color(i);

                var dta = uC_SteelSections1.Get_Section(dgv_columns[3, i].Value.ToString()) as AstraInterface.DataStructure.RolledSteelBeamsRow;

                if (dta != null)
                {

                    Update_Data(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", dgv_columns[2, i].Value
                        //, dgv_columns[3, i].Value
                        //, dgv_columns[4, i].Value
                        , dta.Area / 10000
                        , dta.Ixx / 100000000
                        , dta.Iyy / 100000000
                        , (dta.Ixx + dta.Iyy) / 100000000
                        ));
                }
                //Set_Grid_Color(i);

            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();
            MessageBox.Show("Data Sucessfully Updated.", "ASTRA", MessageBoxButtons.OK);

        }
        public void Update_Data(string UpdText)
        {
            if (TRV == null) return;
            List<List<int>> list1 = new List<List<int>>();

            List<int> mems = new List<int>();
            List<int> Updmems = new List<int>();

            //string UpdText = "1 TO 10 PR YD 0.410 ZD 0.32";




            MyStrings mmls = new MyStrings(UpdText, ' ');

            int intx = 0;
            string kStr = "";
            string PR = "";
            for (intx = 0; intx < mmls.Count; intx++)
            {
                if (mmls.StringList[intx].StartsWith("PR"))
                {
                    kStr = mmls.GetString(0, intx - 1);
                    PR = mmls.GetString(intx);
                    break;
                }
            }
            Updmems = MyStrings.Get_Array_Intiger(kStr);
            string PR_Upd = PR;


            for (int i = 0; i < TRV.Nodes.Count; i++)
            {
                //mems = MyStrings.Get_Array_Intiger(TRV.Nodes[i].Text);

                mmls = new MyStrings(TRV.Nodes[i].Text, ' ');

                intx = 0;
                kStr = "";
                PR = "";
                for (intx = 0; intx < mmls.Count; intx++)
                {
                    if (mmls.StringList[intx].StartsWith("PR"))
                    {
                        kStr = mmls.GetString(0, intx - 1);
                        PR = mmls.GetString(intx);
                        break;
                    }
                }
                mems = MyStrings.Get_Array_Intiger(kStr);


                foreach (var item in Updmems)
                {
                    if (mems.Contains(item)) mems.Remove(item);
                }
                if (mems.Count > 0)
                    TRV.Nodes[i].Text = MyStrings.Get_Array_Text(mems) + " " + PR;
                else
                {
                    TRV.Nodes.RemoveAt(i);
                    i--;
                }
                list1.Add(mems);
            }
            TRV.Nodes.Add(UpdText);

            //string kStr = "";

        }

        private void dgv_columns_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            string secName = dgv_columns[3, e.RowIndex].Value.ToString();


            MyStrings ms = new MyStrings(secName, ' ');
            if (ms.Count > 1)
            {
                uC_SteelSections1.cmb_section_name.SelectedItem = ms.StringList[0];
                uC_SteelSections1.cmb_code1.SelectedItem = ms.StringList[1];
            }

        }

        private void uC_SteelSections1_Load(object sender, EventArgs e)
        {

        }

        private void btn_column_update_data_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                bool v1 = (bool)dgv_columns[0, i].Value;

                if (v1)
                {
                    dgv_columns[3, i].Value = uC_SteelSections1.cmb_section_name.Text + " " + uC_SteelSections1.cmb_code1.Text;
                    dgv_columns[4, i].Value = uC_SteelSections1.cmb_code1.Text;
                }
            }
        }

    }

}
