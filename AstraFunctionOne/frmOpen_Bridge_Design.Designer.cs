namespace AstraFunctionOne
{
    partial class frmOpen_Bridge_Design
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
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.rbtn_new_analysis = new System.Windows.Forms.RadioButton();
            this.rbtn_open_analysis = new System.Windows.Forms.RadioButton();
            this.btn_process = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_option = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.rbtn_new_analysis);
            this.groupBox12.Controls.Add(this.rbtn_open_analysis);
            this.groupBox12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.Location = new System.Drawing.Point(11, 55);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(364, 88);
            this.groupBox12.TabIndex = 97;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Select Analysis && Design Option";
            // 
            // rbtn_new_analysis
            // 
            this.rbtn_new_analysis.AutoSize = true;
            this.rbtn_new_analysis.Checked = true;
            this.rbtn_new_analysis.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_new_analysis.Location = new System.Drawing.Point(6, 26);
            this.rbtn_new_analysis.Name = "rbtn_new_analysis";
            this.rbtn_new_analysis.Size = new System.Drawing.Size(319, 18);
            this.rbtn_new_analysis.TabIndex = 50;
            this.rbtn_new_analysis.TabStop = true;
            this.rbtn_new_analysis.Text = "New Analysis && Design [ASTRA_Data_Input.TXT)";
            this.rbtn_new_analysis.UseVisualStyleBackColor = true;
            this.rbtn_new_analysis.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtn_open_analysis
            // 
            this.rbtn_open_analysis.AutoSize = true;
            this.rbtn_open_analysis.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_open_analysis.Location = new System.Drawing.Point(6, 54);
            this.rbtn_open_analysis.Name = "rbtn_open_analysis";
            this.rbtn_open_analysis.Size = new System.Drawing.Size(324, 18);
            this.rbtn_open_analysis.TabIndex = 50;
            this.rbtn_open_analysis.Text = "Open Analysis && Design [ASTRA_Data_Input.TXT)";
            this.rbtn_open_analysis.UseVisualStyleBackColor = true;
            this.rbtn_open_analysis.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(66, 221);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(115, 36);
            this.btn_process.TabIndex = 98;
            this.btn_process.Text = "Proceed";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(208, 221);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(115, 36);
            this.btn_cancel.TabIndex = 98;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_option
            // 
            this.lbl_option.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_option.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_option.Location = new System.Drawing.Point(12, 146);
            this.lbl_option.Name = "lbl_option";
            this.lbl_option.Size = new System.Drawing.Size(363, 72);
            this.lbl_option.TabIndex = 99;
            this.lbl_option.Text = "Previous Analysis && Design already exist in the working folder that will be dele" +
    "ted by the \"New Design\".";
            this.lbl_option.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(364, 43);
            this.label2.TabIndex = 99;
            this.label2.Text = "RCC T GIRDER BRIDGE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmOpen_Bridge_Design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 263);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_option);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_process);
            this.Controls.Add(this.groupBox12);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpen_Bridge_Design";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Analysis & Design Option";
            this.Load += new System.EventHandler(this.frmOpen_Bridge_Design_Load);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RadioButton rbtn_new_analysis;
        private System.Windows.Forms.RadioButton rbtn_open_analysis;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_option;
        private System.Windows.Forms.Label label2;

    }
}