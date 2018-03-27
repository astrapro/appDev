using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.Foundation
{
    public partial class frmSheetPileDesign : Form
    {
        public frmSheetPileDesign()
        {
            InitializeComponent();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Executable File (*.exe)|*.exe";
                if(ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    txt_app_path.Text = ofd.FileName;
                }
            }

            File.WriteAllText(Get_File(), txt_app_path.Text);
            btn_proceed.Enabled = (File.Exists(txt_app_path.Text));
        }

        public string Get_File()
        {
            string fn = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            fn = Path.Combine(fn, "shpile.ard");
            return fn;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSheetPileDesign_Load(object sender, EventArgs e)
        {
            string sn = Get_File();

            if(File.Exists(sn))
            {
                txt_app_path.Text = File.ReadAllText(sn);
                txt_app_path.Enabled = false;
            }

            btn_proceed.Enabled = (File.Exists(txt_app_path.Text));

        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            if (File.Exists(txt_app_path.Text))
            {
                System.Diagnostics.Process.Start(txt_app_path.Text);
            }
        }

    }
}
