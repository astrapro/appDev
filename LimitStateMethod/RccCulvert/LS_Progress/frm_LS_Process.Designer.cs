namespace LimitStateMethod.LS_Progress
{
    partial class frm_LS_Process
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LS_Process));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_process = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_run_all = new System.Windows.Forms.CheckBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
            this.col_sl_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_prc_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_process)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_process);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(585, 462);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgv_process
            // 
            this.dgv_process.AllowUserToAddRows = false;
            this.dgv_process.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_process.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_process.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_process.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_sl_no,
            this.col_chk,
            this.col_prc_item,
            this.col_rem});
            this.dgv_process.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_process.Location = new System.Drawing.Point(0, 0);
            this.dgv_process.Name = "dgv_process";
            this.dgv_process.RowHeadersVisible = false;
            this.dgv_process.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_process.Size = new System.Drawing.Size(585, 414);
            this.dgv_process.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_run_all);
            this.groupBox1.Controls.Add(this.btn_cancel);
            this.groupBox1.Controls.Add(this.btn_process);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chk_run_all
            // 
            this.chk_run_all.AutoSize = true;
            this.chk_run_all.Location = new System.Drawing.Point(6, 22);
            this.chk_run_all.Name = "chk_run_all";
            this.chk_run_all.Size = new System.Drawing.Size(66, 17);
            this.chk_run_all.TabIndex = 1;
            this.chk_run_all.Text = "Run All";
            this.chk_run_all.UseVisualStyleBackColor = true;
            this.chk_run_all.CheckedChanged += new System.EventHandler(this.chk_run_all_CheckedChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(307, 13);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(122, 32);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(155, 13);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(122, 32);
            this.btn_process.TabIndex = 0;
            this.btn_process.Text = "Process";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // col_sl_no
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_sl_no.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_sl_no.HeaderText = "SL. NOS";
            this.col_sl_no.Name = "col_sl_no";
            this.col_sl_no.ReadOnly = true;
            this.col_sl_no.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.col_sl_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_sl_no.Width = 48;
            // 
            // col_chk
            // 
            this.col_chk.HeaderText = "RUN";
            this.col_chk.Name = "col_chk";
            this.col_chk.Width = 50;
            // 
            // col_prc_item
            // 
            this.col_prc_item.HeaderText = "PROCESS";
            this.col_prc_item.Name = "col_prc_item";
            this.col_prc_item.ReadOnly = true;
            this.col_prc_item.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.col_prc_item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_prc_item.Width = 390;
            // 
            // col_rem
            // 
            this.col_rem.HeaderText = "REMARKS";
            this.col_rem.Name = "col_rem";
            this.col_rem.ReadOnly = true;
            this.col_rem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_rem.Width = 90;
            // 
            // frm_LS_Process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 462);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_LS_Process";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Bridge Analysis Process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_LS_Process_FormClosing);
            this.Load += new System.EventHandler(this.frm_LS_Process_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_process)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_process;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.CheckBox chk_run_all;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sl_no;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_prc_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rem;
    }
}