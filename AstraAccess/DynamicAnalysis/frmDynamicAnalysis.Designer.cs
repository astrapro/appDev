namespace AstraAccess.DynamicAnalysis
{
    //partial class frmDynamicAnalysis
       partial class frmDynamicAnalysis
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.btn_open_design = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label830 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtb_input_data = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_open_analysis_report = new System.Windows.Forms.Button();
            this.btn_create_input_data = new System.Windows.Forms.Button();
            this.btn_process_analysis = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_input_browse = new System.Windows.Forms.Button();
            this.txt_input_data = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_analysis_results = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel30.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel30);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 59);
            this.panel1.TabIndex = 0;
            // 
            // panel30
            // 
            this.panel30.Controls.Add(this.btn_new_design);
            this.panel30.Controls.Add(this.btn_open_design);
            this.panel30.Controls.Add(this.txt_project_name);
            this.panel30.Controls.Add(this.label830);
            this.panel30.Location = new System.Drawing.Point(12, 12);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(773, 37);
            this.panel30.TabIndex = 183;
            // 
            // btn_new_design
            // 
            this.btn_new_design.Location = new System.Drawing.Point(451, 10);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(96, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "New Design";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_open_design
            // 
            this.btn_open_design.Location = new System.Drawing.Point(553, 10);
            this.btn_open_design.Name = "btn_open_design";
            this.btn_open_design.Size = new System.Drawing.Size(96, 24);
            this.btn_open_design.TabIndex = 189;
            this.btn_open_design.Text = "Open Design";
            this.btn_open_design.UseVisualStyleBackColor = true;
            this.btn_open_design.Click += new System.EventHandler(this.btn_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(102, 12);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(341, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label830
            // 
            this.label830.AutoSize = true;
            this.label830.Location = new System.Drawing.Point(3, 16);
            this.label830.Name = "label830";
            this.label830.Size = new System.Drawing.Size(93, 13);
            this.label830.TabIndex = 187;
            this.label830.Text = "Project Name :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 59);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtb_input_data);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.rtb_analysis_results);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(885, 409);
            this.splitContainer1.SplitterDistance = 467;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // rtb_input_data
            // 
            this.rtb_input_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_input_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_input_data.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_input_data.Location = new System.Drawing.Point(0, 63);
            this.rtb_input_data.Name = "rtb_input_data";
            this.rtb_input_data.Size = new System.Drawing.Size(465, 309);
            this.rtb_input_data.TabIndex = 2;
            this.rtb_input_data.Text = "";
            this.rtb_input_data.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_create_input_data);
            this.panel3.Controls.Add(this.btn_process_analysis);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 372);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(465, 35);
            this.panel3.TabIndex = 1;
            // 
            // btn_open_analysis_report
            // 
            this.btn_open_analysis_report.Location = new System.Drawing.Point(134, 5);
            this.btn_open_analysis_report.Name = "btn_open_analysis_report";
            this.btn_open_analysis_report.Size = new System.Drawing.Size(142, 24);
            this.btn_open_analysis_report.TabIndex = 191;
            this.btn_open_analysis_report.Text = "Open Analysis Report";
            this.btn_open_analysis_report.UseVisualStyleBackColor = true;
            this.btn_open_analysis_report.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_create_input_data
            // 
            this.btn_create_input_data.Location = new System.Drawing.Point(55, 6);
            this.btn_create_input_data.Name = "btn_create_input_data";
            this.btn_create_input_data.Size = new System.Drawing.Size(142, 24);
            this.btn_create_input_data.TabIndex = 191;
            this.btn_create_input_data.Text = "Create Input Data";
            this.btn_create_input_data.UseVisualStyleBackColor = true;
            this.btn_create_input_data.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_process_analysis
            // 
            this.btn_process_analysis.Location = new System.Drawing.Point(235, 6);
            this.btn_process_analysis.Name = "btn_process_analysis";
            this.btn_process_analysis.Size = new System.Drawing.Size(142, 24);
            this.btn_process_analysis.TabIndex = 191;
            this.btn_process_analysis.Text = "Process Analysis";
            this.btn_process_analysis.UseVisualStyleBackColor = true;
            this.btn_process_analysis.Click += new System.EventHandler(this.btn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_input_browse);
            this.panel2.Controls.Add(this.txt_input_data);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(465, 63);
            this.panel2.TabIndex = 0;
            // 
            // btn_input_browse
            // 
            this.btn_input_browse.Location = new System.Drawing.Point(351, 21);
            this.btn_input_browse.Name = "btn_input_browse";
            this.btn_input_browse.Size = new System.Drawing.Size(80, 24);
            this.btn_input_browse.TabIndex = 191;
            this.btn_input_browse.Text = "Browse";
            this.btn_input_browse.UseVisualStyleBackColor = true;
            this.btn_input_browse.Click += new System.EventHandler(this.btn_Click);
            // 
            // txt_input_data
            // 
            this.txt_input_data.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_input_data.Location = new System.Drawing.Point(89, 21);
            this.txt_input_data.Name = "txt_input_data";
            this.txt_input_data.Size = new System.Drawing.Size(256, 22);
            this.txt_input_data.TabIndex = 189;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 190;
            this.label1.Text = "Input File :";
            // 
            // rtb_analysis_results
            // 
            this.rtb_analysis_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_analysis_results.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_analysis_results.Location = new System.Drawing.Point(0, 21);
            this.rtb_analysis_results.Name = "rtb_analysis_results";
            this.rtb_analysis_results.Size = new System.Drawing.Size(411, 386);
            this.rtb_analysis_results.TabIndex = 2;
            this.rtb_analysis_results.Text = "";
            this.rtb_analysis_results.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(411, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Analysis Report";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_open_analysis_report);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 372);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(411, 35);
            this.panel4.TabIndex = 4;
            // 
            // frmDynamicAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 468);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDynamicAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Response Spectrum Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDynamicAnalysis_Load);
            this.panel1.ResumeLayout(false);
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_open_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label830;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_input_browse;
        private System.Windows.Forms.TextBox txt_input_data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_open_analysis_report;
        private System.Windows.Forms.Button btn_process_analysis;
        private System.Windows.Forms.RichTextBox rtb_input_data;
        private System.Windows.Forms.RichTextBox rtb_analysis_results;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_create_input_data;
        private System.Windows.Forms.Panel panel4;
    }

}