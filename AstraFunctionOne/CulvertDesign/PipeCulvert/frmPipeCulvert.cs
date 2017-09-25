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
namespace AstraFunctionOne.CulvertDesign.PipeCulvert
{
    public partial class frmPipeCulvert : Form
    {
        #region Variable Declaration
        string rep_file_name = "";
        string file_path = "";
        string user_input_file = "";
        string system_path = "";
        string user_path = "";
        bool is_process = false;

        IApplication iApp = null;

        double Q, V, B, H1, H2, W, I;
        bool IsHighway = true;

        #endregion
        //string ref_string = "";

        public frmPipeCulvert(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
            Q = 0.0;
            V = 0.0;
            B = 0.0;
            H1 = 0.0;
            H2 = 0.0;
            W = 0.0;
            I = 0.0;
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
            kPath = Path.Combine(kPath, "RCC Culverts");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Pipe Culvert");

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
                this.Text = "DESIGN OF PIPE CULVERT : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "PIPE_CULVERT");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "DESIGN_OF_PIPE_CULVERT.TXT");
                user_input_file = Path.Combine(system_path, "PIPE_CULVERT.FIL");




                btnProcess.Enabled = Directory.Exists(value);
                btnDrawing.Enabled  = File.Exists(user_input_file);
                btnReport.Enabled = File.Exists(user_input_file);


                if (File.Exists(user_input_file) && !is_process)
                {
                    //string msg = "The folder already contains Previous Design. Overwrite?";
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_From_File();
                }
            }
        }
        public void Read_From_File()
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
                            Q = mList.GetDouble(1);
                            txt_Q.Text = Q.ToString();
                            break;
                        case "V":
                            V = mList.GetDouble(1);
                            txt_V.Text = V.ToString();
                            break;
                        case "B":
                            B = mList.GetDouble(1);
                            txt_B.Text = B.ToString();
                            break;
                        case "H1":
                            H1 = mList.GetDouble(1);
                            txt_H1.Text = H1.ToString();
                            break;
                        case "H2":
                            H2 = mList.GetDouble(1);
                            txt_H2.Text = H2.ToString();
                            break;
                        case "W":
                            W = mList.GetDouble(1);
                            txt_W.Text = W.ToString();
                            break;
                        case "I":
                            I = mList.GetDouble(1);
                            txt_I.Text = I.ToString();
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
        

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (fbd.SelectedPath != user_path)
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Calculate_Program();
            WriteUserInput();
            is_process = true;
            FilePath = user_path;
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Calculate_Program()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
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
                sw.WriteLine("                = {0:f3} m",d);

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
                string ref_str = "";
                double earthfill_load = iApp.Tables.Embankment_Loading(d * 1000, H, ref outer_dia, ref ref_str);

                sw.WriteLine("  Adopt NP-3 RCC Heavy Duty non pressure Pipe for carring heavy road traffic.");
                sw.WriteLine("  Ref. IS:458-1971, For Pipe of Internal Diameter = {0:f2} m", d);
                sw.WriteLine("                                   Outer Diameter = {0:f2} m", (outer_dia / 1000.0));

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
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Load by Vehicle Wheel");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load by Vehicle Wheel Load = W = {0:f2} kN", W);

                // TO DO
                sw.WriteLine("  From Table 2 (given at end of this Report)");


                string ref_string = "";
                double Cs = 0.0;
                if (IsHighway)
                    Cs = iApp.Tables.Influence_Coefficient_Highway(d * 1000, H, ref outer_dia, ref ref_string);
                else
                    Cs = iApp.Tables.Influence_Coefficient_Railway(H, ref ref_string);

                //double Cs = 0.032;

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

                sw.WriteLine("  Ref. IS:458:1971 code");

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
                sw.WriteLine("Refering to IS:485-1971,");
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
     
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        void InitializeData()
        {
            Q = MyList.StringToDouble(txt_Q.Text, 0.0);
            V = MyList.StringToDouble(txt_V.Text, 0.0);
            B = MyList.StringToDouble(txt_B.Text, 0.0);
            H1 = MyList.StringToDouble(txt_H1.Text, 0.0);
            H2 = MyList.StringToDouble(txt_H2.Text, 0.0);
            W = MyList.StringToDouble(txt_W.Text, 0.0);
            I = MyList.StringToDouble(txt_I.Text, 0.0);
        }
        void WriteUserInput()
        {
            Q = MyList.StringToDouble(txt_Q.Text, 0.0);
            V = MyList.StringToDouble(txt_V.Text, 0.0);
            B = MyList.StringToDouble(txt_B.Text, 0.0);
            H1 = MyList.StringToDouble(txt_H1.Text, 0.0);
            H2 = MyList.StringToDouble(txt_H2.Text, 0.0);
            W = MyList.StringToDouble(txt_W.Text, 0.0);
            I = MyList.StringToDouble(txt_I.Text, 0.0);

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

        private void frmPipeCulvert_Load(object sender, EventArgs e)
        {

        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(rep_file_name, "", "Pipe_Culvert");
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

        private void rbtnHighway_CheckedChanged(object sender, EventArgs e)
        {
            IsHighway = rbtnHighway.Checked;
        }

        public void Write_Tables(StreamWriter sw)
        {
            string file_name = Path.Combine(Application.StartupPath, "TABLES");
            file_name = Path.Combine(file_name, "PIPE_CULVERT_TABLE_5_3.txt");
            List<string> lst_cont = iApp.Tables.Get_Tables_Embankment_Loading();

            sw.WriteLine();
            sw.WriteLine("TABLE 5.3 :");
            sw.WriteLine("-----------");
            sw.WriteLine();
            for (int i = 0; i < lst_cont.Count; i++)
            {
                sw.WriteLine(lst_cont[i]);
            }
            lst_cont.Clear();


            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("TABLE 5.4 :");
            sw.WriteLine("-----------");
            sw.WriteLine();
            file_name = Path.Combine(Path.GetDirectoryName(file_name), "PIPE_CULVERT_TABLE_5_4.txt");

            if (IsHighway)
                lst_cont = iApp.Tables.Get_Tables_Influence_Coefficient_Highway();
            else
                lst_cont = iApp.Tables.Get_Tables_Influence_Coefficient_Railway();

            for (int i = 0; i < lst_cont.Count; i++)
            {
                sw.WriteLine(lst_cont[i]);
            }
            lst_cont.Clear();
       

        }
    }
}
