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

using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign;


namespace LimitStateMethod.PierDesign
{
    public partial class frm_RCC_Pier_Design_with_Pile_Foundation : Form
    {
        IApplication iApp;
        public string Worksheet_Folder { get; set; }

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC PIER WITH PILE FOUNDATION [BS]";
                return "DESIGN OF RCC PIER WITH PILE FOUNDATION [IRC]";
            }
        }
        private void btn_worksheet_1_Click(object sender, EventArgs e)
        {

            Worksheet_Folder = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(Worksheet_Folder)) Directory.CreateDirectory(Worksheet_Folder);

            string ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");

            Button b = sender as Button;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (b.Name == btn_worksheet_Pier_cap.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\01 Pier Cap_BS.XLS");

                else if (b.Name == btn_worksheet_pier_design_with_piles.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\03 Pier Design with 6piles_BS.XLS");

                else if (b.Name == btn_worksheet_pile_capacity.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\02 Pile Capacity_BS.xls");
         
            }
            else
            {
                if (b.Name == btn_worksheet_Pier_cap.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\01 Pier Cap.XLS");

                else if (b.Name == btn_worksheet_pier_design_with_piles.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\03 Pier Design with 6piles.XLS");

                else if (b.Name == btn_worksheet_pile_capacity.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 2\02 Pile Capacity.xls");
            }

            iApp.Excel_Open_Message();
            iApp.OpenExcelFile(Worksheet_Folder, ex_file, "2011ap");
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
        public frm_RCC_Pier_Design_with_Pile_Foundation(IApplication iapp)
        {
            InitializeComponent();
            iApp = iapp;

        }

        private void btn_drawings_Click(object sender, EventArgs e)
        {
            string Drawing_Folder = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(Drawing_Folder))
            {
                Directory.CreateDirectory(Drawing_Folder);
            }

            Drawing_Folder = Path.Combine(Drawing_Folder, "DRAWINGS");
            if (!Directory.Exists(Drawing_Folder))
            {
                Directory.CreateDirectory(Drawing_Folder);
            }
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC_Pier_Worksheet_Design_2"), "RCC_Pier_Worksheet_Design_2");
        }

    }
}
