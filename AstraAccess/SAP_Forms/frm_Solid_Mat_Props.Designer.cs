namespace AstraAccess.SAP_Forms
{
    partial class frm_Solid_Mat_Props
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
            this.label4 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_wgt_den = new System.Windows.Forms.TextBox();
            this.txt_coeff = new System.Windows.Forms.TextBox();
            this.txt_pr = new System.Windows.Forms.TextBox();
            this.txt_mat_no = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_mod_elas = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.grb_Member.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(206, 190);
            this.btn_close.Margin = new System.Windows.Forms.Padding(16, 3, 16, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 37);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_wgt_den);
            this.grb_Member.Controls.Add(this.txt_coeff);
            this.grb_Member.Controls.Add(this.txt_pr);
            this.grb_Member.Controls.Add(this.txt_mat_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_mod_elas);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(13, 12);
            this.grb_Member.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grb_Member.Size = new System.Drawing.Size(348, 172);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "Material Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 112);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Weight Density";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Coefficient of Thermal expansion ";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 86);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 14);
            this.label30.TabIndex = 1;
            this.label30.Text = "Poisson\'s Ratio";
            // 
            // txt_wgt_den
            // 
            this.txt_wgt_den.Location = new System.Drawing.Point(229, 109);
            this.txt_wgt_den.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_wgt_den.Name = "txt_wgt_den";
            this.txt_wgt_den.Size = new System.Drawing.Size(110, 22);
            this.txt_wgt_den.TabIndex = 3;
            this.txt_wgt_den.Text = "2.40";
            this.txt_wgt_den.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_coeff
            // 
            this.txt_coeff.Location = new System.Drawing.Point(229, 141);
            this.txt_coeff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_coeff.Name = "txt_coeff";
            this.txt_coeff.Size = new System.Drawing.Size(110, 22);
            this.txt_coeff.TabIndex = 4;
            this.txt_coeff.Text = "2.40";
            this.txt_coeff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_pr
            // 
            this.txt_pr.Location = new System.Drawing.Point(229, 80);
            this.txt_pr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_pr.Name = "txt_pr";
            this.txt_pr.Size = new System.Drawing.Size(110, 22);
            this.txt_pr.TabIndex = 2;
            this.txt_pr.Text = "0.150";
            this.txt_pr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mat_no
            // 
            this.txt_mat_no.Location = new System.Drawing.Point(229, 22);
            this.txt_mat_no.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mat_no.Name = "txt_mat_no";
            this.txt_mat_no.Size = new System.Drawing.Size(55, 22);
            this.txt_mat_no.TabIndex = 0;
            this.txt_mat_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 25);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(164, 14);
            this.label29.TabIndex = 1;
            this.label29.Text = "Material Identification No";
            // 
            // txt_mod_elas
            // 
            this.txt_mod_elas.Location = new System.Drawing.Point(229, 51);
            this.txt_mod_elas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mod_elas.Name = "txt_mod_elas";
            this.txt_mod_elas.Size = new System.Drawing.Size(110, 22);
            this.txt_mod_elas.TabIndex = 1;
            this.txt_mod_elas.Text = "285000";
            this.txt_mod_elas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(12, 54);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(134, 14);
            this.label33.TabIndex = 1;
            this.label33.Text = "Modulus of Elasticity";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(61, 190);
            this.btn_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(108, 37);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "ADD";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // frm_Solid_Mat_Props
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 243);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Solid_Mat_Props";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solid Material Properties";
            this.Load += new System.EventHandler(this.frm_Solid_Mat_Props_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_wgt_den;
        private System.Windows.Forms.TextBox txt_coeff;
        private System.Windows.Forms.TextBox txt_pr;
        private System.Windows.Forms.TextBox txt_mat_no;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_mod_elas;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_add;
    }
}