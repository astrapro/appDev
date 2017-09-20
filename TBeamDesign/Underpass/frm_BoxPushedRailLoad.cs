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


namespace BridgeAnalysisDesign.Underpass
{
    public partial class frm_BoxPushedRailLoad : Form
    {
        IApplication iApp;

        Box_PushedUnderpassRailLoad sc_box;
        public frm_BoxPushedRailLoad(IApplication iapp)
        {
            InitializeComponent();
            iApp = iapp;

            this.Text = Title + " : " + AstraInterface.DataStructure.MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            //iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);

            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


            if (!System.IO.Directory.Exists(iApp.LastDesignWorkingFolder))
                System.IO.Directory.CreateDirectory(iApp.LastDesignWorkingFolder);

            sc_box = new Box_PushedUnderpassRailLoad(iapp);
        }

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC BOX PUSHED UNDERPASS RAIL LOAD [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "DESIGN OF RCC BOX PUSHED UNDERPASS RAIL LOAD [LRFD]";

                return "DESIGN OF RCC BOX PUSHED UNDERPASS RAIL LOAD [IRC]";
            }
        }

        private void frm_BoxPushedRailLoad_Load(object sender, EventArgs e)
        {




            //pic_box.BackgroundImage = AstraFunctionOne.ImageCollection.Box_Culvert;

            cmb_box_fck.SelectedIndex = 2;
            cmb_box_fy.SelectedIndex = 1;



            Button_Enable_Disable();


            Set_Project_Name();


        }

