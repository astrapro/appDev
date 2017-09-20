namespace AstraAccess.Footing
{
    partial class frm_RaftFoundation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RaftFoundation));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_ColumnData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.btn_Load_ExampleData = new System.Windows.Forms.Button();
            this.btn_open_design = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label283 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_fy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_cover = new System.Windows.Forms.TextBox();
            this.label235 = new System.Windows.Forms.Label();
            this.label221 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_bearing_cap = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_fck = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_bar_dia = new System.Windows.Forms.TextBox();
            this.label234 = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Report = new System.Windows.Forms.Button();
            this.btn_Process = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_drawings = new System.Windows.Forms.Button();
            this.uC_CAD1 = new AstraAccess.ADOC.UC_CAD();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ColumnData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(1030, 578);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1022, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Design Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_ColumnData);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 546);
            this.splitContainer1.SplitterDistance = 471;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgv_ColumnData
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ColumnData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ColumnData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ColumnData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ColumnData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ColumnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ColumnData.Location = new System.Drawing.Point(0, 261);
            this.dgv_ColumnData.Name = "dgv_ColumnData";
            this.dgv_ColumnData.Size = new System.Drawing.Size(469, 237);
            this.dgv_ColumnData.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Coordinates (X,Y) ";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Grid Lengths  (X,Y)";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "L (M)";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 50;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "B (M)";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Load (kN)";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.btn_update);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmb_fy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_cover);
            this.groupBox1.Controls.Add(this.label235);
            this.groupBox1.Controls.Add(this.label221);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_bearing_cap);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmb_fck);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_bar_dia);
            this.groupBox1.Controls.Add(this.label234);
            this.groupBox1.Controls.Add(this.label227);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 261);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_new_design);
            this.panel5.Controls.Add(this.btn_Load_ExampleData);
            this.panel5.Controls.Add(this.btn_open_design);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label283);
            this.panel5.Location = new System.Drawing.Point(6, 13);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(376, 59);
            this.panel5.TabIndex = 181;
            // 
            // btn_new_design
            // 
            this.btn_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "New Design";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_psc_new_design_Click);
            // 
            // btn_Load_ExampleData
            // 
            this.btn_Load_ExampleData.Location = new System.Drawing.Point(172, 53);
            this.btn_Load_ExampleData.Name = "btn_Load_ExampleData";
            this.btn_Load_ExampleData.Size = new System.Drawing.Size(189, 24);
            this.btn_Load_ExampleData.TabIndex = 189;
            this.btn_Load_ExampleData.Text = "Load Example Data";
            this.btn_Load_ExampleData.UseVisualStyleBackColor = true;
            this.btn_Load_ExampleData.Visible = false;
            this.btn_Load_ExampleData.Click += new System.EventHandler(this.btn_psc_new_design_Click);
            // 
            // btn_open_design
            // 
            this.btn_open_design.Location = new System.Drawing.Point(242, 4);
            this.btn_open_design.Name = "btn_open_design";
            this.btn_open_design.Size = new System.Drawing.Size(121, 24);
            this.btn_open_design.TabIndex = 189;
            this.btn_open_design.Text = "Open Design";
            this.btn_open_design.UseVisualStyleBackColor = true;
            this.btn_open_design.Click += new System.EventHandler(this.btn_psc_new_design_Click);
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
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(226, 227);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(206, 28);
            this.btn_update.TabIndex = 0;
            this.btn_update.Text = "Update Grid";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_SelectColumn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select Column from Drawing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_SelectColumn_Click);
            // 
            // cmb_fy
            // 
            this.cmb_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_fy.FormattingEnabled = true;
            this.cmb_fy.Items.AddRange(new object[] {
            "240",
            "415",
            "500"});
            this.cmb_fy.Location = new System.Drawing.Point(212, 200);
            this.cmb_fy.Name = "cmb_fy";
            this.cmb_fy.Size = new System.Drawing.Size(70, 21);
            this.cmb_fy.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bearing Capacity of Soil ";
            // 
            // txt_cover
            // 
            this.txt_cover.Location = new System.Drawing.Point(206, 150);
            this.txt_cover.Name = "txt_cover";
            this.txt_cover.Size = new System.Drawing.Size(76, 21);
            this.txt_cover.TabIndex = 3;
            this.txt_cover.Text = "60";
            this.txt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label235
            // 
            this.label235.AutoSize = true;
            this.label235.Location = new System.Drawing.Point(25, 178);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(129, 13);
            this.label235.TabIndex = 13;
            this.label235.Text = "Concrete Grade [fck]";
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(184, 202);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(23, 14);
            this.label221.TabIndex = 59;
            this.label221.Text = "Fe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "kN/Sq.m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(290, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "N/mm^2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bar Dia ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(290, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "N/mm^2";
            // 
            // txt_bearing_cap
            // 
            this.txt_bearing_cap.Location = new System.Drawing.Point(206, 96);
            this.txt_bearing_cap.Name = "txt_bearing_cap";
            this.txt_bearing_cap.Size = new System.Drawing.Size(76, 21);
            this.txt_bearing_cap.TabIndex = 3;
            this.txt_bearing_cap.Text = "65";
            this.txt_bearing_cap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Cover ";
            // 
            // cmb_fck
            // 
            this.cmb_fck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_fck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_fck.FormattingEnabled = true;
            this.cmb_fck.Items.AddRange(new object[] {
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
            this.cmb_fck.Location = new System.Drawing.Point(212, 175);
            this.cmb_fck.Name = "cmb_fck";
            this.cmb_fck.Size = new System.Drawing.Size(70, 21);
            this.cmb_fck.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "mm";
            // 
            // txt_bar_dia
            // 
            this.txt_bar_dia.Location = new System.Drawing.Point(206, 123);
            this.txt_bar_dia.Name = "txt_bar_dia";
            this.txt_bar_dia.Size = new System.Drawing.Size(76, 21);
            this.txt_bar_dia.TabIndex = 3;
            this.txt_bar_dia.Text = "20";
            this.txt_bar_dia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Location = new System.Drawing.Point(25, 203);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(100, 13);
            this.label234.TabIndex = 15;
            this.label234.Text = "Steel Grade [fy]";
            // 
            // label227
            // 
            this.label227.AutoSize = true;
            this.label227.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label227.Location = new System.Drawing.Point(189, 177);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(18, 14);
            this.label227.TabIndex = 58;
            this.label227.Text = "M";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Report);
            this.panel1.Controls.Add(this.btn_Process);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 498);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 46);
            this.panel1.TabIndex = 1;
            // 
            // btn_Report
            // 
            this.btn_Report.Enabled = false;
            this.btn_Report.Location = new System.Drawing.Point(226, 6);
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.Size = new System.Drawing.Size(129, 32);
            this.btn_Report.TabIndex = 0;
            this.btn_Report.Text = "Report";
            this.btn_Report.UseVisualStyleBackColor = true;
            this.btn_Report.Click += new System.EventHandler(this.btn_Report_Click);
            // 
            // btn_Process
            // 
            this.btn_Process.Enabled = false;
            this.btn_Process.Location = new System.Drawing.Point(78, 6);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(129, 32);
            this.btn_Process.TabIndex = 0;
            this.btn_Process.Text = "Process";
            this.btn_Process.UseVisualStyleBackColor = true;
            this.btn_Process.Click += new System.EventHandler(this.btn_Process_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_drawings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1022, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Drawings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_drawings
            // 
            this.btn_drawings.Location = new System.Drawing.Point(343, 173);
            this.btn_drawings.Name = "btn_drawings";
            this.btn_drawings.Size = new System.Drawing.Size(356, 75);
            this.btn_drawings.TabIndex = 1;
            this.btn_drawings.Text = "Open Raft Foundation Drawings";
            this.btn_drawings.UseVisualStyleBackColor = true;
            this.btn_drawings.Click += new System.EventHandler(this.btn_drawings_Click);
            // 
            // uC_CAD1
            // 
            this.uC_CAD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_CAD1.Location = new System.Drawing.Point(3, 3);
            this.uC_CAD1.Name = "uC_CAD1";
            this.uC_CAD1.Size = new System.Drawing.Size(524, 512);
            this.uC_CAD1.TabIndex = 0;
            this.uC_CAD1.View_Buttons = false;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(538, 544);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.uC_CAD1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(530, 518);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "PLAN DRAWING";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(530, 518);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "SAMPLE FIGURE";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::AstraAccess.Properties.Resources.raftFigure;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(524, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frm_RaftFoundation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 578);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_RaftFoundation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_RaftFoundation";
            this.Load += new System.EventHandler(this.frm_RaftFoundations_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ColumnData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_ColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmb_fy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_cover;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_bearing_cap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_fck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_bar_dia;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.Label label227;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Report;
        private System.Windows.Forms.Button btn_Process;
        private ADOC.UC_CAD uC_CAD1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_open_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label283;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_Load_ExampleData;
        private System.Windows.Forms.Button btn_drawings;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}