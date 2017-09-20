namespace AstraAccess.SAP_Forms
{
    partial class frm_Plate_Mat_Props
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
            this.grb_Member = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Axy = new System.Windows.Forms.TextBox();
            this.txt_Ay = new System.Windows.Forms.TextBox();
            this.txt_Ax = new System.Windows.Forms.TextBox();
            this.txt_mass_den = new System.Windows.Forms.TextBox();
            this.txt_mat_no = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Gxy = new System.Windows.Forms.TextBox();
            this.txt_Cys = new System.Windows.Forms.TextBox();
            this.txt_Cyy = new System.Windows.Forms.TextBox();
            this.txt_Cxs = new System.Windows.Forms.TextBox();
            this.txt_Cxy = new System.Windows.Forms.TextBox();
            this.txt_Cxx = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grb_Member.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(284, 212);
            this.btn_close.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(117, 34);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label3);
            this.grb_Member.Controls.Add(this.label2);
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.txt_Axy);
            this.grb_Member.Controls.Add(this.txt_Ay);
            this.grb_Member.Controls.Add(this.txt_Ax);
            this.grb_Member.Controls.Add(this.txt_mass_den);
            this.grb_Member.Controls.Add(this.txt_mat_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Location = new System.Drawing.Point(12, 22);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(325, 184);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "Material Properties";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Thermal Expansion Coefficient (α xy)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thermal Expansion Coefficient (αy)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thermal Expansion Coefficient  (αx)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Mass Density";
            // 
            // txt_Axy
            // 
            this.txt_Axy.Location = new System.Drawing.Point(226, 128);
            this.txt_Axy.Name = "txt_Axy";
            this.txt_Axy.Size = new System.Drawing.Size(89, 21);
            this.txt_Axy.TabIndex = 4;
            this.txt_Axy.Text = "0.0";
            this.txt_Axy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ay
            // 
            this.txt_Ay.Location = new System.Drawing.Point(226, 101);
            this.txt_Ay.Name = "txt_Ay";
            this.txt_Ay.Size = new System.Drawing.Size(89, 21);
            this.txt_Ay.TabIndex = 3;
            this.txt_Ay.Text = "0.0";
            this.txt_Ay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Ax
            // 
            this.txt_Ax.Location = new System.Drawing.Point(226, 74);
            this.txt_Ax.Name = "txt_Ax";
            this.txt_Ax.Size = new System.Drawing.Size(89, 21);
            this.txt_Ax.TabIndex = 2;
            this.txt_Ax.Text = "0.0";
            this.txt_Ax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mass_den
            // 
            this.txt_mass_den.Location = new System.Drawing.Point(226, 47);
            this.txt_mass_den.Name = "txt_mass_den";
            this.txt_mass_den.Size = new System.Drawing.Size(89, 21);
            this.txt_mass_den.TabIndex = 1;
            this.txt_mass_den.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mat_no
            // 
            this.txt_mat_no.Location = new System.Drawing.Point(226, 20);
            this.txt_mat_no.Name = "txt_mat_no";
            this.txt_mat_no.Size = new System.Drawing.Size(89, 21);
            this.txt_mat_no.TabIndex = 0;
            this.txt_mat_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(0, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(149, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Material Identification No";
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(139, 212);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(117, 34);
            this.btn_add_mem.TabIndex = 2;
            this.btn_add_mem.Text = "ADD";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_add_mem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_Gxy);
            this.groupBox1.Controls.Add(this.txt_Cys);
            this.groupBox1.Controls.Add(this.txt_Cyy);
            this.groupBox1.Controls.Add(this.txt_Cxs);
            this.groupBox1.Controls.Add(this.txt_Cxy);
            this.groupBox1.Controls.Add(this.txt_Cxx);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(343, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 184);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elasticity Element";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Gxy";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cys";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Cyy";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Cxs";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cxy";
            // 
            // txt_Gxy
            // 
            this.txt_Gxy.Location = new System.Drawing.Point(55, 155);
            this.txt_Gxy.Name = "txt_Gxy";
            this.txt_Gxy.Size = new System.Drawing.Size(115, 21);
            this.txt_Gxy.TabIndex = 5;
            this.txt_Gxy.Text = "1239130.0";
            this.txt_Gxy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Cys
            // 
            this.txt_Cys.Location = new System.Drawing.Point(55, 128);
            this.txt_Cys.Name = "txt_Cys";
            this.txt_Cys.Size = new System.Drawing.Size(115, 21);
            this.txt_Cys.TabIndex = 4;
            this.txt_Cys.Text = "0.0";
            this.txt_Cys.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Cyy
            // 
            this.txt_Cyy.Location = new System.Drawing.Point(55, 101);
            this.txt_Cyy.Name = "txt_Cyy";
            this.txt_Cyy.Size = new System.Drawing.Size(115, 21);
            this.txt_Cyy.TabIndex = 3;
            this.txt_Cyy.Text = "3009340.0";
            this.txt_Cyy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Cxs
            // 
            this.txt_Cxs.Location = new System.Drawing.Point(55, 74);
            this.txt_Cxs.Name = "txt_Cxs";
            this.txt_Cxs.Size = new System.Drawing.Size(115, 21);
            this.txt_Cxs.TabIndex = 2;
            this.txt_Cxs.Text = "0.0";
            this.txt_Cxs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Cxy
            // 
            this.txt_Cxy.Location = new System.Drawing.Point(55, 47);
            this.txt_Cxy.Name = "txt_Cxy";
            this.txt_Cxy.Size = new System.Drawing.Size(115, 21);
            this.txt_Cxy.TabIndex = 1;
            this.txt_Cxy.Text = "531061.0";
            this.txt_Cxy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Cxx
            // 
            this.txt_Cxx.Location = new System.Drawing.Point(55, 20);
            this.txt_Cxx.Name = "txt_Cxx";
            this.txt_Cxx.Size = new System.Drawing.Size(115, 21);
            this.txt_Cxx.TabIndex = 0;
            this.txt_Cxx.Text = "3009340.0";
            this.txt_Cxx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Cxx";
            // 
            // frm_Plate_Mat_Props
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 258);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add_mem);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Plate_Mat_Props";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plate Material Propertiess";
            this.Load += new System.EventHandler(this.frm_Plate_Mat_Props_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_mass_den;
        private System.Windows.Forms.TextBox txt_mat_no;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btn_add_mem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Ax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Axy;
        private System.Windows.Forms.TextBox txt_Ay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_Gxy;
        private System.Windows.Forms.TextBox txt_Cys;
        private System.Windows.Forms.TextBox txt_Cyy;
        private System.Windows.Forms.TextBox txt_Cxs;
        private System.Windows.Forms.TextBox txt_Cxy;
        private System.Windows.Forms.TextBox txt_Cxx;
        private System.Windows.Forms.Label label9;

    }
}