        public void Set_Project_Name()
        {
            string dir = Project_Type.ToString();

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
            //sc_box.b1 = MyList.StringToDouble(txt_box_b1.Text, 0.0);
            //sc_box.b2 = MyList.StringToDouble(txt_box_b2.Text, 0.0);
            //sc_box.a1 = MyList.StringToDouble(txt_box_a1.Text, 0.0);
            //sc_box.w1 = MyList.StringToDouble(txt_box_w1.Text, 0.0);
            //sc_box.w2 = MyList.StringToDouble(txt_box_w2.Text, 0.0);
            //sc_box.b3 = MyList.StringToDouble(txt_box_b3.Text, 0.0);
            //sc_box.F = MyList.StringToDouble(txt_box_F.Text, 0.0);
            sc_box.S = MyList.StringToDouble(txt_box_S.Text, 0.0);
            sc_box.sbc = MyList.StringToDouble(txt_box_sbc.Text, 0.0);




            sc_box.fck = MyList.StringToDouble(cmb_box_fck.Text, 0.0);
            sc_box.fy = MyList.StringToDouble(cmb_box_fy.Text, 0.0);


            sc_box.UWB = MyList.StringToDouble(txt_box_UWB.Text, 0.0);
            sc_box.UWR = MyList.StringToDouble(txt_box_UWR.Text, 0.0);
            sc_box.UWW = MyList.StringToDouble(txt_box_UWW.Text, 0.0);
            sc_box.Phi = MyList.StringToDouble(txt_box_Phi.Text, 0.0);
            sc_box.Wf = MyList.StringToDouble(txt_box_Wf.Text, 0.0);
            sc_box.Wvl = MyList.StringToDouble(txt_box_Wvl.Text, 0.0);
            sc_box.Lvl = MyList.StringToDouble(txt_box_Lvl.Text, 0.0);
            sc_box.Dwc = MyList.StringToDouble(txt_box_Dwc.Text, 0.0);

            sc_box.Tno = MyList.StringToDouble(txt_box_Tno.Text, 0.0);
            sc_box.Rs = MyList.StringToDouble(txt_box_Rs.Text, 0.0);
            sc_box.ls = MyList.StringToDouble(txt_box_ls.Text, 0.0);
            sc_box.ws = MyList.StringToDouble(txt_box_ws.Text, 0.0);
            sc_box.ds = MyList.StringToDouble(txt_box_ds.Text, 0.0);
            sc_box.ss = MyList.StringToDouble(txt_box_ss.Text, 0.0);
            sc_box.Db = MyList.StringToDouble(txt_box_Db.Text, 0.0);
            sc_box.wbt = MyList.StringToDouble(txt_box_Wbt.Text, 0.0);
            sc_box.Hb = MyList.StringToDouble(txt_box_hb.Text, 0.0);
            sc_box.Hl = MyList.StringToDouble(txt_box_Hl.Text, 0.0);

        //public double fck, fy, UWB, UWR, UWW, Phi, Wf, Wvl, Lvl, Dwc;
        //public double Tno, Rs, ls, ws, ds, ss, Db, wbt, Hb, _V1, _H1, _V2, _H2, Hl;

            int grade = (int)sc_box.sigma_c;

            sc_box.Con_Grade = (CONCRETE_GRADE)grade;

            sc_box.sigma_st = MyList.StringToDouble(txt_box_sigma_st.Text, 0.0);
            sc_box.sigma_c = MyList.StringToDouble(txt_box_sigma_c.Text, 0.0);

            //sc_box.sigma_c = MyList.StringToDouble(cmb_box_fck.Text, 0.0);
            //sc_box.sigma_st = MyList.StringToDouble(cmb_box_fy.Text, 0.0);


            #endregion

        }
        public string user_path
        {
            get
            {

                string dir = "";

                dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                iApp.user_path = dir;

                //if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Single_Cell)
                //    dir = Path.Combine(dir, "Single Cell Box Culvert Design");
                //else if (Project_Type == eASTRADesignType.RCC_Box_Culvert_Multi_Cell)
                //    dir = Path.Combine(dir, "Multi Cell Box Culvert Design");
                //else if (Project_Type == eASTRADesignType.RCC_Slab_Culvert)
                //    dir = Path.Combine(dir, "Slab Culvert Design");
                //else if (Project_Type == eASTRADesignType.RCC_Pipe_Culvert)
                //    dir = Path.Combine(dir, "Pipe Culvert Design");

                dir = Path.Combine(dir, Project_Name);

                return dir;
            }
            set
            {
                Project_Name = Path.GetFileName(value);
                iApp.user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

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
        public void Box_Read_From_File()
        {
            Save_FormRecord.Read_All_Data(this, user_path);
        }
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
        }
        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == btn_browse_design.Text)
            {
                string tb_name = this.Name;

                frm_Open_Project frm = new frm_Open_Project(tb_name, Path.Combine(iApp.LastDesignWorkingFolder, Title));

                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    if (Project_Type == eASTRADesignType.RCC_Box_Pushed_Underpass)
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

                        iApp.Read_Form_Record(this, user_path);

                        Project_Name = Path.GetFileName(dest_path);
                        sc_box.Project_Name = Project_Name;
                        sc_box.FilePath = Path.GetDirectoryName(user_path);

                        btn_box_Process.Enabled = true;

                        Write_All_Data();
                    }
                }
            }
            else if (btn.Text == btn_new_design.Text)
            {
                //isCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
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
            sc_box.FilePath = Path.GetDirectoryName(user_path);
            Single_Cell_Box_Initialize_InputData();
            sc_box.WriteUserInput();
            sc_box.Calculate_Program(sc_box.rep_file_name);
            sc_box.Write_Drawing();
            Write_All_Data();

            MessageBox.Show(this, "Report file written in " + sc_box.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(sc_box.rep_file_name);
            sc_box.is_process = true;
            Button_Enable_Disable();
        }

        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_box_H.Text = "5.0";
                txt_box_b.Text = "3.0";
                txt_box_d.Text = "3.0";
            }
        }
        public void Create_Project()
        {
            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                   "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        MyList.Delete_Folder(user_path);
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

            iApp.Save_Form_Record(this, user_path);

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        void Button_Enable_Disable()
        {
            btn_box_Process.Enabled = true;
            btn_box_Report.Enabled = File.Exists(sc_box.rep_file_name);
            btn_dwg_box_culvert_single.Enabled = File.Exists(sc_box.drawing_path);
        }

        private void txt_Box_sigma_c_TextChanged(object sender, EventArgs e)
        {
            Single_Cell_Box_Initialize_InputData();

        }
        #endregion  Box Culvert

        public eASTRADesignType Project_Type { get { return eASTRADesignType.RCC_Box_Pushed_Underpass; } }

        private void btn_dwg_box_culvert_single_Click(object sender, EventArgs e)
        {
            if (File.Exists(sc_box.drawing_path))
                iApp.SetDrawingFile_Path(sc_box.drawing_path, "Box_Pushed_Underpass_Interactive", "Box_Culvert");
        }

        private void rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                pcb_diagrams.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.BPUIMG1;
            }
            else if (rbtn2.Checked)
            {
                pcb_diagrams.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.BPUIMG2;
            }
            else if (rbtn3.Checked)
            {
                pcb_diagrams.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.BPUIMG3;
            }
        }
    }


    public class Box_PushedUnderpassRailLoad
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




        public double fck, fy, UWB, UWR, UWW, Phi, Wf, Wvl, Lvl, Dwc;
        public double Tno, Rs, ls, ws, ds, ss, Db, wbt, Hb, _V1, _H1, _V2, _H2, Hl;




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
        public Box_PushedUnderpassRailLoad(IApplication app)
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

                rep_file_name = Path.Combine(file_path, "RCC_BOX_PUSHED_UNDERPASS_REPORT.TXT");
                user_input_file = Path.Combine(system_path, "BOX_CULVERT.FIL");
                drawing_path = Path.Combine(system_path, "BOX_CULVERT_DRAWING.FIL");

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
                sw.WriteLine("\t\t*            ASTRA Pro Release 20.0           *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*           RCC BOX PUSHED UNDERPASS          *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                if (false)
                {
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
                }


                sw.WriteLine("RAILWAY LOADING ");
                sw.WriteLine("");
                sw.WriteLine("DESIGN DATA:");
                sw.WriteLine("");
                sw.WriteLine("General Data:");
                sw.WriteLine("");
                sw.WriteLine("Depth of Earth Cushion between Bottom of Stone Ballast and Top edge of RCC Box = H = 1.01 m", H);
                sw.WriteLine("Inside clear width of RCC Box = b = {0} m", b);
                sw.WriteLine("Inside clear depth of RCC Box = d = {0} m ", d);
                sw.WriteLine("Thickness of Top Slab of RCC Box = d1 = {0} m", d1);
                sw.WriteLine("Thickness of Bottom Slab of RCC Box = d2 = {0} m", d2);
                sw.WriteLine("Thickness of Side Walls of RCC Box = d3 = {0} m", d3);

                gamma_c = gamma_c / 10;
                gamma_b = gamma_b / 10;
                sw.WriteLine("Unit Weight of Concrete = ɣ_c = {0} Tons/Cu.m", gamma_c);
                sw.WriteLine("Unit Weight of Soil = ɣ_b = {0} Tons/Cu.m", gamma_b);
                sw.WriteLine("R = {0}", R);
                sw.WriteLine("Top Bar Diameter = bar_dia_top = {0} mm.", bar_dia_top);
                sw.WriteLine("Side Bar Diameter = bar_dia_side = {0} mm.", bar_dia_side);
                sw.WriteLine("Bottom Bar Diameter = bar_dia_bottom = {0} mm.", bar_dia_bottom);
                sw.WriteLine("Clear Cover to Reinforcements = cover = {0} mm", cover);




                //sw.WriteLine("Grade of Concrete = fck = {0} N/Sq. mm = M40", fck);
                //sw.WriteLine("Grade of Steel = fy = {0} N/Sq. mm = Fe 415", fy);
                sw.WriteLine("");
                sw.WriteLine("Grade of Concrete = fck = {0} N/Sq. mm = M{0}", fck);
                sw.WriteLine("Grade of Steel = fy = {0} N/Sq. mm = Fe {0}", fy);
                sw.WriteLine("");





                //UWB = 1.92 ;
                //UWR = 60;
                //UWW = 1.0;
                //Phi = 30;
                //Wf = 0.5;
                //Wvl = 70;
                //Lvl = 3.6;
                //Dwc = 0.5;


                sw.WriteLine("Unit Weight of Stone Ballast = UWB = {0} Tons/Cu.m", UWB);
                sw.WriteLine("Unit Weight of Rail = UWR = {0} Kg/m", UWR);
                sw.WriteLine("Unit Weight of Pavement = UWW = {0} Tons/Cu.m", UWW);
                sw.WriteLine("Angle of Internal Friction of Soil = Phi = {0} Degrees", Phi);
                sw.WriteLine("Weight of Footpath = Wf = {0} Tons/Sq.m", Wf);
                sw.WriteLine("Vehicle Load on Bottom Slab = Wvl = {0} Tons", Wvl);
                sw.WriteLine("Length of Vehicle Load = Lvl = {0} m", Lvl);
                sw.WriteLine("Thickness of Pavement = Dwc = {0} m", Dwc);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Rail Track Data for each Rail Track:");
                sw.WriteLine("------------------------------------");
                sw.WriteLine("");




                //Tno = 2;
                //Rs = 5.300;
                //ls = 2.75;
                //ws = 0.230;
                //ds = 0.300;
                //ss = 0.66;
                //Db = 0.400;
                //wbt = 0.1524;
                //Hb = 1;
                //Hl = 2;

                _V1 = 1;
                _H1 = 1;

                _V2 = 1;
                _H2 = 2;

                sw.WriteLine("Number of Rail Tracks = Tno = {0}", Tno);
                sw.WriteLine("Centre to Centre distance between the Rail Tracks = Rs = {0} m", Rs);
                sw.WriteLine("Length of Concrete Sleeper = ls = {0} m", ls);
                sw.WriteLine("Width of Concrete Sleeper = ws = {0} m", ws);
                sw.WriteLine("Depth of Concrete Sleeper = ds = {0} m", ds);
                sw.WriteLine("Spacing of Sleepers = ss = {0} m", ss);
                sw.WriteLine("Depth of Stone Ballast below Sleeper = Db ={0} m", Db);
                sw.WriteLine("Either side width of Stone Ballast by the side of sleeper = wbt = {0} m", wbt);
                sw.WriteLine("Either side slope of Stone Ballast = Hb = {0}    (V : H = 1 : {0})   ", Hb);
                sw.WriteLine("Either side slope for Load Dispersion from bottom of Stone Ballast = Hl = {0}    (V : H = 1 : {0}) ", Hl);
                sw.WriteLine("");
                sw.WriteLine("");



                double p1, p2, p3, p4, p5, p6;







                #region STEP 1
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                if (false)
                {
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

                     p1 = earth_cusion + self_weight_top_slab;

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

                     p2 = w1 / loaded_area_dispersion;
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

                     p3 = (load_for_two_tracks * F) / (loaded_area_dispersion_2);

                    sw.WriteLine();
                    sw.WriteLine("    Load per Unit Area for Two Tracks = p3 ");
                    sw.WriteLine("                                      = {0:f2} * F / {1:f2}",
                                                        load_for_two_tracks,
                                                        loaded_area_dispersion_2);
                    sw.WriteLine("                                      = {0:f2} kN/sq.m", p3);

                     p4 = p1 + p3;

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

                     p5 = p1 + loads_walls;
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
                     p6 = p5 + p3;
                    sw.WriteLine();
                    sw.WriteLine("  Total Load Per Unit Area = p6");
                    sw.WriteLine("                           = {0:f3} + {1:f3}", p5, p3);
                    sw.WriteLine("                           = {0:f3} kN/sq.m", p6);

                    sw.WriteLine();

                    #endregion

                }

                #region Rail Load

                //sw.WriteLine("");
                //sw.WriteLine("DESIGN CALCULATIONS:");
                //sw.WriteLine("-----------------------");
                sw.WriteLine("");
                sw.WriteLine("STEP 1.1 : DEAD LOAD AND SUPER IMPOSED DEAD LOAD :");
                sw.WriteLine("----------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Load by Rail");
                sw.WriteLine("----------------------------------------");

                 
                double dsb = ds + Db;
                sw.WriteLine("Total Depth of Stone Ballast = dsb = ds + db = {0:f3} + {1:f3} = {2:f3} m", ds, Db, dsb);

                double wsb = (dsb * Hb) + wbt + ls + wbt + (dsb * Hb);
                sw.WriteLine("Bottom width of Stone Ballast = wsb ");
                sw.WriteLine("                              = (dsb x Hb) + wbt + ls + wbt + (dsb x Hb)");
                sw.WriteLine("                              = ({0:f3} x {1:f3}) + {2:f3} + {3:f3} + {2:f3} + ({0:f3} x {1:f3})", dsb, Hb, wbt, ls);
                sw.WriteLine("                              = {0:f3} m.", wsb);
                sw.WriteLine("");
                sw.WriteLine("Weight of one Rail = UWR = {0:f3} kg/m", UWR);

                double Wr1 = Tno * UWR;


                sw.WriteLine("Weight of one Rail Track = Wr1 = 2 x UWR = {0} x {1:f3} = {2:f3} kg/m = {3:f3} Ton/m", Tno, UWR, Wr1, Wr1 / 1000);

                double Wr2 = Tno * Wr1;

                sw.WriteLine("Weight of all Rail Tracks = Wr2 = Tno x Wr1 = {0} x {1:f3} = {2:f3} kg/m = {3:f3} Ton/m", Tno, Wr1, Wr2, Wr2 / 1000);
                sw.WriteLine("");

                Wr1 = Wr1 / 1000;
                Wr2 = Wr2 / 1000;

                sw.WriteLine("");
                sw.WriteLine("Load by Sleeper");
                sw.WriteLine("----------------");
                sw.WriteLine("");
                sw.WriteLine("");


                double vols = ls * ws * ds;


                sw.WriteLine("Volume of a concrete sleeper = vols = ls x ws x ds = {0:f3} x {1:f3} x {2:f3} = {3:f4} Cu.m", ls, ws, ds, vols);


                double wts = vols * gamma_c;
                sw.WriteLine("Weight of a concrete sleeper = wts = vols x ɣ_c = {0:f4} x {1} = {2:f3} Tons", vols, gamma_c, wts);


                double Ws1 = wts / ss;

                sw.WriteLine("Weight of concrete sleepers per metre length of one track = Ws1 = wts/ss = {0:f4}/{1:f3} = {2:f4} Tons/m", wts, ss, Ws1);
                sw.WriteLine("");
                sw.WriteLine("");

                double Ws2 = Tno * Ws1;


                sw.WriteLine("Weight of concrete sleepers per metre length of all tracks = Ws2 = Tno x Ws1 = {0} x {1:f4} = {2:f4} Tons/m", Tno, Ws1, Ws2);
                sw.WriteLine("");

                double Wrs = Wr2 + Ws2;

                sw.WriteLine("Combined weight of Rail and Sleeper = Wrs = Wr2 + Ws2 = {0:f4} + {1:f4} = {2:f4} Tons/m", Wr2, Ws2, Wrs);

                double Fact_Hl = 1 / Hl;

                sw.WriteLine("Load Dispersion slope = Hl = {0}, Fact_Hl = 1/Hl = {1:f2}", Hl, Fact_Hl);
                sw.WriteLine("");



                double De = H;
                double Disw1 = ls + (2 * Fact_Hl * De) + (2 * Fact_Hl * Db);
                sw.WriteLine("Dispersed width of one Track = Disw1 = ls + (2 x Fact_Hl x De) + (2 x Fact_Hl x Db)");
                sw.WriteLine("                                     = {0:f2} + (2 x {1:f2} x {2:f3}) + (2 x {1:f2} x {3:f3}) = {4:f3} m", ls, Fact_Hl, De, Db, Disw1);
                sw.WriteLine("");


                double Disw2 = Tno * Disw1;
                sw.WriteLine("Dispersed width of all Tracks = Disw2 = Tno x Disw1 = {0} x {1:f3} = {2:f3} m.", Tno, Disw1, Disw2);

                double Prs = Wrs / Disw2;
                sw.WriteLine("Combined pressure by Rail and Sleeper = Prs = Wrs / Disw2 = {0:f2} / {1:f2} = {2:f3} Ton/Sq.m", Wrs, Disw2, Prs);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Load by Ballast :");
                sw.WriteLine("------------------------------");
                sw.WriteLine("");

                double thb = ds + Db;
                sw.WriteLine("Thickness of Stone Ballast for one track = thb = ds + Db = {0:f2} + {1:f2} = {2:f2} m", ds, Db, thb);
                sw.WriteLine("");
                sw.WriteLine("Either side Slope of Stone Ballast = 1:Hb = 1:{0}", Hb);
                sw.WriteLine("");

                double Wbbot = ls + thb * Hb + thb * Hb + wbt + wbt;


                sw.WriteLine("Width of Ballast at the bottom for one track = Wbbot = ls + thb x Hb + thb x Hb + wbt + wbt");
                sw.WriteLine("                                                     = {0:f2} + {1:f2} x {2} + {1:f2} x {2} + {3:f3} + {3:f2}", ls, thb, Hb, wbt);
                sw.WriteLine("                                                     = {0:f3} m", Wbbot);
                sw.WriteLine("");


                double Wbtop = ls + wbt + wbt;
                sw.WriteLine("Width of Ballast at the top for one track = Wbtop = ls + wbt + wbt");
                sw.WriteLine("                                                  = {0:f3} + {1:f4} + {1:f4}", ls, wbt);
                sw.WriteLine("                                                  = {0:f3}", Wbtop);
                sw.WriteLine("");


                double Ab = (Wbtop + Wbbot) * thb / 2;
                sw.WriteLine("Trapezoidal cross sectional area of Stone Ballast for one track = Ab ");
                sw.WriteLine("                                                                = (Wbtop + Wbbot) x thb / 2");
                sw.WriteLine("                                                                = ({0:f3} + {1:f3}) x {2:f3} / 2", Wbtop, Wbbot, thb);
                sw.WriteLine("                                                                = {0:f3} Sq.m", Ab);
                sw.WriteLine("");

                double volb1 = Ab * 1.0;
                sw.WriteLine("Volume of Stone Ballast including Sleepers per metre for one track = volb1 ");
                sw.WriteLine("                                                                   = Ab x 1.0 ");
                sw.WriteLine("                                                                   = {0:f3} x 1.0 = {1:f3} Cu.m", Ab, volb1);
                sw.WriteLine("");


                double volb2 = volb1 - (vols / ss);
                sw.WriteLine("Volume of Stone Ballast excluding Sleepers per metre for one track = volb2 ");
                sw.WriteLine("                                                                   = volb1 - (vols / ss)");
                sw.WriteLine("                                                                   = {0:f3} - ({1:f3} / {2:f3})", volb1, vols, ss);
                sw.WriteLine("                                                                   = {0:f3} Cu.m", volb2);
                sw.WriteLine("");
                double Volb3 = Tno * volb2;
                sw.WriteLine("Volume of Stone Ballast excluding Sleepers per metre for all tracks = Volb3 = Tno x volb2 ");
                sw.WriteLine("                                                                            = {0} x {1:f3} = {2:f3} Cu.m", Tno, volb2, Volb3);
                sw.WriteLine("");

                double Wsb = Volb3 * UWB;
                sw.WriteLine("Weight of Stone Ballast = Wsb = Volb3 x UWB = {0:f3} x {1:f3} = {2:f3} Tons", Volb3, UWB, Wsb);
                Fact_Hl = 1 / Hl;
                sw.WriteLine("");
                sw.WriteLine("Load Dispersion slope = Hl = {0}, Fact_Hl = 1/Hl = {1:f2}", Hl, Fact_Hl);


                sw.WriteLine("");
                sw.WriteLine("Thickness of Earth Cushion = H = {0} m", H);
                sw.WriteLine("");

                Disw1 = Wbbot + (2 * Fact_Hl * H);
                sw.WriteLine("Dispersed width of one Track = Disw1 = Wbbot + (2 x Fact_Hl x H) ");
                sw.WriteLine("                                     = {0:f3} + (2 x {1:f2} x {2:f3})", Wbbot, Fact_Hl, H);
                sw.WriteLine("                                     = {0:f3} m.", Disw1);
                sw.WriteLine("");


                sw.WriteLine("Spacing between Tracks = Rs = {0} m.", Rs);
                sw.WriteLine("");

                Disw2 = (Tno * Disw1 / 2.0) + Rs;
                sw.WriteLine("Dispersed width of all Tracks = Disw2 = (Tno x Disw1 / 2.0) + Rs");
                sw.WriteLine("                                      = ({0} x {1:f3} / 2.0) + {2:f3}", Tno, Disw1, Rs);
                sw.WriteLine("                                      = {0:f3} m.", Disw2);
                sw.WriteLine("");

                double Psb = Wsb / Disw2;
                sw.WriteLine("Pressure by Stone Ballast = Psb = Wsb / Disw2 = {0:f3}  / ({1:f3} x 1.0) = {2:f3} Ton/Sq.m", Wsb, Disw2, Psb);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Pressure by Dead Load and Superimposed Dead Load (SIDL):");
                sw.WriteLine("-----------------------------------------------------------");
                sw.WriteLine("");

                double Apsidl = Prs + Psb;
                sw.WriteLine("Total Applied Superimposed Pressure = Apsidl = Prs + Psb");
                sw.WriteLine("                                             = {0:f3} + {1:f3} = {2:f3} Tons/Sq.m", Prs, Psb, Apsidl);
                sw.WriteLine("");

                double Spsidl1 = 6.75;
                sw.WriteLine("Specified Superimposed Load by one track = Spsidl1 = {0:f3} Tons/m", Spsidl1);

                double Spsidl2 = Tno * Spsidl1;
                sw.WriteLine("Specified Superimposed Load by all tracks = Spsidl2 = Tno x Spsidl1 = {0} x {1:f3} = {2:f3} Tons/m", Tno, Spsidl1, Spsidl2);

                double Spsidl3 = Spsidl2 / Disw2;
                sw.WriteLine("Specified Superimposed Pressure by all tracks = Spsidl3 = Spsidl2 / Disw2 = {0:f3} / {1:f3} = {2:f3} Tons/Sq.m", Spsidl2, Disw2, Spsidl3);
                sw.WriteLine("");
                if (Apsidl < Spsidl3)
                {
                    sw.WriteLine("As, Apsidl < Spsidl3");
                    sw.WriteLine("So, Total Applied Superimposed Pressure < Specified Superimposed Pressure, Hence ‘OK’.");
                } 
                else 
                {
                    sw.WriteLine("As, Apsidl > Spsidl3");
                    sw.WriteLine("So, Total Applied Superimposed Pressure > Specified Superimposed Pressure, Hence NOT OK.");
                }


                double W1_sidl = Math.Max(Apsidl, Spsidl3);


                sw.WriteLine("");
                sw.WriteLine("SIDL = Super Imposed Dead Load =  W1_sidl = greater value of (Apsidl and Spsidl3) = {0:f3} Tons/Sq.m", W1_sidl);
                sw.WriteLine("");

                double W2_earth = H * gamma_b;

                sw.WriteLine("Load by Earth Fill = W2_earth = H x ɣ_b = {0} x {1} = {2:f3} Tons/Sq.m", H, gamma_b, W2_earth);
                sw.WriteLine("");

                double W3_topslab = d1 * gamma_c;
                sw.WriteLine("Load by Top Slab = W3_topslab = d1 x ɣ_c = {0} x {1} = {2:f3} Tons/Sq.m", d1, gamma_c, W3_topslab);
                sw.WriteLine("");


                double W4_walls = ((d + d1) * 2 * d3 * gamma_c) / ((b + d3) * 1.0);



                sw.WriteLine("Load by Vertical Walls = W4_walls = [(d + d1) x 2 x d3 x ɣ_c] / [(b+d3) x 1.0]");
                sw.WriteLine("                                  = [({0:f3} + {1:f3}) x 2 x {2:f2} x {3}] / [({4:f2}+{2:f2}) x 1.0] ", d, d1, d3, gamma_c, b);
                //sw.WriteLine("                                  = (6.05 x 2 x 0.9 x 2.5) / (11.4 x 1.0)");
                sw.WriteLine("                                  = {0:f3} Tons/Sq.m", W4_walls);
                sw.WriteLine("");

                double W5_wc = b * Dwc * UWW / ((b + d3) * 1.0);
                sw.WriteLine("Load by Wearing Course = W5_wc = b x Dwc x UWW / [(b+d3) x 1.0]");
                sw.WriteLine("                               = [{0:f2} x {1:f2} x {2:f2}] / [({3:f2} + {4:f3}) x 1.0]", b, Dwc, UWW, b, d3);
                sw.WriteLine("                               = {0:f2} Tons/Sq.m", W5_wc);
                sw.WriteLine("");

                double W6_fp = Wf;
                sw.WriteLine("Load by Footpath = W6_fp = Wf = {0:f2} Tons/Sq.m", Wf);
                sw.WriteLine("");
                sw.WriteLine("");

                double DLtop = W1_sidl + W2_earth + W3_topslab;
                sw.WriteLine("Dead Load on Top Slab = DLtop = W1_sidl + W2_earth + W3_topslab ");
                sw.WriteLine("                              = {0:f3} + {1:f3} + {2:f3} ", W1_sidl, W2_earth, W3_topslab);
                sw.WriteLine("                              = {0:f3} Tons/Sq.m", DLtop);
                sw.WriteLine("");
                sw.WriteLine("");

                double DLbot = W1_sidl + W2_earth + W3_topslab + W4_walls + W5_wc + W6_fp;
                sw.WriteLine("Dead Load on Bottom Slab = DLbot = W1_sidl + W2_earth + W3_topslab + W4_walls + W5_wc + W6_fp");
                sw.WriteLine("                                 = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}", W1_sidl, W2_earth, W3_topslab, W4_walls, W5_wc, W6_fp);
                sw.WriteLine("                                 = {0:f3} Tons/Sq.m", DLbot);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("STEP 1.2 : LIVE LOAD :");
                sw.WriteLine("----------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("Inside clear width of RCC Box = b = {0} m", b);
                sw.WriteLine("Thickness of each side wall = d3 = {0} m", d3);
                sw.WriteLine("");

                double Lbm = b + 2 * (d3/2);
                sw.WriteLine("Effective span of RCC Box for Bending Moment = Lbm = b + 2 x (d3/2) = {0} + 2 x ({1}/2) = {2:f2} m.", b, d3, Lbm);
                sw.WriteLine("");

                double Lsf = b + 2 * d3;
                sw.WriteLine("Effective span of RCC Box for Shear Force = Lsf = b + 2 x d3 = {0} + 2 x {1} = {2:f2} m.", b, d3, Lsf);
                sw.WriteLine("");



                //double P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1;
                //double P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2;


                //P1_BM_kN = 1282.66;
                //P1_BM_Ton = 130.75;
                //P1_SF_kN = 1492.89;
                //P1_SF_Ton = 152.18;
                //CDA1 = 0.621;

                //P2_BM_kN = 1377.32;
                //P2_BM_Ton = 140.40;
                //P2_SF_kN = 1589.22;
                //P2_SF_Ton = 162.00;
                //CDA2 = 0.594;


                sw.WriteLine("Reading from Table1, for effective span, ");
                //sw.WriteLine("");
                //sw.WriteLine("For Bending Moment,");
                //sw.WriteLine("");
                //sw.WriteLine("L1=11.0           P1(BM)kN =1282.66        P1(BM)Ton=130.75   P1(SF)kN=1492.89        P1(SF)Ton=152.18    CDA1=0.621");
                //sw.WriteLine("L2=12.0           P2(BM)kN =1377.32        P2(BM)Ton =140.40  P2(SF)kN =1589.22        P2(SF)Ton =162.00   CDA2=0.594");
                //sw.WriteLine("");
                //sw.WriteLine("From above values we take,");
                //sw.WriteLine("P1 = 130.75 Ton and P2 = 140.40 Ton for BM, for L1 = 11.0m and L2=12.0 effective spans,");
                //sw.WriteLine("Therefore for 11.4 m effective span Live Load for Maximum BM ");
                //sw.WriteLine("P_bm = 130.75 + (140.40-130.75) x (11.4-11.0)/(12.0-11.0)");
                //sw.WriteLine("= 134.61 Tons");
                //sw.WriteLine("");


                //double L1 = 11.0;
                //double L2 = 12.0;
                //sw.WriteLine("");
                //sw.WriteLine("For Bending Moment,");
                //sw.WriteLine("");
                //sw.WriteLine("L1={0:f3}           P1(BM)kN ={1:f3}        P1(BM)Ton ={2:f3}   P1(SF)kN ={3:f3}        P1(SF)Ton ={4:f3}    CDA1={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1);
                //sw.WriteLine("L2={0:f3}           P2(BM)kN ={1:f3}        P2(BM)Ton ={2:f3}   P2(SF)kN ={3:f3}        P2(SF)Ton ={4:f3}    CDA2={5:f3}", L2, P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2);
                //sw.WriteLine("");
                //sw.WriteLine("From above values we take,");


                //sw.WriteLine("P1 = {0:f3} Ton and P2 = {1:f3} Ton for BM, for L1 = {2:f3} m and L2={3:f3} effective spans,", P1_BM_Ton, P2_BM_Ton, L1, L2);


                //sw.WriteLine("Therefore for {0:f3} m effective span Live Load for Maximum BM ", Lbm);


                ////double P_bm = P1_BM_Ton + (P2_BM_Ton - P1_BM_Ton) * (Lbm - L1) / (L2 - L1);
                List<string> res = new List<string>();


                double P_bm = iApp.Tables.Broad_Gauge_BendingMoment(Lbm, ref res);


                
                //P_bm = iApp.Tables.Broad_Gauge_BendingMoment(11.4, ref res);

                //res.Clear();

                // P_bm = iApp.Tables.Broad_Gauge_BendingMoment(12.0, ref res);



                // res.Clear();
                // P_bm = iApp.Tables.Broad_Gauge_ShearForce(12.3, ref res);




                //sw.WriteLine("P_bm = {0:f3} + ({1:f3}-{0:f3}) x ({2:f3}-{3:f3})/({4:f3}-{3:f3})", P1_BM_Ton, P2_BM_Ton, Lbm, L1, L2)
                //sw.WriteLine("     = {0:f3} Tons", P_bm);

                //sw.WriteLine("P_bm = 130.75 + (140.40-130.75) x (11.4-11.0)/(12.0-11.0)");
                //sw.WriteLine("= 134.61 Tons");
                sw.WriteLine("");






                //sw.WriteLine(" For Shear Force,");
                //sw.WriteLine("L1=12.0           P1(BM)kN =1377.32        P1(BM)Ton =140.40  P1(SF)kN =1589.22        P1(SF)Ton =162.00   CDA1=0.594");
                //sw.WriteLine("L2=13.0           P2(BM)kN =1475.13        P2(BM)Ton =150.37  P2(SF)kN =1670.74        P2(SF)Ton =170.31   CDA2=0.571");
                //sw.WriteLine("");
                //sw.WriteLine("From above values we take,");
                //sw.WriteLine("P1 = 162.00 Ton and P2 = 170.31 Ton for BM, for L1 = 12.0m and L2=13.0 effective spans,");
                //sw.WriteLine("Therefore for 12.3 m effective span Live Load for Maximum SF ");
                //sw.WriteLine("P_sf=162.00 + (170.31-162.00) x (12.3-12.0)/(13.0-12.0)");
                //sw.WriteLine("= 164.493 Tons");

                
                //L1 = 12.0;
                //L2 = 13.0;
                //sw.WriteLine("");
                //sw.WriteLine("L1={0:f3}           P1(BM)kN ={1:f3}        P1(BM)Ton ={2:f3}   P1(SF)kN ={3:f3}        P1(SF)Ton ={4:f3}    CDA1={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1);
                //sw.WriteLine("L2={0:f3}           P2(BM)kN ={1:f3}        P2(BM)Ton ={2:f3}   P2(SF)kN ={3:f3}        P2(SF)Ton ={4:f3}    CDA2={5:f3}", L2, P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2);
                //sw.WriteLine("");
                //sw.WriteLine("From above values we take,");


                //sw.WriteLine("P1 = {0:f3} Ton and P2 = {1:f3} Ton for BM, for L1 = {2:f3} m and L2={3:f3} effective spans,", P1_BM_Ton, P2_BM_Ton, L1, L2);


                //sw.WriteLine("Therefore for {0:f3} m effective span Live Load for Maximum BM ", Lsf);


                //double P_sf = P1_SF_Ton + (P2_SF_Ton - P1_SF_Ton) * (Lsf - L1) / (L2 - L1);
                double P_sf = iApp.Tables.Broad_Gauge_ShearForce(Lsf, ref res);


                foreach (var item in res)
                {
                    sw.WriteLine(item);
                }

                //sw.WriteLine("P_sf = {0:f3} + ({1:f3}-{0:f3}) x ({2:f3}-{3:f3})/({4:f3}-{3:f3})", P1_SF_Ton, P2_SF_Ton, Lsf, L1, L2);
                //sw.WriteLine("     = {0:f3} Tons", P_sf);

                //sw.WriteLine("P_bm = 130.75 + (140.40-130.75) x (11.4-11.0)/(12.0-11.0)");
                //sw.WriteLine("= 134.61 Tons");
                sw.WriteLine("");



                sw.WriteLine("");
                sw.WriteLine("Train Dynamics");
                sw.WriteLine("-----------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("The Cooefficient of Dynamic Augment (CDA) may be either calculated for effective span by interpolating ");
                sw.WriteLine("from values obtained above for Bending Moment and Shear Force from Table 1, or may be calculated from formula,");
                sw.WriteLine("");


                 
                sw.WriteLine("CDA = 0.15 + 8 / (6+L) subject to maximum value of 1.0,");
                sw.WriteLine("");



                sw.WriteLine("CDA for Bending Moment, L = {0:f3} m,", Lbm);

                double CDA_bm = 0.15 + 8 / (6 + Lbm);
                sw.WriteLine("CDA_bm = 0.15 + 8 / (6+L) = 0.15 + 8 / (6 + {0:f3}) = {1:f3}", Lbm, CDA_bm);
                sw.WriteLine("");


                sw.WriteLine("CDA for Shear Force, L = {0:f3} m,", Lsf);

                double CDA_sf = 0.15 + 8 / (6 + Lsf);
                sw.WriteLine("CDA_sf = 0.15 + 8 / (6+L) = 0.15 + 8 / (6 + {0:f3}) = {1:f3}", Lsf, CDA_sf);
                sw.WriteLine("");

                double _d = H + Db;
                sw.WriteLine("Depth of Fill = d = H + Db = {0:f3} + {1:f3} = {2:f3} m.", H, Db, _d);
                sw.WriteLine("");


                sw.WriteLine("Conditions:");
                sw.WriteLine("(1)        If the depth of Fill is less than 0.9 m, CDA is to be modified by, CDA = [2 - (d/0.9)] x 0.5 x CDA");
                sw.WriteLine("(2)        If the depth of Fill equals to 0.9 m, CDA is to be modified by, CDA = CDA x 0.5");
                sw.WriteLine("Subject to maximum value of 0.5");
                sw.WriteLine("(3)        If the depth of Fill is more than 0.9 m, CDA is to be modified by, CDA = CDA x 0.5");
                sw.WriteLine("Subject to maximum value of 0.5 and then reducing to 0.0 in 3.0 m,");
                sw.WriteLine("");

                double CDAm = 0.0;
                double LL = 407.94264;

                double _CDA_sf = 0.0;
                double _CDA_bm = 0.0;


                if (_d < 0.9)
                {
                    sw.WriteLine("");
                    sw.WriteLine("Considering condition (1),");
                    sw.WriteLine("");


                    sw.WriteLine("Modified CDA for bending Moment, at a depth of d = {0:f3} m,", _d);

                    //sw.WriteLine("CDAm = CDA_bm x 0.5 = 0.61 x 0.5 = 0.305");

                    //CDAm = CDA_bm * 0.5;

                    CDAm = (2 - (_d / 0.9)) * 0.5 * CDA_bm;


                    sw.WriteLine("CDA_bm = [2 - (d/0.9)] x 0.5 x CDA_bm = [2 - ({0:f3}/0.9)] x 0.5 x {1:f3} = {2:f3}", _d, CDA_bm, CDAm);

                    _CDA_bm = CDAm;


                    //sw.WriteLine("i.e, CDA_bm = {0:f3} x {1:f3} / 3.0 = {2:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_bm);


                    sw.WriteLine("");
                    //sw.WriteLine("Modified CDA for shear force, at a depth of d = 1.41 m,");
                    sw.WriteLine("Modified CDA for shear force, at a depth of d = {0:f3} m,", _d);

                    CDAm = (2 - (_d / 0.9)) * 0.5 * CDA_sf;

                    sw.WriteLine("CDA_sf = [2 - (d/0.9)] x 0.5 x CDA_sf = [2 - ({0:f3}/0.9)] x 0.5 x {1:f3} = {2:f3}", _d, CDA_sf, CDAm);
                    sw.WriteLine("");
                    //sw.WriteLine("CDA_sf/(0.9+3.0-d) = CDAm/3.0");
                    //sw.WriteLine("i.e, CDA_sf/(0.9+3.0-{0:f3}) = {1:f3}/3.0", _d, CDAm);
                    ////sw.WriteLine("i.e, CDA_sf = 0.295 x 2.49 / 3.0 = 0.24");
                    _CDA_sf = CDAm;
                    //sw.WriteLine("i.e, CDA_sf = {0:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_sf);
                    sw.WriteLine("");
                }
                else if (_d == 0.9)
                {
                    sw.WriteLine("");
                    sw.WriteLine("Considering condition (2),");
                    sw.WriteLine("");


                    sw.WriteLine("Modified CDA for bending Moment, at a depth of d = {0:f3} m,", _d);

                    //sw.WriteLine("CDAm = CDA_bm x 0.5 = 0.61 x 0.5 = 0.305");

                    CDAm = CDA_bm * 0.5;
                    sw.WriteLine("CDA_bm = CDA_bm x 0.5 = {0:f3} x 0.5 = {1:f3}", CDA_bm, CDAm);

                    _CDA_bm = CDAm;


                    //sw.WriteLine("i.e, CDA_bm = {0:f3} x {1:f3} / 3.0 = {2:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_bm);


                    sw.WriteLine("");
                    //sw.WriteLine("Modified CDA for shear force, at a depth of d = 1.41 m,");
                    sw.WriteLine("Modified CDA for shear force, at a depth of d = {0:f3} m,", _d);

                    CDAm = CDA_sf * 0.5;

                    sw.WriteLine("CDA_sf = CDA_sf x 0.5 = {0:f3} x 0.5 = {1:f3}", CDA_sf, CDAm);
                    sw.WriteLine("");
                    //sw.WriteLine("CDA_sf/(0.9+3.0-d) = CDAm/3.0");
                    //sw.WriteLine("i.e, CDA_sf/(0.9+3.0-{0:f3}) = {1:f3}/3.0", _d, CDAm);
                    ////sw.WriteLine("i.e, CDA_sf = 0.295 x 2.49 / 3.0 = 0.24");
                    _CDA_sf = CDAm;

                    //sw.WriteLine("i.e, CDA_sf = {0:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_sf);
                    sw.WriteLine("");

                }
                else
                {
                    sw.WriteLine("");
                    sw.WriteLine("Considering condition (3),");
                    sw.WriteLine("");


                    sw.WriteLine("Modified CDA for bending Moment, at a depth of d = {0:f3} m,", _d);

                    //sw.WriteLine("CDAm = CDA_bm x 0.5 = 0.61 x 0.5 = 0.305");

                    CDAm = CDA_bm * 0.5;
                    sw.WriteLine("CDAm = CDA_bm x 0.5 = {0:f3} x 0.5 = {1:f3}", CDA_bm, CDAm);



                    sw.WriteLine("");
                    sw.WriteLine("CDA_bm/(0.9+3.0-d) = CDAm/3.0");
                    //sw.WriteLine("i.e, CDA_bm /(0.9+3.0-1.41) = 0.305/3.0");
                    //sw.WriteLine("i.e, CDA_bm = 0.305 x 2.49 / 3.0 = 0.25");


                    sw.WriteLine("i.e, CDA_bm /(0.9+3.0-{0:f3}) = {1:f3}/3.0", _d, CDAm);

                     _CDA_bm = CDAm * (0.9 + 3.0 - _d) / 3.0;


                    sw.WriteLine("i.e, CDA_bm = {0:f3} x {1:f3} / 3.0 = {2:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_bm);


                    sw.WriteLine("");
                    //sw.WriteLine("Modified CDA for shear force, at a depth of d = 1.41 m,");
                    sw.WriteLine("Modified CDA for shear force, at a depth of d = {0:f3} m,", _d);

                    CDAm = CDA_sf * 0.5;

                    sw.WriteLine("CDAm = CDA_sf x 0.5 = {0:f3} x 0.5 = {1:f3}", CDA_sf, CDAm);
                    sw.WriteLine("");
                    sw.WriteLine("CDA_sf/(0.9+3.0-d) = CDAm/3.0");
                    sw.WriteLine("i.e, CDA_sf/(0.9+3.0-{0:f3}) = {1:f3}/3.0", _d, CDAm);
                    //sw.WriteLine("i.e, CDA_sf = 0.295 x 2.49 / 3.0 = 0.24");
                     _CDA_sf = CDAm * (0.9 + 3.0 - _d) / 3.0;

                    sw.WriteLine("i.e, CDA_sf = {0:f3} x {1:f3} / 3.0 = {2:f3}", CDAm, (0.9 + 3.0 - _d), _CDA_sf);
                    sw.WriteLine("");

                }




                double LL_bm = (1 + _CDA_bm) * P_bm;
                sw.WriteLine("Live Load for BM for one track = LL_bm = (1+CDA_bm) x P_bm = (1+{0:f3}) x {1:f3} = {2:f3} Tons", _CDA_bm, P_bm, LL_bm);

                double LL_sf = (1 + _CDA_sf) * P_sf;
                sw.WriteLine("Live Load for SF for one track = LL_sf = (1+CDA_sf) x P_sf = (1+{0:f3}) x {1:f3} = {2:f3} Tons", _CDA_sf, P_sf, LL_sf);
                sw.WriteLine("");

                //sw.WriteLine("Live Load for BM for one track = LL_bm = (1+CDA_bm) x P_bm = (1+0.25) x 134.61 = 168.2625 Tons");
                //sw.WriteLine("Live Load for SF for one track = LL_sf = (1+CDA_sf) x P_sf = (1+0.24) x 164.493 = 203.97132 Tons");
                //sw.WriteLine("");

                //sw.WriteLine("Live Load for BM for all tracks = LL-bm = LL-bm x Tno = 168.2625 x 2 = 336.525 Tons");
                //sw.WriteLine("Live Load for SF for all tracks = LL-sf = LL_sf x Tno = 203.97132 x 2 = 407.94264 Tons");


                sw.WriteLine("Live Load for BM for all tracks = LL-bm = LL-bm x Tno = {0:f3} x {1} = {2:f3} Tons", LL_bm, Tno, (LL_bm * Tno));
                sw.WriteLine("Live Load for SF for all tracks = LL-sf = LL_sf x Tno = {0:f3} x {1} = {2:f3} Tons", LL_sf, Tno, (LL_sf * Tno));
                sw.WriteLine("");

                LL_bm = LL_bm * Tno;
                LL_sf = LL_sf * Tno;

                LL = Math.Max(LL_bm, LL_sf);
                sw.WriteLine("Taking larger of these two values,");
                sw.WriteLine("Live Load = LL = {0:f3} Tons", LL);
                sw.WriteLine("");

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Live Load dispersion");
                sw.WriteLine("---------------------");
                sw.WriteLine("");
                sw.WriteLine("");

                _d = H + Db;
                sw.WriteLine("Depth of Fill = d = H + Db = {0:f3} + {1:f3} = {2:f3} m.", H, Db, _d);
                sw.WriteLine("");

                Fact_Hl = 1 / Hl;
                sw.WriteLine("Load Dispersion slope = Hl = {0}, Fact_Hl = 1/Hl = ½ = {1:f2}", Hl, Fact_Hl);
                sw.WriteLine("");

                double lw1 = ls + (2 * Fact_Hl * _d);
                sw.WriteLine("Width of one Track = lw1 = ls + (2 x Fact_Hl x d) = {0:f3} + (2 x {1:f3} x {2:f3}) = {3:f3} m.", ls, Fact_Hl, _d, lw1);
                sw.WriteLine("");
                sw.WriteLine("Effective span of RCC Box = Lbm = {0:f3} m.", Lbm);
                sw.WriteLine("");

                double lw2 = Lbm / 4;
                sw.WriteLine("Extra width for slab = lw2 = {0:f3} / 4 = {1:f3} m.", Lbm, lw2);
                sw.WriteLine("");

                double dlw1 = lw1 + lw2;
                sw.WriteLine("Dispersed width of one Track = dlw1 = lw1 + lw2 = {0:f3} + {1:f3} = {2:f3} m. ", lw1, lw2, dlw1);
                sw.WriteLine("");
                sw.WriteLine("Centre to Centre distance between the Rail Tracks = Rs = {0:f3} m", Rs);
                sw.WriteLine("");

                double dlw2 = dlw1 + (Tno - 1) * Rs;


                sw.WriteLine("Dispersed width of all Tracks = dlw2 = dlw1 + (Tno-1) x Rs  = {0:f3} + ({1}-1) x {2:f3} m. = {3:f3} m.", dlw1, Tno, Rs, dlw2);
                sw.WriteLine("");

                double LLtop = LL / (dlw2 * Lbm);
                sw.WriteLine("Live Load on Top Slab = LLtop = LL / (dlw2 x Lbm) = {0:f3} / ({1:f3} x {2:f3}) = {3:f3} Ton / Sq.m", LL, dlw2, Lbm, LLtop);
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("Vehicle Load on Bottom Slab = Wvl = {0:f3} Tons", Wvl);
                sw.WriteLine("Length of Vehicle Load = Lvl = {0:f3} m", Lvl);
                sw.WriteLine("");


                sw.WriteLine("Load Dispersion slope = Hl = {0}, Fact_Hl = 1/Hl = ½ = {1:f2}", Hl, Fact_Hl);

                lw1 = Lvl + (2 * Fact_Hl);


                sw.WriteLine("Dispersed width = lw1 = Lvl + (2 x Fact_Hl) = {0} + (2 x {1:f2}) = {2:f2} m.", Lvl, Fact_Hl, lw1);
                sw.WriteLine("");
                sw.WriteLine("Effective span of RCC Box for Shear Force = Lsf = {0:f3} m.", Lsf);
                sw.WriteLine("");

                double LLbot = Wvl / (Lsf * lw1);
                sw.WriteLine("Live Load on Bottom Slab = LLbot = {0} / ({1:f3} x {2:f3}) = {3:f3} Tons/Sq.m", Wvl, Lsf, lw1, LLbot);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("FINAL DESIGN LOADS:");
                sw.WriteLine("");
                sw.WriteLine("Loads on Bottom Slab:");



                 p1 = DLtop;
                 p3 = p2 = LLtop;
                 p4 = (p1 + p2);

                sw.WriteLine("Dead Load on Top Slab = p1 = DLtop = {0:f3} Tons/Sq.m", DLtop);
                sw.WriteLine("Live Load on Top Slab = p2 = p3 = LLtop = {0:f3} Ton / Sq.m", LLtop);
                sw.WriteLine("p4 = p1 + p2 = {0:f3} + {1:f3} = {2:f3} Ton / Sq.m", p1, p2, p4);



                 p5 = DLbot;
                 p6 = p5 + p2 + LLbot;
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Loads on Bottom Slab:");
                sw.WriteLine("Dead Load on Bottom Slab = p5 = DLbot = {0:f3} Tons/Sq.m", p5);
                sw.WriteLine("Live Load on Bottom = LLbot = {0:f3} Tons/Sq.m", LLbot);
                sw.WriteLine("p6 = p5 + p2 + LLbot = {0:f3} + {1:f3} + {2:f3} = {3:f3} Tons/Sq.m", p5, p2, LLbot, p6);
                sw.WriteLine("");

                #endregion Rail Load








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
                double DF_ab, DF_ad, DF_ba, DF_bc, DF_cd, DF_cb, DF_da, DF_dc;


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
                                    "DF_ba", DF_ba);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                "",
                                "",
                                "(Cal 2)",
                                "(Cal 10)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "BC",
                                    "K_bc",
                                    "DF_bc", DF_bc);

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
                                    "DF_cd", DF_cd);

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 7)",
                                   "(Cal 15)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "CB",
                                    "K_cb",
                                    "DF_cb", DF_cb);
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
                                    "DF_da", DF_da);
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                      "",
                                      "",
                                      "(Cal 4)",
                                      "(Cal 12)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-7} = {4,-6:f4}",
                                    "",
                                    "DC",
                                    "K_dc",
                                    "DF_dc", DF_dc);
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
                //sw.WriteLine(" Table-1 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE I ");
                sw.WriteLine(" MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE I ");
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
                //sw.WriteLine(" Table-2 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE II ");
                sw.WriteLine("MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE II ");
                sw.WriteLine("-----------------------------------------------------------------------------");
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
                sw.WriteLine("MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE III ");
                sw.WriteLine("-----------------------------------------------------------------------------");
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
                sw.WriteLine("SUMMARY OF SUPPORT MOMENTS FOR TOTAL LOADS");
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
                sw.WriteLine("MID SPAN MOMENTS");
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
                sw.WriteLine("TABLE 1 : BROAD_GAUGE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_2(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 2 : PERMISSIBLE SHEAR STRESS ");
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

        public void Write_Table_2(StreamWriter sw)
        {
            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");

            List<string> lst_content = iApp.Tables.Get_Table_Broad_Gauge();

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

}
