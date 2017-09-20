using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraAccess.StructureAnalysisDesign
{
    public partial class frm_Tunnel_Design : Form
    {
        IApplication iApp;
        public frm_Tunnel_Design(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            lininG_DOC1.OnClick_Drawing += new EventHandler(btn_open_drawings_Click);
        }

        void btn_open_drawings_Click(object sender, EventArgs e)
        {

            string draw_cmd = "TUNNEL_LINING";
            Working_Folder = Working_Folder;

            iApp.RunViewer(Path.Combine(Working_Folder, "TUNNEL LINING DRAWINGS"), draw_cmd);
        }


        public string Working_Folder 
        {
            get
            {
                return lininG_DOC1.Working_Folder;
            }
            set
            {
                lininG_DOC1.LastDesignWorkingFolder = value;
                lininG_DOC1.Working_Folder = value;
                lininG_DOC1.Set_Project_Name();
            }
        }

        private void frm_Tunnel_Design_Load(object sender, EventArgs e)
        {
            lininG_DOC1.Load_Tables();

            lininG_DOC1.IsDemo = iApp.Is_StructureDemo;
        }

        private void lininG_DOC1_Load(object sender, EventArgs e)
        {

        }
    }
    
}
