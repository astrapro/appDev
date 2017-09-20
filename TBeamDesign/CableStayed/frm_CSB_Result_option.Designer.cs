namespace BridgeAnalysisDesign.CableStayed
{
    partial class frm_CSB_Result_option
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
            this.rbtn_Full_Analysis_report = new System.Windows.Forms.RadioButton();
            this.rbtn_Analysis_result = new System.Windows.Forms.RadioButton();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.rbtn_cbl_Analysis_result = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_cbl_Analysis_result);
            this.groupBox1.Controls.Add(this.rbtn_Full_Analysis_report);
            this.groupBox1.Controls.Add(this.rbtn_Analysis_result);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 98);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Result View Option";
            // 
            // rbtn_Full_Analysis_report
            // 
            this.rbtn_Full_Analysis_report.AutoSize = true;
            this.rbtn_Full_Analysis_report.Location = new System.Drawing.Point(22, 21);
            this.rbtn_Full_Analysis_report.Name = "rbtn_Full_Analysis_report";
            this.rbtn_Full_Analysis_report.Size = new System.Drawing.Size(122, 18);
            this.rbtn_Full_Analysis_report.TabIndex = 1;
            this.rbtn_Full_Analysis_report.Text = "Analysis Report";
            this.rbtn_Full_Analysis_report.UseVisualStyleBackColor = true;
            // 
            // rbtn_Analysis_result
            // 
            this.rbtn_Analysis_result.AutoSize = true;
            this.rbtn_Analysis_result.Location = new System.Drawing.Point(22, 45);
            this.rbtn_Analysis_result.Name = "rbtn_Analysis_result";
            this.rbtn_Analysis_result.Size = new System.Drawing.Size(119, 18);
            this.rbtn_Analysis_result.TabIndex = 0;
            this.rbtn_Analysis_result.Text = "Analysis Result";
            this.rbtn_Analysis_result.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(136, 126);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(12, 126);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // rbtn_cbl_Analysis_result
            // 
            this.rbtn_cbl_Analysis_result.AutoSize = true;
            this.rbtn_cbl_Analysis_result.Checked = true;
            this.rbtn_cbl_Analysis_result.Location = new System.Drawing.Point(22, 69);
            this.rbtn_cbl_Analysis_result.Name = "rbtn_cbl_Analysis_result";
            this.rbtn_cbl_Analysis_result.Size = new System.Drawing.Size(159, 18);
            this.rbtn_cbl_Analysis_result.TabIndex = 2;
            this.rbtn_cbl_Analysis_result.TabStop = true;
            this.rbtn_cbl_Analysis_result.Text = "Cable Analysis Result";
            this.rbtn_cbl_Analysis_result.UseVisualStyleBackColor = true;
            // 
            // frm_CSB_Result_option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 170);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_CSB_Result_option";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result Option";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_Full_Analysis_report;
        private System.Windows.Forms.RadioButton rbtn_Analysis_result;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.RadioButton rbtn_cbl_Analysis_result;
    }
}