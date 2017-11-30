using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraFunctionOne.BridgeDesign.Foundation
{
    public partial class frmHydraulicCalculations : Form, IReport
    {
        const string Title = "HYDRAULIC CALCULATIONS";

        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        //string file_path = "";
        string system_path = "";
        bool is_process = false;
        IApplication iApp = null;


        double HFL, LWL, LBL, SDO, V1, S, n, F1, L, Ksb, F2;
        List<double> lst_dist = null;
        List<double> lst_reduce_lvl = null;
        double min_foundation_Level = 0.0;
        double Max_Scour_Depth = 0.0;
        double A, P;

        HR_Table_Collection HR_COL = null;
        public frmHydraulicCalculations(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            lst_reduce_lvl = new List<double>();
            lst_dist = new List<double>();


            FilePath = Path.Combine(iApp.LastDesignWorkingFolder, Title);
        }
        #region Form Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Calculate_Program();
            Write_User_Input();
            iApp.Save_Form_Record(this, user_path);
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(user_input_file, "HYDROGRAPH", "");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmHydraulicCalculations_Load(object sender, EventArgs e)
        {
            // Default Data
            if (dgvRiver.Rows.Count <= 1)
            {
                dgvRiver.Rows.Add(1, 126.933, 67.370);
                dgvRiver.Rows.Add(2, 132.933, 64.170);
                dgvRiver.Rows.Add(3, 138.933, 62.388);
                dgvRiver.Rows.Add(4, 145.433, 61.970);
                dgvRiver.Rows.Add(5, 151.933, 62.250);
                dgvRiver.Rows.Add(6, 157.933, 63.370);
                dgvRiver.Rows.Add(7, 163.933, 67.370);
            }
            Check_Grid();


            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    FilePath = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    iApp.Read_Form_Record(this, user_path);

                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option

        }
        private void dgvRiver_SizeChanged(object sender, EventArgs e)
        {
            Check_Grid();
        }
        private void dgvRiver_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Check_Grid();
        }
        #endregion
        #region IReport Members

        public void Calculate_Program()
        {
            frmCurve f_c = null;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22              *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*      DESIGN OF HYDRAULIC CALCULATIONS       *");
                sw.WriteLine("\t\t*         FOR RCC BRIDGE FOUNDATION           *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("High Flood Level [HFL] = {0} m", HFL);
                sw.WriteLine("Low Water Level [LWL] = {0} m", LWL);
                sw.WriteLine("Lowest Bed Level [LBL] = {0} m", LBL);
                sw.WriteLine("Scour Depth Observed [SDO] = {0} m", SDO);
                sw.WriteLine("Oberved Velocity [V1] = {0} m/sec", V1);
                sw.WriteLine("Slope of River [S] = {0}", S);
                sw.WriteLine("Value of n = {0}", n);
                sw.WriteLine("Factor for Discharge for Foundation Depth [F1] = {0}", F1);
                sw.WriteLine("Proposed Water way [L] = {0}", L);
                sw.WriteLine("Silt Factor [Ksb] = {0}", Ksb);
                sw.WriteLine("Factor for Foundation depth over Scour Depth [F2] = {0}", F2);
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                #endregion
                
               


                #region STEP 1
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                
                //sw.WriteLine("{0,5:f3} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",


                sw.WriteLine("{0,6:f3} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "SL. N", "Distance from", "Reduced", "Depth of", "Mean Depth", "Distance", "Diffence", "Sectional", "X*X", "Y*X", "X*X", "Wetted");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","Reference Point", "Levels", "Water", "of Water", "Between", "of Levels", "Area", "", "", "+ ", "Perimeter");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","to Various", "of the", "from HFL", "between", "two", "between", "Ai=Xi*Di", "", "", "Y*Y", "√(X*X+Y*Y)");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","Points across", "River", "to LBL", "two", " adjacent", "two", "(sq.m)", "", "", "", "(m)");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","cross Section", "Bed", "", "adjacent", "Points", " adjacent", "", "", "", "", "");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","at Bridge", "LBL", "", " Points", "= Xi (m)", "Points", "", "", "", "", "");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","Location", "(m)", "", "= Di (m)", "", "= Yi(m)", "", "", "", "", "");

                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","(m)", "", "", "", "", "", "", "", "", "", "");


                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                  "","A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K");
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                  "", "A", "B", "(HFL-B)", "(C1+C2)/2", "(A2-A1)", "(B1-B2)", "(E*F)", "(E*E)", "(F*F)", "(H+I)", "√J");
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
             

                sw.WriteLine();

                for (int i = 0; i < HR_COL.Count; i++)
                {
                    sw.WriteLine(HR_COL[i].ToString());
                }
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(HR_COL.ToString());
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
               
                #endregion

                #region STEP 2
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2  ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Area = A = {0} sq.m", A);
                sw.WriteLine();
                sw.WriteLine("Total Perimeter = P = {0} sq.m", P);
                sw.WriteLine();
                sw.WriteLine("Water spread at HFL between the banks");

                lst_dist.Sort();

                double water_spread = lst_dist[lst_dist.Count - 1] - lst_dist[0];
                water_spread = double.Parse(water_spread.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("    = {0} - {1}", lst_dist[lst_dist.Count - 1], lst_dist[0]);
                sw.WriteLine("    = {0:f3} m", water_spread);
                sw.WriteLine();

                double R = A / P;
                R = double.Parse(R.ToString("0.000"));

                sw.WriteLine("R = A / P = {0} / {1}", A, P);
                sw.WriteLine();

                double V2 = (1 / n) * Math.Pow(R, (2.0 / 3.0)) * Math.Pow(S, 0.5d);
                V2 = double.Parse(V2.ToString("0.000"));
                sw.WriteLine("Mannings Velocity = V2 = (1/n) * R**(2/3) * S**(1/2)");
                sw.WriteLine("                  = (1/{0}) * {1}^(2/3) * {2}^(1/2)", n, R, S);
                sw.WriteLine("                  = {0} m/sec", V2);
                sw.WriteLine();
                

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Discharge at Bridge Site");
                sw.WriteLine();

                double area_vel_mth = A * V1;
                area_vel_mth = double.Parse(area_vel_mth.ToString("0.000"));
                sw.WriteLine("Area Velocity method = A * V1");
                sw.WriteLine("                     = {0} * {1}", A, V1);
                sw.WriteLine("                     = {0} cu.m/sec", area_vel_mth);
                sw.WriteLine();

                double Manning_vel_mth = A * V2;
                Manning_vel_mth = double.Parse(Manning_vel_mth.ToString("0.000"));
                sw.WriteLine("Manning's Velocity method = A * V2");
                sw.WriteLine("                          = {0} * {1}", A, V2);
                sw.WriteLine("                          = {0} cu.m/sec", Manning_vel_mth);
                sw.WriteLine();

                double discharge_adopt = Math.Max(area_vel_mth, Manning_vel_mth);
                sw.WriteLine("Discharge Adopted for water way = Q = {0} cu.m/sec", discharge_adopt);
                sw.WriteLine();

                double foundation_dep = F1 * discharge_adopt;
                foundation_dep = double.Parse(foundation_dep.ToString("0.000"));
                sw.WriteLine("Discharge adopted for Foundation depth");
                sw.WriteLine();
                sw.WriteLine("           = F1 * {0}", discharge_adopt);
                sw.WriteLine("           = {0} * {1}", F1, discharge_adopt);
                sw.WriteLine("           = {0} cu.m/sec", foundation_dep);
                sw.WriteLine();

                double Lw = 4.8 * Math.Sqrt(discharge_adopt);
                Lw = double.Parse(Lw.ToString("0.000"));
                sw.WriteLine("Lacy's with = Lw = 4.8 * √Q");
                sw.WriteLine("            = 4.8 * √{0:f3}", discharge_adopt);
                sw.WriteLine("            = {0:f3} m", Lw);
                sw.WriteLine();

                
                sw.WriteLine("Water spread at H.F.L between Banks = {0:f3} m", water_spread);
                sw.WriteLine();
                sw.WriteLine("Proposed Waterway = L = {0:f3} m", L);
                sw.WriteLine();

                double Db = foundation_dep / L;
                Db = double.Parse(Db.ToString("0.000"));
                sw.WriteLine("Discharge per metre width = Db = {0} / {1}", foundation_dep, L);
                sw.WriteLine("                          = {0} cu.m/sec/m", Db);
                sw.WriteLine();

                double d2 = 1.34 * Math.Pow(((Db * Db) / Ksb), (1.0 / 3.0));
                d2 = double.Parse(d2.ToString("0.000"));
                sw.WriteLine("Mean Scour Depth = d2 = 1.34 * ((Db*Db)/Ksb)^(1/3)");
                sw.WriteLine("                 = 1.34 * (({0}*{0})/{1})^(1/3)", Db, Ksb);
                sw.WriteLine("                 = {0} m", d2);
                sw.WriteLine();

                double D = 1.272 * d2;
                D = double.Parse(D.ToString("0.000"));
                Max_Scour_Depth = D;
                sw.WriteLine("Maximum Scour Depth adopted = D = 1.272 * d2");
                sw.WriteLine("                            = 1.272 * {0}", d2);
                sw.WriteLine("                            = {0} m", D);
                sw.WriteLine();
               
                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double min_deg_fond_dep = F2 * D;
                min_deg_fond_dep = double.Parse(min_deg_fond_dep.ToString("0.000"));
                sw.WriteLine("Minimum design Foundation depth = F2 * D");
                sw.WriteLine("                                = {0} * {1}", F2, D);
                sw.WriteLine("                                = {0} m", min_deg_fond_dep);
                sw.WriteLine();

                double min_deg_Fond_Lvl = HFL - min_deg_fond_dep;
                min_deg_Fond_Lvl = double.Parse(min_deg_Fond_Lvl.ToString("0.000"));
                sw.WriteLine("Minimum design Foundation Level = HFL - {0}", min_deg_fond_dep);
                sw.WriteLine("                                = {0} - {1}", HFL, min_deg_fond_dep);
                sw.WriteLine("                                = {0} m", min_deg_Fond_Lvl);
                sw.WriteLine();

                double dep = LWL - min_deg_Fond_Lvl;
                dep = double.Parse(dep.ToString("0.000"));
                sw.WriteLine("Depth from LWL to minimum Foundation Level");
                sw.WriteLine("            = LWL - {0}", min_deg_Fond_Lvl);
                sw.WriteLine("            = {0} - {1}", LWL, min_deg_Fond_Lvl);
                sw.WriteLine("            = {0} m", dep);
                sw.WriteLine();

                dep = LBL - min_deg_Fond_Lvl;
                dep = double.Parse(dep.ToString("0.000"));
                min_foundation_Level = dep;
                sw.WriteLine("Depth from LBL to minimum Foundation Level");
                sw.WriteLine("            = LBL - {0}", min_deg_Fond_Lvl);
                sw.WriteLine("            = {0} - {1}", LBL, min_deg_Fond_Lvl);
                sw.WriteLine("            = {0} m", dep);
                sw.WriteLine();
                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("HFL = {0}", HFL);
                sw.WriteLine("LWL = {0}", LWL);
                sw.WriteLine("LBL = {0}", LBL);
                sw.WriteLine("SDO = {0}", SDO);
                sw.WriteLine("V1 = {0}", V1);
                sw.WriteLine("S = {0}", S);
                sw.WriteLine("n = {0}", n);
                sw.WriteLine("F1 = {0}", F1);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("Ksb = {0}", Ksb);
                sw.WriteLine("F2 = {0}", F2);
                sw.WriteLine("MIN_FOUNDATION_LEVEL = {0}", min_foundation_Level);
                sw.WriteLine("MAXIMUM_SCOUR_DEPTH = {0}", Max_Scour_Depth);

                sw.WriteLine("DISTANCE REDUCE_LEVEL");
                for (int i = 0; i < lst_dist.Count; i++)
                {
                    sw.WriteLine("{0:f3}   {1:f3}", lst_dist[i], lst_reduce_lvl[i]);
                }

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
            #region USER DATA INPUT
            try
            {
                HFL = MyList.StringToDouble(txt_HFL.Text, 0.0);
                LWL = MyList.StringToDouble(txt_LWL.Text, 0.0);
                LBL = MyList.StringToDouble(txt_LBL.Text, 0.0);
                SDO = MyList.StringToDouble(txt_SDO.Text, 0.0);
                V1 = MyList.StringToDouble(txt_V1.Text, 0.0);
                S = MyList.StringToDouble(txt_S.Text, 0.0);
                n = MyList.StringToDouble(txt_n.Text, 0.0);
                F1 = MyList.StringToDouble(txt_F1.Text, 0.0);
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                Ksb = MyList.StringToDouble(txt_Ksb.Text, 0.0);
                F2 = MyList.StringToDouble(txt_F2.Text, 0.0);
                lst_dist.Clear();
                lst_reduce_lvl.Clear();

                double d = 0d;
                for (int i = 0; i < dgvRiver.RowCount - 1; i++)
                {
                    if (dgvRiver[1, i].Value != null &&
                        double.TryParse(dgvRiver[1, i].Value.ToString(), out d))
                    {
                        lst_dist.Add(d);
                    }

                    if (dgvRiver[2, i].Value != null &&
                                            double.TryParse(dgvRiver[2, i].Value.ToString(), out d))
                    {
                        lst_reduce_lvl.Add(d);
                    }
                }

                HR_COL = new HR_Table_Collection(HFL, lst_dist[0], lst_reduce_lvl[0]);

                for (int i = 1; i < lst_dist.Count; i++)
                {
                    HR_COL.Add(lst_dist[i], lst_reduce_lvl[i]);
                }
                A = HR_COL.A;
                P = HR_COL.P;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        public void Read_User_Input()
        {
            #region USER DATA INPUT DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            int indx = -1;

            bool chk = false;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);

                    if (VarName.ToUpper() == "DISTANCE REDUCE_LEVEL")
                    {
                        VarName = "DISTANCE REDUCE_LEVEL";
                        dgvRiver.Rows.Clear();
                        chk = true;
                        continue;
                    }

                    if (chk)
                    {
                        VarName = "DISTANCE REDUCE_LEVEL";
                        mList = new MyList(kStr, ' ');

                    }
                    #region SWITCH
                    switch (VarName)
                    {
                        case "HFL":
                            txt_HFL.Text = mList.StringList[1].Trim();
                            break;
                        case "LWL":
                            txt_LWL.Text = mList.StringList[1].Trim();
                            break;
                        case "LBL":
                            txt_LBL.Text = mList.StringList[1].Trim();
                            break;
                        case "SDO":
                            txt_SDO.Text = mList.StringList[1].Trim();
                            break;
                        case "V1":
                            txt_V1.Text = mList.StringList[1].Trim();
                            break;
                        case "S":
                            txt_S.Text = mList.StringList[1].Trim();
                            break;
                        case "n":
                            txt_n.Text = mList.StringList[1].Trim();
                            break;
                        case "F1":
                            txt_F1.Text = mList.StringList[1].Trim();
                            break;
                        case "L":
                            txt_L.Text = mList.StringList[1].Trim();
                            break;
                        case "Ksb":
                            txt_Ksb.Text = mList.StringList[1].Trim();
                            break;
                        case "F2":
                            txt_F2.Text = mList.StringList[1].Trim();
                            break;
                        case "DISTANCE REDUCE_LEVEL":
                            dgvRiver.Rows.Add(dgvRiver.Rows.Count + 1, mList.GetDouble(0), mList.GetDouble(1));
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            chk = false;
            Check_Grid();
            lst_content.Clear();
            #endregion
        }

        public string FilePath
        {
            set
            {
                this.Text = Title + " : " + value;

                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


                if (Directory.Exists(user_path) == false)
                    Directory.CreateDirectory(user_path);

                system_path = Path.Combine(user_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(user_path, "HYDRAULIC_CALCULATIONS_REPORT.TXT");
                user_input_file = Path.Combine(system_path, "HYDRAULIC_CALCULATIONS.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDrawing.Enabled = File.Exists(user_input_file);
                btnProcess.Enabled = Directory.Exists(value);
                //if (File.Exists(user_input_file) && !is_process)
                //{
                //    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                //    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                //    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                //        Read_User_Input();
                //}

            }
        }

        public void Check_Grid()
        {
            double d = 0d;
            LBL = 0.0;
            HFL = 0.0;
            try
            {
                if (!double.TryParse(dgvRiver[2, 0].Value.ToString(), out LBL))
                    d = 0d;
            }
            catch (Exception ex) { }

            for (int i = 0; i < dgvRiver.Rows.Count - 1; i++)
            {
                dgvRiver[0, i].Value = (i + 1);

                if (dgvRiver[1, i].Value != null)
                {
                    if (!double.TryParse(dgvRiver[1, i].Value.ToString(), out d))
                        d = 0;
                }
                else
                {
                    d = 0;
                }
                dgvRiver[1, i].Value = d.ToString("0.000");


                if (dgvRiver[2, i].Value != null)
                {
                    if (!double.TryParse(dgvRiver[2, i].Value.ToString(), out d))
                        d = 0;
                }
                else
                {
                    d = 0;
                }
                if (d < LBL)
                    LBL = d;

                if (d > HFL)
                    HFL = d;

                dgvRiver[2, i].Value = d.ToString("0.000");
            }

            txt_LBL.Text = LBL.ToString("0.000");
            txt_HFL.Text = HFL.ToString("0.000");

            int wid = dgvRiver.Width - dgvRiver.RowHeadersWidth - SL_N.Width;
            wid -= 19;
            dist.Width = wid / 2;
            reduce.Width = wid / 2;
        }
        #endregion
    }
    class HR_Table
    {

        public int _0_SL_No = -1;
        public double _1_Distance = 0.0d;
        public double _2_LBL = 0.0d;
        public double _3_HFL_to_LBL = 0.0d;
        public double _4_Di = 0.0d;
        public double _5_Xi = 0.0d;
        public double _6_Yi = 0.0d;
        public double _7_Ai = 0.0d; // Area
        public double _8_X_sq = 0.0d; // X square
        public double _9_Y_sq = 0.0d; // Y square
        public double _10_X_sq_Y_sq = 0.0d; // X square + Y square
        public double _11_rt_X_sq_Y_sq = 0.0d; // Root X square + Y square

        public HR_Table()
        {
            _1_Distance = 0.0d;
            _2_LBL = 0.0d;
            _3_HFL_to_LBL = 0.0d;
            _4_Di = 0.0d;
            _5_Xi = 0.0d;
            _6_Yi = 0.0d;
            _7_Ai = 0.0d; // Area
            _8_X_sq = 0.0d; // X square
            _9_Y_sq = 0.0d; // Y square
            _10_X_sq_Y_sq = 0.0d; // X square + Y square
            _11_rt_X_sq_Y_sq = 0.0d; // Root X square + Y square
        }
        public HR_Table(double distance, double reduce_level)
        {
            _1_Distance = distance;
            _2_LBL = reduce_level;
            _3_HFL_to_LBL = 0.0d;
            _4_Di = 0.0d;
            _5_Xi = 0.0d;
            _6_Yi = 0.0d;
            _7_Ai = 0.0d; // Area
            _8_X_sq = 0.0d; // X square
            _9_Y_sq = 0.0d; // Y square
            _10_X_sq_Y_sq = 0.0d; // X square + Y square
            _11_rt_X_sq_Y_sq = 0.0d; // Root X square + Y square
        }

        public override string ToString()
        {
            //return (string.Format("{0,8:f3} {1,7:f3} {2,7:f3} {3,7:f3} {4,7:f3} {5,7:f3} {6,7:f3} {7,7:f3} {8,7:f3} {9,7:f3} {10,7:f3}",
            //    _1_Distance,
            //    _2_LBL,
            //    _3_HFL_to_LBL,
            //    _4_Di,
            //    _5_Xi,
            //    _6_Yi,
            //    _7_Ai,
            //    _8_X_sq,
            //    _9_Y_sq,
            //    _10_X_sq_Y_sq,
            //    _11_rt_X_sq_Y_sq));


            return (string.Format("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
              _0_SL_No,
              _1_Distance,
              _2_LBL,
              _3_HFL_to_LBL,
              _4_Di,
              _5_Xi,
              _6_Yi,
              _7_Ai,
              _8_X_sq,
              _9_Y_sq,
              _10_X_sq_Y_sq,
              _11_rt_X_sq_Y_sq));


        }
    }
    class HR_Table_Collection : IList<HR_Table>
    {
        List<HR_Table> list = null;
        double _HFL = 0.0;
        public HR_Table_Collection(double HFL, double distance, double reduce_level)
        {
            HR_Table hr_t = new HR_Table();
            hr_t._0_SL_No = 1;
            hr_t._1_Distance = distance;
            hr_t._2_LBL = reduce_level;
            hr_t._3_HFL_to_LBL = HFL - reduce_level;

            list = new List<HR_Table>();
            list.Add(hr_t);
            _HFL = HFL;

        }

        #region IList<HR_Table> Members

        public int IndexOf(HR_Table item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i]._1_Distance == item._1_Distance && list[i]._2_LBL == item._2_LBL)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, HR_Table item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public HR_Table this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion
        
        #region ICollection<HR_Table> Members

        public void Add(HR_Table item)
        {
            int indx = -1;
            if (list.Count > 0)
            {
                indx = list.Count -1;

                item._0_SL_No = list.Count+1;
                item._3_HFL_to_LBL = _HFL - item._2_LBL;

                item._3_HFL_to_LBL = double.Parse(item._3_HFL_to_LBL.ToString("0.000"));


                item._4_Di = (list[indx]._3_HFL_to_LBL + item._3_HFL_to_LBL) / 2.0;
                item._4_Di = double.Parse(item._4_Di.ToString("0.000"));

                item._5_Xi = item._1_Distance - list[indx]._1_Distance;
                item._5_Xi = double.Parse(item._5_Xi.ToString("0.000"));

                item._6_Yi = list[indx]._2_LBL - item._2_LBL;
                item._6_Yi = double.Parse(item._6_Yi.ToString("0.000"));

                item._7_Ai = item._5_Xi * item._4_Di;
                item._7_Ai = double.Parse(item._7_Ai.ToString("0.000"));

                
                item._8_X_sq = item._5_Xi * item._5_Xi;
                item._8_X_sq = double.Parse(item._8_X_sq.ToString("0.000"));


                item._9_Y_sq = item._6_Yi * item._6_Yi;
                item._9_Y_sq = double.Parse(item._9_Y_sq.ToString("0.000"));

                item._10_X_sq_Y_sq = item._8_X_sq + item._9_Y_sq;
                item._10_X_sq_Y_sq = double.Parse(item._10_X_sq_Y_sq.ToString("0.000"));

                item._11_rt_X_sq_Y_sq = Math.Sqrt(item._10_X_sq_Y_sq);
                item._11_rt_X_sq_Y_sq = double.Parse(item._11_rt_X_sq_Y_sq.ToString("0.000"));
            }
            list.Add(item);
        }
        public void Add(double distance, double reduce_level)
        {
            HR_Table hr_t = new HR_Table(distance, reduce_level);
            Add(hr_t);
        }
        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(HR_Table item)
        {
            return (IndexOf(item) == -1) ? false : true;
        }

        public void CopyTo(HR_Table[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(HR_Table item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                list.RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<HR_Table> Members

        public IEnumerator<HR_Table> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
            
        }

        #endregion

        public double P
        {
            get
            {
                double p = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    p += list[i]._11_rt_X_sq_Y_sq;
                }

                p = double.Parse(p.ToString("0.000"));
                return p;
            }
        }
        public double A
        {
            get
            {
                double a = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    a += list[i]._7_Ai;
                }
                a = double.Parse(a.ToString("0.000"));
                return a;
            }
        }

        public override string ToString()
        {
            //return (string.Format("{0,8:f3} {1,7:f3} {2,7:f3} {3,7:f3} {4,7:f3} {5,7:f3} {6,7:f3} {7,7:f3} {8,7:f3} {9,7:f3} {10,7:f3}",
            //    "", "", "", "", "", "A =", A, "", "", "P =", P));
            return (string.Format("{0,6} {1,15:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3}",
                "","", "", "", "", "", "A =", A, "", "", "P =", P));
        }
    }
}
