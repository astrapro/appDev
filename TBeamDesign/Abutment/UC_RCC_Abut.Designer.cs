namespace BridgeAnalysisDesign.Abutment
{
    partial class UC_RCC_Abut
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_abut_counterfort_LS = new System.Windows.Forms.TabPage();
            this.uC_Abut_Counterfort_LS1 = new BridgeAnalysisDesign.Abutment.UC_Abut_Counterfort_LS();
            this.tab_abut_counterfort_WS = new System.Windows.Forms.TabPage();
            this.uC_Abut1 = new BridgeAnalysisDesign.Abutment.UC_Abut();
            this.tab_abut_canlilever_WS = new System.Windows.Forms.TabPage();
            this.uC_Abut_Cant1 = new BridgeAnalysisDesign.Abutment.UC_Abut_Cant();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tab_abut_counterfort_LS.SuspendLayout();
            this.tab_abut_counterfort_WS.SuspendLayout();
            this.tab_abut_canlilever_WS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_abut_counterfort_LS);
            this.tabControl1.Controls.Add(this.tab_abut_counterfort_WS);
            this.tabControl1.Controls.Add(this.tab_abut_canlilever_WS);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(874, 656);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_abut_counterfort_LS
            // 
            this.tab_abut_counterfort_LS.Controls.Add(this.uC_Abut_Counterfort_LS1);
            this.tab_abut_counterfort_LS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_abut_counterfort_LS.Location = new System.Drawing.Point(4, 22);
            this.tab_abut_counterfort_LS.Name = "tab_abut_counterfort_LS";
            this.tab_abut_counterfort_LS.Padding = new System.Windows.Forms.Padding(3);
            this.tab_abut_counterfort_LS.Size = new System.Drawing.Size(866, 630);
            this.tab_abut_counterfort_LS.TabIndex = 2;
            this.tab_abut_counterfort_LS.Text = "RCC Abutment Counterfort Limit State";
            this.tab_abut_counterfort_LS.UseVisualStyleBackColor = true;
            // 
            // uC_Abut_Counterfort_LS1
            // 
            this.uC_Abut_Counterfort_LS1.AutoScroll = true;
            this.uC_Abut_Counterfort_LS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Abut_Counterfort_LS1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Abut_Counterfort_LS1.iapp = null;
            this.uC_Abut_Counterfort_LS1.Is_Individual = true;
            this.uC_Abut_Counterfort_LS1.Length = 51.3D;
            this.uC_Abut_Counterfort_LS1.Location = new System.Drawing.Point(3, 3);
            this.uC_Abut_Counterfort_LS1.Name = "uC_Abut_Counterfort_LS1";
            this.uC_Abut_Counterfort_LS1.Reaction_A = "4417.59";
            this.uC_Abut_Counterfort_LS1.Reaction_B = "4417.59";
            this.uC_Abut_Counterfort_LS1.Size = new System.Drawing.Size(860, 624);
            this.uC_Abut_Counterfort_LS1.TabIndex = 0;
            this.uC_Abut_Counterfort_LS1.dead_load_CheckedChanged += new System.EventHandler(this.uC_Abut_Counterfort_LS1_dead_load_CheckedChanged);
            this.uC_Abut_Counterfort_LS1.Load += new System.EventHandler(this.uC_Abut_Counterfort_LS1_Load);
            // 
            // tab_abut_counterfort_WS
            // 
            this.tab_abut_counterfort_WS.Controls.Add(this.uC_Abut1);
            this.tab_abut_counterfort_WS.Location = new System.Drawing.Point(4, 22);
            this.tab_abut_counterfort_WS.Name = "tab_abut_counterfort_WS";
            this.tab_abut_counterfort_WS.Padding = new System.Windows.Forms.Padding(3);
            this.tab_abut_counterfort_WS.Size = new System.Drawing.Size(866, 630);
            this.tab_abut_counterfort_WS.TabIndex = 0;
            this.tab_abut_counterfort_WS.Text = "RCC Abutment Counterfort Working Stress";
            this.tab_abut_counterfort_WS.UseVisualStyleBackColor = true;
            // 
            // uC_Abut1
            // 
            this.uC_Abut1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Abut1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Abut1.Is_Box_Type = true;
            this.uC_Abut1.Length = 60D;
            this.uC_Abut1.Location = new System.Drawing.Point(3, 3);
            this.uC_Abut1.Name = "uC_Abut1";
            this.uC_Abut1.Overhang = 0.65D;
            this.uC_Abut1.Size = new System.Drawing.Size(860, 624);
            this.uC_Abut1.TabIndex = 0;
            this.uC_Abut1.Width = 12D;
            this.uC_Abut1.Load += new System.EventHandler(this.uC_Abut1_Load);
            // 
            // tab_abut_canlilever_WS
            // 
            this.tab_abut_canlilever_WS.Controls.Add(this.uC_Abut_Cant1);
            this.tab_abut_canlilever_WS.Location = new System.Drawing.Point(4, 22);
            this.tab_abut_canlilever_WS.Name = "tab_abut_canlilever_WS";
            this.tab_abut_canlilever_WS.Padding = new System.Windows.Forms.Padding(3);
            this.tab_abut_canlilever_WS.Size = new System.Drawing.Size(866, 630);
            this.tab_abut_canlilever_WS.TabIndex = 1;
            this.tab_abut_canlilever_WS.Text = "RCC Abutment Cantilever Working Stress";
            this.tab_abut_canlilever_WS.UseVisualStyleBackColor = true;
            // 
            // uC_Abut_Cant1
            // 
            this.uC_Abut_Cant1.Dead_Load_Reactions = "1635.12";
            this.uC_Abut_Cant1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Abut_Cant1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_Abut_Cant1.Length = "7.900";
            this.uC_Abut_Cant1.Location = new System.Drawing.Point(3, 3);
            this.uC_Abut_Cant1.Name = "uC_Abut_Cant1";
            this.uC_Abut_Cant1.Size = new System.Drawing.Size(860, 624);
            this.uC_Abut_Cant1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 656);
            this.panel1.TabIndex = 1;
            // 
            // UC_RCC_Abut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_RCC_Abut";
            this.Size = new System.Drawing.Size(874, 656);
            this.tabControl1.ResumeLayout(false);
            this.tab_abut_counterfort_LS.ResumeLayout(false);
            this.tab_abut_counterfort_WS.ResumeLayout(false);
            this.tab_abut_canlilever_WS.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_abut_counterfort_WS;
        private System.Windows.Forms.TabPage tab_abut_canlilever_WS;
        private System.Windows.Forms.Panel panel1;
        private UC_Abut_Cant uC_Abut_Cant1;
        private System.Windows.Forms.TabPage tab_abut_counterfort_LS;
        public UC_Abut_Counterfort_LS uC_Abut_Counterfort_LS1;
        public UC_Abut uC_Abut1;
    }
}
