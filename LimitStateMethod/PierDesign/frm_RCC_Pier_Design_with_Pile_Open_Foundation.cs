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
    public partial class frm_RCC_Pier_Design_with_Pile_Open_Foundation : Form
    {
        public frm_RCC_Pier_Design_with_Pile_Open_Foundation(IApplication iapp)
        {
            InitializeComponent();
            iApp = iapp;

        }

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC PIER WITH PILE OPEN FOUNDATION [BS]";
                return "DESIGN OF RCC PIER WITH PILE OPEN FOUNDATION [IRC]";
            }
        }
        IApplication iApp;
        public string Worksheet_Folder { get; set; }

        private void btn_worksheet_1_Click(object sender, EventArgs e)
        {
            Worksheet_Folder = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(Worksheet_Folder)) Directory.CreateDirectory(Worksheet_Folder);

            string ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");

            Button b = sender as Button;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (b.Name == btn_worksheet_1.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design_BS.XLS");
  
            }
            else
            {
                if (b.Name == btn_worksheet_1.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");
            }

            iApp.Excel_Open_Message();
            iApp.OpenExcelFile(Worksheet_Folder, ex_file, "2011ap");
        }

        private void btn_worksheet_1_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
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
            iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC_Pier_Worksheet_Design_1"), "RCC_Pier_Worksheet_Design_1");
        }

    }
}
