using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne
{
    public partial class frmSpash : Form
    {
        double count = 100;
        public frmSpash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity = count/100.0;
            count -= 10;
            if (this.Opacity < 0.01) 
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void frmSpash_Load(object sender, EventArgs e)
        {

        }

        private void frmSpash_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
