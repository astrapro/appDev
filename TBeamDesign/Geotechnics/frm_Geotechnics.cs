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


namespace BridgeAnalysisDesign.Geotechnics
{
    public partial class frm_Geotechnics : Form
    {
        IApplication iApp = null;
        string user_path = "";
        public frm_Geotechnics(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            
            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, "Geotechnics");
            //if (Directory.Exists(user_path) == false)
            //    Directory.CreateDirectory(user_path);

        }
        private void btn_worksheet_1_Click(object sender, EventArgs e)
        {
            string ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");

            Button b = sender as Button;


            //if (b.Name == btn_worksheet_1.Name)
            ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Geotechnics\" + b.Text + ".xls");

            //else if (b.Name == btn_worksheet_Pier_cap.Name)
            //    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\01 Pier Cap.XLS");

            //else if (b.Name == btn_worksheet_pier_design_with_piles.Name)
            //    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\03 Pier Design with 6piles.XLS");

            //else if (b.Name == btn_worksheet_pile_capacity.Name)
            //    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\02 Pile Capacity.xls");

            if (File.Exists(ex_file))
                iApp.OpenExcelFile(Worksheet_Folder, ex_file, "2011ap");
            else
                MessageBox.Show(ex_file + "   not found.");
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "Geotechnics")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Geotechnics"));

                //if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                //    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Geotechnics");
                //return user_path;
            }
        }

        private void btn_working_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(user_path)) fbd.SelectedPath = user_path;
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    user_path = fbd.SelectedPath;
                    iApp.LastDesignWorkingFolder = user_path;
                }
            }
            grb_worksheet.Enabled = (Directory.Exists(user_path));
        }
        
    }
}
