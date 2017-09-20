using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign.Piers;
using AstraInterface.TrussBridge;

using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign;

namespace LimitStateMethod.PierDesign
{
    public partial class frm_Pier_Stone_Masonry_Design : Form
    {
        StoneMasonryPiers stone_masonry = null;
        IApplication iApp = null;
        public frm_Pier_Stone_Masonry_Design(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            stone_masonry = new StoneMasonryPiers(app);
        }

        #region Design Of Stome Masonry Pier

        private void btn_Stone_Masonry_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(stone_masonry.rep_file_name);
            
        }
        string user_path { get; set; }
        private void btn_Stone_Masonry_Process_Click(object sender, EventArgs e)
        {
            stone_masonry.FilePath = user_path;
            Stone_Masonry_Initialize_InputData();
            stone_masonry.Calculate_Program(stone_masonry.rep_file_name);
            stone_masonry.Write_User_Input();
            stone_masonry.Write_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(stone_masonry.rep_file_name)) { MessageBox.Show(this, "Report file written in " + stone_masonry.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(stone_masonry.rep_file_name); }
            stone_masonry.is_process = true;
            Button_Enable_Disable();
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

        private void Button_Enable_Disable()
        {
            //throw new NotImplementedException();
        }
        public void Stone_Masonry_Initialize_InputData()
        {
            #region Variable Initialization
            stone_masonry.w1 = MyList.StringToDouble(txt_w1.Text, 0.0);
            stone_masonry.w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            stone_masonry.e = MyList.StringToDouble(txt_e.Text, 0.0);
            stone_masonry.b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            stone_masonry.b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            stone_masonry.l = MyList.StringToDouble(txt_l.Text, 0.0);
            stone_masonry.h = MyList.StringToDouble(txt_h.Text, 0.0);
            stone_masonry.HFL = MyList.StringToDouble(txt_HFL.Text, 0.0);
            stone_masonry.w3 = MyList.StringToDouble(txt_w3.Text, 0.0);
            stone_masonry.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            stone_masonry.f1 = MyList.StringToDouble(txt_f1.Text, 0.0);
            stone_masonry.f2 = MyList.StringToDouble(txt_f2.Text, 0.0);
            stone_masonry.A = MyList.StringToDouble(txt_A.Text, 0.0);
            stone_masonry.F = MyList.StringToDouble(txt_F.Text, 0.0);
            stone_masonry.V = MyList.StringToDouble(txt_V.Text, 0.0);
            #endregion

        }


        public void Stone_Masonry_Read_From_File()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(stone_masonry.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "w1":
                            stone_masonry.w1 = mList.GetDouble(1);
                            txt_w1.Text = stone_masonry.w1.ToString();
                            break;

                        case "w2":
                            stone_masonry.w2 = mList.GetDouble(1);
                            txt_w2.Text = stone_masonry.w2.ToString();
                            break;

                        case "e":
                            stone_masonry.e = mList.GetDouble(1);
                            txt_e.Text = stone_masonry.e.ToString();
                            break;

                        case "b1":
                            stone_masonry.b1 = mList.GetDouble(1);
                            txt_b1.Text = stone_masonry.b1.ToString();
                            break;

                        case "b2":
                            stone_masonry.b2 = mList.GetDouble(1);
                            txt_b2.Text = stone_masonry.b2.ToString();
                            break;

                        case "l":
                            stone_masonry.l = mList.GetDouble(1);
                            txt_l.Text = stone_masonry.l.ToString();
                            break;

                        case "h":
                            stone_masonry.h = mList.GetDouble(1);
                            txt_h.Text = stone_masonry.h.ToString();
                            break;

                        case "HFL":
                            stone_masonry.HFL = mList.GetDouble(1);
                            txt_HFL.Text = stone_masonry.HFL.ToString();
                            break;

                        case "w3":
                            stone_masonry.w3 = mList.GetDouble(1);
                            txt_w3.Text = stone_masonry.w3.ToString();
                            break;

                        case "gamma_c":
                            stone_masonry.gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = stone_masonry.gamma_c.ToString();
                            break;

                        case "f1":
                            stone_masonry.f1 = mList.GetDouble(1);
                            txt_f1.Text = stone_masonry.f1.ToString();
                            break;

                        case "f2":
                            stone_masonry.f2 = mList.GetDouble(1);
                            txt_f2.Text = stone_masonry.f2.ToString();
                            break;

                        case "A":
                            stone_masonry.A = mList.GetDouble(1);
                            txt_A.Text = stone_masonry.A.ToString();
                            break;

                        case "F":
                            stone_masonry.F = mList.GetDouble(1);
                            txt_F.Text = stone_masonry.F.ToString();
                            break;
                        case "V":
                            stone_masonry.V = mList.GetDouble(1);
                            txt_V.Text = stone_masonry.V.ToString();
                            throw new Exception("DATA_INITIALIZED");
                            break;
                    }
                    #endregion
                    #region USER INPUT DATA

                    //sw.WriteLine(" = {0:f3} ", w1);
                    //sw.WriteLine(" = {0:f3} ", w2);
                    //sw.WriteLine(" = {0:f3} ", e);
                    //sw.WriteLine(" = {0:f3} ", b1);
                    //sw.WriteLine(" = {0:f3} ", b2);
                    //sw.WriteLine(" = {0:f3} ", l);
                    //sw.WriteLine(" = {0:f3} ", h);
                    //sw.WriteLine(" = {0:f3} ", );
                    //sw.WriteLine(" = {0:f3} ", w3);
                    //sw.WriteLine(" = {0:f3} ", gamma_c);
                    //sw.WriteLine(" = {0:f3} ", f1);
                    //sw.WriteLine(" = {0:f3} ", f2);
                    //sw.WriteLine(" = {0:f3} ", A);
                    //sw.WriteLine(" = {0:f3} ", F);
                    //sw.WriteLine(" = {0:f3} ", V);

                    #endregion
                }


            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        #endregion Design Of Stome Masonry Pier

        private void btn_dwg_stone_interactive_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(stone_masonry.drawing_path, "Pier", "");
        }

        #region Chiranjit [2012 07 10]
        //Write All Data in a File
        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            if (user_path != iApp.LastDesignWorkingFolder)
                iApp.Save_Form_Record(this, user_path);
        }
        public void Read_All_Data()
        {
            //if (iApp.IsDemo) return;

            try
            {

            }
            catch (Exception ex) { }
            Button_Enable_Disable();
        }
        #endregion Chiranjit [2012 07 10]

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF STONE MASONRY PIER [BS]";
                return "DESIGN OF STONE MASONRY PIER [IRC]";
            }
        }

        #region Chiranjit [2016 09 07

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    stone_masonry.user_path = user_path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreateData = true;
                //}

                //IsCreateData = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
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
                return eASTRADesignType.PSC_I_Girder_Bridge_LS;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]

        private void frm_Pier_Stone_Masonry_Design_Load(object sender, EventArgs e)
        {
            Set_Project_Name();
        }

    }
}
