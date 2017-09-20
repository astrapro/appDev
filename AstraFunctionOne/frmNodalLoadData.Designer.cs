namespace AstraFunctionOne
{
    partial class frmNodalLoadData
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
            this.cmbLengthUnits = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbMassUnit = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoadCase = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDeleteNodalData = new System.Windows.Forms.Button();
            this.btnPrevNodalData = new System.Windows.Forms.Button();
            this.btnNextNodalData = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMz = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMy = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFz = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNodeNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteLoadCase = new System.Windows.Forms.Button();
            this.btnPrevLoadCase = new System.Windows.Forms.Button();
            this.btnNextLoadCase = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLengthUnits
            // 
            this.cmbLengthUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnits.FormattingEnabled = true;
            this.cmbLengthUnits.Items.AddRange(new object[] {
            "M",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnits.Location = new System.Drawing.Point(472, 16);
            this.cmbLengthUnits.Name = "cmbLengthUnits";
            this.cmbLengthUnits.Size = new System.Drawing.Size(85, 21);
            this.cmbLengthUnits.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(399, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 13);
            this.label20.TabIndex = 30;
            this.label20.Text = "Length Units";
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
            this.cmbMassUnit.Location = new System.Drawing.Point(279, 16);
            this.cmbMassUnit.Name = "cmbMassUnit";
            this.cmbMassUnit.Size = new System.Drawing.Size(72, 21);
            this.cmbMassUnit.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(219, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 28;
            this.label21.Text = "Mass Unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Load Case";
            // 
            // txtLoadCase
            // 
            this.txtLoadCase.Location = new System.Drawing.Point(72, 15);
            this.txtLoadCase.Name = "txtLoadCase";
            this.txtLoadCase.Size = new System.Drawing.Size(100, 20);
            this.txtLoadCase.TabIndex = 0;
            this.txtLoadCase.TextChanged += new System.EventHandler(this.txtLoadCase_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnDeleteNodalData);
            this.groupBox1.Controls.Add(this.btnPrevNodalData);
            this.groupBox1.Controls.Add(this.btnNextNodalData);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtMz);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMy);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMx);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtFz);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNodeNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 310);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nodal Load Data in Global Co-ordinate System";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(327, 226);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 62;
            this.label16.Text = "[Mz]";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(327, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 61;
            this.label15.Text = "[My]";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(327, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "[Mx]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(327, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 13);
            this.label13.TabIndex = 59;
            this.label13.Text = "[Fz]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(327, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 58;
            this.label12.Text = "[Fy]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(327, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 57;
            this.label11.Text = "[Fx]";
            // 
            // btnDeleteNodalData
            // 
            this.btnDeleteNodalData.Location = new System.Drawing.Point(339, 271);
            this.btnDeleteNodalData.Name = "btnDeleteNodalData";
            this.btnDeleteNodalData.Size = new System.Drawing.Size(133, 23);
            this.btnDeleteNodalData.TabIndex = 9;
            this.btnDeleteNodalData.Text = "Delete Nodal Load";
            this.btnDeleteNodalData.UseVisualStyleBackColor = true;
            this.btnDeleteNodalData.Click += new System.EventHandler(this.btnDeleteNodalData_Click);
            // 
            // btnPrevNodalData
            // 
            this.btnPrevNodalData.Location = new System.Drawing.Point(200, 271);
            this.btnPrevNodalData.Name = "btnPrevNodalData";
            this.btnPrevNodalData.Size = new System.Drawing.Size(133, 23);
            this.btnPrevNodalData.TabIndex = 8;
            this.btnPrevNodalData.Text = "Prev. Nodal Load";
            this.btnPrevNodalData.UseVisualStyleBackColor = true;
            this.btnPrevNodalData.Click += new System.EventHandler(this.btnPrevNodalData_Click);
            // 
            // btnNextNodalData
            // 
            this.btnNextNodalData.Location = new System.Drawing.Point(61, 271);
            this.btnNextNodalData.Name = "btnNextNodalData";
            this.btnNextNodalData.Size = new System.Drawing.Size(133, 23);
            this.btnNextNodalData.TabIndex = 7;
            this.btnNextNodalData.Text = "Next Nodal Load";
            this.btnNextNodalData.UseVisualStyleBackColor = true;
            this.btnNextNodalData.Click += new System.EventHandler(this.btnNextNodalData_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "About Z (+/-)";
            // 
            // txtMz
            // 
            this.txtMz.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMz.Location = new System.Drawing.Point(239, 223);
            this.txtMz.Name = "txtMz";
            this.txtMz.Size = new System.Drawing.Size(82, 22);
            this.txtMz.TabIndex = 6;
            this.txtMz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "About Y (+/-)";
            // 
            // txtMy
            // 
            this.txtMy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMy.Location = new System.Drawing.Point(239, 197);
            this.txtMy.Name = "txtMy";
            this.txtMy.Size = new System.Drawing.Size(82, 22);
            this.txtMy.TabIndex = 5;
            this.txtMy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(145, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "About X (+/-)";
            // 
            // txtMx
            // 
            this.txtMx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMx.Location = new System.Drawing.Point(239, 171);
            this.txtMx.Name = "txtMx";
            this.txtMx.Size = new System.Drawing.Size(82, 22);
            this.txtMx.TabIndex = 4;
            this.txtMx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Moment";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Z - Direction (+/-)";
            // 
            // txtFz
            // 
            this.txtFz.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFz.Location = new System.Drawing.Point(239, 131);
            this.txtFz.Name = "txtFz";
            this.txtFz.Size = new System.Drawing.Size(82, 22);
            this.txtFz.TabIndex = 3;
            this.txtFz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Y - Direction (+/-)";
            // 
            // txtFy
            // 
            this.txtFy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFy.Location = new System.Drawing.Point(239, 105);
            this.txtFy.Name = "txtFy";
            this.txtFy.Size = new System.Drawing.Size(82, 22);
            this.txtFy.TabIndex = 2;
            this.txtFy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "X - Direction (+/-)";
            // 
            // txtFx
            // 
            this.txtFx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFx.Location = new System.Drawing.Point(239, 79);
            this.txtFx.Name = "txtFx";
            this.txtFx.Size = new System.Drawing.Size(82, 22);
            this.txtFx.TabIndex = 1;
            this.txtFx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Conc. Load";
            // 
            // txtNodeNo
            // 
            this.txtNodeNo.Location = new System.Drawing.Point(78, 34);
            this.txtNodeNo.Name = "txtNodeNo";
            this.txtNodeNo.Size = new System.Drawing.Size(100, 20);
            this.txtNodeNo.TabIndex = 0;
            this.txtNodeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNodeNo.TextChanged += new System.EventHandler(this.txtNodeNo_TextChanged);
            this.txtNodeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Node No.";
            // 
            // btnDeleteLoadCase
            // 
            this.btnDeleteLoadCase.Location = new System.Drawing.Point(334, 359);
            this.btnDeleteLoadCase.Name = "btnDeleteLoadCase";
            this.btnDeleteLoadCase.Size = new System.Drawing.Size(111, 23);
            this.btnDeleteLoadCase.TabIndex = 5;
            this.btnDeleteLoadCase.Text = "Delete Load Case";
            this.btnDeleteLoadCase.UseVisualStyleBackColor = true;
            this.btnDeleteLoadCase.Click += new System.EventHandler(this.btnDeleteLoadCase_Click);
            // 
            // btnPrevLoadCase
            // 
            this.btnPrevLoadCase.Location = new System.Drawing.Point(212, 359);
            this.btnPrevLoadCase.Name = "btnPrevLoadCase";
            this.btnPrevLoadCase.Size = new System.Drawing.Size(111, 23);
            this.btnPrevLoadCase.TabIndex = 4;
            this.btnPrevLoadCase.Text = "Prev. Load Case";
            this.btnPrevLoadCase.UseVisualStyleBackColor = true;
            this.btnPrevLoadCase.Click += new System.EventHandler(this.btnPrevLoadCase_Click);
            // 
            // btnNextLoadCase
            // 
            this.btnNextLoadCase.Location = new System.Drawing.Point(90, 359);
            this.btnNextLoadCase.Name = "btnNextLoadCase";
            this.btnNextLoadCase.Size = new System.Drawing.Size(111, 23);
            this.btnNextLoadCase.TabIndex = 3;
            this.btnNextLoadCase.Text = "Next Load Case";
            this.btnNextLoadCase.UseVisualStyleBackColor = true;
            this.btnNextLoadCase.Click += new System.EventHandler(this.btnNextLoadCase_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(165, 406);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblStatus.ForeColor = System.Drawing.Color.Beige;
            this.lblStatus.Location = new System.Drawing.Point(0, 432);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(617, 16);
            this.lblStatus.TabIndex = 58;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeleteLoadCase);
            this.groupBox2.Controls.Add(this.btnPrevLoadCase);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btnNextLoadCase);
            this.groupBox2.Controls.Add(this.txtLoadCase);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbLengthUnits);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.cmbMassUnit);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 395);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 60;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmNodalLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 448);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNodalLoadData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nodal Load Data";
            this.Load += new System.EventHandler(this.frmNodalLoadData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLengthUnits;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbMassUnit;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoadCase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteNodalData;
        private System.Windows.Forms.Button btnPrevNodalData;
        private System.Windows.Forms.Button btnNextNodalData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMz;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFz;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNodeNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteLoadCase;
        private System.Windows.Forms.Button btnPrevLoadCase;
        private System.Windows.Forms.Button btnNextLoadCase;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
    }
}