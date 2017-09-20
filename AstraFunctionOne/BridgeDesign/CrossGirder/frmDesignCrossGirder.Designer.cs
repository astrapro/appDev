namespace AstraFunctionOne.BridgeDesign.Design3
{
    partial class frmDesignCrossGirder
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_grade_concrete = new System.Windows.Forms.TextBox();
            this.txt_grade_steel = new System.Windows.Forms.TextBox();
            this.txt_clear_cover = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_web_thickness_cross_girder = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_total_shear = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_modular_ratio = new System.Windows.Forms.TextBox();
            this.txt_depth_cross_girder = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_number_cross_girder = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_spacing_cross_girders = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_number_longitudinal_girder = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_spacing_longitudinal_girder = new System.Windows.Forms.TextBox();
            this.txt_total_hogging_moment = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_stress_steel = new System.Windows.Forms.TextBox();
            this.txt_stress_concrete = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnWorkingFolder = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(496, 7);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_grade_concrete);
            this.groupBox1.Controls.Add(this.txt_grade_steel);
            this.groupBox1.Controls.Add(this.txt_clear_cover);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_web_thickness_cross_girder);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txt_total_shear);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_modular_ratio);
            this.groupBox1.Controls.Add(this.txt_depth_cross_girder);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.txt_number_cross_girder);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_spacing_cross_girders);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_number_longitudinal_girder);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_spacing_longitudinal_girder);
            this.groupBox1.Controls.Add(this.txt_total_hogging_moment);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 343);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER DATA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::AstraFunctionOne.Properties.Resources.T_Beam_Slab_Long_Cross_Girders;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(358, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(465, 321);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(221, 288);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(23, 14);
            this.label35.TabIndex = 44;
            this.label35.Text = "Fe";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(325, 185);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(18, 13);
            this.label27.TabIndex = 43;
            this.label27.Text = "m";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(325, 158);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(18, 13);
            this.label26.TabIndex = 43;
            this.label26.Text = "m";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(226, 265);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(18, 14);
            this.label34.TabIndex = 44;
            this.label34.Text = "M";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(325, 103);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(18, 13);
            this.label25.TabIndex = 43;
            this.label25.Text = "m";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(325, 48);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 13);
            this.label24.TabIndex = 43;
            this.label24.Text = "m";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(325, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(27, 13);
            this.label23.TabIndex = 43;
            this.label23.Text = "t-m";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(323, 238);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(18, 13);
            this.label31.TabIndex = 43;
            this.label31.Text = "m";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Web thickness of Cross Girders";
            // 
            // txt_grade_concrete
            // 
            this.txt_grade_concrete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_grade_concrete.Location = new System.Drawing.Point(250, 262);
            this.txt_grade_concrete.Name = "txt_grade_concrete";
            this.txt_grade_concrete.Size = new System.Drawing.Size(72, 21);
            this.txt_grade_concrete.TabIndex = 1;
            this.txt_grade_concrete.Text = "25";
            // 
            // txt_grade_steel
            // 
            this.txt_grade_steel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_grade_steel.Location = new System.Drawing.Point(250, 285);
            this.txt_grade_steel.Name = "txt_grade_steel";
            this.txt_grade_steel.Size = new System.Drawing.Size(72, 21);
            this.txt_grade_steel.TabIndex = 2;
            this.txt_grade_steel.Text = "415";
            // 
            // txt_clear_cover
            // 
            this.txt_clear_cover.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_clear_cover.Location = new System.Drawing.Point(250, 235);
            this.txt_clear_cover.Name = "txt_clear_cover";
            this.txt_clear_cover.Size = new System.Drawing.Size(69, 21);
            this.txt_clear_cover.TabIndex = 12;
            this.txt_clear_cover.Text = "0.040";
            this.txt_clear_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(23, 265);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 13);
            this.label17.TabIndex = 31;
            this.label17.Text = "Grade of Concrete";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Depth of Cross Girders";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 288);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 13);
            this.label18.TabIndex = 33;
            this.label18.Text = "Grade of Steel";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(20, 238);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Clear Cover";
            // 
            // txt_web_thickness_cross_girder
            // 
            this.txt_web_thickness_cross_girder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_web_thickness_cross_girder.Location = new System.Drawing.Point(250, 182);
            this.txt_web_thickness_cross_girder.Name = "txt_web_thickness_cross_girder";
            this.txt_web_thickness_cross_girder.Size = new System.Drawing.Size(69, 21);
            this.txt_web_thickness_cross_girder.TabIndex = 8;
            this.txt_web_thickness_cross_girder.Text = "0.325";
            this.txt_web_thickness_cross_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(24, 316);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 13);
            this.label22.TabIndex = 41;
            this.label22.Text = "Modular Ratio";
            // 
            // txt_total_shear
            // 
            this.txt_total_shear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_shear.Location = new System.Drawing.Point(250, 209);
            this.txt_total_shear.Name = "txt_total_shear";
            this.txt_total_shear.Size = new System.Drawing.Size(69, 21);
            this.txt_total_shear.TabIndex = 11;
            this.txt_total_shear.Text = " ";
            this.txt_total_shear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Number of Cross Girders";
            // 
            // txt_modular_ratio
            // 
            this.txt_modular_ratio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_modular_ratio.Location = new System.Drawing.Point(250, 312);
            this.txt_modular_ratio.Name = "txt_modular_ratio";
            this.txt_modular_ratio.Size = new System.Drawing.Size(72, 21);
            this.txt_modular_ratio.TabIndex = 3;
            this.txt_modular_ratio.Text = "10";
            this.txt_modular_ratio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_depth_cross_girder
            // 
            this.txt_depth_cross_girder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_depth_cross_girder.Location = new System.Drawing.Point(250, 155);
            this.txt_depth_cross_girder.Name = "txt_depth_cross_girder";
            this.txt_depth_cross_girder.Size = new System.Drawing.Size(69, 21);
            this.txt_depth_cross_girder.TabIndex = 7;
            this.txt_depth_cross_girder.Text = "1.525";
            this.txt_depth_cross_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(123, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "Spacing of Cross Girders";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(323, 212);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(11, 13);
            this.label30.TabIndex = 43;
            this.label30.Text = "t";
            // 
            // txt_number_cross_girder
            // 
            this.txt_number_cross_girder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_number_cross_girder.Location = new System.Drawing.Point(250, 127);
            this.txt_number_cross_girder.Name = "txt_number_cross_girder";
            this.txt_number_cross_girder.Size = new System.Drawing.Size(69, 21);
            this.txt_number_cross_girder.TabIndex = 6;
            this.txt_number_cross_girder.Text = "3";
            this.txt_number_cross_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 212);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Total Shear";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Number of Longitudinal Girders";
            // 
            // txt_spacing_cross_girders
            // 
            this.txt_spacing_cross_girders.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_spacing_cross_girders.Location = new System.Drawing.Point(250, 100);
            this.txt_spacing_cross_girders.Name = "txt_spacing_cross_girders";
            this.txt_spacing_cross_girders.Size = new System.Drawing.Size(69, 21);
            this.txt_spacing_cross_girders.TabIndex = 5;
            this.txt_spacing_cross_girders.Text = "9.60";
            this.txt_spacing_cross_girders.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Spacing of Longitudinal Girders";
            // 
            // txt_number_longitudinal_girder
            // 
            this.txt_number_longitudinal_girder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_number_longitudinal_girder.Location = new System.Drawing.Point(250, 73);
            this.txt_number_longitudinal_girder.Name = "txt_number_longitudinal_girder";
            this.txt_number_longitudinal_girder.Size = new System.Drawing.Size(69, 21);
            this.txt_number_longitudinal_girder.TabIndex = 4;
            this.txt_number_longitudinal_girder.Text = "4";
            this.txt_number_longitudinal_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Total Hogging Moment";
            // 
            // txt_spacing_longitudinal_girder
            // 
            this.txt_spacing_longitudinal_girder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_spacing_longitudinal_girder.Location = new System.Drawing.Point(250, 46);
            this.txt_spacing_longitudinal_girder.Name = "txt_spacing_longitudinal_girder";
            this.txt_spacing_longitudinal_girder.Size = new System.Drawing.Size(69, 21);
            this.txt_spacing_longitudinal_girder.TabIndex = 3;
            this.txt_spacing_longitudinal_girder.Text = "2.65";
            this.txt_spacing_longitudinal_girder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_total_hogging_moment
            // 
            this.txt_total_hogging_moment.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_hogging_moment.Location = new System.Drawing.Point(250, 19);
            this.txt_total_hogging_moment.Name = "txt_total_hogging_moment";
            this.txt_total_hogging_moment.Size = new System.Drawing.Size(69, 21);
            this.txt_total_hogging_moment.TabIndex = 2;
            this.txt_total_hogging_moment.Text = " ";
            this.txt_total_hogging_moment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(335, 427);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(48, 13);
            this.label33.TabIndex = 43;
            this.label33.Text = "t/sq.m.";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(335, 404);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(48, 13);
            this.label32.TabIndex = 43;
            this.label32.Text = "t/sq.m.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(36, 427);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Stress in Steel";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(36, 404);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Stress in Concrete";
            // 
            // txt_stress_steel
            // 
            this.txt_stress_steel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_stress_steel.Location = new System.Drawing.Point(262, 424);
            this.txt_stress_steel.Name = "txt_stress_steel";
            this.txt_stress_steel.Size = new System.Drawing.Size(69, 21);
            this.txt_stress_steel.TabIndex = 5;
            this.txt_stress_steel.Text = "2000";
            this.txt_stress_steel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_stress_concrete
            // 
            this.txt_stress_concrete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_stress_concrete.Location = new System.Drawing.Point(262, 401);
            this.txt_stress_concrete.Name = "txt_stress_concrete";
            this.txt_stress_concrete.Size = new System.Drawing.Size(69, 21);
            this.txt_stress_concrete.TabIndex = 4;
            this.txt_stress_concrete.Text = "1000";
            this.txt_stress_concrete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(426, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Collecting Moment Values from ASTRA Pro Moving Load Analysis:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(730, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnWorkingFolder
            // 
            this.btnWorkingFolder.Location = new System.Drawing.Point(387, 7);
            this.btnWorkingFolder.Name = "btnWorkingFolder";
            this.btnWorkingFolder.Size = new System.Drawing.Size(103, 23);
            this.btnWorkingFolder.TabIndex = 3;
            this.btnWorkingFolder.Text = "Working Folder";
            this.btnWorkingFolder.UseVisualStyleBackColor = true;
            this.btnWorkingFolder.Click += new System.EventHandler(this.btnWorkingFolder_Click);
            // 
            // btnReport
            // 
            this.btnReport.Enabled = false;
            this.btnReport.Location = new System.Drawing.Point(614, 7);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(103, 23);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // pb_image
            // 
            this.pb_image.BackgroundImage = global::AstraFunctionOne.Properties.Resources.RCC_T_Beam_Bridge;
            this.pb_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_image.Location = new System.Drawing.Point(389, 383);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(318, 154);
            this.pb_image.TabIndex = 45;
            this.pb_image.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnWorkingFolder);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 543);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 45);
            this.panel1.TabIndex = 46;
            // 
            // frmDesignCrossGirder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 588);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pb_image);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txt_stress_concrete);
            this.Controls.Add(this.txt_stress_steel);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDesignCrossGirder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DESIGN OF CROSS GIRDER";
            this.Load += new System.EventHandler(this.frmDesignCrossGirder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_depth_cross_girder;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_number_cross_girder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_spacing_cross_girders;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_number_longitudinal_girder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_spacing_longitudinal_girder;
        private System.Windows.Forms.TextBox txt_total_hogging_moment;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_grade_steel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_grade_concrete;
        private System.Windows.Forms.TextBox txt_total_shear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_web_thickness_cross_girder;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_modular_ratio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_stress_steel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_stress_concrete;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_clear_cover;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnWorkingFolder;
        private System.Windows.Forms.PictureBox pb_image;
        private System.Windows.Forms.Panel panel1;
    }
}