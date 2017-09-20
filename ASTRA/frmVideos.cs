using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraFunctionOne
{
    public partial class frmVideos : Form
    {
        IApplication iApp = null;
        public frmVideos( IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }


        public string VideoPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "Videos");
            }
        }
        private void btn_video_Click(object sender, EventArgs e)
        {

            string vid_path = VideoPath;
            List<string> list = new List<string>();

            if (!Directory.Exists(vid_path))
            {
                RunExe("http://techsoftglobal.com/Videos/ASTRAPROVIDS/astraprovids.aspx?k=" + (lst_tutorial.SelectedIndex + 1));
                return;
            }
            foreach (string item in Directory.GetFiles(vid_path))
            {
                if (Path.GetExtension(item).ToLower() == ".wmv")
                {
                    list.Add(item);
                }
            }

            try
            {
                if (list.Count > 0 && lst_tutorial.SelectedIndex > -1)
                {
                    if (File.Exists(list[lst_tutorial.SelectedIndex]))
                    {
                        RunExe(list[lst_tutorial.SelectedIndex]);
                    }
                    else
                    {
                        RunExe("http://techsoftglobal.com/Videos/ASTRAPROVIDS/astraprovids.aspx?k=" + (lst_tutorial.SelectedIndex + 1));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Video File not found!", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void RunExe(string p)
        {
            try
            {
                System.Diagnostics.Process.Start(p);
            }
            catch (Exception ex) { }
        }

        
        private void frmVideos_Load(object sender, EventArgs e)
        {
            lst_tutorial.SelectedIndex = 0;
        }

    }
}
