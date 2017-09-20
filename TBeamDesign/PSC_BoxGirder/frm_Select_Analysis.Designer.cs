namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    partial class frm_Select_Analysis
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_process = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.chk_1 = new System.Windows.Forms.CheckBox();
            this.chk_2 = new System.Windows.Forms.CheckBox();
            this.chk_3 = new System.Windows.Forms.CheckBox();
            this.chk_4 = new System.Windows.Forms.CheckBox();
            this.chk_5 = new System.Windows.Forms.CheckBox();
            this.chk_6 = new System.Windows.Forms.CheckBox();
            this.chk_7 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_7);
            this.groupBox1.Controls.Add(this.chk_6);
            this.groupBox1.Controls.Add(this.chk_5);
            this.groupBox1.Controls.Add(this.chk_4);
            this.groupBox1.Controls.Add(this.chk_3);
            this.groupBox1.Controls.Add(this.chk_2);
            this.groupBox1.Controls.Add(this.chk_1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 190);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Analysis";
            // 
            // btn_process
            // 
            this.btn_process.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_process.Location = new System.Drawing.Point(64, 208);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(97, 26);
            this.btn_process.TabIndex = 1;
            this.btn_process.Text = "Process";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(167, 208);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(97, 26);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // chk_1
            // 
            this.chk_1.AutoSize = true;
            this.chk_1.Checked = true;
            this.chk_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_1.Location = new System.Drawing.Point(27, 21);
            this.chk_1.Name = "chk_1";
            this.chk_1.Size = new System.Drawing.Size(148, 18);
            this.chk_1.TabIndex = 2;
            this.chk_1.Text = "Process All Analysis";
            this.chk_1.UseVisualStyleBackColor = true;
            this.chk_1.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_2
            // 
            this.chk_2.AutoSize = true;
            this.chk_2.Checked = true;
            this.chk_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_2.Location = new System.Drawing.Point(27, 45);
            this.chk_2.Name = "chk_2";
            this.chk_2.Size = new System.Drawing.Size(281, 18);
            this.chk_2.TabIndex = 2;
            this.chk_2.Text = "Analysis DL+SIDL 3 CONTINUOUS SPANS";
            this.chk_2.UseVisualStyleBackColor = true;
            this.chk_2.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_3
            // 
            this.chk_3.AutoSize = true;
            this.chk_3.Checked = true;
            this.chk_3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_3.Location = new System.Drawing.Point(27, 69);
            this.chk_3.Name = "chk_3";
            this.chk_3.Size = new System.Drawing.Size(202, 18);
            this.chk_3.TabIndex = 2;
            this.chk_3.Text = "Analysis DL+SIDL END SPAN";
            this.chk_3.UseVisualStyleBackColor = true;
            this.chk_3.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_4
            // 
            this.chk_4.AutoSize = true;
            this.chk_4.Checked = true;
            this.chk_4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_4.Location = new System.Drawing.Point(27, 93);
            this.chk_4.Name = "chk_4";
            this.chk_4.Size = new System.Drawing.Size(200, 18);
            this.chk_4.TabIndex = 2;
            this.chk_4.Text = "Analysis DL+SIDL MID SPAN";
            this.chk_4.UseVisualStyleBackColor = true;
            this.chk_4.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_5
            // 
            this.chk_5.AutoSize = true;
            this.chk_5.Checked = true;
            this.chk_5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_5.Location = new System.Drawing.Point(27, 117);
            this.chk_5.Name = "chk_5";
            this.chk_5.Size = new System.Drawing.Size(244, 18);
            this.chk_5.TabIndex = 2;
            this.chk_5.Text = "Analysis DS 3 CONTINUOUS SPANS";
            this.chk_5.UseVisualStyleBackColor = true;
            this.chk_5.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_6
            // 
            this.chk_6.AutoSize = true;
            this.chk_6.Checked = true;
            this.chk_6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_6.Location = new System.Drawing.Point(27, 141);
            this.chk_6.Name = "chk_6";
            this.chk_6.Size = new System.Drawing.Size(256, 18);
            this.chk_6.TabIndex = 2;
            this.chk_6.Text = "Analysis FPLL 3 CONTINUOUS SPANS";
            this.chk_6.UseVisualStyleBackColor = true;
            this.chk_6.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // chk_7
            // 
            this.chk_7.AutoSize = true;
            this.chk_7.Checked = true;
            this.chk_7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_7.Location = new System.Drawing.Point(27, 165);
            this.chk_7.Name = "chk_7";
            this.chk_7.Size = new System.Drawing.Size(241, 18);
            this.chk_7.TabIndex = 2;
            this.chk_7.Text = "Analysis LL 3 CONTINUOUS SPANS";
            this.chk_7.UseVisualStyleBackColor = true;
            this.chk_7.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // frm_Select_Analysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 237);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_process);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Select_Analysis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Continuous PSC Box Girder Analysis";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.CheckBox chk_7;
        private System.Windows.Forms.CheckBox chk_6;
        private System.Windows.Forms.CheckBox chk_5;
        private System.Windows.Forms.CheckBox chk_4;
        private System.Windows.Forms.CheckBox chk_3;
        private System.Windows.Forms.CheckBox chk_2;
        private System.Windows.Forms.CheckBox chk_1;
    }
}