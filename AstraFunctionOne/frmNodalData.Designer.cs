namespace AstraFunctionOne
{
    partial class frmNodalData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIncrOn = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNodeIncr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtZIncr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtYIncr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtXIncr = new System.Windows.Forms.TextBox();
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodeNo = new System.Windows.Forms.TextBox();
            this.grbCylindricalAxis = new System.Windows.Forms.GroupBox();
            this.rbtnZAxis = new System.Windows.Forms.RadioButton();
            this.rbtnYAxis = new System.Windows.Forms.RadioButton();
            this.rbtnXAxis = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkYr = new System.Windows.Forms.CheckBox();
            this.chkZr = new System.Windows.Forms.CheckBox();
            this.chkZt = new System.Windows.Forms.CheckBox();
            this.chkXr = new System.Windows.Forms.CheckBox();
            this.chkYt = new System.Windows.Forms.CheckBox();
            this.chkXt = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnGlobalCylindrical = new System.Windows.Forms.RadioButton();
            this.rbtnGlobalCartesian = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.epAstra = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.grbCylindricalAxis.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIncrOn);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtNodeIncr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtZIncr);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtYIncr);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtXIncr);
            this.groupBox1.Controls.Add(this.cmbLengthUnit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNodeNo);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nodal Coordinates";
            // 
            // chkIncrOn
            // 
            this.chkIncrOn.AutoSize = true;
            this.chkIncrOn.Checked = true;
            this.chkIncrOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrOn.Location = new System.Drawing.Point(286, 11);
            this.chkIncrOn.Name = "chkIncrOn";
            this.chkIncrOn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkIncrOn.Size = new System.Drawing.Size(98, 17);
            this.chkIncrOn.TabIndex = 12;
            this.chkIncrOn.Text = "Increament ON";
            this.chkIncrOn.UseVisualStyleBackColor = true;
            this.chkIncrOn.CheckedChanged += new System.EventHandler(this.chkIncrOn_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Node [incr]";
            // 
            // txtNodeIncr
            // 
            this.txtNodeIncr.Location = new System.Drawing.Point(65, 63);
            this.txtNodeIncr.Name = "txtNodeIncr";
            this.txtNodeIncr.Size = new System.Drawing.Size(52, 20);
            this.txtNodeIncr.TabIndex = 16;
            this.txtNodeIncr.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(295, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Z[incr]";
            // 
            // txtZIncr
            // 
            this.txtZIncr.Location = new System.Drawing.Point(332, 99);
            this.txtZIncr.Name = "txtZIncr";
            this.txtZIncr.Size = new System.Drawing.Size(52, 20);
            this.txtZIncr.TabIndex = 14;
            this.txtZIncr.Text = "0.0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(295, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Y[incr]";
            // 
            // txtYIncr
            // 
            this.txtYIncr.Location = new System.Drawing.Point(332, 66);
            this.txtYIncr.Name = "txtYIncr";
            this.txtYIncr.Size = new System.Drawing.Size(52, 20);
            this.txtYIncr.TabIndex = 12;
            this.txtYIncr.Text = "0.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(295, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "X[incr]";
            // 
            // txtXIncr
            // 
            this.txtXIncr.Location = new System.Drawing.Point(332, 30);
            this.txtXIncr.Name = "txtXIncr";
            this.txtXIncr.Size = new System.Drawing.Size(52, 20);
            this.txtXIncr.TabIndex = 10;
            this.txtXIncr.Text = "0.0";
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "M",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(65, 98);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(85, 21);
            this.cmbLengthUnit.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "Length\r\nUnit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Z=";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(204, 99);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(74, 20);
            this.txtZ.TabIndex = 3;
            this.txtZ.Text = "0.0";
            this.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtZ.Leave += new System.EventHandler(this.txtX_Leave);
            this.txtZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZ_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y=";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(204, 66);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(74, 20);
            this.txtY.TabIndex = 2;
            this.txtY.Text = "0.0";
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY.Leave += new System.EventHandler(this.txtX_Leave);
            this.txtY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtY_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X =";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(204, 30);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(74, 20);
            this.txtX.TabIndex = 1;
            this.txtX.Text = "0.0";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtX.Leave += new System.EventHandler(this.txtX_Leave);
            this.txtX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Node No:";
            // 
            // txtNodeNo
            // 
            this.txtNodeNo.Location = new System.Drawing.Point(65, 30);
            this.txtNodeNo.Name = "txtNodeNo";
            this.txtNodeNo.Size = new System.Drawing.Size(85, 20);
            this.txtNodeNo.TabIndex = 0;
            this.txtNodeNo.Text = "1";
            this.txtNodeNo.TextChanged += new System.EventHandler(this.txtNodeNo_TextChanged);
            this.txtNodeNo.Validated += new System.EventHandler(this.txtX_Validated);
            this.txtNodeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNodeNo_KeyDown);
            this.txtNodeNo.Leave += new System.EventHandler(this.txtX_Leave);
            this.txtNodeNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtX_Validating);
            // 
            // grbCylindricalAxis
            // 
            this.grbCylindricalAxis.Controls.Add(this.rbtnZAxis);
            this.grbCylindricalAxis.Controls.Add(this.rbtnYAxis);
            this.grbCylindricalAxis.Controls.Add(this.rbtnXAxis);
            this.grbCylindricalAxis.Enabled = false;
            this.grbCylindricalAxis.Location = new System.Drawing.Point(203, 149);
            this.grbCylindricalAxis.Name = "grbCylindricalAxis";
            this.grbCylindricalAxis.Size = new System.Drawing.Size(137, 106);
            this.grbCylindricalAxis.TabIndex = 1;
            this.grbCylindricalAxis.TabStop = false;
            this.grbCylindricalAxis.Text = "Cylindrical Axis";
            // 
            // rbtnZAxis
            // 
            this.rbtnZAxis.AutoSize = true;
            this.rbtnZAxis.Location = new System.Drawing.Point(6, 75);
            this.rbtnZAxis.Name = "rbtnZAxis";
            this.rbtnZAxis.Size = new System.Drawing.Size(54, 17);
            this.rbtnZAxis.TabIndex = 6;
            this.rbtnZAxis.TabStop = true;
            this.rbtnZAxis.Text = "Z-Axis";
            this.rbtnZAxis.UseVisualStyleBackColor = true;
            // 
            // rbtnYAxis
            // 
            this.rbtnYAxis.AutoSize = true;
            this.rbtnYAxis.Location = new System.Drawing.Point(6, 51);
            this.rbtnYAxis.Name = "rbtnYAxis";
            this.rbtnYAxis.Size = new System.Drawing.Size(54, 17);
            this.rbtnYAxis.TabIndex = 5;
            this.rbtnYAxis.TabStop = true;
            this.rbtnYAxis.Text = "Y-Axis";
            this.rbtnYAxis.UseVisualStyleBackColor = true;
            // 
            // rbtnXAxis
            // 
            this.rbtnXAxis.AutoSize = true;
            this.rbtnXAxis.Location = new System.Drawing.Point(6, 25);
            this.rbtnXAxis.Name = "rbtnXAxis";
            this.rbtnXAxis.Size = new System.Drawing.Size(54, 17);
            this.rbtnXAxis.TabIndex = 4;
            this.rbtnXAxis.TabStop = true;
            this.rbtnXAxis.Text = "X-Axis";
            this.rbtnXAxis.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.chkYr);
            this.groupBox5.Controls.Add(this.chkZr);
            this.groupBox5.Controls.Add(this.chkZt);
            this.groupBox5.Controls.Add(this.chkXr);
            this.groupBox5.Controls.Add(this.chkYt);
            this.groupBox5.Controls.Add(this.chkXt);
            this.groupBox5.Location = new System.Drawing.Point(421, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(120, 246);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Degree of Freedom";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
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
            this.chkYr.Location = new System.Drawing.Point(6, 165);
            this.chkYr.Name = "chkYr";
            this.chkYr.Size = new System.Drawing.Size(71, 17);
            this.chkYr.TabIndex = 4;
            this.chkYr.Text = "Y-rotation";
            this.chkYr.UseVisualStyleBackColor = true;
            // 
            // chkZr
            // 
            this.chkZr.AutoSize = true;
            this.chkZr.Checked = true;
            this.chkZr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZr.Location = new System.Drawing.Point(6, 189);
            this.chkZr.Name = "chkZr";
            this.chkZr.Size = new System.Drawing.Size(71, 17);
            this.chkZr.TabIndex = 5;
            this.chkZr.Text = "Z-rotation";
            this.chkZr.UseVisualStyleBackColor = true;
            // 
            // chkZt
            // 
            this.chkZt.AutoSize = true;
            this.chkZt.Checked = true;
            this.chkZt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZt.Location = new System.Drawing.Point(6, 117);
            this.chkZt.Name = "chkZt";
            this.chkZt.Size = new System.Drawing.Size(84, 17);
            this.chkZt.TabIndex = 2;
            this.chkZt.Text = "Z-translation";
            this.chkZt.UseVisualStyleBackColor = true;
            // 
            // chkXr
            // 
            this.chkXr.AutoSize = true;
            this.chkXr.Checked = true;
            this.chkXr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXr.Location = new System.Drawing.Point(6, 142);
            this.chkXr.Name = "chkXr";
            this.chkXr.Size = new System.Drawing.Size(71, 17);
            this.chkXr.TabIndex = 3;
            this.chkXr.Text = "X-rotation";
            this.chkXr.UseVisualStyleBackColor = true;
            // 
            // chkYt
            // 
            this.chkYt.AutoSize = true;
            this.chkYt.Checked = true;
            this.chkYt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYt.Location = new System.Drawing.Point(6, 93);
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
            this.chkXt.Location = new System.Drawing.Point(6, 70);
            this.chkXt.Name = "chkXt";
            this.chkXt.Size = new System.Drawing.Size(84, 17);
            this.chkXt.TabIndex = 0;
            this.chkXt.Text = "X-translation";
            this.chkXt.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnGlobalCylindrical);
            this.groupBox4.Controls.Add(this.rbtnGlobalCartesian);
            this.groupBox4.Location = new System.Drawing.Point(9, 155);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(132, 100);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Coordinate System";
            // 
            // rbtnGlobalCylindrical
            // 
            this.rbtnGlobalCylindrical.AutoSize = true;
            this.rbtnGlobalCylindrical.Location = new System.Drawing.Point(6, 54);
            this.rbtnGlobalCylindrical.Name = "rbtnGlobalCylindrical";
            this.rbtnGlobalCylindrical.Size = new System.Drawing.Size(105, 17);
            this.rbtnGlobalCylindrical.TabIndex = 1;
            this.rbtnGlobalCylindrical.Text = "Global Cylindrical";
            this.rbtnGlobalCylindrical.UseVisualStyleBackColor = true;
            this.rbtnGlobalCylindrical.CheckedChanged += new System.EventHandler(this.rbtnGlobalCylindrical_CheckedChanged);
            // 
            // rbtnGlobalCartesian
            // 
            this.rbtnGlobalCartesian.AutoSize = true;
            this.rbtnGlobalCartesian.Checked = true;
            this.rbtnGlobalCartesian.Location = new System.Drawing.Point(6, 27);
            this.rbtnGlobalCartesian.Name = "rbtnGlobalCartesian";
            this.rbtnGlobalCartesian.Size = new System.Drawing.Size(102, 17);
            this.rbtnGlobalCartesian.TabIndex = 0;
            this.rbtnGlobalCartesian.TabStop = true;
            this.rbtnGlobalCartesian.Text = "Global Cartesian";
            this.rbtnGlobalCartesian.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(132, 271);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(223, 271);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(316, 271);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(163, 300);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(91, 23);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "OK";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblPosition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPosition.Location = new System.Drawing.Point(0, 326);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(540, 13);
            this.lblPosition.TabIndex = 18;
            this.lblPosition.Text = "Show Status";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // epAstra
            // 
            this.epAstra.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnFinish);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnPrevious);
            this.groupBox2.Controls.Add(this.btnNext);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.grbCylindricalAxis);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(540, 326);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // frmNodalData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 339);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblPosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNodalData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nodal Data";
            this.Load += new System.EventHandler(this.frmNodalData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbCylindricalAxis.ResumeLayout(false);
            this.grbCylindricalAxis.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grbCylindricalAxis;
        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNodeNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkYr;
        private System.Windows.Forms.CheckBox chkZr;
        private System.Windows.Forms.CheckBox chkZt;
        private System.Windows.Forms.CheckBox chkXr;
        private System.Windows.Forms.CheckBox chkYt;
        private System.Windows.Forms.CheckBox chkXt;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnGlobalCylindrical;
        private System.Windows.Forms.RadioButton rbtnGlobalCartesian;
        private System.Windows.Forms.RadioButton rbtnZAxis;
        private System.Windows.Forms.RadioButton rbtnYAxis;
        private System.Windows.Forms.RadioButton rbtnXAxis;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtZIncr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtYIncr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtXIncr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNodeIncr;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider epAstra;
        private System.Windows.Forms.CheckBox chkIncrOn;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}