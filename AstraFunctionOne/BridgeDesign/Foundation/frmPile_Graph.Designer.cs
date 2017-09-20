namespace AstraFunctionOne.BridgeDesign.Foundation
{
    partial class frmPile_Graph
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_sigma_y = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txt_sigma_ck = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ddash = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Pu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Mu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_obtaned_value = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.pile_fond_graph;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(1, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 509);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(402, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "σ_y (N/sq.mm)";
            // 
            // txt_sigma_y
            // 
            this.txt_sigma_y.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_y.Location = new System.Drawing.Point(645, 48);
            this.txt_sigma_y.Name = "txt_sigma_y";
            this.txt_sigma_y.ReadOnly = true;
            this.txt_sigma_y.Size = new System.Drawing.Size(79, 23);
            this.txt_sigma_y.TabIndex = 2;
            this.txt_sigma_y.Text = "415";
            this.txt_sigma_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(478, 396);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(147, 48);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txt_sigma_ck
            // 
            this.txt_sigma_ck.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sigma_ck.Location = new System.Drawing.Point(645, 74);
            this.txt_sigma_ck.Name = "txt_sigma_ck";
            this.txt_sigma_ck.ReadOnly = true;
            this.txt_sigma_ck.Size = new System.Drawing.Size(79, 23);
            this.txt_sigma_ck.TabIndex = 5;
            this.txt_sigma_ck.Text = "20";
            this.txt_sigma_ck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(402, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "σ_ck (N/sq.mm)";
            // 
            // txt_ddash
            // 
            this.txt_ddash.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ddash.Location = new System.Drawing.Point(645, 100);
            this.txt_ddash.Name = "txt_ddash";
            this.txt_ddash.ReadOnly = true;
            this.txt_ddash.Size = new System.Drawing.Size(79, 23);
            this.txt_ddash.TabIndex = 7;
            this.txt_ddash.Text = "0.1";
            this.txt_ddash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(402, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "d\' / D";
            // 
            // txt_Pu
            // 
            this.txt_Pu.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pu.Location = new System.Drawing.Point(645, 126);
            this.txt_Pu.Name = "txt_Pu";
            this.txt_Pu.ReadOnly = true;
            this.txt_Pu.Size = new System.Drawing.Size(79, 23);
            this.txt_Pu.TabIndex = 9;
            this.txt_Pu.Text = "0.0325";
            this.txt_Pu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(402, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pu/σ_ck*D*D";
            // 
            // txt_Mu
            // 
            this.txt_Mu.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Mu.Location = new System.Drawing.Point(645, 152);
            this.txt_Mu.Name = "txt_Mu";
            this.txt_Mu.ReadOnly = true;
            this.txt_Mu.Size = new System.Drawing.Size(79, 23);
            this.txt_Mu.TabIndex = 11;
            this.txt_Mu.Text = "0.0025";
            this.txt_Mu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(402, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mu/σ_ck*D*D*D";
            // 
            // txt_obtaned_value
            // 
            this.txt_obtaned_value.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_obtaned_value.Location = new System.Drawing.Point(645, 234);
            this.txt_obtaned_value.Name = "txt_obtaned_value";
            this.txt_obtaned_value.Size = new System.Drawing.Size(79, 27);
            this.txt_obtaned_value.TabIndex = 0;
            this.txt_obtaned_value.Text = "0.0";
            this.txt_obtaned_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_obtaned_value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_obtaned_value_KeyDown);
            this.txt_obtaned_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_obtaned_value_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(402, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "p/σ_ck";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(385, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(351, 56);
            this.label7.TabIndex = 14;
            this.label7.Text = "Note :\r\nThis is a sample figure. User may refer to any other\r\nequivalent figure r" +
    "elevant to user\'s design data and \r\nobtain the value of \"p/σ_ck\".";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(385, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "Design Data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(385, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Obtained Value";
            // 
            // frmPile_Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 505);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_obtaned_value);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Mu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Pu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_ddash);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_sigma_ck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txt_sigma_y);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPile_Graph";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design curve for a circular column for different steel ratios";
            this.Load += new System.EventHandler(this.frmPile_Graph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txt_sigma_y;
        public System.Windows.Forms.TextBox txt_sigma_ck;
        public System.Windows.Forms.TextBox txt_ddash;
        public System.Windows.Forms.TextBox txt_Pu;
        public System.Windows.Forms.TextBox txt_Mu;
        public System.Windows.Forms.TextBox txt_obtaned_value;
    }
}