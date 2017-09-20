namespace AstraFunctionOne.BridgeDesign
{
    partial class frmCompositeBridge
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
            tmrComp.Stop();
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
            this.components = new System.ComponentModel.Container();
            this.btnWorkingFolder = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnDrawing = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_na = new System.Windows.Forms.ComboBox();
            this.cmb_ang_thk = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.cmb_ang = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.txt_des_shr = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txt_des_mom = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.rbtn_outer_girder = new System.Windows.Forms.RadioButton();
            this.rbtn_inner_girder = new System.Windows.Forms.RadioButton();
            this.label64 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.txt_nf = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txt_tf = new System.Windows.Forms.TextBox();
            this.txt_bf = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.txt_off = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txt_nw = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txt_tw = new System.Windows.Forms.TextBox();
            this.txt_dw = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_sigma_p = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_sigma_tf = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_K = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_sigma_b = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_tau = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_j = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_sigma_st = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_Q = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_IF = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_CF = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_v = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_u = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_gamma_wc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_WL = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_L = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Dwc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_gamma_c = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_D = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_YS = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_m = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_B = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_fck = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_fy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_B2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_B1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_S = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.tmrComp = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWorkingFolder
            // 
            this.btnWorkingFolder.Location = new System.Drawing.Point(353, 2);
            this.btnWorkingFolder.Name = "btnWorkingFolder";
            this.btnWorkingFolder.Size = new System.Drawing.Size(88, 23);
            this.btnWorkingFolder.TabIndex = 0;
            this.btnWorkingFolder.Text = "Working Folder";
            this.btnWorkingFolder.UseVisualStyleBackColor = true;
            this.btnWorkingFolder.Click += new System.EventHandler(this.btnWorkingFolder_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(447, 2);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnReport
            // 
            this.btnReport.Enabled = false;
            this.btnReport.Location = new System.Drawing.Point(528, 2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnDrawing
            // 
            this.btnDrawing.Enabled = false;
            this.btnDrawing.Location = new System.Drawing.Point(627, 2);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(111, 23);
            this.btnDrawing.TabIndex = 3;
            this.btnDrawing.Text = "Default Drawing";
            this.btnDrawing.UseVisualStyleBackColor = true;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(744, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_na);
            this.groupBox1.Controls.Add(this.cmb_ang_thk);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Controls.Add(this.cmb_ang);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Controls.Add(this.txt_des_shr);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.txt_des_mom);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.rbtn_outer_girder);
            this.groupBox1.Controls.Add(this.rbtn_inner_girder);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.label65);
            this.groupBox1.Controls.Add(this.label63);
            this.groupBox1.Controls.Add(this.label61);
            this.groupBox1.Controls.Add(this.txt_nf);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Controls.Add(this.txt_tf);
            this.groupBox1.Controls.Add(this.txt_bf);
            this.groupBox1.Controls.Add(this.label59);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.txt_off);
            this.groupBox1.Controls.Add(this.label55);
            this.groupBox1.Controls.Add(this.txt_nw);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.txt_tw);
            this.groupBox1.Controls.Add(this.txt_dw);
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.pb1);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txt_sigma_p);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.txt_sigma_tf);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txt_K);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.txt_sigma_b);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_tau);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txt_j);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txt_sigma_st);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txt_Q);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.txt_IF);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txt_CF);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txt_v);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt_u);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txt_gamma_wc);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_WL);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_L);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_Dwc);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_gamma_c);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_D);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_YS);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_m);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_B);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_fck);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_fy);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_B2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_B1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_S);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(833, 562);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER\'S Data";
            // 
            // txt_na
            // 
            this.txt_na.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_na.FormattingEnabled = true;
            this.txt_na.Items.AddRange(new object[] {
            "0",
            "4",
            "8"});
            this.txt_na.Location = new System.Drawing.Point(179, 293);
            this.txt_na.Name = "txt_na";
            this.txt_na.Size = new System.Drawing.Size(72, 21);
            this.txt_na.TabIndex = 91;
            // 
            // cmb_ang_thk
            // 
            this.cmb_ang_thk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ang_thk.FormattingEnabled = true;
            this.cmb_ang_thk.Location = new System.Drawing.Point(306, 264);
            this.cmb_ang_thk.Name = "cmb_ang_thk";
            this.cmb_ang_thk.Size = new System.Drawing.Size(54, 22);
            this.cmb_ang_thk.TabIndex = 90;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(145, 267);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(30, 13);
            this.label58.TabIndex = 88;
            this.label58.Text = "ISA";
            // 
            // cmb_ang
            // 
            this.cmb_ang.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ang.FormattingEnabled = true;
            this.cmb_ang.Location = new System.Drawing.Point(179, 264);
            this.cmb_ang.Name = "cmb_ang";
            this.cmb_ang.Size = new System.Drawing.Size(102, 22);
            this.cmb_ang.TabIndex = 87;
            this.cmb_ang.SelectedIndexChanged += new System.EventHandler(this.cmb_ang_SelectedIndexChanged);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(257, 72);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(24, 13);
            this.label56.TabIndex = 86;
            this.label56.Text = "kN";
            // 
            // txt_des_shr
            // 
            this.txt_des_shr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_des_shr.Location = new System.Drawing.Point(179, 69);
            this.txt_des_shr.Name = "txt_des_shr";
            this.txt_des_shr.Size = new System.Drawing.Size(72, 22);
            this.txt_des_shr.TabIndex = 85;
            this.txt_des_shr.Text = "2215";
            this.txt_des_shr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(9, 72);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(71, 13);
            this.label57.TabIndex = 84;
            this.label57.Text = "Design Shear";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(257, 43);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(42, 13);
            this.label49.TabIndex = 83;
            this.label49.Text = "kN-m";
            // 
            // txt_des_mom
            // 
            this.txt_des_mom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_des_mom.Location = new System.Drawing.Point(179, 40);
            this.txt_des_mom.Name = "txt_des_mom";
            this.txt_des_mom.Size = new System.Drawing.Size(72, 22);
            this.txt_des_mom.TabIndex = 82;
            this.txt_des_mom.Text = "3890";
            this.txt_des_mom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(9, 43);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(81, 13);
            this.label52.TabIndex = 81;
            this.label52.Text = "Design Moment";
            // 
            // rbtn_outer_girder
            // 
            this.rbtn_outer_girder.AutoSize = true;
            this.rbtn_outer_girder.Location = new System.Drawing.Point(110, 20);
            this.rbtn_outer_girder.Name = "rbtn_outer_girder";
            this.rbtn_outer_girder.Size = new System.Drawing.Size(82, 17);
            this.rbtn_outer_girder.TabIndex = 80;
            this.rbtn_outer_girder.Text = "Outer Girder";
            this.rbtn_outer_girder.UseVisualStyleBackColor = true;
            this.rbtn_outer_girder.CheckedChanged += new System.EventHandler(this.rbtn_inner_girder_CheckedChanged);
            // 
            // rbtn_inner_girder
            // 
            this.rbtn_inner_girder.AutoSize = true;
            this.rbtn_inner_girder.Checked = true;
            this.rbtn_inner_girder.Location = new System.Drawing.Point(12, 20);
            this.rbtn_inner_girder.Name = "rbtn_inner_girder";
            this.rbtn_inner_girder.Size = new System.Drawing.Size(80, 17);
            this.rbtn_inner_girder.TabIndex = 80;
            this.rbtn_inner_girder.TabStop = true;
            this.rbtn_inner_girder.Text = "Inner Girder";
            this.rbtn_inner_girder.UseVisualStyleBackColor = true;
            this.rbtn_inner_girder.CheckedChanged += new System.EventHandler(this.rbtn_inner_girder_CheckedChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(9, 296);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(112, 13);
            this.label64.TabIndex = 78;
            this.label64.Text = "Number of Angles [na]";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(257, 184);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(15, 13);
            this.label51.TabIndex = 76;
            this.label51.Text = "x";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(257, 129);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(15, 13);
            this.label65.TabIndex = 76;
            this.label65.Text = "x";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(288, 268);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(15, 13);
            this.label63.TabIndex = 76;
            this.label63.Text = "x";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(9, 268);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(96, 13);
            this.label61.TabIndex = 72;
            this.label61.Text = "Connecting Angles";
            // 
            // txt_nf
            // 
            this.txt_nf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nf.Location = new System.Drawing.Point(179, 236);
            this.txt_nf.Name = "txt_nf";
            this.txt_nf.Size = new System.Drawing.Size(72, 22);
            this.txt_nf.TabIndex = 71;
            this.txt_nf.Text = "2";
            this.txt_nf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(9, 239);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(141, 13);
            this.label60.TabIndex = 70;
            this.label60.Text = "Number of Flange Plates [nf]";
            // 
            // txt_tf
            // 
            this.txt_tf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tf.Location = new System.Drawing.Point(278, 180);
            this.txt_tf.Name = "txt_tf";
            this.txt_tf.Size = new System.Drawing.Size(54, 22);
            this.txt_tf.TabIndex = 68;
            this.txt_tf.Text = "20";
            this.txt_tf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_bf
            // 
            this.txt_bf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bf.Location = new System.Drawing.Point(179, 181);
            this.txt_bf.Name = "txt_bf";
            this.txt_bf.Size = new System.Drawing.Size(72, 22);
            this.txt_bf.TabIndex = 65;
            this.txt_bf.Text = "600";
            this.txt_bf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(9, 184);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(119, 13);
            this.label59.TabIndex = 64;
            this.label59.Text = "Size of Flange Plate [bf]";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(257, 212);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(31, 13);
            this.label53.TabIndex = 63;
            this.label53.Text = "mm";
            // 
            // txt_off
            // 
            this.txt_off.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_off.Location = new System.Drawing.Point(179, 209);
            this.txt_off.Name = "txt_off";
            this.txt_off.Size = new System.Drawing.Size(72, 22);
            this.txt_off.TabIndex = 62;
            this.txt_off.Text = "100";
            this.txt_off.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(9, 213);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(107, 13);
            this.label55.TabIndex = 61;
            this.label55.Text = "Offset from Edge [off]";
            // 
            // txt_nw
            // 
            this.txt_nw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nw.Location = new System.Drawing.Point(179, 153);
            this.txt_nw.Name = "txt_nw";
            this.txt_nw.Size = new System.Drawing.Size(72, 22);
            this.txt_nw.TabIndex = 60;
            this.txt_nw.Text = "1";
            this.txt_nw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(9, 157);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(137, 13);
            this.label54.TabIndex = 59;
            this.label54.Text = "Number of Web Plates [nw]";
            // 
            // txt_tw
            // 
            this.txt_tw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tw.Location = new System.Drawing.Point(278, 125);
            this.txt_tw.Name = "txt_tw";
            this.txt_tw.Size = new System.Drawing.Size(54, 22);
            this.txt_tw.TabIndex = 57;
            this.txt_tw.Text = "20";
            this.txt_tw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_dw
            // 
            this.txt_dw.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dw.Location = new System.Drawing.Point(179, 125);
            this.txt_dw.Name = "txt_dw";
            this.txt_dw.Size = new System.Drawing.Size(72, 22);
            this.txt_dw.TabIndex = 54;
            this.txt_dw.Text = "1500";
            this.txt_dw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(9, 129);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(115, 13);
            this.label50.TabIndex = 53;
            this.label50.Text = "Size of Web Plate [dw]";
            // 
            // pb1
            // 
            this.pb1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.DCP_3935;
            this.pb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb1.Location = new System.Drawing.Point(423, 8);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(315, 142);
            this.pb1.TabIndex = 7;
            this.pb1.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(637, 384);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(23, 14);
            this.label32.TabIndex = 52;
            this.label32.Text = "Fe";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(638, 358);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(18, 14);
            this.label31.TabIndex = 52;
            this.label31.Text = "M";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(263, 487);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(24, 13);
            this.label41.TabIndex = 52;
            this.label41.Text = "kN";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(750, 334);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(63, 13);
            this.label40.TabIndex = 52;
            this.label40.Text = "kN/cu.m";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(750, 308);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(63, 13);
            this.label39.TabIndex = 52;
            this.label39.Text = "kN/cu.m";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(257, 404);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 13);
            this.label38.TabIndex = 52;
            this.label38.Text = "mm";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(257, 322);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(31, 13);
            this.label36.TabIndex = 52;
            this.label36.Text = "mm";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(750, 160);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(67, 13);
            this.label35.TabIndex = 52;
            this.label35.Text = "N/sq.mm";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(750, 385);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 13);
            this.label34.TabIndex = 52;
            this.label34.Text = "N/sq.mm";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(750, 359);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(67, 13);
            this.label33.TabIndex = 52;
            this.label33.Text = "N/sq.mm";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(263, 539);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(19, 13);
            this.label43.TabIndex = 52;
            this.label43.Text = "m";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(263, 513);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(19, 13);
            this.label42.TabIndex = 52;
            this.label42.Text = "m";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(257, 459);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(19, 13);
            this.label37.TabIndex = 52;
            this.label37.Text = "m";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(257, 432);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(19, 13);
            this.label30.TabIndex = 52;
            this.label30.Text = "m";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(257, 376);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(19, 13);
            this.label29.TabIndex = 52;
            this.label29.Text = "m";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(257, 348);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(19, 13);
            this.label28.TabIndex = 52;
            this.label28.Text = "m";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(750, 286);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(67, 13);
            this.label48.TabIndex = 52;
            this.label48.Text = "N/sq.mm";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(750, 261);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(67, 13);
            this.label47.TabIndex = 52;
            this.label47.Text = "N/sq.mm";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(750, 235);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(67, 13);
            this.label46.TabIndex = 52;
            this.label46.Text = "N/sq.mm";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(750, 209);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(67, 13);
            this.label45.TabIndex = 52;
            this.label45.Text = "N/sq.mm";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(750, 186);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(67, 13);
            this.label44.TabIndex = 52;
            this.label44.Text = "N/sq.mm";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(257, 100);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(19, 13);
            this.label27.TabIndex = 52;
            this.label27.Text = "m";
            // 
            // txt_sigma_p
            // 
            this.txt_sigma_p.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_p.Location = new System.Drawing.Point(666, 281);
            this.txt_sigma_p.Name = "txt_sigma_p";
            this.txt_sigma_p.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_p.TabIndex = 25;
            this.txt_sigma_p.Text = "189";
            this.txt_sigma_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(420, 284);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(158, 13);
            this.label26.TabIndex = 50;
            this.label26.Text = "Permissible Bearing Stress [σ_p]";
            // 
            // txt_sigma_tf
            // 
            this.txt_sigma_tf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_tf.Location = new System.Drawing.Point(666, 255);
            this.txt_sigma_tf.Name = "txt_sigma_tf";
            this.txt_sigma_tf.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_tf.TabIndex = 23;
            this.txt_sigma_tf.Text = "102";
            this.txt_sigma_tf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(420, 258);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(238, 13);
            this.label24.TabIndex = 48;
            this.label24.Text = "Permissible Shear Stress through fillet Weld [σ_tf]";
            // 
            // txt_K
            // 
            this.txt_K.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_K.Location = new System.Drawing.Point(666, 534);
            this.txt_K.Name = "txt_K";
            this.txt_K.Size = new System.Drawing.Size(72, 22);
            this.txt_K.TabIndex = 24;
            this.txt_K.Text = "0.7";
            this.txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(420, 538);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 13);
            this.label25.TabIndex = 46;
            this.label25.Text = "Constant ‘K’";
            // 
            // txt_sigma_b
            // 
            this.txt_sigma_b.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_b.Location = new System.Drawing.Point(666, 204);
            this.txt_sigma_b.Name = "txt_sigma_b";
            this.txt_sigma_b.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_b.TabIndex = 21;
            this.txt_sigma_b.Text = "165";
            this.txt_sigma_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(420, 207);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(199, 13);
            this.label19.TabIndex = 44;
            this.label19.Text = "Permissible Bending Stress in Steel [σ_b]";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_tau
            // 
            this.txt_tau.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tau.Location = new System.Drawing.Point(666, 230);
            this.txt_tau.Name = "txt_tau";
            this.txt_tau.Size = new System.Drawing.Size(72, 22);
            this.txt_tau.TabIndex = 22;
            this.txt_tau.Text = "85";
            this.txt_tau.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(420, 233);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(175, 13);
            this.label20.TabIndex = 42;
            this.label20.Text = "Permissible Shear Stress in Steel [τ]";
            // 
            // txt_j
            // 
            this.txt_j.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_j.Location = new System.Drawing.Point(666, 458);
            this.txt_j.Name = "txt_j";
            this.txt_j.Size = new System.Drawing.Size(72, 22);
            this.txt_j.TabIndex = 19;
            this.txt_j.Text = "0.91";
            this.txt_j.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(420, 466);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(131, 14);
            this.label21.TabIndex = 40;
            this.label21.Text = "Lever Arm Factor [j]";
            // 
            // txt_sigma_st
            // 
            this.txt_sigma_st.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_st.Location = new System.Drawing.Point(666, 181);
            this.txt_sigma_st.Name = "txt_sigma_st";
            this.txt_sigma_st.Size = new System.Drawing.Size(72, 22);
            this.txt_sigma_st.TabIndex = 20;
            this.txt_sigma_st.Text = "200";
            this.txt_sigma_st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(420, 184);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(228, 15);
            this.label22.TabIndex = 38;
            this.label22.Text = "Permissible tensile stress in steel [σ_st]";
            // 
            // txt_Q
            // 
            this.txt_Q.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Q.Location = new System.Drawing.Point(666, 431);
            this.txt_Q.Name = "txt_Q";
            this.txt_Q.Size = new System.Drawing.Size(72, 22);
            this.txt_Q.TabIndex = 18;
            this.txt_Q.Text = "0.762";
            this.txt_Q.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(420, 438);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(108, 15);
            this.label23.TabIndex = 36;
            this.label23.Text = "Moment Factor [Q]";
            // 
            // txt_IF
            // 
            this.txt_IF.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IF.Location = new System.Drawing.Point(666, 509);
            this.txt_IF.Name = "txt_IF";
            this.txt_IF.Size = new System.Drawing.Size(72, 22);
            this.txt_IF.TabIndex = 16;
            this.txt_IF.Text = "1.25";
            this.txt_IF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(420, 513);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Impact Factor [IF]";
            // 
            // txt_CF
            // 
            this.txt_CF.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CF.Location = new System.Drawing.Point(666, 483);
            this.txt_CF.Name = "txt_CF";
            this.txt_CF.Size = new System.Drawing.Size(72, 22);
            this.txt_CF.TabIndex = 17;
            this.txt_CF.Text = "0.8";
            this.txt_CF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(420, 487);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Continuity Factor [CF]";
            // 
            // txt_v
            // 
            this.txt_v.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_v.Location = new System.Drawing.Point(179, 508);
            this.txt_v.Name = "txt_v";
            this.txt_v.Size = new System.Drawing.Size(72, 22);
            this.txt_v.TabIndex = 14;
            this.txt_v.Text = "3.6";
            this.txt_v.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 512);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(130, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Length of Loaded area [v]";
            // 
            // txt_u
            // 
            this.txt_u.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_u.Location = new System.Drawing.Point(179, 534);
            this.txt_u.Name = "txt_u";
            this.txt_u.Size = new System.Drawing.Size(72, 22);
            this.txt_u.TabIndex = 15;
            this.txt_u.Text = "0.85";
            this.txt_u.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 538);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Width of Loaded area [u]";
            // 
            // txt_gamma_wc
            // 
            this.txt_gamma_wc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_wc.Location = new System.Drawing.Point(666, 330);
            this.txt_gamma_wc.Name = "txt_gamma_wc";
            this.txt_gamma_wc.Size = new System.Drawing.Size(72, 22);
            this.txt_gamma_wc.TabIndex = 12;
            this.txt_gamma_wc.Text = "22";
            this.txt_gamma_wc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(420, 334);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(186, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Unit Weight of wearing cource [γ_wc]";
            // 
            // txt_WL
            // 
            this.txt_WL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_WL.Location = new System.Drawing.Point(179, 482);
            this.txt_WL.Name = "txt_WL";
            this.txt_WL.Size = new System.Drawing.Size(72, 22);
            this.txt_WL.TabIndex = 13;
            this.txt_WL.Text = "350";
            this.txt_WL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 486);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Tracked Vehicle Load [WL]";
            // 
            // txt_L
            // 
            this.txt_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L.Location = new System.Drawing.Point(179, 456);
            this.txt_L.Name = "txt_L";
            this.txt_L.Size = new System.Drawing.Size(72, 22);
            this.txt_L.TabIndex = 9;
            this.txt_L.Text = "4.5";
            this.txt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 459);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Panel Length [L]";
            // 
            // txt_Dwc
            // 
            this.txt_Dwc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Dwc.Location = new System.Drawing.Point(179, 401);
            this.txt_Dwc.Name = "txt_Dwc";
            this.txt_Dwc.Size = new System.Drawing.Size(72, 22);
            this.txt_Dwc.TabIndex = 10;
            this.txt_Dwc.Text = "80";
            this.txt_Dwc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 404);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Wearing Course Thickness [Dwc]";
            // 
            // txt_gamma_c
            // 
            this.txt_gamma_c.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gamma_c.Location = new System.Drawing.Point(666, 304);
            this.txt_gamma_c.Name = "txt_gamma_c";
            this.txt_gamma_c.Size = new System.Drawing.Size(72, 22);
            this.txt_gamma_c.TabIndex = 11;
            this.txt_gamma_c.Text = "24";
            this.txt_gamma_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(420, 308);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Unit Weight of Concrete [γ_c]";
            // 
            // txt_D
            // 
            this.txt_D.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_D.Location = new System.Drawing.Point(179, 319);
            this.txt_D.Name = "txt_D";
            this.txt_D.Size = new System.Drawing.Size(72, 22);
            this.txt_D.TabIndex = 8;
            this.txt_D.Text = "300";
            this.txt_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 322);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "SLAB Thickness [D]";
            // 
            // txt_YS
            // 
            this.txt_YS.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_YS.Location = new System.Drawing.Point(666, 156);
            this.txt_YS.Name = "txt_YS";
            this.txt_YS.Size = new System.Drawing.Size(72, 22);
            this.txt_YS.TabIndex = 7;
            this.txt_YS.Text = "236";
            this.txt_YS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YS.TextAlignChanged += new System.EventHandler(this.txt_fck_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(420, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(196, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Rolled Steel Section of Yield Stress [YS]";
            // 
            // txt_m
            // 
            this.txt_m.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_m.Location = new System.Drawing.Point(666, 406);
            this.txt_m.Name = "txt_m";
            this.txt_m.Size = new System.Drawing.Size(72, 22);
            this.txt_m.TabIndex = 6;
            this.txt_m.Text = "10";
            this.txt_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_m.TextChanged += new System.EventHandler(this.txt_fck_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(420, 415);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Modular Ratio [m]";
            // 
            // txt_B
            // 
            this.txt_B.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_B.Location = new System.Drawing.Point(179, 429);
            this.txt_B.Name = "txt_B";
            this.txt_B.Size = new System.Drawing.Size(72, 22);
            this.txt_B.TabIndex = 3;
            this.txt_B.Text = "2.0";
            this.txt_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 432);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Spacing of Main Girders [B]";
            // 
            // txt_fck
            // 
            this.txt_fck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fck.Location = new System.Drawing.Point(666, 355);
            this.txt_fck.Name = "txt_fck";
            this.txt_fck.Size = new System.Drawing.Size(72, 22);
            this.txt_fck.TabIndex = 4;
            this.txt_fck.Text = "30";
            this.txt_fck.TextChanged += new System.EventHandler(this.txt_fck_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(420, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Concrete Grade [fck]";
            // 
            // txt_fy
            // 
            this.txt_fy.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fy.Location = new System.Drawing.Point(666, 381);
            this.txt_fy.Name = "txt_fy";
            this.txt_fy.Size = new System.Drawing.Size(72, 22);
            this.txt_fy.TabIndex = 5;
            this.txt_fy.Text = "415";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(420, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Reinforcement Steel Frade [fy]";
            // 
            // txt_B2
            // 
            this.txt_B2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_B2.Location = new System.Drawing.Point(179, 373);
            this.txt_B2.Name = "txt_B2";
            this.txt_B2.Size = new System.Drawing.Size(72, 22);
            this.txt_B2.TabIndex = 2;
            this.txt_B2.Text = "1";
            this.txt_B2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Footpath Width [B2]";
            // 
            // txt_B1
            // 
            this.txt_B1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_B1.Location = new System.Drawing.Point(179, 345);
            this.txt_B1.Name = "txt_B1";
            this.txt_B1.Size = new System.Drawing.Size(72, 22);
            this.txt_B1.TabIndex = 1;
            this.txt_B1.Text = "7.5";
            this.txt_B1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Carriageway Width [B1]";
            // 
            // txt_S
            // 
            this.txt_S.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_S.Location = new System.Drawing.Point(179, 97);
            this.txt_S.Name = "txt_S";
            this.txt_S.Size = new System.Drawing.Size(72, 22);
            this.txt_S.TabIndex = 0;
            this.txt_S.TabStop = false;
            this.txt_S.Text = "46";
            this.txt_S.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bridge Span [S]";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDrawing);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Controls.Add(this.btnWorkingFolder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 595);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 28);
            this.panel1.TabIndex = 0;
            // 
            // tmrComp
            // 
            this.tmrComp.Enabled = true;
            this.tmrComp.Interval = 2000;
            this.tmrComp.Tick += new System.EventHandler(this.tmrComp_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(864, 623);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(856, 597);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(856, 597);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Example Dimensions";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Composite_Bridge_Image01;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-4, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(811, 448);
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // frmCompositeBridge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 623);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompositeBridge";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design of Composite Bridge";
            this.Load += new System.EventHandler(this.frmCompositeBridge_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWorkingFolder;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnDrawing;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_L;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Dwc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_gamma_c;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_D;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_YS;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_m;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_B;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_fck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_fy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_B2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_B1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_S;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_IF;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_CF;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_v;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_u;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_gamma_wc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_WL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_sigma_b;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_tau;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_j;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_sigma_st;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_Q;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_sigma_tf;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_K;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_sigma_p;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.Timer tmrComp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_dw;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txt_nw;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txt_tw;
        private System.Windows.Forms.TextBox txt_tf;
        private System.Windows.Forms.TextBox txt_bf;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txt_off;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txt_nf;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txt_des_shr;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txt_des_mom;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.RadioButton rbtn_outer_girder;
        private System.Windows.Forms.RadioButton rbtn_inner_girder;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cmb_ang;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.ComboBox cmb_ang_thk;
        private System.Windows.Forms.ComboBox txt_na;
    }
}