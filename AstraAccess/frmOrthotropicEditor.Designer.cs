namespace AstraAccess
{
    partial class frmOrthotropicEditor
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
            this.uC_Orthotropic1 = new AstraAccess.ADOC.UC_Orthotropic();
            this.SuspendLayout();
            // 
            // uC_Orthotropic1
            // 
            this.uC_Orthotropic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Orthotropic1.Location = new System.Drawing.Point(0, 0);
            this.uC_Orthotropic1.Name = "uC_Orthotropic1";
            this.uC_Orthotropic1.Size = new System.Drawing.Size(740, 417);
            this.uC_Orthotropic1.TabIndex = 0;
            // 
            // frmOrthotropicEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 417);
            this.Controls.Add(this.uC_Orthotropic1);
            this.Name = "frmOrthotropicEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrthotropicEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ADOC.UC_Orthotropic uC_Orthotropic1;
    }
}