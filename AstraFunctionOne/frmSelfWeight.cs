using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace AstraFunctionOne
{
    internal partial class frmSelfWeight : Form
    {
        IApplication iApp;
        public frmSelfWeight()
        {
            InitializeComponent();
        }
        public frmSelfWeight(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            iApp.AppDocument.SelfWeight.loadcase = (cmbLoadFactorSet.SelectedIndex + 1);
            iApp.AppDocument.SelfWeight.SELFWEIGHTX = SpFuncs.getDouble(txtSelfWeightX.Text);
            iApp.AppDocument.SelfWeight.SELFWEIGHTY = SpFuncs.getDouble(txtSelfWeightY.Text);
            iApp.AppDocument.SelfWeight.SELFWEIGHTZ = SpFuncs.getDouble(txtSelfWeightZ.Text);
            this.Close();
        }

        private void frmSelfWeight_Load(object sender, EventArgs e)
        {
            if (!(iApp.AppDocument.SelfWeight.SELFWEIGHTX == -1 &&
                iApp.AppDocument.SelfWeight.SELFWEIGHTY == -1 &&
                iApp.AppDocument.SelfWeight.SELFWEIGHTZ == -1))
            {
                cmbLoadFactorSet.SelectedIndex = (iApp.AppDocument.SelfWeight.loadcase - 1);
                txtSelfWeightX.Text = iApp.AppDocument.SelfWeight.SELFWEIGHTX.ToString();
                txtSelfWeightY.Text = iApp.AppDocument.SelfWeight.SELFWEIGHTY.ToString();
                txtSelfWeightZ.Text = iApp.AppDocument.SelfWeight.SELFWEIGHTZ.ToString();
            }
        }

        private void SelfWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = sender as Control;
                if (ctrl.Name == cmbLoadFactorSet.Name)
                {
                    txtSelfWeightX.Focus();
                }
                else if (ctrl.Name == txtSelfWeightX.Name)
                {
                    txtSelfWeightY.Focus();
                }
                else if (ctrl.Name == txtSelfWeightY.Name)
                {
                    txtSelfWeightZ.Focus();
                }
                else if (ctrl.Name == txtSelfWeightZ.Name)
                {
                    btnOk.Focus();
                }
            }
        }
    }
}