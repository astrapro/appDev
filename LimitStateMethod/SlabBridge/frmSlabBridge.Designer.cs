namespace LimitStateMethod.SlabBridge
{
    partial class frmSlabBridge
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSlabBridge));
            this.panel1 = new System.Windows.Forms.Panel();
            this.grb_Design = new System.Windows.Forms.GroupBox();
            this.btn_open_design = new System.Windows.Forms.Button();
            this.btn_process_design = new System.Windows.Forms.Button();
            this.grb_Analysis = new System.Windows.Forms.GroupBox();
            this.btn_process_data = new System.Windows.Forms.Button();
            this.btn_LL_report = new System.Windows.Forms.Button();
            this.btn_LL_input = new System.Windows.Forms.Button();
            this.btn_DL_report = new System.Windows.Forms.Button();
            this.btn_DL_input = new System.Windows.Forms.Button();
            this.btn_create_data = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_TGirder_new_design = new System.Windows.Forms.Button();
            this.btn_TGirder_browse = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label205 = new System.Windows.Forms.Label();
            this.dgv_design_data = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.uC_AbutmentWallType1 = new BridgeAnalysisDesign.Abutment.UC_AbutmentWallType();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_drawings = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.grb_Design.SuspendLayout();
            this.grb_Analysis.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_open_design);
            this.panel1.Controls.Add(this.btn_process_design);
            this.panel1.Controls.Add(this.grb_Design);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 479);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 48);
            this.panel1.TabIndex = 1;
            // 
            // grb_Design
            // 
            this.grb_Design.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grb_Design.Controls.Add(this.grb_Analysis);
            this.grb_Design.Location = new System.Drawing.Point(558, 6);
            this.grb_Design.Name = "grb_Design";
            this.grb_Design.Size = new System.Drawing.Size(99, 33);
            this.grb_Design.TabIndex = 2;
            this.grb_Design.TabStop = false;
            this.grb_Design.Text = "Design";
            this.grb_Design.Visible = false;
            // 
            // btn_open_design
            // 
            this.btn_open_design.Location = new System.Drawing.Point(361, 6);
            this.btn_open_design.Name = "btn_open_design";
            this.btn_open_design.Size = new System.Drawing.Size(118, 29);
            this.btn_open_design.TabIndex = 0;
            this.btn_open_design.Text = "Open Design";
            this.btn_open_design.UseVisualStyleBackColor = true;
            this.btn_open_design.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_process_design
            // 
            this.btn_process_design.Location = new System.Drawing.Point(219, 6);
            this.btn_process_design.Name = "btn_process_design";
            this.btn_process_design.Size = new System.Drawing.Size(118, 29);
            this.btn_process_design.TabIndex = 0;
            this.btn_process_design.Text = "Process Design";
            this.btn_process_design.UseVisualStyleBackColor = true;
            this.btn_process_design.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // grb_Analysis
            // 
            this.grb_Analysis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grb_Analysis.Controls.Add(this.btn_process_data);
            this.grb_Analysis.Controls.Add(this.btn_LL_report);
            this.grb_Analysis.Controls.Add(this.btn_LL_input);
            this.grb_Analysis.Controls.Add(this.btn_DL_report);
            this.grb_Analysis.Controls.Add(this.btn_DL_input);
            this.grb_Analysis.Controls.Add(this.btn_create_data);
            this.grb_Analysis.Location = new System.Drawing.Point(-44, 12);
            this.grb_Analysis.Name = "grb_Analysis";
            this.grb_Analysis.Size = new System.Drawing.Size(92, 40);
            this.grb_Analysis.TabIndex = 1;
            this.grb_Analysis.TabStop = false;
            this.grb_Analysis.Text = "Analysis";
            // 
            // btn_process_data
            // 
            this.btn_process_data.Location = new System.Drawing.Point(7, 58);
            this.btn_process_data.Name = "btn_process_data";
            this.btn_process_data.Size = new System.Drawing.Size(131, 29);
            this.btn_process_data.TabIndex = 0;
            this.btn_process_data.Text = "Process Data";
            this.btn_process_data.UseVisualStyleBackColor = true;
            this.btn_process_data.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_report
            // 
            this.btn_LL_report.Location = new System.Drawing.Point(283, 58);
            this.btn_LL_report.Name = "btn_LL_report";
            this.btn_LL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_report.TabIndex = 0;
            this.btn_LL_report.Text = "Open LL Report File";
            this.btn_LL_report.UseVisualStyleBackColor = true;
            this.btn_LL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_LL_input
            // 
            this.btn_LL_input.Location = new System.Drawing.Point(283, 17);
            this.btn_LL_input.Name = "btn_LL_input";
            this.btn_LL_input.Size = new System.Drawing.Size(133, 29);
            this.btn_LL_input.TabIndex = 0;
            this.btn_LL_input.Text = "Open LL Input File";
            this.btn_LL_input.UseVisualStyleBackColor = true;
            this.btn_LL_input.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_report
            // 
            this.btn_DL_report.Location = new System.Drawing.Point(144, 58);
            this.btn_DL_report.Name = "btn_DL_report";
            this.btn_DL_report.Size = new System.Drawing.Size(133, 29);
            this.btn_DL_report.TabIndex = 0;
            this.btn_DL_report.Text = "Open DL Report File";
            this.btn_DL_report.UseVisualStyleBackColor = true;
            this.btn_DL_report.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_DL_input
            // 
            this.btn_DL_input.Location = new System.Drawing.Point(144, 17);
            this.btn_DL_input.Name = "btn_DL_input";
            this.btn_DL_input.Size = new System.Drawing.Size(133, 29);
            this.btn_DL_input.TabIndex = 0;
            this.btn_DL_input.Text = "Open DL Input File";
            this.btn_DL_input.UseVisualStyleBackColor = true;
            this.btn_DL_input.Click += new System.EventHandler(this.btn_single_cell_Click);
            // 
            // btn_create_data
            // 
            this.btn_create_data.Location = new System.Drawing.Point(7, 17);
            this.btn_create_data.Name = "btn_create_data";
            this.btn_create_data.Size = new System.Drawing.Size(131, 29);
            this.btn_create_data.TabIndex = 0;
            this.btn_create_data.Text = "Create Data";
            this.btn_create_data.UseVisualStyleBackColor = true;
            this.btn_create_data.Click += new System.EventHandler(this.btn_single_cell_Click);
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
            this.panel4.Size = new System.Drawing.Size(699, 60);
            this.panel4.TabIndex = 179;
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
            // dgv_design_data
            // 
            this.dgv_design_data.AllowUserToAddRows = false;
            this.dgv_design_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_design_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgv_design_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_design_data.Location = new System.Drawing.Point(3, 63);
            this.dgv_design_data.Name = "dgv_design_data";
            this.dgv_design_data.RowHeadersWidth = 27;
            this.dgv_design_data.Size = new System.Drawing.Size(699, 416);
            this.dgv_design_data.TabIndex = 180;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Description";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 450;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Data";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 99;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(713, 556);
            this.tabControl1.TabIndex = 181;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_design_data);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(705, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RCC Slab Bridge";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.uC_AbutmentWallType1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(705, 530);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Wall Type Abutment";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // uC_AbutmentWallType1
            // 
            this.uC_AbutmentWallType1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_AbutmentWallType1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_AbutmentWallType1.Location = new System.Drawing.Point(3, 3);
            this.uC_AbutmentWallType1.Name = "uC_AbutmentWallType1";
            this.uC_AbutmentWallType1.Size = new System.Drawing.Size(699, 524);
            this.uC_AbutmentWallType1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btn_drawings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(705, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Drawings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 18);
            this.label1.TabIndex = 191;
            this.label1.Text = "Editable Construction Drawings";
            // 
            // btn_drawings
            // 
            this.btn_drawings.Location = new System.Drawing.Point(228, 125);
            this.btn_drawings.Name = "btn_drawings";
            this.btn_drawings.Size = new System.Drawing.Size(249, 41);
            this.btn_drawings.TabIndex = 190;
            this.btn_drawings.Text = "Open RCC Slab Bridge Drawings";
            this.btn_drawings.UseVisualStyleBackColor = true;
            this.btn_drawings.Click += new System.EventHandler(this.btn_drawings_Click);
            // 
            // frmSlabBridge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 556);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSlabBridge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RCC Slab Bridge (IRC)";
            this.Load += new System.EventHandler(this.frmSlabBridge_Load);
            this.panel1.ResumeLayout(false);
            this.grb_Design.ResumeLayout(false);
            this.grb_Analysis.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_design_data)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grb_Design;
        private System.Windows.Forms.Button btn_open_design;
        private System.Windows.Forms.Button btn_process_design;
        private System.Windows.Forms.GroupBox grb_Analysis;
        private System.Windows.Forms.Button btn_process_data;
        private System.Windows.Forms.Button btn_LL_report;
        private System.Windows.Forms.Button btn_LL_input;
        private System.Windows.Forms.Button btn_DL_report;
        private System.Windows.Forms.Button btn_DL_input;
        private System.Windows.Forms.Button btn_create_data;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_TGirder_new_design;
        private System.Windows.Forms.Button btn_TGirder_browse;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label205;
        private System.Windows.Forms.DataGridView dgv_design_data;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_drawings;
        private System.Windows.Forms.TabPage tabPage3;
        private BridgeAnalysisDesign.Abutment.UC_AbutmentWallType uC_AbutmentWallType1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}