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

namespace AstraFunctionOne.BridgeDesign.Design3
{
    public partial class frmDesignCrossGirder : Form
    {
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        bool is_process = false;

        #region Variable Declaration
        //double hogging_moment;
        //double max_hogging_moment;
        double M_total_hogging_moment;
        double L_spacing_longitudinal_girder;
        double number_longitudinal_girder;
        double spacing_cross_girder;
        double number_cross_girder;
        double D_depth_cross_girder;
        double b_web_thickness_cross_girder;
        //double shear_DL_SIDL;
        //double max_shear;
        double W_total_shear;
        double grade_concrete;
        double grade_steel;
        double clear_cover;
        double fc_stress_concrete;
        double fs_stress_steel;
        double m_modular_ratio;
        double L_by_D;
        double Z_lever_arm;
        double Ast1;
        double Ast2;
        double located_within_depth;
        double bar_dia;
        double development_length;
        double required_steel_hanging_reinf;
        double provided_steel_per_meter_length;
        double required_steel;
        //hogging_moment = 23.8;
        //max_hogging_moment = 34.6;
        //M_total_hogging_moment = hogging_moment + max_hogging_moment;
        //L_spacing_longitudinal_girder = 2.65;
        //number_longitudinal_girder = 4;
        //spacing_cross_girder = 9.6;
        //number_cross_girder = 3;
        //D_depth_cross_girder = 1.525;
        //b_web_thickness_cross_girder = 0.325;
        //shear_DL_SIDL = 0;
        //max_shear = 16.8;
        //W_total_shear = shear_DL_SIDL + max_shear;
        //grade_concrete = 25; // M25
        //grade_steel = 415;
        //clear_cover = 0.04;
        //fc_stress_concrete = 1000; //t/sq.m.
        //fs_stress_steel = 2000; //t/sq.m.
        //m_modular_ratio = 10;

        #endregion

        #region Drawing Variable
        string _1, _2, _3, _4, _5, _6, _A, _B, _C;
        #endregion

        IApplication iApp = null;

        public sbyte TBeamDesign { get; set; }
        public frmDesignCrossGirder(IApplication app, sbyte TBeamDesignOption)
        {
            TBeamDesign = TBeamDesignOption;
            InitializeComponent();
            this.iApp = app;

        }


