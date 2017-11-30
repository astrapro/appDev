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
    public partial class frmRccWellFoundation : Form, IReport
    {
        string rep_file_name = "";
        string user_input_file = "";
        string drawing_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";
        bool is_process = false;
        IApplication iApp = null;


        #region Drawing Variable
        string _inner_dia = "";
        string _outer_dia = "";
        string _bars_A = "";
        string _bars_B = "";
        string _C = "";
        string _D = "";
        string _E1 = "";
        string _E2 = "";
        #endregion



        double Di, L, fck, fy,  K, D1, D2, Lc, Tc;
        int K_Indx = -1;

        public frmRccWellFoundation(IApplication app)
        {
            InitializeComponent();
            iApp = app;

            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, t)
        }

        #region Form Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
            Write_Drawing_File();
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
            iApp.SetDrawingFile_Path(drawing_file, "Well_Foundation", "Well_Foundation");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmb_K_SelectedIndexChanged(object sender, EventArgs e)
        {
            double d = 0.0;
            switch (cmb_K.SelectedIndex)
            {
                case 0:
                    d = 0.030;
                    break;
                case 1:
                    d = 0.033;
                    break;
                case 2:
                    d = 0.039;
                    break;
                case 3:
                    d = 0.043;
                    break;
            }
            txt_K.Text = d.ToString("0.000");
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
                sw.WriteLine("\t\t*       DESIGN OF RCC WELL FOUNDATION         *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Internal Diameter of Well [Di] = {0} m           Marked as (Di) in the Drawing", txt_di.Text);
                _inner_dia = Di.ToString() + " m";

                sw.WriteLine("Depth of Well below Bed Level [L] = {0} m        Marked as (L) in the Drawing", txt_L.Text);
                sw.WriteLine("Concrete Grade [fck] = {0} N/sq.mm", txt_fck.Text);
                sw.WriteLine("Steel Grade [fy] = {0} N/sq.mm", txt_fy.Text);
                sw.WriteLine("Diameter of Main Reinforcement Steel bars [D1] = {0} mm", txt_D1.Text);
                sw.WriteLine("Diameter of Main Hoop Steel bars [D2] = {0} mm", txt_D2.Text);
                sw.WriteLine("Depth of Curb [Lc] = {0} mm", txt_Lc.Text);
                sw.WriteLine("Thickness of Curb at bottom [Tc] = {0} mm        Marked as (Tc) in the Drawing", txt_Tc.Text);
                sw.WriteLine();
                sw.WriteLine("{0}  K = {1:f3}", cmb_K.Text, K);
                //sw.WriteLine("K = {0}", K);
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                #endregion

                #region STEP 1 : THICKNESS OF STEINING
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : THICKNESS OF STEINING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Minimum thickness of steining is given by");
                sw.WriteLine();
                sw.WriteLine("    h = K * (Di + 2*h)*√L");
                sw.WriteLine("Or, h = (K * Di * √L) + (K * 2*h * √L)");
                sw.WriteLine();
                sw.WriteLine("Or, h * (1 - K * 2 * √L) = (K * Di * √L) ");

                double h = ((K * Di * Math.Sqrt(L)) / ((1 - (K * 2 * Math.Sqrt(L)))));
                h = double.Parse(h.ToString("0.000"));
                sw.WriteLine("Or, h = ((K * Di * √L)/(1 - K * 2 * √L))");
                sw.WriteLine("Or, h = (({0} * {1} * √{2})/(1 - {0} * 2 * √{2}))", K, Di, L);
                sw.WriteLine();
                sw.WriteLine("Or, h = {0} m = {1} mm", h, (h * 1000));

                double Ts = (int)(h * 10.0);
                Ts = Ts * 100;

                if (Ts < 500)
                    Ts = 500;

                sw.WriteLine();
                sw.WriteLine("Adopt a steining of {0} mm", Ts);

                #endregion

                #region STEP 2 : REINFORCEMENT IN STEINING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : REINFORCEMENT IN STEINING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double De = (Di + (2 * (Ts / 1000.0)));
                De = double.Parse(De.ToString("0.000"));

                _outer_dia = string.Format("Di + 2 x {0} = {1} m", (Ts / 1000.0), De);

                sw.WriteLine("De = (Di + (2 * (Ts / 1000.0)))");
                sw.WriteLine("   = ({0} + (2 * ({1} / 1000.0)))", Di, Ts);
                sw.WriteLine("   = {0} m", De);
                sw.WriteLine();
               
                sw.WriteLine("For RCC wells, Minimum longitudinal reinforcement");
                sw.WriteLine();
                sw.WriteLine("Asc = 0.2% of gross cross sectional area");
                sw.WriteLine();


                 double Asc = (0.2 / 100) * ((Math.PI / 4.0) * (((Di + (2 * (Ts / 1000.0))) * (Di + (2 * (Ts / 1000.0)))) - (Di * Di))) * 10E5;
                Asc = double.Parse(Asc.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("   = (0.2/100)*[(π/4)*(De*De - Di*Di)]*10^6");
                sw.WriteLine("   = (0.2/100)*[(π/4)*({0}*{0} - {1}*{1})]*10^6", De, Di);
                sw.WriteLine();
                double Asc_by_2 = Asc / 2.0;
                Asc_by_2 = double.Parse(Asc_by_2.ToString("0"));
                sw.WriteLine("   = {0} sq.mm for both faces or {1} sq.mm for eachfaces.", Asc, Asc_by_2);
                sw.WriteLine();
                sw.WriteLine("Use {0:f0} mm diameter bars at 300 mm centres on both faces.      Marked as (A) in the Drawing", D1);

                // For drawing
                _bars_A = string.Format("Use {0:f0} mm diameter bars at 300 mm centres on both faces.", D1);

                sw.WriteLine();
                sw.WriteLine("Hoop reinforcement ≥ 0.04% of volume / unit length");

                double H_r = (0.04 / 100) * ((Math.PI / 4.0) * (De * De - Di * Di));
                H_r = double.Parse(H_r.ToString("0.0000000"));
                sw.WriteLine("                   ≥ (0.04/100)[(π/4)*(De*De - Di*Di)]");
                sw.WriteLine("                   ≥ (0.04/100)[(π/4)*({0}*{0} - {1}*{1})]", De, Di);
                sw.WriteLine("                   ≥ {0} cu.m/m", H_r);

                double H_r_1 = H_r * 7200.0;
                H_r_1 = double.Parse(H_r_1.ToString("0.000"));
                sw.WriteLine("                   ≥ {0} * 7200 kg/m", H_r);
                sw.WriteLine("                   ≥ {0} kg/m", H_r_1);

                sw.WriteLine();
                sw.WriteLine("Using {0} mm diameter bars", D2);
                sw.WriteLine();

                double avg_cir_hoop = Math.PI * 3.1;
                avg_cir_hoop = double.Parse(avg_cir_hoop.ToString("0.000"));


                double avg_dia = 3.1;
                sw.WriteLine("Using hoops of average diameter = {0} m", avg_dia);
                sw.WriteLine("Average circumference of the hoop = π * 3.1 = {0} m", avg_cir_hoop);
                //sw.WriteLine("                                  = {0} m", avg_cir_hoop);
                double wt_one_hoop = (0.62 * avg_cir_hoop);
                wt_one_hoop = double.Parse(wt_one_hoop.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Weight of one hoop = (0.62 * {0}) = {1} kg.", avg_cir_hoop, wt_one_hoop);
                sw.WriteLine();

                double no_hp_per_mtr = H_r_1 / wt_one_hoop;
                no_hp_per_mtr = double.Parse(no_hp_per_mtr.ToString("0.00"));
                sw.WriteLine("No of hoops per metre = ({0}/{1}) = {2}", H_r_1, wt_one_hoop, no_hp_per_mtr);
                sw.WriteLine();

                double spcng_hoop = (1000.0 / no_hp_per_mtr);
                spcng_hoop = double.Parse(spcng_hoop.ToString("0"));
                sw.WriteLine("So, Spacing of hoops = (1000/{0}) = {1} mm", no_hp_per_mtr, spcng_hoop);
                sw.WriteLine();
                sw.WriteLine("Use {0} diameter hoops at {1} mm centres on both faces.       Marked as (B) in the Drawing", D2, Ts);
                
                // For drawing
                _bars_B = string.Format("Use {0:f0} diameter hoops at {1} mm centres on both faces.", D2, Ts);

                sw.WriteLine();
              
                #endregion

                #region STEP 3 : WELL CURB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : WELL CURB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double min_renf = 72;
                sw.WriteLine("Minimum reinforcement = {0} kg/cu.m", min_renf);
                sw.WriteLine();
                sw.WriteLine("Proving a curb, {0} mm deep with a bottom width of {1} mm.", Lc, Tc);
                sw.WriteLine();

                double f12 = ((Ts / 1000) - 0.15);
                f12 = double.Parse(f12.ToString("0.000"));
                double f13 = (Di + 0.15);
                f13 = double.Parse(f13.ToString("0.000"));
                double vol_con_curb = ((Math.PI / 4.0) * (De * De - Di * Di)) 
                                        - (0.5 * f12 * 0.85 * Math.PI * f13);
                vol_con_curb = double.Parse(vol_con_curb.ToString("0.000"));

                sw.WriteLine("Volume of concrete in curb");
                sw.WriteLine("   = (π/4)*((De*De - Di*Di)) - (0.5 * ((Ts/1000) - 0.15)*0.85*(Di+0.15))");
                sw.WriteLine("   = (π/4)*(({0}*{0} - {1}*{1})) - (0.5 * (({2}/1000) - 0.15)*0.85*({1}+0.15))", De, Di, Ts);
                sw.WriteLine("   = {0} cu.m", vol_con_curb);
                sw.WriteLine();

                double tot_qnty_stl = min_renf * vol_con_curb;
                tot_qnty_stl = double.Parse(tot_qnty_stl.ToString("0"));


                // (C3/4)*(F9*F9-F10*F10)-(F7*F11*F12*C3*F13)
                sw.WriteLine("Total quantity of steel in Curb = {0} * {1} = {2} kg", min_renf, vol_con_curb, tot_qnty_stl);
                sw.WriteLine();
                sw.WriteLine("Using hoops of average diameter = {0} m", avg_dia);
                sw.WriteLine();
                sw.WriteLine("Weight of one hoop of 20 mm diameter = (π * 3.1 * 2.47) = 24 kg       Marked as (C) in the Drawing");

                double val1, val2;

                val1 = tot_qnty_stl / 2.0;

                val2 = val1 / 24.0;
                val2 = 1000.0 / val2;
                val2 = (int)((val2 / 10) + 1);
                val2 = (val2 * 10);

                //Weight of one hoop of 20 mm diameter  = (π * (Di+2x0.300) * 2.47) = (π * 3.1 * 2.47) = 24 kg, and Spacing = 160 mm c/c

                _C = string.Format("Weight of one hoop of 20 mm diameter = (π * (Di+2x0.300) * 2.47) = (π * 3.1 * 2.47) = 24 kg, and Spacing = {0:f0} mm c/c", val2);


                sw.WriteLine("Weight of one hoop of 16 mm diameter = (π * 3.1 * 1.58) = 15.38 kg   Marked as (D) in the Drawing");
                val2 = val1 / 15.38;
                val2 = 1000.0 / val2;
                val2 = (int)((val2 / 10) + 1);
                val2 = (val2 * 10);
                //Weight of one hoop of 16 mm diameter  = (π * (Di+2x0.300) * 1.58) = (π * 3.1 * 1.58) = 15.38 kg, and Spacing = 110 mm c/c
                _D = string.Format("Weight of one hoop of 16 mm diameter = (π * (Di+2x0.300) * 1.58) = (π * 3.1 * 1.58) = 15.38 kg, and Spacing={0:f0} mm c/c", val2);

                sw.WriteLine("Weight of one tie of 8 mm diameter 3 m long = (3 * 0.39) = 1.17 kg   Marked as (E) in the Drawing");
                //val2 = val1 / 15.38;
                _E1 = string.Format("Weight of one tie of 8 mm diameter 3 m long = (3 * 0.39) = 1.17 kg");

                sw.WriteLine();
                sw.WriteLine("Adopting a spacing of 300 mm for ties");
                _E2 = string.Format("Adopting a spacing of 300 mm for ties");
                
                sw.WriteLine();
                sw.WriteLine("Number of ties required = (π*3.1/0.3) = 33");
                sw.WriteLine();
                sw.WriteLine("Weight of ties = (33 * 1.17) = 39 kg");
                sw.WriteLine("Weight of 8 hoops of 20 mm diameter = (8 * 24) = 192 kg");
                sw.WriteLine("Weight of 6 hoops of 16 mm diameter = (6 * 15.38) = 92 kg");
                sw.WriteLine();
                if (tot_qnty_stl < 323)
                {
                    sw.WriteLine("Total quantity of steel provided in the Curb = (39 + 192 + 92)");
                    sw.WriteLine("                                             = 323 kg > {0} kg, OK", tot_qnty_stl);
                }
                else
                {
                    sw.WriteLine("Total quantity of steel provided in the Curb = (39 + 192 + 92)");
                    sw.WriteLine("                                             = 323 kg < {0} kg, NOT OK", tot_qnty_stl);
                }
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

                sw.WriteLine("di = {0}", Di);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine("fy = {0}", fy);
               
                sw.WriteLine("K_Indx = {0}", K_Indx);
                sw.WriteLine("K = {0:f3}", K);

                sw.WriteLine("D1 = {0}", D1);
                sw.WriteLine("D2 = {0}", D2);
                sw.WriteLine("Lc = {0}", Lc);
                sw.WriteLine("Tc = {0}", Tc);

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
                Di = MyList.StringToDouble(txt_di.Text, 0.0);
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                fy = MyList.StringToDouble(txt_fy.Text, 0.0);
                K_Indx = cmb_K.SelectedIndex;
                K = MyList.StringToDouble(txt_K.Text, 0.0);
                D1 = MyList.StringToDouble(txt_D1.Text, 0.0);
                D2 = MyList.StringToDouble(txt_D2.Text, 0.0);
                Lc = MyList.StringToDouble(txt_Lc.Text, 0.0);
                Tc = MyList.StringToDouble(txt_Tc.Text, 0.0);
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
                        case "di":
                            txt_di.Text = mList.StringList[1].Trim();
                            break;
                        case "L":
                            txt_L.Text = mList.StringList[1].Trim();
                            break;
                        case "fck":
                            txt_fck.Text = mList.StringList[1].Trim();
                            break;
                        case "fy":
                            txt_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "K":
                            txt_K.Text = mList.StringList[1].Trim();
                            break;
                        case "D1":
                            txt_D1.Text = mList.StringList[1].Trim();
                            break;
                        case "D2":
                            txt_D2.Text = mList.StringList[1].Trim();
                            break;
                        case "Lc":
                            txt_Lc.Text = mList.StringList[1].Trim();
                            break;
                        case "Tc":
                            txt_Tc.Text = mList.StringList[1].Trim();
                            break;
                        case "K_Indx":
                            K_Indx = mList.GetInt(1);
                            cmb_K.SelectedIndex = K_Indx;
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
            #endregion
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
            kPath = Path.Combine(kPath, "Bridge Foundation");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Well Foundation");

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
                this.Text = "DESIGN OF RCC WELL FOUNDATION : " + value;
                user_path = value;


                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_WELL_FOUNDATION");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Found_Well_Found.TXT");
                user_input_file = Path.Combine(system_path, "RCC_WELL_FOUNDATION.FIL");
                drawing_file = Path.Combine(system_path, "RCC_WELL_FOUNDATION_DRAWING.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDrawing.Enabled = File.Exists(rep_file_name);
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
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_inner_dia={0}", _inner_dia);
                sw.WriteLine("_outer_dia={0}",_outer_dia );
                sw.WriteLine("_bars_A={0}", _bars_A);
                sw.WriteLine("_bars_B={0}",_bars_B );
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E1={0}", _E1);
                sw.WriteLine("_E2={0}", _E2);
                sw.WriteLine("_L=L = {0} m", L);
                sw.WriteLine("_Tc=Tc = {0} m", (Tc / 1000.0));
            }
            catch(Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        #endregion

        private void frmRccWellFoundation_Load(object sender, EventArgs e)
        {
            cmb_K.SelectedIndex = 1;
        }
    }
}
