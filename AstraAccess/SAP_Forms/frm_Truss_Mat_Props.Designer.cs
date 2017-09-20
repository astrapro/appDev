namespace AstraAccess.SAP_Forms
{
    partial class frm_Truss_Mat_Props
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
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_weight_den = new System.Windows.Forms.TextBox();
            this.txt_area = new System.Windows.Forms.TextBox();
            this.txt_mass_den = new System.Windows.Forms.TextBox();
            this.txt_therm_coeff = new System.Windows.Forms.TextBox();
            this.txt_mat_no = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_mod = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.grb_Member.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(192, 205);
            this.btn_close.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(100, 34);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label5);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_weight_den);
            this.grb_Member.Controls.Add(this.txt_area);
            this.grb_Member.Controls.Add(this.txt_mass_den);
            this.grb_Member.Controls.Add(this.txt_therm_coeff);
            this.grb_Member.Controls.Add(this.txt_mat_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_mod);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(12, 12);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(337, 187);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "Material Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Weight Density";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cross Sectional Area";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Mass Density";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 80);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(196, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "Coefficient of Thermal Expansion";
            // 
            // txt_weight_den
            // 
            this.txt_weight_den.Location = new System.Drawing.Point(216, 153);
            this.txt_weight_den.Name = "txt_weight_den";
            this.txt_weight_den.Size = new System.Drawing.Size(99, 21);
            this.txt_weight_den.TabIndex = 5;
            this.txt_weight_den.Text = "7.8332";
            this.txt_weight_den.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_area
            // 
            this.txt_area.Location = new System.Drawing.Point(216, 128);
            this.txt_area.Name = "txt_area";
            this.txt_area.Size = new System.Drawing.Size(99, 21);
            this.txt_area.TabIndex = 4;
            this.txt_area.Text = "0.02361";
            this.txt_area.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mass_den
            // 
            this.txt_mass_den.Location = new System.Drawing.Point(216, 101);
            this.txt_mass_den.Name = "txt_mass_den";
            this.txt_mass_den.Size = new System.Drawing.Size(99, 21);
            this.txt_mass_den.TabIndex = 3;
            this.txt_mass_den.Text = "7.8332";
            this.txt_mass_den.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_therm_coeff
            // 
            this.txt_therm_coeff.Location = new System.Drawing.Point(216, 74);
            this.txt_therm_coeff.Name = "txt_therm_coeff";
            this.txt_therm_coeff.Size = new System.Drawing.Size(99, 21);
            this.txt_therm_coeff.TabIndex = 2;
            this.txt_therm_coeff.Text = "0.000";
            this.txt_therm_coeff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mat_no
            // 
            this.txt_mat_no.Location = new System.Drawing.Point(216, 20);
            this.txt_mat_no.Name = "txt_mat_no";
            this.txt_mat_no.Size = new System.Drawing.Size(64, 21);
            this.txt_mat_no.TabIndex = 0;
            this.txt_mat_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(149, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Material Identification No";
            // 
            // txt_mod
            // 
            this.txt_mod.Location = new System.Drawing.Point(216, 47);
            this.txt_mod.Name = "txt_mod";
            this.txt_mod.Size = new System.Drawing.Size(99, 21);
            this.txt_mod.TabIndex = 1;
            this.txt_mod.Text = "211000000";
            this.txt_mod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 50);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(122, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Modulus of Elasticity";
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(69, 205);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(100, 34);
            this.btn_add_mem.TabIndex = 1;
            this.btn_add_mem.Text = "ADD";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_add_mem_Click);
            // 
            // frm_Truss_Mat_Props
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 246);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add_mem);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Truss_Mat_Props";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Truss Material Property";
            this.Load += new System.EventHandler(this.frm_Truss_Mat_Props_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_weight_den;
        private System.Windows.Forms.TextBox txt_area;
        private System.Windows.Forms.TextBox txt_mass_den;
        private System.Windows.Forms.TextBox txt_therm_coeff;
        private System.Windows.Forms.TextBox txt_mat_no;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_mod;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_add_mem;
    }
}