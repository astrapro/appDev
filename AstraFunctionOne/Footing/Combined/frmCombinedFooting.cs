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
using AstraFunctionOne.BeamDesign;
//using AstraFunctionOne.Footing.Combined;

namespace AstraFunctionOne.Footing
{
    public partial class frmCombinedFooting : Form
    {
        #region File Manage Variable
        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";

        bool is_process = false;
        IApplication iApp = null;
        #endregion


        double H, F, S, N1, N2, A, B, C;
        string ABC = "";
        string kStr = "";

        double N, AB, A1, F1, S1, S2, S3;

        bool Is_Rectangle_Footing = false;

        public frmCombinedFooting(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        #region Form Events
        private void cmb_FootingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FootingType.SelectedIndex == 1)
            {
                txt_ABC.Enabled = true;
                txt_A.Enabled = false;
                txt_C.Enabled = false;
            }
            else
            {
                txt_ABC.Enabled = false;
                txt_A.Enabled = true;
                txt_C.Enabled = true;
            }
        }
        private void frmCombinedFooting_Load(object sender, EventArgs e)
        {
            cmb_FootingType.SelectedIndex = 0;

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            if (Calculate_Program())
                if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            else
                return;
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
        private void btnDescription_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(Application.StartupPath, "Column and Footing\\Footing_Combined.pdf");
            if (!File.Exists(fName))
                fName = Path.Combine(Application.StartupPath, "ASTRAHelp\\Footing_Combined.pdf");
            try
            {
                System.Diagnostics.Process.Start(fName);
            }
            catch
            {
            }

            //frmReport_Viewer f_r_v = new frmReport_Viewer();
            //f_r_v.Report_Viewer.ReportSource = new crtCombinedFooting();
            //f_r_v.Text = "COMBINED FOOTING DESIGN DESCRIPTION";
            //f_r_v.ShowDialog();
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region IReport Members

        public bool Calculate_Program()
        {
            bool success = false;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*         DESIGN OF COMBINED FOUNDATION       *");
                sw.WriteLine("\t\t*                                             *");
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
                sw.WriteLine();
                sw.WriteLine("ESTIMATED BASE DEPTH [H] = {0} m", txt_H.Text);
                sw.WriteLine("ALLOWABLE BEARING PRESSURE [F] = {0} m", txt_F.Text);
                sw.WriteLine("SPACING OF COLUMN [S] = {0} m", txt_S.Text);
                sw.WriteLine("AXIAL LOADING FOR COLUMN 1 [N1] = {0} kN/sq.mm", txt_N1.Text);
                sw.WriteLine("AXIAL LOADING FOR COLUMN 2 [N2] = {0} kN/sq.mm", txt_N2.Text);
                //if (Is_Rectangle_Footing)
                //{
                sw.WriteLine("TRIAL WIDTH OF FOOTING AT ONE END [A] = {0} m", A);
                sw.WriteLine("TRIAL WIDTH OF FOOTING AT OTHER END [B] = {0} m", B);
                sw.WriteLine("TRIAL LENGTH [C] = {0} m", C);
                //}
                //else
                //{
                //    sw.WriteLine("TRIAL DIMENSIONS [A, B, C] = {0} m", txt_ABC.Text);
                //}


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                #endregion



                N = N1 + N2;
                sw.WriteLine();
                sw.WriteLine("N = N1 + N2 = {0} + {1} = {2} kN/sq.mm", N1, N2, N);
                AB = N / (F - 24 * H);
                sw.WriteLine();
                sw.WriteLine("Minimum Base Area required");
                sw.WriteLine();
                sw.WriteLine("AB = N / (F - 24 * H)");
                sw.WriteLine("   = {0} / ({1} - 24 * {2})", N, F, H);
                sw.WriteLine("   = {0:f2} sq.m", AB);
                if (C < (AB / A))
                {
                    kStr = "C < (AB / A)\n";
                    kStr += string.Format("{0} < {1}\n", C, (AB/A));
                    kStr += "CHOSEN LENGTH TOO SMALL";

                    MessageBox.Show(kStr, "ASTRA", MessageBoxButtons.OK);
                    txt_C.Focus();
                    throw new Exception("");
                    
                }
                goto _310;
            _310:
                A1 = 0.5 * (A + B) * C;
                sw.WriteLine();
                sw.WriteLine("A1 = 0.5 * (A + B) * C");
                sw.WriteLine("   = 0.5 * ({0} + {1}) * {2}", A, B, C);
                sw.WriteLine("   = {0:f2} m", A1);
                if (A1 < AB)
                {
                    // Disp "AREA TO SMALL, INPUT ANOTHER SIZE"
                    kStr = "A1 < AB\n";
                    kStr += string.Format("{0} < {1}\n", A1, AB);
                    kStr += "AREA TO SMALL, INPUT ANOTHER SIZE";
                    MessageBox.Show(kStr, "ASTRA", MessageBoxButtons.OK);
                    txt_ABC.Focus();
                    throw new Exception("");
                }

                F1 = (N + 24 * H * A1) / A1;
                sw.WriteLine();
                sw.WriteLine("BASE STRESSESS :");
                sw.WriteLine();
                sw.WriteLine("F1 = (N + 24 * H * A1) / A1");
                sw.WriteLine("   = ({0} + 24 * {1} * {2}) / {2}", N, H, A1);
                sw.WriteLine("   = {0:f2} kN/sq.mm", F1);
                // Calculate base stressess

                // Maximum Stress
                // Do you wish to try another base size?

                // Calculate position of centroid of section

                S1 = (A * C * C / 2 + (B - A) * C * C / 6.0) / ((A + B) * C / 2);
                sw.WriteLine();
                sw.WriteLine("Position of centroid of section");
                sw.WriteLine();
                sw.WriteLine("S1 = (A * C * C / 2 + (B - A) * C * C / 6.0)");
                sw.WriteLine("      / ((A + B) * C / 2)");
                sw.WriteLine();
                sw.WriteLine("   = ({0} * {1} * {1} / 2 + ({2} - {0}) * {1} * {1} / 6.0)", A, C, B);
                sw.WriteLine("      / (({0} + {1}) * {2} / 2)", A, B, C);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} m", S1);

                S2 = N1 * S / (N1 + N2);
                sw.WriteLine();
                sw.WriteLine("S2 = N1 * S / (N1 + N2)");
                sw.WriteLine("   = {0} * {1} / ({0} + {2})", N1, S, N2);
                sw.WriteLine("   = {0:f2} m", S2);
                S3 = S1 - S2;
                sw.WriteLine();
                sw.WriteLine("S3 = S1 - S2 = {0:f2} - {1:f2} = {2:f2} m", S1, S2, (S1 - S2));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Maximum Stress = F1 = {0:f2} kN/sq.mm", F1);
                sw.WriteLine();
                sw.WriteLine("SELECTED BASE DIMENSIONS");
                sw.WriteLine("A = {0} m,  B = {1} m,  C = {2} m", A, B, C);
                sw.WriteLine("Distance of Base End to Column 2 = {0:f2} m", S3);
                sw.WriteLine();
                
                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion
                success = true;
            }
            catch (Exception ex) { success = false; }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            return success;
        }

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("H = {0}", H);
                sw.WriteLine("F = {0}", F);
                sw.WriteLine("S = {0}", S);
                sw.WriteLine("N1 = {0}", N1);
                sw.WriteLine("N2 = {0}", N2);
                sw.WriteLine("ABC = {0}", ABC);
                sw.WriteLine("A = {0}", A);
                sw.WriteLine("C = {0}", C);
                sw.WriteLine("INDX = {0}", cmb_FootingType.SelectedIndex);

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

                H = MyList.StringToDouble(txt_H.Text, 0.0);
                F = MyList.StringToDouble(txt_F.Text, 0.0);
                S = MyList.StringToDouble(txt_S.Text, 0.0);
                N1 = MyList.StringToDouble(txt_N1.Text, 0.0);
                N2 = MyList.StringToDouble(txt_N2.Text, 0.0);
                A = MyList.StringToDouble(txt_A.Text, 0.0);
                //B = MyList.StringToDouble(txt_B.Text, 0.0);
                C = MyList.StringToDouble(txt_C.Text, 0.0);
                Is_Rectangle_Footing = (cmb_FootingType.SelectedIndex == 0);


                if (Is_Rectangle_Footing)
                {
                    B = A;
                }
                else
                {
                    ABC = txt_ABC.Text;
                    MyList mList = new MyList(ABC, ',');
                    if (mList.Count == 3)
                    {
                        A = mList.GetDouble(0);
                        B = mList.GetDouble(1);
                        C = mList.GetDouble(2);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
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
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    /**/
                    #region SWITCH
                    switch (VarName)
                    {
                        case "H":
                            txt_H.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "F":
                            txt_F.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "S":
                            txt_S.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "N1":
                            txt_N1.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "N2":
                            txt_N2.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "A":
                            txt_A.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "ABC":
                            txt_ABC.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "C":
                            txt_C.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "INDX":
                            cmb_FootingType.SelectedIndex = mList.GetInt(1);
                            break;
                    }
                    #endregion
                    /**/
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Structural Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "RCC Foundation");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Combined Foundation");
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
                this.Text = "DESIGN OF COMBINED FOUNDATION : " + value;
                user_path = value;
                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "COMBINED_FOOTING");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Combined Foundation.TXT");
                user_input_file = Path.Combine(system_path, "COMBINED_FOOTING.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDescription.Enabled = File.Exists(rep_file_name);
                btnProcess.Enabled = Directory.Exists(value);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }
            }
        }
        /**/
        #endregion
    }
}
