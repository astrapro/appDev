namespace AstraAccess.SAP_Forms
{
    partial class frm_Joint_Loads
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
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_jload_add = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_mz = new System.Windows.Forms.TextBox();
            this.txt_fz = new System.Windows.Forms.TextBox();
            this.txt_load_case = new System.Windows.Forms.TextBox();
            this.txt_my = new System.Windows.Forms.TextBox();
            this.txt_mx = new System.Windows.Forms.TextBox();
            this.txt_fy = new System.Windows.Forms.TextBox();
            this.txt_fx = new System.Windows.Forms.TextBox();
            this.txt_joint_number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(205, 194);
            this.btn_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(105, 32);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_jload_add
            // 
            this.btn_jload_add.Location = new System.Drawing.Point(65, 194);
            this.btn_jload_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_jload_add.Name = "btn_jload_add";
            this.btn_jload_add.Size = new System.Drawing.Size(105, 32);
            this.btn_jload_add.TabIndex = 1;
            this.btn_jload_add.Text = "ADD";
            this.btn_jload_add.UseVisualStyleBackColor = true;
            this.btn_jload_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.txt_mz);
            this.groupBox8.Controls.Add(this.txt_fz);
            this.groupBox8.Controls.Add(this.txt_load_case);
            this.groupBox8.Controls.Add(this.txt_my);
            this.groupBox8.Controls.Add(this.txt_mx);
            this.groupBox8.Controls.Add(this.txt_fy);
            this.groupBox8.Controls.Add(this.txt_fx);
            this.groupBox8.Controls.Add(this.txt_joint_number);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(13, 12);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(356, 176);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Joint Load";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(17, 57);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 32);
            this.label6.TabIndex = 51;
            this.label6.Text = "Seperated by comma (\',\') or space (\' \')\r\nEx: 1,2,3,4    or  1 2 3 4  or  1 TO 4";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 152);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "MZ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 109);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "FZ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Load Case";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "MY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "FY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "MX";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 109);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "FX";
            // 
            // txt_mz
            // 
            this.txt_mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mz.Location = new System.Drawing.Point(280, 148);
            this.txt_mz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mz.Name = "txt_mz";
            this.txt_mz.Size = new System.Drawing.Size(65, 21);
            this.txt_mz.TabIndex = 7;
            this.txt_mz.Text = "0.0";
            this.txt_mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fz
            // 
            this.txt_fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fz.Location = new System.Drawing.Point(280, 105);
            this.txt_fz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fz.Name = "txt_fz";
            this.txt_fz.Size = new System.Drawing.Size(65, 21);
            this.txt_fz.TabIndex = 4;
            this.txt_fz.Text = "0.0";
            this.txt_fz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_load_case
            // 
            this.txt_load_case.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_load_case.Location = new System.Drawing.Point(280, 33);
            this.txt_load_case.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_load_case.Name = "txt_load_case";
            this.txt_load_case.Size = new System.Drawing.Size(65, 21);
            this.txt_load_case.TabIndex = 1;
            this.txt_load_case.Text = "1";
            this.txt_load_case.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_my
            // 
            this.txt_my.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_my.Location = new System.Drawing.Point(163, 148);
            this.txt_my.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_my.Name = "txt_my";
            this.txt_my.Size = new System.Drawing.Size(65, 21);
            this.txt_my.TabIndex = 6;
            this.txt_my.Text = "0.0";
            this.txt_my.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mx
            // 
            this.txt_mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mx.Location = new System.Drawing.Point(44, 148);
            this.txt_mx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mx.Name = "txt_mx";
            this.txt_mx.Size = new System.Drawing.Size(65, 21);
            this.txt_mx.TabIndex = 5;
            this.txt_mx.Text = "0.0";
            this.txt_mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fy
            // 
            this.txt_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fy.Location = new System.Drawing.Point(163, 105);
            this.txt_fy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fy.Name = "txt_fy";
            this.txt_fy.Size = new System.Drawing.Size(65, 21);
            this.txt_fy.TabIndex = 3;
            this.txt_fy.Text = "0.0";
            this.txt_fy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fx
            // 
            this.txt_fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fx.Location = new System.Drawing.Point(44, 105);
            this.txt_fx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fx.Name = "txt_fx";
            this.txt_fx.Size = new System.Drawing.Size(65, 21);
            this.txt_fx.TabIndex = 2;
            this.txt_fx.Text = "0.0";
            this.txt_fx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_joint_number
            // 
            this.txt_joint_number.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_number.Location = new System.Drawing.Point(11, 33);
            this.txt_joint_number.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_joint_number.Name = "txt_joint_number";
            this.txt_joint_number.Size = new System.Drawing.Size(263, 21);
            this.txt_joint_number.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Joint Numbers";
            // 
            // frm_Joint_Loads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 251);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_jload_add);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Joint_Loads";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joint Loads";
            this.Load += new System.EventHandler(this.frm_Joint_Loads_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_jload_add;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_mz;
        private System.Windows.Forms.TextBox txt_fz;
        private System.Windows.Forms.TextBox txt_my;
        private System.Windows.Forms.TextBox txt_mx;
        private System.Windows.Forms.TextBox txt_fy;
        private System.Windows.Forms.TextBox txt_fx;
        private System.Windows.Forms.TextBox txt_joint_number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_load_case;
        private System.Windows.Forms.Label label6;
    }
}