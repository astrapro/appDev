namespace AstraFunctionOne
{
    partial class frm_DesignStandardOption
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
            this.grb_Option = new System.Windows.Forms.GroupBox();
            this.rbtn_LRFD = new System.Windows.Forms.RadioButton();
            this.rbtn_BS = new System.Windows.Forms.RadioButton();
            this.rbtn_IS = new System.Windows.Forms.RadioButton();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grb_Option.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_Option
            // 
            this.grb_Option.Controls.Add(this.rbtn_LRFD);
            this.grb_Option.Controls.Add(this.rbtn_BS);
            this.grb_Option.Controls.Add(this.rbtn_IS);
            this.grb_Option.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Option.Location = new System.Drawing.Point(15, 12);
            this.grb_Option.Name = "grb_Option";
            this.grb_Option.Size = new System.Drawing.Size(295, 100);
            this.grb_Option.TabIndex = 1;
            this.grb_Option.TabStop = false;
            this.grb_Option.Text = "Select the Design Standard";
            // 
            // rbtn_LRFD
            // 
            this.rbtn_LRFD.AutoSize = true;
            this.rbtn_LRFD.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_LRFD.Location = new System.Drawing.Point(6, 47);
            this.rbtn_LRFD.Name = "rbtn_LRFD";
            this.rbtn_LRFD.Size = new System.Drawing.Size(195, 20);
            this.rbtn_LRFD.TabIndex = 1;
            this.rbtn_LRFD.Text = "AASHTO - LRFD Standard";
            this.rbtn_LRFD.UseVisualStyleBackColor = true;
            this.rbtn_LRFD.Visible = false;
            // 
            // rbtn_BS
            // 
            this.rbtn_BS.AutoSize = true;
            this.rbtn_BS.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_BS.Location = new System.Drawing.Point(6, 21);
            this.rbtn_BS.Name = "rbtn_BS";
            this.rbtn_BS.Size = new System.Drawing.Size(257, 20);
            this.rbtn_BS.TabIndex = 1;
            this.rbtn_BS.Text = "British [BSi] Standard EUROCODE 2";
            this.rbtn_BS.UseVisualStyleBackColor = true;
            // 
            // rbtn_IS
            // 
            this.rbtn_IS.AutoSize = true;
            this.rbtn_IS.Checked = true;
            this.rbtn_IS.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_IS.Location = new System.Drawing.Point(6, 73);
            this.rbtn_IS.Name = "rbtn_IS";
            this.rbtn_IS.Size = new System.Drawing.Size(133, 20);
            this.rbtn_IS.TabIndex = 0;
            this.rbtn_IS.TabStop = true;
            this.rbtn_IS.Text = "IRC/IS Standard";
            this.rbtn_IS.UseVisualStyleBackColor = true;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(91, 118);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(138, 31);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 75);
            this.label1.TabIndex = 2;
            this.label1.Text = "For Limit State Design the system must have Microsoft Excel 2007 or Newer Version" +
    " installed.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_DesignStandardOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 250);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.grb_Option);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "frm_DesignStandardOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Design Standard";
            this.Load += new System.EventHandler(this.frm_DesignStandardOption_Load);
            this.grb_Option.ResumeLayout(false);
            this.grb_Option.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_Option;
        private System.Windows.Forms.RadioButton rbtn_BS;
        private System.Windows.Forms.RadioButton rbtn_IS;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtn_LRFD;
    }
}