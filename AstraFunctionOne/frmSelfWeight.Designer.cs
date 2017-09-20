namespace AstraFunctionOne
{
    partial class frmSelfWeight
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
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtThermalLoad = new System.Windows.Forms.TextBox();
            this.txtSelfWeightZ = new System.Windows.Forms.TextBox();
            this.cmbLoadFactorSet = new System.Windows.Forms.ComboBox();
            this.txtSelfWeightY = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtSelfWeightX = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 151);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(218, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "Thermal Load Fraction for Current Load case";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 100);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(219, 13);
            this.label26.TabIndex = 28;
            this.label26.Text = "Element Self Load Factor in Z - direction (+/-)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 74);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(219, 13);
            this.label27.TabIndex = 27;
            this.label27.Text = "Element Self Load Factor in Y - direction (+/-)";
            // 
            // txtThermalLoad
            // 
            this.txtThermalLoad.Enabled = false;
            this.txtThermalLoad.Location = new System.Drawing.Point(260, 148);
            this.txtThermalLoad.Name = "txtThermalLoad";
            this.txtThermalLoad.Size = new System.Drawing.Size(100, 20);
            this.txtThermalLoad.TabIndex = 26;
            this.txtThermalLoad.Text = "0.0";
            this.txtThermalLoad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelfWeight_KeyDown);
            // 
            // txtSelfWeightZ
            // 
            this.txtSelfWeightZ.Location = new System.Drawing.Point(260, 97);
            this.txtSelfWeightZ.Name = "txtSelfWeightZ";
            this.txtSelfWeightZ.Size = new System.Drawing.Size(100, 20);
            this.txtSelfWeightZ.TabIndex = 25;
            this.txtSelfWeightZ.Text = "0.0";
            this.txtSelfWeightZ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelfWeight_KeyDown);
            // 
            // cmbLoadFactorSet
            // 
            this.cmbLoadFactorSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadFactorSet.FormattingEnabled = true;
            this.cmbLoadFactorSet.Items.AddRange(new object[] {
            "Load Case A",
            "Load Case B",
            "Load Case C",
            "Load Case D"});
            this.cmbLoadFactorSet.Location = new System.Drawing.Point(101, 16);
            this.cmbLoadFactorSet.Name = "cmbLoadFactorSet";
            this.cmbLoadFactorSet.Size = new System.Drawing.Size(121, 21);
            this.cmbLoadFactorSet.TabIndex = 21;
            this.cmbLoadFactorSet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelfWeight_KeyDown);
            // 
            // txtSelfWeightY
            // 
            this.txtSelfWeightY.Location = new System.Drawing.Point(260, 71);
            this.txtSelfWeightY.Name = "txtSelfWeightY";
            this.txtSelfWeightY.Size = new System.Drawing.Size(100, 20);
            this.txtSelfWeightY.TabIndex = 24;
            this.txtSelfWeightY.Text = "-1.0";
            this.txtSelfWeightY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelfWeight_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 19);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(83, 13);
            this.label29.TabIndex = 20;
            this.label29.Text = "Load Factor Set";
            // 
            // txtSelfWeightX
            // 
            this.txtSelfWeightX.Location = new System.Drawing.Point(260, 44);
            this.txtSelfWeightX.Name = "txtSelfWeightX";
            this.txtSelfWeightX.Size = new System.Drawing.Size(100, 20);
            this.txtSelfWeightX.TabIndex = 23;
            this.txtSelfWeightX.Text = "0.0";
            this.txtSelfWeightX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelfWeight_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 47);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(219, 13);
            this.label28.TabIndex = 22;
            this.label28.Text = "Element Self Load Factor in X - direction (+/-)";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(77, 178);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(192, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSelfWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 212);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtThermalLoad);
            this.Controls.Add(this.txtSelfWeightZ);
            this.Controls.Add(this.cmbLoadFactorSet);
            this.Controls.Add(this.txtSelfWeightY);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtSelfWeightX);
            this.Controls.Add(this.label28);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelfWeight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Self Weight";
            this.Load += new System.EventHandler(this.frmSelfWeight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtThermalLoad;
        private System.Windows.Forms.TextBox txtSelfWeightZ;
        private System.Windows.Forms.ComboBox cmbLoadFactorSet;
        private System.Windows.Forms.TextBox txtSelfWeightY;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtSelfWeightX;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}