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
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
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
            this.panel1.Controls.Add(this.btn_open);
            this.panel1.Controls.Add(this.btn_process);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 64);
            this.panel1.TabIndex = 0;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(201, 6);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(130, 39);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "Open Design";
            this.btn_open.UseVisualStyleBackColor = true;
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(45, 6);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(130, 39);
            this.btn_process.TabIndex = 0;
            this.btn_process.Text = "Process Design";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sc1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 414);
            this.panel2.TabIndex = 1;
            // 
            // sc1
            // 
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.Location = new System.Drawing.Point(0, 64);
            this.sc1.Name = "sc1";
            // 
            // sc1.Panel1
            // 
            this.sc1.Panel1.Controls.Add(this.dgv_design_data_single);
            // 
            // sc1.Panel2
            // 
            this.sc1.Panel2.Controls.Add(this.dgv_design_data_multi);
            this.sc1.Panel2Collapsed = true;
            this.sc1.Size = new System.Drawing.Size(555, 350);
            this.sc1.SplitterDistance = 357;
            this.sc1.TabIndex = 5;
            // 
            // dgv_design_data_single
            // 
            this.dgv_design_data_single.AllowUserToAddRows = false;
            this.dgv_design_data_single.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_design_data_single.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgv_design_data_single.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_design_data_single.Location = new System.Drawing.Point(0, 0);
            this.dgv_design_data_single.Name = "dgv_design_data_single";
            this.dgv_design_data_single.RowHeadersWidth = 27;
            this.dgv_design_data_single.Size = new System.Drawing.Size(555, 350);
            this.dgv_design_data_single.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Description";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Data";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 70;
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
            this.dgv_design_data_multi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_design_data_multi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgv_design_data_multi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_design_data_multi.Location = new System.Drawing.Point(0, 0);
            this.dgv_design_data_multi.Name = "dgv_design_data_multi";
            this.dgv_design_data_multi.RowHeadersWidth = 27;
            this.dgv_design_data_multi.Size = new System.Drawing.Size(96, 100);
            this.dgv_design_data_multi.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 250;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.HeaderText = "Data";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 70;
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
            this.groupBox1.Size = new System.Drawing.Size(555, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Box Type";
            // 
            // rbtn_multi
            // 
            this.rbtn_multi.AutoSize = true;
            this.rbtn_multi.Location = new System.Drawing.Point(232, 30);
            this.rbtn_multi.Name = "rbtn_multi";
            this.rbtn_multi.Size = new System.Drawing.Size(181, 20);
            this.rbtn_multi.TabIndex = 0;
            this.rbtn_multi.Text = "Multi Cell Box Culvert";
            this.rbtn_multi.UseVisualStyleBackColor = true;
            this.rbtn_multi.CheckedChanged += new System.EventHandler(this.rbtn_single_CheckedChanged);
            // 
            // rbtn_single
            // 
            this.rbtn_single.AutoSize = true;
            this.rbtn_single.Checked = true;
            this.rbtn_single.Location = new System.Drawing.Point(18, 30);
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
            this.Size = new System.Drawing.Size(555, 478);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_design_data_single;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.SplitContainer sc1;
        private System.Windows.Forms.DataGridView dgv_design_data_multi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_multi;
        private System.Windows.Forms.RadioButton rbtn_single;
    }
}
