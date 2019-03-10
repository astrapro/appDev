namespace LimitStateMethod.Extradossed
{
    partial class frm_Extradosed_AASHTO
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
            try
            {
                iApp.Delete_Temporary_Files();
                iApp.Delete_Temporary_Files(System.IO.Path.GetDirectoryName(Deck_Analysis_DL.Input_File));
                iApp.Delete_Temporary_Files(System.IO.Path.GetDirectoryName(Deck_Analysis_LL.Input_File));
            }
            catch (System.Exception ex) { }
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Extradosed_AASHTO));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_outer_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_DL_outer_long_L2_moment = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_inner_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_DL_inner_long_deff_shear = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_Ana_dead_inner_long_support_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_support_moment = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txt_Ana_dead_inner_long_L8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_L8_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_3L_8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_3L_8_moment = new System.Windows.Forms.TextBox();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.txt_Ana_dead_inner_long_L4_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_L2_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_L4_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_inner_long_deff_moment = new System.Windows.Forms.TextBox();
            this.grb_Ana_Res_DL = new System.Windows.Forms.GroupBox();
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tab_Analysis_DL = new System.Windows.Forms.TabPage();
            this.tbc_girder = new System.Windows.Forms.TabControl();
            this.tab_user_input = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_psc_new_design = new System.Windows.Forms.Button();
            this.btn_psc_browse = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label283 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.pic_diagram = new System.Windows.Forms.PictureBox();
            this.pcb_cables = new System.Windows.Forms.PictureBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.grb_ana_sw_fp = new System.Windows.Forms.GroupBox();
            this.label531 = new System.Windows.Forms.Label();
            this.label529 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label524 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label527 = new System.Windows.Forms.Label();
            this.txt_Ana_wr = new System.Windows.Forms.TextBox();
            this.txt_Ana_Wk = new System.Windows.Forms.TextBox();
            this.label530 = new System.Windows.Forms.Label();
            this.txt_Ana_hf_RHS = new System.Windows.Forms.TextBox();
            this.txt_Ana_hf_LHS = new System.Windows.Forms.TextBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label528 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label525 = new System.Windows.Forms.Label();
            this.txt_Ana_wf_RHS = new System.Windows.Forms.TextBox();
            this.label526 = new System.Windows.Forms.Label();
            this.txt_Ana_wf_LHS = new System.Windows.Forms.TextBox();
            this.chk_footpath = new System.Windows.Forms.CheckBox();
            this.chk_fp_left = new System.Windows.Forms.CheckBox();
            this.chk_fp_right = new System.Windows.Forms.CheckBox();
            this.chk_cb_right = new System.Windows.Forms.CheckBox();
            this.chk_crash_barrier = new System.Windows.Forms.CheckBox();
            this.chk_cb_left = new System.Windows.Forms.CheckBox();
            this.grb_ana_crashBarrier = new System.Windows.Forms.GroupBox();
            this.label178 = new System.Windows.Forms.Label();
            this.label180 = new System.Windows.Forms.Label();
            this.label182 = new System.Windows.Forms.Label();
            this.label184 = new System.Windows.Forms.Label();
            this.txt_Ana_Hc_RHS = new System.Windows.Forms.TextBox();
            this.label186 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.txt_Ana_Wc_RHS = new System.Windows.Forms.TextBox();
            this.txt_Ana_Hc_LHS = new System.Windows.Forms.TextBox();
            this.label189 = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.txt_Ana_Wc_LHS = new System.Windows.Forms.TextBox();
            this.grb_ana_wc = new System.Windows.Forms.GroupBox();
            this.label511 = new System.Windows.Forms.Label();
            this.txt_Ana_Wfws = new System.Windows.Forms.TextBox();
            this.label515 = new System.Windows.Forms.Label();
            this.label520 = new System.Windows.Forms.Label();
            this.txt_Ana_tfws = new System.Windows.Forms.TextBox();
            this.label521 = new System.Windows.Forms.Label();
            this.grb_ana_parapet = new System.Windows.Forms.GroupBox();
            this.label514 = new System.Windows.Forms.Label();
            this.label523 = new System.Windows.Forms.Label();
            this.txt_Ana_Hpar = new System.Windows.Forms.TextBox();
            this.label510 = new System.Windows.Forms.Label();
            this.label522 = new System.Windows.Forms.Label();
            this.txt_Ana_wbase = new System.Windows.Forms.TextBox();
            this.txt_Ana_member_load = new System.Windows.Forms.TextBox();
            this.grb_create_input_data = new System.Windows.Forms.GroupBox();
            this.label273 = new System.Windows.Forms.Label();
            this.label271 = new System.Windows.Forms.Label();
            this.label269 = new System.Windows.Forms.Label();
            this.txt_support_distance = new System.Windows.Forms.TextBox();
            this.label272 = new System.Windows.Forms.Label();
            this.txt_overhang_gap = new System.Windows.Forms.TextBox();
            this.label270 = new System.Windows.Forms.Label();
            this.txt_exp_gap = new System.Windows.Forms.TextBox();
            this.label268 = new System.Windows.Forms.Label();
            this.rbtn_multiple_cell = new System.Windows.Forms.RadioButton();
            this.rbtn_single_cell = new System.Windows.Forms.RadioButton();
            this.label83 = new System.Windows.Forms.Label();
            this.txt_Ana_width_cantilever = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_eff_depth = new System.Windows.Forms.TextBox();
            this.txt_Ana_Bottom_Slab_Thickness = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Ana_Top_Slab_Thickness = new System.Windows.Forms.TextBox();
            this.txt_Ana_DL_factor = new System.Windows.Forms.TextBox();
            this.label240 = new System.Windows.Forms.Label();
            this.label285 = new System.Windows.Forms.Label();
            this.txt_Ana_LL_factor = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.txt_Ana_Web_Thickness = new System.Windows.Forms.TextBox();
            this.txt_Ana_Span = new System.Windows.Forms.TextBox();
            this.txt_Ana_B = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.txt_Ana_Road_Width = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txt_Ana_Superstructure_depth = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label286 = new System.Windows.Forms.Label();
            this.txt_Ana_Web_Spacing = new System.Windows.Forms.TextBox();
            this.txt_L3 = new System.Windows.Forms.TextBox();
            this.label288 = new System.Windows.Forms.Label();
            this.txt_L2 = new System.Windows.Forms.TextBox();
            this.label289 = new System.Windows.Forms.Label();
            this.label290 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ana_L1 = new System.Windows.Forms.TextBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.txt_Ana_Superstructure_fci = new System.Windows.Forms.TextBox();
            this.txt_Ana_column_fc = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txt_Ana_Concrete_DL_Calculation = new System.Windows.Forms.TextBox();
            this.label121 = new System.Windows.Forms.Label();
            this.txt_Ana_Concrete_Ec = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txt_Ana_Superstructure_fc = new System.Windows.Forms.TextBox();
            this.label118 = new System.Windows.Forms.Label();
            this.txt_box_weight = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.grb_SIDL = new System.Windows.Forms.GroupBox();
            this.txt_Ana_LL_member_load = new System.Windows.Forms.TextBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.label311 = new System.Windows.Forms.Label();
            this.cmb_cable_type = new System.Windows.Forms.ComboBox();
            this.txt_cbl_des_f = new System.Windows.Forms.TextBox();
            this.label312 = new System.Windows.Forms.Label();
            this.txt_cp = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label313 = new System.Windows.Forms.Label();
            this.label308 = new System.Windows.Forms.Label();
            this.txt_cbl_des_E = new System.Windows.Forms.TextBox();
            this.label314 = new System.Windows.Forms.Label();
            this.label287 = new System.Windows.Forms.Label();
            this.label504 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label309 = new System.Windows.Forms.Label();
            this.label298 = new System.Windows.Forms.Label();
            this.txt_tower_Dt = new System.Windows.Forms.TextBox();
            this.txt_cbl_des_gamma = new System.Windows.Forms.TextBox();
            this.label150 = new System.Windows.Forms.Label();
            this.label296 = new System.Windows.Forms.Label();
            this.label310 = new System.Windows.Forms.Label();
            this.label294 = new System.Windows.Forms.Label();
            this.txt_Tower_Height = new System.Windows.Forms.TextBox();
            this.txt_init_cable = new System.Windows.Forms.TextBox();
            this.label292 = new System.Windows.Forms.Label();
            this.txt_cable_no = new System.Windows.Forms.TextBox();
            this.txt_horizontal_cbl_dist = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label307 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label503 = new System.Windows.Forms.Label();
            this.label284 = new System.Windows.Forms.Label();
            this.label297 = new System.Windows.Forms.Label();
            this.txt_tower_Bt = new System.Windows.Forms.TextBox();
            this.txt_cable_dia = new System.Windows.Forms.TextBox();
            this.txt_vertical_cbl_min_dist = new System.Windows.Forms.TextBox();
            this.txt_vertical_cbl_dist = new System.Windows.Forms.TextBox();
            this.label295 = new System.Windows.Forms.Label();
            this.label293 = new System.Windows.Forms.Label();
            this.label291 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label136 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_Ana_FPLL = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.txt_Ana_SIDL = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label134 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.txt_Ana_SelfWeight = new System.Windows.Forms.TextBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.label94 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.txt_Ana_Strand_Ep = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.txt_Ana_Strand_Fpy = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.txt_Ana_Strand_Fpu = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.txt_Ana_Strand_Diameter = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.txt_Ana_Strand_Area = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label228 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Top_Cover = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Bottom_Cover = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Wearing_Surface = new System.Windows.Forms.TextBox();
            this.txt_Ana_Deckslab_Thickness = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Overhang = new System.Windows.Forms.TextBox();
            this.label85 = new System.Windows.Forms.Label();
            this.tab_cs_diagram1 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.txt_box_cs2_IZZ = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_box_cs2_IYY = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txt_box_cs2_IXX = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.txt_box_cs2_AX = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.txt_box_cs2_b8 = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b7 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b6 = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d5 = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b5 = new System.Windows.Forms.TextBox();
            this.txt_box_cs2_d4 = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b4 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d3 = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b3 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d2 = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b2 = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d1 = new System.Windows.Forms.TextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.txt_box_cs2_cell_nos = new System.Windows.Forms.TextBox();
            this.txt_box_cs2_b1 = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.tab_cs_diagram2 = new System.Windows.Forms.TabPage();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.dgv_seg_tab3_1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label177 = new System.Windows.Forms.Label();
            this.txt_tab3_L_8 = new System.Windows.Forms.TextBox();
            this.txt_tab3_L_2 = new System.Windows.Forms.TextBox();
            this.txt_tab3_3L_8 = new System.Windows.Forms.TextBox();
            this.txt_tab3_L_4 = new System.Windows.Forms.TextBox();
            this.txt_tab3_support = new System.Windows.Forms.TextBox();
            this.txt_tab3_d = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btn_Show_Section_Resulf = new System.Windows.Forms.Button();
            this.rtb_sections = new System.Windows.Forms.RichTextBox();
            this.label176 = new System.Windows.Forms.Label();
            this.label226 = new System.Windows.Forms.Label();
            this.tab_cs_results = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txt_out_IZZ = new System.Windows.Forms.TextBox();
            this.txt_inn_IZZ = new System.Windows.Forms.TextBox();
            this.txt_cen_IZZ = new System.Windows.Forms.TextBox();
            this.label444 = new System.Windows.Forms.Label();
            this.label354 = new System.Windows.Forms.Label();
            this.txt_tot_IZZ = new System.Windows.Forms.TextBox();
            this.label350 = new System.Windows.Forms.Label();
            this.txt_out_IYY = new System.Windows.Forms.TextBox();
            this.txt_inn_IYY = new System.Windows.Forms.TextBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label443 = new System.Windows.Forms.Label();
            this.txt_cen_IYY = new System.Windows.Forms.TextBox();
            this.label353 = new System.Windows.Forms.Label();
            this.txt_tot_IYY = new System.Windows.Forms.TextBox();
            this.txt_out_IXX = new System.Windows.Forms.TextBox();
            this.label349 = new System.Windows.Forms.Label();
            this.txt_inn_IXX = new System.Windows.Forms.TextBox();
            this.label279 = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.txt_cen_IXX = new System.Windows.Forms.TextBox();
            this.label352 = new System.Windows.Forms.Label();
            this.txt_tot_IXX = new System.Windows.Forms.TextBox();
            this.txt_out_pcnt = new System.Windows.Forms.TextBox();
            this.label348 = new System.Windows.Forms.Label();
            this.txt_inn_pcnt = new System.Windows.Forms.TextBox();
            this.txt_out_AX = new System.Windows.Forms.TextBox();
            this.label162 = new System.Windows.Forms.Label();
            this.txt_inn_AX = new System.Windows.Forms.TextBox();
            this.txt_cen_pcnt = new System.Windows.Forms.TextBox();
            this.label163 = new System.Windows.Forms.Label();
            this.txt_cen_AX = new System.Windows.Forms.TextBox();
            this.label339 = new System.Windows.Forms.Label();
            this.label165 = new System.Windows.Forms.Label();
            this.txt_tot_AX = new System.Windows.Forms.TextBox();
            this.label166 = new System.Windows.Forms.Label();
            this.label338 = new System.Windows.Forms.Label();
            this.label351 = new System.Windows.Forms.Label();
            this.label505 = new System.Windows.Forms.Label();
            this.label502 = new System.Windows.Forms.Label();
            this.label501 = new System.Windows.Forms.Label();
            this.label336 = new System.Windows.Forms.Label();
            this.label399 = new System.Windows.Forms.Label();
            this.label347 = new System.Windows.Forms.Label();
            this.label342 = new System.Windows.Forms.Label();
            this.label281 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label335 = new System.Windows.Forms.Label();
            this.label341 = new System.Windows.Forms.Label();
            this.label346 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.label340 = new System.Windows.Forms.Label();
            this.label345 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.label337 = new System.Windows.Forms.Label();
            this.label344 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label343 = new System.Windows.Forms.Label();
            this.label277 = new System.Windows.Forms.Label();
            this.label498 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.tab_moving_data = new System.Windows.Forms.TabPage();
            this.groupBox79 = new System.Windows.Forms.GroupBox();
            this.btn_irc_view_moving_load = new System.Windows.Forms.Button();
            this.label568 = new System.Windows.Forms.Label();
            this.cmb_irc_view_moving_load = new System.Windows.Forms.ComboBox();
            this.txt_irc_vehicle_gap = new System.Windows.Forms.TextBox();
            this.label569 = new System.Windows.Forms.Label();
            this.label299 = new System.Windows.Forms.Label();
            this.txt_dl_ll_comb = new System.Windows.Forms.TextBox();
            this.btn_edit_load_combs = new System.Windows.Forms.Button();
            this.chk_self_indian = new System.Windows.Forms.CheckBox();
            this.btn_long_restore_ll_IRC = new System.Windows.Forms.Button();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.label300 = new System.Windows.Forms.Label();
            this.dgv_long_loads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label301 = new System.Windows.Forms.Label();
            this.label302 = new System.Windows.Forms.Label();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.label303 = new System.Windows.Forms.Label();
            this.dgv_long_liveloads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label304 = new System.Windows.Forms.Label();
            this.txt_IRC_LL_load_gen = new System.Windows.Forms.TextBox();
            this.txt_IRC_XINCR = new System.Windows.Forms.TextBox();
            this.tab_analysis = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_selfweight = new System.Windows.Forms.CheckBox();
            this.groupBox50 = new System.Windows.Forms.GroupBox();
            this.label252 = new System.Windows.Forms.Label();
            this.label323 = new System.Windows.Forms.Label();
            this.label324 = new System.Windows.Forms.Label();
            this.label328 = new System.Windows.Forms.Label();
            this.label329 = new System.Windows.Forms.Label();
            this.txt_PR_cable = new System.Windows.Forms.TextBox();
            this.txt_den_cable = new System.Windows.Forms.TextBox();
            this.txt_emod_cable = new System.Windows.Forms.TextBox();
            this.groupBox51 = new System.Windows.Forms.GroupBox();
            this.label330 = new System.Windows.Forms.Label();
            this.label331 = new System.Windows.Forms.Label();
            this.label332 = new System.Windows.Forms.Label();
            this.label333 = new System.Windows.Forms.Label();
            this.label334 = new System.Windows.Forms.Label();
            this.txt_PR_conc = new System.Windows.Forms.TextBox();
            this.txt_den_conc = new System.Windows.Forms.TextBox();
            this.txt_emod_conc = new System.Windows.Forms.TextBox();
            this.groupBox52 = new System.Windows.Forms.GroupBox();
            this.label1194 = new System.Windows.Forms.Label();
            this.label1193 = new System.Windows.Forms.Label();
            this.label1196 = new System.Windows.Forms.Label();
            this.label1195 = new System.Windows.Forms.Label();
            this.label1192 = new System.Windows.Forms.Label();
            this.txt_PR_steel = new System.Windows.Forms.TextBox();
            this.txt_den_steel = new System.Windows.Forms.TextBox();
            this.txt_emod_steel = new System.Windows.Forms.TextBox();
            this.groupBox70 = new System.Windows.Forms.GroupBox();
            this.rbtn_esprt_pinned = new System.Windows.Forms.RadioButton();
            this.rbtn_esprt_fixed = new System.Windows.Forms.RadioButton();
            this.chk_esprt_fixed_MZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MX = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FX = new System.Windows.Forms.CheckBox();
            this.groupBox109 = new System.Windows.Forms.GroupBox();
            this.cmb_long_open_file = new System.Windows.Forms.ComboBox();
            this.btn_view_postProcess = new System.Windows.Forms.Button();
            this.btn_view_report = new System.Windows.Forms.Button();
            this.btn_view_data = new System.Windows.Forms.Button();
            this.btn_view_preProcess = new System.Windows.Forms.Button();
            this.btn_Process_LL_Analysis = new System.Windows.Forms.Button();
            this.btn_Ana_DL_create_data = new System.Windows.Forms.Button();
            this.groupBox71 = new System.Windows.Forms.GroupBox();
            this.rbtn_ssprt_pinned = new System.Windows.Forms.RadioButton();
            this.rbtn_ssprt_fixed = new System.Windows.Forms.RadioButton();
            this.chk_ssprt_fixed_MZ = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FZ = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_MY = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FY = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_MX = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FX = new System.Windows.Forms.CheckBox();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.btn_update_force = new System.Windows.Forms.Button();
            this.chk_M2 = new System.Windows.Forms.CheckBox();
            this.label142 = new System.Windows.Forms.Label();
            this.chk_R3 = new System.Windows.Forms.CheckBox();
            this.chk_M3 = new System.Windows.Forms.CheckBox();
            this.chk_R2 = new System.Windows.Forms.CheckBox();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.label238 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.txt_Ana_live_outer_long_support_shear = new System.Windows.Forms.TextBox();
            this.label161 = new System.Windows.Forms.Label();
            this.txt_Ana_live_outer_long_support_moment = new System.Windows.Forms.TextBox();
            this.label181 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label183 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label185 = new System.Windows.Forms.Label();
            this.txt_Ana_live_outer_long_L8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_L8_moment = new System.Windows.Forms.TextBox();
            this.label187 = new System.Windows.Forms.Label();
            this.txt_Ana_live_outer_long_3L_8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_L2_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_3L_8_moment = new System.Windows.Forms.TextBox();
            this.label190 = new System.Windows.Forms.Label();
            this.txt_Ana_live_outer_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_deff_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_L4_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_outer_long_L4_moment = new System.Windows.Forms.TextBox();
            this.txt_max_delf = new System.Windows.Forms.TextBox();
            this.label499 = new System.Windows.Forms.Label();
            this.lbl_max_delf = new System.Windows.Forms.Label();
            this.label500 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txt_Ana_live_inner_long_support_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_support_moment = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txt_Ana_live_inner_long_L8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_L8_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_3L_8_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_3L_8_moment = new System.Windows.Forms.TextBox();
            this.label154 = new System.Windows.Forms.Label();
            this.label322 = new System.Windows.Forms.Label();
            this.label156 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.label159 = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.label174 = new System.Windows.Forms.Label();
            this.label179 = new System.Windows.Forms.Label();
            this.txt_Ana_live_inner_long_L4_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_L2_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_L4_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_inner_long_deff_moment = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label191 = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.label193 = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            this.txt_Ana_live_cross_max_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_live_cross_max_moment = new System.Windows.Forms.TextBox();
            this.grb_Ana_Res_LL = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Ana_LL_inner_long_L2_moment = new System.Windows.Forms.TextBox();
            this.txt_Ana_LL_inner_long_deff_shear = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_Ana_LL_outer_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_LL_outer_long_L2_moment = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.uC_SR_DL = new LimitStateMethod.UC_SupportReactions_LRFD();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.uC_SR_LL = new LimitStateMethod.UC_SupportReactions_LRFD();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.uC_SR_Max = new LimitStateMethod.UC_MaxReactions_LRFD();
            this.tab_stage = new System.Windows.Forms.TabPage();
            this.tc_stage = new System.Windows.Forms.TabControl();
            this.tab_stage1 = new System.Windows.Forms.TabPage();
            this.uC_Stage_Extradosed_LRFD1 = new LimitStateMethod.Extradossed.UC_Stage_Extradosed_LRFD();
            this.tab_stage2 = new System.Windows.Forms.TabPage();
            this.uC_Stage_Extradosed_LRFD2 = new LimitStateMethod.Extradossed.UC_Stage_Extradosed_LRFD();
            this.tab_stage3 = new System.Windows.Forms.TabPage();
            this.uC_Stage_Extradosed_LRFD3 = new LimitStateMethod.Extradossed.UC_Stage_Extradosed_LRFD();
            this.tab_stage4 = new System.Windows.Forms.TabPage();
            this.uC_Stage_Extradosed_LRFD4 = new LimitStateMethod.Extradossed.UC_Stage_Extradosed_LRFD();
            this.tab_stage5 = new System.Windows.Forms.TabPage();
            this.uC_Stage_Extradosed_LRFD5 = new LimitStateMethod.Extradossed.UC_Stage_Extradosed_LRFD();
            this.tab_designSage = new System.Windows.Forms.TabPage();
            this.uC_Res = new LimitStateMethod.Extradossed.UC_Extradosed_Results_LRFD();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_result_summary = new System.Windows.Forms.Button();
            this.cmb_design_stage = new System.Windows.Forms.ComboBox();
            this.label249 = new System.Windows.Forms.Label();
            this.tab_worksheet_design = new System.Windows.Forms.TabPage();
            this.tc_bridge_deck = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_steel_girder_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_steel_section_ws_open = new System.Windows.Forms.Button();
            this.btn_process_steel_section = new System.Windows.Forms.Button();
            this.btn_steel_section_open = new System.Windows.Forms.Button();
            this.tab_rcc_abutment = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_abutment_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_abutment_ws_open = new System.Windows.Forms.Button();
            this.btn_process_abutment = new System.Windows.Forms.Button();
            this.btn_abutment_open = new System.Windows.Forms.Button();
            this.tab_pier = new System.Windows.Forms.TabPage();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.dgv_pier_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_pier_ws_open = new System.Windows.Forms.Button();
            this.btn_process_pier = new System.Windows.Forms.Button();
            this.btn_pier_open = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.dgv_bearing_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_bearing_ws_open = new System.Windows.Forms.Button();
            this.btn_process_bearing = new System.Windows.Forms.Button();
            this.btn_bearing_open = new System.Windows.Forms.Button();
            this.tab_drawings = new System.Windows.Forms.TabPage();
            this.btn_cable_stayed_drawing = new System.Windows.Forms.Button();
            this.btn_dwg_open_Pier = new System.Windows.Forms.Button();
            this.btn_dwg_open_Cantilever = new System.Windows.Forms.Button();
            this.btn_dwg_open_Counterfort = new System.Windows.Forms.Button();
            this.label157 = new System.Windows.Forms.Label();
            this.btn_dwg_pier = new System.Windows.Forms.Button();
            this.btn_construction_drawings = new System.Windows.Forms.Button();
            this.btn_open_drawings = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grb_Ana_Res_DL.SuspendLayout();
            this.tc_main.SuspendLayout();
            this.tab_Analysis_DL.SuspendLayout();
            this.tbc_girder.SuspendLayout();
            this.tab_user_input.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_cables)).BeginInit();
            this.groupBox24.SuspendLayout();
            this.grb_ana_sw_fp.SuspendLayout();
            this.grb_ana_crashBarrier.SuspendLayout();
            this.grb_ana_wc.SuspendLayout();
            this.grb_ana_parapet.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.grb_SIDL.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tab_cs_diagram1.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.tab_cs_diagram2.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage11.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tab_cs_results.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tab_moving_data.SuspendLayout();
            this.groupBox79.SuspendLayout();
            this.groupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).BeginInit();
            this.groupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).BeginInit();
            this.tab_analysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox50.SuspendLayout();
            this.groupBox51.SuspendLayout();
            this.groupBox52.SuspendLayout();
            this.groupBox70.SuspendLayout();
            this.groupBox109.SuspendLayout();
            this.groupBox71.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.grb_Ana_Res_LL.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tab_stage.SuspendLayout();
            this.tc_stage.SuspendLayout();
            this.tab_stage1.SuspendLayout();
            this.tab_stage2.SuspendLayout();
            this.tab_stage3.SuspendLayout();
            this.tab_stage4.SuspendLayout();
            this.tab_stage5.SuspendLayout();
            this.tab_designSage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tab_worksheet_design.SuspendLayout();
            this.tc_bridge_deck.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_steel_girder_input_data)).BeginInit();
            this.tab_rcc_abutment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_abutment_input_data)).BeginInit();
            this.tab_pier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pier_input_data)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bearing_input_data)).BeginInit();
            this.tab_drawings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label36);
            this.groupBox9.Controls.Add(this.label21);
            this.groupBox9.Controls.Add(this.label22);
            this.groupBox9.Controls.Add(this.label37);
            this.groupBox9.Controls.Add(this.txt_Ana_DL_outer_long_deff_shear);
            this.groupBox9.Controls.Add(this.txt_Ana_DL_outer_long_L2_moment);
            this.groupBox9.Location = new System.Drawing.Point(203, 16);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(182, 74);
            this.groupBox9.TabIndex = 82;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Outer Main Girder";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(138, 54);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(27, 13);
            this.label36.TabIndex = 30;
            this.label36.Text = "Ton";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 52);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 13);
            this.label21.TabIndex = 28;
            this.label21.Text = "Shear";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 27;
            this.label22.Text = "Moment";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(138, 23);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(43, 13);
            this.label37.TabIndex = 30;
            this.label37.Text = "Ton-m";
            // 
            // txt_Ana_DL_outer_long_deff_shear
            // 
            this.txt_Ana_DL_outer_long_deff_shear.Location = new System.Drawing.Point(68, 49);
            this.txt_Ana_DL_outer_long_deff_shear.Name = "txt_Ana_DL_outer_long_deff_shear";
            this.txt_Ana_DL_outer_long_deff_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_outer_long_deff_shear.TabIndex = 20;
            this.txt_Ana_DL_outer_long_deff_shear.Text = "0";
            this.txt_Ana_DL_outer_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_DL_outer_long_L2_moment
            // 
            this.txt_Ana_DL_outer_long_L2_moment.Location = new System.Drawing.Point(68, 20);
            this.txt_Ana_DL_outer_long_L2_moment.Name = "txt_Ana_DL_outer_long_L2_moment";
            this.txt_Ana_DL_outer_long_L2_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_outer_long_L2_moment.TabIndex = 25;
            this.txt_Ana_DL_outer_long_L2_moment.Text = "0";
            this.txt_Ana_DL_outer_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Shear";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(132, 48);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(27, 13);
            this.label35.TabIndex = 30;
            this.label35.Text = "Ton";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label35);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.txt_Ana_DL_inner_long_L2_moment);
            this.groupBox6.Controls.Add(this.txt_Ana_DL_inner_long_deff_shear);
            this.groupBox6.Location = new System.Drawing.Point(13, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(183, 74);
            this.groupBox6.TabIndex = 81;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Inner Main Girder";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(132, 22);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 30;
            this.label27.Text = "Ton-m";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Moment";
            // 
            // txt_Ana_DL_inner_long_L2_moment
            // 
            this.txt_Ana_DL_inner_long_L2_moment.Location = new System.Drawing.Point(62, 20);
            this.txt_Ana_DL_inner_long_L2_moment.Name = "txt_Ana_DL_inner_long_L2_moment";
            this.txt_Ana_DL_inner_long_L2_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_inner_long_L2_moment.TabIndex = 25;
            this.txt_Ana_DL_inner_long_L2_moment.Text = "0";
            this.txt_Ana_DL_inner_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_DL_inner_long_deff_shear
            // 
            this.txt_Ana_DL_inner_long_deff_shear.Location = new System.Drawing.Point(62, 45);
            this.txt_Ana_DL_inner_long_deff_shear.Name = "txt_Ana_DL_inner_long_deff_shear";
            this.txt_Ana_DL_inner_long_deff_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_inner_long_deff_shear.TabIndex = 20;
            this.txt_Ana_DL_inner_long_deff_shear.Text = "0";
            this.txt_Ana_DL_inner_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_support_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_support_moment);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L8_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L8_moment);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_3L_8_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_3L_8_moment);
            this.groupBox3.Controls.Add(this.label145);
            this.groupBox3.Controls.Add(this.label146);
            this.groupBox3.Controls.Add(this.label147);
            this.groupBox3.Controls.Add(this.label148);
            this.groupBox3.Controls.Add(this.label151);
            this.groupBox3.Controls.Add(this.label152);
            this.groupBox3.Controls.Add(this.label153);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L4_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L2_moment);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L2_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_L4_moment);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_deff_shear);
            this.groupBox3.Controls.Add(this.txt_Ana_dead_inner_long_deff_moment);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 216);
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Maximum Member Forces [ Load Case 1 ]";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(9, 55);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(52, 13);
            this.label29.TabIndex = 37;
            this.label29.Text = "Support";
            // 
            // txt_Ana_dead_inner_long_support_shear
            // 
            this.txt_Ana_dead_inner_long_support_shear.Location = new System.Drawing.Point(164, 48);
            this.txt_Ana_dead_inner_long_support_shear.Name = "txt_Ana_dead_inner_long_support_shear";
            this.txt_Ana_dead_inner_long_support_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_support_shear.TabIndex = 39;
            this.txt_Ana_dead_inner_long_support_shear.Text = "0";
            this.txt_Ana_dead_inner_long_support_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_support_moment
            // 
            this.txt_Ana_dead_inner_long_support_moment.Location = new System.Drawing.Point(65, 51);
            this.txt_Ana_dead_inner_long_support_moment.Name = "txt_Ana_dead_inner_long_support_moment";
            this.txt_Ana_dead_inner_long_support_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_support_moment.TabIndex = 38;
            this.txt_Ana_dead_inner_long_support_moment.Text = "0";
            this.txt_Ana_dead_inner_long_support_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 163);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(32, 13);
            this.label26.TabIndex = 31;
            this.label26.Text = "3L/8";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(9, 108);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(25, 13);
            this.label28.TabIndex = 34;
            this.label28.Text = "L/8";
            // 
            // txt_Ana_dead_inner_long_L8_shear
            // 
            this.txt_Ana_dead_inner_long_L8_shear.Location = new System.Drawing.Point(164, 102);
            this.txt_Ana_dead_inner_long_L8_shear.Name = "txt_Ana_dead_inner_long_L8_shear";
            this.txt_Ana_dead_inner_long_L8_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_L8_shear.TabIndex = 36;
            this.txt_Ana_dead_inner_long_L8_shear.Text = "0";
            this.txt_Ana_dead_inner_long_L8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_L8_moment
            // 
            this.txt_Ana_dead_inner_long_L8_moment.Location = new System.Drawing.Point(65, 105);
            this.txt_Ana_dead_inner_long_L8_moment.Name = "txt_Ana_dead_inner_long_L8_moment";
            this.txt_Ana_dead_inner_long_L8_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_L8_moment.TabIndex = 35;
            this.txt_Ana_dead_inner_long_L8_moment.Text = "0";
            this.txt_Ana_dead_inner_long_L8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_3L_8_shear
            // 
            this.txt_Ana_dead_inner_long_3L_8_shear.Location = new System.Drawing.Point(164, 156);
            this.txt_Ana_dead_inner_long_3L_8_shear.Name = "txt_Ana_dead_inner_long_3L_8_shear";
            this.txt_Ana_dead_inner_long_3L_8_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_3L_8_shear.TabIndex = 33;
            this.txt_Ana_dead_inner_long_3L_8_shear.Text = "0";
            this.txt_Ana_dead_inner_long_3L_8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_3L_8_moment
            // 
            this.txt_Ana_dead_inner_long_3L_8_moment.Location = new System.Drawing.Point(65, 159);
            this.txt_Ana_dead_inner_long_3L_8_moment.Name = "txt_Ana_dead_inner_long_3L_8_moment";
            this.txt_Ana_dead_inner_long_3L_8_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_3L_8_moment.TabIndex = 32;
            this.txt_Ana_dead_inner_long_3L_8_moment.Text = "0";
            this.txt_Ana_dead_inner_long_3L_8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.ForeColor = System.Drawing.Color.Blue;
            this.label145.Location = new System.Drawing.Point(169, 28);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(34, 13);
            this.label145.TabIndex = 30;
            this.label145.Text = "(kip)";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(166, 12);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(41, 13);
            this.label146.TabIndex = 28;
            this.label146.Text = "Shear";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(9, 82);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(31, 13);
            this.label147.TabIndex = 18;
            this.label147.Text = "Deff";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(9, 189);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(25, 13);
            this.label148.TabIndex = 24;
            this.label148.Text = "L/2";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.ForeColor = System.Drawing.Color.Blue;
            this.label151.Location = new System.Drawing.Point(70, 31);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(47, 13);
            this.label151.TabIndex = 30;
            this.label151.Text = "(kip-ft)";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(70, 15);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(52, 13);
            this.label152.TabIndex = 27;
            this.label152.Text = "Moment";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(9, 135);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(25, 13);
            this.label153.TabIndex = 21;
            this.label153.Text = "L/4";
            // 
            // txt_Ana_dead_inner_long_L4_shear
            // 
            this.txt_Ana_dead_inner_long_L4_shear.Location = new System.Drawing.Point(164, 129);
            this.txt_Ana_dead_inner_long_L4_shear.Name = "txt_Ana_dead_inner_long_L4_shear";
            this.txt_Ana_dead_inner_long_L4_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_L4_shear.TabIndex = 23;
            this.txt_Ana_dead_inner_long_L4_shear.Text = "0";
            this.txt_Ana_dead_inner_long_L4_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_L2_moment
            // 
            this.txt_Ana_dead_inner_long_L2_moment.Location = new System.Drawing.Point(65, 186);
            this.txt_Ana_dead_inner_long_L2_moment.Name = "txt_Ana_dead_inner_long_L2_moment";
            this.txt_Ana_dead_inner_long_L2_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_L2_moment.TabIndex = 25;
            this.txt_Ana_dead_inner_long_L2_moment.Text = "0";
            this.txt_Ana_dead_inner_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_L2_shear
            // 
            this.txt_Ana_dead_inner_long_L2_shear.Location = new System.Drawing.Point(164, 183);
            this.txt_Ana_dead_inner_long_L2_shear.Name = "txt_Ana_dead_inner_long_L2_shear";
            this.txt_Ana_dead_inner_long_L2_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_L2_shear.TabIndex = 26;
            this.txt_Ana_dead_inner_long_L2_shear.Text = "0";
            this.txt_Ana_dead_inner_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_L4_moment
            // 
            this.txt_Ana_dead_inner_long_L4_moment.Location = new System.Drawing.Point(65, 132);
            this.txt_Ana_dead_inner_long_L4_moment.Name = "txt_Ana_dead_inner_long_L4_moment";
            this.txt_Ana_dead_inner_long_L4_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_L4_moment.TabIndex = 22;
            this.txt_Ana_dead_inner_long_L4_moment.Text = "0";
            this.txt_Ana_dead_inner_long_L4_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_deff_shear
            // 
            this.txt_Ana_dead_inner_long_deff_shear.Location = new System.Drawing.Point(164, 75);
            this.txt_Ana_dead_inner_long_deff_shear.Name = "txt_Ana_dead_inner_long_deff_shear";
            this.txt_Ana_dead_inner_long_deff_shear.Size = new System.Drawing.Size(92, 21);
            this.txt_Ana_dead_inner_long_deff_shear.TabIndex = 20;
            this.txt_Ana_dead_inner_long_deff_shear.Text = "0";
            this.txt_Ana_dead_inner_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_inner_long_deff_moment
            // 
            this.txt_Ana_dead_inner_long_deff_moment.Location = new System.Drawing.Point(65, 78);
            this.txt_Ana_dead_inner_long_deff_moment.Name = "txt_Ana_dead_inner_long_deff_moment";
            this.txt_Ana_dead_inner_long_deff_moment.Size = new System.Drawing.Size(90, 21);
            this.txt_Ana_dead_inner_long_deff_moment.TabIndex = 19;
            this.txt_Ana_dead_inner_long_deff_moment.Text = "0";
            this.txt_Ana_dead_inner_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_Ana_Res_DL
            // 
            this.grb_Ana_Res_DL.Controls.Add(this.groupBox6);
            this.grb_Ana_Res_DL.Controls.Add(this.groupBox9);
            this.grb_Ana_Res_DL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Ana_Res_DL.Location = new System.Drawing.Point(8, 435);
            this.grb_Ana_Res_DL.Name = "grb_Ana_Res_DL";
            this.grb_Ana_Res_DL.Size = new System.Drawing.Size(390, 93);
            this.grb_Ana_Res_DL.TabIndex = 102;
            this.grb_Ana_Res_DL.TabStop = false;
            this.grb_Ana_Res_DL.Text = "Analysis Result for Dead Load";
            this.grb_Ana_Res_DL.Visible = false;
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tab_Analysis_DL);
            this.tc_main.Controls.Add(this.tab_worksheet_design);
            this.tc_main.Controls.Add(this.tab_rcc_abutment);
            this.tc_main.Controls.Add(this.tab_pier);
            this.tc_main.Controls.Add(this.tabPage1);
            this.tc_main.Controls.Add(this.tab_drawings);
            this.tc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_main.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc_main.Location = new System.Drawing.Point(0, 0);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(977, 692);
            this.tc_main.TabIndex = 2;
            // 
            // tab_Analysis_DL
            // 
            this.tab_Analysis_DL.Controls.Add(this.tbc_girder);
            this.tab_Analysis_DL.Location = new System.Drawing.Point(4, 22);
            this.tab_Analysis_DL.Name = "tab_Analysis_DL";
            this.tab_Analysis_DL.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Analysis_DL.Size = new System.Drawing.Size(969, 666);
            this.tab_Analysis_DL.TabIndex = 0;
            this.tab_Analysis_DL.Text = "Analysis of Bridge Super Structure";
            this.tab_Analysis_DL.UseVisualStyleBackColor = true;
            // 
            // tbc_girder
            // 
            this.tbc_girder.Controls.Add(this.tab_user_input);
            this.tbc_girder.Controls.Add(this.tab_cs_diagram1);
            this.tbc_girder.Controls.Add(this.tab_cs_diagram2);
            this.tbc_girder.Controls.Add(this.tab_cs_results);
            this.tbc_girder.Controls.Add(this.tab_moving_data);
            this.tbc_girder.Controls.Add(this.tab_analysis);
            this.tbc_girder.Controls.Add(this.tab_stage);
            this.tbc_girder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_girder.Location = new System.Drawing.Point(3, 3);
            this.tbc_girder.Name = "tbc_girder";
            this.tbc_girder.SelectedIndex = 0;
            this.tbc_girder.Size = new System.Drawing.Size(963, 660);
            this.tbc_girder.TabIndex = 107;
            // 
            // tab_user_input
            // 
            this.tab_user_input.AutoScroll = true;
            this.tab_user_input.Controls.Add(this.panel4);
            this.tab_user_input.Location = new System.Drawing.Point(4, 22);
            this.tab_user_input.Name = "tab_user_input";
            this.tab_user_input.Padding = new System.Windows.Forms.Padding(3);
            this.tab_user_input.Size = new System.Drawing.Size(955, 634);
            this.tab_user_input.TabIndex = 0;
            this.tab_user_input.Text = "User\'s Input Data";
            this.tab_user_input.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label227);
            this.panel4.Controls.Add(this.pic_diagram);
            this.panel4.Controls.Add(this.pcb_cables);
            this.panel4.Controls.Add(this.groupBox24);
            this.panel4.Controls.Add(this.grb_create_input_data);
            this.panel4.Controls.Add(this.groupBox23);
            this.panel4.Controls.Add(this.grb_SIDL);
            this.panel4.Controls.Add(this.groupBox27);
            this.panel4.Controls.Add(this.groupBox8);
            this.panel4.Controls.Add(this.groupBox22);
            this.panel4.Controls.Add(this.label228);
            this.panel4.Controls.Add(this.groupBox20);
            this.panel4.Location = new System.Drawing.Point(5, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(957, 1436);
            this.panel4.TabIndex = 188;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_psc_new_design);
            this.panel5.Controls.Add(this.btn_psc_browse);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label283);
            this.panel5.Location = new System.Drawing.Point(4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(376, 56);
            this.panel5.TabIndex = 179;
            // 
            // btn_psc_new_design
            // 
            this.btn_psc_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_psc_new_design.Name = "btn_psc_new_design";
            this.btn_psc_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_psc_new_design.TabIndex = 188;
            this.btn_psc_new_design.Text = "New Design";
            this.btn_psc_new_design.UseVisualStyleBackColor = true;
            this.btn_psc_new_design.Click += new System.EventHandler(this.btn_psc_new_design_Click);
            // 
            // btn_psc_browse
            // 
            this.btn_psc_browse.Location = new System.Drawing.Point(242, 4);
            this.btn_psc_browse.Name = "btn_psc_browse";
            this.btn_psc_browse.Size = new System.Drawing.Size(121, 24);
            this.btn_psc_browse.TabIndex = 189;
            this.btn_psc_browse.Text = "Open Design";
            this.btn_psc_browse.UseVisualStyleBackColor = true;
            this.btn_psc_browse.Click += new System.EventHandler(this.btn_psc_new_design_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(104, 30);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(258, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label283
            // 
            this.label283.AutoSize = true;
            this.label283.Location = new System.Drawing.Point(5, 34);
            this.label283.Name = "label283";
            this.label283.Size = new System.Drawing.Size(93, 13);
            this.label283.TabIndex = 187;
            this.label283.Text = "Project Name :";
            // 
            // label227
            // 
            this.label227.AutoSize = true;
            this.label227.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label227.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label227.ForeColor = System.Drawing.Color.Red;
            this.label227.Location = new System.Drawing.Point(610, 11);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(218, 18);
            this.label227.TabIndex = 126;
            this.label227.Text = "Default Sample Data are shown";
            // 
            // pic_diagram
            // 
            this.pic_diagram.BackgroundImage = global::LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_12_;
            this.pic_diagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_diagram.Location = new System.Drawing.Point(400, 624);
            this.pic_diagram.Name = "pic_diagram";
            this.pic_diagram.Size = new System.Drawing.Size(475, 298);
            this.pic_diagram.TabIndex = 186;
            this.pic_diagram.TabStop = false;
            // 
            // pcb_cables
            // 
            this.pcb_cables.BackgroundImage = global::LimitStateMethod.Properties.Resources.ExtradossedCentralTowers;
            this.pcb_cables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcb_cables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_cables.Location = new System.Drawing.Point(4, 62);
            this.pcb_cables.Name = "pcb_cables";
            this.pcb_cables.Size = new System.Drawing.Size(919, 240);
            this.pcb_cables.TabIndex = 132;
            this.pcb_cables.TabStop = false;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.grb_ana_sw_fp);
            this.groupBox24.Controls.Add(this.chk_footpath);
            this.groupBox24.Controls.Add(this.chk_fp_left);
            this.groupBox24.Controls.Add(this.chk_fp_right);
            this.groupBox24.Controls.Add(this.chk_cb_right);
            this.groupBox24.Controls.Add(this.chk_crash_barrier);
            this.groupBox24.Controls.Add(this.chk_cb_left);
            this.groupBox24.Controls.Add(this.grb_ana_crashBarrier);
            this.groupBox24.Controls.Add(this.grb_ana_wc);
            this.groupBox24.Controls.Add(this.grb_ana_parapet);
            this.groupBox24.Controls.Add(this.txt_Ana_member_load);
            this.groupBox24.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox24.ForeColor = System.Drawing.Color.Black;
            this.groupBox24.Location = new System.Drawing.Point(394, 310);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(487, 308);
            this.groupBox24.TabIndex = 187;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "SUPER IMPOSED DEAD LOAD [SIDL]";
            // 
            // grb_ana_sw_fp
            // 
            this.grb_ana_sw_fp.Controls.Add(this.label531);
            this.grb_ana_sw_fp.Controls.Add(this.label529);
            this.grb_ana_sw_fp.Controls.Add(this.label10);
            this.grb_ana_sw_fp.Controls.Add(this.label524);
            this.grb_ana_sw_fp.Controls.Add(this.label11);
            this.grb_ana_sw_fp.Controls.Add(this.label527);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_wr);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_Wk);
            this.grb_ana_sw_fp.Controls.Add(this.label530);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_hf_RHS);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_hf_LHS);
            this.grb_ana_sw_fp.Controls.Add(this.label141);
            this.grb_ana_sw_fp.Controls.Add(this.label528);
            this.grb_ana_sw_fp.Controls.Add(this.label143);
            this.grb_ana_sw_fp.Controls.Add(this.label525);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_wf_RHS);
            this.grb_ana_sw_fp.Controls.Add(this.label526);
            this.grb_ana_sw_fp.Controls.Add(this.txt_Ana_wf_LHS);
            this.grb_ana_sw_fp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_sw_fp.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_sw_fp.Location = new System.Drawing.Point(6, 184);
            this.grb_ana_sw_fp.Name = "grb_ana_sw_fp";
            this.grb_ana_sw_fp.Size = new System.Drawing.Size(475, 115);
            this.grb_ana_sw_fp.TabIndex = 1;
            this.grb_ana_sw_fp.TabStop = false;
            this.grb_ana_sw_fp.Text = "Footpath and Kerb";
            // 
            // label531
            // 
            this.label531.AutoSize = true;
            this.label531.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label531.Location = new System.Drawing.Point(8, 91);
            this.label531.Name = "label531";
            this.label531.Size = new System.Drawing.Size(160, 13);
            this.label531.TabIndex = 5;
            this.label531.Text = "Width of Outer Railing [wr]";
            // 
            // label529
            // 
            this.label529.AutoSize = true;
            this.label529.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label529.Location = new System.Drawing.Point(8, 67);
            this.label529.Name = "label529";
            this.label529.Size = new System.Drawing.Size(115, 13);
            this.label529.TabIndex = 6;
            this.label529.Text = "Width of Kerb [wk]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(282, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Height [RHS_hf]";
            // 
            // label524
            // 
            this.label524.AutoSize = true;
            this.label524.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label524.Location = new System.Drawing.Point(8, 43);
            this.label524.Name = "label524";
            this.label524.Size = new System.Drawing.Size(98, 13);
            this.label524.TabIndex = 7;
            this.label524.Text = "Height [LHS_hf]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(284, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Width [RHS_wf]";
            // 
            // label527
            // 
            this.label527.AutoSize = true;
            this.label527.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label527.Location = new System.Drawing.Point(8, 16);
            this.label527.Name = "label527";
            this.label527.Size = new System.Drawing.Size(96, 13);
            this.label527.TabIndex = 4;
            this.label527.Text = "Width [LHS_wf]";
            // 
            // txt_Ana_wr
            // 
            this.txt_Ana_wr.Location = new System.Drawing.Point(239, 88);
            this.txt_Ana_wr.Name = "txt_Ana_wr";
            this.txt_Ana_wr.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_wr.TabIndex = 1;
            this.txt_Ana_wr.Text = "0";
            this.txt_Ana_wr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_Wk
            // 
            this.txt_Ana_Wk.Location = new System.Drawing.Point(239, 64);
            this.txt_Ana_Wk.Name = "txt_Ana_Wk";
            this.txt_Ana_Wk.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wk.TabIndex = 1;
            this.txt_Ana_Wk.Text = "0";
            this.txt_Ana_Wk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label530
            // 
            this.label530.AutoSize = true;
            this.label530.Location = new System.Drawing.Point(295, 91);
            this.label530.Name = "label530";
            this.label530.Size = new System.Drawing.Size(15, 13);
            this.label530.TabIndex = 2;
            this.label530.Text = "ft";
            // 
            // txt_Ana_hf_RHS
            // 
            this.txt_Ana_hf_RHS.Location = new System.Drawing.Point(389, 40);
            this.txt_Ana_hf_RHS.Name = "txt_Ana_hf_RHS";
            this.txt_Ana_hf_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_hf_RHS.TabIndex = 1;
            this.txt_Ana_hf_RHS.Text = "3.5";
            this.txt_Ana_hf_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_hf_LHS
            // 
            this.txt_Ana_hf_LHS.Location = new System.Drawing.Point(151, 40);
            this.txt_Ana_hf_LHS.Name = "txt_Ana_hf_LHS";
            this.txt_Ana_hf_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_hf_LHS.TabIndex = 1;
            this.txt_Ana_hf_LHS.Text = "3.5";
            this.txt_Ana_hf_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(445, 43);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(15, 13);
            this.label141.TabIndex = 2;
            this.label141.Text = "ft";
            // 
            // label528
            // 
            this.label528.AutoSize = true;
            this.label528.Location = new System.Drawing.Point(295, 67);
            this.label528.Name = "label528";
            this.label528.Size = new System.Drawing.Size(15, 13);
            this.label528.TabIndex = 2;
            this.label528.Text = "ft";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(445, 17);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(15, 13);
            this.label143.TabIndex = 2;
            this.label143.Text = "ft";
            // 
            // label525
            // 
            this.label525.AutoSize = true;
            this.label525.Location = new System.Drawing.Point(207, 43);
            this.label525.Name = "label525";
            this.label525.Size = new System.Drawing.Size(15, 13);
            this.label525.TabIndex = 2;
            this.label525.Text = "ft";
            // 
            // txt_Ana_wf_RHS
            // 
            this.txt_Ana_wf_RHS.Location = new System.Drawing.Point(389, 14);
            this.txt_Ana_wf_RHS.Name = "txt_Ana_wf_RHS";
            this.txt_Ana_wf_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_wf_RHS.TabIndex = 0;
            this.txt_Ana_wf_RHS.Text = "1.4375";
            this.txt_Ana_wf_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label526
            // 
            this.label526.AutoSize = true;
            this.label526.Location = new System.Drawing.Point(207, 16);
            this.label526.Name = "label526";
            this.label526.Size = new System.Drawing.Size(15, 13);
            this.label526.TabIndex = 2;
            this.label526.Text = "ft";
            // 
            // txt_Ana_wf_LHS
            // 
            this.txt_Ana_wf_LHS.Location = new System.Drawing.Point(151, 13);
            this.txt_Ana_wf_LHS.Name = "txt_Ana_wf_LHS";
            this.txt_Ana_wf_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_wf_LHS.TabIndex = 0;
            this.txt_Ana_wf_LHS.Text = "1.4375";
            this.txt_Ana_wf_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chk_footpath
            // 
            this.chk_footpath.AutoSize = true;
            this.chk_footpath.Location = new System.Drawing.Point(6, 164);
            this.chk_footpath.Name = "chk_footpath";
            this.chk_footpath.Size = new System.Drawing.Size(177, 17);
            this.chk_footpath.TabIndex = 106;
            this.chk_footpath.Text = "SIDE WALK/FOOTPATH ";
            this.chk_footpath.UseVisualStyleBackColor = true;
            this.chk_footpath.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_fp_left
            // 
            this.chk_fp_left.AutoSize = true;
            this.chk_fp_left.ForeColor = System.Drawing.Color.Blue;
            this.chk_fp_left.Location = new System.Drawing.Point(245, 162);
            this.chk_fp_left.Name = "chk_fp_left";
            this.chk_fp_left.Size = new System.Drawing.Size(92, 17);
            this.chk_fp_left.TabIndex = 2;
            this.chk_fp_left.Text = "LEFT SIDE";
            this.chk_fp_left.UseVisualStyleBackColor = true;
            this.chk_fp_left.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_fp_right
            // 
            this.chk_fp_right.AutoSize = true;
            this.chk_fp_right.ForeColor = System.Drawing.Color.Blue;
            this.chk_fp_right.Location = new System.Drawing.Point(343, 162);
            this.chk_fp_right.Name = "chk_fp_right";
            this.chk_fp_right.Size = new System.Drawing.Size(101, 17);
            this.chk_fp_right.TabIndex = 2;
            this.chk_fp_right.Text = "RIGHT SIDE";
            this.chk_fp_right.UseVisualStyleBackColor = true;
            this.chk_fp_right.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_cb_right
            // 
            this.chk_cb_right.AutoSize = true;
            this.chk_cb_right.ForeColor = System.Drawing.Color.Blue;
            this.chk_cb_right.Location = new System.Drawing.Point(343, 89);
            this.chk_cb_right.Name = "chk_cb_right";
            this.chk_cb_right.Size = new System.Drawing.Size(101, 17);
            this.chk_cb_right.TabIndex = 103;
            this.chk_cb_right.Text = "RIGHT SIDE";
            this.chk_cb_right.UseVisualStyleBackColor = true;
            this.chk_cb_right.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_crash_barrier
            // 
            this.chk_crash_barrier.AutoSize = true;
            this.chk_crash_barrier.Location = new System.Drawing.Point(6, 82);
            this.chk_crash_barrier.Name = "chk_crash_barrier";
            this.chk_crash_barrier.Size = new System.Drawing.Size(127, 17);
            this.chk_crash_barrier.TabIndex = 105;
            this.chk_crash_barrier.Text = "CRASH BARRIER";
            this.chk_crash_barrier.UseVisualStyleBackColor = true;
            this.chk_crash_barrier.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chk_cb_left
            // 
            this.chk_cb_left.AutoSize = true;
            this.chk_cb_left.ForeColor = System.Drawing.Color.Blue;
            this.chk_cb_left.Location = new System.Drawing.Point(245, 89);
            this.chk_cb_left.Name = "chk_cb_left";
            this.chk_cb_left.Size = new System.Drawing.Size(92, 17);
            this.chk_cb_left.TabIndex = 104;
            this.chk_cb_left.Text = "LEFT SIDE";
            this.chk_cb_left.UseVisualStyleBackColor = true;
            this.chk_cb_left.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // grb_ana_crashBarrier
            // 
            this.grb_ana_crashBarrier.Controls.Add(this.label178);
            this.grb_ana_crashBarrier.Controls.Add(this.label180);
            this.grb_ana_crashBarrier.Controls.Add(this.label182);
            this.grb_ana_crashBarrier.Controls.Add(this.label184);
            this.grb_ana_crashBarrier.Controls.Add(this.txt_Ana_Hc_RHS);
            this.grb_ana_crashBarrier.Controls.Add(this.label186);
            this.grb_ana_crashBarrier.Controls.Add(this.label188);
            this.grb_ana_crashBarrier.Controls.Add(this.txt_Ana_Wc_RHS);
            this.grb_ana_crashBarrier.Controls.Add(this.txt_Ana_Hc_LHS);
            this.grb_ana_crashBarrier.Controls.Add(this.label189);
            this.grb_ana_crashBarrier.Controls.Add(this.label195);
            this.grb_ana_crashBarrier.Controls.Add(this.txt_Ana_Wc_LHS);
            this.grb_ana_crashBarrier.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_crashBarrier.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_crashBarrier.Location = new System.Drawing.Point(6, 99);
            this.grb_ana_crashBarrier.Name = "grb_ana_crashBarrier";
            this.grb_ana_crashBarrier.Size = new System.Drawing.Size(475, 61);
            this.grb_ana_crashBarrier.TabIndex = 102;
            this.grb_ana_crashBarrier.TabStop = false;
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label178.Location = new System.Drawing.Point(282, 37);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(101, 13);
            this.label178.TabIndex = 5;
            this.label178.Text = "Height [RHS_hc]";
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label180.Location = new System.Drawing.Point(8, 37);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(99, 13);
            this.label180.TabIndex = 5;
            this.label180.Text = "Height [LHS_hc]";
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label182.Location = new System.Drawing.Point(284, 12);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(99, 13);
            this.label182.TabIndex = 4;
            this.label182.Text = "Width [RHS_wc]";
            // 
            // label184
            // 
            this.label184.AutoSize = true;
            this.label184.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label184.Location = new System.Drawing.Point(8, 12);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(97, 13);
            this.label184.TabIndex = 4;
            this.label184.Text = "Width [LHS_wc]";
            // 
            // txt_Ana_Hc_RHS
            // 
            this.txt_Ana_Hc_RHS.Location = new System.Drawing.Point(389, 33);
            this.txt_Ana_Hc_RHS.Name = "txt_Ana_Hc_RHS";
            this.txt_Ana_Hc_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hc_RHS.TabIndex = 1;
            this.txt_Ana_Hc_RHS.Text = "0.0";
            this.txt_Ana_Hc_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(445, 37);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(15, 13);
            this.label186.TabIndex = 2;
            this.label186.Text = "ft";
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Location = new System.Drawing.Point(445, 13);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(15, 13);
            this.label188.TabIndex = 2;
            this.label188.Text = "ft";
            // 
            // txt_Ana_Wc_RHS
            // 
            this.txt_Ana_Wc_RHS.Location = new System.Drawing.Point(389, 9);
            this.txt_Ana_Wc_RHS.Name = "txt_Ana_Wc_RHS";
            this.txt_Ana_Wc_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wc_RHS.TabIndex = 0;
            this.txt_Ana_Wc_RHS.Text = "0.0";
            this.txt_Ana_Wc_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_Hc_LHS
            // 
            this.txt_Ana_Hc_LHS.Location = new System.Drawing.Point(151, 33);
            this.txt_Ana_Hc_LHS.Name = "txt_Ana_Hc_LHS";
            this.txt_Ana_Hc_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hc_LHS.TabIndex = 1;
            this.txt_Ana_Hc_LHS.Text = "0.0";
            this.txt_Ana_Hc_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(207, 13);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(15, 13);
            this.label189.TabIndex = 2;
            this.label189.Text = "ft";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(207, 37);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(15, 13);
            this.label195.TabIndex = 2;
            this.label195.Text = "ft";
            // 
            // txt_Ana_Wc_LHS
            // 
            this.txt_Ana_Wc_LHS.Location = new System.Drawing.Point(151, 9);
            this.txt_Ana_Wc_LHS.Name = "txt_Ana_Wc_LHS";
            this.txt_Ana_Wc_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wc_LHS.TabIndex = 0;
            this.txt_Ana_Wc_LHS.Text = "0.0";
            this.txt_Ana_Wc_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_ana_wc
            // 
            this.grb_ana_wc.Controls.Add(this.label511);
            this.grb_ana_wc.Controls.Add(this.txt_Ana_Wfws);
            this.grb_ana_wc.Controls.Add(this.label515);
            this.grb_ana_wc.Controls.Add(this.label520);
            this.grb_ana_wc.Controls.Add(this.txt_Ana_tfws);
            this.grb_ana_wc.Controls.Add(this.label521);
            this.grb_ana_wc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_wc.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_wc.Location = new System.Drawing.Point(6, 15);
            this.grb_ana_wc.Name = "grb_ana_wc";
            this.grb_ana_wc.Size = new System.Drawing.Size(475, 36);
            this.grb_ana_wc.TabIndex = 1;
            this.grb_ana_wc.TabStop = false;
            this.grb_ana_wc.Text = "Future wearing surface";
            // 
            // label511
            // 
            this.label511.AutoSize = true;
            this.label511.Location = new System.Drawing.Point(445, 16);
            this.label511.Name = "label511";
            this.label511.Size = new System.Drawing.Size(24, 13);
            this.label511.TabIndex = 5;
            this.label511.Text = "kcf";
            // 
            // txt_Ana_Wfws
            // 
            this.txt_Ana_Wfws.Location = new System.Drawing.Point(389, 13);
            this.txt_Ana_Wfws.Name = "txt_Ana_Wfws";
            this.txt_Ana_Wfws.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wfws.TabIndex = 1;
            this.txt_Ana_Wfws.Text = "0.14";
            this.txt_Ana_Wfws.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label515
            // 
            this.label515.AutoSize = true;
            this.label515.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label515.Location = new System.Drawing.Point(289, 16);
            this.label515.Name = "label515";
            this.label515.Size = new System.Drawing.Size(94, 13);
            this.label515.TabIndex = 3;
            this.label515.Text = "Density [Wfws]";
            // 
            // label520
            // 
            this.label520.AutoSize = true;
            this.label520.Location = new System.Drawing.Point(207, 17);
            this.label520.Name = "label520";
            this.label520.Size = new System.Drawing.Size(17, 13);
            this.label520.TabIndex = 2;
            this.label520.Text = "in";
            // 
            // txt_Ana_tfws
            // 
            this.txt_Ana_tfws.Location = new System.Drawing.Point(151, 14);
            this.txt_Ana_tfws.Name = "txt_Ana_tfws";
            this.txt_Ana_tfws.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_tfws.TabIndex = 0;
            this.txt_Ana_tfws.Text = "2.5";
            this.txt_Ana_tfws.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label521
            // 
            this.label521.AutoSize = true;
            this.label521.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label521.Location = new System.Drawing.Point(82, 17);
            this.label521.Name = "label521";
            this.label521.Size = new System.Drawing.Size(63, 13);
            this.label521.TabIndex = 0;
            this.label521.Text = "Thickness";
            // 
            // grb_ana_parapet
            // 
            this.grb_ana_parapet.Controls.Add(this.label514);
            this.grb_ana_parapet.Controls.Add(this.label523);
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_Hpar);
            this.grb_ana_parapet.Controls.Add(this.label510);
            this.grb_ana_parapet.Controls.Add(this.label522);
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_wbase);
            this.grb_ana_parapet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_parapet.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_parapet.Location = new System.Drawing.Point(6, 49);
            this.grb_ana_parapet.Name = "grb_ana_parapet";
            this.grb_ana_parapet.Size = new System.Drawing.Size(475, 37);
            this.grb_ana_parapet.TabIndex = 1;
            this.grb_ana_parapet.TabStop = false;
            this.grb_ana_parapet.Text = "Parapet Wall";
            // 
            // label514
            // 
            this.label514.AutoSize = true;
            this.label514.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label514.Location = new System.Drawing.Point(289, 17);
            this.label514.Name = "label514";
            this.label514.Size = new System.Drawing.Size(88, 13);
            this.label514.TabIndex = 5;
            this.label514.Text = "height: [Hpar]";
            // 
            // label523
            // 
            this.label523.AutoSize = true;
            this.label523.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label523.Location = new System.Drawing.Point(8, 17);
            this.label523.Name = "label523";
            this.label523.Size = new System.Drawing.Size(140, 13);
            this.label523.TabIndex = 4;
            this.label523.Text = "Width at base: [wbase]";
            // 
            // txt_Ana_Hpar
            // 
            this.txt_Ana_Hpar.Location = new System.Drawing.Point(389, 14);
            this.txt_Ana_Hpar.Name = "txt_Ana_Hpar";
            this.txt_Ana_Hpar.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hpar.TabIndex = 1;
            this.txt_Ana_Hpar.Text = "3.5";
            this.txt_Ana_Hpar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label510
            // 
            this.label510.AutoSize = true;
            this.label510.Location = new System.Drawing.Point(445, 17);
            this.label510.Name = "label510";
            this.label510.Size = new System.Drawing.Size(15, 13);
            this.label510.TabIndex = 2;
            this.label510.Text = "ft";
            // 
            // label522
            // 
            this.label522.AutoSize = true;
            this.label522.Location = new System.Drawing.Point(207, 17);
            this.label522.Name = "label522";
            this.label522.Size = new System.Drawing.Size(15, 13);
            this.label522.TabIndex = 2;
            this.label522.Text = "ft";
            // 
            // txt_Ana_wbase
            // 
            this.txt_Ana_wbase.Location = new System.Drawing.Point(151, 14);
            this.txt_Ana_wbase.Name = "txt_Ana_wbase";
            this.txt_Ana_wbase.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_wbase.TabIndex = 0;
            this.txt_Ana_wbase.Text = "1.4375";
            this.txt_Ana_wbase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_member_load
            // 
            this.txt_Ana_member_load.Location = new System.Drawing.Point(400, 23);
            this.txt_Ana_member_load.Multiline = true;
            this.txt_Ana_member_load.Name = "txt_Ana_member_load";
            this.txt_Ana_member_load.Size = new System.Drawing.Size(63, 37);
            this.txt_Ana_member_load.TabIndex = 101;
            this.txt_Ana_member_load.Visible = false;
            // 
            // grb_create_input_data
            // 
            this.grb_create_input_data.Controls.Add(this.label273);
            this.grb_create_input_data.Controls.Add(this.label271);
            this.grb_create_input_data.Controls.Add(this.label269);
            this.grb_create_input_data.Controls.Add(this.txt_support_distance);
            this.grb_create_input_data.Controls.Add(this.label272);
            this.grb_create_input_data.Controls.Add(this.txt_overhang_gap);
            this.grb_create_input_data.Controls.Add(this.label270);
            this.grb_create_input_data.Controls.Add(this.txt_exp_gap);
            this.grb_create_input_data.Controls.Add(this.label268);
            this.grb_create_input_data.Controls.Add(this.rbtn_multiple_cell);
            this.grb_create_input_data.Controls.Add(this.rbtn_single_cell);
            this.grb_create_input_data.Controls.Add(this.label83);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_width_cantilever);
            this.grb_create_input_data.Controls.Add(this.label81);
            this.grb_create_input_data.Controls.Add(this.label6);
            this.grb_create_input_data.Controls.Add(this.label49);
            this.grb_create_input_data.Controls.Add(this.label4);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_DL_eff_depth);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Bottom_Slab_Thickness);
            this.grb_create_input_data.Controls.Add(this.label5);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Top_Slab_Thickness);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_DL_factor);
            this.grb_create_input_data.Controls.Add(this.label240);
            this.grb_create_input_data.Controls.Add(this.label285);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_LL_factor);
            this.grb_create_input_data.Controls.Add(this.label82);
            this.grb_create_input_data.Controls.Add(this.label239);
            this.grb_create_input_data.Controls.Add(this.label32);
            this.grb_create_input_data.Controls.Add(this.label68);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Web_Thickness);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Span);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_B);
            this.grb_create_input_data.Controls.Add(this.label67);
            this.grb_create_input_data.Controls.Add(this.label80);
            this.grb_create_input_data.Controls.Add(this.label66);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Road_Width);
            this.grb_create_input_data.Controls.Add(this.label50);
            this.grb_create_input_data.Controls.Add(this.label65);
            this.grb_create_input_data.Controls.Add(this.label33);
            this.grb_create_input_data.Controls.Add(this.label64);
            this.grb_create_input_data.Controls.Add(this.label51);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Superstructure_depth);
            this.grb_create_input_data.Controls.Add(this.label63);
            this.grb_create_input_data.Controls.Add(this.label3);
            this.grb_create_input_data.Controls.Add(this.label62);
            this.grb_create_input_data.Controls.Add(this.label286);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Web_Spacing);
            this.grb_create_input_data.Controls.Add(this.txt_L3);
            this.grb_create_input_data.Controls.Add(this.label288);
            this.grb_create_input_data.Controls.Add(this.txt_L2);
            this.grb_create_input_data.Controls.Add(this.label289);
            this.grb_create_input_data.Controls.Add(this.label290);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_L1);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.Location = new System.Drawing.Point(4, 308);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(384, 554);
            this.grb_create_input_data.TabIndex = 1;
            this.grb_create_input_data.TabStop = false;
            this.grb_create_input_data.Text = "Bridge Geometry";
            // 
            // label273
            // 
            this.label273.AutoSize = true;
            this.label273.Location = new System.Drawing.Point(337, 243);
            this.label273.Name = "label273";
            this.label273.Size = new System.Drawing.Size(15, 13);
            this.label273.TabIndex = 148;
            this.label273.Text = "ft";
            // 
            // label271
            // 
            this.label271.AutoSize = true;
            this.label271.Location = new System.Drawing.Point(337, 203);
            this.label271.Name = "label271";
            this.label271.Size = new System.Drawing.Size(15, 13);
            this.label271.TabIndex = 149;
            this.label271.Text = "ft";
            // 
            // label269
            // 
            this.label269.AutoSize = true;
            this.label269.Location = new System.Drawing.Point(337, 167);
            this.label269.Name = "label269";
            this.label269.Size = new System.Drawing.Size(15, 13);
            this.label269.TabIndex = 150;
            this.label269.Text = "ft";
            // 
            // txt_support_distance
            // 
            this.txt_support_distance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txt_support_distance.Location = new System.Drawing.Point(273, 240);
            this.txt_support_distance.Name = "txt_support_distance";
            this.txt_support_distance.Size = new System.Drawing.Size(57, 21);
            this.txt_support_distance.TabIndex = 144;
            this.txt_support_distance.Text = "0.022";
            this.txt_support_distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label272
            // 
            this.label272.AutoSize = true;
            this.label272.Location = new System.Drawing.Point(14, 227);
            this.label272.Name = "label272";
            this.label272.Size = new System.Drawing.Size(211, 26);
            this.label272.TabIndex = 145;
            this.label272.Text = "Distance from Centre of Expansion \r\nGap to C.L. of Bearing";
            // 
            // txt_overhang_gap
            // 
            this.txt_overhang_gap.Location = new System.Drawing.Point(273, 200);
            this.txt_overhang_gap.Name = "txt_overhang_gap";
            this.txt_overhang_gap.Size = new System.Drawing.Size(57, 21);
            this.txt_overhang_gap.TabIndex = 143;
            this.txt_overhang_gap.Text = "0.67";
            this.txt_overhang_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_overhang_gap.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label270
            // 
            this.label270.AutoSize = true;
            this.label270.Location = new System.Drawing.Point(14, 195);
            this.label270.Name = "label270";
            this.label270.Size = new System.Drawing.Size(184, 26);
            this.label270.TabIndex = 146;
            this.label270.Text = "Overhang from C.L. of Bearing\r\n to Edge of Deck";
            // 
            // txt_exp_gap
            // 
            this.txt_exp_gap.Location = new System.Drawing.Point(273, 164);
            this.txt_exp_gap.Name = "txt_exp_gap";
            this.txt_exp_gap.Size = new System.Drawing.Size(57, 21);
            this.txt_exp_gap.TabIndex = 142;
            this.txt_exp_gap.Text = "0.012";
            this.txt_exp_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_exp_gap.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label268
            // 
            this.label268.AutoSize = true;
            this.label268.Location = new System.Drawing.Point(14, 167);
            this.label268.Name = "label268";
            this.label268.Size = new System.Drawing.Size(126, 13);
            this.label268.TabIndex = 147;
            this.label268.Text = "Expansion Gap width";
            // 
            // rbtn_multiple_cell
            // 
            this.rbtn_multiple_cell.AutoSize = true;
            this.rbtn_multiple_cell.Checked = true;
            this.rbtn_multiple_cell.Location = new System.Drawing.Point(15, 17);
            this.rbtn_multiple_cell.Name = "rbtn_multiple_cell";
            this.rbtn_multiple_cell.Size = new System.Drawing.Size(295, 17);
            this.rbtn_multiple_cell.TabIndex = 58;
            this.rbtn_multiple_cell.TabStop = true;
            this.rbtn_multiple_cell.Text = "Multiple Cell Box Girder (Cross Section Type 1)";
            this.rbtn_multiple_cell.UseVisualStyleBackColor = true;
            // 
            // rbtn_single_cell
            // 
            this.rbtn_single_cell.AutoSize = true;
            this.rbtn_single_cell.Location = new System.Drawing.Point(15, 40);
            this.rbtn_single_cell.Name = "rbtn_single_cell";
            this.rbtn_single_cell.Size = new System.Drawing.Size(287, 17);
            this.rbtn_single_cell.TabIndex = 58;
            this.rbtn_single_cell.Text = "Single Cell Box Girder (Cross Section Type 2)";
            this.rbtn_single_cell.UseVisualStyleBackColor = true;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(337, 472);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(17, 13);
            this.label83.TabIndex = 17;
            this.label83.Text = "in";
            // 
            // txt_Ana_width_cantilever
            // 
            this.txt_Ana_width_cantilever.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_width_cantilever.Location = new System.Drawing.Point(273, 370);
            this.txt_Ana_width_cantilever.Name = "txt_Ana_width_cantilever";
            this.txt_Ana_width_cantilever.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_width_cantilever.TabIndex = 3;
            this.txt_Ana_width_cantilever.Text = "6.5";
            this.txt_Ana_width_cantilever.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_width_cantilever.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(337, 448);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(17, 13);
            this.label81.TabIndex = 17;
            this.label81.Text = "in";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 346);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tentative Effective Depth";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(337, 424);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(17, 13);
            this.label49.TabIndex = 17;
            this.label49.Text = "in";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Overall Deck Width Along Z-direction [B]";
            // 
            // txt_Ana_DL_eff_depth
            // 
            this.txt_Ana_DL_eff_depth.Location = new System.Drawing.Point(273, 343);
            this.txt_Ana_DL_eff_depth.Name = "txt_Ana_DL_eff_depth";
            this.txt_Ana_DL_eff_depth.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_DL_eff_depth.TabIndex = 2;
            this.txt_Ana_DL_eff_depth.Text = "12.0";
            this.txt_Ana_DL_eff_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_eff_depth.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Bottom_Slab_Thickness
            // 
            this.txt_Ana_Bottom_Slab_Thickness.Location = new System.Drawing.Point(274, 469);
            this.txt_Ana_Bottom_Slab_Thickness.Name = "txt_Ana_Bottom_Slab_Thickness";
            this.txt_Ana_Bottom_Slab_Thickness.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Bottom_Slab_Thickness.TabIndex = 6;
            this.txt_Ana_Bottom_Slab_Thickness.Text = "6.0";
            this.txt_Ana_Bottom_Slab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "ft";
            // 
            // txt_Ana_Top_Slab_Thickness
            // 
            this.txt_Ana_Top_Slab_Thickness.Location = new System.Drawing.Point(274, 445);
            this.txt_Ana_Top_Slab_Thickness.Name = "txt_Ana_Top_Slab_Thickness";
            this.txt_Ana_Top_Slab_Thickness.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Top_Slab_Thickness.TabIndex = 6;
            this.txt_Ana_Top_Slab_Thickness.Text = "8.0";
            this.txt_Ana_Top_Slab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_DL_factor
            // 
            this.txt_Ana_DL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_DL_factor.Location = new System.Drawing.Point(274, 496);
            this.txt_Ana_DL_factor.Name = "txt_Ana_DL_factor";
            this.txt_Ana_DL_factor.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_DL_factor.TabIndex = 127;
            this.txt_Ana_DL_factor.Text = "1.25";
            this.txt_Ana_DL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // label240
            // 
            this.label240.AutoSize = true;
            this.label240.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label240.Location = new System.Drawing.Point(14, 502);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(106, 13);
            this.label240.TabIndex = 129;
            this.label240.Text = "Dead Load Factor";
            // 
            // label285
            // 
            this.label285.AutoSize = true;
            this.label285.Location = new System.Drawing.Point(336, 117);
            this.label285.Name = "label285";
            this.label285.Size = new System.Drawing.Size(15, 13);
            this.label285.TabIndex = 140;
            this.label285.Text = "ft";
            // 
            // txt_Ana_LL_factor
            // 
            this.txt_Ana_LL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_LL_factor.Location = new System.Drawing.Point(274, 523);
            this.txt_Ana_LL_factor.Name = "txt_Ana_LL_factor";
            this.txt_Ana_LL_factor.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_LL_factor.TabIndex = 130;
            this.txt_Ana_LL_factor.Text = "2.5";
            this.txt_Ana_LL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_LL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(16, 472);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(132, 13);
            this.label82.TabIndex = 15;
            this.label82.Text = "Bottom slab thickness";
            // 
            // label239
            // 
            this.label239.AutoSize = true;
            this.label239.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label239.Location = new System.Drawing.Point(14, 529);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(99, 13);
            this.label239.TabIndex = 131;
            this.label239.Text = "Live Load Factor";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(14, 373);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(175, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Width of Cantilever Slab [B2]";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(14, 137);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(73, 13);
            this.label68.TabIndex = 0;
            this.label68.Text = "Total length";
            // 
            // txt_Ana_Web_Thickness
            // 
            this.txt_Ana_Web_Thickness.Location = new System.Drawing.Point(274, 421);
            this.txt_Ana_Web_Thickness.Name = "txt_Ana_Web_Thickness";
            this.txt_Ana_Web_Thickness.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Web_Thickness.TabIndex = 6;
            this.txt_Ana_Web_Thickness.Text = "12.0";
            this.txt_Ana_Web_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_Span
            // 
            this.txt_Ana_Span.Location = new System.Drawing.Point(273, 137);
            this.txt_Ana_Span.Name = "txt_Ana_Span";
            this.txt_Ana_Span.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Span.TabIndex = 0;
            this.txt_Ana_Span.Text = "756.0";
            this.txt_Ana_Span.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_B
            // 
            this.txt_Ana_B.Location = new System.Drawing.Point(273, 267);
            this.txt_Ana_B.Name = "txt_Ana_B";
            this.txt_Ana_B.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_B.TabIndex = 1;
            this.txt_Ana_B.Text = "52.0";
            this.txt_Ana_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_B.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(336, 140);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(15, 13);
            this.label67.TabIndex = 2;
            this.label67.Text = "ft";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(16, 448);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(111, 13);
            this.label80.TabIndex = 15;
            this.label80.Text = "Top slab thickness";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(14, 294);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(93, 13);
            this.label66.TabIndex = 3;
            this.label66.Text = "Roadway width";
            // 
            // txt_Ana_Road_Width
            // 
            this.txt_Ana_Road_Width.Location = new System.Drawing.Point(273, 294);
            this.txt_Ana_Road_Width.Name = "txt_Ana_Road_Width";
            this.txt_Ana_Road_Width.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Road_Width.TabIndex = 1;
            this.txt_Ana_Road_Width.Text = "42.0";
            this.txt_Ana_Road_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(337, 397);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(15, 13);
            this.label50.TabIndex = 18;
            this.label50.Text = "ft";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(336, 297);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(15, 13);
            this.label65.TabIndex = 5;
            this.label65.Text = "ft";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(337, 373);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "ft";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(14, 316);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(128, 13);
            this.label64.TabIndex = 6;
            this.label64.Text = "Superstructure depth";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(16, 424);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(88, 13);
            this.label51.TabIndex = 15;
            this.label51.Text = "Web thickness";
            // 
            // txt_Ana_Superstructure_depth
            // 
            this.txt_Ana_Superstructure_depth.Location = new System.Drawing.Point(273, 316);
            this.txt_Ana_Superstructure_depth.Name = "txt_Ana_Superstructure_depth";
            this.txt_Ana_Superstructure_depth.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Superstructure_depth.TabIndex = 2;
            this.txt_Ana_Superstructure_depth.Text = "5.50";
            this.txt_Ana_Superstructure_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(336, 319);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(15, 13);
            this.label63.TabIndex = 8;
            this.label63.Text = "ft";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ft";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(16, 397);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(78, 13);
            this.label62.TabIndex = 54;
            this.label62.Text = "Web spacing";
            // 
            // label286
            // 
            this.label286.AutoSize = true;
            this.label286.Location = new System.Drawing.Point(336, 93);
            this.label286.Name = "label286";
            this.label286.Size = new System.Drawing.Size(15, 13);
            this.label286.TabIndex = 141;
            this.label286.Text = "ft";
            // 
            // txt_Ana_Web_Spacing
            // 
            this.txt_Ana_Web_Spacing.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_Web_Spacing.Location = new System.Drawing.Point(274, 394);
            this.txt_Ana_Web_Spacing.Name = "txt_Ana_Web_Spacing";
            this.txt_Ana_Web_Spacing.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Web_Spacing.TabIndex = 3;
            this.txt_Ana_Web_Spacing.Text = "7.75";
            this.txt_Ana_Web_Spacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_L3
            // 
            this.txt_L3.Location = new System.Drawing.Point(273, 114);
            this.txt_L3.Name = "txt_L3";
            this.txt_L3.Size = new System.Drawing.Size(57, 21);
            this.txt_L3.TabIndex = 138;
            this.txt_L3.Text = "214.0";
            this.txt_L3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_L3.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label288
            // 
            this.label288.AutoSize = true;
            this.label288.Location = new System.Drawing.Point(14, 117);
            this.label288.Name = "label288";
            this.label288.Size = new System.Drawing.Size(137, 13);
            this.label288.TabIndex = 136;
            this.label288.Text = "End/Side Span 2   [L3]";
            // 
            // txt_L2
            // 
            this.txt_L2.Location = new System.Drawing.Point(273, 90);
            this.txt_L2.Name = "txt_L2";
            this.txt_L2.Size = new System.Drawing.Size(57, 21);
            this.txt_L2.TabIndex = 135;
            this.txt_L2.Text = "214.0";
            this.txt_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_L2.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label289
            // 
            this.label289.AutoSize = true;
            this.label289.Location = new System.Drawing.Point(14, 93);
            this.label289.Name = "label289";
            this.label289.Size = new System.Drawing.Size(137, 13);
            this.label289.TabIndex = 134;
            this.label289.Text = "End/Side Span 1   [L2]";
            // 
            // label290
            // 
            this.label290.AutoSize = true;
            this.label290.Location = new System.Drawing.Point(14, 66);
            this.label290.Name = "label290";
            this.label290.Size = new System.Drawing.Size(253, 13);
            this.label290.TabIndex = 132;
            this.label290.Text = "Central/Intermediate of Central Span  [L1]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ft";
            // 
            // txt_Ana_L1
            // 
            this.txt_Ana_L1.Location = new System.Drawing.Point(273, 63);
            this.txt_Ana_L1.Name = "txt_Ana_L1";
            this.txt_Ana_L1.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_L1.TabIndex = 0;
            this.txt_Ana_L1.Text = "328.0";
            this.txt_Ana_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_L1.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.label115);
            this.groupBox23.Controls.Add(this.label109);
            this.groupBox23.Controls.Add(this.label119);
            this.groupBox23.Controls.Add(this.label111);
            this.groupBox23.Controls.Add(this.label113);
            this.groupBox23.Controls.Add(this.label116);
            this.groupBox23.Controls.Add(this.label122);
            this.groupBox23.Controls.Add(this.label120);
            this.groupBox23.Controls.Add(this.label110);
            this.groupBox23.Controls.Add(this.txt_Ana_Superstructure_fci);
            this.groupBox23.Controls.Add(this.txt_Ana_column_fc);
            this.groupBox23.Controls.Add(this.label112);
            this.groupBox23.Controls.Add(this.label114);
            this.groupBox23.Controls.Add(this.txt_Ana_Concrete_DL_Calculation);
            this.groupBox23.Controls.Add(this.label121);
            this.groupBox23.Controls.Add(this.txt_Ana_Concrete_Ec);
            this.groupBox23.Controls.Add(this.label117);
            this.groupBox23.Controls.Add(this.txt_Ana_Superstructure_fc);
            this.groupBox23.Controls.Add(this.label118);
            this.groupBox23.Controls.Add(this.txt_box_weight);
            this.groupBox23.Controls.Add(this.label18);
            this.groupBox23.Controls.Add(this.label19);
            this.groupBox23.Location = new System.Drawing.Point(4, 1134);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(384, 216);
            this.groupBox23.TabIndex = 183;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Concrete";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(7, 111);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(317, 13);
            this.label115.TabIndex = 15;
            this.label115.Text = "Unit weight for normal weight concrete is listed below:";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(6, 15);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(356, 13);
            this.label109.TabIndex = 15;
            this.label109.Text = "The final and release concrete strengths are specified below:";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(210, 37);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(138, 13);
            this.label119.TabIndex = 15;
            this.label119.Text = "Column & Drilled Shaft";
            this.label119.UseMnemonic = false;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(7, 37);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(92, 13);
            this.label111.TabIndex = 15;
            this.label111.Text = "Superstructure";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(7, 88);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(23, 13);
            this.label113.TabIndex = 15;
            this.label113.Text = "f’ci";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(213, 64);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(20, 13);
            this.label116.TabIndex = 15;
            this.label116.Text = "f’c";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(7, 160);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(173, 13);
            this.label122.TabIndex = 15;
            this.label122.Text = "Unit weight for DL calculation";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(7, 133);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(180, 13);
            this.label120.TabIndex = 15;
            this.label120.Text = "Unit weight for computing [Ec]";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(7, 66);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(20, 13);
            this.label110.TabIndex = 15;
            this.label110.Text = "f’c";
            // 
            // txt_Ana_Superstructure_fci
            // 
            this.txt_Ana_Superstructure_fci.Location = new System.Drawing.Point(36, 85);
            this.txt_Ana_Superstructure_fci.Name = "txt_Ana_Superstructure_fci";
            this.txt_Ana_Superstructure_fci.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Superstructure_fci.TabIndex = 6;
            this.txt_Ana_Superstructure_fci.Text = "3.5";
            this.txt_Ana_Superstructure_fci.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_column_fc
            // 
            this.txt_Ana_column_fc.Location = new System.Drawing.Point(242, 61);
            this.txt_Ana_column_fc.Name = "txt_Ana_column_fc";
            this.txt_Ana_column_fc.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_column_fc.TabIndex = 6;
            this.txt_Ana_column_fc.Text = "3.5";
            this.txt_Ana_column_fc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(93, 88);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(23, 13);
            this.label112.TabIndex = 17;
            this.label112.Text = "ksi";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(299, 64);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(23, 13);
            this.label114.TabIndex = 17;
            this.label114.Text = "ksi";
            // 
            // txt_Ana_Concrete_DL_Calculation
            // 
            this.txt_Ana_Concrete_DL_Calculation.Location = new System.Drawing.Point(235, 160);
            this.txt_Ana_Concrete_DL_Calculation.Name = "txt_Ana_Concrete_DL_Calculation";
            this.txt_Ana_Concrete_DL_Calculation.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Concrete_DL_Calculation.TabIndex = 6;
            this.txt_Ana_Concrete_DL_Calculation.Text = "0.150";
            this.txt_Ana_Concrete_DL_Calculation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(299, 163);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(24, 13);
            this.label121.TabIndex = 17;
            this.label121.Text = "kcf";
            // 
            // txt_Ana_Concrete_Ec
            // 
            this.txt_Ana_Concrete_Ec.Location = new System.Drawing.Point(235, 133);
            this.txt_Ana_Concrete_Ec.Name = "txt_Ana_Concrete_Ec";
            this.txt_Ana_Concrete_Ec.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Concrete_Ec.TabIndex = 6;
            this.txt_Ana_Concrete_Ec.Text = "0.145";
            this.txt_Ana_Concrete_Ec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(299, 136);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(24, 13);
            this.label117.TabIndex = 17;
            this.label117.Text = "kcf";
            // 
            // txt_Ana_Superstructure_fc
            // 
            this.txt_Ana_Superstructure_fc.Location = new System.Drawing.Point(36, 61);
            this.txt_Ana_Superstructure_fc.Name = "txt_Ana_Superstructure_fc";
            this.txt_Ana_Superstructure_fc.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Superstructure_fc.TabIndex = 6;
            this.txt_Ana_Superstructure_fc.Text = "4.5";
            this.txt_Ana_Superstructure_fc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(93, 64);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(23, 13);
            this.label118.TabIndex = 17;
            this.label118.Text = "ksi";
            // 
            // txt_box_weight
            // 
            this.txt_box_weight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_box_weight.Location = new System.Drawing.Point(236, 188);
            this.txt_box_weight.Name = "txt_box_weight";
            this.txt_box_weight.Size = new System.Drawing.Size(57, 21);
            this.txt_box_weight.TabIndex = 130;
            this.txt_box_weight.Text = "2.5";
            this.txt_box_weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_weight.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 189);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(116, 13);
            this.label18.TabIndex = 131;
            this.label18.Text = "Unit Weight of Box \r\n";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(299, 191);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(24, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "kcf";
            // 
            // grb_SIDL
            // 
            this.grb_SIDL.Controls.Add(this.txt_Ana_LL_member_load);
            this.grb_SIDL.Controls.Add(this.btn_remove);
            this.grb_SIDL.Controls.Add(this.btn_remove_all);
            this.grb_SIDL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_SIDL.Location = new System.Drawing.Point(420, 39);
            this.grb_SIDL.Name = "grb_SIDL";
            this.grb_SIDL.Size = new System.Drawing.Size(398, 11);
            this.grb_SIDL.TabIndex = 96;
            this.grb_SIDL.TabStop = false;
            this.grb_SIDL.Text = "Dead Load + Super Imposed Dead Load [DL + SIDL]";
            this.grb_SIDL.Visible = false;
            // 
            // txt_Ana_LL_member_load
            // 
            this.txt_Ana_LL_member_load.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Ana_LL_member_load.Location = new System.Drawing.Point(3, 17);
            this.txt_Ana_LL_member_load.Multiline = true;
            this.txt_Ana_LL_member_load.Name = "txt_Ana_LL_member_load";
            this.txt_Ana_LL_member_load.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Ana_LL_member_load.Size = new System.Drawing.Size(392, 0);
            this.txt_Ana_LL_member_load.TabIndex = 46;
            this.txt_Ana_LL_member_load.WordWrap = false;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(351, 203);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 45;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            // 
            // btn_remove_all
            // 
            this.btn_remove_all.Location = new System.Drawing.Point(441, 203);
            this.btn_remove_all.Name = "btn_remove_all";
            this.btn_remove_all.Size = new System.Drawing.Size(75, 23);
            this.btn_remove_all.TabIndex = 44;
            this.btn_remove_all.Text = "Remove All";
            this.btn_remove_all.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.label311);
            this.groupBox27.Controls.Add(this.cmb_cable_type);
            this.groupBox27.Controls.Add(this.txt_cbl_des_f);
            this.groupBox27.Controls.Add(this.label312);
            this.groupBox27.Controls.Add(this.txt_cp);
            this.groupBox27.Controls.Add(this.label59);
            this.groupBox27.Controls.Add(this.label313);
            this.groupBox27.Controls.Add(this.label308);
            this.groupBox27.Controls.Add(this.txt_cbl_des_E);
            this.groupBox27.Controls.Add(this.label314);
            this.groupBox27.Controls.Add(this.label287);
            this.groupBox27.Controls.Add(this.label504);
            this.groupBox27.Controls.Add(this.label149);
            this.groupBox27.Controls.Add(this.label20);
            this.groupBox27.Controls.Add(this.label60);
            this.groupBox27.Controls.Add(this.label309);
            this.groupBox27.Controls.Add(this.label298);
            this.groupBox27.Controls.Add(this.txt_tower_Dt);
            this.groupBox27.Controls.Add(this.txt_cbl_des_gamma);
            this.groupBox27.Controls.Add(this.label150);
            this.groupBox27.Controls.Add(this.label296);
            this.groupBox27.Controls.Add(this.label310);
            this.groupBox27.Controls.Add(this.label294);
            this.groupBox27.Controls.Add(this.txt_Tower_Height);
            this.groupBox27.Controls.Add(this.txt_init_cable);
            this.groupBox27.Controls.Add(this.label292);
            this.groupBox27.Controls.Add(this.txt_cable_no);
            this.groupBox27.Controls.Add(this.txt_horizontal_cbl_dist);
            this.groupBox27.Controls.Add(this.label75);
            this.groupBox27.Controls.Add(this.label307);
            this.groupBox27.Controls.Add(this.label1);
            this.groupBox27.Controls.Add(this.label503);
            this.groupBox27.Controls.Add(this.label284);
            this.groupBox27.Controls.Add(this.label297);
            this.groupBox27.Controls.Add(this.txt_tower_Bt);
            this.groupBox27.Controls.Add(this.txt_cable_dia);
            this.groupBox27.Controls.Add(this.txt_vertical_cbl_min_dist);
            this.groupBox27.Controls.Add(this.txt_vertical_cbl_dist);
            this.groupBox27.Controls.Add(this.label295);
            this.groupBox27.Controls.Add(this.label293);
            this.groupBox27.Controls.Add(this.label291);
            this.groupBox27.Location = new System.Drawing.Point(400, 928);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(514, 319);
            this.groupBox27.TabIndex = 180;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Cable Inputs";
            // 
            // label311
            // 
            this.label311.AutoSize = true;
            this.label311.Location = new System.Drawing.Point(13, 297);
            this.label311.Name = "label311";
            this.label311.Size = new System.Drawing.Size(204, 13);
            this.label311.TabIndex = 190;
            this.label311.Text = "Permissible Stress in the Cable [f]";
            // 
            // cmb_cable_type
            // 
            this.cmb_cable_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cable_type.FormattingEnabled = true;
            this.cmb_cable_type.Items.AddRange(new object[] {
            "Double Tower on either side at each end",
            "Single Centrally Placed at each end",
            "Without Towers and Cables"});
            this.cmb_cable_type.Location = new System.Drawing.Point(168, 13);
            this.cmb_cable_type.Name = "cmb_cable_type";
            this.cmb_cable_type.Size = new System.Drawing.Size(269, 21);
            this.cmb_cable_type.TabIndex = 181;
            this.cmb_cable_type.Visible = false;
            // 
            // txt_cbl_des_f
            // 
            this.txt_cbl_des_f.Location = new System.Drawing.Point(379, 294);
            this.txt_cbl_des_f.Name = "txt_cbl_des_f";
            this.txt_cbl_des_f.Size = new System.Drawing.Size(58, 21);
            this.txt_cbl_des_f.TabIndex = 189;
            this.txt_cbl_des_f.Text = "1770.0";
            this.txt_cbl_des_f.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cbl_des_f.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label312
            // 
            this.label312.AutoSize = true;
            this.label312.Location = new System.Drawing.Point(445, 297);
            this.label312.Name = "label312";
            this.label312.Size = new System.Drawing.Size(24, 13);
            this.label312.TabIndex = 188;
            this.label312.Text = "ksf";
            // 
            // txt_cp
            // 
            this.txt_cp.Location = new System.Drawing.Point(379, 225);
            this.txt_cp.Name = "txt_cp";
            this.txt_cp.Size = new System.Drawing.Size(58, 21);
            this.txt_cp.TabIndex = 86;
            this.txt_cp.Text = "40";
            this.txt_cp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(13, 228);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(204, 13);
            this.label59.TabIndex = 87;
            this.label59.Text = "Percentage of Load to applied [cp]";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label313
            // 
            this.label313.AutoSize = true;
            this.label313.Location = new System.Drawing.Point(13, 275);
            this.label313.Name = "label313";
            this.label313.Size = new System.Drawing.Size(215, 13);
            this.label313.TabIndex = 187;
            this.label313.Text = "Elustic Modulus of Cable Material [E]";
            // 
            // label308
            // 
            this.label308.AutoSize = true;
            this.label308.Location = new System.Drawing.Point(13, 63);
            this.label308.Name = "label308";
            this.label308.Size = new System.Drawing.Size(134, 13);
            this.label308.TabIndex = 143;
            this.label308.Text = "Tower Size [Bt] X [Dt]";
            // 
            // txt_cbl_des_E
            // 
            this.txt_cbl_des_E.Location = new System.Drawing.Point(355, 272);
            this.txt_cbl_des_E.Name = "txt_cbl_des_E";
            this.txt_cbl_des_E.Size = new System.Drawing.Size(82, 21);
            this.txt_cbl_des_E.TabIndex = 186;
            this.txt_cbl_des_E.Text = "290000";
            this.txt_cbl_des_E.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cbl_des_E.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label314
            // 
            this.label314.AutoSize = true;
            this.label314.Location = new System.Drawing.Point(445, 275);
            this.label314.Name = "label314";
            this.label314.Size = new System.Drawing.Size(24, 13);
            this.label314.TabIndex = 185;
            this.label314.Text = "ksf";
            // 
            // label287
            // 
            this.label287.AutoSize = true;
            this.label287.Location = new System.Drawing.Point(13, 110);
            this.label287.Name = "label287";
            this.label287.Size = new System.Drawing.Size(145, 13);
            this.label287.TabIndex = 143;
            this.label287.Text = "Diameter of Cables [cd]";
            // 
            // label504
            // 
            this.label504.AutoSize = true;
            this.label504.Location = new System.Drawing.Point(13, 209);
            this.label504.Name = "label504";
            this.label504.Size = new System.Drawing.Size(276, 13);
            this.label504.TabIndex = 143;
            this.label504.Text = "Vertical Height of First Cable Above Deck Level";
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(445, 252);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(24, 13);
            this.label149.TabIndex = 182;
            this.label149.Text = "kcf";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(443, 228);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 92;
            this.label20.Text = "%";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(13, 16);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(71, 13);
            this.label60.TabIndex = 143;
            this.label60.Text = "Cable Type";
            this.label60.Visible = false;
            // 
            // label309
            // 
            this.label309.AutoSize = true;
            this.label309.Location = new System.Drawing.Point(13, 252);
            this.label309.Name = "label309";
            this.label309.Size = new System.Drawing.Size(219, 13);
            this.label309.TabIndex = 184;
            this.label309.Text = "Specific Weight of Cable [γ(gamma)]";
            this.label309.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label298
            // 
            this.label298.AutoSize = true;
            this.label298.Location = new System.Drawing.Point(13, 186);
            this.label298.Name = "label298";
            this.label298.Size = new System.Drawing.Size(249, 13);
            this.label298.TabIndex = 143;
            this.label298.Text = "Vertical Spacing between Cables on Tower";
            // 
            // txt_tower_Dt
            // 
            this.txt_tower_Dt.Location = new System.Drawing.Point(379, 60);
            this.txt_tower_Dt.Name = "txt_tower_Dt";
            this.txt_tower_Dt.Size = new System.Drawing.Size(58, 21);
            this.txt_tower_Dt.TabIndex = 4;
            this.txt_tower_Dt.Text = "3.28";
            this.txt_tower_Dt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cbl_des_gamma
            // 
            this.txt_cbl_des_gamma.Location = new System.Drawing.Point(379, 249);
            this.txt_cbl_des_gamma.Name = "txt_cbl_des_gamma";
            this.txt_cbl_des_gamma.Size = new System.Drawing.Size(58, 21);
            this.txt_cbl_des_gamma.TabIndex = 183;
            this.txt_cbl_des_gamma.Text = "0.075";
            this.txt_cbl_des_gamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cbl_des_gamma.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(13, 133);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(366, 13);
            this.label150.TabIndex = 143;
            this.label150.Text = "Horizontal Distance of First Cable from Tower Centre Line [D1]";
            // 
            // label296
            // 
            this.label296.AutoSize = true;
            this.label296.Location = new System.Drawing.Point(13, 162);
            this.label296.Name = "label296";
            this.label296.Size = new System.Drawing.Size(320, 13);
            this.label296.TabIndex = 143;
            this.label296.Text = "Horizontal Spacing between Cables at Deck Level [D2]";
            // 
            // label310
            // 
            this.label310.AutoSize = true;
            this.label310.Location = new System.Drawing.Point(443, 63);
            this.label310.Name = "label310";
            this.label310.Size = new System.Drawing.Size(15, 13);
            this.label310.TabIndex = 92;
            this.label310.Text = "ft";
            this.label310.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label294
            // 
            this.label294.AutoSize = true;
            this.label294.Location = new System.Drawing.Point(13, 86);
            this.label294.Name = "label294";
            this.label294.Size = new System.Drawing.Size(350, 13);
            this.label294.TabIndex = 143;
            this.label294.Text = "Total Number of Cables on either side of Each Tower[NCAB]";
            // 
            // txt_Tower_Height
            // 
            this.txt_Tower_Height.Location = new System.Drawing.Point(379, 36);
            this.txt_Tower_Height.Name = "txt_Tower_Height";
            this.txt_Tower_Height.Size = new System.Drawing.Size(58, 21);
            this.txt_Tower_Height.TabIndex = 137;
            this.txt_Tower_Height.Text = "40.0";
            this.txt_Tower_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_init_cable
            // 
            this.txt_init_cable.Location = new System.Drawing.Point(379, 130);
            this.txt_init_cable.Name = "txt_init_cable";
            this.txt_init_cable.Size = new System.Drawing.Size(58, 21);
            this.txt_init_cable.TabIndex = 137;
            this.txt_init_cable.Text = "44.0";
            this.txt_init_cable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label292
            // 
            this.label292.AutoSize = true;
            this.label292.Location = new System.Drawing.Point(13, 44);
            this.label292.Name = "label292";
            this.label292.Size = new System.Drawing.Size(125, 13);
            this.label292.TabIndex = 143;
            this.label292.Text = "Height of Tower [H1]";
            // 
            // txt_cable_no
            // 
            this.txt_cable_no.Location = new System.Drawing.Point(379, 83);
            this.txt_cable_no.Name = "txt_cable_no";
            this.txt_cable_no.Size = new System.Drawing.Size(58, 21);
            this.txt_cable_no.TabIndex = 137;
            this.txt_cable_no.Text = "10";
            this.txt_cable_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_horizontal_cbl_dist
            // 
            this.txt_horizontal_cbl_dist.Location = new System.Drawing.Point(379, 154);
            this.txt_horizontal_cbl_dist.Name = "txt_horizontal_cbl_dist";
            this.txt_horizontal_cbl_dist.Size = new System.Drawing.Size(58, 21);
            this.txt_horizontal_cbl_dist.TabIndex = 137;
            this.txt_horizontal_cbl_dist.Text = "19.6";
            this.txt_horizontal_cbl_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(355, 63);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(15, 13);
            this.label75.TabIndex = 139;
            this.label75.Text = "X";
            // 
            // label307
            // 
            this.label307.AutoSize = true;
            this.label307.Location = new System.Drawing.Point(330, 63);
            this.label307.Name = "label307";
            this.label307.Size = new System.Drawing.Size(18, 13);
            this.label307.TabIndex = 139;
            this.label307.Text = "m";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "ft";
            // 
            // label503
            // 
            this.label503.AutoSize = true;
            this.label503.Location = new System.Drawing.Point(443, 204);
            this.label503.Name = "label503";
            this.label503.Size = new System.Drawing.Size(15, 13);
            this.label503.TabIndex = 139;
            this.label503.Text = "ft";
            // 
            // label284
            // 
            this.label284.AutoSize = true;
            this.label284.Location = new System.Drawing.Point(443, 133);
            this.label284.Name = "label284";
            this.label284.Size = new System.Drawing.Size(15, 13);
            this.label284.TabIndex = 139;
            this.label284.Text = "ft";
            // 
            // label297
            // 
            this.label297.AutoSize = true;
            this.label297.Location = new System.Drawing.Point(443, 181);
            this.label297.Name = "label297";
            this.label297.Size = new System.Drawing.Size(15, 13);
            this.label297.TabIndex = 139;
            this.label297.Text = "ft";
            // 
            // txt_tower_Bt
            // 
            this.txt_tower_Bt.Location = new System.Drawing.Point(266, 60);
            this.txt_tower_Bt.Name = "txt_tower_Bt";
            this.txt_tower_Bt.Size = new System.Drawing.Size(58, 21);
            this.txt_tower_Bt.TabIndex = 137;
            this.txt_tower_Bt.Text = "3.28";
            this.txt_tower_Bt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cable_dia
            // 
            this.txt_cable_dia.Location = new System.Drawing.Point(379, 107);
            this.txt_cable_dia.Name = "txt_cable_dia";
            this.txt_cable_dia.Size = new System.Drawing.Size(58, 21);
            this.txt_cable_dia.TabIndex = 137;
            this.txt_cable_dia.Text = "0.50";
            this.txt_cable_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_vertical_cbl_min_dist
            // 
            this.txt_vertical_cbl_min_dist.Location = new System.Drawing.Point(379, 201);
            this.txt_vertical_cbl_min_dist.Name = "txt_vertical_cbl_min_dist";
            this.txt_vertical_cbl_min_dist.Size = new System.Drawing.Size(58, 21);
            this.txt_vertical_cbl_min_dist.TabIndex = 137;
            this.txt_vertical_cbl_min_dist.Text = "10.0";
            this.txt_vertical_cbl_min_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_vertical_cbl_dist
            // 
            this.txt_vertical_cbl_dist.Location = new System.Drawing.Point(379, 178);
            this.txt_vertical_cbl_dist.Name = "txt_vertical_cbl_dist";
            this.txt_vertical_cbl_dist.Size = new System.Drawing.Size(58, 21);
            this.txt_vertical_cbl_dist.TabIndex = 137;
            this.txt_vertical_cbl_dist.Text = "4.5";
            this.txt_vertical_cbl_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label295
            // 
            this.label295.AutoSize = true;
            this.label295.Location = new System.Drawing.Point(443, 157);
            this.label295.Name = "label295";
            this.label295.Size = new System.Drawing.Size(15, 13);
            this.label295.TabIndex = 139;
            this.label295.Text = "ft";
            // 
            // label293
            // 
            this.label293.AutoSize = true;
            this.label293.Location = new System.Drawing.Point(443, 86);
            this.label293.Name = "label293";
            this.label293.Size = new System.Drawing.Size(27, 13);
            this.label293.TabIndex = 139;
            this.label293.Text = "nos";
            // 
            // label291
            // 
            this.label291.AutoSize = true;
            this.label291.Location = new System.Drawing.Point(443, 39);
            this.label291.Name = "label291";
            this.label291.Size = new System.Drawing.Size(15, 13);
            this.label291.TabIndex = 139;
            this.label291.Text = "ft";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label136);
            this.groupBox8.Controls.Add(this.button1);
            this.groupBox8.Controls.Add(this.txt_Ana_FPLL);
            this.groupBox8.Controls.Add(this.label138);
            this.groupBox8.Controls.Add(this.txt_Ana_SIDL);
            this.groupBox8.Controls.Add(this.label135);
            this.groupBox8.Controls.Add(this.label137);
            this.groupBox8.Controls.Add(this.button2);
            this.groupBox8.Controls.Add(this.label134);
            this.groupBox8.Controls.Add(this.label133);
            this.groupBox8.Controls.Add(this.txt_Ana_SelfWeight);
            this.groupBox8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(4, 868);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(384, 109);
            this.groupBox8.TabIndex = 182;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Dead Load + Super Imposed Dead Load [DL + SIDL]";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(7, 28);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(75, 13);
            this.label136.TabIndex = 15;
            this.label136.Text = "Selft Weight";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(351, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txt_Ana_FPLL
            // 
            this.txt_Ana_FPLL.Location = new System.Drawing.Point(270, 75);
            this.txt_Ana_FPLL.Name = "txt_Ana_FPLL";
            this.txt_Ana_FPLL.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_FPLL.TabIndex = 6;
            this.txt_Ana_FPLL.Text = "0.098";
            this.txt_Ana_FPLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(7, 78);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(111, 13);
            this.label138.TabIndex = 15;
            this.label138.Text = "FootPath Liveload ";
            // 
            // txt_Ana_SIDL
            // 
            this.txt_Ana_SIDL.Location = new System.Drawing.Point(270, 48);
            this.txt_Ana_SIDL.Name = "txt_Ana_SIDL";
            this.txt_Ana_SIDL.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_SIDL.TabIndex = 6;
            this.txt_Ana_SIDL.Text = "0.169";
            this.txt_Ana_SIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(7, 51);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(151, 13);
            this.label135.TabIndex = 15;
            this.label135.Text = "Superimposed Lead Load";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(334, 79);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(33, 13);
            this.label137.TabIndex = 17;
            this.label137.Text = "K/FT";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(441, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 44;
            this.button2.Text = "Remove All";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(334, 52);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(33, 13);
            this.label134.TabIndex = 17;
            this.label134.Text = "K/FT";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(334, 28);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(33, 13);
            this.label133.TabIndex = 17;
            this.label133.Text = "K/FT";
            // 
            // txt_Ana_SelfWeight
            // 
            this.txt_Ana_SelfWeight.Location = new System.Drawing.Point(270, 25);
            this.txt_Ana_SelfWeight.Name = "txt_Ana_SelfWeight";
            this.txt_Ana_SelfWeight.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_SelfWeight.TabIndex = 6;
            this.txt_Ana_SelfWeight.Text = "0.675";
            this.txt_Ana_SelfWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.label94);
            this.groupBox22.Controls.Add(this.label104);
            this.groupBox22.Controls.Add(this.label89);
            this.groupBox22.Controls.Add(this.txt_Ana_Strand_Ep);
            this.groupBox22.Controls.Add(this.label108);
            this.groupBox22.Controls.Add(this.txt_Ana_Strand_Fpy);
            this.groupBox22.Controls.Add(this.label106);
            this.groupBox22.Controls.Add(this.label107);
            this.groupBox22.Controls.Add(this.txt_Ana_Strand_Fpu);
            this.groupBox22.Controls.Add(this.label105);
            this.groupBox22.Controls.Add(this.label91);
            this.groupBox22.Controls.Add(this.label92);
            this.groupBox22.Controls.Add(this.txt_Ana_Strand_Diameter);
            this.groupBox22.Controls.Add(this.label95);
            this.groupBox22.Controls.Add(this.txt_Ana_Strand_Area);
            this.groupBox22.Controls.Add(this.label93);
            this.groupBox22.Location = new System.Drawing.Point(400, 1250);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(514, 162);
            this.groupBox22.TabIndex = 184;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Prestressing Strand";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(6, 20);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(210, 13);
            this.label94.TabIndex = 15;
            this.label94.Text = "Low relaxation prestressing strands";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(7, 39);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(104, 13);
            this.label104.TabIndex = 15;
            this.label104.Text = "Strand diameter ";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(7, 62);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(111, 13);
            this.label89.TabIndex = 15;
            this.label89.Text = "Strand Area [Aps]";
            // 
            // txt_Ana_Strand_Ep
            // 
            this.txt_Ana_Strand_Ep.Location = new System.Drawing.Point(379, 132);
            this.txt_Ana_Strand_Ep.Name = "txt_Ana_Strand_Ep";
            this.txt_Ana_Strand_Ep.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Strand_Ep.TabIndex = 6;
            this.txt_Ana_Strand_Ep.Text = "28500";
            this.txt_Ana_Strand_Ep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(7, 135);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(135, 13);
            this.label108.TabIndex = 15;
            this.label108.Text = "Modulus Elasticity [Ep]";
            // 
            // txt_Ana_Strand_Fpy
            // 
            this.txt_Ana_Strand_Fpy.Location = new System.Drawing.Point(379, 109);
            this.txt_Ana_Strand_Fpy.Name = "txt_Ana_Strand_Fpy";
            this.txt_Ana_Strand_Fpy.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Strand_Fpy.TabIndex = 6;
            this.txt_Ana_Strand_Fpy.Text = "243";
            this.txt_Ana_Strand_Fpy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(7, 112);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(119, 13);
            this.label106.TabIndex = 15;
            this.label106.Text = "Yield Strength [fpy]";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(443, 136);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(23, 13);
            this.label107.TabIndex = 17;
            this.label107.Text = "ksi";
            // 
            // txt_Ana_Strand_Fpu
            // 
            this.txt_Ana_Strand_Fpu.Location = new System.Drawing.Point(379, 82);
            this.txt_Ana_Strand_Fpu.Name = "txt_Ana_Strand_Fpu";
            this.txt_Ana_Strand_Fpu.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Strand_Fpu.TabIndex = 6;
            this.txt_Ana_Strand_Fpu.Text = "270";
            this.txt_Ana_Strand_Fpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(443, 113);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(23, 13);
            this.label105.TabIndex = 17;
            this.label105.Text = "ksi";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(7, 85);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(131, 13);
            this.label91.TabIndex = 15;
            this.label91.Text = "Tensile Strength [fpu]";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(443, 86);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(23, 13);
            this.label92.TabIndex = 17;
            this.label92.Text = "ksi";
            // 
            // txt_Ana_Strand_Diameter
            // 
            this.txt_Ana_Strand_Diameter.Location = new System.Drawing.Point(379, 36);
            this.txt_Ana_Strand_Diameter.Name = "txt_Ana_Strand_Diameter";
            this.txt_Ana_Strand_Diameter.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Strand_Diameter.TabIndex = 6;
            this.txt_Ana_Strand_Diameter.Text = "0.6";
            this.txt_Ana_Strand_Diameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(443, 39);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(17, 13);
            this.label95.TabIndex = 17;
            this.label95.Text = "in";
            // 
            // txt_Ana_Strand_Area
            // 
            this.txt_Ana_Strand_Area.Location = new System.Drawing.Point(379, 59);
            this.txt_Ana_Strand_Area.Name = "txt_Ana_Strand_Area";
            this.txt_Ana_Strand_Area.Size = new System.Drawing.Size(58, 21);
            this.txt_Ana_Strand_Area.TabIndex = 6;
            this.txt_Ana_Strand_Area.Text = "0.217";
            this.txt_Ana_Strand_Area.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(443, 62);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(33, 13);
            this.label93.TabIndex = 17;
            this.label93.Text = "in^2";
            // 
            // label228
            // 
            this.label228.AutoSize = true;
            this.label228.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label228.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label228.ForeColor = System.Drawing.Color.Green;
            this.label228.Location = new System.Drawing.Point(448, 13);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(135, 18);
            this.label228.TabIndex = 125;
            this.label228.Text = "All User Input Data";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label84);
            this.groupBox20.Controls.Add(this.label38);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Top_Cover);
            this.groupBox20.Controls.Add(this.label42);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Bottom_Cover);
            this.groupBox20.Controls.Add(this.label43);
            this.groupBox20.Controls.Add(this.label44);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Wearing_Surface);
            this.groupBox20.Controls.Add(this.txt_Ana_Deckslab_Thickness);
            this.groupBox20.Controls.Add(this.label45);
            this.groupBox20.Controls.Add(this.label46);
            this.groupBox20.Controls.Add(this.label86);
            this.groupBox20.Controls.Add(this.label87);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Overhang);
            this.groupBox20.Controls.Add(this.label85);
            this.groupBox20.Location = new System.Drawing.Point(4, 988);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(384, 145);
            this.groupBox20.TabIndex = 185;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Concrete Deck Slab Minimum Requirements";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 25);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(94, 13);
            this.label84.TabIndex = 15;
            this.label84.Text = "Deck overhang";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 120);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(99, 13);
            this.label38.TabIndex = 15;
            this.label38.Text = "Wearing surface";
            // 
            // txt_Ana_Deck_Top_Cover
            // 
            this.txt_Ana_Deck_Top_Cover.Location = new System.Drawing.Point(266, 64);
            this.txt_Ana_Deck_Top_Cover.Name = "txt_Ana_Deck_Top_Cover";
            this.txt_Ana_Deck_Top_Cover.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Deck_Top_Cover.TabIndex = 6;
            this.txt_Ana_Deck_Top_Cover.Text = "2.5";
            this.txt_Ana_Deck_Top_Cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(327, 115);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(17, 13);
            this.label42.TabIndex = 17;
            this.label42.Text = "in";
            // 
            // txt_Ana_Deck_Bottom_Cover
            // 
            this.txt_Ana_Deck_Bottom_Cover.Location = new System.Drawing.Point(266, 88);
            this.txt_Ana_Deck_Bottom_Cover.Name = "txt_Ana_Deck_Bottom_Cover";
            this.txt_Ana_Deck_Bottom_Cover.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Deck_Bottom_Cover.TabIndex = 6;
            this.txt_Ana_Deck_Bottom_Cover.Text = "1.0";
            this.txt_Ana_Deck_Bottom_Cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 96);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(137, 13);
            this.label43.TabIndex = 15;
            this.label43.Text = "Bottom concrete cover";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(327, 91);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(17, 13);
            this.label44.TabIndex = 17;
            this.label44.Text = "in";
            // 
            // txt_Ana_Deck_Wearing_Surface
            // 
            this.txt_Ana_Deck_Wearing_Surface.Location = new System.Drawing.Point(266, 112);
            this.txt_Ana_Deck_Wearing_Surface.Name = "txt_Ana_Deck_Wearing_Surface";
            this.txt_Ana_Deck_Wearing_Surface.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Deck_Wearing_Surface.TabIndex = 6;
            this.txt_Ana_Deck_Wearing_Surface.Text = "0.5";
            this.txt_Ana_Deck_Wearing_Surface.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_Deckslab_Thickness
            // 
            this.txt_Ana_Deckslab_Thickness.Location = new System.Drawing.Point(266, 40);
            this.txt_Ana_Deckslab_Thickness.Name = "txt_Ana_Deckslab_Thickness";
            this.txt_Ana_Deckslab_Thickness.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Deckslab_Thickness.TabIndex = 6;
            this.txt_Ana_Deckslab_Thickness.Text = "8.0";
            this.txt_Ana_Deckslab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(327, 67);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(17, 13);
            this.label45.TabIndex = 17;
            this.label45.Text = "in";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 72);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(116, 13);
            this.label46.TabIndex = 15;
            this.label46.Text = "Top concrete cover";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(6, 48);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(89, 13);
            this.label86.TabIndex = 15;
            this.label86.Text = "Slab thickness";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(327, 43);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(17, 13);
            this.label87.TabIndex = 17;
            this.label87.Text = "in";
            // 
            // txt_Ana_Deck_Overhang
            // 
            this.txt_Ana_Deck_Overhang.Location = new System.Drawing.Point(266, 17);
            this.txt_Ana_Deck_Overhang.Name = "txt_Ana_Deck_Overhang";
            this.txt_Ana_Deck_Overhang.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_Deck_Overhang.TabIndex = 6;
            this.txt_Ana_Deck_Overhang.Text = "2.63";
            this.txt_Ana_Deck_Overhang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(329, 20);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(15, 13);
            this.label85.TabIndex = 17;
            this.label85.Text = "ft";
            // 
            // tab_cs_diagram1
            // 
            this.tab_cs_diagram1.Controls.Add(this.groupBox19);
            this.tab_cs_diagram1.Controls.Add(this.groupBox21);
            this.tab_cs_diagram1.Controls.Add(this.pictureBox8);
            this.tab_cs_diagram1.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram1.Name = "tab_cs_diagram1";
            this.tab_cs_diagram1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram1.Size = new System.Drawing.Size(955, 634);
            this.tab_cs_diagram1.TabIndex = 8;
            this.tab_cs_diagram1.Text = "Cross Section Details Type 1";
            this.tab_cs_diagram1.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.txt_box_cs2_IZZ);
            this.groupBox19.Controls.Add(this.label24);
            this.groupBox19.Controls.Add(this.label25);
            this.groupBox19.Controls.Add(this.txt_box_cs2_IYY);
            this.groupBox19.Controls.Add(this.label90);
            this.groupBox19.Controls.Add(this.txt_box_cs2_IXX);
            this.groupBox19.Controls.Add(this.label96);
            this.groupBox19.Controls.Add(this.label97);
            this.groupBox19.Controls.Add(this.txt_box_cs2_AX);
            this.groupBox19.Controls.Add(this.label100);
            this.groupBox19.Controls.Add(this.label102);
            this.groupBox19.Controls.Add(this.label103);
            this.groupBox19.Location = new System.Drawing.Point(391, 332);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(515, 150);
            this.groupBox19.TabIndex = 5;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Results";
            // 
            // txt_box_cs2_IZZ
            // 
            this.txt_box_cs2_IZZ.Location = new System.Drawing.Point(233, 112);
            this.txt_box_cs2_IZZ.Name = "txt_box_cs2_IZZ";
            this.txt_box_cs2_IZZ.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_IZZ.TabIndex = 4;
            this.txt_box_cs2_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(375, 115);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(33, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "in^4";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 115);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(221, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Moment of Inertia about Z-Axis [IZZ]";
            // 
            // txt_box_cs2_IYY
            // 
            this.txt_box_cs2_IYY.Location = new System.Drawing.Point(233, 83);
            this.txt_box_cs2_IYY.Name = "txt_box_cs2_IYY";
            this.txt_box_cs2_IYY.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_IYY.TabIndex = 1;
            this.txt_box_cs2_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_IYY.TextChanged += new System.EventHandler(this.txt_box_cs2_AX_TextChanged);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(375, 86);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(33, 13);
            this.label90.TabIndex = 0;
            this.label90.Text = "in^4";
            // 
            // txt_box_cs2_IXX
            // 
            this.txt_box_cs2_IXX.Location = new System.Drawing.Point(233, 56);
            this.txt_box_cs2_IXX.Name = "txt_box_cs2_IXX";
            this.txt_box_cs2_IXX.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_IXX.TabIndex = 1;
            this.txt_box_cs2_IXX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_IXX.TextChanged += new System.EventHandler(this.txt_box_cs2_AX_TextChanged);
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(375, 59);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(33, 13);
            this.label96.TabIndex = 0;
            this.label96.Text = "in^4";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(6, 86);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(217, 13);
            this.label97.TabIndex = 0;
            this.label97.Text = "Moment of Inertia about Y-Axis [IYY]";
            // 
            // txt_box_cs2_AX
            // 
            this.txt_box_cs2_AX.Location = new System.Drawing.Point(233, 29);
            this.txt_box_cs2_AX.Name = "txt_box_cs2_AX";
            this.txt_box_cs2_AX.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_AX.TabIndex = 1;
            this.txt_box_cs2_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_AX.TextChanged += new System.EventHandler(this.txt_box_cs2_AX_TextChanged);
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(6, 59);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(221, 13);
            this.label100.TabIndex = 0;
            this.label100.Text = "Moment of Inertia about X-Axis [IXX]";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(375, 32);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(33, 13);
            this.label102.TabIndex = 0;
            this.label102.Text = "in^2";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(6, 32);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(64, 13);
            this.label103.TabIndex = 0;
            this.label103.Text = "Area [AX]";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.txt_box_cs2_b8);
            this.groupBox21.Controls.Add(this.label47);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b7);
            this.groupBox21.Controls.Add(this.label48);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b6);
            this.groupBox21.Controls.Add(this.label69);
            this.groupBox21.Controls.Add(this.txt_box_cs2_d5);
            this.groupBox21.Controls.Add(this.label77);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b5);
            this.groupBox21.Controls.Add(this.txt_box_cs2_d4);
            this.groupBox21.Controls.Add(this.label70);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b4);
            this.groupBox21.Controls.Add(this.label76);
            this.groupBox21.Controls.Add(this.label71);
            this.groupBox21.Controls.Add(this.label72);
            this.groupBox21.Controls.Add(this.txt_box_cs2_d3);
            this.groupBox21.Controls.Add(this.label73);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b3);
            this.groupBox21.Controls.Add(this.label74);
            this.groupBox21.Controls.Add(this.label78);
            this.groupBox21.Controls.Add(this.label79);
            this.groupBox21.Controls.Add(this.label88);
            this.groupBox21.Controls.Add(this.txt_box_cs2_d2);
            this.groupBox21.Controls.Add(this.label98);
            this.groupBox21.Controls.Add(this.label99);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b2);
            this.groupBox21.Controls.Add(this.label101);
            this.groupBox21.Controls.Add(this.label123);
            this.groupBox21.Controls.Add(this.label124);
            this.groupBox21.Controls.Add(this.label125);
            this.groupBox21.Controls.Add(this.txt_box_cs2_d1);
            this.groupBox21.Controls.Add(this.label126);
            this.groupBox21.Controls.Add(this.label127);
            this.groupBox21.Controls.Add(this.txt_box_cs2_cell_nos);
            this.groupBox21.Controls.Add(this.txt_box_cs2_b1);
            this.groupBox21.Controls.Add(this.label128);
            this.groupBox21.Controls.Add(this.label129);
            this.groupBox21.Controls.Add(this.label130);
            this.groupBox21.Controls.Add(this.label131);
            this.groupBox21.Controls.Add(this.label132);
            this.groupBox21.Controls.Add(this.label139);
            this.groupBox21.Controls.Add(this.label140);
            this.groupBox21.Location = new System.Drawing.Point(49, 332);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(322, 289);
            this.groupBox21.TabIndex = 4;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Dimension Inputs";
            // 
            // txt_box_cs2_b8
            // 
            this.txt_box_cs2_b8.Location = new System.Drawing.Point(42, 242);
            this.txt_box_cs2_b8.Name = "txt_box_cs2_b8";
            this.txt_box_cs2_b8.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b8.TabIndex = 1;
            this.txt_box_cs2_b8.Text = "436";
            this.txt_box_cs2_b8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b8.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(107, 245);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(17, 13);
            this.label47.TabIndex = 0;
            this.label47.Text = "in";
            // 
            // txt_box_cs2_b7
            // 
            this.txt_box_cs2_b7.Location = new System.Drawing.Point(42, 215);
            this.txt_box_cs2_b7.Name = "txt_box_cs2_b7";
            this.txt_box_cs2_b7.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b7.TabIndex = 1;
            this.txt_box_cs2_b7.Text = "21.5";
            this.txt_box_cs2_b7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b7.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(107, 218);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 13);
            this.label48.TabIndex = 0;
            this.label48.Text = "in";
            // 
            // txt_box_cs2_b6
            // 
            this.txt_box_cs2_b6.Location = new System.Drawing.Point(42, 188);
            this.txt_box_cs2_b6.Name = "txt_box_cs2_b6";
            this.txt_box_cs2_b6.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b6.TabIndex = 1;
            this.txt_box_cs2_b6.Text = "31.5";
            this.txt_box_cs2_b6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b6.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(107, 191);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(17, 13);
            this.label69.TabIndex = 0;
            this.label69.Text = "in";
            // 
            // txt_box_cs2_d5
            // 
            this.txt_box_cs2_d5.Location = new System.Drawing.Point(207, 161);
            this.txt_box_cs2_d5.Name = "txt_box_cs2_d5";
            this.txt_box_cs2_d5.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d5.TabIndex = 1;
            this.txt_box_cs2_d5.Text = "6.0";
            this.txt_box_cs2_d5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d5.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(272, 164);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(17, 13);
            this.label77.TabIndex = 0;
            this.label77.Text = "in";
            // 
            // txt_box_cs2_b5
            // 
            this.txt_box_cs2_b5.Location = new System.Drawing.Point(42, 161);
            this.txt_box_cs2_b5.Name = "txt_box_cs2_b5";
            this.txt_box_cs2_b5.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b5.TabIndex = 1;
            this.txt_box_cs2_b5.Text = "12";
            this.txt_box_cs2_b5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b5.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // txt_box_cs2_d4
            // 
            this.txt_box_cs2_d4.Location = new System.Drawing.Point(207, 134);
            this.txt_box_cs2_d4.Name = "txt_box_cs2_d4";
            this.txt_box_cs2_d4.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d4.TabIndex = 1;
            this.txt_box_cs2_d4.Text = "66";
            this.txt_box_cs2_d4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d4.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(107, 164);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(17, 13);
            this.label70.TabIndex = 0;
            this.label70.Text = "in";
            // 
            // txt_box_cs2_b4
            // 
            this.txt_box_cs2_b4.Location = new System.Drawing.Point(42, 134);
            this.txt_box_cs2_b4.Name = "txt_box_cs2_b4";
            this.txt_box_cs2_b4.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b4.TabIndex = 1;
            this.txt_box_cs2_b4.Text = "12";
            this.txt_box_cs2_b4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b4.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(272, 137);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(17, 13);
            this.label76.TabIndex = 0;
            this.label76.Text = "in";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(5, 245);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(31, 13);
            this.label71.TabIndex = 0;
            this.label71.Text = "[b8]";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(107, 137);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(17, 13);
            this.label72.TabIndex = 0;
            this.label72.Text = "in";
            // 
            // txt_box_cs2_d3
            // 
            this.txt_box_cs2_d3.Location = new System.Drawing.Point(207, 107);
            this.txt_box_cs2_d3.Name = "txt_box_cs2_d3";
            this.txt_box_cs2_d3.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d3.TabIndex = 1;
            this.txt_box_cs2_d3.Text = "8.0";
            this.txt_box_cs2_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d3.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(5, 218);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(31, 13);
            this.label73.TabIndex = 0;
            this.label73.Text = "[b7]";
            // 
            // txt_box_cs2_b3
            // 
            this.txt_box_cs2_b3.Location = new System.Drawing.Point(42, 107);
            this.txt_box_cs2_b3.Name = "txt_box_cs2_b3";
            this.txt_box_cs2_b3.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b3.TabIndex = 1;
            this.txt_box_cs2_b3.Text = "465";
            this.txt_box_cs2_b3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b3.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(272, 110);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(17, 13);
            this.label74.TabIndex = 0;
            this.label74.Text = "in";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(5, 191);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(31, 13);
            this.label78.TabIndex = 0;
            this.label78.Text = "[b6]";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(170, 164);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(31, 13);
            this.label79.TabIndex = 0;
            this.label79.Text = "[d5]";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(107, 110);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(17, 13);
            this.label88.TabIndex = 0;
            this.label88.Text = "in";
            // 
            // txt_box_cs2_d2
            // 
            this.txt_box_cs2_d2.Location = new System.Drawing.Point(207, 80);
            this.txt_box_cs2_d2.Name = "txt_box_cs2_d2";
            this.txt_box_cs2_d2.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d2.TabIndex = 1;
            this.txt_box_cs2_d2.Text = "12.0";
            this.txt_box_cs2_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d2.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(5, 164);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(31, 13);
            this.label98.TabIndex = 0;
            this.label98.Text = "[b5]";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(170, 137);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(31, 13);
            this.label99.TabIndex = 0;
            this.label99.Text = "[d4]";
            // 
            // txt_box_cs2_b2
            // 
            this.txt_box_cs2_b2.Location = new System.Drawing.Point(42, 80);
            this.txt_box_cs2_b2.Name = "txt_box_cs2_b2";
            this.txt_box_cs2_b2.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b2.TabIndex = 1;
            this.txt_box_cs2_b2.Text = "38.5";
            this.txt_box_cs2_b2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b2.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(272, 83);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(17, 13);
            this.label101.TabIndex = 0;
            this.label101.Text = "in";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(5, 137);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(31, 13);
            this.label123.TabIndex = 0;
            this.label123.Text = "[b4]";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(170, 110);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(31, 13);
            this.label124.TabIndex = 0;
            this.label124.Text = "[d3]";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(107, 83);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(17, 13);
            this.label125.TabIndex = 0;
            this.label125.Text = "in";
            // 
            // txt_box_cs2_d1
            // 
            this.txt_box_cs2_d1.Location = new System.Drawing.Point(207, 53);
            this.txt_box_cs2_d1.Name = "txt_box_cs2_d1";
            this.txt_box_cs2_d1.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d1.TabIndex = 1;
            this.txt_box_cs2_d1.Text = "9.0";
            this.txt_box_cs2_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d1.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(5, 110);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(31, 13);
            this.label126.TabIndex = 0;
            this.label126.Text = "[b3]";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(170, 83);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(31, 13);
            this.label127.TabIndex = 0;
            this.label127.Text = "[d2]";
            // 
            // txt_box_cs2_cell_nos
            // 
            this.txt_box_cs2_cell_nos.Location = new System.Drawing.Point(207, 20);
            this.txt_box_cs2_cell_nos.Name = "txt_box_cs2_cell_nos";
            this.txt_box_cs2_cell_nos.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_cell_nos.TabIndex = 1;
            this.txt_box_cs2_cell_nos.Text = "3";
            this.txt_box_cs2_cell_nos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_cell_nos.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // txt_box_cs2_b1
            // 
            this.txt_box_cs2_b1.Location = new System.Drawing.Point(42, 53);
            this.txt_box_cs2_b1.Name = "txt_box_cs2_b1";
            this.txt_box_cs2_b1.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b1.TabIndex = 1;
            this.txt_box_cs2_b1.Text = "542";
            this.txt_box_cs2_b1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b1.TextChanged += new System.EventHandler(this.txt_box_cs2_cell_nos_TextChanged);
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(272, 56);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(17, 13);
            this.label128.TabIndex = 0;
            this.label128.Text = "in";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(5, 83);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(31, 13);
            this.label129.TabIndex = 0;
            this.label129.Text = "[b2]";
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(272, 23);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(27, 13);
            this.label130.TabIndex = 0;
            this.label130.Text = "nos";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(170, 56);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(31, 13);
            this.label131.TabIndex = 0;
            this.label131.Text = "[d1]";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(5, 23);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(136, 13);
            this.label132.TabIndex = 0;
            this.label132.Text = "Number of Cells inside";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(107, 56);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(17, 13);
            this.label139.TabIndex = 0;
            this.label139.Text = "in";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(5, 56);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(31, 13);
            this.label140.TabIndex = 0;
            this.label140.Text = "[b1]";
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackgroundImage = global::LimitStateMethod.Properties.Resources.AASHTO_BOX_CROSSSECTION;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox8.Location = new System.Drawing.Point(175, 14);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(694, 312);
            this.pictureBox8.TabIndex = 3;
            this.pictureBox8.TabStop = false;
            // 
            // tab_cs_diagram2
            // 
            this.tab_cs_diagram2.Controls.Add(this.tabControl4);
            this.tab_cs_diagram2.ForeColor = System.Drawing.Color.Blue;
            this.tab_cs_diagram2.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram2.Name = "tab_cs_diagram2";
            this.tab_cs_diagram2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram2.Size = new System.Drawing.Size(955, 634);
            this.tab_cs_diagram2.TabIndex = 2;
            this.tab_cs_diagram2.Text = "Cross Section Details Type 2";
            this.tab_cs_diagram2.UseVisualStyleBackColor = true;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage16);
            this.tabControl4.Controls.Add(this.tabPage11);
            this.tabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl4.Location = new System.Drawing.Point(3, 3);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(949, 628);
            this.tabControl4.TabIndex = 126;
            // 
            // tabPage16
            // 
            this.tabPage16.AutoScroll = true;
            this.tabPage16.Controls.Add(this.panel6);
            this.tabPage16.Controls.Add(this.pictureBox1);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(941, 602);
            this.tabPage16.TabIndex = 1;
            this.tabPage16.Text = "Data Inputs";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.Controls.Add(this.groupBox32);
            this.panel6.Location = new System.Drawing.Point(184, 311);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(569, 268);
            this.panel6.TabIndex = 3;
            // 
            // groupBox32
            // 
            this.groupBox32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox32.Controls.Add(this.dgv_seg_tab3_1);
            this.groupBox32.Controls.Add(this.label177);
            this.groupBox32.Controls.Add(this.txt_tab3_L_8);
            this.groupBox32.Controls.Add(this.txt_tab3_L_2);
            this.groupBox32.Controls.Add(this.txt_tab3_3L_8);
            this.groupBox32.Controls.Add(this.txt_tab3_L_4);
            this.groupBox32.Controls.Add(this.txt_tab3_support);
            this.groupBox32.Controls.Add(this.txt_tab3_d);
            this.groupBox32.Controls.Add(this.pictureBox2);
            this.groupBox32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox32.Location = new System.Drawing.Point(3, 3);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(541, 457);
            this.groupBox32.TabIndex = 1;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "Cross Section Data Input (Refer to \"Open Diagram for Cross Section Data Input\" bu" +
    "tton)";
            // 
            // dgv_seg_tab3_1
            // 
            this.dgv_seg_tab3_1.AllowUserToAddRows = false;
            this.dgv_seg_tab3_1.AllowUserToDeleteRows = false;
            this.dgv_seg_tab3_1.AllowUserToResizeColumns = false;
            this.dgv_seg_tab3_1.AllowUserToResizeRows = false;
            this.dgv_seg_tab3_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_seg_tab3_1.ColumnHeadersVisible = false;
            this.dgv_seg_tab3_1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgv_seg_tab3_1.Location = new System.Drawing.Point(63, 78);
            this.dgv_seg_tab3_1.Name = "dgv_seg_tab3_1";
            this.dgv_seg_tab3_1.RowHeadersVisible = false;
            this.dgv_seg_tab3_1.RowHeadersWidth = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_seg_tab3_1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_seg_tab3_1.Size = new System.Drawing.Size(429, 353);
            this.dgv_seg_tab3_1.TabIndex = 0;
            this.dgv_seg_tab3_1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_seg_tab3_CellValueChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 51;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 64;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Width = 61;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Width = 61;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.Width = 62;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Width = 63;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.Width = 63;
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.ForeColor = System.Drawing.Color.Blue;
            this.label177.Location = new System.Drawing.Point(64, 439);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(376, 13);
            this.label177.TabIndex = 3;
            this.label177.Text = "Calculated values:    (Cells shaded above are calculated values)";
            // 
            // txt_tab3_L_8
            // 
            this.txt_tab3_L_8.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_L_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_L_8.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_L_8.Location = new System.Drawing.Point(241, 58);
            this.txt_tab3_L_8.Name = "txt_tab3_L_8";
            this.txt_tab3_L_8.Size = new System.Drawing.Size(58, 18);
            this.txt_tab3_L_8.TabIndex = 4;
            this.txt_tab3_L_8.Text = "0.0";
            this.txt_tab3_L_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_tab3_L_2
            // 
            this.txt_tab3_L_2.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_L_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_L_2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_L_2.Location = new System.Drawing.Point(428, 58);
            this.txt_tab3_L_2.Name = "txt_tab3_L_2";
            this.txt_tab3_L_2.Size = new System.Drawing.Size(59, 18);
            this.txt_tab3_L_2.TabIndex = 3;
            this.txt_tab3_L_2.Text = "0.0";
            this.txt_tab3_L_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_tab3_3L_8
            // 
            this.txt_tab3_3L_8.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_3L_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_3L_8.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_3L_8.Location = new System.Drawing.Point(365, 58);
            this.txt_tab3_3L_8.Name = "txt_tab3_3L_8";
            this.txt_tab3_3L_8.Size = new System.Drawing.Size(60, 18);
            this.txt_tab3_3L_8.TabIndex = 3;
            this.txt_tab3_3L_8.Text = "0.0";
            this.txt_tab3_3L_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_tab3_L_4
            // 
            this.txt_tab3_L_4.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_L_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_L_4.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_L_4.Location = new System.Drawing.Point(301, 58);
            this.txt_tab3_L_4.Name = "txt_tab3_L_4";
            this.txt_tab3_L_4.Size = new System.Drawing.Size(61, 18);
            this.txt_tab3_L_4.TabIndex = 3;
            this.txt_tab3_L_4.Text = "0.0";
            this.txt_tab3_L_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_tab3_support
            // 
            this.txt_tab3_support.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_support.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_support.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_support.Location = new System.Drawing.Point(117, 58);
            this.txt_tab3_support.Name = "txt_tab3_support";
            this.txt_tab3_support.Size = new System.Drawing.Size(60, 18);
            this.txt_tab3_support.TabIndex = 1;
            this.txt_tab3_support.Text = "0.0";
            this.txt_tab3_support.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_tab3_d
            // 
            this.txt_tab3_d.BackColor = System.Drawing.Color.GreenYellow;
            this.txt_tab3_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab3_d.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tab3_d.Location = new System.Drawing.Point(179, 58);
            this.txt_tab3_d.Name = "txt_tab3_d";
            this.txt_tab3_d.Size = new System.Drawing.Size(59, 18);
            this.txt_tab3_d.TabIndex = 1;
            this.txt_tab3_d.Text = "0.0";
            this.txt_tab3_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::LimitStateMethod.Properties.Resources.BOX_tab1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Location = new System.Drawing.Point(2, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(500, 425);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LimitStateMethod.Properties.Resources.BOX_1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(129, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(686, 299);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.groupBox26);
            this.tabPage11.Controls.Add(this.btn_Show_Section_Resulf);
            this.tabPage11.Controls.Add(this.rtb_sections);
            this.tabPage11.Controls.Add(this.label176);
            this.tabPage11.Controls.Add(this.label226);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(941, 602);
            this.tabPage11.TabIndex = 0;
            this.tabPage11.Text = "Results";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.pictureBox4);
            this.groupBox26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox26.Location = new System.Drawing.Point(110, 6);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(704, 324);
            this.groupBox26.TabIndex = 2;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Section Properties of various parts in the Cross Section at relevant Sections:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::LimitStateMethod.Properties.Resources.BOX_1;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Location = new System.Drawing.Point(6, 16);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(686, 299);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // btn_Show_Section_Resulf
            // 
            this.btn_Show_Section_Resulf.Location = new System.Drawing.Point(628, 340);
            this.btn_Show_Section_Resulf.Name = "btn_Show_Section_Resulf";
            this.btn_Show_Section_Resulf.Size = new System.Drawing.Size(148, 32);
            this.btn_Show_Section_Resulf.TabIndex = 69;
            this.btn_Show_Section_Resulf.Text = "Show Results";
            this.btn_Show_Section_Resulf.UseVisualStyleBackColor = true;
            this.btn_Show_Section_Resulf.Click += new System.EventHandler(this.btn_Show_Section_Result_Click);
            // 
            // rtb_sections
            // 
            this.rtb_sections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_sections.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_sections.Location = new System.Drawing.Point(19, 378);
            this.rtb_sections.Name = "rtb_sections";
            this.rtb_sections.ReadOnly = true;
            this.rtb_sections.Size = new System.Drawing.Size(886, 218);
            this.rtb_sections.TabIndex = 3;
            this.rtb_sections.Text = "";
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label176.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.Red;
            this.label176.Location = new System.Drawing.Point(418, 346);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(189, 20);
            this.label176.TabIndex = 125;
            this.label176.Text = "All Calculated Values ";
            // 
            // label226
            // 
            this.label226.AutoSize = true;
            this.label226.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label226.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label226.ForeColor = System.Drawing.Color.Green;
            this.label226.Location = new System.Drawing.Point(165, 346);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(229, 20);
            this.label226.TabIndex = 124;
            this.label226.Text = "No User Input in this page";
            // 
            // tab_cs_results
            // 
            this.tab_cs_results.Controls.Add(this.panel8);
            this.tab_cs_results.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_results.Name = "tab_cs_results";
            this.tab_cs_results.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_results.Size = new System.Drawing.Size(955, 634);
            this.tab_cs_results.TabIndex = 9;
            this.tab_cs_results.Text = "Cross Section Results";
            this.tab_cs_results.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.txt_out_IZZ);
            this.panel8.Controls.Add(this.txt_inn_IZZ);
            this.panel8.Controls.Add(this.txt_cen_IZZ);
            this.panel8.Controls.Add(this.label444);
            this.panel8.Controls.Add(this.label354);
            this.panel8.Controls.Add(this.txt_tot_IZZ);
            this.panel8.Controls.Add(this.label350);
            this.panel8.Controls.Add(this.txt_out_IYY);
            this.panel8.Controls.Add(this.txt_inn_IYY);
            this.panel8.Controls.Add(this.label144);
            this.panel8.Controls.Add(this.label443);
            this.panel8.Controls.Add(this.txt_cen_IYY);
            this.panel8.Controls.Add(this.label353);
            this.panel8.Controls.Add(this.txt_tot_IYY);
            this.panel8.Controls.Add(this.txt_out_IXX);
            this.panel8.Controls.Add(this.label349);
            this.panel8.Controls.Add(this.txt_inn_IXX);
            this.panel8.Controls.Add(this.label279);
            this.panel8.Controls.Add(this.label155);
            this.panel8.Controls.Add(this.txt_cen_IXX);
            this.panel8.Controls.Add(this.label352);
            this.panel8.Controls.Add(this.txt_tot_IXX);
            this.panel8.Controls.Add(this.txt_out_pcnt);
            this.panel8.Controls.Add(this.label348);
            this.panel8.Controls.Add(this.txt_inn_pcnt);
            this.panel8.Controls.Add(this.txt_out_AX);
            this.panel8.Controls.Add(this.label162);
            this.panel8.Controls.Add(this.txt_inn_AX);
            this.panel8.Controls.Add(this.txt_cen_pcnt);
            this.panel8.Controls.Add(this.label163);
            this.panel8.Controls.Add(this.txt_cen_AX);
            this.panel8.Controls.Add(this.label339);
            this.panel8.Controls.Add(this.label165);
            this.panel8.Controls.Add(this.txt_tot_AX);
            this.panel8.Controls.Add(this.label166);
            this.panel8.Controls.Add(this.label338);
            this.panel8.Controls.Add(this.label351);
            this.panel8.Controls.Add(this.label505);
            this.panel8.Controls.Add(this.label502);
            this.panel8.Controls.Add(this.label501);
            this.panel8.Controls.Add(this.label336);
            this.panel8.Controls.Add(this.label399);
            this.panel8.Controls.Add(this.label347);
            this.panel8.Controls.Add(this.label342);
            this.panel8.Controls.Add(this.label281);
            this.panel8.Controls.Add(this.label167);
            this.panel8.Controls.Add(this.label335);
            this.panel8.Controls.Add(this.label341);
            this.panel8.Controls.Add(this.label346);
            this.panel8.Controls.Add(this.label168);
            this.panel8.Controls.Add(this.label169);
            this.panel8.Controls.Add(this.label340);
            this.panel8.Controls.Add(this.label345);
            this.panel8.Controls.Add(this.label170);
            this.panel8.Controls.Add(this.label171);
            this.panel8.Controls.Add(this.label337);
            this.panel8.Controls.Add(this.label344);
            this.panel8.Controls.Add(this.label172);
            this.panel8.Controls.Add(this.label343);
            this.panel8.Controls.Add(this.label277);
            this.panel8.Controls.Add(this.label498);
            this.panel8.Controls.Add(this.label175);
            this.panel8.ForeColor = System.Drawing.Color.Black;
            this.panel8.Location = new System.Drawing.Point(18, 21);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(883, 359);
            this.panel8.TabIndex = 127;
            // 
            // txt_out_IZZ
            // 
            this.txt_out_IZZ.Location = new System.Drawing.Point(699, 293);
            this.txt_out_IZZ.Name = "txt_out_IZZ";
            this.txt_out_IZZ.ReadOnly = true;
            this.txt_out_IZZ.Size = new System.Drawing.Size(84, 21);
            this.txt_out_IZZ.TabIndex = 2;
            this.txt_out_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_inn_IZZ
            // 
            this.txt_inn_IZZ.Location = new System.Drawing.Point(699, 214);
            this.txt_inn_IZZ.Name = "txt_inn_IZZ";
            this.txt_inn_IZZ.ReadOnly = true;
            this.txt_inn_IZZ.Size = new System.Drawing.Size(84, 21);
            this.txt_inn_IZZ.TabIndex = 2;
            this.txt_inn_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cen_IZZ
            // 
            this.txt_cen_IZZ.Location = new System.Drawing.Point(699, 136);
            this.txt_cen_IZZ.Name = "txt_cen_IZZ";
            this.txt_cen_IZZ.ReadOnly = true;
            this.txt_cen_IZZ.Size = new System.Drawing.Size(84, 21);
            this.txt_cen_IZZ.TabIndex = 2;
            this.txt_cen_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label444
            // 
            this.label444.AutoSize = true;
            this.label444.Location = new System.Drawing.Point(665, 296);
            this.label444.Name = "label444";
            this.label444.Size = new System.Drawing.Size(28, 13);
            this.label444.TabIndex = 1;
            this.label444.Text = "IZZ";
            // 
            // label354
            // 
            this.label354.AutoSize = true;
            this.label354.Location = new System.Drawing.Point(665, 217);
            this.label354.Name = "label354";
            this.label354.Size = new System.Drawing.Size(28, 13);
            this.label354.TabIndex = 1;
            this.label354.Text = "IZZ";
            // 
            // txt_tot_IZZ
            // 
            this.txt_tot_IZZ.Location = new System.Drawing.Point(699, 63);
            this.txt_tot_IZZ.Name = "txt_tot_IZZ";
            this.txt_tot_IZZ.ReadOnly = true;
            this.txt_tot_IZZ.Size = new System.Drawing.Size(84, 21);
            this.txt_tot_IZZ.TabIndex = 2;
            this.txt_tot_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tot_IZZ.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // label350
            // 
            this.label350.AutoSize = true;
            this.label350.Location = new System.Drawing.Point(665, 139);
            this.label350.Name = "label350";
            this.label350.Size = new System.Drawing.Size(28, 13);
            this.label350.TabIndex = 1;
            this.label350.Text = "IZZ";
            // 
            // txt_out_IYY
            // 
            this.txt_out_IYY.Location = new System.Drawing.Point(491, 293);
            this.txt_out_IYY.Name = "txt_out_IYY";
            this.txt_out_IYY.ReadOnly = true;
            this.txt_out_IYY.Size = new System.Drawing.Size(84, 21);
            this.txt_out_IYY.TabIndex = 2;
            this.txt_out_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_inn_IYY
            // 
            this.txt_inn_IYY.Location = new System.Drawing.Point(491, 214);
            this.txt_inn_IYY.Name = "txt_inn_IYY";
            this.txt_inn_IYY.ReadOnly = true;
            this.txt_inn_IYY.Size = new System.Drawing.Size(84, 21);
            this.txt_inn_IYY.TabIndex = 2;
            this.txt_inn_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(665, 66);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(28, 13);
            this.label144.TabIndex = 1;
            this.label144.Text = "IZZ";
            // 
            // label443
            // 
            this.label443.AutoSize = true;
            this.label443.Location = new System.Drawing.Point(459, 296);
            this.label443.Name = "label443";
            this.label443.Size = new System.Drawing.Size(26, 13);
            this.label443.TabIndex = 1;
            this.label443.Text = "IYY";
            // 
            // txt_cen_IYY
            // 
            this.txt_cen_IYY.Location = new System.Drawing.Point(491, 136);
            this.txt_cen_IYY.Name = "txt_cen_IYY";
            this.txt_cen_IYY.ReadOnly = true;
            this.txt_cen_IYY.Size = new System.Drawing.Size(84, 21);
            this.txt_cen_IYY.TabIndex = 2;
            this.txt_cen_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label353
            // 
            this.label353.AutoSize = true;
            this.label353.Location = new System.Drawing.Point(459, 217);
            this.label353.Name = "label353";
            this.label353.Size = new System.Drawing.Size(26, 13);
            this.label353.TabIndex = 1;
            this.label353.Text = "IYY";
            // 
            // txt_tot_IYY
            // 
            this.txt_tot_IYY.Location = new System.Drawing.Point(491, 63);
            this.txt_tot_IYY.Name = "txt_tot_IYY";
            this.txt_tot_IYY.ReadOnly = true;
            this.txt_tot_IYY.Size = new System.Drawing.Size(84, 21);
            this.txt_tot_IYY.TabIndex = 2;
            this.txt_tot_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tot_IYY.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // txt_out_IXX
            // 
            this.txt_out_IXX.Location = new System.Drawing.Point(291, 293);
            this.txt_out_IXX.Name = "txt_out_IXX";
            this.txt_out_IXX.ReadOnly = true;
            this.txt_out_IXX.Size = new System.Drawing.Size(84, 21);
            this.txt_out_IXX.TabIndex = 2;
            this.txt_out_IXX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label349
            // 
            this.label349.AutoSize = true;
            this.label349.Location = new System.Drawing.Point(459, 139);
            this.label349.Name = "label349";
            this.label349.Size = new System.Drawing.Size(26, 13);
            this.label349.TabIndex = 1;
            this.label349.Text = "IYY";
            // 
            // txt_inn_IXX
            // 
            this.txt_inn_IXX.Location = new System.Drawing.Point(291, 214);
            this.txt_inn_IXX.Name = "txt_inn_IXX";
            this.txt_inn_IXX.ReadOnly = true;
            this.txt_inn_IXX.Size = new System.Drawing.Size(84, 21);
            this.txt_inn_IXX.TabIndex = 2;
            this.txt_inn_IXX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label279
            // 
            this.label279.AutoSize = true;
            this.label279.Location = new System.Drawing.Point(459, 66);
            this.label279.Name = "label279";
            this.label279.Size = new System.Drawing.Size(26, 13);
            this.label279.TabIndex = 1;
            this.label279.Text = "IYY";
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(257, 296);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(28, 13);
            this.label155.TabIndex = 1;
            this.label155.Text = "IXX";
            // 
            // txt_cen_IXX
            // 
            this.txt_cen_IXX.Location = new System.Drawing.Point(291, 136);
            this.txt_cen_IXX.Name = "txt_cen_IXX";
            this.txt_cen_IXX.ReadOnly = true;
            this.txt_cen_IXX.Size = new System.Drawing.Size(84, 21);
            this.txt_cen_IXX.TabIndex = 2;
            this.txt_cen_IXX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label352
            // 
            this.label352.AutoSize = true;
            this.label352.Location = new System.Drawing.Point(257, 217);
            this.label352.Name = "label352";
            this.label352.Size = new System.Drawing.Size(28, 13);
            this.label352.TabIndex = 1;
            this.label352.Text = "IXX";
            // 
            // txt_tot_IXX
            // 
            this.txt_tot_IXX.Location = new System.Drawing.Point(291, 63);
            this.txt_tot_IXX.Name = "txt_tot_IXX";
            this.txt_tot_IXX.ReadOnly = true;
            this.txt_tot_IXX.Size = new System.Drawing.Size(84, 21);
            this.txt_tot_IXX.TabIndex = 2;
            this.txt_tot_IXX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tot_IXX.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // txt_out_pcnt
            // 
            this.txt_out_pcnt.Location = new System.Drawing.Point(479, 258);
            this.txt_out_pcnt.Name = "txt_out_pcnt";
            this.txt_out_pcnt.Size = new System.Drawing.Size(55, 21);
            this.txt_out_pcnt.TabIndex = 2;
            this.txt_out_pcnt.Text = "15";
            this.txt_out_pcnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_out_pcnt.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // label348
            // 
            this.label348.AutoSize = true;
            this.label348.Location = new System.Drawing.Point(257, 139);
            this.label348.Name = "label348";
            this.label348.Size = new System.Drawing.Size(28, 13);
            this.label348.TabIndex = 1;
            this.label348.Text = "IXX";
            // 
            // txt_inn_pcnt
            // 
            this.txt_inn_pcnt.Location = new System.Drawing.Point(479, 179);
            this.txt_inn_pcnt.Name = "txt_inn_pcnt";
            this.txt_inn_pcnt.Size = new System.Drawing.Size(55, 21);
            this.txt_inn_pcnt.TabIndex = 2;
            this.txt_inn_pcnt.Text = "15";
            this.txt_inn_pcnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_inn_pcnt.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // txt_out_AX
            // 
            this.txt_out_AX.Location = new System.Drawing.Point(97, 293);
            this.txt_out_AX.Name = "txt_out_AX";
            this.txt_out_AX.ReadOnly = true;
            this.txt_out_AX.Size = new System.Drawing.Size(73, 21);
            this.txt_out_AX.TabIndex = 2;
            this.txt_out_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(257, 66);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(28, 13);
            this.label162.TabIndex = 1;
            this.label162.Text = "IXX";
            // 
            // txt_inn_AX
            // 
            this.txt_inn_AX.Location = new System.Drawing.Point(97, 214);
            this.txt_inn_AX.Name = "txt_inn_AX";
            this.txt_inn_AX.ReadOnly = true;
            this.txt_inn_AX.Size = new System.Drawing.Size(73, 21);
            this.txt_inn_AX.TabIndex = 2;
            this.txt_inn_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cen_pcnt
            // 
            this.txt_cen_pcnt.Location = new System.Drawing.Point(479, 104);
            this.txt_cen_pcnt.Name = "txt_cen_pcnt";
            this.txt_cen_pcnt.Size = new System.Drawing.Size(55, 21);
            this.txt_cen_pcnt.TabIndex = 2;
            this.txt_cen_pcnt.Text = "40";
            this.txt_cen_pcnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cen_pcnt.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(540, 261);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(19, 13);
            this.label163.TabIndex = 1;
            this.label163.Text = "%";
            // 
            // txt_cen_AX
            // 
            this.txt_cen_AX.Location = new System.Drawing.Point(97, 136);
            this.txt_cen_AX.Name = "txt_cen_AX";
            this.txt_cen_AX.ReadOnly = true;
            this.txt_cen_AX.Size = new System.Drawing.Size(73, 21);
            this.txt_cen_AX.TabIndex = 2;
            this.txt_cen_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label339
            // 
            this.label339.AutoSize = true;
            this.label339.Location = new System.Drawing.Point(540, 182);
            this.label339.Name = "label339";
            this.label339.Size = new System.Drawing.Size(19, 13);
            this.label339.TabIndex = 1;
            this.label339.Text = "%";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label165.Location = new System.Drawing.Point(4, 261);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(418, 18);
            this.label165.TabIndex = 0;
            this.label165.Text = "For Two Outer Member of Lumped Mass Model";
            // 
            // txt_tot_AX
            // 
            this.txt_tot_AX.Location = new System.Drawing.Point(97, 63);
            this.txt_tot_AX.Name = "txt_tot_AX";
            this.txt_tot_AX.ReadOnly = true;
            this.txt_tot_AX.Size = new System.Drawing.Size(73, 21);
            this.txt_tot_AX.TabIndex = 2;
            this.txt_tot_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tot_AX.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(789, 296);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(49, 13);
            this.label166.TabIndex = 1;
            this.label166.Text = "sq.sq.ft";
            // 
            // label338
            // 
            this.label338.AutoSize = true;
            this.label338.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label338.Location = new System.Drawing.Point(4, 182);
            this.label338.Name = "label338";
            this.label338.Size = new System.Drawing.Size(416, 18);
            this.label338.TabIndex = 0;
            this.label338.Text = "For Two Inner Member of Lumped Mass Model";
            // 
            // label351
            // 
            this.label351.AutoSize = true;
            this.label351.Location = new System.Drawing.Point(789, 217);
            this.label351.Name = "label351";
            this.label351.Size = new System.Drawing.Size(49, 13);
            this.label351.TabIndex = 1;
            this.label351.Text = "sq.sq.ft";
            // 
            // label505
            // 
            this.label505.AutoSize = true;
            this.label505.Location = new System.Drawing.Point(447, 261);
            this.label505.Name = "label505";
            this.label505.Size = new System.Drawing.Size(26, 13);
            this.label505.TabIndex = 1;
            this.label505.Text = "2 X";
            // 
            // label502
            // 
            this.label502.AutoSize = true;
            this.label502.Location = new System.Drawing.Point(447, 182);
            this.label502.Name = "label502";
            this.label502.Size = new System.Drawing.Size(26, 13);
            this.label502.TabIndex = 1;
            this.label502.Text = "2 X";
            // 
            // label501
            // 
            this.label501.AutoSize = true;
            this.label501.Location = new System.Drawing.Point(447, 107);
            this.label501.Name = "label501";
            this.label501.Size = new System.Drawing.Size(26, 13);
            this.label501.TabIndex = 1;
            this.label501.Text = "1 X";
            // 
            // label336
            // 
            this.label336.AutoSize = true;
            this.label336.Location = new System.Drawing.Point(540, 107);
            this.label336.Name = "label336";
            this.label336.Size = new System.Drawing.Size(19, 13);
            this.label336.TabIndex = 1;
            this.label336.Text = "%";
            // 
            // label399
            // 
            this.label399.AutoSize = true;
            this.label399.Location = new System.Drawing.Point(578, 296);
            this.label399.Name = "label399";
            this.label399.Size = new System.Drawing.Size(49, 13);
            this.label399.TabIndex = 1;
            this.label399.Text = "sq.sq.ft";
            // 
            // label347
            // 
            this.label347.AutoSize = true;
            this.label347.Location = new System.Drawing.Point(789, 139);
            this.label347.Name = "label347";
            this.label347.Size = new System.Drawing.Size(49, 13);
            this.label347.TabIndex = 1;
            this.label347.Text = "sq.sq.ft";
            // 
            // label342
            // 
            this.label342.AutoSize = true;
            this.label342.Location = new System.Drawing.Point(578, 217);
            this.label342.Name = "label342";
            this.label342.Size = new System.Drawing.Size(49, 13);
            this.label342.TabIndex = 1;
            this.label342.Text = "sq.sq.ft";
            // 
            // label281
            // 
            this.label281.AutoSize = true;
            this.label281.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label281.Location = new System.Drawing.Point(4, 107);
            this.label281.Name = "label281";
            this.label281.Size = new System.Drawing.Size(428, 18);
            this.label281.TabIndex = 0;
            this.label281.Text = "For One Central Member of Lumped Mass Model";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(381, 296);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(49, 13);
            this.label167.TabIndex = 1;
            this.label167.Text = "sq.sq.ft";
            // 
            // label335
            // 
            this.label335.AutoSize = true;
            this.label335.Location = new System.Drawing.Point(578, 139);
            this.label335.Name = "label335";
            this.label335.Size = new System.Drawing.Size(49, 13);
            this.label335.TabIndex = 1;
            this.label335.Text = "sq.sq.ft";
            // 
            // label341
            // 
            this.label341.AutoSize = true;
            this.label341.Location = new System.Drawing.Point(381, 217);
            this.label341.Name = "label341";
            this.label341.Size = new System.Drawing.Size(49, 13);
            this.label341.TabIndex = 1;
            this.label341.Text = "sq.sq.ft";
            // 
            // label346
            // 
            this.label346.AutoSize = true;
            this.label346.Location = new System.Drawing.Point(789, 66);
            this.label346.Name = "label346";
            this.label346.Size = new System.Drawing.Size(49, 13);
            this.label346.TabIndex = 1;
            this.label346.Text = "sq.sq.ft";
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(176, 296);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(32, 13);
            this.label168.TabIndex = 1;
            this.label168.Text = "sq.ft";
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(381, 139);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(49, 13);
            this.label169.TabIndex = 1;
            this.label169.Text = "sq.sq.ft";
            // 
            // label340
            // 
            this.label340.AutoSize = true;
            this.label340.Location = new System.Drawing.Point(176, 217);
            this.label340.Name = "label340";
            this.label340.Size = new System.Drawing.Size(32, 13);
            this.label340.TabIndex = 1;
            this.label340.Text = "sq.ft";
            // 
            // label345
            // 
            this.label345.AutoSize = true;
            this.label345.Location = new System.Drawing.Point(578, 66);
            this.label345.Name = "label345";
            this.label345.Size = new System.Drawing.Size(49, 13);
            this.label345.TabIndex = 1;
            this.label345.Text = "sq.sq.ft";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(59, 296);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(23, 13);
            this.label170.TabIndex = 1;
            this.label170.Text = "AX";
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(176, 139);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(32, 13);
            this.label171.TabIndex = 1;
            this.label171.Text = "sq.ft";
            // 
            // label337
            // 
            this.label337.AutoSize = true;
            this.label337.Location = new System.Drawing.Point(59, 217);
            this.label337.Name = "label337";
            this.label337.Size = new System.Drawing.Size(23, 13);
            this.label337.TabIndex = 1;
            this.label337.Text = "AX";
            // 
            // label344
            // 
            this.label344.AutoSize = true;
            this.label344.Location = new System.Drawing.Point(381, 66);
            this.label344.Name = "label344";
            this.label344.Size = new System.Drawing.Size(49, 13);
            this.label344.TabIndex = 1;
            this.label344.Text = "sq.sq.ft";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(59, 139);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(23, 13);
            this.label172.TabIndex = 1;
            this.label172.Text = "AX";
            // 
            // label343
            // 
            this.label343.AutoSize = true;
            this.label343.Location = new System.Drawing.Point(176, 66);
            this.label343.Name = "label343";
            this.label343.Size = new System.Drawing.Size(32, 13);
            this.label343.TabIndex = 1;
            this.label343.Text = "sq.ft";
            // 
            // label277
            // 
            this.label277.AutoSize = true;
            this.label277.Location = new System.Drawing.Point(59, 66);
            this.label277.Name = "label277";
            this.label277.Size = new System.Drawing.Size(23, 13);
            this.label277.TabIndex = 1;
            this.label277.Text = "AX";
            // 
            // label498
            // 
            this.label498.AutoSize = true;
            this.label498.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label498.Location = new System.Drawing.Point(3, 37);
            this.label498.Name = "label498";
            this.label498.Size = new System.Drawing.Size(88, 14);
            this.label498.TabIndex = 0;
            this.label498.Text = "Total Values";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.Location = new System.Drawing.Point(4, 10);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(378, 18);
            this.label175.TabIndex = 0;
            this.label175.Text = "Average Section Properties for Box Girder";
            // 
            // tab_moving_data
            // 
            this.tab_moving_data.Controls.Add(this.groupBox79);
            this.tab_moving_data.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data.Name = "tab_moving_data";
            this.tab_moving_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data.Size = new System.Drawing.Size(955, 634);
            this.tab_moving_data.TabIndex = 5;
            this.tab_moving_data.Text = "Moving Load Data [AASHTO - LRFD]";
            this.tab_moving_data.UseVisualStyleBackColor = true;
            // 
            // groupBox79
            // 
            this.groupBox79.Controls.Add(this.btn_irc_view_moving_load);
            this.groupBox79.Controls.Add(this.label568);
            this.groupBox79.Controls.Add(this.cmb_irc_view_moving_load);
            this.groupBox79.Controls.Add(this.txt_irc_vehicle_gap);
            this.groupBox79.Controls.Add(this.label569);
            this.groupBox79.Controls.Add(this.label299);
            this.groupBox79.Controls.Add(this.txt_dl_ll_comb);
            this.groupBox79.Controls.Add(this.btn_edit_load_combs);
            this.groupBox79.Controls.Add(this.chk_self_indian);
            this.groupBox79.Controls.Add(this.btn_long_restore_ll_IRC);
            this.groupBox79.Controls.Add(this.groupBox31);
            this.groupBox79.Controls.Add(this.label301);
            this.groupBox79.Controls.Add(this.label302);
            this.groupBox79.Controls.Add(this.groupBox46);
            this.groupBox79.Controls.Add(this.label304);
            this.groupBox79.Controls.Add(this.txt_IRC_LL_load_gen);
            this.groupBox79.Controls.Add(this.txt_IRC_XINCR);
            this.groupBox79.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox79.Location = new System.Drawing.Point(3, 3);
            this.groupBox79.Name = "groupBox79";
            this.groupBox79.Size = new System.Drawing.Size(949, 628);
            this.groupBox79.TabIndex = 82;
            this.groupBox79.TabStop = false;
            // 
            // btn_irc_view_moving_load
            // 
            this.btn_irc_view_moving_load.Location = new System.Drawing.Point(48, 540);
            this.btn_irc_view_moving_load.Name = "btn_irc_view_moving_load";
            this.btn_irc_view_moving_load.Size = new System.Drawing.Size(204, 29);
            this.btn_irc_view_moving_load.TabIndex = 125;
            this.btn_irc_view_moving_load.Text = "View Moving Load";
            this.btn_irc_view_moving_load.UseVisualStyleBackColor = true;
            this.btn_irc_view_moving_load.Click += new System.EventHandler(this.btn_irc_view_moving_load_Click);
            // 
            // label568
            // 
            this.label568.AutoSize = true;
            this.label568.Location = new System.Drawing.Point(10, 519);
            this.label568.Name = "label568";
            this.label568.Size = new System.Drawing.Size(166, 13);
            this.label568.TabIndex = 124;
            this.label568.Text = "Select to view Moving Load ";
            // 
            // cmb_irc_view_moving_load
            // 
            this.cmb_irc_view_moving_load.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_irc_view_moving_load.FormattingEnabled = true;
            this.cmb_irc_view_moving_load.Location = new System.Drawing.Point(190, 516);
            this.cmb_irc_view_moving_load.Name = "cmb_irc_view_moving_load";
            this.cmb_irc_view_moving_load.Size = new System.Drawing.Size(84, 21);
            this.cmb_irc_view_moving_load.TabIndex = 123;
            // 
            // txt_irc_vehicle_gap
            // 
            this.txt_irc_vehicle_gap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_irc_vehicle_gap.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_irc_vehicle_gap.Location = new System.Drawing.Point(734, 518);
            this.txt_irc_vehicle_gap.Name = "txt_irc_vehicle_gap";
            this.txt_irc_vehicle_gap.Size = new System.Drawing.Size(65, 18);
            this.txt_irc_vehicle_gap.TabIndex = 121;
            this.txt_irc_vehicle_gap.Text = "18.8";
            this.txt_irc_vehicle_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label569
            // 
            this.label569.AutoSize = true;
            this.label569.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label569.Location = new System.Drawing.Point(307, 519);
            this.label569.Name = "label569";
            this.label569.Size = new System.Drawing.Size(421, 13);
            this.label569.TabIndex = 122;
            this.label569.Text = "Longitudinal Separating distance between two vehicle in a Lane";
            // 
            // label299
            // 
            this.label299.AutoSize = true;
            this.label299.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label299.Location = new System.Drawing.Point(692, 602);
            this.label299.Name = "label299";
            this.label299.Size = new System.Drawing.Size(170, 13);
            this.label299.TabIndex = 85;
            this.label299.Text = "DL + LL Combine Load No";
            // 
            // txt_dl_ll_comb
            // 
            this.txt_dl_ll_comb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dl_ll_comb.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dl_ll_comb.Location = new System.Drawing.Point(874, 602);
            this.txt_dl_ll_comb.Name = "txt_dl_ll_comb";
            this.txt_dl_ll_comb.Size = new System.Drawing.Size(39, 18);
            this.txt_dl_ll_comb.TabIndex = 84;
            this.txt_dl_ll_comb.Text = "1";
            this.txt_dl_ll_comb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_edit_load_combs
            // 
            this.btn_edit_load_combs.Location = new System.Drawing.Point(214, 575);
            this.btn_edit_load_combs.Name = "btn_edit_load_combs";
            this.btn_edit_load_combs.Size = new System.Drawing.Size(157, 29);
            this.btn_edit_load_combs.TabIndex = 83;
            this.btn_edit_load_combs.Text = "Edit Load Combinations";
            this.btn_edit_load_combs.UseVisualStyleBackColor = true;
            this.btn_edit_load_combs.Click += new System.EventHandler(this.btn_edit_load_combs_Click);
            // 
            // chk_self_indian
            // 
            this.chk_self_indian.AutoSize = true;
            this.chk_self_indian.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_self_indian.Location = new System.Drawing.Point(25, 580);
            this.chk_self_indian.Name = "chk_self_indian";
            this.chk_self_indian.Size = new System.Drawing.Size(165, 20);
            this.chk_self_indian.TabIndex = 81;
            this.chk_self_indian.Text = "Apply SELFWEIGHT";
            this.chk_self_indian.UseVisualStyleBackColor = true;
            // 
            // btn_long_restore_ll_IRC
            // 
            this.btn_long_restore_ll_IRC.Location = new System.Drawing.Point(406, 575);
            this.btn_long_restore_ll_IRC.Name = "btn_long_restore_ll_IRC";
            this.btn_long_restore_ll_IRC.Size = new System.Drawing.Size(167, 29);
            this.btn_long_restore_ll_IRC.TabIndex = 8;
            this.btn_long_restore_ll_IRC.Text = "Restore Default Values";
            this.btn_long_restore_ll_IRC.UseVisualStyleBackColor = true;
            this.btn_long_restore_ll_IRC.Click += new System.EventHandler(this.btn_long_restore_ll_Click);
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.label300);
            this.groupBox31.Controls.Add(this.dgv_long_loads);
            this.groupBox31.ForeColor = System.Drawing.Color.Black;
            this.groupBox31.Location = new System.Drawing.Point(6, 33);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(269, 477);
            this.groupBox31.TabIndex = 8;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Define Load Start position";
            // 
            // label300
            // 
            this.label300.AutoSize = true;
            this.label300.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label300.ForeColor = System.Drawing.Color.Black;
            this.label300.Location = new System.Drawing.Point(3, 17);
            this.label300.Name = "label300";
            this.label300.Size = new System.Drawing.Size(264, 13);
            this.label300.TabIndex = 11;
            this.label300.Text = "(Refer to Page 100, ASTRA Pro User Manual)";
            // 
            // dgv_long_loads
            // 
            this.dgv_long_loads.AllowUserToAddRows = false;
            this.dgv_long_loads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_long_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_long_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_loads.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_long_loads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_long_loads.Location = new System.Drawing.Point(3, 33);
            this.dgv_long_loads.Name = "dgv_long_loads";
            this.dgv_long_loads.RowHeadersWidth = 21;
            this.dgv_long_loads.Size = new System.Drawing.Size(263, 441);
            this.dgv_long_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn31
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn31.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn31.Width = 90;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "1";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn32.Width = 50;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "2";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn33.Width = 50;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "3";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn34.Width = 50;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "4";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn35.Width = 50;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.HeaderText = "5";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn36.Width = 50;
            // 
            // label301
            // 
            this.label301.AutoSize = true;
            this.label301.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label301.Location = new System.Drawing.Point(754, 578);
            this.label301.Name = "label301";
            this.label301.Size = new System.Drawing.Size(114, 13);
            this.label301.TabIndex = 80;
            this.label301.Text = "Load Generation";
            // 
            // label302
            // 
            this.label302.AutoSize = true;
            this.label302.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label302.ForeColor = System.Drawing.Color.Black;
            this.label302.Location = new System.Drawing.Point(110, 17);
            this.label302.Name = "label302";
            this.label302.Size = new System.Drawing.Size(687, 13);
            this.label302.TabIndex = 9;
            this.label302.Text = "GRILLAGE ANALYSIS OF SUPER STRUCTURE APPLYING 5 TYPES OF LIVE LOADS WITH SOME COM" +
    "BINATIONS";
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label303);
            this.groupBox46.Controls.Add(this.dgv_long_liveloads);
            this.groupBox46.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox46.ForeColor = System.Drawing.Color.Black;
            this.groupBox46.Location = new System.Drawing.Point(279, 33);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(664, 477);
            this.groupBox46.TabIndex = 7;
            this.groupBox46.TabStop = false;
            this.groupBox46.Text = "Define Vehicle Axle Loads";
            // 
            // label303
            // 
            this.label303.AutoSize = true;
            this.label303.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label303.ForeColor = System.Drawing.Color.Black;
            this.label303.Location = new System.Drawing.Point(2, 17);
            this.label303.Name = "label303";
            this.label303.Size = new System.Drawing.Size(648, 13);
            this.label303.TabIndex = 10;
            this.label303.Text = "(USER MAY CHANGE THE VAUES IN THE CELLS, THE DATA WILL BE SAVED IN FILE \"LL.TXT\" " +
    "FOR USE)";
            // 
            // dgv_long_liveloads
            // 
            this.dgv_long_liveloads.AllowUserToAddRows = false;
            this.dgv_long_liveloads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_liveloads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_long_liveloads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_long_liveloads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_liveloads.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_long_liveloads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_long_liveloads.Location = new System.Drawing.Point(3, 33);
            this.dgv_long_liveloads.Name = "dgv_long_liveloads";
            this.dgv_long_liveloads.RowHeadersWidth = 21;
            this.dgv_long_liveloads.Size = new System.Drawing.Size(658, 441);
            this.dgv_long_liveloads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn1.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "1";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "2";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "3";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 50;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "4";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 50;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "5";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 50;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "6";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 50;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "7";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.Width = 50;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "8";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn17.Width = 50;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "9";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.Width = 50;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "10";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn19.Width = 50;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "11";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.Width = 50;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "12";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn21.Width = 50;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "13";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn22.Width = 50;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "14";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn23.Width = 50;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "15";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.Width = 50;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "16";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn25.Width = 50;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "17";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 50;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "18";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn27.Width = 50;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.HeaderText = "19";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn28.Width = 50;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "20";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn29.Width = 50;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "21";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn30.Width = 50;
            // 
            // label304
            // 
            this.label304.AutoSize = true;
            this.label304.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label304.Location = new System.Drawing.Point(639, 578);
            this.label304.Name = "label304";
            this.label304.Size = new System.Drawing.Size(50, 13);
            this.label304.TabIndex = 60;
            this.label304.Text = "X INCR";
            // 
            // txt_IRC_LL_load_gen
            // 
            this.txt_IRC_LL_load_gen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IRC_LL_load_gen.Location = new System.Drawing.Point(874, 575);
            this.txt_IRC_LL_load_gen.Name = "txt_IRC_LL_load_gen";
            this.txt_IRC_LL_load_gen.Size = new System.Drawing.Size(39, 21);
            this.txt_IRC_LL_load_gen.TabIndex = 79;
            this.txt_IRC_LL_load_gen.Text = "52";
            this.txt_IRC_LL_load_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_IRC_XINCR
            // 
            this.txt_IRC_XINCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_IRC_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IRC_XINCR.Location = new System.Drawing.Point(695, 577);
            this.txt_IRC_XINCR.Name = "txt_IRC_XINCR";
            this.txt_IRC_XINCR.Size = new System.Drawing.Size(37, 18);
            this.txt_IRC_XINCR.TabIndex = 58;
            this.txt_IRC_XINCR.Text = "5.0";
            this.txt_IRC_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_IRC_XINCR.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // tab_analysis
            // 
            this.tab_analysis.Controls.Add(this.splitContainer1);
            this.tab_analysis.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis.Name = "tab_analysis";
            this.tab_analysis.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis.Size = new System.Drawing.Size(955, 634);
            this.tab_analysis.TabIndex = 1;
            this.tab_analysis.Text = "Analysis (Normal) Process";
            this.tab_analysis.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox25);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl5);
            this.splitContainer1.Size = new System.Drawing.Size(949, 628);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 104;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_selfweight);
            this.groupBox2.Controls.Add(this.groupBox50);
            this.groupBox2.Controls.Add(this.groupBox51);
            this.groupBox2.Controls.Add(this.groupBox52);
            this.groupBox2.Controls.Add(this.groupBox70);
            this.groupBox2.Controls.Add(this.groupBox109);
            this.groupBox2.Controls.Add(this.btn_Process_LL_Analysis);
            this.groupBox2.Controls.Add(this.btn_Ana_DL_create_data);
            this.groupBox2.Controls.Add(this.groupBox71);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(947, 199);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            // 
            // chk_selfweight
            // 
            this.chk_selfweight.AutoSize = true;
            this.chk_selfweight.Checked = true;
            this.chk_selfweight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_selfweight.Location = new System.Drawing.Point(14, 117);
            this.chk_selfweight.Name = "chk_selfweight";
            this.chk_selfweight.Size = new System.Drawing.Size(130, 17);
            this.chk_selfweight.TabIndex = 143;
            this.chk_selfweight.Text = "ADD SELFWEIGHT";
            this.chk_selfweight.UseVisualStyleBackColor = true;
            // 
            // groupBox50
            // 
            this.groupBox50.Controls.Add(this.label252);
            this.groupBox50.Controls.Add(this.label323);
            this.groupBox50.Controls.Add(this.label324);
            this.groupBox50.Controls.Add(this.label328);
            this.groupBox50.Controls.Add(this.label329);
            this.groupBox50.Controls.Add(this.txt_PR_cable);
            this.groupBox50.Controls.Add(this.txt_den_cable);
            this.groupBox50.Controls.Add(this.txt_emod_cable);
            this.groupBox50.Location = new System.Drawing.Point(446, 104);
            this.groupBox50.Name = "groupBox50";
            this.groupBox50.Size = new System.Drawing.Size(478, 40);
            this.groupBox50.TabIndex = 140;
            this.groupBox50.TabStop = false;
            this.groupBox50.Text = "CABLE Material Constants";
            // 
            // label252
            // 
            this.label252.AutoSize = true;
            this.label252.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label252.ForeColor = System.Drawing.Color.Black;
            this.label252.Location = new System.Drawing.Point(382, 18);
            this.label252.Name = "label252";
            this.label252.Size = new System.Drawing.Size(24, 16);
            this.label252.TabIndex = 100;
            this.label252.Text = "PR";
            // 
            // label323
            // 
            this.label323.AutoSize = true;
            this.label323.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label323.ForeColor = System.Drawing.Color.Black;
            this.label323.Location = new System.Drawing.Point(224, 18);
            this.label323.Name = "label323";
            this.label323.Size = new System.Drawing.Size(33, 16);
            this.label323.TabIndex = 100;
            this.label323.Text = "Den";
            // 
            // label324
            // 
            this.label324.AutoSize = true;
            this.label324.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label324.ForeColor = System.Drawing.Color.Black;
            this.label324.Location = new System.Drawing.Point(314, 18);
            this.label324.Name = "label324";
            this.label324.Size = new System.Drawing.Size(66, 16);
            this.label324.TabIndex = 100;
            this.label324.Text = "Kip/Cu.ft";
            // 
            // label328
            // 
            this.label328.AutoSize = true;
            this.label328.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label328.ForeColor = System.Drawing.Color.Black;
            this.label328.Location = new System.Drawing.Point(145, 18);
            this.label328.Name = "label328";
            this.label328.Size = new System.Drawing.Size(66, 16);
            this.label328.TabIndex = 100;
            this.label328.Text = "Kip/Sq.ft";
            // 
            // label329
            // 
            this.label329.AutoSize = true;
            this.label329.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label329.ForeColor = System.Drawing.Color.Black;
            this.label329.Location = new System.Drawing.Point(12, 18);
            this.label329.Name = "label329";
            this.label329.Size = new System.Drawing.Size(48, 16);
            this.label329.TabIndex = 100;
            this.label329.Text = "E Mod";
            // 
            // txt_PR_cable
            // 
            this.txt_PR_cable.Location = new System.Drawing.Point(412, 17);
            this.txt_PR_cable.Name = "txt_PR_cable";
            this.txt_PR_cable.Size = new System.Drawing.Size(48, 21);
            this.txt_PR_cable.TabIndex = 108;
            this.txt_PR_cable.Text = "0.30";
            this.txt_PR_cable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_den_cable
            // 
            this.txt_den_cable.Location = new System.Drawing.Point(260, 17);
            this.txt_den_cable.Name = "txt_den_cable";
            this.txt_den_cable.Size = new System.Drawing.Size(48, 21);
            this.txt_den_cable.TabIndex = 108;
            this.txt_den_cable.Text = "0.49";
            this.txt_den_cable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_emod_cable
            // 
            this.txt_emod_cable.Location = new System.Drawing.Point(66, 17);
            this.txt_emod_cable.Name = "txt_emod_cable";
            this.txt_emod_cable.Size = new System.Drawing.Size(76, 21);
            this.txt_emod_cable.TabIndex = 108;
            this.txt_emod_cable.Text = "4180000";
            this.txt_emod_cable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox51
            // 
            this.groupBox51.Controls.Add(this.label330);
            this.groupBox51.Controls.Add(this.label331);
            this.groupBox51.Controls.Add(this.label332);
            this.groupBox51.Controls.Add(this.label333);
            this.groupBox51.Controls.Add(this.label334);
            this.groupBox51.Controls.Add(this.txt_PR_conc);
            this.groupBox51.Controls.Add(this.txt_den_conc);
            this.groupBox51.Controls.Add(this.txt_emod_conc);
            this.groupBox51.Location = new System.Drawing.Point(446, 58);
            this.groupBox51.Name = "groupBox51";
            this.groupBox51.Size = new System.Drawing.Size(478, 40);
            this.groupBox51.TabIndex = 141;
            this.groupBox51.TabStop = false;
            this.groupBox51.Text = "CONCRETE Material Constants";
            // 
            // label330
            // 
            this.label330.AutoSize = true;
            this.label330.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label330.ForeColor = System.Drawing.Color.Black;
            this.label330.Location = new System.Drawing.Point(382, 17);
            this.label330.Name = "label330";
            this.label330.Size = new System.Drawing.Size(24, 16);
            this.label330.TabIndex = 100;
            this.label330.Text = "PR";
            // 
            // label331
            // 
            this.label331.AutoSize = true;
            this.label331.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label331.ForeColor = System.Drawing.Color.Black;
            this.label331.Location = new System.Drawing.Point(224, 18);
            this.label331.Name = "label331";
            this.label331.Size = new System.Drawing.Size(33, 16);
            this.label331.TabIndex = 100;
            this.label331.Text = "Den";
            // 
            // label332
            // 
            this.label332.AutoSize = true;
            this.label332.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label332.ForeColor = System.Drawing.Color.Black;
            this.label332.Location = new System.Drawing.Point(314, 18);
            this.label332.Name = "label332";
            this.label332.Size = new System.Drawing.Size(66, 16);
            this.label332.TabIndex = 100;
            this.label332.Text = "Kip/Cu.ft";
            // 
            // label333
            // 
            this.label333.AutoSize = true;
            this.label333.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label333.ForeColor = System.Drawing.Color.Black;
            this.label333.Location = new System.Drawing.Point(145, 18);
            this.label333.Name = "label333";
            this.label333.Size = new System.Drawing.Size(66, 16);
            this.label333.TabIndex = 100;
            this.label333.Text = "Kip/Sq.ft";
            // 
            // label334
            // 
            this.label334.AutoSize = true;
            this.label334.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label334.ForeColor = System.Drawing.Color.Black;
            this.label334.Location = new System.Drawing.Point(12, 18);
            this.label334.Name = "label334";
            this.label334.Size = new System.Drawing.Size(48, 16);
            this.label334.TabIndex = 100;
            this.label334.Text = "E Mod";
            // 
            // txt_PR_conc
            // 
            this.txt_PR_conc.Location = new System.Drawing.Point(412, 16);
            this.txt_PR_conc.Name = "txt_PR_conc";
            this.txt_PR_conc.Size = new System.Drawing.Size(48, 21);
            this.txt_PR_conc.TabIndex = 108;
            this.txt_PR_conc.Text = "0.15";
            this.txt_PR_conc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_den_conc
            // 
            this.txt_den_conc.Location = new System.Drawing.Point(260, 17);
            this.txt_den_conc.Name = "txt_den_conc";
            this.txt_den_conc.Size = new System.Drawing.Size(48, 21);
            this.txt_den_conc.TabIndex = 108;
            this.txt_den_conc.Text = "0.15";
            this.txt_den_conc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_emod_conc
            // 
            this.txt_emod_conc.Location = new System.Drawing.Point(66, 17);
            this.txt_emod_conc.Name = "txt_emod_conc";
            this.txt_emod_conc.Size = new System.Drawing.Size(76, 21);
            this.txt_emod_conc.TabIndex = 108;
            this.txt_emod_conc.Text = "432000";
            this.txt_emod_conc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox52
            // 
            this.groupBox52.Controls.Add(this.label1194);
            this.groupBox52.Controls.Add(this.label1193);
            this.groupBox52.Controls.Add(this.label1196);
            this.groupBox52.Controls.Add(this.label1195);
            this.groupBox52.Controls.Add(this.label1192);
            this.groupBox52.Controls.Add(this.txt_PR_steel);
            this.groupBox52.Controls.Add(this.txt_den_steel);
            this.groupBox52.Controls.Add(this.txt_emod_steel);
            this.groupBox52.Location = new System.Drawing.Point(446, 14);
            this.groupBox52.Name = "groupBox52";
            this.groupBox52.Size = new System.Drawing.Size(478, 40);
            this.groupBox52.TabIndex = 142;
            this.groupBox52.TabStop = false;
            this.groupBox52.Text = "STEEL Material Constants";
            // 
            // label1194
            // 
            this.label1194.AutoSize = true;
            this.label1194.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1194.ForeColor = System.Drawing.Color.Black;
            this.label1194.Location = new System.Drawing.Point(382, 18);
            this.label1194.Name = "label1194";
            this.label1194.Size = new System.Drawing.Size(24, 16);
            this.label1194.TabIndex = 100;
            this.label1194.Text = "PR";
            // 
            // label1193
            // 
            this.label1193.AutoSize = true;
            this.label1193.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1193.ForeColor = System.Drawing.Color.Black;
            this.label1193.Location = new System.Drawing.Point(224, 18);
            this.label1193.Name = "label1193";
            this.label1193.Size = new System.Drawing.Size(33, 16);
            this.label1193.TabIndex = 100;
            this.label1193.Text = "Den";
            // 
            // label1196
            // 
            this.label1196.AutoSize = true;
            this.label1196.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1196.ForeColor = System.Drawing.Color.Black;
            this.label1196.Location = new System.Drawing.Point(314, 18);
            this.label1196.Name = "label1196";
            this.label1196.Size = new System.Drawing.Size(66, 16);
            this.label1196.TabIndex = 100;
            this.label1196.Text = "Kip/Cu.ft";
            // 
            // label1195
            // 
            this.label1195.AutoSize = true;
            this.label1195.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1195.ForeColor = System.Drawing.Color.Black;
            this.label1195.Location = new System.Drawing.Point(145, 18);
            this.label1195.Name = "label1195";
            this.label1195.Size = new System.Drawing.Size(66, 16);
            this.label1195.TabIndex = 100;
            this.label1195.Text = "Kip/Sq.ft";
            // 
            // label1192
            // 
            this.label1192.AutoSize = true;
            this.label1192.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1192.ForeColor = System.Drawing.Color.Black;
            this.label1192.Location = new System.Drawing.Point(12, 18);
            this.label1192.Name = "label1192";
            this.label1192.Size = new System.Drawing.Size(48, 16);
            this.label1192.TabIndex = 100;
            this.label1192.Text = "E Mod";
            // 
            // txt_PR_steel
            // 
            this.txt_PR_steel.Location = new System.Drawing.Point(412, 17);
            this.txt_PR_steel.Name = "txt_PR_steel";
            this.txt_PR_steel.Size = new System.Drawing.Size(48, 21);
            this.txt_PR_steel.TabIndex = 108;
            this.txt_PR_steel.Text = "0.30";
            this.txt_PR_steel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_den_steel
            // 
            this.txt_den_steel.Location = new System.Drawing.Point(260, 17);
            this.txt_den_steel.Name = "txt_den_steel";
            this.txt_den_steel.Size = new System.Drawing.Size(48, 21);
            this.txt_den_steel.TabIndex = 108;
            this.txt_den_steel.Text = "0.49";
            this.txt_den_steel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_emod_steel
            // 
            this.txt_emod_steel.Location = new System.Drawing.Point(66, 17);
            this.txt_emod_steel.Name = "txt_emod_steel";
            this.txt_emod_steel.Size = new System.Drawing.Size(76, 21);
            this.txt_emod_steel.TabIndex = 108;
            this.txt_emod_steel.Text = "4180000";
            this.txt_emod_steel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox70
            // 
            this.groupBox70.Controls.Add(this.rbtn_esprt_pinned);
            this.groupBox70.Controls.Add(this.rbtn_esprt_fixed);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_MZ);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_FZ);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_MY);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_FY);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_MX);
            this.groupBox70.Controls.Add(this.chk_esprt_fixed_FX);
            this.groupBox70.Location = new System.Drawing.Point(7, 60);
            this.groupBox70.Name = "groupBox70";
            this.groupBox70.Size = new System.Drawing.Size(420, 35);
            this.groupBox70.TabIndex = 132;
            this.groupBox70.TabStop = false;
            this.groupBox70.Text = "SUPPORT AT END";
            // 
            // rbtn_esprt_pinned
            // 
            this.rbtn_esprt_pinned.AutoSize = true;
            this.rbtn_esprt_pinned.Location = new System.Drawing.Point(6, 14);
            this.rbtn_esprt_pinned.Name = "rbtn_esprt_pinned";
            this.rbtn_esprt_pinned.Size = new System.Drawing.Size(69, 17);
            this.rbtn_esprt_pinned.TabIndex = 2;
            this.rbtn_esprt_pinned.Text = "PINNED";
            this.rbtn_esprt_pinned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbtn_esprt_pinned.UseVisualStyleBackColor = true;
            this.rbtn_esprt_pinned.CheckedChanged += new System.EventHandler(this.rbtn_ssprt_pinned_CheckedChanged);
            // 
            // rbtn_esprt_fixed
            // 
            this.rbtn_esprt_fixed.AutoSize = true;
            this.rbtn_esprt_fixed.Checked = true;
            this.rbtn_esprt_fixed.Location = new System.Drawing.Point(81, 14);
            this.rbtn_esprt_fixed.Name = "rbtn_esprt_fixed";
            this.rbtn_esprt_fixed.Size = new System.Drawing.Size(60, 17);
            this.rbtn_esprt_fixed.TabIndex = 1;
            this.rbtn_esprt_fixed.TabStop = true;
            this.rbtn_esprt_fixed.Text = "FIXED";
            this.rbtn_esprt_fixed.UseVisualStyleBackColor = true;
            this.rbtn_esprt_fixed.CheckedChanged += new System.EventHandler(this.rbtn_ssprt_pinned_CheckedChanged);
            // 
            // chk_esprt_fixed_MZ
            // 
            this.chk_esprt_fixed_MZ.AutoSize = true;
            this.chk_esprt_fixed_MZ.Location = new System.Drawing.Point(371, 14);
            this.chk_esprt_fixed_MZ.Name = "chk_esprt_fixed_MZ";
            this.chk_esprt_fixed_MZ.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MZ.TabIndex = 0;
            this.chk_esprt_fixed_MZ.Text = "MZ";
            this.chk_esprt_fixed_MZ.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FZ
            // 
            this.chk_esprt_fixed_FZ.AutoSize = true;
            this.chk_esprt_fixed_FZ.Checked = true;
            this.chk_esprt_fixed_FZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_FZ.Location = new System.Drawing.Point(237, 14);
            this.chk_esprt_fixed_FZ.Name = "chk_esprt_fixed_FZ";
            this.chk_esprt_fixed_FZ.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FZ.TabIndex = 0;
            this.chk_esprt_fixed_FZ.Text = "FZ";
            this.chk_esprt_fixed_FZ.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_MY
            // 
            this.chk_esprt_fixed_MY.AutoSize = true;
            this.chk_esprt_fixed_MY.Checked = true;
            this.chk_esprt_fixed_MY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_MY.Location = new System.Drawing.Point(327, 14);
            this.chk_esprt_fixed_MY.Name = "chk_esprt_fixed_MY";
            this.chk_esprt_fixed_MY.Size = new System.Drawing.Size(42, 17);
            this.chk_esprt_fixed_MY.TabIndex = 0;
            this.chk_esprt_fixed_MY.Text = "MY";
            this.chk_esprt_fixed_MY.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FY
            // 
            this.chk_esprt_fixed_FY.AutoSize = true;
            this.chk_esprt_fixed_FY.Checked = true;
            this.chk_esprt_fixed_FY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_FY.Location = new System.Drawing.Point(193, 14);
            this.chk_esprt_fixed_FY.Name = "chk_esprt_fixed_FY";
            this.chk_esprt_fixed_FY.Size = new System.Drawing.Size(39, 17);
            this.chk_esprt_fixed_FY.TabIndex = 0;
            this.chk_esprt_fixed_FY.Text = "FY";
            this.chk_esprt_fixed_FY.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_MX
            // 
            this.chk_esprt_fixed_MX.AutoSize = true;
            this.chk_esprt_fixed_MX.Checked = true;
            this.chk_esprt_fixed_MX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_MX.Location = new System.Drawing.Point(278, 14);
            this.chk_esprt_fixed_MX.Name = "chk_esprt_fixed_MX";
            this.chk_esprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MX.TabIndex = 0;
            this.chk_esprt_fixed_MX.Text = "MX";
            this.chk_esprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FX
            // 
            this.chk_esprt_fixed_FX.AutoSize = true;
            this.chk_esprt_fixed_FX.Location = new System.Drawing.Point(147, 14);
            this.chk_esprt_fixed_FX.Name = "chk_esprt_fixed_FX";
            this.chk_esprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FX.TabIndex = 0;
            this.chk_esprt_fixed_FX.Text = "FX";
            this.chk_esprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // groupBox109
            // 
            this.groupBox109.Controls.Add(this.cmb_long_open_file);
            this.groupBox109.Controls.Add(this.btn_view_postProcess);
            this.groupBox109.Controls.Add(this.btn_view_report);
            this.groupBox109.Controls.Add(this.btn_view_data);
            this.groupBox109.Controls.Add(this.btn_view_preProcess);
            this.groupBox109.Location = new System.Drawing.Point(7, 150);
            this.groupBox109.Name = "groupBox109";
            this.groupBox109.Size = new System.Drawing.Size(927, 43);
            this.groupBox109.TabIndex = 106;
            this.groupBox109.TabStop = false;
            this.groupBox109.Text = "Open Analysis File";
            // 
            // cmb_long_open_file
            // 
            this.cmb_long_open_file.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_long_open_file.FormattingEnabled = true;
            this.cmb_long_open_file.Items.AddRange(new object[] {
            "Dead Load Analysis",
            "Live Load Analysis",
            "Total DL+SIDL+LL Analysis",
            "Cable Dead Load Analysis",
            "Cable DL + LL Combine Analysis",
            "Cable Live Load Analysis"});
            this.cmb_long_open_file.Location = new System.Drawing.Point(6, 15);
            this.cmb_long_open_file.Name = "cmb_long_open_file";
            this.cmb_long_open_file.Size = new System.Drawing.Size(308, 21);
            this.cmb_long_open_file.TabIndex = 79;
            this.cmb_long_open_file.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            // 
            // btn_view_postProcess
            // 
            this.btn_view_postProcess.Location = new System.Drawing.Point(774, 13);
            this.btn_view_postProcess.Name = "btn_view_postProcess";
            this.btn_view_postProcess.Size = new System.Drawing.Size(146, 22);
            this.btn_view_postProcess.TabIndex = 78;
            this.btn_view_postProcess.Text = "View Post Process";
            this.btn_view_postProcess.UseVisualStyleBackColor = true;
            this.btn_view_postProcess.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(627, 13);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(146, 22);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(320, 13);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(146, 22);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_preProcess
            // 
            this.btn_view_preProcess.Location = new System.Drawing.Point(469, 13);
            this.btn_view_preProcess.Name = "btn_view_preProcess";
            this.btn_view_preProcess.Size = new System.Drawing.Size(146, 22);
            this.btn_view_preProcess.TabIndex = 74;
            this.btn_view_preProcess.Text = "View Pre Process";
            this.btn_view_preProcess.UseVisualStyleBackColor = true;
            this.btn_view_preProcess.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_Process_LL_Analysis
            // 
            this.btn_Process_LL_Analysis.Enabled = false;
            this.btn_Process_LL_Analysis.Location = new System.Drawing.Point(300, 106);
            this.btn_Process_LL_Analysis.Name = "btn_Process_LL_Analysis";
            this.btn_Process_LL_Analysis.Size = new System.Drawing.Size(129, 36);
            this.btn_Process_LL_Analysis.TabIndex = 104;
            this.btn_Process_LL_Analysis.Text = "Process Analysis";
            this.btn_Process_LL_Analysis.UseVisualStyleBackColor = true;
            this.btn_Process_LL_Analysis.Click += new System.EventHandler(this.btn_Ana_LL_process_analysis_Click);
            // 
            // btn_Ana_DL_create_data
            // 
            this.btn_Ana_DL_create_data.Location = new System.Drawing.Point(154, 106);
            this.btn_Ana_DL_create_data.Name = "btn_Ana_DL_create_data";
            this.btn_Ana_DL_create_data.Size = new System.Drawing.Size(143, 36);
            this.btn_Ana_DL_create_data.TabIndex = 46;
            this.btn_Ana_DL_create_data.Text = "Create Analysis Data";
            this.btn_Ana_DL_create_data.UseVisualStyleBackColor = true;
            this.btn_Ana_DL_create_data.Click += new System.EventHandler(this.btn_Ana_create_data_Click);
            // 
            // groupBox71
            // 
            this.groupBox71.Controls.Add(this.rbtn_ssprt_pinned);
            this.groupBox71.Controls.Add(this.rbtn_ssprt_fixed);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_MZ);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_FZ);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_MY);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_FY);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_MX);
            this.groupBox71.Controls.Add(this.chk_ssprt_fixed_FX);
            this.groupBox71.Location = new System.Drawing.Point(7, 16);
            this.groupBox71.Name = "groupBox71";
            this.groupBox71.Size = new System.Drawing.Size(420, 38);
            this.groupBox71.TabIndex = 133;
            this.groupBox71.TabStop = false;
            this.groupBox71.Text = "SUPPORT AT START";
            // 
            // rbtn_ssprt_pinned
            // 
            this.rbtn_ssprt_pinned.AutoSize = true;
            this.rbtn_ssprt_pinned.Checked = true;
            this.rbtn_ssprt_pinned.Location = new System.Drawing.Point(6, 16);
            this.rbtn_ssprt_pinned.Name = "rbtn_ssprt_pinned";
            this.rbtn_ssprt_pinned.Size = new System.Drawing.Size(69, 17);
            this.rbtn_ssprt_pinned.TabIndex = 2;
            this.rbtn_ssprt_pinned.TabStop = true;
            this.rbtn_ssprt_pinned.Text = "PINNED";
            this.rbtn_ssprt_pinned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbtn_ssprt_pinned.UseVisualStyleBackColor = true;
            this.rbtn_ssprt_pinned.CheckedChanged += new System.EventHandler(this.rbtn_ssprt_pinned_CheckedChanged);
            // 
            // rbtn_ssprt_fixed
            // 
            this.rbtn_ssprt_fixed.AutoSize = true;
            this.rbtn_ssprt_fixed.Location = new System.Drawing.Point(81, 16);
            this.rbtn_ssprt_fixed.Name = "rbtn_ssprt_fixed";
            this.rbtn_ssprt_fixed.Size = new System.Drawing.Size(60, 17);
            this.rbtn_ssprt_fixed.TabIndex = 1;
            this.rbtn_ssprt_fixed.Text = "FIXED";
            this.rbtn_ssprt_fixed.UseVisualStyleBackColor = true;
            this.rbtn_ssprt_fixed.CheckedChanged += new System.EventHandler(this.rbtn_ssprt_pinned_CheckedChanged);
            // 
            // chk_ssprt_fixed_MZ
            // 
            this.chk_ssprt_fixed_MZ.AutoSize = true;
            this.chk_ssprt_fixed_MZ.Checked = true;
            this.chk_ssprt_fixed_MZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_MZ.Location = new System.Drawing.Point(369, 16);
            this.chk_ssprt_fixed_MZ.Name = "chk_ssprt_fixed_MZ";
            this.chk_ssprt_fixed_MZ.Size = new System.Drawing.Size(43, 17);
            this.chk_ssprt_fixed_MZ.TabIndex = 0;
            this.chk_ssprt_fixed_MZ.Text = "MZ";
            this.chk_ssprt_fixed_MZ.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FZ
            // 
            this.chk_ssprt_fixed_FZ.AutoSize = true;
            this.chk_ssprt_fixed_FZ.Checked = true;
            this.chk_ssprt_fixed_FZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_FZ.Location = new System.Drawing.Point(237, 16);
            this.chk_ssprt_fixed_FZ.Name = "chk_ssprt_fixed_FZ";
            this.chk_ssprt_fixed_FZ.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FZ.TabIndex = 0;
            this.chk_ssprt_fixed_FZ.Text = "FZ";
            this.chk_ssprt_fixed_FZ.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_MY
            // 
            this.chk_ssprt_fixed_MY.AutoSize = true;
            this.chk_ssprt_fixed_MY.Checked = true;
            this.chk_ssprt_fixed_MY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_MY.Location = new System.Drawing.Point(327, 16);
            this.chk_ssprt_fixed_MY.Name = "chk_ssprt_fixed_MY";
            this.chk_ssprt_fixed_MY.Size = new System.Drawing.Size(42, 17);
            this.chk_ssprt_fixed_MY.TabIndex = 0;
            this.chk_ssprt_fixed_MY.Text = "MY";
            this.chk_ssprt_fixed_MY.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FY
            // 
            this.chk_ssprt_fixed_FY.AutoSize = true;
            this.chk_ssprt_fixed_FY.Checked = true;
            this.chk_ssprt_fixed_FY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_FY.Location = new System.Drawing.Point(193, 16);
            this.chk_ssprt_fixed_FY.Name = "chk_ssprt_fixed_FY";
            this.chk_ssprt_fixed_FY.Size = new System.Drawing.Size(39, 17);
            this.chk_ssprt_fixed_FY.TabIndex = 0;
            this.chk_ssprt_fixed_FY.Text = "FY";
            this.chk_ssprt_fixed_FY.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_MX
            // 
            this.chk_ssprt_fixed_MX.AutoSize = true;
            this.chk_ssprt_fixed_MX.Checked = true;
            this.chk_ssprt_fixed_MX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_MX.Location = new System.Drawing.Point(278, 16);
            this.chk_ssprt_fixed_MX.Name = "chk_ssprt_fixed_MX";
            this.chk_ssprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_ssprt_fixed_MX.TabIndex = 0;
            this.chk_ssprt_fixed_MX.Text = "MX";
            this.chk_ssprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FX
            // 
            this.chk_ssprt_fixed_FX.AutoSize = true;
            this.chk_ssprt_fixed_FX.Checked = true;
            this.chk_ssprt_fixed_FX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ssprt_fixed_FX.Location = new System.Drawing.Point(147, 16);
            this.chk_ssprt_fixed_FX.Name = "chk_ssprt_fixed_FX";
            this.chk_ssprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FX.TabIndex = 0;
            this.chk_ssprt_fixed_FX.Text = "FX";
            this.chk_ssprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.btn_update_force);
            this.groupBox25.Controls.Add(this.chk_M2);
            this.groupBox25.Controls.Add(this.label142);
            this.groupBox25.Controls.Add(this.chk_R3);
            this.groupBox25.Controls.Add(this.chk_M3);
            this.groupBox25.Controls.Add(this.chk_R2);
            this.groupBox25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox25.ForeColor = System.Drawing.Color.Navy;
            this.groupBox25.Location = new System.Drawing.Point(3, 262);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(921, 35);
            this.groupBox25.TabIndex = 106;
            this.groupBox25.TabStop = false;
            this.groupBox25.Visible = false;
            // 
            // btn_update_force
            // 
            this.btn_update_force.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_force.Location = new System.Drawing.Point(578, 12);
            this.btn_update_force.Name = "btn_update_force";
            this.btn_update_force.Size = new System.Drawing.Size(192, 21);
            this.btn_update_force.TabIndex = 99;
            this.btn_update_force.Text = "UPDATE FORCES (Optional)";
            this.btn_update_force.UseVisualStyleBackColor = true;
            this.btn_update_force.Click += new System.EventHandler(this.btn_update_force_Click);
            // 
            // chk_M2
            // 
            this.chk_M2.AutoSize = true;
            this.chk_M2.Checked = true;
            this.chk_M2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_M2.Location = new System.Drawing.Point(516, 14);
            this.chk_M2.Name = "chk_M2";
            this.chk_M2.Size = new System.Drawing.Size(42, 17);
            this.chk_M2.TabIndex = 55;
            this.chk_M2.Text = "M2";
            this.chk_M2.UseVisualStyleBackColor = true;
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label142.Location = new System.Drawing.Point(7, 16);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(319, 13);
            this.label142.TabIndex = 24;
            this.label142.Text = "Maximum Shear Force and Bending Moments";
            // 
            // chk_R3
            // 
            this.chk_R3.AutoSize = true;
            this.chk_R3.Checked = true;
            this.chk_R3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_R3.Location = new System.Drawing.Point(361, 14);
            this.chk_R3.Name = "chk_R3";
            this.chk_R3.Size = new System.Drawing.Size(41, 17);
            this.chk_R3.TabIndex = 55;
            this.chk_R3.Text = "R3";
            this.chk_R3.UseVisualStyleBackColor = true;
            // 
            // chk_M3
            // 
            this.chk_M3.AutoSize = true;
            this.chk_M3.Checked = true;
            this.chk_M3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_M3.Location = new System.Drawing.Point(468, 14);
            this.chk_M3.Name = "chk_M3";
            this.chk_M3.Size = new System.Drawing.Size(42, 17);
            this.chk_M3.TabIndex = 55;
            this.chk_M3.Text = "M3";
            this.chk_M3.UseVisualStyleBackColor = true;
            // 
            // chk_R2
            // 
            this.chk_R2.AutoSize = true;
            this.chk_R2.Checked = true;
            this.chk_R2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_R2.Location = new System.Drawing.Point(408, 14);
            this.chk_R2.Name = "chk_R2";
            this.chk_R2.Size = new System.Drawing.Size(41, 17);
            this.chk_R2.TabIndex = 55;
            this.chk_R2.Text = "R2";
            this.chk_R2.UseVisualStyleBackColor = true;
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage5);
            this.tabControl5.Controls.Add(this.tabPage3);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(0, 0);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(947, 413);
            this.tabControl5.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.AutoScroll = true;
            this.tabPage5.Controls.Add(this.groupBox44);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(939, 387);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Analysis Results";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.label238);
            this.groupBox44.Controls.Add(this.label164);
            this.groupBox44.Controls.Add(this.groupBox11);
            this.groupBox44.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox44.ForeColor = System.Drawing.Color.Red;
            this.groupBox44.Location = new System.Drawing.Point(3, 3);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(933, 453);
            this.groupBox44.TabIndex = 94;
            this.groupBox44.TabStop = false;
            // 
            // label238
            // 
            this.label238.AutoSize = true;
            this.label238.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label238.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label238.ForeColor = System.Drawing.Color.Green;
            this.label238.Location = new System.Drawing.Point(76, 17);
            this.label238.Name = "label238";
            this.label238.Size = new System.Drawing.Size(229, 20);
            this.label238.TabIndex = 123;
            this.label238.Text = "No User Input in this page";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label164.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label164.ForeColor = System.Drawing.Color.Red;
            this.label164.Location = new System.Drawing.Point(321, 17);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(273, 20);
            this.label164.TabIndex = 122;
            this.label164.Text = "Calculated Values from Analysis";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.groupBox7);
            this.groupBox11.Controls.Add(this.txt_max_delf);
            this.groupBox11.Controls.Add(this.label499);
            this.groupBox11.Controls.Add(this.lbl_max_delf);
            this.groupBox11.Controls.Add(this.label500);
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Controls.Add(this.groupBox14);
            this.groupBox11.Location = new System.Drawing.Point(3, 44);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(896, 295);
            this.groupBox11.TabIndex = 106;
            this.groupBox11.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.groupBox16);
            this.groupBox7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(773, 79);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(78, 42);
            this.groupBox7.TabIndex = 105;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Super Imposed Dead Load [SIDL+FPLL] Results";
            this.groupBox7.Visible = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.groupBox13);
            this.groupBox10.Controls.Add(this.groupBox18);
            this.groupBox10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(8, 435);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(390, 93);
            this.groupBox10.TabIndex = 102;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Analysis Result for Dead Load";
            this.groupBox10.Visible = false;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label52);
            this.groupBox13.Controls.Add(this.label53);
            this.groupBox13.Controls.Add(this.label54);
            this.groupBox13.Controls.Add(this.label55);
            this.groupBox13.Controls.Add(this.textBox13);
            this.groupBox13.Controls.Add(this.textBox14);
            this.groupBox13.Location = new System.Drawing.Point(13, 16);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(183, 74);
            this.groupBox13.TabIndex = 81;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Inner Main Girder";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(132, 48);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(27, 13);
            this.label52.TabIndex = 30;
            this.label52.Text = "Ton";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(4, 48);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(41, 13);
            this.label53.TabIndex = 28;
            this.label53.Text = "Shear";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(132, 22);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(43, 13);
            this.label54.TabIndex = 30;
            this.label54.Text = "Ton-m";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(4, 23);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(52, 13);
            this.label55.TabIndex = 27;
            this.label55.Text = "Moment";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(62, 20);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(64, 21);
            this.textBox13.TabIndex = 25;
            this.textBox13.Text = "0";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(62, 45);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(64, 21);
            this.textBox14.TabIndex = 20;
            this.textBox14.Text = "0";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label56);
            this.groupBox18.Controls.Add(this.label57);
            this.groupBox18.Controls.Add(this.label58);
            this.groupBox18.Controls.Add(this.label61);
            this.groupBox18.Controls.Add(this.textBox15);
            this.groupBox18.Controls.Add(this.textBox16);
            this.groupBox18.Location = new System.Drawing.Point(203, 16);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(182, 74);
            this.groupBox18.TabIndex = 82;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Outer Main Girder";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(138, 54);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(27, 13);
            this.label56.TabIndex = 30;
            this.label56.Text = "Ton";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(7, 52);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(41, 13);
            this.label57.TabIndex = 28;
            this.label57.Text = "Shear";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(7, 23);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(52, 13);
            this.label58.TabIndex = 27;
            this.label58.Text = "Moment";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(138, 23);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(43, 13);
            this.label61.TabIndex = 30;
            this.label61.Text = "Ton-m";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(68, 49);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(64, 21);
            this.textBox15.TabIndex = 20;
            this.textBox15.Text = "0";
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(68, 20);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(64, 21);
            this.textBox16.TabIndex = 25;
            this.textBox16.Text = "0";
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label30);
            this.groupBox16.Controls.Add(this.label160);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_support_shear);
            this.groupBox16.Controls.Add(this.label161);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_support_moment);
            this.groupBox16.Controls.Add(this.label181);
            this.groupBox16.Controls.Add(this.label31);
            this.groupBox16.Controls.Add(this.label183);
            this.groupBox16.Controls.Add(this.label34);
            this.groupBox16.Controls.Add(this.label185);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L8_shear);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L8_moment);
            this.groupBox16.Controls.Add(this.label187);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_3L_8_shear);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L2_shear);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_3L_8_moment);
            this.groupBox16.Controls.Add(this.label190);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_deff_shear);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L2_moment);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_deff_moment);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L4_shear);
            this.groupBox16.Controls.Add(this.txt_Ana_live_outer_long_L4_moment);
            this.groupBox16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox16.Location = new System.Drawing.Point(3, 17);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(72, 22);
            this.groupBox16.TabIndex = 82;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Maximum Member Forces [ Load Case 2 ]";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(10, 56);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 13);
            this.label30.TabIndex = 46;
            this.label30.Text = "Support";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.ForeColor = System.Drawing.Color.Blue;
            this.label160.Location = new System.Drawing.Point(200, 31);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(34, 13);
            this.label160.TabIndex = 35;
            this.label160.Text = "(kip)";
            // 
            // txt_Ana_live_outer_long_support_shear
            // 
            this.txt_Ana_live_outer_long_support_shear.Location = new System.Drawing.Point(195, 51);
            this.txt_Ana_live_outer_long_support_shear.Name = "txt_Ana_live_outer_long_support_shear";
            this.txt_Ana_live_outer_long_support_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_support_shear.TabIndex = 48;
            this.txt_Ana_live_outer_long_support_shear.Text = "0";
            this.txt_Ana_live_outer_long_support_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.ForeColor = System.Drawing.Color.Blue;
            this.label161.Location = new System.Drawing.Point(84, 33);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(47, 13);
            this.label161.TabIndex = 36;
            this.label161.Text = "(kip-ft)";
            // 
            // txt_Ana_live_outer_long_support_moment
            // 
            this.txt_Ana_live_outer_long_support_moment.Location = new System.Drawing.Point(80, 51);
            this.txt_Ana_live_outer_long_support_moment.Name = "txt_Ana_live_outer_long_support_moment";
            this.txt_Ana_live_outer_long_support_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_support_moment.TabIndex = 47;
            this.txt_Ana_live_outer_long_support_moment.Text = "0";
            this.txt_Ana_live_outer_long_support_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Location = new System.Drawing.Point(12, 83);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(31, 13);
            this.label181.TabIndex = 18;
            this.label181.Text = "Deff";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(9, 163);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(32, 13);
            this.label31.TabIndex = 40;
            this.label31.Text = "3L/8";
            // 
            // label183
            // 
            this.label183.AutoSize = true;
            this.label183.Location = new System.Drawing.Point(11, 189);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(25, 13);
            this.label183.TabIndex = 24;
            this.label183.Text = "L/2";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 105);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(25, 13);
            this.label34.TabIndex = 43;
            this.label34.Text = "L/8";
            // 
            // label185
            // 
            this.label185.AutoSize = true;
            this.label185.Location = new System.Drawing.Point(200, 15);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(41, 13);
            this.label185.TabIndex = 28;
            this.label185.Text = "Shear";
            // 
            // txt_Ana_live_outer_long_L8_shear
            // 
            this.txt_Ana_live_outer_long_L8_shear.Location = new System.Drawing.Point(195, 104);
            this.txt_Ana_live_outer_long_L8_shear.Name = "txt_Ana_live_outer_long_L8_shear";
            this.txt_Ana_live_outer_long_L8_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L8_shear.TabIndex = 45;
            this.txt_Ana_live_outer_long_L8_shear.Text = "0";
            this.txt_Ana_live_outer_long_L8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L8_moment
            // 
            this.txt_Ana_live_outer_long_L8_moment.Location = new System.Drawing.Point(80, 104);
            this.txt_Ana_live_outer_long_L8_moment.Name = "txt_Ana_live_outer_long_L8_moment";
            this.txt_Ana_live_outer_long_L8_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L8_moment.TabIndex = 44;
            this.txt_Ana_live_outer_long_L8_moment.Text = "0";
            this.txt_Ana_live_outer_long_L8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Location = new System.Drawing.Point(84, 17);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(52, 13);
            this.label187.TabIndex = 27;
            this.label187.Text = "Moment";
            // 
            // txt_Ana_live_outer_long_3L_8_shear
            // 
            this.txt_Ana_live_outer_long_3L_8_shear.Location = new System.Drawing.Point(195, 158);
            this.txt_Ana_live_outer_long_3L_8_shear.Name = "txt_Ana_live_outer_long_3L_8_shear";
            this.txt_Ana_live_outer_long_3L_8_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_3L_8_shear.TabIndex = 42;
            this.txt_Ana_live_outer_long_3L_8_shear.Text = "0";
            this.txt_Ana_live_outer_long_3L_8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L2_shear
            // 
            this.txt_Ana_live_outer_long_L2_shear.Location = new System.Drawing.Point(195, 184);
            this.txt_Ana_live_outer_long_L2_shear.Name = "txt_Ana_live_outer_long_L2_shear";
            this.txt_Ana_live_outer_long_L2_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L2_shear.TabIndex = 26;
            this.txt_Ana_live_outer_long_L2_shear.Text = "0";
            this.txt_Ana_live_outer_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_3L_8_moment
            // 
            this.txt_Ana_live_outer_long_3L_8_moment.Location = new System.Drawing.Point(80, 158);
            this.txt_Ana_live_outer_long_3L_8_moment.Name = "txt_Ana_live_outer_long_3L_8_moment";
            this.txt_Ana_live_outer_long_3L_8_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_3L_8_moment.TabIndex = 41;
            this.txt_Ana_live_outer_long_3L_8_moment.Text = "0";
            this.txt_Ana_live_outer_long_3L_8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Location = new System.Drawing.Point(11, 136);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(25, 13);
            this.label190.TabIndex = 21;
            this.label190.Text = "L/4";
            // 
            // txt_Ana_live_outer_long_deff_shear
            // 
            this.txt_Ana_live_outer_long_deff_shear.Location = new System.Drawing.Point(195, 77);
            this.txt_Ana_live_outer_long_deff_shear.Name = "txt_Ana_live_outer_long_deff_shear";
            this.txt_Ana_live_outer_long_deff_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_deff_shear.TabIndex = 20;
            this.txt_Ana_live_outer_long_deff_shear.Text = "0";
            this.txt_Ana_live_outer_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L2_moment
            // 
            this.txt_Ana_live_outer_long_L2_moment.Location = new System.Drawing.Point(80, 186);
            this.txt_Ana_live_outer_long_L2_moment.Name = "txt_Ana_live_outer_long_L2_moment";
            this.txt_Ana_live_outer_long_L2_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L2_moment.TabIndex = 25;
            this.txt_Ana_live_outer_long_L2_moment.Text = "0";
            this.txt_Ana_live_outer_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_deff_moment
            // 
            this.txt_Ana_live_outer_long_deff_moment.Location = new System.Drawing.Point(80, 79);
            this.txt_Ana_live_outer_long_deff_moment.Name = "txt_Ana_live_outer_long_deff_moment";
            this.txt_Ana_live_outer_long_deff_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_deff_moment.TabIndex = 19;
            this.txt_Ana_live_outer_long_deff_moment.Text = "0";
            this.txt_Ana_live_outer_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L4_shear
            // 
            this.txt_Ana_live_outer_long_L4_shear.Location = new System.Drawing.Point(195, 131);
            this.txt_Ana_live_outer_long_L4_shear.Name = "txt_Ana_live_outer_long_L4_shear";
            this.txt_Ana_live_outer_long_L4_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L4_shear.TabIndex = 23;
            this.txt_Ana_live_outer_long_L4_shear.Text = "0";
            this.txt_Ana_live_outer_long_L4_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L4_moment
            // 
            this.txt_Ana_live_outer_long_L4_moment.Location = new System.Drawing.Point(80, 133);
            this.txt_Ana_live_outer_long_L4_moment.Name = "txt_Ana_live_outer_long_L4_moment";
            this.txt_Ana_live_outer_long_L4_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L4_moment.TabIndex = 22;
            this.txt_Ana_live_outer_long_L4_moment.Text = "0";
            this.txt_Ana_live_outer_long_L4_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_max_delf
            // 
            this.txt_max_delf.Location = new System.Drawing.Point(401, 268);
            this.txt_max_delf.Name = "txt_max_delf";
            this.txt_max_delf.Size = new System.Drawing.Size(83, 21);
            this.txt_max_delf.TabIndex = 135;
            this.txt_max_delf.Text = "0";
            this.txt_max_delf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label499
            // 
            this.label499.AutoSize = true;
            this.label499.Location = new System.Drawing.Point(490, 271);
            this.label499.Name = "label499";
            this.label499.Size = new System.Drawing.Size(15, 13);
            this.label499.TabIndex = 132;
            this.label499.Text = "ft";
            // 
            // lbl_max_delf
            // 
            this.lbl_max_delf.AutoSize = true;
            this.lbl_max_delf.Location = new System.Drawing.Point(544, 271);
            this.lbl_max_delf.Name = "lbl_max_delf";
            this.lbl_max_delf.Size = new System.Drawing.Size(79, 13);
            this.lbl_max_delf.TabIndex = 133;
            this.lbl_max_delf.Text = "Span/800 = ";
            // 
            // label500
            // 
            this.label500.AutoSize = true;
            this.label500.Location = new System.Drawing.Point(261, 271);
            this.label500.Name = "label500";
            this.label500.Size = new System.Drawing.Size(137, 13);
            this.label500.TabIndex = 134;
            this.label500.Text = "Max Vertical Deflection";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.grb_Ana_Res_DL);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(184, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 236);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dead Load [DL] Results";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.groupBox17);
            this.groupBox14.Controls.Add(this.grb_Ana_Res_LL);
            this.groupBox14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.ForeColor = System.Drawing.Color.Red;
            this.groupBox14.Location = new System.Drawing.Point(469, 14);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(278, 236);
            this.groupBox14.TabIndex = 105;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Moving Load / Live Load [LL] Results";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label39);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_support_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_support_moment);
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.label41);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L8_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L8_moment);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_3L_8_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_3L_8_moment);
            this.groupBox15.Controls.Add(this.label154);
            this.groupBox15.Controls.Add(this.label322);
            this.groupBox15.Controls.Add(this.label156);
            this.groupBox15.Controls.Add(this.label158);
            this.groupBox15.Controls.Add(this.label159);
            this.groupBox15.Controls.Add(this.label173);
            this.groupBox15.Controls.Add(this.label174);
            this.groupBox15.Controls.Add(this.label179);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L4_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L2_moment);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L2_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_L4_moment);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_deff_shear);
            this.groupBox15.Controls.Add(this.txt_Ana_live_inner_long_deff_moment);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox15.Location = new System.Drawing.Point(3, 17);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(272, 216);
            this.groupBox15.TabIndex = 81;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Maximum Member Forces";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(10, 54);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(52, 13);
            this.label39.TabIndex = 46;
            this.label39.Text = "Support";
            // 
            // txt_Ana_live_inner_long_support_shear
            // 
            this.txt_Ana_live_inner_long_support_shear.Location = new System.Drawing.Point(172, 51);
            this.txt_Ana_live_inner_long_support_shear.Name = "txt_Ana_live_inner_long_support_shear";
            this.txt_Ana_live_inner_long_support_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_support_shear.TabIndex = 48;
            this.txt_Ana_live_inner_long_support_shear.Text = "0";
            this.txt_Ana_live_inner_long_support_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_support_moment
            // 
            this.txt_Ana_live_inner_long_support_moment.Location = new System.Drawing.Point(68, 51);
            this.txt_Ana_live_inner_long_support_moment.Name = "txt_Ana_live_inner_long_support_moment";
            this.txt_Ana_live_inner_long_support_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_support_moment.TabIndex = 47;
            this.txt_Ana_live_inner_long_support_moment.Text = "0";
            this.txt_Ana_live_inner_long_support_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(14, 159);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(32, 13);
            this.label40.TabIndex = 40;
            this.label40.Text = "3L/8";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(12, 104);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(25, 13);
            this.label41.TabIndex = 43;
            this.label41.Text = "L/8";
            // 
            // txt_Ana_live_inner_long_L8_shear
            // 
            this.txt_Ana_live_inner_long_L8_shear.Location = new System.Drawing.Point(172, 102);
            this.txt_Ana_live_inner_long_L8_shear.Name = "txt_Ana_live_inner_long_L8_shear";
            this.txt_Ana_live_inner_long_L8_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L8_shear.TabIndex = 45;
            this.txt_Ana_live_inner_long_L8_shear.Text = "0";
            this.txt_Ana_live_inner_long_L8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_L8_moment
            // 
            this.txt_Ana_live_inner_long_L8_moment.Location = new System.Drawing.Point(68, 102);
            this.txt_Ana_live_inner_long_L8_moment.Name = "txt_Ana_live_inner_long_L8_moment";
            this.txt_Ana_live_inner_long_L8_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L8_moment.TabIndex = 44;
            this.txt_Ana_live_inner_long_L8_moment.Text = "0";
            this.txt_Ana_live_inner_long_L8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_3L_8_shear
            // 
            this.txt_Ana_live_inner_long_3L_8_shear.Location = new System.Drawing.Point(172, 156);
            this.txt_Ana_live_inner_long_3L_8_shear.Name = "txt_Ana_live_inner_long_3L_8_shear";
            this.txt_Ana_live_inner_long_3L_8_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_3L_8_shear.TabIndex = 42;
            this.txt_Ana_live_inner_long_3L_8_shear.Text = "0";
            this.txt_Ana_live_inner_long_3L_8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_3L_8_moment
            // 
            this.txt_Ana_live_inner_long_3L_8_moment.Location = new System.Drawing.Point(68, 156);
            this.txt_Ana_live_inner_long_3L_8_moment.Name = "txt_Ana_live_inner_long_3L_8_moment";
            this.txt_Ana_live_inner_long_3L_8_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_3L_8_moment.TabIndex = 41;
            this.txt_Ana_live_inner_long_3L_8_moment.Text = "0";
            this.txt_Ana_live_inner_long_3L_8_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.ForeColor = System.Drawing.Color.Blue;
            this.label154.Location = new System.Drawing.Point(193, 32);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(34, 13);
            this.label154.TabIndex = 33;
            this.label154.Text = "(kip)";
            // 
            // label322
            // 
            this.label322.AutoSize = true;
            this.label322.Location = new System.Drawing.Point(193, 19);
            this.label322.Name = "label322";
            this.label322.Size = new System.Drawing.Size(41, 13);
            this.label322.TabIndex = 28;
            this.label322.Text = "Shear";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(409, -17);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(41, 13);
            this.label156.TabIndex = 32;
            this.label156.Text = "Shear";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.ForeColor = System.Drawing.Color.Blue;
            this.label158.Location = new System.Drawing.Point(73, 32);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(47, 13);
            this.label158.TabIndex = 34;
            this.label158.Text = "(kip-ft)";
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(73, 19);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(52, 13);
            this.label159.TabIndex = 31;
            this.label159.Text = "Moment";
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(12, 78);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(31, 13);
            this.label173.TabIndex = 18;
            this.label173.Text = "Deff";
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(14, 186);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(25, 13);
            this.label174.TabIndex = 24;
            this.label174.Text = "L/2";
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(14, 133);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(25, 13);
            this.label179.TabIndex = 21;
            this.label179.Text = "L/4";
            // 
            // txt_Ana_live_inner_long_L4_shear
            // 
            this.txt_Ana_live_inner_long_L4_shear.Location = new System.Drawing.Point(172, 129);
            this.txt_Ana_live_inner_long_L4_shear.Name = "txt_Ana_live_inner_long_L4_shear";
            this.txt_Ana_live_inner_long_L4_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L4_shear.TabIndex = 23;
            this.txt_Ana_live_inner_long_L4_shear.Text = "0";
            this.txt_Ana_live_inner_long_L4_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_L2_moment
            // 
            this.txt_Ana_live_inner_long_L2_moment.Location = new System.Drawing.Point(68, 183);
            this.txt_Ana_live_inner_long_L2_moment.Name = "txt_Ana_live_inner_long_L2_moment";
            this.txt_Ana_live_inner_long_L2_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L2_moment.TabIndex = 25;
            this.txt_Ana_live_inner_long_L2_moment.Text = "0";
            this.txt_Ana_live_inner_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_L2_shear
            // 
            this.txt_Ana_live_inner_long_L2_shear.Location = new System.Drawing.Point(172, 182);
            this.txt_Ana_live_inner_long_L2_shear.Name = "txt_Ana_live_inner_long_L2_shear";
            this.txt_Ana_live_inner_long_L2_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L2_shear.TabIndex = 26;
            this.txt_Ana_live_inner_long_L2_shear.Text = "0";
            this.txt_Ana_live_inner_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_L4_moment
            // 
            this.txt_Ana_live_inner_long_L4_moment.Location = new System.Drawing.Point(68, 130);
            this.txt_Ana_live_inner_long_L4_moment.Name = "txt_Ana_live_inner_long_L4_moment";
            this.txt_Ana_live_inner_long_L4_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_L4_moment.TabIndex = 22;
            this.txt_Ana_live_inner_long_L4_moment.Text = "0";
            this.txt_Ana_live_inner_long_L4_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_deff_shear
            // 
            this.txt_Ana_live_inner_long_deff_shear.Location = new System.Drawing.Point(171, 75);
            this.txt_Ana_live_inner_long_deff_shear.Name = "txt_Ana_live_inner_long_deff_shear";
            this.txt_Ana_live_inner_long_deff_shear.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_deff_shear.TabIndex = 20;
            this.txt_Ana_live_inner_long_deff_shear.Text = "0";
            this.txt_Ana_live_inner_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_inner_long_deff_moment
            // 
            this.txt_Ana_live_inner_long_deff_moment.Location = new System.Drawing.Point(67, 76);
            this.txt_Ana_live_inner_long_deff_moment.Name = "txt_Ana_live_inner_long_deff_moment";
            this.txt_Ana_live_inner_long_deff_moment.Size = new System.Drawing.Size(89, 21);
            this.txt_Ana_live_inner_long_deff_moment.TabIndex = 19;
            this.txt_Ana_live_inner_long_deff_moment.Text = "0";
            this.txt_Ana_live_inner_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label191);
            this.groupBox17.Controls.Add(this.label192);
            this.groupBox17.Controls.Add(this.label193);
            this.groupBox17.Controls.Add(this.label194);
            this.groupBox17.Controls.Add(this.txt_Ana_live_cross_max_shear);
            this.groupBox17.Controls.Add(this.txt_Ana_live_cross_max_moment);
            this.groupBox17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox17.Location = new System.Drawing.Point(12, 256);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(360, 56);
            this.groupBox17.TabIndex = 82;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Cross Girder";
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Location = new System.Drawing.Point(137, 34);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(29, 13);
            this.label191.TabIndex = 31;
            this.label191.Text = "T-m";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Location = new System.Drawing.Point(279, 34);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(27, 13);
            this.label192.TabIndex = 30;
            this.label192.Text = "Ton";
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Location = new System.Drawing.Point(224, 15);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(41, 13);
            this.label193.TabIndex = 28;
            this.label193.Text = "Shear";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Location = new System.Drawing.Point(81, 15);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(52, 13);
            this.label194.TabIndex = 27;
            this.label194.Text = "Moment";
            // 
            // txt_Ana_live_cross_max_shear
            // 
            this.txt_Ana_live_cross_max_shear.Location = new System.Drawing.Point(209, 31);
            this.txt_Ana_live_cross_max_shear.Name = "txt_Ana_live_cross_max_shear";
            this.txt_Ana_live_cross_max_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_live_cross_max_shear.TabIndex = 20;
            this.txt_Ana_live_cross_max_shear.Text = "0";
            this.txt_Ana_live_cross_max_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_cross_max_moment
            // 
            this.txt_Ana_live_cross_max_moment.Location = new System.Drawing.Point(67, 31);
            this.txt_Ana_live_cross_max_moment.Name = "txt_Ana_live_cross_max_moment";
            this.txt_Ana_live_cross_max_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_live_cross_max_moment.TabIndex = 19;
            this.txt_Ana_live_cross_max_moment.Text = "0";
            this.txt_Ana_live_cross_max_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_Ana_Res_LL
            // 
            this.grb_Ana_Res_LL.Controls.Add(this.groupBox4);
            this.grb_Ana_Res_LL.Controls.Add(this.groupBox5);
            this.grb_Ana_Res_LL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Ana_Res_LL.Location = new System.Drawing.Point(14, 434);
            this.grb_Ana_Res_LL.Name = "grb_Ana_Res_LL";
            this.grb_Ana_Res_LL.Size = new System.Drawing.Size(388, 71);
            this.grb_Ana_Res_LL.TabIndex = 103;
            this.grb_Ana_Res_LL.TabStop = false;
            this.grb_Ana_Res_LL.Text = "Analysis Result for Live Load";
            this.grb_Ana_Res_LL.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txt_Ana_LL_inner_long_L2_moment);
            this.groupBox4.Controls.Add(this.txt_Ana_LL_inner_long_deff_shear);
            this.groupBox4.Location = new System.Drawing.Point(13, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(183, 74);
            this.groupBox4.TabIndex = 81;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Inner Main Girder";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Ton";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Shear";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(132, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Ton-m";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Moment";
            // 
            // txt_Ana_LL_inner_long_L2_moment
            // 
            this.txt_Ana_LL_inner_long_L2_moment.Location = new System.Drawing.Point(62, 20);
            this.txt_Ana_LL_inner_long_L2_moment.Name = "txt_Ana_LL_inner_long_L2_moment";
            this.txt_Ana_LL_inner_long_L2_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_LL_inner_long_L2_moment.TabIndex = 25;
            this.txt_Ana_LL_inner_long_L2_moment.Text = "0";
            this.txt_Ana_LL_inner_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_LL_inner_long_deff_shear
            // 
            this.txt_Ana_LL_inner_long_deff_shear.Location = new System.Drawing.Point(62, 45);
            this.txt_Ana_LL_inner_long_deff_shear.Name = "txt_Ana_LL_inner_long_deff_shear";
            this.txt_Ana_LL_inner_long_deff_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_LL_inner_long_deff_shear.TabIndex = 20;
            this.txt_Ana_LL_inner_long_deff_shear.Text = "0";
            this.txt_Ana_LL_inner_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txt_Ana_LL_outer_long_deff_shear);
            this.groupBox5.Controls.Add(this.txt_Ana_LL_outer_long_L2_moment);
            this.groupBox5.Location = new System.Drawing.Point(203, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(182, 74);
            this.groupBox5.TabIndex = 82;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Outer Main Girder";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(138, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Ton";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Shear";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "Moment";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(138, 23);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(43, 13);
            this.label23.TabIndex = 30;
            this.label23.Text = "Ton-m";
            // 
            // txt_Ana_LL_outer_long_deff_shear
            // 
            this.txt_Ana_LL_outer_long_deff_shear.Location = new System.Drawing.Point(68, 49);
            this.txt_Ana_LL_outer_long_deff_shear.Name = "txt_Ana_LL_outer_long_deff_shear";
            this.txt_Ana_LL_outer_long_deff_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_LL_outer_long_deff_shear.TabIndex = 20;
            this.txt_Ana_LL_outer_long_deff_shear.Text = "0";
            this.txt_Ana_LL_outer_long_deff_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_LL_outer_long_L2_moment
            // 
            this.txt_Ana_LL_outer_long_L2_moment.Location = new System.Drawing.Point(68, 20);
            this.txt_Ana_LL_outer_long_L2_moment.Name = "txt_Ana_LL_outer_long_L2_moment";
            this.txt_Ana_LL_outer_long_L2_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_LL_outer_long_L2_moment.TabIndex = 25;
            this.txt_Ana_LL_outer_long_L2_moment.Text = "0";
            this.txt_Ana_LL_outer_long_L2_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(939, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reaction Forces";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(933, 381);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.uC_SR_DL);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(925, 355);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Support Reactions [DL]";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // uC_SR_DL
            // 
            this.uC_SR_DL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uC_SR_DL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_SR_DL.Location = new System.Drawing.Point(3, 6);
            this.uC_SR_DL.Name = "uC_SR_DL";
            this.uC_SR_DL.Size = new System.Drawing.Size(898, 455);
            this.uC_SR_DL.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.AutoScroll = true;
            this.tabPage8.Controls.Add(this.uC_SR_LL);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(925, 355);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "Support Reactions [LL]";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // uC_SR_LL
            // 
            this.uC_SR_LL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uC_SR_LL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_SR_LL.Location = new System.Drawing.Point(3, 3);
            this.uC_SR_LL.Name = "uC_SR_LL";
            this.uC_SR_LL.Size = new System.Drawing.Size(898, 455);
            this.uC_SR_LL.TabIndex = 1;
            // 
            // tabPage9
            // 
            this.tabPage9.AutoScroll = true;
            this.tabPage9.Controls.Add(this.uC_SR_Max);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(925, 355);
            this.tabPage9.TabIndex = 3;
            this.tabPage9.Text = "Max Forces";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // uC_SR_Max
            // 
            this.uC_SR_Max.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uC_SR_Max.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_SR_Max.Location = new System.Drawing.Point(13, 3);
            this.uC_SR_Max.Name = "uC_SR_Max";
            this.uC_SR_Max.Size = new System.Drawing.Size(882, 385);
            this.uC_SR_Max.TabIndex = 0;
            // 
            // tab_stage
            // 
            this.tab_stage.Controls.Add(this.tc_stage);
            this.tab_stage.Location = new System.Drawing.Point(4, 22);
            this.tab_stage.Name = "tab_stage";
            this.tab_stage.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage.Size = new System.Drawing.Size(955, 634);
            this.tab_stage.TabIndex = 7;
            this.tab_stage.Text = "Stage Analysis";
            this.tab_stage.UseVisualStyleBackColor = true;
            // 
            // tc_stage
            // 
            this.tc_stage.Controls.Add(this.tab_stage1);
            this.tc_stage.Controls.Add(this.tab_stage2);
            this.tc_stage.Controls.Add(this.tab_stage3);
            this.tc_stage.Controls.Add(this.tab_stage4);
            this.tc_stage.Controls.Add(this.tab_stage5);
            this.tc_stage.Controls.Add(this.tab_designSage);
            this.tc_stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_stage.Location = new System.Drawing.Point(3, 3);
            this.tc_stage.Name = "tc_stage";
            this.tc_stage.SelectedIndex = 0;
            this.tc_stage.Size = new System.Drawing.Size(949, 628);
            this.tc_stage.TabIndex = 0;
            // 
            // tab_stage1
            // 
            this.tab_stage1.Controls.Add(this.uC_Stage_Extradosed_LRFD1);
            this.tab_stage1.Location = new System.Drawing.Point(4, 22);
            this.tab_stage1.Name = "tab_stage1";
            this.tab_stage1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage1.Size = new System.Drawing.Size(941, 602);
            this.tab_stage1.TabIndex = 0;
            this.tab_stage1.Text = "STAGE 1";
            this.tab_stage1.UseVisualStyleBackColor = true;
            // 
            // uC_Stage_Extradosed_LRFD1
            // 
            this.uC_Stage_Extradosed_LRFD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stage_Extradosed_LRFD1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stage_Extradosed_LRFD1.Location = new System.Drawing.Point(3, 3);
            this.uC_Stage_Extradosed_LRFD1.Name = "uC_Stage_Extradosed_LRFD1";
            this.uC_Stage_Extradosed_LRFD1.Size = new System.Drawing.Size(935, 596);
            this.uC_Stage_Extradosed_LRFD1.TabIndex = 0;
            this.uC_Stage_Extradosed_LRFD1.OnButtonClick += new System.EventHandler(this.btn_stage_data_Click);
            this.uC_Stage_Extradosed_LRFD1.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_stage_file_SelectedIndexChanged);
            this.uC_Stage_Extradosed_LRFD1.OnTextBoxTextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            this.uC_Stage_Extradosed_LRFD1.OnEModTextChanged += new System.EventHandler(this.uC_Stage_Extradosed_LRFD1_OnEModTextChanged);
            // 
            // tab_stage2
            // 
            this.tab_stage2.Controls.Add(this.uC_Stage_Extradosed_LRFD2);
            this.tab_stage2.Location = new System.Drawing.Point(4, 22);
            this.tab_stage2.Name = "tab_stage2";
            this.tab_stage2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage2.Size = new System.Drawing.Size(941, 602);
            this.tab_stage2.TabIndex = 1;
            this.tab_stage2.Text = "STAGE 2";
            this.tab_stage2.UseVisualStyleBackColor = true;
            // 
            // uC_Stage_Extradosed_LRFD2
            // 
            this.uC_Stage_Extradosed_LRFD2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stage_Extradosed_LRFD2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stage_Extradosed_LRFD2.Location = new System.Drawing.Point(3, 3);
            this.uC_Stage_Extradosed_LRFD2.Name = "uC_Stage_Extradosed_LRFD2";
            this.uC_Stage_Extradosed_LRFD2.Size = new System.Drawing.Size(935, 596);
            this.uC_Stage_Extradosed_LRFD2.TabIndex = 0;
            this.uC_Stage_Extradosed_LRFD2.OnButtonClick += new System.EventHandler(this.btn_stage_data_Click);
            this.uC_Stage_Extradosed_LRFD2.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_stage_file_SelectedIndexChanged);
            this.uC_Stage_Extradosed_LRFD2.OnTextBoxTextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            this.uC_Stage_Extradosed_LRFD2.OnEModTextChanged += new System.EventHandler(this.uC_Stage_Extradosed_LRFD1_OnEModTextChanged);
            // 
            // tab_stage3
            // 
            this.tab_stage3.Controls.Add(this.uC_Stage_Extradosed_LRFD3);
            this.tab_stage3.Location = new System.Drawing.Point(4, 22);
            this.tab_stage3.Name = "tab_stage3";
            this.tab_stage3.Size = new System.Drawing.Size(941, 602);
            this.tab_stage3.TabIndex = 2;
            this.tab_stage3.Text = "STAGE 3";
            this.tab_stage3.UseVisualStyleBackColor = true;
            // 
            // uC_Stage_Extradosed_LRFD3
            // 
            this.uC_Stage_Extradosed_LRFD3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stage_Extradosed_LRFD3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stage_Extradosed_LRFD3.Location = new System.Drawing.Point(0, 0);
            this.uC_Stage_Extradosed_LRFD3.Name = "uC_Stage_Extradosed_LRFD3";
            this.uC_Stage_Extradosed_LRFD3.Size = new System.Drawing.Size(941, 602);
            this.uC_Stage_Extradosed_LRFD3.TabIndex = 0;
            this.uC_Stage_Extradosed_LRFD3.OnButtonClick += new System.EventHandler(this.btn_stage_data_Click);
            this.uC_Stage_Extradosed_LRFD3.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_stage_file_SelectedIndexChanged);
            this.uC_Stage_Extradosed_LRFD3.OnTextBoxTextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            this.uC_Stage_Extradosed_LRFD3.OnEModTextChanged += new System.EventHandler(this.uC_Stage_Extradosed_LRFD1_OnEModTextChanged);
            // 
            // tab_stage4
            // 
            this.tab_stage4.Controls.Add(this.uC_Stage_Extradosed_LRFD4);
            this.tab_stage4.Location = new System.Drawing.Point(4, 22);
            this.tab_stage4.Name = "tab_stage4";
            this.tab_stage4.Size = new System.Drawing.Size(941, 602);
            this.tab_stage4.TabIndex = 3;
            this.tab_stage4.Text = "STAGE 4";
            this.tab_stage4.UseVisualStyleBackColor = true;
            // 
            // uC_Stage_Extradosed_LRFD4
            // 
            this.uC_Stage_Extradosed_LRFD4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stage_Extradosed_LRFD4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stage_Extradosed_LRFD4.Location = new System.Drawing.Point(0, 0);
            this.uC_Stage_Extradosed_LRFD4.Name = "uC_Stage_Extradosed_LRFD4";
            this.uC_Stage_Extradosed_LRFD4.Size = new System.Drawing.Size(941, 602);
            this.uC_Stage_Extradosed_LRFD4.TabIndex = 0;
            this.uC_Stage_Extradosed_LRFD4.OnButtonClick += new System.EventHandler(this.btn_stage_data_Click);
            this.uC_Stage_Extradosed_LRFD4.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_stage_file_SelectedIndexChanged);
            this.uC_Stage_Extradosed_LRFD4.OnTextBoxTextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            this.uC_Stage_Extradosed_LRFD4.OnEModTextChanged += new System.EventHandler(this.uC_Stage_Extradosed_LRFD1_OnEModTextChanged);
            // 
            // tab_stage5
            // 
            this.tab_stage5.Controls.Add(this.uC_Stage_Extradosed_LRFD5);
            this.tab_stage5.Location = new System.Drawing.Point(4, 22);
            this.tab_stage5.Name = "tab_stage5";
            this.tab_stage5.Size = new System.Drawing.Size(941, 602);
            this.tab_stage5.TabIndex = 4;
            this.tab_stage5.Text = "STAGE 5";
            this.tab_stage5.UseVisualStyleBackColor = true;
            // 
            // uC_Stage_Extradosed_LRFD5
            // 
            this.uC_Stage_Extradosed_LRFD5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stage_Extradosed_LRFD5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stage_Extradosed_LRFD5.Location = new System.Drawing.Point(0, 0);
            this.uC_Stage_Extradosed_LRFD5.Name = "uC_Stage_Extradosed_LRFD5";
            this.uC_Stage_Extradosed_LRFD5.Size = new System.Drawing.Size(941, 602);
            this.uC_Stage_Extradosed_LRFD5.TabIndex = 0;
            this.uC_Stage_Extradosed_LRFD5.OnButtonClick += new System.EventHandler(this.btn_stage_data_Click);
            this.uC_Stage_Extradosed_LRFD5.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_stage_file_SelectedIndexChanged);
            this.uC_Stage_Extradosed_LRFD5.OnTextBoxTextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            this.uC_Stage_Extradosed_LRFD5.OnEModTextChanged += new System.EventHandler(this.uC_Stage_Extradosed_LRFD1_OnEModTextChanged);
            // 
            // tab_designSage
            // 
            this.tab_designSage.Controls.Add(this.uC_Res);
            this.tab_designSage.Controls.Add(this.panel3);
            this.tab_designSage.Location = new System.Drawing.Point(4, 22);
            this.tab_designSage.Name = "tab_designSage";
            this.tab_designSage.Size = new System.Drawing.Size(941, 602);
            this.tab_designSage.TabIndex = 5;
            this.tab_designSage.Text = "Design Forces";
            this.tab_designSage.UseVisualStyleBackColor = true;
            // 
            // uC_Res
            // 
            this.uC_Res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Res.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Res.Location = new System.Drawing.Point(0, 48);
            this.uC_Res.Name = "uC_Res";
            this.uC_Res.Size = new System.Drawing.Size(941, 554);
            this.uC_Res.TabIndex = 83;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_result_summary);
            this.panel3.Controls.Add(this.cmb_design_stage);
            this.panel3.Controls.Add(this.label249);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(941, 48);
            this.panel3.TabIndex = 82;
            // 
            // btn_result_summary
            // 
            this.btn_result_summary.Location = new System.Drawing.Point(470, 14);
            this.btn_result_summary.Name = "btn_result_summary";
            this.btn_result_summary.Size = new System.Drawing.Size(254, 23);
            this.btn_result_summary.TabIndex = 82;
            this.btn_result_summary.Text = "Open Analysis Result Summary";
            this.btn_result_summary.UseVisualStyleBackColor = true;
            this.btn_result_summary.Click += new System.EventHandler(this.btn_result_summary_Click);
            // 
            // cmb_design_stage
            // 
            this.cmb_design_stage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_design_stage.FormattingEnabled = true;
            this.cmb_design_stage.Items.AddRange(new object[] {
            "NORMAL ANALYSIS",
            "STAGE 1 ANALYSIS",
            "STAGE 2 ANALYSIS",
            "STAGE 3 ANALYSIS",
            "STAGE 4 ANALYSIS",
            "STAGE 5 ANALYSIS"});
            this.cmb_design_stage.Location = new System.Drawing.Point(152, 16);
            this.cmb_design_stage.Name = "cmb_design_stage";
            this.cmb_design_stage.Size = new System.Drawing.Size(254, 21);
            this.cmb_design_stage.TabIndex = 80;
            this.cmb_design_stage.SelectedIndexChanged += new System.EventHandler(this.cmb_design_stage_SelectedIndexChanged);
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Location = new System.Drawing.Point(8, 19);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(122, 13);
            this.label249.TabIndex = 81;
            this.label249.Text = "Select Design Stage";
            // 
            // tab_worksheet_design
            // 
            this.tab_worksheet_design.Controls.Add(this.tc_bridge_deck);
            this.tab_worksheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_worksheet_design.Name = "tab_worksheet_design";
            this.tab_worksheet_design.Size = new System.Drawing.Size(969, 666);
            this.tab_worksheet_design.TabIndex = 1;
            this.tab_worksheet_design.Text = "Design of Bridge Super Structure";
            this.tab_worksheet_design.UseVisualStyleBackColor = true;
            // 
            // tc_bridge_deck
            // 
            this.tc_bridge_deck.Controls.Add(this.tabPage2);
            this.tc_bridge_deck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_bridge_deck.Location = new System.Drawing.Point(0, 0);
            this.tc_bridge_deck.Name = "tc_bridge_deck";
            this.tc_bridge_deck.SelectedIndex = 0;
            this.tc_bridge_deck.Size = new System.Drawing.Size(969, 666);
            this.tc_bridge_deck.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox7);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.btn_steel_section_ws_open);
            this.tabPage2.Controls.Add(this.btn_process_steel_section);
            this.tabPage2.Controls.Add(this.btn_steel_section_open);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(961, 640);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "PSC Box Girder";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox7.Location = new System.Drawing.Point(45, 448);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(876, 69);
            this.pictureBox7.TabIndex = 134;
            this.pictureBox7.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_steel_girder_input_data);
            this.panel1.Location = new System.Drawing.Point(168, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 436);
            this.panel1.TabIndex = 130;
            // 
            // dgv_steel_girder_input_data
            // 
            this.dgv_steel_girder_input_data.AllowUserToAddRows = false;
            this.dgv_steel_girder_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_steel_girder_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_steel_girder_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40});
            this.dgv_steel_girder_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_steel_girder_input_data.Name = "dgv_steel_girder_input_data";
            this.dgv_steel_girder_input_data.RowHeadersWidth = 27;
            this.dgv_steel_girder_input_data.Size = new System.Drawing.Size(612, 412);
            this.dgv_steel_girder_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.HeaderText = "Description";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn37.Width = 320;
            // 
            // dataGridViewTextBoxColumn38
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn38.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn38.HeaderText = "Name";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.Width = 80;
            // 
            // dataGridViewTextBoxColumn39
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn39.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn39.HeaderText = "Data";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn39.Width = 70;
            // 
            // dataGridViewTextBoxColumn40
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn40.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn40.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn40.Width = 80;
            // 
            // btn_steel_section_ws_open
            // 
            this.btn_steel_section_ws_open.Location = new System.Drawing.Point(213, 592);
            this.btn_steel_section_ws_open.Name = "btn_steel_section_ws_open";
            this.btn_steel_section_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_steel_section_ws_open.TabIndex = 132;
            this.btn_steel_section_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_steel_section_ws_open.UseVisualStyleBackColor = true;
            this.btn_steel_section_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_steel_section
            // 
            this.btn_process_steel_section.Location = new System.Drawing.Point(213, 523);
            this.btn_process_steel_section.Name = "btn_process_steel_section";
            this.btn_process_steel_section.Size = new System.Drawing.Size(559, 29);
            this.btn_process_steel_section.TabIndex = 131;
            this.btn_process_steel_section.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_steel_section.UseVisualStyleBackColor = true;
            this.btn_process_steel_section.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_steel_section_open
            // 
            this.btn_steel_section_open.Location = new System.Drawing.Point(213, 558);
            this.btn_steel_section_open.Name = "btn_steel_section_open";
            this.btn_steel_section_open.Size = new System.Drawing.Size(559, 29);
            this.btn_steel_section_open.TabIndex = 133;
            this.btn_steel_section_open.Text = "Open Design Report";
            this.btn_steel_section_open.UseVisualStyleBackColor = true;
            this.btn_steel_section_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tab_rcc_abutment
            // 
            this.tab_rcc_abutment.Controls.Add(this.pictureBox3);
            this.tab_rcc_abutment.Controls.Add(this.panel2);
            this.tab_rcc_abutment.Controls.Add(this.btn_abutment_ws_open);
            this.tab_rcc_abutment.Controls.Add(this.btn_process_abutment);
            this.tab_rcc_abutment.Controls.Add(this.btn_abutment_open);
            this.tab_rcc_abutment.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_abutment.Name = "tab_rcc_abutment";
            this.tab_rcc_abutment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_abutment.Size = new System.Drawing.Size(969, 666);
            this.tab_rcc_abutment.TabIndex = 5;
            this.tab_rcc_abutment.Text = "Design of RCC Abutment";
            this.tab_rcc_abutment.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(46, 483);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(876, 69);
            this.pictureBox3.TabIndex = 134;
            this.pictureBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_abutment_input_data);
            this.panel2.Location = new System.Drawing.Point(177, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 466);
            this.panel2.TabIndex = 133;
            // 
            // dgv_abutment_input_data
            // 
            this.dgv_abutment_input_data.AllowUserToAddRows = false;
            this.dgv_abutment_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_abutment_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_abutment_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44});
            this.dgv_abutment_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_abutment_input_data.Name = "dgv_abutment_input_data";
            this.dgv_abutment_input_data.RowHeadersWidth = 27;
            this.dgv_abutment_input_data.Size = new System.Drawing.Size(612, 447);
            this.dgv_abutment_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "Description";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn41.Width = 320;
            // 
            // dataGridViewTextBoxColumn42
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn42.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn42.HeaderText = "Name";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.Width = 80;
            // 
            // dataGridViewTextBoxColumn43
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn43.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn43.HeaderText = "Data";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn43.Width = 70;
            // 
            // dataGridViewTextBoxColumn44
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn44.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn44.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn44.Width = 80;
            // 
            // btn_abutment_ws_open
            // 
            this.btn_abutment_ws_open.Location = new System.Drawing.Point(219, 627);
            this.btn_abutment_ws_open.Name = "btn_abutment_ws_open";
            this.btn_abutment_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_abutment_ws_open.TabIndex = 131;
            this.btn_abutment_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_abutment_ws_open.UseVisualStyleBackColor = true;
            this.btn_abutment_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_abutment
            // 
            this.btn_process_abutment.Location = new System.Drawing.Point(219, 558);
            this.btn_process_abutment.Name = "btn_process_abutment";
            this.btn_process_abutment.Size = new System.Drawing.Size(559, 29);
            this.btn_process_abutment.TabIndex = 130;
            this.btn_process_abutment.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_abutment.UseVisualStyleBackColor = true;
            this.btn_process_abutment.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_abutment_open
            // 
            this.btn_abutment_open.Location = new System.Drawing.Point(219, 593);
            this.btn_abutment_open.Name = "btn_abutment_open";
            this.btn_abutment_open.Size = new System.Drawing.Size(559, 29);
            this.btn_abutment_open.TabIndex = 132;
            this.btn_abutment_open.Text = "Open Design Report";
            this.btn_abutment_open.UseVisualStyleBackColor = true;
            this.btn_abutment_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tab_pier
            // 
            this.tab_pier.Controls.Add(this.pictureBox5);
            this.tab_pier.Controls.Add(this.panel11);
            this.tab_pier.Controls.Add(this.btn_pier_ws_open);
            this.tab_pier.Controls.Add(this.btn_process_pier);
            this.tab_pier.Controls.Add(this.btn_pier_open);
            this.tab_pier.Location = new System.Drawing.Point(4, 22);
            this.tab_pier.Name = "tab_pier";
            this.tab_pier.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pier.Size = new System.Drawing.Size(969, 666);
            this.tab_pier.TabIndex = 4;
            this.tab_pier.Text = "Design of RCC Pier";
            this.tab_pier.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox5.Location = new System.Drawing.Point(46, 483);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(876, 69);
            this.pictureBox5.TabIndex = 134;
            this.pictureBox5.TabStop = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.dgv_pier_input_data);
            this.panel11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel11.Location = new System.Drawing.Point(172, 10);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(644, 467);
            this.panel11.TabIndex = 133;
            // 
            // dgv_pier_input_data
            // 
            this.dgv_pier_input_data.AllowUserToAddRows = false;
            this.dgv_pier_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_pier_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pier_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn45,
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn48});
            this.dgv_pier_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_pier_input_data.Name = "dgv_pier_input_data";
            this.dgv_pier_input_data.RowHeadersWidth = 27;
            this.dgv_pier_input_data.Size = new System.Drawing.Size(612, 448);
            this.dgv_pier_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.HeaderText = "Description";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn45.Width = 320;
            // 
            // dataGridViewTextBoxColumn46
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn46.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn46.HeaderText = "Name";
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            this.dataGridViewTextBoxColumn46.Width = 80;
            // 
            // dataGridViewTextBoxColumn47
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn47.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn47.HeaderText = "Data";
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            this.dataGridViewTextBoxColumn47.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn47.Width = 70;
            // 
            // dataGridViewTextBoxColumn48
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn48.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewTextBoxColumn48.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn48.Width = 80;
            // 
            // btn_pier_ws_open
            // 
            this.btn_pier_ws_open.Location = new System.Drawing.Point(217, 627);
            this.btn_pier_ws_open.Name = "btn_pier_ws_open";
            this.btn_pier_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_pier_ws_open.TabIndex = 131;
            this.btn_pier_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_pier_ws_open.UseVisualStyleBackColor = true;
            this.btn_pier_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_pier
            // 
            this.btn_process_pier.Location = new System.Drawing.Point(217, 558);
            this.btn_process_pier.Name = "btn_process_pier";
            this.btn_process_pier.Size = new System.Drawing.Size(559, 29);
            this.btn_process_pier.TabIndex = 130;
            this.btn_process_pier.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_pier.UseVisualStyleBackColor = true;
            this.btn_process_pier.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_pier_open
            // 
            this.btn_pier_open.Location = new System.Drawing.Point(217, 593);
            this.btn_pier_open.Name = "btn_pier_open";
            this.btn_pier_open.Size = new System.Drawing.Size(559, 29);
            this.btn_pier_open.TabIndex = 132;
            this.btn_pier_open.Text = "Open Design Report";
            this.btn_pier_open.UseVisualStyleBackColor = true;
            this.btn_pier_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox6);
            this.tabPage1.Controls.Add(this.panel13);
            this.tabPage1.Controls.Add(this.btn_bearing_ws_open);
            this.tabPage1.Controls.Add(this.btn_process_bearing);
            this.tabPage1.Controls.Add(this.btn_bearing_open);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(969, 666);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "Design of Bearings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox6.Location = new System.Drawing.Point(46, 483);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(876, 69);
            this.pictureBox6.TabIndex = 134;
            this.pictureBox6.TabStop = false;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.dgv_bearing_input_data);
            this.panel13.Location = new System.Drawing.Point(176, 10);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(644, 467);
            this.panel13.TabIndex = 133;
            // 
            // dgv_bearing_input_data
            // 
            this.dgv_bearing_input_data.AllowUserToAddRows = false;
            this.dgv_bearing_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_bearing_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bearing_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn52});
            this.dgv_bearing_input_data.Location = new System.Drawing.Point(18, 16);
            this.dgv_bearing_input_data.Name = "dgv_bearing_input_data";
            this.dgv_bearing_input_data.RowHeadersWidth = 27;
            this.dgv_bearing_input_data.Size = new System.Drawing.Size(612, 448);
            this.dgv_bearing_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.HeaderText = "Description";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn49.Width = 320;
            // 
            // dataGridViewTextBoxColumn50
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn50.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn50.HeaderText = "Name";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.Width = 80;
            // 
            // dataGridViewTextBoxColumn51
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn51.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn51.HeaderText = "Data";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn51.Width = 70;
            // 
            // dataGridViewTextBoxColumn52
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn52.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn52.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn52.Width = 80;
            // 
            // btn_bearing_ws_open
            // 
            this.btn_bearing_ws_open.Location = new System.Drawing.Point(207, 627);
            this.btn_bearing_ws_open.Name = "btn_bearing_ws_open";
            this.btn_bearing_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_bearing_ws_open.TabIndex = 131;
            this.btn_bearing_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_bearing_ws_open.UseVisualStyleBackColor = true;
            this.btn_bearing_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_bearing
            // 
            this.btn_process_bearing.Location = new System.Drawing.Point(207, 558);
            this.btn_process_bearing.Name = "btn_process_bearing";
            this.btn_process_bearing.Size = new System.Drawing.Size(559, 29);
            this.btn_process_bearing.TabIndex = 130;
            this.btn_process_bearing.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_bearing.UseVisualStyleBackColor = true;
            this.btn_process_bearing.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_bearing_open
            // 
            this.btn_bearing_open.Location = new System.Drawing.Point(207, 593);
            this.btn_bearing_open.Name = "btn_bearing_open";
            this.btn_bearing_open.Size = new System.Drawing.Size(559, 29);
            this.btn_bearing_open.TabIndex = 132;
            this.btn_bearing_open.Text = "Open Design Report";
            this.btn_bearing_open.UseVisualStyleBackColor = true;
            this.btn_bearing_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tab_drawings
            // 
            this.tab_drawings.Controls.Add(this.btn_cable_stayed_drawing);
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Pier);
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Cantilever);
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Counterfort);
            this.tab_drawings.Controls.Add(this.label157);
            this.tab_drawings.Controls.Add(this.btn_dwg_pier);
            this.tab_drawings.Controls.Add(this.btn_construction_drawings);
            this.tab_drawings.Controls.Add(this.btn_open_drawings);
            this.tab_drawings.Location = new System.Drawing.Point(4, 22);
            this.tab_drawings.Name = "tab_drawings";
            this.tab_drawings.Size = new System.Drawing.Size(969, 666);
            this.tab_drawings.TabIndex = 2;
            this.tab_drawings.Text = "Design Drawings";
            this.tab_drawings.UseVisualStyleBackColor = true;
            // 
            // btn_cable_stayed_drawing
            // 
            this.btn_cable_stayed_drawing.Location = new System.Drawing.Point(31, 313);
            this.btn_cable_stayed_drawing.Name = "btn_cable_stayed_drawing";
            this.btn_cable_stayed_drawing.Size = new System.Drawing.Size(225, 49);
            this.btn_cable_stayed_drawing.TabIndex = 83;
            this.btn_cable_stayed_drawing.Text = "Cable Stayed Bridge Drawing";
            this.btn_cable_stayed_drawing.UseVisualStyleBackColor = true;
            this.btn_cable_stayed_drawing.Visible = false;
            // 
            // btn_dwg_open_Pier
            // 
            this.btn_dwg_open_Pier.Location = new System.Drawing.Point(321, 404);
            this.btn_dwg_open_Pier.Name = "btn_dwg_open_Pier";
            this.btn_dwg_open_Pier.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Pier.TabIndex = 80;
            this.btn_dwg_open_Pier.Text = "Open Pier Drawings";
            this.btn_dwg_open_Pier.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Pier.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_dwg_open_Cantilever
            // 
            this.btn_dwg_open_Cantilever.Location = new System.Drawing.Point(321, 327);
            this.btn_dwg_open_Cantilever.Name = "btn_dwg_open_Cantilever";
            this.btn_dwg_open_Cantilever.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Cantilever.TabIndex = 81;
            this.btn_dwg_open_Cantilever.Text = "Open Cantilever Abutment Drawings";
            this.btn_dwg_open_Cantilever.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Cantilever.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_dwg_open_Counterfort
            // 
            this.btn_dwg_open_Counterfort.Location = new System.Drawing.Point(321, 258);
            this.btn_dwg_open_Counterfort.Name = "btn_dwg_open_Counterfort";
            this.btn_dwg_open_Counterfort.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Counterfort.TabIndex = 82;
            this.btn_dwg_open_Counterfort.Text = "Open Counterfort Abutment Drawings";
            this.btn_dwg_open_Counterfort.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Counterfort.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label157.Location = new System.Drawing.Point(317, 43);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(308, 23);
            this.label157.TabIndex = 79;
            this.label157.Text = "Editable Construction Drawings";
            this.label157.Visible = false;
            // 
            // btn_dwg_pier
            // 
            this.btn_dwg_pier.Location = new System.Drawing.Point(31, 258);
            this.btn_dwg_pier.Name = "btn_dwg_pier";
            this.btn_dwg_pier.Size = new System.Drawing.Size(225, 49);
            this.btn_dwg_pier.TabIndex = 2;
            this.btn_dwg_pier.Text = "RCC Pier Drawing";
            this.btn_dwg_pier.UseVisualStyleBackColor = true;
            this.btn_dwg_pier.Visible = false;
            this.btn_dwg_pier.Click += new System.EventHandler(this.btn_RccPier_Drawing_Click);
            // 
            // btn_construction_drawings
            // 
            this.btn_construction_drawings.Location = new System.Drawing.Point(321, 98);
            this.btn_construction_drawings.Name = "btn_construction_drawings";
            this.btn_construction_drawings.Size = new System.Drawing.Size(319, 49);
            this.btn_construction_drawings.TabIndex = 1;
            this.btn_construction_drawings.Text = "Open Editable Construction Drawings";
            this.btn_construction_drawings.UseVisualStyleBackColor = true;
            this.btn_construction_drawings.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_open_drawings
            // 
            this.btn_open_drawings.Location = new System.Drawing.Point(321, 167);
            this.btn_open_drawings.Name = "btn_open_drawings";
            this.btn_open_drawings.Size = new System.Drawing.Size(319, 49);
            this.btn_open_drawings.TabIndex = 1;
            this.btn_open_drawings.Text = "Open Editable Design Drawings";
            this.btn_open_drawings.UseVisualStyleBackColor = true;
            this.btn_open_drawings.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // frm_Extradosed_AASHTO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 692);
            this.Controls.Add(this.tc_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Extradosed_AASHTO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extradossed Bridge";
            this.Load += new System.EventHandler(this.frm_Extradosed_AASHTO_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grb_Ana_Res_DL.ResumeLayout(false);
            this.tc_main.ResumeLayout(false);
            this.tab_Analysis_DL.ResumeLayout(false);
            this.tbc_girder.ResumeLayout(false);
            this.tab_user_input.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_cables)).EndInit();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.grb_ana_sw_fp.ResumeLayout(false);
            this.grb_ana_sw_fp.PerformLayout();
            this.grb_ana_crashBarrier.ResumeLayout(false);
            this.grb_ana_crashBarrier.PerformLayout();
            this.grb_ana_wc.ResumeLayout(false);
            this.grb_ana_wc.PerformLayout();
            this.grb_ana_parapet.ResumeLayout(false);
            this.grb_ana_parapet.PerformLayout();
            this.grb_create_input_data.ResumeLayout(false);
            this.grb_create_input_data.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.grb_SIDL.ResumeLayout(false);
            this.grb_SIDL.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.tab_cs_diagram1.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.tab_cs_diagram2.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tab_cs_results.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tab_moving_data.ResumeLayout(false);
            this.groupBox79.ResumeLayout(false);
            this.groupBox79.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).EndInit();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).EndInit();
            this.tab_analysis.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox50.ResumeLayout(false);
            this.groupBox50.PerformLayout();
            this.groupBox51.ResumeLayout(false);
            this.groupBox51.PerformLayout();
            this.groupBox52.ResumeLayout(false);
            this.groupBox52.PerformLayout();
            this.groupBox70.ResumeLayout(false);
            this.groupBox70.PerformLayout();
            this.groupBox109.ResumeLayout(false);
            this.groupBox71.ResumeLayout(false);
            this.groupBox71.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabControl5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox44.ResumeLayout(false);
            this.groupBox44.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.grb_Ana_Res_LL.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tab_stage.ResumeLayout(false);
            this.tc_stage.ResumeLayout(false);
            this.tab_stage1.ResumeLayout(false);
            this.tab_stage2.ResumeLayout(false);
            this.tab_stage3.ResumeLayout(false);
            this.tab_stage4.ResumeLayout(false);
            this.tab_stage5.ResumeLayout(false);
            this.tab_designSage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tab_worksheet_design.ResumeLayout(false);
            this.tc_bridge_deck.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_steel_girder_input_data)).EndInit();
            this.tab_rcc_abutment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_abutment_input_data)).EndInit();
            this.tab_pier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pier_input_data)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bearing_input_data)).EndInit();
            this.tab_drawings.ResumeLayout(false);
            this.tab_drawings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_Ana_DL_outer_long_deff_shear;
        private System.Windows.Forms.TextBox txt_Ana_DL_outer_long_L2_moment;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Ana_DL_inner_long_L2_moment;
        private System.Windows.Forms.TextBox txt_Ana_DL_inner_long_deff_shear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L4_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L2_moment;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L2_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L4_moment;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_deff_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_deff_moment;
        private System.Windows.Forms.GroupBox grb_Ana_Res_DL;
        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tab_Analysis_DL;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grb_create_input_data;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_Ana_width_cantilever;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Ana_DL_eff_depth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Ana_B;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ana_L1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Process_LL_Analysis;
        private System.Windows.Forms.Button btn_Ana_DL_create_data;
        private System.Windows.Forms.GroupBox grb_SIDL;
        private System.Windows.Forms.TextBox txt_Ana_LL_member_load;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_remove_all;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label156;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.Label label174;
        private System.Windows.Forms.Label label179;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L4_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L2_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L2_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L4_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_deff_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_deff_moment;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label181;
        private System.Windows.Forms.Label label183;
        private System.Windows.Forms.Label label185;
        private System.Windows.Forms.Label label187;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L2_shear;
        private System.Windows.Forms.Label label190;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_deff_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L2_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_deff_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L4_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L4_moment;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label191;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.TextBox txt_Ana_live_cross_max_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_cross_max_moment;
        private System.Windows.Forms.GroupBox grb_Ana_Res_LL;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Ana_LL_inner_long_L2_moment;
        private System.Windows.Forms.TextBox txt_Ana_LL_inner_long_deff_shear;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_Ana_LL_outer_long_deff_shear;
        private System.Windows.Forms.TextBox txt_Ana_LL_outer_long_L2_moment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L8_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_L8_moment;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_3L_8_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_3L_8_moment;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_support_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_inner_long_support_moment;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_support_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_support_moment;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L8_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_L8_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_3L_8_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_inner_long_3L_8_moment;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_support_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_support_moment;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L8_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_L8_moment;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_3L_8_shear;
        private System.Windows.Forms.TextBox txt_Ana_live_outer_long_3L_8_moment;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TabPage tab_worksheet_design;
        private System.Windows.Forms.TabPage tab_drawings;
        private System.Windows.Forms.Button btn_open_drawings;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Button btn_update_force;
        private System.Windows.Forms.CheckBox chk_R2;
        private System.Windows.Forms.CheckBox chk_R3;
        private System.Windows.Forms.CheckBox chk_M2;
        private System.Windows.Forms.CheckBox chk_M3;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.DataGridView dgv_seg_tab3_1;
        private System.Windows.Forms.TextBox txt_tab3_d;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txt_tab3_L_8;
        private System.Windows.Forms.TextBox txt_tab3_L_2;
        private System.Windows.Forms.TextBox txt_tab3_3L_8;
        private System.Windows.Forms.TextBox txt_tab3_L_4;
        private System.Windows.Forms.TextBox txt_tab3_support;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.TabPage tab_pier;
        private System.Windows.Forms.Button btn_dwg_pier;
        private System.Windows.Forms.TabControl tbc_girder;
        private System.Windows.Forms.TabPage tab_user_input;
        private System.Windows.Forms.TabPage tab_analysis;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox44;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.TabPage tab_cs_diagram2;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RichTextBox rtb_sections;
        private System.Windows.Forms.Button btn_Show_Section_Resulf;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label226;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label228;
        private System.Windows.Forms.Label label238;
        private System.Windows.Forms.Label label239;
        private System.Windows.Forms.TextBox txt_Ana_LL_factor;
        private System.Windows.Forms.Label label240;
        private System.Windows.Forms.TextBox txt_Ana_DL_factor;
        private System.Windows.Forms.GroupBox groupBox70;
        private System.Windows.Forms.RadioButton rbtn_esprt_pinned;
        private System.Windows.Forms.RadioButton rbtn_esprt_fixed;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_MZ;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_FZ;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_MY;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_FY;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_MX;
        private System.Windows.Forms.CheckBox chk_esprt_fixed_FX;
        private System.Windows.Forms.GroupBox groupBox71;
        private System.Windows.Forms.RadioButton rbtn_ssprt_pinned;
        private System.Windows.Forms.RadioButton rbtn_ssprt_fixed;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_MZ;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_FZ;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_MY;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_FY;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_MX;
        private System.Windows.Forms.CheckBox chk_ssprt_fixed_FX;
        private System.Windows.Forms.GroupBox groupBox109;
        private System.Windows.Forms.ComboBox cmb_long_open_file;
        private System.Windows.Forms.Button btn_view_postProcess;
        private System.Windows.Forms.Button btn_view_report;
        private System.Windows.Forms.Button btn_view_data;
        private System.Windows.Forms.Button btn_view_preProcess;
        private System.Windows.Forms.TabPage tab_rcc_abutment;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_psc_new_design;
        private System.Windows.Forms.Button btn_psc_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label283;
        private System.Windows.Forms.Button btn_dwg_open_Pier;
        private System.Windows.Forms.Button btn_dwg_open_Cantilever;
        private System.Windows.Forms.Button btn_dwg_open_Counterfort;
        private System.Windows.Forms.Label label292;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.Label label291;
        private System.Windows.Forms.Label label284;
        private System.Windows.Forms.Label label285;
        private System.Windows.Forms.Label label286;
        private System.Windows.Forms.TextBox txt_Tower_Height;
        private System.Windows.Forms.TextBox txt_init_cable;
        private System.Windows.Forms.TextBox txt_L3;
        private System.Windows.Forms.Label label288;
        private System.Windows.Forms.TextBox txt_L2;
        private System.Windows.Forms.Label label289;
        private System.Windows.Forms.Label label290;
        private System.Windows.Forms.Label label294;
        private System.Windows.Forms.Label label293;
        private System.Windows.Forms.TextBox txt_cable_no;
        private System.Windows.Forms.Label label298;
        private System.Windows.Forms.Label label296;
        private System.Windows.Forms.Label label297;
        private System.Windows.Forms.Label label295;
        private System.Windows.Forms.TextBox txt_vertical_cbl_dist;
        private System.Windows.Forms.TextBox txt_horizontal_cbl_dist;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.TabControl tc_bridge_deck;
        private System.Windows.Forms.Label label504;
        private System.Windows.Forms.Label label503;
        private System.Windows.Forms.TextBox txt_vertical_cbl_min_dist;
        private System.Windows.Forms.TabPage tab_moving_data;
        private System.Windows.Forms.Label label308;
        private System.Windows.Forms.Label label287;
        private System.Windows.Forms.TextBox txt_tower_Dt;
        private System.Windows.Forms.Label label310;
        private System.Windows.Forms.Label label307;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tower_Bt;
        private System.Windows.Forms.TextBox txt_cable_dia;
        private System.Windows.Forms.GroupBox groupBox79;
        private System.Windows.Forms.Label label299;
        private System.Windows.Forms.TextBox txt_dl_ll_comb;
        private System.Windows.Forms.Button btn_edit_load_combs;
        private System.Windows.Forms.CheckBox chk_self_indian;
        private System.Windows.Forms.Button btn_long_restore_ll_IRC;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Label label300;
        private System.Windows.Forms.DataGridView dgv_long_loads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.Label label301;
        private System.Windows.Forms.Label label302;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Label label303;
        private System.Windows.Forms.DataGridView dgv_long_liveloads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.Label label304;
        private System.Windows.Forms.TextBox txt_IRC_LL_load_gen;
        private System.Windows.Forms.TextBox txt_IRC_XINCR;
        private System.Windows.Forms.Button btn_cable_stayed_drawing;
        private System.Windows.Forms.PictureBox pcb_cables;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_box_weight;
        private System.Windows.Forms.TextBox txt_cp;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmb_cable_type;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.Label label309;
        public System.Windows.Forms.TextBox txt_cbl_des_gamma;
        private System.Windows.Forms.Label label311;
        public System.Windows.Forms.TextBox txt_cbl_des_f;
        private System.Windows.Forms.Label label312;
        private System.Windows.Forms.Label label313;
        public System.Windows.Forms.TextBox txt_cbl_des_E;
        private System.Windows.Forms.Label label314;
        private System.Windows.Forms.Button btn_construction_drawings;
        private System.Windows.Forms.Label label322;
        private System.Windows.Forms.Button btn_irc_view_moving_load;
        private System.Windows.Forms.Label label568;
        private System.Windows.Forms.ComboBox cmb_irc_view_moving_load;
        private System.Windows.Forms.TextBox txt_irc_vehicle_gap;
        private System.Windows.Forms.Label label569;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabPage tab_stage;
        private System.Windows.Forms.TabControl tc_stage;
        private System.Windows.Forms.TabPage tab_stage1;
        private System.Windows.Forms.TabPage tab_stage2;
        private System.Windows.Forms.TabPage tab_stage3;
        private System.Windows.Forms.TabPage tab_stage4;
        private System.Windows.Forms.TabPage tab_stage5;
        private System.Windows.Forms.TabPage tab_designSage;
        private System.Windows.Forms.Label label249;
        private System.Windows.Forms.ComboBox cmb_design_stage;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.TextBox txt_Ana_Superstructure_fci;
        private System.Windows.Forms.TextBox txt_Ana_column_fc;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txt_Ana_Concrete_DL_Calculation;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.TextBox txt_Ana_Concrete_Ec;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.TextBox txt_Ana_Superstructure_fc;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txt_Ana_Strand_Ep;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.TextBox txt_Ana_Strand_Fpy;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.TextBox txt_Ana_Strand_Fpu;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.TextBox txt_Ana_Strand_Diameter;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.TextBox txt_Ana_Strand_Area;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Top_Cover;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Bottom_Cover;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Wearing_Surface;
        private System.Windows.Forms.TextBox txt_Ana_Deckslab_Thickness;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Overhang;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_Ana_FPLL;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.TextBox txt_Ana_SIDL;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox txt_Ana_SelfWeight;
        private System.Windows.Forms.RadioButton rbtn_multiple_cell;
        private System.Windows.Forms.RadioButton rbtn_single_cell;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_Ana_Bottom_Slab_Thickness;
        private System.Windows.Forms.TextBox txt_Ana_Top_Slab_Thickness;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox txt_Ana_Web_Thickness;
        private System.Windows.Forms.TextBox txt_Ana_Span;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txt_Ana_Road_Width;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txt_Ana_Superstructure_depth;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txt_Ana_Web_Spacing;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_steel_girder_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.Button btn_steel_section_ws_open;
        private System.Windows.Forms.Button btn_process_steel_section;
        private System.Windows.Forms.Button btn_steel_section_open;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_abutment_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.Button btn_abutment_ws_open;
        private System.Windows.Forms.Button btn_process_abutment;
        private System.Windows.Forms.Button btn_abutment_open;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView dgv_pier_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.Button btn_pier_ws_open;
        private System.Windows.Forms.Button btn_process_pier;
        private System.Windows.Forms.Button btn_pier_open;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.DataGridView dgv_bearing_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.Button btn_bearing_ws_open;
        private System.Windows.Forms.Button btn_process_bearing;
        private System.Windows.Forms.Button btn_bearing_open;
        private System.Windows.Forms.TabPage tab_cs_diagram1;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.TextBox txt_box_cs2_IYY;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.TextBox txt_box_cs2_IXX;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.TextBox txt_box_cs2_AX;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.TextBox txt_box_cs2_b8;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txt_box_cs2_b7;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txt_box_cs2_b6;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.TextBox txt_box_cs2_d5;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txt_box_cs2_b5;
        private System.Windows.Forms.TextBox txt_box_cs2_d4;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox txt_box_cs2_b4;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_box_cs2_d3;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txt_box_cs2_b3;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txt_box_cs2_d2;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txt_box_cs2_b2;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.TextBox txt_box_cs2_d1;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.TextBox txt_box_cs2_cell_nos;
        private System.Windows.Forms.TextBox txt_box_cs2_b1;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.PictureBox pictureBox8;
        private UC_Stage_Extradosed_LRFD uC_Stage_Extradosed_LRFD1;
        private UC_Stage_Extradosed_LRFD uC_Stage_Extradosed_LRFD2;
        private UC_Stage_Extradosed_LRFD uC_Stage_Extradosed_LRFD3;
        private UC_Stage_Extradosed_LRFD uC_Stage_Extradosed_LRFD4;
        private UC_Stage_Extradosed_LRFD uC_Stage_Extradosed_LRFD5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_result_summary;
        public System.Windows.Forms.CheckBox chk_selfweight;
        private System.Windows.Forms.GroupBox groupBox50;
        private System.Windows.Forms.Label label252;
        private System.Windows.Forms.Label label323;
        private System.Windows.Forms.Label label324;
        private System.Windows.Forms.Label label328;
        private System.Windows.Forms.Label label329;
        private System.Windows.Forms.TextBox txt_PR_cable;
        private System.Windows.Forms.TextBox txt_den_cable;
        private System.Windows.Forms.TextBox txt_emod_cable;
        private System.Windows.Forms.GroupBox groupBox51;
        private System.Windows.Forms.Label label330;
        private System.Windows.Forms.Label label331;
        private System.Windows.Forms.Label label332;
        private System.Windows.Forms.Label label333;
        private System.Windows.Forms.Label label334;
        private System.Windows.Forms.TextBox txt_PR_conc;
        private System.Windows.Forms.TextBox txt_den_conc;
        private System.Windows.Forms.TextBox txt_emod_conc;
        private System.Windows.Forms.GroupBox groupBox52;
        private System.Windows.Forms.Label label1194;
        private System.Windows.Forms.Label label1193;
        private System.Windows.Forms.Label label1196;
        private System.Windows.Forms.Label label1195;
        private System.Windows.Forms.Label label1192;
        private System.Windows.Forms.TextBox txt_PR_steel;
        private System.Windows.Forms.TextBox txt_den_steel;
        private System.Windows.Forms.TextBox txt_emod_steel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private UC_SupportReactions_LRFD uC_SR_DL;
        private UC_SupportReactions_LRFD uC_SR_LL;
        private UC_MaxReactions_LRFD uC_SR_Max;
        public System.Windows.Forms.TextBox txt_max_delf;
        private System.Windows.Forms.Label label499;
        public System.Windows.Forms.Label lbl_max_delf;
        private System.Windows.Forms.Label label500;
        private UC_Extradosed_Results_LRFD uC_Res;
        private System.Windows.Forms.TabPage tab_cs_results;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txt_out_IZZ;
        private System.Windows.Forms.TextBox txt_inn_IZZ;
        private System.Windows.Forms.TextBox txt_cen_IZZ;
        private System.Windows.Forms.Label label444;
        private System.Windows.Forms.Label label354;
        private System.Windows.Forms.TextBox txt_tot_IZZ;
        private System.Windows.Forms.Label label350;
        private System.Windows.Forms.TextBox txt_out_IYY;
        private System.Windows.Forms.TextBox txt_inn_IYY;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label443;
        private System.Windows.Forms.TextBox txt_cen_IYY;
        private System.Windows.Forms.Label label353;
        private System.Windows.Forms.TextBox txt_tot_IYY;
        private System.Windows.Forms.TextBox txt_out_IXX;
        private System.Windows.Forms.Label label349;
        private System.Windows.Forms.TextBox txt_inn_IXX;
        private System.Windows.Forms.Label label279;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.TextBox txt_cen_IXX;
        private System.Windows.Forms.Label label352;
        private System.Windows.Forms.TextBox txt_tot_IXX;
        private System.Windows.Forms.TextBox txt_out_pcnt;
        private System.Windows.Forms.Label label348;
        private System.Windows.Forms.TextBox txt_inn_pcnt;
        private System.Windows.Forms.TextBox txt_out_AX;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.TextBox txt_inn_AX;
        private System.Windows.Forms.TextBox txt_cen_pcnt;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.TextBox txt_cen_AX;
        private System.Windows.Forms.Label label339;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.TextBox txt_tot_AX;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label338;
        private System.Windows.Forms.Label label351;
        private System.Windows.Forms.Label label505;
        private System.Windows.Forms.Label label502;
        private System.Windows.Forms.Label label501;
        private System.Windows.Forms.Label label336;
        private System.Windows.Forms.Label label399;
        private System.Windows.Forms.Label label347;
        private System.Windows.Forms.Label label342;
        private System.Windows.Forms.Label label281;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.Label label335;
        private System.Windows.Forms.Label label341;
        private System.Windows.Forms.Label label346;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label340;
        private System.Windows.Forms.Label label345;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Label label337;
        private System.Windows.Forms.Label label344;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label343;
        private System.Windows.Forms.Label label277;
        private System.Windows.Forms.Label label498;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.Label label273;
        private System.Windows.Forms.Label label271;
        private System.Windows.Forms.Label label269;
        private System.Windows.Forms.TextBox txt_support_distance;
        private System.Windows.Forms.Label label272;
        private System.Windows.Forms.TextBox txt_overhang_gap;
        private System.Windows.Forms.Label label270;
        private System.Windows.Forms.TextBox txt_exp_gap;
        private System.Windows.Forms.Label label268;
        private System.Windows.Forms.PictureBox pic_diagram;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.GroupBox grb_ana_sw_fp;
        private System.Windows.Forms.Label label531;
        private System.Windows.Forms.Label label529;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label524;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label527;
        private System.Windows.Forms.TextBox txt_Ana_wr;
        private System.Windows.Forms.TextBox txt_Ana_Wk;
        private System.Windows.Forms.Label label530;
        private System.Windows.Forms.TextBox txt_Ana_hf_RHS;
        private System.Windows.Forms.TextBox txt_Ana_hf_LHS;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label528;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label525;
        private System.Windows.Forms.TextBox txt_Ana_wf_RHS;
        private System.Windows.Forms.Label label526;
        private System.Windows.Forms.TextBox txt_Ana_wf_LHS;
        private System.Windows.Forms.CheckBox chk_footpath;
        private System.Windows.Forms.CheckBox chk_fp_left;
        private System.Windows.Forms.CheckBox chk_fp_right;
        private System.Windows.Forms.CheckBox chk_cb_right;
        private System.Windows.Forms.CheckBox chk_crash_barrier;
        private System.Windows.Forms.CheckBox chk_cb_left;
        private System.Windows.Forms.GroupBox grb_ana_crashBarrier;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.Label label184;
        private System.Windows.Forms.TextBox txt_Ana_Hc_RHS;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.TextBox txt_Ana_Wc_RHS;
        private System.Windows.Forms.TextBox txt_Ana_Hc_LHS;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.Label label195;
        private System.Windows.Forms.TextBox txt_Ana_Wc_LHS;
        private System.Windows.Forms.GroupBox grb_ana_wc;
        private System.Windows.Forms.Label label511;
        private System.Windows.Forms.TextBox txt_Ana_Wfws;
        private System.Windows.Forms.Label label515;
        private System.Windows.Forms.Label label520;
        private System.Windows.Forms.TextBox txt_Ana_tfws;
        private System.Windows.Forms.Label label521;
        private System.Windows.Forms.GroupBox grb_ana_parapet;
        private System.Windows.Forms.Label label514;
        private System.Windows.Forms.Label label523;
        private System.Windows.Forms.TextBox txt_Ana_Hpar;
        private System.Windows.Forms.Label label510;
        private System.Windows.Forms.Label label522;
        private System.Windows.Forms.TextBox txt_Ana_wbase;
        private System.Windows.Forms.TextBox txt_Ana_member_load;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txt_box_cs2_IZZ;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
    }


}