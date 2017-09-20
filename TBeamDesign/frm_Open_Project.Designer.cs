namespace BridgeAnalysisDesign
{
      partial class frm_Open_Project
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_working_folder = new System.Windows.Forms.TextBox();
            this.lst_proj_folders = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tv_files = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_save_as = new System.Windows.Forms.TextBox();
            this.btn_save_as = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User\'s Working Folder";
            // 
            // txt_working_folder
            // 
            this.txt_working_folder.Location = new System.Drawing.Point(9, 23);
            this.txt_working_folder.Name = "txt_working_folder";
            this.txt_working_folder.ReadOnly = true;
            this.txt_working_folder.Size = new System.Drawing.Size(351, 21);
            this.txt_working_folder.TabIndex = 1;
            // 
            // lst_proj_folders
            // 
            this.lst_proj_folders.FormattingEnabled = true;
            this.lst_proj_folders.Location = new System.Drawing.Point(10, 74);
            this.lst_proj_folders.Name = "lst_proj_folders";
            this.lst_proj_folders.ScrollAlwaysVisible = true;
            this.lst_proj_folders.Size = new System.Drawing.Size(350, 199);
            this.lst_proj_folders.TabIndex = 2;
            this.lst_proj_folders.SelectedIndexChanged += new System.EventHandler(this.lst_proj_folders_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select User\'s Project Folder";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(46, 318);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(115, 30);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(206, 318);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 30);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // tv_files
            // 
            this.tv_files.Location = new System.Drawing.Point(366, 74);
            this.tv_files.Name = "tv_files";
            this.tv_files.Size = new System.Drawing.Size(306, 238);
            this.tv_files.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Project Folder Contains..";
            // 
            // txt_save_as
            // 
            this.txt_save_as.Location = new System.Drawing.Point(46, 279);
            this.txt_save_as.Name = "txt_save_as";
            this.txt_save_as.Size = new System.Drawing.Size(189, 21);
            this.txt_save_as.TabIndex = 7;
            // 
            // btn_save_as
            // 
            this.btn_save_as.Location = new System.Drawing.Point(246, 277);
            this.btn_save_as.Name = "btn_save_as";
            this.btn_save_as.Size = new System.Drawing.Size(75, 23);
            this.btn_save_as.TabIndex = 8;
            this.btn_save_as.Text = "Save As";
            this.btn_save_as.UseVisualStyleBackColor = true;
            this.btn_save_as.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // frm_Open_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 356);
            this.Controls.Add(this.btn_save_as);
            this.Controls.Add(this.txt_save_as);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tv_files);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_proj_folders);
            this.Controls.Add(this.txt_working_folder);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Open_Project";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Folder Selection Dialog";
            this.Load += new System.EventHandler(this.frm_Project_Folder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_working_folder;
        private System.Windows.Forms.ListBox lst_proj_folders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TreeView tv_files;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_save_as;
        private System.Windows.Forms.Button btn_save_as;
    }
}