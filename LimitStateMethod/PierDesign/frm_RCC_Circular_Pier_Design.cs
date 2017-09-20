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
    public partial class frm_RCC_Circular_Pier_Design : Form
    {
        IApplication iApp;

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC CIRCULAR PIER [BS]";
                return "DESIGN OF RCC CIRCULAR PIER [IRC]";
            }
        }

        public frm_RCC_Circular_Pier_Design(IApplication iapp)
        {
            InitializeComponent();
            iApp = iapp;
        }

        public string Worksheet_Folder { get; set; }
        private void btn_worksheet_1_Click(object sender, EventArgs e)
        {
            Worksheet_Folder = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(Worksheet_Folder)) Directory.CreateDirectory(Worksheet_Folder);


            string ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Worksheet Design 1\PIER_Worksheet_Design.XLS");
            Button b = sender as Button;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (b.Name == btn_ws_new_cir.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Circular Design\Design of Circular Pier_BS.xls");
                else if (b.Name == btn_ws_new_cir_well.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier with Well Foundation\Design of Pier with Well Foundation.xls");
            }
            else
            {
                if (b.Name == btn_ws_new_cir.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Circular Design\Design of Circular Pier.xls");
                else if (b.Name == btn_ws_new_cir_well.Name)
                    ex_file = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier with Well Foundation\Design of Pier with Well Foundation.xls");
     
            }
            iApp.Excel_Open_Message();
            iApp.OpenExcelFile(Worksheet_Folder, ex_file, "2011ap");
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
    }
}
