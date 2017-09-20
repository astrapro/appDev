namespace BridgeAnalysisDesign.RCC_T_Girder
{
    partial class frm_ViewForces
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_live_vert_rec_Ton = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_left_end_design_forces = new System.Windows.Forms.DataGridView();
            this.txt_dead_vert_reac_ton = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_right_end_design_forces = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.col_Joints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Vert_Reaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dead_vert_reac_kN = new System.Windows.Forms.TextBox();
            this.txt_live_vert_rec_kN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_live_kN_m = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_dead_kN_m = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_live_vert_rec_Ton
            // 
            this.txt_live_vert_rec_Ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_Ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_Ton.Location = new System.Drawing.Point(106, 310);
            this.txt_live_vert_rec_Ton.Name = "txt_live_vert_rec_Ton";
            this.txt_live_vert_rec_Ton.Size = new System.Drawing.Size(117, 20);
            this.txt_live_vert_rec_Ton.TabIndex = 22;
            this.txt_live_vert_rec_Ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_live_vert_rec_Ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_dead_kN_m);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_dead_vert_reac_kN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgv_left_end_design_forces);
            this.groupBox1.Controls.Add(this.txt_dead_vert_reac_ton);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 397);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Support Reactions from Dead Load";
            // 
            // dgv_left_end_design_forces
            // 
            this.dgv_left_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_left_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_left_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Joints,
            this.col_Vert_Reaction});
            this.dgv_left_end_design_forces.Location = new System.Drawing.Point(18, 19);
            this.dgv_left_end_design_forces.Name = "dgv_left_end_design_forces";
            this.dgv_left_end_design_forces.ReadOnly = true;
            this.dgv_left_end_design_forces.Size = new System.Drawing.Size(258, 285);
            this.dgv_left_end_design_forces.TabIndex = 1;
            // 
            // txt_dead_vert_reac_ton
            // 
            this.txt_dead_vert_reac_ton.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_ton.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_ton.Location = new System.Drawing.Point(126, 310);
            this.txt_dead_vert_reac_ton.Name = "txt_dead_vert_reac_ton";
            this.txt_dead_vert_reac_ton.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_ton.TabIndex = 11;
            this.txt_dead_vert_reac_ton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_dead_vert_reac_ton.TextChanged += new System.EventHandler(this.txt_dead_vert_reac_ton_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label8.Location = new System.Drawing.Point(229, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ton";
            // 
            // dgv_right_end_design_forces
            // 
            this.dgv_right_end_design_forces.AllowUserToResizeColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_right_end_design_forces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_right_end_design_forces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right_end_design_forces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgv_right_end_design_forces.Location = new System.Drawing.Point(17, 19);
            this.dgv_right_end_design_forces.Name = "dgv_right_end_design_forces";
            this.dgv_right_end_design_forces.ReadOnly = true;
            this.dgv_right_end_design_forces.Size = new System.Drawing.Size(255, 285);
            this.dgv_right_end_design_forces.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(59, 420);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(377, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "The Details are also written at the end of the Analysis Report file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_live_kN_m);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_live_vert_rec_kN);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dgv_right_end_design_forces);
            this.groupBox2.Controls.Add(this.txt_live_vert_rec_Ton);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(315, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 397);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Support Reactions from Live Load";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(450, 415);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 23;
            this.btn_close.Text = "CLOSE";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // col_Joints
            // 
            this.col_Joints.HeaderText = "Joints";
            this.col_Joints.Name = "col_Joints";
            this.col_Joints.ReadOnly = true;
            this.col_Joints.Width = 63;
            // 
            // col_Vert_Reaction
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Vert_Reaction.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_Vert_Reaction.HeaderText = "Vertical Reaction (Ton)";
            this.col_Vert_Reaction.Name = "col_Vert_Reaction";
            this.col_Vert_Reaction.ReadOnly = true;
            this.col_Vert_Reaction.Width = 118;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Joints";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 63;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "Vertical Reaction (Ton)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 118;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label1.Location = new System.Drawing.Point(245, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Ton";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label2.Location = new System.Drawing.Point(46, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Total ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label3.Location = new System.Drawing.Point(45, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Total ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label5.Location = new System.Drawing.Point(245, 339);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "kN";
            // 
            // txt_dead_vert_reac_kN
            // 
            this.txt_dead_vert_reac_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_vert_reac_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_vert_reac_kN.Location = new System.Drawing.Point(126, 336);
            this.txt_dead_vert_reac_kN.Name = "txt_dead_vert_reac_kN";
            this.txt_dead_vert_reac_kN.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_vert_reac_kN.TabIndex = 25;
            this.txt_dead_vert_reac_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_live_vert_rec_kN
            // 
            this.txt_live_vert_rec_kN.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_vert_rec_kN.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_vert_rec_kN.Location = new System.Drawing.Point(106, 339);
            this.txt_live_vert_rec_kN.Name = "txt_live_vert_rec_kN";
            this.txt_live_vert_rec_kN.Size = new System.Drawing.Size(117, 20);
            this.txt_live_vert_rec_kN.TabIndex = 26;
            this.txt_live_vert_rec_kN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label6.Location = new System.Drawing.Point(229, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "kN";
            // 
            // txt_live_kN_m
            // 
            this.txt_live_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_live_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_live_kN_m.Location = new System.Drawing.Point(106, 365);
            this.txt_live_kN_m.Name = "txt_live_kN_m";
            this.txt_live_kN_m.Size = new System.Drawing.Size(117, 20);
            this.txt_live_kN_m.TabIndex = 28;
            this.txt_live_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label4.Location = new System.Drawing.Point(229, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "kN/m";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.label7.Location = new System.Drawing.Point(245, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "kN/m";
            // 
            // txt_dead_kN_m
            // 
            this.txt_dead_kN_m.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.txt_dead_kN_m.ForeColor = System.Drawing.Color.Blue;
            this.txt_dead_kN_m.Location = new System.Drawing.Point(126, 365);
            this.txt_dead_kN_m.Name = "txt_dead_kN_m";
            this.txt_dead_kN_m.Size = new System.Drawing.Size(113, 20);
            this.txt_dead_kN_m.TabIndex = 27;
            this.txt_dead_kN_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frm_ViewForces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 446);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_close);
            this.Name = "frm_ViewForces";
            this.Text = "Vertical Reactions";
            this.Load += new System.EventHandler(this.frm_ViewForces_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left_end_design_forces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right_end_design_forces)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_live_vert_rec_Ton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_left_end_design_forces;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Joints;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Vert_Reaction;
        private System.Windows.Forms.TextBox txt_dead_vert_reac_ton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_right_end_design_forces;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_dead_vert_reac_kN;
        public System.Windows.Forms.TextBox txt_live_vert_rec_kN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_dead_kN_m;
        public System.Windows.Forms.TextBox txt_live_kN_m;
        private System.Windows.Forms.Label label4;
    }
}