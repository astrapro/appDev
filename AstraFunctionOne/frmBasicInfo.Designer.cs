namespace AstraFunctionOne
{
    partial class frmBasicInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectTitle = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbStructureType = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAst = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnDataCheakOnly = new System.Windows.Forms.RadioButton();
            this.rbtnProblemSolution = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.cmbMassUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Title";
            // 
            // txtProjectTitle
            // 
            this.txtProjectTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjectTitle.Location = new System.Drawing.Point(240, 58);
            this.txtProjectTitle.Name = "txtProjectTitle";
            this.txtProjectTitle.Size = new System.Drawing.Size(262, 20);
            this.txtProjectTitle.TabIndex = 1;
            this.txtProjectTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProjectTitile_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(140, 128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(221, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lbStructureType
            // 
            this.lbStructureType.FormattingEnabled = true;
            this.lbStructureType.Items.AddRange(new object[] {
            "SPACE",
            "FLOOR",
            "PLANE"});
            this.lbStructureType.Location = new System.Drawing.Point(88, 9);
            this.lbStructureType.Name = "lbStructureType";
            this.lbStructureType.Size = new System.Drawing.Size(87, 43);
            this.lbStructureType.TabIndex = 4;
            this.lbStructureType.SelectedIndexChanged += new System.EventHandler(this.lbStructureType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "StructureType";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtAst
            // 
            this.txtAst.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAst.Location = new System.Drawing.Point(88, 58);
            this.txtAst.Name = "txtAst";
            this.txtAst.ReadOnly = true;
            this.txtAst.Size = new System.Drawing.Size(87, 20);
            this.txtAst.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "User\'s Title";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnDataCheakOnly);
            this.groupBox1.Controls.Add(this.rbtnProblemSolution);
            this.groupBox1.Location = new System.Drawing.Point(15, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 68);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Running Option";
            // 
            // rbtnDataCheakOnly
            // 
            this.rbtnDataCheakOnly.AutoSize = true;
            this.rbtnDataCheakOnly.Location = new System.Drawing.Point(7, 40);
            this.rbtnDataCheakOnly.Name = "rbtnDataCheakOnly";
            this.rbtnDataCheakOnly.Size = new System.Drawing.Size(106, 17);
            this.rbtnDataCheakOnly.TabIndex = 1;
            this.rbtnDataCheakOnly.Text = "Data Cheak Only";
            this.rbtnDataCheakOnly.UseVisualStyleBackColor = true;
            // 
            // rbtnProblemSolution
            // 
            this.rbtnProblemSolution.AutoSize = true;
            this.rbtnProblemSolution.Checked = true;
            this.rbtnProblemSolution.Location = new System.Drawing.Point(6, 19);
            this.rbtnProblemSolution.Name = "rbtnProblemSolution";
            this.rbtnProblemSolution.Size = new System.Drawing.Size(104, 17);
            this.rbtnProblemSolution.TabIndex = 0;
            this.rbtnProblemSolution.TabStop = true;
            this.rbtnProblemSolution.Text = "Problem Solution";
            this.rbtnProblemSolution.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbLengthUnit);
            this.groupBox2.Controls.Add(this.label77);
            this.groupBox2.Controls.Add(this.label78);
            this.groupBox2.Controls.Add(this.cmbMassUnit);
            this.groupBox2.Location = new System.Drawing.Point(306, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 68);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Define Unit";
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "METRES",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(77, 39);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbLengthUnit.TabIndex = 135;
            this.cmbLengthUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLengthUnit_KeyDown);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 42);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(62, 13);
            this.label77.TabIndex = 137;
            this.label77.Text = "Length Unit";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(6, 16);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(54, 13);
            this.label78.TabIndex = 136;
            this.label78.Text = "Mass Unit";
            // 
            // cmbMassUnit
            // 
            this.cmbMassUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMassUnit.FormattingEnabled = true;
            this.cmbMassUnit.Items.AddRange(new object[] {
            "KG",
            "KN",
            "MTON",
            "NEW",
            "GMS",
            "LBS",
            "KIP"});
            this.cmbMassUnit.Location = new System.Drawing.Point(77, 13);
            this.cmbMassUnit.Name = "cmbMassUnit";
            this.cmbMassUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbMassUnit.TabIndex = 134;
            this.cmbMassUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMassUnit_KeyDown);
            // 
            // frmBasicInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 157);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbStructureType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtProjectTitle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBasicInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Basic Information...";
            this.Load += new System.EventHandler(this.frmTitle_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectTitle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbStructureType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnDataCheakOnly;
        private System.Windows.Forms.RadioButton rbtnProblemSolution;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cmbMassUnit;
    }
}