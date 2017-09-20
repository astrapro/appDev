namespace AstraAccess.SAP_Forms
{
    partial class frm_Member_Release
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
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_J_Fx = new System.Windows.Forms.CheckBox();
            this.chk_J_Mz = new System.Windows.Forms.CheckBox();
            this.chk_J_Fy = new System.Windows.Forms.CheckBox();
            this.chk_J_My = new System.Windows.Forms.CheckBox();
            this.chk_J_Fz = new System.Windows.Forms.CheckBox();
            this.chk_J_Mx = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_I_Fx = new System.Windows.Forms.CheckBox();
            this.chk_I_Mz = new System.Windows.Forms.CheckBox();
            this.chk_I_Fy = new System.Windows.Forms.CheckBox();
            this.chk_I_My = new System.Windows.Forms.CheckBox();
            this.chk_I_Fz = new System.Windows.Forms.CheckBox();
            this.chk_I_Mx = new System.Windows.Forms.CheckBox();
            this.txt_mem_nos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txt_mem_nos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(29, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 32);
            this.label6.TabIndex = 50;
            this.label6.Text = "Seperated by comma (\',\') or space (\' \')\r\nEx: 1,2,3,4    or  1 2 3 4  or  1 TO 4";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_J_Fx);
            this.groupBox3.Controls.Add(this.chk_J_Mz);
            this.groupBox3.Controls.Add(this.chk_J_Fy);
            this.groupBox3.Controls.Add(this.chk_J_My);
            this.groupBox3.Controls.Add(this.chk_J_Fz);
            this.groupBox3.Controls.Add(this.chk_J_Mx);
            this.groupBox3.Location = new System.Drawing.Point(9, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Release for Node J";
            // 
            // chk_J_Fx
            // 
            this.chk_J_Fx.AutoSize = true;
            this.chk_J_Fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fx.Location = new System.Drawing.Point(6, 20);
            this.chk_J_Fx.Name = "chk_J_Fx";
            this.chk_J_Fx.Size = new System.Drawing.Size(39, 17);
            this.chk_J_Fx.TabIndex = 0;
            this.chk_J_Fx.Text = "Fx";
            this.chk_J_Fx.UseVisualStyleBackColor = true;
            // 
            // chk_J_Mz
            // 
            this.chk_J_Mz.AutoSize = true;
            this.chk_J_Mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Mz.Location = new System.Drawing.Point(225, 20);
            this.chk_J_Mz.Name = "chk_J_Mz";
            this.chk_J_Mz.Size = new System.Drawing.Size(41, 17);
            this.chk_J_Mz.TabIndex = 5;
            this.chk_J_Mz.Text = "Mz";
            this.chk_J_Mz.UseVisualStyleBackColor = true;
            // 
            // chk_J_Fy
            // 
            this.chk_J_Fy.AutoSize = true;
            this.chk_J_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fy.Location = new System.Drawing.Point(48, 20);
            this.chk_J_Fy.Name = "chk_J_Fy";
            this.chk_J_Fy.Size = new System.Drawing.Size(39, 17);
            this.chk_J_Fy.TabIndex = 1;
            this.chk_J_Fy.Text = "Fy";
            this.chk_J_Fy.UseVisualStyleBackColor = true;
            // 
            // chk_J_My
            // 
            this.chk_J_My.AutoSize = true;
            this.chk_J_My.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_My.Location = new System.Drawing.Point(179, 20);
            this.chk_J_My.Name = "chk_J_My";
            this.chk_J_My.Size = new System.Drawing.Size(42, 17);
            this.chk_J_My.TabIndex = 4;
            this.chk_J_My.Text = "My";
            this.chk_J_My.UseVisualStyleBackColor = true;
            // 
            // chk_J_Fz
            // 
            this.chk_J_Fz.AutoSize = true;
            this.chk_J_Fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fz.Location = new System.Drawing.Point(92, 20);
            this.chk_J_Fz.Name = "chk_J_Fz";
            this.chk_J_Fz.Size = new System.Drawing.Size(38, 17);
            this.chk_J_Fz.TabIndex = 2;
            this.chk_J_Fz.Text = "Fz";
            this.chk_J_Fz.UseVisualStyleBackColor = true;
            // 
            // chk_J_Mx
            // 
            this.chk_J_Mx.AutoSize = true;
            this.chk_J_Mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Mx.Location = new System.Drawing.Point(134, 20);
            this.chk_J_Mx.Name = "chk_J_Mx";
            this.chk_J_Mx.Size = new System.Drawing.Size(42, 17);
            this.chk_J_Mx.TabIndex = 3;
            this.chk_J_Mx.Text = "Mx";
            this.chk_J_Mx.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_I_Fx);
            this.groupBox2.Controls.Add(this.chk_I_Mz);
            this.groupBox2.Controls.Add(this.chk_I_Fy);
            this.groupBox2.Controls.Add(this.chk_I_My);
            this.groupBox2.Controls.Add(this.chk_I_Fz);
            this.groupBox2.Controls.Add(this.chk_I_Mx);
            this.groupBox2.Location = new System.Drawing.Point(9, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 41);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Release for Node I";
            // 
            // chk_I_Fx
            // 
            this.chk_I_Fx.AutoSize = true;
            this.chk_I_Fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fx.Location = new System.Drawing.Point(6, 20);
            this.chk_I_Fx.Name = "chk_I_Fx";
            this.chk_I_Fx.Size = new System.Drawing.Size(39, 17);
            this.chk_I_Fx.TabIndex = 0;
            this.chk_I_Fx.Text = "Fx";
            this.chk_I_Fx.UseVisualStyleBackColor = true;
            // 
            // chk_I_Mz
            // 
            this.chk_I_Mz.AutoSize = true;
            this.chk_I_Mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Mz.Location = new System.Drawing.Point(225, 20);
            this.chk_I_Mz.Name = "chk_I_Mz";
            this.chk_I_Mz.Size = new System.Drawing.Size(41, 17);
            this.chk_I_Mz.TabIndex = 5;
            this.chk_I_Mz.Text = "Mz";
            this.chk_I_Mz.UseVisualStyleBackColor = true;
            // 
            // chk_I_Fy
            // 
            this.chk_I_Fy.AutoSize = true;
            this.chk_I_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fy.Location = new System.Drawing.Point(48, 20);
            this.chk_I_Fy.Name = "chk_I_Fy";
            this.chk_I_Fy.Size = new System.Drawing.Size(39, 17);
            this.chk_I_Fy.TabIndex = 1;
            this.chk_I_Fy.Text = "Fy";
            this.chk_I_Fy.UseVisualStyleBackColor = true;
            // 
            // chk_I_My
            // 
            this.chk_I_My.AutoSize = true;
            this.chk_I_My.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_My.Location = new System.Drawing.Point(179, 20);
            this.chk_I_My.Name = "chk_I_My";
            this.chk_I_My.Size = new System.Drawing.Size(42, 17);
            this.chk_I_My.TabIndex = 4;
            this.chk_I_My.Text = "My";
            this.chk_I_My.UseVisualStyleBackColor = true;
            // 
            // chk_I_Fz
            // 
            this.chk_I_Fz.AutoSize = true;
            this.chk_I_Fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fz.Location = new System.Drawing.Point(92, 20);
            this.chk_I_Fz.Name = "chk_I_Fz";
            this.chk_I_Fz.Size = new System.Drawing.Size(38, 17);
            this.chk_I_Fz.TabIndex = 2;
            this.chk_I_Fz.Text = "Fz";
            this.chk_I_Fz.UseVisualStyleBackColor = true;
            // 
            // chk_I_Mx
            // 
            this.chk_I_Mx.AutoSize = true;
            this.chk_I_Mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Mx.Location = new System.Drawing.Point(134, 20);
            this.chk_I_Mx.Name = "chk_I_Mx";
            this.chk_I_Mx.Size = new System.Drawing.Size(42, 17);
            this.chk_I_Mx.TabIndex = 3;
            this.chk_I_Mx.Text = "Mx";
            this.chk_I_Mx.UseVisualStyleBackColor = true;
            // 
            // txt_mem_nos
            // 
            this.txt_mem_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mem_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txt_mem_nos.Location = new System.Drawing.Point(9, 29);
            this.txt_mem_nos.Name = "txt_mem_nos";
            this.txt_mem_nos.Size = new System.Drawing.Size(283, 22);
            this.txt_mem_nos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Element Numbers";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(190, 184);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(124, 29);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(26, 184);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(124, 29);
            this.btn_add_data.TabIndex = 1;
            this.btn_add_data.Text = "ADD Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // frm_Member_Release
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 217);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_data);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Member_Release";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Release";
            this.Load += new System.EventHandler(this.frm_Member_Release_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_I_Mz;
        private System.Windows.Forms.CheckBox chk_I_My;
        private System.Windows.Forms.CheckBox chk_I_Mx;
        private System.Windows.Forms.CheckBox chk_I_Fz;
        private System.Windows.Forms.CheckBox chk_I_Fy;
        private System.Windows.Forms.CheckBox chk_I_Fx;
        private System.Windows.Forms.TextBox txt_mem_nos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_J_Fx;
        private System.Windows.Forms.CheckBox chk_J_Mz;
        private System.Windows.Forms.CheckBox chk_J_Fy;
        private System.Windows.Forms.CheckBox chk_J_My;
        private System.Windows.Forms.CheckBox chk_J_Fz;
        private System.Windows.Forms.CheckBox chk_J_Mx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
    }
}