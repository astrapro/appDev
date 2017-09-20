namespace BridgeAnalysisDesign.Foundation
{
    partial class frm_Foundation
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
            //iApp.LastDesignWorkingFolder = System.IO.Path.GetDirectoryName(iApp.LastDesignWorkingFolder);

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Foundation));
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tab_rcc_well_fnd = new System.Windows.Forms.TabPage();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.label214 = new System.Windows.Forms.Label();
            this.cmb_well_fy = new System.Windows.Forms.ComboBox();
            this.label216 = new System.Windows.Forms.Label();
            this.cmb_well_fck = new System.Windows.Forms.ComboBox();
            this.label221 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.txt_well_sigma_st = new System.Windows.Forms.TextBox();
            this.label231 = new System.Windows.Forms.Label();
            this.label234 = new System.Windows.Forms.Label();
            this.txt_well_sigma_c = new System.Windows.Forms.TextBox();
            this.label235 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label601 = new System.Windows.Forms.Label();
            this.label602 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_well_mon_reinf = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.txt_well_D1_unit_wt = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.txt_well_D3_unit_wt = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.txt_well_D2_unit_wt = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.txt_well_avg_dia = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.txt_well_Tc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_well_Lc = new System.Windows.Forms.TextBox();
            this.txt_well_D3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.txt_well_D2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_well_D1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_well_K = new System.Windows.Forms.ComboBox();
            this.txt_well_K = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_well_L = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_well_di = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_well_Report = new System.Windows.Forms.Button();
            this.btn_well_open_des = new System.Windows.Forms.Button();
            this.btn_well_Process = new System.Windows.Forms.Button();
            this.tab_rcc_well_fnd_LS = new System.Windows.Forms.TabPage();
            this.uC_Well_Foundation1 = new BridgeAnalysisDesign.Foundation.UC_Well_Foundation();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_well_new_design = new System.Windows.Forms.Button();
            this.btn_well_browse = new System.Windows.Forms.Button();
            this.txt_well_project = new System.Windows.Forms.TextBox();
            this.label242 = new System.Windows.Forms.Label();
            this.tab_rcc_pile_foundation = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_pile_new_design = new System.Windows.Forms.Button();
            this.btn_pile_browse = new System.Windows.Forms.Button();
            this.txt_pile_project = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.btn_pile_open_des = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cmb_pile_fy = new System.Windows.Forms.ComboBox();
            this.label85 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_PL = new System.Windows.Forms.TextBox();
            this.cmb_pile_fck = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_pile_sigma_st = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_gamma_c = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txt_pile_sigma_c = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_Np = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txt_P = new System.Windows.Forms.TextBox();
            this.txt_K = new System.Windows.Forms.TextBox();
            this.txt_max_spacing = new System.Windows.Forms.TextBox();
            this.txt_lateral_dia = new System.Windows.Forms.TextBox();
            this.txt_main_dia = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.txt_d_dash = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.txt_gamma_sub = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_AM = new System.Windows.Forms.TextBox();
            this.txt_D = new System.Windows.Forms.TextBox();
            this.txt_SL = new System.Windows.Forms.TextBox();
            this.txt_FL = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_N = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_PCBL = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_FS = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txt_cap_spacing = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.cmb_pcap_fy = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.cmb_pcap_fck = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.txt_pcap_sigma_st = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.txt_pcap_sigma_c = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txt_m = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.txt_L3 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.txt_L2 = new System.Windows.Forms.TextBox();
            this.txt_L1 = new System.Windows.Forms.TextBox();
            this.txt_DPC = new System.Windows.Forms.TextBox();
            this.txt_BPr = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txt_LPr = new System.Windows.Forms.TextBox();
            this.txt_BPC = new System.Windows.Forms.TextBox();
            this.txt_LPC = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.txt_clear_cover = new System.Windows.Forms.TextBox();
            this.txt_shear_dia = new System.Windows.Forms.TextBox();
            this.txt_d3 = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_d2 = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txt_d1 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.txt_F = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label27 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label87 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.col_SL_N = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Layers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Depth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Thickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ang_fric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cons_adhe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Cohesion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_gama_sub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_pile_process = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.tab_worksheet_design = new System.Windows.Forms.TabPage();
            this.btn_worksheet_open = new System.Windows.Forms.Button();
            this.btn_worksheet_sheet_pile = new System.Windows.Forms.Button();
            this.btn_worksheet_well = new System.Windows.Forms.Button();
            this.tab_drawing = new System.Windows.Forms.TabPage();
            this.label37 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.btn_dwg_pile_interactive = new System.Windows.Forms.Button();
            this.btn_dwg_well_interactive = new System.Windows.Forms.Button();
            this.btn_dwg_well_default = new System.Windows.Forms.Button();
            this.pic_well = new System.Windows.Forms.PictureBox();
            this.pic_pile = new System.Windows.Forms.PictureBox();
            this.tc_main.SuspendLayout();
            this.tab_rcc_well_fnd.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tab_rcc_well_fnd_LS.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tab_rcc_pile_foundation.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel2.SuspendLayout();
            this.tab_worksheet_design.SuspendLayout();
            this.tab_drawing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_well)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pile)).BeginInit();
            this.SuspendLayout();
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tab_rcc_well_fnd);
            this.tc_main.Controls.Add(this.tab_rcc_well_fnd_LS);
            this.tc_main.Controls.Add(this.tab_rcc_pile_foundation);
            this.tc_main.Controls.Add(this.tab_worksheet_design);
            this.tc_main.Controls.Add(this.tab_drawing);
            this.tc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_main.Location = new System.Drawing.Point(0, 0);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(916, 650);
            this.tc_main.TabIndex = 0;
            // 
            // tab_rcc_well_fnd
            // 
            this.tab_rcc_well_fnd.Controls.Add(this.groupBox41);
            this.tab_rcc_well_fnd.Controls.Add(this.groupBox1);
            this.tab_rcc_well_fnd.Controls.Add(this.panel1);
            this.tab_rcc_well_fnd.Controls.Add(this.pic_well);
            this.tab_rcc_well_fnd.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_well_fnd.Name = "tab_rcc_well_fnd";
            this.tab_rcc_well_fnd.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_well_fnd.Size = new System.Drawing.Size(908, 624);
            this.tab_rcc_well_fnd.TabIndex = 0;
            this.tab_rcc_well_fnd.Text = "RCC Well Foundation Design";
            this.tab_rcc_well_fnd.UseVisualStyleBackColor = true;
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.label214);
            this.groupBox41.Controls.Add(this.cmb_well_fy);
            this.groupBox41.Controls.Add(this.label216);
            this.groupBox41.Controls.Add(this.cmb_well_fck);
            this.groupBox41.Controls.Add(this.label221);
            this.groupBox41.Controls.Add(this.label227);
            this.groupBox41.Controls.Add(this.label229);
            this.groupBox41.Controls.Add(this.txt_well_sigma_st);
            this.groupBox41.Controls.Add(this.label231);
            this.groupBox41.Controls.Add(this.label234);
            this.groupBox41.Controls.Add(this.txt_well_sigma_c);
            this.groupBox41.Controls.Add(this.label235);
            this.groupBox41.Location = new System.Drawing.Point(481, 6);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Size = new System.Drawing.Size(410, 120);
            this.groupBox41.TabIndex = 115;
            this.groupBox41.TabStop = false;
            // 
            // label214
            // 
            this.label214.AutoSize = true;
            this.label214.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label214.Location = new System.Drawing.Point(339, 44);
            this.label214.Name = "label214";
            this.label214.Size = new System.Drawing.Size(67, 13);
            this.label214.TabIndex = 81;
            this.label214.Text = "N/sq.mm";
            // 
            // cmb_well_fy
            // 
            this.cmb_well_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_well_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_well_fy.FormattingEnabled = true;
            this.cmb_well_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_well_fy.Location = new System.Drawing.Point(272, 65);
            this.cmb_well_fy.Name = "cmb_well_fy";
            this.cmb_well_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_well_fy.TabIndex = 15;
            this.cmb_well_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Location = new System.Drawing.Point(5, 39);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(211, 13);
            this.label216.TabIndex = 79;
            this.label216.Text = "Allowable Flexural Stress in Concrete [σ_c] ";
            // 
            // cmb_well_fck
            // 
            this.cmb_well_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_well_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_well_fck.FormattingEnabled = true;
            this.cmb_well_fck.Items.AddRange(new object[] {
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
            this.cmb_well_fck.Location = new System.Drawing.Point(272, 16);
            this.cmb_well_fck.Name = "cmb_well_fck";
            this.cmb_well_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_well_fck.TabIndex = 13;
            this.cmb_well_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(248, 67);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(23, 14);
            this.label221.TabIndex = 59;
            this.label221.Text = "Fe";
            // 
            // label227
            // 
            this.label227.AutoSize = true;
            this.label227.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label227.Location = new System.Drawing.Point(253, 18);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(18, 14);
            this.label227.TabIndex = 58;
            this.label227.Text = "M";
            // 
            // label229
            // 
            this.label229.AutoSize = true;
            this.label229.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label229.Location = new System.Drawing.Point(339, 98);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(67, 13);
            this.label229.TabIndex = 57;
            this.label229.Text = "N/sq.mm";
            // 
            // txt_well_sigma_st
            // 
            this.txt_well_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_well_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_well_sigma_st.Location = new System.Drawing.Point(239, 92);
            this.txt_well_sigma_st.Name = "txt_well_sigma_st";
            this.txt_well_sigma_st.ReadOnly = true;
            this.txt_well_sigma_st.Size = new System.Drawing.Size(94, 22);
            this.txt_well_sigma_st.TabIndex = 16;
            this.txt_well_sigma_st.Text = "200";
            this.txt_well_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label231.Location = new System.Drawing.Point(5, 93);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(159, 13);
            this.label231.TabIndex = 55;
            this.label231.Text = "Permissible Stress in Steel [σ_st]";
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Location = new System.Drawing.Point(5, 68);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(80, 13);
            this.label234.TabIndex = 15;
            this.label234.Text = "Steel Grade [fy]";
            // 
            // txt_well_sigma_c
            // 
            this.txt_well_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_well_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_well_sigma_c.Location = new System.Drawing.Point(239, 40);
            this.txt_well_sigma_c.Name = "txt_well_sigma_c";
            this.txt_well_sigma_c.ReadOnly = true;
            this.txt_well_sigma_c.Size = new System.Drawing.Size(94, 22);
            this.txt_well_sigma_c.TabIndex = 14;
            this.txt_well_sigma_c.Text = "83.3";
            this.txt_well_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label235
            // 
            this.label235.AutoSize = true;
            this.label235.Location = new System.Drawing.Point(5, 19);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(106, 13);
            this.label235.TabIndex = 13;
            this.label235.Text = "Concrete Grade [fck]";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label106);
            this.groupBox1.Controls.Add(this.label108);
            this.groupBox1.Controls.Add(this.label601);
            this.groupBox1.Controls.Add(this.label602);
            this.groupBox1.Controls.Add(this.label112);
            this.groupBox1.Controls.Add(this.label104);
            this.groupBox1.Controls.Add(this.label102);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label111);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_well_mon_reinf);
            this.groupBox1.Controls.Add(this.label105);
            this.groupBox1.Controls.Add(this.txt_well_D1_unit_wt);
            this.groupBox1.Controls.Add(this.label107);
            this.groupBox1.Controls.Add(this.txt_well_D3_unit_wt);
            this.groupBox1.Controls.Add(this.label110);
            this.groupBox1.Controls.Add(this.txt_well_D2_unit_wt);
            this.groupBox1.Controls.Add(this.label103);
            this.groupBox1.Controls.Add(this.txt_well_avg_dia);
            this.groupBox1.Controls.Add(this.label101);
            this.groupBox1.Controls.Add(this.txt_well_Tc);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_well_Lc);
            this.groupBox1.Controls.Add(this.txt_well_D3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label109);
            this.groupBox1.Controls.Add(this.txt_well_D2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_well_D1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmb_well_K);
            this.groupBox1.Controls.Add(this.txt_well_K);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_well_L);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_well_di);
            this.groupBox1.Location = new System.Drawing.Point(15, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 422);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER\'S DATA";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(346, 386);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(62, 13);
            this.label106.TabIndex = 23;
            this.label106.Text = "kg/cu.m";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(346, 183);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(43, 13);
            this.label108.TabIndex = 23;
            this.label108.Text = "kg/m";
            // 
            // label601
            // 
            this.label601.AutoSize = true;
            this.label601.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label601.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label601.ForeColor = System.Drawing.Color.Red;
            this.label601.Location = new System.Drawing.Point(159, 16);
            this.label601.Name = "label601";
            this.label601.Size = new System.Drawing.Size(218, 18);
            this.label601.TabIndex = 134;
            this.label601.Text = "Default Sample Data are shown";
            // 
            // label602
            // 
            this.label602.AutoSize = true;
            this.label602.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label602.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label602.ForeColor = System.Drawing.Color.Green;
            this.label602.Location = new System.Drawing.Point(18, 16);
            this.label602.Name = "label602";
            this.label602.Size = new System.Drawing.Size(135, 18);
            this.label602.TabIndex = 133;
            this.label602.Text = "All User Input Data";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(346, 284);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(43, 13);
            this.label112.TabIndex = 23;
            this.label112.Text = "kg/m";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(346, 234);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(43, 13);
            this.label104.TabIndex = 23;
            this.label104.Text = "kg/m";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(346, 362);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(19, 13);
            this.label102.TabIndex = 23;
            this.label102.Text = "m";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(346, 335);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 23;
            this.label20.Text = "mm";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(346, 311);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 23;
            this.label19.Text = "mm";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(346, 257);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(31, 13);
            this.label111.TabIndex = 23;
            this.label111.Text = "mm";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(346, 207);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "mm";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(346, 156);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "mm";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(346, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "m";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(346, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "m";
            // 
            // txt_well_mon_reinf
            // 
            this.txt_well_mon_reinf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_mon_reinf.Location = new System.Drawing.Point(259, 383);
            this.txt_well_mon_reinf.Name = "txt_well_mon_reinf";
            this.txt_well_mon_reinf.Size = new System.Drawing.Size(81, 21);
            this.txt_well_mon_reinf.TabIndex = 20;
            this.txt_well_mon_reinf.Text = "72";
            this.txt_well_mon_reinf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(20, 386);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(142, 13);
            this.label105.TabIndex = 19;
            this.label105.Text = "Minimum reinforcement";
            // 
            // txt_well_D1_unit_wt
            // 
            this.txt_well_D1_unit_wt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D1_unit_wt.Location = new System.Drawing.Point(259, 180);
            this.txt_well_D1_unit_wt.Name = "txt_well_D1_unit_wt";
            this.txt_well_D1_unit_wt.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D1_unit_wt.TabIndex = 20;
            this.txt_well_D1_unit_wt.Text = "1.58";
            this.txt_well_D1_unit_wt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(20, 183);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(226, 13);
            this.label107.TabIndex = 19;
            this.label107.Text = "Unit Weight of Main Reinforcement bar";
            // 
            // txt_well_D3_unit_wt
            // 
            this.txt_well_D3_unit_wt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D3_unit_wt.Location = new System.Drawing.Point(259, 281);
            this.txt_well_D3_unit_wt.Name = "txt_well_D3_unit_wt";
            this.txt_well_D3_unit_wt.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D3_unit_wt.TabIndex = 20;
            this.txt_well_D3_unit_wt.Text = "0.39";
            this.txt_well_D3_unit_wt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.Location = new System.Drawing.Point(20, 284);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(130, 13);
            this.label110.TabIndex = 19;
            this.label110.Text = "Unit Weight of Tie bar";
            // 
            // txt_well_D2_unit_wt
            // 
            this.txt_well_D2_unit_wt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D2_unit_wt.Location = new System.Drawing.Point(259, 231);
            this.txt_well_D2_unit_wt.Name = "txt_well_D2_unit_wt";
            this.txt_well_D2_unit_wt.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D2_unit_wt.TabIndex = 20;
            this.txt_well_D2_unit_wt.Text = "0.62";
            this.txt_well_D2_unit_wt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(20, 234);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(142, 13);
            this.label103.TabIndex = 19;
            this.label103.Text = "Unit Weight of Hoop bar";
            // 
            // txt_well_avg_dia
            // 
            this.txt_well_avg_dia.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_avg_dia.Location = new System.Drawing.Point(259, 359);
            this.txt_well_avg_dia.Name = "txt_well_avg_dia";
            this.txt_well_avg_dia.Size = new System.Drawing.Size(81, 21);
            this.txt_well_avg_dia.TabIndex = 20;
            this.txt_well_avg_dia.Text = "3.1";
            this.txt_well_avg_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.Location = new System.Drawing.Point(20, 362);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(167, 13);
            this.label101.TabIndex = 19;
            this.label101.Text = "Hoops of average diameter ";
            // 
            // txt_well_Tc
            // 
            this.txt_well_Tc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_Tc.Location = new System.Drawing.Point(259, 332);
            this.txt_well_Tc.Name = "txt_well_Tc";
            this.txt_well_Tc.Size = new System.Drawing.Size(81, 21);
            this.txt_well_Tc.TabIndex = 20;
            this.txt_well_Tc.Text = "150";
            this.txt_well_Tc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 335);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(195, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Thickness of Curb at bottom [Tc]";
            // 
            // txt_well_Lc
            // 
            this.txt_well_Lc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_Lc.Location = new System.Drawing.Point(259, 308);
            this.txt_well_Lc.Name = "txt_well_Lc";
            this.txt_well_Lc.Size = new System.Drawing.Size(81, 21);
            this.txt_well_Lc.TabIndex = 18;
            this.txt_well_Lc.Text = "1000";
            this.txt_well_Lc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_well_D3
            // 
            this.txt_well_D3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D3.Location = new System.Drawing.Point(259, 254);
            this.txt_well_D3.Name = "txt_well_D3";
            this.txt_well_D3.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D3.TabIndex = 16;
            this.txt_well_D3.Text = "8";
            this.txt_well_D3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Depth of Curb [Lc]";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(20, 257);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(155, 13);
            this.label109.TabIndex = 15;
            this.label109.Text = "Diameter of Tie bars [D3]";
            // 
            // txt_well_D2
            // 
            this.txt_well_D2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D2.Location = new System.Drawing.Point(259, 204);
            this.txt_well_D2.Name = "txt_well_D2";
            this.txt_well_D2.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D2.TabIndex = 16;
            this.txt_well_D2.Text = "10";
            this.txt_well_D2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Diameter of Main Hoop Steel bars [D2]";
            // 
            // txt_well_D1
            // 
            this.txt_well_D1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_D1.Location = new System.Drawing.Point(259, 153);
            this.txt_well_D1.Name = "txt_well_D1";
            this.txt_well_D1.Size = new System.Drawing.Size(81, 21);
            this.txt_well_D1.TabIndex = 14;
            this.txt_well_D1.Text = "16";
            this.txt_well_D1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(232, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Diameter of Main Reinforcement Steel bars [D1]";
            // 
            // cmb_well_K
            // 
            this.cmb_well_K.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_well_K.FormattingEnabled = true;
            this.cmb_well_K.Items.AddRange(new object[] {
            "Single Circular Dumb Bell (Sandy Soil)",
            "Single Circular Dumb Bell (Clayee Soil)",
            "Twin D-type Cell Well (Sandy Soil)",
            "Twin D-type Cell Well (Clayee Soil)"});
            this.cmb_well_K.Location = new System.Drawing.Point(23, 118);
            this.cmb_well_K.Name = "cmb_well_K";
            this.cmb_well_K.Size = new System.Drawing.Size(231, 21);
            this.cmb_well_K.TabIndex = 12;
            // 
            // txt_well_K
            // 
            this.txt_well_K.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_K.Location = new System.Drawing.Point(281, 118);
            this.txt_well_K.Name = "txt_well_K";
            this.txt_well_K.Size = new System.Drawing.Size(59, 21);
            this.txt_well_K.TabIndex = 11;
            this.txt_well_K.Text = "0.030";
            this.txt_well_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(234, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Values of Constant \'K\' for Cement Concrete Well";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(256, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "K = ";
            // 
            // txt_well_L
            // 
            this.txt_well_L.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_L.Location = new System.Drawing.Point(259, 72);
            this.txt_well_L.Name = "txt_well_L";
            this.txt_well_L.Size = new System.Drawing.Size(81, 21);
            this.txt_well_L.TabIndex = 3;
            this.txt_well_L.Text = "25.0";
            this.txt_well_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Depth of Well below Bed Level [L]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Internal Diameter of Well [di]";
            // 
            // txt_well_di
            // 
            this.txt_well_di.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_di.Location = new System.Drawing.Point(259, 39);
            this.txt_well_di.Name = "txt_well_di";
            this.txt_well_di.Size = new System.Drawing.Size(81, 21);
            this.txt_well_di.TabIndex = 0;
            this.txt_well_di.Text = "2.5";
            this.txt_well_di.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_well_Report);
            this.panel1.Controls.Add(this.btn_well_open_des);
            this.panel1.Controls.Add(this.btn_well_Process);
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(125, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 51);
            this.panel1.TabIndex = 11;
            // 
            // btn_well_Report
            // 
            this.btn_well_Report.Location = new System.Drawing.Point(326, 4);
            this.btn_well_Report.Name = "btn_well_Report";
            this.btn_well_Report.Size = new System.Drawing.Size(126, 38);
            this.btn_well_Report.TabIndex = 2;
            this.btn_well_Report.Text = "Report";
            this.btn_well_Report.UseVisualStyleBackColor = true;
            this.btn_well_Report.Click += new System.EventHandler(this.btn_well_Report_Click);
            // 
            // btn_well_open_des
            // 
            this.btn_well_open_des.Location = new System.Drawing.Point(15, 3);
            this.btn_well_open_des.Name = "btn_well_open_des";
            this.btn_well_open_des.Size = new System.Drawing.Size(138, 36);
            this.btn_well_open_des.TabIndex = 1;
            this.btn_well_open_des.Text = "Open Previous Design [\"ASTRA_Data_Input.txt\"]";
            this.btn_well_open_des.UseVisualStyleBackColor = true;
            this.btn_well_open_des.Visible = false;
            this.btn_well_open_des.Click += new System.EventHandler(this.btn_well_open_des_Click);
            // 
            // btn_well_Process
            // 
            this.btn_well_Process.Location = new System.Drawing.Point(187, 4);
            this.btn_well_Process.Name = "btn_well_Process";
            this.btn_well_Process.Size = new System.Drawing.Size(126, 38);
            this.btn_well_Process.TabIndex = 1;
            this.btn_well_Process.Text = "Process";
            this.btn_well_Process.UseVisualStyleBackColor = true;
            this.btn_well_Process.Click += new System.EventHandler(this.btn_well_Process_Click);
            // 
            // tab_rcc_well_fnd_LS
            // 
            this.tab_rcc_well_fnd_LS.Controls.Add(this.uC_Well_Foundation1);
            this.tab_rcc_well_fnd_LS.Controls.Add(this.panel4);
            this.tab_rcc_well_fnd_LS.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_well_fnd_LS.Name = "tab_rcc_well_fnd_LS";
            this.tab_rcc_well_fnd_LS.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_well_fnd_LS.Size = new System.Drawing.Size(908, 624);
            this.tab_rcc_well_fnd_LS.TabIndex = 5;
            this.tab_rcc_well_fnd_LS.Text = "RCC Well Foundation Design";
            this.tab_rcc_well_fnd_LS.UseVisualStyleBackColor = true;
            // 
            // uC_Well_Foundation1
            // 
            this.uC_Well_Foundation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Well_Foundation1.Location = new System.Drawing.Point(3, 59);
            this.uC_Well_Foundation1.Name = "uC_Well_Foundation1";
            this.uC_Well_Foundation1.Project_Name = "";
            this.uC_Well_Foundation1.Size = new System.Drawing.Size(902, 562);
            this.uC_Well_Foundation1.TabIndex = 0;
            this.uC_Well_Foundation1.user_path = "";
            this.uC_Well_Foundation1.OnProcees += new System.EventHandler(this.uC_Well_Foundation1_OnProcees);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_well_new_design);
            this.panel4.Controls.Add(this.btn_well_browse);
            this.panel4.Controls.Add(this.txt_well_project);
            this.panel4.Controls.Add(this.label242);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(902, 56);
            this.panel4.TabIndex = 176;
            // 
            // btn_well_new_design
            // 
            this.btn_well_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_well_new_design.Name = "btn_well_new_design";
            this.btn_well_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_well_new_design.TabIndex = 188;
            this.btn_well_new_design.Text = "New Design";
            this.btn_well_new_design.UseVisualStyleBackColor = true;
            this.btn_well_new_design.Click += new System.EventHandler(this.btn_well_new_design_Click);
            // 
            // btn_well_browse
            // 
            this.btn_well_browse.Location = new System.Drawing.Point(242, 4);
            this.btn_well_browse.Name = "btn_well_browse";
            this.btn_well_browse.Size = new System.Drawing.Size(121, 24);
            this.btn_well_browse.TabIndex = 189;
            this.btn_well_browse.Text = "Open Design";
            this.btn_well_browse.UseVisualStyleBackColor = true;
            this.btn_well_browse.Click += new System.EventHandler(this.btn_well_new_design_Click);
            // 
            // txt_well_project
            // 
            this.txt_well_project.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_well_project.Location = new System.Drawing.Point(104, 30);
            this.txt_well_project.Name = "txt_well_project";
            this.txt_well_project.Size = new System.Drawing.Size(258, 22);
            this.txt_well_project.TabIndex = 186;
            this.txt_well_project.Text = "RCC Well Foundation Design 1";
            // 
            // label242
            // 
            this.label242.AutoSize = true;
            this.label242.Location = new System.Drawing.Point(5, 34);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(77, 13);
            this.label242.TabIndex = 187;
            this.label242.Text = "Project Name :";
            // 
            // tab_rcc_pile_foundation
            // 
            this.tab_rcc_pile_foundation.Controls.Add(this.tabControl2);
            this.tab_rcc_pile_foundation.Controls.Add(this.panel2);
            this.tab_rcc_pile_foundation.Location = new System.Drawing.Point(4, 22);
            this.tab_rcc_pile_foundation.Name = "tab_rcc_pile_foundation";
            this.tab_rcc_pile_foundation.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rcc_pile_foundation.Size = new System.Drawing.Size(908, 624);
            this.tab_rcc_pile_foundation.TabIndex = 1;
            this.tab_rcc_pile_foundation.Text = "Pile Foundation Design";
            this.tab_rcc_pile_foundation.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(902, 592);
            this.tabControl2.TabIndex = 166;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.label70);
            this.tabPage1.Controls.Add(this.label79);
            this.tabPage1.Controls.Add(this.btn_pile_open_des);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(894, 566);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pile Foundation User Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_pile_new_design);
            this.panel3.Controls.Add(this.btn_pile_browse);
            this.panel3.Controls.Add(this.txt_pile_project);
            this.panel3.Controls.Add(this.label113);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(405, 56);
            this.panel3.TabIndex = 176;
            // 
            // btn_pile_new_design
            // 
            this.btn_pile_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_pile_new_design.Name = "btn_pile_new_design";
            this.btn_pile_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_pile_new_design.TabIndex = 188;
            this.btn_pile_new_design.Text = "New Design";
            this.btn_pile_new_design.UseVisualStyleBackColor = true;
            this.btn_pile_new_design.Click += new System.EventHandler(this.btn_pile_new_design_Click);
            // 
            // btn_pile_browse
            // 
            this.btn_pile_browse.Location = new System.Drawing.Point(242, 4);
            this.btn_pile_browse.Name = "btn_pile_browse";
            this.btn_pile_browse.Size = new System.Drawing.Size(121, 24);
            this.btn_pile_browse.TabIndex = 189;
            this.btn_pile_browse.Text = "Open Design";
            this.btn_pile_browse.UseVisualStyleBackColor = true;
            this.btn_pile_browse.Click += new System.EventHandler(this.btn_pile_new_design_Click);
            // 
            // txt_pile_project
            // 
            this.txt_pile_project.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pile_project.Location = new System.Drawing.Point(104, 30);
            this.txt_pile_project.Name = "txt_pile_project";
            this.txt_pile_project.Size = new System.Drawing.Size(258, 22);
            this.txt_pile_project.TabIndex = 186;
            this.txt_pile_project.Text = "RCC Well Foundation Design 1";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(5, 34);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(77, 13);
            this.label113.TabIndex = 187;
            this.label113.Text = "Project Name :";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label70.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.Red;
            this.label70.Location = new System.Drawing.Point(609, 3);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(218, 18);
            this.label70.TabIndex = 169;
            this.label70.Text = "Default Sample Data are shown";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label79.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.ForeColor = System.Drawing.Color.Green;
            this.label79.Location = new System.Drawing.Point(468, 3);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(135, 18);
            this.label79.TabIndex = 168;
            this.label79.Text = "All User Input Data";
            // 
            // btn_pile_open_des
            // 
            this.btn_pile_open_des.Location = new System.Drawing.Point(545, 3);
            this.btn_pile_open_des.Name = "btn_pile_open_des";
            this.btn_pile_open_des.Size = new System.Drawing.Size(258, 12);
            this.btn_pile_open_des.TabIndex = 116;
            this.btn_pile_open_des.Text = "Open Previous Design [\"ASTRA_Data_Input.txt\"]";
            this.btn_pile_open_des.UseVisualStyleBackColor = true;
            this.btn_pile_open_des.Visible = false;
            this.btn_pile_open_des.Click += new System.EventHandler(this.btn_well_open_des_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.cmb_pile_fy);
            this.groupBox3.Controls.Add(this.label85);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_PL);
            this.groupBox3.Controls.Add(this.cmb_pile_fck);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label86);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.txt_pile_sigma_st);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.txt_gamma_c);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.txt_pile_sigma_c);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label94);
            this.groupBox3.Controls.Add(this.label92);
            this.groupBox3.Controls.Add(this.label90);
            this.groupBox3.Controls.Add(this.label88);
            this.groupBox3.Controls.Add(this.label46);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.txt_Np);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.txt_P);
            this.groupBox3.Controls.Add(this.txt_K);
            this.groupBox3.Controls.Add(this.txt_max_spacing);
            this.groupBox3.Controls.Add(this.txt_lateral_dia);
            this.groupBox3.Controls.Add(this.txt_main_dia);
            this.groupBox3.Controls.Add(this.label93);
            this.groupBox3.Controls.Add(this.txt_d_dash);
            this.groupBox3.Controls.Add(this.label91);
            this.groupBox3.Controls.Add(this.txt_gamma_sub);
            this.groupBox3.Controls.Add(this.label89);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.txt_AM);
            this.groupBox3.Controls.Add(this.txt_D);
            this.groupBox3.Controls.Add(this.txt_SL);
            this.groupBox3.Controls.Add(this.txt_FL);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txt_N);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txt_PCBL);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.txt_FS);
            this.groupBox3.Location = new System.Drawing.Point(3, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 507);
            this.groupBox3.TabIndex = 167;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pile Input Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "N/sq.mm";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(6, 53);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(159, 13);
            this.label26.TabIndex = 119;
            this.label26.Text = "(Load from Deck + Girder + Pier)";
            // 
            // cmb_pile_fy
            // 
            this.cmb_pile_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pile_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_pile_fy.FormattingEnabled = true;
            this.cmb_pile_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_pile_fy.Location = new System.Drawing.Point(259, 271);
            this.cmb_pile_fy.Name = "cmb_pile_fy";
            this.cmb_pile_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_pile_fy.TabIndex = 15;
            this.cmb_pile_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(326, 162);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(19, 14);
            this.label85.TabIndex = 118;
            this.label85.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Allowable Flexural Stress in Concrete [σ_c] ";
            // 
            // txt_PL
            // 
            this.txt_PL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PL.Location = new System.Drawing.Point(247, 159);
            this.txt_PL.Name = "txt_PL";
            this.txt_PL.Size = new System.Drawing.Size(75, 22);
            this.txt_PL.TabIndex = 117;
            this.txt_PL.Text = "25.0";
            this.txt_PL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_PL.TextChanged += new System.EventHandler(this.txt_PCBL_TextChanged);
            // 
            // cmb_pile_fck
            // 
            this.cmb_pile_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pile_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_pile_fck.FormattingEnabled = true;
            this.cmb_pile_fck.Items.AddRange(new object[] {
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
            this.cmb_pile_fck.Location = new System.Drawing.Point(259, 226);
            this.cmb_pile_fck.Name = "cmb_pile_fck";
            this.cmb_pile_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_pile_fck.TabIndex = 13;
            this.cmb_pile_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(235, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 14);
            this.label11.TabIndex = 59;
            this.label11.Text = "Fe";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(6, 163);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(94, 13);
            this.label86.TabIndex = 116;
            this.label86.Text = "Length of Pile [PL)";
            this.label86.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(240, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 14);
            this.label12.TabIndex = 58;
            this.label12.Text = "M";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(326, 301);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "N/sq.mm";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(86, 13);
            this.label29.TabIndex = 71;
            this.label29.Text = "Pile Diameter [D]";
            // 
            // txt_pile_sigma_st
            // 
            this.txt_pile_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_pile_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pile_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_pile_sigma_st.Location = new System.Drawing.Point(245, 295);
            this.txt_pile_sigma_st.Name = "txt_pile_sigma_st";
            this.txt_pile_sigma_st.ReadOnly = true;
            this.txt_pile_sigma_st.Size = new System.Drawing.Size(75, 22);
            this.txt_pile_sigma_st.TabIndex = 16;
            this.txt_pile_sigma_st.Text = "200";
            this.txt_pile_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(5, 343);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(79, 13);
            this.label47.TabIndex = 99;
            this.label47.Text = "Total Piles [Np]";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(9, 301);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(159, 13);
            this.label16.TabIndex = 55;
            this.label16.Text = "Permissible Stress in Steel [σ_st]";
            // 
            // txt_gamma_c
            // 
            this.txt_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_c.Location = new System.Drawing.Point(245, 319);
            this.txt_gamma_c.Name = "txt_gamma_c";
            this.txt_gamma_c.Size = new System.Drawing.Size(75, 22);
            this.txt_gamma_c.TabIndex = 98;
            this.txt_gamma_c.Text = "2.5";
            this.txt_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(9, 274);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(80, 13);
            this.label33.TabIndex = 15;
            this.label33.Text = "Steel Grade [fy]";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(324, 69);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(49, 14);
            this.label41.TabIndex = 109;
            this.label41.Text = "Ton-m";
            // 
            // txt_pile_sigma_c
            // 
            this.txt_pile_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_pile_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pile_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_pile_sigma_c.Location = new System.Drawing.Point(245, 248);
            this.txt_pile_sigma_c.Name = "txt_pile_sigma_c";
            this.txt_pile_sigma_c.ReadOnly = true;
            this.txt_pile_sigma_c.Size = new System.Drawing.Size(75, 22);
            this.txt_pile_sigma_c.TabIndex = 14;
            this.txt_pile_sigma_c.Text = "83.3";
            this.txt_pile_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 323);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(148, 13);
            this.label36.TabIndex = 97;
            this.label36.Text = "Unit Weight of Concrete [γ_c]";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(9, 234);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(106, 13);
            this.label35.TabIndex = 13;
            this.label35.Text = "Concrete Grade [fck]";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(325, 37);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(31, 14);
            this.label40.TabIndex = 110;
            this.label40.Text = "Ton";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(326, 485);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(31, 14);
            this.label94.TabIndex = 108;
            this.label94.Text = "mm";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(326, 461);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(31, 14);
            this.label92.TabIndex = 108;
            this.label92.Text = "mm";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(326, 438);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(31, 14);
            this.label90.TabIndex = 108;
            this.label90.Text = "mm";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(326, 415);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(31, 14);
            this.label88.TabIndex = 108;
            this.label88.Text = "mm";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(326, 392);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(70, 14);
            this.label46.TabIndex = 108;
            this.label46.Text = "Ton/cu.m";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(326, 322);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(58, 14);
            this.label45.TabIndex = 107;
            this.label45.Text = "Ton/cm";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(326, 184);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(19, 14);
            this.label44.TabIndex = 113;
            this.label44.Text = "m";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(326, 207);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(19, 14);
            this.label43.TabIndex = 114;
            this.label43.Text = "m";
            // 
            // txt_Np
            // 
            this.txt_Np.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Np.Location = new System.Drawing.Point(245, 342);
            this.txt_Np.Name = "txt_Np";
            this.txt_Np.Size = new System.Drawing.Size(75, 22);
            this.txt_Np.TabIndex = 100;
            this.txt_Np.Text = "8";
            this.txt_Np.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(326, 139);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(19, 14);
            this.label42.TabIndex = 111;
            this.label42.Text = "m";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(4, 93);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(190, 13);
            this.label32.TabIndex = 75;
            this.label32.Text = "Coefficient of Active Earth Pressure [K]";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(325, 14);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(19, 14);
            this.label39.TabIndex = 112;
            this.label39.Text = "m";
            // 
            // txt_P
            // 
            this.txt_P.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_P.Location = new System.Drawing.Point(247, 35);
            this.txt_P.Name = "txt_P";
            this.txt_P.Size = new System.Drawing.Size(75, 22);
            this.txt_P.TabIndex = 74;
            this.txt_P.Text = "900.0";
            this.txt_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_K
            // 
            this.txt_K.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_K.Location = new System.Drawing.Point(247, 90);
            this.txt_K.Name = "txt_K";
            this.txt_K.Size = new System.Drawing.Size(75, 22);
            this.txt_K.TabIndex = 76;
            this.txt_K.Text = "1.5";
            this.txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_max_spacing
            // 
            this.txt_max_spacing.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_max_spacing.Location = new System.Drawing.Point(245, 482);
            this.txt_max_spacing.Name = "txt_max_spacing";
            this.txt_max_spacing.Size = new System.Drawing.Size(75, 22);
            this.txt_max_spacing.TabIndex = 104;
            this.txt_max_spacing.Text = "200";
            this.txt_max_spacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_lateral_dia
            // 
            this.txt_lateral_dia.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lateral_dia.Location = new System.Drawing.Point(245, 458);
            this.txt_lateral_dia.Name = "txt_lateral_dia";
            this.txt_lateral_dia.Size = new System.Drawing.Size(75, 22);
            this.txt_lateral_dia.TabIndex = 104;
            this.txt_lateral_dia.Text = "8";
            this.txt_lateral_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_main_dia
            // 
            this.txt_main_dia.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_main_dia.Location = new System.Drawing.Point(245, 435);
            this.txt_main_dia.Name = "txt_main_dia";
            this.txt_main_dia.Size = new System.Drawing.Size(75, 22);
            this.txt_main_dia.TabIndex = 104;
            this.txt_main_dia.Text = "20";
            this.txt_main_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(5, 486);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(160, 13);
            this.label93.TabIndex = 103;
            this.label93.Text = "Pile Maximum spacing of Rebars";
            // 
            // txt_d_dash
            // 
            this.txt_d_dash.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d_dash.Location = new System.Drawing.Point(245, 412);
            this.txt_d_dash.Name = "txt_d_dash";
            this.txt_d_dash.Size = new System.Drawing.Size(75, 22);
            this.txt_d_dash.TabIndex = 104;
            this.txt_d_dash.Text = "120";
            this.txt_d_dash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(5, 462);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(217, 13);
            this.label91.TabIndex = 103;
            this.label91.Text = "Pile Lateral Reinforcement / Binder Diameter";
            // 
            // txt_gamma_sub
            // 
            this.txt_gamma_sub.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_sub.Location = new System.Drawing.Point(245, 389);
            this.txt_gamma_sub.Name = "txt_gamma_sub";
            this.txt_gamma_sub.Size = new System.Drawing.Size(75, 22);
            this.txt_gamma_sub.TabIndex = 104;
            this.txt_gamma_sub.Text = "0.92";
            this.txt_gamma_sub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(5, 439);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(167, 13);
            this.label89.TabIndex = 103;
            this.label89.Text = "Pile Main Reinforcement Diameter\r\n";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 38);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(136, 13);
            this.label31.TabIndex = 73;
            this.label31.Text = "Applied Load on Pile Group";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 416);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(150, 13);
            this.label28.TabIndex = 103;
            this.label28.Text = "Pile Reinforcement Cover [ d\' ]";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 70);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(159, 13);
            this.label30.TabIndex = 77;
            this.label30.Text = "Applied Moment on a Pile Group";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 393);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(97, 13);
            this.label34.TabIndex = 103;
            this.label34.Text = "γ_sub (γ = gamma)";
            // 
            // txt_AM
            // 
            this.txt_AM.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AM.Location = new System.Drawing.Point(247, 66);
            this.txt_AM.Name = "txt_AM";
            this.txt_AM.Size = new System.Drawing.Size(75, 22);
            this.txt_AM.TabIndex = 78;
            this.txt_AM.Text = "5.0";
            this.txt_AM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_D
            // 
            this.txt_D.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_D.Location = new System.Drawing.Point(247, 12);
            this.txt_D.Name = "txt_D";
            this.txt_D.Size = new System.Drawing.Size(75, 22);
            this.txt_D.TabIndex = 72;
            this.txt_D.Text = "1.0";
            this.txt_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SL
            // 
            this.txt_SL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SL.ForeColor = System.Drawing.Color.Blue;
            this.txt_SL.Location = new System.Drawing.Point(247, 204);
            this.txt_SL.Name = "txt_SL";
            this.txt_SL.Size = new System.Drawing.Size(75, 22);
            this.txt_SL.TabIndex = 92;
            this.txt_SL.Text = "54.288";
            this.txt_SL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_FL
            // 
            this.txt_FL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FL.ForeColor = System.Drawing.Color.Blue;
            this.txt_FL.Location = new System.Drawing.Point(247, 181);
            this.txt_FL.Name = "txt_FL";
            this.txt_FL.Size = new System.Drawing.Size(75, 22);
            this.txt_FL.TabIndex = 91;
            this.txt_FL.Text = "32.670";
            this.txt_FL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 208);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 13);
            this.label21.TabIndex = 89;
            this.label21.Text = "Scour Level [SL)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 185);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 13);
            this.label22.TabIndex = 90;
            this.label22.Text = "Founding Level [FL]";
            // 
            // txt_N
            // 
            this.txt_N.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_N.Location = new System.Drawing.Point(245, 365);
            this.txt_N.Name = "txt_N";
            this.txt_N.Size = new System.Drawing.Size(75, 22);
            this.txt_N.TabIndex = 102;
            this.txt_N.Text = "4";
            this.txt_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 369);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(128, 13);
            this.label23.TabIndex = 101;
            this.label23.Text = "Total Piles in front row [N]";
            // 
            // txt_PCBL
            // 
            this.txt_PCBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PCBL.Location = new System.Drawing.Point(247, 136);
            this.txt_PCBL.Name = "txt_PCBL";
            this.txt_PCBL.Size = new System.Drawing.Size(75, 22);
            this.txt_PCBL.TabIndex = 88;
            this.txt_PCBL.Text = "211.0";
            this.txt_PCBL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_PCBL.TextChanged += new System.EventHandler(this.txt_PCBL_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 139);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(147, 13);
            this.label24.TabIndex = 87;
            this.label24.Text = "Pile Cap Bottom Level [PCBL]";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 116);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(104, 13);
            this.label25.TabIndex = 85;
            this.label25.Text = "Factor of Safety [FS]";
            // 
            // txt_FS
            // 
            this.txt_FS.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FS.Location = new System.Drawing.Point(247, 113);
            this.txt_FS.Name = "txt_FS";
            this.txt_FS.Size = new System.Drawing.Size(75, 22);
            this.txt_FS.TabIndex = 86;
            this.txt_FS.Text = "2.5";
            this.txt_FS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label99);
            this.groupBox2.Controls.Add(this.txt_cap_spacing);
            this.groupBox2.Controls.Add(this.label100);
            this.groupBox2.Controls.Add(this.cmb_pcap_fy);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label60);
            this.groupBox2.Controls.Add(this.cmb_pcap_fck);
            this.groupBox2.Controls.Add(this.label48);
            this.groupBox2.Controls.Add(this.label59);
            this.groupBox2.Controls.Add(this.label49);
            this.groupBox2.Controls.Add(this.label58);
            this.groupBox2.Controls.Add(this.txt_pcap_sigma_st);
            this.groupBox2.Controls.Add(this.label57);
            this.groupBox2.Controls.Add(this.label71);
            this.groupBox2.Controls.Add(this.label56);
            this.groupBox2.Controls.Add(this.label82);
            this.groupBox2.Controls.Add(this.label55);
            this.groupBox2.Controls.Add(this.txt_pcap_sigma_c);
            this.groupBox2.Controls.Add(this.label54);
            this.groupBox2.Controls.Add(this.label83);
            this.groupBox2.Controls.Add(this.label53);
            this.groupBox2.Controls.Add(this.label63);
            this.groupBox2.Controls.Add(this.label52);
            this.groupBox2.Controls.Add(this.label62);
            this.groupBox2.Controls.Add(this.label98);
            this.groupBox2.Controls.Add(this.label96);
            this.groupBox2.Controls.Add(this.label51);
            this.groupBox2.Controls.Add(this.txt_m);
            this.groupBox2.Controls.Add(this.label50);
            this.groupBox2.Controls.Add(this.label78);
            this.groupBox2.Controls.Add(this.label61);
            this.groupBox2.Controls.Add(this.txt_L3);
            this.groupBox2.Controls.Add(this.label64);
            this.groupBox2.Controls.Add(this.label97);
            this.groupBox2.Controls.Add(this.label95);
            this.groupBox2.Controls.Add(this.label65);
            this.groupBox2.Controls.Add(this.txt_L2);
            this.groupBox2.Controls.Add(this.txt_L1);
            this.groupBox2.Controls.Add(this.txt_DPC);
            this.groupBox2.Controls.Add(this.txt_BPr);
            this.groupBox2.Controls.Add(this.label66);
            this.groupBox2.Controls.Add(this.txt_LPr);
            this.groupBox2.Controls.Add(this.txt_BPC);
            this.groupBox2.Controls.Add(this.txt_LPC);
            this.groupBox2.Controls.Add(this.label67);
            this.groupBox2.Controls.Add(this.label68);
            this.groupBox2.Controls.Add(this.label69);
            this.groupBox2.Controls.Add(this.txt_clear_cover);
            this.groupBox2.Controls.Add(this.txt_shear_dia);
            this.groupBox2.Controls.Add(this.txt_d3);
            this.groupBox2.Controls.Add(this.label72);
            this.groupBox2.Controls.Add(this.txt_d2);
            this.groupBox2.Controls.Add(this.label73);
            this.groupBox2.Controls.Add(this.txt_d1);
            this.groupBox2.Controls.Add(this.label74);
            this.groupBox2.Controls.Add(this.txt_F);
            this.groupBox2.Controls.Add(this.label75);
            this.groupBox2.Controls.Add(this.label76);
            this.groupBox2.Controls.Add(this.label77);
            this.groupBox2.Location = new System.Drawing.Point(427, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 549);
            this.groupBox2.TabIndex = 166;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pile Cap Input Data";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(349, 290);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(31, 14);
            this.label99.TabIndex = 168;
            this.label99.Text = "mm";
            // 
            // txt_cap_spacing
            // 
            this.txt_cap_spacing.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cap_spacing.Location = new System.Drawing.Point(268, 287);
            this.txt_cap_spacing.Name = "txt_cap_spacing";
            this.txt_cap_spacing.Size = new System.Drawing.Size(75, 22);
            this.txt_cap_spacing.TabIndex = 167;
            this.txt_cap_spacing.Text = "150";
            this.txt_cap_spacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(15, 291);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(135, 13);
            this.label100.TabIndex = 166;
            this.label100.Text = "Pile Cap spacing of Rebars";
            // 
            // cmb_pcap_fy
            // 
            this.cmb_pcap_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pcap_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_pcap_fy.FormattingEnabled = true;
            this.cmb_pcap_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_pcap_fy.Location = new System.Drawing.Point(282, 64);
            this.cmb_pcap_fy.Name = "cmb_pcap_fy";
            this.cmb_pcap_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_pcap_fy.TabIndex = 15;
            this.cmb_pcap_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(11, 38);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(211, 13);
            this.label38.TabIndex = 79;
            this.label38.Text = "Allowable Flexural Stress in Concrete [σ_c] ";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.Red;
            this.label60.Location = new System.Drawing.Point(6, 525);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(440, 17);
            this.label60.TabIndex = 165;
            this.label60.Text = "*Refer to Diagram in the Next Tab for Dimensions";
            // 
            // cmb_pcap_fck
            // 
            this.cmb_pcap_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pcap_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_pcap_fck.FormattingEnabled = true;
            this.cmb_pcap_fck.Items.AddRange(new object[] {
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
            this.cmb_pcap_fck.Location = new System.Drawing.Point(282, 15);
            this.cmb_pcap_fck.Name = "cmb_pcap_fck";
            this.cmb_pcap_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_pcap_fck.TabIndex = 13;
            this.cmb_pcap_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(254, 66);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(23, 14);
            this.label48.TabIndex = 59;
            this.label48.Text = "Fe";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(348, 496);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(31, 14);
            this.label59.TabIndex = 155;
            this.label59.Text = "mm";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(259, 17);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(18, 14);
            this.label49.TabIndex = 58;
            this.label49.Text = "M";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(348, 471);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(31, 14);
            this.label58.TabIndex = 156;
            this.label58.Text = "mm";
            // 
            // txt_pcap_sigma_st
            // 
            this.txt_pcap_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_pcap_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pcap_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_pcap_sigma_st.Location = new System.Drawing.Point(268, 91);
            this.txt_pcap_sigma_st.Name = "txt_pcap_sigma_st";
            this.txt_pcap_sigma_st.ReadOnly = true;
            this.txt_pcap_sigma_st.Size = new System.Drawing.Size(75, 22);
            this.txt_pcap_sigma_st.TabIndex = 16;
            this.txt_pcap_sigma_st.Text = "200";
            this.txt_pcap_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(348, 446);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(31, 14);
            this.label57.TabIndex = 154;
            this.label57.Text = "mm";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(11, 92);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(159, 13);
            this.label71.TabIndex = 55;
            this.label71.Text = "Permissible Stress in Steel [σ_st]";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(348, 365);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(31, 14);
            this.label56.TabIndex = 152;
            this.label56.Text = "mm";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(11, 67);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(80, 13);
            this.label82.TabIndex = 15;
            this.label82.Text = "Steel Grade [fy]";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(347, 415);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(31, 14);
            this.label55.TabIndex = 153;
            this.label55.Text = "mm";
            // 
            // txt_pcap_sigma_c
            // 
            this.txt_pcap_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_pcap_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pcap_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_pcap_sigma_c.Location = new System.Drawing.Point(268, 39);
            this.txt_pcap_sigma_c.Name = "txt_pcap_sigma_c";
            this.txt_pcap_sigma_c.ReadOnly = true;
            this.txt_pcap_sigma_c.Size = new System.Drawing.Size(75, 22);
            this.txt_pcap_sigma_c.TabIndex = 14;
            this.txt_pcap_sigma_c.Text = "83.3";
            this.txt_pcap_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(347, 390);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(31, 14);
            this.label54.TabIndex = 157;
            this.label54.Text = "mm";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(11, 18);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(106, 13);
            this.label83.TabIndex = 13;
            this.label83.Text = "Concrete Grade [fck]";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(348, 339);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(31, 14);
            this.label53.TabIndex = 162;
            this.label53.Text = "mm";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(341, 42);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(68, 14);
            this.label63.TabIndex = 160;
            this.label63.Text = "N/sq.mm";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(349, 317);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(31, 14);
            this.label52.TabIndex = 163;
            this.label52.Text = "mm";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(341, 94);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(68, 14);
            this.label62.TabIndex = 159;
            this.label62.Text = "N/sq.mm";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(345, 266);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(31, 14);
            this.label98.TabIndex = 164;
            this.label98.Text = "mm";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(345, 238);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(31, 14);
            this.label96.TabIndex = 164;
            this.label96.Text = "mm";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(344, 211);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(31, 14);
            this.label51.TabIndex = 164;
            this.label51.Text = "mm";
            // 
            // txt_m
            // 
            this.txt_m.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_m.Location = new System.Drawing.Point(268, 115);
            this.txt_m.Name = "txt_m";
            this.txt_m.Size = new System.Drawing.Size(75, 22);
            this.txt_m.TabIndex = 125;
            this.txt_m.Text = "10";
            this.txt_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(343, 187);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(31, 14);
            this.label50.TabIndex = 161;
            this.label50.Text = "mm";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(11, 119);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(90, 13);
            this.label78.TabIndex = 120;
            this.label78.Text = "Modular Ratio [m]";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(343, 165);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(31, 14);
            this.label61.TabIndex = 158;
            this.label61.Text = "mm";
            // 
            // txt_L3
            // 
            this.txt_L3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L3.Location = new System.Drawing.Point(268, 493);
            this.txt_L3.Name = "txt_L3";
            this.txt_L3.Size = new System.Drawing.Size(75, 22);
            this.txt_L3.TabIndex = 149;
            this.txt_L3.Text = "1500";
            this.txt_L3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(15, 487);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(44, 17);
            this.label64.TabIndex = 148;
            this.label64.Text = "L3 *";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(11, 266);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(134, 13);
            this.label97.TabIndex = 147;
            this.label97.Text = "Reinforcement Clear Cover";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(11, 238);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(171, 13);
            this.label95.TabIndex = 147;
            this.label95.Text = "Shear Reinforcement Bar Diameter";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(11, 214);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(183, 13);
            this.label65.TabIndex = 147;
            this.label65.Text = "Top Reinforcement Bar Diameter [d3]";
            // 
            // txt_L2
            // 
            this.txt_L2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L2.Location = new System.Drawing.Point(268, 468);
            this.txt_L2.Name = "txt_L2";
            this.txt_L2.Size = new System.Drawing.Size(75, 22);
            this.txt_L2.TabIndex = 146;
            this.txt_L2.Text = "650";
            this.txt_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_L1
            // 
            this.txt_L1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L1.Location = new System.Drawing.Point(268, 443);
            this.txt_L1.Name = "txt_L1";
            this.txt_L1.Size = new System.Drawing.Size(75, 22);
            this.txt_L1.TabIndex = 145;
            this.txt_L1.Tag = "";
            this.txt_L1.Text = "1250";
            this.txt_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DPC
            // 
            this.txt_DPC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DPC.Location = new System.Drawing.Point(268, 365);
            this.txt_DPC.Name = "txt_DPC";
            this.txt_DPC.Size = new System.Drawing.Size(75, 22);
            this.txt_DPC.TabIndex = 143;
            this.txt_DPC.Text = "1500";
            this.txt_DPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_BPr
            // 
            this.txt_BPr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BPr.Location = new System.Drawing.Point(268, 415);
            this.txt_BPr.Name = "txt_BPr";
            this.txt_BPr.Size = new System.Drawing.Size(75, 22);
            this.txt_BPr.TabIndex = 142;
            this.txt_BPr.Text = "1100";
            this.txt_BPr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(11, 190);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(197, 13);
            this.label66.TabIndex = 144;
            this.label66.Text = "Bottom Reinforcement Bar Diameter [d2]";
            // 
            // txt_LPr
            // 
            this.txt_LPr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LPr.Location = new System.Drawing.Point(268, 388);
            this.txt_LPr.Name = "txt_LPr";
            this.txt_LPr.Size = new System.Drawing.Size(75, 22);
            this.txt_LPr.TabIndex = 141;
            this.txt_LPr.Text = "5778";
            this.txt_LPr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_BPC
            // 
            this.txt_BPC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BPC.Location = new System.Drawing.Point(268, 339);
            this.txt_BPC.Name = "txt_BPC";
            this.txt_BPC.Size = new System.Drawing.Size(75, 22);
            this.txt_BPC.TabIndex = 140;
            this.txt_BPC.Text = "4300";
            this.txt_BPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_LPC
            // 
            this.txt_LPC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LPC.Location = new System.Drawing.Point(268, 314);
            this.txt_LPC.Name = "txt_LPC";
            this.txt_LPC.Size = new System.Drawing.Size(75, 22);
            this.txt_LPC.TabIndex = 139;
            this.txt_LPC.Text = "4300";
            this.txt_LPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(15, 436);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(44, 17);
            this.label67.TabIndex = 138;
            this.label67.Text = "L1 *";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(15, 461);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(44, 17);
            this.label68.TabIndex = 137;
            this.label68.Text = "L2 *";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(15, 360);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(109, 13);
            this.label69.TabIndex = 136;
            this.label69.Text = "Pile Cap Depth [DPC]";
            // 
            // txt_clear_cover
            // 
            this.txt_clear_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_clear_cover.Location = new System.Drawing.Point(268, 263);
            this.txt_clear_cover.Name = "txt_clear_cover";
            this.txt_clear_cover.Size = new System.Drawing.Size(75, 22);
            this.txt_clear_cover.TabIndex = 133;
            this.txt_clear_cover.Text = "75";
            this.txt_clear_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_shear_dia
            // 
            this.txt_shear_dia.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_shear_dia.Location = new System.Drawing.Point(268, 235);
            this.txt_shear_dia.Name = "txt_shear_dia";
            this.txt_shear_dia.Size = new System.Drawing.Size(75, 22);
            this.txt_shear_dia.TabIndex = 133;
            this.txt_shear_dia.Text = "10";
            this.txt_shear_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_d3
            // 
            this.txt_d3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d3.Location = new System.Drawing.Point(268, 211);
            this.txt_d3.Name = "txt_d3";
            this.txt_d3.Size = new System.Drawing.Size(75, 22);
            this.txt_d3.TabIndex = 133;
            this.txt_d3.Text = "16";
            this.txt_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(15, 418);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(89, 13);
            this.label72.TabIndex = 132;
            this.label72.Text = "Pier Width [BPr] *";
            // 
            // txt_d2
            // 
            this.txt_d2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d2.Location = new System.Drawing.Point(268, 187);
            this.txt_d2.Name = "txt_d2";
            this.txt_d2.Size = new System.Drawing.Size(75, 22);
            this.txt_d2.TabIndex = 131;
            this.txt_d2.Text = "25";
            this.txt_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(15, 392);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(93, 13);
            this.label73.TabIndex = 130;
            this.label73.Text = "Pier Length [LPr] *";
            // 
            // txt_d1
            // 
            this.txt_d1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d1.Location = new System.Drawing.Point(268, 163);
            this.txt_d1.Name = "txt_d1";
            this.txt_d1.Size = new System.Drawing.Size(75, 22);
            this.txt_d1.TabIndex = 129;
            this.txt_d1.Text = "20";
            this.txt_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(15, 338);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(107, 13);
            this.label74.TabIndex = 128;
            this.label74.Text = "Pile Cap Width [BPC]";
            // 
            // txt_F
            // 
            this.txt_F.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_F.Location = new System.Drawing.Point(268, 139);
            this.txt_F.Name = "txt_F";
            this.txt_F.Size = new System.Drawing.Size(75, 22);
            this.txt_F.TabIndex = 127;
            this.txt_F.Text = "1.5";
            this.txt_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(15, 313);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(118, 13);
            this.label75.TabIndex = 126;
            this.label75.Text = "Pile Cap Length [LPC] *";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(11, 166);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(230, 13);
            this.label76.TabIndex = 124;
            this.label76.Text = "Diameter of Main Steel Reinforcement bars [d1]";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(11, 143);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(79, 13);
            this.label77.TabIndex = 122;
            this.label77.Text = "Load Factor [F]";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Controls.Add(this.label80);
            this.tabPage2.Controls.Add(this.label84);
            this.tabPage2.Controls.Add(this.pic_pile);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(894, 566);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Layer wise Sub Soil Data && Diagram";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(560, 33);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(259, 18);
            this.label27.TabIndex = 172;
            this.label27.Text = "Default Sample Dimensions are shown";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label80.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.ForeColor = System.Drawing.Color.Red;
            this.label80.Location = new System.Drawing.Point(224, 16);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(218, 18);
            this.label80.TabIndex = 172;
            this.label80.Text = "Default Sample Data are shown";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label84.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.ForeColor = System.Drawing.Color.Green;
            this.label84.Location = new System.Drawing.Point(83, 16);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(135, 18);
            this.label84.TabIndex = 171;
            this.label84.Text = "All User Input Data";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label87);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.dgv);
            this.groupBox4.Location = new System.Drawing.Point(6, 54);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(537, 352);
            this.groupBox4.TabIndex = 115;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Layer wise Sub Soil Data";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(37, 22);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(97, 14);
            this.label87.TabIndex = 40;
            this.label87.Text = "Borehole No. :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(150, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 39;
            this.textBox1.Text = "BH:1";
            // 
            // dgv
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL_N,
            this.col_Layers,
            this.col_Depth,
            this.col_Thickness,
            this.col_ang_fric,
            this.col_cons_adhe,
            this.col_Cohesion,
            this.col_gama_sub});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv.Location = new System.Drawing.Point(3, 49);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 30;
            this.dgv.RowTemplate.Height = 20;
            this.dgv.Size = new System.Drawing.Size(531, 300);
            this.dgv.TabIndex = 38;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_Pile_SelectionChanged);
            // 
            // col_SL_N
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_SL_N.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_SL_N.HeaderText = "S.No.";
            this.col_SL_N.Name = "col_SL_N";
            this.col_SL_N.Width = 37;
            // 
            // col_Layers
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Layers.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_Layers.HeaderText = "Layer No. from Top";
            this.col_Layers.Name = "col_Layers";
            this.col_Layers.Width = 50;
            // 
            // col_Depth
            // 
            this.col_Depth.HeaderText = "Depth upto bottom of the Layer";
            this.col_Depth.Name = "col_Depth";
            this.col_Depth.Width = 60;
            // 
            // col_Thickness
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
            this.col_Thickness.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_Thickness.HeaderText = "Layer Thickness";
            this.col_Thickness.Name = "col_Thickness";
            this.col_Thickness.Width = 60;
            // 
            // col_ang_fric
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_ang_fric.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_ang_fric.HeaderText = "Angle of Internal Friction [φ]";
            this.col_ang_fric.Name = "col_ang_fric";
            this.col_ang_fric.Width = 60;
            // 
            // col_cons_adhe
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_cons_adhe.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_cons_adhe.HeaderText = "Constant for Adhesion [α]";
            this.col_cons_adhe.Name = "col_cons_adhe";
            this.col_cons_adhe.Width = 70;
            // 
            // col_Cohesion
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Cohesion.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_Cohesion.HeaderText = "Cohesion [C] (Ton/sq.m)";
            this.col_Cohesion.Name = "col_Cohesion";
            this.col_Cohesion.Width = 70;
            // 
            // col_gama_sub
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_gama_sub.DefaultCellStyle = dataGridViewCellStyle8;
            this.col_gama_sub.HeaderText = "Submerged Bulk Density [γ_sub] (Ton/cu.m)";
            this.col_gama_sub.Name = "col_gama_sub";
            this.col_gama_sub.Width = 80;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_pile_process);
            this.panel2.Controls.Add(this.btnReport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 595);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(902, 26);
            this.panel2.TabIndex = 171;
            // 
            // btn_pile_process
            // 
            this.btn_pile_process.Location = new System.Drawing.Point(341, 2);
            this.btn_pile_process.Name = "btn_pile_process";
            this.btn_pile_process.Size = new System.Drawing.Size(92, 23);
            this.btn_pile_process.TabIndex = 168;
            this.btn_pile_process.Text = "Process";
            this.btn_pile_process.UseVisualStyleBackColor = true;
            this.btn_pile_process.Click += new System.EventHandler(this.btn_Pile_Process_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(469, 2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(92, 23);
            this.btnReport.TabIndex = 169;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btn_Pile_Report_Click);
            // 
            // tab_worksheet_design
            // 
            this.tab_worksheet_design.Controls.Add(this.btn_worksheet_open);
            this.tab_worksheet_design.Controls.Add(this.btn_worksheet_sheet_pile);
            this.tab_worksheet_design.Controls.Add(this.btn_worksheet_well);
            this.tab_worksheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_worksheet_design.Name = "tab_worksheet_design";
            this.tab_worksheet_design.Size = new System.Drawing.Size(908, 624);
            this.tab_worksheet_design.TabIndex = 3;
            this.tab_worksheet_design.Text = "Worksheet Design";
            this.tab_worksheet_design.UseVisualStyleBackColor = true;
            // 
            // btn_worksheet_open
            // 
            this.btn_worksheet_open.Location = new System.Drawing.Point(219, 334);
            this.btn_worksheet_open.Name = "btn_worksheet_open";
            this.btn_worksheet_open.Size = new System.Drawing.Size(306, 54);
            this.btn_worksheet_open.TabIndex = 3;
            this.btn_worksheet_open.Text = "Open User\'s saved Worksheet Design";
            this.btn_worksheet_open.UseVisualStyleBackColor = true;
            this.btn_worksheet_open.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_worksheet_sheet_pile
            // 
            this.btn_worksheet_sheet_pile.Location = new System.Drawing.Point(219, 257);
            this.btn_worksheet_sheet_pile.Name = "btn_worksheet_sheet_pile";
            this.btn_worksheet_sheet_pile.Size = new System.Drawing.Size(306, 56);
            this.btn_worksheet_sheet_pile.TabIndex = 1;
            this.btn_worksheet_sheet_pile.Text = "Sheet Pile for Cofferdam Worksheet Design";
            this.btn_worksheet_sheet_pile.UseVisualStyleBackColor = true;
            this.btn_worksheet_sheet_pile.Click += new System.EventHandler(this.btn_worksheet_sheet_pile_Click);
            // 
            // btn_worksheet_well
            // 
            this.btn_worksheet_well.Location = new System.Drawing.Point(219, 135);
            this.btn_worksheet_well.Name = "btn_worksheet_well";
            this.btn_worksheet_well.Size = new System.Drawing.Size(306, 56);
            this.btn_worksheet_well.TabIndex = 0;
            this.btn_worksheet_well.Text = "RCC Well Foundation Worksheet Design";
            this.btn_worksheet_well.UseVisualStyleBackColor = true;
            this.btn_worksheet_well.Click += new System.EventHandler(this.btn_worksheet_well_Click);
            // 
            // tab_drawing
            // 
            this.tab_drawing.Controls.Add(this.label37);
            this.tab_drawing.Controls.Add(this.label81);
            this.tab_drawing.Controls.Add(this.label143);
            this.tab_drawing.Controls.Add(this.btn_dwg_pile_interactive);
            this.tab_drawing.Controls.Add(this.btn_dwg_well_interactive);
            this.tab_drawing.Controls.Add(this.btn_dwg_well_default);
            this.tab_drawing.Location = new System.Drawing.Point(4, 22);
            this.tab_drawing.Name = "tab_drawing";
            this.tab_drawing.Size = new System.Drawing.Size(908, 624);
            this.tab_drawing.TabIndex = 4;
            this.tab_drawing.Text = "Drawings";
            this.tab_drawing.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(300, 19);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(308, 23);
            this.label37.TabIndex = 79;
            this.label37.Text = "Editable Construction Drawings";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(253, 225);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(399, 16);
            this.label81.TabIndex = 6;
            this.label81.Text = "Button is Enabled Once the Pile Foundation Design is done.";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(252, 83);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(403, 16);
            this.label143.TabIndex = 5;
            this.label143.Text = "Button is Enabled Once the Well Foundation Design is done.";
            this.label143.Visible = false;
            // 
            // btn_dwg_pile_interactive
            // 
            this.btn_dwg_pile_interactive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_pile_interactive.Location = new System.Drawing.Point(296, 244);
            this.btn_dwg_pile_interactive.Name = "btn_dwg_pile_interactive";
            this.btn_dwg_pile_interactive.Size = new System.Drawing.Size(312, 49);
            this.btn_dwg_pile_interactive.TabIndex = 2;
            this.btn_dwg_pile_interactive.Text = "Pile Foundation Drawings";
            this.btn_dwg_pile_interactive.UseVisualStyleBackColor = true;
            this.btn_dwg_pile_interactive.Click += new System.EventHandler(this.btn_Pile_Drawing_Click);
            // 
            // btn_dwg_well_interactive
            // 
            this.btn_dwg_well_interactive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_well_interactive.Location = new System.Drawing.Point(298, 102);
            this.btn_dwg_well_interactive.Name = "btn_dwg_well_interactive";
            this.btn_dwg_well_interactive.Size = new System.Drawing.Size(312, 47);
            this.btn_dwg_well_interactive.TabIndex = 1;
            this.btn_dwg_well_interactive.Text = "Well Foundation Drawings";
            this.btn_dwg_well_interactive.UseVisualStyleBackColor = true;
            this.btn_dwg_well_interactive.Visible = false;
            this.btn_dwg_well_interactive.Click += new System.EventHandler(this.btn_dwg_well_interactive_Click);
            // 
            // btn_dwg_well_default
            // 
            this.btn_dwg_well_default.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_well_default.Location = new System.Drawing.Point(296, 155);
            this.btn_dwg_well_default.Name = "btn_dwg_well_default";
            this.btn_dwg_well_default.Size = new System.Drawing.Size(312, 48);
            this.btn_dwg_well_default.TabIndex = 0;
            this.btn_dwg_well_default.Text = "Well Foundation Worksheet Drawing";
            this.btn_dwg_well_default.UseVisualStyleBackColor = true;
            this.btn_dwg_well_default.Click += new System.EventHandler(this.btn_dwg_well_default_Click);
            // 
            // pic_well
            // 
            this.pic_well.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_well.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_well.Location = new System.Drawing.Point(481, 132);
            this.pic_well.Name = "pic_well";
            this.pic_well.Size = new System.Drawing.Size(410, 364);
            this.pic_well.TabIndex = 12;
            this.pic_well.TabStop = false;
            // 
            // pic_pile
            // 
            this.pic_pile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_pile.Location = new System.Drawing.Point(560, 54);
            this.pic_pile.Name = "pic_pile";
            this.pic_pile.Size = new System.Drawing.Size(325, 390);
            this.pic_pile.TabIndex = 170;
            this.pic_pile.TabStop = false;
            // 
            // frm_Foundation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 650);
            this.Controls.Add(this.tc_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Foundation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Foundation";
            this.Load += new System.EventHandler(this.frm_Foundation_Load);
            this.tc_main.ResumeLayout(false);
            this.tab_rcc_well_fnd.ResumeLayout(false);
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tab_rcc_well_fnd_LS.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tab_rcc_pile_foundation.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tab_worksheet_design.ResumeLayout(false);
            this.tab_drawing.ResumeLayout(false);
            this.tab_drawing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_well)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tab_rcc_well_fnd;
        private System.Windows.Forms.TabPage tab_rcc_pile_foundation;
        private System.Windows.Forms.TabPage tab_worksheet_design;
        private System.Windows.Forms.TabPage tab_drawing;
        private System.Windows.Forms.Button btn_worksheet_well;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_well_Tc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_well_Lc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_well_D2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_well_D1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_well_K;
        private System.Windows.Forms.TextBox txt_well_K;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_well_L;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_well_di;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_well_Report;
        private System.Windows.Forms.Button btn_well_Process;
        private System.Windows.Forms.PictureBox pic_well;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txt_gamma_sub;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_SL;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_FL;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_N;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_PCBL;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_FS;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_D;
        private System.Windows.Forms.TextBox txt_AM;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_K;
        private System.Windows.Forms.TextBox txt_P;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_Np;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txt_gamma_c;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txt_L3;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txt_L2;
        private System.Windows.Forms.TextBox txt_L1;
        private System.Windows.Forms.TextBox txt_DPC;
        private System.Windows.Forms.TextBox txt_BPr;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txt_LPr;
        private System.Windows.Forms.TextBox txt_BPC;
        private System.Windows.Forms.TextBox txt_LPC;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.TextBox txt_d3;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_d2;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txt_d1;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txt_F;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txt_m;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_worksheet_sheet_pile;
        private System.Windows.Forms.Button btn_dwg_well_interactive;
        private System.Windows.Forms.Button btn_dwg_pile_interactive;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btn_pile_process;
        private System.Windows.Forms.PictureBox pic_pile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Button btn_dwg_well_default;
        private System.Windows.Forms.Button btn_worksheet_open;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Label label214;
        private System.Windows.Forms.ComboBox cmb_well_fy;
        private System.Windows.Forms.Label label216;
        private System.Windows.Forms.ComboBox cmb_well_fck;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.TextBox txt_well_sigma_st;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.TextBox txt_well_sigma_c;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_pile_fy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_pile_fck;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_pile_sigma_st;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_pile_sigma_c;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cmb_pcap_fy;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox cmb_pcap_fck;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_pcap_sigma_st;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox txt_pcap_sigma_c;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label601;
        private System.Windows.Forms.Label label602;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Button btn_well_open_des;
        private System.Windows.Forms.Button btn_pile_open_des;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.TextBox txt_PL;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SL_N;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Layers;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Depth;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Thickness;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ang_fric;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cons_adhe;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Cohesion;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_gama_sub;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txt_max_spacing;
        private System.Windows.Forms.TextBox txt_lateral_dia;
        private System.Windows.Forms.TextBox txt_main_dia;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.TextBox txt_d_dash;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.TextBox txt_shear_dia;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.TextBox txt_clear_cover;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txt_cap_spacing;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.TabPage tab_rcc_well_fnd_LS;
        private UC_Well_Foundation uC_Well_Foundation1;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.TextBox txt_well_avg_dia;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox txt_well_mon_reinf;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.TextBox txt_well_D2_unit_wt;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.TextBox txt_well_D1_unit_wt;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.TextBox txt_well_D3_unit_wt;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.TextBox txt_well_D3;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_pile_new_design;
        private System.Windows.Forms.Button btn_pile_browse;
        private System.Windows.Forms.TextBox txt_pile_project;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_well_new_design;
        private System.Windows.Forms.Button btn_well_browse;
        private System.Windows.Forms.TextBox txt_well_project;
        private System.Windows.Forms.Label label242;
    }
}