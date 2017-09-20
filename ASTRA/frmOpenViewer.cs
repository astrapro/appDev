using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne
{
    public partial class frmOpenViewer : Form
    {
        int counter = 0;
        public frmOpenViewer()
        {
            InitializeComponent();
            counter = 0;
            IsTimerOn = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsTimerOn)
            {
                counter++;
                if (counter == 3)
                {
                    timer1.Stop();
                    this.Close();
                }
            }
            //if (this.Opacity > 0.0)
            //    this.Opacity -= 0.2;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public bool IsTimerOn { get; set; }
        
        public string TextString
        {
            set
            {
                lbl_TextString.Text = value;
            }
        }
    }
}
