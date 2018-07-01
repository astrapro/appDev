using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using AstraInterface.Interface;





namespace AstraFunctionOne
{
    public partial class frm_Authorised_Message : Form
    {
        //IApplication iApp;
        public frm_Authorised_Message(IApplication iapp)
        {
            InitializeComponent();
            //iApp = iapp;
        }
        public frm_Authorised_Message()
        {
            InitializeComponent();
            //iApp = iapp;
        }

        private void frm_Authorised_Message_Load(object sender, EventArgs e)
        {

            frmLockedVersion ff = new frmLockedVersion(false);
           
            if (LockProgram.Version_Type == AstraInterface.DataStructure.eVersionType.Enterprise_Bridge ||
                LockProgram.Version_Type == AstraInterface.DataStructure.eVersionType.Professional_Bridge)
            {
                lbl_bridge.Text = "Authorised";
                lbl_bridge.ForeColor = Color.Black;

                lbl_structure.Text = "Unauthorised";
                lbl_structure.ForeColor = Color.Red;
            }

            else if (LockProgram.Version_Type == AstraInterface.DataStructure.eVersionType.Enterprise_Structure ||
                LockProgram.Version_Type == AstraInterface.DataStructure.eVersionType.Professional_Structure)
            {
                lbl_bridge.Text = "Authorised";
                lbl_bridge.ForeColor = Color.Black;

                lbl_structure.Text = "Authorised";
                lbl_structure.ForeColor = Color.Black;
            }
            else
            {

                lbl_bridge.Text = "Unauthorised";
                lbl_bridge.ForeColor = Color.Red;

                lbl_structure.Text = "Unauthorised";
                lbl_structure.ForeColor = Color.Red;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
