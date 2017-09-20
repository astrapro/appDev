using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraFunctionOne.BridgeDesign.Abutment
{
    public partial class frmAbutmentWorkSheet : Form
    {
        string user_path = "";
        IApplication iApp = null;
        sbyte worksheet_design_option = 1;
        //Chiranjit [2011 06 17] for worksheet design 1 & 2
        public frmAbutmentWorkSheet(IApplication app, sbyte design_option)
        {
            InitializeComponent();
            iApp = app;
            worksheet_design_option = design_option;
        }

        private void btn_design_Click(object sender, EventArgs e)
        {
            //if (worksheet_design_option == 1)
            string copy_path = Path.Combine(user_path, "Abutment_Worksheet_sheet.xls");
            if (worksheet_design_option == 2)
                copy_path = Path.Combine(user_path, "Abutment Worksheet Design 02.xls");

            string source_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Worksheet Design 1\Abutment_Worksheet_sheet.xls");
            if (worksheet_design_option == 2)
                source_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Worksheet Design 2\Abutment Worksheet Design 02.xls");
            

            if (File.Exists(source_path))
            {
                try
                {
                    File.Copy(source_path, copy_path, true);
                    iApp.OpenExcelFile(copy_path, "2011ap");

                    //System.Security.SecureString sttr = new System.Security.SecureString();

                    ////System.Diagnostics.Process.Start(copy_path, "techsoft", "techsoftap", new );
                    //System.Diagnostics.Process.Start(copy_path);
                }
                catch (Exception ex) { }
            }
            else
            {
                MessageBox.Show(source_path, " file not found.", MessageBoxButtons.OK);
            }
        }

        private void btn_working_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(user_path))
                {
                    fbd.SelectedPath = user_path;
                }
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    user_path = fbd.SelectedPath;
                    btn_design.Enabled = true;
                    btn_drawing.Enabled = true;
                    btnSample_Drawing.Enabled = true;
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_drawing_Click(object sender, EventArgs e)
        {
            string drwg_path = "Abutment_Worksheet_Design_1";
            //string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccAbutmentDrawings");
            if (worksheet_design_option == 2)
            {
                drwg_path = "Abutment_Worksheet_Design_2";
            }

            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(user_path, drwg_path);
            //iApp.RunViewer(drwg_path);
        }

        private void frmAbutmentWorkSheet_Load(object sender, EventArgs e)
        {
            btn_design.Enabled = false;
            btn_drawing.Enabled = false;
        }

        private void btnSample_Drawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(user_path, "", "Abutment_Sample");
        }

        private void btn_open_desg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Worksheet Design File(*.xls)|*.xls;*.xlsx";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    user_path = Path.GetDirectoryName(ofd.FileName);
                    iApp.OpenExcelFile(ofd.FileName, "2011ap");
                    btn_drawing.Enabled = true;

                }
            }
        }
    }
}
