namespace AstraAccess.StructureAnalysisDesign
{
    partial class frm_Tunnel_Design
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Tunnel_Design));
            this.lininG_DOC1 = new Tunnel_Lining.LINING_DOC();
            this.SuspendLayout();
            // 
            // lininG_DOC1
            // 
            this.lininG_DOC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lininG_DOC1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lininG_DOC1.H = 7.375D;
            this.lininG_DOC1.Input_File = "";
            this.lininG_DOC1.IsDemo = false;
            this.lininG_DOC1.L = 45D;
            this.lininG_DOC1.Location = new System.Drawing.Point(0, 0);
            this.lininG_DOC1.Name = "lininG_DOC1";
            this.lininG_DOC1.Seismic_Input_File = null;
            this.lininG_DOC1.Size = new System.Drawing.Size(973, 682);
            this.lininG_DOC1.TabIndex = 0;
            this.lininG_DOC1.Table_Lining = null;
            this.lininG_DOC1.W = 10.75D;
            this.lininG_DOC1.Working_Folder = "";
            this.lininG_DOC1.Load += new System.EventHandler(this.lininG_DOC1_Load);
            // 
            // frm_Tunnel_Design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 682);
            this.Controls.Add(this.lininG_DOC1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Tunnel_Design";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tunnel Lining Design";
            this.Load += new System.EventHandler(this.frm_Tunnel_Design_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tunnel_Lining.LINING_DOC lininG_DOC1;
    }
}