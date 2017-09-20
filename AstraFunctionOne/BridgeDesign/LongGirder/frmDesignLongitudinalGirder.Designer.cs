namespace AstraFunctionOne.BridgeDesign.Design2
{
    partial class frmDesignLongitudinalGirder
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_d_s = new System.Windows.Forms.TextBox();
            this.txt_D = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_bw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_span_girders = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Gs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_design_moment_mid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_sigma_c = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_modular_ratio = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_bar_dia = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_total_bar = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.cmb_Steel_Grade = new System.Windows.Forms.ComboBox();
            this.cmb_concrete_grade = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_space_main_girder = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txt_space_cross_girder = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_sigma_sv = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_cover = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_design_shear_deff = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_design_shear_quarter = new System.Windows.Forms.TextBox();
            this.txt_design_shear_mid = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_design_moment_deff = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_design_moment_quarter = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_outert_girder = new System.Windows.Forms.RadioButton();
            this.rbtn_inner_girder = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWorkingFolder = new System.Windows.Forms.Button();
            this.btnDrawing = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(121, 11);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(63, 23);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thickness of Deck Slab";
            // 
            // txt_d_s
            // 
            this.txt_d_s.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d_s.Location = new System.Drawing.Point(197, 175);
            this.txt_d_s.Name = "txt_d_s";
            this.txt_d_s.Size = new System.Drawing.Size(72, 22);
            this.txt_d_s.TabIndex = 6;
            this.txt_d_s.Text = "0.252";
            this.txt_d_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_D
            // 
            this.txt_D.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_D.Location = new System.Drawing.Point(197, 203);
            this.txt_D.Name = "txt_D";
            this.txt_D.Size = new System.Drawing.Size(72, 22);
            this.txt_D.TabIndex = 7;
            this.txt_D.Text = "1.8";
            this.txt_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Depth of Longitudinal Girder";
            // 
            // txt_bw
            // 
            this.txt_bw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bw.Location = new System.Drawing.Point(197, 240);
            this.txt_bw.Name = "txt_bw";
            this.txt_bw.Size = new System.Drawing.Size(72, 22);
            this.txt_bw.TabIndex = 8;
            this.txt_bw.Text = "0.3";
            this.txt_bw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Web Thickness of Longitudinal Girders";
            // 
            // txt_span_girders
            // 
            this.txt_span_girders.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_span_girders.Location = new System.Drawing.Point(197, 268);
            this.txt_span_girders.Name = "txt_span_girders";
            this.txt_span_girders.Size = new System.Drawing.Size(72, 22);
            this.txt_span_girders.TabIndex = 9;
            this.txt_span_girders.Text = "13.6";
            this.txt_span_girders.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Span of Girders";
            // 
            // txt_Gs
            // 
            this.txt_Gs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Gs.Location = new System.Drawing.Point(197, 296);
            this.txt_Gs.Name = "txt_Gs";
            this.txt_Gs.Size = new System.Drawing.Size(72, 22);
            this.txt_Gs.TabIndex = 10;
            this.txt_Gs.Text = "3.0";
            this.txt_Gs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "c/c distance of Longitudinal Girders";
            // 
            // txt_design_moment_mid
            // 
            this.txt_design_moment_mid.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_moment_mid.Location = new System.Drawing.Point(197, 62);
            this.txt_design_moment_mid.Name = "txt_design_moment_mid";
            this.txt_design_moment_mid.Size = new System.Drawing.Size(72, 22);
            this.txt_design_moment_mid.TabIndex = 0;
            this.txt_design_moment_mid.Text = "255.77";
            this.txt_design_moment_mid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "At Mid Span (L/2) Design Moment";
            // 
            // txt_sigma_c
            // 
            this.txt_sigma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_c.Location = new System.Drawing.Point(197, 351);
            this.txt_sigma_c.Name = "txt_sigma_c";
            this.txt_sigma_c.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_c.TabIndex = 12;
            this.txt_sigma_c.Text = "83.3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Concrete Grade";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 379);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Steel Grade";
            // 
            // txt_modular_ratio
            // 
            this.txt_modular_ratio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_modular_ratio.Location = new System.Drawing.Point(197, 437);
            this.txt_modular_ratio.Name = "txt_modular_ratio";
            this.txt_modular_ratio.Size = new System.Drawing.Size(72, 22);
            this.txt_modular_ratio.TabIndex = 15;
            this.txt_modular_ratio.Text = "10";
            this.txt_modular_ratio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 438);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Modular Ratio ";
            // 
            // txt_bar_dia
            // 
            this.txt_bar_dia.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bar_dia.Location = new System.Drawing.Point(197, 465);
            this.txt_bar_dia.Name = "txt_bar_dia";
            this.txt_bar_dia.Size = new System.Drawing.Size(72, 22);
            this.txt_bar_dia.TabIndex = 16;
            this.txt_bar_dia.Text = "3.2";
            this.txt_bar_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 469);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Bar Dia";
            // 
            // txt_total_bar
            // 
            this.txt_total_bar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_bar.Location = new System.Drawing.Point(197, 493);
            this.txt_total_bar.Name = "txt_total_bar";
            this.txt_total_bar.Size = new System.Drawing.Size(72, 22);
            this.txt_total_bar.TabIndex = 17;
            this.txt_total_bar.Text = "17";
            this.txt_total_bar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 497);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Total Bars";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.cmb_Steel_Grade);
            this.groupBox1.Controls.Add(this.cmb_concrete_grade);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.txt_space_main_girder);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.txt_space_cross_girder);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.txt_sigma_sv);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txt_cover);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txt_design_shear_deff);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txt_design_shear_quarter);
            this.groupBox1.Controls.Add(this.txt_design_shear_mid);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_design_moment_deff);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txt_design_moment_quarter);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_total_bar);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_bar_dia);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_modular_ratio);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_sigma_c);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_design_moment_mid);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_Gs);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_span_girders);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_bw);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_D);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_d_s);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(917, 604);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER DATA";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(275, 355);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(60, 13);
            this.label39.TabIndex = 83;
            this.label39.Text = "kg/sq.cm";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(6, 355);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(176, 13);
            this.label38.TabIndex = 82;
            this.label38.Text = "Permissible Stress in Concrete [σ_c]";
            // 
            // cmb_Steel_Grade
            // 
            this.cmb_Steel_Grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Steel_Grade.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Steel_Grade.FormattingEnabled = true;
            this.cmb_Steel_Grade.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_Steel_Grade.Location = new System.Drawing.Point(208, 379);
            this.cmb_Steel_Grade.Name = "cmb_Steel_Grade";
            this.cmb_Steel_Grade.Size = new System.Drawing.Size(61, 21);
            this.cmb_Steel_Grade.TabIndex = 13;
            this.cmb_Steel_Grade.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_SelectedIndexChanged);
            // 
            // cmb_concrete_grade
            // 
            this.cmb_concrete_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_concrete_grade.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_concrete_grade.FormattingEnabled = true;
            this.cmb_concrete_grade.Items.AddRange(new object[] {
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
            this.cmb_concrete_grade.Location = new System.Drawing.Point(208, 324);
            this.cmb_concrete_grade.Name = "cmb_concrete_grade";
            this.cmb_concrete_grade.Size = new System.Drawing.Size(61, 21);
            this.cmb_concrete_grade.TabIndex = 11;
            this.cmb_concrete_grade.SelectedIndexChanged += new System.EventHandler(this.cmb_concrete_grade_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(275, 579);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(19, 14);
            this.label34.TabIndex = 65;
            this.label34.Text = "m";
            // 
            // txt_space_main_girder
            // 
            this.txt_space_main_girder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_space_main_girder.Location = new System.Drawing.Point(197, 576);
            this.txt_space_main_girder.Name = "txt_space_main_girder";
            this.txt_space_main_girder.Size = new System.Drawing.Size(72, 22);
            this.txt_space_main_girder.TabIndex = 20;
            this.txt_space_main_girder.Text = "2.5";
            this.txt_space_main_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(6, 582);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(119, 13);
            this.label35.TabIndex = 64;
            this.label35.Text = "Spacing of main Girders";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(275, 552);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(19, 14);
            this.label36.TabIndex = 62;
            this.label36.Text = "m";
            // 
            // txt_space_cross_girder
            // 
            this.txt_space_cross_girder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_space_cross_girder.Location = new System.Drawing.Point(197, 549);
            this.txt_space_cross_girder.Name = "txt_space_cross_girder";
            this.txt_space_cross_girder.Size = new System.Drawing.Size(72, 22);
            this.txt_space_cross_girder.TabIndex = 19;
            this.txt_space_cross_girder.Text = "4";
            this.txt_space_cross_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 553);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(123, 13);
            this.label37.TabIndex = 61;
            this.label37.Text = "Spacing of Cross Girders";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::AstraFunctionOne.Properties.Resources.RCC_T_Beam_Bridge;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(446, 397);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(465, 158);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(179, 381);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(23, 14);
            this.label32.TabIndex = 59;
            this.label32.Text = "Fe";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(184, 327);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(18, 14);
            this.label33.TabIndex = 58;
            this.label33.Text = "M";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(275, 412);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(61, 13);
            this.label52.TabIndex = 57;
            this.label52.Text = "N/sq.mm.";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(271, 142);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 14);
            this.label30.TabIndex = 46;
            this.label30.Text = "t-m";
            // 
            // txt_sigma_sv
            // 
            this.txt_sigma_sv.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_sv.Location = new System.Drawing.Point(197, 406);
            this.txt_sigma_sv.Name = "txt_sigma_sv";
            this.txt_sigma_sv.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_sv.TabIndex = 14;
            this.txt_sigma_sv.Text = "200";
            this.txt_sigma_sv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(6, 412);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(162, 13);
            this.label31.TabIndex = 55;
            this.label31.Text = "Permissible Stress in Steel [σ_sv]";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(271, 103);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(30, 14);
            this.label29.TabIndex = 45;
            this.label29.Text = "t-m";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(271, 65);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(30, 14);
            this.label28.TabIndex = 44;
            this.label28.Text = "t-m";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(428, 146);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(12, 14);
            this.label27.TabIndex = 43;
            this.label27.Text = "t";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(428, 103);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(12, 14);
            this.label26.TabIndex = 42;
            this.label26.Text = "t";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(428, 65);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(12, 14);
            this.label25.TabIndex = 41;
            this.label25.Text = "t";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(275, 524);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(31, 14);
            this.label24.TabIndex = 40;
            this.label24.Text = "mm";
            // 
            // txt_cover
            // 
            this.txt_cover.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cover.Location = new System.Drawing.Point(197, 521);
            this.txt_cover.Name = "txt_cover";
            this.txt_cover.Size = new System.Drawing.Size(72, 22);
            this.txt_cover.TabIndex = 18;
            this.txt_cover.Text = "50";
            this.txt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 525);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 39;
            this.label23.Text = "Cover";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(309, 142);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Shear";
            // 
            // txt_design_shear_deff
            // 
            this.txt_design_shear_deff.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_shear_deff.Location = new System.Drawing.Point(350, 138);
            this.txt_design_shear_deff.Name = "txt_design_shear_deff";
            this.txt_design_shear_deff.Size = new System.Drawing.Size(72, 22);
            this.txt_design_shear_deff.TabIndex = 5;
            this.txt_design_shear_deff.Text = "80.20";
            this.txt_design_shear_deff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(309, 104);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 13);
            this.label21.TabIndex = 35;
            this.label21.Text = "Shear";
            // 
            // txt_design_shear_quarter
            // 
            this.txt_design_shear_quarter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_shear_quarter.Location = new System.Drawing.Point(350, 100);
            this.txt_design_shear_quarter.Name = "txt_design_shear_quarter";
            this.txt_design_shear_quarter.Size = new System.Drawing.Size(72, 22);
            this.txt_design_shear_quarter.TabIndex = 3;
            this.txt_design_shear_quarter.Text = "65.43";
            this.txt_design_shear_quarter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_design_shear_mid
            // 
            this.txt_design_shear_mid.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_shear_mid.Location = new System.Drawing.Point(350, 62);
            this.txt_design_shear_mid.Name = "txt_design_shear_mid";
            this.txt_design_shear_mid.Size = new System.Drawing.Size(72, 22);
            this.txt_design_shear_mid.TabIndex = 1;
            this.txt_design_shear_mid.Text = "51.88";
            this.txt_design_shear_mid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(309, 66);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(35, 13);
            this.label22.TabIndex = 33;
            this.label22.Text = "Shear";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 134);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(126, 26);
            this.label19.TabIndex = 31;
            this.label19.Text = "At Effective Depth (Deff)\r\nDistance Design Moment";
            // 
            // txt_design_moment_deff
            // 
            this.txt_design_moment_deff.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_moment_deff.Location = new System.Drawing.Point(197, 138);
            this.txt_design_moment_deff.Name = "txt_design_moment_deff";
            this.txt_design_moment_deff.Size = new System.Drawing.Size(72, 22);
            this.txt_design_moment_deff.TabIndex = 4;
            this.txt_design_moment_deff.Text = "94.92";
            this.txt_design_moment_deff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(186, 13);
            this.label18.TabIndex = 29;
            this.label18.Text = "At Quarter Span (L/4) Design Moment";
            // 
            // txt_design_moment_quarter
            // 
            this.txt_design_moment_quarter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_design_moment_quarter.Location = new System.Drawing.Point(197, 100);
            this.txt_design_moment_quarter.Name = "txt_design_moment_quarter";
            this.txt_design_moment_quarter.Size = new System.Drawing.Size(72, 22);
            this.txt_design_moment_quarter.TabIndex = 2;
            this.txt_design_moment_quarter.Text = "207.20";
            this.txt_design_moment_quarter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.T_Beam_Slab_Long_Cross_Girders;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(446, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(465, 378);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(275, 243);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 14);
            this.label17.TabIndex = 26;
            this.label17.Text = "m";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(275, 299);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 14);
            this.label16.TabIndex = 25;
            this.label16.Text = "m";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(275, 467);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 14);
            this.label15.TabIndex = 24;
            this.label15.Text = "cm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(275, 175);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 14);
            this.label13.TabIndex = 23;
            this.label13.Text = "m";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(275, 271);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 14);
            this.label14.TabIndex = 23;
            this.label14.Text = "m";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(275, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 14);
            this.label12.TabIndex = 23;
            this.label12.Text = "m";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtn_outert_girder);
            this.groupBox3.Controls.Add(this.rbtn_inner_girder);
            this.groupBox3.Location = new System.Drawing.Point(10, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 38);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // rbtn_outert_girder
            // 
            this.rbtn_outert_girder.AutoSize = true;
            this.rbtn_outert_girder.Location = new System.Drawing.Point(134, 15);
            this.rbtn_outert_girder.Name = "rbtn_outert_girder";
            this.rbtn_outert_girder.Size = new System.Drawing.Size(82, 17);
            this.rbtn_outert_girder.TabIndex = 1;
            this.rbtn_outert_girder.Text = "Outer Girder";
            this.rbtn_outert_girder.UseVisualStyleBackColor = true;
            this.rbtn_outert_girder.CheckedChanged += new System.EventHandler(this.rbtn_inner_girder_CheckedChanged);
            // 
            // rbtn_inner_girder
            // 
            this.rbtn_inner_girder.AutoSize = true;
            this.rbtn_inner_girder.Checked = true;
            this.rbtn_inner_girder.Location = new System.Drawing.Point(26, 15);
            this.rbtn_inner_girder.Name = "rbtn_inner_girder";
            this.rbtn_inner_girder.Size = new System.Drawing.Size(80, 17);
            this.rbtn_inner_girder.TabIndex = 0;
            this.rbtn_inner_girder.TabStop = true;
            this.rbtn_inner_girder.Text = "Inner Girder";
            this.rbtn_inner_girder.UseVisualStyleBackColor = true;
            this.rbtn_inner_girder.CheckedChanged += new System.EventHandler(this.rbtn_inner_girder_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWorkingFolder);
            this.groupBox2.Controls.Add(this.btnDrawing);
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnProcess);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(467, 603);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btnWorkingFolder
            // 
            this.btnWorkingFolder.Location = new System.Drawing.Point(6, 11);
            this.btnWorkingFolder.Name = "btnWorkingFolder";
            this.btnWorkingFolder.Size = new System.Drawing.Size(109, 23);
            this.btnWorkingFolder.TabIndex = 0;
            this.btnWorkingFolder.Text = "Working Folder";
            this.btnWorkingFolder.UseVisualStyleBackColor = true;
            this.btnWorkingFolder.Click += new System.EventHandler(this.btnWorkingFolder_Click);
            // 
            // btnDrawing
            // 
            this.btnDrawing.Enabled = false;
            this.btnDrawing.Location = new System.Drawing.Point(268, 11);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(101, 23);
            this.btnDrawing.TabIndex = 3;
            this.btnDrawing.Text = "Default Drawing";
            this.btnDrawing.UseVisualStyleBackColor = true;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // btnReport
            // 
            this.btnReport.Enabled = false;
            this.btnReport.Location = new System.Drawing.Point(190, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(63, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(375, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // frmDesignLongitudinalGirder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 642);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDesignLongitudinalGirder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design Longitudinal Girder";
            this.Load += new System.EventHandler(this.frmDesignLongitudinalGirder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_d_s;
        private System.Windows.Forms.TextBox txt_D;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_bw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_span_girders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Gs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_design_moment_mid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_sigma_c;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_modular_ratio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_bar_dia;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_total_bar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDrawing;
        private System.Windows.Forms.Button btnWorkingFolder;
        private System.Windows.Forms.TextBox txt_design_moment_quarter;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_design_moment_deff;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_design_shear_deff;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_design_shear_quarter;
        private System.Windows.Forms.TextBox txt_design_shear_mid;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_cover;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtn_inner_girder;
        private System.Windows.Forms.RadioButton rbtn_outert_girder;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txt_sigma_sv;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txt_space_main_girder;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txt_space_cross_girder;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmb_concrete_grade;
        private System.Windows.Forms.ComboBox cmb_Steel_Grade;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
    }
}