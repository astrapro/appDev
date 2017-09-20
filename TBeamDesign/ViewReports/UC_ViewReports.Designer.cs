namespace BridgeAnalysisDesign.ViewReports
{
    partial class UC_ViewReports
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.trv_reports = new System.Windows.Forms.TreeView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_open_file = new System.Windows.Forms.Button();
            this.rtb_rep_file = new System.Windows.Forms.RichTextBox();
            this.lbl_rep_file = new System.Windows.Forms.Label();
            this.btn_open_folder = new System.Windows.Forms.Button();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.trv_reports);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox8);
            this.splitContainer3.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.rtb_rep_file);
            this.splitContainer3.Panel2.Controls.Add(this.lbl_rep_file);
            this.splitContainer3.Size = new System.Drawing.Size(691, 531);
            this.splitContainer3.SplitterDistance = 227;
            this.splitContainer3.TabIndex = 1;
            // 
            // trv_reports
            // 
            this.trv_reports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_reports.Location = new System.Drawing.Point(0, 44);
            this.trv_reports.Name = "trv_reports";
            this.trv_reports.Size = new System.Drawing.Size(225, 399);
            this.trv_reports.TabIndex = 2;
            this.trv_reports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_reports_AfterSelect);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.comboBox1);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(225, 44);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Select Analysis";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_open_folder);
            this.panel5.Controls.Add(this.btn_open_file);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 443);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(225, 86);
            this.panel5.TabIndex = 4;
            // 
            // btn_open_file
            // 
            this.btn_open_file.Location = new System.Drawing.Point(3, 6);
            this.btn_open_file.Name = "btn_open_file";
            this.btn_open_file.Size = new System.Drawing.Size(182, 29);
            this.btn_open_file.TabIndex = 0;
            this.btn_open_file.Text = "Open File";
            this.btn_open_file.UseVisualStyleBackColor = true;
            this.btn_open_file.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // rtb_rep_file
            // 
            this.rtb_rep_file.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_rep_file.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_rep_file.Location = new System.Drawing.Point(0, 27);
            this.rtb_rep_file.Name = "rtb_rep_file";
            this.rtb_rep_file.Size = new System.Drawing.Size(458, 502);
            this.rtb_rep_file.TabIndex = 1;
            this.rtb_rep_file.Text = "";
            this.rtb_rep_file.WordWrap = false;
            // 
            // lbl_rep_file
            // 
            this.lbl_rep_file.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_rep_file.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_rep_file.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rep_file.Location = new System.Drawing.Point(0, 0);
            this.lbl_rep_file.Name = "lbl_rep_file";
            this.lbl_rep_file.Size = new System.Drawing.Size(458, 27);
            this.lbl_rep_file.TabIndex = 0;
            this.lbl_rep_file.Text = "File :";
            this.lbl_rep_file.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_open_folder
            // 
            this.btn_open_folder.Location = new System.Drawing.Point(3, 41);
            this.btn_open_folder.Name = "btn_open_folder";
            this.btn_open_folder.Size = new System.Drawing.Size(182, 29);
            this.btn_open_folder.TabIndex = 0;
            this.btn_open_folder.Text = "Open Folder";
            this.btn_open_folder.UseVisualStyleBackColor = true;
            this.btn_open_folder.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // UC_ViewReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_ViewReports";
            this.Size = new System.Drawing.Size(691, 531);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView trv_reports;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_open_file;
        private System.Windows.Forms.RichTextBox rtb_rep_file;
        private System.Windows.Forms.Label lbl_rep_file;
        private System.Windows.Forms.Button btn_open_folder;
    }
}
