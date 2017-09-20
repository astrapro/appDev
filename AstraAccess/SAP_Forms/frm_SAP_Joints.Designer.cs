namespace AstraAccess.SAP_Forms
{
    partial class frm_SAP_Joints
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
            this.rbtnZAxis = new System.Windows.Forms.RadioButton();
            this.rbtnXAxis = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkYr = new System.Windows.Forms.CheckBox();
            this.rbtnYAxis = new System.Windows.Forms.RadioButton();
            this.chkIncrOn = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNodeIncr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtZIncr = new System.Windows.Forms.TextBox();
            this.chkZt = new System.Windows.Forms.CheckBox();
            this.chkXr = new System.Windows.Forms.CheckBox();
            this.chkYt = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtYIncr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtXIncr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkXt = new System.Windows.Forms.CheckBox();
            this.rbtnGlobalCylindrical = new System.Windows.Forms.RadioButton();
            this.rbtnGlobalCartesian = new System.Windows.Forms.RadioButton();
            this.chkZr = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.grbCylindricalAxis = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodeNo = new System.Windows.Forms.TextBox();
            this.epAstra = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grbCylindricalAxis.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).BeginInit();
            this.SuspendLayout();
            // 
            // rbtnZAxis
            // 
            this.rbtnZAxis.AutoSize = true;
            this.rbtnZAxis.Location = new System.Drawing.Point(140, 27);
            this.rbtnZAxis.Name = "rbtnZAxis";
            this.rbtnZAxis.Size = new System.Drawing.Size(62, 17);
            this.rbtnZAxis.TabIndex = 6;
            this.rbtnZAxis.TabStop = true;
            this.rbtnZAxis.Text = "Z-Axis";
            this.rbtnZAxis.UseVisualStyleBackColor = true;
            // 
            // rbtnXAxis
            // 
            this.rbtnXAxis.AutoSize = true;
            this.rbtnXAxis.Location = new System.Drawing.Point(6, 27);
            this.rbtnXAxis.Name = "rbtnXAxis";
            this.rbtnXAxis.Size = new System.Drawing.Size(62, 17);
            this.rbtnXAxis.TabIndex = 4;
            this.rbtnXAxis.TabStop = true;
            this.rbtnXAxis.Text = "X-Axis";
            this.rbtnXAxis.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(215, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Restrained D-O-F (\'Tick\' to Restrain)";
            // 
            // chkYr
            // 
            this.chkYr.AutoSize = true;
            this.chkYr.Location = new System.Drawing.Point(184, 76);
            this.chkYr.Name = "chkYr";
            this.chkYr.Size = new System.Drawing.Size(110, 17);
            this.chkYr.TabIndex = 4;
            this.chkYr.Text = "Y-rotation (RY)";
            this.chkYr.UseVisualStyleBackColor = true;
            // 
            // rbtnYAxis
            // 
            this.rbtnYAxis.AutoSize = true;
            this.rbtnYAxis.Location = new System.Drawing.Point(74, 27);
            this.rbtnYAxis.Name = "rbtnYAxis";
            this.rbtnYAxis.Size = new System.Drawing.Size(60, 17);
            this.rbtnYAxis.TabIndex = 5;
            this.rbtnYAxis.TabStop = true;
            this.rbtnYAxis.Text = "Y-Axis";
            this.rbtnYAxis.UseVisualStyleBackColor = true;
            // 
            // chkIncrOn
            // 
            this.chkIncrOn.AutoSize = true;
            this.chkIncrOn.Checked = true;
            this.chkIncrOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrOn.Location = new System.Drawing.Point(339, 11);
            this.chkIncrOn.Name = "chkIncrOn";
            this.chkIncrOn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkIncrOn.Size = new System.Drawing.Size(113, 17);
            this.chkIncrOn.TabIndex = 5;
            this.chkIncrOn.Text = "Increament ON";
            this.chkIncrOn.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Node [incr]";
            // 
            // txtNodeIncr
            // 
            this.txtNodeIncr.Location = new System.Drawing.Point(83, 65);
            this.txtNodeIncr.Name = "txtNodeIncr";
            this.txtNodeIncr.Size = new System.Drawing.Size(52, 21);
            this.txtNodeIncr.TabIndex = 1;
            this.txtNodeIncr.Text = "1";
            this.txtNodeIncr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(336, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Z[incr]";
            // 
            // txtZIncr
            // 
            this.txtZIncr.Location = new System.Drawing.Point(401, 98);
            this.txtZIncr.Name = "txtZIncr";
            this.txtZIncr.Size = new System.Drawing.Size(52, 21);
            this.txtZIncr.TabIndex = 8;
            this.txtZIncr.Text = "0.0";
            // 
            // chkZt
            // 
            this.chkZt.AutoSize = true;
            this.chkZt.Location = new System.Drawing.Point(328, 44);
            this.chkZt.Name = "chkZt";
            this.chkZt.Size = new System.Drawing.Size(128, 17);
            this.chkZt.TabIndex = 2;
            this.chkZt.Text = "Z-translation (TZ)";
            this.chkZt.UseVisualStyleBackColor = true;
            // 
            // chkXr
            // 
            this.chkXr.AutoSize = true;
            this.chkXr.Location = new System.Drawing.Point(9, 76);
            this.chkXr.Name = "chkXr";
            this.chkXr.Size = new System.Drawing.Size(113, 17);
            this.chkXr.TabIndex = 3;
            this.chkXr.Text = "X-rotation (RX)";
            this.chkXr.UseVisualStyleBackColor = true;
            // 
            // chkYt
            // 
            this.chkYt.AutoSize = true;
            this.chkYt.Location = new System.Drawing.Point(184, 44);
            this.chkYt.Name = "chkYt";
            this.chkYt.Size = new System.Drawing.Size(125, 17);
            this.chkYt.TabIndex = 1;
            this.chkYt.Text = "Y-translation (TY)";
            this.chkYt.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(336, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Y[incr]";
            // 
            // txtYIncr
            // 
            this.txtYIncr.Location = new System.Drawing.Point(401, 65);
            this.txtYIncr.Name = "txtYIncr";
            this.txtYIncr.Size = new System.Drawing.Size(52, 21);
            this.txtYIncr.TabIndex = 7;
            this.txtYIncr.Text = "0.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(336, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "X[incr]";
            // 
            // txtXIncr
            // 
            this.txtXIncr.Location = new System.Drawing.Point(401, 29);
            this.txtXIncr.Name = "txtXIncr";
            this.txtXIncr.Size = new System.Drawing.Size(52, 21);
            this.txtXIncr.TabIndex = 6;
            this.txtXIncr.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Z=";
            // 
            // chkXt
            // 
            this.chkXt.AutoSize = true;
            this.chkXt.Location = new System.Drawing.Point(9, 44);
            this.chkXt.Name = "chkXt";
            this.chkXt.Size = new System.Drawing.Size(128, 17);
            this.chkXt.TabIndex = 0;
            this.chkXt.Text = "X-translation (TX)";
            this.chkXt.UseVisualStyleBackColor = true;
            // 
            // rbtnGlobalCylindrical
            // 
            this.rbtnGlobalCylindrical.AutoSize = true;
            this.rbtnGlobalCylindrical.Location = new System.Drawing.Point(132, 27);
            this.rbtnGlobalCylindrical.Name = "rbtnGlobalCylindrical";
            this.rbtnGlobalCylindrical.Size = new System.Drawing.Size(125, 17);
            this.rbtnGlobalCylindrical.TabIndex = 1;
            this.rbtnGlobalCylindrical.Text = "Global Cylindrical";
            this.rbtnGlobalCylindrical.UseVisualStyleBackColor = true;
            // 
            // rbtnGlobalCartesian
            // 
            this.rbtnGlobalCartesian.AutoSize = true;
            this.rbtnGlobalCartesian.Checked = true;
            this.rbtnGlobalCartesian.Location = new System.Drawing.Point(6, 27);
            this.rbtnGlobalCartesian.Name = "rbtnGlobalCartesian";
            this.rbtnGlobalCartesian.Size = new System.Drawing.Size(120, 17);
            this.rbtnGlobalCartesian.TabIndex = 0;
            this.rbtnGlobalCartesian.TabStop = true;
            this.rbtnGlobalCartesian.Text = "Global Cartesian";
            this.rbtnGlobalCartesian.UseVisualStyleBackColor = true;
            // 
            // chkZr
            // 
            this.chkZr.AutoSize = true;
            this.chkZr.Location = new System.Drawing.Point(328, 76);
            this.chkZr.Name = "chkZr";
            this.chkZr.Size = new System.Drawing.Size(113, 17);
            this.chkZr.TabIndex = 5;
            this.chkZr.Text = "Z-rotation (RZ)";
            this.chkZr.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_cancel);
            this.groupBox2.Controls.Add(this.btn_add);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.grbCylindricalAxis);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 309);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(244, 269);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(113, 34);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(120, 269);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(113, 34);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "ADD";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnGlobalCylindrical);
            this.groupBox4.Controls.Add(this.rbtnGlobalCartesian);
            this.groupBox4.Location = new System.Drawing.Point(6, 309);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 50);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Coordinate System";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.chkYr);
            this.groupBox5.Controls.Add(this.chkZr);
            this.groupBox5.Controls.Add(this.chkZt);
            this.groupBox5.Controls.Add(this.chkXr);
            this.groupBox5.Controls.Add(this.chkYt);
            this.groupBox5.Controls.Add(this.chkXt);
            this.groupBox5.Location = new System.Drawing.Point(6, 152);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(471, 111);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Degree of Freedom";
            // 
            // grbCylindricalAxis
            // 
            this.grbCylindricalAxis.Controls.Add(this.rbtnZAxis);
            this.grbCylindricalAxis.Controls.Add(this.rbtnYAxis);
            this.grbCylindricalAxis.Controls.Add(this.rbtnXAxis);
            this.grbCylindricalAxis.Enabled = false;
            this.grbCylindricalAxis.Location = new System.Drawing.Point(269, 309);
            this.grbCylindricalAxis.Name = "grbCylindricalAxis";
            this.grbCylindricalAxis.Size = new System.Drawing.Size(208, 50);
            this.grbCylindricalAxis.TabIndex = 1;
            this.grbCylindricalAxis.TabStop = false;
            this.grbCylindricalAxis.Text = "Cylindrical Axis";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIncrOn);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtNodeIncr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtZIncr);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtYIncr);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtXIncr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNodeNo);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Joint Coordinates";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(204, 99);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(74, 21);
            this.txtZ.TabIndex = 4;
            this.txtZ.Text = "0.0";
            this.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y=";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(204, 66);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(74, 21);
            this.txtY.TabIndex = 3;
            this.txtY.Text = "0.0";
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X =";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(204, 30);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(74, 21);
            this.txtX.TabIndex = 2;
            this.txtX.Text = "0.0";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Node No:";
            // 
            // txtNodeNo
            // 
            this.txtNodeNo.Location = new System.Drawing.Point(83, 30);
            this.txtNodeNo.Name = "txtNodeNo";
            this.txtNodeNo.Size = new System.Drawing.Size(52, 21);
            this.txtNodeNo.TabIndex = 0;
            this.txtNodeNo.Text = "1";
            this.txtNodeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // epAstra
            // 
            this.epAstra.ContainerControl = this;
            // 
            // frm_SAP_Joints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 309);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SAP_Joints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joints";
            this.Load += new System.EventHandler(this.frm_SAP_Joints_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grbCylindricalAxis.ResumeLayout(false);
            this.grbCylindricalAxis.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnZAxis;
        private System.Windows.Forms.RadioButton rbtnXAxis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkYr;
        private System.Windows.Forms.RadioButton rbtnYAxis;
        private System.Windows.Forms.CheckBox chkIncrOn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNodeIncr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtZIncr;
        private System.Windows.Forms.CheckBox chkZt;
        private System.Windows.Forms.CheckBox chkXr;
        private System.Windows.Forms.CheckBox chkYt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtYIncr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtXIncr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkXt;
        private System.Windows.Forms.RadioButton rbtnGlobalCylindrical;
        private System.Windows.Forms.RadioButton rbtnGlobalCartesian;
        private System.Windows.Forms.CheckBox chkZr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox grbCylindricalAxis;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNodeNo;
        private System.Windows.Forms.ErrorProvider epAstra;

    }
}