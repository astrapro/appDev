namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    partial class frm_PSC_Box_Worksheet
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
            iApp.Delete_Temporary_Files();
            iApp.LastDesignWorkingFolder = System.IO.Path.GetDirectoryName(iApp.LastDesignWorkingFolder);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PSC_Box_Worksheet));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Design_Sequence = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tab_work_sheet_design = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_design_of_anchorage = new System.Windows.Forms.Button();
            this.btn_design_blister = new System.Windows.Forms.Button();
            this.btn_design_end_anchorage = new System.Windows.Forms.Button();
            this.btn_design_cross_diaphragms = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Temp_trans = new System.Windows.Forms.Button();
            this.btn_input_data = new System.Windows.Forms.Button();
            this.btn_design = new System.Windows.Forms.Button();
            this.btn_class_A_2_L = new System.Windows.Forms.Button();
            this.btn_class_A_1_L = new System.Windows.Forms.Button();
            this.btn_70RT = new System.Windows.Forms.Button();
            this.btn_70RM = new System.Windows.Forms.Button();
            this.btn_70RL = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_cable_frict = new System.Windows.Forms.Button();
            this.btn_psc_box = new System.Windows.Forms.Button();
            this.btn_sum_LL = new System.Windows.Forms.Button();
            this.tab_drawings = new System.Windows.Forms.TabPage();
            this.btn_open_drawings = new System.Windows.Forms.Button();
            this.btn_worksheet_open = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_Design_Sequence.SuspendLayout();
            this.tab_work_sheet_design.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tab_drawings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Design_Sequence);
            this.tabControl1.Controls.Add(this.tab_work_sheet_design);
            this.tabControl1.Controls.Add(this.tab_drawings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 498);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_Design_Sequence
            // 
            this.tab_Design_Sequence.Controls.Add(this.richTextBox1);
            this.tab_Design_Sequence.Location = new System.Drawing.Point(4, 22);
            this.tab_Design_Sequence.Name = "tab_Design_Sequence";
            this.tab_Design_Sequence.Size = new System.Drawing.Size(639, 472);
            this.tab_Design_Sequence.TabIndex = 2;
            this.tab_Design_Sequence.Text = "Design Sequence";
            this.tab_Design_Sequence.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(101, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(453, 401);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tab_work_sheet_design
            // 
            this.tab_work_sheet_design.Controls.Add(this.btn_worksheet_open);
            this.tab_work_sheet_design.Controls.Add(this.groupBox3);
            this.tab_work_sheet_design.Controls.Add(this.groupBox2);
            this.tab_work_sheet_design.Controls.Add(this.groupBox1);
            this.tab_work_sheet_design.Location = new System.Drawing.Point(4, 22);
            this.tab_work_sheet_design.Name = "tab_work_sheet_design";
            this.tab_work_sheet_design.Padding = new System.Windows.Forms.Padding(3);
            this.tab_work_sheet_design.Size = new System.Drawing.Size(639, 472);
            this.tab_work_sheet_design.TabIndex = 0;
            this.tab_work_sheet_design.Text = "Worksheet Design";
            this.tab_work_sheet_design.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_design_of_anchorage);
            this.groupBox3.Controls.Add(this.btn_design_blister);
            this.groupBox3.Controls.Add(this.btn_design_end_anchorage);
            this.groupBox3.Controls.Add(this.btn_design_cross_diaphragms);
            this.groupBox3.Location = new System.Drawing.Point(19, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 193);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Diaphragm Anchorage Blister Block Future Prestressing";
            // 
            // btn_design_of_anchorage
            // 
            this.btn_design_of_anchorage.Location = new System.Drawing.Point(6, 148);
            this.btn_design_of_anchorage.Name = "btn_design_of_anchorage";
            this.btn_design_of_anchorage.Size = new System.Drawing.Size(318, 35);
            this.btn_design_of_anchorage.TabIndex = 3;
            this.btn_design_of_anchorage.Text = "Design of Anchorage for Future Prestressing Block";
            this.btn_design_of_anchorage.UseVisualStyleBackColor = true;
            this.btn_design_of_anchorage.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // btn_design_blister
            // 
            this.btn_design_blister.Location = new System.Drawing.Point(6, 107);
            this.btn_design_blister.Name = "btn_design_blister";
            this.btn_design_blister.Size = new System.Drawing.Size(318, 35);
            this.btn_design_blister.TabIndex = 2;
            this.btn_design_blister.Text = "Design of Blister Blocks";
            this.btn_design_blister.UseVisualStyleBackColor = true;
            this.btn_design_blister.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // btn_design_end_anchorage
            // 
            this.btn_design_end_anchorage.Location = new System.Drawing.Point(6, 66);
            this.btn_design_end_anchorage.Name = "btn_design_end_anchorage";
            this.btn_design_end_anchorage.Size = new System.Drawing.Size(318, 35);
            this.btn_design_end_anchorage.TabIndex = 1;
            this.btn_design_end_anchorage.Text = "Design of End Anchorage";
            this.btn_design_end_anchorage.UseVisualStyleBackColor = true;
            this.btn_design_end_anchorage.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // btn_design_cross_diaphragms
            // 
            this.btn_design_cross_diaphragms.Location = new System.Drawing.Point(6, 20);
            this.btn_design_cross_diaphragms.Name = "btn_design_cross_diaphragms";
            this.btn_design_cross_diaphragms.Size = new System.Drawing.Size(318, 35);
            this.btn_design_cross_diaphragms.TabIndex = 0;
            this.btn_design_cross_diaphragms.Text = "Design of Cross Diaphragms";
            this.btn_design_cross_diaphragms.UseVisualStyleBackColor = true;
            this.btn_design_cross_diaphragms.Click += new System.EventHandler(this.btn_design_of_anchorage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Temp_trans);
            this.groupBox2.Controls.Add(this.btn_input_data);
            this.groupBox2.Controls.Add(this.btn_design);
            this.groupBox2.Controls.Add(this.btn_class_A_2_L);
            this.groupBox2.Controls.Add(this.btn_class_A_1_L);
            this.groupBox2.Controls.Add(this.btn_70RT);
            this.groupBox2.Controls.Add(this.btn_70RM);
            this.groupBox2.Controls.Add(this.btn_70RL);
            this.groupBox2.Location = new System.Drawing.Point(398, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 352);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transverse Forces";
            // 
            // btn_Temp_trans
            // 
            this.btn_Temp_trans.Location = new System.Drawing.Point(16, 309);
            this.btn_Temp_trans.Name = "btn_Temp_trans";
            this.btn_Temp_trans.Size = new System.Drawing.Size(164, 37);
            this.btn_Temp_trans.TabIndex = 7;
            this.btn_Temp_trans.Text = "Temp-trans";
            this.btn_Temp_trans.UseVisualStyleBackColor = true;
            this.btn_Temp_trans.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_input_data
            // 
            this.btn_input_data.Location = new System.Drawing.Point(16, 223);
            this.btn_input_data.Name = "btn_input_data";
            this.btn_input_data.Size = new System.Drawing.Size(164, 37);
            this.btn_input_data.TabIndex = 6;
            this.btn_input_data.Text = "Input data";
            this.btn_input_data.UseVisualStyleBackColor = true;
            this.btn_input_data.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_design
            // 
            this.btn_design.Location = new System.Drawing.Point(16, 266);
            this.btn_design.Name = "btn_design";
            this.btn_design.Size = new System.Drawing.Size(164, 37);
            this.btn_design.TabIndex = 5;
            this.btn_design.Text = "Design";
            this.btn_design.UseVisualStyleBackColor = true;
            this.btn_design.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_class_A_2_L
            // 
            this.btn_class_A_2_L.Location = new System.Drawing.Point(16, 179);
            this.btn_class_A_2_L.Name = "btn_class_A_2_L";
            this.btn_class_A_2_L.Size = new System.Drawing.Size(164, 37);
            this.btn_class_A_2_L.TabIndex = 4;
            this.btn_class_A_2_L.Text = "CLASS-A-2-L";
            this.btn_class_A_2_L.UseVisualStyleBackColor = true;
            this.btn_class_A_2_L.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_class_A_1_L
            // 
            this.btn_class_A_1_L.Location = new System.Drawing.Point(16, 140);
            this.btn_class_A_1_L.Name = "btn_class_A_1_L";
            this.btn_class_A_1_L.Size = new System.Drawing.Size(164, 37);
            this.btn_class_A_1_L.TabIndex = 3;
            this.btn_class_A_1_L.Text = "CLASS-A-1-L";
            this.btn_class_A_1_L.UseVisualStyleBackColor = true;
            this.btn_class_A_1_L.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_70RT
            // 
            this.btn_70RT.Location = new System.Drawing.Point(16, 92);
            this.btn_70RT.Name = "btn_70RT";
            this.btn_70RT.Size = new System.Drawing.Size(164, 37);
            this.btn_70RT.TabIndex = 2;
            this.btn_70RT.Text = "70RT";
            this.btn_70RT.UseVisualStyleBackColor = true;
            this.btn_70RT.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_70RM
            // 
            this.btn_70RM.Location = new System.Drawing.Point(16, 54);
            this.btn_70RM.Name = "btn_70RM";
            this.btn_70RM.Size = new System.Drawing.Size(164, 37);
            this.btn_70RM.TabIndex = 1;
            this.btn_70RM.Text = "70RM";
            this.btn_70RM.UseVisualStyleBackColor = true;
            this.btn_70RM.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_70RL
            // 
            this.btn_70RL.Location = new System.Drawing.Point(16, 17);
            this.btn_70RL.Name = "btn_70RL";
            this.btn_70RL.Size = new System.Drawing.Size(164, 34);
            this.btn_70RL.TabIndex = 0;
            this.btn_70RL.Text = "70RL";
            this.btn_70RL.UseVisualStyleBackColor = true;
            this.btn_70RL.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_cable_frict);
            this.groupBox1.Controls.Add(this.btn_psc_box);
            this.groupBox1.Controls.Add(this.btn_sum_LL);
            this.groupBox1.Location = new System.Drawing.Point(19, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 146);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Longitudinal Forces";
            // 
            // btn_cable_frict
            // 
            this.btn_cable_frict.Location = new System.Drawing.Point(6, 97);
            this.btn_cable_frict.Name = "btn_cable_frict";
            this.btn_cable_frict.Size = new System.Drawing.Size(318, 32);
            this.btn_cable_frict.TabIndex = 2;
            this.btn_cable_frict.Text = "Cable Friction";
            this.btn_cable_frict.UseVisualStyleBackColor = true;
            this.btn_cable_frict.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_psc_box
            // 
            this.btn_psc_box.Location = new System.Drawing.Point(6, 59);
            this.btn_psc_box.Name = "btn_psc_box";
            this.btn_psc_box.Size = new System.Drawing.Size(318, 32);
            this.btn_psc_box.TabIndex = 1;
            this.btn_psc_box.Text = "PSC BOX";
            this.btn_psc_box.UseVisualStyleBackColor = true;
            this.btn_psc_box.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // btn_sum_LL
            // 
            this.btn_sum_LL.Location = new System.Drawing.Point(6, 19);
            this.btn_sum_LL.Name = "btn_sum_LL";
            this.btn_sum_LL.Size = new System.Drawing.Size(318, 32);
            this.btn_sum_LL.TabIndex = 0;
            this.btn_sum_LL.Text = "Summary of Live Load";
            this.btn_sum_LL.UseVisualStyleBackColor = true;
            this.btn_sum_LL.Click += new System.EventHandler(this.btn_Open_Worksheet_Design_Click);
            // 
            // tab_drawings
            // 
            this.tab_drawings.Controls.Add(this.btn_open_drawings);
            this.tab_drawings.Location = new System.Drawing.Point(4, 22);
            this.tab_drawings.Name = "tab_drawings";
            this.tab_drawings.Padding = new System.Windows.Forms.Padding(3);
            this.tab_drawings.Size = new System.Drawing.Size(639, 472);
            this.tab_drawings.TabIndex = 1;
            this.tab_drawings.Text = "Drawings";
            this.tab_drawings.UseVisualStyleBackColor = true;
            // 
            // btn_open_drawings
            // 
            this.btn_open_drawings.Location = new System.Drawing.Point(163, 216);
            this.btn_open_drawings.Name = "btn_open_drawings";
            this.btn_open_drawings.Size = new System.Drawing.Size(319, 49);
            this.btn_open_drawings.TabIndex = 0;
            this.btn_open_drawings.Text = "Prestressed Box Girder Drawings";
            this.btn_open_drawings.UseVisualStyleBackColor = true;
            this.btn_open_drawings.Click += new System.EventHandler(this.btn_open_drawings_Click);
            // 
            // btn_worksheet_open
            // 
            this.btn_worksheet_open.Location = new System.Drawing.Point(179, 400);
            this.btn_worksheet_open.Name = "btn_worksheet_open";
            this.btn_worksheet_open.Size = new System.Drawing.Size(318, 39);
            this.btn_worksheet_open.TabIndex = 4;
            this.btn_worksheet_open.Text = "Open User's saved Worksheet Design";
            this.btn_worksheet_open.UseVisualStyleBackColor = true;
            this.btn_worksheet_open.Click += new System.EventHandler(this.btn_worksheet_open_Click);
            // 
            // frm_PSC_BoxGirder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 498);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_PSC_BoxGirder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_PSC_BoxGirder";
            this.tabControl1.ResumeLayout(false);
            this.tab_Design_Sequence.ResumeLayout(false);
            this.tab_work_sheet_design.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tab_drawings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_work_sheet_design;
        private System.Windows.Forms.TabPage tab_drawings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_70RT;
        private System.Windows.Forms.Button btn_70RM;
        private System.Windows.Forms.Button btn_70RL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cable_frict;
        private System.Windows.Forms.Button btn_psc_box;
        private System.Windows.Forms.Button btn_sum_LL;
        private System.Windows.Forms.Button btn_design;
        private System.Windows.Forms.Button btn_class_A_2_L;
        private System.Windows.Forms.Button btn_class_A_1_L;
        private System.Windows.Forms.Button btn_Temp_trans;
        private System.Windows.Forms.Button btn_input_data;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_design_blister;
        private System.Windows.Forms.Button btn_design_end_anchorage;
        private System.Windows.Forms.Button btn_design_cross_diaphragms;
        private System.Windows.Forms.Button btn_design_of_anchorage;
        private System.Windows.Forms.TabPage tab_Design_Sequence;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_open_drawings;
        private System.Windows.Forms.Button btn_worksheet_open;
    }
}