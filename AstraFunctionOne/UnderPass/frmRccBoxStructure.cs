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
namespace AstraFunctionOne.UnderPass
{
    public partial class frmRccBoxStructure : Form
    {
       
        string rep_file_name = "";
        string file_path = "";
        string user_input_file = "";
                                string system_path = "";
        string drawing_path = "";
        string user_path = "";

        string ref_string = "";


        bool is_process = false;

        #region Variable Initialization

        double H, b, d, d1, d2, d3, gamma_b, gamma_c, R, t, j, cover;
        double b1, b2, a1, w1, w2, b3, F, S, sbc, sigma_st, sigma_c;
        CONCRETE_GRADE Con_Grade;

        List<double> lst_Bar_Dia = null;
        List<double> lst_Bar_Space = null;

        #endregion

        #region Drawing Variables

        double bd1, bd2, bd3, bd4, bd5, bd6, bd7, bd8, bd9, bd10, bd11, bd12, bd13, bd14, bd15;
        double sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10, sp11, sp12, sp13, sp14, sp15;
        double _pressure = 0.0;

        #endregion

        IApplication iApp = null;
        
        public frmRccBoxStructure(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            //St_Grade = TAU_C.STEEL_GRADE.M25;

            lst_Bar_Dia = new List<double>();
            lst_Bar_Space = new List<double>();
            lst_Bar_Dia.Add(0);
            lst_Bar_Space.Add(0);
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
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
            kPath = Path.Combine(kPath, "Underpass");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Pedestrian Underpass");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of RCC Box Structure");

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
                this.Text = "DESIGN OF RCC BOX STRUCTURE : " + value;

                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_BOX_STRUCTURE");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Underpass_Ped_Box.TXT");
                user_input_file = Path.Combine(system_path, "RCC_BOX_STRUCTURE.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    //string msg = "The folder already contains Previous Design. Overwrite?";
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_From_File(user_input_file);
                }
            }
        }
        public void Read_From_File(string fileName)
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
                            H = mList.GetDouble(1);
                            txt_H.Text = H.ToString();
                            break;
                        case "b":
                            b = mList.GetDouble(1);
                            txt_b.Text = b.ToString();
                            break;
                        case "d":
                            d = mList.GetDouble(1);
                            txt_d.Text = d.ToString();
                            break;
                        case "d1":
                            d1 = mList.GetDouble(1);
                            txt_d1.Text = d1.ToString();
                            break;
                        case "d2":
                            d2 = mList.GetDouble(1);
                            txt_d2.Text = d2.ToString();
                            break;
                        case "d3":
                            d3 = mList.GetDouble(1);
                            txt_d3.Text = d3.ToString();
                            break;
                        case "gamma_b":
                            gamma_b = mList.GetDouble(1);
                            txt_gamma_b.Text = gamma_b.ToString();
                            break;
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "R":
                            R = mList.GetDouble(1);
                            txt_R.Text = R.ToString();
                            break;
                        case "t":
                            t = mList.GetDouble(1);
                            txt_t.Text = t.ToString();
                            break;
                        case "j":
                            j = mList.GetDouble(1);
                            txt_j.Text = j.ToString();
                            break;
                        case "cover":
                            cover = mList.GetDouble(1);
                            txt_cover.Text = cover.ToString();
                            break;
                        case "sigma_c":
                            sigma_c = mList.GetDouble(1);
                            txt_sigma_c.Text = sigma_c.ToString();
                            break;

                        case "sigma_st":
                            sigma_st = mList.GetDouble(1);
                            txt_sigma_st.Text = sigma_st.ToString();
                            break;
                        case "b1":
                            b1 = mList.GetDouble(1);
                            txt_b1.Text = b1.ToString();
                            break;
                        case "b2":
                            b2 = mList.GetDouble(1);
                            txt_b2.Text = b2.ToString();
                            break;
                        case "a1":
                            a1 = mList.GetDouble(1);
                            txt_a1.Text = a1.ToString();
                            break;
                        case "w1":
                            w1 = mList.GetDouble(1);
                            txt_w1.Text = w1.ToString();
                            break;
                        case "w2":
                            w2 = mList.GetDouble(1);
                            txt_w2.Text = w2.ToString();
                            break;
                        case "b3":
                            b3 = mList.GetDouble(1);
                            txt_b3.Text = b3.ToString();
                            break;
                        case "F":
                            F = mList.GetDouble(1);
                            txt_F.Text = F.ToString();
                            break;
                        case "S":
                            S = mList.GetDouble(1);
                            txt_S.Text = S.ToString();
                            break;
                        case "sbc":
                            sbc = mList.GetDouble(1);
                            txt_sbc.Text = sbc.ToString();
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
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            WriteUserInput();
            Calculate_Program(rep_file_name);
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
        }
        public void Calculate_Program(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*         DESIGN OF RCC BOX STRUCTURE         *");
                sw.WriteLine("\t\t*          FOR PEDESTRIAN UNDERPASS           *");
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
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (H) in the Drawing",
                    "Height of Earth Cushion = H",
                    H,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (b) in the Drawing",
                    "Inside Clear Width = b",
                    b,
                    "m");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (d) in the Drawing",
                    "Inside Clear Depth = d",
                    d,
                    "m");


                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d1) in the Drawing",
                    "Thickness of Top Slab = d1",
                    d1,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d2) in the Drawing",
                    "Thickness of Bottom Slab = d2",
                    d2,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d3) in the Drawing",
                    "Thickness of Side Walls = d3",
                    d3,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Unit weight of Earth = γ_b",
                    gamma_b,
                    "kN/cu.m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Unit weight of Concrete = γ_c",
                    gamma_c,
                    "kN/cu.m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "R",
                    R,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "t",
                    t,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Lever Arm Factor = j",
                    j,
                    "");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Clear Cover = cover",
                    cover,
                    "mm");

                sw.WriteLine("{0,50} = {1,12} {2,-5}",
                    "Concrete Grade",
                    "M" + sigma_c.ToString("0"),
                    "");
                sw.WriteLine("{0,50} = {1,12} {2,-5}",
                    "Steel Grade",
                    "Fe " + sigma_st.ToString("0"),
                    "");


                // For single Track Loading in 2-Lane

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For single Track Loading in 2-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                  "Total Load = w1",
                  w1,
                  "kN");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Separating Distance of two loads = b1",
                    b1,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Width of each loaded Area",
                    b2,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Length of each Loaded Area = b2",
                    a1,
                    "m");

                // FOR Double Track Loading in 4-Lane
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For Double Track Loading in 4-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Total Load = w2",
                    w2,
                    "kN");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Separating Distance between two tracks = b3",
                    b3,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Two track Load dispersion factor = F",
                    F,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Equivalent Earth height for Live Load Surchage = S",
                    S,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
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
                sw.WriteLine("                                      = {0:f2} kN", p3);

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
                sw.WriteLine("  Load of Walls = (d * d3 * 2 * γ_c)/(1 * b + d3 + d3)");
                sw.WriteLine("                = ({0:f2} * {1:f2} * 2 * {2:f2})/(1 * ({3:f2} + {1:f2} + {1:f2}))",
                    d,
                    d3,
                    gamma_c,
                    b);
                sw.WriteLine("                = {0:f2} kN", loads_walls);

                double p5 = p1 + loads_walls;
                sw.WriteLine();
                sw.WriteLine("  Total Load = p5 ");
                sw.WriteLine("             = {0:f3} + {1:f3}", p1, loads_walls);
                sw.WriteLine("             = {0:f2} kN", p5);

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
                sw.WriteLine("               Pressure by Submerged Earth Back fill");
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


                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                    "A",
                    "AB",
                    "K_ab",
                    "DF_ab = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 1)",
                                   "(Cal 2)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "AD",
                                   "K_ad",
                                   "DF_ad = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 3)",
                                   "(Cal 4)");


                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "B",
                                    "BA",
                                    "K_ba",
                                    "DF_ba = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                "",
                                "",
                                "(Cal 5)",
                                "(Cal 6)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "BC",
                                    "K_bc",
                                    "DF_bc = 0.5");

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 7)",
                                    "(Cal 8)");
                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "C",
                                    "CD",
                                    "K_cd",
                                    "DF_cd = 0.5");

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 9)",
                                   "(Cal 10)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "CB",
                                    "K_cb",
                                    "DF_cb = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 11)",
                                    "(Cal 12)");

                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "D",
                                    "DA",
                                    "K_da",
                                    "DF_da = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                      "",
                                      "",
                                      "(Cal 13)",
                                      "(Cal 14)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "DC",
                                    "K_dc",
                                    "DF_dc = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 15)",
                                    "(Cal 16)");

                #region Calculation Details
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Cal 1   : K_ab = (k * d1 * d1 * d1) / (b + d3)");
                sw.WriteLine("Cal 2   : DF_ab = K_ab/(K_ab+K_ad)");
                sw.WriteLine("Cal 3   : K_ad = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                sw.WriteLine("Cal 4   : DF_ad = K_ad/(K_ab+K_ad) = 0.5");
                sw.WriteLine("Cal 5   : K_ba = K_ab");
                sw.WriteLine("Cal 6   : DF_ba = K_bA/(K_ba+K_bc) = 0.5");
                sw.WriteLine("Cal 7   : K_bc = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                sw.WriteLine("Cal 8   : DF_bc = K_bc/(K_ba+K_bc) = 0.5");
                sw.WriteLine("Cal 9   : K_cd = K*d2*d2*d2/(b+d3)");
                sw.WriteLine("Cal 10  : DF_cd = K_cd/(K_cd+K_cb) = 0.5");
                sw.WriteLine("Cal 11  : K_cb= (K*d3*d3*d3)/(d+(d3+d2)/2)");
                sw.WriteLine("Cal 12  : DF_cb = K_cb/(K_cd+K_cb) = 0.5");
                sw.WriteLine("Cal 13  : K_da = (K*d3*d3*d3)/(d+(d3+d2)/2)");
                sw.WriteLine("Cal 14  : DF_da = K_da/(K_da+K_dc) = 0.5");
                sw.WriteLine("Cal 15  : K_dc = K_cd");
                sw.WriteLine("Cal 16  : DF_dc = K_dc/(K_da+K_dc) = 0.5");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion
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

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
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
                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


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

                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

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


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

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
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
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

                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


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



                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

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
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

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
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
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

                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


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



                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

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


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

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
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

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

                lst_Mab.Add(m6 - Math.Abs(SMab1));
                lst_Mab.Add(m6 - Math.Abs(SMab2));
                lst_Mab.Add(m6 - Math.Abs(SMab3));
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
                                    String.Format("= {0:f2}", lst_Mab[0]),
                                    String.Format("= {0:f2}", lst_Mab[1]),
                                    String.Format("= {0:f2}", lst_Mab[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");


                lst_Mdc = new List<double>();
                lst_Mdc.Add(m10 - Math.Abs(SMdc1));
                lst_Mdc.Add(m10 - Math.Abs(SMdc2));
                lst_Mdc.Add(m10 - Math.Abs(SMdc3));

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
                                    String.Format("= {0:f2}", lst_Mdc[0]),
                                    String.Format("= {0:f2}", lst_Mdc[1]),
                                    String.Format("= {0:f2}", lst_Mdc[2]));





                sw.WriteLine("------------------------------------------------------------------------------------");

                lst_Mad = new List<double>();
                lst_Mad.Add(m18 - (Math.Abs(SMad1) + Math.Abs(SMda1)) / 2.0);
                lst_Mad.Add(m24 - (Math.Abs(SMad2) + Math.Abs(SMda2)) / 2.0);
                lst_Mad.Add(m29 - (Math.Abs(SMad3) + Math.Abs(SMda3)) / 2.0);



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
                                    String.Format("= {0:f2}", lst_Mad[0]),
                                    String.Format("= {0:f2}", lst_Mad[1]),
                                    String.Format("= {0:f2}", lst_Mad[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------------------------------");
                #endregion

                #region Taking Maiximum Values, Design Moments
                sw.WriteLine();
                sw.WriteLine("  Taking Maximum Values, Design Moments");
                sw.WriteLine();
                sw.WriteLine("      M_AB for Corners /Supports A & B        = {0:f3}", SMabx);
                sw.WriteLine("      M_DC for Corners /Supports C & D        = {0:f3}", SMdcx);
                lst_Mab.Sort();
                sw.WriteLine("      M_AB for Mid span of the Top Slab       = {0:f3}", lst_Mab[2]);

                lst_Mdc.Sort();
                sw.WriteLine("      M_DC for Mid span of the Bottom Slab    = {0:f3}", lst_Mdc[2]);

                lst_Mad.Sort();
                sw.WriteLine("      M_AD for Mid span of the Two side walls = {0:f3}", lst_Mad[2]);
                #endregion


                #region STEP 5

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double M = SMabx;
                sw.WriteLine("  Maximum Bending Moment at Support / Midspan");
                sw.WriteLine("  M = {0:f2} kN-m", M);
                double depth = Math.Sqrt((M * 10E5) / (1000 * R));
                sw.WriteLine();
                sw.WriteLine("  Depth = d = √((M * 10E5) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10E5) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Slab Thickness = {0:f2} mm", (d1 * 1000));
                double bar_dia = 16;
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

                double req_st_ast = (M * 10E5) / (t * j * deff);
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10E5)/(t*j*d)");
                sw.WriteLine("                                        = ({0:f2}*10E5)/({1:f2}*{2:f3}*{3:f2})",
                    M,
                    t, j, deff);
                sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);
                double pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                double spacing = req_st_ast / pro_st_ast;
                spacing = (int)((1000.0 / spacing) / 10.0);
                spacing = spacing * 10.0;



                pro_st_ast = pro_st_ast * (1000.0 / spacing);

                sw.WriteLine();
                sw.WriteLine("Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);

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
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");
                //double percent = req_st_ast * 100 / (1000 * deff);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);
                sw.WriteLine("  Using Table 1, Given at end of this report.");
                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                double percent = req_st_ast * 100 / (1000 * deff);

                double tau_c = Get_Table_1_Value(percent, Con_Grade);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);

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



                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm", ast_2);
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

                double _x = x * 10;
                //double _x = x / 100;
                _x = (int)(_x + 1);
                _x = _x * 100;


                sw.WriteLine("  x = {0:f2} m = {1:f2} mm (say)", x, _x);
                sw.WriteLine();
                sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                sw.WriteLine("  the face of wall on both sides.");


                #endregion

                #region STEP 6


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : DESIGN OF BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span");


                M = SMdcx;
                sw.WriteLine("  M = {0:f2} kN-m", M);
                depth = Math.Sqrt((M * 10E5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Depth Required = √((M * 10E5) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10E5) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Effective Depth deff = {0:f2} mm", (d1 * 1000));

                bar_dia = 16;
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

                req_st_ast = (M * 10E5) / (t * j * deff);
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10E5)/(t*j*d)");
                sw.WriteLine("                                        = ({0:f2}*10E5)/({1:f2}*{2:f3}*{3:f2})",
                    M,
                    t, j, deff);
                sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);


                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);
                spacing = req_st_ast / pro_st_ast;
                spacing = 1000.0 / spacing;
                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);



                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);




                sw.WriteLine();
                sw.WriteLine("  Provided T{0} bars @ {1:f0} mm c/c", bar_dia, spacing);

                bd1 = bd3 = bar_dia;
                sp1 = spacing * 2;
                sp3 = spacing;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(150);
                #endregion



                sw.WriteLine();
                sw.WriteLine("  Provided Ast = {0:f3} sq.mm", pro_st_ast);

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

                //percent = pro_st_ast * 100 / (1000 * deff);
                ////percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);


                sw.WriteLine("  Using Table 1, Given at end of this report.");
                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                percent = req_st_ast * 100 / (1000 * deff);

                tau_c = Get_Table_1_Value(percent, Con_Grade);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);

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
                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm", ast_2);

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

                _x = x * 10;
                //double _x = x / 100;
                _x = (int)(_x + 1);
                _x = _x * 100;


                sw.WriteLine();
                sw.WriteLine("  x = {0:f2} m = {1:f2} mm (say)", x, _x);
                sw.WriteLine();
                sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
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
                sw.WriteLine("  Midspan moments are calculated as:");
                sw.WriteLine("      {0:f2}, {1:f2} and {2:f2}", lst_Mad[0], lst_Mad[1], lst_Mad[2]);

                lst_Mad.Sort();

                M = lst_Mad[2];

                double eff_thickness = Math.Sqrt((M * 10e5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Effective Thickness of wall required = √((M * 10E5) / (1000 * R))");
                sw.WriteLine("                                       = √(({0:f2} * 10E5) / (1000 * {1:f3}))",
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

                req_st_ast = (M * 10E5) / (t * j * deff);

                sw.WriteLine();
                sw.WriteLine("  Required Steel = Ast = (M * 10E5) / (t * j * d)");
                sw.WriteLine("                       = ({0:f2} * 10E5) / ({1:f2} * {2:f3} * {3:f2})",
                    M, t, j, d);
                sw.WriteLine("                       = {0:f3} sq.mm", req_st_ast);

                sw.WriteLine();
                sw.WriteLine("  Provide T16 @300 mm c/c");
                pro_st_ast = (Math.PI * 16 * 16 / 4) * (1000.0 / 300.0);
                sw.WriteLine("  Provided Ast = {0:f0} sq.mm ", pro_st_ast);
                sw.WriteLine();

                bd2 = bd5 = 16;
                sp2 = sp5 = 300;


                #endregion

                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("TABLE 1");
                //sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_1(sw);
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
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
        public void InitializeData()
        {
            #region Variable Initialization

            H = MyList.StringToDouble(txt_H.Text, 0.0);
            b = MyList.StringToDouble(txt_b.Text, 0.0);
            d = MyList.StringToDouble(txt_d.Text, 0.0);
            d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
            gamma_b = MyList.StringToDouble(txt_gamma_b.Text, 0.0);
            gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            R = MyList.StringToDouble(txt_R.Text, 0.0);
            t = MyList.StringToDouble(txt_t.Text, 0.0);
            j = MyList.StringToDouble(txt_j.Text, 0.0);
            sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);
            cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            a1 = MyList.StringToDouble(txt_a1.Text, 0.0);
            w1 = MyList.StringToDouble(txt_w1.Text, 0.0);
            w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            b3 = MyList.StringToDouble(txt_b3.Text, 0.0);
            F = MyList.StringToDouble(txt_F.Text, 0.0);
            S = MyList.StringToDouble(txt_S.Text, 0.0);
            sbc = MyList.StringToDouble(txt_sbc.Text, 0.0);
            int grade = (int)sigma_c;

            Con_Grade = (CONCRETE_GRADE)grade;

            //switch (grade)
            //{
            //    case 15 :
            //        St_Grade = TAU_C.STEEL_GRADE.M15;
            //            break;
            //    case 20 :
            //        St_Grade = TAU_C.STEEL_GRADE.M20;
            //            break;
            //    case 25 :
            //        St_Grade = TAU_C.STEEL_GRADE.M25;
            //            break;
            //    case 30 :
            //        St_Grade = TAU_C.STEEL_GRADE.M30;
            //            break;
            //    case 35 :
            //        St_Grade = TAU_C.STEEL_GRADE.M35;
            //            break;
            //    case 40 :
            //        St_Grade = TAU_C.STEEL_GRADE.M40;
            //            break;
            //}

            #endregion

        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            Write_Drawing_Test();
            iApp.SetDrawingFile_Path(drawing_path, "Box_Culvert", "");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult fvr = new frmViewResult(rep_file_name);
            fvr.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
        }

        public void Write_Table_1(StreamWriter sw)
        {
            
            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            sw.WriteLine();
            sw.WriteLine("TABLE 1 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();
            lst_content.Clear();
        }

        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "RCC_BOX_STRUCTURE_DRAWING.FIL");
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
            }
        }
        public void Write_Drawing_Test()
        {
            drawing_path = Path.Combine(system_path, "RCC_BOX_STRUCTURE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            //string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, H);
            //string _box2 = "[" + _box1 + "]";
            //double _a = b * 1000.0;
            //double _b = d * 1000.0;
            //double _c = 0;
            //double _d = d1 * 1000.0;
            //double _e = d2 * 1000.0;
            //double _f = d3 * 1000.0;
            //double _h = H * 1000.0;
            //double _pressure = 130.43;
            //double _b1 = 16;
            //double _b2 = 16;
            //double _b3 = 16;
            //double _b4 = 0;
            //double _b5 = 10;
            //double _b6 = 16;
            //double _b7 = 16;
            //double _b8 = 16;
            //double _b9 = 10;
            //double _b10 = 10;
            //double _b11 = 10;
            //double _b12 = 10;
            //double _b13 = 10;
            //double _b14 = 10;
            //double _b15 = 16;
            //double _b16 = 10;
            //double _b17 = 10;
            //double _b18 = 10;
            //double _b19 = 10;
            //double _b20 = 10;
            //double _b21 = 12;
            //double _s1 = 210;
            //double _s2 = 300;
            //double _s3 = 130;
            //double _s4 = 0;
            //double _s5 = 250;
            //double _s6 = 300;
            //double _s7 = 300;
            //double _s8 = 300;
            //double _s9 = 300;
            //double _s10 = 300;
            //double _s11 = 250;
            //double _s12 = 250;
            //double _s13 = 250;
            //double _s14 = 250;
            //double _s15 = 250;
            //double _s16 = 0;
            //double _s17 = 0;
            //double _s18 = 150;
            //double _s19 = 0;
            //double _s20 = 150;
            //double _s21 = 0;
            #endregion

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
            double _pressure = 130.43;
            double _b1 = bd1;
            double _b2 = bd2;
            double _b3 = bd3;
            double _b4 = 0;
            double _b5 = bd5;
            double _b6 = bd6;
            double _b7 = bd7;
            double _b8 = bd8;
            double _b9 = bd9;
            double _b10 = bd10;
            double _b11 = bd11;
            double _b12 = bd12;
            double _b13 = bd13;
            double _b14 = bd14;
            double _b15 = 16;
            double _b16 = 10;
            double _b17 = 10;
            double _b18 = 10;
            double _b19 = 10;
            double _b20 = 10;
            double _b21 = 12;

            double _s1 = sp1;
            double _s2 = sp2;
            double _s3 = sp3;
            double _s4 = 0;
            double _s5 = sp5;
            double _s6 = sp6;
            double _s7 = sp7;
            double _s8 = sp8;
            double _s9 = sp9;
            double _s10 = sp10;
            double _s11 = sp11;
            double _s12 = sp12;
            double _s13 = sp13;
            double _s14 = sp14;
            double _s15 = sp15;
            double _s16 = 0;
            double _s17 = 0;
            double _s18 = 150;
            double _s19 = 0;
            double _s20 = 150;
            double _s21 = 0;
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
                sw.WriteLine("_b1={0:f0}", _b1);
                sw.WriteLine("_b2={0:f0}", _b2);
                sw.WriteLine("_b3={0:f0}", _b3);
                sw.WriteLine("_b4={0:f0}", _b4);
                sw.WriteLine("_b5={0:f0}", _b5);
                sw.WriteLine("_b6={0:f0}", _b6);
                sw.WriteLine("_b7={0:f0}", _b7);
                sw.WriteLine("_b8={0:f0}", _b8);
                sw.WriteLine("_b9={0:f0}", _b9);
                sw.WriteLine("_b10={0:f0}", _b10);
                sw.WriteLine("_b11={0:f0}", _b11);
                sw.WriteLine("_b12={0:f0}", _b12);
                sw.WriteLine("_b13={0:f0}", _b13);
                sw.WriteLine("_b14={0:f0}", _b14);
                sw.WriteLine("_b15={0:f0}", _b15);
                sw.WriteLine("_b16={0:f0}", _b16);
                sw.WriteLine("_b17={0:f0}", _b17);
                sw.WriteLine("_b18={0:f0}", _b18);
                sw.WriteLine("_b19={0:f0}", _b19);
                sw.WriteLine("_b20={0:f0}", _b20);
                sw.WriteLine("_b21={0:f0}", _b21);
                sw.WriteLine("_s1={0:f0}", _s1);
                sw.WriteLine("_s2={0:f0}", _s2);
                sw.WriteLine("_s3={0:f0}", _s3);
                sw.WriteLine("_s4={0:f0}", _s4);
                sw.WriteLine("_s5={0:f0}", _s5);
                sw.WriteLine("_s6={0:f0}", _s6);
                sw.WriteLine("_s7={0:f0}", _s7);
                sw.WriteLine("_s8={0:f0}", _s8);
                sw.WriteLine("_s9={0:f0}", _s9);
                sw.WriteLine("_s10={0:f0}", _s10);
                sw.WriteLine("_s11={0:f0}", _s11);
                sw.WriteLine("_s12={0:f0}", _s12);
                sw.WriteLine("_s13={0:f0}", _s13);
                sw.WriteLine("_s14={0:f0}", _s14);
                sw.WriteLine("_s15={0:f0}", _s15);
                sw.WriteLine("_s16={0:f0}", _s16);
                sw.WriteLine("_s17={0:f0}", _s17);
                sw.WriteLine("_s18={0:f0}", _s18);
                sw.WriteLine("_s19={0:f0}", _s19);
                sw.WriteLine("_s20={0:f0}", _s20);
                sw.WriteLine("_s21={0:f0}", _s21);

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
