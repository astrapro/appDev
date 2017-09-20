namespace AstraAccess.SAP_Forms
{
    partial class frm_Boundary_Elements
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_stiff = new System.Windows.Forms.TextBox();
            this.txt_rot = new System.Windows.Forms.TextBox();
            this.txt_disp = new System.Windows.Forms.TextBox();
            this.txt_data_gen = new System.Windows.Forms.TextBox();
            this.txt_rot_code = new System.Windows.Forms.TextBox();
            this.txt_disp_code = new System.Windows.Forms.TextBox();
            this.txt_node_L = new System.Windows.Forms.TextBox();
            this.txt_node_K = new System.Windows.Forms.TextBox();
            this.txt_node_J = new System.Windows.Forms.TextBox();
            this.txt_node_N = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_node_I = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.grb_Member.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(240, 207);
            this.btn_close.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(127, 34);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label2);
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label9);
            this.grb_Member.Controls.Add(this.label8);
            this.grb_Member.Controls.Add(this.label7);
            this.grb_Member.Controls.Add(this.label5);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.label6);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_stiff);
            this.grb_Member.Controls.Add(this.txt_rot);
            this.grb_Member.Controls.Add(this.txt_disp);
            this.grb_Member.Controls.Add(this.txt_data_gen);
            this.grb_Member.Controls.Add(this.txt_rot_code);
            this.grb_Member.Controls.Add(this.txt_disp_code);
            this.grb_Member.Controls.Add(this.txt_node_L);
            this.grb_Member.Controls.Add(this.txt_node_K);
            this.grb_Member.Controls.Add(this.txt_node_J);
            this.grb_Member.Controls.Add(this.txt_node_N);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_node_I);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(12, 12);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(438, 189);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "BOUNDARY ELEMENT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Spring Stiffness";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Specified rotation \r\nabout element axis";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(195, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 26);
            this.label9.TabIndex = 1;
            this.label9.Text = "Specified Displacement \r\nalong element axis";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Data Generator Kn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Code for rotation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Code for displacement";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "NODE L";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "NODE K";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(13, 80);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(49, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "NODE J";
            // 
            // txt_stiff
            // 
            this.txt_stiff.Location = new System.Drawing.Point(348, 159);
            this.txt_stiff.Name = "txt_stiff";
            this.txt_stiff.Size = new System.Drawing.Size(70, 21);
            this.txt_stiff.TabIndex = 10;
            this.txt_stiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_rot
            // 
            this.txt_rot.Location = new System.Drawing.Point(348, 127);
            this.txt_rot.Name = "txt_rot";
            this.txt_rot.Size = new System.Drawing.Size(70, 21);
            this.txt_rot.TabIndex = 9;
            this.txt_rot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_disp
            // 
            this.txt_disp.Location = new System.Drawing.Point(348, 95);
            this.txt_disp.Name = "txt_disp";
            this.txt_disp.Size = new System.Drawing.Size(70, 21);
            this.txt_disp.TabIndex = 8;
            this.txt_disp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_data_gen
            // 
            this.txt_data_gen.Location = new System.Drawing.Point(348, 65);
            this.txt_data_gen.Name = "txt_data_gen";
            this.txt_data_gen.Size = new System.Drawing.Size(70, 21);
            this.txt_data_gen.TabIndex = 7;
            this.txt_data_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_rot_code
            // 
            this.txt_rot_code.Location = new System.Drawing.Point(348, 38);
            this.txt_rot_code.Name = "txt_rot_code";
            this.txt_rot_code.Size = new System.Drawing.Size(70, 21);
            this.txt_rot_code.TabIndex = 6;
            this.txt_rot_code.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_disp_code
            // 
            this.txt_disp_code.Location = new System.Drawing.Point(348, 14);
            this.txt_disp_code.Name = "txt_disp_code";
            this.txt_disp_code.Size = new System.Drawing.Size(70, 21);
            this.txt_disp_code.TabIndex = 5;
            this.txt_disp_code.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_L
            // 
            this.txt_node_L.Location = new System.Drawing.Point(87, 136);
            this.txt_node_L.Name = "txt_node_L";
            this.txt_node_L.Size = new System.Drawing.Size(70, 21);
            this.txt_node_L.TabIndex = 4;
            this.txt_node_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_K
            // 
            this.txt_node_K.Location = new System.Drawing.Point(87, 104);
            this.txt_node_K.Name = "txt_node_K";
            this.txt_node_K.Size = new System.Drawing.Size(70, 21);
            this.txt_node_K.TabIndex = 3;
            this.txt_node_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_J
            // 
            this.txt_node_J.Location = new System.Drawing.Point(87, 74);
            this.txt_node_J.Name = "txt_node_J";
            this.txt_node_J.Size = new System.Drawing.Size(70, 21);
            this.txt_node_J.TabIndex = 2;
            this.txt_node_J.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_N
            // 
            this.txt_node_N.Location = new System.Drawing.Point(87, 23);
            this.txt_node_N.Name = "txt_node_N";
            this.txt_node_N.Size = new System.Drawing.Size(70, 21);
            this.txt_node_N.TabIndex = 0;
            this.txt_node_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(52, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "NODE N";
            // 
            // txt_node_I
            // 
            this.txt_node_I.Location = new System.Drawing.Point(87, 47);
            this.txt_node_I.Name = "txt_node_I";
            this.txt_node_I.Size = new System.Drawing.Size(70, 21);
            this.txt_node_I.TabIndex = 1;
            this.txt_node_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(13, 50);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "NODE  I";
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(100, 207);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(127, 34);
            this.btn_add_mem.TabIndex = 1;
            this.btn_add_mem.Text = "ADD ";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_add_mem_Click);
            // 
            // frm_Boundary_Elements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 251);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add_mem);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Boundary_Elements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boundary Elements";
            this.Load += new System.EventHandler(this.frm_Boundary_Elements_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_rot_code;
        private System.Windows.Forms.TextBox txt_disp_code;
        private System.Windows.Forms.TextBox txt_node_L;
        private System.Windows.Forms.TextBox txt_node_K;
        private System.Windows.Forms.TextBox txt_node_J;
        private System.Windows.Forms.TextBox txt_node_N;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_node_I;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_add_mem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_stiff;
        private System.Windows.Forms.TextBox txt_rot;
        private System.Windows.Forms.TextBox txt_disp;
        private System.Windows.Forms.TextBox txt_data_gen;
    }
}