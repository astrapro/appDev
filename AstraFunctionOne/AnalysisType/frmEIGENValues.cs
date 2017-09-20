using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
namespace AstraFunctionOne.AnalysisType
{
    public partial class frmEIGENValues : Form
    {
        IApplication iApp;
        public frmEIGENValues(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
        }

        private void frmEIGENValues_Load(object sender, EventArgs e)
        {
            if (iApp.AppDocument.Analysis.NF > 0)
                txtTotalFrequencies.Text = iApp.AppDocument.Analysis.NF.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            iApp.AppDocument.Analysis.NF = SpFuncs.getInt(txtTotalFrequencies.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTotalFrequencies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnOK.Focus();
        }
    }
}
