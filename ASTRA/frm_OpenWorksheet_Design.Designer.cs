namespace AstraFunctionOne
{
    partial class frm_OpenWorksheet_Design
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_OpenWorksheet_Design));
            this.btn_open_desg = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_design = new System.Windows.Forms.Button();
            this.btn_drawing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_open_desg
            // 
            this.btn_open_desg.Location = new System.Drawing.Point(147, 12);
            this.btn_open_desg.Name = "btn_open_desg";
            this.btn_open_desg.Size = new System.Drawing.Size(111, 23);
            this.btn_open_desg.TabIndex = 57;
            this.btn_open_desg.Text = "Open  Design";
            this.btn_open_desg.UseVisualStyleBackColor = true;
            this.btn_open_desg.Click += new System.EventHandler(this.btn_open_desg_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(285, 12);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 55;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_design
            // 
            this.btn_design.Location = new System.Drawing.Point(12, 12);
            this.btn_design.Name = "btn_design";
            this.btn_design.Size = new System.Drawing.Size(111, 23);
            this.btn_design.TabIndex = 54;
            this.btn_design.Text = "New Design";
            this.btn_design.UseVisualStyleBackColor = true;
            this.btn_design.Click += new System.EventHandler(this.btn_design_Click);
            // 
            // btn_drawing
            // 
            this.btn_drawing.Enabled = false;
            this.btn_drawing.Location = new System.Drawing.Point(396, 37);
            this.btn_drawing.Name = "btn_drawing";
            this.btn_drawing.Size = new System.Drawing.Size(98, 23);
            this.btn_drawing.TabIndex = 53;
            this.btn_drawing.Text = "Default Drawings";
            this.btn_drawing.UseVisualStyleBackColor = true;
            this.btn_drawing.Click += new System.EventHandler(this.btn_drawing_Click);
            // 
            // frm_OpenWorksheet_Design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 56);
            this.Controls.Add(this.btn_open_desg);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_design);
            this.Controls.Add(this.btn_drawing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_OpenWorksheet_Design";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_OpenWorksheet_Design";
            this.Load += new System.EventHandler(this.frm_OpenWorksheet_Design_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_open_desg;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_design;
        private System.Windows.Forms.Button btn_drawing;
    }
}