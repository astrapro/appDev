namespace AstraAccess.SAP_Forms
{
    partial class frm_Plate_Elements
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
            this.label4 = new System.Windows.Forms.Label();
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.grb_Member = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_data_gen_no = new System.Windows.Forms.TextBox();
            this.txt_temp_grad = new System.Windows.Forms.TextBox();
            this.txt_temp_variation = new System.Windows.Forms.TextBox();
            this.txt_pressure = new System.Windows.Forms.TextBox();
            this.txt_thickness = new System.Windows.Forms.TextBox();
            this.txt_mat_prop_no = new System.Windows.Forms.TextBox();
            this.txt_node_L = new System.Windows.Forms.TextBox();
            this.txt_node_K = new System.Windows.Forms.TextBox();
            this.txt_node_O = new System.Windows.Forms.TextBox();
            this.txt_node_J = new System.Windows.Forms.TextBox();
            this.txt_no = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_node_I = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grb_Member.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Material Property No";
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(88, 218);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(145, 34);
            this.btn_add_mem.TabIndex = 10;
            this.btn_add_mem.Text = "ADD";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_add_mem_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label5);
            this.grb_Member.Controls.Add(this.label10);
            this.grb_Member.Controls.Add(this.label9);
            this.grb_Member.Controls.Add(this.label8);
            this.grb_Member.Controls.Add(this.label7);
            this.grb_Member.Controls.Add(this.label3);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label6);
            this.grb_Member.Controls.Add(this.label2);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_data_gen_no);
            this.grb_Member.Controls.Add(this.txt_temp_grad);
            this.grb_Member.Controls.Add(this.txt_temp_variation);
            this.grb_Member.Controls.Add(this.txt_pressure);
            this.grb_Member.Controls.Add(this.txt_thickness);
            this.grb_Member.Controls.Add(this.txt_mat_prop_no);
            this.grb_Member.Controls.Add(this.txt_node_L);
            this.grb_Member.Controls.Add(this.txt_node_K);
            this.grb_Member.Controls.Add(this.txt_node_O);
            this.grb_Member.Controls.Add(this.txt_node_J);
            this.grb_Member.Controls.Add(this.txt_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_node_I);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(12, 12);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(456, 200);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "PLATE ELEMENT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(58, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(190, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Mean Temperature Gradient";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(190, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Mean Temperature Variation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Element Pressure";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Element Thickness";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Element Data Generator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "NODE L";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "NODE K";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "NODE O";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 80);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(49, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "NODE J";
            // 
            // txt_data_gen_no
            // 
            this.txt_data_gen_no.Location = new System.Drawing.Point(368, 47);
            this.txt_data_gen_no.Name = "txt_data_gen_no";
            this.txt_data_gen_no.Size = new System.Drawing.Size(70, 21);
            this.txt_data_gen_no.TabIndex = 7;
            this.txt_data_gen_no.Text = "0.0";
            this.txt_data_gen_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_temp_grad
            // 
            this.txt_temp_grad.Location = new System.Drawing.Point(368, 164);
            this.txt_temp_grad.Name = "txt_temp_grad";
            this.txt_temp_grad.Size = new System.Drawing.Size(70, 21);
            this.txt_temp_grad.TabIndex = 11;
            this.txt_temp_grad.Text = "0.0";
            this.txt_temp_grad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_temp_variation
            // 
            this.txt_temp_variation.Location = new System.Drawing.Point(368, 131);
            this.txt_temp_variation.Name = "txt_temp_variation";
            this.txt_temp_variation.Size = new System.Drawing.Size(70, 21);
            this.txt_temp_variation.TabIndex = 10;
            this.txt_temp_variation.Text = "0.0";
            this.txt_temp_variation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_pressure
            // 
            this.txt_pressure.Location = new System.Drawing.Point(368, 101);
            this.txt_pressure.Name = "txt_pressure";
            this.txt_pressure.Size = new System.Drawing.Size(70, 21);
            this.txt_pressure.TabIndex = 9;
            this.txt_pressure.Text = "1.0";
            this.txt_pressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_thickness
            // 
            this.txt_thickness.Location = new System.Drawing.Point(368, 74);
            this.txt_thickness.Name = "txt_thickness";
            this.txt_thickness.Size = new System.Drawing.Size(70, 21);
            this.txt_thickness.TabIndex = 8;
            this.txt_thickness.Text = "1.0";
            this.txt_thickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mat_prop_no
            // 
            this.txt_mat_prop_no.Location = new System.Drawing.Point(368, 20);
            this.txt_mat_prop_no.Name = "txt_mat_prop_no";
            this.txt_mat_prop_no.Size = new System.Drawing.Size(70, 21);
            this.txt_mat_prop_no.TabIndex = 6;
            this.txt_mat_prop_no.Text = "1";
            this.txt_mat_prop_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_L
            // 
            this.txt_node_L.Location = new System.Drawing.Point(78, 131);
            this.txt_node_L.Name = "txt_node_L";
            this.txt_node_L.Size = new System.Drawing.Size(70, 21);
            this.txt_node_L.TabIndex = 4;
            this.txt_node_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_K
            // 
            this.txt_node_K.Location = new System.Drawing.Point(78, 101);
            this.txt_node_K.Name = "txt_node_K";
            this.txt_node_K.Size = new System.Drawing.Size(70, 21);
            this.txt_node_K.TabIndex = 3;
            this.txt_node_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_O
            // 
            this.txt_node_O.Location = new System.Drawing.Point(78, 164);
            this.txt_node_O.Name = "txt_node_O";
            this.txt_node_O.Size = new System.Drawing.Size(70, 21);
            this.txt_node_O.TabIndex = 5;
            this.txt_node_O.Text = "0";
            this.txt_node_O.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_J
            // 
            this.txt_node_J.Location = new System.Drawing.Point(78, 74);
            this.txt_node_J.Name = "txt_node_J";
            this.txt_node_J.Size = new System.Drawing.Size(70, 21);
            this.txt_node_J.TabIndex = 2;
            this.txt_node_J.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_no
            // 
            this.txt_no.Location = new System.Drawing.Point(78, 20);
            this.txt_no.Name = "txt_no";
            this.txt_no.Size = new System.Drawing.Size(70, 21);
            this.txt_no.TabIndex = 0;
            this.txt_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(54, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Plate No";
            // 
            // txt_node_I
            // 
            this.txt_node_I.Location = new System.Drawing.Point(78, 47);
            this.txt_node_I.Name = "txt_node_I";
            this.txt_node_I.Size = new System.Drawing.Size(70, 21);
            this.txt_node_I.TabIndex = 1;
            this.txt_node_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 50);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "NODE  I";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(245, 218);
            this.btn_close.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(145, 34);
            this.btn_close.TabIndex = 11;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(43, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 42);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NOTE :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(6, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "* Optional (Default value 0)";
            // 
            // frm_Plate_Elements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 306);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_add_mem);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_close);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Plate_Elements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plate Elements";
            this.Load += new System.EventHandler(this.frm_Plate_Elements_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_add_mem;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_data_gen_no;
        private System.Windows.Forms.TextBox txt_mat_prop_no;
        private System.Windows.Forms.TextBox txt_node_L;
        private System.Windows.Forms.TextBox txt_node_K;
        private System.Windows.Forms.TextBox txt_node_O;
        private System.Windows.Forms.TextBox txt_node_J;
        private System.Windows.Forms.TextBox txt_no;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_node_I;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_temp_grad;
        private System.Windows.Forms.TextBox txt_temp_variation;
        private System.Windows.Forms.TextBox txt_pressure;
        private System.Windows.Forms.TextBox txt_thickness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
    }
}