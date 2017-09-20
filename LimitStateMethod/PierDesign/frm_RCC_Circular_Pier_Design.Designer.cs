namespace LimitStateMethod.PierDesign
{
    partial class frm_RCC_Circular_Pier_Design
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RCC_Circular_Pier_Design));
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btn_ws_open_cir_well = new System.Windows.Forms.Button();
            this.btn_ws_new_cir_well = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btn_ws_open_cir = new System.Windows.Forms.Button();
            this.btn_ws_new_cir = new System.Windows.Forms.Button();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btn_ws_open_cir_well);
            this.groupBox14.Controls.Add(this.btn_ws_new_cir_well);
            this.groupBox14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(95, 148);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(403, 107);
            this.groupBox14.TabIndex = 6;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Design of RCC Circular Pier with Well Foundation";
            // 
            // btn_ws_open_cir_well
            // 
            this.btn_ws_open_cir_well.Location = new System.Drawing.Point(225, 36);
            this.btn_ws_open_cir_well.Name = "btn_ws_open_cir_well";
            this.btn_ws_open_cir_well.Size = new System.Drawing.Size(144, 51);
            this.btn_ws_open_cir_well.TabIndex = 4;
            this.btn_ws_open_cir_well.Text = "Open Design";
            this.btn_ws_open_cir_well.UseVisualStyleBackColor = true;
            this.btn_ws_open_cir_well.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_ws_new_cir_well
            // 
            this.btn_ws_new_cir_well.Location = new System.Drawing.Point(57, 36);
            this.btn_ws_new_cir_well.Name = "btn_ws_new_cir_well";
            this.btn_ws_new_cir_well.Size = new System.Drawing.Size(144, 51);
            this.btn_ws_new_cir_well.TabIndex = 0;
            this.btn_ws_new_cir_well.Text = "New Design ";
            this.btn_ws_new_cir_well.UseVisualStyleBackColor = true;
            this.btn_ws_new_cir_well.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btn_ws_open_cir);
            this.groupBox13.Controls.Add(this.btn_ws_new_cir);
            this.groupBox13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.Location = new System.Drawing.Point(95, 21);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(403, 104);
            this.groupBox13.TabIndex = 7;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Design of RCC Circular Pier";
            // 
            // btn_ws_open_cir
            // 
            this.btn_ws_open_cir.Location = new System.Drawing.Point(225, 34);
            this.btn_ws_open_cir.Name = "btn_ws_open_cir";
            this.btn_ws_open_cir.Size = new System.Drawing.Size(144, 51);
            this.btn_ws_open_cir.TabIndex = 4;
            this.btn_ws_open_cir.Text = "Open Design";
            this.btn_ws_open_cir.UseVisualStyleBackColor = true;
            this.btn_ws_open_cir.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // btn_ws_new_cir
            // 
            this.btn_ws_new_cir.Location = new System.Drawing.Point(57, 34);
            this.btn_ws_new_cir.Name = "btn_ws_new_cir";
            this.btn_ws_new_cir.Size = new System.Drawing.Size(144, 51);
            this.btn_ws_new_cir.TabIndex = 0;
            this.btn_ws_new_cir.Text = "New Design ";
            this.btn_ws_new_cir.UseVisualStyleBackColor = true;
            this.btn_ws_new_cir.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // frm_RCC_Circular_Pier_Design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 279);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.groupBox13);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_RCC_Circular_Pier_Design";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design of RCC Circular Pier";
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button btn_ws_open_cir_well;
        private System.Windows.Forms.Button btn_ws_new_cir_well;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btn_ws_open_cir;
        private System.Windows.Forms.Button btn_ws_new_cir;
    }
}