namespace HydrologyWorkspace
{
    partial class frmHydrology
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
            if (!Save_Data(true)) return;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHydrology));
            this.sc_main = new System.Windows.Forms.SplitContainer();
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tab_create_project = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label107 = new System.Windows.Forms.Label();
            this.pcb_logo = new System.Windows.Forms.PictureBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.grb_survey_type = new System.Windows.Forms.GroupBox();
            this.cmb_transverse = new System.Windows.Forms.ComboBox();
            this.rbtn_transverse = new System.Windows.Forms.RadioButton();
            this.rbtn_bearing_line = new System.Windows.Forms.RadioButton();
            this.rbtn_total_station = new System.Windows.Forms.RadioButton();
            this.rbtn_survey_drawing = new System.Windows.Forms.RadioButton();
            this.rbtn_auto_level = new System.Windows.Forms.RadioButton();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.txt_Project_Name2 = new System.Windows.Forms.TextBox();
            this.txt_Working_Folder = new System.Windows.Forms.TextBox();
            this.btn_open_project = new System.Windows.Forms.Button();
            this.btn_tutor_vids = new System.Windows.Forms.Button();
            this.lbl_tutorial_note = new System.Windows.Forms.Label();
            this.pnl_tutorial = new System.Windows.Forms.Panel();
            this.btn_tutorial_example = new System.Windows.Forms.Button();
            this.btn_Update_Project_Data = new System.Windows.Forms.Button();
            this.btn_Refresh_Project_Data = new System.Windows.Forms.Button();
            this.btn_save_proj_data_file = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_create_project_directory = new System.Windows.Forms.CheckBox();
            this.btn_new_project = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_Project_Name = new System.Windows.Forms.TextBox();
            this.btn_survey_browse = new System.Windows.Forms.Button();
            this.lbl_select_survey = new System.Windows.Forms.Label();
            this.txt_survey_data = new System.Windows.Forms.TextBox();
            this.tab_satellite = new System.Windows.Forms.TabPage();
            this.btn_process_ground_data = new System.Windows.Forms.Button();
            this.btn_run_viewer = new System.Windows.Forms.Button();
            this.btn_run_explorer = new System.Windows.Forms.Button();
            this.btn_info_global_mapper = new System.Windows.Forms.Button();
            this.btn_run_global_mapper = new System.Windows.Forms.Button();
            this.btn_video_ground_data = new System.Windows.Forms.Button();
            this.btn_video_viewer = new System.Windows.Forms.Button();
            this.btn_video_explorer = new System.Windows.Forms.Button();
            this.btn_video_global_mapper = new System.Windows.Forms.Button();
            this.btn_video_google_earth = new System.Windows.Forms.Button();
            this.btn_run_google_earth = new System.Windows.Forms.Button();
            this.tab_survey = new System.Windows.Forms.TabPage();
            this.sc_survey_design = new System.Windows.Forms.SplitContainer();
            this.btn_land_record = new System.Windows.Forms.Button();
            this.btn_traverse_survey = new System.Windows.Forms.Button();
            this.btn_contour_modeling = new System.Windows.Forms.Button();
            this.btn_traingulation = new System.Windows.Forms.Button();
            this.btn_ground_modeling = new System.Windows.Forms.Button();
            this.tc_survey = new System.Windows.Forms.TabControl();
            this.tab_auto_level_halign = new System.Windows.Forms.TabPage();
            this.tc_auto_level_halign = new System.Windows.Forms.TabControl();
            this.tab_auto_level_prohalign_data = new System.Windows.Forms.TabPage();
            this.rtb_auto_level_prohalign = new System.Windows.Forms.RichTextBox();
            this.tab_auto_level_halign_data = new System.Windows.Forms.TabPage();
            this.rtb_auto_level_halign = new System.Windows.Forms.RichTextBox();
            this.tab_auto_traverse_data = new System.Windows.Forms.TabPage();
            this.rtb_auto_level_traverse = new System.Windows.Forms.RichTextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.txt_trav_align_string = new System.Windows.Forms.TextBox();
            this.txt_trav_align_model = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label136 = new System.Windows.Forms.Label();
            this.btn_halign_details_off = new System.Windows.Forms.Button();
            this.btn_halign_details_on = new System.Windows.Forms.Button();
            this.btn_halign_view = new System.Windows.Forms.Button();
            this.btn_halign_chainage_off = new System.Windows.Forms.Button();
            this.btn_halign_chainage_on = new System.Windows.Forms.Button();
            this.grb_auto_level_process_halign = new System.Windows.Forms.GroupBox();
            this.btn_HRP_halign_video = new System.Windows.Forms.Button();
            this.btn_HRP_halign_process = new System.Windows.Forms.Button();
            this.cmb_auto_halign = new System.Windows.Forms.ComboBox();
            this.tab_gm = new System.Windows.Forms.TabPage();
            this.chk_utm_conversion = new System.Windows.Forms.CheckBox();
            this.btn_DGM_open_gm_data = new System.Windows.Forms.Button();
            this.btn_DGM_open_survey_data = new System.Windows.Forms.Button();
            this.btn_DGM_create_model = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grb_from_file = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_select_settings = new System.Windows.Forms.TextBox();
            this.btn_browse_settings = new System.Windows.Forms.Button();
            this.lbl_text = new System.Windows.Forms.Label();
            this.txt_drawing_lib = new System.Windows.Forms.TextBox();
            this.btn_browse_lib = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.cmb_layer = new System.Windows.Forms.ComboBox();
            this.btn_delete_rows = new System.Windows.Forms.Button();
            this.dgv_all_data = new System.Windows.Forms.DataGridView();
            this.colDraw = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_draw_el = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_clr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chk_Draw_All = new System.Windows.Forms.CheckBox();
            this.btn_add_to_list = new System.Windows.Forms.Button();
            this.cmb_draw = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tab_trngu = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.grbGroundModeling = new System.Windows.Forms.GroupBox();
            this.txt_GMT_string = new System.Windows.Forms.TextBox();
            this.btn_Boundary = new System.Windows.Forms.Button();
            this.btn_GMT_OK = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_GMT_model = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lb_GMT_ModelAndStringName = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_GMT_DeSelect = new System.Windows.Forms.TextBox();
            this.txt_GMT_Select = new System.Windows.Forms.TextBox();
            this.btn_GMT_DeSelect = new System.Windows.Forms.Button();
            this.btn_GMT_Select = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_GMT_DeSelectAll = new System.Windows.Forms.Button();
            this.btn_GMT_SelectAll = new System.Windows.Forms.Button();
            this.tab_cont = new System.Windows.Forms.TabPage();
            this.chk_Contour_SURFACE = new System.Windows.Forms.CheckBox();
            this.chk_Contour_ELEV = new System.Windows.Forms.CheckBox();
            this.chk_Contour_C005 = new System.Windows.Forms.CheckBox();
            this.chk_Contour_C001 = new System.Windows.Forms.CheckBox();
            this.btn_contour_refresh = new System.Windows.Forms.Button();
            this.btn_draw_ground_surface = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btn_Contour_create_model = new System.Windows.Forms.Button();
            this.btn_Contour_draw_model = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Contour_ele_model = new System.Windows.Forms.TextBox();
            this.txt_Contour_ele_inc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Contour_ele_string = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txt_Contour_pri_model = new System.Windows.Forms.TextBox();
            this.txt_Contour_pri_inc = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Contour_pri_string = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_Contour_sec_model = new System.Windows.Forms.TextBox();
            this.txt_Contour_sec_inc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Contour_sec_string = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tab_strg_qty = new System.Windows.Forms.TabPage();
            this.tab_discrg_qty = new System.Windows.Forms.TabPage();
            this.tab_alignment = new System.Windows.Forms.TabPage();
            this.label34 = new System.Windows.Forms.Label();
            this.btn_ALGN_dtlsOff = new System.Windows.Forms.Button();
            this.btn_ALGN_dtlsOn = new System.Windows.Forms.Button();
            this.btn_ALGN_chnOff = new System.Windows.Forms.Button();
            this.btn_ALGN_chnOn = new System.Windows.Forms.Button();
            this.btn_ALGN_create = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.txt_ALGN_String = new System.Windows.Forms.TextBox();
            this.label137 = new System.Windows.Forms.Label();
            this.txt_ALGN_Model = new System.Windows.Forms.TextBox();
            this.label142 = new System.Windows.Forms.Label();
            this.lbl_survey_data = new System.Windows.Forms.Label();
            this.tab_trvrs = new System.Windows.Forms.TabPage();
            this.sc_traverse = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tc_traverse = new System.Windows.Forms.TabControl();
            this.tab_bowditch = new System.Windows.Forms.TabPage();
            this.rtb_bowditch_data = new System.Windows.Forms.RichTextBox();
            this.tab_transit = new System.Windows.Forms.TabPage();
            this.rtb_transit_data = new System.Windows.Forms.RichTextBox();
            this.tab_closed_link = new System.Windows.Forms.TabPage();
            this.rtb_closed_link_data = new System.Windows.Forms.RichTextBox();
            this.tab_edm = new System.Windows.Forms.TabPage();
            this.rtb_edm_data = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_transverse_report = new System.Windows.Forms.Button();
            this.btn_transverse_draw = new System.Windows.Forms.Button();
            this.btn_transverse_process = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rtb_transverse_report = new System.Windows.Forms.RichTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tab_land = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txt_draw_path = new System.Windows.Forms.TextBox();
            this.btn_Land_drawings = new System.Windows.Forms.Button();
            this.lst_land_drawings = new System.Windows.Forms.ListBox();
            this.tab_site_lvl_grd = new System.Windows.Forms.TabPage();
            this.tc_Site_Leveling_Grading = new System.Windows.Forms.TabControl();
            this.tab_site_step4 = new System.Windows.Forms.TabPage();
            this.tc_SLG_halign = new System.Windows.Forms.TabControl();
            this.tab_SLG_prohalign_data = new System.Windows.Forms.TabPage();
            this.rtb_SLG_prohalign = new System.Windows.Forms.RichTextBox();
            this.tab_SLG_halign_data = new System.Windows.Forms.TabPage();
            this.rtb_SLG_halign = new System.Windows.Forms.RichTextBox();
            this.tab_SLG_traverse_data = new System.Windows.Forms.TabPage();
            this.rtb_SLG_traverse_data = new System.Windows.Forms.RichTextBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.txt_SLG_trav_string = new System.Windows.Forms.TextBox();
            this.txt_SLG_trav_model = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SLG_dlts_OFF = new System.Windows.Forms.Button();
            this.btn_SLG_dlts_ON = new System.Windows.Forms.Button();
            this.btn_SLG_View_Halign = new System.Windows.Forms.Button();
            this.btn_SLG_chn_OFF = new System.Windows.Forms.Button();
            this.btn_SLG_chn_ON = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btn_SLG_halign_video = new System.Windows.Forms.Button();
            this.btn_SLG_halign_proceed = new System.Windows.Forms.Button();
            this.cmb_SLG_halign = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tab_site_step5 = new System.Windows.Forms.TabPage();
            this.sc_HDP_valign = new System.Windows.Forms.SplitContainer();
            this.tc_HDP_valign = new System.Windows.Forms.TabControl();
            this.tab_HDP_profile_opt = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.btn_HDP_profile_opt_process = new System.Windows.Forms.Button();
            this.tab_HDP_update_valign_data = new System.Windows.Forms.TabPage();
            this.btn_HDP_pro_opt_get_restore = new System.Windows.Forms.Button();
            this.label124 = new System.Windows.Forms.Label();
            this.btn_HDP_pro_opt_update_data_prev = new System.Windows.Forms.Button();
            this.label125 = new System.Windows.Forms.Label();
            this.btn_HDP_pro_opt_get_exst_lvls = new System.Windows.Forms.Button();
            this.label126 = new System.Windows.Forms.Label();
            this.cmb_HDP_pro_opt_exist_level_str = new System.Windows.Forms.ComboBox();
            this.cmb_HDP_pro_opt_Bridge_sections = new System.Windows.Forms.ComboBox();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.dgv_HDP_pro_opt_prev_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.txt_HDP_pro_opt_props_VCL = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.txt_HDP_pro_opt_props_hgt = new System.Windows.Forms.TextBox();
            this.btn_HDP_pro_opt_props_hgt = new System.Windows.Forms.Button();
            this.label127 = new System.Windows.Forms.Label();
            this.dgv_HDP_pro_opt_chns = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_HDP_pro_opt_update_data = new System.Windows.Forms.Button();
            this.btn_HDP_pro_opt_del_row_prev = new System.Windows.Forms.Button();
            this.btn_HDP_pro_opt_del_row = new System.Windows.Forms.Button();
            this.btn_HDP_pro_opt_insert_row_prev = new System.Windows.Forms.Button();
            this.btn_HDP_pro_opt_insert_row = new System.Windows.Forms.Button();
            this.tab_HDP_valign_design = new System.Windows.Forms.TabPage();
            this.label143 = new System.Windows.Forms.Label();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_HDP_Elevation = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.btn_HDP_Elevation = new System.Windows.Forms.Button();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_valign_open_vip_design = new System.Windows.Forms.Button();
            this.btn_HDP_Get_Valign_Data = new System.Windows.Forms.Button();
            this.btn_valign_new_vip_design = new System.Windows.Forms.Button();
            this.btn_HDP_valign_open_viewer = new System.Windows.Forms.Button();
            this.btn_valign_process = new System.Windows.Forms.Button();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.btn_HDP_Valign_video = new System.Windows.Forms.Button();
            this.btn_HDP_Valign_proceed = new System.Windows.Forms.Button();
            this.cmb_HDP_Valign = new System.Windows.Forms.ComboBox();
            this.txt_HDP_ValignData = new System.Windows.Forms.TextBox();
            this.btn_HDP_valign_draw_selected_profile = new System.Windows.Forms.Button();
            this.btn_HDP_valign_draw_glongsec = new System.Windows.Forms.Button();
            this.btn_HDP_valign_draw_vertical_profile = new System.Windows.Forms.Button();
            this.btn_valign_grid_on = new System.Windows.Forms.Button();
            this.btn_valign_details_off = new System.Windows.Forms.Button();
            this.btn_HDP_create_ground_longsec = new System.Windows.Forms.Button();
            this.btn_valign_details_on = new System.Windows.Forms.Button();
            this.btn_valign_grid_off = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tab_site_step6 = new System.Windows.Forms.TabPage();
            this.tc_SLG_cross_secion = new System.Windows.Forms.TabControl();
            this.tab_HDP_define_cross_section = new System.Windows.Forms.TabPage();
            this.sc_SLG_cross_section = new System.Windows.Forms.SplitContainer();
            this.sc_offset = new System.Windows.Forms.SplitContainer();
            this.dgv_offset_service = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btn_offset_delete = new System.Windows.Forms.Button();
            this.btn_offset_insert = new System.Windows.Forms.Button();
            this.btn_offset_add = new System.Windows.Forms.Button();
            this.lbl_off_2 = new System.Windows.Forms.Label();
            this.tab_HDP_interface = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button8 = new System.Windows.Forms.Button();
            this.grb_hill_road = new System.Windows.Forms.GroupBox();
            this.rbtn_HRP_interface_2 = new System.Windows.Forms.RadioButton();
            this.rbtn_HRP_interface_1 = new System.Windows.Forms.RadioButton();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tab_HDP_create_cross_section = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.btn_cross_section_create = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsmi_HDP_cs_First = new System.Windows.Forms.ToolStripButton();
            this.tsmi_HDP_cs_backword = new System.Windows.Forms.ToolStripButton();
            this.tsmi_HDP_cs_Forward = new System.Windows.Forms.ToolStripButton();
            this.tsmi_HDP_cs_Last = new System.Windows.Forms.ToolStripButton();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmb_HDP_cs_sel_chainage = new System.Windows.Forms.ComboBox();
            this.cmb_HDP_cs_sel_strings = new System.Windows.Forms.ComboBox();
            this.btn_cross_section_draw = new System.Windows.Forms.Button();
            this.btn_cross_section_process = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.tab_site_step7 = new System.Windows.Forms.TabPage();
            this.sc_volume = new System.Windows.Forms.SplitContainer();
            this.Volume_HDP_Data = new System.Windows.Forms.RichTextBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.pnl_INT_volume = new System.Windows.Forms.Panel();
            this.label69 = new System.Windows.Forms.Label();
            this.cmb_volume_strings = new System.Windows.Forms.ComboBox();
            this.btn_HDP_volume_process = new System.Windows.Forms.Button();
            this.btn_HDP_volume_masshaul = new System.Windows.Forms.Button();
            this.sc_volume_rep = new System.Windows.Forms.SplitContainer();
            this.Volume_HDP_Report = new System.Windows.Forms.RichTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tab_site_step8 = new System.Windows.Forms.TabPage();
            this.tc_HDP_drawings = new System.Windows.Forms.TabControl();
            this.tab_align_sch = new System.Windows.Forms.TabPage();
            this.btn_process_diagram_HDP = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tab_Plan = new System.Windows.Forms.TabPage();
            this.btn_HDP_plan_process = new System.Windows.Forms.Button();
            this.tab_Profile = new System.Windows.Forms.TabPage();
            this.pnl_INT_str_profile = new System.Windows.Forms.Panel();
            this.label134 = new System.Windows.Forms.Label();
            this.cmb_INT_profile_strings = new System.Windows.Forms.ComboBox();
            this.btn_HDP_profile_process = new System.Windows.Forms.Button();
            this.tab_xsec = new System.Windows.Forms.TabPage();
            this.pnl_INT_str_cs = new System.Windows.Forms.Panel();
            this.label135 = new System.Windows.Forms.Label();
            this.cmb_INT_cross_strings = new System.Windows.Forms.ComboBox();
            this.btn_HDP_cross_process = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.tab_site_step9 = new System.Windows.Forms.TabPage();
            this.btn_HDP_survey_view = new System.Windows.Forms.Button();
            this.btn_HDP_view_drawings = new System.Windows.Forms.Button();
            this.btn_HDP_view_report_file = new System.Windows.Forms.Button();
            this.btn_HDP_open_project_file = new System.Windows.Forms.Button();
            this.btn_HDP_create_project_file = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.rtb_project_data = new System.Windows.Forms.RichTextBox();
            this.btn_HDP_process_project_file = new System.Windows.Forms.Button();
            this.txt_HDP_file = new System.Windows.Forms.TextBox();
            this.tab_disnet = new System.Windows.Forms.TabPage();
            this.tc_disnet = new System.Windows.Forms.TabControl();
            this.tab_create_loops = new System.Windows.Forms.TabPage();
            this.btn_disnet_refresh_loops = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cmb_disnet_pipenet_model = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.btn_disnet_create_loop = new System.Windows.Forms.Button();
            this.btn_disnet_nodal_ground = new System.Windows.Forms.Button();
            this.lst_disnet_strings = new System.Windows.Forms.ListBox();
            this.tab_pipe_network = new System.Windows.Forms.TabPage();
            this.tab_hydrology = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btn_crdn_off = new System.Windows.Forms.Button();
            this.btn_dtls_off = new System.Windows.Forms.Button();
            this.btn_crdn_on = new System.Windows.Forms.Button();
            this.btn_dtls_on = new System.Windows.Forms.Button();
            this.btn_chn_off = new System.Windows.Forms.Button();
            this.btn_chn_on = new System.Windows.Forms.Button();
            this.btn_draw_model_strings = new System.Windows.Forms.Button();
            this.utM_Conversion1 = new HEADS_Project_Mode.DataFroms.UTM_Conversion();
            this.uC_Stockpile = new HEADS_Site_Projects.Controls.UC_QuantityMeasurement();
            this.uC_Discharge = new HEADS_Site_Projects.Controls.UC_QuantityMeasurement();
            this.uC_LandRecord1 = new HEADS_Site_Projects.Controls.UC_LandRecord();
            this.vdsC_DOC_Land = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.design_Profile_Optimization1 = new HEADS_Project_Mode.DataFroms.Design_Project.Design_Profile_Optimization();
            this.vdsC_HDP_Valign = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.uC_Dyke_CrossSection1 = new HEADS_Site_Projects.Controls.UC_Dyke_CrossSection();
            this.uC_Canal_CrossSection1 = new HEADS_Site_Projects.Controls.UC_Canal_CrossSection();
            this.vdsC_DOC_river_canal = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.interface_HDP = new HEADS_Project_Mode.DataFroms.Interface();
            this.interfaceNote2 = new HEADS_Project_Mode.DataFroms.InterfaceNote();
            this.vdsC_HDP_CrossSect = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.vdsC_Volume = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.valign_Diagram_HDP = new HEADS_Project_Mode.DataFroms.Valign_Diagram();
            this.halign_Diagram_HDP = new HEADS_Project_Mode.DataFroms.Halign_Diagram();
            this.plan_Drawing_HDP = new HEADS_Project_Mode.DataFroms.Plan_Drawing();
            this.profile_Drawing_HDP = new HEADS_Project_Mode.DataFroms.Profile_Drawing();
            this.cross_Section_Drawing_HDP = new HEADS_Project_Mode.DataFroms.Cross_Section_Drawing();
            this.uC_DisNet1 = new HEADS_Site_Projects.Controls.UC_DisNet();
            this.uC_StreamHydrology1 = new HEADS_Project_Mode.DataFroms.UC_StreamHydrology();
            this.vdsC_Main = new HEADS_Project_Mode.DataFroms.VDSC_DOC();
            this.sc_main.Panel1.SuspendLayout();
            this.sc_main.Panel2.SuspendLayout();
            this.sc_main.SuspendLayout();
            this.tc_main.SuspendLayout();
            this.tab_create_project.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).BeginInit();
            this.grb_survey_type.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.pnl_tutorial.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tab_satellite.SuspendLayout();
            this.tab_survey.SuspendLayout();
            this.sc_survey_design.Panel1.SuspendLayout();
            this.sc_survey_design.Panel2.SuspendLayout();
            this.sc_survey_design.SuspendLayout();
            this.tc_survey.SuspendLayout();
            this.tab_auto_level_halign.SuspendLayout();
            this.tc_auto_level_halign.SuspendLayout();
            this.tab_auto_level_prohalign_data.SuspendLayout();
            this.tab_auto_level_halign_data.SuspendLayout();
            this.tab_auto_traverse_data.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grb_auto_level_process_halign.SuspendLayout();
            this.tab_gm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grb_from_file.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all_data)).BeginInit();
            this.tab_trngu.SuspendLayout();
            this.grbGroundModeling.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tab_cont.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tab_strg_qty.SuspendLayout();
            this.tab_discrg_qty.SuspendLayout();
            this.tab_alignment.SuspendLayout();
            this.tab_trvrs.SuspendLayout();
            this.sc_traverse.Panel1.SuspendLayout();
            this.sc_traverse.Panel2.SuspendLayout();
            this.sc_traverse.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tc_traverse.SuspendLayout();
            this.tab_bowditch.SuspendLayout();
            this.tab_transit.SuspendLayout();
            this.tab_closed_link.SuspendLayout();
            this.tab_edm.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tab_land.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tab_site_lvl_grd.SuspendLayout();
            this.tc_Site_Leveling_Grading.SuspendLayout();
            this.tab_site_step4.SuspendLayout();
            this.tc_SLG_halign.SuspendLayout();
            this.tab_SLG_prohalign_data.SuspendLayout();
            this.tab_SLG_halign_data.SuspendLayout();
            this.tab_SLG_traverse_data.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tab_site_step5.SuspendLayout();
            this.sc_HDP_valign.Panel1.SuspendLayout();
            this.sc_HDP_valign.Panel2.SuspendLayout();
            this.sc_HDP_valign.SuspendLayout();
            this.tc_HDP_valign.SuspendLayout();
            this.tab_HDP_profile_opt.SuspendLayout();
            this.tab_HDP_update_valign_data.SuspendLayout();
            this.groupBox34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HDP_pro_opt_prev_data)).BeginInit();
            this.groupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HDP_pro_opt_chns)).BeginInit();
            this.tab_HDP_valign_design.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.tab_site_step6.SuspendLayout();
            this.tc_SLG_cross_secion.SuspendLayout();
            this.tab_HDP_define_cross_section.SuspendLayout();
            this.sc_SLG_cross_section.Panel1.SuspendLayout();
            this.sc_SLG_cross_section.Panel2.SuspendLayout();
            this.sc_SLG_cross_section.SuspendLayout();
            this.sc_offset.Panel1.SuspendLayout();
            this.sc_offset.Panel2.SuspendLayout();
            this.sc_offset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_offset_service)).BeginInit();
            this.panel13.SuspendLayout();
            this.tab_HDP_interface.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.grb_hill_road.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tab_HDP_create_cross_section.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tab_site_step7.SuspendLayout();
            this.sc_volume.Panel1.SuspendLayout();
            this.sc_volume.Panel2.SuspendLayout();
            this.sc_volume.SuspendLayout();
            this.panel14.SuspendLayout();
            this.pnl_INT_volume.SuspendLayout();
            this.sc_volume_rep.Panel1.SuspendLayout();
            this.sc_volume_rep.Panel2.SuspendLayout();
            this.sc_volume_rep.SuspendLayout();
            this.tab_site_step8.SuspendLayout();
            this.tc_HDP_drawings.SuspendLayout();
            this.tab_align_sch.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tab_Plan.SuspendLayout();
            this.tab_Profile.SuspendLayout();
            this.pnl_INT_str_profile.SuspendLayout();
            this.tab_xsec.SuspendLayout();
            this.pnl_INT_str_cs.SuspendLayout();
            this.tab_site_step9.SuspendLayout();
            this.tab_disnet.SuspendLayout();
            this.tc_disnet.SuspendLayout();
            this.tab_create_loops.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tab_pipe_network.SuspendLayout();
            this.tab_hydrology.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // sc_main
            // 
            this.sc_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_main.Location = new System.Drawing.Point(0, 0);
            this.sc_main.Name = "sc_main";
            // 
            // sc_main.Panel1
            // 
            this.sc_main.Panel1.Controls.Add(this.tc_main);
            // 
            // sc_main.Panel2
            // 
            this.sc_main.Panel2.Controls.Add(this.vdsC_Main);
            this.sc_main.Panel2.Controls.Add(this.panel8);
            this.sc_main.Size = new System.Drawing.Size(1036, 661);
            this.sc_main.SplitterDistance = 892;
            this.sc_main.TabIndex = 1;
            this.sc_main.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.sc_main_SplitterMoved);
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tab_create_project);
            this.tc_main.Controls.Add(this.tab_satellite);
            this.tc_main.Controls.Add(this.tab_survey);
            this.tc_main.Controls.Add(this.tab_trvrs);
            this.tc_main.Controls.Add(this.tab_land);
            this.tc_main.Controls.Add(this.tab_site_lvl_grd);
            this.tc_main.Controls.Add(this.tab_disnet);
            this.tc_main.Controls.Add(this.tab_hydrology);
            this.tc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_main.Location = new System.Drawing.Point(0, 0);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(890, 659);
            this.tc_main.TabIndex = 0;
            this.tc_main.SelectedIndexChanged += new System.EventHandler(this.tc_main_SelectedIndexChanged);
            // 
            // tab_create_project
            // 
            this.tab_create_project.Controls.Add(this.utM_Conversion1);
            this.tab_create_project.Controls.Add(this.panel9);
            this.tab_create_project.Controls.Add(this.grb_survey_type);
            this.tab_create_project.Controls.Add(this.groupBox21);
            this.tab_create_project.Controls.Add(this.btn_tutor_vids);
            this.tab_create_project.Controls.Add(this.lbl_tutorial_note);
            this.tab_create_project.Controls.Add(this.pnl_tutorial);
            this.tab_create_project.Controls.Add(this.btn_Update_Project_Data);
            this.tab_create_project.Controls.Add(this.btn_Refresh_Project_Data);
            this.tab_create_project.Controls.Add(this.btn_save_proj_data_file);
            this.tab_create_project.Controls.Add(this.groupBox1);
            this.tab_create_project.Location = new System.Drawing.Point(4, 22);
            this.tab_create_project.Name = "tab_create_project";
            this.tab_create_project.Padding = new System.Windows.Forms.Padding(3);
            this.tab_create_project.Size = new System.Drawing.Size(882, 633);
            this.tab_create_project.TabIndex = 0;
            this.tab_create_project.Text = "Create Project";
            this.tab_create_project.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.linkLabel1);
            this.panel9.Controls.Add(this.label107);
            this.panel9.Controls.Add(this.pcb_logo);
            this.panel9.Controls.Add(this.label106);
            this.panel9.Controls.Add(this.label108);
            this.panel9.Controls.Add(this.lbl_Title);
            this.panel9.Location = new System.Drawing.Point(15, 371);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(425, 273);
            this.panel9.TabIndex = 29;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(94, 213);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(229, 13);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Website : www.techsoftglobal.com";
            // 
            // label107
            // 
            this.label107.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(3, 14);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(418, 38);
            this.label107.TabIndex = 16;
            this.label107.Text = "ASTRA Pro";
            this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcb_logo
            // 
            this.pcb_logo.BackgroundImage = global::HydrologyWorkspace.Properties.Resources.techsoft_logo;
            this.pcb_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcb_logo.Location = new System.Drawing.Point(153, 113);
            this.pcb_logo.Name = "pcb_logo";
            this.pcb_logo.Size = new System.Drawing.Size(122, 87);
            this.pcb_logo.TabIndex = 17;
            this.pcb_logo.TabStop = false;
            // 
            // label106
            // 
            this.label106.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(3, 77);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(418, 34);
            this.label106.TabIndex = 16;
            this.label106.Text = "TechSOFT Engineering Services";
            this.label106.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label108
            // 
            this.label108.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.Blue;
            this.label108.Location = new System.Drawing.Point(25, 231);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(379, 35);
            this.label108.TabIndex = 16;
            this.label108.Text = "Email At : techsoft@consultant.com, dataflow@mail.com\r\n                 techsofti" +
    "nfra@gmail.com.com";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Title
            // 
            this.lbl_Title.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(3, 51);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(418, 25);
            this.lbl_Title.TabIndex = 16;
            this.lbl_Title.Text = "Stream Hydrology and Hydrograph";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grb_survey_type
            // 
            this.grb_survey_type.Controls.Add(this.cmb_transverse);
            this.grb_survey_type.Controls.Add(this.rbtn_transverse);
            this.grb_survey_type.Controls.Add(this.rbtn_bearing_line);
            this.grb_survey_type.Controls.Add(this.rbtn_total_station);
            this.grb_survey_type.Controls.Add(this.rbtn_survey_drawing);
            this.grb_survey_type.Controls.Add(this.rbtn_auto_level);
            this.grb_survey_type.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_survey_type.ForeColor = System.Drawing.Color.Black;
            this.grb_survey_type.Location = new System.Drawing.Point(7, 9);
            this.grb_survey_type.Name = "grb_survey_type";
            this.grb_survey_type.Size = new System.Drawing.Size(433, 68);
            this.grb_survey_type.TabIndex = 11;
            this.grb_survey_type.TabStop = false;
            this.grb_survey_type.Text = "Survey Data Type";
            this.grb_survey_type.Enter += new System.EventHandler(this.grb_survey_type_Enter);
            // 
            // cmb_transverse
            // 
            this.cmb_transverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_transverse.Enabled = false;
            this.cmb_transverse.FormattingEnabled = true;
            this.cmb_transverse.Items.AddRange(new object[] {
            "Bowditch",
            "Transit",
            "Closed-Link",
            "EDM"});
            this.cmb_transverse.Location = new System.Drawing.Point(86, 42);
            this.cmb_transverse.Name = "cmb_transverse";
            this.cmb_transverse.Size = new System.Drawing.Size(108, 21);
            this.cmb_transverse.TabIndex = 30;
            this.cmb_transverse.Visible = false;
            this.cmb_transverse.SelectedIndexChanged += new System.EventHandler(this.cmb_transverse_SelectedIndexChanged);
            // 
            // rbtn_transverse
            // 
            this.rbtn_transverse.AutoSize = true;
            this.rbtn_transverse.Location = new System.Drawing.Point(5, 43);
            this.rbtn_transverse.Name = "rbtn_transverse";
            this.rbtn_transverse.Size = new System.Drawing.Size(75, 17);
            this.rbtn_transverse.TabIndex = 8;
            this.rbtn_transverse.Text = "Traverse";
            this.rbtn_transverse.UseVisualStyleBackColor = true;
            this.rbtn_transverse.Visible = false;
            this.rbtn_transverse.CheckedChanged += new System.EventHandler(this.rbtn_survey_options_CheckedChanged);
            // 
            // rbtn_bearing_line
            // 
            this.rbtn_bearing_line.AutoSize = true;
            this.rbtn_bearing_line.Location = new System.Drawing.Point(200, 43);
            this.rbtn_bearing_line.Name = "rbtn_bearing_line";
            this.rbtn_bearing_line.Size = new System.Drawing.Size(96, 17);
            this.rbtn_bearing_line.TabIndex = 7;
            this.rbtn_bearing_line.Text = "Bearing Line";
            this.rbtn_bearing_line.UseVisualStyleBackColor = true;
            this.rbtn_bearing_line.Visible = false;
            this.rbtn_bearing_line.CheckedChanged += new System.EventHandler(this.rbtn_survey_options_CheckedChanged);
            // 
            // rbtn_total_station
            // 
            this.rbtn_total_station.AutoSize = true;
            this.rbtn_total_station.Checked = true;
            this.rbtn_total_station.Location = new System.Drawing.Point(5, 20);
            this.rbtn_total_station.Name = "rbtn_total_station";
            this.rbtn_total_station.Size = new System.Drawing.Size(92, 17);
            this.rbtn_total_station.TabIndex = 5;
            this.rbtn_total_station.TabStop = true;
            this.rbtn_total_station.Text = "Total Staion";
            this.rbtn_total_station.UseVisualStyleBackColor = true;
            this.rbtn_total_station.CheckedChanged += new System.EventHandler(this.rbtn_survey_options_CheckedChanged);
            // 
            // rbtn_survey_drawing
            // 
            this.rbtn_survey_drawing.AutoSize = true;
            this.rbtn_survey_drawing.Location = new System.Drawing.Point(248, 20);
            this.rbtn_survey_drawing.Name = "rbtn_survey_drawing";
            this.rbtn_survey_drawing.Size = new System.Drawing.Size(129, 17);
            this.rbtn_survey_drawing.TabIndex = 6;
            this.rbtn_survey_drawing.Text = "Drawing File in 3D";
            this.rbtn_survey_drawing.UseVisualStyleBackColor = true;
            this.rbtn_survey_drawing.CheckedChanged += new System.EventHandler(this.rbtn_survey_options_CheckedChanged);
            // 
            // rbtn_auto_level
            // 
            this.rbtn_auto_level.AutoSize = true;
            this.rbtn_auto_level.Location = new System.Drawing.Point(130, 20);
            this.rbtn_auto_level.Name = "rbtn_auto_level";
            this.rbtn_auto_level.Size = new System.Drawing.Size(85, 17);
            this.rbtn_auto_level.TabIndex = 6;
            this.rbtn_auto_level.Text = "Auto Level";
            this.rbtn_auto_level.UseVisualStyleBackColor = true;
            this.rbtn_auto_level.CheckedChanged += new System.EventHandler(this.rbtn_survey_options_CheckedChanged);
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label144);
            this.groupBox21.Controls.Add(this.label63);
            this.groupBox21.Controls.Add(this.txt_Project_Name2);
            this.groupBox21.Controls.Add(this.txt_Working_Folder);
            this.groupBox21.Controls.Add(this.btn_open_project);
            this.groupBox21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox21.ForeColor = System.Drawing.Color.Blue;
            this.groupBox21.Location = new System.Drawing.Point(7, 270);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(433, 64);
            this.groupBox21.TabIndex = 28;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Open Design";
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(10, 101);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(394, 26);
            this.label144.TabIndex = 7;
            this.label144.Text = "Open User\'s Project Folder \r\n(For the second time onwords in multiple sessions of" +
    " work)";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.Black;
            this.label63.Location = new System.Drawing.Point(3, 17);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(93, 13);
            this.label63.TabIndex = 7;
            this.label63.Text = "Project Name :";
            // 
            // txt_Project_Name2
            // 
            this.txt_Project_Name2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Project_Name2.ForeColor = System.Drawing.Color.Black;
            this.txt_Project_Name2.Location = new System.Drawing.Point(6, 34);
            this.txt_Project_Name2.Name = "txt_Project_Name2";
            this.txt_Project_Name2.ReadOnly = true;
            this.txt_Project_Name2.Size = new System.Drawing.Size(347, 21);
            this.txt_Project_Name2.TabIndex = 6;
            // 
            // txt_Working_Folder
            // 
            this.txt_Working_Folder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Working_Folder.ForeColor = System.Drawing.Color.Black;
            this.txt_Working_Folder.Location = new System.Drawing.Point(6, 64);
            this.txt_Working_Folder.Name = "txt_Working_Folder";
            this.txt_Working_Folder.ReadOnly = true;
            this.txt_Working_Folder.Size = new System.Drawing.Size(347, 21);
            this.txt_Working_Folder.TabIndex = 6;
            // 
            // btn_open_project
            // 
            this.btn_open_project.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_project.ForeColor = System.Drawing.Color.Black;
            this.btn_open_project.Location = new System.Drawing.Point(359, 32);
            this.btn_open_project.Name = "btn_open_project";
            this.btn_open_project.Size = new System.Drawing.Size(61, 23);
            this.btn_open_project.TabIndex = 12;
            this.btn_open_project.Text = "Browse";
            this.btn_open_project.UseVisualStyleBackColor = true;
            this.btn_open_project.Click += new System.EventHandler(this.btn_open_project_Click);
            // 
            // btn_tutor_vids
            // 
            this.btn_tutor_vids.Location = new System.Drawing.Point(25, 120);
            this.btn_tutor_vids.Name = "btn_tutor_vids";
            this.btn_tutor_vids.Size = new System.Drawing.Size(176, 23);
            this.btn_tutor_vids.TabIndex = 24;
            this.btn_tutor_vids.Text = "View Tutorial Video";
            this.btn_tutor_vids.UseVisualStyleBackColor = true;
            this.btn_tutor_vids.Visible = false;
            this.btn_tutor_vids.Click += new System.EventHandler(this.btn_tutor_vids_Click);
            // 
            // lbl_tutorial_note
            // 
            this.lbl_tutorial_note.AutoSize = true;
            this.lbl_tutorial_note.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tutorial_note.ForeColor = System.Drawing.Color.Red;
            this.lbl_tutorial_note.Location = new System.Drawing.Point(11, 100);
            this.lbl_tutorial_note.Name = "lbl_tutorial_note";
            this.lbl_tutorial_note.Size = new System.Drawing.Size(423, 14);
            this.lbl_tutorial_note.TabIndex = 27;
            this.lbl_tutorial_note.Text = "Note : In this Trial Version user can only run Tutorial Example Data";
            this.lbl_tutorial_note.Visible = false;
            // 
            // pnl_tutorial
            // 
            this.pnl_tutorial.Controls.Add(this.btn_tutorial_example);
            this.pnl_tutorial.Location = new System.Drawing.Point(243, 120);
            this.pnl_tutorial.Name = "pnl_tutorial";
            this.pnl_tutorial.Size = new System.Drawing.Size(177, 25);
            this.pnl_tutorial.TabIndex = 26;
            // 
            // btn_tutorial_example
            // 
            this.btn_tutorial_example.Location = new System.Drawing.Point(0, 1);
            this.btn_tutorial_example.Name = "btn_tutorial_example";
            this.btn_tutorial_example.Size = new System.Drawing.Size(176, 23);
            this.btn_tutorial_example.TabIndex = 14;
            this.btn_tutorial_example.Text = "Open Tutorial Example Data";
            this.btn_tutorial_example.UseVisualStyleBackColor = true;
            this.btn_tutorial_example.Click += new System.EventHandler(this.btn_tutorial_example_Click);
            // 
            // btn_Update_Project_Data
            // 
            this.btn_Update_Project_Data.Location = new System.Drawing.Point(15, 340);
            this.btn_Update_Project_Data.Name = "btn_Update_Project_Data";
            this.btn_Update_Project_Data.Size = new System.Drawing.Size(157, 23);
            this.btn_Update_Project_Data.TabIndex = 25;
            this.btn_Update_Project_Data.Text = "Update Project Data File";
            this.btn_Update_Project_Data.UseVisualStyleBackColor = true;
            this.btn_Update_Project_Data.Click += new System.EventHandler(this.btn_Update_Project_Data_Click);
            // 
            // btn_Refresh_Project_Data
            // 
            this.btn_Refresh_Project_Data.Location = new System.Drawing.Point(178, 340);
            this.btn_Refresh_Project_Data.Name = "btn_Refresh_Project_Data";
            this.btn_Refresh_Project_Data.Size = new System.Drawing.Size(106, 23);
            this.btn_Refresh_Project_Data.TabIndex = 23;
            this.btn_Refresh_Project_Data.Text = "Refresh";
            this.btn_Refresh_Project_Data.UseVisualStyleBackColor = true;
            this.btn_Refresh_Project_Data.Click += new System.EventHandler(this.btn_Refresh_Project_Data_Click);
            // 
            // btn_save_proj_data_file
            // 
            this.btn_save_proj_data_file.Location = new System.Drawing.Point(290, 340);
            this.btn_save_proj_data_file.Name = "btn_save_proj_data_file";
            this.btn_save_proj_data_file.Size = new System.Drawing.Size(146, 23);
            this.btn_save_proj_data_file.TabIndex = 22;
            this.btn_save_proj_data_file.Text = "Save Project Data File";
            this.btn_save_proj_data_file.UseVisualStyleBackColor = true;
            this.btn_save_proj_data_file.Click += new System.EventHandler(this.btn_Save_Project_Data_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_create_project_directory);
            this.groupBox1.Controls.Add(this.btn_new_project);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.txt_Project_Name);
            this.groupBox1.Controls.Add(this.btn_survey_browse);
            this.groupBox1.Controls.Add(this.lbl_select_survey);
            this.groupBox1.Controls.Add(this.txt_survey_data);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(7, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 115);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Design";
            // 
            // chk_create_project_directory
            // 
            this.chk_create_project_directory.AutoSize = true;
            this.chk_create_project_directory.Checked = true;
            this.chk_create_project_directory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_create_project_directory.Location = new System.Drawing.Point(529, 367);
            this.chk_create_project_directory.Name = "chk_create_project_directory";
            this.chk_create_project_directory.Size = new System.Drawing.Size(185, 17);
            this.chk_create_project_directory.TabIndex = 9;
            this.chk_create_project_directory.Text = "Create Project Directory";
            this.chk_create_project_directory.UseVisualStyleBackColor = true;
            this.chk_create_project_directory.Visible = false;
            // 
            // btn_new_project
            // 
            this.btn_new_project.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new_project.ForeColor = System.Drawing.Color.Black;
            this.btn_new_project.Location = new System.Drawing.Point(301, 31);
            this.btn_new_project.Name = "btn_new_project";
            this.btn_new_project.Size = new System.Drawing.Size(120, 23);
            this.btn_new_project.TabIndex = 17;
            this.btn_new_project.Text = "Create Project";
            this.btn_new_project.UseVisualStyleBackColor = true;
            this.btn_new_project.Click += new System.EventHandler(this.btn_new_project_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(5, 17);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(93, 13);
            this.label31.TabIndex = 7;
            this.label31.Text = "Project Name :";
            // 
            // txt_Project_Name
            // 
            this.txt_Project_Name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Project_Name.ForeColor = System.Drawing.Color.Black;
            this.txt_Project_Name.Location = new System.Drawing.Point(8, 33);
            this.txt_Project_Name.Name = "txt_Project_Name";
            this.txt_Project_Name.Size = new System.Drawing.Size(287, 21);
            this.txt_Project_Name.TabIndex = 6;
            // 
            // btn_survey_browse
            // 
            this.btn_survey_browse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_survey_browse.ForeColor = System.Drawing.Color.Black;
            this.btn_survey_browse.Location = new System.Drawing.Point(359, 88);
            this.btn_survey_browse.Name = "btn_survey_browse";
            this.btn_survey_browse.Size = new System.Drawing.Size(61, 23);
            this.btn_survey_browse.TabIndex = 3;
            this.btn_survey_browse.Text = "Browse";
            this.btn_survey_browse.UseVisualStyleBackColor = true;
            this.btn_survey_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // lbl_select_survey
            // 
            this.lbl_select_survey.AutoSize = true;
            this.lbl_select_survey.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_select_survey.ForeColor = System.Drawing.Color.Black;
            this.lbl_select_survey.Location = new System.Drawing.Point(9, 57);
            this.lbl_select_survey.Name = "lbl_select_survey";
            this.lbl_select_survey.Size = new System.Drawing.Size(274, 26);
            this.lbl_select_survey.TabIndex = 0;
            this.lbl_select_survey.Text = "Select Survey Data File \r\n(For the first time in multiple sessions of work)";
            // 
            // txt_survey_data
            // 
            this.txt_survey_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_survey_data.ForeColor = System.Drawing.Color.Black;
            this.txt_survey_data.Location = new System.Drawing.Point(6, 89);
            this.txt_survey_data.Name = "txt_survey_data";
            this.txt_survey_data.Size = new System.Drawing.Size(347, 21);
            this.txt_survey_data.TabIndex = 1;
            // 
            // tab_satellite
            // 
            this.tab_satellite.Controls.Add(this.btn_process_ground_data);
            this.tab_satellite.Controls.Add(this.btn_run_viewer);
            this.tab_satellite.Controls.Add(this.btn_run_explorer);
            this.tab_satellite.Controls.Add(this.btn_info_global_mapper);
            this.tab_satellite.Controls.Add(this.btn_run_global_mapper);
            this.tab_satellite.Controls.Add(this.btn_video_ground_data);
            this.tab_satellite.Controls.Add(this.btn_video_viewer);
            this.tab_satellite.Controls.Add(this.btn_video_explorer);
            this.tab_satellite.Controls.Add(this.btn_video_global_mapper);
            this.tab_satellite.Controls.Add(this.btn_video_google_earth);
            this.tab_satellite.Controls.Add(this.btn_run_google_earth);
            this.tab_satellite.Location = new System.Drawing.Point(4, 22);
            this.tab_satellite.Name = "tab_satellite";
            this.tab_satellite.Padding = new System.Windows.Forms.Padding(3);
            this.tab_satellite.Size = new System.Drawing.Size(882, 633);
            this.tab_satellite.TabIndex = 1;
            this.tab_satellite.Text = "Satellite Applications";
            this.tab_satellite.UseVisualStyleBackColor = true;
            // 
            // btn_process_ground_data
            // 
            this.btn_process_ground_data.Location = new System.Drawing.Point(22, 367);
            this.btn_process_ground_data.Name = "btn_process_ground_data";
            this.btn_process_ground_data.Size = new System.Drawing.Size(221, 37);
            this.btn_process_ground_data.TabIndex = 8;
            this.btn_process_ground_data.Text = "Process Ground Data";
            this.btn_process_ground_data.UseVisualStyleBackColor = true;
            this.btn_process_ground_data.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_run_viewer
            // 
            this.btn_run_viewer.Location = new System.Drawing.Point(22, 313);
            this.btn_run_viewer.Name = "btn_run_viewer";
            this.btn_run_viewer.Size = new System.Drawing.Size(221, 37);
            this.btn_run_viewer.TabIndex = 7;
            this.btn_run_viewer.Text = "Design Horizontal Alignment";
            this.btn_run_viewer.UseVisualStyleBackColor = true;
            this.btn_run_viewer.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_run_explorer
            // 
            this.btn_run_explorer.Location = new System.Drawing.Point(22, 254);
            this.btn_run_explorer.Name = "btn_run_explorer";
            this.btn_run_explorer.Size = new System.Drawing.Size(221, 37);
            this.btn_run_explorer.TabIndex = 9;
            this.btn_run_explorer.Text = "Format Downloaded Ground Data";
            this.btn_run_explorer.UseVisualStyleBackColor = true;
            this.btn_run_explorer.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_info_global_mapper
            // 
            this.btn_info_global_mapper.Location = new System.Drawing.Point(511, 195);
            this.btn_info_global_mapper.Name = "btn_info_global_mapper";
            this.btn_info_global_mapper.Size = new System.Drawing.Size(179, 37);
            this.btn_info_global_mapper.TabIndex = 11;
            this.btn_info_global_mapper.Text = "Info";
            this.btn_info_global_mapper.UseVisualStyleBackColor = true;
            this.btn_info_global_mapper.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_run_global_mapper
            // 
            this.btn_run_global_mapper.Location = new System.Drawing.Point(22, 195);
            this.btn_run_global_mapper.Name = "btn_run_global_mapper";
            this.btn_run_global_mapper.Size = new System.Drawing.Size(221, 37);
            this.btn_run_global_mapper.TabIndex = 10;
            this.btn_run_global_mapper.Text = "Global Mapper";
            this.btn_run_global_mapper.UseVisualStyleBackColor = true;
            this.btn_run_global_mapper.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_video_ground_data
            // 
            this.btn_video_ground_data.Location = new System.Drawing.Point(287, 367);
            this.btn_video_ground_data.Name = "btn_video_ground_data";
            this.btn_video_ground_data.Size = new System.Drawing.Size(179, 37);
            this.btn_video_ground_data.TabIndex = 6;
            this.btn_video_ground_data.Text = "Play Video";
            this.btn_video_ground_data.UseVisualStyleBackColor = true;
            this.btn_video_ground_data.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_video_viewer
            // 
            this.btn_video_viewer.Location = new System.Drawing.Point(287, 313);
            this.btn_video_viewer.Name = "btn_video_viewer";
            this.btn_video_viewer.Size = new System.Drawing.Size(179, 37);
            this.btn_video_viewer.TabIndex = 2;
            this.btn_video_viewer.Text = "Play Video";
            this.btn_video_viewer.UseVisualStyleBackColor = true;
            this.btn_video_viewer.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_video_explorer
            // 
            this.btn_video_explorer.Location = new System.Drawing.Point(287, 254);
            this.btn_video_explorer.Name = "btn_video_explorer";
            this.btn_video_explorer.Size = new System.Drawing.Size(179, 37);
            this.btn_video_explorer.TabIndex = 1;
            this.btn_video_explorer.Text = "Play Video";
            this.btn_video_explorer.UseVisualStyleBackColor = true;
            this.btn_video_explorer.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_video_global_mapper
            // 
            this.btn_video_global_mapper.Location = new System.Drawing.Point(287, 195);
            this.btn_video_global_mapper.Name = "btn_video_global_mapper";
            this.btn_video_global_mapper.Size = new System.Drawing.Size(179, 37);
            this.btn_video_global_mapper.TabIndex = 3;
            this.btn_video_global_mapper.Text = "Play Video";
            this.btn_video_global_mapper.UseVisualStyleBackColor = true;
            this.btn_video_global_mapper.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_video_google_earth
            // 
            this.btn_video_google_earth.Location = new System.Drawing.Point(287, 136);
            this.btn_video_google_earth.Name = "btn_video_google_earth";
            this.btn_video_google_earth.Size = new System.Drawing.Size(179, 37);
            this.btn_video_google_earth.TabIndex = 5;
            this.btn_video_google_earth.Text = "Play Video";
            this.btn_video_google_earth.UseVisualStyleBackColor = true;
            this.btn_video_google_earth.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // btn_run_google_earth
            // 
            this.btn_run_google_earth.Location = new System.Drawing.Point(22, 136);
            this.btn_run_google_earth.Name = "btn_run_google_earth";
            this.btn_run_google_earth.Size = new System.Drawing.Size(221, 37);
            this.btn_run_google_earth.TabIndex = 4;
            this.btn_run_google_earth.Text = "Google Earth";
            this.btn_run_google_earth.UseVisualStyleBackColor = true;
            this.btn_run_google_earth.Click += new System.EventHandler(this.btn_run_google_earth_Click);
            // 
            // tab_survey
            // 
            this.tab_survey.Controls.Add(this.sc_survey_design);
            this.tab_survey.Location = new System.Drawing.Point(4, 22);
            this.tab_survey.Name = "tab_survey";
            this.tab_survey.Size = new System.Drawing.Size(882, 633);
            this.tab_survey.TabIndex = 2;
            this.tab_survey.Text = "Survey Data";
            this.tab_survey.UseVisualStyleBackColor = true;
            // 
            // sc_survey_design
            // 
            this.sc_survey_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_survey_design.Location = new System.Drawing.Point(0, 0);
            this.sc_survey_design.Name = "sc_survey_design";
            // 
            // sc_survey_design.Panel1
            // 
            this.sc_survey_design.Panel1.Controls.Add(this.btn_land_record);
            this.sc_survey_design.Panel1.Controls.Add(this.btn_traverse_survey);
            this.sc_survey_design.Panel1.Controls.Add(this.btn_contour_modeling);
            this.sc_survey_design.Panel1.Controls.Add(this.btn_traingulation);
            this.sc_survey_design.Panel1.Controls.Add(this.btn_ground_modeling);
            this.sc_survey_design.Panel1Collapsed = true;
            // 
            // sc_survey_design.Panel2
            // 
            this.sc_survey_design.Panel2.Controls.Add(this.tc_survey);
            this.sc_survey_design.Panel2.Controls.Add(this.lbl_survey_data);
            this.sc_survey_design.Size = new System.Drawing.Size(882, 633);
            this.sc_survey_design.SplitterDistance = 146;
            this.sc_survey_design.TabIndex = 1;
            // 
            // btn_land_record
            // 
            this.btn_land_record.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_land_record.Location = new System.Drawing.Point(0, 136);
            this.btn_land_record.Name = "btn_land_record";
            this.btn_land_record.Size = new System.Drawing.Size(146, 34);
            this.btn_land_record.TabIndex = 4;
            this.btn_land_record.Text = "Land Record";
            this.btn_land_record.UseVisualStyleBackColor = true;
            this.btn_land_record.Click += new System.EventHandler(this.btn_ground_modeling_Click);
            // 
            // btn_traverse_survey
            // 
            this.btn_traverse_survey.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_traverse_survey.Location = new System.Drawing.Point(0, 102);
            this.btn_traverse_survey.Name = "btn_traverse_survey";
            this.btn_traverse_survey.Size = new System.Drawing.Size(146, 34);
            this.btn_traverse_survey.TabIndex = 3;
            this.btn_traverse_survey.Text = "Traverse Survey";
            this.btn_traverse_survey.UseVisualStyleBackColor = true;
            this.btn_traverse_survey.Click += new System.EventHandler(this.btn_ground_modeling_Click);
            // 
            // btn_contour_modeling
            // 
            this.btn_contour_modeling.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_contour_modeling.Location = new System.Drawing.Point(0, 68);
            this.btn_contour_modeling.Name = "btn_contour_modeling";
            this.btn_contour_modeling.Size = new System.Drawing.Size(146, 34);
            this.btn_contour_modeling.TabIndex = 2;
            this.btn_contour_modeling.Text = "Contour Modeling";
            this.btn_contour_modeling.UseVisualStyleBackColor = true;
            this.btn_contour_modeling.Click += new System.EventHandler(this.btn_ground_modeling_Click);
            // 
            // btn_traingulation
            // 
            this.btn_traingulation.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_traingulation.Location = new System.Drawing.Point(0, 34);
            this.btn_traingulation.Name = "btn_traingulation";
            this.btn_traingulation.Size = new System.Drawing.Size(146, 34);
            this.btn_traingulation.TabIndex = 1;
            this.btn_traingulation.Text = "Triangulation Modeling";
            this.btn_traingulation.UseVisualStyleBackColor = true;
            this.btn_traingulation.Click += new System.EventHandler(this.btn_ground_modeling_Click);
            // 
            // btn_ground_modeling
            // 
            this.btn_ground_modeling.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ground_modeling.Location = new System.Drawing.Point(0, 0);
            this.btn_ground_modeling.Name = "btn_ground_modeling";
            this.btn_ground_modeling.Size = new System.Drawing.Size(146, 34);
            this.btn_ground_modeling.TabIndex = 0;
            this.btn_ground_modeling.Text = "Ground Modeling";
            this.btn_ground_modeling.UseVisualStyleBackColor = true;
            this.btn_ground_modeling.Click += new System.EventHandler(this.btn_ground_modeling_Click);
            // 
            // tc_survey
            // 
            this.tc_survey.Controls.Add(this.tab_auto_level_halign);
            this.tc_survey.Controls.Add(this.tab_gm);
            this.tc_survey.Controls.Add(this.tab_trngu);
            this.tc_survey.Controls.Add(this.tab_cont);
            this.tc_survey.Controls.Add(this.tab_strg_qty);
            this.tc_survey.Controls.Add(this.tab_discrg_qty);
            this.tc_survey.Controls.Add(this.tab_alignment);
            this.tc_survey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_survey.Location = new System.Drawing.Point(0, 35);
            this.tc_survey.Multiline = true;
            this.tc_survey.Name = "tc_survey";
            this.tc_survey.SelectedIndex = 0;
            this.tc_survey.Size = new System.Drawing.Size(882, 598);
            this.tc_survey.TabIndex = 0;
            this.tc_survey.SelectedIndexChanged += new System.EventHandler(this.tc_main_SelectedIndexChanged);
            // 
            // tab_auto_level_halign
            // 
            this.tab_auto_level_halign.Controls.Add(this.tc_auto_level_halign);
            this.tab_auto_level_halign.Controls.Add(this.panel3);
            this.tab_auto_level_halign.Controls.Add(this.grb_auto_level_process_halign);
            this.tab_auto_level_halign.Location = new System.Drawing.Point(4, 22);
            this.tab_auto_level_halign.Name = "tab_auto_level_halign";
            this.tab_auto_level_halign.Size = new System.Drawing.Size(874, 572);
            this.tab_auto_level_halign.TabIndex = 5;
            this.tab_auto_level_halign.Text = "Horizontal Alignment";
            this.tab_auto_level_halign.UseVisualStyleBackColor = true;
            // 
            // tc_auto_level_halign
            // 
            this.tc_auto_level_halign.Controls.Add(this.tab_auto_level_prohalign_data);
            this.tc_auto_level_halign.Controls.Add(this.tab_auto_level_halign_data);
            this.tc_auto_level_halign.Controls.Add(this.tab_auto_traverse_data);
            this.tc_auto_level_halign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_auto_level_halign.Location = new System.Drawing.Point(0, 53);
            this.tc_auto_level_halign.Name = "tc_auto_level_halign";
            this.tc_auto_level_halign.SelectedIndex = 0;
            this.tc_auto_level_halign.Size = new System.Drawing.Size(874, 360);
            this.tc_auto_level_halign.TabIndex = 64;
            // 
            // tab_auto_level_prohalign_data
            // 
            this.tab_auto_level_prohalign_data.Controls.Add(this.rtb_auto_level_prohalign);
            this.tab_auto_level_prohalign_data.Location = new System.Drawing.Point(4, 22);
            this.tab_auto_level_prohalign_data.Name = "tab_auto_level_prohalign_data";
            this.tab_auto_level_prohalign_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_auto_level_prohalign_data.Size = new System.Drawing.Size(866, 334);
            this.tab_auto_level_prohalign_data.TabIndex = 1;
            this.tab_auto_level_prohalign_data.Text = "PRO HALIGN";
            this.tab_auto_level_prohalign_data.UseVisualStyleBackColor = true;
            // 
            // rtb_auto_level_prohalign
            // 
            this.rtb_auto_level_prohalign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_auto_level_prohalign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_auto_level_prohalign.Location = new System.Drawing.Point(3, 3);
            this.rtb_auto_level_prohalign.Name = "rtb_auto_level_prohalign";
            this.rtb_auto_level_prohalign.Size = new System.Drawing.Size(860, 328);
            this.rtb_auto_level_prohalign.TabIndex = 63;
            this.rtb_auto_level_prohalign.Text = "";
            this.rtb_auto_level_prohalign.WordWrap = false;
            // 
            // tab_auto_level_halign_data
            // 
            this.tab_auto_level_halign_data.Controls.Add(this.rtb_auto_level_halign);
            this.tab_auto_level_halign_data.Location = new System.Drawing.Point(4, 22);
            this.tab_auto_level_halign_data.Name = "tab_auto_level_halign_data";
            this.tab_auto_level_halign_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_auto_level_halign_data.Size = new System.Drawing.Size(866, 334);
            this.tab_auto_level_halign_data.TabIndex = 0;
            this.tab_auto_level_halign_data.Text = "HALIGN";
            this.tab_auto_level_halign_data.UseVisualStyleBackColor = true;
            // 
            // rtb_auto_level_halign
            // 
            this.rtb_auto_level_halign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_auto_level_halign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_auto_level_halign.Location = new System.Drawing.Point(3, 3);
            this.rtb_auto_level_halign.Name = "rtb_auto_level_halign";
            this.rtb_auto_level_halign.Size = new System.Drawing.Size(860, 328);
            this.rtb_auto_level_halign.TabIndex = 62;
            this.rtb_auto_level_halign.Text = "";
            this.rtb_auto_level_halign.WordWrap = false;
            this.rtb_auto_level_halign.TextChanged += new System.EventHandler(this.rtb_auto_level_halign_TextChanged);
            // 
            // tab_auto_traverse_data
            // 
            this.tab_auto_traverse_data.Controls.Add(this.rtb_auto_level_traverse);
            this.tab_auto_traverse_data.Controls.Add(this.panel11);
            this.tab_auto_traverse_data.Location = new System.Drawing.Point(4, 22);
            this.tab_auto_traverse_data.Name = "tab_auto_traverse_data";
            this.tab_auto_traverse_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_auto_traverse_data.Size = new System.Drawing.Size(866, 334);
            this.tab_auto_traverse_data.TabIndex = 2;
            this.tab_auto_traverse_data.Text = "TRAVERSE ALIGNMENT";
            this.tab_auto_traverse_data.UseVisualStyleBackColor = true;
            // 
            // rtb_auto_level_traverse
            // 
            this.rtb_auto_level_traverse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_auto_level_traverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_auto_level_traverse.Location = new System.Drawing.Point(3, 42);
            this.rtb_auto_level_traverse.Name = "rtb_auto_level_traverse";
            this.rtb_auto_level_traverse.Size = new System.Drawing.Size(860, 289);
            this.rtb_auto_level_traverse.TabIndex = 63;
            this.rtb_auto_level_traverse.Text = "";
            this.rtb_auto_level_traverse.WordWrap = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.txt_trav_align_string);
            this.panel11.Controls.Add(this.txt_trav_align_model);
            this.panel11.Controls.Add(this.label25);
            this.panel11.Controls.Add(this.label24);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(3, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(860, 39);
            this.panel11.TabIndex = 64;
            // 
            // txt_trav_align_string
            // 
            this.txt_trav_align_string.Location = new System.Drawing.Point(278, 11);
            this.txt_trav_align_string.Name = "txt_trav_align_string";
            this.txt_trav_align_string.Size = new System.Drawing.Size(50, 21);
            this.txt_trav_align_string.TabIndex = 1;
            this.txt_trav_align_string.Text = "M001";
            // 
            // txt_trav_align_model
            // 
            this.txt_trav_align_model.Location = new System.Drawing.Point(92, 11);
            this.txt_trav_align_model.Name = "txt_trav_align_model";
            this.txt_trav_align_model.Size = new System.Drawing.Size(65, 21);
            this.txt_trav_align_model.TabIndex = 1;
            this.txt_trav_align_model.Text = "DESIGN";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(181, 14);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(91, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "STRING LABEL";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 14);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "MODEL NAME";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label136);
            this.panel3.Controls.Add(this.btn_halign_details_off);
            this.panel3.Controls.Add(this.btn_halign_details_on);
            this.panel3.Controls.Add(this.btn_halign_view);
            this.panel3.Controls.Add(this.btn_halign_chainage_off);
            this.panel3.Controls.Add(this.btn_halign_chainage_on);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 413);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(874, 159);
            this.panel3.TabIndex = 63;
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label136.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.ForeColor = System.Drawing.Color.Red;
            this.label136.Location = new System.Drawing.Point(57, 19);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(364, 28);
            this.label136.TabIndex = 65;
            this.label136.Text = "Note : After Finishing a New HALIGN Design user must \r\n            click on butto" +
    "n \"View HALIGN Design\"";
            // 
            // btn_halign_details_off
            // 
            this.btn_halign_details_off.Location = new System.Drawing.Point(244, 111);
            this.btn_halign_details_off.Name = "btn_halign_details_off";
            this.btn_halign_details_off.Size = new System.Drawing.Size(184, 28);
            this.btn_halign_details_off.TabIndex = 64;
            this.btn_halign_details_off.Text = "Details OFF";
            this.btn_halign_details_off.UseVisualStyleBackColor = true;
            this.btn_halign_details_off.Click += new System.EventHandler(this.btn_design_horizontal_Click);
            // 
            // btn_halign_details_on
            // 
            this.btn_halign_details_on.Location = new System.Drawing.Point(40, 112);
            this.btn_halign_details_on.Name = "btn_halign_details_on";
            this.btn_halign_details_on.Size = new System.Drawing.Size(184, 28);
            this.btn_halign_details_on.TabIndex = 63;
            this.btn_halign_details_on.Text = "Details ON";
            this.btn_halign_details_on.UseVisualStyleBackColor = true;
            this.btn_halign_details_on.Click += new System.EventHandler(this.btn_design_horizontal_Click);
            // 
            // btn_halign_view
            // 
            this.btn_halign_view.Location = new System.Drawing.Point(40, 52);
            this.btn_halign_view.Name = "btn_halign_view";
            this.btn_halign_view.Size = new System.Drawing.Size(184, 28);
            this.btn_halign_view.TabIndex = 60;
            this.btn_halign_view.Text = "View HALIGN Design";
            this.btn_halign_view.UseVisualStyleBackColor = true;
            this.btn_halign_view.Click += new System.EventHandler(this.btn_design_horizontal_Click);
            // 
            // btn_halign_chainage_off
            // 
            this.btn_halign_chainage_off.Location = new System.Drawing.Point(244, 82);
            this.btn_halign_chainage_off.Name = "btn_halign_chainage_off";
            this.btn_halign_chainage_off.Size = new System.Drawing.Size(184, 28);
            this.btn_halign_chainage_off.TabIndex = 61;
            this.btn_halign_chainage_off.Text = "Chainage OFF";
            this.btn_halign_chainage_off.UseVisualStyleBackColor = true;
            this.btn_halign_chainage_off.Click += new System.EventHandler(this.btn_design_horizontal_Click);
            // 
            // btn_halign_chainage_on
            // 
            this.btn_halign_chainage_on.Location = new System.Drawing.Point(40, 82);
            this.btn_halign_chainage_on.Name = "btn_halign_chainage_on";
            this.btn_halign_chainage_on.Size = new System.Drawing.Size(184, 28);
            this.btn_halign_chainage_on.TabIndex = 62;
            this.btn_halign_chainage_on.Text = "Chainage ON";
            this.btn_halign_chainage_on.UseVisualStyleBackColor = true;
            this.btn_halign_chainage_on.Click += new System.EventHandler(this.btn_design_horizontal_Click);
            // 
            // grb_auto_level_process_halign
            // 
            this.grb_auto_level_process_halign.Controls.Add(this.btn_HRP_halign_video);
            this.grb_auto_level_process_halign.Controls.Add(this.btn_HRP_halign_process);
            this.grb_auto_level_process_halign.Controls.Add(this.cmb_auto_halign);
            this.grb_auto_level_process_halign.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_auto_level_process_halign.Location = new System.Drawing.Point(0, 0);
            this.grb_auto_level_process_halign.Name = "grb_auto_level_process_halign";
            this.grb_auto_level_process_halign.Size = new System.Drawing.Size(874, 53);
            this.grb_auto_level_process_halign.TabIndex = 61;
            this.grb_auto_level_process_halign.TabStop = false;
            this.grb_auto_level_process_halign.Text = "Horizontal Alignment Design [HALIGN]";
            // 
            // btn_HRP_halign_video
            // 
            this.btn_HRP_halign_video.Location = new System.Drawing.Point(357, 16);
            this.btn_HRP_halign_video.Name = "btn_HRP_halign_video";
            this.btn_HRP_halign_video.Size = new System.Drawing.Size(103, 25);
            this.btn_HRP_halign_video.TabIndex = 1;
            this.btn_HRP_halign_video.Text = "Related Video";
            this.btn_HRP_halign_video.UseVisualStyleBackColor = true;
            this.btn_HRP_halign_video.Click += new System.EventHandler(this.btn_HRP_halign_video_Click);
            // 
            // btn_HRP_halign_process
            // 
            this.btn_HRP_halign_process.Location = new System.Drawing.Point(285, 16);
            this.btn_HRP_halign_process.Name = "btn_HRP_halign_process";
            this.btn_HRP_halign_process.Size = new System.Drawing.Size(72, 25);
            this.btn_HRP_halign_process.TabIndex = 1;
            this.btn_HRP_halign_process.Text = "Proceed";
            this.btn_HRP_halign_process.UseVisualStyleBackColor = true;
            this.btn_HRP_halign_process.Click += new System.EventHandler(this.btn_auto_halign_process_Click);
            // 
            // cmb_auto_halign
            // 
            this.cmb_auto_halign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_auto_halign.DropDownWidth = 370;
            this.cmb_auto_halign.FormattingEnabled = true;
            this.cmb_auto_halign.Items.AddRange(new object[] {
            "New HALIGN Design",
            "Automatic HALIGN Design Wizard",
            "Process HALIGN Design Data",
            "Process PROHALIGN Data to Create HALIGN Design Data",
            "Traverse Alignment [Total Station Data]"});
            this.cmb_auto_halign.Location = new System.Drawing.Point(5, 19);
            this.cmb_auto_halign.Name = "cmb_auto_halign";
            this.cmb_auto_halign.Size = new System.Drawing.Size(274, 21);
            this.cmb_auto_halign.TabIndex = 0;
            this.cmb_auto_halign.SelectedIndexChanged += new System.EventHandler(this.cmb_auto_halign_SelectedIndexChanged);
            // 
            // tab_gm
            // 
            this.tab_gm.Controls.Add(this.chk_utm_conversion);
            this.tab_gm.Controls.Add(this.btn_DGM_open_gm_data);
            this.tab_gm.Controls.Add(this.btn_DGM_open_survey_data);
            this.tab_gm.Controls.Add(this.btn_DGM_create_model);
            this.tab_gm.Controls.Add(this.panel1);
            this.tab_gm.Location = new System.Drawing.Point(4, 22);
            this.tab_gm.Name = "tab_gm";
            this.tab_gm.Padding = new System.Windows.Forms.Padding(3);
            this.tab_gm.Size = new System.Drawing.Size(874, 572);
            this.tab_gm.TabIndex = 0;
            this.tab_gm.Text = "Ground Modeling";
            this.tab_gm.UseVisualStyleBackColor = true;
            // 
            // chk_utm_conversion
            // 
            this.chk_utm_conversion.AutoSize = true;
            this.chk_utm_conversion.Location = new System.Drawing.Point(17, 13);
            this.chk_utm_conversion.Name = "chk_utm_conversion";
            this.chk_utm_conversion.Size = new System.Drawing.Size(150, 17);
            this.chk_utm_conversion.TabIndex = 38;
            this.chk_utm_conversion.Text = "UTM Data Conversion";
            this.chk_utm_conversion.UseVisualStyleBackColor = true;
            // 
            // btn_DGM_open_gm_data
            // 
            this.btn_DGM_open_gm_data.Location = new System.Drawing.Point(198, 72);
            this.btn_DGM_open_gm_data.Name = "btn_DGM_open_gm_data";
            this.btn_DGM_open_gm_data.Size = new System.Drawing.Size(175, 30);
            this.btn_DGM_open_gm_data.TabIndex = 37;
            this.btn_DGM_open_gm_data.Text = "Open Ground Model Data";
            this.btn_DGM_open_gm_data.UseVisualStyleBackColor = true;
            this.btn_DGM_open_gm_data.Visible = false;
            this.btn_DGM_open_gm_data.Click += new System.EventHandler(this.btn_DGM_create_model_Click);
            // 
            // btn_DGM_open_survey_data
            // 
            this.btn_DGM_open_survey_data.Location = new System.Drawing.Point(17, 72);
            this.btn_DGM_open_survey_data.Name = "btn_DGM_open_survey_data";
            this.btn_DGM_open_survey_data.Size = new System.Drawing.Size(175, 30);
            this.btn_DGM_open_survey_data.TabIndex = 37;
            this.btn_DGM_open_survey_data.Text = "Open Survey Data";
            this.btn_DGM_open_survey_data.UseVisualStyleBackColor = true;
            this.btn_DGM_open_survey_data.Click += new System.EventHandler(this.btn_DGM_create_model_Click);
            // 
            // btn_DGM_create_model
            // 
            this.btn_DGM_create_model.Location = new System.Drawing.Point(17, 36);
            this.btn_DGM_create_model.Name = "btn_DGM_create_model";
            this.btn_DGM_create_model.Size = new System.Drawing.Size(175, 30);
            this.btn_DGM_create_model.TabIndex = 37;
            this.btn_DGM_create_model.Text = "Create Ground Model";
            this.btn_DGM_create_model.UseVisualStyleBackColor = true;
            this.btn_DGM_create_model.Click += new System.EventHandler(this.btn_DGM_create_model_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grb_from_file);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btn_proceed);
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.cmb_layer);
            this.panel1.Controls.Add(this.btn_delete_rows);
            this.panel1.Controls.Add(this.dgv_all_data);
            this.panel1.Controls.Add(this.chk_Draw_All);
            this.panel1.Controls.Add(this.btn_add_to_list);
            this.panel1.Controls.Add(this.cmb_draw);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 498);
            this.panel1.TabIndex = 36;
            // 
            // grb_from_file
            // 
            this.grb_from_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grb_from_file.Controls.Add(this.label4);
            this.grb_from_file.Controls.Add(this.txt_select_settings);
            this.grb_from_file.Controls.Add(this.btn_browse_settings);
            this.grb_from_file.Controls.Add(this.lbl_text);
            this.grb_from_file.Controls.Add(this.txt_drawing_lib);
            this.grb_from_file.Controls.Add(this.btn_browse_lib);
            this.grb_from_file.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_from_file.ForeColor = System.Drawing.Color.Red;
            this.grb_from_file.Location = new System.Drawing.Point(8, 3);
            this.grb_from_file.Name = "grb_from_file";
            this.grb_from_file.Size = new System.Drawing.Size(853, 115);
            this.grb_from_file.TabIndex = 29;
            this.grb_from_file.TabStop = false;
            this.grb_from_file.Text = "Optional";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(4, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Select User\'s Settings File (Optional)";
            // 
            // txt_select_settings
            // 
            this.txt_select_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_select_settings.Location = new System.Drawing.Point(7, 85);
            this.txt_select_settings.Name = "txt_select_settings";
            this.txt_select_settings.Size = new System.Drawing.Size(795, 22);
            this.txt_select_settings.TabIndex = 21;
            // 
            // btn_browse_settings
            // 
            this.btn_browse_settings.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_browse_settings.Location = new System.Drawing.Point(810, 85);
            this.btn_browse_settings.Name = "btn_browse_settings";
            this.btn_browse_settings.Size = new System.Drawing.Size(33, 22);
            this.btn_browse_settings.TabIndex = 22;
            this.btn_browse_settings.Text = "...";
            this.btn_browse_settings.UseVisualStyleBackColor = true;
            this.btn_browse_settings.Click += new System.EventHandler(this.btn_browse_settings_Click);
            // 
            // lbl_text
            // 
            this.lbl_text.AutoSize = true;
            this.lbl_text.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_text.ForeColor = System.Drawing.Color.Black;
            this.lbl_text.Location = new System.Drawing.Point(4, 16);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(262, 13);
            this.lbl_text.TabIndex = 19;
            this.lbl_text.Text = "Select Folder for Drawing from Block Library";
            // 
            // txt_drawing_lib
            // 
            this.txt_drawing_lib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_drawing_lib.Location = new System.Drawing.Point(5, 35);
            this.txt_drawing_lib.Name = "txt_drawing_lib";
            this.txt_drawing_lib.Size = new System.Drawing.Size(797, 22);
            this.txt_drawing_lib.TabIndex = 12;
            // 
            // btn_browse_lib
            // 
            this.btn_browse_lib.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_browse_lib.Location = new System.Drawing.Point(810, 35);
            this.btn_browse_lib.Name = "btn_browse_lib";
            this.btn_browse_lib.Size = new System.Drawing.Size(33, 21);
            this.btn_browse_lib.TabIndex = 13;
            this.btn_browse_lib.Text = "...";
            this.btn_browse_lib.UseVisualStyleBackColor = true;
            this.btn_browse_lib.Click += new System.EventHandler(this.btn_browse_lib_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Draw As";
            // 
            // btn_proceed
            // 
            this.btn_proceed.Location = new System.Drawing.Point(258, 470);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(64, 23);
            this.btn_proceed.TabIndex = 25;
            this.btn_proceed.Text = "Draw";
            this.btn_proceed.UseVisualStyleBackColor = true;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Refresh.Location = new System.Drawing.Point(117, 470);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(64, 23);
            this.btn_Refresh.TabIndex = 34;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // cmb_layer
            // 
            this.cmb_layer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_layer.FormattingEnabled = true;
            this.cmb_layer.Items.AddRange(new object[] {
            "POINT",
            "POLYLINE",
            "TEXT"});
            this.cmb_layer.Location = new System.Drawing.Point(99, 135);
            this.cmb_layer.Name = "cmb_layer";
            this.cmb_layer.Size = new System.Drawing.Size(128, 21);
            this.cmb_layer.TabIndex = 27;
            // 
            // btn_delete_rows
            // 
            this.btn_delete_rows.Location = new System.Drawing.Point(188, 470);
            this.btn_delete_rows.Name = "btn_delete_rows";
            this.btn_delete_rows.Size = new System.Drawing.Size(64, 23);
            this.btn_delete_rows.TabIndex = 32;
            this.btn_delete_rows.Text = "Delete Row";
            this.btn_delete_rows.UseVisualStyleBackColor = true;
            this.btn_delete_rows.Click += new System.EventHandler(this.btn_delete_rows_Click);
            // 
            // dgv_all_data
            // 
            this.dgv_all_data.AllowUserToAddRows = false;
            this.dgv_all_data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_all_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_all_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDraw,
            this.col_label,
            this.col_draw_el,
            this.col_layer,
            this.col_clr});
            this.dgv_all_data.Location = new System.Drawing.Point(3, 202);
            this.dgv_all_data.Name = "dgv_all_data";
            this.dgv_all_data.RowHeadersWidth = 27;
            this.dgv_all_data.Size = new System.Drawing.Size(857, 262);
            this.dgv_all_data.TabIndex = 31;
            this.dgv_all_data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_all_data_CellClick);
            // 
            // colDraw
            // 
            this.colDraw.HeaderText = "Draw";
            this.colDraw.Name = "colDraw";
            this.colDraw.Width = 50;
            // 
            // col_label
            // 
            this.col_label.HeaderText = "LABEL Name";
            this.col_label.Name = "col_label";
            this.col_label.Width = 90;
            // 
            // col_draw_el
            // 
            this.col_draw_el.HeaderText = "Drawing Element Name";
            this.col_draw_el.Name = "col_draw_el";
            this.col_draw_el.Width = 115;
            // 
            // col_layer
            // 
            this.col_layer.HeaderText = "LAYER Name";
            this.col_layer.Name = "col_layer";
            this.col_layer.Width = 99;
            // 
            // col_clr
            // 
            this.col_clr.HeaderText = "Color";
            this.col_clr.Name = "col_clr";
            this.col_clr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_clr.Width = 39;
            // 
            // chk_Draw_All
            // 
            this.chk_Draw_All.AutoSize = true;
            this.chk_Draw_All.Checked = true;
            this.chk_Draw_All.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Draw_All.Location = new System.Drawing.Point(37, 476);
            this.chk_Draw_All.Name = "chk_Draw_All";
            this.chk_Draw_All.Size = new System.Drawing.Size(74, 17);
            this.chk_Draw_All.TabIndex = 33;
            this.chk_Draw_All.Text = "Draw All";
            this.chk_Draw_All.UseVisualStyleBackColor = true;
            this.chk_Draw_All.CheckedChanged += new System.EventHandler(this.chk_Draw_All_CheckedChanged);
            // 
            // btn_add_to_list
            // 
            this.btn_add_to_list.Location = new System.Drawing.Point(233, 163);
            this.btn_add_to_list.Name = "btn_add_to_list";
            this.btn_add_to_list.Size = new System.Drawing.Size(75, 23);
            this.btn_add_to_list.TabIndex = 24;
            this.btn_add_to_list.Text = "Add to List";
            this.btn_add_to_list.UseVisualStyleBackColor = true;
            this.btn_add_to_list.Click += new System.EventHandler(this.btn_Add_to_list_Click);
            // 
            // cmb_draw
            // 
            this.cmb_draw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_draw.DropDownWidth = 210;
            this.cmb_draw.FormattingEnabled = true;
            this.cmb_draw.Items.AddRange(new object[] {
            "POLYLINE",
            "POINT",
            "POINT_ELEVATION",
            "POINT_LABEL",
            "ELEVATION",
            "LABEL",
            "TEXT"});
            this.cmb_draw.Location = new System.Drawing.Point(99, 164);
            this.cmb_draw.Name = "cmb_draw";
            this.cmb_draw.Size = new System.Drawing.Size(128, 21);
            this.cmb_draw.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Select Layer";
            // 
            // tab_trngu
            // 
            this.tab_trngu.Controls.Add(this.label22);
            this.tab_trngu.Controls.Add(this.grbGroundModeling);
            this.tab_trngu.Location = new System.Drawing.Point(4, 22);
            this.tab_trngu.Name = "tab_trngu";
            this.tab_trngu.Padding = new System.Windows.Forms.Padding(3);
            this.tab_trngu.Size = new System.Drawing.Size(874, 572);
            this.tab_trngu.TabIndex = 1;
            this.tab_trngu.Text = "Triangulation";
            this.tab_trngu.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(6, 6);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(348, 64);
            this.label22.TabIndex = 6;
            this.label22.Text = "To Write all Survey Features in the Model Files MODEL.FIL and MODEL.LST\r\nInput Fi" +
    "le: Project Data,  MODEL.FIL and MODEL.LST\r\nOUTPUT File: HDS001.FIL, HDS000.FIL," +
    " CONTOUR.FIL \r\n\r\n\r\n";
            // 
            // grbGroundModeling
            // 
            this.grbGroundModeling.Controls.Add(this.txt_GMT_string);
            this.grbGroundModeling.Controls.Add(this.btn_Boundary);
            this.grbGroundModeling.Controls.Add(this.btn_GMT_OK);
            this.grbGroundModeling.Controls.Add(this.label8);
            this.grbGroundModeling.Controls.Add(this.txt_GMT_model);
            this.grbGroundModeling.Controls.Add(this.label33);
            this.grbGroundModeling.Controls.Add(this.label9);
            this.grbGroundModeling.Controls.Add(this.groupBox5);
            this.grbGroundModeling.Controls.Add(this.groupBox4);
            this.grbGroundModeling.Controls.Add(this.groupBox3);
            this.grbGroundModeling.Location = new System.Drawing.Point(6, 73);
            this.grbGroundModeling.Name = "grbGroundModeling";
            this.grbGroundModeling.Size = new System.Drawing.Size(348, 493);
            this.grbGroundModeling.TabIndex = 5;
            this.grbGroundModeling.TabStop = false;
            this.grbGroundModeling.Text = "Ground Modeling ";
            // 
            // txt_GMT_string
            // 
            this.txt_GMT_string.Location = new System.Drawing.Point(252, 343);
            this.txt_GMT_string.Name = "txt_GMT_string";
            this.txt_GMT_string.Size = new System.Drawing.Size(62, 21);
            this.txt_GMT_string.TabIndex = 9;
            this.txt_GMT_string.Text = "BDRY";
            // 
            // btn_Boundary
            // 
            this.btn_Boundary.Location = new System.Drawing.Point(18, 409);
            this.btn_Boundary.Name = "btn_Boundary";
            this.btn_Boundary.Size = new System.Drawing.Size(295, 28);
            this.btn_Boundary.TabIndex = 0;
            this.btn_Boundary.Text = "Create Boundary String";
            this.btn_Boundary.UseVisualStyleBackColor = true;
            this.btn_Boundary.Click += new System.EventHandler(this.btn_Boundary_Click);
            // 
            // btn_GMT_OK
            // 
            this.btn_GMT_OK.Location = new System.Drawing.Point(19, 443);
            this.btn_GMT_OK.Name = "btn_GMT_OK";
            this.btn_GMT_OK.Size = new System.Drawing.Size(295, 41);
            this.btn_GMT_OK.TabIndex = 0;
            this.btn_GMT_OK.Text = "Create Triangulation Model";
            this.btn_GMT_OK.UseVisualStyleBackColor = true;
            this.btn_GMT_OK.Click += new System.EventHandler(this.btn_GMT_OK_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "String";
            // 
            // txt_GMT_model
            // 
            this.txt_GMT_model.Location = new System.Drawing.Point(61, 343);
            this.txt_GMT_model.Name = "txt_GMT_model";
            this.txt_GMT_model.Size = new System.Drawing.Size(78, 21);
            this.txt_GMT_model.TabIndex = 7;
            this.txt_GMT_model.Text = "BOUNDARY";
            // 
            // label33
            // 
            this.label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label33.Location = new System.Drawing.Point(19, 377);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(294, 29);
            this.label33.TabIndex = 6;
            this.label33.Text = "Before Create Boundary String, \r\nuser must create a closed Polyline.";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 346);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Model";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lb_GMT_ModelAndStringName);
            this.groupBox5.Location = new System.Drawing.Point(16, 164);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(315, 173);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Select Model Name and String Names";
            // 
            // lb_GMT_ModelAndStringName
            // 
            this.lb_GMT_ModelAndStringName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_GMT_ModelAndStringName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_GMT_ModelAndStringName.FormattingEnabled = true;
            this.lb_GMT_ModelAndStringName.ItemHeight = 16;
            this.lb_GMT_ModelAndStringName.Location = new System.Drawing.Point(3, 17);
            this.lb_GMT_ModelAndStringName.Name = "lb_GMT_ModelAndStringName";
            this.lb_GMT_ModelAndStringName.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lb_GMT_ModelAndStringName.Size = new System.Drawing.Size(309, 153);
            this.lb_GMT_ModelAndStringName.Sorted = true;
            this.lb_GMT_ModelAndStringName.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txt_GMT_DeSelect);
            this.groupBox4.Controls.Add(this.txt_GMT_Select);
            this.groupBox4.Controls.Add(this.btn_GMT_DeSelect);
            this.groupBox4.Controls.Add(this.btn_GMT_Select);
            this.groupBox4.Location = new System.Drawing.Point(126, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 140);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 32);
            this.label7.TabIndex = 3;
            this.label7.Text = "Wild Character may be used, \r\nExample \"K?\" or \'\'KE?\"";
            // 
            // txt_GMT_DeSelect
            // 
            this.txt_GMT_DeSelect.Location = new System.Drawing.Point(126, 59);
            this.txt_GMT_DeSelect.Name = "txt_GMT_DeSelect";
            this.txt_GMT_DeSelect.Size = new System.Drawing.Size(76, 21);
            this.txt_GMT_DeSelect.TabIndex = 2;
            this.txt_GMT_DeSelect.Text = "K?";
            // 
            // txt_GMT_Select
            // 
            this.txt_GMT_Select.AccessibleDescription = "";
            this.txt_GMT_Select.AllowDrop = true;
            this.txt_GMT_Select.Location = new System.Drawing.Point(126, 21);
            this.txt_GMT_Select.Name = "txt_GMT_Select";
            this.txt_GMT_Select.Size = new System.Drawing.Size(76, 21);
            this.txt_GMT_Select.TabIndex = 2;
            this.txt_GMT_Select.Text = "K?";
            // 
            // btn_GMT_DeSelect
            // 
            this.btn_GMT_DeSelect.Location = new System.Drawing.Point(16, 59);
            this.btn_GMT_DeSelect.Name = "btn_GMT_DeSelect";
            this.btn_GMT_DeSelect.Size = new System.Drawing.Size(96, 23);
            this.btn_GMT_DeSelect.TabIndex = 1;
            this.btn_GMT_DeSelect.Text = "DeSelect";
            this.btn_GMT_DeSelect.UseVisualStyleBackColor = true;
            this.btn_GMT_DeSelect.Click += new System.EventHandler(this.btn_GMT_DeSelect_Click);
            // 
            // btn_GMT_Select
            // 
            this.btn_GMT_Select.Location = new System.Drawing.Point(16, 19);
            this.btn_GMT_Select.Name = "btn_GMT_Select";
            this.btn_GMT_Select.Size = new System.Drawing.Size(96, 23);
            this.btn_GMT_Select.TabIndex = 0;
            this.btn_GMT_Select.Text = "Select";
            this.btn_GMT_Select.UseVisualStyleBackColor = true;
            this.btn_GMT_Select.Click += new System.EventHandler(this.btn_GMT_Select_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_GMT_DeSelectAll);
            this.groupBox3.Controls.Add(this.btn_GMT_SelectAll);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(114, 94);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btn_GMT_DeSelectAll
            // 
            this.btn_GMT_DeSelectAll.Location = new System.Drawing.Point(12, 60);
            this.btn_GMT_DeSelectAll.Name = "btn_GMT_DeSelectAll";
            this.btn_GMT_DeSelectAll.Size = new System.Drawing.Size(96, 23);
            this.btn_GMT_DeSelectAll.TabIndex = 1;
            this.btn_GMT_DeSelectAll.Text = "DeSelect All";
            this.btn_GMT_DeSelectAll.UseVisualStyleBackColor = true;
            this.btn_GMT_DeSelectAll.Click += new System.EventHandler(this.btn_GMT_DeSelectAll_Click);
            // 
            // btn_GMT_SelectAll
            // 
            this.btn_GMT_SelectAll.Location = new System.Drawing.Point(12, 19);
            this.btn_GMT_SelectAll.Name = "btn_GMT_SelectAll";
            this.btn_GMT_SelectAll.Size = new System.Drawing.Size(96, 23);
            this.btn_GMT_SelectAll.TabIndex = 0;
            this.btn_GMT_SelectAll.Text = "Select All";
            this.btn_GMT_SelectAll.UseVisualStyleBackColor = true;
            this.btn_GMT_SelectAll.Click += new System.EventHandler(this.btn_GMT_SelectAll_Click);
            // 
            // tab_cont
            // 
            this.tab_cont.Controls.Add(this.chk_Contour_SURFACE);
            this.tab_cont.Controls.Add(this.chk_Contour_ELEV);
            this.tab_cont.Controls.Add(this.chk_Contour_C005);
            this.tab_cont.Controls.Add(this.chk_Contour_C001);
            this.tab_cont.Controls.Add(this.btn_contour_refresh);
            this.tab_cont.Controls.Add(this.btn_draw_ground_surface);
            this.tab_cont.Controls.Add(this.label23);
            this.tab_cont.Controls.Add(this.btn_Contour_create_model);
            this.tab_cont.Controls.Add(this.btn_Contour_draw_model);
            this.tab_cont.Controls.Add(this.groupBox2);
            this.tab_cont.Controls.Add(this.groupBox7);
            this.tab_cont.Controls.Add(this.groupBox6);
            this.tab_cont.Location = new System.Drawing.Point(4, 22);
            this.tab_cont.Name = "tab_cont";
            this.tab_cont.Size = new System.Drawing.Size(874, 572);
            this.tab_cont.TabIndex = 2;
            this.tab_cont.Text = "Contour";
            this.tab_cont.UseVisualStyleBackColor = true;
            this.tab_cont.Click += new System.EventHandler(this.tab_cont_Click);
            // 
            // chk_Contour_SURFACE
            // 
            this.chk_Contour_SURFACE.AutoSize = true;
            this.chk_Contour_SURFACE.Location = new System.Drawing.Point(107, 491);
            this.chk_Contour_SURFACE.Name = "chk_Contour_SURFACE";
            this.chk_Contour_SURFACE.Size = new System.Drawing.Size(79, 17);
            this.chk_Contour_SURFACE.TabIndex = 26;
            this.chk_Contour_SURFACE.Text = "SURFACE";
            this.chk_Contour_SURFACE.UseVisualStyleBackColor = true;
            this.chk_Contour_SURFACE.CheckedChanged += new System.EventHandler(this.chk_Contour_ELEV_CheckedChanged);
            // 
            // chk_Contour_ELEV
            // 
            this.chk_Contour_ELEV.AutoSize = true;
            this.chk_Contour_ELEV.Location = new System.Drawing.Point(213, 424);
            this.chk_Contour_ELEV.Name = "chk_Contour_ELEV";
            this.chk_Contour_ELEV.Size = new System.Drawing.Size(54, 17);
            this.chk_Contour_ELEV.TabIndex = 26;
            this.chk_Contour_ELEV.Text = "ELEV";
            this.chk_Contour_ELEV.UseVisualStyleBackColor = true;
            this.chk_Contour_ELEV.CheckedChanged += new System.EventHandler(this.chk_Contour_ELEV_CheckedChanged);
            // 
            // chk_Contour_C005
            // 
            this.chk_Contour_C005.AutoSize = true;
            this.chk_Contour_C005.Location = new System.Drawing.Point(130, 425);
            this.chk_Contour_C005.Name = "chk_Contour_C005";
            this.chk_Contour_C005.Size = new System.Drawing.Size(56, 17);
            this.chk_Contour_C005.TabIndex = 26;
            this.chk_Contour_C005.Text = "C005";
            this.chk_Contour_C005.UseVisualStyleBackColor = true;
            this.chk_Contour_C005.CheckedChanged += new System.EventHandler(this.chk_Contour_ELEV_CheckedChanged);
            // 
            // chk_Contour_C001
            // 
            this.chk_Contour_C001.AutoSize = true;
            this.chk_Contour_C001.Location = new System.Drawing.Point(35, 424);
            this.chk_Contour_C001.Name = "chk_Contour_C001";
            this.chk_Contour_C001.Size = new System.Drawing.Size(56, 17);
            this.chk_Contour_C001.TabIndex = 26;
            this.chk_Contour_C001.Text = "C001";
            this.chk_Contour_C001.UseVisualStyleBackColor = true;
            this.chk_Contour_C001.CheckedChanged += new System.EventHandler(this.chk_Contour_ELEV_CheckedChanged);
            // 
            // btn_contour_refresh
            // 
            this.btn_contour_refresh.Location = new System.Drawing.Point(14, 508);
            this.btn_contour_refresh.Name = "btn_contour_refresh";
            this.btn_contour_refresh.Size = new System.Drawing.Size(285, 38);
            this.btn_contour_refresh.TabIndex = 25;
            this.btn_contour_refresh.Text = "Refresh";
            this.btn_contour_refresh.UseVisualStyleBackColor = true;
            this.btn_contour_refresh.Click += new System.EventHandler(this.btn_contour_refresh_Click);
            // 
            // btn_draw_ground_surface
            // 
            this.btn_draw_ground_surface.Location = new System.Drawing.Point(14, 448);
            this.btn_draw_ground_surface.Name = "btn_draw_ground_surface";
            this.btn_draw_ground_surface.Size = new System.Drawing.Size(285, 37);
            this.btn_draw_ground_surface.TabIndex = 20;
            this.btn_draw_ground_surface.Text = "Draw Ground Surface";
            this.btn_draw_ground_surface.UseVisualStyleBackColor = true;
            this.btn_draw_ground_surface.Click += new System.EventHandler(this.btn_draw_ground_surface_Click);
            // 
            // label23
            // 
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(3, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(311, 64);
            this.label23.TabIndex = 24;
            // 
            // btn_Contour_create_model
            // 
            this.btn_Contour_create_model.Location = new System.Drawing.Point(14, 338);
            this.btn_Contour_create_model.Name = "btn_Contour_create_model";
            this.btn_Contour_create_model.Size = new System.Drawing.Size(285, 37);
            this.btn_Contour_create_model.TabIndex = 19;
            this.btn_Contour_create_model.Text = "Create Contour Model";
            this.btn_Contour_create_model.UseVisualStyleBackColor = true;
            this.btn_Contour_create_model.Click += new System.EventHandler(this.btn_Contour_Save_Click);
            // 
            // btn_Contour_draw_model
            // 
            this.btn_Contour_draw_model.Location = new System.Drawing.Point(14, 381);
            this.btn_Contour_draw_model.Name = "btn_Contour_draw_model";
            this.btn_Contour_draw_model.Size = new System.Drawing.Size(285, 37);
            this.btn_Contour_draw_model.TabIndex = 18;
            this.btn_Contour_draw_model.Text = "Draw Contour Model";
            this.btn_Contour_draw_model.UseVisualStyleBackColor = true;
            this.btn_Contour_draw_model.Click += new System.EventHandler(this.btn_Contour_draw_model_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_Contour_ele_model);
            this.groupBox2.Controls.Add(this.txt_Contour_ele_inc);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_Contour_ele_string);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(14, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 67);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Contour Elevation Text";
            // 
            // txt_Contour_ele_model
            // 
            this.txt_Contour_ele_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_ele_model.Location = new System.Drawing.Point(12, 33);
            this.txt_Contour_ele_model.Name = "txt_Contour_ele_model";
            this.txt_Contour_ele_model.ReadOnly = true;
            this.txt_Contour_ele_model.Size = new System.Drawing.Size(82, 22);
            this.txt_Contour_ele_model.TabIndex = 1;
            this.txt_Contour_ele_model.Text = "CONTOUR";
            // 
            // txt_Contour_ele_inc
            // 
            this.txt_Contour_ele_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_ele_inc.Location = new System.Drawing.Point(214, 33);
            this.txt_Contour_ele_inc.Name = "txt_Contour_ele_inc";
            this.txt_Contour_ele_inc.ReadOnly = true;
            this.txt_Contour_ele_inc.Size = new System.Drawing.Size(43, 22);
            this.txt_Contour_ele_inc.TabIndex = 5;
            this.txt_Contour_ele_inc.Text = "5.0";
            this.txt_Contour_ele_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(202, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Increament";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Model";
            // 
            // txt_Contour_ele_string
            // 
            this.txt_Contour_ele_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_ele_string.Location = new System.Drawing.Point(121, 33);
            this.txt_Contour_ele_string.Name = "txt_Contour_ele_string";
            this.txt_Contour_ele_string.ReadOnly = true;
            this.txt_Contour_ele_string.Size = new System.Drawing.Size(60, 22);
            this.txt_Contour_ele_string.TabIndex = 3;
            this.txt_Contour_ele_string.Text = "ELEV";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(129, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "String";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txt_Contour_pri_model);
            this.groupBox7.Controls.Add(this.txt_Contour_pri_inc);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.txt_Contour_pri_string);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Location = new System.Drawing.Point(14, 98);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(285, 69);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Primary Contour Model";
            // 
            // txt_Contour_pri_model
            // 
            this.txt_Contour_pri_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_pri_model.Location = new System.Drawing.Point(12, 38);
            this.txt_Contour_pri_model.Name = "txt_Contour_pri_model";
            this.txt_Contour_pri_model.ReadOnly = true;
            this.txt_Contour_pri_model.Size = new System.Drawing.Size(82, 22);
            this.txt_Contour_pri_model.TabIndex = 1;
            this.txt_Contour_pri_model.Text = "CONTOUR";
            // 
            // txt_Contour_pri_inc
            // 
            this.txt_Contour_pri_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_pri_inc.Location = new System.Drawing.Point(214, 38);
            this.txt_Contour_pri_inc.Name = "txt_Contour_pri_inc";
            this.txt_Contour_pri_inc.Size = new System.Drawing.Size(43, 22);
            this.txt_Contour_pri_inc.TabIndex = 5;
            this.txt_Contour_pri_inc.Text = "1.0";
            this.txt_Contour_pri_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Contour_pri_inc.TextChanged += new System.EventHandler(this.txt_Contour_pri_inc_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(202, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Increament";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Model";
            // 
            // txt_Contour_pri_string
            // 
            this.txt_Contour_pri_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_pri_string.Location = new System.Drawing.Point(121, 38);
            this.txt_Contour_pri_string.Name = "txt_Contour_pri_string";
            this.txt_Contour_pri_string.ReadOnly = true;
            this.txt_Contour_pri_string.Size = new System.Drawing.Size(60, 22);
            this.txt_Contour_pri_string.TabIndex = 3;
            this.txt_Contour_pri_string.Text = "C001";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(129, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "String";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txt_Contour_sec_model);
            this.groupBox6.Controls.Add(this.txt_Contour_sec_inc);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.txt_Contour_sec_string);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Location = new System.Drawing.Point(14, 173);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(285, 64);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Secondary Contour Model";
            // 
            // txt_Contour_sec_model
            // 
            this.txt_Contour_sec_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_sec_model.Location = new System.Drawing.Point(12, 38);
            this.txt_Contour_sec_model.Name = "txt_Contour_sec_model";
            this.txt_Contour_sec_model.ReadOnly = true;
            this.txt_Contour_sec_model.Size = new System.Drawing.Size(82, 22);
            this.txt_Contour_sec_model.TabIndex = 1;
            this.txt_Contour_sec_model.Text = "CONTOUR";
            // 
            // txt_Contour_sec_inc
            // 
            this.txt_Contour_sec_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_sec_inc.Location = new System.Drawing.Point(214, 38);
            this.txt_Contour_sec_inc.Name = "txt_Contour_sec_inc";
            this.txt_Contour_sec_inc.ReadOnly = true;
            this.txt_Contour_sec_inc.Size = new System.Drawing.Size(43, 22);
            this.txt_Contour_sec_inc.TabIndex = 5;
            this.txt_Contour_sec_inc.Text = "5.0";
            this.txt_Contour_sec_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(202, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Increament";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Model";
            // 
            // txt_Contour_sec_string
            // 
            this.txt_Contour_sec_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contour_sec_string.Location = new System.Drawing.Point(121, 38);
            this.txt_Contour_sec_string.Name = "txt_Contour_sec_string";
            this.txt_Contour_sec_string.ReadOnly = true;
            this.txt_Contour_sec_string.Size = new System.Drawing.Size(60, 22);
            this.txt_Contour_sec_string.TabIndex = 3;
            this.txt_Contour_sec_string.Text = "C005";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(129, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "String";
            // 
            // tab_strg_qty
            // 
            this.tab_strg_qty.Controls.Add(this.uC_Stockpile);
            this.tab_strg_qty.Location = new System.Drawing.Point(4, 22);
            this.tab_strg_qty.Name = "tab_strg_qty";
            this.tab_strg_qty.Size = new System.Drawing.Size(874, 572);
            this.tab_strg_qty.TabIndex = 6;
            this.tab_strg_qty.Text = "Stockpile Quantity";
            this.tab_strg_qty.UseVisualStyleBackColor = true;
            // 
            // tab_discrg_qty
            // 
            this.tab_discrg_qty.Controls.Add(this.uC_Discharge);
            this.tab_discrg_qty.Location = new System.Drawing.Point(4, 22);
            this.tab_discrg_qty.Name = "tab_discrg_qty";
            this.tab_discrg_qty.Size = new System.Drawing.Size(874, 572);
            this.tab_discrg_qty.TabIndex = 7;
            this.tab_discrg_qty.Text = "Discharge Quantity";
            this.tab_discrg_qty.UseVisualStyleBackColor = true;
            // 
            // tab_alignment
            // 
            this.tab_alignment.Controls.Add(this.label34);
            this.tab_alignment.Controls.Add(this.btn_ALGN_dtlsOff);
            this.tab_alignment.Controls.Add(this.btn_ALGN_dtlsOn);
            this.tab_alignment.Controls.Add(this.btn_ALGN_chnOff);
            this.tab_alignment.Controls.Add(this.btn_ALGN_chnOn);
            this.tab_alignment.Controls.Add(this.btn_ALGN_create);
            this.tab_alignment.Controls.Add(this.label38);
            this.tab_alignment.Controls.Add(this.txt_ALGN_String);
            this.tab_alignment.Controls.Add(this.label137);
            this.tab_alignment.Controls.Add(this.txt_ALGN_Model);
            this.tab_alignment.Controls.Add(this.label142);
            this.tab_alignment.Location = new System.Drawing.Point(4, 22);
            this.tab_alignment.Name = "tab_alignment";
            this.tab_alignment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_alignment.Size = new System.Drawing.Size(874, 572);
            this.tab_alignment.TabIndex = 8;
            this.tab_alignment.Text = "Alignment";
            this.tab_alignment.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(18, 18);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(270, 36);
            this.label34.TabIndex = 32;
            this.label34.Text = "Creating Alignment String writes in MODEL.FIL and MODEL.LST\r\n.";
            // 
            // btn_ALGN_dtlsOff
            // 
            this.btn_ALGN_dtlsOff.Location = new System.Drawing.Point(132, 267);
            this.btn_ALGN_dtlsOff.Name = "btn_ALGN_dtlsOff";
            this.btn_ALGN_dtlsOff.Size = new System.Drawing.Size(156, 28);
            this.btn_ALGN_dtlsOff.TabIndex = 30;
            this.btn_ALGN_dtlsOff.Text = "Details OFF";
            this.btn_ALGN_dtlsOff.UseVisualStyleBackColor = true;
            this.btn_ALGN_dtlsOff.Click += new System.EventHandler(this.btn_ALGN_create_Click);
            // 
            // btn_ALGN_dtlsOn
            // 
            this.btn_ALGN_dtlsOn.Location = new System.Drawing.Point(18, 268);
            this.btn_ALGN_dtlsOn.Name = "btn_ALGN_dtlsOn";
            this.btn_ALGN_dtlsOn.Size = new System.Drawing.Size(108, 28);
            this.btn_ALGN_dtlsOn.TabIndex = 31;
            this.btn_ALGN_dtlsOn.Text = "Details ON";
            this.btn_ALGN_dtlsOn.UseVisualStyleBackColor = true;
            this.btn_ALGN_dtlsOn.Click += new System.EventHandler(this.btn_ALGN_create_Click);
            // 
            // btn_ALGN_chnOff
            // 
            this.btn_ALGN_chnOff.Location = new System.Drawing.Point(132, 238);
            this.btn_ALGN_chnOff.Name = "btn_ALGN_chnOff";
            this.btn_ALGN_chnOff.Size = new System.Drawing.Size(156, 28);
            this.btn_ALGN_chnOff.TabIndex = 28;
            this.btn_ALGN_chnOff.Text = "Chainage OFF";
            this.btn_ALGN_chnOff.UseVisualStyleBackColor = true;
            this.btn_ALGN_chnOff.Click += new System.EventHandler(this.btn_ALGN_create_Click);
            // 
            // btn_ALGN_chnOn
            // 
            this.btn_ALGN_chnOn.Location = new System.Drawing.Point(18, 238);
            this.btn_ALGN_chnOn.Name = "btn_ALGN_chnOn";
            this.btn_ALGN_chnOn.Size = new System.Drawing.Size(108, 28);
            this.btn_ALGN_chnOn.TabIndex = 29;
            this.btn_ALGN_chnOn.Text = "Chainage ON";
            this.btn_ALGN_chnOn.UseVisualStyleBackColor = true;
            this.btn_ALGN_chnOn.Click += new System.EventHandler(this.btn_ALGN_create_Click);
            // 
            // btn_ALGN_create
            // 
            this.btn_ALGN_create.Location = new System.Drawing.Point(18, 183);
            this.btn_ALGN_create.Name = "btn_ALGN_create";
            this.btn_ALGN_create.Size = new System.Drawing.Size(270, 29);
            this.btn_ALGN_create.TabIndex = 27;
            this.btn_ALGN_create.Text = "Create Alignment String";
            this.btn_ALGN_create.UseVisualStyleBackColor = true;
            this.btn_ALGN_create.Click += new System.EventHandler(this.btn_ALGN_create_Click);
            // 
            // label38
            // 
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.Location = new System.Drawing.Point(18, 150);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(270, 29);
            this.label38.TabIndex = 26;
            this.label38.Text = "Before Creating Alignment String, \r\nuser must create a Polyline.";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_ALGN_String
            // 
            this.txt_ALGN_String.Location = new System.Drawing.Point(226, 104);
            this.txt_ALGN_String.Name = "txt_ALGN_String";
            this.txt_ALGN_String.Size = new System.Drawing.Size(62, 21);
            this.txt_ALGN_String.TabIndex = 25;
            this.txt_ALGN_String.Text = "M001";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(177, 107);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(41, 13);
            this.label137.TabIndex = 24;
            this.label137.Text = "String";
            // 
            // txt_ALGN_Model
            // 
            this.txt_ALGN_Model.Location = new System.Drawing.Point(64, 104);
            this.txt_ALGN_Model.Name = "txt_ALGN_Model";
            this.txt_ALGN_Model.Size = new System.Drawing.Size(62, 21);
            this.txt_ALGN_Model.TabIndex = 23;
            this.txt_ALGN_Model.Text = "DESIGN";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(18, 107);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(40, 13);
            this.label142.TabIndex = 22;
            this.label142.Text = "Model";
            // 
            // lbl_survey_data
            // 
            this.lbl_survey_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_survey_data.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_survey_data.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_survey_data.Location = new System.Drawing.Point(0, 0);
            this.lbl_survey_data.Name = "lbl_survey_data";
            this.lbl_survey_data.Size = new System.Drawing.Size(882, 35);
            this.lbl_survey_data.TabIndex = 1;
            this.lbl_survey_data.Text = "SURVEY DATA TYPE : Total Station";
            this.lbl_survey_data.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_trvrs
            // 
            this.tab_trvrs.Controls.Add(this.sc_traverse);
            this.tab_trvrs.Location = new System.Drawing.Point(4, 22);
            this.tab_trvrs.Name = "tab_trvrs";
            this.tab_trvrs.Size = new System.Drawing.Size(882, 633);
            this.tab_trvrs.TabIndex = 3;
            this.tab_trvrs.Text = "Traverse";
            this.tab_trvrs.UseVisualStyleBackColor = true;
            // 
            // sc_traverse
            // 
            this.sc_traverse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_traverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_traverse.Location = new System.Drawing.Point(0, 0);
            this.sc_traverse.Name = "sc_traverse";
            // 
            // sc_traverse.Panel1
            // 
            this.sc_traverse.Panel1.Controls.Add(this.panel4);
            // 
            // sc_traverse.Panel2
            // 
            this.sc_traverse.Panel2.Controls.Add(this.panel5);
            this.sc_traverse.Size = new System.Drawing.Size(882, 633);
            this.sc_traverse.SplitterDistance = 533;
            this.sc_traverse.TabIndex = 5;
            this.sc_traverse.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.sc_main_SplitterMoved);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tc_traverse);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(531, 631);
            this.panel4.TabIndex = 4;
            // 
            // tc_traverse
            // 
            this.tc_traverse.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tc_traverse.Controls.Add(this.tab_bowditch);
            this.tc_traverse.Controls.Add(this.tab_transit);
            this.tc_traverse.Controls.Add(this.tab_closed_link);
            this.tc_traverse.Controls.Add(this.tab_edm);
            this.tc_traverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_traverse.Location = new System.Drawing.Point(0, 34);
            this.tc_traverse.Multiline = true;
            this.tc_traverse.Name = "tc_traverse";
            this.tc_traverse.SelectedIndex = 0;
            this.tc_traverse.Size = new System.Drawing.Size(531, 537);
            this.tc_traverse.TabIndex = 7;
            // 
            // tab_bowditch
            // 
            this.tab_bowditch.Controls.Add(this.rtb_bowditch_data);
            this.tab_bowditch.Location = new System.Drawing.Point(24, 4);
            this.tab_bowditch.Name = "tab_bowditch";
            this.tab_bowditch.Padding = new System.Windows.Forms.Padding(3);
            this.tab_bowditch.Size = new System.Drawing.Size(503, 529);
            this.tab_bowditch.TabIndex = 0;
            this.tab_bowditch.Text = "Bowditch";
            this.tab_bowditch.UseVisualStyleBackColor = true;
            // 
            // rtb_bowditch_data
            // 
            this.rtb_bowditch_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_bowditch_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_bowditch_data.Location = new System.Drawing.Point(3, 3);
            this.rtb_bowditch_data.Name = "rtb_bowditch_data";
            this.rtb_bowditch_data.Size = new System.Drawing.Size(497, 523);
            this.rtb_bowditch_data.TabIndex = 2;
            this.rtb_bowditch_data.Text = "";
            this.rtb_bowditch_data.WordWrap = false;
            // 
            // tab_transit
            // 
            this.tab_transit.Controls.Add(this.rtb_transit_data);
            this.tab_transit.Location = new System.Drawing.Point(24, 4);
            this.tab_transit.Name = "tab_transit";
            this.tab_transit.Padding = new System.Windows.Forms.Padding(3);
            this.tab_transit.Size = new System.Drawing.Size(503, 529);
            this.tab_transit.TabIndex = 1;
            this.tab_transit.Text = "Transit";
            this.tab_transit.UseVisualStyleBackColor = true;
            // 
            // rtb_transit_data
            // 
            this.rtb_transit_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_transit_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_transit_data.Location = new System.Drawing.Point(3, 3);
            this.rtb_transit_data.Name = "rtb_transit_data";
            this.rtb_transit_data.Size = new System.Drawing.Size(498, 523);
            this.rtb_transit_data.TabIndex = 2;
            this.rtb_transit_data.Text = "";
            this.rtb_transit_data.WordWrap = false;
            // 
            // tab_closed_link
            // 
            this.tab_closed_link.Controls.Add(this.rtb_closed_link_data);
            this.tab_closed_link.Location = new System.Drawing.Point(24, 4);
            this.tab_closed_link.Name = "tab_closed_link";
            this.tab_closed_link.Size = new System.Drawing.Size(503, 529);
            this.tab_closed_link.TabIndex = 2;
            this.tab_closed_link.Text = "Closed-Link";
            this.tab_closed_link.UseVisualStyleBackColor = true;
            // 
            // rtb_closed_link_data
            // 
            this.rtb_closed_link_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_closed_link_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_closed_link_data.Location = new System.Drawing.Point(0, 0);
            this.rtb_closed_link_data.Name = "rtb_closed_link_data";
            this.rtb_closed_link_data.Size = new System.Drawing.Size(504, 529);
            this.rtb_closed_link_data.TabIndex = 2;
            this.rtb_closed_link_data.Text = "";
            this.rtb_closed_link_data.WordWrap = false;
            // 
            // tab_edm
            // 
            this.tab_edm.Controls.Add(this.rtb_edm_data);
            this.tab_edm.Location = new System.Drawing.Point(24, 4);
            this.tab_edm.Name = "tab_edm";
            this.tab_edm.Size = new System.Drawing.Size(503, 529);
            this.tab_edm.TabIndex = 3;
            this.tab_edm.Text = "EDM";
            this.tab_edm.UseVisualStyleBackColor = true;
            // 
            // rtb_edm_data
            // 
            this.rtb_edm_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_edm_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_edm_data.Location = new System.Drawing.Point(0, 0);
            this.rtb_edm_data.Name = "rtb_edm_data";
            this.rtb_edm_data.Size = new System.Drawing.Size(504, 529);
            this.rtb_edm_data.TabIndex = 2;
            this.rtb_edm_data.Text = "";
            this.rtb_edm_data.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_transverse_report);
            this.panel2.Controls.Add(this.btn_transverse_draw);
            this.panel2.Controls.Add(this.btn_transverse_process);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 571);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 60);
            this.panel2.TabIndex = 5;
            // 
            // btn_transverse_report
            // 
            this.btn_transverse_report.Location = new System.Drawing.Point(233, 15);
            this.btn_transverse_report.Name = "btn_transverse_report";
            this.btn_transverse_report.Size = new System.Drawing.Size(150, 35);
            this.btn_transverse_report.TabIndex = 1;
            this.btn_transverse_report.Text = "View Traverse Report";
            this.btn_transverse_report.UseVisualStyleBackColor = true;
            this.btn_transverse_report.Click += new System.EventHandler(this.btn_transverse_process_Click);
            // 
            // btn_transverse_draw
            // 
            this.btn_transverse_draw.Location = new System.Drawing.Point(118, 15);
            this.btn_transverse_draw.Name = "btn_transverse_draw";
            this.btn_transverse_draw.Size = new System.Drawing.Size(109, 35);
            this.btn_transverse_draw.TabIndex = 1;
            this.btn_transverse_draw.Text = "Draw Traverse";
            this.btn_transverse_draw.UseVisualStyleBackColor = true;
            this.btn_transverse_draw.Click += new System.EventHandler(this.btn_transverse_process_Click);
            // 
            // btn_transverse_process
            // 
            this.btn_transverse_process.Location = new System.Drawing.Point(3, 15);
            this.btn_transverse_process.Name = "btn_transverse_process";
            this.btn_transverse_process.Size = new System.Drawing.Size(109, 35);
            this.btn_transverse_process.TabIndex = 1;
            this.btn_transverse_process.Text = "Process Data";
            this.btn_transverse_process.UseVisualStyleBackColor = true;
            this.btn_transverse_process.Click += new System.EventHandler(this.btn_transverse_process_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(531, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "TRAVERSE DATA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rtb_transverse_report);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(343, 631);
            this.panel5.TabIndex = 5;
            // 
            // rtb_transverse_report
            // 
            this.rtb_transverse_report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_transverse_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_transverse_report.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_transverse_report.Location = new System.Drawing.Point(0, 34);
            this.rtb_transverse_report.Name = "rtb_transverse_report";
            this.rtb_transverse_report.Size = new System.Drawing.Size(343, 537);
            this.rtb_transverse_report.TabIndex = 2;
            this.rtb_transverse_report.Text = "";
            this.rtb_transverse_report.WordWrap = false;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 571);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(343, 60);
            this.panel6.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 34);
            this.label2.TabIndex = 4;
            this.label2.Text = "TRAVERSE REPORT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_land
            // 
            this.tab_land.Controls.Add(this.splitContainer1);
            this.tab_land.Location = new System.Drawing.Point(4, 22);
            this.tab_land.Name = "tab_land";
            this.tab_land.Size = new System.Drawing.Size(882, 633);
            this.tab_land.TabIndex = 4;
            this.tab_land.Text = "Land Record Management";
            this.tab_land.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uC_LandRecord1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.vdsC_DOC_Land);
            this.splitContainer1.Panel2.Controls.Add(this.panel7);
            this.splitContainer1.Size = new System.Drawing.Size(882, 633);
            this.splitContainer1.SplitterDistance = 413;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.txt_draw_path);
            this.panel7.Controls.Add(this.btn_Land_drawings);
            this.panel7.Controls.Add(this.lst_land_drawings);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(463, 83);
            this.panel7.TabIndex = 3;
            // 
            // txt_draw_path
            // 
            this.txt_draw_path.Location = new System.Drawing.Point(39, 85);
            this.txt_draw_path.Name = "txt_draw_path";
            this.txt_draw_path.Size = new System.Drawing.Size(100, 21);
            this.txt_draw_path.TabIndex = 2;
            // 
            // btn_Land_drawings
            // 
            this.btn_Land_drawings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Land_drawings.Location = new System.Drawing.Point(375, 7);
            this.btn_Land_drawings.Name = "btn_Land_drawings";
            this.btn_Land_drawings.Size = new System.Drawing.Size(83, 69);
            this.btn_Land_drawings.TabIndex = 1;
            this.btn_Land_drawings.Text = "Browse Drawings";
            this.btn_Land_drawings.UseVisualStyleBackColor = true;
            this.btn_Land_drawings.Click += new System.EventHandler(this.btn_Land_drawings_Click);
            // 
            // lst_land_drawings
            // 
            this.lst_land_drawings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_land_drawings.FormattingEnabled = true;
            this.lst_land_drawings.Location = new System.Drawing.Point(3, 7);
            this.lst_land_drawings.Name = "lst_land_drawings";
            this.lst_land_drawings.Size = new System.Drawing.Size(367, 69);
            this.lst_land_drawings.Sorted = true;
            this.lst_land_drawings.TabIndex = 0;
            this.lst_land_drawings.SelectedIndexChanged += new System.EventHandler(this.lst_land_drawings_SelectedIndexChanged);
            // 
            // tab_site_lvl_grd
            // 
            this.tab_site_lvl_grd.Controls.Add(this.tc_Site_Leveling_Grading);
            this.tab_site_lvl_grd.Location = new System.Drawing.Point(4, 22);
            this.tab_site_lvl_grd.Name = "tab_site_lvl_grd";
            this.tab_site_lvl_grd.Padding = new System.Windows.Forms.Padding(3);
            this.tab_site_lvl_grd.Size = new System.Drawing.Size(882, 633);
            this.tab_site_lvl_grd.TabIndex = 5;
            this.tab_site_lvl_grd.Text = "Site Leveling & Grading";
            this.tab_site_lvl_grd.UseVisualStyleBackColor = true;
            // 
            // tc_Site_Leveling_Grading
            // 
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step4);
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step5);
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step6);
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step7);
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step8);
            this.tc_Site_Leveling_Grading.Controls.Add(this.tab_site_step9);
            this.tc_Site_Leveling_Grading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Site_Leveling_Grading.Location = new System.Drawing.Point(3, 3);
            this.tc_Site_Leveling_Grading.Name = "tc_Site_Leveling_Grading";
            this.tc_Site_Leveling_Grading.SelectedIndex = 0;
            this.tc_Site_Leveling_Grading.Size = new System.Drawing.Size(876, 627);
            this.tc_Site_Leveling_Grading.TabIndex = 0;
            this.tc_Site_Leveling_Grading.SelectedIndexChanged += new System.EventHandler(this.tc_main_SelectedIndexChanged);
            // 
            // tab_site_step4
            // 
            this.tab_site_step4.Controls.Add(this.tc_SLG_halign);
            this.tab_site_step4.Controls.Add(this.panel10);
            this.tab_site_step4.Controls.Add(this.groupBox8);
            this.tab_site_step4.Controls.Add(this.label32);
            this.tab_site_step4.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step4.Name = "tab_site_step4";
            this.tab_site_step4.Padding = new System.Windows.Forms.Padding(3);
            this.tab_site_step4.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step4.TabIndex = 0;
            this.tab_site_step4.Text = "STEP 4";
            this.tab_site_step4.UseVisualStyleBackColor = true;
            // 
            // tc_SLG_halign
            // 
            this.tc_SLG_halign.Controls.Add(this.tab_SLG_prohalign_data);
            this.tc_SLG_halign.Controls.Add(this.tab_SLG_halign_data);
            this.tc_SLG_halign.Controls.Add(this.tab_SLG_traverse_data);
            this.tc_SLG_halign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_SLG_halign.Location = new System.Drawing.Point(3, 80);
            this.tc_SLG_halign.Name = "tc_SLG_halign";
            this.tc_SLG_halign.SelectedIndex = 0;
            this.tc_SLG_halign.Size = new System.Drawing.Size(862, 359);
            this.tc_SLG_halign.TabIndex = 65;
            // 
            // tab_SLG_prohalign_data
            // 
            this.tab_SLG_prohalign_data.Controls.Add(this.rtb_SLG_prohalign);
            this.tab_SLG_prohalign_data.Location = new System.Drawing.Point(4, 22);
            this.tab_SLG_prohalign_data.Name = "tab_SLG_prohalign_data";
            this.tab_SLG_prohalign_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SLG_prohalign_data.Size = new System.Drawing.Size(854, 333);
            this.tab_SLG_prohalign_data.TabIndex = 1;
            this.tab_SLG_prohalign_data.Text = "PRO HALIGN";
            this.tab_SLG_prohalign_data.UseVisualStyleBackColor = true;
            // 
            // rtb_SLG_prohalign
            // 
            this.rtb_SLG_prohalign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_SLG_prohalign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SLG_prohalign.Location = new System.Drawing.Point(3, 3);
            this.rtb_SLG_prohalign.Name = "rtb_SLG_prohalign";
            this.rtb_SLG_prohalign.Size = new System.Drawing.Size(848, 327);
            this.rtb_SLG_prohalign.TabIndex = 63;
            this.rtb_SLG_prohalign.Text = "";
            this.rtb_SLG_prohalign.WordWrap = false;
            // 
            // tab_SLG_halign_data
            // 
            this.tab_SLG_halign_data.Controls.Add(this.rtb_SLG_halign);
            this.tab_SLG_halign_data.Location = new System.Drawing.Point(4, 22);
            this.tab_SLG_halign_data.Name = "tab_SLG_halign_data";
            this.tab_SLG_halign_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SLG_halign_data.Size = new System.Drawing.Size(854, 333);
            this.tab_SLG_halign_data.TabIndex = 0;
            this.tab_SLG_halign_data.Text = "HALIGN";
            this.tab_SLG_halign_data.UseVisualStyleBackColor = true;
            // 
            // rtb_SLG_halign
            // 
            this.rtb_SLG_halign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_SLG_halign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SLG_halign.Location = new System.Drawing.Point(3, 3);
            this.rtb_SLG_halign.Name = "rtb_SLG_halign";
            this.rtb_SLG_halign.Size = new System.Drawing.Size(848, 327);
            this.rtb_SLG_halign.TabIndex = 62;
            this.rtb_SLG_halign.Text = "";
            this.rtb_SLG_halign.WordWrap = false;
            // 
            // tab_SLG_traverse_data
            // 
            this.tab_SLG_traverse_data.Controls.Add(this.rtb_SLG_traverse_data);
            this.tab_SLG_traverse_data.Controls.Add(this.panel12);
            this.tab_SLG_traverse_data.Location = new System.Drawing.Point(4, 22);
            this.tab_SLG_traverse_data.Name = "tab_SLG_traverse_data";
            this.tab_SLG_traverse_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SLG_traverse_data.Size = new System.Drawing.Size(854, 333);
            this.tab_SLG_traverse_data.TabIndex = 2;
            this.tab_SLG_traverse_data.Text = "TRAVERSE ALIGNMENT";
            this.tab_SLG_traverse_data.UseVisualStyleBackColor = true;
            // 
            // rtb_SLG_traverse_data
            // 
            this.rtb_SLG_traverse_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_SLG_traverse_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SLG_traverse_data.Location = new System.Drawing.Point(3, 42);
            this.rtb_SLG_traverse_data.Name = "rtb_SLG_traverse_data";
            this.rtb_SLG_traverse_data.Size = new System.Drawing.Size(848, 288);
            this.rtb_SLG_traverse_data.TabIndex = 63;
            this.rtb_SLG_traverse_data.Text = "";
            this.rtb_SLG_traverse_data.WordWrap = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.txt_SLG_trav_string);
            this.panel12.Controls.Add(this.txt_SLG_trav_model);
            this.panel12.Controls.Add(this.label26);
            this.panel12.Controls.Add(this.label29);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(3, 3);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(848, 39);
            this.panel12.TabIndex = 65;
            // 
            // txt_SLG_trav_string
            // 
            this.txt_SLG_trav_string.Location = new System.Drawing.Point(278, 11);
            this.txt_SLG_trav_string.Name = "txt_SLG_trav_string";
            this.txt_SLG_trav_string.Size = new System.Drawing.Size(50, 21);
            this.txt_SLG_trav_string.TabIndex = 1;
            this.txt_SLG_trav_string.Text = "M001";
            // 
            // txt_SLG_trav_model
            // 
            this.txt_SLG_trav_model.Location = new System.Drawing.Point(92, 11);
            this.txt_SLG_trav_model.Name = "txt_SLG_trav_model";
            this.txt_SLG_trav_model.Size = new System.Drawing.Size(65, 21);
            this.txt_SLG_trav_model.TabIndex = 1;
            this.txt_SLG_trav_model.Text = "DESIGN";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(181, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(91, 13);
            this.label26.TabIndex = 0;
            this.label26.Text = "STRING LABEL";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(83, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "MODEL NAME";
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.label3);
            this.panel10.Controls.Add(this.btn_SLG_dlts_OFF);
            this.panel10.Controls.Add(this.btn_SLG_dlts_ON);
            this.panel10.Controls.Add(this.btn_SLG_View_Halign);
            this.panel10.Controls.Add(this.btn_SLG_chn_OFF);
            this.panel10.Controls.Add(this.btn_SLG_chn_ON);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(3, 439);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(862, 159);
            this.panel10.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(4, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(388, 37);
            this.label3.TabIndex = 65;
            this.label3.Text = "Note : After Finishing a New HALIGN Design user must click on button \"View HALIGN" +
    " Design\"";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_SLG_dlts_OFF
            // 
            this.btn_SLG_dlts_OFF.Location = new System.Drawing.Point(208, 111);
            this.btn_SLG_dlts_OFF.Name = "btn_SLG_dlts_OFF";
            this.btn_SLG_dlts_OFF.Size = new System.Drawing.Size(184, 28);
            this.btn_SLG_dlts_OFF.TabIndex = 64;
            this.btn_SLG_dlts_OFF.Text = "Details OFF";
            this.btn_SLG_dlts_OFF.UseVisualStyleBackColor = true;
            this.btn_SLG_dlts_OFF.Click += new System.EventHandler(this.btn_SLG_design_horizontal_Click);
            // 
            // btn_SLG_dlts_ON
            // 
            this.btn_SLG_dlts_ON.Location = new System.Drawing.Point(4, 112);
            this.btn_SLG_dlts_ON.Name = "btn_SLG_dlts_ON";
            this.btn_SLG_dlts_ON.Size = new System.Drawing.Size(184, 28);
            this.btn_SLG_dlts_ON.TabIndex = 63;
            this.btn_SLG_dlts_ON.Text = "Details ON";
            this.btn_SLG_dlts_ON.UseVisualStyleBackColor = true;
            this.btn_SLG_dlts_ON.Click += new System.EventHandler(this.btn_SLG_design_horizontal_Click);
            // 
            // btn_SLG_View_Halign
            // 
            this.btn_SLG_View_Halign.Location = new System.Drawing.Point(4, 52);
            this.btn_SLG_View_Halign.Name = "btn_SLG_View_Halign";
            this.btn_SLG_View_Halign.Size = new System.Drawing.Size(184, 28);
            this.btn_SLG_View_Halign.TabIndex = 60;
            this.btn_SLG_View_Halign.Text = "View HALIGN Design";
            this.btn_SLG_View_Halign.UseVisualStyleBackColor = true;
            this.btn_SLG_View_Halign.Click += new System.EventHandler(this.btn_SLG_design_horizontal_Click);
            // 
            // btn_SLG_chn_OFF
            // 
            this.btn_SLG_chn_OFF.Location = new System.Drawing.Point(208, 82);
            this.btn_SLG_chn_OFF.Name = "btn_SLG_chn_OFF";
            this.btn_SLG_chn_OFF.Size = new System.Drawing.Size(184, 28);
            this.btn_SLG_chn_OFF.TabIndex = 61;
            this.btn_SLG_chn_OFF.Text = "Chainage OFF";
            this.btn_SLG_chn_OFF.UseVisualStyleBackColor = true;
            this.btn_SLG_chn_OFF.Click += new System.EventHandler(this.btn_SLG_design_horizontal_Click);
            // 
            // btn_SLG_chn_ON
            // 
            this.btn_SLG_chn_ON.Location = new System.Drawing.Point(4, 82);
            this.btn_SLG_chn_ON.Name = "btn_SLG_chn_ON";
            this.btn_SLG_chn_ON.Size = new System.Drawing.Size(184, 28);
            this.btn_SLG_chn_ON.TabIndex = 62;
            this.btn_SLG_chn_ON.Text = "Chainage ON";
            this.btn_SLG_chn_ON.UseVisualStyleBackColor = true;
            this.btn_SLG_chn_ON.Click += new System.EventHandler(this.btn_SLG_design_horizontal_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btn_SLG_halign_video);
            this.groupBox8.Controls.Add(this.btn_SLG_halign_proceed);
            this.groupBox8.Controls.Add(this.cmb_SLG_halign);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(3, 27);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(862, 53);
            this.groupBox8.TabIndex = 62;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Horizontal Alignment Design [HALIGN]";
            // 
            // btn_SLG_halign_video
            // 
            this.btn_SLG_halign_video.Location = new System.Drawing.Point(357, 16);
            this.btn_SLG_halign_video.Name = "btn_SLG_halign_video";
            this.btn_SLG_halign_video.Size = new System.Drawing.Size(103, 25);
            this.btn_SLG_halign_video.TabIndex = 1;
            this.btn_SLG_halign_video.Text = "Related Video";
            this.btn_SLG_halign_video.UseVisualStyleBackColor = true;
            // 
            // btn_SLG_halign_proceed
            // 
            this.btn_SLG_halign_proceed.Location = new System.Drawing.Point(285, 16);
            this.btn_SLG_halign_proceed.Name = "btn_SLG_halign_proceed";
            this.btn_SLG_halign_proceed.Size = new System.Drawing.Size(72, 25);
            this.btn_SLG_halign_proceed.TabIndex = 1;
            this.btn_SLG_halign_proceed.Text = "Proceed";
            this.btn_SLG_halign_proceed.UseVisualStyleBackColor = true;
            this.btn_SLG_halign_proceed.Click += new System.EventHandler(this.btn_SLG_halign_process_Click);
            // 
            // cmb_SLG_halign
            // 
            this.cmb_SLG_halign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SLG_halign.DropDownWidth = 370;
            this.cmb_SLG_halign.FormattingEnabled = true;
            this.cmb_SLG_halign.Items.AddRange(new object[] {
            "New HALIGN Design",
            "Automatic HALIGN Design Wizard",
            "Process HALIGN Design Data",
            "Process PROHALIGN Data to Create HALIGN Design Data",
            "Traverse Alignment [Total Station Data]"});
            this.cmb_SLG_halign.Location = new System.Drawing.Point(5, 19);
            this.cmb_SLG_halign.Name = "cmb_SLG_halign";
            this.cmb_SLG_halign.Size = new System.Drawing.Size(274, 21);
            this.cmb_SLG_halign.TabIndex = 0;
            this.cmb_SLG_halign.SelectedIndexChanged += new System.EventHandler(this.cmb_SLG_halign_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(862, 24);
            this.label32.TabIndex = 66;
            this.label32.Text = "HORIZONTAL ALIGNMENT";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_site_step5
            // 
            this.tab_site_step5.Controls.Add(this.sc_HDP_valign);
            this.tab_site_step5.Controls.Add(this.label21);
            this.tab_site_step5.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step5.Name = "tab_site_step5";
            this.tab_site_step5.Padding = new System.Windows.Forms.Padding(3);
            this.tab_site_step5.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step5.TabIndex = 1;
            this.tab_site_step5.Text = "STEP 5";
            this.tab_site_step5.UseVisualStyleBackColor = true;
            // 
            // sc_HDP_valign
            // 
            this.sc_HDP_valign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_HDP_valign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_HDP_valign.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc_HDP_valign.Location = new System.Drawing.Point(3, 27);
            this.sc_HDP_valign.Name = "sc_HDP_valign";
            // 
            // sc_HDP_valign.Panel1
            // 
            this.sc_HDP_valign.Panel1.Controls.Add(this.tc_HDP_valign);
            // 
            // sc_HDP_valign.Panel2
            // 
            this.sc_HDP_valign.Panel2.Controls.Add(this.vdsC_HDP_Valign);
            this.sc_HDP_valign.Size = new System.Drawing.Size(862, 571);
            this.sc_HDP_valign.SplitterDistance = 367;
            this.sc_HDP_valign.TabIndex = 31;
            this.sc_HDP_valign.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.sc_main_SplitterMoved);
            // 
            // tc_HDP_valign
            // 
            this.tc_HDP_valign.Controls.Add(this.tab_HDP_profile_opt);
            this.tc_HDP_valign.Controls.Add(this.tab_HDP_update_valign_data);
            this.tc_HDP_valign.Controls.Add(this.tab_HDP_valign_design);
            this.tc_HDP_valign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_HDP_valign.Location = new System.Drawing.Point(0, 0);
            this.tc_HDP_valign.Name = "tc_HDP_valign";
            this.tc_HDP_valign.SelectedIndex = 0;
            this.tc_HDP_valign.Size = new System.Drawing.Size(365, 569);
            this.tc_HDP_valign.TabIndex = 28;
            // 
            // tab_HDP_profile_opt
            // 
            this.tab_HDP_profile_opt.Controls.Add(this.label19);
            this.tab_HDP_profile_opt.Controls.Add(this.button14);
            this.tab_HDP_profile_opt.Controls.Add(this.btn_HDP_profile_opt_process);
            this.tab_HDP_profile_opt.Controls.Add(this.design_Profile_Optimization1);
            this.tab_HDP_profile_opt.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_profile_opt.Name = "tab_HDP_profile_opt";
            this.tab_HDP_profile_opt.Padding = new System.Windows.Forms.Padding(3);
            this.tab_HDP_profile_opt.Size = new System.Drawing.Size(357, 543);
            this.tab_HDP_profile_opt.TabIndex = 0;
            this.tab_HDP_profile_opt.Text = "VERTICAL PROFILE OPTIMIZATION DATA";
            this.tab_HDP_profile_opt.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(15, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(300, 18);
            this.label19.TabIndex = 42;
            this.label19.Text = "Automatic VALIGN Design Wizard";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(592, 553);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(154, 10);
            this.button14.TabIndex = 41;
            this.button14.Text = "Draw Carriageways";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Visible = false;
            // 
            // btn_HDP_profile_opt_process
            // 
            this.btn_HDP_profile_opt_process.Location = new System.Drawing.Point(126, 529);
            this.btn_HDP_profile_opt_process.Name = "btn_HDP_profile_opt_process";
            this.btn_HDP_profile_opt_process.Size = new System.Drawing.Size(141, 34);
            this.btn_HDP_profile_opt_process.TabIndex = 40;
            this.btn_HDP_profile_opt_process.Text = "Process Data";
            this.btn_HDP_profile_opt_process.UseVisualStyleBackColor = true;
            this.btn_HDP_profile_opt_process.Click += new System.EventHandler(this.btn_SLG_profile_opt_process_Click);
            // 
            // tab_HDP_update_valign_data
            // 
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_get_restore);
            this.tab_HDP_update_valign_data.Controls.Add(this.label124);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_update_data_prev);
            this.tab_HDP_update_valign_data.Controls.Add(this.label125);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_get_exst_lvls);
            this.tab_HDP_update_valign_data.Controls.Add(this.label126);
            this.tab_HDP_update_valign_data.Controls.Add(this.cmb_HDP_pro_opt_exist_level_str);
            this.tab_HDP_update_valign_data.Controls.Add(this.cmb_HDP_pro_opt_Bridge_sections);
            this.tab_HDP_update_valign_data.Controls.Add(this.groupBox34);
            this.tab_HDP_update_valign_data.Controls.Add(this.groupBox39);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_update_data);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_del_row_prev);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_del_row);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_insert_row_prev);
            this.tab_HDP_update_valign_data.Controls.Add(this.btn_HDP_pro_opt_insert_row);
            this.tab_HDP_update_valign_data.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_update_valign_data.Name = "tab_HDP_update_valign_data";
            this.tab_HDP_update_valign_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_HDP_update_valign_data.Size = new System.Drawing.Size(357, 543);
            this.tab_HDP_update_valign_data.TabIndex = 2;
            this.tab_HDP_update_valign_data.Text = "PROFILE AT ELEVATED SECTIONS";
            this.tab_HDP_update_valign_data.UseVisualStyleBackColor = true;
            // 
            // btn_HDP_pro_opt_get_restore
            // 
            this.btn_HDP_pro_opt_get_restore.Location = new System.Drawing.Point(587, 55);
            this.btn_HDP_pro_opt_get_restore.Name = "btn_HDP_pro_opt_get_restore";
            this.btn_HDP_pro_opt_get_restore.Size = new System.Drawing.Size(219, 52);
            this.btn_HDP_pro_opt_get_restore.TabIndex = 84;
            this.btn_HDP_pro_opt_get_restore.Text = "Restore Project Data";
            this.btn_HDP_pro_opt_get_restore.UseVisualStyleBackColor = true;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(434, 528);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(24, 13);
            this.label124.TabIndex = 83;
            this.label124.Text = "OR";
            // 
            // btn_HDP_pro_opt_update_data_prev
            // 
            this.btn_HDP_pro_opt_update_data_prev.Location = new System.Drawing.Point(490, 518);
            this.btn_HDP_pro_opt_update_data_prev.Name = "btn_HDP_pro_opt_update_data_prev";
            this.btn_HDP_pro_opt_update_data_prev.Size = new System.Drawing.Size(357, 32);
            this.btn_HDP_pro_opt_update_data_prev.TabIndex = 82;
            this.btn_HDP_pro_opt_update_data_prev.Text = "Update VALIGN Data for Current Bridge Section";
            this.btn_HDP_pro_opt_update_data_prev.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_update_data_prev.Click += new System.EventHandler(this.btn_HDP_pro_opt_update_data_Click);
            // 
            // label125
            // 
            this.label125.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.ForeColor = System.Drawing.Color.Red;
            this.label125.Location = new System.Drawing.Point(70, 19);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(706, 29);
            this.label125.TabIndex = 81;
            // 
            // btn_HDP_pro_opt_get_exst_lvls
            // 
            this.btn_HDP_pro_opt_get_exst_lvls.Location = new System.Drawing.Point(342, 55);
            this.btn_HDP_pro_opt_get_exst_lvls.Name = "btn_HDP_pro_opt_get_exst_lvls";
            this.btn_HDP_pro_opt_get_exst_lvls.Size = new System.Drawing.Size(219, 52);
            this.btn_HDP_pro_opt_get_exst_lvls.TabIndex = 80;
            this.btn_HDP_pro_opt_get_exst_lvls.Text = "Get Existing Levels";
            this.btn_HDP_pro_opt_get_exst_lvls.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_get_exst_lvls.Click += new System.EventHandler(this.btn_HDP_pro_opt_get_exst_lvls_Click);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(21, 58);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(190, 13);
            this.label126.TabIndex = 79;
            this.label126.Text = "Select String For Existing Levels";
            // 
            // cmb_HDP_pro_opt_exist_level_str
            // 
            this.cmb_HDP_pro_opt_exist_level_str.FormattingEnabled = true;
            this.cmb_HDP_pro_opt_exist_level_str.Items.AddRange(new object[] {
            "EL01",
            "ER01"});
            this.cmb_HDP_pro_opt_exist_level_str.Location = new System.Drawing.Point(257, 57);
            this.cmb_HDP_pro_opt_exist_level_str.Name = "cmb_HDP_pro_opt_exist_level_str";
            this.cmb_HDP_pro_opt_exist_level_str.Size = new System.Drawing.Size(79, 21);
            this.cmb_HDP_pro_opt_exist_level_str.TabIndex = 73;
            this.cmb_HDP_pro_opt_exist_level_str.Text = "E001";
            // 
            // cmb_HDP_pro_opt_Bridge_sections
            // 
            this.cmb_HDP_pro_opt_Bridge_sections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HDP_pro_opt_Bridge_sections.FormattingEnabled = true;
            this.cmb_HDP_pro_opt_Bridge_sections.Location = new System.Drawing.Point(117, 84);
            this.cmb_HDP_pro_opt_Bridge_sections.Name = "cmb_HDP_pro_opt_Bridge_sections";
            this.cmb_HDP_pro_opt_Bridge_sections.Size = new System.Drawing.Size(219, 21);
            this.cmb_HDP_pro_opt_Bridge_sections.TabIndex = 72;
            this.cmb_HDP_pro_opt_Bridge_sections.SelectedIndexChanged += new System.EventHandler(this.cmb_HDP_pro_opt_Bridge_sections_SelectedIndexChanged);
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.dgv_HDP_pro_opt_prev_data);
            this.groupBox34.Location = new System.Drawing.Point(487, 123);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(360, 355);
            this.groupBox34.TabIndex = 77;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "Modify Previous VIP Data ";
            // 
            // dgv_HDP_pro_opt_prev_data
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HDP_pro_opt_prev_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_HDP_pro_opt_prev_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_HDP_pro_opt_prev_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgv_HDP_pro_opt_prev_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_HDP_pro_opt_prev_data.Location = new System.Drawing.Point(3, 51);
            this.dgv_HDP_pro_opt_prev_data.Name = "dgv_HDP_pro_opt_prev_data";
            this.dgv_HDP_pro_opt_prev_data.RowHeadersWidth = 27;
            this.dgv_HDP_pro_opt_prev_data.Size = new System.Drawing.Size(354, 301);
            this.dgv_HDP_pro_opt_prev_data.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Chainage (m)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 108;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Proposed Levels(m)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 99;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Vertical Curve Length (VCL)(m)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 99;
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.txt_HDP_pro_opt_props_VCL);
            this.groupBox39.Controls.Add(this.label128);
            this.groupBox39.Controls.Add(this.txt_HDP_pro_opt_props_hgt);
            this.groupBox39.Controls.Add(this.btn_HDP_pro_opt_props_hgt);
            this.groupBox39.Controls.Add(this.label127);
            this.groupBox39.Controls.Add(this.dgv_HDP_pro_opt_chns);
            this.groupBox39.Location = new System.Drawing.Point(15, 123);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(466, 355);
            this.groupBox39.TabIndex = 78;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "Redefine VIP Data ";
            // 
            // txt_HDP_pro_opt_props_VCL
            // 
            this.txt_HDP_pro_opt_props_VCL.Location = new System.Drawing.Point(406, 19);
            this.txt_HDP_pro_opt_props_VCL.Name = "txt_HDP_pro_opt_props_VCL";
            this.txt_HDP_pro_opt_props_VCL.Size = new System.Drawing.Size(54, 21);
            this.txt_HDP_pro_opt_props_VCL.TabIndex = 40;
            this.txt_HDP_pro_opt_props_VCL.Text = "30";
            this.txt_HDP_pro_opt_props_VCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(370, 23);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(30, 13);
            this.label128.TabIndex = 41;
            this.label128.Text = "VCL";
            // 
            // txt_HDP_pro_opt_props_hgt
            // 
            this.txt_HDP_pro_opt_props_hgt.Location = new System.Drawing.Point(126, 19);
            this.txt_HDP_pro_opt_props_hgt.Name = "txt_HDP_pro_opt_props_hgt";
            this.txt_HDP_pro_opt_props_hgt.Size = new System.Drawing.Size(54, 21);
            this.txt_HDP_pro_opt_props_hgt.TabIndex = 35;
            this.txt_HDP_pro_opt_props_hgt.Text = "7.5";
            this.txt_HDP_pro_opt_props_hgt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_HDP_pro_opt_props_hgt
            // 
            this.btn_HDP_pro_opt_props_hgt.Location = new System.Drawing.Point(186, 17);
            this.btn_HDP_pro_opt_props_hgt.Name = "btn_HDP_pro_opt_props_hgt";
            this.btn_HDP_pro_opt_props_hgt.Size = new System.Drawing.Size(180, 24);
            this.btn_HDP_pro_opt_props_hgt.TabIndex = 6;
            this.btn_HDP_pro_opt_props_hgt.Text = "Get Proposed Levels && VCL";
            this.btn_HDP_pro_opt_props_hgt.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_props_hgt.Click += new System.EventHandler(this.btn_HDP_pro_opt_props_hgt_Click);
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(6, 22);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(119, 13);
            this.label127.TabIndex = 39;
            this.label127.Text = "Height /Depth (+/-)";
            // 
            // dgv_HDP_pro_opt_chns
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HDP_pro_opt_chns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_HDP_pro_opt_chns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_HDP_pro_opt_chns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dgv_HDP_pro_opt_chns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_HDP_pro_opt_chns.Location = new System.Drawing.Point(3, 51);
            this.dgv_HDP_pro_opt_chns.Name = "dgv_HDP_pro_opt_chns";
            this.dgv_HDP_pro_opt_chns.RowHeadersWidth = 27;
            this.dgv_HDP_pro_opt_chns.Size = new System.Drawing.Size(460, 301);
            this.dgv_HDP_pro_opt_chns.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Chainage (m)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 108;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Existing Levels(m)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 99;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Proposed Levels(m)";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 99;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Vertical Curve Length (VCL)(m)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 99;
            // 
            // btn_HDP_pro_opt_update_data
            // 
            this.btn_HDP_pro_opt_update_data.Location = new System.Drawing.Point(52, 518);
            this.btn_HDP_pro_opt_update_data.Name = "btn_HDP_pro_opt_update_data";
            this.btn_HDP_pro_opt_update_data.Size = new System.Drawing.Size(357, 32);
            this.btn_HDP_pro_opt_update_data.TabIndex = 74;
            this.btn_HDP_pro_opt_update_data.Text = "Update VALIGN Data for Current Bridge Section";
            this.btn_HDP_pro_opt_update_data.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_update_data.Click += new System.EventHandler(this.btn_HDP_pro_opt_update_data_Click);
            // 
            // btn_HDP_pro_opt_del_row_prev
            // 
            this.btn_HDP_pro_opt_del_row_prev.Location = new System.Drawing.Point(678, 484);
            this.btn_HDP_pro_opt_del_row_prev.Name = "btn_HDP_pro_opt_del_row_prev";
            this.btn_HDP_pro_opt_del_row_prev.Size = new System.Drawing.Size(166, 22);
            this.btn_HDP_pro_opt_del_row_prev.TabIndex = 75;
            this.btn_HDP_pro_opt_del_row_prev.Text = "Delete Row";
            this.btn_HDP_pro_opt_del_row_prev.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_del_row_prev.Click += new System.EventHandler(this.btn_HDP_pro_opt_del_row_Click);
            // 
            // btn_HDP_pro_opt_del_row
            // 
            this.btn_HDP_pro_opt_del_row.Location = new System.Drawing.Point(243, 481);
            this.btn_HDP_pro_opt_del_row.Name = "btn_HDP_pro_opt_del_row";
            this.btn_HDP_pro_opt_del_row.Size = new System.Drawing.Size(166, 22);
            this.btn_HDP_pro_opt_del_row.TabIndex = 75;
            this.btn_HDP_pro_opt_del_row.Text = "Delete Row";
            this.btn_HDP_pro_opt_del_row.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_del_row.Click += new System.EventHandler(this.btn_HDP_pro_opt_del_row_Click);
            // 
            // btn_HDP_pro_opt_insert_row_prev
            // 
            this.btn_HDP_pro_opt_insert_row_prev.Location = new System.Drawing.Point(487, 484);
            this.btn_HDP_pro_opt_insert_row_prev.Name = "btn_HDP_pro_opt_insert_row_prev";
            this.btn_HDP_pro_opt_insert_row_prev.Size = new System.Drawing.Size(166, 22);
            this.btn_HDP_pro_opt_insert_row_prev.TabIndex = 76;
            this.btn_HDP_pro_opt_insert_row_prev.Text = "Insert Row";
            this.btn_HDP_pro_opt_insert_row_prev.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_insert_row_prev.Click += new System.EventHandler(this.btn_HDP_pro_opt_del_row_Click);
            // 
            // btn_HDP_pro_opt_insert_row
            // 
            this.btn_HDP_pro_opt_insert_row.Location = new System.Drawing.Point(52, 481);
            this.btn_HDP_pro_opt_insert_row.Name = "btn_HDP_pro_opt_insert_row";
            this.btn_HDP_pro_opt_insert_row.Size = new System.Drawing.Size(166, 22);
            this.btn_HDP_pro_opt_insert_row.TabIndex = 76;
            this.btn_HDP_pro_opt_insert_row.Text = "Insert Row";
            this.btn_HDP_pro_opt_insert_row.UseVisualStyleBackColor = true;
            this.btn_HDP_pro_opt_insert_row.Click += new System.EventHandler(this.btn_HDP_pro_opt_del_row_Click);
            // 
            // tab_HDP_valign_design
            // 
            this.tab_HDP_valign_design.Controls.Add(this.label143);
            this.tab_HDP_valign_design.Controls.Add(this.groupBox36);
            this.tab_HDP_valign_design.Controls.Add(this.groupBox17);
            this.tab_HDP_valign_design.Controls.Add(this.groupBox29);
            this.tab_HDP_valign_design.Controls.Add(this.txt_HDP_ValignData);
            this.tab_HDP_valign_design.Controls.Add(this.btn_HDP_valign_draw_selected_profile);
            this.tab_HDP_valign_design.Controls.Add(this.btn_HDP_valign_draw_glongsec);
            this.tab_HDP_valign_design.Controls.Add(this.btn_HDP_valign_draw_vertical_profile);
            this.tab_HDP_valign_design.Controls.Add(this.btn_valign_grid_on);
            this.tab_HDP_valign_design.Controls.Add(this.btn_valign_details_off);
            this.tab_HDP_valign_design.Controls.Add(this.btn_HDP_create_ground_longsec);
            this.tab_HDP_valign_design.Controls.Add(this.btn_valign_details_on);
            this.tab_HDP_valign_design.Controls.Add(this.btn_valign_grid_off);
            this.tab_HDP_valign_design.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_valign_design.Name = "tab_HDP_valign_design";
            this.tab_HDP_valign_design.Padding = new System.Windows.Forms.Padding(3);
            this.tab_HDP_valign_design.Size = new System.Drawing.Size(357, 543);
            this.tab_HDP_valign_design.TabIndex = 1;
            this.tab_HDP_valign_design.Text = "DESIGN VERTICAL PROFILE (VALIGN)";
            this.tab_HDP_valign_design.UseVisualStyleBackColor = true;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label143.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.ForeColor = System.Drawing.Color.Red;
            this.label143.Location = new System.Drawing.Point(12, 88);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(363, 28);
            this.label143.TabIndex = 67;
            this.label143.Text = "Note : After Finishing a New VALIGN Design user must \r\n            click on butto" +
    "n \"Draw Vertical Profile\"";
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.label72);
            this.groupBox36.Controls.Add(this.txt_HDP_Elevation);
            this.groupBox36.Controls.Add(this.label71);
            this.groupBox36.Controls.Add(this.btn_HDP_Elevation);
            this.groupBox36.Location = new System.Drawing.Point(7, 200);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Size = new System.Drawing.Size(377, 38);
            this.groupBox36.TabIndex = 58;
            this.groupBox36.TabStop = false;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(277, 17);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(18, 13);
            this.label72.TabIndex = 60;
            this.label72.Text = "m";
            // 
            // txt_HDP_Elevation
            // 
            this.txt_HDP_Elevation.Location = new System.Drawing.Point(212, 14);
            this.txt_HDP_Elevation.Name = "txt_HDP_Elevation";
            this.txt_HDP_Elevation.Size = new System.Drawing.Size(57, 21);
            this.txt_HDP_Elevation.TabIndex = 59;
            this.txt_HDP_Elevation.Text = "1.0";
            this.txt_HDP_Elevation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(2, 9);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(204, 26);
            this.label71.TabIndex = 1;
            this.label71.Text = "Add Constants to All \r\nElevations except at Start and End";
            // 
            // btn_HDP_Elevation
            // 
            this.btn_HDP_Elevation.Location = new System.Drawing.Point(298, 12);
            this.btn_HDP_Elevation.Name = "btn_HDP_Elevation";
            this.btn_HDP_Elevation.Size = new System.Drawing.Size(60, 23);
            this.btn_HDP_Elevation.TabIndex = 0;
            this.btn_HDP_Elevation.Text = "Add";
            this.btn_HDP_Elevation.UseVisualStyleBackColor = true;
            this.btn_HDP_Elevation.Click += new System.EventHandler(this.btn_HDP_Elevation_Click);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.button1);
            this.groupBox17.Controls.Add(this.btn_valign_open_vip_design);
            this.groupBox17.Controls.Add(this.btn_HDP_Get_Valign_Data);
            this.groupBox17.Controls.Add(this.btn_valign_new_vip_design);
            this.groupBox17.Controls.Add(this.btn_HDP_valign_open_viewer);
            this.groupBox17.Controls.Add(this.btn_valign_process);
            this.groupBox17.Location = new System.Drawing.Point(498, 6);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(86, 10);
            this.groupBox17.TabIndex = 20;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Vertical Alignment Design [VALIGN]";
            this.groupBox17.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "Automatic VALIGN Design Wizard";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_valign_open_vip_design
            // 
            this.btn_valign_open_vip_design.Location = new System.Drawing.Point(11, 20);
            this.btn_valign_open_vip_design.Name = "btn_valign_open_vip_design";
            this.btn_valign_open_vip_design.Size = new System.Drawing.Size(214, 41);
            this.btn_valign_open_vip_design.TabIndex = 1;
            this.btn_valign_open_vip_design.Text = "VALIGN Design";
            this.btn_valign_open_vip_design.UseVisualStyleBackColor = true;
            // 
            // btn_HDP_Get_Valign_Data
            // 
            this.btn_HDP_Get_Valign_Data.Location = new System.Drawing.Point(246, 25);
            this.btn_HDP_Get_Valign_Data.Name = "btn_HDP_Get_Valign_Data";
            this.btn_HDP_Get_Valign_Data.Size = new System.Drawing.Size(151, 36);
            this.btn_HDP_Get_Valign_Data.TabIndex = 56;
            this.btn_HDP_Get_Valign_Data.Text = "Get VALIGN Data";
            this.btn_HDP_Get_Valign_Data.UseVisualStyleBackColor = true;
            // 
            // btn_valign_new_vip_design
            // 
            this.btn_valign_new_vip_design.Location = new System.Drawing.Point(11, 160);
            this.btn_valign_new_vip_design.Name = "btn_valign_new_vip_design";
            this.btn_valign_new_vip_design.Size = new System.Drawing.Size(214, 25);
            this.btn_valign_new_vip_design.TabIndex = 0;
            this.btn_valign_new_vip_design.Text = "New VIP Design";
            this.btn_valign_new_vip_design.UseVisualStyleBackColor = true;
            // 
            // btn_HDP_valign_open_viewer
            // 
            this.btn_HDP_valign_open_viewer.Location = new System.Drawing.Point(11, 191);
            this.btn_HDP_valign_open_viewer.Name = "btn_HDP_valign_open_viewer";
            this.btn_HDP_valign_open_viewer.Size = new System.Drawing.Size(214, 25);
            this.btn_HDP_valign_open_viewer.TabIndex = 27;
            this.btn_HDP_valign_open_viewer.Text = "HEADS Viewer";
            this.btn_HDP_valign_open_viewer.UseVisualStyleBackColor = true;
            // 
            // btn_valign_process
            // 
            this.btn_valign_process.Location = new System.Drawing.Point(11, 129);
            this.btn_valign_process.Name = "btn_valign_process";
            this.btn_valign_process.Size = new System.Drawing.Size(214, 25);
            this.btn_valign_process.TabIndex = 0;
            this.btn_valign_process.Text = "Process Valign Design Data";
            this.btn_valign_process.UseVisualStyleBackColor = true;
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.btn_HDP_Valign_video);
            this.groupBox29.Controls.Add(this.btn_HDP_Valign_proceed);
            this.groupBox29.Controls.Add(this.cmb_HDP_Valign);
            this.groupBox29.Location = new System.Drawing.Point(7, 37);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(394, 48);
            this.groupBox29.TabIndex = 57;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Vertical Alignment Design [VALIGN]";
            // 
            // btn_HDP_Valign_video
            // 
            this.btn_HDP_Valign_video.Location = new System.Drawing.Point(285, 16);
            this.btn_HDP_Valign_video.Name = "btn_HDP_Valign_video";
            this.btn_HDP_Valign_video.Size = new System.Drawing.Size(101, 25);
            this.btn_HDP_Valign_video.TabIndex = 2;
            this.btn_HDP_Valign_video.Text = "Related Video";
            this.btn_HDP_Valign_video.UseVisualStyleBackColor = true;
            this.btn_HDP_Valign_video.Click += new System.EventHandler(this.btn_HDP_Valign_video_Click);
            // 
            // btn_HDP_Valign_proceed
            // 
            this.btn_HDP_Valign_proceed.Location = new System.Drawing.Point(217, 16);
            this.btn_HDP_Valign_proceed.Name = "btn_HDP_Valign_proceed";
            this.btn_HDP_Valign_proceed.Size = new System.Drawing.Size(64, 25);
            this.btn_HDP_Valign_proceed.TabIndex = 1;
            this.btn_HDP_Valign_proceed.Text = "Proceed";
            this.btn_HDP_Valign_proceed.UseVisualStyleBackColor = true;
            this.btn_HDP_Valign_proceed.Click += new System.EventHandler(this.btn_HDP_Valign_proceed_Click);
            // 
            // cmb_HDP_Valign
            // 
            this.cmb_HDP_Valign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HDP_Valign.FormattingEnabled = true;
            this.cmb_HDP_Valign.Items.AddRange(new object[] {
            "New VALIGN Design",
            "Automatic VALIGN Design Wizard",
            "Process VALIGN Design Data",
            "VALIGN Profile Inputs"});
            this.cmb_HDP_Valign.Location = new System.Drawing.Point(5, 19);
            this.cmb_HDP_Valign.Name = "cmb_HDP_Valign";
            this.cmb_HDP_Valign.Size = new System.Drawing.Size(212, 21);
            this.cmb_HDP_Valign.TabIndex = 0;
            // 
            // txt_HDP_ValignData
            // 
            this.txt_HDP_ValignData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_HDP_ValignData.Location = new System.Drawing.Point(3, 241);
            this.txt_HDP_ValignData.Multiline = true;
            this.txt_HDP_ValignData.Name = "txt_HDP_ValignData";
            this.txt_HDP_ValignData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_HDP_ValignData.Size = new System.Drawing.Size(351, 299);
            this.txt_HDP_ValignData.TabIndex = 55;
            this.txt_HDP_ValignData.WordWrap = false;
            // 
            // btn_HDP_valign_draw_selected_profile
            // 
            this.btn_HDP_valign_draw_selected_profile.Location = new System.Drawing.Point(207, 117);
            this.btn_HDP_valign_draw_selected_profile.Name = "btn_HDP_valign_draw_selected_profile";
            this.btn_HDP_valign_draw_selected_profile.Size = new System.Drawing.Size(186, 27);
            this.btn_HDP_valign_draw_selected_profile.TabIndex = 54;
            this.btn_HDP_valign_draw_selected_profile.Text = "Draw Selected Profile";
            this.btn_HDP_valign_draw_selected_profile.UseVisualStyleBackColor = true;
            this.btn_HDP_valign_draw_selected_profile.Click += new System.EventHandler(this.btn_valign_draw_glongsec_Click);
            // 
            // btn_HDP_valign_draw_glongsec
            // 
            this.btn_HDP_valign_draw_glongsec.Location = new System.Drawing.Point(207, 8);
            this.btn_HDP_valign_draw_glongsec.Name = "btn_HDP_valign_draw_glongsec";
            this.btn_HDP_valign_draw_glongsec.Size = new System.Drawing.Size(186, 27);
            this.btn_HDP_valign_draw_glongsec.TabIndex = 53;
            this.btn_HDP_valign_draw_glongsec.Text = "Draw Ground Long Section";
            this.btn_HDP_valign_draw_glongsec.UseVisualStyleBackColor = true;
            this.btn_HDP_valign_draw_glongsec.Click += new System.EventHandler(this.btn_valign_draw_glongsec_Click);
            // 
            // btn_HDP_valign_draw_vertical_profile
            // 
            this.btn_HDP_valign_draw_vertical_profile.Location = new System.Drawing.Point(6, 117);
            this.btn_HDP_valign_draw_vertical_profile.Name = "btn_HDP_valign_draw_vertical_profile";
            this.btn_HDP_valign_draw_vertical_profile.Size = new System.Drawing.Size(186, 27);
            this.btn_HDP_valign_draw_vertical_profile.TabIndex = 1;
            this.btn_HDP_valign_draw_vertical_profile.Text = "Draw Vertical Profile";
            this.btn_HDP_valign_draw_vertical_profile.UseVisualStyleBackColor = true;
            this.btn_HDP_valign_draw_vertical_profile.Click += new System.EventHandler(this.btn_valign_draw_glongsec_Click);
            // 
            // btn_valign_grid_on
            // 
            this.btn_valign_grid_on.Location = new System.Drawing.Point(6, 145);
            this.btn_valign_grid_on.Name = "btn_valign_grid_on";
            this.btn_valign_grid_on.Size = new System.Drawing.Size(186, 27);
            this.btn_valign_grid_on.TabIndex = 1;
            this.btn_valign_grid_on.Text = "Grid ON";
            this.btn_valign_grid_on.UseVisualStyleBackColor = true;
            this.btn_valign_grid_on.Click += new System.EventHandler(this.btn_design_Valign_Click);
            // 
            // btn_valign_details_off
            // 
            this.btn_valign_details_off.Location = new System.Drawing.Point(207, 172);
            this.btn_valign_details_off.Name = "btn_valign_details_off";
            this.btn_valign_details_off.Size = new System.Drawing.Size(186, 27);
            this.btn_valign_details_off.TabIndex = 1;
            this.btn_valign_details_off.Text = "Vertical Details OFF";
            this.btn_valign_details_off.UseVisualStyleBackColor = true;
            this.btn_valign_details_off.Click += new System.EventHandler(this.btn_design_Valign_Click);
            // 
            // btn_HDP_create_ground_longsec
            // 
            this.btn_HDP_create_ground_longsec.Location = new System.Drawing.Point(6, 8);
            this.btn_HDP_create_ground_longsec.Name = "btn_HDP_create_ground_longsec";
            this.btn_HDP_create_ground_longsec.Size = new System.Drawing.Size(186, 27);
            this.btn_HDP_create_ground_longsec.TabIndex = 0;
            this.btn_HDP_create_ground_longsec.Text = "Create Ground Long Section";
            this.btn_HDP_create_ground_longsec.UseVisualStyleBackColor = true;
            this.btn_HDP_create_ground_longsec.Click += new System.EventHandler(this.btn_valign_process_Click);
            // 
            // btn_valign_details_on
            // 
            this.btn_valign_details_on.Location = new System.Drawing.Point(6, 172);
            this.btn_valign_details_on.Name = "btn_valign_details_on";
            this.btn_valign_details_on.Size = new System.Drawing.Size(186, 27);
            this.btn_valign_details_on.TabIndex = 2;
            this.btn_valign_details_on.Text = "Vertical Details ON";
            this.btn_valign_details_on.UseVisualStyleBackColor = true;
            this.btn_valign_details_on.Click += new System.EventHandler(this.btn_design_Valign_Click);
            // 
            // btn_valign_grid_off
            // 
            this.btn_valign_grid_off.Location = new System.Drawing.Point(207, 145);
            this.btn_valign_grid_off.Name = "btn_valign_grid_off";
            this.btn_valign_grid_off.Size = new System.Drawing.Size(186, 27);
            this.btn_valign_grid_off.TabIndex = 1;
            this.btn_valign_grid_off.Text = "Grid OFF";
            this.btn_valign_grid_off.UseVisualStyleBackColor = true;
            this.btn_valign_grid_off.Click += new System.EventHandler(this.btn_design_Valign_Click);
            // 
            // label21
            // 
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(862, 24);
            this.label21.TabIndex = 30;
            this.label21.Text = "VERTICAL PROFILE";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_site_step6
            // 
            this.tab_site_step6.Controls.Add(this.tc_SLG_cross_secion);
            this.tab_site_step6.Controls.Add(this.label20);
            this.tab_site_step6.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step6.Name = "tab_site_step6";
            this.tab_site_step6.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step6.TabIndex = 2;
            this.tab_site_step6.Text = "STEP 6";
            this.tab_site_step6.UseVisualStyleBackColor = true;
            // 
            // tc_SLG_cross_secion
            // 
            this.tc_SLG_cross_secion.Controls.Add(this.tab_HDP_define_cross_section);
            this.tc_SLG_cross_secion.Controls.Add(this.tab_HDP_interface);
            this.tc_SLG_cross_secion.Controls.Add(this.tab_HDP_create_cross_section);
            this.tc_SLG_cross_secion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_SLG_cross_secion.Location = new System.Drawing.Point(0, 24);
            this.tc_SLG_cross_secion.Name = "tc_SLG_cross_secion";
            this.tc_SLG_cross_secion.SelectedIndex = 0;
            this.tc_SLG_cross_secion.Size = new System.Drawing.Size(868, 577);
            this.tc_SLG_cross_secion.TabIndex = 1;
            this.tc_SLG_cross_secion.SelectedIndexChanged += new System.EventHandler(this.tc_main_SelectedIndexChanged);
            // 
            // tab_HDP_define_cross_section
            // 
            this.tab_HDP_define_cross_section.Controls.Add(this.sc_SLG_cross_section);
            this.tab_HDP_define_cross_section.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_define_cross_section.Name = "tab_HDP_define_cross_section";
            this.tab_HDP_define_cross_section.Padding = new System.Windows.Forms.Padding(3);
            this.tab_HDP_define_cross_section.Size = new System.Drawing.Size(860, 551);
            this.tab_HDP_define_cross_section.TabIndex = 4;
            this.tab_HDP_define_cross_section.Text = "Define Cross Section";
            this.tab_HDP_define_cross_section.UseVisualStyleBackColor = true;
            // 
            // sc_SLG_cross_section
            // 
            this.sc_SLG_cross_section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_SLG_cross_section.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_SLG_cross_section.Location = new System.Drawing.Point(3, 3);
            this.sc_SLG_cross_section.Name = "sc_SLG_cross_section";
            this.sc_SLG_cross_section.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc_SLG_cross_section.Panel1
            // 
            this.sc_SLG_cross_section.Panel1.Controls.Add(this.uC_Dyke_CrossSection1);
            this.sc_SLG_cross_section.Panel1.Controls.Add(this.uC_Canal_CrossSection1);
            // 
            // sc_SLG_cross_section.Panel2
            // 
            this.sc_SLG_cross_section.Panel2.Controls.Add(this.sc_offset);
            this.sc_SLG_cross_section.Panel2.Controls.Add(this.lbl_off_2);
            this.sc_SLG_cross_section.Size = new System.Drawing.Size(854, 545);
            this.sc_SLG_cross_section.SplitterDistance = 76;
            this.sc_SLG_cross_section.TabIndex = 36;
            // 
            // sc_offset
            // 
            this.sc_offset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_offset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_offset.Location = new System.Drawing.Point(0, 28);
            this.sc_offset.Name = "sc_offset";
            this.sc_offset.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc_offset.Panel1
            // 
            this.sc_offset.Panel1.Controls.Add(this.dgv_offset_service);
            this.sc_offset.Panel1.Controls.Add(this.panel13);
            // 
            // sc_offset.Panel2
            // 
            this.sc_offset.Panel2.Controls.Add(this.vdsC_DOC_river_canal);
            this.sc_offset.Size = new System.Drawing.Size(854, 437);
            this.sc_offset.SplitterDistance = 154;
            this.sc_offset.TabIndex = 37;
            // 
            // dgv_offset_service
            // 
            this.dgv_offset_service.AllowUserToAddRows = false;
            this.dgv_offset_service.AllowUserToDeleteRows = false;
            this.dgv_offset_service.AllowUserToOrderColumns = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_offset_service.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_offset_service.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_offset_service.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_offset_service.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_offset_service.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_offset_service.Location = new System.Drawing.Point(0, 0);
            this.dgv_offset_service.Name = "dgv_offset_service";
            this.dgv_offset_service.RowHeadersWidth = 27;
            this.dgv_offset_service.Size = new System.Drawing.Size(852, 115);
            this.dgv_offset_service.TabIndex = 34;
            this.dgv_offset_service.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_offset_service_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Reference String";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 99;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Offset String";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Start Chainage (CH1)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 72;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "End Chainage (CH2)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 72;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Start Width (HO1)";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 72;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "End width (HO2)";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 72;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Start Height (VO1)";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 60;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "Start Height (VO2)";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 60;
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.btn_offset_delete);
            this.panel13.Controls.Add(this.btn_offset_insert);
            this.panel13.Controls.Add(this.btn_offset_add);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel13.Location = new System.Drawing.Point(0, 115);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(852, 37);
            this.panel13.TabIndex = 35;
            // 
            // btn_offset_delete
            // 
            this.btn_offset_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_offset_delete.Location = new System.Drawing.Point(447, 5);
            this.btn_offset_delete.Name = "btn_offset_delete";
            this.btn_offset_delete.Size = new System.Drawing.Size(83, 23);
            this.btn_offset_delete.TabIndex = 0;
            this.btn_offset_delete.Text = "Delete Row";
            this.btn_offset_delete.UseVisualStyleBackColor = true;
            this.btn_offset_delete.Click += new System.EventHandler(this.btn_offset_add_Click);
            // 
            // btn_offset_insert
            // 
            this.btn_offset_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_offset_insert.Location = new System.Drawing.Point(358, 5);
            this.btn_offset_insert.Name = "btn_offset_insert";
            this.btn_offset_insert.Size = new System.Drawing.Size(83, 23);
            this.btn_offset_insert.TabIndex = 0;
            this.btn_offset_insert.Text = "Insert Row";
            this.btn_offset_insert.UseVisualStyleBackColor = true;
            this.btn_offset_insert.Click += new System.EventHandler(this.btn_offset_add_Click);
            // 
            // btn_offset_add
            // 
            this.btn_offset_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_offset_add.Location = new System.Drawing.Point(269, 5);
            this.btn_offset_add.Name = "btn_offset_add";
            this.btn_offset_add.Size = new System.Drawing.Size(83, 23);
            this.btn_offset_add.TabIndex = 0;
            this.btn_offset_add.Text = "Add Row";
            this.btn_offset_add.UseVisualStyleBackColor = true;
            this.btn_offset_add.Click += new System.EventHandler(this.btn_offset_add_Click);
            // 
            // lbl_off_2
            // 
            this.lbl_off_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_off_2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_off_2.Location = new System.Drawing.Point(0, 0);
            this.lbl_off_2.Name = "lbl_off_2";
            this.lbl_off_2.Size = new System.Drawing.Size(854, 28);
            this.lbl_off_2.TabIndex = 35;
            this.lbl_off_2.Text = "OFFSET DATA";
            this.lbl_off_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_HDP_interface
            // 
            this.tab_HDP_interface.Controls.Add(this.tabControl2);
            this.tab_HDP_interface.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_interface.Name = "tab_HDP_interface";
            this.tab_HDP_interface.Size = new System.Drawing.Size(860, 551);
            this.tab_HDP_interface.TabIndex = 2;
            this.tab_HDP_interface.Text = "Interface";
            this.tab_HDP_interface.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(860, 551);
            this.tabControl2.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button8);
            this.tabPage6.Controls.Add(this.interface_HDP);
            this.tabPage6.Controls.Add(this.grb_hill_road);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(852, 525);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Interface Data";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(549, 322);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(198, 80);
            this.button8.TabIndex = 5;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // grb_hill_road
            // 
            this.grb_hill_road.Controls.Add(this.rbtn_HRP_interface_2);
            this.grb_hill_road.Controls.Add(this.rbtn_HRP_interface_1);
            this.grb_hill_road.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_hill_road.Location = new System.Drawing.Point(3, 3);
            this.grb_hill_road.Name = "grb_hill_road";
            this.grb_hill_road.Size = new System.Drawing.Size(846, 77);
            this.grb_hill_road.TabIndex = 3;
            this.grb_hill_road.TabStop = false;
            this.grb_hill_road.Text = "Interface Option";
            this.grb_hill_road.Visible = false;
            // 
            // rbtn_HRP_interface_2
            // 
            this.rbtn_HRP_interface_2.AutoSize = true;
            this.rbtn_HRP_interface_2.Location = new System.Drawing.Point(15, 48);
            this.rbtn_HRP_interface_2.Name = "rbtn_HRP_interface_2";
            this.rbtn_HRP_interface_2.Size = new System.Drawing.Size(284, 17);
            this.rbtn_HRP_interface_2.TabIndex = 1;
            this.rbtn_HRP_interface_2.Text = "INTERAFCE Data with General Standard Data";
            this.rbtn_HRP_interface_2.UseVisualStyleBackColor = true;
            // 
            // rbtn_HRP_interface_1
            // 
            this.rbtn_HRP_interface_1.Checked = true;
            this.rbtn_HRP_interface_1.Location = new System.Drawing.Point(15, 13);
            this.rbtn_HRP_interface_1.Name = "rbtn_HRP_interface_1";
            this.rbtn_HRP_interface_1.Size = new System.Drawing.Size(498, 36);
            this.rbtn_HRP_interface_1.TabIndex = 0;
            this.rbtn_HRP_interface_1.TabStop = true;
            this.rbtn_HRP_interface_1.Text = "INTERFACE Data with provisions for Retaining Wall on Valley side, Gabion Walls on" +
    " Hill Cut side and slope from flatter to steeper varying with depth of cut.";
            this.rbtn_HRP_interface_1.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.interfaceNote2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(852, 525);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Interface Note";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tab_HDP_create_cross_section
            // 
            this.tab_HDP_create_cross_section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_HDP_create_cross_section.Controls.Add(this.splitContainer7);
            this.tab_HDP_create_cross_section.Location = new System.Drawing.Point(4, 22);
            this.tab_HDP_create_cross_section.Name = "tab_HDP_create_cross_section";
            this.tab_HDP_create_cross_section.Padding = new System.Windows.Forms.Padding(3);
            this.tab_HDP_create_cross_section.Size = new System.Drawing.Size(860, 551);
            this.tab_HDP_create_cross_section.TabIndex = 3;
            this.tab_HDP_create_cross_section.Text = "Create Cross Section";
            this.tab_HDP_create_cross_section.UseVisualStyleBackColor = true;
            // 
            // splitContainer7
            // 
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(3, 3);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.btn_cross_section_create);
            this.splitContainer7.Panel1.Controls.Add(this.groupBox11);
            this.splitContainer7.Panel1.Controls.Add(this.btn_cross_section_draw);
            this.splitContainer7.Panel1.Controls.Add(this.btn_cross_section_process);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.vdsC_HDP_CrossSect);
            this.splitContainer7.Size = new System.Drawing.Size(852, 543);
            this.splitContainer7.SplitterDistance = 104;
            this.splitContainer7.TabIndex = 1;
            this.splitContainer7.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.sc_main_SplitterMoved);
            // 
            // btn_cross_section_create
            // 
            this.btn_cross_section_create.Location = new System.Drawing.Point(12, 54);
            this.btn_cross_section_create.Name = "btn_cross_section_create";
            this.btn_cross_section_create.Size = new System.Drawing.Size(211, 42);
            this.btn_cross_section_create.TabIndex = 3;
            this.btn_cross_section_create.Text = "Create Cross Section Drawings";
            this.btn_cross_section_create.UseVisualStyleBackColor = true;
            this.btn_cross_section_create.Click += new System.EventHandler(this.btn_cross_section_draw_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.toolStrip2);
            this.groupBox11.Controls.Add(this.label28);
            this.groupBox11.Controls.Add(this.label27);
            this.groupBox11.Controls.Add(this.cmb_HDP_cs_sel_chainage);
            this.groupBox11.Controls.Add(this.cmb_HDP_cs_sel_strings);
            this.groupBox11.Location = new System.Drawing.Point(229, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(233, 90);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_HDP_cs_First,
            this.tsmi_HDP_cs_backword,
            this.tsmi_HDP_cs_Forward,
            this.tsmi_HDP_cs_Last});
            this.toolStrip2.Location = new System.Drawing.Point(44, 62);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(104, 25);
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsmi_HDP_cs_First
            // 
            this.tsmi_HDP_cs_First.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmi_HDP_cs_First.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmi_HDP_cs_First.Name = "tsmi_HDP_cs_First";
            this.tsmi_HDP_cs_First.Size = new System.Drawing.Size(23, 22);
            this.tsmi_HDP_cs_First.Text = "Move First";
            this.tsmi_HDP_cs_First.Click += new System.EventHandler(this.tsmi_HWP_cs_First_Click);
            // 
            // tsmi_HDP_cs_backword
            // 
            this.tsmi_HDP_cs_backword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmi_HDP_cs_backword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmi_HDP_cs_backword.Name = "tsmi_HDP_cs_backword";
            this.tsmi_HDP_cs_backword.Size = new System.Drawing.Size(23, 22);
            this.tsmi_HDP_cs_backword.Text = "Move Previous";
            this.tsmi_HDP_cs_backword.Click += new System.EventHandler(this.tsmi_HWP_cs_First_Click);
            // 
            // tsmi_HDP_cs_Forward
            // 
            this.tsmi_HDP_cs_Forward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmi_HDP_cs_Forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmi_HDP_cs_Forward.Name = "tsmi_HDP_cs_Forward";
            this.tsmi_HDP_cs_Forward.Size = new System.Drawing.Size(23, 22);
            this.tsmi_HDP_cs_Forward.Text = "Move Next";
            this.tsmi_HDP_cs_Forward.Click += new System.EventHandler(this.tsmi_HWP_cs_First_Click);
            // 
            // tsmi_HDP_cs_Last
            // 
            this.tsmi_HDP_cs_Last.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmi_HDP_cs_Last.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmi_HDP_cs_Last.Name = "tsmi_HDP_cs_Last";
            this.tsmi_HDP_cs_Last.Size = new System.Drawing.Size(23, 22);
            this.tsmi_HDP_cs_Last.Text = "Move Last";
            this.tsmi_HDP_cs_Last.Click += new System.EventHandler(this.tsmi_HWP_cs_First_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(100, 13);
            this.label28.TabIndex = 1;
            this.label28.Text = "Select Chainage";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(114, 13);
            this.label27.TabIndex = 1;
            this.label27.Text = "Select String Label";
            // 
            // cmb_HDP_cs_sel_chainage
            // 
            this.cmb_HDP_cs_sel_chainage.FormattingEnabled = true;
            this.cmb_HDP_cs_sel_chainage.Location = new System.Drawing.Point(126, 37);
            this.cmb_HDP_cs_sel_chainage.Name = "cmb_HDP_cs_sel_chainage";
            this.cmb_HDP_cs_sel_chainage.Size = new System.Drawing.Size(86, 21);
            this.cmb_HDP_cs_sel_chainage.TabIndex = 0;
            this.cmb_HDP_cs_sel_chainage.SelectedIndexChanged += new System.EventHandler(this.cmb_HDP_cs_sel_chainage_SelectedIndexChanged);
            // 
            // cmb_HDP_cs_sel_strings
            // 
            this.cmb_HDP_cs_sel_strings.FormattingEnabled = true;
            this.cmb_HDP_cs_sel_strings.Location = new System.Drawing.Point(126, 10);
            this.cmb_HDP_cs_sel_strings.Name = "cmb_HDP_cs_sel_strings";
            this.cmb_HDP_cs_sel_strings.Size = new System.Drawing.Size(86, 21);
            this.cmb_HDP_cs_sel_strings.TabIndex = 0;
            this.cmb_HDP_cs_sel_strings.SelectedIndexChanged += new System.EventHandler(this.cmb_HDP_cs_sel_chainage_SelectedIndexChanged);
            // 
            // btn_cross_section_draw
            // 
            this.btn_cross_section_draw.Location = new System.Drawing.Point(12, 171);
            this.btn_cross_section_draw.Name = "btn_cross_section_draw";
            this.btn_cross_section_draw.Size = new System.Drawing.Size(233, 24);
            this.btn_cross_section_draw.TabIndex = 1;
            this.btn_cross_section_draw.Text = "Draw Cross Section";
            this.btn_cross_section_draw.UseVisualStyleBackColor = true;
            this.btn_cross_section_draw.Visible = false;
            // 
            // btn_cross_section_process
            // 
            this.btn_cross_section_process.Location = new System.Drawing.Point(12, 6);
            this.btn_cross_section_process.Name = "btn_cross_section_process";
            this.btn_cross_section_process.Size = new System.Drawing.Size(211, 42);
            this.btn_cross_section_process.TabIndex = 0;
            this.btn_cross_section_process.Text = "Process Cross Section Data";
            this.btn_cross_section_process.UseVisualStyleBackColor = true;
            this.btn_cross_section_process.Click += new System.EventHandler(this.btn_cross_section_create_Click);
            // 
            // label20
            // 
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(868, 24);
            this.label20.TabIndex = 2;
            this.label20.Text = "CROSS SECTION";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_site_step7
            // 
            this.tab_site_step7.Controls.Add(this.sc_volume);
            this.tab_site_step7.Controls.Add(this.label35);
            this.tab_site_step7.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step7.Name = "tab_site_step7";
            this.tab_site_step7.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step7.TabIndex = 3;
            this.tab_site_step7.Text = "STEP 7";
            this.tab_site_step7.UseVisualStyleBackColor = true;
            // 
            // sc_volume
            // 
            this.sc_volume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_volume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_volume.Location = new System.Drawing.Point(0, 24);
            this.sc_volume.Name = "sc_volume";
            // 
            // sc_volume.Panel1
            // 
            this.sc_volume.Panel1.Controls.Add(this.Volume_HDP_Data);
            this.sc_volume.Panel1.Controls.Add(this.panel14);
            // 
            // sc_volume.Panel2
            // 
            this.sc_volume.Panel2.Controls.Add(this.sc_volume_rep);
            this.sc_volume.Size = new System.Drawing.Size(868, 577);
            this.sc_volume.SplitterDistance = 387;
            this.sc_volume.TabIndex = 18;
            // 
            // Volume_HDP_Data
            // 
            this.Volume_HDP_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Volume_HDP_Data.Location = new System.Drawing.Point(0, 78);
            this.Volume_HDP_Data.Name = "Volume_HDP_Data";
            this.Volume_HDP_Data.Size = new System.Drawing.Size(385, 497);
            this.Volume_HDP_Data.TabIndex = 15;
            this.Volume_HDP_Data.Text = "";
            this.Volume_HDP_Data.WordWrap = false;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.pnl_INT_volume);
            this.panel14.Controls.Add(this.btn_HDP_volume_process);
            this.panel14.Controls.Add(this.btn_HDP_volume_masshaul);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(385, 78);
            this.panel14.TabIndex = 17;
            // 
            // pnl_INT_volume
            // 
            this.pnl_INT_volume.Controls.Add(this.label69);
            this.pnl_INT_volume.Controls.Add(this.cmb_volume_strings);
            this.pnl_INT_volume.Location = new System.Drawing.Point(12, 6);
            this.pnl_INT_volume.Name = "pnl_INT_volume";
            this.pnl_INT_volume.Size = new System.Drawing.Size(305, 31);
            this.pnl_INT_volume.TabIndex = 16;
            this.pnl_INT_volume.Visible = false;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(3, 9);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(114, 13);
            this.label69.TabIndex = 3;
            this.label69.Text = "Select String Label";
            // 
            // cmb_volume_strings
            // 
            this.cmb_volume_strings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_volume_strings.FormattingEnabled = true;
            this.cmb_volume_strings.Location = new System.Drawing.Point(138, 5);
            this.cmb_volume_strings.Name = "cmb_volume_strings";
            this.cmb_volume_strings.Size = new System.Drawing.Size(128, 21);
            this.cmb_volume_strings.TabIndex = 2;
            // 
            // btn_HDP_volume_process
            // 
            this.btn_HDP_volume_process.Location = new System.Drawing.Point(12, 43);
            this.btn_HDP_volume_process.Name = "btn_HDP_volume_process";
            this.btn_HDP_volume_process.Size = new System.Drawing.Size(176, 29);
            this.btn_HDP_volume_process.TabIndex = 14;
            this.btn_HDP_volume_process.Text = "Process Data";
            this.btn_HDP_volume_process.UseVisualStyleBackColor = true;
            this.btn_HDP_volume_process.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // btn_HDP_volume_masshaul
            // 
            this.btn_HDP_volume_masshaul.Location = new System.Drawing.Point(194, 43);
            this.btn_HDP_volume_masshaul.Name = "btn_HDP_volume_masshaul";
            this.btn_HDP_volume_masshaul.Size = new System.Drawing.Size(176, 29);
            this.btn_HDP_volume_masshaul.TabIndex = 13;
            this.btn_HDP_volume_masshaul.Text = "Draw MASSHAUL Diagram";
            this.btn_HDP_volume_masshaul.UseVisualStyleBackColor = true;
            this.btn_HDP_volume_masshaul.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // sc_volume_rep
            // 
            this.sc_volume_rep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_volume_rep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_volume_rep.Location = new System.Drawing.Point(0, 0);
            this.sc_volume_rep.Name = "sc_volume_rep";
            this.sc_volume_rep.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc_volume_rep.Panel1
            // 
            this.sc_volume_rep.Panel1.Controls.Add(this.vdsC_Volume);
            // 
            // sc_volume_rep.Panel2
            // 
            this.sc_volume_rep.Panel2.Controls.Add(this.Volume_HDP_Report);
            this.sc_volume_rep.Size = new System.Drawing.Size(477, 577);
            this.sc_volume_rep.SplitterDistance = 240;
            this.sc_volume_rep.TabIndex = 0;
            // 
            // Volume_HDP_Report
            // 
            this.Volume_HDP_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Volume_HDP_Report.Location = new System.Drawing.Point(0, 0);
            this.Volume_HDP_Report.Name = "Volume_HDP_Report";
            this.Volume_HDP_Report.Size = new System.Drawing.Size(475, 331);
            this.Volume_HDP_Report.TabIndex = 16;
            this.Volume_HDP_Report.Text = "";
            this.Volume_HDP_Report.WordWrap = false;
            // 
            // label35
            // 
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.Dock = System.Windows.Forms.DockStyle.Top;
            this.label35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(0, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(868, 24);
            this.label35.TabIndex = 17;
            this.label35.Text = "VOLUME";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_site_step8
            // 
            this.tab_site_step8.Controls.Add(this.tc_HDP_drawings);
            this.tab_site_step8.Controls.Add(this.label36);
            this.tab_site_step8.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step8.Name = "tab_site_step8";
            this.tab_site_step8.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step8.TabIndex = 4;
            this.tab_site_step8.Text = "STEP 8";
            this.tab_site_step8.UseVisualStyleBackColor = true;
            // 
            // tc_HDP_drawings
            // 
            this.tc_HDP_drawings.Controls.Add(this.tab_align_sch);
            this.tc_HDP_drawings.Controls.Add(this.tab_Plan);
            this.tc_HDP_drawings.Controls.Add(this.tab_Profile);
            this.tc_HDP_drawings.Controls.Add(this.tab_xsec);
            this.tc_HDP_drawings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_HDP_drawings.Location = new System.Drawing.Point(0, 24);
            this.tc_HDP_drawings.Name = "tc_HDP_drawings";
            this.tc_HDP_drawings.SelectedIndex = 0;
            this.tc_HDP_drawings.Size = new System.Drawing.Size(868, 577);
            this.tc_HDP_drawings.TabIndex = 10;
            // 
            // tab_align_sch
            // 
            this.tab_align_sch.Controls.Add(this.btn_process_diagram_HDP);
            this.tab_align_sch.Controls.Add(this.label42);
            this.tab_align_sch.Controls.Add(this.groupBox10);
            this.tab_align_sch.Controls.Add(this.groupBox9);
            this.tab_align_sch.Location = new System.Drawing.Point(4, 22);
            this.tab_align_sch.Name = "tab_align_sch";
            this.tab_align_sch.Padding = new System.Windows.Forms.Padding(3);
            this.tab_align_sch.Size = new System.Drawing.Size(860, 551);
            this.tab_align_sch.TabIndex = 3;
            this.tab_align_sch.Text = "Alignment Schematics";
            this.tab_align_sch.UseVisualStyleBackColor = true;
            // 
            // btn_process_diagram_HDP
            // 
            this.btn_process_diagram_HDP.Location = new System.Drawing.Point(375, 491);
            this.btn_process_diagram_HDP.Name = "btn_process_diagram_HDP";
            this.btn_process_diagram_HDP.Size = new System.Drawing.Size(165, 37);
            this.btn_process_diagram_HDP.TabIndex = 36;
            this.btn_process_diagram_HDP.Text = "Process Data";
            this.btn_process_diagram_HDP.UseVisualStyleBackColor = true;
            this.btn_process_diagram_HDP.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // label42
            // 
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(3, 3);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(854, 24);
            this.label42.TabIndex = 7;
            this.label42.Text = "ALIGNMENT SCHEMATICS";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.valign_Diagram_HDP);
            this.groupBox10.Location = new System.Drawing.Point(331, 300);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(275, 185);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Schematic Diagram for Vertical Alignment";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.halign_Diagram_HDP);
            this.groupBox9.Location = new System.Drawing.Point(331, 63);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(275, 229);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Schematic Diagram for Horizontal Alignment";
            // 
            // tab_Plan
            // 
            this.tab_Plan.Controls.Add(this.btn_HDP_plan_process);
            this.tab_Plan.Controls.Add(this.plan_Drawing_HDP);
            this.tab_Plan.Location = new System.Drawing.Point(4, 22);
            this.tab_Plan.Name = "tab_Plan";
            this.tab_Plan.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Plan.Size = new System.Drawing.Size(860, 551);
            this.tab_Plan.TabIndex = 0;
            this.tab_Plan.Text = "Plan";
            this.tab_Plan.UseVisualStyleBackColor = true;
            // 
            // btn_HDP_plan_process
            // 
            this.btn_HDP_plan_process.Location = new System.Drawing.Point(312, 439);
            this.btn_HDP_plan_process.Name = "btn_HDP_plan_process";
            this.btn_HDP_plan_process.Size = new System.Drawing.Size(214, 61);
            this.btn_HDP_plan_process.TabIndex = 2;
            this.btn_HDP_plan_process.Text = "Process Data";
            this.btn_HDP_plan_process.UseVisualStyleBackColor = true;
            this.btn_HDP_plan_process.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // tab_Profile
            // 
            this.tab_Profile.Controls.Add(this.pnl_INT_str_profile);
            this.tab_Profile.Controls.Add(this.btn_HDP_profile_process);
            this.tab_Profile.Controls.Add(this.profile_Drawing_HDP);
            this.tab_Profile.Location = new System.Drawing.Point(4, 22);
            this.tab_Profile.Name = "tab_Profile";
            this.tab_Profile.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Profile.Size = new System.Drawing.Size(860, 551);
            this.tab_Profile.TabIndex = 1;
            this.tab_Profile.Text = "Profile";
            this.tab_Profile.UseVisualStyleBackColor = true;
            // 
            // pnl_INT_str_profile
            // 
            this.pnl_INT_str_profile.Controls.Add(this.label134);
            this.pnl_INT_str_profile.Controls.Add(this.cmb_INT_profile_strings);
            this.pnl_INT_str_profile.Location = new System.Drawing.Point(6, 3);
            this.pnl_INT_str_profile.Name = "pnl_INT_str_profile";
            this.pnl_INT_str_profile.Size = new System.Drawing.Size(305, 31);
            this.pnl_INT_str_profile.TabIndex = 13;
            this.pnl_INT_str_profile.Visible = false;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(3, 8);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(114, 13);
            this.label134.TabIndex = 3;
            this.label134.Text = "Select String Label";
            // 
            // cmb_INT_profile_strings
            // 
            this.cmb_INT_profile_strings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_INT_profile_strings.FormattingEnabled = true;
            this.cmb_INT_profile_strings.Location = new System.Drawing.Point(138, 5);
            this.cmb_INT_profile_strings.Name = "cmb_INT_profile_strings";
            this.cmb_INT_profile_strings.Size = new System.Drawing.Size(128, 21);
            this.cmb_INT_profile_strings.TabIndex = 2;
            // 
            // btn_HDP_profile_process
            // 
            this.btn_HDP_profile_process.Location = new System.Drawing.Point(302, 537);
            this.btn_HDP_profile_process.Name = "btn_HDP_profile_process";
            this.btn_HDP_profile_process.Size = new System.Drawing.Size(165, 37);
            this.btn_HDP_profile_process.TabIndex = 2;
            this.btn_HDP_profile_process.Text = "Process Data";
            this.btn_HDP_profile_process.UseVisualStyleBackColor = true;
            this.btn_HDP_profile_process.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // tab_xsec
            // 
            this.tab_xsec.Controls.Add(this.pnl_INT_str_cs);
            this.tab_xsec.Controls.Add(this.btn_HDP_cross_process);
            this.tab_xsec.Controls.Add(this.cross_Section_Drawing_HDP);
            this.tab_xsec.Location = new System.Drawing.Point(4, 22);
            this.tab_xsec.Name = "tab_xsec";
            this.tab_xsec.Size = new System.Drawing.Size(860, 551);
            this.tab_xsec.TabIndex = 2;
            this.tab_xsec.Text = "Cross Section";
            this.tab_xsec.UseVisualStyleBackColor = true;
            // 
            // pnl_INT_str_cs
            // 
            this.pnl_INT_str_cs.Controls.Add(this.label135);
            this.pnl_INT_str_cs.Controls.Add(this.cmb_INT_cross_strings);
            this.pnl_INT_str_cs.Location = new System.Drawing.Point(46, 3);
            this.pnl_INT_str_cs.Name = "pnl_INT_str_cs";
            this.pnl_INT_str_cs.Size = new System.Drawing.Size(305, 31);
            this.pnl_INT_str_cs.TabIndex = 14;
            this.pnl_INT_str_cs.Visible = false;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(3, 8);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(114, 13);
            this.label135.TabIndex = 3;
            this.label135.Text = "Select String Label";
            // 
            // cmb_INT_cross_strings
            // 
            this.cmb_INT_cross_strings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_INT_cross_strings.FormattingEnabled = true;
            this.cmb_INT_cross_strings.Location = new System.Drawing.Point(138, 5);
            this.cmb_INT_cross_strings.Name = "cmb_INT_cross_strings";
            this.cmb_INT_cross_strings.Size = new System.Drawing.Size(128, 21);
            this.cmb_INT_cross_strings.TabIndex = 2;
            // 
            // btn_HDP_cross_process
            // 
            this.btn_HDP_cross_process.Location = new System.Drawing.Point(362, 453);
            this.btn_HDP_cross_process.Name = "btn_HDP_cross_process";
            this.btn_HDP_cross_process.Size = new System.Drawing.Size(165, 37);
            this.btn_HDP_cross_process.TabIndex = 2;
            this.btn_HDP_cross_process.Text = "Process Data";
            this.btn_HDP_cross_process.UseVisualStyleBackColor = true;
            this.btn_HDP_cross_process.Click += new System.EventHandler(this.btn_HDP_process_Click);
            // 
            // label36
            // 
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(868, 24);
            this.label36.TabIndex = 11;
            this.label36.Text = "DRAWINGS";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_site_step9
            // 
            this.tab_site_step9.Controls.Add(this.btn_HDP_survey_view);
            this.tab_site_step9.Controls.Add(this.btn_HDP_view_drawings);
            this.tab_site_step9.Controls.Add(this.btn_HDP_view_report_file);
            this.tab_site_step9.Controls.Add(this.btn_HDP_open_project_file);
            this.tab_site_step9.Controls.Add(this.btn_HDP_create_project_file);
            this.tab_site_step9.Controls.Add(this.label37);
            this.tab_site_step9.Controls.Add(this.rtb_project_data);
            this.tab_site_step9.Controls.Add(this.btn_HDP_process_project_file);
            this.tab_site_step9.Controls.Add(this.txt_HDP_file);
            this.tab_site_step9.Location = new System.Drawing.Point(4, 22);
            this.tab_site_step9.Name = "tab_site_step9";
            this.tab_site_step9.Size = new System.Drawing.Size(868, 601);
            this.tab_site_step9.TabIndex = 5;
            this.tab_site_step9.Text = "STEP 9";
            this.tab_site_step9.UseVisualStyleBackColor = true;
            // 
            // btn_HDP_survey_view
            // 
            this.btn_HDP_survey_view.Location = new System.Drawing.Point(423, 27);
            this.btn_HDP_survey_view.Name = "btn_HDP_survey_view";
            this.btn_HDP_survey_view.Size = new System.Drawing.Size(145, 34);
            this.btn_HDP_survey_view.TabIndex = 29;
            this.btn_HDP_survey_view.Text = "View Survey Data File";
            this.btn_HDP_survey_view.UseVisualStyleBackColor = true;
            this.btn_HDP_survey_view.Click += new System.EventHandler(this.btn_HDP_survey_view_Click);
            // 
            // btn_HDP_view_drawings
            // 
            this.btn_HDP_view_drawings.Location = new System.Drawing.Point(706, 27);
            this.btn_HDP_view_drawings.Name = "btn_HDP_view_drawings";
            this.btn_HDP_view_drawings.Size = new System.Drawing.Size(126, 34);
            this.btn_HDP_view_drawings.TabIndex = 26;
            this.btn_HDP_view_drawings.Text = "View Drawings";
            this.btn_HDP_view_drawings.UseVisualStyleBackColor = true;
            this.btn_HDP_view_drawings.Click += new System.EventHandler(this.btn_HDP_open_project_file_Click);
            // 
            // btn_HDP_view_report_file
            // 
            this.btn_HDP_view_report_file.Location = new System.Drawing.Point(574, 27);
            this.btn_HDP_view_report_file.Name = "btn_HDP_view_report_file";
            this.btn_HDP_view_report_file.Size = new System.Drawing.Size(126, 34);
            this.btn_HDP_view_report_file.TabIndex = 25;
            this.btn_HDP_view_report_file.Text = "View Report Files";
            this.btn_HDP_view_report_file.UseVisualStyleBackColor = true;
            this.btn_HDP_view_report_file.Click += new System.EventHandler(this.btn_HDP_open_project_file_Click);
            // 
            // btn_HDP_open_project_file
            // 
            this.btn_HDP_open_project_file.Location = new System.Drawing.Point(272, 27);
            this.btn_HDP_open_project_file.Name = "btn_HDP_open_project_file";
            this.btn_HDP_open_project_file.Size = new System.Drawing.Size(145, 34);
            this.btn_HDP_open_project_file.TabIndex = 22;
            this.btn_HDP_open_project_file.Text = "View Project Data File";
            this.btn_HDP_open_project_file.UseVisualStyleBackColor = true;
            this.btn_HDP_open_project_file.Click += new System.EventHandler(this.btn_HDP_open_project_file_Click);
            // 
            // btn_HDP_create_project_file
            // 
            this.btn_HDP_create_project_file.Location = new System.Drawing.Point(52, 27);
            this.btn_HDP_create_project_file.Name = "btn_HDP_create_project_file";
            this.btn_HDP_create_project_file.Size = new System.Drawing.Size(214, 34);
            this.btn_HDP_create_project_file.TabIndex = 23;
            this.btn_HDP_create_project_file.Text = "Create / Update Project Data File";
            this.btn_HDP_create_project_file.UseVisualStyleBackColor = true;
            this.btn_HDP_create_project_file.Click += new System.EventHandler(this.btn_HDP_create_project_file_Click);
            // 
            // label37
            // 
            this.label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(868, 24);
            this.label37.TabIndex = 28;
            this.label37.Text = "PROCESS DATA";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtb_project_data
            // 
            this.rtb_project_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_project_data.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_project_data.Location = new System.Drawing.Point(0, 67);
            this.rtb_project_data.Name = "rtb_project_data";
            this.rtb_project_data.Size = new System.Drawing.Size(868, 534);
            this.rtb_project_data.TabIndex = 24;
            this.rtb_project_data.Text = "";
            // 
            // btn_HDP_process_project_file
            // 
            this.btn_HDP_process_project_file.Location = new System.Drawing.Point(329, 90);
            this.btn_HDP_process_project_file.Name = "btn_HDP_process_project_file";
            this.btn_HDP_process_project_file.Size = new System.Drawing.Size(122, 23);
            this.btn_HDP_process_project_file.TabIndex = 21;
            this.btn_HDP_process_project_file.Text = "Process Project Input Data File";
            this.btn_HDP_process_project_file.UseVisualStyleBackColor = true;
            this.btn_HDP_process_project_file.Visible = false;
            // 
            // txt_HDP_file
            // 
            this.txt_HDP_file.Location = new System.Drawing.Point(475, 92);
            this.txt_HDP_file.Name = "txt_HDP_file";
            this.txt_HDP_file.Size = new System.Drawing.Size(48, 21);
            this.txt_HDP_file.TabIndex = 27;
            this.txt_HDP_file.Visible = false;
            // 
            // tab_disnet
            // 
            this.tab_disnet.Controls.Add(this.tc_disnet);
            this.tab_disnet.Location = new System.Drawing.Point(4, 22);
            this.tab_disnet.Name = "tab_disnet";
            this.tab_disnet.Padding = new System.Windows.Forms.Padding(3);
            this.tab_disnet.Size = new System.Drawing.Size(882, 633);
            this.tab_disnet.TabIndex = 6;
            this.tab_disnet.Text = "Water Distribution Network";
            this.tab_disnet.UseVisualStyleBackColor = true;
            // 
            // tc_disnet
            // 
            this.tc_disnet.Controls.Add(this.tab_create_loops);
            this.tc_disnet.Controls.Add(this.tab_pipe_network);
            this.tc_disnet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_disnet.Location = new System.Drawing.Point(3, 3);
            this.tc_disnet.Name = "tc_disnet";
            this.tc_disnet.SelectedIndex = 0;
            this.tc_disnet.Size = new System.Drawing.Size(876, 627);
            this.tc_disnet.TabIndex = 6;
            // 
            // tab_create_loops
            // 
            this.tab_create_loops.Controls.Add(this.btn_disnet_refresh_loops);
            this.tab_create_loops.Controls.Add(this.groupBox12);
            this.tab_create_loops.Controls.Add(this.btn_disnet_nodal_ground);
            this.tab_create_loops.Controls.Add(this.lst_disnet_strings);
            this.tab_create_loops.Location = new System.Drawing.Point(4, 22);
            this.tab_create_loops.Name = "tab_create_loops";
            this.tab_create_loops.Padding = new System.Windows.Forms.Padding(3);
            this.tab_create_loops.Size = new System.Drawing.Size(868, 601);
            this.tab_create_loops.TabIndex = 0;
            this.tab_create_loops.Text = "Create Loops";
            this.tab_create_loops.UseVisualStyleBackColor = true;
            // 
            // btn_disnet_refresh_loops
            // 
            this.btn_disnet_refresh_loops.Location = new System.Drawing.Point(43, 333);
            this.btn_disnet_refresh_loops.Name = "btn_disnet_refresh_loops";
            this.btn_disnet_refresh_loops.Size = new System.Drawing.Size(162, 29);
            this.btn_disnet_refresh_loops.TabIndex = 3;
            this.btn_disnet_refresh_loops.Text = "Refresh Data";
            this.btn_disnet_refresh_loops.UseVisualStyleBackColor = true;
            this.btn_disnet_refresh_loops.Click += new System.EventHandler(this.btn_disnet_refresh_loops_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cmb_disnet_pipenet_model);
            this.groupBox12.Controls.Add(this.label30);
            this.groupBox12.Controls.Add(this.btn_disnet_create_loop);
            this.groupBox12.Location = new System.Drawing.Point(6, 17);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(241, 83);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Pipe Network";
            // 
            // cmb_disnet_pipenet_model
            // 
            this.cmb_disnet_pipenet_model.FormattingEnabled = true;
            this.cmb_disnet_pipenet_model.Items.AddRange(new object[] {
            "PIPENET"});
            this.cmb_disnet_pipenet_model.Location = new System.Drawing.Point(114, 19);
            this.cmb_disnet_pipenet_model.Name = "cmb_disnet_pipenet_model";
            this.cmb_disnet_pipenet_model.Size = new System.Drawing.Size(100, 21);
            this.cmb_disnet_pipenet_model.TabIndex = 2;
            this.cmb_disnet_pipenet_model.Text = "PIPENET";
            this.cmb_disnet_pipenet_model.SelectedIndexChanged += new System.EventHandler(this.cmb_disnet_pipenet_model_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 22);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "Model Name";
            // 
            // btn_disnet_create_loop
            // 
            this.btn_disnet_create_loop.Location = new System.Drawing.Point(114, 46);
            this.btn_disnet_create_loop.Name = "btn_disnet_create_loop";
            this.btn_disnet_create_loop.Size = new System.Drawing.Size(100, 24);
            this.btn_disnet_create_loop.TabIndex = 0;
            this.btn_disnet_create_loop.Text = "Create Loops";
            this.btn_disnet_create_loop.UseVisualStyleBackColor = true;
            this.btn_disnet_create_loop.Click += new System.EventHandler(this.btn_disnet_create_loop_Click);
            // 
            // btn_disnet_nodal_ground
            // 
            this.btn_disnet_nodal_ground.Location = new System.Drawing.Point(43, 272);
            this.btn_disnet_nodal_ground.Name = "btn_disnet_nodal_ground";
            this.btn_disnet_nodal_ground.Size = new System.Drawing.Size(162, 29);
            this.btn_disnet_nodal_ground.TabIndex = 2;
            this.btn_disnet_nodal_ground.Text = "Nodal Ground Elevations";
            this.btn_disnet_nodal_ground.UseVisualStyleBackColor = true;
            this.btn_disnet_nodal_ground.Click += new System.EventHandler(this.btn_disnet_nodal_ground_Click);
            // 
            // lst_disnet_strings
            // 
            this.lst_disnet_strings.FormattingEnabled = true;
            this.lst_disnet_strings.Location = new System.Drawing.Point(6, 106);
            this.lst_disnet_strings.Name = "lst_disnet_strings";
            this.lst_disnet_strings.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lst_disnet_strings.Size = new System.Drawing.Size(241, 160);
            this.lst_disnet_strings.TabIndex = 1;
            this.lst_disnet_strings.SelectedIndexChanged += new System.EventHandler(this.lst_disnet_strings_SelectedIndexChanged);
            // 
            // tab_pipe_network
            // 
            this.tab_pipe_network.Controls.Add(this.uC_DisNet1);
            this.tab_pipe_network.Location = new System.Drawing.Point(4, 22);
            this.tab_pipe_network.Name = "tab_pipe_network";
            this.tab_pipe_network.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pipe_network.Size = new System.Drawing.Size(868, 601);
            this.tab_pipe_network.TabIndex = 1;
            this.tab_pipe_network.Text = "Pipe Network";
            this.tab_pipe_network.UseVisualStyleBackColor = true;
            // 
            // tab_hydrology
            // 
            this.tab_hydrology.Controls.Add(this.uC_StreamHydrology1);
            this.tab_hydrology.Location = new System.Drawing.Point(4, 22);
            this.tab_hydrology.Name = "tab_hydrology";
            this.tab_hydrology.Padding = new System.Windows.Forms.Padding(3);
            this.tab_hydrology.Size = new System.Drawing.Size(882, 633);
            this.tab_hydrology.TabIndex = 7;
            this.tab_hydrology.Text = "Stream Hydrology";
            this.tab_hydrology.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.btn_crdn_off);
            this.panel8.Controls.Add(this.btn_dtls_off);
            this.panel8.Controls.Add(this.btn_crdn_on);
            this.panel8.Controls.Add(this.btn_dtls_on);
            this.panel8.Controls.Add(this.btn_chn_off);
            this.panel8.Controls.Add(this.btn_chn_on);
            this.panel8.Controls.Add(this.btn_draw_model_strings);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 590);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(138, 69);
            this.panel8.TabIndex = 20;
            // 
            // btn_crdn_off
            // 
            this.btn_crdn_off.Location = new System.Drawing.Point(340, 34);
            this.btn_crdn_off.Name = "btn_crdn_off";
            this.btn_crdn_off.Size = new System.Drawing.Size(112, 25);
            this.btn_crdn_off.TabIndex = 21;
            this.btn_crdn_off.Text = "Coordinate OFF";
            this.btn_crdn_off.UseVisualStyleBackColor = true;
            this.btn_crdn_off.Click += new System.EventHandler(this.btn_crdn_on_Click);
            // 
            // btn_dtls_off
            // 
            this.btn_dtls_off.Location = new System.Drawing.Point(224, 34);
            this.btn_dtls_off.Name = "btn_dtls_off";
            this.btn_dtls_off.Size = new System.Drawing.Size(101, 25);
            this.btn_dtls_off.TabIndex = 21;
            this.btn_dtls_off.Text = "Details OFF";
            this.btn_dtls_off.UseVisualStyleBackColor = true;
            this.btn_dtls_off.Click += new System.EventHandler(this.btn_dtls_on_Click);
            // 
            // btn_crdn_on
            // 
            this.btn_crdn_on.Location = new System.Drawing.Point(340, 4);
            this.btn_crdn_on.Name = "btn_crdn_on";
            this.btn_crdn_on.Size = new System.Drawing.Size(112, 25);
            this.btn_crdn_on.TabIndex = 19;
            this.btn_crdn_on.Text = "Coordinate ON";
            this.btn_crdn_on.UseVisualStyleBackColor = true;
            this.btn_crdn_on.Click += new System.EventHandler(this.btn_crdn_on_Click);
            // 
            // btn_dtls_on
            // 
            this.btn_dtls_on.Location = new System.Drawing.Point(224, 4);
            this.btn_dtls_on.Name = "btn_dtls_on";
            this.btn_dtls_on.Size = new System.Drawing.Size(101, 25);
            this.btn_dtls_on.TabIndex = 22;
            this.btn_dtls_on.Text = "Details ON";
            this.btn_dtls_on.UseVisualStyleBackColor = true;
            this.btn_dtls_on.Click += new System.EventHandler(this.btn_dtls_on_Click);
            // 
            // btn_chn_off
            // 
            this.btn_chn_off.Location = new System.Drawing.Point(112, 34);
            this.btn_chn_off.Name = "btn_chn_off";
            this.btn_chn_off.Size = new System.Drawing.Size(101, 25);
            this.btn_chn_off.TabIndex = 19;
            this.btn_chn_off.Text = "Chainage OFF";
            this.btn_chn_off.UseVisualStyleBackColor = true;
            this.btn_chn_off.Click += new System.EventHandler(this.btn_chn_on_Click);
            // 
            // btn_chn_on
            // 
            this.btn_chn_on.Location = new System.Drawing.Point(112, 4);
            this.btn_chn_on.Name = "btn_chn_on";
            this.btn_chn_on.Size = new System.Drawing.Size(101, 25);
            this.btn_chn_on.TabIndex = 20;
            this.btn_chn_on.Text = "Chainage ON";
            this.btn_chn_on.UseVisualStyleBackColor = true;
            this.btn_chn_on.Click += new System.EventHandler(this.btn_chn_on_Click);
            // 
            // btn_draw_model_strings
            // 
            this.btn_draw_model_strings.Location = new System.Drawing.Point(17, 7);
            this.btn_draw_model_strings.Name = "btn_draw_model_strings";
            this.btn_draw_model_strings.Size = new System.Drawing.Size(89, 50);
            this.btn_draw_model_strings.TabIndex = 18;
            this.btn_draw_model_strings.Text = "Draw Model Strings";
            this.btn_draw_model_strings.UseVisualStyleBackColor = true;
            this.btn_draw_model_strings.Click += new System.EventHandler(this.btn_draw_model_strings_Click);
            // 
            // utM_Conversion1
            // 
            this.utM_Conversion1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.utM_Conversion1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utM_Conversion1.HEADS_Data = null;
            this.utM_Conversion1.Location = new System.Drawing.Point(603, 102);
            this.utM_Conversion1.Name = "utM_Conversion1";
            this.utM_Conversion1.Size = new System.Drawing.Size(118, 41);
            this.utM_Conversion1.SurveyFile = null;
            this.utM_Conversion1.SurveyFileFormat = HEADS_Project_Mode.DataStructure.eSurveyFileFormat.TEXT_Format;
            this.utM_Conversion1.TabIndex = 15;
            this.utM_Conversion1.TM1_XC = 96655.9973D;
            this.utM_Conversion1.TM1_YC = 97618.573D;
            this.utM_Conversion1.TM2_XC = 98376.866D;
            this.utM_Conversion1.TM2_YC = 99354.209D;
            this.utM_Conversion1.UTM1_XC = 797040.2D;
            this.utM_Conversion1.UTM1_YC = 348829.2D;
            this.utM_Conversion1.UTM2_XC = 798753.72D;
            this.utM_Conversion1.UTM2_YC = 350568.68D;
            this.utM_Conversion1.Visible = false;
            // 
            // uC_Stockpile
            // 
            this.uC_Stockpile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Stockpile.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Stockpile.Is_Desiltaion_Quantity = true;
            this.uC_Stockpile.Is_Discharge_Quantity = false;
            this.uC_Stockpile.Location = new System.Drawing.Point(0, 0);
            this.uC_Stockpile.Name = "uC_Stockpile";
            this.uC_Stockpile.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.Irrigation_Discharge;
            this.uC_Stockpile.Size = new System.Drawing.Size(874, 572);
            this.uC_Stockpile.TabIndex = 1;
            this.uC_Stockpile.Title = "Stockpile Quantity";
            this.uC_Stockpile.Proceed_Click += new System.EventHandler(this.uC_QuantityMeasurement1_Proceed_Click);
            // 
            // uC_Discharge
            // 
            this.uC_Discharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Discharge.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Discharge.Is_Desiltaion_Quantity = false;
            this.uC_Discharge.Is_Discharge_Quantity = true;
            this.uC_Discharge.Location = new System.Drawing.Point(0, 0);
            this.uC_Discharge.Name = "uC_Discharge";
            this.uC_Discharge.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.Irrigation_Discharge;
            this.uC_Discharge.Size = new System.Drawing.Size(874, 572);
            this.uC_Discharge.TabIndex = 1;
            this.uC_Discharge.Title = "Reservoir Discharge Quantity";
            this.uC_Discharge.Proceed_Click += new System.EventHandler(this.uC_QuantityMeasurement2_Proceed_Click);
            // 
            // uC_LandRecord1
            // 
            this.uC_LandRecord1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_LandRecord1.Entry_Database = "";
            this.uC_LandRecord1.Format_1 = null;
            this.uC_LandRecord1.Format_2 = null;
            this.uC_LandRecord1.Format_3 = null;
            this.uC_LandRecord1.Format_4 = null;
            this.uC_LandRecord1.Full_Data = null;
            this.uC_LandRecord1.IsRetrieve = false;
            this.uC_LandRecord1.Last_Survey = null;
            this.uC_LandRecord1.Location = new System.Drawing.Point(0, 0);
            this.uC_LandRecord1.Name = "uC_LandRecord1";
            this.uC_LandRecord1.Retrive_Database = "";
            this.uC_LandRecord1.Selected_Survey_Nos = null;
            this.uC_LandRecord1.Size = new System.Drawing.Size(411, 631);
            this.uC_LandRecord1.TabIndex = 0;
            // 
            // vdsC_DOC_Land
            // 
            this.vdsC_DOC_Land.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vdsC_DOC_Land.ConfigParam = null;
            this.vdsC_DOC_Land.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_DOC_Land.Drawing_File = null;
            this.vdsC_DOC_Land.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_DOC_Land.Icons_Visible_3D = true;
            this.vdsC_DOC_Land.Location = new System.Drawing.Point(0, 83);
            this.vdsC_DOC_Land.Name = "vdsC_DOC_Land";
            this.vdsC_DOC_Land.Show_DrawString = false;
            this.vdsC_DOC_Land.Size = new System.Drawing.Size(463, 548);
            this.vdsC_DOC_Land.TabIndex = 4;
            this.vdsC_DOC_Land.View_ON = false;
            // 
            // design_Profile_Optimization1
            // 
            this.design_Profile_Optimization1.End_Chainage = 308750D;
            this.design_Profile_Optimization1.Fly_Over_End_Length = 0D;
            this.design_Profile_Optimization1.Fly_Over_End_VCL = 0D;
            this.design_Profile_Optimization1.Fly_Over_Height = 0D;
            this.design_Profile_Optimization1.Fly_Over_Mid_VCL = 0D;
            this.design_Profile_Optimization1.Fly_Over_Min_Chainage_Distance = 0D;
            this.design_Profile_Optimization1.Fly_Over_Start_Length = 0D;
            this.design_Profile_Optimization1.Fly_Over_Start_VCL = 0D;
            this.design_Profile_Optimization1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.design_Profile_Optimization1.Location = new System.Drawing.Point(3, 46);
            this.design_Profile_Optimization1.Name = "design_Profile_Optimization1";
            this.design_Profile_Optimization1.Show_FlyOver = true;
            this.design_Profile_Optimization1.Size = new System.Drawing.Size(407, 477);
            this.design_Profile_Optimization1.Start_Chainage = 304725D;
            this.design_Profile_Optimization1.TabIndex = 0;
            // 
            // vdsC_HDP_Valign
            // 
            this.vdsC_HDP_Valign.ConfigParam = null;
            this.vdsC_HDP_Valign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_HDP_Valign.Drawing_File = null;
            this.vdsC_HDP_Valign.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_HDP_Valign.Icons_Visible_3D = true;
            this.vdsC_HDP_Valign.Location = new System.Drawing.Point(0, 0);
            this.vdsC_HDP_Valign.Name = "vdsC_HDP_Valign";
            this.vdsC_HDP_Valign.Show_DrawString = false;
            this.vdsC_HDP_Valign.Size = new System.Drawing.Size(489, 569);
            this.vdsC_HDP_Valign.TabIndex = 0;
            this.vdsC_HDP_Valign.View_ON = false;
            // 
            // uC_Dyke_CrossSection1
            // 
            this.uC_Dyke_CrossSection1.Chainage_Interval = 0D;
            this.uC_Dyke_CrossSection1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uC_Dyke_CrossSection1.End_Chainage = 0D;
            this.uC_Dyke_CrossSection1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Dyke_CrossSection1.Location = new System.Drawing.Point(379, 0);
            this.uC_Dyke_CrossSection1.Name = "uC_Dyke_CrossSection1";
            this.uC_Dyke_CrossSection1.Size = new System.Drawing.Size(329, 74);
            this.uC_Dyke_CrossSection1.Start_Chainage = 0D;
            this.uC_Dyke_CrossSection1.TabIndex = 1;
            // 
            // uC_Canal_CrossSection1
            // 
            this.uC_Canal_CrossSection1.Chainage_Interval = 0D;
            this.uC_Canal_CrossSection1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uC_Canal_CrossSection1.End_Chainage = 0D;
            this.uC_Canal_CrossSection1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Canal_CrossSection1.Location = new System.Drawing.Point(0, 0);
            this.uC_Canal_CrossSection1.Name = "uC_Canal_CrossSection1";
            this.uC_Canal_CrossSection1.Size = new System.Drawing.Size(379, 74);
            this.uC_Canal_CrossSection1.Start_Chainage = 0D;
            this.uC_Canal_CrossSection1.TabIndex = 0;
            // 
            // vdsC_DOC_river_canal
            // 
            this.vdsC_DOC_river_canal.ConfigParam = null;
            this.vdsC_DOC_river_canal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_DOC_river_canal.Drawing_File = null;
            this.vdsC_DOC_river_canal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_DOC_river_canal.Icons_Visible_3D = true;
            this.vdsC_DOC_river_canal.Location = new System.Drawing.Point(0, 0);
            this.vdsC_DOC_river_canal.Name = "vdsC_DOC_river_canal";
            this.vdsC_DOC_river_canal.Show_DrawString = false;
            this.vdsC_DOC_river_canal.Size = new System.Drawing.Size(852, 277);
            this.vdsC_DOC_river_canal.TabIndex = 36;
            this.vdsC_DOC_river_canal.View_ON = false;
            // 
            // interface_HDP
            // 
            this.interface_HDP.Data = ((System.Collections.Generic.List<string>)(resources.GetObject("interface_HDP.Data")));
            this.interface_HDP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.interface_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interface_HDP.InterfaceOption = HEADS_Project_Mode.DataStructure.eInterfaceOption.Inner_Interface;
            this.interface_HDP.Lines = new string[] {
        "HEADS",
        "600,INTERFACE,XS01",
        "601,MODEL=DESIGN,STRING=M001",
        "602,MODEL=DESIGN,STRING=I001",
        "610 LHS CUT",
        "611 1.0 0.0 0",
        "612 2.0 0.65 2 4 2 1",
        "612 0.2 0.0 3 5 2 0",
        "999",
        "620 LHS FIL",
        "621 0.001 0.0 0",
        "622 2.0 -0.5 2 4 2 1",
        "622 0.2 0.0 3 5 2 0",
        "999",
        "630 RHS CUT",
        "631 1.0 0.0 0",
        "632 2.0 0.65 2 4 2 1",
        "632 0.2 0.0 3 5 2 0",
        "999",
        "640 RHS FIL",
        "641 0.001 0.0 0",
        "642 2.0 -0.5 2 4 2 1",
        "642 0.2 0.0 3 5 2 0",
        "999",
        "FINISH"};
            this.interface_HDP.Location = new System.Drawing.Point(3, 80);
            this.interface_HDP.Name = "interface_HDP";
            this.interface_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.Tunnel;
            this.interface_HDP.Size = new System.Drawing.Size(846, 442);
            this.interface_HDP.TabIndex = 4;
            // 
            // interfaceNote2
            // 
            this.interfaceNote2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.interfaceNote2.Location = new System.Drawing.Point(3, 3);
            this.interfaceNote2.Name = "interfaceNote2";
            this.interfaceNote2.Size = new System.Drawing.Size(846, 519);
            this.interfaceNote2.TabIndex = 2;
            // 
            // vdsC_HDP_CrossSect
            // 
            this.vdsC_HDP_CrossSect.ConfigParam = null;
            this.vdsC_HDP_CrossSect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_HDP_CrossSect.Drawing_File = null;
            this.vdsC_HDP_CrossSect.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_HDP_CrossSect.Icons_Visible_3D = true;
            this.vdsC_HDP_CrossSect.Location = new System.Drawing.Point(0, 0);
            this.vdsC_HDP_CrossSect.Name = "vdsC_HDP_CrossSect";
            this.vdsC_HDP_CrossSect.Show_DrawString = false;
            this.vdsC_HDP_CrossSect.Size = new System.Drawing.Size(850, 433);
            this.vdsC_HDP_CrossSect.TabIndex = 0;
            this.vdsC_HDP_CrossSect.View_ON = false;
            // 
            // vdsC_Volume
            // 
            this.vdsC_Volume.ConfigParam = null;
            this.vdsC_Volume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_Volume.Drawing_File = null;
            this.vdsC_Volume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_Volume.Icons_Visible_3D = true;
            this.vdsC_Volume.Location = new System.Drawing.Point(0, 0);
            this.vdsC_Volume.Name = "vdsC_Volume";
            this.vdsC_Volume.Show_DrawString = false;
            this.vdsC_Volume.Size = new System.Drawing.Size(475, 238);
            this.vdsC_Volume.TabIndex = 1;
            this.vdsC_Volume.View_ON = false;
            // 
            // valign_Diagram_HDP
            // 
            this.valign_Diagram_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valign_Diagram_HDP.HEADS_Data = null;
            this.valign_Diagram_HDP.Location = new System.Drawing.Point(2, 12);
            this.valign_Diagram_HDP.Name = "valign_Diagram_HDP";
            this.valign_Diagram_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.MultipleSection;
            this.valign_Diagram_HDP.Size = new System.Drawing.Size(276, 172);
            this.valign_Diagram_HDP.TabIndex = 2;
            // 
            // halign_Diagram_HDP
            // 
            this.halign_Diagram_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.halign_Diagram_HDP.HEADS_Data = null;
            this.halign_Diagram_HDP.Location = new System.Drawing.Point(6, 9);
            this.halign_Diagram_HDP.Name = "halign_Diagram_HDP";
            this.halign_Diagram_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.MultipleSection;
            this.halign_Diagram_HDP.Size = new System.Drawing.Size(271, 222);
            this.halign_Diagram_HDP.TabIndex = 1;
            // 
            // plan_Drawing_HDP
            // 
            this.plan_Drawing_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plan_Drawing_HDP.HEADS_Data = null;
            this.plan_Drawing_HDP.Location = new System.Drawing.Point(152, 43);
            this.plan_Drawing_HDP.Name = "plan_Drawing_HDP";
            this.plan_Drawing_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.MultipleSection;
            this.plan_Drawing_HDP.Show_Default_Groups = false;
            this.plan_Drawing_HDP.Show_Provide_Service_Road = false;
            this.plan_Drawing_HDP.Size = new System.Drawing.Size(611, 362);
            this.plan_Drawing_HDP.TabIndex = 0;
            // 
            // profile_Drawing_HDP
            // 
            this.profile_Drawing_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profile_Drawing_HDP.HEADS_Data = null;
            this.profile_Drawing_HDP.Location = new System.Drawing.Point(0, 24);
            this.profile_Drawing_HDP.Name = "profile_Drawing_HDP";
            this.profile_Drawing_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.MultipleSection;
            this.profile_Drawing_HDP.Show_Default_Groups = false;
            this.profile_Drawing_HDP.Size = new System.Drawing.Size(819, 507);
            this.profile_Drawing_HDP.TabIndex = 0;
            // 
            // cross_Section_Drawing_HDP
            // 
            this.cross_Section_Drawing_HDP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cross_Section_Drawing_HDP.HEADS_Data = null;
            this.cross_Section_Drawing_HDP.Location = new System.Drawing.Point(46, 34);
            this.cross_Section_Drawing_HDP.Name = "cross_Section_Drawing_HDP";
            this.cross_Section_Drawing_HDP.ProjectType = HEADS_Project_Mode.DataStructure.eTypeOfProject.MultipleSection;
            this.cross_Section_Drawing_HDP.Show_Default_Groups = false;
            this.cross_Section_Drawing_HDP.Size = new System.Drawing.Size(813, 413);
            this.cross_Section_Drawing_HDP.TabIndex = 0;
            // 
            // uC_DisNet1
            // 
            this.uC_DisNet1.doc = null;
            this.uC_DisNet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_DisNet1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_DisNet1.lbStringName = null;
            this.uC_DisNet1.Location = new System.Drawing.Point(3, 3);
            this.uC_DisNet1.Name = "uC_DisNet1";
            this.uC_DisNet1.Size = new System.Drawing.Size(862, 595);
            this.uC_DisNet1.strSeleModel = null;
            this.uC_DisNet1.TabIndex = 0;
            this.uC_DisNet1.Working_Folder = null;
            this.uC_DisNet1.On_Proceed += new System.EventHandler(this.uC_DisNet1_On_Proceed);
            // 
            // uC_StreamHydrology1
            // 
            this.uC_StreamHydrology1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_StreamHydrology1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_StreamHydrology1.Location = new System.Drawing.Point(3, 3);
            this.uC_StreamHydrology1.Name = "uC_StreamHydrology1";
            this.uC_StreamHydrology1.Size = new System.Drawing.Size(876, 627);
            this.uC_StreamHydrology1.TabIndex = 0;
            this.uC_StreamHydrology1.Working_Folder = null;
            // 
            // vdsC_Main
            // 
            this.vdsC_Main.ConfigParam = null;
            this.vdsC_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdsC_Main.Drawing_File = null;
            this.vdsC_Main.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vdsC_Main.Icons_Visible_3D = true;
            this.vdsC_Main.Location = new System.Drawing.Point(0, 0);
            this.vdsC_Main.Name = "vdsC_Main";
            this.vdsC_Main.Show_DrawString = false;
            this.vdsC_Main.Size = new System.Drawing.Size(138, 590);
            this.vdsC_Main.TabIndex = 0;
            this.vdsC_Main.View_ON = true;
            // 
            // frmHydrology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 661);
            this.Controls.Add(this.sc_main);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHydrology";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stream Hydrology and Hydrograph";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProject1_Load);
            this.sc_main.Panel1.ResumeLayout(false);
            this.sc_main.Panel2.ResumeLayout(false);
            this.sc_main.ResumeLayout(false);
            this.tc_main.ResumeLayout(false);
            this.tab_create_project.ResumeLayout(false);
            this.tab_create_project.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).EndInit();
            this.grb_survey_type.ResumeLayout(false);
            this.grb_survey_type.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.pnl_tutorial.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tab_satellite.ResumeLayout(false);
            this.tab_survey.ResumeLayout(false);
            this.sc_survey_design.Panel1.ResumeLayout(false);
            this.sc_survey_design.Panel2.ResumeLayout(false);
            this.sc_survey_design.ResumeLayout(false);
            this.tc_survey.ResumeLayout(false);
            this.tab_auto_level_halign.ResumeLayout(false);
            this.tc_auto_level_halign.ResumeLayout(false);
            this.tab_auto_level_prohalign_data.ResumeLayout(false);
            this.tab_auto_level_halign_data.ResumeLayout(false);
            this.tab_auto_traverse_data.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grb_auto_level_process_halign.ResumeLayout(false);
            this.tab_gm.ResumeLayout(false);
            this.tab_gm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grb_from_file.ResumeLayout(false);
            this.grb_from_file.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all_data)).EndInit();
            this.tab_trngu.ResumeLayout(false);
            this.grbGroundModeling.ResumeLayout(false);
            this.grbGroundModeling.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tab_cont.ResumeLayout(false);
            this.tab_cont.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tab_strg_qty.ResumeLayout(false);
            this.tab_discrg_qty.ResumeLayout(false);
            this.tab_alignment.ResumeLayout(false);
            this.tab_alignment.PerformLayout();
            this.tab_trvrs.ResumeLayout(false);
            this.sc_traverse.Panel1.ResumeLayout(false);
            this.sc_traverse.Panel2.ResumeLayout(false);
            this.sc_traverse.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tc_traverse.ResumeLayout(false);
            this.tab_bowditch.ResumeLayout(false);
            this.tab_transit.ResumeLayout(false);
            this.tab_closed_link.ResumeLayout(false);
            this.tab_edm.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tab_land.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tab_site_lvl_grd.ResumeLayout(false);
            this.tc_Site_Leveling_Grading.ResumeLayout(false);
            this.tab_site_step4.ResumeLayout(false);
            this.tc_SLG_halign.ResumeLayout(false);
            this.tab_SLG_prohalign_data.ResumeLayout(false);
            this.tab_SLG_halign_data.ResumeLayout(false);
            this.tab_SLG_traverse_data.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.tab_site_step5.ResumeLayout(false);
            this.sc_HDP_valign.Panel1.ResumeLayout(false);
            this.sc_HDP_valign.Panel2.ResumeLayout(false);
            this.sc_HDP_valign.ResumeLayout(false);
            this.tc_HDP_valign.ResumeLayout(false);
            this.tab_HDP_profile_opt.ResumeLayout(false);
            this.tab_HDP_profile_opt.PerformLayout();
            this.tab_HDP_update_valign_data.ResumeLayout(false);
            this.tab_HDP_update_valign_data.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HDP_pro_opt_prev_data)).EndInit();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HDP_pro_opt_chns)).EndInit();
            this.tab_HDP_valign_design.ResumeLayout(false);
            this.tab_HDP_valign_design.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox29.ResumeLayout(false);
            this.tab_site_step6.ResumeLayout(false);
            this.tc_SLG_cross_secion.ResumeLayout(false);
            this.tab_HDP_define_cross_section.ResumeLayout(false);
            this.sc_SLG_cross_section.Panel1.ResumeLayout(false);
            this.sc_SLG_cross_section.Panel2.ResumeLayout(false);
            this.sc_SLG_cross_section.ResumeLayout(false);
            this.sc_offset.Panel1.ResumeLayout(false);
            this.sc_offset.Panel2.ResumeLayout(false);
            this.sc_offset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_offset_service)).EndInit();
            this.panel13.ResumeLayout(false);
            this.tab_HDP_interface.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.grb_hill_road.ResumeLayout(false);
            this.grb_hill_road.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tab_HDP_create_cross_section.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tab_site_step7.ResumeLayout(false);
            this.sc_volume.Panel1.ResumeLayout(false);
            this.sc_volume.Panel2.ResumeLayout(false);
            this.sc_volume.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.pnl_INT_volume.ResumeLayout(false);
            this.pnl_INT_volume.PerformLayout();
            this.sc_volume_rep.Panel1.ResumeLayout(false);
            this.sc_volume_rep.Panel2.ResumeLayout(false);
            this.sc_volume_rep.ResumeLayout(false);
            this.tab_site_step8.ResumeLayout(false);
            this.tc_HDP_drawings.ResumeLayout(false);
            this.tab_align_sch.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.tab_Plan.ResumeLayout(false);
            this.tab_Profile.ResumeLayout(false);
            this.pnl_INT_str_profile.ResumeLayout(false);
            this.pnl_INT_str_profile.PerformLayout();
            this.tab_xsec.ResumeLayout(false);
            this.pnl_INT_str_cs.ResumeLayout(false);
            this.pnl_INT_str_cs.PerformLayout();
            this.tab_site_step9.ResumeLayout(false);
            this.tab_site_step9.PerformLayout();
            this.tab_disnet.ResumeLayout(false);
            this.tc_disnet.ResumeLayout(false);
            this.tab_create_loops.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tab_pipe_network.ResumeLayout(false);
            this.tab_hydrology.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tab_create_project;
        private System.Windows.Forms.TabPage tab_satellite;
        private System.Windows.Forms.SplitContainer sc_main;
        private System.Windows.Forms.TabPage tab_survey;
        private HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_Main;
        private System.Windows.Forms.Button btn_process_ground_data;
        private System.Windows.Forms.Button btn_run_viewer;
        private System.Windows.Forms.Button btn_run_explorer;
        private System.Windows.Forms.Button btn_info_global_mapper;
        private System.Windows.Forms.Button btn_run_global_mapper;
        private System.Windows.Forms.Button btn_video_ground_data;
        private System.Windows.Forms.Button btn_video_viewer;
        private System.Windows.Forms.Button btn_video_explorer;
        private System.Windows.Forms.Button btn_video_global_mapper;
        private System.Windows.Forms.Button btn_video_google_earth;
        private System.Windows.Forms.Button btn_run_google_earth;
        private System.Windows.Forms.SplitContainer sc_survey_design;
        private System.Windows.Forms.Button btn_land_record;
        private System.Windows.Forms.Button btn_traverse_survey;
        private System.Windows.Forms.Button btn_contour_modeling;
        private System.Windows.Forms.Button btn_traingulation;
        private System.Windows.Forms.Button btn_ground_modeling;
        private System.Windows.Forms.TabControl tc_survey;
        private System.Windows.Forms.TabPage tab_gm;
        private System.Windows.Forms.TabPage tab_trngu;
        private System.Windows.Forms.TabPage tab_cont;
        private System.Windows.Forms.TabPage tab_trvrs;
        private System.Windows.Forms.TabPage tab_land;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.PictureBox pcb_logo;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txt_Project_Name2;
        private System.Windows.Forms.TextBox txt_Working_Folder;
        private System.Windows.Forms.Button btn_open_project;
        private System.Windows.Forms.Button btn_tutor_vids;
        private System.Windows.Forms.Label lbl_tutorial_note;
        private System.Windows.Forms.Panel pnl_tutorial;
        private System.Windows.Forms.Button btn_tutorial_example;
        private System.Windows.Forms.Button btn_Update_Project_Data;
        private System.Windows.Forms.Button btn_Refresh_Project_Data;
        private System.Windows.Forms.Button btn_save_proj_data_file;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_create_project_directory;
        private System.Windows.Forms.GroupBox grb_survey_type;
        private System.Windows.Forms.RadioButton rbtn_bearing_line;
        private System.Windows.Forms.RadioButton rbtn_total_station;
        private System.Windows.Forms.RadioButton rbtn_survey_drawing;
        private System.Windows.Forms.RadioButton rbtn_auto_level;
        private System.Windows.Forms.Button btn_new_project;
        private System.Windows.Forms.Button btn_survey_browse;
        private System.Windows.Forms.TextBox txt_survey_data;
        private System.Windows.Forms.Label lbl_select_survey;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_Project_Name;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grb_from_file;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_select_settings;
        private System.Windows.Forms.Button btn_browse_settings;
        private System.Windows.Forms.Label lbl_text;
        private System.Windows.Forms.TextBox txt_drawing_lib;
        private System.Windows.Forms.Button btn_browse_lib;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ComboBox cmb_layer;
        private System.Windows.Forms.Button btn_delete_rows;
        private System.Windows.Forms.DataGridView dgv_all_data;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDraw;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_draw_el;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_layer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_clr;
        private System.Windows.Forms.CheckBox chk_Draw_All;
        private System.Windows.Forms.Button btn_add_to_list;
        private System.Windows.Forms.ComboBox cmb_draw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chk_utm_conversion;
        private System.Windows.Forms.Button btn_DGM_create_model;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox grbGroundModeling;
        private System.Windows.Forms.TextBox txt_GMT_string;
        private System.Windows.Forms.Button btn_GMT_OK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_GMT_model;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lb_GMT_ModelAndStringName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_GMT_DeSelect;
        private System.Windows.Forms.TextBox txt_GMT_Select;
        private System.Windows.Forms.Button btn_GMT_DeSelect;
        private System.Windows.Forms.Button btn_GMT_Select;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_GMT_DeSelectAll;
        private System.Windows.Forms.Button btn_GMT_SelectAll;
        private System.Windows.Forms.Button btn_contour_refresh;
        private System.Windows.Forms.Button btn_draw_ground_surface;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btn_Contour_create_model;
        private System.Windows.Forms.Button btn_Contour_draw_model;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_Contour_ele_model;
        private System.Windows.Forms.TextBox txt_Contour_ele_inc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Contour_ele_string;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txt_Contour_pri_model;
        private System.Windows.Forms.TextBox txt_Contour_pri_inc;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_Contour_pri_string;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_Contour_sec_model;
        private System.Windows.Forms.TextBox txt_Contour_sec_inc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Contour_sec_string;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btn_crdn_off;
        private System.Windows.Forms.Button btn_dtls_off;
        private System.Windows.Forms.Button btn_crdn_on;
        private System.Windows.Forms.Button btn_dtls_on;
        private System.Windows.Forms.Button btn_chn_off;
        private System.Windows.Forms.Button btn_chn_on;
        private System.Windows.Forms.Button btn_draw_model_strings;
        private System.Windows.Forms.RadioButton rbtn_transverse;
        private System.Windows.Forms.ComboBox cmb_transverse;
        private System.Windows.Forms.RichTextBox rtb_bowditch_data;
        private System.Windows.Forms.TabPage tab_auto_level_halign;
        private System.Windows.Forms.GroupBox grb_auto_level_process_halign;
        private System.Windows.Forms.Button btn_HRP_halign_video;
        private System.Windows.Forms.Button btn_HRP_halign_process;
        private System.Windows.Forms.ComboBox cmb_auto_halign;
        private System.Windows.Forms.RichTextBox rtb_auto_level_halign;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Button btn_halign_details_off;
        private System.Windows.Forms.Button btn_halign_details_on;
        private System.Windows.Forms.Button btn_halign_view;
        private System.Windows.Forms.Button btn_halign_chainage_off;
        private System.Windows.Forms.Button btn_halign_chainage_on;
        private System.Windows.Forms.SplitContainer sc_traverse;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_transverse_report;
        private System.Windows.Forms.Button btn_transverse_draw;
        private System.Windows.Forms.Button btn_transverse_process;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox rtb_transverse_report;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_DGM_open_gm_data;
        private System.Windows.Forms.Button btn_DGM_open_survey_data;
        private System.Windows.Forms.TabControl tc_traverse;
        private System.Windows.Forms.TabPage tab_bowditch;
        private System.Windows.Forms.TabPage tab_transit;
        private System.Windows.Forms.RichTextBox rtb_transit_data;
        private System.Windows.Forms.TabPage tab_closed_link;
        private System.Windows.Forms.RichTextBox rtb_closed_link_data;
        private System.Windows.Forms.TabPage tab_edm;
        private System.Windows.Forms.RichTextBox rtb_edm_data;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Button btn_Land_drawings;
        public System.Windows.Forms.ListBox lst_land_drawings;
        public HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_DOC_Land;
        private HEADS_Site_Projects.Controls.UC_LandRecord uC_LandRecord1;
        private System.Windows.Forms.TabControl tc_auto_level_halign;
        private System.Windows.Forms.TabPage tab_auto_level_prohalign_data;
        private System.Windows.Forms.RichTextBox rtb_auto_level_prohalign;
        private System.Windows.Forms.TabPage tab_auto_level_halign_data;
        private System.Windows.Forms.TabPage tab_auto_traverse_data;
        private System.Windows.Forms.RichTextBox rtb_auto_level_traverse;
        private System.Windows.Forms.TabPage tab_site_lvl_grd;
        private System.Windows.Forms.TabControl tc_Site_Leveling_Grading;
        private System.Windows.Forms.TabPage tab_site_step4;
        private System.Windows.Forms.TabPage tab_site_step5;
        private System.Windows.Forms.TabPage tab_site_step6;
        private System.Windows.Forms.TabPage tab_site_step7;
        private System.Windows.Forms.TabPage tab_site_step8;
        private System.Windows.Forms.TabPage tab_site_step9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btn_SLG_halign_video;
        private System.Windows.Forms.Button btn_SLG_halign_proceed;
        private System.Windows.Forms.ComboBox cmb_SLG_halign;
        private System.Windows.Forms.TabControl tc_SLG_halign;
        private System.Windows.Forms.TabPage tab_SLG_prohalign_data;
        private System.Windows.Forms.RichTextBox rtb_SLG_prohalign;
        private System.Windows.Forms.TabPage tab_SLG_halign_data;
        private System.Windows.Forms.RichTextBox rtb_SLG_halign;
        private System.Windows.Forms.TabPage tab_SLG_traverse_data;
        private System.Windows.Forms.RichTextBox rtb_SLG_traverse_data;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SLG_dlts_OFF;
        private System.Windows.Forms.Button btn_SLG_dlts_ON;
        private System.Windows.Forms.Button btn_SLG_View_Halign;
        private System.Windows.Forms.Button btn_SLG_chn_OFF;
        private System.Windows.Forms.Button btn_SLG_chn_ON;
        private System.Windows.Forms.TabControl tc_SLG_cross_secion;
        private System.Windows.Forms.TabPage tab_HDP_define_cross_section;
        private System.Windows.Forms.TabPage tab_HDP_interface;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button button8;
        private HEADS_Project_Mode.DataFroms.Interface interface_HDP;
        private System.Windows.Forms.GroupBox grb_hill_road;
        private System.Windows.Forms.RadioButton rbtn_HRP_interface_2;
        private System.Windows.Forms.RadioButton rbtn_HRP_interface_1;
        private System.Windows.Forms.TabPage tabPage7;
        private HEADS_Project_Mode.DataFroms.InterfaceNote interfaceNote2;
        private System.Windows.Forms.TabPage tab_HDP_create_cross_section;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.Button btn_cross_section_create;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsmi_HDP_cs_First;
        private System.Windows.Forms.ToolStripButton tsmi_HDP_cs_backword;
        private System.Windows.Forms.ToolStripButton tsmi_HDP_cs_Forward;
        private System.Windows.Forms.ToolStripButton tsmi_HDP_cs_Last;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmb_HDP_cs_sel_chainage;
        private System.Windows.Forms.ComboBox cmb_HDP_cs_sel_strings;
        private System.Windows.Forms.Button btn_cross_section_draw;
        private System.Windows.Forms.Button btn_cross_section_process;
        private HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_HDP_CrossSect;
        private System.Windows.Forms.Panel pnl_INT_volume;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.ComboBox cmb_volume_strings;
        private System.Windows.Forms.RichTextBox Volume_HDP_Data;
        private System.Windows.Forms.Button btn_HDP_volume_process;
        private System.Windows.Forms.Button btn_HDP_volume_masshaul;
        private System.Windows.Forms.TabControl tc_HDP_drawings;
        private System.Windows.Forms.TabPage tab_align_sch;
        private System.Windows.Forms.Button btn_process_diagram_HDP;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.GroupBox groupBox10;
        private HEADS_Project_Mode.DataFroms.Valign_Diagram valign_Diagram_HDP;
        private System.Windows.Forms.GroupBox groupBox9;
        private HEADS_Project_Mode.DataFroms.Halign_Diagram halign_Diagram_HDP;
        private System.Windows.Forms.TabPage tab_Plan;
        private System.Windows.Forms.Button btn_HDP_plan_process;
        private HEADS_Project_Mode.DataFroms.Plan_Drawing plan_Drawing_HDP;
        private System.Windows.Forms.TabPage tab_Profile;
        private System.Windows.Forms.Panel pnl_INT_str_profile;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.ComboBox cmb_INT_profile_strings;
        private System.Windows.Forms.Button btn_HDP_profile_process;
        private HEADS_Project_Mode.DataFroms.Profile_Drawing profile_Drawing_HDP;
        private System.Windows.Forms.TabPage tab_xsec;
        private System.Windows.Forms.Panel pnl_INT_str_cs;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.ComboBox cmb_INT_cross_strings;
        private System.Windows.Forms.Button btn_HDP_cross_process;
        private HEADS_Project_Mode.DataFroms.Cross_Section_Drawing cross_Section_Drawing_HDP;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button btn_HDP_survey_view;
        private System.Windows.Forms.Button btn_HDP_view_drawings;
        private System.Windows.Forms.Button btn_HDP_view_report_file;
        private System.Windows.Forms.Button btn_HDP_open_project_file;
        private System.Windows.Forms.Button btn_HDP_create_project_file;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_HDP_file;
        private System.Windows.Forms.RichTextBox rtb_project_data;
        private System.Windows.Forms.Button btn_HDP_process_project_file;
        private System.Windows.Forms.SplitContainer sc_HDP_valign;
        private System.Windows.Forms.TabControl tc_HDP_valign;
        private System.Windows.Forms.TabPage tab_HDP_profile_opt;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button btn_HDP_profile_opt_process;
        private HEADS_Project_Mode.DataFroms.Design_Project.Design_Profile_Optimization design_Profile_Optimization1;
        private System.Windows.Forms.TabPage tab_HDP_update_valign_data;
        private System.Windows.Forms.Button btn_HDP_pro_opt_get_restore;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Button btn_HDP_pro_opt_update_data_prev;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Button btn_HDP_pro_opt_get_exst_lvls;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.ComboBox cmb_HDP_pro_opt_exist_level_str;
        private System.Windows.Forms.ComboBox cmb_HDP_pro_opt_Bridge_sections;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.DataGridView dgv_HDP_pro_opt_prev_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.TextBox txt_HDP_pro_opt_props_VCL;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox txt_HDP_pro_opt_props_hgt;
        private System.Windows.Forms.Button btn_HDP_pro_opt_props_hgt;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.DataGridView dgv_HDP_pro_opt_chns;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.Button btn_HDP_pro_opt_update_data;
        private System.Windows.Forms.Button btn_HDP_pro_opt_del_row_prev;
        private System.Windows.Forms.Button btn_HDP_pro_opt_del_row;
        private System.Windows.Forms.Button btn_HDP_pro_opt_insert_row_prev;
        private System.Windows.Forms.Button btn_HDP_pro_opt_insert_row;
        private System.Windows.Forms.TabPage tab_HDP_valign_design;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_HDP_Elevation;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Button btn_HDP_Elevation;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_valign_open_vip_design;
        private System.Windows.Forms.Button btn_HDP_Get_Valign_Data;
        private System.Windows.Forms.Button btn_valign_new_vip_design;
        private System.Windows.Forms.Button btn_HDP_valign_open_viewer;
        private System.Windows.Forms.Button btn_valign_process;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Button btn_HDP_Valign_video;
        private System.Windows.Forms.Button btn_HDP_Valign_proceed;
        private System.Windows.Forms.ComboBox cmb_HDP_Valign;
        private System.Windows.Forms.TextBox txt_HDP_ValignData;
        private System.Windows.Forms.Button btn_HDP_valign_draw_selected_profile;
        private System.Windows.Forms.Button btn_HDP_valign_draw_glongsec;
        private System.Windows.Forms.Button btn_HDP_valign_draw_vertical_profile;
        private System.Windows.Forms.Button btn_valign_grid_on;
        private System.Windows.Forms.Button btn_valign_details_off;
        private System.Windows.Forms.Button btn_HDP_create_ground_longsec;
        private System.Windows.Forms.Button btn_valign_details_on;
        private System.Windows.Forms.Button btn_valign_grid_off;
        private HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_HDP_Valign;
        private System.Windows.Forms.DataGridView dgv_offset_service;
        private System.Windows.Forms.Label lbl_off_2;
        private System.Windows.Forms.Label lbl_survey_data;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox txt_trav_align_string;
        private System.Windows.Forms.TextBox txt_trav_align_model;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TextBox txt_SLG_trav_string;
        private System.Windows.Forms.TextBox txt_SLG_trav_model;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.SplitContainer sc_SLG_cross_section;
        private HEADS_Site_Projects.Controls.UC_Canal_CrossSection uC_Canal_CrossSection1;
        private HEADS_Site_Projects.Controls.UC_Dyke_CrossSection uC_Dyke_CrossSection1;
        private HEADS_Project_Mode.DataFroms.UTM_Conversion utM_Conversion1;
        private System.Windows.Forms.TabPage tab_strg_qty;
        private System.Windows.Forms.TabPage tab_disnet;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btn_disnet_create_loop;
        private System.Windows.Forms.ListBox lst_disnet_strings;
        private System.Windows.Forms.ComboBox cmb_disnet_pipenet_model;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btn_disnet_nodal_ground;
        private System.Windows.Forms.TabControl tc_disnet;
        private System.Windows.Forms.TabPage tab_create_loops;
        private System.Windows.Forms.TabPage tab_pipe_network;
        private HEADS_Site_Projects.Controls.UC_DisNet uC_DisNet1;
        private System.Windows.Forms.TabPage tab_discrg_qty;
        private HEADS_Site_Projects.Controls.UC_QuantityMeasurement uC_Discharge;
        private HEADS_Site_Projects.Controls.UC_QuantityMeasurement uC_Stockpile;
        private System.Windows.Forms.CheckBox chk_Contour_ELEV;
        private System.Windows.Forms.CheckBox chk_Contour_C001;
        private System.Windows.Forms.CheckBox chk_Contour_C005;
        private System.Windows.Forms.CheckBox chk_Contour_SURFACE;
        private System.Windows.Forms.SplitContainer sc_offset;
        private HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_DOC_river_canal;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button btn_offset_delete;
        private System.Windows.Forms.Button btn_offset_insert;
        private System.Windows.Forms.Button btn_offset_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.TextBox txt_draw_path;
        private System.Windows.Forms.Button btn_disnet_refresh_loops;
        private System.Windows.Forms.SplitContainer sc_volume;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.SplitContainer sc_volume_rep;
        private HEADS_Project_Mode.DataFroms.VDSC_DOC vdsC_Volume;
        private System.Windows.Forms.RichTextBox Volume_HDP_Report;
        private System.Windows.Forms.TabPage tab_hydrology;
        private HEADS_Project_Mode.DataFroms.UC_StreamHydrology uC_StreamHydrology1;
        private System.Windows.Forms.Button btn_Boundary;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TabPage tab_alignment;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btn_ALGN_dtlsOff;
        private System.Windows.Forms.Button btn_ALGN_dtlsOn;
        private System.Windows.Forms.Button btn_ALGN_chnOff;
        private System.Windows.Forms.Button btn_ALGN_chnOn;
        private System.Windows.Forms.Button btn_ALGN_create;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txt_ALGN_String;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.TextBox txt_ALGN_Model;
        private System.Windows.Forms.Label label142;
    }



}

