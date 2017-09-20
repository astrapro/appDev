namespace AstraAccess.SAP_Forms
{
    partial class frm_Solid_Loads
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Solid_Loads));
            this.btn_close = new System.Windows.Forms.Button();
            this.grb_Member = new System.Windows.Forms.GroupBox();
            this.cmb_face_no = new System.Windows.Forms.ComboBox();
            this.cmb_LT = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_Y = new System.Windows.Forms.TextBox();
            this.txt_load_no = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_P = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grb_Member.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(233, 147);
            this.btn_close.Margin = new System.Windows.Forms.Padding(19, 3, 19, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(128, 37);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.cmb_face_no);
            this.grb_Member.Controls.Add(this.cmb_LT);
            this.grb_Member.Controls.Add(this.label1);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_Y);
            this.grb_Member.Controls.Add(this.txt_load_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.label3);
            this.grb_Member.Controls.Add(this.txt_P);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(14, 12);
            this.grb_Member.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.grb_Member.Size = new System.Drawing.Size(449, 129);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "Distributed Surface Loads";
            // 
            // cmb_face_no
            // 
            this.cmb_face_no.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_face_no.FormattingEnabled = true;
            this.cmb_face_no.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cmb_face_no.Location = new System.Drawing.Point(368, 102);
            this.cmb_face_no.Name = "cmb_face_no";
            this.cmb_face_no.Size = new System.Drawing.Size(63, 21);
            this.cmb_face_no.TabIndex = 4;
            // 
            // cmb_LT
            // 
            this.cmb_LT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LT.FormattingEnabled = true;
            this.cmb_LT.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmb_LT.Location = new System.Drawing.Point(368, 22);
            this.cmb_LT.Name = "cmb_LT";
            this.cmb_LT.Size = new System.Drawing.Size(63, 21);
            this.cmb_LT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Y";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(14, 69);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "P";
            // 
            // txt_Y
            // 
            this.txt_Y.Location = new System.Drawing.Point(303, 66);
            this.txt_Y.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(128, 21);
            this.txt_Y.TabIndex = 3;
            this.txt_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_load_no
            // 
            this.txt_load_no.Location = new System.Drawing.Point(176, 22);
            this.txt_load_no.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_load_no.Name = "txt_load_no";
            this.txt_load_no.Size = new System.Drawing.Size(63, 21);
            this.txt_load_no.TabIndex = 0;
            this.txt_load_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(14, 25);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(152, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Load set Identification No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Element face number on which surface load acts.";
            // 
            // txt_P
            // 
            this.txt_P.Location = new System.Drawing.Point(38, 66);
            this.txt_P.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_P.Name = "txt_P";
            this.txt_P.Size = new System.Drawing.Size(128, 21);
            this.txt_P.TabIndex = 2;
            this.txt_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(269, 25);
            this.label33.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Load Type (LT)";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(94, 147);
            this.btn_add.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(126, 37);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "ADD";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(14, 187);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(449, 143);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // frm_Solid_Loads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 340);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Solid_Loads";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distributed Surface Loads";
            this.Load += new System.EventHandler(this.frm_Solid_Loads_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.ComboBox cmb_face_no;
        private System.Windows.Forms.ComboBox cmb_LT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_Y;
        private System.Windows.Forms.TextBox txt_load_no;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_P;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label2;
    }
}