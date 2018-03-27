namespace ASTRAStructures
{
    partial class frmSteelBeamDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSteelBeamDesign));
            this.uC_SteelSections_Beam = new ASTRAStructures.UC_SteelSections();
            this.btn_steel_beam_open_design = new System.Windows.Forms.Button();
            this.btn_steel_beam_design = new System.Windows.Forms.Button();
            this.txt_steel_beam_Pbs = new System.Windows.Forms.TextBox();
            this.label422 = new System.Windows.Forms.Label();
            this.txt_steel_beam_Pss = new System.Windows.Forms.TextBox();
            this.label421 = new System.Windows.Forms.Label();
            this.txt_steel_beam_Pms = new System.Windows.Forms.TextBox();
            this.label420 = new System.Windows.Forms.Label();
            this.txt_steel_beam_V = new System.Windows.Forms.TextBox();
            this.label419 = new System.Windows.Forms.Label();
            this.txt_steel_beam_M = new System.Windows.Forms.TextBox();
            this.label425 = new System.Windows.Forms.Label();
            this.label424 = new System.Windows.Forms.Label();
            this.label429 = new System.Windows.Forms.Label();
            this.label428 = new System.Windows.Forms.Label();
            this.label427 = new System.Windows.Forms.Label();
            this.label426 = new System.Windows.Forms.Label();
            this.label423 = new System.Windows.Forms.Label();
            this.label418 = new System.Windows.Forms.Label();
            this.txt_steel_beam_a = new System.Windows.Forms.TextBox();
            this.label417 = new System.Windows.Forms.Label();
            this.txt_steel_beam_l = new System.Windows.Forms.TextBox();
            this.label416 = new System.Windows.Forms.Label();
            this.panel33 = new System.Windows.Forms.Panel();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.btn_browse_design = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label830 = new System.Windows.Forms.Label();
            this.panel33.SuspendLayout();
            this.SuspendLayout();
            // 
            // uC_SteelSections_Beam
            // 
            this.uC_SteelSections_Beam.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_SteelSections_Beam.Location = new System.Drawing.Point(384, 12);
            this.uC_SteelSections_Beam.Name = "uC_SteelSections_Beam";
            this.uC_SteelSections_Beam.Size = new System.Drawing.Size(534, 341);
            this.uC_SteelSections_Beam.TabIndex = 71;
            // 
            // btn_steel_beam_open_design
            // 
            this.btn_steel_beam_open_design.Location = new System.Drawing.Point(193, 319);
            this.btn_steel_beam_open_design.Name = "btn_steel_beam_open_design";
            this.btn_steel_beam_open_design.Size = new System.Drawing.Size(132, 34);
            this.btn_steel_beam_open_design.TabIndex = 94;
            this.btn_steel_beam_open_design.Text = "Open Design Report";
            this.btn_steel_beam_open_design.UseVisualStyleBackColor = true;
            this.btn_steel_beam_open_design.Click += new System.EventHandler(this.btn_steel_beam_open_design_Click);
            // 
            // btn_steel_beam_design
            // 
            this.btn_steel_beam_design.Location = new System.Drawing.Point(48, 319);
            this.btn_steel_beam_design.Name = "btn_steel_beam_design";
            this.btn_steel_beam_design.Size = new System.Drawing.Size(132, 34);
            this.btn_steel_beam_design.TabIndex = 93;
            this.btn_steel_beam_design.Text = "Proceed";
            this.btn_steel_beam_design.UseVisualStyleBackColor = true;
            this.btn_steel_beam_design.Click += new System.EventHandler(this.btn_steel_beam_design_Click);
            // 
            // txt_steel_beam_Pbs
            // 
            this.txt_steel_beam_Pbs.Location = new System.Drawing.Point(239, 193);
            this.txt_steel_beam_Pbs.Name = "txt_steel_beam_Pbs";
            this.txt_steel_beam_Pbs.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_Pbs.TabIndex = 5;
            this.txt_steel_beam_Pbs.Text = "187.5";
            this.txt_steel_beam_Pbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label422
            // 
            this.label422.AutoSize = true;
            this.label422.Location = new System.Drawing.Point(23, 199);
            this.label422.Name = "label422";
            this.label422.Size = new System.Drawing.Size(210, 13);
            this.label422.TabIndex = 83;
            this.label422.Text = "Permissible Bearing Stress = Pbs =";
            // 
            // txt_steel_beam_Pss
            // 
            this.txt_steel_beam_Pss.Location = new System.Drawing.Point(239, 168);
            this.txt_steel_beam_Pss.Name = "txt_steel_beam_Pss";
            this.txt_steel_beam_Pss.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_Pss.TabIndex = 4;
            this.txt_steel_beam_Pss.Text = "100";
            this.txt_steel_beam_Pss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label421
            // 
            this.label421.AutoSize = true;
            this.label421.Location = new System.Drawing.Point(23, 174);
            this.label421.Name = "label421";
            this.label421.Size = new System.Drawing.Size(199, 13);
            this.label421.TabIndex = 82;
            this.label421.Text = "Permissible Shear Stress = Pss =";
            // 
            // txt_steel_beam_Pms
            // 
            this.txt_steel_beam_Pms.Location = new System.Drawing.Point(239, 144);
            this.txt_steel_beam_Pms.Name = "txt_steel_beam_Pms";
            this.txt_steel_beam_Pms.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_Pms.TabIndex = 3;
            this.txt_steel_beam_Pms.Text = "165";
            this.txt_steel_beam_Pms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label420
            // 
            this.label420.AutoSize = true;
            this.label420.Location = new System.Drawing.Point(23, 150);
            this.label420.Name = "label420";
            this.label420.Size = new System.Drawing.Size(216, 13);
            this.label420.TabIndex = 84;
            this.label420.Text = "Permissible Bending Stress = Pms =";
            // 
            // txt_steel_beam_V
            // 
            this.txt_steel_beam_V.ForeColor = System.Drawing.Color.Red;
            this.txt_steel_beam_V.Location = new System.Drawing.Point(239, 245);
            this.txt_steel_beam_V.Name = "txt_steel_beam_V";
            this.txt_steel_beam_V.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_V.TabIndex = 7;
            this.txt_steel_beam_V.Text = "70.2";
            this.txt_steel_beam_V.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label419
            // 
            this.label419.AutoSize = true;
            this.label419.Location = new System.Drawing.Point(23, 251);
            this.label419.Name = "label419";
            this.label419.Size = new System.Drawing.Size(173, 13);
            this.label419.TabIndex = 81;
            this.label419.Text = "Maximum Shear Force = V =";
            // 
            // txt_steel_beam_M
            // 
            this.txt_steel_beam_M.ForeColor = System.Drawing.Color.Red;
            this.txt_steel_beam_M.Location = new System.Drawing.Point(239, 220);
            this.txt_steel_beam_M.Name = "txt_steel_beam_M";
            this.txt_steel_beam_M.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_M.TabIndex = 6;
            this.txt_steel_beam_M.Text = "151.13";
            this.txt_steel_beam_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label425
            // 
            this.label425.AutoSize = true;
            this.label425.Location = new System.Drawing.Point(307, 94);
            this.label425.Name = "label425";
            this.label425.Size = new System.Drawing.Size(18, 13);
            this.label425.TabIndex = 80;
            this.label425.Text = "m";
            // 
            // label424
            // 
            this.label424.AutoSize = true;
            this.label424.Location = new System.Drawing.Point(309, 120);
            this.label424.Name = "label424";
            this.label424.Size = new System.Drawing.Size(29, 13);
            this.label424.TabIndex = 79;
            this.label424.Text = "mm";
            // 
            // label429
            // 
            this.label429.AutoSize = true;
            this.label429.Location = new System.Drawing.Point(309, 196);
            this.label429.Name = "label429";
            this.label429.Size = new System.Drawing.Size(61, 13);
            this.label429.TabIndex = 78;
            this.label429.Text = "N/Sq.mm";
            // 
            // label428
            // 
            this.label428.AutoSize = true;
            this.label428.Location = new System.Drawing.Point(309, 171);
            this.label428.Name = "label428";
            this.label428.Size = new System.Drawing.Size(61, 13);
            this.label428.TabIndex = 77;
            this.label428.Text = "N/Sq.mm";
            // 
            // label427
            // 
            this.label427.AutoSize = true;
            this.label427.Location = new System.Drawing.Point(309, 147);
            this.label427.Name = "label427";
            this.label427.Size = new System.Drawing.Size(61, 13);
            this.label427.TabIndex = 76;
            this.label427.Text = "N/Sq.mm";
            // 
            // label426
            // 
            this.label426.AutoSize = true;
            this.label426.Location = new System.Drawing.Point(309, 248);
            this.label426.Name = "label426";
            this.label426.Size = new System.Drawing.Size(22, 13);
            this.label426.TabIndex = 75;
            this.label426.Text = "kN";
            // 
            // label423
            // 
            this.label423.AutoSize = true;
            this.label423.Location = new System.Drawing.Point(309, 223);
            this.label423.Name = "label423";
            this.label423.Size = new System.Drawing.Size(38, 13);
            this.label423.TabIndex = 74;
            this.label423.Text = "kN-m";
            // 
            // label418
            // 
            this.label418.AutoSize = true;
            this.label418.Location = new System.Drawing.Point(23, 226);
            this.label418.Name = "label418";
            this.label418.Size = new System.Drawing.Size(204, 13);
            this.label418.TabIndex = 73;
            this.label418.Text = "Maximum Bending Moment  = M =";
            // 
            // txt_steel_beam_a
            // 
            this.txt_steel_beam_a.Location = new System.Drawing.Point(239, 117);
            this.txt_steel_beam_a.Name = "txt_steel_beam_a";
            this.txt_steel_beam_a.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_a.TabIndex = 2;
            this.txt_steel_beam_a.Text = "300";
            this.txt_steel_beam_a.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label417
            // 
            this.label417.AutoSize = true;
            this.label417.Location = new System.Drawing.Point(23, 123);
            this.label417.Name = "label417";
            this.label417.Size = new System.Drawing.Size(159, 13);
            this.label417.TabIndex = 72;
            this.label417.Text = "Bearing at each end = a =";
            // 
            // txt_steel_beam_l
            // 
            this.txt_steel_beam_l.Location = new System.Drawing.Point(239, 91);
            this.txt_steel_beam_l.Name = "txt_steel_beam_l";
            this.txt_steel_beam_l.Size = new System.Drawing.Size(62, 21);
            this.txt_steel_beam_l.TabIndex = 1;
            this.txt_steel_beam_l.Text = "8.0";
            this.txt_steel_beam_l.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label416
            // 
            this.label416.AutoSize = true;
            this.label416.Location = new System.Drawing.Point(23, 97);
            this.label416.Name = "label416";
            this.label416.Size = new System.Drawing.Size(73, 13);
            this.label416.TabIndex = 85;
            this.label416.Text = "Span = l = ";
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.btn_new_design);
            this.panel33.Controls.Add(this.btn_browse_design);
            this.panel33.Controls.Add(this.txt_project_name);
            this.panel33.Controls.Add(this.label830);
            this.panel33.Location = new System.Drawing.Point(12, 12);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(366, 61);
            this.panel33.TabIndex = 0;
            // 
            // btn_new_design
            // 
            this.btn_new_design.Location = new System.Drawing.Point(104, 4);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(121, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "New Design";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // btn_browse_design
            // 
            this.btn_browse_design.Location = new System.Drawing.Point(237, 4);
            this.btn_browse_design.Name = "btn_browse_design";
            this.btn_browse_design.Size = new System.Drawing.Size(121, 24);
            this.btn_browse_design.TabIndex = 189;
            this.btn_browse_design.Text = "Open Design";
            this.btn_browse_design.UseVisualStyleBackColor = true;
            this.btn_browse_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(104, 30);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(254, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label830
            // 
            this.label830.AutoSize = true;
            this.label830.Location = new System.Drawing.Point(5, 34);
            this.label830.Name = "label830";
            this.label830.Size = new System.Drawing.Size(93, 13);
            this.label830.TabIndex = 187;
            this.label830.Text = "Project Name :";
            // 
            // frmSteelBeamDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 365);
            this.Controls.Add(this.panel33);
            this.Controls.Add(this.uC_SteelSections_Beam);
            this.Controls.Add(this.btn_steel_beam_open_design);
            this.Controls.Add(this.btn_steel_beam_design);
            this.Controls.Add(this.txt_steel_beam_Pbs);
            this.Controls.Add(this.label422);
            this.Controls.Add(this.txt_steel_beam_Pss);
            this.Controls.Add(this.label421);
            this.Controls.Add(this.txt_steel_beam_Pms);
            this.Controls.Add(this.label420);
            this.Controls.Add(this.txt_steel_beam_V);
            this.Controls.Add(this.label419);
            this.Controls.Add(this.txt_steel_beam_M);
            this.Controls.Add(this.label425);
            this.Controls.Add(this.label424);
            this.Controls.Add(this.label429);
            this.Controls.Add(this.label428);
            this.Controls.Add(this.label427);
            this.Controls.Add(this.label426);
            this.Controls.Add(this.label423);
            this.Controls.Add(this.label418);
            this.Controls.Add(this.txt_steel_beam_a);
            this.Controls.Add(this.label417);
            this.Controls.Add(this.txt_steel_beam_l);
            this.Controls.Add(this.label416);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSteelBeamDesign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design of Steel Beam";
            this.Load += new System.EventHandler(this.frmSteelBeamDesign_Load);
            this.panel33.ResumeLayout(false);
            this.panel33.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC_SteelSections uC_SteelSections_Beam;
        private System.Windows.Forms.Button btn_steel_beam_open_design;
        private System.Windows.Forms.Button btn_steel_beam_design;
        private System.Windows.Forms.TextBox txt_steel_beam_Pbs;
        private System.Windows.Forms.Label label422;
        private System.Windows.Forms.TextBox txt_steel_beam_Pss;
        private System.Windows.Forms.Label label421;
        private System.Windows.Forms.TextBox txt_steel_beam_Pms;
        private System.Windows.Forms.Label label420;
        private System.Windows.Forms.TextBox txt_steel_beam_V;
        private System.Windows.Forms.Label label419;
        private System.Windows.Forms.TextBox txt_steel_beam_M;
        private System.Windows.Forms.Label label425;
        private System.Windows.Forms.Label label424;
        private System.Windows.Forms.Label label429;
        private System.Windows.Forms.Label label428;
        private System.Windows.Forms.Label label427;
        private System.Windows.Forms.Label label426;
        private System.Windows.Forms.Label label423;
        private System.Windows.Forms.Label label418;
        private System.Windows.Forms.TextBox txt_steel_beam_a;
        private System.Windows.Forms.Label label417;
        private System.Windows.Forms.TextBox txt_steel_beam_l;
        private System.Windows.Forms.Label label416;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_browse_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Label label830;
    }
}