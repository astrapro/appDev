using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;

using AstraInterface.Interface;
using AstraInterface.DataStructure;


using MovingLoadAnalysis;

namespace ASTRAStructures
{
    public partial class frmSteelBeamBoQ : Form
    {
        IApplication iApp;
        StructureMemberAnalysis StructureAnalysis { get; set; }

        public sAdd_Beam_BOQ Add_Beam_BOQ;

        Thread thd;
        ASTRADoc AST_DOC { get; set; }

        public double d1 { get; set; }
        public double d2 { get; set; }
        public double d3 { get; set; }
        public double d4 { get; set; }
        //public double d5 { get; set; }
        //public double d6 { get; set; }

        public SteelBeamDesign beamDes { get; set; }
        public BillOfQuantity BOQ { get; set; }

        //Chiranjit [2015 04 01]
        public TreeView TRV { get; set; }

        public void SetIApplication()
        {
        }
        //public double Stirrup_Bar_Dia { get; set; }

        public List<BeamData> All_Beam_Data { get; set; }

        public frmSteelBeamBoQ(IApplication iapp, ASTRADoc ast_doc, StructureMemberAnalysis staAna)
        {

            InitializeComponent();
            iApp = iapp;

            StructureAnalysis = staAna;
            AST_DOC = ast_doc;
            d1 = 16;
            d2 = 25;
            d3 = 16;
            d4 = 25;
            //d5 = 20;
            //d6 = 25;
            //Stirrup_Bar_Dia = 8;

            uC_SteelSections1.SetIApplication(iApp);
        }

