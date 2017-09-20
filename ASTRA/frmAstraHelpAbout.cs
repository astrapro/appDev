using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne
{
    public partial class frmAstraHelpAbout : Form
    {
        public frmAstraHelpAbout()
        {
            InitializeComponent();
        }

        private void lnk_techsoft_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://techsoftglobal.com/astrapage.aspx");
            }
            catch (Exception ex) { }
            this.Close();
        }
    }
}