        void Read_Max_Moment_Shear_from_Analysis()
        {
            //string f_path = Environment.GetEnvironmentVariable("TBEAM_ANALYSIS");
            string f_path = Path.Combine(user_path, "FORCES.TXT");
            if (File.Exists(f_path))
            {
                txt_total_hogging_moment.ForeColor = Color.Red;
                txt_total_shear.ForeColor = Color.Red;

                List<string> list = new List<string>(File.ReadAllLines(f_path));

                MyList mlist = null;
                string kStr = "";
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i].ToUpper();

                    mlist = new MyList(kStr, '=');
                    if (mlist.StringList.Count > 1)
                    {
                        if (mlist.StringList[0] == "CROSS_MOM")
                            txt_total_hogging_moment.Text = mlist.StringList[1];
                        if (mlist.StringList[0] == "CROSS_SHR")
                            txt_total_shear.Text = mlist.StringList[1];
                    }
                }
            }
        }

        private void frmDesignCrossGirder_Load(object sender, EventArgs e)
        {
            //Read_Max_Moment_Shear_from_Analysis();
            if (TBeamDesign == 2)
            {
                user_input_file = Path.Combine(iApp.AppFolder, "DESIGN\\DefaultData\\DESIGN_OF_CROSS_GIRDERS.FIL");
                Read_User_Input();
                user_input_file = "";
                pb_image.BackgroundImage = global::AstraFunctionOne.Properties.Resources.TBeam_Main_Girder_Bottom_Flange;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
           
            if (M_total_hogging_moment == 0.0 && W_total_shear == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : Total Hogging Moment  = 58.4 Ton-m\n";
                msg += "            : Total Shear = 16.8 Ton\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {

                Write_User_Input();
                CalculateProgram();
                Write_Drawing_File();
                MessageBox.Show(this, "Report file wriiten in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                is_process = true;
                FilePath = user_path;
            }
        }
        public void CalculateProgram()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));

            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 22              *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN  OF  CROSS  GIRDER          *");
            sw.WriteLine("\t\t*            FOR T-BEAM RCC BRIDGE            *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                InitializeData();

                #region USER INPUT DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Collecting Bending Moment Values from ASTRA Pro Moving Load Analysis");
                sw.WriteLine();
                //sw.WriteLine("Hogging Moment ( DL + SIDL ) = {0} t-m", hogging_moment);
                //sw.WriteLine("Maximum Hogging Moment due to Moving Load = {0} t-m", max_hogging_moment);
                sw.WriteLine("Total Hogging Moment [M] = {0} t-m", M_total_hogging_moment);

                sw.WriteLine("Spacing of Longitudinal Girders [L] = {0} m    Marked as (A) in the Drawing", L_spacing_longitudinal_girder);
                _A = "(A) " + (L_spacing_longitudinal_girder * 1000) + " mm";

                
                sw.WriteLine("Number of Longitudinal Girders = {0:f0} ", number_longitudinal_girder);
                sw.WriteLine("Spacing of Cross Girders = {0} m", spacing_cross_girder);
                sw.WriteLine("Number of Cross Girders = {0:f0} ", number_cross_girder);
                sw.WriteLine("Depth of Cross Girders [D] = {0} m    Marked as (B) in the Drawing", D_depth_cross_girder);
                _B = "(B) " + D_depth_cross_girder * 1000 + " mm";
                
                
                
                sw.WriteLine("Web thickness of Cross Girders [b] = {0} m    Marked as (C) in the Drawing", b_web_thickness_cross_girder);
                _C = "(C) " + b_web_thickness_cross_girder * 1000 + " mm";
                
                
                //sw.WriteLine("Shear ( DL + SIDL ) = {0} t", shear_DL_SIDL);
                //sw.WriteLine("Maximum Shear due to Moving Load = {0} t", max_shear);
                sw.WriteLine("Total Shear = {0} t", W_total_shear);

                sw.WriteLine("Grade of Concrete = M {0:f0} ", grade_concrete);
                sw.WriteLine("Grade of Steel = Fe {0:f0} ", grade_steel);
                sw.WriteLine("Clear Cover = {0} m", clear_cover);
                sw.WriteLine("Stress in Concrete [fc] = {0} t/sq.m.", fc_stress_concrete);
                sw.WriteLine("Stress in Steel [fs] = {0} t/sq.m.", fs_stress_steel);
                sw.WriteLine("Modular Ratio [m] = {0}", m_modular_ratio);

                #endregion

                #region DESIGN

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF CROSS GIRDER");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double eff_d = (D_depth_cross_girder * 1000) - 50 - 11 - 22;
                sw.WriteLine("Effective Depth = {0} - 50 - 11 - 22 = {1:f2} mm", (D_depth_cross_girder * 1000), eff_d);
                //Step 1
                L_by_D = L_spacing_longitudinal_girder / D_depth_cross_girder;
                #endregion

                #region STEP 1: LEVER ARM

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1: LEVER ARM");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("By Clause 28.2, IS 456-2000");
                if (L_by_D >= 1)
                {
                    sw.WriteLine(" L/D = {0}/{1} = {2} >= 1 , OK for continuous Beam.",
                        L_spacing_longitudinal_girder.ToString("0.000"),
                        D_depth_cross_girder.ToString("0.000"),
                        L_by_D.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine("Now, L/D = {0}/{1} = {2} < 1 , NOT OK for continuous Beam.",
                        L_spacing_longitudinal_girder.ToString("0.000"),
                        D_depth_cross_girder.ToString("0.000"),
                        L_by_D.ToString("0.000"));

                }
                Z_lever_arm = 0.2 * (L_spacing_longitudinal_girder + (1.5 * D_depth_cross_girder));
                sw.WriteLine();
                sw.WriteLine("Lever Arm = Z = 0.2 * (L + 1.5 * D) = {0}", Z_lever_arm.ToString("0.0000"));


                #endregion

                #region Step 2

                //Step 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : TOP STEEL BARS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Total Moment from ASTRA Moving Load analysis = {0:f2} t-m", M_total_hogging_moment);
                Ast1 = (M_total_hogging_moment * 10E3) / (Z_lever_arm * fs_stress_steel * m_modular_ratio);

                sw.WriteLine();
                sw.WriteLine("Required steel at Top = Ast1 = (M * 10E3)/(Z * fs)");
                sw.WriteLine("                      = ({0} * 10E3)/({1} * {2})",
                    M_total_hogging_moment,
                    Z_lever_arm,
                    fs_stress_steel);

                sw.WriteLine("                      = {0:f0} sq.cm.",Ast1);

                double d1 = 2.5;
                double d1_ast = (Math.PI * d1 * d1 / 4);

                int no_d1_ast = (int)(Ast1 / d1_ast);

                double d1_ast_2 = no_d1_ast * d1_ast;

                double d2 = 2.0;
                double d2_ast = (Math.PI * d2 * d2 / 4);

                double diff_ast;
                int no_d2_ast = 0;

                sw.WriteLine();
                if (Ast1 > d1_ast_2)
                {
                    no_d1_ast -= 1;
                    d1_ast_2 = no_d1_ast * d1_ast;
                    diff_ast = Ast1 - d1_ast_2;
                    no_d2_ast = (int)(diff_ast / d2_ast);
                    no_d2_ast++;

                    sw.WriteLine("Provide {0}-T{1} + {2}-T{3}    Marked as (2) in the Drawing",
                        no_d1_ast,
                        (d1 * 10),
                        no_d2_ast,
                        (d2 * 10));

                    //(2)  5 Nos. T25 + 2 Nos. T20
                    _2 = no_d1_ast + " Nos. T" + (d1 * 10) + " + " + no_d2_ast + " Nos. T" + (d2 * 10);



                    sw.WriteLine();
                    sw.WriteLine("Provided Ast1 = {0} sq.cm",
                        (d1_ast_2 + (no_d2_ast * d2_ast)).ToString("0.00"));


                }
                else
                {
                    sw.WriteLine("Provide {0}-T{1}    Marked as (2) in the Drawing",
                    no_d1_ast,
                    (d1 * 10).ToString("0"));

                    _2 = no_d1_ast + " Nos. T" + (d1 * 10);


                    sw.WriteLine();
                    sw.WriteLine("Provided Ast1 = {0} sq.cm",
                                           d1_ast_2.ToString("0.00"));

                }

                //Step 3
                Ast2 = 0.002 * b_web_thickness_cross_girder * 100 * D_depth_cross_girder * 100;

                #endregion

                #region Step 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : BOTTOM STEEL BAR");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Required Steel at bottom (minimum) = Ast2");
                sw.WriteLine("                                   = 0.2 % (b*100 * D*100)");
                sw.WriteLine("                                   = 0.2 % ({0}*100 * {1}*100)",
                    b_web_thickness_cross_girder,
                    D_depth_cross_girder);
                sw.WriteLine("                                   = {0} sq.cm.", Ast2.ToString("0.000"));

                d1 = 1.6;
                d1_ast = Math.PI * d1 * d1 / 4;

                no_d1_ast = (int)(Ast2 / d1_ast);
                no_d1_ast++;
                d1_ast_2 = no_d1_ast * d1_ast;

                sw.WriteLine();
                sw.WriteLine("Provide {0}-T{1:f0}    Marked as (1) in the Drawing", no_d1_ast, (d1 * 10));

                //(1)  (2 + 3) Nos. 22 Ø


                _1 = no_d1_ast + " Nos. 22 Ø";
                //(6)  5 Nos. 22 Ø
                _6 = no_d1_ast + " Nos. 22 Ø";

                sw.WriteLine();
                sw.WriteLine("Provided Ast2 = {0} sq.cm.", d1_ast_2.ToString("0.000"));

                located_within_depth = 0.25 * 1.525 - 0.05 * L_spacing_longitudinal_girder;

                sw.WriteLine();
                sw.WriteLine("Located within a depth of  (0.25 * D - 0.05 * L)");
                sw.WriteLine("                         = (0.25 * {0} - 0.05 * {1})",
                    D_depth_cross_girder,
                    L_spacing_longitudinal_girder);

                sw.WriteLine("                         = {0} m. from bottom face.",
                    located_within_depth.ToString("0.000"));



                bar_dia = 16.0;
                development_length = 0.8 * 35 * bar_dia;

                sw.WriteLine();
                sw.WriteLine("Development Length = 0.8 * 35 * Bar Diameter");
                sw.WriteLine("                   = 0.8 * 35 * {0} = {1} mm", bar_dia, development_length);
                //Step 4
                sw.WriteLine();

                #endregion

                #region Step 4
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Hogging Reinforcements,By Clause 28.33 IS 456 - 2000");
                sw.WriteLine("Total Shear from ASTRA moving Load analysis = {0} t", W_total_shear);
                required_steel_hanging_reinf = W_total_shear * 10000 / 20000; // TO BE CHECKED
                sw.WriteLine();
                sw.WriteLine("Required Steel Hanging Reinforcement = {0} * 10000/20000",
                    W_total_shear);
                sw.WriteLine("                                     = {0} sq.cm.",
                    required_steel_hanging_reinf);

                provided_steel_per_meter_length = required_steel_hanging_reinf / (L_spacing_longitudinal_girder / 2);
                sw.WriteLine();
                sw.WriteLine("Required Steel per meter length = {0} / (L/2) = {0} / {1} = {2} sq.cm/m.",
                    required_steel_hanging_reinf,
                    (L_spacing_longitudinal_girder / 2).ToString("0.000"),
                    provided_steel_per_meter_length.ToString("0.00"));


                d1 = 1.2;
                d1_ast = Math.PI * d1 * d1 / 4;

                double spacing = 220;
                do
                {
                    spacing -= 20;
                    no_d1_ast = (int)(1000.0 / spacing);
                    d1_ast_2 = no_d1_ast * d1_ast * 2;
                }
                while (d1_ast_2 < required_steel_hanging_reinf);

                sw.WriteLine();
                sw.WriteLine("Provided 2-Legged T12 {0} mm c/c stirrups as vertical Reinforcement  Marked as (3) in the Drawing", spacing);

                //(3)  2-Legged T12 200 mm c/c stirrups
                _3 = "2-Legged T12 " + spacing + " mm c/c stirrups";

                sw.WriteLine();
                sw.WriteLine("Provided steel = {0} sq.cm/m", d1_ast_2.ToString("0.000"));
                //Step 5
                sw.WriteLine();

                #endregion

                #region Step 5
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Side Face Reinforcements,By Clause 31.4  IS 456 2000");
                sw.WriteLine("0.1% of Web area on each face with spacing not more than 450 mm");

                required_steel = 0.001 * D_depth_cross_girder * 100 * b_web_thickness_cross_girder * 100;

                sw.WriteLine("Required steel = 0.001 * D * 100 * b * 100");
                sw.WriteLine("               = 0.001 * {0} * 100 * {1} * 100",
                    D_depth_cross_girder,
                    b_web_thickness_cross_girder);
                sw.WriteLine("               = {0} sq.cm.", required_steel.ToString("0.00"));


                d1 = 1.6;
                d1_ast = Math.PI * d1 * d1 / 4;
                spacing = 320;
                do
                {
                    spacing -= 20;
                    no_d1_ast = (int)(1000.0 / spacing);
                    d1_ast_2 = no_d1_ast * d1_ast;
                }
                while (d1_ast_2 < required_steel);

                sw.WriteLine();
                sw.WriteLine("Provide 3-T16 bars @ {0} mm c/c     Marked as (4) in the Drawing", spacing);

                //(4)  6 Nos. 6 Ø Side face reinforcements
                //(5)  3-T16 bars @ 300 mm c/c 
                _4 = "6 Nos. 6 Ø Side face reinforcements";
                _5 = "3-T16 bars @ " + spacing + "mm c/c";

                sw.WriteLine("Provided steel each side = {0:f2} sq.cm", d1_ast_2);
                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                //provided_steel = 
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void InitializeData()
        {
            #region Variable Declaration
            //hogging_moment = MyList.StringToDouble(txt_hogging_moment.Text, 0.0);
            //max_hogging_moment = MyList.StringToDouble(txt_max_hogging_moment.Text, 0.0);

            M_total_hogging_moment = MyList.StringToDouble(txt_total_hogging_moment.Text, 0.0);

            L_spacing_longitudinal_girder = MyList.StringToDouble(txt_spacing_longitudinal_girder.Text, 0.0);

            number_longitudinal_girder = MyList.StringToDouble(txt_number_longitudinal_girder.Text, 0.0);

            spacing_cross_girder = MyList.StringToDouble(txt_spacing_cross_girders.Text, 0.0);

            number_cross_girder = MyList.StringToDouble(txt_number_cross_girder.Text, 0.0);
            D_depth_cross_girder = MyList.StringToDouble(txt_depth_cross_girder.Text, 0.0);
            b_web_thickness_cross_girder = MyList.StringToDouble(txt_web_thickness_cross_girder.Text, 0.0);

            //shear_DL_SIDL = MyList.StringToDouble(txt_shear.Text, 0.0);
            //max_shear = MyList.StringToDouble(txt_maximum_shear.Text, 0.0);
            W_total_shear = MyList.StringToDouble(txt_total_shear.Text, 0.0);
            grade_concrete = MyList.StringToDouble(txt_grade_concrete.Text, 0.0);
            grade_steel = MyList.StringToDouble(txt_grade_steel.Text, 0.0);
            clear_cover = MyList.StringToDouble(txt_clear_cover.Text, 0.0);
            fc_stress_concrete = MyList.StringToDouble(txt_stress_concrete.Text, 0.0);
            fs_stress_steel = MyList.StringToDouble(txt_stress_steel.Text, 0.0);
            m_modular_ratio = MyList.StringToDouble(txt_modular_ratio.Text, 0.0);

            //L_by_D = MyList.StringToDouble(txt_);
            //Z_lever_arm = MyList.StringToDouble(txt_);
            //Ast1 = MyList.StringToDouble(txt_);
            //Ast2 = MyList.StringToDouble(txt_);
            //located_within_depth = MyList.StringToDouble(txt_);
            //bar_dia = MyList.StringToDouble(txt_);
            //development_length = MyList.StringToDouble(txt_);
            //required_steel_hogging_reinf = MyList.StringToDouble(txt_);
            //provided_steel_per_meter_length = MyList.StringToDouble(txt_);
            //required_steel = MyList.StringToDouble(txt_);

            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult f_v_r = new frmViewResult(rep_file_name);
            f_v_r.ShowDialog();
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                    Read_Max_Moment_Shear_from_Analysis();
                }
            }
            else
                return;
        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Bridge Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "RCC T-Beam Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Cross Girders");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }


        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF CROSS GIRDERS : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "CROSS_GIRDERS");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Cross_Girder.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_CROSS_GIRDERS.FIL");
                user_drawing_file = Path.Combine(system_path, "CROSS_GIRDERS_DRAWING.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(rep_file_name);
                //btnDrawing.Enabled = File.Exists(rep_file_name);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }
            }
        }

        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    //indx = lst_content[i].LastIndexOf(" ");
                    //if (indx > 0)
                    //    kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    //else
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        //case "HOGGING_MOMENT":
                        //    hogging_moment = mList.GetDouble(1);
                        //    txt_hogging_moment.Text = hogging_moment.ToString();
                        //    break;
                        //case "MAX_HOGGING_MOMENT":
                        //    max_hogging_moment = mList.GetDouble(1);
                        //    txt_max_hogging_moment.Text = max_hogging_moment.ToString();
                        //    break;
                        case "TOTAL_HOGGING_MOMENT":
                            M_total_hogging_moment = mList.GetDouble(1);
                            txt_total_hogging_moment.Text = M_total_hogging_moment.ToString();
                            break;
                        case "SPACING_LONGITUDINAL_GIRDER":
                            L_spacing_longitudinal_girder = mList.GetDouble(1);
                            txt_spacing_longitudinal_girder.Text = L_spacing_longitudinal_girder.ToString();
                            break;
                        case "NUMBER_LONGITUDINAL_GIRDER":
                            number_longitudinal_girder = mList.GetDouble(1);
                            txt_number_longitudinal_girder.Text = number_longitudinal_girder.ToString();
                            break;
                        case "SPACING_CROSS_GIRDER":
                            spacing_cross_girder = mList.GetDouble(1);
                            txt_spacing_cross_girders.Text = spacing_cross_girder.ToString();
                            break;
                        case "NUMBER_CROSS_GIRDER":
                            number_cross_girder = mList.GetDouble(1);
                            txt_number_cross_girder.Text = number_cross_girder.ToString();
                            break;
                        case "DEPTH_CROSS_GIRDER":
                            D_depth_cross_girder = mList.GetDouble(1);
                            txt_depth_cross_girder.Text = D_depth_cross_girder.ToString();
                            break;
                        case "WEB_THICKNESS_CROSS_GIRDER":
                            b_web_thickness_cross_girder = mList.GetDouble(1);
                            txt_web_thickness_cross_girder.Text = b_web_thickness_cross_girder.ToString();
                            break;
                        //case "SHEAR_DL_SIDL":
                        //    shear_DL_SIDL = mList.GetDouble(1);
                        //    txt_shear.Text = shear_DL_SIDL.ToString();
                        //    break;
                        //case "MAX_SHEAR":
                        //    max_shear = mList.GetDouble(1);
                        //    txt_maximum_shear.Text = max_shear.ToString();
                        //    break;
                        case "TOTAL_SHEAR":
                            W_total_shear = mList.GetDouble(1);
                            txt_total_shear.Text = W_total_shear.ToString();
                            break;
                        case "GRADE_CONCRETE":
                            grade_concrete = mList.GetDouble(1);
                            txt_grade_concrete.Text = grade_concrete.ToString();
                            break;
                        case "GRADE_STEEL":
                            grade_steel = mList.GetDouble(1);
                            txt_grade_steel.Text = grade_concrete.ToString();
                            break;
                        case "CLEAR_COVER":
                            clear_cover = mList.GetDouble(1);
                            txt_clear_cover.Text = clear_cover.ToString();
                            break;
                        case "STRESS_CONCRETE":
                            fc_stress_concrete = mList.GetDouble(1);
                            txt_stress_concrete.Text = fc_stress_concrete.ToString();
                            break;
                        case "STRESS_STEEL":
                            fs_stress_steel = mList.GetDouble(1);
                            txt_stress_steel.Text = fs_stress_steel.ToString();
                            break;
                        case "MODULAR_RATIO":
                            m_modular_ratio = mList.GetDouble(1);
                            txt_modular_ratio.Text = m_modular_ratio.ToString();
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

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                #region SWITCH

                //sw.WriteLine("HOGGING_MOMENT = {0}", txt_hogging_moment.Text);
                //sw.WriteLine("MAX_HOGGING_MOMENT = {0}", txt_max_hogging_moment.Text);
                sw.WriteLine("TOTAL_HOGGING_MOMENT = {0}", txt_total_hogging_moment.Text);
                sw.WriteLine("SPACING_LONGITUDINAL_GIRDER = {0}", txt_spacing_longitudinal_girder.Text);
                sw.WriteLine("NUMBER_LONGITUDINAL_GIRDER = {0}", txt_number_longitudinal_girder.Text);
                sw.WriteLine("SPACING_CROSS_GIRDER = {0}", txt_spacing_cross_girders.Text);
                sw.WriteLine("NUMBER_CROSS_GIRDER = {0}", txt_number_cross_girder.Text);
                sw.WriteLine("DEPTH_CROSS_GIRDER = {0}", txt_depth_cross_girder.Text);
                sw.WriteLine("WEB_THICKNESS_CROSS_GIRDER = {0}", txt_web_thickness_cross_girder.Text);
                //sw.WriteLine("SHEAR_DL_SIDL = {0}", txt_shear.Text);
                //sw.WriteLine("MAX_SHEAR = {0}", txt_maximum_shear.Text);
                sw.WriteLine("TOTAL_SHEAR = {0}", txt_total_shear.Text);
                sw.WriteLine("GRADE_CONCRETE = {0}", txt_grade_concrete.Text);
                sw.WriteLine("GRADE_STEEL = {0}", txt_grade_steel.Text);
                sw.WriteLine("CLEAR_COVER = {0}", txt_clear_cover.Text);
                sw.WriteLine("STRESS_CONCRETE = {0}", txt_stress_concrete.Text);
                sw.WriteLine("STRESS_STEEL = {0}", txt_stress_steel.Text);
                sw.WriteLine("MODULAR_RATIO = {0}", txt_modular_ratio.Text);
                #endregion
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
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_1=(1) {0}", _1);
                sw.WriteLine("_2=(2) {0}", _2);
                sw.WriteLine("_3=(3) {0}", _3);
                sw.WriteLine("_4=(4) {0}", _4);
                sw.WriteLine("_5=(5) {0}", _5);
                sw.WriteLine("_6=(6) {0}", _6);
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
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
