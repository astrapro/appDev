namespace BridgeAnalysisDesign.Abutment
{
    partial class frm_Abutment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Abutment));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_counter_fort_ret_wall = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.txt_cntf_m = new System.Windows.Forms.TextBox();
            this.txt_cntf_Q = new System.Windows.Forms.TextBox();
            this.label191 = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.cmb_cntf_fy = new System.Windows.Forms.ComboBox();
            this.label193 = new System.Windows.Forms.Label();
            this.cmb_cntf_fck = new System.Windows.Forms.ComboBox();
            this.txt_cntf_j = new System.Windows.Forms.TextBox();
            this.label194 = new System.Windows.Forms.Label();
            this.label203 = new System.Windows.Forms.Label();
            this.label204 = new System.Windows.Forms.Label();
            this.label205 = new System.Windows.Forms.Label();
            this.txt_cntf_sigma_st = new System.Windows.Forms.TextBox();
            this.label206 = new System.Windows.Forms.Label();
            this.label207 = new System.Windows.Forms.Label();
            this.txt_cntf_sigma_c = new System.Windows.Forms.TextBox();
            this.label209 = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_counterfort_th3 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_counterfort_l = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_counterfort_mu = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_counterfort_Kc = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_counterfort_Rc = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_counterfort_phi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_counterfort_P = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_counterfort_gama_c = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_counterfort_gama_s = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_counterfort_q0 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_counterfort_th2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_counterfort_th1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_counterfort_h2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_counterfort_h1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_cfort_open_des = new System.Windows.Forms.Button();
            this.btn_counterfort_Process = new System.Windows.Forms.Button();
            this.btn_counterfort_Report = new System.Windows.Forms.Button();
            this.pic_counter_fort_drawing5 = new System.Windows.Forms.PictureBox();
            this.pic_counter_fort_drawing6 = new System.Windows.Forms.PictureBox();
            this.pic_counter_fort_drawing3 = new System.Windows.Forms.PictureBox();
            this.pic_counter_fort_drawing4 = new System.Windows.Forms.PictureBox();
            this.pic_counter_fort_drawing2 = new System.Windows.Forms.PictureBox();
            this.pic_counter_fort_drawing1 = new System.Windows.Forms.PictureBox();
            this.tab_cantilever_ret_wall = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.label214 = new System.Windows.Forms.Label();
            this.cmb_cant_fy = new System.Windows.Forms.ComboBox();
            this.label216 = new System.Windows.Forms.Label();
            this.cmb_cant_fck = new System.Windows.Forms.ComboBox();
            this.label221 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.txt_cant_sigma_st = new System.Windows.Forms.TextBox();
            this.label231 = new System.Windows.Forms.Label();
            this.label234 = new System.Windows.Forms.Label();
            this.txt_cant_sigma_c = new System.Windows.Forms.TextBox();
            this.label235 = new System.Windows.Forms.Label();
            this.btn_cnt_Report = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.btn_cnt_Process = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_cant_open_des = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.txt_cnt_h1 = new System.Windows.Forms.TextBox();
            this.txt_cnt_sc = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.lblCover = new System.Windows.Forms.Label();
            this.txt_cnt_cover = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txt_cnt_fact = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txt_cnt_F = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.txt_cnt_mu = new System.Windows.Forms.TextBox();
            this.txt_cnt_gamma_b = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txt_cnt_gamma_c = new System.Windows.Forms.TextBox();
            this.txt_cnt_z = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.txt_cnt_delta = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.txt_cnt_theta = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.txt_cnt_w5 = new System.Windows.Forms.TextBox();
            this.txt_cnt_phi = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.txt_cnt_w6 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.txt_cnt_p_bearing_capacity = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_cnt_d4 = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.txt_cnt_L4 = new System.Windows.Forms.TextBox();
            this.txt_cnt_L = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.pic_cantilever = new System.Windows.Forms.PictureBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txt_cnt_t = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.txt_cnt_d3 = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.txt_cnt_L3 = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.txt_cnt_L2 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txt_cnt_L1 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_cnt_d1 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txt_cnt_d2 = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.txt_cnt_B = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.txt_cnt_a = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txt_cnt_H = new System.Windows.Forms.TextBox();
            this.label210 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.btn_dwg_cantilever = new System.Windows.Forms.Button();
            this.btn_dwg_counterfort = new System.Windows.Forms.Button();
            this.btn_dwg_worksheet2 = new System.Windows.Forms.Button();
            this.btn_dwg_worksheet1 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uC_Abut_Counterfort_LS1 = new BridgeAnalysisDesign.Abutment.UC_Abut_Counterfort_LS();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uC_Abut_Cant1 = new BridgeAnalysisDesign.Abutment.UC_Abut_Cant();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_dwg_open_Cantilever = new System.Windows.Forms.Button();
            this.btn_dwg_open_Counterfort = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tab_counter_fort_ret_wall.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing1)).BeginInit();
            this.tab_cantilever_ret_wall.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cantilever)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_counter_fort_ret_wall);
            this.tabControl1.Controls.Add(this.tab_cantilever_ret_wall);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(940, 665);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_counter_fort_ret_wall
            // 
            this.tab_counter_fort_ret_wall.Controls.Add(this.groupBox1);
            this.tab_counter_fort_ret_wall.Controls.Add(this.panel1);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing5);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing6);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing3);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing4);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing2);
            this.tab_counter_fort_ret_wall.Controls.Add(this.pic_counter_fort_drawing1);
            this.tab_counter_fort_ret_wall.Location = new System.Drawing.Point(4, 22);
            this.tab_counter_fort_ret_wall.Name = "tab_counter_fort_ret_wall";
            this.tab_counter_fort_ret_wall.Padding = new System.Windows.Forms.Padding(3);
            this.tab_counter_fort_ret_wall.Size = new System.Drawing.Size(932, 639);
            this.tab_counter_fort_ret_wall.TabIndex = 0;
            this.tab_counter_fort_ret_wall.Text = "As Counterfort Retaining Wall";
            this.tab_counter_fort_ret_wall.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.label106);
            this.groupBox1.Controls.Add(this.label108);
            this.groupBox1.Controls.Add(this.groupBox39);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.txt_counterfort_th3);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.txt_counterfort_l);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_counterfort_mu);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_counterfort_Kc);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txt_counterfort_Rc);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txt_counterfort_phi);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_counterfort_P);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_counterfort_gama_c);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_counterfort_gama_s);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_counterfort_q0);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_counterfort_th2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_counterfort_th1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_counterfort_h2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_counterfort_h1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(-1, -10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 601);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label106.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.ForeColor = System.Drawing.Color.Red;
            this.label106.Location = new System.Drawing.Point(203, 10);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(218, 18);
            this.label106.TabIndex = 134;
            this.label106.Text = "Default Sample Data are shown";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label108.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.Green;
            this.label108.Location = new System.Drawing.Point(51, 10);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(135, 18);
            this.label108.TabIndex = 133;
            this.label108.Text = "All User Input Data";
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.txt_cntf_m);
            this.groupBox39.Controls.Add(this.txt_cntf_Q);
            this.groupBox39.Controls.Add(this.label191);
            this.groupBox39.Controls.Add(this.label192);
            this.groupBox39.Controls.Add(this.cmb_cntf_fy);
            this.groupBox39.Controls.Add(this.label193);
            this.groupBox39.Controls.Add(this.cmb_cntf_fck);
            this.groupBox39.Controls.Add(this.txt_cntf_j);
            this.groupBox39.Controls.Add(this.label194);
            this.groupBox39.Controls.Add(this.label203);
            this.groupBox39.Controls.Add(this.label204);
            this.groupBox39.Controls.Add(this.label205);
            this.groupBox39.Controls.Add(this.txt_cntf_sigma_st);
            this.groupBox39.Controls.Add(this.label206);
            this.groupBox39.Controls.Add(this.label207);
            this.groupBox39.Controls.Add(this.txt_cntf_sigma_c);
            this.groupBox39.Controls.Add(this.label209);
            this.groupBox39.Controls.Add(this.label213);
            this.groupBox39.Location = new System.Drawing.Point(7, 321);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(446, 195);
            this.groupBox39.TabIndex = 114;
            this.groupBox39.TabStop = false;
            // 
            // txt_cntf_m
            // 
            this.txt_cntf_m.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cntf_m.Location = new System.Drawing.Point(277, 116);
            this.txt_cntf_m.Name = "txt_cntf_m";
            this.txt_cntf_m.Size = new System.Drawing.Size(65, 22);
            this.txt_cntf_m.TabIndex = 88;
            this.txt_cntf_m.Text = "10";
            this.txt_cntf_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cntf_Q
            // 
            this.txt_cntf_Q.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cntf_Q.ForeColor = System.Drawing.Color.Blue;
            this.txt_cntf_Q.Location = new System.Drawing.Point(277, 169);
            this.txt_cntf_Q.Name = "txt_cntf_Q";
            this.txt_cntf_Q.Size = new System.Drawing.Size(65, 22);
            this.txt_cntf_Q.TabIndex = 91;
            this.txt_cntf_Q.Text = "0.762";
            this.txt_cntf_Q.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label191.Location = new System.Drawing.Point(348, 42);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(67, 13);
            this.label191.TabIndex = 81;
            this.label191.Text = "N/sq.mm";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label192.Location = new System.Drawing.Point(6, 171);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(104, 15);
            this.label192.TabIndex = 95;
            this.label192.Text = "Moment factor [Q]";
            // 
            // cmb_cntf_fy
            // 
            this.cmb_cntf_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cntf_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_cntf_fy.FormattingEnabled = true;
            this.cmb_cntf_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_cntf_fy.Location = new System.Drawing.Point(281, 66);
            this.cmb_cntf_fy.Name = "cmb_cntf_fy";
            this.cmb_cntf_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_cntf_fy.TabIndex = 15;
            this.cmb_cntf_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Location = new System.Drawing.Point(5, 42);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(211, 13);
            this.label193.TabIndex = 79;
            this.label193.Text = "Allowable Flexural Stress in Concrete [σ_c] ";
            // 
            // cmb_cntf_fck
            // 
            this.cmb_cntf_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cntf_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_cntf_fck.FormattingEnabled = true;
            this.cmb_cntf_fck.Items.AddRange(new object[] {
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
            this.cmb_cntf_fck.Location = new System.Drawing.Point(281, 11);
            this.cmb_cntf_fck.Name = "cmb_cntf_fck";
            this.cmb_cntf_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_cntf_fck.TabIndex = 13;
            this.cmb_cntf_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
            // 
            // txt_cntf_j
            // 
            this.txt_cntf_j.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cntf_j.ForeColor = System.Drawing.Color.Blue;
            this.txt_cntf_j.Location = new System.Drawing.Point(277, 145);
            this.txt_cntf_j.Name = "txt_cntf_j";
            this.txt_cntf_j.Size = new System.Drawing.Size(65, 22);
            this.txt_cntf_j.TabIndex = 89;
            this.txt_cntf_j.Text = "0.91";
            this.txt_cntf_j.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label194.Location = new System.Drawing.Point(5, 147);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(107, 15);
            this.label194.TabIndex = 92;
            this.label194.Text = "Lever arm factor [j]";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label203.Location = new System.Drawing.Point(257, 68);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(23, 14);
            this.label203.TabIndex = 59;
            this.label203.Text = "Fe";
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label204.Location = new System.Drawing.Point(262, 13);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(18, 14);
            this.label204.TabIndex = 58;
            this.label204.Text = "M";
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label205.Location = new System.Drawing.Point(348, 99);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(67, 13);
            this.label205.TabIndex = 57;
            this.label205.Text = "N/sq.mm";
            // 
            // txt_cntf_sigma_st
            // 
            this.txt_cntf_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_cntf_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cntf_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_cntf_sigma_st.Location = new System.Drawing.Point(277, 93);
            this.txt_cntf_sigma_st.Name = "txt_cntf_sigma_st";
            this.txt_cntf_sigma_st.ReadOnly = true;
            this.txt_cntf_sigma_st.Size = new System.Drawing.Size(65, 22);
            this.txt_cntf_sigma_st.TabIndex = 16;
            this.txt_cntf_sigma_st.Text = "200";
            this.txt_cntf_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label206.Location = new System.Drawing.Point(5, 99);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(159, 13);
            this.label206.TabIndex = 55;
            this.label206.Text = "Permissible Stress in Steel [σ_st]";
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(5, 74);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(80, 13);
            this.label207.TabIndex = 15;
            this.label207.Text = "Steel Grade [fy]";
            // 
            // txt_cntf_sigma_c
            // 
            this.txt_cntf_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_cntf_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cntf_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_cntf_sigma_c.Location = new System.Drawing.Point(277, 38);
            this.txt_cntf_sigma_c.Name = "txt_cntf_sigma_c";
            this.txt_cntf_sigma_c.ReadOnly = true;
            this.txt_cntf_sigma_c.Size = new System.Drawing.Size(65, 22);
            this.txt_cntf_sigma_c.TabIndex = 14;
            this.txt_cntf_sigma_c.Text = "83.3";
            this.txt_cntf_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label209
            // 
            this.label209.AutoSize = true;
            this.label209.Location = new System.Drawing.Point(5, 19);
            this.label209.Name = "label209";
            this.label209.Size = new System.Drawing.Size(106, 13);
            this.label209.TabIndex = 13;
            this.label209.Text = "Concrete Grade [fck]";
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.Location = new System.Drawing.Point(5, 121);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(90, 13);
            this.label213.TabIndex = 41;
            this.label213.Text = "Modular Ratio [m]";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(350, 578);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(19, 14);
            this.label35.TabIndex = 47;
            this.label35.Text = "m";
            this.label35.Visible = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(354, 300);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(19, 14);
            this.label34.TabIndex = 46;
            this.label34.Text = "m";
            // 
            // txt_counterfort_th3
            // 
            this.txt_counterfort_th3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_th3.Location = new System.Drawing.Point(256, 295);
            this.txt_counterfort_th3.Name = "txt_counterfort_th3";
            this.txt_counterfort_th3.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_th3.TabIndex = 45;
            this.txt_counterfort_th3.Text = "0.5";
            this.txt_counterfort_th3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(7, 297);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(200, 16);
            this.label33.TabIndex = 44;
            this.label33.Text = "Thickness of Counterfot wall [th3]";
            // 
            // txt_counterfort_l
            // 
            this.txt_counterfort_l.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_l.Location = new System.Drawing.Point(256, 574);
            this.txt_counterfort_l.Name = "txt_counterfort_l";
            this.txt_counterfort_l.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_l.TabIndex = 43;
            this.txt_counterfort_l.Text = "3.0";
            this.txt_counterfort_l.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_counterfort_l.Visible = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(7, 564);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(143, 32);
            this.label32.TabIndex = 42;
            this.label32.Text = "Clear spacing between \r\nCounterfort walls [l]";
            this.label32.Visible = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(354, 213);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(45, 14);
            this.label27.TabIndex = 40;
            this.label27.Text = "kN/m";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(354, 187);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 14);
            this.label26.TabIndex = 40;
            this.label26.Text = "kN/cu.m";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(354, 162);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(64, 14);
            this.label25.TabIndex = 40;
            this.label25.Text = "kN/cu.m";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(354, 134);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(64, 14);
            this.label24.TabIndex = 40;
            this.label24.Text = "kN/sq.m";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(354, 108);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(19, 14);
            this.label23.TabIndex = 39;
            this.label23.Text = "m";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(354, 82);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 14);
            this.label22.TabIndex = 38;
            this.label22.Text = "m";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(354, 239);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 14);
            this.label21.TabIndex = 37;
            this.label21.Text = "deg (°)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(354, 60);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 14);
            this.label20.TabIndex = 36;
            this.label20.Text = "m";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(354, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 14);
            this.label19.TabIndex = 36;
            this.label19.Text = "m";
            // 
            // txt_counterfort_mu
            // 
            this.txt_counterfort_mu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_mu.Location = new System.Drawing.Point(256, 269);
            this.txt_counterfort_mu.Name = "txt_counterfort_mu";
            this.txt_counterfort_mu.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_mu.TabIndex = 35;
            this.txt_counterfort_mu.Tag = "";
            this.txt_counterfort_mu.Text = "0.5";
            this.txt_counterfort_mu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 32);
            this.label10.TabIndex = 34;
            this.label10.Text = "Coefficient of friction \r\nbetween Concrete and Soil [µ]";
            // 
            // txt_counterfort_Kc
            // 
            this.txt_counterfort_Kc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_Kc.Location = new System.Drawing.Point(256, 519);
            this.txt_counterfort_Kc.Name = "txt_counterfort_Kc";
            this.txt_counterfort_Kc.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_Kc.TabIndex = 23;
            this.txt_counterfort_Kc.Text = "0.289";
            this.txt_counterfort_Kc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(7, 524);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 16);
            this.label16.TabIndex = 22;
            this.label16.Text = "Kc";
            // 
            // txt_counterfort_Rc
            // 
            this.txt_counterfort_Rc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_Rc.Location = new System.Drawing.Point(256, 543);
            this.txt_counterfort_Rc.Name = "txt_counterfort_Rc";
            this.txt_counterfort_Rc.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_Rc.TabIndex = 19;
            this.txt_counterfort_Rc.Text = "0.914";
            this.txt_counterfort_Rc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(7, 545);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 16);
            this.label18.TabIndex = 18;
            this.label18.Text = "Rc";
            // 
            // txt_counterfort_phi
            // 
            this.txt_counterfort_phi.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_phi.Location = new System.Drawing.Point(256, 236);
            this.txt_counterfort_phi.Name = "txt_counterfort_phi";
            this.txt_counterfort_phi.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_phi.TabIndex = 17;
            this.txt_counterfort_phi.Text = "30";
            this.txt_counterfort_phi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Angle of internal friction of soil [φ]";
            // 
            // txt_counterfort_P
            // 
            this.txt_counterfort_P.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_P.Location = new System.Drawing.Point(256, 210);
            this.txt_counterfort_P.Name = "txt_counterfort_P";
            this.txt_counterfort_P.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_P.TabIndex = 15;
            this.txt_counterfort_P.Text = "18";
            this.txt_counterfort_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Surcharge [P]";
            // 
            // txt_counterfort_gama_c
            // 
            this.txt_counterfort_gama_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_gama_c.Location = new System.Drawing.Point(256, 184);
            this.txt_counterfort_gama_c.Name = "txt_counterfort_gama_c";
            this.txt_counterfort_gama_c.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_gama_c.TabIndex = 13;
            this.txt_counterfort_gama_c.Text = "25";
            this.txt_counterfort_gama_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(175, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Unit weight of Concrete [γ_c]";
            // 
            // txt_counterfort_gama_s
            // 
            this.txt_counterfort_gama_s.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_gama_s.Location = new System.Drawing.Point(256, 158);
            this.txt_counterfort_gama_s.Name = "txt_counterfort_gama_s";
            this.txt_counterfort_gama_s.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_gama_s.TabIndex = 11;
            this.txt_counterfort_gama_s.Text = "18";
            this.txt_counterfort_gama_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Unit weight of Soil [γ_s]";
            // 
            // txt_counterfort_q0
            // 
            this.txt_counterfort_q0.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_q0.Location = new System.Drawing.Point(256, 131);
            this.txt_counterfort_q0.Name = "txt_counterfort_q0";
            this.txt_counterfort_q0.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_q0.TabIndex = 9;
            this.txt_counterfort_q0.Text = "180";
            this.txt_counterfort_q0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Safe bearing capacity of soil [q0]";
            // 
            // txt_counterfort_th2
            // 
            this.txt_counterfort_th2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_th2.Location = new System.Drawing.Point(256, 105);
            this.txt_counterfort_th2.Name = "txt_counterfort_th2";
            this.txt_counterfort_th2.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_th2.TabIndex = 7;
            this.txt_counterfort_th2.Text = "0.3";
            this.txt_counterfort_th2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Thickness of base Slab [th2]";
            // 
            // txt_counterfort_th1
            // 
            this.txt_counterfort_th1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_th1.Location = new System.Drawing.Point(256, 80);
            this.txt_counterfort_th1.Name = "txt_counterfort_th1";
            this.txt_counterfort_th1.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_th1.TabIndex = 5;
            this.txt_counterfort_th1.Text = "0.3";
            this.txt_counterfort_th1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Thickness of stem of Wall [th1]";
            // 
            // txt_counterfort_h2
            // 
            this.txt_counterfort_h2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_h2.Location = new System.Drawing.Point(256, 56);
            this.txt_counterfort_h2.Name = "txt_counterfort_h2";
            this.txt_counterfort_h2.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_h2.TabIndex = 3;
            this.txt_counterfort_h2.Text = "1.0";
            this.txt_counterfort_h2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Foundation depth below ground Level [h2]";
            // 
            // txt_counterfort_h1
            // 
            this.txt_counterfort_h1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counterfort_h1.Location = new System.Drawing.Point(256, 31);
            this.txt_counterfort_h1.Name = "txt_counterfort_h1";
            this.txt_counterfort_h1.Size = new System.Drawing.Size(92, 22);
            this.txt_counterfort_h1.TabIndex = 1;
            this.txt_counterfort_h1.Text = "7.0";
            this.txt_counterfort_h1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Height of Wall above ground Level [h1]";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_cfort_open_des);
            this.panel1.Controls.Add(this.btn_counterfort_Process);
            this.panel1.Controls.Add(this.btn_counterfort_Report);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 39);
            this.panel1.TabIndex = 47;
            // 
            // btn_cfort_open_des
            // 
            this.btn_cfort_open_des.Location = new System.Drawing.Point(40, 6);
            this.btn_cfort_open_des.Name = "btn_cfort_open_des";
            this.btn_cfort_open_des.Size = new System.Drawing.Size(309, 28);
            this.btn_cfort_open_des.TabIndex = 120;
            this.btn_cfort_open_des.Text = "Open Previous Design [\"ASTRA_Data_Input.txt\"]";
            this.btn_cfort_open_des.UseVisualStyleBackColor = true;
            this.btn_cfort_open_des.Visible = false;
            this.btn_cfort_open_des.Click += new System.EventHandler(this.btn_cant_open_des_Click);
            // 
            // btn_counterfort_Process
            // 
            this.btn_counterfort_Process.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_counterfort_Process.Location = new System.Drawing.Point(390, 6);
            this.btn_counterfort_Process.Name = "btn_counterfort_Process";
            this.btn_counterfort_Process.Size = new System.Drawing.Size(117, 28);
            this.btn_counterfort_Process.TabIndex = 0;
            this.btn_counterfort_Process.Text = "Process";
            this.btn_counterfort_Process.UseVisualStyleBackColor = true;
            this.btn_counterfort_Process.Click += new System.EventHandler(this.btn_Counter_Fort_Process_Click);
            // 
            // btn_counterfort_Report
            // 
            this.btn_counterfort_Report.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_counterfort_Report.Location = new System.Drawing.Point(524, 6);
            this.btn_counterfort_Report.Name = "btn_counterfort_Report";
            this.btn_counterfort_Report.Size = new System.Drawing.Size(117, 28);
            this.btn_counterfort_Report.TabIndex = 1;
            this.btn_counterfort_Report.Text = "Report";
            this.btn_counterfort_Report.UseVisualStyleBackColor = true;
            this.btn_counterfort_Report.Click += new System.EventHandler(this.btn_Counter_Fort_Report_Click);
            // 
            // pic_counter_fort_drawing5
            // 
            this.pic_counter_fort_drawing5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing5.Location = new System.Drawing.Point(696, 397);
            this.pic_counter_fort_drawing5.Name = "pic_counter_fort_drawing5";
            this.pic_counter_fort_drawing5.Size = new System.Drawing.Size(222, 194);
            this.pic_counter_fort_drawing5.TabIndex = 4;
            this.pic_counter_fort_drawing5.TabStop = false;
            // 
            // pic_counter_fort_drawing6
            // 
            this.pic_counter_fort_drawing6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing6.Location = new System.Drawing.Point(485, 397);
            this.pic_counter_fort_drawing6.Name = "pic_counter_fort_drawing6";
            this.pic_counter_fort_drawing6.Size = new System.Drawing.Size(205, 194);
            this.pic_counter_fort_drawing6.TabIndex = 5;
            this.pic_counter_fort_drawing6.TabStop = false;
            // 
            // pic_counter_fort_drawing3
            // 
            this.pic_counter_fort_drawing3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing3.Location = new System.Drawing.Point(485, 194);
            this.pic_counter_fort_drawing3.Name = "pic_counter_fort_drawing3";
            this.pic_counter_fort_drawing3.Size = new System.Drawing.Size(205, 199);
            this.pic_counter_fort_drawing3.TabIndex = 2;
            this.pic_counter_fort_drawing3.TabStop = false;
            // 
            // pic_counter_fort_drawing4
            // 
            this.pic_counter_fort_drawing4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing4.Location = new System.Drawing.Point(696, 194);
            this.pic_counter_fort_drawing4.Name = "pic_counter_fort_drawing4";
            this.pic_counter_fort_drawing4.Size = new System.Drawing.Size(222, 199);
            this.pic_counter_fort_drawing4.TabIndex = 3;
            this.pic_counter_fort_drawing4.TabStop = false;
            // 
            // pic_counter_fort_drawing2
            // 
            this.pic_counter_fort_drawing2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing2.Location = new System.Drawing.Point(696, 6);
            this.pic_counter_fort_drawing2.Name = "pic_counter_fort_drawing2";
            this.pic_counter_fort_drawing2.Size = new System.Drawing.Size(222, 182);
            this.pic_counter_fort_drawing2.TabIndex = 1;
            this.pic_counter_fort_drawing2.TabStop = false;
            // 
            // pic_counter_fort_drawing1
            // 
            this.pic_counter_fort_drawing1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_counter_fort_drawing1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_counter_fort_drawing1.Location = new System.Drawing.Point(485, 6);
            this.pic_counter_fort_drawing1.Name = "pic_counter_fort_drawing1";
            this.pic_counter_fort_drawing1.Size = new System.Drawing.Size(205, 182);
            this.pic_counter_fort_drawing1.TabIndex = 0;
            this.pic_counter_fort_drawing1.TabStop = false;
            // 
            // tab_cantilever_ret_wall
            // 
            this.tab_cantilever_ret_wall.Controls.Add(this.groupBox2);
            this.tab_cantilever_ret_wall.Controls.Add(this.groupBox3);
            this.tab_cantilever_ret_wall.Location = new System.Drawing.Point(4, 22);
            this.tab_cantilever_ret_wall.Name = "tab_cantilever_ret_wall";
            this.tab_cantilever_ret_wall.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cantilever_ret_wall.Size = new System.Drawing.Size(932, 639);
            this.tab_cantilever_ret_wall.TabIndex = 1;
            this.tab_cantilever_ret_wall.Text = "As Cantilever Retaining Wall";
            this.tab_cantilever_ret_wall.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox41);
            this.groupBox2.Controls.Add(this.btn_cnt_Report);
            this.groupBox2.Controls.Add(this.label50);
            this.groupBox2.Controls.Add(this.btn_cnt_Process);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label51);
            this.groupBox2.Controls.Add(this.txt_cnt_h1);
            this.groupBox2.Controls.Add(this.txt_cnt_sc);
            this.groupBox2.Controls.Add(this.label93);
            this.groupBox2.Controls.Add(this.label48);
            this.groupBox2.Controls.Add(this.lblCover);
            this.groupBox2.Controls.Add(this.txt_cnt_cover);
            this.groupBox2.Controls.Add(this.label49);
            this.groupBox2.Controls.Add(this.txt_cnt_fact);
            this.groupBox2.Controls.Add(this.label44);
            this.groupBox2.Controls.Add(this.label45);
            this.groupBox2.Controls.Add(this.txt_cnt_F);
            this.groupBox2.Controls.Add(this.label42);
            this.groupBox2.Controls.Add(this.label43);
            this.groupBox2.Controls.Add(this.label46);
            this.groupBox2.Controls.Add(this.txt_cnt_mu);
            this.groupBox2.Controls.Add(this.txt_cnt_gamma_b);
            this.groupBox2.Controls.Add(this.label55);
            this.groupBox2.Controls.Add(this.label56);
            this.groupBox2.Controls.Add(this.txt_cnt_gamma_c);
            this.groupBox2.Controls.Add(this.txt_cnt_z);
            this.groupBox2.Controls.Add(this.label58);
            this.groupBox2.Controls.Add(this.label59);
            this.groupBox2.Controls.Add(this.label61);
            this.groupBox2.Controls.Add(this.label62);
            this.groupBox2.Controls.Add(this.txt_cnt_delta);
            this.groupBox2.Controls.Add(this.label65);
            this.groupBox2.Controls.Add(this.label67);
            this.groupBox2.Controls.Add(this.txt_cnt_theta);
            this.groupBox2.Controls.Add(this.label68);
            this.groupBox2.Controls.Add(this.label66);
            this.groupBox2.Controls.Add(this.label69);
            this.groupBox2.Controls.Add(this.txt_cnt_w5);
            this.groupBox2.Controls.Add(this.txt_cnt_phi);
            this.groupBox2.Controls.Add(this.label70);
            this.groupBox2.Controls.Add(this.label72);
            this.groupBox2.Controls.Add(this.label74);
            this.groupBox2.Controls.Add(this.txt_cnt_w6);
            this.groupBox2.Controls.Add(this.label76);
            this.groupBox2.Controls.Add(this.label82);
            this.groupBox2.Controls.Add(this.txt_cnt_p_bearing_capacity);
            this.groupBox2.Location = new System.Drawing.Point(30, 335);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(865, 312);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.label214);
            this.groupBox41.Controls.Add(this.cmb_cant_fy);
            this.groupBox41.Controls.Add(this.label216);
            this.groupBox41.Controls.Add(this.cmb_cant_fck);
            this.groupBox41.Controls.Add(this.label221);
            this.groupBox41.Controls.Add(this.label227);
            this.groupBox41.Controls.Add(this.label229);
            this.groupBox41.Controls.Add(this.txt_cant_sigma_st);
            this.groupBox41.Controls.Add(this.label231);
            this.groupBox41.Controls.Add(this.label234);
            this.groupBox41.Controls.Add(this.txt_cant_sigma_c);
            this.groupBox41.Controls.Add(this.label235);
            this.groupBox41.Location = new System.Drawing.Point(449, 62);
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
            // cmb_cant_fy
            // 
            this.cmb_cant_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cant_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_cant_fy.FormattingEnabled = true;
            this.cmb_cant_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_cant_fy.Location = new System.Drawing.Point(272, 65);
            this.cmb_cant_fy.Name = "cmb_cant_fy";
            this.cmb_cant_fy.Size = new System.Drawing.Size(61, 21);
            this.cmb_cant_fy.TabIndex = 15;
            this.cmb_cant_fy.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
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
            // cmb_cant_fck
            // 
            this.cmb_cant_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cant_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_cant_fck.FormattingEnabled = true;
            this.cmb_cant_fck.Items.AddRange(new object[] {
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
            this.cmb_cant_fck.Location = new System.Drawing.Point(272, 16);
            this.cmb_cant_fck.Name = "cmb_cant_fck";
            this.cmb_cant_fck.Size = new System.Drawing.Size(61, 21);
            this.cmb_cant_fck.TabIndex = 13;
            this.cmb_cant_fck.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_steel_grade_SelectedIndexChanged);
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
            // txt_cant_sigma_st
            // 
            this.txt_cant_sigma_st.BackColor = System.Drawing.Color.White;
            this.txt_cant_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cant_sigma_st.ForeColor = System.Drawing.Color.Blue;
            this.txt_cant_sigma_st.Location = new System.Drawing.Point(239, 92);
            this.txt_cant_sigma_st.Name = "txt_cant_sigma_st";
            this.txt_cant_sigma_st.ReadOnly = true;
            this.txt_cant_sigma_st.Size = new System.Drawing.Size(94, 22);
            this.txt_cant_sigma_st.TabIndex = 16;
            this.txt_cant_sigma_st.Text = "200";
            this.txt_cant_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // txt_cant_sigma_c
            // 
            this.txt_cant_sigma_c.BackColor = System.Drawing.Color.White;
            this.txt_cant_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cant_sigma_c.ForeColor = System.Drawing.Color.Blue;
            this.txt_cant_sigma_c.Location = new System.Drawing.Point(239, 40);
            this.txt_cant_sigma_c.Name = "txt_cant_sigma_c";
            this.txt_cant_sigma_c.ReadOnly = true;
            this.txt_cant_sigma_c.Size = new System.Drawing.Size(94, 22);
            this.txt_cant_sigma_c.TabIndex = 14;
            this.txt_cant_sigma_c.Text = "83.3";
            this.txt_cant_sigma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // btn_cnt_Report
            // 
            this.btn_cnt_Report.Location = new System.Drawing.Point(450, 270);
            this.btn_cnt_Report.Name = "btn_cnt_Report";
            this.btn_cnt_Report.Size = new System.Drawing.Size(149, 31);
            this.btn_cnt_Report.TabIndex = 1;
            this.btn_cnt_Report.Text = "Report";
            this.btn_cnt_Report.UseVisualStyleBackColor = true;
            this.btn_cnt_Report.Click += new System.EventHandler(this.btn_Cantilever_Report_Click);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(352, 192);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(22, 16);
            this.label50.TabIndex = 73;
            this.label50.Text = "m";
            this.label50.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_cnt_Process
            // 
            this.btn_cnt_Process.Location = new System.Drawing.Point(295, 270);
            this.btn_cnt_Process.Name = "btn_cnt_Process";
            this.btn_cnt_Process.Size = new System.Drawing.Size(149, 31);
            this.btn_cnt_Process.TabIndex = 0;
            this.btn_cnt_Process.Text = "Process";
            this.btn_cnt_Process.UseVisualStyleBackColor = true;
            this.btn_cnt_Process.Click += new System.EventHandler(this.btn_Cantilever_Process_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_cant_open_des);
            this.groupBox4.Location = new System.Drawing.Point(748, 282);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(96, 23);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Visible = false;
            // 
            // btn_cant_open_des
            // 
            this.btn_cant_open_des.Location = new System.Drawing.Point(6, 14);
            this.btn_cant_open_des.Name = "btn_cant_open_des";
            this.btn_cant_open_des.Size = new System.Drawing.Size(78, 38);
            this.btn_cant_open_des.TabIndex = 121;
            this.btn_cant_open_des.Text = "Open Previous Design [\"ASTRA_Data_Input.txt\"]";
            this.btn_cant_open_des.UseVisualStyleBackColor = true;
            this.btn_cant_open_des.Visible = false;
            this.btn_cant_open_des.Click += new System.EventHandler(this.btn_cant_open_des_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(11, 196);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(200, 13);
            this.label51.TabIndex = 72;
            this.label51.Text = "Vehicle Break is applied at a height [ h1 ]";
            // 
            // txt_cnt_h1
            // 
            this.txt_cnt_h1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_h1.Location = new System.Drawing.Point(253, 190);
            this.txt_cnt_h1.Name = "txt_cnt_h1";
            this.txt_cnt_h1.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_h1.TabIndex = 7;
            this.txt_cnt_h1.Text = "1.2";
            this.txt_cnt_h1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cnt_sc
            // 
            this.txt_cnt_sc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_sc.Location = new System.Drawing.Point(253, 243);
            this.txt_cnt_sc.Name = "txt_cnt_sc";
            this.txt_cnt_sc.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_sc.TabIndex = 16;
            this.txt_cnt_sc.Text = "0.18";
            this.txt_cnt_sc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(11, 247);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(116, 13);
            this.label93.TabIndex = 68;
            this.label93.Text = "Seismic Coefficient [sc]";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(352, 167);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(36, 16);
            this.label48.TabIndex = 67;
            this.label48.Text = "mm";
            this.label48.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCover
            // 
            this.lblCover.AutoSize = true;
            this.lblCover.Location = new System.Drawing.Point(11, 169);
            this.lblCover.Name = "lblCover";
            this.lblCover.Size = new System.Drawing.Size(129, 13);
            this.lblCover.TabIndex = 66;
            this.lblCover.Text = "Reinf. Clear Cover [cover]";
            // 
            // txt_cnt_cover
            // 
            this.txt_cnt_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_cover.Location = new System.Drawing.Point(253, 165);
            this.txt_cnt_cover.Name = "txt_cnt_cover";
            this.txt_cnt_cover.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_cover.TabIndex = 6;
            this.txt_cnt_cover.Text = "50";
            this.txt_cnt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(11, 218);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(232, 13);
            this.label49.TabIndex = 64;
            this.label49.Text = "Bending Moment and Shear Force Factor [Fact]";
            // 
            // txt_cnt_fact
            // 
            this.txt_cnt_fact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_fact.Location = new System.Drawing.Point(253, 215);
            this.txt_cnt_fact.Name = "txt_cnt_fact";
            this.txt_cnt_fact.Size = new System.Drawing.Size(93, 22);
            this.txt_cnt_fact.TabIndex = 10;
            this.txt_cnt_fact.Text = "1.5";
            this.txt_cnt_fact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(788, 243);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(26, 16);
            this.label44.TabIndex = 62;
            this.label44.Text = "kN";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(453, 244);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(126, 13);
            this.label45.TabIndex = 61;
            this.label45.Text = "Vehicle Braking Force [F]";
            // 
            // txt_cnt_F
            // 
            this.txt_cnt_F.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_F.Location = new System.Drawing.Point(688, 241);
            this.txt_cnt_F.Name = "txt_cnt_F";
            this.txt_cnt_F.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_F.TabIndex = 13;
            this.txt_cnt_F.Text = "200";
            this.txt_cnt_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(350, 91);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(15, 16);
            this.label42.TabIndex = 59;
            this.label42.Text = "°";
            this.label42.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(351, 66);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(15, 16);
            this.label43.TabIndex = 58;
            this.label43.Text = "°";
            this.label43.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(11, 119);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(232, 13);
            this.label46.TabIndex = 57;
            this.label46.Text = "Coefficient of friction between Earth and wall [µ]";
            // 
            // txt_cnt_mu
            // 
            this.txt_cnt_mu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_mu.Location = new System.Drawing.Point(253, 115);
            this.txt_cnt_mu.Name = "txt_cnt_mu";
            this.txt_cnt_mu.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_mu.TabIndex = 4;
            this.txt_cnt_mu.Text = "0.6";
            this.txt_cnt_mu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cnt_gamma_b
            // 
            this.txt_cnt_gamma_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_gamma_b.Location = new System.Drawing.Point(687, 11);
            this.txt_cnt_gamma_b.Name = "txt_cnt_gamma_b";
            this.txt_cnt_gamma_b.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_gamma_b.TabIndex = 14;
            this.txt_cnt_gamma_b.Text = "17";
            this.txt_cnt_gamma_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(11, 69);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(208, 13);
            this.label55.TabIndex = 55;
            this.label55.Text = "Angle of friction between Earth and wall [z]";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(446, 15);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(167, 13);
            this.label56.TabIndex = 5;
            this.label56.Text = "Unit Weight of Backfill Earth [γ_b]";
            // 
            // txt_cnt_gamma_c
            // 
            this.txt_cnt_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_gamma_c.Location = new System.Drawing.Point(688, 35);
            this.txt_cnt_gamma_c.Name = "txt_cnt_gamma_c";
            this.txt_cnt_gamma_c.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_gamma_c.TabIndex = 15;
            this.txt_cnt_gamma_c.Text = "25";
            this.txt_cnt_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cnt_z
            // 
            this.txt_cnt_z.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_z.Location = new System.Drawing.Point(253, 65);
            this.txt_cnt_z.Name = "txt_cnt_z";
            this.txt_cnt_z.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_z.TabIndex = 2;
            this.txt_cnt_z.Text = "17.5";
            this.txt_cnt_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(447, 38);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(145, 13);
            this.label58.TabIndex = 7;
            this.label58.Text = "Unit weight of Concrete [γ_c]";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(11, 16);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(170, 26);
            this.label59.TabIndex = 50;
            this.label59.Text = "Angle between wall and Horizontal\r\nbase on Earth Side [θ]";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(11, 93);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(212, 13);
            this.label61.TabIndex = 53;
            this.label61.Text = "Inclination of Earth fill with the Horizontal [δ]";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(787, 13);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(76, 16);
            this.label62.TabIndex = 21;
            this.label62.Text = "kN/cu.m.";
            this.label62.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_cnt_delta
            // 
            this.txt_cnt_delta.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_delta.Location = new System.Drawing.Point(253, 90);
            this.txt_cnt_delta.Name = "txt_cnt_delta";
            this.txt_cnt_delta.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_delta.TabIndex = 3;
            this.txt_cnt_delta.Text = "0";
            this.txt_cnt_delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(788, 38);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(76, 16);
            this.label65.TabIndex = 21;
            this.label65.Text = "kN/cu.m.";
            this.label65.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(788, 219);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(49, 16);
            this.label67.TabIndex = 27;
            this.label67.Text = "kN/m";
            // 
            // txt_cnt_theta
            // 
            this.txt_cnt_theta.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_theta.Location = new System.Drawing.Point(253, 12);
            this.txt_cnt_theta.Name = "txt_cnt_theta";
            this.txt_cnt_theta.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_theta.TabIndex = 0;
            this.txt_cnt_theta.Text = "90";
            this.txt_cnt_theta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(453, 220);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(208, 13);
            this.label68.TabIndex = 26;
            this.label68.Text = "Permanent Load from Super Structure [w5]";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(351, 15);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(15, 16);
            this.label66.TabIndex = 18;
            this.label66.Text = "°";
            this.label66.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(350, 40);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(15, 16);
            this.label69.TabIndex = 18;
            this.label69.Text = "°";
            this.label69.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_cnt_w5
            // 
            this.txt_cnt_w5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_w5.Location = new System.Drawing.Point(688, 217);
            this.txt_cnt_w5.Name = "txt_cnt_w5";
            this.txt_cnt_w5.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_w5.TabIndex = 12;
            this.txt_cnt_w5.Text = "119.0";
            this.txt_cnt_w5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cnt_phi
            // 
            this.txt_cnt_phi.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_phi.Location = new System.Drawing.Point(253, 40);
            this.txt_cnt_phi.Name = "txt_cnt_phi";
            this.txt_cnt_phi.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_phi.TabIndex = 1;
            this.txt_cnt_phi.Text = "35";
            this.txt_cnt_phi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(788, 191);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(49, 16);
            this.label70.TabIndex = 24;
            this.label70.Text = "kN/m";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(11, 44);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(224, 13);
            this.label72.TabIndex = 20;
            this.label72.Text = "Angle Internal Friction (Repose) of Back fill [φ]";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(454, 193);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(142, 13);
            this.label74.TabIndex = 23;
            this.label74.Text = "Live Load from vehicles [w6]";
            // 
            // txt_cnt_w6
            // 
            this.txt_cnt_w6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_w6.Location = new System.Drawing.Point(688, 189);
            this.txt_cnt_w6.Name = "txt_cnt_w6";
            this.txt_cnt_w6.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_w6.TabIndex = 11;
            this.txt_cnt_w6.Text = "85.0";
            this.txt_cnt_w6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(353, 143);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(76, 16);
            this.label76.TabIndex = 21;
            this.label76.Text = "kN/sq.m.";
            this.label76.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(11, 144);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(102, 13);
            this.label82.TabIndex = 11;
            this.label82.Text = "Bearing Capacity [p]";
            // 
            // txt_cnt_p_bearing_capacity
            // 
            this.txt_cnt_p_bearing_capacity.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_p_bearing_capacity.Location = new System.Drawing.Point(253, 140);
            this.txt_cnt_p_bearing_capacity.Name = "txt_cnt_p_bearing_capacity";
            this.txt_cnt_p_bearing_capacity.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_p_bearing_capacity.TabIndex = 5;
            this.txt_cnt_p_bearing_capacity.Text = "150";
            this.txt_cnt_p_bearing_capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txt_cnt_d4);
            this.groupBox3.Controls.Add(this.label84);
            this.groupBox3.Controls.Add(this.label85);
            this.groupBox3.Controls.Add(this.txt_cnt_L4);
            this.groupBox3.Controls.Add(this.txt_cnt_L);
            this.groupBox3.Controls.Add(this.label52);
            this.groupBox3.Controls.Add(this.label86);
            this.groupBox3.Controls.Add(this.label53);
            this.groupBox3.Controls.Add(this.label87);
            this.groupBox3.Controls.Add(this.label88);
            this.groupBox3.Controls.Add(this.label89);
            this.groupBox3.Controls.Add(this.pic_cantilever);
            this.groupBox3.Controls.Add(this.label90);
            this.groupBox3.Controls.Add(this.txt_cnt_t);
            this.groupBox3.Controls.Add(this.label81);
            this.groupBox3.Controls.Add(this.txt_cnt_d3);
            this.groupBox3.Controls.Add(this.label63);
            this.groupBox3.Controls.Add(this.label64);
            this.groupBox3.Controls.Add(this.txt_cnt_L3);
            this.groupBox3.Controls.Add(this.label60);
            this.groupBox3.Controls.Add(this.label57);
            this.groupBox3.Controls.Add(this.txt_cnt_L2);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.txt_cnt_L1);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.txt_cnt_d1);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.txt_cnt_d2);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Controls.Add(this.txt_cnt_B);
            this.groupBox3.Controls.Add(this.label71);
            this.groupBox3.Controls.Add(this.label75);
            this.groupBox3.Controls.Add(this.label77);
            this.groupBox3.Controls.Add(this.txt_cnt_a);
            this.groupBox3.Controls.Add(this.label73);
            this.groupBox3.Controls.Add(this.txt_cnt_H);
            this.groupBox3.Location = new System.Drawing.Point(30, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(850, 333);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "USER DATA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(587, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(218, 18);
            this.label11.TabIndex = 134;
            this.label11.Text = "Default Sample Data are shown";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(435, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 18);
            this.label12.TabIndex = 133;
            this.label12.Text = "All User Input Data";
            // 
            // txt_cnt_d4
            // 
            this.txt_cnt_d4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_d4.Location = new System.Drawing.Point(273, 186);
            this.txt_cnt_d4.Name = "txt_cnt_d4";
            this.txt_cnt_d4.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_d4.TabIndex = 7;
            this.txt_cnt_d4.Text = "1.3";
            this.txt_cnt_d4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(17, 191);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(116, 13);
            this.label84.TabIndex = 88;
            this.label84.Text = "Thickness of Base [d4]";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(371, 188);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(22, 16);
            this.label85.TabIndex = 87;
            this.label85.Text = "m";
            this.label85.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_cnt_L4
            // 
            this.txt_cnt_L4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_L4.Location = new System.Drawing.Point(273, 285);
            this.txt_cnt_L4.Name = "txt_cnt_L4";
            this.txt_cnt_L4.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_L4.TabIndex = 11;
            this.txt_cnt_L4.Text = "0.3";
            this.txt_cnt_L4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_cnt_L
            // 
            this.txt_cnt_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_L.Location = new System.Drawing.Point(273, 309);
            this.txt_cnt_L.Name = "txt_cnt_L";
            this.txt_cnt_L.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_L.TabIndex = 16;
            this.txt_cnt_L.Text = "16.1";
            this.txt_cnt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(18, 312);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(150, 13);
            this.label52.TabIndex = 68;
            this.label52.Text = "Span of Longitudinal Girder [L]";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(18, 289);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(249, 13);
            this.label86.TabIndex = 85;
            this.label86.Text = "Thickness of Dirt wall at Girder Seat at the Top [L4]";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(373, 312);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(22, 16);
            this.label53.TabIndex = 70;
            this.label53.Text = "m";
            this.label53.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(371, 290);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(22, 16);
            this.label87.TabIndex = 84;
            this.label87.Text = "m";
            this.label87.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(371, 21);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(22, 16);
            this.label88.TabIndex = 80;
            this.label88.Text = "m";
            this.label88.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(373, 42);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(22, 16);
            this.label89.TabIndex = 81;
            this.label89.Text = "m";
            this.label89.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pic_cantilever
            // 
            this.pic_cantilever.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_cantilever.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_cantilever.Location = new System.Drawing.Point(405, 40);
            this.pic_cantilever.Name = "pic_cantilever";
            this.pic_cantilever.Size = new System.Drawing.Size(439, 285);
            this.pic_cantilever.TabIndex = 0;
            this.pic_cantilever.TabStop = false;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(17, 44);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(127, 13);
            this.label90.TabIndex = 79;
            this.label90.Text = "Thickness of Main wall [t]";
            // 
            // txt_cnt_t
            // 
            this.txt_cnt_t.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_t.Location = new System.Drawing.Point(273, 40);
            this.txt_cnt_t.Name = "txt_cnt_t";
            this.txt_cnt_t.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_t.TabIndex = 1;
            this.txt_cnt_t.Text = "1.0";
            this.txt_cnt_t.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(17, 24);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(125, 13);
            this.label81.TabIndex = 77;
            this.label81.Text = "Depth of Girder Seat [d1]";
            // 
            // txt_cnt_d3
            // 
            this.txt_cnt_d3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_d3.Location = new System.Drawing.Point(273, 162);
            this.txt_cnt_d3.Name = "txt_cnt_d3";
            this.txt_cnt_d3.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_d3.TabIndex = 6;
            this.txt_cnt_d3.Text = "0.3";
            this.txt_cnt_d3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(370, 266);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(22, 16);
            this.label63.TabIndex = 75;
            this.label63.Text = "m";
            this.label63.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(18, 263);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(169, 13);
            this.label64.TabIndex = 74;
            this.label64.Text = "Length of Base at front of wall [L3]";
            // 
            // txt_cnt_L3
            // 
            this.txt_cnt_L3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_L3.Location = new System.Drawing.Point(273, 259);
            this.txt_cnt_L3.Name = "txt_cnt_L3";
            this.txt_cnt_L3.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_L3.TabIndex = 10;
            this.txt_cnt_L3.Text = "1.8";
            this.txt_cnt_L3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(370, 239);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(22, 16);
            this.label60.TabIndex = 72;
            this.label60.Text = "m";
            this.label60.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(18, 237);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(175, 13);
            this.label57.TabIndex = 71;
            this.label57.Text = "Length of base in wall Location [L2]";
            // 
            // txt_cnt_L2
            // 
            this.txt_cnt_L2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_L2.Location = new System.Drawing.Point(273, 233);
            this.txt_cnt_L2.Name = "txt_cnt_L2";
            this.txt_cnt_L2.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_L2.TabIndex = 9;
            this.txt_cnt_L2.Text = "1.2";
            this.txt_cnt_L2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(370, 215);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(22, 16);
            this.label40.TabIndex = 69;
            this.label40.Text = "m";
            this.label40.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(18, 213);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(170, 13);
            this.label38.TabIndex = 68;
            this.label38.Text = "Length of base in back of wall [L1]";
            // 
            // txt_cnt_L1
            // 
            this.txt_cnt_L1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_L1.Location = new System.Drawing.Point(273, 209);
            this.txt_cnt_L1.Name = "txt_cnt_L1";
            this.txt_cnt_L1.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_L1.TabIndex = 8;
            this.txt_cnt_L1.Text = "3.0";
            this.txt_cnt_L1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(370, 165);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(22, 16);
            this.label36.TabIndex = 66;
            this.label36.Text = "m";
            this.label36.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(17, 171);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(162, 13);
            this.label37.TabIndex = 65;
            this.label37.Text = "Thickness of Approach Slab [d3]";
            // 
            // txt_cnt_d1
            // 
            this.txt_cnt_d1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_d1.Location = new System.Drawing.Point(273, 15);
            this.txt_cnt_d1.Name = "txt_cnt_d1";
            this.txt_cnt_d1.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_d1.TabIndex = 0;
            this.txt_cnt_d1.Text = "1.3";
            this.txt_cnt_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(370, 140);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(22, 16);
            this.label39.TabIndex = 63;
            this.label39.Text = "m";
            this.label39.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(18, 134);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(146, 26);
            this.label41.TabIndex = 62;
            this.label41.Text = "Equivalent height of earth for \r\nLive Load Surcharge [d2]";
            // 
            // txt_cnt_d2
            // 
            this.txt_cnt_d2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_d2.Location = new System.Drawing.Point(273, 138);
            this.txt_cnt_d2.Name = "txt_cnt_d2";
            this.txt_cnt_d2.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_d2.TabIndex = 5;
            this.txt_cnt_d2.Text = "1.2";
            this.txt_cnt_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(371, 114);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(22, 16);
            this.label47.TabIndex = 60;
            this.label47.Text = "m";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(17, 116);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(84, 13);
            this.label54.TabIndex = 59;
            this.label54.Text = "Width of wall [B]";
            // 
            // txt_cnt_B
            // 
            this.txt_cnt_B.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_B.Location = new System.Drawing.Point(273, 112);
            this.txt_cnt_B.Name = "txt_cnt_B";
            this.txt_cnt_B.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_B.TabIndex = 4;
            this.txt_cnt_B.Text = "8.5";
            this.txt_cnt_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(371, 90);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(22, 16);
            this.label71.TabIndex = 53;
            this.label71.Text = "m";
            this.label71.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(372, 63);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(22, 16);
            this.label75.TabIndex = 54;
            this.label75.Text = "m";
            this.label75.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(17, 91);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(129, 13);
            this.label77.TabIndex = 52;
            this.label77.Text = "Height of Earth at front [a]";
            // 
            // txt_cnt_a
            // 
            this.txt_cnt_a.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_a.Location = new System.Drawing.Point(273, 88);
            this.txt_cnt_a.Name = "txt_cnt_a";
            this.txt_cnt_a.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_a.TabIndex = 3;
            this.txt_cnt_a.Text = "1.25";
            this.txt_cnt_a.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(17, 67);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(141, 13);
            this.label73.TabIndex = 50;
            this.label73.Text = "Height of Retained Earth [H]";
            // 
            // txt_cnt_H
            // 
            this.txt_cnt_H.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cnt_H.Location = new System.Drawing.Point(273, 64);
            this.txt_cnt_H.Name = "txt_cnt_H";
            this.txt_cnt_H.Size = new System.Drawing.Size(94, 22);
            this.txt_cnt_H.TabIndex = 2;
            this.txt_cnt_H.Text = "5.6";
            this.txt_cnt_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label210
            // 
            this.label210.AutoSize = true;
            this.label210.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label210.Location = new System.Drawing.Point(192, 6);
            this.label210.Name = "label210";
            this.label210.Size = new System.Drawing.Size(308, 23);
            this.label210.TabIndex = 8;
            this.label210.Text = "Editable Construction Drawings";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(66, 154);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(550, 16);
            this.label91.TabIndex = 7;
            this.label91.Text = "Button is Enabled Once the Abutment as Cantilever Retaining Wall Design is done.";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(66, 42);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(560, 16);
            this.label143.TabIndex = 6;
            this.label143.Text = "Button is Enabled Once the Abutment as Counterfort Retaining Wall Design is done." +
    "";
            // 
            // btn_dwg_cantilever
            // 
            this.btn_dwg_cantilever.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_cantilever.Location = new System.Drawing.Point(192, 173);
            this.btn_dwg_cantilever.Name = "btn_dwg_cantilever";
            this.btn_dwg_cantilever.Size = new System.Drawing.Size(308, 51);
            this.btn_dwg_cantilever.TabIndex = 3;
            this.btn_dwg_cantilever.Text = "Abutment as Cantilever Retaining wall Drawings";
            this.btn_dwg_cantilever.UseVisualStyleBackColor = true;
            this.btn_dwg_cantilever.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // btn_dwg_counterfort
            // 
            this.btn_dwg_counterfort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_counterfort.Location = new System.Drawing.Point(192, 61);
            this.btn_dwg_counterfort.Name = "btn_dwg_counterfort";
            this.btn_dwg_counterfort.Size = new System.Drawing.Size(308, 51);
            this.btn_dwg_counterfort.TabIndex = 2;
            this.btn_dwg_counterfort.Text = "Abutment as Counterfort Retaining wall Drawings";
            this.btn_dwg_counterfort.UseVisualStyleBackColor = true;
            this.btn_dwg_counterfort.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // btn_dwg_worksheet2
            // 
            this.btn_dwg_worksheet2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_worksheet2.Location = new System.Drawing.Point(192, 326);
            this.btn_dwg_worksheet2.Name = "btn_dwg_worksheet2";
            this.btn_dwg_worksheet2.Size = new System.Drawing.Size(308, 51);
            this.btn_dwg_worksheet2.TabIndex = 1;
            this.btn_dwg_worksheet2.Text = "Abutment Worksheet Design 2 Drawings";
            this.btn_dwg_worksheet2.UseVisualStyleBackColor = true;
            this.btn_dwg_worksheet2.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // btn_dwg_worksheet1
            // 
            this.btn_dwg_worksheet1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_worksheet1.Location = new System.Drawing.Point(192, 269);
            this.btn_dwg_worksheet1.Name = "btn_dwg_worksheet1";
            this.btn_dwg_worksheet1.Size = new System.Drawing.Size(308, 51);
            this.btn_dwg_worksheet1.TabIndex = 0;
            this.btn_dwg_worksheet1.Text = "Abutment Worksheet Design 1 Drawings";
            this.btn_dwg_worksheet1.UseVisualStyleBackColor = true;
            this.btn_dwg_worksheet1.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(954, 697);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uC_Abut_Counterfort_LS1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(946, 671);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Abutment as Counterfort";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uC_Abut_Counterfort_LS1
            // 
            this.uC_Abut_Counterfort_LS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Abut_Counterfort_LS1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Abut_Counterfort_LS1.iapp = null;
            this.uC_Abut_Counterfort_LS1.Is_Individual = true;
            this.uC_Abut_Counterfort_LS1.Length = 51.3D;
            this.uC_Abut_Counterfort_LS1.Location = new System.Drawing.Point(3, 3);
            this.uC_Abut_Counterfort_LS1.Name = "uC_Abut_Counterfort_LS1";
            this.uC_Abut_Counterfort_LS1.Reaction_A = "4417.59 ";
            this.uC_Abut_Counterfort_LS1.Reaction_B = "4417.59 ";
            this.uC_Abut_Counterfort_LS1.Size = new System.Drawing.Size(940, 665);
            this.uC_Abut_Counterfort_LS1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.uC_Abut_Cant1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(946, 671);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Abutment as Cantilever";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uC_Abut_Cant1
            // 
            this.uC_Abut_Cant1.Dead_Load_Reactions = "1635.12";
            this.uC_Abut_Cant1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Abut_Cant1.Length = "7.900";
            this.uC_Abut_Cant1.Location = new System.Drawing.Point(3, 3);
            this.uC_Abut_Cant1.Name = "uC_Abut_Cant1";
            this.uC_Abut_Cant1.Size = new System.Drawing.Size(940, 665);
            this.uC_Abut_Cant1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(946, 671);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Other Design";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.btn_dwg_open_Cantilever);
            this.tabPage4.Controls.Add(this.btn_dwg_open_Counterfort);
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(946, 671);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Drawings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(320, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(308, 23);
            this.label13.TabIndex = 8;
            this.label13.Text = "Editable Construction Drawings";
            // 
            // btn_dwg_open_Cantilever
            // 
            this.btn_dwg_open_Cantilever.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_open_Cantilever.Location = new System.Drawing.Point(324, 208);
            this.btn_dwg_open_Cantilever.Name = "btn_dwg_open_Cantilever";
            this.btn_dwg_open_Cantilever.Size = new System.Drawing.Size(311, 63);
            this.btn_dwg_open_Cantilever.TabIndex = 10;
            this.btn_dwg_open_Cantilever.Text = "Open Cantilever Abutment Drawings";
            this.btn_dwg_open_Cantilever.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Cantilever.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // btn_dwg_open_Counterfort
            // 
            this.btn_dwg_open_Counterfort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dwg_open_Counterfort.Location = new System.Drawing.Point(324, 139);
            this.btn_dwg_open_Counterfort.Name = "btn_dwg_open_Counterfort";
            this.btn_dwg_open_Counterfort.Size = new System.Drawing.Size(311, 63);
            this.btn_dwg_open_Counterfort.TabIndex = 11;
            this.btn_dwg_open_Counterfort.Text = "Open Counterfort Abutment Drawings";
            this.btn_dwg_open_Counterfort.UseVisualStyleBackColor = true;
            this.btn_dwg_open_Counterfort.Click += new System.EventHandler(this.btn_drawings_open_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label210);
            this.panel2.Controls.Add(this.label91);
            this.panel2.Controls.Add(this.label143);
            this.panel2.Controls.Add(this.btn_dwg_cantilever);
            this.panel2.Controls.Add(this.btn_dwg_counterfort);
            this.panel2.Controls.Add(this.btn_dwg_worksheet2);
            this.panel2.Controls.Add(this.btn_dwg_worksheet1);
            this.panel2.Location = new System.Drawing.Point(26, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(222, 286);
            this.panel2.TabIndex = 9;
            this.panel2.Visible = false;
            // 
            // frm_Abutment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 697);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Abutment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Abutment";
            this.Load += new System.EventHandler(this.frm_Abutment_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_counter_fort_ret_wall.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_counter_fort_drawing1)).EndInit();
            this.tab_cantilever_ret_wall.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cantilever)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_counter_fort_ret_wall;
        private System.Windows.Forms.TabPage tab_cantilever_ret_wall;
        private System.Windows.Forms.Button btn_dwg_worksheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_counterfort_th3;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_counterfort_l;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_counterfort_mu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_counterfort_Kc;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_counterfort_Rc;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_counterfort_phi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_counterfort_P;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_counterfort_gama_c;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_counterfort_gama_s;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_counterfort_q0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_counterfort_th2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_counterfort_th1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_counterfort_h2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_counterfort_h1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing3;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing2;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing1;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing6;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing4;
        private System.Windows.Forms.PictureBox pic_counter_fort_drawing5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_counterfort_Process;
        private System.Windows.Forms.Button btn_counterfort_Report;
        private System.Windows.Forms.PictureBox pic_cantilever;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txt_cnt_h1;
        private System.Windows.Forms.TextBox txt_cnt_L1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_cnt_L;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txt_cnt_d1;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblCover;
        private System.Windows.Forms.TextBox txt_cnt_cover;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_cnt_L2;
        private System.Windows.Forms.TextBox txt_cnt_fact;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txt_cnt_F;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txt_cnt_d2;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txt_cnt_mu;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txt_cnt_gamma_b;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txt_cnt_z;
        private System.Windows.Forms.TextBox txt_cnt_gamma_c;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txt_cnt_delta;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txt_cnt_L3;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txt_cnt_B;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox txt_cnt_theta;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.TextBox txt_cnt_w5;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txt_cnt_phi;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txt_cnt_w6;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox txt_cnt_a;
        private System.Windows.Forms.TextBox txt_cnt_d3;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txt_cnt_p_bearing_capacity;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Button btn_cnt_Report;
        private System.Windows.Forms.Button btn_cnt_Process;
        private System.Windows.Forms.TextBox txt_cnt_d4;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_cnt_L4;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.TextBox txt_cnt_t;
        private System.Windows.Forms.TextBox txt_cnt_H;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_dwg_counterfort;
        private System.Windows.Forms.Button btn_dwg_worksheet2;
        private System.Windows.Forms.Button btn_dwg_cantilever;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txt_cnt_sc;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.TextBox txt_cntf_m;
        private System.Windows.Forms.TextBox txt_cntf_Q;
        private System.Windows.Forms.Label label191;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.ComboBox cmb_cntf_fy;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.ComboBox cmb_cntf_fck;
        private System.Windows.Forms.TextBox txt_cntf_j;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.Label label203;
        private System.Windows.Forms.Label label204;
        private System.Windows.Forms.Label label205;
        private System.Windows.Forms.TextBox txt_cntf_sigma_st;
        private System.Windows.Forms.Label label206;
        private System.Windows.Forms.Label label207;
        private System.Windows.Forms.TextBox txt_cntf_sigma_c;
        private System.Windows.Forms.Label label209;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Label label214;
        private System.Windows.Forms.ComboBox cmb_cant_fy;
        private System.Windows.Forms.Label label216;
        private System.Windows.Forms.ComboBox cmb_cant_fck;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Label label229;
        private System.Windows.Forms.TextBox txt_cant_sigma_st;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.TextBox txt_cant_sigma_c;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label210;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_cfort_open_des;
        private System.Windows.Forms.Button btn_cant_open_des;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel2;
        private UC_Abut_Counterfort_LS uC_Abut_Counterfort_LS1;
        private UC_Abut_Cant uC_Abut_Cant1;
        private System.Windows.Forms.Button btn_dwg_open_Cantilever;
        private System.Windows.Forms.Button btn_dwg_open_Counterfort;
        private System.Windows.Forms.Label label13;
    }
}