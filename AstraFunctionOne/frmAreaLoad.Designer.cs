namespace AstraFunctionOne
{
    partial class frmAreaLoad
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoadcase = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.cmbMassUnit = new System.Windows.Forms.ComboBox();
            this.txtFy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grbAppliedLoad = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnElmtPrevious = new System.Windows.Forms.Button();
            this.rbtnLoadElmtNo = new System.Windows.Forms.RadioButton();
            this.btnElmtApply = new System.Windows.Forms.Button();
            this.txtElementNos = new System.Windows.Forms.TextBox();
            this.btnElmtRangePrevious = new System.Windows.Forms.Button();
            this.rbtnLoadElmtRange = new System.Windows.Forms.RadioButton();
            this.btnElmtRangeApply = new System.Windows.Forms.Button();
            this.txtEndElement = new System.Windows.Forms.TextBox();
            this.rbtnLoadAllElmt = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.txtStartElement = new System.Windows.Forms.TextBox();
            this.btnDeleteLoad = new System.Windows.Forms.Button();
            this.btnPreviousLoad = new System.Windows.Forms.Button();
            this.btnNextLoad = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbAppliedLoad.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Load Case";
            // 
            // txtLoadcase
            // 
            this.txtLoadcase.Location = new System.Drawing.Point(76, 20);
            this.txtLoadcase.Name = "txtLoadcase";
            this.txtLoadcase.Size = new System.Drawing.Size(100, 20);
            this.txtLoadcase.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(212, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(389, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 121;
            this.label14.Text = "Length Unit";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(209, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 120;
            this.label13.Text = "Mass Unit";
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "M",
            "CM",
            "MM",
            "Yds",
            "Ft",
            "Inch"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(453, 17);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbLengthUnit.TabIndex = 119;
            // 
            // cmbMassUnit
            // 
            this.cmbMassUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMassUnit.FormattingEnabled = true;
            this.cmbMassUnit.Items.AddRange(new object[] {
            "Kg",
            "KN",
            "MTon",
            "N",
            "gms",
            "lbs",
            "KIP"});
            this.cmbMassUnit.Location = new System.Drawing.Point(273, 19);
            this.cmbMassUnit.Name = "cmbMassUnit";
            this.cmbMassUnit.Size = new System.Drawing.Size(100, 21);
            this.cmbMassUnit.TabIndex = 118;
            // 
            // txtFy
            // 
            this.txtFy.Location = new System.Drawing.Point(68, 13);
            this.txtFy.Name = "txtFy";
            this.txtFy.Size = new System.Drawing.Size(100, 20);
            this.txtFy.TabIndex = 123;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Area Load";
            // 
            // grbAppliedLoad
            // 
            this.grbAppliedLoad.Controls.Add(this.groupBox4);
            this.grbAppliedLoad.Controls.Add(this.txtFy);
            this.grbAppliedLoad.Controls.Add(this.btnDeleteLoad);
            this.grbAppliedLoad.Controls.Add(this.label2);
            this.grbAppliedLoad.Controls.Add(this.btnPreviousLoad);
            this.grbAppliedLoad.Controls.Add(this.btnNextLoad);
            this.grbAppliedLoad.Location = new System.Drawing.Point(15, 48);
            this.grbAppliedLoad.Name = "grbAppliedLoad";
            this.grbAppliedLoad.Size = new System.Drawing.Size(569, 185);
            this.grbAppliedLoad.TabIndex = 124;
            this.grbAppliedLoad.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnElmtPrevious);
            this.groupBox4.Controls.Add(this.rbtnLoadElmtNo);
            this.groupBox4.Controls.Add(this.btnElmtApply);
            this.groupBox4.Controls.Add(this.txtElementNos);
            this.groupBox4.Controls.Add(this.btnElmtRangePrevious);
            this.groupBox4.Controls.Add(this.rbtnLoadElmtRange);
            this.groupBox4.Controls.Add(this.btnElmtRangeApply);
            this.groupBox4.Controls.Add(this.txtEndElement);
            this.groupBox4.Controls.Add(this.rbtnLoadAllElmt);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtStartElement);
            this.groupBox4.Location = new System.Drawing.Point(6, 44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(557, 104);
            this.groupBox4.TabIndex = 74;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Apply on Elements";
            // 
            // btnElmtPrevious
            // 
            this.btnElmtPrevious.Location = new System.Drawing.Point(471, 48);
            this.btnElmtPrevious.Name = "btnElmtPrevious";
            this.btnElmtPrevious.Size = new System.Drawing.Size(80, 20);
            this.btnElmtPrevious.TabIndex = 78;
            this.btnElmtPrevious.Text = "Previous";
            this.btnElmtPrevious.UseVisualStyleBackColor = true;
            // 
            // rbtnLoadElmtNo
            // 
            this.rbtnLoadElmtNo.AutoSize = true;
            this.rbtnLoadElmtNo.Checked = true;
            this.rbtnLoadElmtNo.Location = new System.Drawing.Point(52, 50);
            this.rbtnLoadElmtNo.Name = "rbtnLoadElmtNo";
            this.rbtnLoadElmtNo.Size = new System.Drawing.Size(80, 17);
            this.rbtnLoadElmtNo.TabIndex = 73;
            this.rbtnLoadElmtNo.TabStop = true;
            this.rbtnLoadElmtNo.Text = "Element No";
            this.rbtnLoadElmtNo.UseVisualStyleBackColor = true;
            // 
            // btnElmtApply
            // 
            this.btnElmtApply.Location = new System.Drawing.Point(377, 49);
            this.btnElmtApply.Name = "btnElmtApply";
            this.btnElmtApply.Size = new System.Drawing.Size(88, 20);
            this.btnElmtApply.TabIndex = 75;
            this.btnElmtApply.Text = "Apply / Next";
            this.btnElmtApply.UseVisualStyleBackColor = true;
            // 
            // txtElementNos
            // 
            this.txtElementNos.Location = new System.Drawing.Point(152, 49);
            this.txtElementNos.Name = "txtElementNos";
            this.txtElementNos.Size = new System.Drawing.Size(213, 20);
            this.txtElementNos.TabIndex = 74;
            // 
            // btnElmtRangePrevious
            // 
            this.btnElmtRangePrevious.Enabled = false;
            this.btnElmtRangePrevious.Location = new System.Drawing.Point(471, 77);
            this.btnElmtRangePrevious.Name = "btnElmtRangePrevious";
            this.btnElmtRangePrevious.Size = new System.Drawing.Size(80, 20);
            this.btnElmtRangePrevious.TabIndex = 72;
            this.btnElmtRangePrevious.Text = "Previous";
            this.btnElmtRangePrevious.UseVisualStyleBackColor = true;
            // 
            // rbtnLoadElmtRange
            // 
            this.rbtnLoadElmtRange.AutoSize = true;
            this.rbtnLoadElmtRange.Location = new System.Drawing.Point(52, 81);
            this.rbtnLoadElmtRange.Name = "rbtnLoadElmtRange";
            this.rbtnLoadElmtRange.Size = new System.Drawing.Size(98, 17);
            this.rbtnLoadElmtRange.TabIndex = 1;
            this.rbtnLoadElmtRange.Text = "Element Range";
            this.rbtnLoadElmtRange.UseVisualStyleBackColor = true;
            // 
            // btnElmtRangeApply
            // 
            this.btnElmtRangeApply.Enabled = false;
            this.btnElmtRangeApply.Location = new System.Drawing.Point(377, 78);
            this.btnElmtRangeApply.Name = "btnElmtRangeApply";
            this.btnElmtRangeApply.Size = new System.Drawing.Size(88, 20);
            this.btnElmtRangeApply.TabIndex = 69;
            this.btnElmtRangeApply.Text = "Apply / Next";
            this.btnElmtRangeApply.UseVisualStyleBackColor = true;
            // 
            // txtEndElement
            // 
            this.txtEndElement.Enabled = false;
            this.txtEndElement.Location = new System.Drawing.Point(297, 79);
            this.txtEndElement.Name = "txtEndElement";
            this.txtEndElement.Size = new System.Drawing.Size(68, 20);
            this.txtEndElement.TabIndex = 71;
            // 
            // rbtnLoadAllElmt
            // 
            this.rbtnLoadAllElmt.AutoSize = true;
            this.rbtnLoadAllElmt.Location = new System.Drawing.Point(52, 21);
            this.rbtnLoadAllElmt.Name = "rbtnLoadAllElmt";
            this.rbtnLoadAllElmt.Size = new System.Drawing.Size(82, 17);
            this.rbtnLoadAllElmt.TabIndex = 0;
            this.rbtnLoadAllElmt.Text = "All Elements";
            this.rbtnLoadAllElmt.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(261, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 70;
            this.label12.Text = "TO";
            // 
            // txtStartElement
            // 
            this.txtStartElement.Enabled = false;
            this.txtStartElement.Location = new System.Drawing.Point(152, 79);
            this.txtStartElement.Name = "txtStartElement";
            this.txtStartElement.Size = new System.Drawing.Size(94, 20);
            this.txtStartElement.TabIndex = 68;
            // 
            // btnDeleteLoad
            // 
            this.btnDeleteLoad.Location = new System.Drawing.Point(361, 154);
            this.btnDeleteLoad.Name = "btnDeleteLoad";
            this.btnDeleteLoad.Size = new System.Drawing.Size(96, 23);
            this.btnDeleteLoad.TabIndex = 122;
            this.btnDeleteLoad.Text = "Delete Load";
            this.btnDeleteLoad.UseVisualStyleBackColor = true;
            // 
            // btnPreviousLoad
            // 
            this.btnPreviousLoad.Location = new System.Drawing.Point(230, 154);
            this.btnPreviousLoad.Name = "btnPreviousLoad";
            this.btnPreviousLoad.Size = new System.Drawing.Size(96, 23);
            this.btnPreviousLoad.TabIndex = 121;
            this.btnPreviousLoad.Text = "Previous Load";
            this.btnPreviousLoad.UseVisualStyleBackColor = true;
            // 
            // btnNextLoad
            // 
            this.btnNextLoad.Location = new System.Drawing.Point(107, 154);
            this.btnNextLoad.Name = "btnNextLoad";
            this.btnNextLoad.Size = new System.Drawing.Size(96, 23);
            this.btnNextLoad.TabIndex = 110;
            this.btnNextLoad.Text = "Next Load";
            this.btnNextLoad.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(318, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 125;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmAreaLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 299);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grbAppliedLoad);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbLengthUnit);
            this.Controls.Add(this.cmbMassUnit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtLoadcase);
            this.Controls.Add(this.label1);
            this.Name = "frmAreaLoad";
            this.Text = "Area Load";
            this.Load += new System.EventHandler(this.frmAreaLoad_Load);
            this.grbAppliedLoad.ResumeLayout(false);
            this.grbAppliedLoad.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoadcase;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.ComboBox cmbMassUnit;
        private System.Windows.Forms.TextBox txtFy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grbAppliedLoad;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnElmtPrevious;
        private System.Windows.Forms.RadioButton rbtnLoadElmtNo;
        private System.Windows.Forms.Button btnElmtApply;
        private System.Windows.Forms.TextBox txtElementNos;
        private System.Windows.Forms.Button btnElmtRangePrevious;
        private System.Windows.Forms.RadioButton rbtnLoadElmtRange;
        private System.Windows.Forms.Button btnElmtRangeApply;
        private System.Windows.Forms.TextBox txtEndElement;
        private System.Windows.Forms.RadioButton rbtnLoadAllElmt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtStartElement;
        private System.Windows.Forms.Button btnDeleteLoad;
        private System.Windows.Forms.Button btnPreviousLoad;
        private System.Windows.Forms.Button btnNextLoad;
        private System.Windows.Forms.Button btnCancel;
    }
}