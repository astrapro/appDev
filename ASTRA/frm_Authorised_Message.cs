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

            if (!LockProgram.IsProfessional_BridgeVersion())
            {
                lbl_bridge.Text = "Unauthorised";
                lbl_bridge.ForeColor = Color.Red;
            }
            else
            {
                lbl_bridge.Text = "Authorised";
                lbl_bridge.ForeColor = Color.Black;
            }
            if (!LockProgram.IsProfessional_StructuralVersion())
            {
                lbl_structure.Text = "Unauthorised";
                lbl_structure.ForeColor = Color.Red;
            }
            else
            {
                lbl_structure.Text = "Authorised";
                lbl_structure.ForeColor = Color.Black;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
