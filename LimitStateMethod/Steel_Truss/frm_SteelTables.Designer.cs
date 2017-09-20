namespace LimitStateMethod.Steel_Truss
{
    partial class frm_SteelTables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SteelTables));
            this.uC_Beams1 = new LimitStateMethod.SuspensionBridge.UC_Beams();
            this.uC_Channels1 = new LimitStateMethod.SuspensionBridge.UC_Channels();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uC_Angles1 = new LimitStateMethod.SuspensionBridge.UC_Angles();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pg_channels = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pg_beam = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtn_bs = new System.Windows.Forms.RadioButton();
            this.rbtn_irc = new System.Windows.Forms.RadioButton();
            this.rbtn_aisc = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uC_Beams1
            // 
            this.uC_Beams1.Area = "";
            this.uC_Beams1.D = "";
            this.uC_Beams1.iApp = null;
            this.uC_Beams1.Ixx = "";
            this.uC_Beams1.Location = new System.Drawing.Point(11, 7);
            this.uC_Beams1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_Beams1.Name = "uC_Beams1";
            this.uC_Beams1.Pro_Grid = null;
            this.uC_Beams1.Section_Name = "";
            this.uC_Beams1.Section_Size = "";
            this.uC_Beams1.Size = new System.Drawing.Size(244, 344);
            this.uC_Beams1.TabIndex = 1;
            this.uC_Beams1.Title = "Beams";
            this.uC_Beams1.tw = "";
            this.uC_Beams1.Zxx = "";
            // 
            // uC_Channels1
            // 
            this.uC_Channels1.Area = "";
            this.uC_Channels1.D = "";
            this.uC_Channels1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Channels1.iApp = null;
            this.uC_Channels1.Ixx = "";
            this.uC_Channels1.Location = new System.Drawing.Point(8, 7);
            this.uC_Channels1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uC_Channels1.Name = "uC_Channels1";
            this.uC_Channels1.Pro_Grid = null;
            this.uC_Channels1.Section_Name = "";
            this.uC_Channels1.Section_Size = "";
            this.uC_Channels1.Size = new System.Drawing.Size(241, 345);
            this.uC_Channels1.TabIndex = 2;
            this.uC_Channels1.Title = "Channels";
            this.uC_Channels1.tw = "";
            this.uC_Channels1.Zxx = "";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(256, 7);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(278, 345);
            this.propertyGrid1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(552, 390);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uC_Angles1);
            this.tabPage1.Controls.Add(this.propertyGrid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(544, 361);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Angle Sections";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uC_Angles1
            // 
            this.uC_Angles1.Area = "";
            this.uC_Angles1.b = "";
            this.uC_Angles1.Cyy = "";
            this.uC_Angles1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Angles1.h = "";
            this.uC_Angles1.iApp = null;
            this.uC_Angles1.Ixx = "";
            this.uC_Angles1.Iyy = "";
            this.uC_Angles1.Location = new System.Drawing.Point(7, 9);
            this.uC_Angles1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uC_Angles1.Name = "uC_Angles1";
            this.uC_Angles1.Pro_Grid = null;
            this.uC_Angles1.Section_Name = "";
            this.uC_Angles1.Section_Size = "";
            this.uC_Angles1.Size = new System.Drawing.Size(233, 343);
            this.uC_Angles1.TabIndex = 5;
            this.uC_Angles1.tf = "";
            this.uC_Angles1.Title = "Angles";
            this.uC_Angles1.tw = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pg_channels);
            this.tabPage2.Controls.Add(this.uC_Channels1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(544, 361);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Channel Sections";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pg_channels
            // 
            this.pg_channels.HelpVisible = false;
            this.pg_channels.Location = new System.Drawing.Point(256, 7);
            this.pg_channels.Name = "pg_channels";
            this.pg_channels.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pg_channels.Size = new System.Drawing.Size(278, 344);
            this.pg_channels.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pg_beam);
            this.tabPage3.Controls.Add(this.uC_Beams1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(544, 361);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Beam Sections";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pg_beam
            // 
            this.pg_beam.HelpVisible = false;
            this.pg_beam.Location = new System.Drawing.Point(262, 7);
            this.pg_beam.Name = "pg_beam";
            this.pg_beam.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pg_beam.Size = new System.Drawing.Size(272, 344);
            this.pg_beam.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtn_aisc);
            this.panel1.Controls.Add(this.rbtn_bs);
            this.panel1.Controls.Add(this.rbtn_irc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 33);
            this.panel1.TabIndex = 5;
            // 
            // rbtn_bs
            // 
            this.rbtn_bs.AutoSize = true;
            this.rbtn_bs.Location = new System.Drawing.Point(198, 8);
            this.rbtn_bs.Name = "rbtn_bs";
            this.rbtn_bs.Size = new System.Drawing.Size(143, 20);
            this.rbtn_bs.TabIndex = 0;
            this.rbtn_bs.TabStop = true;
            this.rbtn_bs.Text = "British Standard";
            this.rbtn_bs.UseVisualStyleBackColor = true;
            this.rbtn_bs.Click += new System.EventHandler(this.rbtn_irc_CheckedChanged);
            // 
            // rbtn_irc
            // 
            this.rbtn_irc.AutoSize = true;
            this.rbtn_irc.Location = new System.Drawing.Point(29, 8);
            this.rbtn_irc.Name = "rbtn_irc";
            this.rbtn_irc.Size = new System.Drawing.Size(142, 20);
            this.rbtn_irc.TabIndex = 0;
            this.rbtn_irc.TabStop = true;
            this.rbtn_irc.Text = "Indian Standard";
            this.rbtn_irc.UseVisualStyleBackColor = true;
            this.rbtn_irc.CheckedChanged += new System.EventHandler(this.rbtn_irc_CheckedChanged);
            // 
            // rbtn_aisc
            // 
            this.rbtn_aisc.AutoSize = true;
            this.rbtn_aisc.Location = new System.Drawing.Point(361, 8);
            this.rbtn_aisc.Name = "rbtn_aisc";
            this.rbtn_aisc.Size = new System.Drawing.Size(160, 20);
            this.rbtn_aisc.TabIndex = 0;
            this.rbtn_aisc.TabStop = true;
            this.rbtn_aisc.Text = "Americal Standard";
            this.rbtn_aisc.UseVisualStyleBackColor = true;
            this.rbtn_aisc.Click += new System.EventHandler(this.rbtn_irc_CheckedChanged);
            // 
            // frm_SteelTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 423);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SteelTables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steel Section Tables";
            this.Load += new System.EventHandler(this.frm_SteelTables_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SuspensionBridge.UC_Beams uC_Beams1;
        private SuspensionBridge.UC_Channels uC_Channels1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid pg_channels;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PropertyGrid pg_beam;
        private SuspensionBridge.UC_Angles uC_Angles1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtn_bs;
        private System.Windows.Forms.RadioButton rbtn_irc;
        private System.Windows.Forms.RadioButton rbtn_aisc;
    }
}