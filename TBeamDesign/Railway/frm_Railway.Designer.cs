namespace BridgeAnalysisDesign.Railway
{
    partial class frm_Railway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Railway));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.btn_browse_design = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label830 = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label211 = new System.Windows.Forms.Label();
            this.label212 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txt_tw = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txt_Dw = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_ES2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_sigma_t = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_Llb = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_pl = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_Cw = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Pw = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_PBS = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_K = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_sigma_tt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Lf = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_tau_v = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_sigma_b = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_wa = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ES1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_FL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_TRL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Ws = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Wm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_G = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_FLL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_L = new System.Windows.Forms.TextBox();
            this.pic_rail2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_open_des = new System.Windows.Forms.Button();
            this.pic_rail1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label143 = new System.Windows.Forms.Label();
            this.btn_dwg_plate = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rail2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rail1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(915, 658);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Controls.Add(this.btnReport);
            this.tabPage1.Controls.Add(this.btnProcess);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.pic_rail2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.pic_rail1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(907, 632);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Steel Plate Girder Railway Bridge";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_new_design);
            this.panel5.Controls.Add(this.btn_browse_design);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label830);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(426, 56);
            this.panel5.TabIndex = 182;
            // 
            // btn_new_design
            // 
            this.btn_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "New Design";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // btn_browse_design
            // 
            this.btn_browse_design.Location = new System.Drawing.Point(242, 4);
            this.btn_browse_design.Name = "btn_browse_design";
            this.btn_browse_design.Size = new System.Drawing.Size(121, 24);
            this.btn_browse_design.TabIndex = 189;
            this.btn_browse_design.Text = "Open Design";
            this.btn_browse_design.UseVisualStyleBackColor = true;
            this.btn_browse_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(104, 30);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(258, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label830
            // 
            this.label830.AutoSize = true;
            this.label830.Location = new System.Drawing.Point(5, 34);
            this.label830.Name = "label830";
            this.label830.Size = new System.Drawing.Size(77, 13);
            this.label830.TabIndex = 187;
            this.label830.Text = "Project Name :";
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(458, 596);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(86, 31);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btn_Plate_Report_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(365, 596);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(86, 31);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btn_Plate_Process_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label211);
            this.groupBox1.Controls.Add(this.label212);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.txt_tw);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.txt_Dw);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txt_ES2);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txt_sigma_t);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_Llb);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txt_pl);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt_Cw);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txt_Pw);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txt_PBS);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_K);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_sigma_tt);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_Lf);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_tau_v);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_sigma_b);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_wa);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_ES1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_FL);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_TRL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_Ws);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_Wm);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_G);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_FLL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_L);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(893, 337);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER\'S Data";
            // 
            // label211
            // 
            this.label211.AutoSize = true;
            this.label211.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label211.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label211.ForeColor = System.Drawing.Color.Red;
            this.label211.Location = new System.Drawing.Point(151, 14);
            this.label211.Name = "label211";
            this.label211.Size = new System.Drawing.Size(218, 18);
            this.label211.TabIndex = 173;
            this.label211.Text = "Default Sample Data are shown";
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label212.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label212.ForeColor = System.Drawing.Color.Green;
            this.label212.Location = new System.Drawing.Point(10, 14);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(135, 18);
            this.label212.TabIndex = 172;
            this.label212.Text = "All User Input Data";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(805, 304);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(31, 14);
            this.label39.TabIndex = 51;
            this.label39.Text = "mm";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(430, 308);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(138, 13);
            this.label40.TabIndex = 50;
            this.label40.Text = "Thickness of Web Plate";
            // 
            // txt_tw
            // 
            this.txt_tw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tw.Location = new System.Drawing.Point(724, 301);
            this.txt_tw.Name = "txt_tw";
            this.txt_tw.Size = new System.Drawing.Size(66, 22);
            this.txt_tw.TabIndex = 49;
            this.txt_tw.Text = "12";
            this.txt_tw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(358, 304);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 14);
            this.label37.TabIndex = 48;
            this.label37.Text = "mm";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 305);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(116, 13);
            this.label38.TabIndex = 47;
            this.label38.Text = "Depth of Web Plate";
            // 
            // txt_Dw
            // 
            this.txt_Dw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Dw.Location = new System.Drawing.Point(285, 301);
            this.txt_Dw.Name = "txt_Dw";
            this.txt_Dw.Size = new System.Drawing.Size(67, 22);
            this.txt_Dw.TabIndex = 46;
            this.txt_Dw.Text = "1500";
            this.txt_Dw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(796, 222);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 14);
            this.label34.TabIndex = 45;
            this.label34.Text = "kN/m.";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(796, 166);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(68, 14);
            this.label33.TabIndex = 45;
            this.label33.Text = "kN/sq.m.";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(792, 279);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(72, 14);
            this.label36.TabIndex = 45;
            this.label36.Text = "N/sq.mm.";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(796, 140);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(72, 14);
            this.label32.TabIndex = 45;
            this.label32.Text = "N/sq.mm.";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(796, 86);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 14);
            this.label31.TabIndex = 45;
            this.label31.Text = "N/sq.mm.";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(796, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 14);
            this.label29.TabIndex = 45;
            this.label29.Text = "N/sq.mm.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(358, 154);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(23, 14);
            this.label26.TabIndex = 44;
            this.label26.Text = "kN";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(358, 121);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(23, 14);
            this.label25.TabIndex = 44;
            this.label25.Text = "kN";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(358, 64);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 14);
            this.label23.TabIndex = 43;
            this.label23.Text = "kN/m";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(358, 279);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(62, 14);
            this.label28.TabIndex = 42;
            this.label28.Text = "N/sq.mm";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(358, 253);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(18, 14);
            this.label27.TabIndex = 42;
            this.label27.Text = "m";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(358, 89);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 14);
            this.label24.TabIndex = 42;
            this.label24.Text = "m";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(796, 251);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(19, 14);
            this.label35.TabIndex = 42;
            this.label35.Text = "m";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(796, 59);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(19, 14);
            this.label30.TabIndex = 42;
            this.label30.Text = "m";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(358, 37);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(18, 14);
            this.label21.TabIndex = 42;
            this.label21.Text = "m";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(305, 230);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(12, 14);
            this.label20.TabIndex = 41;
            this.label20.Text = ":";
            // 
            // txt_ES2
            // 
            this.txt_ES2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ES2.Location = new System.Drawing.Point(320, 227);
            this.txt_ES2.Name = "txt_ES2";
            this.txt_ES2.Size = new System.Drawing.Size(32, 22);
            this.txt_ES2.TabIndex = 40;
            this.txt_ES2.Text = "1.5";
            this.txt_ES2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(430, 282);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(179, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "Tensile Strength of Steel [σ_t]";
            // 
            // txt_sigma_t
            // 
            this.txt_sigma_t.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_t.Location = new System.Drawing.Point(724, 273);
            this.txt_sigma_t.Name = "txt_sigma_t";
            this.txt_sigma_t.Size = new System.Drawing.Size(66, 22);
            this.txt_sigma_t.TabIndex = 38;
            this.txt_sigma_t.Text = "250";
            this.txt_sigma_t.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(430, 254);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(284, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "Length of Cross Frame for Lateral Bracking [Llb]";
            // 
            // txt_Llb
            // 
            this.txt_Llb.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Llb.Location = new System.Drawing.Point(724, 245);
            this.txt_Llb.Name = "txt_Llb";
            this.txt_Llb.Size = new System.Drawing.Size(66, 22);
            this.txt_Llb.TabIndex = 36;
            this.txt_Llb.Text = "2.0";
            this.txt_Llb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(430, 223);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(201, 13);
            this.label18.TabIndex = 35;
            this.label18.Text = "Lateral load by racking forces [pl]";
            // 
            // txt_pl
            // 
            this.txt_pl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pl.Location = new System.Drawing.Point(724, 217);
            this.txt_pl.Name = "txt_pl";
            this.txt_pl.Size = new System.Drawing.Size(66, 22);
            this.txt_pl.TabIndex = 34;
            this.txt_pl.Text = "6";
            this.txt_pl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(430, 193);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(290, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Coefficient for Wind load on Leeward Girder [Cw]";
            // 
            // txt_Cw
            // 
            this.txt_Cw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Cw.Location = new System.Drawing.Point(724, 189);
            this.txt_Cw.Name = "txt_Cw";
            this.txt_Cw.Size = new System.Drawing.Size(66, 22);
            this.txt_Cw.TabIndex = 32;
            this.txt_Cw.Text = "0.25";
            this.txt_Cw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(430, 166);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Wind Load [Pw]";
            // 
            // txt_Pw
            // 
            this.txt_Pw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pw.Location = new System.Drawing.Point(724, 161);
            this.txt_Pw.Name = "txt_Pw";
            this.txt_Pw.Size = new System.Drawing.Size(66, 22);
            this.txt_Pw.TabIndex = 30;
            this.txt_Pw.Text = "1.5";
            this.txt_Pw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(430, 142);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(197, 13);
            this.label17.TabIndex = 29;
            this.label17.Text = "Permissible Bearing Stress [PBS]";
            // 
            // txt_PBS
            // 
            this.txt_PBS.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PBS.Location = new System.Drawing.Point(724, 137);
            this.txt_PBS.Name = "txt_PBS";
            this.txt_PBS.Size = new System.Drawing.Size(66, 22);
            this.txt_PBS.TabIndex = 28;
            this.txt_PBS.Text = "189";
            this.txt_PBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(430, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(221, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Constant ‘K’ for angle between plates";
            // 
            // txt_K
            // 
            this.txt_K.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_K.Location = new System.Drawing.Point(724, 109);
            this.txt_K.Name = "txt_K";
            this.txt_K.Size = new System.Drawing.Size(66, 22);
            this.txt_K.TabIndex = 26;
            this.txt_K.Text = "0.7";
            this.txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(430, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(288, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Permissible shear stress through fillet weld [σ_tt]";
            // 
            // txt_sigma_tt
            // 
            this.txt_sigma_tt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_tt.Location = new System.Drawing.Point(724, 83);
            this.txt_sigma_tt.Name = "txt_sigma_tt";
            this.txt_sigma_tt.Size = new System.Drawing.Size(66, 22);
            this.txt_sigma_tt.TabIndex = 24;
            this.txt_sigma_tt.Text = "102";
            this.txt_sigma_tt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(430, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(255, 26);
            this.label12.TabIndex = 23;
            this.label12.Text = "Interval of Cross Bearings as \r\nEffective length of Compression Flange [Lf]";
            // 
            // txt_Lf
            // 
            this.txt_Lf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Lf.Location = new System.Drawing.Point(724, 56);
            this.txt_Lf.Name = "txt_Lf";
            this.txt_Lf.Size = new System.Drawing.Size(66, 22);
            this.txt_Lf.TabIndex = 22;
            this.txt_Lf.Text = "6.0";
            this.txt_Lf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(430, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(253, 26);
            this.label11.TabIndex = 21;
            this.label11.Text = "Average Shear Stress for mild steel\r\nhaving yield stress of 236 N/Sq.mm.  [τ_v]";
            // 
            // txt_tau_v
            // 
            this.txt_tau_v.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tau_v.Location = new System.Drawing.Point(724, 24);
            this.txt_tau_v.Name = "txt_tau_v";
            this.txt_tau_v.Size = new System.Drawing.Size(66, 22);
            this.txt_tau_v.TabIndex = 20;
            this.txt_tau_v.Text = "85";
            this.txt_tau_v.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 280);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Permissible bending stress [σ_b]";
            // 
            // txt_sigma_b
            // 
            this.txt_sigma_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_b.Location = new System.Drawing.Point(285, 276);
            this.txt_sigma_b.Name = "txt_sigma_b";
            this.txt_sigma_b.Size = new System.Drawing.Size(67, 22);
            this.txt_sigma_b.TabIndex = 18;
            this.txt_sigma_b.Text = "141";
            this.txt_sigma_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Width of Abutment [wa]";
            // 
            // txt_wa
            // 
            this.txt_wa.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_wa.Location = new System.Drawing.Point(285, 251);
            this.txt_wa.Name = "txt_wa";
            this.txt_wa.Size = new System.Drawing.Size(67, 22);
            this.txt_wa.TabIndex = 16;
            this.txt_wa.Text = "4.0";
            this.txt_wa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(165, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Embankment side slop [ES]";
            // 
            // txt_ES1
            // 
            this.txt_ES1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ES1.Location = new System.Drawing.Point(278, 227);
            this.txt_ES1.Name = "txt_ES1";
            this.txt_ES1.Size = new System.Drawing.Size(26, 22);
            this.txt_ES1.TabIndex = 14;
            this.txt_ES1.Text = "1";
            this.txt_ES1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Foundation Level [FL]";
            // 
            // txt_FL
            // 
            this.txt_FL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FL.Location = new System.Drawing.Point(285, 202);
            this.txt_FL.Name = "txt_FL";
            this.txt_FL.Size = new System.Drawing.Size(67, 22);
            this.txt_FL.TabIndex = 12;
            this.txt_FL.Text = "100.50";
            this.txt_FL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Top of Rail Level [TRL]";
            // 
            // txt_TRL
            // 
            this.txt_TRL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TRL.Location = new System.Drawing.Point(285, 177);
            this.txt_TRL.Name = "txt_TRL";
            this.txt_TRL.Size = new System.Drawing.Size(67, 22);
            this.txt_TRL.TabIndex = 10;
            this.txt_TRL.Text = "108.0";
            this.txt_TRL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "Equivalent total moving load per\r\ntrack for Shear force Calculation [Ws]";
            // 
            // txt_Ws
            // 
            this.txt_Ws.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ws.Location = new System.Drawing.Point(285, 151);
            this.txt_Ws.Name = "txt_Ws";
            this.txt_Ws.Size = new System.Drawing.Size(67, 22);
            this.txt_Ws.TabIndex = 8;
            this.txt_Ws.Text = "2927";
            this.txt_Ws.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Equivalent Total moving load per \r\ntrack for Bending Moment Calculation [Wm]";
            // 
            // txt_Wm
            // 
            this.txt_Wm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Wm.Location = new System.Drawing.Point(285, 118);
            this.txt_Wm.Name = "txt_Wm";
            this.txt_Wm.Size = new System.Drawing.Size(67, 22);
            this.txt_Wm.TabIndex = 6;
            this.txt_Wm.Text = "2727";
            this.txt_Wm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rail Gauge [G]";
            // 
            // txt_G
            // 
            this.txt_G.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_G.Location = new System.Drawing.Point(285, 86);
            this.txt_G.Name = "txt_G";
            this.txt_G.Size = new System.Drawing.Size(67, 22);
            this.txt_G.TabIndex = 4;
            this.txt_G.Text = "1.676";
            this.txt_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Permanent Load as open Floor [FLL]";
            // 
            // txt_FLL
            // 
            this.txt_FLL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FLL.Location = new System.Drawing.Point(285, 60);
            this.txt_FLL.Name = "txt_FLL";
            this.txt_FLL.Size = new System.Drawing.Size(67, 22);
            this.txt_FLL.TabIndex = 2;
            this.txt_FLL.Text = "7.5";
            this.txt_FLL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Effective Span of Girder [L]";
            // 
            // txt_L
            // 
            this.txt_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L.Location = new System.Drawing.Point(285, 34);
            this.txt_L.Name = "txt_L";
            this.txt_L.Size = new System.Drawing.Size(67, 22);
            this.txt_L.TabIndex = 0;
            this.txt_L.Text = "30";
            this.txt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pic_rail2
            // 
            this.pic_rail2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_rail2.Location = new System.Drawing.Point(439, 401);
            this.pic_rail2.Name = "pic_rail2";
            this.pic_rail2.Size = new System.Drawing.Size(436, 191);
            this.pic_rail2.TabIndex = 7;
            this.pic_rail2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_open_des);
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(154, 614);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(53, 10);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // btn_open_des
            // 
            this.btn_open_des.Location = new System.Drawing.Point(21, -1);
            this.btn_open_des.Name = "btn_open_des";
            this.btn_open_des.Size = new System.Drawing.Size(293, 34);
            this.btn_open_des.TabIndex = 117;
            this.btn_open_des.Text = "Open Previous Design [\"ASTRA_Data_Input.txt\"]";
            this.btn_open_des.UseVisualStyleBackColor = true;
            this.btn_open_des.Click += new System.EventHandler(this.btn_open_des_Click);
            // 
            // pic_rail1
            // 
            this.pic_rail1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_rail1.Location = new System.Drawing.Point(16, 401);
            this.pic_rail1.Name = "pic_rail1";
            this.pic_rail1.Size = new System.Drawing.Size(410, 191);
            this.pic_rail1.TabIndex = 6;
            this.pic_rail1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label143);
            this.tabPage2.Controls.Add(this.btn_dwg_plate);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(907, 632);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Drawing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(133, 208);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(513, 16);
            this.label143.TabIndex = 7;
            this.label143.Text = "Button is Enabled Once the Steel Plate Girder Railway Bridge Design is done.";
            // 
            // btn_dwg_plate
            // 
            this.btn_dwg_plate.Location = new System.Drawing.Point(217, 238);
            this.btn_dwg_plate.Name = "btn_dwg_plate";
            this.btn_dwg_plate.Size = new System.Drawing.Size(367, 88);
            this.btn_dwg_plate.TabIndex = 0;
            this.btn_dwg_plate.Text = "Steel Plate Girder Railway Bridge Drawing";
            this.btn_dwg_plate.UseVisualStyleBackColor = true;
            this.btn_dwg_plate.Click += new System.EventHandler(this.btn_Plate_Drawing_Click);
            // 
            // frm_Railway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 658);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Railway";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Railway";
            this.Load += new System.EventHandler(this.frm_Railway_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rail2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_rail1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txt_tw;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txt_Dw;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_ES2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_sigma_t;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_Llb;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_pl;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_Cw;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Pw;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_PBS;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_K;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_sigma_tt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Lf;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_tau_v;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_sigma_b;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_wa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ES1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_FL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_TRL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Ws;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Wm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_G;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_FLL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_L;
        private System.Windows.Forms.PictureBox pic_rail2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.PictureBox pic_rail1;
        private System.Windows.Forms.Button btn_dwg_plate;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label211;
        private System.Windows.Forms.Label label212;
        private System.Windows.Forms.Button btn_open_des;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_browse_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label830;
    }
}