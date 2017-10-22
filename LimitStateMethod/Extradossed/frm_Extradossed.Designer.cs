namespace LimitStateMethod.Extradossed
{
    partial class frm_Extradossed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Extradossed));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.txt_Ana_LL_member_load = new System.Windows.Forms.TextBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.grb_create_input_data = new System.Windows.Forms.GroupBox();
            this.txt_Ana_width_cantilever = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_eff_depth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label285 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_Ana_B = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label286 = new System.Windows.Forms.Label();
            this.txt_support_distance = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_L3 = new System.Windows.Forms.TextBox();
            this.label288 = new System.Windows.Forms.Label();
            this.txt_L2 = new System.Windows.Forms.TextBox();
            this.label289 = new System.Windows.Forms.Label();
            this.label290 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_Ana_LL_factor = new System.Windows.Forms.TextBox();
            this.label240 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_factor = new System.Windows.Forms.TextBox();
            this.txt_Ana_skew_angle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ana_L1 = new System.Windows.Forms.TextBox();
            this.pcb_cables = new System.Windows.Forms.PictureBox();
            this.tab_cs_diagram = new System.Windows.Forms.TabPage();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage16 = new System.Windows.Forms.TabPage();
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
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btn_Show_Section_Resulf = new System.Windows.Forms.Button();
            this.rtb_sections = new System.Windows.Forms.RichTextBox();
            this.label176 = new System.Windows.Forms.Label();
            this.label226 = new System.Windows.Forms.Label();
            this.tab_moving_data_british = new System.Windows.Forms.TabPage();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.spc_HB = new System.Windows.Forms.SplitContainer();
            this.groupBox105 = new System.Windows.Forms.GroupBox();
            this.dgv_long_british_loads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn61 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn62 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn65 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn66 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn67 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn68 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn69 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn70 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn71 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn72 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn73 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn74 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn76 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label247 = new System.Windows.Forms.Label();
            this.groupBox106 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.label267 = new System.Windows.Forms.Label();
            this.label282 = new System.Windows.Forms.Label();
            this.dgv_british_loads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn77 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn78 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn79 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn81 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn82 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label248 = new System.Windows.Forms.Label();
            this.lbl_HB = new System.Windows.Forms.Label();
            this.groupBox107 = new System.Windows.Forms.GroupBox();
            this.groupBox108 = new System.Windows.Forms.GroupBox();
            this.chk_HA = new System.Windows.Forms.CheckBox();
            this.rbtn_Rail_Load = new System.Windows.Forms.RadioButton();
            this.rbtn_HA_HB = new System.Windows.Forms.RadioButton();
            this.rbtn_HA = new System.Windows.Forms.RadioButton();
            this.rbtn_HB = new System.Windows.Forms.RadioButton();
            this.txt_LL_lf = new System.Windows.Forms.TextBox();
            this.txt_LL_impf = new System.Windows.Forms.TextBox();
            this.label249 = new System.Windows.Forms.Label();
            this.txt_no_lanes = new System.Windows.Forms.TextBox();
            this.txt_ll_british_lgen = new System.Windows.Forms.TextBox();
            this.label252 = new System.Windows.Forms.Label();
            this.grb_ha = new System.Windows.Forms.GroupBox();
            this.grb_ha_aply = new System.Windows.Forms.GroupBox();
            this.chk_HA_7L = new System.Windows.Forms.CheckBox();
            this.chk_HA_6L = new System.Windows.Forms.CheckBox();
            this.chk_HA_10L = new System.Windows.Forms.CheckBox();
            this.chk_HA_5L = new System.Windows.Forms.CheckBox();
            this.chk_HA_9L = new System.Windows.Forms.CheckBox();
            this.chk_HA_4L = new System.Windows.Forms.CheckBox();
            this.chk_HA_8L = new System.Windows.Forms.CheckBox();
            this.chk_HA_3L = new System.Windows.Forms.CheckBox();
            this.chk_HA_2L = new System.Windows.Forms.CheckBox();
            this.chk_HA_1L = new System.Windows.Forms.CheckBox();
            this.txt_HA_CON = new System.Windows.Forms.TextBox();
            this.label255 = new System.Windows.Forms.Label();
            this.txt_HA_UDL = new System.Windows.Forms.TextBox();
            this.label256 = new System.Windows.Forms.Label();
            this.label257 = new System.Windows.Forms.Label();
            this.label260 = new System.Windows.Forms.Label();
            this.grb_hb = new System.Windows.Forms.GroupBox();
            this.grb_hb_aply = new System.Windows.Forms.GroupBox();
            this.chk_HB_7L = new System.Windows.Forms.CheckBox();
            this.chk_HB_6L = new System.Windows.Forms.CheckBox();
            this.chk_HB_10L = new System.Windows.Forms.CheckBox();
            this.chk_HB_5L = new System.Windows.Forms.CheckBox();
            this.chk_HB_9L = new System.Windows.Forms.CheckBox();
            this.chk_HB_4L = new System.Windows.Forms.CheckBox();
            this.chk_HB_8L = new System.Windows.Forms.CheckBox();
            this.chk_HB_3L = new System.Windows.Forms.CheckBox();
            this.chk_HB_2L = new System.Windows.Forms.CheckBox();
            this.chk_HB_1L = new System.Windows.Forms.CheckBox();
            this.label261 = new System.Windows.Forms.Label();
            this.cmb_HB = new System.Windows.Forms.ComboBox();
            this.txt_ll_british_incr = new System.Windows.Forms.TextBox();
            this.txt_lane_width = new System.Windows.Forms.TextBox();
            this.label262 = new System.Windows.Forms.Label();
            this.txt_deck_width = new System.Windows.Forms.TextBox();
            this.label263 = new System.Windows.Forms.Label();
            this.label264 = new System.Windows.Forms.Label();
            this.label265 = new System.Windows.Forms.Label();
            this.label266 = new System.Windows.Forms.Label();
            this.tab_moving_data_indian = new System.Windows.Forms.TabPage();
            this.groupBox79 = new System.Windows.Forms.GroupBox();
            this.label299 = new System.Windows.Forms.Label();
            this.txt_dl_ll_comb = new System.Windows.Forms.TextBox();
            this.btn_edit_load_combs = new System.Windows.Forms.Button();
            this.chk_self_indian = new System.Windows.Forms.CheckBox();
            this.btn_long_restore_ll = new System.Windows.Forms.Button();
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txt_IRC_XINCR = new System.Windows.Forms.TextBox();
            this.tab_moving_data_LRFD = new System.Windows.Forms.TabPage();
            this.groupBox47 = new System.Windows.Forms.GroupBox();
            this.label305 = new System.Windows.Forms.Label();
            this.txt_LRFD_dl_ll_comb = new System.Windows.Forms.TextBox();
            this.btn_LRFD_edit_load_combs = new System.Windows.Forms.Button();
            this.chk_LRFD_self_indian = new System.Windows.Forms.CheckBox();
            this.btn_LRFD_long_restore_ll = new System.Windows.Forms.Button();
            this.groupBox48 = new System.Windows.Forms.GroupBox();
            this.label306 = new System.Windows.Forms.Label();
            this.dgv_LRFD_long_loads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label317 = new System.Windows.Forms.Label();
            this.label318 = new System.Windows.Forms.Label();
            this.groupBox49 = new System.Windows.Forms.GroupBox();
            this.label319 = new System.Windows.Forms.Label();
            this.dgv_LRFD_long_liveloads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn83 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn85 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn86 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn87 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn88 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn92 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label320 = new System.Windows.Forms.Label();
            this.txt_LRFD_LL_load_gen = new System.Windows.Forms.TextBox();
            this.txt_LRFD_XINCR = new System.Windows.Forms.TextBox();
            this.tab_analysis = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.btn_View_Moving_Load = new System.Windows.Forms.Button();
            this.btn_view_report = new System.Windows.Forms.Button();
            this.btn_view_data = new System.Windows.Forms.Button();
            this.btn_view_structure = new System.Windows.Forms.Button();
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
            this.label238 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
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
            this.groupBox60 = new System.Windows.Forms.GroupBox();
            this.label268 = new System.Windows.Forms.Label();
            this.label269 = new System.Windows.Forms.Label();
            this.groupBox61 = new System.Windows.Forms.GroupBox();
            this.label270 = new System.Windows.Forms.Label();
            this.label271 = new System.Windows.Forms.Label();
            this.label272 = new System.Windows.Forms.Label();
            this.label273 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label274 = new System.Windows.Forms.Label();
            this.label275 = new System.Windows.Forms.Label();
            this.txt_Ana_dead_cross_max_shear = new System.Windows.Forms.TextBox();
            this.txt_Ana_dead_cross_max_moment = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox62 = new System.Windows.Forms.GroupBox();
            this.groupBox63 = new System.Windows.Forms.GroupBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label276 = new System.Windows.Forms.Label();
            this.label277 = new System.Windows.Forms.Label();
            this.txt_final_Mz = new System.Windows.Forms.TextBox();
            this.txt_max_Mz_kN = new System.Windows.Forms.TextBox();
            this.groupBox64 = new System.Windows.Forms.GroupBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.txt_final_Mx = new System.Windows.Forms.TextBox();
            this.txt_max_Mx_kN = new System.Windows.Forms.TextBox();
            this.label278 = new System.Windows.Forms.Label();
            this.label279 = new System.Windows.Forms.Label();
            this.groupBox65 = new System.Windows.Forms.GroupBox();
            this.lbl_factor = new System.Windows.Forms.Label();
            this.txt_final_vert_rec_kN = new System.Windows.Forms.TextBox();
            this.label280 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.txt_final_vert_reac = new System.Windows.Forms.TextBox();
            this.label281 = new System.Windows.Forms.Label();
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
            this.label354 = new System.Windows.Forms.Label();
            this.txt_dead_vert_reac_kN = new System.Windows.Forms.TextBox();
            this.label370 = new System.Windows.Forms.Label();
            this.label371 = new System.Windows.Forms.Label();
            this.dgv_left_end_design_forces = new System.Windows.Forms.DataGridView();
            this.col_Joints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Vert_Reaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dead_vert_reac_ton = new System.Windows.Forms.TextBox();
            this.groupBox68 = new System.Windows.Forms.GroupBox();
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
            this.tc_bridge_deck = new System.Windows.Forms.TabControl();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btn_design = new System.Windows.Forms.Button();
            this.btn_design_of_anchorage = new System.Windows.Forms.Button();
            this.btn_cable_frict = new System.Windows.Forms.Button();
            this.btn_Temp_trans = new System.Windows.Forms.Button();
            this.uC_BoxGirder1 = new BridgeAnalysisDesign.PSC_BoxGirder.UC_BoxGirder();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_psc_box = new System.Windows.Forms.Button();
            this.btn_worksheet_open = new System.Windows.Forms.Button();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.uC_CableStayedDesign1 = new LimitStateMethod.Extradossed.UC_CableStayedDesign();
            this.tab_Segment = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label242 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.label230 = new System.Windows.Forms.Label();
            this.cmb_tab1_Fy = new System.Windows.Forms.ComboBox();
            this.cmb_tab1_Fcu = new System.Windows.Forms.ComboBox();
            this.label221 = new System.Windows.Forms.Label();
            this.label222 = new System.Windows.Forms.Label();
            this.label223 = new System.Windows.Forms.Label();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.label208 = new System.Windows.Forms.Label();
            this.label207 = new System.Windows.Forms.Label();
            this.label206 = new System.Windows.Forms.Label();
            this.label205 = new System.Windows.Forms.Label();
            this.label204 = new System.Windows.Forms.Label();
            this.label201 = new System.Windows.Forms.Label();
            this.label203 = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.txt_ttu = new System.Windows.Forms.TextBox();
            this.txt_tv = new System.Windows.Forms.TextBox();
            this.txt_ttv = new System.Windows.Forms.TextBox();
            this.txt_ft_temp28 = new System.Windows.Forms.TextBox();
            this.label200 = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.txt_fc_factor = new System.Windows.Forms.TextBox();
            this.txt_fc_temp28 = new System.Windows.Forms.TextBox();
            this.label199 = new System.Windows.Forms.Label();
            this.label197 = new System.Windows.Forms.Label();
            this.txt_Mod_rup = new System.Windows.Forms.TextBox();
            this.txt_ft_temp14 = new System.Windows.Forms.TextBox();
            this.label198 = new System.Windows.Forms.Label();
            this.txt_fc_serv = new System.Windows.Forms.TextBox();
            this.label202 = new System.Windows.Forms.Label();
            this.txt_fc_temp14 = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label225 = new System.Windows.Forms.Label();
            this.label224 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txt_tab1_Tr1 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.txt_tab1_Tr2 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.txt_tab1_Tr3 = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label97 = new System.Windows.Forms.Label();
            this.txt_tab1_Tf4 = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.txt_tab1_Tf3 = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txt_tab1_Tf2 = new System.Windows.Forms.TextBox();
            this.label134 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.txt_tab1_Tf1 = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.txt_tab1_ds = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txt_tab1_alpha = new System.Windows.Forms.TextBox();
            this.txt_tab1_FactLL = new System.Windows.Forms.TextBox();
            this.txt_tab1_FactSIDL = new System.Windows.Forms.TextBox();
            this.txt_tab1_FactDL = new System.Windows.Forms.TextBox();
            this.txt_tab1_Mct_SIDL = new System.Windows.Forms.TextBox();
            this.txt_tab1_bt = new System.Windows.Forms.TextBox();
            this.txt_tab1_agt_SIDL = new System.Windows.Forms.TextBox();
            this.txt_tab1_df = new System.Windows.Forms.TextBox();
            this.txt_tab1_sctt = new System.Windows.Forms.TextBox();
            this.txt_tab1_Mct = new System.Windows.Forms.TextBox();
            this.txt_tab1_wct = new System.Windows.Forms.TextBox();
            this.txt_tab1_act = new System.Windows.Forms.TextBox();
            this.txt_tab1_T_loss = new System.Windows.Forms.TextBox();
            this.txt_tab1_D = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txt_tab1_DW = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txt_tab1_L = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txt_tab1_exg = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txt_tab1_L2 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_tab1_L1 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txt_tab1_Lo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.label245 = new System.Windows.Forms.Label();
            this.label235 = new System.Windows.Forms.Label();
            this.label237 = new System.Windows.Forms.Label();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.cmb_tab2_nc = new System.Windows.Forms.ComboBox();
            this.label241 = new System.Windows.Forms.Label();
            this.cmb_tab2_strand_data = new System.Windows.Forms.ComboBox();
            this.label243 = new System.Windows.Forms.Label();
            this.label144 = new System.Windows.Forms.Label();
            this.label184 = new System.Windows.Forms.Label();
            this.txt_tab2_cable_area = new System.Windows.Forms.TextBox();
            this.label182 = new System.Windows.Forms.Label();
            this.label180 = new System.Windows.Forms.Label();
            this.txt_tab2_nc_right = new System.Windows.Forms.TextBox();
            this.txt_tab2_nc_left = new System.Windows.Forms.TextBox();
            this.label137 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label165 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.label186 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label77 = new System.Windows.Forms.Label();
            this.txt_tab2_D = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label189 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.txt_tab2_A = new System.Windows.Forms.TextBox();
            this.txt_tab2_cover1 = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.txt_tab2_Ns = new System.Windows.Forms.TextBox();
            this.txt_tab2_cover2 = new System.Windows.Forms.TextBox();
            this.txt_tab2_Crst56 = new System.Windows.Forms.TextBox();
            this.txt_tab2_Resh56 = new System.Windows.Forms.TextBox();
            this.txt_tab2_Ec = new System.Windows.Forms.TextBox();
            this.txt_tab2_Fcu = new System.Windows.Forms.TextBox();
            this.txt_tab2_qd = new System.Windows.Forms.TextBox();
            this.txt_tab2_td1 = new System.Windows.Forms.TextBox();
            this.txt_tab2_Re2 = new System.Windows.Forms.TextBox();
            this.txt_tab2_Re1 = new System.Windows.Forms.TextBox();
            this.txt_tab2_k = new System.Windows.Forms.TextBox();
            this.txt_tab2_mu = new System.Windows.Forms.TextBox();
            this.txt_tab2_s = new System.Windows.Forms.TextBox();
            this.txt_tab2_Pj = new System.Windows.Forms.TextBox();
            this.txt_tab2_Eps = new System.Windows.Forms.TextBox();
            this.txt_tab2_Pn = new System.Windows.Forms.TextBox();
            this.txt_tab2_Fu = new System.Windows.Forms.TextBox();
            this.txt_tab2_Fy = new System.Windows.Forms.TextBox();
            this.txt_tab2_Pu = new System.Windows.Forms.TextBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.txt_tab2_rss_56 = new System.Windows.Forms.TextBox();
            this.txt_tab2_rss_14 = new System.Windows.Forms.TextBox();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.txt_tab2_cwccb_fcj = new System.Windows.Forms.TextBox();
            this.txt_tab2_ccbg_fcj = new System.Windows.Forms.TextBox();
            this.txt_tab2_fsp_fcj = new System.Windows.Forms.TextBox();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.label109 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.txt_tab2_cwccb_day = new System.Windows.Forms.TextBox();
            this.txt_tab2_ccbg_day = new System.Windows.Forms.TextBox();
            this.txt_tab2_fsp_day = new System.Windows.Forms.TextBox();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.label82 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.label217 = new System.Windows.Forms.Label();
            this.label218 = new System.Windows.Forms.Label();
            this.label219 = new System.Windows.Forms.Label();
            this.label220 = new System.Windows.Forms.Label();
            this.txt_zn3_inn = new System.Windows.Forms.TextBox();
            this.txt_zn3_out = new System.Windows.Forms.TextBox();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.label209 = new System.Windows.Forms.Label();
            this.label212 = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.label214 = new System.Windows.Forms.Label();
            this.txt_zn2_inn = new System.Windows.Forms.TextBox();
            this.txt_zn2_out = new System.Windows.Forms.TextBox();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.label216 = new System.Windows.Forms.Label();
            this.label215 = new System.Windows.Forms.Label();
            this.label211 = new System.Windows.Forms.Label();
            this.label210 = new System.Windows.Forms.Label();
            this.txt_zn1_inn = new System.Windows.Forms.TextBox();
            this.txt_zn1_out = new System.Windows.Forms.TextBox();
            this.tabControl6 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_segment_report = new System.Windows.Forms.Button();
            this.btn_segment_process = new System.Windows.Forms.Button();
            this.tab_rcc_abutment = new System.Windows.Forms.TabPage();
            this.tab_pier = new System.Windows.Forms.TabPage();
            this.tc_pier = new System.Windows.Forms.TabControl();
            this.tab_PierPileLSM = new System.Windows.Forms.TabPage();
            this.uC_PierDesignLSM1 = new BridgeAnalysisDesign.Pier.UC_PierDesignLSM();
            this.tab_PierWSM = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tab_des_form1 = new System.Windows.Forms.TabPage();
            this.label246 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.txt_rcc_pier_m = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
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
            this.label355 = new System.Windows.Forms.Label();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.label356 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_W1_supp_reac = new System.Windows.Forms.TextBox();
            this.label357 = new System.Windows.Forms.Label();
            this.label358 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Mz1 = new System.Windows.Forms.TextBox();
            this.label359 = new System.Windows.Forms.Label();
            this.label360 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Mx1 = new System.Windows.Forms.TextBox();
            this.label361 = new System.Windows.Forms.Label();
            this.label362 = new System.Windows.Forms.Label();
            this.label363 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H7 = new System.Windows.Forms.TextBox();
            this.label364 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_gama_c = new System.Windows.Forms.TextBox();
            this.label365 = new System.Windows.Forms.Label();
            this.label366 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_vehi_load = new System.Windows.Forms.TextBox();
            this.label367 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NR = new System.Windows.Forms.TextBox();
            this.label368 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NP = new System.Windows.Forms.TextBox();
            this.label369 = new System.Windows.Forms.Label();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.label315 = new System.Windows.Forms.Label();
            this.label316 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_tdia = new System.Windows.Forms.TextBox();
            this.txt_RCC_Pier_rdia = new System.Windows.Forms.TextBox();
            this.label321 = new System.Windows.Forms.Label();
            this.label507 = new System.Windows.Forms.Label();
            this.label372 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_fck_2 = new System.Windows.Forms.TextBox();
            this.label373 = new System.Windows.Forms.Label();
            this.label374 = new System.Windows.Forms.Label();
            this.label375 = new System.Windows.Forms.Label();
            this.label376 = new System.Windows.Forms.Label();
            this.label377 = new System.Windows.Forms.Label();
            this.label378 = new System.Windows.Forms.Label();
            this.label379 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_fy2 = new System.Windows.Forms.TextBox();
            this.label380 = new System.Windows.Forms.Label();
            this.label381 = new System.Windows.Forms.Label();
            this.label382 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_D = new System.Windows.Forms.TextBox();
            this.label383 = new System.Windows.Forms.Label();
            this.label384 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_b = new System.Windows.Forms.TextBox();
            this.txt_RCC_Pier_d_dash = new System.Windows.Forms.TextBox();
            this.label385 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_p1 = new System.Windows.Forms.TextBox();
            this.label386 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_p2 = new System.Windows.Forms.TextBox();
            this.label387 = new System.Windows.Forms.Label();
            this.label389 = new System.Windows.Forms.Label();
            this.label390 = new System.Windows.Forms.Label();
            this.label391 = new System.Windows.Forms.Label();
            this.label392 = new System.Windows.Forms.Label();
            this.label393 = new System.Windows.Forms.Label();
            this.label394 = new System.Windows.Forms.Label();
            this.label395 = new System.Windows.Forms.Label();
            this.label396 = new System.Windows.Forms.Label();
            this.label397 = new System.Windows.Forms.Label();
            this.label398 = new System.Windows.Forms.Label();
            this.label403 = new System.Windows.Forms.Label();
            this.label404 = new System.Windows.Forms.Label();
            this.label405 = new System.Windows.Forms.Label();
            this.label406 = new System.Windows.Forms.Label();
            this.label407 = new System.Windows.Forms.Label();
            this.label408 = new System.Windows.Forms.Label();
            this.label409 = new System.Windows.Forms.Label();
            this.label410 = new System.Windows.Forms.Label();
            this.label411 = new System.Windows.Forms.Label();
            this.label412 = new System.Windows.Forms.Label();
            this.label413 = new System.Windows.Forms.Label();
            this.label414 = new System.Windows.Forms.Label();
            this.label415 = new System.Windows.Forms.Label();
            this.label416 = new System.Windows.Forms.Label();
            this.label417 = new System.Windows.Forms.Label();
            this.label418 = new System.Windows.Forms.Label();
            this.label419 = new System.Windows.Forms.Label();
            this.label420 = new System.Windows.Forms.Label();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.label421 = new System.Windows.Forms.Label();
            this.label422 = new System.Windows.Forms.Label();
            this.label423 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H2 = new System.Windows.Forms.TextBox();
            this.label424 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B4 = new System.Windows.Forms.TextBox();
            this.label425 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B3 = new System.Windows.Forms.TextBox();
            this.label426 = new System.Windows.Forms.Label();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.label427 = new System.Windows.Forms.Label();
            this.label428 = new System.Windows.Forms.Label();
            this.label429 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H1 = new System.Windows.Forms.TextBox();
            this.label430 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B2 = new System.Windows.Forms.TextBox();
            this.label431 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B1 = new System.Windows.Forms.TextBox();
            this.label432 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_overall_height = new System.Windows.Forms.TextBox();
            this.label433 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier___B = new System.Windows.Forms.TextBox();
            this.label434 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B13 = new System.Windows.Forms.TextBox();
            this.label435 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B12 = new System.Windows.Forms.TextBox();
            this.label436 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B11 = new System.Windows.Forms.TextBox();
            this.label437 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B10 = new System.Windows.Forms.TextBox();
            this.label438 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B9 = new System.Windows.Forms.TextBox();
            this.label439 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H6 = new System.Windows.Forms.TextBox();
            this.label440 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H5 = new System.Windows.Forms.TextBox();
            this.label441 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B8 = new System.Windows.Forms.TextBox();
            this.label445 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H4 = new System.Windows.Forms.TextBox();
            this.label446 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_H3 = new System.Windows.Forms.TextBox();
            this.label447 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B7 = new System.Windows.Forms.TextBox();
            this.label448 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Form_Lev = new System.Windows.Forms.TextBox();
            this.label449 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL5 = new System.Windows.Forms.TextBox();
            this.label450 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL4 = new System.Windows.Forms.TextBox();
            this.label451 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL3 = new System.Windows.Forms.TextBox();
            this.label452 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL2 = new System.Windows.Forms.TextBox();
            this.label453 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_RL1 = new System.Windows.Forms.TextBox();
            this.label454 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B6 = new System.Windows.Forms.TextBox();
            this.label455 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_B5 = new System.Windows.Forms.TextBox();
            this.label456 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_DS = new System.Windows.Forms.TextBox();
            this.label457 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_DMG = new System.Windows.Forms.TextBox();
            this.label458 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_NMG = new System.Windows.Forms.TextBox();
            this.label459 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Hp = new System.Windows.Forms.TextBox();
            this.label460 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_Wp = new System.Windows.Forms.TextBox();
            this.label461 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier__B = new System.Windows.Forms.TextBox();
            this.label462 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_CW = new System.Windows.Forms.TextBox();
            this.label463 = new System.Windows.Forms.Label();
            this.txt_RCC_Pier_L = new System.Windows.Forms.TextBox();
            this.label464 = new System.Windows.Forms.Label();
            this.tab_des_form2 = new System.Windows.Forms.TabPage();
            this.label471 = new System.Windows.Forms.Label();
            this.label472 = new System.Windows.Forms.Label();
            this.label479 = new System.Windows.Forms.Label();
            this.txt_pier_2_vspc = new System.Windows.Forms.TextBox();
            this.label480 = new System.Windows.Forms.Label();
            this.txt_pier_2_vdia = new System.Windows.Forms.TextBox();
            this.label481 = new System.Windows.Forms.Label();
            this.txt_pier_2_hdia = new System.Windows.Forms.TextBox();
            this.label512 = new System.Windows.Forms.Label();
            this.label513 = new System.Windows.Forms.Label();
            this.label516 = new System.Windows.Forms.Label();
            this.txt_pier_2_ldia = new System.Windows.Forms.TextBox();
            this.label517 = new System.Windows.Forms.Label();
            this.txt_pier_2_vlegs = new System.Windows.Forms.TextBox();
            this.label1076 = new System.Windows.Forms.Label();
            this.txt_pier_2_hlegs = new System.Windows.Forms.TextBox();
            this.label1077 = new System.Windows.Forms.Label();
            this.txt_pier_2_slegs = new System.Windows.Forms.TextBox();
            this.label1078 = new System.Windows.Forms.Label();
            this.txt_pier_2_sdia = new System.Windows.Forms.TextBox();
            this.label1079 = new System.Windows.Forms.Label();
            this.label233 = new System.Windows.Forms.Label();
            this.label234 = new System.Windows.Forms.Label();
            this.label465 = new System.Windows.Forms.Label();
            this.label466 = new System.Windows.Forms.Label();
            this.label467 = new System.Windows.Forms.Label();
            this.label468 = new System.Windows.Forms.Label();
            this.label469 = new System.Windows.Forms.Label();
            this.label470 = new System.Windows.Forms.Label();
            this.label473 = new System.Windows.Forms.Label();
            this.label474 = new System.Windows.Forms.Label();
            this.label475 = new System.Windows.Forms.Label();
            this.label476 = new System.Windows.Forms.Label();
            this.label477 = new System.Windows.Forms.Label();
            this.cmb_pier_2_k = new System.Windows.Forms.ComboBox();
            this.txt_pier_2_SBC = new System.Windows.Forms.TextBox();
            this.label478 = new System.Windows.Forms.Label();
            this.txt_pier_2_Itc = new System.Windows.Forms.TextBox();
            this.label482 = new System.Windows.Forms.Label();
            this.txt_pier_2_Vr = new System.Windows.Forms.TextBox();
            this.label483 = new System.Windows.Forms.Label();
            this.txt_pier_2_LL = new System.Windows.Forms.TextBox();
            this.label484 = new System.Windows.Forms.Label();
            this.txt_pier_2_CF = new System.Windows.Forms.TextBox();
            this.label485 = new System.Windows.Forms.Label();
            this.txt_pier_2_k = new System.Windows.Forms.TextBox();
            this.label486 = new System.Windows.Forms.Label();
            this.txt_pier_2_V = new System.Windows.Forms.TextBox();
            this.label487 = new System.Windows.Forms.Label();
            this.txt_pier_2_HHF = new System.Windows.Forms.TextBox();
            this.label488 = new System.Windows.Forms.Label();
            this.txt_pier_2_SC = new System.Windows.Forms.TextBox();
            this.label489 = new System.Windows.Forms.Label();
            this.txt_pier_2_PD = new System.Windows.Forms.TextBox();
            this.label490 = new System.Windows.Forms.Label();
            this.txt_pier_2_PML = new System.Windows.Forms.TextBox();
            this.label491 = new System.Windows.Forms.Label();
            this.txt_pier_2_PL = new System.Windows.Forms.TextBox();
            this.label492 = new System.Windows.Forms.Label();
            this.label493 = new System.Windows.Forms.Label();
            this.txt_pier_2_APD = new System.Windows.Forms.TextBox();
            this.label494 = new System.Windows.Forms.Label();
            this.txt_pier_2_B16 = new System.Windows.Forms.TextBox();
            this.label495 = new System.Windows.Forms.Label();
            this.txt_pier_2_P3 = new System.Windows.Forms.TextBox();
            this.label496 = new System.Windows.Forms.Label();
            this.label497 = new System.Windows.Forms.Label();
            this.txt_pier_2_P2 = new System.Windows.Forms.TextBox();
            this.tab_des_Diagram = new System.Windows.Forms.TabPage();
            this.pic_pier_interactive_diagram = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_RCC_Pier_Report = new System.Windows.Forms.Button();
            this.btn_RCC_Pier_Process = new System.Windows.Forms.Button();
            this.label155 = new System.Windows.Forms.Label();
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
            this.tab_PierOpenLSM = new System.Windows.Forms.TabPage();
            this.tc_abutment = new System.Windows.Forms.TabControl();
            this.tab_AbutmentLSM = new System.Windows.Forms.TabPage();
            this.uC_RCC_Abut1 = new BridgeAnalysisDesign.Abutment.UC_RCC_Abut();
            this.tab_AbutmentOpenLSM = new System.Windows.Forms.TabPage();
            this.uC_AbutmentOpenLS1 = new BridgeAnalysisDesign.Abutment.UC_AbutmentOpenLS();
            this.tab_AbutmentPileLSM = new System.Windows.Forms.TabPage();
            this.uC_AbutmentPileLS1 = new BridgeAnalysisDesign.Abutment.UC_AbutmentPileLS();
            this.uC_PierOpenLS1 = new BridgeAnalysisDesign.Pier.UC_PierOpenLS();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grb_Ana_Res_DL.SuspendLayout();
            this.tc_main.SuspendLayout();
            this.tab_Analysis_DL.SuspendLayout();
            this.tbc_girder.SuspendLayout();
            this.tab_user_input.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.grb_Ana_DL_select_analysis.SuspendLayout();
            this.grb_SIDL.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_cables)).BeginInit();
            this.tab_cs_diagram.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage11.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tab_moving_data_british.SuspendLayout();
            this.groupBox45.SuspendLayout();
            this.spc_HB.Panel1.SuspendLayout();
            this.spc_HB.Panel2.SuspendLayout();
            this.spc_HB.SuspendLayout();
            this.groupBox105.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_british_loads)).BeginInit();
            this.groupBox106.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_british_loads)).BeginInit();
            this.groupBox107.SuspendLayout();
            this.groupBox108.SuspendLayout();
            this.grb_ha.SuspendLayout();
            this.grb_ha_aply.SuspendLayout();
            this.grb_hb.SuspendLayout();
            this.grb_hb_aply.SuspendLayout();
            this.tab_moving_data_indian.SuspendLayout();
            this.groupBox79.SuspendLayout();
            this.groupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).BeginInit();
            this.groupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).BeginInit();
            this.tab_moving_data_LRFD.SuspendLayout();
            this.groupBox47.SuspendLayout();
            this.groupBox48.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LRFD_long_loads)).BeginInit();
            this.groupBox49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LRFD_long_liveloads)).BeginInit();
            this.tab_analysis.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox70.SuspendLayout();
            this.groupBox109.SuspendLayout();
            this.groupBox71.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.grb_Ana_Res_LL.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox58.SuspendLayout();
            this.groupBox59.SuspendLayout();
            this.groupBox60.SuspendLayout();
            this.groupBox61.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox62.SuspendLayout();
            this.groupBox63.SuspendLayout();
            this.groupBox64.SuspendLayout();
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
            this.tc_bridge_deck.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tab_Segment.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox24.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox37.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.groupBox35.SuspendLayout();
            this.tabControl6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tab_rcc_abutment.SuspendLayout();
            this.tab_pier.SuspendLayout();
            this.tc_pier.SuspendLayout();
            this.tab_PierPileLSM.SuspendLayout();
            this.tab_PierWSM.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tab_des_form1.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.tab_des_form2.SuspendLayout();
            this.tab_des_Diagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pier_interactive_diagram)).BeginInit();
            this.panel4.SuspendLayout();
            this.tab_drawings.SuspendLayout();
            this.tab_PierOpenLSM.SuspendLayout();
            this.tc_abutment.SuspendLayout();
            this.tab_AbutmentLSM.SuspendLayout();
            this.tab_AbutmentOpenLSM.SuspendLayout();
            this.tab_AbutmentPileLSM.SuspendLayout();
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
            this.label11.Location = new System.Drawing.Point(323, 429);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "deg";
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
            this.label145.Size = new System.Drawing.Size(37, 13);
            this.label145.TabIndex = 30;
            this.label145.Text = "(Ton)";
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
            this.label151.Size = new System.Drawing.Size(53, 13);
            this.label151.TabIndex = 30;
            this.label151.Text = "(Ton-m)";
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
            this.tc_main.Controls.Add(this.tab_Segment);
            this.tc_main.Controls.Add(this.tab_rcc_abutment);
            this.tc_main.Controls.Add(this.tab_pier);
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
            this.tab_Analysis_DL.Text = "Analysis of Bridge Deck ";
            this.tab_Analysis_DL.UseVisualStyleBackColor = true;
            // 
            // tbc_girder
            // 
            this.tbc_girder.Controls.Add(this.tab_user_input);
            this.tbc_girder.Controls.Add(this.tab_cs_diagram);
            this.tbc_girder.Controls.Add(this.tab_moving_data_british);
            this.tbc_girder.Controls.Add(this.tab_moving_data_indian);
            this.tbc_girder.Controls.Add(this.tab_moving_data_LRFD);
            this.tbc_girder.Controls.Add(this.tab_analysis);
            this.tbc_girder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_girder.Location = new System.Drawing.Point(3, 3);
            this.tbc_girder.Name = "tbc_girder";
            this.tbc_girder.SelectedIndex = 0;
            this.tbc_girder.Size = new System.Drawing.Size(963, 660);
            this.tbc_girder.TabIndex = 107;
            // 
            // tab_user_input
            // 
            this.tab_user_input.Controls.Add(this.groupBox27);
            this.tab_user_input.Controls.Add(this.panel5);
            this.tab_user_input.Controls.Add(this.label227);
            this.tab_user_input.Controls.Add(this.label228);
            this.tab_user_input.Controls.Add(this.groupBox12);
            this.tab_user_input.Controls.Add(this.grb_SIDL);
            this.tab_user_input.Controls.Add(this.grb_create_input_data);
            this.tab_user_input.Controls.Add(this.pcb_cables);
            this.tab_user_input.Location = new System.Drawing.Point(4, 22);
            this.tab_user_input.Name = "tab_user_input";
            this.tab_user_input.Padding = new System.Windows.Forms.Padding(3);
            this.tab_user_input.Size = new System.Drawing.Size(955, 634);
            this.tab_user_input.TabIndex = 0;
            this.tab_user_input.Text = "User\'s Input Data";
            this.tab_user_input.UseVisualStyleBackColor = true;
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
            this.groupBox27.Location = new System.Drawing.Point(416, 308);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(514, 320);
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
            this.label312.Size = new System.Drawing.Size(59, 13);
            this.label312.TabIndex = 188;
            this.label312.Text = "N/sq.mm";
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
            this.txt_cbl_des_E.Text = "1.95E+007";
            this.txt_cbl_des_E.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cbl_des_E.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label314
            // 
            this.label314.AutoSize = true;
            this.label314.Location = new System.Drawing.Point(445, 275);
            this.label314.Name = "label314";
            this.label314.Size = new System.Drawing.Size(60, 13);
            this.label314.TabIndex = 185;
            this.label314.Text = "Ton/sq.m";
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
            this.label149.Size = new System.Drawing.Size(60, 13);
            this.label149.TabIndex = 182;
            this.label149.Text = "Ton/cu.m";
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
            this.txt_tower_Dt.Text = "1.0";
            this.txt_tower_Dt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cbl_des_gamma
            // 
            this.txt_cbl_des_gamma.Location = new System.Drawing.Point(379, 249);
            this.txt_cbl_des_gamma.Name = "txt_cbl_des_gamma";
            this.txt_cbl_des_gamma.Size = new System.Drawing.Size(58, 21);
            this.txt_cbl_des_gamma.TabIndex = 183;
            this.txt_cbl_des_gamma.Text = "1.18";
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
            this.label310.Size = new System.Drawing.Size(18, 13);
            this.label310.TabIndex = 92;
            this.label310.Text = "m";
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
            this.txt_Tower_Height.Text = "12.0";
            this.txt_Tower_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_init_cable
            // 
            this.txt_init_cable.Location = new System.Drawing.Point(379, 130);
            this.txt_init_cable.Name = "txt_init_cable";
            this.txt_init_cable.Size = new System.Drawing.Size(58, 21);
            this.txt_init_cable.TabIndex = 137;
            this.txt_init_cable.Text = "13.5";
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
            this.txt_horizontal_cbl_dist.Text = "6.0";
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
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "m";
            // 
            // label503
            // 
            this.label503.AutoSize = true;
            this.label503.Location = new System.Drawing.Point(443, 204);
            this.label503.Name = "label503";
            this.label503.Size = new System.Drawing.Size(18, 13);
            this.label503.TabIndex = 139;
            this.label503.Text = "m";
            // 
            // label284
            // 
            this.label284.AutoSize = true;
            this.label284.Location = new System.Drawing.Point(443, 133);
            this.label284.Name = "label284";
            this.label284.Size = new System.Drawing.Size(18, 13);
            this.label284.TabIndex = 139;
            this.label284.Text = "m";
            // 
            // label297
            // 
            this.label297.AutoSize = true;
            this.label297.Location = new System.Drawing.Point(443, 181);
            this.label297.Name = "label297";
            this.label297.Size = new System.Drawing.Size(18, 13);
            this.label297.TabIndex = 139;
            this.label297.Text = "m";
            // 
            // txt_tower_Bt
            // 
            this.txt_tower_Bt.Location = new System.Drawing.Point(266, 60);
            this.txt_tower_Bt.Name = "txt_tower_Bt";
            this.txt_tower_Bt.Size = new System.Drawing.Size(58, 21);
            this.txt_tower_Bt.TabIndex = 137;
            this.txt_tower_Bt.Text = "1.0";
            this.txt_tower_Bt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cable_dia
            // 
            this.txt_cable_dia.Location = new System.Drawing.Point(379, 107);
            this.txt_cable_dia.Name = "txt_cable_dia";
            this.txt_cable_dia.Size = new System.Drawing.Size(58, 21);
            this.txt_cable_dia.TabIndex = 137;
            this.txt_cable_dia.Text = "0.15";
            this.txt_cable_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_vertical_cbl_min_dist
            // 
            this.txt_vertical_cbl_min_dist.Location = new System.Drawing.Point(379, 201);
            this.txt_vertical_cbl_min_dist.Name = "txt_vertical_cbl_min_dist";
            this.txt_vertical_cbl_min_dist.Size = new System.Drawing.Size(58, 21);
            this.txt_vertical_cbl_min_dist.TabIndex = 137;
            this.txt_vertical_cbl_min_dist.Text = "3.0";
            this.txt_vertical_cbl_min_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_vertical_cbl_dist
            // 
            this.txt_vertical_cbl_dist.Location = new System.Drawing.Point(379, 178);
            this.txt_vertical_cbl_dist.Name = "txt_vertical_cbl_dist";
            this.txt_vertical_cbl_dist.Size = new System.Drawing.Size(58, 21);
            this.txt_vertical_cbl_dist.TabIndex = 137;
            this.txt_vertical_cbl_dist.Text = "1.5";
            this.txt_vertical_cbl_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label295
            // 
            this.label295.AutoSize = true;
            this.label295.Location = new System.Drawing.Point(443, 157);
            this.label295.Name = "label295";
            this.label295.Size = new System.Drawing.Size(18, 13);
            this.label295.TabIndex = 139;
            this.label295.Text = "m";
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
            this.label291.Size = new System.Drawing.Size(18, 13);
            this.label291.TabIndex = 139;
            this.label291.Text = "m";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_psc_new_design);
            this.panel5.Controls.Add(this.btn_psc_browse);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label283);
            this.panel5.Location = new System.Drawing.Point(11, 3);
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
            this.label227.Location = new System.Drawing.Point(596, 13);
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
            this.label228.Location = new System.Drawing.Point(455, 13);
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
            this.grb_SIDL.Controls.Add(this.txt_Ana_LL_member_load);
            this.grb_SIDL.Controls.Add(this.btn_remove);
            this.grb_SIDL.Controls.Add(this.btn_remove_all);
            this.grb_SIDL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_SIDL.Location = new System.Drawing.Point(427, 39);
            this.grb_SIDL.Name = "grb_SIDL";
            this.grb_SIDL.Size = new System.Drawing.Size(398, 16);
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
            this.txt_Ana_LL_member_load.Text = resources.GetString("txt_Ana_LL_member_load.Text");
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
            // grb_create_input_data
            // 
            this.grb_create_input_data.Controls.Add(this.txt_Ana_width_cantilever);
            this.grb_create_input_data.Controls.Add(this.label6);
            this.grb_create_input_data.Controls.Add(this.label4);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_DL_eff_depth);
            this.grb_create_input_data.Controls.Add(this.label5);
            this.grb_create_input_data.Controls.Add(this.label285);
            this.grb_create_input_data.Controls.Add(this.label32);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_B);
            this.grb_create_input_data.Controls.Add(this.label19);
            this.grb_create_input_data.Controls.Add(this.label33);
            this.grb_create_input_data.Controls.Add(this.label3);
            this.grb_create_input_data.Controls.Add(this.label286);
            this.grb_create_input_data.Controls.Add(this.txt_support_distance);
            this.grb_create_input_data.Controls.Add(this.label25);
            this.grb_create_input_data.Controls.Add(this.label24);
            this.grb_create_input_data.Controls.Add(this.txt_L3);
            this.grb_create_input_data.Controls.Add(this.label288);
            this.grb_create_input_data.Controls.Add(this.txt_L2);
            this.grb_create_input_data.Controls.Add(this.label289);
            this.grb_create_input_data.Controls.Add(this.label290);
            this.grb_create_input_data.Controls.Add(this.label18);
            this.grb_create_input_data.Controls.Add(this.label239);
            this.grb_create_input_data.Controls.Add(this.label11);
            this.grb_create_input_data.Controls.Add(this.textBox2);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_LL_factor);
            this.grb_create_input_data.Controls.Add(this.label240);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_DL_factor);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_skew_angle);
            this.grb_create_input_data.Controls.Add(this.label10);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_L1);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.Location = new System.Drawing.Point(11, 308);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(396, 319);
            this.grb_create_input_data.TabIndex = 1;
            this.grb_create_input_data.TabStop = false;
            this.grb_create_input_data.Text = "Deck Inputs";
            // 
            // txt_Ana_width_cantilever
            // 
            this.txt_Ana_width_cantilever.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_width_cantilever.Location = new System.Drawing.Point(262, 177);
            this.txt_Ana_width_cantilever.Name = "txt_Ana_width_cantilever";
            this.txt_Ana_width_cantilever.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_width_cantilever.TabIndex = 3;
            this.txt_Ana_width_cantilever.Text = "1.925";
            this.txt_Ana_width_cantilever.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_width_cantilever.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tentative Effective Depth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Width Along Z-direction [B]";
            // 
            // txt_Ana_DL_eff_depth
            // 
            this.txt_Ana_DL_eff_depth.Location = new System.Drawing.Point(262, 150);
            this.txt_Ana_DL_eff_depth.Name = "txt_Ana_DL_eff_depth";
            this.txt_Ana_DL_eff_depth.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_DL_eff_depth.TabIndex = 2;
            this.txt_Ana_DL_eff_depth.Text = "4.0";
            this.txt_Ana_DL_eff_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_eff_depth.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "m";
            // 
            // label285
            // 
            this.label285.AutoSize = true;
            this.label285.Location = new System.Drawing.Point(325, 72);
            this.label285.Name = "label285";
            this.label285.Size = new System.Drawing.Size(18, 13);
            this.label285.TabIndex = 140;
            this.label285.Text = "m";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(10, 180);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(175, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Width of Cantilever Slab [B2]";
            // 
            // txt_Ana_B
            // 
            this.txt_Ana_B.Location = new System.Drawing.Point(262, 96);
            this.txt_Ana_B.Name = "txt_Ana_B";
            this.txt_Ana_B.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_B.TabIndex = 1;
            this.txt_Ana_B.Text = "15.6";
            this.txt_Ana_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_B.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(325, 261);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Tons/Cu.m";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(326, 180);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(18, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "m";
            // 
            // label286
            // 
            this.label286.AutoSize = true;
            this.label286.Location = new System.Drawing.Point(325, 48);
            this.label286.Name = "label286";
            this.label286.Size = new System.Drawing.Size(18, 13);
            this.label286.TabIndex = 141;
            this.label286.Text = "m";
            // 
            // txt_support_distance
            // 
            this.txt_support_distance.Location = new System.Drawing.Point(262, 123);
            this.txt_support_distance.Name = "txt_support_distance";
            this.txt_support_distance.Size = new System.Drawing.Size(57, 21);
            this.txt_support_distance.TabIndex = 55;
            this.txt_support_distance.Text = "0.50";
            this.txt_support_distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_support_distance.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 126);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(132, 13);
            this.label25.TabIndex = 56;
            this.label25.Text = "Girder end to Support";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(326, 126);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 13);
            this.label24.TabIndex = 57;
            this.label24.Text = "m";
            // 
            // txt_L3
            // 
            this.txt_L3.Location = new System.Drawing.Point(262, 69);
            this.txt_L3.Name = "txt_L3";
            this.txt_L3.Size = new System.Drawing.Size(57, 21);
            this.txt_L3.TabIndex = 138;
            this.txt_L3.Text = "65.0";
            this.txt_L3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label288
            // 
            this.label288.AutoSize = true;
            this.label288.Location = new System.Drawing.Point(10, 72);
            this.label288.Name = "label288";
            this.label288.Size = new System.Drawing.Size(168, 13);
            this.label288.TabIndex = 136;
            this.label288.Text = "Length of Side Span 2   [L3]";
            // 
            // txt_L2
            // 
            this.txt_L2.Location = new System.Drawing.Point(262, 45);
            this.txt_L2.Name = "txt_L2";
            this.txt_L2.Size = new System.Drawing.Size(57, 21);
            this.txt_L2.TabIndex = 135;
            this.txt_L2.Text = "65.0";
            this.txt_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label289
            // 
            this.label289.AutoSize = true;
            this.label289.Location = new System.Drawing.Point(10, 48);
            this.label289.Name = "label289";
            this.label289.Size = new System.Drawing.Size(168, 13);
            this.label289.TabIndex = 134;
            this.label289.Text = "Length of Side Span 1   [L2]";
            // 
            // label290
            // 
            this.label290.AutoSize = true;
            this.label290.Location = new System.Drawing.Point(10, 21);
            this.label290.Name = "label290";
            this.label290.Size = new System.Drawing.Size(170, 13);
            this.label290.TabIndex = 132;
            this.label290.Text = "Length of Central Span  [L1]";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(10, 261);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(116, 13);
            this.label18.TabIndex = 131;
            this.label18.Text = "Unit Weight of Box \r\n";
            // 
            // label239
            // 
            this.label239.AutoSize = true;
            this.label239.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label239.Location = new System.Drawing.Point(10, 234);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(99, 13);
            this.label239.TabIndex = 131;
            this.label239.Text = "Live Load Factor";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(262, 258);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(57, 21);
            this.textBox2.TabIndex = 130;
            this.textBox2.Text = "2.5";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox2.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // txt_Ana_LL_factor
            // 
            this.txt_Ana_LL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_LL_factor.Location = new System.Drawing.Point(262, 231);
            this.txt_Ana_LL_factor.Name = "txt_Ana_LL_factor";
            this.txt_Ana_LL_factor.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_LL_factor.TabIndex = 130;
            this.txt_Ana_LL_factor.Text = "2.5";
            this.txt_Ana_LL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_LL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // label240
            // 
            this.label240.AutoSize = true;
            this.label240.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label240.Location = new System.Drawing.Point(10, 207);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(106, 13);
            this.label240.TabIndex = 129;
            this.label240.Text = "Dead Load Factor";
            // 
            // txt_Ana_DL_factor
            // 
            this.txt_Ana_DL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_DL_factor.Location = new System.Drawing.Point(262, 204);
            this.txt_Ana_DL_factor.Name = "txt_Ana_DL_factor";
            this.txt_Ana_DL_factor.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_DL_factor.TabIndex = 127;
            this.txt_Ana_DL_factor.Text = "1.25";
            this.txt_Ana_DL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // txt_Ana_skew_angle
            // 
            this.txt_Ana_skew_angle.Location = new System.Drawing.Point(259, 426);
            this.txt_Ana_skew_angle.Name = "txt_Ana_skew_angle";
            this.txt_Ana_skew_angle.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_skew_angle.TabIndex = 6;
            this.txt_Ana_skew_angle.Text = "0";
            this.txt_Ana_skew_angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_skew_angle.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 429);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Skew Angle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "m";
            // 
            // txt_Ana_L1
            // 
            this.txt_Ana_L1.Location = new System.Drawing.Point(262, 18);
            this.txt_Ana_L1.Name = "txt_Ana_L1";
            this.txt_Ana_L1.Size = new System.Drawing.Size(57, 21);
            this.txt_Ana_L1.TabIndex = 0;
            this.txt_Ana_L1.Text = "100.0";
            this.txt_Ana_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_L1.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // pcb_cables
            // 
            this.pcb_cables.BackgroundImage = global::LimitStateMethod.Properties.Resources.ExtradossedCentralTowers;
            this.pcb_cables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcb_cables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_cables.Location = new System.Drawing.Point(11, 62);
            this.pcb_cables.Name = "pcb_cables";
            this.pcb_cables.Size = new System.Drawing.Size(919, 240);
            this.pcb_cables.TabIndex = 132;
            this.pcb_cables.TabStop = false;
            // 
            // tab_cs_diagram
            // 
            this.tab_cs_diagram.Controls.Add(this.tabControl4);
            this.tab_cs_diagram.ForeColor = System.Drawing.Color.Blue;
            this.tab_cs_diagram.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram.Name = "tab_cs_diagram";
            this.tab_cs_diagram.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram.Size = new System.Drawing.Size(955, 634);
            this.tab_cs_diagram.TabIndex = 2;
            this.tab_cs_diagram.Text = "Cross Section Diagram";
            this.tab_cs_diagram.UseVisualStyleBackColor = true;
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
            this.tabPage16.Controls.Add(this.groupBox32);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(941, 602);
            this.tabPage16.TabIndex = 1;
            this.tabPage16.Text = "Data Inputs";
            this.tabPage16.UseVisualStyleBackColor = true;
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
            this.groupBox32.Location = new System.Drawing.Point(191, 14);
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
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Location = new System.Drawing.Point(2, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(500, 425);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
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
            this.groupBox26.Location = new System.Drawing.Point(109, -10);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(704, 324);
            this.groupBox26.TabIndex = 2;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Section Properties of various parts in the Cross Section at relevant Sections:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
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
            this.btn_Show_Section_Resulf.Location = new System.Drawing.Point(632, 318);
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
            this.rtb_sections.Location = new System.Drawing.Point(21, 356);
            this.rtb_sections.Name = "rtb_sections";
            this.rtb_sections.ReadOnly = true;
            this.rtb_sections.Size = new System.Drawing.Size(886, 240);
            this.rtb_sections.TabIndex = 3;
            this.rtb_sections.Text = "";
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label176.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.Red;
            this.label176.Location = new System.Drawing.Point(422, 324);
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
            this.label226.Location = new System.Drawing.Point(169, 324);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(229, 20);
            this.label226.TabIndex = 124;
            this.label226.Text = "No User Input in this page";
            // 
            // tab_moving_data_british
            // 
            this.tab_moving_data_british.Controls.Add(this.groupBox45);
            this.tab_moving_data_british.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data_british.Name = "tab_moving_data_british";
            this.tab_moving_data_british.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data_british.Size = new System.Drawing.Size(955, 634);
            this.tab_moving_data_british.TabIndex = 3;
            this.tab_moving_data_british.Text = "Moving Load Data [EURO 2]";
            this.tab_moving_data_british.UseVisualStyleBackColor = true;
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.spc_HB);
            this.groupBox45.Controls.Add(this.lbl_HB);
            this.groupBox45.Controls.Add(this.groupBox107);
            this.groupBox45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox45.Location = new System.Drawing.Point(3, 3);
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.Size = new System.Drawing.Size(949, 628);
            this.groupBox45.TabIndex = 86;
            this.groupBox45.TabStop = false;
            // 
            // spc_HB
            // 
            this.spc_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_HB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_HB.Location = new System.Drawing.Point(3, 274);
            this.spc_HB.Name = "spc_HB";
            // 
            // spc_HB.Panel1
            // 
            this.spc_HB.Panel1.Controls.Add(this.groupBox105);
            // 
            // spc_HB.Panel2
            // 
            this.spc_HB.Panel2.Controls.Add(this.groupBox106);
            this.spc_HB.Size = new System.Drawing.Size(943, 351);
            this.spc_HB.SplitterDistance = 525;
            this.spc_HB.TabIndex = 83;
            // 
            // groupBox105
            // 
            this.groupBox105.Controls.Add(this.dgv_long_british_loads);
            this.groupBox105.Controls.Add(this.label247);
            this.groupBox105.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox105.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox105.ForeColor = System.Drawing.Color.Black;
            this.groupBox105.Location = new System.Drawing.Point(0, 0);
            this.groupBox105.Name = "groupBox105";
            this.groupBox105.Size = new System.Drawing.Size(523, 349);
            this.groupBox105.TabIndex = 7;
            this.groupBox105.TabStop = false;
            this.groupBox105.Text = "Define Vehicle Axle Loads";
            // 
            // dgv_long_british_loads
            // 
            this.dgv_long_british_loads.AllowUserToAddRows = false;
            this.dgv_long_british_loads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_british_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_long_british_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_long_british_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn57,
            this.dataGridViewTextBoxColumn58,
            this.dataGridViewTextBoxColumn59,
            this.dataGridViewTextBoxColumn60,
            this.dataGridViewTextBoxColumn61,
            this.dataGridViewTextBoxColumn62,
            this.dataGridViewTextBoxColumn63,
            this.dataGridViewTextBoxColumn64,
            this.dataGridViewTextBoxColumn65,
            this.dataGridViewTextBoxColumn66,
            this.dataGridViewTextBoxColumn67,
            this.dataGridViewTextBoxColumn68,
            this.dataGridViewTextBoxColumn69,
            this.dataGridViewTextBoxColumn70,
            this.dataGridViewTextBoxColumn71,
            this.dataGridViewTextBoxColumn72,
            this.dataGridViewTextBoxColumn73,
            this.dataGridViewTextBoxColumn74,
            this.dataGridViewTextBoxColumn75,
            this.dataGridViewTextBoxColumn76});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_british_loads.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_long_british_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_long_british_loads.Location = new System.Drawing.Point(3, 51);
            this.dgv_long_british_loads.Name = "dgv_long_british_loads";
            this.dgv_long_british_loads.RowHeadersWidth = 21;
            this.dgv_long_british_loads.Size = new System.Drawing.Size(517, 295);
            this.dgv_long_british_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn55
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn55.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn55.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            this.dataGridViewTextBoxColumn55.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn55.Width = 200;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.HeaderText = "1";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn56.Width = 120;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.HeaderText = "2";
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            this.dataGridViewTextBoxColumn57.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn57.Width = 50;
            // 
            // dataGridViewTextBoxColumn58
            // 
            this.dataGridViewTextBoxColumn58.HeaderText = "3";
            this.dataGridViewTextBoxColumn58.Name = "dataGridViewTextBoxColumn58";
            this.dataGridViewTextBoxColumn58.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn58.Width = 50;
            // 
            // dataGridViewTextBoxColumn59
            // 
            this.dataGridViewTextBoxColumn59.HeaderText = "4";
            this.dataGridViewTextBoxColumn59.Name = "dataGridViewTextBoxColumn59";
            this.dataGridViewTextBoxColumn59.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn59.Width = 50;
            // 
            // dataGridViewTextBoxColumn60
            // 
            this.dataGridViewTextBoxColumn60.HeaderText = "5";
            this.dataGridViewTextBoxColumn60.Name = "dataGridViewTextBoxColumn60";
            this.dataGridViewTextBoxColumn60.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn60.Width = 50;
            // 
            // dataGridViewTextBoxColumn61
            // 
            this.dataGridViewTextBoxColumn61.HeaderText = "6";
            this.dataGridViewTextBoxColumn61.Name = "dataGridViewTextBoxColumn61";
            this.dataGridViewTextBoxColumn61.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn61.Width = 50;
            // 
            // dataGridViewTextBoxColumn62
            // 
            this.dataGridViewTextBoxColumn62.HeaderText = "7";
            this.dataGridViewTextBoxColumn62.Name = "dataGridViewTextBoxColumn62";
            this.dataGridViewTextBoxColumn62.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn62.Width = 50;
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.HeaderText = "8";
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn63.Width = 50;
            // 
            // dataGridViewTextBoxColumn64
            // 
            this.dataGridViewTextBoxColumn64.HeaderText = "9";
            this.dataGridViewTextBoxColumn64.Name = "dataGridViewTextBoxColumn64";
            this.dataGridViewTextBoxColumn64.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn64.Width = 50;
            // 
            // dataGridViewTextBoxColumn65
            // 
            this.dataGridViewTextBoxColumn65.HeaderText = "10";
            this.dataGridViewTextBoxColumn65.Name = "dataGridViewTextBoxColumn65";
            this.dataGridViewTextBoxColumn65.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn65.Width = 50;
            // 
            // dataGridViewTextBoxColumn66
            // 
            this.dataGridViewTextBoxColumn66.HeaderText = "11";
            this.dataGridViewTextBoxColumn66.Name = "dataGridViewTextBoxColumn66";
            this.dataGridViewTextBoxColumn66.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn66.Width = 50;
            // 
            // dataGridViewTextBoxColumn67
            // 
            this.dataGridViewTextBoxColumn67.HeaderText = "12";
            this.dataGridViewTextBoxColumn67.Name = "dataGridViewTextBoxColumn67";
            this.dataGridViewTextBoxColumn67.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn67.Width = 50;
            // 
            // dataGridViewTextBoxColumn68
            // 
            this.dataGridViewTextBoxColumn68.HeaderText = "13";
            this.dataGridViewTextBoxColumn68.Name = "dataGridViewTextBoxColumn68";
            this.dataGridViewTextBoxColumn68.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn68.Width = 50;
            // 
            // dataGridViewTextBoxColumn69
            // 
            this.dataGridViewTextBoxColumn69.HeaderText = "14";
            this.dataGridViewTextBoxColumn69.Name = "dataGridViewTextBoxColumn69";
            this.dataGridViewTextBoxColumn69.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn69.Width = 50;
            // 
            // dataGridViewTextBoxColumn70
            // 
            this.dataGridViewTextBoxColumn70.HeaderText = "15";
            this.dataGridViewTextBoxColumn70.Name = "dataGridViewTextBoxColumn70";
            this.dataGridViewTextBoxColumn70.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn70.Width = 50;
            // 
            // dataGridViewTextBoxColumn71
            // 
            this.dataGridViewTextBoxColumn71.HeaderText = "16";
            this.dataGridViewTextBoxColumn71.Name = "dataGridViewTextBoxColumn71";
            this.dataGridViewTextBoxColumn71.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn71.Width = 50;
            // 
            // dataGridViewTextBoxColumn72
            // 
            this.dataGridViewTextBoxColumn72.HeaderText = "17";
            this.dataGridViewTextBoxColumn72.Name = "dataGridViewTextBoxColumn72";
            this.dataGridViewTextBoxColumn72.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn72.Width = 50;
            // 
            // dataGridViewTextBoxColumn73
            // 
            this.dataGridViewTextBoxColumn73.HeaderText = "18";
            this.dataGridViewTextBoxColumn73.Name = "dataGridViewTextBoxColumn73";
            this.dataGridViewTextBoxColumn73.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn73.Width = 50;
            // 
            // dataGridViewTextBoxColumn74
            // 
            this.dataGridViewTextBoxColumn74.HeaderText = "19";
            this.dataGridViewTextBoxColumn74.Name = "dataGridViewTextBoxColumn74";
            this.dataGridViewTextBoxColumn74.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn74.Width = 50;
            // 
            // dataGridViewTextBoxColumn75
            // 
            this.dataGridViewTextBoxColumn75.HeaderText = "20";
            this.dataGridViewTextBoxColumn75.Name = "dataGridViewTextBoxColumn75";
            this.dataGridViewTextBoxColumn75.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn75.Width = 50;
            // 
            // dataGridViewTextBoxColumn76
            // 
            this.dataGridViewTextBoxColumn76.HeaderText = "21";
            this.dataGridViewTextBoxColumn76.Name = "dataGridViewTextBoxColumn76";
            this.dataGridViewTextBoxColumn76.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn76.Width = 50;
            // 
            // label247
            // 
            this.label247.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label247.Dock = System.Windows.Forms.DockStyle.Top;
            this.label247.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label247.ForeColor = System.Drawing.Color.Black;
            this.label247.Location = new System.Drawing.Point(3, 17);
            this.label247.Name = "label247";
            this.label247.Size = new System.Drawing.Size(517, 34);
            this.label247.TabIndex = 10;
            this.label247.Text = "(USER MAY CHANGE THE VAUES IN THE CELLS, THE DATA WILL BE SAVED IN FILE \"LL.TXT\" " +
    "FOR USE)";
            this.label247.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox106
            // 
            this.groupBox106.Controls.Add(this.panel3);
            this.groupBox106.Controls.Add(this.dgv_british_loads);
            this.groupBox106.Controls.Add(this.label248);
            this.groupBox106.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox106.ForeColor = System.Drawing.Color.Black;
            this.groupBox106.Location = new System.Drawing.Point(0, 0);
            this.groupBox106.Name = "groupBox106";
            this.groupBox106.Size = new System.Drawing.Size(412, 349);
            this.groupBox106.TabIndex = 8;
            this.groupBox106.TabStop = false;
            this.groupBox106.Text = "Define Load Start position";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.label267);
            this.panel3.Controls.Add(this.label282);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(406, 23);
            this.panel3.TabIndex = 16;
            // 
            // label38
            // 
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(291, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(104, 21);
            this.label38.TabIndex = 12;
            this.label38.Text = "Lane 3";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label267
            // 
            this.label267.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label267.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label267.Location = new System.Drawing.Point(77, 0);
            this.label267.Name = "label267";
            this.label267.Size = new System.Drawing.Size(104, 21);
            this.label267.TabIndex = 12;
            this.label267.Text = "Lane 1";
            this.label267.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label282
            // 
            this.label282.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label282.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label282.Location = new System.Drawing.Point(184, 0);
            this.label282.Name = "label282";
            this.label282.Size = new System.Drawing.Size(104, 21);
            this.label282.TabIndex = 12;
            this.label282.Text = "Lane 2";
            this.label282.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_british_loads
            // 
            this.dgv_british_loads.AllowUserToAddRows = false;
            this.dgv_british_loads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_british_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_british_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_british_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn77,
            this.dataGridViewTextBoxColumn78,
            this.dataGridViewTextBoxColumn79,
            this.dataGridViewTextBoxColumn80,
            this.dataGridViewTextBoxColumn81,
            this.dataGridViewTextBoxColumn82,
            this.Column28,
            this.Column29,
            this.Column30,
            this.Column31,
            this.Column32,
            this.Column33,
            this.Column34,
            this.Column35,
            this.Column36,
            this.Column37,
            this.Column38,
            this.Column39,
            this.Column40,
            this.Column41,
            this.Column42});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_british_loads.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_british_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_british_loads.Location = new System.Drawing.Point(3, 51);
            this.dgv_british_loads.Name = "dgv_british_loads";
            this.dgv_british_loads.RowHeadersWidth = 21;
            this.dgv_british_loads.Size = new System.Drawing.Size(406, 295);
            this.dgv_british_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn77
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn77.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn77.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn77.Name = "dataGridViewTextBoxColumn77";
            this.dataGridViewTextBoxColumn77.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn77.Width = 90;
            // 
            // dataGridViewTextBoxColumn78
            // 
            this.dataGridViewTextBoxColumn78.HeaderText = "1";
            this.dataGridViewTextBoxColumn78.Name = "dataGridViewTextBoxColumn78";
            this.dataGridViewTextBoxColumn78.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn78.Width = 50;
            // 
            // dataGridViewTextBoxColumn79
            // 
            this.dataGridViewTextBoxColumn79.HeaderText = "2";
            this.dataGridViewTextBoxColumn79.Name = "dataGridViewTextBoxColumn79";
            this.dataGridViewTextBoxColumn79.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn79.Width = 50;
            // 
            // dataGridViewTextBoxColumn80
            // 
            this.dataGridViewTextBoxColumn80.HeaderText = "3";
            this.dataGridViewTextBoxColumn80.Name = "dataGridViewTextBoxColumn80";
            this.dataGridViewTextBoxColumn80.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn80.Width = 50;
            // 
            // dataGridViewTextBoxColumn81
            // 
            this.dataGridViewTextBoxColumn81.HeaderText = "4";
            this.dataGridViewTextBoxColumn81.Name = "dataGridViewTextBoxColumn81";
            this.dataGridViewTextBoxColumn81.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn81.Width = 50;
            // 
            // dataGridViewTextBoxColumn82
            // 
            this.dataGridViewTextBoxColumn82.HeaderText = "5";
            this.dataGridViewTextBoxColumn82.Name = "dataGridViewTextBoxColumn82";
            this.dataGridViewTextBoxColumn82.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn82.Width = 50;
            // 
            // Column28
            // 
            this.Column28.HeaderText = "6";
            this.Column28.Name = "Column28";
            // 
            // Column29
            // 
            this.Column29.HeaderText = "7";
            this.Column29.Name = "Column29";
            // 
            // Column30
            // 
            this.Column30.HeaderText = "8";
            this.Column30.Name = "Column30";
            // 
            // Column31
            // 
            this.Column31.HeaderText = "9";
            this.Column31.Name = "Column31";
            // 
            // Column32
            // 
            this.Column32.HeaderText = "10";
            this.Column32.Name = "Column32";
            // 
            // Column33
            // 
            this.Column33.HeaderText = "11";
            this.Column33.Name = "Column33";
            // 
            // Column34
            // 
            this.Column34.HeaderText = "12";
            this.Column34.Name = "Column34";
            // 
            // Column35
            // 
            this.Column35.HeaderText = "13";
            this.Column35.Name = "Column35";
            // 
            // Column36
            // 
            this.Column36.HeaderText = "14";
            this.Column36.Name = "Column36";
            // 
            // Column37
            // 
            this.Column37.HeaderText = "15";
            this.Column37.Name = "Column37";
            // 
            // Column38
            // 
            this.Column38.HeaderText = "16";
            this.Column38.Name = "Column38";
            // 
            // Column39
            // 
            this.Column39.HeaderText = "17";
            this.Column39.Name = "Column39";
            // 
            // Column40
            // 
            this.Column40.HeaderText = "18";
            this.Column40.Name = "Column40";
            // 
            // Column41
            // 
            this.Column41.HeaderText = "19";
            this.Column41.Name = "Column41";
            // 
            // Column42
            // 
            this.Column42.HeaderText = "20";
            this.Column42.Name = "Column42";
            // 
            // label248
            // 
            this.label248.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label248.Dock = System.Windows.Forms.DockStyle.Top;
            this.label248.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label248.ForeColor = System.Drawing.Color.Black;
            this.label248.Location = new System.Drawing.Point(3, 17);
            this.label248.Name = "label248";
            this.label248.Size = new System.Drawing.Size(406, 34);
            this.label248.TabIndex = 11;
            this.label248.Text = "(Refer to Page 100, ASTRA Pro User Manual)";
            this.label248.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_HB
            // 
            this.lbl_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HB.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_HB.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_HB.Location = new System.Drawing.Point(3, 246);
            this.lbl_HB.Name = "lbl_HB";
            this.lbl_HB.Size = new System.Drawing.Size(943, 28);
            this.lbl_HB.TabIndex = 84;
            this.lbl_HB.Text = "HB LOADING";
            this.lbl_HB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox107
            // 
            this.groupBox107.Controls.Add(this.groupBox108);
            this.groupBox107.Controls.Add(this.txt_LL_lf);
            this.groupBox107.Controls.Add(this.txt_LL_impf);
            this.groupBox107.Controls.Add(this.label249);
            this.groupBox107.Controls.Add(this.txt_no_lanes);
            this.groupBox107.Controls.Add(this.txt_ll_british_lgen);
            this.groupBox107.Controls.Add(this.label252);
            this.groupBox107.Controls.Add(this.grb_ha);
            this.groupBox107.Controls.Add(this.grb_hb);
            this.groupBox107.Controls.Add(this.txt_ll_british_incr);
            this.groupBox107.Controls.Add(this.txt_lane_width);
            this.groupBox107.Controls.Add(this.label262);
            this.groupBox107.Controls.Add(this.txt_deck_width);
            this.groupBox107.Controls.Add(this.label263);
            this.groupBox107.Controls.Add(this.label264);
            this.groupBox107.Controls.Add(this.label265);
            this.groupBox107.Controls.Add(this.label266);
            this.groupBox107.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox107.Location = new System.Drawing.Point(3, 17);
            this.groupBox107.Name = "groupBox107";
            this.groupBox107.Size = new System.Drawing.Size(943, 229);
            this.groupBox107.TabIndex = 82;
            this.groupBox107.TabStop = false;
            this.groupBox107.Text = "Moving Vehicle Live Load [BS 5400, Part 2, BD 37/01]";
            // 
            // groupBox108
            // 
            this.groupBox108.Controls.Add(this.chk_HA);
            this.groupBox108.Controls.Add(this.rbtn_Rail_Load);
            this.groupBox108.Controls.Add(this.rbtn_HA_HB);
            this.groupBox108.Controls.Add(this.rbtn_HA);
            this.groupBox108.Controls.Add(this.rbtn_HB);
            this.groupBox108.Location = new System.Drawing.Point(282, 22);
            this.groupBox108.Name = "groupBox108";
            this.groupBox108.Size = new System.Drawing.Size(558, 30);
            this.groupBox108.TabIndex = 5;
            this.groupBox108.TabStop = false;
            this.groupBox108.Text = "Select Live Load";
            // 
            // chk_HA
            // 
            this.chk_HA.AutoSize = true;
            this.chk_HA.Checked = true;
            this.chk_HA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_HA.Location = new System.Drawing.Point(212, 11);
            this.chk_HA.Name = "chk_HA";
            this.chk_HA.Size = new System.Drawing.Size(90, 17);
            this.chk_HA.TabIndex = 6;
            this.chk_HA.Text = "HA Loading";
            this.chk_HA.UseVisualStyleBackColor = true;
            this.chk_HA.CheckedChanged += new System.EventHandler(this.rbtn_HA_HB_CheckedChanged);
            // 
            // rbtn_Rail_Load
            // 
            this.rbtn_Rail_Load.AutoSize = true;
            this.rbtn_Rail_Load.Location = new System.Drawing.Point(440, 11);
            this.rbtn_Rail_Load.Name = "rbtn_Rail_Load";
            this.rbtn_Rail_Load.Size = new System.Drawing.Size(94, 17);
            this.rbtn_Rail_Load.TabIndex = 5;
            this.rbtn_Rail_Load.Text = "Rail Loading";
            this.rbtn_Rail_Load.UseVisualStyleBackColor = true;
            this.rbtn_Rail_Load.CheckedChanged += new System.EventHandler(this.rbtn_HA_HB_CheckedChanged);
            // 
            // rbtn_HA_HB
            // 
            this.rbtn_HA_HB.AutoSize = true;
            this.rbtn_HA_HB.Location = new System.Drawing.Point(85, 12);
            this.rbtn_HA_HB.Name = "rbtn_HA_HB";
            this.rbtn_HA_HB.Size = new System.Drawing.Size(121, 17);
            this.rbtn_HA_HB.TabIndex = 5;
            this.rbtn_HA_HB.Text = "HA && HB Loading";
            this.rbtn_HA_HB.UseVisualStyleBackColor = true;
            this.rbtn_HA_HB.Visible = false;
            this.rbtn_HA_HB.CheckedChanged += new System.EventHandler(this.rbtn_HA_HB_CheckedChanged);
            // 
            // rbtn_HA
            // 
            this.rbtn_HA.AutoSize = true;
            this.rbtn_HA.Location = new System.Drawing.Point(12, 13);
            this.rbtn_HA.Name = "rbtn_HA";
            this.rbtn_HA.Size = new System.Drawing.Size(89, 17);
            this.rbtn_HA.TabIndex = 5;
            this.rbtn_HA.Text = "HA Loading";
            this.rbtn_HA.UseVisualStyleBackColor = true;
            this.rbtn_HA.Visible = false;
            this.rbtn_HA.CheckedChanged += new System.EventHandler(this.rbtn_HA_HB_CheckedChanged);
            // 
            // rbtn_HB
            // 
            this.rbtn_HB.AutoSize = true;
            this.rbtn_HB.Checked = true;
            this.rbtn_HB.Location = new System.Drawing.Point(329, 11);
            this.rbtn_HB.Name = "rbtn_HB";
            this.rbtn_HB.Size = new System.Drawing.Size(89, 17);
            this.rbtn_HB.TabIndex = 5;
            this.rbtn_HB.TabStop = true;
            this.rbtn_HB.Text = "HB Loading";
            this.rbtn_HB.UseVisualStyleBackColor = true;
            this.rbtn_HB.CheckedChanged += new System.EventHandler(this.rbtn_HA_HB_CheckedChanged);
            // 
            // txt_LL_lf
            // 
            this.txt_LL_lf.Location = new System.Drawing.Point(177, 137);
            this.txt_LL_lf.Name = "txt_LL_lf";
            this.txt_LL_lf.Size = new System.Drawing.Size(59, 21);
            this.txt_LL_lf.TabIndex = 3;
            this.txt_LL_lf.Text = "1.0";
            this.txt_LL_lf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LL_lf.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // txt_LL_impf
            // 
            this.txt_LL_impf.Location = new System.Drawing.Point(177, 110);
            this.txt_LL_impf.Name = "txt_LL_impf";
            this.txt_LL_impf.Size = new System.Drawing.Size(59, 21);
            this.txt_LL_impf.TabIndex = 3;
            this.txt_LL_impf.Text = "1.0";
            this.txt_LL_impf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LL_impf.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label249.Location = new System.Drawing.Point(27, 196);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(101, 13);
            this.label249.TabIndex = 80;
            this.label249.Text = "Load Generation";
            // 
            // txt_no_lanes
            // 
            this.txt_no_lanes.Enabled = false;
            this.txt_no_lanes.Location = new System.Drawing.Point(177, 83);
            this.txt_no_lanes.Name = "txt_no_lanes";
            this.txt_no_lanes.Size = new System.Drawing.Size(59, 21);
            this.txt_no_lanes.TabIndex = 3;
            this.txt_no_lanes.Text = "3";
            this.txt_no_lanes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_no_lanes.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // txt_ll_british_lgen
            // 
            this.txt_ll_british_lgen.Enabled = false;
            this.txt_ll_british_lgen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ll_british_lgen.Location = new System.Drawing.Point(177, 193);
            this.txt_ll_british_lgen.Name = "txt_ll_british_lgen";
            this.txt_ll_british_lgen.Size = new System.Drawing.Size(59, 21);
            this.txt_ll_british_lgen.TabIndex = 79;
            this.txt_ll_british_lgen.Text = "191";
            this.txt_ll_british_lgen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ll_british_lgen.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // label252
            // 
            this.label252.AutoSize = true;
            this.label252.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label252.Location = new System.Drawing.Point(27, 175);
            this.label252.Name = "label252";
            this.label252.Size = new System.Drawing.Size(110, 13);
            this.label252.TabIndex = 60;
            this.label252.Text = "Moving Increment";
            // 
            // grb_ha
            // 
            this.grb_ha.Controls.Add(this.grb_ha_aply);
            this.grb_ha.Controls.Add(this.txt_HA_CON);
            this.grb_ha.Controls.Add(this.label255);
            this.grb_ha.Controls.Add(this.txt_HA_UDL);
            this.grb_ha.Controls.Add(this.label256);
            this.grb_ha.Controls.Add(this.label257);
            this.grb_ha.Controls.Add(this.label260);
            this.grb_ha.Location = new System.Drawing.Point(282, 57);
            this.grb_ha.Name = "grb_ha";
            this.grb_ha.Size = new System.Drawing.Size(558, 86);
            this.grb_ha.TabIndex = 4;
            this.grb_ha.TabStop = false;
            this.grb_ha.Text = "HA Loading";
            // 
            // grb_ha_aply
            // 
            this.grb_ha_aply.Controls.Add(this.chk_HA_7L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_6L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_10L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_5L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_9L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_4L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_8L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_3L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_2L);
            this.grb_ha_aply.Controls.Add(this.chk_HA_1L);
            this.grb_ha_aply.Location = new System.Drawing.Point(284, 20);
            this.grb_ha_aply.Name = "grb_ha_aply";
            this.grb_ha_aply.Size = new System.Drawing.Size(250, 59);
            this.grb_ha_aply.TabIndex = 7;
            this.grb_ha_aply.TabStop = false;
            this.grb_ha_aply.Text = "Apply HA Load on Lanes";
            this.grb_ha_aply.Visible = false;
            // 
            // chk_HA_7L
            // 
            this.chk_HA_7L.AutoSize = true;
            this.chk_HA_7L.Location = new System.Drawing.Point(71, 39);
            this.chk_HA_7L.Name = "chk_HA_7L";
            this.chk_HA_7L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_7L.TabIndex = 4;
            this.chk_HA_7L.Text = "7";
            this.chk_HA_7L.UseVisualStyleBackColor = true;
            this.chk_HA_7L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_6L
            // 
            this.chk_HA_6L.AutoSize = true;
            this.chk_HA_6L.Location = new System.Drawing.Point(32, 39);
            this.chk_HA_6L.Name = "chk_HA_6L";
            this.chk_HA_6L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_6L.TabIndex = 4;
            this.chk_HA_6L.Text = "6";
            this.chk_HA_6L.UseVisualStyleBackColor = true;
            this.chk_HA_6L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_10L
            // 
            this.chk_HA_10L.AutoSize = true;
            this.chk_HA_10L.Location = new System.Drawing.Point(183, 39);
            this.chk_HA_10L.Name = "chk_HA_10L";
            this.chk_HA_10L.Size = new System.Drawing.Size(40, 17);
            this.chk_HA_10L.TabIndex = 4;
            this.chk_HA_10L.Text = "10";
            this.chk_HA_10L.UseVisualStyleBackColor = true;
            this.chk_HA_10L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_5L
            // 
            this.chk_HA_5L.AutoSize = true;
            this.chk_HA_5L.Location = new System.Drawing.Point(183, 16);
            this.chk_HA_5L.Name = "chk_HA_5L";
            this.chk_HA_5L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_5L.TabIndex = 4;
            this.chk_HA_5L.Text = "5";
            this.chk_HA_5L.UseVisualStyleBackColor = true;
            this.chk_HA_5L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_9L
            // 
            this.chk_HA_9L.AutoSize = true;
            this.chk_HA_9L.Location = new System.Drawing.Point(149, 39);
            this.chk_HA_9L.Name = "chk_HA_9L";
            this.chk_HA_9L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_9L.TabIndex = 4;
            this.chk_HA_9L.Text = "9";
            this.chk_HA_9L.UseVisualStyleBackColor = true;
            this.chk_HA_9L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_4L
            // 
            this.chk_HA_4L.AutoSize = true;
            this.chk_HA_4L.Location = new System.Drawing.Point(149, 16);
            this.chk_HA_4L.Name = "chk_HA_4L";
            this.chk_HA_4L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_4L.TabIndex = 4;
            this.chk_HA_4L.Text = "4";
            this.chk_HA_4L.UseVisualStyleBackColor = true;
            this.chk_HA_4L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_8L
            // 
            this.chk_HA_8L.AutoSize = true;
            this.chk_HA_8L.Location = new System.Drawing.Point(110, 39);
            this.chk_HA_8L.Name = "chk_HA_8L";
            this.chk_HA_8L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_8L.TabIndex = 4;
            this.chk_HA_8L.Text = "8";
            this.chk_HA_8L.UseVisualStyleBackColor = true;
            this.chk_HA_8L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_3L
            // 
            this.chk_HA_3L.AutoSize = true;
            this.chk_HA_3L.Location = new System.Drawing.Point(110, 16);
            this.chk_HA_3L.Name = "chk_HA_3L";
            this.chk_HA_3L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_3L.TabIndex = 4;
            this.chk_HA_3L.Text = "3";
            this.chk_HA_3L.UseVisualStyleBackColor = true;
            this.chk_HA_3L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_2L
            // 
            this.chk_HA_2L.AutoSize = true;
            this.chk_HA_2L.Location = new System.Drawing.Point(71, 16);
            this.chk_HA_2L.Name = "chk_HA_2L";
            this.chk_HA_2L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_2L.TabIndex = 4;
            this.chk_HA_2L.Text = "2";
            this.chk_HA_2L.UseVisualStyleBackColor = true;
            this.chk_HA_2L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // chk_HA_1L
            // 
            this.chk_HA_1L.AutoSize = true;
            this.chk_HA_1L.Location = new System.Drawing.Point(32, 16);
            this.chk_HA_1L.Name = "chk_HA_1L";
            this.chk_HA_1L.Size = new System.Drawing.Size(33, 17);
            this.chk_HA_1L.TabIndex = 4;
            this.chk_HA_1L.Text = "1";
            this.chk_HA_1L.UseVisualStyleBackColor = true;
            this.chk_HA_1L.CheckedChanged += new System.EventHandler(this.chk_HA_1L_CheckedChanged);
            // 
            // txt_HA_CON
            // 
            this.txt_HA_CON.Location = new System.Drawing.Point(170, 55);
            this.txt_HA_CON.Name = "txt_HA_CON";
            this.txt_HA_CON.Size = new System.Drawing.Size(59, 21);
            this.txt_HA_CON.TabIndex = 3;
            this.txt_HA_CON.Text = "12.0";
            this.txt_HA_CON.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label255
            // 
            this.label255.AutoSize = true;
            this.label255.Location = new System.Drawing.Point(235, 58);
            this.label255.Name = "label255";
            this.label255.Size = new System.Drawing.Size(27, 13);
            this.label255.TabIndex = 1;
            this.label255.Text = "Ton";
            // 
            // txt_HA_UDL
            // 
            this.txt_HA_UDL.Location = new System.Drawing.Point(170, 20);
            this.txt_HA_UDL.Name = "txt_HA_UDL";
            this.txt_HA_UDL.Size = new System.Drawing.Size(59, 21);
            this.txt_HA_UDL.TabIndex = 3;
            this.txt_HA_UDL.Text = "3.44";
            this.txt_HA_UDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label256
            // 
            this.label256.AutoSize = true;
            this.label256.Location = new System.Drawing.Point(9, 50);
            this.label256.Name = "label256";
            this.label256.Size = new System.Drawing.Size(152, 26);
            this.label256.TabIndex = 1;
            this.label256.Text = "HA Loading Concentrated\r\n (Knife Edge Load, KEL)";
            // 
            // label257
            // 
            this.label257.AutoSize = true;
            this.label257.Location = new System.Drawing.Point(235, 23);
            this.label257.Name = "label257";
            this.label257.Size = new System.Drawing.Size(43, 13);
            this.label257.TabIndex = 1;
            this.label257.Text = "Ton/m";
            // 
            // label260
            // 
            this.label260.AutoSize = true;
            this.label260.Location = new System.Drawing.Point(9, 23);
            this.label260.Name = "label260";
            this.label260.Size = new System.Drawing.Size(98, 13);
            this.label260.TabIndex = 1;
            this.label260.Text = "HA Loading UDL";
            // 
            // grb_hb
            // 
            this.grb_hb.Controls.Add(this.grb_hb_aply);
            this.grb_hb.Controls.Add(this.label261);
            this.grb_hb.Controls.Add(this.cmb_HB);
            this.grb_hb.Location = new System.Drawing.Point(282, 146);
            this.grb_hb.Name = "grb_hb";
            this.grb_hb.Size = new System.Drawing.Size(558, 76);
            this.grb_hb.TabIndex = 4;
            this.grb_hb.TabStop = false;
            this.grb_hb.Text = "HB Loading";
            // 
            // grb_hb_aply
            // 
            this.grb_hb_aply.Controls.Add(this.chk_HB_7L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_6L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_10L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_5L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_9L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_4L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_8L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_3L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_2L);
            this.grb_hb_aply.Controls.Add(this.chk_HB_1L);
            this.grb_hb_aply.Location = new System.Drawing.Point(284, 11);
            this.grb_hb_aply.Name = "grb_hb_aply";
            this.grb_hb_aply.Size = new System.Drawing.Size(250, 59);
            this.grb_hb_aply.TabIndex = 7;
            this.grb_hb_aply.TabStop = false;
            this.grb_hb_aply.Text = "Apply HB Load on Lanes";
            // 
            // chk_HB_7L
            // 
            this.chk_HB_7L.AutoSize = true;
            this.chk_HB_7L.Location = new System.Drawing.Point(71, 39);
            this.chk_HB_7L.Name = "chk_HB_7L";
            this.chk_HB_7L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_7L.TabIndex = 4;
            this.chk_HB_7L.Text = "7";
            this.chk_HB_7L.UseVisualStyleBackColor = true;
            this.chk_HB_7L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_6L
            // 
            this.chk_HB_6L.AutoSize = true;
            this.chk_HB_6L.Location = new System.Drawing.Point(32, 39);
            this.chk_HB_6L.Name = "chk_HB_6L";
            this.chk_HB_6L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_6L.TabIndex = 4;
            this.chk_HB_6L.Text = "6";
            this.chk_HB_6L.UseVisualStyleBackColor = true;
            this.chk_HB_6L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_10L
            // 
            this.chk_HB_10L.AutoSize = true;
            this.chk_HB_10L.Location = new System.Drawing.Point(183, 39);
            this.chk_HB_10L.Name = "chk_HB_10L";
            this.chk_HB_10L.Size = new System.Drawing.Size(40, 17);
            this.chk_HB_10L.TabIndex = 4;
            this.chk_HB_10L.Text = "10";
            this.chk_HB_10L.UseVisualStyleBackColor = true;
            this.chk_HB_10L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_5L
            // 
            this.chk_HB_5L.AutoSize = true;
            this.chk_HB_5L.Location = new System.Drawing.Point(183, 16);
            this.chk_HB_5L.Name = "chk_HB_5L";
            this.chk_HB_5L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_5L.TabIndex = 4;
            this.chk_HB_5L.Text = "5";
            this.chk_HB_5L.UseVisualStyleBackColor = true;
            this.chk_HB_5L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_9L
            // 
            this.chk_HB_9L.AutoSize = true;
            this.chk_HB_9L.Location = new System.Drawing.Point(149, 39);
            this.chk_HB_9L.Name = "chk_HB_9L";
            this.chk_HB_9L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_9L.TabIndex = 4;
            this.chk_HB_9L.Text = "9";
            this.chk_HB_9L.UseVisualStyleBackColor = true;
            this.chk_HB_9L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_4L
            // 
            this.chk_HB_4L.AutoSize = true;
            this.chk_HB_4L.Location = new System.Drawing.Point(149, 16);
            this.chk_HB_4L.Name = "chk_HB_4L";
            this.chk_HB_4L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_4L.TabIndex = 4;
            this.chk_HB_4L.Text = "4";
            this.chk_HB_4L.UseVisualStyleBackColor = true;
            this.chk_HB_4L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_8L
            // 
            this.chk_HB_8L.AutoSize = true;
            this.chk_HB_8L.Location = new System.Drawing.Point(110, 39);
            this.chk_HB_8L.Name = "chk_HB_8L";
            this.chk_HB_8L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_8L.TabIndex = 4;
            this.chk_HB_8L.Text = "8";
            this.chk_HB_8L.UseVisualStyleBackColor = true;
            this.chk_HB_8L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_3L
            // 
            this.chk_HB_3L.AutoSize = true;
            this.chk_HB_3L.Checked = true;
            this.chk_HB_3L.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_HB_3L.Location = new System.Drawing.Point(110, 16);
            this.chk_HB_3L.Name = "chk_HB_3L";
            this.chk_HB_3L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_3L.TabIndex = 4;
            this.chk_HB_3L.Text = "3";
            this.chk_HB_3L.UseVisualStyleBackColor = true;
            this.chk_HB_3L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_2L
            // 
            this.chk_HB_2L.AutoSize = true;
            this.chk_HB_2L.Checked = true;
            this.chk_HB_2L.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_HB_2L.Location = new System.Drawing.Point(71, 16);
            this.chk_HB_2L.Name = "chk_HB_2L";
            this.chk_HB_2L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_2L.TabIndex = 4;
            this.chk_HB_2L.Text = "2";
            this.chk_HB_2L.UseVisualStyleBackColor = true;
            this.chk_HB_2L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // chk_HB_1L
            // 
            this.chk_HB_1L.AutoSize = true;
            this.chk_HB_1L.Checked = true;
            this.chk_HB_1L.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_HB_1L.Location = new System.Drawing.Point(32, 16);
            this.chk_HB_1L.Name = "chk_HB_1L";
            this.chk_HB_1L.Size = new System.Drawing.Size(33, 17);
            this.chk_HB_1L.TabIndex = 4;
            this.chk_HB_1L.Text = "1";
            this.chk_HB_1L.UseVisualStyleBackColor = true;
            this.chk_HB_1L.CheckedChanged += new System.EventHandler(this.chk_HB_1L_CheckedChanged);
            // 
            // label261
            // 
            this.label261.AutoSize = true;
            this.label261.Location = new System.Drawing.Point(22, 33);
            this.label261.Name = "label261";
            this.label261.Size = new System.Drawing.Size(124, 13);
            this.label261.TabIndex = 2;
            this.label261.Text = "Select HB Load Type";
            // 
            // cmb_HB
            // 
            this.cmb_HB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HB.FormattingEnabled = true;
            this.cmb_HB.Items.AddRange(new object[] {
            "HB_UNIT",
            "HB_25",
            "HB_30",
            "HB_37.5",
            "HB_45"});
            this.cmb_HB.Location = new System.Drawing.Point(152, 30);
            this.cmb_HB.Name = "cmb_HB";
            this.cmb_HB.Size = new System.Drawing.Size(90, 21);
            this.cmb_HB.TabIndex = 0;
            this.cmb_HB.SelectedIndexChanged += new System.EventHandler(this.cmb_HB_SelectedIndexChanged);
            // 
            // txt_ll_british_incr
            // 
            this.txt_ll_british_incr.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ll_british_incr.Location = new System.Drawing.Point(177, 170);
            this.txt_ll_british_incr.Name = "txt_ll_british_incr";
            this.txt_ll_british_incr.Size = new System.Drawing.Size(59, 18);
            this.txt_ll_british_incr.TabIndex = 58;
            this.txt_ll_british_incr.Text = "0.5";
            this.txt_ll_british_incr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ll_british_incr.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // txt_lane_width
            // 
            this.txt_lane_width.Enabled = false;
            this.txt_lane_width.Location = new System.Drawing.Point(177, 56);
            this.txt_lane_width.Name = "txt_lane_width";
            this.txt_lane_width.Size = new System.Drawing.Size(59, 21);
            this.txt_lane_width.TabIndex = 3;
            this.txt_lane_width.Text = "3.5";
            this.txt_lane_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_lane_width.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // label262
            // 
            this.label262.AutoSize = true;
            this.label262.Location = new System.Drawing.Point(27, 140);
            this.label262.Name = "label262";
            this.label262.Size = new System.Drawing.Size(72, 13);
            this.label262.TabIndex = 1;
            this.label262.Text = "Load Factor";
            // 
            // txt_deck_width
            // 
            this.txt_deck_width.Location = new System.Drawing.Point(177, 31);
            this.txt_deck_width.Name = "txt_deck_width";
            this.txt_deck_width.Size = new System.Drawing.Size(59, 21);
            this.txt_deck_width.TabIndex = 3;
            this.txt_deck_width.Text = "12.0";
            this.txt_deck_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_deck_width.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // label263
            // 
            this.label263.AutoSize = true;
            this.label263.Location = new System.Drawing.Point(27, 113);
            this.label263.Name = "label263";
            this.label263.Size = new System.Drawing.Size(85, 13);
            this.label263.TabIndex = 1;
            this.label263.Text = "Impact Factor";
            // 
            // label264
            // 
            this.label264.AutoSize = true;
            this.label264.Location = new System.Drawing.Point(27, 84);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(109, 13);
            this.label264.TabIndex = 1;
            this.label264.Text = "Total No. of Lanes";
            // 
            // label265
            // 
            this.label265.AutoSize = true;
            this.label265.Location = new System.Drawing.Point(27, 57);
            this.label265.Name = "label265";
            this.label265.Size = new System.Drawing.Size(101, 13);
            this.label265.TabIndex = 1;
            this.label265.Text = "Each Lane Width";
            // 
            // label266
            // 
            this.label266.AutoSize = true;
            this.label266.Location = new System.Drawing.Point(27, 34);
            this.label266.Name = "label266";
            this.label266.Size = new System.Drawing.Size(144, 13);
            this.label266.TabIndex = 1;
            this.label266.Text = "Total Bridge Deck Width";
            // 
            // tab_moving_data_indian
            // 
            this.tab_moving_data_indian.Controls.Add(this.groupBox79);
            this.tab_moving_data_indian.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data_indian.Name = "tab_moving_data_indian";
            this.tab_moving_data_indian.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data_indian.Size = new System.Drawing.Size(955, 634);
            this.tab_moving_data_indian.TabIndex = 5;
            this.tab_moving_data_indian.Text = "Moving Load Data [IRC 112]";
            this.tab_moving_data_indian.UseVisualStyleBackColor = true;
            // 
            // groupBox79
            // 
            this.groupBox79.Controls.Add(this.label299);
            this.groupBox79.Controls.Add(this.txt_dl_ll_comb);
            this.groupBox79.Controls.Add(this.btn_edit_load_combs);
            this.groupBox79.Controls.Add(this.chk_self_indian);
            this.groupBox79.Controls.Add(this.btn_long_restore_ll);
            this.groupBox79.Controls.Add(this.groupBox31);
            this.groupBox79.Controls.Add(this.label301);
            this.groupBox79.Controls.Add(this.label302);
            this.groupBox79.Controls.Add(this.groupBox46);
            this.groupBox79.Controls.Add(this.label304);
            this.groupBox79.Controls.Add(this.textBox1);
            this.groupBox79.Controls.Add(this.txt_IRC_XINCR);
            this.groupBox79.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox79.Location = new System.Drawing.Point(3, 3);
            this.groupBox79.Name = "groupBox79";
            this.groupBox79.Size = new System.Drawing.Size(949, 628);
            this.groupBox79.TabIndex = 82;
            this.groupBox79.TabStop = false;
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
            // btn_long_restore_ll
            // 
            this.btn_long_restore_ll.Location = new System.Drawing.Point(406, 575);
            this.btn_long_restore_ll.Name = "btn_long_restore_ll";
            this.btn_long_restore_ll.Size = new System.Drawing.Size(167, 29);
            this.btn_long_restore_ll.TabIndex = 8;
            this.btn_long_restore_ll.Text = "Restore Default Values";
            this.btn_long_restore_ll.UseVisualStyleBackColor = true;
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.label300);
            this.groupBox31.Controls.Add(this.dgv_long_loads);
            this.groupBox31.ForeColor = System.Drawing.Color.Black;
            this.groupBox31.Location = new System.Drawing.Point(665, 47);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(269, 522);
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_long_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_long_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_loads.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgv_long_loads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_long_loads.Location = new System.Drawing.Point(3, 33);
            this.dgv_long_loads.Name = "dgv_long_loads";
            this.dgv_long_loads.RowHeadersWidth = 21;
            this.dgv_long_loads.Size = new System.Drawing.Size(263, 486);
            this.dgv_long_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn31
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle10;
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
            this.groupBox46.Location = new System.Drawing.Point(3, 47);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(656, 522);
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_liveloads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
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
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_liveloads.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_long_liveloads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_long_liveloads.Location = new System.Drawing.Point(3, 33);
            this.dgv_long_liveloads.Name = "dgv_long_liveloads";
            this.dgv_long_liveloads.RowHeadersWidth = 21;
            this.dgv_long_liveloads.Size = new System.Drawing.Size(650, 486);
            this.dgv_long_liveloads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle13;
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
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(874, 575);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(39, 21);
            this.textBox1.TabIndex = 79;
            this.textBox1.Text = "191";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_IRC_XINCR
            // 
            this.txt_IRC_XINCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_IRC_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IRC_XINCR.Location = new System.Drawing.Point(695, 577);
            this.txt_IRC_XINCR.Name = "txt_IRC_XINCR";
            this.txt_IRC_XINCR.Size = new System.Drawing.Size(37, 18);
            this.txt_IRC_XINCR.TabIndex = 58;
            this.txt_IRC_XINCR.Text = "0.5";
            this.txt_IRC_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_IRC_XINCR.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // tab_moving_data_LRFD
            // 
            this.tab_moving_data_LRFD.Controls.Add(this.groupBox47);
            this.tab_moving_data_LRFD.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data_LRFD.Name = "tab_moving_data_LRFD";
            this.tab_moving_data_LRFD.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data_LRFD.Size = new System.Drawing.Size(955, 634);
            this.tab_moving_data_LRFD.TabIndex = 6;
            this.tab_moving_data_LRFD.Text = "Moving Load Data [AASHTO LRFD]";
            this.tab_moving_data_LRFD.UseVisualStyleBackColor = true;
            // 
            // groupBox47
            // 
            this.groupBox47.Controls.Add(this.label305);
            this.groupBox47.Controls.Add(this.txt_LRFD_dl_ll_comb);
            this.groupBox47.Controls.Add(this.btn_LRFD_edit_load_combs);
            this.groupBox47.Controls.Add(this.chk_LRFD_self_indian);
            this.groupBox47.Controls.Add(this.btn_LRFD_long_restore_ll);
            this.groupBox47.Controls.Add(this.groupBox48);
            this.groupBox47.Controls.Add(this.label317);
            this.groupBox47.Controls.Add(this.label318);
            this.groupBox47.Controls.Add(this.groupBox49);
            this.groupBox47.Controls.Add(this.label320);
            this.groupBox47.Controls.Add(this.txt_LRFD_LL_load_gen);
            this.groupBox47.Controls.Add(this.txt_LRFD_XINCR);
            this.groupBox47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox47.Location = new System.Drawing.Point(3, 3);
            this.groupBox47.Name = "groupBox47";
            this.groupBox47.Size = new System.Drawing.Size(949, 628);
            this.groupBox47.TabIndex = 83;
            this.groupBox47.TabStop = false;
            // 
            // label305
            // 
            this.label305.AutoSize = true;
            this.label305.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label305.Location = new System.Drawing.Point(692, 601);
            this.label305.Name = "label305";
            this.label305.Size = new System.Drawing.Size(170, 13);
            this.label305.TabIndex = 85;
            this.label305.Text = "DL + LL Combine Load No";
            // 
            // txt_LRFD_dl_ll_comb
            // 
            this.txt_LRFD_dl_ll_comb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_LRFD_dl_ll_comb.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LRFD_dl_ll_comb.Location = new System.Drawing.Point(874, 601);
            this.txt_LRFD_dl_ll_comb.Name = "txt_LRFD_dl_ll_comb";
            this.txt_LRFD_dl_ll_comb.Size = new System.Drawing.Size(39, 18);
            this.txt_LRFD_dl_ll_comb.TabIndex = 84;
            this.txt_LRFD_dl_ll_comb.Text = "1";
            this.txt_LRFD_dl_ll_comb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_LRFD_edit_load_combs
            // 
            this.btn_LRFD_edit_load_combs.Location = new System.Drawing.Point(214, 574);
            this.btn_LRFD_edit_load_combs.Name = "btn_LRFD_edit_load_combs";
            this.btn_LRFD_edit_load_combs.Size = new System.Drawing.Size(157, 29);
            this.btn_LRFD_edit_load_combs.TabIndex = 83;
            this.btn_LRFD_edit_load_combs.Text = "Edit Load Combinations";
            this.btn_LRFD_edit_load_combs.UseVisualStyleBackColor = true;
            // 
            // chk_LRFD_self_indian
            // 
            this.chk_LRFD_self_indian.AutoSize = true;
            this.chk_LRFD_self_indian.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LRFD_self_indian.Location = new System.Drawing.Point(25, 579);
            this.chk_LRFD_self_indian.Name = "chk_LRFD_self_indian";
            this.chk_LRFD_self_indian.Size = new System.Drawing.Size(165, 20);
            this.chk_LRFD_self_indian.TabIndex = 81;
            this.chk_LRFD_self_indian.Text = "Apply SELFWEIGHT";
            this.chk_LRFD_self_indian.UseVisualStyleBackColor = true;
            // 
            // btn_LRFD_long_restore_ll
            // 
            this.btn_LRFD_long_restore_ll.Location = new System.Drawing.Point(406, 574);
            this.btn_LRFD_long_restore_ll.Name = "btn_LRFD_long_restore_ll";
            this.btn_LRFD_long_restore_ll.Size = new System.Drawing.Size(167, 29);
            this.btn_LRFD_long_restore_ll.TabIndex = 8;
            this.btn_LRFD_long_restore_ll.Text = "Restore Default Values";
            this.btn_LRFD_long_restore_ll.UseVisualStyleBackColor = true;
            // 
            // groupBox48
            // 
            this.groupBox48.Controls.Add(this.label306);
            this.groupBox48.Controls.Add(this.dgv_LRFD_long_loads);
            this.groupBox48.ForeColor = System.Drawing.Color.Black;
            this.groupBox48.Location = new System.Drawing.Point(665, 47);
            this.groupBox48.Name = "groupBox48";
            this.groupBox48.Size = new System.Drawing.Size(269, 522);
            this.groupBox48.TabIndex = 8;
            this.groupBox48.TabStop = false;
            this.groupBox48.Text = "Define Load Start position";
            // 
            // label306
            // 
            this.label306.AutoSize = true;
            this.label306.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label306.ForeColor = System.Drawing.Color.Black;
            this.label306.Location = new System.Drawing.Point(3, 17);
            this.label306.Name = "label306";
            this.label306.Size = new System.Drawing.Size(264, 13);
            this.label306.TabIndex = 11;
            this.label306.Text = "(Refer to Page 100, ASTRA Pro User Manual)";
            // 
            // dgv_LRFD_long_loads
            // 
            this.dgv_LRFD_long_loads.AllowUserToAddRows = false;
            this.dgv_LRFD_long_loads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LRFD_long_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgv_LRFD_long_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LRFD_long_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_LRFD_long_loads.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgv_LRFD_long_loads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_LRFD_long_loads.Location = new System.Drawing.Point(3, 33);
            this.dgv_LRFD_long_loads.Name = "dgv_LRFD_long_loads";
            this.dgv_LRFD_long_loads.RowHeadersWidth = 21;
            this.dgv_LRFD_long_loads.Size = new System.Drawing.Size(263, 486);
            this.dgv_LRFD_long_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn37
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn37.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn37.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn37.Width = 90;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "1";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn38.Width = 50;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.HeaderText = "2";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn39.Width = 50;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.HeaderText = "3";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn40.Width = 50;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "4";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn41.Width = 50;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.HeaderText = "5";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn42.Width = 50;
            // 
            // label317
            // 
            this.label317.AutoSize = true;
            this.label317.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label317.Location = new System.Drawing.Point(754, 577);
            this.label317.Name = "label317";
            this.label317.Size = new System.Drawing.Size(114, 13);
            this.label317.TabIndex = 80;
            this.label317.Text = "Load Generation";
            // 
            // label318
            // 
            this.label318.AutoSize = true;
            this.label318.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label318.ForeColor = System.Drawing.Color.Black;
            this.label318.Location = new System.Drawing.Point(110, 17);
            this.label318.Name = "label318";
            this.label318.Size = new System.Drawing.Size(687, 13);
            this.label318.TabIndex = 9;
            this.label318.Text = "GRILLAGE ANALYSIS OF SUPER STRUCTURE APPLYING 5 TYPES OF LIVE LOADS WITH SOME COM" +
    "BINATIONS";
            // 
            // groupBox49
            // 
            this.groupBox49.Controls.Add(this.label319);
            this.groupBox49.Controls.Add(this.dgv_LRFD_long_liveloads);
            this.groupBox49.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox49.ForeColor = System.Drawing.Color.Black;
            this.groupBox49.Location = new System.Drawing.Point(3, 47);
            this.groupBox49.Name = "groupBox49";
            this.groupBox49.Size = new System.Drawing.Size(656, 522);
            this.groupBox49.TabIndex = 7;
            this.groupBox49.TabStop = false;
            this.groupBox49.Text = "Define Vehicle Axle Loads";
            // 
            // label319
            // 
            this.label319.AutoSize = true;
            this.label319.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label319.ForeColor = System.Drawing.Color.Black;
            this.label319.Location = new System.Drawing.Point(2, 17);
            this.label319.Name = "label319";
            this.label319.Size = new System.Drawing.Size(648, 13);
            this.label319.TabIndex = 10;
            this.label319.Text = "(USER MAY CHANGE THE VAUES IN THE CELLS, THE DATA WILL BE SAVED IN FILE \"LL.TXT\" " +
    "FOR USE)";
            // 
            // dgv_LRFD_long_liveloads
            // 
            this.dgv_LRFD_long_liveloads.AllowUserToAddRows = false;
            this.dgv_LRFD_long_liveloads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LRFD_long_liveloads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgv_LRFD_long_liveloads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LRFD_long_liveloads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45,
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn83,
            this.dataGridViewTextBoxColumn84,
            this.dataGridViewTextBoxColumn85,
            this.dataGridViewTextBoxColumn86,
            this.dataGridViewTextBoxColumn87,
            this.dataGridViewTextBoxColumn88,
            this.dataGridViewTextBoxColumn89,
            this.dataGridViewTextBoxColumn90,
            this.dataGridViewTextBoxColumn91,
            this.dataGridViewTextBoxColumn92});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_LRFD_long_liveloads.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgv_LRFD_long_liveloads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_LRFD_long_liveloads.Location = new System.Drawing.Point(3, 33);
            this.dgv_LRFD_long_liveloads.Name = "dgv_LRFD_long_liveloads";
            this.dgv_LRFD_long_liveloads.RowHeadersWidth = 21;
            this.dgv_LRFD_long_liveloads.Size = new System.Drawing.Size(650, 486);
            this.dgv_LRFD_long_liveloads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn43
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn43.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn43.HeaderText = "Moving Load";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn43.Width = 200;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.HeaderText = "1";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn44.Width = 150;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.HeaderText = "2";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn45.Width = 50;
            // 
            // dataGridViewTextBoxColumn46
            // 
            this.dataGridViewTextBoxColumn46.HeaderText = "3";
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            this.dataGridViewTextBoxColumn46.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn46.Width = 50;
            // 
            // dataGridViewTextBoxColumn47
            // 
            this.dataGridViewTextBoxColumn47.HeaderText = "4";
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            this.dataGridViewTextBoxColumn47.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn47.Width = 50;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.HeaderText = "5";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn48.Width = 50;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.HeaderText = "6";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn49.Width = 50;
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.HeaderText = "7";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn50.Width = 50;
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.HeaderText = "8";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn51.Width = 50;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.HeaderText = "9";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn52.Width = 50;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.HeaderText = "10";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn53.Width = 50;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.HeaderText = "11";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn54.Width = 50;
            // 
            // dataGridViewTextBoxColumn83
            // 
            this.dataGridViewTextBoxColumn83.HeaderText = "12";
            this.dataGridViewTextBoxColumn83.Name = "dataGridViewTextBoxColumn83";
            this.dataGridViewTextBoxColumn83.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn83.Width = 50;
            // 
            // dataGridViewTextBoxColumn84
            // 
            this.dataGridViewTextBoxColumn84.HeaderText = "13";
            this.dataGridViewTextBoxColumn84.Name = "dataGridViewTextBoxColumn84";
            this.dataGridViewTextBoxColumn84.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn84.Width = 50;
            // 
            // dataGridViewTextBoxColumn85
            // 
            this.dataGridViewTextBoxColumn85.HeaderText = "14";
            this.dataGridViewTextBoxColumn85.Name = "dataGridViewTextBoxColumn85";
            this.dataGridViewTextBoxColumn85.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn85.Width = 50;
            // 
            // dataGridViewTextBoxColumn86
            // 
            this.dataGridViewTextBoxColumn86.HeaderText = "15";
            this.dataGridViewTextBoxColumn86.Name = "dataGridViewTextBoxColumn86";
            this.dataGridViewTextBoxColumn86.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn86.Width = 50;
            // 
            // dataGridViewTextBoxColumn87
            // 
            this.dataGridViewTextBoxColumn87.HeaderText = "16";
            this.dataGridViewTextBoxColumn87.Name = "dataGridViewTextBoxColumn87";
            this.dataGridViewTextBoxColumn87.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn87.Width = 50;
            // 
            // dataGridViewTextBoxColumn88
            // 
            this.dataGridViewTextBoxColumn88.HeaderText = "17";
            this.dataGridViewTextBoxColumn88.Name = "dataGridViewTextBoxColumn88";
            this.dataGridViewTextBoxColumn88.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn88.Width = 50;
            // 
            // dataGridViewTextBoxColumn89
            // 
            this.dataGridViewTextBoxColumn89.HeaderText = "18";
            this.dataGridViewTextBoxColumn89.Name = "dataGridViewTextBoxColumn89";
            this.dataGridViewTextBoxColumn89.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn89.Width = 50;
            // 
            // dataGridViewTextBoxColumn90
            // 
            this.dataGridViewTextBoxColumn90.HeaderText = "19";
            this.dataGridViewTextBoxColumn90.Name = "dataGridViewTextBoxColumn90";
            this.dataGridViewTextBoxColumn90.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn90.Width = 50;
            // 
            // dataGridViewTextBoxColumn91
            // 
            this.dataGridViewTextBoxColumn91.HeaderText = "20";
            this.dataGridViewTextBoxColumn91.Name = "dataGridViewTextBoxColumn91";
            this.dataGridViewTextBoxColumn91.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn91.Width = 50;
            // 
            // dataGridViewTextBoxColumn92
            // 
            this.dataGridViewTextBoxColumn92.HeaderText = "21";
            this.dataGridViewTextBoxColumn92.Name = "dataGridViewTextBoxColumn92";
            this.dataGridViewTextBoxColumn92.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn92.Width = 50;
            // 
            // label320
            // 
            this.label320.AutoSize = true;
            this.label320.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label320.Location = new System.Drawing.Point(639, 577);
            this.label320.Name = "label320";
            this.label320.Size = new System.Drawing.Size(50, 13);
            this.label320.TabIndex = 60;
            this.label320.Text = "X INCR";
            // 
            // txt_LRFD_LL_load_gen
            // 
            this.txt_LRFD_LL_load_gen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LRFD_LL_load_gen.Location = new System.Drawing.Point(874, 574);
            this.txt_LRFD_LL_load_gen.Name = "txt_LRFD_LL_load_gen";
            this.txt_LRFD_LL_load_gen.Size = new System.Drawing.Size(39, 21);
            this.txt_LRFD_LL_load_gen.TabIndex = 79;
            this.txt_LRFD_LL_load_gen.Text = "191";
            this.txt_LRFD_LL_load_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_LRFD_XINCR
            // 
            this.txt_LRFD_XINCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_LRFD_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LRFD_XINCR.Location = new System.Drawing.Point(695, 576);
            this.txt_LRFD_XINCR.Name = "txt_LRFD_XINCR";
            this.txt_LRFD_XINCR.Size = new System.Drawing.Size(37, 18);
            this.txt_LRFD_XINCR.TabIndex = 58;
            this.txt_LRFD_XINCR.Text = "0.5";
            this.txt_LRFD_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LRFD_XINCR.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // tab_analysis
            // 
            this.tab_analysis.Controls.Add(this.splitContainer1);
            this.tab_analysis.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis.Name = "tab_analysis";
            this.tab_analysis.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis.Size = new System.Drawing.Size(955, 634);
            this.tab_analysis.TabIndex = 1;
            this.tab_analysis.Text = "Analysis Process";
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
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 104;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox70);
            this.groupBox2.Controls.Add(this.groupBox109);
            this.groupBox2.Controls.Add(this.btn_Process_LL_Analysis);
            this.groupBox2.Controls.Add(this.btn_Ana_DL_create_data);
            this.groupBox2.Controls.Add(this.groupBox71);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(947, 246);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
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
            this.groupBox70.Location = new System.Drawing.Point(532, 63);
            this.groupBox70.Name = "groupBox70";
            this.groupBox70.Size = new System.Drawing.Size(339, 53);
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
            this.rbtn_esprt_fixed.Location = new System.Drawing.Point(6, 32);
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
            this.chk_esprt_fixed_MZ.Checked = true;
            this.chk_esprt_fixed_MZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_MZ.Location = new System.Drawing.Point(290, 32);
            this.chk_esprt_fixed_MZ.Name = "chk_esprt_fixed_MZ";
            this.chk_esprt_fixed_MZ.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MZ.TabIndex = 0;
            this.chk_esprt_fixed_MZ.Text = "MZ";
            this.chk_esprt_fixed_MZ.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FZ
            // 
            this.chk_esprt_fixed_FZ.AutoSize = true;
            this.chk_esprt_fixed_FZ.Location = new System.Drawing.Point(162, 32);
            this.chk_esprt_fixed_FZ.Name = "chk_esprt_fixed_FZ";
            this.chk_esprt_fixed_FZ.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FZ.TabIndex = 0;
            this.chk_esprt_fixed_FZ.Text = "FZ";
            this.chk_esprt_fixed_FZ.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_MY
            // 
            this.chk_esprt_fixed_MY.AutoSize = true;
            this.chk_esprt_fixed_MY.Location = new System.Drawing.Point(252, 32);
            this.chk_esprt_fixed_MY.Name = "chk_esprt_fixed_MY";
            this.chk_esprt_fixed_MY.Size = new System.Drawing.Size(42, 17);
            this.chk_esprt_fixed_MY.TabIndex = 0;
            this.chk_esprt_fixed_MY.Text = "MY";
            this.chk_esprt_fixed_MY.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FY
            // 
            this.chk_esprt_fixed_FY.AutoSize = true;
            this.chk_esprt_fixed_FY.Location = new System.Drawing.Point(118, 32);
            this.chk_esprt_fixed_FY.Name = "chk_esprt_fixed_FY";
            this.chk_esprt_fixed_FY.Size = new System.Drawing.Size(39, 17);
            this.chk_esprt_fixed_FY.TabIndex = 0;
            this.chk_esprt_fixed_FY.Text = "FY";
            this.chk_esprt_fixed_FY.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_MX
            // 
            this.chk_esprt_fixed_MX.AutoSize = true;
            this.chk_esprt_fixed_MX.Location = new System.Drawing.Point(203, 32);
            this.chk_esprt_fixed_MX.Name = "chk_esprt_fixed_MX";
            this.chk_esprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MX.TabIndex = 0;
            this.chk_esprt_fixed_MX.Text = "MX";
            this.chk_esprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FX
            // 
            this.chk_esprt_fixed_FX.AutoSize = true;
            this.chk_esprt_fixed_FX.Checked = true;
            this.chk_esprt_fixed_FX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_esprt_fixed_FX.Location = new System.Drawing.Point(72, 32);
            this.chk_esprt_fixed_FX.Name = "chk_esprt_fixed_FX";
            this.chk_esprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FX.TabIndex = 0;
            this.chk_esprt_fixed_FX.Text = "FX";
            this.chk_esprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // groupBox109
            // 
            this.groupBox109.Controls.Add(this.cmb_long_open_file);
            this.groupBox109.Controls.Add(this.btn_View_Moving_Load);
            this.groupBox109.Controls.Add(this.btn_view_report);
            this.groupBox109.Controls.Add(this.btn_view_data);
            this.groupBox109.Controls.Add(this.btn_view_structure);
            this.groupBox109.Location = new System.Drawing.Point(198, 7);
            this.groupBox109.Name = "groupBox109";
            this.groupBox109.Size = new System.Drawing.Size(319, 102);
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
            this.cmb_long_open_file.Location = new System.Drawing.Point(6, 18);
            this.cmb_long_open_file.Name = "cmb_long_open_file";
            this.cmb_long_open_file.Size = new System.Drawing.Size(308, 21);
            this.cmb_long_open_file.TabIndex = 79;
            this.cmb_long_open_file.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            // 
            // btn_View_Moving_Load
            // 
            this.btn_View_Moving_Load.Location = new System.Drawing.Point(168, 73);
            this.btn_View_Moving_Load.Name = "btn_View_Moving_Load";
            this.btn_View_Moving_Load.Size = new System.Drawing.Size(146, 22);
            this.btn_View_Moving_Load.TabIndex = 78;
            this.btn_View_Moving_Load.Text = "View Moving Load";
            this.btn_View_Moving_Load.UseVisualStyleBackColor = true;
            this.btn_View_Moving_Load.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(6, 73);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(146, 22);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(6, 45);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(146, 22);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_structure
            // 
            this.btn_view_structure.Location = new System.Drawing.Point(168, 45);
            this.btn_view_structure.Name = "btn_view_structure";
            this.btn_view_structure.Size = new System.Drawing.Size(146, 22);
            this.btn_view_structure.TabIndex = 74;
            this.btn_view_structure.Text = "View Structure";
            this.btn_view_structure.UseVisualStyleBackColor = true;
            this.btn_view_structure.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_Process_LL_Analysis
            // 
            this.btn_Process_LL_Analysis.Enabled = false;
            this.btn_Process_LL_Analysis.Location = new System.Drawing.Point(10, 63);
            this.btn_Process_LL_Analysis.Name = "btn_Process_LL_Analysis";
            this.btn_Process_LL_Analysis.Size = new System.Drawing.Size(182, 39);
            this.btn_Process_LL_Analysis.TabIndex = 104;
            this.btn_Process_LL_Analysis.Text = "Process Analysis";
            this.btn_Process_LL_Analysis.UseVisualStyleBackColor = true;
            this.btn_Process_LL_Analysis.Click += new System.EventHandler(this.btn_Ana_LL_process_analysis_Click);
            // 
            // btn_Ana_DL_create_data
            // 
            this.btn_Ana_DL_create_data.Location = new System.Drawing.Point(10, 20);
            this.btn_Ana_DL_create_data.Name = "btn_Ana_DL_create_data";
            this.btn_Ana_DL_create_data.Size = new System.Drawing.Size(182, 40);
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
            this.groupBox71.Location = new System.Drawing.Point(532, 4);
            this.groupBox71.Name = "groupBox71";
            this.groupBox71.Size = new System.Drawing.Size(339, 58);
            this.groupBox71.TabIndex = 133;
            this.groupBox71.TabStop = false;
            this.groupBox71.Text = "SUPPORT AT START";
            // 
            // rbtn_ssprt_pinned
            // 
            this.rbtn_ssprt_pinned.AutoSize = true;
            this.rbtn_ssprt_pinned.Checked = true;
            this.rbtn_ssprt_pinned.Location = new System.Drawing.Point(6, 13);
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
            this.rbtn_ssprt_fixed.Location = new System.Drawing.Point(6, 32);
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
            this.chk_ssprt_fixed_MZ.Location = new System.Drawing.Point(290, 32);
            this.chk_ssprt_fixed_MZ.Name = "chk_ssprt_fixed_MZ";
            this.chk_ssprt_fixed_MZ.Size = new System.Drawing.Size(43, 17);
            this.chk_ssprt_fixed_MZ.TabIndex = 0;
            this.chk_ssprt_fixed_MZ.Text = "MZ";
            this.chk_ssprt_fixed_MZ.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FZ
            // 
            this.chk_ssprt_fixed_FZ.AutoSize = true;
            this.chk_ssprt_fixed_FZ.Location = new System.Drawing.Point(162, 32);
            this.chk_ssprt_fixed_FZ.Name = "chk_ssprt_fixed_FZ";
            this.chk_ssprt_fixed_FZ.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FZ.TabIndex = 0;
            this.chk_ssprt_fixed_FZ.Text = "FZ";
            this.chk_ssprt_fixed_FZ.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_MY
            // 
            this.chk_ssprt_fixed_MY.AutoSize = true;
            this.chk_ssprt_fixed_MY.Location = new System.Drawing.Point(252, 32);
            this.chk_ssprt_fixed_MY.Name = "chk_ssprt_fixed_MY";
            this.chk_ssprt_fixed_MY.Size = new System.Drawing.Size(42, 17);
            this.chk_ssprt_fixed_MY.TabIndex = 0;
            this.chk_ssprt_fixed_MY.Text = "MY";
            this.chk_ssprt_fixed_MY.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FY
            // 
            this.chk_ssprt_fixed_FY.AutoSize = true;
            this.chk_ssprt_fixed_FY.Location = new System.Drawing.Point(118, 32);
            this.chk_ssprt_fixed_FY.Name = "chk_ssprt_fixed_FY";
            this.chk_ssprt_fixed_FY.Size = new System.Drawing.Size(39, 17);
            this.chk_ssprt_fixed_FY.TabIndex = 0;
            this.chk_ssprt_fixed_FY.Text = "FY";
            this.chk_ssprt_fixed_FY.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_MX
            // 
            this.chk_ssprt_fixed_MX.AutoSize = true;
            this.chk_ssprt_fixed_MX.Location = new System.Drawing.Point(203, 32);
            this.chk_ssprt_fixed_MX.Name = "chk_ssprt_fixed_MX";
            this.chk_ssprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_ssprt_fixed_MX.TabIndex = 0;
            this.chk_ssprt_fixed_MX.Text = "MX";
            this.chk_ssprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_ssprt_fixed_FX
            // 
            this.chk_ssprt_fixed_FX.AutoSize = true;
            this.chk_ssprt_fixed_FX.Location = new System.Drawing.Point(72, 32);
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
            this.tabControl5.Controls.Add(this.tabPage6);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(0, 0);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(947, 501);
            this.tabControl5.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox44);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(939, 475);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Analysis Results";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.groupBox7);
            this.groupBox44.Controls.Add(this.label238);
            this.groupBox44.Controls.Add(this.label164);
            this.groupBox44.Controls.Add(this.groupBox11);
            this.groupBox44.Controls.Add(this.groupBox58);
            this.groupBox44.Controls.Add(this.groupBox59);
            this.groupBox44.Controls.Add(this.groupBox60);
            this.groupBox44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox44.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox44.ForeColor = System.Drawing.Color.Red;
            this.groupBox44.Location = new System.Drawing.Point(3, 3);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(933, 469);
            this.groupBox44.TabIndex = 94;
            this.groupBox44.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.groupBox16);
            this.groupBox7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(400, 437);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(327, 17);
            this.groupBox7.TabIndex = 105;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Super Imposed Dead Load [SIDL+FPLL] Analysis Result";
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
            this.groupBox16.Size = new System.Drawing.Size(321, 0);
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
            this.label160.Size = new System.Drawing.Size(37, 13);
            this.label160.TabIndex = 35;
            this.label160.Text = "(Ton)";
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
            this.label161.Size = new System.Drawing.Size(53, 13);
            this.label161.TabIndex = 36;
            this.label161.Text = "(Ton-m)";
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
            this.txt_Ana_live_outer_long_L8_shear.Location = new System.Drawing.Point(196, 104);
            this.txt_Ana_live_outer_long_L8_shear.Name = "txt_Ana_live_outer_long_L8_shear";
            this.txt_Ana_live_outer_long_L8_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L8_shear.TabIndex = 45;
            this.txt_Ana_live_outer_long_L8_shear.Text = "0";
            this.txt_Ana_live_outer_long_L8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L8_moment
            // 
            this.txt_Ana_live_outer_long_L8_moment.Location = new System.Drawing.Point(81, 104);
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
            this.txt_Ana_live_outer_long_3L_8_shear.Location = new System.Drawing.Point(194, 158);
            this.txt_Ana_live_outer_long_3L_8_shear.Name = "txt_Ana_live_outer_long_3L_8_shear";
            this.txt_Ana_live_outer_long_3L_8_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_3L_8_shear.TabIndex = 42;
            this.txt_Ana_live_outer_long_3L_8_shear.Text = "0";
            this.txt_Ana_live_outer_long_3L_8_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L2_shear
            // 
            this.txt_Ana_live_outer_long_L2_shear.Location = new System.Drawing.Point(194, 184);
            this.txt_Ana_live_outer_long_L2_shear.Name = "txt_Ana_live_outer_long_L2_shear";
            this.txt_Ana_live_outer_long_L2_shear.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_L2_shear.TabIndex = 26;
            this.txt_Ana_live_outer_long_L2_shear.Text = "0";
            this.txt_Ana_live_outer_long_L2_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_3L_8_moment
            // 
            this.txt_Ana_live_outer_long_3L_8_moment.Location = new System.Drawing.Point(79, 158);
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
            this.txt_Ana_live_outer_long_deff_moment.Location = new System.Drawing.Point(81, 79);
            this.txt_Ana_live_outer_long_deff_moment.Name = "txt_Ana_live_outer_long_deff_moment";
            this.txt_Ana_live_outer_long_deff_moment.Size = new System.Drawing.Size(99, 21);
            this.txt_Ana_live_outer_long_deff_moment.TabIndex = 19;
            this.txt_Ana_live_outer_long_deff_moment.Text = "0";
            this.txt_Ana_live_outer_long_deff_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_live_outer_long_L4_shear
            // 
            this.txt_Ana_live_outer_long_L4_shear.Location = new System.Drawing.Point(194, 131);
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
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Controls.Add(this.groupBox14);
            this.groupBox11.Location = new System.Drawing.Point(3, 153);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(905, 256);
            this.groupBox11.TabIndex = 106;
            this.groupBox11.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.grb_Ana_Res_DL);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(452, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 236);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dead Load [DL] Analysis Result";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.groupBox17);
            this.groupBox14.Controls.Add(this.grb_Ana_Res_LL);
            this.groupBox14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.ForeColor = System.Drawing.Color.Red;
            this.groupBox14.Location = new System.Drawing.Point(174, 14);
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
            this.label154.Size = new System.Drawing.Size(37, 13);
            this.label154.TabIndex = 33;
            this.label154.Text = "(Ton)";
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
            this.label158.Location = new System.Drawing.Point(66, 32);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(53, 13);
            this.label158.TabIndex = 34;
            this.label158.Text = "(Ton-m)";
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
            this.groupBox58.Location = new System.Drawing.Point(334, 51);
            this.groupBox58.Name = "groupBox58";
            this.groupBox58.Size = new System.Drawing.Size(445, 99);
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
            this.label534.Size = new System.Drawing.Size(24, 13);
            this.label534.TabIndex = 104;
            this.label534.Text = "kN";
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
            this.label536.Size = new System.Drawing.Size(42, 13);
            this.label536.TabIndex = 101;
            this.label536.Text = "kN-m";
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
            this.label538.Size = new System.Drawing.Size(42, 13);
            this.label538.TabIndex = 98;
            this.label538.Text = "kN-m";
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
            this.groupBox59.Size = new System.Drawing.Size(325, 100);
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
            this.label541.Size = new System.Drawing.Size(44, 13);
            this.label541.TabIndex = 104;
            this.label541.Text = "kN/m";
            // 
            // label540
            // 
            this.label540.AutoSize = true;
            this.label540.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label540.Location = new System.Drawing.Point(272, 24);
            this.label540.Name = "label540";
            this.label540.Size = new System.Drawing.Size(44, 13);
            this.label540.TabIndex = 104;
            this.label540.Text = "kN/m";
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
            // groupBox60
            // 
            this.groupBox60.Controls.Add(this.label268);
            this.groupBox60.Controls.Add(this.label269);
            this.groupBox60.Controls.Add(this.groupBox61);
            this.groupBox60.Controls.Add(this.label274);
            this.groupBox60.Controls.Add(this.label275);
            this.groupBox60.Controls.Add(this.txt_Ana_dead_cross_max_shear);
            this.groupBox60.Controls.Add(this.txt_Ana_dead_cross_max_moment);
            this.groupBox60.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox60.Location = new System.Drawing.Point(674, 20);
            this.groupBox60.Name = "groupBox60";
            this.groupBox60.Size = new System.Drawing.Size(214, 21);
            this.groupBox60.TabIndex = 82;
            this.groupBox60.TabStop = false;
            this.groupBox60.Text = "Cross Girder";
            this.groupBox60.Visible = false;
            // 
            // label268
            // 
            this.label268.AutoSize = true;
            this.label268.Location = new System.Drawing.Point(137, 34);
            this.label268.Name = "label268";
            this.label268.Size = new System.Drawing.Size(29, 13);
            this.label268.TabIndex = 31;
            this.label268.Text = "T-m";
            // 
            // label269
            // 
            this.label269.AutoSize = true;
            this.label269.Location = new System.Drawing.Point(279, 34);
            this.label269.Name = "label269";
            this.label269.Size = new System.Drawing.Size(27, 13);
            this.label269.TabIndex = 30;
            this.label269.Text = "Ton";
            // 
            // groupBox61
            // 
            this.groupBox61.Controls.Add(this.label270);
            this.groupBox61.Controls.Add(this.label271);
            this.groupBox61.Controls.Add(this.label272);
            this.groupBox61.Controls.Add(this.label273);
            this.groupBox61.Controls.Add(this.textBox9);
            this.groupBox61.Controls.Add(this.textBox10);
            this.groupBox61.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox61.Location = new System.Drawing.Point(6, 15);
            this.groupBox61.Name = "groupBox61";
            this.groupBox61.Size = new System.Drawing.Size(19, 25);
            this.groupBox61.TabIndex = 82;
            this.groupBox61.TabStop = false;
            this.groupBox61.Text = "Cross Girder";
            // 
            // label270
            // 
            this.label270.AutoSize = true;
            this.label270.Location = new System.Drawing.Point(137, 34);
            this.label270.Name = "label270";
            this.label270.Size = new System.Drawing.Size(29, 13);
            this.label270.TabIndex = 31;
            this.label270.Text = "T-m";
            // 
            // label271
            // 
            this.label271.AutoSize = true;
            this.label271.Location = new System.Drawing.Point(279, 34);
            this.label271.Name = "label271";
            this.label271.Size = new System.Drawing.Size(27, 13);
            this.label271.TabIndex = 30;
            this.label271.Text = "Ton";
            // 
            // label272
            // 
            this.label272.AutoSize = true;
            this.label272.Location = new System.Drawing.Point(224, 15);
            this.label272.Name = "label272";
            this.label272.Size = new System.Drawing.Size(41, 13);
            this.label272.TabIndex = 28;
            this.label272.Text = "Shear";
            // 
            // label273
            // 
            this.label273.AutoSize = true;
            this.label273.Location = new System.Drawing.Point(81, 15);
            this.label273.Name = "label273";
            this.label273.Size = new System.Drawing.Size(52, 13);
            this.label273.TabIndex = 27;
            this.label273.Text = "Moment";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(209, 31);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(64, 21);
            this.textBox9.TabIndex = 20;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(67, 31);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(64, 21);
            this.textBox10.TabIndex = 19;
            this.textBox10.Text = "0";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label274
            // 
            this.label274.AutoSize = true;
            this.label274.Location = new System.Drawing.Point(224, 15);
            this.label274.Name = "label274";
            this.label274.Size = new System.Drawing.Size(41, 13);
            this.label274.TabIndex = 28;
            this.label274.Text = "Shear";
            // 
            // label275
            // 
            this.label275.AutoSize = true;
            this.label275.Location = new System.Drawing.Point(81, 15);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(52, 13);
            this.label275.TabIndex = 27;
            this.label275.Text = "Moment";
            // 
            // txt_Ana_dead_cross_max_shear
            // 
            this.txt_Ana_dead_cross_max_shear.Location = new System.Drawing.Point(209, 31);
            this.txt_Ana_dead_cross_max_shear.Name = "txt_Ana_dead_cross_max_shear";
            this.txt_Ana_dead_cross_max_shear.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_dead_cross_max_shear.TabIndex = 20;
            this.txt_Ana_dead_cross_max_shear.Text = "0";
            this.txt_Ana_dead_cross_max_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ana_dead_cross_max_moment
            // 
            this.txt_Ana_dead_cross_max_moment.Location = new System.Drawing.Point(67, 31);
            this.txt_Ana_dead_cross_max_moment.Name = "txt_Ana_dead_cross_max_moment";
            this.txt_Ana_dead_cross_max_moment.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_dead_cross_max_moment.TabIndex = 19;
            this.txt_Ana_dead_cross_max_moment.Text = "0";
            this.txt_Ana_dead_cross_max_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox62);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(939, 475);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Reaction Forces";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox62
            // 
            this.groupBox62.Controls.Add(this.groupBox63);
            this.groupBox62.Controls.Add(this.groupBox64);
            this.groupBox62.Controls.Add(this.groupBox65);
            this.groupBox62.Controls.Add(this.groupBox66);
            this.groupBox62.Controls.Add(this.groupBox67);
            this.groupBox62.Controls.Add(this.groupBox68);
            this.groupBox62.Controls.Add(this.g);
            this.groupBox62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox62.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox62.Location = new System.Drawing.Point(3, 3);
            this.groupBox62.Name = "groupBox62";
            this.groupBox62.Size = new System.Drawing.Size(933, 469);
            this.groupBox62.TabIndex = 29;
            this.groupBox62.TabStop = false;
            // 
            // groupBox63
            // 
            this.groupBox63.Controls.Add(this.textBox11);
            this.groupBox63.Controls.Add(this.label276);
            this.groupBox63.Controls.Add(this.label277);
            this.groupBox63.Controls.Add(this.txt_final_Mz);
            this.groupBox63.Controls.Add(this.txt_max_Mz_kN);
            this.groupBox63.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox63.ForeColor = System.Drawing.Color.Red;
            this.groupBox63.Location = new System.Drawing.Point(9, 177);
            this.groupBox63.Name = "groupBox63";
            this.groupBox63.Size = new System.Drawing.Size(310, 54);
            this.groupBox63.TabIndex = 26;
            this.groupBox63.TabStop = false;
            this.groupBox63.Text = "Maximum Mz";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(364, 176);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(76, 20);
            this.textBox11.TabIndex = 6;
            // 
            // label276
            // 
            this.label276.AutoSize = true;
            this.label276.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label276.Location = new System.Drawing.Point(113, 22);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(47, 13);
            this.label276.TabIndex = 21;
            this.label276.Text = "Ton-m";
            // 
            // label277
            // 
            this.label277.AutoSize = true;
            this.label277.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label277.Location = new System.Drawing.Point(266, 22);
            this.label277.Name = "label277";
            this.label277.Size = new System.Drawing.Size(39, 13);
            this.label277.TabIndex = 18;
            this.label277.Text = "kN-m";
            // 
            // txt_final_Mz
            // 
            this.txt_final_Mz.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_Mz.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_Mz.Location = new System.Drawing.Point(22, 19);
            this.txt_final_Mz.Name = "txt_final_Mz";
            this.txt_final_Mz.Size = new System.Drawing.Size(85, 20);
            this.txt_final_Mz.TabIndex = 16;
            this.txt_final_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_max_Mz_kN
            // 
            this.txt_max_Mz_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_max_Mz_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_max_Mz_kN.Location = new System.Drawing.Point(175, 19);
            this.txt_max_Mz_kN.Name = "txt_max_Mz_kN";
            this.txt_max_Mz_kN.Size = new System.Drawing.Size(85, 20);
            this.txt_max_Mz_kN.TabIndex = 24;
            this.txt_max_Mz_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox64
            // 
            this.groupBox64.Controls.Add(this.textBox12);
            this.groupBox64.Controls.Add(this.txt_final_Mx);
            this.groupBox64.Controls.Add(this.txt_max_Mx_kN);
            this.groupBox64.Controls.Add(this.label278);
            this.groupBox64.Controls.Add(this.label279);
            this.groupBox64.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox64.ForeColor = System.Drawing.Color.Red;
            this.groupBox64.Location = new System.Drawing.Point(9, 98);
            this.groupBox64.Name = "groupBox64";
            this.groupBox64.Size = new System.Drawing.Size(310, 56);
            this.groupBox64.TabIndex = 26;
            this.groupBox64.TabStop = false;
            this.groupBox64.Text = "Maximum Mx";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(364, 176);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(76, 20);
            this.textBox12.TabIndex = 6;
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
            // label278
            // 
            this.label278.AutoSize = true;
            this.label278.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label278.Location = new System.Drawing.Point(113, 22);
            this.label278.Name = "label278";
            this.label278.Size = new System.Drawing.Size(47, 13);
            this.label278.TabIndex = 21;
            this.label278.Text = "Ton-m";
            // 
            // label279
            // 
            this.label279.AutoSize = true;
            this.label279.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label279.Location = new System.Drawing.Point(266, 22);
            this.label279.Name = "label279";
            this.label279.Size = new System.Drawing.Size(39, 13);
            this.label279.TabIndex = 18;
            this.label279.Text = "kN-m";
            // 
            // groupBox65
            // 
            this.groupBox65.Controls.Add(this.lbl_factor);
            this.groupBox65.Controls.Add(this.txt_final_vert_rec_kN);
            this.groupBox65.Controls.Add(this.label280);
            this.groupBox65.Controls.Add(this.textBox17);
            this.groupBox65.Controls.Add(this.txt_final_vert_reac);
            this.groupBox65.Controls.Add(this.label281);
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
            this.lbl_factor.Location = new System.Drawing.Point(3, 22);
            this.lbl_factor.Name = "lbl_factor";
            this.lbl_factor.Size = new System.Drawing.Size(167, 13);
            this.lbl_factor.TabIndex = 23;
            this.lbl_factor.Text = "DL X 1.25 + LL X 2.5";
            // 
            // txt_final_vert_rec_kN
            // 
            this.txt_final_vert_rec_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_vert_rec_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_vert_rec_kN.Location = new System.Drawing.Point(180, 45);
            this.txt_final_vert_rec_kN.Name = "txt_final_vert_rec_kN";
            this.txt_final_vert_rec_kN.Size = new System.Drawing.Size(85, 20);
            this.txt_final_vert_rec_kN.TabIndex = 22;
            this.txt_final_vert_rec_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label280
            // 
            this.label280.AutoSize = true;
            this.label280.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label280.Location = new System.Drawing.Point(271, 22);
            this.label280.Name = "label280";
            this.label280.Size = new System.Drawing.Size(31, 13);
            this.label280.TabIndex = 19;
            this.label280.Text = "Ton";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(364, 176);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(76, 20);
            this.textBox17.TabIndex = 6;
            // 
            // txt_final_vert_reac
            // 
            this.txt_final_vert_reac.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_final_vert_reac.ForeColor = System.Drawing.Color.Blue;
            this.txt_final_vert_reac.Location = new System.Drawing.Point(180, 19);
            this.txt_final_vert_reac.Name = "txt_final_vert_reac";
            this.txt_final_vert_reac.Size = new System.Drawing.Size(85, 20);
            this.txt_final_vert_reac.TabIndex = 11;
            this.txt_final_vert_reac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label281
            // 
            this.label281.AutoSize = true;
            this.label281.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label281.Location = new System.Drawing.Point(271, 48);
            this.label281.Name = "label281";
            this.label281.Size = new System.Drawing.Size(23, 13);
            this.label281.TabIndex = 14;
            this.label281.Text = "kN";
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
            this.txt_left_total_Mz.Location = new System.Drawing.Point(365, 139);
            this.txt_left_total_Mz.Name = "txt_left_total_Mz";
            this.txt_left_total_Mz.ReadOnly = true;
            this.txt_left_total_Mz.Size = new System.Drawing.Size(76, 20);
            this.txt_left_total_Mz.TabIndex = 6;
            this.txt_left_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_left_total_Mx
            // 
            this.txt_left_total_Mx.Location = new System.Drawing.Point(261, 139);
            this.txt_left_total_Mx.Name = "txt_left_total_Mx";
            this.txt_left_total_Mx.ReadOnly = true;
            this.txt_left_total_Mx.Size = new System.Drawing.Size(76, 20);
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
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
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
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn10.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 118;
            // 
            // col_Max_Mx
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mx.DefaultCellStyle = dataGridViewCellStyle23;
            this.col_Max_Mx.HeaderText = "Maximum    Mx   (Ton-m)";
            this.col_Max_Mx.Name = "col_Max_Mx";
            this.col_Max_Mx.ReadOnly = true;
            this.col_Max_Mx.Width = 108;
            // 
            // col_Max_Mz
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Max_Mz.DefaultCellStyle = dataGridViewCellStyle24;
            this.col_Max_Mz.HeaderText = "Maximum   Mz  (Ton-m)";
            this.col_Max_Mz.Name = "col_Max_Mz";
            this.col_Max_Mz.ReadOnly = true;
            this.col_Max_Mz.Width = 108;
            // 
            // groupBox67
            // 
            this.groupBox67.Controls.Add(this.label327);
            this.groupBox67.Controls.Add(this.txt_dead_kN_m);
            this.groupBox67.Controls.Add(this.label354);
            this.groupBox67.Controls.Add(this.txt_dead_vert_reac_kN);
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
            this.label327.Location = new System.Drawing.Point(243, 207);
            this.label327.Name = "label327";
            this.label327.Size = new System.Drawing.Size(39, 13);
            this.label327.TabIndex = 28;
            this.label327.Text = "kN/m";
            // 
            // txt_dead_kN_m
            // 
            this.txt_dead_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_kN_m.Location = new System.Drawing.Point(124, 204);
            this.txt_dead_kN_m.Name = "txt_dead_kN_m";
            this.txt_dead_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_kN_m.TabIndex = 27;
            this.txt_dead_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label354
            // 
            this.label354.AutoSize = true;
            this.label354.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label354.Location = new System.Drawing.Point(243, 185);
            this.label354.Name = "label354";
            this.label354.Size = new System.Drawing.Size(23, 13);
            this.label354.TabIndex = 26;
            this.label354.Text = "kN";
            // 
            // txt_dead_vert_reac_kN
            // 
            this.txt_dead_vert_reac_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_kN.Location = new System.Drawing.Point(124, 182);
            this.txt_dead_vert_reac_kN.Name = "txt_dead_vert_reac_kN";
            this.txt_dead_vert_reac_kN.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_kN.TabIndex = 25;
            this.txt_dead_vert_reac_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.label371.Location = new System.Drawing.Point(243, 158);
            this.label371.Name = "label371";
            this.label371.Size = new System.Drawing.Size(31, 13);
            this.label371.TabIndex = 23;
            this.label371.Text = "Ton";
            // 
            // dgv_left_end_design_forces
            // 
            this.dgv_left_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
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
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Vert_Reaction.DefaultCellStyle = dataGridViewCellStyle26;
            this.col_Vert_Reaction.HeaderText = "Vertical Reaction (Ton)";
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
            this.txt_dead_vert_reac_ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // groupBox68
            // 
            this.groupBox68.Controls.Add(this.txt_live_kN_m);
            this.groupBox68.Controls.Add(this.label388);
            this.groupBox68.Controls.Add(this.txt_live_vert_rec_kN);
            this.groupBox68.Controls.Add(this.label399);
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
            this.txt_live_kN_m.Location = new System.Drawing.Point(120, 204);
            this.txt_live_kN_m.Name = "txt_live_kN_m";
            this.txt_live_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_live_kN_m.TabIndex = 28;
            this.txt_live_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label388
            // 
            this.label388.AutoSize = true;
            this.label388.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label388.Location = new System.Drawing.Point(239, 207);
            this.label388.Name = "label388";
            this.label388.Size = new System.Drawing.Size(39, 13);
            this.label388.TabIndex = 27;
            this.label388.Text = "kN/m";
            // 
            // txt_live_vert_rec_kN
            // 
            this.txt_live_vert_rec_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_kN.Location = new System.Drawing.Point(120, 181);
            this.txt_live_vert_rec_kN.Name = "txt_live_vert_rec_kN";
            this.txt_live_vert_rec_kN.Size = new System.Drawing.Size(113, 20);
            this.txt_live_vert_rec_kN.TabIndex = 26;
            this.txt_live_vert_rec_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label399
            // 
            this.label399.AutoSize = true;
            this.label399.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label399.Location = new System.Drawing.Point(239, 180);
            this.label399.Name = "label399";
            this.label399.Size = new System.Drawing.Size(23, 13);
            this.label399.TabIndex = 25;
            this.label399.Text = "kN";
            // 
            // label400
            // 
            this.label400.AutoSize = true;
            this.label400.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label400.Location = new System.Drawing.Point(55, 158);
            this.label400.Name = "label400";
            this.label400.Size = new System.Drawing.Size(55, 13);
            this.label400.TabIndex = 24;
            this.label400.Text = "Total ";
            // 
            // dgv_right_end_design_forces
            // 
            this.dgv_right_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgv_right_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgv_right_end_design_forces.Location = new System.Drawing.Point(17, 19);
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
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn4.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 118;
            // 
            // txt_live_vert_rec_Ton
            // 
            this.txt_live_vert_rec_Ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_Ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_Ton.Location = new System.Drawing.Point(120, 155);
            this.txt_live_vert_rec_Ton.Name = "txt_live_vert_rec_Ton";
            this.txt_live_vert_rec_Ton.Size = new System.Drawing.Size(113, 20);
            this.txt_live_vert_rec_Ton.TabIndex = 22;
            this.txt_live_vert_rec_Ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_vert_rec_Ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label401
            // 
            this.label401.AutoSize = true;
            this.label401.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label401.Location = new System.Drawing.Point(239, 158);
            this.label401.Name = "label401";
            this.label401.Size = new System.Drawing.Size(31, 13);
            this.label401.TabIndex = 14;
            this.label401.Text = "Ton";
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
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_des_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
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
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn6.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 118;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridViewTextBoxColumn7.HeaderText = "Maximum    Mx   (Ton-m)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 108;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn8.HeaderText = "Maximum   Mz  (Ton-m)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 108;
            // 
            // txt_right_total_Mz
            // 
            this.txt_right_total_Mz.Location = new System.Drawing.Point(364, 139);
            this.txt_right_total_Mz.Name = "txt_right_total_Mz";
            this.txt_right_total_Mz.ReadOnly = true;
            this.txt_right_total_Mz.Size = new System.Drawing.Size(76, 20);
            this.txt_right_total_Mz.TabIndex = 6;
            this.txt_right_total_Mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_right_total_Mx
            // 
            this.txt_right_total_Mx.Location = new System.Drawing.Point(260, 139);
            this.txt_right_total_Mx.Name = "txt_right_total_Mx";
            this.txt_right_total_Mx.ReadOnly = true;
            this.txt_right_total_Mx.Size = new System.Drawing.Size(76, 20);
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
            this.tab_worksheet_design.Controls.Add(this.tc_bridge_deck);
            this.tab_worksheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_worksheet_design.Name = "tab_worksheet_design";
            this.tab_worksheet_design.Size = new System.Drawing.Size(969, 666);
            this.tab_worksheet_design.TabIndex = 1;
            this.tab_worksheet_design.Text = "Design of Bridge";
            this.tab_worksheet_design.UseVisualStyleBackColor = true;
            // 
            // tc_bridge_deck
            // 
            this.tc_bridge_deck.Controls.Add(this.tabPage17);
            this.tc_bridge_deck.Controls.Add(this.tabPage18);
            this.tc_bridge_deck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_bridge_deck.Location = new System.Drawing.Point(0, 0);
            this.tc_bridge_deck.Name = "tc_bridge_deck";
            this.tc_bridge_deck.SelectedIndex = 0;
            this.tc_bridge_deck.Size = new System.Drawing.Size(969, 666);
            this.tc_bridge_deck.TabIndex = 16;
            // 
            // tabPage17
            // 
            this.tabPage17.Controls.Add(this.tabControl1);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(961, 640);
            this.tabPage17.TabIndex = 0;
            this.tabPage17.Text = "Bridge Deck Design";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage12);
            this.tabControl1.Controls.Add(this.tabPage13);
            this.tabControl1.Controls.Add(this.tabPage14);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(955, 634);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.groupBox8);
            this.tabPage10.Controls.Add(this.uC_BoxGirder1);
            this.tabPage10.Controls.Add(this.panel2);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(947, 608);
            this.tabPage10.TabIndex = 0;
            this.tabPage10.Text = "PSC Box Girder Design";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btn_design);
            this.groupBox8.Controls.Add(this.btn_design_of_anchorage);
            this.groupBox8.Controls.Add(this.btn_cable_frict);
            this.groupBox8.Controls.Add(this.btn_Temp_trans);
            this.groupBox8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(15, 47);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(46, 33);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "User\'s Design Trials";
            this.groupBox8.Visible = false;
            // 
            // btn_design
            // 
            this.btn_design.Location = new System.Drawing.Point(35, 147);
            this.btn_design.Name = "btn_design";
            this.btn_design.Size = new System.Drawing.Size(318, 37);
            this.btn_design.TabIndex = 12;
            this.btn_design.Tag = "Structural Design";
            this.btn_design.Text = "Structural Design";
            this.btn_design.UseVisualStyleBackColor = true;
            this.btn_design.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_design_of_anchorage
            // 
            this.btn_design_of_anchorage.Location = new System.Drawing.Point(35, 106);
            this.btn_design_of_anchorage.Name = "btn_design_of_anchorage";
            this.btn_design_of_anchorage.Size = new System.Drawing.Size(318, 37);
            this.btn_design_of_anchorage.TabIndex = 10;
            this.btn_design_of_anchorage.Text = "Cross Diaphragms, End Anchorage , Blister Block";
            this.btn_design_of_anchorage.UseVisualStyleBackColor = true;
            this.btn_design_of_anchorage.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // btn_cable_frict
            // 
            this.btn_cable_frict.Location = new System.Drawing.Point(35, 20);
            this.btn_cable_frict.Name = "btn_cable_frict";
            this.btn_cable_frict.Size = new System.Drawing.Size(318, 37);
            this.btn_cable_frict.TabIndex = 9;
            this.btn_cable_frict.Tag = "Cable Friction";
            this.btn_cable_frict.Text = "Cable Friction";
            this.btn_cable_frict.UseVisualStyleBackColor = true;
            this.btn_cable_frict.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_Temp_trans
            // 
            this.btn_Temp_trans.Location = new System.Drawing.Point(35, 63);
            this.btn_Temp_trans.Name = "btn_Temp_trans";
            this.btn_Temp_trans.Size = new System.Drawing.Size(318, 37);
            this.btn_Temp_trans.TabIndex = 13;
            this.btn_Temp_trans.Tag = "Temperature Stresses";
            this.btn_Temp_trans.Text = "Temperature Stresses";
            this.btn_Temp_trans.UseVisualStyleBackColor = true;
            this.btn_Temp_trans.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // uC_BoxGirder1
            // 
            this.uC_BoxGirder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder1.iApp = null;
            this.uC_BoxGirder1.Location = new System.Drawing.Point(3, 3);
            this.uC_BoxGirder1.Name = "uC_BoxGirder1";
            this.uC_BoxGirder1.Size = new System.Drawing.Size(941, 535);
            this.uC_BoxGirder1.Span = 48.75D;
            this.uC_BoxGirder1.TabIndex = 12;
            this.uC_BoxGirder1.Width = 9.75D;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_psc_box);
            this.panel2.Controls.Add(this.btn_worksheet_open);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 538);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(941, 67);
            this.panel2.TabIndex = 13;
            this.panel2.Visible = false;
            // 
            // btn_psc_box
            // 
            this.btn_psc_box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_psc_box.Location = new System.Drawing.Point(23, 30);
            this.btn_psc_box.Name = "btn_psc_box";
            this.btn_psc_box.Size = new System.Drawing.Size(318, 32);
            this.btn_psc_box.TabIndex = 8;
            this.btn_psc_box.Tag = "New Design Of PSC Box Girder";
            this.btn_psc_box.Text = "New Design in Excel Format";
            this.btn_psc_box.UseVisualStyleBackColor = true;
            this.btn_psc_box.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_worksheet_open
            // 
            this.btn_worksheet_open.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_worksheet_open.Location = new System.Drawing.Point(347, 30);
            this.btn_worksheet_open.Name = "btn_worksheet_open";
            this.btn_worksheet_open.Size = new System.Drawing.Size(318, 32);
            this.btn_worksheet_open.TabIndex = 11;
            this.btn_worksheet_open.Text = "Open Design in Excel Format";
            this.btn_worksheet_open.UseVisualStyleBackColor = true;
            this.btn_worksheet_open.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.button5);
            this.tabPage12.Controls.Add(this.button6);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(947, 608);
            this.tabPage12.TabIndex = 2;
            this.tabPage12.Text = "Cable Friction";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(165, 204);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(318, 32);
            this.button5.TabIndex = 16;
            this.button5.Tag = "Cable Friction";
            this.button5.Text = "New Design in Excel Format";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(489, 204);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(318, 32);
            this.button6.TabIndex = 17;
            this.button6.Text = "Open Design in Excel Format";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.button7);
            this.tabPage13.Controls.Add(this.button8);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(947, 608);
            this.tabPage13.TabIndex = 3;
            this.tabPage13.Text = "Temperature Stresses";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(150, 212);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(318, 32);
            this.button7.TabIndex = 18;
            this.button7.Tag = "Temperature Stresses";
            this.button7.Text = "New Design in Excel Format";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(474, 212);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(318, 32);
            this.button8.TabIndex = 19;
            this.button8.Text = "Open Design in Excel Format";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.button9);
            this.tabPage14.Controls.Add(this.button10);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(947, 608);
            this.tabPage14.TabIndex = 4;
            this.tabPage14.Text = "Cross Diaphragms, End Anchorage , Blister Block";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(150, 212);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(318, 32);
            this.button9.TabIndex = 18;
            this.button9.Tag = "";
            this.button9.Text = "New Design in Excel Format";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(474, 212);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(318, 32);
            this.button10.TabIndex = 19;
            this.button10.Text = "Open Design in Excel Format";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.button11);
            this.tabPage15.Controls.Add(this.button12);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(947, 608);
            this.tabPage15.TabIndex = 5;
            this.tabPage15.Text = "Structural Design";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(150, 212);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(318, 32);
            this.button11.TabIndex = 18;
            this.button11.Tag = "Structural Design";
            this.button11.Text = "New Design in Excel Format";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(474, 212);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(318, 32);
            this.button12.TabIndex = 19;
            this.button12.Text = "Open Design in Excel Format";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.uC_CableStayedDesign1);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage18.Size = new System.Drawing.Size(961, 640);
            this.tabPage18.TabIndex = 1;
            this.tabPage18.Text = "Cable Structure Design";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // uC_CableStayedDesign1
            // 
            this.uC_CableStayedDesign1.Cable_D = 0.15D;
            this.uC_CableStayedDesign1.Cable_E = 19500000D;
            this.uC_CableStayedDesign1.Cable_f = 1770D;
            this.uC_CableStayedDesign1.Cable_Gamma = 1.18D;
            this.uC_CableStayedDesign1.Cable_Nos_Text = "2277 TO 2300   2301 TO 2324   2325 TO 2348   2349 TO 2372 ";
            this.uC_CableStayedDesign1.CS_Analysis = null;
            this.uC_CableStayedDesign1.Cursor = System.Windows.Forms.Cursors.Default;
            this.uC_CableStayedDesign1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_CableStayedDesign1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_CableStayedDesign1.Location = new System.Drawing.Point(3, 3);
            this.uC_CableStayedDesign1.Name = "uC_CableStayedDesign1";
            this.uC_CableStayedDesign1.Results = null;
            this.uC_CableStayedDesign1.Size = new System.Drawing.Size(955, 634);
            this.uC_CableStayedDesign1.TabIndex = 0;
            this.uC_CableStayedDesign1.user_path = "";
            this.uC_CableStayedDesign1.Load += new System.EventHandler(this.uC_CableStayedDesign1_Load);
            // 
            // tab_Segment
            // 
            this.tab_Segment.Controls.Add(this.tabControl2);
            this.tab_Segment.Controls.Add(this.panel1);
            this.tab_Segment.Location = new System.Drawing.Point(4, 22);
            this.tab_Segment.Name = "tab_Segment";
            this.tab_Segment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Segment.Size = new System.Drawing.Size(969, 666);
            this.tab_Segment.TabIndex = 3;
            this.tab_Segment.Text = "Design in Text Format";
            this.tab_Segment.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(963, 627);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox19);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(955, 601);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Design Data [Tab 1]";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.label242);
            this.groupBox19.Controls.Add(this.label229);
            this.groupBox19.Controls.Add(this.label230);
            this.groupBox19.Controls.Add(this.cmb_tab1_Fy);
            this.groupBox19.Controls.Add(this.cmb_tab1_Fcu);
            this.groupBox19.Controls.Add(this.label221);
            this.groupBox19.Controls.Add(this.label222);
            this.groupBox19.Controls.Add(this.label223);
            this.groupBox19.Controls.Add(this.groupBox34);
            this.groupBox19.Controls.Add(this.label123);
            this.groupBox19.Controls.Add(this.label122);
            this.groupBox19.Controls.Add(this.label119);
            this.groupBox19.Controls.Add(this.label121);
            this.groupBox19.Controls.Add(this.label118);
            this.groupBox19.Controls.Add(this.label120);
            this.groupBox19.Controls.Add(this.label225);
            this.groupBox19.Controls.Add(this.label224);
            this.groupBox19.Controls.Add(this.label116);
            this.groupBox19.Controls.Add(this.label115);
            this.groupBox19.Controls.Add(this.label127);
            this.groupBox19.Controls.Add(this.label126);
            this.groupBox19.Controls.Add(this.label125);
            this.groupBox19.Controls.Add(this.label124);
            this.groupBox19.Controls.Add(this.label110);
            this.groupBox19.Controls.Add(this.label108);
            this.groupBox19.Controls.Add(this.label107);
            this.groupBox19.Controls.Add(this.label106);
            this.groupBox19.Controls.Add(this.label105);
            this.groupBox19.Controls.Add(this.groupBox21);
            this.groupBox19.Controls.Add(this.groupBox20);
            this.groupBox19.Controls.Add(this.label104);
            this.groupBox19.Controls.Add(this.label72);
            this.groupBox19.Controls.Add(this.label71);
            this.groupBox19.Controls.Add(this.label70);
            this.groupBox19.Controls.Add(this.label64);
            this.groupBox19.Controls.Add(this.label69);
            this.groupBox19.Controls.Add(this.label63);
            this.groupBox19.Controls.Add(this.label67);
            this.groupBox19.Controls.Add(this.txt_tab1_ds);
            this.groupBox19.Controls.Add(this.label68);
            this.groupBox19.Controls.Add(this.label62);
            this.groupBox19.Controls.Add(this.label51);
            this.groupBox19.Controls.Add(this.label66);
            this.groupBox19.Controls.Add(this.label50);
            this.groupBox19.Controls.Add(this.label65);
            this.groupBox19.Controls.Add(this.label49);
            this.groupBox19.Controls.Add(this.label48);
            this.groupBox19.Controls.Add(this.txt_tab1_alpha);
            this.groupBox19.Controls.Add(this.txt_tab1_FactLL);
            this.groupBox19.Controls.Add(this.txt_tab1_FactSIDL);
            this.groupBox19.Controls.Add(this.txt_tab1_FactDL);
            this.groupBox19.Controls.Add(this.txt_tab1_Mct_SIDL);
            this.groupBox19.Controls.Add(this.txt_tab1_bt);
            this.groupBox19.Controls.Add(this.txt_tab1_agt_SIDL);
            this.groupBox19.Controls.Add(this.txt_tab1_df);
            this.groupBox19.Controls.Add(this.txt_tab1_sctt);
            this.groupBox19.Controls.Add(this.txt_tab1_Mct);
            this.groupBox19.Controls.Add(this.txt_tab1_wct);
            this.groupBox19.Controls.Add(this.txt_tab1_act);
            this.groupBox19.Controls.Add(this.txt_tab1_T_loss);
            this.groupBox19.Controls.Add(this.txt_tab1_D);
            this.groupBox19.Controls.Add(this.label47);
            this.groupBox19.Controls.Add(this.txt_tab1_DW);
            this.groupBox19.Controls.Add(this.label46);
            this.groupBox19.Controls.Add(this.txt_tab1_L);
            this.groupBox19.Controls.Add(this.label45);
            this.groupBox19.Controls.Add(this.txt_tab1_exg);
            this.groupBox19.Controls.Add(this.label44);
            this.groupBox19.Controls.Add(this.txt_tab1_L2);
            this.groupBox19.Controls.Add(this.label43);
            this.groupBox19.Controls.Add(this.txt_tab1_L1);
            this.groupBox19.Controls.Add(this.label42);
            this.groupBox19.Controls.Add(this.txt_tab1_Lo);
            this.groupBox19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox19.Location = new System.Drawing.Point(3, 3);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(949, 595);
            this.groupBox19.TabIndex = 0;
            this.groupBox19.TabStop = false;
            // 
            // label242
            // 
            this.label242.AutoSize = true;
            this.label242.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label242.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label242.ForeColor = System.Drawing.Color.Blue;
            this.label242.Location = new System.Drawing.Point(425, 9);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(221, 18);
            this.label242.TabIndex = 126;
            this.label242.Text = "Blue Color values are calculated";
            // 
            // label229
            // 
            this.label229.AutoSize = true;
            this.label229.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label229.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label229.ForeColor = System.Drawing.Color.Red;
            this.label229.Location = new System.Drawing.Point(169, 9);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(218, 18);
            this.label229.TabIndex = 126;
            this.label229.Text = "Default Sample Data are shown";
            // 
            // label230
            // 
            this.label230.AutoSize = true;
            this.label230.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label230.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label230.ForeColor = System.Drawing.Color.Green;
            this.label230.Location = new System.Drawing.Point(17, 9);
            this.label230.Name = "label230";
            this.label230.Size = new System.Drawing.Size(135, 18);
            this.label230.TabIndex = 125;
            this.label230.Text = "All User Input Data";
            // 
            // cmb_tab1_Fy
            // 
            this.cmb_tab1_Fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tab1_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_tab1_Fy.FormattingEnabled = true;
            this.cmb_tab1_Fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_tab1_Fy.Location = new System.Drawing.Point(311, 225);
            this.cmb_tab1_Fy.Name = "cmb_tab1_Fy";
            this.cmb_tab1_Fy.Size = new System.Drawing.Size(65, 21);
            this.cmb_tab1_Fy.TabIndex = 62;
            // 
            // cmb_tab1_Fcu
            // 
            this.cmb_tab1_Fcu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tab1_Fcu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_tab1_Fcu.FormattingEnabled = true;
            this.cmb_tab1_Fcu.Items.AddRange(new object[] {
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
            this.cmb_tab1_Fcu.Location = new System.Drawing.Point(311, 198);
            this.cmb_tab1_Fcu.Name = "cmb_tab1_Fcu";
            this.cmb_tab1_Fcu.Size = new System.Drawing.Size(65, 21);
            this.cmb_tab1_Fcu.TabIndex = 60;
            this.cmb_tab1_Fcu.SelectedIndexChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(286, 228);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(23, 14);
            this.label221.TabIndex = 65;
            this.label221.Text = "Fe";
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label222.Location = new System.Drawing.Point(291, 201);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(18, 14);
            this.label222.TabIndex = 64;
            this.label222.Text = "M";
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Location = new System.Drawing.Point(6, 229);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(100, 13);
            this.label223.TabIndex = 63;
            this.label223.Text = "Steel Grade [fy]";
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.label208);
            this.groupBox34.Controls.Add(this.label207);
            this.groupBox34.Controls.Add(this.label206);
            this.groupBox34.Controls.Add(this.label205);
            this.groupBox34.Controls.Add(this.label204);
            this.groupBox34.Controls.Add(this.label201);
            this.groupBox34.Controls.Add(this.label203);
            this.groupBox34.Controls.Add(this.label195);
            this.groupBox34.Controls.Add(this.txt_ttu);
            this.groupBox34.Controls.Add(this.txt_tv);
            this.groupBox34.Controls.Add(this.txt_ttv);
            this.groupBox34.Controls.Add(this.txt_ft_temp28);
            this.groupBox34.Controls.Add(this.label200);
            this.groupBox34.Controls.Add(this.label196);
            this.groupBox34.Controls.Add(this.txt_fc_factor);
            this.groupBox34.Controls.Add(this.txt_fc_temp28);
            this.groupBox34.Controls.Add(this.label199);
            this.groupBox34.Controls.Add(this.label197);
            this.groupBox34.Controls.Add(this.txt_Mod_rup);
            this.groupBox34.Controls.Add(this.txt_ft_temp14);
            this.groupBox34.Controls.Add(this.label198);
            this.groupBox34.Controls.Add(this.txt_fc_serv);
            this.groupBox34.Controls.Add(this.label202);
            this.groupBox34.Controls.Add(this.txt_fc_temp14);
            this.groupBox34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox34.Location = new System.Drawing.Point(9, 425);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(895, 131);
            this.groupBox34.TabIndex = 16;
            this.groupBox34.TabStop = false;
            // 
            // label208
            // 
            this.label208.AutoSize = true;
            this.label208.Location = new System.Drawing.Point(799, 106);
            this.label208.Name = "label208";
            this.label208.Size = new System.Drawing.Size(30, 13);
            this.label208.TabIndex = 8;
            this.label208.Text = "Mpa";
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(799, 82);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(30, 13);
            this.label207.TabIndex = 8;
            this.label207.Text = "Mpa";
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Location = new System.Drawing.Point(799, 36);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(30, 13);
            this.label206.TabIndex = 8;
            this.label206.Text = "Mpa";
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Location = new System.Drawing.Point(799, 14);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(30, 13);
            this.label205.TabIndex = 8;
            this.label205.Text = "Mpa";
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Location = new System.Drawing.Point(430, 105);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(300, 13);
            this.label204.TabIndex = 7;
            this.label204.Text = "Perm. shear stress in combined shear & torsion [ttu]";
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Location = new System.Drawing.Point(430, 82);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(140, 13);
            this.label201.TabIndex = 7;
            this.label201.Text = "Perm. shear stress [tv]";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Location = new System.Drawing.Point(7, 102);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(184, 13);
            this.label203.TabIndex = 7;
            this.label203.Text = "Perm. direct  shear stress [ttv]";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(7, 76);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(228, 13);
            this.label195.TabIndex = 7;
            this.label195.Text = "Temporary tensile stress after 28 days";
            // 
            // txt_ttu
            // 
            this.txt_ttu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ttu.Location = new System.Drawing.Point(731, 103);
            this.txt_ttu.Name = "txt_ttu";
            this.txt_ttu.Size = new System.Drawing.Size(65, 21);
            this.txt_ttu.TabIndex = 6;
            this.txt_ttu.Text = "4.75";
            this.txt_ttu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tv
            // 
            this.txt_tv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tv.Location = new System.Drawing.Point(731, 80);
            this.txt_tv.Name = "txt_tv";
            this.txt_tv.Size = new System.Drawing.Size(65, 21);
            this.txt_tv.TabIndex = 6;
            this.txt_tv.Text = "4.70";
            this.txt_tv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ttv
            // 
            this.txt_ttv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ttv.Location = new System.Drawing.Point(302, 106);
            this.txt_ttv.Name = "txt_ttv";
            this.txt_ttv.Size = new System.Drawing.Size(65, 21);
            this.txt_ttv.TabIndex = 6;
            this.txt_ttv.Text = "0.42";
            this.txt_ttv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ft_temp28
            // 
            this.txt_ft_temp28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ft_temp28.Location = new System.Drawing.Point(302, 80);
            this.txt_ft_temp28.Name = "txt_ft_temp28";
            this.txt_ft_temp28.Size = new System.Drawing.Size(65, 21);
            this.txt_ft_temp28.TabIndex = 6;
            this.txt_ft_temp28.Text = "2.0";
            this.txt_ft_temp28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(430, 53);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(280, 26);
            this.label200.TabIndex = 5;
            this.label200.Text = "Factor for extra time dependent loss considered\r\n(Should be 1.0 as well as 1.2)";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(7, 53);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(263, 13);
            this.label196.TabIndex = 5;
            this.label196.Text = "Temporary compressive stress after 28 days";
            // 
            // txt_fc_factor
            // 
            this.txt_fc_factor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_factor.Location = new System.Drawing.Point(731, 57);
            this.txt_fc_factor.Name = "txt_fc_factor";
            this.txt_fc_factor.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_factor.TabIndex = 4;
            this.txt_fc_factor.Text = "1.2";
            this.txt_fc_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fc_temp28
            // 
            this.txt_fc_temp28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_temp28.Location = new System.Drawing.Point(302, 57);
            this.txt_fc_temp28.Name = "txt_fc_temp28";
            this.txt_fc_temp28.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_temp28.TabIndex = 4;
            this.txt_fc_temp28.Text = "20.0";
            this.txt_fc_temp28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(430, 30);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(170, 13);
            this.label199.TabIndex = 3;
            this.label199.Text = "Modulus of rupture [Modrup]";
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Location = new System.Drawing.Point(7, 30);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(228, 13);
            this.label197.TabIndex = 3;
            this.label197.Text = "Temporary tensile stress after 14 days";
            // 
            // txt_Mod_rup
            // 
            this.txt_Mod_rup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Mod_rup.Location = new System.Drawing.Point(731, 34);
            this.txt_Mod_rup.Name = "txt_Mod_rup";
            this.txt_Mod_rup.Size = new System.Drawing.Size(65, 21);
            this.txt_Mod_rup.TabIndex = 2;
            this.txt_Mod_rup.Text = "2.95";
            this.txt_Mod_rup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ft_temp14
            // 
            this.txt_ft_temp14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ft_temp14.Location = new System.Drawing.Point(302, 34);
            this.txt_ft_temp14.Name = "txt_ft_temp14";
            this.txt_ft_temp14.Size = new System.Drawing.Size(65, 21);
            this.txt_ft_temp14.TabIndex = 2;
            this.txt_ft_temp14.Text = " 1.74";
            this.txt_ft_temp14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(430, 14);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(295, 13);
            this.label198.TabIndex = 1;
            this.label198.Text = "Service Stage compressive stress [Scompservice]";
            // 
            // txt_fc_serv
            // 
            this.txt_fc_serv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_serv.Location = new System.Drawing.Point(731, 12);
            this.txt_fc_serv.Name = "txt_fc_serv";
            this.txt_fc_serv.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_serv.TabIndex = 0;
            this.txt_fc_serv.Text = "13.46";
            this.txt_fc_serv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Location = new System.Drawing.Point(7, 14);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(263, 13);
            this.label202.TabIndex = 1;
            this.label202.Text = "Temporary compressive stress after 14 days";
            // 
            // txt_fc_temp14
            // 
            this.txt_fc_temp14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_temp14.Location = new System.Drawing.Point(302, 12);
            this.txt_fc_temp14.Name = "txt_fc_temp14";
            this.txt_fc_temp14.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_temp14.TabIndex = 0;
            this.txt_fc_temp14.Text = "17.40";
            this.txt_fc_temp14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(381, 376);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(19, 13);
            this.label123.TabIndex = 15;
            this.label123.Text = "%";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(382, 353);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(19, 13);
            this.label122.TabIndex = 15;
            this.label122.Text = "%";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(382, 278);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(19, 13);
            this.label119.TabIndex = 15;
            this.label119.Text = "%";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(382, 327);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(34, 13);
            this.label121.TabIndex = 15;
            this.label121.Text = "days";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(382, 251);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(34, 13);
            this.label118.TabIndex = 15;
            this.label118.Text = "days";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(379, 303);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(91, 13);
            this.label120.TabIndex = 15;
            this.label120.Text = "MPa(N/mm^2)";
            // 
            // label225
            // 
            this.label225.AutoSize = true;
            this.label225.Location = new System.Drawing.Point(382, 229);
            this.label225.Name = "label225";
            this.label225.Size = new System.Drawing.Size(91, 13);
            this.label225.TabIndex = 15;
            this.label225.Text = "MPa(N/mm^2)";
            // 
            // label224
            // 
            this.label224.AutoSize = true;
            this.label224.Location = new System.Drawing.Point(380, 201);
            this.label224.Name = "label224";
            this.label224.Size = new System.Drawing.Size(91, 13);
            this.label224.TabIndex = 15;
            this.label224.Text = "MPa(N/mm^2)";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(382, 174);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(18, 13);
            this.label116.TabIndex = 15;
            this.label116.Text = "m";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(382, 150);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(18, 13);
            this.label115.TabIndex = 15;
            this.label115.Text = "m";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(811, 77);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(18, 13);
            this.label127.TabIndex = 15;
            this.label127.Text = "m";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(811, 54);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(18, 13);
            this.label126.TabIndex = 15;
            this.label126.Text = "m";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(811, 32);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(18, 13);
            this.label125.TabIndex = 15;
            this.label125.Text = "m";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(383, 402);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(18, 13);
            this.label124.TabIndex = 15;
            this.label124.Text = "m";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(382, 126);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(18, 13);
            this.label110.TabIndex = 15;
            this.label110.Text = "m";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(382, 104);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(18, 13);
            this.label108.TabIndex = 15;
            this.label108.Text = "m";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(382, 81);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(18, 13);
            this.label107.TabIndex = 15;
            this.label107.Text = "m";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(382, 58);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(18, 13);
            this.label106.TabIndex = 15;
            this.label106.Text = "m";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(382, 34);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(18, 13);
            this.label105.TabIndex = 15;
            this.label105.Text = "m";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label73);
            this.groupBox21.Controls.Add(this.txt_tab1_Tr1);
            this.groupBox21.Controls.Add(this.label76);
            this.groupBox21.Controls.Add(this.txt_tab1_Tr2);
            this.groupBox21.Controls.Add(this.label74);
            this.groupBox21.Controls.Add(this.txt_tab1_Tr3);
            this.groupBox21.Controls.Add(this.label130);
            this.groupBox21.Controls.Add(this.label129);
            this.groupBox21.Controls.Add(this.label128);
            this.groupBox21.Location = new System.Drawing.Point(511, 195);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(393, 93);
            this.groupBox21.TabIndex = 14;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "For Rise in Temperature";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(9, 22);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(112, 13);
            this.label73.TabIndex = 13;
            this.label73.Text = "Temperature [Tr1]";
            // 
            // txt_tab1_Tr1
            // 
            this.txt_tab1_Tr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tr1.Location = new System.Drawing.Point(229, 14);
            this.txt_tab1_Tr1.Name = "txt_tab1_Tr1";
            this.txt_tab1_Tr1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tr1.TabIndex = 12;
            this.txt_tab1_Tr1.Text = "17.8";
            this.txt_tab1_Tr1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(9, 76);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(112, 13);
            this.label76.TabIndex = 13;
            this.label76.Text = "Temperature [Tr3]";
            // 
            // txt_tab1_Tr2
            // 
            this.txt_tab1_Tr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tr2.Location = new System.Drawing.Point(229, 41);
            this.txt_tab1_Tr2.Name = "txt_tab1_Tr2";
            this.txt_tab1_Tr2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tr2.TabIndex = 12;
            this.txt_tab1_Tr2.Text = "4.0";
            this.txt_tab1_Tr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(9, 49);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(112, 13);
            this.label74.TabIndex = 13;
            this.label74.Text = "Temperature [Tr2]";
            // 
            // txt_tab1_Tr3
            // 
            this.txt_tab1_Tr3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tr3.Location = new System.Drawing.Point(229, 68);
            this.txt_tab1_Tr3.Name = "txt_tab1_Tr3";
            this.txt_tab1_Tr3.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tr3.TabIndex = 12;
            this.txt_tab1_Tr3.Text = "2.1";
            this.txt_tab1_Tr3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(300, 70);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(80, 13);
            this.label130.TabIndex = 15;
            this.label130.Text = "° Centigrade";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(300, 43);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(80, 13);
            this.label129.TabIndex = 15;
            this.label129.Text = "° Centigrade";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(300, 16);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(80, 13);
            this.label128.TabIndex = 15;
            this.label128.Text = "° Centigrade";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label97);
            this.groupBox20.Controls.Add(this.txt_tab1_Tf4);
            this.groupBox20.Controls.Add(this.label98);
            this.groupBox20.Controls.Add(this.txt_tab1_Tf3);
            this.groupBox20.Controls.Add(this.label99);
            this.groupBox20.Controls.Add(this.txt_tab1_Tf2);
            this.groupBox20.Controls.Add(this.label134);
            this.groupBox20.Controls.Add(this.label133);
            this.groupBox20.Controls.Add(this.label132);
            this.groupBox20.Controls.Add(this.label131);
            this.groupBox20.Controls.Add(this.label100);
            this.groupBox20.Controls.Add(this.txt_tab1_Tf1);
            this.groupBox20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox20.Location = new System.Drawing.Point(511, 290);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(393, 124);
            this.groupBox20.TabIndex = 0;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "For Fall in Temperature";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(7, 98);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(112, 13);
            this.label97.TabIndex = 7;
            this.label97.Text = "Temperature [Tf4]";
            // 
            // txt_tab1_Tf4
            // 
            this.txt_tab1_Tf4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tf4.Location = new System.Drawing.Point(229, 96);
            this.txt_tab1_Tf4.Name = "txt_tab1_Tf4";
            this.txt_tab1_Tf4.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tf4.TabIndex = 6;
            this.txt_tab1_Tf4.Text = "6.6";
            this.txt_tab1_Tf4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(7, 71);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(112, 13);
            this.label98.TabIndex = 5;
            this.label98.Text = "Temperature [Tf3]";
            // 
            // txt_tab1_Tf3
            // 
            this.txt_tab1_Tf3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tf3.Location = new System.Drawing.Point(229, 69);
            this.txt_tab1_Tf3.Name = "txt_tab1_Tf3";
            this.txt_tab1_Tf3.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tf3.TabIndex = 4;
            this.txt_tab1_Tf3.Text = "0.8";
            this.txt_tab1_Tf3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(7, 44);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(112, 13);
            this.label99.TabIndex = 3;
            this.label99.Text = "Temperature [Tf2]";
            // 
            // txt_tab1_Tf2
            // 
            this.txt_tab1_Tf2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tf2.Location = new System.Drawing.Point(229, 42);
            this.txt_tab1_Tf2.Name = "txt_tab1_Tf2";
            this.txt_tab1_Tf2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tf2.TabIndex = 2;
            this.txt_tab1_Tf2.Text = "0.7";
            this.txt_tab1_Tf2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(300, 71);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(80, 13);
            this.label134.TabIndex = 15;
            this.label134.Text = "° Centigrade";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(300, 98);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(80, 13);
            this.label133.TabIndex = 15;
            this.label133.Text = "° Centigrade";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(300, 50);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(80, 13);
            this.label132.TabIndex = 15;
            this.label132.Text = "° Centigrade";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(300, 17);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(80, 13);
            this.label131.TabIndex = 15;
            this.label131.Text = "° Centigrade";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(7, 17);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(112, 13);
            this.label100.TabIndex = 1;
            this.label100.Text = "Temperature [Tf1]";
            // 
            // txt_tab1_Tf1
            // 
            this.txt_tab1_Tf1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Tf1.Location = new System.Drawing.Point(229, 15);
            this.txt_tab1_Tf1.Name = "txt_tab1_Tf1";
            this.txt_tab1_Tf1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Tf1.TabIndex = 0;
            this.txt_tab1_Tf1.Text = "10.6";
            this.txt_tab1_Tf1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(422, 174);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(312, 13);
            this.label104.TabIndex = 13;
            this.label104.Text = "Coefficient of Thermal Expansion of Concrete [alpha]";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(424, 149);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(197, 13);
            this.label72.TabIndex = 13;
            this.label72.Text = "Ultimate Live Load factor [FactLL]";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(422, 124);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(312, 13);
            this.label71.TabIndex = 13;
            this.label71.Text = "Ultimate Super Imposed Dead Load factor [FactSIDL]";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(422, 102);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(207, 13);
            this.label70.TabIndex = 13;
            this.label70.Text = "Ultimate Dead Load factor [FactDL]";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 353);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(282, 13);
            this.label64.TabIndex = 13;
            this.label64.Text = "Maturity of girder at the time of casting of SIDL ";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(422, 78);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(262, 13);
            this.label69.TabIndex = 13;
            this.label69.Text = "Width of Top Flange of Equivalent Girder [bt]";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 327);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(254, 13);
            this.label63.TabIndex = 13;
            this.label63.Text = "Age of girder at the time of casting of SIDL";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(422, 33);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(293, 13);
            this.label67.TabIndex = 13;
            this.label67.Text = "Average Thickness of Top Slab [ds] (depth at L/2)";
            // 
            // txt_tab1_ds
            // 
            this.txt_tab1_ds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_ds.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab1_ds.Location = new System.Drawing.Point(740, 30);
            this.txt_tab1_ds.Name = "txt_tab1_ds";
            this.txt_tab1_ds.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_ds.TabIndex = 12;
            this.txt_tab1_ds.Text = "0.225";
            this.txt_tab1_ds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(422, 55);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(287, 13);
            this.label68.TabIndex = 13;
            this.label68.Text = "Thickness of Top Flange of Equivalent Girder [df]";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(6, 303);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(243, 13);
            this.label62.TabIndex = 13;
            this.label62.Text = "Strength concrete at the time of transfer ";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 278);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(205, 13);
            this.label51.TabIndex = 13;
            this.label51.Text = "Maturity of concrete for at transfer";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 402);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(138, 13);
            this.label66.TabIndex = 13;
            this.label66.Text = "Wearing coat thickness";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 251);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(181, 13);
            this.label50.TabIndex = 13;
            this.label50.Text = "Age of concrete for at transfer";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 376);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(305, 13);
            this.label65.TabIndex = 13;
            this.label65.Text = "Extra time dependent loss to be considered [T_loss]";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 200);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(201, 13);
            this.label49.TabIndex = 13;
            this.label49.Text = "Grade of Concrete of Girder [Fcu]";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 174);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(145, 13);
            this.label48.TabIndex = 13;
            this.label48.Text = "Depth of Box Girder [D]";
            // 
            // txt_tab1_alpha
            // 
            this.txt_tab1_alpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_alpha.Location = new System.Drawing.Point(740, 172);
            this.txt_tab1_alpha.Name = "txt_tab1_alpha";
            this.txt_tab1_alpha.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_alpha.TabIndex = 12;
            this.txt_tab1_alpha.Text = "0.0000117";
            this.txt_tab1_alpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactLL
            // 
            this.txt_tab1_FactLL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactLL.Location = new System.Drawing.Point(740, 146);
            this.txt_tab1_FactLL.Name = "txt_tab1_FactLL";
            this.txt_tab1_FactLL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactLL.TabIndex = 12;
            this.txt_tab1_FactLL.Text = "2.500";
            this.txt_tab1_FactLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactSIDL
            // 
            this.txt_tab1_FactSIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactSIDL.Location = new System.Drawing.Point(740, 122);
            this.txt_tab1_FactSIDL.Name = "txt_tab1_FactSIDL";
            this.txt_tab1_FactSIDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactSIDL.TabIndex = 12;
            this.txt_tab1_FactSIDL.Text = "2.0";
            this.txt_tab1_FactSIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactDL
            // 
            this.txt_tab1_FactDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactDL.Location = new System.Drawing.Point(740, 100);
            this.txt_tab1_FactDL.Name = "txt_tab1_FactDL";
            this.txt_tab1_FactDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactDL.TabIndex = 12;
            this.txt_tab1_FactDL.Text = "1.250";
            this.txt_tab1_FactDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_Mct_SIDL
            // 
            this.txt_tab1_Mct_SIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Mct_SIDL.Location = new System.Drawing.Point(311, 351);
            this.txt_tab1_Mct_SIDL.Name = "txt_tab1_Mct_SIDL";
            this.txt_tab1_Mct_SIDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Mct_SIDL.TabIndex = 12;
            this.txt_tab1_Mct_SIDL.Text = "100";
            this.txt_tab1_Mct_SIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_Mct_SIDL.TextChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // txt_tab1_bt
            // 
            this.txt_tab1_bt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_bt.Location = new System.Drawing.Point(740, 75);
            this.txt_tab1_bt.Name = "txt_tab1_bt";
            this.txt_tab1_bt.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_bt.TabIndex = 12;
            this.txt_tab1_bt.Text = "1.0";
            this.txt_tab1_bt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_agt_SIDL
            // 
            this.txt_tab1_agt_SIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_agt_SIDL.Location = new System.Drawing.Point(311, 325);
            this.txt_tab1_agt_SIDL.Name = "txt_tab1_agt_SIDL";
            this.txt_tab1_agt_SIDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_agt_SIDL.TabIndex = 12;
            this.txt_tab1_agt_SIDL.Text = "56";
            this.txt_tab1_agt_SIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_agt_SIDL.TextChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // txt_tab1_df
            // 
            this.txt_tab1_df.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_df.Location = new System.Drawing.Point(740, 52);
            this.txt_tab1_df.Name = "txt_tab1_df";
            this.txt_tab1_df.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_df.TabIndex = 12;
            this.txt_tab1_df.Text = "0.175";
            this.txt_tab1_df.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_sctt
            // 
            this.txt_tab1_sctt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_sctt.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab1_sctt.Location = new System.Drawing.Point(311, 301);
            this.txt_tab1_sctt.Name = "txt_tab1_sctt";
            this.txt_tab1_sctt.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_sctt.TabIndex = 12;
            this.txt_tab1_sctt.Text = "34.8";
            this.txt_tab1_sctt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_Mct
            // 
            this.txt_tab1_Mct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Mct.Location = new System.Drawing.Point(311, 276);
            this.txt_tab1_Mct.Name = "txt_tab1_Mct";
            this.txt_tab1_Mct.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Mct.TabIndex = 12;
            this.txt_tab1_Mct.Text = "87";
            this.txt_tab1_Mct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_Mct.TextChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // txt_tab1_wct
            // 
            this.txt_tab1_wct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_wct.Location = new System.Drawing.Point(311, 400);
            this.txt_tab1_wct.Name = "txt_tab1_wct";
            this.txt_tab1_wct.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_wct.TabIndex = 12;
            this.txt_tab1_wct.Text = "0.065";
            this.txt_tab1_wct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_act
            // 
            this.txt_tab1_act.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_act.Location = new System.Drawing.Point(311, 249);
            this.txt_tab1_act.Name = "txt_tab1_act";
            this.txt_tab1_act.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_act.TabIndex = 12;
            this.txt_tab1_act.Text = "14";
            this.txt_tab1_act.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_act.TextChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // txt_tab1_T_loss
            // 
            this.txt_tab1_T_loss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_T_loss.Location = new System.Drawing.Point(311, 374);
            this.txt_tab1_T_loss.Name = "txt_tab1_T_loss";
            this.txt_tab1_T_loss.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_T_loss.TabIndex = 12;
            this.txt_tab1_T_loss.Text = "20";
            this.txt_tab1_T_loss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_D
            // 
            this.txt_tab1_D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_D.Location = new System.Drawing.Point(311, 172);
            this.txt_tab1_D.Name = "txt_tab1_D";
            this.txt_tab1_D.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_D.TabIndex = 12;
            this.txt_tab1_D.Text = "2.50";
            this.txt_tab1_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 150);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(117, 13);
            this.label47.TabIndex = 11;
            this.label47.Text = "Width of deck [Dw]";
            // 
            // txt_tab1_DW
            // 
            this.txt_tab1_DW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_DW.ForeColor = System.Drawing.Color.Red;
            this.txt_tab1_DW.Location = new System.Drawing.Point(311, 148);
            this.txt_tab1_DW.Name = "txt_tab1_DW";
            this.txt_tab1_DW.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_DW.TabIndex = 10;
            this.txt_tab1_DW.Text = "9.75";
            this.txt_tab1_DW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 118);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(246, 26);
            this.label46.TabIndex = 9;
            this.label46.Text = "Effective Span (Centre to Centre spacing \r\nof Bearing) [L = Lo - 2 x L1]";
            // 
            // txt_tab1_L
            // 
            this.txt_tab1_L.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab1_L.Location = new System.Drawing.Point(311, 124);
            this.txt_tab1_L.Name = "txt_tab1_L";
            this.txt_tab1_L.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_L.TabIndex = 8;
            this.txt_tab1_L.Text = "47.75";
            this.txt_tab1_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 104);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(125, 13);
            this.label45.TabIndex = 7;
            this.label45.Text = "Expansion gap [exg]";
            // 
            // txt_tab1_exg
            // 
            this.txt_tab1_exg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_exg.Location = new System.Drawing.Point(311, 102);
            this.txt_tab1_exg.Name = "txt_tab1_exg";
            this.txt_tab1_exg.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_exg.TabIndex = 6;
            this.txt_tab1_exg.Text = "0.04";
            this.txt_tab1_exg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 73);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(241, 26);
            this.label44.TabIndex = 5;
            this.label44.Text = "Distance between Centre Line of Bearing\r\nand Centre Line of Expansion Joint [L2]";
            // 
            // txt_tab1_L2
            // 
            this.txt_tab1_L2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L2.Location = new System.Drawing.Point(311, 79);
            this.txt_tab1_L2.Name = "txt_tab1_L2";
            this.txt_tab1_L2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_L2.TabIndex = 4;
            this.txt_tab1_L2.Text = "0.50";
            this.txt_tab1_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 58);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(221, 13);
            this.label43.TabIndex = 3;
            this.label43.Text = "Girder end to bearing centre line [L1]";
            // 
            // txt_tab1_L1
            // 
            this.txt_tab1_L1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L1.Location = new System.Drawing.Point(311, 56);
            this.txt_tab1_L1.Name = "txt_tab1_L1";
            this.txt_tab1_L1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_L1.TabIndex = 2;
            this.txt_tab1_L1.Text = "0.50";
            this.txt_tab1_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_L1.TextChanged += new System.EventHandler(this.txt_tab1_Lo_TextChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 27);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(191, 26);
            this.label42.TabIndex = 1;
            this.label42.Text = "Overall Span (Centre to Centre \r\nspacing of exp. joint) [Lo]";
            // 
            // txt_tab1_Lo
            // 
            this.txt_tab1_Lo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Lo.ForeColor = System.Drawing.Color.Red;
            this.txt_tab1_Lo.Location = new System.Drawing.Point(311, 32);
            this.txt_tab1_Lo.Name = "txt_tab1_Lo";
            this.txt_tab1_Lo.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_Lo.TabIndex = 0;
            this.txt_tab1_Lo.Text = "48.75";
            this.txt_tab1_Lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab1_Lo.TextChanged += new System.EventHandler(this.txt_tab1_Lo_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox22);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(955, 601);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pre Stressing Data [Tab 2]";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.label245);
            this.groupBox22.Controls.Add(this.label235);
            this.groupBox22.Controls.Add(this.label237);
            this.groupBox22.Controls.Add(this.groupBox23);
            this.groupBox22.Controls.Add(this.groupBox24);
            this.groupBox22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox22.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox22.Location = new System.Drawing.Point(3, 3);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(949, 595);
            this.groupBox22.TabIndex = 1;
            this.groupBox22.TabStop = false;
            // 
            // label245
            // 
            this.label245.AutoSize = true;
            this.label245.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label245.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label245.ForeColor = System.Drawing.Color.Blue;
            this.label245.Location = new System.Drawing.Point(505, 8);
            this.label245.Name = "label245";
            this.label245.Size = new System.Drawing.Size(221, 18);
            this.label245.TabIndex = 174;
            this.label245.Text = "Blue Color values are calculated";
            // 
            // label235
            // 
            this.label235.AutoSize = true;
            this.label235.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label235.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label235.ForeColor = System.Drawing.Color.Red;
            this.label235.Location = new System.Drawing.Point(281, 8);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(218, 18);
            this.label235.TabIndex = 173;
            this.label235.Text = "Default Sample Data are shown";
            // 
            // label237
            // 
            this.label237.AutoSize = true;
            this.label237.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label237.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label237.ForeColor = System.Drawing.Color.Green;
            this.label237.Location = new System.Drawing.Point(140, 8);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(135, 18);
            this.label237.TabIndex = 172;
            this.label237.Text = "All User Input Data";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.cmb_tab2_nc);
            this.groupBox23.Controls.Add(this.label241);
            this.groupBox23.Controls.Add(this.cmb_tab2_strand_data);
            this.groupBox23.Controls.Add(this.label243);
            this.groupBox23.Controls.Add(this.label144);
            this.groupBox23.Controls.Add(this.label184);
            this.groupBox23.Controls.Add(this.txt_tab2_cable_area);
            this.groupBox23.Controls.Add(this.label182);
            this.groupBox23.Controls.Add(this.label180);
            this.groupBox23.Controls.Add(this.txt_tab2_nc_right);
            this.groupBox23.Controls.Add(this.txt_tab2_nc_left);
            this.groupBox23.Controls.Add(this.label137);
            this.groupBox23.Controls.Add(this.label136);
            this.groupBox23.Controls.Add(this.label168);
            this.groupBox23.Controls.Add(this.label143);
            this.groupBox23.Controls.Add(this.label141);
            this.groupBox23.Controls.Add(this.label140);
            this.groupBox23.Controls.Add(this.label167);
            this.groupBox23.Controls.Add(this.label166);
            this.groupBox23.Controls.Add(this.label139);
            this.groupBox23.Controls.Add(this.label171);
            this.groupBox23.Controls.Add(this.label170);
            this.groupBox23.Controls.Add(this.label138);
            this.groupBox23.Controls.Add(this.label165);
            this.groupBox23.Controls.Add(this.label163);
            this.groupBox23.Controls.Add(this.label169);
            this.groupBox23.Controls.Add(this.label186);
            this.groupBox23.Controls.Add(this.label175);
            this.groupBox23.Controls.Add(this.label172);
            this.groupBox23.Controls.Add(this.label162);
            this.groupBox23.Controls.Add(this.label135);
            this.groupBox23.Controls.Add(this.pictureBox3);
            this.groupBox23.Controls.Add(this.label77);
            this.groupBox23.Controls.Add(this.txt_tab2_D);
            this.groupBox23.Controls.Add(this.label103);
            this.groupBox23.Controls.Add(this.label189);
            this.groupBox23.Controls.Add(this.label188);
            this.groupBox23.Controls.Add(this.label101);
            this.groupBox23.Controls.Add(this.label178);
            this.groupBox23.Controls.Add(this.label102);
            this.groupBox23.Controls.Add(this.label96);
            this.groupBox23.Controls.Add(this.label95);
            this.groupBox23.Controls.Add(this.label94);
            this.groupBox23.Controls.Add(this.label93);
            this.groupBox23.Controls.Add(this.label92);
            this.groupBox23.Controls.Add(this.label91);
            this.groupBox23.Controls.Add(this.label90);
            this.groupBox23.Controls.Add(this.label89);
            this.groupBox23.Controls.Add(this.label88);
            this.groupBox23.Controls.Add(this.label87);
            this.groupBox23.Controls.Add(this.label86);
            this.groupBox23.Controls.Add(this.label85);
            this.groupBox23.Controls.Add(this.label84);
            this.groupBox23.Controls.Add(this.label78);
            this.groupBox23.Controls.Add(this.txt_tab2_A);
            this.groupBox23.Controls.Add(this.txt_tab2_cover1);
            this.groupBox23.Controls.Add(this.label79);
            this.groupBox23.Controls.Add(this.txt_tab2_Ns);
            this.groupBox23.Controls.Add(this.txt_tab2_cover2);
            this.groupBox23.Controls.Add(this.txt_tab2_Crst56);
            this.groupBox23.Controls.Add(this.txt_tab2_Resh56);
            this.groupBox23.Controls.Add(this.txt_tab2_Ec);
            this.groupBox23.Controls.Add(this.txt_tab2_Fcu);
            this.groupBox23.Controls.Add(this.txt_tab2_qd);
            this.groupBox23.Controls.Add(this.txt_tab2_td1);
            this.groupBox23.Controls.Add(this.txt_tab2_Re2);
            this.groupBox23.Controls.Add(this.txt_tab2_Re1);
            this.groupBox23.Controls.Add(this.txt_tab2_k);
            this.groupBox23.Controls.Add(this.txt_tab2_mu);
            this.groupBox23.Controls.Add(this.txt_tab2_s);
            this.groupBox23.Controls.Add(this.txt_tab2_Pj);
            this.groupBox23.Controls.Add(this.txt_tab2_Eps);
            this.groupBox23.Controls.Add(this.txt_tab2_Pn);
            this.groupBox23.Controls.Add(this.txt_tab2_Fu);
            this.groupBox23.Controls.Add(this.txt_tab2_Fy);
            this.groupBox23.Controls.Add(this.txt_tab2_Pu);
            this.groupBox23.Location = new System.Drawing.Point(7, 131);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(910, 463);
            this.groupBox23.TabIndex = 14;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Cable and Prestressing Data";
            // 
            // cmb_tab2_nc
            // 
            this.cmb_tab2_nc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tab2_nc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_tab2_nc.FormattingEnabled = true;
            this.cmb_tab2_nc.Items.AddRange(new object[] {
            "4 [2 LHS & 2 RHS]",
            "6 [3 LHS & 3 RHS]",
            "8 [4 LHS & 4 RHS]",
            "10 [5 LHS & 5 RHS]",
            "12 [6 LHS & 6 RHS]",
            "14 [7 LHS & 7 RHS]"});
            this.cmb_tab2_nc.Location = new System.Drawing.Point(675, 297);
            this.cmb_tab2_nc.Name = "cmb_tab2_nc";
            this.cmb_tab2_nc.Size = new System.Drawing.Size(158, 21);
            this.cmb_tab2_nc.TabIndex = 69;
            this.cmb_tab2_nc.SelectedIndexChanged += new System.EventHandler(this.cmb_tab2_nc_SelectedIndexChanged);
            // 
            // label241
            // 
            this.label241.AutoSize = true;
            this.label241.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label241.Location = new System.Drawing.Point(454, 300);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(161, 13);
            this.label241.TabIndex = 68;
            this.label241.Text = "Number of Cables used ";
            // 
            // cmb_tab2_strand_data
            // 
            this.cmb_tab2_strand_data.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tab2_strand_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_tab2_strand_data.FormattingEnabled = true;
            this.cmb_tab2_strand_data.Items.AddRange(new object[] {
            "19 K 13 [BS:5896]",
            "19 K 13 [GRADE 270]",
            "19 K 15 [BS:5896]",
            "19 K 15 [GRADE 270]"});
            this.cmb_tab2_strand_data.Location = new System.Drawing.Point(204, 206);
            this.cmb_tab2_strand_data.Name = "cmb_tab2_strand_data";
            this.cmb_tab2_strand_data.Size = new System.Drawing.Size(189, 21);
            this.cmb_tab2_strand_data.TabIndex = 67;
            this.cmb_tab2_strand_data.SelectedIndexChanged += new System.EventHandler(this.cmb_tab2_strand_data_SelectedIndexChanged);
            // 
            // label243
            // 
            this.label243.AutoSize = true;
            this.label243.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label243.Location = new System.Drawing.Point(5, 209);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(132, 13);
            this.label243.TabIndex = 65;
            this.label243.Text = "Select Strand Data ";
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.ForeColor = System.Drawing.Color.Blue;
            this.label144.Location = new System.Drawing.Point(5, 38);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(146, 13);
            this.label144.TabIndex = 21;
            this.label144.Text = "Sample Reference Table";
            // 
            // label184
            // 
            this.label184.AutoSize = true;
            this.label184.Location = new System.Drawing.Point(454, 366);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(176, 13);
            this.label184.TabIndex = 20;
            this.label184.Text = "Cross section area of Cables ";
            // 
            // txt_tab2_cable_area
            // 
            this.txt_tab2_cable_area.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cable_area.Location = new System.Drawing.Point(768, 364);
            this.txt_tab2_cable_area.Name = "txt_tab2_cable_area";
            this.txt_tab2_cable_area.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cable_area.TabIndex = 19;
            this.txt_tab2_cable_area.Text = "1875.3";
            this.txt_tab2_cable_area.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Location = new System.Drawing.Point(454, 322);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(223, 13);
            this.label182.TabIndex = 18;
            this.label182.Text = "Number of Cables used  for Left Side ";
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(454, 344);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(231, 13);
            this.label180.TabIndex = 17;
            this.label180.Text = "Number of Cables used  for Right Side ";
            // 
            // txt_tab2_nc_right
            // 
            this.txt_tab2_nc_right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_nc_right.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_nc_right.Location = new System.Drawing.Point(768, 342);
            this.txt_tab2_nc_right.Name = "txt_tab2_nc_right";
            this.txt_tab2_nc_right.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_nc_right.TabIndex = 16;
            this.txt_tab2_nc_right.Text = "7";
            this.txt_tab2_nc_right.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_nc_left
            // 
            this.txt_tab2_nc_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_nc_left.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_nc_left.Location = new System.Drawing.Point(768, 320);
            this.txt_tab2_nc_left.Name = "txt_tab2_nc_left";
            this.txt_tab2_nc_left.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_nc_left.TabIndex = 16;
            this.txt_tab2_nc_left.Text = "7";
            this.txt_tab2_nc_left.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(399, 275);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(37, 13);
            this.label137.TabIndex = 3;
            this.label137.Text = "kg/m";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(399, 253);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(46, 13);
            this.label136.TabIndex = 3;
            this.label136.Text = "sq.mm";
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(838, 113);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(28, 13);
            this.label168.TabIndex = 7;
            this.label168.Text = "day";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(397, 390);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(19, 13);
            this.label143.TabIndex = 7;
            this.label143.Text = "%";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(397, 367);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(30, 13);
            this.label141.TabIndex = 7;
            this.label141.Text = "Gpa";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(399, 345);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(22, 13);
            this.label140.TabIndex = 7;
            this.label140.Text = "kN";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(838, 90);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(30, 13);
            this.label167.TabIndex = 7;
            this.label167.Text = "Mpa";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(838, 66);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(30, 13);
            this.label166.TabIndex = 7;
            this.label166.Text = "Mpa";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(398, 323);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(30, 13);
            this.label139.TabIndex = 7;
            this.label139.Text = "Mpa";
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(838, 180);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(30, 13);
            this.label171.TabIndex = 7;
            this.label171.Text = "Mpa";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(838, 159);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(30, 13);
            this.label170.TabIndex = 7;
            this.label170.Text = "Mpa";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(399, 299);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(30, 13);
            this.label138.TabIndex = 7;
            this.label138.Text = "Mpa";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(838, 44);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(66, 13);
            this.label165.TabIndex = 3;
            this.label165.Text = "per radian";
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(838, 22);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(66, 13);
            this.label163.TabIndex = 3;
            this.label163.Text = "per radian";
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(838, 136);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(29, 13);
            this.label169.TabIndex = 3;
            this.label169.Text = "mm";
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(837, 366);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(46, 13);
            this.label186.TabIndex = 3;
            this.label186.Text = "sq.mm";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(838, 274);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(29, 13);
            this.label175.TabIndex = 3;
            this.label175.Text = "mm";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(838, 254);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(29, 13);
            this.label172.TabIndex = 3;
            this.label172.Text = "mm";
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(837, 388);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(29, 13);
            this.label162.TabIndex = 3;
            this.label162.Text = "mm";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(398, 230);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(29, 13);
            this.label135.TabIndex = 3;
            this.label135.Text = "mm";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(-1, 38);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(409, 165);
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.ForeColor = System.Drawing.Color.Blue;
            this.label77.Location = new System.Drawing.Point(6, 236);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(137, 13);
            this.label77.TabIndex = 13;
            this.label77.Text = "Nominal Diameter  [D]";
            // 
            // txt_tab2_D
            // 
            this.txt_tab2_D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_D.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_D.Location = new System.Drawing.Point(323, 228);
            this.txt_tab2_D.Name = "txt_tab2_D";
            this.txt_tab2_D.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_D.TabIndex = 12;
            this.txt_tab2_D.Text = "15.2";
            this.txt_tab2_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(454, 254);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(49, 13);
            this.label103.TabIndex = 13;
            this.label103.Text = "Cover1";
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(454, 231);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(258, 13);
            this.label189.TabIndex = 13;
            this.label189.Text = "Creep Strain at 56 days / 10 Mpa [Crst56]  ";
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Location = new System.Drawing.Point(454, 208);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(275, 13);
            this.label188.TabIndex = 13;
            this.label188.Text = "Residual Shrinkage Strain at 56 days [Resh56]";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(454, 182);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(308, 13);
            this.label101.TabIndex = 13;
            this.label101.Text = "Elastic Modulus of Concrete [Ec] = 5000 x Sqrt(Fcu) ";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(6, 16);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(143, 13);
            this.label178.TabIndex = 13;
            this.label178.Text = "Number of Strands [Ns]";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(454, 276);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(230, 13);
            this.label102.TabIndex = 13;
            this.label102.Text = "Cover2  (must be Cover 2 > Cover 1 )";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(454, 159);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(135, 13);
            this.label96.TabIndex = 13;
            this.label96.Text = "Concrete Grade [Fcu] ";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(454, 142);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(211, 13);
            this.label95.TabIndex = 13;
            this.label95.Text = "Diameter of Prestressing Duct [qd] ";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(454, 119);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(295, 13);
            this.label94.TabIndex = 13;
            this.label94.Text = "Age of Concrete for First Stage Prestressing  [td1]";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(454, 96);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(291, 13);
            this.label93.TabIndex = 13;
            this.label93.Text = "Relaxation of Prestressing Steel at 50% uts [Re2]";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(454, 72);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(291, 13);
            this.label92.TabIndex = 13;
            this.label92.Text = "Relaxation of Prestressing Steel at 70% uts [Re1]";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(454, 50);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(180, 13);
            this.label91.TabIndex = 13;
            this.label91.Text = "Wobble Friction coefficient [k] ";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(454, 22);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(153, 13);
            this.label90.TabIndex = 13;
            this.label90.Text = "Coefficient of Friction [µ] ";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(454, 388);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(134, 13);
            this.label89.TabIndex = 13;
            this.label89.Text = "Slip at Jacking End [s]";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 390);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(306, 13);
            this.label88.TabIndex = 13;
            this.label88.Text = "Jacking Force at Transfer (% of Breaking Load) [Pj] ";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 367);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(131, 13);
            this.label87.TabIndex = 13;
            this.label87.Text = "Elastic Modulus [Eps] ";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.ForeColor = System.Drawing.Color.Blue;
            this.label86.Location = new System.Drawing.Point(6, 345);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(176, 13);
            this.label86.TabIndex = 13;
            this.label86.Text = "Minimum Breaking Load [Pn] ";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.ForeColor = System.Drawing.Color.Blue;
            this.label85.Location = new System.Drawing.Point(6, 323);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(130, 13);
            this.label85.TabIndex = 13;
            this.label85.Text = "Tensile Strength [Fu] ";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.ForeColor = System.Drawing.Color.Blue;
            this.label84.Location = new System.Drawing.Point(6, 305);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(118, 13);
            this.label84.TabIndex = 13;
            this.label84.Text = "Yield Strength [Fy] ";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.ForeColor = System.Drawing.Color.Blue;
            this.label78.Location = new System.Drawing.Point(6, 282);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(115, 13);
            this.label78.TabIndex = 13;
            this.label78.Text = "Nominal mass [Pu]";
            // 
            // txt_tab2_A
            // 
            this.txt_tab2_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_A.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_A.Location = new System.Drawing.Point(323, 251);
            this.txt_tab2_A.Name = "txt_tab2_A";
            this.txt_tab2_A.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_A.TabIndex = 12;
            this.txt_tab2_A.Text = "140.0";
            this.txt_tab2_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_cover1
            // 
            this.txt_tab2_cover1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cover1.Location = new System.Drawing.Point(768, 252);
            this.txt_tab2_cover1.Name = "txt_tab2_cover1";
            this.txt_tab2_cover1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cover1.TabIndex = 12;
            this.txt_tab2_cover1.Text = "130";
            this.txt_tab2_cover1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.ForeColor = System.Drawing.Color.Blue;
            this.label79.Location = new System.Drawing.Point(6, 259);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(110, 13);
            this.label79.TabIndex = 13;
            this.label79.Text = "Nominal Area [A] ";
            // 
            // txt_tab2_Ns
            // 
            this.txt_tab2_Ns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Ns.Location = new System.Drawing.Point(323, 14);
            this.txt_tab2_Ns.Name = "txt_tab2_Ns";
            this.txt_tab2_Ns.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Ns.TabIndex = 12;
            this.txt_tab2_Ns.Text = "19";
            this.txt_tab2_Ns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_cover2
            // 
            this.txt_tab2_cover2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cover2.Location = new System.Drawing.Point(768, 274);
            this.txt_tab2_cover2.Name = "txt_tab2_cover2";
            this.txt_tab2_cover2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cover2.TabIndex = 12;
            this.txt_tab2_cover2.Text = "250";
            this.txt_tab2_cover2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Crst56
            // 
            this.txt_tab2_Crst56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Crst56.Location = new System.Drawing.Point(768, 229);
            this.txt_tab2_Crst56.Name = "txt_tab2_Crst56";
            this.txt_tab2_Crst56.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Crst56.TabIndex = 12;
            this.txt_tab2_Crst56.Text = " 0.00040";
            this.txt_tab2_Crst56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Resh56
            // 
            this.txt_tab2_Resh56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Resh56.Location = new System.Drawing.Point(768, 206);
            this.txt_tab2_Resh56.Name = "txt_tab2_Resh56";
            this.txt_tab2_Resh56.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Resh56.TabIndex = 12;
            this.txt_tab2_Resh56.Text = " 0.00015";
            this.txt_tab2_Resh56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Ec
            // 
            this.txt_tab2_Ec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Ec.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Ec.Location = new System.Drawing.Point(768, 180);
            this.txt_tab2_Ec.Name = "txt_tab2_Ec";
            this.txt_tab2_Ec.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Ec.TabIndex = 12;
            this.txt_tab2_Ec.Text = "31622.8";
            this.txt_tab2_Ec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Fcu
            // 
            this.txt_tab2_Fcu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Fcu.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Fcu.Location = new System.Drawing.Point(768, 157);
            this.txt_tab2_Fcu.Name = "txt_tab2_Fcu";
            this.txt_tab2_Fcu.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Fcu.TabIndex = 12;
            this.txt_tab2_Fcu.Text = "40";
            this.txt_tab2_Fcu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tab2_Fcu.TextChanged += new System.EventHandler(this.txt_tab2_Fcu_TextChanged);
            // 
            // txt_tab2_qd
            // 
            this.txt_tab2_qd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_qd.Location = new System.Drawing.Point(768, 134);
            this.txt_tab2_qd.Name = "txt_tab2_qd";
            this.txt_tab2_qd.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_qd.TabIndex = 12;
            this.txt_tab2_qd.Text = "110";
            this.txt_tab2_qd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_td1
            // 
            this.txt_tab2_td1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_td1.Location = new System.Drawing.Point(768, 111);
            this.txt_tab2_td1.Name = "txt_tab2_td1";
            this.txt_tab2_td1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_td1.TabIndex = 12;
            this.txt_tab2_td1.Text = "14";
            this.txt_tab2_td1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Re2
            // 
            this.txt_tab2_Re2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Re2.Location = new System.Drawing.Point(768, 88);
            this.txt_tab2_Re2.Name = "txt_tab2_Re2";
            this.txt_tab2_Re2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Re2.TabIndex = 12;
            this.txt_tab2_Re2.Text = "0.0";
            this.txt_tab2_Re2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Re1
            // 
            this.txt_tab2_Re1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Re1.Location = new System.Drawing.Point(768, 64);
            this.txt_tab2_Re1.Name = "txt_tab2_Re1";
            this.txt_tab2_Re1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Re1.TabIndex = 12;
            this.txt_tab2_Re1.Text = "35.0";
            this.txt_tab2_Re1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_k
            // 
            this.txt_tab2_k.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_k.Location = new System.Drawing.Point(768, 42);
            this.txt_tab2_k.Name = "txt_tab2_k";
            this.txt_tab2_k.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_k.TabIndex = 12;
            this.txt_tab2_k.Text = "0.002";
            this.txt_tab2_k.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_mu
            // 
            this.txt_tab2_mu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_mu.Location = new System.Drawing.Point(768, 20);
            this.txt_tab2_mu.Name = "txt_tab2_mu";
            this.txt_tab2_mu.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_mu.TabIndex = 12;
            this.txt_tab2_mu.Text = "0.17";
            this.txt_tab2_mu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_s
            // 
            this.txt_tab2_s.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_s.Location = new System.Drawing.Point(768, 386);
            this.txt_tab2_s.Name = "txt_tab2_s";
            this.txt_tab2_s.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_s.TabIndex = 12;
            this.txt_tab2_s.Text = "6.0.";
            this.txt_tab2_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Pj
            // 
            this.txt_tab2_Pj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Pj.Location = new System.Drawing.Point(323, 388);
            this.txt_tab2_Pj.Name = "txt_tab2_Pj";
            this.txt_tab2_Pj.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Pj.TabIndex = 12;
            this.txt_tab2_Pj.Text = "76.5";
            this.txt_tab2_Pj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Eps
            // 
            this.txt_tab2_Eps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Eps.Location = new System.Drawing.Point(323, 365);
            this.txt_tab2_Eps.Name = "txt_tab2_Eps";
            this.txt_tab2_Eps.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Eps.TabIndex = 12;
            this.txt_tab2_Eps.Text = "195";
            this.txt_tab2_Eps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Pn
            // 
            this.txt_tab2_Pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Pn.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Pn.Location = new System.Drawing.Point(323, 343);
            this.txt_tab2_Pn.Name = "txt_tab2_Pn";
            this.txt_tab2_Pn.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Pn.TabIndex = 12;
            this.txt_tab2_Pn.Text = "260.7";
            this.txt_tab2_Pn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Fu
            // 
            this.txt_tab2_Fu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Fu.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Fu.Location = new System.Drawing.Point(323, 321);
            this.txt_tab2_Fu.Name = "txt_tab2_Fu";
            this.txt_tab2_Fu.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Fu.TabIndex = 12;
            this.txt_tab2_Fu.Text = "1860.0";
            this.txt_tab2_Fu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Fy
            // 
            this.txt_tab2_Fy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Fy.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Fy.Location = new System.Drawing.Point(323, 297);
            this.txt_tab2_Fy.Name = "txt_tab2_Fy";
            this.txt_tab2_Fy.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Fy.TabIndex = 12;
            this.txt_tab2_Fy.Text = "1670.0";
            this.txt_tab2_Fy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Pu
            // 
            this.txt_tab2_Pu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Pu.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_Pu.Location = new System.Drawing.Point(323, 274);
            this.txt_tab2_Pu.Name = "txt_tab2_Pu";
            this.txt_tab2_Pu.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Pu.TabIndex = 12;
            this.txt_tab2_Pu.Text = "1.100";
            this.txt_tab2_Pu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.groupBox33);
            this.groupBox24.Controls.Add(this.groupBox30);
            this.groupBox24.Controls.Add(this.groupBox29);
            this.groupBox24.Controls.Add(this.groupBox28);
            this.groupBox24.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox24.Location = new System.Drawing.Point(7, 28);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(910, 107);
            this.groupBox24.TabIndex = 0;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "A)\t Construction Schedule and Prestressing Stages";
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.txt_tab2_rss_56);
            this.groupBox33.Controls.Add(this.txt_tab2_rss_14);
            this.groupBox33.Location = new System.Drawing.Point(751, 15);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(117, 92);
            this.groupBox33.TabIndex = 12;
            this.groupBox33.TabStop = false;
            this.groupBox33.Text = "Residual Shrinkage Strain";
            // 
            // txt_tab2_rss_56
            // 
            this.txt_tab2_rss_56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_rss_56.Location = new System.Drawing.Point(18, 65);
            this.txt_tab2_rss_56.Name = "txt_tab2_rss_56";
            this.txt_tab2_rss_56.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_rss_56.TabIndex = 4;
            this.txt_tab2_rss_56.Text = "0.00019";
            this.txt_tab2_rss_56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_rss_14
            // 
            this.txt_tab2_rss_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_rss_14.Location = new System.Drawing.Point(18, 43);
            this.txt_tab2_rss_14.Name = "txt_tab2_rss_14";
            this.txt_tab2_rss_14.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_rss_14.TabIndex = 2;
            this.txt_tab2_rss_14.Text = "0.00025";
            this.txt_tab2_rss_14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.label83);
            this.groupBox30.Controls.Add(this.label113);
            this.groupBox30.Controls.Add(this.label114);
            this.groupBox30.Controls.Add(this.txt_tab2_cwccb_fcj);
            this.groupBox30.Controls.Add(this.txt_tab2_ccbg_fcj);
            this.groupBox30.Controls.Add(this.txt_tab2_fsp_fcj);
            this.groupBox30.Location = new System.Drawing.Point(592, 15);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(153, 92);
            this.groupBox30.TabIndex = 11;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "[fcj] (N/sq.mm (Mpa))";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(89, 22);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(30, 13);
            this.label83.TabIndex = 3;
            this.label83.Text = "Mpa";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(89, 45);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(30, 13);
            this.label113.TabIndex = 5;
            this.label113.Text = "Mpa";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(89, 67);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(30, 13);
            this.label114.TabIndex = 7;
            this.label114.Text = "Mpa";
            // 
            // txt_tab2_cwccb_fcj
            // 
            this.txt_tab2_cwccb_fcj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cwccb_fcj.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_cwccb_fcj.Location = new System.Drawing.Point(18, 65);
            this.txt_tab2_cwccb_fcj.Name = "txt_tab2_cwccb_fcj";
            this.txt_tab2_cwccb_fcj.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cwccb_fcj.TabIndex = 4;
            this.txt_tab2_cwccb_fcj.Text = "40.0";
            this.txt_tab2_cwccb_fcj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_ccbg_fcj
            // 
            this.txt_tab2_ccbg_fcj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_ccbg_fcj.Location = new System.Drawing.Point(18, 20);
            this.txt_tab2_ccbg_fcj.Name = "txt_tab2_ccbg_fcj";
            this.txt_tab2_ccbg_fcj.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_ccbg_fcj.TabIndex = 0;
            this.txt_tab2_ccbg_fcj.Text = "0.0";
            this.txt_tab2_ccbg_fcj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_fsp_fcj
            // 
            this.txt_tab2_fsp_fcj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_fsp_fcj.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_fsp_fcj.Location = new System.Drawing.Point(18, 43);
            this.txt_tab2_fsp_fcj.Name = "txt_tab2_fsp_fcj";
            this.txt_tab2_fsp_fcj.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_fsp_fcj.TabIndex = 2;
            this.txt_tab2_fsp_fcj.Text = "34.8";
            this.txt_tab2_fsp_fcj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.label109);
            this.groupBox29.Controls.Add(this.label111);
            this.groupBox29.Controls.Add(this.label112);
            this.groupBox29.Controls.Add(this.txt_tab2_cwccb_day);
            this.groupBox29.Controls.Add(this.txt_tab2_ccbg_day);
            this.groupBox29.Controls.Add(this.txt_tab2_fsp_day);
            this.groupBox29.Location = new System.Drawing.Point(457, 15);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(129, 92);
            this.groupBox29.TabIndex = 10;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Day after casting ";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(86, 21);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(28, 13);
            this.label109.TabIndex = 3;
            this.label109.Text = "day";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(86, 44);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(28, 13);
            this.label111.TabIndex = 5;
            this.label111.Text = "day";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(86, 66);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(28, 13);
            this.label112.TabIndex = 7;
            this.label112.Text = "day";
            // 
            // txt_tab2_cwccb_day
            // 
            this.txt_tab2_cwccb_day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cwccb_day.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_cwccb_day.Location = new System.Drawing.Point(35, 64);
            this.txt_tab2_cwccb_day.Name = "txt_tab2_cwccb_day";
            this.txt_tab2_cwccb_day.Size = new System.Drawing.Size(45, 21);
            this.txt_tab2_cwccb_day.TabIndex = 4;
            this.txt_tab2_cwccb_day.Text = "56";
            this.txt_tab2_cwccb_day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_ccbg_day
            // 
            this.txt_tab2_ccbg_day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_ccbg_day.Location = new System.Drawing.Point(35, 19);
            this.txt_tab2_ccbg_day.Name = "txt_tab2_ccbg_day";
            this.txt_tab2_ccbg_day.Size = new System.Drawing.Size(45, 21);
            this.txt_tab2_ccbg_day.TabIndex = 0;
            this.txt_tab2_ccbg_day.Text = "0";
            this.txt_tab2_ccbg_day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_fsp_day
            // 
            this.txt_tab2_fsp_day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_fsp_day.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_fsp_day.Location = new System.Drawing.Point(35, 42);
            this.txt_tab2_fsp_day.Name = "txt_tab2_fsp_day";
            this.txt_tab2_fsp_day.Size = new System.Drawing.Size(45, 21);
            this.txt_tab2_fsp_day.TabIndex = 2;
            this.txt_tab2_fsp_day.Text = "14";
            this.txt_tab2_fsp_day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.label82);
            this.groupBox28.Controls.Add(this.label81);
            this.groupBox28.Controls.Add(this.label80);
            this.groupBox28.Location = new System.Drawing.Point(5, 13);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(446, 92);
            this.groupBox28.TabIndex = 9;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Job";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(26, 21);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(229, 13);
            this.label82.TabIndex = 3;
            this.label82.Text = "(i) Completion of casting of Box Girder";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(26, 42);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(145, 13);
            this.label81.TabIndex = 5;
            this.label81.Text = "(ii) First Stage Prestress";
            // 
            // label80
            // 
            this.label80.Location = new System.Drawing.Point(26, 66);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(385, 21);
            this.label80.TabIndex = 7;
            this.label80.Text = "(iii) Completion of Wearing Course && Crash Barrier  ";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox38);
            this.tabPage4.Controls.Add(this.tabControl6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(955, 601);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Reinforcement Data";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox38
            // 
            this.groupBox38.BackColor = System.Drawing.Color.Transparent;
            this.groupBox38.Controls.Add(this.groupBox37);
            this.groupBox38.Controls.Add(this.groupBox36);
            this.groupBox38.Controls.Add(this.groupBox35);
            this.groupBox38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox38.Location = new System.Drawing.Point(3, 497);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(949, 101);
            this.groupBox38.TabIndex = 6;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "Area for Steel Reinforcement";
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.label217);
            this.groupBox37.Controls.Add(this.label218);
            this.groupBox37.Controls.Add(this.label219);
            this.groupBox37.Controls.Add(this.label220);
            this.groupBox37.Controls.Add(this.txt_zn3_inn);
            this.groupBox37.Controls.Add(this.txt_zn3_out);
            this.groupBox37.Location = new System.Drawing.Point(625, 20);
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.Size = new System.Drawing.Size(301, 58);
            this.groupBox37.TabIndex = 6;
            this.groupBox37.TabStop = false;
            this.groupBox37.Text = "ZONE III : L/4 to L/2";
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.Location = new System.Drawing.Point(260, 36);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(41, 13);
            this.label217.TabIndex = 6;
            this.label217.Text = "Sq.m.";
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.Location = new System.Drawing.Point(100, 36);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(41, 13);
            this.label218.TabIndex = 6;
            this.label218.Text = "Sq.m.";
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Location = new System.Drawing.Point(179, 17);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(38, 13);
            this.label219.TabIndex = 6;
            this.label219.Text = "Inner";
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Location = new System.Drawing.Point(19, 17);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(39, 13);
            this.label220.TabIndex = 6;
            this.label220.Text = "Outer";
            // 
            // txt_zn3_inn
            // 
            this.txt_zn3_inn.Location = new System.Drawing.Point(166, 33);
            this.txt_zn3_inn.Name = "txt_zn3_inn";
            this.txt_zn3_inn.Size = new System.Drawing.Size(88, 21);
            this.txt_zn3_inn.TabIndex = 4;
            this.txt_zn3_inn.Text = "1260.0";
            this.txt_zn3_inn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_zn3_out
            // 
            this.txt_zn3_out.Location = new System.Drawing.Point(6, 33);
            this.txt_zn3_out.Name = "txt_zn3_out";
            this.txt_zn3_out.Size = new System.Drawing.Size(88, 21);
            this.txt_zn3_out.TabIndex = 4;
            this.txt_zn3_out.Text = "1480.0";
            this.txt_zn3_out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.label209);
            this.groupBox36.Controls.Add(this.label212);
            this.groupBox36.Controls.Add(this.label213);
            this.groupBox36.Controls.Add(this.label214);
            this.groupBox36.Controls.Add(this.txt_zn2_inn);
            this.groupBox36.Controls.Add(this.txt_zn2_out);
            this.groupBox36.Location = new System.Drawing.Point(313, 20);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Size = new System.Drawing.Size(301, 58);
            this.groupBox36.TabIndex = 6;
            this.groupBox36.TabStop = false;
            this.groupBox36.Text = "ZONE II : L/8 to L/4";
            // 
            // label209
            // 
            this.label209.AutoSize = true;
            this.label209.Location = new System.Drawing.Point(260, 36);
            this.label209.Name = "label209";
            this.label209.Size = new System.Drawing.Size(41, 13);
            this.label209.TabIndex = 6;
            this.label209.Text = "Sq.m.";
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.Location = new System.Drawing.Point(100, 36);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(41, 13);
            this.label212.TabIndex = 6;
            this.label212.Text = "Sq.m.";
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.Location = new System.Drawing.Point(179, 17);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(38, 13);
            this.label213.TabIndex = 6;
            this.label213.Text = "Inner";
            // 
            // label214
            // 
            this.label214.AutoSize = true;
            this.label214.Location = new System.Drawing.Point(19, 17);
            this.label214.Name = "label214";
            this.label214.Size = new System.Drawing.Size(39, 13);
            this.label214.TabIndex = 6;
            this.label214.Text = "Outer";
            // 
            // txt_zn2_inn
            // 
            this.txt_zn2_inn.Location = new System.Drawing.Point(166, 33);
            this.txt_zn2_inn.Name = "txt_zn2_inn";
            this.txt_zn2_inn.Size = new System.Drawing.Size(88, 21);
            this.txt_zn2_inn.TabIndex = 4;
            this.txt_zn2_inn.Text = "1260.0";
            this.txt_zn2_inn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_zn2_out
            // 
            this.txt_zn2_out.Location = new System.Drawing.Point(6, 33);
            this.txt_zn2_out.Name = "txt_zn2_out";
            this.txt_zn2_out.Size = new System.Drawing.Size(88, 21);
            this.txt_zn2_out.TabIndex = 4;
            this.txt_zn2_out.Text = "1480.0";
            this.txt_zn2_out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox35
            // 
            this.groupBox35.Controls.Add(this.label216);
            this.groupBox35.Controls.Add(this.label215);
            this.groupBox35.Controls.Add(this.label211);
            this.groupBox35.Controls.Add(this.label210);
            this.groupBox35.Controls.Add(this.txt_zn1_inn);
            this.groupBox35.Controls.Add(this.txt_zn1_out);
            this.groupBox35.Location = new System.Drawing.Point(6, 20);
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.Size = new System.Drawing.Size(301, 58);
            this.groupBox35.TabIndex = 6;
            this.groupBox35.TabStop = false;
            this.groupBox35.Text = "ZONE I : Support to L/8";
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Location = new System.Drawing.Point(260, 36);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(41, 13);
            this.label216.TabIndex = 6;
            this.label216.Text = "Sq.m.";
            // 
            // label215
            // 
            this.label215.AutoSize = true;
            this.label215.Location = new System.Drawing.Point(100, 36);
            this.label215.Name = "label215";
            this.label215.Size = new System.Drawing.Size(41, 13);
            this.label215.TabIndex = 6;
            this.label215.Text = "Sq.m.";
            // 
            // label211
            // 
            this.label211.AutoSize = true;
            this.label211.Location = new System.Drawing.Point(179, 17);
            this.label211.Name = "label211";
            this.label211.Size = new System.Drawing.Size(38, 13);
            this.label211.TabIndex = 6;
            this.label211.Text = "Inner";
            // 
            // label210
            // 
            this.label210.AutoSize = true;
            this.label210.Location = new System.Drawing.Point(19, 17);
            this.label210.Name = "label210";
            this.label210.Size = new System.Drawing.Size(39, 13);
            this.label210.TabIndex = 6;
            this.label210.Text = "Outer";
            // 
            // txt_zn1_inn
            // 
            this.txt_zn1_inn.Location = new System.Drawing.Point(166, 33);
            this.txt_zn1_inn.Name = "txt_zn1_inn";
            this.txt_zn1_inn.Size = new System.Drawing.Size(88, 21);
            this.txt_zn1_inn.TabIndex = 4;
            this.txt_zn1_inn.Text = "1260.0";
            this.txt_zn1_inn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_zn1_out
            // 
            this.txt_zn1_out.Location = new System.Drawing.Point(6, 33);
            this.txt_zn1_out.Name = "txt_zn1_out";
            this.txt_zn1_out.Size = new System.Drawing.Size(88, 21);
            this.txt_zn1_out.TabIndex = 4;
            this.txt_zn1_out.Text = "1480.0";
            this.txt_zn1_out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabControl6
            // 
            this.tabControl6.Controls.Add(this.tabPage3);
            this.tabControl6.Controls.Add(this.tabPage7);
            this.tabControl6.Controls.Add(this.tabPage8);
            this.tabControl6.Controls.Add(this.tabPage9);
            this.tabControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl6.Location = new System.Drawing.Point(3, 3);
            this.tabControl6.Name = "tabControl6";
            this.tabControl6.SelectedIndex = 0;
            this.tabControl6.Size = new System.Drawing.Size(949, 494);
            this.tabControl6.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage3.BackgroundImage")));
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(941, 468);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Details 1";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage7.BackgroundImage")));
            this.tabPage7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(941, 468);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Details 2";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage8.BackgroundImage")));
            this.tabPage8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(941, 468);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "Details 3";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage9.BackgroundImage")));
            this.tabPage9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(941, 468);
            this.tabPage9.TabIndex = 3;
            this.tabPage9.Text = "Details 4";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_segment_report);
            this.panel1.Controls.Add(this.btn_segment_process);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 630);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 33);
            this.panel1.TabIndex = 2;
            // 
            // btn_segment_report
            // 
            this.btn_segment_report.Location = new System.Drawing.Point(491, 2);
            this.btn_segment_report.Name = "btn_segment_report";
            this.btn_segment_report.Size = new System.Drawing.Size(206, 29);
            this.btn_segment_report.TabIndex = 0;
            this.btn_segment_report.Text = "Report";
            this.btn_segment_report.UseVisualStyleBackColor = true;
            this.btn_segment_report.Click += new System.EventHandler(this.btn_segment_report_Click);
            // 
            // btn_segment_process
            // 
            this.btn_segment_process.Location = new System.Drawing.Point(231, 2);
            this.btn_segment_process.Name = "btn_segment_process";
            this.btn_segment_process.Size = new System.Drawing.Size(206, 29);
            this.btn_segment_process.TabIndex = 0;
            this.btn_segment_process.Text = "Process";
            this.btn_segment_process.UseVisualStyleBackColor = true;
            this.btn_segment_process.Click += new System.EventHandler(this.btn_segment_process_Click);
            // 
            // tab_rcc_abutment
            // 
            this.tab_rcc_abutment.Controls.Add(this.tc_abutment);
            this.tab_rcc_abutment.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_abutment.Name = "tab_rcc_abutment";
            this.tab_rcc_abutment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_abutment.Size = new System.Drawing.Size(969, 666);
            this.tab_rcc_abutment.TabIndex = 5;
            this.tab_rcc_abutment.Text = "Design of RCC Abutment";
            this.tab_rcc_abutment.UseVisualStyleBackColor = true;
            // 
            // tab_pier
            // 
            this.tab_pier.Controls.Add(this.tc_pier);
            this.tab_pier.Location = new System.Drawing.Point(4, 22);
            this.tab_pier.Name = "tab_pier";
            this.tab_pier.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pier.Size = new System.Drawing.Size(969, 666);
            this.tab_pier.TabIndex = 4;
            this.tab_pier.Text = "Design of RCC Pier";
            this.tab_pier.UseVisualStyleBackColor = true;
            // 
            // tc_pier
            // 
            this.tc_pier.Controls.Add(this.tab_PierOpenLSM);
            this.tc_pier.Controls.Add(this.tab_PierPileLSM);
            this.tc_pier.Controls.Add(this.tab_PierWSM);
            this.tc_pier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_pier.Location = new System.Drawing.Point(3, 3);
            this.tc_pier.Name = "tc_pier";
            this.tc_pier.SelectedIndex = 0;
            this.tc_pier.Size = new System.Drawing.Size(963, 660);
            this.tc_pier.TabIndex = 121;
            // 
            // tab_PierPileLSM
            // 
            this.tab_PierPileLSM.Controls.Add(this.uC_PierDesignLSM1);
            this.tab_PierPileLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_PierPileLSM.Name = "tab_PierPileLSM";
            this.tab_PierPileLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PierPileLSM.Size = new System.Drawing.Size(955, 634);
            this.tab_PierPileLSM.TabIndex = 1;
            this.tab_PierPileLSM.Text = "Pier Design with Pile Foundation in LS";
            this.tab_PierPileLSM.UseVisualStyleBackColor = true;
            // 
            // uC_PierDesignLSM1
            // 
            this.uC_PierDesignLSM1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_PierDesignLSM1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_PierDesignLSM1.iApp = null;
            this.uC_PierDesignLSM1.Left_Span = "35";
            this.uC_PierDesignLSM1.Left_Span_Moment_Mx = "0.0";
            this.uC_PierDesignLSM1.Left_Span_Moment_Mz = "0.0";
            this.uC_PierDesignLSM1.Left_Span_Vertical_Load = "42";
            this.uC_PierDesignLSM1.Location = new System.Drawing.Point(3, 3);
            this.uC_PierDesignLSM1.Name = "uC_PierDesignLSM1";
            this.uC_PierDesignLSM1.Right_Span = "35";
            this.uC_PierDesignLSM1.Right_Span_Moment_Mx = "0.0";
            this.uC_PierDesignLSM1.Right_Span_Moment_Mz = "0.0";
            this.uC_PierDesignLSM1.Right_Span_Vertical_Load = "42";
            this.uC_PierDesignLSM1.Show_Note = false;
            this.uC_PierDesignLSM1.Show_Title = false;
            this.uC_PierDesignLSM1.Size = new System.Drawing.Size(949, 628);
            this.uC_PierDesignLSM1.TabIndex = 0;
            this.uC_PierDesignLSM1.Total_weight_of_superstructure = "460";
            // 
            // tab_PierWSM
            // 
            this.tab_PierWSM.Controls.Add(this.tabControl3);
            this.tab_PierWSM.Controls.Add(this.panel4);
            this.tab_PierWSM.Location = new System.Drawing.Point(4, 22);
            this.tab_PierWSM.Name = "tab_PierWSM";
            this.tab_PierWSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PierWSM.Size = new System.Drawing.Size(955, 634);
            this.tab_PierWSM.TabIndex = 0;
            this.tab_PierWSM.Text = "Design of RCC Pier in Working Stress Method";
            this.tab_PierWSM.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tab_des_form1);
            this.tabControl3.Controls.Add(this.tab_des_form2);
            this.tabControl3.Controls.Add(this.tab_des_Diagram);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControl3.Location = new System.Drawing.Point(3, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(949, 572);
            this.tabControl3.TabIndex = 10;
            // 
            // tab_des_form1
            // 
            this.tab_des_form1.Controls.Add(this.label246);
            this.tab_des_form1.Controls.Add(this.label231);
            this.tab_des_form1.Controls.Add(this.label232);
            this.tab_des_form1.Controls.Add(this.groupBox42);
            this.tab_des_form1.Controls.Add(this.label355);
            this.tab_des_form1.Controls.Add(this.groupBox39);
            this.tab_des_form1.Controls.Add(this.label362);
            this.tab_des_form1.Controls.Add(this.label363);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H7);
            this.tab_des_form1.Controls.Add(this.label364);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_gama_c);
            this.tab_des_form1.Controls.Add(this.label365);
            this.tab_des_form1.Controls.Add(this.label366);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_vehi_load);
            this.tab_des_form1.Controls.Add(this.label367);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NR);
            this.tab_des_form1.Controls.Add(this.label368);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NP);
            this.tab_des_form1.Controls.Add(this.label369);
            this.tab_des_form1.Controls.Add(this.groupBox40);
            this.tab_des_form1.Controls.Add(this.label389);
            this.tab_des_form1.Controls.Add(this.label390);
            this.tab_des_form1.Controls.Add(this.label391);
            this.tab_des_form1.Controls.Add(this.label392);
            this.tab_des_form1.Controls.Add(this.label393);
            this.tab_des_form1.Controls.Add(this.label394);
            this.tab_des_form1.Controls.Add(this.label395);
            this.tab_des_form1.Controls.Add(this.label396);
            this.tab_des_form1.Controls.Add(this.label397);
            this.tab_des_form1.Controls.Add(this.label398);
            this.tab_des_form1.Controls.Add(this.label403);
            this.tab_des_form1.Controls.Add(this.label404);
            this.tab_des_form1.Controls.Add(this.label405);
            this.tab_des_form1.Controls.Add(this.label406);
            this.tab_des_form1.Controls.Add(this.label407);
            this.tab_des_form1.Controls.Add(this.label408);
            this.tab_des_form1.Controls.Add(this.label409);
            this.tab_des_form1.Controls.Add(this.label410);
            this.tab_des_form1.Controls.Add(this.label411);
            this.tab_des_form1.Controls.Add(this.label412);
            this.tab_des_form1.Controls.Add(this.label413);
            this.tab_des_form1.Controls.Add(this.label414);
            this.tab_des_form1.Controls.Add(this.label415);
            this.tab_des_form1.Controls.Add(this.label416);
            this.tab_des_form1.Controls.Add(this.label417);
            this.tab_des_form1.Controls.Add(this.label418);
            this.tab_des_form1.Controls.Add(this.label419);
            this.tab_des_form1.Controls.Add(this.label420);
            this.tab_des_form1.Controls.Add(this.groupBox41);
            this.tab_des_form1.Controls.Add(this.groupBox43);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_overall_height);
            this.tab_des_form1.Controls.Add(this.label433);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier___B);
            this.tab_des_form1.Controls.Add(this.label434);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B13);
            this.tab_des_form1.Controls.Add(this.label435);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B12);
            this.tab_des_form1.Controls.Add(this.label436);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B11);
            this.tab_des_form1.Controls.Add(this.label437);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B10);
            this.tab_des_form1.Controls.Add(this.label438);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B9);
            this.tab_des_form1.Controls.Add(this.label439);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H6);
            this.tab_des_form1.Controls.Add(this.label440);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H5);
            this.tab_des_form1.Controls.Add(this.label441);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B8);
            this.tab_des_form1.Controls.Add(this.label445);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H4);
            this.tab_des_form1.Controls.Add(this.label446);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_H3);
            this.tab_des_form1.Controls.Add(this.label447);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B7);
            this.tab_des_form1.Controls.Add(this.label448);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_Form_Lev);
            this.tab_des_form1.Controls.Add(this.label449);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL5);
            this.tab_des_form1.Controls.Add(this.label450);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL4);
            this.tab_des_form1.Controls.Add(this.label451);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL3);
            this.tab_des_form1.Controls.Add(this.label452);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL2);
            this.tab_des_form1.Controls.Add(this.label453);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_RL1);
            this.tab_des_form1.Controls.Add(this.label454);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B6);
            this.tab_des_form1.Controls.Add(this.label455);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_B5);
            this.tab_des_form1.Controls.Add(this.label456);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_DS);
            this.tab_des_form1.Controls.Add(this.label457);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_DMG);
            this.tab_des_form1.Controls.Add(this.label458);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_NMG);
            this.tab_des_form1.Controls.Add(this.label459);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_Hp);
            this.tab_des_form1.Controls.Add(this.label460);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_Wp);
            this.tab_des_form1.Controls.Add(this.label461);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier__B);
            this.tab_des_form1.Controls.Add(this.label462);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_CW);
            this.tab_des_form1.Controls.Add(this.label463);
            this.tab_des_form1.Controls.Add(this.txt_RCC_Pier_L);
            this.tab_des_form1.Controls.Add(this.label464);
            this.tab_des_form1.Location = new System.Drawing.Point(4, 22);
            this.tab_des_form1.Name = "tab_des_form1";
            this.tab_des_form1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_form1.Size = new System.Drawing.Size(941, 546);
            this.tab_des_form1.TabIndex = 0;
            this.tab_des_form1.Text = "Design Input Data [Form1]";
            this.tab_des_form1.UseVisualStyleBackColor = true;
            // 
            // label246
            // 
            this.label246.AutoSize = true;
            this.label246.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label246.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label246.ForeColor = System.Drawing.Color.Blue;
            this.label246.Location = new System.Drawing.Point(637, 28);
            this.label246.Name = "label246";
            this.label246.Size = new System.Drawing.Size(221, 18);
            this.label246.TabIndex = 127;
            this.label246.Text = "Blue Color values are calculated";
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label231.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label231.ForeColor = System.Drawing.Color.Red;
            this.label231.Location = new System.Drawing.Point(516, 4);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(218, 18);
            this.label231.TabIndex = 126;
            this.label231.Text = "Default Sample Data are shown";
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label232.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label232.ForeColor = System.Drawing.Color.Green;
            this.label232.Location = new System.Drawing.Point(364, 4);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(135, 18);
            this.label232.TabIndex = 125;
            this.label232.Text = "All User Input Data";
            // 
            // groupBox42
            // 
            this.groupBox42.Controls.Add(this.txt_rcc_pier_m);
            this.groupBox42.Controls.Add(this.label117);
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
            this.groupBox42.Location = new System.Drawing.Point(572, 49);
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.Size = new System.Drawing.Size(364, 144);
            this.groupBox42.TabIndex = 115;
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
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(293, 43);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(67, 13);
            this.label117.TabIndex = 81;
            this.label117.Text = "N/sq.mm";
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
            // label355
            // 
            this.label355.AutoSize = true;
            this.label355.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label355.Location = new System.Drawing.Point(233, 242);
            this.label355.Name = "label355";
            this.label355.Size = new System.Drawing.Size(51, 14);
            this.label355.TabIndex = 107;
            this.label355.Text = "kN/cu.m";
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.label356);
            this.groupBox39.Controls.Add(this.txt_RCC_Pier_W1_supp_reac);
            this.groupBox39.Controls.Add(this.label357);
            this.groupBox39.Controls.Add(this.label358);
            this.groupBox39.Controls.Add(this.txt_RCC_Pier_Mz1);
            this.groupBox39.Controls.Add(this.label359);
            this.groupBox39.Controls.Add(this.label360);
            this.groupBox39.Controls.Add(this.txt_RCC_Pier_Mx1);
            this.groupBox39.Controls.Add(this.label361);
            this.groupBox39.Location = new System.Drawing.Point(572, 386);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(363, 131);
            this.groupBox39.TabIndex = 90;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "From Design Forces we have";
            // 
            // label356
            // 
            this.label356.AutoSize = true;
            this.label356.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label356.Location = new System.Drawing.Point(292, 22);
            this.label356.Name = "label356";
            this.label356.Size = new System.Drawing.Size(24, 13);
            this.label356.TabIndex = 104;
            this.label356.Text = "kN";
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
            // label357
            // 
            this.label357.AutoSize = true;
            this.label357.Location = new System.Drawing.Point(5, 22);
            this.label357.Name = "label357";
            this.label357.Size = new System.Drawing.Size(201, 13);
            this.label357.TabIndex = 105;
            this.label357.Text = "Total Support Reaction on The Pier [W1]";
            // 
            // label358
            // 
            this.label358.AutoSize = true;
            this.label358.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label358.Location = new System.Drawing.Point(292, 97);
            this.label358.Name = "label358";
            this.label358.Size = new System.Drawing.Size(42, 13);
            this.label358.TabIndex = 101;
            this.label358.Text = "kN-m";
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
            // label359
            // 
            this.label359.AutoSize = true;
            this.label359.Location = new System.Drawing.Point(7, 90);
            this.label359.Name = "label359";
            this.label359.Size = new System.Drawing.Size(134, 26);
            this.label359.TabIndex = 102;
            this.label359.Text = "Moment at Supports in \r\nTransverse Direction [Mz1]";
            // 
            // label360
            // 
            this.label360.AutoSize = true;
            this.label360.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label360.Location = new System.Drawing.Point(290, 63);
            this.label360.Name = "label360";
            this.label360.Size = new System.Drawing.Size(42, 13);
            this.label360.TabIndex = 98;
            this.label360.Text = "kN-m";
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
            // label361
            // 
            this.label361.AutoSize = true;
            this.label361.Location = new System.Drawing.Point(7, 54);
            this.label361.Name = "label361";
            this.label361.Size = new System.Drawing.Size(138, 26);
            this.label361.TabIndex = 99;
            this.label361.Text = "Moment at Supports in \r\nLongitudinal Direction [Mx1]";
            // 
            // label362
            // 
            this.label362.AutoSize = true;
            this.label362.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label362.ForeColor = System.Drawing.Color.Red;
            this.label362.Location = new System.Drawing.Point(13, 9);
            this.label362.Name = "label362";
            this.label362.Size = new System.Drawing.Size(334, 13);
            this.label362.TabIndex = 106;
            this.label362.Text = "Refer to the \'Diagram\' Tab for various Dimensions";
            // 
            // label363
            // 
            this.label363.AutoSize = true;
            this.label363.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label363.Location = new System.Drawing.Point(535, 334);
            this.label363.Name = "label363";
            this.label363.Size = new System.Drawing.Size(19, 13);
            this.label363.TabIndex = 103;
            this.label363.Text = "m";
            // 
            // txt_RCC_Pier_H7
            // 
            this.txt_RCC_Pier_H7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H7.Location = new System.Drawing.Point(476, 331);
            this.txt_RCC_Pier_H7.Name = "txt_RCC_Pier_H7";
            this.txt_RCC_Pier_H7.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H7.TabIndex = 31;
            this.txt_RCC_Pier_H7.Text = "6.560";
            this.txt_RCC_Pier_H7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label364
            // 
            this.label364.AutoSize = true;
            this.label364.Location = new System.Drawing.Point(282, 334);
            this.label364.Name = "label364";
            this.label364.Size = new System.Drawing.Size(121, 13);
            this.label364.TabIndex = 104;
            this.label364.Text = "Total Height of Pier [H7]";
            // 
            // txt_RCC_Pier_gama_c
            // 
            this.txt_RCC_Pier_gama_c.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_gama_c.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_gama_c.Location = new System.Drawing.Point(183, 236);
            this.txt_RCC_Pier_gama_c.Name = "txt_RCC_Pier_gama_c";
            this.txt_RCC_Pier_gama_c.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_gama_c.TabIndex = 8;
            this.txt_RCC_Pier_gama_c.Text = "24";
            this.txt_RCC_Pier_gama_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label365
            // 
            this.label365.AutoSize = true;
            this.label365.Location = new System.Drawing.Point(17, 239);
            this.label365.Name = "label365";
            this.label365.Size = new System.Drawing.Size(148, 13);
            this.label365.TabIndex = 101;
            this.label365.Text = "Unit Weight of Concrete [γ_c]";
            // 
            // label366
            // 
            this.label366.AutoSize = true;
            this.label366.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label366.Location = new System.Drawing.Point(862, 523);
            this.label366.Name = "label366";
            this.label366.Size = new System.Drawing.Size(24, 13);
            this.label366.TabIndex = 98;
            this.label366.Text = "kN";
            // 
            // txt_RCC_Pier_vehi_load
            // 
            this.txt_RCC_Pier_vehi_load.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_vehi_load.Location = new System.Drawing.Point(805, 520);
            this.txt_RCC_Pier_vehi_load.Name = "txt_RCC_Pier_vehi_load";
            this.txt_RCC_Pier_vehi_load.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_vehi_load.TabIndex = 36;
            this.txt_RCC_Pier_vehi_load.Text = "1000.0";
            this.txt_RCC_Pier_vehi_load.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label367
            // 
            this.label367.AutoSize = true;
            this.label367.Location = new System.Drawing.Point(578, 523);
            this.label367.Name = "label367";
            this.label367.Size = new System.Drawing.Size(119, 13);
            this.label367.TabIndex = 99;
            this.label367.Text = "Total Vehicle Live Load";
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
            // label368
            // 
            this.label368.AutoSize = true;
            this.label368.Location = new System.Drawing.Point(17, 381);
            this.label368.Name = "label368";
            this.label368.Size = new System.Drawing.Size(91, 13);
            this.label368.TabIndex = 96;
            this.label368.Text = "Nos. of Row [NR]";
            // 
            // txt_RCC_Pier_NP
            // 
            this.txt_RCC_Pier_NP.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_NP.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_NP.Location = new System.Drawing.Point(183, 355);
            this.txt_RCC_Pier_NP.Name = "txt_RCC_Pier_NP";
            this.txt_RCC_Pier_NP.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_NP.TabIndex = 10;
            this.txt_RCC_Pier_NP.Text = "2";
            this.txt_RCC_Pier_NP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label369
            // 
            this.label369.AutoSize = true;
            this.label369.Location = new System.Drawing.Point(16, 358);
            this.label369.Name = "label369";
            this.label369.Size = new System.Drawing.Size(157, 13);
            this.label369.TabIndex = 93;
            this.label369.Text = "Nos. of Pedestals per Row [NP]";
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.label315);
            this.groupBox40.Controls.Add(this.label316);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_tdia);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_rdia);
            this.groupBox40.Controls.Add(this.label321);
            this.groupBox40.Controls.Add(this.label507);
            this.groupBox40.Controls.Add(this.label372);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_fck_2);
            this.groupBox40.Controls.Add(this.label373);
            this.groupBox40.Controls.Add(this.label374);
            this.groupBox40.Controls.Add(this.label375);
            this.groupBox40.Controls.Add(this.label376);
            this.groupBox40.Controls.Add(this.label377);
            this.groupBox40.Controls.Add(this.label378);
            this.groupBox40.Controls.Add(this.label379);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_fy2);
            this.groupBox40.Controls.Add(this.label380);
            this.groupBox40.Controls.Add(this.label381);
            this.groupBox40.Controls.Add(this.label382);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_D);
            this.groupBox40.Controls.Add(this.label383);
            this.groupBox40.Controls.Add(this.label384);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_b);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_d_dash);
            this.groupBox40.Controls.Add(this.label385);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_p1);
            this.groupBox40.Controls.Add(this.label386);
            this.groupBox40.Controls.Add(this.txt_RCC_Pier_p2);
            this.groupBox40.Controls.Add(this.label387);
            this.groupBox40.Location = new System.Drawing.Point(572, 196);
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.Size = new System.Drawing.Size(363, 188);
            this.groupBox40.TabIndex = 90;
            this.groupBox40.TabStop = false;
            this.groupBox40.Text = "Steel Reinforcements";
            // 
            // label315
            // 
            this.label315.AutoSize = true;
            this.label315.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label315.Location = new System.Drawing.Point(293, 166);
            this.label315.Name = "label315";
            this.label315.Size = new System.Drawing.Size(31, 13);
            this.label315.TabIndex = 107;
            this.label315.Text = "mm";
            // 
            // label316
            // 
            this.label316.AutoSize = true;
            this.label316.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label316.Location = new System.Drawing.Point(293, 141);
            this.label316.Name = "label316";
            this.label316.Size = new System.Drawing.Size(31, 13);
            this.label316.TabIndex = 108;
            this.label316.Text = "mm";
            // 
            // txt_RCC_Pier_tdia
            // 
            this.txt_RCC_Pier_tdia.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_tdia.Location = new System.Drawing.Point(235, 163);
            this.txt_RCC_Pier_tdia.Name = "txt_RCC_Pier_tdia";
            this.txt_RCC_Pier_tdia.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_tdia.TabIndex = 105;
            this.txt_RCC_Pier_tdia.Text = "12";
            this.txt_RCC_Pier_tdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_RCC_Pier_rdia
            // 
            this.txt_RCC_Pier_rdia.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_rdia.Location = new System.Drawing.Point(235, 138);
            this.txt_RCC_Pier_rdia.Name = "txt_RCC_Pier_rdia";
            this.txt_RCC_Pier_rdia.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_rdia.TabIndex = 106;
            this.txt_RCC_Pier_rdia.Text = "32";
            this.txt_RCC_Pier_rdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label321
            // 
            this.label321.AutoSize = true;
            this.label321.Location = new System.Drawing.Point(9, 166);
            this.label321.Name = "label321";
            this.label321.Size = new System.Drawing.Size(147, 13);
            this.label321.TabIndex = 109;
            this.label321.Text = "Lateral Tie Bar Diameter [tdia]";
            // 
            // label507
            // 
            this.label507.AutoSize = true;
            this.label507.Location = new System.Drawing.Point(9, 141);
            this.label507.Name = "label507";
            this.label507.Size = new System.Drawing.Size(166, 13);
            this.label507.TabIndex = 110;
            this.label507.Text = "Reinforcement Bar Diameter [rdia]";
            // 
            // label372
            // 
            this.label372.AutoSize = true;
            this.label372.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label372.Location = new System.Drawing.Point(208, 223);
            this.label372.Name = "label372";
            this.label372.Size = new System.Drawing.Size(17, 13);
            this.label372.TabIndex = 92;
            this.label372.Text = "M";
            this.label372.Visible = false;
            // 
            // txt_RCC_Pier_fck_2
            // 
            this.txt_RCC_Pier_fck_2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_fck_2.Location = new System.Drawing.Point(234, 220);
            this.txt_RCC_Pier_fck_2.Name = "txt_RCC_Pier_fck_2";
            this.txt_RCC_Pier_fck_2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_fck_2.TabIndex = 3;
            this.txt_RCC_Pier_fck_2.Text = "30";
            this.txt_RCC_Pier_fck_2.Visible = false;
            // 
            // label373
            // 
            this.label373.AutoSize = true;
            this.label373.Location = new System.Drawing.Point(7, 215);
            this.label373.Name = "label373";
            this.label373.Size = new System.Drawing.Size(104, 13);
            this.label373.TabIndex = 74;
            this.label373.Text = "Concrete grade [fck]";
            this.label373.Visible = false;
            // 
            // label374
            // 
            this.label374.AutoSize = true;
            this.label374.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label374.Location = new System.Drawing.Point(292, 64);
            this.label374.Name = "label374";
            this.label374.Size = new System.Drawing.Size(31, 13);
            this.label374.TabIndex = 22;
            this.label374.Text = "mm";
            // 
            // label375
            // 
            this.label375.AutoSize = true;
            this.label375.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label375.Location = new System.Drawing.Point(208, 247);
            this.label375.Name = "label375";
            this.label375.Size = new System.Drawing.Size(23, 13);
            this.label375.TabIndex = 91;
            this.label375.Text = "Fe";
            this.label375.Visible = false;
            // 
            // label376
            // 
            this.label376.AutoSize = true;
            this.label376.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label376.Location = new System.Drawing.Point(292, 40);
            this.label376.Name = "label376";
            this.label376.Size = new System.Drawing.Size(21, 13);
            this.label376.TabIndex = 22;
            this.label376.Text = "%";
            // 
            // label377
            // 
            this.label377.AutoSize = true;
            this.label377.Location = new System.Drawing.Point(7, 247);
            this.label377.Name = "label377";
            this.label377.Size = new System.Drawing.Size(78, 13);
            this.label377.TabIndex = 76;
            this.label377.Text = "Steel grade [fy]";
            this.label377.Visible = false;
            // 
            // label378
            // 
            this.label378.AutoSize = true;
            this.label378.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label378.Location = new System.Drawing.Point(292, 13);
            this.label378.Name = "label378";
            this.label378.Size = new System.Drawing.Size(21, 13);
            this.label378.TabIndex = 22;
            this.label378.Text = "%";
            // 
            // label379
            // 
            this.label379.AutoSize = true;
            this.label379.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label379.Location = new System.Drawing.Point(293, 117);
            this.label379.Name = "label379";
            this.label379.Size = new System.Drawing.Size(31, 13);
            this.label379.TabIndex = 22;
            this.label379.Text = "mm";
            // 
            // txt_RCC_Pier_fy2
            // 
            this.txt_RCC_Pier_fy2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_fy2.Location = new System.Drawing.Point(234, 244);
            this.txt_RCC_Pier_fy2.Name = "txt_RCC_Pier_fy2";
            this.txt_RCC_Pier_fy2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_fy2.TabIndex = 4;
            this.txt_RCC_Pier_fy2.Text = "415";
            this.txt_RCC_Pier_fy2.Visible = false;
            // 
            // label380
            // 
            this.label380.AutoSize = true;
            this.label380.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label380.Location = new System.Drawing.Point(293, 90);
            this.label380.Name = "label380";
            this.label380.Size = new System.Drawing.Size(31, 13);
            this.label380.TabIndex = 22;
            this.label380.Text = "mm";
            // 
            // label381
            // 
            this.label381.AutoSize = true;
            this.label381.Location = new System.Drawing.Point(7, 90);
            this.label381.Name = "label381";
            this.label381.Size = new System.Drawing.Size(195, 13);
            this.label381.TabIndex = 78;
            this.label381.Text = "Width of Pier in Transverse direction [D]";
            // 
            // label382
            // 
            this.label382.AutoSize = true;
            this.label382.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label382.Location = new System.Drawing.Point(293, 245);
            this.label382.Name = "label382";
            this.label382.Size = new System.Drawing.Size(68, 13);
            this.label382.TabIndex = 22;
            this.label382.Text = "N/Sq.mm";
            this.label382.Visible = false;
            // 
            // txt_RCC_Pier_D
            // 
            this.txt_RCC_Pier_D.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_D.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_D.Location = new System.Drawing.Point(234, 87);
            this.txt_RCC_Pier_D.Name = "txt_RCC_Pier_D";
            this.txt_RCC_Pier_D.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_D.TabIndex = 5;
            this.txt_RCC_Pier_D.Text = "2500";
            this.txt_RCC_Pier_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label383
            // 
            this.label383.AutoSize = true;
            this.label383.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label383.Location = new System.Drawing.Point(293, 221);
            this.label383.Name = "label383";
            this.label383.Size = new System.Drawing.Size(55, 13);
            this.label383.TabIndex = 22;
            this.label383.Text = "N/sq.m";
            this.label383.Visible = false;
            // 
            // label384
            // 
            this.label384.AutoSize = true;
            this.label384.Location = new System.Drawing.Point(6, 117);
            this.label384.Name = "label384";
            this.label384.Size = new System.Drawing.Size(197, 13);
            this.label384.TabIndex = 82;
            this.label384.Text = "Width of Pier in Longitudinal direction [b]";
            // 
            // txt_RCC_Pier_b
            // 
            this.txt_RCC_Pier_b.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_b.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_b.Location = new System.Drawing.Point(234, 114);
            this.txt_RCC_Pier_b.Name = "txt_RCC_Pier_b";
            this.txt_RCC_Pier_b.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_b.TabIndex = 6;
            this.txt_RCC_Pier_b.Text = "2500";
            this.txt_RCC_Pier_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_RCC_Pier_d_dash
            // 
            this.txt_RCC_Pier_d_dash.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_d_dash.Location = new System.Drawing.Point(234, 61);
            this.txt_RCC_Pier_d_dash.Name = "txt_RCC_Pier_d_dash";
            this.txt_RCC_Pier_d_dash.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_d_dash.TabIndex = 2;
            this.txt_RCC_Pier_d_dash.Text = "40";
            this.txt_RCC_Pier_d_dash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label385
            // 
            this.label385.AutoSize = true;
            this.label385.Location = new System.Drawing.Point(6, 17);
            this.label385.Name = "label385";
            this.label385.Size = new System.Drawing.Size(187, 13);
            this.label385.TabIndex = 68;
            this.label385.Text = "Standard Minimum Reinforcement [p1]";
            // 
            // txt_RCC_Pier_p1
            // 
            this.txt_RCC_Pier_p1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_p1.Location = new System.Drawing.Point(234, 10);
            this.txt_RCC_Pier_p1.Name = "txt_RCC_Pier_p1";
            this.txt_RCC_Pier_p1.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_p1.TabIndex = 0;
            this.txt_RCC_Pier_p1.Text = "0.8";
            this.txt_RCC_Pier_p1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label386
            // 
            this.label386.AutoSize = true;
            this.label386.Location = new System.Drawing.Point(6, 40);
            this.label386.Name = "label386";
            this.label386.Size = new System.Drawing.Size(156, 13);
            this.label386.TabIndex = 70;
            this.label386.Text = "Design Trial Reinforcement [p2]";
            // 
            // txt_RCC_Pier_p2
            // 
            this.txt_RCC_Pier_p2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_p2.Location = new System.Drawing.Point(234, 36);
            this.txt_RCC_Pier_p2.Name = "txt_RCC_Pier_p2";
            this.txt_RCC_Pier_p2.Size = new System.Drawing.Size(52, 20);
            this.txt_RCC_Pier_p2.TabIndex = 1;
            this.txt_RCC_Pier_p2.Text = "1.5";
            this.txt_RCC_Pier_p2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label387
            // 
            this.label387.AutoSize = true;
            this.label387.Location = new System.Drawing.Point(6, 58);
            this.label387.Name = "label387";
            this.label387.Size = new System.Drawing.Size(125, 13);
            this.label387.TabIndex = 72;
            this.label387.Text = "Reinforcement Cover [d’]";
            // 
            // label389
            // 
            this.label389.AutoSize = true;
            this.label389.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label389.Location = new System.Drawing.Point(533, 524);
            this.label389.Name = "label389";
            this.label389.Size = new System.Drawing.Size(19, 13);
            this.label389.TabIndex = 22;
            this.label389.Text = "m";
            // 
            // label390
            // 
            this.label390.AutoSize = true;
            this.label390.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label390.Location = new System.Drawing.Point(533, 491);
            this.label390.Name = "label390";
            this.label390.Size = new System.Drawing.Size(19, 13);
            this.label390.TabIndex = 22;
            this.label390.Text = "m";
            // 
            // label391
            // 
            this.label391.AutoSize = true;
            this.label391.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label391.Location = new System.Drawing.Point(533, 459);
            this.label391.Name = "label391";
            this.label391.Size = new System.Drawing.Size(19, 13);
            this.label391.TabIndex = 22;
            this.label391.Text = "m";
            // 
            // label392
            // 
            this.label392.AutoSize = true;
            this.label392.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label392.Location = new System.Drawing.Point(535, 104);
            this.label392.Name = "label392";
            this.label392.Size = new System.Drawing.Size(19, 13);
            this.label392.TabIndex = 22;
            this.label392.Text = "m";
            // 
            // label393
            // 
            this.label393.AutoSize = true;
            this.label393.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label393.Location = new System.Drawing.Point(535, 133);
            this.label393.Name = "label393";
            this.label393.Size = new System.Drawing.Size(19, 13);
            this.label393.TabIndex = 22;
            this.label393.Text = "m";
            // 
            // label394
            // 
            this.label394.AutoSize = true;
            this.label394.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label394.Location = new System.Drawing.Point(535, 156);
            this.label394.Name = "label394";
            this.label394.Size = new System.Drawing.Size(19, 13);
            this.label394.TabIndex = 22;
            this.label394.Text = "m";
            // 
            // label395
            // 
            this.label395.AutoSize = true;
            this.label395.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label395.Location = new System.Drawing.Point(535, 180);
            this.label395.Name = "label395";
            this.label395.Size = new System.Drawing.Size(19, 13);
            this.label395.TabIndex = 22;
            this.label395.Text = "m";
            // 
            // label396
            // 
            this.label396.AutoSize = true;
            this.label396.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label396.Location = new System.Drawing.Point(535, 209);
            this.label396.Name = "label396";
            this.label396.Size = new System.Drawing.Size(19, 13);
            this.label396.TabIndex = 22;
            this.label396.Text = "m";
            // 
            // label397
            // 
            this.label397.AutoSize = true;
            this.label397.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label397.Location = new System.Drawing.Point(535, 231);
            this.label397.Name = "label397";
            this.label397.Size = new System.Drawing.Size(19, 13);
            this.label397.TabIndex = 22;
            this.label397.Text = "m";
            // 
            // label398
            // 
            this.label398.AutoSize = true;
            this.label398.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label398.Location = new System.Drawing.Point(535, 256);
            this.label398.Name = "label398";
            this.label398.Size = new System.Drawing.Size(19, 13);
            this.label398.TabIndex = 22;
            this.label398.Text = "m";
            // 
            // label403
            // 
            this.label403.AutoSize = true;
            this.label403.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label403.Location = new System.Drawing.Point(535, 285);
            this.label403.Name = "label403";
            this.label403.Size = new System.Drawing.Size(19, 13);
            this.label403.TabIndex = 22;
            this.label403.Text = "m";
            // 
            // label404
            // 
            this.label404.AutoSize = true;
            this.label404.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label404.Location = new System.Drawing.Point(535, 308);
            this.label404.Name = "label404";
            this.label404.Size = new System.Drawing.Size(19, 13);
            this.label404.TabIndex = 22;
            this.label404.Text = "m";
            // 
            // label405
            // 
            this.label405.AutoSize = true;
            this.label405.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label405.Location = new System.Drawing.Point(237, 497);
            this.label405.Name = "label405";
            this.label405.Size = new System.Drawing.Size(19, 13);
            this.label405.TabIndex = 22;
            this.label405.Text = "m";
            this.label405.Visible = false;
            // 
            // label406
            // 
            this.label406.AutoSize = true;
            this.label406.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label406.Location = new System.Drawing.Point(237, 524);
            this.label406.Name = "label406";
            this.label406.Size = new System.Drawing.Size(19, 13);
            this.label406.TabIndex = 22;
            this.label406.Text = "m";
            // 
            // label407
            // 
            this.label407.AutoSize = true;
            this.label407.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label407.Location = new System.Drawing.Point(535, 358);
            this.label407.Name = "label407";
            this.label407.Size = new System.Drawing.Size(19, 13);
            this.label407.TabIndex = 22;
            this.label407.Text = "m";
            // 
            // label408
            // 
            this.label408.AutoSize = true;
            this.label408.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label408.Location = new System.Drawing.Point(535, 385);
            this.label408.Name = "label408";
            this.label408.Size = new System.Drawing.Size(19, 13);
            this.label408.TabIndex = 22;
            this.label408.Text = "m";
            // 
            // label409
            // 
            this.label409.AutoSize = true;
            this.label409.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label409.Location = new System.Drawing.Point(535, 414);
            this.label409.Name = "label409";
            this.label409.Size = new System.Drawing.Size(19, 13);
            this.label409.TabIndex = 22;
            this.label409.Text = "m";
            // 
            // label410
            // 
            this.label410.AutoSize = true;
            this.label410.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label410.Location = new System.Drawing.Point(535, 436);
            this.label410.Name = "label410";
            this.label410.Size = new System.Drawing.Size(19, 13);
            this.label410.TabIndex = 22;
            this.label410.Text = "m";
            // 
            // label411
            // 
            this.label411.AutoSize = true;
            this.label411.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label411.Location = new System.Drawing.Point(535, 78);
            this.label411.Name = "label411";
            this.label411.Size = new System.Drawing.Size(19, 13);
            this.label411.TabIndex = 22;
            this.label411.Text = "m";
            // 
            // label412
            // 
            this.label412.AutoSize = true;
            this.label412.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label412.Location = new System.Drawing.Point(536, 54);
            this.label412.Name = "label412";
            this.label412.Size = new System.Drawing.Size(19, 13);
            this.label412.TabIndex = 22;
            this.label412.Text = "m";
            // 
            // label413
            // 
            this.label413.AutoSize = true;
            this.label413.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label413.Location = new System.Drawing.Point(536, 28);
            this.label413.Name = "label413";
            this.label413.Size = new System.Drawing.Size(19, 13);
            this.label413.TabIndex = 22;
            this.label413.Text = "m";
            // 
            // label414
            // 
            this.label414.AutoSize = true;
            this.label414.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label414.Location = new System.Drawing.Point(237, 213);
            this.label414.Name = "label414";
            this.label414.Size = new System.Drawing.Size(18, 14);
            this.label414.TabIndex = 22;
            this.label414.Text = "m";
            // 
            // label415
            // 
            this.label415.AutoSize = true;
            this.label415.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label415.Location = new System.Drawing.Point(237, 187);
            this.label415.Name = "label415";
            this.label415.Size = new System.Drawing.Size(18, 14);
            this.label415.TabIndex = 22;
            this.label415.Text = "m";
            // 
            // label416
            // 
            this.label416.AutoSize = true;
            this.label416.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label416.Location = new System.Drawing.Point(237, 136);
            this.label416.Name = "label416";
            this.label416.Size = new System.Drawing.Size(18, 14);
            this.label416.TabIndex = 22;
            this.label416.Text = "m";
            // 
            // label417
            // 
            this.label417.AutoSize = true;
            this.label417.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label417.Location = new System.Drawing.Point(237, 109);
            this.label417.Name = "label417";
            this.label417.Size = new System.Drawing.Size(18, 14);
            this.label417.TabIndex = 22;
            this.label417.Text = "m";
            // 
            // label418
            // 
            this.label418.AutoSize = true;
            this.label418.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label418.Location = new System.Drawing.Point(237, 83);
            this.label418.Name = "label418";
            this.label418.Size = new System.Drawing.Size(18, 14);
            this.label418.TabIndex = 22;
            this.label418.Text = "m";
            // 
            // label419
            // 
            this.label419.AutoSize = true;
            this.label419.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label419.Location = new System.Drawing.Point(237, 58);
            this.label419.Name = "label419";
            this.label419.Size = new System.Drawing.Size(18, 14);
            this.label419.TabIndex = 22;
            this.label419.Text = "m";
            // 
            // label420
            // 
            this.label420.AutoSize = true;
            this.label420.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label420.Location = new System.Drawing.Point(237, 28);
            this.label420.Name = "label420";
            this.label420.Size = new System.Drawing.Size(18, 14);
            this.label420.TabIndex = 22;
            this.label420.Text = "m";
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.label421);
            this.groupBox41.Controls.Add(this.label422);
            this.groupBox41.Controls.Add(this.label423);
            this.groupBox41.Controls.Add(this.txt_RCC_Pier_H2);
            this.groupBox41.Controls.Add(this.label424);
            this.groupBox41.Controls.Add(this.txt_RCC_Pier_B4);
            this.groupBox41.Controls.Add(this.label425);
            this.groupBox41.Controls.Add(this.txt_RCC_Pier_B3);
            this.groupBox41.Controls.Add(this.label426);
            this.groupBox41.Location = new System.Drawing.Point(3, 400);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Size = new System.Drawing.Size(267, 89);
            this.groupBox41.TabIndex = 12;
            this.groupBox41.TabStop = false;
            this.groupBox41.Text = "Size of Bearings (B3 x B4 x H2)";
            // 
            // label421
            // 
            this.label421.AutoSize = true;
            this.label421.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label421.Location = new System.Drawing.Point(233, 68);
            this.label421.Name = "label421";
            this.label421.Size = new System.Drawing.Size(19, 13);
            this.label421.TabIndex = 22;
            this.label421.Text = "m";
            // 
            // label422
            // 
            this.label422.AutoSize = true;
            this.label422.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label422.Location = new System.Drawing.Point(233, 42);
            this.label422.Name = "label422";
            this.label422.Size = new System.Drawing.Size(19, 13);
            this.label422.TabIndex = 22;
            this.label422.Text = "m";
            // 
            // label423
            // 
            this.label423.AutoSize = true;
            this.label423.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label423.Location = new System.Drawing.Point(233, 16);
            this.label423.Name = "label423";
            this.label423.Size = new System.Drawing.Size(19, 13);
            this.label423.TabIndex = 22;
            this.label423.Text = "m";
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
            // label424
            // 
            this.label424.AutoSize = true;
            this.label424.Location = new System.Drawing.Point(10, 72);
            this.label424.Name = "label424";
            this.label424.Size = new System.Drawing.Size(21, 13);
            this.label424.TabIndex = 20;
            this.label424.Text = "H2";
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
            // label425
            // 
            this.label425.AutoSize = true;
            this.label425.Location = new System.Drawing.Point(10, 46);
            this.label425.Name = "label425";
            this.label425.Size = new System.Drawing.Size(20, 13);
            this.label425.TabIndex = 18;
            this.label425.Text = "B4";
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
            // label426
            // 
            this.label426.AutoSize = true;
            this.label426.Location = new System.Drawing.Point(11, 20);
            this.label426.Name = "label426";
            this.label426.Size = new System.Drawing.Size(20, 13);
            this.label426.TabIndex = 16;
            this.label426.Text = "B3";
            // 
            // groupBox43
            // 
            this.groupBox43.Controls.Add(this.label427);
            this.groupBox43.Controls.Add(this.label428);
            this.groupBox43.Controls.Add(this.label429);
            this.groupBox43.Controls.Add(this.txt_RCC_Pier_H1);
            this.groupBox43.Controls.Add(this.label430);
            this.groupBox43.Controls.Add(this.txt_RCC_Pier_B2);
            this.groupBox43.Controls.Add(this.label431);
            this.groupBox43.Controls.Add(this.txt_RCC_Pier_B1);
            this.groupBox43.Controls.Add(this.label432);
            this.groupBox43.Location = new System.Drawing.Point(6, 259);
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.Size = new System.Drawing.Size(267, 90);
            this.groupBox43.TabIndex = 9;
            this.groupBox43.TabStop = false;
            this.groupBox43.Text = "Size of Pedestals (B1 x B2 x H1)";
            // 
            // label427
            // 
            this.label427.AutoSize = true;
            this.label427.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label427.Location = new System.Drawing.Point(227, 68);
            this.label427.Name = "label427";
            this.label427.Size = new System.Drawing.Size(19, 13);
            this.label427.TabIndex = 22;
            this.label427.Text = "m";
            // 
            // label428
            // 
            this.label428.AutoSize = true;
            this.label428.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label428.Location = new System.Drawing.Point(227, 46);
            this.label428.Name = "label428";
            this.label428.Size = new System.Drawing.Size(19, 13);
            this.label428.TabIndex = 22;
            this.label428.Text = "m";
            // 
            // label429
            // 
            this.label429.AutoSize = true;
            this.label429.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label429.Location = new System.Drawing.Point(227, 20);
            this.label429.Name = "label429";
            this.label429.Size = new System.Drawing.Size(19, 13);
            this.label429.TabIndex = 22;
            this.label429.Text = "m";
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
            // label430
            // 
            this.label430.AutoSize = true;
            this.label430.Location = new System.Drawing.Point(10, 72);
            this.label430.Name = "label430";
            this.label430.Size = new System.Drawing.Size(21, 13);
            this.label430.TabIndex = 20;
            this.label430.Text = "H1";
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
            // label431
            // 
            this.label431.AutoSize = true;
            this.label431.Location = new System.Drawing.Point(10, 46);
            this.label431.Name = "label431";
            this.label431.Size = new System.Drawing.Size(20, 13);
            this.label431.TabIndex = 18;
            this.label431.Text = "B2";
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
            // label432
            // 
            this.label432.AutoSize = true;
            this.label432.Location = new System.Drawing.Point(11, 20);
            this.label432.Name = "label432";
            this.label432.Size = new System.Drawing.Size(20, 13);
            this.label432.TabIndex = 16;
            this.label432.Text = "B1";
            // 
            // txt_RCC_Pier_overall_height
            // 
            this.txt_RCC_Pier_overall_height.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_overall_height.Location = new System.Drawing.Point(476, 522);
            this.txt_RCC_Pier_overall_height.Name = "txt_RCC_Pier_overall_height";
            this.txt_RCC_Pier_overall_height.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_overall_height.TabIndex = 39;
            this.txt_RCC_Pier_overall_height.Text = "7.760";
            this.txt_RCC_Pier_overall_height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label433
            // 
            this.label433.AutoSize = true;
            this.label433.Location = new System.Drawing.Point(281, 516);
            this.label433.Name = "label433";
            this.label433.Size = new System.Drawing.Size(152, 26);
            this.label433.TabIndex = 62;
            this.label433.Text = "Overall Height of Substructure \r\n([H7 + H5 + H6])";
            // 
            // txt_RCC_Pier___B
            // 
            this.txt_RCC_Pier___B.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier___B.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier___B.Location = new System.Drawing.Point(476, 489);
            this.txt_RCC_Pier___B.Name = "txt_RCC_Pier___B";
            this.txt_RCC_Pier___B.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier___B.TabIndex = 38;
            this.txt_RCC_Pier___B.Text = "3.0";
            this.txt_RCC_Pier___B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label434
            // 
            this.label434.AutoSize = true;
            this.label434.Location = new System.Drawing.Point(281, 488);
            this.label434.Name = "label434";
            this.label434.Size = new System.Drawing.Size(145, 26);
            this.label434.TabIndex = 60;
            this.label434.Text = "Pier Cap width in Transverse \r\nDirection [B14]";
            // 
            // txt_RCC_Pier_B13
            // 
            this.txt_RCC_Pier_B13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B13.Location = new System.Drawing.Point(476, 456);
            this.txt_RCC_Pier_B13.Name = "txt_RCC_Pier_B13";
            this.txt_RCC_Pier_B13.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B13.TabIndex = 37;
            this.txt_RCC_Pier_B13.Text = "3.0";
            this.txt_RCC_Pier_B13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label435
            // 
            this.label435.AutoSize = true;
            this.label435.Location = new System.Drawing.Point(282, 459);
            this.label435.Name = "label435";
            this.label435.Size = new System.Drawing.Size(149, 26);
            this.label435.TabIndex = 58;
            this.label435.Text = "Pier Cap width in Longitudinal \r\nDirection [B13]";
            // 
            // txt_RCC_Pier_B12
            // 
            this.txt_RCC_Pier_B12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B12.Location = new System.Drawing.Point(476, 433);
            this.txt_RCC_Pier_B12.Name = "txt_RCC_Pier_B12";
            this.txt_RCC_Pier_B12.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B12.TabIndex = 35;
            this.txt_RCC_Pier_B12.Text = "5.0";
            this.txt_RCC_Pier_B12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label436
            // 
            this.label436.AutoSize = true;
            this.label436.Location = new System.Drawing.Point(282, 436);
            this.label436.Name = "label436";
            this.label436.Size = new System.Drawing.Size(183, 13);
            this.label436.TabIndex = 56;
            this.label436.Text = "Transverse width of Pier at Top [B12]";
            // 
            // txt_RCC_Pier_B11
            // 
            this.txt_RCC_Pier_B11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B11.Location = new System.Drawing.Point(476, 407);
            this.txt_RCC_Pier_B11.Name = "txt_RCC_Pier_B11";
            this.txt_RCC_Pier_B11.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B11.TabIndex = 34;
            this.txt_RCC_Pier_B11.Text = "1.0";
            this.txt_RCC_Pier_B11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label437
            // 
            this.label437.AutoSize = true;
            this.label437.Location = new System.Drawing.Point(282, 410);
            this.label437.Name = "label437";
            this.label437.Size = new System.Drawing.Size(187, 13);
            this.label437.TabIndex = 54;
            this.label437.Text = "Longitudinal width of Pier at Top [B11]";
            // 
            // txt_RCC_Pier_B10
            // 
            this.txt_RCC_Pier_B10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B10.Location = new System.Drawing.Point(476, 381);
            this.txt_RCC_Pier_B10.Name = "txt_RCC_Pier_B10";
            this.txt_RCC_Pier_B10.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B10.TabIndex = 33;
            this.txt_RCC_Pier_B10.Text = "5.0";
            this.txt_RCC_Pier_B10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label438
            // 
            this.label438.AutoSize = true;
            this.label438.Location = new System.Drawing.Point(282, 388);
            this.label438.Name = "label438";
            this.label438.Size = new System.Drawing.Size(188, 13);
            this.label438.TabIndex = 52;
            this.label438.Text = "Transverse width of Pier at Base [B10]";
            // 
            // txt_RCC_Pier_B9
            // 
            this.txt_RCC_Pier_B9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B9.Location = new System.Drawing.Point(476, 355);
            this.txt_RCC_Pier_B9.Name = "txt_RCC_Pier_B9";
            this.txt_RCC_Pier_B9.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B9.TabIndex = 32;
            this.txt_RCC_Pier_B9.Text = "0.9";
            this.txt_RCC_Pier_B9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label439
            // 
            this.label439.AutoSize = true;
            this.label439.Location = new System.Drawing.Point(282, 358);
            this.label439.Name = "label439";
            this.label439.Size = new System.Drawing.Size(186, 13);
            this.label439.TabIndex = 50;
            this.label439.Text = "Longitudinal width of Pier at Base [B9]";
            // 
            // txt_RCC_Pier_H6
            // 
            this.txt_RCC_Pier_H6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H6.Location = new System.Drawing.Point(476, 305);
            this.txt_RCC_Pier_H6.Name = "txt_RCC_Pier_H6";
            this.txt_RCC_Pier_H6.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H6.TabIndex = 30;
            this.txt_RCC_Pier_H6.Text = "0.6";
            this.txt_RCC_Pier_H6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label440
            // 
            this.label440.AutoSize = true;
            this.label440.Location = new System.Drawing.Point(282, 312);
            this.label440.Name = "label440";
            this.label440.Size = new System.Drawing.Size(152, 13);
            this.label440.TabIndex = 48;
            this.label440.Text = "Varying Depth of Pier Cap [H6]";
            // 
            // txt_RCC_Pier_H5
            // 
            this.txt_RCC_Pier_H5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H5.Location = new System.Drawing.Point(476, 279);
            this.txt_RCC_Pier_H5.Name = "txt_RCC_Pier_H5";
            this.txt_RCC_Pier_H5.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H5.TabIndex = 29;
            this.txt_RCC_Pier_H5.Text = "0.6";
            this.txt_RCC_Pier_H5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label441
            // 
            this.label441.AutoSize = true;
            this.label441.Location = new System.Drawing.Point(282, 286);
            this.label441.Name = "label441";
            this.label441.Size = new System.Drawing.Size(151, 13);
            this.label441.TabIndex = 46;
            this.label441.Text = "Straight depth of Pier Cap [H5]";
            // 
            // txt_RCC_Pier_B8
            // 
            this.txt_RCC_Pier_B8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B8.Location = new System.Drawing.Point(476, 253);
            this.txt_RCC_Pier_B8.Name = "txt_RCC_Pier_B8";
            this.txt_RCC_Pier_B8.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B8.TabIndex = 25;
            this.txt_RCC_Pier_B8.Text = "0.15";
            this.txt_RCC_Pier_B8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label445
            // 
            this.label445.AutoSize = true;
            this.label445.Location = new System.Drawing.Point(282, 247);
            this.label445.Name = "label445";
            this.label445.Size = new System.Drawing.Size(130, 26);
            this.label445.TabIndex = 38;
            this.label445.Text = "P.C.C. Projection under \r\nFooting on either side [B8]";
            // 
            // txt_RCC_Pier_H4
            // 
            this.txt_RCC_Pier_H4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H4.Location = new System.Drawing.Point(476, 228);
            this.txt_RCC_Pier_H4.Name = "txt_RCC_Pier_H4";
            this.txt_RCC_Pier_H4.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H4.TabIndex = 24;
            this.txt_RCC_Pier_H4.Text = "0.60";
            this.txt_RCC_Pier_H4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label446
            // 
            this.label446.AutoSize = true;
            this.label446.Location = new System.Drawing.Point(282, 231);
            this.label446.Name = "label446";
            this.label446.Size = new System.Drawing.Size(147, 13);
            this.label446.TabIndex = 36;
            this.label446.Text = "Varying Depth of Footing [H4]";
            // 
            // txt_RCC_Pier_H3
            // 
            this.txt_RCC_Pier_H3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_H3.Location = new System.Drawing.Point(476, 202);
            this.txt_RCC_Pier_H3.Name = "txt_RCC_Pier_H3";
            this.txt_RCC_Pier_H3.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_H3.TabIndex = 23;
            this.txt_RCC_Pier_H3.Text = "0.60";
            this.txt_RCC_Pier_H3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label447
            // 
            this.label447.AutoSize = true;
            this.label447.Location = new System.Drawing.Point(282, 202);
            this.label447.Name = "label447";
            this.label447.Size = new System.Drawing.Size(148, 13);
            this.label447.TabIndex = 34;
            this.label447.Text = "Straight Depth of Footing [H3]";
            // 
            // txt_RCC_Pier_B7
            // 
            this.txt_RCC_Pier_B7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B7.Location = new System.Drawing.Point(476, 177);
            this.txt_RCC_Pier_B7.Name = "txt_RCC_Pier_B7";
            this.txt_RCC_Pier_B7.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_B7.TabIndex = 22;
            this.txt_RCC_Pier_B7.Text = "9.50";
            this.txt_RCC_Pier_B7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label448
            // 
            this.label448.AutoSize = true;
            this.label448.Location = new System.Drawing.Point(282, 180);
            this.label448.Name = "label448";
            this.label448.Size = new System.Drawing.Size(107, 13);
            this.label448.TabIndex = 32;
            this.label448.Text = "Width of Footing [B7]";
            // 
            // txt_RCC_Pier_Form_Lev
            // 
            this.txt_RCC_Pier_Form_Lev.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Form_Lev.Location = new System.Drawing.Point(476, 153);
            this.txt_RCC_Pier_Form_Lev.Name = "txt_RCC_Pier_Form_Lev";
            this.txt_RCC_Pier_Form_Lev.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_Form_Lev.TabIndex = 21;
            this.txt_RCC_Pier_Form_Lev.Text = "531.505";
            this.txt_RCC_Pier_Form_Lev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label449
            // 
            this.label449.AutoSize = true;
            this.label449.Location = new System.Drawing.Point(282, 156);
            this.label449.Name = "label449";
            this.label449.Size = new System.Drawing.Size(187, 13);
            this.label449.TabIndex = 30;
            this.label449.Text = "Formation Level [RL1+d1+d2+H1+H2]";
            // 
            // txt_RCC_Pier_RL5
            // 
            this.txt_RCC_Pier_RL5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL5.Location = new System.Drawing.Point(476, 127);
            this.txt_RCC_Pier_RL5.Name = "txt_RCC_Pier_RL5";
            this.txt_RCC_Pier_RL5.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL5.TabIndex = 20;
            this.txt_RCC_Pier_RL5.Text = "520.42";
            this.txt_RCC_Pier_RL5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label450
            // 
            this.label450.AutoSize = true;
            this.label450.Location = new System.Drawing.Point(282, 130);
            this.label450.Name = "label450";
            this.label450.Size = new System.Drawing.Size(142, 13);
            this.label450.TabIndex = 28;
            this.label450.Text = "R.L. at Footing Bottom [RL5]";
            // 
            // txt_RCC_Pier_RL4
            // 
            this.txt_RCC_Pier_RL4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL4.Location = new System.Drawing.Point(476, 101);
            this.txt_RCC_Pier_RL4.Name = "txt_RCC_Pier_RL4";
            this.txt_RCC_Pier_RL4.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL4.TabIndex = 19;
            this.txt_RCC_Pier_RL4.Text = "521.62";
            this.txt_RCC_Pier_RL4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label451
            // 
            this.label451.AutoSize = true;
            this.label451.Location = new System.Drawing.Point(282, 104);
            this.label451.Name = "label451";
            this.label451.Size = new System.Drawing.Size(128, 13);
            this.label451.TabIndex = 26;
            this.label451.Text = "R.L. at Footing Top [RL4]";
            // 
            // txt_RCC_Pier_RL3
            // 
            this.txt_RCC_Pier_RL3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_RL3.Location = new System.Drawing.Point(476, 75);
            this.txt_RCC_Pier_RL3.Name = "txt_RCC_Pier_RL3";
            this.txt_RCC_Pier_RL3.Size = new System.Drawing.Size(53, 20);
            this.txt_RCC_Pier_RL3.TabIndex = 18;
            this.txt_RCC_Pier_RL3.Text = "523.417";
            this.txt_RCC_Pier_RL3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label452
            // 
            this.label452.AutoSize = true;
            this.label452.Location = new System.Drawing.Point(282, 82);
            this.label452.Name = "label452";
            this.label452.Size = new System.Drawing.Size(139, 13);
            this.label452.TabIndex = 24;
            this.label452.Text = "Existing Ground Level [RL3]";
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
            // label453
            // 
            this.label453.AutoSize = true;
            this.label453.Location = new System.Drawing.Point(282, 54);
            this.label453.Name = "label453";
            this.label453.Size = new System.Drawing.Size(145, 13);
            this.label453.TabIndex = 22;
            this.label453.Text = "High Flood Level (HFL) [RL2]";
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
            // label454
            // 
            this.label454.AutoSize = true;
            this.label454.Location = new System.Drawing.Point(282, 28);
            this.label454.Name = "label454";
            this.label454.Size = new System.Drawing.Size(133, 13);
            this.label454.TabIndex = 20;
            this.label454.Text = "R.L. at Pier Cap Top [RL1]";
            // 
            // txt_RCC_Pier_B6
            // 
            this.txt_RCC_Pier_B6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B6.Location = new System.Drawing.Point(183, 521);
            this.txt_RCC_Pier_B6.Name = "txt_RCC_Pier_B6";
            this.txt_RCC_Pier_B6.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B6.TabIndex = 15;
            this.txt_RCC_Pier_B6.Text = "5.50";
            this.txt_RCC_Pier_B6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label455
            // 
            this.label455.AutoSize = true;
            this.label455.Location = new System.Drawing.Point(16, 524);
            this.label455.Name = "label455";
            this.label455.Size = new System.Drawing.Size(112, 13);
            this.label455.TabIndex = 18;
            this.label455.Text = "Length of Footing [B6]";
            // 
            // txt_RCC_Pier_B5
            // 
            this.txt_RCC_Pier_B5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_B5.Location = new System.Drawing.Point(183, 494);
            this.txt_RCC_Pier_B5.Name = "txt_RCC_Pier_B5";
            this.txt_RCC_Pier_B5.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_B5.TabIndex = 14;
            this.txt_RCC_Pier_B5.Text = "2.65";
            this.txt_RCC_Pier_B5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_RCC_Pier_B5.Visible = false;
            // 
            // label456
            // 
            this.label456.AutoSize = true;
            this.label456.Location = new System.Drawing.Point(16, 497);
            this.label456.Name = "label456";
            this.label456.Size = new System.Drawing.Size(152, 13);
            this.label456.TabIndex = 13;
            this.label456.Text = "Distance Between Girders [B5]";
            this.label456.Visible = false;
            // 
            // txt_RCC_Pier_DS
            // 
            this.txt_RCC_Pier_DS.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_DS.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_DS.Location = new System.Drawing.Point(183, 210);
            this.txt_RCC_Pier_DS.Name = "txt_RCC_Pier_DS";
            this.txt_RCC_Pier_DS.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_DS.TabIndex = 7;
            this.txt_RCC_Pier_DS.Text = "0.25";
            this.txt_RCC_Pier_DS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label457
            // 
            this.label457.AutoSize = true;
            this.label457.Location = new System.Drawing.Point(16, 213);
            this.label457.Name = "label457";
            this.label457.Size = new System.Drawing.Size(122, 13);
            this.label457.TabIndex = 14;
            this.label457.Text = "Depth of Deck Slab [d2]";
            // 
            // txt_RCC_Pier_DMG
            // 
            this.txt_RCC_Pier_DMG.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_DMG.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_DMG.Location = new System.Drawing.Point(183, 184);
            this.txt_RCC_Pier_DMG.Name = "txt_RCC_Pier_DMG";
            this.txt_RCC_Pier_DMG.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_DMG.TabIndex = 6;
            this.txt_RCC_Pier_DMG.Text = "1.55";
            this.txt_RCC_Pier_DMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label458
            // 
            this.label458.AutoSize = true;
            this.label458.Location = new System.Drawing.Point(16, 187);
            this.label458.Name = "label458";
            this.label458.Size = new System.Drawing.Size(100, 13);
            this.label458.TabIndex = 12;
            this.label458.Text = "Depth of Girder [d1]";
            // 
            // txt_RCC_Pier_NMG
            // 
            this.txt_RCC_Pier_NMG.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_NMG.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_NMG.Location = new System.Drawing.Point(183, 158);
            this.txt_RCC_Pier_NMG.Name = "txt_RCC_Pier_NMG";
            this.txt_RCC_Pier_NMG.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_NMG.TabIndex = 5;
            this.txt_RCC_Pier_NMG.Text = "2";
            this.txt_RCC_Pier_NMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label459
            // 
            this.label459.AutoSize = true;
            this.label459.Location = new System.Drawing.Point(16, 161);
            this.label459.Name = "label459";
            this.label459.Size = new System.Drawing.Size(124, 13);
            this.label459.TabIndex = 10;
            this.label459.Text = "Number of Bearings [NB]";
            // 
            // txt_RCC_Pier_Hp
            // 
            this.txt_RCC_Pier_Hp.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Hp.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_Hp.Location = new System.Drawing.Point(183, 132);
            this.txt_RCC_Pier_Hp.Name = "txt_RCC_Pier_Hp";
            this.txt_RCC_Pier_Hp.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_Hp.TabIndex = 4;
            this.txt_RCC_Pier_Hp.Text = "1.05";
            this.txt_RCC_Pier_Hp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label460
            // 
            this.label460.AutoSize = true;
            this.label460.Location = new System.Drawing.Point(16, 135);
            this.label460.Name = "label460";
            this.label460.Size = new System.Drawing.Size(134, 13);
            this.label460.TabIndex = 8;
            this.label460.Text = "Height of Crash Barrier [a1]";
            // 
            // txt_RCC_Pier_Wp
            // 
            this.txt_RCC_Pier_Wp.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_Wp.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier_Wp.Location = new System.Drawing.Point(183, 106);
            this.txt_RCC_Pier_Wp.Name = "txt_RCC_Pier_Wp";
            this.txt_RCC_Pier_Wp.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_Wp.TabIndex = 3;
            this.txt_RCC_Pier_Wp.Text = "0.50";
            this.txt_RCC_Pier_Wp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label461
            // 
            this.label461.AutoSize = true;
            this.label461.Location = new System.Drawing.Point(16, 109);
            this.label461.Name = "label461";
            this.label461.Size = new System.Drawing.Size(133, 13);
            this.label461.TabIndex = 6;
            this.label461.Text = "Width of Crash Barrier [w3]";
            // 
            // txt_RCC_Pier__B
            // 
            this.txt_RCC_Pier__B.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier__B.ForeColor = System.Drawing.Color.Black;
            this.txt_RCC_Pier__B.Location = new System.Drawing.Point(183, 80);
            this.txt_RCC_Pier__B.Name = "txt_RCC_Pier__B";
            this.txt_RCC_Pier__B.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier__B.TabIndex = 2;
            this.txt_RCC_Pier__B.Text = "12.50";
            this.txt_RCC_Pier__B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label462
            // 
            this.label462.AutoSize = true;
            this.label462.Location = new System.Drawing.Point(16, 83);
            this.label462.Name = "label462";
            this.label462.Size = new System.Drawing.Size(132, 13);
            this.label462.TabIndex = 4;
            this.label462.Text = "Overall width of Deck [w2]";
            // 
            // txt_RCC_Pier_CW
            // 
            this.txt_RCC_Pier_CW.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_CW.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_CW.Location = new System.Drawing.Point(183, 54);
            this.txt_RCC_Pier_CW.Name = "txt_RCC_Pier_CW";
            this.txt_RCC_Pier_CW.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_CW.TabIndex = 1;
            this.txt_RCC_Pier_CW.Text = "9.75";
            this.txt_RCC_Pier_CW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label463
            // 
            this.label463.AutoSize = true;
            this.label463.Location = new System.Drawing.Point(16, 57);
            this.label463.Name = "label463";
            this.label463.Size = new System.Drawing.Size(116, 13);
            this.label463.TabIndex = 2;
            this.label463.Text = "Carriageway width [w1]";
            // 
            // txt_RCC_Pier_L
            // 
            this.txt_RCC_Pier_L.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RCC_Pier_L.ForeColor = System.Drawing.Color.Red;
            this.txt_RCC_Pier_L.Location = new System.Drawing.Point(183, 25);
            this.txt_RCC_Pier_L.Name = "txt_RCC_Pier_L";
            this.txt_RCC_Pier_L.Size = new System.Drawing.Size(48, 20);
            this.txt_RCC_Pier_L.TabIndex = 0;
            this.txt_RCC_Pier_L.Text = "18.0";
            this.txt_RCC_Pier_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label464
            // 
            this.label464.AutoSize = true;
            this.label464.Location = new System.Drawing.Point(16, 28);
            this.label464.Name = "label464";
            this.label464.Size = new System.Drawing.Size(162, 13);
            this.label464.TabIndex = 0;
            this.label464.Text = "C/C Distance between Piers [L1]";
            // 
            // tab_des_form2
            // 
            this.tab_des_form2.Controls.Add(this.label471);
            this.tab_des_form2.Controls.Add(this.label472);
            this.tab_des_form2.Controls.Add(this.label479);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_vspc);
            this.tab_des_form2.Controls.Add(this.label480);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_vdia);
            this.tab_des_form2.Controls.Add(this.label481);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_hdia);
            this.tab_des_form2.Controls.Add(this.label512);
            this.tab_des_form2.Controls.Add(this.label513);
            this.tab_des_form2.Controls.Add(this.label516);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_ldia);
            this.tab_des_form2.Controls.Add(this.label517);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_vlegs);
            this.tab_des_form2.Controls.Add(this.label1076);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_hlegs);
            this.tab_des_form2.Controls.Add(this.label1077);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_slegs);
            this.tab_des_form2.Controls.Add(this.label1078);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_sdia);
            this.tab_des_form2.Controls.Add(this.label1079);
            this.tab_des_form2.Controls.Add(this.label233);
            this.tab_des_form2.Controls.Add(this.label234);
            this.tab_des_form2.Controls.Add(this.label465);
            this.tab_des_form2.Controls.Add(this.label466);
            this.tab_des_form2.Controls.Add(this.label467);
            this.tab_des_form2.Controls.Add(this.label468);
            this.tab_des_form2.Controls.Add(this.label469);
            this.tab_des_form2.Controls.Add(this.label470);
            this.tab_des_form2.Controls.Add(this.label473);
            this.tab_des_form2.Controls.Add(this.label474);
            this.tab_des_form2.Controls.Add(this.label475);
            this.tab_des_form2.Controls.Add(this.label476);
            this.tab_des_form2.Controls.Add(this.label477);
            this.tab_des_form2.Controls.Add(this.cmb_pier_2_k);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_SBC);
            this.tab_des_form2.Controls.Add(this.label478);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_Itc);
            this.tab_des_form2.Controls.Add(this.label482);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_Vr);
            this.tab_des_form2.Controls.Add(this.label483);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_LL);
            this.tab_des_form2.Controls.Add(this.label484);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_CF);
            this.tab_des_form2.Controls.Add(this.label485);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_k);
            this.tab_des_form2.Controls.Add(this.label486);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_V);
            this.tab_des_form2.Controls.Add(this.label487);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_HHF);
            this.tab_des_form2.Controls.Add(this.label488);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_SC);
            this.tab_des_form2.Controls.Add(this.label489);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_PD);
            this.tab_des_form2.Controls.Add(this.label490);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_PML);
            this.tab_des_form2.Controls.Add(this.label491);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_PL);
            this.tab_des_form2.Controls.Add(this.label492);
            this.tab_des_form2.Controls.Add(this.label493);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_APD);
            this.tab_des_form2.Controls.Add(this.label494);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_B16);
            this.tab_des_form2.Controls.Add(this.label495);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_P3);
            this.tab_des_form2.Controls.Add(this.label496);
            this.tab_des_form2.Controls.Add(this.label497);
            this.tab_des_form2.Controls.Add(this.txt_pier_2_P2);
            this.tab_des_form2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_des_form2.Location = new System.Drawing.Point(4, 22);
            this.tab_des_form2.Name = "tab_des_form2";
            this.tab_des_form2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_form2.Size = new System.Drawing.Size(941, 546);
            this.tab_des_form2.TabIndex = 2;
            this.tab_des_form2.Text = "Design Input Data [Form2]";
            this.tab_des_form2.UseVisualStyleBackColor = true;
            // 
            // label471
            // 
            this.label471.AutoSize = true;
            this.label471.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label471.Location = new System.Drawing.Point(869, 430);
            this.label471.Name = "label471";
            this.label471.Size = new System.Drawing.Size(31, 13);
            this.label471.TabIndex = 264;
            this.label471.Text = "mm";
            // 
            // label472
            // 
            this.label472.AutoSize = true;
            this.label472.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label472.Location = new System.Drawing.Point(869, 372);
            this.label472.Name = "label472";
            this.label472.Size = new System.Drawing.Size(31, 13);
            this.label472.TabIndex = 265;
            this.label472.Text = "mm";
            // 
            // label479
            // 
            this.label479.AutoSize = true;
            this.label479.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label479.Location = new System.Drawing.Point(869, 316);
            this.label479.Name = "label479";
            this.label479.Size = new System.Drawing.Size(31, 13);
            this.label479.TabIndex = 266;
            this.label479.Text = "mm";
            // 
            // txt_pier_2_vspc
            // 
            this.txt_pier_2_vspc.Location = new System.Drawing.Point(789, 427);
            this.txt_pier_2_vspc.Name = "txt_pier_2_vspc";
            this.txt_pier_2_vspc.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_vspc.TabIndex = 261;
            this.txt_pier_2_vspc.Text = "200";
            this.txt_pier_2_vspc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label480
            // 
            this.label480.AutoSize = true;
            this.label480.Location = new System.Drawing.Point(471, 430);
            this.label480.Name = "label480";
            this.label480.Size = new System.Drawing.Size(263, 13);
            this.label480.TabIndex = 258;
            this.label480.Text = "Spacing between Vertical Stirrup Bars [vspc]";
            // 
            // txt_pier_2_vdia
            // 
            this.txt_pier_2_vdia.Location = new System.Drawing.Point(789, 369);
            this.txt_pier_2_vdia.Name = "txt_pier_2_vdia";
            this.txt_pier_2_vdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_vdia.TabIndex = 262;
            this.txt_pier_2_vdia.Text = "10";
            this.txt_pier_2_vdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label481
            // 
            this.label481.AutoSize = true;
            this.label481.Location = new System.Drawing.Point(471, 372);
            this.label481.Name = "label481";
            this.label481.Size = new System.Drawing.Size(232, 13);
            this.label481.TabIndex = 259;
            this.label481.Text = "Diameter of Vertical Stirrup Bars [vdia]";
            // 
            // txt_pier_2_hdia
            // 
            this.txt_pier_2_hdia.Location = new System.Drawing.Point(789, 313);
            this.txt_pier_2_hdia.Name = "txt_pier_2_hdia";
            this.txt_pier_2_hdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_hdia.TabIndex = 263;
            this.txt_pier_2_hdia.Text = "12";
            this.txt_pier_2_hdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label512
            // 
            this.label512.AutoSize = true;
            this.label512.Location = new System.Drawing.Point(471, 316);
            this.label512.Name = "label512";
            this.label512.Size = new System.Drawing.Size(247, 13);
            this.label512.TabIndex = 260;
            this.label512.Text = "Diameter of Horizontal Stirrup Bars [hdia]";
            // 
            // label513
            // 
            this.label513.AutoSize = true;
            this.label513.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label513.Location = new System.Drawing.Point(869, 289);
            this.label513.Name = "label513";
            this.label513.Size = new System.Drawing.Size(31, 13);
            this.label513.TabIndex = 257;
            this.label513.Text = "mm";
            // 
            // label516
            // 
            this.label516.AutoSize = true;
            this.label516.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label516.Location = new System.Drawing.Point(869, 235);
            this.label516.Name = "label516";
            this.label516.Size = new System.Drawing.Size(31, 13);
            this.label516.TabIndex = 256;
            this.label516.Text = "mm";
            // 
            // txt_pier_2_ldia
            // 
            this.txt_pier_2_ldia.Location = new System.Drawing.Point(789, 286);
            this.txt_pier_2_ldia.Name = "txt_pier_2_ldia";
            this.txt_pier_2_ldia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_ldia.TabIndex = 255;
            this.txt_pier_2_ldia.Text = "25";
            this.txt_pier_2_ldia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label517
            // 
            this.label517.AutoSize = true;
            this.label517.Location = new System.Drawing.Point(471, 289);
            this.label517.Name = "label517";
            this.label517.Size = new System.Drawing.Size(295, 13);
            this.label517.TabIndex = 254;
            this.label517.Text = "Diameter of Longitudinal reinforcement Bars [ldia]";
            // 
            // txt_pier_2_vlegs
            // 
            this.txt_pier_2_vlegs.Location = new System.Drawing.Point(789, 399);
            this.txt_pier_2_vlegs.Name = "txt_pier_2_vlegs";
            this.txt_pier_2_vlegs.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_vlegs.TabIndex = 253;
            this.txt_pier_2_vlegs.Text = "4";
            this.txt_pier_2_vlegs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1076
            // 
            this.label1076.AutoSize = true;
            this.label1076.Location = new System.Drawing.Point(470, 402);
            this.label1076.Name = "label1076";
            this.label1076.Size = new System.Drawing.Size(282, 13);
            this.label1076.TabIndex = 250;
            this.label1076.Text = "Vertical Stirrup Reinforcement Legs Nos. [vlegs]";
            // 
            // txt_pier_2_hlegs
            // 
            this.txt_pier_2_hlegs.Location = new System.Drawing.Point(789, 340);
            this.txt_pier_2_hlegs.Name = "txt_pier_2_hlegs";
            this.txt_pier_2_hlegs.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_hlegs.TabIndex = 252;
            this.txt_pier_2_hlegs.Text = "12";
            this.txt_pier_2_hlegs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1077
            // 
            this.label1077.AutoSize = true;
            this.label1077.Location = new System.Drawing.Point(470, 343);
            this.label1077.Name = "label1077";
            this.label1077.Size = new System.Drawing.Size(297, 13);
            this.label1077.TabIndex = 249;
            this.label1077.Text = "Horizontal Stirrup Reinforcement Legs Nos. [hlegs]";
            // 
            // txt_pier_2_slegs
            // 
            this.txt_pier_2_slegs.Location = new System.Drawing.Point(789, 259);
            this.txt_pier_2_slegs.Name = "txt_pier_2_slegs";
            this.txt_pier_2_slegs.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_slegs.TabIndex = 251;
            this.txt_pier_2_slegs.Text = "6";
            this.txt_pier_2_slegs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1078
            // 
            this.label1078.AutoSize = true;
            this.label1078.Location = new System.Drawing.Point(471, 267);
            this.label1078.Name = "label1078";
            this.label1078.Size = new System.Drawing.Size(230, 13);
            this.label1078.TabIndex = 248;
            this.label1078.Text = "Shear Reinforcement Legs Nos. [slegs]";
            // 
            // txt_pier_2_sdia
            // 
            this.txt_pier_2_sdia.Location = new System.Drawing.Point(789, 232);
            this.txt_pier_2_sdia.Name = "txt_pier_2_sdia";
            this.txt_pier_2_sdia.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_sdia.TabIndex = 247;
            this.txt_pier_2_sdia.Text = "16";
            this.txt_pier_2_sdia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1079
            // 
            this.label1079.AutoSize = true;
            this.label1079.Location = new System.Drawing.Point(471, 240);
            this.label1079.Name = "label1079";
            this.label1079.Size = new System.Drawing.Size(223, 13);
            this.label1079.TabIndex = 246;
            this.label1079.Text = "Diameter of Reinforcement Bar [sdia]";
            // 
            // label233
            // 
            this.label233.AutoSize = true;
            this.label233.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label233.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label233.ForeColor = System.Drawing.Color.Red;
            this.label233.Location = new System.Drawing.Point(407, 12);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(218, 18);
            this.label233.TabIndex = 126;
            this.label233.Text = "Default Sample Data are shown";
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label234.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label234.ForeColor = System.Drawing.Color.Green;
            this.label234.Location = new System.Drawing.Point(255, 12);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(135, 18);
            this.label234.TabIndex = 125;
            this.label234.Text = "All User Input Data";
            // 
            // label465
            // 
            this.label465.AutoSize = true;
            this.label465.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label465.Location = new System.Drawing.Point(869, 77);
            this.label465.Name = "label465";
            this.label465.Size = new System.Drawing.Size(49, 13);
            this.label465.TabIndex = 39;
            this.label465.Text = "m/sec";
            // 
            // label466
            // 
            this.label466.AutoSize = true;
            this.label466.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label466.Location = new System.Drawing.Point(414, 444);
            this.label466.Name = "label466";
            this.label466.Size = new System.Drawing.Size(19, 13);
            this.label466.TabIndex = 39;
            this.label466.Text = "m";
            // 
            // label467
            // 
            this.label467.AutoSize = true;
            this.label467.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label467.Location = new System.Drawing.Point(414, 330);
            this.label467.Name = "label467";
            this.label467.Size = new System.Drawing.Size(24, 13);
            this.label467.TabIndex = 39;
            this.label467.Text = "kN";
            // 
            // label468
            // 
            this.label468.AutoSize = true;
            this.label468.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label468.Location = new System.Drawing.Point(414, 286);
            this.label468.Name = "label468";
            this.label468.Size = new System.Drawing.Size(24, 13);
            this.label468.TabIndex = 39;
            this.label468.Text = "kN";
            // 
            // label469
            // 
            this.label469.AutoSize = true;
            this.label469.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label469.Location = new System.Drawing.Point(414, 147);
            this.label469.Name = "label469";
            this.label469.Size = new System.Drawing.Size(19, 13);
            this.label469.TabIndex = 39;
            this.label469.Text = "m";
            // 
            // label470
            // 
            this.label470.AutoSize = true;
            this.label470.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label470.Location = new System.Drawing.Point(869, 489);
            this.label470.Name = "label470";
            this.label470.Size = new System.Drawing.Size(63, 13);
            this.label470.TabIndex = 39;
            this.label470.Text = "kN/sq.m";
            // 
            // label473
            // 
            this.label473.AutoSize = true;
            this.label473.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label473.Location = new System.Drawing.Point(869, 201);
            this.label473.Name = "label473";
            this.label473.Size = new System.Drawing.Size(56, 13);
            this.label473.TabIndex = 39;
            this.label473.Text = "kN/mm";
            // 
            // label474
            // 
            this.label474.AutoSize = true;
            this.label474.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label474.Location = new System.Drawing.Point(869, 176);
            this.label474.Name = "label474";
            this.label474.Size = new System.Drawing.Size(56, 13);
            this.label474.TabIndex = 39;
            this.label474.Text = "kN/mm";
            // 
            // label475
            // 
            this.label475.AutoSize = true;
            this.label475.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label475.Location = new System.Drawing.Point(415, 489);
            this.label475.Name = "label475";
            this.label475.Size = new System.Drawing.Size(24, 13);
            this.label475.TabIndex = 39;
            this.label475.Text = "kN";
            // 
            // label476
            // 
            this.label476.AutoSize = true;
            this.label476.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label476.Location = new System.Drawing.Point(414, 113);
            this.label476.Name = "label476";
            this.label476.Size = new System.Drawing.Size(44, 13);
            this.label476.TabIndex = 39;
            this.label476.Text = "kN/m";
            // 
            // label477
            // 
            this.label477.AutoSize = true;
            this.label477.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label477.Location = new System.Drawing.Point(414, 77);
            this.label477.Name = "label477";
            this.label477.Size = new System.Drawing.Size(44, 13);
            this.label477.TabIndex = 39;
            this.label477.Text = "kN/m";
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
            this.cmb_pier_2_k.Location = new System.Drawing.Point(619, 110);
            this.cmb_pier_2_k.Name = "cmb_pier_2_k";
            this.cmb_pier_2_k.Size = new System.Drawing.Size(164, 21);
            this.cmb_pier_2_k.TabIndex = 38;
            this.cmb_pier_2_k.SelectedIndexChanged += new System.EventHandler(this.cmb_pier_2_k_SelectedIndexChanged);
            // 
            // txt_pier_2_SBC
            // 
            this.txt_pier_2_SBC.Location = new System.Drawing.Point(789, 486);
            this.txt_pier_2_SBC.Name = "txt_pier_2_SBC";
            this.txt_pier_2_SBC.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_SBC.TabIndex = 37;
            this.txt_pier_2_SBC.Text = "120";
            this.txt_pier_2_SBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label478
            // 
            this.label478.AutoSize = true;
            this.label478.Location = new System.Drawing.Point(468, 489);
            this.label478.Name = "label478";
            this.label478.Size = new System.Drawing.Size(274, 13);
            this.label478.TabIndex = 36;
            this.label478.Text = "Safe Bearing Capacity of River Bed Soil [SBC]";
            // 
            // txt_pier_2_Itc
            // 
            this.txt_pier_2_Itc.Location = new System.Drawing.Point(789, 198);
            this.txt_pier_2_Itc.Name = "txt_pier_2_Itc";
            this.txt_pier_2_Itc.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_Itc.TabIndex = 29;
            this.txt_pier_2_Itc.Text = "4.21";
            this.txt_pier_2_Itc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label482
            // 
            this.label482.AutoSize = true;
            this.label482.Location = new System.Drawing.Point(468, 203);
            this.label482.Name = "label482";
            this.label482.Size = new System.Drawing.Size(226, 13);
            this.label482.TabIndex = 28;
            this.label482.Text = "Shirnkage Force on Each Bearing [Itc]";
            // 
            // txt_pier_2_Vr
            // 
            this.txt_pier_2_Vr.Location = new System.Drawing.Point(789, 171);
            this.txt_pier_2_Vr.Name = "txt_pier_2_Vr";
            this.txt_pier_2_Vr.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_Vr.TabIndex = 27;
            this.txt_pier_2_Vr.Text = "2.5";
            this.txt_pier_2_Vr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label483
            // 
            this.label483.AutoSize = true;
            this.label483.Location = new System.Drawing.Point(468, 179);
            this.label483.Name = "label483";
            this.label483.Size = new System.Drawing.Size(239, 13);
            this.label483.TabIndex = 26;
            this.label483.Text = "Temperature Force on Each Bearing [Vr]";
            // 
            // txt_pier_2_LL
            // 
            this.txt_pier_2_LL.Location = new System.Drawing.Point(322, 486);
            this.txt_pier_2_LL.Name = "txt_pier_2_LL";
            this.txt_pier_2_LL.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_LL.TabIndex = 25;
            this.txt_pier_2_LL.Text = "1000";
            this.txt_pier_2_LL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label484
            // 
            this.label484.AutoSize = true;
            this.label484.Location = new System.Drawing.Point(6, 489);
            this.label484.Name = "label484";
            this.label484.Size = new System.Drawing.Size(222, 13);
            this.label484.TabIndex = 24;
            this.label484.Text = "Breaking Force 20% of Live Load [LL]";
            // 
            // txt_pier_2_CF
            // 
            this.txt_pier_2_CF.Location = new System.Drawing.Point(789, 144);
            this.txt_pier_2_CF.Name = "txt_pier_2_CF";
            this.txt_pier_2_CF.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_CF.TabIndex = 23;
            this.txt_pier_2_CF.Text = "0.6";
            this.txt_pier_2_CF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label485
            // 
            this.label485.AutoSize = true;
            this.label485.Location = new System.Drawing.Point(468, 139);
            this.label485.Name = "label485";
            this.label485.Size = new System.Drawing.Size(180, 26);
            this.label485.TabIndex = 22;
            this.label485.Text = "Coefficient of Friction between\r\nConcrete and River Bed [CF]";
            // 
            // txt_pier_2_k
            // 
            this.txt_pier_2_k.Location = new System.Drawing.Point(789, 110);
            this.txt_pier_2_k.Name = "txt_pier_2_k";
            this.txt_pier_2_k.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_k.TabIndex = 21;
            this.txt_pier_2_k.Text = "0.66";
            this.txt_pier_2_k.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label486
            // 
            this.label486.AutoSize = true;
            this.label486.Location = new System.Drawing.Point(468, 113);
            this.label486.Name = "label486";
            this.label486.Size = new System.Drawing.Size(145, 13);
            this.label486.TabIndex = 20;
            this.label486.Text = "Pier Shape Constant [k]";
            // 
            // txt_pier_2_V
            // 
            this.txt_pier_2_V.Location = new System.Drawing.Point(789, 74);
            this.txt_pier_2_V.Name = "txt_pier_2_V";
            this.txt_pier_2_V.Size = new System.Drawing.Size(74, 21);
            this.txt_pier_2_V.TabIndex = 19;
            this.txt_pier_2_V.Text = "8.0";
            this.txt_pier_2_V.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label487
            // 
            this.label487.AutoSize = true;
            this.label487.Location = new System.Drawing.Point(468, 82);
            this.label487.Name = "label487";
            this.label487.Size = new System.Drawing.Size(261, 13);
            this.label487.TabIndex = 18;
            this.label487.Text = "Observed Velocity of water at High Flood [V]";
            // 
            // txt_pier_2_HHF
            // 
            this.txt_pier_2_HHF.Location = new System.Drawing.Point(322, 441);
            this.txt_pier_2_HHF.Name = "txt_pier_2_HHF";
            this.txt_pier_2_HHF.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_HHF.TabIndex = 17;
            this.txt_pier_2_HHF.Text = "6.0";
            this.txt_pier_2_HHF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label488
            // 
            this.label488.AutoSize = true;
            this.label488.Location = new System.Drawing.Point(6, 436);
            this.label488.Name = "label488";
            this.label488.Size = new System.Drawing.Size(304, 26);
            this.label488.TabIndex = 16;
            this.label488.Text = "Height of Water from River Bed at High Flood [HHF] \r\n(put value 0 if not required" +
    ")\r\n";
            // 
            // txt_pier_2_SC
            // 
            this.txt_pier_2_SC.ForeColor = System.Drawing.Color.Black;
            this.txt_pier_2_SC.Location = new System.Drawing.Point(322, 400);
            this.txt_pier_2_SC.Name = "txt_pier_2_SC";
            this.txt_pier_2_SC.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_SC.TabIndex = 15;
            this.txt_pier_2_SC.Text = "0.18";
            this.txt_pier_2_SC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label489
            // 
            this.label489.AutoSize = true;
            this.label489.Location = new System.Drawing.Point(6, 392);
            this.label489.Name = "label489";
            this.label489.Size = new System.Drawing.Size(170, 26);
            this.label489.TabIndex = 14;
            this.label489.Text = "Seismic Coefficient [SC]\r\n (put value 0 if not required)";
            // 
            // txt_pier_2_PD
            // 
            this.txt_pier_2_PD.Location = new System.Drawing.Point(258, 242);
            this.txt_pier_2_PD.Name = "txt_pier_2_PD";
            this.txt_pier_2_PD.ReadOnly = true;
            this.txt_pier_2_PD.Size = new System.Drawing.Size(151, 21);
            this.txt_pier_2_PD.TabIndex = 13;
            this.txt_pier_2_PD.Text = "2.0,0.5";
            this.txt_pier_2_PD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label490
            // 
            this.label490.AutoSize = true;
            this.label490.Location = new System.Drawing.Point(6, 237);
            this.label490.Name = "label490";
            this.label490.Size = new System.Drawing.Size(246, 26);
            this.label490.TabIndex = 12;
            this.label490.Text = "(Get Distances of each pairs of pedestals \r\nwithin the distance of B16) [PD]";
            // 
            // txt_pier_2_PML
            // 
            this.txt_pier_2_PML.Location = new System.Drawing.Point(322, 327);
            this.txt_pier_2_PML.Name = "txt_pier_2_PML";
            this.txt_pier_2_PML.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_PML.TabIndex = 11;
            this.txt_pier_2_PML.Text = "195.56";
            this.txt_pier_2_PML.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label491
            // 
            this.label491.AutoSize = true;
            this.label491.Location = new System.Drawing.Point(6, 322);
            this.label491.Name = "label491";
            this.label491.Size = new System.Drawing.Size(259, 26);
            this.label491.TabIndex = 10;
            this.label491.Text = "(Get Moments on each \r\nPedestal = Total Moment / total Pairs) [PML]";
            // 
            // txt_pier_2_PL
            // 
            this.txt_pier_2_PL.Location = new System.Drawing.Point(322, 283);
            this.txt_pier_2_PL.Name = "txt_pier_2_PL";
            this.txt_pier_2_PL.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_PL.TabIndex = 9;
            this.txt_pier_2_PL.Text = "306.4";
            this.txt_pier_2_PL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label492
            // 
            this.label492.AutoSize = true;
            this.label492.Location = new System.Drawing.Point(6, 275);
            this.label492.Name = "label492";
            this.label492.Size = new System.Drawing.Size(303, 26);
            this.label492.TabIndex = 8;
            this.label492.Text = "(Get Load Reactions on each pair of\r\nPedestals =   Total Load Reaction / total Pa" +
    "irs ) [PL]";
            // 
            // label493
            // 
            this.label493.AutoSize = true;
            this.label493.Location = new System.Drawing.Point(6, 181);
            this.label493.Name = "label493";
            this.label493.Size = new System.Drawing.Size(237, 39);
            this.label493.TabIndex = 7;
            this.label493.Text = "Distances from Left Edge of Pier Cap to \r\nCentre of Each  pair of Pedestals [APD]" +
    "\r\n(seperated by comma \',\' or space \' \')";
            // 
            // txt_pier_2_APD
            // 
            this.txt_pier_2_APD.Location = new System.Drawing.Point(258, 171);
            this.txt_pier_2_APD.Multiline = true;
            this.txt_pier_2_APD.Name = "txt_pier_2_APD";
            this.txt_pier_2_APD.Size = new System.Drawing.Size(151, 54);
            this.txt_pier_2_APD.TabIndex = 6;
            this.txt_pier_2_APD.Text = "0.5,2.0,3.5,6.5, 8.0, 9.5";
            this.txt_pier_2_APD.TextChanged += new System.EventHandler(this.txt_pier_2_APD_TextChanged);
            // 
            // label494
            // 
            this.label494.AutoSize = true;
            this.label494.Location = new System.Drawing.Point(6, 139);
            this.label494.Name = "label494";
            this.label494.Size = new System.Drawing.Size(201, 26);
            this.label494.TabIndex = 5;
            this.label494.Text = "Distance from Left Edge Pier Cap \r\nEdge to Left face of Pier [B16]";
            // 
            // txt_pier_2_B16
            // 
            this.txt_pier_2_B16.ForeColor = System.Drawing.Color.Blue;
            this.txt_pier_2_B16.Location = new System.Drawing.Point(322, 144);
            this.txt_pier_2_B16.Name = "txt_pier_2_B16";
            this.txt_pier_2_B16.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_B16.TabIndex = 4;
            this.txt_pier_2_B16.Text = "2.500";
            this.txt_pier_2_B16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label495
            // 
            this.label495.AutoSize = true;
            this.label495.Location = new System.Drawing.Point(6, 113);
            this.label495.Name = "label495";
            this.label495.Size = new System.Drawing.Size(283, 13);
            this.label495.TabIndex = 3;
            this.label495.Text = "Live Load Support Reaction for all Supports [P3]";
            // 
            // txt_pier_2_P3
            // 
            this.txt_pier_2_P3.ForeColor = System.Drawing.Color.Red;
            this.txt_pier_2_P3.Location = new System.Drawing.Point(322, 108);
            this.txt_pier_2_P3.Name = "txt_pier_2_P3";
            this.txt_pier_2_P3.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_P3.TabIndex = 2;
            this.txt_pier_2_P3.Text = " ";
            this.txt_pier_2_P3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label496
            // 
            this.label496.AutoSize = true;
            this.label496.Location = new System.Drawing.Point(6, 82);
            this.label496.Name = "label496";
            this.label496.Size = new System.Drawing.Size(290, 13);
            this.label496.TabIndex = 1;
            this.label496.Text = "Dead Load Support Reaction for all Supports [P2]";
            this.label496.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label497
            // 
            this.label497.AutoSize = true;
            this.label497.Location = new System.Drawing.Point(6, 47);
            this.label497.Name = "label497";
            this.label497.Size = new System.Drawing.Size(526, 13);
            this.label497.TabIndex = 1;
            this.label497.Text = "Various Load Reactions for Supports within this distance are read from Analysis R" +
    "eport File";
            // 
            // txt_pier_2_P2
            // 
            this.txt_pier_2_P2.ForeColor = System.Drawing.Color.Red;
            this.txt_pier_2_P2.Location = new System.Drawing.Point(322, 74);
            this.txt_pier_2_P2.Name = "txt_pier_2_P2";
            this.txt_pier_2_P2.Size = new System.Drawing.Size(87, 21);
            this.txt_pier_2_P2.TabIndex = 0;
            this.txt_pier_2_P2.Text = " ";
            this.txt_pier_2_P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tab_des_Diagram
            // 
            this.tab_des_Diagram.Controls.Add(this.pic_pier_interactive_diagram);
            this.tab_des_Diagram.Location = new System.Drawing.Point(4, 22);
            this.tab_des_Diagram.Name = "tab_des_Diagram";
            this.tab_des_Diagram.Padding = new System.Windows.Forms.Padding(3);
            this.tab_des_Diagram.Size = new System.Drawing.Size(941, 546);
            this.tab_des_Diagram.TabIndex = 1;
            this.tab_des_Diagram.Text = "Diagram";
            this.tab_des_Diagram.UseVisualStyleBackColor = true;
            // 
            // pic_pier_interactive_diagram
            // 
            this.pic_pier_interactive_diagram.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_pier_interactive_diagram.BackgroundImage")));
            this.pic_pier_interactive_diagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_pier_interactive_diagram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_pier_interactive_diagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_pier_interactive_diagram.Location = new System.Drawing.Point(3, 3);
            this.pic_pier_interactive_diagram.Name = "pic_pier_interactive_diagram";
            this.pic_pier_interactive_diagram.Size = new System.Drawing.Size(935, 540);
            this.pic_pier_interactive_diagram.TabIndex = 0;
            this.pic_pier_interactive_diagram.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_RCC_Pier_Report);
            this.panel4.Controls.Add(this.btn_RCC_Pier_Process);
            this.panel4.Controls.Add(this.label155);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 575);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(949, 56);
            this.panel4.TabIndex = 0;
            // 
            // btn_RCC_Pier_Report
            // 
            this.btn_RCC_Pier_Report.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_RCC_Pier_Report.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RCC_Pier_Report.Location = new System.Drawing.Point(520, 22);
            this.btn_RCC_Pier_Report.Name = "btn_RCC_Pier_Report";
            this.btn_RCC_Pier_Report.Size = new System.Drawing.Size(161, 28);
            this.btn_RCC_Pier_Report.TabIndex = 12;
            this.btn_RCC_Pier_Report.Text = "Report";
            this.btn_RCC_Pier_Report.UseVisualStyleBackColor = true;
            this.btn_RCC_Pier_Report.Click += new System.EventHandler(this.btn_RccPier_Report_Click);
            // 
            // btn_RCC_Pier_Process
            // 
            this.btn_RCC_Pier_Process.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_RCC_Pier_Process.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RCC_Pier_Process.Location = new System.Drawing.Point(348, 22);
            this.btn_RCC_Pier_Process.Name = "btn_RCC_Pier_Process";
            this.btn_RCC_Pier_Process.Size = new System.Drawing.Size(161, 28);
            this.btn_RCC_Pier_Process.TabIndex = 11;
            this.btn_RCC_Pier_Process.Text = "Process";
            this.btn_RCC_Pier_Process.UseVisualStyleBackColor = true;
            this.btn_RCC_Pier_Process.Click += new System.EventHandler(this.btn_RccPier_Process_Click);
            // 
            // label155
            // 
            this.label155.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.ForeColor = System.Drawing.Color.Red;
            this.label155.Location = new System.Drawing.Point(449, 6);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(124, 14);
            this.label155.TabIndex = 120;
            this.label155.Text = "Design of RCC Pier";
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
            // tab_PierOpenLSM
            // 
            this.tab_PierOpenLSM.Controls.Add(this.uC_PierOpenLS1);
            this.tab_PierOpenLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_PierOpenLSM.Name = "tab_PierOpenLSM";
            this.tab_PierOpenLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PierOpenLSM.Size = new System.Drawing.Size(955, 634);
            this.tab_PierOpenLSM.TabIndex = 2;
            this.tab_PierOpenLSM.Text = "Pier Design with Open Foundation in LS";
            this.tab_PierOpenLSM.UseVisualStyleBackColor = true;
            // 
            // tc_abutment
            // 
            this.tc_abutment.Controls.Add(this.tab_AbutmentLSM);
            this.tc_abutment.Controls.Add(this.tab_AbutmentOpenLSM);
            this.tc_abutment.Controls.Add(this.tab_AbutmentPileLSM);
            this.tc_abutment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_abutment.Location = new System.Drawing.Point(3, 3);
            this.tc_abutment.Name = "tc_abutment";
            this.tc_abutment.SelectedIndex = 0;
            this.tc_abutment.Size = new System.Drawing.Size(963, 660);
            this.tc_abutment.TabIndex = 2;
            // 
            // tab_AbutmentLSM
            // 
            this.tab_AbutmentLSM.Controls.Add(this.uC_RCC_Abut1);
            this.tab_AbutmentLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_AbutmentLSM.Name = "tab_AbutmentLSM";
            this.tab_AbutmentLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_AbutmentLSM.Size = new System.Drawing.Size(955, 634);
            this.tab_AbutmentLSM.TabIndex = 0;
            this.tab_AbutmentLSM.Text = "Abutment Design with Open Foundation in LSM";
            this.tab_AbutmentLSM.UseVisualStyleBackColor = true;
            // 
            // uC_RCC_Abut1
            // 
            this.uC_RCC_Abut1.Deadload_Reaction = "";
            this.uC_RCC_Abut1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_RCC_Abut1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_RCC_Abut1.iApp = null;
            this.uC_RCC_Abut1.Is_Individual = true;
            this.uC_RCC_Abut1.Is_Limit_State = true;
            this.uC_RCC_Abut1.IsBoxType = false;
            this.uC_RCC_Abut1.Length = 19.58D;
            this.uC_RCC_Abut1.Location = new System.Drawing.Point(3, 3);
            this.uC_RCC_Abut1.Name = "uC_RCC_Abut1";
            this.uC_RCC_Abut1.Overhang = 0.65D;
            this.uC_RCC_Abut1.Size = new System.Drawing.Size(949, 628);
            this.uC_RCC_Abut1.TabIndex = 0;
            this.uC_RCC_Abut1.Width = 551D;
            // 
            // tab_AbutmentOpenLSM
            // 
            this.tab_AbutmentOpenLSM.Controls.Add(this.uC_AbutmentOpenLS1);
            this.tab_AbutmentOpenLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_AbutmentOpenLSM.Name = "tab_AbutmentOpenLSM";
            this.tab_AbutmentOpenLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_AbutmentOpenLSM.Size = new System.Drawing.Size(940, 642);
            this.tab_AbutmentOpenLSM.TabIndex = 1;
            this.tab_AbutmentOpenLSM.Text = "Abutment Design with Open Foundation in LSM";
            this.tab_AbutmentOpenLSM.UseVisualStyleBackColor = true;
            // 
            // uC_AbutmentOpenLS1
            // 
            this.uC_AbutmentOpenLS1.Bridge_Type = "RCC Girder with Deck Slab";
            this.uC_AbutmentOpenLS1.Carriageway_width = "16";
            this.uC_AbutmentOpenLS1.Concrete_Grade = "30";
            this.uC_AbutmentOpenLS1.Concrete_Reinforcement = "500";
            this.uC_AbutmentOpenLS1.Crash_Barrier = "0.5";
            this.uC_AbutmentOpenLS1.Crash_Barrier_weight = "0.8";
            this.uC_AbutmentOpenLS1.Cross_Camber = "0.025";
            this.uC_AbutmentOpenLS1.DL_MLL = "0.0";
            this.uC_AbutmentOpenLS1.DL_MTT = "-2095.50";
            this.uC_AbutmentOpenLS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_AbutmentOpenLS1.Exp_Gap = "40";
            this.uC_AbutmentOpenLS1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_AbutmentOpenLS1.Foot_path = "1.5";
            this.uC_AbutmentOpenLS1.Foot_Path_Live_Load = "0.4";
            this.uC_AbutmentOpenLS1.Girder_Depth = "2.0";
            this.uC_AbutmentOpenLS1.Girder_Nos = "6";
            this.uC_AbutmentOpenLS1.Girder_Spacing = "2.6";
            this.uC_AbutmentOpenLS1.Is_Force_From_Analysis = true;
            this.uC_AbutmentOpenLS1.LL_MLL_Max = "104.72";
            this.uC_AbutmentOpenLS1.LL_MLL_Min = "26.23";
            this.uC_AbutmentOpenLS1.LL_MTT_Max = "-660.72";
            this.uC_AbutmentOpenLS1.LL_MTT_Min = "-165.473";
            this.uC_AbutmentOpenLS1.Location = new System.Drawing.Point(3, 3);
            this.uC_AbutmentOpenLS1.Name = "uC_AbutmentOpenLS1";
            this.uC_AbutmentOpenLS1.Railing = "0.5";
            this.uC_AbutmentOpenLS1.Railing_weight = "0.6";
            this.uC_AbutmentOpenLS1.RCC_Density = "2.5";
            this.uC_AbutmentOpenLS1.SIDL_MLL = "0.0";
            this.uC_AbutmentOpenLS1.SIDL_MTT = "-458.64";
            this.uC_AbutmentOpenLS1.Size = new System.Drawing.Size(934, 636);
            this.uC_AbutmentOpenLS1.Slab_Thickness = "0.22";
            this.uC_AbutmentOpenLS1.Span = "25.0";
            this.uC_AbutmentOpenLS1.TabIndex = 0;
            this.uC_AbutmentOpenLS1.Wearing_coat_load = "0.22";
            this.uC_AbutmentOpenLS1.Wearing_Coat_Thickness = "65";
            // 
            // tab_AbutmentPileLSM
            // 
            this.tab_AbutmentPileLSM.Controls.Add(this.uC_AbutmentPileLS1);
            this.tab_AbutmentPileLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_AbutmentPileLSM.Name = "tab_AbutmentPileLSM";
            this.tab_AbutmentPileLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_AbutmentPileLSM.Size = new System.Drawing.Size(940, 642);
            this.tab_AbutmentPileLSM.TabIndex = 2;
            this.tab_AbutmentPileLSM.Text = "Abutment Design with Pile Foundation in LSM";
            this.tab_AbutmentPileLSM.UseVisualStyleBackColor = true;
            // 
            // uC_AbutmentPileLS1
            // 
            this.uC_AbutmentPileLS1.Abutment_Length = "13.0";
            this.uC_AbutmentPileLS1.Bridge_Type = "PSC Girder with Deck Slab";
            this.uC_AbutmentPileLS1.Carriageway_width = "13.0";
            this.uC_AbutmentPileLS1.Concrete_Grade = "35";
            this.uC_AbutmentPileLS1.Concrete_Reinforcement = "500";
            this.uC_AbutmentPileLS1.Crash_Barrier = "0.5";
            this.uC_AbutmentPileLS1.Crash_Barrier_weight = "0.8";
            this.uC_AbutmentPileLS1.Cross_Camber = "0.025";
            this.uC_AbutmentPileLS1.DL_MLL = "0.0";
            this.uC_AbutmentPileLS1.DL_MTT = "-458.64";
            this.uC_AbutmentPileLS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_AbutmentPileLS1.Exp_Gap = "50";
            this.uC_AbutmentPileLS1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_AbutmentPileLS1.Foot_path = "0.0";
            this.uC_AbutmentPileLS1.Foot_Path_Live_Load = "0.4";
            this.uC_AbutmentPileLS1.Girder_Depth = "1.08";
            this.uC_AbutmentPileLS1.Girder_Nos = "4";
            this.uC_AbutmentPileLS1.Girder_Spacing = "3.25";
            this.uC_AbutmentPileLS1.Is_Force_From_Analysis = true;
            this.uC_AbutmentPileLS1.LL_MLL_Max = "104.72";
            this.uC_AbutmentPileLS1.LL_MLL_Min = "26.23";
            this.uC_AbutmentPileLS1.LL_MTT_Max = "-660.726";
            this.uC_AbutmentPileLS1.LL_MTT_Min = "-165.47";
            this.uC_AbutmentPileLS1.Location = new System.Drawing.Point(3, 3);
            this.uC_AbutmentPileLS1.MAX_HOR_LOAD = "80.0";
            this.uC_AbutmentPileLS1.Max_Horizontal_capacity = "80.0";
            this.uC_AbutmentPileLS1.MAX_VERT_LOAD = "433.0";
            this.uC_AbutmentPileLS1.Max_Vertical_capacity = "433.0";
            this.uC_AbutmentPileLS1.Name = "uC_AbutmentPileLS1";
            this.uC_AbutmentPileLS1.Pile_Dia = "1.2";
            this.uC_AbutmentPileLS1.Railing = "0.0";
            this.uC_AbutmentPileLS1.Railing_weight = "0.6";
            this.uC_AbutmentPileLS1.RCC_Density = "2.5";
            this.uC_AbutmentPileLS1.Show_Title = false;
            this.uC_AbutmentPileLS1.SIDL_MLL = "0.0";
            this.uC_AbutmentPileLS1.SIDL_MTT = "-315.30";
            this.uC_AbutmentPileLS1.Size = new System.Drawing.Size(934, 636);
            this.uC_AbutmentPileLS1.Slab_Thickness = "0.22";
            this.uC_AbutmentPileLS1.Span = "12.687";
            this.uC_AbutmentPileLS1.TabIndex = 0;
            this.uC_AbutmentPileLS1.Wearing_coat_load = "0.22";
            this.uC_AbutmentPileLS1.Wearing_Coat_Thickness = "65";
            // 
            // uC_PierOpenLS1
            // 
            this.uC_PierOpenLS1.CarriageWidth_Left = "11.00";
            this.uC_PierOpenLS1.CarriageWidth_Right = "11.00";
            this.uC_PierOpenLS1.CC_Exp_Gap_Left = "40";
            this.uC_PierOpenLS1.CC_Exp_Gap_Right = "40";
            this.uC_PierOpenLS1.CC_Exp_Joint_CL_Brg_Left_Skew = "0.42";
            this.uC_PierOpenLS1.CC_Exp_Joint_CL_Brg_Right_Skew = "0.42";
            this.uC_PierOpenLS1.CC_Exp_Joint_Left_Skew = "25.84";
            this.uC_PierOpenLS1.CC_Exp_Joint_Right_Skew = "25.84";
            this.uC_PierOpenLS1.CrashBarierHeight_Left = "1.10";
            this.uC_PierOpenLS1.CrashBarierHeight_Right = "1.10";
            this.uC_PierOpenLS1.CrashBarierWidth_Left = "0.50";
            this.uC_PierOpenLS1.CrashBarierWidth_Nos = "2";
            this.uC_PierOpenLS1.CrashBarierWidth_Right = "0.50";
            this.uC_PierOpenLS1.CrossGirderNos_Left = "2";
            this.uC_PierOpenLS1.CrossGirderNos_Right = "2";
            this.uC_PierOpenLS1.CrossGirderWidth_Left = "0.45";
            this.uC_PierOpenLS1.CrossGirderWidth_Right = "0.45";
            this.uC_PierOpenLS1.DL_Force = "3322.53";
            this.uC_PierOpenLS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_PierOpenLS1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_PierOpenLS1.FootPathWidth_Left = "1.50";
            this.uC_PierOpenLS1.FootPathWidth_Nos = "2";
            this.uC_PierOpenLS1.FootPathWidth_Right = "1.50";
            this.uC_PierOpenLS1.FPLL_Force = "310.08";
            this.uC_PierOpenLS1.GirderDepth_Left = "2.00";
            this.uC_PierOpenLS1.GirderDepth_Right = "2.00";
            this.uC_PierOpenLS1.Is_Force_From_Analysis = true;
            this.uC_PierOpenLS1.Location = new System.Drawing.Point(3, 3);
            this.uC_PierOpenLS1.Name = "uC_PierOpenLS1";
            this.uC_PierOpenLS1.RailingWidth_Left = "0.50";
            this.uC_PierOpenLS1.RailingWidth_Nos = "2";
            this.uC_PierOpenLS1.RailingWidth_Right = "0.50";
            this.uC_PierOpenLS1.Show_Title = true;
            this.uC_PierOpenLS1.SIDL_Force = "723.52";
            this.uC_PierOpenLS1.Size = new System.Drawing.Size(949, 628);
            this.uC_PierOpenLS1.SkewAngle = "0.0";
            this.uC_PierOpenLS1.SlabDepth_Left = "0.22";
            this.uC_PierOpenLS1.SlabDepth_Right = "0.22";
            this.uC_PierOpenLS1.Surface_Force = "723.52";
            this.uC_PierOpenLS1.TabIndex = 0;
            this.uC_PierOpenLS1.TopFlangeWidth_Left = "1.00";
            this.uC_PierOpenLS1.TopFlangeWidth_Right = "0.22";
            this.uC_PierOpenLS1.WearingCoatThickness_Left = "65";
            this.uC_PierOpenLS1.WearingCoatThickness_Right = "65";
            // 
            // frm_Extradossed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 692);
            this.Controls.Add(this.tc_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Extradossed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extradossed Bridge";
            this.Load += new System.EventHandler(this.frm_Extradosed_Load);
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
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.pcb_cables)).EndInit();
            this.tab_cs_diagram.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tab_moving_data_british.ResumeLayout(false);
            this.groupBox45.ResumeLayout(false);
            this.spc_HB.Panel1.ResumeLayout(false);
            this.spc_HB.Panel2.ResumeLayout(false);
            this.spc_HB.ResumeLayout(false);
            this.groupBox105.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_british_loads)).EndInit();
            this.groupBox106.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_british_loads)).EndInit();
            this.groupBox107.ResumeLayout(false);
            this.groupBox107.PerformLayout();
            this.groupBox108.ResumeLayout(false);
            this.groupBox108.PerformLayout();
            this.grb_ha.ResumeLayout(false);
            this.grb_ha.PerformLayout();
            this.grb_ha_aply.ResumeLayout(false);
            this.grb_ha_aply.PerformLayout();
            this.grb_hb.ResumeLayout(false);
            this.grb_hb.PerformLayout();
            this.grb_hb_aply.ResumeLayout(false);
            this.grb_hb_aply.PerformLayout();
            this.tab_moving_data_indian.ResumeLayout(false);
            this.groupBox79.ResumeLayout(false);
            this.groupBox79.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).EndInit();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).EndInit();
            this.tab_moving_data_LRFD.ResumeLayout(false);
            this.groupBox47.ResumeLayout(false);
            this.groupBox47.PerformLayout();
            this.groupBox48.ResumeLayout(false);
            this.groupBox48.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LRFD_long_loads)).EndInit();
            this.groupBox49.ResumeLayout(false);
            this.groupBox49.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LRFD_long_liveloads)).EndInit();
            this.tab_analysis.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
            this.groupBox7.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox11.ResumeLayout(false);
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
            this.groupBox60.ResumeLayout(false);
            this.groupBox60.PerformLayout();
            this.groupBox61.ResumeLayout(false);
            this.groupBox61.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox62.ResumeLayout(false);
            this.groupBox63.ResumeLayout(false);
            this.groupBox63.PerformLayout();
            this.groupBox64.ResumeLayout(false);
            this.groupBox64.PerformLayout();
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
            this.tc_bridge_deck.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage18.ResumeLayout(false);
            this.tab_Segment.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox24.ResumeLayout(false);
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.groupBox37.ResumeLayout(false);
            this.groupBox37.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.groupBox35.ResumeLayout(false);
            this.groupBox35.PerformLayout();
            this.tabControl6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tab_rcc_abutment.ResumeLayout(false);
            this.tab_pier.ResumeLayout(false);
            this.tc_pier.ResumeLayout(false);
            this.tab_PierPileLSM.ResumeLayout(false);
            this.tab_PierWSM.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tab_des_form1.ResumeLayout(false);
            this.tab_des_form1.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.groupBox40.ResumeLayout(false);
            this.groupBox40.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox43.ResumeLayout(false);
            this.groupBox43.PerformLayout();
            this.tab_des_form2.ResumeLayout(false);
            this.tab_des_form2.PerformLayout();
            this.tab_des_Diagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_pier_interactive_diagram)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tab_drawings.ResumeLayout(false);
            this.tab_drawings.PerformLayout();
            this.tab_PierOpenLSM.ResumeLayout(false);
            this.tc_abutment.ResumeLayout(false);
            this.tab_AbutmentLSM.ResumeLayout(false);
            this.tab_AbutmentOpenLSM.ResumeLayout(false);
            this.tab_AbutmentPileLSM.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txt_Ana_skew_angle;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_support_distance;
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
        private System.Windows.Forms.Button btn_psc_box;
        private System.Windows.Forms.Button btn_design;
        private System.Windows.Forms.Button btn_worksheet_open;
        private System.Windows.Forms.Button btn_design_of_anchorage;
        private System.Windows.Forms.Button btn_cable_frict;
        private System.Windows.Forms.Button btn_Temp_trans;
        private System.Windows.Forms.Button btn_open_drawings;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Button btn_update_force;
        private System.Windows.Forms.CheckBox chk_R2;
        private System.Windows.Forms.CheckBox chk_R3;
        private System.Windows.Forms.CheckBox chk_M2;
        private System.Windows.Forms.CheckBox chk_M3;
        private System.Windows.Forms.TabPage tab_Segment;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txt_tab1_Lo;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txt_tab1_DW;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txt_tab1_L;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txt_tab1_exg;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_tab1_L2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txt_tab1_L1;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txt_tab1_Mct_SIDL;
        private System.Windows.Forms.TextBox txt_tab1_agt_SIDL;
        private System.Windows.Forms.TextBox txt_tab1_sctt;
        private System.Windows.Forms.TextBox txt_tab1_Mct;
        private System.Windows.Forms.TextBox txt_tab1_act;
        private System.Windows.Forms.TextBox txt_tab1_D;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txt_tab1_FactDL;
        private System.Windows.Forms.TextBox txt_tab1_bt;
        private System.Windows.Forms.TextBox txt_tab1_df;
        private System.Windows.Forms.TextBox txt_tab1_ds;
        private System.Windows.Forms.TextBox txt_tab1_wct;
        private System.Windows.Forms.TextBox txt_tab1_T_loss;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txt_tab1_Tr3;
        private System.Windows.Forms.TextBox txt_tab1_Tr2;
        private System.Windows.Forms.TextBox txt_tab1_Tr1;
        private System.Windows.Forms.TextBox txt_tab1_FactLL;
        private System.Windows.Forms.TextBox txt_tab1_FactSIDL;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.TextBox txt_tab1_Tf4;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.TextBox txt_tab1_Tf3;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txt_tab1_Tf2;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.TextBox txt_tab1_Tf1;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txt_tab2_D;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox txt_tab2_A;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox txt_tab2_Pu;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txt_tab2_cwccb_day;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox txt_tab2_fsp_day;
        private System.Windows.Forms.TextBox txt_tab2_ccbg_day;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txt_tab2_cwccb_fcj;
        private System.Windows.Forms.TextBox txt_tab2_ccbg_fcj;
        private System.Windows.Forms.TextBox txt_tab2_fsp_fcj;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox txt_tab2_mu;
        private System.Windows.Forms.TextBox txt_tab2_s;
        private System.Windows.Forms.TextBox txt_tab2_Pj;
        private System.Windows.Forms.TextBox txt_tab2_Eps;
        private System.Windows.Forms.TextBox txt_tab2_Pn;
        private System.Windows.Forms.TextBox txt_tab2_Fu;
        private System.Windows.Forms.TextBox txt_tab2_Fy;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox txt_tab2_cover1;
        private System.Windows.Forms.TextBox txt_tab2_cover2;
        private System.Windows.Forms.TextBox txt_tab2_Ec;
        private System.Windows.Forms.TextBox txt_tab2_Fcu;
        private System.Windows.Forms.TextBox txt_tab2_qd;
        private System.Windows.Forms.TextBox txt_tab2_td1;
        private System.Windows.Forms.TextBox txt_tab2_Re2;
        private System.Windows.Forms.TextBox txt_tab2_Re1;
        private System.Windows.Forms.TextBox txt_tab2_k;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.DataGridView dgv_seg_tab3_1;
        private System.Windows.Forms.TextBox txt_tab3_d;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txt_tab3_L_8;
        private System.Windows.Forms.TextBox txt_tab3_L_2;
        private System.Windows.Forms.TextBox txt_tab3_3L_8;
        private System.Windows.Forms.TextBox txt_tab3_L_4;
        private System.Windows.Forms.TextBox txt_tab3_support;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox txt_tab1_alpha;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_segment_process;
        private System.Windows.Forms.Button btn_segment_report;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.TextBox txt_tab2_Ns;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.TextBox txt_tab2_nc_left;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.TextBox txt_tab2_nc_right;
        private System.Windows.Forms.Label label184;
        private System.Windows.Forms.TextBox txt_tab2_cable_area;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.TextBox txt_tab2_Resh56;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.TextBox txt_tab2_Crst56;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.TextBox txt_tab2_rss_56;
        private System.Windows.Forms.TextBox txt_tab2_rss_14;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.Label label201;
        private System.Windows.Forms.Label label195;
        private System.Windows.Forms.TextBox txt_tv;
        private System.Windows.Forms.TextBox txt_ft_temp28;
        private System.Windows.Forms.Label label200;
        private System.Windows.Forms.Label label196;
        private System.Windows.Forms.TextBox txt_fc_factor;
        private System.Windows.Forms.TextBox txt_fc_temp28;
        private System.Windows.Forms.Label label199;
        private System.Windows.Forms.Label label197;
        private System.Windows.Forms.TextBox txt_Mod_rup;
        private System.Windows.Forms.TextBox txt_ft_temp14;
        private System.Windows.Forms.Label label198;
        private System.Windows.Forms.TextBox txt_fc_serv;
        private System.Windows.Forms.Label label202;
        private System.Windows.Forms.TextBox txt_fc_temp14;
        private System.Windows.Forms.Label label204;
        private System.Windows.Forms.Label label203;
        private System.Windows.Forms.TextBox txt_ttu;
        private System.Windows.Forms.TextBox txt_ttv;
        private System.Windows.Forms.Label label205;
        private System.Windows.Forms.Label label208;
        private System.Windows.Forms.Label label207;
        private System.Windows.Forms.Label label206;
        private System.Windows.Forms.GroupBox groupBox35;
        private System.Windows.Forms.TextBox txt_zn1_out;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.Label label217;
        private System.Windows.Forms.Label label218;
        private System.Windows.Forms.Label label219;
        private System.Windows.Forms.Label label220;
        private System.Windows.Forms.TextBox txt_zn3_inn;
        private System.Windows.Forms.TextBox txt_zn3_out;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label209;
        private System.Windows.Forms.Label label212;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.Label label214;
        private System.Windows.Forms.TextBox txt_zn2_inn;
        private System.Windows.Forms.TextBox txt_zn2_out;
        private System.Windows.Forms.Label label216;
        private System.Windows.Forms.Label label215;
        private System.Windows.Forms.Label label211;
        private System.Windows.Forms.Label label210;
        private System.Windows.Forms.TextBox txt_zn1_inn;
        private System.Windows.Forms.ComboBox cmb_tab1_Fy;
        private System.Windows.Forms.ComboBox cmb_tab1_Fcu;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label222;
        private System.Windows.Forms.Label label223;
        private System.Windows.Forms.Label label225;
        private System.Windows.Forms.Label label224;
        private System.Windows.Forms.TabPage tab_pier;
        private System.Windows.Forms.Button btn_RCC_Pier_Process;
        private System.Windows.Forms.Button btn_RCC_Pier_Report;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tab_des_form1;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.TextBox txt_rcc_pier_m;
        private System.Windows.Forms.Label label117;
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
        private System.Windows.Forms.Label label355;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.Label label356;
        private System.Windows.Forms.TextBox txt_RCC_Pier_W1_supp_reac;
        private System.Windows.Forms.Label label357;
        private System.Windows.Forms.Label label358;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Mz1;
        private System.Windows.Forms.Label label359;
        private System.Windows.Forms.Label label360;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Mx1;
        private System.Windows.Forms.Label label361;
        private System.Windows.Forms.Label label362;
        private System.Windows.Forms.Label label363;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H7;
        private System.Windows.Forms.Label label364;
        private System.Windows.Forms.TextBox txt_RCC_Pier_gama_c;
        private System.Windows.Forms.Label label365;
        private System.Windows.Forms.Label label366;
        private System.Windows.Forms.TextBox txt_RCC_Pier_vehi_load;
        private System.Windows.Forms.Label label367;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NR;
        private System.Windows.Forms.Label label368;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NP;
        private System.Windows.Forms.Label label369;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Label label372;
        private System.Windows.Forms.TextBox txt_RCC_Pier_fck_2;
        private System.Windows.Forms.Label label373;
        private System.Windows.Forms.Label label374;
        private System.Windows.Forms.Label label375;
        private System.Windows.Forms.Label label376;
        private System.Windows.Forms.Label label377;
        private System.Windows.Forms.Label label378;
        private System.Windows.Forms.Label label379;
        private System.Windows.Forms.TextBox txt_RCC_Pier_fy2;
        private System.Windows.Forms.Label label380;
        private System.Windows.Forms.Label label381;
        private System.Windows.Forms.Label label382;
        private System.Windows.Forms.TextBox txt_RCC_Pier_D;
        private System.Windows.Forms.Label label383;
        private System.Windows.Forms.Label label384;
        private System.Windows.Forms.TextBox txt_RCC_Pier_b;
        private System.Windows.Forms.TextBox txt_RCC_Pier_d_dash;
        private System.Windows.Forms.Label label385;
        private System.Windows.Forms.TextBox txt_RCC_Pier_p1;
        private System.Windows.Forms.Label label386;
        private System.Windows.Forms.TextBox txt_RCC_Pier_p2;
        private System.Windows.Forms.Label label387;
        private System.Windows.Forms.Label label389;
        private System.Windows.Forms.Label label390;
        private System.Windows.Forms.Label label391;
        private System.Windows.Forms.Label label392;
        private System.Windows.Forms.Label label393;
        private System.Windows.Forms.Label label394;
        private System.Windows.Forms.Label label395;
        private System.Windows.Forms.Label label396;
        private System.Windows.Forms.Label label397;
        private System.Windows.Forms.Label label398;
        private System.Windows.Forms.Label label403;
        private System.Windows.Forms.Label label404;
        private System.Windows.Forms.Label label405;
        private System.Windows.Forms.Label label406;
        private System.Windows.Forms.Label label407;
        private System.Windows.Forms.Label label408;
        private System.Windows.Forms.Label label409;
        private System.Windows.Forms.Label label410;
        private System.Windows.Forms.Label label411;
        private System.Windows.Forms.Label label412;
        private System.Windows.Forms.Label label413;
        private System.Windows.Forms.Label label414;
        private System.Windows.Forms.Label label415;
        private System.Windows.Forms.Label label416;
        private System.Windows.Forms.Label label417;
        private System.Windows.Forms.Label label418;
        private System.Windows.Forms.Label label419;
        private System.Windows.Forms.Label label420;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Label label421;
        private System.Windows.Forms.Label label422;
        private System.Windows.Forms.Label label423;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H2;
        private System.Windows.Forms.Label label424;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B4;
        private System.Windows.Forms.Label label425;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B3;
        private System.Windows.Forms.Label label426;
        private System.Windows.Forms.GroupBox groupBox43;
        private System.Windows.Forms.Label label427;
        private System.Windows.Forms.Label label428;
        private System.Windows.Forms.Label label429;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H1;
        private System.Windows.Forms.Label label430;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B2;
        private System.Windows.Forms.Label label431;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B1;
        private System.Windows.Forms.Label label432;
        private System.Windows.Forms.TextBox txt_RCC_Pier_overall_height;
        private System.Windows.Forms.Label label433;
        private System.Windows.Forms.TextBox txt_RCC_Pier___B;
        private System.Windows.Forms.Label label434;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B13;
        private System.Windows.Forms.Label label435;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B12;
        private System.Windows.Forms.Label label436;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B11;
        private System.Windows.Forms.Label label437;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B10;
        private System.Windows.Forms.Label label438;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B9;
        private System.Windows.Forms.Label label439;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H6;
        private System.Windows.Forms.Label label440;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H5;
        private System.Windows.Forms.Label label441;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B8;
        private System.Windows.Forms.Label label445;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H4;
        private System.Windows.Forms.Label label446;
        private System.Windows.Forms.TextBox txt_RCC_Pier_H3;
        private System.Windows.Forms.Label label447;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B7;
        private System.Windows.Forms.Label label448;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Form_Lev;
        private System.Windows.Forms.Label label449;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL5;
        private System.Windows.Forms.Label label450;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL4;
        private System.Windows.Forms.Label label451;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL3;
        private System.Windows.Forms.Label label452;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL2;
        private System.Windows.Forms.Label label453;
        private System.Windows.Forms.TextBox txt_RCC_Pier_RL1;
        private System.Windows.Forms.Label label454;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B6;
        private System.Windows.Forms.Label label455;
        private System.Windows.Forms.TextBox txt_RCC_Pier_B5;
        private System.Windows.Forms.Label label456;
        private System.Windows.Forms.TextBox txt_RCC_Pier_DS;
        private System.Windows.Forms.Label label457;
        private System.Windows.Forms.TextBox txt_RCC_Pier_DMG;
        private System.Windows.Forms.Label label458;
        private System.Windows.Forms.TextBox txt_RCC_Pier_NMG;
        private System.Windows.Forms.Label label459;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Hp;
        private System.Windows.Forms.Label label460;
        private System.Windows.Forms.TextBox txt_RCC_Pier_Wp;
        private System.Windows.Forms.Label label461;
        private System.Windows.Forms.TextBox txt_RCC_Pier__B;
        private System.Windows.Forms.Label label462;
        private System.Windows.Forms.TextBox txt_RCC_Pier_CW;
        private System.Windows.Forms.Label label463;
        private System.Windows.Forms.TextBox txt_RCC_Pier_L;
        private System.Windows.Forms.Label label464;
        private System.Windows.Forms.TabPage tab_des_form2;
        private System.Windows.Forms.Label label465;
        private System.Windows.Forms.Label label466;
        private System.Windows.Forms.Label label467;
        private System.Windows.Forms.Label label468;
        private System.Windows.Forms.Label label469;
        private System.Windows.Forms.Label label470;
        private System.Windows.Forms.Label label473;
        private System.Windows.Forms.Label label474;
        private System.Windows.Forms.Label label475;
        private System.Windows.Forms.Label label476;
        private System.Windows.Forms.Label label477;
        private System.Windows.Forms.ComboBox cmb_pier_2_k;
        private System.Windows.Forms.TextBox txt_pier_2_SBC;
        private System.Windows.Forms.Label label478;
        private System.Windows.Forms.TextBox txt_pier_2_Itc;
        private System.Windows.Forms.Label label482;
        private System.Windows.Forms.TextBox txt_pier_2_Vr;
        private System.Windows.Forms.Label label483;
        private System.Windows.Forms.TextBox txt_pier_2_LL;
        private System.Windows.Forms.Label label484;
        private System.Windows.Forms.TextBox txt_pier_2_CF;
        private System.Windows.Forms.Label label485;
        private System.Windows.Forms.TextBox txt_pier_2_k;
        private System.Windows.Forms.Label label486;
        private System.Windows.Forms.TextBox txt_pier_2_V;
        private System.Windows.Forms.Label label487;
        private System.Windows.Forms.TextBox txt_pier_2_HHF;
        private System.Windows.Forms.Label label488;
        private System.Windows.Forms.TextBox txt_pier_2_SC;
        private System.Windows.Forms.Label label489;
        private System.Windows.Forms.TextBox txt_pier_2_PD;
        private System.Windows.Forms.Label label490;
        private System.Windows.Forms.TextBox txt_pier_2_PML;
        private System.Windows.Forms.Label label491;
        private System.Windows.Forms.TextBox txt_pier_2_PL;
        private System.Windows.Forms.Label label492;
        private System.Windows.Forms.Label label493;
        private System.Windows.Forms.TextBox txt_pier_2_APD;
        private System.Windows.Forms.Label label494;
        private System.Windows.Forms.TextBox txt_pier_2_B16;
        private System.Windows.Forms.Label label495;
        private System.Windows.Forms.TextBox txt_pier_2_P3;
        private System.Windows.Forms.Label label496;
        private System.Windows.Forms.Label label497;
        private System.Windows.Forms.TextBox txt_pier_2_P2;
        private System.Windows.Forms.TabPage tab_des_Diagram;
        private System.Windows.Forms.PictureBox pic_pier_interactive_diagram;
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
        private System.Windows.Forms.GroupBox groupBox60;
        private System.Windows.Forms.Label label268;
        private System.Windows.Forms.Label label269;
        private System.Windows.Forms.GroupBox groupBox61;
        private System.Windows.Forms.Label label270;
        private System.Windows.Forms.Label label271;
        private System.Windows.Forms.Label label272;
        private System.Windows.Forms.Label label273;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label274;
        private System.Windows.Forms.Label label275;
        private System.Windows.Forms.TextBox txt_Ana_dead_cross_max_shear;
        private System.Windows.Forms.TextBox txt_Ana_dead_cross_max_moment;
        private System.Windows.Forms.TextBox txt_ana_LLSR;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox62;
        private System.Windows.Forms.GroupBox groupBox63;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label276;
        private System.Windows.Forms.Label label277;
        private System.Windows.Forms.TextBox txt_final_Mz;
        public System.Windows.Forms.TextBox txt_max_Mz_kN;
        private System.Windows.Forms.GroupBox groupBox64;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox txt_final_Mx;
        public System.Windows.Forms.TextBox txt_max_Mx_kN;
        private System.Windows.Forms.Label label278;
        private System.Windows.Forms.Label label279;
        private System.Windows.Forms.GroupBox groupBox65;
        public System.Windows.Forms.TextBox txt_final_vert_rec_kN;
        private System.Windows.Forms.Label label280;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox txt_final_vert_reac;
        private System.Windows.Forms.Label label281;
        private System.Windows.Forms.GroupBox groupBox66;
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
        private System.Windows.Forms.GroupBox groupBox67;
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
        private System.Windows.Forms.GroupBox groupBox68;
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
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.TabPage tab_cs_diagram;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RichTextBox rtb_sections;
        private System.Windows.Forms.Button btn_Show_Section_Resulf;
        private System.Windows.Forms.TabControl tabControl6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label226;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label228;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.Label label230;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label232;
        private System.Windows.Forms.Label label233;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label237;
        private System.Windows.Forms.Label label238;
        private System.Windows.Forms.Label lbl_factor;
        private System.Windows.Forms.Label label239;
        private System.Windows.Forms.TextBox txt_Ana_LL_factor;
        private System.Windows.Forms.Label label240;
        private System.Windows.Forms.TextBox txt_Ana_DL_factor;
        private System.Windows.Forms.ComboBox cmb_tab2_strand_data;
        private System.Windows.Forms.Label label243;
        private System.Windows.Forms.ComboBox cmb_tab2_nc;
        private System.Windows.Forms.Label label241;
        private System.Windows.Forms.Label label242;
        private System.Windows.Forms.Label label245;
        private System.Windows.Forms.Label label246;
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
        private System.Windows.Forms.TabPage tab_moving_data_british;
        private System.Windows.Forms.GroupBox groupBox45;
        private System.Windows.Forms.SplitContainer spc_HB;
        private System.Windows.Forms.GroupBox groupBox105;
        private System.Windows.Forms.DataGridView dgv_long_british_loads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn66;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn67;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn68;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn69;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn70;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn71;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn72;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn73;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn74;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn75;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn76;
        private System.Windows.Forms.Label label247;
        private System.Windows.Forms.GroupBox groupBox106;
        private System.Windows.Forms.DataGridView dgv_british_loads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn77;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn78;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn79;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn80;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn81;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn82;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column32;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column33;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column35;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column37;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column38;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column39;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column40;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column41;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column42;
        private System.Windows.Forms.Label label248;
        private System.Windows.Forms.Label lbl_HB;
        private System.Windows.Forms.GroupBox groupBox107;
        private System.Windows.Forms.GroupBox groupBox108;
        private System.Windows.Forms.RadioButton rbtn_Rail_Load;
        private System.Windows.Forms.RadioButton rbtn_HB;
        private System.Windows.Forms.RadioButton rbtn_HA;
        private System.Windows.Forms.RadioButton rbtn_HA_HB;
        private System.Windows.Forms.TextBox txt_LL_lf;
        private System.Windows.Forms.TextBox txt_LL_impf;
        private System.Windows.Forms.Label label249;
        private System.Windows.Forms.TextBox txt_no_lanes;
        private System.Windows.Forms.TextBox txt_ll_british_lgen;
        private System.Windows.Forms.Label label252;
        private System.Windows.Forms.GroupBox grb_ha;
        private System.Windows.Forms.GroupBox grb_ha_aply;
        private System.Windows.Forms.CheckBox chk_HA_7L;
        private System.Windows.Forms.CheckBox chk_HA_6L;
        private System.Windows.Forms.CheckBox chk_HA_10L;
        private System.Windows.Forms.CheckBox chk_HA_5L;
        private System.Windows.Forms.CheckBox chk_HA_9L;
        private System.Windows.Forms.CheckBox chk_HA_4L;
        private System.Windows.Forms.CheckBox chk_HA_8L;
        private System.Windows.Forms.CheckBox chk_HA_3L;
        private System.Windows.Forms.CheckBox chk_HA_2L;
        private System.Windows.Forms.CheckBox chk_HA_1L;
        private System.Windows.Forms.TextBox txt_HA_CON;
        private System.Windows.Forms.Label label255;
        private System.Windows.Forms.TextBox txt_HA_UDL;
        private System.Windows.Forms.Label label256;
        private System.Windows.Forms.Label label257;
        private System.Windows.Forms.Label label260;
        private System.Windows.Forms.GroupBox grb_hb;
        private System.Windows.Forms.GroupBox grb_hb_aply;
        private System.Windows.Forms.CheckBox chk_HB_7L;
        private System.Windows.Forms.CheckBox chk_HB_6L;
        private System.Windows.Forms.CheckBox chk_HB_10L;
        private System.Windows.Forms.CheckBox chk_HB_5L;
        private System.Windows.Forms.CheckBox chk_HB_9L;
        private System.Windows.Forms.CheckBox chk_HB_4L;
        private System.Windows.Forms.CheckBox chk_HB_8L;
        private System.Windows.Forms.CheckBox chk_HB_3L;
        private System.Windows.Forms.CheckBox chk_HB_2L;
        private System.Windows.Forms.CheckBox chk_HB_1L;
        private System.Windows.Forms.Label label261;
        private System.Windows.Forms.ComboBox cmb_HB;
        private System.Windows.Forms.TextBox txt_ll_british_incr;
        private System.Windows.Forms.TextBox txt_lane_width;
        private System.Windows.Forms.Label label262;
        private System.Windows.Forms.TextBox txt_deck_width;
        private System.Windows.Forms.Label label263;
        private System.Windows.Forms.Label label264;
        private System.Windows.Forms.Label label265;
        private System.Windows.Forms.Label label266;
        private System.Windows.Forms.GroupBox groupBox109;
        private System.Windows.Forms.ComboBox cmb_long_open_file;
        private System.Windows.Forms.Button btn_View_Moving_Load;
        private System.Windows.Forms.Button btn_view_report;
        private System.Windows.Forms.Button btn_view_data;
        private System.Windows.Forms.Button btn_view_structure;
        private System.Windows.Forms.CheckBox chk_HA;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label267;
        private System.Windows.Forms.Label label282;
        private System.Windows.Forms.TabPage tab_rcc_abutment;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_psc_new_design;
        private System.Windows.Forms.Button btn_psc_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label283;
        private System.Windows.Forms.Button btn_dwg_open_Pier;
        private System.Windows.Forms.Button btn_dwg_open_Cantilever;
        private System.Windows.Forms.Button btn_dwg_open_Counterfort;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage10;
        private BridgeAnalysisDesign.PSC_BoxGirder.UC_BoxGirder uC_BoxGirder1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
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
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.TabPage tabPage18;
        private UC_CableStayedDesign uC_CableStayedDesign1;
        private System.Windows.Forms.Label label504;
        private System.Windows.Forms.Label label503;
        private System.Windows.Forms.TextBox txt_vertical_cbl_min_dist;
        private System.Windows.Forms.TabPage tab_moving_data_indian;
        private System.Windows.Forms.Label label308;
        private System.Windows.Forms.Label label287;
        private System.Windows.Forms.TextBox txt_tower_Dt;
        private System.Windows.Forms.Label label310;
        private System.Windows.Forms.Label label307;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tower_Bt;
        private System.Windows.Forms.TextBox txt_cable_dia;
        private System.Windows.Forms.TabPage tab_moving_data_LRFD;
        private System.Windows.Forms.GroupBox groupBox79;
        private System.Windows.Forms.Label label299;
        private System.Windows.Forms.TextBox txt_dl_ll_comb;
        private System.Windows.Forms.Button btn_edit_load_combs;
        private System.Windows.Forms.CheckBox chk_self_indian;
        private System.Windows.Forms.Button btn_long_restore_ll;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txt_IRC_XINCR;
        private System.Windows.Forms.GroupBox groupBox47;
        private System.Windows.Forms.Label label305;
        private System.Windows.Forms.TextBox txt_LRFD_dl_ll_comb;
        private System.Windows.Forms.Button btn_LRFD_edit_load_combs;
        private System.Windows.Forms.CheckBox chk_LRFD_self_indian;
        private System.Windows.Forms.Button btn_LRFD_long_restore_ll;
        private System.Windows.Forms.GroupBox groupBox48;
        private System.Windows.Forms.Label label306;
        private System.Windows.Forms.DataGridView dgv_LRFD_long_loads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.Label label317;
        private System.Windows.Forms.Label label318;
        private System.Windows.Forms.GroupBox groupBox49;
        private System.Windows.Forms.Label label319;
        private System.Windows.Forms.DataGridView dgv_LRFD_long_liveloads;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn83;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn84;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn85;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn86;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn87;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn88;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn89;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn90;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn91;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn92;
        private System.Windows.Forms.Label label320;
        private System.Windows.Forms.TextBox txt_LRFD_LL_load_gen;
        private System.Windows.Forms.TextBox txt_LRFD_XINCR;
        private System.Windows.Forms.Button btn_cable_stayed_drawing;
        private System.Windows.Forms.PictureBox pcb_cables;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox2;
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
        private System.Windows.Forms.Label label315;
        private System.Windows.Forms.Label label316;
        private System.Windows.Forms.TextBox txt_RCC_Pier_tdia;
        private System.Windows.Forms.TextBox txt_RCC_Pier_rdia;
        private System.Windows.Forms.Label label321;
        private System.Windows.Forms.Label label507;
        private System.Windows.Forms.Label label471;
        private System.Windows.Forms.Label label472;
        private System.Windows.Forms.Label label479;
        private System.Windows.Forms.TextBox txt_pier_2_vspc;
        private System.Windows.Forms.Label label480;
        private System.Windows.Forms.TextBox txt_pier_2_vdia;
        private System.Windows.Forms.Label label481;
        private System.Windows.Forms.TextBox txt_pier_2_hdia;
        private System.Windows.Forms.Label label512;
        private System.Windows.Forms.Label label513;
        private System.Windows.Forms.Label label516;
        private System.Windows.Forms.TextBox txt_pier_2_ldia;
        private System.Windows.Forms.Label label517;
        private System.Windows.Forms.TextBox txt_pier_2_vlegs;
        private System.Windows.Forms.Label label1076;
        private System.Windows.Forms.TextBox txt_pier_2_hlegs;
        private System.Windows.Forms.Label label1077;
        private System.Windows.Forms.TextBox txt_pier_2_slegs;
        private System.Windows.Forms.Label label1078;
        private System.Windows.Forms.TextBox txt_pier_2_sdia;
        private System.Windows.Forms.Label label1079;
        private System.Windows.Forms.Button btn_construction_drawings;
        private System.Windows.Forms.TabControl tc_pier;
        private System.Windows.Forms.TabPage tab_PierWSM;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tab_PierPileLSM;
        private BridgeAnalysisDesign.Pier.UC_PierDesignLSM uC_PierDesignLSM1;
        private System.Windows.Forms.TabPage tab_PierOpenLSM;
        private System.Windows.Forms.TabControl tc_abutment;
        private System.Windows.Forms.TabPage tab_AbutmentLSM;
        private BridgeAnalysisDesign.Abutment.UC_RCC_Abut uC_RCC_Abut1;
        private System.Windows.Forms.TabPage tab_AbutmentOpenLSM;
        private BridgeAnalysisDesign.Abutment.UC_AbutmentOpenLS uC_AbutmentOpenLS1;
        private System.Windows.Forms.TabPage tab_AbutmentPileLSM;
        private BridgeAnalysisDesign.Abutment.UC_AbutmentPileLS uC_AbutmentPileLS1;
        private BridgeAnalysisDesign.Pier.UC_PierOpenLS uC_PierOpenLS1;
    }



}