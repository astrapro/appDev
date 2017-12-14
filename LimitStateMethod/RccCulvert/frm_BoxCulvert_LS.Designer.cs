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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.rbtn_dwg_multi_without_earth_cusion = new System.Windows.Forms.RadioButton();
            this.rbtn_dwg_multi_with_earth_cusion = new System.Windows.Forms.RadioButton();
            this.btn_dwg_box_culvert_multicell = new System.Windows.Forms.Button();
            this.uC_BoxCulvert1 = new BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_TGirder_new_design);
            this.panel4.Controls.Add(this.btn_TGirder_browse);
            this.panel4.Controls.Add(this.txt_project_name);
            this.panel4.Controls.Add(this.label205);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(684, 63);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 597);
            this.tabControl1.TabIndex = 179;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uC_BoxCulvert1);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(690, 571);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RCC Box Culvert";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.groupBox19);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(690, 571);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Drawings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 18);
            this.label1.TabIndex = 192;
            this.label1.Text = "Editable Construction Drawings";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.rbtn_dwg_multi_without_earth_cusion);
            this.groupBox19.Controls.Add(this.rbtn_dwg_multi_with_earth_cusion);
            this.groupBox19.Controls.Add(this.btn_dwg_box_culvert_multicell);
            this.groupBox19.Location = new System.Drawing.Point(192, 142);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(307, 112);
            this.groupBox19.TabIndex = 82;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Single / Multi cell Box Culvert Design Drawing";
            // 
            // rbtn_dwg_multi_without_earth_cusion
            // 
            this.rbtn_dwg_multi_without_earth_cusion.AutoSize = true;
            this.rbtn_dwg_multi_without_earth_cusion.Location = new System.Drawing.Point(6, 43);
            this.rbtn_dwg_multi_without_earth_cusion.Name = "rbtn_dwg_multi_without_earth_cusion";
            this.rbtn_dwg_multi_without_earth_cusion.Size = new System.Drawing.Size(268, 17);
            this.rbtn_dwg_multi_without_earth_cusion.TabIndex = 5;
            this.rbtn_dwg_multi_without_earth_cusion.Text = "Multicell Box Culvert without Earth Cusion ";
            this.rbtn_dwg_multi_without_earth_cusion.UseVisualStyleBackColor = true;
            // 
            // rbtn_dwg_multi_with_earth_cusion
            // 
            this.rbtn_dwg_multi_with_earth_cusion.AutoSize = true;
            this.rbtn_dwg_multi_with_earth_cusion.Checked = true;
            this.rbtn_dwg_multi_with_earth_cusion.Location = new System.Drawing.Point(6, 20);
            this.rbtn_dwg_multi_with_earth_cusion.Name = "rbtn_dwg_multi_with_earth_cusion";
            this.rbtn_dwg_multi_with_earth_cusion.Size = new System.Drawing.Size(250, 17);
            this.rbtn_dwg_multi_with_earth_cusion.TabIndex = 5;
            this.rbtn_dwg_multi_with_earth_cusion.TabStop = true;
            this.rbtn_dwg_multi_with_earth_cusion.Text = "Multicell Box Culvert with Earth Cusion ";
            this.rbtn_dwg_multi_with_earth_cusion.UseVisualStyleBackColor = true;
            // 
            // btn_dwg_box_culvert_multicell
            // 
            this.btn_dwg_box_culvert_multicell.Location = new System.Drawing.Point(6, 68);
            this.btn_dwg_box_culvert_multicell.Name = "btn_dwg_box_culvert_multicell";
            this.btn_dwg_box_culvert_multicell.Size = new System.Drawing.Size(290, 38);
            this.btn_dwg_box_culvert_multicell.TabIndex = 4;
            this.btn_dwg_box_culvert_multicell.Text = "Open Design Drawings\r\n";
            this.btn_dwg_box_culvert_multicell.UseVisualStyleBackColor = true;
            this.btn_dwg_box_culvert_multicell.Click += new System.EventHandler(this.btn_dwg_box_culvert_multicell_Click);
            // 
            // uC_BoxCulvert1
            // 
            this.uC_BoxCulvert1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BoxCulvert1.Enabled = false;
            this.uC_BoxCulvert1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_BoxCulvert1.Is_Multi_Cell = false;
            this.uC_BoxCulvert1.Location = new System.Drawing.Point(3, 66);
            this.uC_BoxCulvert1.Name = "uC_BoxCulvert1";
            this.uC_BoxCulvert1.Size = new System.Drawing.Size(684, 502);
            this.uC_BoxCulvert1.TabIndex = 0;
            this.uC_BoxCulvert1.OnButtonProceed += new System.EventHandler(this.uC_BoxCulvert1_OnButtonProceed);
            this.uC_BoxCulvert1.Load += new System.EventHandler(this.uC_BoxCulvert1_Load);
            // 
            // frm_BoxCulvert_LS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 597);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_BoxCulvert_LS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box Culvert Design in Limit State Method (IRC)";
            this.Load += new System.EventHandler(this.frm_BoxCulvert_LS_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BridgeAnalysisDesign.RCC_Culvert.UC_BoxCulvert uC_BoxCulvert1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_TGirder_new_design;
        private System.Windows.Forms.Button btn_TGirder_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label205;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.RadioButton rbtn_dwg_multi_without_earth_cusion;
        private System.Windows.Forms.RadioButton rbtn_dwg_multi_with_earth_cusion;
        private System.Windows.Forms.Button btn_dwg_box_culvert_multicell;
        private System.Windows.Forms.Label label1;
    }
}