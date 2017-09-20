namespace LimitStateMethod.PierDesign
{
    partial class frm_RCC_Pier_Limit_State
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RCC_Pier_Limit_State));
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.btn_browse_design = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label232 = new System.Windows.Forms.Label();
            this.uC_PierDesignLimitState1 = new BridgeAnalysisDesign.Pier.UC_PierDesignLimitState();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_new_design);
            this.panel5.Controls.Add(this.btn_browse_design);
            this.panel5.Controls.Add(this.txt_project_name);
            this.panel5.Controls.Add(this.label232);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(838, 65);
            this.panel5.TabIndex = 181;
            // 
            // btn_new_design
            // 
            this.btn_new_design.Location = new System.Drawing.Point(121, 11);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(141, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "New Design";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // btn_browse_design
            // 
            this.btn_browse_design.Location = new System.Drawing.Point(282, 11);
            this.btn_browse_design.Name = "btn_browse_design";
            this.btn_browse_design.Size = new System.Drawing.Size(141, 24);
            this.btn_browse_design.TabIndex = 189;
            this.btn_browse_design.Text = "Open Design";
            this.btn_browse_design.UseVisualStyleBackColor = true;
            this.btn_browse_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(121, 37);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(300, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.Location = new System.Drawing.Point(6, 41);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(93, 13);
            this.label232.TabIndex = 187;
            this.label232.Text = "Project Name :";
            // 
            // uC_PierDesignLimitState1
            // 
            this.uC_PierDesignLimitState1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_PierDesignLimitState1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_PierDesignLimitState1.Left_Span_Force = "2600.0";
            this.uC_PierDesignLimitState1.Location = new System.Drawing.Point(0, 65);
            this.uC_PierDesignLimitState1.Name = "uC_PierDesignLimitState1";
            this.uC_PierDesignLimitState1.Right_Span_Force = "1300.0";
            this.uC_PierDesignLimitState1.Show_Note = false;
            this.uC_PierDesignLimitState1.Show_Title = false;
            this.uC_PierDesignLimitState1.Size = new System.Drawing.Size(838, 390);
            this.uC_PierDesignLimitState1.TabIndex = 182;
            this.uC_PierDesignLimitState1.OnProcess += new System.EventHandler(this.uC_PierDesignLimitState1_OnProcess);
            // 
            // frm_RCC_Pier_Limit_State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 455);
            this.Controls.Add(this.uC_PierDesignLimitState1);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_RCC_Pier_Limit_State";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design of RCC Pier in Limit State Method";
            this.Load += new System.EventHandler(this.frm_RCC_Pier_Limit_State_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_browse_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label232;
        private BridgeAnalysisDesign.Pier.UC_PierDesignLimitState uC_PierDesignLimitState1;
    }
}