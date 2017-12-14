﻿namespace BridgeAnalysisDesign.RCC_Culvert
{
    partial class UC_BoxCulvert
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_process_data = new System.Windows.Forms.Button();
            this.btn_LL_report = new System.Windows.Forms.Button();
            this.btn_LL_input = new System.Windows.Forms.Button();
            this.btn_DL_report = new System.Windows.Forms.Button();
            this.btn_DL_input = new System.Windows.Forms.Button();
            this.btn_create_data = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_process_design = new System.Windows.Forms.Button();
            this.btn_open_design = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.dgv_design_data_single = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_design_data_multi = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtb_results = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_multi = new System.Windows.Forms.RadioButton();
            this.rbtn_single = new System.Windows.Forms.RadioButton();
            this.btn_open_result_summary = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtb_load_data = new System.Windows.Forms.RichTextBox();
            this.btn_save_load_data = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.sc1.Panel1.SuspendLayout();
            this.sc1.Panel2.SuspendLayout();
            this.sc1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_single)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_multi)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 90);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.btn_process_data);
            this.groupBox2.Controls.Add(this.btn_LL_report);
            this.groupBox2.Controls.Add(this.btn_LL_input);
            this.groupBox2.Controls.Add(this.btn_DL_report);
            this.groupBox2.Controls.Add(this.btn_DL_input);
            this.groupBox2.Controls.Add(this.btn_create_data);
            this.groupBox2.Location = new System.Drawing.Point(4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis";
            // 
            // btn_process_data
            // 
            this.btn_process_data.Location = new System.Drawing.Point(7, 47);
            this.btn_process_data.Name = "btn_process_data";
            this.btn_process_data.Size = new System.Drawing.Size(99, 29);
            this.btn_process_data.TabIndex = 0;
            this.btn_process_data.Text = "Process Data";
            this.btn_process_data.UseVisualStyleBackColor = true;
            this.btn_process_data.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_report
            // 
            this.btn_LL_report.Location = new System.Drawing.Point(251, 47);
            this.btn_LL_report.Name = "btn_LL_report";
            this.btn_LL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_report.TabIndex = 0;
            this.btn_LL_report.Text = "Open LL Report File";
            this.btn_LL_report.UseVisualStyleBackColor = true;
            this.btn_LL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_input
            // 
            this.btn_LL_input.Location = new System.Drawing.Point(251, 17);
            this.btn_LL_input.Name = "btn_LL_input";
            this.btn_LL_input.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_input.TabIndex = 0;
            this.btn_LL_input.Text = "Open LL Input File";
            this.btn_LL_input.UseVisualStyleBackColor = true;
            this.btn_LL_input.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_report
            // 
            this.btn_DL_report.Location = new System.Drawing.Point(112, 47);
            this.btn_DL_report.Name = "btn_DL_report";
            this.btn_DL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_DL_report.TabIndex = 0;
            this.btn_DL_report.Text = "Open DL Report File";
            this.btn_DL_report.UseVisualStyleBackColor = true;
            this.btn_DL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_input
            // 
            this.btn_DL_input.Location = new System.Drawing.Point(112, 17);
            this.btn_DL_input.Name = "btn_DL_input";
            this.btn_DL_input.Size = new System.Drawing.Size(133, 29);
            this.btn_DL_input.TabIndex = 0;
            this.btn_DL_input.Text = "Open DL Input File";
            this.btn_DL_input.UseVisualStyleBackColor = true;
            this.btn_DL_input.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_create_data
            // 
            this.btn_create_data.Location = new System.Drawing.Point(7, 17);
            this.btn_create_data.Name = "btn_create_data";
            this.btn_create_data.Size = new System.Drawing.Size(99, 29);
            this.btn_create_data.TabIndex = 0;
            this.btn_create_data.Text = "Create Data";
            this.btn_create_data.UseVisualStyleBackColor = true;
            this.btn_create_data.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.btn_open_result_summary);
            this.groupBox3.Controls.Add(this.btn_process_design);
            this.groupBox3.Controls.Add(this.btn_open_design);
            this.groupBox3.Location = new System.Drawing.Point(399, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Design";
            // 
            // btn_process_design
            // 
            this.btn_process_design.Location = new System.Drawing.Point(6, 16);
            this.btn_process_design.Name = "btn_process_design";
            this.btn_process_design.Size = new System.Drawing.Size(103, 29);
            this.btn_process_design.TabIndex = 0;
            this.btn_process_design.Text = "Process Design";
            this.btn_process_design.UseVisualStyleBackColor = true;
            this.btn_process_design.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_open_design
            // 
            this.btn_open_design.Location = new System.Drawing.Point(6, 47);
            this.btn_open_design.Name = "btn_open_design";
            this.btn_open_design.Size = new System.Drawing.Size(103, 29);
            this.btn_open_design.TabIndex = 0;
            this.btn_open_design.Text = "Open Design";
            this.btn_open_design.UseVisualStyleBackColor = true;
            this.btn_open_design.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(648, 402);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 59);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 343);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sc1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // sc1
            // 
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.Location = new System.Drawing.Point(3, 3);
            this.sc1.Name = "sc1";
            this.sc1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc1.Panel1
            // 
            this.sc1.Panel1.Controls.Add(this.dgv_design_data_single);
            // 
            // sc1.Panel2
            // 
            this.sc1.Panel2.Controls.Add(this.dgv_design_data_multi);
            this.sc1.Size = new System.Drawing.Size(634, 311);
            this.sc1.SplitterDistance = 150;
            this.sc1.TabIndex = 5;
            // 
            // dgv_design_data_single
            // 
            this.dgv_design_data_single.AllowUserToAddRows = false;
            this.dgv_design_data_single.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgv_design_data_single.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_design_data_single.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgv_design_data_single.Location = new System.Drawing.Point(31, 3);
            this.dgv_design_data_single.Name = "dgv_design_data_single";
            this.dgv_design_data_single.RowHeadersWidth = 27;
            this.dgv_design_data_single.Size = new System.Drawing.Size(563, 144);
            this.dgv_design_data_single.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Description";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 350;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn2.HeaderText = "Data";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 99;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dgv_design_data_multi
            // 
            this.dgv_design_data_multi.AllowUserToAddRows = false;
            this.dgv_design_data_multi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgv_design_data_multi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_design_data_multi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgv_design_data_multi.Location = new System.Drawing.Point(31, 3);
            this.dgv_design_data_multi.Name = "dgv_design_data_multi";
            this.dgv_design_data_multi.RowHeadersWidth = 27;
            this.dgv_design_data_multi.Size = new System.Drawing.Size(563, 148);
            this.dgv_design_data_multi.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 350;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn5.HeaderText = "Data";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 99;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn6.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.rtb_results);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(640, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analysis Results";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtb_results
            // 
            this.rtb_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_results.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_results.Location = new System.Drawing.Point(3, 3);
            this.rtb_results.Name = "rtb_results";
            this.rtb_results.Size = new System.Drawing.Size(634, 311);
            this.rtb_results.TabIndex = 0;
            this.rtb_results.Text = "";
            this.rtb_results.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_multi);
            this.groupBox1.Controls.Add(this.rbtn_single);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Box Type";
            // 
            // rbtn_multi
            // 
            this.rbtn_multi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rbtn_multi.AutoSize = true;
            this.rbtn_multi.Location = new System.Drawing.Point(341, 22);
            this.rbtn_multi.Name = "rbtn_multi";
            this.rbtn_multi.Size = new System.Drawing.Size(181, 20);
            this.rbtn_multi.TabIndex = 0;
            this.rbtn_multi.Text = "Multi Cell Box Culvert";
            this.rbtn_multi.UseVisualStyleBackColor = true;
            this.rbtn_multi.CheckedChanged += new System.EventHandler(this.rbtn_single_CheckedChanged);
            // 
            // rbtn_single
            // 
            this.rbtn_single.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rbtn_single.AutoSize = true;
            this.rbtn_single.Checked = true;
            this.rbtn_single.Location = new System.Drawing.Point(127, 22);
            this.rbtn_single.Name = "rbtn_single";
            this.rbtn_single.Size = new System.Drawing.Size(190, 20);
            this.rbtn_single.TabIndex = 0;
            this.rbtn_single.TabStop = true;
            this.rbtn_single.Text = "Single Cell Box Culvert";
            this.rbtn_single.UseVisualStyleBackColor = true;
            this.rbtn_single.CheckedChanged += new System.EventHandler(this.rbtn_single_CheckedChanged);
            // 
            // btn_open_result_summary
            // 
            this.btn_open_result_summary.Location = new System.Drawing.Point(115, 17);
            this.btn_open_result_summary.Name = "btn_open_result_summary";
            this.btn_open_result_summary.Size = new System.Drawing.Size(118, 59);
            this.btn_open_result_summary.TabIndex = 1;
            this.btn_open_result_summary.Text = "Open Analysis Results Summary";
            this.btn_open_result_summary.UseVisualStyleBackColor = true;
            this.btn_open_result_summary.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_save_load_data);
            this.tabPage3.Controls.Add(this.rtb_load_data);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(640, 317);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Load Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtb_load_data
            // 
            this.rtb_load_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_load_data.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_load_data.Location = new System.Drawing.Point(3, 3);
            this.rtb_load_data.Name = "rtb_load_data";
            this.rtb_load_data.Size = new System.Drawing.Size(634, 273);
            this.rtb_load_data.TabIndex = 1;
            this.rtb_load_data.Text = "";
            this.rtb_load_data.WordWrap = false;
            // 
            // btn_save_load_data
            // 
            this.btn_save_load_data.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_save_load_data.Location = new System.Drawing.Point(190, 282);
            this.btn_save_load_data.Name = "btn_save_load_data";
            this.btn_save_load_data.Size = new System.Drawing.Size(231, 29);
            this.btn_save_load_data.TabIndex = 2;
            this.btn_save_load_data.Text = "Save Load Data";
            this.btn_save_load_data.UseVisualStyleBackColor = true;
            this.btn_save_load_data.Click += new System.EventHandler(this.btn_save_load_data_Click);
            // 
            // UC_BoxCulvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_BoxCulvert";
            this.Size = new System.Drawing.Size(648, 492);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.sc1.Panel1.ResumeLayout(false);
            this.sc1.Panel2.ResumeLayout(false);
            this.sc1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_single)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_multi)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_process_design;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_design_data_single;
        private System.Windows.Forms.Button btn_open_design;
        private System.Windows.Forms.SplitContainer sc1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_multi;
        private System.Windows.Forms.RadioButton rbtn_single;
        private System.Windows.Forms.Button btn_process_data;
        private System.Windows.Forms.Button btn_create_data;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_LL_report;
        private System.Windows.Forms.Button btn_LL_input;
        private System.Windows.Forms.Button btn_DL_report;
        private System.Windows.Forms.Button btn_DL_input;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView dgv_design_data_multi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtb_results;
        private System.Windows.Forms.Button btn_open_result_summary;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox rtb_load_data;
        private System.Windows.Forms.Button btn_save_load_data;
    }
}
