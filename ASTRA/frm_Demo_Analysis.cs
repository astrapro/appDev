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
    public partial class frm_Demo_Analysis : Form
    {
         IApplication iapp;
         public frm_Demo_Analysis(IApplication app)
        {
            InitializeComponent();
            iapp = app;
        }

        private void rtb_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_send_mail_Click(object sender, EventArgs e)
        {
            string flname = Path.Combine(Application.StartupPath, "sendmail.exe");
            if (File.Exists(flname))
                System.Diagnostics.Process.Start(flname);

            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iapp.IsDemo == false)
                this.Close();
        }
    }
}
