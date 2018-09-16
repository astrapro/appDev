namespace ASTRAStructures
{
    partial class frmAnalysisWorkspaceNew
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
            if (!Save_Data(true)) return ;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalysisWorkspaceNew));
            this.tc_parrent = new System.Windows.Forms.TabControl();
            this.tab_create_project = new System.Windows.Forms.TabPage();
            this.sc_design = new System.Windows.Forms.SplitContainer();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.panel25 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label107 = new System.Windows.Forms.Label();
            this.pcb_logo = new System.Windows.Forms.PictureBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.txt_Working_Folder = new System.Windows.Forms.TextBox();
            this.btn_browse_design = new System.Windows.Forms.Button();
            this.grb_survey_type = new System.Windows.Forms.GroupBox();
            this.rbtn_TEXT = new System.Windows.Forms.RadioButton();
            this.rbtn_3D_Drawing = new System.Windows.Forms.RadioButton();
            this.rbtn_SAP = new System.Windows.Forms.RadioButton();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.txt_Project_Name2 = new System.Windows.Forms.TextBox();
            this.btn_tutor_vids = new System.Windows.Forms.Button();
            this.lbl_tutorial_note = new System.Windows.Forms.Label();
            this.pnl_tutorial = new System.Windows.Forms.Panel();
            this.btn_tutorial_example = new System.Windows.Forms.Button();
            this.btn_Update_Project_Data = new System.Windows.Forms.Button();
            this.btn_Refresh_Project_Data = new System.Windows.Forms.Button();
            this.btn_save_proj_data_file = new System.Windows.Forms.Button();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.btn_new_design = new System.Windows.Forms.Button();
            this.chk_create_project_directory = new System.Windows.Forms.CheckBox();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.btn_input_browse = new System.Windows.Forms.Button();
            this.lbl_select_survey = new System.Windows.Forms.Label();
            this.txt_input_file = new System.Windows.Forms.TextBox();
            this.uC_CAD_Model = new AstraAccess.ADOC.UC_CAD();
            this.tab_procees = new System.Windows.Forms.TabPage();
            this.spc_results = new System.Windows.Forms.SplitContainer();
            this.lsv_steps = new System.Windows.Forms.ListView();
            this.dessteps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtb_ana_rep = new System.Windows.Forms.RichTextBox();
            this.panel23 = new System.Windows.Forms.Panel();
            this.btn_open_data = new System.Windows.Forms.Button();
            this.btn_postprocess_data = new System.Windows.Forms.Button();
            this.btn_preprocess_data = new System.Windows.Forms.Button();
            this.chk_show_steps = new System.Windows.Forms.CheckBox();
            this.btn_view_analysis = new System.Windows.Forms.Button();
            this.btn_process_analysis = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmr_moving_load = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tmrLoadDeflection = new System.Windows.Forms.Timer(this.components);
            this.tc_parrent.SuspendLayout();
            this.tab_create_project.SuspendLayout();
            this.sc_design.Panel1.SuspendLayout();
            this.sc_design.Panel2.SuspendLayout();
            this.sc_design.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.panel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).BeginInit();
            this.grb_survey_type.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.pnl_tutorial.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tab_procees.SuspendLayout();
            this.spc_results.Panel1.SuspendLayout();
            this.spc_results.Panel2.SuspendLayout();
            this.spc_results.SuspendLayout();
            this.panel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_parrent
            // 
            this.tc_parrent.Controls.Add(this.tab_create_project);
            this.tc_parrent.Controls.Add(this.tab_procees);
            this.tc_parrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_parrent.Location = new System.Drawing.Point(0, 0);
            this.tc_parrent.Name = "tc_parrent";
            this.tc_parrent.SelectedIndex = 0;
            this.tc_parrent.Size = new System.Drawing.Size(1009, 681);
            this.tc_parrent.TabIndex = 0;
            // 
            // tab_create_project
            // 
            this.tab_create_project.Controls.Add(this.sc_design);
            this.tab_create_project.Location = new System.Drawing.Point(4, 22);
            this.tab_create_project.Name = "tab_create_project";
            this.tab_create_project.Padding = new System.Windows.Forms.Padding(3);
            this.tab_create_project.Size = new System.Drawing.Size(1001, 655);
            this.tab_create_project.TabIndex = 3;
            this.tab_create_project.Text = "Create Project";
            this.tab_create_project.UseVisualStyleBackColor = true;
            // 
            // sc_design
            // 
            this.sc_design.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_design.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc_design.Location = new System.Drawing.Point(3, 3);
            this.sc_design.Name = "sc_design";
            // 
            // sc_design.Panel1
            // 
            this.sc_design.Panel1.Controls.Add(this.groupBox31);
            // 
            // sc_design.Panel2
            // 
            this.sc_design.Panel2.Controls.Add(this.uC_CAD_Model);
            this.sc_design.Size = new System.Drawing.Size(995, 649);
            this.sc_design.SplitterDistance = 459;
            this.sc_design.TabIndex = 3;
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.panel25);
            this.groupBox31.Controls.Add(this.btn_browse_design);
            this.groupBox31.Controls.Add(this.grb_survey_type);
            this.groupBox31.Controls.Add(this.groupBox21);
            this.groupBox31.Controls.Add(this.btn_tutor_vids);
            this.groupBox31.Controls.Add(this.lbl_tutorial_note);
            this.groupBox31.Controls.Add(this.pnl_tutorial);
            this.groupBox31.Controls.Add(this.btn_Update_Project_Data);
            this.groupBox31.Controls.Add(this.btn_Refresh_Project_Data);
            this.groupBox31.Controls.Add(this.btn_save_proj_data_file);
            this.groupBox31.Controls.Add(this.groupBox20);
            this.groupBox31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox31.Location = new System.Drawing.Point(0, 0);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(457, 647);
            this.groupBox31.TabIndex = 4;
            this.groupBox31.TabStop = false;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.linkLabel1);
            this.panel25.Controls.Add(this.label107);
            this.panel25.Controls.Add(this.pcb_logo);
            this.panel25.Controls.Add(this.label106);
            this.panel25.Controls.Add(this.label108);
            this.panel25.Controls.Add(this.lbl_Title);
            this.panel25.Controls.Add(this.txt_Working_Folder);
            this.panel25.Location = new System.Drawing.Point(12, 357);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(425, 273);
            this.panel25.TabIndex = 192;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(94, 213);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(229, 13);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Website : www.techsoftglobal.com";
            // 
            // label107
            // 
            this.label107.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(3, 9);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(418, 38);
            this.label107.TabIndex = 16;
            this.label107.Text = "ASTRA Pro";
            this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcb_logo
            // 
            this.pcb_logo.BackgroundImage = global::ASTRAStructures.Properties.Resources.techsoft_logo;
            this.pcb_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcb_logo.Location = new System.Drawing.Point(153, 113);
            this.pcb_logo.Name = "pcb_logo";
            this.pcb_logo.Size = new System.Drawing.Size(122, 87);
            this.pcb_logo.TabIndex = 17;
            this.pcb_logo.TabStop = false;
            // 
            // label106
            // 
            this.label106.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(3, 77);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(418, 34);
            this.label106.TabIndex = 16;
            this.label106.Text = "TechSOFT Engineering Services";
            this.label106.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label108
            // 
            this.label108.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.Blue;
            this.label108.Location = new System.Drawing.Point(25, 231);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(379, 35);
            this.label108.TabIndex = 16;
            this.label108.Text = "Email At : techsoft@consultant.com, dataflow@mail.com\r\n                 techsofti" +
    "nfra@gmail.com.com";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Title
            // 
            this.lbl_Title.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(3, 47);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(418, 35);
            this.lbl_Title.TabIndex = 16;
            this.lbl_Title.Text = "Analysis";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Working_Folder
            // 
            this.txt_Working_Folder.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Working_Folder.ForeColor = System.Drawing.Color.Black;
            this.txt_Working_Folder.Location = new System.Drawing.Point(42, 9);
            this.txt_Working_Folder.Name = "txt_Working_Folder";
            this.txt_Working_Folder.ReadOnly = true;
            this.txt_Working_Folder.Size = new System.Drawing.Size(248, 21);
            this.txt_Working_Folder.TabIndex = 6;
            // 
            // btn_browse_design
            // 
            this.btn_browse_design.Location = new System.Drawing.Point(363, 287);
            this.btn_browse_design.Name = "btn_browse_design";
            this.btn_browse_design.Size = new System.Drawing.Size(61, 24);
            this.btn_browse_design.TabIndex = 189;
            this.btn_browse_design.Text = "Browse";
            this.btn_browse_design.UseVisualStyleBackColor = true;
            this.btn_browse_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // grb_survey_type
            // 
            this.grb_survey_type.Controls.Add(this.rbtn_TEXT);
            this.grb_survey_type.Controls.Add(this.rbtn_3D_Drawing);
            this.grb_survey_type.Controls.Add(this.rbtn_SAP);
            this.grb_survey_type.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_survey_type.ForeColor = System.Drawing.Color.Black;
            this.grb_survey_type.Location = new System.Drawing.Point(6, 20);
            this.grb_survey_type.Name = "grb_survey_type";
            this.grb_survey_type.Size = new System.Drawing.Size(433, 52);
            this.grb_survey_type.TabIndex = 183;
            this.grb_survey_type.TabStop = false;
            this.grb_survey_type.Text = "Input Data Type";
            // 
            // rbtn_TEXT
            // 
            this.rbtn_TEXT.AutoSize = true;
            this.rbtn_TEXT.Checked = true;
            this.rbtn_TEXT.Location = new System.Drawing.Point(16, 20);
            this.rbtn_TEXT.Name = "rbtn_TEXT";
            this.rbtn_TEXT.Size = new System.Drawing.Size(80, 17);
            this.rbtn_TEXT.TabIndex = 5;
            this.rbtn_TEXT.TabStop = true;
            this.rbtn_TEXT.Text = "Text Data";
            this.rbtn_TEXT.UseVisualStyleBackColor = true;
            this.rbtn_TEXT.CheckedChanged += new System.EventHandler(this.rbtn_3D_Drawing_CheckedChanged);
            // 
            // rbtn_3D_Drawing
            // 
            this.rbtn_3D_Drawing.AutoSize = true;
            this.rbtn_3D_Drawing.Location = new System.Drawing.Point(308, 20);
            this.rbtn_3D_Drawing.Name = "rbtn_3D_Drawing";
            this.rbtn_3D_Drawing.Size = new System.Drawing.Size(96, 17);
            this.rbtn_3D_Drawing.TabIndex = 6;
            this.rbtn_3D_Drawing.Text = "3D Drawing ";
            this.rbtn_3D_Drawing.UseVisualStyleBackColor = true;
            this.rbtn_3D_Drawing.CheckedChanged += new System.EventHandler(this.rbtn_3D_Drawing_CheckedChanged);
            // 
            // rbtn_SAP
            // 
            this.rbtn_SAP.AutoSize = true;
            this.rbtn_SAP.Location = new System.Drawing.Point(161, 20);
            this.rbtn_SAP.Name = "rbtn_SAP";
            this.rbtn_SAP.Size = new System.Drawing.Size(79, 17);
            this.rbtn_SAP.TabIndex = 6;
            this.rbtn_SAP.Text = "SAP Data";
            this.rbtn_SAP.UseVisualStyleBackColor = true;
            this.rbtn_SAP.CheckedChanged += new System.EventHandler(this.rbtn_3D_Drawing_CheckedChanged);
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label144);
            this.groupBox21.Controls.Add(this.label63);
            this.groupBox21.Controls.Add(this.txt_Project_Name2);
            this.groupBox21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox21.ForeColor = System.Drawing.Color.Blue;
            this.groupBox21.Location = new System.Drawing.Point(4, 256);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(433, 64);
            this.groupBox21.TabIndex = 191;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Open Design";
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(10, 101);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(394, 26);
            this.label144.TabIndex = 7;
            this.label144.Text = "Open User\'s Project Folder \r\n(For the second time onwords in multiple sessions of" +
    " work)";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.Black;
            this.label63.Location = new System.Drawing.Point(3, 17);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(93, 13);
            this.label63.TabIndex = 7;
            this.label63.Text = "Project Name :";
            // 
            // txt_Project_Name2
            // 
            this.txt_Project_Name2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Project_Name2.ForeColor = System.Drawing.Color.Black;
            this.txt_Project_Name2.Location = new System.Drawing.Point(6, 34);
            this.txt_Project_Name2.Name = "txt_Project_Name2";
            this.txt_Project_Name2.ReadOnly = true;
            this.txt_Project_Name2.Size = new System.Drawing.Size(347, 21);
            this.txt_Project_Name2.TabIndex = 6;
            // 
            // btn_tutor_vids
            // 
            this.btn_tutor_vids.Location = new System.Drawing.Point(22, 106);
            this.btn_tutor_vids.Name = "btn_tutor_vids";
            this.btn_tutor_vids.Size = new System.Drawing.Size(176, 23);
            this.btn_tutor_vids.TabIndex = 187;
            this.btn_tutor_vids.Text = "View Tutorial Video";
            this.btn_tutor_vids.UseVisualStyleBackColor = true;
            this.btn_tutor_vids.Visible = false;
            // 
            // lbl_tutorial_note
            // 
            this.lbl_tutorial_note.AutoSize = true;
            this.lbl_tutorial_note.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tutorial_note.ForeColor = System.Drawing.Color.Red;
            this.lbl_tutorial_note.Location = new System.Drawing.Point(8, 86);
            this.lbl_tutorial_note.Name = "lbl_tutorial_note";
            this.lbl_tutorial_note.Size = new System.Drawing.Size(423, 14);
            this.lbl_tutorial_note.TabIndex = 190;
            this.lbl_tutorial_note.Text = "Note : In this Trial Version user can only run Tutorial Example Data";
            this.lbl_tutorial_note.Visible = false;
            // 
            // pnl_tutorial
            // 
            this.pnl_tutorial.Controls.Add(this.btn_tutorial_example);
            this.pnl_tutorial.Location = new System.Drawing.Point(240, 106);
            this.pnl_tutorial.Name = "pnl_tutorial";
            this.pnl_tutorial.Size = new System.Drawing.Size(177, 25);
            this.pnl_tutorial.TabIndex = 189;
            // 
            // btn_tutorial_example
            // 
            this.btn_tutorial_example.Location = new System.Drawing.Point(0, 1);
            this.btn_tutorial_example.Name = "btn_tutorial_example";
            this.btn_tutorial_example.Size = new System.Drawing.Size(176, 23);
            this.btn_tutorial_example.TabIndex = 14;
            this.btn_tutorial_example.Text = "Open Tutorial Example Data";
            this.btn_tutorial_example.UseVisualStyleBackColor = true;
            this.btn_tutorial_example.Click += new System.EventHandler(this.btn_tutorial_example_Click);
            // 
            // btn_Update_Project_Data
            // 
            this.btn_Update_Project_Data.Location = new System.Drawing.Point(12, 326);
            this.btn_Update_Project_Data.Name = "btn_Update_Project_Data";
            this.btn_Update_Project_Data.Size = new System.Drawing.Size(157, 23);
            this.btn_Update_Project_Data.TabIndex = 188;
            this.btn_Update_Project_Data.Text = "Update Project Data File";
            this.btn_Update_Project_Data.UseVisualStyleBackColor = true;
            this.btn_Update_Project_Data.Visible = false;
            // 
            // btn_Refresh_Project_Data
            // 
            this.btn_Refresh_Project_Data.Location = new System.Drawing.Point(175, 326);
            this.btn_Refresh_Project_Data.Name = "btn_Refresh_Project_Data";
            this.btn_Refresh_Project_Data.Size = new System.Drawing.Size(106, 23);
            this.btn_Refresh_Project_Data.TabIndex = 186;
            this.btn_Refresh_Project_Data.Text = "Refresh";
            this.btn_Refresh_Project_Data.UseVisualStyleBackColor = true;
            this.btn_Refresh_Project_Data.Visible = false;
            // 
            // btn_save_proj_data_file
            // 
            this.btn_save_proj_data_file.Location = new System.Drawing.Point(287, 326);
            this.btn_save_proj_data_file.Name = "btn_save_proj_data_file";
            this.btn_save_proj_data_file.Size = new System.Drawing.Size(146, 23);
            this.btn_save_proj_data_file.TabIndex = 185;
            this.btn_save_proj_data_file.Text = "Save Project Data File";
            this.btn_save_proj_data_file.UseVisualStyleBackColor = true;
            this.btn_save_proj_data_file.Visible = false;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.btn_new_design);
            this.groupBox20.Controls.Add(this.chk_create_project_directory);
            this.groupBox20.Controls.Add(this.txt_project_name);
            this.groupBox20.Controls.Add(this.label43);
            this.groupBox20.Controls.Add(this.btn_input_browse);
            this.groupBox20.Controls.Add(this.lbl_select_survey);
            this.groupBox20.Controls.Add(this.txt_input_file);
            this.groupBox20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox20.ForeColor = System.Drawing.Color.Blue;
            this.groupBox20.Location = new System.Drawing.Point(4, 135);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(433, 115);
            this.groupBox20.TabIndex = 184;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "New Design";
            // 
            // btn_new_design
            // 
            this.btn_new_design.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new_design.ForeColor = System.Drawing.Color.Black;
            this.btn_new_design.Location = new System.Drawing.Point(306, 30);
            this.btn_new_design.Name = "btn_new_design";
            this.btn_new_design.Size = new System.Drawing.Size(114, 24);
            this.btn_new_design.TabIndex = 188;
            this.btn_new_design.Text = "Create Project";
            this.btn_new_design.UseVisualStyleBackColor = true;
            this.btn_new_design.Click += new System.EventHandler(this.btn_new_design_Click);
            // 
            // chk_create_project_directory
            // 
            this.chk_create_project_directory.AutoSize = true;
            this.chk_create_project_directory.Checked = true;
            this.chk_create_project_directory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_create_project_directory.Location = new System.Drawing.Point(529, 367);
            this.chk_create_project_directory.Name = "chk_create_project_directory";
            this.chk_create_project_directory.Size = new System.Drawing.Size(185, 17);
            this.chk_create_project_directory.TabIndex = 9;
            this.chk_create_project_directory.Text = "Create Project Directory";
            this.chk_create_project_directory.UseVisualStyleBackColor = true;
            this.chk_create_project_directory.Visible = false;
            // 
            // txt_project_name
            // 
            this.txt_project_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_project_name.Location = new System.Drawing.Point(5, 32);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(293, 22);
            this.txt_project_name.TabIndex = 186;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.Black;
            this.label43.Location = new System.Drawing.Point(5, 17);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(93, 13);
            this.label43.TabIndex = 7;
            this.label43.Text = "Project Name :";
            // 
            // btn_input_browse
            // 
            this.btn_input_browse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_input_browse.ForeColor = System.Drawing.Color.Black;
            this.btn_input_browse.Location = new System.Drawing.Point(359, 88);
            this.btn_input_browse.Name = "btn_input_browse";
            this.btn_input_browse.Size = new System.Drawing.Size(61, 23);
            this.btn_input_browse.TabIndex = 3;
            this.btn_input_browse.Text = "Browse";
            this.btn_input_browse.UseVisualStyleBackColor = true;
            this.btn_input_browse.Click += new System.EventHandler(this.btn_input_browse_Click);
            // 
            // lbl_select_survey
            // 
            this.lbl_select_survey.AutoSize = true;
            this.lbl_select_survey.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_select_survey.ForeColor = System.Drawing.Color.Black;
            this.lbl_select_survey.Location = new System.Drawing.Point(9, 57);
            this.lbl_select_survey.Name = "lbl_select_survey";
            this.lbl_select_survey.Size = new System.Drawing.Size(274, 26);
            this.lbl_select_survey.TabIndex = 0;
            this.lbl_select_survey.Text = "Select Analysis Input Data File \r\n(For the first time in multiple sessions of wor" +
    "k)";
            // 
            // txt_input_file
            // 
            this.txt_input_file.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_input_file.ForeColor = System.Drawing.Color.Black;
            this.txt_input_file.Location = new System.Drawing.Point(6, 89);
            this.txt_input_file.Name = "txt_input_file";
            this.txt_input_file.Size = new System.Drawing.Size(347, 21);
            this.txt_input_file.TabIndex = 1;
            // 
            // uC_CAD_Model
            // 
            this.uC_CAD_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_CAD_Model.Location = new System.Drawing.Point(0, 0);
            this.uC_CAD_Model.Name = "uC_CAD_Model";
            this.uC_CAD_Model.Size = new System.Drawing.Size(530, 647);
            this.uC_CAD_Model.TabIndex = 0;
            this.uC_CAD_Model.View_Buttons = false;
            this.uC_CAD_Model.Visible = false;
            // 
            // tab_procees
            // 
            this.tab_procees.Controls.Add(this.spc_results);
            this.tab_procees.Controls.Add(this.panel23);
            this.tab_procees.Location = new System.Drawing.Point(4, 22);
            this.tab_procees.Name = "tab_procees";
            this.tab_procees.Padding = new System.Windows.Forms.Padding(3);
            this.tab_procees.Size = new System.Drawing.Size(1001, 655);
            this.tab_procees.TabIndex = 1;
            this.tab_procees.Text = "Process Analysis";
            this.tab_procees.UseVisualStyleBackColor = true;
            // 
            // spc_results
            // 
            this.spc_results.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_results.Location = new System.Drawing.Point(3, 49);
            this.spc_results.Name = "spc_results";
            // 
            // spc_results.Panel1
            // 
            this.spc_results.Panel1.Controls.Add(this.lsv_steps);
            // 
            // spc_results.Panel2
            // 
            this.spc_results.Panel2.Controls.Add(this.rtb_ana_rep);
            this.spc_results.Size = new System.Drawing.Size(995, 603);
            this.spc_results.SplitterDistance = 196;
            this.spc_results.TabIndex = 6;
            // 
            // lsv_steps
            // 
            this.lsv_steps.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsv_steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lsv_steps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dessteps});
            this.lsv_steps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_steps.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_steps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsv_steps.Location = new System.Drawing.Point(0, 0);
            this.lsv_steps.Name = "lsv_steps";
            this.lsv_steps.ShowItemToolTips = true;
            this.lsv_steps.Size = new System.Drawing.Size(194, 601);
            this.lsv_steps.TabIndex = 1;
            this.lsv_steps.UseCompatibleStateImageBehavior = false;
            this.lsv_steps.View = System.Windows.Forms.View.Details;
            this.lsv_steps.SelectedIndexChanged += new System.EventHandler(this.lstb_steps_SelectedIndexChanged);
            // 
            // dessteps
            // 
            this.dessteps.Text = "ANALYSIS STEPS";
            this.dessteps.Width = 702;
            // 
            // rtb_ana_rep
            // 
            this.rtb_ana_rep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rtb_ana_rep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_ana_rep.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_ana_rep.Location = new System.Drawing.Point(0, 0);
            this.rtb_ana_rep.Name = "rtb_ana_rep";
            this.rtb_ana_rep.Size = new System.Drawing.Size(793, 601);
            this.rtb_ana_rep.TabIndex = 5;
            this.rtb_ana_rep.Text = "";
            this.rtb_ana_rep.WordWrap = false;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.btn_open_data);
            this.panel23.Controls.Add(this.btn_postprocess_data);
            this.panel23.Controls.Add(this.btn_preprocess_data);
            this.panel23.Controls.Add(this.chk_show_steps);
            this.panel23.Controls.Add(this.btn_view_analysis);
            this.panel23.Controls.Add(this.btn_process_analysis);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(3, 3);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(995, 46);
            this.panel23.TabIndex = 4;
            // 
            // btn_open_data
            // 
            this.btn_open_data.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_open_data.Location = new System.Drawing.Point(415, 4);
            this.btn_open_data.Name = "btn_open_data";
            this.btn_open_data.Size = new System.Drawing.Size(133, 38);
            this.btn_open_data.TabIndex = 4;
            this.btn_open_data.Text = "Open Analysis Input Data";
            this.btn_open_data.UseVisualStyleBackColor = true;
            this.btn_open_data.Click += new System.EventHandler(this.btn_open_data_Click);
            // 
            // btn_postprocess_data
            // 
            this.btn_postprocess_data.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_postprocess_data.Location = new System.Drawing.Point(832, 4);
            this.btn_postprocess_data.Name = "btn_postprocess_data";
            this.btn_postprocess_data.Size = new System.Drawing.Size(133, 38);
            this.btn_postprocess_data.TabIndex = 3;
            this.btn_postprocess_data.Text = "View Post Process";
            this.btn_postprocess_data.UseVisualStyleBackColor = true;
            this.btn_postprocess_data.Click += new System.EventHandler(this.btn_postprocess_data_Click);
            // 
            // btn_preprocess_data
            // 
            this.btn_preprocess_data.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_preprocess_data.Location = new System.Drawing.Point(276, 4);
            this.btn_preprocess_data.Name = "btn_preprocess_data";
            this.btn_preprocess_data.Size = new System.Drawing.Size(133, 38);
            this.btn_preprocess_data.TabIndex = 3;
            this.btn_preprocess_data.Text = "View Pre Process";
            this.btn_preprocess_data.UseVisualStyleBackColor = true;
            this.btn_preprocess_data.Click += new System.EventHandler(this.btn_preprocess_data_Click);
            // 
            // chk_show_steps
            // 
            this.chk_show_steps.AutoSize = true;
            this.chk_show_steps.Checked = true;
            this.chk_show_steps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_show_steps.Location = new System.Drawing.Point(16, 16);
            this.chk_show_steps.Name = "chk_show_steps";
            this.chk_show_steps.Size = new System.Drawing.Size(144, 17);
            this.chk_show_steps.TabIndex = 1;
            this.chk_show_steps.Text = "Show Analysis Steps";
            this.chk_show_steps.UseVisualStyleBackColor = true;
            // 
            // btn_view_analysis
            // 
            this.btn_view_analysis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_view_analysis.Location = new System.Drawing.Point(693, 4);
            this.btn_view_analysis.Name = "btn_view_analysis";
            this.btn_view_analysis.Size = new System.Drawing.Size(133, 38);
            this.btn_view_analysis.TabIndex = 0;
            this.btn_view_analysis.Text = "View Analysis Report File";
            this.btn_view_analysis.UseVisualStyleBackColor = true;
            this.btn_view_analysis.Click += new System.EventHandler(this.btn_view_analysis_Click);
            // 
            // btn_process_analysis
            // 
            this.btn_process_analysis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_process_analysis.Location = new System.Drawing.Point(554, 4);
            this.btn_process_analysis.Name = "btn_process_analysis";
            this.btn_process_analysis.Size = new System.Drawing.Size(133, 38);
            this.btn_process_analysis.TabIndex = 0;
            this.btn_process_analysis.Text = "Process Analysis";
            this.btn_process_analysis.UseVisualStyleBackColor = true;
            this.btn_process_analysis.Click += new System.EventHandler(this.btn_process_analysis_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 999;
            this.timer1.Tick += new System.EventHandler(this.PP_timer1_Tick);
            // 
            // frmAnalysisWorkspaceNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 681);
            this.Controls.Add(this.tc_parrent);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAnalysisWorkspaceNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAnalysisWorkspaceNew_FormClosing);
            this.Load += new System.EventHandler(this.frmAnalysisWorkspaceNew_Load);
            this.SizeChanged += new System.EventHandler(this.frm_ASTRA_Analysis_SizeChanged);
            this.tc_parrent.ResumeLayout(false);
            this.tab_create_project.ResumeLayout(false);
            this.sc_design.Panel1.ResumeLayout(false);
            this.sc_design.Panel2.ResumeLayout(false);
            this.sc_design.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_logo)).EndInit();
            this.grb_survey_type.ResumeLayout(false);
            this.grb_survey_type.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.pnl_tutorial.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.tab_procees.ResumeLayout(false);
            this.spc_results.Panel1.ResumeLayout(false);
            this.spc_results.Panel2.ResumeLayout(false);
            this.spc_results.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TabControl tc_parrent;
        private System.Windows.Forms.TabPage tab_procees;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtb_ana_rep;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Button btn_process_analysis;
        private System.Windows.Forms.Timer tmr_moving_load;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer tmrLoadDeflection;
        private System.Windows.Forms.SplitContainer spc_results;
        private System.Windows.Forms.ListView lsv_steps;
        private System.Windows.Forms.ColumnHeader dessteps;
        private System.Windows.Forms.CheckBox chk_show_steps;
        private System.Windows.Forms.Button btn_view_analysis;
        private System.Windows.Forms.Button btn_open_data;
        private System.Windows.Forms.Button btn_preprocess_data;
        private System.Windows.Forms.TabPage tab_create_project;
        private System.Windows.Forms.SplitContainer sc_design;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Button btn_new_design;
        private System.Windows.Forms.Button btn_browse_design;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.PictureBox pcb_logo;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.GroupBox grb_survey_type;
        private System.Windows.Forms.RadioButton rbtn_TEXT;
        private System.Windows.Forms.RadioButton rbtn_3D_Drawing;
        private System.Windows.Forms.RadioButton rbtn_SAP;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txt_Project_Name2;
        private System.Windows.Forms.TextBox txt_Working_Folder;
        private System.Windows.Forms.Button btn_tutor_vids;
        private System.Windows.Forms.Label lbl_tutorial_note;
        private System.Windows.Forms.Panel pnl_tutorial;
        private System.Windows.Forms.Button btn_tutorial_example;
        private System.Windows.Forms.Button btn_Update_Project_Data;
        private System.Windows.Forms.Button btn_Refresh_Project_Data;
        private System.Windows.Forms.Button btn_save_proj_data_file;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.CheckBox chk_create_project_directory;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button btn_input_browse;
        private System.Windows.Forms.Label lbl_select_survey;
        private System.Windows.Forms.TextBox txt_input_file;
        private AstraAccess.ADOC.UC_CAD uC_CAD_Model;
        private System.Windows.Forms.Button btn_postprocess_data;

    }

}