namespace AstraFunctionOne
{
    partial class frmSupport
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodeNo = new System.Windows.Forms.TextBox();
            this.grbDegOfFrdm = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkYr = new System.Windows.Forms.CheckBox();
            this.chkZr = new System.Windows.Forms.CheckBox();
            this.chkZt = new System.Windows.Forms.CheckBox();
            this.chkXr = new System.Windows.Forms.CheckBox();
            this.chkYt = new System.Windows.Forms.CheckBox();
            this.chkXt = new System.Windows.Forms.CheckBox();
            this.grbSupportType = new System.Windows.Forms.GroupBox();
            this.rbtnPinned = new System.Windows.Forms.RadioButton();
            this.rbtnFixed = new System.Windows.Forms.RadioButton();
            this.rbtnSupportType = new System.Windows.Forms.RadioButton();
            this.rbtnDegreeOfFreedom = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbxNodeDesc = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbDegOfFrdm.SuspendLayout();
            this.grbSupportType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(197, 269);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add / Save";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(155, 306);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Node No";
            // 
            // txtNodeNo
            // 
            this.txtNodeNo.Location = new System.Drawing.Point(69, 28);
            this.txtNodeNo.Name = "txtNodeNo";
            this.txtNodeNo.Size = new System.Drawing.Size(100, 20);
            this.txtNodeNo.TabIndex = 3;
            this.txtNodeNo.TextChanged += new System.EventHandler(this.txtNodeNo_TextChanged);
            this.txtNodeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeNo_KeyPress);
            // 
            // grbDegOfFrdm
            // 
            this.grbDegOfFrdm.Controls.Add(this.label7);
            this.grbDegOfFrdm.Controls.Add(this.label6);
            this.grbDegOfFrdm.Controls.Add(this.chkYr);
            this.grbDegOfFrdm.Controls.Add(this.chkZr);
            this.grbDegOfFrdm.Controls.Add(this.chkZt);
            this.grbDegOfFrdm.Controls.Add(this.chkXr);
            this.grbDegOfFrdm.Controls.Add(this.chkYt);
            this.grbDegOfFrdm.Controls.Add(this.chkXt);
            this.grbDegOfFrdm.Enabled = false;
            this.grbDegOfFrdm.Location = new System.Drawing.Point(134, 45);
            this.grbDegOfFrdm.Name = "grbDegOfFrdm";
            this.grbDegOfFrdm.Size = new System.Drawing.Size(120, 188);
            this.grbDegOfFrdm.TabIndex = 4;
            this.grbDegOfFrdm.TabStop = false;
            this.grbDegOfFrdm.Text = "Degree of Freedom";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "Restrained D-O-F\r\n(\'Tick\' to Restrain)";
            // 
            // chkYr
            // 
            this.chkYr.AutoSize = true;
            this.chkYr.Checked = true;
            this.chkYr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYr.Location = new System.Drawing.Point(6, 141);
            this.chkYr.Name = "chkYr";
            this.chkYr.Size = new System.Drawing.Size(71, 17);
            this.chkYr.TabIndex = 5;
            this.chkYr.Text = "Y-rotation";
            this.chkYr.UseVisualStyleBackColor = true;
            // 
            // chkZr
            // 
            this.chkZr.AutoSize = true;
            this.chkZr.Checked = true;
            this.chkZr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZr.Location = new System.Drawing.Point(6, 165);
            this.chkZr.Name = "chkZr";
            this.chkZr.Size = new System.Drawing.Size(71, 17);
            this.chkZr.TabIndex = 4;
            this.chkZr.Text = "Z-rotation";
            this.chkZr.UseVisualStyleBackColor = true;
            // 
            // chkZt
            // 
            this.chkZt.AutoSize = true;
            this.chkZt.Checked = true;
            this.chkZt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZt.Location = new System.Drawing.Point(6, 93);
            this.chkZt.Name = "chkZt";
            this.chkZt.Size = new System.Drawing.Size(84, 17);
            this.chkZt.TabIndex = 3;
            this.chkZt.Text = "Z-translation";
            this.chkZt.UseVisualStyleBackColor = true;
            // 
            // chkXr
            // 
            this.chkXr.AutoSize = true;
            this.chkXr.Checked = true;
            this.chkXr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXr.Location = new System.Drawing.Point(6, 118);
            this.chkXr.Name = "chkXr";
            this.chkXr.Size = new System.Drawing.Size(71, 17);
            this.chkXr.TabIndex = 2;
            this.chkXr.Text = "X-rotation";
            this.chkXr.UseVisualStyleBackColor = true;
            // 
            // chkYt
            // 
            this.chkYt.AutoSize = true;
            this.chkYt.Checked = true;
            this.chkYt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYt.Location = new System.Drawing.Point(6, 69);
            this.chkYt.Name = "chkYt";
            this.chkYt.Size = new System.Drawing.Size(84, 17);
            this.chkYt.TabIndex = 1;
            this.chkYt.Text = "Y-translation";
            this.chkYt.UseVisualStyleBackColor = true;
            // 
            // chkXt
            // 
            this.chkXt.AutoSize = true;
            this.chkXt.Checked = true;
            this.chkXt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXt.Location = new System.Drawing.Point(6, 46);
            this.chkXt.Name = "chkXt";
            this.chkXt.Size = new System.Drawing.Size(84, 17);
            this.chkXt.TabIndex = 0;
            this.chkXt.Text = "X-translation";
            this.chkXt.UseVisualStyleBackColor = true;
            // 
            // grbSupportType
            // 
            this.grbSupportType.Controls.Add(this.rbtnPinned);
            this.grbSupportType.Controls.Add(this.rbtnFixed);
            this.grbSupportType.Enabled = false;
            this.grbSupportType.Location = new System.Drawing.Point(13, 45);
            this.grbSupportType.Name = "grbSupportType";
            this.grbSupportType.Size = new System.Drawing.Size(111, 188);
            this.grbSupportType.TabIndex = 5;
            this.grbSupportType.TabStop = false;
            this.grbSupportType.Text = "Support Type";
            // 
            // rbtnPinned
            // 
            this.rbtnPinned.AutoSize = true;
            this.rbtnPinned.Location = new System.Drawing.Point(22, 43);
            this.rbtnPinned.Name = "rbtnPinned";
            this.rbtnPinned.Size = new System.Drawing.Size(58, 17);
            this.rbtnPinned.TabIndex = 1;
            this.rbtnPinned.TabStop = true;
            this.rbtnPinned.Text = "Pinned";
            this.rbtnPinned.UseVisualStyleBackColor = true;
            this.rbtnPinned.CheckedChanged += new System.EventHandler(this.rbtnPinned_CheckedChanged);
            // 
            // rbtnFixed
            // 
            this.rbtnFixed.AutoSize = true;
            this.rbtnFixed.Location = new System.Drawing.Point(22, 20);
            this.rbtnFixed.Name = "rbtnFixed";
            this.rbtnFixed.Size = new System.Drawing.Size(50, 17);
            this.rbtnFixed.TabIndex = 0;
            this.rbtnFixed.TabStop = true;
            this.rbtnFixed.Text = "Fixed";
            this.rbtnFixed.UseVisualStyleBackColor = true;
            this.rbtnFixed.CheckedChanged += new System.EventHandler(this.rbtnFixed_CheckedChanged);
            // 
            // rbtnSupportType
            // 
            this.rbtnSupportType.AutoSize = true;
            this.rbtnSupportType.Location = new System.Drawing.Point(13, 19);
            this.rbtnSupportType.Name = "rbtnSupportType";
            this.rbtnSupportType.Size = new System.Drawing.Size(89, 17);
            this.rbtnSupportType.TabIndex = 3;
            this.rbtnSupportType.TabStop = true;
            this.rbtnSupportType.Text = "Support Type";
            this.rbtnSupportType.UseVisualStyleBackColor = true;
            this.rbtnSupportType.CheckedChanged += new System.EventHandler(this.rbtnSupportType_CheckedChanged);
            // 
            // rbtnDegreeOfFreedom
            // 
            this.rbtnDegreeOfFreedom.AutoSize = true;
            this.rbtnDegreeOfFreedom.Location = new System.Drawing.Point(134, 19);
            this.rbtnDegreeOfFreedom.Name = "rbtnDegreeOfFreedom";
            this.rbtnDegreeOfFreedom.Size = new System.Drawing.Size(118, 17);
            this.rbtnDegreeOfFreedom.TabIndex = 4;
            this.rbtnDegreeOfFreedom.TabStop = true;
            this.rbtnDegreeOfFreedom.Text = "Degree Of Freedom";
            this.rbtnDegreeOfFreedom.UseVisualStyleBackColor = true;
            this.rbtnDegreeOfFreedom.CheckedChanged += new System.EventHandler(this.rbtnDegreeOfFreedom_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnDegreeOfFreedom);
            this.groupBox2.Controls.Add(this.grbSupportType);
            this.groupBox2.Controls.Add(this.rbtnSupportType);
            this.groupBox2.Controls.Add(this.grbDegOfFrdm);
            this.groupBox2.Location = new System.Drawing.Point(179, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 251);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Selection";
            // 
            // lbxNodeDesc
            // 
            this.lbxNodeDesc.FormattingEnabled = true;
            this.lbxNodeDesc.Items.AddRange(new object[] {
            "NODE\tSUPPORT_TYPE",
            "-----------------------------------------------------------"});
            this.lbxNodeDesc.Location = new System.Drawing.Point(19, 71);
            this.lbxNodeDesc.Name = "lbxNodeDesc";
            this.lbxNodeDesc.Size = new System.Drawing.Size(149, 186);
            this.lbxNodeDesc.TabIndex = 7;
            this.lbxNodeDesc.SelectedIndexChanged += new System.EventHandler(this.lbxNodeDesc_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(291, 269);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.lbxNodeDesc);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtNodeNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 297);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(248, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 332);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSupport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Support";
            this.Load += new System.EventHandler(this.frmSupport_Load);
            this.grbDegOfFrdm.ResumeLayout(false);
            this.grbDegOfFrdm.PerformLayout();
            this.grbSupportType.ResumeLayout(false);
            this.grbSupportType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNodeNo;
        private System.Windows.Forms.GroupBox grbDegOfFrdm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkYr;
        private System.Windows.Forms.CheckBox chkZr;
        private System.Windows.Forms.CheckBox chkZt;
        private System.Windows.Forms.CheckBox chkXr;
        private System.Windows.Forms.CheckBox chkYt;
        private System.Windows.Forms.CheckBox chkXt;
        private System.Windows.Forms.GroupBox grbSupportType;
        private System.Windows.Forms.RadioButton rbtnPinned;
        private System.Windows.Forms.RadioButton rbtnFixed;
        private System.Windows.Forms.RadioButton rbtnSupportType;
        private System.Windows.Forms.RadioButton rbtnDegreeOfFreedom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbxNodeDesc;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
    }
}