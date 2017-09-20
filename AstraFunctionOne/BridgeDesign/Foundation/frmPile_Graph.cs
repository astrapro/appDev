using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne.BridgeDesign.Foundation
{
    public partial class frmPile_Graph : Form
    {
        public frmPile_Graph()
        {
            InitializeComponent();
        }

        private void frmPile_Graph_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txt_obtaned_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txt_obtaned_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
