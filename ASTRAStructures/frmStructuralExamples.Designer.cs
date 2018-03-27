namespace ASTRAStructures
{
    partial class frmStructuralExamples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStructuralExamples));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_dwg = new System.Windows.Forms.RadioButton();
            this.rbtn_Stage = new System.Windows.Forms.RadioButton();
            this.rbtn_TEXT = new System.Windows.Forms.RadioButton();
            this.rbtn_SAP = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_examples = new System.Windows.Forms.ComboBox();
            this.chk_show_processed_files = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_copy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_files = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_open_input = new System.Windows.Forms.Button();
            this.btn_open_analysis = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_expend = new System.Windows.Forms.CheckBox();
            this.sc_file = new System.Windows.Forms.SplitContainer();
            this.rtb_input_files = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_fname = new System.Windows.Forms.Label();
            this.uC_CAD1 = new AstraAccess.ADOC.UC_CAD();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.sc_file.Panel1.SuspendLayout();
            this.sc_file.Panel2.SuspendLayout();
            this.sc_file.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmb_examples);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(955, 50);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_dwg);
            this.groupBox1.Controls.Add(this.rbtn_Stage);
            this.groupBox1.Controls.Add(this.rbtn_TEXT);
            this.groupBox1.Controls.Add(this.rbtn_SAP);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 43);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Example Data Files";
            // 
            // rbtn_dwg
            // 
            this.rbtn_dwg.AutoSize = true;
            this.rbtn_dwg.Location = new System.Drawing.Point(139, 20);
            this.rbtn_dwg.Name = "rbtn_dwg";
            this.rbtn_dwg.Size = new System.Drawing.Size(83, 17);
            this.rbtn_dwg.TabIndex = 1;
            this.rbtn_dwg.Text = "DRAWING";
            this.rbtn_dwg.UseVisualStyleBackColor = true;
            this.rbtn_dwg.CheckedChanged += new System.EventHandler(this.rbtn_TEXT_CheckedChanged);
            // 
            // rbtn_Stage
            // 
            this.rbtn_Stage.AutoSize = true;
            this.rbtn_Stage.Location = new System.Drawing.Point(232, 20);
            this.rbtn_Stage.Name = "rbtn_Stage";
            this.rbtn_Stage.Size = new System.Drawing.Size(124, 17);
            this.rbtn_Stage.TabIndex = 1;
            this.rbtn_Stage.Text = "STAGE ANALYSIS";
            this.rbtn_Stage.UseVisualStyleBackColor = true;
            this.rbtn_Stage.Visible = false;
            this.rbtn_Stage.CheckedChanged += new System.EventHandler(this.rbtn_TEXT_CheckedChanged);
            // 
            // rbtn_TEXT
            // 
            this.rbtn_TEXT.AutoSize = true;
            this.rbtn_TEXT.Checked = true;
            this.rbtn_TEXT.Location = new System.Drawing.Point(5, 20);
            this.rbtn_TEXT.Name = "rbtn_TEXT";
            this.rbtn_TEXT.Size = new System.Drawing.Size(54, 17);
            this.rbtn_TEXT.TabIndex = 0;
            this.rbtn_TEXT.TabStop = true;
            this.rbtn_TEXT.Text = "TEXT";
            this.rbtn_TEXT.UseVisualStyleBackColor = true;
            this.rbtn_TEXT.CheckedChanged += new System.EventHandler(this.rbtn_TEXT_CheckedChanged);
            // 
            // rbtn_SAP
            // 
            this.rbtn_SAP.AutoSize = true;
            this.rbtn_SAP.Location = new System.Drawing.Point(79, 20);
            this.rbtn_SAP.Name = "rbtn_SAP";
            this.rbtn_SAP.Size = new System.Drawing.Size(48, 17);
            this.rbtn_SAP.TabIndex = 0;
            this.rbtn_SAP.Text = "SAP";
            this.rbtn_SAP.UseVisualStyleBackColor = true;
            this.rbtn_SAP.CheckedChanged += new System.EventHandler(this.rbtn_TEXT_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(386, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select ASTRA Pro Analysis Examples";
            // 
            // cmb_examples
            // 
            this.cmb_examples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_examples.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_examples.FormattingEnabled = true;
            this.cmb_examples.Location = new System.Drawing.Point(389, 19);
            this.cmb_examples.Name = "cmb_examples";
            this.cmb_examples.Size = new System.Drawing.Size(553, 22);
            this.cmb_examples.TabIndex = 1;
            this.cmb_examples.SelectedIndexChanged += new System.EventHandler(this.cmb_examples_SelectedIndexChanged);
            // 
            // chk_show_processed_files
            // 
            this.chk_show_processed_files.AutoSize = true;
            this.chk_show_processed_files.Location = new System.Drawing.Point(112, 3);
            this.chk_show_processed_files.Name = "chk_show_processed_files";
            this.chk_show_processed_files.Size = new System.Drawing.Size(148, 17);
            this.chk_show_processed_files.TabIndex = 1;
            this.chk_show_processed_files.Text = "Show Processed Files";
            this.chk_show_processed_files.UseVisualStyleBackColor = true;
            this.chk_show_processed_files.CheckedChanged += new System.EventHandler(this.chk_show_processed_files_CheckedChanged);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(247, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(187, 38);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Save Data to File";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_copy
            // 
            this.btn_copy.Location = new System.Drawing.Point(52, 3);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(189, 38);
            this.btn_copy.TabIndex = 0;
            this.btn_copy.Text = "Copy File to Working Folder";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_files);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sc_file);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_fname);
            this.splitContainer1.Size = new System.Drawing.Size(955, 541);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // tv_files
            // 
            this.tv_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_files.Location = new System.Drawing.Point(0, 25);
            this.tv_files.Name = "tv_files";
            this.tv_files.Size = new System.Drawing.Size(382, 468);
            this.tv_files.TabIndex = 0;
            this.tv_files.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_files_AfterSelect);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_open_input);
            this.panel3.Controls.Add(this.btn_open_analysis);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 493);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 46);
            this.panel3.TabIndex = 2;
            // 
            // btn_open_input
            // 
            this.btn_open_input.Location = new System.Drawing.Point(3, 3);
            this.btn_open_input.Name = "btn_open_input";
            this.btn_open_input.Size = new System.Drawing.Size(177, 38);
            this.btn_open_input.TabIndex = 0;
            this.btn_open_input.Text = "Open Analysis Input Data";
            this.btn_open_input.UseVisualStyleBackColor = true;
            this.btn_open_input.Visible = false;
            this.btn_open_input.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_open_analysis
            // 
            this.btn_open_analysis.Location = new System.Drawing.Point(186, 3);
            this.btn_open_analysis.Name = "btn_open_analysis";
            this.btn_open_analysis.Size = new System.Drawing.Size(183, 38);
            this.btn_open_analysis.TabIndex = 0;
            this.btn_open_analysis.Text = "Analysis Process && Results";
            this.btn_open_analysis.UseVisualStyleBackColor = true;
            this.btn_open_analysis.Visible = false;
            this.btn_open_analysis.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chk_expend);
            this.panel2.Controls.Add(this.chk_show_processed_files);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 25);
            this.panel2.TabIndex = 1;
            // 
            // chk_expend
            // 
            this.chk_expend.AutoSize = true;
            this.chk_expend.Location = new System.Drawing.Point(20, 3);
            this.chk_expend.Name = "chk_expend";
            this.chk_expend.Size = new System.Drawing.Size(86, 17);
            this.chk_expend.TabIndex = 1;
            this.chk_expend.Text = "Expand All";
            this.chk_expend.UseVisualStyleBackColor = true;
            this.chk_expend.CheckedChanged += new System.EventHandler(this.chk_show_processed_files_CheckedChanged);
            // 
            // sc_file
            // 
            this.sc_file.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_file.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_file.Location = new System.Drawing.Point(0, 25);
            this.sc_file.Name = "sc_file";
            // 
            // sc_file.Panel1
            // 
            this.sc_file.Panel1.Controls.Add(this.uC_CAD1);
            this.sc_file.Panel1Collapsed = true;
            // 
            // sc_file.Panel2
            // 
            this.sc_file.Panel2.Controls.Add(this.rtb_input_files);
            this.sc_file.Size = new System.Drawing.Size(566, 470);
            this.sc_file.SplitterDistance = 188;
            this.sc_file.TabIndex = 4;
            // 
            // rtb_input_files
            // 
            this.rtb_input_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_input_files.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_input_files.Location = new System.Drawing.Point(0, 0);
            this.rtb_input_files.Name = "rtb_input_files";
            this.rtb_input_files.Size = new System.Drawing.Size(564, 468);
            this.rtb_input_files.TabIndex = 0;
            this.rtb_input_files.Text = "";
            this.rtb_input_files.WordWrap = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btn_copy);
            this.panel4.Controls.Add(this.btn_close);
            this.panel4.Controls.Add(this.btn_save);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 495);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(566, 46);
            this.panel4.TabIndex = 3;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(440, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 38);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_fname
            // 
            this.lbl_fname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_fname.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_fname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fname.Location = new System.Drawing.Point(0, 0);
            this.lbl_fname.Name = "lbl_fname";
            this.lbl_fname.Size = new System.Drawing.Size(566, 25);
            this.lbl_fname.TabIndex = 1;
            this.lbl_fname.Text = "File_Name";
            this.lbl_fname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uC_CAD1
            // 
            this.uC_CAD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_CAD1.Location = new System.Drawing.Point(0, 0);
            this.uC_CAD1.Name = "uC_CAD1";
            this.uC_CAD1.Size = new System.Drawing.Size(186, 98);
            this.uC_CAD1.TabIndex = 0;
            this.uC_CAD1.View_Buttons = false;
            // 
            // frmStructuralExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 591);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStructuralExamples";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Pro Analysis Examples";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStructuralExamples_FormClosing);
            this.Load += new System.EventHandler(this.frmStructuralExamples_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.sc_file.Panel1.ResumeLayout(false);
            this.sc_file.Panel2.ResumeLayout(false);
            this.sc_file.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.TreeView tv_files;
        private System.Windows.Forms.RichTextBox rtb_input_files;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox chk_show_processed_files;
        private System.Windows.Forms.Label lbl_fname;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmb_examples;
        private System.Windows.Forms.CheckBox chk_expend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_open_analysis;
        private System.Windows.Forms.Button btn_open_input;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer sc_file;
        private AstraAccess.ADOC.UC_CAD uC_CAD1;
        public System.Windows.Forms.RadioButton rbtn_TEXT;
        public System.Windows.Forms.RadioButton rbtn_SAP;
        public System.Windows.Forms.RadioButton rbtn_Stage;
        public System.Windows.Forms.RadioButton rbtn_dwg;
    }

}