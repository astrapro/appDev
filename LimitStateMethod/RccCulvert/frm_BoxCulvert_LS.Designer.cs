namespace LimitStateMethod.RccCulvert
{
    partial class frm_BoxCulvert_LS
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
            this.uC_BoxCulvert1 = new BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert();
            this.SuspendLayout();
            // 
            // uC_BoxCulvert1
            // 
            this.uC_BoxCulvert1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxCulvert1.Location = new System.Drawing.Point(29, 33);
            this.uC_BoxCulvert1.Name = "uC_BoxCulvert1";
            this.uC_BoxCulvert1.Size = new System.Drawing.Size(595, 424);
            this.uC_BoxCulvert1.TabIndex = 0;
            // 
            // frm_BoxCulvert_LS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 557);
            this.Controls.Add(this.uC_BoxCulvert1);
            this.Name = "frm_BoxCulvert_LS";
            this.Text = "frm_BoxCulvert_LS";
            this.ResumeLayout(false);

        }

        #endregion

        private BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert uC_BoxCulvert1;
    }
}