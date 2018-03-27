namespace AstraFunctionOne
{
    partial class frmLockedVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLockedVersion));
            this.lbl_code = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txt_Auth_code_Bridge = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_strucrure = new System.Windows.Forms.CheckBox();
            this.chk_bridge = new System.Windows.Forms.CheckBox();
            this.txt_Auth_code_Building = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_code
            // 
            this.lbl_code.AutoSize = true;
            this.lbl_code.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_code.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_code.Location = new System.Drawing.Point(19, 100);
            this.lbl_code.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_code.Name = "lbl_code";
            this.lbl_code.Size = new System.Drawing.Size(272, 16);
            this.lbl_code.TabIndex = 0;
            this.lbl_code.Text = "Enter Authorization Code";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(40, 261);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(230, 46);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txt_Auth_code_Bridge
            // 
            this.txt_Auth_code_Bridge.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Auth_code_Bridge.Location = new System.Drawing.Point(10, 172);
            this.txt_Auth_code_Bridge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Auth_code_Bridge.Name = "txt_Auth_code_Bridge";
            this.txt_Auth_code_Bridge.Size = new System.Drawing.Size(291, 22);
            this.txt_Auth_code_Bridge.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 399;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Astra;
            this.panel1.Location = new System.Drawing.Point(21, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 85);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_strucrure);
            this.groupBox1.Controls.Add(this.chk_bridge);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 346);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(268, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // chk_strucrure
            // 
            this.chk_strucrure.AutoSize = true;
            this.chk_strucrure.Location = new System.Drawing.Point(148, 17);
            this.chk_strucrure.Name = "chk_strucrure";
            this.chk_strucrure.Size = new System.Drawing.Size(113, 22);
            this.chk_strucrure.TabIndex = 1;
            this.chk_strucrure.Text = "Structures";
            this.chk_strucrure.UseVisualStyleBackColor = true;
            // 
            // chk_bridge
            // 
            this.chk_bridge.AutoSize = true;
            this.chk_bridge.Location = new System.Drawing.Point(18, 17);
            this.chk_bridge.Name = "chk_bridge";
            this.chk_bridge.Size = new System.Drawing.Size(88, 22);
            this.chk_bridge.TabIndex = 1;
            this.chk_bridge.Text = "Bridges";
            this.chk_bridge.UseVisualStyleBackColor = true;
            // 
            // txt_Auth_code_Building
            // 
            this.txt_Auth_code_Building.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Auth_code_Building.Location = new System.Drawing.Point(10, 233);
            this.txt_Auth_code_Building.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Auth_code_Building.Name = "txt_Auth_code_Building";
            this.txt_Auth_code_Building.Size = new System.Drawing.Size(291, 22);
            this.txt_Auth_code_Building.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 214);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASTRA Pro Structure Design";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "ASTRA Pro Bridge Design";
            // 
            // frmLockedVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 313);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Auth_code_Building);
            this.Controls.Add(this.lbl_code);
            this.Controls.Add(this.txt_Auth_code_Bridge);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmLockedVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Pro";
            this.Load += new System.EventHandler(this.frmCheckHasp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_code;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txt_Auth_code_Bridge;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_strucrure;
        private System.Windows.Forms.CheckBox chk_bridge;
        private System.Windows.Forms.TextBox txt_Auth_code_Building;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }


}