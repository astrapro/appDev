using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_BoxGirder_Msg : Form
    {
        public frm_BoxGirder_Msg()
        {
            InitializeComponent();
            
        }
        public static void Msg_Showw()
        {
            frm_BoxGirder_Msg fm = new frm_BoxGirder_Msg();
            fm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
