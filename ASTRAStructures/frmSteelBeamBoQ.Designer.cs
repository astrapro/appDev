namespace ASTRAStructures
{
    partial class frmSteelBeamBoQ
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_beams = new System.Windows.Forms.DataGridView();
            this.cmb_flr_lvl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_sele_all = new System.Windows.Forms.CheckBox();
            this.btn_oprn_report = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_ana_input_data = new System.Windows.Forms.Button();
            this.uC_SteelSections1 = new ASTRAStructures.UC_SteelSections();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_beam_data = new System.Windows.Forms.Button();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label422 = new System.Windows.Forms.Label();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.label421 = new System.Windows.Forms.Label();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.label420 = new System.Windows.Forms.Label();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.label419 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label425 = new System.Windows.Forms.Label();
            this.label424 = new System.Windows.Forms.Label();
            this.label429 = new System.Windows.Forms.Label();
            this.label428 = new System.Windows.Forms.Label();
            this.label427 = new System.Windows.Forms.Label();
            this.label426 = new System.Windows.Forms.Label();
            this.label423 = new System.Windows.Forms.Label();
            this.label418 = new System.Windows.Forms.Label();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label417 = new System.Windows.Forms.Label();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.label416 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beams)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_beams);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 209);
            this.panel1.TabIndex = 0;
            // 
            // dgv_beams
            // 
            this.dgv_beams.AllowUserToAddRows = false;
            this.dgv_beams.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_beams.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_beams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_beams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12,
            this.Column1,
            this.Column13,
            this.Column2,
            this.Column3,
            this.Column6,
            this.Column7,
            this.Column16,
            this.Column21,
            this.Column15});
            this.dgv_beams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_beams.Location = new System.Drawing.Point(0, 0);
            this.dgv_beams.Name = "dgv_beams";
            this.dgv_beams.RowHeadersWidth = 27;
            this.dgv_beams.Size = new System.Drawing.Size(909, 209);
            this.dgv_beams.TabIndex = 0;
            this.dgv_beams.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_beams_CellEnter);
            // 
            // cmb_flr_lvl
            // 
            this.cmb_flr_lvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_flr_lvl.FormattingEnabled = true;
            this.cmb_flr_lvl.Location = new System.Drawing.Point(834, 22);
            this.cmb_flr_lvl.Name = "cmb_flr_lvl";
            this.cmb_flr_lvl.Size = new System.Drawing.Size(65, 21);
            this.cmb_flr_lvl.TabIndex = 1;
            this.cmb_flr_lvl.Visible = false;
            this.cmb_flr_lvl.SelectedIndexChanged += new System.EventHandler(this.cmb_flr_lvl_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(828, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Floor Level";
            this.label1.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_OK.Location = new System.Drawing.Point(402, 2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(133, 41);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "Process Design";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_cancel.Location = new System.Drawing.Point(689, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(133, 41);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(905, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "m";
            this.label2.Visible = false;
            // 
            // cmb_sele_all
            // 
            this.cmb_sele_all.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmb_sele_all.AutoSize = true;
            this.cmb_sele_all.Location = new System.Drawing.Point(22, 15);
            this.cmb_sele_all.Name = "cmb_sele_all";
            this.cmb_sele_all.Size = new System.Drawing.Size(79, 17);
            this.cmb_sele_all.TabIndex = 6;
            this.cmb_sele_all.Text = "Select All";
            this.cmb_sele_all.UseVisualStyleBackColor = true;
            this.cmb_sele_all.CheckedChanged += new System.EventHandler(this.cmb_sele_all_CheckedChanged);
            // 
            // btn_oprn_report
            // 
            this.btn_oprn_report.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_oprn_report.Location = new System.Drawing.Point(541, 2);
            this.btn_oprn_report.Name = "btn_oprn_report";
            this.btn_oprn_report.Size = new System.Drawing.Size(133, 41);
            this.btn_oprn_report.TabIndex = 16;
            this.btn_oprn_report.Text = "Open Reports for Selected Members";
            this.btn_oprn_report.UseVisualStyleBackColor = true;
            this.btn_oprn_report.Click += new System.EventHandler(this.btn_oprn_report_Click);
            // 
            // btn_Modify
            // 
            this.btn_Modify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Modify.Location = new System.Drawing.Point(107, 2);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(133, 41);
            this.btn_Modify.TabIndex = 16;
            this.btn_Modify.Text = "Modify Values in Selected Members";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_ana_input_data
            // 
            this.btn_ana_input_data.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ana_input_data.Location = new System.Drawing.Point(246, 2);
            this.btn_ana_input_data.Name = "btn_ana_input_data";
            this.btn_ana_input_data.Size = new System.Drawing.Size(133, 41);
            this.btn_ana_input_data.TabIndex = 16;
            this.btn_ana_input_data.Text = "Update Analysis Input Data";
            this.btn_ana_input_data.UseVisualStyleBackColor = true;
            this.btn_ana_input_data.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // uC_SteelSections1
            // 
            this.uC_SteelSections1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_SteelSections1.Location = new System.Drawing.Point(367, 11);
            this.uC_SteelSections1.Name = "uC_SteelSections1";
            this.uC_SteelSections1.Size = new System.Drawing.Size(531, 350);
            this.uC_SteelSections1.TabIndex = 17;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.btn_beam_data);
            this.splitContainer1.Panel1.Controls.Add(this.textBox42);
            this.splitContainer1.Panel1.Controls.Add(this.label422);
            this.splitContainer1.Panel1.Controls.Add(this.textBox41);
            this.splitContainer1.Panel1.Controls.Add(this.label421);
            this.splitContainer1.Panel1.Controls.Add(this.textBox40);
            this.splitContainer1.Panel1.Controls.Add(this.label420);
            this.splitContainer1.Panel1.Controls.Add(this.textBox39);
            this.splitContainer1.Panel1.Controls.Add(this.label419);
            this.splitContainer1.Panel1.Controls.Add(this.textBox38);
            this.splitContainer1.Panel1.Controls.Add(this.label425);
            this.splitContainer1.Panel1.Controls.Add(this.label424);
            this.splitContainer1.Panel1.Controls.Add(this.label429);
            this.splitContainer1.Panel1.Controls.Add(this.label428);
            this.splitContainer1.Panel1.Controls.Add(this.label427);
            this.splitContainer1.Panel1.Controls.Add(this.label426);
            this.splitContainer1.Panel1.Controls.Add(this.label423);
            this.splitContainer1.Panel1.Controls.Add(this.label418);
            this.splitContainer1.Panel1.Controls.Add(this.textBox37);
            this.splitContainer1.Panel1.Controls.Add(this.label417);
            this.splitContainer1.Panel1.Controls.Add(this.textBox36);
            this.splitContainer1.Panel1.Controls.Add(this.label416);
            this.splitContainer1.Panel1.Controls.Add(this.uC_SteelSections1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(911, 439);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 18;
            // 
            // btn_beam_data
            // 
            this.btn_beam_data.Location = new System.Drawing.Point(67, 247);
            this.btn_beam_data.Name = "btn_beam_data";
            this.btn_beam_data.Size = new System.Drawing.Size(188, 47);
            this.btn_beam_data.TabIndex = 89;
            this.btn_beam_data.Text = "Update Selected Beam Data";
            this.btn_beam_data.UseVisualStyleBackColor = true;
            this.btn_beam_data.Click += new System.EventHandler(this.btn_beam_data_Click);
            // 
            // textBox42
            // 
            this.textBox42.Location = new System.Drawing.Point(227, 183);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(62, 21);
            this.textBox42.TabIndex = 87;
            this.textBox42.Text = "187.5";
            this.textBox42.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label422
            // 
            this.label422.AutoSize = true;
            this.label422.Location = new System.Drawing.Point(11, 189);
            this.label422.Name = "label422";
            this.label422.Size = new System.Drawing.Size(210, 13);
            this.label422.TabIndex = 80;
            this.label422.Text = "Permissible Bearing Stress = Pbs =";
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(227, 158);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(62, 21);
            this.textBox41.TabIndex = 84;
            this.textBox41.Text = "100";
            this.textBox41.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label421
            // 
            this.label421.AutoSize = true;
            this.label421.Location = new System.Drawing.Point(11, 164);
            this.label421.Name = "label421";
            this.label421.Size = new System.Drawing.Size(199, 13);
            this.label421.TabIndex = 79;
            this.label421.Text = "Permissible Shear Stress = Pss =";
            // 
            // textBox40
            // 
            this.textBox40.Location = new System.Drawing.Point(227, 134);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(62, 21);
            this.textBox40.TabIndex = 83;
            this.textBox40.Text = "165";
            this.textBox40.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label420
            // 
            this.label420.AutoSize = true;
            this.label420.Location = new System.Drawing.Point(11, 140);
            this.label420.Name = "label420";
            this.label420.Size = new System.Drawing.Size(216, 13);
            this.label420.TabIndex = 78;
            this.label420.Text = "Permissible Bending Stress = Pms =";
            // 
            // textBox39
            // 
            this.textBox39.Location = new System.Drawing.Point(227, 107);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(62, 21);
            this.textBox39.TabIndex = 82;
            this.textBox39.Text = "70.2";
            this.textBox39.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label419
            // 
            this.label419.AutoSize = true;
            this.label419.Location = new System.Drawing.Point(11, 113);
            this.label419.Name = "label419";
            this.label419.Size = new System.Drawing.Size(173, 13);
            this.label419.TabIndex = 77;
            this.label419.Text = "Maximum Shear Force = V =";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(227, 80);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(62, 21);
            this.textBox38.TabIndex = 86;
            this.textBox38.Text = "151.13";
            this.textBox38.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label425
            // 
            this.label425.AutoSize = true;
            this.label425.Location = new System.Drawing.Point(295, 29);
            this.label425.Name = "label425";
            this.label425.Size = new System.Drawing.Size(18, 13);
            this.label425.TabIndex = 75;
            this.label425.Text = "m";
            // 
            // label424
            // 
            this.label424.AutoSize = true;
            this.label424.Location = new System.Drawing.Point(297, 56);
            this.label424.Name = "label424";
            this.label424.Size = new System.Drawing.Size(29, 13);
            this.label424.TabIndex = 67;
            this.label424.Text = "mm";
            // 
            // label429
            // 
            this.label429.AutoSize = true;
            this.label429.Location = new System.Drawing.Point(297, 186);
            this.label429.Name = "label429";
            this.label429.Size = new System.Drawing.Size(61, 13);
            this.label429.TabIndex = 74;
            this.label429.Text = "N/Sq.mm";
            // 
            // label428
            // 
            this.label428.AutoSize = true;
            this.label428.Location = new System.Drawing.Point(297, 161);
            this.label428.Name = "label428";
            this.label428.Size = new System.Drawing.Size(61, 13);
            this.label428.TabIndex = 73;
            this.label428.Text = "N/Sq.mm";
            // 
            // label427
            // 
            this.label427.AutoSize = true;
            this.label427.Location = new System.Drawing.Point(297, 137);
            this.label427.Name = "label427";
            this.label427.Size = new System.Drawing.Size(61, 13);
            this.label427.TabIndex = 72;
            this.label427.Text = "N/Sq.mm";
            // 
            // label426
            // 
            this.label426.AutoSize = true;
            this.label426.Location = new System.Drawing.Point(297, 110);
            this.label426.Name = "label426";
            this.label426.Size = new System.Drawing.Size(22, 13);
            this.label426.TabIndex = 71;
            this.label426.Text = "kN";
            // 
            // label423
            // 
            this.label423.AutoSize = true;
            this.label423.Location = new System.Drawing.Point(297, 83);
            this.label423.Name = "label423";
            this.label423.Size = new System.Drawing.Size(38, 13);
            this.label423.TabIndex = 70;
            this.label423.Text = "kN-m";
            // 
            // label418
            // 
            this.label418.AutoSize = true;
            this.label418.Location = new System.Drawing.Point(11, 86);
            this.label418.Name = "label418";
            this.label418.Size = new System.Drawing.Size(204, 13);
            this.label418.TabIndex = 69;
            this.label418.Text = "Maximum Bending Moment  = M =";
            // 
            // textBox37
            // 
            this.textBox37.Location = new System.Drawing.Point(227, 53);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(62, 21);
            this.textBox37.TabIndex = 85;
            this.textBox37.Text = "300";
            this.textBox37.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label417
            // 
            this.label417.AutoSize = true;
            this.label417.Location = new System.Drawing.Point(11, 59);
            this.label417.Name = "label417";
            this.label417.Size = new System.Drawing.Size(159, 13);
            this.label417.TabIndex = 68;
            this.label417.Text = "Bearing at each end = a =";
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(227, 26);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(62, 21);
            this.textBox36.TabIndex = 81;
            this.textBox36.Text = "8.0";
            this.textBox36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label416
            // 
            this.label416.AutoSize = true;
            this.label416.Location = new System.Drawing.Point(11, 32);
            this.label416.Name = "label416";
            this.label416.Size = new System.Drawing.Size(73, 13);
            this.label416.TabIndex = 76;
            this.label416.Text = "Span = l = ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Modify);
            this.panel2.Controls.Add(this.cmb_flr_lvl);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btn_OK);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.cmb_sele_all);
            this.panel2.Controls.Add(this.btn_ana_input_data);
            this.panel2.Controls.Add(this.btn_oprn_report);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 439);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(911, 46);
            this.panel2.TabIndex = 19;
            // 
            // Column12
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column12.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column12.Frozen = true;
            this.Column12.HeaderText = "SL. Nos.";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 50;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column13
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column13.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column13.HeaderText = "Floor Level (m)";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 80;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "Beam Nos";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.HeaderText = "Continuous Beam with Member Nos";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column6.HeaderText = "Section Name";
            this.Column6.Name = "Column6";
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column7.HeaderText = "Depth [D] (m)";
            this.Column7.Name = "Column7";
            this.Column7.Width = 60;
            // 
            // Column16
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column16.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column16.HeaderText = "Maximum Bending Moment (kN-m)";
            this.Column16.Name = "Column16";
            // 
            // Column21
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column21.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column21.HeaderText = "Maximum Shear Force (kN)";
            this.Column21.Name = "Column21";
            // 
            // Column15
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column15.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column15.HeaderText = "Design Result";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 90;
            // 
            // frmSteelBeamBoQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSteelBeamBoQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steel Beam Design";
            this.Load += new System.EventHandler(this.frmSteelBeamBoQ_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beams)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_beams;
        private System.Windows.Forms.ComboBox cmb_flr_lvl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cmb_sele_all;
        private System.Windows.Forms.Button btn_oprn_report;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_ana_input_data;
        private UC_SteelSections uC_SteelSections1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.Label label422;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Label label421;
        private System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.Label label420;
        private System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.Label label419;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.Label label425;
        private System.Windows.Forms.Label label424;
        private System.Windows.Forms.Label label429;
        private System.Windows.Forms.Label label428;
        private System.Windows.Forms.Label label427;
        private System.Windows.Forms.Label label426;
        private System.Windows.Forms.Label label423;
        private System.Windows.Forms.Label label418;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.Label label417;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.Label label416;
        private System.Windows.Forms.Button btn_beam_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
    }


}