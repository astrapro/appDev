using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.CableStayed
{
    public partial class frm_ProblemDescription : Form
    {
        public frm_ProblemDescription()
        {
            InitializeComponent();
        }

        private void frm_ProblemDescription_Load(object sender, EventArgs e)
        {
            string file_name = Path.Combine(Application.StartupPath, @"DESIGN\Example Problem Description.rtf");

            if (File.Exists(file_name))
            {
                richTextBox1.LoadFile(file_name);
            }
        }
    }
}
