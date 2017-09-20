using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraFunctionOne.AnalysisType
{
    public partial class frmTimeHistory : Form
    {
        IApplication iApp;
        public frmTimeHistory(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
        }

        #region Time History 1
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
        public int TimeSteps
        {
            get
            {
                return SpFuncs.getInt(txtTimeSteps.Text);
            }
            set
            {
                txtTimeSteps.Text = value.ToString();
            }
        }
        public int PrintInterval
        {
            get
            {
                return SpFuncs.getInt(txtPrintInterval.Text);
            }
            set
            {
                txtPrintInterval.Text = value.ToString();
            }
        }
        public double StepInterval
        {
            get
            {
                return SpFuncs.getDouble(txtStepInterval.Text);
            }
            set
            {
                txtStepInterval.Text = value.ToString();
            }
        }
        public int DampingFactor
        {
            get
            {
                return SpFuncs.getInt(txtDampingFactor.Text);
            }
            set
            {
                txtDampingFactor.Text = value.ToString();
            }
        }
        #endregion

        // Time History 3
        public double[] TimeFunctions
        {
            get
            {
                double[] d3 = new double[3];
                string str = txtTimeFunction.Text;
                str = str.Replace(' ', ',');
                string[] values = str.Split(new char[] { ',' });
                if (values.Length > 3)
                {
                    epAstra.SetError(txtTimeFunction, "Error :Values more than 3!");
                }
                else if (values.Length == 3)
                {
                    d3[0] = double.Parse(values[0]);
                    d3[1] = double.Parse(values[1]);
                    d3[2] = double.Parse(values[2]);
                }
                return d3;
            }
            set
            {
                txtTimeFunction.Text = value[0].ToString("0.0") + "," + value[1].ToString("0.0") + "," + value[2].ToString("0.0");
            }
        }
        public double[] TimeValues
        {
            get
            {
                double[] d3 = new double[3];
                string str = txtTimeValues.Text;
                str = str.Replace(' ', ',');
                string[] values = str.Split(new char[] { ',' });
                if (values.Length > 3)
                {
                    epAstra.SetError(txtTimeValues, "Error :Values more than 3!");
                }
                else if (values.Length == 3)
                {
                    d3[0] = double.Parse(values[0]);
                    d3[1] = double.Parse(values[1]);
                    d3[2] = double.Parse(values[2]);
                }
                return d3;
            }
            set
            {
                txtTimeValues.Text = value[0].ToString("0.0") + "," + value[1].ToString("0.0") + "," + value[2].ToString("0.0");
            }
        }


        // Time History 4
        public int[] NodeNumbers
        {
            get
            {

                string str = txtNodeNumbers.Text;
                str = str.Replace(' ', ',');
                string[] values = str.Split(new char[] { ',' });

                int[] nn = new int[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    nn[i] = SpFuncs.getInt(values[i]);
                }
                return nn;
            }
            set
            {
                string str = "";
                foreach (int n in value)
                {
                    str += n.ToString() + ",";
                }
                txtNodeNumbers.Text = str.Substring(0, str.Length - 1);
            }
        }
        public int[] MemberNumbers
        {
            get
            {

                string str = txtMemberNumbers.Text;
                str = str.Replace(' ', ',');
                string[] values = str.Split(new char[] { ',' });

                int[] nn = new int[values.Length];
                try
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        nn[i] = int.Parse(values[i]);
                    }
                }
                catch (Exception exx) { }
                return nn;
            }
            set
            {
                string str = "";
                foreach (int n in value)
                {
                    str += n.ToString() + ",";
                }
                txtMemberNumbers.Text = str.Substring(0, str.Length - 1);
            }
        }


        public void Save()
        {
            this.iApp.AppDocument.Analysis.NF = TotalFrequencies;
            this.iApp.AppDocument.TimeHistory.THist_1.timeSteps = TimeSteps;
            this.iApp.AppDocument.TimeHistory.THist_1.printInterval = PrintInterval;
            this.iApp.AppDocument.TimeHistory.THist_1.stepInterval = StepInterval;
            this.iApp.AppDocument.TimeHistory.THist_1.dampingFactor = DampingFactor;

            this.iApp.AppDocument.TimeHistory.THist_2.groundMotion = cmbGroundMotion.SelectedIndex + 1;
            this.iApp.AppDocument.TimeHistory.THist_3.xDiv = SpFuncs.getInt(txtXDivision.Text);
            this.iApp.AppDocument.TimeHistory.THist_3.scaleFactor = SpFuncs.getInt(txtScaleFactor.Text);

            this.iApp.AppDocument.TimeHistory.THist_4.timeValues = TimeValues;
            this.iApp.AppDocument.TimeHistory.THist_4.timeFunction = TimeFunctions;

            this.iApp.AppDocument.TimeHistory.THist_5.Clear();
            this.iApp.AppDocument.TimeHistory.THist_5.NodalConstraint = chkNodalConstraint.Checked;
            foreach (int ndNo in NodeNumbers)
            {
                CTimeHistory5 th5 = new CTimeHistory5();
                th5.nodeNo = ndNo;
                th5.Tx = chkTx.Checked;
                th5.Ty = chkTy.Checked;
                th5.Tz = chkTz.Checked;
                th5.Rx = chkRx.Checked;
                th5.Ry = chkRy.Checked;
                th5.Rz = chkRz.Checked;
                this.iApp.AppDocument.TimeHistory.THist_5.Add(th5);
            }

            this.iApp.AppDocument.TimeHistory.THist_6.Clear();
            foreach (int mbNo in MemberNumbers)
            {
                CTimeHistory6 th6 = new CTimeHistory6();
                th6.memberNo = mbNo;
                if (rbtnStart.Checked)
                {
                    th6.member = CTimeHistory6.Member.START;
                }
                else
                {
                    th6.member = CTimeHistory6.Member.END;
                }
                this.iApp.AppDocument.TimeHistory.THist_6.Add(th6);
            }

            iApp.AppDocument.TimeHistory.THist_5.NodalConstraint = chkNodalConstraint.Checked;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            iApp.AppDocument.TimeHistory.IsDefault = false;
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkNodalConstraint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNodalConstraint.Checked)
            {
                grbNodalConstraint.Enabled = true;
            }
            else
                grbNodalConstraint.Enabled = false;

        }

        private void frmTimeHistory_Load(object sender, EventArgs e)
        {
            if (iApp.AppDocument.TimeHistory.IsDefault) return;

            txtTotalFrequencies.Text = iApp.AppDocument.Analysis.NF.ToString();
            txtTimeSteps.Text = iApp.AppDocument.TimeHistory.THist_1.timeSteps.ToString();
            txtPrintInterval.Text = iApp.AppDocument.TimeHistory.THist_1.printInterval.ToString();
            txtStepInterval.Text = iApp.AppDocument.TimeHistory.THist_1.stepInterval.ToString();
            txtDampingFactor.Text = iApp.AppDocument.TimeHistory.THist_1.dampingFactor.ToString();

            cmbGroundMotion.SelectedIndex = iApp.AppDocument.TimeHistory.THist_2.groundMotion - 1;
            txtXDivision.Text = iApp.AppDocument.TimeHistory.THist_3.xDiv.ToString();
            txtScaleFactor.Text = iApp.AppDocument.TimeHistory.THist_3.scaleFactor.ToString();

            TimeValues = iApp.AppDocument.TimeHistory.THist_4.timeValues;
            TimeFunctions = iApp.AppDocument.TimeHistory.THist_4.timeFunction;

            chkNodalConstraint.Checked = iApp.AppDocument.TimeHistory.THist_5.NodalConstraint;
            if(iApp.AppDocument.TimeHistory.THist_5.NodalConstraint)
            {
                txtNodeNumbers.Text = iApp.AppDocument.TimeHistory.THist_5.GetNodeNumbers();
                if (iApp.AppDocument.TimeHistory.THist_5.Count > 0)
                {
                    chkTx.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Tx;
                    chkTy.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Ty;
                    chkTz.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Tz;
                    chkRx.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Rx;
                    chkRy.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Ry;
                    chkRz.Checked = iApp.AppDocument.TimeHistory.THist_5[0].Rz;
                }
            }
            txtMemberNumbers.Text = iApp.AppDocument.TimeHistory.MemberNumbers();


            if (iApp.AppDocument.TimeHistory.THist_6.Count > 0)
            {
                if (iApp.AppDocument.TimeHistory.THist_6[0].member == CTimeHistory6.Member.START)
                    rbtnStart.Checked = true;
                else
                    rbtnEnd.Checked = true;

            }





        }

        private void txtboxes_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            ttAstra.SetToolTip(txt, txt.Text);
        }

        private void txtTotalFrequencies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control cntr = (Control)sender;
                if (cntr.Name == txtTotalFrequencies.Name)
                    txtTimeSteps.Focus();
                else if (cntr.Name == txtTimeSteps.Name)
                    txtPrintInterval.Focus();
                else if (cntr.Name == txtPrintInterval.Name)
                    txtStepInterval.Focus();
                else if (cntr.Name == txtStepInterval.Name)
                    txtDampingFactor.Focus();
                else if (cntr.Name == txtDampingFactor.Name)
                    cmbGroundMotion.Focus();
                else if (cntr.Name == cmbGroundMotion.Name)
                    txtXDivision.Focus();
                else if (cntr.Name == txtXDivision.Name)
                    txtScaleFactor.Focus();
                else if (cntr.Name == txtScaleFactor.Name)
                    txtTimeValues.Focus();
                else if (cntr.Name == txtTimeValues.Name)
                    txtTimeFunction.Focus();
                else if (cntr.Name == txtTimeFunction.Name)
                    chkNodalConstraint.Focus();
                else if (cntr.Name == chkNodalConstraint.Name)
                {
                    if (chkNodalConstraint.Checked)
                        txtNodeNumbers.Focus();
                    else
                        txtMemberNumbers.Focus();
                }
                else if (cntr.Name == txtNodeNumbers.Name)
                    chkTx.Focus();
                else if (cntr.Name == chkTx.Name)
                    chkTy.Focus();
                else if (cntr.Name == chkTy.Name)
                    chkTz.Focus();
                else if (cntr.Name == chkTz.Name)
                    chkRx.Focus();
                else if (cntr.Name == chkRx.Name)
                    chkRy.Focus();
                else if (cntr.Name == chkRy.Name)
                    chkRz.Focus();
                else if (cntr.Name == chkRz.Name)
                    txtMemberNumbers.Focus();
                else if (cntr.Name == txtMemberNumbers.Name)
                    rbtnStart.Focus();
                else if (cntr.Name == rbtnStart.Name)
                    btnOk.Focus();
                else if (cntr.Name == rbtnEnd.Name)
                    btnOk.Focus();
            }
        }

        private void chkRz_Leave(object sender, EventArgs e)
        {
            txtMemberNumbers.Focus();
        }
    }
}
