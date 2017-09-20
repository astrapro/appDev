﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;


namespace AstraFunctionOne.BridgeDesign
{
    public partial class frmCompositWorksheetDesign : Form
    {
        string user_path = "";
        IApplication iApp = null;
        public frmCompositWorksheetDesign(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        private void btn_design_Click(object sender, EventArgs e)
        {
            string copy_path = Path.Combine(user_path, "Composit_Worksheet_Design.xls");
            string source_path = Path.Combine(Application.StartupPath, "DESIGN\\Composit_Worksheet_Design.xls");


            if (File.Exists(source_path))
            {
                try
                {
                    iApp.OpenExcelFile(source_path, "2011ap");
                    //File.Copy(source_path, copy_path, true);

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
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_drawing_Click(object sender, EventArgs e)
        {
            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\CompositDrawings");
            System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(user_path, drwg_path);
            //iApp.RunViewer(drwg_path);
        }

        private void frmCompositWorksheetDesign_Load(object sender, EventArgs e)
        {
            btn_design.Enabled = false;
            btn_drawing.Enabled = false;
        }

        private void btn_prev_desg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Worksheet Design File(*.xls)|*.xls;*.xlsx";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    iApp.OpenExcelFile(ofd.FileName, "2011ap");
                }
            }
        }
    }
}
