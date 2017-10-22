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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BoxCulvert_LS));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_TGirder_new_design = new System.Windows.Forms.Button();
            this.btn_TGirder_browse = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label205 = new System.Windows.Forms.Label();
            this.uC_BoxCulvert1 = new BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_TGirder_new_design);
            this.panel4.Controls.Add(this.btn_TGirder_browse);
            this.panel4.Controls.Add(this.txt_project_name);
            this.panel4.Controls.Add(this.label205);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(698, 63);
            this.panel4.TabIndex = 178;
            // 
            // btn_TGirder_new_design
            // 
            this.btn_TGirder_new_design.Location = new System.Drawing.Point(121, 7);
            this.btn_TGirder_new_design.Name = "btn_TGirder_new_design";
            this.btn_TGirder_new_design.Size = new System.Drawing.Size(141, 24);
            this.btn_TGirder_new_design.TabIndex = 188;
            this.btn_TGirder_new_design.Text = "New Design";
            this.btn_TGirder_new_design.UseVisualStyleBackColor = true;
            this.btn_TGirder_new_design.Click += new System.EventHandler(this.btn_TGirder_new_design_Click);
            // 
            // btn_TGirder_browse
            // 
            this.btn_TGirder_browse.Location = new System.Drawing.Point(282, 7);
            this.btn_TGirder_browse.Name = "btn_TGirder_browse";
            this.btn_TGirder_browse.Size = new System.Drawing.Size(141, 24);
            this.btn_TGirder_browse.TabIndex = 189;
            this.btn_TGirder_browse.Text = "Open Design";
            this.btn_TGirder_browse.UseVisualStyleBackColor = true;
            this.btn_TGirder_browse.Click += new System.EventHandler(this.btn_TGirder_new_design_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(121, 33);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(302, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Location = new System.Drawing.Point(6, 37);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(93, 13);
            this.label205.TabIndex = 187;
            this.label205.Text = "Project Name :";
            // 
            // uC_BoxCulvert1
            // 
            this.uC_BoxCulvert1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxCulvert1.Enabled = false;
            this.uC_BoxCulvert1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxCulvert1.Is_Multi_Cell = false;
            this.uC_BoxCulvert1.Location = new System.Drawing.Point(0, 63);
            this.uC_BoxCulvert1.Name = "uC_BoxCulvert1";
            this.uC_BoxCulvert1.Size = new System.Drawing.Size(698, 534);
            this.uC_BoxCulvert1.TabIndex = 0;
            this.uC_BoxCulvert1.Load += new System.EventHandler(this.uC_BoxCulvert1_Load);
            // 
            // frm_BoxCulvert_LS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 597);
            this.Controls.Add(this.uC_BoxCulvert1);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_BoxCulvert_LS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box Culvert Design in Limit State Method";
            this.Load += new System.EventHandler(this.frm_BoxCulvert_LS_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert uC_BoxCulvert1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_TGirder_new_design;
        private System.Windows.Forms.Button btn_TGirder_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label205;
    }
}