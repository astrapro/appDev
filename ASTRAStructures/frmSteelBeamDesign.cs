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
    public partial class frmSteelBeamDesign : Form
    {
        IApplication iApp;
        public frmSteelBeamDesign(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            uC_SteelSections_Beam.SetIApplication(app);
        }

        private void btn_steel_beam_open_design_Click(object sender, EventArgs e)
        {
            if (File.Exists(Beam_Design_Report))
            {
                System.Diagnostics.Process.Start(Beam_Design_Report);
            }
        }


        private void btn_steel_beam_design_Click(object sender, EventArgs e)
        {

            #region Read User Input

            SteelBeamDesign beamDes = new SteelBeamDesign(iApp);


            #region Chiranjit [2015 05 01]



            beamDes.l = MyList.StringToDouble(txt_steel_beam_l.Text, 0.0);
            beamDes.a = MyList.StringToDouble(txt_steel_beam_a.Text, 0.0);
            beamDes.M = MyList.StringToDouble(txt_steel_beam_M.Text, 0.0);
            beamDes.V = MyList.StringToDouble(txt_steel_beam_V.Text, 0.0);
            beamDes.Pms = MyList.StringToDouble(txt_steel_beam_Pms.Text, 0.0);
            beamDes.Pss = MyList.StringToDouble(txt_steel_beam_Pss.Text, 0.0);
            beamDes.Pbs = MyList.StringToDouble(txt_steel_beam_Pbs.Text, 0.0);


            beamDes.sectionName = uC_SteelSections_Beam.cmb_section_name.Text + " " + uC_SteelSections_Beam.cmb_code1.Text;
            beamDes.h = MyList.StringToDouble(uC_SteelSections_Beam.txt_h.Text, 0.0);
            beamDes.tw = MyList.StringToDouble(uC_SteelSections_Beam.txt_tw.Text, 0.0);
            beamDes.Ixx = MyList.StringToDouble(uC_SteelSections_Beam.txt_Ixx.Text, 0.0);
            beamDes.h1 = MyList.StringToDouble(uC_SteelSections_Beam.txt_h1.Text, 0.0);
            beamDes.h2 = MyList.StringToDouble(uC_SteelSections_Beam.txt_h2.Text, 0.0);
            beamDes.w = MyList.StringToDouble(uC_SteelSections_Beam.txt_w.Text, 0.0);
            beamDes.Z = MyList.StringToDouble(uC_SteelSections_Beam.txt_Z.Text, 0.0);



            #endregion Chiranjit [2015 05 01]

            #endregion Read User Input


            beamDes.Report_File = Beam_Design_Report;

            beamDes.Calculate_Program();

            //File.WriteAllLines(
            MessageBox.Show("Report file created in file " + Beam_Design_Report);

            iApp.View_Result(Beam_Design_Report);

            Button_Enabled_Disabled();

        }

        //public  string Beam_Design_Report { get; set; }}



        #region Create Project / Open Project



        private void Open_Project()
        {
            #region Design Option

            try
            {
                iApp.Read_Form_Record(this, user_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option
        }

        private bool Check_Project_Folder()
        {
            if (Path.GetFileName(user_path) != Project_Name)
            {
                MessageBox.Show(this, "New Project is not created. Please create New Project.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }


        public string user_path { get; set; }

        //const string Title = "DESIGN OF RE (Reinforced Earth) WALL";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF STEEL BEAMS [BS]";
                return "DESIGN OF STEEL BEAMS [IS]";
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
                return eASTRADesignType.Steel_Beam;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]



        private void frmSteelBeamDesign_Load(object sender, EventArgs e)
        {
            Set_Project_Name();
            Button_Enabled_Disabled();
        }


        void Button_Enabled_Disabled()
        {
            btn_steel_beam_design.Enabled = Directory.Exists(user_path);
            btn_steel_beam_open_design.Enabled = File.Exists(Beam_Design_Report);
        }
        public string Beam_Design_Report
        {
            get
            {
                if (user_path == null) return "";
                return Path.Combine(user_path, "STEEL_BEAM_DESIGN_REPORT.TXT");
            }
        }
    }
}
