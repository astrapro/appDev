using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

//using AstraFunctionOne.BridgeDesign;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Pier;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_PSC_Box_Worksheet : Form
    {
        const string Title = "ANALYSIS OF PSC BOX GIRDER";
        IApplication iApp = null;

        //Chiranjit [2012 10 30]
        RccPier rcc_pier = null;


        public frm_PSC_Box_Worksheet(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            this.Text = Title + " : " + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!System.IO.Directory.Exists(iApp.LastDesignWorkingFolder))
                System.IO.Directory.CreateDirectory(iApp.LastDesignWorkingFolder);
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design"));
                return Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS"));
                return Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS");
            }
        }

        private void btn_Open_Worksheet_Design_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                string excel_file = "PSC Box Girder\\" + b.Text + ".xls";
                excel_file = Path.Combine(excel_path, excel_file);
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    //iApp.OpenExcelFile(excel_file, "2011ap");
                    //iApp.OpenExcelFile(
                    iApp.OpenExcelFile(iApp.LastDesignWorkingFolder, excel_file, "2011ap");
                    
                }
            }
            catch (Exception ex) { }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string draw_cmd = "PSC_Box_Girder_Worksheet_Design";
            iApp.RunViewer(Path.Combine(iApp.LastDesignWorkingFolder, "PSC Box Girder Drawings"), draw_cmd);
        }

        private void btn_design_of_anchorage_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = sender as Button;
                string excel_path = Path.Combine(Application.StartupPath, "DESIGN");
                string excel_file = "PSC Box Girder\\" + "Diaphragm_EndAnchorage_BlisterBlock_FuturePreStress.XLS";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(iApp.LastDesignWorkingFolder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
    }
}
