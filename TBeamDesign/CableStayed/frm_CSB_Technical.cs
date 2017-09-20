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
    public partial class frm_CSB_Technical : Form
    {
        public frm_CSB_Technical()
        {
            InitializeComponent();
        }

        private void frm_CSB_Technical_Load(object sender, EventArgs e)
        {
            string fl = Path.Combine(Application.StartupPath, @"ASTRAHelp\NONLINEAR_Technical_Specification.rtf");
      
            if(File.Exists(fl))
            {
                rtb.LoadFile(fl);
                rtb.ReadOnly = true;
            }
        }
    }
}
