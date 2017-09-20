namespace AstraFunctionOne
{
    partial class frmTimerScreen
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pcb_Images = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Images)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 22);
            this.label1.TabIndex = 1;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcb_Images
            // 
            this.pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower3;
            this.pcb_Images.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcb_Images.Location = new System.Drawing.Point(12, 12);
            this.pcb_Images.Name = "pcb_Images";
            this.pcb_Images.Size = new System.Drawing.Size(661, 473);
            this.pcb_Images.TabIndex = 0;
            this.pcb_Images.TabStop = false;
            this.pcb_Images.Click += new System.EventHandler(this.pcb_Images_Click);
            // 
            // frmTimerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcb_Images);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTimerScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTimerScreen";
            this.Load += new System.EventHandler(this.frmTimerScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Images)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Images;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}