namespace AstraFunctionOne
{
    partial class frmAnalysisType
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
            this.rbtnDataCheck = new System.Windows.Forms.RadioButton();
            this.grbSelectType = new System.Windows.Forms.GroupBox();
            this.rbtnResponse = new System.Windows.Forms.RadioButton();
            this.rbtnTimeHistoryAnalysis = new System.Windows.Forms.RadioButton();
            this.rbtnEigenvalue = new System.Windows.Forms.RadioButton();
            this.rbtnDynamicAnalysis = new System.Windows.Forms.RadioButton();
            this.rbtnStaticAnalysis = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grbSelectType.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnDataCheck);
            this.groupBox1.Controls.Add(this.grbSelectType);
            this.groupBox1.Controls.Add(this.rbtnDynamicAnalysis);
            this.groupBox1.Controls.Add(this.rbtnStaticAnalysis);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 188);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Type";
            // 
            // rbtnDataCheck
            // 
            this.rbtnDataCheck.AutoSize = true;
            this.rbtnDataCheck.Location = new System.Drawing.Point(21, 164);
            this.rbtnDataCheck.Name = "rbtnDataCheck";
            this.rbtnDataCheck.Size = new System.Drawing.Size(82, 17);
            this.rbtnDataCheck.TabIndex = 3;
            this.rbtnDataCheck.TabStop = true;
            this.rbtnDataCheck.Text = "Data Check";
            this.rbtnDataCheck.UseVisualStyleBackColor = true;
            this.rbtnDataCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // grbSelectType
            // 
            this.grbSelectType.Controls.Add(this.rbtnResponse);
            this.grbSelectType.Controls.Add(this.rbtnTimeHistoryAnalysis);
            this.grbSelectType.Controls.Add(this.rbtnEigenvalue);
            this.grbSelectType.Enabled = false;
            this.grbSelectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbSelectType.Location = new System.Drawing.Point(21, 65);
            this.grbSelectType.Name = "grbSelectType";
            this.grbSelectType.Size = new System.Drawing.Size(290, 93);
            this.grbSelectType.TabIndex = 2;
            this.grbSelectType.TabStop = false;
            this.grbSelectType.Text = "Select for Dynamic Analysis";
            // 
            // rbtnResponse
            // 
            this.rbtnResponse.AutoSize = true;
            this.rbtnResponse.Location = new System.Drawing.Point(23, 65);
            this.rbtnResponse.Name = "rbtnResponse";
            this.rbtnResponse.Size = new System.Drawing.Size(258, 17);
            this.rbtnResponse.TabIndex = 2;
            this.rbtnResponse.TabStop = true;
            this.rbtnResponse.Text = "PERFORM RESPONSE SPECTRUM ANALYSIS";
            this.rbtnResponse.UseVisualStyleBackColor = true;
            this.rbtnResponse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // rbtnTimeHistoryAnalysis
            // 
            this.rbtnTimeHistoryAnalysis.AutoSize = true;
            this.rbtnTimeHistoryAnalysis.Location = new System.Drawing.Point(23, 42);
            this.rbtnTimeHistoryAnalysis.Name = "rbtnTimeHistoryAnalysis";
            this.rbtnTimeHistoryAnalysis.Size = new System.Drawing.Size(216, 17);
            this.rbtnTimeHistoryAnalysis.TabIndex = 1;
            this.rbtnTimeHistoryAnalysis.TabStop = true;
            this.rbtnTimeHistoryAnalysis.Text = "PERFORM TIME  HISTORY ANALYSIS";
            this.rbtnTimeHistoryAnalysis.UseVisualStyleBackColor = true;
            this.rbtnTimeHistoryAnalysis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // rbtnEigenvalue
            // 
            this.rbtnEigenvalue.AutoSize = true;
            this.rbtnEigenvalue.Location = new System.Drawing.Point(23, 19);
            this.rbtnEigenvalue.Name = "rbtnEigenvalue";
            this.rbtnEigenvalue.Size = new System.Drawing.Size(214, 17);
            this.rbtnEigenvalue.TabIndex = 0;
            this.rbtnEigenvalue.TabStop = true;
            this.rbtnEigenvalue.Text = "PERFORM EIGEN VALUES ANALYSIS";
            this.rbtnEigenvalue.UseVisualStyleBackColor = true;
            this.rbtnEigenvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // rbtnDynamicAnalysis
            // 
            this.rbtnDynamicAnalysis.AutoSize = true;
            this.rbtnDynamicAnalysis.Location = new System.Drawing.Point(21, 42);
            this.rbtnDynamicAnalysis.Name = "rbtnDynamicAnalysis";
            this.rbtnDynamicAnalysis.Size = new System.Drawing.Size(107, 17);
            this.rbtnDynamicAnalysis.TabIndex = 1;
            this.rbtnDynamicAnalysis.TabStop = true;
            this.rbtnDynamicAnalysis.Text = "Dynamic Analysis";
            this.rbtnDynamicAnalysis.UseVisualStyleBackColor = true;
            this.rbtnDynamicAnalysis.CheckedChanged += new System.EventHandler(this.rbtnDynamicAnalysis_CheckedChanged);
            this.rbtnDynamicAnalysis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // rbtnStaticAnalysis
            // 
            this.rbtnStaticAnalysis.AutoSize = true;
            this.rbtnStaticAnalysis.Location = new System.Drawing.Point(21, 19);
            this.rbtnStaticAnalysis.Name = "rbtnStaticAnalysis";
            this.rbtnStaticAnalysis.Size = new System.Drawing.Size(93, 17);
            this.rbtnStaticAnalysis.TabIndex = 0;
            this.rbtnStaticAnalysis.TabStop = true;
            this.rbtnStaticAnalysis.Text = "Static Analysis";
            this.rbtnStaticAnalysis.UseVisualStyleBackColor = true;
            this.rbtnStaticAnalysis.CheckedChanged += new System.EventHandler(this.rbtnStaticAnalysis_CheckedChanged);
            this.rbtnStaticAnalysis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dynamicAnalysis_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(69, 206);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 26);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(187, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAnalysisType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 244);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAnalysisType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analysis Type";
            this.Load += new System.EventHandler(this.frmAnalysisType_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbSelectType.ResumeLayout(false);
            this.grbSelectType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnDynamicAnalysis;
        private System.Windows.Forms.RadioButton rbtnStaticAnalysis;
        private System.Windows.Forms.GroupBox grbSelectType;
        private System.Windows.Forms.RadioButton rbtnResponse;
        private System.Windows.Forms.RadioButton rbtnTimeHistoryAnalysis;
        private System.Windows.Forms.RadioButton rbtnEigenvalue;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbtnDataCheck;
    }
}