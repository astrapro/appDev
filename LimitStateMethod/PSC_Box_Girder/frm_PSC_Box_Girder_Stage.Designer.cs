﻿namespace LimitStateMethod.PSC_Box_Girder
{
    partial class frm_PSC_Box_Girder_Stage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PSC_Box_Girder_Stage));
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
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tab_Analysis_DL = new System.Windows.Forms.TabPage();
            this.tbc_girder = new System.Windows.Forms.TabControl();
            this.tab_user_input = new System.Windows.Forms.TabPage();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.grb_ana_footpath = new System.Windows.Forms.GroupBox();
            this.txt_Ana_Wr = new System.Windows.Forms.TextBox();
            this.label531 = new System.Windows.Forms.Label();
            this.txt_Ana_Wf_RHS = new System.Windows.Forms.TextBox();
            this.txt_Ana_Wk = new System.Windows.Forms.TextBox();
            this.label529 = new System.Windows.Forms.Label();
            this.label530 = new System.Windows.Forms.Label();
            this.label566 = new System.Windows.Forms.Label();
            this.txt_Ana_Hf_RHS = new System.Windows.Forms.TextBox();
            this.txt_Ana_Hf_LHS = new System.Windows.Forms.TextBox();
            this.label528 = new System.Windows.Forms.Label();
            this.label565 = new System.Windows.Forms.Label();
            this.label567 = new System.Windows.Forms.Label();
            this.label524 = new System.Windows.Forms.Label();
            this.label525 = new System.Windows.Forms.Label();
            this.label526 = new System.Windows.Forms.Label();
            this.label564 = new System.Windows.Forms.Label();
            this.txt_Ana_Wf_LHS = new System.Windows.Forms.TextBox();
            this.label527 = new System.Windows.Forms.Label();
            this.chk_fp_right = new System.Windows.Forms.CheckBox();
            this.chk_footpath = new System.Windows.Forms.CheckBox();
            this.chk_fp_left = new System.Windows.Forms.CheckBox();
            this.grb_ana_parapet = new System.Windows.Forms.GroupBox();
            this.txt_Ana_hp = new System.Windows.Forms.TextBox();
            this.label252 = new System.Windows.Forms.Label();
            this.label268 = new System.Windows.Forms.Label();
            this.label269 = new System.Windows.Forms.Label();
            this.txt_Ana_wp = new System.Windows.Forms.TextBox();
            this.label270 = new System.Windows.Forms.Label();
            this.chk_cb_right = new System.Windows.Forms.CheckBox();
            this.chk_crash_barrier = new System.Windows.Forms.CheckBox();
            this.pic_diagram = new System.Windows.Forms.PictureBox();
            this.grb_ana_crash_barrier = new System.Windows.Forms.GroupBox();
            this.txt_Ana_Hc_RHS = new System.Windows.Forms.TextBox();
            this.label563 = new System.Windows.Forms.Label();
            this.txt_Ana_Hc_LHS = new System.Windows.Forms.TextBox();
            this.label562 = new System.Windows.Forms.Label();
            this.label514 = new System.Windows.Forms.Label();
            this.label481 = new System.Windows.Forms.Label();
            this.txt_Ana_Wc_RHS = new System.Windows.Forms.TextBox();
            this.label522 = new System.Windows.Forms.Label();
            this.label510 = new System.Windows.Forms.Label();
            this.label480 = new System.Windows.Forms.Label();
            this.txt_Ana_Wc_LHS = new System.Windows.Forms.TextBox();
            this.label523 = new System.Windows.Forms.Label();
            this.chk_cb_left = new System.Windows.Forms.CheckBox();
            this.grb_ana_wc = new System.Windows.Forms.GroupBox();
            this.label511 = new System.Windows.Forms.Label();
            this.txt_Ana_gamma_w = new System.Windows.Forms.TextBox();
            this.label515 = new System.Windows.Forms.Label();
            this.label520 = new System.Windows.Forms.Label();
            this.txt_Ana_Dw = new System.Windows.Forms.TextBox();
            this.label521 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_psc_new_design = new System.Windows.Forms.Button();
            this.btn_psc_browse = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label283 = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Ana_LL_factor = new System.Windows.Forms.TextBox();
            this.label240 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_factor = new System.Windows.Forms.TextBox();
            this.txt_FPLL = new System.Windows.Forms.TextBox();
            this.txt_SIDL = new System.Windows.Forms.TextBox();
            this.label227 = new System.Windows.Forms.Label();
            this.label228 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.grb_Ana_DL_select_analysis = new System.Windows.Forms.GroupBox();
            this.txt_Ana_analysis_file = new System.Windows.Forms.TextBox();
            this.btn_Ana_DL_browse_input_file = new System.Windows.Forms.Button();
            this.rbtn_Ana_DL_create_analysis_file = new System.Windows.Forms.RadioButton();
            this.rbtn_Ana_DL_select_analysis_file = new System.Windows.Forms.RadioButton();
            this.txt_gd_np = new System.Windows.Forms.TextBox();
            this.grb_create_input_data = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_support_distance = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_Ana_width_cantilever = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Ana_DL_eff_depth = new System.Windows.Forms.TextBox();
            this.txt_carriageway_width = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Ana_B = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ana_L = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_exp_gap = new System.Windows.Forms.TextBox();
            this.txt_overhang_gap = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tab_cs_diagram = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txt_out_IZZ = new System.Windows.Forms.TextBox();
            this.txt_inn_IZZ = new System.Windows.Forms.TextBox();
            this.txt_cen_IZZ = new System.Windows.Forms.TextBox();
            this.label444 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_tot_IZZ = new System.Windows.Forms.TextBox();
            this.label350 = new System.Windows.Forms.Label();
            this.txt_out_IYY = new System.Windows.Forms.TextBox();
            this.txt_inn_IYY = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label443 = new System.Windows.Forms.Label();
            this.txt_cen_IYY = new System.Windows.Forms.TextBox();
            this.label353 = new System.Windows.Forms.Label();
            this.txt_tot_IYY = new System.Windows.Forms.TextBox();
            this.txt_out_IXX = new System.Windows.Forms.TextBox();
            this.label349 = new System.Windows.Forms.Label();
            this.txt_inn_IXX = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txt_cen_IXX = new System.Windows.Forms.TextBox();
            this.label352 = new System.Windows.Forms.Label();
            this.txt_tot_IXX = new System.Windows.Forms.TextBox();
            this.txt_out_pcnt = new System.Windows.Forms.TextBox();
            this.label348 = new System.Windows.Forms.Label();
            this.txt_inn_pcnt = new System.Windows.Forms.TextBox();
            this.txt_out_AX = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txt_inn_AX = new System.Windows.Forms.TextBox();
            this.txt_cen_pcnt = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txt_cen_AX = new System.Windows.Forms.TextBox();
            this.label339 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.txt_tot_AX = new System.Windows.Forms.TextBox();
            this.label271 = new System.Windows.Forms.Label();
            this.label338 = new System.Windows.Forms.Label();
            this.label351 = new System.Windows.Forms.Label();
            this.label505 = new System.Windows.Forms.Label();
            this.label502 = new System.Windows.Forms.Label();
            this.label501 = new System.Windows.Forms.Label();
            this.label336 = new System.Windows.Forms.Label();
            this.label272 = new System.Windows.Forms.Label();
            this.label347 = new System.Windows.Forms.Label();
            this.label342 = new System.Windows.Forms.Label();
            this.label273 = new System.Windows.Forms.Label();
            this.label274 = new System.Windows.Forms.Label();
            this.label335 = new System.Windows.Forms.Label();
            this.label341 = new System.Windows.Forms.Label();
            this.label346 = new System.Windows.Forms.Label();
            this.label275 = new System.Windows.Forms.Label();
            this.label302 = new System.Windows.Forms.Label();
            this.label340 = new System.Windows.Forms.Label();
            this.label345 = new System.Windows.Forms.Label();
            this.label305 = new System.Windows.Forms.Label();
            this.label306 = new System.Windows.Forms.Label();
            this.label337 = new System.Windows.Forms.Label();
            this.label344 = new System.Windows.Forms.Label();
            this.label307 = new System.Windows.Forms.Label();
            this.label343 = new System.Windows.Forms.Label();
            this.label308 = new System.Windows.Forms.Label();
            this.label498 = new System.Windows.Forms.Label();
            this.label309 = new System.Windows.Forms.Label();
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
            this.btn_Show_Section_Resulf = new System.Windows.Forms.Button();
            this.rtb_sections = new System.Windows.Forms.RichTextBox();
            this.label176 = new System.Windows.Forms.Label();
            this.label226 = new System.Windows.Forms.Label();
            this.tab_moving_data_british = new System.Windows.Forms.TabPage();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.label557 = new System.Windows.Forms.Label();
            this.label558 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label559 = new System.Windows.Forms.Label();
            this.label561 = new System.Windows.Forms.Label();
            this.btn_bs_view_moving_load = new System.Windows.Forms.Button();
            this.label1190 = new System.Windows.Forms.Label();
            this.cmb_bs_view_moving_load = new System.Windows.Forms.ComboBox();
            this.txt_bs_vehicle_gap = new System.Windows.Forms.TextBox();
            this.txt_ll_british_lgen = new System.Windows.Forms.TextBox();
            this.label1191 = new System.Windows.Forms.Label();
            this.spc_HB = new System.Windows.Forms.SplitContainer();
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
            this.lbl_HB = new System.Windows.Forms.Label();
            this.txt_ll_british_incr = new System.Windows.Forms.TextBox();
            this.groupBox107 = new System.Windows.Forms.GroupBox();
            this.groupBox108 = new System.Windows.Forms.GroupBox();
            this.chk_HA = new System.Windows.Forms.CheckBox();
            this.rbtn_Rail_Load = new System.Windows.Forms.RadioButton();
            this.rbtn_HA_HB = new System.Windows.Forms.RadioButton();
            this.rbtn_HA = new System.Windows.Forms.RadioButton();
            this.rbtn_HB = new System.Windows.Forms.RadioButton();
            this.txt_LL_lf = new System.Windows.Forms.TextBox();
            this.txt_LL_impf = new System.Windows.Forms.TextBox();
            this.txt_no_lanes = new System.Windows.Forms.TextBox();
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
            this.txt_lane_width = new System.Windows.Forms.TextBox();
            this.label262 = new System.Windows.Forms.Label();
            this.txt_deck_width = new System.Windows.Forms.TextBox();
            this.label263 = new System.Windows.Forms.Label();
            this.label264 = new System.Windows.Forms.Label();
            this.label265 = new System.Windows.Forms.Label();
            this.label266 = new System.Windows.Forms.Label();
            this.tab_moving_data_indian = new System.Windows.Forms.TabPage();
            this.btn_irc_view_moving_load = new System.Windows.Forms.Button();
            this.label568 = new System.Windows.Forms.Label();
            this.cmb_irc_view_moving_load = new System.Windows.Forms.ComboBox();
            this.txt_irc_vehicle_gap = new System.Windows.Forms.TextBox();
            this.label569 = new System.Windows.Forms.Label();
            this.label249 = new System.Windows.Forms.Label();
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
            this.groupBox51 = new System.Windows.Forms.GroupBox();
            this.label330 = new System.Windows.Forms.Label();
            this.label331 = new System.Windows.Forms.Label();
            this.label332 = new System.Windows.Forms.Label();
            this.label333 = new System.Windows.Forms.Label();
            this.label334 = new System.Windows.Forms.Label();
            this.txt_PR_conc = new System.Windows.Forms.TextBox();
            this.txt_den_conc = new System.Windows.Forms.TextBox();
            this.txt_emod_conc = new System.Windows.Forms.TextBox();
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
            this.btn_View_Post_Process = new System.Windows.Forms.Button();
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
            this.uC_Res_Normal = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Results();
            this.tab_stage = new System.Windows.Forms.TabPage();
            this.tc_stage = new System.Windows.Forms.TabControl();
            this.tab_stage1 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage1 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage();
            this.tab_stage2 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage2 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage();
            this.tab_stage3 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage3 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage();
            this.tab_stage4 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage4 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage();
            this.tab_stage5 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage5 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage();
            this.tab_designSage = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Results1 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Results();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btn_result_summary = new System.Windows.Forms.Button();
            this.cmb_design_stage = new System.Windows.Forms.ComboBox();
            this.label117 = new System.Windows.Forms.Label();
            this.tab_worksheet_design = new System.Windows.Forms.TabPage();
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
            this.tc_abutment = new System.Windows.Forms.TabControl();
            this.tab_AbutmentLSM = new System.Windows.Forms.TabPage();
            this.uC_RCC_Abut1 = new BridgeAnalysisDesign.Abutment.UC_RCC_Abut();
            this.tab_AbutmentPileLSM = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.uC_AbutmentPileLS1 = new BridgeAnalysisDesign.Abutment.UC_AbutmentPileLS();
            this.tab_pier = new System.Windows.Forms.TabPage();
            this.tc_pier = new System.Windows.Forms.TabControl();
            this.tab_PierOpenLSM = new System.Windows.Forms.TabPage();
            this.uC_PierOpenLS1 = new BridgeAnalysisDesign.Pier.UC_PierOpenLS();
            this.tab_PierPileLSM = new System.Windows.Forms.TabPage();
            this.uC_PierDesignLSM1 = new BridgeAnalysisDesign.Pier.UC_PierDesignLSM();
            this.label155 = new System.Windows.Forms.Label();
            this.btn_RCC_Pier_Process = new System.Windows.Forms.Button();
            this.btn_RCC_Pier_Report = new System.Windows.Forms.Button();
            this.tab_drawings = new System.Windows.Forms.TabPage();
            this.btn_dwg_open_Pier = new System.Windows.Forms.Button();
            this.btn_dwg_open_Cantilever = new System.Windows.Forms.Button();
            this.btn_dwg_open_Counterfort = new System.Windows.Forms.Button();
            this.label157 = new System.Windows.Forms.Label();
            this.btn_dwg_pier = new System.Windows.Forms.Button();
            this.btn_open_drawings = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.tc_main.SuspendLayout();
            this.tab_Analysis_DL.SuspendLayout();
            this.tbc_girder.SuspendLayout();
            this.tab_user_input.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.grb_ana_footpath.SuspendLayout();
            this.grb_ana_parapet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).BeginInit();
            this.grb_ana_crash_barrier.SuspendLayout();
            this.grb_ana_wc.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.grb_Ana_DL_select_analysis.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tab_cs_diagram.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel8.SuspendLayout();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tab_moving_data_british.SuspendLayout();
            this.groupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_HB)).BeginInit();
            this.spc_HB.Panel1.SuspendLayout();
            this.spc_HB.Panel2.SuspendLayout();
            this.spc_HB.SuspendLayout();
            this.groupBox106.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_british_loads)).BeginInit();
            this.groupBox105.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_british_loads)).BeginInit();
            this.groupBox107.SuspendLayout();
            this.groupBox108.SuspendLayout();
            this.grb_ha.SuspendLayout();
            this.grb_ha_aply.SuspendLayout();
            this.grb_hb.SuspendLayout();
            this.grb_hb_aply.SuspendLayout();
            this.tab_moving_data_indian.SuspendLayout();
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
            this.groupBox51.SuspendLayout();
            this.groupBox70.SuspendLayout();
            this.groupBox109.SuspendLayout();
            this.groupBox71.SuspendLayout();
            this.tab_stage.SuspendLayout();
            this.tc_stage.SuspendLayout();
            this.tab_stage1.SuspendLayout();
            this.tab_stage2.SuspendLayout();
            this.tab_stage3.SuspendLayout();
            this.tab_stage4.SuspendLayout();
            this.tab_stage5.SuspendLayout();
            this.tab_designSage.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tab_worksheet_design.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
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
            this.tc_abutment.SuspendLayout();
            this.tab_AbutmentLSM.SuspendLayout();
            this.tab_AbutmentPileLSM.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tab_pier.SuspendLayout();
            this.tc_pier.SuspendLayout();
            this.tab_PierOpenLSM.SuspendLayout();
            this.tab_PierPileLSM.SuspendLayout();
            this.tab_drawings.SuspendLayout();
            this.SuspendLayout();
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
            this.tc_main.Size = new System.Drawing.Size(983, 692);
            this.tc_main.TabIndex = 2;
            // 
            // tab_Analysis_DL
            // 
            this.tab_Analysis_DL.Controls.Add(this.tbc_girder);
            this.tab_Analysis_DL.Location = new System.Drawing.Point(4, 22);
            this.tab_Analysis_DL.Name = "tab_Analysis_DL";
            this.tab_Analysis_DL.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Analysis_DL.Size = new System.Drawing.Size(975, 666);
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
            this.tbc_girder.Controls.Add(this.tab_analysis);
            this.tbc_girder.Controls.Add(this.tab_stage);
            this.tbc_girder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_girder.Location = new System.Drawing.Point(3, 3);
            this.tbc_girder.Name = "tbc_girder";
            this.tbc_girder.SelectedIndex = 0;
            this.tbc_girder.Size = new System.Drawing.Size(969, 660);
            this.tbc_girder.TabIndex = 107;
            // 
            // tab_user_input
            // 
            this.tab_user_input.Controls.Add(this.groupBox27);
            this.tab_user_input.Controls.Add(this.panel5);
            this.tab_user_input.Controls.Add(this.label239);
            this.tab_user_input.Controls.Add(this.label21);
            this.tab_user_input.Controls.Add(this.label11);
            this.tab_user_input.Controls.Add(this.txt_Ana_LL_factor);
            this.tab_user_input.Controls.Add(this.label240);
            this.tab_user_input.Controls.Add(this.label17);
            this.tab_user_input.Controls.Add(this.label10);
            this.tab_user_input.Controls.Add(this.txt_Ana_DL_factor);
            this.tab_user_input.Controls.Add(this.txt_FPLL);
            this.tab_user_input.Controls.Add(this.txt_SIDL);
            this.tab_user_input.Controls.Add(this.label227);
            this.tab_user_input.Controls.Add(this.label228);
            this.tab_user_input.Controls.Add(this.groupBox12);
            this.tab_user_input.Controls.Add(this.grb_create_input_data);
            this.tab_user_input.Controls.Add(this.pictureBox1);
            this.tab_user_input.Location = new System.Drawing.Point(4, 22);
            this.tab_user_input.Name = "tab_user_input";
            this.tab_user_input.Padding = new System.Windows.Forms.Padding(3);
            this.tab_user_input.Size = new System.Drawing.Size(961, 634);
            this.tab_user_input.TabIndex = 0;
            this.tab_user_input.Text = "User\'s Input Data";
            this.tab_user_input.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.grb_ana_footpath);
            this.groupBox27.Controls.Add(this.chk_fp_right);
            this.groupBox27.Controls.Add(this.chk_footpath);
            this.groupBox27.Controls.Add(this.chk_fp_left);
            this.groupBox27.Controls.Add(this.grb_ana_parapet);
            this.groupBox27.Controls.Add(this.chk_cb_right);
            this.groupBox27.Controls.Add(this.chk_crash_barrier);
            this.groupBox27.Controls.Add(this.pic_diagram);
            this.groupBox27.Controls.Add(this.grb_ana_crash_barrier);
            this.groupBox27.Controls.Add(this.chk_cb_left);
            this.groupBox27.Controls.Add(this.grb_ana_wc);
            this.groupBox27.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox27.ForeColor = System.Drawing.Color.Black;
            this.groupBox27.Location = new System.Drawing.Point(406, 14);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(493, 624);
            this.groupBox27.TabIndex = 181;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "SUPER IMPOSED DEAD LOAD [SIDL]";
            // 
            // grb_ana_footpath
            // 
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Wr);
            this.grb_ana_footpath.Controls.Add(this.label531);
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Wf_RHS);
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Wk);
            this.grb_ana_footpath.Controls.Add(this.label529);
            this.grb_ana_footpath.Controls.Add(this.label530);
            this.grb_ana_footpath.Controls.Add(this.label566);
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Hf_RHS);
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Hf_LHS);
            this.grb_ana_footpath.Controls.Add(this.label528);
            this.grb_ana_footpath.Controls.Add(this.label565);
            this.grb_ana_footpath.Controls.Add(this.label567);
            this.grb_ana_footpath.Controls.Add(this.label524);
            this.grb_ana_footpath.Controls.Add(this.label525);
            this.grb_ana_footpath.Controls.Add(this.label526);
            this.grb_ana_footpath.Controls.Add(this.label564);
            this.grb_ana_footpath.Controls.Add(this.txt_Ana_Wf_LHS);
            this.grb_ana_footpath.Controls.Add(this.label527);
            this.grb_ana_footpath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_footpath.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_footpath.Location = new System.Drawing.Point(6, 203);
            this.grb_ana_footpath.Name = "grb_ana_footpath";
            this.grb_ana_footpath.Size = new System.Drawing.Size(467, 139);
            this.grb_ana_footpath.TabIndex = 1;
            this.grb_ana_footpath.TabStop = false;
            this.grb_ana_footpath.Text = "Footpath and Kerb";
            // 
            // txt_Ana_Wr
            // 
            this.txt_Ana_Wr.Location = new System.Drawing.Point(178, 109);
            this.txt_Ana_Wr.Name = "txt_Ana_Wr";
            this.txt_Ana_Wr.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wr.TabIndex = 3;
            this.txt_Ana_Wr.Text = "0.0";
            this.txt_Ana_Wr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wr.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label531
            // 
            this.label531.AutoSize = true;
            this.label531.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label531.Location = new System.Drawing.Point(12, 112);
            this.label531.Name = "label531";
            this.label531.Size = new System.Drawing.Size(160, 13);
            this.label531.TabIndex = 3;
            this.label531.Text = "Width of Outer Railing [wr]";
            // 
            // txt_Ana_Wf_RHS
            // 
            this.txt_Ana_Wf_RHS.Location = new System.Drawing.Point(351, 20);
            this.txt_Ana_Wf_RHS.Name = "txt_Ana_Wf_RHS";
            this.txt_Ana_Wf_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wf_RHS.TabIndex = 2;
            this.txt_Ana_Wf_RHS.Text = "0.0";
            this.txt_Ana_Wf_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wf_RHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Wk
            // 
            this.txt_Ana_Wk.Location = new System.Drawing.Point(178, 82);
            this.txt_Ana_Wk.Name = "txt_Ana_Wk";
            this.txt_Ana_Wk.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wk.TabIndex = 2;
            this.txt_Ana_Wk.Text = "0.0";
            this.txt_Ana_Wk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wk.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label529
            // 
            this.label529.AutoSize = true;
            this.label529.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label529.Location = new System.Drawing.Point(12, 85);
            this.label529.Name = "label529";
            this.label529.Size = new System.Drawing.Size(115, 13);
            this.label529.TabIndex = 3;
            this.label529.Text = "Width of Kerb [wk]";
            // 
            // label530
            // 
            this.label530.AutoSize = true;
            this.label530.Location = new System.Drawing.Point(234, 112);
            this.label530.Name = "label530";
            this.label530.Size = new System.Drawing.Size(18, 13);
            this.label530.TabIndex = 2;
            this.label530.Text = "m";
            // 
            // label566
            // 
            this.label566.AutoSize = true;
            this.label566.Location = new System.Drawing.Point(407, 23);
            this.label566.Name = "label566";
            this.label566.Size = new System.Drawing.Size(18, 13);
            this.label566.TabIndex = 2;
            this.label566.Text = "m";
            // 
            // txt_Ana_Hf_RHS
            // 
            this.txt_Ana_Hf_RHS.Location = new System.Drawing.Point(351, 41);
            this.txt_Ana_Hf_RHS.Name = "txt_Ana_Hf_RHS";
            this.txt_Ana_Hf_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hf_RHS.TabIndex = 1;
            this.txt_Ana_Hf_RHS.Text = "0.0";
            this.txt_Ana_Hf_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hf_RHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Hf_LHS
            // 
            this.txt_Ana_Hf_LHS.Location = new System.Drawing.Point(130, 44);
            this.txt_Ana_Hf_LHS.Name = "txt_Ana_Hf_LHS";
            this.txt_Ana_Hf_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hf_LHS.TabIndex = 1;
            this.txt_Ana_Hf_LHS.Text = "0.0";
            this.txt_Ana_Hf_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hf_LHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label528
            // 
            this.label528.AutoSize = true;
            this.label528.Location = new System.Drawing.Point(234, 85);
            this.label528.Name = "label528";
            this.label528.Size = new System.Drawing.Size(18, 13);
            this.label528.TabIndex = 2;
            this.label528.Text = "m";
            // 
            // label565
            // 
            this.label565.AutoSize = true;
            this.label565.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label565.Location = new System.Drawing.Point(245, 44);
            this.label565.Name = "label565";
            this.label565.Size = new System.Drawing.Size(100, 13);
            this.label565.TabIndex = 3;
            this.label565.Text = "Height [RHS_hf]";
            // 
            // label567
            // 
            this.label567.AutoSize = true;
            this.label567.Location = new System.Drawing.Point(407, 44);
            this.label567.Name = "label567";
            this.label567.Size = new System.Drawing.Size(18, 13);
            this.label567.TabIndex = 2;
            this.label567.Text = "m";
            // 
            // label524
            // 
            this.label524.AutoSize = true;
            this.label524.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label524.Location = new System.Drawing.Point(6, 47);
            this.label524.Name = "label524";
            this.label524.Size = new System.Drawing.Size(98, 13);
            this.label524.TabIndex = 3;
            this.label524.Text = "Height [LHS_hf]";
            // 
            // label525
            // 
            this.label525.AutoSize = true;
            this.label525.Location = new System.Drawing.Point(186, 47);
            this.label525.Name = "label525";
            this.label525.Size = new System.Drawing.Size(18, 13);
            this.label525.TabIndex = 2;
            this.label525.Text = "m";
            // 
            // label526
            // 
            this.label526.AutoSize = true;
            this.label526.Location = new System.Drawing.Point(186, 23);
            this.label526.Name = "label526";
            this.label526.Size = new System.Drawing.Size(18, 13);
            this.label526.TabIndex = 2;
            this.label526.Text = "m";
            // 
            // label564
            // 
            this.label564.AutoSize = true;
            this.label564.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label564.Location = new System.Drawing.Point(245, 23);
            this.label564.Name = "label564";
            this.label564.Size = new System.Drawing.Size(98, 13);
            this.label564.TabIndex = 0;
            this.label564.Text = "Width [RHS_wf]";
            // 
            // txt_Ana_Wf_LHS
            // 
            this.txt_Ana_Wf_LHS.Location = new System.Drawing.Point(130, 20);
            this.txt_Ana_Wf_LHS.Name = "txt_Ana_Wf_LHS";
            this.txt_Ana_Wf_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wf_LHS.TabIndex = 0;
            this.txt_Ana_Wf_LHS.Text = "0.0";
            this.txt_Ana_Wf_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wf_LHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label527
            // 
            this.label527.AutoSize = true;
            this.label527.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label527.Location = new System.Drawing.Point(6, 20);
            this.label527.Name = "label527";
            this.label527.Size = new System.Drawing.Size(96, 13);
            this.label527.TabIndex = 0;
            this.label527.Text = "Width [LHS_wf]";
            // 
            // chk_fp_right
            // 
            this.chk_fp_right.AutoSize = true;
            this.chk_fp_right.ForeColor = System.Drawing.Color.Blue;
            this.chk_fp_right.Location = new System.Drawing.Point(343, 182);
            this.chk_fp_right.Name = "chk_fp_right";
            this.chk_fp_right.Size = new System.Drawing.Size(101, 17);
            this.chk_fp_right.TabIndex = 0;
            this.chk_fp_right.Text = "RIGHT SIDE";
            this.chk_fp_right.UseVisualStyleBackColor = true;
            this.chk_fp_right.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
            // 
            // chk_footpath
            // 
            this.chk_footpath.AutoSize = true;
            this.chk_footpath.Location = new System.Drawing.Point(6, 182);
            this.chk_footpath.Name = "chk_footpath";
            this.chk_footpath.Size = new System.Drawing.Size(177, 17);
            this.chk_footpath.TabIndex = 3;
            this.chk_footpath.Text = "SIDE WALK/FOOTPATH ";
            this.chk_footpath.UseVisualStyleBackColor = true;
            this.chk_footpath.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
            // 
            // chk_fp_left
            // 
            this.chk_fp_left.AutoSize = true;
            this.chk_fp_left.ForeColor = System.Drawing.Color.Blue;
            this.chk_fp_left.Location = new System.Drawing.Point(245, 182);
            this.chk_fp_left.Name = "chk_fp_left";
            this.chk_fp_left.Size = new System.Drawing.Size(92, 17);
            this.chk_fp_left.TabIndex = 1;
            this.chk_fp_left.Text = "LEFT SIDE";
            this.chk_fp_left.UseVisualStyleBackColor = true;
            this.chk_fp_left.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
            // 
            // grb_ana_parapet
            // 
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_hp);
            this.grb_ana_parapet.Controls.Add(this.label252);
            this.grb_ana_parapet.Controls.Add(this.label268);
            this.grb_ana_parapet.Controls.Add(this.label269);
            this.grb_ana_parapet.Controls.Add(this.txt_Ana_wp);
            this.grb_ana_parapet.Controls.Add(this.label270);
            this.grb_ana_parapet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_parapet.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_parapet.Location = new System.Drawing.Point(6, 58);
            this.grb_ana_parapet.Name = "grb_ana_parapet";
            this.grb_ana_parapet.Size = new System.Drawing.Size(467, 40);
            this.grb_ana_parapet.TabIndex = 96;
            this.grb_ana_parapet.TabStop = false;
            this.grb_ana_parapet.Text = "Parapet Wall";
            // 
            // txt_Ana_hp
            // 
            this.txt_Ana_hp.Location = new System.Drawing.Point(351, 14);
            this.txt_Ana_hp.Name = "txt_Ana_hp";
            this.txt_Ana_hp.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_hp.TabIndex = 2;
            this.txt_Ana_hp.Text = "1.200";
            this.txt_Ana_hp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_hp.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label252
            // 
            this.label252.AutoSize = true;
            this.label252.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label252.Location = new System.Drawing.Point(274, 17);
            this.label252.Name = "label252";
            this.label252.Size = new System.Drawing.Size(71, 13);
            this.label252.TabIndex = 3;
            this.label252.Text = "Height [hp]";
            // 
            // label268
            // 
            this.label268.AutoSize = true;
            this.label268.Location = new System.Drawing.Point(407, 17);
            this.label268.Name = "label268";
            this.label268.Size = new System.Drawing.Size(18, 13);
            this.label268.TabIndex = 2;
            this.label268.Text = "m";
            // 
            // label269
            // 
            this.label269.AutoSize = true;
            this.label269.Location = new System.Drawing.Point(186, 17);
            this.label269.Name = "label269";
            this.label269.Size = new System.Drawing.Size(18, 13);
            this.label269.TabIndex = 0;
            this.label269.Text = "m";
            // 
            // txt_Ana_wp
            // 
            this.txt_Ana_wp.Location = new System.Drawing.Point(130, 14);
            this.txt_Ana_wp.Name = "txt_Ana_wp";
            this.txt_Ana_wp.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_wp.TabIndex = 0;
            this.txt_Ana_wp.Text = "0.500";
            this.txt_Ana_wp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_wp.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label270
            // 
            this.label270.AutoSize = true;
            this.label270.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label270.Location = new System.Drawing.Point(5, 17);
            this.label270.Name = "label270";
            this.label270.Size = new System.Drawing.Size(69, 13);
            this.label270.TabIndex = 0;
            this.label270.Text = "Width [wp]";
            // 
            // chk_cb_right
            // 
            this.chk_cb_right.AutoSize = true;
            this.chk_cb_right.ForeColor = System.Drawing.Color.Blue;
            this.chk_cb_right.Location = new System.Drawing.Point(343, 101);
            this.chk_cb_right.Name = "chk_cb_right";
            this.chk_cb_right.Size = new System.Drawing.Size(101, 17);
            this.chk_cb_right.TabIndex = 0;
            this.chk_cb_right.Text = "RIGHT SIDE";
            this.chk_cb_right.UseVisualStyleBackColor = true;
            this.chk_cb_right.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
            // 
            // chk_crash_barrier
            // 
            this.chk_crash_barrier.AutoSize = true;
            this.chk_crash_barrier.Location = new System.Drawing.Point(6, 101);
            this.chk_crash_barrier.Name = "chk_crash_barrier";
            this.chk_crash_barrier.Size = new System.Drawing.Size(131, 17);
            this.chk_crash_barrier.TabIndex = 3;
            this.chk_crash_barrier.Text = "CRASH BARRIER ";
            this.chk_crash_barrier.UseVisualStyleBackColor = true;
            this.chk_crash_barrier.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
            // 
            // pic_diagram
            // 
            this.pic_diagram.BackgroundImage = global::LimitStateMethod.Properties.Resources.case_1;
            this.pic_diagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_diagram.Location = new System.Drawing.Point(6, 348);
            this.pic_diagram.Name = "pic_diagram";
            this.pic_diagram.Size = new System.Drawing.Size(456, 273);
            this.pic_diagram.TabIndex = 95;
            this.pic_diagram.TabStop = false;
            // 
            // grb_ana_crash_barrier
            // 
            this.grb_ana_crash_barrier.Controls.Add(this.txt_Ana_Hc_RHS);
            this.grb_ana_crash_barrier.Controls.Add(this.label563);
            this.grb_ana_crash_barrier.Controls.Add(this.txt_Ana_Hc_LHS);
            this.grb_ana_crash_barrier.Controls.Add(this.label562);
            this.grb_ana_crash_barrier.Controls.Add(this.label514);
            this.grb_ana_crash_barrier.Controls.Add(this.label481);
            this.grb_ana_crash_barrier.Controls.Add(this.txt_Ana_Wc_RHS);
            this.grb_ana_crash_barrier.Controls.Add(this.label522);
            this.grb_ana_crash_barrier.Controls.Add(this.label510);
            this.grb_ana_crash_barrier.Controls.Add(this.label480);
            this.grb_ana_crash_barrier.Controls.Add(this.txt_Ana_Wc_LHS);
            this.grb_ana_crash_barrier.Controls.Add(this.label523);
            this.grb_ana_crash_barrier.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ana_crash_barrier.ForeColor = System.Drawing.Color.Blue;
            this.grb_ana_crash_barrier.Location = new System.Drawing.Point(6, 117);
            this.grb_ana_crash_barrier.Name = "grb_ana_crash_barrier";
            this.grb_ana_crash_barrier.Size = new System.Drawing.Size(467, 60);
            this.grb_ana_crash_barrier.TabIndex = 1;
            this.grb_ana_crash_barrier.TabStop = false;
            // 
            // txt_Ana_Hc_RHS
            // 
            this.txt_Ana_Hc_RHS.Location = new System.Drawing.Point(351, 32);
            this.txt_Ana_Hc_RHS.Name = "txt_Ana_Hc_RHS";
            this.txt_Ana_Hc_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hc_RHS.TabIndex = 2;
            this.txt_Ana_Hc_RHS.Text = "1.200";
            this.txt_Ana_Hc_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hc_RHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label563
            // 
            this.label563.AutoSize = true;
            this.label563.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label563.Location = new System.Drawing.Point(249, 35);
            this.label563.Name = "label563";
            this.label563.Size = new System.Drawing.Size(101, 13);
            this.label563.TabIndex = 3;
            this.label563.Text = "Height [RHS_hc]";
            // 
            // txt_Ana_Hc_LHS
            // 
            this.txt_Ana_Hc_LHS.Location = new System.Drawing.Point(130, 32);
            this.txt_Ana_Hc_LHS.Name = "txt_Ana_Hc_LHS";
            this.txt_Ana_Hc_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Hc_LHS.TabIndex = 2;
            this.txt_Ana_Hc_LHS.Text = "1.200";
            this.txt_Ana_Hc_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Hc_LHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label562
            // 
            this.label562.AutoSize = true;
            this.label562.Location = new System.Drawing.Point(407, 35);
            this.label562.Name = "label562";
            this.label562.Size = new System.Drawing.Size(18, 13);
            this.label562.TabIndex = 2;
            this.label562.Text = "m";
            // 
            // label514
            // 
            this.label514.AutoSize = true;
            this.label514.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label514.Location = new System.Drawing.Point(8, 35);
            this.label514.Name = "label514";
            this.label514.Size = new System.Drawing.Size(99, 13);
            this.label514.TabIndex = 3;
            this.label514.Text = "Height [LHS_hc]";
            // 
            // label481
            // 
            this.label481.AutoSize = true;
            this.label481.Location = new System.Drawing.Point(407, 13);
            this.label481.Name = "label481";
            this.label481.Size = new System.Drawing.Size(18, 13);
            this.label481.TabIndex = 0;
            this.label481.Text = "m";
            // 
            // txt_Ana_Wc_RHS
            // 
            this.txt_Ana_Wc_RHS.Location = new System.Drawing.Point(351, 10);
            this.txt_Ana_Wc_RHS.Name = "txt_Ana_Wc_RHS";
            this.txt_Ana_Wc_RHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wc_RHS.TabIndex = 1;
            this.txt_Ana_Wc_RHS.Text = " 0.500";
            this.txt_Ana_Wc_RHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wc_RHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label522
            // 
            this.label522.AutoSize = true;
            this.label522.Location = new System.Drawing.Point(186, 13);
            this.label522.Name = "label522";
            this.label522.Size = new System.Drawing.Size(18, 13);
            this.label522.TabIndex = 0;
            this.label522.Text = "m";
            // 
            // label510
            // 
            this.label510.AutoSize = true;
            this.label510.Location = new System.Drawing.Point(186, 35);
            this.label510.Name = "label510";
            this.label510.Size = new System.Drawing.Size(18, 13);
            this.label510.TabIndex = 2;
            this.label510.Text = "m";
            // 
            // label480
            // 
            this.label480.AutoSize = true;
            this.label480.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label480.Location = new System.Drawing.Point(249, 13);
            this.label480.Name = "label480";
            this.label480.Size = new System.Drawing.Size(99, 13);
            this.label480.TabIndex = 0;
            this.label480.Text = "Width [RHS_wc]";
            // 
            // txt_Ana_Wc_LHS
            // 
            this.txt_Ana_Wc_LHS.Location = new System.Drawing.Point(130, 10);
            this.txt_Ana_Wc_LHS.Name = "txt_Ana_Wc_LHS";
            this.txt_Ana_Wc_LHS.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Wc_LHS.TabIndex = 1;
            this.txt_Ana_Wc_LHS.Text = " 0.500";
            this.txt_Ana_Wc_LHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Wc_LHS.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label523
            // 
            this.label523.AutoSize = true;
            this.label523.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label523.Location = new System.Drawing.Point(5, 13);
            this.label523.Name = "label523";
            this.label523.Size = new System.Drawing.Size(97, 13);
            this.label523.TabIndex = 0;
            this.label523.Text = "Width [LHS_wc]";
            // 
            // chk_cb_left
            // 
            this.chk_cb_left.AutoSize = true;
            this.chk_cb_left.ForeColor = System.Drawing.Color.Blue;
            this.chk_cb_left.Location = new System.Drawing.Point(245, 101);
            this.chk_cb_left.Name = "chk_cb_left";
            this.chk_cb_left.Size = new System.Drawing.Size(92, 17);
            this.chk_cb_left.TabIndex = 1;
            this.chk_cb_left.Text = "LEFT SIDE";
            this.chk_cb_left.UseVisualStyleBackColor = true;
            this.chk_cb_left.CheckedChanged += new System.EventHandler(this.chk_crash_barrier_CheckedChanged);
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
            this.grb_ana_wc.Location = new System.Drawing.Point(6, 17);
            this.grb_ana_wc.Name = "grb_ana_wc";
            this.grb_ana_wc.Size = new System.Drawing.Size(467, 40);
            this.grb_ana_wc.TabIndex = 1;
            this.grb_ana_wc.TabStop = false;
            this.grb_ana_wc.Text = "Wearing Course";
            // 
            // label511
            // 
            this.label511.AutoSize = true;
            this.label511.Location = new System.Drawing.Point(407, 18);
            this.label511.Name = "label511";
            this.label511.Size = new System.Drawing.Size(55, 13);
            this.label511.TabIndex = 1;
            this.label511.Text = "kN/cu.m";
            // 
            // txt_Ana_gamma_w
            // 
            this.txt_Ana_gamma_w.Location = new System.Drawing.Point(351, 15);
            this.txt_Ana_gamma_w.Name = "txt_Ana_gamma_w";
            this.txt_Ana_gamma_w.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_gamma_w.TabIndex = 1;
            this.txt_Ana_gamma_w.Text = "22";
            this.txt_Ana_gamma_w.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_gamma_w.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label515
            // 
            this.label515.AutoSize = true;
            this.label515.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label515.Location = new System.Drawing.Point(238, 19);
            this.label515.Name = "label515";
            this.label515.Size = new System.Drawing.Size(107, 13);
            this.label515.TabIndex = 3;
            this.label515.Text = "Unit weight [Y_w]";
            // 
            // label520
            // 
            this.label520.AutoSize = true;
            this.label520.Location = new System.Drawing.Point(186, 17);
            this.label520.Name = "label520";
            this.label520.Size = new System.Drawing.Size(18, 13);
            this.label520.TabIndex = 0;
            this.label520.Text = "m";
            // 
            // txt_Ana_Dw
            // 
            this.txt_Ana_Dw.Location = new System.Drawing.Point(130, 14);
            this.txt_Ana_Dw.Name = "txt_Ana_Dw";
            this.txt_Ana_Dw.Size = new System.Drawing.Size(50, 21);
            this.txt_Ana_Dw.TabIndex = 0;
            this.txt_Ana_Dw.Text = "0.075";
            this.txt_Ana_Dw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Dw.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label521
            // 
            this.label521.AutoSize = true;
            this.label521.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label521.Location = new System.Drawing.Point(5, 17);
            this.label521.Name = "label521";
            this.label521.Size = new System.Drawing.Size(95, 13);
            this.label521.TabIndex = 0;
            this.label521.Text = "Thickness [Dw]";
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
            // label239
            // 
            this.label239.AutoSize = true;
            this.label239.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label239.Location = new System.Drawing.Point(25, 496);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(99, 13);
            this.label239.TabIndex = 131;
            this.label239.Text = "Live Load Factor";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(362, 437);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(30, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "T/m";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(362, 410);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "T/m";
            // 
            // txt_Ana_LL_factor
            // 
            this.txt_Ana_LL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_LL_factor.Location = new System.Drawing.Point(292, 493);
            this.txt_Ana_LL_factor.Name = "txt_Ana_LL_factor";
            this.txt_Ana_LL_factor.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_LL_factor.TabIndex = 130;
            this.txt_Ana_LL_factor.Text = "2.5";
            this.txt_Ana_LL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_LL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // label240
            // 
            this.label240.AutoSize = true;
            this.label240.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label240.Location = new System.Drawing.Point(25, 469);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(106, 13);
            this.label240.TabIndex = 129;
            this.label240.Text = "Dead Load Factor";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(25, 437);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(129, 13);
            this.label17.TabIndex = 54;
            this.label17.Text = "Applied Load for FPLL";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(25, 410);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 54;
            this.label10.Text = "Applied Load for SIDL";
            // 
            // txt_Ana_DL_factor
            // 
            this.txt_Ana_DL_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_DL_factor.Location = new System.Drawing.Point(292, 466);
            this.txt_Ana_DL_factor.Name = "txt_Ana_DL_factor";
            this.txt_Ana_DL_factor.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_factor.TabIndex = 127;
            this.txt_Ana_DL_factor.Text = "1.25";
            this.txt_Ana_DL_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_factor.TextChanged += new System.EventHandler(this.txt_Ana_DL_factor_TextChanged);
            // 
            // txt_FPLL
            // 
            this.txt_FPLL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FPLL.Location = new System.Drawing.Point(292, 434);
            this.txt_FPLL.Name = "txt_FPLL";
            this.txt_FPLL.Size = new System.Drawing.Size(64, 21);
            this.txt_FPLL.TabIndex = 3;
            this.txt_FPLL.Text = "0.98";
            this.txt_FPLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_FPLL.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_SIDL
            // 
            this.txt_SIDL.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SIDL.Location = new System.Drawing.Point(292, 407);
            this.txt_SIDL.Name = "txt_SIDL";
            this.txt_SIDL.Size = new System.Drawing.Size(64, 21);
            this.txt_SIDL.TabIndex = 3;
            this.txt_SIDL.Text = "2.5";
            this.txt_SIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_SIDL.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
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
            // grb_create_input_data
            // 
            this.grb_create_input_data.Controls.Add(this.label7);
            this.grb_create_input_data.Controls.Add(this.label8);
            this.grb_create_input_data.Controls.Add(this.txt_support_distance);
            this.grb_create_input_data.Controls.Add(this.label33);
            this.grb_create_input_data.Controls.Add(this.label9);
            this.grb_create_input_data.Controls.Add(this.label32);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_width_cantilever);
            this.grb_create_input_data.Controls.Add(this.label12);
            this.grb_create_input_data.Controls.Add(this.label5);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_DL_eff_depth);
            this.grb_create_input_data.Controls.Add(this.txt_carriageway_width);
            this.grb_create_input_data.Controls.Add(this.label6);
            this.grb_create_input_data.Controls.Add(this.label3);
            this.grb_create_input_data.Controls.Add(this.label13);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_B);
            this.grb_create_input_data.Controls.Add(this.label4);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_L);
            this.grb_create_input_data.Controls.Add(this.label14);
            this.grb_create_input_data.Controls.Add(this.label1);
            this.grb_create_input_data.Controls.Add(this.txt_exp_gap);
            this.grb_create_input_data.Controls.Add(this.txt_overhang_gap);
            this.grb_create_input_data.Controls.Add(this.label16);
            this.grb_create_input_data.Controls.Add(this.label15);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.Location = new System.Drawing.Point(15, 125);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(374, 264);
            this.grb_create_input_data.TabIndex = 1;
            this.grb_create_input_data.TabStop = false;
            this.grb_create_input_data.Text = "Analysis Input Data";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 182;
            this.label7.Text = "Carriageway Width";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 191;
            this.label8.Text = "m";
            // 
            // txt_support_distance
            // 
            this.txt_support_distance.ForeColor = System.Drawing.Color.Blue;
            this.txt_support_distance.Location = new System.Drawing.Point(277, 115);
            this.txt_support_distance.Name = "txt_support_distance";
            this.txt_support_distance.Size = new System.Drawing.Size(64, 21);
            this.txt_support_distance.TabIndex = 55;
            this.txt_support_distance.Text = "0.50";
            this.txt_support_distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(347, 235);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(18, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "m";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(347, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 192;
            this.label9.Text = "m";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(10, 235);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(146, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Width of Cantilever Slab";
            // 
            // txt_Ana_width_cantilever
            // 
            this.txt_Ana_width_cantilever.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_width_cantilever.Location = new System.Drawing.Point(277, 232);
            this.txt_Ana_width_cantilever.Name = "txt_Ana_width_cantilever";
            this.txt_Ana_width_cantilever.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_width_cantilever.TabIndex = 3;
            this.txt_Ana_width_cantilever.Text = "1.5";
            this.txt_Ana_width_cantilever.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_width_cantilever.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(347, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 193;
            this.label12.Text = "m";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(347, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "m";
            // 
            // txt_Ana_DL_eff_depth
            // 
            this.txt_Ana_DL_eff_depth.Location = new System.Drawing.Point(277, 205);
            this.txt_Ana_DL_eff_depth.Name = "txt_Ana_DL_eff_depth";
            this.txt_Ana_DL_eff_depth.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_DL_eff_depth.TabIndex = 2;
            this.txt_Ana_DL_eff_depth.Text = "2.50";
            this.txt_Ana_DL_eff_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_DL_eff_depth.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_carriageway_width
            // 
            this.txt_carriageway_width.Location = new System.Drawing.Point(277, 173);
            this.txt_carriageway_width.Name = "txt_carriageway_width";
            this.txt_carriageway_width.Size = new System.Drawing.Size(64, 21);
            this.txt_carriageway_width.TabIndex = 187;
            this.txt_carriageway_width.Text = "7.0";
            this.txt_carriageway_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_carriageway_width.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tentative Effective Depth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "m";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(347, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 185;
            this.label13.Text = "m";
            // 
            // txt_Ana_B
            // 
            this.txt_Ana_B.Location = new System.Drawing.Point(277, 146);
            this.txt_Ana_B.Name = "txt_Ana_B";
            this.txt_Ana_B.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_B.TabIndex = 1;
            this.txt_Ana_B.Text = "9.75";
            this.txt_Ana_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_B.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Width Along Z-direction [B]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "m";
            // 
            // txt_Ana_L
            // 
            this.txt_Ana_L.Location = new System.Drawing.Point(277, 24);
            this.txt_Ana_L.Name = "txt_Ana_L";
            this.txt_Ana_L.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_L.TabIndex = 0;
            this.txt_Ana_L.Text = "48.75";
            this.txt_Ana_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_L.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 110);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(211, 26);
            this.label14.TabIndex = 188;
            this.label14.Text = "Distance from Centre of Expansion \r\nGap to C.L. of Bearing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Length Along X-direction [L]";
            // 
            // txt_exp_gap
            // 
            this.txt_exp_gap.Location = new System.Drawing.Point(277, 51);
            this.txt_exp_gap.Name = "txt_exp_gap";
            this.txt_exp_gap.Size = new System.Drawing.Size(64, 21);
            this.txt_exp_gap.TabIndex = 183;
            this.txt_exp_gap.Text = " 0.040";
            this.txt_exp_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_exp_gap.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_overhang_gap
            // 
            this.txt_overhang_gap.Location = new System.Drawing.Point(277, 82);
            this.txt_overhang_gap.Name = "txt_overhang_gap";
            this.txt_overhang_gap.Size = new System.Drawing.Size(64, 21);
            this.txt_overhang_gap.TabIndex = 184;
            this.txt_overhang_gap.Text = "0.22";
            this.txt_overhang_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_overhang_gap.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 13);
            this.label16.TabIndex = 190;
            this.label16.Text = "Expansion Gap width";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(146, 26);
            this.label15.TabIndex = 189;
            this.label15.Text = "Overhang from C.L. of \r\nBearing to Edge of Deck";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(439, 466);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(437, 165);
            this.pictureBox1.TabIndex = 132;
            this.pictureBox1.TabStop = false;
            // 
            // tab_cs_diagram
            // 
            this.tab_cs_diagram.AutoScroll = true;
            this.tab_cs_diagram.Controls.Add(this.panel6);
            this.tab_cs_diagram.ForeColor = System.Drawing.Color.Blue;
            this.tab_cs_diagram.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram.Name = "tab_cs_diagram";
            this.tab_cs_diagram.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram.Size = new System.Drawing.Size(961, 634);
            this.tab_cs_diagram.TabIndex = 2;
            this.tab_cs_diagram.Text = "Cross Section Diagram";
            this.tab_cs_diagram.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.Controls.Add(this.groupBox26);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.groupBox32);
            this.panel6.Controls.Add(this.btn_Show_Section_Resulf);
            this.panel6.Controls.Add(this.rtb_sections);
            this.panel6.Controls.Add(this.label176);
            this.panel6.Controls.Add(this.label226);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(955, 628);
            this.panel6.TabIndex = 128;
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.pictureBox4);
            this.groupBox26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox26.Location = new System.Drawing.Point(117, 17);
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
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.txt_out_IZZ);
            this.panel8.Controls.Add(this.txt_inn_IZZ);
            this.panel8.Controls.Add(this.txt_cen_IZZ);
            this.panel8.Controls.Add(this.label444);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.txt_tot_IZZ);
            this.panel8.Controls.Add(this.label350);
            this.panel8.Controls.Add(this.txt_out_IYY);
            this.panel8.Controls.Add(this.txt_inn_IYY);
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label443);
            this.panel8.Controls.Add(this.txt_cen_IYY);
            this.panel8.Controls.Add(this.label353);
            this.panel8.Controls.Add(this.txt_tot_IYY);
            this.panel8.Controls.Add(this.txt_out_IXX);
            this.panel8.Controls.Add(this.label349);
            this.panel8.Controls.Add(this.txt_inn_IXX);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.label59);
            this.panel8.Controls.Add(this.txt_cen_IXX);
            this.panel8.Controls.Add(this.label352);
            this.panel8.Controls.Add(this.txt_tot_IXX);
            this.panel8.Controls.Add(this.txt_out_pcnt);
            this.panel8.Controls.Add(this.label348);
            this.panel8.Controls.Add(this.txt_inn_pcnt);
            this.panel8.Controls.Add(this.txt_out_AX);
            this.panel8.Controls.Add(this.label60);
            this.panel8.Controls.Add(this.txt_inn_AX);
            this.panel8.Controls.Add(this.txt_cen_pcnt);
            this.panel8.Controls.Add(this.label75);
            this.panel8.Controls.Add(this.txt_cen_AX);
            this.panel8.Controls.Add(this.label339);
            this.panel8.Controls.Add(this.label149);
            this.panel8.Controls.Add(this.txt_tot_AX);
            this.panel8.Controls.Add(this.label271);
            this.panel8.Controls.Add(this.label338);
            this.panel8.Controls.Add(this.label351);
            this.panel8.Controls.Add(this.label505);
            this.panel8.Controls.Add(this.label502);
            this.panel8.Controls.Add(this.label501);
            this.panel8.Controls.Add(this.label336);
            this.panel8.Controls.Add(this.label272);
            this.panel8.Controls.Add(this.label347);
            this.panel8.Controls.Add(this.label342);
            this.panel8.Controls.Add(this.label273);
            this.panel8.Controls.Add(this.label274);
            this.panel8.Controls.Add(this.label335);
            this.panel8.Controls.Add(this.label341);
            this.panel8.Controls.Add(this.label346);
            this.panel8.Controls.Add(this.label275);
            this.panel8.Controls.Add(this.label302);
            this.panel8.Controls.Add(this.label340);
            this.panel8.Controls.Add(this.label345);
            this.panel8.Controls.Add(this.label305);
            this.panel8.Controls.Add(this.label306);
            this.panel8.Controls.Add(this.label337);
            this.panel8.Controls.Add(this.label344);
            this.panel8.Controls.Add(this.label307);
            this.panel8.Controls.Add(this.label343);
            this.panel8.Controls.Add(this.label308);
            this.panel8.Controls.Add(this.label498);
            this.panel8.Controls.Add(this.label309);
            this.panel8.ForeColor = System.Drawing.Color.Black;
            this.panel8.Location = new System.Drawing.Point(26, 1121);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(886, 359);
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
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(665, 217);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(28, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "IZZ";
            // 
            // txt_tot_IZZ
            // 
            this.txt_tot_IZZ.Location = new System.Drawing.Point(699, 63);
            this.txt_tot_IZZ.Name = "txt_tot_IZZ";
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
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(665, 66);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "IZZ";
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
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(459, 66);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "IYY";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(257, 296);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(28, 13);
            this.label59.TabIndex = 1;
            this.label59.Text = "IXX";
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
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(257, 66);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(28, 13);
            this.label60.TabIndex = 1;
            this.label60.Text = "IXX";
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
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(540, 261);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(19, 13);
            this.label75.TabIndex = 1;
            this.label75.Text = "%";
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
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(4, 261);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(418, 18);
            this.label149.TabIndex = 0;
            this.label149.Text = "For Two Outer Member of Lumped Mass Model";
            // 
            // txt_tot_AX
            // 
            this.txt_tot_AX.Location = new System.Drawing.Point(97, 63);
            this.txt_tot_AX.Name = "txt_tot_AX";
            this.txt_tot_AX.Size = new System.Drawing.Size(73, 21);
            this.txt_tot_AX.TabIndex = 2;
            this.txt_tot_AX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tot_AX.TextChanged += new System.EventHandler(this.txt_tot_AX_TextChanged);
            // 
            // label271
            // 
            this.label271.AutoSize = true;
            this.label271.Location = new System.Drawing.Point(789, 296);
            this.label271.Name = "label271";
            this.label271.Size = new System.Drawing.Size(52, 13);
            this.label271.TabIndex = 1;
            this.label271.Text = "sq.sq.m";
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
            this.label351.Size = new System.Drawing.Size(52, 13);
            this.label351.TabIndex = 1;
            this.label351.Text = "sq.sq.m";
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
            // label272
            // 
            this.label272.AutoSize = true;
            this.label272.Location = new System.Drawing.Point(578, 296);
            this.label272.Name = "label272";
            this.label272.Size = new System.Drawing.Size(52, 13);
            this.label272.TabIndex = 1;
            this.label272.Text = "sq.sq.m";
            // 
            // label347
            // 
            this.label347.AutoSize = true;
            this.label347.Location = new System.Drawing.Point(789, 139);
            this.label347.Name = "label347";
            this.label347.Size = new System.Drawing.Size(52, 13);
            this.label347.TabIndex = 1;
            this.label347.Text = "sq.sq.m";
            // 
            // label342
            // 
            this.label342.AutoSize = true;
            this.label342.Location = new System.Drawing.Point(578, 217);
            this.label342.Name = "label342";
            this.label342.Size = new System.Drawing.Size(52, 13);
            this.label342.TabIndex = 1;
            this.label342.Text = "sq.sq.m";
            // 
            // label273
            // 
            this.label273.AutoSize = true;
            this.label273.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label273.Location = new System.Drawing.Point(4, 107);
            this.label273.Name = "label273";
            this.label273.Size = new System.Drawing.Size(428, 18);
            this.label273.TabIndex = 0;
            this.label273.Text = "For One Central Member of Lumped Mass Model";
            // 
            // label274
            // 
            this.label274.AutoSize = true;
            this.label274.Location = new System.Drawing.Point(381, 296);
            this.label274.Name = "label274";
            this.label274.Size = new System.Drawing.Size(52, 13);
            this.label274.TabIndex = 1;
            this.label274.Text = "sq.sq.m";
            // 
            // label335
            // 
            this.label335.AutoSize = true;
            this.label335.Location = new System.Drawing.Point(578, 139);
            this.label335.Name = "label335";
            this.label335.Size = new System.Drawing.Size(52, 13);
            this.label335.TabIndex = 1;
            this.label335.Text = "sq.sq.m";
            // 
            // label341
            // 
            this.label341.AutoSize = true;
            this.label341.Location = new System.Drawing.Point(381, 217);
            this.label341.Name = "label341";
            this.label341.Size = new System.Drawing.Size(52, 13);
            this.label341.TabIndex = 1;
            this.label341.Text = "sq.sq.m";
            // 
            // label346
            // 
            this.label346.AutoSize = true;
            this.label346.Location = new System.Drawing.Point(789, 66);
            this.label346.Name = "label346";
            this.label346.Size = new System.Drawing.Size(52, 13);
            this.label346.TabIndex = 1;
            this.label346.Text = "sq.sq.m";
            // 
            // label275
            // 
            this.label275.AutoSize = true;
            this.label275.Location = new System.Drawing.Point(176, 296);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(35, 13);
            this.label275.TabIndex = 1;
            this.label275.Text = "sq.m";
            // 
            // label302
            // 
            this.label302.AutoSize = true;
            this.label302.Location = new System.Drawing.Point(381, 139);
            this.label302.Name = "label302";
            this.label302.Size = new System.Drawing.Size(52, 13);
            this.label302.TabIndex = 1;
            this.label302.Text = "sq.sq.m";
            // 
            // label340
            // 
            this.label340.AutoSize = true;
            this.label340.Location = new System.Drawing.Point(176, 217);
            this.label340.Name = "label340";
            this.label340.Size = new System.Drawing.Size(35, 13);
            this.label340.TabIndex = 1;
            this.label340.Text = "sq.m";
            // 
            // label345
            // 
            this.label345.AutoSize = true;
            this.label345.Location = new System.Drawing.Point(578, 66);
            this.label345.Name = "label345";
            this.label345.Size = new System.Drawing.Size(52, 13);
            this.label345.TabIndex = 1;
            this.label345.Text = "sq.sq.m";
            // 
            // label305
            // 
            this.label305.AutoSize = true;
            this.label305.Location = new System.Drawing.Point(59, 296);
            this.label305.Name = "label305";
            this.label305.Size = new System.Drawing.Size(23, 13);
            this.label305.TabIndex = 1;
            this.label305.Text = "AX";
            // 
            // label306
            // 
            this.label306.AutoSize = true;
            this.label306.Location = new System.Drawing.Point(176, 139);
            this.label306.Name = "label306";
            this.label306.Size = new System.Drawing.Size(35, 13);
            this.label306.TabIndex = 1;
            this.label306.Text = "sq.m";
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
            this.label344.Size = new System.Drawing.Size(52, 13);
            this.label344.TabIndex = 1;
            this.label344.Text = "sq.sq.m";
            // 
            // label307
            // 
            this.label307.AutoSize = true;
            this.label307.Location = new System.Drawing.Point(59, 139);
            this.label307.Name = "label307";
            this.label307.Size = new System.Drawing.Size(23, 13);
            this.label307.TabIndex = 1;
            this.label307.Text = "AX";
            // 
            // label343
            // 
            this.label343.AutoSize = true;
            this.label343.Location = new System.Drawing.Point(176, 66);
            this.label343.Name = "label343";
            this.label343.Size = new System.Drawing.Size(35, 13);
            this.label343.TabIndex = 1;
            this.label343.Text = "sq.m";
            // 
            // label308
            // 
            this.label308.AutoSize = true;
            this.label308.Location = new System.Drawing.Point(59, 66);
            this.label308.Name = "label308";
            this.label308.Size = new System.Drawing.Size(23, 13);
            this.label308.TabIndex = 1;
            this.label308.Text = "AX";
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
            // label309
            // 
            this.label309.AutoSize = true;
            this.label309.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label309.Location = new System.Drawing.Point(4, 10);
            this.label309.Name = "label309";
            this.label309.Size = new System.Drawing.Size(378, 18);
            this.label309.TabIndex = 0;
            this.label309.Text = "Average Section Properties for Box Girder";
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
            this.groupBox32.Location = new System.Drawing.Point(199, 347);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(541, 465);
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
            // btn_Show_Section_Resulf
            // 
            this.btn_Show_Section_Resulf.Location = new System.Drawing.Point(576, 821);
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
            this.rtb_sections.Location = new System.Drawing.Point(26, 859);
            this.rtb_sections.Name = "rtb_sections";
            this.rtb_sections.ReadOnly = true;
            this.rtb_sections.Size = new System.Drawing.Size(886, 256);
            this.rtb_sections.TabIndex = 3;
            this.rtb_sections.Text = "";
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label176.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.Red;
            this.label176.Location = new System.Drawing.Point(374, 827);
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
            this.label226.Location = new System.Drawing.Point(132, 827);
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
            this.tab_moving_data_british.Size = new System.Drawing.Size(961, 634);
            this.tab_moving_data_british.TabIndex = 3;
            this.tab_moving_data_british.Text = "Moving Load Data [BS]";
            this.tab_moving_data_british.UseVisualStyleBackColor = true;
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.label557);
            this.groupBox45.Controls.Add(this.label558);
            this.groupBox45.Controls.Add(this.textBox22);
            this.groupBox45.Controls.Add(this.label559);
            this.groupBox45.Controls.Add(this.label561);
            this.groupBox45.Controls.Add(this.btn_bs_view_moving_load);
            this.groupBox45.Controls.Add(this.label1190);
            this.groupBox45.Controls.Add(this.cmb_bs_view_moving_load);
            this.groupBox45.Controls.Add(this.txt_bs_vehicle_gap);
            this.groupBox45.Controls.Add(this.txt_ll_british_lgen);
            this.groupBox45.Controls.Add(this.label1191);
            this.groupBox45.Controls.Add(this.spc_HB);
            this.groupBox45.Controls.Add(this.lbl_HB);
            this.groupBox45.Controls.Add(this.txt_ll_british_incr);
            this.groupBox45.Controls.Add(this.groupBox107);
            this.groupBox45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox45.Location = new System.Drawing.Point(3, 3);
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.Size = new System.Drawing.Size(955, 628);
            this.groupBox45.TabIndex = 86;
            this.groupBox45.TabStop = false;
            // 
            // label557
            // 
            this.label557.AutoSize = true;
            this.label557.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label557.Location = new System.Drawing.Point(711, 605);
            this.label557.Name = "label557";
            this.label557.Size = new System.Drawing.Size(170, 13);
            this.label557.TabIndex = 125;
            this.label557.Text = "DL + LL Combine Load No";
            // 
            // label558
            // 
            this.label558.AutoSize = true;
            this.label558.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label558.Location = new System.Drawing.Point(767, 583);
            this.label558.Name = "label558";
            this.label558.Size = new System.Drawing.Size(114, 13);
            this.label558.TabIndex = 127;
            this.label558.Text = "Load Generation";
            // 
            // textBox22
            // 
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox22.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox22.Location = new System.Drawing.Point(887, 604);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(39, 18);
            this.textBox22.TabIndex = 124;
            this.textBox22.Text = "1";
            this.textBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label559
            // 
            this.label559.AutoSize = true;
            this.label559.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label559.Location = new System.Drawing.Point(647, 583);
            this.label559.Name = "label559";
            this.label559.Size = new System.Drawing.Size(50, 13);
            this.label559.TabIndex = 126;
            this.label559.Text = "X INCR";
            // 
            // label561
            // 
            this.label561.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label561.ForeColor = System.Drawing.Color.Red;
            this.label561.Location = new System.Drawing.Point(0, 592);
            this.label561.Name = "label561";
            this.label561.Size = new System.Drawing.Size(614, 33);
            this.label561.TabIndex = 123;
            // 
            // btn_bs_view_moving_load
            // 
            this.btn_bs_view_moving_load.Location = new System.Drawing.Point(86, 549);
            this.btn_bs_view_moving_load.Name = "btn_bs_view_moving_load";
            this.btn_bs_view_moving_load.Size = new System.Drawing.Size(204, 29);
            this.btn_bs_view_moving_load.TabIndex = 122;
            this.btn_bs_view_moving_load.Text = "View Moving Load";
            this.btn_bs_view_moving_load.UseVisualStyleBackColor = true;
            this.btn_bs_view_moving_load.Click += new System.EventHandler(this.btn_bs_view_moving_load_Click);
            // 
            // label1190
            // 
            this.label1190.AutoSize = true;
            this.label1190.Location = new System.Drawing.Point(48, 528);
            this.label1190.Name = "label1190";
            this.label1190.Size = new System.Drawing.Size(166, 13);
            this.label1190.TabIndex = 121;
            this.label1190.Text = "Select to view Moving Load ";
            // 
            // cmb_bs_view_moving_load
            // 
            this.cmb_bs_view_moving_load.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bs_view_moving_load.FormattingEnabled = true;
            this.cmb_bs_view_moving_load.Location = new System.Drawing.Point(228, 525);
            this.cmb_bs_view_moving_load.Name = "cmb_bs_view_moving_load";
            this.cmb_bs_view_moving_load.Size = new System.Drawing.Size(84, 21);
            this.cmb_bs_view_moving_load.TabIndex = 120;
            // 
            // txt_bs_vehicle_gap
            // 
            this.txt_bs_vehicle_gap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_bs_vehicle_gap.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bs_vehicle_gap.Location = new System.Drawing.Point(772, 527);
            this.txt_bs_vehicle_gap.Name = "txt_bs_vehicle_gap";
            this.txt_bs_vehicle_gap.Size = new System.Drawing.Size(65, 18);
            this.txt_bs_vehicle_gap.TabIndex = 118;
            this.txt_bs_vehicle_gap.Text = "18.8";
            this.txt_bs_vehicle_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ll_british_lgen
            // 
            this.txt_ll_british_lgen.Enabled = false;
            this.txt_ll_british_lgen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ll_british_lgen.Location = new System.Drawing.Point(887, 577);
            this.txt_ll_british_lgen.Name = "txt_ll_british_lgen";
            this.txt_ll_british_lgen.Size = new System.Drawing.Size(39, 21);
            this.txt_ll_british_lgen.TabIndex = 79;
            this.txt_ll_british_lgen.Text = "191";
            this.txt_ll_british_lgen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ll_british_lgen.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // label1191
            // 
            this.label1191.AutoSize = true;
            this.label1191.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1191.Location = new System.Drawing.Point(345, 528);
            this.label1191.Name = "label1191";
            this.label1191.Size = new System.Drawing.Size(421, 13);
            this.label1191.TabIndex = 119;
            this.label1191.Text = "Longitudinal Separating distance between two vehicle in a Lane";
            // 
            // spc_HB
            // 
            this.spc_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_HB.Dock = System.Windows.Forms.DockStyle.Top;
            this.spc_HB.Location = new System.Drawing.Point(3, 274);
            this.spc_HB.Name = "spc_HB";
            // 
            // spc_HB.Panel1
            // 
            this.spc_HB.Panel1.Controls.Add(this.groupBox106);
            // 
            // spc_HB.Panel2
            // 
            this.spc_HB.Panel2.Controls.Add(this.groupBox105);
            this.spc_HB.Size = new System.Drawing.Size(949, 246);
            this.spc_HB.SplitterDistance = 414;
            this.spc_HB.TabIndex = 83;
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
            this.groupBox106.Size = new System.Drawing.Size(412, 244);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_british_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_british_loads.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_british_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_british_loads.Location = new System.Drawing.Point(3, 51);
            this.dgv_british_loads.Name = "dgv_british_loads";
            this.dgv_british_loads.RowHeadersWidth = 21;
            this.dgv_british_loads.Size = new System.Drawing.Size(406, 190);
            this.dgv_british_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn77
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn77.DefaultCellStyle = dataGridViewCellStyle4;
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
            // groupBox105
            // 
            this.groupBox105.Controls.Add(this.dgv_long_british_loads);
            this.groupBox105.Controls.Add(this.label247);
            this.groupBox105.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox105.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox105.ForeColor = System.Drawing.Color.Black;
            this.groupBox105.Location = new System.Drawing.Point(0, 0);
            this.groupBox105.Name = "groupBox105";
            this.groupBox105.Size = new System.Drawing.Size(529, 244);
            this.groupBox105.TabIndex = 7;
            this.groupBox105.TabStop = false;
            this.groupBox105.Text = "Define Vehicle Axle Loads";
            // 
            // dgv_long_british_loads
            // 
            this.dgv_long_british_loads.AllowUserToAddRows = false;
            this.dgv_long_british_loads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_long_british_loads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_long_british_loads.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_long_british_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_long_british_loads.Location = new System.Drawing.Point(3, 51);
            this.dgv_long_british_loads.Name = "dgv_long_british_loads";
            this.dgv_long_british_loads.RowHeadersWidth = 21;
            this.dgv_long_british_loads.Size = new System.Drawing.Size(523, 190);
            this.dgv_long_british_loads.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn55
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn55.DefaultCellStyle = dataGridViewCellStyle7;
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
            this.label247.Size = new System.Drawing.Size(523, 34);
            this.label247.TabIndex = 10;
            this.label247.Text = "(USER MAY CHANGE THE VAUES IN THE CELLS, THE DATA WILL BE SAVED IN FILE \"LL.TXT\" " +
    "FOR USE)";
            this.label247.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_HB
            // 
            this.lbl_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HB.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_HB.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_HB.Location = new System.Drawing.Point(3, 246);
            this.lbl_HB.Name = "lbl_HB";
            this.lbl_HB.Size = new System.Drawing.Size(949, 28);
            this.lbl_HB.TabIndex = 84;
            this.lbl_HB.Text = "HB LOADING";
            this.lbl_HB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_ll_british_incr
            // 
            this.txt_ll_british_incr.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ll_british_incr.Location = new System.Drawing.Point(702, 581);
            this.txt_ll_british_incr.Name = "txt_ll_british_incr";
            this.txt_ll_british_incr.Size = new System.Drawing.Size(59, 18);
            this.txt_ll_british_incr.TabIndex = 58;
            this.txt_ll_british_incr.Text = "0.5";
            this.txt_ll_british_incr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ll_british_incr.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // groupBox107
            // 
            this.groupBox107.Controls.Add(this.groupBox108);
            this.groupBox107.Controls.Add(this.txt_LL_lf);
            this.groupBox107.Controls.Add(this.txt_LL_impf);
            this.groupBox107.Controls.Add(this.txt_no_lanes);
            this.groupBox107.Controls.Add(this.grb_ha);
            this.groupBox107.Controls.Add(this.grb_hb);
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
            this.groupBox107.Size = new System.Drawing.Size(949, 229);
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
            this.txt_LL_lf.Location = new System.Drawing.Point(177, 176);
            this.txt_LL_lf.Name = "txt_LL_lf";
            this.txt_LL_lf.Size = new System.Drawing.Size(59, 21);
            this.txt_LL_lf.TabIndex = 3;
            this.txt_LL_lf.Text = "1.0";
            this.txt_LL_lf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LL_lf.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // txt_LL_impf
            // 
            this.txt_LL_impf.Location = new System.Drawing.Point(177, 146);
            this.txt_LL_impf.Name = "txt_LL_impf";
            this.txt_LL_impf.Size = new System.Drawing.Size(59, 21);
            this.txt_LL_impf.TabIndex = 3;
            this.txt_LL_impf.Text = "1.0";
            this.txt_LL_impf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_LL_impf.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
            // 
            // txt_no_lanes
            // 
            this.txt_no_lanes.Enabled = false;
            this.txt_no_lanes.Location = new System.Drawing.Point(177, 107);
            this.txt_no_lanes.Name = "txt_no_lanes";
            this.txt_no_lanes.Size = new System.Drawing.Size(59, 21);
            this.txt_no_lanes.TabIndex = 3;
            this.txt_no_lanes.Text = "3";
            this.txt_no_lanes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_no_lanes.TextChanged += new System.EventHandler(this.txt_deck_width_TextChanged);
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
            // txt_lane_width
            // 
            this.txt_lane_width.Enabled = false;
            this.txt_lane_width.Location = new System.Drawing.Point(177, 72);
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
            this.label262.Location = new System.Drawing.Point(27, 179);
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
            this.label263.Location = new System.Drawing.Point(27, 149);
            this.label263.Name = "label263";
            this.label263.Size = new System.Drawing.Size(85, 13);
            this.label263.TabIndex = 1;
            this.label263.Text = "Impact Factor";
            // 
            // label264
            // 
            this.label264.AutoSize = true;
            this.label264.Location = new System.Drawing.Point(27, 108);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(109, 13);
            this.label264.TabIndex = 1;
            this.label264.Text = "Total No. of Lanes";
            // 
            // label265
            // 
            this.label265.AutoSize = true;
            this.label265.Location = new System.Drawing.Point(27, 73);
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
            this.tab_moving_data_indian.Controls.Add(this.btn_irc_view_moving_load);
            this.tab_moving_data_indian.Controls.Add(this.label568);
            this.tab_moving_data_indian.Controls.Add(this.cmb_irc_view_moving_load);
            this.tab_moving_data_indian.Controls.Add(this.txt_irc_vehicle_gap);
            this.tab_moving_data_indian.Controls.Add(this.label569);
            this.tab_moving_data_indian.Controls.Add(this.label249);
            this.tab_moving_data_indian.Controls.Add(this.txt_dl_ll_comb);
            this.tab_moving_data_indian.Controls.Add(this.btn_edit_load_combs);
            this.tab_moving_data_indian.Controls.Add(this.chk_self_indian);
            this.tab_moving_data_indian.Controls.Add(this.btn_long_restore_ll_IRC);
            this.tab_moving_data_indian.Controls.Add(this.groupBox31);
            this.tab_moving_data_indian.Controls.Add(this.label301);
            this.tab_moving_data_indian.Controls.Add(this.groupBox46);
            this.tab_moving_data_indian.Controls.Add(this.label304);
            this.tab_moving_data_indian.Controls.Add(this.txt_IRC_LL_load_gen);
            this.tab_moving_data_indian.Controls.Add(this.txt_IRC_XINCR);
            this.tab_moving_data_indian.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_data_indian.Name = "tab_moving_data_indian";
            this.tab_moving_data_indian.Padding = new System.Windows.Forms.Padding(3);
            this.tab_moving_data_indian.Size = new System.Drawing.Size(961, 634);
            this.tab_moving_data_indian.TabIndex = 4;
            this.tab_moving_data_indian.Text = "Moving Load Data [IRC]";
            this.tab_moving_data_indian.UseVisualStyleBackColor = true;
            // 
            // btn_irc_view_moving_load
            // 
            this.btn_irc_view_moving_load.Location = new System.Drawing.Point(59, 507);
            this.btn_irc_view_moving_load.Name = "btn_irc_view_moving_load";
            this.btn_irc_view_moving_load.Size = new System.Drawing.Size(204, 29);
            this.btn_irc_view_moving_load.TabIndex = 141;
            this.btn_irc_view_moving_load.Text = "View Moving Load";
            this.btn_irc_view_moving_load.UseVisualStyleBackColor = true;
            this.btn_irc_view_moving_load.Click += new System.EventHandler(this.btn_irc_view_moving_load_Click);
            // 
            // label568
            // 
            this.label568.AutoSize = true;
            this.label568.Location = new System.Drawing.Point(21, 486);
            this.label568.Name = "label568";
            this.label568.Size = new System.Drawing.Size(166, 13);
            this.label568.TabIndex = 140;
            this.label568.Text = "Select to view Moving Load ";
            // 
            // cmb_irc_view_moving_load
            // 
            this.cmb_irc_view_moving_load.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_irc_view_moving_load.FormattingEnabled = true;
            this.cmb_irc_view_moving_load.Location = new System.Drawing.Point(201, 483);
            this.cmb_irc_view_moving_load.Name = "cmb_irc_view_moving_load";
            this.cmb_irc_view_moving_load.Size = new System.Drawing.Size(84, 21);
            this.cmb_irc_view_moving_load.TabIndex = 139;
            // 
            // txt_irc_vehicle_gap
            // 
            this.txt_irc_vehicle_gap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_irc_vehicle_gap.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_irc_vehicle_gap.Location = new System.Drawing.Point(745, 485);
            this.txt_irc_vehicle_gap.Name = "txt_irc_vehicle_gap";
            this.txt_irc_vehicle_gap.Size = new System.Drawing.Size(65, 18);
            this.txt_irc_vehicle_gap.TabIndex = 137;
            this.txt_irc_vehicle_gap.Text = "18.8";
            this.txt_irc_vehicle_gap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label569
            // 
            this.label569.AutoSize = true;
            this.label569.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label569.Location = new System.Drawing.Point(318, 486);
            this.label569.Name = "label569";
            this.label569.Size = new System.Drawing.Size(421, 13);
            this.label569.TabIndex = 138;
            this.label569.Text = "Longitudinal Separating distance between two vehicle in a Lane";
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label249.Location = new System.Drawing.Point(703, 569);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(170, 13);
            this.label249.TabIndex = 136;
            this.label249.Text = "DL + LL Combine Load No";
            // 
            // txt_dl_ll_comb
            // 
            this.txt_dl_ll_comb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dl_ll_comb.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dl_ll_comb.Location = new System.Drawing.Point(885, 569);
            this.txt_dl_ll_comb.Name = "txt_dl_ll_comb";
            this.txt_dl_ll_comb.Size = new System.Drawing.Size(39, 18);
            this.txt_dl_ll_comb.TabIndex = 135;
            this.txt_dl_ll_comb.Text = "1";
            this.txt_dl_ll_comb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_edit_load_combs
            // 
            this.btn_edit_load_combs.Location = new System.Drawing.Point(225, 542);
            this.btn_edit_load_combs.Name = "btn_edit_load_combs";
            this.btn_edit_load_combs.Size = new System.Drawing.Size(157, 29);
            this.btn_edit_load_combs.TabIndex = 134;
            this.btn_edit_load_combs.Text = "Edit Load Combinations";
            this.btn_edit_load_combs.UseVisualStyleBackColor = true;
            this.btn_edit_load_combs.Click += new System.EventHandler(this.btn_edit_load_combs_Click);
            // 
            // chk_self_indian
            // 
            this.chk_self_indian.AutoSize = true;
            this.chk_self_indian.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_self_indian.Location = new System.Drawing.Point(36, 547);
            this.chk_self_indian.Name = "chk_self_indian";
            this.chk_self_indian.Size = new System.Drawing.Size(165, 20);
            this.chk_self_indian.TabIndex = 133;
            this.chk_self_indian.Text = "Apply SELFWEIGHT";
            this.chk_self_indian.UseVisualStyleBackColor = true;
            this.chk_self_indian.Visible = false;
            // 
            // btn_long_restore_ll_IRC
            // 
            this.btn_long_restore_ll_IRC.Location = new System.Drawing.Point(417, 542);
            this.btn_long_restore_ll_IRC.Name = "btn_long_restore_ll_IRC";
            this.btn_long_restore_ll_IRC.Size = new System.Drawing.Size(167, 29);
            this.btn_long_restore_ll_IRC.TabIndex = 127;
            this.btn_long_restore_ll_IRC.Text = "Restore Default Values";
            this.btn_long_restore_ll_IRC.UseVisualStyleBackColor = true;
            this.btn_long_restore_ll_IRC.Click += new System.EventHandler(this.btn_long_restore_ll_IRC_Click);
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.label300);
            this.groupBox31.Controls.Add(this.dgv_long_loads);
            this.groupBox31.ForeColor = System.Drawing.Color.Black;
            this.groupBox31.Location = new System.Drawing.Point(7, 6);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(269, 477);
            this.groupBox31.TabIndex = 128;
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
            this.dgv_long_loads.Size = new System.Drawing.Size(263, 441);
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
            this.label301.Location = new System.Drawing.Point(765, 545);
            this.label301.Name = "label301";
            this.label301.Size = new System.Drawing.Size(114, 13);
            this.label301.TabIndex = 132;
            this.label301.Text = "Load Generation";
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label303);
            this.groupBox46.Controls.Add(this.dgv_long_liveloads);
            this.groupBox46.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox46.ForeColor = System.Drawing.Color.Black;
            this.groupBox46.Location = new System.Drawing.Point(280, 6);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(664, 477);
            this.groupBox46.TabIndex = 126;
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
            this.dgv_long_liveloads.Size = new System.Drawing.Size(658, 441);
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
            this.label304.Location = new System.Drawing.Point(650, 545);
            this.label304.Name = "label304";
            this.label304.Size = new System.Drawing.Size(50, 13);
            this.label304.TabIndex = 130;
            this.label304.Text = "X INCR";
            // 
            // txt_IRC_LL_load_gen
            // 
            this.txt_IRC_LL_load_gen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IRC_LL_load_gen.Location = new System.Drawing.Point(885, 542);
            this.txt_IRC_LL_load_gen.Name = "txt_IRC_LL_load_gen";
            this.txt_IRC_LL_load_gen.Size = new System.Drawing.Size(39, 21);
            this.txt_IRC_LL_load_gen.TabIndex = 131;
            this.txt_IRC_LL_load_gen.Text = "52";
            this.txt_IRC_LL_load_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_IRC_XINCR
            // 
            this.txt_IRC_XINCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_IRC_XINCR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IRC_XINCR.Location = new System.Drawing.Point(706, 544);
            this.txt_IRC_XINCR.Name = "txt_IRC_XINCR";
            this.txt_IRC_XINCR.Size = new System.Drawing.Size(37, 18);
            this.txt_IRC_XINCR.TabIndex = 129;
            this.txt_IRC_XINCR.Text = "5.0";
            this.txt_IRC_XINCR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_IRC_XINCR.TextChanged += new System.EventHandler(this.txt_IRC_XINCR_TextChanged);
            // 
            // tab_analysis
            // 
            this.tab_analysis.Controls.Add(this.splitContainer1);
            this.tab_analysis.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis.Name = "tab_analysis";
            this.tab_analysis.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis.Size = new System.Drawing.Size(961, 634);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uC_Res_Normal);
            this.splitContainer1.Size = new System.Drawing.Size(955, 628);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 104;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_selfweight);
            this.groupBox2.Controls.Add(this.groupBox51);
            this.groupBox2.Controls.Add(this.groupBox70);
            this.groupBox2.Controls.Add(this.groupBox109);
            this.groupBox2.Controls.Add(this.btn_Process_LL_Analysis);
            this.groupBox2.Controls.Add(this.btn_Ana_DL_create_data);
            this.groupBox2.Controls.Add(this.groupBox71);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(953, 309);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            // 
            // chk_selfweight
            // 
            this.chk_selfweight.AutoSize = true;
            this.chk_selfweight.Checked = true;
            this.chk_selfweight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_selfweight.Location = new System.Drawing.Point(22, 89);
            this.chk_selfweight.Name = "chk_selfweight";
            this.chk_selfweight.Size = new System.Drawing.Size(130, 17);
            this.chk_selfweight.TabIndex = 141;
            this.chk_selfweight.Text = "ADD SELFWEIGHT";
            this.chk_selfweight.UseVisualStyleBackColor = true;
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
            this.groupBox51.Location = new System.Drawing.Point(7, 20);
            this.groupBox51.Name = "groupBox51";
            this.groupBox51.Size = new System.Drawing.Size(478, 52);
            this.groupBox51.TabIndex = 138;
            this.groupBox51.TabStop = false;
            this.groupBox51.Text = "CONCRETE Material Constants";
            // 
            // label330
            // 
            this.label330.AutoSize = true;
            this.label330.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label330.ForeColor = System.Drawing.Color.Black;
            this.label330.Location = new System.Drawing.Point(390, 18);
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
            this.label332.Size = new System.Drawing.Size(56, 16);
            this.label332.TabIndex = 100;
            this.label332.Text = "T/Cu.m";
            // 
            // label333
            // 
            this.label333.AutoSize = true;
            this.label333.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label333.ForeColor = System.Drawing.Color.Black;
            this.label333.Location = new System.Drawing.Point(145, 18);
            this.label333.Name = "label333";
            this.label333.Size = new System.Drawing.Size(56, 16);
            this.label333.TabIndex = 100;
            this.label333.Text = "T/Sq.m";
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
            this.txt_PR_conc.Location = new System.Drawing.Point(420, 17);
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
            this.txt_den_conc.Text = "2.5";
            this.txt_den_conc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_emod_conc
            // 
            this.txt_emod_conc.Location = new System.Drawing.Point(66, 17);
            this.txt_emod_conc.Name = "txt_emod_conc";
            this.txt_emod_conc.Size = new System.Drawing.Size(76, 21);
            this.txt_emod_conc.TabIndex = 108;
            this.txt_emod_conc.Text = "2110000";
            this.txt_emod_conc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.groupBox70.Location = new System.Drawing.Point(498, 76);
            this.groupBox70.Name = "groupBox70";
            this.groupBox70.Size = new System.Drawing.Size(431, 41);
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
            this.rbtn_esprt_fixed.Location = new System.Drawing.Point(79, 14);
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
            this.chk_esprt_fixed_MZ.Location = new System.Drawing.Point(367, 15);
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
            this.chk_esprt_fixed_FZ.Location = new System.Drawing.Point(235, 14);
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
            this.chk_esprt_fixed_MY.Location = new System.Drawing.Point(325, 14);
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
            this.chk_esprt_fixed_FY.Location = new System.Drawing.Point(191, 14);
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
            this.chk_esprt_fixed_MX.Location = new System.Drawing.Point(276, 14);
            this.chk_esprt_fixed_MX.Name = "chk_esprt_fixed_MX";
            this.chk_esprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MX.TabIndex = 0;
            this.chk_esprt_fixed_MX.Text = "MX";
            this.chk_esprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FX
            // 
            this.chk_esprt_fixed_FX.AutoSize = true;
            this.chk_esprt_fixed_FX.Location = new System.Drawing.Point(145, 14);
            this.chk_esprt_fixed_FX.Name = "chk_esprt_fixed_FX";
            this.chk_esprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FX.TabIndex = 0;
            this.chk_esprt_fixed_FX.Text = "FX";
            this.chk_esprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // groupBox109
            // 
            this.groupBox109.Controls.Add(this.cmb_long_open_file);
            this.groupBox109.Controls.Add(this.btn_View_Post_Process);
            this.groupBox109.Controls.Add(this.btn_view_report);
            this.groupBox109.Controls.Add(this.btn_view_data);
            this.groupBox109.Controls.Add(this.btn_view_structure);
            this.groupBox109.Location = new System.Drawing.Point(7, 123);
            this.groupBox109.Name = "groupBox109";
            this.groupBox109.Size = new System.Drawing.Size(927, 48);
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
            "Total DL+SIDL+LL Analysis"});
            this.cmb_long_open_file.Location = new System.Drawing.Point(6, 18);
            this.cmb_long_open_file.Name = "cmb_long_open_file";
            this.cmb_long_open_file.Size = new System.Drawing.Size(308, 21);
            this.cmb_long_open_file.TabIndex = 79;
            this.cmb_long_open_file.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            // 
            // btn_View_Post_Process
            // 
            this.btn_View_Post_Process.Location = new System.Drawing.Point(773, 16);
            this.btn_View_Post_Process.Name = "btn_View_Post_Process";
            this.btn_View_Post_Process.Size = new System.Drawing.Size(146, 22);
            this.btn_View_Post_Process.TabIndex = 78;
            this.btn_View_Post_Process.Text = "View Post Process";
            this.btn_View_Post_Process.UseVisualStyleBackColor = true;
            this.btn_View_Post_Process.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(622, 17);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(146, 22);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(318, 17);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(146, 22);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_structure
            // 
            this.btn_view_structure.Location = new System.Drawing.Point(470, 16);
            this.btn_view_structure.Name = "btn_view_structure";
            this.btn_view_structure.Size = new System.Drawing.Size(146, 22);
            this.btn_view_structure.TabIndex = 74;
            this.btn_view_structure.Text = "View Pre Process";
            this.btn_view_structure.UseVisualStyleBackColor = true;
            this.btn_view_structure.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_Process_LL_Analysis
            // 
            this.btn_Process_LL_Analysis.Enabled = false;
            this.btn_Process_LL_Analysis.Location = new System.Drawing.Point(327, 76);
            this.btn_Process_LL_Analysis.Name = "btn_Process_LL_Analysis";
            this.btn_Process_LL_Analysis.Size = new System.Drawing.Size(148, 40);
            this.btn_Process_LL_Analysis.TabIndex = 104;
            this.btn_Process_LL_Analysis.Text = "Process Analysis";
            this.btn_Process_LL_Analysis.UseVisualStyleBackColor = true;
            this.btn_Process_LL_Analysis.Click += new System.EventHandler(this.btn_Ana_LL_process_analysis_Click);
            // 
            // btn_Ana_DL_create_data
            // 
            this.btn_Ana_DL_create_data.Location = new System.Drawing.Point(173, 76);
            this.btn_Ana_DL_create_data.Name = "btn_Ana_DL_create_data";
            this.btn_Ana_DL_create_data.Size = new System.Drawing.Size(148, 40);
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
            this.groupBox71.Location = new System.Drawing.Point(498, 20);
            this.groupBox71.Name = "groupBox71";
            this.groupBox71.Size = new System.Drawing.Size(431, 40);
            this.groupBox71.TabIndex = 133;
            this.groupBox71.TabStop = false;
            this.groupBox71.Text = "SUPPORT AT START";
            // 
            // rbtn_ssprt_pinned
            // 
            this.rbtn_ssprt_pinned.AutoSize = true;
            this.rbtn_ssprt_pinned.Checked = true;
            this.rbtn_ssprt_pinned.Location = new System.Drawing.Point(6, 17);
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
            this.rbtn_ssprt_fixed.Location = new System.Drawing.Point(79, 17);
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
            this.chk_ssprt_fixed_MZ.Location = new System.Drawing.Point(369, 17);
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
            this.chk_ssprt_fixed_FZ.Location = new System.Drawing.Point(235, 17);
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
            this.chk_ssprt_fixed_MY.Location = new System.Drawing.Point(325, 17);
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
            this.chk_ssprt_fixed_FY.Location = new System.Drawing.Point(191, 17);
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
            this.chk_ssprt_fixed_MX.Location = new System.Drawing.Point(276, 17);
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
            this.chk_ssprt_fixed_FX.Location = new System.Drawing.Point(145, 17);
            this.chk_ssprt_fixed_FX.Name = "chk_ssprt_fixed_FX";
            this.chk_ssprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FX.TabIndex = 0;
            this.chk_ssprt_fixed_FX.Text = "FX";
            this.chk_ssprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // uC_Res_Normal
            // 
            this.uC_Res_Normal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Res_Normal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Res_Normal.Location = new System.Drawing.Point(0, 0);
            this.uC_Res_Normal.Name = "uC_Res_Normal";
            this.uC_Res_Normal.Size = new System.Drawing.Size(953, 441);
            this.uC_Res_Normal.TabIndex = 0;
            // 
            // tab_stage
            // 
            this.tab_stage.Controls.Add(this.tc_stage);
            this.tab_stage.Location = new System.Drawing.Point(4, 22);
            this.tab_stage.Name = "tab_stage";
            this.tab_stage.Size = new System.Drawing.Size(961, 634);
            this.tab_stage.TabIndex = 5;
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
            this.tc_stage.Location = new System.Drawing.Point(0, 0);
            this.tc_stage.Name = "tc_stage";
            this.tc_stage.SelectedIndex = 0;
            this.tc_stage.Size = new System.Drawing.Size(961, 634);
            this.tc_stage.TabIndex = 1;
            // 
            // tab_stage1
            // 
            this.tab_stage1.Controls.Add(this.uC_BoxGirder_Stage1);
            this.tab_stage1.Location = new System.Drawing.Point(4, 22);
            this.tab_stage1.Name = "tab_stage1";
            this.tab_stage1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage1.Size = new System.Drawing.Size(953, 608);
            this.tab_stage1.TabIndex = 0;
            this.tab_stage1.Text = "STAGE 1";
            this.tab_stage1.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage1
            // 
            this.uC_BoxGirder_Stage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage1.Location = new System.Drawing.Point(3, 3);
            this.uC_BoxGirder_Stage1.Name = "uC_BoxGirder_Stage1";
            this.uC_BoxGirder_Stage1.Size = new System.Drawing.Size(947, 602);
            this.uC_BoxGirder_Stage1.TabIndex = 0;
            this.uC_BoxGirder_Stage1.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_Click);
            this.uC_BoxGirder_Stage1.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            this.uC_BoxGirder_Stage1.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage1_OnEmodTextChanged);
            // 
            // tab_stage2
            // 
            this.tab_stage2.Controls.Add(this.uC_BoxGirder_Stage2);
            this.tab_stage2.Location = new System.Drawing.Point(4, 22);
            this.tab_stage2.Name = "tab_stage2";
            this.tab_stage2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage2.Size = new System.Drawing.Size(953, 608);
            this.tab_stage2.TabIndex = 1;
            this.tab_stage2.Text = "STAGE 2";
            this.tab_stage2.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage2
            // 
            this.uC_BoxGirder_Stage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage2.Location = new System.Drawing.Point(3, 3);
            this.uC_BoxGirder_Stage2.Name = "uC_BoxGirder_Stage2";
            this.uC_BoxGirder_Stage2.Size = new System.Drawing.Size(947, 602);
            this.uC_BoxGirder_Stage2.TabIndex = 1;
            this.uC_BoxGirder_Stage2.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_Click);
            this.uC_BoxGirder_Stage2.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            this.uC_BoxGirder_Stage2.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage1_OnEmodTextChanged);
            // 
            // tab_stage3
            // 
            this.tab_stage3.Controls.Add(this.uC_BoxGirder_Stage3);
            this.tab_stage3.Location = new System.Drawing.Point(4, 22);
            this.tab_stage3.Name = "tab_stage3";
            this.tab_stage3.Size = new System.Drawing.Size(953, 608);
            this.tab_stage3.TabIndex = 2;
            this.tab_stage3.Text = "STAGE 3";
            this.tab_stage3.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage3
            // 
            this.uC_BoxGirder_Stage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage3.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage3.Name = "uC_BoxGirder_Stage3";
            this.uC_BoxGirder_Stage3.Size = new System.Drawing.Size(953, 608);
            this.uC_BoxGirder_Stage3.TabIndex = 1;
            this.uC_BoxGirder_Stage3.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_Click);
            this.uC_BoxGirder_Stage3.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            this.uC_BoxGirder_Stage3.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage1_OnEmodTextChanged);
            // 
            // tab_stage4
            // 
            this.tab_stage4.Controls.Add(this.uC_BoxGirder_Stage4);
            this.tab_stage4.Location = new System.Drawing.Point(4, 22);
            this.tab_stage4.Name = "tab_stage4";
            this.tab_stage4.Size = new System.Drawing.Size(953, 608);
            this.tab_stage4.TabIndex = 3;
            this.tab_stage4.Text = "STAGE 4";
            this.tab_stage4.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage4
            // 
            this.uC_BoxGirder_Stage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage4.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage4.Name = "uC_BoxGirder_Stage4";
            this.uC_BoxGirder_Stage4.Size = new System.Drawing.Size(953, 608);
            this.uC_BoxGirder_Stage4.TabIndex = 1;
            this.uC_BoxGirder_Stage4.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_Click);
            this.uC_BoxGirder_Stage4.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            this.uC_BoxGirder_Stage4.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage1_OnEmodTextChanged);
            // 
            // tab_stage5
            // 
            this.tab_stage5.Controls.Add(this.uC_BoxGirder_Stage5);
            this.tab_stage5.Location = new System.Drawing.Point(4, 22);
            this.tab_stage5.Name = "tab_stage5";
            this.tab_stage5.Size = new System.Drawing.Size(953, 608);
            this.tab_stage5.TabIndex = 4;
            this.tab_stage5.Text = "STAGE 5";
            this.tab_stage5.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage5
            // 
            this.uC_BoxGirder_Stage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage5.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage5.Name = "uC_BoxGirder_Stage5";
            this.uC_BoxGirder_Stage5.Size = new System.Drawing.Size(953, 608);
            this.uC_BoxGirder_Stage5.TabIndex = 1;
            this.uC_BoxGirder_Stage5.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_Click);
            this.uC_BoxGirder_Stage5.OnComboboxSelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_SelectedIndexChanged);
            this.uC_BoxGirder_Stage5.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage1_OnEmodTextChanged);
            // 
            // tab_designSage
            // 
            this.tab_designSage.Controls.Add(this.uC_BoxGirder_Results1);
            this.tab_designSage.Controls.Add(this.panel7);
            this.tab_designSage.Location = new System.Drawing.Point(4, 22);
            this.tab_designSage.Name = "tab_designSage";
            this.tab_designSage.Size = new System.Drawing.Size(953, 608);
            this.tab_designSage.TabIndex = 5;
            this.tab_designSage.Text = "Design Forces";
            this.tab_designSage.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Results1
            // 
            this.uC_BoxGirder_Results1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Results1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Results1.Location = new System.Drawing.Point(0, 58);
            this.uC_BoxGirder_Results1.Name = "uC_BoxGirder_Results1";
            this.uC_BoxGirder_Results1.Size = new System.Drawing.Size(953, 550);
            this.uC_BoxGirder_Results1.TabIndex = 83;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btn_result_summary);
            this.panel7.Controls.Add(this.cmb_design_stage);
            this.panel7.Controls.Add(this.label117);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(953, 58);
            this.panel7.TabIndex = 82;
            // 
            // btn_result_summary
            // 
            this.btn_result_summary.Location = new System.Drawing.Point(694, -47);
            this.btn_result_summary.Name = "btn_result_summary";
            this.btn_result_summary.Size = new System.Drawing.Size(260, 23);
            this.btn_result_summary.TabIndex = 82;
            this.btn_result_summary.Text = "Open Analysis Result Summary";
            this.btn_result_summary.UseVisualStyleBackColor = true;
            this.btn_result_summary.Visible = false;
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
            this.cmb_design_stage.Location = new System.Drawing.Point(202, 15);
            this.cmb_design_stage.Name = "cmb_design_stage";
            this.cmb_design_stage.Size = new System.Drawing.Size(254, 21);
            this.cmb_design_stage.TabIndex = 80;
            this.cmb_design_stage.SelectedIndexChanged += new System.EventHandler(this.cmb_design_stage_SelectedIndexChanged);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(58, 18);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(122, 13);
            this.label117.TabIndex = 81;
            this.label117.Text = "Select Design Stage";
            // 
            // tab_worksheet_design
            // 
            this.tab_worksheet_design.Controls.Add(this.tabControl1);
            this.tab_worksheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_worksheet_design.Name = "tab_worksheet_design";
            this.tab_worksheet_design.Size = new System.Drawing.Size(975, 666);
            this.tab_worksheet_design.TabIndex = 1;
            this.tab_worksheet_design.Text = "Design in Excel Format";
            this.tab_worksheet_design.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage12);
            this.tabControl1.Controls.Add(this.tabPage13);
            this.tabControl1.Controls.Add(this.tabPage14);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(975, 666);
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
            this.tabPage10.Size = new System.Drawing.Size(967, 640);
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
            this.uC_BoxGirder1.Size = new System.Drawing.Size(961, 567);
            this.uC_BoxGirder1.Span = 48.75D;
            this.uC_BoxGirder1.TabIndex = 12;
            this.uC_BoxGirder1.Width = 9.75D;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_psc_box);
            this.panel2.Controls.Add(this.btn_worksheet_open);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 570);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(961, 67);
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
            this.tabPage12.Size = new System.Drawing.Size(967, 640);
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
            this.tabPage13.Size = new System.Drawing.Size(967, 640);
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
            this.tabPage14.Size = new System.Drawing.Size(967, 640);
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
            this.tabPage15.Size = new System.Drawing.Size(967, 640);
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
            // tab_Segment
            // 
            this.tab_Segment.Controls.Add(this.tabControl2);
            this.tab_Segment.Controls.Add(this.panel1);
            this.tab_Segment.Location = new System.Drawing.Point(4, 22);
            this.tab_Segment.Name = "tab_Segment";
            this.tab_Segment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Segment.Size = new System.Drawing.Size(975, 666);
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
            this.tabControl2.Size = new System.Drawing.Size(969, 627);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox19);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(961, 601);
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
            this.groupBox19.Size = new System.Drawing.Size(955, 595);
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
            this.cmb_tab1_Fy.Location = new System.Drawing.Point(311, 245);
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
            this.cmb_tab1_Fcu.Location = new System.Drawing.Point(311, 218);
            this.cmb_tab1_Fcu.Name = "cmb_tab1_Fcu";
            this.cmb_tab1_Fcu.Size = new System.Drawing.Size(65, 21);
            this.cmb_tab1_Fcu.TabIndex = 60;
            this.cmb_tab1_Fcu.SelectedIndexChanged += new System.EventHandler(this.cmb_tab1_Fcu_SelectedIndexChanged);
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(286, 248);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(23, 14);
            this.label221.TabIndex = 65;
            this.label221.Text = "Fe";
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label222.Location = new System.Drawing.Point(291, 221);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(18, 14);
            this.label222.TabIndex = 64;
            this.label222.Text = "M";
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Location = new System.Drawing.Point(6, 249);
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
            this.groupBox34.Location = new System.Drawing.Point(9, 441);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(895, 151);
            this.groupBox34.TabIndex = 16;
            this.groupBox34.TabStop = false;
            // 
            // label208
            // 
            this.label208.AutoSize = true;
            this.label208.Location = new System.Drawing.Point(799, 123);
            this.label208.Name = "label208";
            this.label208.Size = new System.Drawing.Size(30, 13);
            this.label208.TabIndex = 8;
            this.label208.Text = "Mpa";
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(799, 98);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(30, 13);
            this.label207.TabIndex = 8;
            this.label207.Text = "Mpa";
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Location = new System.Drawing.Point(799, 44);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(30, 13);
            this.label206.TabIndex = 8;
            this.label206.Text = "Mpa";
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Location = new System.Drawing.Point(799, 17);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(30, 13);
            this.label205.TabIndex = 8;
            this.label205.Text = "Mpa";
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Location = new System.Drawing.Point(430, 122);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(300, 13);
            this.label204.TabIndex = 7;
            this.label204.Text = "Perm. shear stress in combined shear & torsion [ttu]";
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Location = new System.Drawing.Point(430, 98);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(140, 13);
            this.label201.TabIndex = 7;
            this.label201.Text = "Perm. shear stress [tv]";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Location = new System.Drawing.Point(7, 119);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(184, 13);
            this.label203.TabIndex = 7;
            this.label203.Text = "Perm. direct  shear stress [ttv]";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(7, 92);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(228, 13);
            this.label195.TabIndex = 7;
            this.label195.Text = "Temporary tensile stress after 28 days";
            // 
            // txt_ttu
            // 
            this.txt_ttu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ttu.Location = new System.Drawing.Point(731, 120);
            this.txt_ttu.Name = "txt_ttu";
            this.txt_ttu.Size = new System.Drawing.Size(65, 21);
            this.txt_ttu.TabIndex = 6;
            this.txt_ttu.Text = "4.75";
            this.txt_ttu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tv
            // 
            this.txt_tv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tv.Location = new System.Drawing.Point(731, 96);
            this.txt_tv.Name = "txt_tv";
            this.txt_tv.Size = new System.Drawing.Size(65, 21);
            this.txt_tv.TabIndex = 6;
            this.txt_tv.Text = "4.70";
            this.txt_tv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ttv
            // 
            this.txt_ttv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ttv.Location = new System.Drawing.Point(302, 123);
            this.txt_ttv.Name = "txt_ttv";
            this.txt_ttv.Size = new System.Drawing.Size(65, 21);
            this.txt_ttv.TabIndex = 6;
            this.txt_ttv.Text = "0.42";
            this.txt_ttv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ft_temp28
            // 
            this.txt_ft_temp28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ft_temp28.Location = new System.Drawing.Point(302, 96);
            this.txt_ft_temp28.Name = "txt_ft_temp28";
            this.txt_ft_temp28.Size = new System.Drawing.Size(65, 21);
            this.txt_ft_temp28.TabIndex = 6;
            this.txt_ft_temp28.Text = "2.0";
            this.txt_ft_temp28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(430, 65);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(280, 26);
            this.label200.TabIndex = 5;
            this.label200.Text = "Factor for extra time dependent loss considered\r\n(Should be 1.0 as well as 1.2)";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(7, 65);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(263, 13);
            this.label196.TabIndex = 5;
            this.label196.Text = "Temporary compressive stress after 28 days";
            // 
            // txt_fc_factor
            // 
            this.txt_fc_factor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_factor.Location = new System.Drawing.Point(731, 69);
            this.txt_fc_factor.Name = "txt_fc_factor";
            this.txt_fc_factor.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_factor.TabIndex = 4;
            this.txt_fc_factor.Text = "1.2";
            this.txt_fc_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fc_temp28
            // 
            this.txt_fc_temp28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_temp28.Location = new System.Drawing.Point(302, 69);
            this.txt_fc_temp28.Name = "txt_fc_temp28";
            this.txt_fc_temp28.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_temp28.TabIndex = 4;
            this.txt_fc_temp28.Text = "20.0";
            this.txt_fc_temp28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(430, 38);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(170, 13);
            this.label199.TabIndex = 3;
            this.label199.Text = "Modulus of rupture [Modrup]";
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Location = new System.Drawing.Point(7, 38);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(228, 13);
            this.label197.TabIndex = 3;
            this.label197.Text = "Temporary tensile stress after 14 days";
            // 
            // txt_Mod_rup
            // 
            this.txt_Mod_rup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Mod_rup.Location = new System.Drawing.Point(731, 42);
            this.txt_Mod_rup.Name = "txt_Mod_rup";
            this.txt_Mod_rup.Size = new System.Drawing.Size(65, 21);
            this.txt_Mod_rup.TabIndex = 2;
            this.txt_Mod_rup.Text = "2.95";
            this.txt_Mod_rup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ft_temp14
            // 
            this.txt_ft_temp14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ft_temp14.Location = new System.Drawing.Point(302, 42);
            this.txt_ft_temp14.Name = "txt_ft_temp14";
            this.txt_ft_temp14.Size = new System.Drawing.Size(65, 21);
            this.txt_ft_temp14.TabIndex = 2;
            this.txt_ft_temp14.Text = " 1.74";
            this.txt_ft_temp14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(430, 17);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(295, 13);
            this.label198.TabIndex = 1;
            this.label198.Text = "Service Stage compressive stress [Scompservice]";
            // 
            // txt_fc_serv
            // 
            this.txt_fc_serv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_serv.Location = new System.Drawing.Point(731, 15);
            this.txt_fc_serv.Name = "txt_fc_serv";
            this.txt_fc_serv.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_serv.TabIndex = 0;
            this.txt_fc_serv.Text = "13.46";
            this.txt_fc_serv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Location = new System.Drawing.Point(7, 17);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(263, 13);
            this.label202.TabIndex = 1;
            this.label202.Text = "Temporary compressive stress after 14 days";
            // 
            // txt_fc_temp14
            // 
            this.txt_fc_temp14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fc_temp14.Location = new System.Drawing.Point(302, 15);
            this.txt_fc_temp14.Name = "txt_fc_temp14";
            this.txt_fc_temp14.Size = new System.Drawing.Size(65, 21);
            this.txt_fc_temp14.TabIndex = 0;
            this.txt_fc_temp14.Text = "17.40";
            this.txt_fc_temp14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(381, 396);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(19, 13);
            this.label123.TabIndex = 15;
            this.label123.Text = "%";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(382, 373);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(19, 13);
            this.label122.TabIndex = 15;
            this.label122.Text = "%";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(382, 298);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(19, 13);
            this.label119.TabIndex = 15;
            this.label119.Text = "%";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(382, 347);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(34, 13);
            this.label121.TabIndex = 15;
            this.label121.Text = "days";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(382, 271);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(34, 13);
            this.label118.TabIndex = 15;
            this.label118.Text = "days";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(379, 323);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(91, 13);
            this.label120.TabIndex = 15;
            this.label120.Text = "MPa(N/mm^2)";
            // 
            // label225
            // 
            this.label225.AutoSize = true;
            this.label225.Location = new System.Drawing.Point(382, 249);
            this.label225.Name = "label225";
            this.label225.Size = new System.Drawing.Size(91, 13);
            this.label225.TabIndex = 15;
            this.label225.Text = "MPa(N/mm^2)";
            // 
            // label224
            // 
            this.label224.AutoSize = true;
            this.label224.Location = new System.Drawing.Point(380, 221);
            this.label224.Name = "label224";
            this.label224.Size = new System.Drawing.Size(91, 13);
            this.label224.TabIndex = 15;
            this.label224.Text = "MPa(N/mm^2)";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(382, 196);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(18, 13);
            this.label116.TabIndex = 15;
            this.label116.Text = "m";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(382, 169);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(18, 13);
            this.label115.TabIndex = 15;
            this.label115.Text = "m";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(811, 84);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(18, 13);
            this.label127.TabIndex = 15;
            this.label127.Text = "m";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(811, 57);
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
            this.label124.Location = new System.Drawing.Point(383, 422);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(18, 13);
            this.label124.TabIndex = 15;
            this.label124.Text = "m";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(382, 142);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(18, 13);
            this.label110.TabIndex = 15;
            this.label110.Text = "m";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(382, 115);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(18, 13);
            this.label108.TabIndex = 15;
            this.label108.Text = "m";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(382, 88);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(18, 13);
            this.label107.TabIndex = 15;
            this.label107.Text = "m";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(382, 61);
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
            this.groupBox21.Location = new System.Drawing.Point(511, 215);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(393, 102);
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
            this.groupBox20.Location = new System.Drawing.Point(511, 321);
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
            this.label104.Location = new System.Drawing.Point(422, 196);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(312, 13);
            this.label104.TabIndex = 13;
            this.label104.Text = "Coefficient of Thermal Expansion of Concrete [alpha]";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(424, 168);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(197, 13);
            this.label72.TabIndex = 13;
            this.label72.Text = "Ultimate Live Load factor [FactLL]";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(422, 140);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(312, 13);
            this.label71.TabIndex = 13;
            this.label71.Text = "Ultimate Super Imposed Dead Load factor [FactSIDL]";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(422, 113);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(207, 13);
            this.label70.TabIndex = 13;
            this.label70.Text = "Ultimate Dead Load factor [FactDL]";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 373);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(282, 13);
            this.label64.TabIndex = 13;
            this.label64.Text = "Maturity of girder at the time of casting of SIDL ";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(422, 85);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(262, 13);
            this.label69.TabIndex = 13;
            this.label69.Text = "Width of Top Flange of Equivalent Girder [bt]";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 347);
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
            this.label68.Location = new System.Drawing.Point(422, 58);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(287, 13);
            this.label68.TabIndex = 13;
            this.label68.Text = "Thickness of Top Flange of Equivalent Girder [df]";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(6, 323);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(243, 13);
            this.label62.TabIndex = 13;
            this.label62.Text = "Strength concrete at the time of transfer ";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 298);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(205, 13);
            this.label51.TabIndex = 13;
            this.label51.Text = "Maturity of concrete for at transfer";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 422);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(138, 13);
            this.label66.TabIndex = 13;
            this.label66.Text = "Wearing coat thickness";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 271);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(181, 13);
            this.label50.TabIndex = 13;
            this.label50.Text = "Age of concrete for at transfer";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 396);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(305, 13);
            this.label65.TabIndex = 13;
            this.label65.Text = "Extra time dependent loss to be considered [T_loss]";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 220);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(201, 13);
            this.label49.TabIndex = 13;
            this.label49.Text = "Grade of Concrete of Girder [Fcu]";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 196);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(145, 13);
            this.label48.TabIndex = 13;
            this.label48.Text = "Depth of Box Girder [D]";
            // 
            // txt_tab1_alpha
            // 
            this.txt_tab1_alpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_alpha.Location = new System.Drawing.Point(740, 194);
            this.txt_tab1_alpha.Name = "txt_tab1_alpha";
            this.txt_tab1_alpha.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_alpha.TabIndex = 12;
            this.txt_tab1_alpha.Text = "0.0000117";
            this.txt_tab1_alpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactLL
            // 
            this.txt_tab1_FactLL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactLL.Location = new System.Drawing.Point(740, 165);
            this.txt_tab1_FactLL.Name = "txt_tab1_FactLL";
            this.txt_tab1_FactLL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactLL.TabIndex = 12;
            this.txt_tab1_FactLL.Text = "2.500";
            this.txt_tab1_FactLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactSIDL
            // 
            this.txt_tab1_FactSIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactSIDL.Location = new System.Drawing.Point(740, 138);
            this.txt_tab1_FactSIDL.Name = "txt_tab1_FactSIDL";
            this.txt_tab1_FactSIDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactSIDL.TabIndex = 12;
            this.txt_tab1_FactSIDL.Text = "2.0";
            this.txt_tab1_FactSIDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_FactDL
            // 
            this.txt_tab1_FactDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_FactDL.Location = new System.Drawing.Point(740, 111);
            this.txt_tab1_FactDL.Name = "txt_tab1_FactDL";
            this.txt_tab1_FactDL.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_FactDL.TabIndex = 12;
            this.txt_tab1_FactDL.Text = "1.250";
            this.txt_tab1_FactDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_Mct_SIDL
            // 
            this.txt_tab1_Mct_SIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Mct_SIDL.Location = new System.Drawing.Point(311, 371);
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
            this.txt_tab1_bt.Location = new System.Drawing.Point(740, 82);
            this.txt_tab1_bt.Name = "txt_tab1_bt";
            this.txt_tab1_bt.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_bt.TabIndex = 12;
            this.txt_tab1_bt.Text = "1.0";
            this.txt_tab1_bt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_agt_SIDL
            // 
            this.txt_tab1_agt_SIDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_agt_SIDL.Location = new System.Drawing.Point(311, 345);
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
            this.txt_tab1_df.Location = new System.Drawing.Point(740, 55);
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
            this.txt_tab1_sctt.Location = new System.Drawing.Point(311, 321);
            this.txt_tab1_sctt.Name = "txt_tab1_sctt";
            this.txt_tab1_sctt.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_sctt.TabIndex = 12;
            this.txt_tab1_sctt.Text = "34.8";
            this.txt_tab1_sctt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_Mct
            // 
            this.txt_tab1_Mct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_Mct.Location = new System.Drawing.Point(311, 296);
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
            this.txt_tab1_wct.Location = new System.Drawing.Point(311, 420);
            this.txt_tab1_wct.Name = "txt_tab1_wct";
            this.txt_tab1_wct.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_wct.TabIndex = 12;
            this.txt_tab1_wct.Text = "0.065";
            this.txt_tab1_wct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_act
            // 
            this.txt_tab1_act.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_act.Location = new System.Drawing.Point(311, 269);
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
            this.txt_tab1_T_loss.Location = new System.Drawing.Point(311, 394);
            this.txt_tab1_T_loss.Name = "txt_tab1_T_loss";
            this.txt_tab1_T_loss.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_T_loss.TabIndex = 12;
            this.txt_tab1_T_loss.Text = "20";
            this.txt_tab1_T_loss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab1_D
            // 
            this.txt_tab1_D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_D.Location = new System.Drawing.Point(311, 194);
            this.txt_tab1_D.Name = "txt_tab1_D";
            this.txt_tab1_D.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_D.TabIndex = 12;
            this.txt_tab1_D.Text = "2.50";
            this.txt_tab1_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 169);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(117, 13);
            this.label47.TabIndex = 11;
            this.label47.Text = "Width of deck [Dw]";
            // 
            // txt_tab1_DW
            // 
            this.txt_tab1_DW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_DW.ForeColor = System.Drawing.Color.Red;
            this.txt_tab1_DW.Location = new System.Drawing.Point(311, 167);
            this.txt_tab1_DW.Name = "txt_tab1_DW";
            this.txt_tab1_DW.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_DW.TabIndex = 10;
            this.txt_tab1_DW.Text = "9.75";
            this.txt_tab1_DW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 134);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(246, 26);
            this.label46.TabIndex = 9;
            this.label46.Text = "Effective Span (Centre to Centre spacing \r\nof Bearing) [L = Lo - 2 x L1]";
            // 
            // txt_tab1_L
            // 
            this.txt_tab1_L.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab1_L.Location = new System.Drawing.Point(311, 140);
            this.txt_tab1_L.Name = "txt_tab1_L";
            this.txt_tab1_L.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_L.TabIndex = 8;
            this.txt_tab1_L.Text = "47.75";
            this.txt_tab1_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 115);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(125, 13);
            this.label45.TabIndex = 7;
            this.label45.Text = "Expansion gap [exg]";
            // 
            // txt_tab1_exg
            // 
            this.txt_tab1_exg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_exg.Location = new System.Drawing.Point(311, 113);
            this.txt_tab1_exg.Name = "txt_tab1_exg";
            this.txt_tab1_exg.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_exg.TabIndex = 6;
            this.txt_tab1_exg.Text = "0.04";
            this.txt_tab1_exg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 80);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(241, 26);
            this.label44.TabIndex = 5;
            this.label44.Text = "Distance between Centre Line of Bearing\r\nand Centre Line of Expansion Joint [L2]";
            // 
            // txt_tab1_L2
            // 
            this.txt_tab1_L2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L2.Location = new System.Drawing.Point(311, 86);
            this.txt_tab1_L2.Name = "txt_tab1_L2";
            this.txt_tab1_L2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab1_L2.TabIndex = 4;
            this.txt_tab1_L2.Text = "0.50";
            this.txt_tab1_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 61);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(221, 13);
            this.label43.TabIndex = 3;
            this.label43.Text = "Girder end to bearing centre line [L1]";
            // 
            // txt_tab1_L1
            // 
            this.txt_tab1_L1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab1_L1.Location = new System.Drawing.Point(311, 59);
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
            this.tabPage2.Size = new System.Drawing.Size(961, 601);
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
            this.groupBox22.Size = new System.Drawing.Size(955, 595);
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
            this.groupBox23.Location = new System.Drawing.Point(7, 136);
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
            this.cmb_tab2_nc.Location = new System.Drawing.Point(675, 327);
            this.cmb_tab2_nc.Name = "cmb_tab2_nc";
            this.cmb_tab2_nc.Size = new System.Drawing.Size(158, 21);
            this.cmb_tab2_nc.TabIndex = 69;
            this.cmb_tab2_nc.SelectedIndexChanged += new System.EventHandler(this.cmb_tab2_nc_SelectedIndexChanged);
            // 
            // label241
            // 
            this.label241.AutoSize = true;
            this.label241.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label241.Location = new System.Drawing.Point(454, 330);
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
            this.label184.Location = new System.Drawing.Point(454, 406);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(176, 13);
            this.label184.TabIndex = 20;
            this.label184.Text = "Cross section area of Cables ";
            // 
            // txt_tab2_cable_area
            // 
            this.txt_tab2_cable_area.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cable_area.Location = new System.Drawing.Point(768, 404);
            this.txt_tab2_cable_area.Name = "txt_tab2_cable_area";
            this.txt_tab2_cable_area.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cable_area.TabIndex = 19;
            this.txt_tab2_cable_area.Text = "1875.3";
            this.txt_tab2_cable_area.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Location = new System.Drawing.Point(454, 354);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(223, 13);
            this.label182.TabIndex = 18;
            this.label182.Text = "Number of Cables used  for Left Side ";
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(454, 381);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(231, 13);
            this.label180.TabIndex = 17;
            this.label180.Text = "Number of Cables used  for Right Side ";
            // 
            // txt_tab2_nc_right
            // 
            this.txt_tab2_nc_right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_nc_right.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_nc_right.Location = new System.Drawing.Point(768, 379);
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
            this.txt_tab2_nc_left.Location = new System.Drawing.Point(768, 352);
            this.txt_tab2_nc_left.Name = "txt_tab2_nc_left";
            this.txt_tab2_nc_left.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_nc_left.TabIndex = 16;
            this.txt_tab2_nc_left.Text = "7";
            this.txt_tab2_nc_left.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(399, 288);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(37, 13);
            this.label137.TabIndex = 3;
            this.label137.Text = "kg/m";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(399, 262);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(46, 13);
            this.label136.TabIndex = 3;
            this.label136.Text = "sq.mm";
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(838, 130);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(28, 13);
            this.label168.TabIndex = 7;
            this.label168.Text = "day";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(397, 426);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(19, 13);
            this.label143.TabIndex = 7;
            this.label143.Text = "%";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(397, 399);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(30, 13);
            this.label141.TabIndex = 7;
            this.label141.Text = "Gpa";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(399, 372);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(22, 13);
            this.label140.TabIndex = 7;
            this.label140.Text = "kN";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(838, 103);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(30, 13);
            this.label167.TabIndex = 7;
            this.label167.Text = "Mpa";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(838, 76);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(30, 13);
            this.label166.TabIndex = 7;
            this.label166.Text = "Mpa";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(398, 345);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(30, 13);
            this.label139.TabIndex = 7;
            this.label139.Text = "Mpa";
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(838, 201);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(30, 13);
            this.label171.TabIndex = 7;
            this.label171.Text = "Mpa";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(838, 180);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(30, 13);
            this.label170.TabIndex = 7;
            this.label170.Text = "Mpa";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(399, 318);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(30, 13);
            this.label138.TabIndex = 7;
            this.label138.Text = "Mpa";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(838, 49);
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
            this.label169.Location = new System.Drawing.Point(838, 157);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(29, 13);
            this.label169.TabIndex = 3;
            this.label169.Text = "mm";
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(837, 406);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(46, 13);
            this.label186.TabIndex = 3;
            this.label186.Text = "sq.mm";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(838, 302);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(29, 13);
            this.label175.TabIndex = 3;
            this.label175.Text = "mm";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(838, 280);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(29, 13);
            this.label172.TabIndex = 3;
            this.label172.Text = "mm";
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(837, 433);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(29, 13);
            this.label162.TabIndex = 3;
            this.label162.Text = "mm";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(398, 235);
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
            this.label77.Location = new System.Drawing.Point(6, 241);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(137, 13);
            this.label77.TabIndex = 13;
            this.label77.Text = "Nominal Diameter  [D]";
            // 
            // txt_tab2_D
            // 
            this.txt_tab2_D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_D.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_D.Location = new System.Drawing.Point(323, 233);
            this.txt_tab2_D.Name = "txt_tab2_D";
            this.txt_tab2_D.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_D.TabIndex = 12;
            this.txt_tab2_D.Text = "15.2";
            this.txt_tab2_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(454, 280);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(49, 13);
            this.label103.TabIndex = 13;
            this.label103.Text = "Cover1";
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(454, 257);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(258, 13);
            this.label189.TabIndex = 13;
            this.label189.Text = "Creep Strain at 56 days / 10 Mpa [Crst56]  ";
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Location = new System.Drawing.Point(454, 230);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(275, 13);
            this.label188.TabIndex = 13;
            this.label188.Text = "Residual Shrinkage Strain at 56 days [Resh56]";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(454, 203);
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
            this.label102.Location = new System.Drawing.Point(454, 304);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(230, 13);
            this.label102.TabIndex = 13;
            this.label102.Text = "Cover2  (must be Cover 2 > Cover 1 )";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(454, 186);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(135, 13);
            this.label96.TabIndex = 13;
            this.label96.Text = "Concrete Grade [Fcu] ";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(454, 163);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(211, 13);
            this.label95.TabIndex = 13;
            this.label95.Text = "Diameter of Prestressing Duct [qd] ";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(454, 136);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(295, 13);
            this.label94.TabIndex = 13;
            this.label94.Text = "Age of Concrete for First Stage Prestressing  [td1]";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(454, 109);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(291, 13);
            this.label93.TabIndex = 13;
            this.label93.Text = "Relaxation of Prestressing Steel at 50% uts [Re2]";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(454, 82);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(291, 13);
            this.label92.TabIndex = 13;
            this.label92.Text = "Relaxation of Prestressing Steel at 70% uts [Re1]";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(454, 55);
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
            this.label89.Location = new System.Drawing.Point(454, 433);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(134, 13);
            this.label89.TabIndex = 13;
            this.label89.Text = "Slip at Jacking End [s]";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 426);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(306, 13);
            this.label88.TabIndex = 13;
            this.label88.Text = "Jacking Force at Transfer (% of Breaking Load) [Pj] ";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 399);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(131, 13);
            this.label87.TabIndex = 13;
            this.label87.Text = "Elastic Modulus [Eps] ";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.ForeColor = System.Drawing.Color.Blue;
            this.label86.Location = new System.Drawing.Point(6, 372);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(176, 13);
            this.label86.TabIndex = 13;
            this.label86.Text = "Minimum Breaking Load [Pn] ";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.ForeColor = System.Drawing.Color.Blue;
            this.label85.Location = new System.Drawing.Point(6, 345);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(130, 13);
            this.label85.TabIndex = 13;
            this.label85.Text = "Tensile Strength [Fu] ";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.ForeColor = System.Drawing.Color.Blue;
            this.label84.Location = new System.Drawing.Point(6, 324);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(118, 13);
            this.label84.TabIndex = 13;
            this.label84.Text = "Yield Strength [Fy] ";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.ForeColor = System.Drawing.Color.Blue;
            this.label78.Location = new System.Drawing.Point(6, 295);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(115, 13);
            this.label78.TabIndex = 13;
            this.label78.Text = "Nominal mass [Pu]";
            // 
            // txt_tab2_A
            // 
            this.txt_tab2_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_A.ForeColor = System.Drawing.Color.Blue;
            this.txt_tab2_A.Location = new System.Drawing.Point(323, 260);
            this.txt_tab2_A.Name = "txt_tab2_A";
            this.txt_tab2_A.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_A.TabIndex = 12;
            this.txt_tab2_A.Text = "140.0";
            this.txt_tab2_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_cover1
            // 
            this.txt_tab2_cover1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_cover1.Location = new System.Drawing.Point(768, 278);
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
            this.label79.Location = new System.Drawing.Point(6, 268);
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
            this.txt_tab2_cover2.Location = new System.Drawing.Point(768, 302);
            this.txt_tab2_cover2.Name = "txt_tab2_cover2";
            this.txt_tab2_cover2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_cover2.TabIndex = 12;
            this.txt_tab2_cover2.Text = "250";
            this.txt_tab2_cover2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Crst56
            // 
            this.txt_tab2_Crst56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Crst56.Location = new System.Drawing.Point(768, 255);
            this.txt_tab2_Crst56.Name = "txt_tab2_Crst56";
            this.txt_tab2_Crst56.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Crst56.TabIndex = 12;
            this.txt_tab2_Crst56.Text = " 0.00040";
            this.txt_tab2_Crst56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Resh56
            // 
            this.txt_tab2_Resh56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Resh56.Location = new System.Drawing.Point(768, 228);
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
            this.txt_tab2_Ec.Location = new System.Drawing.Point(768, 201);
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
            this.txt_tab2_Fcu.Location = new System.Drawing.Point(768, 178);
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
            this.txt_tab2_qd.Location = new System.Drawing.Point(768, 155);
            this.txt_tab2_qd.Name = "txt_tab2_qd";
            this.txt_tab2_qd.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_qd.TabIndex = 12;
            this.txt_tab2_qd.Text = "110";
            this.txt_tab2_qd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_td1
            // 
            this.txt_tab2_td1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_td1.Location = new System.Drawing.Point(768, 128);
            this.txt_tab2_td1.Name = "txt_tab2_td1";
            this.txt_tab2_td1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_td1.TabIndex = 12;
            this.txt_tab2_td1.Text = "14";
            this.txt_tab2_td1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Re2
            // 
            this.txt_tab2_Re2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Re2.Location = new System.Drawing.Point(768, 101);
            this.txt_tab2_Re2.Name = "txt_tab2_Re2";
            this.txt_tab2_Re2.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Re2.TabIndex = 12;
            this.txt_tab2_Re2.Text = "0.0";
            this.txt_tab2_Re2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Re1
            // 
            this.txt_tab2_Re1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Re1.Location = new System.Drawing.Point(768, 74);
            this.txt_tab2_Re1.Name = "txt_tab2_Re1";
            this.txt_tab2_Re1.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_Re1.TabIndex = 12;
            this.txt_tab2_Re1.Text = "35.0";
            this.txt_tab2_Re1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_k
            // 
            this.txt_tab2_k.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_k.Location = new System.Drawing.Point(768, 47);
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
            this.txt_tab2_s.Location = new System.Drawing.Point(768, 431);
            this.txt_tab2_s.Name = "txt_tab2_s";
            this.txt_tab2_s.Size = new System.Drawing.Size(65, 21);
            this.txt_tab2_s.TabIndex = 12;
            this.txt_tab2_s.Text = "6.0.";
            this.txt_tab2_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Pj
            // 
            this.txt_tab2_Pj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Pj.Location = new System.Drawing.Point(323, 424);
            this.txt_tab2_Pj.Name = "txt_tab2_Pj";
            this.txt_tab2_Pj.Size = new System.Drawing.Size(70, 21);
            this.txt_tab2_Pj.TabIndex = 12;
            this.txt_tab2_Pj.Text = "76.5";
            this.txt_tab2_Pj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tab2_Eps
            // 
            this.txt_tab2_Eps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tab2_Eps.Location = new System.Drawing.Point(323, 397);
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
            this.txt_tab2_Pn.Location = new System.Drawing.Point(323, 370);
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
            this.txt_tab2_Fu.Location = new System.Drawing.Point(323, 343);
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
            this.txt_tab2_Fy.Location = new System.Drawing.Point(323, 316);
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
            this.txt_tab2_Pu.Location = new System.Drawing.Point(323, 287);
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
            this.groupBox24.Location = new System.Drawing.Point(7, 29);
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
            this.tabPage4.Size = new System.Drawing.Size(961, 601);
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
            this.groupBox38.Location = new System.Drawing.Point(3, 517);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(955, 81);
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
            this.tabControl6.Size = new System.Drawing.Size(955, 514);
            this.tabControl6.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage3.BackgroundImage")));
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(947, 488);
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
            this.tabPage7.Size = new System.Drawing.Size(947, 488);
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
            this.tabPage8.Size = new System.Drawing.Size(947, 488);
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
            this.tabPage9.Size = new System.Drawing.Size(947, 488);
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
            this.panel1.Size = new System.Drawing.Size(969, 33);
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
            this.tab_rcc_abutment.Size = new System.Drawing.Size(975, 666);
            this.tab_rcc_abutment.TabIndex = 5;
            this.tab_rcc_abutment.Text = "Design of RCC Abutment";
            this.tab_rcc_abutment.UseVisualStyleBackColor = true;
            // 
            // tc_abutment
            // 
            this.tc_abutment.Controls.Add(this.tab_AbutmentLSM);
            this.tc_abutment.Controls.Add(this.tab_AbutmentPileLSM);
            this.tc_abutment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_abutment.Location = new System.Drawing.Point(3, 3);
            this.tc_abutment.Name = "tc_abutment";
            this.tc_abutment.SelectedIndex = 0;
            this.tc_abutment.Size = new System.Drawing.Size(969, 660);
            this.tc_abutment.TabIndex = 3;
            // 
            // tab_AbutmentLSM
            // 
            this.tab_AbutmentLSM.Controls.Add(this.uC_RCC_Abut1);
            this.tab_AbutmentLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_AbutmentLSM.Name = "tab_AbutmentLSM";
            this.tab_AbutmentLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_AbutmentLSM.Size = new System.Drawing.Size(961, 634);
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
            this.uC_RCC_Abut1.IsBoxType = true;
            this.uC_RCC_Abut1.Length = 60D;
            this.uC_RCC_Abut1.Location = new System.Drawing.Point(3, 3);
            this.uC_RCC_Abut1.Name = "uC_RCC_Abut1";
            this.uC_RCC_Abut1.Overhang = 0.65D;
            this.uC_RCC_Abut1.Size = new System.Drawing.Size(955, 628);
            this.uC_RCC_Abut1.TabIndex = 0;
            this.uC_RCC_Abut1.Width = 730D;
            this.uC_RCC_Abut1.Abut_Counterfort_LS1_dead_load_CheckedChanged += new System.EventHandler(this.uC_RCC_Abut1_Abut_Counterfort_LS1_dead_load_CheckedChanged);
            // 
            // tab_AbutmentPileLSM
            // 
            this.tab_AbutmentPileLSM.Controls.Add(this.panel4);
            this.tab_AbutmentPileLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_AbutmentPileLSM.Name = "tab_AbutmentPileLSM";
            this.tab_AbutmentPileLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_AbutmentPileLSM.Size = new System.Drawing.Size(961, 634);
            this.tab_AbutmentPileLSM.TabIndex = 2;
            this.tab_AbutmentPileLSM.Text = "Abutment Design with Pile Foundation in LSM";
            this.tab_AbutmentPileLSM.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.uC_AbutmentPileLS1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(955, 628);
            this.panel4.TabIndex = 1;
            // 
            // uC_AbutmentPileLS1
            // 
            this.uC_AbutmentPileLS1.Abutment_Length = "13.0";
            this.uC_AbutmentPileLS1.AutoScroll = true;
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
            this.uC_AbutmentPileLS1.Location = new System.Drawing.Point(0, 0);
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
            this.uC_AbutmentPileLS1.Size = new System.Drawing.Size(955, 628);
            this.uC_AbutmentPileLS1.Slab_Thickness = "0.22";
            this.uC_AbutmentPileLS1.Span = "12.687";
            this.uC_AbutmentPileLS1.TabIndex = 0;
            this.uC_AbutmentPileLS1.Wearing_coat_load = "0.22";
            this.uC_AbutmentPileLS1.Wearing_Coat_Thickness = "65";
            // 
            // tab_pier
            // 
            this.tab_pier.Controls.Add(this.tc_pier);
            this.tab_pier.Controls.Add(this.label155);
            this.tab_pier.Controls.Add(this.btn_RCC_Pier_Process);
            this.tab_pier.Controls.Add(this.btn_RCC_Pier_Report);
            this.tab_pier.Location = new System.Drawing.Point(4, 22);
            this.tab_pier.Name = "tab_pier";
            this.tab_pier.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pier.Size = new System.Drawing.Size(975, 666);
            this.tab_pier.TabIndex = 4;
            this.tab_pier.Text = "Design of RCC Pier";
            this.tab_pier.UseVisualStyleBackColor = true;
            // 
            // tc_pier
            // 
            this.tc_pier.Controls.Add(this.tab_PierOpenLSM);
            this.tc_pier.Controls.Add(this.tab_PierPileLSM);
            this.tc_pier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_pier.Location = new System.Drawing.Point(3, 3);
            this.tc_pier.Name = "tc_pier";
            this.tc_pier.SelectedIndex = 0;
            this.tc_pier.Size = new System.Drawing.Size(969, 660);
            this.tc_pier.TabIndex = 122;
            // 
            // tab_PierOpenLSM
            // 
            this.tab_PierOpenLSM.Controls.Add(this.uC_PierOpenLS1);
            this.tab_PierOpenLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_PierOpenLSM.Name = "tab_PierOpenLSM";
            this.tab_PierOpenLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PierOpenLSM.Size = new System.Drawing.Size(961, 634);
            this.tab_PierOpenLSM.TabIndex = 2;
            this.tab_PierOpenLSM.Text = "Pier Design with Open Foundation in LS";
            this.tab_PierOpenLSM.UseVisualStyleBackColor = true;
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
            this.uC_PierOpenLS1.Show_Title = false;
            this.uC_PierOpenLS1.SIDL_Force = "723.52";
            this.uC_PierOpenLS1.Size = new System.Drawing.Size(955, 628);
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
            // tab_PierPileLSM
            // 
            this.tab_PierPileLSM.Controls.Add(this.uC_PierDesignLSM1);
            this.tab_PierPileLSM.Location = new System.Drawing.Point(4, 22);
            this.tab_PierPileLSM.Name = "tab_PierPileLSM";
            this.tab_PierPileLSM.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PierPileLSM.Size = new System.Drawing.Size(961, 634);
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
            this.uC_PierDesignLSM1.Size = new System.Drawing.Size(955, 628);
            this.uC_PierDesignLSM1.TabIndex = 0;
            this.uC_PierDesignLSM1.Total_weight_of_superstructure = "460";
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.ForeColor = System.Drawing.Color.Red;
            this.label155.Location = new System.Drawing.Point(403, 609);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(124, 14);
            this.label155.TabIndex = 120;
            this.label155.Text = "Design of RCC Pier";
            // 
            // btn_RCC_Pier_Process
            // 
            this.btn_RCC_Pier_Process.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RCC_Pier_Process.Location = new System.Drawing.Point(302, 625);
            this.btn_RCC_Pier_Process.Name = "btn_RCC_Pier_Process";
            this.btn_RCC_Pier_Process.Size = new System.Drawing.Size(161, 28);
            this.btn_RCC_Pier_Process.TabIndex = 11;
            this.btn_RCC_Pier_Process.Text = "Process";
            this.btn_RCC_Pier_Process.UseVisualStyleBackColor = true;
            // 
            // btn_RCC_Pier_Report
            // 
            this.btn_RCC_Pier_Report.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RCC_Pier_Report.Location = new System.Drawing.Point(474, 625);
            this.btn_RCC_Pier_Report.Name = "btn_RCC_Pier_Report";
            this.btn_RCC_Pier_Report.Size = new System.Drawing.Size(161, 28);
            this.btn_RCC_Pier_Report.TabIndex = 12;
            this.btn_RCC_Pier_Report.Text = "Report";
            this.btn_RCC_Pier_Report.UseVisualStyleBackColor = true;
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
            this.tab_drawings.Size = new System.Drawing.Size(975, 666);
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
            this.btn_dwg_open_Pier.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_dwg_open_Cantilever
            // 
            this.btn_dwg_open_Cantilever.Location = new System.Drawing.Point(316, 299);
            this.btn_dwg_open_Cantilever.Name = "btn_dwg_open_Cantilever";
            this.btn_dwg_open_Cantilever.Size = new System.Drawing.Size(319, 49);
            this.btn_dwg_open_Cantilever.TabIndex = 81;
            this.btn_dwg_open_Cantilever.Text = "Open Cantilever Abutment Drawings";
            this.btn_dwg_open_Cantilever.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Cantilever.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_dwg_open_Counterfort
            // 
            this.btn_dwg_open_Counterfort.Location = new System.Drawing.Point(316, 230);
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
            this.btn_open_drawings.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // frm_PSC_Box_Girder_Stage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 692);
            this.Controls.Add(this.tc_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_PSC_Box_Girder_Stage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_PSC_Box";
            this.Load += new System.EventHandler(this.frm_PSC_Box_Load);
            this.tc_main.ResumeLayout(false);
            this.tab_Analysis_DL.ResumeLayout(false);
            this.tbc_girder.ResumeLayout(false);
            this.tab_user_input.ResumeLayout(false);
            this.tab_user_input.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.grb_ana_footpath.ResumeLayout(false);
            this.grb_ana_footpath.PerformLayout();
            this.grb_ana_parapet.ResumeLayout(false);
            this.grb_ana_parapet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).EndInit();
            this.grb_ana_crash_barrier.ResumeLayout(false);
            this.grb_ana_crash_barrier.PerformLayout();
            this.grb_ana_wc.ResumeLayout(false);
            this.grb_ana_wc.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.grb_Ana_DL_select_analysis.ResumeLayout(false);
            this.grb_Ana_DL_select_analysis.PerformLayout();
            this.grb_create_input_data.ResumeLayout(false);
            this.grb_create_input_data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tab_cs_diagram.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tab_moving_data_british.ResumeLayout(false);
            this.groupBox45.ResumeLayout(false);
            this.groupBox45.PerformLayout();
            this.spc_HB.Panel1.ResumeLayout(false);
            this.spc_HB.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_HB)).EndInit();
            this.spc_HB.ResumeLayout(false);
            this.groupBox106.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_british_loads)).EndInit();
            this.groupBox105.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_british_loads)).EndInit();
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
            this.tab_moving_data_indian.PerformLayout();
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
            this.groupBox51.ResumeLayout(false);
            this.groupBox51.PerformLayout();
            this.groupBox70.ResumeLayout(false);
            this.groupBox70.PerformLayout();
            this.groupBox109.ResumeLayout(false);
            this.groupBox71.ResumeLayout(false);
            this.groupBox71.PerformLayout();
            this.tab_stage.ResumeLayout(false);
            this.tc_stage.ResumeLayout(false);
            this.tab_stage1.ResumeLayout(false);
            this.tab_stage2.ResumeLayout(false);
            this.tab_stage3.ResumeLayout(false);
            this.tab_stage4.ResumeLayout(false);
            this.tab_stage5.ResumeLayout(false);
            this.tab_designSage.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tab_worksheet_design.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
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
            this.tc_abutment.ResumeLayout(false);
            this.tab_AbutmentLSM.ResumeLayout(false);
            this.tab_AbutmentPileLSM.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tab_pier.ResumeLayout(false);
            this.tab_pier.PerformLayout();
            this.tc_pier.ResumeLayout(false);
            this.tab_PierOpenLSM.ResumeLayout(false);
            this.tab_PierPileLSM.ResumeLayout(false);
            this.tab_drawings.ResumeLayout(false);
            this.tab_drawings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.TextBox txt_Ana_L;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Process_LL_Analysis;
        private System.Windows.Forms.Button btn_Ana_DL_create_data;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TextBox txt_support_distance;
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
        private System.Windows.Forms.Button btn_dwg_pier;
        private System.Windows.Forms.TabControl tbc_girder;
        private System.Windows.Forms.TabPage tab_user_input;
        private System.Windows.Forms.TabPage tab_analysis;
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
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label226;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label228;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.Label label230;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label237;
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
        private System.Windows.Forms.TextBox txt_no_lanes;
        private System.Windows.Forms.TextBox txt_ll_british_lgen;
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
        private System.Windows.Forms.Button btn_View_Post_Process;
        private System.Windows.Forms.Button btn_view_report;
        private System.Windows.Forms.Button btn_view_data;
        private System.Windows.Forms.Button btn_view_structure;
        private System.Windows.Forms.CheckBox chk_HA;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label267;
        private System.Windows.Forms.Label label282;
        private System.Windows.Forms.TabPage tab_rcc_abutment;
        private BridgeAnalysisDesign.Abutment.UC_RCC_Abut uC_RCC_Abut1;
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
        private System.Windows.Forms.Button btn_bs_view_moving_load;
        private System.Windows.Forms.Label label1190;
        private System.Windows.Forms.ComboBox cmb_bs_view_moving_load;
        private System.Windows.Forms.TextBox txt_bs_vehicle_gap;
        private System.Windows.Forms.Label label1191;
        private System.Windows.Forms.Label label561;
        private System.Windows.Forms.Label label557;
        private System.Windows.Forms.Label label558;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label559;
        private System.Windows.Forms.TabPage tab_moving_data_indian;
        private System.Windows.Forms.TabPage tab_stage;
        private System.Windows.Forms.Button btn_irc_view_moving_load;
        private System.Windows.Forms.Label label568;
        private System.Windows.Forms.ComboBox cmb_irc_view_moving_load;
        private System.Windows.Forms.TextBox txt_irc_vehicle_gap;
        private System.Windows.Forms.Label label569;
        private System.Windows.Forms.Label label249;
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
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.GroupBox grb_ana_footpath;
        private System.Windows.Forms.TextBox txt_Ana_Wr;
        private System.Windows.Forms.Label label531;
        private System.Windows.Forms.TextBox txt_Ana_Wf_RHS;
        private System.Windows.Forms.TextBox txt_Ana_Wk;
        private System.Windows.Forms.Label label529;
        private System.Windows.Forms.Label label530;
        private System.Windows.Forms.Label label566;
        private System.Windows.Forms.TextBox txt_Ana_Hf_RHS;
        private System.Windows.Forms.TextBox txt_Ana_Hf_LHS;
        private System.Windows.Forms.Label label528;
        private System.Windows.Forms.Label label565;
        private System.Windows.Forms.Label label567;
        private System.Windows.Forms.Label label524;
        private System.Windows.Forms.Label label525;
        private System.Windows.Forms.Label label526;
        private System.Windows.Forms.Label label564;
        private System.Windows.Forms.TextBox txt_Ana_Wf_LHS;
        private System.Windows.Forms.Label label527;
        private System.Windows.Forms.CheckBox chk_fp_right;
        private System.Windows.Forms.CheckBox chk_footpath;
        private System.Windows.Forms.CheckBox chk_fp_left;
        private System.Windows.Forms.GroupBox grb_ana_parapet;
        private System.Windows.Forms.TextBox txt_Ana_hp;
        private System.Windows.Forms.Label label252;
        private System.Windows.Forms.Label label268;
        private System.Windows.Forms.Label label269;
        private System.Windows.Forms.TextBox txt_Ana_wp;
        private System.Windows.Forms.Label label270;
        private System.Windows.Forms.CheckBox chk_cb_right;
        private System.Windows.Forms.CheckBox chk_crash_barrier;
        private System.Windows.Forms.PictureBox pic_diagram;
        private System.Windows.Forms.GroupBox grb_ana_crash_barrier;
        private System.Windows.Forms.TextBox txt_Ana_Hc_RHS;
        private System.Windows.Forms.Label label563;
        private System.Windows.Forms.TextBox txt_Ana_Hc_LHS;
        private System.Windows.Forms.Label label562;
        private System.Windows.Forms.Label label514;
        private System.Windows.Forms.Label label481;
        private System.Windows.Forms.TextBox txt_Ana_Wc_RHS;
        private System.Windows.Forms.Label label522;
        private System.Windows.Forms.Label label510;
        private System.Windows.Forms.Label label480;
        private System.Windows.Forms.TextBox txt_Ana_Wc_LHS;
        private System.Windows.Forms.Label label523;
        private System.Windows.Forms.CheckBox chk_cb_left;
        private System.Windows.Forms.GroupBox grb_ana_wc;
        private System.Windows.Forms.Label label511;
        private System.Windows.Forms.TextBox txt_Ana_gamma_w;
        private System.Windows.Forms.Label label515;
        private System.Windows.Forms.Label label520;
        private System.Windows.Forms.TextBox txt_Ana_Dw;
        private System.Windows.Forms.Label label521;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txt_out_IZZ;
        private System.Windows.Forms.TextBox txt_inn_IZZ;
        private System.Windows.Forms.TextBox txt_cen_IZZ;
        private System.Windows.Forms.Label label444;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_tot_IZZ;
        private System.Windows.Forms.Label label350;
        private System.Windows.Forms.TextBox txt_out_IYY;
        private System.Windows.Forms.TextBox txt_inn_IYY;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label443;
        private System.Windows.Forms.TextBox txt_cen_IYY;
        private System.Windows.Forms.Label label353;
        private System.Windows.Forms.TextBox txt_tot_IYY;
        private System.Windows.Forms.TextBox txt_out_IXX;
        private System.Windows.Forms.Label label349;
        private System.Windows.Forms.TextBox txt_inn_IXX;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txt_cen_IXX;
        private System.Windows.Forms.Label label352;
        private System.Windows.Forms.TextBox txt_tot_IXX;
        private System.Windows.Forms.TextBox txt_out_pcnt;
        private System.Windows.Forms.Label label348;
        private System.Windows.Forms.TextBox txt_inn_pcnt;
        private System.Windows.Forms.TextBox txt_out_AX;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txt_inn_AX;
        private System.Windows.Forms.TextBox txt_cen_pcnt;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txt_cen_AX;
        private System.Windows.Forms.Label label339;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.TextBox txt_tot_AX;
        private System.Windows.Forms.Label label271;
        private System.Windows.Forms.Label label338;
        private System.Windows.Forms.Label label351;
        private System.Windows.Forms.Label label505;
        private System.Windows.Forms.Label label502;
        private System.Windows.Forms.Label label501;
        private System.Windows.Forms.Label label336;
        private System.Windows.Forms.Label label272;
        private System.Windows.Forms.Label label347;
        private System.Windows.Forms.Label label342;
        private System.Windows.Forms.Label label273;
        private System.Windows.Forms.Label label274;
        private System.Windows.Forms.Label label335;
        private System.Windows.Forms.Label label341;
        private System.Windows.Forms.Label label346;
        private System.Windows.Forms.Label label275;
        private System.Windows.Forms.Label label302;
        private System.Windows.Forms.Label label340;
        private System.Windows.Forms.Label label345;
        private System.Windows.Forms.Label label305;
        private System.Windows.Forms.Label label306;
        private System.Windows.Forms.Label label337;
        private System.Windows.Forms.Label label344;
        private System.Windows.Forms.Label label307;
        private System.Windows.Forms.Label label343;
        private System.Windows.Forms.Label label308;
        private System.Windows.Forms.Label label498;
        private System.Windows.Forms.Label label309;
        private System.Windows.Forms.GroupBox groupBox51;
        private System.Windows.Forms.Label label330;
        private System.Windows.Forms.Label label331;
        private System.Windows.Forms.Label label332;
        private System.Windows.Forms.Label label333;
        private System.Windows.Forms.Label label334;
        private System.Windows.Forms.TextBox txt_PR_conc;
        private System.Windows.Forms.TextBox txt_den_conc;
        private System.Windows.Forms.TextBox txt_emod_conc;
        private System.Windows.Forms.TabControl tc_stage;
        private System.Windows.Forms.TabPage tab_stage1;
        private System.Windows.Forms.TabPage tab_stage2;
        private System.Windows.Forms.TabPage tab_stage3;
        private System.Windows.Forms.TabPage tab_stage4;
        private System.Windows.Forms.TabPage tab_stage5;
        private System.Windows.Forms.TabPage tab_designSage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btn_result_summary;
        private System.Windows.Forms.ComboBox cmb_design_stage;
        private System.Windows.Forms.Label label117;
        private UC_BoxGirder_Results uC_Res_Normal;
        private System.Windows.Forms.TabControl tc_pier;
        private System.Windows.Forms.TabPage tab_PierOpenLSM;
        private BridgeAnalysisDesign.Pier.UC_PierOpenLS uC_PierOpenLS1;
        private System.Windows.Forms.TabPage tab_PierPileLSM;
        private BridgeAnalysisDesign.Pier.UC_PierDesignLSM uC_PierDesignLSM1;
        private System.Windows.Forms.TabControl tc_abutment;
        private System.Windows.Forms.TabPage tab_AbutmentPileLSM;
        private System.Windows.Forms.Panel panel4;
        private BridgeAnalysisDesign.Abutment.UC_AbutmentPileLS uC_AbutmentPileLS1;
        private System.Windows.Forms.TabPage tab_AbutmentLSM;
        private UC_BoxGirder_Stage uC_BoxGirder_Stage1;
        private UC_BoxGirder_Stage uC_BoxGirder_Stage2;
        private UC_BoxGirder_Stage uC_BoxGirder_Stage3;
        private UC_BoxGirder_Stage uC_BoxGirder_Stage4;
        private UC_BoxGirder_Stage uC_BoxGirder_Stage5;
        private UC_BoxGirder_Results uC_BoxGirder_Results1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_carriageway_width;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_exp_gap;
        private System.Windows.Forms.TextBox txt_overhang_gap;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.CheckBox chk_selfweight;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_FPLL;
        private System.Windows.Forms.TextBox txt_SIDL;
    }


}