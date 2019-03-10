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
            this.label11 = new System.Windows.Forms.Label();
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tab_Analysis_DL = new System.Windows.Forms.TabPage();
            this.tbc_girder = new System.Windows.Forms.TabControl();
            this.tab_user_input = new System.Windows.Forms.TabPage();
            this.pic_diagram = new System.Windows.Forms.PictureBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.grb_ana_sw_fp = new System.Windows.Forms.GroupBox();
            this.label531 = new System.Windows.Forms.Label();
            this.label529 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label524 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
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
            this.label12 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Spans_2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Spans_1 = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.txt_Ana_Span = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_cs_diagram1 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.txt_box_cs2_IZZ = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btn_Show_Section_Resulf = new System.Windows.Forms.Button();
            this.rtb_sections = new System.Windows.Forms.RichTextBox();
            this.btn_open_diagram = new System.Windows.Forms.Button();
            this.label226 = new System.Windows.Forms.Label();
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
            this.tab_section_results = new System.Windows.Forms.TabPage();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_selfweight = new System.Windows.Forms.CheckBox();
            this.btn_Process_LL_Analysis = new System.Windows.Forms.Button();
            this.groupBox51 = new System.Windows.Forms.GroupBox();
            this.label330 = new System.Windows.Forms.Label();
            this.label331 = new System.Windows.Forms.Label();
            this.label332 = new System.Windows.Forms.Label();
            this.label333 = new System.Windows.Forms.Label();
            this.label334 = new System.Windows.Forms.Label();
            this.txt_PR_conc = new System.Windows.Forms.TextBox();
            this.txt_den_conc = new System.Windows.Forms.TextBox();
            this.txt_emod_conc = new System.Windows.Forms.TextBox();
            this.groupBox109 = new System.Windows.Forms.GroupBox();
            this.btn_view_report = new System.Windows.Forms.Button();
            this.cmb_long_open_file_analysis = new System.Windows.Forms.ComboBox();
            this.btn_view_data = new System.Windows.Forms.Button();
            this.btn_view_postprocess = new System.Windows.Forms.Button();
            this.btn_view_preprocess = new System.Windows.Forms.Button();
            this.groupBox71 = new System.Windows.Forms.GroupBox();
            this.rbtn_ssprt_pinned = new System.Windows.Forms.RadioButton();
            this.rbtn_ssprt_fixed = new System.Windows.Forms.RadioButton();
            this.chk_ssprt_fixed_MZ = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FZ = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_MY = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FY = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_MX = new System.Windows.Forms.CheckBox();
            this.chk_ssprt_fixed_FX = new System.Windows.Forms.CheckBox();
            this.btn_Ana_DL_create_data = new System.Windows.Forms.Button();
            this.groupBox70 = new System.Windows.Forms.GroupBox();
            this.rbtn_esprt_pinned = new System.Windows.Forms.RadioButton();
            this.rbtn_esprt_fixed = new System.Windows.Forms.RadioButton();
            this.chk_esprt_fixed_MZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FZ = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FY = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_MX = new System.Windows.Forms.CheckBox();
            this.chk_esprt_fixed_FX = new System.Windows.Forms.CheckBox();
            this.uc_Results = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Results_AASHTO();
            this.tab_stage = new System.Windows.Forms.TabPage();
            this.tc_Stage = new System.Windows.Forms.TabControl();
            this.tab_stage1 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage_AASHTO1 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage_AASHTO();
            this.tab_stage2 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage_AASHTO2 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage_AASHTO();
            this.tab_stage3 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage_AASHTO3 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage_AASHTO();
            this.tab_stage4 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage_AASHTO4 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage_AASHTO();
            this.tab_stage5 = new System.Windows.Forms.TabPage();
            this.uC_BoxGirder_Stage_AASHTO5 = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Stage_AASHTO();
            this.tab_design = new System.Windows.Forms.TabPage();
            this.uC_Des_Res = new LimitStateMethod.PSC_Box_Girder.UC_BoxGirder_Results_AASHTO();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_result_summary = new System.Windows.Forms.Button();
            this.cmb_design_stage = new System.Windows.Forms.ComboBox();
            this.label249 = new System.Windows.Forms.Label();
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
            this.tc_main.SuspendLayout();
            this.tab_Analysis_DL.SuspendLayout();
            this.tbc_girder.SuspendLayout();
            this.tab_user_input.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).BeginInit();
            this.groupBox24.SuspendLayout();
            this.grb_ana_sw_fp.SuspendLayout();
            this.grb_ana_crashBarrier.SuspendLayout();
            this.grb_ana_wc.SuspendLayout();
            this.grb_ana_parapet.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.panel5.SuspendLayout();
            this.grb_SIDL.SuspendLayout();
            this.grb_create_input_data.SuspendLayout();
            this.tab_cs_diagram1.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tab_cs_diagram2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tab_section_results.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tab_moving_data.SuspendLayout();
            this.groupBox117.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).BeginInit();
            this.groupBox118.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_liveloads)).BeginInit();
            this.tab_analysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox51.SuspendLayout();
            this.groupBox109.SuspendLayout();
            this.groupBox71.SuspendLayout();
            this.groupBox70.SuspendLayout();
            this.tab_stage.SuspendLayout();
            this.tc_Stage.SuspendLayout();
            this.tab_stage1.SuspendLayout();
            this.tab_stage2.SuspendLayout();
            this.tab_stage3.SuspendLayout();
            this.tab_stage4.SuspendLayout();
            this.tab_stage5.SuspendLayout();
            this.tab_design.SuspendLayout();
            this.panel3.SuspendLayout();
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(322, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "in";
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
            this.tbc_girder.Controls.Add(this.tab_section_results);
            this.tbc_girder.Controls.Add(this.tab_moving_data);
            this.tbc_girder.Controls.Add(this.tab_analysis);
            this.tbc_girder.Controls.Add(this.tab_stage);
            this.tbc_girder.Controls.Add(this.tab_design);
            this.tbc_girder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_girder.Location = new System.Drawing.Point(3, 3);
            this.tbc_girder.Name = "tbc_girder";
            this.tbc_girder.SelectedIndex = 0;
            this.tbc_girder.Size = new System.Drawing.Size(944, 660);
            this.tbc_girder.TabIndex = 107;
            // 
            // tab_user_input
            // 
            this.tab_user_input.AutoScroll = true;
            this.tab_user_input.Controls.Add(this.pic_diagram);
            this.tab_user_input.Controls.Add(this.groupBox24);
            this.tab_user_input.Controls.Add(this.groupBox23);
            this.tab_user_input.Controls.Add(this.groupBox22);
            this.tab_user_input.Controls.Add(this.groupBox21);
            this.tab_user_input.Controls.Add(this.groupBox20);
            this.tab_user_input.Controls.Add(this.panel5);
            this.tab_user_input.Controls.Add(this.label227);
            this.tab_user_input.Controls.Add(this.label228);
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
            // pic_diagram
            // 
            this.pic_diagram.BackgroundImage = global::LimitStateMethod.Properties.Resources.Crash_Barrier_on_RHS__Case_12_;
            this.pic_diagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_diagram.Location = new System.Drawing.Point(417, 320);
            this.pic_diagram.Name = "pic_diagram";
            this.pic_diagram.Size = new System.Drawing.Size(475, 298);
            this.pic_diagram.TabIndex = 189;
            this.pic_diagram.TabStop = false;
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
            this.groupBox24.Location = new System.Drawing.Point(411, 6);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(487, 308);
            this.groupBox24.TabIndex = 188;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "SUPER IMPOSED DEAD LOAD [SIDL]";
            // 
            // grb_ana_sw_fp
            // 
            this.grb_ana_sw_fp.Controls.Add(this.label531);
            this.grb_ana_sw_fp.Controls.Add(this.label529);
            this.grb_ana_sw_fp.Controls.Add(this.label139);
            this.grb_ana_sw_fp.Controls.Add(this.label524);
            this.grb_ana_sw_fp.Controls.Add(this.label140);
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
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(282, 40);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(100, 13);
            this.label139.TabIndex = 7;
            this.label139.Text = "Height [RHS_hf]";
            // 
            // label524
            // 
            this.label524.AutoSize = true;
            this.label524.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label524.Location = new System.Drawing.Point(282, 16);
            this.label524.Name = "label524";
            this.label524.Size = new System.Drawing.Size(98, 13);
            this.label524.TabIndex = 7;
            this.label524.Text = "Height [LHS_hf]";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(8, 40);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(98, 13);
            this.label140.TabIndex = 4;
            this.label140.Text = "Width [RHS_wf]";
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
            this.txt_Ana_hf_LHS.Location = new System.Drawing.Point(389, 16);
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
            this.label143.Location = new System.Drawing.Point(207, 40);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(15, 13);
            this.label143.TabIndex = 2;
            this.label143.Text = "ft";
            // 
            // label525
            // 
            this.label525.AutoSize = true;
            this.label525.Location = new System.Drawing.Point(445, 19);
            this.label525.Name = "label525";
            this.label525.Size = new System.Drawing.Size(15, 13);
            this.label525.TabIndex = 2;
            this.label525.Text = "ft";
            // 
            // txt_Ana_wf_RHS
            // 
            this.txt_Ana_wf_RHS.Location = new System.Drawing.Point(151, 37);
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
            this.chk_footpath.Checked = true;
            this.chk_footpath.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.chk_fp_left.Checked = true;
            this.chk_fp_left.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.chk_cb_right.Checked = true;
            this.chk_cb_right.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.chk_crash_barrier.Checked = true;
            this.chk_crash_barrier.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.chk_cb_left.Checked = true;
            this.chk_cb_left.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.groupBox23.Location = new System.Drawing.Point(15, 624);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(370, 196);
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
            this.groupBox22.Location = new System.Drawing.Point(416, 624);
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
            this.groupBox21.Location = new System.Drawing.Point(19, 826);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(365, 69);
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
            this.groupBox20.Size = new System.Drawing.Size(372, 142);
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
            this.panel5.Location = new System.Drawing.Point(11, 21);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(376, 61);
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
            this.label227.Location = new System.Drawing.Point(159, 94);
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
            this.label228.Location = new System.Drawing.Point(18, 94);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(135, 18);
            this.label228.TabIndex = 125;
            this.label228.Text = "All User Input Data";
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
            this.grb_SIDL.Location = new System.Drawing.Point(411, 797);
            this.grb_SIDL.Name = "grb_SIDL";
            this.grb_SIDL.Size = new System.Drawing.Size(372, 111);
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
            this.grb_create_input_data.Controls.Add(this.label12);
            this.grb_create_input_data.Controls.Add(this.label124);
            this.grb_create_input_data.Controls.Add(this.label2);
            this.grb_create_input_data.Controls.Add(this.txt_Spans_2);
            this.grb_create_input_data.Controls.Add(this.label9);
            this.grb_create_input_data.Controls.Add(this.txt_Spans_1);
            this.grb_create_input_data.Controls.Add(this.label123);
            this.grb_create_input_data.Controls.Add(this.txt_Ana_Span);
            this.grb_create_input_data.Controls.Add(this.label1);
            this.grb_create_input_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_create_input_data.Location = new System.Drawing.Point(15, 125);
            this.grb_create_input_data.Name = "grb_create_input_data";
            this.grb_create_input_data.Size = new System.Drawing.Size(372, 337);
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
            this.label24.Location = new System.Drawing.Point(321, 151);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 13);
            this.label24.TabIndex = 57;
            this.label24.Text = "ft";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 151);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(78, 13);
            this.label25.TabIndex = 56;
            this.label25.Text = "Bridge width";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(322, 308);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(17, 13);
            this.label83.TabIndex = 17;
            this.label83.Text = "in";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(322, 284);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(17, 13);
            this.label81.TabIndex = 17;
            this.label81.Text = "in";
            // 
            // txt_Ana_Bridge_Width
            // 
            this.txt_Ana_Bridge_Width.Location = new System.Drawing.Point(251, 148);
            this.txt_Ana_Bridge_Width.Name = "txt_Ana_Bridge_Width";
            this.txt_Ana_Bridge_Width.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Bridge_Width.TabIndex = 55;
            this.txt_Ana_Bridge_Width.Text = "45.17";
            this.txt_Ana_Bridge_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Bridge_Width.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Bottom_Slab_Thickness
            // 
            this.txt_Ana_Bottom_Slab_Thickness.Location = new System.Drawing.Point(252, 305);
            this.txt_Ana_Bottom_Slab_Thickness.Name = "txt_Ana_Bottom_Slab_Thickness";
            this.txt_Ana_Bottom_Slab_Thickness.Size = new System.Drawing.Size(64, 21);
            this.txt_Ana_Bottom_Slab_Thickness.TabIndex = 6;
            this.txt_Ana_Bottom_Slab_Thickness.Text = "6.0";
            this.txt_Ana_Bottom_Slab_Thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ana_Bottom_Slab_Thickness.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // txt_Ana_Top_Slab_Thickness
            // 
            this.txt_Ana_Top_Slab_Thickness.Location = new System.Drawing.Point(252, 281);
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
            this.label82.Location = new System.Drawing.Point(6, 308);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(132, 13);
            this.label82.TabIndex = 15;
            this.label82.Text = "Bottom slab thickness";
            // 
            // txt_Ana_Web_Thickness
            // 
            this.txt_Ana_Web_Thickness.Location = new System.Drawing.Point(252, 257);
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
            this.label80.Location = new System.Drawing.Point(6, 284);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(111, 13);
            this.label80.TabIndex = 15;
            this.label80.Text = "Top slab thickness";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(322, 236);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "ft";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Web thickness";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(6, 236);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(78, 13);
            this.label32.TabIndex = 54;
            this.label32.Text = "Web spacing";
            // 
            // txt_Ana_Web_Spacing
            // 
            this.txt_Ana_Web_Spacing.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ana_Web_Spacing.Location = new System.Drawing.Point(252, 233);
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
            this.label5.Location = new System.Drawing.Point(322, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "ft";
            // 
            // txt_Ana_Superstructure_depth
            // 
            this.txt_Ana_Superstructure_depth.Location = new System.Drawing.Point(252, 210);
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
            this.label6.Location = new System.Drawing.Point(4, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Superstructure depth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ft";
            // 
            // txt_Ana_Road_Width
            // 
            this.txt_Ana_Road_Width.Location = new System.Drawing.Point(251, 175);
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
            this.label4.Location = new System.Drawing.Point(3, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Roadway width";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(321, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "ft";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(321, 69);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(15, 13);
            this.label124.TabIndex = 2;
            this.label124.Text = "ft";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ft";
            // 
            // txt_Spans_2
            // 
            this.txt_Spans_2.Location = new System.Drawing.Point(251, 93);
            this.txt_Spans_2.Name = "txt_Spans_2";
            this.txt_Spans_2.Size = new System.Drawing.Size(64, 21);
            this.txt_Spans_2.TabIndex = 0;
            this.txt_Spans_2.Text = "130.0";
            this.txt_Spans_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Spans_2.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Span 2 length";
            // 
            // txt_Spans_1
            // 
            this.txt_Spans_1.Location = new System.Drawing.Point(251, 66);
            this.txt_Spans_1.Name = "txt_Spans_1";
            this.txt_Spans_1.Size = new System.Drawing.Size(64, 21);
            this.txt_Spans_1.TabIndex = 0;
            this.txt_Spans_1.Text = "118.0";
            this.txt_Spans_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Spans_1.TextChanged += new System.EventHandler(this.txt_Ana_DL_length_TextChanged);
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(3, 69);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(86, 13);
            this.label123.TabIndex = 0;
            this.label123.Text = "Span 1 length";
            // 
            // txt_Ana_Span
            // 
            this.txt_Ana_Span.Location = new System.Drawing.Point(251, 121);
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
            this.label1.Location = new System.Drawing.Point(3, 124);
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
            this.groupBox19.Controls.Add(this.txt_box_cs2_IZZ);
            this.groupBox19.Controls.Add(this.label7);
            this.groupBox19.Controls.Add(this.label8);
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
            this.groupBox19.Size = new System.Drawing.Size(515, 182);
            this.groupBox19.TabIndex = 2;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Results";
            // 
            // txt_box_cs2_IZZ
            // 
            this.txt_box_cs2_IZZ.Location = new System.Drawing.Point(233, 110);
            this.txt_box_cs2_IZZ.Name = "txt_box_cs2_IZZ";
            this.txt_box_cs2_IZZ.Size = new System.Drawing.Size(136, 21);
            this.txt_box_cs2_IZZ.TabIndex = 4;
            this.txt_box_cs2_IZZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "in^4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(221, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Moment of Inertia about Z-Axis [IZZ]";
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
            this.tab_cs_diagram2.Controls.Add(this.panel2);
            this.tab_cs_diagram2.ForeColor = System.Drawing.Color.Blue;
            this.tab_cs_diagram2.Location = new System.Drawing.Point(4, 22);
            this.tab_cs_diagram2.Name = "tab_cs_diagram2";
            this.tab_cs_diagram2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cs_diagram2.Size = new System.Drawing.Size(936, 634);
            this.tab_cs_diagram2.TabIndex = 2;
            this.tab_cs_diagram2.Text = "Cross Section Diagram Type 2";
            this.tab_cs_diagram2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox26);
            this.panel2.Controls.Add(this.btn_Show_Section_Resulf);
            this.panel2.Controls.Add(this.rtb_sections);
            this.panel2.Controls.Add(this.btn_open_diagram);
            this.panel2.Controls.Add(this.label226);
            this.panel2.Controls.Add(this.label176);
            this.panel2.Controls.Add(this.groupBox32);
            this.panel2.Location = new System.Drawing.Point(6, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 1207);
            this.panel2.TabIndex = 134;
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.pictureBox4);
            this.groupBox26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox26.Location = new System.Drawing.Point(99, 14);
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
            this.btn_Show_Section_Resulf.Location = new System.Drawing.Point(570, 866);
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
            this.rtb_sections.Location = new System.Drawing.Point(12, 899);
            this.rtb_sections.Name = "rtb_sections";
            this.rtb_sections.ReadOnly = true;
            this.rtb_sections.Size = new System.Drawing.Size(875, 238);
            this.rtb_sections.TabIndex = 3;
            this.rtb_sections.Text = "";
            // 
            // btn_open_diagram
            // 
            this.btn_open_diagram.Location = new System.Drawing.Point(296, 345);
            this.btn_open_diagram.Name = "btn_open_diagram";
            this.btn_open_diagram.Size = new System.Drawing.Size(291, 23);
            this.btn_open_diagram.TabIndex = 133;
            this.btn_open_diagram.Text = "Open Diagram for Cross Section Data Input ";
            this.btn_open_diagram.UseVisualStyleBackColor = true;
            this.btn_open_diagram.Click += new System.EventHandler(this.btn_open_diagram_Click);
            // 
            // label226
            // 
            this.label226.AutoSize = true;
            this.label226.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label226.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label226.ForeColor = System.Drawing.Color.Green;
            this.label226.Location = new System.Drawing.Point(107, 872);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(229, 20);
            this.label226.TabIndex = 124;
            this.label226.Text = "No User Input in this page";
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label176.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.Red;
            this.label176.Location = new System.Drawing.Point(360, 872);
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
            this.groupBox32.Location = new System.Drawing.Point(205, 374);
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
            // tab_section_results
            // 
            this.tab_section_results.Controls.Add(this.panel8);
            this.tab_section_results.Location = new System.Drawing.Point(4, 22);
            this.tab_section_results.Name = "tab_section_results";
            this.tab_section_results.Size = new System.Drawing.Size(936, 634);
            this.tab_section_results.TabIndex = 7;
            this.tab_section_results.Text = "Cross Section Results";
            this.tab_section_results.UseVisualStyleBackColor = true;
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
            this.panel8.Location = new System.Drawing.Point(13, 37);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(883, 359);
            this.panel8.TabIndex = 128;
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
            this.txt_tot_IYY.CausesValidation = false;
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
            this.txt_tot_IXX.CausesValidation = false;
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
            this.tab_analysis.Controls.Add(this.splitContainer1);
            this.tab_analysis.Location = new System.Drawing.Point(4, 22);
            this.tab_analysis.Name = "tab_analysis";
            this.tab_analysis.Padding = new System.Windows.Forms.Padding(3);
            this.tab_analysis.Size = new System.Drawing.Size(936, 634);
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
            this.splitContainer1.Panel2.Controls.Add(this.uc_Results);
            this.splitContainer1.Size = new System.Drawing.Size(930, 628);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 104;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_selfweight);
            this.groupBox2.Controls.Add(this.btn_Process_LL_Analysis);
            this.groupBox2.Controls.Add(this.groupBox51);
            this.groupBox2.Controls.Add(this.groupBox109);
            this.groupBox2.Controls.Add(this.groupBox71);
            this.groupBox2.Controls.Add(this.btn_Ana_DL_create_data);
            this.groupBox2.Controls.Add(this.groupBox70);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(928, 180);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            // 
            // chk_selfweight
            // 
            this.chk_selfweight.AutoSize = true;
            this.chk_selfweight.Checked = true;
            this.chk_selfweight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_selfweight.Location = new System.Drawing.Point(33, 86);
            this.chk_selfweight.Name = "chk_selfweight";
            this.chk_selfweight.Size = new System.Drawing.Size(130, 17);
            this.chk_selfweight.TabIndex = 145;
            this.chk_selfweight.Text = "ADD SELFWEIGHT";
            this.chk_selfweight.UseVisualStyleBackColor = true;
            // 
            // btn_Process_LL_Analysis
            // 
            this.btn_Process_LL_Analysis.Location = new System.Drawing.Point(345, 70);
            this.btn_Process_LL_Analysis.Name = "btn_Process_LL_Analysis";
            this.btn_Process_LL_Analysis.Size = new System.Drawing.Size(149, 47);
            this.btn_Process_LL_Analysis.TabIndex = 104;
            this.btn_Process_LL_Analysis.Text = "Process Analysis\r\n(for all data files)";
            this.btn_Process_LL_Analysis.UseVisualStyleBackColor = true;
            this.btn_Process_LL_Analysis.Click += new System.EventHandler(this.btn_Ana_LL_process_analysis_Click);
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
            this.groupBox51.Location = new System.Drawing.Point(16, 20);
            this.groupBox51.Name = "groupBox51";
            this.groupBox51.Size = new System.Drawing.Size(478, 40);
            this.groupBox51.TabIndex = 144;
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
            this.txt_emod_conc.TextChanged += new System.EventHandler(this.txt_emod_conc_TextChanged);
            // 
            // groupBox109
            // 
            this.groupBox109.Controls.Add(this.btn_view_report);
            this.groupBox109.Controls.Add(this.cmb_long_open_file_analysis);
            this.groupBox109.Controls.Add(this.btn_view_data);
            this.groupBox109.Controls.Add(this.btn_view_postprocess);
            this.groupBox109.Controls.Add(this.btn_view_preprocess);
            this.groupBox109.Location = new System.Drawing.Point(5, 123);
            this.groupBox109.Name = "groupBox109";
            this.groupBox109.Size = new System.Drawing.Size(916, 49);
            this.groupBox109.TabIndex = 106;
            this.groupBox109.TabStop = false;
            this.groupBox109.Text = "Open Analysis File";
            // 
            // btn_view_report
            // 
            this.btn_view_report.Location = new System.Drawing.Point(617, 14);
            this.btn_view_report.Name = "btn_view_report";
            this.btn_view_report.Size = new System.Drawing.Size(146, 26);
            this.btn_view_report.TabIndex = 76;
            this.btn_view_report.Text = "View Analysis Report";
            this.btn_view_report.UseVisualStyleBackColor = true;
            this.btn_view_report.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // cmb_long_open_file_analysis
            // 
            this.cmb_long_open_file_analysis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_long_open_file_analysis.FormattingEnabled = true;
            this.cmb_long_open_file_analysis.Items.AddRange(new object[] {
            "Dead Load Analysis",
            "Live Load Analysis",
            "Total DL+SIDL+LL Analysis"});
            this.cmb_long_open_file_analysis.Location = new System.Drawing.Point(7, 19);
            this.cmb_long_open_file_analysis.Name = "cmb_long_open_file_analysis";
            this.cmb_long_open_file_analysis.Size = new System.Drawing.Size(300, 21);
            this.cmb_long_open_file_analysis.TabIndex = 79;
            this.cmb_long_open_file_analysis.SelectedIndexChanged += new System.EventHandler(this.cmb_long_open_file_preprocess_SelectedIndexChanged);
            // 
            // btn_view_data
            // 
            this.btn_view_data.Location = new System.Drawing.Point(313, 14);
            this.btn_view_data.Name = "btn_view_data";
            this.btn_view_data.Size = new System.Drawing.Size(146, 26);
            this.btn_view_data.TabIndex = 74;
            this.btn_view_data.Text = "View Analysis Data";
            this.btn_view_data.UseVisualStyleBackColor = true;
            this.btn_view_data.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_postprocess
            // 
            this.btn_view_postprocess.Location = new System.Drawing.Point(769, 14);
            this.btn_view_postprocess.Name = "btn_view_postprocess";
            this.btn_view_postprocess.Size = new System.Drawing.Size(146, 26);
            this.btn_view_postprocess.TabIndex = 74;
            this.btn_view_postprocess.Text = "View Post Process";
            this.btn_view_postprocess.UseVisualStyleBackColor = true;
            this.btn_view_postprocess.Click += new System.EventHandler(this.btn_view_data_Click);
            // 
            // btn_view_preprocess
            // 
            this.btn_view_preprocess.Location = new System.Drawing.Point(465, 14);
            this.btn_view_preprocess.Name = "btn_view_preprocess";
            this.btn_view_preprocess.Size = new System.Drawing.Size(146, 26);
            this.btn_view_preprocess.TabIndex = 74;
            this.btn_view_preprocess.Text = "View Pre Process";
            this.btn_view_preprocess.UseVisualStyleBackColor = true;
            this.btn_view_preprocess.Click += new System.EventHandler(this.btn_view_data_Click);
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
            this.groupBox71.Location = new System.Drawing.Point(502, 20);
            this.groupBox71.Name = "groupBox71";
            this.groupBox71.Size = new System.Drawing.Size(419, 44);
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
            this.rbtn_ssprt_fixed.Location = new System.Drawing.Point(81, 20);
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
            this.chk_ssprt_fixed_MZ.Location = new System.Drawing.Point(371, 20);
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
            this.chk_ssprt_fixed_FZ.Location = new System.Drawing.Point(234, 20);
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
            this.chk_ssprt_fixed_MY.Location = new System.Drawing.Point(325, 20);
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
            this.chk_ssprt_fixed_FY.Location = new System.Drawing.Point(191, 20);
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
            this.chk_ssprt_fixed_MX.Location = new System.Drawing.Point(278, 20);
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
            this.chk_ssprt_fixed_FX.Location = new System.Drawing.Point(147, 20);
            this.chk_ssprt_fixed_FX.Name = "chk_ssprt_fixed_FX";
            this.chk_ssprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_ssprt_fixed_FX.TabIndex = 0;
            this.chk_ssprt_fixed_FX.Text = "FX";
            this.chk_ssprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // btn_Ana_DL_create_data
            // 
            this.btn_Ana_DL_create_data.Location = new System.Drawing.Point(190, 70);
            this.btn_Ana_DL_create_data.Name = "btn_Ana_DL_create_data";
            this.btn_Ana_DL_create_data.Size = new System.Drawing.Size(149, 47);
            this.btn_Ana_DL_create_data.TabIndex = 46;
            this.btn_Ana_DL_create_data.Text = "Create Analysis Data\r\n(in each separate file)";
            this.btn_Ana_DL_create_data.UseVisualStyleBackColor = true;
            this.btn_Ana_DL_create_data.Click += new System.EventHandler(this.btn_Ana_create_data_Click);
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
            this.groupBox70.Location = new System.Drawing.Point(502, 70);
            this.groupBox70.Name = "groupBox70";
            this.groupBox70.Size = new System.Drawing.Size(419, 47);
            this.groupBox70.TabIndex = 132;
            this.groupBox70.TabStop = false;
            this.groupBox70.Text = "SUPPORT AT END";
            // 
            // rbtn_esprt_pinned
            // 
            this.rbtn_esprt_pinned.AutoSize = true;
            this.rbtn_esprt_pinned.Location = new System.Drawing.Point(6, 20);
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
            this.rbtn_esprt_fixed.Location = new System.Drawing.Point(81, 20);
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
            this.chk_esprt_fixed_MZ.Location = new System.Drawing.Point(371, 20);
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
            this.chk_esprt_fixed_FZ.Location = new System.Drawing.Point(234, 20);
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
            this.chk_esprt_fixed_MY.Location = new System.Drawing.Point(325, 20);
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
            this.chk_esprt_fixed_FY.Location = new System.Drawing.Point(191, 20);
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
            this.chk_esprt_fixed_MX.Location = new System.Drawing.Point(278, 20);
            this.chk_esprt_fixed_MX.Name = "chk_esprt_fixed_MX";
            this.chk_esprt_fixed_MX.Size = new System.Drawing.Size(43, 17);
            this.chk_esprt_fixed_MX.TabIndex = 0;
            this.chk_esprt_fixed_MX.Text = "MX";
            this.chk_esprt_fixed_MX.UseVisualStyleBackColor = true;
            // 
            // chk_esprt_fixed_FX
            // 
            this.chk_esprt_fixed_FX.AutoSize = true;
            this.chk_esprt_fixed_FX.Location = new System.Drawing.Point(147, 20);
            this.chk_esprt_fixed_FX.Name = "chk_esprt_fixed_FX";
            this.chk_esprt_fixed_FX.Size = new System.Drawing.Size(40, 17);
            this.chk_esprt_fixed_FX.TabIndex = 0;
            this.chk_esprt_fixed_FX.Text = "FX";
            this.chk_esprt_fixed_FX.UseVisualStyleBackColor = true;
            // 
            // uc_Results
            // 
            this.uc_Results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Results.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_Results.Location = new System.Drawing.Point(0, 0);
            this.uc_Results.Name = "uc_Results";
            this.uc_Results.Size = new System.Drawing.Size(928, 443);
            this.uc_Results.TabIndex = 1;
            // 
            // tab_stage
            // 
            this.tab_stage.Controls.Add(this.tc_Stage);
            this.tab_stage.Location = new System.Drawing.Point(4, 22);
            this.tab_stage.Name = "tab_stage";
            this.tab_stage.Size = new System.Drawing.Size(936, 634);
            this.tab_stage.TabIndex = 5;
            this.tab_stage.Text = "Stage Analysis";
            this.tab_stage.UseVisualStyleBackColor = true;
            // 
            // tc_Stage
            // 
            this.tc_Stage.Controls.Add(this.tab_stage1);
            this.tc_Stage.Controls.Add(this.tab_stage2);
            this.tc_Stage.Controls.Add(this.tab_stage3);
            this.tc_Stage.Controls.Add(this.tab_stage4);
            this.tc_Stage.Controls.Add(this.tab_stage5);
            this.tc_Stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Stage.Location = new System.Drawing.Point(0, 0);
            this.tc_Stage.Name = "tc_Stage";
            this.tc_Stage.SelectedIndex = 0;
            this.tc_Stage.Size = new System.Drawing.Size(936, 634);
            this.tc_Stage.TabIndex = 0;
            // 
            // tab_stage1
            // 
            this.tab_stage1.Controls.Add(this.uC_BoxGirder_Stage_AASHTO1);
            this.tab_stage1.Location = new System.Drawing.Point(4, 22);
            this.tab_stage1.Name = "tab_stage1";
            this.tab_stage1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage1.Size = new System.Drawing.Size(928, 608);
            this.tab_stage1.TabIndex = 0;
            this.tab_stage1.Text = "Stage 1";
            this.tab_stage1.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage_AASHTO1
            // 
            this.uC_BoxGirder_Stage_AASHTO1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage_AASHTO1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage_AASHTO1.Location = new System.Drawing.Point(3, 3);
            this.uC_BoxGirder_Stage_AASHTO1.Name = "uC_BoxGirder_Stage_AASHTO1";
            this.uC_BoxGirder_Stage_AASHTO1.Size = new System.Drawing.Size(922, 602);
            this.uC_BoxGirder_Stage_AASHTO1.TabIndex = 0;
            this.uC_BoxGirder_Stage_AASHTO1.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnButtonClick);
            this.uC_BoxGirder_Stage_AASHTO1.OnComboboxSelectedIndexChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnComboboxSelectedIndexChanged);
            this.uC_BoxGirder_Stage_AASHTO1.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnEmodTextChanged);
            // 
            // tab_stage2
            // 
            this.tab_stage2.Controls.Add(this.uC_BoxGirder_Stage_AASHTO2);
            this.tab_stage2.Location = new System.Drawing.Point(4, 22);
            this.tab_stage2.Name = "tab_stage2";
            this.tab_stage2.Padding = new System.Windows.Forms.Padding(3);
            this.tab_stage2.Size = new System.Drawing.Size(928, 608);
            this.tab_stage2.TabIndex = 1;
            this.tab_stage2.Text = "Stage 2";
            this.tab_stage2.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage_AASHTO2
            // 
            this.uC_BoxGirder_Stage_AASHTO2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage_AASHTO2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage_AASHTO2.Location = new System.Drawing.Point(3, 3);
            this.uC_BoxGirder_Stage_AASHTO2.Name = "uC_BoxGirder_Stage_AASHTO2";
            this.uC_BoxGirder_Stage_AASHTO2.Size = new System.Drawing.Size(922, 602);
            this.uC_BoxGirder_Stage_AASHTO2.TabIndex = 0;
            this.uC_BoxGirder_Stage_AASHTO2.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnButtonClick);
            this.uC_BoxGirder_Stage_AASHTO2.OnComboboxSelectedIndexChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnComboboxSelectedIndexChanged);
            this.uC_BoxGirder_Stage_AASHTO2.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnEmodTextChanged);
            // 
            // tab_stage3
            // 
            this.tab_stage3.Controls.Add(this.uC_BoxGirder_Stage_AASHTO3);
            this.tab_stage3.Location = new System.Drawing.Point(4, 22);
            this.tab_stage3.Name = "tab_stage3";
            this.tab_stage3.Size = new System.Drawing.Size(928, 608);
            this.tab_stage3.TabIndex = 2;
            this.tab_stage3.Text = "Stage 3";
            this.tab_stage3.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage_AASHTO3
            // 
            this.uC_BoxGirder_Stage_AASHTO3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage_AASHTO3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage_AASHTO3.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage_AASHTO3.Name = "uC_BoxGirder_Stage_AASHTO3";
            this.uC_BoxGirder_Stage_AASHTO3.Size = new System.Drawing.Size(928, 608);
            this.uC_BoxGirder_Stage_AASHTO3.TabIndex = 0;
            this.uC_BoxGirder_Stage_AASHTO3.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnButtonClick);
            this.uC_BoxGirder_Stage_AASHTO3.OnComboboxSelectedIndexChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnComboboxSelectedIndexChanged);
            this.uC_BoxGirder_Stage_AASHTO3.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnEmodTextChanged);
            // 
            // tab_stage4
            // 
            this.tab_stage4.Controls.Add(this.uC_BoxGirder_Stage_AASHTO4);
            this.tab_stage4.Location = new System.Drawing.Point(4, 22);
            this.tab_stage4.Name = "tab_stage4";
            this.tab_stage4.Size = new System.Drawing.Size(928, 608);
            this.tab_stage4.TabIndex = 3;
            this.tab_stage4.Text = "Stage 4";
            this.tab_stage4.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage_AASHTO4
            // 
            this.uC_BoxGirder_Stage_AASHTO4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage_AASHTO4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage_AASHTO4.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage_AASHTO4.Name = "uC_BoxGirder_Stage_AASHTO4";
            this.uC_BoxGirder_Stage_AASHTO4.Size = new System.Drawing.Size(928, 608);
            this.uC_BoxGirder_Stage_AASHTO4.TabIndex = 0;
            this.uC_BoxGirder_Stage_AASHTO4.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnButtonClick);
            this.uC_BoxGirder_Stage_AASHTO4.OnComboboxSelectedIndexChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnComboboxSelectedIndexChanged);
            this.uC_BoxGirder_Stage_AASHTO4.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnEmodTextChanged);
            // 
            // tab_stage5
            // 
            this.tab_stage5.Controls.Add(this.uC_BoxGirder_Stage_AASHTO5);
            this.tab_stage5.Location = new System.Drawing.Point(4, 22);
            this.tab_stage5.Name = "tab_stage5";
            this.tab_stage5.Size = new System.Drawing.Size(928, 608);
            this.tab_stage5.TabIndex = 4;
            this.tab_stage5.Text = "Stage 5";
            this.tab_stage5.UseVisualStyleBackColor = true;
            // 
            // uC_BoxGirder_Stage_AASHTO5
            // 
            this.uC_BoxGirder_Stage_AASHTO5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxGirder_Stage_AASHTO5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxGirder_Stage_AASHTO5.Location = new System.Drawing.Point(0, 0);
            this.uC_BoxGirder_Stage_AASHTO5.Name = "uC_BoxGirder_Stage_AASHTO5";
            this.uC_BoxGirder_Stage_AASHTO5.Size = new System.Drawing.Size(928, 608);
            this.uC_BoxGirder_Stage_AASHTO5.TabIndex = 0;
            this.uC_BoxGirder_Stage_AASHTO5.OnButtonClick += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnButtonClick);
            this.uC_BoxGirder_Stage_AASHTO5.OnComboboxSelectedIndexChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnComboboxSelectedIndexChanged);
            this.uC_BoxGirder_Stage_AASHTO5.OnEmodTextChanged += new System.EventHandler(this.uC_BoxGirder_Stage_AASHTO1_OnEmodTextChanged);
            // 
            // tab_design
            // 
            this.tab_design.Controls.Add(this.uC_Des_Res);
            this.tab_design.Controls.Add(this.panel3);
            this.tab_design.Location = new System.Drawing.Point(4, 22);
            this.tab_design.Name = "tab_design";
            this.tab_design.Size = new System.Drawing.Size(936, 634);
            this.tab_design.TabIndex = 6;
            this.tab_design.Text = "Design Forces";
            this.tab_design.UseVisualStyleBackColor = true;
            // 
            // uC_Des_Res
            // 
            this.uC_Des_Res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Des_Res.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Des_Res.Location = new System.Drawing.Point(0, 48);
            this.uC_Des_Res.Name = "uC_Des_Res";
            this.uC_Des_Res.Size = new System.Drawing.Size(936, 586);
            this.uC_Des_Res.TabIndex = 84;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_result_summary);
            this.panel3.Controls.Add(this.cmb_design_stage);
            this.panel3.Controls.Add(this.label249);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(936, 48);
            this.panel3.TabIndex = 83;
            // 
            // btn_result_summary
            // 
            this.btn_result_summary.Location = new System.Drawing.Point(470, 14);
            this.btn_result_summary.Name = "btn_result_summary";
            this.btn_result_summary.Size = new System.Drawing.Size(254, 23);
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn13.HeaderText = "Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 80;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn14.HeaderText = "Data";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle11;
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn25.HeaderText = "Name";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 80;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn26.HeaderText = "Data";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 70;
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle14;
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
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn38.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn38.HeaderText = "Name";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.Width = 80;
            // 
            // dataGridViewTextBoxColumn39
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn39.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn39.HeaderText = "Data";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn39.Width = 70;
            // 
            // dataGridViewTextBoxColumn40
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn40.DefaultCellStyle = dataGridViewCellStyle17;
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
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn49.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn49.HeaderText = "Name";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.Width = 80;
            // 
            // dataGridViewTextBoxColumn50
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn50.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn50.HeaderText = "Data";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn50.Width = 70;
            // 
            // dataGridViewTextBoxColumn51
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn51.DefaultCellStyle = dataGridViewCellStyle20;
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
            this.tc_main.ResumeLayout(false);
            this.tab_Analysis_DL.ResumeLayout(false);
            this.tbc_girder.ResumeLayout(false);
            this.tab_user_input.ResumeLayout(false);
            this.tab_user_input.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_diagram)).EndInit();
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seg_tab3_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tab_section_results.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tab_moving_data.ResumeLayout(false);
            this.tab_moving_data.PerformLayout();
            this.groupBox117.ResumeLayout(false);
            this.groupBox117.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_long_loads)).EndInit();
            this.groupBox118.ResumeLayout(false);
            this.groupBox118.PerformLayout();
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
            this.groupBox109.ResumeLayout(false);
            this.groupBox71.ResumeLayout(false);
            this.groupBox71.PerformLayout();
            this.groupBox70.ResumeLayout(false);
            this.groupBox70.PerformLayout();
            this.tab_stage.ResumeLayout(false);
            this.tc_Stage.ResumeLayout(false);
            this.tab_stage1.ResumeLayout(false);
            this.tab_stage2.ResumeLayout(false);
            this.tab_stage3.ResumeLayout(false);
            this.tab_stage4.ResumeLayout(false);
            this.tab_stage5.ResumeLayout(false);
            this.tab_design.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tab_Analysis_DL;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_Ana_Bridge_Width;
        private System.Windows.Forms.TabPage tab_worksheet_design;
        private System.Windows.Forms.TabPage tab_drawings;
        private System.Windows.Forms.Button btn_open_drawings;
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
        private System.Windows.Forms.TabPage tab_cs_diagram2;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RichTextBox rtb_sections;
        private System.Windows.Forms.Button btn_Show_Section_Resulf;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label226;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label228;
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
        private System.Windows.Forms.TextBox txt_Spans_1;
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
        private System.Windows.Forms.ComboBox cmb_long_open_file_analysis;
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
        private System.Windows.Forms.GroupBox groupBox51;
        private System.Windows.Forms.Label label330;
        private System.Windows.Forms.Label label331;
        private System.Windows.Forms.Label label332;
        private System.Windows.Forms.Label label333;
        private System.Windows.Forms.Label label334;
        private System.Windows.Forms.TextBox txt_PR_conc;
        private System.Windows.Forms.TextBox txt_den_conc;
        private System.Windows.Forms.TextBox txt_emod_conc;
        public System.Windows.Forms.CheckBox chk_selfweight;
        private System.Windows.Forms.TabPage tab_section_results;
        private System.Windows.Forms.TabPage tab_stage;
        private System.Windows.Forms.TabPage tab_design;
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
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.GroupBox grb_ana_sw_fp;
        private System.Windows.Forms.Label label531;
        private System.Windows.Forms.Label label529;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label524;
        private System.Windows.Forms.Label label140;
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
        private System.Windows.Forms.PictureBox pic_diagram;
        private System.Windows.Forms.TabControl tc_Stage;
        private System.Windows.Forms.TabPage tab_stage1;
        private System.Windows.Forms.TabPage tab_stage2;
        private System.Windows.Forms.TabPage tab_stage3;
        private System.Windows.Forms.TabPage tab_stage4;
        private System.Windows.Forms.TabPage tab_stage5;
        private UC_BoxGirder_Results_AASHTO uC_Des_Res;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_result_summary;
        private System.Windows.Forms.ComboBox cmb_design_stage;
        private System.Windows.Forms.Label label249;
        private UC_BoxGirder_Stage_AASHTO uC_BoxGirder_Stage_AASHTO1;
        private UC_BoxGirder_Stage_AASHTO uC_BoxGirder_Stage_AASHTO2;
        private UC_BoxGirder_Stage_AASHTO uC_BoxGirder_Stage_AASHTO3;
        private UC_BoxGirder_Stage_AASHTO uC_BoxGirder_Stage_AASHTO4;
        private UC_BoxGirder_Stage_AASHTO uC_BoxGirder_Stage_AASHTO5;
        private System.Windows.Forms.Panel panel2;
        private UC_BoxGirder_Results_AASHTO uc_Results;
        private System.Windows.Forms.TextBox txt_box_cs2_IZZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Spans_2;
        private System.Windows.Forms.Label label9;
    }

}