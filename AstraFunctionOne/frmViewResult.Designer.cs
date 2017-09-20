namespace AstraFunctionOne
{
    partial class frmViewResult
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewResult));
            this.rtbData = new System.Windows.Forms.RichTextBox();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.psd = new System.Windows.Forms.PageSetupDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_page_setup = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_print_prev = new System.Windows.Forms.ToolStripButton();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_print = new System.Windows.Forms.ToolStripButton();
            this.tsmi_land_scape = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmb_step = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_prev = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_next = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_line_col = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_last = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssb_find = new System.Windows.Forms.ToolStripSplitButton();
            this.tslbl_notok = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.spc_1 = new System.Windows.Forms.SplitContainer();
            this.lsv_steps = new System.Windows.Forms.ListView();
            this.dessteps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.spc_1.Panel1.SuspendLayout();
            this.spc_1.Panel2.SuspendLayout();
            this.spc_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbData
            // 
            this.rtbData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbData.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbData.ForeColor = System.Drawing.Color.Black;
            this.rtbData.Location = new System.Drawing.Point(0, 0);
            this.rtbData.Name = "rtbData";
            this.rtbData.ReadOnly = true;
            this.rtbData.Size = new System.Drawing.Size(805, 517);
            this.rtbData.TabIndex = 0;
            this.rtbData.Text = "";
            this.rtbData.WordWrap = false;
            this.rtbData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbData_MouseClick);
            this.rtbData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbData_KeyDown);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // ppd
            // 
            this.ppd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppd.ClientSize = new System.Drawing.Size(400, 300);
            this.ppd.Document = this.pd;
            this.ppd.Enabled = true;
            this.ppd.Icon = ((System.Drawing.Icon)(resources.GetObject("ppd.Icon")));
            this.ppd.Name = "ppd";
            this.ppd.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_page_setup,
            this.tss1,
            this.tsb_print_prev,
            this.tss2,
            this.tsb_print,
            this.tsmi_land_scape,
            this.toolStripSeparator1,
            this.cmb_step,
            this.toolStripSeparator2,
            this.tsb_prev,
            this.toolStripSeparator4,
            this.tsb_next,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1005, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // tsb_page_setup
            // 
            this.tsb_page_setup.Image = ((System.Drawing.Image)(resources.GetObject("tsb_page_setup.Image")));
            this.tsb_page_setup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_page_setup.Name = "tsb_page_setup";
            this.tsb_page_setup.Size = new System.Drawing.Size(86, 22);
            this.tsb_page_setup.Text = "Page &Setup";
            this.tsb_page_setup.Click += new System.EventHandler(this.tsb_print_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_print_prev
            // 
            this.tsb_print_prev.Image = ((System.Drawing.Image)(resources.GetObject("tsb_print_prev.Image")));
            this.tsb_print_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_print_prev.Name = "tsb_print_prev";
            this.tsb_print_prev.Size = new System.Drawing.Size(96, 22);
            this.tsb_print_prev.Text = "Print Pre&view";
            this.tsb_print_prev.Click += new System.EventHandler(this.tsb_print_Click);
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_print
            // 
            this.tsb_print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_print.Image")));
            this.tsb_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_print.Name = "tsb_print";
            this.tsb_print.Size = new System.Drawing.Size(52, 22);
            this.tsb_print.Text = "&Print";
            this.tsb_print.Click += new System.EventHandler(this.tsb_print_Click);
            // 
            // tsmi_land_scape
            // 
            this.tsmi_land_scape.CheckOnClick = true;
            this.tsmi_land_scape.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_land_scape.Image")));
            this.tsmi_land_scape.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmi_land_scape.Name = "tsmi_land_scape";
            this.tsmi_land_scape.Size = new System.Drawing.Size(83, 22);
            this.tsmi_land_scape.Text = "Landscape";
            this.tsmi_land_scape.Click += new System.EventHandler(this.tsmi_land_scape_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmb_step
            // 
            this.cmb_step.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_step.DropDownWidth = 500;
            this.cmb_step.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_step.ForeColor = System.Drawing.Color.Blue;
            this.cmb_step.Name = "cmb_step";
            this.cmb_step.Size = new System.Drawing.Size(599, 25);
            this.cmb_step.ToolTipText = "Click Here to go STEPS and TABLES quick references.";
            this.cmb_step.SelectedIndexChanged += new System.EventHandler(this.cmb_step_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_prev
            // 
            this.tsb_prev.Checked = true;
            this.tsb_prev.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.tsb_prev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_prev.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_prev.Image = global::AstraFunctionOne.Properties.Resources.DataContainer_MovePreviousHS1;
            this.tsb_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_prev.Name = "tsb_prev";
            this.tsb_prev.Size = new System.Drawing.Size(23, 22);
            this.tsb_prev.Text = "Previos Selected Text";
            this.tsb_prev.Click += new System.EventHandler(this.tsb_prev_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_next
            // 
            this.tsb_next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_next.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_next.Image = global::AstraFunctionOne.Properties.Resources.DataContainer_MoveNextHS;
            this.tsb_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_next.Name = "tsb_next";
            this.tsb_next.Size = new System.Drawing.Size(23, 20);
            this.tsb_next.Text = "Next Selected Text";
            this.tsb_next.Click += new System.EventHandler(this.tsb_prev_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_line_col,
            this.tssl_last,
            this.toolStripStatusLabel1,
            this.tssb_find,
            this.tslbl_notok});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1005, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_line_col
            // 
            this.lbl_line_col.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_line_col.Name = "lbl_line_col";
            this.lbl_line_col.Size = new System.Drawing.Size(101, 17);
            this.lbl_line_col.Text = "Line : 1 , Col : 1";
            this.lbl_line_col.ToolTipText = "Click Here to go this selected lines.";
            this.lbl_line_col.Click += new System.EventHandler(this.lbl_line_col_Click);
            // 
            // tssl_last
            // 
            this.tssl_last.BackColor = System.Drawing.Color.Yellow;
            this.tssl_last.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssl_last.ForeColor = System.Drawing.Color.Blue;
            this.tssl_last.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssl_last.Name = "tssl_last";
            this.tssl_last.Size = new System.Drawing.Size(0, 17);
            this.tssl_last.Click += new System.EventHandler(this.tssl_last_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabel1.Text = "                         ";
            // 
            // tssb_find
            // 
            this.tssb_find.Image = ((System.Drawing.Image)(resources.GetObject("tssb_find.Image")));
            this.tssb_find.ImageTransparentColor = System.Drawing.Color.Black;
            this.tssb_find.Name = "tssb_find";
            this.tssb_find.Size = new System.Drawing.Size(87, 20);
            this.tssb_find.Text = "Find Text";
            this.tssb_find.ButtonClick += new System.EventHandler(this.tssb_find_ButtonClick);
            // 
            // tslbl_notok
            // 
            this.tslbl_notok.ForeColor = System.Drawing.Color.Red;
            this.tslbl_notok.Name = "tslbl_notok";
            this.tslbl_notok.Size = new System.Drawing.Size(197, 17);
            this.tslbl_notok.Text = "            \"NOT OK\" Statement Found! ";
            this.tslbl_notok.Click += new System.EventHandler(this.tslbl_notok_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // spc_1
            // 
            this.spc_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_1.Location = new System.Drawing.Point(0, 25);
            this.spc_1.Name = "spc_1";
            // 
            // spc_1.Panel1
            // 
            this.spc_1.Panel1.Controls.Add(this.lsv_steps);
            // 
            // spc_1.Panel2
            // 
            this.spc_1.Panel2.Controls.Add(this.rtbData);
            this.spc_1.Size = new System.Drawing.Size(1005, 519);
            this.spc_1.SplitterDistance = 195;
            this.spc_1.SplitterIncrement = 3;
            this.spc_1.SplitterWidth = 3;
            this.spc_1.TabIndex = 3;
            // 
            // lsv_steps
            // 
            this.lsv_steps.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsv_steps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dessteps});
            this.lsv_steps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_steps.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_steps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsv_steps.Location = new System.Drawing.Point(0, 0);
            this.lsv_steps.Name = "lsv_steps";
            this.lsv_steps.ShowItemToolTips = true;
            this.lsv_steps.Size = new System.Drawing.Size(193, 517);
            this.lsv_steps.TabIndex = 0;
            this.lsv_steps.UseCompatibleStateImageBehavior = false;
            this.lsv_steps.View = System.Windows.Forms.View.Details;
            this.lsv_steps.SelectedIndexChanged += new System.EventHandler(this.lstb_steps_SelectedIndexChanged);
            // 
            // dessteps
            // 
            this.dessteps.Text = "DESIGN STEPS";
            this.dessteps.Width = 702;
            // 
            // frmViewResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 566);
            this.Controls.Add(this.spc_1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewResult";
            this.Opacity = 0.99D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Result";
            this.Load += new System.EventHandler(this.frmViewResult_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.spc_1.Panel1.ResumeLayout(false);
            this.spc_1.Panel2.ResumeLayout(false);
            this.spc_1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbData;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PageSetupDialog psd;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_page_setup;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.ToolStripButton tsb_print_prev;
        private System.Windows.Forms.ToolStripSeparator tss2;
        private System.Windows.Forms.ToolStripButton tsb_print;
        private System.Windows.Forms.ToolStripComboBox cmb_step;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_line_col;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tsb_prev;
        private System.Windows.Forms.ToolStripButton tsb_next;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsmi_land_scape;
        private System.Windows.Forms.ToolStripStatusLabel tssl_last;
        private System.Windows.Forms.ToolStripSplitButton tssb_find;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tslbl_notok;
        private System.Windows.Forms.SplitContainer spc_1;
        private System.Windows.Forms.ListView lsv_steps;
        private System.Windows.Forms.ColumnHeader dessteps;

    }
}