namespace AstraFunctionOne
{
    partial class frmVideos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVideos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_video = new System.Windows.Forms.Button();
            this.lst_tutorial = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_video);
            this.groupBox1.Controls.Add(this.lst_tutorial);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 185);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Tutorial";
            // 
            // btn_video
            // 
            this.btn_video.Location = new System.Drawing.Point(31, 141);
            this.btn_video.Name = "btn_video";
            this.btn_video.Size = new System.Drawing.Size(199, 37);
            this.btn_video.TabIndex = 1;
            this.btn_video.Text = "Watch Video";
            this.btn_video.UseVisualStyleBackColor = true;
            this.btn_video.Click += new System.EventHandler(this.btn_video_Click);
            // 
            // lst_tutorial
            // 
            this.lst_tutorial.Dock = System.Windows.Forms.DockStyle.Top;
            this.lst_tutorial.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_tutorial.FormattingEnabled = true;
            this.lst_tutorial.ItemHeight = 16;
            this.lst_tutorial.Items.AddRange(new object[] {
            "RCC T-Girder Bridge Tutorial",
            "Composite Bridge Tutorial",
            "PSC I-Girder Bridge Tutorial",
            "PSC Box Girder Bridge Tutorial",
            "Steel Truss Bridge Tutorial"});
            this.lst_tutorial.Location = new System.Drawing.Point(3, 19);
            this.lst_tutorial.Name = "lst_tutorial";
            this.lst_tutorial.Size = new System.Drawing.Size(266, 116);
            this.lst_tutorial.TabIndex = 0;
            // 
            // frmVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 185);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVideos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Tutorial Training Videos";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_video;
        private System.Windows.Forms.ListBox lst_tutorial;
    }
}