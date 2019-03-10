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
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.PSC_I_Girder;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign.PSC_BoxGirder;
using LimitStateMethod.RCC_T_Girder;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.CableStayed;




namespace LimitStateMethod.Extradossed
{
    public partial class frm_Extradosed : Form
    {
        //const string Title = "ANALYSIS OF PSC BOX GIRDER BRIDGE";
        public string Title
        {
            get
            {
                if (Project_Type == eASTRADesignType.Extradossed_Side_Towers_Bridge_LS)
                {
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                        return "EXTRADOSED CABLE STAYED BRIDGE WITH EITHER SIDE TOWERS  [BS]";
                    if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                        return "EXTRADOSED CABLE STAYED BRIDGE WITH EITHER SIDE TOWERS [AASHTO - LRFD]";
                    return "EXTRADOSED CABLE STAYED BRIDGE WITH EITHER SIDE TOWERS [IRC]";
                }
                else if (Project_Type == eASTRADesignType.Extradossed_Central_Towers_Bridge_LS)
                {
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                        return "EXTRADOSED CABLE STAYED BRIDGE WITH CENTRAL TOWER [BS]";
                    if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                        return "EXTRADOSED CABLE STAYED BRIDGE WITH CENTRAL TOWER  [AASHTO - LRFD]";
                    return "EXTRADOSED CABLE STAYED BRIDGE WITH CENTRAL TOWER  [IRC]";
                }
                return "EXTRADOSED CABLE STAYED BRIDGE [IRC]";
            }
        }

        //CompositAnalysis Deck_Analysis_DL = null;
        //CompositAnalysis Deck_Analysis_LL = null;
        //PostTensionLongGirder LongGirder = null;
        //RccDeckSlab Deck = null;

        PSC_BoxGirderAnalysis Deck_Analysis_DL = null;
        PSC_BoxGirderAnalysis Deck_Analysis_LL = null;

        //CABLE_STAYED_Extradosed_LS_Analysis Deck_Analysis_DL
        //{
        //    get
        //    {
        //        return Extradosed;
        //    }
        //    set
        //    {
        //         Extradosed = value;
        //    }
        //}
        //CABLE_STAYED_Extradosed_LS_Analysis Deck_Analysis_LL
        //{
        //    get
        //    {
        //        return Extradosed;
        //    }
        //    set
        //    {
        //        Extradosed = value;
        //    }
        //}

        CABLE_STAYED_Extradosed_LS_Analysis Extradosed = null;

        public CABLE_STAYED_Extradosed_LS_Analysis Bridge_Analysis
        {
            get
            {
                return Extradosed;
            }
        }

        public List<BridgeMemberAnalysis> All_Analysis { get; set; }


        PostTensionLongGirder LongGirder = null;

        PSC_Box_Section_Data PSC_SECIONS;

        //Chiranjit [2012 10 30]
        RccPier rcc_pier = null;
        Save_FormRecord SaveRec = new Save_FormRecord();
        //Chiranjit [2012 10 31]
        //SupportReactionCollection support_reactions = null;



        //Chiranjit [2012 09 22]
        PSC_Box_Segmental_Girder Segment_Girder = null;
        //Chiranjit [2012 09 28]
        PSC_Box_Forces Box_Forces = null;

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        public double L { get { return (L1 + L2 + L3); } }
        public double L1 { get { return MyList.StringToDouble(txt_Ana_L1.Text, 13.0); } set { txt_Ana_L1.Text = value.ToString("f3"); } }
        public double L2 { get { return MyList.StringToDouble(txt_L2.Text, 13.0); } set { txt_L2.Text = value.ToString("f3"); } }
        public double L3 { get { return MyList.StringToDouble(txt_L3.Text, 13.0); } set { txt_L3.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }

        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
            //B = abut_width;
        }

