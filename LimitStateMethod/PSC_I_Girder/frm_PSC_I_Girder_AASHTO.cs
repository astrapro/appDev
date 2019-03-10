using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.PSC_I_Girder;
using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.LS_Progress;
using LimitStateMethod.Composite;

using AstraAccess.SAP_Classes;
using AstraAccess.ADOC;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace LimitStateMethod.PSC_I_Girder
{
    public partial class frm_PSC_I_Girder_AASHTO : Form
    {
        //const string Title = "ANALYSIS OF PSC I-GIRDER BRIDGE (LIMIT STATE METHOD)";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC I-GIRDER BRIDGE LIMIT STATE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "PSC I-GIRDER BRIDGE LIMIT STATE [AASHTO - LRFD]";
                return "PSC I-GIRDER BRIDGE LIMIT STATE [IRC]";
            }
        }


        IApplication iApp;
        PSC_I_Girder_Analysis_AASHTO Bridge_Analysis = null;

        LS_DeckSlab_Analysis Deck_Analysis = null;

        PreStressedConcrete_SectionProperties sec_1 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_2 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_3 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_4 = new PreStressedConcrete_SectionProperties();
        PreStressedConcrete_SectionProperties sec_5 = new PreStressedConcrete_SectionProperties();


        AASHTO_PSC_I_Section LG_INNER_MID;
        AASHTO_PSC_I_Section LG_OUTER_MID;
        AASHTO_PSC_I_Section LG_INNER_SUP;
        AASHTO_PSC_I_Section LG_OUTER_SUP;


        AASHTO_Design_PC_I_Girder_CS CG_INTER;
        AASHTO_Design_PC_I_Girder_CS CG_END;

        public int DL_LL_Comb_Load_No
        {
            get
            {
                return MyList.StringToInt(txt_dl_ll_comb.Text, 1);
            }
        }

        bool IsCreateData = true;
        //bool IsInnerGirder
        public frm_PSC_I_Girder_AASHTO(IApplication thisApp)
        {
            InitializeComponent();
            iApp = thisApp;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            Result = new List<string>();

            LG_INNER_MID = new AASHTO_PSC_I_Section();
            LG_OUTER_MID = new AASHTO_PSC_I_Section();
            LG_INNER_SUP = new AASHTO_PSC_I_Section();
            LG_OUTER_SUP = new AASHTO_PSC_I_Section();
            CG_INTER = new AASHTO_Design_PC_I_Girder_CS();
            CG_END = new AASHTO_Design_PC_I_Girder_CS();
        }
        #region Define Properties Chiranjit [2013 09 23]
        //Define Properties
        public double L { get { return MyList.StringToDouble(txt_Ana_L.Text, 26.0); } set { txt_Ana_L.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }
        public double CW { get { return MyList.StringToDouble(txt_Ana_CW.Text, 0.0); } set { txt_Ana_CW.Text = value.ToString("f3"); } }
        public double CL { get { return MyList.StringToDouble(txt_Ana_CL.Text, 0.0); } set { txt_Ana_CL.Text = value.ToString("f3"); } }
        public double CR { get { return MyList.StringToDouble(txt_Ana_CR.Text, 0.0); } set { txt_Ana_CR.Text = value.ToString("f3"); } }
        public double Ds { get { return MyList.StringToDouble(txt_Ana_Ds.Text, 0.0); } set { txt_Ana_Ds.Text = value.ToString("f3"); } }
        public double Dso { get { return MyList.StringToDouble(txt_Ana_Dso.Text, 0.0); } set { txt_Ana_Dso.Text = value.ToString("f3"); } }
        public double Y_c { get { return MyList.StringToDouble(txt_Ana_concrete_weight.Text, 0.0); } set { txt_Ana_concrete_weight.Text = value.ToString("f3"); } }
        public double Y_c_wet { get { return MyList.StringToDouble(txt_Ana_Lc.Text, 0.0); } set { txt_Ana_Lc.Text = value.ToString("f3"); } }
        public double Ang { get { return MyList.StringToDouble(txt_Ana_ang.Text, 0.0); } set { txt_Ana_ang.Text = value.ToString("f3"); } }
        public double NMG { get { return MyList.StringToDouble(txt_Ana_NMG.Text, 0.0); } set { txt_Ana_NMG.Text = value.ToString("f3"); } }
        public double SMG { get { return MyList.StringToDouble(txt_Ana_SMG.Text, 0.0); } set { txt_Ana_SMG.Text = value.ToString("f3"); } }
        public double DMG { get { return MyList.StringToDouble(txt_Ana_DMG.Text, 0.0); } set { txt_Ana_DMG.Text = value.ToString("f3"); } }
        public double Deff { get { return (DMG - 0.0500 - 0.016 - 6 * 0.032); } }
        public double BMG { get { return MyList.StringToDouble(txt_sec_b5.Text, 0.0); } set { txt_sec_b5.Text = value.ToString("f3"); } }
        public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
        public double DCG { get { return MyList.StringToDouble(txt_sec_int_cg_d.Text, 0.0); } set { txt_sec_int_cg_d.Text = value.ToString("f3"); } }
        public double BCG { get { return MyList.StringToDouble(txt_sec_int_cg_bw.Text, 0.0); } set { txt_sec_int_cg_bw.Text = value.ToString("f3"); } }
        public double Dw { get { return MyList.StringToDouble(txt_Ana_Dw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
        //public double Y_w { get { return MyList.StringToDouble(txt_Ana_yw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
        //public double Y_w { get { return 22.0; } }

        public double Y_w { get { return MyList.StringToDouble(txt_Ana_FWS_Weight.Text, 0.0); } set { txt_Ana_FWS_Weight.Text = value.ToString("f3"); } }
        public double Hc { get { return MyList.StringToDouble(txt_Ana_Hc_LHS.Text, 0.0); } set { txt_Ana_Hc_LHS.Text = value.ToString("f3"); } }
        public double Wc { get { return MyList.StringToDouble(txt_Ana_Wc_LHS.Text, 0.0); } set { txt_Ana_Wc_LHS.Text = value.ToString("f3"); } }
        public double Wf { get { return MyList.StringToDouble(txt_Ana_Wf_LHS.Text, 0.0); } set { txt_Ana_Wf_LHS.Text = value.ToString("f3"); } }
        public double Hs { get { return MyList.StringToDouble(txt_Ana_Hf_LHS.Text, 0.0); } set { txt_Ana_Hf_LHS.Text = value.ToString("f3"); } }
        public double Wk { get { return MyList.StringToDouble(txt_Ana_Wk.Text, 0.0); } set { txt_Ana_Wk.Text = value.ToString("f3"); } }
        public double Wr { get { return MyList.StringToDouble(txt_Ana_Wr.Text, 0.0); } set { txt_Ana_Wr.Text = value.ToString("f3"); } }
        //public double swf { get { return MyList.StringToDouble(txt_Ana_swf.Text, 0.0); } set { txt_Ana_swf.Text = value.ToString("f3"); } }
        public double swf { get { return 1.0; } }

        public double Es { get { return MyList.StringToDouble(txt_Ana_Es.Text, 0.0); } set { txt_Ana_Es.Text = value.ToString("f3"); } }
        public double Ec { get { return MyList.StringToDouble(txt_Ana_Ec.Text, 0.0); } set { txt_Ana_Ec.Text = value.ToString("f3"); } }
        public double Ecm { get { return MyList.StringToDouble(txt_Ana_Ecm.Text, 0.0); } set { txt_Ana_Ecm.Text = value.ToString("f3"); } }

        #endregion Chiranjit [2012 06 20]

       
        //Chiranjit [2011 11 04] for Display Results
        public List<string> Result { get; set; }
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
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
        public string Worksheet_Folder
        {
            get
            {
                if (Path.GetFileName(user_path) == Project_Name)
                    if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
                //return user_path;
            }
        }
        public string Drawing_Folder
        {
            get
            {

                if (Path.GetFileName(user_path) == Project_Name)
                {
                    if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                }
                return Path.Combine(user_path, "DRAWINGS");
            }
        }
        public string Design_Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS"));
                return Path.Combine(Drawing_Folder, "DESIGN DRAWINGS");
            }
        }
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            //rft.M2 = chk_M2.Checked;
            //rft.M3 = chk_M3.Checked;
            //rft.R3 = chk_R3.Checked;
            //rft.R2 = chk_R2.Checked;
            rft.M2 = true;
            rft.M3 = true;
            rft.R3 = true;
            rft.R2 = true;
            return rft;
        }

        #region Form Events
        private void frm_RCC_T_Girder_LS_Load(object sender, EventArgs e)
        {
            //cmb_NMG.SelectedIndex = 1;
            cmb_NMG.SelectedIndex = 3;



            Bridge_Analysis = new PSC_I_Girder_Analysis_AASHTO(iApp);
            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Moving_Type_LoadData(dgv_long_loads);


            cmb_long_open_file_process.SelectedIndex = 0;



            #region Initialise default input data


            AASHTO_Design_PC_I_Girder.Input_Deck_Data(dgv_deck_input_data);
            AASHTO_Design_PC_I_Girder.Input_PSC_I_Girder_Data(dgv_psc_i_girder_input_data);
            AASHTO_Design_PC_I_Girder.Input_Abutment_Data(dgv_abutment_input_data);
            AASHTO_Design_PC_I_Girder.Input_Pier_Data(dgv_pier_input_data);
            AASHTO_Design_PC_I_Girder.Input_Bearing_Data(dgv_bearing_input_data);


            #endregion Initialise default input data



            Button_Enable_Disable();

            txt_Ana_B.Text = txt_Ana_B.Text + "";
            txt_LL_load_gen.Text = (L / 0.2).ToString("0");

            Text_Changed();
            Button_Enable_Disable();


            tabControl6.TabPages.Remove(tabPage8);
            tabControl6.TabPages.Remove(tabPage11);
            tabControl6.TabPages.Remove(tabPage9);
            tabControl6.TabPages.Remove(tabPage5);



            tab_sec_props.TabPages.Remove(tab_details);

            Set_Project_Name();


            chk_crash_barrier.Checked = true;
            chk_cb_left.Checked = true;
            chk_cb_right.Checked = false;
            chk_footpath.Checked = false;


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

        private void Open_Project()
        {

            //Chiranjit [2014 10 06]
            #region Chiranjit Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                IsCreateData = false;
                string chk_file = "";


                string usp = Path.Combine(user_path, "ANALYSIS PROCESS");
                if (!Directory.Exists(usp))
                {
                    Directory.CreateDirectory(usp);
                }

                chk_file = Path.Combine(usp, "INPUT_DATA.TXT");
                Bridge_Analysis.Input_File = chk_file;



                Ana_OpenAnalysisFile(chk_file);






                //Read_All_Data();


                #region Read Previous Record
                IsRead = true;
                iApp.Read_Form_Record(this, user_path);
                //txt_analysis_file.Text = chk_file;
                IsRead = false;

                #endregion

                //rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                Open_Create_Data();//Chiranjit [2013 06 25]

                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}

                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option
        }

        #region PSC I Girder Code
        void Calculate_Interactive_Values()
        {
            //return;
            //double eff_dp = MyList.StringToDouble(txt_Ana_eff_depth.Text, 0.0);
            double eff_dp = Deff;
            double lg_spa = SMG;
            double len = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double wd = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            double cant_wd = MyList.StringToDouble(txt_Ana_Ds.Text, 0.0);
            double long_bw = MyList.StringToDouble(txt_sec_b5.Text, 0.0);



            //Long Girder

            //Cross Girders

            int lng_no = (int)((wd - 2 * cant_wd) / lg_spa);
            int cg_no = (int)((len) / lg_spa);
            lng_no++;
            cg_no++;
             
        }


        double Get_Max_Vehicle_Length()
        {
            double mvl = 13.4;

            List<double> lst_mvl = new List<double>();
            DataGridView dgv = dgv_long_liveloads;

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
            train_length = veh_len;
            double eff = L;
            bool fl = false;
            while (train_length <= eff)
            {
                fl = !fl;
                if (fl)
                {
                    train_length += veh_gap;
                    if (train_length > L)
                    {
                        train_length = train_length - veh_gap;
                    }
                }
                else
                {
                    train_length += veh_len;
                }
            }
            return train_length;
        }

        void Text_Changed()
        {

            txt_Ana_NMG.Text = cmb_NMG.Text;
            SMG = (B - CL - CR) / (NMG - 1);

            txt_Ana_girder_spacing.Text = SMG.ToString("f3");


            double SCG = L / (NCG - 1);

            double Bb = MyList.StringToDouble(txt_sec_b4.Text, 0.65);
            double Db = MyList.StringToDouble(txt_sec_d3.Text, 0.65);

            txt_LL_load_gen.Text = ((L + Get_Max_Vehicle_Length()) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");


            if (chk_crash_barrier.Checked)
            {
                if (chk_cb_left.Checked && chk_cb_right.Checked)
                    CW = B - 2 * Wc;
                else if (!chk_cb_left.Checked || !chk_cb_right.Checked)
                    CW = B - Wc;
            }
            else if (chk_footpath.Checked)
            {
                if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else
                    CW = B - 2 * Wf;
            }

            Calculate_Interactive_Values();
            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();


            //chiranjit [2014 09 06] British Standard

            txt_sec_b_deck.Text = (SMG * 12).ToString("f3");
            txt_sec_d_deck.Text = txt_Ana_Ds.Text;


            txt_sec_b_deck_ext.Text = (((0.5 * SMG) + CL) * 12).ToString("f3");
            txt_sec_d_deck_ext.Text = txt_Ana_Ds.Text;
            //txt_sec_d4.Text = txt_Ana_DMG.Text;


            txt_sec_end_cg_d.Text = txt_Ana_DCG.Text;
            txt_sec_end_cg_Ds.Text = txt_Ana_Ds.Text;
            txt_sec_end_cg_D1.Text = (Dso - Ds).ToString("f2");

            txt_sec_int_cg_d.Text = txt_Ana_DCG.Text;
            txt_sec_int_cg_Ds.Text = txt_Ana_Ds.Text;


            Calculate_Section_Properties();



        }

        private void txt_sec_in_mid_lg_w_TextChanged(object sender, EventArgs e)
        {
            Calculate_Section_Properties();
            //leff
        }

        private void Calculate_Section_Properties()
        {

            double _bw = MyList.StringToDouble(txt_sec_end_cg_bw.Text, 0.0);
            double _lo = 0.7 * SMG;
            double _b1 = (_lo / 5.0) + _bw;
            double _b = 0.2 * _b1 + 0.1 * _lo;


            _b = Math.Min(_lo, _b1);

            //txt_sec_int_cg_w.Text = (0.7 * SMG).ToString("f3");
            //txt_sec_end_cg_w.Text = (_b).ToString("f3");

            //txt_Ana_DCG.Text = (LG_INNER_MID.d - LG_INNER_MID.D4).ToString("f3");



            LG_INNER_MID.b_deck = MyList.StringToDouble(txt_sec_b_deck.Text, 0.0);
            LG_INNER_MID.b1 = MyList.StringToDouble(txt_sec_b1.Text, 0.0);
            LG_INNER_MID.b2 = MyList.StringToDouble(txt_sec_b2.Text, 0.0);
            LG_INNER_MID.b3 = MyList.StringToDouble(txt_sec_b2.Text, 0.0);
            LG_INNER_MID.b4 = MyList.StringToDouble(txt_sec_b4.Text, 0.0);
            LG_INNER_MID.b5 = MyList.StringToDouble(txt_sec_b5.Text, 0.0);
            LG_INNER_MID.b6 = MyList.StringToDouble(txt_sec_b6.Text, 0.0);

            LG_INNER_MID.d_deck = MyList.StringToDouble(txt_sec_d_deck.Text, 0.0);
            LG_INNER_MID.d1 = MyList.StringToDouble(txt_sec_d1.Text, 0.0);
            LG_INNER_MID.d2 = MyList.StringToDouble(txt_sec_d2.Text, 0.0);
            LG_INNER_MID.d3 = MyList.StringToDouble(txt_sec_d3.Text, 0.0);
            LG_INNER_MID.d4 = MyList.StringToDouble(txt_sec_d4.Text, 0.0);
            LG_INNER_MID.d5 = MyList.StringToDouble(txt_sec_d5.Text, 0.0);
            LG_INNER_MID.d6 = MyList.StringToDouble(txt_sec_d6.Text, 0.0);



            LG_OUTER_MID.b_deck = MyList.StringToDouble(txt_sec_b_deck_ext.Text, 0.0);
            LG_OUTER_MID.b1 = MyList.StringToDouble(txt_sec_b1_ext.Text, 0.0);
            LG_OUTER_MID.b2 = MyList.StringToDouble(txt_sec_b2_ext.Text, 0.0);
            LG_OUTER_MID.b3 = MyList.StringToDouble(txt_sec_b2_ext.Text, 0.0);
            LG_OUTER_MID.b4 = MyList.StringToDouble(txt_sec_b4_ext.Text, 0.0);
            LG_OUTER_MID.b5 = MyList.StringToDouble(txt_sec_b5_ext.Text, 0.0);
            LG_OUTER_MID.b6 = MyList.StringToDouble(txt_sec_b6_ext.Text, 0.0);

            LG_OUTER_MID.d_deck = MyList.StringToDouble(txt_sec_d_deck_ext.Text, 0.0);
            LG_OUTER_MID.d1 = MyList.StringToDouble(txt_sec_d1_ext.Text, 0.0);
            LG_OUTER_MID.d2 = MyList.StringToDouble(txt_sec_d2_ext.Text, 0.0);
            LG_OUTER_MID.d3 = MyList.StringToDouble(txt_sec_d3_ext.Text, 0.0);
            LG_OUTER_MID.d4 = MyList.StringToDouble(txt_sec_d4_ext.Text, 0.0);
            LG_OUTER_MID.d5 = MyList.StringToDouble(txt_sec_d5_ext.Text, 0.0);
            LG_OUTER_MID.d6 = MyList.StringToDouble(txt_sec_d6_ext.Text, 0.0);

            #region cg_inter

            CG_INTER.W = MyList.StringToDouble(txt_sec_int_cg_w.Text, 0.0);
            CG_INTER.Ds = MyList.StringToDouble(txt_sec_int_cg_Ds.Text, 0.0);
            CG_INTER.bw = MyList.StringToDouble(txt_sec_int_cg_bw.Text, 0.0);
            CG_INTER.D1 = MyList.StringToDouble(txt_sec_int_cg_D1.Text, 0.0);
            CG_INTER.d = MyList.StringToDouble(txt_sec_int_cg_d.Text, 0.0);


            CG_INTER.Es_Deck = Ec;
            CG_INTER.Es_Girder = Ecm;

            #endregion cg_inter

            #region cg_end
            CG_END.W = MyList.StringToDouble(txt_sec_end_cg_w.Text, 0.0);
            CG_END.Ds = MyList.StringToDouble(txt_sec_end_cg_Ds.Text, 0.0);
            CG_END.bw = MyList.StringToDouble(txt_sec_end_cg_bw.Text, 0.0);
            CG_END.D1 = MyList.StringToDouble(txt_sec_end_cg_D1.Text, 0.0);
            CG_END.d = MyList.StringToDouble(txt_sec_end_cg_d.Text, 0.0);

            CG_END.Es_Deck = Ec;
            CG_END.Es_Girder = Ecm;

            #endregion cg_end

            #region Show Results
            txt_smp_i_a_mid.Text = LG_INNER_MID.Girder_Section_A.ToString("f4");
            txt_smp_i_ix_mid.Text = LG_INNER_MID.Girder_Section_Ix.ToString("f4");
            txt_smp_i_iz_mid.Text = LG_INNER_MID.Girder_Section_Iz.ToString("f4");
            txt_smp_i_a_sup.Text = LG_INNER_SUP.Girder_Section_A.ToString("f4");
            txt_smp_i_Ix_sup.Text = LG_INNER_SUP.Girder_Section_Ix.ToString("f4");
            txt_smp_i_Iz_sup.Text = LG_INNER_SUP.Girder_Section_Iz.ToString("f4");

            //txt_smp_i_a_mid.Text = lg_outer_mid.Girder_Section_A.ToString("f4");
            //txt_smp_i_a_sup.Text = lg_outer_mid.Girder_Section_A.ToString("f4");


            txt_smp_ii_a_mid.Text = LG_INNER_MID.Composite_Section_A.ToString("f4");
            txt_smp_ii_a_sup.Text = LG_INNER_SUP.Composite_Section_A.ToString("f4");
            txt_smp_ii_ix_mid.Text = LG_INNER_MID.Composite_Section_Ix.ToString("f4");
            txt_smp_ii_ix_sup.Text = LG_INNER_SUP.Composite_Section_Ix.ToString("f4");
            txt_smp_ii_iz_mid.Text = LG_INNER_MID.Composite_Section_Iz.ToString("f4");
            txt_smp_ii_iz_sup.Text = LG_INNER_SUP.Composite_Section_Iz.ToString("f4");



            txt_smp_iii_a_mid.Text = LG_OUTER_MID.Composite_Section_A.ToString("f4");
            txt_smp_iii_a_sup.Text = LG_OUTER_SUP.Composite_Section_A.ToString("f4");
            txt_smp_iii_ix_mid.Text = LG_OUTER_MID.Composite_Section_Ix.ToString("f4");
            txt_smp_iii_ix_sup.Text = LG_OUTER_SUP.Composite_Section_Ix.ToString("f4");
            txt_smp_iii_iz_mid.Text = LG_OUTER_MID.Composite_Section_Iz.ToString("f4");
            txt_smp_iii_iz_sup.Text = LG_OUTER_SUP.Composite_Section_Iz.ToString("f4");




            txt_smp_iv_a_cg.Text = (CG_END.Composite_Section_A/144).ToString("f4");
            txt_smp_iv_ix_cg.Text = (CG_END.Composite_Section_Ix/Math.Pow(12,4)).ToString("f4");
            txt_smp_iv_iz_cg.Text = (CG_END.Composite_Section_Iz / Math.Pow(12, 4)).ToString("f4");


            txt_smp_v_a_cg.Text = (CG_INTER.Composite_Section_A / 144).ToString("f4");
            txt_smp_v_ix_cg.Text = (CG_INTER.Composite_Section_Ix/Math.Pow(12,4)).ToString("f4");
            txt_smp_v_iz_cg.Text = (CG_INTER.Composite_Section_Iz/Math.Pow(12,4)).ToString("f4");
            #endregion Show Results
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

        //Chiranjit [2012 09 21]

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;
                Write_All_Data(true);
                //MessageBox.Show("1");
                Analysis_Initialize_InputData(true);

                if (Path.GetFileName(user_path) != Project_Name) Create_Project();

                    //if (IsCreateData)
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                ////MessageBox.Show("2");

                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);
                string usp = Path.Combine(user_path, "ANALYSIS PROCESS");
                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);

                    LONG_GIRDER_LL_TXT();
             
                string inp_file = Path.Combine(usp, "INPUT_DATA.TXT");

                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;

                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");


                    Bridge_Analysis.Skew_Angle = Ang;
                    Bridge_Analysis.CreateData();


                    Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                        Bridge_Analysis.Inner_Girders_as_String,
                        Bridge_Analysis.joints_list_for_load);


                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);
                    Bridge_Analysis.WriteData_Total_Analysis(inp_file);

                    //txt_analysis_file.Text = Bridge_Analysis.Input_File;



                    Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, all_loads[0]);

                    Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);




                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, DL_LL_Comb_Load_No);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, DL_LL_Comb_Load_No);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, true, DL_LL_Comb_Load_No);
               


                #region Chiranjit [2014 10 22]
                cmb_long_open_file_process.Items.Clear();
                cmb_long_open_file_process.Items.Add("DEAD LOAD ANALYSIS");

                //cmb_long_open_file_process.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                //cmb_long_open_file_process.Items.Add(string.Format("TOTAL ANALYSIS"));
                cmb_long_open_file_process.Items.Add(string.Format("DL + LL COMBINE ANALYSIS"));
                cmb_long_open_file_process.Items.Add(string.Format("LIVE LOAD ANALYSIS"));

                for (int i = 0; i < all_loads.Count; i++)
                {
                    if (ll_comb.Count == all_loads.Count)
                    {
                        cmb_long_open_file_process.Items.Add("LIVE LOAD ANALYSIS " + (i + 1) + " (" + ll_comb[i] + ")");

                    }
                    else
                    {
                        cmb_long_open_file_process.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                    }

                }
                cmb_long_open_file_process.SelectedIndex = 0;

                cmb_long_open_file_analysis.Items.Clear();
                for (int i = 0; i < cmb_long_open_file_process.Items.Count; i++)
                {
                    cmb_long_open_file_analysis.Items.Add(cmb_long_open_file_process.Items[i].ToString());
                }

                cmb_long_open_file_analysis.SelectedIndex = 0;


                #endregion Chiranjit [2014 10 22]

                for (int i = 0; i < all_loads.Count; i++)
                {
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.GetAnalysis_Input_File(i + 3), all_loads[i]);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.GetAnalysis_Input_File(i + 3), true, false, i);
                }

                Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                Button_Enable_Disable();
                Write_All_Data(false);

                MessageBox.Show(this, "Input Data Files for various Analysis Processes are created within the folder  \"" + Project_Name + "\".",
                  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

               Save_Input_Data();
            }
            catch (ThreadAbortException ex1) { MessageBox.Show(ex1.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_Ana_process_analysis_Click1(object sender, EventArgs e)
        {
            #region Process
            //Chiranjit [2012 07 13]
            Write_All_Data(true);
            int i = 0;

            string flPath = Bridge_Analysis.Input_File;
            do
            {
                if (i == 0)
                    flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                else if (i == 1)
                {
                    MessageBox.Show(this, "PROCESS ANALYSIS FOR LIVE LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;
                }
                else if (i == 2)
                {
                    MessageBox.Show(this, "PROCESS ANALYSIS FOR DEAD LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
                }


                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                System.Diagnostics.Process prs = new System.Diagnostics.Process();

                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                if (prs.Start())
                    prs.WaitForExit();
                i++;
            }
            while (i < 3);

            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {

                iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Total Load Analysis Result");

                iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File");
                iApp.Progress_Works.Add("Set Structure Geometry for Dead Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Dead Load Analysis Result");


                iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File");
                iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Live Load Analysis Result");


                iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                //iApp.Progress_Works = new ProgressList(Work_List);

                Bridge_Analysis.Analysis = null;
                Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);
                

                string s1 = "";
                string s2 = "";
                try
                {
                    for (i = 0; i < Bridge_Analysis.Analysis.Supports.Count; i++)
                    {
                        if (i < Bridge_Analysis.Analysis.Supports.Count / 2)
                        {
                            if (i == Bridge_Analysis.Analysis.Supports.Count / 2 - 1)
                            {
                                s1 += Bridge_Analysis.Analysis.Supports[i].NodeNo;
                            }
                            else
                                s1 += Bridge_Analysis.Analysis.Supports[i].NodeNo + ",";
                        }
                        else
                        {
                            if (i == Bridge_Analysis.Analysis.Supports.Count - 1)
                            {
                                s2 += Bridge_Analysis.Analysis.Supports[i].NodeNo;
                            }
                            else
                                s2 += Bridge_Analysis.Analysis.Supports[i].NodeNo + ",";
                        }
                    }
                }
                catch (Exception ex) { }
                //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
                double BB = B;



                txt_node_displacement.Text = Bridge_Analysis.Analysis.Node_Displacements.Get_Max_Deflection().ToString();


                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                frm_ViewDesign_Forces_Load();
            }


            Button_Enable_Disable();


            Button_Enable_Disable();
            Write_All_Data(false);
            #endregion Process
            iApp.Save_Form_Record(this, user_path);


            iApp.Progress_Works.Clear();

        }

        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            try
            {
                if (!Check_Project_Folder()) return;

                #region Process
                int i = 0;
                Write_All_Data(true);
                Analysis_Initialize_InputData(false);

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = Bridge_Analysis.Input_File;
                i = 0;
                iApp.Progress_Works.Clear();
              
                pcol.Clear();

                if (iApp.DesignStandard == eDesignStandard.IndianStandard ||
                    iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    LONG_GIRDER_LL_TXT();
              

                for (i = 0; i < (all_loads.Count + 3); i++)
                {
                    flPath = Bridge_Analysis.GetAnalysis_Input_File(i);

                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + cmb_long_open_file_process.Items[i].ToString().ToUpper();


                    pcol.Add(pd);

                    if (File.Exists(flPath))
                    {
                        iApp.Progress_Works.Add("Reading Analysis Data from " + cmb_long_open_file_process.Items[i].ToString().ToUpper() + " (ANALYSIS_REP.TXT)");
                    }
                }
             
                if (btn == btn_Ana_process_analysis)
                {
                    if (!iApp.Show_and_Run_Process_List(pcol))
                    {
                        iApp.Progress_Works.Clear();
                        return;
                    }
                }
                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                Bridge_Analysis.All_Analysis.Clear();

                for (i = 0; i < pcol.Count; i++)
                {
                    flPath = MyList.Get_Analysis_Report_File(pcol[i].Process_File_Name);
                    if (File.Exists(flPath))
                    {
                        Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, flPath);
                        Bridge_Analysis.All_Analysis.Add(Bridge_Analysis.Analysis);
                        if (iApp.Is_Progress_Cancel)
                        {
                            iApp.Progress_Works.Clear();
                            iApp.Progress_OFF();
                            return;
                        }
                    }
                }

                if (!iApp.Is_Progress_Cancel)
                {
                    Show_Long_Girder_Moment_Shear();
                }
                #region Abutment & Pier
                string s1 = "";
                string s2 = "";
                try
                {
                    for (i = 0; i < Bridge_Analysis.All_Analysis[1].Supports.Count; i++)
                    {
                        if (i < Bridge_Analysis.All_Analysis[1].Supports.Count / 2)
                        {
                            if (i == Bridge_Analysis.All_Analysis[1].Supports.Count / 2 - 1)
                            {
                                s1 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo;
                            }
                            else
                                s1 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo + ",";
                        }
                        else
                        {
                            if (i == Bridge_Analysis.All_Analysis[1].Supports.Count - 1)
                            {
                                s2 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo;
                            }
                            else
                                s2 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo + ", ";
                        }
                    }
                }
                catch (Exception ex) { }
                double BB = MyList.StringToDouble(txt_Ana_B.Text, 8.5);



                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                frm_ViewDesign_Forces_Load();

                Save_Support_Reactions();

                List<string> ls = new List<string>();


                if (!File.Exists(FILE_BASIC_INPUT_DATA)) Save_Input_Data();
                ls.AddRange(File.ReadAllLines(FILE_BASIC_INPUT_DATA));
                ls.AddRange(rtb_calc_load.Lines);
                ls.AddRange(File.ReadAllLines(File_Long_Girder_Results));
                ls.AddRange(File.ReadAllLines(FILE_SUPPORT_REACTIONS));
                File.WriteAllLines(FILE_SUMMARY_RESULTS, ls.ToArray());
                iApp.RunExe(FILE_SUMMARY_RESULTS);


                //Chiranjit [2012 06 22]
                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_final_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_final_Mz_kN.Text;


                #endregion Abutment & Pier


                _file_Check :
                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;



                Button_Enable_Disable();
                Write_All_Data(false);

                iApp.Progress_Works.Clear();

                #endregion Process
            }
            catch (Exception ex) { }
        }
        void Analysis_Initialize_InputData(bool initialize)
        {
            if (initialize)
                Bridge_Analysis = new PSC_I_Girder_Analysis_AASHTO(iApp);





            Bridge_Analysis.Long_Inner_Mid_Section = LG_INNER_MID;
            Bridge_Analysis.Long_Outer_Mid_Section = LG_OUTER_MID;

            Bridge_Analysis.Long_Inner_Support_Section = LG_INNER_SUP;
            Bridge_Analysis.Long_Outer_Support_Section = LG_OUTER_SUP;

            Bridge_Analysis.Cross_End_Section = CG_END;
            Bridge_Analysis.Cross_Intermediate_Section = CG_INTER;

            Bridge_Analysis.Length = L;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Ang;
            //Bridge_Analysis.Effective_Depth = Deff;


            Bridge_Analysis.Support_Distance = L * (1.6 / 26.52); // 1.6
            Bridge_Analysis.Effective_Depth = L * (5.0 / 26.52); // 1.6


            //Bridge_Analysis.Support_Distance = og; // 1.6
            Bridge_Analysis.NMG = NMG;

            Bridge_Analysis.NCG = NCG;





            Bridge_Analysis.Ds = Ds;
            //Bridge_Analysis.Leff = leff;

            Bridge_Analysis.Wc = Wc;
            Bridge_Analysis.Es = Es;
            Bridge_Analysis.Ec = Ec;
            Bridge_Analysis.Ecm = Ecm;

            if (chk_footpath.Checked)
            {
                if (chk_fp_left.Checked && !chk_fp_right.Checked)
                {
                    Bridge_Analysis.Wf_left = Wf;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = 0.0;
                    Bridge_Analysis.Wk_right = Wk;
                }
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                {
                    Bridge_Analysis.Wf_left = 0.0;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = Wf;
                    Bridge_Analysis.Wk_right = Wk;
                }
                else
                {
                    Bridge_Analysis.Wf_left = Wf;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = Wf;
                    Bridge_Analysis.Wk_right = Wk;
                }
                Bridge_Analysis.Wr = Wr;
            }
            Bridge_Analysis.Start_Support = Start_Support_Text;
            Bridge_Analysis.End_Support = END_Support_Text;

        }
       
      
        #endregion PSC I Girder Code

        private void btn_WorkSheet_Design_Click(object sender, EventArgs e)
        {
            string excel_file_name = "";
            string copy_path = "";
            Button btn = sender as Button;

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
        }
        #endregion Form Events

        #region Bridge Deck Analysis Form Events
        
        public void Open_Create_Data()
        {

            try
            {
                Ana_Initialize_Analysis_InputData();
            }
            catch (Exception ex) { }
        }
      
        
        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            Button_Enable_Disable();
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
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt";
                    ofd.InitialDirectory = Analysis_Path;
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        IsCreateData = false;
                        string chk_file = "";
                        user_path = Path.GetDirectoryName(ofd.FileName);

                        string usp = Path.Combine(user_path, "PSC Girder Analysis");
                        if (Directory.Exists(usp))
                        {
                            chk_file = Path.Combine(usp, "INPUT_DATA.TXT");
                            Bridge_Analysis.Input_File = chk_file;
                        }

                        Ana_OpenAnalysisFile(chk_file);
                        Read_All_Data();



                        #region Read Previous Record
                        IsRead = true;
                        iApp.Read_Form_Record(this, user_path);
                        //txt_analysis_file.Text = chk_file;
                        IsRead = false;

                        #endregion

                        //rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                        Open_Create_Data();//Chiranjit [2013 06 25]

                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");


            }
        }

        private void txt_Ana_length_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txt_Ana_X.Text = "-" + txt_Ana_L.Text; //Chiranjit [2013 05 29]
                Text_Changed();
                //L
            }
            catch (Exception ex) { }
        }
        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            //grb_SIDL.Enabled = chk_ana_active_SIDL.Checked;
        }
        #endregion Bridge Deck Analysis Form Events

        #region Bridge Deck Analysis Methods

        void Ana_Initialize_Analysis_InputData()
        {
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

            if (add_DeadLoad)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (add_LiveLoad)
                {
                    load_lst.Insert(0, "LOAD 1 DL + SIDL");
                    for (i = 1; i < load_lst.Count; i++)
                    {
                        if (load_lst[i].StartsWith("LOAD"))
                            load_lst[i] = "*" + load_lst[i];
                    }
                }

            }
            else
            {
                //if (chk_self_indian.Checked)
                //    load_lst.Add("SELFWEIGHT Y -1");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad && all_loads.Count > 0)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                load_lst.AddRange(all_loads[load_no].ToArray());
                if (long_ll.Count > 0)
                    File.WriteAllLines(MyList.Get_LL_TXT_File(file_name), long_ll.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Show_Long_Girder_Moment_Shear()
        {
            if (Bridge_Analysis.MemColls == null)
                Bridge_Analysis.CreateData(true);
            MemberCollection mc = new MemberCollection(Bridge_Analysis.MemColls);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Bridge_Analysis.Joints;

            double L = Bridge_Analysis.Length;
            double W = Bridge_Analysis.WidthBridge;

            //Bridge_Analysis.Effective_Depth
            double val = L / 2;
            int i = 0;

            //List<int> _support_inn_joints = new List<int>();
            //List<int> _deff_inn_joints = new List<int>();
            //List<int> _L8_inn_joints = new List<int>();
            //List<int> _L4_inn_joints = new List<int>();
            //List<int> _3L8_inn_joints = new List<int>();
            //List<int> _L2_inn_joints = new List<int>();



            //List<int> _support_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();
            //List<int> _L8_out_joints = new List<int>();
            //List<int> _L4_out_joints = new List<int>();
            //List<int> _3L8_out_joints = new List<int>();
            //List<int> _L2_out_joints = new List<int>();

            #region Inner Joints
            List<int> _L1_inn_joints = new List<int>();
            List<int> _L2_inn_joints = new List<int>();
            List<int> _L3_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _L5_inn_joints = new List<int>();
            List<int> _L6_inn_joints = new List<int>();
            List<int> _L7_inn_joints = new List<int>();
            List<int> _L8_inn_joints = new List<int>();
            List<int> _L9_inn_joints = new List<int>();
            List<int> _L10_inn_joints = new List<int>();

            List<int> _L11_inn_joints = new List<int>();
            List<int> _L12_inn_joints = new List<int>();
            List<int> _L13_inn_joints = new List<int>();
            List<int> _L14_inn_joints = new List<int>();
            List<int> _L15_inn_joints = new List<int>();
            List<int> _L16_inn_joints = new List<int>();
            List<int> _L17_inn_joints = new List<int>();
            List<int> _L18_inn_joints = new List<int>();
            List<int> _L19_inn_joints = new List<int>();
            List<int> _L20_inn_joints = new List<int>();

            List<int> _L21_inn_joints = new List<int>();
            List<int> _L22_inn_joints = new List<int>();
            List<int> _L23_inn_joints = new List<int>();
            List<int> _L24_inn_joints = new List<int>();
            List<int> _L25_inn_joints = new List<int>();
            #endregion Inner Joints

            #region Outer Joints
            List<int> _L1_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();
            List<int> _L3_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _L5_out_joints = new List<int>();
            List<int> _L6_out_joints = new List<int>();
            List<int> _L7_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L9_out_joints = new List<int>();
            List<int> _L10_out_joints = new List<int>();

            List<int> _L11_out_joints = new List<int>();
            List<int> _L12_out_joints = new List<int>();
            List<int> _L13_out_joints = new List<int>();
            List<int> _L14_out_joints = new List<int>();
            List<int> _L15_out_joints = new List<int>();
            List<int> _L16_out_joints = new List<int>();
            List<int> _L17_out_joints = new List<int>();
            List<int> _L18_out_joints = new List<int>();
            List<int> _L19_out_joints = new List<int>();
            List<int> _L20_out_joints = new List<int>();

            List<int> _L21_out_joints = new List<int>();
            List<int> _L22_out_joints = new List<int>();
            List<int> _L23_out_joints = new List<int>();
            List<int> _L24_out_joints = new List<int>();
            List<int> _L25_out_joints = new List<int>();
            #endregion Outer Joints


            //List<int> _L4_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();

            List<int> _cross_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Deff;

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
            if (_X_min.Count > 0)
            {
                _X_min.Sort();
                x_min = _X_min[0];
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Bridge_Analysis.Effective_Depth;
         

            double cant_wi_left = Bridge_Analysis.Width_LeftCantilever;
            double cant_wi_right = Bridge_Analysis.Width_RightCantilever;
            //Bridge_Analysis.Width_LeftCantilever = cant_wi;
            //Bridge_Analysis.Width_RightCantilever = _Z_joints[_Z_joints.Count - 1] - _Z_joints[_Z_joints.Count - 3];


            #region Find Joints
            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];


                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L1) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L1_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L2) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L3) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L3_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L4) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L5) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L5_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L6) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L6_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L7) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L7_inn_joints.Add(jn_col[i].NodeNo);
                    }

                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L8) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L9) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L9_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L10) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L10_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L11) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L11_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L12) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L12_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L13) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L13_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L14) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L14_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L15) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L15_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L16) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L17) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L17_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L18) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L18_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L19) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L19_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L20) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L20_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L21) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L21_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L22) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L22_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L23) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L23_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L24) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L24_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == ((Bridge_Analysis.L25) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L25_inn_joints.Add(jn_col[i].NodeNo);
                    }
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            #endregion Find Joints



            _L25_out_joints.Add(_L25_inn_joints[0]);
            _L25_inn_joints.RemoveAt(0);

            _L25_out_joints.Add(_L25_inn_joints[_L25_inn_joints.Count - 1]);
            _L25_inn_joints.RemoveAt(_L25_inn_joints.Count - 1);

            var _inner = _L24_inn_joints;
            var _outer = _L24_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);

            _inner = _L23_inn_joints;
            _outer = _L23_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L22_inn_joints;
            _outer = _L22_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);





            _inner = _L21_inn_joints;
            _outer = _L21_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L20_inn_joints;
            _outer = _L20_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);

            _inner = _L19_inn_joints;
            _outer = _L19_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L18_inn_joints;
            _outer = _L18_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);






            _inner = _L17_inn_joints;
            _outer = _L17_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);





            _inner = _L16_inn_joints;
            _outer = _L16_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L15_inn_joints;
            _outer = _L15_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L14_inn_joints;
            _outer = _L14_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L13_inn_joints;
            _outer = _L13_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L12_inn_joints;
            _outer = _L12_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L11_inn_joints;
            _outer = _L11_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L10_inn_joints;
            _outer = _L10_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);


            _inner = _L9_inn_joints;
            _outer = _L9_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);


            _inner = _L8_inn_joints;
            _outer = _L8_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);





            _inner = _L7_inn_joints;
            _outer = _L7_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);





            _inner = _L6_inn_joints;
            _outer = _L6_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L5_inn_joints;
            _outer = _L5_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L4_inn_joints;
            _outer = _L4_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);



            _inner = _L3_inn_joints;
            _outer = _L3_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);






            _inner = _L2_inn_joints;
            _outer = _L2_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);




            _inner = _L1_inn_joints;
            _outer = _L1_out_joints;

            _outer.Add(_inner[0]);
            _inner.RemoveAt(0);

            _outer.Add(_inner[_inner.Count - 1]);
            _inner.RemoveAt(_inner.Count - 1);
















            Result.Clear();
            Result.Add("");
            Result.Add("");
            //Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");


            BeamMemberForce f = null;


            List<int> tmp_jts = new List<int>();


            List<BeamMemberForce> ll_SF = new List<BeamMemberForce>();
            List<BeamMemberForce> ll_BM = new List<BeamMemberForce>();



            List<List<int>> All_inn_Joints = new List<List<int>>();

            All_inn_Joints.Add(_L1_inn_joints);
            All_inn_Joints.Add(_L2_inn_joints);
            All_inn_Joints.Add(_L3_inn_joints);
            All_inn_Joints.Add(_L4_inn_joints);
            All_inn_Joints.Add(_L5_inn_joints);
            All_inn_Joints.Add(_L6_inn_joints);
            All_inn_Joints.Add(_L7_inn_joints);
            All_inn_Joints.Add(_L8_inn_joints);
            All_inn_Joints.Add(_L9_inn_joints);
            All_inn_Joints.Add(_L10_inn_joints);


            All_inn_Joints.Add(_L11_inn_joints);
            All_inn_Joints.Add(_L12_inn_joints);
            All_inn_Joints.Add(_L13_inn_joints);
            All_inn_Joints.Add(_L14_inn_joints);
            All_inn_Joints.Add(_L15_inn_joints);
            All_inn_Joints.Add(_L16_inn_joints);
            All_inn_Joints.Add(_L17_inn_joints);
            All_inn_Joints.Add(_L18_inn_joints);
            All_inn_Joints.Add(_L19_inn_joints);
            All_inn_Joints.Add(_L20_inn_joints);


            All_inn_Joints.Add(_L21_inn_joints);
            All_inn_Joints.Add(_L22_inn_joints);
            All_inn_Joints.Add(_L23_inn_joints);
            All_inn_Joints.Add(_L24_inn_joints);
            All_inn_Joints.Add(_L25_inn_joints);


            List<List<int>> All_out_Joints = new List<List<int>>();

            All_out_Joints.Add(_L1_out_joints);
            All_out_Joints.Add(_L2_out_joints);
            All_out_Joints.Add(_L3_out_joints);
            All_out_Joints.Add(_L4_out_joints);
            All_out_Joints.Add(_L5_out_joints);
            All_out_Joints.Add(_L6_out_joints);
            All_out_Joints.Add(_L7_out_joints);
            All_out_Joints.Add(_L8_out_joints);
            All_out_Joints.Add(_L9_out_joints);
            All_out_Joints.Add(_L10_out_joints);

            All_out_Joints.Add(_L11_out_joints);
            All_out_Joints.Add(_L12_out_joints);
            All_out_Joints.Add(_L13_out_joints);
            All_out_Joints.Add(_L14_out_joints);
            All_out_Joints.Add(_L15_out_joints);
            All_out_Joints.Add(_L16_out_joints);
            All_out_Joints.Add(_L17_out_joints);
            All_out_Joints.Add(_L18_out_joints);
            All_out_Joints.Add(_L19_out_joints);
            All_out_Joints.Add(_L20_out_joints);

            All_out_Joints.Add(_L21_out_joints);
            All_out_Joints.Add(_L22_out_joints);
            All_out_Joints.Add(_L23_out_joints);
            All_out_Joints.Add(_L24_out_joints);
            All_out_Joints.Add(_L25_out_joints);

            int j = 0;
            List<string> list_SF = new List<string>();
            List<string> list_BM = new List<string>();
            List<string> list = new List<string>();

            MyList ml = null;

            List<string> ll_type = new List<string>();

            for (i = 0; i < long_ll_types.Count; i++)
            {
                ml = new MyList(long_ll_types[i], ' ');
                ll_type.Add(ml[2]);
            }

            List<double> _Location = new List<double>();
            List<double> _Girder1 = new List<double>();
            List<double> _Girder2 = new List<double>();
            List<double> _Slab = new List<double>();
            List<double> _Diaphragm = new List<double>();
            List<double> _Parapet = new List<double>();
            List<double> _FWS = new List<double>();
            List<double> _Positive_LL = new List<double>();
            List<double> _Negative_LL = new List<double>();

            _Location.Add(Bridge_Analysis.L1);
            _Location.Add(Bridge_Analysis.L2);
            _Location.Add(Bridge_Analysis.L3);
            _Location.Add(Bridge_Analysis.L4);
            _Location.Add(Bridge_Analysis.L5);
            _Location.Add(Bridge_Analysis.L6);
            _Location.Add(Bridge_Analysis.L7);
            _Location.Add(Bridge_Analysis.L8);
            _Location.Add(Bridge_Analysis.L9);
            _Location.Add(Bridge_Analysis.L10);


            _Location.Add(Bridge_Analysis.L11);
            _Location.Add(Bridge_Analysis.L12);
            _Location.Add(Bridge_Analysis.L13);
            _Location.Add(Bridge_Analysis.L14);
            _Location.Add(Bridge_Analysis.L15);
            _Location.Add(Bridge_Analysis.L16);
            _Location.Add(Bridge_Analysis.L17);
            _Location.Add(Bridge_Analysis.L18);
            _Location.Add(Bridge_Analysis.L19);
            _Location.Add(Bridge_Analysis.L20);


            _Location.Add(Bridge_Analysis.L21);
            _Location.Add(Bridge_Analysis.L22);
            _Location.Add(Bridge_Analysis.L23);
            _Location.Add(Bridge_Analysis.L24);
            _Location.Add(Bridge_Analysis.L25);


            #region Table 5.3-1 - Summary of Unfactored Moments
            string _format = "{0,10:f3} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3}";

            for (j = 0; j < All_inn_Joints.Count; j++)
            {
                tmp_jts.Clear();
                tmp_jts.AddRange(All_inn_Joints[j].ToArray());
                //tmp_jts.AddRange(All_out_Joints[j].ToArray());

                var mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 1);
                _Girder1.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 2);
                _Girder2.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 3);
                _Slab.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 4);
                _Diaphragm.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 5);
                _Parapet.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 6);
                _FWS.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Positive_Moment(tmp_jts);
                _Positive_LL.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Negative_Moment(tmp_jts);
                _Negative_LL.Add(mf.Force);
            }

            Result.Add(string.Format(""));
            Result.Add(string.Format("Table 5.3-1 - Summary of Unfactored Moments"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Interior girder, Span 1 shown, Span 2 symmetric"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Location*    -------Girder------     Slab    Diaphragm    Total      Parapet      FWS    Positive    Negative"));
            Result.Add(string.Format("                **          ***      and    "));
            Result.Add(string.Format("                                    Haunch               Noncomp.                          HL-93      HL-93 "));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("  (ft)        (k-ft)      (k-ft)    (k-ft)     (k-ft)     (k-ft)      (k-ft)    (k-ft)     (k-ft)     (k-ft)"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));

            for (i = 0; i < _Location.Count; i++)
            {
                Result.Add(string.Format(_format
                    , _Location[i]
                    , _Girder1[i]
                    , _Girder2[i]
                    , _Slab[i]
                    , _Diaphragm[i]
                    , _Girder2[i] + _Slab[i] + _Diaphragm[i]
                    , _Parapet[i]
                    , _FWS[i]
                    , _Positive_LL[i]
                    , _Negative_LL[i]
                    ));

            }
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            #endregion Table 5.3-1 - Summary of Unfactored Moments

            #region Table 5.3-3 - Summary of Unfactored Shear


            _Girder1.Clear();
            _Girder2.Clear();
            _Slab.Clear();
            _Diaphragm.Clear();
            _Parapet.Clear();
            _FWS.Clear();
            _Positive_LL.Clear();
            _Negative_LL.Clear();


            for (j = 0; j < All_inn_Joints.Count; j++)
            {
                tmp_jts.Clear();
                tmp_jts.AddRange(All_inn_Joints[j].ToArray());
                //tmp_jts.AddRange(All_out_Joints[j].ToArray());

                var mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 1);
                _Girder1.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 2);
                _Girder2.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 3);
                _Slab.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 4);
                _Diaphragm.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 5);
                _Parapet.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 6);
                _FWS.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Positive_Shear(tmp_jts);
                _Positive_LL.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Negative_Shear(tmp_jts);
                _Negative_LL.Add(mf.Force);
            }

            _format = "{0,10:f3} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

         
            Result.Add(string.Format(""));
            Result.Add(string.Format("Table 5.3-3 - Summary of Unfactored Shear"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Interior girder, Span 1 shown, Span 2 symmetric"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Location*     Girder        Slab    Diaphragm    Total      Parapet     FWS    Positive    Negative"));
            Result.Add(string.Format("                            and                 Noncomp.                         HL-93      HL-93 "));
            Result.Add(string.Format("                          Haunch                        "));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("    (ft)        (k)        (k)        (k)       (k)         (k)        (k)        (k)        (k)"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            for (i = 0; i < _Location.Count; i++)
            {
                Result.Add(string.Format(_format
                    , _Location[i]
                    , _Girder1[i]
                    , _Slab[i]
                    , _Diaphragm[i]
                    , _Girder2[i] + _Slab[i] + _Diaphragm[i]
                    , _Parapet[i]
                    , _FWS[i]
                    , _Positive_LL[i]
                    , _Negative_LL[i]
                    ));

            }
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            #endregion Table 5.3-3 - Summary of Unfactored Shear



            #region Table 5.3-5 - Summary of Unfactored Moments
            _format = "{0,10:f3} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3}";

            for (j = 0; j < All_out_Joints.Count; j++)
            {
                tmp_jts.Clear();
                tmp_jts.AddRange(All_out_Joints[j].ToArray());
                //tmp_jts.AddRange(All_out_Joints[j].ToArray());

                var mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 1);
                _Girder1.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 2);
                _Girder2.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 3);
                _Slab.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 4);
                _Diaphragm.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 5);
                _Parapet.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(tmp_jts, 6);
                _FWS.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Positive_Moment(tmp_jts);
                _Positive_LL.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Negative_Moment(tmp_jts);
                _Negative_LL.Add(mf.Force);
            }

            Result.Add(string.Format(""));
            Result.Add(string.Format("Table 5.3-5 - Summary of Unfactored Moments"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Exterior girder, Span 1 shown, Span 2 symmetric"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Location*    -------Girder------     Slab    Diaphragm    Total      Parapet      FWS    Positive    Negative"));
            Result.Add(string.Format("                **          ***      and    "));
            Result.Add(string.Format("                                    Haunch               Noncomp.                          HL-93      HL-93 "));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("  (ft)        (k-ft)      (k-ft)    (k-ft)     (k-ft)     (k-ft)      (k-ft)    (k-ft)     (k-ft)     (k-ft)"));
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));

            for (i = 0; i < _Location.Count; i++)
            {
                Result.Add(string.Format(_format
                    , _Location[i]
                    , _Girder1[i]
                    , _Girder2[i]
                    , _Slab[i]
                    , _Diaphragm[i]
                    , _Girder2[i] + _Slab[i] + _Diaphragm[i]
                    , _Parapet[i]
                    , _FWS[i]
                    , _Positive_LL[i]
                    , _Negative_LL[i]
                    ));

            }
            Result.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            #endregion Table 5.3-1 - Summary of Unfactored Moments

            #region Table 5.3-7 - Summary of Unfactored Shear


            _Girder1.Clear();
            _Girder2.Clear();
            _Slab.Clear();
            _Diaphragm.Clear();
            _Parapet.Clear();
            _FWS.Clear();
            _Positive_LL.Clear();
            _Negative_LL.Clear();


            for (j = 0; j < All_inn_Joints.Count; j++)
            {
                tmp_jts.Clear();
                tmp_jts.AddRange(All_inn_Joints[j].ToArray());
                //tmp_jts.AddRange(All_out_Joints[j].ToArray());

                var mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 1);
                _Girder1.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 2);
                _Girder2.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 3);
                _Slab.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 4);
                _Diaphragm.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 5);
                _Parapet.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(tmp_jts, 6);
                _FWS.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Positive_Shear(tmp_jts);
                _Positive_LL.Add((mf.Force));

                mf = Bridge_Analysis.All_Analysis[1].GetJoint_Max_Negative_Shear(tmp_jts);
                _Negative_LL.Add(mf.Force);
            }

            _format = "{0,10:f3} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";


            Result.Add(string.Format(""));
            Result.Add(string.Format("Table 5.3-7 - Summary of Unfactored Shear"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Exterior girder, Span 1 shown, Span 2 symmetric"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("Location*     Girder        Slab    Diaphragm    Total      Parapet     FWS    Positive    Negative"));
            Result.Add(string.Format("                            and                 Noncomp.                         HL-93      HL-93 "));
            Result.Add(string.Format("                          Haunch                        "));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format("    (ft)        (k)        (k)        (k)       (k)         (k)        (k)        (k)        (k)"));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            for (i = 0; i < _Location.Count; i++)
            {
                Result.Add(string.Format(_format
                    , _Location[i]
                    , _Girder1[i]
                    , _Slab[i]
                    , _Diaphragm[i]
                    , _Girder2[i] + _Slab[i] + _Diaphragm[i]
                    , _Parapet[i]
                    , _FWS[i]
                    , _Positive_LL[i]
                    , _Negative_LL[i]
                    ));

            }
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------"));
            #endregion Table 5.3-7 - Summary of Unfactored Shear

            if (false)
            {

                #region Vertical Deflection


                List<int> outer_joints = new List<int>();
                List<int> outer_joints_right = new List<int>();


                outer_joints.Add(_L4_inn_joints[0]);
                outer_joints_right.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);

                outer_joints.Add(_L2_inn_joints[0]);
                outer_joints_right.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);


                outer_joints.Add(_L4_out_joints[0]);
                outer_joints_right.Add(_L4_out_joints[_L4_out_joints.Count - 1]);



                List<NodeResultData> lst_nrd = new List<NodeResultData>();

                //iApp.SetProgressValue(10, 100);


                for (i = 1; i < Bridge_Analysis.All_Analysis.Count; i++)
                {
                    lst_nrd.Add(Bridge_Analysis.All_Analysis[i].Node_Displacements.Get_Max_Deflection());
                }



                int max_indx = 0;
                double max_def, allow_def;


                max_def = 0;
                allow_def = (L / 800.0);

                for (i = 0; i < lst_nrd.Count; i++)
                {
                    if (max_def < Math.Abs(lst_nrd[i].Max_Translation))
                    {
                        max_def = Math.Abs(lst_nrd[i].Max_Translation);
                        max_indx = i;
                    }
                }



                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format("CHECK FOR LIVE LOAD DEFLECTION"));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
                Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(lst_nrd[max_indx].ToString()));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));


                Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
                Result.Add(string.Format(""));
                if (max_def > allow_def)
                    Result.Add(string.Format("MAXIMUM  VERTICAL DEFLECTION = {0:f6} M. > {1:f6} M. ", max_def, allow_def));
                else
                    Result.Add(string.Format("MAXIMUM  VERTICAL DEFLECTION = {0:f6} M. < {1:f6} M. ", max_def, allow_def));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format("CHECK FOR DEAD LOAD DEFLECTION LEFT SIDE OUTER GIRDER"));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
                Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
                Result.Add(string.Format(""));
                lst_nrd.Clear();
                for (i = 0; i < outer_joints.Count; i++)
                {
                    lst_nrd.Add(Bridge_Analysis.All_Analysis[0].Node_Displacements.Get_Node_Deflection(outer_joints[i]));
                    Result.Add(string.Format(lst_nrd[i].ToString()));
                }
                //iApp.SetProgressValue(70, 100);

                Result.Add(string.Format(""));
                Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
                Result.Add(string.Format(""));
                Result.Add(string.Format("MAXIMUM NODE DISPLACEMENTS FOR LEFT SIDE OUTER LONG GIRDER"));
                Result.Add(string.Format("----------------------------------------------------------"));
                Result.Add(string.Format(""));
                for (i = 0; i < lst_nrd.Count; i++)
                {
                    if (lst_nrd[i].Max_Translation < allow_def)
                        Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. < {2:f6} M.    OK.", lst_nrd[i].NodeNo, lst_nrd[i].Max_Translation, allow_def));
                    else
                        Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. > {2:f6} M.    NOT OK.", lst_nrd[i].NodeNo, Math.Abs(lst_nrd[i].Max_Translation), allow_def));
                }

                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format("CHECK FOR DEAD LOAD DEFLECTION RIGHT SIDE OUTER GIRDER"));
                Result.Add(string.Format("---------------------------------------------------------------------------------"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
                Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
                Result.Add(string.Format(""));
                lst_nrd.Clear();
                for (i = 0; i < outer_joints_right.Count; i++)
                {
                    lst_nrd.Add(Bridge_Analysis.All_Analysis[0].Node_Displacements.Get_Node_Deflection(outer_joints_right[i]));
                    Result.Add(string.Format(lst_nrd[i].ToString()));
                }

                Result.Add(string.Format(""));
                Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
                Result.Add(string.Format(""));
                Result.Add(string.Format("MAXIMUM NODE DISPLACEMENTS FOR RIGHT SIDE OUTER LONG GIRDER"));
                Result.Add(string.Format("------------------------------------------------------------"));
                Result.Add(string.Format(""));
                for (i = 0; i < lst_nrd.Count; i++)
                {
                    if (lst_nrd[i].Max_Translation < allow_def)
                        Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. < {2:f6} M.    OK.", lst_nrd[i].NodeNo, lst_nrd[i].Max_Translation, allow_def));
                    else
                        Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. > {2:f6} M.    NOT OK.", lst_nrd[i].NodeNo, Math.Abs(lst_nrd[i].Max_Translation), allow_def));
                }

                Result.Add(string.Format(""));
                Result.Add(string.Format(""));
                Result.Add(string.Format("The Deflection for Dead Load is to be controlled by providing Longitudinal"));
                Result.Add(string.Format("Camber along the length of the Main Girder between end to end supports."));

                #endregion Vertical Deflection
            }
            Result.Add(string.Format(""));
            rtb_live_load_results.Lines = Result.ToArray();

            File.WriteAllLines(File_Long_Girder_Results, Result.ToArray());

            Result.Add(string.Format(""));
        }

        public string File_Long_Girder_Results
        {
            get
            {
                return Path.Combine(user_path, "PROCESS\\LONG_GIRDER_ANALYSIS_RESULT.TXT");
            }
        }
        public string File_DeckSlab_Results
        {
            get
            {
                return Path.Combine(user_path, "PROCESS\\DECKSLAB_ANALYSIS_RESULT.TXT");
            }
        }

        public void Button_Enable_Disable()
        {
            btn_view_data.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_view_preprocess.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            //btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            //btn_process_analysis.Enabled = File.Exists(Long_Girder_Analysis.TotalAnalysis_Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(File_Long_Girder_Results);


        }
        public void Ana_OpenAnalysisFile(string file_name)
        {
            string analysis_file = "";
            analysis_file = file_name;



            string usp = Path.Combine(user_path, "Deck Slab Analysis");
            if (Directory.Exists(usp))
            {
                Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            }
            usp = Path.Combine(user_path, "ANALYSIS PROCESS");

            if (Directory.Exists(usp))
            {
                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            }


            if (File.Exists(analysis_file))
            {
                btn_view_preprocess.Enabled = true;
            }
            Button_Enable_Disable();
        }

        List<string> load_list_1 = new List<string>();
        List<string> load_list_2 = new List<string>();
        List<string> load_list_3 = new List<string>();
        List<string> load_list_4 = new List<string>();
        List<string> load_list_5 = new List<string>();
        List<string> load_list_6 = new List<string>();
        List<string> load_total_7 = new List<string>();


        List<string> Ana_Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Analysis.Analysis.Members);

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


            z_min = Bridge_Analysis.Joints.MinZ;
            double z_max = Bridge_Analysis.Joints.MaxZ;


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
        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
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
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    #region Long Girder
                    list.Clear();
                    list.Add(string.Format("LOAD 1,TYPE 3"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 2,TYPE 3"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 3,TYPE 3"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,5.9"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 4,TYPE 1"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 5,TYPE 1,TYPE 1,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 6,TYPE 1,TYPE 1,TYPE 1"));
                    list.Add(string.Format("X,0,0,0"));
                    list.Add(string.Format("Z,1.5,4.5,7.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 7,TYPE 1,TYPE 3,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 8,TYPE 3,TYPE 1,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 9,TYPE 3,TYPE 1,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 10,TYPE 1,TYPE 1,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 11,TYPE 2,TYPE 2,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 12,TYPE 2,TYPE 2,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,2.5,5.5,"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 13,TYPE 2"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,2.5"));
                    list.Add(string.Format(""));
                    #endregion
                }
                else
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



        #endregion Bridge Deck Analysis Methods

        #region Long Girder Form Events

        #endregion Long Girder

        #region Design of RCC Pier

      
        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
        }
        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Name == btn_dwg_rcc_abut.Name)
            {
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
            }
            else if (b.Name == btn_dwg_box_abut.Name)
            {
                //iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Box Type Abutment Drawings"), "BOX_ABUTMENT");
            }
            else if (b.Name == btn_dwg_pier.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
            }
        }

        #endregion Design of RCC Pier

        #region Chiranjit [2012 06 20]
        
        void Text_Changed_T_Girder()
        {

            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);

            txt_LL_load_gen.Text = ((L) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");

            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();
        }

     

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        //IApplication iApp = null;
        //double B = 0.0;
        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
            //B = abut_width;
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
            MyList mlist = new MyList(MyList.RemoveAllSpaces(Supports), ' ');

            double tot_dead_vert_reac = 0.0;
            double tot_live_vert_reac = 0.0;

            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = DL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_left_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));

                    tot_dead_vert_reac += Math.Abs(sr.Max_Reaction); ;
                }
                catch (Exception ex)
                {
                }

            }

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
            if (B != 0)
            {
                txt_dead_vert_reac_kN.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10)).ToString("f3");
                txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10) / B).ToString("f3");
                txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10) / B).ToString("f3");
            }
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
                //Show_and_Save_Data();
                //Show_and_Save_Data_Load_1_2_3();
                Text_Changed_Forces();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data()
        {
            return;
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



            double _vert_load, _mx, _mz;

            _vert_load = _mx = _mz = 0.0;
            int _jnt_no = 0;
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 1);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 1);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 1);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                //LOAD 4 DEAD LOAD SELF WEIGHT + DECK SLAB DRY CONCRETE
                var shr1 = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 4);
                var mx1 = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 4);
                var mz1 = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 4);

                //Add Load 1 + Load 4
                _vert_load += shr1.Force;
                _mx += mx1.Force;
                _mz += mz1.Force;

                //dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);
                dgv_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
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
                _jnt_no = mlist.GetInt(i);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 1);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 1);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 1);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;

                //LOAD 4 DEAD LOAD SELF WEIGHT + DECK SLAB DRY CONCRETE
                shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 4);
                mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 4);
                mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 4);

                //Add Load 1 + Load 4
                _vert_load += shr.Force;
                _mx += mx.Force;
                _mz += mz.Force;


                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_des_frc.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load);
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));

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

            txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            #region Chiranjit [2017 06 11]
            txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]



            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            //txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");





            #region Chiranjit [2017 06 11]
            txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]





            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_final_Mx_kN.Text + " kN-m");
            //txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            #region Chiranjit [2017 06 11]

            txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]



            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");





            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            dgv_sidl_left_des_frc.Rows.Clear();
            dgv_sidl_right_des_frc.Rows.Clear();

            mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 5);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 5);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 5);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;

                shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 6);
                mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 6);
                mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 6);


                _vert_load += shr.Force;
                _mx += mx;
                _mz += mz;

                //if (_vert_load < shr.Force)
                //{
                //    _vert_load = shr.Force;
                //}
                //if (_mx < mx)
                //{
                //    _mx = mx;
                //}
                //if (_mz < mz)
                //{
                //    _mz = mz;
                //}

                dgv_sidl_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            txt_sidl_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_sidl_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_sidl_left_total_Mz.Text = tot_left_Mz.ToString("0.000");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 5);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 5);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 5);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;

                shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 6);
                mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 6);
                mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 6);

                _vert_load += shr;
                _mx += mx;
                _mz += mz;

                //if (_vert_load < shr.Force)
                //{
                //    _vert_load = shr.Force;
                //}
                //if (_mx < mx)
                //{
                //    _mx = mx;
                //}
                //if (_mz < mz)
                //{
                //    _mz = mz;
                //}

                dgv_sidl_right_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load); ;
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
            }

            txt_sidl_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_sidl_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_sidl_right_total_Mz.Text = tot_right_Mz.ToString("0.000");




            txt_sidl_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_sidl_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            txt_sidl_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_sidl_final_Mx_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mx.Text, 0.0) * 10.0).ToString("f3");



            txt_sidl_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_sidl_final_Mz_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mz.Text, 0.0) * 10.0).ToString("f3");




            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        string FILE_SUPPORT_REACTIONS { get { return Path.Combine(user_path, "Process\\SUPPORT_REACTIONS.TXT"); } }
        string FILE_SUMMARY_RESULTS { get { return Path.Combine(user_path, "Process\\SUMMARY_RESULTS.TXT"); } }
        string FILE_BASIC_INPUT_DATA { get { return Path.Combine(user_path, "Process\\Analysis_User_Data.TXT"); } }

        void Save_Support_Reactions()
        {

            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";

            List<string> list = new List<string>();
            string separator = "-------------------------------------------------------------------";


            int i = 0;

            list.Add(separator);
            list.Add(string.Format("SUPPORT REACTIONS [DL]"));
            list.Add(separator);
            list.Add(string.Format(""));

            #region Dead Load [DL]
            #region  Left End Design Forces
            list.Add(string.Format(""));
            list.Add(string.Format("Left End Design Forces"));
            list.Add(separator);
            //list.Add(string.Format("dgv_left_des_frc"));
            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "  MX", "   MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);



            for (i = 0; i < dgv_left_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format, dgv_left_des_frc[0, i].Value
                    , dgv_left_des_frc[1, i].Value
                    , dgv_left_des_frc[2, i].Value
                    , dgv_left_des_frc[3, i].Value));
            }

            list.Add(separator);


            //list.Add(string.Format("txt_left_total_vert_reac"));
            //list.Add(string.Format("txt_left_total_Mx"));
            //list.Add(string.Format("txt_left_total_Mz"));

            list.Add(string.Format(format, "Total", txt_left_total_vert_reac.Text
                , txt_left_total_Mx.Text
                , txt_left_total_Mz.Text));

            list.Add(separator);

            //list.Add(string.Format("txt_left_max_total_vert_reac"));
            //list.Add(string.Format("txt_left_max_total_Mx"));
            list.Add(string.Format(""));


            list.Add(string.Format(format
                , "Maximum Forces"
                , txt_left_max_total_vert_reac.Text
                , txt_left_max_total_Mx.Text
                , txt_left_max_total_Mz.Text));

            list.Add(separator);


            #endregion  Left End Design Forces

            #region  Right End Design Forces

            list.Add("");
            list.Add(string.Format("Right End Design Forces"));
            list.Add(separator);

            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);

            //list.Add(string.Format("dgv_right_des_frc"));

            for (i = 0; i < dgv_right_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format, dgv_right_des_frc[0, i].Value
                    , dgv_right_des_frc[1, i].Value
                    , dgv_right_des_frc[2, i].Value
                    , dgv_right_des_frc[3, i].Value));
            }

            list.Add(separator);



            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_total_vert_reac"));
            //list.Add(string.Format("txt_right_total_Mx"));
            //list.Add(string.Format("txt_right_total_Mz"));


            list.Add(string.Format(format, "Total",
                txt_right_total_vert_reac.Text
                , txt_right_total_Mx.Text
                , txt_right_total_Mz.Text));

            list.Add(separator);


            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_max_total_vert_reac"));
            //list.Add(string.Format("txt_right_max_total_Mx"));
            //list.Add(string.Format("txt_right_max_total_Mz"));


            list.Add(string.Format(format, "Maximum Forces"
                , txt_right_max_total_vert_reac.Text
                , txt_right_max_total_Mx.Text
                , txt_right_max_total_Mz.Text));

            list.Add(separator);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Abutment (from One Side)"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_max_vert_reac.Text, txt_max_vert_reac_kN.Text));
            //list.Add(string.Format("txt_max_vert_reac"));
            //list.Add(string.Format("txt_max_vert_reac_kN"));

            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_max_Mx.Text, txt_max_Mx_kN.Text));
            //list.Add(string.Format("txt_max_Mx"));
            //list.Add(string.Format("txt_max_Mx_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_max_Mz.Text, txt_max_Mz_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_max_Mz"));
            //list.Add(string.Format("txt_max_Mz_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Pier (from Both Sides)"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_final_vert_reac.Text, txt_final_vert_rec_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_final_vert_reac"));
            //list.Add(string.Format("txt_final_vert_rec_kN"));
            list.Add(string.Format("Total Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_final_Mx.Text, txt_final_Mx_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_final_Mz.Text, txt_final_Mz_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            #endregion Right End Design Forces
            #endregion Dead Load [DL]


            list.Add(string.Format(""));
            list.Add(separator);
            list.Add(string.Format("SUPPORT REACTIONS  [SIDL]"));
            list.Add(separator);

            #region Super Imposed Dead Load [DL]

            #region  Left End Design Forces
            list.Add(string.Format(""));
            list.Add(string.Format("Left End Design Forces"));
            list.Add(separator);
            //list.Add(string.Format("dgv_left_des_frc"));
            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);




            for (i = 0; i < dgv_sidl_left_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format, dgv_sidl_left_des_frc[0, i].Value
                    , dgv_sidl_left_des_frc[1, i].Value
                    , dgv_sidl_left_des_frc[2, i].Value
                    , dgv_sidl_left_des_frc[3, i].Value));
            }

            list.Add(separator);


            //list.Add(string.Format("txt_left_total_vert_reac"));
            //list.Add(string.Format("txt_left_total_Mx"));
            //list.Add(string.Format("txt_left_total_Mz"));

            list.Add(string.Format(format, "Total"
                , txt_sidl_left_total_vert_reac.Text
                , txt_sidl_left_total_Mx.Text
                , txt_sidl_left_total_Mz.Text));

            list.Add(separator);

            //list.Add(string.Format("txt_left_max_total_vert_reac"));
            //list.Add(string.Format("txt_left_max_total_Mx"));
            //list.Add(string.Format("txt_left_max_total_Mz"));


            list.Add(string.Format(format, "Maximum Forces"
                , txt_sidl_left_max_total_vert_reac.Text
                , txt_sidl_left_max_total_Mx.Text
                , txt_sidl_left_max_total_Mz.Text));

            list.Add(separator);
            list.Add(string.Format(""));


            #endregion  Left End Design Forces

            #region  Right End Design Forces

            list.Add(string.Format("Right End Design Forces"));
            list.Add(separator);


            list.Add(string.Format(""));
            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);

            //list.Add(string.Format("dgv_right_des_frc"));

            for (i = 0; i < dgv_sidl_right_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format
                    , dgv_sidl_right_des_frc[0, i].Value
                    , dgv_sidl_right_des_frc[1, i].Value
                    , dgv_sidl_right_des_frc[2, i].Value
                    , dgv_sidl_right_des_frc[3, i].Value));
            }

            list.Add(separator);



            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_total_vert_reac"));
            //list.Add(string.Format("txt_right_total_Mx"));
            //list.Add(string.Format("txt_right_total_Mz"));


            list.Add(string.Format(format, "Total",
                txt_sidl_right_total_vert_reac.Text
                , txt_sidl_right_total_Mx.Text
                , txt_sidl_right_total_Mz.Text));

            list.Add(separator);


            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_max_total_vert_reac"));
            //list.Add(string.Format("txt_right_max_total_Mx"));
            //list.Add(string.Format("txt_right_max_total_Mz"));


            list.Add(string.Format(format
                , "Maximum Forces"
                , txt_sidl_right_max_total_vert_reac.Text
                , txt_sidl_right_max_total_Mx.Text
                , txt_sidl_right_max_total_Mz.Text));

            list.Add(separator);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Abutment (from One Side)"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_sidl_max_vert_reac.Text, txt_sidl_max_vert_reac_kN.Text));
            //list.Add(string.Format("txt_max_vert_reac"));
            //list.Add(string.Format("txt_max_vert_reac_kN"));

            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_sidl_max_Mx.Text, txt_sidl_max_Mx_kN.Text));
            //list.Add(string.Format("txt_max_Mx"));
            //list.Add(string.Format("txt_max_Mx_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_sidl_max_Mz.Text, txt_sidl_max_Mz_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_max_Mz"));
            //list.Add(string.Format("txt_max_Mz_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Pier (from Both Sides)"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_sidl_final_vert_reac.Text, txt_sidl_final_vert_rec_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_final_vert_reac"));
            //list.Add(string.Format("txt_final_vert_rec_kN"));
            list.Add(string.Format("Total Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_sidl_final_Mx.Text, txt_sidl_final_Mx_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_sidl_final_Mz.Text, txt_sidl_final_Mz_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion Right End Design Forces

            #endregion Super Imposed Dead Load [DL]


            list.Add(string.Format(""));
            list.Add(separator);
            list.Add(string.Format("SUPPORT REACTIONS  [LL]"));
            list.Add(separator);
            list.Add(string.Format(""));
            #region Live Load [LL]

            #region  Left End Design Forces
            list.Add(string.Format(""));
            list.Add(string.Format("Left End Design Forces"));
            list.Add(separator);
            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);




            for (i = 0; i < dgv_ll_left_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format
                    , dgv_ll_left_des_frc[0, i].Value
                    , dgv_ll_left_des_frc[1, i].Value
                    , dgv_ll_left_des_frc[2, i].Value
                    , dgv_ll_left_des_frc[3, i].Value));
            }

            list.Add(separator);


            //list.Add(string.Format("txt_left_total_vert_reac"));
            //list.Add(string.Format("txt_left_total_Mx"));
            //list.Add(string.Format("txt_left_total_Mz"));

            list.Add(string.Format(format, "Total"
                , txt_ll_left_total_vert_reac.Text
                , txt_ll_left_total_Mx.Text
                , txt_ll_left_total_Mz.Text));

            list.Add(separator);

            //list.Add(string.Format("txt_left_max_total_vert_reac"));
            //list.Add(string.Format("txt_left_max_total_Mx"));
            //list.Add(string.Format("txt_left_max_total_Mz"));


            list.Add(string.Format(format, "Maximum Forces"
                , txt_ll_left_max_vert_reac.Text
                , txt_ll_left_max_total_Mx.Text
                , txt_ll_left_max_total_Mz.Text));

            list.Add(separator);
            list.Add("");



            #endregion  Left End Design Forces

            #region  Right End Design Forces

            list.Add(string.Format("Right End Design Forces"));
            list.Add(separator);
            list.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);

            //list.Add(string.Format("dgv_right_des_frc"));

            for (i = 0; i < dgv_ll_right_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format
                    , dgv_ll_right_des_frc[0, i].Value
                    , dgv_ll_right_des_frc[1, i].Value
                    , dgv_ll_right_des_frc[2, i].Value
                    , dgv_ll_right_des_frc[3, i].Value));
            }

            list.Add(separator);



            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_total_vert_reac"));
            //list.Add(string.Format("txt_right_total_Mx"));
            //list.Add(string.Format("txt_right_total_Mz"));


            list.Add(string.Format(format, "Total"
                , txt_ll_right_total_vert_reac.Text
                , txt_ll_right_total_Mx.Text
                , txt_ll_right_total_Mz.Text));

            list.Add(separator);


            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_max_total_vert_reac"));
            //list.Add(string.Format("txt_right_max_total_Mx"));
            //list.Add(string.Format("txt_right_max_total_Mz"));


            list.Add(string.Format(format, "Maximum Forces"
                , txt_ll_right_max_total_vert_reac.Text
                , txt_ll_right_max_total_Mx.Text
                , txt_ll_right_max_total_Mz.Text));

            list.Add(separator);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Abutment (from One Side)"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_ll_max_vert_reac.Text, txt_ll_max_vert_reac_kN.Text));
            //list.Add(string.Format("txt_max_vert_reac"));
            //list.Add(string.Format("txt_max_vert_reac_kN"));

            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_ll_max_Mx.Text, txt_ll_max_Mx_kN.Text));
            //list.Add(string.Format("txt_max_Mx"));
            //list.Add(string.Format("txt_max_Mx_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_ll_max_Mz.Text, txt_ll_max_Mz_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_max_Mz"));
            //list.Add(string.Format("txt_max_Mz_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Pier (from Both Sides)"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_ll_final_vert_reac.Text, txt_ll_final_vert_rec_kN.Text));
            list.Add(string.Format(""));
            //list.Add(string.Format("txt_final_vert_reac"));
            //list.Add(string.Format("txt_final_vert_rec_kN"));
            list.Add(string.Format("Total Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_ll_final_Mx.Text, txt_ll_final_Mx_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_ll_final_Mz.Text, txt_ll_final_Mz_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Right End Design Forces

            #endregion Live Load [LL]


            list.Add(string.Format(""));
            list.Add(separator);
            list.Add(string.Format("MAXIMUM FORCES"));
            list.Add(separator);
            list.Add(string.Format(""));

            #region MAX [DL SIDL LL]

            #region  Left End Design Forces
            list.Add(string.Format(""));
            list.Add(string.Format("Left End Design Forces"));
            list.Add(separator);
            list.Add(string.Format(format, "LOAD", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NAME", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);




            for (i = 0; i < dgv_mxf_left_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format
                    , dgv_mxf_left_des_frc[0, i].Value
                    , dgv_mxf_left_des_frc[1, i].Value
                    , dgv_mxf_left_des_frc[2, i].Value
                    , dgv_mxf_left_des_frc[3, i].Value));
            }

            list.Add(separator);


            //list.Add(string.Format("txt_left_total_vert_reac"));
            //list.Add(string.Format("txt_left_total_Mx"));
            //list.Add(string.Format("txt_left_total_Mz"));

            list.Add(string.Format(format, "Total"
                , txt_mxf_left_total_vert_reac.Text
                , txt_mxf_left_total_Mx.Text
                , txt_mxf_left_total_Mz.Text));

            list.Add(separator);
            list.Add("");


            #endregion  Left End Design Forces

            #region  Right End Design Forces

            list.Add(string.Format("Right End Design Forces"));
            list.Add(separator);

            list.Add(string.Format(format, "LOAD", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list.Add(string.Format(format, "NAME", "REACTIONS", "MX", "MZ"));
            list.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list.Add(separator);

            //list.Add(string.Format("dgv_right_des_frc"));

            for (i = 0; i < dgv_mxf_right_des_frc.RowCount - 1; i++)
            {
                list.Add(string.Format(format
                    , dgv_mxf_right_des_frc[0, i].Value
                    , dgv_mxf_right_des_frc[1, i].Value
                    , dgv_mxf_right_des_frc[2, i].Value
                    , dgv_mxf_right_des_frc[3, i].Value));
            }

            list.Add(separator);



            //list.Add(string.Format(""));
            //list.Add(string.Format("txt_right_total_vert_reac"));
            //list.Add(string.Format("txt_right_total_Mx"));
            //list.Add(string.Format("txt_right_total_Mz"));


            list.Add(string.Format(format, "Total"
                , txt_mxf_right_total_vert_reac.Text
                , txt_mxf_right_total_Mx.Text
                , txt_mxf_right_total_Mz.Text));

            list.Add(separator);


            list.Add(separator);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Design Forces for Bearings"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Maximum Vertical Reaction = {0:f3} Ton = {1:f3} kN", txt_mxf_max_vert_reac.Text, txt_mxf_max_vert_reac_kN.Text));

            list.Add(string.Format("Maximum Vertical Reaction (DL + SIDL + LL) = {0:f3} Ton = {1:f3} kN", txt_brg_max_VR_Ton.Text, txt_brg_max_VR_kN.Text));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Vertical Reaction (DL + SIDL) = {0:f3} Ton-m = {1:f3} kN-m", txt_brg_max_DL_Ton.Text, txt_brg_max_DL_kN.Text));

            list.Add(string.Format("{0:f3} + {1:f3} = {2:f3} OR {3:f3} + {4:f3} = {5:f3}"
                , dgv_mxf_left_des_frc[1, 0].Value
                , dgv_mxf_left_des_frc[1, 1].Value
                , (MyList.StringToDouble(dgv_mxf_left_des_frc[1, 0].Value.ToString()) + MyList.StringToDouble(dgv_mxf_left_des_frc[1, 1].Value.ToString()))
                , dgv_mxf_right_des_frc[1, 0].Value
                , dgv_mxf_right_des_frc[1, 1].Value
                , (MyList.StringToDouble(dgv_mxf_right_des_frc[1, 0].Value.ToString()) + MyList.StringToDouble(dgv_mxf_right_des_frc[1, 1].Value.ToString()))
                ));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mx] = {0:f3} Ton-m = {1:f3} kN-m", txt_mxf_max_Mx.Text, txt_mxf_max_Mx_kN.Text));
            //list.Add(string.Format("txt_max_Mx"));
            //list.Add(string.Format("txt_max_Mx_kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Bending Moment [Mz] = {0:f3} Ton-m = {1:f3} kN-m", txt_mxf_max_Mz.Text, txt_mxf_max_Mz_kN.Text));
            list.Add(string.Format(""));

            #endregion Right End Design Forces

            #endregion Live Load [LL]

            File.WriteAllLines(FILE_SUPPORT_REACTIONS, list.ToArray());

        }
        void Show_and_Save_Data_Load_1_2_3()
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



            double _vert_load, _mx, _mz;

            _vert_load = _mx = _mz = 0.0;
            int _jnt_no = 0;
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);


                //LOAD 1 DEAD LOAD SELF WEIGHT
                //var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 1);
                //var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 1);
                //var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 1);

                //_vert_load = shr.Force;
                //_mx = mx.Force;
                //_mz = mz.Force;

                //// LOAD 2 DEAD LOAD DECK SLAB WET CONCRETE AND SHUTTERING
                //shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 2);
                //mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 2);
                //mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 2);

                ////Add Load 1 + Load 2
                //_vert_load += shr.Force;
                //_mx += mx.Force;
                //_mz += mz.Force;

                ////LOAD 3 DEAD LOAD DESHUTTERING
                //shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 3);
                //mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 3);
                //mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 3);



                var lst = new List<int>();

                lst.Add(_jnt_no);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 4);
                //var shr = Bridge_Analysis.All_Analysis[0].GetJoint_R2_Shear(lst, 4);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 4);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 4);



                //Add Load 1 + Load 2 + Load 3
                //_vert_load += shr.Force;
                //_mx += mx.Force;
                //_mz += mz.Force;


                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;




                //dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);
                dgv_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
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
                _jnt_no = mlist.GetInt(i);




                ////LOAD 1 DEAD LOAD SELF WEIGHT
                //var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 1);
                //var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 1);
                //var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 1);

                //_vert_load = shr.Force;
                //_mx = mx.Force;
                //_mz = mz.Force;

                //// LOAD 2 DEAD LOAD DECK SLAB WET CONCRETE AND SHUTTERING
                //shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 2);
                //mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 2);
                //mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 2);

                ////Add Load 1 + Load 2
                //_vert_load += shr.Force;
                //_mx += mx.Force;
                //_mz += mz.Force;

                ////LOAD 3 DEAD LOAD DESHUTTERING
                //shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 3);
                //mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 3);
                //mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 3);


                ////Add Load 1 + Load 2 + Load 3
                //_vert_load += shr.Force;
                //_mx += mx.Force;
                //_mz += mz.Force;



                var lst = new List<int>();

                lst.Add(_jnt_no);

                //LOAD 1 DEAD LOAD SELF WEIGHT
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 4);
                //var shr = Bridge_Analysis.All_Analysis[0].GetJoint_R2_Shear(lst, 4);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 4);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 4);

                _vert_load = shr.Force;
                _mx = mx.Force;
                _mz = mz.Force;


                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_des_frc.Rows.Add(_jnt_no, Math.Abs(_vert_load), _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load);
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));

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

            txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            #region Chiranjit [2017 06 11]
            txt_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]



            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            //txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            //txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            txt_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_final_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");





            #region Chiranjit [2017 06 11]
            txt_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_max_Mx.Text, 0.0) * 10.0).ToString("f3");
            #endregion Chiranjit [2017 06 11]





            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_final_Mx_kN.Text + " kN-m");
            //txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            //txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            txt_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_final_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");



            #region Chiranjit [2017 06 11]

            txt_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]








            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_final_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");




            #region SIDL
            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            dgv_sidl_left_des_frc.Rows.Clear();
            dgv_sidl_right_des_frc.Rows.Clear();

            mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);


                var jnt = new List<int>();
                jnt.Add(_jnt_no);

                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 5);
                //var shr = Bridge_Analysis.All_Analysis[0].GetJoint_R2_Shear(jnt, 5);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 5);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 5);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;

                shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 6);
                //shr = Bridge_Analysis.All_Analysis[0].GetJoint_R2_Shear(jnt, 6);
                mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 6);
                mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 6);


                _vert_load += shr.Force;
                _mx += mx;
                _mz += mz;

                //if (_vert_load < shr.Force)
                //{
                //    _vert_load = shr.Force;
                //}
                //if (_mx < mx)
                //{
                //    _mx = mx;
                //}
                //if (_mz < mz)
                //{
                //    _mz = mz;
                //}

                dgv_sidl_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            txt_sidl_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_sidl_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_sidl_left_total_Mz.Text = tot_left_Mz.ToString("0.000");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);
                var shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 5);
                var mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 5);
                var mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 5);


                _vert_load = shr;
                _mx = mx;
                _mz = mz;

                shr = Bridge_Analysis.All_Analysis[0].GetJoint_ShearForce(_jnt_no, 6);
                mx = Bridge_Analysis.All_Analysis[0].GetJoint_Torsion(_jnt_no, 6);
                mz = Bridge_Analysis.All_Analysis[0].GetJoint_MomentForce(_jnt_no, 6);

                _vert_load += shr;
                _mx += mx;
                _mz += mz;

                //if (_vert_load < shr.Force)
                //{
                //    _vert_load = shr.Force;
                //}
                //if (_mx < mx)
                //{
                //    _mx = mx;
                //}
                //if (_mz < mz)
                //{
                //    _mz = mz;
                //}

                dgv_sidl_right_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load); ;
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
            }

            txt_sidl_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_sidl_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_sidl_right_total_Mz.Text = tot_right_Mz.ToString("0.000");




            txt_sidl_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_sidl_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            txt_sidl_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_sidl_final_Mx_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mx.Text, 0.0) * 10.0).ToString("f3");



            txt_sidl_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_sidl_final_Mz_kN.Text = (MyList.StringToDouble(txt_sidl_final_Mz.Text, 0.0) * 10.0).ToString("f3");






            #region Chiranjit [2017 06 11]
            txt_sidl_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_sidl_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_sidl_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");

            txt_sidl_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_sidl_max_Mx_kN.Text = (MyList.StringToDouble(txt_sidl_max_Mx.Text, 0.0) * 10.0).ToString("f3");

            txt_sidl_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_sidl_max_Mz_kN.Text = (MyList.StringToDouble(txt_sidl_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]



            #endregion SIDL






            #region LL
            tot_left_vert_reac = 0.0;
            tot_left_Mx = 0.0;
            tot_left_Mz = 0.0;



            dgv_ll_left_des_frc.Rows.Clear();
            dgv_ll_right_des_frc.Rows.Clear();

            mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);


                var jnt = new List<int>();
                jnt.Add(_jnt_no);

                _vert_load = 0;
                _mx = 0;
                _mz = 0;



                for (int j = 0; j < all_loads.Count; j++)
                {

                    #region Get Node results from Dead load analysis
                    //Get Node results from Dead load analysis
                    //var mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_R2_Shear(jnt);
                    var mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_ShearForce(jnt);
                    if (_vert_load < Math.Abs(mxf.Force))
                    {
                        _vert_load = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_Torsion(jnt);
                    if (_mx < Math.Abs(mxf.Force))
                    {
                        _mx = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_MomentForce(jnt);
                    if (_mz < Math.Abs(mxf.Force))
                    {
                        _mz = Math.Abs(mxf.Force);
                    }

                    #endregion Get Forces LL ANALYSIS

                }


                dgv_ll_left_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_left_vert_reac += Math.Abs(_vert_load); ;
                tot_left_Mx += _mx;
                tot_left_Mz += _mz;
                list_arr.Add(string.Format(format, _jnt_no, Math.Abs(_vert_load), _mx, _mz));
            }

            txt_ll_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_ll_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_ll_left_total_Mz.Text = tot_left_Mz.ToString("0.000");


            tot_right_vert_reac = 0.0;
            tot_right_Mx = 0.0;
            tot_right_Mz = 0.0;

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            for (int i = 0; i < mlist.Count; i++)
            {

                _jnt_no = mlist.GetInt(i);

                var jnt = new List<int>();
                jnt.Add(_jnt_no);

                _vert_load = 0;
                _mx = 0;
                _mz = 0;
                for (int j = 0; j < all_loads.Count; j++)
                {

                    #region Get Node results from Dead load analysis
                    //Get Node results from Dead load analysis
                    //var mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_R2_Shear(jnt);
                    var mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_ShearForce(jnt);
                    if (_vert_load < Math.Abs(mxf.Force))
                    {
                        _vert_load = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_Torsion(jnt);
                    if (_mx < Math.Abs(mxf.Force))
                    {
                        _mx = Math.Abs(mxf.Force);
                    }
                    //Get Node results from Dead load analysis
                    mxf = Bridge_Analysis.All_Analysis[j + 3].GetJoint_MomentForce(jnt);
                    if (_mz < Math.Abs(mxf.Force))
                    {
                        _mz = Math.Abs(mxf.Force);
                    }

                    #endregion Get Forces LL ANALYSIS

                }


                dgv_ll_right_des_frc.Rows.Add(_jnt_no, _vert_load, _mx, _mz);

                tot_right_vert_reac += Math.Abs(_vert_load); ;
                tot_right_Mx += _mx;
                tot_right_Mz += _mz;
            }

            txt_ll_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_ll_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_ll_right_total_Mz.Text = tot_right_Mz.ToString("0.000");




            txt_ll_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_ll_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            txt_ll_final_Mx.Text = (tot_left_Mx + tot_right_Mx).ToString("0.000");
            txt_ll_final_Mx_kN.Text = (MyList.StringToDouble(txt_ll_final_Mx.Text, 0.0) * 10.0).ToString("f3");



            txt_ll_final_Mz.Text = (tot_left_Mz + tot_right_Mz).ToString("0.000");
            txt_ll_final_Mz_kN.Text = (MyList.StringToDouble(txt_ll_final_Mz.Text, 0.0) * 10.0).ToString("f3");





            #region Chiranjit [2017 06 11]
            txt_ll_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_ll_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_ll_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");

            txt_ll_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_ll_max_Mx_kN.Text = (MyList.StringToDouble(txt_ll_max_Mx.Text, 0.0) * 10.0).ToString("f3");

            txt_ll_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_ll_max_Mz_kN.Text = (MyList.StringToDouble(txt_ll_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]



            #endregion LL

            tot_right_vert_reac = 0.0;
            tot_left_vert_reac = 0.0;

            tot_left_Mx = 0.0;
            tot_right_Mx = 0.0;

            tot_left_Mz = 0.0;
            tot_right_Mz = 0.0;

            #region DL



            dgv_mxf_left_des_frc.Rows.Clear();
            dgv_mxf_right_des_frc.Rows.Clear();



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            double v1 = 0.0;
            double v2 = 0.0;
            double v3 = 0.0;
            for (int i = 0; i < dgv_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load))
                {
                    _vert_load = v1;
                }

                v2 = MyList.StringToDouble(dgv_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx))
                {
                    _mx = v2;
                }

                v3 = MyList.StringToDouble(dgv_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }

            dgv_mxf_left_des_frc.Rows.Add("DL", _vert_load, _mx, _mz);


            txt_left_max_total_vert_reac.Text = _vert_load.ToString();
            txt_left_max_total_Mx.Text = _mx.ToString();
            txt_left_max_total_Mz.Text = _mz.ToString();


            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(dgv_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(dgv_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            txt_right_max_total_vert_reac.Text = _vert_load.ToString();
            txt_right_max_total_Mx.Text = _mx.ToString();
            txt_right_max_total_Mz.Text = _mz.ToString();


            dgv_mxf_right_des_frc.Rows.Add("DL", _vert_load, _mx, _mz);







            #endregion DL



            #region SIDL

            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_sidl_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_sidl_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(dgv_sidl_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(dgv_sidl_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }


            txt_sidl_left_max_total_vert_reac.Text = _vert_load.ToString();
            txt_sidl_left_max_total_Mx.Text = _mx.ToString();
            txt_sidl_left_max_total_Mz.Text = _mz.ToString();


            dgv_mxf_left_des_frc.Rows.Add("SIDL", _vert_load, _mx, _mz);



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_sidl_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_sidl_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(dgv_sidl_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(dgv_sidl_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }


            txt_sidl_right_max_total_vert_reac.Text = _vert_load.ToString();
            txt_sidl_right_max_total_Mx.Text = _mx.ToString();
            txt_sidl_right_max_total_Mz.Text = _mz.ToString();

            dgv_mxf_right_des_frc.Rows.Add("SIDL", _vert_load, _mx, _mz);


            #endregion SIDL



            #region LL

            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_ll_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_ll_left_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(dgv_ll_left_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(dgv_ll_left_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            txt_ll_left_max_vert_reac.Text = _vert_load.ToString();
            txt_ll_left_max_total_Mx.Text = _mx.ToString();
            txt_ll_left_max_total_Mz.Text = _mz.ToString();


            dgv_mxf_left_des_frc.Rows.Add("LL", _vert_load, _mx, _mz);



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_ll_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_ll_right_des_frc[1, i].Value.ToString(), 0.0);
                if (Math.Abs(v1) > Math.Abs(_vert_load)) _vert_load = v1;

                v2 = MyList.StringToDouble(dgv_ll_right_des_frc[2, i].Value.ToString(), 0.0);
                if (Math.Abs(v2) > Math.Abs(_mx)) _mx = v2;

                v3 = MyList.StringToDouble(dgv_ll_right_des_frc[3, i].Value.ToString(), 0.0);
                if (Math.Abs(v3) > Math.Abs(_mz)) _mz = v3;

            }




            txt_ll_right_max_total_vert_reac.Text = _vert_load.ToString();
            txt_ll_right_max_total_Mx.Text = _mx.ToString();
            txt_ll_right_max_total_Mz.Text = _mz.ToString();



            dgv_mxf_right_des_frc.Rows.Add("LL", _vert_load, _mx, _mz);


            #endregion LL





            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_mxf_left_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_mxf_left_des_frc[1, i].Value.ToString(), 0.0);
                _vert_load += v1;

                v2 = MyList.StringToDouble(dgv_mxf_left_des_frc[2, i].Value.ToString(), 0.0);
                _mx += v2;

                v3 = MyList.StringToDouble(dgv_mxf_left_des_frc[3, i].Value.ToString(), 0.0);
                _mz += v3;

            }



            tot_left_vert_reac = _vert_load;
            tot_left_Mx = _mx;
            tot_left_Mz = _mz;


            txt_mxf_left_total_vert_reac.Text = _vert_load.ToString();
            txt_mxf_left_total_Mx.Text = _mx.ToString();
            txt_mxf_left_total_Mz.Text = _mz.ToString();



            _vert_load = 0.0;
            _mx = 0.0;
            _mz = 0.0;

            v1 = 0.0;
            v2 = 0.0;
            v3 = 0.0;
            for (int i = 0; i < dgv_mxf_right_des_frc.RowCount - 1; i++)
            {
                v1 = MyList.StringToDouble(dgv_mxf_right_des_frc[1, i].Value.ToString(), 0.0);
                _vert_load += v1;

                v2 = MyList.StringToDouble(dgv_mxf_right_des_frc[2, i].Value.ToString(), 0.0);
                _mx += v2;

                v3 = MyList.StringToDouble(dgv_mxf_right_des_frc[3, i].Value.ToString(), 0.0);
                _mz += v3;

            }


            tot_right_vert_reac = _vert_load;
            tot_right_Mx = _mx;
            tot_right_Mz = _mz;

            txt_mxf_right_total_vert_reac.Text = _vert_load.ToString();
            txt_mxf_right_total_Mx.Text = _mx.ToString();
            txt_mxf_right_total_Mz.Text = _mz.ToString();

            //txt_left_max_total_vert_reac
            //txt_left_max_total_Mx
            //txt_left_max_total_Mz

            //txt_right_max_total_vert_reac
            //txt_right_max_total_Mx
            //txt_right_max_total_Mz



            //txt_sidl_left_max_total_vert_reac
            //txt_sidl_left_max_total_Mx
            //txt_sidl_left_max_total_Mz

            //txt_sidl_right_max_total_vert_reac
            //txt_sidl_right_max_total_Mx
            //txt_sidl_right_max_total_Mz


            //txt_ll_left_max_total_vert_reac
            //txt_ll_left_max_total_Mx
            //txt_ll_left_max_total_Mz

            //txt_ll_right_max_total_vert_reac
            //txt_ll_right_max_total_Mx
            //txt_ll_right_max_total_Mz




            #region Chiranjit [2017 06 11]

            txt_mxf_max_vert_reac.Text = Math.Max(tot_right_vert_reac, tot_left_vert_reac).ToString("0.000");
            txt_mxf_max_vert_reac_kN.Text = (MyList.StringToDouble(txt_mxf_max_vert_reac.Text, 0.0) * 10.0).ToString("f3");

            txt_mxf_max_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_mxf_max_Mx_kN.Text = (MyList.StringToDouble(txt_mxf_max_Mx.Text, 0.0) * 10.0).ToString("f3");

            txt_mxf_max_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz)) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_mxf_max_Mz_kN.Text = (MyList.StringToDouble(txt_mxf_max_Mz.Text, 0.0) * 10.0).ToString("f3");

            #endregion Chiranjit [2017 06 11]


            txt_brg_max_VR_Ton.Text = txt_mxf_max_vert_reac.Text;
            txt_brg_max_VR_kN.Text = txt_mxf_max_vert_reac_kN.Text;



            tot_left_vert_reac = 0.0;
            tot_right_vert_reac = 0.0;


            for (int i = 0; i < dgv_mxf_right_des_frc.RowCount - 2; i++)
            {
                v1 = MyList.StringToDouble(dgv_mxf_left_des_frc[1, i].Value.ToString(), 0.0);
                tot_left_vert_reac += v1;

                v1 = MyList.StringToDouble(dgv_mxf_right_des_frc[1, i].Value.ToString(), 0.0);
                tot_right_vert_reac += v1;
            }



            _vert_load = Math.Max(tot_left_vert_reac, tot_right_vert_reac);
            txt_brg_max_DL_Ton.Text = _vert_load.ToString();
            txt_brg_max_DL_kN.Text = (_vert_load * 10).ToString();


            double VR = MyList.StringToDouble(txt_brg_max_VR_Ton.Text, 0.0) * 10;
            double DL = MyList.StringToDouble(txt_brg_max_DL_Ton.Text, 0.0) * 10;
            //double HRT = MyList.StringToDouble(txt_brg_max_HRT_Ton.Text, 0.0) * 10;
            //double HRL = MyList.StringToDouble(txt_brg_max_HRL_Ton.Text, 0.0) * 10;



            txt_brg_max_VR_kN.Text = VR.ToString("f3");
            txt_brg_max_DL_kN.Text = DL.ToString("f3");
            //txt_brg_max_HRT_kN.Text = HRT.ToString("f3");
            //txt_brg_max_HRL_kN.Text = HRL.ToString("f3");

            //Text_Changed_Forces();

            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_final_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_final_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        #endregion frm_Pier_ViewDesign_Forces
        #endregion

        #region Chiranjit [2012 07 10]
        //Write All Data in a File
        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            Text_Changed();

            if (showMessage) DemoCheck();

            if (user_path != iApp.LastDesignWorkingFolder)
                iApp.Save_Form_Record(this, user_path);
        }
        public void Read_All_Data()
        {
            //if (iApp.IsDemo) return;

            string data_file = Bridge_Analysis.Input_File;

            if (!File.Exists(data_file)) return;
            try
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                }

            }
            catch (Exception ex) { }
            Button_Enable_Disable();
        }
        #endregion Chiranjit [2012 07 10]

        #region Chiranjit [2012 07 20]

        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "110";
                txt_Ana_B.Text = "55.375";
                txt_Ana_CW.Text = "52.0";
                txt_Ana_ang.Text = "20";
                cmb_NMG.SelectedIndex = 3;
            }
        }
        #endregion Chiranjit [2012 07 20]

        #region Excel Files

        public string Excel_PSC_Girder
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder BS.xlsx");
             
                
                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder IS.xlsx");
            }
        }
        public string Excel_Deckslab
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder_Deck Slab BS.xlsx");
                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder_Deck Slab IS.xlsx");
            }
        }

        #endregion Excel Files


        string Get_Design_Report()
        {
            string file_path = Path.Combine(user_path, "Design of PSC I Girder");

            file_path = Path.Combine(file_path, Path.GetFileName(Excel_PSC_Girder));

            return file_path;
            
        }
        /*
        private void btn_LS_Click(object sender, EventArgs e)
        {
            if (!Check_Project_Folder()) return;

            Button btn = sender as Button;

            string excel_file_name = "";
            string copy_path = "";
            if (btn.Name == btn_LS_long_ws.Name)
            {
                //excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\RCC T Girder LS\RCC Precast Long Girder.xlsm");


                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder BS.xlsx");
                //else
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder IS.xlsx");
                Write_All_Data(true);

                excel_file_name = Excel_PSC_Girder;
                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                copy_path = Get_Design_Report();

                if (!Directory.Exists(Path.GetDirectoryName(copy_path))) Directory.CreateDirectory(Path.GetDirectoryName(copy_path));


                File.Copy(excel_file_name, copy_path, true);
             
                return;

            }
            //else if (btn.Name == btn_LS_psc_rep_open.Name)
            //{
            //    //copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder));
            //    copy_path = Get_Design_Report();
            //    if (File.Exists(copy_path))
            //        iApp.OpenExcelFile(copy_path, "2011ap");
            //}
            else
            {
                iApp.Open_WorkSheet_Design();
                return;
            }

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
            Button_Enable_Disable();
        }
        */

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();


        #region Chiranjit [2016 07 11]
        List<string> ll_comb = new List<string>();

        public void Store_LL_Combinations(DataGridView dgv_live_loads, DataGridView dgv_loads)
        {
            ll_comb.Clear();
            List<string> com = new List<string>();
            Hashtable ht_cmb = new Hashtable();

            string kStr = "";
            string txt = "";
            int i = 0;
            for (i = 0; i < dgv_live_loads.RowCount; i++)
            {
                kStr = dgv_live_loads[0, i].Value.ToString();
                txt = dgv_live_loads[1, i].Value.ToString();

                if (kStr.StartsWith("TYPE"))
                {
                    try
                    {
                        ht_cmb.Add(kStr, txt);
                    }
                    catch (Exception exx) { }
                }
            }

            dgv_live_loads.Tag = ht_cmb;


            com.Clear();


            for (i = 0; i < dgv_loads.RowCount; i++)
            {

                kStr = dgv_loads[0, i].Value.ToString();


                if (kStr.StartsWith("LOAD"))
                {
                    com.Clear();

                    for (int c = 1; c < dgv_loads.ColumnCount; c++)
                    {
                        txt = dgv_loads[c, i].Value.ToString();

                        if (txt == "") break;
                        kStr = ht_cmb[txt] as string;
                        com.Add(kStr);
                    }

                    //List<string> lst_lane1 = new List<string>();
                    //List<string> lst_lane2 = new List<string>();


                    //lst_lane1.AddRange(com);


                    //lst_lane2.Add(lst_lane1[0]);


                    //lst_lane1.RemoveAt(0);


                    //for(int k = 0; )




                    if (com.Count > 0)
                    {
                        txt = com[0];

                        int lane = 1;

                        for (int j = 1; j < com.Count; j++)
                        {
                            if (txt == com[j]) lane++;
                        }
                        if (lane > 1)
                        {
                            txt = lane + " Lane " + txt;
                        }
                        else
                        {
                            txt = lane + " Lane " + txt;

                            for (int j = 1; j < com.Count; j++)
                            {
                                txt += " + 1 Lane " + com[j];
                            }
                        }
                        ll_comb.Add(txt);
                    }

                }
            }
            dgv_loads.Tag = ll_comb;
        }

        #endregion Chiranjit [2016 07 11]

        public void LONG_GIRDER_LL_TXT()
        {

            Store_LL_Combinations(dgv_long_liveloads, dgv_long_loads);

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
            long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            load_list_1 = new List<string>();
            load_list_2 = new List<string>();
            load_list_3 = new List<string>();
            load_list_4 = new List<string>();
            load_list_5 = new List<string>();
            load_list_6 = new List<string>();
            load_total_7 = new List<string>();



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
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
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

        private void btn_restore_ll_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_long_restore_ll.Name)
                    Default_Moving_LoadData(dgv_long_liveloads);
            }
        }
        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            ComboBox cmb = cmb_long_open_file_process;

            if (btn == btn_view_data_1)
            {
                cmb = cmb_long_open_file_analysis;
            }


            string file_name = "";
            string ll_txt = "";

           
            #region Set File Name
            if (cmb.SelectedIndex < cmb.Items.Count)
            {
                file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
            }
            else 
            {
                file_name = File_Long_Girder_Results;
            }
            #endregion Set File Name

            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name || btn.Name == btn_view_data_1.Name)
            {
                iApp.View_Input_File(file_name);
                //if (File.Exists(ll_txt))
                //    iApp.RunExe(ll_txt);
                //if (File.Exists(file_name))
                //    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_view_preprocess.Name)
            {
                if (File.Exists(file_name))
                {
                    iApp.View_PreProcess(file_name);
                }
            }
            else if (btn.Name == btn_view_postprocess.Name)
            {
                if (File.Exists(file_name))
                {
                    iApp.View_PostProcess(file_name);
                }
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name)) iApp.RunExe(file_name);
                //if (File.Exists(FILE_SUMMARY_RESULTS)) iApp.RunExe(FILE_SUMMARY_RESULTS);
            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                //file_name = MyList.Get_Analysis_Report_File(file_name);
                //if (File.Exists(file_name))
                //    iApp.OpenWork(file_name, true);

                if (File.Exists(File_Long_Girder_Results)) iApp.RunExe(File_Long_Girder_Results);

            }
        }

        #region Design of RCC Pier

       
       

        private void btn_RccPier_Drawing_Click1(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);
        }
        #endregion Design of RCC Pier


        private void btn_dwg_open_Click(object sender, EventArgs e)
        {

            if (!Check_Project_Folder()) return;

            Button btn = sender as Button;

            string draw = Drawing_Folder;


            string copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder));


            if (btn.Name == btn_dwg_open_consts.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Sample Construction Drawings"), "COST_Girder_Bridges");
                return;
            }
            eOpenDrawingOption opt = iApp.Open_Drawing_Option();

            if (opt == eOpenDrawingOption.Cancel) return;

            if (opt == eOpenDrawingOption.Design_Drawings)
            {
                #region Design Drawings
                if (btn.Name == btn_dwg_open.Name)
                {
                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");

                    //iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");

                    copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder));

                    iApp.Form_Drawing_Editor(eBaseDrawings.PSC_I_Girder_LS_GAD, Drawing_Folder,
                        Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder))).ShowDialog();

                }
                if (btn.Name == btn_dwg_rcc_abut.Name)
                {
                    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                }
                else if (btn.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                else if (btn.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_PIER, Title, draw, copy_path).ShowDialog();
                }
                #endregion Design Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {
                #region Design Drawings
                if (btn.Name == btn_dwg_open.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");
                }
                if (btn.Name == btn_dwg_rcc_abut.Name)
                {
                    //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
                }
                else if (btn.Name == btn_dwg_pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
                }
                else if (btn.Name == btn_dwg_open_Deckslab.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Composite Deck Slab Drawing"), "COMPOSITE_DECK_LS");
                    //iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB, Title, draw, copy_path).ShowDialog();
                }
                else if (btn.Name == btn_dwg_open_Counterfort.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Counterfort Abutment Drawings"), "BOX_ABUTMENT");
                }
                else if (btn.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Canlilever Abutment Drawings"), "TBeam_Abutment");
                }
                else if (btn.Name == btn_dwg_open_Pier.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "TBeam_Pier");
                }
                #endregion Design Drawings
            }


        }

       


        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {



            ComboBox cmb = sender as ComboBox;
            string file_name = "";

            if (Bridge_Analysis != null)
            {
                if (cmb.SelectedIndex < cmb.Items.Count)
                {
                    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb.SelectedIndex);
                }
                else
                {
                    file_name = File_Long_Girder_Results;
                }
            }
            if (cmb == cmb_long_open_file_process)
            {

                btn_view_data.Enabled = File.Exists(file_name);
                btn_View_Moving_Load.Enabled = File.Exists(FILE_SUMMARY_RESULTS);
                //btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file_process.SelectedIndex != 9;
                btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
            }
            else if (cmb == cmb_long_open_file_analysis)
            {
                btn_view_data_1.Enabled = File.Exists(file_name);
            }
            Button_Enable_Disable();

        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            //Control rbtn = sender as Control;


            //if (rbtn.Name == chk_fp_left.Name)
            //{
            //    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
            //        chk_fp_right.Checked = true;
            //}
            //else if (rbtn.Name == chk_fp_right.Name)
            //{
            //    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
            //        chk_fp_left.Checked = true;
            //}


            //if (rbtn.Name == chk_cb_left.Name)
            //{
            //    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
            //        chk_cb_right.Checked = true;
            //}
            //else if (rbtn.Name == chk_cb_right.Name)
            //{
            //    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
            //        chk_cb_left.Checked = true;
            //}

            Control rbtn = sender as Control;
            if (rbtn.Name == chk_fp_left.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                        chk_fp_right.Checked = true;

                    if (!chk_fp_left.Checked)
                    {
                        txt_Ana_Hf_LHS.Enabled = false;
                        txt_Ana_Wf_LHS.Enabled = false;
                        txt_Ana_Hf_LHS.Text = "0.000";
                        txt_Ana_Wf_LHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hf_LHS.Enabled = true;
                        txt_Ana_Wf_LHS.Enabled = true;
                        txt_Ana_Hf_LHS.Text = "1.000";
                        txt_Ana_Wf_LHS.Text = "1.69";
                    }

                }
            }
            else if (rbtn.Name == chk_fp_right.Name)
            {
                if (chk_footpath.Checked)
                {
                    if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                        chk_fp_left.Checked = true;


                    if (!chk_fp_right.Checked)
                    {
                        txt_Ana_Hf_RHS.Enabled = false;
                        txt_Ana_Wf_RHS.Enabled = false;
                        txt_Ana_Hf_RHS.Text = "0.000";
                        txt_Ana_Wf_RHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hf_RHS.Enabled = true;
                        txt_Ana_Wf_RHS.Enabled = true;
                        txt_Ana_Hf_RHS.Text = "1.000";
                        txt_Ana_Wf_RHS.Text = "1.69";
                    }
                }
            }

            if (rbtn.Name == chk_cb_left.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                        chk_cb_right.Checked = true;

                    if (!chk_cb_left.Checked)
                    {
                        txt_Ana_Hc_LHS.Enabled = false;
                        txt_Ana_Wc_LHS.Enabled = false;
                        txt_Ana_Hc_LHS.Text = "0.000";
                        txt_Ana_Wc_LHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hc_LHS.Enabled = true;
                        txt_Ana_Wc_LHS.Enabled = true;
                        txt_Ana_Hc_LHS.Text = "3.50";
                        txt_Ana_Wc_LHS.Text = "1.69";
                    }
                }
            }
            else if (rbtn.Name == chk_cb_right.Name)
            {
                if (chk_crash_barrier.Checked)
                {
                    if (!chk_cb_left.Checked && !chk_cb_right.Checked)
                        chk_cb_left.Checked = true;


                    if (!chk_cb_right.Checked)
                    {
                        txt_Ana_Hc_RHS.Enabled = false;
                        txt_Ana_Wc_RHS.Enabled = false;
                        txt_Ana_Hc_RHS.Text = "0.000";
                        txt_Ana_Wc_RHS.Text = "0.000";
                    }
                    else
                    {
                        txt_Ana_Hc_RHS.Enabled = true;
                        txt_Ana_Wc_RHS.Enabled = true;
                        txt_Ana_Hc_RHS.Text = "3.50";
                        txt_Ana_Wc_RHS.Text = "1.69";
                    }
                }
            }
            else if (rbtn.Name == chk_crash_barrier.Name)
            {
                chk_cb_left.Checked = chk_crash_barrier.Checked;
                chk_cb_right.Checked = chk_crash_barrier.Checked;
            }
            else if (rbtn.Name == chk_footpath.Name)
            {
                chk_fp_left.Checked = chk_footpath.Checked;
                chk_fp_right.Checked = chk_footpath.Checked;
            }

            chk_cb_left.Enabled = chk_crash_barrier.Checked;
            chk_cb_right.Enabled = chk_crash_barrier.Checked;

            chk_fp_left.Enabled = chk_footpath.Checked;
            chk_fp_right.Enabled = chk_footpath.Checked;

            if (rbtn.Name == chk_crash_barrier.Name)
            {
                grb_crash_barrier.Enabled = chk_crash_barrier.Checked;
                if (!chk_crash_barrier.Checked)
                {
                    txt_Ana_Hc_LHS.Text = "0.000";
                    txt_Ana_Wc_LHS.Text = "0.000";
                    txt_Ana_Hc_RHS.Text = "0.000";
                    txt_Ana_Wc_RHS.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc_LHS.Text = "3.50";
                    txt_Ana_Wc_LHS.Text = "1.69";
                    txt_Ana_Hc_RHS.Text = "3.50";
                    txt_Ana_Wc_RHS.Text = "1.69";
                }
            }
            else if (rbtn.Name == chk_footpath.Name)
            {
                grb_ana_footpath.Enabled = chk_footpath.Checked;
                if (!chk_footpath.Checked)
                {
                    txt_Ana_Wf_LHS.Text = "0.000";
                    txt_Ana_Hf_LHS.Text = "0.000";
                    txt_Ana_Wf_RHS.Text = "0.000";
                    txt_Ana_Hf_RHS.Text = "0.000";

                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_Wr.Text = "0.000";
                }
                else
                {
                    txt_Ana_Wf_LHS.Text = "1.000";
                    txt_Ana_Hf_LHS.Text = "0.250";
                    txt_Ana_Wf_RHS.Text = "1.000";
                    txt_Ana_Hf_RHS.Text = "0.250";
                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_Wr.Text = "0.100";
                }
            }




            if (chk_crash_barrier.Checked && chk_footpath.Checked)
            {

                if (chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_4_;

                if (chk_cb_left.Checked && !chk_cb_right.Checked && chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_5_;

                if (!chk_cb_left.Checked && chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_6_;

                if (chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_7_;

                if (chk_cb_left.Checked && chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_8_;

                if (!chk_cb_left.Checked && chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_9_;

                if (chk_cb_left.Checked && !chk_cb_right.Checked && chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_10_;

                if (chk_cb_left.Checked && !chk_cb_right.Checked && !chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_11_;


            }
            else if (chk_crash_barrier.Checked)
            {
                if (chk_cb_left.Checked && chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_BHS__Case_1_;
                if (chk_cb_left.Checked && !chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_LHS__Case_2_;
                if (!chk_cb_left.Checked && chk_cb_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_3_;

            }
            else if (chk_footpath.Checked)
            {
                if (chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_2__Both_Footpaths;
                else if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_3__LHS_Footpath;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_4__LHS_Footpath;
            }
            else
                pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.Parapet__Case_1__No_Footpath_;


            Refresh();


            Text_Changed();
        }
        public void Calculate_Load_Computation()
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();


            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for PSC I - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab"));
            wi1 = SMG * SCG * (Ds * Y_c + Dw * Y_w);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wi2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wi2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wi2));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
                wi3, SCG, wi4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders"));
            NIG = NMG * (NCG - 1);
            list.Add(string.Format("NIG = NMG*(NCG - 1) = {0:f0} * ({1:f0}-1) = {2:f0} nos.",
                NMG, NCG, NIG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            NIM = 70;
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            list.Add(string.Format("NIM = 70 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wiu = wi4 * NIG / NIM;
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3} kN/m. = {3:f3} Ton/m.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
                wiu, swf, (wiu = wiu * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            //member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));

            //list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                131 TO 200 UNI GY -{0:f4}", wiu));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab and Wearing Course"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c + Dw * Y_w);
            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c, Dw, Y_w));
            list.Add(string.Format("   = {0:f3} kN.", wo1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wo2 = SCG * BMG * DMG * Y_c;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hc, Wc, Y_c, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Wf * Hs * Y_c;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hs, Y_c, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Wr * Wk * Y_c;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wr, Wk, Y_c, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders ")); wo6 = wo1 + wo2 + wo3 + wo4 + wo5;
            list.Add(string.Format("wo6 = wo1 + wo2 + wo3 + wo4 + wo5 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} ",
                wo1, wo2, wo3, wo4, wo5));
            list.Add(string.Format("   = {0:f3} kN.", wo6));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL")); wo7 = wo6 / SCG;
            list.Add(string.Format("wo7 = wo6/SCG = {0:f3}/{1:f3} = {2:f3} kN/m.", wo6, SCG, wo7));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total outer members segments of main long girders")); NOG = 2 * (NCG - 1);
            list.Add(string.Format("NOG = 2*(NCG - 1) = 2*({0:f0}-1) = {1:f0} nos.", NCG, NOG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            NOM = 20;
            list.Add(string.Format("NOM = 20 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wou = wo7 * NOG / NOM;
            list.Add(string.Format("wou = wo7*NOG/NOM = {0:f3}*{1:f0}/{2} = {3:f3} kN/m. = {4:f3} Ton/m.",
                wo7, NOG, NOM, wou, (wou = wou / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));
            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Ton/m.",
                wou, swf, (wou = wou * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));
            member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                121 TO 130 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            list.Add(string.Format("                13 TO 21 FZ -{0:f4}", wjl));
            list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            //member_load.Add(string.Format("LOAD 2"));
            member_load.Add(string.Format("JOINT LOAD"));
            member_load.Add(string.Format("13 TO 21 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));

            txt_member_load.Lines = member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();

            //if (user_path != iApp.LastDesignWorkingFolder)
            //    File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        List<double> deck_member_load = new List<double>();

        List<string> self_member_load = new List<string>();

        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
        {
            if (Bridge_Analysis._Outer_Girder_Support == null)
            {
                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);
            }
            outer_girders = Bridge_Analysis._Outer_Girder_Support + " " + Bridge_Analysis._Outer_Girder_Mid;
            inner_girders = Bridge_Analysis._Inner_Girder_Support + " " + Bridge_Analysis._Inner_Girder_Mid;

            List<string> list = new List<string>();
            List<string> long_member_load = new List<string>();

            double SMG, SCG, wi1, wi2, wi3, wi4, wi5, wi6, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;


            double ts = Ds / 12;

//Girder spacing = 9 ft.- 8 in.


//Top cover = 2 ½ in. (S5.12.3)
//(includes ½ in. integral wearing surface)
//Bottom cover = 1 in. (S5.12.3)
//Steel yield strength = 60 ksi
//Slab conc. compressive strength = 4 ksi
//Concrete density = 150 pcf
//Future wearing surface density = 30 psf

            

            SMG = (B - CL - CR) / (NMG - 1);


            double conc_wt = MyList.StringToDouble(txt_Ana_concrete_weight.Text, 0.0);

            double deck_wt = ts * conc_wt;

            //list.Add(string.Format("Self weight of the deck = 8(150)/12 = 100 psf "));
            list.Add(string.Format("Self weight of the deck = {0}x{1}/12 = {2:f3} ksf", deck_wt, conc_wt, deck_wt));


            double self_mom = (deck_wt) * Math.Pow(SMG, 2) / 10;
            //list.Add(string.Format("Unfactored self weight positive or negative moment = (100/1000)(9.66)^2/10= 0.93 k-ft/ft "));
            list.Add(string.Format("Unfactored self weight positive or negative moment = ({0:f3} x {1:f3})^2/10= {2:f3} k-ft/ft ", deck_wt, SMG, self_mom));
            list.Add(string.Format(""));

            double FWS_WT = MyList.StringToDouble(txt_Ana_FWS_Weight.Text, 0.0);

            list.Add(string.Format("Future wearing surface = {0} ksf ", FWS_WT));


            //list.Add(string.Format("Unfactored FWS positive or negative moment = (30/1000)(9.66)2/10 "));
            double FWS_MOM = (FWS_WT * Math.Pow(SMG , 2) / 10);
            list.Add(string.Format("Unfactored FWS positive or negative moment = ({0:f3} x {1:f3})^ 2 / 10 = {2:f3} k-ft/ft", FWS_WT, SMG, FWS_MOM));
            list.Add(string.Format(""));

            double parapet_wt = MyList.StringToDouble(txt_Ana_parapet_unit_weight.Text, 0.0);
            list.Add(string.Format("Assumed loads "));
            list.Add(string.Format("Self weight of the slab in the overhang area = 0.1125 k/sq.ft of the deck overhang surface area "));
            list.Add(string.Format("Weight of parapet = {0:f3} k/ft of length of parapet", parapet_wt));
            list.Add(string.Format(""));
            list.Add(string.Format("Future wearing surface = {0:f3} k/sq.ft of deck surface area ", FWS_WT));
            //list.Add(string.Format("As required by SA13.4.1, there are three design cases to be checked when designing the deck overhang regions."));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Superstructure"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Required information: "));
            //list.Add(string.Format("AASHTO Type I-Beam (28/72) "));
            //list.Add(string.Format("Noncomposite beam area, Ag = 1,085 in2 "));
            //list.Add(string.Format("Noncomposite beam moment of inertia, Ig = 733,320 in4 "));
            //list.Add(string.Format("Deck slab thickness, ts = 8 in. "));
            //list.Add(string.Format("Span length, L = 110 ft. "));
            //list.Add(string.Format("Girder spacing, S = 9 ft. - 8 in. "));
            //list.Add(string.Format("Modulus of elasticity of the beam, EB 4,696 ksi (S5.4.2.4) "));
            //list.Add(string.Format("Modulus of elasticity of the deck, ED = 3,834 ksi (refer to Design Example Section 2.1.3) "));
            //list.Add(string.Format("C.G. to top of the basic beam = 35.62 in. "));
            //list.Add(string.Format("C.G. to bottom of the basic beam = 36.38 in."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Interior girder "));
            list.Add(string.Format(""));
            list.Add(string.Format("Girder weight "));
            list.Add(string.Format(""));
            list.Add(string.Format("DCgirder (I) = Ag(γgirder) "));
            list.Add(string.Format("where: "));
            //list.Add(string.Format("Ag = beam cross-sectional area (sq.in) = 1,085 sq.in"));
            double Ag = LG_INNER_MID.Girder_Section_A * 144;
            list.Add(string.Format("Ag = beam cross-sectional area (sq.in) = {0:f3} sq.in", Ag));
            list.Add(string.Format(" "));
            //list.Add(string.Format("γ = unit weight of beam concrete (kcf) = 0.150 kcf "));
            list.Add(string.Format("γ = unit weight of beam concrete (kcf) = {0:f3} kcf ", conc_wt));
            list.Add(string.Format(""));


            double DCgirder_I = (Ag / 144) * conc_wt;

            //list.Add(string.Format("DCgirder (I) = (1,085/144)(0.150) = 1.13 k/ft/girder "));
            list.Add(string.Format("DCgirder (I) = ({0:f2}/144) x {1:f3} = {2:f3} k/ft/girder ", Ag, conc_wt, DCgirder_I));
            list.Add(string.Format(""));
            list.Add(string.Format("Deck slab weight "));
            list.Add(string.Format(""));
            list.Add(string.Format("The total thickness of the slab is used in calculating the weight. "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Girder spacing = 9.667 ft. "));
            list.Add(string.Format("Girder spacing = {0:f3} ft. ", SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("Slab thickness = {0} in. ", Ds));
            list.Add(string.Format(""));

            double DCslab_I = (SMG * Ds / 12) * conc_wt;

            //list.Add(string.Format("DCslab (I) = 9.667(8/12)(0.150) = 0.967 k/ft/girder"));
            list.Add(string.Format("DCslab (I) = {0:f3}x({1}/12)x({2:f3}) = {3:f3} k/ft/girder", SMG, Ds, conc_wt, DCslab_I));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Exterior girder "));
            list.Add(string.Format(""));
            list.Add(string.Format("Girder weight "));
            list.Add(string.Format(""));

            double DCgirder_E = LG_OUTER_MID.Girder_Section_A * conc_wt;
            //list.Add(string.Format("DCgirder (E) = 1.13 k/ft/girder "));
            list.Add(string.Format("DCgirder (E) = {0:f3} k/ft/girder ", DCgirder_E));
            list.Add(string.Format(""));
            list.Add(string.Format("Deck slab weight"));
            list.Add(string.Format(""));
            list.Add(string.Format("Slab width = overhang width + ½ girder spacing "));
            list.Add(string.Format(""));
            //list.Add(string.Format("= 3.521 + ½(9.667) "));
            list.Add(string.Format("= {0:f3} + ½({1:f3}) ", CL, SMG));
            list.Add(string.Format(""));

            double slab_w_ext = CL + SMG / 2;
            //list.Add(string.Format("= 8.35 ft. "));
            list.Add(string.Format("= {0:f3} ft. ", slab_w_ext));
            list.Add(string.Format(""));
            //list.Add(string.Format("Slab thickness = 8 in."));
            list.Add(string.Format(""));

            double DCslab_E = slab_w_ext * ts * conc_wt;
            //list.Add(string.Format("DCslab (E) = 8.35(8/12)(0.150) = 0.835 k/ft/girder"));
            list.Add(string.Format("DCslab (E) = {0:f3}x({1}/12)x({2:f3}) = {3:f3} k/ft/girder", slab_w_ext, Ds, conc_wt, DCslab_E));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double hanch_w = MyList.StringToDouble(txt_Ana_haunch_width.Text, 0.0);
            double hanch_t = MyList.StringToDouble(txt_Ana_haunch_thickness.Text, 0.0);
            list.Add(string.Format("Haunch weight "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Width = 42 in. "));
            list.Add(string.Format("Width = {0} in. ", hanch_w));
            list.Add(string.Format(""));
            //list.Add(string.Format("Thickness = 4 in. "));
            list.Add(string.Format("Thickness = {0} in. ", hanch_t));
            list.Add(string.Format(""));

            double DChaunch = (hanch_w * hanch_t / 144) * conc_wt;
            //list.Add(string.Format("DChaunch = [42(4)/144](0.150) = 0.175 k/ft/girder"));
            list.Add(string.Format("DChaunch = [({0}x{1}/144)]x({2:f3}) = {2:f3} k/ft/girder", hanch_w, hanch_t, conc_wt, DChaunch));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Concrete diaphragm weight "));
            list.Add(string.Format(""));
            list.Add(string.Format("A concrete diaphragm is placed at one-half the noncomposite span length. "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Location of the diaphragms: "));
            list.Add(string.Format(""));

            //list.Add(string.Format("Span 1 = 54.5 ft. from centerline of end bearing "));
            //list.Add(string.Format("Span 2 = 55.5 ft. from centerline of pier "));
            list.Add(string.Format(""));
            //For this example, arbitrarily assume that the thickness of the diaphragm is 10 in. 
            //The diaphragm spans from beam to beam minus the web thickness and has a depth equal to the distance from 
            //the top of the beam to the bottom of the web. Therefore, the concentrated load to be applied at the locations above is: 

            double diaph_t = MyList.StringToDouble(txt_sec_int_cg_bw.Text, 0.0);
            double diaph_d = MyList.StringToDouble(txt_Ana_DCG.Text, 0.0);

            //LG_INNER_MID.d7
            list.Add(string.Format("thickness of the diaphragm = {0} in", diaph_t));
            list.Add(string.Format("thickness of the diaphragm = {0} in", diaph_d));

            double DCdiaphragm = conc_wt * (diaph_t / 12) * (SMG - (LG_INNER_MID.b4 / 12)) * (LG_INNER_MID.d7 - LG_INNER_MID.d6 - LG_INNER_MID.d5) / 12;


            //list.Add(string.Format("DCdiaphragm = 0.15(10/12)[9.667 - (8/12)](72 - 18)/12 "));
            list.Add(string.Format("DCdiaphragm = {0:f3} x ({1}/12) x [{2:f3} - ({3}/12)]({4} - {5})/12",
                conc_wt, diaph_t, SMG, LG_INNER_MID.b4, LG_INNER_MID.d7, (LG_INNER_MID.d6 - LG_INNER_MID.d5)));
            list.Add(string.Format(""));
            //list.Add(string.Format("= 5.0625 k/girder"));
            list.Add(string.Format("            = {0:f3} k/girder", DCdiaphragm));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Parapet weight "));
            list.Add(string.Format(""));

            double parapet_area = 4.33;


            list.Add(string.Format("According to the Parapet cross-sectional area = {0:f3} sq.ft", parapet_area));
            list.Add(string.Format(""));

            double DCparapet = parapet_area * conc_wt;
            //list.Add(string.Format("DCparapet = 4.33(0.150) = 0.650 k/ft "));
            //list.Add(string.Format("DCparapet = {0:f3} x {1:f3} = {2:f3} k/ft = 0.650/6 girders ", parapet_area, conc_wt, DCparapet));
            list.Add(string.Format("DCparapet = {0:f3} x {1:f3} = {2:f3} k/ft = {2:f3} / {3} girders ", parapet_area, conc_wt, DCparapet, NMG));
            list.Add(string.Format(""));

            DCparapet = DCparapet / NMG;
            list.Add(string.Format("= {0:f3} k/ft/girder for one parapet ", DCparapet));
            list.Add(string.Format("Therefore, the effect of two parapets yields: "));

            DCparapet = DCparapet * 2;

            list.Add(string.Format("DCparapet = {0:f3} k/ft per girder", DCparapet));
            list.Add(string.Format(""));
            list.Add(string.Format("Future wearing surface "));
            list.Add(string.Format(""));

            double FWS_wt = MyList.StringToDouble(txt_Ana_FWS_Weight.Text, 0.0);
            list.Add(string.Format("Interior girder "));
            list.Add(string.Format(""));
            list.Add(string.Format("Weight/sq.ft = {0:f3} k/sq.ft ", FWS_wt));

            list.Add(string.Format("Width = {0:f3} ft. ", SMG));

            double DWFWS_I = FWS_wt * SMG;
            //list.Add(string.Format("DWFWS (I) = (0.030 x 9.667) = 0.290 k/ft/girder"));
            list.Add(string.Format("DWFWS (I) = ({0:f3} x {1:f3}) = {2:f3} k/ft/girder", FWS_wt, SMG, DWFWS_I));
            list.Add(string.Format(""));
            list.Add(string.Format("Exterior Girder "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Weight/sq.ft = 0.030 k/sq.ft"));
            list.Add(string.Format("Weight/sq.ft = {0:f3} k/sq.ft", FWS_wt));
            //list.Add(string.Format("Width = slab width - parapet width = 8.35 - 1.6875 = 6.663 ft. "));

            double parapet_wd = MyList.StringToDouble(txt_Ana_Parapet_Width.Text, 0.0);

            double fws_wd = slab_w_ext - parapet_wd;
            list.Add(string.Format(""));
            list.Add(string.Format("Width = slab width - parapet width = {0:f3} - {1:f3} = {2:f3} ft. ", slab_w_ext, parapet_wd, fws_wd));
            list.Add(string.Format(""));


            double DWFWS_E = FWS_wt * parapet_wd;
            list.Add(string.Format(""));
            //list.Add(string.Format("DWFWS (E) = 0.030(6.663) = 0.200 k/ft/girder"));
            list.Add(string.Format("DWFWS(E) = ({0:f3} x {1:f3}) = {2:f3} k/ft/girder", FWS_wt, parapet_wd, DWFWS_E));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));










            long_member_load.Add(string.Format("LOAD 1 DEAD LOAD SELF WEIGHT1"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DCgirder_I));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DCgirder_E));


            long_member_load.Add(string.Format("LOAD 2 DEAD LOAD SELF WEIGHT2"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DCgirder_I));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DCgirder_E));

            long_member_load.Add(string.Format("LOAD 3 DEAD LOAD DECK SLAB AND HAUNCH"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DCslab_I + DChaunch));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DCslab_E + DChaunch));

            long_member_load.Add(string.Format("LOAD 4 DEAD LOAD CONCRETE DIAPHRAGM "));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DCdiaphragm));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DCdiaphragm));


            #region Weight of Crash Barrier Footpath Parapet

            double pp_width = MyList.StringToDouble(txt_Ana_Parapet_Width.Text, 0.0);
            double pp_height = MyList.StringToDouble(txt_Ana_Parapet_Height.Text, 0.0);
            double pp_weight = pp_width * pp_height * Y_c / 10;

            double cb_width_LHS = MyList.StringToDouble(txt_Ana_Wc_LHS.Text, 0.0);
            double cb_height_LHS = MyList.StringToDouble(txt_Ana_Hc_LHS.Text, 0.0);

            double cb_width_RHS = MyList.StringToDouble(txt_Ana_Wc_RHS.Text, 0.0);
            double cb_height_RHS = MyList.StringToDouble(txt_Ana_Hc_RHS.Text, 0.0);

            double cb_weight_LHS = cb_width_LHS * cb_height_LHS * Y_c / 10;
            double cb_weight_RHS = cb_width_RHS * cb_height_RHS * Y_c / 10;


            double fp_width_LHS = MyList.StringToDouble(txt_Ana_Wf_LHS.Text, 0.0);
            double fp_height_LHS = MyList.StringToDouble(txt_Ana_Hf_LHS.Text, 0.0);
            double fp_width_RHS = MyList.StringToDouble(txt_Ana_Wf_RHS.Text, 0.0);
            double fp_height_RHS = MyList.StringToDouble(txt_Ana_Hf_RHS.Text, 0.0);

            double fp_weight_LHS = fp_width_LHS * fp_height_LHS * Y_c / 10;
            double fp_weight_RHS = fp_width_RHS * fp_height_RHS * Y_c / 10;

            wo5 = pp_weight + cb_weight_LHS + fp_weight_LHS + cb_weight_RHS + fp_weight_RHS;

            #endregion Weight of Crash Barrier Footpath Parapet


            long_member_load.Add(string.Format("LOAD 5 SIDL PARAPET, CRASH BARRIER AND FOOTPATH "));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5 / 2));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5 / 2));

            string LHS_outer = Bridge_Analysis.Get_LHS_Outer_Girder();
            string RHS_outer = Bridge_Analysis.Get_RHS_Outer_Girder();


            if (pp_weight != 0.0)
            {
                long_member_load.Add(string.Format("*Parapet Load {0} Ton/m", pp_weight));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, pp_weight));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, pp_weight));
            }

            if (cb_weight_LHS != 0.0)
            {
                long_member_load.Add(string.Format("*Crash Barier Load LHS {0} Ton/m", cb_weight_LHS));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, cb_weight_LHS));
            }

            if (cb_weight_RHS != 0.0)
            {
                long_member_load.Add(string.Format("*Crash Barier Load RHS {0} Ton/m", cb_weight_RHS));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, cb_weight_RHS));
            }

            if (fp_weight_LHS != 0.0)
            {
                long_member_load.Add(string.Format("*Footpath Load RHS {0} Ton/m", fp_weight_LHS));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", LHS_outer, fp_weight_LHS));
            }
            if (fp_weight_RHS != 0.0)
            {
                long_member_load.Add(string.Format("*Footpath Load RHS {0} Ton/m", fp_weight_RHS));
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", RHS_outer, fp_weight_RHS));
            }


            //long_member_load.Add(string.Format("LOAD 5 DEAD LOAD PARAPET"));
            //long_member_load.Add(string.Format("MEMBER LOAD"));
            ////long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5));
            ////long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5 / 2));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DCparapet));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DCparapet));


            long_member_load.Add(string.Format("LOAD 6 FUTURE WEARING SURFACE"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6 / 2));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, DWFWS_I));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, DWFWS_E));





            //member_load.Add(string.Format(""));
            txt_member_load.Lines = long_member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            if (Path.GetFileName(user_path) == Project_Name)
                File.WriteAllLines(Path.Combine(user_path, "Process\\Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }


        public void Calculate_Load_Computation1(string outer_girders, string inner_girders, List<string> joints_nos)
        {
            if (Bridge_Analysis._Outer_Girder_Support == null)
            {
                Bridge_Analysis.CreateData();
                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);
            }
            outer_girders = Bridge_Analysis._Outer_Girder_Support + " " + Bridge_Analysis._Outer_Girder_Mid;
            inner_girders = Bridge_Analysis._Inner_Girder_Support + " " + Bridge_Analysis._Inner_Girder_Mid;
            //Bridge_Analysis._
            List<string> list = new List<string>();
            List<string> long_member_load = new List<string>();

            //Bridge_Analysis

            double SMG, SCG, wi1, wi2, wi3, wi4, wi5, wi6, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for PSC I - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));


            //Self weight
            wi1 = (LG_INNER_MID.Girder_Section_A) * Y_c / 10;
            wo1 = (LG_OUTER_MID.Girder_Section_A) * Y_c / 10;


            long_member_load.Add(string.Format("LOAD 1 DEAD LOAD SELF WEIGHT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (Bridge_Analysis._Inner_Girder_Mid.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", Bridge_Analysis._Inner_Girder_Mid, wi1));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", Bridge_Analysis._Outer_Girder_Mid, wo1));


            wi1 = (LG_INNER_SUP.Girder_Section_A) * Y_c / 10;
            wo1 = (LG_OUTER_SUP.Girder_Section_A) * Y_c / 10;


            if (Bridge_Analysis._Inner_Girder_Support.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", Bridge_Analysis._Inner_Girder_Support, wi1));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", Bridge_Analysis._Outer_Girder_Support, wo1));



            //Deck Slab Load Dry Concrete
            wi2 = (SMG / 2) * Ds * Y_c / 10;
            wo2 = (CL + SMG / 2) * Ds * Y_c / 10;


            //Deck Slab Load Wet Concrete
            wi3 = (SMG / 2) * Ds * Y_c_wet / 10;
            wo3 = (CL + SMG / 2) * Ds * Y_c_wet / 10;

            //Shuttering

            //Crash Barrier
            //wi5 = wgc;


            wi4 = 0.0;
            wo4 = 0.0;

            wi5 = 0.0;
            wo5 = 0.0;

            // Wearing Coat
            //wi6 = (SMG / 2) * Ds * ils;
            wo6 = SCG * Dw * Y_w / 10;


            long_member_load.Add(string.Format("LOAD 2 DEAD LOAD DECK SLAB WET CONCRETE AND SHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi3 - wi4)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo3 - wo4)));

            long_member_load.Add(string.Format("LOAD 3 DEAD LOAD DESHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi3 + wi4)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo3 + wo4)));

            long_member_load.Add(string.Format("LOAD 4 DEAD LOAD SELF WEIGHT + DECK SLAB DRY CONCRETE"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            if (inner_girders.StartsWith("0") == false)
                long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi1 + wi2)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo1 + wo2)));


            long_member_load.Add(string.Format("LOAD 5 SIDL CRASH BARRIER"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5 / 2));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5 / 3));


            long_member_load.Add(string.Format("LOAD 6 SIDL WEARING COAT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6));
            //long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6 / 2));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6 / 3));



            #region Dead Load Value for DeckSlab analysis
            deck_member_load.Clear();
            deck_member_load.Add((wo1 + wo2) * 0.5);
            deck_member_load.Add((wo5 / 2) * 0.5);
            deck_member_load.Add((wo5 / 2 + wo6 / 2) * 0.5);
            #endregion Dead Load Value for DeckSlab analysis



            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            long_member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                long_member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));



            //member_load.Add(string.Format(""));
            txt_member_load.Lines = long_member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            if (Path.GetFileName(user_path) == Project_Name)
                File.WriteAllLines(Path.Combine(user_path, "Process\\Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }


        public double wi5 { get; set; }


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


                    if (!chk_ssprt_fixed_FX.Checked
                        || !chk_ssprt_fixed_FY.Checked
                        || !chk_ssprt_fixed_FZ.Checked
                        || !chk_ssprt_fixed_MX.Checked
                        || !chk_ssprt_fixed_MY.Checked
                        || !chk_ssprt_fixed_MZ.Checked)
                        kStr += " BUT";

                    if (!chk_ssprt_fixed_FX.Checked) kStr += " FX";
                    if (!chk_ssprt_fixed_FY.Checked) kStr += " FY";
                    if (!chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                    if (!chk_ssprt_fixed_MX.Checked) kStr += " MX";
                    if (!chk_ssprt_fixed_MY.Checked) kStr += " MY";
                    if (!chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
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
                    if (!chk_esprt_fixed_FX.Checked
                        || !chk_esprt_fixed_FY.Checked
                        || !chk_esprt_fixed_FZ.Checked
                        || !chk_esprt_fixed_MX.Checked
                        || !chk_esprt_fixed_MY.Checked
                        || !chk_esprt_fixed_MZ.Checked)
                        kStr += " BUT";
                    if (!chk_esprt_fixed_FX.Checked) kStr += " FX";
                    if (!chk_esprt_fixed_FY.Checked) kStr += " FY";
                    if (!chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                    if (!chk_esprt_fixed_MX.Checked) kStr += " MX";
                    if (!chk_esprt_fixed_MY.Checked) kStr += " MY";
                    if (!chk_esprt_fixed_MZ.Checked) kStr += " MZ";
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

        private void dgv_cable_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        public List<string> cab1, cab2, cab3, cab4;

        #region British Standard Loading

        public bool IsRead = false;
      
      

        #endregion British Standard Loading

        private void btn_edit_load_combs_Click(object sender, EventArgs e)
        {
            LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
            ff.Owner = this;
            ff.ShowDialog();
        }

        private void tc_limit_design_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Text_Changed();
            }
            catch (Exception exx) { }

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

                    iApp.user_path = user_path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    Analysis_Initialize_InputData(true);
                    Open_Project();
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
            else if (btn.Name == btn_psc_new_design.Name)
            {
                
                IsCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }
        #region Chiranjit [2016 09 07
        public void All_Button_Enable(bool flag)
        {
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
                return eASTRADesignType.PSC_I_Girder_Bridge_LS;
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

      
        private void uC_PierDesignLimitState1_OnProcess(object sender, EventArgs e)
        {
            Write_All_Data();
        }

        private void tc_AnaProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_AnaProcess.SelectedTab == tab_ana_process)
            {
                //if (!ucPreProcess1.IsFlag)
                //{
                //ucPreProcess1.FilePath = Get_LongGirder_File(cmb_long_open_file_process.SelectedIndex);
                //ucPreProcess1.Load_Initials();
                //ucPreProcess1.IsFlag = true;
                if (cmb_long_open_file_process.SelectedIndex == -1)
                {
                    if (cmb_long_open_file_process.Items.Count > 0)
                        cmb_long_open_file_process.SelectedIndex = 0;
                    //ucPostProcess1.Load_Initials(ucPreProcess1.FilePath);
                }
            }
        }
        public void Save_Input_Data()
        {
            return;
            List<string> list = new List<string>();

            File.WriteAllLines(FILE_BASIC_INPUT_DATA, list.ToArray());
            //System.Diagnostics.Process.Start(fname);
        }

        private void btn__Loadings_help_Click(object sender, EventArgs e)
        {
            string load_help = Path.Combine(Application.StartupPath, "\\ASTRAHelp\\AASHTO DESIGN LRFD Truck Load.pdf");


            load_help = Path.Combine(Application.StartupPath, @"ASTRAHelp\AASHTO DESIGN LRFD Truck Load.pdf");


            if (File.Exists(load_help)) System.Diagnostics.Process.Start(load_help);
        }

        private void btn_process_deck_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn == btn_process_deck)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Deck Design.xlsx");
                AASHTO_Design_PC_I_Girder.Process_Deck_Design(iApp, dgv_deck_input_data, Excel_File);
            }
            else if (btn == btn_process_steel_section)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC I Girder Design.xlsx");
                AASHTO_Design_PC_I_Girder.Process_PSC_I_Girder_Design(iApp, dgv_psc_i_girder_input_data, Excel_File);
            }
            else if (btn == btn_process_abutment)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Abutment Design.xlsx");
                AASHTO_Design_PC_I_Girder.Process_Abutment_and_Wingwall_Design(iApp, dgv_abutment_input_data, Excel_File);
            }
            else if (btn == btn_process_pier)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pier Design.xlsx");
                AASHTO_Design_PC_I_Girder.Process_Pier_Design(iApp, dgv_pier_input_data, Excel_File);
            }
            //else if (btn == btn_process_Foundation)
            //{
            //    var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Pile Foundation Design.xlsx");
            //    AASHTO_Design.Process_Pile_Foundation_Design_Design(iApp, dgv_foundation_input_data, Excel_File);
            //}
            else if (btn == btn_process_bearing)
            {
                var Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Bearing Design.xlsx");
                AASHTO_Design_PC_I_Girder.Process_Bearing_Design(iApp, dgv_bearing_input_data, Excel_File);
            }
        }

        private void btn_deck_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string Excel_File = "";

            if (btn == btn_deck_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD Deck Design.xlsx");
            }
            else if (btn == btn_steel_section_open)
            {
                Excel_File = Path.Combine(Worksheet_Folder, "AASHTO LRFD PSC I Girder Design.xlsx");
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
        }

        public void DataChange()
        {
            DataChange(dgv_deck_input_data);
            DataChange(dgv_psc_i_girder_input_data);
            DataChange(dgv_abutment_input_data);
            DataChange(dgv_pier_input_data);
            DataChange(dgv_bearing_input_data);
        }
        public void DataChange(DataGridView dgv)
        {
            Set_PSC_I_Girder_Inputs();
            //DataGridView dgv = dgv_deck_input_data;

            if (dgv == dgv_deck_input_data)
            {
                if (false)
                {
                    #region For Deck Slab

                    //1	Deck width:	Wdeck	46.875	ft
                    //dgv[2, 1].Value = Inputs.wdeck.ToString(); //Deck width:
                    ////2	Roadway width:	Wroadway	44	ft
                    //dgv[2, 2].Value = Inputs.wroadway.ToString(); //Roadway width:
                    ////3	Bridge length:	Ltotal	240	ft
                    //dgv[2, 3].Value = Inputs.LTotal.ToString(); // //Bridge length:
                    ////4	Skew Angle	Skew	0	degree
                    //dgv[2, 4].Value = Inputs.Skew.ToString(); //Skew Angle


                    //9	Steel density	Ws	0.49	kcf
                    //10	Concrete density	Wc	0.15	kcf
                    //11	Parapet weight (each)	Wpar	0.53	K/ft
                    //12	Future wearing surface	Wfws	0.14	kcf
                    //13	Future wearing surface thickness	tfws	2.5	in


                    //15	Girder spacing	S	9.75	ft
                    //var S = Inputs.S;
                    //dgv[2, 15].Value = S.ToString();

                    ////16	Number of girders	N	5	nos
                    //var N = Inputs.N;
                    //dgv[2, 16].Value = N.ToString();
                    ////17	Deck top cover	Covert	2.5	in
                    //var Covert = Inputs.Covert;
                    //dgv[2, 17].Value = Covert.ToString();
                    ////18	Deck bottom cover	Coverb	1	in
                    //var Coverb = Inputs.Coverb;
                    //dgv[2, 18].Value = Coverb.ToString();
                    ////19	Concrete density	Wc	0.15	kcf
                    //var Wc = Inputs.Wc;
                    //dgv[2, 18].Value = Wc.ToString();


                    ////20	Concrete 28day compressive strength	f'c	4	ksi
                    //var Fc = Inputs.fc;
                    //dgv[2, 20].Value = Fc.ToString();

                    ////24	Weight per foot:	Wpar	0.53	K/ft
                    //var Wpar = Inputs.Wpar;
                    //dgv[2, 24].Value = Wpar.ToString();
                    ////25	Width at base:	Wbase	1.4375	ft
                    //var Wbase = Inputs.wbase;
                    //dgv[2, 25].Value = Wbase.ToString();
                    ////27	Parapet height:	Hpar	3.5	ft
                    //var Hpar = Inputs.Hpar;
                    //dgv[2, 27].Value = Hpar.ToString();

                    ////33	Assume slab thicknesses	ts	8.5	in
                    //var ts = Inputs.tdeck;
                    //dgv[2, 33].Value = ts.ToString();
                    //34	Assume overhang thicknesses	to	9	in
                    //var _to = 9;
                    //dgv[2, 34].Value = _to.ToString();


                    #endregion For Deck Slab
                }
            }
            else if (dgv == dgv_psc_i_girder_input_data)
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
                //2	Concrete density:	Wc =	0.15	kcf
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
                //18	Deck overhang thickness:	to =	9	ft
                //var _to = 9;
                //dgv[2, 18].Value = _to;
                //19	Haunch thickness:	Hhnch =	3.5	in
                //var Hhnch = 3.5;
                //dgv[2, 14].Value = Hhnch;
                //20	Web depth:	Do =	66	in
                //var _Do = 66;
                //dgv[2, 14].Value = _Do;



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
        PSC_I_Girder_Inputs Inputs = new PSC_I_Girder_Inputs();
        public void Set_PSC_I_Girder_Inputs()
        {
        //    Inputs.txt_LSpan = txt_Ana_L;
        //    //Inputs.txt_LTotal = txt_amultiSpan;
        //    //Inputs.txt_Coverb = txt_Ana_coverb;
        //    //Inputs.txt_Covert = txt_Ana_covert;
        //    Inputs.txt_Ec = txt_Ana_Ec;
        //    Inputs.txt_Es = txt_Ana_Es;
        //    //Inputs.txt_fc = txt_Ana_fc;
        //    //Inputs.txt_Fu = txt_Ana_Fu;
        //    //Inputs.txt_Fy = txt_Ana_Fy;
        //    //Inputs.txt_fys = txt_Ana_fys;
        //    //Inputs.txt_Hpar = txt_Ana_Hpar;
        //    Inputs.txt_LSpan = txt_Ana_L;
        //    //Inputs.cmb_N = cmb_Ana_NMG;
        //    //Inputs.txt_n = txt_Ana_n;
        //    //Inputs.txt_S = txt_Ana_S;
        //    //Inputs.txt_Skew = txt_Ana_Skew;
        //    //Inputs.txt_Soverhang = txt_Ana_Soverhang;
        //    //Inputs.txt_tdeck = txt_Ana_tdeck;
        //    //Inputs.txt_tfws = txt_Ana_tfws;
        //    //Inputs.txt_wbase = txt_Ana_wbase;
        //    //Inputs.txt_wdeck = txt_Ana_wdeck;
        //    //Inputs.txt_wfws = txt_Ana_Wfws;
        //    //Inputs.txt_Wmisc = txt_Ana_Wmisc;
        //    //Inputs.txt_Wpar = txt_Ana_Wpar;
        //    //Inputs.txt_wroadway = txt_Ana_wroadway;
        //    //Inputs.txt_Ws = txt_Ana_Ws;
        //    //Inputs.txt_Wc = txt_Ana_Wc;


        //    Inputs.wbase = (Inputs.wdeck - Inputs.wroadway) / 2;
        //    Inputs.txt_wbase.ForeColor = Color.Blue;



        //    Inputs.S = (Inputs.wdeck - 2 * Inputs.Soverhang) / (Inputs.N - 1);
        //    Inputs.txt_S.ForeColor = Color.Blue;


        //    Inputs.Ec = 33000 * (Math.Pow(Inputs.Wc, 1.5) * Math.Sqrt(Inputs.fc));
        //    Inputs.txt_Ec.ForeColor = Color.Blue;


        //    Inputs.n = Math.Round((Inputs.Es / Inputs.Ec), 0);
        //    Inputs.txt_n.ForeColor = Color.Blue;


        }

        private void btn_deck_ws_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void btn_irc_view_moving_load_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;
                Write_All_Data(true);
                Analysis_Initialize_InputData(true);

                if (Path.GetFileName(user_path) != Project_Name) Create_Project();
                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);
                string usp = Path.Combine(user_path, "ANALYSIS PROCESS");
                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);

                LONG_GIRDER_LL_TXT();

                string inp_file = Path.Combine(usp, "INPUT_DATA.TXT");

                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;

                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");


                Bridge_Analysis.Skew_Angle = Ang;
                Bridge_Analysis.CreateData();


                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);


                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);
                Bridge_Analysis.WriteData_Total_Analysis(inp_file);

                ////txt_analysis_file.Text = Bridge_Analysis.Input_File;



                //Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                //Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, all_loads[0]);

                //Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);




                //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, DL_LL_Comb_Load_No);
                //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, DL_LL_Comb_Load_No);
                //Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, true, DL_LL_Comb_Load_No);



                //for (int i = 0; i < all_loads.Count; i++)
                //{
                //    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.GetAnalysis_Input_File(i + 3), all_loads[i]);
                //    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.GetAnalysis_Input_File(i + 3), true, false, i);
                ////}

                string ll_file = Bridge_Analysis.GetAnalysis_Input_File(cmb_irc_view_moving_load.SelectedIndex + 3);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(ll_file, all_loads[cmb_irc_view_moving_load.SelectedIndex]);
                Ana_Write_Long_Girder_Load_Data(ll_file, true, false, cmb_irc_view_moving_load.SelectedIndex);
                //iApp.View_MovingLoad(ll_file);

                iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_irc_vehicle_gap.Text));


            }
            catch (ThreadAbortException ex1) { MessageBox.Show(ex1.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
    public class AASHTO_PSC_I_Section
    {

        public double b_deck,b1, b2, b3, b4, b5, b6;
        public double d_deck,d1, d2, d3, d4, d5, d6;
        public AASHTO_PSC_I_Section()
        {
            b1 = 42;
            b2 = 4;
            b3 = 13;
            b4 = 8;
            b5 = 10;
            b6 = 28;


            d1 = 5;
            d2 = 3;
            d3 = 4;
            d4 = 42;
            d5 = 10;
            d6 = 8;
        }
        public double d7
        {
            get
            {
                return (d1 + d2 + d3 + d4 + d5 + d6);
            }
        }


        #region Area

        public double A_Deck
        {
            get
            {
                return (b_deck * d_deck);
            }
        }

        public double A1
        {
            get
            {
                return (b1 * d1);
            }
        }
        public double A2
        {
            get
            {
                return (0.5 * b3 * d2);
            }
        }
        public double A3
        {
            get
            {
                return ((b4 + b2) * d2);
            }
        }
        public double A4
        {
            get
            {
                return (0.5 * b2 * d3);
            }
        }
        public double A5
        {
            get
            {
                return (b4 * d3);
            }
        }
        public double A6
        {
            get
            {
                return (b4 * d4);
            }
        }
        public double A7
        {
            get
            {
                return (0.5 * b5 * d5);
            }
        }
        public double A8
        {
            get
            {
                return (b4 * d5);
            }
        }
        public double A9
        {
            get
            {
                return (b6 * d6);
            }
        }

        #endregion Area

        #region IXX
        public double IX_Deck
        {
            get
            {
                return (b_deck * Math.Pow(d_deck, 3)) / 12;
            }
        }

        public double Ix1
        {
            get
            {
                return b1 * Math.Pow(d1, 3) / 12.0;
            }
        }
        public double Ix2
        {
            get
            {
                return b3 * Math.Pow(d2, 3) / 36.0;
            }
        }
        public double Ix3
        {
            get
            {
                return (b4+b2) * Math.Pow(d2, 3) / 12.0;
            }
        }
        public double Ix4
        {
            get
            {
                return b2 * Math.Pow(d3, 3) / 36.0;
            }
        }
        public double Ix5
        {
            get
            {
                return b4 * Math.Pow(d3, 3) / 12.0;
            }
        }
        public double Ix6
        {
            get
            {
                return b4 * Math.Pow(d4, 3) / 12.0;
            }
        }
        public double Ix7
        {
            get
            {
                return b5 * Math.Pow(d5, 3) / 36.0;
            }
        }
        public double Ix8
        {
            get
            {
                return b4 * Math.Pow(d5, 3) / 12.0;
            }
        }
        public double Ix9
        {
            get
            {
                return b6 * Math.Pow(d6, 3) / 12.0;
            }
        }
        #endregion IXX

        #region IYY

        public double IY_Deck
        {
            get
            {
                return (d_deck * Math.Pow(b_deck, 3)) / 12;
            }
        }

        public double Iy1
        {
            get
            {
                return Math.Pow(b1, 3) * d1 / 12.0;
            }
        }
        public double Iy2
        {
            get
            {
                return Math.Pow(b3 / 2, 3) * d2 / 12.0;
            }
        }
        public double Iy3
        {
            get
            {
                return Math.Pow((b4+b2), 3) * d2 / 12.0;
            }
        }
        public double Iy4
        {
            get
            {
                return Math.Pow((b2/2), 3) * d3 / 36.0;
            }
        }
        public double Iy5
        {
            get
            {
                return Math.Pow(b4, 3) * d3 / 12.0;
            }
        }
        public double Iy6
        {
            get
            {
                return Math.Pow(b4, 3) * d4 / 12.0;
            }
        }
        public double Iy7
        {
            get
            {
                return Math.Pow(b5/2, 3) * d5 / 36.0;
            }
        }
        public double Iy8
        {
            get
            {
                return Math.Pow(b4, 3) * d5 / 12.0;
            }
        }
        public double Iy9
        {
            get
            {
                return Math.Pow(b6, 3) * d6 / 12.0;
            }
        }
        #endregion IYY

        public double A_Total_Composite
        {
            get
            {
                return (A_Deck + A1 + A2 + A3 + A4 + A5 + A6 + A7 + A8 + A9);
            }
        }
        public double A_Total_Non_Composite
        {
            get
            {
                return (A1 + A2 + A3 + A4 + A5 + A6 + A7 + A8 + A9);
            }
        }
        public double IX_Total_Composite
        {
            get
            {
                return (IX_Deck + Ix1 + Ix2 + Ix3 + Ix4 + Ix5 + Ix6 + Ix7 + Ix8 + Ix9);
            }
        }
        public double IX_Total_Non_Composite
        {
            get
            {
                return (Ix1 + Ix2 + Ix3 + Ix4 + Ix5 + Ix6 + Ix7 + Ix8 + Ix9);
            }
        }
        public double IY_Total_Composite
        {
            get
            {
                return (IY_Deck + Iy1 + Iy2 + Iy3 + Iy4 + Iy5 + Iy6 + Iy7 + Iy8 + Iy9);
            }
        }
        public double IY_Total_Non_Composite
        {
            get
            {
                return (Iy1 + Iy2 + Iy3 + Iy4 + Iy5 + Iy6 + Iy7 + Iy8 + Iy9);
            }
        }

        public double Area_in_FT
        {
            get
            {
                return A_Total_Composite / (12 * 12);
            }
        }

        public double Girder_Section_A
        {
            get
            {
                return A_Total_Non_Composite / (12 * 12);
            }
        }
        public double Composite_Section_A
        {
            get
            {
                return A_Total_Composite / (12 * 12);
            }
        }
        public double IXX_in_FT
        {
            get
            {
                return IX_Total_Composite / (12 * 12 * 12 * 12);
            }
        }

        public double Composite_Section_Ix
        {
            get
            {
                return IX_Total_Composite / (12 * 12 * 12 * 12);
            }
        }
        public double Girder_Section_Ix
        {
            get
            {
                return IX_Total_Non_Composite / (12 * 12 * 12 * 12);
            }
        }
        public double IYY_in_FT
        {
            get
            {
                return IY_Total_Composite / (12 * 12 * 12 * 12);
            }
        }

        public double Composite_Section_Iy
        {
            get
            {
                return IY_Total_Composite / (12 * 12 * 12 * 12);
            }
        }
        public double Girder_Section_Iy
        {
            get
            {
                return IY_Total_Non_Composite / (12 * 12 * 12 * 12);
            }
        }
        public double Composite_Section_Iz
        {
            get
            {
                return (Composite_Section_Ix + Composite_Section_Iy);
            }
        }
        public double Girder_Section_Iz
        {
            get
            {
                return (Girder_Section_Ix + Girder_Section_Iy);
            }
        }

    }
    public class PSC_I_Girder_Analysis_AASHTO
    {


        IApplication iApp;


        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Analysis = null;



        public List<BridgeMemberAnalysis> All_Analysis = null;




        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;



        //Chiranjit [2012 12 18]
        public AASHTO_PSC_I_Section Long_Inner_Mid_Section { get; set; }
        public AASHTO_PSC_I_Section Long_Inner_Support_Section { get; set; }
        public AASHTO_PSC_I_Section Long_Outer_Mid_Section { get; set; }
        public AASHTO_PSC_I_Section Long_Outer_Support_Section { get; set; }
        public AASHTO_Design_PC_I_Girder_CS Cross_End_Section { get; set; }
        public AASHTO_Design_PC_I_Girder_CS Cross_Intermediate_Section { get; set; }


        //Chiranjit [2013 06 06] Kolkata

        public string List_Envelop_Inner { get; set; }
        public string List_Envelop_Outer { get; set; }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        protected int _Columns = 0, _Rows = 0;

        protected double span_length = 0.0;


        string input_file, working_folder, user_path;
        public PSC_I_Girder_Analysis_AASHTO(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
            NMG = 7;
            Total_Columns = 11;
            Total_Rows = 11;

            All_Analysis = new List<BridgeMemberAnalysis>();
        }

        #region Properties
        /// <summary>
        /// Elustic Modulus of Steel
        /// </summary>
        public double Es { get; set; }
        /// <summary>
        /// Elustic Modulus of Deck
        /// </summary>
        public double Ec { get; set; }
        /// <summary>
        /// Elustic Modulus of Girder
        /// </summary>
        public double Ecm { get; set; }
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


        public double Support_Distance { get; set; }
        public double End_Varring_Distance { get; set; }


        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }

        public int Total_Rows { get; set; }
        public int Total_Columns { get; set; }

        public double Skew_Angle { get; set; }

        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }

        public double Spacing_Long_Girder
        {
            get
            {
                //return Math.Abs(MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / (NMG - 1)).ToString("0.000"), 0.0));
                //Chiranjit [2013 05 05]
                return Math.Abs(MyList.StringToDouble(((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (NMG - 1)).ToString("0.000"), 0.0));
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                double val = 0.0;

                val = (Length - 2 * Effective_Depth) / (NCG - 1);

                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }


        #endregion Properties
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(working_folder)) return "";
                return Path.Combine(working_folder, "ASTRA_DATA_FILE.TXT");

            }
        }

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
                //if (File.Exists(value))
                user_path = Path.GetDirectoryName(input_file);
                working_folder = user_path;
            }
        }
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "DL + LL Combine Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DL_LL_Combine_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string TempAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    string pd = Path.Combine(working_folder, "Temp Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Temp_Input_File.txt");
                }
                return "";
            }
        }


        public string GetAnalysis_Input_File(int analysis_no)
        {

            if (Directory.Exists(user_path))
            {

                if (analysis_no == 0) return DeadLoadAnalysis_Input_File;
                if (analysis_no == 1) return TotalAnalysis_Input_File;
                if (analysis_no == 2) return LiveLoadAnalysis_Input_File;

                string pd = "Live Load Analysis Load " + (analysis_no - 2);
                pd = Path.Combine(working_folder, pd);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_Load_" + (analysis_no - 2) + "_Input_File" + ".txt");
            }
            return "";
        }

        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "Live Load Analysis");
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
                if (Directory.Exists(working_folder))
                {
                    string pd = Path.Combine(working_folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string Total_Analysis_Report
        {
            get
            {
                if (File.Exists(TotalAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(TotalAnalysis_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }
        public string LiveLoad_Analysis_Report
        {
            get
            {
                if (File.Exists(LiveLoadAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(LiveLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }
        public string DeadLoad_Analysis_Report
        {
            get
            {
                if (File.Exists(DeadLoadAnalysis_Input_File))
                    return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
                return "";
            }
        }

        #endregion Analysis Input File

        public int NoOfInsideJoints
        {
            get
            {
                return 1;
            }
        }
        public double L1 { get { return 0.0; } }
        public double L2 { get { return Length * (0.009090909); } }
        public double L3 { get { return Length * (0.05); } }
        public double L4 { get { return Length * (0.1); } }
        public double L5 { get { return Length * (0.15); } }
        public double L6 { get { return Length * (0.2); } }
        public double L7 { get { return Length * (0.25); } }
        public double L8 { get { return Length * (0.3); } }
        public double L9 { get { return Length * (0.35); } }
        public double L10 { get { return Length * (0.4); } }
        public double L11 { get { return Length * (0.45); } }
        public double L12 { get { return Length * (0.495454545); } }
        public double L13 { get { return Length * (0.5); } }
        public double L14 { get { return Length * (0.55); } }
        public double L15 { get { return Length * (0.6); } }
        public double L16 { get { return Length * (0.65); } }
        public double L17 { get { return Length * (0.7); } }
        public double L18 { get { return Length * (0.75); } }
        public double L19 { get { return Length * (0.8); } }
        public double L20 { get { return Length * (0.85); } }
        public double L21 { get { return Length * (0.9); } }
        public double L22 { get { return Length * (0.95); } }
        public double L23 { get { return Length * (0.981818182); } }
        public double L24 { get { return Length * (0.990909091); } }
        public double L25 { get { return Length; } }
       
      
        public double NMG { get; set; }
        public double NCG { get; set; }

        public double Mid_Span_Length { get { return Length / 2.0; } }
        public double Penultimate_Span_Length { get { return Length / 4.0; } }


        #region Chiranjit [2013 05 06]
        //Chiranjit [2013 05 06]
        public List<string> joints_list_for_load = new List<string>();

        //Chiranjit [2013 05 06]
        public string support_left_joints = "";
        public string support_right_joints = "";

        public string L2_Girders_as_String { get; set; }
        public string L4_Girders_as_String { get; set; }
        public string Deff_Girders_as_String { get; set; }
        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }
        public string Cross_Girders_as_String { get; set; }


        public List<int> _L2_inn_joints = new List<int>();
        public List<int> _L4_inn_joints = new List<int>();
        public List<int> _deff_inn_joints = new List<int>();

        public List<int> _L2_out_joints = new List<int>();
        public List<int> _L4_out_joints = new List<int>();
        public List<int> _deff_out_joints = new List<int>();

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


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            Inner_Girders_as_String = MyList.Get_Array_Text(Inner_Girder);
            Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);

        }

        void Set_Girders()
        {


            List<int> L2_Girders = new List<int>();
            List<int> L4_Girders = new List<int>();
            List<int> Deff_Girders = new List<int>();
            List<int> Cross_Girder = new List<int>();


            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    if (_L2_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L2_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L2_Girders.Add(MemColls[i].MemberNo);
                    }
                    else if (_L4_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                       _L4_out_joints.Contains(MemColls[i].StartNode.NodeNo))
                    {
                        L4_Girders.Add(MemColls[i].MemberNo);
                    }
                    //else if (_deff_inn_joints.Contains(MemColls[i].StartNode.NodeNo) ||
                    //   _deff_inn_joints.Contains(MemColls[i].StartNode.NodeNo))
                    //{
                    //    Deff_Girders.Add(MemColls[i].MemberNo);
                    //}
                    else
                    {
                        Deff_Girders.Add(MemColls[i].MemberNo);
                    }

                }
            }
            L2_Girders.Sort();
            L4_Girders.Sort();
            Deff_Girders.Sort();

            Cross_Girder.Sort();


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            L2_Girders_as_String = MyList.Get_Array_Text(L2_Girders);
            L4_Girders_as_String = MyList.Get_Array_Text(L4_Girders);
            Deff_Girders_as_String = MyList.Get_Array_Text(Deff_Girders);
            //Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);
            Set_Inner_Outer_Cross_Girders();
        }

        void Set_L2_L4_Deff_Girders()
        {
            double L = Length;
            double W = WidthBridge;
            double val = L / 2;
            int i = 0;

            _L2_inn_joints.Clear(); ;
            _L4_inn_joints.Clear(); ;
            _deff_inn_joints.Clear(); ;

            _L2_out_joints.Clear(); ;
            _L4_out_joints.Clear(); ;
            _deff_out_joints.Clear(); ;





            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < Joints.Count; i++)
            {
                if (_X_joints.Contains(Joints[i].X) == false)
                    _X_joints.Add(Joints[i].X);
                if (_Z_joints.Contains(Joints[i].Z) == false)
                    _Z_joints.Add(Joints[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Effective_Depth;


            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            iApp.SetProgressValue(20, 100);
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < Joints.Count; i++)
                {
                    //if (_X_joints.Contains(Joints[i].X) == false) _X_joints.Add(Joints[i].X);
                    //if (_Z_joints.Contains(Joints[i].Z) == false) _Z_joints.Add(Joints[i].Z);

                    if (_Z_joints[zc] == Joints[i].Z)
                    {
                        if (x_min > Joints[i].X)
                            x_min = Joints[i].X;
                        if (x_max < Joints[i].X)
                            x_max = Joints[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }

            val = Effective_Depth;

            double cant_wi_left = Width_LeftCantilever;
            double cant_wi_right = Width_RightCantilever;

            for (i = 0; i < Joints.Count; i++)
            {
                try
                {
                    //if ((Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right)) == false) continue;


                    x_min = _X_min[_Z_joints.IndexOf(Joints[i].Z)];

                    if ((Joints[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left && Joints[i].Z <= (W - cant_wi_right))
                        _L2_inn_joints.Add(Joints[i].NodeNo);
                    }


                    if (Joints[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _L4_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if (Joints[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _L4_out_joints.Add(Joints[i].NodeNo);
                    }


                    if ((Joints[i].X.ToString("0.0") == (Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z >= cant_wi_left)
                        _deff_inn_joints.Add(Joints[i].NodeNo);
                    }
                    if ((Joints[i].X.ToString("0.0") == (L - Effective_Depth + x_min).ToString("0.0")))
                    {
                        //if (Joints[i].Z <= (W - cant_wi_right))
                        _deff_out_joints.Add(Joints[i].NodeNo);
                    }
                }
                catch (Exception ex) { }
            }


            if (_L2_inn_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    _L2_out_joints.Add(_L2_inn_joints[0]);
                    _L2_inn_joints.RemoveAt(0);
                }
                _L2_out_joints.Add(_L2_inn_joints[0]);
                _L2_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {

                    _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                    _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
                }
                _L2_out_joints.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);
                _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            }

            List<int> temp_joints = new List<int>();


            if (_L4_inn_joints.Count > 2)
            {

                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L4_inn_joints[0]);
                    _L4_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_L4_inn_joints[0]);
                _L4_inn_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);
                    _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
                }
                temp_joints.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);
                _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
            }

            if (_L4_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_L4_out_joints[0]);
                    _L4_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_L4_out_joints[0]);
                _L4_out_joints.RemoveAt(0);

                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_L4_out_joints[_L4_out_joints.Count - 1]);
                    _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
                }
                temp_joints.Add(_L4_out_joints[_L4_out_joints.Count - 1]);
                _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
            }
            _L4_inn_joints.AddRange(_L4_out_joints.ToArray());

            _L4_out_joints.Clear();
            _L4_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();

            if (_deff_inn_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_deff_inn_joints[0]);
                    _deff_inn_joints.RemoveAt(0);
                }
                temp_joints.Add(_deff_inn_joints[0]);
                _deff_inn_joints.RemoveAt(0);
                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_deff_inn_joints[_deff_inn_joints.Count - 1]);
                    _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
                }
                temp_joints.Add(_deff_inn_joints[_deff_inn_joints.Count - 1]);
                _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
            }
            if (_deff_out_joints.Count > 2)
            {
                if (Width_LeftCantilever > 0)
                {
                    temp_joints.Add(_deff_out_joints[0]);
                    _deff_out_joints.RemoveAt(0);
                }
                temp_joints.Add(_deff_out_joints[0]);
                _deff_out_joints.RemoveAt(0);
                if (Width_RightCantilever > 0)
                {
                    temp_joints.Add(_deff_out_joints[_deff_out_joints.Count - 1]);
                    _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
                }
                temp_joints.Add(_deff_out_joints[_deff_out_joints.Count - 1]);
                _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
            }

            _deff_inn_joints.AddRange(_deff_out_joints.ToArray());

            _deff_out_joints.Clear();
            _deff_out_joints.AddRange(temp_joints.ToArray());
            temp_joints.Clear();
            Set_Girders();
        }

        #endregion Chiranjit [2013 05 06]



        public string _DeckSlab { get; set; }
        public string _Inner_Girder_Mid { get; set; }
        public string _Inner_Girder_Support { get; set; }
        public string _Outer_Girder_Mid { get; set; }
        public string _Outer_Girder_Support { get; set; }
        public string _Cross_Girder_Inter { get; set; }
        public string _Cross_Girder_End { get; set; }
        public void CreateData()
        {
            CreateData(true);
        }
        public void CreateData(bool isPSC_I_Girder)
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




            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data
           
            list_x.Clear();
            list_x.Add(L1);
            last_x = L2;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L3;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L4;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L5;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L6;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L7;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = L8;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = L9;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            last_x = L10;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = L11;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L12;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L13;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L14;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = L15;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L16;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L17;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L18;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L19;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = L20;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L21;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L22;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L23;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L24;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = L25;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = x_incr;

            int i = 0;
            bool flag = true;
          
            list_x.Sort();

            list_z.Add(0);
            last_z = Width_LeftCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);


            last_z = WidthBridge - Width_RightCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

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

                if (z_incr == 0.0) break;

            } while (last_z <= WidthBridge);

            list_z.Sort();
            #endregion Chiranjit [2011 09 23] Correct Create Data



            _Columns = list_x.Count;
            _Rows = list_z.Count;
            Total_Rows = _Rows;
            Total_Columns = _Columns;

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

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();


            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                    if (iCols == 0)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1)
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
            Set_L2_L4_Deff_Girders();
        }

        public virtual void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
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
            list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
            list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
            list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
            list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
            list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
            list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
            list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
            list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
            list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
            list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            list.Add("MATERIAL CONSTANT");
            list.Add("E 3896 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add("3 5 7 9  PINNED");
            list.Add("113 115 117 119  PINNED");
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
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);

        }

        public virtual void WriteData_Total_Analysis(string file_name)
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






            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
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

            _DeckSlab = MyList.Get_Array_Text(DeckSlab);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder_Mid);
            _Inner_Girder_Support = MyList.Get_Array_Text(Inner_Girder_Support);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder_Mid);
            _Outer_Girder_Support = MyList.Get_Array_Text(Outer_Girder_Support);
            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder_Inter);
            _Cross_Girder_End = MyList.Get_Array_Text(Cross_Girder_End);


            list.Add("SECTION PROPERTIES");
            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANTS");
            //list.Add("E 2.85E6 ALL");
            list.Add("E " + Ecm + " ALL");
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
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }

        private void Set_Section_Properties(List<string> list)
        {

            list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                _DeckSlab,
                Ds));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Cross_Girder_End,
                Cross_End_Section.Composite_Section_A,
                Cross_End_Section.Composite_Section_Ix,
                Cross_End_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Cross_Girder_Inter,
                Cross_Intermediate_Section.Composite_Section_A,
                Cross_Intermediate_Section.Composite_Section_Ix,
                Cross_Intermediate_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Outer_Girder_Support,
                Long_Outer_Support_Section.Composite_Section_A,
                Long_Outer_Support_Section.Composite_Section_Ix,
                Long_Outer_Support_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Outer_Girder_Mid,
                Long_Outer_Mid_Section.Composite_Section_A,
                Long_Outer_Mid_Section.Composite_Section_Ix,
                Long_Outer_Mid_Section.Composite_Section_Iz));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Inner_Girder_Support,
                Long_Inner_Support_Section.Composite_Section_A,
                Long_Inner_Support_Section.Composite_Section_Ix,
                Long_Inner_Support_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Inner_Girder_Mid,
                Long_Inner_Mid_Section.Composite_Section_A,
                Long_Inner_Mid_Section.Composite_Section_Ix,
                Long_Inner_Mid_Section.Composite_Section_Iz));

        }
        public virtual void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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
            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);

            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            //list.Add("E " + Ecm * 100 + " ALL");
            list.Add("E " + Ecm + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
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
        public virtual void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
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
            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            //list.Add("E " + Ecm * 100 + " ALL");
            list.Add("E " + Ecm + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
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
            MemberCollection mc = new MemberCollection(Analysis.Analysis.Members);

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

            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            z_min = Analysis.Analysis.Joints.MinZ;
            double z_max = Analysis.Analysis.Joints.MaxZ;


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

        public string Get_LHS_Outer_Girder()
        {
            string LHS = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, _Columns - 2].MemberNo;
            return LHS;

        }
        public string Get_RHS_Outer_Girder()
        {
            string RHS = Long_Girder_Members_Array[_Rows - 3, 0].MemberNo + " TO " + Long_Girder_Members_Array[_Rows - 3, _Columns - 2].MemberNo;
            return RHS;
        }

    }


    public class AASHTO_Design_PC_I_Girder
    {

        #region Set_Input Data

        public static void Input_Positive_Moment_Region_Section_Properties(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Positive_Moment_Region_Section_Properties

            list.Add(string.Format("Girder only:$$$$$$"));
            list.Add(string.Format("Top flange$8.75$55.188$482.9$0.3$7530.2$7530.5"));
            list.Add(string.Format("Web$27$27.875$752.6$6561$110.5$6671.5"));
            list.Add(string.Format("Bottom Flange$12.25$0.438$5.4$0.8$7912$7912.7"));
            list.Add(string.Format("Total$48$25.852$1240.9$6562.1$15552.7$22114.7"));
            list.Add(string.Format("Composite (3n):$$$$$$"));
            list.Add(string.Format("Girder$48$25.852$1240.9$22114.8$11134.4$33249.2"));
            list.Add(string.Format("Slab$34.333$62.375$2141.5$183.1$15566.5$15749.6"));
            list.Add(string.Format("Total$82.333$41.082$3382.4$22297.9$26700.9$48998.8"));
            list.Add(string.Format("Composite (n):$$$$$$"));
            list.Add(string.Format("Girder$48$25.852$1240.9$22114.8$29792.4$51907.2"));
            list.Add(string.Format("Slab$103$62.375$6424.6$549.3$13883.8$14433.2"));
            list.Add(string.Format("Total$151$50.765$7665.5$22664.1$43676.2$66340.4"));
            //list.Add(string.Format("Section$ybotgdr$ytopgdr$ytopslab$Sbotgdr$Stopgdr$Stopslab"));
            //list.Add(string.Format("$(Inches)$(Inches)$(Inches)$(Inches3)$(Inches3)$(Inches3)"));
            //list.Add(string.Format("Girder only$25.852$29.648$---$855.5$745.9$---"));
            //list.Add(string.Format("Composite (3n)$41.082$14.418$25.293$1192.7$3398.4$1937.2"));
            //list.Add(string.Format("Composite (n)$50.765$4.735$15.61$1306.8$14010.3$4249.8"));

            #endregion Positive_Moment_Region_Section_Properties

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }

        public static void Input_Negative_Moment_Region_Section_Properties(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Negative_Moment_Region_Section_Properties


            list.Add(string.Format("Girder only:$$$$$$"));
            list.Add(string.Format("Top flange$35$58$2030$18.2$30009.7$30027.9"));
            list.Add(string.Format("Web$27$29.75$803.3$6561$28.7$6589.7"));
            list.Add(string.Format("Bottom flange$38.5$1.375$52.9$24.3$28784.7$28809"));
            list.Add(string.Format("Total$100.5$28.718$2886.2$6603.5$58823.1$65426.6"));
            list.Add(string.Format("Composite (deck concrete using 3n):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$8226.9$73653.5"));
            list.Add(string.Format("Slab$34.333$64.25$2205.9$183.1$24081.6$24264.7"));
            list.Add(string.Format("Total$134.833$37.766$5092.1$65609.7$32308.5$97918.2"));
            list.Add(string.Format("Composite (deck concrete using n):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$32504.5$97931.2"));
            list.Add(string.Format("Slab$103$64.25$6617.8$549.3$31715.6$32264.9"));
            list.Add(string.Format("Total$203.5$46.703$9504$65975.9$64220.1$130196.1"));
            list.Add(string.Format("Composite (deck reinforcement only):$$$$$$"));
            list.Add(string.Format("Girder$100.5$28.718$2886.2$65426.6$1568.1$66994.7"));
            list.Add(string.Format("Deck reinf.$12.772$63.75$814.2$0$12338.7$12338.7"));
            list.Add(string.Format("Total$113.272$32.668$3700.4$65426.6$13906.8$79333.4"));


            //list.Add(string.Format("Section$ybotgdr$ytopgdr$ytopslab$Sbotgdr$Stopgdr$Stopslab"));
            //list.Add(string.Format("$(Inches)$(Inches)$(Inches)$(Inches3)$(Inches3)$(Inches3)"));
            //list.Add(string.Format("Girder only$28.718$30.532$---$2278.2$2142.9$---"));
            //list.Add(string.Format("Composite (3n)$37.766$21.484$30.484$2592.8$4557.7$3212.1"));
            //list.Add(string.Format("Composite (n)$46.702$12.548$21.548$2787.8$10376.2$6042.3"));
            //list.Add(string.Format("Composite (rebar)$32.668$26.582$31.082$2428.5$2984.5$2552.4"));

            #endregion Negative_Moment_Region_Section_Properties

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }

        public static void Input_Deck_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Deck Inputs


            list.Add("Required information:$$$");

            list.Add("Width:$$55.375$ft");
            list.Add("Roadway Width$B$52$ft");
            list.Add("Distance from Outer girder cl to parapet edge$L2 $1.83$ft");
            list.Add("Railings wide at the base$L3$1.69$ft");
            list.Add("Deck slab thickness$d1$8$in");
            list.Add("Overhang slab thickness$d2$9$in");
            list.Add("Girder spacing$L1$9.66$ft");
            list.Add("Top cover $tc$2.5$in.");

            list.Add("Bottom cover $$1$in.");
            list.Add("Steel yield strength $$60$ ksi");
            list.Add("Slab conc. compressive strength $$4$ksi");
            list.Add("Concrete density $$150$pcf");
            list.Add("Future wearing surface density$$30$ psf");


            list.Add("Concrete Parapet General Values and Dimensions:$$$");
            list.Add("Weight per unit length $$650$ lb/ft");
            list.Add("Width at base $$1.687$ft");
            list.Add("Moment capacity at the base calculated$$$");
            list.Add("assuming the parapet acts as a vertical$$$");
            list.Add("cantilever, Mc/length $$17.83$ k-ft/ft");
            list.Add("Parapet height, H $$42$ in.");
            list.Add("Length of parapet failure mechanism, Lc $$235.2$in.");
            list.Add("Collision load capacity, Rw $$137.22$k");

            list.Add("Future wearing surface = $$30$psf");

            list.Add("Girder top flange width = $$42$in.");

            list.Add("Assumed loads$$$");

            list.Add("Self weight of the slab in the overhang area =$$0.1125$k/ft2");

            list.Add("Weight of parapet =$$0.65$ k/ft");

            list.Add("Future wearing surface = $$0.03$ k/ft2");
            #endregion Deck Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv, 2);

            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        }
        public static void Input_PSC_I_Girder_Data(DataGridView dgv)
        {
            List<string> list = new List<string>();

            #region Inputs
            list.Add("Required information:$$$");
            list.Add("AASHTO Type I-Beam (28/72)$$$");
            list.Add("Noncomposite beam area, Ag$$1,085$in^2");
            list.Add("Noncomposite beam moment of inertia, Ig$$733,320$in^4");
            list.Add("Deck slab thickness, ts$$8$in.");
            list.Add("Span length, L$$110$ft.");
            list.Add("Girder spacing, S$$9.666$");
            list.Add("Modulus of elasticity of the beam, EB$$4,696$ksi");
            list.Add("Modulus of elasticity of the deck, ED$$3,834$ksi");
            list.Add("C.G. to top of the basic beam$$35.62$in.");
            list.Add("C.G. to bottom of the basic beam$$36.38$in.");
            
            list.Add("overhang width =$$3.521$ft");
            list.Add("Haunch weight$$$");
            list.Add("Width$$42$in.");
            list.Add("Thickness$$4$in.");
            list.Add("Location of the diaphragms:$$$");
            list.Add("Span 1 (from centerline of end bearing)$$54.5$ft.");
            list.Add("Span 2 (from centerline of pier)$$55.5$ft.");
            list.Add("diaphragm thickness =$$10$in");
            list.Add("parapet width =$$1.6875$ft");
            list.Add("Parapet cross-sectional area =$$4.33$sq.ft");
            list.Add("Future wearing surface$$$");
            list.Add("Interior girder$$$");
            list.Add("Weight/ft2$$0.03$k/sq.ft");
            list.Add("Exterior Girder$$$");
            list.Add("Weight/sq.ft$$0.03$k/sq.ft");
            
            list.Add("$Nstrands$44$");
            list.Add("$Aps$0.153$");
            list.Add("$e54.5'$31.38$");
            list.Add("$Mg$20142$");
            list.Add("$Eci$4200$");
            list.Add("$Ep$28500$");
            list.Add("For the midspan section$$$");
            list.Add("girder depth =$$72$in");
            list.Add("structural slab thickness =$$7.5$in");
            
            
            list.Add("Initial prestressing force =$$924.4$ k");
            
            list.Add("Distance from bottom of the beam to the neutral axis = $$36.38$ in.");
            
            list.Add("Distance from the bottom of the beam to the centroid of Group 1 strands = $$5.375$in.");
            
            list.Add("modulus of elasticity of mild reinforcement =$$29,000$ksi");
            list.Add("area of prestressing steel =$$4.896$ in2");
            list.Add("modulus of elasticity  =$$28,500$ ksi");
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

            list.Add(string.Format("Dry unit weight:$?dry =$90$PCF"));


            list.Add(string.Format("Wet unit weight:$?wet =$110$PCF"));


            list.Add(string.Format("Unit weight of water:$?water =$62.4$PCF"));


            list.Add(string.Format("Design Step P.4 - Verify Need for a Pile Foundation$$$"));

            list.Add(string.Format("Maximum possible length of footing$L =$46.875$ft"));



            list.Add(string.Format("Preliminary minimum required width$Bmin =$12.736$ft"));



            list.Add(string.Format("Modulus of elasticity of soil, from Design Step P.1:$Es =$60$TSF"));
            #endregion Inputs

            MyList.Fill_List_to_Grid(dgv, list, '$');
            MyList.Modified_Cell(dgv);
        }
        public static void Process_Deck_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder AASHTO LRFD\04 Design of AASHTO LRFD Deck.xlsx");

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

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);


            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["1.0 Input"];
            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Introduction"];
            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets[0];

            //Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(0);


            //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
            //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
            //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

            Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

            Excel.Worksheet EXL_INP;

            eui.Read_From_Grid(dgv);

            #region Ref Cells
            List<string> list = new List<string>();
            //list.Add("Step 4.1");
            list.Add("E68");
            list.Add("E69");
            list.Add("E70");
            list.Add("E71");
            list.Add("E72");
            list.Add("E73");
            list.Add("E74");
            list.Add("E75");
            //list.Add("E76");
            list.Add("E77");
            list.Add("E78");
            list.Add("E79");
            list.Add("E80");
            list.Add("E81"); //14
            //list.Add("Step 4.4");
            list.Add("E8");
            list.Add("E9");
            list.Add("E12");
            list.Add("E13");
            list.Add("E14");
            list.Add("E15"); //20
            //list.Add("Step 4.5");
            list.Add("C53"); //21
            //list.Add("Step 4.6");
            list.Add("C7"); //22
            //list.Add("Step 4.10");
            list.Add("C50");
            list.Add("C52");
            list.Add("C54");
            #endregion Ref Cells

            try
            {
                string val = "";
                EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4.1"];

                for (int i = 0; i < 13; i++)
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


                EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4.4"];

                for (int i = 13; i < 19; i++)
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

                EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4.5"];

                for (int i = 19; i < 20; i++)
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
                EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4.6"];

                for (int i = 20; i < 21; i++)
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
                EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 4.10"];

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
            iApp.Excel_Close_Message();
            myExcelWorkbook.Save();
            MyList.releaseObject(myExcelWorkbook);
        }
        public static void Process_PSC_I_Girder_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {
            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder AASHTO LRFD\05 Design of AASHTO LRFD PSC I-Girder.xlsx");

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

            myExcelApp = new Excel.Application();
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

                List<string> list = new List<string>();
                #region Cell Ref
                //list.Add("Step 5.1");
                list.Add("G52");
                list.Add("G53");
                list.Add("G54");
                list.Add("G55");
                list.Add("G56");
                list.Add("G57");
                list.Add("G58");
                list.Add("G59");
                list.Add("G60"); //9
                //list.Add("Step 5.2");
                list.Add("B45");
                list.Add("D60");
                list.Add("D62");
                list.Add("B84");
                list.Add("B85");
                list.Add("C87");
                list.Add("C105");
                list.Add("C106");
                list.Add("C124");
                list.Add("C134"); //19
                //list.Add("Step 5.4");
                list.Add("D105");
                list.Add("D106");
                list.Add("D110");
                list.Add("D112");
                list.Add("D113");
                list.Add("D114"); // 25
                //list.Add("Step 5.5");
                list.Add("E48");
                list.Add("E49"); // 27
                //list.Add("Step 5.6");
                list.Add("H1398");
                list.Add("H1400");
                list.Add("H1402"); // 30
                //list.Add("Step 5.7");
                list.Add("G340");
                list.Add("G341");
                list.Add("G342");
                #endregion 
                try
                {
                    string val = "";

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.1"];
                    for (int i = 0; i < 9; i++)
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

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.2"];
                    for (int i = 9; i < 19; i++)
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

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.4"];
                    for (int i = 19; i < 25; i++)
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




                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.5"];
                    for (int i = 25; i < 27; i++)
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




                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.6"];
                    for (int i = 27; i < 30; i++)
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

                    EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Step 5.7"];
                    for (int i = 30; i < list.Count; i++)
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

            myExcelApp = new Excel.Application();
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

            myExcelApp = new Excel.Application();
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

            myExcelApp = new Excel.Application();
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
        public static void Process_Pile_Foundation_Design_Design(IApplication iApp, DataGridView dgv, string Excel_File)
        {

            string file_path = Excel_File;

            if (!Directory.Exists(Path.GetDirectoryName(file_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Composite AASHTO LRFD\Design Step P Pile Foundation Design.xlsx");

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

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            try
            {
                //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
                Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets[0];

                //Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.1 DL SuperStructure"];
                //Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["3.2 SIDL"];
                //Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["4.1 LiveLoad"];

                Excel_AASHTO_Data eui = new Excel_AASHTO_Data();

                eui.Read_From_Grid(dgv);

                List<double> data = new List<double>();
                try
                {
                    string kStr = "";
                    foreach (var item in eui)
                    {
                        try
                        {

                            EXL_INP.get_Range(item.Excel_Cell_Reference).Formula = item.Input_Value;
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

    public class AASHTO_Design_PC_I_Girder_CS
    {

        //Width of Slab		W	=
        //Depth of slab		Ds	=
        //Width of top flange		wtf	=
        //Width of bottom flange		bwf	=
        //Width of web		bw	=
        //Thickness of top flange		D1	=
        //Thickness of top haunch		D2	=
        //Thickness of bottom haunch		D3	=
        //Thickness of bottom flange		D4	=
        //Depth of girder		d	=
        //Modulus of Elasticity of Deck = Es_Deck
        //Modulus of Elasticity of Girder = Es_Girder


        public double W, Ds, wtf, bwf, bw, D1, D2, D3, D4, d, Es_Deck, Es_Girder;
        public bool Is_End_Cross_Girder { get; set; }
        public AASHTO_Design_PC_I_Girder_CS()
        {
            W = 0.0;
            Ds = 0.0;
            wtf = 0.0;
            bwf = 0.0;
            bw = 0.0;
            D1 = 0.0;
            D2 = 0.0;
            D3 = 0.0;
            D4 = 0.0;
            d = 0.0;

            Is_End_Cross_Girder = false;
        }
        #region Properties
        public double Width_Of_Slab
        {
            get
            {
                return W;
            }
            set
            {
                W = value;
            }
        }
        public double Depth_of_slab
        {
            get
            {
                return Ds;
            }
            set
            {
                Ds = value;
            }
        }

        public double Width_of_top_flange
        {
            get
            {
                return wtf;
            }
            set
            {
                wtf = value;
            }
        }
        public double Width_of_bottom_flange
        {
            get
            {
                return bwf;
            }
            set
            {
                bwf = value;
            }
        }
        public double Width_of_web
        {
            get
            {
                return bw;
            }
            set
            {
                bw = value;
            }
        }
        public double Thickness_of_top_flange
        {
            get
            {
                return D1;
            }
            set
            {
                D1 = value;
            }
        }
        public double Thickness_of_top_haunch
        {
            get
            {
                return D2;
            }
            set
            {
                D2 = value;
            }
        }
        public double Thickness_of_bottom_haunch
        {
            get
            {
                return D3;
            }
            set
            {
                D3 = value;
            }
        }
        public double Thickness_of_bottom_flange
        {
            get
            {
                return D4;
            }
            set
            {
                D4 = value;
            }
        }
        public double Depth_of_girder
        {
            get
            {
                return d;
            }
            set
            {
                d = value;
            }
        }
        #endregion Properties

        #region Deck Slab Properties
        public double Deck_Slab_A
        {
            get
            {
                return W * Ds;
            }
        }
        public double Deck_Slab_Y
        {
            get
            {
                return Ds / 2.0 + d;
            }
        }
        public double Deck_Slab_AY
        {
            get
            {
                return Deck_Slab_A * Deck_Slab_Y;
            }
        }
        public double Deck_Slab_AYY
        {
            get
            {
                return Deck_Slab_A * Deck_Slab_Y * Deck_Slab_Y;
            }
        }
        public double Deck_Slab_Ix
        {
            get
            {
                return W * Ds * Ds * Ds / 12.0;
            }
        }
        #endregion Deck Slab


        #region Rectangle Top Flange Properties
        public double Rectangle_Top_Flange_A
        {
            get
            {
                if (D1 != 0)
                    return (wtf - bw) * D1;

                return 0.0;
            }
        }
        public double Rectangle_Top_Flange_Y
        {
            get
            {
                return (d - D1 / 2.0);
            }
        }
        public double Rectangle_Top_Flange_AY
        {
            get
            {
                return Rectangle_Top_Flange_A * Rectangle_Top_Flange_Y;
            }
        }
        public double Rectangle_Top_Flange_AYY
        {
            get
            {
                return Rectangle_Top_Flange_AY * Rectangle_Top_Flange_Y;
            }
        }
        public double Rectangle_Top_Flange_Ix
        {
            get
            {
                if (D1 != 0)
                {
                    return (wtf - bw) * D1 * D1 * D1 / 12.0;
                }
                return 0.0;
            }
        }
        #endregion Rectangle_Top_Flange

        #region Triangular Top Flange Properties
        public double Triangular_Top_Flange_A
        {
            get
            {
                if (D2 != 0)
                    return 0.5 * (wtf - bw) * D2;

                return 0.0;
            }
        }
        public double Triangular_Top_Flange_Y
        {
            get
            {
                return (d - D1 - (D2 - D1) / 3.0);
            }
        }
        public double Triangular_Top_Flange_AY
        {
            get
            {
                return Triangular_Top_Flange_A * Triangular_Top_Flange_Y;
            }
        }
        public double Triangular_Top_Flange_AYY
        {
            get
            {
                return Triangular_Top_Flange_AY * Triangular_Top_Flange_Y;
            }
        }
        public double Triangular_Top_Flange_Ix
        {
            get
            {
                if (D1 != 0)
                {
                    return (wtf - bw) * D2 * D2 * D2 / 36.0;
                }
                return 0.0;
            }
        }
        #endregion Triangular_Top_Flange


        #region Web + top & bottom rectangle haunch Properties
        public double Web_A
        {
            get
            {
                return bw * d;
            }
        }
        public double Web_Y
        {
            get
            {
                return (d / 2.0);
            }
        }
        public double Web_AY
        {
            get
            {
                return Web_A * Web_Y;
            }
        }
        public double Web_AYY
        {
            get
            {
                return Web_AY * Web_Y;
            }
        }
        public double Web_Ix
        {
            get
            {
                return (bw) * d * d * d / 12.0;
            }
        }
        #endregion Web

        #region Bottom Bulb, Triangle Properties
        public double Bottom_Bulb_Triangle_A
        {
            get
            {
                if (D3 != 0)
                    return 0.5 * (bwf - bw) * D3;
                return 0.0;
            }
        }
        public double Bottom_Bulb_Triangle_Y
        {
            get
            {
                return (D4 + D3 / 3.0);
            }
        }
        public double Bottom_Bulb_Triangle_AY
        {
            get
            {
                return Bottom_Bulb_Triangle_A * Bottom_Bulb_Triangle_Y;
            }
        }
        public double Bottom_Bulb_Triangle_AYY
        {
            get
            {
                return Bottom_Bulb_Triangle_AY * Bottom_Bulb_Triangle_Y;
            }
        }
        public double Bottom_Bulb_Triangle_Ix
        {
            get
            {
                return (bwf - bw) * D3 * D3 * D3 / 36.0;
            }
        }
        #endregion Web

        #region Bottom Bulb, Rectangle Properties
        public double Bottom_Bulb_Rectangle_A
        {
            get
            {
                if (D4 != 0)
                    return (bwf - bw) * D4;
                return 0.0;
            }
        }
        public double Bottom_Bulb_Rectangle_Y
        {
            get
            {
                return (D4 / 2.0);
            }
        }
        public double Bottom_Bulb_Rectangle_AY
        {
            get
            {
                return Bottom_Bulb_Rectangle_A * Bottom_Bulb_Rectangle_Y;
            }
        }
        public double Bottom_Bulb_Rectangle_AYY
        {
            get
            {
                return Bottom_Bulb_Rectangle_AY * Bottom_Bulb_Rectangle_Y;
            }
        }
        public double Bottom_Bulb_Rectangle_Ix
        {
            get
            {
                return (bwf - bw) * D4 * D4 * D4 / 36.0;
            }
        }
        #endregion Bottom_Bulb_Rectangle


        #region Composite Section Properties
        public double Composite_Section_A
        {
            get
            {

                return (Deck_Slab_A + Rectangle_Top_Flange_A + Triangular_Top_Flange_A
                    + Web_A + Bottom_Bulb_Triangle_A + Bottom_Bulb_Rectangle_A);
            }
        }
        public double Composite_Section_Y
        {
            get
            {

                return (Composite_Section_AY / Composite_Section_A);
            }
        }
        public double Composite_Section_AY
        {
            get
            {

                return (Deck_Slab_AY + Rectangle_Top_Flange_AY + Triangular_Top_Flange_AY
                    + Web_AY + Bottom_Bulb_Triangle_AY + Bottom_Bulb_Rectangle_AY);
            }
        }
        public double Composite_Section_AYY
        {
            get
            {

                return (Deck_Slab_AYY + Rectangle_Top_Flange_AYY + Triangular_Top_Flange_AYY
                    + Web_AYY + Bottom_Bulb_Triangle_AYY + Bottom_Bulb_Rectangle_AYY);
            }
        }
        public double Composite_Section_Ix
        {
            get
            {
                return (Deck_Slab_Ix
                    + Rectangle_Top_Flange_Ix
                    + Triangular_Top_Flange_Ix
                    + Web_Ix
                    + Bottom_Bulb_Triangle_Ix
                    + Bottom_Bulb_Rectangle_Ix);
            }
        }

        public double Composite_Section_Iz
        {
            get
            {
                return (Deck_Slab_Ix + Deck_Slab_AYY
                    + Rectangle_Top_Flange_Ix + Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_Ix + Triangular_Top_Flange_AYY
                    + Web_Ix + Web_AYY
                    + Bottom_Bulb_Triangle_Ix + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_Ix + Bottom_Bulb_Rectangle_AYY) - Composite_Section_A * Composite_Section_Y * Composite_Section_Y;
            }
        }

        #endregion Composite_Section

        #region Girder Section Properties
        public double Girder_Section_A
        {
            get
            {

                return (Rectangle_Top_Flange_A
                    + Triangular_Top_Flange_A
                    + Web_A
                    + Bottom_Bulb_Triangle_A
                    + Bottom_Bulb_Rectangle_A);
            }
        }

        public double Girder_Section_Y
        {
            get
            {

                return (Girder_Section_AY / Girder_Section_A);
            }
        }

        public double Girder_Section_AY
        {
            get
            {

                return (Rectangle_Top_Flange_AY
                    + Triangular_Top_Flange_AY
                    + Web_AY
                    + Bottom_Bulb_Triangle_AY
                    + Bottom_Bulb_Rectangle_AY);
            }
        }
        public double Girder_Section_AYY
        {
            get
            {

                return (Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_AYY
                    + Web_AYY
                    + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_AYY);
            }
        }
        public double Girder_Section_Ix
        {
            get
            {
                return (Rectangle_Top_Flange_Ix
                    + Triangular_Top_Flange_Ix
                    + Web_Ix
                    + Bottom_Bulb_Triangle_Ix
                    + Bottom_Bulb_Rectangle_Ix);
            }
        }

        public double Girder_Section_Iz
        {
            get
            {
                return (Rectangle_Top_Flange_Ix + Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_Ix + Triangular_Top_Flange_AYY
                    + Web_Ix + Web_AYY
                    + Bottom_Bulb_Triangle_Ix + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_Ix + Bottom_Bulb_Rectangle_AYY) - Girder_Section_A * Girder_Section_Y * Girder_Section_Y;
            }
        }
        #endregion Girder_Section
    }

    public class PSC_I_Girder_Inputs
    {
        public PSC_I_Girder_Inputs() { }

        public TextBox txt_Ana_L;
        public TextBox txt_Ana_B;

        public TextBox txt_Ana_CW;

        public TextBox txt_Ana_CL;

        public TextBox txt_Ana_CR;

        public TextBox cmb_NMG;

        public TextBox txt_Ana_girder_spacing;

        public TextBox txt_Ana_Ds;

        public TextBox txt_Ana_Dso;

        public TextBox txt_Ana_Lc;

        public TextBox txt_Ana_ang;

        public TextBox txt_Ana_concrete_weight;
        public TextBox txt_Ana_FWS_Y;

        public TextBox txt_Ana_parapet_unit_weight;

        public TextBox txt_Ana_haunch_width;

        public TextBox txt_Ana_haunch_thickness;

        public TextBox txt_Ana_Rw;

        public TextBox txt_Ana_Ec;

        public TextBox txt_Ana_Ecm;

        public TextBox txt_Ana_Es;

        public TextBox txt_Ana_Dw;

        public TextBox txt_Ana_FWS_Weight;

        public TextBox txt_Ana_Parapet_Width;

        public TextBox txt_Ana_Parapet_Height;

        public TextBox txt_Ana_Crash_Barier_Width;

        public TextBox txt_Ana_Crash_Barier_Height;

        public TextBox txt_Ana_SMG;

        public TextBox txt_Ana_DMG;

    }

}
