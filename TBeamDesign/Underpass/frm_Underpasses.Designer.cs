namespace BridgeAnalysisDesign.Underpass
{
    partial class frm_Underpasses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Underpasses));
            this.btn_dwg_vup = new System.Windows.Forms.Button();
            this.grb1 = new System.Windows.Forms.GroupBox();
            this.btn_dwg_rob = new System.Windows.Forms.Button();
            this.btn_dwg_pup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label50 = new System.Windows.Forms.Label();
            this.grb1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_dwg_vup
            // 
            this.btn_dwg_vup.Location = new System.Drawing.Point(30, 33);
            this.btn_dwg_vup.Name = "btn_dwg_vup";
            this.btn_dwg_vup.Size = new System.Drawing.Size(546, 33);
            this.btn_dwg_vup.TabIndex = 0;
            this.btn_dwg_vup.Text = "General Arrangememt Drawing for Standard Vehicular Under Pass (VUP)";
            this.btn_dwg_vup.UseVisualStyleBackColor = true;
            this.btn_dwg_vup.Click += new System.EventHandler(this.btn_dwg_Click);
            // 
            // grb1
            // 
            this.grb1.Controls.Add(this.btn_dwg_rob);
            this.grb1.Controls.Add(this.btn_dwg_pup);
            this.grb1.Controls.Add(this.btn_dwg_vup);
            this.grb1.Location = new System.Drawing.Point(167, 55);
            this.grb1.Name = "grb1";
            this.grb1.Size = new System.Drawing.Size(582, 311);
            this.grb1.TabIndex = 1;
            this.grb1.TabStop = false;
            // 
            // btn_dwg_rob
            // 
            this.btn_dwg_rob.Location = new System.Drawing.Point(30, 247);
            this.btn_dwg_rob.Name = "btn_dwg_rob";
            this.btn_dwg_rob.Size = new System.Drawing.Size(546, 33);
            this.btn_dwg_rob.TabIndex = 0;
            this.btn_dwg_rob.Text = "General Arrangememt Drawing for Standard Railway Over Bridge (ROB)";
            this.btn_dwg_rob.UseVisualStyleBackColor = true;
            this.btn_dwg_rob.Click += new System.EventHandler(this.btn_dwg_Click);
            // 
            // btn_dwg_pup
            // 
            this.btn_dwg_pup.Location = new System.Drawing.Point(30, 141);
            this.btn_dwg_pup.Name = "btn_dwg_pup";
            this.btn_dwg_pup.Size = new System.Drawing.Size(546, 33);
            this.btn_dwg_pup.TabIndex = 0;
            this.btn_dwg_pup.Text = "General Arrangememt Drawing for Standard Pedestrian Under Pass (PUP)";
            this.btn_dwg_pup.UseVisualStyleBackColor = true;
            this.btn_dwg_pup.Click += new System.EventHandler(this.btn_dwg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(702, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(98, 433);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(721, 76);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Note : ";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(304, 9);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(308, 23);
            this.label50.TabIndex = 79;
            this.label50.Text = "Editable Construction Drawings";
            // 
            // frm_Underpasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 588);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grb1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Underpasses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GADs for ROB and Underpasses";
            this.grb1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dwg_vup;
        private System.Windows.Forms.GroupBox grb1;
        private System.Windows.Forms.Button btn_dwg_rob;
        private System.Windows.Forms.Button btn_dwg_pup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label50;
    }
}