        //Chiranjit [2013 06 17]
        public double DL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_DL_factor.Text, 1.0);
            }
        }
        public double LL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_LL_factor.Text, 1.0);
            }
        }

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
        }


        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            //if (txt.Name == txt_dead_vert_reac_ton.Name)
            //{
            Text_Changed_Forces();
            //}

        }

        private void Text_Changed_Forces()
        {

            //double DL_Factor = MyList.StringToDouble(txt_Ana_DL_Factor.Text, 1.0);
            //double LL_Factor = MyList.StringToDouble(txt_Ana_LL_Factor.Text, 1.0);

            //lbl_DL_factor.Text = "Factor = " + txt_Ana_DL_Factor.Text;
            //lbl_LL_factor.Text = "Factor = " + txt_Ana_LL_Factor.Text;

            if (B != 0)
            {
                //Chiranjit [2013 06 16]
                //txt_dead_vert_reac_ton_factor.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * DL_Factor)).ToString("f3");
                txt_dead_vert_reac_kN.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10)).ToString("f3");
                txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_kN.Text, 0.0)) / B).ToString("f3");


                //txt_live_vert_rec_Ton_factor.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * LL_Factor)).ToString("f3");


                txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_kN.Text, 0.0)) / B).ToString("f3");

                txt_final_vert_reac.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * DL_Factor) +
                    (MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * LL_Factor)).ToString("f3");
                txt_final_vert_rec_kN.Text = (MyList.StringToDouble(txt_final_vert_reac.Text, 0.0) * 10).ToString("f3");
            }

            //txt_abut
            //else if (txt.Name == txt_dead_kN_m.Name)
            //{
            //txt_abut_w5.Text = txt_dead_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            //}
            //else if (txt.Name == txt_live_kN_m.Name)
            //{
            //txt_abut_w6.Text = txt_live_kN_m.Text;
            txt_pier_2_P3.Text = txt_live_kN_m.Text;
            //}
            //else if (txt.Name == txt_final_vert_rec_kN.Name)
            //{
            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mx_kN.Name)
            //{
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mz_kN.Name)
            //{
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;


            //txt_Abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_Ana_B.Text;

            txt_RCC_Pier_L.Text = txt_Ana_L1.Text;
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
            List<string> list_arr = new List<string>(File.ReadAllLines(analysis_rep));
            list_arr.Add("");
            list_arr.Add("                   =====================================");
            list_arr.Add("                     DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                   =====================================");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add(string.Format(""));
            list_arr.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list_arr.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list_arr.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
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
            //Change unit kN to Ton
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
            //Change unit kN to Ton
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
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Ton");


            txt_final_vert_reac.Text = (MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * DL_Factor +
                (tot_right_vert_reac + tot_left_vert_reac) * LL_Factor).ToString("0.000");

            txt_final_vert_rec_kN.Text = ((MyList.StringToDouble(txt_final_vert_reac.Text, 0.0)) * 10).ToString("0.000");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_max_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_max_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");





            //File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_max_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_max_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);


            //uC_PierDesignLimitState1.Left_Span_Force = (MyList.StringToDouble(txt_left_total_vert_reac.Text, 0.0) * 10).ToString();
            //uC_PierDesignLimitState1.Right_Span_Force = (MyList.StringToDouble(txt_right_total_vert_reac.Text, 0.0) * 10).ToString();

        }
        #endregion frm_Pier_ViewDesign_Forces



        RccDeckSlab Deck = null;
        public List<string> Results { get; set; }

        IApplication iApp = null;

        bool IsCreate_Data = true;
        public frm_Extradosed(IApplication app, eASTRADesignType projType)
        {
            
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;

            Project_Type = projType;

            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            Results = new List<string>();

            Segment_Girder = new PSC_Box_Segmental_Girder(app);

            Box_Forces = new PSC_Box_Forces();
            All_Analysis = new List<BridgeMemberAnalysis>();

            if (projType == eASTRADesignType.Extradossed_Side_Towers_Bridge_LS)
                pcb_cables.BackgroundImage = LimitStateMethod.Properties.Resources.ExtradosedEitherSideTower;
            else
                pcb_cables.BackgroundImage = LimitStateMethod.Properties.Resources.ExtradossedCentralTowers;
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
                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != "Live Load Analysis")
                        if (!Directory.Exists(Path.Combine(user_path, "Live Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(user_path, "Live Load Analysis"));


                    return Path.Combine(Path.Combine(user_path, "Live Load Analysis"), "Input_Data_LL.txt");
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
                if (Directory.Exists(user_path))
                {
                    if (Path.GetFileName(user_path) != "Dead Load Analysis")
                        if (!Directory.Exists(Path.Combine(user_path, "Dead Load Analysis")))
                            Directory.CreateDirectory(Path.Combine(user_path, "Dead Load Analysis"));


                    return Path.Combine(Path.Combine(user_path, "Dead Load Analysis"), "Input_Data_DL.txt");
                    //return Path.Combine(user_path, "Input_Data_DL.txt");
                }

                //if (Directory.Exists(user_path))
                //    return Path.Combine(user_path, "Input_Data_DL.txt");
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
                Write_All_Data();
                Analysis_Initialize_InputData();
                //Create_Data_LL(Input_File);

                //Create_Data_Extradossed(Input_File);


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    LONG_GIRDER_BRITISH_LL_TXT();
                else
                    LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {

                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        Create_Data_LL(Input_File_LL);
                    }
                    else 
                    {
                        Create_Data_LL_British(Input_File_LL);
                    }
                    Create_Data_DL(Input_File_DL);

                    #region Chiranjit [2017 03 29]

                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                    //cmb_long_open_file.Items.Add("DL + LL ANALYSIS");


                    #endregion Chiranjit [2014 10 22]


                }
                else
                {
                    Create_Data_Extradossed(Input_File);
                    #region Chiranjit [2017 03 29]

                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                    //cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                    for (int i = 0; i < all_loads.Count; i++)
                    {
                        cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                    }
                    cmb_long_open_file.Items.Add("DL + LL ANALYSIS");


                    #endregion Chiranjit [2014 10 22]

                }





                //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                //    Create_Data_LL(Input_File_LL);
                //else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //{
                //    Create_Data_LL_British(Input_File_LL);
                //}
                //Create_Data_DL(Input_File_DL);




                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");
                cmb_long_open_file.SelectedIndex = 0;
                Button_Enable_Disable();
            }
            catch (Exception ex) { }
        }


        private void btn_Ana_LL_process_analysis_Click(object sender, EventArgs e)
        {
            string flPath = Extradosed.Input_File;
            string ana_rep_file = "";
            int c = 0;

            Write_All_Data();
            //groupBox25.Visible = true;

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();


            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {

                if (cmb_cable_type.SelectedIndex == 2)
                {
                    iApp.Progress_Works.Clear();

                    flPath = Deck_Analysis_DL.Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    //Extradosed.WriteData_LiveLoad(Extradosed.LiveLoadAnalysis_Input_File, long_ll);
                    //Ana_Write_Cable_Load_Data(Extradosed.LiveLoadAnalysis_Input_File, true, false, 1);

                    flPath = Deck_Analysis_LL.Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");

                }
                else
                {
                    iApp.Form_Stage_Analysis(flPath = Extradosed.DeadLoadAnalysis_Input_File).ShowDialog();

                    iApp.Progress_Works.Clear();

                    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    //Extradosed.WriteData_LiveLoad(Extradosed.LiveLoadAnalysis_Input_File, long_ll);
                    //Ana_Write_Cable_Load_Data(Extradosed.LiveLoadAnalysis_Input_File, true, false, 1);

                    flPath = Extradosed.TotalAnalysis_Input_File;
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");


                    //for (int i = 0; i < all_loads.Count; i++)
                    //{
                    //    Extradosed.WriteData_LiveLoad(Extradosed.Get_LL_Analysis_Input_File(i + 1), long_ll);
                    //    //Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), true, true, i + 1);
                    //    //Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), false, true, i + 1);
                    //    Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
                    //}




                    if (File.Exists(iApp.Stage_File))
                    {
                        flPath = Extradosed.DeadLoadAnalysis_Input_File;

                        //File.Copy(iApp.Stage_File, flPath, true);

                    }
                    //iApp.Form_Stage_Analysis(flPath = Extradosed.TotalAnalysis_Input_File).ShowDialog();

                    for (int i = 0; i < (all_loads.Count); i++)
                    {
                        //if (i == 0)
                        //{
                        //    flPath = Extradosed.TotalAnalysis_Input_File;
                        //}
                        ////else if (i == 1)
                        ////    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                        //else
                        //{
                        flPath = Get_Live_Load_Analysis_Input_File(i + 1);

                        //    File.Copy(iApp.Stage_File, flPath, true);
                        //    Ana_Write_Long_Girder_Load_Data(flPath, true, false, i + 1);
                        //}

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
                }
            }
            else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (false)
                {
                    #region Bridtish Loads
                    iApp.Progress_Works.Clear();
                    for (int i = 0; i < (all_loads.Count + 1); i++)
                    {
                        if (i == 0)
                            flPath = Extradosed.DeadLoadAnalysis_Input_File;
                        else
                            flPath = Get_Live_Load_Analysis_Input_File(i);


                        pd = new ProcessData();
                        pd.Process_File_Name = flPath;
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);


                        iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");
                    }

                    #endregion British Loads
                }

                iApp.Form_Stage_Analysis(flPath = Extradosed.DeadLoadAnalysis_Input_File).ShowDialog();

                iApp.Progress_Works.Clear();

                flPath = Extradosed.DeadLoadAnalysis_Input_File;
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);


                iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");



                if (File.Exists(iApp.Stage_File))
                {

                    flPath = Extradosed.DeadLoadAnalysis_Input_File;

                    //File.Copy(iApp.Stage_File, flPath, true);

                }
                //iApp.Form_Stage_Analysis(flPath = Extradosed.TotalAnalysis_Input_File).ShowDialog();

                for (int i = 0; i < (all_loads.Count + 1); i++)
                {
                    if (i == 0)
                    {
                        flPath = Extradosed.TotalAnalysis_Input_File;
                        //flPath = Extradosed.DeadLoadAnalysis_Input_File;
                    }
                    //else if (i == 1)
                    //    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                    else
                    {
                        flPath = Get_Live_Load_Analysis_Input_File(i);

                        //File.Copy(iApp.Stage_File, flPath, true);
                        Ana_Write_Long_Girder_Load_Data(flPath, true, false, i);
                    }


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



            }
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {

                iApp.Form_Stage_Analysis(flPath = Extradosed.DeadLoadAnalysis_Input_File).ShowDialog();
                
                iApp.Progress_Works.Clear();

                //flPath = Extradosed.DeadLoadAnalysis_Input_File;
                //pd = new ProcessData();
                //pd.Process_File_Name = flPath;
                //pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                //pcol.Add(pd);


                //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath) + " File");



                if (File.Exists(iApp.Stage_File))
                {
                    flPath = Extradosed.DeadLoadAnalysis_Input_File;

                    File.Copy(MyList.Get_Analysis_Report_File(iApp.Stage_File), MyList.Get_Analysis_Report_File(flPath), true);

                }
                //iApp.Form_Stage_Analysis(flPath = Extradosed.TotalAnalysis_Input_File).ShowDialog();

                for (int i = 0; i < (all_loads.Count + 1); i++)
                {
                    if (i == 0)
                    {
                        flPath = Extradosed.TotalAnalysis_Input_File;
                    }
                    //else if (i == 1)
                    //    flPath = Extradosed.DeadLoadAnalysis_Input_File;
                    else
                    {
                        flPath = Get_Live_Load_Analysis_Input_File(i);

                        //File.Copy(iApp.Stage_File, flPath, true);
                        Ana_Write_Long_Girder_Load_Data(flPath, true, false, i);
                    }


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
            }

            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }
            //string file_name = Path.Combine(Path.GetDirectoryName(Extradosed.TotalAnalysis_Input_File), @"PROCESS STAGE (P DELTA) ANALYSIS\STAGE 1 ANALYSIS\ANALYSIS_REP.TXT");
            //Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis("DL + LL Combine Analysis\PROCESS STAGE (P DELTA) ANALYSIS\STAGE 1 ANALYSIS")

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                Show_Member_Forces_Indian(ana_rep_file);
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                //Show_Member_Forces_Indian(ana_rep_file);
                Show_Member_Forces_British();
            }
            else
                Show_Member_Forces_British();


            //iApp.Show_and_Run_Process_List(pcol)
            uC_CableStayedDesign1.iApp = iApp;
            uC_CableStayedDesign1.txt_cbl_des_mem_nos.Text = "";



            uC_CableStayedDesign1.txt_cbl_des_mem_nos.Text = MyList.Get_Array_Text(Extradosed.Cable_members);



            Extradosed.All_LL_Analysis = All_Analysis;
            uC_CableStayedDesign1.CS_Analysis = Extradosed;




            Save_FormRecord.Write_All_Data(this, user_path);


            #region Load BM-SF
            uC_BoxGirder1.txt_BM_DL_Supp.Text = txt_Ana_dead_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Deff.Text = txt_Ana_dead_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L8.Text = txt_Ana_dead_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L4.Text = txt_Ana_dead_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_DL_3L8.Text = txt_Ana_dead_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Mid.Text = txt_Ana_dead_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_DL_Supp.Text = txt_Ana_dead_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Deff.Text = txt_Ana_dead_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L8.Text = txt_Ana_dead_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L4.Text = txt_Ana_dead_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_DL_3L8.Text = txt_Ana_dead_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Mid.Text = txt_Ana_dead_inner_long_L2_shear.Text;


            uC_BoxGirder1.txt_BM_LL_Supp.Text = txt_Ana_live_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Deff.Text = txt_Ana_live_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L8.Text = txt_Ana_live_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L4.Text = txt_Ana_live_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_LL_3L8.Text = txt_Ana_live_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Mid.Text = txt_Ana_live_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_LL_Supp.Text = txt_Ana_live_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Deff.Text = txt_Ana_live_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L8.Text = txt_Ana_live_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L4.Text = txt_Ana_live_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_LL_3L8.Text = txt_Ana_live_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Mid.Text = txt_Ana_live_inner_long_L2_shear.Text;


            #endregion Load BM-SF

        }

        private void Show_Member_Forces_Indian(string ana_rep_file)
        {

            //string ana_rep_file = "";

            All_Analysis.Clear();

            if (cmb_cable_type.SelectedIndex == 2)
            {
                //Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;
                //Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_DL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


                ana_rep_file = MyList.Get_Analysis_Report_File(Deck_Analysis_LL.Input_File);

                if (File.Exists(ana_rep_file))
                {
                    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


            }
            else
            {
                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.DeadLoadAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    ////All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                    Extradosed.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }

                ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.TotalAnalysis_Input_File);

                if (File.Exists(ana_rep_file))
                {
                    ////All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                    Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                }


                for (int i = 0; i < all_loads.Count; i++)
                {
                    //if (i == 0)
                    //    ana_rep_file = Extradosed.TotalAnalysis_Input_File;
                    ////else if (i == 1)
                    ////    ana_rep_file = Extradosed.TotalAnalysis_Input_File;
                    //else
                    ana_rep_file = Get_Live_Load_Analysis_Input_File(i + 1);

                    ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                    if (File.Exists(ana_rep_file))
                    {
                        All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                    }
                }
                //Deck_Analysis_DL.Bridge_Analysis = All_Analysis[0];
                Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;


                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];
            }

            Show_Moment_Shear_DL();
            Show_Moment_Shear_LL();

            Show_ReactionForces();



            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;

            iApp.Save_Form_Record(this, user_path);

            Button_Enable_Disable();


            iApp.Progress_Works.Clear();
        }

        private void Show_Member_Forces_British()
        {

            //List<string> Work_List = new List<string>();
            //Work_List.Add("Reading Analysis Data from Live Load Analysis Report File");
            //Work_List.Add("Reading Analysis Data from Dead Load Analysis Report File");

            //iApp.Progress_Works = new ProgressList(Work_List);


            string ana_rep_file = "";

            All_Analysis.Clear();


            ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.DeadLoadAnalysis_Input_File);

            if (File.Exists(ana_rep_file))
            {
                ////All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                Extradosed.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
            }

            ana_rep_file = MyList.Get_Analysis_Report_File(Extradosed.TotalAnalysis_Input_File);

            if (File.Exists(ana_rep_file))
            {
                ////All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                Extradosed.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
            }


            for (int i = 0; i < all_loads.Count; i++)
            {
                //if (i == 0)
                //    ana_rep_file = Extradosed.TotalAnalysis_Input_File;
                ////else if (i == 1)
                ////    ana_rep_file = Extradosed.TotalAnalysis_Input_File;
                //else
                ana_rep_file = Get_Live_Load_Analysis_Input_File(i + 1);

                ana_rep_file = MyList.Get_Analysis_Report_File(ana_rep_file);

                if (File.Exists(ana_rep_file))
                {
                    All_Analysis.Add(new BridgeMemberAnalysis(iApp, ana_rep_file));
                }
            }
            //Deck_Analysis_DL.Bridge_Analysis = All_Analysis[0];
            Deck_Analysis_DL.Bridge_Analysis = Extradosed.DeadLoad_Analysis;



            //List<double> frc = new List<double>();
            //List<double> moms = new List<double>();
            //for (int i = 0; i < Extradosed._Columns - 2; i++)
            //{
            //    var item = Extradosed.Long_Girder_Members_Array[2, i];

            //    var f = (AnalysisData) Extradosed.DeadLoad_Analysis.MemberAnalysis[item.MemberNo];
            //    if (f != null)
            //    {
            //        if (!frc.Contains(f.MaxShearForce.Force))
            //        frc.Add(f.MaxShearForce.Force);
            //        if (!moms.Contains(f.MaxBendingMoment.Force))
            //            moms.Add(f.MaxBendingMoment.Force);
            //    }
            //}

            ////moms.Add(0.0);

            //moms.Sort();
            //frc.Sort();






            Show_Moment_Shear_DL();

            Show_Moment_Shear_LL();

            //Show_Moment_Shear_LL_British();


            Show_ReactionForces();
        }

        void Show_ReactionForces()
        {
            #region Chiranjit [2012 10 31]

            if(cmb_cable_type.SelectedIndex != 2)
            Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

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

            txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
            txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
            txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

            txt_pier_2_P3.Text = Total_LiveLoad_Reaction;

            txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
            #endregion Chiranjit [2012 10 31]

        }


        private void btn_Ana_add_load_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { }
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

            if (!IsLiveLoad)
            {
                load_lst.AddRange(txt_Ana_LL_member_load.Lines);
                //if (dgv_Ana_live_load.RowCount != 0)
                //{
                //    if (!File.Exists(Deck_Analysis_DL.LiveLoad_File))
                //    {
                //        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                //    }
                //}
            }
            else
            {
                load_lst.Add("LOAD    1   " + s);
                load_lst.Add("MEMBER LOAD");
                load_lst.Add("1 TO 220 UNI GY -0.001");
                Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                //if (dgv_Ana_live_load.RowCount != 0)
                load_lst.AddRange(Get_MovingLoad_Data(Deck_Analysis_LL.Live_Load_List));
                //inp_file_cont.InsertRange(indx, );
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
        public void Show_ReadMemberLoad_Old(string file_name)
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
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    isMoving_load = true;

                    //continue;
                }

                if (kStr.Contains("DEFINE MOV"))
                {
                    mov_flag = false;
                    //continue;
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
            txt_Ana_LL_member_load.Lines = list_member_load.ToArray();
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
                    //if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    //dgv_Ana_live_load.Rows.Clear();
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
            if (IsDeadLoad)
                txt_Ana_LL_member_load.Lines = list_member_load.ToArray();


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
                    //if (mlist.Count == 3) txt_LL_load_gen.Text = mlist.StringList[2];
                    //dgv_Ana_live_load.Rows.Clear();
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
                    Segment_Girder.FilePath = user_path;
                    rcc_pier.FilePath = user_path;

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

        private void dgv_Ana_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
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

        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            //grb_Ana_DL_LL.Enabled = chk_Ana_DL_active_LL.Checked;
            //grb_SIDL.Enabled = chk_Ana_DL_active_SIDL.Checked;
        }
        #endregion  Composite Analysis Form Events

        #region Deck Methods

        private void Create_Data_DL(string file_name)
        {

            Deck_Analysis_DL.Input_File = file_name;
            Deck_Analysis_DL.CreateData_DeadLoad();
            Deck_Analysis_DL.WriteData_DeadLoad(Deck_Analysis_DL.Input_File, PSC_SECIONS);
            //Write_Ana_Load_Data(false);

            Ana_Write_Long_Girder_Load_Data(file_name, false, true, 1);

            Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_DL.Input_File);

            string ll_txt = Deck_Analysis_DL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_DL.Live_Load_List == null) return;

            //int prev
            //cmb_Ana_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_DL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_load_type.Items.Add(Deck_Analysis_DL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_DL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_load_type.SelectedIndex = cmb_Ana_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}
            Button_Enable_Disable();
        }
        private void Create_Data_LL(string file_name)
        {

            Deck_Analysis_LL.Input_File = file_name;
            Deck_Analysis_LL.CreateData();
            Deck_Analysis_LL.WriteData_LiveLoad(Deck_Analysis_LL.Input_File, PSC_SECIONS);


            Ana_Write_Long_Girder_Load_Data(file_name, true, false, 1);
            //Write_Ana_Load_Data(true);
            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis_LL.Input_File);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_LL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
            //for (int i = 0; i < Deck_Analysis_LL.Live_Load_List.Count; i++)
            //{
            //    cmb_Ana_load_type.Items.Add(Deck_Analysis_LL.Live_Load_List[i].TypeNo + " : " + Deck_Analysis_LL.Live_Load_List[i].Code);
            //}
            //if (cmb_Ana_load_type.Items.Count > 0)
            //{
            //    cmb_Ana_load_type.SelectedIndex = cmb_Ana_load_type.Items.Count - 1;
            //    //if (dgv_live_load.RowCount == 0)
            //    //Add_LiveLoad();
            //}


            Button_Enable_Disable();




        }

        List<MemberSectionProperty> SectionProperty { get; set; }


        private void Create_Data_Extradossed(string file_name)
        {

            Extradosed.Input_File = file_name;

            Extradosed.L2 = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Extradosed.L1 = MyList.StringToDouble(txt_L2.Text, 0.0);
            Extradosed.L3 = MyList.StringToDouble(txt_L3.Text, 0.0);
            Extradosed.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Extradosed.Cantilever_Width = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);


            Extradosed.Init_dist = MyList.StringToDouble(txt_init_cable.Text, 0.0);

            Extradosed.Cable_Nos = MyList.StringToInt(txt_cable_no.Text, 1) / 2;



            //Extradosed.IsCentral_Cable = cmb_cable_type.SelectedIndex == 1;

            Extradosed.Cable_x_dist = MyList.StringToDouble(txt_horizontal_cbl_dist.Text, 0.0);
            Extradosed.Cable_intv_dist = MyList.StringToDouble(txt_vertical_cbl_dist.Text, 0.0);
            Extradosed.Cable_y_dist = MyList.StringToDouble(txt_vertical_cbl_min_dist.Text, 0.0);


            Extradosed.cd = MyList.StringToDouble(txt_cable_dia.Text, 0.0);
            Extradosed.Bt = MyList.StringToDouble(txt_tower_Bt.Text, 0.0);
            Extradosed.Dt = MyList.StringToDouble(txt_tower_Dt.Text, 0.0);
            //Extradosed.tw = MyList.StringToDouble(txt_tower_tw.Text, 0.0);


            Extradosed.Support_Distance = MyList.StringToDouble(txt_support_distance.Text, 0.0);
            Extradosed.Effective_Distance = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            Extradosed.Support_Distance = 0.0;

            if (cmb_cable_type.SelectedIndex == 0)
            {
                Extradosed.IsCentral_Cable = false;
                Extradosed.Create_Extradossed_Data();
            }
            else
            {
                Extradosed.IsCentral_Cable = true;
                Extradosed.Create_Extradossed_Data_1_Side_Centre();
            }


            Extradosed.PSC_SECIONS = PSC_SECIONS;


            File.WriteAllLines(Path.Combine(user_path, "LoadCalculations.txt"), Extradosed.Load_Calculation().ToArray());
            //File.WriteAllLines(Path.Combine(user_path, "LoadCalculations.txt"), Extradosed.Load_Calculation_2017_02_14().ToArray());
            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                LONG_GIRDER_BRITISH_LL_TXT();
            else
                LONG_GIRDER_LL_TXT();

            Extradosed.WriteData_LiveLoad(Extradosed.Input_File, long_ll);

            txt_Ana_LL_member_load.Lines = Extradosed.joint_loads.ToArray();

            Extradosed.WriteData_LiveLoad(Extradosed.LiveLoadAnalysis_Input_File, long_ll);
            Ana_Write_Cable_Load_Data(Extradosed.LiveLoadAnalysis_Input_File, true, false, 1);

            Extradosed.WriteData_LiveLoad(Extradosed.TotalAnalysis_Input_File, long_ll);
            Ana_Write_Cable_Load_Data(Extradosed.TotalAnalysis_Input_File, true, true, 1);

            Extradosed.WriteData_LiveLoad(Extradosed.DeadLoadAnalysis_Input_File, long_ll);
            Ana_Write_Cable_Load_Data(Extradosed.DeadLoadAnalysis_Input_File, false, true, 1);


            for (int i = 0; i < all_loads.Count; i++)
            {
                Extradosed.WriteData_LiveLoad(Extradosed.Get_LL_Analysis_Input_File(i + 1), long_ll);
                Ana_Write_Cable_Load_Data(Extradosed.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
            }

            Button_Enable_Disable();

        }

         
        void Analysis_Initialize_InputData()
        {
            Deck_Analysis_DL.Length = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Deck_Analysis_DL.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Deck_Analysis_DL.WidthCantilever = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
            Deck_Analysis_DL.Skew_Angle = MyList.StringToDouble(txt_Ana_skew_angle.Text, 0.0);
            Deck_Analysis_DL.Effective_Depth = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);

            Deck_Analysis_LL.Length = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);
            Deck_Analysis_LL.WidthBridge = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            Deck_Analysis_LL.WidthCantilever = MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0);
            Deck_Analysis_LL.Skew_Angle = MyList.StringToDouble(txt_Ana_skew_angle.Text, 0.0);
            Deck_Analysis_LL.Effective_Depth = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            Deck_Analysis_LL.Start_Support = Start_Support_Text;
            Deck_Analysis_LL.End_Support = END_Support_Text;

            Deck_Analysis_DL.Start_Support = Start_Support_Text;
            Deck_Analysis_DL.End_Support = END_Support_Text;




        }



        //Chiranjit [2013 04 27]
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

            int i = 0;

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17

                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];

                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                for (i = 0; i < Extradosed._Columns - 2; i++)
                {
                    var item = Extradosed.Long_Girder_Members_Array[2, i];

                    var f = (AnalysisData)Deck_Analysis_LL.Bridge_Analysis.MemberAnalysis[item.MemberNo];
                    if (f != null)
                    {
                        if (!frc.Contains(f.MaxShearForce.Force))
                            frc.Add(f.MaxShearForce.Force);
                        if (!moms.Contains(f.MaxBendingMoment.Force))
                            moms.Add(f.MaxBendingMoment.Force);
                    }
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                txt_Ana_live_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                txt_Ana_live_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }

























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


            double eff_d = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            double val = L / 2;
            //int i = 0;

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
                Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[0] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
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
                Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[1] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
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
                Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[2] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
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
                Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[3] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Ton-m"));
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
                Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[4] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Ton-m"));
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
                Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                Box_Forces.FRC_LL_Shear[5] = force;

                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                Box_Forces.FRC_LL_Moment[5] = force;


                force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                Box_Forces.FRC_LL_Torsion[5] = force;

                _joints.Clear();
                _joints = null;
            }


            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_LL_British()
        {
            #region CC

            int i = 0;


            #region CC 2017 02 17


            

            List<double> frc = new List<double>();
            List<double> moms = new List<double>();
            for (i = 0; i < Extradosed._Columns - 2; i++)
            {
                var item = Extradosed.Long_Girder_Members_Array[2, i];

                var f = (AnalysisData)All_Analysis[0].MemberAnalysis[item.MemberNo];
                if (f != null)
                {
                    if (!frc.Contains(f.MaxShearForce.Force))
                        frc.Add(f.MaxShearForce.Force);
                    if (!moms.Contains(f.MaxBendingMoment.Force))
                        moms.Add(f.MaxBendingMoment.Force);
                }
            }

            //moms.Add(0.0);

            moms.Sort();
            frc.Sort();


            moms.Reverse();


            double dval = 0.0;

            int idx = frc.Count - 1;
            dval = frc[idx];
            txt_Ana_live_inner_long_support_shear.Text = Math.Abs(dval).ToString();

            idx = moms.Count - 1;
            dval = moms[idx];
            txt_Ana_live_inner_long_support_moment.Text = Math.Abs(dval).ToString();




            idx = frc.Count - 4;
            dval = frc[idx];
            txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

            idx = moms.Count - 6;
            dval = moms[idx];
            txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






            idx = frc.Count - frc.Count / 3;
            dval = frc[idx];
            txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


            idx = moms.Count / 4;
            dval = moms[idx];
            txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





            idx = frc.Count - frc.Count / 2;
            dval = frc[idx];
            txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


            idx = moms.Count / 8;
            dval = moms[idx];
            txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


            idx = frc.Count / 8;
            dval = frc[idx];
            txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


            idx = moms.Count / 10;
            dval = moms[idx];
            txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


            idx = 0;
            dval = frc[idx];
            txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(dval).ToString();


            idx = 0;
            dval = moms[idx];
            txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


            return;

            #endregion CC 2017 02 17


            
            
            return;


            #endregion CC








            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();


            Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Bridge_Analysis.Analysis.Members);
            JointNodeCollection jn_col = Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints;


            double L = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length;
            double W = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width;
            double cant_wi = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever;

            //double support = Deck_Analysis_LL.Bridge_Analysis.Supports.Count > 0 ? Deck_Analysis_LL.Bridge_Analysis.Supports[0].X : 0.5;
            double support = Deck_Analysis_LL.Support_Distance;
            support = Deck_Analysis_LL.Support_Distance;


            double eff_d = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


            double val = L / 2;

            List<int> _L2_joints = new List<int>();
            List<int> _L4_joints = new List<int>();
            List<int> _deff_joints = new List<int>();

            List<int> _L8_joints = new List<int>();
            List<int> _3L8_joints = new List<int>();
            List<int> _support_joints = new List<int>();


            #region Support Joints
            _joints.Clear();
            foreach (var item in Extradosed._supp_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }
            _support_joints.AddRange(_joints.ToArray());
            #endregion Support Joints


            #region Deff Joints
            _joints.Clear();

            foreach (var item in Extradosed._deff_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }
            #endregion Support Joints
            _deff_joints.AddRange(_joints.ToArray());



            #region L/8
            _joints.Clear();
            foreach (var item in Extradosed._L8_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }

            #endregion L/8

            _L8_joints.AddRange(_joints.ToArray());


            #region L/4
            _joints.Clear();
            foreach (var item in Extradosed._L8_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }
            #endregion L/4
            _L4_joints.AddRange(_joints.ToArray());

            #region 3L/8
            _joints.Clear();



            foreach (var item in Extradosed._L4_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }

            #endregion 3L/8
            _3L8_joints.AddRange(_joints.ToArray());

            #region L/2
            _joints.Clear();

            foreach (var item in Extradosed._L2_mems)
            {
                Member mbr = Extradosed.MemColls.GetMember(item);
                _joints.Add(mbr.StartNode.NodeNo);
            }


            #endregion L/2
            _L2_joints.AddRange(_joints.ToArray());


            //For Support
            //for (node = 12; node <= 22; node++) _joints.Add(node);
            //for (node = 122; node <= 132; node++) _joints.Add(node);

            Results.Clear();
            Results.Add("");
            Results.Add("");
            Results.Add("Analysis Result of Live Loads of PSC Box Girder Bridge");
            Results.Add("");
            Results.Add("");
            for (i = 1; i < All_Analysis.Count; i++)
            {
                Results.Add("");
                Results.Add("----------------------------------------------------------------");
                Results.Add(string.Format("STEP {0} : ANALYSIS RESULT OF {1}", i, all_loads[i - 1][0]));
                Results.Add("----------------------------------------------------------------");
                Results.Add("");
                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[i];

                #region Write Data to List

                #region _support_joints

                if (_support_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                    txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[0] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                    txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[0] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                    Box_Forces.FRC_LL_Torsion[0] = force;

                    _joints.Clear();
                }
                #endregion _support_joints


                #region _deff_joints

                //For Deff
                //for (node = 111; node <= 121; node++) _joints.Add(node);
                //for (node = 23; node <= 33; node++) _joints.Add(node);
                if (_deff_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                    txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[1] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                    txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[1] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                    Box_Forces.FRC_LL_Torsion[1] = force;

                    _joints.Clear();
                }
                #endregion _deff_joints



                #region _L8_joints

                //For L/8
                //for (node = 100; node <= 110; node++) _joints.Add(node);
                //for (node = 34; node <= 44; node++) _joints.Add(node);
                if (_L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                    txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[2] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                    txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[2] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                    Box_Forces.FRC_LL_Torsion[2] = force;

                    _joints.Clear();
                }

                #endregion _L8_joints


                #region _L4_joints

                //For L/4
                //for (node = 89; node <= 99; node++) _joints.Add(node);
                //for (node = 45; node <= 55; node++) _joints.Add(node);
                if (_L4_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                    txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[3] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                    txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[3] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L4_joints);
                    Box_Forces.FRC_LL_Torsion[3] = force;

                    _joints.Clear();
                }
                #endregion _L4_joints


                #region _3L8_joints
                //For 3L/8
                //for (node = 78; node <= 88; node++) _joints.Add(node);
                //for (node = 56; node <= 66; node++) _joints.Add(node);
                if (_3L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_3L8_joints);
                    txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                    txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[4] = force;

                    _joints.Clear();
                }
                #endregion _3L8_joints

                #region _L2_joints

                //For L/2
                //for (node = 67; node <= 77; node++) _joints.Add(node);
                if (_L2_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L2_joints);
                    txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[5] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                    txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[5] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[5] = force;

                    _joints.Clear();
                    //_joints = null;
                }
                #endregion _L2_joints

                #endregion Write Data to List
            }
            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_LL_British_2017_02_16()
        {
            List<int> _joints = new List<int>();
            int node = 0;
            MaxForce force = new MaxForce();


            Deck_Analysis_LL.Bridge_Analysis = All_Analysis[0];
            MemberCollection mc = new MemberCollection(Deck_Analysis_LL.Bridge_Analysis.Analysis.Members);
            JointNodeCollection jn_col = Deck_Analysis_LL.Bridge_Analysis.Analysis.Joints;


            double L = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length;
            double W = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width;
            double cant_wi = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever;

            //double support = Deck_Analysis_LL.Bridge_Analysis.Supports.Count > 0 ? Deck_Analysis_LL.Bridge_Analysis.Supports[0].X : 0.5;
            double support = Deck_Analysis_LL.Support_Distance;
            support = Deck_Analysis_LL.Support_Distance;


            double eff_d = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);


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



                #region CC


                if (Extradosed._L2_Dist.Contains(jn_col[i].X))
                {
                    _L2_joints.Add(jn_col[i].NodeNo);
                }
                if (Extradosed._L4_Dist.Contains(jn_col[i].X))
                {
                    _L4_joints.Add(jn_col[i].NodeNo);
                }
                if (Extradosed._L8_Dist.Contains(jn_col[i].X))
                {
                    _L8_joints.Add(jn_col[i].NodeNo);
                }
                if (Extradosed._3L8_Dist.Contains(jn_col[i].X))
                {
                    _3L8_joints.Add(jn_col[i].NodeNo);
                }
                if (Extradosed._Deff_Dist.Contains(jn_col[i].X))
                {
                    _deff_joints.Add(jn_col[i].NodeNo);
                }

                #endregion CC


                //if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                //{
                //    _L2_joints.Add(jn_col[i].NodeNo);
                //}
                //if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                //{
                //    _L4_joints.Add(jn_col[i].NodeNo);
                //}
                //if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                //{
                //    _L4_joints.Add(jn_col[i].NodeNo);
                //}



                //if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                //{
                //    _deff_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis_LL.Effective_Depth + x_min).ToString("0.0")))
                //{
                //    _deff_joints.Add(jn_col[i].NodeNo);
                //}



                //if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                //{
                //    _L8_joints.Add(jn_col[i].NodeNo);
                //}
                //if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                //{
                //    _L8_joints.Add(jn_col[i].NodeNo);
                //}


                //if ((jn_col[i].X.ToString("0.0") == ((3.0 * L / 8.0) + x_min).ToString("0.0")))
                //{
                //    _3L8_joints.Add(jn_col[i].NodeNo);
                //}
                //if ((jn_col[i].X.ToString("0.0") == ((L - (3.0 * L / 8.0)) + x_min).ToString("0.0")))
                //{
                //    _3L8_joints.Add(jn_col[i].NodeNo);
                //}

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
            for (i = 1; i < All_Analysis.Count; i++)
            {
                Results.Add("");
                Results.Add("----------------------------------------------------------------");
                Results.Add(string.Format("STEP {0} : ANALYSIS RESULT OF {1}", i, all_loads[i - 1][0]));
                Results.Add("----------------------------------------------------------------");
                Results.Add("");
                Deck_Analysis_LL.Bridge_Analysis = All_Analysis[i];

                #region Write Data to List

                #region _support_joints

                if (_support_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_support_joints);
                    txt_Ana_live_inner_long_support_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _support_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[0] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_support_joints);
                    txt_Ana_live_inner_long_support_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _support_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[0] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_support_joints);
                    Box_Forces.FRC_LL_Torsion[0] = force;

                    _joints.Clear();
                }
                #endregion _support_joints


                #region _deff_joints

                //For Deff
                //for (node = 111; node <= 121; node++) _joints.Add(node);
                //for (node = 23; node <= 33; node++) _joints.Add(node);
                if (_deff_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
                    txt_Ana_live_inner_long_deff_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _deff_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[1] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints);
                    txt_Ana_live_inner_long_deff_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _deff_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[1] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_deff_joints);
                    Box_Forces.FRC_LL_Torsion[1] = force;

                    _joints.Clear();
                }
                #endregion _deff_joints



                #region _L8_joints

                //For L/8
                //for (node = 100; node <= 110; node++) _joints.Add(node);
                //for (node = 34; node <= 44; node++) _joints.Add(node);
                if (_L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L8_joints);
                    txt_Ana_live_inner_long_L8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[2] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L8_joints);
                    txt_Ana_live_inner_long_L8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[2] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L8_joints);
                    Box_Forces.FRC_LL_Torsion[2] = force;

                    _joints.Clear();
                }

                #endregion _L8_joints


                #region _L4_joints

                //For L/4
                //for (node = 89; node <= 99; node++) _joints.Add(node);
                //for (node = 45; node <= 55; node++) _joints.Add(node);
                if (_L4_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L4_joints);
                    txt_Ana_live_inner_long_L4_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _L4_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[3] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L4_joints);
                    txt_Ana_live_inner_long_L4_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _L4_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[3] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_L4_joints);
                    Box_Forces.FRC_LL_Torsion[3] = force;

                    _joints.Clear();
                }
                #endregion _L4_joints


                #region _3L8_joints
                //For 3L/8
                //for (node = 78; node <= 88; node++) _joints.Add(node);
                //for (node = 56; node <= 66; node++) _joints.Add(node);
                if (_3L8_joints.Count > 0)
                {


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_3L8_joints);
                    txt_Ana_live_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _3L8_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_3L8_joints);
                    txt_Ana_live_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _3L8_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[4] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[4] = force;

                    _joints.Clear();
                }
                #endregion _3L8_joints

                #region _L2_joints

                //For L/2
                //for (node = 67; node <= 77; node++) _joints.Add(node);
                if (_L2_joints.Count > 0)
                {

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_L2_joints);
                    txt_Ana_live_inner_long_L2_shear.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _L2_joints, "Ton"));
                    Box_Forces.FRC_LL_Shear[5] = force;

                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_L2_joints);
                    txt_Ana_live_inner_long_L2_moment.Text = Math.Abs(force).ToString();
                    Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _L2_joints, "Ton-m"));
                    Box_Forces.FRC_LL_Moment[5] = force;


                    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_Torsion(_3L8_joints);
                    Box_Forces.FRC_LL_Torsion[5] = force;

                    _joints.Clear();
                    //_joints = null;
                }
                #endregion _L2_joints

                #endregion Write Data to List
            }
            File.WriteAllLines(Result_Report_LL, Results.ToArray());
            //iApp.RunExe(Result_Report_LL);
        }

        void Show_Moment_Shear_DL()
        {

            if (cmb_cable_type.SelectedIndex != 2)
            {
                #region CC 2017 02 17




                List<double> frc = new List<double>();
                List<double> moms = new List<double>();
                int memNo = -1;
                for (int i = 0; i < Extradosed._Columns - 2; i++)
                {

                    var item = Extradosed.Long_Girder_Members_Array[2, i];

                    if (cmb_cable_type.SelectedIndex == 0)
                        memNo = Extradosed.Long_Girder_Members_Array[2, i].MemberNo;
                    else
                        memNo = Extradosed.Long_Girder_Members_Array[1, i].MemberNo;




                    var f = (AnalysisData)Extradosed.DeadLoad_Analysis.MemberAnalysis[item.MemberNo];
                    if (f != null)
                    {
                        if (!frc.Contains(f.MaxShearForce.Force))
                            frc.Add(f.MaxShearForce.Force);
                        if (!moms.Contains(f.MaxBendingMoment.Force))
                            moms.Add(f.MaxBendingMoment.Force);
                    }
                }

                //moms.Add(0.0);

                moms.Sort();
                frc.Sort();


                moms.Reverse();


                double dval = 0.0;

                int idx = frc.Count - 1;
                dval = frc[idx];
                txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 1;
                dval = moms[idx];
                txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(dval).ToString();




                idx = frc.Count - 4;
                dval = frc[idx];
                txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(dval).ToString();

                idx = moms.Count - 6;
                dval = moms[idx];
                txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(dval).ToString();






                idx = frc.Count - frc.Count / 3;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 4;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(dval).ToString();





                idx = frc.Count - frc.Count / 2;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 8;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(dval).ToString();


                idx = frc.Count / 8;
                dval = frc[idx];
                txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(dval).ToString();


                idx = moms.Count / 10;
                dval = moms[idx];
                txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(dval).ToString();


                idx = 0;
                dval = frc[idx];
                txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(dval).ToString();



                idx = 0;
                dval = moms[idx];
                txt_Ana_dead_inner_long_L2_moment.Text = Math.Abs(dval).ToString();


                return;

                #endregion CC 2017 02 17
            }


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



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_support_shear.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_support_moment.Text = Math.Abs(force).ToString();

            Box_Forces.FRC_DL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[0] = force;


            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);






            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[1] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[1] = force;


            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[2] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[2] = force;


            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[3] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[3] = force;



            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);





            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[4] = force;


            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_dead_inner_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Moment[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 1);
            Box_Forces.FRC_DL_Torsion[4] = force;



            //L/2
            _joints.Clear();
            _joints.Add(7);




            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_dead_inner_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_DL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 1);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
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
            Results.AddRange(force.GetDetails("SUPPORT : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_support_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[0] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("SUPPORT : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_support_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[0] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[0] = force;




            //Deff
            _joints.Clear();
            _joints.Add(3);
            _joints.Add(11);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_deff_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[1] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("Effective Depth : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_deff_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[1] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[1] = force;



            //L/8
            _joints.Clear();
            _joints.Add(4);
            _joints.Add(10);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[2] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[2] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[2] = force;




            //L/4
            _joints.Clear();
            _joints.Add(5);
            _joints.Add(9);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L4_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[3] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/4 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L4_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[3] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[3] = force;

            //3L/8
            _joints.Clear();
            _joints.Add(6);
            _joints.Add(8);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_3L_8_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[4] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("3L/8 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_3L_8_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[4] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[4] = force;

            //L/2
            _joints.Clear();
            _joints.Add(7);

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX SHEAR FORCE", _joints, "Ton"));
            txt_Ana_live_outer_long_L2_shear.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Shear[5] = force;

            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_joints, 2);
            Results.AddRange(force.GetDetails("L/2 : MAX BENDING MOMENT", _joints, "Ton-m"));
            txt_Ana_live_outer_long_L2_moment.Text = Math.Abs(force).ToString();
            Box_Forces.FRC_SIDL_Moment[5] = force;



            force = Deck_Analysis_DL.Bridge_Analysis.GetJoint_Torsion(_joints, 2);
            Box_Forces.FRC_SIDL_Torsion[5] = force;

            File.WriteAllLines(Result_Report_DL, Results.ToArray());

            //iApp.RunExe(Result_Report_DL);
        }
        #region Show_Moment_Shear_LL_2011_10_26
        //void Show_Moment_Shear_LL_2011_10_26()
        //{

        //    List<int> _joints = new List<int>();
        //    int node = 0;
        //    MaxForce force = 0.0;

        //    //For Support
        //    for (node = 12; node <= 22; node++) _joints.Add(node);
        //    for (node = 122; node <= 132; node++) _joints.Add(node);

        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_support_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_support_shear.Text = force.ToString();
        //    _joints.Clear();

        //    //For Deff
        //    for (node = 111; node <= 121; node++) _joints.Add(node);
        //    for (node = 23; node <= 33; node++) _joints.Add(node);
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_deff_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_deff_shear.Text = force.ToString();
        //    _joints.Clear();

        //    //For L/8
        //    for (node = 100; node <= 110; node++) _joints.Add(node);
        //    for (node = 34; node <= 44; node++) _joints.Add(node);
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_L8_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_L8_shear.Text = force.ToString();
        //    _joints.Clear();



        //    //For L/4
        //    for (node = 89; node <= 99; node++) _joints.Add(node);
        //    for (node = 45; node <= 55; node++) _joints.Add(node);
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_L4_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_L4_shear.Text = force.ToString();
        //    _joints.Clear();

        //    //For 3L/8
        //    for (node = 78; node <= 88; node++) _joints.Add(node);
        //    for (node = 56; node <= 66; node++) _joints.Add(node);
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_3L_8_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_3L_8_shear.Text = force.ToString();
        //    _joints.Clear();



        //    //For L/2
        //    for (node = 67; node <= 77; node++) _joints.Add(node);
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_MomentForce(_joints);
        //    txt_Ana_live_inner_long_L2_moment.Text = force.ToString();
        //    force = Deck_Analysis_LL.Bridge_Analysis.GetJoint_ShearForce(_joints);
        //    txt_Ana_live_inner_long_L2_shear.Text = force.ToString();
        //    _joints.Clear();
        //    _joints = null;


        //}
        //void Show_Moment_Shear_DL_2011_10_17()
        //{
        //    MemberCollection mc = new MemberCollection(Deck_Analysis_DL.Bridge_Analysis.Analysis.Members);

        //    MemberCollection sort_membs = new MemberCollection();

        //    double z_min = double.MaxValue;
        //    double x = double.MaxValue;
        //    int indx = -1;

        //    int i = 0;
        //    int j = 0;

        //    List<double> list_z = new List<double>();

        //    List<MemberCollection> list_mc = new List<MemberCollection>();

        //    double last_z = 0.0;
        //    //double z_min = double.MaxValue;

        //    iApp.Progress_ON("Sorting Data..");
        //    while (mc.Count != 0)
        //    {
        //        indx = -1;
        //        for (i = 0; i < mc.Count; i++)
        //        {
        //            if (z_min > mc[i].StartNode.Z)
        //            {
        //                z_min = mc[i].StartNode.Z;
        //                indx = i;
        //            }
        //        }
        //        if (indx != -1)
        //        {

        //            if (!list_z.Contains(z_min))
        //                list_z.Add(z_min);

        //            sort_membs.Add(mc[indx]);
        //            mc.Members.RemoveAt(indx);
        //            z_min = double.MaxValue;
        //            iApp.SetProgressValue(sort_membs.Count, (sort_membs.Count + mc.Count));
        //        }
        //    }

        //    iApp.Progress_OFF();


        //    List<string> list_arr = new List<string>();


        //    last_z = -1.0;

        //    //Outer Long Girder
        //    MemberCollection outer_long = new MemberCollection();
        //    MemberCollection inner_long = new MemberCollection();
        //    MemberCollection inner_cross = new MemberCollection();


        //    //z_min = Truss_Analysis.Analysis.Joints.MinZ;
        //    //double z_max = Truss_Analysis.Analysis.Joints.MaxZ;

        //    //Chiranjit [2011 07 09]
        //    //Store Outer Girder Members
        //    int count = 0;
        //    z_min = 0.0;
        //    for (i = 0; i < sort_membs.Count; i++)
        //    {
        //        if (z_min < sort_membs[i].StartNode.Z)
        //        {
        //            z_min = sort_membs[i].StartNode.Z;
        //            count++;
        //        }
        //        if (z_min < sort_membs[i].EndNode.Z)
        //        {
        //            z_min = sort_membs[i].EndNode.Z;
        //            count++;
        //        }
        //        //For Outer Girder
        //        if (count == 2) break;
        //        //if (count == 0) break;
        //    }

        //    //z_min = WidthCantilever;
        //    double z_max = z_min;


        //    //Store inner and outer Long Girder
        //    for (i = 0; i < sort_membs.Count; i++)
        //    {
        //        if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
        //            sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
        //        {
        //            outer_long.Add(sort_membs[i]);
        //        }
        //        else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
        //            sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
        //        {
        //            inner_long.Add(sort_membs[i]);
        //        }
        //    }

        //    //Store Cross Girders
        //    for (i = 0; i < sort_membs.Count; i++)
        //    {
        //        if (outer_long.Contains(sort_membs[i]) == false &&
        //            inner_long.Contains(sort_membs[i]) == false)
        //        {
        //            inner_cross.Add(sort_membs[i]);
        //        }
        //    }


        //    //Print
        //    //OUTER LONG GIRDER
        //    list_arr.Add("");
        //    list_arr.Add("OUTER LONG GIRDERS");
        //    list_arr.Add("");
        //    list_arr.Add("");
        //    for (j = 0; j < outer_long.Count; j++)
        //    {
        //        //mc = outer_long[i];
        //        list_arr.Add(string.Format("{0,-10} {1} {2}",
        //     outer_long[j].MemberNo, outer_long[j].StartNode, outer_long[j].EndNode));
        //    }

        //    list_arr.Add("");
        //    list_arr.Add("INNER LONG GIRDERS");
        //    list_arr.Add("");
        //    for (j = 0; j < inner_long.Count; j++)
        //    {
        //        //mc = inner_long[i];
        //        list_arr.Add(string.Format("{0,-10} {1} {2}",
        //     inner_long[j].MemberNo, inner_long[j].StartNode, inner_long[j].EndNode));
        //    }


        //    list_arr.Add("");
        //    list_arr.Add("ALL CROSS GIRDERS");
        //    list_arr.Add("");
        //    for (j = 0; j < inner_cross.Count; j++)
        //    {
        //        //mc = inner_cross[i];

        //        list_arr.Add(string.Format("{0,-10} {1} {2}",
        //     inner_cross[j].MemberNo, inner_cross[j].StartNode, inner_cross[j].EndNode));
        //    }


        //    //Find X MIN    X MAX   for outer long girder
        //    double x_min, x_max;

        //    List<double> list_outer_xmin = new List<double>();
        //    List<double> list_inner_xmin = new List<double>();
        //    List<double> list_inner_cur_z = new List<double>();
        //    List<double> list_outer_cur_z = new List<double>();

        //    List<double> list_outer_xmax = new List<double>();
        //    List<double> list_inner_xmax = new List<double>();


        //    x_min = double.MaxValue;
        //    x_max = double.MinValue;

        //    last_z = outer_long[0].StartNode.Z;
        //    for (i = 0; i < outer_long.Count; i++)
        //    {
        //        if (last_z == outer_long[i].StartNode.Z)
        //        {
        //            if (x_min > outer_long[i].StartNode.X)
        //            {
        //                x_min = outer_long[i].StartNode.X;
        //            }
        //            if (x_max < outer_long[i].EndNode.X)
        //            {
        //                x_max = outer_long[i].EndNode.X;
        //            }
        //        }
        //        else
        //        {
        //            list_outer_xmax.Add(x_max);
        //            list_outer_xmin.Add(x_min);
        //            list_outer_cur_z.Add(last_z);

        //            x_min = outer_long[i].StartNode.X;
        //            x_max = outer_long[i].EndNode.X;


        //        }
        //        last_z = outer_long[i].StartNode.Z;
        //    }

        //    list_outer_xmax.Add(x_max);
        //    list_outer_xmin.Add(x_min);
        //    list_outer_cur_z.Add(last_z);

        //    x_min = double.MaxValue;
        //    x_max = double.MinValue;

        //    last_z = inner_long[0].StartNode.Z;

        //    for (i = 0; i < inner_long.Count; i++)
        //    {
        //        if (last_z == inner_long[i].StartNode.Z)
        //        {
        //            if (x_min > inner_long[i].StartNode.X)
        //            {
        //                x_min = inner_long[i].StartNode.X;
        //            }
        //            if (x_max < inner_long[i].EndNode.X)
        //            {
        //                x_max = inner_long[i].EndNode.X;
        //            }
        //        }
        //        else
        //        {
        //            list_inner_xmax.Add(x_max);
        //            list_inner_xmin.Add(x_min);
        //            list_inner_cur_z.Add(last_z);

        //            x_min = inner_long[i].StartNode.X;
        //            x_max = inner_long[i].EndNode.X;

        //        }
        //        last_z = inner_long[i].StartNode.Z;
        //    }

        //    list_inner_xmax.Add(x_max);
        //    list_inner_xmin.Add(x_min);

        //    list_inner_cur_z.Add(last_z);

        //    List<int> _deff_joints = new List<int>();
        //    List<int> _L_4_joints = new List<int>();
        //    List<int> _L_2_joints = new List<int>();
        //    //Member Forces from Report for Inner girder


        //    //int cur_node = -1;
        //    int cur_member = -1;
        //    // FOR L/2
        //    string curr_membs_L2_text = "";
        //    // FOR L/4
        //    string curr_membs_L4_text = "";
        //    //FOR Effective Depth
        //    string curr_membs_Deff_text = "";


        //    double cur_z = 0.0;
        //    double cur_y = 0.0;

        //    double curr_L2_x = 0.0;
        //    double curr_L4_x = 0.0;
        //    double curr_Deff_x = 0.0;

        //    curr_membs_L2_text = "";
        //    curr_membs_L4_text = "";
        //    curr_membs_Deff_text = "";
        //    cur_member = -1;

        //    if (outer_long.Count > 0)
        //        Deck_Analysis_DL.Effective_Depth = outer_long[0].Length;

        //    for (i = 0; i < list_inner_xmax.Count; i++)
        //    {
        //        x_max = list_inner_xmax[i];
        //        x_min = list_inner_xmin[i];

        //        cur_z = list_inner_cur_z[i];

        //        curr_L2_x = (x_max + x_min) / 2.0;
        //        curr_L4_x = (curr_L2_x + x_min) / 2.0;
        //        curr_Deff_x = (Deck_Analysis_DL.Effective_Depth + x_min);

        //        cur_y = 0.0;

        //        for (j = 0; j < inner_long.Count; j++)
        //        {
        //            if ((inner_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
        //                (inner_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
        //            {
        //                if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
        //                {
        //                    cur_member = inner_long[j].MemberNo;
        //                    curr_membs_L2_text += cur_member + " ";
        //                    _L_2_joints.Add(inner_long[j].EndNode.NodeNo);
        //                }
        //                else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
        //                {
        //                    cur_member = inner_long[j].MemberNo;
        //                    curr_membs_L4_text += cur_member + " ";
        //                    _L_4_joints.Add(inner_long[j].EndNode.NodeNo);
        //                }
        //                else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
        //                {
        //                    cur_member = inner_long[j].MemberNo;
        //                    curr_membs_Deff_text += cur_member + " ";
        //                    _deff_joints.Add(inner_long[j].EndNode.NodeNo);
        //                }
        //            }
        //        }
        //    }

        //    // FOR Inner Long Girder
        //    _L_2_joints.Remove(64);
        //    double val = 0.0;
        //    val = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_2_joints);
        //    txt_Ana_DL_inner_long_L2_moment.Text = val.ToString();


        //    txt_Ana_dead_inner_long_L2_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_2_joints).ToString();
        //    txt_Ana_dead_inner_long_L4_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_4_joints).ToString();
        //    txt_Ana_dead_inner_long_deff_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints).ToString();



        //    //txt_DL_BM_IG.Text = (val * 10.0).ToString();
        //    //txt_DL_BM_IG.ForeColor = Color.Red;

        //    _deff_joints.Remove(20);
        //    val = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
        //    txt_Ana_DL_inner_long_deff_shear.Text = val.ToString();

        //    txt_Ana_dead_inner_long_deff_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints).ToString();
        //    txt_Ana_dead_inner_long_L2_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_L_2_joints).ToString();
        //    txt_Ana_dead_inner_long_L4_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_L_4_joints).ToString();


        //    //txt_DL_SF_IG.Text = (val * 10.0).ToString();
        //    //txt_DL_SF_IG.ForeColor = Color.Red;


        //    //For Outer Long Girder
        //    curr_membs_L2_text = "";
        //    curr_membs_L4_text = "";
        //    curr_membs_Deff_text = "";
        //    cur_member = -1;
        //    _deff_joints.Clear();
        //    _L_2_joints.Clear();
        //    _L_4_joints.Clear();
        //    //Creating X Coordinates at every Z level
        //    for (i = 0; i < list_outer_xmax.Count; i++)
        //    {
        //        x_max = list_outer_xmax[i];
        //        x_min = list_outer_xmin[i];

        //        cur_z = list_outer_cur_z[i];

        //        curr_L2_x = (x_max + x_min) / 2.0;
        //        curr_L4_x = (curr_L2_x + x_min) / 2.0;
        //        curr_Deff_x = (Deck_Analysis_DL.Effective_Depth + x_min);

        //        cur_y = 0.0;

        //        for (j = 0; j < outer_long.Count; j++)
        //        {
        //            if ((outer_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
        //                (outer_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
        //            {
        //                if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
        //                {
        //                    cur_member = outer_long[j].MemberNo;
        //                    curr_membs_L2_text += cur_member + " ";
        //                    _L_2_joints.Add(outer_long[j].EndNode.NodeNo);

        //                }
        //                else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
        //                {
        //                    cur_member = outer_long[j].MemberNo;
        //                    curr_membs_L4_text += cur_member + " ";
        //                    _L_4_joints.Add(outer_long[j].EndNode.NodeNo);
        //                }
        //                else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
        //                {
        //                    cur_member = outer_long[j].MemberNo;
        //                    curr_membs_Deff_text += cur_member + " ";
        //                    _deff_joints.Add(outer_long[j].EndNode.NodeNo);
        //                }
        //            }
        //        }
        //    }
        //    _L_2_joints.Add(64);
        //    val = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_2_joints);
        //    txt_Ana_DL_outer_long_L2_moment.Text = val.ToString();

        //    txt_Ana_dead_outer_long_L2_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_2_joints).ToString();
        //    txt_Ana_dead_outer_long_L4_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_L_4_joints).ToString();
        //    txt_Ana_dead_outer_long_deff_moment.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_MomentForce(_deff_joints).ToString();


        //    //txt_DL_BM_OG.Text = "" + (val * 10);
        //    //txt_DL_BM_OG.ForeColor = Color.Red;



        //    _deff_joints.Add(20);
        //    val = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints);
        //    txt_Ana_DL_outer_long_deff_shear.Text = val.ToString();

        //    txt_Ana_dead_outer_long_deff_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_deff_joints).ToString();
        //    txt_Ana_dead_outer_long_L2_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_L_2_joints).ToString();
        //    txt_Ana_dead_outer_long_L4_shear.Text = Deck_Analysis_DL.Bridge_Analysis.GetJoint_ShearForce(_L_4_joints).ToString();


        //    //txt_DL_SF_OG.Text = "" + (val * 10);
        //    //txt_DL_SF_OG.ForeColor = Color.Red;

        //    //Cross Girder
        //    string cross_text = "";
        //    for (j = 0; j < inner_cross.Count; j++)
        //    {

        //        cur_member = inner_cross[j].MemberNo;
        //        cross_text += cur_member + " ";
        //    }

        //    CMember m = new CMember();
        //    m.Group.MemberNosText = cross_text;
        //    m.Force = Deck_Analysis_DL.Bridge_Analysis.GetForce(ref m);
        //    //txt_Ana_dead_cross_max_moment.Text = (m.MaxMoment).ToString();
        //    //txt_Ana_dead_cross_max_shear.Text = m.MaxShearForce.ToString();
        //    //Write_Max_Moment_Shear();

        //}
        #endregion Show_Moment_Shear_LL_2011_10_26
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
            //btn_Ana_DL_view_data.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
            //btn_Ana_DL_view_structure.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
            //btn_Ana_LL_view_structure.Enabled = File.Exists(Deck_Analysis_LL.Input_File);
            //btn_Ana_DL_view_report.Enabled = File.Exists(Deck_Analysis_DL.Analysis_Report);

            //btn_Ana_LL_view_data.Enabled = File.Exists(Deck_Analysis_LL.Input_File);
            //btn_Ana_LL_view_report.Enabled = File.Exists(Deck_Analysis_LL.Analysis_Report);

            if(cmb_cable_type.SelectedIndex == 2)
                btn_Process_LL_Analysis.Enabled = File.Exists(Deck_Analysis_DL.Input_File);
            else
                btn_Process_LL_Analysis.Enabled = File.Exists(Extradosed.Input_File);


            btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
            //btn_dwg_pier.Enabled = File.Exists(rcc_pier.rep_file_name);

            //btn_long_Report.Enabled = File.Exists(LongGirder.rep_file_name);
            //btnReport.Enabled = File.Exists(Deck.rep_file_name);
            //btn_dwg_rcc_deck.Enabled = File.Exists(Deck.drawing_path);
            //btn_dwg_main_girder.Enabled = File.Exists(LongGirder.user_drawing_file);


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
                //wrkg_folder = Path.GetDirectoryName(analysis_file);
                //wrkg_folder = Path.Combine(wrkg_folder, "Dead Load Analysis");


                //dl_file = Path.Combine(wrkg_folder, "Input_Data_DL.txt");
                //Deck_Analysis_DL.Input_File = dl_file;
                //Create_Data_Extradossed(Input_File);


                //if (File.Exists(dl_file))
                //{
                //    flag = true;
                //    Deck_Analysis_DL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, dl_file);
                //    Show_Moment_Shear_DL();
                //}

                //wrkg_folder = Path.GetDirectoryName(analysis_file);
                //wrkg_folder = Path.Combine(wrkg_folder, "Live Load Analysis");



                //ll_file = Path.Combine(wrkg_folder, "Input_Data_LL.txt");

                //if (!File.Exists(ll_file))
                //    ll_file = Get_Live_Load_Analysis_Input_File(1);

                //Deck_Analysis_LL.Input_File = ll_file;



                //if (File.Exists(ll_file))
                //{
                //    Deck_Analysis_LL.Bridge_Analysis = null;
                //    Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);
                //    Show_Moment_Shear_LL();
                //}

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
                                txt_Ana_skew_angle.Text = Deck_Analysis_LL.Skew_Angle.ToString();
                            }
                        }

                        txt_Ana_L1.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Length.ToString();
                        txt_Ana_B.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();
                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();

                        txt_support_distance.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        txt_Ana_DL_eff_depth.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        txt_Ana_width_cantilever.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        txt_Ana_skew_angle.Text = Deck_Analysis_LL.Bridge_Analysis.Analysis.Skew_Angle.ToString();

                        txt_gd_np.Text = (Deck_Analysis_LL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                        txt_Ana_analysis_file.Visible = true;
                        txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");
                    }
                    else
                    {
                        //txt_Ana_L1.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Length.ToString();
                        //txt_Ana_B.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width.ToString();
                        //txt_gd_np.Text = (Deck_Analysis_DL.Bridge_Analysis.Analysis.NoOfPanels - 1).ToString("0");

                        //txt_support_distance.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Support_Distance.ToString();

                        //txt_Ana_DL_eff_depth.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Effective_Depth.ToString();
                        //txt_Ana_width_cantilever.Text = Deck_Analysis_DL.Bridge_Analysis.Analysis.Width_Cantilever.ToString();

                        //txt_Ana_analysis_file.Visible = true;
                        //txt_Ana_analysis_file.Text = analysis_file;
                        //MessageBox.Show(this, "File opened successfully.");

                        //Extradosed.DeadLoad_Analysis.Analysis.Length
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
                //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");

                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);


                //Deck_Analysis_LL.LoadReadFromGrid(dgv_Ana_live_load);

                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                foreach (LoadData ld in Deck_Analysis_LL.LoadList)
                {
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }
                load_lst.Add("LOAD GENERATION " + txt_LRFD_LL_load_gen.Text);
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


        private void frm_Extradosed_Load(object sender, EventArgs e)
        {

            tc_abutment.TabPages.Remove(tab_AbutmentOpenLSM);


            
            tc_main.TabPages.Remove(tab_Segment);
            tc_bridge_deck.TabPages.Add(tab_Segment);
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));

                //grb_ll_indian.Visible = false;
                //pic_diagram.Size = new Size(pic_diagram.Size.Width, 280);

                cmb_HB.SelectedIndex = 0;

                British_Interactive();



                tbc_girder.TabPages.Remove(tab_moving_data_LRFD);
                tbc_girder.TabPages.Remove(tab_moving_data_indian);
                //tc_main.TabPages.Remove(tab_rcc_abutment);

            }
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                tbc_girder.TabPages.Remove(tab_moving_data_british);
                tbc_girder.TabPages.Remove(tab_moving_data_indian);
                //tbc_girder.TabPages.Remove(tab_moving_data_LRFD);
            }
            else
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                tbc_girder.TabPages.Remove(tab_moving_data_british);
                tbc_girder.TabPages.Remove(tab_moving_data_LRFD);

            }

            uC_CableStayedDesign1.iApp = iApp;

            //txt_Ana_B.Text = "9.750";





            Deck_Analysis_DL = new BridgeAnalysisDesign.PSC_BoxGirder.PSC_BoxGirderAnalysis(iApp);
            Deck_Analysis_LL = new BridgeAnalysisDesign.PSC_BoxGirder.PSC_BoxGirderAnalysis(iApp);

            Extradosed = new CABLE_STAYED_Extradosed_LS_Analysis(iApp);

            LongGirder = new PostTensionLongGirder(iApp);
            Deck = new RccDeckSlab(iApp);

            #region RCC Pier
            cmb_pier_2_k.SelectedIndex = 1;
            rcc_pier = new RccPier(iApp);
            //pic_pier_interactive_diagram.BackgroundImage = AstraFunctionOne.ImageCollection.Pier_drawing

            cmb_rcc_pier_fck.SelectedIndex = 4;
            cmb_rcc_pier_fy.SelectedIndex = 1;




            uC_PierDesignLSM1.iApp = iApp;
            #endregion RCC Pier

            #region IRC Abutment

            //tc_limit_design.TabPages.Remove(tab_abutment);
            uC_RCC_Abut1.iApp = iApp;
            uC_RCC_Abut1.Load_Data();
            uC_RCC_Abut1.Is_Individual = false;

            #endregion IRC Abutment




            Button_Enable_Disable();
            Load_Tab2_Tab3_Box_Segment_Data();
            Update_Tab3_Data();
            cmb_tab1_Fcu.SelectedIndex = 6;
            cmb_tab1_Fy.SelectedIndex = 1;
            //cmb_main_DL_BM.SelectedIndex = 2;
            //cmb_main_LL_BM.SelectedIndex = 2;
            //cmb_main_DL_SF.SelectedIndex = 0;
            //cmb_main_LL_SF.SelectedIndex = 0;


            //Chiranjit [2013 06 19]
            cmb_tab2_strand_data.SelectedIndex = 3;
            cmb_tab2_nc.SelectedIndex = 5;

            //Chiranjit [2017 02 20]
            if (Project_Type == eASTRADesignType.Extradossed_Central_Towers_Bridge_LS)
                cmb_cable_type.SelectedIndex = 1;
            else
                cmb_cable_type.SelectedIndex = 0;

            //Open_Project();
            Set_Project_Name();

            uC_BoxGirder1.iApp = iApp;

            uC_AbutmentPileLS1.SetIApplication(iApp);
            uC_PierDesignLSM1.iApp = iApp;
            uC_PierOpenLS1.SetIApplication(iApp);

            #region Add Limit State Method Live Loads

            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                Default_LRFD_Moving_LoadData(dgv_LRFD_long_liveloads);
                Default_LRFD_Moving_Type_LoadData(dgv_LRFD_long_loads);
            }
            else
            {
                Default_Moving_LoadData(dgv_long_liveloads);
                Default_Moving_Type_LoadData(dgv_long_loads);
            }


            cmb_HB.SelectedIndex = 0;
            British_Interactive();

            //tc_limit_design.TabPages.Remove(tab_deck_slab);

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("DL + LL COMBINE ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LONGITUDINAL GIRDER ANALYSIS RESULTS"));

                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
                //tc_limit_design.TabPages.Remove(tab_deck_slab_IS);
            }
            #endregion Add Limit State Method Live Loads

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                Select_Moving_Load_Combo(dgv_british_loads, cmb_bs_view_moving_load);
            }
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                Select_Moving_Load_Combo(dgv_british_loads, cmb_bs_view_moving_load);
            }
            else
                Select_Moving_Load_Combo(dgv_long_loads, cmb_irc_view_moving_load);
        }

        public void Select_Moving_Load_Combo(DataGridView dgv, ComboBox cmb)
        {
            string load = "";
            cmb.Items.Clear();
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                load = dgv[0, i].Value.ToString();
                if (load.StartsWith("LOAD"))
                {
                    if (!cmb.Items.Contains(load))
                        cmb.Items.Add(load);
                }
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;

        }
        void Cable_Structure_Load()
        {

        }
        private void Open_Project()
        {


            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{

                IsCreate_Data = false;


                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                Segment_Girder.FilePath = user_path;
                rcc_pier.FilePath = user_path;
                uC_RCC_Abut1.iApp = iApp;
                Show_Section_Result();

                Analysis_Initialize_InputData();


                Set_Segment_Data();
                //Open_AnalysisFile(chk_file);

                Create_Data_Extradossed(Input_File);

                //Chiranjit [2013 04 26]
                //iApp.Read_Form_Record(this, user_path);


                //Chiranjit [2013 10 09]
                IsRead = true;
                iApp.Read_Form_Record(this, user_path);
                IsRead = false;


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    British_Interactive();
                    Default_British_HB_Type_LoadData(dgv_british_loads);
                }

                Set_Box_Forces();
                Set_Segment_Data();
                Segment_Girder.FilePath = user_path;
                rcc_pier.FilePath = user_path;

                txt_Ana_analysis_file.Text = chk_file;


                uC_RCC_Abut1.Modified_Cells();



                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

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

        private void cmb_pier_2_k_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_pier_2_k.SelectedIndex)
            {
                case 0: txt_pier_2_k.Text = "1.50"; break;
                case 1: txt_pier_2_k.Text = "0.66"; break;
                case 2: txt_pier_2_k.Text = "0.50"; break;
                case 3: txt_pier_2_k.Text = ""; txt_pier_2_k.Focus(); break;
            }
        }

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rcc_pier.rep_file_name);

        }
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_rcc_pier") || ctrl.Name.ToLower().StartsWith("txt_rcc_pier"))
            {
                astg = new ASTRAGrade(cmb_rcc_pier_fck.Text, cmb_rcc_pier_fy.Text);
                txt_rcc_pier_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_rcc_pier_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }
        }

        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 10 30]
            Write_All_Data();



            double MX1, MY1, W1;

            MX1 = MY1 = W1 = 0.0;

            MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            if (MX1 == 0.0 && MY1 == 0.0 && W1 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : W1  = 6101.1 kN\n";
                msg += "            : MX1 = 274.8 kN-m\n";
                msg += "            : MZ1 = 603.1 kN-m\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                if (rcc_pier == null) rcc_pier = new RccPier(iApp);
                rcc_pier.FilePath = user_path;
                RCC_Pier_Initialize_InputData();
                rcc_pier.Calculate_Program();
                //rcc_pier.Write_User_Input();
                rcc_pier.Write_Drawing_File();
                iApp.Save_Form_Record(this, user_path);
                if (File.Exists(rcc_pier.rep_file_name)) { MessageBox.Show(this, "Report file written in " + rcc_pier.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rcc_pier.rep_file_name); }
                rcc_pier.is_process = true;
            }
            Button_Enable_Disable();
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
        public void RCC_Pier_Initialize_InputData()
        {
            rcc_pier.L1 = 0.0d;
            rcc_pier.W1 = 0.0d;
            rcc_pier.W2 = 0.0d;
            rcc_pier.W3 = 0.0d;
            rcc_pier.W4 = 0.0d;
            rcc_pier.W5 = 0.0d;
            rcc_pier.total_vehicle_load = 0.0d;
            rcc_pier.D1 = 0.0d;
            rcc_pier.D2 = 0.0d;
            rcc_pier.D3 = 0.0d;

            rcc_pier.RL6 = 0.0d;
            rcc_pier.RL5 = 0.0d;
            rcc_pier.RL4 = 0.0d;
            rcc_pier.RL3 = 0.0d;
            rcc_pier.RL2 = 0.0d;
            rcc_pier.RL1 = 0.0d;
            rcc_pier.H1 = 0.0d;
            rcc_pier.H2 = 0.0d;
            rcc_pier.H3 = 0.0d;
            rcc_pier.H4 = 0.0d;
            rcc_pier.H5 = 0.0d;
            rcc_pier.H6 = 0.0d;
            rcc_pier.H7 = 0.0d;
            rcc_pier.H8 = 0.0d;
            rcc_pier.B1 = 0.0d;
            rcc_pier.B2 = 0.0d;
            rcc_pier.B3 = 0.0d;
            rcc_pier.B4 = 0.0d;
            rcc_pier.B5 = 0.0d;
            rcc_pier.B6 = 0.0d;
            rcc_pier.B7 = 0.0d;
            rcc_pier.B8 = 0.0d;
            rcc_pier.B9 = 0.0d;
            rcc_pier.B10 = 0.0d;
            rcc_pier.B11 = 0.0d;
            rcc_pier.B12 = 0.0d;
            rcc_pier.B13 = 0.0d;
            rcc_pier.B14 = 0.0d;
            rcc_pier.B15 = 1.078d;
            rcc_pier.B16 = 0.0d;
            rcc_pier.NR = 0.0d;
            rcc_pier.NP = 0.0d;
            rcc_pier.gama_c = 0.0d;
            rcc_pier.MX1 = 0.0d;
            rcc_pier.MY1 = 0.0d;
            rcc_pier.sigma_s = 0.0d;

            #region Data Input Form 1 Variables
            rcc_pier.L1 = MyList.StringToDouble(txt_RCC_Pier_L.Text, 0.0);
            rcc_pier.w1 = MyList.StringToDouble(txt_RCC_Pier_CW.Text, 0.0);
            rcc_pier.w2 = MyList.StringToDouble(txt_RCC_Pier__B.Text, 0.0);
            rcc_pier.w3 = MyList.StringToDouble(txt_RCC_Pier_Wp.Text, 0.0);


            rcc_pier.a1 = MyList.StringToDouble(txt_RCC_Pier_Hp.Text, 0.0);
            rcc_pier.NB = MyList.StringToDouble(txt_RCC_Pier_NMG.Text, 0.0);
            rcc_pier.d1 = MyList.StringToDouble(txt_RCC_Pier_DMG.Text, 0.0);
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_DS.Text, 0.0);
            rcc_pier.gama_c = MyList.StringToDouble(txt_RCC_Pier_gama_c.Text, 0.0);
            rcc_pier.B1 = MyList.StringToDouble(txt_RCC_Pier_B1.Text, 0.0);
            rcc_pier.B2 = MyList.StringToDouble(txt_RCC_Pier_B2.Text, 0.0);
            rcc_pier.H1 = MyList.StringToDouble(txt_RCC_Pier_H1.Text, 0.0);
            rcc_pier.B3 = MyList.StringToDouble(txt_RCC_Pier_B3.Text, 0.0);
            rcc_pier.B4 = MyList.StringToDouble(txt_RCC_Pier_B4.Text, 0.0);
            rcc_pier.H2 = MyList.StringToDouble(txt_RCC_Pier_H2.Text, 0.0);
            rcc_pier.B5 = MyList.StringToDouble(txt_RCC_Pier_B5.Text, 0.0);
            rcc_pier.B6 = MyList.StringToDouble(txt_RCC_Pier_B6.Text, 0.0);
            rcc_pier.RL1 = MyList.StringToDouble(txt_RCC_Pier_RL1.Text, 0.0);
            rcc_pier.RL2 = MyList.StringToDouble(txt_RCC_Pier_RL2.Text, 0.0);
            rcc_pier.RL3 = MyList.StringToDouble(txt_RCC_Pier_RL3.Text, 0.0);
            rcc_pier.RL4 = MyList.StringToDouble(txt_RCC_Pier_RL4.Text, 0.0);
            rcc_pier.RL5 = MyList.StringToDouble(txt_RCC_Pier_RL5.Text, 0.0);
            rcc_pier.form_lev = MyList.StringToDouble(txt_RCC_Pier_Form_Lev.Text, 0.0);
            rcc_pier.B7 = MyList.StringToDouble(txt_RCC_Pier_B7.Text, 0.0);
            rcc_pier.H3 = MyList.StringToDouble(txt_RCC_Pier_H3.Text, 0.0);
            rcc_pier.H4 = MyList.StringToDouble(txt_RCC_Pier_H4.Text, 0.0);
            rcc_pier.B8 = MyList.StringToDouble(txt_RCC_Pier_B8.Text, 0.0);
            rcc_pier.H5 = MyList.StringToDouble(txt_RCC_Pier_H5.Text, 0.0);
            rcc_pier.H6 = MyList.StringToDouble(txt_RCC_Pier_H6.Text, 0.0);
            rcc_pier.H7 = MyList.StringToDouble(txt_RCC_Pier_H7.Text, 0.0);
            rcc_pier.B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            rcc_pier.B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            rcc_pier.B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            rcc_pier.B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            rcc_pier.B13 = MyList.StringToDouble(txt_RCC_Pier_B13.Text, 0.0);
            rcc_pier.B14 = MyList.StringToDouble(txt_RCC_Pier___B.Text, 0.0);
            rcc_pier.over_all = rcc_pier.H7 + rcc_pier.H5 + rcc_pier.H6;
            //rcc_pier.B15 = MyList.StringToDouble(txt_RCC_Pier_B15.Text, 0.0);


            rcc_pier.p1 = MyList.StringToDouble(txt_RCC_Pier_p1.Text, 0.0);
            rcc_pier.p2 = MyList.StringToDouble(txt_RCC_Pier_p2.Text, 0.0);
            rcc_pier.d_dash = MyList.StringToDouble(txt_RCC_Pier_d_dash.Text, 0.0);
            rcc_pier.D = MyList.StringToDouble(txt_RCC_Pier_D.Text, 0.0);
            rcc_pier.b = MyList.StringToDouble(txt_RCC_Pier_b.Text, 0.0);

            //rcc_pier.Pu = MyList.StringToDouble(txt_Pu.Text, 0.0);
            //rcc_pier.Mux = MyList.StringToDouble(txt_Mux.Text, 0.0);
            //rcc_pier.Muy = MyList.StringToDouble(txt_Muy.Text, 0.0);
            rcc_pier.NP = MyList.StringToDouble(txt_RCC_Pier_NP.Text, 0.0);
            rcc_pier.NR = MyList.StringToDouble(txt_RCC_Pier_NR.Text, 0.0);
            rcc_pier.MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            rcc_pier.MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            rcc_pier.total_vehicle_load = MyList.StringToDouble(txt_RCC_Pier_vehi_load.Text, 0.0);
            rcc_pier.W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);

            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);
            #endregion Data Input Form 1 Variables

            #region Data Input Form 2 Variables
            rcc_pier.P2 = MyList.StringToDouble(txt_pier_2_P2.Text, 0.0);
            rcc_pier.P3 = MyList.StringToDouble(txt_pier_2_P3.Text, 0.0);

            rcc_pier.B16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);
            //rcc_pier.total_pairs = MyList.StringToDouble(txt_pier_2_total_pairs.Text, 0.0);
            rcc_pier.PL = MyList.StringToDouble(txt_pier_2_PL.Text, 0.0);
            rcc_pier.PML = MyList.StringToDouble(txt_pier_2_PML.Text, 0.0);
            rcc_pier.APD = txt_pier_2_APD.Text;
            rcc_pier.PD = txt_pier_2_PD.Text;
            rcc_pier.SC = MyList.StringToDouble(txt_pier_2_SC.Text, 0.0);
            rcc_pier.HHF = MyList.StringToDouble(txt_pier_2_HHF.Text, 0.0);
            rcc_pier.V = MyList.StringToDouble(txt_pier_2_V.Text, 0.0);
            rcc_pier.K = MyList.StringToDouble(txt_pier_2_k.Text, 0.0);
            rcc_pier.CF = MyList.StringToDouble(txt_pier_2_CF.Text, 0.0);
            rcc_pier.LL = MyList.StringToDouble(txt_pier_2_LL.Text, 0.0);
            rcc_pier.Vr = MyList.StringToDouble(txt_pier_2_Vr.Text, 0.0);
            rcc_pier.Itc = MyList.StringToDouble(txt_pier_2_Itc.Text, 0.0);
            rcc_pier.sdia = MyList.StringToDouble(txt_pier_2_sdia.Text, 0.0);
            rcc_pier.sleg = MyList.StringToDouble(txt_pier_2_slegs.Text, 0.0);
            rcc_pier.ldia = MyList.StringToDouble(txt_pier_2_ldia.Text, 0.0);
            rcc_pier.SBC = MyList.StringToDouble(txt_pier_2_SBC.Text, 0.0);

            #endregion Data Input Form 2 Variables



            rcc_pier.rdia = MyList.StringToDouble(txt_RCC_Pier_rdia.Text, 0.0);
            rcc_pier.tdia = MyList.StringToDouble(txt_RCC_Pier_tdia.Text, 0.0);



            rcc_pier.hdia = MyList.StringToDouble(txt_pier_2_hdia.Text, 0.0);
            rcc_pier.hlegs = MyList.StringToDouble(txt_pier_2_hlegs.Text, 0.0);
            rcc_pier.vdia = MyList.StringToDouble(txt_pier_2_vdia.Text, 0.0);
            rcc_pier.vlegs = MyList.StringToDouble(txt_pier_2_vlegs.Text, 0.0);
            rcc_pier.vspc = MyList.StringToDouble(txt_pier_2_vspc.Text, 0.0);

        }

        void Text_Changed()
        {
            //double L = MyList.StringToDouble(txt_Ana_L1.Text, 0.0);



            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                //txt_LRFD_LL_load_gen.Text = (MyList.StringToDouble(txt_Ana_L1.Text, 0.0) / MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.2)).ToString("f0");

                //if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                //{
                txt_LRFD_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.2)).ToString("f0");
                //}
            }
            else if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                //textBox1.Text = (MyList.StringToDouble(txt_Ana_L1.Text, 0.0) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
                txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
            }


            //double B = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            txt_LRFD_LL_load_gen.Text = (L + Get_Max_Vehicle_Length()  / MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.0)).ToString("f0");
            txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");


            txt_tab1_Lo.Text = L.ToString("f3");
            txt_tab1_DW.Text = B.ToString("f3");
            txt_RCC_Pier_L.Text = L.ToString("f3");


            txt_RCC_Pier__B.Text = B.ToString("f3");
            txt_RCC_Pier___B.Text = B.ToString("f3");

            //txt_RCC_Pier_CW.Text = CW.ToString();
            //txt_RCC_Pier_DS.Text = (Ds).ToString();
            //txt_RCC_Pier_gama_c.Text = Y_c.ToString();

            //txt_RCC_Pier_NMG.Text = NMG.ToString();
            //txt_RCC_Pier_NP.Text = NMG.ToString();
            //txt_RCC_Pier_DMG.Text = (DMG).ToString();

            //txt_RCC_Pier_Hp.Text = Hp.ToString();

            //txt_RCC_Pier_Wp.Text = Wp.ToString();




            uC_RCC_Abut1.Length = L;
            uC_RCC_Abut1.Width = B;
            uC_RCC_Abut1.Overhang = MyList.StringToDouble(txt_support_distance.Text, 0.0);
        }

        #endregion Design of RCC Pier



        private void btn_Open_Worksheet_Design_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                //string excel_file = "PSC Box Girder\\" + b.Text + ".xls";
                //string excel_file = "PSC Box Girder\\New Design of PSC BOX Girder.xls";
                string excel_file = "PSC Box Girder\\" + b.Tag.ToString() + ".xls";
                excel_file = Path.Combine(excel_path, excel_file);
                //excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    //iApp.OpenExcelFile(excel_file, "2011ap");
                    //iApp.OpenExcelFile(
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder";
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), draw_cmd);

            Button b = sender as Button;

            string draw = Drawing_Folder;



            //eOpenDrawingOption opt = iApp.Open_Drawing_Option();
            //if (opt == eOpenDrawingOption.Cancel) return;

            eOpenDrawingOption opt = eOpenDrawingOption.Sample_Drawings;

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
                if (b.Name == btn_construction_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Editable Construction Drawings"), "EXTRADOSSED_BRIDGE");
                }
                if (b.Name == btn_open_drawings.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "PSC Box Girder Drawings"), "PSC_Box_Girder");
                }
                if (b.Name == btn_cable_stayed_drawing.Name)
                {
                    iApp.RunViewer(Drawing_Folder, "Cable_Stayed_Bridge");
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

        private void cmb_Ana_DL_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                txt_Ana_L1.Text = "0";
                txt_Ana_L1.Text = "100.0";
                txt_Ana_B.Text = "15.6";
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
                Deck.FilePath = user_path;
                rcc_pier.FilePath = user_path;
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


        double Get_Max_Vehicle_Length()
        {
            double mvl = 13.4;

            List<double> lst_mvl = new List<double>();
            DataGridView dgv = dgv_long_liveloads;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                dgv = dgv_long_british_loads;
            }
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value.ToString().StartsWith("AXLE SPACING"))
                    {
                        mvl = 0;
                        for (int c = 1; c < dgv.ColumnCount; c++)
                        {
                            try
                            {
                                mvl += MyList.StringToDouble(dgv[c, i].Value.ToString(), 0.0);
                            }
                            catch (Exception exx)
                            {

                            }
                        }
                        lst_mvl.Add(mvl);
                    }
                }
                catch (Exception ex1) { }

            }
            if (lst_mvl.Count > 0)
            {
                lst_mvl.Sort();
                lst_mvl.Reverse();
                mvl = lst_mvl[0];
            }
            return mvl;



            double veh_len, veh_gap, train_length;

            veh_len = mvl;
            veh_gap = mvl;
            //train_length = veh_len;
            train_length = 0.0;
            double eff = L;
            bool fl = false;
            while (train_length <= eff)
            {
                fl = !fl;
                if (fl)
                {
                    train_length += veh_gap;
                    //if (train_length > L)
                    //{
                    //    train_length = train_length - veh_gap;
                    //}
                }
                else
                {
                    train_length += veh_len;
                }
            }



            //return mvl;

            return train_length;


        }

        private void txt_Ana_DL_length_TextChanged(object sender, EventArgs e)
        {
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                //txt_LRFD_LL_load_gen.Text = (MyList.StringToDouble(txt_Ana_L1.Text, 0.0) / MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.2)).ToString("f0");

                //if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                //{
                txt_LRFD_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.2)).ToString("f0");
                //}
            }
            else if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                //textBox1.Text = (MyList.StringToDouble(txt_Ana_L1.Text, 0.0) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
                txt_IRC_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_IRC_XINCR.Text, 0.2)).ToString("f0");
            }

            uC_BoxGirder1.Span = L1;
            uC_BoxGirder1.Width = B;

            uC_CableStayedDesign1.Cable_D = MyList.StringToDouble(txt_cable_dia.Text, 0.5);
            uC_CableStayedDesign1.Cable_E = MyList.StringToDouble(txt_cbl_des_E.Text, 0.5);
            uC_CableStayedDesign1.Cable_f = MyList.StringToDouble(txt_cbl_des_f.Text, 0.5);


            //txt_Ana_X.Text = "-" + txt_Ana_L.Text;


            txt_deck_width.Text = txt_Ana_B.Text;
            try
            {

                for (int i = 1; i < dgv_seg_tab3_1.ColumnCount; i++)
                {
                    //dgv_seg_tab3_1[i, 0].Value = txt_Ana_DL_eff_depth.Text;
                    dgv_seg_tab3_1[i, 1].Value = txt_Ana_B.Text;
                    dgv_seg_tab3_1[i, 3].Value = txt_Ana_width_cantilever.Text;
                }
            }
            catch (Exception ex) { }



            Text_Changed();



            uC_BoxGirder1.txt_BM_DL_Supp.Text = txt_Ana_dead_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Deff.Text = txt_Ana_dead_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L8.Text = txt_Ana_dead_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_L4.Text = txt_Ana_dead_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_DL_3L8.Text = txt_Ana_dead_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_DL_Mid.Text = txt_Ana_dead_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_DL_Supp.Text = txt_Ana_dead_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Deff.Text = txt_Ana_dead_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L8.Text = txt_Ana_dead_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_L4.Text = txt_Ana_dead_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_DL_3L8.Text = txt_Ana_dead_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_DL_Mid.Text = txt_Ana_dead_inner_long_L2_shear.Text;


            uC_BoxGirder1.txt_BM_LL_Supp.Text = txt_Ana_live_inner_long_support_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Deff.Text = txt_Ana_live_inner_long_deff_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L8.Text = txt_Ana_live_inner_long_L8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_L4.Text = txt_Ana_live_inner_long_L4_moment.Text;
            uC_BoxGirder1.txt_BM_LL_3L8.Text = txt_Ana_live_inner_long_3L_8_moment.Text;
            uC_BoxGirder1.txt_BM_LL_Mid.Text = txt_Ana_live_inner_long_L2_moment.Text;



            uC_BoxGirder1.txt_SF_LL_Supp.Text = txt_Ana_live_inner_long_support_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Deff.Text = txt_Ana_live_inner_long_deff_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L8.Text = txt_Ana_live_inner_long_L8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_L4.Text = txt_Ana_live_inner_long_L4_shear.Text;
            uC_BoxGirder1.txt_SF_LL_3L8.Text = txt_Ana_live_inner_long_3L_8_shear.Text;
            uC_BoxGirder1.txt_SF_LL_Mid.Text = txt_Ana_live_inner_long_L2_shear.Text;





        }

        public void Load_Tab2_Tab3_Box_Segment_Data()
        {

            List<string> list = new List<string>();

            if (false)
            {
                #region Data
                list.Add(string.Format("D 4.4 4.4 4.4 4.4 4.4 4.4"));
                list.Add(string.Format("Dw 13.0 13.0 13.0 13.0 13.0 13.0"));
                list.Add(string.Format("Td 0.225 0.225 0.225 0.225 0.225 0.225"));
                list.Add(string.Format("C1 2.5 2.5 2.5 2.5 2.5 2.5"));
                list.Add(string.Format("C2 0 0 0 0 0 0"));
                list.Add(string.Format("Tip 0.2 0.2 0.2 0.2 0.2 0.2"));
                list.Add(string.Format("Tf 0.3 0.3 0.3 0.3 0.3 0.3"));
                list.Add(string.Format("Iw 0.7 0.7 0.7 0.7 0.7 0.7"));
                list.Add(string.Format("D1 2.2 2.2 2.2 2.2 2.2 2.2"));
                list.Add(string.Format("Tw 0.6 0.579 0.48 0.31 0.31 0.31"));
                list.Add(string.Format("SW 7.5 7.5 7.5 7.5 7.5 7.5"));
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
                #endregion Data
            }
            else if (true)
            {
                list.Add(string.Format("D$4.5$4.0$3.5$3.0$2.5$2.0"));
                list.Add(string.Format("Dw$15.6$15.6$15.6$15.6$15.6$15.6"));
                list.Add(string.Format("Td$0.275$0.275$0.275$0.275$0.275$0.275"));
                list.Add(string.Format("C1$1.925$1.925$1.925$1.925$1.925$1.925"));
                list.Add(string.Format("C2$0$0$0$0$0$0"));
                list.Add(string.Format("Tip$0.2$0.2$0.2$0.2$0.2$0.2"));
                list.Add(string.Format("Tf$0.3$0.3$0.3$0.3$0.3$0.3"));
                list.Add(string.Format("Iw$0.7$0.7$0.7$0.7$0.7$0.7"));
                list.Add(string.Format("D1$3.45$3.45$3.45$3.45$3.45$3.45"));
                list.Add(string.Format("Tw$0.35$0.35$0.35$0.35$0.35$0.35"));
                list.Add(string.Format("SW$6.5$6.5$6.5$6.5$6.5$6.5"));
                list.Add(string.Format("Ts$0.275$0.275$0.275$0.275$0.275$0.275"));
                list.Add(string.Format("D2$0.250$0.250$0.250$0.250$0.250$0.250"));
                list.Add(string.Format("K1$0.051$0.051$0.051$0.051$0.051$0.051"));
                list.Add(string.Format("K2$0.056$0.056$0.056$0.056$0.056$0.056"));
                list.Add(string.Format("HW1$0.409$0.485$0.582$0.75$0.75$0.75"));
                list.Add(string.Format("HH1$0.083$0.097$0.116$0.15$0.15$0.15"));
                list.Add(string.Format("CH1$1.85$1.85$1.85$1.85$1.85$1.85"));
                list.Add(string.Format("HW2$0$0$0$0$0$0"));
                list.Add(string.Format("HH2$0$0$0$0$0$0"));
                list.Add(string.Format("HW3$0.75$0.75$0.75$0.75$0.75$0.75"));
                list.Add(string.Format("HH3$0.25$0.25$0.25$0.25$0.25$0.15"));
            }


            if (false)
            {
                #region Original Data
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
                #endregion Original Data
            }


            MyList mlist = null;


            for (int i = 0; i < list.Count; i++)
            {
                //mlist = new MyList(list[i], ' ');
                if (list[i].Contains("$"))
                    mlist = new MyList(list[i], '$');
                else
                    mlist = new MyList(list[i], ' ');
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

        public void Segment_Girder_Initialize_Data()
        {
            #region Variable Declaration
            //Chiranjit [2012 10 26]
            Segment_Girder.Area_Zone1_Outer = MyList.StringToDouble(txt_zn1_out.Text, 1480.0);
            Segment_Girder.Area_Zone2_Outer = MyList.StringToDouble(txt_zn2_out.Text, 1480.0);
            Segment_Girder.Area_Zone3_Outer = MyList.StringToDouble(txt_zn3_out.Text, 1480.0);
            Segment_Girder.Area_Zone1_Inner = MyList.StringToDouble(txt_zn1_inn.Text, 1480.0);
            Segment_Girder.Area_Zone2_Inner = MyList.StringToDouble(txt_zn2_inn.Text, 1480.0);
            Segment_Girder.Area_Zone3_Inner = MyList.StringToDouble(txt_zn3_inn.Text, 1480.0);


            //Chiranjit [2012 10 17]
            Segment_Girder.rss_56 = MyList.StringToDouble(txt_tab2_rss_56.Text, 0.00019);
            Segment_Girder.rss_14 = MyList.StringToDouble(txt_tab2_rss_14.Text, 0.00025);
            Segment_Girder.Resh56 = MyList.StringToDouble(txt_tab2_Resh56.Text, 0.00025);
            Segment_Girder.Crst56 = MyList.StringToDouble(txt_tab2_Crst56.Text, 0.00025);


            //Chiranjit [2012 10 18]
            Segment_Girder.fc_temp14 = MyList.StringToDouble(txt_fc_temp14.Text, 0.00025);
            Segment_Girder.ft_temp14 = MyList.StringToDouble(txt_ft_temp14.Text, 0.00025);
            Segment_Girder.fc_temp28 = MyList.StringToDouble(txt_fc_temp28.Text, 0.00025);
            Segment_Girder.ft_temp28 = MyList.StringToDouble(txt_ft_temp28.Text, 0.00025);
            Segment_Girder.ttv = MyList.StringToDouble(txt_ttv.Text, 0.00025);
            Segment_Girder.fc_serv = MyList.StringToDouble(txt_fc_serv.Text, 0.00025);
            Segment_Girder.Modrup = MyList.StringToDouble(txt_Mod_rup.Text, 0.00025);
            Segment_Girder.fc_fact = MyList.StringToDouble(txt_fc_factor.Text, 0.00025);
            Segment_Girder.tv = MyList.StringToDouble(txt_tv.Text, 0.00025);
            Segment_Girder.ttu = MyList.StringToDouble(txt_ttu.Text, 0.00025);





            Segment_Girder.Lo = MyList.StringToDouble(txt_tab1_Lo.Text, 48.750);

            Segment_Girder.L1 = MyList.StringToDouble(txt_tab1_L1.Text, 0.500);
            Segment_Girder.L2 = MyList.StringToDouble(txt_tab1_L2.Text, 0.500);
            Segment_Girder.exg = MyList.StringToDouble(txt_tab1_exg.Text, 0.040);

            Segment_Girder.Dw = MyList.StringToDouble(txt_tab1_DW.Text, 9.750);
            Segment_Girder.D = MyList.StringToDouble(txt_tab1_D.Text, 2.500);
            Segment_Girder.Fcu = MyList.StringToDouble(cmb_tab1_Fcu.Text, 40);
            Segment_Girder.Tab1_Fy = MyList.StringToDouble(cmb_tab1_Fy.Text, 415);
            Segment_Girder.act = MyList.StringToDouble(txt_tab1_act.Text, 14);
            Segment_Girder.mct = MyList.StringToDouble(txt_tab1_Mct.Text, 87);
            Segment_Girder.sct = MyList.StringToDouble(txt_tab1_sctt.Text, 34.8);
            Segment_Girder.acsidl = MyList.StringToDouble(txt_tab1_agt_SIDL.Text, 56);
            Segment_Girder.mtcsidl = MyList.StringToDouble(txt_tab1_Mct_SIDL.Text, 100);
            Segment_Girder.T_loss = MyList.StringToDouble(txt_tab1_T_loss.Text, 20);
            Segment_Girder.wct = MyList.StringToDouble(txt_tab1_wct.Text, 0.065);
            Segment_Girder.ds = MyList.StringToDouble(txt_tab1_ds.Text, 0.225);
            Segment_Girder.df = MyList.StringToDouble(txt_tab1_df.Text, 0.175);
            Segment_Girder.bt = MyList.StringToDouble(txt_tab1_bt.Text, 1.000);

            Segment_Girder.FactDL = MyList.StringToDouble(txt_tab1_FactDL.Text, 1.250);
            Segment_Girder.FactSIDL = MyList.StringToDouble(txt_tab1_FactSIDL.Text, 2.000);
            Segment_Girder.FactLL = MyList.StringToDouble(txt_tab1_FactLL.Text, 2.500);

            Segment_Girder.alpha = MyList.StringToDouble(txt_tab1_alpha.Text, 0.0000117);


            Segment_Girder.Tr1 = MyList.StringToDouble(txt_tab1_Tr1.Text, 17.8);
            Segment_Girder.Tr2 = MyList.StringToDouble(txt_tab1_Tr2.Text, 4.0);
            Segment_Girder.Tr3 = MyList.StringToDouble(txt_tab1_Tr3.Text, 2.1);


            Segment_Girder.Tf1 = MyList.StringToDouble(txt_tab1_Tf1.Text, 10.6);
            Segment_Girder.Tf2 = MyList.StringToDouble(txt_tab1_Tf2.Text, 0.7);
            Segment_Girder.Tf3 = MyList.StringToDouble(txt_tab1_Tf3.Text, 0.8);
            Segment_Girder.Tf4 = MyList.StringToDouble(txt_tab1_Tf4.Text, 6.6);


            //Prestressing Input Data:   [Tab 2]

            //A)                 Construction Schedule and Prestressing Stages

            //    Job                                                                                                 Day after casting                 fcj   N/sq.mm (Mpa)
            //(i)                Completion of casting of Box Girder                                0                day
            //(ii)                First Stage Prestress                                                                14                day                                fcj14 = 34.80
            //(iii)                Completion of Wearing Course & Crash Barrier                56                day                                fcj56 = 40.00


            Segment_Girder.ccbg_day = MyList.StringToDouble(txt_tab2_ccbg_day.Text, 10.6);
            Segment_Girder.ccbg_fcj = MyList.StringToDouble(txt_tab2_ccbg_fcj.Text, 0.7);

            Segment_Girder.fsp_day = MyList.StringToDouble(txt_tab2_fsp_day.Text, 10.6);
            Segment_Girder.fsp_fcj = MyList.StringToDouble(txt_tab2_fsp_fcj.Text, 0.7);

            Segment_Girder.cwccb_day = MyList.StringToDouble(txt_tab2_cwccb_day.Text, 10.6);
            Segment_Girder.cwccb_fcj = MyList.StringToDouble(txt_tab2_cwccb_fcj.Text, 0.7);



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


            Segment_Girder.ND = MyList.StringToDouble(txt_tab2_D.Text, 15.200);
            Segment_Girder.NA = MyList.StringToDouble(txt_tab2_A.Text, 140.000);
            Segment_Girder.Pu = MyList.StringToDouble(txt_tab2_Pu.Text, 1.100);
            Segment_Girder.Tab2_Fy = MyList.StringToDouble(txt_tab2_Fy.Text, 1670.000);
            Segment_Girder.Fu = MyList.StringToDouble(txt_tab2_Fu.Text, 1860.000);
            Segment_Girder.Pn = MyList.StringToDouble(txt_tab2_Pn.Text, 260.700);
            Segment_Girder.Eps = MyList.StringToDouble(txt_tab2_Eps.Text, 195);
            Segment_Girder.Pj = MyList.StringToDouble(txt_tab2_Pj.Text, 76.5);
            Segment_Girder.s = MyList.StringToDouble(txt_tab2_s.Text, 6);
            Segment_Girder.mu = MyList.StringToDouble(txt_tab2_mu.Text, 0.17);
            Segment_Girder.k = MyList.StringToDouble(txt_tab2_k.Text, 0.002);
            Segment_Girder.Re1 = MyList.StringToDouble(txt_tab2_Re1.Text, 35.0);
            Segment_Girder.Re2 = MyList.StringToDouble(txt_tab2_Re2.Text, 0.0);
            Segment_Girder.td1 = MyList.StringToDouble(txt_tab2_td1.Text, 14);
            Segment_Girder.qd = MyList.StringToDouble(txt_tab2_qd.Text, 110);
            Segment_Girder.cover1 = MyList.StringToDouble(txt_tab2_cover1.Text, 110);
            Segment_Girder.cover2 = MyList.StringToDouble(txt_tab2_cover2.Text, 110);
            Segment_Girder.Ns = MyList.StringToDouble(txt_tab2_Ns.Text, 19);
            Segment_Girder.Nc_Left = MyList.StringToInt(txt_tab2_nc_left.Text, 7);
            Segment_Girder.Nc_Right = MyList.StringToInt(txt_tab2_nc_right.Text, 7);
            Segment_Girder.Cable_Area = MyList.StringToInt(txt_tab2_cable_area.Text, 7);


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


            //Chiranjit [2013 06 19]
            Segment_Girder.Cable_Type = cmb_tab2_strand_data.Text;


            //Chiranjit [2013 06 19]
            Segment_Girder.L_Deff = MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0);



            #endregion Variable Declaration
        }

        private void btn_segment_process_Click(object sender, EventArgs e)
        {
            Write_All_Data();

            Segment_Girder.FilePath = user_path;
            Segment_Girder_Initialize_Data();
            Set_Box_Forces();

            Box_Forces.Set_Absolute();
            Segment_Girder.Calculate_Program(Segment_Girder.rep_file_name, Box_Forces);
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(Segment_Girder.rep_file_name))
            {
                MessageBox.Show(this, "Report file written in " + Segment_Girder.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm_BoxGirder_Msg.Msg_Showw();
                iApp.View_Result(Segment_Girder.rep_file_name, true);
            }
            Segment_Girder.is_process = true;
            Button_Enable_Disable();
        }

        private void txt_tab2_Fcu_TextChanged(object sender, EventArgs e)
        {
            txt_tab2_Ec.Text = (5000.0 * Math.Sqrt(MyList.StringToDouble(txt_tab2_Fcu.Text, 0.0))).ToString("f2");
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



                    txt_tab1_ds.Text = dgv_seg_tab3_1[i, 2].Value.ToString();

                }



                for (int i = 0; i < dgv_seg_tab3_1.RowCount; i++)
                {
                    for (int c = 0; c < uC_BoxGirder1.dgv_seg_tab3.ColumnCount; c++)
                    {
                        uC_BoxGirder1.dgv_seg_tab3[c, i].Value = dgv_seg_tab3_1[c, i].Value;
                    }
                }
                uC_BoxGirder1.Update_Tab3_Data();

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


        private void btn_segment_report_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Segment_Girder.rep_file_name))
                    iApp.View_Result(Segment_Girder.rep_file_name);
            }
            catch (Exception ex) { }
        }

        private void txt_Ana_DL_factor_TextChanged(object sender, EventArgs e)
        {
            lbl_factor.Text = "DL X " + DL_Factor + " + LL X " + LL_Factor;
            Text_Changed_Forces();

        }

        private void txt_tab1_Lo_TextChanged(object sender, EventArgs e)
        {
            txt_tab1_L.Text = (MyList.StringToDouble(txt_tab1_Lo.Text, 0.0) - 2 * MyList.StringToDouble(txt_tab1_L1.Text, 0.0)).ToString("f3");
        }

        private void cmb_tab1_Fcu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Concrete_Grade();
        }

        private void Set_Concrete_Grade()
        {

            double mct_14 = MyList.StringToDouble(txt_tab1_Mct.Text, 0.0);
            double mct_SIDL = MyList.StringToDouble(txt_tab1_Mct_SIDL.Text, 0.0);

            double fcu = MyList.StringToDouble(cmb_tab1_Fcu.Text, 0.0);

            //txt_tab1_Mct.Text = "";
            txt_tab1_sctt.Text = (mct_14 / 100.0 * fcu).ToString("f3");
            txt_tab2_fsp_fcj.Text = txt_tab1_sctt.Text;
            txt_tab2_cwccb_fcj.Text = (mct_SIDL / 100.0 * fcu).ToString("f3");



            txt_tab2_Fcu.Text = cmb_tab1_Fcu.Text;


            //txt_tab1_act.Text = cmb_tab1_Fcu.Text;

            txt_tab2_fsp_day.Text = txt_tab1_act.Text;


            //txt_tab1_agt_SIDL.Text = cmb_tab1_Fcu.Text;


            txt_tab2_cwccb_day.Text = txt_tab1_agt_SIDL.Text;




        }

        private void cmb_tab2_strand_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            double D = 0.0, A = 0.0, Pu = 0.0, Fy = 0.0, Fu = 0.0, Pn = 0.0;

            if (cmb_tab2_strand_data.SelectedIndex == 0)
            {
                D = 12.9;
                A = 100.0;
                Pu = 0.785;
                Fy = 1580.0;
                Fu = 1860.0;
                Pn = 186.0;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 1)
            {
                D = 12.7;
                A = 98.7;
                Pu = 0.775;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 183.7;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 2)
            {
                D = 15.7;
                A = 150;
                Pu = 1.18;
                Fy = 1500;
                Fu = 1770;
                Pn = 265.0;
            }
            else if (cmb_tab2_strand_data.SelectedIndex == 3)
            {
                D = 15.2;
                A = 140;
                Pu = 1.10;
                Fy = 1670.0;
                Fu = 1860.0;
                Pn = 260.7;
            }

            txt_tab2_D.Text = D.ToString();
            txt_tab2_A.Text = A.ToString();
            txt_tab2_Pu.Text = Pu.ToString();
            txt_tab2_Fy.Text = Fy.ToString();
            txt_tab2_Fu.Text = Fu.ToString();
            txt_tab2_Pn.Text = Pn.ToString();
        }

        private void cmb_tab2_nc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tab2_nc_left.Text = (cmb_tab2_nc.SelectedIndex + 2).ToString();
            txt_tab2_nc_right.Text = txt_tab2_nc_left.Text;

        }

        private void txt_pier_2_APD_TextChanged(object sender, EventArgs e)
        {

            txt_pier_2_APD.TextAlign = HorizontalAlignment.Left;
            txt_pier_2_APD.WordWrap = true;

            double b16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);

            string kStr = txt_pier_2_APD.Text.Replace(",", " ").Trim().TrimEnd().TrimStart();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            kStr = "";
            try
            {
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.GetDouble(i) < b16)
                    {
                        kStr += mlist.StringList[i] + ",";
                    }
                }
                kStr = kStr.Substring(0, kStr.Length - 1);
            }
            catch (Exception ex) { }

            txt_pier_2_PD.Text = kStr;
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
        private void txt_deck_width_TextChanged(object sender, EventArgs e)
        {
            British_Interactive();

        }


        private void rbtn_HA_HB_CheckedChanged(object sender, EventArgs e)
        {
            British_Interactive();
            spc_HB.Visible = !rbtn_HA.Checked;

            if (rbtn_HA_HB.Checked || rbtn_HB.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));


                if (IsRead) return;

                Default_British_HB_LoadData(dgv_long_british_loads);
                Default_British_HB_Type_LoadData(dgv_british_loads);

                lbl_HB.Text = "HB LOADINGS";

                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_HA.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));
                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_Rail_Load.Checked)
            {
                lbl_HB.Text = "BS RAIL LOADINGS";

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("TOTAL DL + LL ANALYSIS"));
                //cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("GIRDER ANALYSIS RESULTS"));


                if (IsRead) return;

                Default_British_Rail_LoadData(dgv_long_british_loads);
                Default_British_Rail_Type_LoadData(dgv_british_loads);

                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
        }
        public bool IsRead = false;
        public void British_Interactive()
        {
            if (IsRead) return;

            double d, lane_width, impf, lf;

            double incr, lgen;

            //txt_ll_british_incr
            int nos_lane, i;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);
            incr = MyList.StringToDouble(txt_ll_british_incr.Text, 0.0);

            if (incr == 0) incr = 1;
            lgen = ((int)((L + Get_Max_Vehicle_Length()) / incr)) + 1;

            nos_lane = (int)(d / lane_width);

            txt_no_lanes.Text = nos_lane.ToString();
            txt_no_lanes.Enabled = false;

            txt_ll_british_lgen.Text = lgen.ToString();
            txt_ll_british_lgen.Enabled = false;


            chk_HA_1L.Enabled = (nos_lane >= 1);
            chk_HA_2L.Enabled = (nos_lane >= 2);
            chk_HA_3L.Enabled = (nos_lane >= 3);
            chk_HA_4L.Enabled = (nos_lane >= 4);
            chk_HA_5L.Enabled = (nos_lane >= 5);
            chk_HA_6L.Enabled = (nos_lane >= 6);
            chk_HA_7L.Enabled = (nos_lane >= 7);
            chk_HA_8L.Enabled = (nos_lane >= 8);
            chk_HA_9L.Enabled = (nos_lane >= 9);
            chk_HA_10L.Enabled = (nos_lane >= 10);


            chk_HB_1L.Enabled = (nos_lane >= 1);
            chk_HB_2L.Enabled = (nos_lane >= 2);
            chk_HB_3L.Enabled = (nos_lane >= 3);
            chk_HB_4L.Enabled = (nos_lane >= 4);
            chk_HB_5L.Enabled = (nos_lane >= 5);
            chk_HB_6L.Enabled = (nos_lane >= 6);
            chk_HB_7L.Enabled = (nos_lane >= 7);
            chk_HB_8L.Enabled = (nos_lane >= 8);
            chk_HB_9L.Enabled = (nos_lane >= 9);
            chk_HB_10L.Enabled = (nos_lane >= 10);


            grb_ha.Enabled = (rbtn_HA.Checked || rbtn_HA_HB.Checked || chk_HA.Checked);
            grb_hb.Enabled = (rbtn_HB.Checked || rbtn_HA_HB.Checked);

            if (rbtn_HA.Checked)
            {
                chk_HA_1L.Checked = chk_HA_1L.Enabled;
                chk_HA_2L.Checked = chk_HA_2L.Enabled;
                chk_HA_3L.Checked = chk_HA_3L.Enabled;
                chk_HA_4L.Checked = chk_HA_4L.Enabled;
                chk_HA_5L.Checked = chk_HA_5L.Enabled;
                chk_HA_6L.Checked = chk_HA_6L.Enabled;
                chk_HA_7L.Checked = chk_HA_7L.Enabled;
                chk_HA_8L.Checked = chk_HA_8L.Enabled;
                chk_HA_9L.Checked = chk_HA_9L.Enabled;
                chk_HA_10L.Checked = chk_HA_10L.Enabled;
            }

            if (rbtn_HB.Checked || rbtn_Rail_Load.Checked)
            {
                chk_HB_1L.Checked = chk_HB_1L.Enabled;
                chk_HB_2L.Checked = chk_HB_2L.Enabled;
                chk_HB_3L.Checked = chk_HB_3L.Enabled;
                chk_HB_4L.Checked = chk_HB_4L.Enabled;
                chk_HB_5L.Checked = chk_HB_5L.Enabled;
                chk_HB_6L.Checked = chk_HB_6L.Enabled;
                chk_HB_7L.Checked = chk_HB_7L.Enabled;
                chk_HB_8L.Checked = chk_HB_8L.Enabled;
                chk_HB_9L.Checked = chk_HB_9L.Enabled;
                chk_HB_10L.Checked = chk_HB_10L.Enabled;
            }

            //if(rbtn_HA_HB.Checked)
            //{

            //    chk_HB_1L.Checked = !chk_HA_1L.Checked;
            //    chk_HB_2L.Checked = !chk_HA_2L.Checked;
            //    chk_HB_3L.Checked = !chk_HA_3L.Checked;
            //    chk_HB_4L.Checked = !chk_HA_4L.Checked;
            //    chk_HB_5L.Checked = !chk_HA_5L.Checked;
            //    chk_HB_6L.Checked = !chk_HA_6L.Checked;
            //    chk_HB_7L.Checked = !chk_HA_7L.Checked;
            //    chk_HB_8L.Checked = !chk_HA_8L.Checked;
            //    chk_HB_9L.Checked = !chk_HA_9L.Checked;
            //    chk_HB_10L.Checked = !chk_HA_10L.Checked;
            //}


        }

        public void Default_British_HB_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();
            int typ_no = 1;

            double ll = MyList.StringToDouble(load.Replace("HB_", ""), 1.0);


            for (i = 6; i <= 26; i += 5)
            {
                list.Add(string.Format("TYPE {0}, {1}_{2}", typ_no++, load, i));
                list.Add(string.Format("AXLE LOAD IN TONS ,{0:f1}, {0:f1}, {0:f1}, {0:f1}", ll));
                list.Add(string.Format("AXLE SPACING IN METRES, 1.8,{0:f1},1.8", i));
                list.Add(string.Format("AXLE WIDTH IN METRES, 1.0"));
                list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
                list.Add(string.Format(""));
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

                //for (int j = 0; j < mlist.Count; j++)
                //{
                //    dgv_live_load[j, i].Value = mlist[j];
                //}

                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_HB_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            if (chk_HB_1L.Checked) lanes.Add(1);
            if (chk_HB_2L.Checked) lanes.Add(2);
            if (chk_HB_3L.Checked) lanes.Add(3);
            if (chk_HB_4L.Checked) lanes.Add(4);
            if (chk_HB_5L.Checked) lanes.Add(5);
            if (chk_HB_6L.Checked) lanes.Add(6);
            if (chk_HB_7L.Checked) lanes.Add(7);
            if (chk_HB_8L.Checked) lanes.Add(8);
            if (chk_HB_9L.Checked) lanes.Add(9);
            if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);



            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 5; ld++)
            {
                load = "LOAD " + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25);

                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            //list.Add(string.Format("LOAD 1,TYPE 1"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 2,TYPE 2"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 3,TYPE 3"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,5.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 4,TYPE 4"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 5,TYPE 5"));
            //list.Add(string.Format("X,0,0"));
            //list.Add(string.Format("Z,1.5,4.5"));
            //list.Add(string.Format(""));
            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count * 2; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }


        public void Default_British_Rail_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();

            list.Add(string.Format("TYPE 1, BS_RAIL_1"));
            list.Add(string.Format("AXLE LOAD IN TONS , 24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52,24.52"));
            list.Add(string.Format("AXLE SPACING IN METRES,2.05,1.95,5.56,1.95,2.05,5.94,2.05,1.95,5.56,1.95,2.05", i));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.676"));
            list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
            list.Add(string.Format(""));


            list.Add(string.Format("TYPE 2, BS_RAIL_2"));
            list.Add(string.Format("AXLE LOAD IN TONS , 22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06,22.06"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.65,1.65,6.4,1.65,1.65,3.0,1.65,1.65,6.4,1.65,1.65"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.676"));
            list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
            list.Add(string.Format(""));

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
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_Rail_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            //if (chk_HB_1L.Checked) lanes.Add(1);
            //if (chk_HB_2L.Checked) lanes.Add(2);
            //if (chk_HB_3L.Checked) lanes.Add(3);
            //if (chk_HB_4L.Checked) lanes.Add(4);
            //if (chk_HB_5L.Checked) lanes.Add(5);
            //if (chk_HB_6L.Checked) lanes.Add(6);
            //if (chk_HB_7L.Checked) lanes.Add(7);
            //if (chk_HB_8L.Checked) lanes.Add(8);
            //if (chk_HB_9L.Checked) lanes.Add(9);
            //if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);

            for (i = 1; i <= nos_lane; i++)
            {
                lanes.Add(i);
            }

            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 2; ld++)
            {
                load = "LOAD " + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0);

                    //load += ",TYPE " + ld;
                    //x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    //z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }


        private void cmb_HB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRead) return;

            Default_British_HB_LoadData(dgv_long_british_loads);
            Default_British_HB_Type_LoadData(dgv_british_loads);
        }

        private void chk_HA_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HB_1L.Checked = (!chk_HA_1L.Checked && chk_HB_1L.Enabled);
            chk_HB_2L.Checked = !chk_HA_2L.Checked && chk_HB_2L.Enabled;
            chk_HB_3L.Checked = !chk_HA_3L.Checked && chk_HB_3L.Enabled;
            chk_HB_4L.Checked = !chk_HA_4L.Checked && chk_HB_4L.Enabled;
            chk_HB_5L.Checked = !chk_HA_5L.Checked && chk_HB_5L.Enabled;
            chk_HB_6L.Checked = !chk_HA_6L.Checked && chk_HB_6L.Enabled;
            chk_HB_7L.Checked = !chk_HA_7L.Checked && chk_HB_7L.Enabled;
            chk_HB_8L.Checked = !chk_HA_8L.Checked && chk_HB_8L.Enabled;
            chk_HB_9L.Checked = !chk_HA_9L.Checked && chk_HB_9L.Enabled;
            chk_HB_10L.Checked = !chk_HA_10L.Checked && chk_HB_10L.Enabled;
            Default_British_HB_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;

        }

        private void chk_HB_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HA_1L.Checked = !chk_HB_1L.Checked && chk_HB_1L.Enabled;
            chk_HA_2L.Checked = !chk_HB_2L.Checked && chk_HB_2L.Enabled;
            chk_HA_3L.Checked = !chk_HB_3L.Checked && chk_HB_3L.Enabled;
            chk_HA_4L.Checked = !chk_HB_4L.Checked && chk_HB_4L.Enabled;
            chk_HA_5L.Checked = !chk_HB_5L.Checked && chk_HB_5L.Enabled;
            chk_HA_6L.Checked = !chk_HB_6L.Checked && chk_HB_6L.Enabled;
            chk_HA_7L.Checked = !chk_HB_7L.Checked && chk_HB_7L.Enabled;
            chk_HA_8L.Checked = !chk_HB_8L.Checked && chk_HB_8L.Enabled;
            chk_HA_9L.Checked = !chk_HB_9L.Checked && chk_HB_9L.Enabled;
            chk_HA_10L.Checked = !chk_HB_10L.Checked && chk_HB_10L.Enabled;

            Default_British_HB_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;
        }

        private void chk_HA_3L_EnabledChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            CheckBox chk = sender as CheckBox;
            if (!chk.Enabled) chk.Checked = false;
        }

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();

        public void LONG_GIRDER_BRITISH_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();

            if (rbtn_HA.Checked) return;

            List<string> long_ll_impact = new List<string>();
            //long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format(""));


            bool flag = false;
            for (i = 0; i < dgv_long_british_loads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_british_loads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
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
            //long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> load_list_1 = new List<string>();
            List<string> load_list_2 = new List<string>();
            List<string> load_list_3 = new List<string>();
            List<string> load_list_4 = new List<string>();
            List<string> load_list_5 = new List<string>();
            List<string> load_list_6 = new List<string>();
            List<string> load_total_7 = new List<string>();



            int fl = 0;
            double xinc = MyList.StringToDouble(txt_ll_british_incr.Text, 0.5);
            //double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_british_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_british_loads[0, i].Value.ToString();

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
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_ll_british_lgen.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
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
                for (c = 1; c < dgv_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_british_loads[c, i].Value.ToString();

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

        public List<int> HA_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HA_1L.Checked) lanes.Add(1);
                if (chk_HA_2L.Checked) lanes.Add(2);
                if (chk_HA_3L.Checked) lanes.Add(3);
                if (chk_HA_4L.Checked) lanes.Add(4);
                if (chk_HA_5L.Checked) lanes.Add(5);
                if (chk_HA_6L.Checked) lanes.Add(6);
                if (chk_HA_7L.Checked) lanes.Add(7);
                if (chk_HA_8L.Checked) lanes.Add(8);
                if (chk_HA_9L.Checked) lanes.Add(9);
                if (chk_HA_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }
        public List<int> HB_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HB_1L.Checked) lanes.Add(1);
                if (chk_HB_2L.Checked) lanes.Add(2);
                if (chk_HB_3L.Checked) lanes.Add(3);
                if (chk_HB_4L.Checked) lanes.Add(4);
                if (chk_HB_5L.Checked) lanes.Add(5);
                if (chk_HB_6L.Checked) lanes.Add(6);
                if (chk_HB_7L.Checked) lanes.Add(7);
                if (chk_HB_8L.Checked) lanes.Add(8);
                if (chk_HB_9L.Checked) lanes.Add(9);
                if (chk_HB_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }



        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Set File Name

            string file_name = "";
            if (Extradosed != null)
            {

                if (cmb_cable_type.SelectedIndex == 2)
                {

                    if (cmb_long_open_file.SelectedIndex == 0)
                        file_name = Deck_Analysis_DL.Input_File;
                    else if (cmb_long_open_file.SelectedIndex == 1)
                        file_name = Deck_Analysis_LL.Input_File;
                }
                else
                {
                    //if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
                        if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count)
                    {
                        if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        {


                            if (cmb_cable_type.SelectedIndex == 2)
                            {

                                if (cmb_long_open_file.SelectedIndex == 0)
                                    file_name = Deck_Analysis_DL.Input_File;
                                else if (cmb_long_open_file.SelectedIndex == 1)
                                    file_name = Deck_Analysis_LL.Input_File;
                            }
                            else
                            {
                                //if (cmb_long_open_file.SelectedIndex == 0)
                                //    file_name = Input_File_DL;
                                //else if (cmb_long_open_file.SelectedIndex == 1)
                                //    file_name = Input_File_LL;
                                //else if (cmb_long_open_file.SelectedIndex == 2)
                                //    file_name = Extradosed.DeadLoadAnalysis_Input_File;
                                //else if (cmb_long_open_file.SelectedIndex == 3)
                                //    file_name = Extradosed.TotalAnalysis_Input_File;
                                //else if (cmb_long_open_file.SelectedIndex == 4)
                                //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                                //else
                                //    file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);

                                if (cmb_long_open_file.SelectedIndex == 0)
                                    file_name = Extradosed.DeadLoadAnalysis_Input_File;
                                else if (cmb_long_open_file.SelectedIndex == cmb_long_open_file.Items.Count - 1)
                                    file_name = Extradosed.TotalAnalysis_Input_File;
                                else
                                    file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                            }
                        }
                        else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                        {
                            if (cmb_long_open_file.SelectedIndex == 0)
                                file_name = Extradosed.DeadLoadAnalysis_Input_File;
                            //else if (cmb_long_open_file.SelectedIndex == 1)
                            //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                            //else if (cmb_long_open_file.SelectedIndex == 2)
                            //    file_name = Extradosed.TotalAnalysis_Input_File;
                            else
                                file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                        }
                        else
                        {
                            if (cmb_long_open_file.SelectedIndex == 0)
                                file_name = Extradosed.DeadLoadAnalysis_Input_File;
                            else
                                file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                        }
                    }
                    else
                    {
                        file_name = Result_Report_LL;
                    }
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_view_postProcess.Enabled = File.Exists(MyList.Get_LL_TXT_File(file_name)) && File.Exists(MyList.Get_Analysis_Report_File(file_name));

            if (cmb_cable_type.SelectedIndex == 2)
            {
                btn_view_preProcess.Enabled = File.Exists(file_name);
            }
            else
            {
                //btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file.SelectedIndex != cmb_long_open_file.Items.Count - 1;
                btn_view_preProcess.Enabled = File.Exists(file_name) ;
            }
            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

        }

        #endregion British Standard Loading

        #endregion Chiranjit [2014 09 10]

        public string Get_Live_Load_Analysis_Input_File(int analysis_no)
        {
            string working_folder = user_path;

            if (Directory.Exists(working_folder))
            {
                string pd = Path.Combine(working_folder, "LL ANALYSIS LOAD " + analysis_no);
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

                if (add_LiveLoad)
                {
                    foreach (var item in txt_Ana_LL_member_load.Lines)
                    {

                        if (item == "") continue;
                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (HA_Lanes.Count > 0)
                        {
                            //load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("*HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("MEMBER LOAD");
                            //if (chk_self_british.Checked)
                            //    load_lst.Add("SELFWEIGHT Y -1");

                            load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                            //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                            foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                            {
                                load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                            }
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            else
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    //if (chk_self_indian.Checked)
                    //    load_lst.Add("SELFWEIGHT Y -1");
                }
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    if (HA_Lanes.Count > 0)
                    {
                        load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                        load_lst.Add("MEMBER LOAD");
                        //if (chk_self_british.Checked)
                        //    load_lst.Add("SELFWEIGHT Y -1");

                        load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                        //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                        foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                        {
                            load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                        }
                    }
                }
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                if ((rbtn_HB.Checked || rbtn_HA_HB.Checked || rbtn_Rail_Load.Checked)
                    || iApp.DesignStandard == eDesignStandard.IndianStandard)
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                //load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);


                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

                //if (load_no == 1)
                //    load_lst.AddRange(load_list_1.ToArray());
                //else if (load_no == 2)
                //    load_lst.AddRange(load_list_2.ToArray());
                //else if (load_no == 3)
                //    load_lst.AddRange(load_list_3.ToArray());
                //else if (load_no == 4)
                //    load_lst.AddRange(load_list_4.ToArray());
                //else if (load_no == 5)
                //    load_lst.AddRange(load_list_5.ToArray());
                //else if (load_no == 6)
                //    load_lst.AddRange(load_list_6.ToArray());
                //else if (load_no == 7)
                //    load_lst.AddRange(load_total_7.ToArray());
            }
            if(indx != -1)  inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Ana_Write_Cable_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
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

                if (add_LiveLoad)
                {
                    foreach (var item in txt_Ana_LL_member_load.Lines)
                    {
                        if (item == "") continue;
                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (HA_Lanes.Count > 0)
                        {
                            //load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("*HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("MEMBER LOAD");
                            //if (chk_self_british.Checked)
                            //    load_lst.Add("SELFWEIGHT Y -1");

                            load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                            //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                            foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                            {
                                load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                            }
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_Ana_LL_member_load.Lines);
            }
            else
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    //if (chk_self_indian.Checked)
                    //    load_lst.Add("SELFWEIGHT Y -1");
                }
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    if (HA_Lanes.Count > 0)
                    {
                        load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                        load_lst.Add("MEMBER LOAD");
                        //if (chk_self_british.Checked)
                        //    load_lst.Add("SELFWEIGHT Y -1");

                        load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                        //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                        foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                        {
                            load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                        }
                    }
                }
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                if ((rbtn_HB.Checked || rbtn_HA_HB.Checked || rbtn_Rail_Load.Checked)
                    || iApp.DesignStandard == eDesignStandard.IndianStandard)
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                //load_lst.Add("LOAD GENERATION " + txt_LRFD_LL_load_gen.Text);


                if (all_loads.Count >= load_no && all_loads.Count != 0)
                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

                //if (load_no == 1)
                //    load_lst.AddRange(load_list_1.ToArray());
                //else if (load_no == 2)
                //    load_lst.AddRange(load_list_2.ToArray());
                //else if (load_no == 3)
                //    load_lst.AddRange(load_list_3.ToArray());
                //else if (load_no == 4)
                //    load_lst.AddRange(load_list_4.ToArray());
                //else if (load_no == 5)
                //    load_lst.AddRange(load_list_5.ToArray());
                //else if (load_no == 6)
                //    load_lst.AddRange(load_list_6.ToArray());
                //else if (load_no == 7)
                //    load_lst.AddRange(load_total_7.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Create_Data_LL_British(string file_name)
        {
            //Input_File_LL
            Deck_Analysis_LL.Input_File = file_name;
            LONG_GIRDER_BRITISH_LL_TXT();

            Bridge_Analysis.HA_Lanes = HA_Lanes;

            Deck_Analysis_LL.CreateData_British();

            Bridge_Analysis.HA_Loading_Members = "191 TO 202";

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
                Deck_Analysis_LL.WriteData_LiveLoad(ll_file, PSC_SECIONS, long_ll);
                Ana_Write_Long_Girder_Load_Data(ll_file, true, false, (i + 1));
            }

            Deck_Analysis_LL.Bridge_Analysis = new BridgeMemberAnalysis(iApp, ll_file);

            string ll_txt = Deck_Analysis_LL.LiveLoad_File;

            Deck_Analysis_DL.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Deck_Analysis_LL.Live_Load_List == null) return;

            Button_Enable_Disable();



        }
        private void btn_view_data_Click(object sender, EventArgs e)
        {


            string file_name = "";
            string ll_txt = "";

            Button btn = sender as Button;


            if (cmb_cable_type.SelectedIndex == 2)
            {

                if (cmb_long_open_file.SelectedIndex == 0)
                    file_name = Deck_Analysis_DL.Input_File;
                else if (cmb_long_open_file.SelectedIndex == 1)
                    file_name = Deck_Analysis_LL.Input_File;
            }
            else
            {
                #region Set File Name
                //if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
                    if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count)
                {
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {

                        //if (cmb_long_open_file.SelectedIndex == 0)
                        //    file_name = Input_File_DL;
                        //else if (cmb_long_open_file.SelectedIndex == 1)
                        //    file_name = Input_File_LL;
                        //else if (cmb_long_open_file.SelectedIndex == 2)
                        //    file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 3)
                        //    file_name = Extradosed.TotalAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 4)
                        //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                        //else
                        //    file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);


                        if (cmb_long_open_file.SelectedIndex == 0)
                            file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        else if (cmb_long_open_file.SelectedIndex == cmb_long_open_file.Items.Count - 1)
                            file_name = Extradosed.TotalAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 1)
                        //    file_name = Input_File_LL;
                        //else if (cmb_long_open_file.SelectedIndex == 2)
                        //    file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 3)
                        //    file_name = Extradosed.TotalAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 4)
                        //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                        else
                            file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);


                    }
                    else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    {
                        if (cmb_long_open_file.SelectedIndex == 0)
                            file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 1)
                        //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 2)
                        //    file_name = Extradosed.TotalAnalysis_Input_File;
                        else
                            file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                    }
                    //file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);
                    else
                    {


                        //if (cmb_long_open_file.SelectedIndex == 0)
                        //    file_name = Input_File_DL;
                        //else if (cmb_long_open_file.SelectedIndex == 1 && iApp.DesignStandard == eDesignStandard.IndianStandard)
                        //    file_name = Input_File_LL;
                        //else
                        //    file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                        ////file_name = Result_Report_LL;

                        if (cmb_long_open_file.SelectedIndex == 0)
                            file_name = Extradosed.DeadLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 1)
                        //    file_name = Extradosed.LiveLoadAnalysis_Input_File;
                        //else if (cmb_long_open_file.SelectedIndex == 2)
                        //    file_name = Extradosed.TotalAnalysis_Input_File;
                        else
                            file_name = Get_Live_Load_Analysis_Input_File(cmb_long_open_file.SelectedIndex);
                    }
                }
                else
                {
                    file_name = Result_Report_LL;
                }
                #endregion Set File Name
            }
            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name)
            {
                if (cmb_long_open_file.SelectedIndex == cmb_long_open_file.Items.Count - 1)
                    iApp.View_Result(file_name);
                else
                    System.Diagnostics.Process.Start(file_name);
                //if (File.Exists(ll_txt))
                //    iApp.RunExe(ll_txt);
                //if (File.Exists(file_name))
                //    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_view_preProcess.Name)
            {
                if (File.Exists(file_name))
                {
                    //iApp.Form_ASTRA_Input_Data(file_name, false).ShowDialog();

                    //iApp.OpenWork(file_name, false);
                    iApp.View_PreProcess(file_name);
                }
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_view_postProcess.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                {
                    //iApp.OpenWork(file_name, true);
                    iApp.View_PostProcess(file_name);
                }
            }
        }
        #region Chiranjit [2015 08 31]
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
        #endregion Chiranjit [2015 08 31]

        private void btn_psc_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_psc_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, user_path);

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
                    iApp.user_path = user_path;
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
        #region Chiranjit [2016 09 07
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
        eASTRADesignType Project_Type { get; set; }
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

        #endregion Chiranjit [2016 09 07]

        private void uC_RCC_Abut1_Abut_Counterfort_LS1_dead_load_CheckedChanged(object sender, EventArgs e)
        {

            if (uC_RCC_Abut1.uC_Abut_Counterfort_LS1.rbtn_dead_load.Checked)
            {
                uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_A = txt_dead_vert_reac_kN.Text;
                uC_RCC_Abut1.uC_Abut_Counterfort_LS1.Reaction_B = txt_dead_vert_reac_kN.Text;
            }
        }


        #region Chiranjit [2017 02 01]
        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            list.Add(string.Format("TYPE 1, IRCCLASSA"));
            list.Add(string.Format("AXLE LOAD IN TONS , 2.7,2.7,11.4,11.4,6.8,6.8,6.8,6.8"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.10,3.20,1.20,4.30,3.00,3.00,3.00"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
            list.Add(string.Format("IMPACT FACTOR, 1.179"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, IRC70RTRACK"));
            list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
            list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
            list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));


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
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {
                #region Long Girder
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 3"));
                list.Add(string.Format("X,-13.4"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1"));
                list.Add(string.Format("X,-18.8"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5,TYPE 1,TYPE 3"));
                list.Add(string.Format("X,-18.8,-13.4"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6,TYPE 3,TYPE 1"));
                list.Add(string.Format("X,-13.4,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                //list.Add(string.Format("TOTAL LOAD,TYPE 1,TYPE 1,TYPE 1"));
                //list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                //list.Add(string.Format("Z,1.5,4.5,7.5"));
                //list.Add(string.Format(""));
                #endregion
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


        //private void btn_edit_load_combs_Click(object sender, EventArgs e)
        //{
        //    LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
        //    ff.Owner = this;
        //    ff.ShowDialog();
        //}


        private void btn_restore_ll_data_Click(object sender, EventArgs e)
        {

        }




        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelBeams;

                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelBeams;

                }
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelChannels;
                        break;
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelChannels;
                        break;
                }
                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelAngles;
                        break;
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelAngles;
                        break;
                }
                return iApp.Tables.IS_SteelAngles;
            }
        }

        public double Cable_D
        {
            get { return MyList.StringToDouble(uC_CableStayedDesign1.txt_cbl_des_d.Text, 0.0); }
            set { uC_CableStayedDesign1.txt_cbl_des_d.Text = value.ToString("E3"); }
        }
        public double Cable_Ax
        {
            get { return (Math.PI * Cable_D * Cable_D) / 4.0; }
        }
        public double Cable_Gamma
        {
            get { return MyList.StringToDouble(uC_CableStayedDesign1.txt_cbl_des_gamma.Text, 0.0); }
            set { uC_CableStayedDesign1.txt_cbl_des_gamma.Text = value.ToString("E3"); }
        }
        public double Cable_E
        {
            get { return MyList.StringToDouble(uC_CableStayedDesign1.txt_cbl_des_E.Text, 0.0); }
            set { uC_CableStayedDesign1.txt_cbl_des_E.Text = value.ToString("E3"); }
        }
        public double Cable_f
        {
            get { return MyList.StringToDouble(uC_CableStayedDesign1.txt_cbl_des_f.Text, 0.0); }
            set { uC_CableStayedDesign1.txt_cbl_des_f.Text = value.ToString("f3"); }
        }

        public void Design_Cables(ref CableMember mem)
        {
            double Ax = Cable_Ax;
            double gamma = Cable_Gamma;
            double alpha_x = mem.InclinationAngle * (Math.PI / 180);
            double na = 0.0;
            double n = 0.0;
            double a = 0.0;
            double E = Cable_E;
            double Fx = 0.0;
            double f = Cable_f;

            double x1, y1, z1;
            double x2, y2, z2;
            double x3, y3, z3;

            Fx = mem.AnalysisForce.Force;

            x1 = mem.MemberDetails.StartNode.X;
            y1 = mem.MemberDetails.StartNode.Y;
            z1 = mem.MemberDetails.StartNode.Z;

            x2 = mem.MemberDetails.EndNode.X;
            y2 = mem.MemberDetails.EndNode.Y;
            z2 = mem.MemberDetails.EndNode.Z;


            x3 = x1;
            y3 = y2;
            z3 = z2;

            List<string> list = new List<string>();
            //list.Add(string.Format("Cross Sectional Area of Cable [Ax] = {0} sq.mm", Ax));
            //list.Add(string.Format("Angle of Inclination of Cable [alpha_x] = {0} ", alpha_x));
            //list.Add(string.Format("Horizontal Projection of Cable Length [na] = {0} ", na));
            //list.Add(string.Format("Number of Panels up to the Cable = {0} ", n));
            //list.Add(string.Format("Length of Each Panel = {0} "));
            //list.Add(string.Format("Elastic Modulus of Cable Material = {0} "));
            //list.Add(string.Format("Force in the Cable [Fx] = {0} ", Fx));

            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User's Cable No : {0}", mem.User_MemberNo));
            list.Add(string.Format("ASTRA's Cable No : {0} ", mem.ASTRA_MemberNo));
            list.Add(string.Format("Joint Coorinate at Upper End : {0} [x1 : {1:f3}, y1 : {2:f3}, z1 : {3:f3}]", mem.StartJointNo, mem.StartJoint_X, mem.StartJoint_Y, mem.StartJoint_Z));
            list.Add(string.Format("Joint Coorinate at Lower End : {0} [x2 : {1:f3}, y2 : {2:f3}, z2 : {3:f3}]", mem.EndJointNo, mem.EndJoint_X, mem.EndJoint_Y, mem.EndJoint_Z));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Diameter = d = {0:f3} m", Cable_D));
            list.Add(string.Format("Provided Cable Cross Section Area     = π*d^2/4 = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Permissible Shear Stress [f] = {0} N/sq.mm = {1} Ton/sq.m", f, (f = f * 100)));
            list.Add(string.Format("Specific Weight of Cable Material [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));


            list.Add(string.Format("Specific Weight of Cable [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));
            list.Add(string.Format("Analysis Force :  {0:E3} Ton           [Member No :{1}, LoadCase : {2}]", mem.AnalysisForce, mem.AnalysisForce.MemberNo, mem.AnalysisForce.Loadcase));
            list.Add(string.Format("Analysis Stress : {0:E3} Ton/sq.m      [Member No :{1}, LoadCase : {2}]", mem.AnalysisStress, mem.AnalysisStress.MemberNo, mem.AnalysisStress.Loadcase));
            list.Add(string.Format(""));
            list.Add(string.Format("Pylon Coordinate at Deck : {0} m", mem.StartJoint_Y));
            list.Add(string.Format(""));
            double Lx = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
            double Lh = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3) + (z2 - z3) * (z2 - z3));
            double Lv = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3) + (z1 - z3) * (z1 - z3));
            //list.Add(string.Format("Length of Cable = Lx = SQRT((x1 - x2)^2 + (y1 - y2)^2 + (z1 - z2)^2) = {0:f3} m", Lx));
            list.Add(string.Format("Length of Cable = Lx = {0:f3} m", Lx));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lh = SQRT((x2 - x3)^2 + (y2 - y3)^2 + (z2 - z3)^2) = {0:f3} m", Lh));
            list.Add(string.Format("Length of Horizontal Projection = Lh = {0:f3} m", Lh));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lv = SQRT((x1 - x3)^2 + (y1 - y3)^2 + (z1 - z3)^2) = {0:f3} m", Lv));
            list.Add(string.Format("Length of Vertical Projection = Lv = {0:f3} m", Lv));
            list.Add(string.Format(""));
            alpha_x = mem.MemberDetails.InclinationAngle_in_Radian;
            list.Add(string.Format("Inclination Angle of Cable = α_x"));
            list.Add(string.Format("                           = tan^(-1)[(y2-y1)/(x2-x1)]", Lv));
            list.Add(string.Format("                           = {0:f0}° (degree)", mem.InclinationAngle));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Pu = Fx * Math.Sin(alpha_x);
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Component of Force = Pu = Fx * Sin α_x"));
            list.Add(string.Format(""));
            list.Add(string.Format("                                 = {0:E3} * {1:F3}", Fx, Math.Sin(alpha_x)));
            list.Add(string.Format("                                 = {0:E3} Ton", Pu));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Ax = Fx / f;
            list.Add(string.Format("Required Cross Section Area = Ax = Fx / f"));
            list.Add(string.Format("                                 = {0:E3} / {1}", Fx, f));
            list.Add(string.Format("                                 = {0:E3} sq.m", Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Area = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double W = Cable_Ax * Lx * gamma;
            list.Add(string.Format("Weight of Cable = W = Ax * Lx * γ "));
            list.Add(string.Format("                = {0:E3} * {1:f3} * {2}", Cable_Ax, Lx, gamma));
            list.Add(string.Format("                = {0:E3} Ton", W));
            list.Add(string.Format(""));

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Calculated Force = Fx"));
            //list.Add(string.Format("                 = Pu/Sin α_x"));
            //list.Add(string.Format("                 = {0:E3}/{1:f3}", Pu, Math.Sin(alpha_x)));
            //list.Add(string.Format("                 = {0:E3} Ton", Fx));
            //list.Add(string.Format(""));

            mem.CalculatedStress = Fx / Ax;
            //list.Add(string.Format("Calculated Stress = Fx / Ax "));
            //list.Add(string.Format("                  =  Pu/(Sin α_x * Ax)"));
            //list.Add(string.Format("                  =  {0:E3}/({1:F3} * {2:E3})", Pu, Math.Sin(alpha_x), Ax));
            //list.Add(string.Format("                  =  {0:E3} Ton/sq.m", mem.CalculatedStress));
            list.Add(string.Format(""));
            list.Add(string.Format("E = Stress / Strain"));
            mem.Strain = (Fx / Cable_Ax) / E;
            list.Add(string.Format("Strain = Stress / E  "));
            list.Add(string.Format("       = (Fx / Ax) / E  "));
            list.Add(string.Format("       = ({0:E3}/{1:E3}) / {2:E3}", Fx, Cable_Ax, E));
            list.Add(string.Format("       = {0:E3} ", mem.Strain));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Strain  = Elongation / Lx"));

            mem.Elongation = mem.Strain * Lx;
            list.Add(string.Format(""));

            list.Add(string.Format("Elongation in Cable = δx = Strain * Lx"));
            list.Add(string.Format("                         = {0:E3} * {1:E3} ", mem.Strain, Lx));
            list.Add(string.Format("                         = {0:E3}  m", mem.Elongation));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Deflection of Deck at joint x2 = δhn = δx / Sin α_x"));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Sin(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Vertical_Deflection_at_Deck));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Deflection at Pylon Top      = δhx = δx/Cos α_x "));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Cos(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Horizontal_Deflection_at_Pylon_Top));
            list.Add("");
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add("");
            list.Add("");

            mem.DesignResult.Clear();
            mem.DesignResult.AddRange(list.ToArray());

        }


        public void Read_Angle_Sections(ComboBox cmb)
        {
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
        public void Read_Angle_Sections()
        {

        }
        public void Read_Beam_Sections()
        {

        }
        public void Read_Channel_Sections()
        {

        }



        #endregion


        public eDesignStandard DesignStandard { get { return iApp.DesignStandard; } }

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



            DataGridView dgv = dgv_long_liveloads;


            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                dgv = dgv_LRFD_long_liveloads;



            bool flag = false;
            for (i = 0; i < dgv.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv.ColumnCount; c++)
                {
                    kStr = dgv[c, i].Value.ToString();


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
            //long_ll.Add(string.Format(""));
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


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_IRC_XINCR.Text, 0.0);
            double imp_fact = 1.179;


            dgv = dgv_long_loads;
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                dgv = dgv_LRFD_long_loads;
                xinc = MyList.StringToDouble(txt_LRFD_XINCR.Text, 0.0);
            }

            int count = 0;
            for (i = 0; i < dgv.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv[0, i].Value.ToString();

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

                    if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                        list.Add("LOAD GENERATION " + txt_LRFD_LL_load_gen.Text);
                    else
                        list.Add("LOAD GENERATION " + txt_IRC_LL_load_gen.Text);


                    string fn = "";
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} {2:f3} {3:f3} XINC {4}", def_load[j], def_x[j], 0.0, def_z[j], xinc);
                        list.Add(txt);

                        fn = fn + " " + def_load[j];
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
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
                for (c = 1; c < dgv.ColumnCount; c++)
                {
                    kStr = dgv[c, i].Value.ToString();

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
        #region LRFD Loading
        public void Default_LRFD_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            list.Add(string.Format("TYPE 1, LRFD_HTL57"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.5,10.5,10.5,10.5,10.5,4.5 "));
            list.Add(string.Format("AXLE SPACING IN METRES,1.6,4.572,4.572,1.6,4.572"));
            list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
            list.Add(string.Format("IMPACT FACTOR,1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, LRFD_HL93_HS20"));
            list.Add(string.Format("AXLE LOAD IN TONS,4.0,16.0,16.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,4.2672,4.2672 "));
            list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
            list.Add(string.Format("IMPACT FACTOR,1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, LRFD_HL93_H20"));
            list.Add(string.Format("AXLE LOAD IN TONS,4.0,16.0 "));
            list.Add(string.Format("AXLE SPACING IN METRES,4.2672 "));
            list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
            list.Add(string.Format("IMPACT FACTOR,1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, LRFD_H30S24"));
            list.Add(string.Format("AXLE LOAD IN TONS,60.0,240.0,240.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,4.25,8.0"));
            list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
            list.Add(string.Format("IMPACT FACTOR,1.10"));

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
            dgv_live_load.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_live_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void Default_LRFD_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            #region Long Girder
            list.Clear();
            list.Add(string.Format("LOAD 1,TYPE 4"));
            list.Add(string.Format("X,-16.916"));
            list.Add(string.Format("Z,0.60"));
            list.Add(string.Format(" "));
            list.Add(string.Format("LOAD 2,TYPE 4,TYPE 4"));
            list.Add(string.Format("X,-16.916,-16.916"));
            list.Add(string.Format("Z,0.60,3.60"));
            list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 3,TYPE 4,TYPE 4,TYPE 4"));
            //list.Add(string.Format("X,-16.916,-16.916,-16.916"));
            //list.Add(string.Format("Z,0.60,3.60,6.60"));
            //list.Add(string.Format(" "));
            //list.Add(string.Format("LOAD 4,TYPE 4,TYPE 4,TYPE 4,TYPE 4"));
            //list.Add(string.Format("X,-16.916,-16.916,-16.916,-16.916"));
            //list.Add(string.Format("Z,0.60,3.60,6.60,9.60"));
            //list.Add(string.Format(" "));
            #endregion

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


            dgv_live_load.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_live_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        #endregion LRFD LOading

        private void uC_CableStayedDesign1_Load(object sender, EventArgs e)
        {

        }

        private void uC_PierDesignLimitState1_OnProcess(object sender, EventArgs e)
        {
            Write_All_Data();
        }

        private void btn_irc_view_moving_load_Click(object sender, EventArgs e)
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
                Write_All_Data();
                Analysis_Initialize_InputData();

                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    British_Interactive();

                    LONG_GIRDER_BRITISH_LL_TXT();
                }
                else
                {
                    Text_Changed();
                    LONG_GIRDER_LL_TXT();
                }

                if (cmb_cable_type.SelectedIndex == 2)
                {
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        Create_Data_LL(Input_File_LL);
                    }
                    else
                    {
                        Create_Data_LL_British(Input_File_LL);
                    }
                    Create_Data_DL(Input_File_DL);
                }
                else
                {
                    Create_Data_Extradossed(Input_File);

                    string ll_file = Extradosed.Get_LL_Analysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 1);
                    //iApp.View_MovingLoad(ll_file);
                    iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_irc_vehicle_gap.Text));
                }

            }
            catch (Exception ex) { }
        }

        private void btn_bs_view_moving_load_Click(object sender, EventArgs e)
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
                Write_All_Data();
                Analysis_Initialize_InputData();

                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    LONG_GIRDER_BRITISH_LL_TXT();
                else
                    LONG_GIRDER_LL_TXT();

                if (cmb_cable_type.SelectedIndex == 2)
                {
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    {
                        Create_Data_LL(Input_File_LL);
                    }
                    else
                    {
                        Create_Data_LL_British(Input_File_LL);
                    }
                    Create_Data_DL(Input_File_DL);
                }
                else
                {
                    Create_Data_Extradossed(Input_File);

                    string ll_file = Extradosed.Get_LL_Analysis_Input_File(cmb_bs_view_moving_load.SelectedIndex + 1);
                    //iApp.View_MovingLoad(ll_file);
                    iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_bs_vehicle_gap.Text));
                }

            }
            catch (Exception ex) { }
        }

        private void uC_BoxGirder1_Load(object sender, EventArgs e)
        {

        }


    }


    public class CABLE_STAYED_Extradosed_LS_Analysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }

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


        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;


        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;

        public bool Apply_Selfweight { get; set; }
        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        public int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        public PSC_Box_Section_Data PSC_SECIONS;

        public List<MemberSectionProperty> SectionProperty { get; set; }

        //Chiranjit [2013 06 06] Kolkata

        public string List_Envelop_Inner { get; set; }
        public string List_Envelop_Outer { get; set; }

        public string Start_Support { get; set; }
        public string End_Support { get; set; }


        //E_CONC;
        //E_STEEL;
        //E_CABLE;
        //DEN_CONC;
        //DEN_STEEL;
        //DEN_CABLE;
        //PR_CONC;
        //PR_STEEL;
        //PR_CABLE;
        #region Material Constant
        public string E_CONC { get; set; }
        public string E_STEEL { get; set; }
        public string E_CABLE { get; set; }
        public string DEN_CONC { get; set; }
        public string DEN_STEEL { get; set; }
        public string DEN_CABLE { get; set; }
        public string PR_CONC { get; set; }
        public string PR_STEEL { get; set; }
        public string PR_CABLE { get; set; }
        #endregion Material Constant

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
        #endregion Analysis Input File
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
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "DL + LL Combine Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DL_LL_Combine_Input_File.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

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

        //Chiranjit [2014 10 22]
        public string Get_LL_Analysis_Input_File(int AnalysisNo)
        {
            if (AnalysisNo <= 0) return "";

            if (Directory.Exists(Working_Folder))
            {
                string pd = Path.Combine(Working_Folder, "LL Analysis Load " + AnalysisNo);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_Load_" + AnalysisNo + "_Input_File.txt");



            }
            return "";
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

        string input_file, user_path;
        public CABLE_STAYED_Extradosed_LS_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            //Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            //Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            List_Envelop_Inner = "";
            List_Envelop_Outer = "";



            Cable_Nos = 6;
            Init_dist = 15.0;
            Cable_x_dist = 6.0;
            Cable_y_dist = 3.0;
            Cable_intv_dist = 1.5;
            Tower_height = 12.0;

            Apply_Selfweight = true;


            L1 = 0.0;

            L2 = 0.0;

            L3 = 0.0;

            Spacing_Cross_Girder = 0.0;

            Spacing_Long_Girder = 0.0;

            Skew_Angle = 0.0;

            IsCentral_Cable = false;



            #region Distances

            _Support_Dist = new List<double>();
            _Deff_Dist = new List<double>();
            _L8_Dist = new List<double>();
            _L4_Dist = new List<double>();
            _3L8_Dist = new List<double>();
            _L2_Dist = new List<double>();

            #endregion Distances
        }


        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();

        #region Chiranjit [2014 09 02] For British Standard

        public List<int> HA_Lanes;

        public string HA_Loading_Members;
        public void CreateData_British()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();


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


            List<double> HA_distances = new List<double>();
            if (HA_Lanes.Count > 0)
            {
                double ha = 0.0;

                for (int i = 0; i < HA_Lanes.Count; i++)
                {
                    ha = 1.75 + (HA_Lanes[i] - 1) * 3.5;
                    if (!list_z.Contains(ha))
                    {
                        list_z.Add(ha);
                        HA_distances.Add(ha);
                    }
                }
            }

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
                    list.Add(list_x[iCols] + list_z[iRows]);
                }
                z_table.Add(list_z[iRows], list);
            }

            int nodeNo = 0;
            Joints.Clear();


            #region Chiranjit [2013 05 30]

            #endregion Chiranjit [2013 05 30]

            #region Chiranjit [2013 06 06]

            #endregion Chiranjit [2013 06 06]
        }

        #endregion Chiranjit [2014 09 02] For British Standard

        public void Clear()
        {
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
                    //MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public double WidthBridge { get; set; }


        public List<int> Long_Girder_members = new List<int>();
        public List<int> Cross_Girder_members = new List<int>();
        public List<int> Tower_members = new List<int>();
        public List<int> Cable_members = new List<int>();
        public List<int> Supports = new List<int>();



        public List<int> side_load_joints = new List<int>();
        public List<int> central_load_joints = new List<int>();


        public List<int> side_supports = new List<int>();
        public List<int> central_supports = new List<int>();

        public void Create_Extradossed_Data1()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();

            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];

            #endregion End Span 2

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}

            list_x.AddRange(lst_x_coords);


            bool flag = true;

            list_x.Sort();


            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 0; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();
            MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.StartNode.NodeNo);

            }


            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members





            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));

        }



       public  List<int> _L8_mems = new List<int>();
       public List<int> _L4_mems = new List<int>();
       public List<int> _3L8_mems = new List<int>();
       public List<int> _L2_mems = new List<int>();
       public List<int> _supp_mems = new List<int>();
       public List<int> _deff_mems = new List<int>();
       public List<int> _End_mems = new List<int>();



       public List<int> _L8_jnts = new List<int>();
       public List<int> _L4_jnts = new List<int>();
       public List<int> _3L8_jnts = new List<int>();
       public List<int> _L2_jnts = new List<int>();
       public List<int> _supp_jnts = new List<int>();
       public List<int> _deff_jnts = new List<int>();
       public List<int> _End_jnts = new List<int>();
       public List<int> _Cable_jnts = new List<int>();


       public bool IsCentral_Cable { get; set; }

        public void Create_Extradossed_Data()
        {
            _L8_jnts.Clear();
            _L4_jnts.Clear();
            _3L8_jnts.Clear();
            _L2_jnts.Clear();
            _supp_jnts.Clear();
            _deff_jnts.Clear();
            _End_jnts.Clear();

            _Cable_jnts.Clear();


            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                //lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    //if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.1)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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

                    if (iRows == 1 || iRows == 3)
                    {
                        if (Joints_Array[iRows, iCols].X >= L1 && Joints_Array[iRows, iCols].X <= L1 + L2)
                        {
                            _Cable_jnts.Add(nodeNo);
                        }
                    }

                    if (iRows == 2)
                    {
                        var x = Joints_Array[iRows, iCols].X;
                        if (_Support_Dist.Contains(x))
                        {
                            _supp_jnts.Add(nodeNo);
                        }
                        else if (_Deff_Dist.Contains(x))
                        {
                            _deff_jnts.Add(nodeNo);
                        }
                        else if (_L8_Dist.Contains(x))
                        {
                            _L8_jnts.Add(nodeNo);
                        }
                        else if (_L4_Dist.Contains(x))
                        {
                            _L4_jnts.Add(nodeNo);
                        }
                        else if (_3L8_Dist.Contains(x))
                        {
                            _3L8_jnts.Add(nodeNo);
                        }
                        else if (_L2_Dist.Contains(x))
                        {
                            _L2_jnts.Add(nodeNo);
                        }
                    }
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        //Chiranjit Remove one Side cables
        public void Create_Extradossed_Data_1_Side()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    //Joints.Add(nd);
                }
            }
            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
            }
            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_Data_1_Side_Centre()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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


                    if (iRows == 2)
                    {
                        if (Joints_Array[iRows, iCols].X >= L1 && Joints_Array[iRows, iCols].X <= L1 + L2)
                        {
                            _Cable_jnts.Add(nodeNo);
                        }
                    }

                    if (iRows == 1 || iRows == 3)
                    {
                        var x = Joints_Array[iRows, iCols].X;
                        if (_Support_Dist.Contains(x))
                        {
                            _supp_jnts.Add(nodeNo);
                        }
                        else if (_Deff_Dist.Contains(x))
                        {
                            _deff_jnts.Add(nodeNo);
                        }
                        else if (_L8_Dist.Contains(x))
                        {
                            _L8_jnts.Add(nodeNo);
                        }
                        else if (_L4_Dist.Contains(x))
                        {
                            _L4_jnts.Add(nodeNo);
                        }
                        else if (_3L8_Dist.Contains(x))
                        {
                            _3L8_jnts.Add(nodeNo);
                        }
                        else if (_L2_Dist.Contains(x))
                        {
                            _L2_jnts.Add(nodeNo);
                        }
                    }
                }
            }

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    //Joints.Add(nd);
                }
            }
            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
            }
            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();

            //Chiranjit [2017 03 29]
            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }










            //MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            if (false)
            {
                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }
            }
            #endregion Tower Members

            if (true)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;

                int a = 0;
                while (a <= 2)
                {
                    Member item = null;
                    a++;
                    for (i = 0; i < _Columns - 2; i++)
                    {
                        //var item = Long_Girder_Members_Array[2, i];

                        if (a == 1)
                            item = Long_Girder_Members_Array[1, i];
                        else
                            item = Long_Girder_Members_Array[3, i];

                        _End_mems.Add(item.MemberNo);



                        //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                        //    cnt = 1;
                        //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                        //    cnt = 2;
                        //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                        //    cnt = 3;
                        //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                        //    cnt = 4;
                        //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                        //    cnt = 5;
                        //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                        //    cnt = 6;


                        //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                        //    _supp_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                        //    _deff_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                        //    _L8_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                        //    _L4_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                        //    _3L8_mems.Add(item.MemberNo);
                        //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                        //    _L2_mems.Add(item.MemberNo);



                        //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                        //   cnt = 1;
                        //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                        //    cnt = 2;
                        //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                        //    cnt = 3;
                        //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                        //    cnt = 4;
                        //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                        //    cnt = 5;
                        //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                        //    cnt = 6;

                        foreach (var d in _L2_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 6;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _3L8_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 5;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _L4_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 4;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _L8_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 3;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _Deff_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 2;
                                goto _SS;
                                break;
                            }
                        }
                        foreach (var d in _Support_Dist)
                        {

                            if (Math.Abs(item.StartNode.X - d) < 0.9)
                            {
                                cnt = 1;
                                goto _SS;
                                break;
                            }
                        }

                    _SS:

                        if (cnt == 1)
                        {
                            _supp_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 2)
                        {
                            _deff_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 3)
                        {
                            _L8_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 4)
                        {
                            _L4_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 5)
                        {
                            _3L8_mems.Add(item.MemberNo);
                        }
                        else if (cnt == 6)
                        {
                            _L2_mems.Add(item.MemberNo);
                        }
                    }
                }


                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_CentralCable_Data()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();


            _End_mems.Clear();
            _L8_mems.Clear();
            _L4_mems.Clear();
            _3L8_mems.Clear();
            _L2_mems.Clear();
            _supp_mems.Clear();
            _deff_mems.Clear();


            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance != 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1



                #region Central Span
                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));
                #endregion Central Span


                #region Span 2

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 1

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                //list_x.AddRange(lst1.ToArray());

                //foreach (var item in lst2)
                //{
                //    list_x.Add(L1 + item);
                //}



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            //count = 2;
            count = 4;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();


            #region Section Define
            lst2.Sort();


            List<double> tmp = new List<double>();


            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst2.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + lst2[i])) < 0.9)
                    {
                        lst2.RemoveAt(i); i--; break;
                    }
                }
            }

            for (i = 0; i < lst1.Count; i++)
            {
                foreach (var item in list_x)
                {
                    if (Math.Abs(item - (L1 + L2 + lst1[i])) < 0.9)
                    {
                        lst1.RemoveAt(i); i--; break;
                    }
                }
            }


            foreach (var item in lst1)
            {
                list_x.Add(item);
            }


            foreach (var item in lst2)
            {
                list_x.Add(L1 + item);
            }

            foreach (var item in lst1)
            {
                list_x.Add(L1 + L2 + item);
            }

            list_x.Sort();


            #endregion Section Define

            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            if (false)
            {
                #region Right Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }

                #endregion Right Tower 1

                #region Right Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[list_z.Count - 2];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }

                #endregion Right Tower 1
            }


            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();



            #region Cable


            if (false)
            {
                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members
            }
            #endregion Cable











            //MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            #endregion Tower Members

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));
            if (true)
            {
                #region Define


                _supp_mems.Clear();
                _deff_mems.Clear();
                _L8_mems.Clear();
                _L4_mems.Clear();
                _3L8_mems.Clear();
                _L2_mems.Clear();



                int cnt = 0;
                for (i = 0; i < _Columns - 2; i++)
                {
                    var item = Long_Girder_Members_Array[2, i];


                    _End_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[0]) < 0.9)
                    //    cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[0]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[0]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[0]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[0]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;


                    //if (Math.Abs(item.EndNode.X - _Support_Dist[1]) < 0.9)
                    //    _supp_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _Deff_Dist[1]) < 0.9)
                    //    _deff_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L8_Dist[1]) < 0.9)
                    //    _L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L4_Dist[1]) < 0.9)
                    //    _L4_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _3L8_Dist[1]) < 0.9)
                    //    _3L8_mems.Add(item.MemberNo);
                    //else if (Math.Abs(item.EndNode.X - _L2_Dist[0]) < 0.9)
                    //    _L2_mems.Add(item.MemberNo);



                    //if (Math.Abs(item.StartNode.X - _Support_Dist[1]) < 0.9)
                    //   cnt = 1;
                    //else if (Math.Abs(item.StartNode.X - _Deff_Dist[1]) < 0.9)
                    //    cnt = 2;
                    //else if (Math.Abs(item.StartNode.X - _L8_Dist[1]) < 0.9)
                    //    cnt = 3;
                    //else if (Math.Abs(item.StartNode.X - _L4_Dist[1]) < 0.9)
                    //    cnt = 4;
                    //else if (Math.Abs(item.StartNode.X - _3L8_Dist[1]) < 0.9)
                    //    cnt = 5;
                    //else if (Math.Abs(item.StartNode.X - _L2_Dist[0]) < 0.9)
                    //    cnt = 6;

                    foreach (var d in _L2_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 6;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _3L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 5;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L4_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 4;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _L8_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 3;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Deff_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 2;
                            goto _SS;
                            break;
                        }
                    }
                    foreach (var d in _Support_Dist)
                    {

                        if (Math.Abs(item.StartNode.X - d) < 0.9)
                        {
                            cnt = 1;
                            goto _SS;
                            break;
                        }
                    }

                _SS:

                    if (cnt == 1)
                    {
                        _supp_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 2)
                    {
                        _deff_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 3)
                    {
                        _L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 4)
                    {
                        _L4_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 5)
                    {
                        _3L8_mems.Add(item.MemberNo);
                    }
                    else if (cnt == 6)
                    {
                        _L2_mems.Add(item.MemberNo);
                    }
                }

                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L4_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _3L8_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _deff_mems)
                {
                    _End_mems.Remove(item);
                }
                foreach (var item in _L2_mems)
                {
                    _End_mems.Remove(item);
                }

                #endregion
            }
        }

        public void Create_Extradossed_Data_2017_02_16()
        {

            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance == 0.0)
            {
                #region Span 1

                //_Support_Dist.Add(Support_Distance);
                //_Support_Dist.Add(L1 - Support_Distance);

                //_Deff_Dist.Add(Effective_Distance);
                //_Deff_Dist.Add(L1 - Effective_Distance);

                //_L8_Dist.Add(L1 / 8);
                //_L8_Dist.Add(L1 - L1 / 8);

                //_L4_Dist.Add(L1 / 4);
                //_L4_Dist.Add(L1 - L1 / 4);


                //_3L8_Dist.Add(3 * L1 / 8.0);
                //_3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                //_L2_Dist.Add(L1 / 2.0);

                #endregion Span 1




                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));



                //lst1.Add(Support_Distance);
                //lst1.Add(Effective_Distance);
                //lst1.Add(L1 / 8);
                //lst1.Add(L1 / 4);
                //lst1.Add(3 * L1 / 8);
                //lst1.Add(L1 / 2);

                //lst1.Add(L1 - Support_Distance);
                //lst1.Add(L1 - Effective_Distance);
                //lst1.Add(L1 - L1 / 8);
                //lst1.Add(L1 - L1 / 4);
                //lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                list_x.AddRange(lst1.ToArray());

                foreach (var item in lst2)
                {
                    list_x.Add(L1 + item);
                }



                lst2.Sort();


                //foreach (var item in lst1)
                //{
                //    list_x.Add(L1 + L2 + item);
                //}

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();




            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;



            #region Z Coordinates


            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //last_z = WidthBridge / 2.0;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            ////last_z = WidthBridge - WidthCantilever;
            ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            ////list_z.Add(last_z);

            //last_z = WidthBridge;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            //last_z = WidthCantilever + z_incr;
            //do
            //{
            //    flag = false;
            //    for (i = 0; i < list_z.Count; i++)
            //    {
            //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
            //        list_z.Add(last_z);
            //    last_z += z_incr;
            //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //} while (last_z <= WidthBridge);


            list_z.Clear();
            list_z.Add(0);
            list_z.Add(Cantilever_Width);
            list_z.Add(WidthBridge - Cantilever_Width);


            list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
            list_z.Add(WidthBridge);



            //list_z.Add(2.5);
            //list_z.Add(2.5 + 2.0);
            //list_z.Add(4.5 + 4.0);
            //list_z.Add(4.5 + 4.0 + 2.0);
            //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


            list_z.Sort();
            #endregion Z Coordinates


            #endregion Chiranjit [2013 07 25] Correct Create Data

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

            JointNodeCollection verticals = new JointNodeCollection();


            #region Left Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1


            #region Left Tower 2

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[1];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }
            #endregion Left Tower 1

            #region Right Tower 1

            for (i = 1; i < list_y.Count; i++)
            {
                nd = new JointNode();

                nd.Y = list_y[i];
                nd.Z = list_z[list_z.Count - 2];
                nd.X = L1 + L2;

                nd.NodeNo = Joints.JointNodes.Count + 1;
                Joints.Add(nd);
            }

            #endregion Right Tower 1



            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();



            MemColls.Clear();



            #region Cable

            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);


                side_load_joints.Add(mem.StartNode.NodeNo);
            }






            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members


            #region Cable Members

            y_cables.Sort();
            y_cables.Reverse();
            for (i = 0; i < x_cable1.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.StartNode.NodeNo);

            }


            y_cables.Reverse();


            for (i = 0; i < x_cable2.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.EndNode.NodeNo);
            }




            y_cables.Reverse();


            for (i = 0; i < x_cable3.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);
                central_load_joints.Add(mem.StartNode.NodeNo);
            }



            y_cables.Reverse();


            for (i = 0; i < x_cable4.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);
                Cable_members.Add(mem.MemberNo);

                side_load_joints.Add(mem.EndNode.NodeNo);
            }



            #endregion Cable Members

            #endregion Cable











            //MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }


            #region Tower Members


            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[1]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[1]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }

            for (i = 1; i < list_y.Count; i++)
            {
                mem = new Member();
                mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                mem.MemberNo = MemColls.Count + 1;
                MemColls.Add(mem);

                Tower_members.Add(mem.MemberNo);
            }
            #endregion Tower Members

            if (false)
            {
                #region Cable

                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[1]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[1]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[1]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.StartNode.NodeNo);

                }


                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                #endregion Cable
            }

            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[1]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));



            List<int> deff_mems = new List<int>();


            //Long_Girder_Members_Array
        }

        public void Create_Extradossed_Linear_Data()
        {
            Long_Girder_members.Clear();
            Cross_Girder_members.Clear();
            Tower_members.Clear();
            Cable_members.Clear();
            Supports.Clear();

            side_supports.Clear();
            central_supports.Clear();
            side_load_joints.Clear();
            central_load_joints.Clear();



            _Support_Dist.Clear();
            _Deff_Dist.Clear();
            _L8_Dist.Clear();
            _L4_Dist.Clear();
            _3L8_Dist.Clear();
            _L2_Dist.Clear();

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            Hashtable z_table = new Hashtable();



            last_x = 0.0;

            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();



            #region ss

            List<double> lst1 = new List<double>();
            List<double> lst2 = new List<double>();
            if (Effective_Distance == 0.0)
            {
                #region Span 1

                _Support_Dist.Add(Support_Distance);
                _Support_Dist.Add(L1 - Support_Distance);

                _Deff_Dist.Add(Effective_Distance);
                _Deff_Dist.Add(L1 - Effective_Distance);

                _L8_Dist.Add(L1 / 8);
                _L8_Dist.Add(L1 - L1 / 8);

                _L4_Dist.Add(L1 / 4);
                _L4_Dist.Add(L1 - L1 / 4);


                _3L8_Dist.Add(3 * L1 / 8.0);
                _3L8_Dist.Add(L1 - 3 * L1 / 8.0);


                _L2_Dist.Add(L1 / 2.0);

                #endregion Span 1


                #region Span 2


                _Support_Dist.Add(L1 + Support_Distance);
                _Support_Dist.Add(L1 + (L2 - Support_Distance));

                _Deff_Dist.Add(L1 + Effective_Distance);
                _Deff_Dist.Add(L1 + (L2 - Effective_Distance));

                _L8_Dist.Add(L1 + (L2 / 8));
                _L8_Dist.Add(L1 + (L2 - (L2 / 8)));

                _L4_Dist.Add(L1 + (L2 / 4));
                _L4_Dist.Add(L1 + L2 - (L2 / 4));

                _3L8_Dist.Add(L1 + (3 * L2 / 8.0));
                _3L8_Dist.Add(L1 + L2 - (3 * L2 / 8.0));


                _L2_Dist.Add(L1 + (L2 / 2.0));

                #endregion Span 2

                #region Span 3

                _Support_Dist.Add((L1 + L2) + Support_Distance);
                _Support_Dist.Add((L1 + L2) + L1 - Support_Distance);

                _Deff_Dist.Add((L1 + L2) + Effective_Distance);
                _Deff_Dist.Add((L1 + L2) + L1 - Effective_Distance);

                _L8_Dist.Add((L1 + L2) + L1 / 8);
                _L8_Dist.Add((L1 + L2) + L1 - L1 / 8);

                _L4_Dist.Add((L1 + L2) + L1 / 4);
                _L4_Dist.Add((L1 + L2) + L1 - L1 / 4);


                _3L8_Dist.Add((L1 + L2) + 3 * L1 / 8.0);
                _3L8_Dist.Add((L1 + L2) + L1 - 3 * L1 / 8.0);


                _L2_Dist.Add((L1 + L2) + L1 / 2.0);

                #endregion Span 3

                lst1.Add(Support_Distance);
                lst1.Add(Effective_Distance);
                lst1.Add(L1 / 8);
                lst1.Add(L1 / 4);
                lst1.Add(3 * L1 / 8);
                lst1.Add(L1 / 2);

                lst1.Add(L1 - Support_Distance);
                lst1.Add(L1 - Effective_Distance);
                lst1.Add(L1 - L1 / 8);
                lst1.Add(L1 - L1 / 4);
                lst1.Add(L1 - (3 * L1 / 8));

                lst1.Sort();



                lst2.Add(Support_Distance);
                lst2.Add(Effective_Distance);
                lst2.Add(L2 / 8);
                lst2.Add(L2 / 4);
                lst2.Add(3 * L2 / 8);
                lst2.Add(L2 / 2);

                lst2.Add(L2 - Support_Distance);
                lst2.Add(L2 - Effective_Distance);
                lst2.Add(L2 - L2 / 8);
                lst2.Add(L2 - L1 / 4);
                lst2.Add(L2 - (3 * L2 / 8));


                lst2.Sort();

                list_x.AddRange(lst1.ToArray());

                foreach (var item in lst2)
                {
                    list_x.Add(L1 + item);
                }



                lst2.Sort();


                foreach (var item in lst1)
                {
                    list_x.Add(L1 + L2 + item);
                }

            }
            #endregion ss







            List<double> lst_x_coords = new List<double>();



            List<double> x_cable1 = new List<double>();
            List<double> x_cable2 = new List<double>();
            List<double> x_cable3 = new List<double>();
            List<double> x_cable4 = new List<double>();

            int i = 0;


            int count = 4;


            double start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));





            #region X-Coordinates

            #region End Span 1

            lst_x_coords.Clear();
            count = 4;

            for (i = 0; i <= count; i++)
            {
                last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x);
            }



            //Cable_Nos = 6;
            x_cable1.Add(last_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable1.Add(last_x);
                lst_x_coords.Add(last_x);
            }



            count = 3;

            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            #endregion End Span 1

            #region Mid Span 1


            count = 3;

            last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];



            x_cable2.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable2.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 2;


            double mid_gap = L2 - (2 * (Init_dist + Cable_x_dist * (Cable_Nos - 1)));

            for (i = 1; i <= count; i++)
            {
                last_x = start_x + ((mid_gap / count) * i);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];


            x_cable3.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable3.Add(last_x);
                lst_x_coords.Add(last_x);
            }


            start_x = lst_x_coords[lst_x_coords.Count - 1];

            count = 3;

            //last_x = L1;
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(last_x + (Init_dist / count) * i);
            }



            #endregion Mid Span 1

            #region End Span 2

            count = 3;

            //last_x = L1;
            start_x = lst_x_coords[lst_x_coords.Count - 1];
            for (i = 1; i <= count; i++)
            {
                lst_x_coords.Add(start_x + (Init_dist / count) * i);
            }

            start_x = lst_x_coords[lst_x_coords.Count - 1];

            x_cable4.Add(start_x);
            for (i = 1; i < Cable_Nos; i++)
            {
                last_x = (start_x + Cable_x_dist * i);
                x_cable4.Add(last_x);
                lst_x_coords.Add(last_x);
            }

            count = 4;


            start_x = L1 - Init_dist - (Cable_x_dist * (Cable_Nos - 1));

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            for (i = 1; i <= count; i++)
            {
                //last_x = ((start_x / count) * i);
                lst_x_coords.Add(last_x + ((start_x / count) * i));
            }

            last_x = lst_x_coords[lst_x_coords.Count - 1];


            #endregion End Span 2


            //list_x.AddRange(lst_x_coords);

            foreach (var item in lst_x_coords)
            {
                if (!list_x.Contains(item)) list_x.Add(item);
            }
            list_x.Sort();




            #endregion X-Coordinates


            #region Y - Coodinates

            list_y.Add(0.0);


            list_y.Add(Cable_y_dist);

            List<double> y_cables = new List<double>();

            y_cables.Add(Cable_y_dist);
            for (i = 1; i < Cable_Nos; i++)
            {
                //list_y.Add(cable_y_dist + cable_intv_dist * i);

                y_cables.Add(Cable_y_dist + Cable_intv_dist * i);
                list_y.Add(Cable_y_dist + Cable_intv_dist * i);

            }


            list_y.Add(Tower_height);

            #endregion Y - Coodinates


            //for (i = 0; i < lst_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            //}

            //for (i = 0; i < lst_mid_factor.Count; i++)
            //{
            //    lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3)) * lst_mid_factor[i]);
            //}
            //for (i = 0; i < lst_right_factor.Count; i++)
            //{
            //    lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) +
            //                     (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            //}



            bool flag = true;


            if (false)
            {
                #region Z Coordinates


                //last_z = WidthCantilever;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                //last_z = WidthBridge / 2.0;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                ////last_z = WidthBridge - WidthCantilever;
                ////last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                ////list_z.Add(last_z);

                //last_z = WidthBridge;
                //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //list_z.Add(last_z);

                //z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
                //last_z = WidthCantilever + z_incr;
                //do
                //{
                //    flag = false;
                //    for (i = 0; i < list_z.Count; i++)
                //    {
                //        if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                //        {
                //            flag = true;
                //            break;
                //        }
                //    }

                //    if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
                //        list_z.Add(last_z);
                //    last_z += z_incr;
                //    last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                //} while (last_z <= WidthBridge);


                list_z.Clear();
                list_z.Add(0);
                list_z.Add(Cantilever_Width);
                list_z.Add(WidthBridge - Cantilever_Width);


                list_z.Add(Cantilever_Width + ((WidthBridge - 2 * Cantilever_Width) / 2));
                list_z.Add(WidthBridge);



                //list_z.Add(2.5);
                //list_z.Add(2.5 + 2.0);
                //list_z.Add(4.5 + 4.0);
                //list_z.Add(4.5 + 4.0 + 2.0);
                //list_z.Add(4.5 + 4.0 + 2.0 + 2.5);


                list_z.Sort();
                #endregion Z Coordinates
            }
            list_z.Add(0);

            #endregion Chiranjit [2013 07 25] Correct Create Data

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

            if (true)
            {
                JointNodeCollection verticals = new JointNodeCollection();
                #region Left Tower 1

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[0];
                    nd.X = L1;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
                #endregion Left Tower 1

                if (false)
                {
                    #region Right Tower 1

                    for (i = 1; i < list_y.Count; i++)
                    {
                        nd = new JointNode();

                        nd.Y = list_y[i];
                        nd.Z = list_z[list_z.Count - 2];
                        nd.X = L1;

                        nd.NodeNo = Joints.JointNodes.Count + 1;
                        Joints.Add(nd);
                    }

                    #endregion Right Tower 1
                }


                #region Left Tower 2

                for (i = 1; i < list_y.Count; i++)
                {
                    nd = new JointNode();

                    nd.Y = list_y[i];
                    nd.Z = list_z[0];
                    nd.X = L1 + L2;

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);
                }
                #endregion Left Tower 1

                if (false)
                {
                    #region Right Tower 1

                    for (i = 1; i < list_y.Count; i++)
                    {
                        nd = new JointNode();

                        nd.Y = list_y[i];
                        nd.Z = list_z[list_z.Count - 2];
                        nd.X = L1 + L2;

                        nd.NodeNo = Joints.JointNodes.Count + 1;
                        Joints.Add(nd);
                    }

                    #endregion Right Tower 1
                }

            }

            Member mem = new Member();

            if (MemColls == null) MemColls = new MemberCollection();
            MemColls.Clear();
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

                    Long_Girder_members.Add(mem.MemberNo);
                }
            }



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
                    Cross_Girder_members.Add(mem.MemberNo);
                }
            }

            if (true)
            {
                #region Tower Members


                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }

                for (i = 1; i < list_y.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);

                    Tower_members.Add(mem.MemberNo);
                }

                //for (i = 1; i < list_y.Count; i++)
                //{
                //    mem = new Member();
                //    mem.StartNode = Joints.GetJoints(L1, list_y[i - 1], list_z[list_z.Count - 2]);
                //    mem.EndNode = Joints.GetJoints(L1, list_y[i], list_z[list_z.Count - 2]);
                //    mem.MemberNo = MemColls.Count + 1;
                //    MemColls.Add(mem);

                //    Tower_members.Add(mem.MemberNo);
                //}

                //for (i = 1; i < list_y.Count; i++)
                //{
                //    mem = new Member();
                //    mem.StartNode = Joints.GetJoints(L1 + L2, list_y[i - 1], list_z[list_z.Count - 2]);
                //    mem.EndNode = Joints.GetJoints(L1 + L2, list_y[i], list_z[list_z.Count - 2]);
                //    mem.MemberNo = MemColls.Count + 1;
                //    MemColls.Add(mem);

                //    Tower_members.Add(mem.MemberNo);
                //}
                #endregion Tower Members


                #region Cable Members

                y_cables.Sort();
                y_cables.Reverse();
                for (i = 0; i < x_cable1.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);


                    side_load_joints.Add(mem.StartNode.NodeNo);
                }






                y_cables.Reverse();


                for (i = 0; i < x_cable2.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[0]);
                    mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);

                    central_load_joints.Add(mem.EndNode.NodeNo);
                }




                y_cables.Reverse();


                for (i = 0; i < x_cable3.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[0]);
                    mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    central_load_joints.Add(mem.StartNode.NodeNo);
                }



                y_cables.Reverse();


                for (i = 0; i < x_cable4.Count; i++)
                {
                    mem = new Member();
                    mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[0]);
                    mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[0]);
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cable_members.Add(mem.MemberNo);
                    side_load_joints.Add(mem.EndNode.NodeNo);
                }



                #endregion Cable Members

                if (false)
                {
                    #region Cable Members

                    y_cables.Sort();
                    y_cables.Reverse();
                    for (i = 0; i < x_cable1.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable1[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.StartNode.NodeNo);

                    }


                    y_cables.Reverse();


                    for (i = 0; i < x_cable2.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable2[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.EndNode.NodeNo);
                    }




                    y_cables.Reverse();


                    for (i = 0; i < x_cable3.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(x_cable3[i], 0.0, list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);
                        central_load_joints.Add(mem.StartNode.NodeNo);
                    }



                    y_cables.Reverse();


                    for (i = 0; i < x_cable4.Count; i++)
                    {
                        mem = new Member();
                        mem.StartNode = Joints.GetJoints(L1 + L2, y_cables[i], list_z[list_z.Count - 2]);
                        mem.EndNode = Joints.GetJoints(x_cable4[i], 0.0, list_z[list_z.Count - 2]);
                        mem.MemberNo = MemColls.Count + 1;
                        MemColls.Add(mem);
                        Cable_members.Add(mem.MemberNo);

                        side_load_joints.Add(mem.EndNode.NodeNo);
                    }



                    #endregion Cable Members
                }
            }
            JointNode jn = Joints.GetJoints(0.0, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);

            jn = Joints.GetJoints(L1, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            central_supports.Add(jn.NodeNo);



            jn = Joints.GetJoints(L1 + L2, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);

            central_supports.Add(jn.NodeNo);


            jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[0]);
            Supports.Add(jn.NodeNo);
            side_supports.Add(jn.NodeNo);




            //jn = Joints.GetJoints(0.0, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //side_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //central_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1 + L2, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //central_supports.Add(jn.NodeNo);

            //jn = Joints.GetJoints(L1 + L2 + L3, 0.0, list_z[list_z.Count - 2]);
            //Supports.Add(jn.NodeNo);
            //side_supports.Add(jn.NodeNo);

            //Environment.SetEnvironmentVariable("sup", MyList.Get_Array_Text(Supports));



            List<int> deff_mems = new List<int>();


            //Long_Girder_Members_Array




            if (false)
            {
                List<int> _supp_mems = new List<int>();
                List<int> _deff_mems = new List<int>();
                List<int> _L8_mems = new List<int>();
                List<int> _L4_mems = new List<int>();
                List<int> _3L8_mems = new List<int>();
                List<int> _L2_mems = new List<int>();

                #region Section Properties
                Long_Girder_members.Sort();


                foreach (var item in MemColls)
                {
                    for (i = 0; i < _Support_Dist.Count; i++)
                    {
                        try
                        {
                            if (item.StartNode.X >= _Support_Dist[i] && item.StartNode.X <= _Deff_Dist[i])
                            {
                                _supp_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _Deff_Dist[i] && item.StartNode.X <= _L8_Dist[i])
                            {
                                _deff_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _L8_Dist[i] && item.StartNode.X <= _L4_Dist[i])
                            {
                                _L8_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _L4_Dist[i] && item.StartNode.X <= _3L8_Dist[i])
                            {
                                _L4_mems.Add(item.MemberNo); break;
                            }
                            else if (item.StartNode.X >= _3L8_Dist[i] && item.StartNode.X <= _L2_Dist[i])
                            {
                                _3L8_mems.Add(item.MemberNo); break;
                            }
                            else
                            {
                                _L2_mems.Add(item.MemberNo); break;
                            }
                        }
                        catch (Exception ex) { }

                    }

                }

                #endregion Section Properties
            }
        }

        public void WriteData_LiveLoad(string file_name, List<string> ll_txt_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;
            if (ll_txt_data == null)
                list.Add("ASTRA SPACE EXTRADOSSED BRIDGE DECK ANALYSIS WITH DEAD LOAD");
            else
                list.Add("ASTRA SPACE EXTRADOSSED BRIDGE DECK ANALYSIS WITH MOVING LOAD");

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

            //list.Add("SECTION PROPERTIES");
            list.AddRange(Get_Continuous_LL_Member_Properties_Data());
            //list.Add(string.Format("MATERIAL CONSTANT"));
            //list.Add(string.Format("E 3162277 ALL"));
            //list.Add(string.Format("DENSITY CONCRETE ALL"));
            //list.Add(string.Format("POISSON CONCRETE ALL"));

            list.Add(string.Format("MEMBER CABLE"));
            list.Add(string.Format("_CABLES"));
            if (false)
            {
                #region Mat Constants
                list.Add(string.Format("MEMBER CABLE"));
                list.Add(string.Format("_CABLES"));
                list.Add(string.Format("MATERIAL CONSTANT"));
                list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Long_Girder_members)));
                list.Add(string.Format("DENSITY CONCRETE {0}", MyList.Get_Array_Text(Long_Girder_members)));
                list.Add(string.Format("POISSON CONCRETE {0}", MyList.Get_Array_Text(Long_Girder_members)));


                if (Cross_Girder_members.Count > 0)
                {
                    list.Add(string.Format("MATERIAL CONSTANT"));
                    list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    list.Add(string.Format("DENSITY CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                    list.Add(string.Format("POISSON CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));
                }


                list.Add(string.Format("MATERIAL CONSTANT"));
                list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Tower_members)));
                list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Tower_members)));
                list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Tower_members)));




                list.Add(string.Format("MATERIAL CONSTANT"));
                list.Add(string.Format("E 1950000  {0}", MyList.Get_Array_Text(Cable_members)));
                list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Cable_members)));
                list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Cable_members)));

                //list.Add(string.Format("MATERIAL CONSTANTS "));
                //list.Add(string.Format("E 3150 ALL"));
                //list.Add(string.Format("DEN 0.000383 ALL"));



                //list.Add(string.Format("MATERIAL CONSTANTS "));
                //list.Add(string.Format("E 29000 ALL "));
                //list.Add(string.Format("DEN 0.000283 ALL"));
                //list.Add(string.Format("PR STEEL ALL"));


                //list.Add(string.Format("MATERIAL CONSTANTS"));
                //list.Add(string.Format("E 200 1 TO 47"));
                //list.Add(string.Format("DEN 0.078 1 TO 47"));


                //list.Add(string.Format("CONSTANTS"));
                //list.Add(string.Format("E 4200E3 ALL"));
                //list.Add(string.Format("CONSTANT"));
                //list.Add(string.Format("E 2.85E6 ALL"));
                //list.Add(string.Format("DENSITY CONCRETE ALL"));
                //list.Add(string.Format("POISSON CONCRETE ALL"));
                //list.Add(string.Format(""));

                #endregion Mat Constants
            }
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E {0}  {1}", E_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("E {0}  {1}", E_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("E {0}  {1}", E_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("E {0}  {1}", E_CABLE, MyList.Get_Array_Text(Cable_members)));

            list.Add(string.Format("DENSITY {0} {1}", DEN_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("DENSITY {0} {1}", DEN_CABLE, MyList.Get_Array_Text(Cable_members)));



            list.Add(string.Format("POISSON {0} {1}", PR_CONC, MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_CONC, MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_STEEL, MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("POISSON {0} {1}", PR_CABLE, MyList.Get_Array_Text(Cable_members)));


            list.Add("SUPPORT");
            //list.Add("11 TO 15  116 TO 120 276 TO 280 381 TO 385 FIXED");
            string ss = MyList.Get_Array_Text(Supports);
            //list.Add(ss + " FIXED");

            if (_Rows == 1)
            {
                list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[3]));
                list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2]));
            }
            else
            {
                //list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[4]));
                ////list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2]));
                ////list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[1], Supports[5]));
                ////list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[2], Supports[6]));


                //list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[5]));
                //list.Add(string.Format("{0} {1} PINNED", Supports[2], Supports[6]));
                //list.Add(string.Format("{0} {1} FIXED", Supports[3], Supports[7]));


                list.Add(string.Format("{0} {1} {2}", Supports[0], Supports[4], Start_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[1], Supports[5], Start_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[2], Supports[6], End_Support));
                list.Add(string.Format("{0} {1}  {2}", Supports[3], Supports[7], End_Support));

            }


            #region Chiranjit [2017 02 15]
            //foreach (var item in central_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            //foreach (var item in side_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            #endregion Chiranjit [2017 02 15]




            //list.Add(string.Format("LOAD 1 DL"));

            joint_loads = new List<string>();

            joint_loads.Add(string.Format("LOAD 1 DEADLOAD"));

             
            //if (false)
            if (true)
            {
                #region Joint Loads
                joint_loads.Add(string.Format("JOINT LOAD"));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_supports), (DLCSP + DLSSP)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_supports), (DLSSP)));


                //foreach (var item in central_supports)
                //{
                //    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLCSP + DLSSP))));
                //    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                //}

                //foreach (var item in side_supports)
                //{
                //    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSSP)));
                //    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                //}

                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_load_joints), (DLCJN)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_load_joints), (DLSJN)));

                central_load_joints.Sort();
                side_load_joints.Sort();
                foreach (var item in central_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN * (Cable_x_dist + Init_dist) * 2))));
                    //joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN + DLSJN)));

                }
                foreach (var item in side_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN * 2 * (Cable_x_dist + Init_dist)))));
                    //joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN + DLSJN)));
                }

                #endregion Joint Loads
            }



            #region Applied Load on Cable Member
            joint_loads.Add(string.Format("MEMBER LOAD"));
            ss = MyList.Get_Array_Text(Cable_members);

            joint_loads.Add(string.Format("{0} UNI GX {1:f3}", ss, DLSJN));

            #endregion Applied Load on Cable Member






            // Applied Member Load for All Center Load
            #region Member Load

            ////double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3)) / 3;
            double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3));

            #region Applied Load on Center Line
            //applied_load = ((2 * DLSS + DLCS) / (Cable_Nos * 2));
            applied_load = DLCSS;

            if (_Rows == 1)
            {
                joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                          Long_Girder_Members_Array[0, 0].MemberNo
                    //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                          , Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                    //, ((2*DLSS + DLCS)*10 / (L2))));
                          , applied_load));
            }
            else
            {
                if (IsCentral_Cable)
                {
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                        Long_Girder_Members_Array[1, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                        , Long_Girder_Members_Array[1, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                        , applied_load));
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                        Long_Girder_Members_Array[3, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                        , Long_Girder_Members_Array[3, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                        , applied_load));
                }
                else
                {
                    joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                        Long_Girder_Members_Array[2, 0].MemberNo
                        //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                        , Long_Girder_Members_Array[2, _Columns - 2].MemberNo
                        //, ((2*DLSS + DLCS)*10 / (L2))));
                        , applied_load));

                }
            }



            //, ((2*DLSS + DLCS) / (L2+L1+L3))*(0.6)));
            #endregion Applied Load on Center Line



            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("{0} UNI GY -0.001", MyList.Get_Array_Text(Long_Girder_members)));
            #endregion Member Load

            list.AddRange(joint_loads.ToArray());

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.AddRange(ll_txt_data.ToArray());

            list.Add("PERFORM ANALYSIS");
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            if (ll_txt_data != null)
            {
                string ll = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
                File.WriteAllLines(ll, ll_txt_data.ToArray());
            }
        }


        public void WriteData_LiveLoad_2017_02_16(string file_name, List<string> ll_txt_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA SPACE EXTRADOSSED BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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

            //list.Add("SECTION PROPERTIES");
            list.AddRange(Get_Continuous_LL_Member_Properties_Data());
            //list.Add(string.Format("MATERIAL CONSTANT"));
            //list.Add(string.Format("E 3162277 ALL"));
            //list.Add(string.Format("DENSITY CONCRETE ALL"));
            //list.Add(string.Format("POISSON CONCRETE ALL"));

            list.Add(string.Format("MEMBER CABLE"));
            list.Add(string.Format("_CABLES"));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("DENSITY CONCRETE {0}", MyList.Get_Array_Text(Long_Girder_members)));
            list.Add(string.Format("POISSON CONCRETE {0}", MyList.Get_Array_Text(Long_Girder_members)));



            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("DENSITY CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("POISSON CONCRETE {0}", MyList.Get_Array_Text(Cross_Girder_members)));




            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277  {0}", MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Tower_members)));




            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 1950000  {0}", MyList.Get_Array_Text(Cable_members)));
            list.Add(string.Format("DENSITY STEEL {0}", MyList.Get_Array_Text(Cable_members)));
            list.Add(string.Format("POISSON STEEL {0}", MyList.Get_Array_Text(Cable_members)));

            //list.Add(string.Format("MATERIAL CONSTANTS "));
            //list.Add(string.Format("E 3150 ALL"));
            //list.Add(string.Format("DEN 0.000383 ALL"));



            //list.Add(string.Format("MATERIAL CONSTANTS "));
            //list.Add(string.Format("E 29000 ALL "));
            //list.Add(string.Format("DEN 0.000283 ALL"));
            //list.Add(string.Format("PR STEEL ALL"));


            //list.Add(string.Format("MATERIAL CONSTANTS"));
            //list.Add(string.Format("E 200 1 TO 47"));
            //list.Add(string.Format("DEN 0.078 1 TO 47"));


            //list.Add(string.Format("CONSTANTS"));
            //list.Add(string.Format("E 4200E3 ALL"));
            //list.Add(string.Format("CONSTANT"));
            //list.Add(string.Format("E 2.85E6 ALL"));
            //list.Add(string.Format("DENSITY CONCRETE ALL"));
            //list.Add(string.Format("POISSON CONCRETE ALL"));
            //list.Add(string.Format(""));

            list.Add("SUPPORT");
            //list.Add("11 TO 15  116 TO 120 276 TO 280 381 TO 385 FIXED");
            string ss = MyList.Get_Array_Text(Supports);
            //list.Add(ss + " FIXED");

            list.Add(string.Format("{0} {1} FIXED", Supports[0], Supports[4]));
            //list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[2]));
            //list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[1], Supports[5]));
            //list.Add(string.Format("{0} {1} FIXED BUT FX MZ", Supports[2], Supports[6]));


            list.Add(string.Format("{0} {1} PINNED", Supports[1], Supports[5]));
            list.Add(string.Format("{0} {1} PINNED", Supports[2], Supports[6]));
            list.Add(string.Format("{0} {1} FIXED", Supports[3], Supports[7]));

            #region Chiranjit [2017 02 15]
            //foreach (var item in central_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            //foreach (var item in side_load_joints)
            //{
            //    //list.Add(string.Format("{0} PINNED", item));
            //    list.Add(string.Format("{0} FIXED BUT FX MZ", item));
            //}
            #endregion Chiranjit [2017 02 15]




            //list.Add(string.Format("LOAD 1 DL"));

            joint_loads = new List<string>();

            joint_loads.Add(string.Format("LOAD 1 DEADLOAD"));

            if (false)
            {
                #region Joint Loads
                joint_loads.Add(string.Format("JOINT LOAD"));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_supports), (DLCSP + DLSSP)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_supports), (DLSSP)));


                foreach (var item in central_supports)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLCSP + DLSSP))));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                }

                foreach (var item in side_supports)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSSP)));
                    joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));

                }

                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(central_load_joints), (DLCJN)));
                //joint_loads.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_load_joints), (DLSJN)));

                central_load_joints.Sort();
                side_load_joints.Sort();
                foreach (var item in central_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLCJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));

                }
                foreach (var item in side_load_joints)
                {
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, (DLSJN)));
                    //joint_loads.Add(string.Format("{0} FY  -{1:f3}", item, ((DLSJN))));
                    joint_loads.Add(string.Format("{0} FX  -{1:f3}", item, ((DLSJN))));
                }

                #endregion Joint Loads
            }
            joint_loads.Add(string.Format("MEMBER LOAD"));


            #region Applied Load on Cable Member
            ss = MyList.Get_Array_Text(Cable_members);

            joint_loads.Add(string.Format("{0} UNI GX {1:f3}", ss, DLSJN));

            #endregion Applied Load on Cable Member







            // Applied Member Load for All Center Load
            #region Member Load

            ////double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3)) / 3;
            double applied_load = ((2 * DLSS + DLCS) / (L2 + L1 + L3));

            #region Applied Load on Center Line
            //applied_load = ((2 * DLSS + DLCS) / (Cable_Nos * 2));
            applied_load = DLCSS;


            joint_loads.Add(string.Format("{0} TO {1} UNI GY  -{2:f3}",
                Long_Girder_Members_Array[2, 0].MemberNo
                //, Long_Girder_Members_Array[0, _Columns - 2].MemberNo
                , Long_Girder_Members_Array[2, _Columns - 2].MemberNo
                //, ((2*DLSS + DLCS)*10 / (L2))));
                , applied_load));
            //, ((2*DLSS + DLCS) / (L2+L1+L3))*(0.6)));
            #endregion Applied Load on Center Line



            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("{0} UNI GY -0.001", MyList.Get_Array_Text(Long_Girder_members)));
            #endregion Member Load

            list.AddRange(joint_loads.ToArray());

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");

            //list.AddRange(ll_txt_data.ToArray());

            list.Add("PERFORM ANALYSIS");
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            string ll = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
            File.WriteAllLines(ll, ll_txt_data.ToArray());
        }


       public double _cen_Ax = 0.0;
       public double _cen_Ix = 0.0;
       public double _cen_Iy = 0.0;
       public double _cen_Iz = 0.0;

       public double _inn_Ax = 0.0;
       public double _inn_Ix = 0.0;
       public double _inn_Iy = 0.0;
       public double _inn_Iz = 0.0;

       public double _out_Ax = 0.0;
       public double _out_Ix = 0.0;
       public double _out_Iy = 0.0;
       public double _out_Iz = 0.0;

        public List<string> Get_Continuous_LL_Member_Properties_Data()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("START GROUP DEFINITION"));




            //list.Add(string.Format("_LGIRDER " + MyList.Get_Array_Text(Long_Girder_members)));

            if (_Rows == 1)
            {
                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
            }
            else
            {
                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));



                list.Add(string.Format("_LGIRDER2 {0} TO {1} ", Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[1, _Columns - 2].MemberNo));


                if (IsCentral_Cable)
                {

                    list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                }
                else
                {
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));


                    //list.Add(string.Format("_LGIRDER3 {0}", MyList.Get_Array_Text(_End_mems)));
                    //list.Add(string.Format("_LSUPP {0}", MyList.Get_Array_Text(_supp_mems)));
                    //list.Add(string.Format("_LDEFF {0}", MyList.Get_Array_Text(_deff_mems)));
                    //list.Add(string.Format("_L8 {0}", MyList.Get_Array_Text(_L8_mems)));
                    //list.Add(string.Format("_L4 {0}", MyList.Get_Array_Text(_L4_mems)));
                    //list.Add(string.Format("_3L8 {0}", MyList.Get_Array_Text(_3L8_mems)));
                    //list.Add(string.Format("_L2 {0}", MyList.Get_Array_Text(_L2_mems)));

                    list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));


                }
                list.Add(string.Format("_LGIRDER4 {0} TO {1} ", Long_Girder_Members_Array[3, 0].MemberNo, Long_Girder_Members_Array[3, _Columns - 2].MemberNo));
                list.Add(string.Format("_LGIRDER5 {0} TO {1} ", Long_Girder_Members_Array[4, 0].MemberNo, Long_Girder_Members_Array[4, _Columns - 2].MemberNo));

                list.Add(string.Format("_XGIRDER " + MyList.Get_Array_Text(Cross_Girder_members)));
            }
            list.Add(string.Format("_TOWERS " + MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("_CABLES " + MyList.Get_Array_Text(Cable_members)));



            //list.Add(string.Format("_LGIRDER2 13 TO 37 62 TO 86 111 TO 135 160 TO 184 209 TO 233 258 TO 282 307 TO 331"));
            //list.Add(string.Format("_XGIRDER1 344 TO 415 572 TO 643"));
            //list.Add(string.Format("_XGIRDER2 416 TO 571"));
            //list.Add(string.Format("_PYLON1 644 TO 673"));
            //list.Add(string.Format("_PYLON2 674 TO 703"));
            //list.Add(string.Format("_CABLE1 704 TO 727"));
            //list.Add(string.Format("_CABLE2 728 TO 751"));
            //list.Add(string.Format("_CABLE3 752 TO 775"));
            //list.Add(string.Format("_CABLE4 776 TO 799"));
            //list.Add(string.Format("_TIEBEAM1 800"));
            //list.Add(string.Format("_TIEBEAM2 801"));
            list.Add(string.Format("END GROUP DEFINITION"));

            list.Add(string.Format("SECTION PROPERTIES"));


            List<string> mem_nos = new List<string>();
            List<int> lval = new List<int>();
            List<int> lval_dummy = new List<int>();
            List<string> mem_nos_dummy = new List<string>();

            //int nd = 0;
            //for (int i = 1; i < _Columns; i++)
            //{
            //    lval.Clear();
            //    //lval_dummy.Clear();
            //    for (int j = 0; j < _Rows; j++)
            //    {
            //        //nd = i + 78*j;
            //        if (j == ((_Rows / 2)))
            //            lval.Add(i + (_Columns - 1) * j);
            //        else
            //            lval_dummy.Add(i + (_Columns - 1) * j);
            //    }
            //    mem_nos.Add(MyList.Get_Array_Text(lval));
            //    //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            //}
            //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));

            //list.Add(mem_nos[nd++] + string.Format("      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("1 TO {0}      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011", MemColls.Count));
            //list.Add(string.Format("_LGIRDER1    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));

            if (PSC_SECIONS != null)
            {
                //list.Add(string.Format("_LGIRDER    PRI   AX  {0:f4}   IX   {0:f4}   IY {0:f4}   IZ   {0:f4}", PSC_SECIONS.Area, PSC_SECIONS.Ixx, PSC_SECIONS.Iyy, PSC_SECIONS.Izz));
                //list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));

                if (_Rows == 1)
                {
                    //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _cen_Ax, _cen_Ix, _cen_Iy, _cen_Iz));
                }
                else
                {

                    //list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    ////list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                    //if (IsCentral_Cable)
                    //{
                    //    //list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //    //list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //    //list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                    //    list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0] / 2, PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
                    //    list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
                    //    list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0] / 2, PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));

                    //}
                    //else
                    //{

                    //    list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //    list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //    list.Add(string.Format("_LSUPP    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //    list.Add(string.Format("_LDEFF    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[1], PSC_SECIONS.Ixx[1], PSC_SECIONS.Iyy[1], PSC_SECIONS.Iyy[1] + PSC_SECIONS.Ixx[1]));
                    //    list.Add(string.Format("_L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[2], PSC_SECIONS.Ixx[2], PSC_SECIONS.Iyy[2], PSC_SECIONS.Iyy[2] + PSC_SECIONS.Ixx[2]));
                    //    list.Add(string.Format("_L4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[3], PSC_SECIONS.Ixx[3], PSC_SECIONS.Iyy[3], PSC_SECIONS.Iyy[3] + PSC_SECIONS.Ixx[3]));
                    //    list.Add(string.Format("_3L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[4], PSC_SECIONS.Ixx[4], PSC_SECIONS.Iyy[4], PSC_SECIONS.Iyy[4] + PSC_SECIONS.Ixx[4]));
                    //    list.Add(string.Format("_L2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[5], PSC_SECIONS.Ixx[5], PSC_SECIONS.Iyy[5], PSC_SECIONS.Iyy[5] + PSC_SECIONS.Ixx[5]));
                    //    list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //}


                    //list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                }
                //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));
                list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _cen_Ax, _cen_Ix, _cen_Iy, _cen_Iz));
                list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _inn_Ax, _inn_Ix, _inn_Iy, _inn_Iz));
                list.Add(string.Format("_LGIRDER5    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", _out_Ax, _out_Ix, _out_Iy, _out_Iz));

            }
            else
            {
                list.Add(string.Format("_LGIRDER    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));

            }

            //list.Add(string.Format("_LGIRDER1    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            ////list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));
            ////list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));



            if (_Rows > 1)
            {
                list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            }

            //list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            list.Add(string.Format("_TOWERS     PRI   AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100"));
            list.Add(string.Format("_CABLES     PRI   AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005"));
            return list;
        }

        public List<string> Get_Continuous_LL_Member_Properties_Data_2019_01_24()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("START GROUP DEFINITION"));




            //list.Add(string.Format("_LGIRDER " + MyList.Get_Array_Text(Long_Girder_members)));

            if (_Rows == 1)
            {
                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
            }
            else
            {
                list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));



                list.Add(string.Format("_LGIRDER2 {0} TO {1} ", Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[1, _Columns - 2].MemberNo));


                if (IsCentral_Cable)
                {

                    list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                }
                else
                {
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
                    //list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));


                    list.Add(string.Format("_LGIRDER3 {0}", MyList.Get_Array_Text(_End_mems)));
                    list.Add(string.Format("_LSUPP {0}", MyList.Get_Array_Text(_supp_mems)));
                    list.Add(string.Format("_LDEFF {0}", MyList.Get_Array_Text(_deff_mems)));
                    list.Add(string.Format("_L8 {0}", MyList.Get_Array_Text(_L8_mems)));
                    list.Add(string.Format("_L4 {0}", MyList.Get_Array_Text(_L4_mems)));
                    list.Add(string.Format("_3L8 {0}", MyList.Get_Array_Text(_3L8_mems)));
                    list.Add(string.Format("_L2 {0}", MyList.Get_Array_Text(_L2_mems)));

                }
                list.Add(string.Format("_LGIRDER4 {0} TO {1} ", Long_Girder_Members_Array[3, 0].MemberNo, Long_Girder_Members_Array[3, _Columns - 2].MemberNo));
                list.Add(string.Format("_LGIRDER5 {0} TO {1} ", Long_Girder_Members_Array[4, 0].MemberNo, Long_Girder_Members_Array[4, _Columns - 2].MemberNo));

                list.Add(string.Format("_XGIRDER " + MyList.Get_Array_Text(Cross_Girder_members)));
            }
            list.Add(string.Format("_TOWERS " + MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("_CABLES " + MyList.Get_Array_Text(Cable_members)));



            //list.Add(string.Format("_LGIRDER2 13 TO 37 62 TO 86 111 TO 135 160 TO 184 209 TO 233 258 TO 282 307 TO 331"));
            //list.Add(string.Format("_XGIRDER1 344 TO 415 572 TO 643"));
            //list.Add(string.Format("_XGIRDER2 416 TO 571"));
            //list.Add(string.Format("_PYLON1 644 TO 673"));
            //list.Add(string.Format("_PYLON2 674 TO 703"));
            //list.Add(string.Format("_CABLE1 704 TO 727"));
            //list.Add(string.Format("_CABLE2 728 TO 751"));
            //list.Add(string.Format("_CABLE3 752 TO 775"));
            //list.Add(string.Format("_CABLE4 776 TO 799"));
            //list.Add(string.Format("_TIEBEAM1 800"));
            //list.Add(string.Format("_TIEBEAM2 801"));
            list.Add(string.Format("END GROUP DEFINITION"));

            list.Add(string.Format("SECTION PROPERTIES"));


            List<string> mem_nos = new List<string>();
            List<int> lval = new List<int>();
            List<int> lval_dummy = new List<int>();
            List<string> mem_nos_dummy = new List<string>();

            //int nd = 0;
            //for (int i = 1; i < _Columns; i++)
            //{
            //    lval.Clear();
            //    //lval_dummy.Clear();
            //    for (int j = 0; j < _Rows; j++)
            //    {
            //        //nd = i + 78*j;
            //        if (j == ((_Rows / 2)))
            //            lval.Add(i + (_Columns - 1) * j);
            //        else
            //            lval_dummy.Add(i + (_Columns - 1) * j);
            //    }
            //    mem_nos.Add(MyList.Get_Array_Text(lval));
            //    //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            //}
            //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));

            //list.Add(mem_nos[nd++] + string.Format("      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("1 TO {0}      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011", MemColls.Count));
            //list.Add(string.Format("_LGIRDER1    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));

            if (PSC_SECIONS != null)
            {
                //list.Add(string.Format("_LGIRDER    PRI   AX  {0:f4}   IX   {0:f4}   IY {0:f4}   IZ   {0:f4}", PSC_SECIONS.Area, PSC_SECIONS.Ixx, PSC_SECIONS.Iyy, PSC_SECIONS.Izz));
                //list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));

                if (_Rows == 1)
                {
                    list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                }
                else
                {

                    list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                    if (IsCentral_Cable)
                    {
                        //list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        //list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        //list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));


                        list.Add(string.Format("_LGIRDER2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0]/2, PSC_SECIONS.Ixx[0]/2, PSC_SECIONS.Iyy[0]/2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0])/2));
                        list.Add(string.Format("_LGIRDER3    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
                        list.Add(string.Format("_LGIRDER4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0] / 2, PSC_SECIONS.Ixx[0] / 2, PSC_SECIONS.Iyy[0] / 2, (PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]) / 2));
         
                    }
                    else
                    {

                        list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        list.Add(string.Format("_LSUPP    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                        list.Add(string.Format("_LDEFF    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[1], PSC_SECIONS.Ixx[1], PSC_SECIONS.Iyy[1], PSC_SECIONS.Iyy[1] + PSC_SECIONS.Ixx[1]));
                        list.Add(string.Format("_L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[2], PSC_SECIONS.Ixx[2], PSC_SECIONS.Iyy[2], PSC_SECIONS.Iyy[2] + PSC_SECIONS.Ixx[2]));
                        list.Add(string.Format("_L4    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[3], PSC_SECIONS.Ixx[3], PSC_SECIONS.Iyy[3], PSC_SECIONS.Iyy[3] + PSC_SECIONS.Ixx[3]));
                        list.Add(string.Format("_3L8    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[4], PSC_SECIONS.Ixx[4], PSC_SECIONS.Iyy[4], PSC_SECIONS.Iyy[4] + PSC_SECIONS.Ixx[4]));
                        list.Add(string.Format("_L2    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[5], PSC_SECIONS.Ixx[5], PSC_SECIONS.Iyy[5], PSC_SECIONS.Iyy[5] + PSC_SECIONS.Ixx[5]));
                        list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                    }


                    list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                }
                //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

            }
            else
            {
                list.Add(string.Format("_LGIRDER    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));

            }

            //list.Add(string.Format("_LGIRDER1    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            ////list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));
            ////list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));



            if (_Rows > 1)
            {
                list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            }

            //list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            list.Add(string.Format("_TOWERS     PRI   AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100"));
            list.Add(string.Format("_CABLES     PRI   AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005"));
            return list;
        }

        public List<string> Get_Continuous_LL_Member_Properties_Data_2017_02_16()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("START GROUP DEFINITION"));




            //list.Add(string.Format("_LGIRDER " + MyList.Get_Array_Text(Long_Girder_members)));

            list.Add(string.Format("_LGIRDER1 {0} TO {1}", Long_Girder_Members_Array[0, 0].MemberNo, Long_Girder_Members_Array[0, _Columns - 2].MemberNo));
            list.Add(string.Format("_LGIRDER2 {0} TO {1} ", Long_Girder_Members_Array[1, 0].MemberNo, Long_Girder_Members_Array[1, _Columns - 2].MemberNo));
            list.Add(string.Format("_LGIRDER3 {0} TO {1} ", Long_Girder_Members_Array[2, 0].MemberNo, Long_Girder_Members_Array[2, _Columns - 2].MemberNo));
            list.Add(string.Format("_LGIRDER4 {0} TO {1} ", Long_Girder_Members_Array[3, 0].MemberNo, Long_Girder_Members_Array[3, _Columns - 2].MemberNo));
            list.Add(string.Format("_LGIRDER5 {0} TO {1} ", Long_Girder_Members_Array[4, 0].MemberNo, Long_Girder_Members_Array[4, _Columns - 2].MemberNo));

            list.Add(string.Format("_XGIRDER " + MyList.Get_Array_Text(Cross_Girder_members)));
            list.Add(string.Format("_TOWERS " + MyList.Get_Array_Text(Tower_members)));
            list.Add(string.Format("_CABLES " + MyList.Get_Array_Text(Cable_members)));



            //list.Add(string.Format("_LGIRDER2 13 TO 37 62 TO 86 111 TO 135 160 TO 184 209 TO 233 258 TO 282 307 TO 331"));
            //list.Add(string.Format("_XGIRDER1 344 TO 415 572 TO 643"));
            //list.Add(string.Format("_XGIRDER2 416 TO 571"));
            //list.Add(string.Format("_PYLON1 644 TO 673"));
            //list.Add(string.Format("_PYLON2 674 TO 703"));
            //list.Add(string.Format("_CABLE1 704 TO 727"));
            //list.Add(string.Format("_CABLE2 728 TO 751"));
            //list.Add(string.Format("_CABLE3 752 TO 775"));
            //list.Add(string.Format("_CABLE4 776 TO 799"));
            //list.Add(string.Format("_TIEBEAM1 800"));
            //list.Add(string.Format("_TIEBEAM2 801"));
            list.Add(string.Format("END GROUP DEFINITION"));

            list.Add(string.Format("SECTION PROPERTIES"));


            List<string> mem_nos = new List<string>();
            List<int> lval = new List<int>();
            List<int> lval_dummy = new List<int>();
            List<string> mem_nos_dummy = new List<string>();

            //int nd = 0;
            //for (int i = 1; i < _Columns; i++)
            //{
            //    lval.Clear();
            //    //lval_dummy.Clear();
            //    for (int j = 0; j < _Rows; j++)
            //    {
            //        //nd = i + 78*j;
            //        if (j == ((_Rows / 2)))
            //            lval.Add(i + (_Columns - 1) * j);
            //        else
            //            lval_dummy.Add(i + (_Columns - 1) * j);
            //    }
            //    mem_nos.Add(MyList.Get_Array_Text(lval));
            //    //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            //}
            //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));

            //list.Add(mem_nos[nd++] + string.Format("      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("1 TO {0}      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011", MemColls.Count));
            //list.Add(string.Format("_LGIRDER1    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));

            if (PSC_SECIONS != null)
            {
                //list.Add(string.Format("_LGIRDER    PRI   AX  {0:f4}   IX   {0:f4}   IY {0:f4}   IZ   {0:f4}", PSC_SECIONS.Area, PSC_SECIONS.Ixx, PSC_SECIONS.Iyy, PSC_SECIONS.Izz));
                //list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));
                //list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}",  PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Izz[0]));


                list.Add(string.Format("_LGIRDER1    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                list.Add(string.Format("_LGIRDER2    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                list.Add(string.Format("_LGIRDER3    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                list.Add(string.Format("_LGIRDER4    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));
                list.Add(string.Format("_LGIRDER5    PRI   AX  0.0001   IX   {0:f4}   IY {1:f4}   IZ   {2:f4}", PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

                //list.Add(string.Format("_LGIRDER1    PRI   AX  {0:f4}   IX   {1:f4}   IY {2:f4}   IZ   {3:f4}", PSC_SECIONS.Area[0], PSC_SECIONS.Ixx[0], PSC_SECIONS.Iyy[0], PSC_SECIONS.Iyy[0] + PSC_SECIONS.Ixx[0]));

            }
            else
            {
                list.Add(string.Format("_LGIRDER    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));

            }

            //list.Add(string.Format("_LGIRDER1    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER2    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            ////list.Add(string.Format("_LGIRDER2    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER3    PRI   AX  7.0989   IX   6.0395   IY 35.8858   IZ   15.8871"));
            ////list.Add(string.Format("_LGIRDER4    PRI   AX 7.0989 IX 6.0395 IY 35.8858 IZ 15.8871"));
            //list.Add(string.Format("_LGIRDER4    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            //list.Add(string.Format("_LGIRDER5    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));



            list.Add(string.Format("_XGIRDER    PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));
            list.Add(string.Format("_TOWERS     PRI   AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100"));
            list.Add(string.Format("_CABLES     PRI   AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005"));
            return list;
        }


        public List<string> joint_loads = new List<string>();

        public int Cable_Nos { get; set; }
        public double Init_dist { get; set; }
        public double Cable_x_dist { get; set; }
        public double Cable_y_dist { get; set; }
        public double Cable_intv_dist { get; set; }
        public double Tower_height { get; set; }


        public double L1 { get; set; }

        public double L2 { get; set; }

        public double L3 { get; set; }

        public double Spacing_Cross_Girder { get; set; }

        public double Spacing_Long_Girder { get; set; }

        public double Skew_Angle { get; set; }


        public double cd { get; set; }
        public double Bt { get; set; }
        public double Dt { get; set; }
        public double tw { get; set; }


        public double DLSSP;
        public double DLCSP;

        public double DLSJN; //Side Span Joint Node Load
        public double DLCJN;//Centre Span Joint Node Load

        public double Cantilever_Width;

        public double Support_Distance;
        public double Effective_Distance;


        #region Distances
        public List<double> _Support_Dist { get; set; }
        public List<double> _Deff_Dist { get; set; }
        public List<double> _L8_Dist { get; set; }
        public List<double> _L4_Dist { get; set; }
        public List<double> _3L8_Dist { get; set; }
        public List<double> _L2_Dist { get; set; }

        #endregion Distances


        public double AVG_CS, Vol_CS, DLCS, DLCCS, NCCS, Vol_Side, DLSS, DLCSS, NCSS;

        List<string> Member_Loads = new List<string>();
        public List<string> Load_Calculation()
        {
            List<string> list = new List<string>();

            double _L1, _L2, _B1, _B2, _H1, _H2, _NCAB, _D1, _D2, _D3, _D4, _UNIT_WGT, _Percentage;

            _L1 = 100.0;
            _L2 = 65.0;
            _B1 = 9.75;
            _B2 = 1.925;
            _H1 = 12.0;
            _H2 = 4.4;
            _NCAB = 6.0;
            _D1 = 13.5;
            _D2 = 6.0;

            _L1 = L2;
            _L2 = L1;
            _B1 = WidthBridge;
            _B2 = Cantilever_Width;

            _NCAB = Cable_Nos;

            //_D1 = Ca;


            _H1 = Tower_height;


            _UNIT_WGT = 2.5;
            _Percentage = 40.0;
            //_Percentage = 100.0;


            list.Add(string.Format(" "));
            list.Add(string.Format(""));
            list.Add(string.Format("L1 = {0} metres", _L1));
            list.Add(string.Format("L2 = {0} metres", _L2));
            list.Add(string.Format("B1 = {0} metres", _B1));
            list.Add(string.Format("B2 = {0} metres", _B2));
            list.Add(string.Format("H1 = {0} metres", _H1));
            list.Add(string.Format("H2 = {0} metres", _H2));
            list.Add(string.Format("Number of Cable on Each Side = NCAB = {0}", _NCAB));
            list.Add(string.Format("D1 = {0} metres", _D1));
            list.Add(string.Format("D2 = {0} metres", _D2));



            _D3 = _L1 - 2 * _D1 - 2 * (_NCAB - 1) * _D2;
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            list.Add(string.Format("D3 = L1 - 2xD1 - 2x(NCAB-1)xD2 = {0} - 2x{1} - 2x{2}x{3} = {4} metres", _L1, _D1, (_NCAB - 1), _D2, _D3));

            _D4 = _L2 - _D1 - (_NCAB - 1) * _D2;
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            list.Add(string.Format("D4 = L2 - D1 - (NCAB-1)xD2 = {0} - {1} - {2}x{3} = {4} metres", _L2, _D1, (_NCAB - 1), _D2, _D4));



            list.Add(string.Format("Unit Weight of Box = {0} Tons/Cu.m", _UNIT_WGT));
            list.Add(string.Format("Percentage of Load in cables in central span = cp = {0}%", _Percentage));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CENTRAL SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //PSC_SECIONS

            AVG_CS = 0.0;
            foreach (var item in PSC_SECIONS.Area)
            {
                AVG_CS += item;
            }
            AVG_CS = AVG_CS / PSC_SECIONS.Area.Count;
            //AVG_CS = (7.099 + 6.008 + 5.6080 + 4.966 + 4.966 + 4.966) / 6;

            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));
            list.Add(string.Format("Average Cross Section Area = ({0:f3}+{1:f3}+{2:f3}+{3:f3}+{4:f3}+{5:f3})/6 = {6:f3}/6 = {7:f3} Sq. m.",

                PSC_SECIONS.Area[0],
                PSC_SECIONS.Area[1],
                PSC_SECIONS.Area[2],
                PSC_SECIONS.Area[3],
                PSC_SECIONS.Area[4],
                PSC_SECIONS.Area[5],
                AVG_CS * 6,
                AVG_CS
                ));

            list.Add(string.Format("Length of Central Span = {0} metres", _L1));

            Vol_CS = AVG_CS * _L1;

            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1} = {2:f3} Cu.m", AVG_CS, _L1, Vol_CS));

            DLCS = Vol_CS * _UNIT_WGT;
            list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = {0:f3} x {1} = {2:f3} Tons", Vol_CS, _UNIT_WGT, DLCS));
            list.Add(string.Format(""));

            double cp = _Percentage / 100;

            DLCCS = cp * DLCS;

            list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = {0:f3} x {1:f3} = {2:f3} Tons", cp, DLCS, DLCCS));

            NCCS = 4 * _NCAB;
            list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4 x {0} = {1}", _NCAB, NCCS));


            DLCJN = DLCCS / NCCS;

            list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = {0:f3} / {1} = {2:f3} Tons", DLCCS, NCCS, DLCJN));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            list.Add(string.Format(""));

            double sp = 1.0 - cp;
            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));

            DLCSP = sp * DLCS / 4.0;
            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = {0:f2} x {1:f3} / 4 = {2:f3} Tons", sp, DLCS, DLCSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SIDE SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Side Span = {0:f2} metres", _L2));

            Vol_Side = AVG_CS * _L2;
            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1:f2} = {2:f3} Cu.m", AVG_CS, _L2, Vol_Side));

            DLSS = Vol_Side * _UNIT_WGT;

            list.Add(string.Format("DEAD Load of Side Span = DLSS = Volume x Unit Weight = {0:f3} x {1:f2} = {2:f3} Tons", Vol_Side, _UNIT_WGT, DLSS));
            list.Add(string.Format(""));


            DLCSS = cp * DLSS;

            list.Add(string.Format("Dead Load on Cables in Side Span = DLCSS = cp x DLCS = {0:f2} x {1:f3} = {2:f3} Tons", cp, DLSS, DLCSS));

            NCSS = 2 * _NCAB;
            list.Add(string.Format("Number of Cable in Side Span = NCSS = 2 x NCAB = 2 x {0} = {1}", _NCAB, NCSS));
            list.Add(string.Format(""));

            DLSJN = DLCSS / NCSS;

            list.Add(string.Format("Dead Load on Each Cable Node in Side Span = DLCSS/NCSS = {0:f3}/{1} = {2:f3} Tons", DLCSS, NCSS, DLSJN));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));


            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));


            //DLSSP = sp * DLSS / 4.0;
            DLSSP = DLSS / 2.0;

            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = {0:f2} x {1:f2} / 4 = {2:f2} Tons", sp, DLSS, DLSSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));






            //DLCSS = AVG_CS * L2 * (1-cp);


            //DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);

            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (cp) / 10.0;


            //DLCSS = AVG_CS * _UNIT_WGT;

            //DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (1 + cp) / 10.0;
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (1 + 0.5) / 10.0;
            //DLSJN = AVG_CS * L2 * _UNIT_WGT * (cp) / _NCAB;
            //DLCSS = AVG_CS * _UNIT_WGT ;
            //DLSJN = AVG_CS * _UNIT_WGT * (cp) * this.Cable_x_dist;


            DLSJN = AVG_CS * _UNIT_WGT * (cp);
            DLCSS = AVG_CS * _UNIT_WGT * (1 - cp);

            #region Sample
            //list.Add(string.Format(" "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("L1 = 100 metres", _L1));
            //list.Add(string.Format("L2 = 65 metres", _L2));
            //list.Add(string.Format("B1 = 9.75 metres", _B1));
            //list.Add(string.Format("B2 = 1.925 metres", _B2));
            //list.Add(string.Format("H1 = 12.0 metres", _H1));
            //list.Add(string.Format("H2 = 4.4 metres", _H2));
            //list.Add(string.Format("Number of Cable on Each Side = NCAB = 6", _NCAB));
            //list.Add(string.Format("D1 = 13.5 metres", _D1));
            //list.Add(string.Format("D2 = 6.0 metres", _D2));
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            //list.Add(string.Format("Unit Weight of Box = 2.5 Tons/Cu.m", _UNIT_WGT));
            //list.Add(string.Format("Percentage of Load in cables in central span = cp = 40%", _Percentage));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CENTRAL SPAN"));
            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));

            //list.Add(string.Format("Length of Central Span = 100.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 100.0 = 560.2 Cu.m"));
            //list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = 560.2 x 2.5 = 1400.5 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 1400.5 = 560.2 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4x6 = 24"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 560.2/24 = 23.342 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = 0.6 x 1400.5 / 4 = 210.075 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("SIDE SPAN"));
            //list.Add(string.Format("Length of Side Span = 65.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 65.0 = 364.13 Cu.m"));
            //list.Add(string.Format("DEAD Load of Side Span = Volume x Unit Weight = DLSS = 364.13 x 2.5 = 910.325 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 910.325 = 364.13 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 2 x NCAB = 2x6 = 12"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 364.13/12 = 30.34 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = 0.6 x 910.325 / 4 = 136.55 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            #endregion
            return list;
        }


        public List<string> Load_Calculation_2017_02_14()
        {

            List<string> list = new List<string>();



            double _L1, _L2, _B1, _B2, _H1, _H2, _NCAB, _D1, _D2, _D3, _D4, _UNIT_WGT, _Percentage;


            _L1 = 100.0;
            _L2 = 65.0;
            _B1 = 9.75;
            _B2 = 1.925;
            _H1 = 12.0;
            _H2 = 4.4;
            _NCAB = 6.0;
            _D1 = 13.5;
            _D2 = 6.0;

            _L1 = L2;
            _L2 = L1;
            _B1 = WidthBridge;
            _B2 = Cantilever_Width;

            _NCAB = Cable_Nos;

            //_D1 = Ca;


            _H1 = Tower_height;


            _UNIT_WGT = 2.5;
            _Percentage = 40.0;


            list.Add(string.Format(" "));
            list.Add(string.Format(""));
            list.Add(string.Format("L1 = {0} metres", _L1));
            list.Add(string.Format("L2 = {0} metres", _L2));
            list.Add(string.Format("B1 = {0} metres", _B1));
            list.Add(string.Format("B2 = {0} metres", _B2));
            list.Add(string.Format("H1 = {0} metres", _H1));
            list.Add(string.Format("H2 = {0} metres", _H2));
            list.Add(string.Format("Number of Cable on Each Side = NCAB = {0}", _NCAB));
            list.Add(string.Format("D1 = {0} metres", _D1));
            list.Add(string.Format("D2 = {0} metres", _D2));



            _D3 = _L1 - 2 * _D1 - 2 * (_NCAB - 1) * _D2;
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            list.Add(string.Format("D3 = L1 - 2xD1 - 2x(NCAB-1)xD2 = {0} - 2x{1} - 2x{2}x{3} = {4} metres", _L1, _D1, (_NCAB - 1), _D2, _D3));

            _D4 = _L2 - _D1 - (_NCAB - 1) * _D2;
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            list.Add(string.Format("D4 = L2 - D1 - (NCAB-1)xD2 = {0} - {1} - {2}x{3} = {4} metres", _L2, _D1, (_NCAB - 1), _D2, _D4));



            list.Add(string.Format("Unit Weight of Box = {0} Tons/Cu.m", _UNIT_WGT));
            list.Add(string.Format("Percentage of Load in cables in central span = cp = {0}%", _Percentage));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CENTRAL SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //PSC_SECIONS

            AVG_CS = 0.0;
            foreach (var item in PSC_SECIONS.Area)
            {
                AVG_CS += item;
            }
            AVG_CS = AVG_CS / PSC_SECIONS.Area.Count;
            //AVG_CS = (7.099 + 6.008 + 5.6080 + 4.966 + 4.966 + 4.966) / 6;

            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));
            list.Add(string.Format("Average Cross Section Area = ({0:f3}+{1:f3}+{2:f3}+{3:f3}+{4:f3}+{5:f3})/6 = {6:f3}/6 = {7:f3} Sq. m.",

                PSC_SECIONS.Area[0],
                PSC_SECIONS.Area[1],
                PSC_SECIONS.Area[2],
                PSC_SECIONS.Area[3],
                PSC_SECIONS.Area[4],
                PSC_SECIONS.Area[5],
                AVG_CS * 6,
                AVG_CS
                ));

            list.Add(string.Format("Length of Central Span = {0} metres", _L1));

            Vol_CS = AVG_CS * _L1;

            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1} = {2:f3} Cu.m", AVG_CS, _L1, Vol_CS));

            DLCS = Vol_CS * _UNIT_WGT;
            list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = {0:f3} x {1} = {2:f3} Tons", Vol_CS, _UNIT_WGT, DLCS));
            list.Add(string.Format(""));

            double cp = _Percentage / 100;

            DLCCS = cp * DLCS;

            list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = {0:f3} x {1:f3} = {2:f3} Tons", cp, DLCS, DLCCS));

            NCCS = 4 * _NCAB;
            list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4 x {0} = {1}", _NCAB, NCCS));


            DLCJN = DLCCS / NCCS;

            list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = {0:f3} / {1} = {2:f3} Tons", DLCCS, NCCS, DLCJN));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            list.Add(string.Format(""));

            double sp = 1.0 - cp;
            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));

            DLCSP = sp * DLCS / 4.0;
            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = {0:f2} x {1:f3} / 4 = {2:f3} Tons", sp, DLCS, DLCSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SIDE SPAN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Side Span = {0:f2} metres", _L2));

            Vol_Side = AVG_CS * _L2;
            list.Add(string.Format("Volume of Central Span =  {0:f3} x {1:f2} = {2:f3} Cu.m", AVG_CS, _L2, Vol_Side));

            DLSS = Vol_Side * _UNIT_WGT;

            list.Add(string.Format("DEAD Load of Side Span = DLSS = Volume x Unit Weight = {0:f3} x {1:f2} = {2:f3} Tons", Vol_Side, _UNIT_WGT, DLSS));
            list.Add(string.Format(""));


            DLCSS = cp * DLSS;

            list.Add(string.Format("Dead Load on Cables in Side Span = DLCSS = cp x DLCS = {0:f2} x {1:f3} = {2:f3} Tons", cp, DLSS, DLCSS));

            NCSS = 2 * _NCAB;
            list.Add(string.Format("Number of Cable in Side Span = NCSS = 2 x NCAB = 2 x {0} = {1}", _NCAB, NCSS));
            list.Add(string.Format(""));

            DLSJN = DLCSS / NCSS;

            list.Add(string.Format("Dead Load on Each Cable Node in Side Span = DLCSS/NCSS = {0:f3}/{1} = {2:f3} Tons", DLCSS, NCSS, DLSJN));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));


            list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-{0:f2} = {1:f2}", cp, sp));


            DLSSP = sp * DLSS / 4.0;

            list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = {0:f2} x {1:f2} / 4 = {2:f2} Tons", sp, DLSS, DLSSP));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            #region Sample
            //list.Add(string.Format(" "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("L1 = 100 metres", _L1));
            //list.Add(string.Format("L2 = 65 metres", _L2));
            //list.Add(string.Format("B1 = 9.75 metres", _B1));
            //list.Add(string.Format("B2 = 1.925 metres", _B2));
            //list.Add(string.Format("H1 = 12.0 metres", _H1));
            //list.Add(string.Format("H2 = 4.4 metres", _H2));
            //list.Add(string.Format("Number of Cable on Each Side = NCAB = 6", _NCAB));
            //list.Add(string.Format("D1 = 13.5 metres", _D1));
            //list.Add(string.Format("D2 = 6.0 metres", _D2));
            //list.Add(string.Format("D3 = L1 - 2xD1 - 2x5xD2 = 100 - 2x13.5 - 2x5x6.0 = 100.0 - 87.0 = 13.0 metres", _D3));
            //list.Add(string.Format("D4 = L2 - D1 - 5xD2 = 100 - 13.5 - 5x6.0 = 100.0 - 43.5 = 56.5 metres", _D4));
            //list.Add(string.Format("Unit Weight of Box = 2.5 Tons/Cu.m", _UNIT_WGT));
            //list.Add(string.Format("Percentage of Load in cables in central span = cp = 40%", _Percentage));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CENTRAL SPAN"));
            //list.Add(string.Format("Average Cross Section Area = (7.099+6.008+5.6080+4.966+4.966+4.966)/6 = 33.613/6 = 5.602 Sq. m."));

            //list.Add(string.Format("Length of Central Span = 100.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 100.0 = 560.2 Cu.m"));
            //list.Add(string.Format("DEAD Load of Central Span = Volume x Unit Weight = DLCS = 560.2 x 2.5 = 1400.5 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 1400.5 = 560.2 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 4 x NCAB = 4x6 = 24"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 560.2/24 = 23.342 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports ( at each two nodes on Each Two Central Span Piers)"));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLCS / Four Supports = 0.6 x 1400.5 / 4 = 210.075 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("SIDE SPAN"));
            //list.Add(string.Format("Length of Side Span = 65.0 metres"));
            //list.Add(string.Format("Volume of Central Span =  5.602 x 65.0 = 364.13 Cu.m"));
            //list.Add(string.Format("DEAD Load of Side Span = Volume x Unit Weight = DLSS = 364.13 x 2.5 = 910.325 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Cables in Central Span = DLCCS = cp x DLCS = 0.40 x 910.325 = 364.13 Tons"));
            //list.Add(string.Format("Number of Cable in Central Span = NCCS = 2 x NCAB = 2x6 = 12"));
            //list.Add(string.Format("Dead Load on Each Cable Node in Central Span = DLCCS/NCCS = 364.13/12 = 30.34 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dead Load on Four supports (at each two nodes on Central Span and End Piers) "));
            //list.Add(string.Format("Dead Load Percentage on each of four supports = sp = 1.0-cp = 1.0-0.4 = 0.6"));
            //list.Add(string.Format("Dead Load on each of four support Nodes =  sp x DLSS / Four Supports = 0.6 x 910.325 / 4 = 136.55 Tons"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            #endregion
            return list;
        }

    }

}