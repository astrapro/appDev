using System.ComponentModel;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.BearingDesign;

namespace LimitStateMethod.Bearing
{
    
    public partial class Form1 : Form
    {
        //const string Title = "DESIGN OF BEARING";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF BEARING [BS]";
                return "DESIGN OF BEARING [IRC]";
            }
        }



        IApplication iApp = null;

        //VERSO MONO AXIAL BEARING Transverse
        POT_PTFE_VERSO_BEARING_DESIGN VMABT;
        //VERSO FIXED BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VFB;
        //VERSO BI AXIAL BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VBAB;

        //VERSO MONO AXIAL BEARING Longitudinal
        POT_PTFE_VERSO_BEARING_DESIGN VMABL;

        public Form1(IApplication app)
        {
            InitializeComponent();

            iApp = app;
            //user_path = iApp.LastDesignWorkingFolder;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
        }

        public string user_path { get; set; }

        public string Working_Folder
        {
            get
            {
                user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                if (Directory.Exists(user_path) == false)
                    Directory.CreateDirectory(user_path);

                return user_path;
            }
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Working_Folder, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(Working_Folder, "Worksheet_Design"));
                return Path.Combine(Working_Folder, "Worksheet_Design");
            }
        }

        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uC_BRD1.iApp = iApp;
        }
         

    }

}
