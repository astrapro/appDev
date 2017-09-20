namespace BridgeAnalysisDesign.Underpass
{
    partial class frm_Underpass
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
            iApp.LastDesignWorkingFolder = System.IO.Path.GetDirectoryName(iApp.LastDesignWorkingFolder);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Underpass));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Vehicular = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tab_Top_RCC_Slab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.txt_top_slab_delta_wc = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_top_slab_delta_c = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txt_top_slab_cover = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.txt_top_slab_W1 = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txt_top_slab_b2 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.txt_top_slab_b1 = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.txt_top_slab_a1 = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.txt_top_slab_Q = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.txt_top_slab_j = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.txt_top_slab_m = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.txt_top_slab_sigma_st = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.txt_top_slab_sigma_cb = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.txt_top_slab_steel_grade = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.txt_top_slab_concrete_grade = new System.Windows.Forms.TextBox();
            this.label85 = new System.Windows.Forms.Label();
            this.txt_top_slab_width_support = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.txt_top_slab_WC = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.txt_top_slab_L = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txt_top_slab_FP = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.txt_top_slab_CW = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txt_top_slab_D = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_top_slab_process = new System.Windows.Forms.Button();
            this.btn_top_slab_report = new System.Windows.Forms.Button();
            this.pic_veh_top_slab = new System.Windows.Forms.PictureBox();
            this.tab_RCC_Abutment = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txt_abut_h1 = new System.Windows.Forms.TextBox();
            this.txt_abut_L = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.lblCover = new System.Windows.Forms.Label();
            this.txt_abut_cover = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txt_abut_fact = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txt_abut_F = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txt_abut_mu = new System.Windows.Forms.TextBox();
            this.txt_abut_gamma_b = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_abut_gamma_c = new System.Windows.Forms.TextBox();
            this.txt_abut_z = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_abut_delta = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_abut_theta = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_abut_w5 = new System.Windows.Forms.TextBox();
            this.txt_abut_phi = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_abut_w6 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_abut_steel_grade = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_abut_concrete_grade = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_abut_p_bearing_capacity = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_abut_d4 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.txt_abut_L4 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.pic_veh_rcc_abut = new System.Windows.Forms.PictureBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txt_abut_t = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_abut_d3 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txt_abut_L3 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_abut_L2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_abut_L1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_abut_d1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_abut_d2 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_abut_B = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_abut_a = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_abut_H = new System.Windows.Forms.TextBox();
            this.tab_Pedestrian = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txt_sigma_st = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_box_Report = new System.Windows.Forms.Button();
            this.btn_box_Process = new System.Windows.Forms.Button();
            this.label92 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.txt_sigma_c = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_b1 = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.txt_b2 = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txt_a1 = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.txt_w1 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_w2 = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.txt_b3 = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.txt_F = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.txt_S = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.txt_sbc = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.txt_cover = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.pic_pedestrian = new System.Windows.Forms.PictureBox();
            this.txt_j = new System.Windows.Forms.TextBox();
            this.label120 = new System.Windows.Forms.Label();
            this.txt_t = new System.Windows.Forms.TextBox();
            this.label121 = new System.Windows.Forms.Label();
            this.txt_R = new System.Windows.Forms.TextBox();
            this.label122 = new System.Windows.Forms.Label();
            this.txt_gamma_c = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.txt_gamma_b = new System.Windows.Forms.TextBox();
            this.label124 = new System.Windows.Forms.Label();
            this.txt_d3 = new System.Windows.Forms.TextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.txt_d2 = new System.Windows.Forms.TextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.txt_d1 = new System.Windows.Forms.TextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.txt_d = new System.Windows.Forms.TextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.txt_b = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.txt_H = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.tab_Drawings = new System.Windows.Forms.TabPage();
            this.label132 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_dwg_pedestrian = new System.Windows.Forms.Button();
            this.btn_dwg_abut = new System.Windows.Forms.Button();
            this.btn_dwg_slab = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tab_Vehicular.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tab_Top_RCC_Slab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_veh_top_slab)).BeginInit();
            this.tab_RCC_Abutment.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_veh_rcc_abut)).BeginInit();
            this.tab_Pedestrian.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pedestrian)).BeginInit();
            this.tab_Drawings.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Vehicular);
            this.tabControl1.Controls.Add(this.tab_Pedestrian);
            this.tabControl1.Controls.Add(this.tab_Drawings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 711);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_Vehicular
            // 
            this.tab_Vehicular.Controls.Add(this.tabControl2);
            this.tab_Vehicular.Location = new System.Drawing.Point(4, 22);
            this.tab_Vehicular.Name = "tab_Vehicular";
            this.tab_Vehicular.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Vehicular.Size = new System.Drawing.Size(876, 685);
            this.tab_Vehicular.TabIndex = 0;
            this.tab_Vehicular.Text = "Vehicular Underpass";
            this.tab_Vehicular.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tab_Top_RCC_Slab);
            this.tabControl2.Controls.Add(this.tab_RCC_Abutment);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(870, 679);
            this.tabControl2.TabIndex = 0;
            // 
            // tab_Top_RCC_Slab
            // 
            this.tab_Top_RCC_Slab.Controls.Add(this.groupBox4);
            this.tab_Top_RCC_Slab.Controls.Add(this.flowLayoutPanel1);
            this.tab_Top_RCC_Slab.Controls.Add(this.pic_veh_top_slab);
            this.tab_Top_RCC_Slab.Location = new System.Drawing.Point(4, 22);
            this.tab_Top_RCC_Slab.Name = "tab_Top_RCC_Slab";
            this.tab_Top_RCC_Slab.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Top_RCC_Slab.Size = new System.Drawing.Size(862, 653);
            this.tab_Top_RCC_Slab.TabIndex = 0;
            this.tab_Top_RCC_Slab.Text = "Top RCC Slab";
            this.tab_Top_RCC_Slab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label56);
            this.groupBox4.Controls.Add(this.label57);
            this.groupBox4.Controls.Add(this.label58);
            this.groupBox4.Controls.Add(this.label59);
            this.groupBox4.Controls.Add(this.label60);
            this.groupBox4.Controls.Add(this.label61);
            this.groupBox4.Controls.Add(this.label62);
            this.groupBox4.Controls.Add(this.label63);
            this.groupBox4.Controls.Add(this.label64);
            this.groupBox4.Controls.Add(this.label65);
            this.groupBox4.Controls.Add(this.label66);
            this.groupBox4.Controls.Add(this.label67);
            this.groupBox4.Controls.Add(this.label68);
            this.groupBox4.Controls.Add(this.label69);
            this.groupBox4.Controls.Add(this.label70);
            this.groupBox4.Controls.Add(this.label71);
            this.groupBox4.Controls.Add(this.txt_top_slab_delta_wc);
            this.groupBox4.Controls.Add(this.label72);
            this.groupBox4.Controls.Add(this.txt_top_slab_delta_c);
            this.groupBox4.Controls.Add(this.label73);
            this.groupBox4.Controls.Add(this.txt_top_slab_cover);
            this.groupBox4.Controls.Add(this.label74);
            this.groupBox4.Controls.Add(this.txt_top_slab_W1);
            this.groupBox4.Controls.Add(this.label75);
            this.groupBox4.Controls.Add(this.txt_top_slab_b2);
            this.groupBox4.Controls.Add(this.label76);
            this.groupBox4.Controls.Add(this.txt_top_slab_b1);
            this.groupBox4.Controls.Add(this.label77);
            this.groupBox4.Controls.Add(this.txt_top_slab_a1);
            this.groupBox4.Controls.Add(this.label78);
            this.groupBox4.Controls.Add(this.txt_top_slab_Q);
            this.groupBox4.Controls.Add(this.label79);
            this.groupBox4.Controls.Add(this.txt_top_slab_j);
            this.groupBox4.Controls.Add(this.label80);
            this.groupBox4.Controls.Add(this.txt_top_slab_m);
            this.groupBox4.Controls.Add(this.label81);
            this.groupBox4.Controls.Add(this.txt_top_slab_sigma_st);
            this.groupBox4.Controls.Add(this.label82);
            this.groupBox4.Controls.Add(this.txt_top_slab_sigma_cb);
            this.groupBox4.Controls.Add(this.label83);
            this.groupBox4.Controls.Add(this.txt_top_slab_steel_grade);
            this.groupBox4.Controls.Add(this.label84);
            this.groupBox4.Controls.Add(this.txt_top_slab_concrete_grade);
            this.groupBox4.Controls.Add(this.label85);
            this.groupBox4.Controls.Add(this.txt_top_slab_width_support);
            this.groupBox4.Controls.Add(this.label86);
            this.groupBox4.Controls.Add(this.txt_top_slab_WC);
            this.groupBox4.Controls.Add(this.label87);
            this.groupBox4.Controls.Add(this.txt_top_slab_L);
            this.groupBox4.Controls.Add(this.label88);
            this.groupBox4.Controls.Add(this.txt_top_slab_FP);
            this.groupBox4.Controls.Add(this.label89);
            this.groupBox4.Controls.Add(this.txt_top_slab_CW);
            this.groupBox4.Controls.Add(this.label90);
            this.groupBox4.Controls.Add(this.txt_top_slab_D);
            this.groupBox4.Controls.Add(this.label91);
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(368, 582);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "USER INPUT DATA :";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(179, 218);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(23, 14);
            this.label56.TabIndex = 43;
            this.label56.Text = "Fe";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(183, 190);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(18, 14);
            this.label57.TabIndex = 42;
            this.label57.Text = "M";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(297, 465);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(25, 14);
            this.label58.TabIndex = 41;
            this.label58.Text = "kN";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(297, 494);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(31, 14);
            this.label59.TabIndex = 41;
            this.label59.Text = "mm";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(297, 437);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(19, 14);
            this.label60.TabIndex = 41;
            this.label60.Text = "m";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(297, 411);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(19, 14);
            this.label61.TabIndex = 41;
            this.label61.Text = "m";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(297, 374);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(19, 14);
            this.label62.TabIndex = 41;
            this.label62.Text = "m";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(297, 162);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(19, 14);
            this.label63.TabIndex = 40;
            this.label63.Text = "m";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(297, 105);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(19, 14);
            this.label64.TabIndex = 40;
            this.label64.Text = "m";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(297, 77);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(19, 14);
            this.label65.TabIndex = 40;
            this.label65.Text = "m";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(297, 49);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(19, 14);
            this.label66.TabIndex = 40;
            this.label66.Text = "m";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(297, 549);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(56, 14);
            this.label67.TabIndex = 40;
            this.label67.Text = "N/cu.m";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(297, 522);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(56, 14);
            this.label68.TabIndex = 40;
            this.label68.Text = "N/cu.m";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(297, 244);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(72, 14);
            this.label69.TabIndex = 40;
            this.label69.Text = "N/sq. mm";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(297, 133);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(31, 14);
            this.label70.TabIndex = 40;
            this.label70.Text = "mm";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(297, 22);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(31, 14);
            this.label71.TabIndex = 40;
            this.label71.Text = "mm";
            // 
            // txt_top_slab_delta_wc
            // 
            this.txt_top_slab_delta_wc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_delta_wc.Location = new System.Drawing.Point(211, 547);
            this.txt_top_slab_delta_wc.Name = "txt_top_slab_delta_wc";
            this.txt_top_slab_delta_wc.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_delta_wc.TabIndex = 39;
            this.txt_top_slab_delta_wc.Text = "22";
            this.txt_top_slab_delta_wc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(6, 550);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(185, 13);
            this.label72.TabIndex = 38;
            this.label72.Text = "Unit weight of Wearing course [δ_wc]";
            // 
            // txt_top_slab_delta_c
            // 
            this.txt_top_slab_delta_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_delta_c.Location = new System.Drawing.Point(211, 519);
            this.txt_top_slab_delta_c.Name = "txt_top_slab_delta_c";
            this.txt_top_slab_delta_c.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_delta_c.TabIndex = 37;
            this.txt_top_slab_delta_c.Text = "24";
            this.txt_top_slab_delta_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(6, 522);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(145, 13);
            this.label73.TabIndex = 36;
            this.label73.Text = "Unit weight of Concrete [δ_c]";
            // 
            // txt_top_slab_cover
            // 
            this.txt_top_slab_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_cover.Location = new System.Drawing.Point(211, 491);
            this.txt_top_slab_cover.Name = "txt_top_slab_cover";
            this.txt_top_slab_cover.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_cover.TabIndex = 35;
            this.txt_top_slab_cover.Text = "30";
            this.txt_top_slab_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 494);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(205, 13);
            this.label74.TabIndex = 34;
            this.label74.Text = "Clear cover to Reinforcement Bars [cover]";
            // 
            // txt_top_slab_W1
            // 
            this.txt_top_slab_W1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_W1.Location = new System.Drawing.Point(211, 463);
            this.txt_top_slab_W1.Name = "txt_top_slab_W1";
            this.txt_top_slab_W1.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_W1.TabIndex = 33;
            this.txt_top_slab_W1.Text = "700";
            this.txt_top_slab_W1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(6, 466);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(107, 13);
            this.label75.TabIndex = 32;
            this.label75.Text = "Total Live Load [W1]";
            // 
            // txt_top_slab_b2
            // 
            this.txt_top_slab_b2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_b2.Location = new System.Drawing.Point(211, 435);
            this.txt_top_slab_b2.Name = "txt_top_slab_b2";
            this.txt_top_slab_b2.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_b2.TabIndex = 31;
            this.txt_top_slab_b2.Text = "1.20";
            this.txt_top_slab_b2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 438);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(127, 13);
            this.label76.TabIndex = 30;
            this.label76.Text = "Live Load Dimension [b2]";
            // 
            // txt_top_slab_b1
            // 
            this.txt_top_slab_b1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_b1.Location = new System.Drawing.Point(211, 409);
            this.txt_top_slab_b1.Name = "txt_top_slab_b1";
            this.txt_top_slab_b1.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_b1.TabIndex = 29;
            this.txt_top_slab_b1.Text = "0.85";
            this.txt_top_slab_b1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 412);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(127, 13);
            this.label77.TabIndex = 28;
            this.label77.Text = "Live Load Dimension [b1]";
            // 
            // txt_top_slab_a1
            // 
            this.txt_top_slab_a1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_a1.Location = new System.Drawing.Point(211, 381);
            this.txt_top_slab_a1.Name = "txt_top_slab_a1";
            this.txt_top_slab_a1.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_a1.TabIndex = 27;
            this.txt_top_slab_a1.Text = "3.6";
            this.txt_top_slab_a1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(6, 384);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(127, 13);
            this.label78.TabIndex = 26;
            this.label78.Text = "Live Load Dimension [a1]";
            // 
            // txt_top_slab_Q
            // 
            this.txt_top_slab_Q.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_Q.Location = new System.Drawing.Point(211, 353);
            this.txt_top_slab_Q.Name = "txt_top_slab_Q";
            this.txt_top_slab_Q.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_Q.TabIndex = 25;
            this.txt_top_slab_Q.Text = "1.1";
            this.txt_top_slab_Q.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(6, 356);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(108, 15);
            this.label79.TabIndex = 24;
            this.label79.Text = "Moment Factor [Q]";
            // 
            // txt_top_slab_j
            // 
            this.txt_top_slab_j.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_j.Location = new System.Drawing.Point(211, 325);
            this.txt_top_slab_j.Name = "txt_top_slab_j";
            this.txt_top_slab_j.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_j.TabIndex = 23;
            this.txt_top_slab_j.Text = "0.9";
            this.txt_top_slab_j.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(6, 328);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(131, 14);
            this.label80.TabIndex = 22;
            this.label80.Text = "Lever Arm Factor [j]";
            // 
            // txt_top_slab_m
            // 
            this.txt_top_slab_m.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_m.Location = new System.Drawing.Point(211, 297);
            this.txt_top_slab_m.Name = "txt_top_slab_m";
            this.txt_top_slab_m.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_m.TabIndex = 21;
            this.txt_top_slab_m.Text = "10";
            this.txt_top_slab_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(6, 300);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(90, 13);
            this.label81.TabIndex = 20;
            this.label81.Text = "Modular Ratio [m]";
            // 
            // txt_top_slab_sigma_st
            // 
            this.txt_top_slab_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_sigma_st.Location = new System.Drawing.Point(211, 271);
            this.txt_top_slab_sigma_st.Name = "txt_top_slab_sigma_st";
            this.txt_top_slab_sigma_st.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_sigma_st.TabIndex = 19;
            this.txt_top_slab_sigma_st.Text = "200";
            this.txt_top_slab_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(6, 274);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(121, 13);
            this.label82.TabIndex = 18;
            this.label82.Text = "Permissible Stress [σ_st]";
            // 
            // txt_top_slab_sigma_cb
            // 
            this.txt_top_slab_sigma_cb.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_sigma_cb.Location = new System.Drawing.Point(211, 243);
            this.txt_top_slab_sigma_cb.Name = "txt_top_slab_sigma_cb";
            this.txt_top_slab_sigma_cb.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_sigma_cb.TabIndex = 17;
            this.txt_top_slab_sigma_cb.Text = "8.3";
            this.txt_top_slab_sigma_cb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(6, 246);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(125, 13);
            this.label83.TabIndex = 16;
            this.label83.Text = "Permissible Stress [σ_cb]";
            // 
            // txt_top_slab_steel_grade
            // 
            this.txt_top_slab_steel_grade.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_steel_grade.Location = new System.Drawing.Point(211, 215);
            this.txt_top_slab_steel_grade.Name = "txt_top_slab_steel_grade";
            this.txt_top_slab_steel_grade.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_steel_grade.TabIndex = 15;
            this.txt_top_slab_steel_grade.Text = "415";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 219);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(63, 13);
            this.label84.TabIndex = 14;
            this.label84.Text = "Steel Grade";
            // 
            // txt_top_slab_concrete_grade
            // 
            this.txt_top_slab_concrete_grade.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_concrete_grade.Location = new System.Drawing.Point(211, 187);
            this.txt_top_slab_concrete_grade.Name = "txt_top_slab_concrete_grade";
            this.txt_top_slab_concrete_grade.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_concrete_grade.TabIndex = 13;
            this.txt_top_slab_concrete_grade.Text = "25";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(6, 190);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(82, 13);
            this.label85.TabIndex = 12;
            this.label85.Text = "Concrete Grade";
            // 
            // txt_top_slab_width_support
            // 
            this.txt_top_slab_width_support.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_width_support.Location = new System.Drawing.Point(211, 159);
            this.txt_top_slab_width_support.Name = "txt_top_slab_width_support";
            this.txt_top_slab_width_support.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_width_support.TabIndex = 11;
            this.txt_top_slab_width_support.Text = "0.4";
            this.txt_top_slab_width_support.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(6, 162);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(156, 13);
            this.label86.TabIndex = 10;
            this.label86.Text = "Width of End Support / Bearing";
            // 
            // txt_top_slab_WC
            // 
            this.txt_top_slab_WC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_WC.Location = new System.Drawing.Point(211, 131);
            this.txt_top_slab_WC.Name = "txt_top_slab_WC";
            this.txt_top_slab_WC.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_WC.TabIndex = 9;
            this.txt_top_slab_WC.Text = "80";
            this.txt_top_slab_WC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 134);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(174, 13);
            this.label87.TabIndex = 8;
            this.label87.Text = "Thickness of Wearing Course [WC]";
            // 
            // txt_top_slab_L
            // 
            this.txt_top_slab_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_L.Location = new System.Drawing.Point(211, 103);
            this.txt_top_slab_L.Name = "txt_top_slab_L";
            this.txt_top_slab_L.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_L.TabIndex = 7;
            this.txt_top_slab_L.Text = "6.0";
            this.txt_top_slab_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 106);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(74, 13);
            this.label88.TabIndex = 6;
            this.label88.Text = "Clear Span [L]";
            // 
            // txt_top_slab_FP
            // 
            this.txt_top_slab_FP.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_FP.Location = new System.Drawing.Point(211, 75);
            this.txt_top_slab_FP.Name = "txt_top_slab_FP";
            this.txt_top_slab_FP.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_FP.TabIndex = 5;
            this.txt_top_slab_FP.Text = "1.0";
            this.txt_top_slab_FP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(6, 78);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(99, 13);
            this.label89.TabIndex = 4;
            this.label89.Text = "Footpath width [FP]";
            // 
            // txt_top_slab_CW
            // 
            this.txt_top_slab_CW.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_CW.Location = new System.Drawing.Point(211, 47);
            this.txt_top_slab_CW.Name = "txt_top_slab_CW";
            this.txt_top_slab_CW.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_CW.TabIndex = 3;
            this.txt_top_slab_CW.Text = "7.5";
            this.txt_top_slab_CW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(6, 50);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(120, 13);
            this.label90.TabIndex = 2;
            this.label90.Text = "Carriageway width [CW]";
            // 
            // txt_top_slab_D
            // 
            this.txt_top_slab_D.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_top_slab_D.Location = new System.Drawing.Point(211, 19);
            this.txt_top_slab_D.Name = "txt_top_slab_D";
            this.txt_top_slab_D.Size = new System.Drawing.Size(80, 22);
            this.txt_top_slab_D.TabIndex = 1;
            this.txt_top_slab_D.Text = "500";
            this.txt_top_slab_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(6, 22);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(109, 13);
            this.label91.TabIndex = 0;
            this.label91.Text = "Thickness of Slab [D]";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.btn_top_slab_process);
            this.flowLayoutPanel1.Controls.Add(this.btn_top_slab_report);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(288, 605);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 32);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // btn_top_slab_process
            // 
            this.btn_top_slab_process.Location = new System.Drawing.Point(3, 3);
            this.btn_top_slab_process.Name = "btn_top_slab_process";
            this.btn_top_slab_process.Size = new System.Drawing.Size(130, 23);
            this.btn_top_slab_process.TabIndex = 1;
            this.btn_top_slab_process.Text = "Process";
            this.btn_top_slab_process.UseVisualStyleBackColor = true;
            this.btn_top_slab_process.Click += new System.EventHandler(this.btn_Top_Slab_Process_Click);
            // 
            // btn_top_slab_report
            // 
            this.btn_top_slab_report.Location = new System.Drawing.Point(139, 3);
            this.btn_top_slab_report.Name = "btn_top_slab_report";
            this.btn_top_slab_report.Size = new System.Drawing.Size(105, 23);
            this.btn_top_slab_report.TabIndex = 2;
            this.btn_top_slab_report.Text = "Report";
            this.btn_top_slab_report.UseVisualStyleBackColor = true;
            this.btn_top_slab_report.Click += new System.EventHandler(this.btn_Top_Slab_Report_Click);
            // 
            // pic_veh_top_slab
            // 
            this.pic_veh_top_slab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_veh_top_slab.Location = new System.Drawing.Point(376, 14);
            this.pic_veh_top_slab.Name = "pic_veh_top_slab";
            this.pic_veh_top_slab.Size = new System.Drawing.Size(480, 574);
            this.pic_veh_top_slab.TabIndex = 10;
            this.pic_veh_top_slab.TabStop = false;
            // 
            // tab_RCC_Abutment
            // 
            this.tab_RCC_Abutment.Controls.Add(this.groupBox1);
            this.tab_RCC_Abutment.Controls.Add(this.groupBox2);
            this.tab_RCC_Abutment.Controls.Add(this.groupBox3);
            this.tab_RCC_Abutment.Location = new System.Drawing.Point(4, 22);
            this.tab_RCC_Abutment.Name = "tab_RCC_Abutment";
            this.tab_RCC_Abutment.Padding = new System.Windows.Forms.Padding(3);
            this.tab_RCC_Abutment.Size = new System.Drawing.Size(862, 653);
            this.tab_RCC_Abutment.TabIndex = 1;
            this.tab_RCC_Abutment.Text = "RCC Abutment";
            this.tab_RCC_Abutment.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.txt_abut_h1);
            this.groupBox1.Controls.Add(this.txt_abut_L);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.lblCover);
            this.groupBox1.Controls.Add(this.txt_abut_cover);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.txt_abut_fact);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.txt_abut_F);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.txt_abut_mu);
            this.groupBox1.Controls.Add(this.txt_abut_gamma_b);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_abut_gamma_c);
            this.groupBox1.Controls.Add(this.txt_abut_z);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt_abut_delta);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txt_abut_theta);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_abut_w5);
            this.groupBox1.Controls.Add(this.txt_abut_phi);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_abut_w6);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_abut_steel_grade);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_abut_concrete_grade);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_abut_p_bearing_capacity);
            this.groupBox1.Location = new System.Drawing.Point(3, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(850, 245);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER DATA";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(352, 217);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(22, 16);
            this.label50.TabIndex = 73;
            this.label50.Text = "m";
            this.label50.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(11, 221);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(162, 13);
            this.label51.TabIndex = 72;
            this.label51.Text = "Break is applied at a height [ h1 ]";
            // 
            // txt_abut_h1
            // 
            this.txt_abut_h1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_h1.Location = new System.Drawing.Point(253, 215);
            this.txt_abut_h1.Name = "txt_abut_h1";
            this.txt_abut_h1.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_h1.TabIndex = 7;
            this.txt_abut_h1.Text = "1.2";
            this.txt_abut_h1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_abut_L
            // 
            this.txt_abut_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_L.Location = new System.Drawing.Point(641, 218);
            this.txt_abut_L.Name = "txt_abut_L";
            this.txt_abut_L.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_L.TabIndex = 16;
            this.txt_abut_L.Text = "16.1";
            this.txt_abut_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(431, 221);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(150, 13);
            this.label52.TabIndex = 68;
            this.label52.Text = "Span of Longitudinal Girder [L]";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(741, 221);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(22, 16);
            this.label53.TabIndex = 70;
            this.label53.Text = "m";
            this.label53.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(352, 192);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(36, 16);
            this.label48.TabIndex = 67;
            this.label48.Text = "mm";
            this.label48.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCover
            // 
            this.lblCover.AutoSize = true;
            this.lblCover.Location = new System.Drawing.Point(11, 194);
            this.lblCover.Name = "lblCover";
            this.lblCover.Size = new System.Drawing.Size(98, 13);
            this.lblCover.TabIndex = 66;
            this.lblCover.Text = "Clear Cover [cover]";
            // 
            // txt_abut_cover
            // 
            this.txt_abut_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_cover.Location = new System.Drawing.Point(253, 190);
            this.txt_abut_cover.Name = "txt_abut_cover";
            this.txt_abut_cover.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_cover.TabIndex = 6;
            this.txt_abut_cover.Text = "50";
            this.txt_abut_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(429, 55);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(128, 26);
            this.label49.TabIndex = 64;
            this.label49.Text = "Bending Moment and\r\nShear Force Factor [Fact]";
            // 
            // txt_abut_fact
            // 
            this.txt_abut_fact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_fact.Location = new System.Drawing.Point(641, 59);
            this.txt_abut_fact.Name = "txt_abut_fact";
            this.txt_abut_fact.Size = new System.Drawing.Size(93, 22);
            this.txt_abut_fact.TabIndex = 10;
            this.txt_abut_fact.Text = "1.5";
            this.txt_abut_fact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(740, 138);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(26, 16);
            this.label44.TabIndex = 62;
            this.label44.Text = "kN";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(429, 140);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(88, 13);
            this.label45.TabIndex = 61;
            this.label45.Text = "Braking Force [F]";
            // 
            // txt_abut_F
            // 
            this.txt_abut_F.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_F.Location = new System.Drawing.Point(640, 136);
            this.txt_abut_F.Name = "txt_abut_F";
            this.txt_abut_F.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_F.TabIndex = 13;
            this.txt_abut_F.Text = "200";
            this.txt_abut_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(350, 106);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(15, 16);
            this.label23.TabIndex = 59;
            this.label23.Text = "°";
            this.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(351, 73);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(15, 16);
            this.label22.TabIndex = 58;
            this.label22.Text = "°";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(11, 137);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(232, 13);
            this.label40.TabIndex = 57;
            this.label40.Text = "Coefficient of friction between Earth and wall [µ]";
            // 
            // txt_abut_mu
            // 
            this.txt_abut_mu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_mu.Location = new System.Drawing.Point(253, 133);
            this.txt_abut_mu.Name = "txt_abut_mu";
            this.txt_abut_mu.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_mu.TabIndex = 4;
            this.txt_abut_mu.Text = "0.6";
            this.txt_abut_mu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_abut_gamma_b
            // 
            this.txt_abut_gamma_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_gamma_b.Location = new System.Drawing.Point(640, 163);
            this.txt_abut_gamma_b.Name = "txt_abut_gamma_b";
            this.txt_abut_gamma_b.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_gamma_b.TabIndex = 14;
            this.txt_abut_gamma_b.Text = "17";
            this.txt_abut_gamma_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(11, 76);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(208, 13);
            this.label39.TabIndex = 55;
            this.label39.Text = "Angle of friction between Earth and wall [z]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Unit Weight of Backfill Earth [γ_b]";
            // 
            // txt_abut_gamma_c
            // 
            this.txt_abut_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_gamma_c.Location = new System.Drawing.Point(641, 193);
            this.txt_abut_gamma_c.Name = "txt_abut_gamma_c";
            this.txt_abut_gamma_c.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_gamma_c.TabIndex = 15;
            this.txt_abut_gamma_c.Text = "25";
            this.txt_abut_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_abut_z
            // 
            this.txt_abut_z.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_z.Location = new System.Drawing.Point(253, 72);
            this.txt_abut_z.Name = "txt_abut_z";
            this.txt_abut_z.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_z.TabIndex = 2;
            this.txt_abut_z.Text = "17.5";
            this.txt_abut_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Unit weight of Concrete [γ_c]";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(11, 16);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(170, 26);
            this.label37.TabIndex = 50;
            this.label37.Text = "Angle between wall and Horizontal\r\nbase on Earth Side [θ]";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(11, 101);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(134, 26);
            this.label38.TabIndex = 53;
            this.label38.Text = "Inclination of Earth fill Side \r\nwith the Horizontal [δ]";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(740, 165);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "kN/cu.m.";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_abut_delta
            // 
            this.txt_abut_delta.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_delta.Location = new System.Drawing.Point(253, 105);
            this.txt_abut_delta.Name = "txt_abut_delta";
            this.txt_abut_delta.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_delta.TabIndex = 3;
            this.txt_abut_delta.Text = "0";
            this.txt_abut_delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(741, 196);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 16);
            this.label16.TabIndex = 21;
            this.label16.Text = "kN/cu.m.";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(352, 14);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(22, 16);
            this.label36.TabIndex = 51;
            this.label36.Text = "m";
            this.label36.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(740, 114);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 16);
            this.label20.TabIndex = 27;
            this.label20.Text = "kN/m";
            // 
            // txt_abut_theta
            // 
            this.txt_abut_theta.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_theta.Location = new System.Drawing.Point(253, 12);
            this.txt_abut_theta.Name = "txt_abut_theta";
            this.txt_abut_theta.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_theta.TabIndex = 0;
            this.txt_abut_theta.Text = "90";
            this.txt_abut_theta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(429, 116);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(208, 13);
            this.label21.TabIndex = 26;
            this.label21.Text = "Permanent Load from Super Structure [w5]";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(350, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 16);
            this.label14.TabIndex = 18;
            this.label14.Text = "°";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_abut_w5
            // 
            this.txt_abut_w5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_w5.Location = new System.Drawing.Point(640, 112);
            this.txt_abut_w5.Name = "txt_abut_w5";
            this.txt_abut_w5.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_w5.TabIndex = 12;
            this.txt_abut_w5.Text = "119.0";
            this.txt_abut_w5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_abut_phi
            // 
            this.txt_abut_phi.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_phi.Location = new System.Drawing.Point(253, 44);
            this.txt_abut_phi.Name = "txt_abut_phi";
            this.txt_abut_phi.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_phi.TabIndex = 1;
            this.txt_abut_phi.Text = "35";
            this.txt_abut_phi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(740, 90);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 16);
            this.label18.TabIndex = 24;
            this.label18.Text = "kN/m";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(224, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Angle Internal Friction (Repose) of Back fill [φ]";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(431, 93);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(142, 13);
            this.label19.TabIndex = 23;
            this.label19.Text = "Live Load from vehicles [w6]";
            // 
            // txt_abut_w6
            // 
            this.txt_abut_w6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_w6.Location = new System.Drawing.Point(640, 88);
            this.txt_abut_w6.Name = "txt_abut_w6";
            this.txt_abut_w6.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_w6.TabIndex = 11;
            this.txt_abut_w6.Text = "85.0";
            this.txt_abut_w6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(353, 165);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 21;
            this.label17.Text = "kN/sq.m.";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(632, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Fe";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(635, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "M";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Steel Grade [σ_st]";
            // 
            // txt_abut_steel_grade
            // 
            this.txt_abut_steel_grade.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_steel_grade.Location = new System.Drawing.Point(660, 35);
            this.txt_abut_steel_grade.Name = "txt_abut_steel_grade";
            this.txt_abut_steel_grade.Size = new System.Drawing.Size(74, 22);
            this.txt_abut_steel_grade.TabIndex = 9;
            this.txt_abut_steel_grade.Text = "415";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(430, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Concrete Grade [σ_c]";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_abut_concrete_grade
            // 
            this.txt_abut_concrete_grade.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_concrete_grade.Location = new System.Drawing.Point(661, 12);
            this.txt_abut_concrete_grade.Name = "txt_abut_concrete_grade";
            this.txt_abut_concrete_grade.Size = new System.Drawing.Size(74, 22);
            this.txt_abut_concrete_grade.TabIndex = 8;
            this.txt_abut_concrete_grade.Text = "25";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Bearing Capacity [p]";
            // 
            // txt_abut_p_bearing_capacity
            // 
            this.txt_abut_p_bearing_capacity.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_p_bearing_capacity.Location = new System.Drawing.Point(253, 162);
            this.txt_abut_p_bearing_capacity.Name = "txt_abut_p_bearing_capacity";
            this.txt_abut_p_bearing_capacity.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_p_bearing_capacity.TabIndex = 5;
            this.txt_abut_p_bearing_capacity.Text = "150";
            this.txt_abut_p_bearing_capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Controls.Add(this.btnProcess);
            this.groupBox2.Location = new System.Drawing.Point(176, 613);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 40);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(275, 9);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(92, 23);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btn_Abut_Report_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(113, 9);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(92, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btn_Abut_Process_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_abut_d4);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Controls.Add(this.label55);
            this.groupBox3.Controls.Add(this.txt_abut_L4);
            this.groupBox3.Controls.Add(this.label46);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.pic_veh_rcc_abut);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.txt_abut_t);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.txt_abut_d3);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.txt_abut_L3);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.txt_abut_L2);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.txt_abut_L1);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.txt_abut_d1);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.txt_abut_d2);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.txt_abut_B);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txt_abut_a);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txt_abut_H);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(850, 355);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "USER DATA";
            // 
            // txt_abut_d4
            // 
            this.txt_abut_d4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_d4.Location = new System.Drawing.Point(253, 218);
            this.txt_abut_d4.Name = "txt_abut_d4";
            this.txt_abut_d4.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_d4.TabIndex = 7;
            this.txt_abut_d4.Text = "0.75";
            this.txt_abut_d4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(17, 223);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(116, 13);
            this.label54.TabIndex = 88;
            this.label54.Text = "Thickness of Base [d4]";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(351, 220);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(22, 16);
            this.label55.TabIndex = 87;
            this.label55.Text = "m";
            this.label55.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_abut_L4
            // 
            this.txt_abut_L4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_L4.Location = new System.Drawing.Point(253, 324);
            this.txt_abut_L4.Name = "txt_abut_L4";
            this.txt_abut_L4.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_L4.TabIndex = 11;
            this.txt_abut_L4.Text = "0.3";
            this.txt_abut_L4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(18, 328);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(218, 13);
            this.label46.TabIndex = 85;
            this.label46.Text = "Thickness of wall at Girder Seat the Top [L4]";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(351, 329);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(22, 16);
            this.label47.TabIndex = 84;
            this.label47.Text = "m";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(351, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 16);
            this.label8.TabIndex = 80;
            this.label8.Text = "m";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(353, 46);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(22, 16);
            this.label41.TabIndex = 81;
            this.label41.Text = "m";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pic_veh_rcc_abut
            // 
            this.pic_veh_rcc_abut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_veh_rcc_abut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_veh_rcc_abut.Location = new System.Drawing.Point(405, 12);
            this.pic_veh_rcc_abut.Name = "pic_veh_rcc_abut";
            this.pic_veh_rcc_abut.Size = new System.Drawing.Size(439, 329);
            this.pic_veh_rcc_abut.TabIndex = 0;
            this.pic_veh_rcc_abut.TabStop = false;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(17, 48);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(127, 13);
            this.label42.TabIndex = 79;
            this.label42.Text = "Thickness of Main wall [t]";
            // 
            // txt_abut_t
            // 
            this.txt_abut_t.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_t.Location = new System.Drawing.Point(253, 44);
            this.txt_abut_t.Name = "txt_abut_t";
            this.txt_abut_t.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_t.TabIndex = 1;
            this.txt_abut_t.Text = "1.0";
            this.txt_abut_t.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(17, 24);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(125, 13);
            this.label43.TabIndex = 77;
            this.label43.Text = "Depth of Girder Seat [d1]";
            // 
            // txt_abut_d3
            // 
            this.txt_abut_d3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_d3.Location = new System.Drawing.Point(253, 190);
            this.txt_abut_d3.Name = "txt_abut_d3";
            this.txt_abut_d3.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_d3.TabIndex = 6;
            this.txt_abut_d3.Text = "0.3";
            this.txt_abut_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(350, 305);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(22, 16);
            this.label34.TabIndex = 75;
            this.label34.Text = "m";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(18, 302);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(172, 13);
            this.label35.TabIndex = 74;
            this.label35.Text = "Length of Base at back of wall [L3]";
            // 
            // txt_abut_L3
            // 
            this.txt_abut_L3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_L3.Location = new System.Drawing.Point(253, 298);
            this.txt_abut_L3.Name = "txt_abut_L3";
            this.txt_abut_L3.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_L3.TabIndex = 10;
            this.txt_abut_L3.Text = "1.2";
            this.txt_abut_L3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(350, 278);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(22, 16);
            this.label32.TabIndex = 72;
            this.label32.Text = "m";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(18, 276);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(175, 13);
            this.label33.TabIndex = 71;
            this.label33.Text = "Length of base in wall Location [L2]";
            // 
            // txt_abut_L2
            // 
            this.txt_abut_L2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_L2.Location = new System.Drawing.Point(253, 272);
            this.txt_abut_L2.Name = "txt_abut_L2";
            this.txt_abut_L2.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_L2.TabIndex = 9;
            this.txt_abut_L2.Text = "1.0";
            this.txt_abut_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(350, 252);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(22, 16);
            this.label30.TabIndex = 69;
            this.label30.Text = "m";
            this.label30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(18, 250);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(167, 13);
            this.label31.TabIndex = 68;
            this.label31.Text = "Length of base in front of wall [L1]";
            // 
            // txt_abut_L1
            // 
            this.txt_abut_L1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_L1.Location = new System.Drawing.Point(253, 246);
            this.txt_abut_L1.Name = "txt_abut_L1";
            this.txt_abut_L1.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_L1.TabIndex = 8;
            this.txt_abut_L1.Text = "2.6";
            this.txt_abut_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(350, 193);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(22, 16);
            this.label28.TabIndex = 66;
            this.label28.Text = "m";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(17, 199);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(162, 13);
            this.label29.TabIndex = 65;
            this.label29.Text = "Thickness of Approach Slab [d3]";
            // 
            // txt_abut_d1
            // 
            this.txt_abut_d1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_d1.Location = new System.Drawing.Point(253, 15);
            this.txt_abut_d1.Name = "txt_abut_d1";
            this.txt_abut_d1.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_d1.TabIndex = 0;
            this.txt_abut_d1.Text = "1.6";
            this.txt_abut_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(350, 162);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(22, 16);
            this.label26.TabIndex = 63;
            this.label26.Text = "m";
            this.label26.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 156);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(146, 26);
            this.label27.TabIndex = 62;
            this.label27.Text = "Equivalent height of earth for \r\nLive Load Surcharge [d2]";
            // 
            // txt_abut_d2
            // 
            this.txt_abut_d2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_d2.Location = new System.Drawing.Point(253, 160);
            this.txt_abut_d2.Name = "txt_abut_d2";
            this.txt_abut_d2.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_d2.TabIndex = 5;
            this.txt_abut_d2.Text = "1.2";
            this.txt_abut_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(351, 130);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(22, 16);
            this.label24.TabIndex = 60;
            this.label24.Text = "m";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(17, 132);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(84, 13);
            this.label25.TabIndex = 59;
            this.label25.Text = "Width of wall [B]";
            // 
            // txt_abut_B
            // 
            this.txt_abut_B.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_B.Location = new System.Drawing.Point(253, 128);
            this.txt_abut_B.Name = "txt_abut_B";
            this.txt_abut_B.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_B.TabIndex = 4;
            this.txt_abut_B.Text = "8.5";
            this.txt_abut_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(351, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 16);
            this.label12.TabIndex = 53;
            this.label12.Text = "m";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(352, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 16);
            this.label11.TabIndex = 54;
            this.label11.Text = "m";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Height of Earth at front [a]";
            // 
            // txt_abut_a
            // 
            this.txt_abut_a.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_a.Location = new System.Drawing.Point(253, 100);
            this.txt_abut_a.Name = "txt_abut_a";
            this.txt_abut_a.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_a.TabIndex = 3;
            this.txt_abut_a.Text = "1.25";
            this.txt_abut_a.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Height of Retained Earth [H]";
            // 
            // txt_abut_H
            // 
            this.txt_abut_H.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_abut_H.Location = new System.Drawing.Point(253, 72);
            this.txt_abut_H.Name = "txt_abut_H";
            this.txt_abut_H.Size = new System.Drawing.Size(94, 22);
            this.txt_abut_H.TabIndex = 2;
            this.txt_abut_H.Text = "5.6";
            this.txt_abut_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tab_Pedestrian
            // 
            this.tab_Pedestrian.Controls.Add(this.groupBox7);
            this.tab_Pedestrian.Location = new System.Drawing.Point(4, 22);
            this.tab_Pedestrian.Name = "tab_Pedestrian";
            this.tab_Pedestrian.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Pedestrian.Size = new System.Drawing.Size(876, 685);
            this.tab_Pedestrian.TabIndex = 1;
            this.tab_Pedestrian.Text = "Pedestrian Underpass";
            this.tab_Pedestrian.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txt_sigma_st);
            this.groupBox7.Controls.Add(this.panel1);
            this.groupBox7.Controls.Add(this.label92);
            this.groupBox7.Controls.Add(this.label94);
            this.groupBox7.Controls.Add(this.txt_sigma_c);
            this.groupBox7.Controls.Add(this.label95);
            this.groupBox7.Controls.Add(this.label96);
            this.groupBox7.Controls.Add(this.groupBox5);
            this.groupBox7.Controls.Add(this.groupBox6);
            this.groupBox7.Controls.Add(this.txt_cover);
            this.groupBox7.Controls.Add(this.label110);
            this.groupBox7.Controls.Add(this.label111);
            this.groupBox7.Controls.Add(this.label112);
            this.groupBox7.Controls.Add(this.label115);
            this.groupBox7.Controls.Add(this.label113);
            this.groupBox7.Controls.Add(this.label114);
            this.groupBox7.Controls.Add(this.label116);
            this.groupBox7.Controls.Add(this.label117);
            this.groupBox7.Controls.Add(this.label118);
            this.groupBox7.Controls.Add(this.label119);
            this.groupBox7.Controls.Add(this.pic_pedestrian);
            this.groupBox7.Controls.Add(this.txt_j);
            this.groupBox7.Controls.Add(this.label120);
            this.groupBox7.Controls.Add(this.txt_t);
            this.groupBox7.Controls.Add(this.label121);
            this.groupBox7.Controls.Add(this.txt_R);
            this.groupBox7.Controls.Add(this.label122);
            this.groupBox7.Controls.Add(this.txt_gamma_c);
            this.groupBox7.Controls.Add(this.label123);
            this.groupBox7.Controls.Add(this.txt_gamma_b);
            this.groupBox7.Controls.Add(this.label124);
            this.groupBox7.Controls.Add(this.txt_d3);
            this.groupBox7.Controls.Add(this.label125);
            this.groupBox7.Controls.Add(this.txt_d2);
            this.groupBox7.Controls.Add(this.label126);
            this.groupBox7.Controls.Add(this.txt_d1);
            this.groupBox7.Controls.Add(this.label127);
            this.groupBox7.Controls.Add(this.txt_d);
            this.groupBox7.Controls.Add(this.label128);
            this.groupBox7.Controls.Add(this.txt_b);
            this.groupBox7.Controls.Add(this.label129);
            this.groupBox7.Controls.Add(this.txt_H);
            this.groupBox7.Controls.Add(this.label130);
            this.groupBox7.Location = new System.Drawing.Point(52, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(789, 655);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "USER DATA";
            // 
            // txt_sigma_st
            // 
            this.txt_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_st.Location = new System.Drawing.Point(188, 350);
            this.txt_sigma_st.Name = "txt_sigma_st";
            this.txt_sigma_st.Size = new System.Drawing.Size(77, 22);
            this.txt_sigma_st.TabIndex = 51;
            this.txt_sigma_st.Text = "415";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_box_Report);
            this.panel1.Controls.Add(this.btn_box_Process);
            this.panel1.Location = new System.Drawing.Point(117, 611);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 38);
            this.panel1.TabIndex = 14;
            // 
            // btn_box_Report
            // 
            this.btn_box_Report.Enabled = false;
            this.btn_box_Report.Location = new System.Drawing.Point(280, 6);
            this.btn_box_Report.Name = "btn_box_Report";
            this.btn_box_Report.Size = new System.Drawing.Size(86, 23);
            this.btn_box_Report.TabIndex = 2;
            this.btn_box_Report.Text = "Report";
            this.btn_box_Report.UseVisualStyleBackColor = true;
            this.btn_box_Report.Click += new System.EventHandler(this.btn_Box_Report_Click);
            // 
            // btn_box_Process
            // 
            this.btn_box_Process.Location = new System.Drawing.Point(98, 6);
            this.btn_box_Process.Name = "btn_box_Process";
            this.btn_box_Process.Size = new System.Drawing.Size(82, 23);
            this.btn_box_Process.TabIndex = 1;
            this.btn_box_Process.Text = "Process";
            this.btn_box_Process.UseVisualStyleBackColor = true;
            this.btn_box_Process.Click += new System.EventHandler(this.btn_Box_Process_Click);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(18, 356);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(93, 13);
            this.label92.TabIndex = 53;
            this.label92.Text = "Steel Grade [σ_st]";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(162, 353);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(26, 16);
            this.label94.TabIndex = 52;
            this.label94.Text = "Fe";
            // 
            // txt_sigma_c
            // 
            this.txt_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_c.Location = new System.Drawing.Point(188, 327);
            this.txt_sigma_c.Name = "txt_sigma_c";
            this.txt_sigma_c.Size = new System.Drawing.Size(77, 22);
            this.txt_sigma_c.TabIndex = 48;
            this.txt_sigma_c.Text = "25";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(18, 331);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(110, 13);
            this.label95.TabIndex = 50;
            this.label95.Text = "Concrete Grade [σ_c]";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(162, 329);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(20, 16);
            this.label96.TabIndex = 49;
            this.label96.Text = "M";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txt_b1);
            this.groupBox5.Controls.Add(this.label97);
            this.groupBox5.Controls.Add(this.label98);
            this.groupBox5.Controls.Add(this.txt_b2);
            this.groupBox5.Controls.Add(this.label99);
            this.groupBox5.Controls.Add(this.txt_a1);
            this.groupBox5.Controls.Add(this.label100);
            this.groupBox5.Controls.Add(this.label101);
            this.groupBox5.Controls.Add(this.txt_w1);
            this.groupBox5.Location = new System.Drawing.Point(8, 370);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(321, 129);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "For Single Track Loading in 2 Lane";
            // 
            // txt_b1
            // 
            this.txt_b1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b1.Location = new System.Drawing.Point(199, 23);
            this.txt_b1.Name = "txt_b1";
            this.txt_b1.Size = new System.Drawing.Size(77, 22);
            this.txt_b1.TabIndex = 0;
            this.txt_b1.Text = "1.22";
            this.txt_b1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(8, 27);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(186, 13);
            this.label97.TabIndex = 22;
            this.label97.Text = "Separating distance of two Loads [b1]";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(8, 53);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(160, 13);
            this.label98.TabIndex = 24;
            this.label98.Text = "Width of Each Loaded Area [b2]";
            // 
            // txt_b2
            // 
            this.txt_b2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b2.Location = new System.Drawing.Point(199, 49);
            this.txt_b2.Name = "txt_b2";
            this.txt_b2.Size = new System.Drawing.Size(77, 22);
            this.txt_b2.TabIndex = 1;
            this.txt_b2.Text = "0.84";
            this.txt_b2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(8, 81);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(165, 13);
            this.label99.TabIndex = 26;
            this.label99.Text = "Length of Each Loaded Area [a1]";
            // 
            // txt_a1
            // 
            this.txt_a1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_a1.Location = new System.Drawing.Point(199, 77);
            this.txt_a1.Name = "txt_a1";
            this.txt_a1.Size = new System.Drawing.Size(77, 22);
            this.txt_a1.TabIndex = 2;
            this.txt_a1.Text = "4.57";
            this.txt_a1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(291, 104);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(26, 16);
            this.label100.TabIndex = 41;
            this.label100.Text = "kN";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(8, 107);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(81, 13);
            this.label101.TabIndex = 28;
            this.label101.Text = "Total Load [w1]";
            // 
            // txt_w1
            // 
            this.txt_w1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_w1.Location = new System.Drawing.Point(199, 103);
            this.txt_w1.Name = "txt_w1";
            this.txt_w1.Size = new System.Drawing.Size(77, 22);
            this.txt_w1.TabIndex = 3;
            this.txt_w1.Text = "700";
            this.txt_w1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txt_w2);
            this.groupBox6.Controls.Add(this.label102);
            this.groupBox6.Controls.Add(this.label103);
            this.groupBox6.Controls.Add(this.txt_b3);
            this.groupBox6.Controls.Add(this.label104);
            this.groupBox6.Controls.Add(this.label105);
            this.groupBox6.Controls.Add(this.txt_F);
            this.groupBox6.Controls.Add(this.label106);
            this.groupBox6.Controls.Add(this.label107);
            this.groupBox6.Controls.Add(this.txt_S);
            this.groupBox6.Controls.Add(this.label108);
            this.groupBox6.Controls.Add(this.label109);
            this.groupBox6.Controls.Add(this.txt_sbc);
            this.groupBox6.Controls.Add(this.label93);
            this.groupBox6.Location = new System.Drawing.Point(8, 499);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(767, 106);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "For Double Track Loading in 4 Lane";
            // 
            // txt_w2
            // 
            this.txt_w2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_w2.Location = new System.Drawing.Point(199, 19);
            this.txt_w2.Name = "txt_w2";
            this.txt_w2.Size = new System.Drawing.Size(77, 22);
            this.txt_w2.TabIndex = 0;
            this.txt_w2.Text = "1400";
            this.txt_w2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(6, 23);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(81, 13);
            this.label102.TabIndex = 31;
            this.label102.Text = "Total Load [w2]";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(6, 49);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(121, 26);
            this.label103.TabIndex = 33;
            this.label103.Text = "Separating Distance \r\nbetween two tracks [b3]";
            // 
            // txt_b3
            // 
            this.txt_b3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b3.Location = new System.Drawing.Point(199, 45);
            this.txt_b3.Name = "txt_b3";
            this.txt_b3.Size = new System.Drawing.Size(77, 22);
            this.txt_b3.TabIndex = 1;
            this.txt_b3.Text = "1.2";
            this.txt_b3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(659, 53);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(76, 16);
            this.label104.TabIndex = 41;
            this.label104.Text = "kN/sq.m.";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(6, 77);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(171, 13);
            this.label105.TabIndex = 35;
            this.label105.Text = "Two track load dispertion factor [F]";
            // 
            // txt_F
            // 
            this.txt_F.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_F.Location = new System.Drawing.Point(199, 73);
            this.txt_F.Name = "txt_F";
            this.txt_F.Size = new System.Drawing.Size(77, 22);
            this.txt_F.TabIndex = 2;
            this.txt_F.Text = "0.8";
            this.txt_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(282, 21);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(26, 16);
            this.label106.TabIndex = 41;
            this.label106.Text = "kN";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(355, 16);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(137, 26);
            this.label107.TabIndex = 37;
            this.label107.Text = "Equivalent Earth Height\r\nfor Live Load Surcharge [S]";
            // 
            // txt_S
            // 
            this.txt_S.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_S.Location = new System.Drawing.Point(576, 20);
            this.txt_S.Name = "txt_S";
            this.txt_S.Size = new System.Drawing.Size(77, 22);
            this.txt_S.TabIndex = 3;
            this.txt_S.Text = "1.2";
            this.txt_S.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(355, 53);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(188, 13);
            this.label108.TabIndex = 39;
            this.label108.Text = "Safe Bearing Capacity of Ground [sbc]";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(659, 23);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(22, 16);
            this.label109.TabIndex = 41;
            this.label109.Text = "m";
            // 
            // txt_sbc
            // 
            this.txt_sbc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sbc.Location = new System.Drawing.Point(576, 49);
            this.txt_sbc.Name = "txt_sbc";
            this.txt_sbc.Size = new System.Drawing.Size(77, 22);
            this.txt_sbc.TabIndex = 4;
            this.txt_sbc.Text = "150";
            this.txt_sbc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(282, 47);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(22, 16);
            this.label93.TabIndex = 41;
            this.label93.Text = "m";
            // 
            // txt_cover
            // 
            this.txt_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cover.Location = new System.Drawing.Point(188, 303);
            this.txt_cover.Name = "txt_cover";
            this.txt_cover.Size = new System.Drawing.Size(77, 22);
            this.txt_cover.TabIndex = 11;
            this.txt_cover.Text = "50";
            this.txt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(18, 307);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(98, 13);
            this.label110.TabIndex = 47;
            this.label110.Text = "Clear Cover [cover]";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(271, 305);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(36, 16);
            this.label111.TabIndex = 46;
            this.label111.Text = "mm";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(271, 206);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(76, 16);
            this.label112.TabIndex = 41;
            this.label112.Text = "kN/cu.m.";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(271, 180);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(76, 16);
            this.label115.TabIndex = 41;
            this.label115.Text = "kN/cu.m.";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(271, 154);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(22, 16);
            this.label113.TabIndex = 41;
            this.label113.Text = "m";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(271, 128);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(22, 16);
            this.label114.TabIndex = 41;
            this.label114.Text = "m";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.Location = new System.Drawing.Point(271, 102);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(22, 16);
            this.label116.TabIndex = 41;
            this.label116.Text = "m";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(271, 76);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(22, 16);
            this.label117.TabIndex = 41;
            this.label117.Text = "m";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(271, 50);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(22, 16);
            this.label118.TabIndex = 41;
            this.label118.Text = "m";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(271, 26);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(22, 16);
            this.label119.TabIndex = 41;
            this.label119.Text = "m";
            // 
            // pic_pedestrian
            // 
            this.pic_pedestrian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_pedestrian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_pedestrian.Location = new System.Drawing.Point(350, 19);
            this.pic_pedestrian.Name = "pic_pedestrian";
            this.pic_pedestrian.Size = new System.Drawing.Size(425, 480);
            this.pic_pedestrian.TabIndex = 30;
            this.pic_pedestrian.TabStop = false;
            // 
            // txt_j
            // 
            this.txt_j.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_j.Location = new System.Drawing.Point(188, 278);
            this.txt_j.Name = "txt_j";
            this.txt_j.Size = new System.Drawing.Size(77, 22);
            this.txt_j.TabIndex = 10;
            this.txt_j.Text = "0.902";
            this.txt_j.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(18, 282);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(131, 14);
            this.label120.TabIndex = 20;
            this.label120.Text = "Lever Arm Factor [j]";
            // 
            // txt_t
            // 
            this.txt_t.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_t.Location = new System.Drawing.Point(188, 252);
            this.txt_t.Name = "txt_t";
            this.txt_t.Size = new System.Drawing.Size(77, 22);
            this.txt_t.TabIndex = 9;
            this.txt_t.Text = "200";
            this.txt_t.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(18, 256);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(22, 14);
            this.label121.TabIndex = 18;
            this.label121.Text = "[t]";
            // 
            // txt_R
            // 
            this.txt_R.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_R.Location = new System.Drawing.Point(188, 228);
            this.txt_R.Name = "txt_R";
            this.txt_R.Size = new System.Drawing.Size(77, 22);
            this.txt_R.TabIndex = 8;
            this.txt_R.Text = "1.105";
            this.txt_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(18, 232);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(25, 14);
            this.label122.TabIndex = 16;
            this.label122.Text = "[R]";
            // 
            // txt_gamma_c
            // 
            this.txt_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_c.Location = new System.Drawing.Point(188, 204);
            this.txt_gamma_c.Name = "txt_gamma_c";
            this.txt_gamma_c.Size = new System.Drawing.Size(77, 22);
            this.txt_gamma_c.TabIndex = 7;
            this.txt_gamma_c.Text = "24";
            this.txt_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(18, 208);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(139, 13);
            this.label123.TabIndex = 14;
            this.label123.Text = "Unit weight of Conrete [γ_c]";
            // 
            // txt_gamma_b
            // 
            this.txt_gamma_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_b.Location = new System.Drawing.Point(188, 178);
            this.txt_gamma_b.Name = "txt_gamma_b";
            this.txt_gamma_b.Size = new System.Drawing.Size(77, 22);
            this.txt_gamma_b.TabIndex = 6;
            this.txt_gamma_b.Text = "18";
            this.txt_gamma_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(18, 182);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(130, 13);
            this.label124.TabIndex = 12;
            this.label124.Text = "Unit Weight of Earth [γ_b]";
            // 
            // txt_d3
            // 
            this.txt_d3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d3.Location = new System.Drawing.Point(188, 152);
            this.txt_d3.Name = "txt_d3";
            this.txt_d3.Size = new System.Drawing.Size(77, 22);
            this.txt_d3.TabIndex = 5;
            this.txt_d3.Text = "0.42";
            this.txt_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(18, 156);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(139, 13);
            this.label125.TabIndex = 10;
            this.label125.Text = "Thickness of Side walls [d3]";
            // 
            // txt_d2
            // 
            this.txt_d2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d2.Location = new System.Drawing.Point(188, 126);
            this.txt_d2.Name = "txt_d2";
            this.txt_d2.Size = new System.Drawing.Size(77, 22);
            this.txt_d2.TabIndex = 4;
            this.txt_d2.Text = "0.42";
            this.txt_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(18, 130);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(149, 13);
            this.label126.TabIndex = 8;
            this.label126.Text = "Thickness of Bottom Slab [d2]";
            // 
            // txt_d1
            // 
            this.txt_d1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d1.Location = new System.Drawing.Point(188, 100);
            this.txt_d1.Name = "txt_d1";
            this.txt_d1.Size = new System.Drawing.Size(77, 22);
            this.txt_d1.TabIndex = 3;
            this.txt_d1.Text = "0.42";
            this.txt_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(18, 104);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(135, 13);
            this.label127.TabIndex = 6;
            this.label127.Text = "Thickness of Top Slab [d1]";
            // 
            // txt_d
            // 
            this.txt_d.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d.Location = new System.Drawing.Point(188, 74);
            this.txt_d.Name = "txt_d";
            this.txt_d.Size = new System.Drawing.Size(77, 22);
            this.txt_d.TabIndex = 2;
            this.txt_d.Text = "3.0";
            this.txt_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(18, 78);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(106, 13);
            this.label128.TabIndex = 4;
            this.label128.Text = "Inside Clear Depth[d]";
            // 
            // txt_b
            // 
            this.txt_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_b.Location = new System.Drawing.Point(188, 48);
            this.txt_b.Name = "txt_b";
            this.txt_b.Size = new System.Drawing.Size(77, 22);
            this.txt_b.TabIndex = 1;
            this.txt_b.Text = "3.0";
            this.txt_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(18, 52);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(108, 13);
            this.label129.TabIndex = 2;
            this.label129.Text = "Inside Clear Width [b]";
            // 
            // txt_H
            // 
            this.txt_H.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_H.Location = new System.Drawing.Point(188, 22);
            this.txt_H.Name = "txt_H";
            this.txt_H.Size = new System.Drawing.Size(77, 22);
            this.txt_H.TabIndex = 0;
            this.txt_H.Text = "5.0";
            this.txt_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(18, 26);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(136, 13);
            this.label130.TabIndex = 0;
            this.label130.Text = "Height of Earth Cushion [H]";
            // 
            // tab_Drawings
            // 
            this.tab_Drawings.Controls.Add(this.groupBox9);
            this.tab_Drawings.Controls.Add(this.groupBox8);
            this.tab_Drawings.Location = new System.Drawing.Point(4, 22);
            this.tab_Drawings.Name = "tab_Drawings";
            this.tab_Drawings.Size = new System.Drawing.Size(876, 685);
            this.tab_Drawings.TabIndex = 2;
            this.tab_Drawings.Text = "Drawings";
            this.tab_Drawings.UseVisualStyleBackColor = true;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label132.Location = new System.Drawing.Point(16, 125);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(394, 16);
            this.label132.TabIndex = 9;
            this.label132.Text = "Button is Enabled Once the RCC Abutment Design is done.";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(19, 28);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(388, 16);
            this.label131.TabIndex = 8;
            this.label131.Text = "Button is Enabled Once the Top RCC Slab Design is done.";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.listBox1);
            this.groupBox8.Controls.Add(this.btn_dwg_pedestrian);
            this.groupBox8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(181, 310);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(410, 146);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Pedestrian Underpass Standard Drawings";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Single Cell Box Culvert",
            "Double Cell Box Culvert",
            "Tripple Cell Box Culvert"});
            this.listBox1.Location = new System.Drawing.Point(77, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(279, 56);
            this.listBox1.TabIndex = 1;
            // 
            // btn_dwg_pedestrian
            // 
            this.btn_dwg_pedestrian.Location = new System.Drawing.Point(77, 82);
            this.btn_dwg_pedestrian.Name = "btn_dwg_pedestrian";
            this.btn_dwg_pedestrian.Size = new System.Drawing.Size(279, 52);
            this.btn_dwg_pedestrian.TabIndex = 2;
            this.btn_dwg_pedestrian.Text = "Pedestrian Underpass Standard Drawings";
            this.btn_dwg_pedestrian.UseVisualStyleBackColor = true;
            this.btn_dwg_pedestrian.AutoSizeChanged += new System.EventHandler(this.btn_dwg_slab_Click);
            this.btn_dwg_pedestrian.Click += new System.EventHandler(this.btn_dwg_slab_Click);
            // 
            // btn_dwg_abut
            // 
            this.btn_dwg_abut.Location = new System.Drawing.Point(77, 153);
            this.btn_dwg_abut.Name = "btn_dwg_abut";
            this.btn_dwg_abut.Size = new System.Drawing.Size(279, 52);
            this.btn_dwg_abut.TabIndex = 1;
            this.btn_dwg_abut.Text = "RCC Abutment Drawing";
            this.btn_dwg_abut.UseVisualStyleBackColor = true;
            this.btn_dwg_abut.AutoSizeChanged += new System.EventHandler(this.btn_dwg_slab_Click);
            this.btn_dwg_abut.Click += new System.EventHandler(this.btn_dwg_slab_Click);
            // 
            // btn_dwg_slab
            // 
            this.btn_dwg_slab.Location = new System.Drawing.Point(77, 56);
            this.btn_dwg_slab.Name = "btn_dwg_slab";
            this.btn_dwg_slab.Size = new System.Drawing.Size(279, 52);
            this.btn_dwg_slab.TabIndex = 0;
            this.btn_dwg_slab.Text = "Top RCC Slab Drawing";
            this.btn_dwg_slab.UseVisualStyleBackColor = true;
            this.btn_dwg_slab.Click += new System.EventHandler(this.btn_dwg_slab_Click);
            // 
            // fbd
            // 
            this.fbd.Description = "Select ASTRA Design";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label132);
            this.groupBox9.Controls.Add(this.label131);
            this.groupBox9.Controls.Add(this.btn_dwg_abut);
            this.groupBox9.Controls.Add(this.btn_dwg_slab);
            this.groupBox9.Location = new System.Drawing.Point(181, 61);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(407, 215);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Vehicular Underpass Drawings";
            // 
            // frm_Underpass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 711);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Underpass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Underpass";
            this.Load += new System.EventHandler(this.frm_Underpass_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_Vehicular.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tab_Top_RCC_Slab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_veh_top_slab)).EndInit();
            this.tab_RCC_Abutment.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_veh_rcc_abut)).EndInit();
            this.tab_Pedestrian.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pedestrian)).EndInit();
            this.tab_Drawings.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Vehicular;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tab_Top_RCC_Slab;
        private System.Windows.Forms.TabPage tab_RCC_Abutment;
        private System.Windows.Forms.TabPage tab_Pedestrian;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txt_abut_h1;
        private System.Windows.Forms.TextBox txt_abut_L;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label lblCover;
        private System.Windows.Forms.TextBox txt_abut_cover;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_abut_fact;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txt_abut_F;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txt_abut_mu;
        private System.Windows.Forms.TextBox txt_abut_gamma_b;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_abut_gamma_c;
        private System.Windows.Forms.TextBox txt_abut_z;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_abut_delta;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_abut_theta;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_abut_w5;
        private System.Windows.Forms.TextBox txt_abut_phi;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_abut_w6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_abut_steel_grade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_abut_concrete_grade;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_abut_p_bearing_capacity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_abut_d4;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txt_abut_L4;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.PictureBox pic_veh_rcc_abut;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txt_abut_t;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txt_abut_d3;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txt_abut_L3;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_abut_L2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_abut_L1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_abut_d1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_abut_d2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_abut_B;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_abut_a;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_abut_H;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txt_top_slab_delta_wc;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_top_slab_delta_c;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txt_top_slab_cover;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txt_top_slab_W1;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txt_top_slab_b2;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox txt_top_slab_b1;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txt_top_slab_a1;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox txt_top_slab_Q;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox txt_top_slab_j;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.TextBox txt_top_slab_m;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txt_top_slab_sigma_st;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox txt_top_slab_sigma_cb;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.TextBox txt_top_slab_steel_grade;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox txt_top_slab_concrete_grade;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.TextBox txt_top_slab_width_support;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.TextBox txt_top_slab_WC;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txt_top_slab_L;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txt_top_slab_FP;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txt_top_slab_CW;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.TextBox txt_top_slab_D;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_top_slab_process;
        private System.Windows.Forms.Button btn_top_slab_report;
        private System.Windows.Forms.PictureBox pic_veh_top_slab;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txt_sigma_st;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_box_Report;
        private System.Windows.Forms.Button btn_box_Process;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.TextBox txt_sigma_c;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txt_b1;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.TextBox txt_b2;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txt_a1;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.TextBox txt_w1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_w2;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.TextBox txt_b3;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.TextBox txt_F;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.TextBox txt_S;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.TextBox txt_sbc;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.TextBox txt_cover;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.PictureBox pic_pedestrian;
        private System.Windows.Forms.TextBox txt_j;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.TextBox txt_t;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.TextBox txt_R;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.TextBox txt_gamma_c;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.TextBox txt_gamma_b;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.TextBox txt_d3;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.TextBox txt_d2;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.TextBox txt_d1;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.TextBox txt_d;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox txt_b;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.TextBox txt_H;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.TabPage tab_Drawings;
        private System.Windows.Forms.Button btn_dwg_pedestrian;
        private System.Windows.Forms.Button btn_dwg_abut;
        private System.Windows.Forms.Button btn_dwg_slab;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.GroupBox groupBox9;
    }
}