using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.PSC_I_Girder;
using BridgeAnalysisDesign.PSC_BoxGirder;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign;

using LimitStateMethod.Composite;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;





namespace LimitStateMethod.PSC_Box_Girder
{
    public partial class frm_PSC_Box_Girder_AASHTO : Form
    {
        //const string Title = "ANALYSIS OF PSC BOX GIRDER BRIDGE";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "PSC BOX GIRDER BRIDGE [LRFD]";
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC BOX GIRDER BRIDGE [BS]";
                return "PSC BOX GIRDER BRIDGE WORKING STRESS [IRC]";
            }
        }

        PSC_BoxGirderAnalysis Deck_Analysis_DL = null;
        PSC_BoxGirderAnalysis Deck_Analysis_LL = null;


        public PSC_BoxGirderAnalysis Bridge_Analysis
        {
            get
            {
                return Deck_Analysis_LL;
            }
        }

        public List<BridgeMemberAnalysis> All_Analysis { get; set; }

        PSC_Box_Segmental_Girder Segment_Girder = null;

        PostTensionLongGirder LongGirder = null;

        PSC_Box_Section_Data PSC_SECIONS;

        Save_FormRecord SaveRec = new Save_FormRecord();


        PSC_Box_Forces Box_Forces = null;

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        public double L { get { return MyList.StringToDouble(txt_Ana_Span.Text, 13.0); } set { txt_Ana_Span.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_Road_Width.Text, 0.0); } set { txt_Ana_Road_Width.Text = value.ToString("f3"); } }

        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
            //B = abut_width;
        }

        //Chiranjit [2013 06 17]
        public string Total_DeadLoad_Reaction
        {
            get
            {
                return txt_dead_kN_m.Text;
            }
            set
            {
                txt_dead_kN_m.Text = value;
            }
        }
        public string Total_LiveLoad_Reaction
        {
            get
            {
                return txt_live_kN_m.Text;
            }
            set
            {
                txt_live_kN_m.Text = value;
            }
        }
        void frm_ViewForces_Load()
        {
            try
            {
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {

            dgv_left_end_design_forces.Rows.Clear();
            dgv_right_end_design_forces.Rows.Clear();

            SupportReaction sr = null;
            MyList mlist = new MyList(MyList.RemoveAllSpaces("2 12"), ' ');

            double tot_dead_vert_reac = 0.0;
            double tot_live_vert_reac = 0.0;

            //for (int i = 0; i < mlist.Count; i++)
            //{
            try
            {
                //sr = DL_support_reactions.Get_Data(mlist.GetInt(i));

                List<int> lint = new List<int>();
                lint.Add(2);
                MaxForce mf = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(lint);


                dgv_left_end_design_forces.Rows.Add(mf.NodeNo, Math.Abs(mf.Force).ToString("f3"));
                tot_dead_vert_reac += Math.Abs(mf.Force); ;


                lint.Clear();
                lint.Add(12);
                mf = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(lint);


                dgv_left_end_design_forces.Rows.Add(mf.NodeNo, Math.Abs(mf.Force).ToString("f3"));
                tot_dead_vert_reac += Math.Abs(mf.Force); ;
            }
            catch (Exception ex)
            {
            }

            //}
            mlist = new MyList(MyList.RemoveAllSpaces(Supports), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = LL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_right_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));
                    tot_live_vert_reac += Math.Abs(sr.Max_Reaction);
                }
                catch (Exception ex)
                {
                }
            }
            txt_dead_vert_reac_ton.Text = (tot_dead_vert_reac).ToString("f3");
            txt_live_vert_rec_Ton.Text = (tot_live_vert_reac).ToString("f3");

            txt_ana_TSRP.Text = (tot_dead_vert_reac + tot_live_vert_reac).ToString("f3");

        }


        private void txt_dead_vert_reac_Ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            //if (txt.Name == txt_dead_vert_reac_Ton.Name)
            //{
            Text_Changed_Forces();
            //}

        }

        private void Text_Changed_Forces()
        {

            double DL_Factor = MyList.StringToDouble(txt_dl_factor.Text, 1.0);
            double LL_Factor = MyList.StringToDouble(txt_ll_factor.Text, 1.0);

            //lbl_DL_factor.Text = "Factor = " + txt_Ana_DL_Factor.Text;
            //lbl_LL_factor.Text
            lbl_factor.Text = "DL x " + DL_Factor.ToString() + " + LL x " + LL_Factor.ToString();
            if (B != 0)
            {
                //txt_dead_vert_reac_Ton_factor.Text = ((MyList.StringToDouble(txt_dead_vert_reac_Ton.Text, 0.0) * DL_Factor)).ToString("f3");
                txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0)) / B).ToString("f3");


                //txt_live_vert_rec_Ton_factor.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * LL_Factor)).ToString("f3");

                //txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0)) / B).ToString("f3");

                txt_final_vert_reac.Text = (MyList.StringToDouble(txt_dead_kN_m.Text, 0.0) + MyList.StringToDouble(txt_live_kN_m.Text, 0.0)).ToString("f3");
            }


            txt_ana_DLSR.Text = txt_dead_kN_m.Text;

            txt_ana_LLSR.Text = txt_live_kN_m.Text;


            txt_ana_MSLD.Text = txt_final_Mx.Text;
            txt_ana_MSTD.Text = txt_final_Mz.Text;


           
        }
        #endregion View Force

        #region frm_Pier_ViewDesign_Forces
        string analysis_rep = "";
        SupportReactionTable support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        public void frm_Pier_ViewDesign_Forces(string Analysis_Report_file, string left_support, string right_support)
        {
            analysis_rep = Analysis_Report_file;
            Left_support = left_support.Replace(",", " ");
            Right_support = right_support.Replace(",", " ");
        }

        private void frm_ViewDesign_Forces_Load()
        {
            support_reactions = new SupportReactionTable(iApp, analysis_rep);
            try
            {
                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        void Show_and_Save_Data()
        {
            if (!File.Exists(analysis_rep)) return;
            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";
            //List<string> list_arr = new List<string>(File.ReadAllLines(analysis_rep));
            List<string> list_arr = new List<string>();
            list_arr.Add("");
            list_arr.Add("                   =====================================");
            list_arr.Add("                     DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                   =====================================");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add(string.Format(""));
            list_arr.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list_arr.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list_arr.Add(string.Format(format, "   ", "  (Kip)   ", "  (Kip-Ft)", "  (Kip-Ft)"));
            list_arr.Add("");
            SupportReaction sr = null;

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;


            dgv_left_des_frc.Rows.Clear();
            dgv_right_des_frc.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);

                tot_left_vert_reac += Math.Abs(sr.Max_Reaction); ;
                tot_left_Mx += sr.Max_Mx;
                tot_left_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }

            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Kip
            //tot_left_vert_reac /= 10.0;
            //tot_left_Mx /= 10.0;
            //tot_left_Mz /= 10.0;

            txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_des_frc.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz);

                tot_right_vert_reac += Math.Abs(sr.Max_Reaction);
                tot_right_Mx += sr.Max_Mx;
                tot_right_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }
            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Kip
            //tot_right_vert_reac /= 10.0;
            //tot_right_Mx /= 10.0;
            //tot_right_Mz /= 10.0;
            txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");


            //txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Kip");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            //list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Kip" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");


            //list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Kip-Ft" + "  =  " + txt_max_Mx_To.Text + " kN-m");
            txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");

            //list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Kip-Ft" + "  =  " + txt_max_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");




            //File.WriteAllLines(analysis_rep, list_arr.ToArray());
            list_arr.InsertRange(0, File.ReadAllLines(analysis_rep));

            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());


            list_arr.Clear();

            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.AddRange(File.ReadAllLines(Result_Report_DL));
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.AddRange(File.ReadAllLines(Result_Report_LL));

            FILE_SUMMARY_RESULTS = Path.Combine(user_path, "Process\\ANALYSIS RESULT SUMMMARY.TXT");
            File.WriteAllLines(FILE_SUMMARY_RESULTS, list_arr.ToArray());



            Environment.SetEnvironmentVariable("PIER", f_path);
        }
        #endregion frm_Pier_ViewDesign_Forces

        public List<string> Results { get; set; }

        IApplication iApp = null;

        bool IsCreate_Data = true;
        public frm_PSC_Box_Girder_AASHTO(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            Results = new List<string>();

            Segment_Girder = new PSC_Box_Segmental_Girder(iApp);

            Box_Forces = new PSC_Box_Forces();
            All_Analysis = new List<BridgeMemberAnalysis>();
        }

        public string Worksheet_Folder
        {
            get
            {
                if (Path.GetFileName(user_path) == Project_Name)
                {
                    if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                }
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {
                //if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                //    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                //return Path.Combine(user_path, "DRAWINGS");


                if (Path.GetFileName(user_path) == Project_Name)
                {
                    if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                }
                return Path.Combine(user_path, "DRAWINGS");
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

        public string Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    //return Path.Combine(Path.Combine(user_path, "Live Load Analysis"), "Input_Data_LL.txt");
                    return Path.Combine(user_path, "INPUT_DATA.TXT");
                }
                return "";
                //return "";
            }
        }

        public string Input_File_LL
        {
            get
            { 
                string ana_path = "ANALYSIS PROCESS\\Live Load Analysis";
                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != ana_path)
                        if (!Directory.Exists(Path.Combine(user_path, ana_path)))
                            Directory.CreateDirectory(Path.Combine(user_path, ana_path));


                    return Path.Combine(Path.Combine(user_path, ana_path), "Input_Data_LL.txt");
                    //return Path.Combine(user_path, "Input_Data_LL.txt");
                }
                return "";
                //return "";
            }
        }
        public string Input_File_DL
        {
            get
            {
                string ana_path = "ANALYSIS PROCESS\\Dead Load Analysis";

                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != ana_path)
                        if (!Directory.Exists(Path.Combine(user_path, ana_path)))
                            Directory.CreateDirectory(Path.Combine(user_path, ana_path));


                    return Path.Combine(Path.Combine(user_path, ana_path), "Input_Data_DL.txt");
                    //return Path.Combine(user_path, "Input_Data_LL.txt");
                }
                return "";
            }
        }

        public string Analysis_Report_DL
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(Path.GetDirectoryName(Input_File_DL), "ANALYSIS_REP.TXT");
                //return Path.Combine(Path.GetDirectoryName(Input_File_DL), "ANALYSIS_REP_DL.TXT");
                return "";
            }
        }
        public string Analysis_Report_LL
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(Path.GetDirectoryName(Input_File_LL), "ANALYSIS_REP.TXT");
                //return Path.Combine(Path.GetDirectoryName(Input_File_LL), "ANALYSIS_REP_LL.TXT");
                return "";
            }
        }

        #region Deck Analysis Form Events
        private void Default_Input_Data(object sender, EventArgs e)
        {
            Deck_Analysis_DL.Joints = new JointNodeCollection();
            Deck_Analysis_DL.MemColls = new MemberCollection();
            Button_Enable_Disable();
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

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;

                Show_Section_Result();

                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }

                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }


                if (rbtn_multiple_cell.Checked)
                {
                    Section2_Section_Properties();
                }
                Write_All_Data();
                Analysis_Initialize_InputData();

                Create_Data_DL(Input_File_DL);
                Create_Data_LL(Input_File_LL);

                ucPreProcess1.IsFlag = false;
                ucPostProcess1.IsFlag = false;

                cmb_long_open_file_preprocess.Items.Clear();
                cmb_long_open_file_post_process.Items.Clear();
                cmb_long_open_file_analysis.Items.Clear();
               
                for (int i = 0; i < cmb_long_open_file_process.Items.Count; i++)
                {
                    cmb_long_open_file_preprocess.Items.Add(cmb_long_open_file_process.Items[i]);
                    cmb_long_open_file_post_process.Items.Add(cmb_long_open_file_process.Items[i]);
                    cmb_long_open_file_analysis.Items.Add(cmb_long_open_file_process.Items[i]);
                }
                cmb_long_open_file_analysis.SelectedIndex = 0;
                //cmb_long_open_file_post_process.SelectedIndex = 0;
                cmb_long_open_file_preprocess.SelectedIndex = 0;

                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");
                cmb_long_open_file_process.SelectedIndex = 0;
                Button_Enable_Disable();
            }
            catch (Exception ex) { }
        }


        public void LONG_GIRDER_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();

            bool flag = false;
            for (i = 0; i < dgv_long_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_long_liveloads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        //if (long_ll_impact.Contains(kStr) == false)
                        long_ll_impact.Add(kStr);

                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> files = new List<string>();


            List<string> load_list_1 = new List<string>();
            List<string> load_list_2 = new List<string>();
            List<string> load_list_3 = new List<string>();
            List<string> load_list_4 = new List<string>();
            List<string> load_list_5 = new List<string>();
            List<string> load_list_6 = new List<string>();
            List<string> load_total_7 = new List<string>();


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);

            double imp_fact = 1.179;

            int count = 0;
            for (i = 0; i < dgv_long_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_long_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                //txt = string.Format("{0} {1:f3}", long_ll_types[f], imp_fact);
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (!list.Contains(txt))
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

                    string fn = "";
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);

                        fn = fn + " " + def_load[j];
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_long_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }

        private void Create_Data_LL(string file_name)
        {
            //Input_File_LL
            Deck_Analysis_LL.Input_File = file_name;

            LONG_GIRDER_LL_TXT();
            Deck_Analysis_LL.CreateData();
            
            //if (rbtn_HA.Checked)
            //{
            //    //Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);
            //    //Ana_Write_Long_Girder_Load_Data(Deck_Analysis_LL.Input_File, true, false, 1);
            //}
            int i = 0;
            string ll_file = "";
            for (i = 0; i < all_loads.Count; i++)
            {
                ll_file = Get_Live_Load_Analysis_Input_File(i + 1);
                if(rbtn_multiple_cell.Checked)
                    Deck_Analysis_LL.WriteData_LiveLoad(ll_file, aashto_box, long_ll);
                else
                    Deck_Analysis_LL.WriteData_LiveLoad(ll_file, PSC_SECIONS);

                Ana_Write_Long_Girder_Load_Data(ll_file, true, false, (i + 1));
            }

            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            Button_Enable_Disable();
        }


        private void btn_Ana_LL_process_analysis_Click(object sender, EventArgs e)
        {
            string flPath = Deck_Analysis_LL.Input_File;
            string ana_rep_file = "";
            int c = 0;

            Write_All_Data();
            //groupBox25.Visible = true;

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();


            iApp.Progress_Works.Clear();
            for (int i = 0; i < (all_loads.Count + 1); i++)
            {
                if (i == 0)
                    flPath = Deck_Analysis_DL.Input_File;
                else
                    flPath = Get_Live_Load_Analysis_Input_File(i);


                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");
                //flPath = Deck_Analysis_DL.Input_File;
                //pd = new ProcessData();
                //pd.Process_File_Name = flPath;
                //pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                //pcol.Add(pd);
            }

            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }

            Show_Member_Forces_Indian(ana_rep_file);
            Show_and_Save_Data();

            Save_FormRecord.Write_All_Data(this, user_path);
        }

        private void Show_Member_Forces_Indian(string ana_rep_file)
        {

            List<string> Work_List = new List<string>();

            //Work_List.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
            //Work_List.Add("Set Structure Geometry for Total Load Analysis");
            //Work_List.Add("Reading Bending Moment & Shear Force from Total Load Analysis Result");


            //Work_List.Add("Reading Analysis Data from Live Load Analysis Report File");
            //Work_List.Add("Set Structure Geometry for Live Load Analysis");
            //Work_List.Add("Reading Bending Moment & Shear Force from Live Load Analysis Result");



            for (int i = 1; i < (all_loads.Count + 1); i++)
            {
                var flPath = Get_Live_Load_Analysis_Input_File(i);

                Work_List.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

            }

            Work_List.Add("Reading Analysis Data from Dead Load File File");

            iApp.Progress_Works = new ProgressList(Work_List);


            ana_rep_file = File.Exists(Deck_Analysis_LL.Analysis_Report) ? Deck_Analysis_LL.Analysis_Report : Analysis_Report_LL;

            ana_rep_file = MyList.Get_Analysis_Report_File(Get_Live_Load_Analysis_Input_File(1));

            if (File.Exists(ana_rep_file))
            {
                Deck_Analysis_LL.Bridge_Analysis = null;

                for (int i = 1; i < (all_loads.Count + 1); i++)
                {
                    var flPath = Get_Live_Load_Analysis_Input_File(i);
                    ana_rep_file = MyList.Get_Analysis_Report_File(Get_Live_Load_Analysis_Input_File(i));

                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                    Deck_Analysis_LL.All_Analysis.Add(Deck_Analysis_LL.Bridge_Analysis);

                }


            }

            ana_rep_file = File.Exists(Deck_Analysis_DL.Analysis_Report) ? Deck_Analysis_DL.Analysis_Report : Analysis_Report_DL;

            if (File.Exists(ana_rep_file))
            {
                Deck_Analysis_DL.Bridge_Analysis = null;
                Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                //Show_Moment_Shear_DL();
            }

            Show_Moment_Shear_DL();
            Show_Moment_Shear_LL();

            Show_ReactionForces();
            Text_Changed_Forces();


            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            iApp.Save_Form_Record(this, user_path);

            Button_Enable_Disable();


            iApp.Progress_Works.Clear();
        }


        void Show_ReactionForces()
        {
            #region Chiranjit [2012 10 31]

            string s1 = "";
            string s2 = "";
            try
            {
                for (int i = 0; i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count; i++)
                {
                    if (i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count / 2)
                    {
                        if (i == Deck_Analysis_LL.Bridge_Analysis.Supports.Count / 2 - 1)
                        {
                            s1 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s1 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo + ",";
                    }
                    else
                    {
                        if (i == Deck_Analysis_LL.Bridge_Analysis.Supports.Count - 1)
                        {
                            s2 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo;
                        }
                        else
                            s2 += Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo + ",";
                    }
                }
            }
            catch (Exception ex) { }
            //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
            double BB = B;


            frm_ViewForces(BB, Deck_Analysis_DL.Analysis_Report, Deck_Analysis_LL.Bridge_Analysis.Analysis_File, (s1 + " " + s2));
            frm_ViewForces_Load();

            frm_Pier_ViewDesign_Forces(Deck_Analysis_LL.Bridge_Analysis.Analysis_File, s1, s2);
            frm_ViewDesign_Forces_Load();






            //Chiranjit [2012 11 01]
            txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
            txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

            #endregion Chiranjit [2012 10 31]

        }
        private void Write_Ana_Load_Data(bool IsLiveLoad)
        {

            string file_name = (IsLiveLoad) ? Deck_Analysis_LL.Input_File : Deck_Analysis_DL.Input_File;

            if (!File.Exists(file_name)) return;


            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";
            s += (!IsLiveLoad ? " + SIDL " : "");
            //s += (IsLiveLoad ? " + LL " : "");



            List<string> list = new List<string>();
           


            if (!IsLiveLoad)
            {
                load_lst.Add(string.Format("****SELF WEIGHT {0} K/FT*****", Deck_Analysis_DL.Load1));
                load_lst.Add(string.Format("LOAD 1 DEAD LOAD"));
                load_lst.Add(string.Format("MEMBER LOAD "));
                load_lst.Add(string.Format("1 TO {0} UNI GY {1:f3} ",Deck_Analysis_DL.MemColls.Count, Deck_Analysis_DL.Load1));
                load_lst.Add(string.Format("**** SUPERIMPOSED DEAD LOAD {0:f3} K/FT****", Deck_Analysis_DL.Load2));
                load_lst.Add(string.Format("**** FootPath Liveload {0:f3} kip/ft Per Footpat*****", Deck_Analysis_DL.Load3));
                load_lst.Add(string.Format("LOAD 2 SIDL+FPLL"));
                load_lst.Add(string.Format("MEMBER LOAD"));
                load_lst.Add(string.Format("1 TO {0} UNI GY {1:f3}", Deck_Analysis_DL.MemColls.Count, Deck_Analysis_DL.Load2));
                load_lst.Add(string.Format("1 TO {0} UNI GY {1:f3}", Deck_Analysis_DL.MemColls.Count, Deck_Analysis_DL.Load3));

            
                //load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            else
            {
            }
            inp_file_cont.InsertRange(indx, load_lst);

            inp_file_cont.Remove("");

            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;
            btn_Ana_DL_create_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            Button_Enable_Disable();
        }
        private void txt_Ana_custom_LL_Xcrmt_TextChanged(object sender, EventArgs e)
        {
        }

        public void Show_ReadMemberLoad(string file_name, bool IsDeadLoad)
        {

            if (!File.Exists(file_name)) return;


            List<LoadData> lds = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "LL.txt"));

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            bool mov_flag = false;
            bool isMoving_load = false;

            Hashtable ht_impact = new Hashtable();
            bool is_def_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    is_def_load = false;
                    isMoving_load = true;
                    continue;
                }

                if (kStr.Contains("DEFINE MOV"))
                {
                    is_def_load = true;
                    mov_flag = false;
                    //continue;
                }
                if (kStr.StartsWith("PRINT") || kStr.StartsWith("PERFO"))
                {
                    is_def_load = false;
                    mov_flag = false;
                    isMoving_load = false;
                    //continue;
                }
                else if (is_def_load)
                {
                    try
                    {
                        ht_impact.Add(mlist[2], mlist.GetDouble(3));
                    }
                    catch (Exception ex) { }
                }


                if (isMoving_load)
                {
                    try
                    {
                        LoadData ld = LoadData.Parse(kStr);
                        for (int c = 0; c < lds.Count; c++)
                        {
                            if (lds[c].TypeNo == ld.TypeNo)
                            {
                                ld.Code = lds[c].Code;
                                break;
                            }
                        }
                        try
                        {
                            ld.ImpactFactor = (double)ht_impact[ld.Code];
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception ex) { }
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    i++;
                    continue;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                    mov_flag = false;
                }
                if (flag)
                {
                    if (mov_flag)
                    {
                        list_member_load.Add(inp_file_cont[i]);
                    }
                    inp_file_cont.RemoveAt(i);
                    i--;
                }
            }
            //if (IsDeadLoad)
            //    txt_Ana_LL_member_load.Lines = list_member_load.ToArray();


        }

        public void Show_LoadGeneration(string file_name)
        {

            if (!File.Exists(file_name)) return;
            List<LoadData> lds = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "LL.txt"));

            //iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            bool mov_flag = false;
            bool isMoving_load = false;

            Hashtable ht_impact = new Hashtable();
            bool is_def_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    isMoving_load = true;
                    is_def_load = false;
                    continue;
                }


                if (kStr.Contains("DEFINE MOV"))
                {
                    mov_flag = false;
                    is_def_load = true;

                    //continue;
                }
                if (kStr.StartsWith("PRINT") || kStr.StartsWith("PERFO"))
                {
                    is_def_load = false;
                    mov_flag = false;
                    isMoving_load = false;
                    //continue;
                }
                else if (is_def_load)
                {
                    try
                    {
                        ht_impact.Add(mlist[2], mlist.GetDouble(3));
                    }
                    catch (Exception ex) { }
                }

                if (isMoving_load)
                {
                    try
                    {
                        LoadData ld = LoadData.Parse(kStr);
                        for (int c = 0; c < lds.Count; c++)
                        {
                            if (lds[c].TypeNo == ld.TypeNo)
                            {
                                ld.Code = lds[c].Code;
                                break;
                            }
                        }
                        try
                        {
                            ld.ImpactFactor = (double)ht_impact[ld.Code];
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception ex) { }
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    continue;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                    mov_flag = false;
                }
                if (flag)
                {
                    if (mov_flag)
                    {
                        list_member_load.Add(inp_file_cont[i]);
                    }
                    inp_file_cont.RemoveAt(i);
                    i--;
                }
            }
        }
        public string Analysis_Path
        {
            get
            {

                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                    return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return iApp.LastDesignWorkingFolder;
            }
        }

        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                ofd.InitialDirectory = Analysis_Path;
                //ofd.InitialDirectory = user_path;
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    IsCreate_Data = false;
                    //Show_ReadMemberLoad(ofd.FileName);

                    string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                    if (!File.Exists(chk_file)) chk_file = ofd.FileName;
                    //Read_All_Data();
                    Set_Segment_Data();
                    Open_AnalysisFile(chk_file);

                    //Chiranjit [2013 04 26]
                    iApp.Read_Form_Record(this, user_path);

                    Set_Box_Forces();
                    Set_Segment_Data();

                    txt_Ana_analysis_file.Text = chk_file;
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Chiranjit [2012 11 01]
                    //Show_ReactionForces();



                    Button_Enable_Disable();

                    //Chiranjit [2012 12 18]

                    //if (File.Exists(Deck_Analysis_DL.Input_File))
                    //{

                    //    Show_ReadMemberLoad(Deck_Analysis_DL.Input_File, true);
                    //}
                    //if (File.Exists(Deck_Analysis_LL.Input_File))
                    //{

                    //    //Show_ReadMemberLoad(Deck_Analysis_LL.Input_File, false);
                    //    Show_LoadGeneration(Deck_Analysis_LL.Input_File);
                    //}
                    //Show_Analysis_Result();
                }
            }



            //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            //string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            //Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Deck_Analysis_LL.Live_Load_List == null) return;

            //cmb_Ana_DL_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_LL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_DL_load_type.Items.Add(Deck_Analysis_LL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_LL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_DL_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_DL_load_type.SelectedIndex = cmb_Ana_DL_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}




        }

       
        private void btn_Ana_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
        }

        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Deck_Analysis_DL.Input_File))
                iApp.OpenWork(Deck_Analysis_DL.Input_File, true);
        }
      
        #endregion  Composite Analysis Form Events

        #region Deck Methods

        private void Create_Data_DL(string file_name)
        {

            Deck_Analysis_DL.Input_File = file_name;
            Deck_Analysis_DL.CreateData_DeadLoad();


            if (rbtn_multiple_cell.Checked)
            {
                Deck_Analysis_DL.WriteData_DeadLoad(Deck_Analysis_DL.Input_File, aashto_box);
            }
            else
            {
                Deck_Analysis_DL.WriteData_DeadLoad(Deck_Analysis_DL.Input_File, PSC_SECIONS);
            }
            Write_Ana_Load_Data(false);
            Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_DL.Input_File);

            string ll_txt = Deck_Analysis_DL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

           
            Button_Enable_Disable();
        }

        void Analysis_Initialize_InputData()
        {

            Deck_Analysis_DL.Spans.Clear();
            Deck_Analysis_LL.Spans.Clear();

            MyList ml = new MyList(txt_Spans.Text, ',');
            for (int i = 0; i < ml.StringList.Count; i++)
            {
                Deck_Analysis_DL.Spans.Add(ml.GetDouble(i));
                Deck_Analysis_LL.Spans.Add(ml.GetDouble(i));

            }

            Deck_Analysis_DL.Length = MyList.StringToDouble(txt_Ana_Span.Text, 0.0);
            Deck_Analysis_DL.Load1 = MyList.StringToDouble(txt_Ana_SelfWeight.Text, 0.0);
            Deck_Analysis_DL.Load2 = MyList.StringToDouble(txt_Ana_SIDL.Text, 0.0);
            Deck_Analysis_DL.Load3 = MyList.StringToDouble(txt_Ana_FPLL.Text, 0.0);


            Deck_Analysis_DL.WidthBridge = MyList.StringToDouble(txt_Ana_Road_Width.Text, 0.0);
            Deck_Analysis_DL.WidthCantilever = MyList.StringToDouble(txt_Ana_Web_Spacing.Text, 0.0);
            Deck_Analysis_DL.Effective_Depth = MyList.StringToDouble(txt_Ana_Superstructure_depth.Text, 0.0);

            Deck_Analysis_DL.Skew_Angle = 0.0;



            Deck_Analysis_LL.Length = MyList.StringToDouble(txt_Ana_Span.Text, 0.0);

            Deck_Analysis_LL.Load1 = MyList.StringToDouble(txt_Ana_SelfWeight.Text, 0.0);
            Deck_Analysis_LL.Load2 = MyList.StringToDouble(txt_Ana_SIDL.Text, 0.0);
            Deck_Analysis_LL.Load3 = MyList.StringToDouble(txt_Ana_FPLL.Text, 0.0);



            Deck_Analysis_LL.WidthBridge = MyList.StringToDouble(txt_Ana_Road_Width.Text, 0.0);
            Deck_Analysis_LL.WidthCantilever = MyList.StringToDouble(txt_Ana_Web_Spacing.Text, 0.0);
            Deck_Analysis_LL.Effective_Depth = MyList.StringToDouble(txt_Ana_Superstructure_depth.Text, 0.0);
            Deck_Analysis_LL.Skew_Angle = 0.0;

            Deck_Analysis_LL.Start_Support = Start_Support_Text;
            Deck_Analysis_LL.End_Support = END_Support_Text;

            Deck_Analysis_DL.Start_Support = Start_Support_Text;
            Deck_Analysis_DL.End_Support = END_Support_Text;


        }

        void Set_Box_Forces()
        {
            Box_Forces.FRC_LL_Shear[0] = MyList.StringToDouble(txt_Ana_live_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[0] = MyList.StringToDouble(txt_Ana_live_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[1] = MyList.StringToDouble(txt_Ana_live_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[1] = MyList.StringToDouble(txt_Ana_live_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[2] = MyList.StringToDouble(txt_Ana_live_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[2] = MyList.StringToDouble(txt_Ana_live_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_LL_Shear[3] = MyList.StringToDouble(txt_Ana_live_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[3] = MyList.StringToDouble(txt_Ana_live_inner_long_L4_moment.Text, 0.0);



            Box_Forces.FRC_LL_Shear[4] = MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[4] = MyList.StringToDouble(txt_Ana_live_inner_long_3L_8_moment.Text, 0.0);


            Box_Forces.FRC_LL_Shear[5] = MyList.StringToDouble(txt_Ana_live_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_LL_Moment[5] = MyList.StringToDouble(txt_Ana_live_inner_long_L2_moment.Text, 0.0);




            Box_Forces.FRC_DL_Shear[0] = MyList.StringToDouble(txt_Ana_dead_inner_long_support_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[0] = MyList.StringToDouble(txt_Ana_dead_inner_long_support_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[1] = MyList.StringToDouble(txt_Ana_dead_inner_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[1] = MyList.StringToDouble(txt_Ana_dead_inner_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[2] = MyList.StringToDouble(txt_Ana_dead_inner_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[2] = MyList.StringToDouble(txt_Ana_dead_inner_long_L8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[3] = MyList.StringToDouble(txt_Ana_dead_inner_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[3] = MyList.StringToDouble(txt_Ana_dead_inner_long_L4_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[4] = MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[4] = MyList.StringToDouble(txt_Ana_dead_inner_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_DL_Shear[5] = MyList.StringToDouble(txt_Ana_dead_inner_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_DL_Moment[5] = MyList.StringToDouble(txt_Ana_dead_inner_long_L2_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[0] = MyList.StringToDouble(txt_Ana_live_outer_long_support_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[0] = MyList.StringToDouble(txt_Ana_live_outer_long_support_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[1] = MyList.StringToDouble(txt_Ana_live_outer_long_deff_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[1] = MyList.StringToDouble(txt_Ana_live_outer_long_deff_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[2] = MyList.StringToDouble(txt_Ana_live_outer_long_L8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[2] = MyList.StringToDouble(txt_Ana_live_outer_long_L8_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[3] = MyList.StringToDouble(txt_Ana_live_outer_long_L4_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[3] = MyList.StringToDouble(txt_Ana_live_outer_long_L4_moment.Text, 0.0);


            Box_Forces.FRC_SIDL_Shear[4] = MyList.StringToDouble(txt_Ana_live_outer_long_3L_8_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[4] = MyList.StringToDouble(txt_Ana_live_outer_long_3L_8_moment.Text, 0.0);

            Box_Forces.FRC_SIDL_Shear[5] = MyList.StringToDouble(txt_Ana_live_outer_long_L2_shear.Text, 0.0);
            Box_Forces.FRC_SIDL_Moment[5] = MyList.StringToDouble(txt_Ana_live_outer_long_L2_moment.Text, 0.0);


            Box_Forces.Set_Absolute();

        }
        void Show_Moment_Shear_LL()
        {

            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();

            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Bridge_Analysis.Analysis.Members);
            JointNodeCollection jn_col = Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints;


            double L = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length;
            double W = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width;
            double cant_wi = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever;

            //double support = Deck_Analysis_LL.Bridge_Analysis.Supports.Count > 0 ? Deck_Analysis_LL.Bridge_Analysis.Supports[0].X : 0.5;
            double support = Deck_Analysis_LL.Support_Distance;
            support = Deck_Analysis_LL.Support_Distance;


            double eff_d = MyList.StringToDouble(txt_Ana_Superstructure_depth.Text, 0.0);


            double val = L / 2;
            int i = 0;

            List<int> _L2_joints = new List<int>();
            List<int> _L4_joints = new List<int>();
            List<int> _deff_joints = new List<int>();

            List<int> _L8_joints = new List<int>();
            List<int> _3L8_joints = new List<int>();
            List<int> _support_joints = new List<int>();


            for (i = 0; i < Deck_Analysis_LL.Bridge_Analysis.Supports.Count; i++)
            {
                _support_joints.Add(Deck_Analysis_LL.Bridge_Analysis.Supports[i].NodeNo);
                //_support_joints.Add(item.
            }



            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            val = -999;


            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }

            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            eff_d = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth;
            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0;

                eff_d = Deck_Analysis_LL.Effective_Depth;
            }
            //double eff_dep = ;

            //_L_2_joints.Clear();

            cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
            Deck_Analysis_LL.WidthCantilever = cant_wi;



            //eff_d = _X_joints.Count > 1 ? _X_joints[1] : 2.5;
            //eff_d = (eff_d == support ? _X_joints[2] : 2.5);



            if (_X_joints.Contains(eff_d))
            {
                Deck_Analysis_LL.Effective_Depth = eff_d;
            }
            else
            {
                Deck_Analysis_LL.Effective_Depth = _X_joints.Count > 1 ? _X_joints[2] : 0.0; ;
            }

            //_L_2_joints.Clear();
            for (i = 0; i < jn_col.Count; i++)
            {
                if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];



                if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                {
                    _L2_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }



                if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }



                if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }
                if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }


                if ((jn_col[i].X.ToString("0.0") == ((3.0 * L / 8.0) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }
                if ((jn_col[i].X.ToString("0.0") == ((L - (3.0 * L / 8.0)) + x_min).ToString("0.0")))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }

                //if ((jn_col[i].X.ToString("0.0") == ((support) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == ((L - (support)) + x_min).ToString("0.0")))
                //{
                //    _support_joints.Add(jn_col[i].NodeNo);
                //}
            }

            //For Support
            //for (node = 12; node <= 22; node++) _joints.Add(node);
            //for (node = 122; node <= 132; node++) _joints.Add(node);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            if (_support_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[0] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                Box_Forces.FRC_LL_Torsion[0] = force;

                _joints.Clear();
            }

            //For Deff
            //for (node = 111; node <= 121; node++) _joints.Add(node);
            //for (node = 23; node <= 33; node++) _joints.Add(node);
            if (_deff_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[1] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                Box_Forces.FRC_LL_Torsion[1] = force;

                _joints.Clear();
            }

            //For L/8
            //for (node = 100; node <= 110; node++) _joints.Add(node);
            //for (node = 34; node <= 44; node++) _joints.Add(node);
            if (_L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[2] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                Box_Forces.FRC_LL_Torsion[2] = force;

                _joints.Clear();
            }



            //For L/4
            //for (node = 89; node <= 99; node++) _joints.Add(node);
            //for (node = 45; node <= 55; node++) _joints.Add(node);
            if (_L4_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[3] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L4_joints);
                Box_Forces.FRC_LL_Torsion[3] = force;

                _joints.Clear();
            }

            //For 3L/8
            //for (node = 78; node <= 88; node++) _joints.Add(node);
            //for (node = 56; node <= 66; node++) _joints.Add(node);
            if (_3L8_joints.Count > 0)
            {


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_3L8_joints);
                txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[4] = force;

                _joints.Clear();
            }

            //For L/2
            //for (node = 67; node <= 77; node++) _joints.Add(node);
            if (_L2_joints.Count > 0)
            {

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L2_joints);
                txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Kip"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Kip-ft"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }


            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_DL()
        {
            List<int> _joints = new List<int>();
            MaxForce force = new MaxForce();

            //Support
            _joints.Add(2);
            _joints.Add(12);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Dead Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            Results.Add("");
            Results.Add("SELF WEIGHT");
            Results.Add("-----------");
            Results.Add("");
            Results.Add("");
            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[5] = force;



            _joints.Clear();


            Results.Add("");
            Results.Add("");
            Results.Add("SUPER IMPOSED DEAD LOAD");
            Results.Add("-----------------------");
            Results.Add("");
            Results.Add("");
            //Support
            _joints.Add(2);
            _joints.Add(12);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Kip"));
            txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Kip-ft"));
            txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

            //iApp.RunExe(Result_Report_DL);
        }
       
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
            }
        }
        public string Result_Report_LL
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT_LL.TXT");
            }
        }
        public string Result_Report_DL
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT_DL.TXT");
            }
        }
        private void Show_Analysis_Result()
        {

            List<string> list = new List<string>();
            if (File.Exists(Result_Report_DL))
            {
                list.AddRange(File.ReadAllLines(Result_Report_DL));
            }
            if (File.Exists(Result_Report_LL))
            {
                list.AddRange(File.ReadAllLines(Result_Report_LL));
            }

            File.WriteAllLines(Result_Report, list.ToArray());
            //iApp.RunExe(Result_Report);
        }

        void Write_Max_Moment_Shear()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_Ana_DL_inner_long_deff_shear.Text));
            list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_Ana_DL_inner_long_L2_moment.Text));


            list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_Ana_DL_outer_long_deff_shear.Text));
            list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_Ana_DL_outer_long_L2_moment.Text));

            string f_path = Path.Combine(user_path, "FORCES.TXT");
            File.WriteAllLines(f_path, list.ToArray());
            Environment.SetEnvironmentVariable("TBEAM_ANALYSIS", f_path);
            list = null;
        }
        public void Button_Enable_Disable()
        {
            btn_Ana_DL_create_data.Enabled = true;
            //btn_Process_LL_Analysis.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
        }
        public void Open_AnalysisFile(string file_name)
        {
            string analysis_file = file_name;
            bool flag = false;


            if (!File.Exists(analysis_file)) return;
            user_path = Path.GetDirectoryName(file_name);

            string wrkg_folder = Path.GetDirectoryName(analysis_file);
            string dl_file, ll_file;

            if (Path.GetFileName(wrkg_folder) == "Dead Load Analysis")
            {
                user_path = Path.GetDirectoryName(user_path);

                Deck_Analysis_DL.Input_File = analysis_file;
                dl_file = Deck_Analysis_DL.Input_File;
                //dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");

                ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");
                Deck_Analysis_LL.Input_File = ll_file;
                //ll_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                //ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ll_file);

                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }
            }
            else if (Path.GetFileName(wrkg_folder) == "Live Load Analysis")
            {
                ll_file = analysis_file;

                Deck_Analysis_LL.Input_File = analysis_file;
                ll_file = analysis_file;
                //ll_file = File.Exists(ll_file) ? ll_file : Deck_Analysis_LL.Input_File;
                if (File.Exists(ll_file))
                {
                    flag = true;
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

                wrkg_folder = Path.GetDirectoryName(wrkg_folder);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");

                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                Deck_Analysis_DL.Input_File = dl_file;
                //dl_file = Path.Combine(wrkg_folder, "ANALYSIS_REP.txt");
                //dl_file = File.Exists(dl_file) ? dl_file : Deck_Analysis_DL.Input_File;
                if (File.Exists(dl_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                    //Show_ReadMemberLoad(analysis_file);
                }
            }
            else
            {
                wrkg_folder = Path.GetDirectoryName(analysis_file);
                wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");


                dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                Deck_Analysis_DL.Input_File = dl_file;


                if (File.Exists(dl_file))
                {
                    flag = true;
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                    Show_Moment_Shear_DL();
                }

                wrkg_folder = Path.GetDirectoryName(analysis_file);
                wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");



                ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");

                if (!File.Exists(ll_file))
                    ll_file = Get_Live_Load_Analysis_Input_File(1);

                Deck_Analysis_LL.Input_File = ll_file;



                if (File.Exists(ll_file))
                {
                    Deck_Analysis_LL.Bridge_Analysis = null;
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                    Show_Moment_Shear_LL();
                }

            }

            if (File.Exists(analysis_file))
            {
                //btn_Ana_DL_view_structure.Enabled = true;

                try
                {

                    if (flag)
                    {
                        if (Deck_Analysis_LL.Bridge_Analysis != null)
                        {
                            if (Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints.Count > 1)
                            {
                                Deck_Analysis_LL.Skew_Angle = (int)((180.0 / Math.PI) * Math.Atan((Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].X / Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints[1].Z)));
                                txt_Ana_Web_Thickness.Text = Deck_Analysis_LL.Skew_Angle.ToString();
                            }
                        }

                        txt_Ana_Span.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_Road_Width.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_Ana_Web_Spacing.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();
                        txt_Ana_Superstructure_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();

                        txt_Ana_Bridge_Width.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_Superstructure_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_Web_Spacing.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        txt_Ana_Web_Thickness.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Skew_Angle.ToString();

                        txt_gd_np.Text = (Deck_Analysis_LL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                    else
                    {
                        txt_Ana_Span.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_Road_Width.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_gd_np.Text = (Deck_Analysis_DL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");

                        txt_Ana_Bridge_Width.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_Superstructure_depth.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_Web_Spacing.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            string ll_txt = Path.Combine(user_path, "LL.txt");
            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);
            if (Deck_Analysis_DL.Live_Load_List == null) return;
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT kN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (true)
            {
                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                foreach (LoadData ld in Deck_Analysis_LL.LoadList)
                {
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }
                foreach (LoadData ld in Deck_Analysis_LL.LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }
            }
            if (calc_width > Deck_Analysis_DL.WidthBridge)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Deck_Analysis_DL.WidthBridge;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }

        #endregion Composite Methods


        private void frm_PSC_Box_Load(object sender, EventArgs e)
        {

            cmb_long_open_file_process.Items.Clear();
            cmb_long_open_file_process.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
            //cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
            //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
            //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
            cmb_long_open_file_process.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
            cmb_long_open_file_process.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
            //cmb_long_open_file_process.Items.Add(string.Format("ANALYSIS RESULTS"));


            Deck_Analysis_DL = new PSC_BoxGirderAnalysis(iApp);
            Deck_Analysis_LL = new PSC_BoxGirderAnalysis(iApp);
            LongGirder = new PostTensionLongGirder(iApp);

            #region Initialise default input data


            //AASHTO_Design.Input_Deck_Data(dgv_deck_input_data);
            AASHTO_Design_PC_Box_Girder.Input_PSC_Box_Girder_Data(dgv_steel_girder_input_data);
            //AASHTO_Design_PC_Box_Girder.Input_Bolted_Splice_Data(dgv_bolted_field_splice_input_data);
            //AASHTO_Design_PC_Box_Girder.Input_Misc_Steel_Data(dgv_misc_steel_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Abutment_Data(dgv_abutment_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Pier_Data(dgv_pier_input_data);
            //AASHTO_Design_PC_Box_Girder.Input_Foundation_Data(dgv_foundation_input_data);
            AASHTO_Design_PC_Box_Girder.Input_Bearing_Data(dgv_bearing_input_data);


            #endregion Initialise default input data


            Section2_Section_Properties();

            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Moving_Type_LoadData(dgv_long_loads);

            Button_Enable_Disable();
            Load_Tab2_Tab3_Box_Segment_Data();
            Update_Tab3_Data();

            Set_Project_Name();


        }


        private void Open_Project()
        {

            #region Select Design Option

            try
            {
                IsCreate_Data = false;


                string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                Set_Segment_Data();
                Open_AnalysisFile(chk_file);

                IsRead = true;
                iApp.Read_Form_Record(this, user_path);
                IsRead = false;

                Set_Box_Forces();
                Set_Segment_Data();

                txt_Ana_analysis_file.Text = chk_file;

                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Button_Enable_Disable();
                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option

        }

        private void btn_Stressing_Graph_Click(object sender, EventArgs e)
        {
            try
            {
                string excel_path = Path.Combine(Application.StartupPath, @"DESIGN\Pre Stressed\Stressing Graph");
                string excel_file = "TECHSOFT_STRESSING_GRAPH.xls";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_dwg_main_girder_Click(object sender, EventArgs e)
        {

            iApp.SetDrawingFile_Path(LongGirder.user_drawing_file, "PreStressed_Main_Girder", "");
        }

        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {

        }

        //Chiranjit [2012 10 30]
        #region Design of RCC Pier

       
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;
        }

        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            //string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            //iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);


            string draw_cmd = "PSC_Box_Girder_Pier";
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), draw_cmd);


        }

        void Text_Changed()
        {
            //double L = MyList.StringToDouble(txt_Ana_Span.Text, 0.0);
            //double B = MyList.StringToDouble(txt_Ana_Road_Width.Text, 0.0);

            double Xincr = MyList.StringToDouble(txt_XINCR.Text, 0.0);

            txt_LL_load_gen.Text = (L / Xincr + 1).ToString("f0");
        }

        #endregion Design of RCC Pier



        private void btn_Open_Worksheet_Design_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex) { }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder";
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), draw_cmd);

            Button b = sender as Button;

            string draw = Drawing_Folder;



            eOpenDrawingOption opt = iApp.Open_Drawing_Option();
            if (opt == eOpenDrawingOption.Cancel) return;



            string excel_file = "PSC Box Girder\\" + "New Design Of PSC Box Girder.xls";
            //excel_file = Path.Combine(excel_path, excel_file);

            string copy_path = Path.Combine(Worksheet_Folder, excel_file);


            if (opt == eOpenDrawingOption.Design_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");

                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");


                    iApp.Form_Drawing_Editor(eBaseDrawings.PSC_BOX_Girder_GAD, Title, Drawing_Folder, copy_path).ShowDialog();

                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, Title, draw, copy_path).ShowDialog();
                }
                #endregion Design_Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), "PSC_Box_Girder");
                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                #endregion Design_Drawings
            }
        }

        private void btn_design_of_anchorage_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                string excel_file = "PSC Box Girder\\" + "Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

       

        #region Chiranjit [2012 02 08]
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = chk_M2.Checked;
            rft.M3 = chk_M3.Checked;
            rft.R3 = chk_R3.Checked;
            rft.R2 = chk_R2.Checked;
            return rft;
        }

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            string ana_rep_file = File.Exists(Deck_Analysis_LL.Analysis_Report) ? Deck_Analysis_LL.Analysis_Report : Analysis_Report_LL;
            //string ana_rep_file = Deck_Analysis_LL.Analysis_Report;
            iApp.Progress_ON("Read forces...");
            iApp.SetProgressValue(9, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_LL.Truss_Analysis = null;
                //Deck_Analysis_LL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_LL.Bridge_Analysis.ForceType = GetForceType();
                iApp.SetProgressValue(19, 100);
                Show_Moment_Shear_LL();
                iApp.SetProgressValue(29, 100);

                grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
                grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

                grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
                grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

                Button_Enable_Disable();
                Show_Analysis_Result();
                iApp.SetProgressValue(49, 100);
            }
            //else
            //    MessageBox.Show("Analysis Result not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



            ana_rep_file = File.Exists(Deck_Analysis_DL.Analysis_Report) ? Deck_Analysis_DL.Analysis_Report : Analysis_Report_DL;
            iApp.SetProgressValue(59, 100);
            if (File.Exists(ana_rep_file))
            {
                //Deck_Analysis_DL.Truss_Analysis = null;
                //Deck_Analysis_DL.Truss_Analysis = new SteelTrussMemberAnalysis(iApp, ana_rep_file);
                Deck_Analysis_DL.Bridge_Analysis.ForceType = GetForceType();
                iApp.SetProgressValue(69, 100);
                Show_Moment_Shear_DL();
            }
            iApp.SetProgressValue(89, 100);

            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            Button_Enable_Disable();
            Show_Analysis_Result();
            iApp.SetProgressValue(99, 100);
            iApp.Progress_OFF();
        }
        #endregion
        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                //txt_Ana_Span.Text = "0";
                //txt_Ana_Span.Text = "48.75";
                //txt_Ana_Road_Width.Text = "9.75";
                //txt_Ana_CW.Text = "10.75";txt_tab1_Lo

                //string str = "This is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "Email at: techsoft@consultant.com, dataflow@mail.com\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]
        public void Write_All_Data()
        {
            DemoCheck();

            iApp.Save_Form_Record(this, user_path);
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(user_path)) return "";
                return Path.Combine(user_path, "ASTRA_DATA_FILE.TXT");
            }
        }

        public void Read_All_Data()
        {
            if (iApp.IsDemo) return;

            string data_file = User_Input_Data;

            if (!File.Exists(data_file)) return;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            try
            {
                LongGirder.FilePath = user_path;
            }
            catch (Exception ex) { }

            List<string> file_content = new List<string>(File.ReadAllLines(data_file));

            ePSC_I_GirderOption TOpt = ePSC_I_GirderOption.None;

            MyList mlist = null;
            MyList mlist_mov_ll = null;
            string kStr = "";

            SaveRec.Clear();
            SaveRec.AddControls(this);

            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;

                    if (mlist.Count == 2)
                    {
                        try
                        {
                            Control c = SaveRec.Controls[mlist.StringList[0]] as Control;
                            if (c != null)
                            {
                                if (c.Name.StartsWith("txt"))
                                    c.Text = mlist.StringList[1];
                                else if (c.Name.StartsWith("cmb"))
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as ComboBox).SelectedItem = mlist.StringList[1];
                                }
                                else if (c.Name.StartsWith("dgv"))
                                {

                                    DataGridView dgv = c as DataGridView;
                                    int row = mlist.GetInt(1);
                                    dgv.Rows.Clear();
                                    i++;
                                    for (int j = 0; j < row; j++, i++)
                                    {
                                        kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                                        mlist = new MyList(kStr, '$');
                                        dgv.Rows.Add(mlist.StringList.ToArray());

                                    }

                                }

                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + kStr);
                }
            }
        }

        private void txt_Ana_DL_length_TextChanged(object sender, EventArgs e)
        {
            //txt_Ana_X.Text = "-" + txt_Ana_L.Text;

            MyList ml = new MyList(txt_Spans.Text, ',');
            txt_Ana_Span.Text = ml.SUM.ToString("f2");
            try
            {

                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    dgv_seg_tab3_1[i, 0].Value = txt_Ana_Superstructure_depth.Text;
                    dgv_seg_tab3_1[i, 1].Value = txt_Ana_Road_Width.Text;
                    dgv_seg_tab3_1[i, 3].Value = txt_Ana_Web_Spacing.Text;
                }
            }
            catch (Exception ex) { }



            Text_Changed();

        }

        public void Load_Tab2_Tab3_Box_Segment_Data()
        {

            List<string> list = new List<string>();
            //list.Add(string.Format("D 2.5 2.5 2.5 2.5 2.5 2.5"));
            //list.Add(string.Format("Dw 9.75 9.75 9.75 9.75 9.75 9.75"));
            //list.Add(string.Format("Td 0.225 0.225 0.225 0.225 0.225 0.225"));
            //list.Add(string.Format("C1 1.925 1.925 1.925 1.925 1.925 1.925"));
            //list.Add(string.Format("C2 0 0 0 0 0 0"));
            //list.Add(string.Format("Tip 0.2 0.2 0.2 0.2 0.2 0.2"));
            //list.Add(string.Format("Tf 0.3 0.3 0.3 0.3 0.3 0.3"));
            //list.Add(string.Format("Iw 0.7 0.7 0.7 0.7 0.7 0.7"));
            //list.Add(string.Format("D1 2.2 2.2 2.2 2.2 2.2 2.2"));
            //list.Add(string.Format("Tw 0.6 0.579 0.48 0.31 0.31 0.31"));
            //list.Add(string.Format("SW 4.5 4.5 4.5 4.5 4.5 4.5"));
            //list.Add(string.Format("Ts 0.55 0.26 0.26 0.26 0.26 0.26"));
            //list.Add(string.Format("D2 0 0 0 0 0 0"));
            //list.Add(string.Format("K1 0 0 0 0 0 0"));
            //list.Add(string.Format("K2 0.175 0.0827 0.0827 0.0827 0.0827 0.0827"));
            //list.Add(string.Format("HW1 0.409 0.485 0.582 0.75 0.75 0.75"));
            //list.Add(string.Format("HH1 0.083 0.097 0.116 0.15 0.15 0.15"));
            //list.Add(string.Format("CH1 1.85 1.85 1.85 1.85 1.85 1.85"));
            //list.Add(string.Format("HW2 0 0 0 0 0 0"));
            //list.Add(string.Format("HH2 0 0 0 0 0 0"));
            //list.Add(string.Format("HW3 0 0 0.088 0.3 0.3 0.3"));
            //list.Add(string.Format("HH3 0 0 0.044 0.15 0.15 0.15"));

            #region AASHTO DATA
            list.Add(string.Format("D$5.500$5.500$5.500$0.208$5.500$5.500"));
            list.Add(string.Format("Dw$45.167$45.167$45.167$45.167$45.167$45.167"));
            list.Add(string.Format("Td$0.667$0.667$0.667$0.667$0.667$0.667"));
            list.Add(string.Format("C1$2.625$2.625$2.625$2.625$2.625$2.625"));
            list.Add(string.Format("C2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("Tip$0.750$0.750$0.750$0.750$0.750$0.750"));
            list.Add(string.Format("Tf$1.000$1.000$1.000$1.000$1.000$1.000"));
            list.Add(string.Format("Iw$1.792$1.792$1.792$1.792$1.792$1.792"));
            list.Add(string.Format("D1$4.000$4.000$4.000$4.000$4.000$4.000"));
            list.Add(string.Format("Tw$1.000$1.000$1.000$1.000$1.000$1.000"));
            list.Add(string.Format("SW$36.333$36.333$36.333$36.333$36.333$36.333"));
            list.Add(string.Format("Ts$0.500$0.500$0.500$0.500$0.500$0.500"));
            list.Add(string.Format("D2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("K1$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("K2$0.015$0.007$0.007$0.007$0.007$0.007"));
            list.Add(string.Format("HW1$0.333$0.333$0.333$0.333$0.333$0.333"));
            list.Add(string.Format("HH1$0.333$0.333$0.333$0.333$0.333$0.333"));
            list.Add(string.Format("CH1$19.375$19.375$19.375$19.375$19.375$19.375"));
            list.Add(string.Format("HW2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HH2$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HW3$0.000$0.000$0.000$0.000$0.000$0.000"));
            list.Add(string.Format("HH3$0.000$0.000$0.000$0.000$0.000$0.000"));
            #endregion AASHTO DATA

            MyList mlist = null;


            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], '$');
                dgv_seg_tab3_1.Rows.Add(mlist.StringList.ToArray());
            }

            Set_Segment_Data();

        }

        private void Set_Segment_Data()
        {


            for (int i = 0; i < dgv_seg_tab3_1.Rows.Count; i++)
            {

                if (i == 0)
                    dgv_seg_tab3_1.Rows[i].Height = 26;
                else if (i > 13)
                    dgv_seg_tab3_1.Rows[i].Height = 16;
                else
                    dgv_seg_tab3_1.Rows[i].Height = 15;

                if (dgv_seg_tab3_1[0, i].Value.ToString() == "D2" ||
                    dgv_seg_tab3_1[0, i].Value.ToString() == "K1" ||
                    dgv_seg_tab3_1[0, i].Value.ToString() == "K2")
                {
                    dgv_seg_tab3_1.Rows[i].ReadOnly = true;
                    dgv_seg_tab3_1.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    dgv_seg_tab3_1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

      

        private void dgv_seg_tab3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Update_Tab3_Data();
        }

        private void Update_Tab3_Data()
        {
            try
            {
                double theta = 0.0;
                double D2 = 0.0;
                double K1 = 0.0;
                double K2 = 0.0;
                double va = 5;
                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    theta = Math.Atan((MyList.StringToDouble(dgv_seg_tab3_1[i, 7].Value.ToString(), 0.0) /
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 8].Value.ToString(), 0.0)));

                    va = (180.0 / Math.PI) * theta;

                    //Web Inclination = θ (degrees) = atan (Iw / D1) = atan (0.7 / 2.200) = atan(0.3182) = 17.650
                    //and  D2 = D – Tf – D1 = 2.500 – 0.300 – 2.200 = 0.0
                    D2 = (MyList.StringToDouble(dgv_seg_tab3_1[i, 0].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 6].Value.ToString(), 0.0) -
                            MyList.StringToDouble(dgv_seg_tab3_1[i, 8].Value.ToString(), 0.0));

                    K1 = D2 * Math.Tan(theta);
                    K2 = MyList.StringToDouble(dgv_seg_tab3_1[i, 11].Value.ToString(), 0.0) * Math.Tan(theta);


                    dgv_seg_tab3_1[i, 12].Value = D2.ToString("f3");
                    dgv_seg_tab3_1[i, 13].Value = K1.ToString("f3");
                    dgv_seg_tab3_1[i, 14].Value = K2.ToString("f3");
                    //K1 = D2 x tan(θ) = 0.0 x tan(17.6501) = 0.0
                    //K2 = Ts x tan(θ) = 0.550 x tan(17.6501) = 0.1750
                    theta = (180.0 / Math.PI) * theta;
                    if (i == 1) txt_tab3_support.Text = theta.ToString("f3");
                    if (i == 2) txt_tab3_d.Text = theta.ToString("f3");
                    if (i == 3) txt_tab3_L_8.Text = theta.ToString("f3");
                    if (i == 4) txt_tab3_L_4.Text = theta.ToString("f3");
                    if (i == 5) txt_tab3_3L_8.Text = theta.ToString("f3");
                    if (i == 6) txt_tab3_L_2.Text = theta.ToString("f3");
                }


            }
            catch (Exception ex) { }
        }

        private void btn_Show_Section_Result_Click(object sender, EventArgs e)
        {
            Show_Section_Result();
        }

        private void Show_Section_Result()
        {
            //Segment_Girder.FilePath = user_path;
            Segment_Girder_Initialize_Data();
            rtb_sections.Lines = Segment_Girder.Get_Step_1(ref PSC_SECIONS).ToArray();
        }

        public void Segment_Girder_Initialize_Data()
        {
            #region Variable Declaration

            //Chiranjit [2012 10 26]
            //Segment_Girder.Area_Zone1_Outer = MyList.StringToDouble(txt_zn1_out.Text, 1480.0);
            //Segment_Girder.Area_Zone2_Outer = MyList.StringToDouble(txt_zn2_out.Text, 1480.0);
            //Segment_Girder.Area_Zone3_Outer = MyList.StringToDouble(txt_zn3_out.Text, 1480.0);
            //Segment_Girder.Area_Zone1_Inner = MyList.StringToDouble(txt_zn1_inn.Text, 1480.0);
            //Segment_Girder.Area_Zone2_Inner = MyList.StringToDouble(txt_zn2_inn.Text, 1480.0);
            //Segment_Girder.Area_Zone3_Inner = MyList.StringToDouble(txt_zn3_inn.Text, 1480.0);

            ////Chiranjit [2012 10 17]
            //Segment_Girder.rss_56 = MyList.StringToDouble(txt_tab2_rss_56.Text, 0.00019);
            //Segment_Girder.rss_14 = MyList.StringToDouble(txt_tab2_rss_14.Text, 0.00025);
            //Segment_Girder.Resh56 = MyList.StringToDouble(txt_tab2_Resh56.Text, 0.00025);
            //Segment_Girder.Crst56 = MyList.StringToDouble(txt_tab2_Crst56.Text, 0.00025);

            ////Chiranjit [2012 10 18]
            //Segment_Girder.fc_temp14 = MyList.StringToDouble(txt_fc_temp14.Text, 0.00025);
            //Segment_Girder.ft_temp14 = MyList.StringToDouble(txt_ft_temp14.Text, 0.00025);
            //Segment_Girder.fc_temp28 = MyList.StringToDouble(txt_fc_temp28.Text, 0.00025);
            //Segment_Girder.ft_temp28 = MyList.StringToDouble(txt_ft_temp28.Text, 0.00025);
            //Segment_Girder.ttv = MyList.StringToDouble(txt_ttv.Text, 0.00025);
            //Segment_Girder.fc_serv = MyList.StringToDouble(txt_fc_serv.Text, 0.00025);
            //Segment_Girder.Modrup = MyList.StringToDouble(txt_Mod_rup.Text, 0.00025);
            //Segment_Girder.fc_fact = MyList.StringToDouble(txt_fc_factor.Text, 0.00025);
            //Segment_Girder.tv = MyList.StringToDouble(txt_tv.Text, 0.00025);
            //Segment_Girder.ttu = MyList.StringToDouble(txt_ttu.Text, 0.00025);

            //Segment_Girder.Lo = MyList.StringToDouble(txt_tab1_Lo.Text, 48.750);

            //Segment_Girder.L1 = MyList.StringToDouble(txt_tab1_L1.Text, 0.500);
            //Segment_Girder.L2 = MyList.StringToDouble(txt_tab1_L2.Text, 0.500);
            //Segment_Girder.exg = MyList.StringToDouble(txt_tab1_exg.Text, 0.040);

            //Segment_Girder.Dw = MyList.StringToDouble(txt_tab1_DW.Text, 9.750);
            //Segment_Girder.D = MyList.StringToDouble(txt_tab1_D.Text, 2.500);
            //Segment_Girder.Fcu = MyList.StringToDouble(cmb_tab1_Fcu.Text, 40);
            //Segment_Girder.Tab1_Fy = MyList.StringToDouble(cmb_tab1_Fy.Text, 415);
            //Segment_Girder.act = MyList.StringToDouble(txt_tab1_act.Text, 14);
            //Segment_Girder.mct = MyList.StringToDouble(txt_tab1_Mct.Text, 87);
            //Segment_Girder.sct = MyList.StringToDouble(txt_tab1_sctt.Text, 34.8);
            //Segment_Girder.acsidl = MyList.StringToDouble(txt_tab1_agt_SIDL.Text, 56);
            //Segment_Girder.mtcsidl = MyList.StringToDouble(txt_tab1_Mct_SIDL.Text, 100);
            //Segment_Girder.T_loss = MyList.StringToDouble(txt_tab1_T_loss.Text, 20);
            //Segment_Girder.wct = MyList.StringToDouble(txt_tab1_wct.Text, 0.065);
            //Segment_Girder.ds = MyList.StringToDouble(txt_tab1_ds.Text, 0.225);
            //Segment_Girder.df = MyList.StringToDouble(txt_tab1_df.Text, 0.175);
            //Segment_Girder.bt = MyList.StringToDouble(txt_tab1_bt.Text, 1.000);

            //Segment_Girder.FactDL = MyList.StringToDouble(txt_tab1_FactDL.Text, 1.250);
            //Segment_Girder.FactSIDL = MyList.StringToDouble(txt_tab1_FactSIDL.Text, 2.000);
            //Segment_Girder.FactLL = MyList.StringToDouble(txt_tab1_FactLL.Text, 2.500);

            //Segment_Girder.alpha = MyList.StringToDouble(txt_tab1_alpha.Text, 0.0000117);


            //Segment_Girder.Tr1 = MyList.StringToDouble(txt_tab1_Tr1.Text, 17.8);
            //Segment_Girder.Tr2 = MyList.StringToDouble(txt_tab1_Tr2.Text, 4.0);
            //Segment_Girder.Tr3 = MyList.StringToDouble(txt_tab1_Tr3.Text, 2.1);


            //Segment_Girder.Tf1 = MyList.StringToDouble(txt_tab1_Tf1.Text, 10.6);
            //Segment_Girder.Tf2 = MyList.StringToDouble(txt_tab1_Tf2.Text, 0.7);
            //Segment_Girder.Tf3 = MyList.StringToDouble(txt_tab1_Tf3.Text, 0.8);
            //Segment_Girder.Tf4 = MyList.StringToDouble(txt_tab1_Tf4.Text, 6.6);


            //Prestressing Input Data:   [Tab 2]

            //A)                 Construction Schedule and Prestressing Stages

            //    Job                                                                                                 Day after casting                 fcj   N/sq.mm (Mpa)
            //(i)                Completion of casting of Box Girder                                0                day
            //(ii)                First Stage Prestress                                                                14                day                                fcj14 = 34.80
            //(iii)                Completion of Wearing Course & Crash Barrier                56                day                                fcj56 = 40.00


            //Segment_Girder.ccbg_day = MyList.StringToDouble(txt_tab2_ccbg_day.Text, 10.6);
            //Segment_Girder.ccbg_fcj = MyList.StringToDouble(txt_tab2_ccbg_fcj.Text, 0.7);

            //Segment_Girder.fsp_day = MyList.StringToDouble(txt_tab2_fsp_day.Text, 10.6);
            //Segment_Girder.fsp_fcj = MyList.StringToDouble(txt_tab2_fsp_fcj.Text, 0.7);

            //Segment_Girder.cwccb_day = MyList.StringToDouble(txt_tab2_cwccb_day.Text, 10.6);
            //Segment_Girder.cwccb_fcj = MyList.StringToDouble(txt_tab2_cwccb_fcj.Text, 0.7);



            //B)                Cable and Prestressing Data

            //D                = 15.200 mm.                                   
            //A = 140.000 Sq.mm.
            //Pu = 1.100 Kg/m
            //Fy = 1670.000 N/Sq.mm. (Mpa)
            //Fu = 1860.000 N/Sq.mm. (Mpa)
            //Pn = 260.700 kN
            //Eps = 195 Gpa
            //Pj = 76.5 %
            //s = 6 mm.
            //µ = 0.17 per radian
            //k = 0.002 per metre
            //Re1 = 35.0 N/Sq.mm (Mpa)
            //Re2 = 0.0 N/Sq.mm (Mpa)
            //td1 = 14 days                     (Taken From Tab 1)
            //qd = 110 mm.
            //Fcu = 40 N/Sq.mm (Mpa)                                                  (Taken  From Tab 1)
            //Ec = 5000 x Sqrt(40) = 31622.8 N/Sq.mm (Mpa)


            //Segment_Girder.ND = MyList.StringToDouble(txt_tab2_D.Text, 15.200);
            //Segment_Girder.NA = MyList.StringToDouble(txt_tab2_A.Text, 140.000);
            //Segment_Girder.Pu = MyList.StringToDouble(txt_tab2_Pu.Text, 1.100);
            //Segment_Girder.Tab2_Fy = MyList.StringToDouble(txt_tab2_Fy.Text, 1670.000);
            //Segment_Girder.Fu = MyList.StringToDouble(txt_tab2_Fu.Text, 1860.000);
            //Segment_Girder.Pn = MyList.StringToDouble(txt_tab2_Pn.Text, 260.700);
            //Segment_Girder.Eps = MyList.StringToDouble(txt_tab2_Eps.Text, 195);
            //Segment_Girder.Pj = MyList.StringToDouble(txt_tab2_Pj.Text, 76.5);
            //Segment_Girder.s = MyList.StringToDouble(txt_tab2_s.Text, 6);
            //Segment_Girder.mu = MyList.StringToDouble(txt_tab2_mu.Text, 0.17);
            //Segment_Girder.k = MyList.StringToDouble(txt_tab2_k.Text, 0.002);
            //Segment_Girder.Re1 = MyList.StringToDouble(txt_tab2_Re1.Text, 35.0);
            //Segment_Girder.Re2 = MyList.StringToDouble(txt_tab2_Re2.Text, 0.0);
            //Segment_Girder.td1 = MyList.StringToDouble(txt_tab2_td1.Text, 14);
            //Segment_Girder.qd = MyList.StringToDouble(txt_tab2_qd.Text, 110);
            //Segment_Girder.cover1 = MyList.StringToDouble(txt_tab2_cover1.Text, 110);
            //Segment_Girder.cover2 = MyList.StringToDouble(txt_tab2_cover2.Text, 110);
            //Segment_Girder.Ns = MyList.StringToDouble(txt_tab2_Ns.Text, 19);
            //Segment_Girder.Nc_Left = MyList.StringToInt(txt_tab2_nc_left.Text, 7);
            //Segment_Girder.Nc_Right = MyList.StringToInt(txt_tab2_nc_right.Text, 7);
            //Segment_Girder.Cable_Area = MyList.StringToInt(txt_tab2_cable_area.Text, 7);


            Segment_Girder.Section_Theta.Clear();
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_support.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_d.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_8.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_4.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_3L_8.Text, 0.0));
            Segment_Girder.Section_Theta.Add(MyList.StringToDouble(txt_tab3_L_2.Text, 0.0));


            PSC_Force_Data list_va = new PSC_Force_Data(0);
            for (int i = 0; i < dgv_seg_tab3_1.RowCount; i++)
            {
                list_va = new PSC_Force_Data(0);
                for (int c = 1; c < dgv_seg_tab3_1.ColumnCount; c++)
                {
                    list_va.Add(MyList.StringToDouble(dgv_seg_tab3_1[c, i].Value.ToString(), 0.0));
                }

                if (i == 0)
                    Segment_Girder.Section_D = list_va;

                else if (i == 1)
                    Segment_Girder.Section_Dw = list_va;

                else if (i == 2)
                    Segment_Girder.Section_Td = list_va;

                else if (i == 3)
                    Segment_Girder.Section_C1 = list_va;

                else if (i == 4)
                    Segment_Girder.Section_C2 = list_va;

                else if (i == 5)
                    Segment_Girder.Section_Tip = list_va;

                else if (i == 6)
                    Segment_Girder.Section_Tf = list_va;

                else if (i == 7)
                    Segment_Girder.Section_lw = list_va;

                else if (i == 8)
                    Segment_Girder.Section_D1 = list_va;

                else if (i == 9)
                    Segment_Girder.Section_Tw = list_va;

                else if (i == 10)
                    Segment_Girder.Section_SW = list_va;

                else if (i == 11)
                    Segment_Girder.Section_Ts = list_va;

                else if (i == 12)
                    Segment_Girder.Section_D2 = list_va;

                else if (i == 13)
                    Segment_Girder.Section_K1 = list_va;

                else if (i == 14)
                    Segment_Girder.Section_K2 = list_va;

                else if (i == 15)
                    Segment_Girder.Section_HW1 = list_va;

                else if (i == 16)
                    Segment_Girder.Section_HH1 = list_va;

                else if (i == 17)
                    Segment_Girder.Section_CH1 = list_va;

                else if (i == 18)
                    Segment_Girder.Section_HW2 = list_va;

                else if (i == 19)
                    Segment_Girder.Section_HH2 = list_va;

                else if (i == 20)
                    Segment_Girder.Section_HW3 = list_va;

                else if (i == 21)
                    Segment_Girder.Section_HH3 = list_va;
            }

            //Segment_Girder.Fcu = 40;
            //Ec = 31622.8;
            //double Ec = 5000 x rt(40) = 31622.8 N/.mm (Mpa)


   
            //Segment_Girder.Cable_Type = cmb_tab2_strand_data.Text;

 
            //Segment_Girder.L_Deff = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);



            #endregion Variable Declaration
        }

        private void btn_segment_report_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { }
        }

        private void txt_Ana_DL_factor_TextChanged(object sender, EventArgs e)
        {
            Text_Changed_Forces();
        }

        private void txt_tab1_Lo_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmb_tab1_Fcu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Concrete_Grade();
        }

        private void Set_Concrete_Grade()
        {
        }

        private void cmb_tab2_strand_data_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_tab2_nc_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txt_pier_2_APD_TextChanged(object sender, EventArgs e)
        {
        }


        #region Chiranjit [2014 03 12] Support Input
        public string Start_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_ssprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_ssprt_fixed.Checked)
                {
                    kStr = "FIXED";


                    if (chk_ssprt_fixed_FX.Checked
                        || chk_ssprt_fixed_FY.Checked
                        || chk_ssprt_fixed_FZ.Checked
                        || chk_ssprt_fixed_MX.Checked
                        || chk_ssprt_fixed_MY.Checked
                        || chk_ssprt_fixed_MZ.Checked)
                        kStr += " BUT";

                    if (chk_ssprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_ssprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_ssprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_ssprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }
        public string END_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_esprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_esprt_fixed.Checked)
                {
                    kStr = "FIXED";
                    if (chk_esprt_fixed_FX.Checked
                        || chk_esprt_fixed_FY.Checked
                        || chk_esprt_fixed_FZ.Checked
                        || chk_esprt_fixed_MX.Checked
                        || chk_esprt_fixed_MY.Checked
                        || chk_esprt_fixed_MZ.Checked)
                        kStr += " BUT";
                    if (chk_esprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_esprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_esprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_esprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_esprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }

        private void rbtn_ssprt_pinned_CheckedChanged(object sender, EventArgs e)
        {

            chk_esprt_fixed_FX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FZ.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MZ.Enabled = rbtn_esprt_fixed.Checked;

            chk_ssprt_fixed_FX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FZ.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MZ.Enabled = rbtn_ssprt_fixed.Checked;
        }
        #endregion Chiranjit [2014 03 12] Support Input

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {
            iApp.Show_LL_Dialog();
        }



        #region Chiranjit [2014 09 10]
        #region British Standard Loading

        public bool IsRead = false;



        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();





        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Set File Name
            FILE_SUMMARY_RESULTS = Path.Combine(user_path, "Process\\ANALYSIS RESULT SUMMMARY.TXT");

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                if (cmb_long_open_file_process.SelectedIndex < cmb_long_open_file_process.Items.Count)
                {

                    if (cmb_long_open_file_process.SelectedIndex == 0)
                        file_name = Input_File_DL;
                    //else if (cmb_long_open_file_process.SelectedIndex == 1 && iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    file_name = Input_File_LL;
                    else
                        file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file_process.SelectedIndex);
                }
                else
                {
                    //file_name = Result_Report_LL;
                    file_name = FILE_SUMMARY_RESULTS;
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_view_structure.Enabled = File.Exists(file_name);
            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            btn_View_Result_summary.Enabled = File.Exists(FILE_SUMMARY_RESULTS);

        }

        #endregion British Standard Loading

        #endregion Chiranjit [2014 09 10]

        public string Get_Live_Load_Analysis_Input_File(int analysis_no)
        {
            string working_folder = user_path;

            //string ll_path =  Path.Combine(user_path, "Input_Data_DL.txt");

            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "ANALYSIS PROCESS\\LL ANALYSIS LOAD " + analysis_no);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_LOAD_" + analysis_no + "_INPUT_FILE.txt");
            }
            return "";
        }

        void Ana_Write_Long_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";
            bool fl = false;
            if (add_DeadLoad)
            {

                //if (add_LiveLoad)
                //{
                //    foreach (var item in txt_Ana_LL_member_load.Lines)
                //    {

                //        if (item.ToUpper().StartsWith("LOAD"))
                //        {
                //            if (fl == false)
                //            {
                //                fl = true;
                //                load_lst.Add(item);
                //            }
                //            else
                //                load_lst.Add("*" + item);
                //        }
                //        else
                //        {
                //            if (!load_lst.Contains(item))
                //                load_lst.Add(item);
                //            else
                //                load_lst.Add("*" + item);
                //        }
                //    }
                //}
                //else
                    //load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

            }
            inp_file_cont.InsertRange(indx, load_lst);
            File.WriteAllLines(file_name, inp_file_cont.ToArray());

        }

        private void btn_view_data_Click(object sender, EventArgs e)
        {



            string file_name = "";
            string ll_txt = "";




            Button btn = sender as Button;


            ComboBox cmb = cmb_long_open_file_process;

            if (btn == btn_view_data_1)
            {
                cmb = cmb_long_open_file_analysis;
            }


            #region Set File Name
            if (cmb.SelectedIndex < cmb.Items.Count)
            {
                //file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);

                if (cmb.SelectedIndex == 0)
                    file_name = Input_File_DL;
                else
                    file_name = Get_Live_Load_Analysis_Input_File(cmb.SelectedIndex);

            }
            //else
            //{
            //    file_name = Result_Report_LL;
            //}
            #endregion Set File Name

            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name || btn == btn_view_data_1)
            {
                //if (cmb.SelectedIndex == cmb.Items.Count - 1)
                //    iApp.View_Result(file_name);
                //else
                    iApp.View_Input_File(file_name);
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                if (File.Exists(file_name))
                    iApp.OpenWork(file_name, false);
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_View_Result_summary.Name)
            {
                //file_name = MyList.Get_Analysis_Report_File(file_name);
                //if (File.Exists(file_name))
                //    iApp.OpenWork(file_name, true);

                if (File.Exists(FILE_SUMMARY_RESULTS))
                {
                    System.Diagnostics.Process.Start(FILE_SUMMARY_RESULTS);
                }
            }
        }
        frm_Box_Girder_Diagra_ f_diagram;
        private void btn_open_diagram_Click(object sender, EventArgs e)
        {
            try
            {
                f_diagram.Close();
            }
            catch (Exception ex) { }


            f_diagram = new frm_Box_Girder_Diagra_();
            f_diagram.Owner = this;
            f_diagram.Show();
        }

        private void btn_psc_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_psc_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    //iApp.Read_Form_Record(this, user_path);

                    txt_project_name.Text = Path.GetFileName(user_path);


                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                    }


                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    //Write_All_Data();

                    #endregion Save As

                }
            }
            else if (btn.Name == btn_psc_new_design.Name)
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
                //    IsCreate_Data = true;
                //}
                IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }
        
        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
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
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.PSC_Box_Girder_Bridge_WS;
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
        bool dim_flag = false;
        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            if (dim_flag)
            {
                pictureBox3.BackgroundImage = LimitStateMethod.Properties.Resources.AASHTO_BOX_CROSSSECTION;
            }
            else
            {
                pictureBox3.BackgroundImage = LimitStateMethod.Properties.Resources.AASHTO_BOX_CROSSSECTION_SAMPLE;
            }
            dim_flag = !dim_flag;
        }

        AASHTO_Box_Section aashto_box;
        void Section2_Section_Properties()
        {
            aashto_box = new AASHTO_Box_Section();

            int cs_cell_nos = MyList.StringToInt(txt_box_cs2_cell_nos.Text, 0);
            double cs_b1 = MyList.StringToDouble(txt_box_cs2_b1.Text, 0.0);
            double cs_b2 = MyList.StringToDouble(txt_box_cs2_b2.Text, 0.0);
            double cs_b3 = MyList.StringToDouble(txt_box_cs2_b3.Text, 0.0);
            double cs_b4 = MyList.StringToDouble(txt_box_cs2_b4.Text, 0.0);
            double cs_b5 = MyList.StringToDouble(txt_box_cs2_b5.Text, 0.0);
            double cs_b6 = MyList.StringToDouble(txt_box_cs2_b6.Text, 0.0);
            double cs_b7 = MyList.StringToDouble(txt_box_cs2_b7.Text, 0.0);
            double cs_b8 = MyList.StringToDouble(txt_box_cs2_b8.Text, 0.0);


            double cs_d1 = MyList.StringToDouble(txt_box_cs2_d1.Text, 0.0);
            double cs_d2 = MyList.StringToDouble(txt_box_cs2_d2.Text, 0.0);
            double cs_d3 = MyList.StringToDouble(txt_box_cs2_d3.Text, 0.0);
            double cs_d4 = MyList.StringToDouble(txt_box_cs2_d4.Text, 0.0);
            double cs_d5 = MyList.StringToDouble(txt_box_cs2_d5.Text, 0.0);



            aashto_box.Cell_Nos = cs_cell_nos;
            aashto_box.b1 = cs_b3;
            aashto_box.d1 = cs_d3;



            aashto_box.b2 = cs_b4;
            aashto_box.d2 = cs_d4 - cs_d3 -cs_d5;

            aashto_box.b3 = cs_b8;
            aashto_box.d3 = cs_d5;


            aashto_box.b4 = cs_b6;
            aashto_box.d4 = cs_d1;

            aashto_box.b5 = cs_b6;
            aashto_box.d5 = cs_d2 - cs_d1;

            aashto_box.b6 = cs_b5;
            //aashto_box.d6 = cs_d;

            aashto_box.b7 = cs_b7;
            aashto_box.d7 = cs_d3;

            aashto_box.b8 = cs_b7;
            aashto_box.d8 = cs_d2 - cs_d3;

            double val = cs_d4 - cs_d3 - cs_d5;

            double theta = Math.Atan(cs_b7 / val);
            aashto_box.theta_radian = theta;


            double b10 = Math.Tan(theta) * cs_d5;

            aashto_box.b9 = cs_b5 - b10;
            aashto_box.d9 = cs_d5;

            aashto_box.b10 = b10;
            aashto_box.d10 = cs_d5;


            txt_box_cs2_AX.Text = aashto_box.AX.ToString("f0");
            txt_box_cs2_IXX.Text = aashto_box.IXX.ToString("f0");
            txt_box_cs2_IYY.Text = aashto_box.IYY.ToString("f0");


            
        }

        private void txt_box_cs2_b1_TextChanged(object sender, EventArgs e)
        {
            Section2_Section_Properties();
        }


        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                list.Add(string.Format("TYPE 1, LRFD_HL_93"));
                list.Add(string.Format("AXLE LOAD IN KIP,8.0,32.0,32.0 "));
                list.Add(string.Format("AXLE SPACING IN FT,14.1,29.52"));
                list.Add(string.Format("AXLE WIDTH IN FT,5.9"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
            }

            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {

                #region Long Girder
                list.Add(string.Format("LOAD 1,TYPE 1"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,6.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,6.5,16.5,"));
                list.Add(string.Format(""));
                #endregion
            }

            Default_Moving_Type_LoadData(dgv_live_load, list);
        }
        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load, List<string> list)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        private void btn_process_steel_section_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_process_steel_section)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC Box Girder Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_PSC_Box_Girder_Design(iApp, dgv_steel_girder_input_data, Excel_File);
            }
            else if (btn == btn_process_abutment)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Abutment_and_Wingwall_Design(iApp, dgv_abutment_input_data, Excel_File);
            }
            else if (btn == btn_process_pier)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Pier_Design(iApp, dgv_pier_input_data, Excel_File);
            }
            else if (btn == btn_process_bearing)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
                AASHTO_Design_PC_Box_Girder.Process_Bearing_Design(iApp, dgv_bearing_input_data, Excel_File);
            }
        }

        private void btn_bearing_open_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string Excel_File = "";
            if (btn == btn_steel_section_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC Box Girder Design.xlsx");
            }
            else if (btn == btn_abutment_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
            }
            else if (btn == btn_pier_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
            }
            else if (btn == btn_bearing_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
            }
            if (File.Exists(Excel_File)) iApp.OpenExcelFile(Excel_File, "2011ap");
            else MessageBox.Show(Excel_File + " file not created.");

        }


        public void DataChange()
        {
            DataChange(dgv_steel_girder_input_data);
            DataChange(dgv_abutment_input_data);
            DataChange(dgv_pier_input_data);
            DataChange(dgv_bearing_input_data);
        }
        public void DataChange(DataGridView dgv)
        {
            Set_PSC_Box_Girder_Inputs();
            //DataGridView dgv = dgv_deck_input_data;

            if (dgv == dgv_steel_girder_input_data)
            {
                #region For Steel Girder Design
                //1	Number of spans:	Nspans =	2	
                //var Nspans = Inputs.Nspans;
                //dgv[2, 1].Value = Nspans;

                ////2	Span length:	Lspan =	120	ft
                //dgv[2, 2].Value = Inputs.Lspan.ToString();

                ////3	Skew angle:	Skew =	0	deg
                //dgv[2, 3].Value = Inputs.Skew.ToString();

                ////4	Number of girders:	Ngirders =	5	
                //dgv[2, 4].Value = Inputs.N.ToString();
                ////5	Girder spacing:	S =	9.75	ft
                //dgv[2, 5].Value = Inputs.S.ToString();
                ////6	Deck overhang:	Soverhang =	3.9375	ft
                //var Soverhang = Inputs.Soverhang;
                //dgv[2, 6].Value = Soverhang.ToString();

                #endregion For Steel Girder Design
            }
            else if (dgv == dgv_abutment_input_data)
            {
                #region abutment Design
                ////2	Concrete density:	Wc =	0.15	kcf
                //var Wc = Inputs.Wc;
                //dgv[2, 2].Value = Wc;
                ////3	Concrete 28-day compressive strength:	f'c =	4	ksi
                //var Fc = Inputs.fc;
                //dgv[2, 3].Value = Fc;
                ////4	Reinforcement strength:	fy =	60	ksi
                //var fy = Inputs.fys;
                //dgv[2, 4].Value = fy;

                ////11	Girder spacing:	S =	9.75	ft
                //var S = Inputs.S;
                //dgv[2, 11].Value = S;
                ////12	Number of girders:	N =	5	nos
                //var N = Inputs.N;
                //dgv[2, 12].Value = S;
                ////13	Span length:	Lspan =	120	ft
                //var Lspan = Inputs.Lspan;
                //dgv[2, 13].Value = Lspan;
                ////14	Parapet height:	Hpar =	3.5	ft
                //var Hpar = Inputs.Hpar;
                //dgv[2, 14].Value = Hpar;
                ////15	Parapet weight (each):	Wpar =	0.53	K/ft
                //var Wpar = Inputs.Wpar;
                //dgv[2, 15].Value = Wpar;
                ////16	Out-to-out deck width:	Wdeck =	46.875	ft
                //var Wdeck = Inputs.wdeck;
                //dgv[2, 16].Value = Wdeck;

                ////21	Abutment length:	Labut =	46.875	ft
                //var Labut = Inputs.wdeck;
                //dgv[2, 21].Value = Labut;


                #endregion For abutment Design
            }
            else if (dgv == dgv_pier_input_data)
            {
                #region pier Design
                //2	Concrete density:	Wc =	0.15	kcf

                //var Wc = Inputs.Wc;
                //dgv[2, 2].Value = Wc;
                ////3	Concrete 28-day	f'c =	4	ksi
                //var Fc = Inputs.fc;
                //dgv[2, 3].Value = Fc;
                ////5	Reinforcement	fy =	60	ksi
                //var fy = Inputs.fys;
                //dgv[2, 5].Value = fy;

                ////13	Girder spacing:	S =	9.75	ft
                //var S = Inputs.S;
                //dgv[2, 13].Value = S;
                ////14	Number of girders:	N =	5	
                //var N = Inputs.N;
                //dgv[2, 14].Value = N;
                ////15	Deck overhang:	DOH =	3.9375	ft
                //var DOH = Inputs.Soverhang;
                //dgv[2, 15].Value = DOH;
                ////16	Span length:	Lspan =	120	ft
                //var Lspan = Inputs.Lspan;
                //dgv[2, 16].Value = Lspan;
                ////17	Parapet height:	Hpar =	3.5	ft
                //var Hpar = Inputs.Hpar;
                //dgv[2, 17].Value = Hpar;
                ////18	Deck overhang thickness:	to =	9	ft
                ////var _to = 9;
                ////dgv[2, 18].Value = _to;
                ////19	Haunch thickness:	Hhnch =	3.5	in
                ////var Hhnch = 3.5;
                ////dgv[2, 14].Value = Hhnch;
                ////20	Web depth:	Do =	66	in
                ////var _Do = 66;
                ////dgv[2, 14].Value = _Do;



                #endregion For abutment Design
            }
            else if (dgv == dgv_bearing_input_data)
            {
                #region pier Design
                //5	Maximum possible length of footing	L =	46.875	ft
                //var L = 46.875;
                //dgv[2, 5].Value = Wc;
                #endregion For abutment Design
            }
        }
        PSC_Box_Girder_Inputs Inputs = new PSC_Box_Girder_Inputs();

        public void Set_PSC_Box_Girder_Inputs()
        {

            //Inputs.txt_LSpan = txt_Ana_L;
            //Inputs.txt_LTotal = txt_multiSpan;
            //Inputs.txt_Coverb = txt_Ana_coverb;
            //Inputs.txt_Covert = txt_Ana_covert;
            //Inputs.txt_Ec = txt_Ana_Ec;
            //Inputs.txt_Es = txt_Ana_Es;
            //Inputs.txt_fc = txt_Ana_fc;
            //Inputs.txt_Fu = txt_Ana_Fu;
            //Inputs.txt_Fy = txt_Ana_Fy;
            //Inputs.txt_fys = txt_Ana_fys;
            //Inputs.txt_Hpar = txt_Ana_Hpar;
            //Inputs.txt_LSpan = txt_Ana_L;
            //Inputs.cmb_N = cmb_Ana_NMG;
            //Inputs.txt_n = txt_Ana_n;
            //Inputs.txt_S = txt_Ana_S;
            //Inputs.txt_Skew = txt_Ana_Skew;
            //Inputs.txt_Soverhang = txt_Ana_Soverhang;
            //Inputs.txt_tdeck = txt_Ana_tdeck;
            //Inputs.txt_tfws = txt_Ana_tfws;
            //Inputs.txt_wbase = txt_Ana_wbase;
            //Inputs.txt_wdeck = txt_Ana_wdeck;
            //Inputs.txt_wfws = txt_Ana_Wfws;
            //Inputs.txt_Wmisc = txt_Ana_Wmisc;
            //Inputs.txt_Wpar = txt_Ana_Wpar;
            //Inputs.txt_wroadway = txt_Ana_wroadway;
            //Inputs.txt_Ws = txt_Ana_Ws;
            //Inputs.txt_Wc = txt_Ana_Wc;


            //Inputs.wbase = (Inputs.wdeck - Inputs.wroadway) / 2;
            //Inputs.txt_wbase.ForeColor = Color.Blue;



            //Inputs.S = (Inputs.wdeck - 2 * Inputs.Soverhang) / (Inputs.N - 1);
            //Inputs.txt_S.ForeColor = Color.Blue;


            //Inputs.Ec = 33000 * (Math.Pow(Inputs.Wc, 1.5) * Math.Sqrt(Inputs.fc));
            //Inputs.txt_Ec.ForeColor = Color.Blue;


            //Inputs.n = Math.Round((Inputs.Es / Inputs.Ec), 0);
            //Inputs.txt_n.ForeColor = Color.Blue;


        }

        private void btn_steel_section_ws_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();

        }

        private void btn_open_drawings_Click_1(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder";
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), draw_cmd);

            Button b = sender as Button;

            string draw = Drawing_Folder;



            eOpenDrawingOption opt = iApp.Open_Drawing_Option();
            if (opt == eOpenDrawingOption.Cancel) return;



            string excel_file = "PSC Box Girder\\" + "New Design Of PSC Box Girder.xls";
            //excel_file = Path.Combine(excel_path, excel_file);

            string copy_path = Path.Combine(Worksheet_Folder, excel_file);


            if (opt == eOpenDrawingOption.Design_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");

                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");


                    iApp.Form_Drawing_Editor(eBaseDrawings.PSC_BOX_Girder_GAD, Title, Drawing_Folder, copy_path).ShowDialog();

                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, Title, draw, copy_path).ShowDialog();
                }
                #endregion Design_Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {

                #region Design_Drawings
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), "PSC_Box_Girder");
                }
                //if (b.Name == btn_dwg_open_consts.Name)
                //{
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                //}
                //if (b.Name == btn_dwg_rcc_abut.Name)
                //{
                //    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                //    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                //}
                else if (b.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                //else if (b.Name == btn_dwg_open_Deckslab.Name)
                //{
                //    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                //}
                else if (b.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Counterfort Abutment Drawings"), "BOX_ABUTMENT");
                }
                else if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Canlilever Abutment Drawings"), "TBeam_Abutment");
                }
                else if (b.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_Box_Girder_Pier");
                }
                #endregion Design_Drawings
            }
        }

        private void cmb_long_open_file_preprocess_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null) return;

            #region Set File Name

            string file_name = "";
            if (Deck_Analysis_DL != null )
            {
                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    if (cmb.SelectedIndex < cmb.Items.Count && cmb.SelectedIndex > 0)
                    {
                        file_name = Get_Live_Load_Analysis_Input_File(cmb.SelectedIndex);
                    }
                    else
                    {
                        file_name = Input_File_DL;
                    }
                }
                //else
                //{
                //    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
                //}
            }

            #endregion Set File Name


            if (cmb == cmb_long_open_file_analysis)
            {
                if (File.Exists(file_name))
                {
                    ucPreProcess1.FilePath = file_name;
                    ucPreProcess1.IsFlag = false;
                    ucPreProcess1.Load_Initials();
                }
            }
            else if (cmb_long_open_file_preprocess == cmb)
            {
                if (File.Exists(file_name))
                {
                    ucPreProcess1.FilePath = file_name;
                    ucPreProcess1.IsFlag = false;
                    ucPreProcess1.Load_Initials();
                }
            }
            else if (cmb == cmb_long_open_file_process)
            {

                btn_view_data.Enabled = File.Exists(file_name);
                btn_View_Result_summary.Enabled = File.Exists(FILE_SUMMARY_RESULTS);
                btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            }
            else if (cmb == cmb_long_open_file_post_process)
            {
                if (File.Exists(file_name))
                {

                    
                    ucPostProcess1.FilePath = file_name;
                    ucPostProcess1.Curve_Radius = 0.0;
                    ucPostProcess1.Load_Initials(file_name);
                    //ucPostProcess1.PP_Tab_Selection();

                }
            }
        }


        public string FILE_SUMMARY_RESULTS { get; set; }

        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {

        }
        internal string GetAnalysis_Input_File(int p)
        {
            if (p == 0)
                return Deck_Analysis_DL.Input_File;
            else
            {
                return Get_Live_Load_Analysis_Input_File(p);
            }
            return "";

        }
        private void tc_AnaProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_process.SelectedTab == tab_pre_process)
            {
                if (!ucPreProcess1.IsFlag)
                {
                    ucPreProcess1.FilePath = GetAnalysis_Input_File(cmb_long_open_file_preprocess.SelectedIndex);
                    ucPreProcess1.Load_Initials();
                    ucPreProcess1.IsFlag = true;
                }
            }
            else if (tc_process.SelectedTab == tab_process)
            {
                //if (!ucPreProcess1.IsFlag)
                //{
                //ucPreProcess1.FilePath = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
                //ucPreProcess1.Load_Initials();
                //ucPreProcess1.IsFlag = true;
                if (cmb_long_open_file_process.SelectedIndex == -1)
                {
                    if (cmb_long_open_file_process.Items.Count > 0)
                        cmb_long_open_file_process.SelectedIndex = 0;
                    //ucPostProcess1.Load_Initials(ucPreProcess1.FilePath);
                }
                //btn_pro

                //}
            }
            else if (tc_process.SelectedTab == tab_post_process)
            {
                //if (!ucPreProcess1.IsFlag)
                //{
                //ucPreProcess1.FilePath = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
                //ucPreProcess1.Load_Initials();
                //ucPreProcess1.IsFlag = true;
                if (cmb_long_open_file_post_process.SelectedIndex == -1)
                {
                    if (cmb_long_open_file_post_process.Items.Count > 0)
                        cmb_long_open_file_post_process.SelectedIndex = 0;
                    //ucPostProcess1.Load_Initials(ucPreProcess1.FilePath);
                }
                //}
            }
        }

        private void txt_XINCR_TextChanged(object sender, EventArgs e)
        {
            double ll_gen = 0.0;
            double Xincr = MyList.StringToDouble(txt_XINCR.Text, 0.0);

            txt_LL_load_gen.Text = (L / Xincr + 1).ToString("f0");
        }

        private void btn__Loadings_help_Click(object sender, EventArgs e)
        {
            string load_help = Path.Combine(Application.StartupPath, "\\ASTRAHelp\\AASHTO DESIGN LRFD Truck Load.pdf");
            load_help = Path.Combine(Application.StartupPath, @"ASTRAHelp\AASHTO DESIGN LRFD Truck Load.pdf");
            if (File.Exists(load_help)) System.Diagnostics.Process.Start(load_help);
        }

        private void btn_edit_load_combs_IRC_Click(object sender, EventArgs e)
        {

            LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
            ff.Owner = this;
            ff.ShowDialog();
        }

        private void btn_long_restore_ll_IRC_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_long_restore_ll_IRC.Name)
                    Default_Moving_LoadData(dgv_long_liveloads);
            }
        }


    }
    public class AASHTO_Box_Section
    {
        public double b1, d1;
        public double b2, d2;
        public double b3, d3;
        public double b4, d4;
        public double b5, d5;
        public double b6, d6;
        public double b7, d7;
        public double b8, d8;
        public double b9, d9;
        public double b10, d10;

        public double D;

        public int Cell_Nos;
        public AASHTO_Box_Section()
        {
            b1 = 465.0;
            d1 = 8.0;

            b2 = 12.0;
            d2 = 52.0;

            b3 = 417.38;
            d3 = 6;

            b4 = 31.5;
            d4 = 9;

            b5 = 31.5;
            d5 = 3;

            b6 = 12.0;
            d6 = 52.6;

            b7 = 12.0;
            d7 = 8;

            b8 = 12.0;
            d8 = 4;

            b9 = 9.31;
            d9 = 6;

            b10 = 2.69;
            d10 = 6.0;

            D = 542.0;
        }

        public double theta_radian { get; set; }

        #region Section 1
        public double Ax1
        {
            get
            {
                double a = 0.0;
                a = b1 * d1;
                return a;
            }
        }
        public double Ix1
        {
            get
            {
                double ix = 0.0;
                ix = b1 * Math.Pow(d1, 3.0) / 12;
                return ix;
            }
        }
        public double Iy1
        {
            get
            {
                double iy = 0.0;
                iy = d1 * Math.Pow(b1, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 1


        #region Section 2
        public double Ax2
        {
            get
            {
                double a = 0.0;
                a = b2 * d2;
                return a;
            }
        }
        public double Ix2
        {
            get
            {
                double ix = 0.0;
                ix = b2 * Math.Pow(d2, 3.0) / 12;
                return ix;
            }
        }
        public double Iy2
        {
            get
            {
                double iy = 0.0;
                iy = d2 * Math.Pow(b2, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 2


        #region Section 3
        public double Ax3
        {
            get
            {
                double a = 0.0;
                a = b3 * d3;
                return a;
            }
        }
        public double Ix3
        {
            get
            {
                double ix = 0.0;
                ix = b3 * Math.Pow(d3, 3.0) / 12;
                return ix;
            }
        }
        public double Iy3
        {
            get
            {
                double iy = 0.0;
                iy = d3 * Math.Pow(b3, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 3


        #region Section 4
        public double Ax4
        {
            get
            {
                double a = 0.0;
                a = b4 * d4;
                return a;
            }
        }
        public double Ix4
        {
            get
            {
                double ix = 0.0;
                ix = b4 * Math.Pow(d4, 3.0) / 12;
                return ix;
            }
        }
        public double Iy4
        {
            get
            {
                double iy = 0.0;
                iy = d4 * Math.Pow(b4, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 3



        #region Section 5
        public double Ax5
        {
            get
            {
                double a = 0.0;
                a = 0.5 * b5 * d5;
                return a;
            }
        }
        public double Ix5
        {
            get
            {
                double ix = 0.0;
                ix = b5 * Math.Pow(d5, 3.0) / 36.0;
                return ix;
            }
        }
        public double Iy5
        {
            get
            {
                double iy = 0.0;
                iy = d5 * Math.Pow(b5 / 2, 3.0) / 36;
                return iy;
            }
        }
        #endregion Section 5

        #region Section 6

        public double Ax6
        {
            get
            {
                double a = 0.0;


                a = 2 * (d2 - d3) * (b6 / Math.Cos(theta_radian));

                return a;
            }
        }
        public double Ix6
        {
            get
            {
                double ix = 0.0;
                //ix = (b6/Math.Cos(theta_radian))  * Math.Pow((d2-d3, 3.0) / 36.0;
                ix = Math.Pow(((b6 / Math.Cos(theta_radian)) * d2 - d3), 3.0) / 6;
                return ix;
            }
        }
        public double Iy6
        {
            get
            {
                double iy = 0.0;
                iy = (Math.Pow((d2 - d3) * (b6 / Math.Cos(theta_radian)), 3.0) / 6.0) +
                    Ax6 * Math.Pow(((D / 2) - b4 - (d2 - d3) * (Math.Tan(theta_radian) / 2) - (b6 / 2) / Math.Cos(theta_radian)), 2.0);
                return iy;
            }
        }
        #endregion Section 6



        #region Section 7
        public double Ax7
        {
            get
            {
                double a = 0.0;
                a = b7 * d7;
                return a;
            }
        }
        public double Ix7
        {
            get
            {
                double ix = 0.0;
                ix = b7 * Math.Pow(d7, 3.0) / 12;
                return ix;
            }
        }
        public double Iy7
        {
            get
            {
                double iy = 0.0;
                iy = d7 * Math.Pow(b7, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 7




        #region Section 8
        public double Ax8
        {
            get
            {
                double a = 0.0;
                a = 0.5 * b8 * d8;
                return a;
            }
        }
        public double Ix8
        {
            get
            {
                double ix = 0.0;
                ix = b8 * Math.Pow(d8, 3.0) / 36;
                return ix;
            }
        }
        public double Iy8
        {
            get
            {
                double iy = 0.0;
                iy = d8 * Math.Pow(b8 / 2, 3.0) / 36;
                return iy;
            }
        }
        #endregion Section 8




        #region Section 9
        public double Ax9
        {
            get
            {
                double a = 0.0;
                a = b9 * d9;
                return a;
            }
        }
        public double Ix9
        {
            get
            {
                double ix = 0.0;
                ix = b9 * Math.Pow(d9, 3.0) / 12;
                return ix;
            }
        }
        public double Iy9
        {
            get
            {
                double iy = 0.0;
                iy = d9 * Math.Pow(b9, 3.0) / 12;
                return iy;
            }
        }
        #endregion Section 9


        #region Section 10
        public double Ax10
        {
            get
            {
                double a = 0.0;
                a = 0.5 * b10 * d10;
                return a;
            }
        }
        public double Ix10
        {
            get
            {
                double ix = 0.0;
                ix = b10 * Math.Pow(d10, 3.0) / 36;
                return ix;
            }
        }
        public double Iy10
        {
            get
            {
                double iy = 0.0;
                iy = d10 * Math.Pow(b10 / 2, 3.0) / 36;
                return iy;
            }
        }
        #endregion Section 10



        public double AX
        {
            get
            {
                return Ax1 + (Cell_Nos)* Ax2 + Ax3 + Ax4 + Ax5 + Ax6 + Ax7 + Ax8 + Ax9 + Ax10;
            }
        }
        public double IXX
        {
            get
            {
                return Ix1 + Ix2 + Ix3 + Ix4 + Ix5 + Ix6 + Ix7 + Ix8 + Ix9 + Ix10;
            }
        }

        public double IYY
        {
            get
            {
                return Iy1 + Iy2 + Iy3 + Iy4 + Iy5 + Iy6 + Iy7 + Iy8 + Iy9 + Iy10;
            }
        }
        public double IZZ
        {
            get
            {
                return IXX + IYY;
            }
        }
    }

    public class PSC_BoxGirderAnalysis
    {


        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Bridge_Analysis = null;
        public List<BridgeMemberAnalysis> All_Analysis = null;
        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        public bool Is_MultipleCell { get; set; }

        public double Load1 { get; set; }
        public double Load2 { get; set; }
        public double Load3 { get; set; }
        public string Start_Support { get; set; }
        public string End_Support { get; set; }
        string input_file, working_folder, user_path;
        public PSC_BoxGirderAnalysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
            Support_Distance = 0.5;
            Is_MultipleCell = true;
            All_Analysis = new List<BridgeMemberAnalysis>();
            Spans = new List<double>();

            Load1 = 0.675;
            Load2 = 0.169;
            Load3 = 0.098;

            Skew_Angle = 0.0;

        }

        #region Properties

        public double Length { get; set; }
        public List<double> Spans { get; set; }
        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }
        public int Total_Rows
        {
            get
            {
                //return (int)(((WidthBridge - (WidthCantilever)) / Spacing_Long_Girder) + 1);
                return 11;
            }
        }
        public int Total_Columns
        {
            get
            {
                //return (int)(((Length - (Effective_Depth)) / Spacing_Cross_Girder) + 5);
                //return (int)(((Length) / Spacing_Cross_Girder) + 2);
                return 11;
            }
        }
        public double Skew_Angle { get; set; }
        //Chiranjit [2011 10 17]  distance from end to support
        public double Support_Distance { get; set; }

        public double WidthCantilever { get; set; }
        public double Spacing_Long_Girder
        {
            get
            {
                return MyList.StringToDouble(((WidthBridge - (2 * WidthCantilever)) / 6.0).ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //return MyList.StringToDouble(txt_cross_girder_spacing.Text, 0.0);
                return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);
            }
        }


        #endregion Properties

        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(working_folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(working_folder, "ANALYSIS_REP.TXT");
            }
        }
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                try
                {
                    input_file = value;
                    working_folder = Path.GetDirectoryName(input_file);
                    user_path = working_folder;
                }
                catch (Exception ex) { }
            }
        }
        public int NoOfInsideJoints
        {
            get
            {
                //return MyList.StringToInt(txt_cd_total_joints.Text, 0);
                return 1;
            }
        }

        public void CreateData()
        {
            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;

            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();
            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = 12.1;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d, L_8;
            double x_max, x_min;

            last_x = 0.0;

            double last_ll = 0.0;
            double last_len = 0.0;
            int i = 0;

            for (i = 0; i < Spans.Count; i++)
            {
                last_ll = Spans[i];
                if (i > 0) last_len += last_ll;

                #region Chiranjit [2011 09 23] Correct Create Data


                //Start
                list_x.Add(last_len + 0.0);

                last_x = last_ll;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                //Support 
                last_x = Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);
                //Support 
                last_x = last_ll - Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                //Deff 
                last_x = Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                last_x = last_ll - Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);



                // L/8 
                last_x = last_ll / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                // L/8 
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);




                // L/4 
                last_x = last_ll / 4.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                // L/4 
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);


                // 3L/8 
                last_x = 3 * last_ll / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);


                last_x = last_ll / 2.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                #endregion Chiranjit [2011 09 23] Correct Create Data

            }
            if (false)
            {
                #region Chiranjit [2011 09 23] Correct Create Data

                list_x.Clear();

                //Start
                list_x.Add(0.0);

                last_x = Length;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                //Support 
                last_x = Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);
                //Support 
                last_x = Length - Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                //Deff 
                last_x = Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                last_x = Length - Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);



                // L/8 
                last_x = Length / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                // L/8 
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);




                // L/4 
                last_x = Length / 4.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                // L/4 
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);


                // 3L/8 
                last_x = 3 * Length / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);


                last_x = Length / 2.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);





                #endregion Chiranjit [2011 09 23] Correct Create Data

            }


            //last_x = x_incr;

            bool flag = true;

            list_x.Sort();


            list_z.Add(0);
            last_z = WidthCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = WidthCantilever / 2;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = WidthBridge - WidthCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);
            last_z = WidthBridge - (WidthCantilever / 2.0);
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);


            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = WidthCantilever + z_incr;
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

                if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
                    list_z.Add(last_z);
                last_z += z_incr;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);

            list_z.Sort();
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

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                }
            }

            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
        }
        public void CreateData_DeadLoad()
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

            double val1 = 12.1;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d, L_8;
            double x_max, x_min;

            last_x = 0.0;

            double last_ll = 0.0;

            double last_len = 0.0;
            list_x.Clear();

            int i = 0;
            bool flag = true;

            for (i = 0; i < Spans.Count; i++)
            {
                last_ll = Spans[i];
                if (i > 0) last_len += last_ll;

                #region Chiranjit [2011 09 23] Correct Create Data


                //Start
                list_x.Add(last_len + 0.0);

                last_x = last_ll;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                //Support 
                last_x = Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);
                //Support 
                last_x = last_ll - Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                //Deff 
                last_x = Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                last_x = last_ll - Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);



                // L/8 
                last_x = last_ll / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                // L/8 
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);




                // L/4 
                last_x = last_ll / 4.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                // L/4 
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);


                // 3L/8 
                last_x = 3 * last_ll / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);
                last_x = last_ll - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);


                last_x = last_ll / 2.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_len + last_x);

                #endregion Chiranjit [2011 09 23] Correct Create Data

            }
            if (false)
            {
                #region Chiranjit [2011 09 23] Correct Create Data


                //Start
                list_x.Add(0.0);

                last_x = Length;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                //Support 
                last_x = Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);
                //Support 
                last_x = Length - Support_Distance;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                //Deff 
                last_x = Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                last_x = Length - Effective_Depth;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);



                // L/8 
                last_x = Length / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                // L/8 
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);




                // L/4 
                last_x = Length / 4.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);

                // L/4 
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);


                // 3L/8 
                last_x = 3 * Length / 8.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);
                last_x = Length - last_x;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);


                last_x = Length / 2.0;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
                list_x.Add(last_x);








                #endregion Chiranjit [2011 09 23] Correct Create Data
            }


            list_x.Sort();


            list_z.Add(0);
            last_z = WidthCantilever;

            list_z.Sort();

            list_x.Sort();

            _Columns = list_x.Count;
            _Rows = list_z.Count;

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

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }
            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                }
            }


            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
        }


        public List<string> Get_JointCoordinates()
        {
            List<string> list = new List<string>();

            string kStr = "";
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            //list.Add("UNIT METER MKip");
            list.Add("UNIT FT KIP");
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

            return list;
        }

        public List<string> Get_SectionProperties(PSC_Box_Section_Data psc_section)
        {
            List<string> list = new List<string>();

            list.Add("SECTION PROPERTIES");

            list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));
            //Support
            list.Add(string.Format("131 143 155 167 179 191 203 215 227 239 251  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("141 153 165 177 189 201 213 225 237 249 261  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("132 144 156 168 180 192 204 216 228 240 252  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("142 154 166 178 190 202 214 226 238 250 262  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));

            //L/2
            list.Add(string.Format("133 TO 140  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("145 TO 152  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("157 TO 164 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("169 TO 176 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("181 TO 188 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("193 TO 200 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("205 TO 212 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("217 TO 224 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("229 TO 236 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("241 TO 248 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("253 TO 260 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));


            list.Add("MATERIAL CONSTANT");
            list.Add("E CONCRETE ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            string support_left_joints = "12 13 14 15 16 17 18 19 20 21 22";
            string support_right_joints = "122 123 124 125 126 127 128 129 130 131 132";

            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0} {1}", support_right_joints, End_Support));
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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            return list;

        }


        public void WriteData_LiveLoad(string file_name, PSC_Box_Section_Data psc_section)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            //list.Add("UNIT METER MKip");
            list.Add("UNIT FT KIP");
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
            //list.Add("191 TO 226 215 TO 226 239 TO 262  PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //list.Add("181 TO 188 205 TO 212 PRIS AX 1.146 IX 0.022 IZ 0.187 ");
            //list.Add("180 189 204 213 PRIS AX 0.7001 IX 0.0442 IZ 0.105 ");
            //list.Add("179 190 203 214 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            //list.Add("157 TO 164 229 TO 236 PRIS AX 1.215 IX 0.023 IZ 0.192 ");
            //list.Add("155 166 227 238 PRIS AX 1.2407 IX 0.0698 IZ 0.181 ");
            //list.Add("156 165 228 237 PRIS AX 0.7897 IX 0.0461 IZ 0.115 ");
            //list.Add("61 TO 70 PRIS AX 0.385 IX 0.008 IZ 0.277");
            //list.Add("51 TO 60 71 TO 80 PRIS AX 0.523 IX 0.010 IZ 0.003");
            //list.Add("41 TO 50 81 TO 90 PRIS AX 0.406 IX 0.008 IZ 0.002");
            //list.Add("31 TO 30 91 TO 100 PRIS AX 0.482 IX 0.009 IZ 0.003");
            //list.Add("21 TO 30 101 TO 110  PRIS AX 0.482 IX 0.009 IZ 0.003");
            //list.Add("11 TO 20 111 TO 120 131 TO 154 167 TO 178  PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //list.Add("1 TO 10 121 TO 130  PRIS AX 0.001 IX 0.0001 IZ 0.0001");


            //list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));
            list.Add(string.Format(
            Cross_Girder_Members_Array[0, 0].MemberNo +
                    " TO " +
                Cross_Girder_Members_Array[_Rows - 2, _Columns - 1].MemberNo

                + "  PRIS AX 0.001 IX 0.0001 IZ 0.0001"));

            //Support
            //list.Add(string.Format("131 143 155 167 179 191 203 215 227 239 251  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            //list.Add(string.Format("141 153 165 177 189 201 213 225 237 249 261  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            //list.Add(string.Format("132 144 156 168 180 192 204 216 228 240 252  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            //list.Add(string.Format("142 154 166 178 190 202 214 226 238 250 262  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));

            ////L/2
            //list.Add(string.Format("133 TO 140  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("145 TO 152  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("157 TO 164 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("169 TO 176 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("181 TO 188 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("193 TO 200 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("205 TO 212 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("217 TO 224 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("229 TO 236 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("241 TO 248 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            //list.Add(string.Format("253 TO 260 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));

            
          //_Columns, _Rows

            list.Add(string.Format(Long_Girder_Members_Array[0,0].MemberNo +
                    " TO " + 
                Long_Girder_Members_Array[_Rows-1,_Columns - 2].MemberNo

                + " PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));

            //list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));


            list.Add("MATERIAL CONSTANT");
            list.Add("E CONCRETE ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            string support_left_joints = "12 13 14 15 16 17 18 19 20 21 22";
            string support_right_joints = "122 123 124 125 126 127 128 129 130 131 132";

            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0} {1}", support_right_joints, End_Support));
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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(working_folder, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));

        }

        public void WriteData_LiveLoad(string file_name, AASHTO_Box_Section psc_section)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            //list.Add("UNIT METER MKip");
            list.Add("UNIT FT KIP");
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
            //list.Add("191 TO 226 215 TO 226 239 TO 262  PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //list.Add("181 TO 188 205 TO 212 PRIS AX 1.146 IX 0.022 IZ 0.187 ");
            //list.Add("180 189 204 213 PRIS AX 0.7001 IX 0.0442 IZ 0.105 ");
            //list.Add("179 190 203 214 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            //list.Add("157 TO 164 229 TO 236 PRIS AX 1.215 IX 0.023 IZ 0.192 ");
            //list.Add("155 166 227 238 PRIS AX 1.2407 IX 0.0698 IZ 0.181 ");
            //list.Add("156 165 228 237 PRIS AX 0.7897 IX 0.0461 IZ 0.115 ");
            //list.Add("61 TO 70 PRIS AX 0.385 IX 0.008 IZ 0.277");
            //list.Add("51 TO 60 71 TO 80 PRIS AX 0.523 IX 0.010 IZ 0.003");
            //list.Add("41 TO 50 81 TO 90 PRIS AX 0.406 IX 0.008 IZ 0.002");
            //list.Add("31 TO 30 91 TO 100 PRIS AX 0.482 IX 0.009 IZ 0.003");
            //list.Add("21 TO 30 101 TO 110  PRIS AX 0.482 IX 0.009 IZ 0.003");
            //list.Add("11 TO 20 111 TO 120 131 TO 154 167 TO 178  PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            //list.Add("1 TO 10 121 TO 130  PRIS AX 0.001 IX 0.0001 IZ 0.0001");


            list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));
            //Support
            list.Add(string.Format("131 TO 260 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.AX/144, psc_section.IXX / (12 * 12 * 12 * 12), psc_section.IYY / (12 * 12 * 12 * 12), (psc_section.IXX + psc_section.IYY) / (12 * 12 * 12 * 12)));


            list.Add("MATERIAL CONSTANT");
            list.Add("E CONCRETE ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            string support_left_joints = "12 13 14 15 16 17 18 19 20 21 22";
            string support_right_joints = "122 123 124 125 126 127 128 129 130 131 132";

            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0} {1}", support_right_joints, End_Support));
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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(working_folder, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));

        }

        public void WriteData_LiveLoad(string file_name, PSC_Box_Section_Data psc_section, List<string> ll_loads)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            list.Add("UNIT FT KIP");
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

            list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));
            //Support
            list.Add(string.Format("131 143 155 167 179 191 203 215 227 239 251  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("141 153 165 177 189 201 213 225 237 249 261  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("132 144 156 168 180 192 204 216 228 240 252  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            list.Add(string.Format("142 154 166 178 190 202 214 226 238 250 262  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));

            //L/2
            list.Add(string.Format("133 TO 140  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("145 TO 152  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("157 TO 164 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("169 TO 176 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("181 TO 188 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("193 TO 200 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("205 TO 212 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("217 TO 224 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("229 TO 236 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("241 TO 248 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add(string.Format("253 TO 260 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
           
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");

          
            string support_left_joints = "12 13 14 15 16 17 18 19 20 21 22";
            string support_right_joints = "122 123 124 125 126 127 128 129 130 131 132";

            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0} {1}", support_right_joints, End_Support));

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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            if (ll_loads.Count > 0)
                File.WriteAllLines(MyList.Get_LL_TXT_File(file_name), ll_loads.ToArray());
        }

        public void WriteData_LiveLoad(string file_name, AASHTO_Box_Section psc_section, List<string> ll_loads)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            list.Add("UNIT FT KIP");
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


            list.Add(string.Format("1 TO 130   PRIS AX 0.001 IX 0.0001 IZ 0.0001"));
            //Support
            list.Add(string.Format("131 TO 260 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}", psc_section.AX / 144, psc_section.IXX / (12 * 12 * 12 * 12), psc_section.IYY / (12 * 12 * 12 * 12), (psc_section.IZZ) / (12 * 12 * 12 * 12)));

            list.Add("MATERIAL CONSTANT");
            list.Add("E CONCRETE ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");


            string support_left_joints = "12 13 14 15 16 17 18 19 20 21 22";
            string support_right_joints = "122 123 124 125 126 127 128 129 130 131 132";




            list.Add(string.Format("{0} {1}", support_left_joints, Start_Support));
            string support_inner_joints = "";

            var ls = MyList.Get_Array_Intiger("12 TO 22");
            if (Spans.Count > 1)
            {
                var len = 0.0;
                for (i = 0; i < Spans.Count - 1; i++)
                {

                    for (int j = 0; j < ls.Count; j++)
                    {
                        ls[j] = ls[j] + 110;
                    }
                    support_inner_joints = MyList.Get_Array_Text(ls);
                    if (i < Spans.Count - 1)
                        list.Add(string.Format("{0} {1}", support_inner_joints, End_Support));

                }

                //support_right_joints = support_inner_joints;
            }
            ls.Clear();

            for (i = 0; i < _Rows; i++)
            {
                ls.Add(Joints_Array[i, _Columns - 2].NodeNo);
                
            }
            support_right_joints = MyList.Get_Array_Text(ls);

            list.Add(string.Format("{0} {1}", support_right_joints, Start_Support));

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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            if (ll_loads.Count > 0)
                File.WriteAllLines(MyList.Get_LL_TXT_File(file_name), ll_loads.ToArray());
        }

        public void WriteData_DeadLoad(string file_name, PSC_Box_Section_Data psc_section)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
             
            list.Add("UNIT FT KIP");
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
            //list.Add("1 13 PRIS AX 6.1997 IX 3.1639 IY 35.886 IZ 35.886");
            //list.Add("2 12 PRIS AX 7.099 IX 6.062 IY 35.886 IZ 35.886");
            //list.Add("3 11 PRIS AX 6.008 IX 5.034 IY 34.516 IZ 34.516");
            //list.Add("4 10 PRIS AX 5.6081 IX 4.886 IY 32.772 IZ 32.772");
            //list.Add("5 9  PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");
            //list.Add("6 8  PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");
            //list.Add("7 PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");

            //Chiranjit [2012 11 01]
            //Support
            //list.Add(string.Format("1 13 PRIS AX 6.1997 IX 3.1639 IY 35.886 IZ 35.886"));
            //list.Add(string.Format("1 2 11 12 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            //list.Add(string.Format("2 12 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            ////D eff
            //list.Add(string.Format("3 11 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F2, psc_section.Ixx.F2, psc_section.Iyy.F2, psc_section.Izz.F2));
            ////L/8
            //list.Add(string.Format("4 10 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F3, psc_section.Ixx.F3, psc_section.Iyy.F3, psc_section.Izz.F3));
            ////L/4
            //list.Add(string.Format("5 9  PRISAX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F4, psc_section.Ixx.F4, psc_section.Iyy.F4, psc_section.Izz.F4));
            ////3L/8
            //list.Add(string.Format("6 8  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F5, psc_section.Ixx.F5, psc_section.Iyy.F5, psc_section.Izz.F5));
            //L/2
            list.Add(string.Format("1 TO " + MemColls.Count + " PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
                psc_section.Area.F6, psc_section.Ixx.F6, psc_section.Iyy.F6, psc_section.Izz.F6));
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORTS");
            //list.Add("2 PINNED");
            //list.Add("12 FIXED BUT FX MZ");




            list.Add(string.Format("{0} {1}", Joints[1].NodeNo, Start_Support));

            if (Spans.Count > 1)
            {
                for (i = 1; i < Spans.Count - 1; i++)
                {
                    var jn = Joints.GetJoints(Spans[i], 0, 0);
                    list.Add(string.Format("{0} {1}", jn.NodeNo, End_Support));

                }
            }


            list.Add(string.Format("****SELF WEIGHT {0} K/FT*****", Load1));
            list.Add(string.Format("LOAD 1 DEAD LOAD"));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO 12 UNI GY {0:f3} ", Load1));
            list.Add(string.Format("**** SUPERIMPOSED DEAD LOAD {0:f3} K/FT****", Load2));
            list.Add(string.Format("**** FootPath Liveload {0:f3} kip/ft Per Footpat*****", Load3));
            list.Add(string.Format("LOAD 2 SIDL+FPLL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TO 12 UNI GY {0:f3}", Load2));
            list.Add(string.Format("1 TO 12 UNI GY {0:f3}", Load3));

            //list.Add("LOAD 1 DEAD LOAD");
            //list.Add("MEMBER LOAD ");
            //list.Add("****SELF WEIGHT-25.0 T/M*****");
            //list.Add("1 TO 13 UNI GY -25.0 ");
            //list.Add("****SUPERIMPOSED DEAD LOAD -8.2 T/M*****");
            //list.Add("LOAD 2 SIDL");
            //list.Add("MEMBER LOAD");
            //list.Add("1 TO 13 UNI GY -8.2");
            //list.Add("******FootPath Liveload 490 kg/m^2 Per Footpath*****");
            //list.Add("LOAD 3 FPLL");
            //list.Add("MEMBER LOAD");
            //list.Add("1 TO 13 UNI GY -0.98");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");


            list.Add(kStr);

            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(working_folder, true, iApp.DesignStandard);


            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
        }

        public void WriteData_DeadLoad(string file_name, AASHTO_Box_Section psc_section)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC BOX GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");

            list.Add("UNIT FT KIP");
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
            //list.Add("1 13 PRIS AX 6.1997 IX 3.1639 IY 35.886 IZ 35.886");
            //list.Add("2 12 PRIS AX 7.099 IX 6.062 IY 35.886 IZ 35.886");
            //list.Add("3 11 PRIS AX 6.008 IX 5.034 IY 34.516 IZ 34.516");
            //list.Add("4 10 PRIS AX 5.6081 IX 4.886 IY 32.772 IZ 32.772");
            //list.Add("5 9  PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");
            //list.Add("6 8  PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");
            //list.Add("7 PRIS AX 4.966 IX 4.676 IY 29.546 IZ 29.546");

            //Chiranjit [2012 11 01]
            //Support
            //list.Add(string.Format("1 13 PRIS AX 6.1997 IX 3.1639 IY 35.886 IZ 35.886"));
            //list.Add(string.Format("1 2 11 12 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.AX / (Math.Pow(12, 2)), psc_section.IXX / (Math.Pow(12, 4)), psc_section.IYY / (Math.Pow(12, 4)), psc_section.IZZ / (Math.Pow(12, 4))));
            ////list.Add(string.Format("2 12 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F1, psc_section.Ixx.F1, psc_section.Iyy.F1, psc_section.Izz.F1));
            ////D eff
            //list.Add(string.Format("3 11 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F2, psc_section.Ixx.F2, psc_section.Iyy.F2, psc_section.Izz.F2));
            ////L/8
            //list.Add(string.Format("4 10 PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F3, psc_section.Ixx.F3, psc_section.Iyy.F3, psc_section.Izz.F3));
            ////L/4
            //list.Add(string.Format("5 9  PRISAX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F4, psc_section.Ixx.F4, psc_section.Iyy.F4, psc_section.Izz.F4));
            ////3L/8
            //list.Add(string.Format("6 8  PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
            //    psc_section.Area.F5, psc_section.Ixx.F5, psc_section.Iyy.F5, psc_section.Izz.F5));
            //L/2
            list.Add(string.Format("1 TO " + MemColls.Count + " PRIS AX {0:f4} IX {1:f4} IY {2:f4} IZ {3:f4}",
                psc_section.AX / (Math.Pow(12, 2)), psc_section.IXX / (Math.Pow(12, 4)), psc_section.IYY / (Math.Pow(12, 4)), psc_section.IZZ / (Math.Pow(12, 4))));
            list.Add("MATERIAL CONSTANT");
            list.Add("E CONCRETE ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORTS");
            //list.Add("2 PINNED");
            //list.Add("12 FIXED BUT FX MZ");


            list.Add(string.Format("{0} {1}", Joints[1].NodeNo, Start_Support));

            if (Spans.Count > 1)
            {
                for (i = 0; i < Spans.Count - 1; i++)
                {
                    var jn = Joints.GetJoints(Spans[i], 0, 0);
                    list.Add(string.Format("{0} {1}", jn.NodeNo, End_Support));

                }
            }

            list.Add(string.Format("{0} {1}", Joints[Joints.Count - 2].NodeNo, Start_Support));


            list.Add(string.Format("****SELF WEIGHT {0} K/FT*****", Load1));
            list.Add(string.Format("LOAD 1 DEAD LOAD"));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO {0} UNI GY {1:f3} ", MemColls.Count, Load1));
            list.Add(string.Format("**** SUPERIMPOSED DEAD LOAD {0:f3} K/FT****", Load2));
            list.Add(string.Format("**** FootPath Liveload {0:f3} kip/ft Per Footpat*****", Load3));
            list.Add(string.Format("LOAD 2 SIDL+FPLL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TO {0} UNI GY {1:f3}", MemColls.Count, Load2));
            list.Add(string.Format("1 TO {0} UNI GY {1:f3}", MemColls.Count, Load3));
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");


            list.Add(kStr);

            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(working_folder, true, iApp.DesignStandard);


            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
        }

        
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList = new List<LoadData>();
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
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 0.0);

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
                        LoadList.Add(ld);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        List<string> Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            double z_min = double.MaxValue;
            double x = double.MaxValue;
            int indx = -1;

            int i = 0;
            int j = 0;

            List<double> list_z = new List<double>();
            List<string> list_arr = new List<string>();

            List<MemberCollection> list_mc = new List<MemberCollection>();

            double last_z = 0.0;

            while (mc.Count != 0)
            {
                indx = -1;
                for (i = 0; i < mc.Count; i++)
                {
                    if (z_min > mc[i].StartNode.Z)
                    {
                        z_min = mc[i].StartNode.Z;
                        indx = i;
                    }
                }
                if (indx != -1)
                {

                    if (!list_z.Contains(z_min))
                        list_z.Add(z_min);

                    sort_membs.Add(mc[indx]);
                    mc.Members.RemoveAt(indx);
                    z_min = double.MaxValue;
                }
            }

            last_z = -1.0;

            //Inner & Outer Long Girder
            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            z_min = Bridge_Analysis.Analysis.Joints.MinZ;
            double z_max = Bridge_Analysis.Analysis.Joints.MaxZ;


            //Store inner and outer Long Girder
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    outer_long.Add(sort_membs[i]);
                }
                else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    inner_long.Add(sort_membs[i]);
                }
            }

            List<int> Outer_Joints = new List<int>();
            List<int> Inner_Joints = new List<int>();

            for (i = 0; i < outer_long.Count; i++)
            {
                if (Outer_Joints.Contains(outer_long[i].EndNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].EndNode.NodeNo);
                if (Outer_Joints.Contains(outer_long[i].StartNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].StartNode.NodeNo);
            }

            for (i = 0; i < inner_long.Count; i++)
            {
                if (Inner_Joints.Contains(inner_long[i].EndNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].EndNode.NodeNo);
                if (Inner_Joints.Contains(inner_long[i].StartNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].StartNode.NodeNo);
            }
            Outer_Joints.Sort();
            Inner_Joints.Sort();


            string inner_long_text = "";
            string outer_long_text = "";
            int last_val = 0;
            int to_val = 0;
            int from_val = 0;

            last_val = Outer_Joints[0];
            from_val = last_val;
            bool flag_1 = false;
            for (i = 0; i < Outer_Joints.Count; i++)
            {
                if (i < Outer_Joints.Count - 1)
                {
                    if ((Outer_Joints[i] + 1) == (Outer_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Outer_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Outer_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            outer_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            outer_long_text = outer_long_text + " " + last_val;
                        }
                    }
                    last_val = Outer_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        outer_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        outer_long_text = outer_long_text + " " + last_val;
                    }
                }
            }

            for (i = 0; i < Inner_Joints.Count; i++)
            {
                if (i < Inner_Joints.Count - 1)
                {
                    if ((Inner_Joints[i] + 1) == (Inner_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Inner_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Inner_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            inner_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            inner_long_text = inner_long_text + " " + last_val;
                        }
                    }
                    last_val = Inner_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        inner_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        inner_long_text = inner_long_text + " " + last_val;
                    }
                }
            }
            list_arr.Add(inner_long_text + " FY  -" + load.ToString("0.000"));
            list_arr.Add(outer_long_text + " FY  -" + (load / 2.0).ToString("0.000"));

            return list_arr;
        }

        internal string GetAnalysis_Input_File(int p)
        {
            return "";
        }
    }



    public class PSC_Box_Segmental_Girder
    {
        IApplication iApp;
        public string rep_file_name = "";
        public string file_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;


        #region Variable Declaration
        //
        //
        /// Chiranjit [2012 10 26]
        public double Area_Zone1_Outer { get; set; }
        public double Area_Zone2_Outer { get; set; }
        public double Area_Zone3_Outer { get; set; }
        public double Area_Zone1_Inner { get; set; }
        public double Area_Zone2_Inner { get; set; }
        public double Area_Zone3_Inner { get; set; }

        /// <summary>
        /// Chiranjit [2012 10 17]
        /// Residual Shrinkage at 56 Days
        /// </summary>
        public double rss_56 { get; set; }
        /// <summary>
        /// Chiranjit [2012 10 17]
        /// Residual Shrinkage at 14 Days
        /// </summary>
        public double rss_14 { get; set; }
        public double Resh56 { get; set; }
        public double Crst56 { get; set; }

        //Chiranjit [2012 10 18]
        public double fc_temp14 { get; set; }
        public double ft_temp14 { get; set; }
        public double fc_temp28 { get; set; }
        public double ft_temp28 { get; set; }
        public double ttv { get; set; }
        public double fc_serv { get; set; }
        public double Modrup { get; set; }
        public double fc_fact { get; set; }
        public double tv { get; set; }
        public double ttu { get; set; }


        public double Lo { get; set; }
        public double L1 { get; set; }
        public double L2 { get; set; }
        public double exg { get; set; }
        //public double L { get; set; }
        public double Dw { get; set; }
        public double D { get; set; }
        public double Fcu { get; set; }
        public double Tab1_Fy { get; set; }
        public double act { get; set; }
        public double mct { get; set; }
        public double sct { get; set; }
        public double acsidl { get; set; }
        public double mtcsidl { get; set; }
        public double T_loss { get; set; }
        public double wct { get; set; }
        public double ds { get; set; }
        public double df { get; set; }
        public double bt { get; set; }

        public double FactDL { get; set; }
        public double FactSIDL { get; set; }
        public double FactLL { get; set; }

        public double alpha { get; set; }


        public double Tr1 { get; set; }
        public double Tr2 { get; set; }
        public double Tr3 { get; set; }


        public double Tf1 { get; set; }
        public double Tf2 { get; set; }
        public double Tf3 { get; set; }
        public double Tf4 { get; set; }



        public double ccbg_day { get; set; }
        public double ccbg_fcj { get; set; }

        public double fsp_day { get; set; }
        public double fsp_fcj { get; set; }

        public double cwccb_day { get; set; }
        public double cwccb_fcj { get; set; }




        public double ND { get; set; }
        public double NA { get; set; }
        public double Pu { get; set; }
        public double Tab2_Fy { get; set; }
        public double Fu { get; set; }
        public double Pn { get; set; }
        public double Eps { get; set; }
        public double Pj { get; set; }
        public double s { get; set; }
        //public double µ { get; set; }
        public double mu { get; set; }
        public double k { get; set; }
        public double Re1 { get; set; }
        public double Re2 { get; set; }
        public double td1 { get; set; }
        public double qd { get; set; }
        //public double Fcu  {get; set;} 
        //public double Ec { get; set; }
        public double cover1 { get; set; }
        public double cover2 { get; set; }
        public double Ns { get; set; }

        /// <summary>
        /// Number of Cables at Right Side
        /// </summary>
        public int Nc_Right { get; set; }
        /// <summary>
        /// Cross Section Area of Cables
        /// </summary>
        public int Cable_Area { get; set; }

        /// <summary>
        /// Number of Cables at Left Side
        /// </summary>
        public int Nc_Left { get; set; }




        public double Ec { get { return (5000.0 * Math.Sqrt(Fcu)); } }
        public double L { get { return (Lo - 2 * L1); } }



        //double Ec = 5000 x rt(40) = 31622.8 N/.mm (Mpa)
        //double L = Lo – 2 x L1 = 47.750m.



        //PSC_Force_Data


        public PSC_Force_Data Section_Theta { get; set; }

        public PSC_Force_Data Section_D { get; set; }
        public PSC_Force_Data Section_Dw { get; set; }
        public PSC_Force_Data Section_Td { get; set; }
        public PSC_Force_Data Section_C1 { get; set; }
        public PSC_Force_Data Section_C2 { get; set; }
        public PSC_Force_Data Section_Tip { get; set; }
        public PSC_Force_Data Section_Tp { get { return Section_Tip; } set { Section_Tip = value; } }
        public PSC_Force_Data Section_Tf { get; set; }
        public PSC_Force_Data Section_lw { get; set; }
        public PSC_Force_Data Section_D1 { get; set; }
        public PSC_Force_Data Section_Tw { get; set; }
        public PSC_Force_Data Section_SW { get; set; }
        public PSC_Force_Data Section_Ts { get; set; }
        public PSC_Force_Data Section_D2 { get; set; }
        public PSC_Force_Data Section_K1 { get; set; }
        public PSC_Force_Data Section_K2 { get; set; }
        public PSC_Force_Data Section_HW1 { get; set; }
        public PSC_Force_Data Section_HH1 { get; set; }
        public PSC_Force_Data Section_CH1 { get; set; }
        public PSC_Force_Data Section_HW2 { get; set; }
        public PSC_Force_Data Section_HH2 { get; set; }
        public PSC_Force_Data Section_HW3 { get; set; }
        public PSC_Force_Data Section_HH3 { get; set; }
        #endregion Variable Declaration


        public double L_support { get; set; }
        public double L_Deff { get; set; }
        public double L_8
        {
            get
            {
                return (L / 8.0);
            }
        }
        public double L_4
        {
            get
            {
                return (L / 4.0);
            }
        }
        public double L3_8
        {
            get
            {
                return (L * 3.0) / 8.0;
            }
        }

        public double L_2
        {
            get
            {
                return L / 2.0;
            }
        }

        //Chiranjit [2013 06 19]
        public string Cable_Type { get; set; }
        #region


        public PSC_Force_Data A = new PSC_Force_Data(6);
        public PSC_Force_Data Yt = new PSC_Force_Data(6);
        public PSC_Force_Data Yb = new PSC_Force_Data(6);
        public PSC_Force_Data Zt = new PSC_Force_Data(6);
        public PSC_Force_Data Zb = new PSC_Force_Data(6);
        public PSC_Force_Data A_Yt = new PSC_Force_Data(6);
        public PSC_Force_Data A_YT_Yt = new PSC_Force_Data(6);
        public PSC_Force_Data Iself = new PSC_Force_Data(6);
        public PSC_Force_Data Iself_A_Yt_Yt = new PSC_Force_Data(6);
        public PSC_Force_Data Iyy = new PSC_Force_Data(6);
        public PSC_Force_Data Ixx = new PSC_Force_Data(6);



        #endregion


        public PSC_Box_Segmental_Girder(IApplication iApp)
        {
            this.iApp = iApp;



            #region Variable Declaration
            Lo = 48.750;
            L1 = 0.500;
            L2 = 0.500;
            exg = 0.040;
            // L = Lo – 2 x L1 = 47.750m.
            //L = 47.750;
            Dw = 9.750;
            D = 2.500;
            Fcu = 40;
            act = 14;
            mct = 87;
            sct = 34.8;
            acsidl = 56;
            mtcsidl = 100;
            T_loss = 20;
            wct = 0.065;
            ds = 0.225;
            df = 0.175;
            bt = 1.000;

            FactDL = 1.250;
            FactSIDL = 2.000;
            FactLL = 2.500;

            alpha = 0.0000117;


            Tr1 = 17.8;
            Tr2 = 4.0;
            Tr3 = 2.1;


            Tf1 = 10.6;
            Tf2 = 0.7;
            Tf3 = 0.8;
            Tf4 = 6.6;


            //Prestressing Input Data:   [Tab 2]

            //A)                 Construction Schedule and Prestressing Stages

            //    Job                                                                                                 Day after casting                 fcj   N/sq.mm (Mpa)
            //(i)                Completion of casting of Box Girder                                0                day
            //(ii)                First Stage Prestress                                                                14                day                                fcj14 = 34.80
            //(iii)                Completion of Wearing Course & Crash Barrier                56                day                                fcj56 = 40.00

            //B)                Cable and Prestressing Data

            //D                = 15.200 mm.                                   
            //A = 140.000 Sq.mm.
            //Pu = 1.100 Kg/m
            //Fy = 1670.000 N/Sq.mm. (Mpa)
            //Fu = 1860.000 N/Sq.mm. (Mpa)
            //Pn = 260.700 kN
            //Eps = 195 Gpa
            //Pj = 76.5 %
            //s = 6 mm.
            //µ = 0.17 per radian
            //k = 0.002 per metre
            //Re1 = 35.0 N/Sq.mm (Mpa)
            //Re2 = 0.0 N/Sq.mm (Mpa)
            //td1 = 14 days                     (Taken From Tab 1)
            //qd = 110 mm.
            //Fcu = 40 N/Sq.mm (Mpa)                                                  (Taken  From Tab 1)
            //Ec = 5000 x Sqrt(40) = 31622.8 N/Sq.mm (Mpa)


            ND = 15.200;
            NA = 140.000;
            Pu = 1.100;
            Tab2_Fy = 1670.000;
            Fu = 1860.000;
            Pn = 260.700;
            Eps = 195;
            Pj = 76.5;
            s = 6;
            mu = 0.17;
            k = 0.002;
            Re1 = 35.0;
            Re2 = 0.0;
            td1 = 14;
            qd = 110;
            // Fcu = 40;
            //Ec = 31622.8;
            //double Ec = 5000 x rt(40) = 31622.8 N/.mm (Mpa)
            #endregion Variable Declaration


            Section_Theta = new PSC_Force_Data();
            Section_D = new PSC_Force_Data();
            Section_Dw = new PSC_Force_Data();
            Section_Td = new PSC_Force_Data();
            Section_C1 = new PSC_Force_Data();
            Section_C2 = new PSC_Force_Data();
            Section_Tip = new PSC_Force_Data();
            Section_Tf = new PSC_Force_Data();
            Section_lw = new PSC_Force_Data();
            Section_D1 = new PSC_Force_Data();
            Section_Tw = new PSC_Force_Data();
            Section_SW = new PSC_Force_Data();
            Section_Ts = new PSC_Force_Data();
            Section_D2 = new PSC_Force_Data();
            Section_K1 = new PSC_Force_Data();
            Section_K2 = new PSC_Force_Data();
            Section_HW1 = new PSC_Force_Data();
            Section_HH1 = new PSC_Force_Data();
            Section_CH1 = new PSC_Force_Data();
            Section_HW2 = new PSC_Force_Data();
            Section_HH2 = new PSC_Force_Data();
            Section_HW3 = new PSC_Force_Data();
            Section_HH3 = new PSC_Force_Data();
        }
        public List<string> Get_Step_1(ref PSC_Box_Section_Data psc_setion)
        {
            #region STEP 1 :  Section Properties of various parts in the Cross Section at relevant Sections
            List<string> list = new List<string>();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Cross Section Dimension Input Data :   [Tab 3]"));
            //list.Add(string.Format("----------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 :  Section Properties of various parts in the Cross Section at relevant Sections "));
            list.Add(string.Format("----------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("Refer to Cross Section Diagram"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            string format = "{0,-16:f3} {1,8:f3} {2,12:f3} {3,12:f3} {4,12:f3} {5,12:f3} {6,12:f3} {7,12:f3}";
            string format2 = "{0,-16:f3} {1,8:f3} {2,12:f3}°{3,12:f3}°{4,12:f3}°{5,12:f3}°{6,12:f3}°{7,12:f3}°";
            list.Add(string.Format("TABLE 2: User’s  Cross Section Dimension Input Data "));
            list.Add(string.Format("----------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(108, '-')));
            list.Add(string.Format(format, "", "", "", "D away", "", "", "", ""));
            list.Add(string.Format(format, "SECTION", "", "Support", "from", "L/8", "L/4", "3L/8", "L/2"));
            list.Add(string.Format(format, "", "", "", "support", "", "", "", ""));
            list.Add(string.Format("".PadLeft(108, '-')));
            list.Add(string.Format(format2, "Web inclination", " (θ)deg", Section_Theta[0], Section_Theta[1], Section_Theta[2], Section_Theta[3], Section_Theta[4], Section_Theta[5]));
            list.Add(string.Format("".PadLeft(108, '_')));
            list.Add(string.Format(format, "Total Depth", "D", Section_D[0], Section_D[1], Section_D[2], Section_D[3], Section_D[4], Section_D[5]));
            list.Add(string.Format("".PadLeft(108, '_')));
            list.Add(string.Format(format, "Top Flange", "Dw", Section_Dw[0], Section_Dw[1], Section_Dw[2], Section_Dw[3], Section_Dw[4], Section_Dw[5]));
            list.Add(string.Format(format, "", "Td", Section_Td[0], Section_Td[1], Section_Td[2], Section_Td[3], Section_Td[4], Section_Td[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "Cantilever", "C1", Section_C1[0], Section_C1[1], Section_C1[2], Section_C1[3], Section_C1[4], Section_C1[5]));
            list.Add(string.Format(format, "", "C2", Section_C2[0], Section_C2[1], Section_C2[2], Section_C2[3], Section_C2[4], Section_C2[5]));
            list.Add(string.Format(format, "", "Tp", Section_Tip[0], Section_Tip[1], Section_Tip[2], Section_Tip[3], Section_Tip[4], Section_Tip[5]));
            list.Add(string.Format(format, "", "Tt", Section_Tf[0], Section_Tf[1], Section_Tf[2], Section_Tf[3], Section_Tf[4], Section_Tf[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "Web", "lw", Section_lw[0], Section_lw[1], Section_lw[2], Section_lw[3], Section_lw[4], Section_lw[5]));
            list.Add(string.Format(format, "", "D1", Section_D1[0], Section_D1[1], Section_D1[2], Section_D1[3], Section_D1[4], Section_D1[5]));
            list.Add(string.Format(format, "", "Tw", Section_Tw[0], Section_Tw[1], Section_Tw[2], Section_Tw[3], Section_Tw[4], Section_Tw[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "Soffit Slab", "SW", Section_D[0], Section_D[1], Section_D[2], Section_D[3], Section_D[4], Section_D[5]));
            list.Add(string.Format(format, "", "Ts", Section_Ts[0], Section_Ts[1], Section_Ts[2], Section_Ts[3], Section_Ts[4], Section_Ts[5]));
            list.Add(string.Format(format, "", "D2", Section_D2[0], Section_D2[1], Section_D2[2], Section_D2[3], Section_D2[4], Section_D2[5]));
            list.Add(string.Format(format, "", "K1", Section_K1[0], Section_K1[1], Section_K1[2], Section_K1[3], Section_K1[4], Section_K1[5]));
            list.Add(string.Format(format, "", "K2", Section_K2[0], Section_K2[1], Section_K2[2], Section_K2[3], Section_K2[4], Section_K2[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "t. hnch1", "HW1", Section_HW1[0], Section_HW1[1], Section_HW1[2], Section_HW1[3], Section_HW1[4], Section_HW1[5]));
            list.Add(string.Format(format, "", "HH1", Section_HH1[0], Section_HH1[1], Section_HH1[2], Section_HH1[3], Section_HH1[4], Section_HH1[5]));
            list.Add(string.Format(format, "", "CH1", Section_CH1[0], Section_CH1[1], Section_CH1[2], Section_CH1[3], Section_CH1[4], Section_CH1[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "t. hnch2", "HW2", Section_HW2[0], Section_HW2[1], Section_HW2[2], Section_HW2[3], Section_HW2[4], Section_HW2[5]));
            list.Add(string.Format(format, "", "HH2", Section_HH2[0], Section_HH2[1], Section_HH2[2], Section_HH2[3], Section_HH2[4], Section_HH2[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(format, "bot. hnch", "HW3", Section_HW3[0], Section_HW3[1], Section_HW3[2], Section_HW3[3], Section_HW3[4], Section_HW3[5]));
            list.Add(string.Format(format, "", "HH3", Section_HH3[0], Section_HH3[1], Section_HH3[2], Section_HH3[3], Section_HH3[4], Section_HH3[5]));
            list.Add(string.Format("".PadLeft(108, '_')));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            PSC_Force_Data theta = new PSC_Force_Data(0);
            PSC_Force_Data D2 = new PSC_Force_Data(0);
            PSC_Force_Data K1 = new PSC_Force_Data(0);
            PSC_Force_Data K2 = new PSC_Force_Data(0);

            list.Add(string.Format("Calculated values: "));
            list.Add(string.Format(""));
            for (int i = 0; i < Section_lw.Count; i++)
            {
                theta.Add(Math.Atan(Section_lw[i] / Section_D1[i]));
                D2.Add(Section_D[i] - Section_Tf[i] - Section_D1[i]);
                K1.Add(D2[i] * Math.Tan(theta[i]));
                K2.Add(Section_Ts[i] * Math.Tan(theta[i]));

                //list.Add(string.Format("Calculated values:    (Cells shaded above are with these calculated values)"));
                list.Add(string.Format("Web Inclination = θ = atan (Iw / D1) = atan ({0} / {1}) = atan({2:f3}) = {3:f3}°(degrees) = {4:f3} radian",
                   Section_lw[i], Section_D1[i], (Section_lw[i] / Section_D1[i]), Section_Theta[i], theta[i]));

                list.Add(string.Format("and  D2 = D - Tf - D1 = {0:f3} - {1:f3} - {2:f3} = {3:f3}", Section_D[i], Section_Tf[i], Section_D1[i], D2[i]));
                list.Add(string.Format("K1 = D2 x tan(θ) = {0:f3} x tan({1:f3}) = {2:f3}", D2[i], Section_Theta[i], K1[0]));
                list.Add(string.Format("K2 = Ts x tan(θ) = {0:f3} x tan({1:f3}) = {2:f3}", Section_Ts[i], Section_Theta[i], K2[0]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            List<PSC_Force_Data> list_A = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_Yt = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_A_Yt = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_A_YT_Yt = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_Iself = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_Iself_A_Yt_Yt = new List<PSC_Force_Data>();
            List<PSC_Force_Data> list_Iyy = new List<PSC_Force_Data>();




            PSC_Force_Data A = new PSC_Force_Data(6);
            PSC_Force_Data Yt = new PSC_Force_Data(6);
            PSC_Force_Data Yb = new PSC_Force_Data(6);
            PSC_Force_Data Zt = new PSC_Force_Data(6);
            PSC_Force_Data Zb = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt = new PSC_Force_Data(6);
            PSC_Force_Data Iself = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt = new PSC_Force_Data(6);
            PSC_Force_Data Iyy = new PSC_Force_Data(6);
            PSC_Force_Data Ixx = new PSC_Force_Data(6);



            PSC_Force_Data A1 = new PSC_Force_Data(6);
            PSC_Force_Data Yt1 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt1 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt1 = new PSC_Force_Data(6);
            PSC_Force_Data Iself1 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt1 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy1 = new PSC_Force_Data(6);


            list_A.Add(A1);
            list_Yt.Add(Yt1);
            list_A_Yt.Add(A_Yt1);
            list_A_YT_Yt.Add(A_YT_Yt1);
            list_Iself.Add(Iself1);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt1);
            list_Iyy.Add(Iyy1);



            PSC_Force_Data A2 = new PSC_Force_Data(6);
            PSC_Force_Data Yt2 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt2 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt2 = new PSC_Force_Data(6);
            PSC_Force_Data Iself2 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt2 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy2 = new PSC_Force_Data(6);



            list_A.Add(A2);
            list_Yt.Add(Yt2);
            list_A_Yt.Add(A_Yt2);
            list_A_YT_Yt.Add(A_YT_Yt2);
            list_Iself.Add(Iself2);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt2);
            list_Iyy.Add(Iyy2);





            PSC_Force_Data A3 = new PSC_Force_Data(6);
            PSC_Force_Data Yt3 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt3 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt3 = new PSC_Force_Data(6);
            PSC_Force_Data Iself3 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt3 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy3 = new PSC_Force_Data(6);



            list_A.Add(A3);
            list_Yt.Add(Yt3);
            list_A_Yt.Add(A_Yt3);
            list_A_YT_Yt.Add(A_YT_Yt3);
            list_Iself.Add(Iself3);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt3);
            list_Iyy.Add(Iyy3);



            PSC_Force_Data A4 = new PSC_Force_Data(6);
            PSC_Force_Data Yt4 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt4 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt4 = new PSC_Force_Data(6);
            PSC_Force_Data Iself4 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt4 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy4 = new PSC_Force_Data(6);

            list_A.Add(A4);
            list_Yt.Add(Yt4);
            list_A_Yt.Add(A_Yt4);
            list_A_YT_Yt.Add(A_YT_Yt4);
            list_Iself.Add(Iself4);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt4);
            list_Iyy.Add(Iyy4);



            PSC_Force_Data A5 = new PSC_Force_Data(6);
            PSC_Force_Data Yt5 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt5 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt5 = new PSC_Force_Data(6);
            PSC_Force_Data Iself5 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt5 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy5 = new PSC_Force_Data(6);


            list_A.Add(A5);
            list_Yt.Add(Yt5);
            list_A_Yt.Add(A_Yt5);
            list_A_YT_Yt.Add(A_YT_Yt5);
            list_Iself.Add(Iself5);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt5);
            list_Iyy.Add(Iyy5);


            PSC_Force_Data A6 = new PSC_Force_Data(6);
            PSC_Force_Data Yt6 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt6 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt6 = new PSC_Force_Data(6);
            PSC_Force_Data Iself6 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt6 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy6 = new PSC_Force_Data(6);


            list_A.Add(A6);
            list_Yt.Add(Yt6);
            list_A_Yt.Add(A_Yt6);
            list_A_YT_Yt.Add(A_YT_Yt6);
            list_Iself.Add(Iself6);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt6);
            list_Iyy.Add(Iyy6);


            PSC_Force_Data A7 = new PSC_Force_Data(6);
            PSC_Force_Data Yt7 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt7 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt7 = new PSC_Force_Data(6);
            PSC_Force_Data Iself7 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt7 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy7 = new PSC_Force_Data(6);


            list_A.Add(A7);
            list_Yt.Add(Yt7);
            list_A_Yt.Add(A_Yt7);
            list_A_YT_Yt.Add(A_YT_Yt7);
            list_Iself.Add(Iself7);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt7);
            list_Iyy.Add(Iyy7);


            PSC_Force_Data A8 = new PSC_Force_Data(6);
            PSC_Force_Data Yt8 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt8 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt8 = new PSC_Force_Data(6);
            PSC_Force_Data Iself8 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt8 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy8 = new PSC_Force_Data(6);


            list_A.Add(A8);
            list_Yt.Add(Yt8);
            list_A_Yt.Add(A_Yt8);
            list_A_YT_Yt.Add(A_YT_Yt8);
            list_Iself.Add(Iself8);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt8);
            list_Iyy.Add(Iyy8);


            PSC_Force_Data A9 = new PSC_Force_Data(6);
            PSC_Force_Data Yt9 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt9 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt9 = new PSC_Force_Data(6);
            PSC_Force_Data Iself9 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt9 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy9 = new PSC_Force_Data(6);


            list_A.Add(A9);
            list_Yt.Add(Yt9);
            list_A_Yt.Add(A_Yt9);
            list_A_YT_Yt.Add(A_YT_Yt9);
            list_Iself.Add(Iself9);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt9);
            list_Iyy.Add(Iyy9);


            PSC_Force_Data A10 = new PSC_Force_Data(6);
            PSC_Force_Data Yt10 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt10 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt10 = new PSC_Force_Data(6);
            PSC_Force_Data Iself10 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt10 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy10 = new PSC_Force_Data(6);

            list_A.Add(A10);
            list_Yt.Add(Yt10);
            list_A_Yt.Add(A_Yt10);
            list_A_YT_Yt.Add(A_YT_Yt10);
            list_Iself.Add(Iself10);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt10);
            list_Iyy.Add(Iyy10);


            PSC_Force_Data A11 = new PSC_Force_Data(6);
            PSC_Force_Data Yt11 = new PSC_Force_Data(6);
            PSC_Force_Data A_Yt11 = new PSC_Force_Data(6);
            PSC_Force_Data A_YT_Yt11 = new PSC_Force_Data(6);
            PSC_Force_Data Iself11 = new PSC_Force_Data(6);
            PSC_Force_Data Iself_A_Yt_Yt11 = new PSC_Force_Data(6);
            PSC_Force_Data Iyy11 = new PSC_Force_Data(6);



            list_A.Add(A11);
            list_Yt.Add(Yt11);
            list_A_Yt.Add(A_Yt11);
            list_A_YT_Yt.Add(A_YT_Yt11);
            list_Iself.Add(Iself11);
            list_Iself_A_Yt_Yt.Add(Iself_A_Yt_Yt11);
            list_Iyy.Add(Iyy11);




            int c = 0;
            string part_text = "";
            do
            {
                if (c == 0) part_text = "SUPPORT";
                else if (c == 1) part_text = "D away from Support";
                else if (c == 2) part_text = "L/8";
                else if (c == 3) part_text = "L/4";
                else if (c == 4) part_text = "3L/8";
                else if (c == 5) part_text = "L/2";

                list.Add(string.Format(""));
                list.Add(string.Format("-----------------------------------------------------------"));
                list.Add(string.Format("DESIGN CALCULATIONS OF CROSS SECTION AT {0}", part_text));
                list.Add(string.Format("-----------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 1, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A1[c] = (Section_Dw[c] - 2 * (Section_C1[c] + Section_C2[c])) * Section_Td[c];
                list.Add(string.Format("Area = A = (Dw - 2 x (C1 + C2)) x Td "));
                list.Add(string.Format("         = ({0:f3} - 2 x ({1:f3} + {2:f3})) x {3:f3}", Section_Dw[c], Section_C1[c], Section_C2[c], Section_Td[c]));
                list.Add(string.Format("         = {0:f3}", A1[c]));
                list.Add(string.Format(""));
                Yt1[c] = Section_Td[c] / 2.0;
                list.Add(string.Format("Yt = Td/2.0 = {0:f3} / 2.0 = {1:f3}", Section_Td[c], Yt1[c]));

                A_Yt1[c] = A1[c] * Yt1[c];
                list.Add(string.Format("A x Yt = 1.3275 x 0.1125 = 0.1493"));

                A_YT_Yt1[c] = A_Yt1[c] * Yt1[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A1[0], Yt1[0], A_YT_Yt1[0]));


                Iself1[c] = A1[c] * Section_Td[c] * Section_Td[c] / 12.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia = A x Td^2 / 12 = {0:f4} x {1:f4}^2 / 12 = {2:f3}", A1[c], Section_Td[c], Iself1[0]));

                Iself_A_Yt_Yt1[c] = Iself1[c] * A_YT_Yt1[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself1[c], A_YT_Yt1[c], Iself_A_Yt_Yt1[c]));

                Iyy1[c] = (Math.Pow((Section_Dw[c] - 2 * (Section_C1[c] + Section_C2[c])), 3)) * Section_Td[c] / 12;

                list.Add(string.Format("I(yy) = ((Dw - 2 x (C1+C2))^3) x Td / 12 "));
                list.Add(string.Format("      = (({0:f4} - 2 x ({1:f4}+{2:f4}))^3) x {3:f4} / 12", Section_Dw[c], Section_C1[c], Section_C2[c], Section_Td[c]));
                list.Add(string.Format("      = {0:f4}", Iyy1[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 2, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A2[c] = (Section_C1[c] + Section_C2[c]) * Section_Tip[c] * 2.0;
                list.Add(string.Format("Area = A = (C1 + C2) x Ttip x 2"));
                list.Add(string.Format("         = ({0:f4} + {1:f4}) x {2:f4} x 2", Section_C1[c], Section_C2[c], Section_Tip[c]));
                list.Add(string.Format("         = {0:f4}", A2[c]));
                list.Add(string.Format(""));


                Yt2[c] = Section_Tip[c] / 2.0;
                list.Add(string.Format("Yt = Ttip/2.0 = {0:f3} / 2.0 = {1:f4}", Section_Tip[c], Yt2[c]));
                list.Add(string.Format(""));

                A_Yt2[c] = A2[c] * Yt2[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A2[c], Yt2[c], A_Yt2[c]));
                list.Add(string.Format(""));
                A_YT_Yt2[c] = A2[c] * Yt2[c] * Yt2[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A2[c], Yt2[c], A_YT_Yt2[c]));
                list.Add(string.Format(""));

                Iself2[c] = A2[c] * Section_Tip[c] * Section_Tip[c] / 12.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia = A x Ttip^2 / 12 = {0:f4} x {1:f4}^2 / 12 = {2:f4}", A2[c], Section_Tip[c], Iself2[c]));

                list.Add(string.Format(""));
                Iself_A_Yt_Yt2[c] = Iself2[c] + A_YT_Yt2[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself2[c], A_YT_Yt2[c], Iself_A_Yt_Yt2[c]));
                list.Add(string.Format(""));


                Iyy2[c] = (A2[c] * Math.Pow((Section_C1[c] + Section_C2[c]), 2)) / 12.0 +
                            A2[c] * Math.Pow((Section_Dw[c] / 2.0 - ((Section_C1[c] + Section_C2[c]) / 2)), 2.0);

                list.Add(string.Format("I(yy) = (A x (C1+C2)^2)/12  + (A x (Dw/2 - (C1+C2)/2)^2"));
                list.Add(string.Format("      = ({0:f4}x({1:f3}+{2:f3})^2) /12 + ({0:f4} x ({3:f3}/2 - ({1:f3}+{2:f3})/2)^2  ",
                    A2[c], Section_C1[c], Section_C2[c], Section_Dw[c]));
                //list.Add(string.Format("      = 0.2378 + 11.787"));
                list.Add(string.Format("      = {0:f4}", Iyy2[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 3, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A3[c] = (Section_C1[c] * (Section_Tf[c] - Section_Tip[c]));
                list.Add(string.Format("Area = A = C1 x (Tf - Ttip)"));
                list.Add(string.Format("         = {0:f3} x ({1:f3} - {2:f3})", Section_C1[c], Section_Tf[c], Section_Tip[c]));
                list.Add(string.Format("         = {0:f4}", A3[c]));
                list.Add(string.Format(""));

                Yt3[c] = (Section_Tf[c] - Section_Tip[c]) / 3.0 + Section_Tip[c];
                list.Add(string.Format("Yt = (Tf - Ttip)/3.0 + Ttip = ({0:f3} -{1:f3})/3.0 + {1:f3} = {2:f3}", Section_Tf[c], Section_Tip[c], Yt3[c]));
                list.Add(string.Format(""));
                A_Yt3[c] = A3[c] * Yt3[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A3[c], Yt3[c], A_Yt3[c]));
                list.Add(string.Format(""));

                A_YT_Yt3[c] = A_Yt3[c] * Yt3[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A3[c], Yt3[c], A_YT_Yt3[c]));
                list.Add(string.Format(""));

                Iself3[c] = (Section_C1[c] * Math.Pow((Section_Tf[c] - Section_Tip[c]), 3.0)) / 18.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia  = (C1 x (Tf - Ttip)^3) / 18 "));
                list.Add(string.Format("                                = ({0:f3} x ({1:f3} - {2:f3})^3)/18", Section_C1[c], Section_Tf[c], Section_Tip[c]));
                list.Add(string.Format("                                = {0:f5}", Iself3[c]));
                list.Add(string.Format(""));
                Iself_A_Yt_Yt3[c] = Iself3[c] + A_YT_Yt3[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself3[c], A_YT_Yt3[c], Iself_A_Yt_Yt3[c]));
                list.Add(string.Format(""));

                Iyy3[c] = (((Section_Tf[c] - Section_Tip[c]) * Math.Pow(Section_C1[c], 3)) / 18.0) +
                         (Math.Pow(((Section_Dw[c] / 2.0) - Section_C2[c] - (Section_C1[c] / 3) * 2), 2)) * A3[c];


                list.Add(string.Format("I(yy) = ((Tf - Ttip) x C1^3)/18 + (((Dw/2)-C2-(C1/3) x 2)^2) x A"));
                list.Add(string.Format("      = (({0:f3} - {1:f3}) x {2:f3}^3)/18 + (({3:f3} /2) - {4:f3} - ({2:f3}/3) x 2)^2) x {5:f3}",
                   Section_Tf[c], Section_Tip[c], Section_C1[c], Section_Dw[c], Section_C2[c], A3[c]));
                //list.Add(string.Format("      = 0.0396 + 2.4833"));
                list.Add(string.Format("      = {0:f4}", Iyy3[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 4, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));
                A4[c] = (Section_Tf[c] - Section_Tip[c]) * (Section_Tw[c] / Math.Cos(theta[c]) * 2);
                list.Add(string.Format("Area = A = (Tf - Ttip) x Tw / COS(θ) x 2"));
                list.Add(string.Format("         = ({0:f3} - {1:f3}) x {2:f3}/COS({3:f3}) x 2", Section_Tf[c], Section_Tip[c], Section_Tw[c], theta[c]));
                list.Add(string.Format("         = {0:f4}", A4[c]));
                list.Add(string.Format(""));

                Yt4[c] = (Section_Tf[c] - Section_Tip[c]) / 2.0 + Section_Tip[c];
                list.Add(string.Format("Yt = (Tf - Ttip)/2.0 + Ttip = ({0:f3} - {1:f3})/2.0 + {1:f3} = {2:f4}", Section_Tf[c], Section_Tip[c], Yt4[c]));
                list.Add(string.Format(""));

                A_Yt4[c] = A4[c] * Yt4[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A4[c], Yt4[c], A_Yt4[c]));
                list.Add(string.Format(""));
                A_YT_Yt4[c] = A_Yt4[c] * Yt4[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A4[c], Yt4[c], A_YT_Yt4[c]));
                list.Add(string.Format(""));

                Iself4[c] = (A4[c] * Math.Pow((Section_Tf[c] - Section_Tip[c]), 2.0)) / 12.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia  = (A x (Tf - Ttip)^2) / 12 "));
                list.Add(string.Format("                                = ({0:f4} x ({1:f4} - {2:f3})^2)/12",
                        A4[c], Section_Tf[c], Section_Tip[c]));
                list.Add(string.Format("                                = {0:f6}", Iself4[c]));
                list.Add(string.Format(""));
                Iself_A_Yt_Yt4[c] = Iself4[c] + A_YT_Yt4[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself4[c], A_YT_Yt4[c], Iself_A_Yt_Yt4[c]));
                list.Add(string.Format(""));

                Iyy4[c] = (A4[c] * Math.Pow((A4[c] / 2 / (Section_Tf[c] - Section_Tip[c])), 2) / 12.0) +

                    Math.Pow((Section_Dw[c] / 2 - (Section_C1[c] + Section_C2[c]) - Section_Tw[c] / Math.Cos(theta[c]) / 2), 2) * A4[c];

                list.Add(string.Format("I(yy)  = (A x (A/2/( Tf - Ttip))^2/12 + (Dw/2 - (C1+C2) - Tw/COS(θ)/2)^2 x A"));
                list.Add(string.Format("       = ({0:f3}x({0:f3}/2/({1:f3}-{2:f3}))^2/12 + ({3:f3}/2-({4:f3}+{5:f3})-{6:f3}/COS({7:f3})/2)^2 x {0:f3}",
                    A4[c], Section_Tf[c], Section_Tip[c], Section_Dw[c], Section_C1[c], Section_C2[c], Section_Tw[c], theta[c]));
                //list.Add(string.Format("       = 0.0042 + 0.8743"));
                list.Add(string.Format("       = {0:f4}", Iyy4[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 5, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A5[c] = (Section_HW1[c] + 2 * Section_HW2[c]) * Section_HH1[c];

                list.Add(string.Format("Area = A  = (HW1 + 2 x HW2) x HH1"));
                list.Add(string.Format("          = ({0:f4} + 2 x {1:f4}) x {2:f4}", Section_HW1[c], Section_HW2[c], Section_HH1[c]));
                list.Add(string.Format("          = {0:f4}", A5[c]));
                list.Add(string.Format(""));

                Yt5[c] = Section_HH1[c] / 3 + Section_Td[c];
                list.Add(string.Format("Yt = HH1/3 + Td = {0:f4}/3 + {1:f4} = {2:f4}", Section_HH1[c], Section_Td[c], Yt5[c]));
                list.Add(string.Format(""));
                A_Yt5[c] = A5[c] * Yt5[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A5[c], Yt5[c], A_Yt5[c]));
                list.Add(string.Format(""));
                A_YT_Yt5[c] = A5[c] * Yt5[c] * Yt5[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A5[c], Yt5[c], A_YT_Yt5[c]));
                list.Add(string.Format(""));
                Iself5[c] = (A5[c] * Math.Pow((Section_HH1[c]), 2)) / 18.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia = (A x (HH1)^2) / 18 = ({0:f4} x ({1:f4})^2) / 18 = {2:E3}", A5[c], Section_HH1[c], Iself5[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt5[c] = Iself5[c] + A_YT_Yt5[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:E3} + {1:f4} = {2:E3}", Iself5[c], A_YT_Yt5[c], Iself_A_Yt_Yt5[c]));
                list.Add(string.Format(""));

                Iyy5[c] = (Section_HH1[c] * Math.Pow((Section_HW1[c] + Section_HW2[c]), 3)) / 18.0 +
                        (A5[c] * Math.Pow((Section_CH1[c] + (Section_HW1[c] + Section_HW2[c]) / 3 * 2), 2));

                list.Add(string.Format("I(yy)   = (HH1 x (HW1+HW2)^3)/18 + (A x (CH1 + (HW1 + HW2)/3 x 2)^2)"));
                list.Add(string.Format("        = ({0:f4} x ({1:f4} + {2:f4})^3)/18 + ({3:f4} x ({4:f4} + ({1:f4} + {2:f4})/3 x 2)^2)",
                                                Section_HH1[c], Section_HW1[c], Section_HW2[c], A5[c], Section_CH1[c]));
                //list.Add(string.Format("        = 0.00031548 + 0.1527"));
                list.Add(string.Format("        = {0:f4}", Iyy5[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 6, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A6[c] = Section_HH2[c] * Section_HW2[c];
                list.Add(string.Format("Area = A = HH2 x HW2  = {0:f4} x {1:f4} = {2:f4}", Section_HH2[c], Section_HW2[c], A6[c]));
                list.Add(string.Format(""));

                Yt6[c] = (Section_HH2[c] / 3.0) + Section_HH1[c] + Section_Td[c];

                list.Add(string.Format("Yt = HH2/3 x HH1 + Td = {0:f4}/3 + {1:f4} + {2:f4} = {3:f4}", Section_HH2[c], Section_HH1[c], Section_Td[c], Yt6[c]));

                A_Yt6[c] = A6[c] * Yt6[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A6[c], Yt6[c], A_Yt6[c]));

                A_YT_Yt6[c] = A6[c] * Yt6[c] * Yt6[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A6[c], Yt6[c], A_YT_Yt6[c]));

                Iself6[c] = (A6[c] * Math.Pow((Section_HH2[c]), 2)) / 18.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia = (A x (HH2)^2) / 18 = ({0:f4} x ({1:f4})^2) / 18 = {2:f4}", A6[c], Section_HH2[c], Iself6[c]));

                list.Add(string.Format(""));

                Iself_A_Yt_Yt6[c] = Iself6[c] + A_YT_Yt6[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself6[c], A_YT_Yt6[c], Iself_A_Yt_Yt6[c]));

                Iyy6[c] = ((Section_HH2[c] * Math.Pow(Section_HW2[c], 3)) / 18.00) +
                                 (A6[c] * Math.Pow((Section_CH1[c] + Section_HW1[c] + Section_HW2[c] / 3 * 2), 2));

                list.Add(string.Format("I(yy)   = (HH2 x HW2^3)/18 + (A x (CH1 + HW1 + HW2/3 x 2)^2)"));
                list.Add(string.Format("        = ({0:f4} x {1:f4}^3)/18 + ({2:f4} x ({3:f4} + {4:f4} + {1:f4}/3 x 2)^2)",
                                                Section_HH2[c], Section_HW2[c], A6[c], Section_CH1[c], Section_HW1[c]));
                //list.Add(string.Format("        = 0.0 + 0.0"));
                list.Add(string.Format("        = {0:f4}", Iyy6[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 7, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A7[c] = 2 * (Section_D1[c] + Section_D2[c] - Section_Ts[c]) * Section_Tw[c] / Math.Cos(theta[c]);

                list.Add(string.Format("Area = A = 2 x (D1+D2-Ts) x Tw/ COS(θ)"));
                list.Add(string.Format("         = 2 x ({0:f4}+{1:f4}-{2:f4}) x {3:f4} / COS({4:f4})",
                    Section_D1[c], Section_D2[c], Section_Ts[c], Section_Tw[c], theta[c]));
                list.Add(string.Format("         = {0:f4}", A7[c]));
                list.Add(string.Format(""));

                Yt7[c] = Section_Tf[c] + (Section_D1[c] + Section_D2[c] - Section_Ts[c]) / 2.0;
                list.Add(string.Format("Yt = Tf + (D1 + D2 - Ts)/2 = {0:f4} + ({1:f4} + {2:f4} - {3:f4})/2 = {4:f4}",
                    Section_Tf[c], Section_D1[c], Section_D2[c], Section_Ts[c], Yt7[c]));

                A_Yt7[c] = A7[c] * Yt7[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A7[c], Yt7[c], A_Yt7[c]));
                A_YT_Yt7[c] = A7[c] * Yt7[c] * Yt7[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A7[c], Yt7[c], A_Yt7[c]));

                Iself7[c] = ((Section_Tw[c] / Math.Cos(theta[c])) * Math.Pow((Section_D1[c] + Section_D2[c] - Section_Ts[c]), 3) / 6);

                list.Add(string.Format("Iself (xx) = Moment of Inertia "));
                list.Add(string.Format("           = ((Tw/COS(θ)) x (D1+D2-Ts)^3/6"));
                list.Add(string.Format("           = (({0:f4}/COS({1:f4})) x ({2:f4} + {3:f4} - {4:f4})^3/6",
                    Section_Tw[c], theta[c], Section_D1[c], Section_D2[c], Section_Ts[c]));
                list.Add(string.Format("           = {0:f6}", Iself7[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt7[c] = Iself7[c] + A_YT_Yt7[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}",
                    Iself7[c], A_YT_Yt7[c], Iself_A_Yt_Yt7[c]));


                Iyy7[c] = ((Section_D1[c] + Section_D2[c] - Section_Ts[c]) * Math.Pow((Section_Tw[c] / Math.Cos(theta[c])), 3)) / 6

                            + A7[c] * Math.Pow((Section_Dw[c] / 2 - Section_C1[c] - Section_C2[c] -
                            (Section_D1[c] + Section_D2[c] - Section_Ts[c]) * Math.Tan(theta[c]) / 2 - Section_Tw[c] / 2 / Math.Cos(theta[c])), 2);


                list.Add(string.Format(""));
                list.Add(string.Format("I(yy)   = ((D1+D2-Ts) x (Tw/COS(θ))^3)/6 "));
                list.Add(string.Format("           + A x (Dw/2 - C1 - C2 - (D1+D2-Ts) x TAN(θ)/2 - Tw/2/COS(D θ))^2"));
                list.Add(string.Format(""));

                list.Add(string.Format("        = (({0:f4} + {1:f4} - {2:f4}) x ({3:f4}/COS({4:f4}))^3)/6 ",
                                                Section_D1[c], Section_D2[c], Section_Ts[c], Section_Tw[c], theta[c]));

                list.Add(string.Format("          + {0:f4} x ({1:f4}/2 - {2:f4} - {3:f4} - ({4:f4} + {5:f4} - {6:f4}) x TAN({7:f4})/2 - {8:f4}/2/COS({7:f4}))^2",
                                                A7[c], Section_Dw[c], Section_C1[c], Section_C2[c], Section_D1[c], Section_D2[c], Section_Ts[c], theta[c], Section_Tw[c]));
                list.Add(string.Format("        = {0:f4}", Iyy7[c]));
                //list.Add(string.Format("        = 0.0686 + 11.6972"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 8, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A8[c] = (Section_SW[c] - Section_K1[c] * 2) * Section_Ts[c];

                list.Add(string.Format("Area = A = (Sw-K1 x 2) x Ts = ({0:f4}-{1:f4} x 2) x {2:f4} = {3:f4}", Section_SW[c], Section_K1[c], Section_Ts[c], A8[c]));

                list.Add(string.Format(""));
                Yt8[c] = Section_D[c] - Section_Ts[c] / 2.0;
                list.Add(string.Format("Yt = D - Ts/2 = {0:f4} - {1:f4}/2 = {2:f4}", Section_D[c], Section_Ts[c], Yt8[c]));

                A_Yt8[c] = A8[c] * Yt8[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A8[c], Yt8[c], A_Yt8[c]));

                A_YT_Yt8[c] = A8[c] * Yt8[c] * Yt8[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A8[c], Yt8[c], A_Yt8[c]));


                Iself8[c] = (A8[c] * Math.Pow(Section_Ts[c], 2)) / 12.0;
                list.Add(string.Format("Iself (xx) = Moment of Inertia"));
                //list.Add(string.Format("  = (D95 x D34^2) / 12"));
                list.Add(string.Format("           = (A x Ts^2) / 12"));
                list.Add(string.Format("           = ({0:f4} x {1:f4}^2) / 12", A8[c], Section_Ts[c]));
                list.Add(string.Format("           = {0:f6}", Iself8[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt8[c] = Iself8[c] + A_YT_Yt8[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself8[c], A_YT_Yt8[c], Iself_A_Yt_Yt8[c]));

                Iyy8[c] = (Section_Ts[c] * Math.Pow((Section_SW[c] - 2 * Section_K1[c]), 3)) / 12.0;

                list.Add(string.Format("I(yy)   = (Ts x (Sw - 2 x K1)^3) / 12"));
                list.Add(string.Format("        = {0:f4} x ({1:f4} - 2 x {2:f4})^3 / 12", Section_Ts[c], Section_SW[c], Section_K1[c]));
                list.Add(string.Format("        = {0:f4}", Iyy8[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 9, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A9[c] = Section_K1[c] * Section_D2[c];
                list.Add(string.Format("Area = A = K1 x D2 = {0:f4} x {1:f4} = {2:f4}", Section_K1[c], Section_D2[c], A9[c]));
                list.Add(string.Format(""));

                Yt9[c] = Section_D[c] - Section_D2[c] / 3.0;
                list.Add(string.Format("Yt = D - D2/3 = {0:f4} - {1:f4}/2 = {2:f4}", Section_D[c], Section_D2[c], Yt9[c]));

                A_Yt9[c] = A9[c] * Yt9[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A9[c], Yt9[c], A_Yt9[c]));
                A_YT_Yt9[c] = A9[c] * Yt9[c] * Yt9[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4}  x {1:f4} = {2:f4}", A9[c], Yt9[c], A_YT_Yt9[c]));

                Iself9[c] = (Section_K1[c] * Math.Pow(Section_D2[c], 3)) / 18;

                list.Add(string.Format("Iself (xx) = Moment of Inertia         "));
                list.Add(string.Format("           = (K1 x D2^3) / 18"));
                list.Add(string.Format("           = ({0:f4} x {1:f4}^2) / 18", Section_K1[c], Section_D2[c]));
                list.Add(string.Format("           = {0:f6}", Iself9[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt9[c] = Iself9[c] + A_YT_Yt9[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself9[c], A_YT_Yt9[c], Iself_A_Yt_Yt9[c]));



                Iyy9[c] = (Section_D2[c] * Math.Pow(Section_K1[c], 3)) / 18.0 + A9[c] * Math.Pow((Section_SW[c] / 2 - Section_K1[c] / 3), 2);
                list.Add(string.Format("I(yy)   = (D2 x K1^3) / 18 + A x (Sw/2 - K1/3)^2"));
                list.Add(string.Format("        = ({0:f4} x {1:f4}^3) / 18 + {2:f4} x ({3:f4}/2 -{1:f4}/3)^2",
                                                    Section_D2[c], Section_K1[c], A9[c], Section_SW[c]));
                list.Add(string.Format("        = {0:f4}", Iyy9[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 10, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format(""));

                A10[c] = Section_Ts[c] * Section_K2[c];
                list.Add(string.Format("Area = A = Ts x K2"));
                list.Add(string.Format("         = {0:f4} x {1:f4}", Section_Ts[c], Section_K2[c]));
                list.Add(string.Format("         = {0:f4}", A10[c]));
                list.Add(string.Format(""));

                Yt10[c] = Section_D[c] - (Section_Ts[c] / 3) * 2.0;

                list.Add(string.Format("Yt = D - (Ts/3) x 2 = {0:f4} - ({1:f4}/3) x 2 = {2:f4}",
                    Section_D[c], Section_Ts[c], Yt10[c]));

                A_Yt10[c] = A10[c] * Yt10[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A10[c], Yt10[c], A_Yt10[c]));
                A_YT_Yt10[c] = A10[c] * Yt10[c] * Yt10[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A10[c], Yt10[c], A_YT_Yt10[c]));


                Iself10[c] = (Section_K2[c] * Math.Pow(Section_Ts[c], 3)) / 18.0;

                list.Add(string.Format("Iself (xx) = Moment of Inertia "));
                list.Add(string.Format("           = (K2 x Ts^3) / 18"));
                list.Add(string.Format("           = ({0:f4} x {1:f4}^3) / 18", Section_K2[c], Section_Ts[c]));
                list.Add(string.Format("           = {0:f5}", Iself10[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt10[c] = Iself10[c] + A_YT_Yt10[c];


                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f5} + {1:f4} = {2:f5}", Iself10[c], A_YT_Yt10[c], Iself_A_Yt_Yt10[c]));


                Iyy10[c] = (Section_Ts[c] * Math.Pow(Section_K2[c], 3)) / 18.0 + A10[c] * Math.Pow((Section_SW[c] / 2 - Section_K1[c] + Section_K2[c] / 3), 2);

                list.Add(string.Format("I(yy)   = (Ts x K2^3) / 18 + A x (Sw/2 - K1 + K2/3)^2"));
                list.Add(string.Format("        = ({0:f4} x {1:f4}^3) / 18 + {2:f4} x ({3:f4}/2 - {4:f4} + {1:f4}/3)^2",
                                               Section_Ts[c], Section_K2[c], A10[c], Section_SW[c], Section_K1[c]));
                list.Add(string.Format("        = {0:f4}", Iyy10[c]));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("Cross Section Part 11, at {0}:", part_text));
                list.Add(string.Format("---------------------------------------------------"));


                A11[c] = Section_HW3[c] * Section_HH3[c];
                list.Add(string.Format("Area = A = HW3 x HH3 = {0:f4} x {1:f4} = {2:f4}", Section_HW3[c], Section_HH3[c], A11[c]));
                list.Add(string.Format(""));

                Yt11[c] = Section_D[c] - Section_Ts[c] - (Section_HH3[c] / 3);


                list.Add(string.Format("Yt = D - Ts - (HH3/3) = {0:f4} - {1:f4} - ({2:f4}/3) = {3:f4}",
                    Section_D[c], Section_Ts[c], Section_HH3[c], Yt11[c]));

                A_Yt11[c] = A11[c] * Yt11[c];
                list.Add(string.Format("A x Yt = {0:f4} x {1:f4} = {2:f4}", A11[c], Yt11[c], A_Yt11[c]));
                A_YT_Yt11[c] = A11[c] * Yt11[c] * Yt11[c];
                list.Add(string.Format("A x Yt x Yt = {0:f4} x {1:f4} x {1:f4} = {2:f4}", A11[c], Yt11[c], A_YT_Yt11[c]));

                list.Add(string.Format("Iself (xx) = Moment of Inertia         "));

                Iself11[c] = (Section_HW3[c] * Math.Pow(Section_HH3[c], 3)) / 18.0;

                list.Add(string.Format("  = (HW3 x HH3^3) / 18"));
                list.Add(string.Format("  = ({0:f4} x {1:f4}^3) / 18", Section_HW3[c], Section_HH3[c]));
                list.Add(string.Format("  = {0:f6}", Iself11[c]));
                list.Add(string.Format(""));

                Iself_A_Yt_Yt11[c] = Iself11[c] + A_YT_Yt11[c];
                list.Add(string.Format("Iself (xx) + A x Yt x Yt = {0:f4} + {1:f4} = {2:f4}", Iself11[c], A_YT_Yt11[c], Iself_A_Yt_Yt11[c]));


                Iyy11[c] = (Section_HH3[c] * Math.Pow(Section_HW3[c], 3) / 18) +
                           A11[c] * Math.Pow((Section_SW[c] / 2 - Section_K1[c] + Section_K2[c] - Section_Tw[c] / Math.Cos(theta[c]) - Section_HW3[c] / 3), 2);


                list.Add(string.Format("I(yy)   = (HH3 xHW3^3)/18 + A x (Sw/2 - K1 + K2 - Tw/COS(θ) - HW3/3)^2"));
                list.Add(string.Format("        = ({0:f4} x {1:f4}^3)/18 + {2:f4} x ({3:f4}/2 - {4:f4} + {5:f4} - {6:f4}/COS({7:f4}) - {1:f4}/3)^2",
                            Section_HH3[c], Section_HW3[c], A11[c], Section_SW[c], Section_K1[c], Section_K2[c], Section_Tw[c], theta[c]));
                list.Add(string.Format("        = {0:f4}", Iyy11[c]));
                list.Add(string.Format(""));

                c++;
            }
            while (c < 6);

            list.Add(string.Format("TABLE  3:  Calculated values for Section Properties of various parts in the Cross Section at relevant Sections"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            format = "{0,-8:f4} {1,-18:f4} {2,12:f4} {3,12:f4} {4,12:f4} {5,12:f4} {6,12:f4} {7,12:f4}";
            //list.Add(string.Format("                At Support    At ‘D’        L/8          L/4           3L/8          L/2"));
            list.Add(string.Format(format, "", "", "At Support", "At 'D'", "L/8", "L/4", "3L/8", "L/2"));



            for (int i = 0; i < list_A.Count; i++)
            {

                A1 = new PSC_Force_Data();

                A1 = list_A[i];
                Yt1 = list_Yt[i];
                A_Yt1 = list_A_Yt[i];
                A_YT_Yt1 = list_A_YT_Yt[i];
                Iself1 = list_Iself[i];
                Iself_A_Yt_Yt1 = list_Iself_A_Yt_Yt[i];
                Iyy1 = list_Iyy[i];

                list.Add(string.Format("".PadLeft(108, '-')));
                list.Add(string.Format(format, "Part " + (i + 1), "AREA", A1[0], A1[1], A1[2], A1[3], A1[4], A1[5]));
                list.Add(string.Format(format, "", "Yt", Yt1[0], Yt1[1], Yt1[2], Yt1[3], Yt1[4], Yt1[5]));
                list.Add(string.Format(format, "", "A.Yt", A_Yt1[0], A_Yt1[1], A_Yt1[2], A_Yt1[3], A_Yt1[4], A_Yt1[5]));
                list.Add(string.Format(format, "", "A.Yt^2", A_YT_Yt1[0], A_YT_Yt1[1], A_YT_Yt1[2], A_YT_Yt1[3], A_YT_Yt1[4], A_YT_Yt1[5]));
                list.Add(string.Format(format, "", "Iself -(xx)", Iself1[0], Iself1[1], Iself1[2], Iself1[3], Iself1[4], Iself1[5]));
                list.Add(string.Format(format, "", "Iself + A.Yt^2", Iself_A_Yt_Yt1[0], Iself_A_Yt_Yt1[1], Iself_A_Yt_Yt1[2], Iself_A_Yt_Yt1[3], Iself_A_Yt_Yt1[4], Iself_A_Yt_Yt1[5]));
                list.Add(string.Format(format, "", "Iself-(yy)", Iyy1[0], Iyy1[1], Iyy1[2], Iyy1[3], Iyy1[4], Iyy1[5]));
            }
            list.Add(string.Format("".PadLeft(108, '-')));

            A1 = list_A[0];
            Yt1 = list_Yt[0];
            A_Yt1 = list_A_Yt[0];
            A_YT_Yt1 = list_A_YT_Yt[0];
            Iself1 = list_Iself[0];
            Iself_A_Yt_Yt1 = list_Iself_A_Yt_Yt[0];
            Iyy1 = list_Iyy[0];
            #endregion STEP 1
            #region STEP 2 :  Section Properties:

            list.Add(string.Format(""));
            //list.Add(string.Format("(This will continue for all 11 Parts, at SUPPORT, as calculated below)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 :  Section Properties :"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculations :"));
            list.Add(string.Format("--------------"));

            c = 0;

            for (int i = 0; i < list_A.Count; i++)
            {
                A += list_A[i];
                Yt += list_Yt[i];
                A_Yt += list_A_Yt[i];
                A_YT_Yt += list_A_YT_Yt[i];
                Iself += list_Iself[i];
                Iself_A_Yt_Yt += list_Iself_A_Yt_Yt[i];
                Iyy += list_Iyy[i];
            }
            do
            {

                if (c == 0) part_text = "SUPPORT";
                else if (c == 1) part_text = "'D'";
                else if (c == 2) part_text = "L/8";
                else if (c == 3) part_text = "L/4";
                else if (c == 4) part_text = "3L/8";
                else if (c == 5) part_text = "L/2";

                list.Add(string.Format(""));
                list.Add(string.Format("Values at {0} :", part_text));
                list.Add(string.Format(""));
                list.Add(string.Format("Total [AREA] = A = A1+A2+A3+A4+A5+A6+A7+A8+A9+A10+A11 = {0:f5}", A[c]));
                list.Add(string.Format(""));
                list.Add(string.Format("Total [A x Yt] = [A x Yt]1 + [A x Yt]2 + [A x Yt]3 + [A x Yt]4 + [A x Yt]5"));
                list.Add(string.Format("                 + [A x Yt]6 + [A x Yt]7 + [A x Yt]8 + [A x Yt]9 + [A x Yt]10  "));
                list.Add(string.Format("                 + [A x Yt]11"));
                list.Add(string.Format(""));
                list.Add(string.Format("                 = {0:f4}", A_Yt[c]));
                list.Add(string.Format(""));
                list.Add(string.Format("Total [Iself + A x Yt^2] = [Iself + A x Yt^2]1 + [Iself + A x Yt^2]2 + [Iself + A x Yt^2]3 + [Iself + A x Yt^2]4"));
                list.Add(string.Format("                           + [Iself + A x Yt^2]5 + [Iself + A x Yt^2]6 + [Iself + A x Yt^2]7 + [Iself + A x Yt^2]8"));
                list.Add(string.Format("                           + [Iself + A x Yt^2]9  + [Iself + A x Yt^2]10 + [Iself + A x Yt^2]11 "));
                list.Add(string.Format(""));
                list.Add(string.Format("                          = {0:f4} ", Iself_A_Yt_Yt[c]));
                list.Add(string.Format(""));

                Yt[c] = A_Yt[c] / A[c];
                list.Add(string.Format("Yt = Total [A x Yt] / Total [AREA] = {0:f4}", Yt[c]));
                list.Add(string.Format(""));

                Yb[c] = Section_D[c] - Yt[c];
                list.Add(string.Format("Yb = D - Yt = {0:f4} - {1:f4} = {2:f4}", Section_D[c], Yt[c], Yb[c]));
                list.Add(string.Format(""));
                Ixx[c] = Iself_A_Yt_Yt[c] - A[c] * Yt[c] * Yt[c];
                list.Add(string.Format("Ixx = (Total [Iself + A x Yt^2) - (Total [AREA]) x (Yt)^2 = {0:f4}", Ixx[c]));
                list.Add(string.Format(""));
                list.Add(string.Format("Iyy = [Iyy]1 + [Iyy]2 + [Iyy]3 + [Iyy]4 + [Iyy]5"));
                list.Add(string.Format("      + [Iyy]6 + [Iyy]7 + [Iyy]8 + [Iyy]9 + [Iyy]10 + [Iyy]11"));
                list.Add(string.Format("    = {0:f4}", Iyy[c]));
                list.Add(string.Format(""));

                Zt[c] = Ixx[c] / Yt[c];
                list.Add(string.Format("Zt = Ixx / Yt = {0:F5} / {1:f5} = {2:f5}", Ixx[c], Yt[c], Zt[c]));
                list.Add(string.Format(""));
                Zb[c] = Ixx[c] / Yb[c];
                list.Add(string.Format("Zb = Ixx / Yb = {0:F5} / {1:f5} = {2:f5}", Ixx[c], Yb[c], Zb[c]));
                list.Add(string.Format(""));

                c++;
            }
            while (c < 6);
            list.Add(string.Format(""));
            format = "{0,-14:f4} {1,-12:f4} {2,12:f4} {3,12:f4} {4,12:f4} {5,12:f4} {6,12:f4} {7,12:f4}";
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("TABLE  4:  Final Section Properties Table :"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("".PadLeft(108, '-')));
            list.Add(string.Format(format, "", "Unit", "At Support", "At 'D'", "L/8", "L/4", "3L/8", "L/2"));
            list.Add(string.Format("".PadLeft(108, '-')));
            list.Add(string.Format(format, "AREA", "sq.ft", A[0], A[1], A[2], A[3], A[4], A[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, SYMBOLS.SIGMA_SUM + "A.Yt", "ft^3", A_Yt[0], A_Yt[1], A_Yt[2], A_Yt[3], A_Yt[4], A_Yt[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, SYMBOLS.SIGMA_SUM + "Iself+A.Yt^2", "sq.sq.ft", Iself_A_Yt_Yt[0], Iself_A_Yt_Yt[1], Iself_A_Yt_Yt[2], Iself_A_Yt_Yt[3], Iself_A_Yt_Yt[4], Iself_A_Yt_Yt[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Yt", "ft", Yt[0], Yt[1], Yt[2], Yt[3], Yt[4], Yt[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Yb", "ft", Yb[0], Yb[1], Yb[2], Yb[3], Yb[4], Yb[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Ix-x", "sq.sq.ft", Ixx[0], Ixx[1], Ixx[2], Ixx[3], Ixx[4], Ixx[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Iy-y", "sq.sq.ft", Iyy[0], Iyy[1], Iyy[2], Iyy[3], Iyy[4], Iyy[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Zt", "ft^3", Zt[0], Zt[1], Zt[2], Zt[3], Zt[4], Zt[5]));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "Zb", "ft^3", Zb[0], Zb[1], Zb[2], Zb[3], Zb[4], Zb[5]));
            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(108, '-')));
            list.Add(string.Format(""));

            psc_setion = new PSC_Box_Section_Data(A, Ixx, Iyy, Iself_A_Yt_Yt);
            #endregion STEP 2

            return list;
        }
        
        private double Sqrt(double val)
        {
            return Math.Sqrt(val);
        }

        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF RCC DECK SLAB : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of PSC Box Girder");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "DESIGN_OF_PSC_BOX_GIRDER.TXT");
                user_input_file = Path.Combine(system_path, "PSC_BOX_GIRDER.FIL");
            }
        }

    }
    //Chiranjit [2012 11 01]
    public class PSC_Box_Section_Data
    {
        public PSC_Force_Data Area { get; set; }
        public PSC_Force_Data Ixx { get; set; }
        public PSC_Force_Data Iyy { get; set; }
        public PSC_Force_Data Izz { get; set; }

        public PSC_Box_Section_Data(PSC_Force_Data A, PSC_Force_Data Ix, PSC_Force_Data Iy, PSC_Force_Data Iz)
        {
            Area = A;
            Ixx = Ix;
            Iyy = Iy;
            Izz = Iz;
        }
        //A += list_A[i];
        //   Yt += list_Yt[i];
        //   A_Yt += list_A_Yt[i];
        //   A_YT_Yt += list_A_YT_Yt[i];
        //   Iself += list_Iself[i];
        //   Iself_A_Yt_Yt += list_Iself_A_Yt_Yt[i];
        //   Iyy += list_Iyy[i];
    }
    //Chiranjit [2012 09 28]
    public class PSC_Box_Forces
    {
        public PSC_Force_Data FRC_DL_Moment { get; set; }
        public PSC_Force_Data FRC_SIDL_Moment { get; set; }
        public PSC_Force_Data FRC_LL_Moment { get; set; }
        public PSC_Force_Data FRC_Total_Moment
        {
            get
            {
                return (FRC_DL_Moment + FRC_SIDL_Moment + FRC_LL_Moment);
            }
        }

        public PSC_Force_Data FRC_DL_Shear { get; set; }
        public PSC_Force_Data FRC_SIDL_Shear { get; set; }
        public PSC_Force_Data FRC_LL_Shear { get; set; }
        public PSC_Force_Data FRC_Total_Shear
        {
            get
            {
                return (FRC_DL_Shear + FRC_SIDL_Shear + FRC_LL_Shear);
            }
        }
        public PSC_Force_Data FRC_DL_Torsion { get; set; }
        public PSC_Force_Data FRC_SIDL_Torsion { get; set; }
        public PSC_Force_Data FRC_LL_Torsion { get; set; }
        public PSC_Force_Data FRC_Total_Torsion
        {
            get
            {
                return (FRC_DL_Torsion + FRC_SIDL_Torsion + FRC_LL_Torsion);
            }
        }
        public PSC_Box_Forces()
        {
            FRC_DL_Moment = new PSC_Force_Data(6);
            FRC_SIDL_Moment = new PSC_Force_Data(6);
            FRC_LL_Moment = new PSC_Force_Data(6);

            FRC_DL_Shear = new PSC_Force_Data(6);
            FRC_SIDL_Shear = new PSC_Force_Data(6);
            FRC_LL_Shear = new PSC_Force_Data(6);

            FRC_DL_Torsion = new PSC_Force_Data(6);
            FRC_SIDL_Torsion = new PSC_Force_Data(6);
            FRC_LL_Torsion = new PSC_Force_Data(6);
        }

        public void Set_Absolute()
        {
            FRC_DL_Moment.Set_Absolute();
            FRC_SIDL_Moment.Set_Absolute();
            FRC_LL_Moment.Set_Absolute();
            FRC_DL_Shear.Set_Absolute();
            FRC_SIDL_Shear.Set_Absolute();
            FRC_LL_Shear.Set_Absolute();
            FRC_DL_Torsion.Set_Absolute();
            FRC_SIDL_Torsion.Set_Absolute();
            FRC_LL_Torsion.Set_Absolute();
            FRC_LL_Torsion.Set_Absolute();
        }
    }

    public class AASHTO_Design_PC_Box_Girder
    {

        #region Set_Input Data
        public static void Input_PSC_Box_Girder_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Bridge Geometry$$$"));
            list.Add(string.Format("Span 1 length$$118$ft"));
            list.Add(string.Format("Span 2 length$$130$ft"));
            list.Add(string.Format("Bridge width $$45.17$ft"));
            list.Add(string.Format("Roadway width $$42.00$ft"));
            list.Add(string.Format("Superstructure depth $$5.50$ft"));
            list.Add(string.Format("Web spacing $$7.75$ft"));
            list.Add(string.Format("Web thickness $$12.00$in"));
            list.Add(string.Format("Top slab thickness $$8.00$in"));
            list.Add(string.Format("Bottom slab thickness $$6.00$in"));
            list.Add(string.Format("Deck overhang $$2.63$ft"));
            list.Add(string.Format("bar dia =$$0.625$in"));
            
            list.Add(string.Format("centerline spacing =$$7.75$ft"));
            list.Add(string.Format("effective length =$$6.75$ft"));
            list.Add(string.Format("minimum thickness =$$8$in"));
            list.Add(string.Format("overhang thickness =$$9$in"));
            list.Add(string.Format("minimum thickness of the web=$$12$in"));
            
            list.Add(string.Format("The minimum thickness of the web shall be $$12$in"));
            
            list.Add(string.Format("Concrete Deck Slab Minimum Requirements$$$"));
            
            list.Add(string.Format("Slab thickness $$8.00$in"));
            list.Add(string.Format("Top concrete cover $$2.50$in"));
            list.Add(string.Format("Bottom concrete cover $$1.00$in"));
            list.Add(string.Format("Wearing surface $$0.50$in"));
            
            list.Add(string.Format("Material Properties$$$"));
            list.Add(string.Format("Reinforcing Steel$$$"));
            list.Add(string.Format("Yield Strength $fy = $60$ksi"));
            list.Add(string.Format("Modulus of Elasticity $Es = $29000$ksi"));
            
            list.Add(string.Format("Prestressing Strand$$$"));
            
            list.Add(string.Format("Low relaxation prestressing strands$$$"));
            list.Add(string.Format("0.6” diameter strand $Aps =$0.217$in2"));
            list.Add(string.Format("Tensile Strength$fpu = $270$ ksi"));
            list.Add(string.Format("Yield Strength $fpy = $243$ksi"));
            list.Add(string.Format("Modulus Elasticity $Ep = $28500$ksi"));
            
            list.Add(string.Format("Unit weight for computing $Ec = $0.145$kcf"));
            list.Add(string.Format("Unit weight for DL calculation$ = $0.150$kcf"));
            
            list.Add(string.Format("Live Loads$$$"));
            list.Add(string.Format("Pos M = $$5.1$ft-k/ft"));
            list.Add(string.Format("Neg M = $$-5.50$ft-k/ft "));
            
            list.Add(string.Format("Prestress Design$$$"));
            list.Add(string.Format("tendons per web=$$2$nos."));
            list.Add(string.Format("diameter of the duct =$$4.375$in"));
            list.Add(string.Format("no of layers =$$3$layers"));
            list.Add(string.Format("Z = $$1$ inch."));
            list.Add(string.Format("One duct =$$17$ strands "));
            list.Add(string.Format("other  duct =$$16$strands"));
            
            list.Add(string.Format("Step 4 – Calculate Anchor Set Losses$$$"));
            list.Add(string.Format("Anchor Set Loss = $$19.522$ksi"));

            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv, 2);
        }
        public static void Input_Abutment_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add("pad thickness $$0.75$in");
            list.Add("girder height $$72$in");
            list.Add("haunch thickness $$4$in");
            list.Add("deck thickness $$8$in");

            list.Add("Wingwall thickness$$20.25$in.");

            list.Add("Parapet weight $$0.65$k/ft ");
            list.Add("Approach slab length $$25$ft");
            list.Add("flange width =$$12.045$in");



            list.Add("t = flange thickness (in.)$$0.435$in.");
            list.Add("$E =$29000$");
            list.Add("$fy =$36$");

            list.Add("Required information:$$$");
            list.Add("Concrete compressive strength, f'c = $$3$ksi");
            list.Add("Reinforcing steel yield strength, Fy = $$60$ksi");

            list.Add(" unit weight of soil bearing on the backwall (kcf)$Y =$0.13$kcf");


            list.Add(" width of backwall  =$$3$ft");
            list.Add("concrete cover  =$$3$ft");
            list.Add("vertical bar dia =$$0.625$ft");
            list.Add("bar dia. =$$0.75$ft");

            list.Add(" Load on the parapet =$$124$kips");
            list.Add("Applied length =$$8$ft");

            list.Add("Parapet thickness at base =$$20.25$in");

            list.Add("slab depth =$$1.5$ft");
            list.Add("cover (cast against soil) =$$3$in");
            list.Add("bar diameter =$$1.128$in");
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Pier_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs

            list.Add("Girders (E/I) $$61.6$k");
            list.Add("Deck slab and haunch (E) $$55.1$k");
            list.Add("Deck slab and haunch (I) $$62.2$k");
            list.Add("Intermediate diaphragm (E) $$1.3$k");
            list.Add("Intermediate diaphragm (I) $$2.5$k");
            list.Add("Parapets (E/I) $$14.8$k");
            list.Add("Future wearing surface (E) $$13.4$k");
            list.Add("Future wearing surface (I)$$19.9$k");

            list.Add("deck thickness =$$0.667$ft");
            list.Add("haunch thickness =$$0.333$ft");
            list.Add(" girder depth =$$6$ft");

            list.Add("girder height =$$6$ft");
            list.Add("haunch height =$$0.333$ft");
            list.Add("deck height =$$0.667$ft");
            list.Add("parapet height = $$3.5$ft");


            list.Add("span length to the deck joint, or end of bridge, back station from pier (ft.)$Lback $110$ ft.");

            list.Add(" span length to the deck joint, or end of bridge, ahead station from pier (ft.)$Lahead$110$ ft.");

            list.Add("Wind load along axes of superstructure (longitudinal direction)$Hwind = $10.5$ft.");


            list.Add("Applied Strength I moment, Mu =$$1,015.50$k-ft");

            list.Add("Applied Service I moment, Ms = $$653.3$k-ft ");


            list.Add("Circular Columns:$$$");

            list.Add("Column diameter = $$3.5$ft.");

            list.Add("Column area, Ag = $$9.62$ft2");

            list.Add("Side cover = $$2$in.");

            list.Add("Vertical reinforcing bar diameter (#8) = $$1.0$in.");

            list.Add("Number of bars = $$16$");



            list.Add("Type of transverse reinforcement = ties$$$");
            list.Add("Tie spacing = $$12$ in.");

            list.Add("Transverse reinforcement bar diameter (#3) = $$0.375$in. ");
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Bearing_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step 6.1 - Obtain Design Criteria$$$"));
            list.Add(string.Format("Service I limit state dead load$DLserv =$78.4$K"));
            list.Add(string.Format("Service I limit state live load (including dynamic load allowance)$LLserv =$110.4$K"));
            list.Add(string.Format("Service I limit state total rotation about the transverse axis$Ysx =$0.0121$rad"));
            list.Add(string.Format("Strength limit state minimum vertical force due to permanent loads$Psd =$67.8$K"));
            list.Add(string.Format("Design Step 6.3 - Select Preliminary Bearing Properties$$$"));
            list.Add(string.Format("Bearing Pad Configuration$$$"));
            list.Add(string.Format("Pad length (bridge longitudinal direction):$Lpad =$14$in"));
            list.Add(string.Format("Pad width (bridge transverse direction):$Wpad =$15$in"));
            list.Add(string.Format("Elastomer cover thickness:$hrcover =$0.25$in"));
            list.Add(string.Format("Elastomer internal layer thickness:$hrinternal =$0.375$in"));
            list.Add(string.Format("Number of steel reinforcement layers:$Nstlayers =$9$"));
            list.Add(string.Format("Steel reinforcement thickness:$hreinf =$0.1196$in"));
            list.Add(string.Format("Material Properties$$$"));
            list.Add(string.Format("Elastomer hardness:$HshoreA =$50$"));
            list.Add(string.Format("Elastomer shear modulus:$G =$0.095$ksi"));
            list.Add(string.Format("Elastomer creep deflection at 25 years$$$"));
            list.Add(string.Format("divided by the instantaneous deflection:$Cd =$0.25$"));
            list.Add(string.Format("Steel reinforcement yield strength:$Fy =$50$ksi"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Input_Foundation_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add(string.Format("Design Step P.1 - Define Subsurface Conditions and Any Geometric Constraints$$$"));

            list.Add(string.Format("Dry unit weight:$Ydry =$90$PCF"));


            list.Add(string.Format("Wet unit weight:$Ywet =$110$PCF"));


            list.Add(string.Format("Unit weight of water:$Ywater =$62.4$PCF"));


            list.Add(string.Format("Design Step P.4 - Verify Need for a Pile Foundation$$$"));

            list.Add(string.Format("Maximum possible length of footing$L =$46.875$ft"));



            list.Add(string.Format("Preliminary minimum required width$Bmin =$12.736$ft"));



            list.Add(string.Format("Modulus of elasticity of soil, from Design Step P.1:$Es =$60$TSF"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Process_PSC_Box_Girder_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC Box Girder AASHTO LRFD\Design of Deckslab PSC Box-GirderBridge.xlsx");

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
            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP;

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();
                if (dgv != null)
                    eui.Read_From_Grid(dgv);


                #region Ref Cells
                List<string> list = new List<string>();
                //list.Add("Deckslab");
                list.Add("B20");
                list.Add("B21");
                list.Add("B22");
                list.Add("B23");
                list.Add("B24");
                list.Add("B25");
                list.Add("B26");
                list.Add("B27");
                list.Add("B28");
                list.Add("B29");
                list.Add("B30");
                list.Add("C47");
                list.Add("C48");
                list.Add("C49");
                list.Add("C50");
                list.Add("C51");
                list.Add("D59");
                list.Add("B63");
                list.Add("B64");
                list.Add("B65");
                list.Add("B66");
                list.Add("C129");
                list.Add("C130");
                list.Add("C138");
                list.Add("C139");
                list.Add("C140");
                list.Add("C141");
                list.Add("D157");
                list.Add("D158");
                list.Add("B300");
                list.Add("B301");//31
                //list.Add("Step 2");
                list.Add("C13");
                list.Add("C14");
                list.Add("C15");
                list.Add("C16");
                list.Add("C17");
                list.Add("C18");//37
                //list.Add("Step 4");
                list.Add("B11");
                #endregion Ref Cells

                try
                {
                    string val = "";
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Deckslab"];

                    for (int i = 0; i < 31; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 2"];

                    for (int i = 31; i < 37; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4"];

                    for (int i = 37; i < list.Count; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception ex111) { }
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_Bearing_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step 6 Bearing Design.xlsx");

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
            try
            {
                Excel.Worksheet EXL_INP;

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                if (dgv != null)
                    eui.Read_From_Grid(dgv);

                List<string> list = new List<string>();
                //list.Add("STEP 6.1");
                list.Add("B26");
                list.Add("B27");
                list.Add("B30");
                list.Add("B33");
                //list.Add("STEP 6.3");
                list.Add("E10");
                list.Add("E12");
                list.Add("E14");
                list.Add("E16");
                list.Add("E18");
                list.Add("E20");
                list.Add("E24");
                list.Add("E25");
                list.Add("E28");
                list.Add("E32");
                try
                {
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 6.1"];

                    string kStr = "";
                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["STEP 6.3"];

                    for (int i = 4; i < list.Count; i++)
                    {
                        try
                        {
                            kStr = EXL_INP.get_Range(list[i]).Formula.ToString();
                            double d = double.Parse(kStr);
                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception exx) { }
            }
            catch (Exception ex) { }

            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);


        }
        public static void Process_Abutment_and_Wingwall_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder AASHTO LRFD\07 Design of AASHTO LRFD Abutment.xlsx");

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
            try
            {
                myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                try
                {
                    //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                    Excel.Worksheet EXL_INP;

                    //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                    //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                    //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                    Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                    eui.Read_From_Grid(dgv);


                    List<string> list = new List<string>();
                    #region Cell Ref
                    //list.Add("Step7.1.1");
                    list.Add("E150");
                    list.Add("E151");
                    list.Add("E152");
                    list.Add("E153");
                    list.Add("E170");
                    list.Add("E187");
                    list.Add("E197");//7
                    //list.Add("Step7.1.3");
                    list.Add("C80");
                    list.Add("C87");
                    list.Add("C92");
                    list.Add("C93");//11
                    //list.Add("Step7.1.4");
                    list.Add("F29");
                    list.Add("F30");
                    list.Add("C490");
                    list.Add("D542");
                    list.Add("D543");
                    list.Add("D544");
                    list.Add("D545");//18
                    //list.Add("Step7.1.5");
                    list.Add("E89");
                    list.Add("E90");
                    list.Add("E124");//21
                    //list.Add("Step7.1.6");
                    list.Add("E94");
                    list.Add("E95");
                    list.Add("E96");
                    #endregion
                    try
                    {
                        string val = "";
                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.1.1"];
                        for (int i = 0; i < 7; i++)
                        {
                            try
                            {
                                val = EXL_INP.get_Range(list[i]).Formula.ToString();

                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.1.3"];
                        for (int i = 7; i < 11; i++)
                        {
                            try
                            {
                                val = EXL_INP.get_Range(list[i]).Formula.ToString();

                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.1.4"];
                        for (int i = 11; i < 18; i++)
                        {
                            try
                            {
                                val = EXL_INP.get_Range(list[i]).Formula.ToString();

                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }


                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.1.5"];
                        for (int i = 18; i < 21; i++)
                        {
                            try
                            {
                                val = EXL_INP.get_Range(list[i]).Formula.ToString();

                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }


                        EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.1.6"];
                        for (int i = 21; i < list.Count; i++)
                        {
                            try
                            {
                                val = EXL_INP.get_Range(list[i]).Formula.ToString();

                                EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                                //EXL_INP.get_Range(kStr).Formula = item.Text;
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                    }
                    catch (Exception exx) { }
                }
                catch (Exception exx1) { }

                iApp.Excel_Close_Message();
                myExcelWorkbook.Save();
                MyList.releaseObject(myExcelWorkbook);
            }
            catch (Exception exx) { }
        }
        public static void Process_Pier_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder AASHTO LRFD\08 Design of AASHTO LRFD Pier.xlsx");

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

            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP;

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                eui.Read_From_Grid(dgv);


                List<string> list = new List<string>();
                #region Cell Ref
                //list.Add("Step7.2.1");
                list.Add("F141");
                list.Add("F142");
                list.Add("F143");
                list.Add("F144");
                list.Add("F145");
                list.Add("F146");
                list.Add("F147");
                list.Add("F148");
                list.Add("D273");
                list.Add("D274");
                list.Add("D275");
                list.Add("E318");
                list.Add("E319");
                list.Add("E320");
                list.Add("E321");
                list.Add("C334");
                list.Add("C338");
                list.Add("D355"); //18
                //list.Add("Step 7.2.2.2");
                list.Add("F5");
                list.Add("F7"); // 20
                //list.Add("Step 7.2.3");
                list.Add("F13");
                list.Add("F15");
                list.Add("F17");
                list.Add("F19");
                list.Add("F23");
                list.Add("F28");
                list.Add("F30");

                #endregion
                try
                {
                    string val = "";
                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step7.2.1"];

                    for (int i = 0; i < 18; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 7.2.2.2"];

                    for (int i = 18; i < 20; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 7.2.3"];

                    for (int i = 20; i < list.Count; i++)
                    {
                        try
                        {
                            val = EXL_INP.get_Range(list[i]).Formula.ToString();

                            EXL_INP.get_Range(list[i]).Formula = eui[i].Input_Value;
                            //EXL_INP.get_Range(kStr).Formula = item.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
                catch (Exception exx) { }
            }
            catch (Exception exx) { }

            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        #endregion Set_Input Data
    }


    public class PSC_Box_Girder_Inputs
    {
        public PSC_Box_Girder_Inputs() { }

        public TextBox txt_Spans { get; set; }
        public TextBox txt_Ana_Span { get; set; }
        public TextBox txt_Ana_Bridge_Width { get; set; }
        public TextBox txt_Ana_Road_Width { get; set; }
        public TextBox txt_Ana_Superstructure_depth { get; set; }
        public TextBox txt_Ana_Web_Spacing { get; set; }
        public TextBox txt_Ana_Web_Thickness { get; set; }
        public TextBox txt_Ana_Top_Slab_Thickness { get; set; }
        public TextBox txt_Ana_Bottom_Slab_Thickness { get; set; }
        public TextBox txt_Ana_Deck_Overhang { get; set; }
        public TextBox txt_Ana_Deckslab_Thickness { get; set; }
        public TextBox txt_Ana_Deck_Top_Cover { get; set; }
        public TextBox txt_Ana_Deck_Bottom_Cover { get; set; }
        public TextBox txt_Ana_Deck_Wearing_Surface { get; set; }
        public TextBox txt_Ana_Strand_Diameter { get; set; }
        public TextBox txt_Ana_Strand_Area { get; set; }
        public TextBox txt_Ana_Strand_Fpu { get; set; }
        public TextBox txt_Ana_Strand_Fpy { get; set; }
        public TextBox txt_Ana_Strand_Ep { get; set; }
        public TextBox txt_Ana_Superstructure_fc { get; set; }
        public TextBox txt_Ana_column_fc { get; set; }
        public TextBox txt_Ana_Superstructure_fci { get; set; }
        public TextBox txt_Ana_Concrete_Ec { get; set; }
        public TextBox txt_Ana_Concrete_DL_Calculation { get; set; }
        public TextBox txt_Ana_Steel_Fy { get; set; }
        public TextBox txt_Ana_Steel_Es { get; set; }
        public TextBox txt_Ana_SelfWeight { get; set; }
        public TextBox txt_Ana_SIDL { get; set; }
        public TextBox txt_Ana_FPLL { get; set; }

    }

}
