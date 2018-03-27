namespace ASTRAStructures
{
    partial class frmPlanMovingLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanMovingLoad));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Moving_Load = new System.Windows.Forms.Button();
            this.btn_DrawModel = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uC_CAD1 = new AstraAccess.ADOC.UC_CAD();
            this.btn_stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_time = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_time);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_stop);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Moving_Load);
            this.splitContainer1.Panel1.Controls.Add(this.btn_DrawModel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uC_CAD1);
            this.splitContainer1.Size = new System.Drawing.Size(825, 495);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_Moving_Load
            // 
            this.btn_Moving_Load.Location = new System.Drawing.Point(246, 11);
            this.btn_Moving_Load.Name = "btn_Moving_Load";
            this.btn_Moving_Load.Size = new System.Drawing.Size(97, 31);
            this.btn_Moving_Load.TabIndex = 0;
            this.btn_Moving_Load.Text = "Run";
            this.btn_Moving_Load.UseVisualStyleBackColor = true;
            this.btn_Moving_Load.Click += new System.EventHandler(this.btn_Moving_Load_Click);
            // 
            // btn_DrawModel
            // 
            this.btn_DrawModel.Location = new System.Drawing.Point(55, 11);
            this.btn_DrawModel.Name = "btn_DrawModel";
            this.btn_DrawModel.Size = new System.Drawing.Size(149, 31);
            this.btn_DrawModel.TabIndex = 0;
            this.btn_DrawModel.Text = "Draw Model";
            this.btn_DrawModel.UseVisualStyleBackColor = true;
            this.btn_DrawModel.Visible = false;
            this.btn_DrawModel.Click += new System.EventHandler(this.btn_DrawModel_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uC_CAD1
            // 
            this.uC_CAD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_CAD1.Location = new System.Drawing.Point(0, 0);
            this.uC_CAD1.Name = "uC_CAD1";
            this.uC_CAD1.Size = new System.Drawing.Size(823, 422);
            this.uC_CAD1.TabIndex = 0;
            this.uC_CAD1.View_Buttons = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(349, 11);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(97, 31);
            this.btn_stop.TabIndex = 0;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_Moving_Load_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(462, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Time Interval";
            // 
            // txt_time
            // 
            this.txt_time.Location = new System.Drawing.Point(552, 17);
            this.txt_time.Name = "txt_time";
            this.txt_time.Size = new System.Drawing.Size(56, 21);
            this.txt_time.TabIndex = 2;
            this.txt_time.Text = "0.5";
            this.txt_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(614, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "sec";
            // 
            // frmPlanMovingLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 495);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPlanMovingLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan Moving Load";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPlanMovingLoad_FormClosing);
            this.Load += new System.EventHandler(this.frmPlanMovingLoad_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AstraAccess.ADOC.UC_CAD uC_CAD1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_DrawModel;
        private System.Windows.Forms.Button btn_Moving_Load;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox txt_time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}