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
    public partial class frmResponseSpectrum : Form
    {
        IApplication iApp;

        public frmResponseSpectrum(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
        }
        public int TotalFrequencies
        {
            get
            {
                return SpFuncs.getInt(txtTotalFrequencies.Text);
            }
            set
            {
                txtTotalFrequencies.Text = value.ToString();
            }
        }
        public int SpectrumPoints
        {
            get
            {
                return SpFuncs.getInt(txtSpectrumPoints.Text);
            }
            set
            {
                txtSpectrumPoints.Text = value.ToString();
            }
        }
        public double ScaleFactor
        {
            get
            {
                return SpFuncs.getDouble(txtScaleFactor.Text);
            }
            set
            {
                txtScaleFactor.Text = value.ToString("0.0");
            }
        }
        
        private void rbtnAcceleration_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAcceleration.Checked)
            {
                grbAcceleration.Enabled = true;
                grbDisplacement.Enabled = false;
            }
            else
            {
                grbAcceleration.Enabled = false;
                grbDisplacement.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.iApp.AppDocument.Analysis.NF = SpFuncs.getInt(txtTotalFrequencies.Text);
            this.iApp.AppDocument.Response.CutOffFrequencies = SpFuncs.getDouble(txtCutOffFrequencies.Text);
            this.iApp.AppDocument.Response.X = SpFuncs.getDouble(txtX.Text);
            this.iApp.AppDocument.Response.Y = SpFuncs.getDouble(txtY.Text);
            this.iApp.AppDocument.Response.Z = SpFuncs.getDouble(txtZ.Text);

            this.iApp.AppDocument.Response.SpectrumPoints = SpectrumPoints;
            this.iApp.AppDocument.Response.ScaleFactor = ScaleFactor;

            if (rbtnAcceleration.Checked)
            {
                this.iApp.AppDocument.Response.Type = AstraInterface.DataStructure.CResponse.SpectrumType.Acceleration;
                this.iApp.AppDocument.Response.PeriodsText = txtAccPeriods.Text;
                this.iApp.AppDocument.Response.AccelerationText = txtAcceleration.Text;

            }
            else if (rbtnDisplacement.Checked)
            {
                this.iApp.AppDocument.Response.Type = AstraInterface.DataStructure.CResponse.SpectrumType.Displacement;
                this.iApp.AppDocument.Response.PeriodsText = txtDispPeriods.Text;
                this.iApp.AppDocument.Response.DisplacementText = txtDisplacement.Text;
            }

            this.iApp.AppDocument.Response.IsDefault = false;
            this.Close();
        }

        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            toolTipAstra.SetToolTip(txt, txt.Text);
        }

        private void frmResponseSpectrum_Load(object sender, EventArgs e)
        {
            if (iApp.AppDocument.Response.IsDefault) return;

            TotalFrequencies = iApp.AppDocument.Analysis.NF;
            txtCutOffFrequencies.Text = iApp.AppDocument.Response.CutOffFrequencies.ToString();
            txtX.Text = iApp.AppDocument.Response.X.ToString("0.00");
            txtY.Text = iApp.AppDocument.Response.Y.ToString("0.00");
            txtZ.Text = iApp.AppDocument.Response.Z.ToString("0.00");

            SpectrumPoints = iApp.AppDocument.Response.SpectrumPoints;
            ScaleFactor = iApp.AppDocument.Response.ScaleFactor;

            if (iApp.AppDocument.Response.Type == AstraInterface.DataStructure.CResponse.SpectrumType.Acceleration)
            {
                rbtnAcceleration.Checked = true;
                txtAccPeriods.Text = iApp.AppDocument.Response.PeriodsText;
                txtAcceleration.Text = iApp.AppDocument.Response.AccelerationText;
            }
            else
            {
                rbtnDisplacement.Checked = true;
                txtDispPeriods.Text = iApp.AppDocument.Response.PeriodsText;
                txtDisplacement.Text = iApp.AppDocument.Response.DisplacementText;
            }
            
        }

        private void txtTotalFrequencies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TextBox txt = sender as TextBox;

                if (txt.Name == txtTotalFrequencies.Name)
                    txtCutOffFrequencies.Focus();

                else if (txt.Name == txtCutOffFrequencies.Name)
                    txtX.Focus();
                else if (txt.Name == txtX.Name)
                    txtY.Focus();
                else if (txt.Name == txtY.Name)
                    txtZ.Focus();
                else if (txt.Name == txtZ.Name)
                    txtSpectrumPoints.Focus();
                else if (txt.Name == txtSpectrumPoints.Name)
                    txtScaleFactor.Focus();
                else if (txt.Name == txtScaleFactor.Name)
                    rbtnAcceleration.Focus();

                else if (txt.Name == txtAccPeriods.Name)
                    txtAcceleration.Focus();
                else if (txt.Name == txtAcceleration.Name)
                    btnOk.Focus();
                else if (txt.Name == txtDispPeriods.Name)
                    txtDisplacement.Focus();
                else if (txt.Name == "txtDisplacement")
                    btnOk.Focus();
            }
        }

        private void rbtnAcceleration_KeyDown(object sender, KeyEventArgs e)
        {
            Control rbtn = sender as Control;
            if (rbtn.Name == rbtnAcceleration.Name)
            {
                txtAccPeriods.Focus();
            }
            else if (rbtn.Name == rbtnDisplacement.Name)
            {
                txtDispPeriods.Focus();
            }
        }

        
    }
}
