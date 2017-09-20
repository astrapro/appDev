namespace AstraFunctionOne.BridgeDesign
{
    partial class frmCurve
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
            this.pbCurve = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txt_m1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_m2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_k = new System.Windows.Forms.TextBox();
            this.grb_u_B = new System.Windows.Forms.GroupBox();
            this.txt_v_L = new System.Windows.Forms.TextBox();
            this.txt_u_b = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChangeText = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurve)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.grb_u_B.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCurve
            // 
            this.pbCurve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCurve.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCurve.Location = new System.Drawing.Point(12, 12);
            this.pbCurve.Name = "pbCurve";
            this.pbCurve.Size = new System.Drawing.Size(631, 491);
            this.pbCurve.TabIndex = 0;
            this.pbCurve.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(694, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txt_m1
            // 
            this.txt_m1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_m1.Location = new System.Drawing.Point(45, 31);
            this.txt_m1.Name = "txt_m1";
            this.txt_m1.Size = new System.Drawing.Size(87, 20);
            this.txt_m1.TabIndex = 0;
            this.txt_m1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_m1.TextChanged += new System.EventHandler(this.txt_m1_TextChanged);
            this.txt_m1.Validated += new System.EventHandler(this.txt_m1_TextChanged);
            this.txt_m1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_m1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "m1 =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "m2 =";
            // 
            // txt_m2
            // 
            this.txt_m2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_m2.Location = new System.Drawing.Point(45, 57);
            this.txt_m2.Name = "txt_m2";
            this.txt_m2.Size = new System.Drawing.Size(87, 20);
            this.txt_m2.TabIndex = 1;
            this.txt_m2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_m2.TextChanged += new System.EventHandler(this.txt_m1_TextChanged);
            this.txt_m2.Validated += new System.EventHandler(this.txt_m1_TextChanged);
            this.txt_m2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_m1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "k =";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_k);
            this.groupBox3.Controls.Add(this.grb_u_B);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(649, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(145, 118);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // txt_k
            // 
            this.txt_k.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_k.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_k.Location = new System.Drawing.Point(45, 15);
            this.txt_k.Name = "txt_k";
            this.txt_k.ReadOnly = true;
            this.txt_k.Size = new System.Drawing.Size(87, 26);
            this.txt_k.TabIndex = 6;
            this.txt_k.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grb_u_B
            // 
            this.grb_u_B.Controls.Add(this.txt_v_L);
            this.grb_u_B.Controls.Add(this.txt_u_b);
            this.grb_u_B.Controls.Add(this.label4);
            this.grb_u_B.Controls.Add(this.lblChangeText);
            this.grb_u_B.Location = new System.Drawing.Point(12, 38);
            this.grb_u_B.Name = "grb_u_B";
            this.grb_u_B.Size = new System.Drawing.Size(121, 76);
            this.grb_u_B.TabIndex = 7;
            this.grb_u_B.TabStop = false;
            // 
            // txt_v_L
            // 
            this.txt_v_L.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_v_L.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_v_L.Location = new System.Drawing.Point(45, 45);
            this.txt_v_L.Name = "txt_v_L";
            this.txt_v_L.ReadOnly = true;
            this.txt_v_L.Size = new System.Drawing.Size(66, 20);
            this.txt_v_L.TabIndex = 4;
            this.txt_v_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_u_b
            // 
            this.txt_u_b.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_u_b.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_u_b.Location = new System.Drawing.Point(45, 19);
            this.txt_u_b.Name = "txt_u_b";
            this.txt_u_b.ReadOnly = true;
            this.txt_u_b.Size = new System.Drawing.Size(66, 20);
            this.txt_u_b.TabIndex = 2;
            this.txt_u_b.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "v / L";
            // 
            // lblChangeText
            // 
            this.lblChangeText.AutoSize = true;
            this.lblChangeText.Location = new System.Drawing.Point(9, 22);
            this.lblChangeText.Name = "lblChangeText";
            this.lblChangeText.Size = new System.Drawing.Size(31, 13);
            this.lblChangeText.TabIndex = 3;
            this.lblChangeText.Text = "u / B";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txt_m1);
            this.groupBox4.Controls.Add(this.txt_m2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(649, 136);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(145, 83);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "value ( >= 0.0   and <= 0.1)";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(153, 508);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(355, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "Select m1 and m2 from Pigeaud\'s curve";
            // 
            // frmCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 531);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pbCurve);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCurve";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select m1 and m2 from Pigeaud\'s curve";
            this.Load += new System.EventHandler(this.frmCurve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurve)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grb_u_B.ResumeLayout(false);
            this.grb_u_B.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCurve;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grb_u_B;
        private System.Windows.Forms.TextBox txt_v_L;
        private System.Windows.Forms.TextBox txt_u_b;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChangeText;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_k;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.TextBox txt_m1;
        public System.Windows.Forms.TextBox txt_m2;
        private System.Windows.Forms.Label label6;
    }
}