namespace BridgeAnalysisDesign.RCC_Culvert
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_open_design = new System.Windows.Forms.Button();
            this.btn_process_design = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_process_data = new System.Windows.Forms.Button();
            this.btn_LL_report = new System.Windows.Forms.Button();
            this.btn_LL_input = new System.Windows.Forms.Button();
            this.btn_DL_report = new System.Windows.Forms.Button();
            this.btn_DL_input = new System.Windows.Forms.Button();
            this.btn_create_data = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.dgv_design_data_single = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_design_data_multi = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_multi = new System.Windows.Forms.RadioButton();
            this.rbtn_single = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.sc1.Panel1.SuspendLayout();
            this.sc1.Panel2.SuspendLayout();
            this.sc1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_single)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_multi)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_open_design);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btn_process_design);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 429);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 49);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(412, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(129, 26);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Design";
            this.groupBox3.Visible = false;
            // 
            // btn_open_design
            // 
            this.btn_open_design.Location = new System.Drawing.Point(288, 8);
            this.btn_open_design.Name = "btn_open_design";
            this.btn_open_design.Size = new System.Drawing.Size(118, 29);
            this.btn_open_design.TabIndex = 0;
            this.btn_open_design.Text = "Open Design";
            this.btn_open_design.UseVisualStyleBackColor = true;
            this.btn_open_design.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_process_design
            // 
            this.btn_process_design.Location = new System.Drawing.Point(145, 8);
            this.btn_process_design.Name = "btn_process_design";
            this.btn_process_design.Size = new System.Drawing.Size(118, 29);
            this.btn_process_design.TabIndex = 0;
            this.btn_process_design.Text = "Process Design";
            this.btn_process_design.UseVisualStyleBackColor = true;
            this.btn_process_design.Click += new System.EventHandler(this.btn_single_cell_Click);
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
            this.groupBox2.Location = new System.Drawing.Point(24, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(68, 29);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis";
            this.groupBox2.Visible = false;
            // 
            // btn_process_data
            // 
            this.btn_process_data.Location = new System.Drawing.Point(7, 58);
            this.btn_process_data.Name = "btn_process_data";
            this.btn_process_data.Size = new System.Drawing.Size(131, 29);
            this.btn_process_data.TabIndex = 0;
            this.btn_process_data.Text = "Process Data";
            this.btn_process_data.UseVisualStyleBackColor = true;
            this.btn_process_data.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_report
            // 
            this.btn_LL_report.Location = new System.Drawing.Point(283, 58);
            this.btn_LL_report.Name = "btn_LL_report";
            this.btn_LL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_report.TabIndex = 0;
            this.btn_LL_report.Text = "Open LL Report File";
            this.btn_LL_report.UseVisualStyleBackColor = true;
            this.btn_LL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_input
            // 
            this.btn_LL_input.Location = new System.Drawing.Point(283, 17);
            this.btn_LL_input.Name = "btn_LL_input";
            this.btn_LL_input.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_input.TabIndex = 0;
            this.btn_LL_input.Text = "Open LL Input File";
            this.btn_LL_input.UseVisualStyleBackColor = true;
            this.btn_LL_input.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_report
            // 
            this.btn_DL_report.Location = new System.Drawing.Point(144, 58);
            this.btn_DL_report.Name = "btn_DL_report";
            this.btn_DL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_DL_report.TabIndex = 0;
            this.btn_DL_report.Text = "Open DL Report File";
            this.btn_DL_report.UseVisualStyleBackColor = true;
            this.btn_DL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_input
            // 
            this.btn_DL_input.Location = new System.Drawing.Point(144, 17);
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
            this.btn_create_data.Size = new System.Drawing.Size(131, 29);
            this.btn_create_data.TabIndex = 0;
            this.btn_create_data.Text = "Create Data";
            this.btn_create_data.UseVisualStyleBackColor = true;
            this.btn_create_data.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sc1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 429);
            this.panel2.TabIndex = 1;
            // 
            // sc1
            // 
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.Location = new System.Drawing.Point(0, 58);
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
            this.sc1.Size = new System.Drawing.Size(578, 371);
            this.sc1.SplitterDistance = 242;
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
            this.dgv_design_data_single.Location = new System.Drawing.Point(3, 3);
            this.dgv_design_data_single.Name = "dgv_design_data_single";
            this.dgv_design_data_single.RowHeadersWidth = 27;
            this.dgv_design_data_single.Size = new System.Drawing.Size(572, 236);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Data";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 99;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.dgv_design_data_multi.Location = new System.Drawing.Point(3, 3);
            this.dgv_design_data_multi.Name = "dgv_design_data_multi";
            this.dgv_design_data_multi.RowHeadersWidth = 27;
            this.dgv_design_data_multi.Size = new System.Drawing.Size(572, 124);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.HeaderText = "Data";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 99;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn6.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_multi);
            this.groupBox1.Controls.Add(this.rbtn_single);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Box Type";
            // 
            // rbtn_multi
            // 
            this.rbtn_multi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rbtn_multi.AutoSize = true;
            this.rbtn_multi.Location = new System.Drawing.Point(306, 22);
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
            this.rbtn_single.Location = new System.Drawing.Point(92, 22);
            this.rbtn_single.Name = "rbtn_single";
            this.rbtn_single.Size = new System.Drawing.Size(190, 20);
            this.rbtn_single.TabIndex = 0;
            this.rbtn_single.TabStop = true;
            this.rbtn_single.Text = "Single Cell Box Culvert";
            this.rbtn_single.UseVisualStyleBackColor = true;
            this.rbtn_single.CheckedChanged += new System.EventHandler(this.rbtn_single_CheckedChanged);
            // 
            // UC_BoxCulvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_BoxCulvert";
            this.Size = new System.Drawing.Size(578, 478);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.sc1.Panel1.ResumeLayout(false);
            this.sc1.Panel2.ResumeLayout(false);
            this.sc1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_single)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data_multi)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}
