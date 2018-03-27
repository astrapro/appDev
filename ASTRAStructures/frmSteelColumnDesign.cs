using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using BridgeAnalysisDesign;

namespace ASTRAStructures
{
    public partial class frmSteelColumnDesign : Form
    {
        
        IApplication iApp;
        public frmSteelColumnDesign(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            uC_SteelSections_Column.SetIApplication(app);
        }

        private void btn_steel_column_design_Click(object sender, EventArgs e)
        {

            SteelColumnDesign cd = new SteelColumnDesign(iApp);


            #region User Input

            //cd.AST_DOC = AST_DOC;

            //if (AST_DOC_ORG != null)
            //    cd.AST_DOC = AST_DOC_ORG;
            //else
            //    cd.AST_DOC = AST_DOC;

            cd.l = MyList.StringToDouble(txt_steel_column_l.Text, 0.0);
            cd.a = MyList.StringToDouble(txt_steel_column_a.Text, 0.0);
            cd.P = MyList.StringToDouble(txt_steel_column_P.Text, 0.0);
            cd.M = MyList.StringToDouble(txt_steel_column_M.Text, 0.0);
            cd.V = MyList.StringToDouble(txt_steel_column_V.Text, 0.0);
            cd.e = MyList.StringToDouble(txt_steel_column_e.Text, 0.0);
            cd.Pms = MyList.StringToDouble(txt_steel_column_Pms.Text, 0.0);
            cd.fy = MyList.StringToDouble(txt_steel_column_fy.Text, 0.0);
            cd.fs = MyList.StringToDouble(txt_steel_column_fs.Text, 0.0);
            cd.fb = MyList.StringToDouble(txt_steel_column_fb.Text, 0.0);
            cd.Pcs = MyList.StringToDouble(txt_steel_column_Pcs.Text, 0.0);
            cd.Ps = MyList.StringToDouble(txt_steel_column_Ps.Text, 0.0);
            cd.n = MyList.StringToDouble(txt_steel_column_n.Text, 0.0);
            cd.tb = MyList.StringToDouble(txt_steel_column_tb.Text, 0.0);
            cd.Dr = MyList.StringToDouble(txt_steel_column_Dr.Text, 0.0);
            cd.Nr = MyList.StringToDouble(txt_steel_column_Nr.Text, 0.0);


            cd.A = MyList.StringToDouble(uC_SteelSections_Column.txt_a.Text, 0.0);
            cd.h = MyList.StringToDouble(uC_SteelSections_Column.txt_h.Text, 0.0);
            cd.Bf = MyList.StringToDouble(uC_SteelSections_Column.txt_Bf.Text, 0.0);
            cd.tw = MyList.StringToDouble(uC_SteelSections_Column.txt_tw.Text, 0.0);
            cd.Ixx = MyList.StringToDouble(uC_SteelSections_Column.txt_Ixx.Text, 0.0);
            cd.Iyy = MyList.StringToDouble(uC_SteelSections_Column.txt_Iyy.Text, 0.0);
            cd.rxx = MyList.StringToDouble(uC_SteelSections_Column.txt_rxx.Text, 0.0);
            cd.ryy = MyList.StringToDouble(uC_SteelSections_Column.txt_ryy.Text, 0.0);

            #endregion User Input

            cd.Report_File = Column_Design_Report;

            //cd.Calculate_Program_Loop();
            //cd.Calculate_Program();


            //cd.Design_Program_Individual(1);
            cd.Calculate_Program();
            MessageBox.Show("Report file created in file " + Column_Design_Report);
            iApp.View_Result(cd.Report_File);
            Button_Enabled_Disabled();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            if (File.Exists(Column_Design_Report))
            {
                System.Diagnostics.Process.Start(Column_Design_Report);
            }
        }


        #region Create Project / Open Project

        public string user_path { get; set; }

        //const string Title = "DESIGN OF RE (Reinforced Earth) WALL";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF STEEL COLUMNS [BS]";
                return "DESIGN OF STEEL COLUMNS [IS]";
            }
        }



        public string Working_Folder
        {
            get
            {
                // if (Directory.Exists(Path.Combine(user_path, Title)) == false)
                //    Directory.CreateDirectory(Path.Combine(user_path, Title));
                //return Path.Combine(user_path, Title);

                return user_path;

            }
        }


        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    //txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    //Open_Project();

                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As

                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    Write_All_Data();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enabled_Disabled();
        }

        private void Open_Project()
        {
            #region Chiranjit Design Option

            try
            {
                iApp.Read_Form_Record(this, user_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option

        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }


        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Steel_Column;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]

        public string Column_Design_Report { 
            get
            {
                if (user_path == null) return "";
                return Path.Combine(user_path, "STEEL_COLUMN_DESIGN_REPORT.TXT");
            }
        
        }

        private void frmSteelColumnDesign_Load(object sender, EventArgs e)
        {
            Set_Project_Name();
            Button_Enabled_Disabled();
        }
        void Button_Enabled_Disabled()
        {
            btn_steel_column_design.Enabled = Directory.Exists(user_path);
            btn_report.Enabled = File.Exists(Column_Design_Report);
        }
    }
}
