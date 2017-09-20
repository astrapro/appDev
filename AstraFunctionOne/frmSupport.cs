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
    internal partial class frmSupport : Form
    {
        IApplication iApp;
        CSupport spt;
        CSupportCollection sptCollectionCancel;

        int i = 0;
        public frmSupport()
        {
            InitializeComponent();
        }
        public int NodeNo
        {
            get
            {
                return SpFuncs.getInt(txtNodeNo.Text);
            }
            set
            {
                if (value > 0)
                    txtNodeNo.Text = value.ToString();
            }
        }
        public frmSupport(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }

        private void frmSupport_Load(object sender, EventArgs e)
        {
            sptCollectionCancel = new CSupportCollection();

            for (int i = 0; i < iApp.AppDocument.Support.Count; i++)
            {
                sptCollectionCancel.Add(iApp.AppDocument.Support[i]);
            }


            SetAllSupport();
        }

        private void rbtnSupportType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSupportType.Checked)
                grbSupportType.Enabled = true;
            else
                grbSupportType.Enabled = false;

        }

        private void rbtnDegreeOfFreedom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDegreeOfFreedom.Checked)
                grbDegOfFrdm.Enabled = true;
            else
                grbDegOfFrdm.Enabled = false;

        }

        private void rbtnFixed_CheckedChanged(object sender, EventArgs e)
        {
            chkXt.Checked = true;
            chkYt.Checked = true;
            chkZt.Checked = true;
            chkXr.Checked = true;
            chkYr.Checked = true;
            chkZr.Checked = true;
        }

        private void rbtnPinned_CheckedChanged(object sender, EventArgs e)
        {
            chkXt.Checked = true;
            chkYt.Checked = true;
            chkZt.Checked = true;
            chkXr.Checked = false;
            chkYr.Checked = false;
            chkZr.Checked = false;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            spt = new CSupport();
            spt.nodeNo = SpFuncs.getInt(txtNodeNo.Text);
            spt.dof0 = chkXt.Checked;
            spt.dof1 = chkYt.Checked;
            spt.dof2 = chkZt.Checked;
            spt.dof3 = chkXr.Checked;
            spt.dof4 = chkYr.Checked;
            spt.dof5 = chkZr.Checked;
            int indx = this.iApp.AppDocument.Support.IndexOf(spt.nodeNo);
            if (indx == -1)
            {
                if (spt.nodeNo > 0)
                {
                    
                    iApp.AppDocument.Support.Add(spt);
                    txtNodeNo.Text = "";
                    txtNodeNo.Focus();
                }
            }
            else
            {
                this.iApp.AppDocument.Support[indx] = spt;
            }
            SetAllSupport();
        }
        public void SetAllSupport()
        {

            lbxNodeDesc.Items.Clear();
            lbxNodeDesc.Items.Add("NODE\tSupportType");
            lbxNodeDesc.Items.Add("------------\t-------------------------------");
            for (int i = 0; i < iApp.AppDocument.Support.Count; i++)
            {
                lbxNodeDesc.Items.Add(setComboString(iApp.AppDocument.Support[i]));
                //lbxNodeDesc.Items.Add(iApp.AppDocument.Support[i].ToString());
            }
        }
        private string setComboString(CSupport csp)
        {
            string str = "";

            if (csp.dof0 == true && csp.dof1 == true && csp.dof2 == true && csp.dof3 == true && csp.dof4 == true && csp.dof5 == true)
                str = csp.nodeNo + " \t FIXED";
            else if (csp.dof0 == true && csp.dof1 == true && csp.dof2 == true && csp.dof3 == false && csp.dof4 == false && csp.dof5 == false)
                str = csp.nodeNo + " \t PINNED";
            else
                str = csp.nodeNo + " \t DEG_FRDM";
            
            return str;
        }

        private void txtNodeNo_TextChanged(object sender, EventArgs e)
        {
            setSupport(SpFuncs.getInt(txtNodeNo.Text));
        }
        private void setSupport(int nodeNo)
        {
            for (i = 0; i < this.iApp.AppDocument.Support.Count; i++)
            {
                if (this.iApp.AppDocument.Support[i].nodeNo == nodeNo)
                {
                    chkXt.Checked = this.iApp.AppDocument.Support[i].dof0;
                    chkYt.Checked = this.iApp.AppDocument.Support[i].dof1;
                    chkZt.Checked = this.iApp.AppDocument.Support[i].dof2;
                    chkXr.Checked = this.iApp.AppDocument.Support[i].dof3;
                    chkYr.Checked = this.iApp.AppDocument.Support[i].dof4;
                    chkZr.Checked = this.iApp.AppDocument.Support[i].dof5;

                    if (chkXt.Checked && chkYt.Checked && chkZt.Checked && chkXr.Checked && chkYr.Checked && chkZr.Checked)
                    {
                        rbtnSupportType.Checked = true;
                        rbtnFixed.Checked = true;
                    }
                    else if (chkXt.Checked && chkYt.Checked && chkZt.Checked && !chkXr.Checked && !chkYr.Checked && !chkZr.Checked)
                    {
                        rbtnSupportType.Checked = true;
                        rbtnPinned.Checked = true;
                    }
                    else
                        rbtnDegreeOfFreedom.Checked = true;
                    return;
                }
            }
        }

        private void lbxNodeDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kStr = lbxNodeDesc.SelectedItem.ToString();
            try
            {
                string[] values = kStr.Split(new char[] { '\t' });

                int elNo = int.Parse(values[0]);
                txtNodeNo.Text = elNo.ToString();
                setSupport(elNo);
            }
            catch (Exception ex) { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int indx = iApp.AppDocument.Support.IndexOf(NodeNo);
            if (indx != -1)
            {
                iApp.AppDocument.Support.RemoveAt(indx);
                SetAllSupport();
            }
        }

        private void txtNodeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnAdd.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iApp.AppDocument.Support = sptCollectionCancel;
            this.Close();
        }
    }
}