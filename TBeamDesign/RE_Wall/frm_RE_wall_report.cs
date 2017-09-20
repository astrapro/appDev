using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
namespace BridgeAnalysisDesign.RE_Wall
{
    public partial class frm_RE_wall_report : Form
    {
       
        IApplication iapp;
        public frm_RE_wall_report(IApplication app, bool is_details)
        {
            InitializeComponent();
            iapp = app;
            rbtn_details.Checked = is_details;


        }
        public bool Is_Details { get; set; }
        private void btn_open_Click(object sender, EventArgs e)
        {
            Is_Details = rbtn_details.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
