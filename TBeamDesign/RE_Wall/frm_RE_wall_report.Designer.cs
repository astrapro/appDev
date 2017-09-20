namespace BridgeAnalysisDesign.RE_Wall
{
    partial class frm_RE_wall_report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RE_wall_report));
            this.rbtn_details = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.rbtn_summary = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtn_details
            // 
            this.rbtn_details.AutoSize = true;
            this.rbtn_details.Location = new System.Drawing.Point(30, 26);
            this.rbtn_details.Name = "rbtn_details";
            this.rbtn_details.Size = new System.Drawing.Size(311, 22);
            this.rbtn_details.TabIndex = 0;
            this.rbtn_details.TabStop = true;
            this.rbtn_details.Text = "RE Wall Details Calculation Report";
            this.rbtn_details.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_open);
            this.groupBox1.Controls.Add(this.rbtn_summary);
            this.groupBox1.Controls.Add(this.rbtn_details);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 127);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Report Type";
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(91, 87);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(156, 30);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // rbtn_summary
            // 
            this.rbtn_summary.AutoSize = true;
            this.rbtn_summary.Location = new System.Drawing.Point(30, 53);
            this.rbtn_summary.Name = "rbtn_summary";
            this.rbtn_summary.Size = new System.Drawing.Size(282, 22);
            this.rbtn_summary.TabIndex = 0;
            this.rbtn_summary.TabStop = true;
            this.rbtn_summary.Text = "RE Wall Table Summary Report";
            this.rbtn_summary.UseVisualStyleBackColor = true;
            // 
            // frm_RE_wall_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 158);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_RE_wall_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Pro";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_details;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.RadioButton rbtn_summary;
    }
}