namespace LimitStateMethod.Steel_Truss
{
    partial class frm_Arch_Input_Files
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
            this.rbtn_5 = new System.Windows.Forms.RadioButton();
            this.rbtn_3 = new System.Windows.Forms.RadioButton();
            this.rbtn_4 = new System.Windows.Forms.RadioButton();
            this.rbtn_2 = new System.Windows.Forms.RadioButton();
            this.rbtn_1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_5);
            this.groupBox1.Controls.Add(this.rbtn_3);
            this.groupBox1.Controls.Add(this.rbtn_4);
            this.groupBox1.Controls.Add(this.rbtn_2);
            this.groupBox1.Controls.Add(this.rbtn_1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Analysis Type";
            // 
            // rbtn_5
            // 
            this.rbtn_5.AutoSize = true;
            this.rbtn_5.Location = new System.Drawing.Point(17, 123);
            this.rbtn_5.Name = "rbtn_5";
            this.rbtn_5.Size = new System.Drawing.Size(271, 20);
            this.rbtn_5.TabIndex = 0;
            this.rbtn_5.TabStop = true;
            this.rbtn_5.Text = "Analysis Removing Both Outer Cables";
            this.rbtn_5.UseVisualStyleBackColor = true;
            // 
            // rbtn_3
            // 
            this.rbtn_3.AutoSize = true;
            this.rbtn_3.Location = new System.Drawing.Point(17, 71);
            this.rbtn_3.Name = "rbtn_3";
            this.rbtn_3.Size = new System.Drawing.Size(267, 20);
            this.rbtn_3.TabIndex = 0;
            this.rbtn_3.TabStop = true;
            this.rbtn_3.Text = "Analysis Removing Left Outer Cables";
            this.rbtn_3.UseVisualStyleBackColor = true;
            // 
            // rbtn_4
            // 
            this.rbtn_4.AutoSize = true;
            this.rbtn_4.Location = new System.Drawing.Point(17, 97);
            this.rbtn_4.Name = "rbtn_4";
            this.rbtn_4.Size = new System.Drawing.Size(268, 20);
            this.rbtn_4.TabIndex = 0;
            this.rbtn_4.TabStop = true;
            this.rbtn_4.Text = "Analysis Removing Both Inner Cables";
            this.rbtn_4.UseVisualStyleBackColor = true;
            // 
            // rbtn_2
            // 
            this.rbtn_2.AutoSize = true;
            this.rbtn_2.Location = new System.Drawing.Point(17, 45);
            this.rbtn_2.Name = "rbtn_2";
            this.rbtn_2.Size = new System.Drawing.Size(264, 20);
            this.rbtn_2.TabIndex = 0;
            this.rbtn_2.TabStop = true;
            this.rbtn_2.Text = "Analysis Removing Left Inner Cables";
            this.rbtn_2.UseVisualStyleBackColor = true;
            // 
            // rbtn_1
            // 
            this.rbtn_1.AutoSize = true;
            this.rbtn_1.Location = new System.Drawing.Point(17, 22);
            this.rbtn_1.Name = "rbtn_1";
            this.rbtn_1.Size = new System.Drawing.Size(114, 20);
            this.rbtn_1.TabIndex = 0;
            this.rbtn_1.TabStop = true;
            this.rbtn_1.Text = "Main Analysis";
            this.rbtn_1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(96, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_Arch_Input_Files
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 213);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_Arch_Input_Files";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARCH CABLE SUSPENSION BRIDGE";
            this.Load += new System.EventHandler(this.frm_Arch_Input_Files_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_5;
        private System.Windows.Forms.RadioButton rbtn_3;
        private System.Windows.Forms.RadioButton rbtn_4;
        private System.Windows.Forms.RadioButton rbtn_2;
        private System.Windows.Forms.RadioButton rbtn_1;
        private System.Windows.Forms.Button button1;
    }
}