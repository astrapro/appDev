using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.AnalysisType;
namespace AstraFunctionOne
{
    internal partial class frmAnalysisType : Form
    {



        IApplication iApp;
        public frmAnalysisType(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbtnStaticAnalysis.Checked)
            {
                iApp.AppDocument.Analysis.NDYN = 0;
            }
            else if (rbtnDynamicAnalysis.Checked)
            {
                if (rbtnEigenvalue.Checked)
                {
                    iApp.AppDocument.Analysis.NDYN = 1;
                    frmEIGENValues feigen = new frmEIGENValues(iApp);
                    feigen.Owner = this;
                    feigen.ShowDialog();
                }
                else if (rbtnTimeHistoryAnalysis.Checked)
                {

                    iApp.AppDocument.Analysis.NDYN = 2;
                    AnalysisType.frmTimeHistory fth = new AstraFunctionOne.AnalysisType.frmTimeHistory(iApp);
                    fth.Owner = this;
                    fth.ShowDialog();
                }
                else if (rbtnResponse.Checked)
                {

                    iApp.AppDocument.Analysis.NDYN = 3;
                    frmResponseSpectrum fres = new frmResponseSpectrum(iApp);
                    fres.Owner = this;
                    fres.ShowDialog();
                }
                this.iApp.AppDocument.Analysis.AnalysisType = "NDYN";
            }
            else if (rbtnDataCheck.Checked)
            {
                iApp.AppDocument.Analysis.NDYN = 9;
            }
            this.Close();
        }

        private void rbtnDynamicAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDynamicAnalysis.Checked)
                grbSelectType.Enabled = true;
            else
                grbSelectType.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void setData()
        {
            if (iApp.AppDocument.Analysis.NDYN == 0)
            {
                rbtnStaticAnalysis.Checked = true;

            }
            else if (iApp.AppDocument.Analysis.NDYN != 9)
            {
                rbtnDynamicAnalysis.Checked = true;

                if (iApp.AppDocument.Analysis.NDYN == 1)
                {
                    rbtnEigenvalue.Checked = true;
                }
                else if (iApp.AppDocument.Analysis.NDYN == 2)
                {
                    rbtnTimeHistoryAnalysis.Checked = true;
                }
                else if (iApp.AppDocument.Analysis.NDYN == 3)
                {
                    rbtnResponse.Checked = true;
                }

            }
            else if(iApp.AppDocument.Analysis.NDYN == 9)
            {
                rbtnDataCheck.Checked = true;
            }
            btnOk.Focus();
        }

        private void frmAnalysisType_Load(object sender, EventArgs e)
        {
            setData();
            btnOk.Focus();
        }

        private void rbtnStaticAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            //txtTotalFrequencies.Text = "";
            rbtnEigenvalue.Checked = false;
            rbtnTimeHistoryAnalysis.Checked = false;
            rbtnResponse.Checked = false;
            //rbtnDirect.Checked = false;

        }


        private void dynamicAnalysis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = sender as Control;
                if (ctrl.Name == rbtnDynamicAnalysis.Name)
                {
                    rbtnEigenvalue.Focus();
                }
                else
                    btnOk.Focus();
            }
        }
    }
}