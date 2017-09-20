using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraAccess.DynamicAnalysis
{

    public partial class frmResponseSpectrumAnalysis : Form
    {
        IApplication iApp;

        public string user_path { get; set; }
        string Input_File { get; set; }

        public eASTRADesignType ProjectType { get; set; }
        public string Title
        {
            get
            {
                if (ProjectType == eASTRADesignType.Response_Spectrum_Analysis) return "RESPONSE SPECTRUM ANALYSIS";
                if (ProjectType == eASTRADesignType.Time_History_Analysis) return "TIME HISTORY ANALYSIS";
                return "EIGEN VALUE ANALYSIS";
            }
        }

        public frmResponseSpectrumAnalysis(IApplication app, eASTRADesignType projType)
        {
            InitializeComponent();
            iApp = app;
            ProjectType = projType;

            txt_project_name.Text = iApp.Set_Project_Name(Title);

            Text = Title + " [" + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder) + "]";

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_new_design)
            {
                New_Design();
            }
            else if (btn == btn_open_design)
            {
                Open_Design();
            }
            else if (btn == btn_input_browse)
            {
                Input_File_Browse();
            }
            else  if (btn == btn_create_input_data)
            {
                Create_Input_Data();

            }
            else if (btn == btn_process_analysis)
            {
                Process_Analysis();

            }
            else if (btn == btn_open_analysis_report)
            {
                Open_Analysis_Report();
            }
            Save_Data();
        }

        void Analysis_Data_Modified(string file_name)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("DEFINE MOVING"))
                {

                    if (indx == -1)
                        indx = i;
                    flag = true;
                    isMoving_load = true;
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    //if (indx == -1)
                    //    indx = i;
                    //flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT") || kStr.Contains("FINISH"))
                {
                    if (!isMoving_load)
                    {
                        if (indx == -1)
                            indx = i;
                    }
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();


            if (ProjectType == eASTRADesignType.Eigen_Value_Analysis)
            {
                load_lst.Add(string.Format("PERFORM EIGEN VALUES ANALYSIS"));
                load_lst.Add(string.Format("FREQUENCIES 3"));
                //load_lst
            }
            else if (ProjectType == eASTRADesignType.Time_History_Analysis)
            {
                //load_lst.Add(string.Format(""));
                load_lst.Add(string.Format("PERFORM RESPONSE SPECTRUM ANALYSIS"));
                load_lst.Add(string.Format("FREQUENCIES 5"));
                load_lst.Add(string.Format("CUTOFF FREQUENCY 10.5"));
                load_lst.Add(string.Format("DIRECTION FACTORS X 1.0 Y 0.6667 Z 0.0 "));
                load_lst.Add(string.Format("*SPECTRUM TYPE DISPLACEMENT"));
                load_lst.Add(string.Format("SPECTRUM TYPE ACCELERATION"));
                load_lst.Add(string.Format("SPECTRUM POINTS 16"));
                load_lst.Add(string.Format("SCALE FACTOR 1.0"));
                load_lst.Add(string.Format("*PERIOD DISPLACEMENT"));
                load_lst.Add(string.Format("PERIOD ACCELERATION "));
                load_lst.Add(string.Format("      0.00    46.328"));
                load_lst.Add(string.Format("      0.02    46.328"));
                load_lst.Add(string.Format("      0.10   149.023"));
                load_lst.Add(string.Format("      0.18   207.706"));
                load_lst.Add(string.Format("      0.20   211.566"));
                load_lst.Add(string.Format("      0.22   212.725"));
                load_lst.Add(string.Format("      0.26   210.408"));
                load_lst.Add(string.Format("      0.30   203.845"));
                load_lst.Add(string.Format("      0.56   134.352"));
                load_lst.Add(string.Format("      0.70   110.416"));
                load_lst.Add(string.Format("      0.90    86.094"));
                load_lst.Add(string.Format("      1.20    62.929"));
                load_lst.Add(string.Format("      1.40    52.892"));
                load_lst.Add(string.Format("      1.80    39.765"));
                load_lst.Add(string.Format("      2.00    35.518"));
                load_lst.Add(string.Format("      3.00    21.620"));
                //load_lst.Add(string.Format(""));
            }
            else if (ProjectType == eASTRADesignType.Response_Spectrum_Analysis)
            {
                
                //load_lst.Add(string.Format(""));
                load_lst.Add(string.Format("PERFORM TIME HISTORY ANALYSIS"));
                load_lst.Add(string.Format("FREQUENCIES 8"));
                load_lst.Add(string.Format("TIME STEPS 200"));
                load_lst.Add(string.Format("PRINT INTERVAL 10"));
                load_lst.Add(string.Format("STEP INTERVAL 0.1"));
                load_lst.Add(string.Format("DAMPING FACTOR 0"));
                load_lst.Add(string.Format("GROUND MOTION TZ"));
                load_lst.Add(string.Format("X DIVISION 3"));
                load_lst.Add(string.Format("SCALE FACTOR 1000.0"));
                load_lst.Add(string.Format("TIME VALUES 0.0 10.0 30.0"));
                load_lst.Add(string.Format("TIME FUNCTION 0.0 1.0 -1.0"));
                //load_lst.Add(string.Format(""));
            }

            if (indx != -1)
            {
                inp_file_cont.InsertRange(indx, load_lst);


                rtb_input_data.Lines = inp_file_cont.ToArray();
                //inp_file_cont.InsertRange(indx, );
                File.WriteAllLines(file_name, inp_file_cont.ToArray());
                //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void Process_Analysis()
        {
           if( iApp.RunAnalysis(Input_File))
           {

               string rep = MyList.Get_Analysis_Report_File(Input_File);
               if (File.Exists(rep)) rtb_analysis_results.Lines = File.ReadAllLines(rep);
           }
        }

        public void Save_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }
        private void New_Design()
        {
            string ss = iApp.Create_Project(Title, txt_project_name.Text, eASTRADesignType.Response_Spectrum_Analysis);
            if (ss != "") user_path = ss;

            iApp.Save_Form_Record(this, user_path);
        }

        private void Open_Design()
        {
            string ss = iApp.Open_Project_Dialog(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
            if (ss != "")
            {
                user_path = ss;
                iApp.Read_Form_Record(this, user_path);

            }
        }
        private void Input_File_Browse()
        {
            string ext = "";
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "ASTRA Project Files (*.apr)|*.apr|Input Text File (*.txt)|*.txt";
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    ext = Path.GetExtension(ofd.FileName).ToLower();
                    if (ext == ".txt")
                    {
                        rtb_input_data.Lines = File.ReadAllLines(ofd.FileName);
                        txt_input_data.Text = Path.GetFileName(ofd.FileName);
                        string ll_txt = Path.Combine(Path.GetDirectoryName(ofd.FileName), "LL.TXT");
                        if(File.Exists(ll_txt))
                        {
                            File.Copy(ll_txt, Path.Combine(user_path, "LL.TXT"), true);
                        }

                    }
                    else if( ext == ".apr")
                    {
                        Select_From_APR_File(ofd.FileName);
                    }
                }
            }
            Create_Input_Data();
        }
        public void Select_From_APR_File(string apr_file)
        {
            string dir = Path.GetDirectoryName(apr_file);

            string flName = Path.GetFileNameWithoutExtension(apr_file);


            string pth = Path.Combine(dir, flName);

            List<string> allFiles = new List<string>();

            if(Directory.Exists(pth))
            {
                foreach (var item in Directory.GetFiles(pth))
                {

                    if (Path.GetExtension(item).ToLower() == ".txt")
                    {
                        allFiles.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in Directory.GetFiles(dir))
                {
                    if (Path.GetExtension(item).ToLower() == ".txt")
                    {
                        allFiles.Add(item);
                    }
                }
            }

            pth = Path.Combine(dir, flName);

            if(allFiles.Count > 0)
            {
                foreach (var item in allFiles)
                {
                    pth = Path.GetFileName(item).ToUpper();
                    if(pth.StartsWith("INPUT"))
                    {
                        txt_input_data.Text = pth;
                        rtb_input_data.Lines = File.ReadAllLines(pth);
                        string ll_txt = Path.Combine(Path.GetDirectoryName(pth), "LL.TXT");
                        if (File.Exists(ll_txt))
                        {
                            File.Copy(ll_txt, Path.Combine(user_path, "LL.TXT"), true);
                        }
                        break;
                    }

                }
            }

        }

        private void Create_Input_Data()
        {

            Input_File = Path.Combine(user_path, txt_input_data.Text);
            Analysis_Data_Modified(Input_File);
            File.WriteAllLines(Input_File, rtb_input_data.Lines);

            MessageBox.Show("Analysis Input Data File created as " + Input_File, "ASTRA", MessageBoxButtons.OK);
        }

        private void Open_Analysis_Report()
        {
            string rep = MyList.Get_Analysis_Report_File(Input_File);


            if (File.Exists(rep)) System.Diagnostics.Process.Start(rep);
        }

        private void frmResponseSpectrumAnalysis_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 450;
        }


    }


}
