namespace BridgeAnalysisDesign
{
    partial class frm_Result_Option
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.grb_1 = new System.Windows.Forms.GroupBox();
            this.rbtn_Full_Analysis_report = new System.Windows.Forms.RadioButton();
            this.rbtn_Analysis_result = new System.Windows.Forms.RadioButton();
            this.grb_2 = new System.Windows.Forms.GroupBox();
            this.rbtn_dead_load = new System.Windows.Forms.RadioButton();
            this.rbtn_live_load = new System.Windows.Forms.RadioButton();
            this.rbtn_total_load = new System.Windows.Forms.RadioButton();
            this.rbtn_ana_res = new System.Windows.Forms.RadioButton();
            this.grb_1.SuspendLayout();
            this.grb_2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(23, 132);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(147, 132);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // grb_1
            // 
            this.grb_1.Controls.Add(this.rbtn_Full_Analysis_report);
            this.grb_1.Controls.Add(this.rbtn_Analysis_result);
            this.grb_1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_1.Location = new System.Drawing.Point(23, 22);
            this.grb_1.Name = "grb_1";
            this.grb_1.Size = new System.Drawing.Size(199, 94);
            this.grb_1.TabIndex = 2;
            this.grb_1.TabStop = false;
            this.grb_1.Text = "Select Result View Option";
            // 
            // rbtn_Full_Analysis_report
            // 
            this.rbtn_Full_Analysis_report.AutoSize = true;
            this.rbtn_Full_Analysis_report.Location = new System.Drawing.Point(22, 31);
            this.rbtn_Full_Analysis_report.Name = "rbtn_Full_Analysis_report";
            this.rbtn_Full_Analysis_report.Size = new System.Drawing.Size(147, 18);
            this.rbtn_Full_Analysis_report.TabIndex = 1;
            this.rbtn_Full_Analysis_report.Text = "Full Analysis Report";
            this.rbtn_Full_Analysis_report.UseVisualStyleBackColor = true;
            // 
            // rbtn_Analysis_result
            // 
            this.rbtn_Analysis_result.AutoSize = true;
            this.rbtn_Analysis_result.Checked = true;
            this.rbtn_Analysis_result.Location = new System.Drawing.Point(22, 55);
            this.rbtn_Analysis_result.Name = "rbtn_Analysis_result";
            this.rbtn_Analysis_result.Size = new System.Drawing.Size(119, 18);
            this.rbtn_Analysis_result.TabIndex = 0;
            this.rbtn_Analysis_result.TabStop = true;
            this.rbtn_Analysis_result.Text = "Analysis Result";
            this.rbtn_Analysis_result.UseVisualStyleBackColor = true;
            // 
            // grb_2
            // 
            this.grb_2.Controls.Add(this.rbtn_dead_load);
            this.grb_2.Controls.Add(this.rbtn_live_load);
            this.grb_2.Controls.Add(this.rbtn_total_load);
            this.grb_2.Controls.Add(this.rbtn_ana_res);
            this.grb_2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_2.Location = new System.Drawing.Point(12, 12);
            this.grb_2.Name = "grb_2";
            this.grb_2.Size = new System.Drawing.Size(223, 114);
            this.grb_2.TabIndex = 3;
            this.grb_2.TabStop = false;
            this.grb_2.Text = "Select Result View Option";
            // 
            // rbtn_dead_load
            // 
            this.rbtn_dead_load.AutoSize = true;
            this.rbtn_dead_load.Location = new System.Drawing.Point(7, 66);
            this.rbtn_dead_load.Name = "rbtn_dead_load";
            this.rbtn_dead_load.Size = new System.Drawing.Size(194, 18);
            this.rbtn_dead_load.TabIndex = 2;
            this.rbtn_dead_load.Text = "Dead Load Analysis Report";
            this.rbtn_dead_load.UseVisualStyleBackColor = true;
            // 
            // rbtn_live_load
            // 
            this.rbtn_live_load.AutoSize = true;
            this.rbtn_live_load.Location = new System.Drawing.Point(7, 45);
            this.rbtn_live_load.Name = "rbtn_live_load";
            this.rbtn_live_load.Size = new System.Drawing.Size(186, 18);
            this.rbtn_live_load.TabIndex = 2;
            this.rbtn_live_load.Text = "Live Load Analysis Report";
            this.rbtn_live_load.UseVisualStyleBackColor = true;
            // 
            // rbtn_total_load
            // 
            this.rbtn_total_load.AutoSize = true;
            this.rbtn_total_load.Checked = true;
            this.rbtn_total_load.Location = new System.Drawing.Point(7, 21);
            this.rbtn_total_load.Name = "rbtn_total_load";
            this.rbtn_total_load.Size = new System.Drawing.Size(192, 18);
            this.rbtn_total_load.TabIndex = 1;
            this.rbtn_total_load.TabStop = true;
            this.rbtn_total_load.Text = "Total Load Analysis Report";
            this.rbtn_total_load.UseVisualStyleBackColor = true;
            // 
            // rbtn_ana_res
            // 
            this.rbtn_ana_res.AutoSize = true;
            this.rbtn_ana_res.Location = new System.Drawing.Point(7, 90);
            this.rbtn_ana_res.Name = "rbtn_ana_res";
            this.rbtn_ana_res.Size = new System.Drawing.Size(119, 18);
            this.rbtn_ana_res.TabIndex = 0;
            this.rbtn_ana_res.Text = "Analysis Result";
            this.rbtn_ana_res.UseVisualStyleBackColor = true;
            // 
            // frm_Result_Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 163);
            this.ControlBox = false;
            this.Controls.Add(this.grb_2);
            this.Controls.Add(this.grb_1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_Result_Option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result Option";
            this.Load += new System.EventHandler(this.frm_Result_Option_Load);
            this.grb_1.ResumeLayout(false);
            this.grb_1.PerformLayout();
            this.grb_2.ResumeLayout(false);
            this.grb_2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox grb_1;
        private System.Windows.Forms.RadioButton rbtn_Full_Analysis_report;
        private System.Windows.Forms.RadioButton rbtn_Analysis_result;
        private System.Windows.Forms.GroupBox grb_2;
        private System.Windows.Forms.RadioButton rbtn_total_load;
        private System.Windows.Forms.RadioButton rbtn_dead_load;
        private System.Windows.Forms.RadioButton rbtn_live_load;
        public System.Windows.Forms.RadioButton rbtn_ana_res;
    }
}