namespace AstraFunctionOne
{
    partial class frm_findcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_findcs));
            this.btn_find_next = new System.Windows.Forms.Button();
            this.grb_find = new System.Windows.Forms.GroupBox();
            this.chk_case = new System.Windows.Forms.CheckBox();
            this.cmb_find = new System.Windows.Forms.ComboBox();
            this.btn_find_prev = new System.Windows.Forms.Button();
            this.grb_find.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_find_next
            // 
            this.btn_find_next.Location = new System.Drawing.Point(233, 50);
            this.btn_find_next.Name = "btn_find_next";
            this.btn_find_next.Size = new System.Drawing.Size(109, 27);
            this.btn_find_next.TabIndex = 1;
            this.btn_find_next.Text = "Find Next";
            this.btn_find_next.UseVisualStyleBackColor = true;
            this.btn_find_next.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // grb_find
            // 
            this.grb_find.Controls.Add(this.chk_case);
            this.grb_find.Controls.Add(this.cmb_find);
            this.grb_find.Controls.Add(this.btn_find_prev);
            this.grb_find.Controls.Add(this.btn_find_next);
            this.grb_find.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_find.Location = new System.Drawing.Point(0, 8);
            this.grb_find.Name = "grb_find";
            this.grb_find.Size = new System.Drawing.Size(348, 93);
            this.grb_find.TabIndex = 0;
            this.grb_find.TabStop = false;
            // 
            // chk_case
            // 
            this.chk_case.AutoSize = true;
            this.chk_case.Location = new System.Drawing.Point(128, 55);
            this.chk_case.Name = "chk_case";
            this.chk_case.Size = new System.Drawing.Size(99, 18);
            this.chk_case.TabIndex = 2;
            this.chk_case.Text = "Match Case";
            this.chk_case.UseVisualStyleBackColor = true;
            this.chk_case.CheckedChanged += new System.EventHandler(this.txt_find_TextChanged);
            // 
            // cmb_find
            // 
            this.cmb_find.FormattingEnabled = true;
            this.cmb_find.Location = new System.Drawing.Point(6, 22);
            this.cmb_find.Name = "cmb_find";
            this.cmb_find.Size = new System.Drawing.Size(336, 22);
            this.cmb_find.TabIndex = 0;
            this.cmb_find.TextChanged += new System.EventHandler(this.txt_find_TextChanged);
            this.cmb_find.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_find_KeyPress);
            // 
            // btn_find_prev
            // 
            this.btn_find_prev.Enabled = false;
            this.btn_find_prev.Location = new System.Drawing.Point(6, 50);
            this.btn_find_prev.Name = "btn_find_prev";
            this.btn_find_prev.Size = new System.Drawing.Size(109, 27);
            this.btn_find_prev.TabIndex = 3;
            this.btn_find_prev.Text = "Find Previous";
            this.btn_find_prev.UseVisualStyleBackColor = true;
            this.btn_find_prev.Click += new System.EventHandler(this.btn_find_prev_Click);
            // 
            // frm_findcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 113);
            this.Controls.Add(this.grb_find);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_findcs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Text";
            this.Load += new System.EventHandler(this.frm_findcs_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_find_KeyPress);
            this.grb_find.ResumeLayout(false);
            this.grb_find.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_find_next;
        private System.Windows.Forms.GroupBox grb_find;
        private System.Windows.Forms.Button btn_find_prev;
        private System.Windows.Forms.ComboBox cmb_find;
        private System.Windows.Forms.CheckBox chk_case;
    }
}