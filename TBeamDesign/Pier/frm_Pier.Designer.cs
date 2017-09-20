namespace BridgeAnalysisDesign.Pier
{
    partial class frm_Pier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            iApp.Delete_Temporary_Files();
            iApp.Delete_Temporary_Files(user_path);
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Pier));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tab_dl = new System.Windows.Forms.TabPage();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.btn_def_mov_load = new System.Windows.Forms.Button();
            this.chk_ana_active_LL = new System.Windows.Forms.CheckBox();
            this.grb_LL = new System.Windows.Forms.GroupBox();
            this.label75 = new System.Windows.Forms.Label();
            this.btn_live_load_remove_all = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_LL_load_gen = new System.Windows.Forms.TextBox();
            this.btn_add_load = new System.Windows.Forms.Button();
            this.btn_live_load_remove = new System.Windows.Forms.Button();
            this.txt_Y = new System.Windows.Forms.TextBox();
            this.dgv_live_load = new System.Windows.Forms.DataGridView();
            this.col_load_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_if = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_XINC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_imf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label207 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.txt_Load_Impact = new System.Windows.Forms.TextBox();
            this.txt_XINCR = new System.Windows.Forms.TextBox();
            this.cmb_load_type = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txt_Ana_X = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_Z = new System.Windows.Forms.TextBox();
            this.grb_SIDL = new System.Windows.Forms.GroupBox();
            this.txt_member_load = new System.Windows.Forms.TextBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.chk_ana_active_SIDL = new System.Windows.Forms.CheckBox();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.chk_WC = new System.Windows.Forms.CheckBox();
            this.chk_swf = new System.Windows.Forms.CheckBox();
            this.txt_Ana_swf = new System.Windows.Forms.TextBox();
            this.chk_sw_fp = new System.Windows.Forms.CheckBox();
            this.grb_ana_wc = new System.Windows.Forms.GroupBox();
            this.label511 = new System.Windows.Forms.Label();
            this.txt_Ana_gamma_w = new System.Windows.Forms.TextBox();
            this.label515 = new System.Windows.Forms.Label();
            this.label520 = new System.Windows.Forms.Label();
            this.txt_Ana_Dw = new System.Windows.Forms.TextBox();
            this.label521 = new System.Windows.Forms.Label();
            this.chk_parapet = new System.Windows.Forms.CheckBox();
            this.grb_ana_parapet = new System.Windows.Forms.GroupBox();
            this.txt_Ana_Hp = new System.Windows.Forms.TextBox();
            this.label514 = new System.Windows.Forms.Label();
            this.label510 = new System.Windows.Forms.Label();
            this.label522 = new System.Windows.Forms.Label();
            this.txt_Ana_Wp = new System.Windows.Forms.TextBox();
            this.label523 = new System.Windows.Forms.Label();
            this.grb_ana_sw_fp = new System.Windows.Forms.GroupBox();
            this.txt_Ana_Hps = new System.Windows.Forms.TextBox();
            this.label531 = new System.Windows.Forms.Label();
            this.txt_Ana_Wps = new System.Windows.Forms.TextBox();
            this.label529 = new System.Windows.Forms.Label();
            this.label530 = new System.Windows.Forms.Label();
            this.txt_Ana_Hs = new System.Windows.Forms.TextBox();
            this.label528 = new System.Windows.Forms.Label();
            this.label524 = new System.Windows.Forms.Label();
            this.label525 = new System.Windows.Forms.Label();
            this.label526 = new System.Windows.Forms.Label();
            this.txt_Ana_Bs = new System.Windows.Forms.TextBox();
            this.label527 = new System.Windows.Forms.Label();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.grb_create_input_data = new System.Windows.Forms.GroupBox();
            this.label211 = new System.Windows.Forms.Label();
            this.label212 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Ana_ang = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_Ana_gamma_c = new System.Windows.Forms.TextBox();
            this.txt_Ana_Ds = new System.Windows.Forms.TextBox();
            this.label503 = new System.Windows.Forms.Label();
            this.label501 = new System.Windows.Forms.Label();
            this.label499 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Ana_CR = new System.Windows.Forms.TextBox();
            this.label502 = new System.Windows.Forms.Label();
            this.txt_Ana_CL = new System.Windows.Forms.TextBox();
            this.label500 = new System.Windows.Forms.Label();
            this.txt_Ana_CW = new System.Windows.Forms.TextBox();
            this.label498 = new System.Windows.Forms.Label();
            this.txt_Ana_B = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ana_L = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.label512 = new System.Windows.Forms.Label();
            this.label513 = new System.Windows.Forms.Label();
            this.txt_Ana_BMG = new System.Windows.Forms.TextBox();
            this.label516 = new System.Windows.Forms.Label();
            this.txt_Ana_DMG = new System.Windows.Forms.TextBox();
            this.label517 = new System.Windows.Forms.Label();
            this.txt_Ana_NMG = new System.Windows.Forms.TextBox();
            this.label519 = new System.Windows.Forms.Label();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.label504 = new System.Windows.Forms.Label();
            this.label505 = new System.Windows.Forms.Label();
            this.txt_Ana_BCG = new System.Windows.Forms.TextBox();
            this.label506 = new System.Windows.Forms.Label();
            this.txt_Ana_DCG = new System.Windows.Forms.TextBox();
            this.label507 = new System.Windows.Forms.Label();
            this.txt_Ana_NCG = new System.Windows.Forms.TextBox();
            this.label509 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.grb_select_analysis = new System.Windows.Forms.GroupBox();
            this.txt_analysis_file = new System.Windows.Forms.TextBox();
            this.btn_ana_browse_input_file = new System.Windows.Forms.Button();
            this.rbtn_ana_create_analysis_file = new System.Windows.Forms.RadioButton();
            this.rbtn_ana_select_analysis_file = new System.Windows.Forms.RadioButton();
            this.tab_result = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_View_Moving_Load = new System.Windows.Forms.Button();
            this.btn_view_report = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.btn_process_analysis = new System.Windows.Forms.Button();
            this.btn_create_data = new System.Windows.Forms.Button();
            this.btn_view_data = new System.Windows.Forms.Button();
            this.btn_view_structure = new System.Windows.Forms.Button();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.btn_update_force = new System.Windows.Forms.Button();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.chk_R2 = new System.Windows.Forms.CheckBox();
            this.chk_R3 = new System.Windows.Forms.CheckBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.chk_M2 = new System.Windows.Forms.CheckBox();
            this.chk_M3 = new System.Windows.Forms.CheckBox();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label222 = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.label534 = new System.Windows.Forms.Label();
            this.txt_ana_TSRP = new System.Windows.Forms.TextBox();
            this.label535 = new System.Windows.Forms.Label();
            this.label536 = new System.Windows.Forms.Label();
            this.txt_ana_MSTD = new System.Windows.Forms.TextBox();
            this.label537 = new System.Windows.Forms.Label();
            this.label538 = new System.Windows.Forms.Label();
            this.txt_ana_MSLD = new System.Windows.Forms.TextBox();
            this.label539 = new System.Windows.Forms.Label();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label541 = new System.Windows.Forms.Label();
            this.label540 = new System.Windows.Forms.Label();
            this.txt_ana_DLSR = new System.Windows.Forms.TextBox();
            this.txt_ana_LLSR = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.txt_outer_long_L2_shear = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.txt_outer_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_outer_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_outer_long_deff_moment = new System.Windows.Forms.TextBox();
            this.txt_outer_long_L4_shear = new System.Windows.Forms.TextBox();
            this.txt_outer_long_L4_moment = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label162 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.label165 = new System.Windows.Forms.Label();
            this.txt_Ana_cross_max_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_cross_max_moment = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label166 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label198 = new System.Windows.Forms.Label();
            this.label199 = new System.Windows.Forms.Label();
            this.label200 = new System.Windows.Forms.Label();
            this.label201 = new System.Windows.Forms.Label();
            this.label202 = new System.Windows.Forms.Label();
            this.label203 = new System.Windows.Forms.Label();
            this.label204 = new System.Windows.Forms.Label();
            this.label205 = new System.Windows.Forms.Label();
            this.label206 = new System.Windows.Forms.Label();
            this.txt_Ana_inner_long_L4_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_inner_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_inner_long_L2_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_inner_long_L4_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_inner_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_inner_long_deff_moment = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.groupBox49 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_final_Mz = new System.Windows.Forms.TextBox();
            this.label220 = new System.Windows.Forms.Label();
            this.txt_max_Mz_kN = new System.Windows.Forms.TextBox();
            this.label261 = new System.Windows.Forms.Label();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.txt_left_total_Mz = new System.Windows.Forms.TextBox();
            this.txt_left_total_Mx = new System.Windows.Forms.TextBox();
            this.label325 = new System.Windows.Forms.Label();
            this.label326 = new System.Windows.Forms.Label();
            this.txt_left_total_vert_reac = new System.Windows.Forms.TextBox();
            this.dgv_left_des_frc = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Max_Mx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Max_Mz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g = new System.Windows.Forms.GroupBox();
            this.dgv_right_des_frc = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_right_total_Mz = new System.Windows.Forms.TextBox();
            this.txt_right_total_Mx = new System.Windows.Forms.TextBox();
            this.label402 = new System.Windows.Forms.Label();
            this.label442 = new System.Windows.Forms.Label();
            this.txt_right_total_vert_reac = new System.Windows.Forms.TextBox();
            this.groupBox48 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txt_final_Mx = new System.Windows.Forms.TextBox();
            this.label237 = new System.Windows.Forms.Label();
            this.txt_max_Mx_kN = new System.Windows.Forms.TextBox();
            this.label262 = new System.Windows.Forms.Label();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.txt_final_vert_rec_kN = new System.Windows.Forms.TextBox();
            this.label260 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txt_final_vert_reac = new System.Windows.Forms.TextBox();
            this.label264 = new System.Windows.Forms.Label();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.label327 = new System.Windows.Forms.Label();
            this.txt_dead_kN_m = new System.Windows.Forms.TextBox();
            this.label354 = new System.Windows.Forms.Label();
            this.txt_dead_vert_reac_kN = new System.Windows.Forms.TextBox();
            this.label370 = new System.Windows.Forms.Label();
            this.label371 = new System.Windows.Forms.Label();
            this.dgv_left_end_design_forces = new System.Windows.Forms.DataGridView();
            this.col_Joints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Vert_Reaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dead_vert_reac_ton = new System.Windows.Forms.TextBox();
            this.groupBox47 = new System.Windows.Forms.GroupBox();
            this.txt_live_kN_m = new System.Windows.Forms.TextBox();
            this.label388 = new System.Windows.Forms.Label();
            this.txt_live_vert_rec_kN = new System.Windows.Forms.TextBox();
            this.label399 = new System.Windows.Forms.Label();
            this.label400 = new System.Windows.Forms.Label();
            this.dgv_right_end_design_forces = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_live_vert_rec_Ton = new System.Windows.Forms.TextBox();
            this.label401 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tab_des_form1 = new System.Windows.Forms.TabPage();
            this.label214 = new System.Windows.Forms.Label();
            this.label216 = new System.Windows.Forms.Label();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.txt_rcc_pier_m = new System.Windows.Forms.TextBox();
            this.label215 = new System.Windows.Forms.Label();
            this.cmb_rcc_pier_fy = new System.Windows.Forms.ComboBox();
            this.label236 = new System.Windows.Forms.Label();
            this.cmb_rcc_pier_fck = new System.Windows.Forms.ComboBox();
            this.label244 = new System.Windows.Forms.Label();
            this.label250 = new System.Windows.Forms.Label();
            this.label251 = new System.Windows.Forms.Label();
            this.txt_rcc_pier_sigma_st = new System.Windows.Forms.TextBox();
            this.label253 = new System.Windows.Forms.Label();
            this.label254 = new System.Windows.Forms.Label();
            this.txt_rcc_pier_sigma_c = new System.Windows.Forms.TextBox();
            this.label258 = new System.Windows.Forms.Label();
            this.label259 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label122 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_W1_supp_reac = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Mz1 = new System.Windows.Forms.TextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Mx1 = new System.Windows.Forms.TextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H7 = new System.Windows.Forms.TextBox();
            this.label116 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_gama_c = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_vehi_load = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NR = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NP = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_fck_2 = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.label225 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_fy2 = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_D = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_b = new System.Windows.Forms.TextBox();
            this.txt_RCC_Pier_tdia = new System.Windows.Forms.TextBox();
            this.txt_RCC_Pier_rdia = new System.Windows.Forms.TextBox();
            this.txt_RCC_Pier_d_dash = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_p1 = new System.Windows.Forms.TextBox();
            this.label226 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label224 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_p2 = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H2 = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B4 = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B3 = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H1 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B2 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B1 = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_overall_height = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B14 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B13 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B12 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B11 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B10 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B9 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H6 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H5 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B8 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H4 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H3 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B7 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Form_Lev = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL5 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL4 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL3 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL2 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL1 = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B6 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B5 = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_d2 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_d1 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NB = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_a1 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_w3 = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_w2 = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_w1 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_L1 = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.tab_des_form2 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label191 = new System.Windows.Forms.Label();
            this.label190 = new System.Windows.Forms.Label();
            this.label189 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.label187 = new System.Windows.Forms.Label();
            this.label197 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            this.label193 = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.label186 = new System.Windows.Forms.Label();
            this.label185 = new System.Windows.Forms.Label();
            this.cmb_pier_2_k = new System.Windows.Forms.ComboBox();
            this.txt_pier_2_SBC = new System.Windows.Forms.TextBox();
            this.label184 = new System.Windows.Forms.Label();
            this.txt_pier_2_vdia = new System.Windows.Forms.TextBox();
            this.label230 = new System.Windows.Forms.Label();
            this.txt_pier_2_hdia = new System.Windows.Forms.TextBox();
            this.label228 = new System.Windows.Forms.Label();
            this.txt_pier_2_ldia = new System.Windows.Forms.TextBox();
            this.label183 = new System.Windows.Forms.Label();
            this.txt_pier_2_slegs = new System.Windows.Forms.TextBox();
            this.label182 = new System.Windows.Forms.Label();
            this.txt_pier_2_sdia = new System.Windows.Forms.TextBox();
            this.label181 = new System.Windows.Forms.Label();
            this.txt_pier_2_Itc = new System.Windows.Forms.TextBox();
            this.label180 = new System.Windows.Forms.Label();
            this.txt_pier_2_Vr = new System.Windows.Forms.TextBox();
            this.label179 = new System.Windows.Forms.Label();
            this.txt_pier_2_LL = new System.Windows.Forms.TextBox();
            this.label178 = new System.Windows.Forms.Label();
            this.txt_pier_2_CF = new System.Windows.Forms.TextBox();
            this.label177 = new System.Windows.Forms.Label();
            this.txt_pier_2_k = new System.Windows.Forms.TextBox();
            this.label176 = new System.Windows.Forms.Label();
            this.txt_pier_2_V = new System.Windows.Forms.TextBox();
            this.label175 = new System.Windows.Forms.Label();
            this.txt_pier_2_HHF = new System.Windows.Forms.TextBox();
            this.label174 = new System.Windows.Forms.Label();
            this.txt_pier_2_SC = new System.Windows.Forms.TextBox();
            this.label173 = new System.Windows.Forms.Label();
            this.txt_pier_2_PD = new System.Windows.Forms.TextBox();
            this.label172 = new System.Windows.Forms.Label();
            this.txt_pier_2_PML = new System.Windows.Forms.TextBox();
            this.label171 = new System.Windows.Forms.Label();
            this.txt_pier_2_PL = new System.Windows.Forms.TextBox();
            this.label170 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.txt_pier_2_APD = new System.Windows.Forms.TextBox();
            this.label168 = new System.Windows.Forms.Label();
            this.txt_pier_2_B16 = new System.Windows.Forms.TextBox();
            this.label160 = new System.Windows.Forms.Label();
            this.txt_pier_2_P3 = new System.Windows.Forms.TextBox();
            this.label161 = new System.Windows.Forms.Label();
            this.txt_pier_2_P2 = new System.Windows.Forms.TextBox();
            this.label217 = new System.Windows.Forms.Label();
            this.label218 = new System.Windows.Forms.Label();
            this.label159 = new System.Windows.Forms.Label();
            this.tab_des_Diagram = new System.Windows.Forms.TabPage();
            this.pic_pier_interactive_diagram = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label208 = new System.Windows.Forms.Label();
            this.btn_RCC_Pier_Process = new System.Windows.Forms.Button();
            this.btn_RCC_Pier_Report = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label219 = new System.Windows.Forms.Label();
            this.label221 = new System.Windows.Forms.Label();
            this.label209 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.txt_V = new System.Windows.Forms.TextBox();
            this.label144 = new System.Windows.Forms.Label();
            this.txt_F = new System.Windows.Forms.TextBox();
            this.label145 = new System.Windows.Forms.Label();
            this.txt_A = new System.Windows.Forms.TextBox();
            this.label146 = new System.Windows.Forms.Label();
            this.txt_f2 = new System.Windows.Forms.TextBox();
            this.label147 = new System.Windows.Forms.Label();
            this.txt_f1 = new System.Windows.Forms.TextBox();
            this.label148 = new System.Windows.Forms.Label();
            this.txt_gamma_c = new System.Windows.Forms.TextBox();
            this.label149 = new System.Windows.Forms.Label();
            this.txt_HFL = new System.Windows.Forms.TextBox();
            this.label150 = new System.Windows.Forms.Label();
            this.txt_h = new System.Windows.Forms.TextBox();
            this.label151 = new System.Windows.Forms.Label();
            this.txt_l = new System.Windows.Forms.TextBox();
            this.label152 = new System.Windows.Forms.Label();
            this.txt_b2 = new System.Windows.Forms.TextBox();
            this.label153 = new System.Windows.Forms.Label();
            this.txt_b1 = new System.Windows.Forms.TextBox();
            this.label154 = new System.Windows.Forms.Label();
            this.txt_w3 = new System.Windows.Forms.TextBox();
            this.label155 = new System.Windows.Forms.Label();
            this.txt_e = new System.Windows.Forms.TextBox();
            this.label156 = new System.Windows.Forms.Label();
            this.txt_w2 = new System.Windows.Forms.TextBox();
            this.label157 = new System.Windows.Forms.Label();
            this.txt_w1 = new System.Windows.Forms.TextBox();
            this.label158 = new System.Windows.Forms.Label();
            this.pic_stone_masonry = new System.Windows.Forms.PictureBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btn_ws_open_cir_well = new System.Windows.Forms.Button();
            this.btn_ws_new_cir_well = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btn_ws_open_cir = new System.Windows.Forms.Button();
            this.btn_ws_new_cir = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btn_worksheet_pier_design_with_piles = new System.Windows.Forms.Button();
            this.btn_worksheet_open = new System.Windows.Forms.Button();
            this.btn_worksheet_pile_capacity = new System.Windows.Forms.Button();
            this.btn_worksheet_Pier_cap = new System.Windows.Forms.Button();
            this.btn_worksheet_1 = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label223 = new System.Windows.Forms.Label();
            this.label210 = new System.Windows.Forms.Label();
            this.btn_dwg_rcc_pier = new System.Windows.Forms.Button();
            this.btn_dwg_pier_1 = new System.Windows.Forms.Button();
            this.btn_dwg_pier_2 = new System.Windows.Forms.Button();
            this.btn_dwg_stone_interactive = new System.Windows.Forms.Button();
            this.label233 = new System.Windows.Forms.Label();
            this.txt_pier_2_vspc = new System.Windows.Forms.TextBox();
            this.label234 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tab_dl.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.grb_LL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_live_load)).BeginInit();
            this.grb_SIDL.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.grb_ana_wc.SuspendLayout();
            this.grb_ana_parapet.SuspendLayout();
            this.grb_ana_sw_fp.SuspendLayout();
            this.groupBox37.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.grb_select_analysis.SuspendLayout();
            this.tab_result.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.groupBox49.SuspendLayout();
            this.groupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_des_frc)).BeginInit();
            this.g.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_des_frc)).BeginInit();
            this.groupBox48.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.groupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).BeginInit();
            this.groupBox47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tab_des_form1.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tab_des_form2.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tab_des_Diagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pier_interactive_diagram)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_stone_masonry)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(962, 700);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(954, 674);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Analysis of Bridge Deck for Piers";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tab_dl);
            this.tabControl3.Controls.Add(this.tab_result);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.Location = new System.Drawing.Point(3, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(948, 668);
            this.tabControl3.TabIndex = 98;
            // 
            // tab_dl
            // 
            this.tab_dl.Controls.Add(this.groupBox38);
            this.tab_dl.Controls.Add(this.grb_SIDL);
            this.tab_dl.Controls.Add(this.chk_ana_active_SIDL);
            this.tab_dl.Controls.Add(this.groupBox36);
            this.tab_dl.Controls.Add(this.groupBox37);
            this.tab_dl.Controls.Add(this.groupBox12);
            this.tab_dl.Location = new System.Drawing.Point(4, 22);
            this.tab_dl.Name = "tab_dl";
            this.tab_dl.Padding = new System.Windows.Forms.Padding(3);
            this.tab_dl.Size = new System.Drawing.Size(940, 642);
            this.tab_dl.TabIndex = 0;
            this.tab_dl.Text = "User Input Data";
            this.tab_dl.UseVisualStyleBackColor = true;
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.btn_def_mov_load);
            this.groupBox38.Controls.Add(this.chk_ana_active_LL);
            this.groupBox38.Controls.Add(this.grb_LL);
            this.groupBox38.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox38.ForeColor = System.Drawing.Color.Black;
            this.groupBox38.Location = new System.Drawing.Point(441, 344);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(493, 247);
            this.groupBox38.TabIndex = 95;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "LIVE LOAD (LL) / Vehicle Moving Load";
            // 
            // btn_def_mov_load
            // 
            this.btn_def_mov_load.Location = new System.Drawing.Point(269, 14);
            this.btn_def_mov_load.Name = "btn_def_mov_load";
            this.btn_def_mov_load.Size = new System.Drawing.Size(203, 23);
            this.btn_def_mov_load.TabIndex = 94;
            this.btn_def_mov_load.Text = "Define New Moving Load";
            this.btn_def_mov_load.UseVisualStyleBackColor = true;
            this.btn_def_mov_load.Click += new System.EventHandler(this.btn_def_mov_load_Click);
            // 
            // chk_ana_active_LL
            // 
            this.chk_ana_active_LL.AutoSize = true;
            this.chk_ana_active_LL.Checked = true;
            this.chk_ana_active_LL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ana_active_LL.ForeColor = System.Drawing.Color.Blue;
            this.chk_ana_active_LL.Location = new System.Drawing.Point(6, 18);
            this.chk_ana_active_LL.Name = "chk_ana_active_LL";
            this.chk_ana_active_LL.Size = new System.Drawing.Size(165, 17);
            this.chk_ana_active_LL.TabIndex = 92;
            this.chk_ana_active_LL.Text = "Modify Live Load (LL)";
            this.chk_ana_active_LL.UseVisualStyleBackColor = true;
            // 
            // grb_LL
            // 
            this.grb_LL.Controls.Add(this.label75);
            this.grb_LL.Controls.Add(this.btn_live_load_remove_all);
            this.grb_LL.Controls.Add(this.label18);
            this.grb_LL.Controls.Add(this.txt_LL_load_gen);
            this.grb_LL.Controls.Add(this.btn_add_load);
            this.grb_LL.Controls.Add(this.btn_live_load_remove);
            this.grb_LL.Controls.Add(this.txt_Y);
            this.grb_LL.Controls.Add(this.dgv_live_load);
            this.grb_LL.Controls.Add(this.label207);
            this.grb_LL.Controls.Add(this.label19);
            this.grb_LL.Controls.Add(this.label60);
            this.grb_LL.Controls.Add(this.txt_Load_Impact);
            this.grb_LL.Controls.Add(this.txt_XINCR);
            this.grb_LL.Controls.Add(this.cmb_load_type);
            this.grb_LL.Controls.Add(this.label59);
            this.grb_LL.Controls.Add(this.txt_Ana_X);
            this.grb_LL.Controls.Add(this.label20);
            this.grb_LL.Controls.Add(this.txt_Z);
            this.grb_LL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_LL.ForeColor = System.Drawing.Color.Blue;
            this.grb_LL.Location = new System.Drawing.Point(6, 41);
            this.grb_LL.Name = "grb_LL";
            this.grb_LL.Size = new System.Drawing.Size(484, 195);
            this.grb_LL.TabIndex = 89;
            this.grb_LL.TabStop = false;
            this.grb_LL.Text = "Define Moving Load";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(30, 165);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(114, 13);
            this.label75.TabIndex = 80;
            this.label75.Text = "Load Generation";
            // 
            // btn_live_load_remove_all
            // 
            this.btn_live_load_remove_all.Location = new System.Drawing.Point(372, 157);
            this.btn_live_load_remove_all.Name = "btn_live_load_remove_all";
            this.btn_live_load_remove_all.Size = new System.Drawing.Size(94, 23);
            this.btn_live_load_remove_all.TabIndex = 69;
            this.btn_live_load_remove_all.Text = "Remove All";
            this.btn_live_load_remove_all.UseVisualStyleBackColor = true;
            this.btn_live_load_remove_all.Click += new System.EventHandler(this.btn_Ana_live_load_remove_all_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(30, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 59;
            this.label18.Text = "Load Type";
            // 
            // txt_LL_load_gen
            // 
            this.txt_LL_load_gen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LL_load_gen.Location = new System.Drawing.Point(158, 162);
            this.txt_LL_load_gen.Name = "txt_LL_load_gen";
            this.txt_LL_load_gen.Size = new System.Drawing.Size(53, 21);
            this.txt_LL_load_gen.TabIndex = 79;
            this.txt_LL_load_gen.Text = "191";
            this.txt_LL_load_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LL_load_gen.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // btn_add_load
            // 
            this.btn_add_load.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_load.Location = new System.Drawing.Point(446, 28);
            this.btn_add_load.Name = "btn_add_load";
            this.btn_add_load.Size = new System.Drawing.Size(37, 23);
            this.btn_add_load.TabIndex = 63;
            this.btn_add_load.Text = "Add";
            this.btn_add_load.UseVisualStyleBackColor = true;
            this.btn_add_load.Click += new System.EventHandler(this.btn_Ana_add_load_Click);
            // 
            // btn_live_load_remove
            // 
            this.btn_live_load_remove.Location = new System.Drawing.Point(279, 157);
            this.btn_live_load_remove.Name = "btn_live_load_remove";
            this.btn_live_load_remove.Size = new System.Drawing.Size(87, 23);
            this.btn_live_load_remove.TabIndex = 68;
            this.btn_live_load_remove.Text = "Remove";
            this.btn_live_load_remove.UseVisualStyleBackColor = true;
            this.btn_live_load_remove.Click += new System.EventHandler(this.btn_Ana_live_load_remove_Click);
            // 
            // txt_Y
            // 
            this.txt_Y.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Y.Location = new System.Drawing.Point(256, 31);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(36, 18);
            this.txt_Y.TabIndex = 64;
            this.txt_Y.Text = "0.0";
            this.txt_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgv_live_load
            // 
            this.dgv_live_load.AllowUserToAddRows = false;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_live_load.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgv_live_load.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_live_load.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_load_type,
            this.col_code,
            this.col_if,
            this.col_Z,
            this.col_XINC,
            this.col_imf});
            this.dgv_live_load.Location = new System.Drawing.Point(8, 58);
            this.dgv_live_load.Name = "dgv_live_load";
            this.dgv_live_load.RowHeadersWidth = 25;
            this.dgv_live_load.Size = new System.Drawing.Size(473, 93);
            this.dgv_live_load.TabIndex = 62;
            // 
            // col_load_type
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_load_type.DefaultCellStyle = dataGridViewCellStyle21;
            this.col_load_type.HeaderText = "Load Type";
            this.col_load_type.Name = "col_load_type";
            this.col_load_type.Width = 170;
            // 
            // col_code
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_code.DefaultCellStyle = dataGridViewCellStyle22;
            this.col_code.HeaderText = "X";
            this.col_code.Name = "col_code";
            this.col_code.Width = 60;
            // 
            // col_if
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_if.DefaultCellStyle = dataGridViewCellStyle23;
            this.col_if.HeaderText = "Y";
            this.col_if.Name = "col_if";
            this.col_if.Width = 45;
            // 
            // col_Z
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_Z.DefaultCellStyle = dataGridViewCellStyle24;
            this.col_Z.HeaderText = "Z";
            this.col_Z.Name = "col_Z";
            this.col_Z.Width = 50;
            // 
            // col_XINC
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_XINC.DefaultCellStyle = dataGridViewCellStyle25;
            this.col_XINC.HeaderText = "XINC";
            this.col_XINC.Name = "col_XINC";
            this.col_XINC.Width = 55;
            // 
            // col_imf
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_imf.DefaultCellStyle = dataGridViewCellStyle26;
            this.col_imf.HeaderText = "Impact Factor";
            this.col_imf.Name = "col_imf";
            this.col_imf.Width = 55;
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label207.Location = new System.Drawing.Point(388, 2);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(51, 26);
            this.label207.TabIndex = 60;
            this.label207.Text = "Impact \r\nFactor";
            this.label207.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(333, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 13);
            this.label19.TabIndex = 60;
            this.label19.Text = "X INCR";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(310, 14);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(15, 13);
            this.label60.TabIndex = 67;
            this.label60.Text = "Z";
            // 
            // txt_Load_Impact
            // 
            this.txt_Load_Impact.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Load_Impact.Location = new System.Drawing.Point(391, 31);
            this.txt_Load_Impact.Name = "txt_Load_Impact";
            this.txt_Load_Impact.Size = new System.Drawing.Size(47, 18);
            this.txt_Load_Impact.TabIndex = 58;
            this.txt_Load_Impact.Text = "0.2";
            this.txt_Load_Impact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Load_Impact.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // txt_XINCR
            // 
            this.txt_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_XINCR.Location = new System.Drawing.Point(335, 31);
            this.txt_XINCR.Name = "txt_XINCR";
            this.txt_XINCR.Size = new System.Drawing.Size(47, 18);
            this.txt_XINCR.TabIndex = 58;
            this.txt_XINCR.Text = "0.2";
            this.txt_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_XINCR.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // cmb_load_type
            // 
            this.cmb_load_type.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_load_type.FormattingEnabled = true;
            this.cmb_load_type.Items.AddRange(new object[] {
            "TYPE 1 : IRCCLASSA",
            "TYPE 2 : IRCCLASSB",
            "TYPE 3 : IRC70RTRACK",
            "TYPE 4 : IRC70RWHEEL",
            "TYPE 5 : IRCCLASSAATRACK",
            "TYPE 6 : IRC24RTRACK",
            "TYPE 7 : BG_RAIL_1",
            "TYPE 8 : BG_RAIL_2",
            "TYPE 9 : MG_RAIL_1",
            "TYPE 10 : MG_RAIL_2"});
            this.cmb_load_type.Location = new System.Drawing.Point(33, 31);
            this.cmb_load_type.Name = "cmb_load_type";
            this.cmb_load_type.Size = new System.Drawing.Size(168, 20);
            this.cmb_load_type.TabIndex = 56;
            this.cmb_load_type.SelectedIndexChanged += new System.EventHandler(this.cmb_Ana_load_type_SelectedIndexChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(266, 14);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(14, 13);
            this.label59.TabIndex = 65;
            this.label59.Text = "Y";
            // 
            // txt_Ana_X
            // 
            this.txt_Ana_X.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_X.Location = new System.Drawing.Point(202, 31);
            this.txt_Ana_X.Name = "txt_Ana_X";
            this.txt_Ana_X.Size = new System.Drawing.Size(51, 18);
            this.txt_Ana_X.TabIndex = 57;
            this.txt_Ana_X.Text = "-13.0";
            this.txt_Ana_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(219, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 13);
            this.label20.TabIndex = 61;
            this.label20.Text = "X";
            // 
            // txt_Z
            // 
            this.txt_Z.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Z.Location = new System.Drawing.Point(295, 31);
            this.txt_Z.Name = "txt_Z";
            this.txt_Z.Size = new System.Drawing.Size(34, 18);
            this.txt_Z.TabIndex = 66;
            this.txt_Z.Text = "0.5";
            this.txt_Z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_SIDL
            // 
            this.grb_SIDL.Controls.Add(this.txt_member_load);
            this.grb_SIDL.Controls.Add(this.btn_remove);
            this.grb_SIDL.Controls.Add(this.btn_remove_all);
            this.grb_SIDL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_SIDL.Location = new System.Drawing.Point(835, 219);
            this.grb_SIDL.Name = "grb_SIDL";
            this.grb_SIDL.Size = new System.Drawing.Size(224, 44);
            this.grb_SIDL.TabIndex = 88;
            this.grb_SIDL.TabStop = false;
            this.grb_SIDL.Text = "Dead Load + Super Imposed Dead Load [DL + SIDL]";
            this.grb_SIDL.Visible = false;
            // 
            // txt_member_load
            // 
            this.txt_member_load.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_member_load.Location = new System.Drawing.Point(3, 17);
            this.txt_member_load.Multiline = true;
            this.txt_member_load.Name = "txt_member_load";
            this.txt_member_load.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_member_load.Size = new System.Drawing.Size(218, 24);
            this.txt_member_load.TabIndex = 46;
            this.txt_member_load.Text = resources.GetString("txt_member_load.Text");
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(350, 203);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 45;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            // 
            // btn_remove_all
            // 
            this.btn_remove_all.Location = new System.Drawing.Point(440, 203);
            this.btn_remove_all.Name = "btn_remove_all";
            this.btn_remove_all.Size = new System.Drawing.Size(75, 23);
            this.btn_remove_all.TabIndex = 44;
            this.btn_remove_all.Text = "Remove All";
            this.btn_remove_all.UseVisualStyleBackColor = true;
            // 
            // chk_ana_active_SIDL
            // 
            this.chk_ana_active_SIDL.AutoSize = true;
            this.chk_ana_active_SIDL.Checked = true;
            this.chk_ana_active_SIDL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ana_active_SIDL.Location = new System.Drawing.Point(835, 193);
            this.chk_ana_active_SIDL.Name = "chk_ana_active_SIDL";
            this.chk_ana_active_SIDL.Size = new System.Drawing.Size(372, 17);
            this.chk_ana_active_SIDL.TabIndex = 93;
            this.chk_ana_active_SIDL.Text = "Modify Dead Load + Super Imposed Dead Load (DL + SIDL)";
            this.chk_ana_active_SIDL.UseVisualStyleBackColor = true;
            this.chk_ana_active_SIDL.Visible = false;
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.chk_WC);
            this.groupBox36.Controls.Add(this.chk_swf);
            this.groupBox36.Controls.Add(this.txt_Ana_swf);
            this.groupBox36.Controls.Add(this.chk_sw_fp);
            this.groupBox36.Controls.Add(this.grb_ana_wc);
            this.groupBox36.Controls.Add(this.chk_parapet);
            this.groupBox36.Controls.Add(this.grb_ana_parapet);
            this.groupBox36.Controls.Add(this.grb_ana_sw_fp);
            this.groupBox36.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox36.ForeColor = System.Drawing.Color.Black;
            this.groupBox36.Location = new System.Drawing.Point(441, 6);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Size = new System.Drawing.Size(375, 335);
            this.groupBox36.TabIndex = 3;
            this.groupBox36.TabStop = false;
            this.groupBox36.Text = "SUPER IMPOSED DEAD LOAD [SIDL]";
            // 
            // chk_WC
            // 
            this.chk_WC.AutoSize = true;
            this.chk_WC.Checked = true;
            this.chk_WC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_WC.ForeColor = System.Drawing.Color.Blue;
            this.chk_WC.Location = new System.Drawing.Point(6, 19);
            this.chk_WC.Name = "chk_WC";
            this.chk_WC.Size = new System.Drawing.Size(141, 17);
            this.chk_WC.TabIndex = 2;
            this.chk_WC.Text = "WEARING COURSE";
            this.chk_WC.UseVisualStyleBackColor = true;
            this.chk_WC.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_swf
            // 
            this.chk_swf.AutoSize = true;
            this.chk_swf.Checked = true;
            this.chk_swf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_swf.ForeColor = System.Drawing.Color.Blue;
            this.chk_swf.Location = new System.Drawing.Point(6, 313);
            this.chk_swf.Name = "chk_swf";
            this.chk_swf.Size = new System.Drawing.Size(141, 17);
            this.chk_swf.TabIndex = 2;
            this.chk_swf.Text = "Load Factor [swf]";
            this.chk_swf.UseVisualStyleBackColor = true;
            // 
            // txt_Ana_swf
            // 
            this.txt_Ana_swf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_swf.Location = new System.Drawing.Point(164, 311);
            this.txt_Ana_swf.Name = "txt_Ana_swf";
            this.txt_Ana_swf.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_swf.TabIndex = 0;
            this.txt_Ana_swf.Text = "1.400";
            this.txt_Ana_swf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_swf.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // chk_sw_fp
            // 
            this.chk_sw_fp.AutoSize = true;
            this.chk_sw_fp.Checked = true;
            this.chk_sw_fp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_sw_fp.ForeColor = System.Drawing.Color.Blue;
            this.chk_sw_fp.Location = new System.Drawing.Point(6, 180);
            this.chk_sw_fp.Name = "chk_sw_fp";
            this.chk_sw_fp.Size = new System.Drawing.Size(260, 17);
            this.chk_sw_fp.TabIndex = 2;
            this.chk_sw_fp.Text = "SIDE WALK/FOOTPATH ON ONE SIDE";
            this.chk_sw_fp.UseVisualStyleBackColor = true;
            this.chk_sw_fp.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // grb_ana_wc
            // 
            this.grb_ana_wc.Controls.Add(this.label511);
            this.grb_ana_wc.Controls.Add(this.txt_Ana_gamma_w);
            this.grb_ana_wc.Controls.Add(this.label515);
            this.grb_ana_wc.Controls.Add(this.label520);
            this.grb_ana_wc.Controls.Add(this.txt_Ana_Dw);
            this.grb_ana_wc.Controls.Add(this.label521);
            this.grb_ana_wc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_wc.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_wc.Location = new System.Drawing.Point(6, 29);
            this.grb_ana_wc.Name = "grb_ana_wc";
            this.grb_ana_wc.Size = new System.Drawing.Size(356, 64);
            this.grb_ana_wc.TabIndex = 1;
            this.grb_ana_wc.TabStop = false;
            // 
            // label511
            // 
            this.label511.AutoSize = true;
            this.label511.Location = new System.Drawing.Point(295, 44);
            this.label511.Name = "label511";
            this.label511.Size = new System.Drawing.Size(55, 13);
            this.label511.TabIndex = 5;
            this.label511.Text = "kN/cu.m";
            // 
            // txt_Ana_gamma_w
            // 
            this.txt_Ana_gamma_w.Location = new System.Drawing.Point(239, 41);
            this.txt_Ana_gamma_w.Name = "txt_Ana_gamma_w";
            this.txt_Ana_gamma_w.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_gamma_w.TabIndex = 1;
            this.txt_Ana_gamma_w.Text = "22.000";
            this.txt_Ana_gamma_w.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_gamma_w.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label515
            // 
            this.label515.AutoSize = true;
            this.label515.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label515.Location = new System.Drawing.Point(5, 44);
            this.label515.Name = "label515";
            this.label515.Size = new System.Drawing.Size(217, 13);
            this.label515.TabIndex = 3;
            this.label515.Text = "Unit weight of Wearing Course [Y_w]";
            // 
            // label520
            // 
            this.label520.AutoSize = true;
            this.label520.Location = new System.Drawing.Point(295, 17);
            this.label520.Name = "label520";
            this.label520.Size = new System.Drawing.Size(18, 13);
            this.label520.TabIndex = 2;
            this.label520.Text = "m";
            // 
            // txt_Ana_Dw
            // 
            this.txt_Ana_Dw.Location = new System.Drawing.Point(239, 14);
            this.txt_Ana_Dw.Name = "txt_Ana_Dw";
            this.txt_Ana_Dw.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Dw.TabIndex = 0;
            this.txt_Ana_Dw.Text = "0.080";
            this.txt_Ana_Dw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Dw.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label521
            // 
            this.label521.AutoSize = true;
            this.label521.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label521.Location = new System.Drawing.Point(5, 17);
            this.label521.Name = "label521";
            this.label521.Size = new System.Drawing.Size(209, 13);
            this.label521.TabIndex = 0;
            this.label521.Text = "Thickness of Wearing Course  [Dw]";
            // 
            // chk_parapet
            // 
            this.chk_parapet.AutoSize = true;
            this.chk_parapet.Checked = true;
            this.chk_parapet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_parapet.ForeColor = System.Drawing.Color.Blue;
            this.chk_parapet.Location = new System.Drawing.Point(6, 99);
            this.chk_parapet.Name = "chk_parapet";
            this.chk_parapet.Size = new System.Drawing.Size(226, 17);
            this.chk_parapet.TabIndex = 2;
            this.chk_parapet.Text = "PARAPET WALL ON BOTH SIDES";
            this.chk_parapet.UseVisualStyleBackColor = true;
            this.chk_parapet.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // grb_ana_parapet
            // 
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_Hp);
            this.grb_ana_parapet.Controls.Add(this.label514);
            this.grb_ana_parapet.Controls.Add(this.label510);
            this.grb_ana_parapet.Controls.Add(this.label522);
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_Wp);
            this.grb_ana_parapet.Controls.Add(this.label523);
            this.grb_ana_parapet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_parapet.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_parapet.Location = new System.Drawing.Point(6, 111);
            this.grb_ana_parapet.Name = "grb_ana_parapet";
            this.grb_ana_parapet.Size = new System.Drawing.Size(356, 69);
            this.grb_ana_parapet.TabIndex = 1;
            this.grb_ana_parapet.TabStop = false;
            // 
            // txt_Ana_Hp
            // 
            this.txt_Ana_Hp.Location = new System.Drawing.Point(239, 41);
            this.txt_Ana_Hp.Name = "txt_Ana_Hp";
            this.txt_Ana_Hp.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hp.TabIndex = 1;
            this.txt_Ana_Hp.Text = "1.200";
            this.txt_Ana_Hp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hp.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label514
            // 
            this.label514.AutoSize = true;
            this.label514.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label514.Location = new System.Drawing.Point(5, 44);
            this.label514.Name = "label514";
            this.label514.Size = new System.Drawing.Size(166, 13);
            this.label514.TabIndex = 3;
            this.label514.Text = "Height of Parapet Wall  [Hp]";
            // 
            // label510
            // 
            this.label510.AutoSize = true;
            this.label510.Location = new System.Drawing.Point(295, 44);
            this.label510.Name = "label510";
            this.label510.Size = new System.Drawing.Size(18, 13);
            this.label510.TabIndex = 2;
            this.label510.Text = "m";
            // 
            // label522
            // 
            this.label522.AutoSize = true;
            this.label522.Location = new System.Drawing.Point(295, 17);
            this.label522.Name = "label522";
            this.label522.Size = new System.Drawing.Size(18, 13);
            this.label522.TabIndex = 2;
            this.label522.Text = "m";
            // 
            // txt_Ana_Wp
            // 
            this.txt_Ana_Wp.Location = new System.Drawing.Point(239, 14);
            this.txt_Ana_Wp.Name = "txt_Ana_Wp";
            this.txt_Ana_Wp.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wp.TabIndex = 0;
            this.txt_Ana_Wp.Text = " 0.500";
            this.txt_Ana_Wp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wp.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label523
            // 
            this.label523.AutoSize = true;
            this.label523.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label523.Location = new System.Drawing.Point(5, 17);
            this.label523.Name = "label523";
            this.label523.Size = new System.Drawing.Size(185, 13);
            this.label523.TabIndex = 0;
            this.label523.Text = "Thickness of Parapet Wall [Wp]";
            // 
            // grb_ana_sw_fp
            // 
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_Hps);
            this.grb_ana_sw_fp.Controls.Add(this.label531);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_Wps);
            this.grb_ana_sw_fp.Controls.Add(this.label529);
            this.grb_ana_sw_fp.Controls.Add(this.label530);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_Hs);
            this.grb_ana_sw_fp.Controls.Add(this.label528);
            this.grb_ana_sw_fp.Controls.Add(this.label524);
            this.grb_ana_sw_fp.Controls.Add(this.label525);
            this.grb_ana_sw_fp.Controls.Add(this.label526);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_Bs);
            this.grb_ana_sw_fp.Controls.Add(this.label527);
            this.grb_ana_sw_fp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_sw_fp.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_sw_fp.Location = new System.Drawing.Point(6, 192);
            this.grb_ana_sw_fp.Name = "grb_ana_sw_fp";
            this.grb_ana_sw_fp.Size = new System.Drawing.Size(356, 116);
            this.grb_ana_sw_fp.TabIndex = 1;
            this.grb_ana_sw_fp.TabStop = false;
            // 
            // txt_Ana_Hps
            // 
            this.txt_Ana_Hps.Location = new System.Drawing.Point(239, 91);
            this.txt_Ana_Hps.Name = "txt_Ana_Hps";
            this.txt_Ana_Hps.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hps.TabIndex = 1;
            this.txt_Ana_Hps.Text = "1.000";
            this.txt_Ana_Hps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hps.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label531
            // 
            this.label531.AutoSize = true;
            this.label531.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label531.Location = new System.Drawing.Point(5, 94);
            this.label531.Name = "label531";
            this.label531.Size = new System.Drawing.Size(211, 13);
            this.label531.TabIndex = 3;
            this.label531.Text = "Side walk Parapet Wall height [Hps]";
            // 
            // txt_Ana_Wps
            // 
            this.txt_Ana_Wps.Location = new System.Drawing.Point(239, 64);
            this.txt_Ana_Wps.Name = "txt_Ana_Wps";
            this.txt_Ana_Wps.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wps.TabIndex = 1;
            this.txt_Ana_Wps.Text = "0.500";
            this.txt_Ana_Wps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wps.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label529
            // 
            this.label529.AutoSize = true;
            this.label529.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label529.Location = new System.Drawing.Point(5, 67);
            this.label529.Name = "label529";
            this.label529.Size = new System.Drawing.Size(209, 13);
            this.label529.TabIndex = 3;
            this.label529.Text = "Side walk Parapet Wall width [Wps]";
            // 
            // label530
            // 
            this.label530.AutoSize = true;
            this.label530.Location = new System.Drawing.Point(295, 94);
            this.label530.Name = "label530";
            this.label530.Size = new System.Drawing.Size(18, 13);
            this.label530.TabIndex = 2;
            this.label530.Text = "m";
            // 
            // txt_Ana_Hs
            // 
            this.txt_Ana_Hs.Location = new System.Drawing.Point(239, 37);
            this.txt_Ana_Hs.Name = "txt_Ana_Hs";
            this.txt_Ana_Hs.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hs.TabIndex = 1;
            this.txt_Ana_Hs.Text = "0.250";
            this.txt_Ana_Hs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hs.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label528
            // 
            this.label528.AutoSize = true;
            this.label528.Location = new System.Drawing.Point(295, 67);
            this.label528.Name = "label528";
            this.label528.Size = new System.Drawing.Size(18, 13);
            this.label528.TabIndex = 2;
            this.label528.Text = "m";
            // 
            // label524
            // 
            this.label524.AutoSize = true;
            this.label524.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label524.Location = new System.Drawing.Point(5, 40);
            this.label524.Name = "label524";
            this.label524.Size = new System.Drawing.Size(129, 13);
            this.label524.TabIndex = 3;
            this.label524.Text = "Side walk height [Hs]";
            // 
            // label525
            // 
            this.label525.AutoSize = true;
            this.label525.Location = new System.Drawing.Point(295, 40);
            this.label525.Name = "label525";
            this.label525.Size = new System.Drawing.Size(18, 13);
            this.label525.TabIndex = 2;
            this.label525.Text = "m";
            // 
            // label526
            // 
            this.label526.AutoSize = true;
            this.label526.Location = new System.Drawing.Point(295, 13);
            this.label526.Name = "label526";
            this.label526.Size = new System.Drawing.Size(18, 13);
            this.label526.TabIndex = 2;
            this.label526.Text = "m";
            // 
            // txt_Ana_Bs
            // 
            this.txt_Ana_Bs.Location = new System.Drawing.Point(239, 10);
            this.txt_Ana_Bs.Name = "txt_Ana_Bs";
            this.txt_Ana_Bs.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Bs.TabIndex = 0;
            this.txt_Ana_Bs.Text = "1.000";
            this.txt_Ana_Bs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Bs.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label527
            // 
            this.label527.AutoSize = true;
            this.label527.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label527.Location = new System.Drawing.Point(5, 13);
            this.label527.Name = "label527";
            this.label527.Size = new System.Drawing.Size(124, 13);
            this.label527.TabIndex = 0;
            this.label527.Text = "Side walk width [Bs]";
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.grb_create_input_data);
            this.groupBox37.Controls.Add(this.groupBox31);
            this.groupBox37.Controls.Add(this.groupBox32);
            this.groupBox37.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox37.ForeColor = System.Drawing.Color.Black;
            this.groupBox37.Location = new System.Drawing.Point(6, 6);
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.Size = new System.Drawing.Size(426, 585);
            this.groupBox37.TabIndex = 94;
            this.groupBox37.TabStop = false;
            this.groupBox37.Text = "DEAD LOAD [DL]";
            // 
            // grb_create_input_data
            // 
            this.grb_create_input_data.Controls.Add(this.label211);
            this.grb_create_input_data.Controls.Add(this.label212);
            this.grb_create_input_data.Controls.Add(this.label11);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_ang);
            this.grb_create_input_data.Controls.Add(this.label6);
            this.grb_create_input_data.Controls.Add(this.label33);
            this.grb_create_input_data.Controls.Add(this.label10);
            this.grb_create_input_data.Controls.Add(this.label5);
            this.grb_create_input_data.Controls.Add(this.label32);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_gamma_c);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Ds);
            this.grb_create_input_data.Controls.Add(this.label503);
            this.grb_create_input_data.Controls.Add(this.label501);
            this.grb_create_input_data.Controls.Add(this.label499);
            this.grb_create_input_data.Controls.Add(this.label3);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_CR);
            this.grb_create_input_data.Controls.Add(this.label502);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_CL);
            this.grb_create_input_data.Controls.Add(this.label500);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_CW);
            this.grb_create_input_data.Controls.Add(this.label498);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_B);
            this.grb_create_input_data.Controls.Add(this.label4);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_L);
            this.grb_create_input_data.Controls.Add(this.label1);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.ForeColor = System.Drawing.Color.Blue;
            this.grb_create_input_data.Location = new System.Drawing.Point(6, 20);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(416, 302);
            this.grb_create_input_data.TabIndex = 1;
            this.grb_create_input_data.TabStop = false;
            this.grb_create_input_data.Text = "RCC DECK SLAB";
            // 
            // label211
            // 
            this.label211.AutoSize = true;
            this.label211.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label211.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label211.ForeColor = System.Drawing.Color.Red;
            this.label211.Location = new System.Drawing.Point(152, 25);
            this.label211.Name = "label211";
            this.label211.Size = new System.Drawing.Size(218, 18);
            this.label211.TabIndex = 171;
            this.label211.Text = "Default Sample Data are shown";
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label212.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label212.ForeColor = System.Drawing.Color.Green;
            this.label212.Location = new System.Drawing.Point(11, 25);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(135, 18);
            this.label212.TabIndex = 170;
            this.label212.Text = "All User Input Data";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(351, 269);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "deg";
            // 
            // txt_Ana_ang
            // 
            this.txt_Ana_ang.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_ang.Location = new System.Drawing.Point(295, 266);
            this.txt_Ana_ang.Name = "txt_Ana_ang";
            this.txt_Ana_ang.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_ang.TabIndex = 6;
            this.txt_Ana_ang.Text = "0";
            this.txt_Ana_ang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_ang.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "kN/cu.m";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(351, 215);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(19, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "m";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Skew Angle [Ang]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Unit weight of Concrete [Y_c]";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(4, 212);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(169, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Thickness of Deck Slab [Ds]";
            // 
            // txt_Ana_gamma_c
            // 
            this.txt_Ana_gamma_c.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_gamma_c.Location = new System.Drawing.Point(295, 239);
            this.txt_Ana_gamma_c.Name = "txt_Ana_gamma_c";
            this.txt_Ana_gamma_c.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_gamma_c.TabIndex = 3;
            this.txt_Ana_gamma_c.Text = "24.000";
            this.txt_Ana_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_gamma_c.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // txt_Ana_Ds
            // 
            this.txt_Ana_Ds.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_Ds.Location = new System.Drawing.Point(295, 212);
            this.txt_Ana_Ds.Name = "txt_Ana_Ds";
            this.txt_Ana_Ds.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Ds.TabIndex = 3;
            this.txt_Ana_Ds.Text = "0.250";
            this.txt_Ana_Ds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Ds.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label503
            // 
            this.label503.AutoSize = true;
            this.label503.Location = new System.Drawing.Point(351, 188);
            this.label503.Name = "label503";
            this.label503.Size = new System.Drawing.Size(19, 13);
            this.label503.TabIndex = 5;
            this.label503.Text = "m";
            // 
            // label501
            // 
            this.label501.AutoSize = true;
            this.label501.Location = new System.Drawing.Point(351, 155);
            this.label501.Name = "label501";
            this.label501.Size = new System.Drawing.Size(19, 13);
            this.label501.TabIndex = 5;
            this.label501.Text = "m";
            // 
            // label499
            // 
            this.label499.AutoSize = true;
            this.label499.Location = new System.Drawing.Point(351, 111);
            this.label499.Name = "label499";
            this.label499.Size = new System.Drawing.Size(19, 13);
            this.label499.TabIndex = 5;
            this.label499.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(351, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "m";
            // 
            // txt_Ana_CR
            // 
            this.txt_Ana_CR.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_CR.Location = new System.Drawing.Point(295, 185);
            this.txt_Ana_CR.Name = "txt_Ana_CR";
            this.txt_Ana_CR.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_CR.TabIndex = 1;
            this.txt_Ana_CR.Text = "1.250";
            this.txt_Ana_CR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_CR.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label502
            // 
            this.label502.AutoSize = true;
            this.label502.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label502.Location = new System.Drawing.Point(4, 177);
            this.label502.Name = "label502";
            this.label502.Size = new System.Drawing.Size(285, 26);
            this.label502.TabIndex = 3;
            this.label502.Text = "Width of Right Cantilever part of Deck Slab [CR]\r\n(must be < Width of Bridge Deck" +
    "/3)";
            // 
            // txt_Ana_CL
            // 
            this.txt_Ana_CL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_CL.Location = new System.Drawing.Point(295, 152);
            this.txt_Ana_CL.Name = "txt_Ana_CL";
            this.txt_Ana_CL.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_CL.TabIndex = 1;
            this.txt_Ana_CL.Text = "1.250";
            this.txt_Ana_CL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_CL.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label500
            // 
            this.label500.AutoSize = true;
            this.label500.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label500.Location = new System.Drawing.Point(4, 144);
            this.label500.Name = "label500";
            this.label500.Size = new System.Drawing.Size(275, 26);
            this.label500.TabIndex = 3;
            this.label500.Text = "Width of Left Cantilever part of Deck Slab [CL]\r\n(must be < Width of Bridge Deck/" +
    "3)";
            // 
            // txt_Ana_CW
            // 
            this.txt_Ana_CW.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_CW.Location = new System.Drawing.Point(295, 108);
            this.txt_Ana_CW.Name = "txt_Ana_CW";
            this.txt_Ana_CW.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_CW.TabIndex = 1;
            this.txt_Ana_CW.Text = "10.00";
            this.txt_Ana_CW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_CW.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label498
            // 
            this.label498.AutoSize = true;
            this.label498.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label498.Location = new System.Drawing.Point(4, 108);
            this.label498.Name = "label498";
            this.label498.Size = new System.Drawing.Size(205, 26);
            this.label498.TabIndex = 3;
            this.label498.Text = "Carriageway Width [CW]\r\n (must be < Width of Bridge Deck)";
            // 
            // txt_Ana_B
            // 
            this.txt_Ana_B.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_B.Location = new System.Drawing.Point(295, 81);
            this.txt_Ana_B.Name = "txt_Ana_B";
            this.txt_Ana_B.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_B.TabIndex = 1;
            this.txt_Ana_B.Text = "12.500";
            this.txt_Ana_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_B.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Width of Bridge Deck  (along Z-direction) [B]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "m";
            // 
            // txt_Ana_L
            // 
            this.txt_Ana_L.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_L.Location = new System.Drawing.Point(295, 54);
            this.txt_Ana_L.Name = "txt_Ana_L";
            this.txt_Ana_L.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_L.TabIndex = 0;
            this.txt_Ana_L.Text = "26.0";
            this.txt_Ana_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_L.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Length of Deck Span (along X-direction)  [L]";
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.label512);
            this.groupBox31.Controls.Add(this.label513);
            this.groupBox31.Controls.Add(this.txt_Ana_BMG);
            this.groupBox31.Controls.Add(this.label516);
            this.groupBox31.Controls.Add(this.txt_Ana_DMG);
            this.groupBox31.Controls.Add(this.label517);
            this.groupBox31.Controls.Add(this.txt_Ana_NMG);
            this.groupBox31.Controls.Add(this.label519);
            this.groupBox31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox31.ForeColor = System.Drawing.Color.Blue;
            this.groupBox31.Location = new System.Drawing.Point(3, 338);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(416, 118);
            this.groupBox31.TabIndex = 1;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "LONG MAIN GIRDERS";
            // 
            // label512
            // 
            this.label512.AutoSize = true;
            this.label512.Location = new System.Drawing.Point(353, 77);
            this.label512.Name = "label512";
            this.label512.Size = new System.Drawing.Size(19, 13);
            this.label512.TabIndex = 5;
            this.label512.Text = "m";
            // 
            // label513
            // 
            this.label513.AutoSize = true;
            this.label513.Location = new System.Drawing.Point(353, 50);
            this.label513.Name = "label513";
            this.label513.Size = new System.Drawing.Size(19, 13);
            this.label513.TabIndex = 5;
            this.label513.Text = "m";
            // 
            // txt_Ana_BMG
            // 
            this.txt_Ana_BMG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_BMG.Location = new System.Drawing.Point(297, 74);
            this.txt_Ana_BMG.Name = "txt_Ana_BMG";
            this.txt_Ana_BMG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_BMG.TabIndex = 1;
            this.txt_Ana_BMG.Text = "0.300";
            this.txt_Ana_BMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_BMG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label516
            // 
            this.label516.AutoSize = true;
            this.label516.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label516.Location = new System.Drawing.Point(6, 77);
            this.label516.Name = "label516";
            this.label516.Size = new System.Drawing.Size(247, 13);
            this.label516.TabIndex = 3;
            this.label516.Text = "Web thickness of main long girders [BMG]";
            // 
            // txt_Ana_DMG
            // 
            this.txt_Ana_DMG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_DMG.Location = new System.Drawing.Point(297, 47);
            this.txt_Ana_DMG.Name = "txt_Ana_DMG";
            this.txt_Ana_DMG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_DMG.TabIndex = 1;
            this.txt_Ana_DMG.Text = "2.80";
            this.txt_Ana_DMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DMG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label517
            // 
            this.label517.AutoSize = true;
            this.label517.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label517.Location = new System.Drawing.Point(6, 50);
            this.label517.Name = "label517";
            this.label517.Size = new System.Drawing.Size(201, 13);
            this.label517.TabIndex = 3;
            this.label517.Text = "Depth of main long girders [DMG]";
            // 
            // txt_Ana_NMG
            // 
            this.txt_Ana_NMG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_NMG.Location = new System.Drawing.Point(297, 14);
            this.txt_Ana_NMG.Name = "txt_Ana_NMG";
            this.txt_Ana_NMG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_NMG.TabIndex = 0;
            this.txt_Ana_NMG.Text = "4";
            this.txt_Ana_NMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_NMG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label519
            // 
            this.label519.AutoSize = true;
            this.label519.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label519.Location = new System.Drawing.Point(6, 17);
            this.label519.Name = "label519";
            this.label519.Size = new System.Drawing.Size(241, 26);
            this.label519.TabIndex = 0;
            this.label519.Text = "Total number of main long girders [NMG]\r\n (must be >= 3)";
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.label504);
            this.groupBox32.Controls.Add(this.label505);
            this.groupBox32.Controls.Add(this.txt_Ana_BCG);
            this.groupBox32.Controls.Add(this.label506);
            this.groupBox32.Controls.Add(this.txt_Ana_DCG);
            this.groupBox32.Controls.Add(this.label507);
            this.groupBox32.Controls.Add(this.txt_Ana_NCG);
            this.groupBox32.Controls.Add(this.label509);
            this.groupBox32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox32.ForeColor = System.Drawing.Color.Blue;
            this.groupBox32.Location = new System.Drawing.Point(3, 474);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(417, 100);
            this.groupBox32.TabIndex = 1;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "CROSS GIRDERS";
            // 
            // label504
            // 
            this.label504.AutoSize = true;
            this.label504.Location = new System.Drawing.Point(353, 77);
            this.label504.Name = "label504";
            this.label504.Size = new System.Drawing.Size(19, 13);
            this.label504.TabIndex = 5;
            this.label504.Text = "m";
            // 
            // label505
            // 
            this.label505.AutoSize = true;
            this.label505.Location = new System.Drawing.Point(353, 50);
            this.label505.Name = "label505";
            this.label505.Size = new System.Drawing.Size(19, 13);
            this.label505.TabIndex = 5;
            this.label505.Text = "m";
            // 
            // txt_Ana_BCG
            // 
            this.txt_Ana_BCG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_BCG.Location = new System.Drawing.Point(297, 74);
            this.txt_Ana_BCG.Name = "txt_Ana_BCG";
            this.txt_Ana_BCG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_BCG.TabIndex = 1;
            this.txt_Ana_BCG.Text = "0.250";
            this.txt_Ana_BCG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_BCG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label506
            // 
            this.label506.AutoSize = true;
            this.label506.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label506.Location = new System.Drawing.Point(6, 77);
            this.label506.Name = "label506";
            this.label506.Size = new System.Drawing.Size(221, 13);
            this.label506.TabIndex = 3;
            this.label506.Text = "Web thickness of cross girders [BCG]";
            // 
            // txt_Ana_DCG
            // 
            this.txt_Ana_DCG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_DCG.Location = new System.Drawing.Point(297, 47);
            this.txt_Ana_DCG.Name = "txt_Ana_DCG";
            this.txt_Ana_DCG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_DCG.TabIndex = 1;
            this.txt_Ana_DCG.Text = "1.5";
            this.txt_Ana_DCG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DCG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label507
            // 
            this.label507.AutoSize = true;
            this.label507.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label507.Location = new System.Drawing.Point(6, 50);
            this.label507.Name = "label507";
            this.label507.Size = new System.Drawing.Size(179, 13);
            this.label507.TabIndex = 3;
            this.label507.Text = "Depth of cross girders  [DCG]";
            // 
            // txt_Ana_NCG
            // 
            this.txt_Ana_NCG.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_NCG.Location = new System.Drawing.Point(297, 14);
            this.txt_Ana_NCG.Name = "txt_Ana_NCG";
            this.txt_Ana_NCG.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_NCG.TabIndex = 0;
            this.txt_Ana_NCG.Text = "3";
            this.txt_Ana_NCG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_NCG.TextChanged += new System.EventHandler(this.txt_Ana_width_TextChanged);
            // 
            // label509
            // 
            this.label509.AutoSize = true;
            this.label509.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label509.Location = new System.Drawing.Point(6, 17);
            this.label509.Name = "label509";
            this.label509.Size = new System.Drawing.Size(218, 26);
            this.label509.TabIndex = 0;
            this.label509.Text = "Total number of Cross girders [NCG]\r\n(must be >= 3)";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.grb_select_analysis);
            this.groupBox12.Controls.Add(this.rbtn_ana_create_analysis_file);
            this.groupBox12.Controls.Add(this.rbtn_ana_select_analysis_file);
            this.groupBox12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.Location = new System.Drawing.Point(9, 25);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(426, 11);
            this.groupBox12.TabIndex = 91;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Analysis Input Data";
            this.groupBox12.Visible = false;
            // 
            // grb_select_analysis
            // 
            this.grb_select_analysis.Controls.Add(this.txt_analysis_file);
            this.grb_select_analysis.Controls.Add(this.btn_ana_browse_input_file);
            this.grb_select_analysis.Enabled = false;
            this.grb_select_analysis.Location = new System.Drawing.Point(10, 55);
            this.grb_select_analysis.Name = "grb_select_analysis";
            this.grb_select_analysis.Size = new System.Drawing.Size(404, 47);
            this.grb_select_analysis.TabIndex = 52;
            this.grb_select_analysis.TabStop = false;
            this.grb_select_analysis.Text = "Select Analysis Input Data from File";
            // 
            // txt_analysis_file
            // 
            this.txt_analysis_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_analysis_file.Location = new System.Drawing.Point(11, 21);
            this.txt_analysis_file.Name = "txt_analysis_file";
            this.txt_analysis_file.ReadOnly = true;
            this.txt_analysis_file.Size = new System.Drawing.Size(320, 21);
            this.txt_analysis_file.TabIndex = 47;
            // 
            // btn_ana_browse_input_file
            // 
            this.btn_ana_browse_input_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ana_browse_input_file.Location = new System.Drawing.Point(337, 19);
            this.btn_ana_browse_input_file.Name = "btn_ana_browse_input_file";
            this.btn_ana_browse_input_file.Size = new System.Drawing.Size(61, 23);
            this.btn_ana_browse_input_file.TabIndex = 35;
            this.btn_ana_browse_input_file.Text = "Browse";
            this.btn_ana_browse_input_file.UseVisualStyleBackColor = true;
            this.btn_ana_browse_input_file.Click += new System.EventHandler(this.btn_Ana_browse_input_file_Click);
            // 
            // rbtn_ana_create_analysis_file
            // 
            this.rbtn_ana_create_analysis_file.AutoSize = true;
            this.rbtn_ana_create_analysis_file.Checked = true;
            this.rbtn_ana_create_analysis_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_ana_create_analysis_file.Location = new System.Drawing.Point(12, 17);
            this.rbtn_ana_create_analysis_file.Name = "rbtn_ana_create_analysis_file";
            this.rbtn_ana_create_analysis_file.Size = new System.Drawing.Size(314, 17);
            this.rbtn_ana_create_analysis_file.TabIndex = 50;
            this.rbtn_ana_create_analysis_file.TabStop = true;
            this.rbtn_ana_create_analysis_file.Text = "Create Analysis Input Data File (INPUT_DATA.TXT)";
            this.rbtn_ana_create_analysis_file.UseVisualStyleBackColor = true;
            this.rbtn_ana_create_analysis_file.Click += new System.EventHandler(this.rbtn_Ana_select_analysis_file_CheckedChanged);
            // 
            // rbtn_ana_select_analysis_file
            // 
            this.rbtn_ana_select_analysis_file.AutoSize = true;
            this.rbtn_ana_select_analysis_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_ana_select_analysis_file.Location = new System.Drawing.Point(12, 35);
            this.rbtn_ana_select_analysis_file.Name = "rbtn_ana_select_analysis_file";
            this.rbtn_ana_select_analysis_file.Size = new System.Drawing.Size(272, 17);
            this.rbtn_ana_select_analysis_file.TabIndex = 50;
            this.rbtn_ana_select_analysis_file.Text = "Open Analysis Data File (INPUT_DATA.TXT)";
            this.rbtn_ana_select_analysis_file.UseVisualStyleBackColor = true;
            this.rbtn_ana_select_analysis_file.Click += new System.EventHandler(this.rbtn_Ana_select_analysis_file_CheckedChanged);
            // 
            // tab_result
            // 
            this.tab_result.Controls.Add(this.splitContainer1);
            this.tab_result.Location = new System.Drawing.Point(4, 22);
            this.tab_result.Name = "tab_result";
            this.tab_result.Size = new System.Drawing.Size(940, 642);
            this.tab_result.TabIndex = 3;
            this.tab_result.Text = "Analysis Process";
            this.tab_result.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl4);
            this.splitContainer1.Size = new System.Drawing.Size(940, 642);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.TabIndex = 97;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox21);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 130);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_View_Moving_Load);
            this.groupBox2.Controls.Add(this.btn_view_report);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.btn_process_analysis);
            this.groupBox2.Controls.Add(this.btn_create_data);
            this.groupBox2.Controls.Add(this.btn_view_data);
            this.groupBox2.Controls.Add(this.btn_view_structure);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 129);
            this.groupBox2.TabIndex = 95;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis";
            // 
            // btn_View_Moving_Load
            // 
            this.btn_View_Moving_Load.Location = new System.Drawing.Point(342, 88);
            this.btn_View_Moving_Load.Name = "btn_View_Moving_Load";
            this.btn_View_Moving_Load.Size = new System.Drawing.Size(149, 30);
            this.btn_View_Moving_Load.TabIndex = 78;
            this.btn_View_Moving_Load.Text = "View Moving Load";
            this.btn_View_Moving_Load.UseVisualStyleBackColor = true;
            this.btn_View_Moving_Load.Click += new System.EventHandler(this.btn_Ana_View_Moving_Load_Click);
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(178, 88);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(137, 30);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_Ana_view_report_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Location = new System.Drawing.Point(73, 45);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(326, 39);
            this.label38.TabIndex = 57;
            this.label38.Text = "After Clicking on button \"Create Analysis Data\", User \r\nmay use \"View Structure\" " +
    "and \"View Analysis Data\" for \r\nany Desired modifications followed by saving the " +
    "data.";
            // 
            // btn_process_analysis
            // 
            this.btn_process_analysis.Location = new System.Drawing.Point(6, 88);
            this.btn_process_analysis.Name = "btn_process_analysis";
            this.btn_process_analysis.Size = new System.Drawing.Size(137, 30);
            this.btn_process_analysis.TabIndex = 75;
            this.btn_process_analysis.Text = "Process Analysis";
            this.btn_process_analysis.UseVisualStyleBackColor = true;
            this.btn_process_analysis.Click += new System.EventHandler(this.btn_Ana_process_analysis_Click);
            // 
            // btn_create_data
            // 
            this.btn_create_data.Location = new System.Drawing.Point(6, 19);
            this.btn_create_data.Name = "btn_create_data";
            this.btn_create_data.Size = new System.Drawing.Size(137, 23);
            this.btn_create_data.TabIndex = 46;
            this.btn_create_data.Text = "Create Analysis Data";
            this.btn_create_data.UseVisualStyleBackColor = true;
            this.btn_create_data.Click += new System.EventHandler(this.btn_Ana_create_data_Click);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(178, 19);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(137, 23);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_Ana_view_data_Click);
            // 
            // btn_view_structure
            // 
            this.btn_view_structure.Location = new System.Drawing.Point(342, 19);
            this.btn_view_structure.Name = "btn_view_structure";
            this.btn_view_structure.Size = new System.Drawing.Size(149, 23);
            this.btn_view_structure.TabIndex = 74;
            this.btn_view_structure.Text = "View Structure";
            this.btn_view_structure.UseVisualStyleBackColor = true;
            this.btn_view_structure.Click += new System.EventHandler(this.btn_Ana_view_structure_Click);
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.btn_update_force);
            this.groupBox21.Controls.Add(this.groupBox23);
            this.groupBox21.Controls.Add(this.groupBox22);
            this.groupBox21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox21.ForeColor = System.Drawing.Color.Navy;
            this.groupBox21.Location = new System.Drawing.Point(528, 3);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(399, 129);
            this.groupBox21.TabIndex = 96;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Read Analysis For";
            // 
            // btn_update_force
            // 
            this.btn_update_force.Location = new System.Drawing.Point(112, 90);
            this.btn_update_force.Name = "btn_update_force";
            this.btn_update_force.Size = new System.Drawing.Size(192, 26);
            this.btn_update_force.TabIndex = 99;
            this.btn_update_force.Text = "UPDATE FORCES (Optional)";
            this.btn_update_force.UseVisualStyleBackColor = true;
            this.btn_update_force.Click += new System.EventHandler(this.btn_update_force_Click);
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.chk_R2);
            this.groupBox23.Controls.Add(this.chk_R3);
            this.groupBox23.Location = new System.Drawing.Point(215, 29);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(176, 45);
            this.groupBox23.TabIndex = 98;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Maximum Shear Forces";
            // 
            // chk_R2
            // 
            this.chk_R2.AutoSize = true;
            this.chk_R2.Checked = true;
            this.chk_R2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_R2.Location = new System.Drawing.Point(102, 19);
            this.chk_R2.Name = "chk_R2";
            this.chk_R2.Size = new System.Drawing.Size(41, 17);
            this.chk_R2.TabIndex = 55;
            this.chk_R2.Text = "R2";
            this.chk_R2.UseVisualStyleBackColor = true;
            // 
            // chk_R3
            // 
            this.chk_R3.AutoSize = true;
            this.chk_R3.Checked = true;
            this.chk_R3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_R3.Location = new System.Drawing.Point(12, 19);
            this.chk_R3.Name = "chk_R3";
            this.chk_R3.Size = new System.Drawing.Size(41, 17);
            this.chk_R3.TabIndex = 55;
            this.chk_R3.Text = "R3";
            this.chk_R3.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.chk_M2);
            this.groupBox22.Controls.Add(this.chk_M3);
            this.groupBox22.Location = new System.Drawing.Point(6, 29);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(198, 45);
            this.groupBox22.TabIndex = 97;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Maximum Bending Moments";
            // 
            // chk_M2
            // 
            this.chk_M2.AutoSize = true;
            this.chk_M2.Checked = true;
            this.chk_M2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_M2.Location = new System.Drawing.Point(106, 19);
            this.chk_M2.Name = "chk_M2";
            this.chk_M2.Size = new System.Drawing.Size(42, 17);
            this.chk_M2.TabIndex = 55;
            this.chk_M2.Text = "M2";
            this.chk_M2.UseVisualStyleBackColor = true;
            // 
            // chk_M3
            // 
            this.chk_M3.AutoSize = true;
            this.chk_M3.Checked = true;
            this.chk_M3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_M3.Location = new System.Drawing.Point(7, 19);
            this.chk_M3.Name = "chk_M3";
            this.chk_M3.Size = new System.Drawing.Size(42, 17);
            this.chk_M3.TabIndex = 55;
            this.chk_M3.Text = "M3";
            this.chk_M3.UseVisualStyleBackColor = true;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage6);
            this.tabControl4.Controls.Add(this.tabPage7);
            this.tabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl4.Location = new System.Drawing.Point(0, 0);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(936, 500);
            this.tabControl4.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox7);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(928, 474);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Analysis Results";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label222);
            this.groupBox7.Controls.Add(this.label213);
            this.groupBox7.Controls.Add(this.groupBox34);
            this.groupBox7.Controls.Add(this.groupBox33);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(922, 468);
            this.groupBox7.TabIndex = 94;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Analysis Result";
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label222.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label222.ForeColor = System.Drawing.Color.Green;
            this.label222.Location = new System.Drawing.Point(67, 17);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(229, 20);
            this.label222.TabIndex = 123;
            this.label222.Text = "No User Input in this page";
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label213.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label213.ForeColor = System.Drawing.Color.Red;
            this.label213.Location = new System.Drawing.Point(311, 17);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(273, 20);
            this.label213.TabIndex = 122;
            this.label213.Text = "Calculated Values from Analysis";
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.label534);
            this.groupBox34.Controls.Add(this.txt_ana_TSRP);
            this.groupBox34.Controls.Add(this.label535);
            this.groupBox34.Controls.Add(this.label536);
            this.groupBox34.Controls.Add(this.txt_ana_MSTD);
            this.groupBox34.Controls.Add(this.label537);
            this.groupBox34.Controls.Add(this.label538);
            this.groupBox34.Controls.Add(this.txt_ana_MSLD);
            this.groupBox34.Controls.Add(this.label539);
            this.groupBox34.Location = new System.Drawing.Point(178, 268);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(521, 131);
            this.groupBox34.TabIndex = 91;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "Design Forces";
            // 
            // label534
            // 
            this.label534.AutoSize = true;
            this.label534.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label534.Location = new System.Drawing.Point(458, 22);
            this.label534.Name = "label534";
            this.label534.Size = new System.Drawing.Size(24, 13);
            this.label534.TabIndex = 104;
            this.label534.Text = "kN";
            // 
            // txt_ana_TSRP
            // 
            this.txt_ana_TSRP.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_TSRP.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_TSRP.Location = new System.Drawing.Point(365, 19);
            this.txt_ana_TSRP.Name = "txt_ana_TSRP";
            this.txt_ana_TSRP.Size = new System.Drawing.Size(87, 20);
            this.txt_ana_TSRP.TabIndex = 5;
            this.txt_ana_TSRP.Text = "0";
            this.txt_ana_TSRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ana_TSRP.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label535
            // 
            this.label535.AutoSize = true;
            this.label535.Location = new System.Drawing.Point(5, 22);
            this.label535.Name = "label535";
            this.label535.Size = new System.Drawing.Size(237, 13);
            this.label535.TabIndex = 105;
            this.label535.Text = "Total Support Reaction on The Pier [W1]";
            // 
            // label536
            // 
            this.label536.AutoSize = true;
            this.label536.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label536.Location = new System.Drawing.Point(458, 97);
            this.label536.Name = "label536";
            this.label536.Size = new System.Drawing.Size(42, 13);
            this.label536.TabIndex = 101;
            this.label536.Text = "kN-m";
            // 
            // txt_ana_MSTD
            // 
            this.txt_ana_MSTD.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_MSTD.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_MSTD.Location = new System.Drawing.Point(364, 96);
            this.txt_ana_MSTD.Name = "txt_ana_MSTD";
            this.txt_ana_MSTD.Size = new System.Drawing.Size(88, 20);
            this.txt_ana_MSTD.TabIndex = 4;
            this.txt_ana_MSTD.Text = "0";
            this.txt_ana_MSTD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ana_MSTD.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label537
            // 
            this.label537.AutoSize = true;
            this.label537.Location = new System.Drawing.Point(7, 90);
            this.label537.Name = "label537";
            this.label537.Size = new System.Drawing.Size(161, 26);
            this.label537.TabIndex = 102;
            this.label537.Text = "Moment at Supports in \r\nTransverse Direction [Mz1]";
            // 
            // label538
            // 
            this.label538.AutoSize = true;
            this.label538.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label538.Location = new System.Drawing.Point(456, 63);
            this.label538.Name = "label538";
            this.label538.Size = new System.Drawing.Size(42, 13);
            this.label538.TabIndex = 98;
            this.label538.Text = "kN-m";
            // 
            // txt_ana_MSLD
            // 
            this.txt_ana_MSLD.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_MSLD.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_MSLD.Location = new System.Drawing.Point(365, 60);
            this.txt_ana_MSLD.Name = "txt_ana_MSLD";
            this.txt_ana_MSLD.Size = new System.Drawing.Size(88, 20);
            this.txt_ana_MSLD.TabIndex = 3;
            this.txt_ana_MSLD.Text = "0";
            this.txt_ana_MSLD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ana_MSLD.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label539
            // 
            this.label539.AutoSize = true;
            this.label539.Location = new System.Drawing.Point(7, 54);
            this.label539.Name = "label539";
            this.label539.Size = new System.Drawing.Size(167, 26);
            this.label539.TabIndex = 99;
            this.label539.Text = "Moment at Supports in \r\nLongitudinal Direction [Mx1]";
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.label9);
            this.groupBox33.Controls.Add(this.label12);
            this.groupBox33.Controls.Add(this.label541);
            this.groupBox33.Controls.Add(this.label540);
            this.groupBox33.Controls.Add(this.txt_ana_DLSR);
            this.groupBox33.Controls.Add(this.txt_ana_LLSR);
            this.groupBox33.Location = new System.Drawing.Point(178, 182);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(521, 80);
            this.groupBox33.TabIndex = 82;
            this.groupBox33.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(346, 13);
            this.label9.TabIndex = 110;
            this.label9.Text = "Live Load Support Reaction per unit width of Abutment/Pier";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(353, 13);
            this.label12.TabIndex = 109;
            this.label12.Text = "Dead Load Support Reaction per unit width of Abutment/Pier";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label541
            // 
            this.label541.AutoSize = true;
            this.label541.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label541.Location = new System.Drawing.Point(458, 51);
            this.label541.Name = "label541";
            this.label541.Size = new System.Drawing.Size(44, 13);
            this.label541.TabIndex = 104;
            this.label541.Text = "kN/m";
            // 
            // label540
            // 
            this.label540.AutoSize = true;
            this.label540.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label540.Location = new System.Drawing.Point(458, 25);
            this.label540.Name = "label540";
            this.label540.Size = new System.Drawing.Size(44, 13);
            this.label540.TabIndex = 104;
            this.label540.Text = "kN/m";
            // 
            // txt_ana_DLSR
            // 
            this.txt_ana_DLSR.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_DLSR.Location = new System.Drawing.Point(365, 21);
            this.txt_ana_DLSR.Name = "txt_ana_DLSR";
            this.txt_ana_DLSR.Size = new System.Drawing.Size(87, 21);
            this.txt_ana_DLSR.TabIndex = 83;
            this.txt_ana_DLSR.Text = "0";
            this.txt_ana_DLSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ana_DLSR.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // txt_ana_LLSR
            // 
            this.txt_ana_LLSR.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_LLSR.Location = new System.Drawing.Point(365, 48);
            this.txt_ana_LLSR.Name = "txt_ana_LLSR";
            this.txt_ana_LLSR.Size = new System.Drawing.Size(87, 21);
            this.txt_ana_LLSR.TabIndex = 85;
            this.txt_ana_LLSR.Text = "0";
            this.txt_ana_LLSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ana_LLSR.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.label28);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.label40);
            this.groupBox9.Controls.Add(this.label76);
            this.groupBox9.Controls.Add(this.label77);
            this.groupBox9.Controls.Add(this.label78);
            this.groupBox9.Controls.Add(this.label83);
            this.groupBox9.Controls.Add(this.label84);
            this.groupBox9.Controls.Add(this.txt_outer_long_L2_shear);
            this.groupBox9.Controls.Add(this.label102);
            this.groupBox9.Controls.Add(this.label103);
            this.groupBox9.Controls.Add(this.txt_outer_long_deff_shear);
            this.groupBox9.Controls.Add(this.txt_outer_long_L2_moment);
            this.groupBox9.Controls.Add(this.txt_outer_long_deff_moment);
            this.groupBox9.Controls.Add(this.txt_outer_long_L4_shear);
            this.groupBox9.Controls.Add(this.txt_outer_long_L4_moment);
            this.groupBox9.Location = new System.Drawing.Point(302, 63);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(295, 113);
            this.groupBox9.TabIndex = 82;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Outer Main Girder";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(254, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Ton";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 79);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 13);
            this.label28.TabIndex = 18;
            this.label28.Text = "Deff";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(254, 55);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(27, 13);
            this.label39.TabIndex = 30;
            this.label39.Text = "Ton";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 29);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(25, 13);
            this.label40.TabIndex = 24;
            this.label40.Text = "L/2";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(254, 29);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(27, 13);
            this.label76.TabIndex = 30;
            this.label76.Text = "Ton";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(168, 13);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(41, 13);
            this.label77.TabIndex = 28;
            this.label77.Text = "Shear";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(126, 82);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(29, 13);
            this.label78.TabIndex = 30;
            this.label78.Text = "T-m";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(40, 12);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(52, 13);
            this.label83.TabIndex = 27;
            this.label83.Text = "Moment";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(126, 57);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(29, 13);
            this.label84.TabIndex = 30;
            this.label84.Text = "T-m";
            // 
            // txt_outer_long_L2_shear
            // 
            this.txt_outer_long_L2_shear.Location = new System.Drawing.Point(171, 29);
            this.txt_outer_long_L2_shear.Name = "txt_outer_long_L2_shear";
            this.txt_outer_long_L2_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_L2_shear.TabIndex = 26;
            this.txt_outer_long_L2_shear.Text = "0";
            this.txt_outer_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(126, 31);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(29, 13);
            this.label102.TabIndex = 30;
            this.label102.Text = "T-m";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(6, 54);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(25, 13);
            this.label103.TabIndex = 21;
            this.label103.Text = "L/4";
            // 
            // txt_outer_long_deff_shear
            // 
            this.txt_outer_long_deff_shear.Location = new System.Drawing.Point(171, 78);
            this.txt_outer_long_deff_shear.Name = "txt_outer_long_deff_shear";
            this.txt_outer_long_deff_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_deff_shear.TabIndex = 20;
            this.txt_outer_long_deff_shear.Text = "0";
            this.txt_outer_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_outer_long_L2_moment
            // 
            this.txt_outer_long_L2_moment.Location = new System.Drawing.Point(43, 28);
            this.txt_outer_long_L2_moment.Name = "txt_outer_long_L2_moment";
            this.txt_outer_long_L2_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_L2_moment.TabIndex = 25;
            this.txt_outer_long_L2_moment.Text = "0";
            this.txt_outer_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_outer_long_deff_moment
            // 
            this.txt_outer_long_deff_moment.Location = new System.Drawing.Point(43, 77);
            this.txt_outer_long_deff_moment.Name = "txt_outer_long_deff_moment";
            this.txt_outer_long_deff_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_deff_moment.TabIndex = 19;
            this.txt_outer_long_deff_moment.Text = "0";
            this.txt_outer_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_outer_long_L4_shear
            // 
            this.txt_outer_long_L4_shear.Location = new System.Drawing.Point(171, 54);
            this.txt_outer_long_L4_shear.Name = "txt_outer_long_L4_shear";
            this.txt_outer_long_L4_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_L4_shear.TabIndex = 23;
            this.txt_outer_long_L4_shear.Text = "0";
            this.txt_outer_long_L4_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_outer_long_L4_moment
            // 
            this.txt_outer_long_L4_moment.Location = new System.Drawing.Point(43, 53);
            this.txt_outer_long_L4_moment.Name = "txt_outer_long_L4_moment";
            this.txt_outer_long_L4_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_outer_long_L4_moment.TabIndex = 22;
            this.txt_outer_long_L4_moment.Text = "0";
            this.txt_outer_long_L4_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label162);
            this.groupBox8.Controls.Add(this.label163);
            this.groupBox8.Controls.Add(this.label164);
            this.groupBox8.Controls.Add(this.label165);
            this.groupBox8.Controls.Add(this.txt_Ana_cross_max_shear);
            this.groupBox8.Controls.Add(this.txt_Ana_cross_max_moment);
            this.groupBox8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(603, 63);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(294, 67);
            this.groupBox8.TabIndex = 82;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Cross Girder";
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(125, 35);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(29, 13);
            this.label162.TabIndex = 31;
            this.label162.Text = "T-m";
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(253, 37);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(27, 13);
            this.label163.TabIndex = 30;
            this.label163.Text = "Ton";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Location = new System.Drawing.Point(167, 18);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(41, 13);
            this.label164.TabIndex = 28;
            this.label164.Text = "Shear";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(56, 16);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(52, 13);
            this.label165.TabIndex = 27;
            this.label165.Text = "Moment";
            // 
            // txt_Ana_cross_max_shear
            // 
            this.txt_Ana_cross_max_shear.Location = new System.Drawing.Point(170, 32);
            this.txt_Ana_cross_max_shear.Name = "txt_Ana_cross_max_shear";
            this.txt_Ana_cross_max_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_cross_max_shear.TabIndex = 20;
            this.txt_Ana_cross_max_shear.Text = "0";
            this.txt_Ana_cross_max_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_cross_max_moment
            // 
            this.txt_Ana_cross_max_moment.Location = new System.Drawing.Point(42, 32);
            this.txt_Ana_cross_max_moment.Name = "txt_Ana_cross_max_moment";
            this.txt_Ana_cross_max_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_cross_max_moment.TabIndex = 19;
            this.txt_Ana_cross_max_moment.Text = "0";
            this.txt_Ana_cross_max_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label166);
            this.groupBox10.Controls.Add(this.label167);
            this.groupBox10.Controls.Add(this.label198);
            this.groupBox10.Controls.Add(this.label199);
            this.groupBox10.Controls.Add(this.label200);
            this.groupBox10.Controls.Add(this.label201);
            this.groupBox10.Controls.Add(this.label202);
            this.groupBox10.Controls.Add(this.label203);
            this.groupBox10.Controls.Add(this.label204);
            this.groupBox10.Controls.Add(this.label205);
            this.groupBox10.Controls.Add(this.label206);
            this.groupBox10.Controls.Add(this.txt_Ana_inner_long_L4_shear);
            this.groupBox10.Controls.Add(this.txt_Ana_inner_long_L2_moment);
            this.groupBox10.Controls.Add(this.txt_inner_long_L2_shear);
            this.groupBox10.Controls.Add(this.txt_Ana_inner_long_L4_moment);
            this.groupBox10.Controls.Add(this.txt_Ana_inner_long_deff_shear);
            this.groupBox10.Controls.Add(this.txt_inner_long_deff_moment);
            this.groupBox10.Location = new System.Drawing.Point(6, 63);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(290, 111);
            this.groupBox10.TabIndex = 81;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Inner Main Girder";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(257, 80);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(27, 13);
            this.label166.TabIndex = 30;
            this.label166.Text = "Ton";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(257, 55);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(27, 13);
            this.label167.TabIndex = 30;
            this.label167.Text = "Ton";
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(257, 29);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(27, 13);
            this.label198.TabIndex = 30;
            this.label198.Text = "Ton";
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(171, 11);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(41, 13);
            this.label199.TabIndex = 28;
            this.label199.Text = "Shear";
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(2, 81);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(31, 13);
            this.label200.TabIndex = 18;
            this.label200.Text = "Deff";
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Location = new System.Drawing.Point(2, 30);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(25, 13);
            this.label201.TabIndex = 24;
            this.label201.Text = "L/2";
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Location = new System.Drawing.Point(129, 80);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(29, 13);
            this.label202.TabIndex = 30;
            this.label202.Text = "T-m";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Location = new System.Drawing.Point(129, 55);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(29, 13);
            this.label203.TabIndex = 30;
            this.label203.Text = "T-m";
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Location = new System.Drawing.Point(129, 30);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(29, 13);
            this.label204.TabIndex = 30;
            this.label204.Text = "T-m";
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Location = new System.Drawing.Point(43, 11);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(52, 13);
            this.label205.TabIndex = 27;
            this.label205.Text = "Moment";
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Location = new System.Drawing.Point(2, 55);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(25, 13);
            this.label206.TabIndex = 21;
            this.label206.Text = "L/4";
            // 
            // txt_Ana_inner_long_L4_shear
            // 
            this.txt_Ana_inner_long_L4_shear.Location = new System.Drawing.Point(174, 52);
            this.txt_Ana_inner_long_L4_shear.Name = "txt_Ana_inner_long_L4_shear";
            this.txt_Ana_inner_long_L4_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_inner_long_L4_shear.TabIndex = 23;
            this.txt_Ana_inner_long_L4_shear.Text = "0";
            this.txt_Ana_inner_long_L4_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_inner_long_L2_moment
            // 
            this.txt_Ana_inner_long_L2_moment.Location = new System.Drawing.Point(46, 27);
            this.txt_Ana_inner_long_L2_moment.Name = "txt_Ana_inner_long_L2_moment";
            this.txt_Ana_inner_long_L2_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_inner_long_L2_moment.TabIndex = 25;
            this.txt_Ana_inner_long_L2_moment.Text = "0";
            this.txt_Ana_inner_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_inner_long_L2_shear
            // 
            this.txt_inner_long_L2_shear.Location = new System.Drawing.Point(174, 27);
            this.txt_inner_long_L2_shear.Name = "txt_inner_long_L2_shear";
            this.txt_inner_long_L2_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_inner_long_L2_shear.TabIndex = 26;
            this.txt_inner_long_L2_shear.Text = "0";
            this.txt_inner_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_inner_long_L4_moment
            // 
            this.txt_Ana_inner_long_L4_moment.Location = new System.Drawing.Point(46, 52);
            this.txt_Ana_inner_long_L4_moment.Name = "txt_Ana_inner_long_L4_moment";
            this.txt_Ana_inner_long_L4_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_inner_long_L4_moment.TabIndex = 22;
            this.txt_Ana_inner_long_L4_moment.Text = "0";
            this.txt_Ana_inner_long_L4_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_inner_long_deff_shear
            // 
            this.txt_Ana_inner_long_deff_shear.Location = new System.Drawing.Point(174, 77);
            this.txt_Ana_inner_long_deff_shear.Name = "txt_Ana_inner_long_deff_shear";
            this.txt_Ana_inner_long_deff_shear.Size = new System.Drawing.Size(77, 21);
            this.txt_Ana_inner_long_deff_shear.TabIndex = 20;
            this.txt_Ana_inner_long_deff_shear.Text = "0";
            this.txt_Ana_inner_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_inner_long_deff_moment
            // 
            this.txt_inner_long_deff_moment.Location = new System.Drawing.Point(46, 77);
            this.txt_inner_long_deff_moment.Name = "txt_inner_long_deff_moment";
            this.txt_inner_long_deff_moment.Size = new System.Drawing.Size(77, 21);
            this.txt_inner_long_deff_moment.TabIndex = 19;
            this.txt_inner_long_deff_moment.Text = "0";
            this.txt_inner_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.groupBox43);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(928, 474);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Reaction Forces";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // groupBox43
            // 
            this.groupBox43.Controls.Add(this.groupBox49);
            this.groupBox43.Controls.Add(this.groupBox45);
            this.groupBox43.Controls.Add(this.g);
            this.groupBox43.Controls.Add(this.groupBox48);
            this.groupBox43.Controls.Add(this.groupBox44);
            this.groupBox43.Controls.Add(this.groupBox46);
            this.groupBox43.Controls.Add(this.groupBox47);
            this.groupBox43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox43.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox43.Location = new System.Drawing.Point(3, 3);
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.Size = new System.Drawing.Size(922, 468);
            this.groupBox43.TabIndex = 28;
            this.groupBox43.TabStop = false;
            // 
            // groupBox49
            // 
            this.groupBox49.Controls.Add(this.textBox2);
            this.groupBox49.Controls.Add(this.txt_final_Mz);
            this.groupBox49.Controls.Add(this.label220);
            this.groupBox49.Controls.Add(this.txt_max_Mz_kN);
            this.groupBox49.Controls.Add(this.label261);
            this.groupBox49.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox49.ForeColor = System.Drawing.Color.Red;
            this.groupBox49.Location = new System.Drawing.Point(6, 194);
            this.groupBox49.Name = "groupBox49";
            this.groupBox49.Size = new System.Drawing.Size(333, 48);
            this.groupBox49.TabIndex = 28;
            this.groupBox49.TabStop = false;
            this.groupBox49.Text = "Maximum Mz";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(364, 176);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 20);
            this.textBox2.TabIndex = 6;
            // 
            // txt_final_Mz
            // 
            this.txt_final_Mz.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_Mz.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_Mz.Location = new System.Drawing.Point(19, 19);
            this.txt_final_Mz.Name = "txt_final_Mz";
            this.txt_final_Mz.Size = new System.Drawing.Size(85, 20);
            this.txt_final_Mz.TabIndex = 16;
            this.txt_final_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label220.Location = new System.Drawing.Point(110, 22);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(47, 13);
            this.label220.TabIndex = 20;
            this.label220.Text = "Ton-m";
            // 
            // txt_max_Mz_kN
            // 
            this.txt_max_Mz_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_max_Mz_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_max_Mz_kN.Location = new System.Drawing.Point(172, 19);
            this.txt_max_Mz_kN.Name = "txt_max_Mz_kN";
            this.txt_max_Mz_kN.Size = new System.Drawing.Size(85, 20);
            this.txt_max_Mz_kN.TabIndex = 24;
            this.txt_max_Mz_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label261
            // 
            this.label261.AutoSize = true;
            this.label261.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label261.Location = new System.Drawing.Point(263, 22);
            this.label261.Name = "label261";
            this.label261.Size = new System.Drawing.Size(39, 13);
            this.label261.TabIndex = 18;
            this.label261.Text = "kN-m";
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.txt_left_total_Mz);
            this.groupBox45.Controls.Add(this.txt_left_total_Mx);
            this.groupBox45.Controls.Add(this.label325);
            this.groupBox45.Controls.Add(this.label326);
            this.groupBox45.Controls.Add(this.txt_left_total_vert_reac);
            this.groupBox45.Controls.Add(this.dgv_left_des_frc);
            this.groupBox45.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox45.Location = new System.Drawing.Point(0, 296);
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.Size = new System.Drawing.Size(472, 176);
            this.groupBox45.TabIndex = 24;
            this.groupBox45.TabStop = false;
            this.groupBox45.Text = "Left End Design Forces";
            // 
            // txt_left_total_Mz
            // 
            this.txt_left_total_Mz.Location = new System.Drawing.Point(364, 146);
            this.txt_left_total_Mz.Name = "txt_left_total_Mz";
            this.txt_left_total_Mz.ReadOnly = true;
            this.txt_left_total_Mz.Size = new System.Drawing.Size(76, 20);
            this.txt_left_total_Mz.TabIndex = 6;
            this.txt_left_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_left_total_Mx
            // 
            this.txt_left_total_Mx.Location = new System.Drawing.Point(260, 146);
            this.txt_left_total_Mx.Name = "txt_left_total_Mx";
            this.txt_left_total_Mx.ReadOnly = true;
            this.txt_left_total_Mx.Size = new System.Drawing.Size(76, 20);
            this.txt_left_total_Mx.TabIndex = 5;
            this.txt_left_total_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label325
            // 
            this.label325.AutoSize = true;
            this.label325.Location = new System.Drawing.Point(94, 149);
            this.label325.Name = "label325";
            this.label325.Size = new System.Drawing.Size(47, 13);
            this.label325.TabIndex = 4;
            this.label325.Text = "Total";
            // 
            // label326
            // 
            this.label326.AutoSize = true;
            this.label326.Location = new System.Drawing.Point(6, 149);
            this.label326.Name = "label326";
            this.label326.Size = new System.Drawing.Size(71, 13);
            this.label326.TabIndex = 3;
            this.label326.Text = "Left End";
            // 
            // txt_left_total_vert_reac
            // 
            this.txt_left_total_vert_reac.Location = new System.Drawing.Point(147, 146);
            this.txt_left_total_vert_reac.Name = "txt_left_total_vert_reac";
            this.txt_left_total_vert_reac.ReadOnly = true;
            this.txt_left_total_vert_reac.Size = new System.Drawing.Size(76, 20);
            this.txt_left_total_vert_reac.TabIndex = 2;
            this.txt_left_total_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgv_left_des_frc
            // 
            this.dgv_left_des_frc.AllowUserToResizeColumns = false;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgv_left_des_frc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left_des_frc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.col_Max_Mx,
            this.col_Max_Mz});
            this.dgv_left_des_frc.Location = new System.Drawing.Point(6, 19);
            this.dgv_left_des_frc.Name = "dgv_left_des_frc";
            this.dgv_left_des_frc.ReadOnly = true;
            this.dgv_left_des_frc.Size = new System.Drawing.Size(460, 121);
            this.dgv_left_des_frc.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Joints";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 63;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn10.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 118;
            // 
            // col_Max_Mx
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mx.DefaultCellStyle = dataGridViewCellStyle29;
            this.col_Max_Mx.HeaderText = "Maximum    Mx   (Ton-m)";
            this.col_Max_Mx.Name = "col_Max_Mx";
            this.col_Max_Mx.ReadOnly = true;
            this.col_Max_Mx.Width = 108;
            // 
            // col_Max_Mz
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mz.DefaultCellStyle = dataGridViewCellStyle30;
            this.col_Max_Mz.HeaderText = "Maximum   Mz  (Ton-m)";
            this.col_Max_Mz.Name = "col_Max_Mz";
            this.col_Max_Mz.ReadOnly = true;
            this.col_Max_Mz.Width = 108;
            // 
            // g
            // 
            this.g.Controls.Add(this.dgv_right_des_frc);
            this.g.Controls.Add(this.txt_right_total_Mz);
            this.g.Controls.Add(this.txt_right_total_Mx);
            this.g.Controls.Add(this.label402);
            this.g.Controls.Add(this.label442);
            this.g.Controls.Add(this.txt_right_total_vert_reac);
            this.g.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g.Location = new System.Drawing.Point(478, 296);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(460, 176);
            this.g.TabIndex = 25;
            this.g.TabStop = false;
            this.g.Text = "Right End Design Forces";
            // 
            // dgv_right_des_frc
            // 
            this.dgv_right_des_frc.AllowUserToResizeColumns = false;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dgv_right_des_frc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_des_frc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgv_right_des_frc.Location = new System.Drawing.Point(6, 19);
            this.dgv_right_des_frc.Name = "dgv_right_des_frc";
            this.dgv_right_des_frc.ReadOnly = true;
            this.dgv_right_des_frc.Size = new System.Drawing.Size(444, 121);
            this.dgv_right_des_frc.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Joints";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 63;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn6.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 118;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle33;
            this.dataGridViewTextBoxColumn7.HeaderText = "Maximum    Mx   (Ton-m)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 108;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridViewTextBoxColumn8.HeaderText = "Maximum   Mz  (Ton-m)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 108;
            // 
            // txt_right_total_Mz
            // 
            this.txt_right_total_Mz.Location = new System.Drawing.Point(363, 146);
            this.txt_right_total_Mz.Name = "txt_right_total_Mz";
            this.txt_right_total_Mz.ReadOnly = true;
            this.txt_right_total_Mz.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_Mz.TabIndex = 6;
            this.txt_right_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_right_total_Mx
            // 
            this.txt_right_total_Mx.Location = new System.Drawing.Point(259, 146);
            this.txt_right_total_Mx.Name = "txt_right_total_Mx";
            this.txt_right_total_Mx.ReadOnly = true;
            this.txt_right_total_Mx.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_Mx.TabIndex = 5;
            this.txt_right_total_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label402
            // 
            this.label402.AutoSize = true;
            this.label402.Location = new System.Drawing.Point(93, 149);
            this.label402.Name = "label402";
            this.label402.Size = new System.Drawing.Size(47, 13);
            this.label402.TabIndex = 4;
            this.label402.Text = "Total";
            // 
            // label442
            // 
            this.label442.AutoSize = true;
            this.label442.Location = new System.Drawing.Point(5, 149);
            this.label442.Name = "label442";
            this.label442.Size = new System.Drawing.Size(79, 13);
            this.label442.TabIndex = 3;
            this.label442.Text = "Right End";
            // 
            // txt_right_total_vert_reac
            // 
            this.txt_right_total_vert_reac.Location = new System.Drawing.Point(146, 146);
            this.txt_right_total_vert_reac.Name = "txt_right_total_vert_reac";
            this.txt_right_total_vert_reac.ReadOnly = true;
            this.txt_right_total_vert_reac.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_vert_reac.TabIndex = 2;
            this.txt_right_total_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox48
            // 
            this.groupBox48.Controls.Add(this.textBox3);
            this.groupBox48.Controls.Add(this.txt_final_Mx);
            this.groupBox48.Controls.Add(this.label237);
            this.groupBox48.Controls.Add(this.txt_max_Mx_kN);
            this.groupBox48.Controls.Add(this.label262);
            this.groupBox48.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox48.ForeColor = System.Drawing.Color.Red;
            this.groupBox48.Location = new System.Drawing.Point(6, 111);
            this.groupBox48.Name = "groupBox48";
            this.groupBox48.Size = new System.Drawing.Size(333, 49);
            this.groupBox48.TabIndex = 27;
            this.groupBox48.TabStop = false;
            this.groupBox48.Text = "Maximum Mx";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(364, 176);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(76, 20);
            this.textBox3.TabIndex = 6;
            // 
            // txt_final_Mx
            // 
            this.txt_final_Mx.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_Mx.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_Mx.Location = new System.Drawing.Point(22, 19);
            this.txt_final_Mx.Name = "txt_final_Mx";
            this.txt_final_Mx.Size = new System.Drawing.Size(85, 20);
            this.txt_final_Mx.TabIndex = 12;
            this.txt_final_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label237
            // 
            this.label237.AutoSize = true;
            this.label237.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label237.Location = new System.Drawing.Point(113, 22);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(47, 13);
            this.label237.TabIndex = 21;
            this.label237.Text = "Ton-m";
            // 
            // txt_max_Mx_kN
            // 
            this.txt_max_Mx_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_max_Mx_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_max_Mx_kN.Location = new System.Drawing.Point(175, 19);
            this.txt_max_Mx_kN.Name = "txt_max_Mx_kN";
            this.txt_max_Mx_kN.Size = new System.Drawing.Size(85, 20);
            this.txt_max_Mx_kN.TabIndex = 23;
            this.txt_max_Mx_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label262
            // 
            this.label262.AutoSize = true;
            this.label262.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label262.Location = new System.Drawing.Point(266, 22);
            this.label262.Name = "label262";
            this.label262.Size = new System.Drawing.Size(39, 13);
            this.label262.TabIndex = 18;
            this.label262.Text = "kN-m";
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.txt_final_vert_rec_kN);
            this.groupBox44.Controls.Add(this.label260);
            this.groupBox44.Controls.Add(this.textBox1);
            this.groupBox44.Controls.Add(this.txt_final_vert_reac);
            this.groupBox44.Controls.Add(this.label264);
            this.groupBox44.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox44.ForeColor = System.Drawing.Color.Red;
            this.groupBox44.Location = new System.Drawing.Point(6, 21);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(333, 64);
            this.groupBox44.TabIndex = 26;
            this.groupBox44.TabStop = false;
            this.groupBox44.Text = "Both Ends Total Vertical Reaction";
            // 
            // txt_final_vert_rec_kN
            // 
            this.txt_final_vert_rec_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_vert_rec_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_vert_rec_kN.Location = new System.Drawing.Point(175, 29);
            this.txt_final_vert_rec_kN.Name = "txt_final_vert_rec_kN";
            this.txt_final_vert_rec_kN.Size = new System.Drawing.Size(85, 20);
            this.txt_final_vert_rec_kN.TabIndex = 22;
            this.txt_final_vert_rec_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label260
            // 
            this.label260.AutoSize = true;
            this.label260.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label260.Location = new System.Drawing.Point(113, 32);
            this.label260.Name = "label260";
            this.label260.Size = new System.Drawing.Size(31, 13);
            this.label260.TabIndex = 19;
            this.label260.Text = "Ton";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(364, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 6;
            // 
            // txt_final_vert_reac
            // 
            this.txt_final_vert_reac.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_vert_reac.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_vert_reac.Location = new System.Drawing.Point(22, 29);
            this.txt_final_vert_reac.Name = "txt_final_vert_reac";
            this.txt_final_vert_reac.Size = new System.Drawing.Size(85, 20);
            this.txt_final_vert_reac.TabIndex = 11;
            this.txt_final_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label264
            // 
            this.label264.AutoSize = true;
            this.label264.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label264.Location = new System.Drawing.Point(266, 32);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(23, 13);
            this.label264.TabIndex = 14;
            this.label264.Text = "kN";
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label327);
            this.groupBox46.Controls.Add(this.txt_dead_kN_m);
            this.groupBox46.Controls.Add(this.label354);
            this.groupBox46.Controls.Add(this.txt_dead_vert_reac_kN);
            this.groupBox46.Controls.Add(this.label370);
            this.groupBox46.Controls.Add(this.label371);
            this.groupBox46.Controls.Add(this.dgv_left_end_design_forces);
            this.groupBox46.Controls.Add(this.txt_dead_vert_reac_ton);
            this.groupBox46.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox46.Location = new System.Drawing.Point(348, 21);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(297, 263);
            this.groupBox46.TabIndex = 22;
            this.groupBox46.TabStop = false;
            this.groupBox46.Text = "Support Reactions from Dead Load";
            // 
            // label327
            // 
            this.label327.AutoSize = true;
            this.label327.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label327.Location = new System.Drawing.Point(244, 237);
            this.label327.Name = "label327";
            this.label327.Size = new System.Drawing.Size(39, 13);
            this.label327.TabIndex = 28;
            this.label327.Text = "kN/m";
            // 
            // txt_dead_kN_m
            // 
            this.txt_dead_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_kN_m.Location = new System.Drawing.Point(125, 234);
            this.txt_dead_kN_m.Name = "txt_dead_kN_m";
            this.txt_dead_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_kN_m.TabIndex = 27;
            this.txt_dead_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_dead_kN_m.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label354
            // 
            this.label354.AutoSize = true;
            this.label354.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label354.Location = new System.Drawing.Point(244, 208);
            this.label354.Name = "label354";
            this.label354.Size = new System.Drawing.Size(23, 13);
            this.label354.TabIndex = 26;
            this.label354.Text = "kN";
            // 
            // txt_dead_vert_reac_kN
            // 
            this.txt_dead_vert_reac_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_kN.Location = new System.Drawing.Point(125, 205);
            this.txt_dead_vert_reac_kN.Name = "txt_dead_vert_reac_kN";
            this.txt_dead_vert_reac_kN.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_kN.TabIndex = 25;
            this.txt_dead_vert_reac_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_dead_vert_reac_kN.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label370
            // 
            this.label370.AutoSize = true;
            this.label370.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label370.Location = new System.Drawing.Point(45, 182);
            this.label370.Name = "label370";
            this.label370.Size = new System.Drawing.Size(55, 13);
            this.label370.TabIndex = 24;
            this.label370.Text = "Total ";
            // 
            // label371
            // 
            this.label371.AutoSize = true;
            this.label371.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label371.Location = new System.Drawing.Point(244, 182);
            this.label371.Name = "label371";
            this.label371.Size = new System.Drawing.Size(31, 13);
            this.label371.TabIndex = 23;
            this.label371.Text = "Ton";
            // 
            // dgv_left_end_design_forces
            // 
            this.dgv_left_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dgv_left_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Joints,
            this.col_Vert_Reaction});
            this.dgv_left_end_design_forces.Location = new System.Drawing.Point(18, 19);
            this.dgv_left_end_design_forces.Name = "dgv_left_end_design_forces";
            this.dgv_left_end_design_forces.ReadOnly = true;
            this.dgv_left_end_design_forces.Size = new System.Drawing.Size(258, 154);
            this.dgv_left_end_design_forces.TabIndex = 1;
            // 
            // col_Joints
            // 
            this.col_Joints.HeaderText = "Joints";
            this.col_Joints.Name = "col_Joints";
            this.col_Joints.ReadOnly = true;
            this.col_Joints.Width = 63;
            // 
            // col_Vert_Reaction
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Vert_Reaction.DefaultCellStyle = dataGridViewCellStyle36;
            this.col_Vert_Reaction.HeaderText = "Vertical Reaction (Ton)";
            this.col_Vert_Reaction.Name = "col_Vert_Reaction";
            this.col_Vert_Reaction.ReadOnly = true;
            this.col_Vert_Reaction.Width = 118;
            // 
            // txt_dead_vert_reac_ton
            // 
            this.txt_dead_vert_reac_ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_ton.Location = new System.Drawing.Point(125, 179);
            this.txt_dead_vert_reac_ton.Name = "txt_dead_vert_reac_ton";
            this.txt_dead_vert_reac_ton.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_ton.TabIndex = 11;
            this.txt_dead_vert_reac_ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_dead_vert_reac_ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // groupBox47
            // 
            this.groupBox47.Controls.Add(this.txt_live_kN_m);
            this.groupBox47.Controls.Add(this.label388);
            this.groupBox47.Controls.Add(this.txt_live_vert_rec_kN);
            this.groupBox47.Controls.Add(this.label399);
            this.groupBox47.Controls.Add(this.label400);
            this.groupBox47.Controls.Add(this.dgv_right_end_design_forces);
            this.groupBox47.Controls.Add(this.txt_live_vert_rec_Ton);
            this.groupBox47.Controls.Add(this.label401);
            this.groupBox47.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox47.Location = new System.Drawing.Point(651, 21);
            this.groupBox47.Name = "groupBox47";
            this.groupBox47.Size = new System.Drawing.Size(288, 263);
            this.groupBox47.TabIndex = 23;
            this.groupBox47.TabStop = false;
            this.groupBox47.Text = "Support Reactions from Live Load";
            // 
            // txt_live_kN_m
            // 
            this.txt_live_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_kN_m.Location = new System.Drawing.Point(111, 234);
            this.txt_live_kN_m.Name = "txt_live_kN_m";
            this.txt_live_kN_m.Size = new System.Drawing.Size(117, 20);
            this.txt_live_kN_m.TabIndex = 28;
            this.txt_live_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_kN_m.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label388
            // 
            this.label388.AutoSize = true;
            this.label388.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label388.Location = new System.Drawing.Point(234, 237);
            this.label388.Name = "label388";
            this.label388.Size = new System.Drawing.Size(39, 13);
            this.label388.TabIndex = 27;
            this.label388.Text = "kN/m";
            // 
            // txt_live_vert_rec_kN
            // 
            this.txt_live_vert_rec_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_kN.Location = new System.Drawing.Point(111, 208);
            this.txt_live_vert_rec_kN.Name = "txt_live_vert_rec_kN";
            this.txt_live_vert_rec_kN.Size = new System.Drawing.Size(117, 20);
            this.txt_live_vert_rec_kN.TabIndex = 26;
            this.txt_live_vert_rec_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_vert_rec_kN.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label399
            // 
            this.label399.AutoSize = true;
            this.label399.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label399.Location = new System.Drawing.Point(234, 211);
            this.label399.Name = "label399";
            this.label399.Size = new System.Drawing.Size(23, 13);
            this.label399.TabIndex = 25;
            this.label399.Text = "kN";
            // 
            // label400
            // 
            this.label400.AutoSize = true;
            this.label400.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label400.Location = new System.Drawing.Point(50, 182);
            this.label400.Name = "label400";
            this.label400.Size = new System.Drawing.Size(55, 13);
            this.label400.TabIndex = 24;
            this.label400.Text = "Total ";
            // 
            // dgv_right_end_design_forces
            // 
            this.dgv_right_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dgv_right_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgv_right_end_design_forces.Location = new System.Drawing.Point(17, 19);
            this.dgv_right_end_design_forces.Name = "dgv_right_end_design_forces";
            this.dgv_right_end_design_forces.ReadOnly = true;
            this.dgv_right_end_design_forces.Size = new System.Drawing.Size(255, 154);
            this.dgv_right_end_design_forces.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Joints";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 63;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataGridViewTextBoxColumn4.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 118;
            // 
            // txt_live_vert_rec_Ton
            // 
            this.txt_live_vert_rec_Ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_Ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_Ton.Location = new System.Drawing.Point(111, 179);
            this.txt_live_vert_rec_Ton.Name = "txt_live_vert_rec_Ton";
            this.txt_live_vert_rec_Ton.Size = new System.Drawing.Size(117, 20);
            this.txt_live_vert_rec_Ton.TabIndex = 22;
            this.txt_live_vert_rec_Ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_vert_rec_Ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label401
            // 
            this.label401.AutoSize = true;
            this.label401.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label401.Location = new System.Drawing.Point(234, 182);
            this.label401.Name = "label401";
            this.label401.Size = new System.Drawing.Size(31, 13);
            this.label401.TabIndex = 14;
            this.label401.Text = "Ton";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(954, 674);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RCC Pier Interactive Design";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tab_des_form1);
            this.tabControl2.Controls.Add(this.tab_des_form2);
            this.tabControl2.Controls.Add(this.tab_des_Diagram);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(948, 622);
            this.tabControl2.TabIndex = 3;
            // 
            // tab_des_form1
            // 
            this.tab_des_form1.Controls.Add(this.label214);
            this.tab_des_form1.Controls.Add(this.label216);
            this.tab_des_form1.Controls.Add(this.groupBox42);
            this.tab_des_form1.Controls.Add(this.label118);
            this.tab_des_form1.Controls.Add(this.groupBox3);
            this.tab_des_form1.Controls.Add(this.label117);
            this.tab_des_form1.Controls.Add(this.label115);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H7);
            this.tab_des_form1.Controls.Add(this.label116);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_gama_c);
            this.tab_des_form1.Controls.Add(this.label112);
            this.tab_des_form1.Controls.Add(this.label104);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_vehi_load);
            this.tab_des_form1.Controls.Add(this.label106);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NR);
            this.tab_des_form1.Controls.Add(this.label107);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NP);
            this.tab_des_form1.Controls.Add(this.label105);
            this.tab_des_form1.Controls.Add(this.groupBox4);
            this.tab_des_form1.Controls.Add(this.label100);
            this.tab_des_form1.Controls.Add(this.label99);
            this.tab_des_form1.Controls.Add(this.label98);
            this.tab_des_form1.Controls.Add(this.label97);
            this.tab_des_form1.Controls.Add(this.label96);
            this.tab_des_form1.Controls.Add(this.label95);
            this.tab_des_form1.Controls.Add(this.label82);
            this.tab_des_form1.Controls.Add(this.label81);
            this.tab_des_form1.Controls.Add(this.label80);
            this.tab_des_form1.Controls.Add(this.label79);
            this.tab_des_form1.Controls.Add(this.label69);
            this.tab_des_form1.Controls.Add(this.label74);
            this.tab_des_form1.Controls.Add(this.label73);
            this.tab_des_form1.Controls.Add(this.label72);
            this.tab_des_form1.Controls.Add(this.label71);
            this.tab_des_form1.Controls.Add(this.label70);
            this.tab_des_form1.Controls.Add(this.label92);
            this.tab_des_form1.Controls.Add(this.label93);
            this.tab_des_form1.Controls.Add(this.label94);
            this.tab_des_form1.Controls.Add(this.label108);
            this.tab_des_form1.Controls.Add(this.label109);
            this.tab_des_form1.Controls.Add(this.label110);
            this.tab_des_form1.Controls.Add(this.label111);
            this.tab_des_form1.Controls.Add(this.label113);
            this.tab_des_form1.Controls.Add(this.label114);
            this.tab_des_form1.Controls.Add(this.label119);
            this.tab_des_form1.Controls.Add(this.label120);
            this.tab_des_form1.Controls.Add(this.label121);
            this.tab_des_form1.Controls.Add(this.groupBox5);
            this.tab_des_form1.Controls.Add(this.groupBox1);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_overall_height);
            this.tab_des_form1.Controls.Add(this.label29);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B14);
            this.tab_des_form1.Controls.Add(this.label30);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B13);
            this.tab_des_form1.Controls.Add(this.label31);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B12);
            this.tab_des_form1.Controls.Add(this.label13);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B11);
            this.tab_des_form1.Controls.Add(this.label14);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B10);
            this.tab_des_form1.Controls.Add(this.label34);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B9);
            this.tab_des_form1.Controls.Add(this.label35);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H6);
            this.tab_des_form1.Controls.Add(this.label36);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H5);
            this.tab_des_form1.Controls.Add(this.label37);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B8);
            this.tab_des_form1.Controls.Add(this.label16);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H4);
            this.tab_des_form1.Controls.Add(this.label17);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H3);
            this.tab_des_form1.Controls.Add(this.label21);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B7);
            this.tab_des_form1.Controls.Add(this.label22);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_Form_Lev);
            this.tab_des_form1.Controls.Add(this.label23);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL5);
            this.tab_des_form1.Controls.Add(this.label24);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL4);
            this.tab_des_form1.Controls.Add(this.label25);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL3);
            this.tab_des_form1.Controls.Add(this.label26);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL2);
            this.tab_des_form1.Controls.Add(this.label44);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL1);
            this.tab_des_form1.Controls.Add(this.label45);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B6);
            this.tab_des_form1.Controls.Add(this.label46);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B5);
            this.tab_des_form1.Controls.Add(this.label47);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_d2);
            this.tab_des_form1.Controls.Add(this.label48);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_d1);
            this.tab_des_form1.Controls.Add(this.label49);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NB);
            this.tab_des_form1.Controls.Add(this.label50);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_a1);
            this.tab_des_form1.Controls.Add(this.label51);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_w3);
            this.tab_des_form1.Controls.Add(this.label52);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_w2);
            this.tab_des_form1.Controls.Add(this.label53);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_w1);
            this.tab_des_form1.Controls.Add(this.label54);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_L1);
            this.tab_des_form1.Controls.Add(this.label55);
            this.tab_des_form1.Location = new System.Drawing.Point(4, 22);
            this.tab_des_form1.Name = "tab_des_form1";
            this.tab_des_form1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_form1.Size = new System.Drawing.Size(940, 596);
            this.tab_des_form1.TabIndex = 0;
            this.tab_des_form1.Text = "Design Input Data [Form1]";
            this.tab_des_form1.UseVisualStyleBackColor = true;
            // 
            // label214
            // 
            this.label214.AutoSize = true;
            this.label214.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label214.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label214.ForeColor = System.Drawing.Color.Red;
            this.label214.Location = new System.Drawing.Point(526, 4);
            this.label214.Name = "label214";
            this.label214.Size = new System.Drawing.Size(218, 18);
            this.label214.TabIndex = 173;
            this.label214.Text = "Default Sample Data are shown";
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label216.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label216.ForeColor = System.Drawing.Color.Green;
            this.label216.Location = new System.Drawing.Point(385, 4);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(135, 18);
            this.label216.TabIndex = 172;
            this.label216.Text = "All User Input Data";
            // 
            // groupBox42
            // 
            this.groupBox42.Controls.Add(this.txt_rcc_pier_m);
            this.groupBox42.Controls.Add(this.label215);
            this.groupBox42.Controls.Add(this.cmb_rcc_pier_fy);
            this.groupBox42.Controls.Add(this.label236);
            this.groupBox42.Controls.Add(this.cmb_rcc_pier_fck);
            this.groupBox42.Controls.Add(this.label244);
            this.groupBox42.Controls.Add(this.label250);
            this.groupBox42.Controls.Add(this.label251);
            this.groupBox42.Controls.Add(this.txt_rcc_pier_sigma_st);
            this.groupBox42.Controls.Add(this.label253);
            this.groupBox42.Controls.Add(this.label254);
            this.groupBox42.Controls.Add(this.txt_rcc_pier_sigma_c);
            this.groupBox42.Controls.Add(this.label258);
            this.groupBox42.Controls.Add(this.label259);
            this.groupBox42.Location = new System.Drawing.Point(561, 25);
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.Size = new System.Drawing.Size(364, 144);
            this.groupBox42.TabIndex = 116;
            this.groupBox42.TabStop = false;
            // 
            // txt_rcc_pier_m
            // 
            this.txt_rcc_pier_m.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rcc_pier_m.Location = new System.Drawing.Point(222, 117);
            this.txt_rcc_pier_m.Name = "txt_rcc_pier_m";
            this.txt_rcc_pier_m.Size = new System.Drawing.Size(65, 22);
            this.txt_rcc_pier_m.TabIndex = 88;
            this.txt_rcc_pier_m.Text = "10";
            this.txt_rcc_pier_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label215
            // 
            this.label215.AutoSize = true;
            this.label215.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label215.Location = new System.Drawing.Point(293, 43);
            this.label215.Name = "label215";
            this.label215.Size = new System.Drawing.Size(67, 13);
            this.label215.TabIndex = 81;
            this.label215.Text = "N/sq.mm";
            // 
            // cmb_rcc_pier_fy
            // 
            this.cmb_rcc_pier_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_rcc_pier_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_rcc_pier_fy.FormattingEnabled = true;
            this.cmb_rcc_pier_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_rcc_pier_fy.Location = new System.Drawing.Point(226, 67);
            this.cmb_rcc_pier_fy.Name = "cmb_rcc_pier_fy";
            this.cmb_rcc_pier_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_rcc_pier_fy.TabIndex = 15;
            this.cmb_rcc_pier_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label236
            // 
            this.label236.AutoSize = true;
            this.label236.Location = new System.Drawing.Point(5, 42);
            this.label236.Name = "label236";
            this.label236.Size = new System.Drawing.Size(211, 13);
            this.label236.TabIndex = 79;
            this.label236.Text = "Allowable Flexural Stress in Concrete [σ_c] ";
            // 
            // cmb_rcc_pier_fck
            // 
            this.cmb_rcc_pier_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_rcc_pier_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_rcc_pier_fck.FormattingEnabled = true;
            this.cmb_rcc_pier_fck.Items.AddRange(new object[] {
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.cmb_rcc_pier_fck.Location = new System.Drawing.Point(226, 12);
            this.cmb_rcc_pier_fck.Name = "cmb_rcc_pier_fck";
            this.cmb_rcc_pier_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_rcc_pier_fck.TabIndex = 13;
            this.cmb_rcc_pier_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label244
            // 
            this.label244.AutoSize = true;
            this.label244.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label244.Location = new System.Drawing.Point(202, 69);
            this.label244.Name = "label244";
            this.label244.Size = new System.Drawing.Size(23, 14);
            this.label244.TabIndex = 59;
            this.label244.Text = "Fe";
            // 
            // label250
            // 
            this.label250.AutoSize = true;
            this.label250.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label250.Location = new System.Drawing.Point(207, 14);
            this.label250.Name = "label250";
            this.label250.Size = new System.Drawing.Size(18, 14);
            this.label250.TabIndex = 58;
            this.label250.Text = "M";
            // 
            // label251
            // 
            this.label251.AutoSize = true;
            this.label251.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label251.Location = new System.Drawing.Point(293, 100);
            this.label251.Name = "label251";
            this.label251.Size = new System.Drawing.Size(67, 13);
            this.label251.TabIndex = 57;
            this.label251.Text = "N/sq.mm";
            // 
            // txt_rcc_pier_sigma_st
            // 
            this.txt_rcc_pier_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_rcc_pier_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rcc_pier_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_rcc_pier_sigma_st.Location = new System.Drawing.Point(222, 94);
            this.txt_rcc_pier_sigma_st.Name = "txt_rcc_pier_sigma_st";
            this.txt_rcc_pier_sigma_st.ReadOnly = true;
            this.txt_rcc_pier_sigma_st.Size = new System.Drawing.Size(65, 22);
            this.txt_rcc_pier_sigma_st.TabIndex = 16;
            this.txt_rcc_pier_sigma_st.Text = "200";
            this.txt_rcc_pier_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label253
            // 
            this.label253.AutoSize = true;
            this.label253.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label253.Location = new System.Drawing.Point(5, 99);
            this.label253.Name = "label253";
            this.label253.Size = new System.Drawing.Size(159, 13);
            this.label253.TabIndex = 55;
            this.label253.Text = "Permissible Stress in Steel [σ_st]";
            // 
            // label254
            // 
            this.label254.AutoSize = true;
            this.label254.Location = new System.Drawing.Point(5, 74);
            this.label254.Name = "label254";
            this.label254.Size = new System.Drawing.Size(80, 13);
            this.label254.TabIndex = 15;
            this.label254.Text = "Steel Grade [fy]";
            // 
            // txt_rcc_pier_sigma_c
            // 
            this.txt_rcc_pier_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_rcc_pier_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rcc_pier_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_rcc_pier_sigma_c.Location = new System.Drawing.Point(222, 39);
            this.txt_rcc_pier_sigma_c.Name = "txt_rcc_pier_sigma_c";
            this.txt_rcc_pier_sigma_c.ReadOnly = true;
            this.txt_rcc_pier_sigma_c.Size = new System.Drawing.Size(65, 22);
            this.txt_rcc_pier_sigma_c.TabIndex = 14;
            this.txt_rcc_pier_sigma_c.Text = "83.3";
            this.txt_rcc_pier_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label258
            // 
            this.label258.AutoSize = true;
            this.label258.Location = new System.Drawing.Point(5, 19);
            this.label258.Name = "label258";
            this.label258.Size = new System.Drawing.Size(106, 13);
            this.label258.TabIndex = 13;
            this.label258.Text = "Concrete Grade [fck]";
            // 
            // label259
            // 
            this.label259.AutoSize = true;
            this.label259.Location = new System.Drawing.Point(5, 121);
            this.label259.Name = "label259";
            this.label259.Size = new System.Drawing.Size(90, 13);
            this.label259.TabIndex = 41;
            this.label259.Text = "Modular Ratio [m]";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(233, 242);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(51, 14);
            this.label118.TabIndex = 107;
            this.label118.Text = "kN/cu.m";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label122);
            this.groupBox3.Controls.Add(this.txt_RCC_Pier_W1_supp_reac);
            this.groupBox3.Controls.Add(this.label123);
            this.groupBox3.Controls.Add(this.label124);
            this.groupBox3.Controls.Add(this.txt_RCC_Pier_Mz1);
            this.groupBox3.Controls.Add(this.label125);
            this.groupBox3.Controls.Add(this.label126);
            this.groupBox3.Controls.Add(this.txt_RCC_Pier_Mx1);
            this.groupBox3.Controls.Add(this.label127);
            this.groupBox3.Location = new System.Drawing.Point(561, 409);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 131);
            this.groupBox3.TabIndex = 90;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "From Design Forces we have";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(292, 22);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(24, 13);
            this.label122.TabIndex = 104;
            this.label122.Text = "kN";
            // 
            // txt_RCC_Pier_W1_supp_reac
            // 
            this.txt_RCC_Pier_W1_supp_reac.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_W1_supp_reac.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_W1_supp_reac.Location = new System.Drawing.Point(212, 19);
            this.txt_RCC_Pier_W1_supp_reac.Name = "txt_RCC_Pier_W1_supp_reac";
            this.txt_RCC_Pier_W1_supp_reac.Size = new System.Drawing.Size(74, 20);
            this.txt_RCC_Pier_W1_supp_reac.TabIndex = 5;
            this.txt_RCC_Pier_W1_supp_reac.Text = " ";
            this.txt_RCC_Pier_W1_supp_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(5, 22);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(201, 13);
            this.label123.TabIndex = 105;
            this.label123.Text = "Total Support Reaction on The Pier [W1]";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label124.Location = new System.Drawing.Point(292, 97);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(42, 13);
            this.label124.TabIndex = 101;
            this.label124.Text = "kN-m";
            // 
            // txt_RCC_Pier_Mz1
            // 
            this.txt_RCC_Pier_Mz1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Mz1.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_Mz1.Location = new System.Drawing.Point(211, 96);
            this.txt_RCC_Pier_Mz1.Name = "txt_RCC_Pier_Mz1";
            this.txt_RCC_Pier_Mz1.Size = new System.Drawing.Size(75, 20);
            this.txt_RCC_Pier_Mz1.TabIndex = 4;
            this.txt_RCC_Pier_Mz1.Text = " ";
            this.txt_RCC_Pier_Mz1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(7, 90);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(134, 26);
            this.label125.TabIndex = 102;
            this.label125.Text = "Moment at Supports in \r\nTransverse Direction [Mz1]";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(290, 63);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(42, 13);
            this.label126.TabIndex = 98;
            this.label126.Text = "kN-m";
            // 
            // txt_RCC_Pier_Mx1
            // 
            this.txt_RCC_Pier_Mx1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Mx1.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_Mx1.Location = new System.Drawing.Point(212, 60);
            this.txt_RCC_Pier_Mx1.Name = "txt_RCC_Pier_Mx1";
            this.txt_RCC_Pier_Mx1.Size = new System.Drawing.Size(75, 20);
            this.txt_RCC_Pier_Mx1.TabIndex = 3;
            this.txt_RCC_Pier_Mx1.Text = " ";
            this.txt_RCC_Pier_Mx1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(7, 54);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(138, 26);
            this.label127.TabIndex = 99;
            this.label127.Text = "Moment at Supports in \r\nLongitudinal Direction [Mx1]";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.Red;
            this.label117.Location = new System.Drawing.Point(13, 9);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(334, 13);
            this.label117.TabIndex = 106;
            this.label117.Text = "Refer to the \'Diagram\' Tab for various Dimensions";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(535, 353);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(19, 13);
            this.label115.TabIndex = 103;
            this.label115.Text = "m";
            // 
            // txt_RCC_Pier_H7
            // 
            this.txt_RCC_Pier_H7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H7.Location = new System.Drawing.Point(476, 350);
            this.txt_RCC_Pier_H7.Name = "txt_RCC_Pier_H7";
            this.txt_RCC_Pier_H7.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H7.TabIndex = 31;
            this.txt_RCC_Pier_H7.Text = "6.560";
            this.txt_RCC_Pier_H7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(282, 353);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(121, 13);
            this.label116.TabIndex = 104;
            this.label116.Text = "Total Height of Pier [H7]";
            // 
            // txt_RCC_Pier_gama_c
            // 
            this.txt_RCC_Pier_gama_c.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_gama_c.Location = new System.Drawing.Point(183, 236);
            this.txt_RCC_Pier_gama_c.Name = "txt_RCC_Pier_gama_c";
            this.txt_RCC_Pier_gama_c.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_gama_c.TabIndex = 8;
            this.txt_RCC_Pier_gama_c.Text = "24";
            this.txt_RCC_Pier_gama_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(17, 239);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(148, 13);
            this.label112.TabIndex = 101;
            this.label112.Text = "Unit Weight of Concrete [γ_c]";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(853, 553);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(24, 13);
            this.label104.TabIndex = 98;
            this.label104.Text = "kN";
            // 
            // txt_RCC_Pier_vehi_load
            // 
            this.txt_RCC_Pier_vehi_load.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_vehi_load.Location = new System.Drawing.Point(796, 550);
            this.txt_RCC_Pier_vehi_load.Name = "txt_RCC_Pier_vehi_load";
            this.txt_RCC_Pier_vehi_load.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_vehi_load.TabIndex = 36;
            this.txt_RCC_Pier_vehi_load.Text = "1000.0";
            this.txt_RCC_Pier_vehi_load.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(569, 553);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(119, 13);
            this.label106.TabIndex = 99;
            this.label106.Text = "Total Vehicle Live Load";
            // 
            // txt_RCC_Pier_NR
            // 
            this.txt_RCC_Pier_NR.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_NR.Location = new System.Drawing.Point(183, 378);
            this.txt_RCC_Pier_NR.Name = "txt_RCC_Pier_NR";
            this.txt_RCC_Pier_NR.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_NR.TabIndex = 11;
            this.txt_RCC_Pier_NR.Text = "2";
            this.txt_RCC_Pier_NR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(17, 381);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(91, 13);
            this.label107.TabIndex = 96;
            this.label107.Text = "Nos. of Row [NR]";
            // 
            // txt_RCC_Pier_NP
            // 
            this.txt_RCC_Pier_NP.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_NP.Location = new System.Drawing.Point(183, 355);
            this.txt_RCC_Pier_NP.Name = "txt_RCC_Pier_NP";
            this.txt_RCC_Pier_NP.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_NP.TabIndex = 10;
            this.txt_RCC_Pier_NP.Text = "4";
            this.txt_RCC_Pier_NP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(16, 358);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(157, 13);
            this.label105.TabIndex = 93;
            this.label105.Text = "Nos. of Pedestals per Row [NP]";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_fck_2);
            this.groupBox4.Controls.Add(this.label56);
            this.groupBox4.Controls.Add(this.label227);
            this.groupBox4.Controls.Add(this.label225);
            this.groupBox4.Controls.Add(this.label87);
            this.groupBox4.Controls.Add(this.label101);
            this.groupBox4.Controls.Add(this.label86);
            this.groupBox4.Controls.Add(this.label57);
            this.groupBox4.Controls.Add(this.label85);
            this.groupBox4.Controls.Add(this.label91);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_fy2);
            this.groupBox4.Controls.Add(this.label90);
            this.groupBox4.Controls.Add(this.label58);
            this.groupBox4.Controls.Add(this.label89);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_D);
            this.groupBox4.Controls.Add(this.label88);
            this.groupBox4.Controls.Add(this.label65);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_b);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_tdia);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_rdia);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_d_dash);
            this.groupBox4.Controls.Add(this.label66);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_p1);
            this.groupBox4.Controls.Add(this.label226);
            this.groupBox4.Controls.Add(this.label67);
            this.groupBox4.Controls.Add(this.label224);
            this.groupBox4.Controls.Add(this.txt_RCC_Pier_p2);
            this.groupBox4.Controls.Add(this.label68);
            this.groupBox4.Location = new System.Drawing.Point(561, 177);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(363, 214);
            this.groupBox4.TabIndex = 90;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Design of Pier Stem";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(210, 218);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 13);
            this.label27.TabIndex = 92;
            this.label27.Text = "M";
            // 
            // txt_RCC_Pier_fck_2
            // 
            this.txt_RCC_Pier_fck_2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_fck_2.Location = new System.Drawing.Point(236, 215);
            this.txt_RCC_Pier_fck_2.Name = "txt_RCC_Pier_fck_2";
            this.txt_RCC_Pier_fck_2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_fck_2.TabIndex = 3;
            this.txt_RCC_Pier_fck_2.Text = "30";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(9, 218);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(104, 13);
            this.label56.TabIndex = 74;
            this.label56.Text = "Concrete grade [fck]";
            // 
            // label227
            // 
            this.label227.AutoSize = true;
            this.label227.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label227.Location = new System.Drawing.Point(292, 184);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(31, 13);
            this.label227.TabIndex = 22;
            this.label227.Text = "mm";
            // 
            // label225
            // 
            this.label225.AutoSize = true;
            this.label225.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label225.Location = new System.Drawing.Point(292, 157);
            this.label225.Name = "label225";
            this.label225.Size = new System.Drawing.Size(31, 13);
            this.label225.TabIndex = 22;
            this.label225.Text = "mm";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(292, 79);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(31, 13);
            this.label87.TabIndex = 22;
            this.label87.Text = "mm";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.Location = new System.Drawing.Point(210, 242);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(23, 13);
            this.label101.TabIndex = 91;
            this.label101.Text = "Fe";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(292, 55);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(21, 13);
            this.label86.TabIndex = 22;
            this.label86.Text = "%";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(10, 240);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(78, 13);
            this.label57.TabIndex = 76;
            this.label57.Text = "Steel grade [fy]";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(292, 28);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(21, 13);
            this.label85.TabIndex = 22;
            this.label85.Text = "%";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(293, 131);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(31, 13);
            this.label91.TabIndex = 22;
            this.label91.Text = "mm";
            // 
            // txt_RCC_Pier_fy2
            // 
            this.txt_RCC_Pier_fy2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_fy2.Location = new System.Drawing.Point(236, 239);
            this.txt_RCC_Pier_fy2.Name = "txt_RCC_Pier_fy2";
            this.txt_RCC_Pier_fy2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_fy2.TabIndex = 4;
            this.txt_RCC_Pier_fy2.Text = "415";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(293, 104);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(31, 13);
            this.label90.TabIndex = 22;
            this.label90.Text = "mm";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(7, 104);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(195, 13);
            this.label58.TabIndex = 78;
            this.label58.Text = "Width of Pier in Transverse direction [D]";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(295, 240);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(68, 13);
            this.label89.TabIndex = 22;
            this.label89.Text = "N/Sq.mm";
            // 
            // txt_RCC_Pier_D
            // 
            this.txt_RCC_Pier_D.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_D.Location = new System.Drawing.Point(234, 101);
            this.txt_RCC_Pier_D.Name = "txt_RCC_Pier_D";
            this.txt_RCC_Pier_D.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_D.TabIndex = 5;
            this.txt_RCC_Pier_D.Text = "6000";
            this.txt_RCC_Pier_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(295, 216);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(55, 13);
            this.label88.TabIndex = 22;
            this.label88.Text = "N/sq.m";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 131);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(197, 13);
            this.label65.TabIndex = 82;
            this.label65.Text = "Width of Pier in Longitudinal direction [b]";
            // 
            // txt_RCC_Pier_b
            // 
            this.txt_RCC_Pier_b.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_b.Location = new System.Drawing.Point(234, 128);
            this.txt_RCC_Pier_b.Name = "txt_RCC_Pier_b";
            this.txt_RCC_Pier_b.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_b.TabIndex = 6;
            this.txt_RCC_Pier_b.Text = "1000";
            this.txt_RCC_Pier_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_RCC_Pier_tdia
            // 
            this.txt_RCC_Pier_tdia.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_tdia.Location = new System.Drawing.Point(234, 181);
            this.txt_RCC_Pier_tdia.Name = "txt_RCC_Pier_tdia";
            this.txt_RCC_Pier_tdia.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_tdia.TabIndex = 2;
            this.txt_RCC_Pier_tdia.Text = "12";
            this.txt_RCC_Pier_tdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_RCC_Pier_rdia
            // 
            this.txt_RCC_Pier_rdia.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_rdia.Location = new System.Drawing.Point(234, 154);
            this.txt_RCC_Pier_rdia.Name = "txt_RCC_Pier_rdia";
            this.txt_RCC_Pier_rdia.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_rdia.TabIndex = 2;
            this.txt_RCC_Pier_rdia.Text = "32";
            this.txt_RCC_Pier_rdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_RCC_Pier_d_dash
            // 
            this.txt_RCC_Pier_d_dash.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_d_dash.Location = new System.Drawing.Point(234, 76);
            this.txt_RCC_Pier_d_dash.Name = "txt_RCC_Pier_d_dash";
            this.txt_RCC_Pier_d_dash.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_d_dash.TabIndex = 2;
            this.txt_RCC_Pier_d_dash.Text = "40";
            this.txt_RCC_Pier_d_dash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 32);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(187, 13);
            this.label66.TabIndex = 68;
            this.label66.Text = "Standard Minimum Reinforcement [p1]";
            // 
            // txt_RCC_Pier_p1
            // 
            this.txt_RCC_Pier_p1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_p1.Location = new System.Drawing.Point(234, 25);
            this.txt_RCC_Pier_p1.Name = "txt_RCC_Pier_p1";
            this.txt_RCC_Pier_p1.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_p1.TabIndex = 0;
            this.txt_RCC_Pier_p1.Text = "0.8";
            this.txt_RCC_Pier_p1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label226
            // 
            this.label226.AutoSize = true;
            this.label226.Location = new System.Drawing.Point(8, 184);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(147, 13);
            this.label226.TabIndex = 72;
            this.label226.Text = "Lateral Tie Bar Diameter [tdia]";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 55);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(156, 13);
            this.label67.TabIndex = 70;
            this.label67.Text = "Design Trial Reinforcement [p2]";
            // 
            // label224
            // 
            this.label224.AutoSize = true;
            this.label224.Location = new System.Drawing.Point(8, 157);
            this.label224.Name = "label224";
            this.label224.Size = new System.Drawing.Size(166, 13);
            this.label224.TabIndex = 72;
            this.label224.Text = "Reinforcement Bar Diameter [rdia]";
            // 
            // txt_RCC_Pier_p2
            // 
            this.txt_RCC_Pier_p2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_p2.Location = new System.Drawing.Point(234, 51);
            this.txt_RCC_Pier_p2.Name = "txt_RCC_Pier_p2";
            this.txt_RCC_Pier_p2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_p2.TabIndex = 1;
            this.txt_RCC_Pier_p2.Text = "1.5";
            this.txt_RCC_Pier_p2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(8, 79);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(125, 13);
            this.label68.TabIndex = 72;
            this.label68.Text = "Reinforcement Cover [d’]";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(533, 553);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(19, 13);
            this.label100.TabIndex = 22;
            this.label100.Text = "m";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(534, 515);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(19, 13);
            this.label99.TabIndex = 22;
            this.label99.Text = "m";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(533, 483);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(19, 13);
            this.label98.TabIndex = 22;
            this.label98.Text = "m";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(535, 108);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(19, 13);
            this.label97.TabIndex = 22;
            this.label97.Text = "m";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(535, 137);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(19, 13);
            this.label96.TabIndex = 22;
            this.label96.Text = "m";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.Location = new System.Drawing.Point(535, 160);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(19, 13);
            this.label95.TabIndex = 22;
            this.label95.Text = "m";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(535, 186);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(19, 13);
            this.label82.TabIndex = 22;
            this.label82.Text = "m";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(535, 222);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(19, 13);
            this.label81.TabIndex = 22;
            this.label81.Text = "m";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(535, 244);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(19, 13);
            this.label80.TabIndex = 22;
            this.label80.Text = "m";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(535, 276);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(19, 13);
            this.label79.TabIndex = 22;
            this.label79.Text = "m";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(535, 304);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(19, 13);
            this.label69.TabIndex = 22;
            this.label69.Text = "m";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(535, 327);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(19, 13);
            this.label74.TabIndex = 22;
            this.label74.Text = "m";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(237, 513);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(19, 13);
            this.label73.TabIndex = 22;
            this.label73.Text = "m";
            this.label73.Visible = false;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(237, 539);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(19, 13);
            this.label72.TabIndex = 22;
            this.label72.Text = "m";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(535, 378);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(19, 13);
            this.label71.TabIndex = 22;
            this.label71.Text = "m";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(535, 405);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(19, 13);
            this.label70.TabIndex = 22;
            this.label70.Text = "m";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(535, 434);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(19, 13);
            this.label92.TabIndex = 22;
            this.label92.Text = "m";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(535, 456);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(19, 13);
            this.label93.TabIndex = 22;
            this.label93.Text = "m";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(535, 82);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(19, 13);
            this.label94.TabIndex = 22;
            this.label94.Text = "m";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(536, 54);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(19, 13);
            this.label108.TabIndex = 22;
            this.label108.Text = "m";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(536, 28);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(19, 13);
            this.label109.TabIndex = 22;
            this.label109.Text = "m";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.Location = new System.Drawing.Point(237, 213);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(18, 14);
            this.label110.TabIndex = 22;
            this.label110.Text = "m";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(237, 187);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(18, 14);
            this.label111.TabIndex = 22;
            this.label111.Text = "m";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(237, 136);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(18, 14);
            this.label113.TabIndex = 22;
            this.label113.Text = "m";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(237, 109);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(18, 14);
            this.label114.TabIndex = 22;
            this.label114.Text = "m";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(237, 83);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(18, 14);
            this.label119.TabIndex = 22;
            this.label119.Text = "m";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(237, 58);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(18, 14);
            this.label120.TabIndex = 22;
            this.label120.Text = "m";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(237, 28);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(18, 14);
            this.label121.TabIndex = 22;
            this.label121.Text = "m";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label64);
            this.groupBox5.Controls.Add(this.label63);
            this.groupBox5.Controls.Add(this.label62);
            this.groupBox5.Controls.Add(this.txt_RCC_Pier_H2);
            this.groupBox5.Controls.Add(this.label128);
            this.groupBox5.Controls.Add(this.txt_RCC_Pier_B4);
            this.groupBox5.Controls.Add(this.label129);
            this.groupBox5.Controls.Add(this.txt_RCC_Pier_B3);
            this.groupBox5.Controls.Add(this.label130);
            this.groupBox5.Location = new System.Drawing.Point(3, 408);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(267, 97);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Size of Bearings (B3 x B4 x H2)";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(233, 68);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(19, 13);
            this.label64.TabIndex = 22;
            this.label64.Text = "m";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(233, 42);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(19, 13);
            this.label63.TabIndex = 22;
            this.label63.Text = "m";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(233, 16);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(19, 13);
            this.label62.TabIndex = 22;
            this.label62.Text = "m";
            // 
            // txt_RCC_Pier_H2
            // 
            this.txt_RCC_Pier_H2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H2.Location = new System.Drawing.Point(180, 65);
            this.txt_RCC_Pier_H2.Name = "txt_RCC_Pier_H2";
            this.txt_RCC_Pier_H2.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_H2.TabIndex = 2;
            this.txt_RCC_Pier_H2.Text = "0.078";
            this.txt_RCC_Pier_H2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(10, 72);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(21, 13);
            this.label128.TabIndex = 20;
            this.label128.Text = "H2";
            // 
            // txt_RCC_Pier_B4
            // 
            this.txt_RCC_Pier_B4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B4.Location = new System.Drawing.Point(180, 39);
            this.txt_RCC_Pier_B4.Name = "txt_RCC_Pier_B4";
            this.txt_RCC_Pier_B4.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B4.TabIndex = 1;
            this.txt_RCC_Pier_B4.Text = "0.32";
            this.txt_RCC_Pier_B4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(10, 46);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(20, 13);
            this.label129.TabIndex = 18;
            this.label129.Text = "B4";
            // 
            // txt_RCC_Pier_B3
            // 
            this.txt_RCC_Pier_B3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B3.Location = new System.Drawing.Point(180, 13);
            this.txt_RCC_Pier_B3.Name = "txt_RCC_Pier_B3";
            this.txt_RCC_Pier_B3.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B3.TabIndex = 0;
            this.txt_RCC_Pier_B3.Text = "0.50";
            this.txt_RCC_Pier_B3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(11, 20);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(20, 13);
            this.label130.TabIndex = 16;
            this.label130.Text = "B3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label61);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_RCC_Pier_H1);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.txt_RCC_Pier_B2);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.txt_RCC_Pier_B1);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Location = new System.Drawing.Point(6, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 90);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size of Pedestals (B1 x B2 x H1)";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(227, 68);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(19, 13);
            this.label61.TabIndex = 22;
            this.label61.Text = "m";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(227, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(227, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "m";
            // 
            // txt_RCC_Pier_H1
            // 
            this.txt_RCC_Pier_H1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H1.Location = new System.Drawing.Point(177, 66);
            this.txt_RCC_Pier_H1.Name = "txt_RCC_Pier_H1";
            this.txt_RCC_Pier_H1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_H1.TabIndex = 2;
            this.txt_RCC_Pier_H1.Text = "0.25";
            this.txt_RCC_Pier_H1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(10, 72);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(21, 13);
            this.label43.TabIndex = 20;
            this.label43.Text = "H1";
            // 
            // txt_RCC_Pier_B2
            // 
            this.txt_RCC_Pier_B2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B2.Location = new System.Drawing.Point(177, 40);
            this.txt_RCC_Pier_B2.Name = "txt_RCC_Pier_B2";
            this.txt_RCC_Pier_B2.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B2.TabIndex = 1;
            this.txt_RCC_Pier_B2.Text = "0.62";
            this.txt_RCC_Pier_B2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(10, 46);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(20, 13);
            this.label42.TabIndex = 18;
            this.label42.Text = "B2";
            // 
            // txt_RCC_Pier_B1
            // 
            this.txt_RCC_Pier_B1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B1.Location = new System.Drawing.Point(177, 14);
            this.txt_RCC_Pier_B1.Name = "txt_RCC_Pier_B1";
            this.txt_RCC_Pier_B1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B1.TabIndex = 0;
            this.txt_RCC_Pier_B1.Text = "0.80";
            this.txt_RCC_Pier_B1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(11, 20);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(20, 13);
            this.label41.TabIndex = 16;
            this.label41.Text = "B1";
            // 
            // txt_RCC_Pier_overall_height
            // 
            this.txt_RCC_Pier_overall_height.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_overall_height.Location = new System.Drawing.Point(476, 551);
            this.txt_RCC_Pier_overall_height.Name = "txt_RCC_Pier_overall_height";
            this.txt_RCC_Pier_overall_height.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_overall_height.TabIndex = 39;
            this.txt_RCC_Pier_overall_height.Text = "7.760";
            this.txt_RCC_Pier_overall_height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(282, 541);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(149, 26);
            this.label29.TabIndex = 62;
            this.label29.Text = "Overall Height of Substructure\r\n( [H7 + H5 + H6])";
            // 
            // txt_RCC_Pier_B14
            // 
            this.txt_RCC_Pier_B14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B14.Location = new System.Drawing.Point(477, 513);
            this.txt_RCC_Pier_B14.Name = "txt_RCC_Pier_B14";
            this.txt_RCC_Pier_B14.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B14.TabIndex = 38;
            this.txt_RCC_Pier_B14.Text = "10.0";
            this.txt_RCC_Pier_B14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(282, 508);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(145, 26);
            this.label30.TabIndex = 60;
            this.label30.Text = "Pier Cap width in Transverse \r\nDirection [B14]";
            // 
            // txt_RCC_Pier_B13
            // 
            this.txt_RCC_Pier_B13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B13.Location = new System.Drawing.Point(476, 480);
            this.txt_RCC_Pier_B13.Name = "txt_RCC_Pier_B13";
            this.txt_RCC_Pier_B13.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B13.TabIndex = 37;
            this.txt_RCC_Pier_B13.Text = "1.75";
            this.txt_RCC_Pier_B13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(282, 476);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(149, 26);
            this.label31.TabIndex = 58;
            this.label31.Text = "Pier Cap width in Longitudinal \r\nDirection [B13]";
            // 
            // txt_RCC_Pier_B12
            // 
            this.txt_RCC_Pier_B12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B12.Location = new System.Drawing.Point(476, 453);
            this.txt_RCC_Pier_B12.Name = "txt_RCC_Pier_B12";
            this.txt_RCC_Pier_B12.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B12.TabIndex = 35;
            this.txt_RCC_Pier_B12.Text = "5.0";
            this.txt_RCC_Pier_B12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(282, 456);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(183, 13);
            this.label13.TabIndex = 56;
            this.label13.Text = "Transverse width of Pier at Top [B12]";
            // 
            // txt_RCC_Pier_B11
            // 
            this.txt_RCC_Pier_B11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B11.Location = new System.Drawing.Point(476, 427);
            this.txt_RCC_Pier_B11.Name = "txt_RCC_Pier_B11";
            this.txt_RCC_Pier_B11.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B11.TabIndex = 34;
            this.txt_RCC_Pier_B11.Text = "1.0";
            this.txt_RCC_Pier_B11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(282, 430);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(187, 13);
            this.label14.TabIndex = 54;
            this.label14.Text = "Longitudinal width of Pier at Top [B11]";
            // 
            // txt_RCC_Pier_B10
            // 
            this.txt_RCC_Pier_B10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B10.Location = new System.Drawing.Point(476, 401);
            this.txt_RCC_Pier_B10.Name = "txt_RCC_Pier_B10";
            this.txt_RCC_Pier_B10.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B10.TabIndex = 33;
            this.txt_RCC_Pier_B10.Text = "5.0";
            this.txt_RCC_Pier_B10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(282, 408);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(188, 13);
            this.label34.TabIndex = 52;
            this.label34.Text = "Transverse width of Pier at Base [B10]";
            // 
            // txt_RCC_Pier_B9
            // 
            this.txt_RCC_Pier_B9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B9.Location = new System.Drawing.Point(476, 375);
            this.txt_RCC_Pier_B9.Name = "txt_RCC_Pier_B9";
            this.txt_RCC_Pier_B9.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B9.TabIndex = 32;
            this.txt_RCC_Pier_B9.Text = "0.9";
            this.txt_RCC_Pier_B9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(282, 378);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(186, 13);
            this.label35.TabIndex = 50;
            this.label35.Text = "Longitudinal width of Pier at Base [B9]";
            // 
            // txt_RCC_Pier_H6
            // 
            this.txt_RCC_Pier_H6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H6.Location = new System.Drawing.Point(476, 324);
            this.txt_RCC_Pier_H6.Name = "txt_RCC_Pier_H6";
            this.txt_RCC_Pier_H6.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H6.TabIndex = 30;
            this.txt_RCC_Pier_H6.Text = "0.80";
            this.txt_RCC_Pier_H6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(282, 331);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(152, 13);
            this.label36.TabIndex = 48;
            this.label36.Text = "Varying Depth of Pier Cap [H6]";
            // 
            // txt_RCC_Pier_H5
            // 
            this.txt_RCC_Pier_H5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H5.Location = new System.Drawing.Point(476, 298);
            this.txt_RCC_Pier_H5.Name = "txt_RCC_Pier_H5";
            this.txt_RCC_Pier_H5.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H5.TabIndex = 29;
            this.txt_RCC_Pier_H5.Text = "0.80";
            this.txt_RCC_Pier_H5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(282, 305);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(151, 13);
            this.label37.TabIndex = 46;
            this.label37.Text = "Straight depth of Pier Cap [H5]";
            // 
            // txt_RCC_Pier_B8
            // 
            this.txt_RCC_Pier_B8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B8.Location = new System.Drawing.Point(476, 273);
            this.txt_RCC_Pier_B8.Name = "txt_RCC_Pier_B8";
            this.txt_RCC_Pier_B8.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B8.TabIndex = 25;
            this.txt_RCC_Pier_B8.Text = "0.15";
            this.txt_RCC_Pier_B8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(282, 267);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 26);
            this.label16.TabIndex = 38;
            this.label16.Text = "P.C.C. Projection under \r\nFooting on either side [B8]";
            // 
            // txt_RCC_Pier_H4
            // 
            this.txt_RCC_Pier_H4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H4.Location = new System.Drawing.Point(476, 241);
            this.txt_RCC_Pier_H4.Name = "txt_RCC_Pier_H4";
            this.txt_RCC_Pier_H4.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H4.TabIndex = 24;
            this.txt_RCC_Pier_H4.Text = "0.80";
            this.txt_RCC_Pier_H4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(282, 244);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(147, 13);
            this.label17.TabIndex = 36;
            this.label17.Text = "Varying Depth of Footing [H4]";
            // 
            // txt_RCC_Pier_H3
            // 
            this.txt_RCC_Pier_H3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H3.Location = new System.Drawing.Point(476, 215);
            this.txt_RCC_Pier_H3.Name = "txt_RCC_Pier_H3";
            this.txt_RCC_Pier_H3.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H3.TabIndex = 23;
            this.txt_RCC_Pier_H3.Text = "0.80";
            this.txt_RCC_Pier_H3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(282, 215);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(148, 13);
            this.label21.TabIndex = 34;
            this.label21.Text = "Straight Depth of Footing [H3]";
            // 
            // txt_RCC_Pier_B7
            // 
            this.txt_RCC_Pier_B7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B7.Location = new System.Drawing.Point(476, 183);
            this.txt_RCC_Pier_B7.Name = "txt_RCC_Pier_B7";
            this.txt_RCC_Pier_B7.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B7.TabIndex = 22;
            this.txt_RCC_Pier_B7.Text = "9.50";
            this.txt_RCC_Pier_B7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(282, 186);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(107, 13);
            this.label22.TabIndex = 32;
            this.label22.Text = "Width of Footing [B7]";
            // 
            // txt_RCC_Pier_Form_Lev
            // 
            this.txt_RCC_Pier_Form_Lev.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Form_Lev.Location = new System.Drawing.Point(476, 157);
            this.txt_RCC_Pier_Form_Lev.Name = "txt_RCC_Pier_Form_Lev";
            this.txt_RCC_Pier_Form_Lev.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_Form_Lev.TabIndex = 21;
            this.txt_RCC_Pier_Form_Lev.Text = "531.505";
            this.txt_RCC_Pier_Form_Lev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(282, 160);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(187, 13);
            this.label23.TabIndex = 30;
            this.label23.Text = "Formation Level [RL1+d1+d2+H1+H2]";
            // 
            // txt_RCC_Pier_RL5
            // 
            this.txt_RCC_Pier_RL5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL5.Location = new System.Drawing.Point(476, 131);
            this.txt_RCC_Pier_RL5.Name = "txt_RCC_Pier_RL5";
            this.txt_RCC_Pier_RL5.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL5.TabIndex = 20;
            this.txt_RCC_Pier_RL5.Text = "520.42";
            this.txt_RCC_Pier_RL5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(282, 134);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(142, 13);
            this.label24.TabIndex = 28;
            this.label24.Text = "R.L. at Footing Bottom [RL5]";
            // 
            // txt_RCC_Pier_RL4
            // 
            this.txt_RCC_Pier_RL4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL4.Location = new System.Drawing.Point(476, 105);
            this.txt_RCC_Pier_RL4.Name = "txt_RCC_Pier_RL4";
            this.txt_RCC_Pier_RL4.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL4.TabIndex = 19;
            this.txt_RCC_Pier_RL4.Text = "521.62";
            this.txt_RCC_Pier_RL4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(282, 108);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(128, 13);
            this.label25.TabIndex = 26;
            this.label25.Text = "R.L. at Footing Top [RL4]";
            // 
            // txt_RCC_Pier_RL3
            // 
            this.txt_RCC_Pier_RL3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL3.Location = new System.Drawing.Point(476, 79);
            this.txt_RCC_Pier_RL3.Name = "txt_RCC_Pier_RL3";
            this.txt_RCC_Pier_RL3.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL3.TabIndex = 18;
            this.txt_RCC_Pier_RL3.Text = "523.417";
            this.txt_RCC_Pier_RL3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(282, 86);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(139, 13);
            this.label26.TabIndex = 24;
            this.label26.Text = "Existing Ground Level [RL3]";
            // 
            // txt_RCC_Pier_RL2
            // 
            this.txt_RCC_Pier_RL2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL2.Location = new System.Drawing.Point(476, 51);
            this.txt_RCC_Pier_RL2.Name = "txt_RCC_Pier_RL2";
            this.txt_RCC_Pier_RL2.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL2.TabIndex = 17;
            this.txt_RCC_Pier_RL2.Text = "527.39";
            this.txt_RCC_Pier_RL2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(282, 54);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(145, 13);
            this.label44.TabIndex = 22;
            this.label44.Text = "High Flood Level (HFL) [RL2]";
            // 
            // txt_RCC_Pier_RL1
            // 
            this.txt_RCC_Pier_RL1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL1.Location = new System.Drawing.Point(476, 25);
            this.txt_RCC_Pier_RL1.Name = "txt_RCC_Pier_RL1";
            this.txt_RCC_Pier_RL1.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL1.TabIndex = 16;
            this.txt_RCC_Pier_RL1.Text = "529.377";
            this.txt_RCC_Pier_RL1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(282, 28);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(133, 13);
            this.label45.TabIndex = 20;
            this.label45.Text = "R.L. at Pier Cap Top [RL1]";
            // 
            // txt_RCC_Pier_B6
            // 
            this.txt_RCC_Pier_B6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B6.Location = new System.Drawing.Point(183, 536);
            this.txt_RCC_Pier_B6.Name = "txt_RCC_Pier_B6";
            this.txt_RCC_Pier_B6.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B6.TabIndex = 15;
            this.txt_RCC_Pier_B6.Text = "5.50";
            this.txt_RCC_Pier_B6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(16, 539);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(112, 13);
            this.label46.TabIndex = 18;
            this.label46.Text = "Length of Footing [B6]";
            // 
            // txt_RCC_Pier_B5
            // 
            this.txt_RCC_Pier_B5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B5.Location = new System.Drawing.Point(183, 510);
            this.txt_RCC_Pier_B5.Name = "txt_RCC_Pier_B5";
            this.txt_RCC_Pier_B5.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B5.TabIndex = 14;
            this.txt_RCC_Pier_B5.Text = "2.65";
            this.txt_RCC_Pier_B5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_RCC_Pier_B5.Visible = false;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(16, 513);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(152, 13);
            this.label47.TabIndex = 13;
            this.label47.Text = "Distance Between Girders [B5]";
            this.label47.Visible = false;
            // 
            // txt_RCC_Pier_d2
            // 
            this.txt_RCC_Pier_d2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_d2.Location = new System.Drawing.Point(183, 210);
            this.txt_RCC_Pier_d2.Name = "txt_RCC_Pier_d2";
            this.txt_RCC_Pier_d2.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_d2.TabIndex = 7;
            this.txt_RCC_Pier_d2.Text = "0.25";
            this.txt_RCC_Pier_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(16, 213);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(122, 13);
            this.label48.TabIndex = 14;
            this.label48.Text = "Depth of Deck Slab [d2]";
            // 
            // txt_RCC_Pier_d1
            // 
            this.txt_RCC_Pier_d1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_d1.Location = new System.Drawing.Point(183, 184);
            this.txt_RCC_Pier_d1.Name = "txt_RCC_Pier_d1";
            this.txt_RCC_Pier_d1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_d1.TabIndex = 6;
            this.txt_RCC_Pier_d1.Text = "1.55";
            this.txt_RCC_Pier_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(16, 187);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(100, 13);
            this.label49.TabIndex = 12;
            this.label49.Text = "Depth of Girder [d1]";
            // 
            // txt_RCC_Pier_NB
            // 
            this.txt_RCC_Pier_NB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_NB.Location = new System.Drawing.Point(183, 158);
            this.txt_RCC_Pier_NB.Name = "txt_RCC_Pier_NB";
            this.txt_RCC_Pier_NB.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_NB.TabIndex = 5;
            this.txt_RCC_Pier_NB.Text = "4";
            this.txt_RCC_Pier_NB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(16, 161);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(124, 13);
            this.label50.TabIndex = 10;
            this.label50.Text = "Number of Bearings [NB]";
            // 
            // txt_RCC_Pier_a1
            // 
            this.txt_RCC_Pier_a1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_a1.Location = new System.Drawing.Point(183, 132);
            this.txt_RCC_Pier_a1.Name = "txt_RCC_Pier_a1";
            this.txt_RCC_Pier_a1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_a1.TabIndex = 4;
            this.txt_RCC_Pier_a1.Text = "1.05";
            this.txt_RCC_Pier_a1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(16, 135);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(134, 13);
            this.label51.TabIndex = 8;
            this.label51.Text = "Height of Crash Barrier [a1]";
            // 
            // txt_RCC_Pier_w3
            // 
            this.txt_RCC_Pier_w3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_w3.Location = new System.Drawing.Point(183, 106);
            this.txt_RCC_Pier_w3.Name = "txt_RCC_Pier_w3";
            this.txt_RCC_Pier_w3.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_w3.TabIndex = 3;
            this.txt_RCC_Pier_w3.Text = "0.50";
            this.txt_RCC_Pier_w3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(16, 109);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(133, 13);
            this.label52.TabIndex = 6;
            this.label52.Text = "Width of Crash Barrier [w3]";
            // 
            // txt_RCC_Pier_w2
            // 
            this.txt_RCC_Pier_w2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_w2.Location = new System.Drawing.Point(183, 80);
            this.txt_RCC_Pier_w2.Name = "txt_RCC_Pier_w2";
            this.txt_RCC_Pier_w2.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_w2.TabIndex = 2;
            this.txt_RCC_Pier_w2.Text = "12.50";
            this.txt_RCC_Pier_w2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(16, 83);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(132, 13);
            this.label53.TabIndex = 4;
            this.label53.Text = "Overall width of Deck [w2]";
            // 
            // txt_RCC_Pier_w1
            // 
            this.txt_RCC_Pier_w1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_w1.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_w1.Location = new System.Drawing.Point(183, 54);
            this.txt_RCC_Pier_w1.Name = "txt_RCC_Pier_w1";
            this.txt_RCC_Pier_w1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_w1.TabIndex = 1;
            this.txt_RCC_Pier_w1.Text = "9.75";
            this.txt_RCC_Pier_w1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(16, 57);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(116, 13);
            this.label54.TabIndex = 2;
            this.label54.Text = "Carriageway width [w1]";
            // 
            // txt_RCC_Pier_L1
            // 
            this.txt_RCC_Pier_L1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_L1.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_L1.Location = new System.Drawing.Point(183, 25);
            this.txt_RCC_Pier_L1.Name = "txt_RCC_Pier_L1";
            this.txt_RCC_Pier_L1.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_L1.TabIndex = 0;
            this.txt_RCC_Pier_L1.Text = "18.0";
            this.txt_RCC_Pier_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(16, 28);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(162, 13);
            this.label55.TabIndex = 0;
            this.label55.Text = "C/C Distance between Piers [L1]";
            // 
            // tab_des_form2
            // 
            this.tab_des_form2.Controls.Add(this.groupBox15);
            this.tab_des_form2.Controls.Add(this.label217);
            this.tab_des_form2.Controls.Add(this.label218);
            this.tab_des_form2.Controls.Add(this.label159);
            this.tab_des_form2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_des_form2.Location = new System.Drawing.Point(4, 22);
            this.tab_des_form2.Name = "tab_des_form2";
            this.tab_des_form2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_form2.Size = new System.Drawing.Size(940, 596);
            this.tab_des_form2.TabIndex = 2;
            this.tab_des_form2.Text = "Design Input Data [Form2]";
            this.tab_des_form2.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label191);
            this.groupBox15.Controls.Add(this.label190);
            this.groupBox15.Controls.Add(this.label189);
            this.groupBox15.Controls.Add(this.label188);
            this.groupBox15.Controls.Add(this.label187);
            this.groupBox15.Controls.Add(this.label197);
            this.groupBox15.Controls.Add(this.label234);
            this.groupBox15.Controls.Add(this.label231);
            this.groupBox15.Controls.Add(this.label229);
            this.groupBox15.Controls.Add(this.label196);
            this.groupBox15.Controls.Add(this.label232);
            this.groupBox15.Controls.Add(this.label195);
            this.groupBox15.Controls.Add(this.label194);
            this.groupBox15.Controls.Add(this.label193);
            this.groupBox15.Controls.Add(this.label192);
            this.groupBox15.Controls.Add(this.label186);
            this.groupBox15.Controls.Add(this.label185);
            this.groupBox15.Controls.Add(this.cmb_pier_2_k);
            this.groupBox15.Controls.Add(this.txt_pier_2_SBC);
            this.groupBox15.Controls.Add(this.label184);
            this.groupBox15.Controls.Add(this.txt_pier_2_vspc);
            this.groupBox15.Controls.Add(this.label233);
            this.groupBox15.Controls.Add(this.txt_pier_2_vdia);
            this.groupBox15.Controls.Add(this.label230);
            this.groupBox15.Controls.Add(this.txt_pier_2_hdia);
            this.groupBox15.Controls.Add(this.label228);
            this.groupBox15.Controls.Add(this.txt_pier_2_ldia);
            this.groupBox15.Controls.Add(this.label183);
            this.groupBox15.Controls.Add(this.txt_pier_2_slegs);
            this.groupBox15.Controls.Add(this.label182);
            this.groupBox15.Controls.Add(this.txt_pier_2_sdia);
            this.groupBox15.Controls.Add(this.label181);
            this.groupBox15.Controls.Add(this.txt_pier_2_Itc);
            this.groupBox15.Controls.Add(this.label180);
            this.groupBox15.Controls.Add(this.txt_pier_2_Vr);
            this.groupBox15.Controls.Add(this.label179);
            this.groupBox15.Controls.Add(this.txt_pier_2_LL);
            this.groupBox15.Controls.Add(this.label178);
            this.groupBox15.Controls.Add(this.txt_pier_2_CF);
            this.groupBox15.Controls.Add(this.label177);
            this.groupBox15.Controls.Add(this.txt_pier_2_k);
            this.groupBox15.Controls.Add(this.label176);
            this.groupBox15.Controls.Add(this.txt_pier_2_V);
            this.groupBox15.Controls.Add(this.label175);
            this.groupBox15.Controls.Add(this.txt_pier_2_HHF);
            this.groupBox15.Controls.Add(this.label174);
            this.groupBox15.Controls.Add(this.txt_pier_2_SC);
            this.groupBox15.Controls.Add(this.label173);
            this.groupBox15.Controls.Add(this.txt_pier_2_PD);
            this.groupBox15.Controls.Add(this.label172);
            this.groupBox15.Controls.Add(this.txt_pier_2_PML);
            this.groupBox15.Controls.Add(this.label171);
            this.groupBox15.Controls.Add(this.txt_pier_2_PL);
            this.groupBox15.Controls.Add(this.label170);
            this.groupBox15.Controls.Add(this.label169);
            this.groupBox15.Controls.Add(this.txt_pier_2_APD);
            this.groupBox15.Controls.Add(this.label168);
            this.groupBox15.Controls.Add(this.txt_pier_2_B16);
            this.groupBox15.Controls.Add(this.label160);
            this.groupBox15.Controls.Add(this.txt_pier_2_P3);
            this.groupBox15.Controls.Add(this.label161);
            this.groupBox15.Controls.Add(this.txt_pier_2_P2);
            this.groupBox15.Location = new System.Drawing.Point(7, 61);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(927, 442);
            this.groupBox15.TabIndex = 174;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Design of Pier Cap";
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label191.Location = new System.Drawing.Point(857, 22);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(49, 13);
            this.label191.TabIndex = 39;
            this.label191.Text = "m/sec";
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label190.Location = new System.Drawing.Point(419, 389);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(19, 13);
            this.label190.TabIndex = 39;
            this.label190.Text = "m";
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label189.Location = new System.Drawing.Point(419, 275);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(24, 13);
            this.label189.TabIndex = 39;
            this.label189.Text = "kN";
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label188.Location = new System.Drawing.Point(419, 231);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(24, 13);
            this.label188.TabIndex = 39;
            this.label188.Text = "kN";
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label187.Location = new System.Drawing.Point(419, 92);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(19, 13);
            this.label187.TabIndex = 39;
            this.label187.Text = "m";
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label197.Location = new System.Drawing.Point(857, 389);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(63, 13);
            this.label197.TabIndex = 39;
            this.label197.Text = "kN/sq.m";
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label231.Location = new System.Drawing.Point(857, 328);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(31, 13);
            this.label231.TabIndex = 39;
            this.label231.Text = "mm";
            // 
            // label229
            // 
            this.label229.AutoSize = true;
            this.label229.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label229.Location = new System.Drawing.Point(857, 301);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(31, 13);
            this.label229.TabIndex = 39;
            this.label229.Text = "mm";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label196.Location = new System.Drawing.Point(857, 274);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(31, 13);
            this.label196.TabIndex = 39;
            this.label196.Text = "mm";
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label232.Location = new System.Drawing.Point(857, 239);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(30, 13);
            this.label232.TabIndex = 39;
            this.label232.Text = "nos";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label195.Location = new System.Drawing.Point(857, 212);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(31, 13);
            this.label195.TabIndex = 39;
            this.label195.Text = "mm";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label194.Location = new System.Drawing.Point(857, 185);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(56, 13);
            this.label194.TabIndex = 39;
            this.label194.Text = "kN/mm";
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label193.Location = new System.Drawing.Point(857, 160);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(56, 13);
            this.label193.TabIndex = 39;
            this.label193.Text = "kN/mm";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label192.Location = new System.Drawing.Point(857, 126);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(24, 13);
            this.label192.TabIndex = 39;
            this.label192.Text = "kN";
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label186.Location = new System.Drawing.Point(419, 58);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(24, 13);
            this.label186.TabIndex = 39;
            this.label186.Text = "kN";
            // 
            // label185
            // 
            this.label185.AutoSize = true;
            this.label185.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label185.Location = new System.Drawing.Point(419, 22);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(24, 13);
            this.label185.TabIndex = 39;
            this.label185.Text = "kN";
            // 
            // cmb_pier_2_k
            // 
            this.cmb_pier_2_k.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pier_2_k.FormattingEnabled = true;
            this.cmb_pier_2_k.Items.AddRange(new object[] {
            "Square End Piers",
            "Semi Circular End Piers",
            "Angular End Piers",
            "User Defined Value"});
            this.cmb_pier_2_k.Location = new System.Drawing.Point(607, 55);
            this.cmb_pier_2_k.Name = "cmb_pier_2_k";
            this.cmb_pier_2_k.Size = new System.Drawing.Size(164, 21);
            this.cmb_pier_2_k.TabIndex = 38;
            this.cmb_pier_2_k.SelectedIndexChanged += new System.EventHandler(this.cmb_pier_2_k_SelectedIndexChanged);
            // 
            // txt_pier_2_SBC
            // 
            this.txt_pier_2_SBC.Location = new System.Drawing.Point(777, 386);
            this.txt_pier_2_SBC.Name = "txt_pier_2_SBC";
            this.txt_pier_2_SBC.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_SBC.TabIndex = 37;
            this.txt_pier_2_SBC.Text = "150";
            this.txt_pier_2_SBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label184
            // 
            this.label184.AutoSize = true;
            this.label184.Location = new System.Drawing.Point(459, 389);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(184, 13);
            this.label184.TabIndex = 36;
            this.label184.Text = "Bearing Capacity of Soil [SBC]";
            // 
            // txt_pier_2_vdia
            // 
            this.txt_pier_2_vdia.Location = new System.Drawing.Point(777, 325);
            this.txt_pier_2_vdia.Name = "txt_pier_2_vdia";
            this.txt_pier_2_vdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_vdia.TabIndex = 35;
            this.txt_pier_2_vdia.Text = "10";
            this.txt_pier_2_vdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label230
            // 
            this.label230.AutoSize = true;
            this.label230.Location = new System.Drawing.Point(459, 328);
            this.label230.Name = "label230";
            this.label230.Size = new System.Drawing.Size(232, 13);
            this.label230.TabIndex = 34;
            this.label230.Text = "Diameter of Vertical Stirrup Bars [vdia]";
            // 
            // txt_pier_2_hdia
            // 
            this.txt_pier_2_hdia.Location = new System.Drawing.Point(777, 298);
            this.txt_pier_2_hdia.Name = "txt_pier_2_hdia";
            this.txt_pier_2_hdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_hdia.TabIndex = 35;
            this.txt_pier_2_hdia.Text = "12";
            this.txt_pier_2_hdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label228
            // 
            this.label228.AutoSize = true;
            this.label228.Location = new System.Drawing.Point(459, 301);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(247, 13);
            this.label228.TabIndex = 34;
            this.label228.Text = "Diameter of Horizontal Stirrup Bars [hdia]";
            // 
            // txt_pier_2_ldia
            // 
            this.txt_pier_2_ldia.Location = new System.Drawing.Point(777, 271);
            this.txt_pier_2_ldia.Name = "txt_pier_2_ldia";
            this.txt_pier_2_ldia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_ldia.TabIndex = 35;
            this.txt_pier_2_ldia.Text = "25";
            this.txt_pier_2_ldia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label183
            // 
            this.label183.AutoSize = true;
            this.label183.Location = new System.Drawing.Point(459, 274);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(295, 13);
            this.label183.TabIndex = 34;
            this.label183.Text = "Diameter of Longitudinal reinforcement Bars [ldia]";
            // 
            // txt_pier_2_slegs
            // 
            this.txt_pier_2_slegs.Location = new System.Drawing.Point(777, 236);
            this.txt_pier_2_slegs.Name = "txt_pier_2_slegs";
            this.txt_pier_2_slegs.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_slegs.TabIndex = 33;
            this.txt_pier_2_slegs.Text = "6";
            this.txt_pier_2_slegs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Location = new System.Drawing.Point(459, 244);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(230, 13);
            this.label182.TabIndex = 32;
            this.label182.Text = "Shear Reinforcement Legs Nos. [slegs]";
            // 
            // txt_pier_2_sdia
            // 
            this.txt_pier_2_sdia.Location = new System.Drawing.Point(777, 209);
            this.txt_pier_2_sdia.Name = "txt_pier_2_sdia";
            this.txt_pier_2_sdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_sdia.TabIndex = 31;
            this.txt_pier_2_sdia.Text = "16";
            this.txt_pier_2_sdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Location = new System.Drawing.Point(459, 217);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(223, 13);
            this.label181.TabIndex = 30;
            this.label181.Text = "Diameter of Reinforcement Bar [sdia]";
            // 
            // txt_pier_2_Itc
            // 
            this.txt_pier_2_Itc.Location = new System.Drawing.Point(777, 182);
            this.txt_pier_2_Itc.Name = "txt_pier_2_Itc";
            this.txt_pier_2_Itc.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_Itc.TabIndex = 29;
            this.txt_pier_2_Itc.Text = "4.21";
            this.txt_pier_2_Itc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(459, 187);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(226, 13);
            this.label180.TabIndex = 28;
            this.label180.Text = "Shirnkage Force on Each Bearing [Itc]";
            // 
            // txt_pier_2_Vr
            // 
            this.txt_pier_2_Vr.Location = new System.Drawing.Point(777, 155);
            this.txt_pier_2_Vr.Name = "txt_pier_2_Vr";
            this.txt_pier_2_Vr.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_Vr.TabIndex = 27;
            this.txt_pier_2_Vr.Text = "2.5";
            this.txt_pier_2_Vr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(459, 163);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(239, 13);
            this.label179.TabIndex = 26;
            this.label179.Text = "Temperature Force on Each Bearing [Vr]";
            // 
            // txt_pier_2_LL
            // 
            this.txt_pier_2_LL.Location = new System.Drawing.Point(777, 123);
            this.txt_pier_2_LL.Name = "txt_pier_2_LL";
            this.txt_pier_2_LL.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_LL.TabIndex = 25;
            this.txt_pier_2_LL.Text = "1000";
            this.txt_pier_2_LL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(459, 131);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(222, 13);
            this.label178.TabIndex = 24;
            this.label178.Text = "Breaking Force 20% of Live Load [LL]";
            // 
            // txt_pier_2_CF
            // 
            this.txt_pier_2_CF.Location = new System.Drawing.Point(777, 89);
            this.txt_pier_2_CF.Name = "txt_pier_2_CF";
            this.txt_pier_2_CF.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_CF.TabIndex = 23;
            this.txt_pier_2_CF.Text = "0.6";
            this.txt_pier_2_CF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(459, 84);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(180, 26);
            this.label177.TabIndex = 22;
            this.label177.Text = "Coefficient of Friction between\r\nConcrete and River Bed [CF]";
            // 
            // txt_pier_2_k
            // 
            this.txt_pier_2_k.Location = new System.Drawing.Point(777, 55);
            this.txt_pier_2_k.Name = "txt_pier_2_k";
            this.txt_pier_2_k.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_k.TabIndex = 21;
            this.txt_pier_2_k.Text = "0.66";
            this.txt_pier_2_k.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Location = new System.Drawing.Point(459, 58);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(145, 13);
            this.label176.TabIndex = 20;
            this.label176.Text = "Pier Shape Constant [k]";
            // 
            // txt_pier_2_V
            // 
            this.txt_pier_2_V.Location = new System.Drawing.Point(777, 19);
            this.txt_pier_2_V.Name = "txt_pier_2_V";
            this.txt_pier_2_V.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_V.TabIndex = 19;
            this.txt_pier_2_V.Text = "8.0";
            this.txt_pier_2_V.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(459, 27);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(261, 13);
            this.label175.TabIndex = 18;
            this.label175.Text = "Observed Velocity of water at High Flood [V]";
            // 
            // txt_pier_2_HHF
            // 
            this.txt_pier_2_HHF.Location = new System.Drawing.Point(327, 386);
            this.txt_pier_2_HHF.Name = "txt_pier_2_HHF";
            this.txt_pier_2_HHF.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_HHF.TabIndex = 17;
            this.txt_pier_2_HHF.Text = "6.0";
            this.txt_pier_2_HHF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(11, 381);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(304, 26);
            this.label174.TabIndex = 16;
            this.label174.Text = "Height of Water from River Bed at High Flood [HHF] \r\n(put value 0 if not required" +
    ")\r\n";
            // 
            // txt_pier_2_SC
            // 
            this.txt_pier_2_SC.Location = new System.Drawing.Point(327, 345);
            this.txt_pier_2_SC.Name = "txt_pier_2_SC";
            this.txt_pier_2_SC.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_SC.TabIndex = 15;
            this.txt_pier_2_SC.Text = "0.18";
            this.txt_pier_2_SC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(11, 337);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(170, 26);
            this.label173.TabIndex = 14;
            this.label173.Text = "Seismic Coefficient [SC]\r\n (put value 0 if not required)";
            // 
            // txt_pier_2_PD
            // 
            this.txt_pier_2_PD.Location = new System.Drawing.Point(263, 187);
            this.txt_pier_2_PD.Name = "txt_pier_2_PD";
            this.txt_pier_2_PD.ReadOnly = true;
            this.txt_pier_2_PD.Size = new System.Drawing.Size(151, 21);
            this.txt_pier_2_PD.TabIndex = 13;
            this.txt_pier_2_PD.Text = "2.0,0.5";
            this.txt_pier_2_PD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(11, 182);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(246, 26);
            this.label172.TabIndex = 12;
            this.label172.Text = "(Get Distances of each pairs of pedestals \r\nwithin the distance of B16) [PD]";
            // 
            // txt_pier_2_PML
            // 
            this.txt_pier_2_PML.Location = new System.Drawing.Point(327, 272);
            this.txt_pier_2_PML.Name = "txt_pier_2_PML";
            this.txt_pier_2_PML.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_PML.TabIndex = 11;
            this.txt_pier_2_PML.Text = "195.56";
            this.txt_pier_2_PML.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(11, 267);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(259, 26);
            this.label171.TabIndex = 10;
            this.label171.Text = "(Get Moments on each \r\nPedestal = Total Moment / total Pairs) [PML]";
            // 
            // txt_pier_2_PL
            // 
            this.txt_pier_2_PL.Location = new System.Drawing.Point(327, 228);
            this.txt_pier_2_PL.Name = "txt_pier_2_PL";
            this.txt_pier_2_PL.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_PL.TabIndex = 9;
            this.txt_pier_2_PL.Text = "306.4";
            this.txt_pier_2_PL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(11, 220);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(303, 26);
            this.label170.TabIndex = 8;
            this.label170.Text = "(Get Load Reactions on each pair of\r\nPedestals =   Total Load Reaction / total Pa" +
    "irs ) [PL]";
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(11, 126);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(237, 39);
            this.label169.TabIndex = 7;
            this.label169.Text = "Distances from Left Edge of Pier Cap to \r\nCentre of Each  pair of Pedestals [APD]" +
    "\r\n(seperated by comma \',\' or space \' \')";
            // 
            // txt_pier_2_APD
            // 
            this.txt_pier_2_APD.Location = new System.Drawing.Point(263, 116);
            this.txt_pier_2_APD.Multiline = true;
            this.txt_pier_2_APD.Name = "txt_pier_2_APD";
            this.txt_pier_2_APD.Size = new System.Drawing.Size(151, 54);
            this.txt_pier_2_APD.TabIndex = 6;
            this.txt_pier_2_APD.Text = "0.5,2.0,3.5,6.5, 8.0, 9.5";
            this.txt_pier_2_APD.TextChanged += new System.EventHandler(this.txt_pier_2_APD_TextChanged);
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(11, 84);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(201, 26);
            this.label168.TabIndex = 5;
            this.label168.Text = "Distance from Left Edge Pier Cap \r\nEdge to Left face of Pier [B16]";
            // 
            // txt_pier_2_B16
            // 
            this.txt_pier_2_B16.Location = new System.Drawing.Point(327, 89);
            this.txt_pier_2_B16.Name = "txt_pier_2_B16";
            this.txt_pier_2_B16.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_B16.TabIndex = 4;
            this.txt_pier_2_B16.Text = "2.5";
            this.txt_pier_2_B16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_pier_2_B16.TextChanged += new System.EventHandler(this.txt_pier_2_APD_TextChanged);
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(11, 58);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(283, 13);
            this.label160.TabIndex = 3;
            this.label160.Text = "Live Load Support Reaction for all Supports [P3]";
            // 
            // txt_pier_2_P3
            // 
            this.txt_pier_2_P3.ForeColor = System.Drawing.Color.Red;
            this.txt_pier_2_P3.Location = new System.Drawing.Point(327, 53);
            this.txt_pier_2_P3.Name = "txt_pier_2_P3";
            this.txt_pier_2_P3.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_P3.TabIndex = 2;
            this.txt_pier_2_P3.Text = "338";
            this.txt_pier_2_P3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Location = new System.Drawing.Point(11, 27);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(290, 13);
            this.label161.TabIndex = 1;
            this.label161.Text = "Dead Load Support Reaction for all Supports [P2]";
            this.label161.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_pier_2_P2
            // 
            this.txt_pier_2_P2.ForeColor = System.Drawing.Color.Red;
            this.txt_pier_2_P2.Location = new System.Drawing.Point(327, 19);
            this.txt_pier_2_P2.Name = "txt_pier_2_P2";
            this.txt_pier_2_P2.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_P2.TabIndex = 0;
            this.txt_pier_2_P2.Text = "1500";
            this.txt_pier_2_P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label217.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label217.ForeColor = System.Drawing.Color.Red;
            this.label217.Location = new System.Drawing.Point(432, 13);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(218, 18);
            this.label217.TabIndex = 173;
            this.label217.Text = "Default Sample Data are shown";
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label218.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label218.ForeColor = System.Drawing.Color.Green;
            this.label218.Location = new System.Drawing.Point(291, 13);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(135, 18);
            this.label218.TabIndex = 172;
            this.label218.Text = "All User Input Data";
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(6, 41);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(526, 13);
            this.label159.TabIndex = 1;
            this.label159.Text = "Various Load Reactions for Supports within this distance are read from Analysis R" +
    "eport File";
            // 
            // tab_des_Diagram
            // 
            this.tab_des_Diagram.Controls.Add(this.pic_pier_interactive_diagram);
            this.tab_des_Diagram.Location = new System.Drawing.Point(4, 22);
            this.tab_des_Diagram.Name = "tab_des_Diagram";
            this.tab_des_Diagram.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_Diagram.Size = new System.Drawing.Size(940, 596);
            this.tab_des_Diagram.TabIndex = 1;
            this.tab_des_Diagram.Text = "Diagram";
            this.tab_des_Diagram.UseVisualStyleBackColor = true;
            // 
            // pic_pier_interactive_diagram
            // 
            this.pic_pier_interactive_diagram.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Pier_Image_for_RCC___PSC_Bridges;
            this.pic_pier_interactive_diagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_pier_interactive_diagram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_pier_interactive_diagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_pier_interactive_diagram.Location = new System.Drawing.Point(3, 3);
            this.pic_pier_interactive_diagram.Name = "pic_pier_interactive_diagram";
            this.pic_pier_interactive_diagram.Size = new System.Drawing.Size(934, 590);
            this.pic_pier_interactive_diagram.TabIndex = 0;
            this.pic_pier_interactive_diagram.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label208);
            this.panel2.Controls.Add(this.btn_RCC_Pier_Process);
            this.panel2.Controls.Add(this.btn_RCC_Pier_Report);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 625);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 46);
            this.panel2.TabIndex = 2;
            // 
            // label208
            // 
            this.label208.AutoSize = true;
            this.label208.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label208.ForeColor = System.Drawing.Color.Red;
            this.label208.Location = new System.Drawing.Point(409, 3);
            this.label208.Name = "label208";
            this.label208.Size = new System.Drawing.Size(124, 14);
            this.label208.TabIndex = 120;
            this.label208.Text = "Design of RCC Pier";
            // 
            // btn_RCC_Pier_Process
            // 
            this.btn_RCC_Pier_Process.Location = new System.Drawing.Point(368, 18);
            this.btn_RCC_Pier_Process.Name = "btn_RCC_Pier_Process";
            this.btn_RCC_Pier_Process.Size = new System.Drawing.Size(92, 25);
            this.btn_RCC_Pier_Process.TabIndex = 1;
            this.btn_RCC_Pier_Process.Text = "Process";
            this.btn_RCC_Pier_Process.UseVisualStyleBackColor = true;
            this.btn_RCC_Pier_Process.Click += new System.EventHandler(this.btn_RccPier_Process_Click);
            // 
            // btn_RCC_Pier_Report
            // 
            this.btn_RCC_Pier_Report.Location = new System.Drawing.Point(481, 18);
            this.btn_RCC_Pier_Report.Name = "btn_RCC_Pier_Report";
            this.btn_RCC_Pier_Report.Size = new System.Drawing.Size(92, 25);
            this.btn_RCC_Pier_Report.TabIndex = 2;
            this.btn_RCC_Pier_Report.Text = "Report";
            this.btn_RCC_Pier_Report.UseVisualStyleBackColor = true;
            this.btn_RCC_Pier_Report.Click += new System.EventHandler(this.btn_RccPier_Report_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label219);
            this.tabPage3.Controls.Add(this.label221);
            this.tabPage3.Controls.Add(this.label209);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.btnProcess);
            this.tabPage3.Controls.Add(this.btnReport);
            this.tabPage3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(954, 674);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Stone Masonry Pier";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label219.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label219.ForeColor = System.Drawing.Color.Red;
            this.label219.Location = new System.Drawing.Point(414, 11);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(218, 18);
            this.label219.TabIndex = 173;
            this.label219.Text = "Default Sample Data are shown";
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label221.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.ForeColor = System.Drawing.Color.Green;
            this.label221.Location = new System.Drawing.Point(273, 11);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(135, 18);
            this.label221.TabIndex = 172;
            this.label221.Text = "All User Input Data";
            // 
            // label209
            // 
            this.label209.AutoSize = true;
            this.label209.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label209.ForeColor = System.Drawing.Color.Red;
            this.label209.Location = new System.Drawing.Point(375, 507);
            this.label209.Name = "label209";
            this.label209.Size = new System.Drawing.Size(192, 14);
            this.label209.TabIndex = 120;
            this.label209.Text = "Design of Stone Masonry Pier";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label131);
            this.groupBox6.Controls.Add(this.label132);
            this.groupBox6.Controls.Add(this.label133);
            this.groupBox6.Controls.Add(this.label134);
            this.groupBox6.Controls.Add(this.label135);
            this.groupBox6.Controls.Add(this.label136);
            this.groupBox6.Controls.Add(this.label137);
            this.groupBox6.Controls.Add(this.label138);
            this.groupBox6.Controls.Add(this.label139);
            this.groupBox6.Controls.Add(this.label140);
            this.groupBox6.Controls.Add(this.label141);
            this.groupBox6.Controls.Add(this.label142);
            this.groupBox6.Controls.Add(this.label143);
            this.groupBox6.Controls.Add(this.txt_V);
            this.groupBox6.Controls.Add(this.label144);
            this.groupBox6.Controls.Add(this.txt_F);
            this.groupBox6.Controls.Add(this.label145);
            this.groupBox6.Controls.Add(this.txt_A);
            this.groupBox6.Controls.Add(this.label146);
            this.groupBox6.Controls.Add(this.txt_f2);
            this.groupBox6.Controls.Add(this.label147);
            this.groupBox6.Controls.Add(this.txt_f1);
            this.groupBox6.Controls.Add(this.label148);
            this.groupBox6.Controls.Add(this.txt_gamma_c);
            this.groupBox6.Controls.Add(this.label149);
            this.groupBox6.Controls.Add(this.txt_HFL);
            this.groupBox6.Controls.Add(this.label150);
            this.groupBox6.Controls.Add(this.txt_h);
            this.groupBox6.Controls.Add(this.label151);
            this.groupBox6.Controls.Add(this.txt_l);
            this.groupBox6.Controls.Add(this.label152);
            this.groupBox6.Controls.Add(this.txt_b2);
            this.groupBox6.Controls.Add(this.label153);
            this.groupBox6.Controls.Add(this.txt_b1);
            this.groupBox6.Controls.Add(this.label154);
            this.groupBox6.Controls.Add(this.txt_w3);
            this.groupBox6.Controls.Add(this.label155);
            this.groupBox6.Controls.Add(this.txt_e);
            this.groupBox6.Controls.Add(this.label156);
            this.groupBox6.Controls.Add(this.txt_w2);
            this.groupBox6.Controls.Add(this.label157);
            this.groupBox6.Controls.Add(this.txt_w1);
            this.groupBox6.Controls.Add(this.label158);
            this.groupBox6.Controls.Add(this.pic_stone_masonry);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox6.Location = new System.Drawing.Point(57, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(882, 460);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "USER DATA : Design Bridge Piers";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(383, 355);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(22, 16);
            this.label131.TabIndex = 35;
            this.label131.Text = "m";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label132.Location = new System.Drawing.Point(383, 187);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(56, 16);
            this.label132.TabIndex = 31;
            this.label132.Text = "m/sec";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(383, 161);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(71, 16);
            this.label133.TabIndex = 31;
            this.label133.Text = "kN/sq.m";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(383, 131);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(44, 16);
            this.label134.TabIndex = 31;
            this.label134.Text = "sq.m";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(383, 328);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(71, 16);
            this.label135.TabIndex = 31;
            this.label135.Text = "kN/cu.m";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.Location = new System.Drawing.Point(383, 104);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(22, 16);
            this.label136.TabIndex = 31;
            this.label136.Text = "m";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(383, 76);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(22, 16);
            this.label137.TabIndex = 31;
            this.label137.Text = "m";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(383, 46);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(22, 16);
            this.label138.TabIndex = 31;
            this.label138.Text = "m";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(383, 19);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(22, 16);
            this.label139.TabIndex = 31;
            this.label139.Text = "m";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(383, 214);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(22, 16);
            this.label140.TabIndex = 31;
            this.label140.Text = "m";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(383, 300);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(26, 16);
            this.label141.TabIndex = 31;
            this.label141.Text = "kN";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label142.Location = new System.Drawing.Point(383, 273);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(26, 16);
            this.label142.TabIndex = 31;
            this.label142.Text = "kN";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(383, 244);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(26, 16);
            this.label143.TabIndex = 31;
            this.label143.Text = "kN";
            // 
            // txt_V
            // 
            this.txt_V.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_V.Location = new System.Drawing.Point(289, 186);
            this.txt_V.Name = "txt_V";
            this.txt_V.Size = new System.Drawing.Size(92, 22);
            this.txt_V.TabIndex = 6;
            this.txt_V.Text = "3.6";
            this.txt_V.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(7, 190);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(188, 13);
            this.label144.TabIndex = 29;
            this.label144.Text = "Mean water current Velocity [V]";
            // 
            // txt_F
            // 
            this.txt_F.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_F.Location = new System.Drawing.Point(289, 158);
            this.txt_F.Name = "txt_F";
            this.txt_F.Size = new System.Drawing.Size(92, 22);
            this.txt_F.TabIndex = 5;
            this.txt_F.Text = "2.4";
            this.txt_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(7, 162);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(90, 13);
            this.label145.TabIndex = 27;
            this.label145.Text = "Wind Force [F]";
            // 
            // txt_A
            // 
            this.txt_A.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_A.Location = new System.Drawing.Point(289, 130);
            this.txt_A.Name = "txt_A";
            this.txt_A.Size = new System.Drawing.Size(92, 22);
            this.txt_A.TabIndex = 4;
            this.txt_A.Text = "72";
            this.txt_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(7, 134);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(250, 13);
            this.label146.TabIndex = 25;
            this.label146.Text = "Area of Deck and Handrail in elevation [A]";
            // 
            // txt_f2
            // 
            this.txt_f2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_f2.Location = new System.Drawing.Point(289, 410);
            this.txt_f2.Name = "txt_f2";
            this.txt_f2.Size = new System.Drawing.Size(92, 22);
            this.txt_f2.TabIndex = 15;
            this.txt_f2.Text = "0.225";
            this.txt_f2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(7, 414);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(275, 13);
            this.label147.TabIndex = 23;
            this.label147.Text = "Frictional Coefficient of Right Side Bending [f2]";
            // 
            // txt_f1
            // 
            this.txt_f1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_f1.Location = new System.Drawing.Point(289, 382);
            this.txt_f1.Name = "txt_f1";
            this.txt_f1.Size = new System.Drawing.Size(92, 22);
            this.txt_f1.TabIndex = 13;
            this.txt_f1.Text = "0.25";
            this.txt_f1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(7, 386);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(267, 13);
            this.label148.TabIndex = 21;
            this.label148.Text = "Frictional Coefficient of Left Side Bending [f1]";
            // 
            // txt_gamma_c
            // 
            this.txt_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_c.Location = new System.Drawing.Point(289, 326);
            this.txt_gamma_c.Name = "txt_gamma_c";
            this.txt_gamma_c.Size = new System.Drawing.Size(92, 22);
            this.txt_gamma_c.TabIndex = 11;
            this.txt_gamma_c.Text = "25";
            this.txt_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(7, 331);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(176, 13);
            this.label149.TabIndex = 19;
            this.label149.Text = "Unit weight of concrete  [γ_c]";
            // 
            // txt_HFL
            // 
            this.txt_HFL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_HFL.Location = new System.Drawing.Point(289, 354);
            this.txt_HFL.Name = "txt_HFL";
            this.txt_HFL.Size = new System.Drawing.Size(92, 22);
            this.txt_HFL.TabIndex = 12;
            this.txt_HFL.Text = "8.1";
            this.txt_HFL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(7, 358);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(186, 13);
            this.label150.TabIndex = 17;
            this.label150.Text = "Height of high flood Level [HFL]";
            // 
            // txt_h
            // 
            this.txt_h.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_h.Location = new System.Drawing.Point(289, 102);
            this.txt_h.Name = "txt_h";
            this.txt_h.Size = new System.Drawing.Size(92, 22);
            this.txt_h.TabIndex = 3;
            this.txt_h.Text = "9.0";
            this.txt_h.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(7, 106);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(105, 13);
            this.label151.TabIndex = 15;
            this.label151.Text = "Height of Pier [h]";
            // 
            // txt_l
            // 
            this.txt_l.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_l.Location = new System.Drawing.Point(289, 74);
            this.txt_l.Name = "txt_l";
            this.txt_l.Size = new System.Drawing.Size(92, 22);
            this.txt_l.TabIndex = 2;
            this.txt_l.Text = "8.2";
            this.txt_l.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(7, 78);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(103, 13);
            this.label152.TabIndex = 13;
            this.label152.Text = "Length of Pier [l]";
            // 
            // txt_b2
            // 
            this.txt_b2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b2.Location = new System.Drawing.Point(289, 46);
            this.txt_b2.Name = "txt_b2";
            this.txt_b2.Size = new System.Drawing.Size(92, 22);
            this.txt_b2.TabIndex = 1;
            this.txt_b2.Text = "1.8";
            this.txt_b2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(7, 50);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(147, 13);
            this.label153.TabIndex = 11;
            this.label153.Text = "Width of Pier at Top [b2]";
            // 
            // txt_b1
            // 
            this.txt_b1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b1.Location = new System.Drawing.Point(289, 18);
            this.txt_b1.Name = "txt_b1";
            this.txt_b1.Size = new System.Drawing.Size(92, 22);
            this.txt_b1.TabIndex = 0;
            this.txt_b1.Text = "2.7";
            this.txt_b1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Location = new System.Drawing.Point(7, 22);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(168, 13);
            this.label154.TabIndex = 9;
            this.label154.Text = "Width of Pier at Bottom [b1]";
            // 
            // txt_w3
            // 
            this.txt_w3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_w3.Location = new System.Drawing.Point(289, 298);
            this.txt_w3.Name = "txt_w3";
            this.txt_w3.Size = new System.Drawing.Size(92, 22);
            this.txt_w3.TabIndex = 10;
            this.txt_w3.Text = "700";
            this.txt_w3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(7, 302);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(190, 13);
            this.label155.TabIndex = 7;
            this.label155.Text = "Vehicle Load on each Span [w3]";
            // 
            // txt_e
            // 
            this.txt_e.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_e.Location = new System.Drawing.Point(289, 214);
            this.txt_e.Name = "txt_e";
            this.txt_e.Size = new System.Drawing.Size(92, 22);
            this.txt_e.TabIndex = 7;
            this.txt_e.Text = "0.45";
            this.txt_e.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(7, 218);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(245, 13);
            this.label156.TabIndex = 5;
            this.label156.Text = "Acts at distance from Centre Live Pier [e]";
            // 
            // txt_w2
            // 
            this.txt_w2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_w2.Location = new System.Drawing.Point(289, 270);
            this.txt_w2.Name = "txt_w2";
            this.txt_w2.Size = new System.Drawing.Size(92, 22);
            this.txt_w2.TabIndex = 9;
            this.txt_w2.Text = "900";
            this.txt_w2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(7, 274);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(186, 13);
            this.label157.TabIndex = 3;
            this.label157.Text = "Live Load from each Span [w2]";
            // 
            // txt_w1
            // 
            this.txt_w1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_w1.Location = new System.Drawing.Point(289, 242);
            this.txt_w1.Name = "txt_w1";
            this.txt_w1.Size = new System.Drawing.Size(92, 22);
            this.txt_w1.TabIndex = 8;
            this.txt_w1.Text = "2250";
            this.txt_w1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(7, 243);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(225, 13);
            this.label158.TabIndex = 1;
            this.label158.Text = "Permanent Load from each Span [w1]";
            // 
            // pic_stone_masonry
            // 
            this.pic_stone_masonry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_stone_masonry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_stone_masonry.Location = new System.Drawing.Point(477, 19);
            this.pic_stone_masonry.Name = "pic_stone_masonry";
            this.pic_stone_masonry.Size = new System.Drawing.Size(399, 414);
            this.pic_stone_masonry.TabIndex = 0;
            this.pic_stone_masonry.TabStop = false;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(249, 536);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(189, 49);
            this.btnProcess.TabIndex = 22;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btn_Stone_Masonry_Process_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(488, 536);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(189, 49);
            this.btnReport.TabIndex = 23;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btn_Stone_Masonry_Report_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox14);
            this.tabPage4.Controls.Add(this.groupBox13);
            this.tabPage4.Controls.Add(this.groupBox11);
            this.tabPage4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(954, 674);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Worksheet Design";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btn_ws_open_cir_well);
            this.groupBox14.Controls.Add(this.btn_ws_new_cir_well);
            this.groupBox14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(488, 320);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(403, 224);
            this.groupBox14.TabIndex = 5;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "RCC Circular Pier with Well Foundation Design";
            // 
            // btn_ws_open_cir_well
            // 
            this.btn_ws_open_cir_well.Location = new System.Drawing.Point(12, 129);
            this.btn_ws_open_cir_well.Name = "btn_ws_open_cir_well";
            this.btn_ws_open_cir_well.Size = new System.Drawing.Size(370, 54);
            this.btn_ws_open_cir_well.TabIndex = 4;
            this.btn_ws_open_cir_well.Text = "Open Worksheet Design";
            this.btn_ws_open_cir_well.UseVisualStyleBackColor = true;
            this.btn_ws_open_cir_well.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_ws_new_cir_well
            // 
            this.btn_ws_new_cir_well.Location = new System.Drawing.Point(12, 34);
            this.btn_ws_new_cir_well.Name = "btn_ws_new_cir_well";
            this.btn_ws_new_cir_well.Size = new System.Drawing.Size(370, 66);
            this.btn_ws_new_cir_well.TabIndex = 0;
            this.btn_ws_new_cir_well.Text = "New Worksheet Design ";
            this.btn_ws_new_cir_well.UseVisualStyleBackColor = true;
            this.btn_ws_new_cir_well.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btn_ws_open_cir);
            this.groupBox13.Controls.Add(this.btn_ws_new_cir);
            this.groupBox13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.Location = new System.Drawing.Point(488, 131);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(403, 172);
            this.groupBox13.TabIndex = 5;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "RCC Circular Pier Design";
            // 
            // btn_ws_open_cir
            // 
            this.btn_ws_open_cir.Location = new System.Drawing.Point(12, 106);
            this.btn_ws_open_cir.Name = "btn_ws_open_cir";
            this.btn_ws_open_cir.Size = new System.Drawing.Size(370, 54);
            this.btn_ws_open_cir.TabIndex = 4;
            this.btn_ws_open_cir.Text = "Open Worksheet Design";
            this.btn_ws_open_cir.UseVisualStyleBackColor = true;
            this.btn_ws_open_cir.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_ws_new_cir
            // 
            this.btn_ws_new_cir.Location = new System.Drawing.Point(12, 34);
            this.btn_ws_new_cir.Name = "btn_ws_new_cir";
            this.btn_ws_new_cir.Size = new System.Drawing.Size(370, 66);
            this.btn_ws_new_cir.TabIndex = 0;
            this.btn_ws_new_cir.Text = "New Worksheet Design ";
            this.btn_ws_new_cir.UseVisualStyleBackColor = true;
            this.btn_ws_new_cir.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btn_worksheet_pier_design_with_piles);
            this.groupBox11.Controls.Add(this.btn_worksheet_open);
            this.groupBox11.Controls.Add(this.btn_worksheet_pile_capacity);
            this.groupBox11.Controls.Add(this.btn_worksheet_Pier_cap);
            this.groupBox11.Controls.Add(this.btn_worksheet_1);
            this.groupBox11.Location = new System.Drawing.Point(64, 131);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(403, 413);
            this.groupBox11.TabIndex = 5;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "RCC Pier Design";
            // 
            // btn_worksheet_pier_design_with_piles
            // 
            this.btn_worksheet_pier_design_with_piles.Location = new System.Drawing.Point(12, 262);
            this.btn_worksheet_pier_design_with_piles.Name = "btn_worksheet_pier_design_with_piles";
            this.btn_worksheet_pier_design_with_piles.Size = new System.Drawing.Size(370, 48);
            this.btn_worksheet_pier_design_with_piles.TabIndex = 3;
            this.btn_worksheet_pier_design_with_piles.Text = "RCC Pier Worksheet Design 2 - RCC Pier Design with Piles";
            this.btn_worksheet_pier_design_with_piles.UseVisualStyleBackColor = true;
            this.btn_worksheet_pier_design_with_piles.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_worksheet_open
            // 
            this.btn_worksheet_open.Location = new System.Drawing.Point(12, 340);
            this.btn_worksheet_open.Name = "btn_worksheet_open";
            this.btn_worksheet_open.Size = new System.Drawing.Size(370, 54);
            this.btn_worksheet_open.TabIndex = 4;
            this.btn_worksheet_open.Text = "Open User\'s saved Worksheet Design";
            this.btn_worksheet_open.UseVisualStyleBackColor = true;
            this.btn_worksheet_open.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_worksheet_pile_capacity
            // 
            this.btn_worksheet_pile_capacity.Location = new System.Drawing.Point(12, 208);
            this.btn_worksheet_pile_capacity.Name = "btn_worksheet_pile_capacity";
            this.btn_worksheet_pile_capacity.Size = new System.Drawing.Size(370, 48);
            this.btn_worksheet_pile_capacity.TabIndex = 2;
            this.btn_worksheet_pile_capacity.Text = "RCC Pier Worksheet Design 2 - RCC Pier Pile Capacity";
            this.btn_worksheet_pile_capacity.UseVisualStyleBackColor = true;
            this.btn_worksheet_pile_capacity.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_worksheet_Pier_cap
            // 
            this.btn_worksheet_Pier_cap.Location = new System.Drawing.Point(12, 154);
            this.btn_worksheet_Pier_cap.Name = "btn_worksheet_Pier_cap";
            this.btn_worksheet_Pier_cap.Size = new System.Drawing.Size(370, 48);
            this.btn_worksheet_Pier_cap.TabIndex = 1;
            this.btn_worksheet_Pier_cap.Text = "RCC Pier Worksheet Design 2 - RCC Pier Cap";
            this.btn_worksheet_Pier_cap.UseVisualStyleBackColor = true;
            this.btn_worksheet_Pier_cap.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_worksheet_1
            // 
            this.btn_worksheet_1.Location = new System.Drawing.Point(12, 34);
            this.btn_worksheet_1.Name = "btn_worksheet_1";
            this.btn_worksheet_1.Size = new System.Drawing.Size(370, 66);
            this.btn_worksheet_1.TabIndex = 0;
            this.btn_worksheet_1.Text = "RCC Pier Worksheet Design 1";
            this.btn_worksheet_1.UseVisualStyleBackColor = true;
            this.btn_worksheet_1.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label223);
            this.tabPage5.Controls.Add(this.label210);
            this.tabPage5.Controls.Add(this.btn_dwg_rcc_pier);
            this.tabPage5.Controls.Add(this.btn_dwg_pier_1);
            this.tabPage5.Controls.Add(this.btn_dwg_pier_2);
            this.tabPage5.Controls.Add(this.btn_dwg_stone_interactive);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(954, 674);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Drawings";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Click += new System.EventHandler(this.btn_dwg_pier_1_Click);
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label223.Location = new System.Drawing.Point(264, 197);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(427, 16);
            this.label223.TabIndex = 81;
            this.label223.Text = "Button is Enabled Once the Stone Masonry Pier Design is done.";
            // 
            // label210
            // 
            this.label210.AutoSize = true;
            this.label210.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label210.Location = new System.Drawing.Point(323, 24);
            this.label210.Name = "label210";
            this.label210.Size = new System.Drawing.Size(308, 23);
            this.label210.TabIndex = 4;
            this.label210.Text = "Editable Construction Drawings";
            // 
            // btn_dwg_rcc_pier
            // 
            this.btn_dwg_rcc_pier.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_rcc_pier.Location = new System.Drawing.Point(316, 93);
            this.btn_dwg_rcc_pier.Name = "btn_dwg_rcc_pier";
            this.btn_dwg_rcc_pier.Size = new System.Drawing.Size(323, 46);
            this.btn_dwg_rcc_pier.TabIndex = 3;
            this.btn_dwg_rcc_pier.Text = "RCC Pier Drawings";
            this.btn_dwg_rcc_pier.UseVisualStyleBackColor = true;
            this.btn_dwg_rcc_pier.Click += new System.EventHandler(this.btn_dwg_pier_Click);
            // 
            // btn_dwg_pier_1
            // 
            this.btn_dwg_pier_1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_pier_1.Location = new System.Drawing.Point(316, 316);
            this.btn_dwg_pier_1.Name = "btn_dwg_pier_1";
            this.btn_dwg_pier_1.Size = new System.Drawing.Size(323, 52);
            this.btn_dwg_pier_1.TabIndex = 2;
            this.btn_dwg_pier_1.Text = "RCC Pier Worksheet Design 1 Drawings";
            this.btn_dwg_pier_1.UseVisualStyleBackColor = true;
            this.btn_dwg_pier_1.Click += new System.EventHandler(this.btn_dwg_pier_2_Click);
            // 
            // btn_dwg_pier_2
            // 
            this.btn_dwg_pier_2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_pier_2.Location = new System.Drawing.Point(316, 385);
            this.btn_dwg_pier_2.Name = "btn_dwg_pier_2";
            this.btn_dwg_pier_2.Size = new System.Drawing.Size(323, 51);
            this.btn_dwg_pier_2.TabIndex = 0;
            this.btn_dwg_pier_2.Text = "RCC Pier Worksheet Design 2 Drawings";
            this.btn_dwg_pier_2.UseVisualStyleBackColor = true;
            this.btn_dwg_pier_2.Click += new System.EventHandler(this.btn_dwg_pier_1_Click);
            // 
            // btn_dwg_stone_interactive
            // 
            this.btn_dwg_stone_interactive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_stone_interactive.Location = new System.Drawing.Point(316, 216);
            this.btn_dwg_stone_interactive.Name = "btn_dwg_stone_interactive";
            this.btn_dwg_stone_interactive.Size = new System.Drawing.Size(323, 46);
            this.btn_dwg_stone_interactive.TabIndex = 1;
            this.btn_dwg_stone_interactive.Text = "Stone Masonry Pier Drawing";
            this.btn_dwg_stone_interactive.UseVisualStyleBackColor = true;
            this.btn_dwg_stone_interactive.Click += new System.EventHandler(this.btn_dwg_stone_interactive_Click);
            // 
            // label233
            // 
            this.label233.AutoSize = true;
            this.label233.Location = new System.Drawing.Point(459, 355);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(263, 13);
            this.label233.TabIndex = 34;
            this.label233.Text = "Spacing between Vertical Stirrup Bars [vspc]";
            // 
            // txt_pier_2_vspc
            // 
            this.txt_pier_2_vspc.Location = new System.Drawing.Point(777, 352);
            this.txt_pier_2_vspc.Name = "txt_pier_2_vspc";
            this.txt_pier_2_vspc.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_vspc.TabIndex = 35;
            this.txt_pier_2_vspc.Text = "200";
            this.txt_pier_2_vspc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label234.Location = new System.Drawing.Point(857, 355);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(31, 13);
            this.label234.TabIndex = 39;
            this.label234.Text = "mm";
            // 
            // frm_Pier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 700);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Pier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pier Analysis";
            this.Load += new System.EventHandler(this.frm_Pier_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tab_dl.ResumeLayout(false);
            this.tab_dl.PerformLayout();
            this.groupBox38.ResumeLayout(false);
            this.groupBox38.PerformLayout();
            this.grb_LL.ResumeLayout(false);
            this.grb_LL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_live_load)).EndInit();
            this.grb_SIDL.ResumeLayout(false);
            this.grb_SIDL.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.grb_ana_wc.ResumeLayout(false);
            this.grb_ana_wc.PerformLayout();
            this.grb_ana_parapet.ResumeLayout(false);
            this.grb_ana_parapet.PerformLayout();
            this.grb_ana_sw_fp.ResumeLayout(false);
            this.grb_ana_sw_fp.PerformLayout();
            this.groupBox37.ResumeLayout(false);
            this.grb_create_input_data.ResumeLayout(false);
            this.grb_create_input_data.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.grb_select_analysis.ResumeLayout(false);
            this.grb_select_analysis.PerformLayout();
            this.tab_result.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.tabControl4.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBox43.ResumeLayout(false);
            this.groupBox49.ResumeLayout(false);
            this.groupBox49.PerformLayout();
            this.groupBox45.ResumeLayout(false);
            this.groupBox45.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_des_frc)).EndInit();
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_des_frc)).EndInit();
            this.groupBox48.ResumeLayout(false);
            this.groupBox48.PerformLayout();
            this.groupBox44.ResumeLayout(false);
            this.groupBox44.PerformLayout();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).EndInit();
            this.groupBox47.ResumeLayout(false);
            this.groupBox47.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tab_des_form1.ResumeLayout(false);
            this.tab_des_form1.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tab_des_form2.ResumeLayout(false);
            this.tab_des_form2.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tab_des_Diagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_pier_interactive_diagram)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_stone_masonry)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btn_worksheet_Pier_cap;
        private System.Windows.Forms.Button btn_worksheet_1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_RCC_Pier_Process;
        private System.Windows.Forms.Button btn_RCC_Pier_Report;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tab_des_form1;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H7;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.TextBox txt_RCC_Pier_gama_c;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox txt_RCC_Pier_vehi_load;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NR;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NP;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_RCC_Pier_fck_2;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox txt_RCC_Pier_fy2;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txt_RCC_Pier_D;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txt_RCC_Pier_b;
        private System.Windows.Forms.TextBox txt_RCC_Pier_d_dash;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txt_RCC_Pier_p1;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox txt_RCC_Pier_p2;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.TextBox txt_RCC_Pier_W1_supp_reac;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Mz1;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Mx1;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H2;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B4;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B3;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H1;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B2;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B1;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txt_RCC_Pier_overall_height;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B14;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B13;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B10;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B9;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H6;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H5;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Form_Lev;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL2;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL1;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B6;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B5;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txt_RCC_Pier_d2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txt_RCC_Pier_d1;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NB;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txt_RCC_Pier_a1;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txt_RCC_Pier_w3;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txt_RCC_Pier_w2;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txt_RCC_Pier_w1;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txt_RCC_Pier_L1;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TabPage tab_des_Diagram;
        private System.Windows.Forms.PictureBox pic_pier_interactive_diagram;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.TextBox txt_V;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.TextBox txt_F;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.TextBox txt_A;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.TextBox txt_f2;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.TextBox txt_f1;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.TextBox txt_gamma_c;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.TextBox txt_HFL;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.TextBox txt_h;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.TextBox txt_l;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.TextBox txt_b2;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.TextBox txt_b1;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.TextBox txt_w3;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.TextBox txt_e;
        private System.Windows.Forms.Label label156;
        private System.Windows.Forms.TextBox txt_w2;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.TextBox txt_w1;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.PictureBox pic_stone_masonry;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btn_worksheet_pier_design_with_piles;
        private System.Windows.Forms.Button btn_worksheet_pile_capacity;
        private System.Windows.Forms.Button btn_dwg_stone_interactive;
        private System.Windows.Forms.Button btn_dwg_pier_2;
        private System.Windows.Forms.Button btn_dwg_pier_1;
        private System.Windows.Forms.Button btn_dwg_rcc_pier;
        private System.Windows.Forms.Button btn_worksheet_open;
        private System.Windows.Forms.TabPage tab_des_form2;
        private System.Windows.Forms.Label label191;
        private System.Windows.Forms.Label label190;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.Label label187;
        private System.Windows.Forms.Label label197;
        private System.Windows.Forms.Label label196;
        private System.Windows.Forms.Label label195;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label185;
        private System.Windows.Forms.ComboBox cmb_pier_2_k;
        private System.Windows.Forms.TextBox txt_pier_2_SBC;
        private System.Windows.Forms.Label label184;
        private System.Windows.Forms.TextBox txt_pier_2_ldia;
        private System.Windows.Forms.Label label183;
        private System.Windows.Forms.TextBox txt_pier_2_slegs;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.TextBox txt_pier_2_sdia;
        private System.Windows.Forms.Label label181;
        private System.Windows.Forms.TextBox txt_pier_2_Itc;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.TextBox txt_pier_2_Vr;
        private System.Windows.Forms.Label label179;
        private System.Windows.Forms.TextBox txt_pier_2_LL;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.TextBox txt_pier_2_CF;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.TextBox txt_pier_2_k;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.TextBox txt_pier_2_V;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.TextBox txt_pier_2_HHF;
        private System.Windows.Forms.Label label174;
        private System.Windows.Forms.TextBox txt_pier_2_SC;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.TextBox txt_pier_2_PD;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.TextBox txt_pier_2_PML;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.TextBox txt_pier_2_PL;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.TextBox txt_pier_2_APD;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.TextBox txt_pier_2_B16;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.TextBox txt_pier_2_P3;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.TextBox txt_pier_2_P2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.TextBox txt_rcc_pier_m;
        private System.Windows.Forms.Label label215;
        private System.Windows.Forms.ComboBox cmb_rcc_pier_fy;
        private System.Windows.Forms.Label label236;
        private System.Windows.Forms.ComboBox cmb_rcc_pier_fck;
        private System.Windows.Forms.Label label244;
        private System.Windows.Forms.Label label250;
        private System.Windows.Forms.Label label251;
        private System.Windows.Forms.TextBox txt_rcc_pier_sigma_st;
        private System.Windows.Forms.Label label253;
        private System.Windows.Forms.Label label254;
        private System.Windows.Forms.TextBox txt_rcc_pier_sigma_c;
        private System.Windows.Forms.Label label258;
        private System.Windows.Forms.Label label259;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tab_dl;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.CheckBox chk_ana_active_LL;
        private System.Windows.Forms.GroupBox grb_LL;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Button btn_live_load_remove_all;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_LL_load_gen;
        private System.Windows.Forms.Button btn_add_load;
        private System.Windows.Forms.Button btn_live_load_remove;
        private System.Windows.Forms.TextBox txt_Y;
        private System.Windows.Forms.DataGridView dgv_live_load;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txt_XINCR;
        private System.Windows.Forms.ComboBox cmb_load_type;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txt_Ana_X;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_Z;
        private System.Windows.Forms.GroupBox grb_SIDL;
        private System.Windows.Forms.TextBox txt_member_load;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_remove_all;
        private System.Windows.Forms.CheckBox chk_ana_active_SIDL;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.CheckBox chk_WC;
        private System.Windows.Forms.CheckBox chk_swf;
        private System.Windows.Forms.TextBox txt_Ana_swf;
        private System.Windows.Forms.CheckBox chk_sw_fp;
        private System.Windows.Forms.GroupBox grb_ana_wc;
        private System.Windows.Forms.Label label511;
        private System.Windows.Forms.TextBox txt_Ana_gamma_w;
        private System.Windows.Forms.Label label515;
        private System.Windows.Forms.Label label520;
        private System.Windows.Forms.TextBox txt_Ana_Dw;
        private System.Windows.Forms.Label label521;
        private System.Windows.Forms.CheckBox chk_parapet;
        private System.Windows.Forms.GroupBox grb_ana_parapet;
        private System.Windows.Forms.TextBox txt_Ana_Hp;
        private System.Windows.Forms.Label label514;
        private System.Windows.Forms.Label label510;
        private System.Windows.Forms.Label label522;
        private System.Windows.Forms.TextBox txt_Ana_Wp;
        private System.Windows.Forms.Label label523;
        private System.Windows.Forms.GroupBox grb_ana_sw_fp;
        private System.Windows.Forms.TextBox txt_Ana_Hps;
        private System.Windows.Forms.Label label531;
        private System.Windows.Forms.TextBox txt_Ana_Wps;
        private System.Windows.Forms.Label label529;
        private System.Windows.Forms.Label label530;
        private System.Windows.Forms.TextBox txt_Ana_Hs;
        private System.Windows.Forms.Label label528;
        private System.Windows.Forms.Label label524;
        private System.Windows.Forms.Label label525;
        private System.Windows.Forms.Label label526;
        private System.Windows.Forms.TextBox txt_Ana_Bs;
        private System.Windows.Forms.Label label527;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.GroupBox grb_create_input_data;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Ana_ang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_Ana_gamma_c;
        private System.Windows.Forms.TextBox txt_Ana_Ds;
        private System.Windows.Forms.Label label503;
        private System.Windows.Forms.Label label501;
        private System.Windows.Forms.Label label499;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Ana_CR;
        private System.Windows.Forms.Label label502;
        private System.Windows.Forms.TextBox txt_Ana_CL;
        private System.Windows.Forms.Label label500;
        private System.Windows.Forms.TextBox txt_Ana_CW;
        private System.Windows.Forms.Label label498;
        private System.Windows.Forms.TextBox txt_Ana_B;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ana_L;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Label label512;
        private System.Windows.Forms.Label label513;
        private System.Windows.Forms.TextBox txt_Ana_BMG;
        private System.Windows.Forms.Label label516;
        private System.Windows.Forms.TextBox txt_Ana_DMG;
        private System.Windows.Forms.Label label517;
        private System.Windows.Forms.TextBox txt_Ana_NMG;
        private System.Windows.Forms.Label label519;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.Label label504;
        private System.Windows.Forms.Label label505;
        private System.Windows.Forms.TextBox txt_Ana_BCG;
        private System.Windows.Forms.Label label506;
        private System.Windows.Forms.TextBox txt_Ana_DCG;
        private System.Windows.Forms.Label label507;
        private System.Windows.Forms.TextBox txt_Ana_NCG;
        private System.Windows.Forms.Label label509;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox grb_select_analysis;
        private System.Windows.Forms.TextBox txt_analysis_file;
        private System.Windows.Forms.Button btn_ana_browse_input_file;
        private System.Windows.Forms.RadioButton rbtn_ana_create_analysis_file;
        private System.Windows.Forms.RadioButton rbtn_ana_select_analysis_file;
        private System.Windows.Forms.TabPage tab_result;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_View_Moving_Load;
        private System.Windows.Forms.Button btn_view_report;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btn_process_analysis;
        private System.Windows.Forms.Button btn_create_data;
        private System.Windows.Forms.Button btn_view_data;
        private System.Windows.Forms.Button btn_view_structure;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Button btn_update_force;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.CheckBox chk_R2;
        private System.Windows.Forms.CheckBox chk_R3;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.CheckBox chk_M2;
        private System.Windows.Forms.CheckBox chk_M3;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.Label label534;
        private System.Windows.Forms.TextBox txt_ana_TSRP;
        private System.Windows.Forms.Label label535;
        private System.Windows.Forms.Label label536;
        private System.Windows.Forms.TextBox txt_ana_MSTD;
        private System.Windows.Forms.Label label537;
        private System.Windows.Forms.Label label538;
        private System.Windows.Forms.TextBox txt_ana_MSLD;
        private System.Windows.Forms.Label label539;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label541;
        private System.Windows.Forms.Label label540;
        private System.Windows.Forms.TextBox txt_ana_DLSR;
        private System.Windows.Forms.TextBox txt_ana_LLSR;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox txt_outer_long_L2_shear;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.TextBox txt_outer_long_deff_shear;
        private System.Windows.Forms.TextBox txt_outer_long_L2_moment;
        private System.Windows.Forms.TextBox txt_outer_long_deff_moment;
        private System.Windows.Forms.TextBox txt_outer_long_L4_shear;
        private System.Windows.Forms.TextBox txt_outer_long_L4_moment;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.TextBox txt_Ana_cross_max_shear;
        private System.Windows.Forms.TextBox txt_Ana_cross_max_moment;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.Label label198;
        private System.Windows.Forms.Label label199;
        private System.Windows.Forms.Label label200;
        private System.Windows.Forms.Label label201;
        private System.Windows.Forms.Label label202;
        private System.Windows.Forms.Label label203;
        private System.Windows.Forms.Label label204;
        private System.Windows.Forms.Label label205;
        private System.Windows.Forms.Label label206;
        private System.Windows.Forms.TextBox txt_Ana_inner_long_L4_shear;
        private System.Windows.Forms.TextBox txt_Ana_inner_long_L2_moment;
        private System.Windows.Forms.TextBox txt_inner_long_L2_shear;
        private System.Windows.Forms.TextBox txt_Ana_inner_long_L4_moment;
        private System.Windows.Forms.TextBox txt_Ana_inner_long_deff_shear;
        private System.Windows.Forms.TextBox txt_inner_long_deff_moment;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.GroupBox groupBox43;
        private System.Windows.Forms.GroupBox groupBox49;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txt_final_Mz;
        private System.Windows.Forms.Label label220;
        public System.Windows.Forms.TextBox txt_max_Mz_kN;
        private System.Windows.Forms.Label label261;
        private System.Windows.Forms.GroupBox groupBox45;
        private System.Windows.Forms.TextBox txt_left_total_Mz;
        private System.Windows.Forms.TextBox txt_left_total_Mx;
        private System.Windows.Forms.Label label325;
        private System.Windows.Forms.Label label326;
        private System.Windows.Forms.TextBox txt_left_total_vert_reac;
        private System.Windows.Forms.DataGridView dgv_left_des_frc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Max_Mx;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Max_Mz;
        private System.Windows.Forms.GroupBox g;
        private System.Windows.Forms.DataGridView dgv_right_des_frc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.TextBox txt_right_total_Mz;
        private System.Windows.Forms.TextBox txt_right_total_Mx;
        private System.Windows.Forms.Label label402;
        private System.Windows.Forms.Label label442;
        private System.Windows.Forms.TextBox txt_right_total_vert_reac;
        private System.Windows.Forms.GroupBox groupBox48;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txt_final_Mx;
        private System.Windows.Forms.Label label237;
        public System.Windows.Forms.TextBox txt_max_Mx_kN;
        private System.Windows.Forms.Label label262;
        private System.Windows.Forms.GroupBox groupBox44;
        public System.Windows.Forms.TextBox txt_final_vert_rec_kN;
        private System.Windows.Forms.Label label260;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txt_final_vert_reac;
        private System.Windows.Forms.Label label264;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Label label327;
        private System.Windows.Forms.TextBox txt_dead_kN_m;
        private System.Windows.Forms.Label label354;
        private System.Windows.Forms.TextBox txt_dead_vert_reac_kN;
        private System.Windows.Forms.Label label370;
        private System.Windows.Forms.Label label371;
        private System.Windows.Forms.DataGridView dgv_left_end_design_forces;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Joints;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Vert_Reaction;
        private System.Windows.Forms.TextBox txt_dead_vert_reac_ton;
        private System.Windows.Forms.GroupBox groupBox47;
        public System.Windows.Forms.TextBox txt_live_kN_m;
        private System.Windows.Forms.Label label388;
        public System.Windows.Forms.TextBox txt_live_vert_rec_kN;
        private System.Windows.Forms.Label label399;
        private System.Windows.Forms.Label label400;
        private System.Windows.Forms.DataGridView dgv_right_end_design_forces;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        public System.Windows.Forms.TextBox txt_live_vert_rec_Ton;
        private System.Windows.Forms.Label label401;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_load_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_if;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_XINC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_imf;
        private System.Windows.Forms.Label label207;
        private System.Windows.Forms.TextBox txt_Load_Impact;
        private System.Windows.Forms.Label label208;
        private System.Windows.Forms.Label label209;
        private System.Windows.Forms.Label label210;
        private System.Windows.Forms.Label label211;
        private System.Windows.Forms.Label label212;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.Label label214;
        private System.Windows.Forms.Label label216;
        private System.Windows.Forms.Label label217;
        private System.Windows.Forms.Label label218;
        private System.Windows.Forms.Label label219;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label222;
        private System.Windows.Forms.Label label223;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button btn_ws_open_cir_well;
        private System.Windows.Forms.Button btn_ws_new_cir_well;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btn_ws_open_cir;
        private System.Windows.Forms.Button btn_ws_new_cir;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btn_def_mov_load;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label225;
        private System.Windows.Forms.TextBox txt_RCC_Pier_tdia;
        private System.Windows.Forms.TextBox txt_RCC_Pier_rdia;
        private System.Windows.Forms.Label label226;
        private System.Windows.Forms.Label label224;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.TextBox txt_pier_2_vdia;
        private System.Windows.Forms.Label label230;
        private System.Windows.Forms.TextBox txt_pier_2_hdia;
        private System.Windows.Forms.Label label228;
        private System.Windows.Forms.Label label232;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.TextBox txt_pier_2_vspc;
        private System.Windows.Forms.Label label233;
    }
}