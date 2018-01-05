namespace AstraAccess.SAP_Forms
{
    partial class frm_Beam_Elements
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
            this.btn_close = new System.Windows.Forms.Button();
            this.grb_incr = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_incr_end_jnt = new System.Windows.Forms.TextBox();
            this.txt_incr_no = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_incr_start_jnt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grb_Member = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_end_frc_no = new System.Windows.Forms.TextBox();
            this.txt_sec_prop_no = new System.Windows.Forms.TextBox();
            this.txt_mat_prop_no = new System.Windows.Forms.TextBox();
            this.txt_node_K = new System.Windows.Forms.TextBox();
            this.txt_node_J = new System.Windows.Forms.TextBox();
            this.txt_mbr_No = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_node_I = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_J_Fx = new System.Windows.Forms.CheckBox();
            this.chk_J_Mz = new System.Windows.Forms.CheckBox();
            this.chk_J_Fy = new System.Windows.Forms.CheckBox();
            this.chk_J_My = new System.Windows.Forms.CheckBox();
            this.chk_J_Fz = new System.Windows.Forms.CheckBox();
            this.chk_J_Mx = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_I_Fx = new System.Windows.Forms.CheckBox();
            this.chk_I_Mz = new System.Windows.Forms.CheckBox();
            this.chk_I_Fy = new System.Windows.Forms.CheckBox();
            this.chk_I_My = new System.Windows.Forms.CheckBox();
            this.chk_I_Fz = new System.Windows.Forms.CheckBox();
            this.chk_I_Mx = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grb_incr.SuspendLayout();
            this.grb_Member.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(256, 229);
            this.btn_close.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(189, 34);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // grb_incr
            // 
            this.grb_incr.Controls.Add(this.label1);
            this.grb_incr.Controls.Add(this.txt_incr_end_jnt);
            this.grb_incr.Controls.Add(this.txt_incr_no);
            this.grb_incr.Controls.Add(this.label2);
            this.grb_incr.Controls.Add(this.txt_incr_start_jnt);
            this.grb_incr.Controls.Add(this.label3);
            this.grb_incr.Location = new System.Drawing.Point(256, 12);
            this.grb_incr.Name = "grb_incr";
            this.grb_incr.Size = new System.Drawing.Size(234, 105);
            this.grb_incr.TabIndex = 1;
            this.grb_incr.TabStop = false;
            this.grb_incr.Text = "INCREMENT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "NODE J";
            // 
            // txt_incr_end_jnt
            // 
            this.txt_incr_end_jnt.Location = new System.Drawing.Point(125, 77);
            this.txt_incr_end_jnt.Name = "txt_incr_end_jnt";
            this.txt_incr_end_jnt.Size = new System.Drawing.Size(61, 21);
            this.txt_incr_end_jnt.TabIndex = 2;
            this.txt_incr_end_jnt.Text = "0";
            this.txt_incr_end_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_incr_no
            // 
            this.txt_incr_no.Location = new System.Drawing.Point(125, 20);
            this.txt_incr_no.Name = "txt_incr_no";
            this.txt_incr_no.Size = new System.Drawing.Size(61, 21);
            this.txt_incr_no.TabIndex = 0;
            this.txt_incr_no.Text = "0";
            this.txt_incr_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Member No";
            // 
            // txt_incr_start_jnt
            // 
            this.txt_incr_start_jnt.Location = new System.Drawing.Point(125, 47);
            this.txt_incr_start_jnt.Name = "txt_incr_start_jnt";
            this.txt_incr_start_jnt.Size = new System.Drawing.Size(61, 21);
            this.txt_incr_start_jnt.TabIndex = 1;
            this.txt_incr_start_jnt.Text = "0";
            this.txt_incr_start_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "NODE I";
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label8);
            this.grb_Member.Controls.Add(this.label7);
            this.grb_Member.Controls.Add(this.label5);
            this.grb_Member.Controls.Add(this.label4);
            this.grb_Member.Controls.Add(this.label6);
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_end_frc_no);
            this.grb_Member.Controls.Add(this.txt_sec_prop_no);
            this.grb_Member.Controls.Add(this.txt_mat_prop_no);
            this.grb_Member.Controls.Add(this.txt_node_K);
            this.grb_Member.Controls.Add(this.txt_node_J);
            this.grb_Member.Controls.Add(this.txt_mbr_No);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_node_I);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(12, 12);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(238, 211);
            this.grb_Member.TabIndex = 0;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "BEAM ELEMENT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(56, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "Fixed End Force \r\nIdentification No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Section Property No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Material Property No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "NODE K";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(7, 80);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(49, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "NODE J";
            // 
            // txt_end_frc_no
            // 
            this.txt_end_frc_no.Location = new System.Drawing.Point(159, 186);
            this.txt_end_frc_no.Name = "txt_end_frc_no";
            this.txt_end_frc_no.Size = new System.Drawing.Size(61, 21);
            this.txt_end_frc_no.TabIndex = 6;
            this.txt_end_frc_no.Text = "0";
            this.txt_end_frc_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_sec_prop_no
            // 
            this.txt_sec_prop_no.Location = new System.Drawing.Point(159, 154);
            this.txt_sec_prop_no.Name = "txt_sec_prop_no";
            this.txt_sec_prop_no.Size = new System.Drawing.Size(61, 21);
            this.txt_sec_prop_no.TabIndex = 5;
            this.txt_sec_prop_no.Text = "1";
            this.txt_sec_prop_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mat_prop_no
            // 
            this.txt_mat_prop_no.Location = new System.Drawing.Point(159, 126);
            this.txt_mat_prop_no.Name = "txt_mat_prop_no";
            this.txt_mat_prop_no.Size = new System.Drawing.Size(61, 21);
            this.txt_mat_prop_no.TabIndex = 4;
            this.txt_mat_prop_no.Text = "1";
            this.txt_mat_prop_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_K
            // 
            this.txt_node_K.Location = new System.Drawing.Point(159, 101);
            this.txt_node_K.Name = "txt_node_K";
            this.txt_node_K.Size = new System.Drawing.Size(61, 21);
            this.txt_node_K.TabIndex = 3;
            this.txt_node_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_node_J
            // 
            this.txt_node_J.Location = new System.Drawing.Point(159, 74);
            this.txt_node_J.Name = "txt_node_J";
            this.txt_node_J.Size = new System.Drawing.Size(61, 21);
            this.txt_node_J.TabIndex = 2;
            this.txt_node_J.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mbr_No
            // 
            this.txt_mbr_No.Location = new System.Drawing.Point(159, 20);
            this.txt_mbr_No.Name = "txt_mbr_No";
            this.txt_mbr_No.Size = new System.Drawing.Size(61, 21);
            this.txt_mbr_No.TabIndex = 0;
            this.txt_mbr_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(7, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Member No";
            // 
            // txt_node_I
            // 
            this.txt_node_I.Location = new System.Drawing.Point(159, 47);
            this.txt_node_I.Name = "txt_node_I";
            this.txt_node_I.Size = new System.Drawing.Size(61, 21);
            this.txt_node_I.TabIndex = 1;
            this.txt_node_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(7, 50);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "NODE  I";
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(53, 229);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(189, 34);
            this.btn_add_mem.TabIndex = 4;
            this.btn_add_mem.Text = "ADD BEAM";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_add_mem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_J_Fx);
            this.groupBox3.Controls.Add(this.chk_J_Mz);
            this.groupBox3.Controls.Add(this.chk_J_Fy);
            this.groupBox3.Controls.Add(this.chk_J_My);
            this.groupBox3.Controls.Add(this.chk_J_Fz);
            this.groupBox3.Controls.Add(this.chk_J_Mx);
            this.groupBox3.Location = new System.Drawing.Point(256, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 52);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Release for Node J";
            // 
            // chk_J_Fx
            // 
            this.chk_J_Fx.AutoSize = true;
            this.chk_J_Fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fx.Location = new System.Drawing.Point(6, 20);
            this.chk_J_Fx.Name = "chk_J_Fx";
            this.chk_J_Fx.Size = new System.Drawing.Size(39, 17);
            this.chk_J_Fx.TabIndex = 0;
            this.chk_J_Fx.Text = "Fx";
            this.chk_J_Fx.UseVisualStyleBackColor = true;
            // 
            // chk_J_Mz
            // 
            this.chk_J_Mz.AutoSize = true;
            this.chk_J_Mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Mz.Location = new System.Drawing.Point(205, 20);
            this.chk_J_Mz.Name = "chk_J_Mz";
            this.chk_J_Mz.Size = new System.Drawing.Size(41, 17);
            this.chk_J_Mz.TabIndex = 5;
            this.chk_J_Mz.Text = "Mz";
            this.chk_J_Mz.UseVisualStyleBackColor = true;
            // 
            // chk_J_Fy
            // 
            this.chk_J_Fy.AutoSize = true;
            this.chk_J_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fy.Location = new System.Drawing.Point(46, 20);
            this.chk_J_Fy.Name = "chk_J_Fy";
            this.chk_J_Fy.Size = new System.Drawing.Size(39, 17);
            this.chk_J_Fy.TabIndex = 1;
            this.chk_J_Fy.Text = "Fy";
            this.chk_J_Fy.UseVisualStyleBackColor = true;
            // 
            // chk_J_My
            // 
            this.chk_J_My.AutoSize = true;
            this.chk_J_My.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_My.Location = new System.Drawing.Point(167, 20);
            this.chk_J_My.Name = "chk_J_My";
            this.chk_J_My.Size = new System.Drawing.Size(42, 17);
            this.chk_J_My.TabIndex = 4;
            this.chk_J_My.Text = "My";
            this.chk_J_My.UseVisualStyleBackColor = true;
            // 
            // chk_J_Fz
            // 
            this.chk_J_Fz.AutoSize = true;
            this.chk_J_Fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Fz.Location = new System.Drawing.Point(85, 20);
            this.chk_J_Fz.Name = "chk_J_Fz";
            this.chk_J_Fz.Size = new System.Drawing.Size(38, 17);
            this.chk_J_Fz.TabIndex = 2;
            this.chk_J_Fz.Text = "Fz";
            this.chk_J_Fz.UseVisualStyleBackColor = true;
            // 
            // chk_J_Mx
            // 
            this.chk_J_Mx.AutoSize = true;
            this.chk_J_Mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_J_Mx.Location = new System.Drawing.Point(123, 20);
            this.chk_J_Mx.Name = "chk_J_Mx";
            this.chk_J_Mx.Size = new System.Drawing.Size(42, 17);
            this.chk_J_Mx.TabIndex = 3;
            this.chk_J_Mx.Text = "Mx";
            this.chk_J_Mx.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_I_Fx);
            this.groupBox2.Controls.Add(this.chk_I_Mz);
            this.groupBox2.Controls.Add(this.chk_I_Fy);
            this.groupBox2.Controls.Add(this.chk_I_My);
            this.groupBox2.Controls.Add(this.chk_I_Fz);
            this.groupBox2.Controls.Add(this.chk_I_Mx);
            this.groupBox2.Location = new System.Drawing.Point(256, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 42);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Release for Node I";
            // 
            // chk_I_Fx
            // 
            this.chk_I_Fx.AutoSize = true;
            this.chk_I_Fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fx.Location = new System.Drawing.Point(6, 20);
            this.chk_I_Fx.Name = "chk_I_Fx";
            this.chk_I_Fx.Size = new System.Drawing.Size(39, 17);
            this.chk_I_Fx.TabIndex = 0;
            this.chk_I_Fx.Text = "Fx";
            this.chk_I_Fx.UseVisualStyleBackColor = true;
            // 
            // chk_I_Mz
            // 
            this.chk_I_Mz.AutoSize = true;
            this.chk_I_Mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Mz.Location = new System.Drawing.Point(205, 20);
            this.chk_I_Mz.Name = "chk_I_Mz";
            this.chk_I_Mz.Size = new System.Drawing.Size(41, 17);
            this.chk_I_Mz.TabIndex = 5;
            this.chk_I_Mz.Text = "Mz";
            this.chk_I_Mz.UseVisualStyleBackColor = true;
            // 
            // chk_I_Fy
            // 
            this.chk_I_Fy.AutoSize = true;
            this.chk_I_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fy.Location = new System.Drawing.Point(46, 20);
            this.chk_I_Fy.Name = "chk_I_Fy";
            this.chk_I_Fy.Size = new System.Drawing.Size(39, 17);
            this.chk_I_Fy.TabIndex = 1;
            this.chk_I_Fy.Text = "Fy";
            this.chk_I_Fy.UseVisualStyleBackColor = true;
            // 
            // chk_I_My
            // 
            this.chk_I_My.AutoSize = true;
            this.chk_I_My.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_My.Location = new System.Drawing.Point(167, 20);
            this.chk_I_My.Name = "chk_I_My";
            this.chk_I_My.Size = new System.Drawing.Size(42, 17);
            this.chk_I_My.TabIndex = 4;
            this.chk_I_My.Text = "My";
            this.chk_I_My.UseVisualStyleBackColor = true;
            // 
            // chk_I_Fz
            // 
            this.chk_I_Fz.AutoSize = true;
            this.chk_I_Fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Fz.Location = new System.Drawing.Point(85, 20);
            this.chk_I_Fz.Name = "chk_I_Fz";
            this.chk_I_Fz.Size = new System.Drawing.Size(38, 17);
            this.chk_I_Fz.TabIndex = 2;
            this.chk_I_Fz.Text = "Fz";
            this.chk_I_Fz.UseVisualStyleBackColor = true;
            // 
            // chk_I_Mx
            // 
            this.chk_I_Mx.AutoSize = true;
            this.chk_I_Mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_I_Mx.Location = new System.Drawing.Point(123, 20);
            this.chk_I_Mx.Name = "chk_I_Mx";
            this.chk_I_Mx.Size = new System.Drawing.Size(42, 17);
            this.chk_I_Mx.TabIndex = 3;
            this.chk_I_Mx.Text = "Mx";
            this.chk_I_Mx.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(71, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 63);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NOTE :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(6, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(327, 39);
            this.label11.TabIndex = 13;
            this.label11.Text = "* Node \'K\' will not be in the same line of the member\r\n axis but in the Plane pas" +
    "sing through the member axis \r\nto define the plane of Bending";
            // 
            // frm_Beam_Elements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 344);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.grb_incr);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add_mem);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Beam_Elements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beam Elements";
            this.Load += new System.EventHandler(this.frm_Beam_Elements_Load);
            this.grb_incr.ResumeLayout(false);
            this.grb_incr.PerformLayout();
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox grb_incr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_incr_end_jnt;
        private System.Windows.Forms.TextBox txt_incr_no;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_incr_start_jnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_sec_prop_no;
        private System.Windows.Forms.TextBox txt_mat_prop_no;
        private System.Windows.Forms.TextBox txt_node_J;
        private System.Windows.Forms.TextBox txt_mbr_No;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_node_I;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_add_mem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_node_K;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_J_Fx;
        private System.Windows.Forms.CheckBox chk_J_Mz;
        private System.Windows.Forms.CheckBox chk_J_Fy;
        private System.Windows.Forms.CheckBox chk_J_My;
        private System.Windows.Forms.CheckBox chk_J_Fz;
        private System.Windows.Forms.CheckBox chk_J_Mx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_I_Fx;
        private System.Windows.Forms.CheckBox chk_I_Mz;
        private System.Windows.Forms.CheckBox chk_I_Fy;
        private System.Windows.Forms.CheckBox chk_I_My;
        private System.Windows.Forms.CheckBox chk_I_Fz;
        private System.Windows.Forms.CheckBox chk_I_Mx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_end_frc_no;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
    }
}