namespace AstraFunctionOne
{
    partial class frm_Open_Drawings
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
            this.btn_drawing = new System.Windows.Forms.Button();
            this.cmb_drawing = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_working_folder
            // 
            this.btn_working_folder.Location = new System.Drawing.Point(6, 23);
            this.btn_working_folder.Name = "btn_working_folder";
            this.btn_working_folder.Size = new System.Drawing.Size(105, 23);
            this.btn_working_folder.TabIndex = 61;
            this.btn_working_folder.Text = "Working Folder";
            this.btn_working_folder.UseVisualStyleBackColor = true;
            this.btn_working_folder.Click += new System.EventHandler(this.btn_working_folder_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(531, 22);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 60;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_drawing
            // 
            this.btn_drawing.Enabled = false;
            this.btn_drawing.Location = new System.Drawing.Point(427, 22);
            this.btn_drawing.Name = "btn_drawing";
            this.btn_drawing.Size = new System.Drawing.Size(98, 23);
            this.btn_drawing.TabIndex = 58;
            this.btn_drawing.Text = "Open Drawings";
            this.btn_drawing.UseVisualStyleBackColor = true;
            this.btn_drawing.Click += new System.EventHandler(this.btn_drawing_Click);
            // 
            // cmb_drawing
            // 
            this.cmb_drawing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_drawing.Enabled = false;
            this.cmb_drawing.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_drawing.FormattingEnabled = true;
            this.cmb_drawing.Location = new System.Drawing.Point(117, 23);
            this.cmb_drawing.Name = "cmb_drawing";
            this.cmb_drawing.Size = new System.Drawing.Size(304, 21);
            this.cmb_drawing.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "Select Drawing";
            // 
            // frm_Open_Drawings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 52);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_drawing);
            this.Controls.Add(this.btn_working_folder);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_drawing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frm_Open_Drawings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Drawings";
            this.Load += new System.EventHandler(this.frm_Open_Drawings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_working_folder;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_drawing;
        private System.Windows.Forms.ComboBox cmb_drawing;
        private System.Windows.Forms.Label label1;
    }
}