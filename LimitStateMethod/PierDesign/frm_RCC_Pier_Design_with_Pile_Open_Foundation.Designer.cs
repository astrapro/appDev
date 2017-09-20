namespace LimitStateMethod.PierDesign
{
    partial class frm_RCC_Pier_Design_with_Pile_Open_Foundation
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
            this.btn_drawings = new System.Windows.Forms.Button();
            this.btn_worksheet_1_open = new System.Windows.Forms.Button();
            this.btn_worksheet_1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_drawings);
            this.groupBox1.Controls.Add(this.btn_worksheet_1_open);
            this.groupBox1.Controls.Add(this.btn_worksheet_1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Design of RCC Pier with Pile / Open Foundation";
            // 
            // btn_drawings
            // 
            this.btn_drawings.Location = new System.Drawing.Point(406, 39);
            this.btn_drawings.Name = "btn_drawings";
            this.btn_drawings.Size = new System.Drawing.Size(163, 48);
            this.btn_drawings.TabIndex = 0;
            this.btn_drawings.Text = "Open Drawings";
            this.btn_drawings.UseVisualStyleBackColor = true;
            this.btn_drawings.Click += new System.EventHandler(this.btn_drawings_Click);
            // 
            // btn_worksheet_1_open
            // 
            this.btn_worksheet_1_open.Location = new System.Drawing.Point(211, 39);
            this.btn_worksheet_1_open.Name = "btn_worksheet_1_open";
            this.btn_worksheet_1_open.Size = new System.Drawing.Size(163, 48);
            this.btn_worksheet_1_open.TabIndex = 0;
            this.btn_worksheet_1_open.Text = "Open Design";
            this.btn_worksheet_1_open.UseVisualStyleBackColor = true;
            this.btn_worksheet_1_open.Click += new System.EventHandler(this.btn_worksheet_1_open_Click);
            // 
            // btn_worksheet_1
            // 
            this.btn_worksheet_1.Location = new System.Drawing.Point(18, 39);
            this.btn_worksheet_1.Name = "btn_worksheet_1";
            this.btn_worksheet_1.Size = new System.Drawing.Size(163, 48);
            this.btn_worksheet_1.TabIndex = 0;
            this.btn_worksheet_1.Text = "New Design";
            this.btn_worksheet_1.UseVisualStyleBackColor = true;
            this.btn_worksheet_1.Click += new System.EventHandler(this.btn_worksheet_1_Click);
            // 
            // frm_RCC_Pier_Design_with_Pile_Open_Foundation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 241);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frm_RCC_Pier_Design_with_Pile_Open_Foundation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design of RCC Pier with Pile / Open Foundation";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_worksheet_1_open;
        private System.Windows.Forms.Button btn_worksheet_1;
        private System.Windows.Forms.Button btn_drawings;
    }
}