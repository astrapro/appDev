namespace BridgeAnalysisDesign.Composite
{
    partial class frm_Connecting_Angles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Connecting_Angles));
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.cmb_ana_ang_section_name = new System.Windows.Forms.ComboBox();
            this.cmb_ana_nos_ang = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.cmb_ana_ang_section_code = new System.Windows.Forms.ComboBox();
            this.cmb_ana_ang_thk = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox24.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.cmb_ana_ang_section_name);
            this.groupBox24.Controls.Add(this.cmb_ana_nos_ang);
            this.groupBox24.Controls.Add(this.label8);
            this.groupBox24.Controls.Add(this.label123);
            this.groupBox24.Controls.Add(this.cmb_ana_ang_section_code);
            this.groupBox24.Controls.Add(this.cmb_ana_ang_thk);
            this.groupBox24.Location = new System.Drawing.Point(24, 51);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(437, 50);
            this.groupBox24.TabIndex = 102;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Angle && Angle Thickness";
            // 
            // cmb_ana_ang_section_name
            // 
            this.cmb_ana_ang_section_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ana_ang_section_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ana_ang_section_name.FormattingEnabled = true;
            this.cmb_ana_ang_section_name.Location = new System.Drawing.Point(70, 18);
            this.cmb_ana_ang_section_name.Name = "cmb_ana_ang_section_name";
            this.cmb_ana_ang_section_name.Size = new System.Drawing.Size(78, 22);
            this.cmb_ana_ang_section_name.TabIndex = 103;
            this.cmb_ana_ang_section_name.SelectedIndexChanged += new System.EventHandler(this.cmb_L_2_ang_section_name_SelectedIndexChanged);
            // 
            // cmb_ana_nos_ang
            // 
            this.cmb_ana_nos_ang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ana_nos_ang.FormattingEnabled = true;
            this.cmb_ana_nos_ang.Items.AddRange(new object[] {
            "4"});
            this.cmb_ana_nos_ang.Location = new System.Drawing.Point(6, 16);
            this.cmb_ana_nos_ang.Name = "cmb_ana_nos_ang";
            this.cmb_ana_nos_ang.Size = new System.Drawing.Size(43, 24);
            this.cmb_ana_nos_ang.TabIndex = 91;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(380, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 16);
            this.label8.TabIndex = 99;
            this.label8.Tag = "";
            this.label8.Text = "mm";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(296, 24);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(15, 13);
            this.label123.TabIndex = 76;
            this.label123.Text = "x";
            // 
            // cmb_ana_ang_section_code
            // 
            this.cmb_ana_ang_section_code.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ana_ang_section_code.FormattingEnabled = true;
            this.cmb_ana_ang_section_code.Location = new System.Drawing.Point(163, 19);
            this.cmb_ana_ang_section_code.Name = "cmb_ana_ang_section_code";
            this.cmb_ana_ang_section_code.Size = new System.Drawing.Size(127, 22);
            this.cmb_ana_ang_section_code.TabIndex = 87;
            this.cmb_ana_ang_section_code.SelectedIndexChanged += new System.EventHandler(this.cmb_ang_section_SelectedIndexChanged);
            // 
            // cmb_ana_ang_thk
            // 
            this.cmb_ana_ang_thk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ana_ang_thk.FormattingEnabled = true;
            this.cmb_ana_ang_thk.Location = new System.Drawing.Point(317, 19);
            this.cmb_ana_ang_thk.Name = "cmb_ana_ang_thk";
            this.cmb_ana_ang_thk.Size = new System.Drawing.Size(54, 22);
            this.cmb_ana_ang_thk.TabIndex = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 16);
            this.label1.TabIndex = 103;
            this.label1.Text = "Select the size 4 Angles connecting the Web and Flanges";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_OK);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox24);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 143);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(187, 107);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(109, 28);
            this.btn_OK.TabIndex = 105;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // frm_Connecting_Angles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 149);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Connecting_Angles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connecting Angles";
            this.Load += new System.EventHandler(this.frm_Connecting_Angles_Load);
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.ComboBox cmb_ana_ang_section_name;
        private System.Windows.Forms.ComboBox cmb_ana_nos_ang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.ComboBox cmb_ana_ang_section_code;
        private System.Windows.Forms.ComboBox cmb_ana_ang_thk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_OK;
    }
}