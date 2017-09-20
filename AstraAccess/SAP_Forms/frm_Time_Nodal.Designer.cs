namespace AstraAccess.SAP_Forms
{
    partial class frm_Time_Nodal
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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_joint_nos = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddData = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_Ry = new System.Windows.Forms.CheckBox();
            this.chk_Rz = new System.Windows.Forms.CheckBox();
            this.chk_Tz = new System.Windows.Forms.CheckBox();
            this.chk_Rx = new System.Windows.Forms.CheckBox();
            this.chk_Ty = new System.Windows.Forms.CheckBox();
            this.chk_Tx = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
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
            // txt_joint_nos
            // 
            this.txt_joint_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_joint_nos.Location = new System.Drawing.Point(128, 7);
            this.txt_joint_nos.Name = "txt_joint_nos";
            this.txt_joint_nos.Size = new System.Drawing.Size(255, 22);
            this.txt_joint_nos.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_joint_nos);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 188);
            this.panel1.TabIndex = 3;
            // 
            // btnAddData
            // 
            this.btnAddData.Location = new System.Drawing.Point(74, 206);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(135, 28);
            this.btnAddData.TabIndex = 4;
            this.btnAddData.Text = "ADD Data";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.chk_Ry);
            this.groupBox5.Controls.Add(this.chk_Rz);
            this.groupBox5.Controls.Add(this.chk_Tz);
            this.groupBox5.Controls.Add(this.chk_Rx);
            this.groupBox5.Controls.Add(this.chk_Ty);
            this.groupBox5.Controls.Add(this.chk_Tx);
            this.groupBox5.Location = new System.Drawing.Point(6, 67);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(397, 111);
            this.groupBox5.TabIndex = 53;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Degree of Freedom";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Restrained D-O-F (\'Tick\' to Restrain)";
            // 
            // chk_Ry
            // 
            this.chk_Ry.AutoSize = true;
            this.chk_Ry.Location = new System.Drawing.Point(139, 76);
            this.chk_Ry.Name = "chk_Ry";
            this.chk_Ry.Size = new System.Drawing.Size(110, 17);
            this.chk_Ry.TabIndex = 4;
            this.chk_Ry.Text = "Y-rotation (RY)";
            this.chk_Ry.UseVisualStyleBackColor = true;
            // 
            // chk_Rz
            // 
            this.chk_Rz.AutoSize = true;
            this.chk_Rz.Location = new System.Drawing.Point(270, 76);
            this.chk_Rz.Name = "chk_Rz";
            this.chk_Rz.Size = new System.Drawing.Size(113, 17);
            this.chk_Rz.TabIndex = 5;
            this.chk_Rz.Text = "Z-rotation (RZ)";
            this.chk_Rz.UseVisualStyleBackColor = true;
            // 
            // chk_Tz
            // 
            this.chk_Tz.AutoSize = true;
            this.chk_Tz.Location = new System.Drawing.Point(270, 44);
            this.chk_Tz.Name = "chk_Tz";
            this.chk_Tz.Size = new System.Drawing.Size(128, 17);
            this.chk_Tz.TabIndex = 2;
            this.chk_Tz.Text = "Z-translation (TZ)";
            this.chk_Tz.UseVisualStyleBackColor = true;
            // 
            // chk_Rx
            // 
            this.chk_Rx.AutoSize = true;
            this.chk_Rx.Location = new System.Drawing.Point(9, 76);
            this.chk_Rx.Name = "chk_Rx";
            this.chk_Rx.Size = new System.Drawing.Size(113, 17);
            this.chk_Rx.TabIndex = 3;
            this.chk_Rx.Text = "X-rotation (RX)";
            this.chk_Rx.UseVisualStyleBackColor = true;
            // 
            // chk_Ty
            // 
            this.chk_Ty.AutoSize = true;
            this.chk_Ty.Location = new System.Drawing.Point(139, 44);
            this.chk_Ty.Name = "chk_Ty";
            this.chk_Ty.Size = new System.Drawing.Size(125, 17);
            this.chk_Ty.TabIndex = 1;
            this.chk_Ty.Text = "Y-translation (TY)";
            this.chk_Ty.UseVisualStyleBackColor = true;
            // 
            // chk_Tx
            // 
            this.chk_Tx.AutoSize = true;
            this.chk_Tx.Location = new System.Drawing.Point(9, 44);
            this.chk_Tx.Name = "chk_Tx";
            this.chk_Tx.Size = new System.Drawing.Size(128, 17);
            this.chk_Tx.TabIndex = 0;
            this.chk_Tx.Text = "X-translation (TX)";
            this.chk_Tx.UseVisualStyleBackColor = true;
            // 
            // frm_Time_Nodal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 245);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddData);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Time_Nodal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NODAL CONSTRAINT DOF";
            this.Load += new System.EventHandler(this.frm_Time_Nodal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_joint_nos;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_Ry;
        private System.Windows.Forms.CheckBox chk_Rz;
        private System.Windows.Forms.CheckBox chk_Tz;
        private System.Windows.Forms.CheckBox chk_Rx;
        private System.Windows.Forms.CheckBox chk_Ty;
        private System.Windows.Forms.CheckBox chk_Tx;
    }
}