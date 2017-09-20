namespace AstraFunctionOne.BridgeDesign
{
    partial class frmCompositWorksheetDesign
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
            this.btn_working_folder = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_design = new System.Windows.Forms.Button();
            this.btn_drawing = new System.Windows.Forms.Button();
            this.btn_open_desg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_working_folder
            // 
            this.btn_working_folder.Location = new System.Drawing.Point(12, 12);
            this.btn_working_folder.Name = "btn_working_folder";
            this.btn_working_folder.Size = new System.Drawing.Size(106, 23);
            this.btn_working_folder.TabIndex = 51;
            this.btn_working_folder.Text = "Working Folder";
            this.btn_working_folder.UseVisualStyleBackColor = true;
            this.btn_working_folder.Click += new System.EventHandler(this.btn_working_folder_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(440, 12);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 50;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_design
            // 
            this.btn_design.Location = new System.Drawing.Point(255, 12);
            this.btn_design.Name = "btn_design";
            this.btn_design.Size = new System.Drawing.Size(75, 23);
            this.btn_design.TabIndex = 49;
            this.btn_design.Text = "New Design";
            this.btn_design.UseVisualStyleBackColor = true;
            this.btn_design.Click += new System.EventHandler(this.btn_design_Click);
            // 
            // btn_drawing
            // 
            this.btn_drawing.Location = new System.Drawing.Point(336, 12);
            this.btn_drawing.Name = "btn_drawing";
            this.btn_drawing.Size = new System.Drawing.Size(98, 23);
            this.btn_drawing.TabIndex = 48;
            this.btn_drawing.Text = "Default Drawings";
            this.btn_drawing.UseVisualStyleBackColor = true;
            this.btn_drawing.Click += new System.EventHandler(this.btn_drawing_Click);
            // 
            // btn_open_desg
            // 
            this.btn_open_desg.Location = new System.Drawing.Point(124, 12);
            this.btn_open_desg.Name = "btn_open_desg";
            this.btn_open_desg.Size = new System.Drawing.Size(125, 23);
            this.btn_open_desg.TabIndex = 52;
            this.btn_open_desg.Text = "Open  Design";
            this.btn_open_desg.UseVisualStyleBackColor = true;
            this.btn_open_desg.Click += new System.EventHandler(this.btn_prev_desg_Click);
            // 
            // frmCompositWorksheetDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 43);
            this.Controls.Add(this.btn_open_desg);
            this.Controls.Add(this.btn_working_folder);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_design);
            this.Controls.Add(this.btn_drawing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompositWorksheetDesign";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Composite Bridge : Worksheet Design";
            this.Load += new System.EventHandler(this.frmCompositWorksheetDesign_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_working_folder;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_design;
        private System.Windows.Forms.Button btn_drawing;
        private System.Windows.Forms.Button btn_open_desg;
    }
}