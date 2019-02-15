namespace LimitStateMethod.PSC_Box_Girder
{
    partial class frm_PSC_Box_Girder_AASHTO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PSC_Box_Girder_AASHTO));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_outer_long_deff_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_DL_outer_long_L2_moment = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
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
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txt_Ana_Steel_Es = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.txt_Ana_Steel_Fy = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Top_Cover = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Bottom_Cover = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Wearing_Surface = new System.Windows.Forms.TextBox();
            this.txt_Ana_Deckslab_Thickness = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.txt_Ana_Deck_Overhang = new System.Windows.Forms.TextBox();
            this.label85 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_psc_new_design = new System.Windows.Forms.Button();
            this.btn_psc_browse = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label283 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.label228 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.grb_Ana_DL_select_analysis = new System.Windows.Forms.GroupBox();
            this.txt_Ana_analysis_file = new System.Windows.Forms.TextBox();
            this.btn_Ana_DL_browse_input_file = new System.Windows.Forms.Button();
            this.rbtn_Ana_DL_create_analysis_file = new System.Windows.Forms.RadioButton();
            this.rbtn_Ana_DL_select_analysis_file = new System.Windows.Forms.RadioButton();
            this.txt_gd_np = new System.Windows.Forms.TextBox();
            this.grb_SIDL = new System.Windows.Forms.GroupBox();
            this.label136 = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.Button();
            this.txt_Ana_FPLL = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.txt_Ana_SIDL = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.label134 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.txt_Ana_SelfWeight = new System.Windows.Forms.TextBox();
            this.grb_create_input_data = new System.Windows.Forms.GroupBox();
            this.rbtn_multiple_cell = new System.Windows.Forms.RadioButton();
            this.rbtn_single_cell = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.txt_Ana_Bridge_Width = new System.Windows.Forms.TextBox();
            this.txt_Ana_Bottom_Slab_Thickness = new System.Windows.Forms.TextBox();
            this.txt_Ana_Top_Slab_Thickness = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.txt_Ana_Web_Thickness = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_Ana_Web_Spacing = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Ana_Superstructure_depth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Ana_Road_Width = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Spans = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.txt_Ana_Span = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_cs_diagram1 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.txt_box_cs2_IYY = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txt_box_cs2_IXX = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.txt_box_cs2_AX = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txt_box_cs2_b8 = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b7 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b6 = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d5 = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b5 = new System.Windows.Forms.TextBox();
            this.txt_box_cs2_d4 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b4 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d3 = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b3 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d2 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_box_cs2_b2 = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txt_box_cs2_d1 = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.txt_box_cs2_cell_nos = new System.Windows.Forms.TextBox();
            this.txt_box_cs2_b1 = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tab_cs_diagram2 = new System.Windows.Forms.TabPage();
            this.btn_Show_Section_Resulf = new System.Windows.Forms.Button();
            this.btn_open_diagram = new System.Windows.Forms.Button();
            this.label176 = new System.Windows.Forms.Label();
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
            this.label226 = new System.Windows.Forms.Label();
            this.rtb_sections = new System.Windows.Forms.RichTextBox();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tab_moving_data = new System.Windows.Forms.TabPage();
            this.btn_bs_view_moving_load = new System.Windows.Forms.Button();
            this.label1190 = new System.Windows.Forms.Label();
            this.cmb_bs_view_moving_load = new System.Windows.Forms.ComboBox();
            this.txt_bs_vehicle_gap = new System.Windows.Forms.TextBox();
            this.label1191 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.txt_LL_load_gen = new System.Windows.Forms.TextBox();
            this.txt_dl_ll_comb_IRC = new System.Windows.Forms.TextBox();
            this.txt_XINCR = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.label1176 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.btn__Loadings_help = new System.Windows.Forms.Button();
            this.btn_edit_load_combs_IRC = new System.Windows.Forms.Button();
            this.groupBox117 = new System.Windows.Forms.GroupBox();
            this.label1173 = new System.Windows.Forms.Label();
            this.label1129 = new System.Windows.Forms.Label();
            this.dgv_long_loads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1130 = new System.Windows.Forms.Label();
            this.btn_long_restore_ll_IRC = new System.Windows.Forms.Button();
            this.groupBox118 = new System.Windows.Forms.GroupBox();
            this.label1170 = new System.Windows.Forms.Label();
            this.label1132 = new System.Windows.Forms.Label();
            this.dgv_long_liveloads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn92 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn93 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn95 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn96 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn97 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn101 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn102 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn103 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_analysis = new System.Windows.Forms.TabPage();
            this.tc_process = new System.Windows.Forms.TabControl();
            this.tab_analysis_data = new System.Windows.Forms.TabPage();
            this.groupBox136 = new System.Windows.Forms.GroupBox();
            this.label1174 = new System.Windows.Forms.Label();
            this.cmb_long_open_file_analysis = new System.Windows.Forms.ComboBox();
            this.btn_view_data_1 = new System.Windows.Forms.Button();
            this.groupBox70 = new System.Windows.Forms.GroupBox();
            this.rbtn_esprt_pinned = new System.Windows.Forms.RadioButton();
            this.rbtn_esprt_fixed = new System.Windows.Forms.RadioButton();
            this.chk_esprt_fixed_MZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MX = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FX = new System.Windows.Forms.CheckBox();
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
            this.tab_process = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Process_LL_Analysis = new System.Windows.Forms.Button();
            this.groupBox109 = new System.Windows.Forms.GroupBox();
            this.cmb_long_open_file_process = new System.Windows.Forms.ComboBox();
            this.btn_View_Result_summary = new System.Windows.Forms.Button();
            this.btn_view_report = new System.Windows.Forms.Button();
            this.btn_view_data = new System.Windows.Forms.Button();
            this.btn_view_postprocess = new System.Windows.Forms.Button();
            this.btn_view_preprocess = new System.Windows.Forms.Button();
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
            this.groupBox58 = new System.Windows.Forms.GroupBox();
            this.label534 = new System.Windows.Forms.Label();
            this.txt_ana_TSRP = new System.Windows.Forms.TextBox();
            this.label535 = new System.Windows.Forms.Label();
            this.label536 = new System.Windows.Forms.Label();
            this.txt_ana_MSTD = new System.Windows.Forms.TextBox();
            this.label537 = new System.Windows.Forms.Label();
            this.label538 = new System.Windows.Forms.Label();
            this.txt_ana_MSLD = new System.Windows.Forms.TextBox();
            this.label539 = new System.Windows.Forms.Label();
            this.groupBox59 = new System.Windows.Forms.GroupBox();
            this.label541 = new System.Windows.Forms.Label();
            this.label540 = new System.Windows.Forms.Label();
            this.txt_ana_DLSR = new System.Windows.Forms.TextBox();
            this.label532 = new System.Windows.Forms.Label();
            this.label533 = new System.Windows.Forms.Label();
            this.txt_ana_LLSR = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox62 = new System.Windows.Forms.GroupBox();
            this.label276 = new System.Windows.Forms.Label();
            this.txt_final_Mx = new System.Windows.Forms.TextBox();
            this.txt_final_Mz = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label278 = new System.Windows.Forms.Label();
            this.groupBox65 = new System.Windows.Forms.GroupBox();
            this.lbl_factor = new System.Windows.Forms.Label();
            this.txt_ll_factor = new System.Windows.Forms.TextBox();
            this.txt_dl_factor = new System.Windows.Forms.TextBox();
            this.label280 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.txt_final_vert_reac = new System.Windows.Forms.TextBox();
            this.groupBox66 = new System.Windows.Forms.GroupBox();
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
            this.groupBox67 = new System.Windows.Forms.GroupBox();
            this.label327 = new System.Windows.Forms.Label();
            this.txt_dead_kN_m = new System.Windows.Forms.TextBox();
            this.label370 = new System.Windows.Forms.Label();
            this.label371 = new System.Windows.Forms.Label();
            this.dgv_left_end_design_forces = new System.Windows.Forms.DataGridView();
            this.col_Joints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Vert_Reaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dead_vert_reac_ton = new System.Windows.Forms.TextBox();
            this.groupBox68 = new System.Windows.Forms.GroupBox();
            this.txt_live_kN_m = new System.Windows.Forms.TextBox();
            this.label388 = new System.Windows.Forms.Label();
            this.label400 = new System.Windows.Forms.Label();
            this.dgv_right_end_design_forces = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_live_vert_rec_Ton = new System.Windows.Forms.TextBox();
            this.label401 = new System.Windows.Forms.Label();
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
            this.tab_worksheet_design = new System.Windows.Forms.TabPage();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_steel_girder_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_steel_section_ws_open = new System.Windows.Forms.Button();
            this.btn_process_steel_section = new System.Windows.Forms.Button();
            this.btn_steel_section_open = new System.Windows.Forms.Button();
            this.tab_rcc_abutment = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgv_abutment_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_abutment_ws_open = new System.Windows.Forms.Button();
            this.btn_process_abutment = new System.Windows.Forms.Button();
            this.btn_abutment_open = new System.Windows.Forms.Button();
            this.tab_pier = new System.Windows.Forms.TabPage();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.dgv_pier_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_pier_ws_open = new System.Windows.Forms.Button();
            this.btn_process_pier = new System.Windows.Forms.Button();
            this.btn_pier_open = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.dgv_bearing_input_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_bearing_ws_open = new System.Windows.Forms.Button();
            this.btn_process_bearing = new System.Windows.Forms.Button();
            this.btn_bearing_open = new System.Windows.Forms.Button();
            this.tab_drawings = new System.Windows.Forms.TabPage();
            this.btn_dwg_open_Pier = new System.Windows.Forms.Button();
            this.btn_dwg_open_Cantilever = new System.Windows.Forms.Button();
            this.btn_dwg_open_Counterfort = new System.Windows.Forms.Button();
            this.label157 = new System.Windows.Forms.Label();
            this.btn_dwg_pier = new System.Windows.Forms.Button();
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
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.grb_Ana_DL_select_analysis.SuspendLayout();
            this.grb_SIDL.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            this.tab_cs_diagram1.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tab_cs_diagram2.SuspendLayout();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tab_moving_data.SuspendLayout();
            this.groupBox117.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).BeginInit();
            this.groupBox118.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).BeginInit();
            this.tab_analysis.SuspendLayout();
            this.tc_process.SuspendLayout();
            this.tab_analysis_data.SuspendLayout();
            this.groupBox136.SuspendLayout();
            this.groupBox70.SuspendLayout();
            this.groupBox71.SuspendLayout();
            this.tab_process.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox109.SuspendLayout();
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
            this.groupBox58.SuspendLayout();
            this.groupBox59.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox62.SuspendLayout();
            this.groupBox65.SuspendLayout();
            this.groupBox66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_des_frc)).BeginInit();
            this.groupBox67.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).BeginInit();
            this.groupBox68.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).BeginInit();
            this.g.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_des_frc)).BeginInit();
            this.tab_worksheet_design.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_steel_girder_input_data)).BeginInit();
            this.tab_rcc_abutment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel6.SuspendLayout();
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(322, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "in";
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
            this.label145.Location = new System.Drawing.Point(188, 31);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(30, 13);
            this.label145.TabIndex = 30;
            this.label145.Text = "(FT)";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(185, 15);
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
            this.label151.Size = new System.Drawing.Size(55, 13);
            this.label151.TabIndex = 30;
            this.label151.Text = "(KIP-FT)";
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
            this.tc_main.Size = new System.Drawing.Size(958, 692);
            this.tc_main.TabIndex = 2;
            // 
            // tab_Analysis_DL
            // 
            this.tab_Analysis_DL.Controls.Add(this.tbc_girder);
            this.tab_Analysis_DL.Location = new System.Drawing.Point(4, 22);
            this.tab_Analysis_DL.Name = "tab_Analysis_DL";
            this.tab_Analysis_DL.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Analysis_DL.Size = new System.Drawing.Size(950, 666);
            this.tab_Analysis_DL.TabIndex = 0;
            this.tab_Analysis_DL.Text = "Analysis of Bridge Deck ";
            this.tab_Analysis_DL.UseVisualStyleBackColor = true;
            // 
            // tbc_girder
            // 
            this.tbc_girder.Controls.Add(this.tab_user_input);
            this.tbc_girder.Controls.Add(this.tab_cs_diagram1);
            this.tbc_girder.Controls.Add(this.tab_cs_diagram2);
            this.tbc_girder.Controls.Add(this.tab_moving_data);
            this.tbc_girder.Controls.Add(this.tab_analysis);
            this.tbc_girder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_girder.Location = new System.Drawing.Point(3, 3);
            this.tbc_girder.Name = "tbc_girder";
            this.tbc_girder.SelectedIndex = 0;
            this.tbc_girder.Size = new System.Drawing.Size(944, 660);
            this.tbc_girder.TabIndex = 107;
            // 
            // tab_user_input
            // 
            this.tab_user_input.Controls.Add(this.groupBox23);
            this.tab_user_input.Controls.Add(this.groupBox22);
            this.tab_user_input.Controls.Add(this.groupBox21);
            this.tab_user_input.Controls.Add(this.groupBox20);
            this.tab_user_input.Controls.Add(this.panel5);
            this.tab_user_input.Controls.Add(this.label227);
            this.tab_user_input.Controls.Add(this.label228);
            this.tab_user_input.Controls.Add(this.groupBox12);
            this.tab_user_input.Controls.Add(this.grb_SIDL);
            this.tab_user_input.Controls.Add(this.grb_create_input_data);
            this.tab_user_input.Location = new System.Drawing.Point(4, 22);
            this.tab_user_input.Name = "tab_user_input";
            this.tab_user_input.Padding = new System.Windows.Forms.Padding(3);
            this.tab_user_input.Size = new System.Drawing.Size(936, 634);
            this.tab_user_input.TabIndex = 0;
            this.tab_user_input.Text = "User\'s Input Data";
            this.tab_user_input.UseVisualStyleBackColor = true;
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
            this.groupBox23.Location = new System.Drawing.Point(396, 189);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(388, 196);
            this.groupBox23.TabIndex = 180;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Concrete";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(7, 116);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(317, 13);
            this.label115.TabIndex = 15;
            this.label115.Text = "Unit weight for normal weight concrete is listed below:";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(6, 20);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(356, 13);
            this.label109.TabIndex = 15;
            this.label109.Text = "The final and release concrete strengths are specified below:";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(210, 42);
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
            this.label111.Location = new System.Drawing.Point(7, 42);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(92, 13);
            this.label111.TabIndex = 15;
            this.label111.Text = "Superstructure";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(7, 93);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(23, 13);
            this.label113.TabIndex = 15;
            this.label113.Text = "f’ci";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(213, 69);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(20, 13);
            this.label116.TabIndex = 15;
            this.label116.Text = "f’c";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(7, 165);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(173, 13);
            this.label122.TabIndex = 15;
            this.label122.Text = "Unit weight for DL calculation";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(7, 138);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(180, 13);
            this.label120.TabIndex = 15;
            this.label120.Text = "Unit weight for computing [Ec]";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(7, 69);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(20, 13);
            this.label110.TabIndex = 15;
            this.label110.Text = "f’c";
            // 
            // txt_Ana_Superstructure_fci
            // 
            this.txt_Ana_Superstructure_fci.Location = new System.Drawing.Point(36, 90);
            this.txt_Ana_Superstructure_fci.Name = "txt_Ana_Superstructure_fci";
            this.txt_Ana_Superstructure_fci.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Superstructure_fci.TabIndex = 6;
            this.txt_Ana_Superstructure_fci.Text = "3.5";
            this.txt_Ana_Superstructure_fci.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Superstructure_fci.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_column_fc
            // 
            this.txt_Ana_column_fc.Location = new System.Drawing.Point(242, 66);
            this.txt_Ana_column_fc.Name = "txt_Ana_column_fc";
            this.txt_Ana_column_fc.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_column_fc.TabIndex = 6;
            this.txt_Ana_column_fc.Text = "3.5";
            this.txt_Ana_column_fc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_column_fc.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(93, 93);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(23, 13);
            this.label112.TabIndex = 17;
            this.label112.Text = "ksi";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(299, 69);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(23, 13);
            this.label114.TabIndex = 17;
            this.label114.Text = "ksi";
            // 
            // txt_Ana_Concrete_DL_Calculation
            // 
            this.txt_Ana_Concrete_DL_Calculation.Location = new System.Drawing.Point(266, 162);
            this.txt_Ana_Concrete_DL_Calculation.Name = "txt_Ana_Concrete_DL_Calculation";
            this.txt_Ana_Concrete_DL_Calculation.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Concrete_DL_Calculation.TabIndex = 6;
            this.txt_Ana_Concrete_DL_Calculation.Text = "0.150";
            this.txt_Ana_Concrete_DL_Calculation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Concrete_DL_Calculation.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(323, 165);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(24, 13);
            this.label121.TabIndex = 17;
            this.label121.Text = "kcf";
            // 
            // txt_Ana_Concrete_Ec
            // 
            this.txt_Ana_Concrete_Ec.Location = new System.Drawing.Point(266, 135);
            this.txt_Ana_Concrete_Ec.Name = "txt_Ana_Concrete_Ec";
            this.txt_Ana_Concrete_Ec.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Concrete_Ec.TabIndex = 6;
            this.txt_Ana_Concrete_Ec.Text = "0.145";
            this.txt_Ana_Concrete_Ec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Concrete_Ec.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(323, 138);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(24, 13);
            this.label117.TabIndex = 17;
            this.label117.Text = "kcf";
            // 
            // txt_Ana_Superstructure_fc
            // 
            this.txt_Ana_Superstructure_fc.Location = new System.Drawing.Point(36, 66);
            this.txt_Ana_Superstructure_fc.Name = "txt_Ana_Superstructure_fc";
            this.txt_Ana_Superstructure_fc.Size = new System.Drawing.Size(51, 21);
            this.txt_Ana_Superstructure_fc.TabIndex = 6;
            this.txt_Ana_Superstructure_fc.Text = "4.5";
            this.txt_Ana_Superstructure_fc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Superstructure_fc.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(93, 69);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(23, 13);
            this.label118.TabIndex = 17;
            this.label118.Text = "ksi";
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
            this.groupBox22.Location = new System.Drawing.Point(396, 25);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(388, 158);
            this.groupBox22.TabIndex = 180;
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
            this.txt_Ana_Strand_Ep.Location = new System.Drawing.Point(253, 132);
            this.txt_Ana_Strand_Ep.Name = "txt_Ana_Strand_Ep";
            this.txt_Ana_Strand_Ep.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Strand_Ep.TabIndex = 6;
            this.txt_Ana_Strand_Ep.Text = "28500";
            this.txt_Ana_Strand_Ep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Strand_Ep.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
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
            this.txt_Ana_Strand_Fpy.Location = new System.Drawing.Point(253, 109);
            this.txt_Ana_Strand_Fpy.Name = "txt_Ana_Strand_Fpy";
            this.txt_Ana_Strand_Fpy.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Strand_Fpy.TabIndex = 6;
            this.txt_Ana_Strand_Fpy.Text = "243";
            this.txt_Ana_Strand_Fpy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Strand_Fpy.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
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
            this.label107.Location = new System.Drawing.Point(323, 136);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(23, 13);
            this.label107.TabIndex = 17;
            this.label107.Text = "ksi";
            // 
            // txt_Ana_Strand_Fpu
            // 
            this.txt_Ana_Strand_Fpu.Location = new System.Drawing.Point(253, 82);
            this.txt_Ana_Strand_Fpu.Name = "txt_Ana_Strand_Fpu";
            this.txt_Ana_Strand_Fpu.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Strand_Fpu.TabIndex = 6;
            this.txt_Ana_Strand_Fpu.Text = "270";
            this.txt_Ana_Strand_Fpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Strand_Fpu.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(323, 113);
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
            this.label92.Location = new System.Drawing.Point(323, 86);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(23, 13);
            this.label92.TabIndex = 17;
            this.label92.Text = "ksi";
            // 
            // txt_Ana_Strand_Diameter
            // 
            this.txt_Ana_Strand_Diameter.Location = new System.Drawing.Point(253, 36);
            this.txt_Ana_Strand_Diameter.Name = "txt_Ana_Strand_Diameter";
            this.txt_Ana_Strand_Diameter.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Strand_Diameter.TabIndex = 6;
            this.txt_Ana_Strand_Diameter.Text = "0.6";
            this.txt_Ana_Strand_Diameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Strand_Diameter.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(323, 39);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(17, 13);
            this.label95.TabIndex = 17;
            this.label95.Text = "in";
            // 
            // txt_Ana_Strand_Area
            // 
            this.txt_Ana_Strand_Area.Location = new System.Drawing.Point(253, 59);
            this.txt_Ana_Strand_Area.Name = "txt_Ana_Strand_Area";
            this.txt_Ana_Strand_Area.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Strand_Area.TabIndex = 6;
            this.txt_Ana_Strand_Area.Text = "0.217";
            this.txt_Ana_Strand_Area.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Strand_Area.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(323, 62);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(33, 13);
            this.label93.TabIndex = 17;
            this.label93.Text = "in^2";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label88);
            this.groupBox21.Controls.Add(this.txt_Ana_Steel_Es);
            this.groupBox21.Controls.Add(this.label98);
            this.groupBox21.Controls.Add(this.label99);
            this.groupBox21.Controls.Add(this.txt_Ana_Steel_Fy);
            this.groupBox21.Controls.Add(this.label101);
            this.groupBox21.Location = new System.Drawing.Point(396, 393);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(388, 69);
            this.groupBox21.TabIndex = 180;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Reinforcing Steel";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 20);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(112, 13);
            this.label88.TabIndex = 15;
            this.label88.Text = "Yield Strength [fy]";
            // 
            // txt_Ana_Steel_Es
            // 
            this.txt_Ana_Steel_Es.Location = new System.Drawing.Point(252, 40);
            this.txt_Ana_Steel_Es.Name = "txt_Ana_Steel_Es";
            this.txt_Ana_Steel_Es.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Steel_Es.TabIndex = 6;
            this.txt_Ana_Steel_Es.Text = "29000";
            this.txt_Ana_Steel_Es.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Steel_Es.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(6, 43);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(149, 13);
            this.label98.TabIndex = 15;
            this.label98.Text = "Modulus of Elasticity [Es]";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(322, 44);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(23, 13);
            this.label99.TabIndex = 17;
            this.label99.Text = "ksi";
            // 
            // txt_Ana_Steel_Fy
            // 
            this.txt_Ana_Steel_Fy.Location = new System.Drawing.Point(252, 17);
            this.txt_Ana_Steel_Fy.Name = "txt_Ana_Steel_Fy";
            this.txt_Ana_Steel_Fy.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Steel_Fy.TabIndex = 6;
            this.txt_Ana_Steel_Fy.Text = "60";
            this.txt_Ana_Steel_Fy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Steel_Fy.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(322, 20);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(23, 13);
            this.label101.TabIndex = 17;
            this.label101.Text = "ksi";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label84);
            this.groupBox20.Controls.Add(this.label60);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Top_Cover);
            this.groupBox20.Controls.Add(this.label75);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Bottom_Cover);
            this.groupBox20.Controls.Add(this.label20);
            this.groupBox20.Controls.Add(this.label59);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Wearing_Surface);
            this.groupBox20.Controls.Add(this.txt_Ana_Deckslab_Thickness);
            this.groupBox20.Controls.Add(this.label19);
            this.groupBox20.Controls.Add(this.label18);
            this.groupBox20.Controls.Add(this.label86);
            this.groupBox20.Controls.Add(this.label87);
            this.groupBox20.Controls.Add(this.txt_Ana_Deck_Overhang);
            this.groupBox20.Controls.Add(this.label85);
            this.groupBox20.Location = new System.Drawing.Point(15, 475);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(358, 142);
            this.groupBox20.TabIndex = 180;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Concrete Deck Slab Minimum Requirements";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 20);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(94, 13);
            this.label84.TabIndex = 15;
            this.label84.Text = "Deck overhang";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(6, 115);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(99, 13);
            this.label60.TabIndex = 15;
            this.label60.Text = "Wearing surface";
            // 
            // txt_Ana_Deck_Top_Cover
            // 
            this.txt_Ana_Deck_Top_Cover.Location = new System.Drawing.Point(252, 64);
            this.txt_Ana_Deck_Top_Cover.Name = "txt_Ana_Deck_Top_Cover";
            this.txt_Ana_Deck_Top_Cover.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Deck_Top_Cover.TabIndex = 6;
            this.txt_Ana_Deck_Top_Cover.Text = "2.5";
            this.txt_Ana_Deck_Top_Cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Deck_Top_Cover.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(322, 115);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(17, 13);
            this.label75.TabIndex = 17;
            this.label75.Text = "in";
            // 
            // txt_Ana_Deck_Bottom_Cover
            // 
            this.txt_Ana_Deck_Bottom_Cover.Location = new System.Drawing.Point(252, 88);
            this.txt_Ana_Deck_Bottom_Cover.Name = "txt_Ana_Deck_Bottom_Cover";
            this.txt_Ana_Deck_Bottom_Cover.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Deck_Bottom_Cover.TabIndex = 6;
            this.txt_Ana_Deck_Bottom_Cover.Text = "1.0";
            this.txt_Ana_Deck_Bottom_Cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Deck_Bottom_Cover.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 91);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(137, 13);
            this.label20.TabIndex = 15;
            this.label20.Text = "Bottom concrete cover";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(322, 98);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(17, 13);
            this.label59.TabIndex = 17;
            this.label59.Text = "in";
            // 
            // txt_Ana_Deck_Wearing_Surface
            // 
            this.txt_Ana_Deck_Wearing_Surface.Location = new System.Drawing.Point(252, 112);
            this.txt_Ana_Deck_Wearing_Surface.Name = "txt_Ana_Deck_Wearing_Surface";
            this.txt_Ana_Deck_Wearing_Surface.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Deck_Wearing_Surface.TabIndex = 6;
            this.txt_Ana_Deck_Wearing_Surface.Text = "0.5";
            this.txt_Ana_Deck_Wearing_Surface.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Deck_Wearing_Surface.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Deckslab_Thickness
            // 
            this.txt_Ana_Deckslab_Thickness.Location = new System.Drawing.Point(252, 40);
            this.txt_Ana_Deckslab_Thickness.Name = "txt_Ana_Deckslab_Thickness";
            this.txt_Ana_Deckslab_Thickness.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Deckslab_Thickness.TabIndex = 6;
            this.txt_Ana_Deckslab_Thickness.Text = "8.0";
            this.txt_Ana_Deckslab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Deckslab_Thickness.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(322, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "in";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(116, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Top concrete cover";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(6, 43);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(89, 13);
            this.label86.TabIndex = 15;
            this.label86.Text = "Slab thickness";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(322, 44);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(17, 13);
            this.label87.TabIndex = 17;
            this.label87.Text = "in";
            // 
            // txt_Ana_Deck_Overhang
            // 
            this.txt_Ana_Deck_Overhang.Location = new System.Drawing.Point(252, 17);
            this.txt_Ana_Deck_Overhang.Name = "txt_Ana_Deck_Overhang";
            this.txt_Ana_Deck_Overhang.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Deck_Overhang.TabIndex = 6;
            this.txt_Ana_Deck_Overhang.Text = "2.63";
            this.txt_Ana_Deck_Overhang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Deck_Overhang.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(322, 17);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(15, 13);
            this.label85.TabIndex = 17;
            this.label85.Text = "ft";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_psc_new_design);
            this.panel5.Controls.Add(this.btn_psc_browse);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label283);
            this.panel5.Location = new System.Drawing.Point(11, 26);
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
            this.label227.Location = new System.Drawing.Point(160, 104);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(218, 18);
            this.label227.TabIndex = 126;
            this.label227.Text = "Default Sample Data are shown";
            // 
            // label228
            // 
            this.label228.AutoSize = true;
            this.label228.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label228.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label228.ForeColor = System.Drawing.Color.Green;
            this.label228.Location = new System.Drawing.Point(19, 104);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(135, 18);
            this.label228.TabIndex = 125;
            this.label228.Text = "All User Input Data";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.grb_Ana_DL_select_analysis);
            this.groupBox12.Controls.Add(this.rbtn_Ana_DL_create_analysis_file);
            this.groupBox12.Controls.Add(this.rbtn_Ana_DL_select_analysis_file);
            this.groupBox12.Controls.Add(this.txt_gd_np);
            this.groupBox12.Location = new System.Drawing.Point(6, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(325, 11);
            this.groupBox12.TabIndex = 99;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Analysis Input Data";
            this.groupBox12.Visible = false;
            // 
            // grb_Ana_DL_select_analysis
            // 
            this.grb_Ana_DL_select_analysis.Controls.Add(this.txt_Ana_analysis_file);
            this.grb_Ana_DL_select_analysis.Controls.Add(this.btn_Ana_DL_browse_input_file);
            this.grb_Ana_DL_select_analysis.Enabled = false;
            this.grb_Ana_DL_select_analysis.Location = new System.Drawing.Point(10, 60);
            this.grb_Ana_DL_select_analysis.Name = "grb_Ana_DL_select_analysis";
            this.grb_Ana_DL_select_analysis.Size = new System.Drawing.Size(369, 39);
            this.grb_Ana_DL_select_analysis.TabIndex = 52;
            this.grb_Ana_DL_select_analysis.TabStop = false;
            this.grb_Ana_DL_select_analysis.Text = "Select Analysis Input Data from File";
            // 
            // txt_Ana_analysis_file
            // 
            this.txt_Ana_analysis_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_analysis_file.Location = new System.Drawing.Point(12, 14);
            this.txt_Ana_analysis_file.Name = "txt_Ana_analysis_file";
            this.txt_Ana_analysis_file.ReadOnly = true;
            this.txt_Ana_analysis_file.Size = new System.Drawing.Size(283, 21);
            this.txt_Ana_analysis_file.TabIndex = 47;
            // 
            // btn_Ana_DL_browse_input_file
            // 
            this.btn_Ana_DL_browse_input_file.Location = new System.Drawing.Point(303, 12);
            this.btn_Ana_DL_browse_input_file.Name = "btn_Ana_DL_browse_input_file";
            this.btn_Ana_DL_browse_input_file.Size = new System.Drawing.Size(61, 23);
            this.btn_Ana_DL_browse_input_file.TabIndex = 35;
            this.btn_Ana_DL_browse_input_file.Text = "Browse";
            this.btn_Ana_DL_browse_input_file.UseVisualStyleBackColor = true;
            this.btn_Ana_DL_browse_input_file.Click += new System.EventHandler(this.btn_Ana_browse_input_file_Click);
            // 
            // rbtn_Ana_DL_create_analysis_file
            // 
            this.rbtn_Ana_DL_create_analysis_file.AutoSize = true;
            this.rbtn_Ana_DL_create_analysis_file.Checked = true;
            this.rbtn_Ana_DL_create_analysis_file.Location = new System.Drawing.Point(13, 19);
            this.rbtn_Ana_DL_create_analysis_file.Name = "rbtn_Ana_DL_create_analysis_file";
            this.rbtn_Ana_DL_create_analysis_file.Size = new System.Drawing.Size(315, 17);
            this.rbtn_Ana_DL_create_analysis_file.TabIndex = 50;
            this.rbtn_Ana_DL_create_analysis_file.TabStop = true;
            this.rbtn_Ana_DL_create_analysis_file.Text = "Create Analysis Input Data File (INPUT_DATA.TXT)";
            this.rbtn_Ana_DL_create_analysis_file.UseVisualStyleBackColor = true;
            this.rbtn_Ana_DL_create_analysis_file.Click += new System.EventHandler(this.rbtn_Ana_select_analysis_file_CheckedChanged);
            // 
            // rbtn_Ana_DL_select_analysis_file
            // 
            this.rbtn_Ana_DL_select_analysis_file.AutoSize = true;
            this.rbtn_Ana_DL_select_analysis_file.Location = new System.Drawing.Point(13, 39);
            this.rbtn_Ana_DL_select_analysis_file.Name = "rbtn_Ana_DL_select_analysis_file";
            this.rbtn_Ana_DL_select_analysis_file.Size = new System.Drawing.Size(272, 17);
            this.rbtn_Ana_DL_select_analysis_file.TabIndex = 50;
            this.rbtn_Ana_DL_select_analysis_file.Text = "Open Analysis Data File (INPUT_DATA.TXT)";
            this.rbtn_Ana_DL_select_analysis_file.UseVisualStyleBackColor = true;
            this.rbtn_Ana_DL_select_analysis_file.Click += new System.EventHandler(this.rbtn_Ana_select_analysis_file_CheckedChanged);
            // 
            // txt_gd_np
            // 
            this.txt_gd_np.Enabled = false;
            this.txt_gd_np.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gd_np.Location = new System.Drawing.Point(410, 264);
            this.txt_gd_np.Name = "txt_gd_np";
            this.txt_gd_np.Size = new System.Drawing.Size(61, 22);
            this.txt_gd_np.TabIndex = 2;
            this.txt_gd_np.Text = "0";
            this.txt_gd_np.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_SIDL
            // 
            this.grb_SIDL.Controls.Add(this.label136);
            this.grb_SIDL.Controls.Add(this.btn_remove);
            this.grb_SIDL.Controls.Add(this.txt_Ana_FPLL);
            this.grb_SIDL.Controls.Add(this.label138);
            this.grb_SIDL.Controls.Add(this.txt_Ana_SIDL);
            this.grb_SIDL.Controls.Add(this.label135);
            this.grb_SIDL.Controls.Add(this.label137);
            this.grb_SIDL.Controls.Add(this.btn_remove_all);
            this.grb_SIDL.Controls.Add(this.label134);
            this.grb_SIDL.Controls.Add(this.label133);
            this.grb_SIDL.Controls.Add(this.txt_Ana_SelfWeight);
            this.grb_SIDL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_SIDL.Location = new System.Drawing.Point(396, 468);
            this.grb_SIDL.Name = "grb_SIDL";
            this.grb_SIDL.Size = new System.Drawing.Size(388, 111);
            this.grb_SIDL.TabIndex = 96;
            this.grb_SIDL.TabStop = false;
            this.grb_SIDL.Text = "Dead Load + Super Imposed Dead Load [DL + SIDL]";
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
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(351, 203);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 45;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            // 
            // txt_Ana_FPLL
            // 
            this.txt_Ana_FPLL.Location = new System.Drawing.Point(252, 74);
            this.txt_Ana_FPLL.Name = "txt_Ana_FPLL";
            this.txt_Ana_FPLL.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_FPLL.TabIndex = 6;
            this.txt_Ana_FPLL.Text = "0.098";
            this.txt_Ana_FPLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_FPLL.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
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
            this.txt_Ana_SIDL.Location = new System.Drawing.Point(253, 48);
            this.txt_Ana_SIDL.Name = "txt_Ana_SIDL";
            this.txt_Ana_SIDL.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_SIDL.TabIndex = 6;
            this.txt_Ana_SIDL.Text = "0.169";
            this.txt_Ana_SIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_SIDL.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
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
            this.label137.Location = new System.Drawing.Point(322, 78);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(33, 13);
            this.label137.TabIndex = 17;
            this.label137.Text = "K/FT";
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
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(323, 52);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(33, 13);
            this.label134.TabIndex = 17;
            this.label134.Text = "K/FT";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(323, 28);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(33, 13);
            this.label133.TabIndex = 17;
            this.label133.Text = "K/FT";
            // 
            // txt_Ana_SelfWeight
            // 
            this.txt_Ana_SelfWeight.Location = new System.Drawing.Point(253, 25);
            this.txt_Ana_SelfWeight.Name = "txt_Ana_SelfWeight";
            this.txt_Ana_SelfWeight.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_SelfWeight.TabIndex = 6;
            this.txt_Ana_SelfWeight.Text = "0.675";
            this.txt_Ana_SelfWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_SelfWeight.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // grb_create_input_data
            // 
            this.grb_create_input_data.Controls.Add(this.rbtn_multiple_cell);
            this.grb_create_input_data.Controls.Add(this.rbtn_single_cell);
            this.grb_create_input_data.Controls.Add(this.label24);
            this.grb_create_input_data.Controls.Add(this.label25);
            this.grb_create_input_data.Controls.Add(this.label83);
            this.grb_create_input_data.Controls.Add(this.label81);
            this.grb_create_input_data.Controls.Add(this.label11);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Bridge_Width);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Bottom_Slab_Thickness);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Top_Slab_Thickness);
            this.grb_create_input_data.Controls.Add(this.label82);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Web_Thickness);
            this.grb_create_input_data.Controls.Add(this.label80);
            this.grb_create_input_data.Controls.Add(this.label33);
            this.grb_create_input_data.Controls.Add(this.label10);
            this.grb_create_input_data.Controls.Add(this.label32);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Web_Spacing);
            this.grb_create_input_data.Controls.Add(this.label5);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Superstructure_depth);
            this.grb_create_input_data.Controls.Add(this.label6);
            this.grb_create_input_data.Controls.Add(this.label3);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Road_Width);
            this.grb_create_input_data.Controls.Add(this.label4);
            this.grb_create_input_data.Controls.Add(this.label124);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Spans);
            this.grb_create_input_data.Controls.Add(this.label123);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Span);
            this.grb_create_input_data.Controls.Add(this.label1);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.Location = new System.Drawing.Point(15, 125);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(358, 337);
            this.grb_create_input_data.TabIndex = 1;
            this.grb_create_input_data.TabStop = false;
            this.grb_create_input_data.Text = "Bridge Geometry";
            // 
            // rbtn_multiple_cell
            // 
            this.rbtn_multiple_cell.AutoSize = true;
            this.rbtn_multiple_cell.Checked = true;
            this.rbtn_multiple_cell.Location = new System.Drawing.Point(13, 20);
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
            this.rbtn_single_cell.Location = new System.Drawing.Point(13, 43);
            this.rbtn_single_cell.Name = "rbtn_single_cell";
            this.rbtn_single_cell.Size = new System.Drawing.Size(287, 17);
            this.rbtn_single_cell.TabIndex = 58;
            this.rbtn_single_cell.Text = "Single Cell Box Girder (Cross Section Type 2)";
            this.rbtn_single_cell.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(322, 147);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 13);
            this.label24.TabIndex = 57;
            this.label24.Text = "ft";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 147);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(78, 13);
            this.label25.TabIndex = 56;
            this.label25.Text = "Bridge width";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(322, 288);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(17, 13);
            this.label83.TabIndex = 17;
            this.label83.Text = "in";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(322, 264);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(17, 13);
            this.label81.TabIndex = 17;
            this.label81.Text = "in";
            // 
            // txt_Ana_Bridge_Width
            // 
            this.txt_Ana_Bridge_Width.Location = new System.Drawing.Point(252, 144);
            this.txt_Ana_Bridge_Width.Name = "txt_Ana_Bridge_Width";
            this.txt_Ana_Bridge_Width.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Bridge_Width.TabIndex = 55;
            this.txt_Ana_Bridge_Width.Text = "45.17";
            this.txt_Ana_Bridge_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Bridge_Width.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Bottom_Slab_Thickness
            // 
            this.txt_Ana_Bottom_Slab_Thickness.Location = new System.Drawing.Point(252, 285);
            this.txt_Ana_Bottom_Slab_Thickness.Name = "txt_Ana_Bottom_Slab_Thickness";
            this.txt_Ana_Bottom_Slab_Thickness.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Bottom_Slab_Thickness.TabIndex = 6;
            this.txt_Ana_Bottom_Slab_Thickness.Text = "6.0";
            this.txt_Ana_Bottom_Slab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Bottom_Slab_Thickness.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Top_Slab_Thickness
            // 
            this.txt_Ana_Top_Slab_Thickness.Location = new System.Drawing.Point(252, 261);
            this.txt_Ana_Top_Slab_Thickness.Name = "txt_Ana_Top_Slab_Thickness";
            this.txt_Ana_Top_Slab_Thickness.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Top_Slab_Thickness.TabIndex = 6;
            this.txt_Ana_Top_Slab_Thickness.Text = "8.0";
            this.txt_Ana_Top_Slab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Top_Slab_Thickness.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(6, 288);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(132, 13);
            this.label82.TabIndex = 15;
            this.label82.Text = "Bottom slab thickness";
            // 
            // txt_Ana_Web_Thickness
            // 
            this.txt_Ana_Web_Thickness.Location = new System.Drawing.Point(252, 237);
            this.txt_Ana_Web_Thickness.Name = "txt_Ana_Web_Thickness";
            this.txt_Ana_Web_Thickness.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Web_Thickness.TabIndex = 6;
            this.txt_Ana_Web_Thickness.Text = "12.0";
            this.txt_Ana_Web_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Web_Thickness.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(6, 264);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(111, 13);
            this.label80.TabIndex = 15;
            this.label80.Text = "Top slab thickness";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(322, 216);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "ft";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Web thickness";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(6, 216);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(78, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Web spacing";
            // 
            // txt_Ana_Web_Spacing
            // 
            this.txt_Ana_Web_Spacing.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_Web_Spacing.Location = new System.Drawing.Point(252, 213);
            this.txt_Ana_Web_Spacing.Name = "txt_Ana_Web_Spacing";
            this.txt_Ana_Web_Spacing.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Web_Spacing.TabIndex = 3;
            this.txt_Ana_Web_Spacing.Text = "7.75";
            this.txt_Ana_Web_Spacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Web_Spacing.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "ft";
            // 
            // txt_Ana_Superstructure_depth
            // 
            this.txt_Ana_Superstructure_depth.Location = new System.Drawing.Point(252, 190);
            this.txt_Ana_Superstructure_depth.Name = "txt_Ana_Superstructure_depth";
            this.txt_Ana_Superstructure_depth.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Superstructure_depth.TabIndex = 2;
            this.txt_Ana_Superstructure_depth.Text = "5.50";
            this.txt_Ana_Superstructure_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Superstructure_depth.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Superstructure depth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ft";
            // 
            // txt_Ana_Road_Width
            // 
            this.txt_Ana_Road_Width.Location = new System.Drawing.Point(252, 167);
            this.txt_Ana_Road_Width.Name = "txt_Ana_Road_Width";
            this.txt_Ana_Road_Width.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Road_Width.TabIndex = 1;
            this.txt_Ana_Road_Width.Text = "42.0";
            this.txt_Ana_Road_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Road_Width.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Roadway width";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(322, 87);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(15, 13);
            this.label124.TabIndex = 2;
            this.label124.Text = "ft";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ft";
            // 
            // txt_Spans
            // 
            this.txt_Spans.Location = new System.Drawing.Point(121, 84);
            this.txt_Spans.Name = "txt_Spans";
            this.txt_Spans.Size = new System.Drawing.Size(195, 21);
            this.txt_Spans.TabIndex = 0;
            this.txt_Spans.Text = "118.0,130.0";
            this.txt_Spans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Spans.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(4, 87);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(84, 13);
            this.label123.TabIndex = 0;
            this.label123.Text = "Span Lengths";
            // 
            // txt_Ana_Span
            // 
            this.txt_Ana_Span.Location = new System.Drawing.Point(252, 121);
            this.txt_Ana_Span.Name = "txt_Ana_Span";
            this.txt_Ana_Span.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Span.TabIndex = 0;
            this.txt_Ana_Span.Text = "248.00";
            this.txt_Ana_Span.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Span.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total length";
            // 
            // tab_cs_diagram1
            // 
            this.tab_cs_diagram1.Controls.Add(this.groupBox19);
            this.tab_cs_diagram1.Controls.Add(this.groupBox8);
            this.tab_cs_diagram1.Controls.Add(this.pictureBox3);
            this.tab_cs_diagram1.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram1.Name = "tab_cs_diagram1";
            this.tab_cs_diagram1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram1.Size = new System.Drawing.Size(936, 634);
            this.tab_cs_diagram1.TabIndex = 4;
            this.tab_cs_diagram1.Text = "Cross Section Diagram Type 1";
            this.tab_cs_diagram1.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.txt_box_cs2_IYY);
            this.groupBox19.Controls.Add(this.label90);
            this.groupBox19.Controls.Add(this.txt_box_cs2_IXX);
            this.groupBox19.Controls.Add(this.label96);
            this.groupBox19.Controls.Add(this.label97);
            this.groupBox19.Controls.Add(this.txt_box_cs2_AX);
            this.groupBox19.Controls.Add(this.label100);
            this.groupBox19.Controls.Add(this.label102);
            this.groupBox19.Controls.Add(this.label103);
            this.groupBox19.Location = new System.Drawing.Point(348, 324);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(515, 121);
            this.groupBox19.TabIndex = 2;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Results";
            // 
            // txt_box_cs2_IYY
            // 
            this.txt_box_cs2_IYY.Location = new System.Drawing.Point(233, 83);
            this.txt_box_cs2_IYY.Name = "txt_box_cs2_IYY";
            this.txt_box_cs2_IYY.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_IYY.TabIndex = 1;
            this.txt_box_cs2_IYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.label97.Size = new System.Drawing.Size(219, 13);
            this.label97.TabIndex = 0;
            this.label97.Text = "Moment of Inertia about X-Axis [IYY]";
            // 
            // txt_box_cs2_AX
            // 
            this.txt_box_cs2_AX.Location = new System.Drawing.Point(233, 29);
            this.txt_box_cs2_AX.Name = "txt_box_cs2_AX";
            this.txt_box_cs2_AX.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_AX.TabIndex = 1;
            this.txt_box_cs2_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txt_box_cs2_b8);
            this.groupBox8.Controls.Add(this.label66);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b7);
            this.groupBox8.Controls.Add(this.label64);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b6);
            this.groupBox8.Controls.Add(this.label62);
            this.groupBox8.Controls.Add(this.txt_box_cs2_d5);
            this.groupBox8.Controls.Add(this.label77);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b5);
            this.groupBox8.Controls.Add(this.txt_box_cs2_d4);
            this.groupBox8.Controls.Add(this.label50);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b4);
            this.groupBox8.Controls.Add(this.label76);
            this.groupBox8.Controls.Add(this.label65);
            this.groupBox8.Controls.Add(this.label48);
            this.groupBox8.Controls.Add(this.txt_box_cs2_d3);
            this.groupBox8.Controls.Add(this.label63);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b3);
            this.groupBox8.Controls.Add(this.label74);
            this.groupBox8.Controls.Add(this.label51);
            this.groupBox8.Controls.Add(this.label73);
            this.groupBox8.Controls.Add(this.label46);
            this.groupBox8.Controls.Add(this.txt_box_cs2_d2);
            this.groupBox8.Controls.Add(this.label49);
            this.groupBox8.Controls.Add(this.label72);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b2);
            this.groupBox8.Controls.Add(this.label71);
            this.groupBox8.Controls.Add(this.label47);
            this.groupBox8.Controls.Add(this.label70);
            this.groupBox8.Controls.Add(this.label44);
            this.groupBox8.Controls.Add(this.txt_box_cs2_d1);
            this.groupBox8.Controls.Add(this.label45);
            this.groupBox8.Controls.Add(this.label69);
            this.groupBox8.Controls.Add(this.txt_box_cs2_cell_nos);
            this.groupBox8.Controls.Add(this.txt_box_cs2_b1);
            this.groupBox8.Controls.Add(this.label68);
            this.groupBox8.Controls.Add(this.label43);
            this.groupBox8.Controls.Add(this.label79);
            this.groupBox8.Controls.Add(this.label67);
            this.groupBox8.Controls.Add(this.label78);
            this.groupBox8.Controls.Add(this.label42);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Location = new System.Drawing.Point(6, 324);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(322, 289);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Dimension Inputs";
            // 
            // txt_box_cs2_b8
            // 
            this.txt_box_cs2_b8.Location = new System.Drawing.Point(42, 242);
            this.txt_box_cs2_b8.Name = "txt_box_cs2_b8";
            this.txt_box_cs2_b8.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b8.TabIndex = 1;
            this.txt_box_cs2_b8.Text = "436";
            this.txt_box_cs2_b8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b8.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(107, 245);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(17, 13);
            this.label66.TabIndex = 0;
            this.label66.Text = "in";
            // 
            // txt_box_cs2_b7
            // 
            this.txt_box_cs2_b7.Location = new System.Drawing.Point(42, 215);
            this.txt_box_cs2_b7.Name = "txt_box_cs2_b7";
            this.txt_box_cs2_b7.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b7.TabIndex = 1;
            this.txt_box_cs2_b7.Text = "21.5";
            this.txt_box_cs2_b7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b7.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(107, 218);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(17, 13);
            this.label64.TabIndex = 0;
            this.label64.Text = "in";
            // 
            // txt_box_cs2_b6
            // 
            this.txt_box_cs2_b6.Location = new System.Drawing.Point(42, 188);
            this.txt_box_cs2_b6.Name = "txt_box_cs2_b6";
            this.txt_box_cs2_b6.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b6.TabIndex = 1;
            this.txt_box_cs2_b6.Text = "31.5";
            this.txt_box_cs2_b6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b6.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(107, 191);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(17, 13);
            this.label62.TabIndex = 0;
            this.label62.Text = "in";
            // 
            // txt_box_cs2_d5
            // 
            this.txt_box_cs2_d5.Location = new System.Drawing.Point(207, 161);
            this.txt_box_cs2_d5.Name = "txt_box_cs2_d5";
            this.txt_box_cs2_d5.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d5.TabIndex = 1;
            this.txt_box_cs2_d5.Text = "6.0";
            this.txt_box_cs2_d5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d5.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
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
            this.txt_box_cs2_b5.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // txt_box_cs2_d4
            // 
            this.txt_box_cs2_d4.Location = new System.Drawing.Point(207, 134);
            this.txt_box_cs2_d4.Name = "txt_box_cs2_d4";
            this.txt_box_cs2_d4.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d4.TabIndex = 1;
            this.txt_box_cs2_d4.Text = "66";
            this.txt_box_cs2_d4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d4.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(107, 164);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(17, 13);
            this.label50.TabIndex = 0;
            this.label50.Text = "in";
            // 
            // txt_box_cs2_b4
            // 
            this.txt_box_cs2_b4.Location = new System.Drawing.Point(42, 134);
            this.txt_box_cs2_b4.Name = "txt_box_cs2_b4";
            this.txt_box_cs2_b4.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b4.TabIndex = 1;
            this.txt_box_cs2_b4.Text = "12";
            this.txt_box_cs2_b4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b4.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
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
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(5, 245);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(31, 13);
            this.label65.TabIndex = 0;
            this.label65.Text = "[b8]";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(107, 137);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 13);
            this.label48.TabIndex = 0;
            this.label48.Text = "in";
            // 
            // txt_box_cs2_d3
            // 
            this.txt_box_cs2_d3.Location = new System.Drawing.Point(207, 107);
            this.txt_box_cs2_d3.Name = "txt_box_cs2_d3";
            this.txt_box_cs2_d3.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d3.TabIndex = 1;
            this.txt_box_cs2_d3.Text = "8.0";
            this.txt_box_cs2_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d3.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(5, 218);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(31, 13);
            this.label63.TabIndex = 0;
            this.label63.Text = "[b7]";
            // 
            // txt_box_cs2_b3
            // 
            this.txt_box_cs2_b3.Location = new System.Drawing.Point(42, 107);
            this.txt_box_cs2_b3.Name = "txt_box_cs2_b3";
            this.txt_box_cs2_b3.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b3.TabIndex = 1;
            this.txt_box_cs2_b3.Text = "465";
            this.txt_box_cs2_b3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b3.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
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
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(5, 191);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(31, 13);
            this.label51.TabIndex = 0;
            this.label51.Text = "[b6]";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(170, 164);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(31, 13);
            this.label73.TabIndex = 0;
            this.label73.Text = "[d5]";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(107, 110);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(17, 13);
            this.label46.TabIndex = 0;
            this.label46.Text = "in";
            // 
            // txt_box_cs2_d2
            // 
            this.txt_box_cs2_d2.Location = new System.Drawing.Point(207, 80);
            this.txt_box_cs2_d2.Name = "txt_box_cs2_d2";
            this.txt_box_cs2_d2.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d2.TabIndex = 1;
            this.txt_box_cs2_d2.Text = "12.0";
            this.txt_box_cs2_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d2.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(5, 164);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(31, 13);
            this.label49.TabIndex = 0;
            this.label49.Text = "[b5]";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(170, 137);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(31, 13);
            this.label72.TabIndex = 0;
            this.label72.Text = "[d4]";
            // 
            // txt_box_cs2_b2
            // 
            this.txt_box_cs2_b2.Location = new System.Drawing.Point(42, 80);
            this.txt_box_cs2_b2.Name = "txt_box_cs2_b2";
            this.txt_box_cs2_b2.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b2.TabIndex = 1;
            this.txt_box_cs2_b2.Text = "38.5";
            this.txt_box_cs2_b2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b2.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(272, 83);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(17, 13);
            this.label71.TabIndex = 0;
            this.label71.Text = "in";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(5, 137);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(31, 13);
            this.label47.TabIndex = 0;
            this.label47.Text = "[b4]";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(170, 110);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(31, 13);
            this.label70.TabIndex = 0;
            this.label70.Text = "[d3]";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(107, 83);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(17, 13);
            this.label44.TabIndex = 0;
            this.label44.Text = "in";
            // 
            // txt_box_cs2_d1
            // 
            this.txt_box_cs2_d1.Location = new System.Drawing.Point(207, 53);
            this.txt_box_cs2_d1.Name = "txt_box_cs2_d1";
            this.txt_box_cs2_d1.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_d1.TabIndex = 1;
            this.txt_box_cs2_d1.Text = "9.0";
            this.txt_box_cs2_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_d1.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(5, 110);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(31, 13);
            this.label45.TabIndex = 0;
            this.label45.Text = "[b3]";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(170, 83);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(31, 13);
            this.label69.TabIndex = 0;
            this.label69.Text = "[d2]";
            // 
            // txt_box_cs2_cell_nos
            // 
            this.txt_box_cs2_cell_nos.Location = new System.Drawing.Point(207, 20);
            this.txt_box_cs2_cell_nos.Name = "txt_box_cs2_cell_nos";
            this.txt_box_cs2_cell_nos.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_cell_nos.TabIndex = 1;
            this.txt_box_cs2_cell_nos.Text = "3";
            this.txt_box_cs2_cell_nos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_cell_nos.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // txt_box_cs2_b1
            // 
            this.txt_box_cs2_b1.Location = new System.Drawing.Point(42, 53);
            this.txt_box_cs2_b1.Name = "txt_box_cs2_b1";
            this.txt_box_cs2_b1.Size = new System.Drawing.Size(59, 21);
            this.txt_box_cs2_b1.TabIndex = 1;
            this.txt_box_cs2_b1.Text = "542";
            this.txt_box_cs2_b1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_box_cs2_b1.TextChanged += new System.EventHandler(this.txt_box_cs2_b1_TextChanged);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(272, 56);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(17, 13);
            this.label68.TabIndex = 0;
            this.label68.Text = "in";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(5, 83);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(31, 13);
            this.label43.TabIndex = 0;
            this.label43.Text = "[b2]";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(272, 23);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(27, 13);
            this.label79.TabIndex = 0;
            this.label79.Text = "nos";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(170, 56);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(31, 13);
            this.label67.TabIndex = 0;
            this.label67.Text = "[d1]";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(5, 23);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(136, 13);
            this.label78.TabIndex = 0;
            this.label78.Text = "Number of Cells inside";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(107, 56);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(17, 13);
            this.label42.TabIndex = 0;
            this.label42.Text = "in";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(5, 56);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 13);
            this.label38.TabIndex = 0;
            this.label38.Text = "[b1]";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::LimitStateMethod.Properties.Resources.AASHTO_BOX_CROSSSECTION;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(132, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(694, 312);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.DoubleClick += new System.EventHandler(this.pictureBox3_DoubleClick);
            // 
            // tab_cs_diagram2
            // 
            this.tab_cs_diagram2.AutoScroll = true;
            this.tab_cs_diagram2.Controls.Add(this.btn_Show_Section_Resulf);
            this.tab_cs_diagram2.Controls.Add(this.btn_open_diagram);
            this.tab_cs_diagram2.Controls.Add(this.label176);
            this.tab_cs_diagram2.Controls.Add(this.groupBox32);
            this.tab_cs_diagram2.Controls.Add(this.label226);
            this.tab_cs_diagram2.Controls.Add(this.rtb_sections);
            this.tab_cs_diagram2.Controls.Add(this.groupBox26);
            this.tab_cs_diagram2.ForeColor = System.Drawing.Color.Blue;
            this.tab_cs_diagram2.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram2.Name = "tab_cs_diagram2";
            this.tab_cs_diagram2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram2.Size = new System.Drawing.Size(936, 634);
            this.tab_cs_diagram2.TabIndex = 2;
            this.tab_cs_diagram2.Text = "Cross Section Diagram Type 2";
            this.tab_cs_diagram2.UseVisualStyleBackColor = true;
            // 
            // btn_Show_Section_Resulf
            // 
            this.btn_Show_Section_Resulf.Location = new System.Drawing.Point(587, 858);
            this.btn_Show_Section_Resulf.Name = "btn_Show_Section_Resulf";
            this.btn_Show_Section_Resulf.Size = new System.Drawing.Size(148, 32);
            this.btn_Show_Section_Resulf.TabIndex = 69;
            this.btn_Show_Section_Resulf.Text = "Show Results";
            this.btn_Show_Section_Resulf.UseVisualStyleBackColor = true;
            this.btn_Show_Section_Resulf.Click += new System.EventHandler(this.btn_Show_Section_Result_Click);
            // 
            // btn_open_diagram
            // 
            this.btn_open_diagram.Location = new System.Drawing.Point(313, 337);
            this.btn_open_diagram.Name = "btn_open_diagram";
            this.btn_open_diagram.Size = new System.Drawing.Size(291, 23);
            this.btn_open_diagram.TabIndex = 133;
            this.btn_open_diagram.Text = "Open Diagram for Cross Section Data Input ";
            this.btn_open_diagram.UseVisualStyleBackColor = true;
            this.btn_open_diagram.Click += new System.EventHandler(this.btn_open_diagram_Click);
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label176.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.Red;
            this.label176.Location = new System.Drawing.Point(377, 864);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(189, 20);
            this.label176.TabIndex = 125;
            this.label176.Text = "All Calculated Values ";
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
            this.groupBox32.Location = new System.Drawing.Point(222, 366);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(513, 462);
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
            // label226
            // 
            this.label226.AutoSize = true;
            this.label226.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label226.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label226.ForeColor = System.Drawing.Color.Green;
            this.label226.Location = new System.Drawing.Point(124, 864);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(229, 20);
            this.label226.TabIndex = 124;
            this.label226.Text = "No User Input in this page";
            // 
            // rtb_sections
            // 
            this.rtb_sections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_sections.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_sections.Location = new System.Drawing.Point(6, 898);
            this.rtb_sections.Name = "rtb_sections";
            this.rtb_sections.ReadOnly = true;
            this.rtb_sections.Size = new System.Drawing.Size(886, 238);
            this.rtb_sections.TabIndex = 3;
            this.rtb_sections.Text = "";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.pictureBox4);
            this.groupBox26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox26.Location = new System.Drawing.Point(116, 6);
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
            // tab_moving_data
            // 
            this.tab_moving_data.Controls.Add(this.btn_bs_view_moving_load);
            this.tab_moving_data.Controls.Add(this.label1190);
            this.tab_moving_data.Controls.Add(this.cmb_bs_view_moving_load);
            this.tab_moving_data.Controls.Add(this.txt_bs_vehicle_gap);
            this.tab_moving_data.Controls.Add(this.label1191);
            this.tab_moving_data.Controls.Add(this.label126);
            this.tab_moving_data.Controls.Add(this.label127);
            this.tab_moving_data.Controls.Add(this.txt_LL_load_gen);
            this.tab_moving_data.Controls.Add(this.txt_dl_ll_comb_IRC);
            this.tab_moving_data.Controls.Add(this.txt_XINCR);
            this.tab_moving_data.Controls.Add(this.label128);
            this.tab_moving_data.Controls.Add(this.label1176);
            this.tab_moving_data.Controls.Add(this.label125);
            this.tab_moving_data.Controls.Add(this.btn__Loadings_help);
            this.tab_moving_data.Controls.Add(this.btn_edit_load_combs_IRC);
            this.tab_moving_data.Controls.Add(this.groupBox117);
            this.tab_moving_data.Controls.Add(this.label1130);
            this.tab_moving_data.Controls.Add(this.btn_long_restore_ll_IRC);
            this.tab_moving_data.Controls.Add(this.groupBox118);
            this.tab_moving_data.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data.Name = "tab_moving_data";
            this.tab_moving_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data.Size = new System.Drawing.Size(936, 634);
            this.tab_moving_data.TabIndex = 3;
            this.tab_moving_data.Text = "Moving Load Data";
            this.tab_moving_data.UseVisualStyleBackColor = true;
            // 
            // btn_bs_view_moving_load
            // 
            this.btn_bs_view_moving_load.Location = new System.Drawing.Point(48, 421);
            this.btn_bs_view_moving_load.Name = "btn_bs_view_moving_load";
            this.btn_bs_view_moving_load.Size = new System.Drawing.Size(204, 29);
            this.btn_bs_view_moving_load.TabIndex = 121;
            this.btn_bs_view_moving_load.Text = "View Moving Load";
            this.btn_bs_view_moving_load.UseVisualStyleBackColor = true;
            this.btn_bs_view_moving_load.Click += new System.EventHandler(this.btn_bs_view_moving_load_Click);
            // 
            // label1190
            // 
            this.label1190.AutoSize = true;
            this.label1190.Location = new System.Drawing.Point(10, 400);
            this.label1190.Name = "label1190";
            this.label1190.Size = new System.Drawing.Size(166, 13);
            this.label1190.TabIndex = 120;
            this.label1190.Text = "Select to view Moving Load ";
            // 
            // cmb_bs_view_moving_load
            // 
            this.cmb_bs_view_moving_load.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bs_view_moving_load.FormattingEnabled = true;
            this.cmb_bs_view_moving_load.Location = new System.Drawing.Point(190, 397);
            this.cmb_bs_view_moving_load.Name = "cmb_bs_view_moving_load";
            this.cmb_bs_view_moving_load.Size = new System.Drawing.Size(84, 21);
            this.cmb_bs_view_moving_load.TabIndex = 119;
            // 
            // txt_bs_vehicle_gap
            // 
            this.txt_bs_vehicle_gap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_bs_vehicle_gap.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bs_vehicle_gap.Location = new System.Drawing.Point(734, 399);
            this.txt_bs_vehicle_gap.Name = "txt_bs_vehicle_gap";
            this.txt_bs_vehicle_gap.Size = new System.Drawing.Size(65, 18);
            this.txt_bs_vehicle_gap.TabIndex = 117;
            this.txt_bs_vehicle_gap.Text = "18.8";
            this.txt_bs_vehicle_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1191
            // 
            this.label1191.AutoSize = true;
            this.label1191.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1191.Location = new System.Drawing.Point(307, 400);
            this.label1191.Name = "label1191";
            this.label1191.Size = new System.Drawing.Size(421, 13);
            this.label1191.TabIndex = 118;
            this.label1191.Text = "Longitudinal Separating distance between two vehicle in a Lane";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(709, 597);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(170, 13);
            this.label126.TabIndex = 113;
            this.label126.Text = "DL + LL Combine Load No";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label127.Location = new System.Drawing.Point(771, 572);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(114, 13);
            this.label127.TabIndex = 116;
            this.label127.Text = "Load Generation";
            // 
            // txt_LL_load_gen
            // 
            this.txt_LL_load_gen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LL_load_gen.Location = new System.Drawing.Point(891, 569);
            this.txt_LL_load_gen.Name = "txt_LL_load_gen";
            this.txt_LL_load_gen.Size = new System.Drawing.Size(39, 21);
            this.txt_LL_load_gen.TabIndex = 115;
            this.txt_LL_load_gen.Text = "191";
            this.txt_LL_load_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_dl_ll_comb_IRC
            // 
            this.txt_dl_ll_comb_IRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dl_ll_comb_IRC.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dl_ll_comb_IRC.Location = new System.Drawing.Point(891, 597);
            this.txt_dl_ll_comb_IRC.Name = "txt_dl_ll_comb_IRC";
            this.txt_dl_ll_comb_IRC.Size = new System.Drawing.Size(39, 18);
            this.txt_dl_ll_comb_IRC.TabIndex = 111;
            this.txt_dl_ll_comb_IRC.Text = "1";
            this.txt_dl_ll_comb_IRC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XINCR
            // 
            this.txt_XINCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_XINCR.Location = new System.Drawing.Point(712, 571);
            this.txt_XINCR.Name = "txt_XINCR";
            this.txt_XINCR.Size = new System.Drawing.Size(37, 18);
            this.txt_XINCR.TabIndex = 112;
            this.txt_XINCR.Text = "0.5";
            this.txt_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_XINCR.TextChanged += new System.EventHandler(this.txt_XINCR_TextChanged);
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label128.Location = new System.Drawing.Point(656, 572);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(50, 13);
            this.label128.TabIndex = 114;
            this.label128.Text = "X INCR";
            // 
            // label1176
            // 
            this.label1176.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1176.ForeColor = System.Drawing.Color.Red;
            this.label1176.Location = new System.Drawing.Point(4, 599);
            this.label1176.Name = "label1176";
            this.label1176.Size = new System.Drawing.Size(614, 33);
            this.label1176.TabIndex = 110;
            this.label1176.Text = resources.GetString("label1176.Text");
            // 
            // label125
            // 
            this.label125.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label125.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.Location = new System.Drawing.Point(14, 468);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(925, 96);
            this.label125.TabIndex = 108;
            this.label125.Text = resources.GetString("label125.Text");
            // 
            // btn__Loadings_help
            // 
            this.btn__Loadings_help.Location = new System.Drawing.Point(15, 567);
            this.btn__Loadings_help.Name = "btn__Loadings_help";
            this.btn__Loadings_help.Size = new System.Drawing.Size(204, 29);
            this.btn__Loadings_help.TabIndex = 106;
            this.btn__Loadings_help.Text = "Loading guide";
            this.btn__Loadings_help.UseVisualStyleBackColor = true;
            this.btn__Loadings_help.Click += new System.EventHandler(this.btn__Loadings_help_Click);
            // 
            // btn_edit_load_combs_IRC
            // 
            this.btn_edit_load_combs_IRC.Location = new System.Drawing.Point(243, 567);
            this.btn_edit_load_combs_IRC.Name = "btn_edit_load_combs_IRC";
            this.btn_edit_load_combs_IRC.Size = new System.Drawing.Size(175, 29);
            this.btn_edit_load_combs_IRC.TabIndex = 107;
            this.btn_edit_load_combs_IRC.Text = "Edit Load Combinations";
            this.btn_edit_load_combs_IRC.UseVisualStyleBackColor = true;
            this.btn_edit_load_combs_IRC.Click += new System.EventHandler(this.btn_edit_load_combs_IRC_Click);
            // 
            // groupBox117
            // 
            this.groupBox117.Controls.Add(this.label1173);
            this.groupBox117.Controls.Add(this.label1129);
            this.groupBox117.Controls.Add(this.dgv_long_loads);
            this.groupBox117.ForeColor = System.Drawing.Color.Black;
            this.groupBox117.Location = new System.Drawing.Point(3, 18);
            this.groupBox117.Name = "groupBox117";
            this.groupBox117.Size = new System.Drawing.Size(266, 370);
            this.groupBox117.TabIndex = 103;
            this.groupBox117.TabStop = false;
            this.groupBox117.Text = "                                               ";
            // 
            // label1173
            // 
            this.label1173.AutoSize = true;
            this.label1173.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1173.ForeColor = System.Drawing.Color.Black;
            this.label1173.Location = new System.Drawing.Point(8, -1);
            this.label1173.Name = "label1173";
            this.label1173.Size = new System.Drawing.Size(171, 13);
            this.label1173.TabIndex = 85;
            this.label1173.Text = "Apply Load Combinations";
            // 
            // label1129
            // 
            this.label1129.AutoSize = true;
            this.label1129.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1129.ForeColor = System.Drawing.Color.Black;
            this.label1129.Location = new System.Drawing.Point(3, 17);
            this.label1129.Name = "label1129";
            this.label1129.Size = new System.Drawing.Size(264, 13);
            this.label1129.TabIndex = 11;
            this.label1129.Text = "(Refer to Page 100, ASTRA Pro User Manual)";
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
            this.dataGridViewTextBoxColumn36,
            this.Column24,
            this.Column25,
            this.Column26,
            this.Column27,
            this.Column43});
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
            this.dgv_long_loads.Size = new System.Drawing.Size(260, 334);
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
            // Column24
            // 
            this.Column24.HeaderText = "6";
            this.Column24.Name = "Column24";
            this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column24.Width = 50;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "7";
            this.Column25.Name = "Column25";
            this.Column25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column25.Width = 50;
            // 
            // Column26
            // 
            this.Column26.HeaderText = "8";
            this.Column26.Name = "Column26";
            this.Column26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column26.Width = 50;
            // 
            // Column27
            // 
            this.Column27.HeaderText = "9";
            this.Column27.Name = "Column27";
            this.Column27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column27.Width = 50;
            // 
            // Column43
            // 
            this.Column43.HeaderText = "10";
            this.Column43.Name = "Column43";
            this.Column43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column43.Width = 50;
            // 
            // label1130
            // 
            this.label1130.AutoSize = true;
            this.label1130.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1130.ForeColor = System.Drawing.Color.Black;
            this.label1130.Location = new System.Drawing.Point(124, 2);
            this.label1130.Name = "label1130";
            this.label1130.Size = new System.Drawing.Size(687, 13);
            this.label1130.TabIndex = 105;
            this.label1130.Text = "GRILLAGE ANALYSIS OF SUPER STRUCTURE APPLYING 5 TYPES OF LIVE LOADS WITH SOME COM" +
    "BINATIONS";
            // 
            // btn_long_restore_ll_IRC
            // 
            this.btn_long_restore_ll_IRC.Location = new System.Drawing.Point(424, 567);
            this.btn_long_restore_ll_IRC.Name = "btn_long_restore_ll_IRC";
            this.btn_long_restore_ll_IRC.Size = new System.Drawing.Size(175, 29);
            this.btn_long_restore_ll_IRC.TabIndex = 104;
            this.btn_long_restore_ll_IRC.Text = "Restore Default Values";
            this.btn_long_restore_ll_IRC.UseVisualStyleBackColor = true;
            this.btn_long_restore_ll_IRC.Click += new System.EventHandler(this.btn_long_restore_ll_IRC_Click);
            // 
            // groupBox118
            // 
            this.groupBox118.Controls.Add(this.label1170);
            this.groupBox118.Controls.Add(this.label1132);
            this.groupBox118.Controls.Add(this.dgv_long_liveloads);
            this.groupBox118.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox118.ForeColor = System.Drawing.Color.Black;
            this.groupBox118.Location = new System.Drawing.Point(272, 18);
            this.groupBox118.Name = "groupBox118";
            this.groupBox118.Size = new System.Drawing.Size(662, 370);
            this.groupBox118.TabIndex = 102;
            this.groupBox118.TabStop = false;
            this.groupBox118.Text = "                                     ";
            // 
            // label1170
            // 
            this.label1170.AutoSize = true;
            this.label1170.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1170.ForeColor = System.Drawing.Color.Black;
            this.label1170.Location = new System.Drawing.Point(19, 0);
            this.label1170.Name = "label1170";
            this.label1170.Size = new System.Drawing.Size(127, 13);
            this.label1170.TabIndex = 85;
            this.label1170.Text = "Define Load Types";
            // 
            // label1132
            // 
            this.label1132.AutoSize = true;
            this.label1132.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1132.ForeColor = System.Drawing.Color.Black;
            this.label1132.Location = new System.Drawing.Point(2, 17);
            this.label1132.Name = "label1132";
            this.label1132.Size = new System.Drawing.Size(648, 13);
            this.label1132.TabIndex = 10;
            this.label1132.Text = "(USER MAY CHANGE THE VAUES IN THE CELLS, THE DATA WILL BE SAVED IN FILE \"LL.TXT\" " +
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
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn91,
            this.dataGridViewTextBoxColumn92,
            this.dataGridViewTextBoxColumn93,
            this.dataGridViewTextBoxColumn94,
            this.dataGridViewTextBoxColumn95,
            this.dataGridViewTextBoxColumn96,
            this.dataGridViewTextBoxColumn97,
            this.dataGridViewTextBoxColumn98,
            this.dataGridViewTextBoxColumn99,
            this.dataGridViewTextBoxColumn100,
            this.dataGridViewTextBoxColumn101,
            this.dataGridViewTextBoxColumn102,
            this.dataGridViewTextBoxColumn103});
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
            this.dgv_long_liveloads.Size = new System.Drawing.Size(656, 334);
            this.dgv_long_liveloads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn28.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 200;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "1";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.Width = 150;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "2";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.Width = 50;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.HeaderText = "3";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.Width = 50;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.HeaderText = "4";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.Width = 50;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.HeaderText = "5";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.Width = 50;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.HeaderText = "6";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.Width = 50;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.HeaderText = "7";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.Width = 50;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.HeaderText = "8";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.Width = 50;
            // 
            // dataGridViewTextBoxColumn91
            // 
            this.dataGridViewTextBoxColumn91.HeaderText = "9";
            this.dataGridViewTextBoxColumn91.Name = "dataGridViewTextBoxColumn91";
            this.dataGridViewTextBoxColumn91.Width = 50;
            // 
            // dataGridViewTextBoxColumn92
            // 
            this.dataGridViewTextBoxColumn92.HeaderText = "10";
            this.dataGridViewTextBoxColumn92.Name = "dataGridViewTextBoxColumn92";
            this.dataGridViewTextBoxColumn92.Width = 50;
            // 
            // dataGridViewTextBoxColumn93
            // 
            this.dataGridViewTextBoxColumn93.HeaderText = "11";
            this.dataGridViewTextBoxColumn93.Name = "dataGridViewTextBoxColumn93";
            this.dataGridViewTextBoxColumn93.Width = 50;
            // 
            // dataGridViewTextBoxColumn94
            // 
            this.dataGridViewTextBoxColumn94.HeaderText = "12";
            this.dataGridViewTextBoxColumn94.Name = "dataGridViewTextBoxColumn94";
            this.dataGridViewTextBoxColumn94.Width = 50;
            // 
            // dataGridViewTextBoxColumn95
            // 
            this.dataGridViewTextBoxColumn95.HeaderText = "13";
            this.dataGridViewTextBoxColumn95.Name = "dataGridViewTextBoxColumn95";
            this.dataGridViewTextBoxColumn95.Width = 50;
            // 
            // dataGridViewTextBoxColumn96
            // 
            this.dataGridViewTextBoxColumn96.HeaderText = "14";
            this.dataGridViewTextBoxColumn96.Name = "dataGridViewTextBoxColumn96";
            this.dataGridViewTextBoxColumn96.Width = 50;
            // 
            // dataGridViewTextBoxColumn97
            // 
            this.dataGridViewTextBoxColumn97.HeaderText = "15";
            this.dataGridViewTextBoxColumn97.Name = "dataGridViewTextBoxColumn97";
            this.dataGridViewTextBoxColumn97.Width = 50;
            // 
            // dataGridViewTextBoxColumn98
            // 
            this.dataGridViewTextBoxColumn98.HeaderText = "16";
            this.dataGridViewTextBoxColumn98.Name = "dataGridViewTextBoxColumn98";
            this.dataGridViewTextBoxColumn98.Width = 50;
            // 
            // dataGridViewTextBoxColumn99
            // 
            this.dataGridViewTextBoxColumn99.HeaderText = "17";
            this.dataGridViewTextBoxColumn99.Name = "dataGridViewTextBoxColumn99";
            this.dataGridViewTextBoxColumn99.Width = 50;
            // 
            // dataGridViewTextBoxColumn100
            // 
            this.dataGridViewTextBoxColumn100.HeaderText = "18";
            this.dataGridViewTextBoxColumn100.Name = "dataGridViewTextBoxColumn100";
            this.dataGridViewTextBoxColumn100.Width = 50;
            // 
            // dataGridViewTextBoxColumn101
            // 
            this.dataGridViewTextBoxColumn101.HeaderText = "19";
            this.dataGridViewTextBoxColumn101.Name = "dataGridViewTextBoxColumn101";
            this.dataGridViewTextBoxColumn101.Width = 50;
            // 
            // dataGridViewTextBoxColumn102
            // 
            this.dataGridViewTextBoxColumn102.HeaderText = "20";
            this.dataGridViewTextBoxColumn102.Name = "dataGridViewTextBoxColumn102";
            this.dataGridViewTextBoxColumn102.Width = 50;
            // 
            // dataGridViewTextBoxColumn103
            // 
            this.dataGridViewTextBoxColumn103.HeaderText = "21";
            this.dataGridViewTextBoxColumn103.Name = "dataGridViewTextBoxColumn103";
            this.dataGridViewTextBoxColumn103.Width = 50;
            // 
            // tab_analysis
            // 
            this.tab_analysis.Controls.Add(this.tc_process);
            this.tab_analysis.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis.Name = "tab_analysis";
            this.tab_analysis.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis.Size = new System.Drawing.Size(936, 634);
            this.tab_analysis.TabIndex = 1;
            this.tab_analysis.Text = "Analysis Process";
            this.tab_analysis.UseVisualStyleBackColor = true;
            // 
            // tc_process
            // 
            this.tc_process.Controls.Add(this.tab_analysis_data);
            this.tc_process.Controls.Add(this.tab_process);
            this.tc_process.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_process.Location = new System.Drawing.Point(3, 3);
            this.tc_process.Name = "tc_process";
            this.tc_process.SelectedIndex = 0;
            this.tc_process.Size = new System.Drawing.Size(930, 628);
            this.tc_process.TabIndex = 105;
            this.tc_process.SelectedIndexChanged += new System.EventHandler(this.tc_AnaProcess_SelectedIndexChanged);
            // 
            // tab_analysis_data
            // 
            this.tab_analysis_data.Controls.Add(this.groupBox136);
            this.tab_analysis_data.Controls.Add(this.groupBox70);
            this.tab_analysis_data.Controls.Add(this.btn_Ana_DL_create_data);
            this.tab_analysis_data.Controls.Add(this.groupBox71);
            this.tab_analysis_data.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis_data.Name = "tab_analysis_data";
            this.tab_analysis_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis_data.Size = new System.Drawing.Size(922, 602);
            this.tab_analysis_data.TabIndex = 0;
            this.tab_analysis_data.Text = "Analysis Data";
            this.tab_analysis_data.UseVisualStyleBackColor = true;
            // 
            // groupBox136
            // 
            this.groupBox136.Controls.Add(this.label1174);
            this.groupBox136.Controls.Add(this.cmb_long_open_file_analysis);
            this.groupBox136.Controls.Add(this.btn_view_data_1);
            this.groupBox136.ForeColor = System.Drawing.Color.Black;
            this.groupBox136.Location = new System.Drawing.Point(217, 328);
            this.groupBox136.Name = "groupBox136";
            this.groupBox136.Size = new System.Drawing.Size(530, 88);
            this.groupBox136.TabIndex = 134;
            this.groupBox136.TabStop = false;
            this.groupBox136.Text = "Open Analysis File";
            // 
            // label1174
            // 
            this.label1174.AutoSize = true;
            this.label1174.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1174.Location = new System.Drawing.Point(8, 21);
            this.label1174.Name = "label1174";
            this.label1174.Size = new System.Drawing.Size(135, 16);
            this.label1174.TabIndex = 100;
            this.label1174.Text = "Select Analysis File";
            // 
            // cmb_long_open_file_analysis
            // 
            this.cmb_long_open_file_analysis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_long_open_file_analysis.FormattingEnabled = true;
            this.cmb_long_open_file_analysis.Items.AddRange(new object[] {
            "Dead Load Analysis",
            "Live Load Analysis",
            "Total DL+SIDL+LL Analysis"});
            this.cmb_long_open_file_analysis.Location = new System.Drawing.Point(149, 20);
            this.cmb_long_open_file_analysis.Name = "cmb_long_open_file_analysis";
            this.cmb_long_open_file_analysis.Size = new System.Drawing.Size(365, 21);
            this.cmb_long_open_file_analysis.TabIndex = 79;
            this.cmb_long_open_file_analysis.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_preprocess_SelectedIndexChanged);
            // 
            // btn_view_data_1
            // 
            this.btn_view_data_1.Location = new System.Drawing.Point(176, 47);
            this.btn_view_data_1.Name = "btn_view_data_1";
            this.btn_view_data_1.Size = new System.Drawing.Size(165, 31);
            this.btn_view_data_1.TabIndex = 74;
            this.btn_view_data_1.Text = "View Analysis Data";
            this.btn_view_data_1.UseVisualStyleBackColor = true;
            this.btn_view_data_1.Click += new System.EventHandler(this.btn_view_data_Click);
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
            this.groupBox70.Location = new System.Drawing.Point(304, 132);
            this.groupBox70.Name = "groupBox70";
            this.groupBox70.Size = new System.Drawing.Size(351, 84);
            this.groupBox70.TabIndex = 132;
            this.groupBox70.TabStop = false;
            this.groupBox70.Text = "SUPPORT AT END";
            // 
            // rbtn_esprt_pinned
            // 
            this.rbtn_esprt_pinned.AutoSize = true;
            this.rbtn_esprt_pinned.Location = new System.Drawing.Point(6, 27);
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
            this.rbtn_esprt_fixed.Location = new System.Drawing.Point(6, 50);
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
            this.chk_esprt_fixed_MZ.Location = new System.Drawing.Point(290, 50);
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
            this.chk_esprt_fixed_FZ.Location = new System.Drawing.Point(162, 50);
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
            this.chk_esprt_fixed_MY.Location = new System.Drawing.Point(252, 50);
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
            this.chk_esprt_fixed_FY.Location = new System.Drawing.Point(118, 50);
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
            this.chk_esprt_fixed_MX.Location = new System.Drawing.Point(203, 50);
            this.chk_esprt_fixed_MX.Name = "chk_esprt_fixed_MX";
            this.chk_esprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MX.TabIndex = 0;
            this.chk_esprt_fixed_MX.Text = "MX";
            this.chk_esprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FX
            // 
            this.chk_esprt_fixed_FX.AutoSize = true;
            this.chk_esprt_fixed_FX.Location = new System.Drawing.Point(72, 50);
            this.chk_esprt_fixed_FX.Name = "chk_esprt_fixed_FX";
            this.chk_esprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FX.TabIndex = 0;
            this.chk_esprt_fixed_FX.Text = "FX";
            this.chk_esprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // btn_Ana_DL_create_data
            // 
            this.btn_Ana_DL_create_data.Location = new System.Drawing.Point(351, 249);
            this.btn_Ana_DL_create_data.Name = "btn_Ana_DL_create_data";
            this.btn_Ana_DL_create_data.Size = new System.Drawing.Size(261, 47);
            this.btn_Ana_DL_create_data.TabIndex = 46;
            this.btn_Ana_DL_create_data.Text = "Create Analysis Data\r\n(in each separate file)";
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
            this.groupBox71.Location = new System.Drawing.Point(304, 18);
            this.groupBox71.Name = "groupBox71";
            this.groupBox71.Size = new System.Drawing.Size(339, 82);
            this.groupBox71.TabIndex = 133;
            this.groupBox71.TabStop = false;
            this.groupBox71.Text = "SUPPORT AT START";
            // 
            // rbtn_ssprt_pinned
            // 
            this.rbtn_ssprt_pinned.AutoSize = true;
            this.rbtn_ssprt_pinned.Checked = true;
            this.rbtn_ssprt_pinned.Location = new System.Drawing.Point(6, 20);
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
            this.rbtn_ssprt_fixed.Location = new System.Drawing.Point(6, 48);
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
            this.chk_ssprt_fixed_MZ.Location = new System.Drawing.Point(290, 48);
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
            this.chk_ssprt_fixed_FZ.Location = new System.Drawing.Point(162, 48);
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
            this.chk_ssprt_fixed_MY.Location = new System.Drawing.Point(252, 48);
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
            this.chk_ssprt_fixed_FY.Location = new System.Drawing.Point(118, 48);
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
            this.chk_ssprt_fixed_MX.Location = new System.Drawing.Point(203, 48);
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
            this.chk_ssprt_fixed_FX.Location = new System.Drawing.Point(72, 48);
            this.chk_ssprt_fixed_FX.Name = "chk_ssprt_fixed_FX";
            this.chk_ssprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FX.TabIndex = 0;
            this.chk_ssprt_fixed_FX.Text = "FX";
            this.chk_ssprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // tab_process
            // 
            this.tab_process.Controls.Add(this.splitContainer1);
            this.tab_process.Location = new System.Drawing.Point(4, 22);
            this.tab_process.Name = "tab_process";
            this.tab_process.Size = new System.Drawing.Size(922, 602);
            this.tab_process.TabIndex = 2;
            this.tab_process.Text = "Process";
            this.tab_process.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(922, 602);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 104;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Process_LL_Analysis);
            this.groupBox2.Controls.Add(this.groupBox109);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 246);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            // 
            // btn_Process_LL_Analysis
            // 
            this.btn_Process_LL_Analysis.Location = new System.Drawing.Point(60, 38);
            this.btn_Process_LL_Analysis.Name = "btn_Process_LL_Analysis";
            this.btn_Process_LL_Analysis.Size = new System.Drawing.Size(182, 53);
            this.btn_Process_LL_Analysis.TabIndex = 104;
            this.btn_Process_LL_Analysis.Text = "Process Analysis\r\n(for all data files)";
            this.btn_Process_LL_Analysis.UseVisualStyleBackColor = true;
            this.btn_Process_LL_Analysis.Click += new System.EventHandler(this.btn_Ana_LL_process_analysis_Click);
            // 
            // groupBox109
            // 
            this.groupBox109.Controls.Add(this.cmb_long_open_file_process);
            this.groupBox109.Controls.Add(this.btn_View_Result_summary);
            this.groupBox109.Controls.Add(this.btn_view_report);
            this.groupBox109.Controls.Add(this.btn_view_data);
            this.groupBox109.Controls.Add(this.btn_view_postprocess);
            this.groupBox109.Controls.Add(this.btn_view_preprocess);
            this.groupBox109.Location = new System.Drawing.Point(252, 20);
            this.groupBox109.Name = "groupBox109";
            this.groupBox109.Size = new System.Drawing.Size(668, 89);
            this.groupBox109.TabIndex = 106;
            this.groupBox109.TabStop = false;
            this.groupBox109.Text = "Open Analysis File";
            // 
            // cmb_long_open_file_process
            // 
            this.cmb_long_open_file_process.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_long_open_file_process.FormattingEnabled = true;
            this.cmb_long_open_file_process.Items.AddRange(new object[] {
            "Dead Load Analysis",
            "Live Load Analysis",
            "Total DL+SIDL+LL Analysis"});
            this.cmb_long_open_file_process.Location = new System.Drawing.Point(6, 18);
            this.cmb_long_open_file_process.Name = "cmb_long_open_file_process";
            this.cmb_long_open_file_process.Size = new System.Drawing.Size(459, 21);
            this.cmb_long_open_file_process.TabIndex = 79;
            this.cmb_long_open_file_process.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            // 
            // btn_View_Result_summary
            // 
            this.btn_View_Result_summary.Location = new System.Drawing.Point(319, 51);
            this.btn_View_Result_summary.Name = "btn_View_Result_summary";
            this.btn_View_Result_summary.Size = new System.Drawing.Size(146, 32);
            this.btn_View_Result_summary.TabIndex = 78;
            this.btn_View_Result_summary.Text = "Result Summary";
            this.btn_View_Result_summary.UseVisualStyleBackColor = true;
            this.btn_View_Result_summary.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(164, 51);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(146, 32);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(6, 51);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(146, 32);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_postprocess
            // 
            this.btn_view_postprocess.Location = new System.Drawing.Point(471, 51);
            this.btn_view_postprocess.Name = "btn_view_postprocess";
            this.btn_view_postprocess.Size = new System.Drawing.Size(146, 32);
            this.btn_view_postprocess.TabIndex = 74;
            this.btn_view_postprocess.Text = "View Post Process";
            this.btn_view_postprocess.UseVisualStyleBackColor = true;
            this.btn_view_postprocess.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_preprocess
            // 
            this.btn_view_preprocess.Location = new System.Drawing.Point(471, 16);
            this.btn_view_preprocess.Name = "btn_view_preprocess";
            this.btn_view_preprocess.Size = new System.Drawing.Size(146, 29);
            this.btn_view_preprocess.TabIndex = 74;
            this.btn_view_preprocess.Text = "View Pre Process";
            this.btn_view_preprocess.UseVisualStyleBackColor = true;
            this.btn_view_preprocess.Click += new System.EventHandler(this.btn_view_data_Click);
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
            this.tabControl5.Controls.Add(this.tabPage6);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(0, 0);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(920, 480);
            this.tabControl5.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox44);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(912, 454);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Analysis Results";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.label238);
            this.groupBox44.Controls.Add(this.label164);
            this.groupBox44.Controls.Add(this.groupBox11);
            this.groupBox44.Controls.Add(this.groupBox58);
            this.groupBox44.Controls.Add(this.groupBox59);
            this.groupBox44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox44.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox44.ForeColor = System.Drawing.Color.Red;
            this.groupBox44.Location = new System.Drawing.Point(3, 3);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(906, 448);
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
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Controls.Add(this.groupBox14);
            this.groupBox11.Location = new System.Drawing.Point(3, 153);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(905, 256);
            this.groupBox11.TabIndex = 106;
            this.groupBox11.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.groupBox16);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(555, 17);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(343, 236);
            this.groupBox7.TabIndex = 105;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Super Imposed Dead Load [SIDL+FPLL] Analysis Result";
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
            this.groupBox16.Size = new System.Drawing.Size(337, 216);
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
            this.label160.Location = new System.Drawing.Point(228, 31);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(30, 13);
            this.label160.TabIndex = 35;
            this.label160.Text = "(FT)";
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
            this.label161.Size = new System.Drawing.Size(55, 13);
            this.label161.TabIndex = 36;
            this.label161.Text = "(KIP-FT)";
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
            this.label185.Location = new System.Drawing.Point(228, 15);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.grb_Ana_Res_DL);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(281, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 236);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dead Load [DL] Analysis Result";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.groupBox17);
            this.groupBox14.Controls.Add(this.grb_Ana_Res_LL);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.ForeColor = System.Drawing.Color.Red;
            this.groupBox14.Location = new System.Drawing.Point(3, 17);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(278, 236);
            this.groupBox14.TabIndex = 105;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Live Load (Moving Load) Analysis Result";
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
            this.label154.Location = new System.Drawing.Point(177, 32);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(30, 13);
            this.label154.TabIndex = 33;
            this.label154.Text = "(FT)";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(174, 16);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(41, 13);
            this.label156.TabIndex = 32;
            this.label156.Text = "Shear";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.ForeColor = System.Drawing.Color.Blue;
            this.label158.Location = new System.Drawing.Point(66, 32);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(55, 13);
            this.label158.TabIndex = 34;
            this.label158.Text = "(KIP-FT)";
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(66, 16);
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
            // groupBox58
            // 
            this.groupBox58.Controls.Add(this.label534);
            this.groupBox58.Controls.Add(this.txt_ana_TSRP);
            this.groupBox58.Controls.Add(this.label535);
            this.groupBox58.Controls.Add(this.label536);
            this.groupBox58.Controls.Add(this.txt_ana_MSTD);
            this.groupBox58.Controls.Add(this.label537);
            this.groupBox58.Controls.Add(this.label538);
            this.groupBox58.Controls.Add(this.txt_ana_MSLD);
            this.groupBox58.Controls.Add(this.label539);
            this.groupBox58.Location = new System.Drawing.Point(373, 52);
            this.groupBox58.Name = "groupBox58";
            this.groupBox58.Size = new System.Drawing.Size(483, 99);
            this.groupBox58.TabIndex = 91;
            this.groupBox58.TabStop = false;
            this.groupBox58.Text = "Design Forces";
            // 
            // label534
            // 
            this.label534.AutoSize = true;
            this.label534.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label534.Location = new System.Drawing.Point(401, 22);
            this.label534.Name = "label534";
            this.label534.Size = new System.Drawing.Size(29, 13);
            this.label534.TabIndex = 104;
            this.label534.Text = "KIP";
            // 
            // txt_ana_TSRP
            // 
            this.txt_ana_TSRP.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_TSRP.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_TSRP.Location = new System.Drawing.Point(308, 19);
            this.txt_ana_TSRP.Name = "txt_ana_TSRP";
            this.txt_ana_TSRP.Size = new System.Drawing.Size(87, 20);
            this.txt_ana_TSRP.TabIndex = 5;
            this.txt_ana_TSRP.Text = "0";
            this.txt_ana_TSRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.label536.Location = new System.Drawing.Point(402, 74);
            this.label536.Name = "label536";
            this.label536.Size = new System.Drawing.Size(51, 13);
            this.label536.TabIndex = 101;
            this.label536.Text = "KIP-FT";
            // 
            // txt_ana_MSTD
            // 
            this.txt_ana_MSTD.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_MSTD.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_MSTD.Location = new System.Drawing.Point(308, 71);
            this.txt_ana_MSTD.Name = "txt_ana_MSTD";
            this.txt_ana_MSTD.Size = new System.Drawing.Size(88, 20);
            this.txt_ana_MSTD.TabIndex = 4;
            this.txt_ana_MSTD.Text = "0";
            this.txt_ana_MSTD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label537
            // 
            this.label537.AutoSize = true;
            this.label537.Location = new System.Drawing.Point(8, 74);
            this.label537.Name = "label537";
            this.label537.Size = new System.Drawing.Size(294, 13);
            this.label537.TabIndex = 102;
            this.label537.Text = "Moment at Supports in Transverse Direction [Mz1]";
            // 
            // label538
            // 
            this.label538.AutoSize = true;
            this.label538.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label538.Location = new System.Drawing.Point(399, 48);
            this.label538.Name = "label538";
            this.label538.Size = new System.Drawing.Size(51, 13);
            this.label538.TabIndex = 98;
            this.label538.Text = "KIP-FT";
            // 
            // txt_ana_MSLD
            // 
            this.txt_ana_MSLD.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ana_MSLD.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_MSLD.Location = new System.Drawing.Point(308, 45);
            this.txt_ana_MSLD.Name = "txt_ana_MSLD";
            this.txt_ana_MSLD.Size = new System.Drawing.Size(88, 20);
            this.txt_ana_MSLD.TabIndex = 3;
            this.txt_ana_MSLD.Text = "0";
            this.txt_ana_MSLD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label539
            // 
            this.label539.AutoSize = true;
            this.label539.Location = new System.Drawing.Point(7, 48);
            this.label539.Name = "label539";
            this.label539.Size = new System.Drawing.Size(300, 13);
            this.label539.TabIndex = 99;
            this.label539.Text = "Moment at Supports in Longitudinal Direction [Mx1]";
            // 
            // groupBox59
            // 
            this.groupBox59.Controls.Add(this.label541);
            this.groupBox59.Controls.Add(this.label540);
            this.groupBox59.Controls.Add(this.txt_ana_DLSR);
            this.groupBox59.Controls.Add(this.label532);
            this.groupBox59.Controls.Add(this.label533);
            this.groupBox59.Controls.Add(this.txt_ana_LLSR);
            this.groupBox59.Location = new System.Drawing.Point(3, 51);
            this.groupBox59.Name = "groupBox59";
            this.groupBox59.Size = new System.Drawing.Size(364, 100);
            this.groupBox59.TabIndex = 82;
            this.groupBox59.TabStop = false;
            this.groupBox59.Text = "Support Reactions per unit width of Abutment/Pier";
            // 
            // label541
            // 
            this.label541.AutoSize = true;
            this.label541.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label541.Location = new System.Drawing.Point(273, 72);
            this.label541.Name = "label541";
            this.label541.Size = new System.Drawing.Size(53, 13);
            this.label541.TabIndex = 104;
            this.label541.Text = "KIP/FT";
            // 
            // label540
            // 
            this.label540.AutoSize = true;
            this.label540.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label540.Location = new System.Drawing.Point(272, 24);
            this.label540.Name = "label540";
            this.label540.Size = new System.Drawing.Size(53, 13);
            this.label540.TabIndex = 104;
            this.label540.Text = "KIP/FT";
            // 
            // txt_ana_DLSR
            // 
            this.txt_ana_DLSR.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_DLSR.Location = new System.Drawing.Point(186, 21);
            this.txt_ana_DLSR.Name = "txt_ana_DLSR";
            this.txt_ana_DLSR.Size = new System.Drawing.Size(80, 21);
            this.txt_ana_DLSR.TabIndex = 83;
            this.txt_ana_DLSR.Text = "0";
            this.txt_ana_DLSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label532
            // 
            this.label532.AutoSize = true;
            this.label532.Location = new System.Drawing.Point(6, 75);
            this.label532.Name = "label532";
            this.label532.Size = new System.Drawing.Size(163, 13);
            this.label532.TabIndex = 86;
            this.label532.Text = "Live Load Support Reaction";
            // 
            // label533
            // 
            this.label533.AutoSize = true;
            this.label533.Location = new System.Drawing.Point(5, 24);
            this.label533.Name = "label533";
            this.label533.Size = new System.Drawing.Size(170, 13);
            this.label533.TabIndex = 84;
            this.label533.Text = "Dead Load Support Reaction";
            this.label533.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_ana_LLSR
            // 
            this.txt_ana_LLSR.ForeColor = System.Drawing.Color.Red;
            this.txt_ana_LLSR.Location = new System.Drawing.Point(186, 68);
            this.txt_ana_LLSR.Name = "txt_ana_LLSR";
            this.txt_ana_LLSR.Size = new System.Drawing.Size(80, 21);
            this.txt_ana_LLSR.TabIndex = 85;
            this.txt_ana_LLSR.Text = "0";
            this.txt_ana_LLSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox62);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(912, 454);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Reaction Forces";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox62
            // 
            this.groupBox62.Controls.Add(this.label276);
            this.groupBox62.Controls.Add(this.txt_final_Mx);
            this.groupBox62.Controls.Add(this.txt_final_Mz);
            this.groupBox62.Controls.Add(this.label130);
            this.groupBox62.Controls.Add(this.label129);
            this.groupBox62.Controls.Add(this.label278);
            this.groupBox62.Controls.Add(this.groupBox65);
            this.groupBox62.Controls.Add(this.groupBox66);
            this.groupBox62.Controls.Add(this.groupBox67);
            this.groupBox62.Controls.Add(this.groupBox68);
            this.groupBox62.Controls.Add(this.g);
            this.groupBox62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox62.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox62.Location = new System.Drawing.Point(3, 3);
            this.groupBox62.Name = "groupBox62";
            this.groupBox62.Size = new System.Drawing.Size(906, 448);
            this.groupBox62.TabIndex = 29;
            this.groupBox62.TabStop = false;
            // 
            // label276
            // 
            this.label276.AutoSize = true;
            this.label276.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label276.Location = new System.Drawing.Point(276, 135);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(55, 13);
            this.label276.TabIndex = 21;
            this.label276.Text = "KIP-FT";
            // 
            // txt_final_Mx
            // 
            this.txt_final_Mx.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_Mx.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_Mx.Location = new System.Drawing.Point(189, 102);
            this.txt_final_Mx.Name = "txt_final_Mx";
            this.txt_final_Mx.Size = new System.Drawing.Size(85, 20);
            this.txt_final_Mx.TabIndex = 12;
            this.txt_final_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_final_Mz
            // 
            this.txt_final_Mz.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_Mz.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_Mz.Location = new System.Drawing.Point(189, 132);
            this.txt_final_Mz.Name = "txt_final_Mz";
            this.txt_final_Mz.Size = new System.Drawing.Size(85, 20);
            this.txt_final_Mz.TabIndex = 16;
            this.txt_final_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label130.Location = new System.Drawing.Point(17, 135);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(87, 13);
            this.label130.TabIndex = 21;
            this.label130.Text = "Maximum Mz";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label129.Location = new System.Drawing.Point(17, 105);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(87, 13);
            this.label129.TabIndex = 21;
            this.label129.Text = "Maximum Mx";
            // 
            // label278
            // 
            this.label278.AutoSize = true;
            this.label278.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label278.Location = new System.Drawing.Point(276, 105);
            this.label278.Name = "label278";
            this.label278.Size = new System.Drawing.Size(55, 13);
            this.label278.TabIndex = 21;
            this.label278.Text = "KIP/FT";
            // 
            // groupBox65
            // 
            this.groupBox65.Controls.Add(this.lbl_factor);
            this.groupBox65.Controls.Add(this.txt_ll_factor);
            this.groupBox65.Controls.Add(this.txt_dl_factor);
            this.groupBox65.Controls.Add(this.label280);
            this.groupBox65.Controls.Add(this.textBox17);
            this.groupBox65.Controls.Add(this.label132);
            this.groupBox65.Controls.Add(this.label131);
            this.groupBox65.Controls.Add(this.txt_final_vert_reac);
            this.groupBox65.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox65.ForeColor = System.Drawing.Color.Red;
            this.groupBox65.Location = new System.Drawing.Point(9, 21);
            this.groupBox65.Name = "groupBox65";
            this.groupBox65.Size = new System.Drawing.Size(310, 71);
            this.groupBox65.TabIndex = 26;
            this.groupBox65.TabStop = false;
            this.groupBox65.Text = "Total Factored Vertical Reaction";
            // 
            // lbl_factor
            // 
            this.lbl_factor.AutoSize = true;
            this.lbl_factor.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.lbl_factor.ForeColor = System.Drawing.Color.Blue;
            this.lbl_factor.Location = new System.Drawing.Point(3, 48);
            this.lbl_factor.Name = "lbl_factor";
            this.lbl_factor.Size = new System.Drawing.Size(167, 13);
            this.lbl_factor.TabIndex = 23;
            this.lbl_factor.Text = "DL X 1.25 + LL X 2.5";
            // 
            // txt_ll_factor
            // 
            this.txt_ll_factor.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_ll_factor.ForeColor = System.Drawing.Color.Blue;
            this.txt_ll_factor.Location = new System.Drawing.Point(243, 19);
            this.txt_ll_factor.Name = "txt_ll_factor";
            this.txt_ll_factor.Size = new System.Drawing.Size(47, 20);
            this.txt_ll_factor.TabIndex = 12;
            this.txt_ll_factor.Text = "2.5";
            this.txt_ll_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_dl_factor
            // 
            this.txt_dl_factor.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dl_factor.ForeColor = System.Drawing.Color.Blue;
            this.txt_dl_factor.Location = new System.Drawing.Point(95, 19);
            this.txt_dl_factor.Name = "txt_dl_factor";
            this.txt_dl_factor.Size = new System.Drawing.Size(47, 20);
            this.txt_dl_factor.TabIndex = 12;
            this.txt_dl_factor.Text = "1.25";
            this.txt_dl_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label280
            // 
            this.label280.AutoSize = true;
            this.label280.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label280.Location = new System.Drawing.Point(271, 48);
            this.label280.Name = "label280";
            this.label280.Size = new System.Drawing.Size(31, 13);
            this.label280.TabIndex = 19;
            this.label280.Text = "KIP";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(364, 176);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(76, 20);
            this.textBox17.TabIndex = 6;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label132.Location = new System.Drawing.Point(158, 22);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(79, 13);
            this.label132.TabIndex = 21;
            this.label132.Text = "LL FACTOR";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label131.Location = new System.Drawing.Point(10, 22);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(79, 13);
            this.label131.TabIndex = 21;
            this.label131.Text = "DL FACTOR";
            // 
            // txt_final_vert_reac
            // 
            this.txt_final_vert_reac.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_vert_reac.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_vert_reac.Location = new System.Drawing.Point(180, 45);
            this.txt_final_vert_reac.Name = "txt_final_vert_reac";
            this.txt_final_vert_reac.Size = new System.Drawing.Size(85, 20);
            this.txt_final_vert_reac.TabIndex = 11;
            this.txt_final_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox66
            // 
            this.groupBox66.Controls.Add(this.txt_left_total_Mz);
            this.groupBox66.Controls.Add(this.txt_left_total_Mx);
            this.groupBox66.Controls.Add(this.label325);
            this.groupBox66.Controls.Add(this.label326);
            this.groupBox66.Controls.Add(this.txt_left_total_vert_reac);
            this.groupBox66.Controls.Add(this.dgv_left_des_frc);
            this.groupBox66.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox66.Location = new System.Drawing.Point(9, 246);
            this.groupBox66.Name = "groupBox66";
            this.groupBox66.Size = new System.Drawing.Size(459, 172);
            this.groupBox66.TabIndex = 24;
            this.groupBox66.TabStop = false;
            this.groupBox66.Text = "Left End Design Forces";
            // 
            // txt_left_total_Mz
            // 
            this.txt_left_total_Mz.Location = new System.Drawing.Point(348, 139);
            this.txt_left_total_Mz.Name = "txt_left_total_Mz";
            this.txt_left_total_Mz.ReadOnly = true;
            this.txt_left_total_Mz.Size = new System.Drawing.Size(87, 20);
            this.txt_left_total_Mz.TabIndex = 6;
            this.txt_left_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_left_total_Mx
            // 
            this.txt_left_total_Mx.Location = new System.Drawing.Point(243, 139);
            this.txt_left_total_Mx.Name = "txt_left_total_Mx";
            this.txt_left_total_Mx.ReadOnly = true;
            this.txt_left_total_Mx.Size = new System.Drawing.Size(87, 20);
            this.txt_left_total_Mx.TabIndex = 5;
            this.txt_left_total_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label325
            // 
            this.label325.AutoSize = true;
            this.label325.Location = new System.Drawing.Point(95, 142);
            this.label325.Name = "label325";
            this.label325.Size = new System.Drawing.Size(47, 13);
            this.label325.TabIndex = 4;
            this.label325.Text = "Total";
            // 
            // label326
            // 
            this.label326.AutoSize = true;
            this.label326.Location = new System.Drawing.Point(7, 142);
            this.label326.Name = "label326";
            this.label326.Size = new System.Drawing.Size(71, 13);
            this.label326.TabIndex = 3;
            this.label326.Text = "Left End";
            // 
            // txt_left_total_vert_reac
            // 
            this.txt_left_total_vert_reac.Location = new System.Drawing.Point(148, 139);
            this.txt_left_total_vert_reac.Name = "txt_left_total_vert_reac";
            this.txt_left_total_vert_reac.ReadOnly = true;
            this.txt_left_total_vert_reac.Size = new System.Drawing.Size(76, 20);
            this.txt_left_total_vert_reac.TabIndex = 2;
            this.txt_left_total_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgv_left_des_frc
            // 
            this.dgv_left_des_frc.AllowUserToResizeColumns = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_left_des_frc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left_des_frc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.col_Max_Mx,
            this.col_Max_Mz});
            this.dgv_left_des_frc.Location = new System.Drawing.Point(6, 19);
            this.dgv_left_des_frc.Name = "dgv_left_des_frc";
            this.dgv_left_des_frc.ReadOnly = true;
            this.dgv_left_des_frc.Size = new System.Drawing.Size(448, 114);
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn10.HeaderText = "Vertical Reaction (KIP)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 118;
            // 
            // col_Max_Mx
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mx.DefaultCellStyle = dataGridViewCellStyle11;
            this.col_Max_Mx.HeaderText = "Maximum    Mx   (KIP-FT)";
            this.col_Max_Mx.Name = "col_Max_Mx";
            this.col_Max_Mx.ReadOnly = true;
            this.col_Max_Mx.Width = 108;
            // 
            // col_Max_Mz
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mz.DefaultCellStyle = dataGridViewCellStyle12;
            this.col_Max_Mz.HeaderText = "Maximum   Mz  (KIP-FT)";
            this.col_Max_Mz.Name = "col_Max_Mz";
            this.col_Max_Mz.ReadOnly = true;
            this.col_Max_Mz.Width = 108;
            // 
            // groupBox67
            // 
            this.groupBox67.Controls.Add(this.label327);
            this.groupBox67.Controls.Add(this.txt_dead_kN_m);
            this.groupBox67.Controls.Add(this.label370);
            this.groupBox67.Controls.Add(this.label371);
            this.groupBox67.Controls.Add(this.dgv_left_end_design_forces);
            this.groupBox67.Controls.Add(this.txt_dead_vert_reac_ton);
            this.groupBox67.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox67.Location = new System.Drawing.Point(328, 11);
            this.groupBox67.Name = "groupBox67";
            this.groupBox67.Size = new System.Drawing.Size(294, 229);
            this.groupBox67.TabIndex = 22;
            this.groupBox67.TabStop = false;
            this.groupBox67.Text = "Support Reactions from Dead Load";
            // 
            // label327
            // 
            this.label327.AutoSize = true;
            this.label327.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label327.Location = new System.Drawing.Point(238, 184);
            this.label327.Name = "label327";
            this.label327.Size = new System.Drawing.Size(55, 13);
            this.label327.TabIndex = 28;
            this.label327.Text = "KIP/FT";
            // 
            // txt_dead_kN_m
            // 
            this.txt_dead_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_kN_m.Location = new System.Drawing.Point(124, 181);
            this.txt_dead_kN_m.Name = "txt_dead_kN_m";
            this.txt_dead_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_kN_m.TabIndex = 27;
            this.txt_dead_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label370
            // 
            this.label370.AutoSize = true;
            this.label370.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label370.Location = new System.Drawing.Point(44, 158);
            this.label370.Name = "label370";
            this.label370.Size = new System.Drawing.Size(55, 13);
            this.label370.TabIndex = 24;
            this.label370.Text = "Total ";
            // 
            // label371
            // 
            this.label371.AutoSize = true;
            this.label371.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label371.Location = new System.Drawing.Point(238, 158);
            this.label371.Name = "label371";
            this.label371.Size = new System.Drawing.Size(31, 13);
            this.label371.TabIndex = 23;
            this.label371.Text = "KIP";
            // 
            // dgv_left_end_design_forces
            // 
            this.dgv_left_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_left_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Joints,
            this.col_Vert_Reaction});
            this.dgv_left_end_design_forces.Location = new System.Drawing.Point(18, 19);
            this.dgv_left_end_design_forces.Name = "dgv_left_end_design_forces";
            this.dgv_left_end_design_forces.ReadOnly = true;
            this.dgv_left_end_design_forces.Size = new System.Drawing.Size(258, 130);
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
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Vert_Reaction.DefaultCellStyle = dataGridViewCellStyle14;
            this.col_Vert_Reaction.HeaderText = "Vertical Reaction (KIP)";
            this.col_Vert_Reaction.Name = "col_Vert_Reaction";
            this.col_Vert_Reaction.ReadOnly = true;
            this.col_Vert_Reaction.Width = 118;
            // 
            // txt_dead_vert_reac_ton
            // 
            this.txt_dead_vert_reac_ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_ton.Location = new System.Drawing.Point(124, 155);
            this.txt_dead_vert_reac_ton.Name = "txt_dead_vert_reac_ton";
            this.txt_dead_vert_reac_ton.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_ton.TabIndex = 11;
            this.txt_dead_vert_reac_ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_dead_vert_reac_ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_Ton_TextChanged);
            // 
            // groupBox68
            // 
            this.groupBox68.Controls.Add(this.txt_live_kN_m);
            this.groupBox68.Controls.Add(this.label388);
            this.groupBox68.Controls.Add(this.label400);
            this.groupBox68.Controls.Add(this.dgv_right_end_design_forces);
            this.groupBox68.Controls.Add(this.txt_live_vert_rec_Ton);
            this.groupBox68.Controls.Add(this.label401);
            this.groupBox68.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox68.Location = new System.Drawing.Point(628, 11);
            this.groupBox68.Name = "groupBox68";
            this.groupBox68.Size = new System.Drawing.Size(286, 229);
            this.groupBox68.TabIndex = 23;
            this.groupBox68.TabStop = false;
            this.groupBox68.Text = "Support Reactions from Live Load";
            // 
            // txt_live_kN_m
            // 
            this.txt_live_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_kN_m.Location = new System.Drawing.Point(108, 181);
            this.txt_live_kN_m.Name = "txt_live_kN_m";
            this.txt_live_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_live_kN_m.TabIndex = 28;
            this.txt_live_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label388
            // 
            this.label388.AutoSize = true;
            this.label388.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label388.Location = new System.Drawing.Point(221, 184);
            this.label388.Name = "label388";
            this.label388.Size = new System.Drawing.Size(55, 13);
            this.label388.TabIndex = 27;
            this.label388.Text = "KIP/FT";
            // 
            // label400
            // 
            this.label400.AutoSize = true;
            this.label400.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label400.Location = new System.Drawing.Point(43, 158);
            this.label400.Name = "label400";
            this.label400.Size = new System.Drawing.Size(55, 13);
            this.label400.TabIndex = 24;
            this.label400.Text = "Total ";
            // 
            // dgv_right_end_design_forces
            // 
            this.dgv_right_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgv_right_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgv_right_end_design_forces.Location = new System.Drawing.Point(7, 19);
            this.dgv_right_end_design_forces.Name = "dgv_right_end_design_forces";
            this.dgv_right_end_design_forces.ReadOnly = true;
            this.dgv_right_end_design_forces.Size = new System.Drawing.Size(255, 130);
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
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn4.HeaderText = "Vertical Reaction (KIP)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 118;
            // 
            // txt_live_vert_rec_Ton
            // 
            this.txt_live_vert_rec_Ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_Ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_Ton.Location = new System.Drawing.Point(108, 155);
            this.txt_live_vert_rec_Ton.Name = "txt_live_vert_rec_Ton";
            this.txt_live_vert_rec_Ton.Size = new System.Drawing.Size(113, 20);
            this.txt_live_vert_rec_Ton.TabIndex = 22;
            this.txt_live_vert_rec_Ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_vert_rec_Ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_Ton_TextChanged);
            // 
            // label401
            // 
            this.label401.AutoSize = true;
            this.label401.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label401.Location = new System.Drawing.Point(221, 158);
            this.label401.Name = "label401";
            this.label401.Size = new System.Drawing.Size(31, 13);
            this.label401.TabIndex = 14;
            this.label401.Text = "KIP";
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
            this.g.Location = new System.Drawing.Point(470, 246);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(447, 172);
            this.g.TabIndex = 25;
            this.g.TabStop = false;
            this.g.Text = "Right End Design Forces";
            // 
            // dgv_right_des_frc
            // 
            this.dgv_right_des_frc.AllowUserToResizeColumns = false;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgv_right_des_frc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_des_frc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgv_right_des_frc.Location = new System.Drawing.Point(6, 19);
            this.dgv_right_des_frc.Name = "dgv_right_des_frc";
            this.dgv_right_des_frc.ReadOnly = true;
            this.dgv_right_des_frc.Size = new System.Drawing.Size(427, 114);
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
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn6.HeaderText = "Vertical Reaction (KIP)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 118;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn7.HeaderText = "Maximum    Mx   (KIP-FT)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 108;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn8.HeaderText = "Maximum   Mz  (KIP-FT)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 108;
            // 
            // txt_right_total_Mz
            // 
            this.txt_right_total_Mz.Location = new System.Drawing.Point(354, 139);
            this.txt_right_total_Mz.Name = "txt_right_total_Mz";
            this.txt_right_total_Mz.ReadOnly = true;
            this.txt_right_total_Mz.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_Mz.TabIndex = 6;
            this.txt_right_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_right_total_Mx
            // 
            this.txt_right_total_Mx.Location = new System.Drawing.Point(250, 139);
            this.txt_right_total_Mx.Name = "txt_right_total_Mx";
            this.txt_right_total_Mx.ReadOnly = true;
            this.txt_right_total_Mx.Size = new System.Drawing.Size(87, 20);
            this.txt_right_total_Mx.TabIndex = 5;
            this.txt_right_total_Mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label402
            // 
            this.label402.AutoSize = true;
            this.label402.Location = new System.Drawing.Point(94, 142);
            this.label402.Name = "label402";
            this.label402.Size = new System.Drawing.Size(47, 13);
            this.label402.TabIndex = 4;
            this.label402.Text = "Total";
            // 
            // label442
            // 
            this.label442.AutoSize = true;
            this.label442.Location = new System.Drawing.Point(6, 142);
            this.label442.Name = "label442";
            this.label442.Size = new System.Drawing.Size(79, 13);
            this.label442.TabIndex = 3;
            this.label442.Text = "Right End";
            // 
            // txt_right_total_vert_reac
            // 
            this.txt_right_total_vert_reac.Location = new System.Drawing.Point(147, 139);
            this.txt_right_total_vert_reac.Name = "txt_right_total_vert_reac";
            this.txt_right_total_vert_reac.ReadOnly = true;
            this.txt_right_total_vert_reac.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_vert_reac.TabIndex = 2;
            this.txt_right_total_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tab_worksheet_design
            // 
            this.tab_worksheet_design.Controls.Add(this.pictureBox7);
            this.tab_worksheet_design.Controls.Add(this.panel1);
            this.tab_worksheet_design.Controls.Add(this.btn_steel_section_ws_open);
            this.tab_worksheet_design.Controls.Add(this.btn_process_steel_section);
            this.tab_worksheet_design.Controls.Add(this.btn_steel_section_open);
            this.tab_worksheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_worksheet_design.Name = "tab_worksheet_design";
            this.tab_worksheet_design.Size = new System.Drawing.Size(950, 666);
            this.tab_worksheet_design.TabIndex = 1;
            this.tab_worksheet_design.Text = "PSC Box Girder";
            this.tab_worksheet_design.UseVisualStyleBackColor = true;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox7.Location = new System.Drawing.Point(27, 483);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(876, 69);
            this.pictureBox7.TabIndex = 129;
            this.pictureBox7.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_steel_girder_input_data);
            this.panel1.Location = new System.Drawing.Point(153, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 462);
            this.panel1.TabIndex = 7;
            // 
            // dgv_steel_girder_input_data
            // 
            this.dgv_steel_girder_input_data.AllowUserToAddRows = false;
            this.dgv_steel_girder_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_steel_girder_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_steel_girder_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15});
            this.dgv_steel_girder_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_steel_girder_input_data.Name = "dgv_steel_girder_input_data";
            this.dgv_steel_girder_input_data.RowHeadersWidth = 27;
            this.dgv_steel_girder_input_data.Size = new System.Drawing.Size(612, 443);
            this.dgv_steel_girder_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Description";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 320;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn13.HeaderText = "Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 80;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn14.HeaderText = "Data";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridViewTextBoxColumn15.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // btn_steel_section_ws_open
            // 
            this.btn_steel_section_ws_open.Location = new System.Drawing.Point(195, 627);
            this.btn_steel_section_ws_open.Name = "btn_steel_section_ws_open";
            this.btn_steel_section_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_steel_section_ws_open.TabIndex = 8;
            this.btn_steel_section_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_steel_section_ws_open.UseVisualStyleBackColor = true;
            this.btn_steel_section_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_steel_section
            // 
            this.btn_process_steel_section.Location = new System.Drawing.Point(195, 558);
            this.btn_process_steel_section.Name = "btn_process_steel_section";
            this.btn_process_steel_section.Size = new System.Drawing.Size(559, 29);
            this.btn_process_steel_section.TabIndex = 7;
            this.btn_process_steel_section.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_steel_section.UseVisualStyleBackColor = true;
            this.btn_process_steel_section.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_steel_section_open
            // 
            this.btn_steel_section_open.Location = new System.Drawing.Point(195, 593);
            this.btn_steel_section_open.Name = "btn_steel_section_open";
            this.btn_steel_section_open.Size = new System.Drawing.Size(559, 29);
            this.btn_steel_section_open.TabIndex = 9;
            this.btn_steel_section_open.Text = "Open Design Report";
            this.btn_steel_section_open.UseVisualStyleBackColor = true;
            this.btn_steel_section_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tab_rcc_abutment
            // 
            this.tab_rcc_abutment.Controls.Add(this.pictureBox1);
            this.tab_rcc_abutment.Controls.Add(this.panel6);
            this.tab_rcc_abutment.Controls.Add(this.btn_abutment_ws_open);
            this.tab_rcc_abutment.Controls.Add(this.btn_process_abutment);
            this.tab_rcc_abutment.Controls.Add(this.btn_abutment_open);
            this.tab_rcc_abutment.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_abutment.Name = "tab_rcc_abutment";
            this.tab_rcc_abutment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_abutment.Size = new System.Drawing.Size(950, 666);
            this.tab_rcc_abutment.TabIndex = 5;
            this.tab_rcc_abutment.Text = "Abutment";
            this.tab_rcc_abutment.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(22, 487);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(876, 69);
            this.pictureBox1.TabIndex = 129;
            this.pictureBox1.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dgv_abutment_input_data);
            this.panel6.Location = new System.Drawing.Point(153, 15);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(644, 466);
            this.panel6.TabIndex = 10;
            // 
            // dgv_abutment_input_data
            // 
            this.dgv_abutment_input_data.AllowUserToAddRows = false;
            this.dgv_abutment_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_abutment_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_abutment_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27});
            this.dgv_abutment_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_abutment_input_data.Name = "dgv_abutment_input_data";
            this.dgv_abutment_input_data.RowHeadersWidth = 27;
            this.dgv_abutment_input_data.Size = new System.Drawing.Size(612, 447);
            this.dgv_abutment_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "Description";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.Width = 320;
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridViewTextBoxColumn25.HeaderText = "Name";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 80;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewTextBoxColumn26.HeaderText = "Data";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 70;
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewTextBoxColumn27.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn27.Width = 80;
            // 
            // btn_abutment_ws_open
            // 
            this.btn_abutment_ws_open.Location = new System.Drawing.Point(195, 631);
            this.btn_abutment_ws_open.Name = "btn_abutment_ws_open";
            this.btn_abutment_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_abutment_ws_open.TabIndex = 8;
            this.btn_abutment_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_abutment_ws_open.UseVisualStyleBackColor = true;
            this.btn_abutment_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_abutment
            // 
            this.btn_process_abutment.Location = new System.Drawing.Point(195, 562);
            this.btn_process_abutment.Name = "btn_process_abutment";
            this.btn_process_abutment.Size = new System.Drawing.Size(559, 29);
            this.btn_process_abutment.TabIndex = 7;
            this.btn_process_abutment.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_abutment.UseVisualStyleBackColor = true;
            this.btn_process_abutment.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_abutment_open
            // 
            this.btn_abutment_open.Location = new System.Drawing.Point(195, 597);
            this.btn_abutment_open.Name = "btn_abutment_open";
            this.btn_abutment_open.Size = new System.Drawing.Size(559, 29);
            this.btn_abutment_open.TabIndex = 9;
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
            this.tab_pier.Size = new System.Drawing.Size(950, 666);
            this.tab_pier.TabIndex = 4;
            this.tab_pier.Text = "Pier";
            this.tab_pier.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox5.Location = new System.Drawing.Point(27, 488);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(876, 69);
            this.pictureBox5.TabIndex = 129;
            this.pictureBox5.TabStop = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.dgv_pier_input_data);
            this.panel11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel11.Location = new System.Drawing.Point(153, 15);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(644, 467);
            this.panel11.TabIndex = 121;
            // 
            // dgv_pier_input_data
            // 
            this.dgv_pier_input_data.AllowUserToAddRows = false;
            this.dgv_pier_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_pier_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pier_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40});
            this.dgv_pier_input_data.Location = new System.Drawing.Point(19, 16);
            this.dgv_pier_input_data.Name = "dgv_pier_input_data";
            this.dgv_pier_input_data.RowHeadersWidth = 27;
            this.dgv_pier_input_data.Size = new System.Drawing.Size(612, 448);
            this.dgv_pier_input_data.TabIndex = 3;
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
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn38.DefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewTextBoxColumn38.HeaderText = "Name";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.Width = 80;
            // 
            // dataGridViewTextBoxColumn39
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn39.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn39.HeaderText = "Data";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn39.Width = 70;
            // 
            // dataGridViewTextBoxColumn40
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn40.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn40.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn40.Width = 80;
            // 
            // btn_pier_ws_open
            // 
            this.btn_pier_ws_open.Location = new System.Drawing.Point(198, 632);
            this.btn_pier_ws_open.Name = "btn_pier_ws_open";
            this.btn_pier_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_pier_ws_open.TabIndex = 8;
            this.btn_pier_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_pier_ws_open.UseVisualStyleBackColor = true;
            this.btn_pier_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_pier
            // 
            this.btn_process_pier.Location = new System.Drawing.Point(198, 563);
            this.btn_process_pier.Name = "btn_process_pier";
            this.btn_process_pier.Size = new System.Drawing.Size(559, 29);
            this.btn_process_pier.TabIndex = 7;
            this.btn_process_pier.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_pier.UseVisualStyleBackColor = true;
            this.btn_process_pier.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_pier_open
            // 
            this.btn_pier_open.Location = new System.Drawing.Point(198, 598);
            this.btn_pier_open.Name = "btn_pier_open";
            this.btn_pier_open.Size = new System.Drawing.Size(559, 29);
            this.btn_pier_open.TabIndex = 9;
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
            this.tabPage1.Size = new System.Drawing.Size(950, 666);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "Bearing";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = global::LimitStateMethod.Properties.Resources.Excel_note;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox6.Location = new System.Drawing.Point(23, 488);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(876, 69);
            this.pictureBox6.TabIndex = 129;
            this.pictureBox6.TabStop = false;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.dgv_bearing_input_data);
            this.panel13.Location = new System.Drawing.Point(153, 15);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(644, 467);
            this.panel13.TabIndex = 10;
            // 
            // dgv_bearing_input_data
            // 
            this.dgv_bearing_input_data.AllowUserToAddRows = false;
            this.dgv_bearing_input_data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_bearing_input_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bearing_input_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn51});
            this.dgv_bearing_input_data.Location = new System.Drawing.Point(18, 16);
            this.dgv_bearing_input_data.Name = "dgv_bearing_input_data";
            this.dgv_bearing_input_data.RowHeadersWidth = 27;
            this.dgv_bearing_input_data.Size = new System.Drawing.Size(612, 448);
            this.dgv_bearing_input_data.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.HeaderText = "Description";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn48.Width = 320;
            // 
            // dataGridViewTextBoxColumn49
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn49.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn49.HeaderText = "Name";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.Width = 80;
            // 
            // dataGridViewTextBoxColumn50
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn50.DefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridViewTextBoxColumn50.HeaderText = "Data";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn50.Width = 70;
            // 
            // dataGridViewTextBoxColumn51
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn51.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn51.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn51.Width = 80;
            // 
            // btn_bearing_ws_open
            // 
            this.btn_bearing_ws_open.Location = new System.Drawing.Point(184, 632);
            this.btn_bearing_ws_open.Name = "btn_bearing_ws_open";
            this.btn_bearing_ws_open.Size = new System.Drawing.Size(559, 29);
            this.btn_bearing_ws_open.TabIndex = 8;
            this.btn_bearing_ws_open.Text = "Open User\'s Design by selecting User\'s given file name";
            this.btn_bearing_ws_open.UseVisualStyleBackColor = true;
            this.btn_bearing_ws_open.Click += new System.EventHandler(this.btn_steel_section_ws_open_Click);
            // 
            // btn_process_bearing
            // 
            this.btn_process_bearing.Location = new System.Drawing.Point(184, 563);
            this.btn_process_bearing.Name = "btn_process_bearing";
            this.btn_process_bearing.Size = new System.Drawing.Size(559, 29);
            this.btn_process_bearing.TabIndex = 7;
            this.btn_process_bearing.Text = "Process for New Design and Save as User\'s Design with User\'s given file name";
            this.btn_process_bearing.UseVisualStyleBackColor = true;
            this.btn_process_bearing.Click += new System.EventHandler(this.btn_process_steel_section_Click);
            // 
            // btn_bearing_open
            // 
            this.btn_bearing_open.Location = new System.Drawing.Point(184, 598);
            this.btn_bearing_open.Name = "btn_bearing_open";
            this.btn_bearing_open.Size = new System.Drawing.Size(559, 29);
            this.btn_bearing_open.TabIndex = 9;
            this.btn_bearing_open.Text = "Open Design Report";
            this.btn_bearing_open.UseVisualStyleBackColor = true;
            this.btn_bearing_open.Click += new System.EventHandler(this.btn_bearing_open_Click);
            // 
            // tab_drawings
            // 
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Pier);
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Cantilever);
            this.tab_drawings.Controls.Add(this.btn_dwg_open_Counterfort);
            this.tab_drawings.Controls.Add(this.label157);
            this.tab_drawings.Controls.Add(this.btn_dwg_pier);
            this.tab_drawings.Controls.Add(this.btn_open_drawings);
            this.tab_drawings.Location = new System.Drawing.Point(4, 22);
            this.tab_drawings.Name = "tab_drawings";
            this.tab_drawings.Size = new System.Drawing.Size(950, 666);
            this.tab_drawings.TabIndex = 2;
            this.tab_drawings.Text = "Design Drawings";
            this.tab_drawings.UseVisualStyleBackColor = true;
            // 
            // btn_dwg_open_Pier
            // 
            this.btn_dwg_open_Pier.Location = new System.Drawing.Point(316, 376);
            this.btn_dwg_open_Pier.Name = "btn_dwg_open_Pier";
            this.btn_dwg_open_Pier.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Pier.TabIndex = 80;
            this.btn_dwg_open_Pier.Text = "Open Pier Drawings";
            this.btn_dwg_open_Pier.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Pier.Click += new System.EventHandler(this.btn_open_drawings_Click_1);
            // 
            // btn_dwg_open_Cantilever
            // 
            this.btn_dwg_open_Cantilever.Location = new System.Drawing.Point(316, 299);
            this.btn_dwg_open_Cantilever.Name = "btn_dwg_open_Cantilever";
            this.btn_dwg_open_Cantilever.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Cantilever.TabIndex = 81;
            this.btn_dwg_open_Cantilever.Text = "Open Cantilever Abutment Drawings";
            this.btn_dwg_open_Cantilever.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Cantilever.Click += new System.EventHandler(this.btn_open_drawings_Click_1);
            // 
            // btn_dwg_open_Counterfort
            // 
            this.btn_dwg_open_Counterfort.Location = new System.Drawing.Point(316, 230);
            this.btn_dwg_open_Counterfort.Name = "btn_dwg_open_Counterfort";
            this.btn_dwg_open_Counterfort.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Counterfort.TabIndex = 82;
            this.btn_dwg_open_Counterfort.Text = "Open Counterfort Abutment Drawings";
            this.btn_dwg_open_Counterfort.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Counterfort.Click += new System.EventHandler(this.btn_open_drawings_Click_1);
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
            // btn_open_drawings
            // 
            this.btn_open_drawings.Location = new System.Drawing.Point(316, 159);
            this.btn_open_drawings.Name = "btn_open_drawings";
            this.btn_open_drawings.Size = new System.Drawing.Size(319, 49);
            this.btn_open_drawings.TabIndex = 1;
            this.btn_open_drawings.Text = "Open Prestressed Box Girder Drawings";
            this.btn_open_drawings.UseVisualStyleBackColor = true;
            this.btn_open_drawings.Click += new System.EventHandler(this.btn_open_drawings_Click_1);
            // 
            // frm_PSC_Box_Girder_AASHTO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 692);
            this.Controls.Add(this.tc_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_PSC_Box_Girder_AASHTO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_PSC_Box";
            this.Load += new System.EventHandler(this.frm_PSC_Box_Load);
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
            this.tab_user_input.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.grb_Ana_DL_select_analysis.ResumeLayout(false);
            this.grb_Ana_DL_select_analysis.PerformLayout();
            this.grb_SIDL.ResumeLayout(false);
            this.grb_SIDL.PerformLayout();
            this.grb_create_input_data.ResumeLayout(false);
            this.grb_create_input_data.PerformLayout();
            this.tab_cs_diagram1.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tab_cs_diagram2.ResumeLayout(false);
            this.tab_cs_diagram2.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tab_moving_data.ResumeLayout(false);
            this.tab_moving_data.PerformLayout();
            this.groupBox117.ResumeLayout(false);
            this.groupBox117.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).EndInit();
            this.groupBox118.ResumeLayout(false);
            this.groupBox118.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).EndInit();
            this.tab_analysis.ResumeLayout(false);
            this.tc_process.ResumeLayout(false);
            this.tab_analysis_data.ResumeLayout(false);
            this.groupBox136.ResumeLayout(false);
            this.groupBox136.PerformLayout();
            this.groupBox70.ResumeLayout(false);
            this.groupBox70.PerformLayout();
            this.groupBox71.ResumeLayout(false);
            this.groupBox71.PerformLayout();
            this.tab_process.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox109.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabControl5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox44.ResumeLayout(false);
            this.groupBox44.PerformLayout();
            this.groupBox11.ResumeLayout(false);
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
            this.groupBox58.ResumeLayout(false);
            this.groupBox58.PerformLayout();
            this.groupBox59.ResumeLayout(false);
            this.groupBox59.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox62.ResumeLayout(false);
            this.groupBox62.PerformLayout();
            this.groupBox65.ResumeLayout(false);
            this.groupBox65.PerformLayout();
            this.groupBox66.ResumeLayout(false);
            this.groupBox66.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_des_frc)).EndInit();
            this.groupBox67.ResumeLayout(false);
            this.groupBox67.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).EndInit();
            this.groupBox68.ResumeLayout(false);
            this.groupBox68.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).EndInit();
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_des_frc)).EndInit();
            this.tab_worksheet_design.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_steel_girder_input_data)).EndInit();
            this.tab_rcc_abutment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel6.ResumeLayout(false);
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
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox grb_Ana_DL_select_analysis;
        private System.Windows.Forms.TextBox txt_Ana_analysis_file;
        private System.Windows.Forms.Button btn_Ana_DL_browse_input_file;
        private System.Windows.Forms.RadioButton rbtn_Ana_DL_create_analysis_file;
        private System.Windows.Forms.RadioButton rbtn_Ana_DL_select_analysis_file;
        private System.Windows.Forms.TextBox txt_gd_np;
        private System.Windows.Forms.GroupBox grb_create_input_data;
        private System.Windows.Forms.TextBox txt_Ana_Web_Thickness;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_Ana_Web_Spacing;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Ana_Superstructure_depth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Ana_Road_Width;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ana_Span;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Process_LL_Analysis;
        private System.Windows.Forms.Button btn_Ana_DL_create_data;
        private System.Windows.Forms.GroupBox grb_SIDL;
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
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_Ana_Bridge_Width;
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
        private System.Windows.Forms.GroupBox groupBox58;
        private System.Windows.Forms.Label label534;
        private System.Windows.Forms.TextBox txt_ana_TSRP;
        private System.Windows.Forms.Label label535;
        private System.Windows.Forms.Label label536;
        private System.Windows.Forms.TextBox txt_ana_MSTD;
        private System.Windows.Forms.Label label537;
        private System.Windows.Forms.Label label538;
        private System.Windows.Forms.TextBox txt_ana_MSLD;
        private System.Windows.Forms.Label label539;
        private System.Windows.Forms.GroupBox groupBox59;
        private System.Windows.Forms.Label label541;
        private System.Windows.Forms.Label label540;
        private System.Windows.Forms.TextBox txt_ana_DLSR;
        private System.Windows.Forms.Label label532;
        private System.Windows.Forms.Label label533;
        private System.Windows.Forms.TextBox txt_ana_LLSR;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox62;
        private System.Windows.Forms.Label label276;
        private System.Windows.Forms.TextBox txt_final_Mz;
        private System.Windows.Forms.TextBox txt_final_Mx;
        private System.Windows.Forms.Label label278;
        private System.Windows.Forms.GroupBox groupBox65;
        private System.Windows.Forms.Label label280;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox txt_final_vert_reac;
        private System.Windows.Forms.GroupBox groupBox66;
        private System.Windows.Forms.TextBox txt_left_total_Mz;
        private System.Windows.Forms.TextBox txt_left_total_Mx;
        private System.Windows.Forms.Label label325;
        private System.Windows.Forms.Label label326;
        private System.Windows.Forms.TextBox txt_left_total_vert_reac;
        private System.Windows.Forms.DataGridView dgv_left_des_frc;
        private System.Windows.Forms.GroupBox groupBox67;
        private System.Windows.Forms.Label label327;
        private System.Windows.Forms.TextBox txt_dead_kN_m;
        private System.Windows.Forms.Label label370;
        private System.Windows.Forms.Label label371;
        private System.Windows.Forms.DataGridView dgv_left_end_design_forces;
        private System.Windows.Forms.TextBox txt_dead_vert_reac_ton;
        private System.Windows.Forms.GroupBox groupBox68;
        public System.Windows.Forms.TextBox txt_live_kN_m;
        private System.Windows.Forms.Label label388;
        private System.Windows.Forms.Label label400;
        private System.Windows.Forms.DataGridView dgv_right_end_design_forces;
        public System.Windows.Forms.TextBox txt_live_vert_rec_Ton;
        private System.Windows.Forms.Label label401;
        private System.Windows.Forms.GroupBox g;
        private System.Windows.Forms.DataGridView dgv_right_des_frc;
        private System.Windows.Forms.TextBox txt_right_total_Mz;
        private System.Windows.Forms.TextBox txt_right_total_Mx;
        private System.Windows.Forms.Label label402;
        private System.Windows.Forms.Label label442;
        private System.Windows.Forms.TextBox txt_right_total_vert_reac;
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
        private System.Windows.Forms.Label lbl_factor;
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
        private System.Windows.Forms.TabPage tab_moving_data;
        private System.Windows.Forms.GroupBox groupBox109;
        private System.Windows.Forms.ComboBox cmb_long_open_file_process;
        private System.Windows.Forms.Button btn_View_Result_summary;
        private System.Windows.Forms.Button btn_view_report;
        private System.Windows.Forms.Button btn_view_data;
        private System.Windows.Forms.Button btn_view_preprocess;
        private System.Windows.Forms.Button btn_open_diagram;
        private System.Windows.Forms.TabPage tab_rcc_abutment;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_psc_new_design;
        private System.Windows.Forms.Button btn_psc_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label283;
        private System.Windows.Forms.Button btn_dwg_open_Pier;
        private System.Windows.Forms.Button btn_dwg_open_Cantilever;
        private System.Windows.Forms.Button btn_dwg_open_Counterfort;
        private System.Windows.Forms.TabPage tab_cs_diagram1;
        private System.Windows.Forms.PictureBox pictureBox3;
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
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txt_box_cs2_b8;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txt_box_cs2_b7;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txt_box_cs2_b6;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txt_box_cs2_d5;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txt_box_cs2_b5;
        private System.Windows.Forms.TextBox txt_box_cs2_d4;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txt_box_cs2_b4;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txt_box_cs2_d3;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txt_box_cs2_b3;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txt_box_cs2_d2;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_box_cs2_b2;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_box_cs2_d1;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.TextBox txt_box_cs2_cell_nos;
        private System.Windows.Forms.TextBox txt_box_cs2_b1;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txt_Ana_Steel_Es;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txt_Ana_Steel_Fy;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Top_Cover;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Bottom_Cover;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Wearing_Surface;
        private System.Windows.Forms.TextBox txt_Ana_Deckslab_Thickness;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txt_Ana_Deck_Overhang;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txt_Ana_Bottom_Slab_Thickness;
        private System.Windows.Forms.TextBox txt_Ana_Top_Slab_Thickness;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label80;
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
        private System.Windows.Forms.RadioButton rbtn_multiple_cell;
        private System.Windows.Forms.RadioButton rbtn_single_cell;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.TextBox txt_Spans;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label1176;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Button btn__Loadings_help;
        private System.Windows.Forms.Button btn_edit_load_combs_IRC;
        private System.Windows.Forms.GroupBox groupBox117;
        private System.Windows.Forms.Label label1173;
        private System.Windows.Forms.Label label1129;
        private System.Windows.Forms.DataGridView dgv_long_loads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column43;
        private System.Windows.Forms.Label label1130;
        private System.Windows.Forms.Button btn_long_restore_ll_IRC;
        private System.Windows.Forms.GroupBox groupBox118;
        private System.Windows.Forms.Label label1170;
        private System.Windows.Forms.Label label1132;
        private System.Windows.Forms.DataGridView dgv_long_liveloads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn91;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn92;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn93;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn94;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn95;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn96;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn97;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn98;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn99;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn100;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn101;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn102;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn103;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.TextBox txt_LL_load_gen;
        private System.Windows.Forms.TextBox txt_dl_ll_comb_IRC;
        private System.Windows.Forms.TextBox txt_XINCR;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Max_Mx;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Max_Mz;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Joints;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Vert_Reaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.TextBox txt_ll_factor;
        private System.Windows.Forms.TextBox txt_dl_factor;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_steel_girder_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.Button btn_steel_section_ws_open;
        private System.Windows.Forms.Button btn_process_steel_section;
        private System.Windows.Forms.Button btn_steel_section_open;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgv_abutment_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.Button btn_abutment_ws_open;
        private System.Windows.Forms.Button btn_process_abutment;
        private System.Windows.Forms.Button btn_abutment_open;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView dgv_pier_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.Button btn_pier_ws_open;
        private System.Windows.Forms.Button btn_process_pier;
        private System.Windows.Forms.Button btn_pier_open;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.DataGridView dgv_bearing_input_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.Button btn_bearing_ws_open;
        private System.Windows.Forms.Button btn_process_bearing;
        private System.Windows.Forms.Button btn_bearing_open;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.TextBox txt_Ana_FPLL;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.TextBox txt_Ana_SIDL;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox txt_Ana_SelfWeight;
        private System.Windows.Forms.TabControl tc_process;
        private System.Windows.Forms.TabPage tab_analysis_data;
        private System.Windows.Forms.TabPage tab_process;
        private System.Windows.Forms.GroupBox groupBox136;
        private System.Windows.Forms.Label label1174;
        private System.Windows.Forms.ComboBox cmb_long_open_file_analysis;
        private System.Windows.Forms.Button btn_view_data_1;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button btn_view_postprocess;
        private System.Windows.Forms.Button btn_bs_view_moving_load;
        private System.Windows.Forms.Label label1190;
        private System.Windows.Forms.ComboBox cmb_bs_view_moving_load;
        private System.Windows.Forms.TextBox txt_bs_vehicle_gap;
        private System.Windows.Forms.Label label1191;
    }

}