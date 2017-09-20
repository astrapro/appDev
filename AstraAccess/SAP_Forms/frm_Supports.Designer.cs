namespace AstraAccess.SAP_Forms
{
    partial class frm_Supports
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
            this.btnAddData = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_displacement = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_kVal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtn_kFZ = new System.Windows.Forms.RadioButton();
            this.rbtn_kFY = new System.Windows.Forms.RadioButton();
            this.rbtn_kFX = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_mz = new System.Windows.Forms.CheckBox();
            this.chk_my = new System.Windows.Forms.CheckBox();
            this.chk_mx = new System.Windows.Forms.CheckBox();
            this.chk_fz = new System.Windows.Forms.CheckBox();
            this.chk_fy = new System.Windows.Forms.CheckBox();
            this.chk_fx = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_support_type = new System.Windows.Forms.ComboBox();
            this.txt_joint_nos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddData
            // 
            this.btnAddData.Location = new System.Drawing.Point(77, 251);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(135, 28);
            this.btnAddData.TabIndex = 1;
            this.btnAddData.Text = "ADD Data";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.chk_mz);
            this.panel1.Controls.Add(this.chk_my);
            this.panel1.Controls.Add(this.chk_mx);
            this.panel1.Controls.Add(this.chk_fz);
            this.panel1.Controls.Add(this.chk_fy);
            this.panel1.Controls.Add(this.chk_fx);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmb_support_type);
            this.panel1.Controls.Add(this.txt_joint_nos);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 233);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_displacement);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_kVal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rbtn_kFZ);
            this.groupBox1.Controls.Add(this.rbtn_kFY);
            this.groupBox1.Controls.Add(this.rbtn_kFX);
            this.groupBox1.Location = new System.Drawing.Point(6, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 80);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Springs at Support";
            // 
            // txt_displacement
            // 
            this.txt_displacement.Location = new System.Drawing.Point(288, 53);
            this.txt_displacement.Name = "txt_displacement";
            this.txt_displacement.Size = new System.Drawing.Size(89, 21);
            this.txt_displacement.TabIndex = 4;
            this.txt_displacement.Text = "1";
            this.txt_displacement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "Displacement along element axis";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_kVal
            // 
            this.txt_kVal.Location = new System.Drawing.Point(287, 23);
            this.txt_kVal.Name = "txt_kVal";
            this.txt_kVal.Size = new System.Drawing.Size(90, 21);
            this.txt_kVal.TabIndex = 3;
            this.txt_kVal.Text = "0";
            this.txt_kVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 14);
            this.label3.TabIndex = 24;
            this.label3.Text = "Value";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbtn_kFZ
            // 
            this.rbtn_kFZ.AutoSize = true;
            this.rbtn_kFZ.Location = new System.Drawing.Point(138, 24);
            this.rbtn_kFZ.Name = "rbtn_kFZ";
            this.rbtn_kFZ.Size = new System.Drawing.Size(46, 17);
            this.rbtn_kFZ.TabIndex = 2;
            this.rbtn_kFZ.TabStop = true;
            this.rbtn_kFZ.Text = "kFZ";
            this.rbtn_kFZ.UseVisualStyleBackColor = true;
            // 
            // rbtn_kFY
            // 
            this.rbtn_kFY.AutoSize = true;
            this.rbtn_kFY.Location = new System.Drawing.Point(77, 24);
            this.rbtn_kFY.Name = "rbtn_kFY";
            this.rbtn_kFY.Size = new System.Drawing.Size(45, 17);
            this.rbtn_kFY.TabIndex = 1;
            this.rbtn_kFY.TabStop = true;
            this.rbtn_kFY.Text = "kFY";
            this.rbtn_kFY.UseVisualStyleBackColor = true;
            // 
            // rbtn_kFX
            // 
            this.rbtn_kFX.AutoSize = true;
            this.rbtn_kFX.Checked = true;
            this.rbtn_kFX.Location = new System.Drawing.Point(16, 24);
            this.rbtn_kFX.Name = "rbtn_kFX";
            this.rbtn_kFX.Size = new System.Drawing.Size(46, 17);
            this.rbtn_kFX.TabIndex = 0;
            this.rbtn_kFX.TabStop = true;
            this.rbtn_kFX.Text = "kFX";
            this.rbtn_kFX.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "Release";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chk_mz
            // 
            this.chk_mz.AutoSize = true;
            this.chk_mz.Location = new System.Drawing.Point(346, 109);
            this.chk_mz.Name = "chk_mz";
            this.chk_mz.Size = new System.Drawing.Size(43, 17);
            this.chk_mz.TabIndex = 7;
            this.chk_mz.Text = "MZ";
            this.chk_mz.UseVisualStyleBackColor = true;
            // 
            // chk_my
            // 
            this.chk_my.AutoSize = true;
            this.chk_my.Location = new System.Drawing.Point(298, 109);
            this.chk_my.Name = "chk_my";
            this.chk_my.Size = new System.Drawing.Size(42, 17);
            this.chk_my.TabIndex = 6;
            this.chk_my.Text = "MY";
            this.chk_my.UseVisualStyleBackColor = true;
            // 
            // chk_mx
            // 
            this.chk_mx.AutoSize = true;
            this.chk_mx.Location = new System.Drawing.Point(253, 109);
            this.chk_mx.Name = "chk_mx";
            this.chk_mx.Size = new System.Drawing.Size(43, 17);
            this.chk_mx.TabIndex = 5;
            this.chk_mx.Text = "MX";
            this.chk_mx.UseVisualStyleBackColor = true;
            // 
            // chk_fz
            // 
            this.chk_fz.AutoSize = true;
            this.chk_fz.Location = new System.Drawing.Point(211, 109);
            this.chk_fz.Name = "chk_fz";
            this.chk_fz.Size = new System.Drawing.Size(40, 17);
            this.chk_fz.TabIndex = 4;
            this.chk_fz.Text = "FZ";
            this.chk_fz.UseVisualStyleBackColor = true;
            // 
            // chk_fy
            // 
            this.chk_fy.AutoSize = true;
            this.chk_fy.Location = new System.Drawing.Point(169, 109);
            this.chk_fy.Name = "chk_fy";
            this.chk_fy.Size = new System.Drawing.Size(39, 17);
            this.chk_fy.TabIndex = 3;
            this.chk_fy.Text = "FY";
            this.chk_fy.UseVisualStyleBackColor = true;
            // 
            // chk_fx
            // 
            this.chk_fx.AutoSize = true;
            this.chk_fx.Location = new System.Drawing.Point(128, 109);
            this.chk_fx.Name = "chk_fx";
            this.chk_fx.Size = new System.Drawing.Size(40, 17);
            this.chk_fx.TabIndex = 2;
            this.chk_fx.Text = "FX";
            this.chk_fx.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 14);
            this.label1.TabIndex = 22;
            this.label1.Text = "Support Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmb_support_type
            // 
            this.cmb_support_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_support_type.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_support_type.FormattingEnabled = true;
            this.cmb_support_type.Items.AddRange(new object[] {
            "FIXED",
            "PINNED"});
            this.cmb_support_type.Location = new System.Drawing.Point(128, 82);
            this.cmb_support_type.Name = "cmb_support_type";
            this.cmb_support_type.Size = new System.Drawing.Size(110, 21);
            this.cmb_support_type.TabIndex = 1;
            this.cmb_support_type.SelectedIndexChanged += new System.EventHandler(this.cmb_support_type_SelectedIndexChanged);
            // 
            // txt_joint_nos
            // 
            this.txt_joint_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_joint_nos.Location = new System.Drawing.Point(128, 7);
            this.txt_joint_nos.Name = "txt_joint_nos";
            this.txt_joint_nos.Size = new System.Drawing.Size(255, 22);
            this.txt_joint_nos.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 14);
            this.label8.TabIndex = 6;
            this.label8.Text = "Node Numbers";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(128, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 32);
            this.label6.TabIndex = 52;
            this.label6.Text = "Seperated by comma (\',\') or space (\' \')\r\nEx: 1,2,3,4    or  1 2 3 4  or  1 TO 4";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_Supports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 291);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Supports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supports";
            this.Load += new System.EventHandler(this.frm_Supports_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_kVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtn_kFZ;
        private System.Windows.Forms.RadioButton rbtn_kFY;
        private System.Windows.Forms.RadioButton rbtn_kFX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_mz;
        private System.Windows.Forms.CheckBox chk_my;
        private System.Windows.Forms.CheckBox chk_mx;
        private System.Windows.Forms.CheckBox chk_fz;
        private System.Windows.Forms.CheckBox chk_fy;
        private System.Windows.Forms.CheckBox chk_fx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_support_type;
        private System.Windows.Forms.TextBox txt_joint_nos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_displacement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}