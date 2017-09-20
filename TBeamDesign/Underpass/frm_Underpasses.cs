using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace BridgeAnalysisDesign.Underpass
{
    public partial class frm_Underpasses : Form
    {
        IApplication iApp = null;
        public frm_Underpasses(IApplication App)
        {
            InitializeComponent();
            iApp = App;
        }

        private void btn_dwg_Click(object sender, EventArgs e)
        {



            Button btn = sender as Button;


            string user_path = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, "Under Pass Drawings");

            if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);
            if (btn.Name == btn_dwg_pup.Name)
            {
                iApp.RunViewer(System.IO.Path.Combine(user_path, "Pedestrian Under Pass"), "PUP");
            }
            else if (btn.Name == btn_dwg_vup.Name)
            {
                iApp.RunViewer(System.IO.Path.Combine(user_path, "Vehicular Under Pass"), "VUP");
            }
            else if (btn.Name == btn_dwg_rob.Name)
            {
                iApp.RunViewer(System.IO.Path.Combine(user_path, "Railway Over Bridge"), "ROB");
            }
        }
    }
}
