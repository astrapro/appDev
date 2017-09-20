namespace AstraFunctionOne.AnalysisType
{
    partial class frmResponseSpectrum
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grbDisplacement = new System.Windows.Forms.GroupBox();
            this.txtDisplacement = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDispPeriods = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grbAcceleration = new System.Windows.Forms.GroupBox();
            this.txtAcceleration = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAccPeriods = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtScaleFactor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpectrumPoints = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtnDisplacement = new System.Windows.Forms.RadioButton();
            this.rbtnAcceleration = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCutOffFrequencies = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalFrequencies = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTipAstra = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.grbDisplacement.SuspendLayout();
            this.grbAcceleration.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.grbDisplacement);
            this.groupBox1.Controls.Add(this.grbAcceleration);
            this.groupBox1.Controls.Add(this.txtScaleFactor);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSpectrumPoints);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtCutOffFrequencies);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTotalFrequencies);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 431);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(254, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(151, 400);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grbDisplacement
            // 
            this.grbDisplacement.Controls.Add(this.txtDisplacement);
            this.grbDisplacement.Controls.Add(this.label10);
            this.grbDisplacement.Controls.Add(this.txtDispPeriods);
            this.grbDisplacement.Controls.Add(this.label11);
            this.grbDisplacement.Enabled = false;
            this.grbDisplacement.Location = new System.Drawing.Point(24, 304);
            this.grbDisplacement.Name = "grbDisplacement";
            this.grbDisplacement.Size = new System.Drawing.Size(452, 90);
            this.grbDisplacement.TabIndex = 20;
            this.grbDisplacement.TabStop = false;
            this.grbDisplacement.Text = "Period  and Displacement";
            // 
            // txtDisplacement
            // 
            this.txtDisplacement.Location = new System.Drawing.Point(191, 45);
            this.txtDisplacement.Name = "txtDisplacement";
            this.txtDisplacement.Size = new System.Drawing.Size(255, 20);
            this.txtDisplacement.TabIndex = 1;
            this.txtDisplacement.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            this.txtDisplacement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(180, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Displacement (Separated by comma)";
            // 
            // txtDispPeriods
            // 
            this.txtDispPeriods.Location = new System.Drawing.Point(191, 19);
            this.txtDispPeriods.Name = "txtDispPeriods";
            this.txtDispPeriods.Size = new System.Drawing.Size(255, 20);
            this.txtDispPeriods.TabIndex = 0;
            this.txtDispPeriods.Text = "0.00,0.02,0.10,0.18,0.20,0.22,0.26,0.30,0.56,0.70,0.90,1.20,1.40,1.80,2.00,3.00";
            this.txtDispPeriods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            this.txtDispPeriods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Periods (Separated by comma)";
            // 
            // grbAcceleration
            // 
            this.grbAcceleration.Controls.Add(this.txtAcceleration);
            this.grbAcceleration.Controls.Add(this.label7);
            this.grbAcceleration.Controls.Add(this.txtAccPeriods);
            this.grbAcceleration.Controls.Add(this.label6);
            this.grbAcceleration.Location = new System.Drawing.Point(24, 208);
            this.grbAcceleration.Name = "grbAcceleration";
            this.grbAcceleration.Size = new System.Drawing.Size(452, 90);
            this.grbAcceleration.TabIndex = 13;
            this.grbAcceleration.TabStop = false;
            this.grbAcceleration.Text = "Period and Acceleration ";
            // 
            // txtAcceleration
            // 
            this.txtAcceleration.Location = new System.Drawing.Point(191, 45);
            this.txtAcceleration.Name = "txtAcceleration";
            this.txtAcceleration.Size = new System.Drawing.Size(255, 20);
            this.txtAcceleration.TabIndex = 1;
            this.txtAcceleration.Text = "46.328,46.328,149.023,207.706,211.566,212.725,210.408,203.845,134.352,110.416,86." +
                "094,62.929,52.892,39.765,35.518,21.620";
            this.txtAcceleration.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            this.txtAcceleration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Acceleration (Separated by comma)";
            // 
            // txtAccPeriods
            // 
            this.txtAccPeriods.Location = new System.Drawing.Point(191, 19);
            this.txtAccPeriods.Name = "txtAccPeriods";
            this.txtAccPeriods.Size = new System.Drawing.Size(255, 20);
            this.txtAccPeriods.TabIndex = 0;
            this.txtAccPeriods.Text = "0.00,0.02,0.10,0.18,0.20,0.22,0.26,0.30,0.56,0.70,0.90,1.20,1.40,1.80,2.00,3.00";
            this.txtAccPeriods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            this.txtAccPeriods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Periods (Separated by comma)";
            // 
            // txtScaleFactor
            // 
            this.txtScaleFactor.Location = new System.Drawing.Point(228, 125);
            this.txtScaleFactor.Name = "txtScaleFactor";
            this.txtScaleFactor.Size = new System.Drawing.Size(59, 20);
            this.txtScaleFactor.TabIndex = 3;
            this.txtScaleFactor.Text = "1.0";
            this.txtScaleFactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(161, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Scale Factor";
            // 
            // txtSpectrumPoints
            // 
            this.txtSpectrumPoints.Location = new System.Drawing.Point(96, 125);
            this.txtSpectrumPoints.Name = "txtSpectrumPoints";
            this.txtSpectrumPoints.Size = new System.Drawing.Size(59, 20);
            this.txtSpectrumPoints.TabIndex = 2;
            this.txtSpectrumPoints.Text = "16";
            this.txtSpectrumPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Spectrum Points";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtnDisplacement);
            this.groupBox3.Controls.Add(this.rbtnAcceleration);
            this.groupBox3.Location = new System.Drawing.Point(24, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 51);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Spectrum Type";
            // 
            // rbtnDisplacement
            // 
            this.rbtnDisplacement.AutoSize = true;
            this.rbtnDisplacement.Location = new System.Drawing.Point(137, 19);
            this.rbtnDisplacement.Name = "rbtnDisplacement";
            this.rbtnDisplacement.Size = new System.Drawing.Size(89, 17);
            this.rbtnDisplacement.TabIndex = 10;
            this.rbtnDisplacement.Text = "Displacement";
            this.rbtnDisplacement.UseVisualStyleBackColor = true;
            this.rbtnDisplacement.CheckedChanged += new System.EventHandler(this.rbtnAcceleration_CheckedChanged);
            this.rbtnDisplacement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtnAcceleration_KeyDown);
            // 
            // rbtnAcceleration
            // 
            this.rbtnAcceleration.AutoSize = true;
            this.rbtnAcceleration.Checked = true;
            this.rbtnAcceleration.Location = new System.Drawing.Point(24, 19);
            this.rbtnAcceleration.Name = "rbtnAcceleration";
            this.rbtnAcceleration.Size = new System.Drawing.Size(84, 17);
            this.rbtnAcceleration.TabIndex = 0;
            this.rbtnAcceleration.TabStop = true;
            this.rbtnAcceleration.Text = "Acceleration";
            this.rbtnAcceleration.UseVisualStyleBackColor = true;
            this.rbtnAcceleration.CheckedChanged += new System.EventHandler(this.rbtnAcceleration_CheckedChanged);
            this.rbtnAcceleration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtnAcceleration_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtZ);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Direction Factors";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(208, 24);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(49, 20);
            this.txtZ.TabIndex = 2;
            this.txtZ.Text = "0.0";
            this.txtZ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Z = ";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(115, 24);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(49, 20);
            this.txtY.TabIndex = 1;
            this.txtY.Text = "0.6667";
            this.txtY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y = ";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(30, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(49, 20);
            this.txtX.TabIndex = 0;
            this.txtX.Text = "1.0";
            this.txtX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "X = ";
            // 
            // txtCutOffFrequencies
            // 
            this.txtCutOffFrequencies.Location = new System.Drawing.Point(119, 35);
            this.txtCutOffFrequencies.Name = "txtCutOffFrequencies";
            this.txtCutOffFrequencies.Size = new System.Drawing.Size(59, 20);
            this.txtCutOffFrequencies.TabIndex = 1;
            this.txtCutOffFrequencies.Text = "10.5";
            this.txtCutOffFrequencies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "CUTOFF Frequencies";
            // 
            // txtTotalFrequencies
            // 
            this.txtTotalFrequencies.Location = new System.Drawing.Point(119, 9);
            this.txtTotalFrequencies.Name = "txtTotalFrequencies";
            this.txtTotalFrequencies.Size = new System.Drawing.Size(59, 20);
            this.txtTotalFrequencies.TabIndex = 0;
            this.txtTotalFrequencies.Text = "5";
            this.txtTotalFrequencies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalFrequencies_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Frequencies";
            // 
            // frmResponseSpectrum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 439);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResponseSpectrum";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Response Spectrum Analysis";
            this.Load += new System.EventHandler(this.frmResponseSpectrum_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbDisplacement.ResumeLayout(false);
            this.grbDisplacement.PerformLayout();
            this.grbAcceleration.ResumeLayout(false);
            this.grbAcceleration.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTotalFrequencies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCutOffFrequencies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbAcceleration;
        private System.Windows.Forms.TextBox txtAccPeriods;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtScaleFactor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpectrumPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtnDisplacement;
        private System.Windows.Forms.RadioButton rbtnAcceleration;
        private System.Windows.Forms.GroupBox grbDisplacement;
        private System.Windows.Forms.TextBox txtDisplacement;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDispPeriods;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAcceleration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolTip toolTipAstra;
    }
}