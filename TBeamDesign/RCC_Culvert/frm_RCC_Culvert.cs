using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraFunctionOne;


namespace BridgeAnalysisDesign.RCC_Culvert
{
    public partial class frm_RCC_Culvert : Form
    {
        
        //const string Title = "ANALYSIS OF RCC CULVERT";


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC CULVERT [BS]";
                if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "DESIGN OF RCC CULVERT [LRFD]";
                return "DESIGN OF RCC CULVERT [IRC]";
            }
        }



        IApplication iApp = null;

        SingleCell_BoxCulvert sc_box = null;
        MultiCell_BoxCulvert_Analysis mcb_ana = null;

        bool IsOpen = false;

        bool Is_MCB_Create = false;



        SlabCulvert slab = null;
        PipeCulvert pipe = null;
        public frm_RCC_Culvert(AstraInterface.Interface.IApplication app)
        {
            
            InitializeComponent();
            iApp = app;
            this.Text = Title + " : " + AstraInterface.DataStructure.MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            //iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);

            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


            if (!System.IO.Directory.Exists(iApp.LastDesignWorkingFolder))
                System.IO.Directory.CreateDirectory(iApp.LastDesignWorkingFolder);

            sc_box = new SingleCell_BoxCulvert(iApp);
            pipe = new PipeCulvert(iApp);
            slab = new SlabCulvert(iApp);
            mcb_ana = new MultiCell_BoxCulvert_Analysis(iApp);
        }
        public string user_path 
        { 
            get
            {

                string dir = "";

                dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                iApp.user_path = dir;

                if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Single_Cell)
                    dir = Path.Combine(dir, "Single Cell Box Culvert Design");
                else if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Multi_Cell)
                    dir = Path.Combine(dir, "Multi Cell Box Culvert Design");
                else if (Project_Type == eASTRADesignType.RCC_Slab_Culvert)
                    dir = Path.Combine(dir, "Slab Culvert Design");
                else if (Project_Type == eASTRADesignType.RCC_Pipe_Culvert)
                    dir = Path.Combine(dir, "Pipe Culvert Design");

                dir = Path.Combine(dir, Project_Name);

                return dir;
            }
            set
            {
                Project_Name = Path.GetFileName(value);
                iApp.user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            }
        }


        public string Get_User_path(eASTRADesignType prjType)
        {
            {
                string dir = "";

                dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                iApp.user_path = dir;

                if (prjType == eASTRADesignType.RCC_Box_Culvert_Single_Cell)
                    dir = Path.Combine(dir, "Single Cell Box Culvert Design");
                else if (prjType == eASTRADesignType.RCC_Box_Culvert_Multi_Cell)
                    dir = Path.Combine(dir, "Multi Cell Box Culvert Design");
                else if (prjType == eASTRADesignType.RCC_Slab_Culvert)
                    dir = Path.Combine(dir, "Slab Culvert Design");
                else if (prjType == eASTRADesignType.RCC_Pipe_Culvert)
                    dir = Path.Combine(dir, "Pipe Culvert Design");

                return dir;
            }
        }

        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {

                string dir = Path.Combine(iApp.user_path, "DRAWINGS");


                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);


                return dir;
            }
        }

        #region Box Culvert
        public void Single_Cell_Box_Initialize_InputData()
        {
            #region Variable Initialization

            sc_box.H = MyList.StringToDouble(txt_box_H.Text, 0.0);
            sc_box.b = MyList.StringToDouble(txt_box_b.Text, 0.0);
            sc_box.d = MyList.StringToDouble(txt_box_d.Text, 0.0);
            sc_box.d1 = MyList.StringToDouble(txt_box_d1.Text, 0.0);
            sc_box.d2 = MyList.StringToDouble(txt_box_d2.Text, 0.0);
            sc_box.d3 = MyList.StringToDouble(txt_box_d3.Text, 0.0);
            sc_box.gamma_b = MyList.StringToDouble(txt_box_gamma_b.Text, 0.0);
            sc_box.gamma_c = MyList.StringToDouble(txt_box_gamma_c.Text, 0.0);
            sc_box.R = MyList.StringToDouble(txt_box_R.Text, 0.0);
            //box.t = MyList.StringToDouble(txt_box_t.Text, 0.0);
            sc_box.bar_dia_top = MyList.StringToDouble(txt_box_bar_dia_top.Text, 0.0);
            sc_box.bar_dia_bottom = MyList.StringToDouble(txt_box_bar_dia_bottom.Text, 0.0);
            sc_box.bar_dia_side = MyList.StringToDouble(txt_box_bar_dia_side.Text, 0.0);
       
            sc_box.cover = MyList.StringToDouble(txt_box_cover.Text, 0.0);
            sc_box.b1 = MyList.StringToDouble(txt_box_b1.Text, 0.0);
            sc_box.b2 = MyList.StringToDouble(txt_box_b2.Text, 0.0);
            sc_box.a1 = MyList.StringToDouble(txt_box_a1.Text, 0.0);
            sc_box.w1 = MyList.StringToDouble(txt_box_w1.Text, 0.0);
            sc_box.w2 = MyList.StringToDouble(txt_box_w2.Text, 0.0);
            sc_box.b3 = MyList.StringToDouble(txt_box_b3.Text, 0.0);
            sc_box.F = MyList.StringToDouble(txt_box_F.Text, 0.0);
            sc_box.S = MyList.StringToDouble(txt_box_S.Text, 0.0);
            sc_box.sbc = MyList.StringToDouble(txt_box_sbc.Text, 0.0);

            int grade = (int)sc_box.sigma_c;

            sc_box.Con_Grade = (CONCRETE_GRADE)grade;
            //box.sigma_st = MyList.StringToDouble(txt_box_sigma_st.Text, 0.0);
            //box.sigma_c = MyList.StringToDouble(txt_box_sigma_c.Text, 0.0);

            sc_box.sigma_c = MyList.StringToDouble(cmb_box_fck.Text, 0.0);
            sc_box.sigma_st = MyList.StringToDouble(cmb_box_fy.Text, 0.0);
            
            #endregion

        }
        public void Box_Read_From_File(string fileName)
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(fileName));
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
                        case "H":
                            sc_box.H = mList.GetDouble(1);
                            txt_box_H.Text = sc_box.H.ToString();
                            break;
                        case "b":
                            sc_box.b = mList.GetDouble(1);
                            txt_box_b.Text = sc_box.b.ToString();
                            break;
                        case "d":
                            sc_box.d = mList.GetDouble(1);
                            txt_box_d.Text = sc_box.d.ToString();
                            break;
                        case "d1":
                            sc_box.d1 = mList.GetDouble(1);
                            txt_box_d1.Text = sc_box.d1.ToString();
                            break;
                        case "d2":
                            sc_box.d2 = mList.GetDouble(1);
                            txt_box_d2.Text = sc_box.d2.ToString();
                            break;
                        case "d3":
                            sc_box.d3 = mList.GetDouble(1);
                            txt_box_d3.Text = sc_box.d3.ToString();
                            break;
                        case "gamma_b":
                            sc_box.gamma_b = mList.GetDouble(1);
                            txt_box_gamma_b.Text = sc_box.gamma_b.ToString();
                            break;
                        case "gamma_c":
                            sc_box.gamma_c = mList.GetDouble(1);
                            txt_box_gamma_c.Text = sc_box.gamma_c.ToString();
                            break;
                        case "R":
                            sc_box.R = mList.GetDouble(1);
                            txt_box_R.Text = sc_box.R.ToString();
                            break;
                        case "t":
                            sc_box.t = mList.GetDouble(1);
                            //txt_box_t.Text = box.t.ToString();
                            break;
                        case "j":
                            sc_box.j = mList.GetDouble(1);
                            //txt_box_j.Text = box.j.ToString();
                            break;
                        case "cover":
                            sc_box.cover = mList.GetDouble(1);
                            txt_box_cover.Text = sc_box.cover.ToString();
                            break;
                        case "sigma_c":
                            sc_box.sigma_c = mList.GetDouble(1);
                            txt_box_sigma_c.Text = sc_box.sigma_c.ToString();
                            break;

                        case "sigma_st":
                            sc_box.sigma_st = mList.GetDouble(1);
                            txt_box_sigma_st.Text = sc_box.sigma_st.ToString();
                            break;
                        case "b1":
                            sc_box.b1 = mList.GetDouble(1);
                            txt_box_b1.Text = sc_box.b1.ToString();
                            break;
                        case "b2":
                            sc_box.b2 = mList.GetDouble(1);
                            txt_box_b2.Text = sc_box.b2.ToString();
                            break;
                        case "a1":
                            sc_box.a1 = mList.GetDouble(1);
                            txt_box_a1.Text = sc_box.a1.ToString();
                            break;
                        case "w1":
                            sc_box.w1 = mList.GetDouble(1);
                            txt_box_w1.Text = sc_box.w1.ToString();
                            break;
                        case "w2":
                            sc_box.w2 = mList.GetDouble(1);
                            txt_box_w2.Text = sc_box.w2.ToString();
                            break;
                        case "b3":
                            sc_box.b3 = mList.GetDouble(1);
                            txt_box_b3.Text = sc_box.b3.ToString();
                            break;
                        case "F":
                            sc_box.F = mList.GetDouble(1);
                            txt_box_F.Text = sc_box.F.ToString();
                            break;
                        case "S":
                            sc_box.S = mList.GetDouble(1);
                            txt_box_S.Text = sc_box.S.ToString();
                            break;
                        case "sbc":
                            sc_box.sbc = mList.GetDouble(1);
                            txt_box_sbc.Text = sc_box.sbc.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }

        private void btn_Box_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(sc_box.rep_file_name);
        }
        private void btn_Box_Process_Click(object sender, EventArgs e)
        {
            DemoCheck();

            //sc_box.FilePath = iApp.LastDesignWorkingFolder;

            
            sc_box.Project_Name = Project_Name;
            sc_box.FilePath = Path.GetDirectoryName( user_path);
            Single_Cell_Box_Initialize_InputData();
            sc_box.WriteUserInput();
            sc_box.Calculate_Program(sc_box.rep_file_name);
            sc_box.Write_Drawing();
            iApp.Save_Form_Record(tab_single_cell, user_path);


            MessageBox.Show(this, "Report file written in " + sc_box.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(sc_box.rep_file_name);
            sc_box.is_process = true;
            Button_Enable_Disable();
        }
        private void txt_Box_sigma_c_TextChanged(object sender, EventArgs e)
        {
            Single_Cell_Box_Initialize_InputData();
            //double fck, fcc, j, Q, fcb, n;

            //fck = sigma_c;

            //fcb = fck / 3;
            //fcc = fck / 4;

            //n = m * fcb / (modu * fcb * sigma_st);

            //j = 1 - (n / 3.0);
            //Q = n * j * fcb / 2;

            ////txt_sigma_cb.Text = fcb.ToString("0.00");
            //txt_j.Text = j.ToString("0.000");
            //txt_Q.Text = Q.ToString("0.000");
        }
        #endregion  Box Culvert

        #region Slab Culvert
        public void Slab_Read_From_File()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(slab.user_input_file));
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
                        case "D":
                            slab.D = mList.GetDouble(1);
                            txt_D.Text = slab.D.ToString();
                            break;
                        case "CW":
                            slab.CW = mList.GetDouble(1);
                            txt_CW.Text = slab.CW.ToString();
                            break;
                        case "FP":
                            slab.FP = mList.GetDouble(1);
                            txt_FP.Text = slab.FP.ToString();
                            break;
                        case "L":
                            slab.L = mList.GetDouble(1);
                            txt_L.Text = slab.L.ToString();
                            break;
                        case "support_width":
                            slab.support_width = mList.GetDouble(1);
                            txt_width_support.Text = slab.support_width.ToString();
                            break;
                        case "W":
                            slab.W1 = mList.GetDouble(1);
                            txt_W1.Text = slab.W1.ToString();
                            break;
                        case "conc_grade":
                            slab.conc_grade = mList.GetDouble(1);
                            //txt_concrete_grade.Text = slab.conc_grade.ToString();
                            break;
                        case "st_grade":
                            slab.st_grade = mList.GetDouble(1);
                            //txt_steel_grade.Text = slab.st_grade.ToString();
                            break;
                        case "sigma_cb":
                            slab.sigma_cb = mList.GetDouble(1);
                            //txt_sigma_cb.Text = slab.sigma_cb.ToString();
                            break;
                        case "sigma_st":
                            slab.sigma_st = mList.GetDouble(1);
                            //txt_sigma_st.Text = slab.sigma_st.ToString();
                            break;

                        case "m":
                            slab.m = mList.GetDouble(1);
                            //txt_m.Text = slab.m.ToString();
                            break;
                        case "j":
                            slab.j = mList.GetDouble(1);
                            //txt_j.Text = slab.j.ToString();
                            break;
                        case "Q":
                            slab.Q = mList.GetDouble(1);
                            txt_pipe_Q.Text = slab.Q.ToString();
                            break;
                        case "a1":
                            slab.a1 = mList.GetDouble(1);
                            txt_a1.Text = slab.a1.ToString();
                            break;
                        case "b1":
                            slab.b1 = mList.GetDouble(1);
                            txt_b1.Text = slab.b1.ToString();
                            break;
                        case "W1":
                            slab.W1 = mList.GetDouble(1);
                            txt_W1.Text = slab.W1.ToString();
                            break;
                        case "cover":
                            slab.cover = mList.GetDouble(1);
                            txt_cover.Text = slab.cover.ToString();
                            break;
                        case "delta_c":
                            slab.delta_c = mList.GetDouble(1);
                            txt_delta_c.Text = slab.delta_c.ToString();
                            break;
                        case "delta_wc":
                            slab.delta_wc = mList.GetDouble(1);
                            txt_delta_wc.Text = slab.delta_wc.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        void Slab_Initialize_InputData()
        {
            slab.D = MyList.StringToDouble(txt_D.Text, 0.0);
            slab.CW = MyList.StringToDouble(txt_CW.Text, 0.0);
            slab.FP = MyList.StringToDouble(txt_FP.Text, 0.0);
            slab.L = MyList.StringToDouble(txt_L.Text, 0.0);
            slab.WC = MyList.StringToDouble(txt_WC.Text, 0.0);
            slab.support_width = MyList.StringToDouble(txt_width_support.Text, 0.0);
          
            slab.a1 = MyList.StringToDouble(txt_a1.Text, 0.0);
            slab.b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            slab.b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            slab.W1 = MyList.StringToDouble(txt_W1.Text, 0.0);
            slab.cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            slab.delta_c = MyList.StringToDouble(txt_delta_c.Text, 0.0);
            slab.delta_wc = MyList.StringToDouble(txt_delta_wc.Text, 0.0);
            slab.bar_dia = MyList.StringToDouble(txt_slab_bar_dia_top.Text, 0.0);


            slab.conc_grade = MyList.StringToDouble(cmb_slab_fck.Text, 0.0);
            slab.CON_GRADE = (CONCRETE_GRADE)(int)slab.conc_grade;
            slab.st_grade = MyList.StringToDouble(cmb_slab_fy.Text, 0.0);
            slab.sigma_cb = MyList.StringToDouble(txt_slab_sigma_c.Text, 0.0);
            slab.sigma_st = MyList.StringToDouble(txt_slab_sigma_st.Text, 0.0);
            slab.m = MyList.StringToDouble(txt_slab_m.Text, 0.0);
            slab.Q = MyList.StringToDouble(txt_slab_Q.Text, 0.0);
        }

        private void btn_Slab_Process_Click(object sender, EventArgs e)
        {
            DemoCheck();
            slab.Project_Name = txt_slab_project.Text;
            slab.FilePath = Path.GetDirectoryName(user_path);

            Slab_Initialize_InputData();
            slab.WriteUserInput();
            slab.Calculate_Program(slab.rep_file_name);
            slab.Write_Drawing();
            iApp.Save_Form_Record(tab_slab_culvert, slab.file_path);
            MessageBox.Show(this, "Report file written in " + slab.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(slab.rep_file_name);
            slab.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_Box_Drawing_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_dwg_box_culvert_single.Name)
                iApp.SetDrawingFile_Path(sc_box.drawing_path, "Box_Culvert_Interactive", "Box_Culvert");
            else if (btn.Name == btn_dwg_box_culvert_multicell.Name)
            {
                if (rbtn_dwg_multi_without_earth_cusion.Checked)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITHOUT_EARTH_CUSION,
                        Path.Combine(mcb_ana.file_path, "DRAWINGS"),
                        mcb_ana.rep_file_name).ShowDialog();
                }
                else
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITH_EARTH_CUSION,
                        Path.Combine(mcb_ana.file_path, "DRAWINGS"),
                        mcb_ana.rep_file_name).ShowDialog();
                }
            }
        }
        private void btn_Slab_Drawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(slab.drawing_path, "Slab_Culvert_Interactive", "Slab_Culvert");
        }
        private void btn_Slab_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(slab.rep_file_name);
        }
        private void txt_concrete_grade_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion Slab Culvert


        #region Pipe Culvert

        void Pipe_Initialize_InputData()
        {
            pipe.Q = MyList.StringToDouble(txt_pipe_Q.Text, 0.0);
            pipe.V = MyList.StringToDouble(txt_pipe_V.Text, 0.0);
            pipe.B = MyList.StringToDouble(txt_pipe_B.Text, 0.0);
            pipe.H1 = MyList.StringToDouble(txt_pipe_H1.Text, 0.0);
            pipe.H2 = MyList.StringToDouble(txt_pipe_H2.Text, 0.0);
            pipe.W = MyList.StringToDouble(txt_pipe_W.Text, 0.0);
            pipe.I = MyList.StringToDouble(txt_pipe_I.Text, 0.0);

            pipe.pipe_dia = Pipe_Dia;
            pipe.long_reinf = MyList.StringToDouble(txt_pipe_long_reinf.Text, 0.0);
            pipe.spiral_reinf = MyList.StringToDouble(txt_pipe_spiral_reinf.Text, 0.0);
            pipe.ultimate_load = MyList.StringToDouble(txt_pipe_ult_ld.Text, 0.0);

            if (rbtn_NP3.Checked)
            {
                pipe.culvert_class = "NP-3";
            }
            else
            {
                pipe.culvert_class = "NP-4";
            }
        }
        public void Pipe_Read_From_File()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(pipe.user_input_file));
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

                    //sw.WriteLine("={0}", Q);
                    //sw.WriteLine("={0}", V);
                    //sw.WriteLine("={0}", B);
                    //sw.WriteLine("={0}", H1);
                    //sw.WriteLine("={0}", H2);
                    //sw.WriteLine("W={0}", W);
                    //sw.WriteLine("I={0}", I);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "Q":
                            pipe.Q = mList.GetDouble(1);
                            txt_pipe_Q.Text = pipe.Q.ToString();
                            break;
                        case "V":
                            pipe.V = mList.GetDouble(1);
                            txt_pipe_V.Text = pipe.V.ToString();
                            break;
                        case "B":
                            pipe.B = mList.GetDouble(1);
                            txt_pipe_B.Text = pipe.B.ToString();
                            break;
                        case "H1":
                            pipe.H1 = mList.GetDouble(1);
                            txt_pipe_H1.Text = pipe.H1.ToString();
                            break;
                        case "H2":
                            pipe.H2 = mList.GetDouble(1);
                            txt_pipe_H2.Text = pipe.H2.ToString();
                            break;
                        case "W":
                            pipe.W = mList.GetDouble(1);
                            txt_pipe_W.Text = pipe.W.ToString();
                            break;
                        case "I":
                            pipe.I = mList.GetDouble(1);
                            txt_pipe_I.Text = pipe.I.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }

        private void btn_Pipe_Drawing_Click(object sender, EventArgs e)
        {
            //iApp.SetDrawingFile_Path(pipe.rep_file_name, "", "Pipe_Culvert");
            //iApp.OpenDefaultDrawings(Path.GetDirectoryName(pipe.rep_file_name), "Pipe_Culvert");
            //iApp.RunViewer(Path.Combine(Drawing_Folder,"RCC Pipe Culvert Drawings"), "Pipe_Culvert");

            iApp.Form_Drawing_Editor(eBaseDrawings.RCC_CULVERT_PIPE, Path.Combine(pipe.file_path, "DRAWINGS"), pipe.rep_file_name).ShowDialog();

        }
        private void btn_Pipe_Process_Click(object sender, EventArgs e)
        {
            DemoCheck();
            pipe.Project_Name = txt_pipe_project.Text;
            pipe.FilePath = Path.GetDirectoryName(user_path);

            Pipe_Initialize_InputData();

            pipe.Calculate_Program();
            pipe.Pipe_WriteUserInput();
            pipe.is_process = true;
            iApp.Save_Form_Record(tab_pipe_culvert, pipe.file_path);
            MessageBox.Show(this, "Report file written in " + pipe.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(pipe.rep_file_name);
            Button_Enable_Disable();
        }
        private void btn_Pipe_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(pipe.rep_file_name);
        }
        private void rbtn_Pipe_Highway_CheckedChanged(object sender, EventArgs e)
        {
            pipe.IsHighway = rbtn_pipe_Highway.Checked;
        }

        #endregion Pipe Culvert

        private void btn_dwg_box_Click(object sender, EventArgs e)
        {
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            //iApp.RunViewer(user_path, drawing_command);
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Select a Drawing item from the list.", "ASTRA", MessageBoxButtons.OK);
                return;
            }
            string drawing_command, User_Drawing_Folder;
            drawing_command = User_Drawing_Folder = "";

            switch (listBox1.SelectedIndex)
            {
                case 0:
                    drawing_command = "Box_Culvert_Single_Cell";
                    User_Drawing_Folder = "Box_Culvert_Single_Cell";
                    break;

                case 1:
                    drawing_command = "Box_Culvert_Double_Cell";
                    User_Drawing_Folder = "Box_Culvert_Double_Cell";
                    break;

                case 2:
                    drawing_command = "Box_Culvert_3_Cell";
                    User_Drawing_Folder = "Box_Culvert_Tripple_Cell";
                    break;
            }
            //User_Drawing_Folder = Path.Combine(Drawing_Folder, User_Drawing_Folder);
            //if (!Directory.Exists(User_Drawing_Folder))
            //    Directory.CreateDirectory(User_Drawing_Folder);
            iApp.RunViewer(Path.Combine(Drawing_Folder, User_Drawing_Folder), drawing_command);
        }

        private void frm_RCC_Culvert_Load(object sender, EventArgs e)
        {

            tc_box.TabPages.Remove(tab_single_cell);



            tc_multicell.TabPages.Remove(tab_box_design);




            pic_box.BackgroundImage = AstraFunctionOne.ImageCollection.Box_Culvert;
            pictureBox1.BackgroundImage = AstraFunctionOne.ImageCollection.Box_Culvert;
            pic_slab.BackgroundImage = AstraFunctionOne.ImageCollection.SLAB_Culvert;
            pic_pipe1.BackgroundImage = AstraFunctionOne.ImageCollection.PIPE_Culvert_1;
            pic_pipe2.BackgroundImage = AstraFunctionOne.ImageCollection.PIPE_Culvert_2;

            this.pcb_mcb_1.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Multicell_Box_Culvert_Pressure_fig1;
            this.pcb_mcb_2.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Multicell_Box_Culvert_Pressure_fig2;


            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;

            cmb_pipe_dia.Items.Clear();
            foreach (var item in iApp.Tables.PipeCulvert.Class_NP3)
            {
                cmb_pipe_dia.Items.Add(item.Internal_Diameter);
            }

            if (cmb_pipe_dia.Items.Count > 0)
                cmb_pipe_dia.SelectedItem = 1000.0;

            cmb_box_fck.SelectedIndex = 2;
            cmb_box_fy.SelectedIndex = 1;

            cmb_mcb_cell_nos.SelectedIndex = 1;

            cmb_mcb_fck.SelectedIndex = 2;
            cmb_mcb_fy.SelectedIndex = 1;
            cmb_slab_fck.SelectedIndex = 2;
            cmb_slab_fy.SelectedIndex = 1;

            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            Button_Enable_Disable();


            Set_Project_Name();
            if (false)
            {
                #region Chiranjit Design Option

                try
                {
                    eDesignOption edp = iApp.Get_Design_Option(Title);
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                    if (edp == eDesignOption.None)
                    {
                        this.Close();
                    }
                    else if (edp == eDesignOption.Open_Design)
                    {

                        iApp.Read_Form_Record(this, user_path);

                        //iApp.LastDesignWorkingFolder = user_path;

                        IsOpen = true;

                        Is_MCB_Create = true;
                        prss = 4;
                        sc_box.IsOpen = IsOpen;
                        sc_box.FilePath = user_path;
                        mcb_ana.IsOpen = IsOpen;
                        mcb_ana.FilePath = user_path;
                        slab.IsOpen = IsOpen;
                        slab.FilePath = user_path;
                        pipe.IsOpen = IsOpen;
                        pipe.FilePath = user_path;

                        Format_Multi_Cell_Load_Grids();
                        Button_Enable_Disable();

                        if (File.Exists(mcb_ana.User_Input_File))
                        {
                            Multi_Cell_Box_Initialize_InputData();
                            mcb_ana.Create_Data();
                            mcb_ana.Set_Loads_From_Grid(dgv_dl_sidl, dgv_ep_l, dgv_ll_loads, dgv_load_comb);
                        }
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    Button_Enable_Disable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Input data file Error..");
                }
                #endregion Chiranjit Design Option
            }

        }
        void Button_Enable_Disable()
        {
            btn_box_Process.Enabled = true;
            btn_box_Report.Enabled = File.Exists(sc_box.rep_file_name);
            btn_dwg_box_culvert_single.Enabled = File.Exists(sc_box.drawing_path);

            btn_dwg_box_culvert_multicell.Enabled = File.Exists(mcb_ana.User_Input_File);


            

            btn_slab_process.Enabled = true;
            btn_slab_report.Enabled = File.Exists(slab.rep_file_name);
            btn_dwg_slab.Enabled = File.Exists(slab.drawing_path);

            btn_pipe_Process.Enabled = true;
            btn_pipe_Report.Enabled = File.Exists(pipe.rep_file_name);
            //btn_dwg_pipe.Enabled = File.Exists(pipe.u);


            if (mcb_ana != null)
            {
                //btn_mcb_save_data.Enabled = File.Exists(mcb_ana.User_Input_File);

                Is_MCB_Create = true;
                if (Is_MCB_Create)
                {
                    btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(mcb_ana.User_Input_File));
                    btn_mcb_view_data.Enabled = File.Exists(mcb_ana.User_Input_File);
                    btn_mcb_view_sreucture.Enabled = File.Exists(mcb_ana.User_Input_File);
                    btn_mcb_process_analysis.Enabled = File.Exists(mcb_ana.User_Input_File);
                    btn_mcb_process_design.Enabled = File.Exists(MyList.Get_Analysis_Report_File(mcb_ana.User_Input_File));
                    btn_mcb_open_design.Enabled = File.Exists(mcb_ana.rep_file_name);
                }
                else
                {
                    btn_view_report.Enabled = false;
                    btn_mcb_view_data.Enabled = false;
                    btn_mcb_view_sreucture.Enabled = false;
                    btn_mcb_process_analysis.Enabled = false;
                    btn_mcb_process_design.Enabled = false;
                    btn_mcb_open_design.Enabled = false;
                }
            }
        
        }

        private void cmb_deck_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied la = LoadApplied.Get_Applied_Load(cmb_deck_select_load.Text);
            txt_W1.Text = la.Total_Load.ToString();
            txt_a1.Text = la.b1_b.ToString();
            txt_b1.Text = la.a1_a.ToString();
        }

        #region Chiranjit [2012 07 04]
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_box"))
            {
                astg = new ASTRAGrade(cmb_box_fck.Text, cmb_box_fy.Text);
                txt_box_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_box_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_mcb"))
            {
                astg = new ASTRAGrade(cmb_mcb_fck.Text, cmb_mcb_fy.Text);
                txt_mcb_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_mcb_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_slab") ||
                ctrl.Name.ToLower().StartsWith("txt_slab"))
            {
                astg = new ASTRAGrade(cmb_slab_fck.Text, cmb_slab_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_slab_m.Text, 10.0);
                txt_slab_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_slab_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_slab_j.Text = astg.j.ToString("f3");
                txt_slab_Q.Text = astg.Q.ToString("f3");
            }
        }
        #endregion Chiranjit [2012 07 04]

        #region Chiranjit [2012 07 20]
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_box_H.Text = "5.0";
                txt_box_b.Text = "3.0";
                txt_box_d.Text = "3.0";

                txt_D.Text = "500.0";
                txt_CW.Text = "7.50";
                txt_FP.Text = "1.0";
                txt_L.Text = "6.0";

                txt_pipe_B.Text = "7.5";
                txt_pipe_H1.Text = "100.0";
                txt_pipe_H2.Text = "103.0";


                //string str = "ASTRA USB Lock not found at any port.\n\nThis is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "   Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "  Email at : techsoft@consultant.com, dataflow@mail.com\n";
                //str += "Contact No : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion Chiranjit [2012 07 20]

        private void rbtn_NP4_CheckedChanged(object sender, EventArgs e)
        {
            double d = Pipe_Dia;
            if (rbtn_NP3.Checked)
            {
                cmb_pipe_dia.Items.Clear();
                foreach (var item in iApp.Tables.PipeCulvert.Class_NP3)
                {
                    cmb_pipe_dia.Items.Add(item.Internal_Diameter);
                }
            }
            else if (rbtn_NP4.Checked)
            {
                cmb_pipe_dia.Items.Clear();
                foreach (var item in iApp.Tables.PipeCulvert.Class_NP4)
                {
                    cmb_pipe_dia.Items.Add(item.Internal_Diameter);
                }
            }
            if (cmb_pipe_dia.Items.Count > 0)
                cmb_pipe_dia.SelectedItem = d;
            Select_Pipe_Strength_Data();
        }
        public double Pipe_Dia
        {
            get
            {
                return MyList.StringToDouble(cmb_pipe_dia.Text, 0.0);
            }
        }

        private void cmb_pipe_dia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Pipe_Strength_Data();
        }

        private void Select_Pipe_Strength_Data()
        {

            PipeCulvertData pcd = iApp.Tables.PipeCulvert.Get_NP3_Data(Pipe_Dia);
            if (rbtn_NP3.Checked)
                pcd = iApp.Tables.PipeCulvert.Get_NP3_Data(Pipe_Dia);
            else if (rbtn_NP4.Checked)
                pcd = iApp.Tables.PipeCulvert.Get_NP4_Data(Pipe_Dia);

            if (pcd != null)
            {
                txt_pipe_long_reinf.Text = pcd.Longitudinal_Reinforcement.ToString("f3");
                if (chk_pipe_mild.Checked) txt_pipe_spiral_reinf.Text = pcd.Mild_Steel_Reinforcement.ToString("f3");
                else txt_pipe_spiral_reinf.Text = pcd.Spiral_Reinforcement.ToString("f3");
                txt_pipe_ult_ld.Text = pcd.Ultimate_Load.ToString("f3");
            }
        }

        private void btn_box_open_des_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = user_path;
                ofd.Filter = "Text File |*.txt";
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    string input_file = ofd.FileName;
                    user_path = Path.GetDirectoryName(input_file);
                    iApp.Read_Form_Record(this, user_path);
                    iApp.LastDesignWorkingFolder = user_path;

                    IsOpen = true;

                    Is_MCB_Create = true;
                    prss = 4;
                    sc_box.IsOpen = IsOpen;
                    sc_box.FilePath = user_path;
                    mcb_ana.IsOpen = IsOpen;
                    mcb_ana.FilePath = user_path;
                    slab.IsOpen = IsOpen;
                    slab.FilePath = user_path;
                    pipe.IsOpen = IsOpen;
                    pipe.FilePath = user_path;

                    Format_Multi_Cell_Load_Grids();
                    Button_Enable_Disable();

                    if (File.Exists(mcb_ana.User_Input_File))
                    {
                        Multi_Cell_Box_Initialize_InputData();
                        mcb_ana.Create_Data();
                        mcb_ana.Set_Loads_From_Grid(dgv_dl_sidl, dgv_ep_l, dgv_ll_loads, dgv_load_comb);
                    }
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_mcb_create_data_Click(object sender, EventArgs e)
        {
            DemoCheck();
            //mcb_ana.FilePath = iApp.LastDesignWorkingFolder;
            mcb_ana.Project_Name = txt_mc_box_project.Text;
            mcb_ana.FilePath = user_path;


            Multi_Cell_Box_Initialize_InputData();

            mcb_ana.DL_Loads.Clear();
            mcb_ana.SIDL_Loads.Clear();
            mcb_ana.EP_Loads.Clear();
            mcb_ana.Moving_Loads.Clear();
            mcb_ana.Combine_Loads.Clear();

            mcb_ana.Create_Data();
            mcb_ana.Write_Data(mcb_ana.User_Input_File);
            //mcb_ana.Set_Default_Load_Data();

            //Multi_Cell_Box_Grid_Load_Data();
            //Format_Multi_Cell_Load_Grids();
            //mcb_ana.Write_Data(mcb_ana.User_Input_File);

            //iApp.Save_Form_Record(this, user_path);
            iApp.Save_Form_Record(tab_multi_cell, mcb_ana.file_path);
            MessageBox.Show(this, "Structure Data is Created in folder " + mcb_ana.User_Input_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Is_MCB_Create = false;
            prss = 1;
            Button_Enable_Disable();
        }
        public void Multi_Cell_Box_Initialize_InputData()
        {
            if (mcb_ana.Intermediate_Members == null)
            {
                mcb_ana.Intermediate_Members = new List<int>();
            }
            #region Variable Initialization
            //mcb_ana = new MultiCell_BoxCulvert_Analysis(iApp);

            mcb_ana.De = MyList.StringToDouble(txt_mcb_De.Text, 1.0);
            mcb_ana.b = MyList.StringToDouble(txt_mcb_b.Text, 1.0);
            mcb_ana.w = MyList.StringToDouble(txt_mcb_w.Text, 1.0);



            //mcb_ana.d = MyList.StringToDouble(txt_mcb_H.Text, 1.0);



            mcb_ana.d1 = MyList.StringToDouble(txt_mcb_d1.Text, 1.0);
            mcb_ana.d2 = MyList.StringToDouble(txt_mcb_d2.Text, 1.0);
            mcb_ana.d3 = MyList.StringToDouble(txt_mcb_d3.Text, 1.0);
            mcb_ana.d4 = MyList.StringToDouble(txt_mcb_d4.Text, 1.0);

            mcb_ana.Cell_Nos = MyList.StringToInt(cmb_mcb_cell_nos.Text, 2);
            mcb_ana.Gama_b = MyList.StringToDouble(txt_mcb_gama_b.Text, 18.0);
            mcb_ana.Gama_c = MyList.StringToDouble(txt_mcb_gama_c.Text, 24.0);
            mcb_ana.Ka = MyList.StringToDouble(txt_mcb_Ka.Text, 0.19);

            mcb_ana.R = MyList.StringToDouble(txt_mcb_R.Text, 1.102);
            mcb_ana.m = MyList.StringToDouble(txt_mcb_M.Text, 1.102);
            mcb_ana.cover = MyList.StringToDouble(txt_mcb_cover.Text, 1.102);
            mcb_ana.Con_Grade = (CONCRETE_GRADE)(cmb_box_fck.SelectedIndex);
            //box.t = MyList.StringToDouble(txt_box_t.Text, 0.0);
            mcb_ana.bar_dia_top = MyList.StringToDouble(txt_mcb_bar_top.Text, 0.0);
            mcb_ana.bar_dia_bottom = MyList.StringToDouble(txt_mcb_bar_bottom.Text, 0.0);
            mcb_ana.bar_dia_side = MyList.StringToDouble(txt_mcb_bar_side.Text, 0.0);
            mcb_ana.bar_dia_intermediate = MyList.StringToDouble(txt_mcb_bar_intermediate.Text, 0.0);

            //mcb_ana.cover = MyList.StringToDouble(txt_box_cover.Text, 0.0);
            //mcb_ana.b1 = MyList.StringToDouble(txt_mcb_b1.Text, 0.0);
            //mcb_ana.b2 = MyList.StringToDouble(txt_box_b2.Text, 0.0);
            //mcb_ana.a1 = MyList.StringToDouble(txt_box_a1.Text, 0.0);
            //mcb_ana.w1 = MyList.StringToDouble(txt_box_w1.Text, 0.0);
            //mcb_ana.w2 = MyList.StringToDouble(txt_box_w2.Text, 0.0);
            //mcb_ana.b3 = MyList.StringToDouble(txt_box_b3.Text, 0.0);
            //mcb_ana.F = MyList.StringToDouble(txt_box_F.Text, 0.0);
            //mcb_ana.S = MyList.StringToDouble(txt_mcb_S.Text, 0.0);
            //mcb_ana.sbc = MyList.StringToDouble(txt_mcb_sbc.Text, 0.0);

            //Add SIDL on Top Members
            mcb_ana.SIDL = MyList.StringToDouble(txt_mca_ld_sidl.Text, 0.0);

            if(rbtn_mca_ld_only_side.Checked)
            {
                mcb_ana.Is_Only_Sidewall = true;
            }
            else
                mcb_ana.Is_Only_Sidewall = false;



            mcb_ana.fck = MyList.StringToDouble(cmb_mcb_fck.Text, 0.0);
            mcb_ana.fy = MyList.StringToDouble(cmb_mcb_fy.Text, 0.0);

            mcb_ana.Soil_Bearing_Pressure = MyList.StringToDouble(txt_mcb_bps.Text, 0.0);


            if(chk_mcb_slwt.Checked)
                mcb_ana.Self_Weight_Factor = MyList.StringToDouble(txt_mcb_slwt.Text, 1.0);
            else
                mcb_ana.Self_Weight_Factor = 0.0;

            int grade = (int)mcb_ana.fck;

            mcb_ana.Con_Grade = (CONCRETE_GRADE)grade;


            mcb_ana.Member_Properties = new List<string>(txt_memb_props.Lines);



            mcb_ana.sigma_c = MyList.StringToDouble(txt_mcb_sigma_c.Text, 0.0);
            mcb_ana.sigma_st = MyList.StringToDouble(txt_mcb_sigma_st.Text, 0.0);

            mcb_ana.dia_shr = MyList.StringToDouble(txt_mcb_dia_shr.Text, 0.0);



            MaxForce mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_ts_bm.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_ts_bm_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_ts_bm_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_ts_bm_jno.Text, 0);

            mcb_ana.BM_TS = mf;



            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_bs_bm.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_bs_bm_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_bs_bm_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_bs_bm_jno.Text, 0);

            mcb_ana.BM_BS = mf;

             
            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_sw_bm.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_sw_bm_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_sw_bm_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_sw_bm_jno.Text, 0);

            mcb_ana.BM_SW = mf;


            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_iw_bm.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_iw_bm_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_iw_bm_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_iw_bm_jno.Text, 0);

            mcb_ana.BM_IW = mf;




            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_ts_sf.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_ts_sf_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_ts_sf_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_ts_sf_jno.Text, 0);

            mcb_ana.SF_TS = mf;

            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_bs_sf.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_bs_sf_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_bs_sf_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_bs_sf_jno.Text, 0);

            mcb_ana.SF_BS = mf;

            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_sw_sf.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_sw_sf_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_sw_sf_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_sw_sf_jno.Text, 0);

            mcb_ana.SF_SW = mf;


            mf = new MaxForce();
            mf.Force = MyList.StringToDouble(txt_res_iw_sf.Text, 0.0);
            mf.MemberNo = MyList.StringToInt(txt_res_iw_sf_mno.Text, 0);
            mf.Loadcase = MyList.StringToInt(txt_res_iw_sf_lc.Text, 0);
            mf.NodeNo = MyList.StringToInt(txt_res_iw_sf_jno.Text, 0);

            mcb_ana.SF_IW = mf;

            #endregion


            #region Chiranjit [2016 08 05]


            mcb_ana._H = MyList.StringToDouble(txt_mca_ld_H.Text, 0);
            mcb_ana._gama_b = MyList.StringToDouble(txt_mca_ld_gama_b.Text, 0);
            mcb_ana._phi = MyList.StringToDouble(txt_mca_ld_phi.Text, 0);
            mcb_ana._q1 = MyList.StringToDouble(txt_mca_ld_q1.Text, 0);
            mcb_ana._q2 = MyList.StringToDouble(txt_mca_ld_q2.Text, 0);
            mcb_ana._Hw = MyList.StringToDouble(txt_mca_ld_Hw.Text, 0);
            mcb_ana._gama_w = MyList.StringToDouble(txt_mca_ld_gama_w.Text, 0);
            //mcb_ana._Hb = MyList.StringToDouble(txt_mca_ld_Hd.Text, 0);
            mcb_ana._gama_d = MyList.StringToDouble(txt_mca_ld_gama_d.Text, 0);
            mcb_ana._Hs = MyList.StringToDouble(txt_mca_ld_Hs.Text, 0);
            //mcb_ana._gama_s = MyList.StringToDouble(txt_mca_ld_gama_s.Text, 0);
            mcb_ana._Ds = MyList.StringToDouble(txt_mca_ld_Ds.Text, 0);
            mcb_ana._gama_c = MyList.StringToDouble(txt_mca_ld_gama_c.Text, 0);
            mcb_ana._De = MyList.StringToDouble(txt_mcb_De.Text, 0);
            mcb_ana._gama_e = MyList.StringToDouble(txt_mca_ld_gama_e.Text, 0);

            mcb_ana._brng_presr = MyList.StringToDouble(txt_mca_ld_bps.Text, 0);
            mcb_ana._oth_lds = MyList.StringToDouble(txt_mca_ld_oth_lds.Text, 0);
            mcb_ana._FS = MyList.StringToDouble(txt_mca_ld_FS.Text, 0);
            mcb_ana._BC = MyList.StringToDouble(txt_mca_ld_BC.Text, 0);
            mcb_ana._AS = MyList.StringToDouble(txt_mca_ld_AS.Text, 0);

            mcb_ana.Get_Load_Calculation();
            #endregion Chiranjit [2016 08 05]


            #region Chiranjit [2016 08 06]

            mcb_ana.Apply_Seismic = chk_mca_ld_apply_seismic.Checked;
            mcb_ana._Z = MyList.StringToDouble(txt_mca_ld_Z.Text, 0);
            mcb_ana._I = MyList.StringToDouble(txt_mca_ld_I.Text, 0);
            mcb_ana._R = MyList.StringToDouble(txt_mca_ld_R.Text, 0);
            mcb_ana._Sa_by_g = MyList.StringToDouble(txt_mca_ld_Sa_by_g.Text, 0);

            mcb_ana._Ah = MyList.StringToDouble(txt_mca_Ah.Text, 0);


            #endregion Chiranjit [2016 08 06]


        }
        public void Multi_Cell_Box_Grid_Load_Data()
        {

            int i = 0;
            Applied_Load ap = new Applied_Load();
            dgv_dl_sidl.Rows.Clear();
            dgv_ep_l.Rows.Clear();
            dgv_ll_loads.Rows.Clear();

            dgv_dl_sidl.Rows.Add(true, mcb_ana.DL_Loads.Load_Title, "", "", "");
            dgv_dl_sidl[0, dgv_dl_sidl.Rows.Count - 1].ReadOnly = true;
            dgv_dl_sidl.Rows[dgv_dl_sidl.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;
             
            for (i = 0; i < mcb_ana.DL_Loads.Count; i++)
            {
                ap = mcb_ana.DL_Loads[i];
                dgv_dl_sidl.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, "", "");
            }
            dgv_dl_sidl.Rows.Add(true, mcb_ana.SIDL_Loads.Load_Title, "", "", "");
            dgv_dl_sidl[0, dgv_dl_sidl.Rows.Count - 1].ReadOnly = true;
            dgv_dl_sidl.Rows[dgv_dl_sidl.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;
            
            for (i = 0; i < mcb_ana.SIDL_Loads.Count; i++)
            {
                ap = mcb_ana.SIDL_Loads[i];
                dgv_dl_sidl.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, "", "");
            }

            Earth_Pressure_Load ep = new Earth_Pressure_Load();
            dgv_ep_l.Rows.Add(true, "LOAD 3 EARTH PRESSURE", "", "", "", "");
            dgv_ep_l[0, dgv_ep_l.Rows.Count - 1].ReadOnly = true;
            dgv_ep_l.Rows[dgv_ep_l.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;
           
            for (i = 0; i < mcb_ana.EP_Loads.Count; i++)
            {
                ep = mcb_ana.EP_Loads[i];
                dgv_ep_l.Rows.Add(true, ep.MemberNos, ep.Direction, ep.LoadType, ep.Load_P1.ToString("f3"), ep.Load_P2.ToString("f3"));
            }

            for (i = 0; i < mcb_ana.Moving_Loads.Count; i++)
            {
                dgv_ll_loads.Rows.Add(true, mcb_ana.Moving_Loads[i].Load_Title, "", "", "", "", "");
                dgv_ll_loads[0, dgv_ll_loads.Rows.Count - 1].ReadOnly = true;
                dgv_ll_loads.Rows[dgv_ll_loads.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;
                for (int j = 0; j < mcb_ana.Moving_Loads[i].Count; j++)
                {
                    ap = mcb_ana.Moving_Loads[i][j];
                    dgv_ll_loads.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, ap.Start_Distance, ap.End_Distance);
                }
            }

            #region Load Combination
            List<string> list = new List<string>();
            list.Add(string.Format("true 19 1  1.0    3  1.0"));
            list.Add(string.Format("true 20 1  1.0   2  1.0    3  1.0"));
            list.Add(string.Format("true 21 1  1.0   2  1.0    3  1.0    4  1.0"));
            list.Add(string.Format("true 22 1  1.0   2  1.0 3 1.0    5  1.0"));
            list.Add(string.Format("true 23 1  1.0   2  1.0 3 1.0    6  1.0"));
            list.Add(string.Format("true 24 1  1.0   2  1.0 3  1.0   7  1.0"));
            list.Add(string.Format("true 25 1  1.0   2  1.0 3  1.0   8  1.0"));
            list.Add(string.Format("true 26 1  1.0   2   1.0    3  1.0   9  1.0"));
            list.Add(string.Format("true 27 1  1.0   2   1.0    3  1.0   10  1.0"));
            list.Add(string.Format("true 28 1  1.0    2  1.0    3  1.0   11   1.0"));
            list.Add(string.Format("true 29 1  1.0    2  1.0    3  1.0   12   1.0"));
            list.Add(string.Format("true 30 1  1.0   2  1.0    3  1.0   13   1.0"));
            list.Add(string.Format("true 31 1  1.0   2  1.0 3 1.0   14   1.0"));
            list.Add(string.Format("true 32 1  1.0   2   1.0    3  1.0   15    1.0"));
            list.Add(string.Format("true 33 1  1.0    2  1.0    3  1.0    16   1.0"));
            list.Add(string.Format("true 34 1 1.0    2  1.0 3 1.0    17   1.0"));
            list.Add(string.Format("true 35 1  1.0   2  1.0 3  1.0   18   1.0"));

            MyList mlist = null;
            dgv_load_comb.Rows.Clear();
            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), ' ');
                dgv_load_comb.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Load Combination

        }
        public void Multi_Cell_Box_Grid_DL_SIDL_Load_Data()
        {
            int i = 0;
            Applied_Load ap = new Applied_Load();
            dgv_dl_sidl.Rows.Clear();

            dgv_dl_sidl.Rows.Add(true, mcb_ana.DL_Loads.Load_Title, "", "", "");
            dgv_dl_sidl[0, dgv_dl_sidl.Rows.Count - 1].ReadOnly = true;
            dgv_dl_sidl.Rows[dgv_dl_sidl.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;

            for (i = 0; i < mcb_ana.DL_Loads.Count; i++)
            {
                ap = mcb_ana.DL_Loads[i];
                dgv_dl_sidl.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, "", "");
            }
            dgv_dl_sidl.Rows.Add(true, mcb_ana.SIDL_Loads.Load_Title, "", "", "");
            dgv_dl_sidl[0, dgv_dl_sidl.Rows.Count - 1].ReadOnly = true;
            dgv_dl_sidl.Rows[dgv_dl_sidl.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;

            for (i = 0; i < mcb_ana.SIDL_Loads.Count; i++)
            {
                ap = mcb_ana.SIDL_Loads[i];
                dgv_dl_sidl.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, "", "");
            }
        }
        public void Multi_Cell_Box_Grid_EP_Load_Data()
        {

            int i = 0;
            Applied_Load ap = new Applied_Load();
            dgv_ep_l.Rows.Clear();

            Earth_Pressure_Load ep = new Earth_Pressure_Load();
            dgv_ep_l.Rows.Add(true, "LOAD 3 EARTH PRESSURE", "", "", "", "");
            dgv_ep_l[0, dgv_ep_l.Rows.Count - 1].ReadOnly = true;
            dgv_ep_l.Rows[dgv_ep_l.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;

            for (i = 0; i < mcb_ana.EP_Loads.Count; i++)
            {
                ep = mcb_ana.EP_Loads[i];
                dgv_ep_l.Rows.Add(true, ep.MemberNos, ep.Direction, ep.LoadType, ep.Load_P1.ToString("f3"), ep.Load_P2.ToString("f3"));
            }
        }
        public void Multi_Cell_Box_Grid_LL_Load_Data()
        {

            int i = 0;
            Applied_Load ap = new Applied_Load();
           
            dgv_ll_loads.Rows.Clear();
          
            for (i = 0; i < mcb_ana.Moving_Loads.Count; i++)
            {
                dgv_ll_loads.Rows.Add(true, mcb_ana.Moving_Loads[i].Load_Title, "", "", "", "", "");
                dgv_ll_loads[0, dgv_ll_loads.Rows.Count - 1].ReadOnly = true;
                dgv_ll_loads.Rows[dgv_ll_loads.RowCount - 1].DefaultCellStyle.BackColor = Color.BurlyWood;
                for (int j = 0; j < mcb_ana.Moving_Loads[i].Count; j++)
                {
                    ap = mcb_ana.Moving_Loads[i][j];
                    dgv_ll_loads.Rows.Add(true, ap.MemberNos, ap.Direction, ap.LoadType, ap.Load, ap.Start_Distance, ap.End_Distance);
                }
            }
        }
        public void Multi_Cell_Box_Grid_COMB_Load_Data()
        {
            int i = 0;
            #region Load Combination
            List<string> list = new List<string>();
            list.Add(string.Format("true 19 1  1.0    3  1.0"));
            list.Add(string.Format("true 20 1  1.0   2  1.0    3  1.0"));
            list.Add(string.Format("true 21 1  1.0   2  1.0    3  1.0    4  1.0"));
            list.Add(string.Format("true 22 1  1.0   2  1.0 3 1.0    5  1.0"));
            list.Add(string.Format("true 23 1  1.0   2  1.0 3 1.0    6  1.0"));
            list.Add(string.Format("true 24 1  1.0   2  1.0 3  1.0   7  1.0"));
            list.Add(string.Format("true 25 1  1.0   2  1.0 3  1.0   8  1.0"));
            list.Add(string.Format("true 26 1  1.0   2   1.0    3  1.0   9  1.0"));
            list.Add(string.Format("true 27 1  1.0   2   1.0    3  1.0   10  1.0"));
            list.Add(string.Format("true 28 1  1.0    2  1.0    3  1.0   11   1.0"));
            list.Add(string.Format("true 29 1  1.0    2  1.0    3  1.0   12   1.0"));
            list.Add(string.Format("true 30 1  1.0   2  1.0    3  1.0   13   1.0"));
            list.Add(string.Format("true 31 1  1.0   2  1.0 3 1.0   14   1.0"));
            list.Add(string.Format("true 32 1  1.0   2   1.0    3  1.0   15    1.0"));
            list.Add(string.Format("true 33 1  1.0    2  1.0    3  1.0    16   1.0"));
            list.Add(string.Format("true 34 1 1.0    2  1.0 3 1.0    17   1.0"));
            list.Add(string.Format("true 35 1  1.0   2  1.0 3  1.0   18   1.0"));

            MyList mlist = null;
            dgv_load_comb.Rows.Clear();
            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), ' ');
                dgv_load_comb.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Load Combination
        }

        public void Format_Multi_Cell_Load_Grids()
        {
            double dval = 0.0;
            double mval = 0.0;
            string kStr = "";
            string dir = "";
            string s_dist = "";
            string e_dist = "";





            int i = 0;
            #region Format Grid DL + SIDL
            for (i = 0; i < dgv_dl_sidl.Rows.Count; i++)
            {
                kStr = dgv_dl_sidl[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                {
                    dgv_dl_sidl.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;
                    dgv_dl_sidl.Rows[i].ReadOnly = true;

                }


                dir = dgv_dl_sidl[2, i].Value.ToString();
                kStr = dgv_dl_sidl[4, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);

                    //if (dval < 0)
                    //    dgv_dl_sidl[2, i].Value = "-GY";
                    //else
                    //    dgv_dl_sidl[2, i].Value = "GY";


                    //dgv_dl_sidl[4, i].Value = Math.Abs(dval).ToString("f3");
                    dgv_dl_sidl[4, i].Value = dval.ToString("f3");
                }
            }
            #endregion Format Grid DL + SIDL


            #region Format Grid EP
            for (i = 0; i < dgv_ep_l.Rows.Count; i++)
            {
                kStr = dgv_ep_l[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                {
                    dgv_ep_l.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;
                    dgv_ep_l.Rows[i].ReadOnly = true;
                    continue;
                }


                dir = dgv_ep_l[2, i].Value.ToString();
                kStr = dgv_ep_l[4, i].Value.ToString();
                s_dist = dgv_ep_l[5, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);
                    mval = MyList.StringToDouble(s_dist, 0.0);

                    //if (dval < 0)
                    //    dgv_ep_l[2, i].Value = "-Y";
                    //else
                    //    dgv_ep_l[2, i].Value = "Y";


                    //dgv_ep_l[4, i].Value = Math.Abs(dval).ToString("f3");
                    //dgv_ep_l[5, i].Value = Math.Abs(mval).ToString("f3");


                    dgv_ep_l[4, i].Value = dval.ToString("f3");
                    dgv_ep_l[5, i].Value = (mval).ToString("f3");
                }



            }
            #endregion Format Grid EP



            #region Format Grid LL
            for (i = 0; i < dgv_ll_loads.Rows.Count; i++)
            {
                kStr = dgv_ll_loads[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                {
                    dgv_ll_loads.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;
                    dgv_ll_loads.Rows[i].ReadOnly = true;
                }

                dir = dgv_ll_loads[2, i].Value.ToString();
                kStr = dgv_ll_loads[4, i].Value.ToString();
                s_dist = dgv_ll_loads[5, i].Value.ToString();
                e_dist = dgv_ll_loads[6, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);

                    //if (dval < 0)
                    //    dgv_ll_loads[2, i].Value = "-GY";
                    //else
                    //    dgv_ll_loads[2, i].Value = "GY";


                    dgv_ll_loads[4, i].Value = (dval).ToString("f3");

                    mval = MyList.StringToDouble(s_dist, 0.0);
                    dval = MyList.StringToDouble(e_dist, 0.0);

                    //if (dval == 0.0 && mval == 0.0)
                    //{
                    //    dgv_ll_loads[5, i].Value = "";
                    //    dgv_ll_loads[6, i].Value = "";
                    //}
                    //else if (dval == 0.0 && mval != 0.0)
                    //{
                    //    dgv_ll_loads[5, i].Value = Math.Abs(mval).ToString("f3");
                    //    dgv_ll_loads[6, i].Value = "";
                    //}
                    //else
                    //{
                    //    dgv_ll_loads[5, i].Value = Math.Abs(mval).ToString("f3");
                    //    dgv_ll_loads[6, i].Value = Math.Abs(dval).ToString("f3");


                    dgv_ll_loads[5, i].Value = Math.Abs(mval).ToString("f3");
                    dgv_ll_loads[6, i].Value = Math.Abs(dval).ToString("f3");
                    //}


                    //dgv_ll_loads[5, i].Value = mval.ToString("f3");
                    //dgv_ll_loads[6, i].Value = dval.ToString("f3");

                    if (dval == 0.0)
                    {
                        dgv_ll_loads[5, i].Value = "";
                    }
                    if (mval == 0.0)
                    {
                        dgv_ll_loads[6, i].Value = "";
                    }
                }
            }
            #endregion Format Grid LL

        }

        public void Format_Multi_Cell_DL_SIDL_Load_Grids()
        {
            double dval = 0.0;
            double mval = 0.0;
            string kStr = "";
            string dir = "";
            string s_dist = "";
            string e_dist = "";

            int i = 0;
            #region Format Grid DL + SIDL
            for (i = 0; i < dgv_dl_sidl.Rows.Count; i++)
            {
                kStr = dgv_dl_sidl[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                    dgv_dl_sidl.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;


                dir = dgv_dl_sidl[2, i].Value.ToString();
                kStr = dgv_dl_sidl[4, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);

                    if (dval < 0)
                        dgv_dl_sidl[2, i].Value = "-GY";
                    else
                        dgv_dl_sidl[2, i].Value = "GY";


                    dgv_dl_sidl[4, i].Value = Math.Abs(dval).ToString("f3");
                }
            }
            #endregion Format Grid DL + SIDL
        }
        public void Format_Multi_Cell_EP_Load_Grids()
        {
            double dval = 0.0;
            double mval = 0.0;
            string kStr = "";
            string dir = "";
            string s_dist = "";
            string e_dist = "";

            int i = 0;

            #region Format Grid EP
            for (i = 0; i < dgv_ep_l.Rows.Count; i++)
            {
                kStr = dgv_ep_l[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                    dgv_ep_l.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;


                dir = dgv_ep_l[2, i].Value.ToString();
                kStr = dgv_ep_l[4, i].Value.ToString();
                s_dist = dgv_ep_l[5, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);
                    mval = MyList.StringToDouble(s_dist, 0.0);

                    if (dval < 0)
                        dgv_ep_l[2, i].Value = "-Y";
                    else
                        dgv_ep_l[2, i].Value = "Y";


                    dgv_ep_l[4, i].Value = Math.Abs(dval).ToString("f3");
                    dgv_ep_l[5, i].Value = Math.Abs(mval).ToString("f3");
                }
            }
            #endregion Format Grid EP
        }
        public void Format_Multi_Cell_LL_Load_Grids()
        {
            double dval = 0.0;
            double mval = 0.0;
            string kStr = "";
            string dir = "";
            string s_dist = "";
            string e_dist = "";

            int i = 0;

            #region Format Grid LL
            for (i = 0; i < dgv_ll_loads.Rows.Count; i++)
            {
                kStr = dgv_ll_loads[1, i].Value.ToString();
                if (kStr.StartsWith("LOAD"))
                    dgv_ll_loads.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;

                dir = dgv_ll_loads[2, i].Value.ToString();
                kStr = dgv_ll_loads[4, i].Value.ToString();
                s_dist = dgv_ll_loads[5, i].Value.ToString();
                e_dist = dgv_ll_loads[6, i].Value.ToString();

                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr != "")
                {
                    dval = MyList.StringToDouble(kStr, 0.0);

                    if (dval < 0)
                        dgv_ll_loads[2, i].Value = "-GY";
                    else
                        dgv_ll_loads[2, i].Value = "GY";


                    dgv_ll_loads[4, i].Value = Math.Abs(dval).ToString("f3");

                    mval = MyList.StringToDouble(s_dist, 0.0);
                    dval = MyList.StringToDouble(e_dist, 0.0);

                    if (dval == 0.0 && mval == 0.0)
                    {
                        dgv_ll_loads[5, i].Value = "";
                        dgv_ll_loads[6, i].Value = "";
                    }
                    else if (dval == 0.0 && mval != 0.0)
                    {
                        dgv_ll_loads[5, i].Value = Math.Abs(mval).ToString("f3");
                        dgv_ll_loads[6, i].Value = "";
                    }
                    else
                    {
                        dgv_ll_loads[5, i].Value = Math.Abs(mval).ToString("f3");
                        dgv_ll_loads[6, i].Value = Math.Abs(dval).ToString("f3");
                    }
                }
            }
            #endregion Format Grid LL
        }

        private void btn_mcb_view_sreucture_Click(object sender, EventArgs e)
        {

            if (File.Exists(mcb_ana.User_Input_File))
            {
                //iApp.OpenWork(mcb_ana.User_Input_File, false);
                //iApp.Form_ASTRA_TEXT_Data(mcb_ana.User_Input_File, false).Show();
                iApp.Form_ASTRA_Input_Data(mcb_ana.User_Input_File, false).Show();
            }
        }

        private void btn_mcb_view_data_Click(object sender, EventArgs e)
        {
            if (File.Exists(mcb_ana.User_Input_File))
                System.Diagnostics.Process.Start(mcb_ana.User_Input_File);
        }

        private void btn_mcb_save_data_Click(object sender, EventArgs e)
        {
            DemoCheck();

            mcb_ana.Project_Name = txt_mc_box_project.Text;
            mcb_ana.FilePath = Path.GetDirectoryName(user_path);

            Multi_Cell_Box_Initialize_InputData();

            mcb_ana.Create_Data();
            mcb_ana.Write_Data(mcb_ana.User_Input_File);

            iApp.Save_Form_Record(tab_multi_cell, user_path);
            MessageBox.Show(this, "Input Data file Created in " + mcb_ana.User_Input_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Is_MCB_Create = true;
            prss = 2;
            Button_Enable_Disable();
        }

        private void btn_mcb_process_analysis_Click(object sender, EventArgs e)
        {
            DemoCheck();

            //Multi_Cell_Box_Initialize_InputData();
            //mcb_ana.Create_Data();

            //mcb_ana.Set_Loads_From_Grid(dgv_dl_sidl, dgv_ep_l, dgv_ll_loads, dgv_load_comb);
            //mcb_ana.Write_Data(mcb_ana.User_Input_File);


            mcb_ana.Project_Name = txt_mc_box_project.Text;
            mcb_ana.FilePath = Path.GetDirectoryName(user_path);
            if (mcb_ana.Top_Members.Count == 0)
            {
                Multi_Cell_Box_Initialize_InputData();
                mcb_ana.Create_Data();
            }
            string flPath = mcb_ana.User_Input_File;

            if (!File.Exists(flPath))
            {
                MessageBox.Show(flPath + " file not found.", "ASTRA", MessageBoxButtons.OK);
                return;
            }

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();


            mcb_ana.Structure = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(flPath));
           
            MaxForce mf = mcb_ana.Structure.GetJoint_MomentForce(mcb_ana.Top_Members);


            txt_res_ts_bm.Text = mf.Force.ToString("f3");
            txt_res_ts_bm_mno.Text = mf.MemberNo.ToString();
            txt_res_ts_bm_jno.Text = mf.NodeNo.ToString();
            txt_res_ts_bm_lc.Text = mf.Loadcase.ToString();


            mf = mcb_ana.Structure.GetJoint_ShearForce(mcb_ana.Top_Members);


            txt_res_ts_sf.Text = mf.Force.ToString("f3");
            txt_res_ts_sf_mno.Text = mf.MemberNo.ToString();
            txt_res_ts_sf_jno.Text = mf.NodeNo.ToString();
            txt_res_ts_sf_lc.Text = mf.Loadcase.ToString();


            mf = mcb_ana.Structure.GetJoint_MomentForce(mcb_ana.Bottom_Members);


            txt_res_bs_bm.Text = mf.Force.ToString("f3");
            txt_res_bs_bm_mno.Text = mf.MemberNo.ToString();
            txt_res_bs_bm_jno.Text = mf.NodeNo.ToString();
            txt_res_bs_bm_lc.Text = mf.Loadcase.ToString();


            mf = mcb_ana.Structure.GetJoint_ShearForce(mcb_ana.Bottom_Members);

            txt_res_bs_sf.Text = mf.Force.ToString("f3");
            txt_res_bs_sf_mno.Text = mf.MemberNo.ToString();
            txt_res_bs_sf_jno.Text = mf.NodeNo.ToString();
            txt_res_bs_sf_lc.Text = mf.Loadcase.ToString();


            List<int> lst = new List<int>();

            lst.AddRange(mcb_ana.Left_Side_Members.ToArray());
            lst.AddRange(mcb_ana.Right_Side_Members.ToArray());
            mf = mcb_ana.Structure.GetJoint_MomentForce(lst);

            txt_res_sw_bm.Text = mf.Force.ToString("f3");
            txt_res_sw_bm_mno.Text = mf.MemberNo.ToString();
            txt_res_sw_bm_jno.Text = mf.NodeNo.ToString();
            txt_res_sw_bm_lc.Text = mf.Loadcase.ToString();

            mf = mcb_ana.Structure.GetJoint_ShearForce(lst);
            txt_res_sw_sf.Text = mf.Force.ToString("f3");
            txt_res_sw_sf_mno.Text = mf.MemberNo.ToString();
            txt_res_sw_sf_jno.Text = mf.NodeNo.ToString();
            txt_res_sw_sf_lc.Text = mf.Loadcase.ToString();



            mf = mcb_ana.Structure.GetJoint_MomentForce(mcb_ana.Intermediate_Members);

            txt_res_iw_bm.Text = mf.Force.ToString("f3");
            txt_res_iw_bm_mno.Text = mf.MemberNo.ToString();
            txt_res_iw_bm_jno.Text = mf.NodeNo.ToString();
            txt_res_iw_bm_lc.Text = mf.Loadcase.ToString();

            mf = mcb_ana.Structure.GetJoint_ShearForce(mcb_ana.Intermediate_Members);
            txt_res_iw_sf.Text = mf.Force.ToString("f3");
            txt_res_iw_sf_mno.Text = mf.MemberNo.ToString();
            txt_res_iw_sf_jno.Text = mf.NodeNo.ToString();
            txt_res_iw_sf_lc.Text = mf.Loadcase.ToString();



            iApp.Save_Form_Record(tab_multi_cell, mcb_ana.file_path);
            prss = 3;

            Button_Enable_Disable();

        }

        private void btn_view_report_Click(object sender, EventArgs e)
        {
            if (File.Exists(MyList.Get_Analysis_Report_File(mcb_ana.User_Input_File)))
            {
                iApp.RunExe(MyList.Get_Analysis_Report_File(mcb_ana.User_Input_File));
            }
        }

        private void btn_process_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_mcb_process_design.Name)
            {
                Multi_Cell_Box_Initialize_InputData();
                mcb_ana.Set_Loads_From_Grid(dgv_dl_sidl, dgv_ep_l, dgv_ll_loads, dgv_load_comb);
                mcb_ana.Calculate_Program();
                iApp.View_Result(mcb_ana.rep_file_name);
            }
            else if (btn.Name == btn_mcb_open_design.Name)
            {
                iApp.RunExe(mcb_ana.rep_file_name);
            }
            prss = 4;

            Button_Enable_Disable();

        }

        private void dgv_load_comb_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Catch all the Error of Data
        }

        private void btn_mcb_dl_insert_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string kStr = "";
            try
            {
                #region DL + SIDL
                if (btn.Name == btn_mcb_dl_add.Name)
                {
                    dgv_dl_sidl.Rows.Add(true, "", "", "", "");
                }
                else if (btn.Name == btn_mcb_dl_insert.Name)
                {
                    dgv_dl_sidl.Rows.Insert(dgv_dl_sidl.CurrentCell.RowIndex, true, "", "", "", "");
                }
                else if (btn.Name == btn_mcb_dl_delete.Name)
                {
                    kStr = dgv_dl_sidl[1, dgv_dl_sidl.CurrentCell.RowIndex].Value.ToString();

                    if (!kStr.ToUpper().StartsWith("LOAD"))
                        dgv_dl_sidl.Rows.RemoveAt(dgv_dl_sidl.CurrentCell.RowIndex);
                    else
                        MessageBox.Show("Cannot Delete Load Case.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btn.Name == btn_mcb_dl_restore.Name)
                {
                    if (MessageBox.Show("This will Remove all the current data by the default data.\n\n Do you want to remove current data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Multi_Cell_Box_Grid_DL_SIDL_Load_Data();
                        Format_Multi_Cell_DL_SIDL_Load_Grids();
                    }
                }
                #endregion DL + SIDL

                #region EARTH PRESSURE
                else if (btn.Name == btn_mcb_ep_add.Name)
                {
                    dgv_ep_l.Rows.Add(true, "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_ep_insert.Name)
                {
                    dgv_ep_l.Rows.Insert(dgv_ep_l.CurrentCell.RowIndex, true, "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_ep_delete.Name)
                {
                    kStr = dgv_ep_l[1, dgv_ep_l.CurrentCell.RowIndex].Value.ToString();
                    if (!kStr.ToUpper().StartsWith("LOAD"))
                        dgv_ep_l.Rows.RemoveAt(dgv_ep_l.CurrentCell.RowIndex);
                    else
                        MessageBox.Show("Cannot Delete Load Case.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btn.Name == btn_mcb_ep_restore.Name)
                {
                    if (MessageBox.Show("This will Remove all the current data by the default data.\n\n Do you want to remove current data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Multi_Cell_Box_Grid_EP_Load_Data();
                        Format_Multi_Cell_EP_Load_Grids();
                    }
                }
                #endregion EARTH PRESSURE

                #region LIVE LOADS
                else if (btn.Name == btn_mcb_ll_add.Name)
                {
                    dgv_ll_loads.Rows.Add(true, "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_ll_insert.Name)
                {
                    dgv_ll_loads.Rows.Insert(dgv_ll_loads.CurrentCell.RowIndex, true, "", "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_ll_delete.Name)
                {
                    kStr = dgv_ll_loads[1, dgv_ll_loads.CurrentCell.RowIndex].Value.ToString();
                    if (!kStr.ToUpper().StartsWith("LOAD"))
                        dgv_ll_loads.Rows.RemoveAt(dgv_ll_loads.CurrentCell.RowIndex);
                    else
                        MessageBox.Show("Cannot Delete Load Case.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btn.Name == btn_mcb_ll_restore.Name)
                {
                    if (MessageBox.Show("This will Remove all the current data by the default data.\n\n Do you want to remove current data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Multi_Cell_Box_Grid_LL_Load_Data();
                        Format_Multi_Cell_LL_Load_Grids();
                    }
                }
                #endregion LIVE LOADS

                #region Load Combinations
                else if (btn.Name == btn_mcb_lcomb_add.Name)
                {
                    dgv_load_comb.Rows.Add(true, "", "", "", "", "", "", "", "", "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_lcomb_insert.Name)
                {
                    dgv_load_comb.Rows.Insert(dgv_load_comb.CurrentCell.RowIndex, true, "", "", "", "", "", "", "", "", "", "", "", "", "");
                }
                else if (btn.Name == btn_mcb_lcomb_delete.Name)
                {
                    kStr = dgv_load_comb[1, dgv_load_comb.CurrentCell.RowIndex].Value.ToString();
                    if (!kStr.ToUpper().StartsWith("LOAD"))
                        dgv_load_comb.Rows.RemoveAt(dgv_load_comb.CurrentCell.RowIndex);
                    else
                        MessageBox.Show("Cannot Delete Load Case.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btn.Name == btn_mcb_lcomb_restore.Name)
                {
                    if (MessageBox.Show("This will Remove all the current data by the default data.\n\n Do you want to remove current data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Multi_Cell_Box_Grid_COMB_Load_Data();
                    }
                }
                #endregion Load Combinations

                Format_Load_Comb_Grid();


            }
            catch (Exception ex) { }
        }
        public void Format_Load_Comb_Grid()
        {
            bool check = false;

            int loadcase = 19;
            for (int i = 0; i < dgv_load_comb.RowCount; i++)
            {
                check = bool.Parse(dgv_load_comb[0, i].Value.ToString());

                if (!check)
                    dgv_load_comb[1, i].Value = "";
                else
                    dgv_load_comb[1, i].Value = loadcase++;
            }
        }

        private void dgv_load_comb_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Format_Load_Comb_Grid();
        }

        private void chk_mcb_slwt_CheckedChanged(object sender, EventArgs e)
        {
            txt_mcb_slwt.Enabled = chk_mcb_slwt.Checked;
        }

        public List<string> Member_Props = new List<string>();

        private void rbtn_mcb_indi_mp_CheckedChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add("MATERIAL CONCRETE ALL");
            list.Add("E  2.5E+007 ALL");
            list.Add("POISSON   0.18 ALL");
            list.Add("DENSITY  25 ALL");
            list.Add("ALPHA 1E-005 ALL");
            list.Add("DAMP 0.05 ALL");

            //if (rbtn_mcb_indi_mp.Checked)
            //{
            //    if (Member_Props.Count > 0)
            //        txt_memb_props.Lines = Member_Props.ToArray();
            //    txt_memb_props.Enabled = true;
            //}
            //if (rbtn_mcb_preset.Checked)
            //{
            //    Member_Props.Clear();
            //    Member_Props.AddRange(txt_memb_props.Lines);
            //    txt_memb_props.Lines = list.ToArray();
            //    txt_memb_props.Enabled = false;
            //}



        }


        bool blink = false;
        short prss = 5;
        private void tmr_blink_Tick(object sender, EventArgs e)
        {
            Panel pnl = null;

           


            if (prss == 0)
            {
               
                    pnl = pnl_cs;

                    //pnl_cs.BackColor = Color.White;
                    pnl_cid.BackColor = Color.White;
                    pnl_pana.BackColor = Color.White;
                    pnl_pdes.BackColor = Color.White;

            }
            else if (prss == 1)
            {
                pnl = pnl_cid;

                pnl_cs.BackColor = Color.White;
                //pnl_cid.BackColor = Color.White;
                pnl_pana.BackColor = Color.White;
                pnl_pdes.BackColor = Color.White;
            }
            else if (prss == 2)
            {
                pnl = pnl_pana;
                pnl_cs.BackColor = Color.White;
                pnl_cid.BackColor = Color.White;
                //pnl_pana.BackColor = Color.White;
                pnl_pdes.BackColor = Color.White;
            }
            else if (prss == 3)
            {
                pnl = pnl_pdes;
                pnl_cs.BackColor = Color.White;
                pnl_cid.BackColor = Color.White;
                pnl_pana.BackColor = Color.White;
                //pnl_pdes.BackColor = Color.White;
            }
            else
            {
                pnl_cs.BackColor = Color.White;
                pnl_cid.BackColor = Color.White;
                pnl_pana.BackColor = Color.White;
                pnl_pdes.BackColor = Color.White;
            }

            if (prss < 4)
            {
                pnl.BackColor = blink ? Color.White : Color.Red;
                blink = !blink;
            }
        }

        private void btn_multicell_culvert_browse_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_mc_box_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(tab_multi_cell.Text, user_path);
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    iApp.Read_Form_Record(tab_multi_cell, frm.Example_Path);
                    txt_mc_box_project.Text = Path.GetFileName(frm.Example_Path);
                    mcb_ana.Project_Name = txt_mc_box_project.Text;
                    mcb_ana.FilePath = user_path;
                    Is_MCB_Create = true;
                    prss = 4;
                    btn_mcb_create_data.Enabled = false;

                    btn_mcb_save_data.Enabled = true;


                    Format_Multi_Cell_Load_Grids();
                }
            }
            else if (btn.Name == btn_mc_box_new_design.Name)
            {
                frm_NewProject frm = new frm_NewProject(user_path);
                frm.Project_Name = "Multicell Box Culvert Design Project";
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_mc_box_project.Text = frm.Project_Name;

                    Is_MCB_Create = true;
                    prss = 1;
                    //btn_mcb_create_data.Enabled = true;
                    btn_mcb_save_data.Enabled = true;
                }
            }
            Button_Enable_Disable();
        }

        private void label217_Click(object sender, EventArgs e)
        {

        }

        private void txt_mcb_H_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Name == txt_mcb_De.Name)
            {
                double de = MyList.StringToDouble(txt_mcb_De.Text, 0.0);
                if (de != 0.0)
                {
                    //In Tab "Create Input Data":
                    //Height of Earth Cushion [De] = 1.0 m.


                    //In Tab "Load":
                    //Radio Button AUTOMATICALLY should be "On Top Slabs and Side Walls", 
                    //q1 to be calculated as q1 = De x Ye = 1.0 x 19.0 = 19.0 kN/m,

                    rbtn_mca_ld_top_side.Checked = true;
                    txt_mca_ld_q1.Text = (de * MyList.StringToDouble(txt_mca_ld_gama_e.Text, 0.0)).ToString("f3");
                    //        AND

                    //In Tab "Create Input Data":
                    //Height of Earth Cushion [De] = 0.0 m.


                    //In Tab "Load":
                    //Radio Button AUTOMATICALLY should be "On Side Walls only", 
                    //q1 = 20.0 kN/m, (Default Input Data for weight of Approach Slab)

                }
                else
                {
                    rbtn_mca_ld_only_side.Checked = true;
                    txt_mca_ld_q1.Text = "20.0";

                }
            }
            Multicell_Text_Change();
        }

        private void Multicell_Text_Change()
        {
            //txt_mca_ld_De.Text = txt_mcb_De.Text;
            //txt_mca_ld_H.Text = (MyList.StringToDouble(txt_mcb_H.Text, 0.0) + MyList.StringToDouble(txt_mcb_d1.Text, 0.0) + MyList.StringToDouble(txt_mcb_d2.Text, 0.0)).ToString();
            txt_mca_ld_H.Text = txt_mcb_H.Text;
            txt_mca_ld_Ds.Text = txt_mcb_d1.Text;

            txt_mca_ld_gama_b.Text = txt_mcb_gama_b.Text;
            txt_mca_ld_gama_c.Text = txt_mcb_gama_c.Text;
            txt_mca_ld_bps.Text = txt_mcb_bps.Text;




            double mm = MyList.StringToDouble(txt_mca_ld_phi.Text, 0.0);

            txt_mcb_Ka.Text = ((1 - Math.Sin(MyList.Convert_Degree_To_Radian(mm))) / (1 + Math.Sin(MyList.Convert_Degree_To_Radian(mm)))).ToString("f3");

        }

        private void txt_mca_ld_phi_TextChanged(object sender, EventArgs e)
        {
            Multicell_Text_Change();
         }

        private void cmb_mca_ld_apply_seismic_CheckedChanged(object sender, EventArgs e)
        {
            //grb_mca_ld_seismic.Enabled = chk_mca_ld_apply_seismic.Checked;
            txt_mca_Ah.Enabled = chk_mca_ld_apply_seismic.Checked;
        }

        private void btn_mcb_seismic_help_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string fpath = "";
            if (btn.Name == btn_mcb_seismic_help.Name)
            {
                fpath = Path.Combine(Application.StartupPath, @"DESIGN\Siesmic Coefficient\Calculation for Siesmic Coefficient.xls");
            }
            else if (btn.Name == btn_mca_user_guide.Name)
            {
                fpath = Path.Combine(Application.StartupPath, @"ASTRAHelp\Users Guide Multi Cell RCC Box Culvert.pdf");
            }



            if(File.Exists(fpath))
            {
                System.Diagnostics.Process.Start(fpath);
            }
        }

        private void btn_slab_new_deign_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_slab_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(tab_slab_culvert.Text, user_path);
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    iApp.Read_Form_Record(tab_slab_culvert, frm.Example_Path);
                    txt_slab_project.Text = Path.GetFileName(frm.Example_Path);
                    sc_box.Project_Name = txt_slab_project.Text;
                    sc_box.FilePath = user_path;
                    btn_slab_process.Enabled = true;
                }
            }
            else if (btn.Name == btn_slab_new_deign.Name)
            {
                frm_NewProject frm = new frm_NewProject(user_path);
                //frm.Project_Name = "Singlecell Box Culvert Design Project";
                if (txt_slab_project.Text != "")
                    frm.Project_Name = txt_slab_project.Text;
                else
                    frm.Project_Name = "Slab Culvert Design Project";
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_slab_project.Text = frm.Project_Name;
                    btn_slab_process.Enabled = true;
                }
            }
            Button_Enable_Disable();
        }

        private void btn_pipe_new_design_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_pipe_browse.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(tab_pipe_culvert.Text, user_path);
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    iApp.Read_Form_Record(tab_pipe_culvert, frm.Example_Path);
                    txt_pipe_project.Text = Path.GetFileName(frm.Example_Path);
                    pipe.Project_Name = txt_pipe_project.Text;
                    pipe.FilePath = user_path;
                    btn_pipe_Process.Enabled = true;
                }
            }
            else if (btn.Name == btn_pipe_new_design.Name)
            {
                frm_NewProject frm = new frm_NewProject(user_path);
                //frm.Project_Name = "Singlecell Box Culvert Design Project";
                if (txt_pipe_project.Text != "")
                    frm.Project_Name = txt_pipe_project.Text;
                else
                    frm.Project_Name = "Pipe Culvert Design Project";
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_pipe_project.Text = frm.Project_Name;
                    btn_pipe_Process.Enabled = true;
                }
            }
            Button_Enable_Disable();
        }

        private void txt_slab_project_TextChanged(object sender, EventArgs e)
        {

        }



        #region Create Project / Open Project

        public void Create_Project()
        {
            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


            //string dir = user_path;

            //if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Single_Cell)
            //    dir = Path.Combine(dir, "SINGLE CELL BOX CULVERT");
            //else if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Multi_Cell)
            //    dir = Path.Combine(dir, "MULTI CELL BOX CULVERT");
            //else if (Project_Type == eASTRADesignType.RCC_Slab_Culvert)
            //    dir = Path.Combine(dir, "SLAB CULVERT");
            //else if (Project_Type == eASTRADesignType.RCC_Slab_Culvert)
            //    dir = Path.Combine(dir, "PIPE CULVERT");

            //user_path = dir;

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

            string fname = Path.Combine(Path.GetDirectoryName(user_path), Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
          
            Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        private void Write_All_Data()
        {
            switch (Project_Type)
            {
                case eASTRADesignType.RCC_Box_Culvert_Single_Cell:
                    iApp.Save_Form_Record(tab_single_cell, user_path);
                    break;
                case eASTRADesignType.RCC_Box_Culvert_Multi_Cell:
                    iApp.Save_Form_Record(tab_multi_cell, user_path);
                    break;
                case eASTRADesignType.RCC_Slab_Culvert:
                    iApp.Save_Form_Record(tab_slab_culvert, user_path);
                    break;
                case eASTRADesignType.RCC_Pipe_Culvert:
                    iApp.Save_Form_Record(tab_pipe_culvert, user_path);
                    break;
            }
            //throw new NotImplementedException();
        }

        public void Set_Project_Name()
        {
            //string dir = Path.GetDirectoryName(user_path);




            //string prj_name = "";
            //string prj_dir = "";
            //int c = 1;
            //if (Directory.Exists(dir))
            //{
            //    while (true)
            //    {
            //        prj_name = "DESIGN JOB #" + c.ToString("00");
            //        prj_dir = Path.Combine(dir, prj_name);

            //        if (!Directory.Exists(prj_dir)) break;
            //        c++;
            //    }
            //}
            //else
            //    prj_name = "DESIGN JOB #" + c.ToString("00");

            //txt_project_name.Text = prj_name;

            Set_Project_Name(eASTRADesignType.RCC_Pipe_Culvert);
            Set_Project_Name(eASTRADesignType.RCC_Slab_Culvert);
            Set_Project_Name(eASTRADesignType.RCC_Box_Culvert_Multi_Cell);
            Set_Project_Name(eASTRADesignType.RCC_Box_Culvert_Single_Cell);
        }
        public void Set_Project_Name(eASTRADesignType prjType)
        {
            //string dir = Path.GetDirectoryName(Get_User_path(prjType));


            string dir =  Get_User_path(prjType);

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

            //txt_project_name.Text = prj_name;

            if (prjType == eASTRADesignType.RCC_Box_Culvert_Single_Cell) txt_project_name.Text = prj_name;
            else if (prjType == eASTRADesignType.RCC_Box_Culvert_Multi_Cell) txt_mc_box_project.Text = prj_name;
            else if (prjType == eASTRADesignType.RCC_Slab_Culvert) txt_slab_project.Text = prj_name;
            else if (prjType == eASTRADesignType.RCC_Pipe_Culvert) txt_pipe_project.Text = prj_name;

        }

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == btn_browse_design.Text)
            {
                //frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                string tb_name = "";
                if (tc_main.SelectedTab == tab_box_culvert)
                {
                    if (tc_box.SelectedTab == tab_single_cell) tb_name = tab_single_cell.Text;
                    else if (tc_box.SelectedTab == tab_multi_cell) tb_name = tab_multi_cell.Text;
                }
                else if (tc_main.SelectedTab == tab_slab_culvert) tb_name = tab_slab_culvert.Text;
                else if (tc_main.SelectedTab == tab_pipe_culvert) tb_name = tab_pipe_culvert.Text;

                frm_Open_Project frm = new frm_Open_Project(tb_name, Path.GetDirectoryName(user_path));




                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;


                    if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Multi_Cell)
                    {

                        string src_path = user_path;
                        string dest_path = user_path;


                        #region Save As
                        if (frm.SaveAs_Path != "")
                        {
                            src_path = user_path;
                            Project_Name = Path.GetFileName(frm.SaveAs_Path);
                            Create_Project();
                            dest_path = user_path;
                            MyList.Folder_Copy(src_path, dest_path);
                        }
                        #endregion Save As


                        iApp.Read_Form_Record(tab_multi_cell, dest_path);
                        mcb_ana.FilePath = Path.GetDirectoryName(dest_path);
                        Project_Name = Path.GetFileName(dest_path);
                        mcb_ana.Project_Name = Project_Name;
                        Write_All_Data();
                    }
                    else if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Single_Cell)
                    {
                        string src_path = user_path;
                        string dest_path = user_path;

                        #region Save As
                        if (frm.SaveAs_Path != "")
                        {
                            src_path = user_path;
                            Project_Name = Path.GetFileName(frm.SaveAs_Path);
                            Create_Project();
                            dest_path = user_path;
                            MyList.Folder_Copy(src_path, dest_path);
                        }
                        #endregion Save As

                        iApp.Read_Form_Record(tab_single_cell, user_path);

                        Project_Name = Path.GetFileName(dest_path);
                        sc_box.Project_Name = Project_Name;
                        sc_box.FilePath = Path.GetDirectoryName(user_path);

                        btn_box_Process.Enabled = true;

                        Write_All_Data();

                    }
                    else if (Project_Type == eASTRADesignType.RCC_Slab_Culvert)
                    {
                        //slab.Project_Name = Project_Name;
                        //slab.FilePath = Path.GetDirectoryName(user_path);
                        //btn_slab_process.Enabled = true;
                    
                        string src_path = user_path;
                        string dest_path = user_path;


                        #region Save As
                        if (frm.SaveAs_Path != "")
                        {
                            src_path = user_path;
                            Project_Name = Path.GetFileName(frm.SaveAs_Path);
                            Create_Project();
                            dest_path = user_path;
                            MyList.Folder_Copy(src_path, dest_path);
                        }
                        #endregion Save As

                        iApp.Read_Form_Record(tab_slab_culvert, user_path);

                        Project_Name = Path.GetFileName(dest_path);
                        slab.Project_Name = Project_Name;
                        slab.FilePath = Path.GetDirectoryName(user_path);
                        btn_slab_process.Enabled = true;

                        Write_All_Data();
                    }
                    else if (Project_Type == eASTRADesignType.RCC_Pipe_Culvert)
                    {
                        string src_path = user_path;
                        string dest_path = user_path;

                        #region Save As
                        if (frm.SaveAs_Path != "")
                        {
                            src_path = user_path;
                            Project_Name = Path.GetFileName(frm.SaveAs_Path);
                            Create_Project();
                            dest_path = user_path;
                            MyList.Folder_Copy(src_path, dest_path);
                        }
                        #endregion Save As

                        iApp.Read_Form_Record(tab_pipe_culvert, user_path);

                        Project_Name = Path.GetFileName(dest_path);
                        pipe.Project_Name = Project_Name;
                        pipe.FilePath = Path.GetDirectoryName(user_path);
                        btn_pipe_Process.Enabled = true;
                        Write_All_Data();
                    }

                    Button_Enable_Disable();
                    //Open_Project();
                }
            }
            else if (btn.Text == btn_new_design.Text)
            {
                //isCreateData = true;
                Create_Project();
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
                switch (Project_Type)
                {
                    
                    case eASTRADesignType.RCC_Box_Culvert_Single_Cell:
                        return txt_project_name.Text;
                    case eASTRADesignType.RCC_Box_Culvert_Multi_Cell:
                        return txt_mc_box_project.Text;
                    case eASTRADesignType.RCC_Slab_Culvert:
                        return txt_slab_project.Text;
                    case eASTRADesignType.RCC_Pipe_Culvert:
                        return txt_pipe_project.Text;
                }
                return txt_project_name.Text;
            }
            set
            {

                switch (Project_Type)
                {

                    case eASTRADesignType.RCC_Box_Culvert_Single_Cell:
                       txt_project_name.Text = value;
                       break;
                    case eASTRADesignType.RCC_Box_Culvert_Multi_Cell:
                        txt_mc_box_project.Text = value;
                        break;
                    case eASTRADesignType.RCC_Slab_Culvert:
                        txt_slab_project.Text = value;
                        break;
                    case eASTRADesignType.RCC_Pipe_Culvert:
                        txt_pipe_project.Text = value;
                        break;
                }
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                if (tc_main.SelectedTab == tab_box_culvert)
                {
                    if (tc_box.SelectedTab == tab_single_cell)
                    {
                        return eASTRADesignType.RCC_Box_Culvert_Single_Cell;
                    }
                    else if (tc_box.SelectedTab == tab_multi_cell)
                    {
                        return eASTRADesignType.RCC_Box_Culvert_Multi_Cell;
                    }

                }
                else if (tc_main.SelectedTab == tab_slab_culvert)
                {

                    return eASTRADesignType.RCC_Slab_Culvert;
                }
                else if (tc_main.SelectedTab == tab_pipe_culvert)
                {

                    return eASTRADesignType.RCC_Pipe_Culvert;
                }

                return eASTRADesignType.NOT_DEFINED;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]



    }

    public  class SingleCell_BoxCulvert
    {
        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_path = "";


        //flag true if user opens previous design
        public bool IsOpen = false;

        public bool is_process = false;

        #region Variable Initialization

        public double H, b, d, d1, d2, d3, gamma_b, gamma_c, R, t, j, cover;
        public double b1, b2, a1, w1, w2, b3, F, S, sbc, sigma_st, sigma_c;
        public double bar_dia_top, bar_dia_side, bar_dia_bottom;


        public CONCRETE_GRADE Con_Grade;

        public List<double> lst_Bar_Dia = null;
        public List<double> lst_Bar_Space = null;

        #endregion

        #region Drawing Variables

        double bd1, bd2, bd3, bd4, bd5, bd6, bd7, bd8, bd9, bd10, bd11, bd12, bd13, bd14, bd15;
        double sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10, sp11, sp12, sp13, sp14, sp15;
        public double _pressure = 0.0;

        #endregion

        IApplication iApp = null;
        public SingleCell_BoxCulvert(IApplication app)
        {
            this.iApp = app;
            //St_Grade = TAU_C.STEEL_GRADE.M25;
            lst_Bar_Dia = new List<double>();
            lst_Bar_Space = new List<double>();
            lst_Bar_Dia.Add(0);
            lst_Bar_Space.Add(0);

            Project_Name = "Design of Box Culvert";
        }

        //Chiranjit [2016 08 08] Add Project Name
        public string Project_Name { get; set; }
        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF BOX CULVERT : " + value;
                
                user_path = value;
                //file_path = GetAstraDirectoryPath(user_path);

                //file_path = Path.Combine(user_path, "Design of Box Culvert");
                //Chiranjit [2016 08 08] Add Project Name
                file_path = Path.Combine(user_path, Project_Name);

                if (!Directory.Exists(file_path))
                {
                    if (IsOpen) return;
                    Directory.CreateDirectory(file_path);
                }
                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Culvert_Rcc_Box.TXT");
                user_input_file = Path.Combine(system_path, "BOX_CULVERT.FIL");
                drawing_path = Path.Combine(system_path, "BOX_CULVERT_DRAWING.FIL");

                //btnProcess.Enabled = Directory.Exists(value);
                //btnReport.Enabled = File.Exists(user_input_file);
                //btnDrawing.Enabled = File.Exists(user_input_file);
                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    //string msg = "The folder already contains Previous Design. Overwrite?";
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Box_Read_From_File(user_input_file);
                //}
            }
        }
        public void Calculate_Program(string file_name)
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*            DESIGN OF BOX CULVERT            *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                #region USER INPUT DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0} = {1:f3} {2}    Marked as (H) in the Drawing",
                    "Height of Earth Cushion = H",
                    H,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}    Marked as (b) in the Drawing",
                    "Inside Clear Width = b",
                    b,
                    "m");

                sw.WriteLine("{0} = {1:f3} {2}    Marked as (d) in the Drawing",
                    "Inside Clear Depth = d",
                    d,
                    "m");


                sw.WriteLine("{0} = {1:f3} {2}   Marked as (d1) in the Drawing",
                    "Thickness of Top Slab = d1",
                    d1,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}   Marked as (d2) in the Drawing",
                    "Thickness of Bottom Slab = d2",
                    d2,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}   Marked as (d3) in the Drawing",
                    "Thickness of Side Walls = d3",
                    d3,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Unit weight of Earth = γ_b",
                    gamma_b,
                    "kN/cu.m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Unit weight of Concrete = γ_c",
                    gamma_c,
                    "kN/cu.m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "R",
                    R,
                    "");
                //sw.WriteLine("{0} = {1:f3} {2}",
                //    "t",
                //    t,
                //    "");
                sw.WriteLine("{0} = {1} {2}",
                    "Top Bar Diameter  = bar_dia_top",
                    bar_dia_top,
                    "mm");
                sw.WriteLine("{0} = {1} {2}",
                    "Side Bar Diameter  = bar_dia_side",
                    bar_dia_side,
                    "mm");
                sw.WriteLine("{0} = {1} {2}",
                    "Bottom Bar Diameter  = bar_dia_bottom",
                    bar_dia_bottom,
                    "mm");

                sw.WriteLine("{0} = {1:f3} {2}",
                    "Clear Cover = cover",
                    cover,
                    "mm");

                sw.WriteLine("{0} = {1} {2}",
                    "Concrete Grade",
                    "M" + sigma_c.ToString("0"),
                    "");
                sw.WriteLine("{0} = {1} {2}",
                    "Steel Grade",
                    "Fe " + sigma_st.ToString("0"),
                    "");


                // For single Track Loading in 2-Lane

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For single Track Loading in 2-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0} = {1:f3} {2}",
                  "Total Load = w1",
                  w1,
                  "kN");

                sw.WriteLine("{0} = {1:f3} {2}",
                    "Separating Distance of two loads = b1",
                    b1,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Width of each loaded Area",
                    b2,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Length of each Loaded Area = b2",
                    a1,
                    "m");

                // FOR Double Track Loading in 4-Lane
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For Double Track Loading in 4-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Total Load = w2",
                    w2,
                    "kN");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Separating Distance between two tracks = b3",
                    b3,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Two track Load dispersion factor = F",
                    F,
                    "");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Equivalent Earth height for Live Load Surchage = S",
                    S,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Safe bearing capacity of Ground = sbc",
                    sbc,
                    "kN/sq.m.");

                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 1
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.0 : LOAD CALCULATION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region STEP 1.1

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("1.1 LOAD ON TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Loads");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double earth_cusion = H * gamma_b;
                sw.WriteLine("    Earth Cushion = H * γ_b = {0:f2} * {1:f3} = {2:f2} kN/sq.m",
                    H,
                    gamma_b,
                    earth_cusion);

                double self_weight_top_slab = d1 * gamma_c;

                sw.WriteLine("    Self weight of Top Slab = d1 * γ_c ");
                sw.WriteLine("                            = {0:f3} * {1:f3}", d1, gamma_c);
                sw.WriteLine("                            = {0:f2} kN/sq.m", self_weight_top_slab);

                double p1 = earth_cusion + self_weight_top_slab;

                sw.WriteLine();
                sw.WriteLine("    Total Permanent Load per unit area for one track = p1");
                sw.WriteLine("                                                 = {0:f3} + {1:f3}",
                                    earth_cusion, self_weight_top_slab);
                sw.WriteLine("                                                 = {0:f3} kN/sq.m", p1);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For One Track Load Covering 2-Lane");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double width_loaded_area = b1 + b2 + b2 + H + H;
                //double a = b1 + b2 + b2 + H + H;
                sw.WriteLine("    Width of Loaded Area including 45° dispersion");
                sw.WriteLine("      = a = b1 + b2 + b2 + H + H");
                sw.WriteLine("      = {0:f3} + {1:f3} + {1:f3} + {2:f3} + {2:f3}",
                    b1, b2, H);
                sw.WriteLine("      = {0:f3} m", width_loaded_area);

                double length_loaded_area = a1 + H + H;
                sw.WriteLine();
                sw.WriteLine("    Length of Loaded Area including 45° dispersion");
                sw.WriteLine("       = b = a1 + H + H");
                sw.WriteLine("       = {0:f3} + {1:f3} + {1:f3}",
                    a1, H);
                sw.WriteLine("       = {0:f3} m", length_loaded_area);

                double loaded_area_dispersion = width_loaded_area * length_loaded_area;

                sw.WriteLine();
                sw.WriteLine("    Loaded Area including dispersion");
                sw.WriteLine("        = {0:f3} * {1:f3} = {2:f3} sq.m",
                    width_loaded_area,
                    length_loaded_area, loaded_area_dispersion);

                sw.WriteLine();
                sw.WriteLine("    Load for One Track = w1 = {0:f2} kN ", w1);

                double p2 = w1 / loaded_area_dispersion;
                sw.WriteLine();
                sw.WriteLine("    Load per unit Area for one Track ");
                sw.WriteLine("       = p2 = {0:f2}/{1:f2}", w1, loaded_area_dispersion);
                sw.WriteLine("       = {0:f3} kN/sq.m", p2);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For Two Track Load Covering 4-Lane");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double width_of_loaded_area_2 = (b1 + b2 + b2) * 2 + b3 + (H + H);
                sw.WriteLine("    Width of Loaded Area = (b1 + b2 + b2) * 2 + b3 + (H + H)");
                sw.WriteLine("                         = ({0:f2} + {1:f2} + {1:f2}) * 2 + {2:f3} + ({3:f3} + {3:f3})",
                    b1, b2, b3, H);
                sw.WriteLine("                         =  {0:f3} m", width_of_loaded_area_2);

                double length_loaded_area_2 = a1 + H + H;
                sw.WriteLine();
                sw.WriteLine("    Length of Loaded Area = (a1 + H + H)");
                sw.WriteLine("                          = ({0:f2} + {1:f2} + {1:f2})", a1, H);
                sw.WriteLine("                          = {0:f2} m", length_loaded_area_2);

                double loaded_area_dispersion_2 = width_of_loaded_area_2 * length_loaded_area_2;

                sw.WriteLine();
                sw.WriteLine("    Loaded Area including dispersion = {0:f2} * {1:f2}",
                    width_of_loaded_area_2, length_loaded_area_2);
                sw.WriteLine("                                     = {0:f2} sq.m", loaded_area_dispersion_2);

                double load_for_two_tracks = 2 * w1;
                sw.WriteLine();
                sw.WriteLine("    Load for Two Tracks = 2 * {0:f2} = {1:f2} kN",
                                    w1,
                                    load_for_two_tracks);

                double p3 = (load_for_two_tracks * F) / (loaded_area_dispersion_2);

                sw.WriteLine();
                sw.WriteLine("    Load per Unit Area for Two Tracks = p3 ");
                sw.WriteLine("                                      = {0:f2} * F / {1:f2}",
                                                    load_for_two_tracks,
                                                    loaded_area_dispersion_2);
                sw.WriteLine("                                      = {0:f2} kN/sq.m", p3);

                double p4 = p1 + p3;

                sw.WriteLine();
                sw.WriteLine(" Considering Two-track load Covering 4-Lane");
                sw.WriteLine();
                sw.WriteLine(" Total Load per unit area = p4 = p1 + p3");
                sw.WriteLine("                               = {0:f2} + {1:f3} ", p1, p3);
                sw.WriteLine("                               = {0:f3} kN/sq.m.", p4);

                #endregion

                #region STEP 1.2  LOAD ON BOTTOM SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.2 : LOAD ON BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load from Top Slab = p1 = {0:f3} kN/sq.m", p1);
                double loads_walls = (d * d3 * 2 * gamma_c) / (1 * b + d3 + d3);
                sw.WriteLine();
                sw.WriteLine("  Load of Walls = (d * d3 * 2 * γ_c)/(1 * (b + d3 + d3))");
                sw.WriteLine("                = ({0:f2} * {1:f2} * 2 * {2:f2})/(1 * ({3:f2} + {1:f2} + {1:f2}))",
                    d,
                    d3,
                    gamma_c,
                    b);
                sw.WriteLine("                = {0:f2} kN/sq.m", loads_walls);

                double p5 = p1 + loads_walls;
                sw.WriteLine();
                sw.WriteLine("  Total Load = p5 ");
                sw.WriteLine("             = {0:f3} + {1:f3}", p1, loads_walls);
                sw.WriteLine("             = {0:f2} kN/sq.m", p5);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load from Top Slab = p3 = {0:f3} kN/sq.m", p3);
                double p6 = p5 + p3;
                sw.WriteLine();
                sw.WriteLine("  Total Load Per Unit Area = p6");
                sw.WriteLine("                           = {0:f3} + {1:f3}", p5, p3);
                sw.WriteLine("                           = {0:f3} kN/sq.m", p6);

                sw.WriteLine();

                #endregion

                #region STEP 1.3  LOAD ON SIDE WALLS

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.3 : LOAD ON SIDE WALLS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region Case 1
                sw.WriteLine("      Case 1 : Box Empty + Live Load Surcharge");
                sw.WriteLine();
                double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Live Load Surchage = p7");
                sw.WriteLine("                                              = S * γ_b * 0.5");
                sw.WriteLine("                                              = {0:f3} * {1:f3} * 0.5",
                    S,
                    gamma_b);
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p7);

                double p8 = H * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage = p8");
                sw.WriteLine("                                          = H * γ_b * 0.5");
                sw.WriteLine("                                          = {0:f3} * {1:f3} * 0.5",
                    H,
                    gamma_b);
                sw.WriteLine("                                          = {0:f3} kN/sq.m", p8);

                double p9 = (d + d1) * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Back fill = p9");
                sw.WriteLine("                                           = (d + d1) * γ_b * 0.5");
                sw.WriteLine("                                           = ({0:f3} + {1:f3}) * {2:f3} * 0.5",
                    d,
                    d1,
                    gamma_b);
                sw.WriteLine("                                           = {0:f3} kN/sq.m", p9);

                #endregion
                #region Case 2
                sw.WriteLine();
                sw.WriteLine("      Case 2 : Box Full with Water + Live Load Surcharge");

                //double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Live Load Surchage = p7 ");
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p7);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage     = p8");
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p8);

                double p10 = 0.5 * (gamma_b - 10) * (d + d1);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Surchage Earth Back fill");
                sw.WriteLine("                                     = p10 = 0.5 * (γ_b - 10) * (d + d1)");
                sw.WriteLine("                                     = 0.5 * ({0:f2} - 10) * ({1:f2} + {2:f2})",
                    gamma_b, d, d1);
                sw.WriteLine("                                     = {0:f3} kN/sq.m", p10);
                sw.WriteLine();
                #endregion
                #region Case 3
                sw.WriteLine("      Case 3 : Box Full with Water + No Live Load Surcharge");

                //double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Surchage Earth Backfill = p10");
                sw.WriteLine("                                                   = {0:f3} kN/sq.m", p10);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage = p8 ");
                sw.WriteLine("                                          = {0:f3} kN/sq.m", p8);
                sw.WriteLine();
                #endregion

                #endregion

                #region STEP 1.4  BASE PRESSURE

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.4 : BASE PRESSURE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      Load from Top Slab and Walls including Cushion = p5");
                sw.WriteLine("                                                     = {0:f3} kN/sq.m", p5);
                sw.WriteLine();
                double self_weight_bottom_slab = d2 * gamma_c;

                sw.WriteLine("      Self weight at Bottom slab = d2 * γ_c");
                sw.WriteLine("                                 = {0:f2} * {1:f2}", d2, gamma_c);
                sw.WriteLine("                                 = {0:f3} kN/sq.m", self_weight_bottom_slab);
                double total_load = p5 + self_weight_bottom_slab;
                sw.WriteLine();
                sw.WriteLine("      Total Load = {0:f2} + {1:f2} ", p5, self_weight_bottom_slab);
                sw.WriteLine("                 = {0:f3} kN/sq.m", total_load);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      Load from Top Slab = p3 = {0:f3} kN/sq.m", p3);
                sw.WriteLine();
                double base_pressure = total_load + p3;

                if (base_pressure < sbc)
                {
                    sw.WriteLine("      Base Pressure = {0:f3} + {1:f3} = {2:f3} kN/sq.m  < {3:f3} (sbc) OK.",
                        total_load, p3, base_pressure, sbc);
                }
                else
                {
                    sw.WriteLine("      Base Pressure = {0:f3} + {1:f3} = {2:f3} kN/sq.m  > {3:f3} (sbc) NOT OK.",
                        total_load, p3, base_pressure, sbc);
                }
                _pressure = base_pressure;
                #endregion
                #endregion

                #region STEP 2 BENDING MOMENT CALCULATION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.0 : BENDING MOMENT CALCULATION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region 2.1 TOP SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.1 : TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double m1 = p1 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine("      Fixed End Moment for Permanent Load");
                sw.WriteLine("           = m1");
                sw.WriteLine("           = p1 * (d + d1) * (d + d1) / 12 ");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12 ",
                    p1, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m1);

                double m2 = p3 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine();
                sw.WriteLine("      Fixed End Moment for Live Load");
                sw.WriteLine("           = m2");
                sw.WriteLine("           = p3 * (d + d1) * (d + d1)/12 ");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12",
                    p3, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m2);

                double m3 = m1 + m2;
                sw.WriteLine();
                sw.WriteLine("      Total Fixed End Moment = m3 ");
                sw.WriteLine("                             = {0:f3} + {1:f3} ", m1, m2);
                sw.WriteLine("                             = {0:f3} kN-m", m3);

                double m4 = p1 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Permanent Load ");
                sw.WriteLine("           = m4");
                sw.WriteLine("           = p1 * (d + d1) * (d + d1) / 8");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/8",
                    p1, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m4);

                double m5 = p3 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Live Load");
                sw.WriteLine("           = m5");
                sw.WriteLine("           = p3 * (d + d1) * (d + d1)/8");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8 ",
                    p3, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m5);

                double m6 = m4 + m5;
                sw.WriteLine();
                sw.WriteLine("      Total Mid span Moment = m6");
                sw.WriteLine("                            = m4 + m5");
                sw.WriteLine("                            = {0:f2} + {1:f2}", m4, m5);
                sw.WriteLine("                            = {0:f2} kN-m", m6);


                #endregion


                #region 2.2 BOTTOM SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.2 : BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double m7 = p5 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine("      Fixed End Moment for Permanent Load");
                sw.WriteLine("          = m7 ");
                sw.WriteLine("          = p5 * (d + d1) * (d + d1) / 12 ");
                sw.WriteLine("          = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12 ",
                    p5, d, d1);
                sw.WriteLine("          = {0:f3} kN-m", m7);

                //double m2 = p3 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine();
                sw.WriteLine("      Fixed End Moment for Live Load");
                sw.WriteLine("          = m2 ");
                sw.WriteLine("          ={0:f3} kN-m", m2);

                double m8 = m7 + m2;
                sw.WriteLine();
                sw.WriteLine("      Total Fixed End Moment = m8 ");
                sw.WriteLine("                             = m7 + m2 ");
                sw.WriteLine("                             = {0:f3} + {1:f3}", m7, m2);
                sw.WriteLine("                             = {0:f3} kN-m", m8);
                sw.WriteLine();
                double m9 = p5 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Permanent Load");
                sw.WriteLine("          = m9");
                sw.WriteLine("          = p5 * (d + d1) * (d + d1) / 8");
                sw.WriteLine("          = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/8",
                    p5, d, d1);
                sw.WriteLine("          = {0:f3} kN-m", m9);

                //double m5 = p3 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Live Load = m5 ");
                sw.WriteLine("                                    = {0:f3} kN-m", m5);

                double m10 = m9 + m5;
                sw.WriteLine();
                sw.WriteLine("      Total Mid span Moment = m10");
                sw.WriteLine("                            = m9 + m5");
                sw.WriteLine("                            = {0:f2} + {1:f2}", m9, m5);
                sw.WriteLine("                            = {0:f2} kN-m", m10);
                sw.WriteLine();


                #endregion

                #region Side Wall
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SIDE WALL");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region  Case 1 : BOX Empty + Live Load Surcharge
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine(" Case 1 : BOX Empty + Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load");
                double m11 = ((p8 * (d + d1) * (d + d1) / 12) + ((p9 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine();
                sw.WriteLine("   = m11 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("           ((p9 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m11);

                sw.WriteLine();
                double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load");
                sw.WriteLine();
                sw.WriteLine("   = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} kN-m", m12);
                double m13 = m11 + m12;
                sw.WriteLine();
                sw.WriteLine(" Total Fixed End Moment at Top = m13 ");
                sw.WriteLine("                               = m11 + m12");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m11, m12);
                sw.WriteLine("                               = {0:f3} kN-m", m13);




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");

                double m14 = ((p8 * (d + d1) * (d + d1) / 12) + ((p9 * (d + d1) * (d + d1) / 20)));
                sw.WriteLine();
                sw.WriteLine("  = m14 = (p8 * (d + d1) * (d + d1) / 12) +");
                sw.WriteLine("          ((p9 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1, p9);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/20))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m14);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load ");
                sw.WriteLine();
                sw.WriteLine("  = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("  =  {0:f2} kN-m", m12);

                double m15 = m14 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base  = m15 ");
                sw.WriteLine("                                = m14 + m12");
                sw.WriteLine("                                = {0:f2} + {1:f2}", m14, m12);
                sw.WriteLine("                                = {0:f3} kN-m", m15);



                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Dead Load ");
                double m16 = ((p8 * (d + d1) * (d + d1) / 8) + ((p9 * (d + d1) * (d + d1) / 16)));

                sw.WriteLine("   = m16 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("           ((p9 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/16))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m16);

                double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load ");
                sw.WriteLine();
                sw.WriteLine("   = m17 = (p7 * (d + d1) * (d + d1)) / 8.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/8.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m17);

                double m18 = m16 + m17;
                sw.WriteLine();
                sw.WriteLine(" Total Fixed End Moment at Base = m18");
                sw.WriteLine("                                = m16 + m17");
                sw.WriteLine("                                = {0:f2} + {1:f2}", m16, m17);
                sw.WriteLine("                                = {0:f3} kN-m", m18);

                #endregion

                #region  Case 2 : BOX Full with Water + Live Load Surcharge
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine(" Case 2 : BOX Full with Water + Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load ");
                sw.WriteLine();
                double m19 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine("  = m19 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m19);


                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load ");
                sw.WriteLine();
                sw.WriteLine("  = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m12);

                double m20 = m19 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Top = m20");
                sw.WriteLine("                              = m19 + m12");
                sw.WriteLine("                              = {0:f2} + {1:f2}",
                    m19, m12);
                sw.WriteLine("                              = {0:f3} kN-m", m20);




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");
                sw.WriteLine();
                double m21 = ((p8 * (d + d1) * (d + d1) / 12.0) + ((p10 * (d + d1) * (d + d1) / 20.0)));
                sw.WriteLine("   = m21 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("           ((p10 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/20))",
                     p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m21);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load ");
                sw.WriteLine();
                sw.WriteLine("   = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})) / 12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} kN-m", m12);

                double m22 = m21 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m22");
                sw.WriteLine("                               = m21 + m12");
                sw.WriteLine("                               = {0:f2} + {1:f2} kN-m", m21, m12);
                sw.WriteLine("                               = {0:f3} kN-m", m22);
                sw.WriteLine();

                sw.WriteLine("Mid Span Moment for Dead Load ");
                sw.WriteLine();
                double m23 = ((p8 * (d + d1) * (d + d1) / 8) + ((p10 * (d + d1) * (d + d1) / 16)));
                sw.WriteLine("  = m23 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 16))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m23);

                //double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load ");
                sw.WriteLine("     = m17 = (p7 * (d + d1) * (d + d1)) / 8.0");
                sw.WriteLine("     = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})) / 8.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("     = {0:f3} kN-m", m17);

                double m24 = m23 + m17;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m24 ");
                sw.WriteLine("                               = m23 + m17");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m23, m17);
                sw.WriteLine("                               = {0:f3} kN-m", m24);

                #endregion

                #region  Case 3 : BOX Full with Water + No Live Load Surcharge
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Case 3 :  BOX Full with Water + No Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load ");
                double m25 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine("  = m25 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ", p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))", p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m25);


                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load ");
                double m26 = 0;
                sw.WriteLine("  = m26 = {0:f2} kN-m", m26);

                //double m20 = m19 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Top = m25 + m26 ");
                sw.WriteLine("                              = {0:f2} + {1:f2} m", m25, m26);
                sw.WriteLine("                              = {0:f3} kN-m", m25);
                sw.WriteLine();




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");

                double m27 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 20)));

                sw.WriteLine("  = m27 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) +", p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 20))", p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m27);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                double m28 = 0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load = m28 = 0 kN-m");

                //double m22 = m21 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m27 + m28 ");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m27, m28);
                sw.WriteLine("                               = {0:f3} kN-m", m27);



                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Permanent Load ");
                double m29 = ((p8 * (d + d1) * (d + d1) / 8) + ((p10 * (d + d1) * (d + d1) / 16)));

                sw.WriteLine("  = m29 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 16))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m29);

                //double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                double m30 = 0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load = m30 = 0 kN-m");

                //double m24 = m23 + m17;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m29 + m30 ");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m29, m30);
                sw.WriteLine("                               = {0:f3} kN-m", m29);
                sw.WriteLine();

                #endregion


                #endregion

                #endregion

                #region STEP 3 DISTRIBUTION FACTORS

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : DISTRIBUTION FACTORS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Let us denote the Four corner of Box Culvert as A,B,C and D clockwise,");
                sw.WriteLine("Starting from Left Top Corner, then next Right Top Corner, ");
                sw.WriteLine("next Right Bottom Corner and finally Left Bottom Corner");
                sw.WriteLine();
                #region Figure
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("        A                             B"));
                sw.WriteLine(string.Format("        -------------------------------"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        -------------------------------"));
                sw.WriteLine(string.Format("        D                             C"));
                sw.WriteLine(string.Format("              Box Culvert"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format(""));
                #endregion Figure
               
                #region Calculation Details

                double E = 5000 * Math.Sqrt(sigma_c);

                double K_ab, K_ad, K_ba, K_bc, K_cd, K_cb, K_da, K_dc;
                double DF_ab,DF_ad, DF_ba, DF_bc, DF_cd, DF_cb, DF_da, DF_dc;


                K_ab = (4 * E * d1 * d1 * d1) / ((b + d3) * 12);
                K_ba = K_ab;
                K_ad = (4 * E * d3 * d3 * d3) / ((d + d1 / 2 + d2 / 2) * 12);
                K_da = K_ad;
                K_bc = K_ad;
                K_cb = K_ad;

                K_cd = (4 * E * d2 * d2 * d2) / ((b + d3) * 12);
                K_dc = K_cd;


                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Modulus Of Elasticity Of Concrete = E = 5000 * √fck");
                sw.WriteLine("                                      = 5000 * √{0})", sigma_c);
                sw.WriteLine("                                      = {0:E3} N/sq.mm", E);
                sw.WriteLine();
                sw.WriteLine("Moment of Inertia = I, and Length = L");
                sw.WriteLine();
                sw.WriteLine("Cal 1   : K_ab = (4 * E * I) / L");
                sw.WriteLine("               = (4 * E * d1^3) / ((b + d3) * 12)");
                sw.WriteLine("               = (4 * {0:E3} * {1}^3) / (({2} + {3}) * 12)", E, d1, b, d3);
                sw.WriteLine("               = {0:E3}", K_ab);
                sw.WriteLine();
                sw.WriteLine("Cal 2   : K_ba = K_ab = {0:E3}", K_ba);
                sw.WriteLine();
                sw.WriteLine("Cal 3   : K_ad = (4 * E * I) / L");
                sw.WriteLine("               = (4 * E * d3^3) / ((d + d1 / 2 + d2 / 2) * 12)");
                sw.WriteLine("               = (4 * {0:E3} * {1}^3) / (({2} + {3} / 2 + {4} / 2) * 12)", E, d3, d, d1, d2);
                sw.WriteLine("               = {0:E3} ", K_ad);
                sw.WriteLine();
                sw.WriteLine("Cal 4   : K_da = K_ad = {0:E3}", K_ad);
                sw.WriteLine();
                sw.WriteLine("Cal 5   : K_bc = K_da = {0:E3}", K_da);
                sw.WriteLine();
                sw.WriteLine("Cal 6   : K_cb = K_bc = {0:E3}", K_bc);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Cal 7   : K_cd = (4 * E * I) / L");
                sw.WriteLine("               = (4 * E * d2^3) / ((b + d3) * 12)");
                sw.WriteLine("               = (4 * {0:E3} * {1}^3) / (({2} + {3}) * 12)", E, d2, b, d3);
                sw.WriteLine("               = {0:E3} ", K_cd);
                sw.WriteLine();
                sw.WriteLine("Cal 8   : K_dc = K_cd = {0:E3}", K_cd);
                sw.WriteLine();
                sw.WriteLine();
                DF_ab = K_ab / (K_ab + K_ad);
                sw.WriteLine("Cal 9    : DF_ab = K_ab/(K_ab+K_ad) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_ab, K_ad, DF_ab);
                sw.WriteLine();
                DF_ba = K_ba / (K_ba + K_bc);
                sw.WriteLine("Cal 10   : DF_ba = K_ba/(K_ba+K_bc) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_ba, K_bc, DF_ba);
                sw.WriteLine();
                DF_ad = K_ad / (K_ab + K_ad);
                sw.WriteLine("Cal 11   : DF_ad = K_ad/(K_ab+K_ad) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_ad, K_ad, DF_ad);
                sw.WriteLine();
                DF_da = K_da / (K_da + K_dc);
                sw.WriteLine("Cal 12   : DF_da = K_da/(K_da+K_dc) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_da, K_dc, DF_da);
                sw.WriteLine();
                DF_bc = K_bc / (K_ba + K_bc);
                sw.WriteLine("Cal 13   : DF_bc = K_bc/(K_ba+K_bc) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_bc, K_ba, DF_bc);
                sw.WriteLine();
                DF_cb = K_cb / (K_cd + K_cb);
                sw.WriteLine("Cal 14   : DF_cb = K_cb/(K_cd+K_cb) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_cb, K_cb, DF_cb);
                sw.WriteLine();
                DF_cd = K_cd / (K_cd + K_cb);
                sw.WriteLine("Cal 15   : DF_cd = K_cd/(K_cd+K_cb) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_cd, K_cb, DF_cd);
                sw.WriteLine();
                DF_dc = K_dc / (K_da + K_dc);
                sw.WriteLine("Cal 16   : DF_dc = K_dc/(K_da+K_dc) =  {0:E3}/({0:E3}+{1:E3}) = {2:f4}", K_dc, K_da, DF_dc);
                //sw.WriteLine();
                //sw.WriteLine();
                ////sw.WriteLine("Cal 1   : K_ab = (k * d1 * d1 * d1) / (b + d3)");
                //sw.WriteLine("Cal 2   : DF_ab = K_ab/(K_ab+K_ad)");
                ////sw.WriteLine("Cal 3   : K_ad = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                //sw.WriteLine("Cal 4   : DF_ad = K_ad/(K_ab+K_ad) = 0.5");
                ////sw.WriteLine("Cal 5   : K_ba = K_ab");
                //sw.WriteLine("Cal 6   : DF_ba = K_bA/(K_ba+K_bc) = 0.5");
                ////sw.WriteLine("Cal 7   : K_bc = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                //sw.WriteLine("Cal 8   : DF_bc = K_bc/(K_ba+K_bc) = 0.5");
                //sw.WriteLine("Cal 9   : K_cd = K*d2*d2*d2/(b+d3)");
                //sw.WriteLine("Cal 10  : DF_cd = K_cd/(K_cd+K_cb) = 0.5");
                //sw.WriteLine("Cal 11  : K_cb= (K*d3*d3*d3)/(d+(d3+d2)/2)");
                //sw.WriteLine("Cal 12  : DF_cb = K_cb/(K_cd+K_cb) = 0.5");
                //sw.WriteLine("Cal 13  : K_da = (K*d3*d3*d3)/(d+(d3+d2)/2)");
                //sw.WriteLine("Cal 14  : DF_da = K_da/(K_da+K_dc) = 0.5");
                //sw.WriteLine("Cal 15  : K_dc = K_cd");
                //sw.WriteLine("Cal 16  : DF_dc = K_dc/(K_da+K_dc) = 0.5");

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("{0,-15}{1,-11}{2,-15}{3,-15}",
                    "Corner/Joint",
                    "Associated",
                    "  4EI/L",
                    "  DF");
                sw.WriteLine("{0,-15}{1,-11}{2,-15}{3,-15}",
                                   "",
                                   "  Sides",
                                   "",
                                   "");

                sw.WriteLine("------------------------------------------------------------");


                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                    "A",
                    "AB",
                    "K_ab",
                    "DF_ab",
                    DF_ab);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 1)",
                                   "(Cal 9)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                   "",
                                   "AD",
                                   "K_ad",
                                   "DF_ad",
                                   DF_ad);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 3)",
                                   "(Cal 11)");


                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "B",
                                    "BA",
                                    "K_ba",
                                    "DF_ba",DF_ba);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                "",
                                "",
                                "(Cal 2)",
                                "(Cal 10)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "BC",
                                    "K_bc",
                                    "DF_bc",DF_bc);

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 5)",
                                    "(Cal 13)");
                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "C",
                                    "CD",
                                    "K_cd",
                                    "DF_cd",DF_cd);

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 7)",
                                   "(Cal 15)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "CB",
                                    "K_cb",
                                    "DF_cb",DF_cb);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 6)",
                                    "(Cal 14)");

                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "D",
                                    "DA",
                                    "K_da",
                                    "DF_da",DF_da);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                      "",
                                      "",
                                      "(Cal 4)",
                                      "(Cal 12)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "DC",
                                    "K_dc",
                                    "DF_dc",DF_dc);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 8)",
                                    "(Cal 16)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #region STEP 4 : MOMENT DISTRIBUTION
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : MOMENT DISTRIBUTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region Case 1 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m11 = {0:f3}", m11);
                sw.WriteLine("  Mbc = m11 = {0:f3}", m11);
                sw.WriteLine("  Mda = m14 = {0:f3}", m14);
                sw.WriteLine("  Mcb = m14 = {0:f3}", m14);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m12 = {0:f3}", m12);
                sw.WriteLine("  Mbc = m12 = {0:f3}", m12);
                sw.WriteLine("  Mda = m12 = {0:f3}", m12);
                sw.WriteLine("  Mcb = m12 = {0:f3}", m12);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab1 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba1 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc1 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd1 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad1 = m13 = {0:f3} (+)", m13);
                sw.WriteLine("  Mbc1 = m13 = -{0:f3} (-)", m13);
                sw.WriteLine("  Mda1 = m15 = -{0:f3} (-)", m15);
                sw.WriteLine("  Mcb1 = m15 = {0:f3} (+)", m15);
                sw.WriteLine();

                sw.WriteLine("----------------------------------------");

                sw.WriteLine("    AB         BC        CD       DA");
                sw.WriteLine("  AB  BA     BC  CB    CD  DC   DA  AD");
                sw.WriteLine("  -   +      -   +     -   +    -   + ");
                sw.WriteLine("----------------------------------------");

                #endregion


                #region Case 2 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m19 = {0:f3}", m19);
                sw.WriteLine("  Mbc = m19 = {0:f3}", m19);
                sw.WriteLine("  Mda = m21 = {0:f3}", m21);
                sw.WriteLine("  Mcb = m21 = {0:f3}", m21);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m12 = {0:f3}", m12);
                sw.WriteLine("  Mbc = m12 = {0:f3}", m12);
                sw.WriteLine("  Mda = m12 = {0:f3}", m12);
                sw.WriteLine("  Mcb = m12 = {0:f3}", m12);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab2 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba2 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc2 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd2 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad2 = m20 = {0:f3} (+)", m20);
                sw.WriteLine("  Mbc2 = m20 = -{0:f3} (-)", m20);
                sw.WriteLine("  Mda2 = m22 = -{0:f3} (-)", m22);
                sw.WriteLine("  Mcb2 = m22 = {0:f3} (+)", m22);
                sw.WriteLine();


                #endregion

                #region Case 3 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m19 = {0:f3}", m19);
                sw.WriteLine("  Mbc = m19 = {0:f3}", m19);
                sw.WriteLine("  Mda = m21 = {0:f3}", m21);
                sw.WriteLine("  Mcb = m21 = {0:f3}", m21);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m26 = {0:f3}", m26);
                sw.WriteLine("  Mbc = m26 = {0:f3}", m26);
                sw.WriteLine("  Mda = m28 = {0:f3}", m28);
                sw.WriteLine("  Mcb = m28 = {0:f3}", m28);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab3 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba3 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc3 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd3 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad3 = m25 = {0:f3} (+)", m25);
                sw.WriteLine("  Mbc3 = m25 = -{0:f3} (-)", m25);
                sw.WriteLine("  Mda3 = m27 = -{0:f3} (-)", m27);
                sw.WriteLine("  Mcb3 = m27 = {0:f3} (+)", m27);
                sw.WriteLine();


                #endregion

                #endregion

                #region Table-1
                sw.WriteLine(" Table-1 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE I ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");


                DF_ab = MyList.StringToDouble(DF_ab.ToString("F4"), 0.0);
                DF_ad = MyList.StringToDouble(DF_ad.ToString("F4"), 0.0);
                DF_ba = MyList.StringToDouble(DF_ba.ToString("F4"), 0.0);
                DF_bc = MyList.StringToDouble(DF_bc.ToString("F4"), 0.0);
                DF_cb = MyList.StringToDouble(DF_cb.ToString("F4"), 0.0);
                DF_cd = MyList.StringToDouble(DF_cd.ToString("F4"), 0.0);
                DF_dc = MyList.StringToDouble(DF_dc.ToString("F4"), 0.0);
                DF_da = MyList.StringToDouble(DF_da.ToString("F4"), 0.0);

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    DF_ab,
                    DF_ad,
                    DF_ba,
                    DF_bc,
                    DF_cb,
                    DF_cd,
                    DF_dc,
                    DF_da);
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FEM",
                    "Mab1 =",
                    "Mad1 =",
                    "Mba1 =",
                    "Mbc1 =",
                    "Mcb1 =",
                    "Mcd1 =",
                    "Mdc1 =",
                    "Mda1 =");
                double Mab, Mad, Mba, Mbc, Mcb, Mcd, Mdc, Mda;
                Mab = -m3;
                Mad = m13;
                Mba = m3;
                Mbc = -m13;
                Mcb = m15;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m15;

                double SMab1, SMad1, SMba1, SMbc1, SMcb1, SMcd1, SMdc1, SMda1;
                double SMab2, SMad2, SMba2, SMbc2, SMcb2, SMcd2, SMdc2, SMda2;
                double SMab3, SMad3, SMba3, SMbc3, SMcb3, SMcd3, SMdc3, SMda3;

                SMab1 = Mab;

                SMab1 = Mab;
                SMad1 = Mad;
                SMba1 = Mba;
                SMbc1 = Mbc;
                SMcb1 = Mcb;
                SMcd1 = Mcd;
                SMdc1 = Mdc;
                SMda1 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                double D1, D2, D3, D4, D5, D6, D7, D8;
                D1 = 0 - (Mab + Mad) * DF_ab;
                D2 = 0 - (Mab + Mad) * DF_ad;
                D3 = 0 - (Mba + Mbc) * DF_ba;
                D4 = 0 - (Mba + Mbc) * DF_bc;
                D5 = 0 - (Mcb + Mcd) * DF_cb;
                D6 = 0 - (Mcb + Mcd) * DF_cd;
                D7 = 0 - (Mdc + Mda) * DF_dc;
                D8 = 0 - (Mdc + Mda) * DF_da;


                //SMab1 += D1;
                //SMad1 += D2;
                //SMba1 += D3;
                //SMbc1 += D4;
                //SMcb1 += D5;
                //SMcd1 += D6;
                //SMdc1 += D7;
                //SMda1 += D8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");


                double C1, C2, C3, C4, C5, C6, C7, C8;

                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1

                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;


                //C1 = D3 * 0.5;
                //C2 = D8 * 0.5;
                //C3 = D1 * 0.5;
                //C4 = D5 * 0.5;
                //C5 = D4 * 0.5;
                //C6 = D7 * 0.5;
                //C7 = D6 * 0.5;
                //C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");


                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;


                //D1 = 0 - (C1 + C2) * 0.5;
                //D2 = 0 - (C1 + C2) * 0.5;
                //D3 = 0 - (C3 + C4) * 0.5;
                //D4 = 0 - (C3 + C4) * 0.5;
                //D5 = 0 - (C5 + C6) * 0.5;
                //D6 = 0 - (C5 + C6) * 0.5;
                //D7 = 0 - (C7 + C8) * 0.5;
                //D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2

                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;


                //C1 = D3 * 0.5;
                //C2 = D8 * 0.5;
                //C3 = D1 * 0.5;
                //C4 = D5 * 0.5;
                //C5 = D4 * 0.5;
                //C6 = D7 * 0.5;
                //C7 = D6 * 0.5;
                //C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");



                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                //D1 = 0 - (C1 + C2) * 0.5;
                //D2 = 0 - (C1 + C2) * 0.5;
                //D3 = 0 - (C3 + C4) * 0.5;
                //D4 = 0 - (C3 + C4) * 0.5;
                //D5 = 0 - (C5 + C6) * 0.5;
                //D6 = 0 - (C5 + C6) * 0.5;
                //D7 = 0 - (C7 + C8) * 0.5;
                //D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3
                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;


                //C1 = D3 * 0.5;
                //C2 = D8 * 0.5;
                //C3 = D1 * 0.5;
                //C4 = D5 * 0.5;
                //C5 = D4 * 0.5;
                //C6 = D7 * 0.5;
                //C7 = D6 * 0.5;
                //C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");


                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;
                //D1 = 0 - (C1 + C2) * 0.5;
                //D2 = 0 - (C1 + C2) * 0.5;
                //D3 = 0 - (C3 + C4) * 0.5;
                //D4 = 0 - (C3 + C4) * 0.5;
                //D5 = 0 - (C5 + C6) * 0.5;
                //D6 = 0 - (C5 + C6) * 0.5;
                //D7 = 0 - (C7 + C8) * 0.5;
                //D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab1.ToString("0.00"),
                                                    "" + SMad1.ToString("0.00"),
                                                    "" + SMba1.ToString("0.00"),
                                                    "" + SMbc1.ToString("0.00"),
                                                    "" + SMcb1.ToString("0.00"),
                                                    "" + SMcd1.ToString("0.00"),
                                                    "" + SMdc1.ToString("0.00"),
                                                    "" + SMda1.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1 : D1 = 0-(Mab1 + Mad1) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2 : D2 = 0-(Mab1 + Mad1) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3 : D3 = 0-(Mba1 + Mbc1) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4 : D4 = 0-(Mba1 + Mbc1) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5 : D5 = 0-(Mcb1 + Mcd1) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6 : D6 = 0-(Mcb1 + Mcd1) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7 : D7 = 0-(Mdc1 + Mda1) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8 : D8 = 0-(Mdc1 + Mda1) * DF_da");

                #endregion

                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * DF_da");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * DF_da");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * DF_da");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * DF_da");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * DF_da");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * DF_da");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * DF_da");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * DF_da");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

                #region Table-2
                sw.WriteLine();
                sw.WriteLine(" Table-2 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE II ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                     DF_ab,
                    DF_ad,
                    DF_ba,
                    DF_bc,
                    DF_cb,
                    DF_cd,
                    DF_dc,
                    DF_da);
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                   "FEM",
                    "Mab2 =",
                    "Mad2 =",
                    "Mba2 =",
                    "Mbc2 =",
                    "Mcb2 =",
                    "Mcd2 =",
                    "Mdc2 =",
                    "Mda2 =");
                Mab = -m3;
                Mad = m20;
                Mba = m3;
                Mbc = -m20;
                Mcb = m22;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m22;


                SMab2 = Mab;
                SMad2 = Mad;
                SMba2 = Mba;
                SMbc2 = Mbc;
                SMcb2 = Mcb;
                SMcd2 = Mcd;
                SMdc2 = Mdc;
                SMda2 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                D1 = 0 - (Mab + Mad) * DF_ab;
                D2 = 0 - (Mab + Mad) * DF_ad;
                D3 = 0 - (Mba + Mbc) * DF_ba;
                D4 = 0 - (Mba + Mbc) * DF_bc;
                D5 = 0 - (Mcb + Mcd) * DF_cb;
                D6 = 0 - (Mcb + Mcd) * DF_cd;
                D7 = 0 - (Mdc + Mda) * DF_dc;
                D8 = 0 - (Mdc + Mda) * DF_da;


                //SMab1 += D1;
                //SMad1 += D2;
                //SMba1 += D3;
                //SMbc1 += D4;
                //SMcb1 += D5;
                //SMcd1 += D6;
                //SMdc1 += D7;
                //SMda1 += D8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");



                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                //D1 = 0 - (C1 + C2) * 0.5;
                //D2 = 0 - (C1 + C2) * 0.5;
                //D3 = 0 - (C3 + C4) * 0.5;
                //D4 = 0 - (C3 + C4) * 0.5;
                //D5 = 0 - (C5 + C6) * 0.5;
                //D6 = 0 - (C5 + C6) * 0.5;
                //D7 = 0 - (C7 + C8) * 0.5;
                //D8 = 0 - (C7 + C8) * 0.5;

                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1
                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");

                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;


                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3

                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;


                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");

                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab2.ToString("0.00"),
                                                    "" + SMad2.ToString("0.00"),
                                                    "" + SMba2.ToString("0.00"),
                                                    "" + SMbc2.ToString("0.00"),
                                                    "" + SMcb2.ToString("0.00"),
                                                    "" + SMcd2.ToString("0.00"),
                                                    "" + SMdc2.ToString("0.00"),
                                                    "" + SMda2.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1  : D1 = 0-(Mab2 + Mad2) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2  : D2 = 0-(Mab2 + Mad2) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3  : D3 = 0-(Mba2 + Mbc2) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4  : D4 = 0-(Mba2 + Mbc2) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5  : D5 = 0-(Mcb2 + Mcd2) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6  : D6 = 0-(Mcb2 + Mcd2) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7  : D7 = 0-(Mdc2 + Mda2) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8  : D8 = 0-(Mdc2 + Mda2) * DF_da");

                #endregion

                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * DF_da");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * DF_da");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * DF_da");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * DF_da");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * DF_da");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * DF_da");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * DF_da");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * DF_da");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

                #region Table-3
                sw.WriteLine();
                sw.WriteLine(" Table-3 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE III ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    DF_ab,
                    DF_ad,
                    DF_ba,
                    DF_bc,
                    DF_cb,
                    DF_cd,
                    DF_dc,
                    DF_da);
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                   "FEM",
                   "Mab3 =",
                    "Mad3 =",
                    "Mba3 =",
                    "Mbc3 =",
                    "Mcb3 =",
                    "Mcd3 =",
                    "Mdc3 =",
                    "Mda3 =");
                Mab = -m3;
                Mad = m25;
                Mba = m3;
                Mbc = -m25;
                Mcb = m27;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m27;

                SMab3 = Mab;
                SMad3 = Mad;
                SMba3 = Mba;
                SMbc3 = Mbc;
                SMcb3 = Mcb;
                SMcd3 = Mcd;
                SMdc3 = Mdc;
                SMda3 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                D1 = 0 - (Mab + Mad) * DF_ab;
                D2 = 0 - (Mab + Mad) * DF_ad;
                D3 = 0 - (Mba + Mbc) * DF_ba;
                D4 = 0 - (Mba + Mbc) * DF_bc;
                D5 = 0 - (Mcb + Mcd) * DF_cb;
                D6 = 0 - (Mcb + Mcd) * DF_cd;
                D7 = 0 - (Mdc + Mda) * DF_dc;
                D8 = 0 - (Mdc + Mda) * DF_da;


                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");



                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1
                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");

                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2
                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");


                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3
                C1 = D3 * DF_ab;
                C2 = D8 * DF_ad;
                C3 = D1 * DF_ba;
                C4 = D5 * DF_bc;
                C5 = D4 * DF_cb;
                C6 = D7 * DF_cd;
                C7 = D6 * DF_dc;
                C8 = D2 * DF_da;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");

                D1 = 0 - (C1 + C2) * DF_ab;
                D2 = 0 - (C1 + C2) * DF_ad;
                D3 = 0 - (C3 + C4) * DF_ba;
                D4 = 0 - (C3 + C4) * DF_bc;
                D5 = 0 - (C5 + C6) * DF_cb;
                D6 = 0 - (C5 + C6) * DF_cd;
                D7 = 0 - (C7 + C8) * DF_dc;
                D8 = 0 - (C7 + C8) * DF_da;


                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab3.ToString("0.00"),
                                                    "" + SMad3.ToString("0.00"),
                                                    "" + SMba3.ToString("0.00"),
                                                    "" + SMbc3.ToString("0.00"),
                                                    "" + SMcb3.ToString("0.00"),
                                                    "" + SMcd3.ToString("0.00"),
                                                    "" + SMdc3.ToString("0.00"),
                                                    "" + SMda3.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1  : D1 = 0-(Mab3 + Mad3) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2  : D2 = 0-(Mab3 + Mad3) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3  : D3 = 0-(Mba3 + Mbc3) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4  : D4 = 0-(Mba3 + Mbc3) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5  : D5 = 0-(Mcb3 + Mcd3) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6  : D6 = 0-(Mcb3 + Mcd3) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7  : D7 = 0-(Mdc3 + Mda3) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8  : D8 = 0-(Mdc3 + Mda3) * DF_da");

                #endregion


                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * DF_da");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * DF_da");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * DF_da");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * DF_da");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * DF_da");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * DF_da");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * DF_da");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * DF_ab");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * DF_ad");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * DF_ba");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * DF_bc");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * DF_cb");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * DF_cd");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * DF_dc");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * DF_da");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion


                #region Figure
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("        A                             B"));
                sw.WriteLine(string.Format("        -------------------------------"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        |                             |"));
                sw.WriteLine(string.Format("        -------------------------------"));
                sw.WriteLine(string.Format("        D                             C"));
                sw.WriteLine(string.Format("              Box Culvert"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format(""));
                #endregion Figure

                #region TABLE-4 SUMMARY OF SUPPORT MOMENTS FOR TOTAL LOADS

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE-4 SUMMARY OF SUPPORT MOMENTS FOR TOTAL LOADS");
                sw.WriteLine("--------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-10}{1,12}{2,18}{3,18}{4,18}",
                    "CASES",
                    "Mab",
                    "Mdc",
                    "Mad",
                    "Mda");
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                    "CASE 1",
                                    "SMab1 = " + Math.Abs(SMab1).ToString("0.000"),
                                    "SMdc1 = " + Math.Abs(SMdc1).ToString("0.000"),
                                    "SMad1 = " + Math.Abs(SMad1).ToString("0.000"),
                                    "SMda1 = " + Math.Abs(SMda1).ToString("0.000"));
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                    "CASE 2",
                    "SMab2 = " + Math.Abs(SMab2).ToString("0.000"),
                    "SMdc2 = " + Math.Abs(SMdc2).ToString("0.000"),
                    "SMad2 = " + Math.Abs(SMad2).ToString("0.000"),
                    "SMda2 = " + Math.Abs(SMda2).ToString("0.000"));
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                    "CASE 3",
                                    "SMab3 = " + Math.Abs(SMab3).ToString("0.000"),
                                    "SMdc3 = " + Math.Abs(SMdc3).ToString("0.000"),
                                    "SMad3 = " + Math.Abs(SMad3).ToString("0.000"),
                                    "SMda3 = " + Math.Abs(SMda3).ToString("0.000"));
                sw.WriteLine("------------------------------------------------------------------------------------");

                double SMabx, SMdcx, SMadx, SMdax;
                SMabx = 0.0;
                SMdcx = 0.0;
                SMadx = 0.0;
                SMdax = 0.0;

                //SMabx = (SMab1 > SMab2) ? ((SMab1 > SMab3) ? SMab1 : SMab3) : ((SMab2 > SMab3) ? SMab2 : SMab3);
                //SMdcx = (SMdc1 > SMdc2) ? ((SMdc1 > SMdc3) ? SMdc1 : SMdc3) : ((SMdc2 > SMdc3) ? SMdc2 : SMdc3);
                //SMadx = (SMad1 > SMad2) ? ((SMad1 > SMad3) ? SMad1 : SMad3) : ((SMad2 > SMad3) ? SMad2 : SMad3);
                //SMdax = (SMda1 > SMda2) ? ((SMda1 > SMda3) ? SMda1 : SMda3) : ((SMda2 > SMda3) ? SMda2 : SMda3);

                SMabx = (Math.Abs(SMab1) > Math.Abs(SMab2)) ? ((Math.Abs(SMab1) > Math.Abs(SMab3)) ? Math.Abs(SMab1) : Math.Abs(SMab3)) : ((Math.Abs(SMab2) > Math.Abs(SMab3)) ? Math.Abs(SMab2) : Math.Abs(SMab3));
                SMdcx = (Math.Abs(SMdc1) > Math.Abs(SMdc2)) ? ((Math.Abs(SMdc1) > Math.Abs(SMdc3)) ? Math.Abs(SMdc1) : Math.Abs(SMdc3)) : ((Math.Abs(SMdc2) > Math.Abs(SMdc3)) ? Math.Abs(SMdc2) : Math.Abs(SMdc3));
                SMadx = (Math.Abs(SMad1) > Math.Abs(SMad2)) ? ((Math.Abs(SMad1) > Math.Abs(SMad3)) ? Math.Abs(SMad1) : Math.Abs(SMad3)) : ((Math.Abs(SMad2) > Math.Abs(SMad3)) ? Math.Abs(SMad2) : Math.Abs(SMad3));
                SMdax = (Math.Abs(SMda1) > Math.Abs(SMda2)) ? ((Math.Abs(SMda1) > Math.Abs(SMda3)) ? Math.Abs(SMda1) : Math.Abs(SMda3)) : ((Math.Abs(SMda2) > Math.Abs(SMda3)) ? Math.Abs(SMda2) : Math.Abs(SMda3));

                //SMdcx = (SMdc1 > SMdc2) ? ((SMdc1 > SMdc3) ? SMdc1 : SMdc3) : ((SMdc2 > SMdc3) ? SMdc2 : SMdc3);
                //SMadx = (SMad1 > SMad2) ? ((SMad1 > SMad3) ? SMad1 : SMad3) : ((SMad2 > SMad3) ? SMad2 : SMad3);
                //SMdax = (SMda1 > SMda2) ? ((SMda1 > SMda3) ? SMda1 : SMda3) : ((SMda2 > SMda3) ? SMda2 : SMda3);


                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                      "MAXIMUM",
                                    "SMabx = " + SMabx.ToString("0.000"),
                                    "SMdcx = " + SMdcx.ToString("0.000"),
                                    "SMadx = " + SMadx.ToString("0.000"),
                                    "SMdax = " + SMdax.ToString("0.000"));

                sw.WriteLine("------------------------------------------------------------------------------------");

                sw.WriteLine("{0,-10}{1,12}{2,18}{3,18}{4,18}",
                     "",
                     "Top",
                     "Bottom",
                     "Top/Side",
                     "Bottom/Side");

                sw.WriteLine("------------------------------------------------------------------------------------");

                #endregion

                #region TABLE 5 : MID SPAN MOMENT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE-5 MID SPAN MOMENTS");
                sw.WriteLine("------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    "CASE 1",
                    "CASE 2",
                    "CASE 3");
                sw.WriteLine("------------------------------------------------------------------------------------");
                List<double> lst_Mab, lst_Mdc, lst_Mad;
                lst_Mab = new List<double>();

                lst_Mab.Add(Math.Abs(m6 - Math.Abs(SMab1)));
                lst_Mab.Add(Math.Abs(m6 - Math.Abs(SMab2)));
                lst_Mab.Add(Math.Abs(m6 - Math.Abs(SMab3)));
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mab",
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab1"),
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab2"),
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab3"));
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab1)),
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab2)),
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2} (Top)", lst_Mab[0]),
                                    String.Format("= {0:f2} (Top)", lst_Mab[1]),
                                    String.Format("= {0:f2} (Top)", lst_Mab[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");


                lst_Mdc = new List<double>();
                lst_Mdc.Add(Math.Abs(m10 - Math.Abs(SMdc1)));
                lst_Mdc.Add(Math.Abs(m10 - Math.Abs(SMdc2)));
                lst_Mdc.Add(Math.Abs(m10 - Math.Abs(SMdc3)));

                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mdc",
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc1"),
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc2"),
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc3"));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc1)),
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc2)),
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2} (Bottom)", lst_Mdc[0]),
                                    String.Format("= {0:f2} (Bottom)", lst_Mdc[1]),
                                    String.Format("= {0:f2} (Bottom)", lst_Mdc[2]));





                sw.WriteLine("------------------------------------------------------------------------------------");

                lst_Mad = new List<double>();
                lst_Mad.Add(Math.Abs(m18 - (Math.Abs(SMad1) + Math.Abs(SMda1)) / 2.0));
                lst_Mad.Add(Math.Abs(m24 - (Math.Abs(SMad2) + Math.Abs(SMda2)) / 2.0));
                lst_Mad.Add(Math.Abs(m29 - (Math.Abs(SMad3) + Math.Abs(SMda3)) / 2.0));



                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mad",
                    String.Format("  {0:f2}-{1:f2}", "m18", "(SMad1+SMda1)/2"),
                    String.Format("  {0:f2}-{1:f2}", "m24", "(SMad2+SMda2)/2"),
                    String.Format("  {0:f2}-{1:f2}", "m29", "(SMad3+SMda3)/2"));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m18, (Math.Abs(SMad1)), Math.Abs(SMda1)),
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m24, (Math.Abs(SMad2)), Math.Abs(SMda2)),
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m29, (Math.Abs(SMad3)), Math.Abs(SMda3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2} (Side)", lst_Mad[0]),
                                    String.Format("= {0:f2} (Side)", lst_Mad[1]),
                                    String.Format("= {0:f2} (Side)", lst_Mad[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------------------------------");
                #endregion

                #region Taking Maiximum Values, Design Moments
                sw.WriteLine();
                sw.WriteLine("  Taking Maximum Values, Design Moments");
                sw.WriteLine();
                sw.WriteLine("      M_AB for Corners /Supports A & B        = {0:f3} kN-m (Top)", SMabx);
                sw.WriteLine("      M_DC for Corners /Supports C & D        = {0:f3} kN-m (Bottom)", SMdcx);
                sw.WriteLine("      M_AD for Corners /Supports A & D        = {0:f3} kN-m (Top/Side)", SMadx);
                sw.WriteLine("      M_DA for Corners /Supports D & A        = {0:f3} kN-m (Bottom/Side)", SMdax);
                lst_Mab.Sort();

                double SMabx_1 = lst_Mab[2];
                sw.WriteLine("      M_AB for Mid span of the Top Slab       = {0:f3} kN-m (Top)", lst_Mab[2]);

                lst_Mdc.Sort();
                double SMdcx_1 = lst_Mdc[2];
                sw.WriteLine("      M_DC for Mid span of the Bottom Slab    = {0:f3} kN-m (Bottom)", lst_Mdc[2]);

                lst_Mad.Sort();
                double SMadx_1 = lst_Mad[2];
                sw.WriteLine("      M_AD for Mid span of the Two side walls = {0:f3} kN-m (Side)", lst_Mad[2]);
                #endregion


                #region STEP 5

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //double M = SMabx;
                //Chiranjit [2012 05 04]
                double M = Math.Abs(SMabx);

                if (M < Math.Abs(SMadx)) M = Math.Abs(SMadx);
                if (M < Math.Abs(SMabx_1)) M = Math.Abs(SMabx_1);

                sw.WriteLine("  Maximum Bending Moment at Support / Midspan");
                sw.WriteLine("  M = {0:f2} kN-m", M);
                double depth = Math.Sqrt((M * 10E5) / (1000 * R));
                sw.WriteLine();
                sw.WriteLine("  Depth = d = √((M * 10^6) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10^6) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Slab Thickness = {0:f2} mm", (d1 * 1000));
                double bar_dia = bar_dia_top;
                sw.WriteLine();
                sw.WriteLine("  Considering Clear Cover = {0:f2} mm and Reinforcement bar dia = {1} mm",
                    cover, bar_dia);
                double deff = (d1 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f2} = {3:f2} mm < {4:f2} mm NOT OK",
                        (d1 * 1000), cover, (bar_dia / 2.0), deff, depth);
                }

                j = 0.5 + Math.Sqrt((0.25) - ((M * 10E5) / (0.87 * sigma_c * 1000 * deff * deff)));
                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                double req_st_ast = (M * 10E5) / (0.87 * sigma_st * j * deff);
                sw.WriteLine();
               
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);
                double pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                double spacing = 1000 / (req_st_ast / pro_st_ast);
                //spacing = (int)((1000.0 / spacing) / 10.0);
                //spacing = spacing * 10.0;
                sw.WriteLine();
                sw.WriteLine("Abar = π*{0}^2/4 = {1:f3}", bar_dia, pro_st_ast);
                sw.WriteLine();
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine("          = 1000 / ({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                sw.WriteLine();
                spacing = (int)(spacing / 10.0);
                spacing = spacing * 10.0;
                sw.WriteLine("          = {0} mm", spacing);
                sw.WriteLine();
                if (spacing < 200)
                {
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }
               
               
                    lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(spacing);
                bd6 = bar_dia;
                bd7 = bar_dia;
                bd8 = bar_dia;
                sp6 = sp7 = sp8 = spacing * 2;


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area = p4 = {0:f2} kN/sq.m", p4);
                sw.WriteLine();
                sw.WriteLine("  Shear Force at distance = deff ,from face of wall");

                double shear_force = p4 * (b - (deff + cover) * 2 / 1000) / 2;
                sw.WriteLine("  = p4*[b-(deff+cover)*2/1000] / 2");
                sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    p4,
                    b,
                    deff,
                    cover);
                sw.WriteLine("  = {0:f2} kN", shear_force);
                double shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                double percent = req_st_ast * 100 / (1000 * deff);

                double tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);
                sw.WriteLine("  Using Table 1, given at end of this report  {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                double shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f2}*1000*{1:f2} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                double balance_shear = shear_force - shear_capacity;
                sw.WriteLine();
                sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                    shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                balance_shear = balance_shear * 1000;
                sw.WriteLine();

                //double ast_2 = Math.PI * 8 * 8 / 4 * (1000 / 250);

                double ast_2 = Math.PI * 10 * 10 / 4;



                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm per meter", ast_2);
                bd9 = bd10 = 10;
                sp9 = sp10 = 250;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(8);
                lst_Bar_Space.Add(250);
                #endregion


                double Asw = balance_shear * 250 / (200 * deff);
                sw.WriteLine();
                sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    balance_shear, ast_2, deff);

                sw.WriteLine("      = {0:f3} sq.mm.", Asw);


                double x = -((((shear_capacity * 2) / p4) - b) * (1000 / 2) + cover);
                x = x / 1000;
                sw.WriteLine();
                sw.WriteLine("  p4*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                double _x = x * 1000;
                //double _x = x / 100;
                //_x = (int)(_x + 1);
                //_x = _x * 100;



                sw.WriteLine("  x = -((((shear_capacity * 2) / p4) - b) * (1000 / 2) + cover)", x, _x);
                sw.WriteLine();
                sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p4, b, cover);


                sw.WriteLine("    = {0:f4} m = {1:f0} mm", x, _x);
                sw.WriteLine();
                if (_x < 300)
                    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                else
                {
                    sw.WriteLine("  As the required spacing per metre width is more than 300,");
                    sw.WriteLine("  Provided Shear reinforcements for a distance of 300mm from", _x);
                }
                sw.WriteLine("  the face of wall on both sides.");


                #endregion

                #region STEP 6


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : DESIGN OF BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span");


                M = Math.Abs(SMdcx);

                if (M < Math.Abs(SMdax)) M = Math.Abs(SMdax);
                if (M < Math.Abs(SMdcx_1)) M = Math.Abs(SMdcx_1);

                sw.WriteLine("  M = {0:f2} kN-m", M);
                depth = Math.Sqrt((M * 10E5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Depth Required = √((M * 10^6) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10^6) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Effective Depth deff = {0:f2} mm", (d1 * 1000));

                bar_dia = bar_dia_bottom;
                //sw.WriteLine("  Considering Clear Cover = {0:f2} mm and Reinforcement bar dia = {1} mm",
                //cover, bar_dia);
                deff = (d1 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm < {4:f2} mm NOT OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }

                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                req_st_ast = (M * 10E5) / (0.87 * sigma_st * j * deff);
                sw.WriteLine();
                //sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10^6)/(t*j*d)");
                //sw.WriteLine("                                        = ({0:f2}*10^6)/({1:f2}*{2:f3}*{3:f2})",
                //    M,
                //    t, j, deff);
                //sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);

                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);
                sw.WriteLine();
                sw.WriteLine("Abar = π*{0}^2/4 = {1:f3}", bar_dia, pro_st_ast);
                sw.WriteLine();
                spacing = 1000 / (req_st_ast / pro_st_ast);
                //spacing = 1000.0 / spacing;
                //spacing = (int)(spacing / 10.0);
                //spacing = (spacing * 10.0);
                //sw.WriteLine("Abar = π*{0}^2/4 = {1:f3}", bar_dia, pro_st_ast);
                sw.WriteLine();
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine("          = 1000 / ({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                sw.WriteLine();
                spacing = (int)(spacing / 10.0);
                spacing = spacing * 10.0;
                sw.WriteLine("          = {0} mm", spacing);


                if (spacing < 200)
                {
                    pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);
                    sw.WriteLine("Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);
                    sw.WriteLine("As the required spacing per metre width is more than 200,");
                    sw.WriteLine("Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }

                sw.WriteLine();
                //sw.WriteLine("  Provided T{0} bars @ {1:f0} mm c/c", bar_dia, spacing);

                bd1 = bd3 = bar_dia;
                sp1 = spacing * 2;
                sp3 = spacing;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(spacing);
                #endregion



                sw.WriteLine();
                //sw.WriteLine("  Provided Ast = {0:f3} sq.mm", pro_st_ast);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("  Total Load per unit Area = p4 = {0:f2} kN/sq.m", p4);
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");

                shear_force = p6 * (b - (deff + cover) * 2 / 1000) / 2;
                sw.WriteLine("  = p6*[b-(deff+cover)*2/1000] / 2");
                sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    p6,
                    b,
                    deff,
                    cover);
                sw.WriteLine("  = {0:f2} kN", shear_force);

                shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                percent = pro_st_ast * 100 / (1000 * deff);
                //percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);
                tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);

                sw.WriteLine("  Using Table 1, given at end of this report. {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                balance_shear = shear_force - shear_capacity;
                sw.WriteLine();
                sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                    shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                balance_shear = balance_shear * 1000;
                sw.WriteLine();
                ast_2 = Math.PI * 10 * 10 / 4 * (1000 / 250);

                Asw = balance_shear * 250 / (200 * deff);
                sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    balance_shear, 200, deff);

                sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                sw.WriteLine();
                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm per meter", ast_2);

                bd13 = bd11 = bd14 = bd12 = 10;
                sp13 = sp11 = sp14 = sp12 = 250;

                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(10);
                lst_Bar_Space.Add(250);
                #endregion

                x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover);
                x = x / 1000;
                sw.WriteLine();
                sw.WriteLine("  p6*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                _x = x * 1000;
                //double _x = x / 100;
                //_x = (int)(_x + 1);
                //_x = _x * 100;



                sw.WriteLine("  x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover)", x, _x);
                sw.WriteLine();
                sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p6, b, cover);


                sw.WriteLine("    = {0:f4} m = {1:f0} mm", x, _x);
                sw.WriteLine();
                if (_x < 300)
                    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                else
                {
                    sw.WriteLine("  As the required spacing per metre width is more than 300,");
                    sw.WriteLine("  Provided Shear reinforcements for a distance of 300mm from", _x);
                }
                sw.WriteLine("  the face of wall on both sides.");

                #endregion

                #region STEP 7

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : DESIGN OF SIDE WALLS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum moments at joints with top slab and bottom slab are save as");
                sw.WriteLine("  taken for Design of Slabs. So provide same Reinforments.");

                sw.WriteLine();
                //sw.WriteLine("  Midspan moments are calculated as:");
                //sw.WriteLine("      {0:f2}, {1:f2} and {2:f2}", lst_Mad[0], lst_Mad[1], lst_Mad[2]);
            
                lst_Mad.Sort();
                
                M = Math.Abs(SMadx_1);
                if (M < Math.Abs(SMadx)) M = Math.Abs(SMadx);
                if (M < Math.Abs(SMdax)) M = Math.Abs(SMdax);
                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span");
                sw.WriteLine("  M = {0:f2} kN-m", M);

                double eff_thickness = Math.Sqrt((M * 10e5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Effective Thickness of wall required = √((M * 10^6) / (1000 * R))");
                sw.WriteLine("                                       = √(({0:f2} * 10^6) / (1000 * {1:f3}))",
                    M, R);
                sw.WriteLine("                                       = {0:f2} mm", eff_thickness);
                if (deff > eff_thickness)
                {
                    sw.WriteLine(" Provided Effective thickness = {0:f2} > {1:f2} mm , OK", deff, eff_thickness);
                }
                else
                {
                    sw.WriteLine("      Provided Effective thickness = {0:f2} < {1:f2} mm , NOT OK", deff, eff_thickness);
                }

                req_st_ast = (M * 10E5) / (0.87 * sigma_st * j * deff);

                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);

                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                spacing = 1000.0 / (req_st_ast / pro_st_ast);
                //spacing = (int)((1000.0 / spacing) / 10.0);
                //spacing = spacing * 10.0;
                sw.WriteLine();
                sw.WriteLine("Abar = π*{0}^2/4 = {1:f3}", bar_dia, pro_st_ast);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine("          = 1000 / ({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                sw.WriteLine();
                spacing = (int)(spacing / 10.0);
                spacing = spacing * 10.0;
                sw.WriteLine("          = {0} mm", spacing);

                sw.WriteLine();
                sw.WriteLine();
                if (spacing < 200)
                {
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);
                }
                bar_dia = bar_dia_side;
                //pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 300.0);
                //sw.WriteLine("  Provided Ast = {0:f0} sq.mm ", pro_st_ast);
                sw.WriteLine();

                bd2 = bd5 = bar_dia;
                sp2 = sp5 = 300;

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area = p12 = p7 + p8 + p9 + p10 = {0:f2} kN/sq.m", p6);
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");
                p6 = p7 + p8 + p9 + p10;
                shear_force = p6 * (b - (deff + cover) * 2 / 1000) / 2;
                sw.WriteLine("  = p12*[b-(deff+cover)*2/1000] / 2");
                sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    p6,
                    b,
                    deff,
                    cover);
                sw.WriteLine("  = {0:f2} kN", shear_force);

                shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                percent = pro_st_ast * 100 / (1000 * deff);
                //percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);
                tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);

                sw.WriteLine("  Using Table 1, given at end of this report. {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                balance_shear = shear_force - shear_capacity;
                sw.WriteLine();
                sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                    shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                balance_shear = balance_shear * 1000;
                sw.WriteLine();
                ast_2 = Math.PI * 10 * 10 / 4 * (1000 / 250);

                Asw = balance_shear * 250 / (200 * deff);
                sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    balance_shear, 200, deff);

                sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                sw.WriteLine();
                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm per meter", ast_2);

                bd13 = bd11 = bd14 = bd12 = 10;
                sp13 = sp11 = sp14 = sp12 = 250;

                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(10);
                lst_Bar_Space.Add(250);
                #endregion

                x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover);
                x = x / 1000;
                sw.WriteLine();
                sw.WriteLine("  p12*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                _x = x * 1000;
                //double _x = x / 100;
                //_x = (int)(_x + 1);
                //_x = _x * 100;


                sw.WriteLine("  x = -((((shear_capacity * 2) / p12) - b) * (1000 / 2) + cover)", x, _x);
                sw.WriteLine();
                sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p6, b, cover);


                sw.WriteLine("    = {0:f4} m = {1:f0} mm", x, _x);
                sw.WriteLine();
                sw.WriteLine();
                if (_x < 300)
                    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                else
                {
                    sw.WriteLine("  As the required spacing per metre width is more than 300,");
                    sw.WriteLine("  Provided Shear reinforcements for a distance of 300mm from", _x);
                }
                sw.WriteLine("  the face of wall on both sides.");

                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 1 : PERMISSIBLE SHEAR STRESS ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_1(sw);

                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade, ref string ref_string)
        {

            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
        }

        public void Write_Table_1(StreamWriter sw)
        {
            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();
        }

        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {

                #region WRITE USER INPUT DATA

                sw.WriteLine("CODE : BOX CULVERT");
                sw.WriteLine("USER INPUT DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("H = {0:f3}", H);
                sw.WriteLine("b = {0:f3}", b);
                sw.WriteLine("d = {0:f3}", d);
                sw.WriteLine("d1 = {0:f3}", d1);
                sw.WriteLine("d2 = {0:f3}", d2);
                sw.WriteLine("d3 = {0:f3}", d3);
                sw.WriteLine("gamma_b = {0:f3}", gamma_b);
                sw.WriteLine("gamma_c = {0:f3}", gamma_c);
                sw.WriteLine("R = {0:f3}", R);
                sw.WriteLine("t = {0:f3}", t);
                sw.WriteLine("j = {0:f3}", j);
                sw.WriteLine("cover = {0:f3}", cover);
                sw.WriteLine("sigma_st = {0:f3}", sigma_st);
                sw.WriteLine("sigma_c = {0:f3}", sigma_c);
                sw.WriteLine("b1 = {0:f3}", b1);
                sw.WriteLine("b2 = {0:f3}", b2);
                sw.WriteLine("a1 = {0:f3}", a1);
                sw.WriteLine("w1 = {0:f3}", w1);
                sw.WriteLine("w2 = {0:f3}", w2);
                sw.WriteLine("b3 = {0:f3}", b3);
                sw.WriteLine("F = {0:f3}", F);
                sw.WriteLine("S = {0:f3}", S);
                sw.WriteLine("sbc = {0:f3}", sbc);
                sw.WriteLine();
                sw.WriteLine("FINISH");

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "BOX_CULVERT_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, H);
            string _box2 = "[" + _box1 + "]";
            double _a = b * 1000.0;
            double _b = d * 1000.0;
            double _c = 0;
            double _d = d1 * 1000.0;
            double _e = d2 * 1000.0;
            double _f = d3 * 1000.0;
            double _h = H * 1000.0;
            //double _pressure = 130.43;

            #endregion

            try
            {
                sw.WriteLine("_box1={0:f0}", _box1);
                sw.WriteLine("_box2={0:f0}", _box2);
                sw.WriteLine("_a={0:f0}", _a);
                sw.WriteLine("_b={0:f0}", _b);
                sw.WriteLine("_c={0:f0}", _c);
                sw.WriteLine("_d={0:f0}", _d);
                sw.WriteLine("_e={0:f0}", _e);
                sw.WriteLine("_f={0:f0}", _f);
                sw.WriteLine("_h={0:f0}", _h);
                sw.WriteLine("_pressure={0:f2}", _pressure);

                sw.WriteLine("_b1={0:f0}", lst_Bar_Dia[1]);
                sw.WriteLine("_s1={0:f0}", lst_Bar_Space[1]);
                sw.WriteLine("_b2={0:f0}", lst_Bar_Dia[2]);
                sw.WriteLine("_s2={0:f0}", lst_Bar_Space[2]);
                sw.WriteLine("_b3={0:f0}", lst_Bar_Dia[3]);
                sw.WriteLine("_s3={0:f0}", lst_Bar_Space[3]);
                sw.WriteLine("_b4={0:f0}", lst_Bar_Dia[4]);
                sw.WriteLine("_s4={0:f0}", lst_Bar_Space[4]);
                sw.WriteLine("_b5={0:f0}", lst_Bar_Dia[5]);
                sw.WriteLine("_s5={0:f0}", lst_Bar_Space[5]);
                sw.WriteLine("_b6={0:f0}", lst_Bar_Dia[6]);
                sw.WriteLine("_s6={0:f0}", lst_Bar_Space[6]);
                sw.WriteLine("_b7={0:f0}", lst_Bar_Dia[7]);
                sw.WriteLine("_s7={0:f0}", lst_Bar_Space[7]);
                sw.WriteLine("_b8={0:f0}", lst_Bar_Dia[8]);
                sw.WriteLine("_s8={0:f0}", lst_Bar_Space[8]);
                sw.WriteLine("_b9={0:f0}", lst_Bar_Dia[9]);
                sw.WriteLine("_s9={0:f0}", lst_Bar_Space[9]);
                sw.WriteLine("_b10={0:f0}", lst_Bar_Dia[10]);
                sw.WriteLine("_s10={0:f0}", lst_Bar_Space[10]);
                sw.WriteLine("_b11={0:f0}", lst_Bar_Dia[11]);
                sw.WriteLine("_s11={0:f0}", lst_Bar_Space[11]);
                sw.WriteLine("_b12={0:f0}", lst_Bar_Dia[12]);
                sw.WriteLine("_s12={0:f0}", lst_Bar_Space[12]);
                sw.WriteLine("_b13={0:f0}", lst_Bar_Dia[13]);
                sw.WriteLine("_s13={0:f0}", lst_Bar_Space[13]);
                sw.WriteLine("_b14={0:f0}", lst_Bar_Dia[14]);
                sw.WriteLine("_s14={0:f0}", lst_Bar_Space[14]);
                sw.WriteLine("_b15={0:f0}", lst_Bar_Dia[15]);
                sw.WriteLine("_s15={0:f0}", lst_Bar_Space[15]);
                sw.WriteLine("_b16={0:f0}", lst_Bar_Dia[16]);
                sw.WriteLine("_s16={0:f0}", lst_Bar_Space[16]);
                sw.WriteLine("_b17={0:f0}", lst_Bar_Dia[17]);
                sw.WriteLine("_s17={0:f0}", lst_Bar_Space[17]);
                sw.WriteLine("_b18={0:f0}", lst_Bar_Dia[18]);
                sw.WriteLine("_s18={0:f0}", lst_Bar_Space[18]);
                sw.WriteLine("_b19={0:f0}", lst_Bar_Dia[19]);
                sw.WriteLine("_s19={0:f0}", lst_Bar_Space[19]);
                sw.WriteLine("_b20={0:f0}", lst_Bar_Dia[20]);
                sw.WriteLine("_s20={0:f0}", lst_Bar_Space[20]);
                sw.WriteLine("_b21={0:f0}", lst_Bar_Dia[21]);
                sw.WriteLine("_s21={0:f0}", lst_Bar_Space[21]);



            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
                lst_Bar_Dia.Clear();
                lst_Bar_Space.Clear();
            }
        }
    }
    public  class PipeCulvert
    {
        #region Variable Declaration
        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string user_path = "";
        public bool is_process = false;

        IApplication iApp = null;

        public double Q, V, B, H1, H2, W, I;
        public bool IsHighway = true;

        //public PipeCulvertData pipe_data { get; set; }
        //Chiranjit [2012 11 13]
        public double pipe_dia { get; set; }
        public double long_reinf { get; set; }
        public double spiral_reinf { get; set; }
        public double ultimate_load { get; set; }
        public string culvert_class { get; set; }
        #endregion


        //flag true if user opens previous design
        public bool IsOpen = false;

        public PipeCulvert(IApplication iApp)
        {
            this.iApp = iApp;
            Q = 0.0;
            V = 0.0;
            B = 0.0;
            H1 = 0.0;
            H2 = 0.0;
            W = 0.0;
            I = 0.0;
            Project_Name = "Design of Pipe Culvert";
        }
        public string Project_Name { get; set; }
        public string FilePath
        {
            set
            {
                user_path = value;

                file_path = Path.Combine(user_path, Project_Name);
                if (!Directory.Exists(file_path))
                {
                    //if (IsOpen) return;
                    Directory.CreateDirectory(file_path);
                }
                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Culvert_Rcc_Pipe.TXT");
                user_input_file = Path.Combine(system_path, "PIPE_CULVERT.FIL");
            }
        }
        public void Pipe_WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("Q={0}", Q);
                sw.WriteLine("V={0}", V);
                sw.WriteLine("B={0}", B);
                sw.WriteLine("H1={0}", H1);
                sw.WriteLine("H2={0}", H2);
                sw.WriteLine("W={0}", W);
                sw.WriteLine("I={0}", I);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Calculate_Program()
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21              *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*            DESIGN OF PIPE CULVERT           *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                #region USER DATA
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Discharge through Pipe Culvert = Q = {0} cu.m/sec", Q);
                sw.WriteLine("Velocity of Flow through Pipe = V = {0} m/sec", V);
                sw.WriteLine("Width of Road = B = {0} m", B);
                sw.WriteLine("Bed Level of Flow = H1 = {0} m", H1);
                sw.WriteLine("Top Level of Embankment = H2 = {0} m", H2);
                sw.WriteLine("Wheel Load = W = {0} kN", W);
                sw.WriteLine("Impact Factor = I = {0} ", I);
                sw.WriteLine("Diameter of RCC Pipe = pd = {0} mm", pipe_dia);
                sw.WriteLine("Longitudinal Reinforcements = lr = {0} kg/m", long_reinf);
                sw.WriteLine("Spiral Reinforcement = sr = {0} kg/m ", spiral_reinf);
                sw.WriteLine("Ultimate_Load = sr = {0} kN/m ", ultimate_load);

                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region Step 1

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : DIAMETER OF PIPE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Discharge = Q = (π*d*d/4) * V");
                sw.WriteLine();

                double d = Math.Sqrt((4 * Q) / (Math.PI * V));
                sw.WriteLine(" Required Diameter = d = √(4*Q/π*V)");
                sw.WriteLine("                       = √(4*{0:f3}/π*{1:f2})", Q, V);
                sw.WriteLine("                       = {0:f3} m", d);
                sw.WriteLine();
                sw.WriteLine();
                 d = MyList.StringToDouble(d.ToString("f3"), 0.0);
                double _d = pipe_dia / 1000.0;

                if (_d < d)
                {
                    sw.WriteLine("Provided Diameter = {0:f3} m  <  {1:f3} m", _d, d);
                }
                else if (_d > d)
                {
                    sw.WriteLine("Provided Diameter = {0:f3} m  >  {1:f3} m", _d, d);

                }
                else
                {
                    sw.WriteLine("Provided Diameter = {0:f3} m  ", _d, d);
                }

                d = _d;



                double total_height = H2 - H1;
                double H = total_height - 1.0;

                double outer_dia = 0.0;
                double earthfill_load = iApp.Tables.Embankment_Loading(d * 1000, H, ref outer_dia, ref ref_string);

                sw.WriteLine();
                sw.WriteLine("Adopt {0} RCC Heavy Duty non pressure Pipe for carring heavy road traffic.", culvert_class);
                sw.WriteLine();
                sw.WriteLine("For Pipe of Internal Diameter = {0:f2} m", d);
                sw.WriteLine("               Outer Diameter = {0:f2} m  from TABLE 1", (outer_dia / 1000.0));

                #endregion

                #region Step 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Load by Earth Fill");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();




                sw.WriteLine("  Total Height = H2 - H1 = {0:f2} - {1:f2} = {2:f2} m", H2, H1, total_height);
                sw.WriteLine("  Considering Height of Culvert  = 1.0 m");
                sw.WriteLine("  Height of embankment over pipe = {0:f3} m", H);
                sw.WriteLine();

                // TO DO
                sw.WriteLine("  From Table 1 (given at end of this Report) Load by Earth fill = {0:f2} kN/m", earthfill_load);
                sw.WriteLine();
                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Load by Vehicle Wheel");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load by Vehicle Wheel Load = W = {0:f2} kN", W);

                // TO DO


                double Cs = 0.0;
                if (IsHighway)
                    Cs = iApp.Tables.Influence_Coefficient_Highway(d * 1000, H, ref outer_dia, ref ref_string);
                else
                    Cs = iApp.Tables.Influence_Coefficient_Railway(H, ref ref_string);

                //double Cs = 0.032;
                sw.WriteLine("  From Table 2 (given at end of this Report) : {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine();

                double pipe_load = 4 * Cs * I * W;
                sw.WriteLine("  Load on Pipe = 4 * Cs * I * W");
                sw.WriteLine("               = 4 * {0:f3} * {1:f3} * {2:f3}", Cs, I, W);
                sw.WriteLine("               = {0:f3} kN/m", pipe_load);
                sw.WriteLine();

                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Check for Strength Factor");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      The type of non Pressure Pipe and bedding should be so chosen that under");
                sw.WriteLine("    Maximum combination of field loading a Factor of Safety = 1.5 is available.");

                sw.WriteLine();
                sw.WriteLine("    (3-Edge Bearing Strength in kN/m) / (Factor of Safety) ");
                sw.WriteLine("  = (Load on Pipe by earthfill in kN/m) / (Corresponding Strength Factor) ");
                sw.WriteLine("    + (Load on Pipe by Wheel Load in kN/m) / (Strength Factor) ");
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard) sw.WriteLine("  Ref. IS:458:2003 code");
                sw.WriteLine();

                // Problem clan
                sw.WriteLine("  Three edge beam strength of {0} class {1:f0} mm diameter pipe is {2:f0} kN/m", culvert_class,(d * 1000), ultimate_load);

                double SF = (earthfill_load * I) / (ultimate_load - pipe_load);
                //double SF = 0.9;


                sw.WriteLine();
                sw.WriteLine("      So, {0:f2}/{1:f2} = ({2:f2}/S.F.) + ({3:f2}/{1:f2})",
                    ultimate_load,I, earthfill_load, pipe_load);
                sw.WriteLine();
                sw.WriteLine("      From above Required F.S. = {0:f3} ", SF);
                sw.WriteLine();
                sw.WriteLine("  For First class prepared ground bedding, F.S. = 2.3");
                sw.WriteLine();
                sw.WriteLine("  For Concrete concrete cradle bedding F.S. = 3.7");
                // Problem = away
                sw.WriteLine("  So, providing any of these two the F.S. is more than its required value of F.S.");
                #endregion

                #region Step 5

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Reinforcement in Pipe");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard) sw.WriteLine("  Ref. IS:458:2003 code");
                sw.WriteLine();
                sw.WriteLine();

                // Problem wire
                sw.WriteLine("Spiral Reinforcement of hard drawn steel wire {0} class {1:f0} mm diameter pipe", culvert_class, (d * 1000));
                sw.WriteLine();
                sw.WriteLine("with a permissible stress of 140 N/sq.mm is {0:f3} kg/m", spiral_reinf);

                sw.WriteLine();
                // Problem Mild
                sw.WriteLine("Longitudinal reinforcement of Mild Steel with a {0} class {1:f0} mm diameter pipe ", culvert_class, (d * 1000));
                sw.WriteLine();
                sw.WriteLine("permissible stress of 126.5 N/sq.mm is {0:f3} kg/m", long_reinf);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Using 12 mm dia bars @60 mm centre as Spiral Reinforcement");
                sw.WriteLine();

                double avg_d = (d + 0.100);
                sw.WriteLine("average diameter of Spiral = {0:f3} m", avg_d);
                sw.WriteLine();
                sw.WriteLine();

                double wt_one_sp_dia = Math.PI * avg_d * 0.88;
                sw.WriteLine(" Weight of One  spiral of 12 mm dia = π* {0:f3} * 0.88  = {1:f3} kg",avg_d,wt_one_sp_dia);
                sw.WriteLine();

                double no_sp = ((int)(d*1000.0 / 60.0)) + 1;

                sw.WriteLine();
                sw.WriteLine("Number of Spirals in {0:f3} m = ({1:f3}/60) = {2:f0}", (d), d*1000.0, no_sp);
                sw.WriteLine();
                double wt_per_meter = wt_one_sp_dia * no_sp;
                sw.WriteLine("Weight of Spiral Reinforcement per meter length of Pipe  = {0:f3} * {1:f2} = {2:f3} kg/m",wt_one_sp_dia,no_sp,wt_per_meter);
                sw.WriteLine();

                if (wt_per_meter > spiral_reinf)
                {
                    sw.WriteLine("So, spiral steel provided {0:f2} kg/m is more than minimum reinforcement of {1:f3} kg/m", wt_per_meter,spiral_reinf);
                }
                else
                {
                    sw.WriteLine("So, spiral steel provided {0:f2} kg/m is less than minimum reinforcement of {1:f3} kg/m", wt_per_meter, spiral_reinf);
                   
                }
                #endregion

                #region Step 6

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Longitudinal Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Providing 6 mm dia mild steel bars as Longitudial Reinforcement");
                sw.WriteLine();

                double wt_each_bar = Math.PI * (0.006 * 0.006) * 7800 / 4.0;
                sw.WriteLine("Weight of each bar = π *(0.006 * 0.006) * 7800 / 4.0");
                sw.WriteLine("                   = {0:f3} kg/m", wt_each_bar);

                double no_bar_req = (5.8 / wt_each_bar);

                sw.WriteLine();
                sw.WriteLine("Number of bars required = 5.8/{0:f0} = {1:f3}", wt_each_bar, no_bar_req);
                sw.WriteLine();
                double spacing = Math.PI * (avg_d*1000) / no_bar_req;

                sw.WriteLine("Spacing = π*1100/{0:f3} = {1:f3} mm", no_bar_req, spacing);

                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);

                sw.WriteLine();
                sw.WriteLine("Adopt {0:f0} mm spacing for the Longitudinal reinforcements.", spacing);

                #endregion

                Write_Tables(sw);
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------------------------");
                if (culvert_class == "NP-3")
                {
                    sw.WriteLine("TABLE 3 : Design and Strength Test Requirements of Concrete Pipes of Class NP3 - Reinforced");
                    sw.WriteLine("          Concrete, Medium Duty, Non-pressure Pipes");
                    sw.WriteLine("---------------------------------------------------------------------------------------------");
               
                    foreach (var item in iApp.Tables.PipeCulvert.Table_NP3)
                    {
                        sw.WriteLine(item);
                    }
                }
                else
                {
                    sw.WriteLine("TABLE 3 : Design and Strength Test Requirements of Concrete Pipes of Class NP4 - Reinforced");
                    sw.WriteLine("          Concrete, Medium Duty, Non-pressure Pipes");
                    sw.WriteLine("---------------------------------------------------------------------------------------------");
               
                    foreach (var item in iApp.Tables.PipeCulvert.Table_NP4)
                    {
                        sw.WriteLine(item);
                    }
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Calculate_Program_2012_11_13()
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21              *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*            DESIGN OF PIPE CULVERT           *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                #region USER DATA
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Discharge through Pipe Culvert = Q = {0} cu.m/sec", Q);
                sw.WriteLine("Velocity of Flow through Pipe = V = {0} m/sec", V);
                sw.WriteLine("Width of Road = B = {0} m", B);
                sw.WriteLine("Bed Level of Flow = H1 = {0} m", H1);
                sw.WriteLine("Top Level of Embankment = H2 = {0} m", H2);
                sw.WriteLine("Wheel Load = W = {0} kN", W);
                sw.WriteLine("Impact Factor = I = {0} ", I);

                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region Step 1

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : DIAMETER OF PIPE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Discharge = Q = (π*d*d/4) * V");

                double d = Math.Sqrt((4 * Q) / (Math.PI * V));
                sw.WriteLine("              d = √(4*Q/π*V)");
                sw.WriteLine("                = √(4*{0:f3}/π*{1:f2})", Q, V);
                sw.WriteLine("                = {0:f3} m", d);

                double _d = double.Parse(string.Format("{0:f3}", d));
                if (_d < 1.0)
                {
                    d = 1.0;
                    sw.WriteLine("                = {0:f3} m ", d);
                }
                else
                    d = _d;
                double total_height = H2 - H1;
                double H = total_height - 1.0;

                double outer_dia = 0.0;
                double earthfill_load = iApp.Tables.Embankment_Loading(d * 1000, H, ref outer_dia, ref ref_string);

                sw.WriteLine("  Adopt NP-3 RCC Heavy Duty non pressure Pipe for carring heavy road traffic.");
                sw.WriteLine();
                sw.WriteLine("  For Pipe of Internal Diameter = {0:f2} m", d);
                sw.WriteLine("                 Outer Diameter = {0:f2} m", (outer_dia / 1000.0));

                #endregion

                #region Step 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Load by Earth Fill");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();




                sw.WriteLine("  Total Height = H2 - H1 = {0:f2} - {1:f2} = {2:f2} m", H2, H1, total_height);
                sw.WriteLine("  Considering Height of Culvert  = 1.0 m");
                sw.WriteLine("  Height of embankment over pipe = {0:f3} m", H);

                // TO DO
                sw.WriteLine("  From Table 1 (given at end of this Report)");
                sw.WriteLine("  Load by Earth fill = {0:f2} kN/m", earthfill_load);
                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Load by Vehicle Wheel");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load by Vehicle Wheel Load = W = {0:f2} kN", W);

                // TO DO


                double Cs = 0.0;
                if (IsHighway)
                    Cs = iApp.Tables.Influence_Coefficient_Highway(d * 1000, H, ref outer_dia, ref ref_string);
                else
                    Cs = iApp.Tables.Influence_Coefficient_Railway(H, ref ref_string);

                //double Cs = 0.032;
                sw.WriteLine("  From Table 2 (given at end of this Report) : {0}", ref_string);
                sw.WriteLine();

                double pipe_load = 4 * Cs * I * W;
                sw.WriteLine("  Load on Pipe = 4 * Cs * I * W");
                sw.WriteLine("               = 4 * {0:f3} * {1:f3} * {2:f3}", Cs, I, W);
                sw.WriteLine("               = {0:f3} kN/m", pipe_load);

                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Check for Strength Factor");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      The type of non Pressure Pipe and bedding should be so chosen that under");
                sw.WriteLine("    Maximum combination of field loading a Factor of Safety = 1.5 is available.");

                sw.WriteLine();
                sw.WriteLine("  (3-Edge Bearing Strength in kN/m) / (Factor of Safety) ");
                sw.WriteLine("  = (Load on Pipe by earthfill in kN/m) / (Corresponding Strength Factor) ");
                sw.WriteLine("  + (Load on Pipe by Wheel Load in kN/m) / (Strength Factor) ");
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard) sw.WriteLine("  Ref. IS:458:2003 code");
                sw.WriteLine();

                // Problem clan
                sw.WriteLine("  Three edge beam strength of NP-3 class {0:f0} mm diameter pipe is 111 kN/m", (d * 1000));

                double SF = (earthfill_load * I) / (111.0 - pipe_load);
                //double SF = 0.9;


                sw.WriteLine("      So, 111/{0:f2} = ({1:f2}/S.F.) + ({2:f2}/{0:f2})",
                    I, earthfill_load, pipe_load);
                sw.WriteLine("      From above Required S.F. = {0:f3} ", SF);
                sw.WriteLine("  For First class prepared ground bedding, S.F = 2.3");
                sw.WriteLine("  For Concrete concrete cradle bedding S.F = 3.7");
                // Problem = away
                sw.WriteLine("  So, providing any of these two the S.F. is more than its required value of S.F.");
                #endregion

                #region Step 5

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Reinforcement in Pipe");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                if (iApp.DesignStandard == eDesignStandard.IndianStandard) sw.WriteLine("  Ref. IS:458:2003 code");
                sw.WriteLine();
                sw.WriteLine();

                // Problem wire
                sw.WriteLine("  Spiral Reinforcement of hard drawn steel wire ");
                sw.WriteLine("with a permissible stress of 140 N/sq.mm is 44 kg/m");

                sw.WriteLine();
                // Problem Mild
                sw.WriteLine("Longitudinal reinforcement of Mild Steel with a ");
                sw.WriteLine("permissible stress of 126.5 N/sq.mm is 5.80 kg/m");
                sw.WriteLine("Using 12 mm dia bars @60 mm centre as Spiral Reinforcement");
                sw.WriteLine("average diameter of Spiral = 1.1 m");
                sw.WriteLine();

                double wt_one_sp_dia = Math.PI * 1.1 * 0.88;
                sw.WriteLine(" Weight of One  spiral of 12 mm dia");
                sw.WriteLine("                  = π* 1.1 * 0.88");
                sw.WriteLine("                  = {0:f3} kg", wt_one_sp_dia);

                double no_sp = 1000.0 / 60.0;

                sw.WriteLine();
                sw.WriteLine("Number of Spirals in 1 m = 1000/60 = 16.66");
                sw.WriteLine();
                sw.WriteLine("Weight of Spiral Reinforcement per meter length of Pipe");
                double wt_per_meter = wt_one_sp_dia * no_sp;
                sw.WriteLine("              = {0:f3} * {1:f2}", wt_one_sp_dia, no_sp);
                sw.WriteLine("              = {0:f3} kg/m", wt_per_meter);
                sw.WriteLine("  So, spiral steel provided {0:f2} kg/m is more ", wt_per_meter);
                sw.WriteLine(" than minimum reinforcement of 44 kg/m as specified in IS:485-197");
                #endregion

                #region Step 6

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Longitudinal Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Providing 6 mm dia mild steel bars as Longitudial Reinforcement");
                sw.WriteLine();

                double wt_each_bar = Math.PI * (0.006 * 0.006) * 7800 / 4.0;
                sw.WriteLine("Weight of each bar = π *(0.006 * 0.006) * 7800 / 4.0");
                sw.WriteLine("                   = {0:f3} kg/m", wt_each_bar);

                double no_bar_req = (5.8 / wt_each_bar);

                sw.WriteLine();
                sw.WriteLine("Number of bars required = 5.8/{0:f0} = {1:f3}", wt_each_bar, no_bar_req);
                sw.WriteLine();
                double spacing = Math.PI * 1100 / no_bar_req;

                sw.WriteLine("Spacing = π*1100/{0:f3} = {1:f3} mm", no_bar_req, spacing);

                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);

                sw.WriteLine();
                sw.WriteLine("Adopt {0:f0} mm spacing for the Longitudinal reinforcements.", spacing);

                #endregion

                Write_Tables(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Write_Tables(StreamWriter sw)
        {
            string file_name = Path.Combine(Application.StartupPath, "TABLES");
            file_name = Path.Combine(file_name, "PIPE_CULVERT_TABLE_5_3.txt");
            List<string> lst_cont = iApp.Tables.Get_Tables_Embankment_Loading();

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("TABLE 1 : EMBANKMENT LOADING ON PIPE ");
            sw.WriteLine("------------------------------------");
            for (int i = 0; i < lst_cont.Count; i++)
            {
                sw.WriteLine(lst_cont[i]);
            }
            lst_cont.Clear();


            sw.WriteLine();
            file_name = Path.Combine(Path.GetDirectoryName(file_name), "PIPE_CULVERT_TABLE_5_4.txt");

            lst_cont = IsHighway ? iApp.Tables.Get_Tables_Influence_Coefficient_Highway() : iApp.Tables.Get_Tables_Influence_Coefficient_Railway();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("TABLE 2 : Cs for Various Depths");
            sw.WriteLine("-------------------------------");
            for (int i = 0; i < lst_cont.Count; i++)
            {
                sw.WriteLine(lst_cont[i]);
            }
            lst_cont.Clear();


        }
    }
    public  class SlabCulvert
    {

        //flag true if user opens previous design
        public bool IsOpen = false;

        #region Variable Declaration

        public string file_path = "";
        public string rep_file_name = "";
        public string user_input_file = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_path = "";


        public double D, CW, FP, L, WC, support_width, conc_grade, st_grade, sigma_cb, sigma_st;
        public double m, j, Q, a1, b1, b2, W1, cover, delta_c, delta_wc;
        public double bar_dia;
        public CONCRETE_GRADE CON_GRADE;

        public bool is_process = false;

        IApplication iApp = null;

        #region  Drawing Variable
        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp7;

        #endregion

        #endregion
        public SlabCulvert(IApplication app)
        {
            iApp = app;
            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;
            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp7 = 0.0;
            Project_Name = "Design of Slab Culvert";

        }
        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("D  = {0:f2}", D);
                sw.WriteLine("CW  = {0:f2}", CW);
                sw.WriteLine("FP  = {0:f2}", FP);
                sw.WriteLine("L  = {0:f2}", L);
                sw.WriteLine("WC  = {0:f2}", WC);
                sw.WriteLine("support_width  = {0:f2}", support_width);
                sw.WriteLine("conc_grade  = {0:f2}", conc_grade);
                sw.WriteLine("st_grade  = {0:f2}", st_grade);
                sw.WriteLine("sigma_cb  = {0:f2}", sigma_cb);

                sw.WriteLine("sigma_st  = {0:f2}", sigma_st);
                sw.WriteLine("m  = {0:f2}", m);
                sw.WriteLine("j  = {0:f2}", j);
                sw.WriteLine("Q  = {0:f2}", Q);
                sw.WriteLine("a1  = {0:f2}", a1);
                sw.WriteLine("b1  = {0:f2}", b1);
                sw.WriteLine("b2  = {0:f2}", b2);
                sw.WriteLine("W1  = {0:f2}", W1);
                sw.WriteLine("cover  = {0:f2}", cover);
                sw.WriteLine("delta_c  = {0:f2}", delta_c);
                sw.WriteLine("delta_wc  = {0:f2}", delta_wc);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        //Chiranjit [2016 08 08] Add Project Name
        public string Project_Name { get; set; }

        public string FilePath
        {
            set
            {
                user_path = value;
                //Chiranjit [2016 08 08] Add Project Name
                //file_path = Path.Combine(user_path, "Design of Slab Culvert");
                file_path = Path.Combine(user_path, Project_Name);
                if (!Directory.Exists(file_path))
                {
                    if (!Directory.Exists(file_path))
                        Directory.CreateDirectory(file_path);
                }
                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Culvert_Rcc_Slab.TXT");
                user_input_file = Path.Combine(system_path, "SLAB_CULVERT.FIL");
                drawing_path = Path.Combine(system_path, "SLAB_CULVERT_DRAWING.FIL");
            }
        }

        public void Calculate_Program(string fileName)
        {
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21              *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*            DESIGN OF SLAB CULVERT           *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region User's Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Thickness of Slab [D] = {0:f2} mm               Marked as (B) in the Drawing", D);
                sw.WriteLine("Carriageway width [CW] = {0:f2} m               Marked as (D) in the Drawing", CW);
                sw.WriteLine("Footpath width [FP] = {0:f2} m", FP);
                sw.WriteLine("Clear Span [L] = {0:f2} m                       Marked as (A) in the Drawing", L);
                sw.WriteLine("Thickness of Wearing Course [WC] = {0:f2} mm    Marked as (C) in the Drawing", WC);
                sw.WriteLine("Width of End Support / Bearing = {0:f2} m       Marked as (E) in the Drawing", support_width);
                sw.WriteLine("Concrete Grade = M {0:f0} ", conc_grade);
                sw.WriteLine("Steel Grade = Fe {0:f0} ", st_grade);
                sw.WriteLine("Permissible Stress [σ_cb] = {0:f2} N/sq. mm", sigma_cb);
                sw.WriteLine("Permissible Stress [σ_st] = {0:f2} ", sigma_st);
                sw.WriteLine("Modular Ratio [m] = {0:f2} ", m);
                sw.WriteLine("Bar Diameter [bar_dia] = {0:f0} ", bar_dia);
                sw.WriteLine("Q = {0:f2} ", Q);
                sw.WriteLine("Live Load Dimension [a1] = {0:f2} m", a1);
                sw.WriteLine("Live Load Dimension [b1] = {0:f2} m", b1);
                sw.WriteLine("Live Load Dimension [b2] = {0:f2} m", b2);
                sw.WriteLine("Total Live Load [W1] = {0:f2} kN", W1);
                sw.WriteLine("Clear cover to Reinforcement Bars [cover] = {0:f2} mm", cover);
                sw.WriteLine("Unit weight of Concrete [γ_c] = {0:f2} N/cu.m", delta_c);
                sw.WriteLine("Unit weight of Wearing course [γ_wc] = {0:f2} N/cu.m", delta_wc);
                sw.WriteLine();
                sw.WriteLine();



                #endregion


                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculating Effective Span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Overall thickness of Slab = {0:f2} mm = D", D);

                double deff = D - (cover + 10);
                sw.WriteLine("Effective thickness of Slab = {0:f2} - ({1:f2} + 10) = {2:f2} mn = deff",
                    D,
                    cover,
                    deff);
                sw.WriteLine();

                sw.WriteLine("Effective span is lesser of");
                double total_span = L + (deff / 1000);
                sw.WriteLine("  i)   Clear Span + Effective Depth = {0:f2} + {1:f2} = {2:f2} m",
                    L, (deff / 1000), total_span);

                double l = L + support_width;
                sw.WriteLine("  ii)  Centre to Centre distance of End Supports / Bearings = {0:f2} + {1:f2} = {2:f2} m",
                    L, support_width, l);


                sw.WriteLine();
                sw.WriteLine("  So, Effective Span = {0:f2} m = l", l);



                #endregion

                #region STEP 2

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Bending Moment by Permanent Loads ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double slab_weight = (D / 1000.0) * delta_c;

                sw.WriteLine("Weight of Slab = (D /1000) * γc = {0:f3} * {1:f2} = {2:f2} kN/sq.m.",
                    (D / 1000.0),
                    delta_c,
                    slab_weight);

                double wt_wear_cour = (WC / 1000) * delta_wc;

                sw.WriteLine("Weight of Wearing Course = (WC /1000) * γwc = {0:f3} * {1} = {2:f3} kN/sq.m.",
                    (WC / 1000.0), delta_wc, wt_wear_cour);


                double W2 = (int)(slab_weight + wt_wear_cour);
                W2 = W2 + 1;

                sw.WriteLine();
                sw.WriteLine("Total Load = {0:f3} kN/sq.m = {1} kN/sq.m. = W2",
                    (slab_weight + wt_wear_cour), W2);

                double M1 = (W2 * l * l) / 8;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Permanent Loads = M1 = ({0:f2} * l*l) / 8", W2);
                sw.WriteLine("                                    = ({0:f2} * {1:f2}*{1:f2}) / 8 ",
                    W2, l);
                sw.WriteLine("                                    = {0:f2} kN-m", M1);

                #endregion

                #region STEP 3

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Bending Moment by Vehicle Load / Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("For 5m Span Impact Factor 25%");
                sw.WriteLine("For 9m Span Impact Factor 10%");

                sw.WriteLine();
                sw.WriteLine("So, for {0:f2} m Span Impact factor ", l);
                //double im_fact = 25 - ((25 - 10) / (9 - 5)) * (6.4 - 5);


                double fact = 25.0 - ((25.0 - 10.0) / (9.0 - 5.0)) * (6.4 - 5);

                //double fact = 25 - ((25 - 10) / (9 - 5)) * (l - 5);
                sw.WriteLine("  = 25 - ((25 - 10) / (9 - 5)) * ({0:f2} - 5) = {1:f2}% = fact", l, fact);

                sw.WriteLine();
                sw.WriteLine("Length of Load = a1 = {0:f2} m.", a1);

                double ld = a1 + 2 * ((D + WC) / 1000);

                sw.WriteLine("Length of Load including 45° dispersal = a1 + 2 * ((D + WC) / 1000)");
                sw.WriteLine("                                       = {0:f2} + 2 * (({1:f2} + {2:f2}) / 1000)",
                    a1, D, WC);
                sw.WriteLine("                                       = {0:f2} m. = ld", ld);

                sw.WriteLine();
                sw.WriteLine("Effective Width of Slab perpendicular to Span = be = K * x * (1 - (x / L)) + bw");

                sw.WriteLine();
                sw.WriteLine("Placing the Load symmetrically on the Span");

                double x = l / 2;
                sw.WriteLine("x = Distance from centre of end support to centre of Load = l/2 = {0:f2}/2={1:f2} m.", l, x);

                double B = CW + (2 * FP);
                sw.WriteLine("B = Width of Slab = CW + (2 * FP) =  {0:f2} + (2 * {1:f2}) = {2:f2} m",
                    CW, FP, B);


                double b_by_l = B / l;
                sw.WriteLine();
                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                double bw = b1 + (2 * (WC / 1000));

                sw.WriteLine("bw = b1 + (2 * (WC / 1000)) = {0:f3} + (2 * {1:f3}) = {2:f3} m.",
                    b1, (WC / 1000.0), bw);

                sw.WriteLine();
                sw.WriteLine("From Table of IRC 21:2000 ");
                sw.WriteLine();

                double K = KValue.Get_K_Value(b_by_l);
                //double K = 2.84;

                sw.WriteLine();
                sw.WriteLine("For B / l = {0:f3}, for simply Supported Slab,  K = {1:f3}", b_by_l, K);
                double be = K * x * (1 - (x / l)) + bw;

                //sw.WriteLine("So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.");
                sw.WriteLine();
                sw.WriteLine("So, Effective Width of Load = be ");
                sw.WriteLine("                            = {0:f2} * {1:f2} * (1 - ({1:f2} / {2:f2})) + {3:f2}",
                    K, x, l, bw);
                sw.WriteLine("                            = {0:f2} m.", be);

                double Wd = (2 * (be / 2)) + (2 * (b1 / 2)) + b2;

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45 dispersal = Wd");
                sw.WriteLine("                                =(2 * (be / 2)) + (2 * (b1 / 2)) + b2");
                sw.WriteLine("                                =(2 * ({0:f2}) / 2)) + (2 * ({1:f2} / 2)) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                = {0:f3} m", Wd);


                double TLL = W1 * ((fact / 100.0) + 1.0);
                sw.WriteLine();
                sw.WriteLine("Total Live Load including Impact = TLL");
                sw.WriteLine("                                 = W1 * ((fact/ 100.0) + 1.0) kN");
                sw.WriteLine("                                 = {0:f2} * ({1:f2}) ", W1, (fact * L / 100));
                sw.WriteLine("                                 = {0:f0} kN", TLL);

                double LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit Area = LLUA");
                sw.WriteLine("                        = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f0} / ({1:f2} * {2:f2})", TLL, ld, Wd);

                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double M2 = Math.Abs(((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4));

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Live Load = M2");
                sw.WriteLine("                             = ((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4)");
                sw.WriteLine("                             = (({0:f2}*{1:f2})/2) * ({2:f2}/2) - (({0:f2} * {1:f2})/2) * ({1:f2}/4)",
                    LLUA, ld, l);
                sw.WriteLine("                             = {0:f2} kN-m", M2);


                double M = M1 + M2;

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment = M = M1 + M2 ");
                sw.WriteLine("                          = {0:f2} + {1:f2}", M1, M2);
                sw.WriteLine("                          = {0:f2} kN-m", M);
                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Shear Force by Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Effective Span = l = {0:f2} m", l);
                sw.WriteLine("Length of Load including 45° dispersal = ld = {0:f2} m", ld);
                sw.WriteLine();
                sw.WriteLine("To get maximum Shear Force at support let us place the Load ");
                sw.WriteLine("coinciding the start point of the above lengths");

                x = ld / 2;

                sw.WriteLine();
                sw.WriteLine("x = ld / 2 = {0:f2} / 2 = {1:f2} m.", ld, x);
                b_by_l = B / l;

                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                K = KValue.Get_K_Value(b_by_l);
                sw.WriteLine();
                sw.WriteLine("From IRC 21:2000, K = {0:f3}", K);
                sw.WriteLine("bw = {0:f2} m", bw);

                be = K * x * (1 - (x / l)) + bw;

                sw.WriteLine();
                sw.WriteLine("Effective width of Load = be");
                sw.WriteLine("                        = K * x * (1 - (x / l)) + bw");
                sw.WriteLine("                        = {0:f2} * {1:f2} * (1 - ({1:f2}/{2:f2})) + {3:f3}",
                    K, x, l, bw);
                sw.WriteLine("                        = {0:f2} m", be);

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45° dispersal = Wd");
                sw.WriteLine("                                 =((2 * be) / 2) + ((2 * b1) / 2) + b2");
                sw.WriteLine("                                 =((2 * {0:f2}) / 2) + ((2 * {1:f2}) / 2) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                 = {0:f3} m", Wd);

                LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit area = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f2} / ({1:f2} * {2:f2})", TLL, ld, Wd);
                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double V1 = (LLUA * ld * 2 * (b1 + 2 * (WC / 1000) + 2 * (D / 1000)) / l);

                sw.WriteLine();
                sw.WriteLine("Shear Force by Live Load = V1");
                sw.WriteLine("                         = [LLUA * ld * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)]/l");
                sw.WriteLine("                         = [{0:f2} * {1:f2} * 2 * ({2:f2} + 2 * {3:f3} + 2 * {4:f3}] / {5:f3}",
                    LLUA, ld, b1, (WC / 1000.0), (D / 1000), l);
                sw.WriteLine("                         = {0:f3} kN", V1);


                double V2 = W2 * l / 2;
                sw.WriteLine();
                sw.WriteLine("Dead Load Shear = V2 = W2 * l / 2 ");
                sw.WriteLine("                     = ({0:f2} * {1:f2}) / 2 ", W2, l);
                sw.WriteLine("                     = {0:f2} kN", V2);

                double V = V1 + V2;

                sw.WriteLine();
                sw.WriteLine("Total Design Shear = V = V1 + V2 kN");
                sw.WriteLine("                       = {0:f2} + {1:f2} kN", V1, V2);
                sw.WriteLine("                       = {0:f2} kN", V);
                #endregion

                #region STEP 5 : Structural Design of Slab
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Structural Design of Slab ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                M = Math.Abs(M);
                double d = Math.Sqrt((M * 10E5) / (Q * 1000));
                sw.WriteLine("Required Effective Depth = d = √((M * 10^6) / (Q * b))");
                sw.WriteLine("                             = √(({0:f2} * 10^6) / ({1:f2} * 1000))",
                    M, Q);
                sw.WriteLine("                             = {0:f2} m", d);
                sw.WriteLine();


                sw.WriteLine();
                if (deff > d)
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} > d = {1:f2} OK", deff, d);
                }
                else
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} < d = {1:f2} NOT OK", deff, d);
                }
                j = 0.5 + Math.Sqrt((0.25) - ((M * 10E5) / (0.87 * ((int)conc_grade) * 1000 * deff * deff)));
                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                //double Ast = (M * 10E5) / (sigma_st * j * deff);
                double Ast = (M * 10E5) / (0.87 * sigma_st * j * deff);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", Ast);
                //sw.WriteLine("Required Steel reinforcement = Ast = ( M * 10^6) / (σ_st * j * d)");
                //sw.WriteLine("                                   = ({0:f2} * 10^6) / ({1:f2} * {2:f3} * {3:f2})",
                //    M,
                //    sigma_st, j, deff);
                //sw.WriteLine("                                   = {0:f3} sq.mm", Ast);

                double spacing = (1000 * Math.PI * bar_dia * bar_dia / 4.0) / Ast;

                sw.WriteLine();
                sw.WriteLine("Using {0:f0} mm dia. Bars, spacing = [1000 * ((π * {0:f0} * {0:f0}) / 4)] / {1:f2}", bar_dia, Ast);
                sw.WriteLine("                               = {0:f2} mm", spacing);

                if (spacing > 50 && spacing < 200)
                {
                    spacing = (int)(spacing / 10.0); ;
                    spacing = (spacing * 10.0); ;
                }
                else if (spacing < 50)
                {
                    sw.WriteLine("Spacing {0:f0} mm c/c < 50 mm c/c , NOT OK, Redesign", spacing);
                }
                else
                {
                    spacing = 200;
                }
                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0}  @{1:f0} mm c/c          Marked as (1) in the Drawing", bar_dia, spacing);

                _bd1 = 20;
                _sp1 = spacing;

                double BMDS = 0.2 * M1 + 0.3 * M2;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2");
                sw.WriteLine("                                      = 0.2 * {0:f2} + 0.3 * {1:f2}", M1, M2);
                sw.WriteLine("                                      = {0:f3} kN-m", BMDS);
                BMDS = (int)BMDS;
                BMDS += 1.0;

                sw.WriteLine("                                      = {0:f2} kN-m", BMDS);

                double e_dep = deff - 10.0 - 10.0 / 2.0;
                sw.WriteLine();
                sw.WriteLine("Using 12 mm. dia Bars, Effective Depth = deff - (10 + 6) = {0:f2} mm", e_dep);

                double req_steel = BMDS * 10E5 / (sigma_st * j * e_dep);

                sw.WriteLine();
                sw.WriteLine("Required Steel = ({0:f2} * 10^6) / ( {1:f3} * {2:f3} * {3:f2}) = {4:f3} sq.mm",
                    BMDS, sigma_st, j, e_dep, req_steel); ;

                spacing = (1000 * Math.PI * 12 * 12) / (req_steel * 4);
                sw.WriteLine();
                sw.WriteLine("Spacing of bars = (1000 * π * 12 * 12) / ({0:f2} * 4)", req_steel);
                sw.WriteLine("                = {0:f3} mm", spacing);

                if (spacing > 150)
                    spacing = 150;
                else
                {
                    spacing = (int)(spacing / 10.0);
                    spacing = (spacing * 10.0);

                }

                sw.WriteLine();
                sw.WriteLine("Provide T12 @ {0:f0} mm c/c as distribution Steel.      Marked as (2) in the Drawing", spacing);

                _bd2 = 12;
                _sp2 = spacing;
                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Shear Reinforcements ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double tau = V * 10E2 / (1000 * deff);

                sw.WriteLine("Design Shear = τ = (V * 10E2) / (b * deff) ");
                sw.WriteLine("                 = ({0:f2} * 10E2) / (1000 * {1:f2}) N/sq.mm", V, deff);
                sw.WriteLine("                 = {0:f3} N/sq.mm", tau);
                sw.WriteLine();
                double K1 = 1.14 - 0.7 * (deff / 1000); // 1.14 = ?, 0.7 = ?
                if (K1 >= 0.5)
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} >= 0.5, O.K.",
                        (deff / 1000.0),
                        K1);
                }
                else
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} < 0.5, NOT OK",
                        (deff / 1000.0),
                        K1);
                }

                sw.WriteLine();
                sw.WriteLine("Bending up alternate bars, provide T20 bars @ 280 mm c/c");



                double ast_prov = (Math.PI * 20 * 20 / 4.0) * (1000.0 / 280.0);
                sw.WriteLine();
                sw.WriteLine("Ast provided = {0:f0} sq.mm", ast_prov);

                double p = (ast_prov * 100) / (1000 * deff);

                sw.WriteLine();
                sw.WriteLine("Percentage p = ({0:f0} * 100) / (1000 * {1:f2}) = {2:f3}%",
                    ast_prov, deff, p);

                double K2 = 0.5 + 0.25 * p;
                sw.WriteLine();
                if (K2 < 1.0)
                {
                    //sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} < 1.0,  NOT OK.", p, K2);
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} < 1.0", p, K2);
                    K2 = 1.0;
                    sw.WriteLine("So, K2 = 1.0");

                }
                else
                {
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} >= 1.0, OK.", p, K2);
                }

                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------");
                sw.WriteLine("Concrete Grade (M)  15     20     25     30     35     40 ");
                sw.WriteLine("τ_co               0.28   0.34   0.40   0.45   0.50   0.50");
                sw.WriteLine("----------------------------------------------------------");
                sw.WriteLine();
                double tau_co = 0.0;
                switch (CON_GRADE)
                {
                    case CONCRETE_GRADE.M15:
                        tau_co = 0.28;
                        break;
                    case CONCRETE_GRADE.M20:
                        tau_co = 0.34;
                        break;
                    case CONCRETE_GRADE.M25:
                        tau_co = 0.40;
                        break;
                    case CONCRETE_GRADE.M30:
                        tau_co = 0.45;
                        break;
                    case CONCRETE_GRADE.M35:
                        tau_co = 0.50;
                        break;
                    case CONCRETE_GRADE.M40:
                        tau_co = 0.50;
                        break;
                }

                sw.WriteLine("Therefore, Permissible Shear Stress");
                double tau_c = K1 * K2 * tau_co;
                sw.WriteLine("τ_c = K1 * K2 * τ_co");
                sw.WriteLine("    = {0:f3} * {1:f3} * {2:f3}", K1, K2, tau_co);
                if (tau_c > tau)
                    sw.WriteLine("    = {0:f3} > τ = {1:f3} N/sq.mm, OK", tau_c, tau);
                else
                    sw.WriteLine("    = {0:f3} < τ = {1:f3} N/sq.mm, NOT OK, more Steel will required.", tau_c, tau);


                //sw.WriteLine("      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
                //sw.WriteLine("So, O.K.
                //sw.WriteLine("Else not O.K.. more Steel will be required.

                #endregion


                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }



            #region Page 1 USER DATA
            //Thickness of Slab (Assumed) = 500 mm
            //Carriageway width   = 7.5 m = CW
            //Footpath width         = 1.0 m = FP
            //Clear Span = 6.0 m

            //Thickness of Wearing Course = 80 mm = WC
            //Width of End Support / Bearing = 0.4 m
            //Concrete Grade = M25
            //Steel Grade = Fe 415
            //Permissible Stress σcb = 8.3 N/sq. mm.
            //Permissible Stress σst = 200 N/sq. mm

            //Modular Ratio m = 10

            //j = 0.9
            //Q = 1.1

            //Live Load Dimension a1 = 3.6 m
            //Live Load Dimension b1 = 0.85 m
            //Live Load Dimension b2 = 1.20 m

            //Total Live Load W1 = 700 KN

            //Clear cover to Reinforcement Bars = 30 mm’
            //Unit weight of Concrete = δc = 24 KN/cu.m.
            //Unit weight of Wearing course = δwc = 22 KN / cu.m.
            #endregion

            #region Page2
            //            Overall depth of Slab = 500 mm = D
            //Effective depth of Slab = 500 - (cover + 10) = 460 mn = d eff.

            //Effective span is lesser of

            //i)                Clear Span + Effective Depth = 6 + 0.46 = 6.46 m
            //ii)                Centre to Centre distance of End Supports / Bearings = 6 + 0.4 = 6.4 m

            //So, Effective Span = 6.4 m = l

            //STEP 2:

            //Bending Moment by Permanent Loads
            //Weight of Slab = (D /1000) * δc = 0.5 * 24 = 12 KN/sq.m.
            //Weight of Wearing Course = (WC /1000) * δwc = 0.080 * 22 = 1.76 KN/sq.m.
            //Total Load = 13.76 NK/sq.m. - 14 KN/sq.m. = W2
            //Bending Moment for Permanent Loads = M1 = (14 * l2) / 8 = (14 * 6.42) / 8 = 72 KN m.

            //STEP 3:

            //Bending Moment by Vehicle Load / Live Load

            //For 5m Span Impact Factor 25%
            //For 9m Span Impact Factor 10%

            //So, for 6.4m Span Impact factor 
            //= 25 - {(25 - 10) / (9 - 5)} * (6.4 - 5) = 25 - (15 / 4) * 1.4 = 19.7% = If
            //Length of Load = a1 = 3.6 m.
            //Length of Load including 45° dispersal = 3.6 + 2 * {(D + WC) / 1000}
            //                        = 3.6 + 2 * (0.58) = 4.76 m. = ld

            //Effective Width of Slab perpendicular to Span = be = Kx * {1 - (x / L)} + bw

            //Placing the Load symmetrically on the Span

            //x = Distance from centre of end support to centre of Load = l / 2 = 6.4 / 2 = 3.2 m.
            //B = Width of Slab = CW + (2 * FP) =  7.5 + 2 = 9.5 m
            //B / l = 9.5 / 6.4 = 1.48
            //bw = b1 + {2 * (WC / 1000)} = 0.85 + (2 * 0.08) = 1.01 m.

            //From Table of IRC 21:2000 (given at the end of this design)

            //For B / l = 1.48, for simply Supported Slab,  K = 2.84
            //So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.

            //Width of Load with 45 dispersal = 2 * (5.56 / 2) + 2 * (0.85 / 2) + 1.2
            //                     = {(2 * be) / 2} + {(2 * b1) / 2} + b2
            //                     = 7.61 m = Wd


            #endregion


            #region Page 3
            //            Total Live Load including Impact = W1 * (IF / 100) = 700 * 1.197 = 838 KN
            //Live Load per Unit area = 838 / (ld * wd) = 838 / (4.76 * 7.61) = 23.134 KN / sq.m.

            //Bending Moment for Live Load = M2 = {(23.14 * ld) / 2} * (l / 2) - {(23.14 * ld) / 2) * (ld / 4)
            //                      = {(23.14 * 4.76) / 2} * 3.2 - {(23.14 * 4.76) / 2) * 1.19
            //                      = 110.7 KN m.

            //Design Bending Moment = M = M1 + M2 = 72 + 110.7 = 182.7 = 185 KN m.

            //STEP 4:

            //Shear Force by Live Load
            //Effective Span = l = 6.4m.
            //Length of Load including 45° dispersal = ld = 4.76 m.
            //To get maximum Shear Force at support let us place the Load coinciding the start point of the above lengths

            //x = ld / 2 = 4.76 / 2 = 2.38 m.
            //B / l = 9.5 / 6.4 = 1.48

            //From IRC 21:2000, K = 2.84
            //bw = 1.01m.

            //Effective width of Load = be = Kx * {1 - (x / L)} + bw
            //                = 2.84 * 2.38 * {1 - (2.38 / 6.4)}+ 1.01
            //                = 5.256 m.

            //Width of Load with 45° dispersal = 2 * (5.256 / 2) + 2 * (0.85 / 2) + 1.2 = 7.3 M. = wd

            //Live Load per unit area = 838 / (ld * wd) = 838 / (4.76 * 7.3) = 24.1 KN / sq.m.

            //Shear Force by Live Load = [24.1 * 4.76 * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)] / l
            //                   = [24.1 * 4.76 * 2 * {0.85 + (2 * 0.08) + 1}] / 6.4
            //                   = 72 KN = V1

            //Dead Load Shear = (W2 * l) / 2 = (14 * 6.4) / 2 = 45 KN = V2

            //Total Design Shear = V = V1 + V2 = 72 + 45 = 117 KN

            //STEP 5:

            //Structural Design of Slab.
            //                      ________________
            //Required Effective Depth = d = √ ( M * 106) / (Q * b)
            //                      _____________________
            //                = √ ( 185 * 106) / (1.1 * 1000) = 410 mm

            #endregion

            #region Page 4
            //            Effective depth provided = d eff = 460 > d = 410, O.K.

            //Required Steel reinforcement = Ast = ( M * 106) / (σst * j * d)
            //                          = (185 * 106) / ( 200 * 0.9 * 460)
            //                          = 2234 Sq. mm.

            //Using 20 mm dia. Bars, spacing = [1000 * {(π * 20 * 20) / 4}] / 2234
            //                    = 140 mm

            //Provide T20 @ 140 mm c/c,

            //Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2
            //                        = 0.2 * 72 + 0.3 * 110
            //                        = 47.4
            //                        = 48 KN m.

            //Using 12 mm. diaBars, Effective Depth = deff - (10 + 6) = 444 mm

            //Required Steel = (48 * 106) / ( 200 * 0.9 * 444) = 600.6 Sq. mm.

            //Spacing of bars = (1000 * π * 12 * 12) / (600.6 * 4) = 188.3 mm

            //Provide T12 @ 150 mm c/c as distribution Steel.

            #endregion

            #region Page 5
            //STEP 6:    Shear Reinforcements
            //Design Shear = τ = V / bd eff = (117 * 103) / (1000 * 460) = 0.254 N / Sq. mm.
            //K1 = 1.14 - 0.7 * (d eff / 1000) = 1.14 - 0.7 * 0.460 = 0.82 >= 0.5, O.K.
            //Bending up alternate bars, provide T20 bars @ 280 mm c/c
            //Ast provided = 1122 Sq. mm.
            //Percentage p = (1122 * 100) / (1000 * 460) = 0.243%
            //K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * 0.243 = 0.560 < 1.0, Not O.K.
            //So, K2 = 1.0
            //Concrete Grade (M)                15                20                25                30                35                40
            // τ co                                                0.28                0.34                0.40                0.45                0.50                0.50
            //Therefore, Permissible Shear Stress
            //τ c = K1 * K2 * τ co
            //      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
            //So, O.K.
            //Else not O.K.. more Steel will be required.

            #endregion

        }
        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "SLAB_CULVERT_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));

            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";

            double _A, _B, _C, _D, _E;
            //double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
            //double _sp1, _sp2, _sp3, _sp4, _sp7;

            _A = L * 1000;
            _B = D;
            _C = WC;
            _D = CW * 1000;
            _E = support_width * 1000;

            //_bd1 = 0.0;
            //_bd2 = 0.0;
            _bd3 = 10;
            _bd4 = 10;
            _bd5 = 20;
            _bd6 = 20;
            _bd7 = 10;
            //_sp1 = 0.0;
            //_sp2 = 0.0;
            _sp3 = 300;
            _sp4 = 300;
            _sp7 = 300;
            #endregion

            try
            {

                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);

                sw.WriteLine("_bd1=T{0:f0}", _bd1);
                sw.WriteLine("_bd2=T{0:f0}", _bd2);
                sw.WriteLine("_bd3=T{0:f0}", _bd3);
                sw.WriteLine("_bd4=T{0:f0}", _bd4);
                sw.WriteLine("_bd5=T{0:f0}", _bd5);
                sw.WriteLine("_bd6=T{0:f0}", _bd6);
                sw.WriteLine("_bd7=T{0:f0}", _bd7);

                sw.WriteLine("_sp1={0:f0} c/c", _sp1);
                sw.WriteLine("_sp2={0:f0} c/c", _sp2);
                sw.WriteLine("_sp3={0:f0} c/c", _sp3);
                sw.WriteLine("_sp4={0:f0} c/c", _sp4);
                sw.WriteLine("_sp7={0:f0} c/c", _sp7);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

    }

}

//Chiranjit [2016 08 08] Add Project Name for Single cell box culvert
//Chiranjit [2016 08 08] Add Project Name for Slab culvert
