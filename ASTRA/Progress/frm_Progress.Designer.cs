namespace AstraFunctionOne.Progress
{
    partial class frm_Progress
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
            Close_Flag = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Progress));
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.col_sl_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_work = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_tr = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_percentage = new System.Windows.Forms.Label();
            this.pnl_btn = new System.Windows.Forms.Panel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_time_remain = new System.Windows.Forms.Label();
            this.lbl_total_time = new System.Windows.Forms.Label();
            this.lbl_tm = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnl_btn.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToOrderColumns = true;
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_sl_no,
            this.col_work,
            this.col_status});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(527, 0);
            this.dgv_list.TabIndex = 0;
            // 
            // col_sl_no
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_sl_no.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_sl_no.HeaderText = "SL NO";
            this.col_sl_no.Name = "col_sl_no";
            this.col_sl_no.ReadOnly = true;
            this.col_sl_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_sl_no.Width = 50;
            // 
            // col_work
            // 
            this.col_work.HeaderText = "Work";
            this.col_work.Name = "col_work";
            this.col_work.ReadOnly = true;
            this.col_work.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_work.Width = 380;
            // 
            // col_status
            // 
            this.col_status.HeaderText = "Status";
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_status.Width = 90;
            // 
            // lbl_tr
            // 
            this.lbl_tr.AutoSize = true;
            this.lbl_tr.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tr.Location = new System.Drawing.Point(225, 5);
            this.lbl_tr.Name = "lbl_tr";
            this.lbl_tr.Size = new System.Drawing.Size(119, 13);
            this.lbl_tr.TabIndex = 2;
            this.lbl_tr.Text = "Time Remaining :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 0);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lbl_percentage
            // 
            this.lbl_percentage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbl_percentage.AutoSize = true;
            this.lbl_percentage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_percentage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_percentage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_percentage.ForeColor = System.Drawing.Color.Blue;
            this.lbl_percentage.Location = new System.Drawing.Point(250, 5);
            this.lbl_percentage.Name = "lbl_percentage";
            this.lbl_percentage.Size = new System.Drawing.Size(31, 14);
            this.lbl_percentage.TabIndex = 6;
            this.lbl_percentage.Text = "0%";
            this.lbl_percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_btn
            // 
            this.pnl_btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_btn.Controls.Add(this.btn_cancel);
            this.pnl_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btn.Location = new System.Drawing.Point(0, -28);
            this.pnl_btn.Name = "pnl_btn";
            this.pnl_btn.Size = new System.Drawing.Size(533, 30);
            this.pnl_btn.TabIndex = 13;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(199, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(133, 23);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "CANCEL";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 1);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(531, 20);
            this.progressBar1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_time_remain);
            this.panel1.Controls.Add(this.lbl_tr);
            this.panel1.Controls.Add(this.lbl_total_time);
            this.panel1.Controls.Add(this.lbl_tm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 27);
            this.panel1.TabIndex = 11;
            // 
            // lbl_time_remain
            // 
            this.lbl_time_remain.AutoSize = true;
            this.lbl_time_remain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time_remain.Location = new System.Drawing.Point(361, 5);
            this.lbl_time_remain.Name = "lbl_time_remain";
            this.lbl_time_remain.Size = new System.Drawing.Size(67, 13);
            this.lbl_time_remain.TabIndex = 3;
            this.lbl_time_remain.Text = "Total Time";
            // 
            // lbl_total_time
            // 
            this.lbl_total_time.AutoSize = true;
            this.lbl_total_time.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_time.Location = new System.Drawing.Point(119, 5);
            this.lbl_total_time.Name = "lbl_total_time";
            this.lbl_total_time.Size = new System.Drawing.Size(67, 13);
            this.lbl_total_time.TabIndex = 1;
            this.lbl_total_time.Text = "Total Time";
            // 
            // lbl_tm
            // 
            this.lbl_tm.AutoSize = true;
            this.lbl_tm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tm.Location = new System.Drawing.Point(3, 5);
            this.lbl_tm.Name = "lbl_tm";
            this.lbl_tm.Size = new System.Drawing.Size(84, 13);
            this.lbl_tm.TabIndex = 0;
            this.lbl_tm.Text = "Total Time :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbl_percentage);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 23);
            this.panel2.TabIndex = 14;
            // 
            // frm_Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 52);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnl_btn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Progress";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_Progress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.pnl_btn.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sl_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_work;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.Label lbl_tr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_percentage;
        private System.Windows.Forms.Panel pnl_btn;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_time_remain;
        private System.Windows.Forms.Label lbl_total_time;
        private System.Windows.Forms.Label lbl_tm;
        private System.Windows.Forms.Panel panel2;
    }
}