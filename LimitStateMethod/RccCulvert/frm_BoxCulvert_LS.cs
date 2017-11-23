using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using BridgeAnalysisDesign;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace LimitStateMethod.RccCulvert
{
    public partial class frm_BoxCulvert_LS : Form
    {
        IApplication iApp;

        public frm_BoxCulvert_LS(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            
        }

        private void frm_BoxCulvert_LS_Load(object sender, EventArgs e)
        {
            uC_BoxCulvert1.SetIApplication(iApp);

            Set_Project_Name();

        }
        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {

            iApp.Save_Form_Record(this, user_path);
        }

        private void btn_TGirder_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_TGirder_browse.Name)
            {
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //user_path = Path.Combine(user_path, Project_Name);
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                        Write_All_Data();
                    }
                    #endregion Save As

                    
                }
            }
            else if (btn.Name == btn_TGirder_new_design.Name)
            {
                Create_Project();
            }

            uC_BoxCulvert1.Enabled = (Directory.Exists(user_path));

            uC_BoxCulvert1.Button_Enable_Disable();

        }

        #region Chiranjit [2016 09 07]

        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.BOX_CULVERT_LSM;
            }
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

        public string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
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

            Write_All_Data();


            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "RCC BOX CULVERT DESIGN LIMIT STATE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "RCC BOX CULVERT DESIGN LIMIT STATE [LRFD]";

                return "RCC BOX CULVERT DESIGN LIMIT STATE [IRC]";
            }
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

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]

        private void uC_BoxCulvert1_Load(object sender, EventArgs e)
        {

        }

    }
}
