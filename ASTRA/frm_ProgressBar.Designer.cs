namespace AstraFunctionOne
{
    partial class frm_ProgressBar
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
            AbortThread();
            if (disposing && (components != null))
            {
                On = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ProgressBar));
            this.lbl_percentage = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_time_remain = new System.Windows.Forms.Label();
            this.lbl_tr = new System.Windows.Forms.Label();
            this.lbl_total_time = new System.Windows.Forms.Label();
            this.lbl_tm = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_percentage
            // 
            this.lbl_percentage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_percentage.AutoSize = true;
            this.lbl_percentage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_percentage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_percentage.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_percentage.ForeColor = System.Drawing.Color.Blue;
            this.lbl_percentage.Location = new System.Drawing.Point(211, 4);
            this.lbl_percentage.Name = "lbl_percentage";
            this.lbl_percentage.Size = new System.Drawing.Size(33, 22);
            this.lbl_percentage.TabIndex = 3;
            this.lbl_percentage.Text = "0%";
            this.lbl_percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_percentage.Click += new System.EventHandler(this.lbl_percentage_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(475, 27);
            this.progressBar1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_time_remain);
            this.panel1.Controls.Add(this.lbl_tr);
            this.panel1.Controls.Add(this.lbl_total_time);
            this.panel1.Controls.Add(this.lbl_tm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 24);
            this.panel1.TabIndex = 4;
            // 
            // lbl_time_remain
            // 
            this.lbl_time_remain.AutoSize = true;
            this.lbl_time_remain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time_remain.Location = new System.Drawing.Point(361, 5);
            this.lbl_time_remain.Name = "lbl_time_remain";
            this.lbl_time_remain.Size = new System.Drawing.Size(67, 13);
            this.lbl_time_remain.TabIndex = 3;
            this.lbl_time_remain.Text = "Total Time";
            // 
            // lbl_tr
            // 
            this.lbl_tr.AutoSize = true;
            this.lbl_tr.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tr.Location = new System.Drawing.Point(225, 5);
            this.lbl_tr.Name = "lbl_tr";
            this.lbl_tr.Size = new System.Drawing.Size(119, 13);
            this.lbl_tr.TabIndex = 2;
            this.lbl_tr.Text = "Time Remaining :";
            // 
            // lbl_total_time
            // 
            this.lbl_total_time.AutoSize = true;
            this.lbl_total_time.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_time.Location = new System.Drawing.Point(119, 5);
            this.lbl_total_time.Name = "lbl_total_time";
            this.lbl_total_time.Size = new System.Drawing.Size(67, 13);
            this.lbl_total_time.TabIndex = 1;
            this.lbl_total_time.Text = "Total Time";
            // 
            // lbl_tm
            // 
            this.lbl_tm.AutoSize = true;
            this.lbl_tm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tm.Location = new System.Drawing.Point(3, 5);
            this.lbl_tm.Name = "lbl_tm";
            this.lbl_tm.Size = new System.Drawing.Size(84, 13);
            this.lbl_tm.TabIndex = 0;
            this.lbl_tm.Text = "Total Time :";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1009;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frm_ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 53);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_percentage);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ProgressBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please wait...";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_ProgressBar_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_percentage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_total_time;
        private System.Windows.Forms.Label lbl_tm;
        private System.Windows.Forms.Label lbl_time_remain;
        private System.Windows.Forms.Label lbl_tr;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}