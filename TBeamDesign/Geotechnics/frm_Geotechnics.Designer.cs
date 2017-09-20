namespace BridgeAnalysisDesign.Geotechnics
{
    partial class frm_Geotechnics
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
            this.btn_CBR_Test = new System.Windows.Forms.Button();
            this.grb_worksheet = new System.Windows.Forms.GroupBox();
            this.btn_summary_lab_test = new System.Windows.Forms.Button();
            this.btn_proctor_density_test = new System.Windows.Forms.Button();
            this.btn_grain_size_analysis = new System.Windows.Forms.Button();
            this.btn_working_folder = new System.Windows.Forms.Button();
            this.grb_worksheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_CBR_Test
            // 
            this.btn_CBR_Test.Location = new System.Drawing.Point(6, 20);
            this.btn_CBR_Test.Name = "btn_CBR_Test";
            this.btn_CBR_Test.Size = new System.Drawing.Size(332, 42);
            this.btn_CBR_Test.TabIndex = 0;
            this.btn_CBR_Test.Tag = "CBR = California Bearing Ratio";
            this.btn_CBR_Test.Text = "CBR Test";
            this.btn_CBR_Test.UseVisualStyleBackColor = true;
            this.btn_CBR_Test.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // grb_worksheet
            // 
            this.grb_worksheet.Controls.Add(this.btn_summary_lab_test);
            this.grb_worksheet.Controls.Add(this.btn_proctor_density_test);
            this.grb_worksheet.Controls.Add(this.btn_grain_size_analysis);
            this.grb_worksheet.Controls.Add(this.btn_CBR_Test);
            this.grb_worksheet.Enabled = false;
            this.grb_worksheet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_worksheet.Location = new System.Drawing.Point(8, 50);
            this.grb_worksheet.Name = "grb_worksheet";
            this.grb_worksheet.Size = new System.Drawing.Size(353, 212);
            this.grb_worksheet.TabIndex = 1;
            this.grb_worksheet.TabStop = false;
            // 
            // btn_summary_lab_test
            // 
            this.btn_summary_lab_test.Location = new System.Drawing.Point(6, 164);
            this.btn_summary_lab_test.Name = "btn_summary_lab_test";
            this.btn_summary_lab_test.Size = new System.Drawing.Size(332, 42);
            this.btn_summary_lab_test.TabIndex = 3;
            this.btn_summary_lab_test.Text = "Summary Lab Test";
            this.btn_summary_lab_test.UseVisualStyleBackColor = true;
            this.btn_summary_lab_test.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_proctor_density_test
            // 
            this.btn_proctor_density_test.Location = new System.Drawing.Point(6, 116);
            this.btn_proctor_density_test.Name = "btn_proctor_density_test";
            this.btn_proctor_density_test.Size = new System.Drawing.Size(332, 42);
            this.btn_proctor_density_test.TabIndex = 2;
            this.btn_proctor_density_test.Text = "Proctor Density Test";
            this.btn_proctor_density_test.UseVisualStyleBackColor = true;
            this.btn_proctor_density_test.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_grain_size_analysis
            // 
            this.btn_grain_size_analysis.Location = new System.Drawing.Point(6, 68);
            this.btn_grain_size_analysis.Name = "btn_grain_size_analysis";
            this.btn_grain_size_analysis.Size = new System.Drawing.Size(332, 42);
            this.btn_grain_size_analysis.TabIndex = 1;
            this.btn_grain_size_analysis.Text = "Grain Size Analysis Test";
            this.btn_grain_size_analysis.UseVisualStyleBackColor = true;
            this.btn_grain_size_analysis.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // btn_working_folder
            // 
            this.btn_working_folder.Location = new System.Drawing.Point(18, 14);
            this.btn_working_folder.Name = "btn_working_folder";
            this.btn_working_folder.Size = new System.Drawing.Size(332, 30);
            this.btn_working_folder.TabIndex = 4;
            this.btn_working_folder.Tag = "CBR = California Bearing Ratio";
            this.btn_working_folder.Text = "Working Folder";
            this.btn_working_folder.UseVisualStyleBackColor = true;
            this.btn_working_folder.Click += new System.EventHandler(this.btn_working_folder_Click);
            // 
            // frm_Geotechnics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 286);
            this.Controls.Add(this.btn_working_folder);
            this.Controls.Add(this.grb_worksheet);
            this.MaximizeBox = false;
            this.Name = "frm_Geotechnics";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geotechnics";
            this.grb_worksheet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_CBR_Test;
        private System.Windows.Forms.GroupBox grb_worksheet;
        private System.Windows.Forms.Button btn_summary_lab_test;
        private System.Windows.Forms.Button btn_proctor_density_test;
        private System.Windows.Forms.Button btn_grain_size_analysis;
        private System.Windows.Forms.Button btn_working_folder;
    }
}