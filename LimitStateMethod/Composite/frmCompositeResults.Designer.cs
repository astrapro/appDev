namespace LimitStateMethod.Composite
{
    partial class frmCompositeResults
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_read = new System.Windows.Forms.RadioButton();
            this.rbtn_openAnalysis = new System.Windows.Forms.RadioButton();
            this.rbtn_designForces = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(24, 118);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(138, 36);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(181, 118);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(138, 36);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_designForces);
            this.groupBox1.Controls.Add(this.rbtn_openAnalysis);
            this.groupBox1.Controls.Add(this.rbtn_read);
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a Option";
            // 
            // rbtn_read
            // 
            this.rbtn_read.AutoSize = true;
            this.rbtn_read.Location = new System.Drawing.Point(18, 20);
            this.rbtn_read.Name = "rbtn_read";
            this.rbtn_read.Size = new System.Drawing.Size(150, 17);
            this.rbtn_read.TabIndex = 0;
            this.rbtn_read.Text = "Read Analysis Results";
            this.rbtn_read.UseVisualStyleBackColor = true;
            // 
            // rbtn_openAnalysis
            // 
            this.rbtn_openAnalysis.AutoSize = true;
            this.rbtn_openAnalysis.Checked = true;
            this.rbtn_openAnalysis.Location = new System.Drawing.Point(18, 43);
            this.rbtn_openAnalysis.Name = "rbtn_openAnalysis";
            this.rbtn_openAnalysis.Size = new System.Drawing.Size(272, 17);
            this.rbtn_openAnalysis.TabIndex = 0;
            this.rbtn_openAnalysis.TabStop = true;
            this.rbtn_openAnalysis.Text = "Open Analysis Report for Selected Analysis";
            this.rbtn_openAnalysis.UseVisualStyleBackColor = true;
            // 
            // rbtn_designForces
            // 
            this.rbtn_designForces.AutoSize = true;
            this.rbtn_designForces.Location = new System.Drawing.Point(18, 66);
            this.rbtn_designForces.Name = "rbtn_designForces";
            this.rbtn_designForces.Size = new System.Drawing.Size(139, 17);
            this.rbtn_designForces.TabIndex = 0;
            this.rbtn_designForces.Text = "Open Design Forces";
            this.rbtn_designForces.UseVisualStyleBackColor = true;
            // 
            // frmCompositeResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 166);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCompositeResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Composite Results";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_designForces;
        private System.Windows.Forms.RadioButton rbtn_openAnalysis;
        private System.Windows.Forms.RadioButton rbtn_read;
    }
}