        private void frmSteelBeamBoQ_Load(object sender, EventArgs e)
        {
            if (beamDes != null)
            {
                //d1 = beamDes.Bar_dia1;
                //d2 = beamDes.Bar_dia2;
                //d3 = beamDes.Bar_dia3;
                //d4 = beamDes.Bar_dia4;
                //d5 = beamDes.Bar_dia5;
                //d6 = beamDes.Bar_dia6;
            }

            List<double> list = new List<double>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.Y == item.StartNode.Y)
                {
                    if (!list.Contains(item.StartNode.Y))
                        list.Add(item.StartNode.Y);
                    //if (!cmb_flr_lvl.Items.Contains(item.StartNode.Y.ToString("f3")))
                    //    cmb_flr_lvl.Items.Add(item.StartNode.Y.ToString("f3"));
                }

            }
            list.Sort();
            foreach (var item in list)
            {

                if (!cmb_flr_lvl.Items.Contains(item.ToString("f4")))
                    cmb_flr_lvl.Items.Add(item.ToString("f4"));
            }
            if (cmb_flr_lvl.Items.Count > 0)
            {
                cmb_flr_lvl.SelectedIndex = 0;
                Select_Members();
            }
            Load_Beam_Data();

        }

        private void btn_get_beams_Click(object sender, EventArgs e)
        {
            Select_Members();

        }

        private void Select_Members()
        {
            double d = MyStrings.StringToDouble(cmb_flr_lvl.Text, 0.0);
            dgv_beams.Rows.Clear();
            cmb_sele_all.Checked = true;

            //for (int i = 0; i < AST_DOC.Members.Count; i++)
            //{
            //    var item = AST_DOC.Members[i];

            //    if (item.EndNode.Y == d && item.StartNode.Y == d)
            //    {
            //        dgv_beams.Rows.Add(true, item.MemberNo, "", item.Property.YD, item.Property.ZD);
            //    }
            //}

            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();

            for (int c = 0; c < cmb_flr_lvl.Items.Count; c++)
            {
                d = MyStrings.StringToDouble(cmb_flr_lvl.Items[c].ToString(), 0.0);

                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    var item = AST_DOC.Members[i];

                    if (item.EndNode.Y == d && item.StartNode.Y == d)
                    //if (item.EndNode.Y == item.StartNode.Y)
                    {
                        list_mem.Add(item);
                    }
                }
            }
            bool flag = false;

            MovingLoadAnalysis.frm_ProgressBar.On = false;
            MovingLoadAnalysis.frm_ProgressBar.ON("Reading continuous Beam Nos........");


            MemberIncidence mi = null;
            int count = 1;
            double last_Y = 0.0;

            double am1, am2, am3, am4, av1, av2, av3;

            am1 = am2 = am3 = am4 = av1 = av2 = av3 = 0.0;

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            int cnt = 1;
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
                    cont_jcc = new JointCoordinateCollection();
                    list_mem1 = Get_Continuous_Beams(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);


                    #region 
                    
                    var item = list_mem1;
                    

                    var lst_jnt = new List<int>();
                    mi = AST_DOC.Members.Get_Member(item[0]);


                    foreach (var mbr in list_mem1)
	                {
                        mi = AST_DOC.Members.Get_Member(mbr);
                        
                        if(!lst_jnt.Contains(mi.EndNode.NodeNo))
                            lst_jnt.Add(mi.EndNode.NodeNo);
                        if(!lst_jnt.Contains(mi.StartNode.NodeNo))
                            lst_jnt.Add(mi.StartNode.NodeNo);
		 
                    }

                    if (last_Y != mi.EndNode.Y)
                    {
                        count = 1;
                    }

                    //beamDes.Get_All_Forces(cont_jcc, ref am1, ref am2, ref am3, ref am4, ref av1, ref av2, ref av3);


                    am1 = StructureAnalysis.GetJoint_MomentForce(lst_jnt);
                    am2 = StructureAnalysis.GetJoint_ShearForce(lst_jnt);
                    dgv_beams.Rows.Add(cnt++, true, mi.EndNode.Y.ToString("f4"), "B" + (count++),
                        MyStrings.Get_Array_Text(item),
                        //mi.Property.YD, mi.Property.ZD,
                        beamDes.sectionName,
                        
                        beamDes.h,
                        am1,am2,

                         "");
                    last_Y = mi.EndNode.Y;
                    #endregion


                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();


            //MemberIncidence mi = null;
            //int count = 1;
            //double last_Y = 0.0;

            //double am1, am2, am3, am4, av1, av2, av3;

            //am1 = am2 = am3 = am4 = av1 = av2 = av3 = 0.0;


            //for (int i = 0; i < list_conts.Count; i++)
            //{
            //    var item = list_conts[i];

            //    mi = AST_DOC.Members.Get_Member(item[0]);
            //    if (last_Y != mi.EndNode.Y)
            //    {
            //        count = 1;
            //    }


            //    dgv_beams.Rows.Add(i+1, true, mi.EndNode.Y.ToString("f3"), "B" + (count++), 
            //        MyStrings.Get_Array_Text(item), 
            //        mi.Property.YD, mi.Property.ZD, 
            //        d1, d2, d3, d4, d5, d6,
            //        beamDes.Shear_Bar_dia,
            //        am1.ToString("f3"),
            //        am2.ToString("f3"),
            //        am3.ToString("f3"),
            //        am4.ToString("f3"),
            //        av1.ToString("f3"),
            //        av2.ToString("f3"),
            //        av3.ToString("f3"),
            //         "");
            //    last_Y = mi.EndNode.Y;
            //}
        }

        public List<int> Get_Continuous_Beams(MemberIncidence b1, ref JointCoordinateCollection cont_jcc)
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
                    //MovingLoadAnalysis.frm_ProgressBar.OFF();
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
            for (int i = 0; i < dgv_beams.RowCount; i++)
            {
                dgv_beams[1, i].Value = cmb_sele_all.Checked;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

            //All_Beam_Data = new List<BeamData>();
            //int c = 0;
            //for (int i = 0; i < dgv_beams.RowCount; i++)
            //{
            //    try
            //    {
            //        c = 1;
            //        BeamData bd = new BeamData();
            //        bd.IsSelect = (bool)dgv_beams[c++, i].Value;
            //        if (bd.IsSelect)
            //        {
            //            c++;
            //            bd.BeamNos = dgv_beams[c++, i].Value.ToString();
            //            bd.Continuous_BeamMembers = dgv_beams[c++, i].Value.ToString();
            //            bd.Breadth = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.Depth = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d1 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d2 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d3 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d4 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d5 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d6 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            All_Beam_Data.Add(bd);
            //        }
                
            //    }
            //    catch (Exception exx) { }
            //}


            //this.DialogResult = DialogResult.OK;
            //this.Close();



            thd = new Thread(new ThreadStart(RunThread));
            thd.Start();
            thd.Join();
            Save_Data();

            MessageBox.Show(this, "Report file created in file " + beamDes.Report_File);
            frmASTRAReport.OpenReport(beamDes.Report_File, this);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Save_Data();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmb_flr_lvl_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_beams.Rows.Clear();
        }

        #region Thread Functions


        public List<string> Design_Summary { get; set; }
        public void RunThread()
        {
            Design_Summary = new List<string>();
            List<string> BOQ_Summary = new List<string>();

            List<string> list = new List<string>();

            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*       DESIGN OF RCC FLANGED BEAM           *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add("");
            list.Add("");

            #endregion


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(Design Standard : BS 8110 / IS 456)"));
            list.Add(string.Format(""));

            int c = 0;
            int step = 1;

            MovingLoadAnalysis.frm_ProgressBar.ON("Member Analysis....");
            for (int i = 0; i < dgv_beams.RowCount; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, dgv_beams.RowCount);
                try
                {
                    c = 1;
                    bool IsSelect = (bool)dgv_beams[c++, i].Value;
                    if (IsSelect)
                    {
                        Set_Grid_Color(i);

                        //beamDes.Floor_Level = MyStrings.StringToDouble(dgv_beams[2, i].Value.ToString(), 0.0);
                        beamDes.Floor_Level = MyStrings.StringToDouble(dgv_beams[2, i].Value.ToString(), 0.0);

                        beamDes.Beam_Nos = dgv_beams[3, i].Value.ToString();
                        beamDes.sectionName = dgv_beams[5, i].Value.ToString();

                        var dta = uC_SteelSections1.Get_BeamSection(beamDes.sectionName);

                        var lst = dta.Data;

                        beamDes.a = (dta.Area * 100);

                        //beamDes.h = dta.Depth.ToString("f3");
                        beamDes.h = dta.Depth;
                        //beamDes.h1 = dta.h.ToString("f3");

                        beamDes.Ixx = (dta.Ixx * 10000);
                        //beamDes.Iyy = (dta.Iyy * 10000).ToString();
                        //beamDes.rxx = (dta.Rxx * 10).ToString();
                        //beamDes.ryy = (dta.Ryy * 10).ToString();

                        beamDes.Z = (dta.Zxx * 1000);
                        beamDes.tw = dta.WebThickness;
                        //beamDes.tw = dta..ToString("f3");

                        beamDes.w = (dta.Weight / 9.81);
                        //beamDes.Bf = (dta.FlangeWidth).ToString();

                        if (lst.Count > 20)
                        {
                            beamDes.h1 = lst.GetDouble(17);
                            beamDes.h2 = lst.GetDouble(18); 
                        }


                        beamDes.M = MyStrings.StringToDouble(dgv_beams[7, i].Value.ToString(), 0.0);
                        beamDes.V = MyStrings.StringToDouble(dgv_beams[8, i].Value.ToString(), 0.0);


                        //All_Beam_Data.Add(bd);

                        //beamDes.Beam_Title = bd.BeamNos;
                        //beamDes.Beam_Nos = bd.Continuous_BeamMembers;
                        //beamDes.Bar_dia1 = bd.d1;
                        //beamDes.Bar_dia2 = bd.d2;
                        //beamDes.Bar_dia3 = bd.d3;
                        //beamDes.Bar_dia4 = bd.d4;
                        //beamDes.Bar_dia5 = bd.d5;
                        //beamDes.Bar_dia6 = bd.d6;

                        //beamDes.AM1 = bd.AM1;
                        //beamDes.AM2 = bd.AM2;
                        //beamDes.AM3 = bd.AM3;
                        //beamDes.AM4 = bd.AM4;

                        //beamDes.AV1 = bd.AV1;
                        //beamDes.AV2 = bd.AV2;
                        //beamDes.AV3 = bd.AV3;


                        //beamDes.D = bd.Depth * 1000;
                        //beamDes.bw = bd.Breadth * 1000;


                        //if (beamDes.BOQ == null)
                        //    beamDes.BOQ = new Beam_BOQ();

                        //beamDes.BOQ.Section_B = bd.Breadth;
                        //beamDes.BOQ.Section_D = bd.Depth;
                        //beamDes.BOQ.Area = bd.Depth * bd.Breadth;


                        //list.AddRange(beamDes.Design_Program_Loop(step++).ToArray());
                        //list.AddRange(beamDes.Design_Program_Loop(i + 1).ToArray());


                        //Design_Summary.Add(string.Format("-------------------------------------"));
                        //Design_Summary.Add(string.Format("BEAM = {0}, FLOOR LEVEL = {1:f3} M", beamDes.Beam_Title, beamDes.BOQ.Floor_ELevation));
                        //Design_Summary.Add(string.Format("CONTINUOUS MEMBERS = {0}", beamDes.Beam_Nos));
                        //Design_Summary.AddRange(beamDes.Design_Summary.ToArray());
                        //Design_Summary.Add(string.Format("-------------------------------------"));



                        //BOQ_Summary.Add(string.Format("-------------------------------------"));
                        //BOQ_Summary.Add(string.Format("BEAM = {0}, FLOOR LEVEL = {1:f3} M", beamDes.Beam_Title, beamDes.BOQ.Floor_ELevation));
                        //BOQ_Summary.Add(string.Format("CONTINUOUS MEMBERS = {0}", beamDes.Beam_Nos));
                        //BOQ_Summary.AddRange(beamDes.BOQ_Summary.ToArray());
                        //BOQ_Summary.Add(string.Format("-------------------------------------"));






                        //beamDes.Calculate_Program_Loop();


                        beamDes.Calculate_Program();
                       

                        dgv_beams[9, i].Value = beamDes.IS_DESIGN_OK ? "OK" : "NOT OK";
                        Set_Grid_Color(i, true, beamDes.IS_DESIGN_OK);

                        //Add_Beam_BOQ(beamDes.BOQ); 

                    }
                    //else
                    //    Set_Grid_Color(i);

                }
                catch (Exception exx) { }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format("TABLE 1 : PERMISSIBLE_SHEAR_STRESS"));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format(""));

            list.AddRange(Tables.Get_File_Permissible_Shear_Stress().ToArray());


            #region End of Report
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            #endregion End of Report


            //File.WriteAllLines(beamDes.Report_File, list.ToArray());

            //string des_sum = Path.Combine(Path.GetDirectoryName(beamDes.Get_Report_File(1)), "BEAM_DESIGN_SUMMARY.TXT");
            //File.WriteAllLines(des_sum, Design_Summary.ToArray());

            //des_sum = Path.Combine(Path.GetDirectoryName(beamDes.Get_Report_File(1)), "BEAM_BOQ_SUMMARY.TXT");
            //File.WriteAllLines(des_sum, BOQ_Summary.ToArray());


        }

        public void Set_Grid_Color(int row_index, bool isdone, bool Is_Design_OK)
        {
            if (isdone)
            {
                if (Is_Design_OK)
                    dgv_beams.Rows[row_index].DefaultCellStyle.BackColor = Color.LawnGreen;
                else
                    dgv_beams.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCoral;

                if (row_index > 4)
                    dgv_beams.FirstDisplayedScrollingRowIndex = row_index - 4;
                else
                    dgv_beams.FirstDisplayedScrollingRowIndex = row_index;
            }
            else
                dgv_beams.Rows[row_index].DefaultCellStyle.BackColor = Color.Yellow;
        }

        public void Set_Grid_Color(int row_index)
        {
            Set_Grid_Color(row_index, false, false);
        }

        #endregion Thread Functions

        private void dgv_beams_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int step = e.RowIndex + 1;

            if (beamDes.Report_File != "")
            {
                string tmp_file = Path.Combine(Path.GetDirectoryName(beamDes.Report_File), "temp");
                //if (!Directory.Exists(tmp_file))
                //    Directory.CreateDirectory(tmp_file);
                //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
                tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");

                if (File.Exists(tmp_file))
                {
                    frmASTRAReport fap = new frmASTRAReport(tmp_file);
                    fap.Owner = this;
                    fap.ShowDialog();
                }
            }
        }

        private void btn_oprn_report_Click(object sender, EventArgs e)
        {

            int r = -1;
            if (dgv_beams.SelectedCells.Count == 0) return;

            List<int> list_rows = new List<int>();

            int i = 0;
            //for (i = 0; i < dgv_beams.SelectedCells.Count; i++)
            //{
            //    list_rows.Add(dgv_beams.SelectedCells[i].RowIndex);
            //}
            bool fl = false;
            for (i = 0; i < dgv_beams.RowCount; i++)
            {
                fl = (bool)dgv_beams[1, i].Value;
                if(fl)
                list_rows.Add(i);
            }



            list_rows.Sort();
            string f = "";


            List<string> list = new List<string>();

            list.AddRange(BeamDesign.Get_Banner());
            foreach (var item in list_rows)
            {
                r = item;

                //f = beamDes.Get_Report_File(r + 1);
                f = beamDes.Get_Report_File();

                if (File.Exists(f))
                {
                    list.AddRange(File.ReadAllLines(f));
                }
                else
                {
                    MessageBox.Show(this, string.Format("Design is not done for the selected Beam {0}.", dgv_beams[1, r].Value));
                }
            }
            if (list.Count > 0)
            {
                File.WriteAllLines(beamDes.Report_File, list.ToArray());

                f = beamDes.Report_File;

                if (File.Exists(f))
                {
                    frmASTRAReport.OpenReport(f, this);
                }
                else
                {
                    MessageBox.Show(this, "Design is not done for the selected Beam(s).");
                }
            }
        }


        #region Save Data Function
        //Hashtable Beam_Data = new Hashtable();
        public void Save_Data()
        {
            //string fname = beamDes.Get_Report_File(1);
            string fname = "";
            //fname = Path.Combine(Path.GetDirectoryName(fname), "BEAM_DATA.TXT");
            fname = Path.Combine(iApp.WorkingFolder, "STEEL_BEAM_DATA.TXT");

            List<string> list = new List<string>();

            string kStr = "";
            int i = 0;
            for (i = 0; i < dgv_beams.RowCount; i++)
            {
                kStr = "";
                for (int c = 0; c < dgv_beams.ColumnCount; c++)
                {
                    kStr += string.Format("{0}$", dgv_beams[c, i].Value.ToString());
                }
                list.Add(kStr);
            }
            File.WriteAllLines(fname, list.ToArray());
        }

        void Load_Beam_Data()
        {
            //string fname = beamDes.Get_Report_File(1);
            string fname = "";
            //fname = Path.Combine(iApp.WorkingFolder, "STEEL_BEAM_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                int c = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], '$');
                    try
                    {
                        c = 5;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;
                        dgv_beams[c, i].Value = mli.StringList[c]; c++;

                        //Chiranjit
                        //dgv_beams[c, i].Value = mli.StringList[c];
                        c++;
                        //dgv_beams[c, i].Value = mli.StringList[c];
                        c++;
                        //dgv_beams[c, i].Value = mli.StringList[c];
                        c++;
                        //dgv_beams[c, i].Value = mli.StringList[c];
                        c++;
                        //dgv_beams[c, i].Value = mli.StringList[c]; 
                        //c++;
                        ////dgv_beams[c, i].Value = mli.StringList[c]; 
                        //c++;
                        ////dgv_beams[c, i].Value = mli.StringList[c]; 
                        c++;

                        dgv_beams[c, i].Value = mli.StringList[c];

                        if (mli.StringList[c] != "")
                            Set_Grid_Color(i, true, !mli.StringList[c].Contains("NOT"));

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

            if (btn.Name == btn_ana_input_data.Name)
            {
                //thd = new Thread(new ThreadStart(Update_Data));
                //thd.Start();
                //thd.Join();
                Update_Data();
                Save_Data();
            }
            else if (btn.Name == btn_Modify.Name)
            {
                frm_Modify_Beam_BOQ fmboq = new frm_Modify_Beam_BOQ();
                fmboq.DGV_INPUTS = dgv_beams;
                fmboq.ShowDialog();
            }
        }

        private void Update_Data()
        {
            MovingLoadAnalysis.frm_ProgressBar.ON("Update Analysis Input Data....");
            for (int i = 0; i < dgv_beams.RowCount; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, dgv_beams.RowCount);
                //Set_Grid_Color(i);
                //Update_Data(string.Format("{0} PRISMATIC YD {1} ZD {2}", dgv_beams[4, i].Value
                //    , dgv_beams[5, i].Value
                //    , dgv_beams[6, i].Value));
                //Set_Grid_Color(i);



                var dta = uC_SteelSections1.Get_Section(dgv_beams[5, i].Value.ToString()) as AstraInterface.DataStructure.RolledSteelBeamsRow;

                if (dta != null)
                {

                    Update_Data(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", dgv_beams[4, i].Value
                        //, dgv_columns[3, i].Value
                        //, dgv_columns[4, i].Value
                        , dta.Area / 10000
                        , dta.Ixx / 100000000
                        , dta.Iyy / 100000000
                        , (dta.Ixx + dta.Iyy) / 100000000
                        ));
                }
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
        }

        private void dgv_beams_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string secName = dgv_beams[5, e.RowIndex].Value.ToString();


            MyStrings ms = new MyStrings(secName, ' ');

            uC_SteelSections1.cmb_section_name.SelectedItem = ms.StringList[0];
            uC_SteelSections1.cmb_code1.SelectedItem = ms.StringList[1];

        }

        private void btn_beam_data_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_beams.RowCount; i++)
            {
                bool v1 = (bool) dgv_beams[1, i].Value ;

                if(v1)
                {
                    dgv_beams[5, i].Value = uC_SteelSections1.cmb_section_name.Text + " " + uC_SteelSections1.cmb_code1.Text;
                    dgv_beams[6, i].Value = uC_SteelSections1.cmb_code1.Text;
                }
            }
        }


    }
}
