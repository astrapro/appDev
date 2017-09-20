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
    internal partial class frmNodalData : Form
    {
        int elCount = 1;
        int i = 0;
        IApplication aApp;
        CNodeData cnd;
        List<CNodeData> nodalList = new List<CNodeData>();
        public frmNodalData()
        {
            InitializeComponent();
        }
        public frmNodalData(IApplication app)
        {
            InitializeComponent();
            this.aApp = app;
            //aApp.AppDocument.NodeData = new CNodeDataCollection();
        }
        private CNodeData GetNodeData()
        {
            cnd = new CNodeData();
            cnd.NodeNo = int.Parse(txtNodeNo.Text);
            cnd.X = double.Parse(txtX.Text);
            cnd.Y = double.Parse(txtY.Text);
            cnd.Z = double.Parse(txtZ.Text);

            cnd.TX = chkXt.Checked;
            cnd.TY = chkYt.Checked;
            cnd.TZ = chkZt.Checked;

            cnd.RX = chkXr.Checked;
            cnd.RY = chkYr.Checked;
            cnd.RZ = chkZr.Checked;
            return cnd;
        }

        private int NodeNo
        {
            get
            {
                return SpFuncs.getInt(txtNodeNo.Text);
            }
            set
            {
                if (value > 0)
                    txtNodeNo.Text = value.ToString();
                //if (value > 0 && value <= (this.aApp.AppDocument.NodeData.Count + 1))
                //    txtNodeNo.Text = value.ToString();
            }
        }
        private void SaveWithNodeNo(int nodeNo)
        {
            cnd = new CNodeData();
            cnd.NodeNo = SpFuncs.getInt(txtNodeNo.Text);
            int indx = aApp.AppDocument.NodeData.IndexOf(cnd);

            try
            {
                cnd = GetNodeData();
            }
            catch (Exception ex)
            { return; }

            if (indx != -1)
            {
                this.aApp.AppDocument.NodeData[indx] = cnd;
            }
            else
            {
                aApp.AppDocument.NodeData.Add(GetNodeData());
            }
            SetStatus();
        }
        private void WorkWithNodeNo(int nodeNo)
        {
            cnd = new CNodeData();
            cnd.NodeNo = SpFuncs.getInt(txtNodeNo.Text);
            int indx = aApp.AppDocument.NodeData.IndexOf(cnd);
            if (indx != -1)
            {
                ShowNodeData(this.aApp.AppDocument.NodeData[indx]);
            }
            else
            {
                cnd = new CNodeData();
                cnd.NodeNo = aApp.AppDocument.NodeData.Count + 1;
                if (chkIncrOn.Checked)
                {
                    SetIncreament();
                    cnd.X = SpFuncs.getDouble(txtX.Text);
                    cnd.Y = SpFuncs.getDouble(txtY.Text);
                    cnd.Z = SpFuncs.getDouble(txtZ.Text);
                    ShowNodeData(cnd);
                }
                else
                {
                    txtX.Text = "";
                    txtY.Text = "";
                    txtZ.Text = "";
                    txtX.Focus();
                }
                //aApp.AppDocument.NodeData.Add(getCNodeData());
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                cnd = GetNodeData();
            }
            catch (Exception ex) { return; }
            int indx = aApp.AppDocument.NodeData.IndexOf(cnd);
            SaveWithNodeNo(NodeNo);
            if (indx == -1)
            {
                if (chkIncrOn.Checked)
                {
                    int i = SpFuncs.getInt(txtNodeIncr.Text);
                    NodeNo += i;
                }
                else
                {
                    NodeNo += 1;
                }
            }
            else
            {
                if (indx < aApp.AppDocument.NodeData.Count - 1)
                    NodeNo = aApp.AppDocument.NodeData[indx + 1].NodeNo;
                else
                {
                    if (chkIncrOn.Checked)
                    {
                        int i = SpFuncs.getInt(txtNodeIncr.Text);
                        NodeNo += i;
                    }
                    else
                    {
                        NodeNo += 1;
                    }
                }

            }

            SetStatus();
        }
        private void SetIncreament()
        {
            //txtNodeNo.Text = SpFuncs.getInt(txtNodeNo.Text) + SpFuncs.getInt(txtNodeIncr.Text) + "";
            double val = SpFuncs.getDouble(txtXIncr.Text);
            double dVal = 0;
            if (val != 0.0)
            {
                dVal = (SpFuncs.getDouble(txtX.Text) + SpFuncs.getDouble(txtXIncr.Text));
                txtX.Text = dVal.ToString("0.000");
            }

            val = SpFuncs.getDouble(txtYIncr.Text);
            if (val != 0.0)
            {
                dVal = (SpFuncs.getDouble(txtY.Text) + SpFuncs.getDouble(txtYIncr.Text));
                txtY.Text = dVal.ToString("0.000");
            }

            val = SpFuncs.getDouble(txtZIncr.Text);
            if (val != 0.0)
            {
                dVal = (SpFuncs.getDouble(txtZ.Text) + SpFuncs.getDouble(txtZIncr.Text));
                txtZ.Text = dVal.ToString("0.000");
            }
            SetStatus();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            SaveWithNodeNo(NodeNo);
            aApp.AppDocument.NodeData.LengthUnit = (eLengthUnits)cmbLengthUnit.SelectedIndex;
            nodalList.Clear();
            this.aApp.AppDocument.NodeData.Sort();
            this.Close();
        }

        private void rbtnGlobalCylindrical_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGlobalCylindrical.Checked)
                grbCylindricalAxis.Enabled = true;
            else
                grbCylindricalAxis.Enabled = false;

        }
        private void ShowNodeData(CNodeData ndata)
        {
            txtNodeNo.Text = ndata.NodeNo.ToString();
            txtX.Text = ndata.X.ToString();
            txtY.Text = ndata.Y.ToString();
            txtZ.Text = ndata.Z.ToString();

            chkXt.Checked = ndata.TX;
            chkYt.Checked = ndata.TY;
            chkZt.Checked = ndata.TZ;
            chkXr.Checked = ndata.RX;
            chkYr.Checked = ndata.RY;
            chkZr.Checked = ndata.RZ;
            SetStatus();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int indx = -1;
            try
            {
                cnd = GetNodeData();
                indx = aApp.AppDocument.NodeData.IndexOf(cnd);
            }
            catch (Exception exx)
            {
                indx = -1;
            }
            if (indx == -1)
            {
                if (aApp.AppDocument.NodeData.Count > 0)
                    NodeNo = aApp.AppDocument.NodeData[aApp.AppDocument.NodeData.Count - 1].NodeNo;
            }
            else
            {
                SaveWithNodeNo(NodeNo);
                if (indx > 0)
                    NodeNo = aApp.AppDocument.NodeData[indx - 1].NodeNo;
                if (indx == 0)
                {
                    NodeNo = aApp.AppDocument.NodeData[aApp.AppDocument.NodeData.Count - 1].NodeNo;
                }
                //else
                //{
                //    if (chkIncrOn.Checked)
                //    {
                //        int i = SpFuncs.getInt(txtNodeIncr.Text);
                //        NodeNo += i;
                //    }
                //    else
                //    {
                //        NodeNo += 1;
                //    }
                //}
            }
            //NodeNo -= 1;
            SetStatus();
        }
        private void SetStatus()
        {
            cnd = new CNodeData();
            cnd.NodeNo = SpFuncs.getInt(txtNodeNo.Text, -1);
            int indx = aApp.AppDocument.NodeData.IndexOf(cnd);
            if (indx == -1)
                elCount = aApp.AppDocument.NodeData.Count + 1;
            else
                elCount = this.aApp.AppDocument.NodeData.IndexOf(cnd) + 1;

            lblPosition.Text = "Total Node = " + aApp.AppDocument.NodeData.Count + ", Current Position = " + elCount;
        }

        private void frmNodalData_Load(object sender, EventArgs e)
        {

            if (aApp.AppDocument.NodeData.Count > 0)
                WorkWithNodeNo(aApp.AppDocument.NodeData.Count - 1);

            cmbLengthUnit.SelectedIndex = (int)aApp.AppDocument.NodeData.LengthUnit;

            nodalList.Clear();
            for (i = 0; i < this.aApp.AppDocument.NodeData.Count; i++)
            {
                nodalList.Add(this.aApp.AppDocument.NodeData[i]);
            }
            chkIncrOn.Checked = false;

            SetStatus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cnd = GetNodeData();
                int indx = aApp.AppDocument.NodeData.IndexOf(cnd);
                if (indx != -1)
                {
                    aApp.AppDocument.NodeData.RemoveAt(indx);
                    if (indx > 0)
                        ShowNodeData(aApp.AppDocument.NodeData[indx - 1]);
                    else if (indx == 0)
                    {
                        if (aApp.AppDocument.NodeData.Count > 0)
                            ShowNodeData(aApp.AppDocument.NodeData[0]);
                        else
                        {
                            NodeNo = 1;
                            txtX.Text = "";
                            txtY.Text = "";
                            txtZ.Text = "";
                        }
                    }

                }
            }
            catch (Exception exx)
            {
            }
            SetStatus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.aApp.AppDocument.NodeData.Clear();
            for (i = 0; i < nodalList.Count; i++)
            {
                this.aApp.AppDocument.NodeData.Add(nodalList[i]);
            }
            nodalList.Clear();
            this.aApp.AppDocument.NodeData.Sort();
            this.Close();
        }

        private void txtNodeNo_TextChanged(object sender, EventArgs e)
        {
            //CNodeData nodeData = new CNodeData();
            //int nd = 0;
            //nd = SpFuncs.getInt(txtNodeNo.Text);
            //nodeData.NodeNo = nd;
            //int indx = this.aApp.AppDocument.NodeData.IndexOf(nodeData);
            //if (indx != -1)
            //{
            //    //nodeData = this.aApp.AppDocument.NodeData[indx];
            //    ShowNodeData(indx);
            //}
            //else
            //{
            //    txtX.Text = "";
            //    txtY.Text = "";
            //    txtZ.Text = "";
            //}
            WorkWithNodeNo(NodeNo);
            //System.Console.Write(NodeNo);
        }

        private void txtX_Validated(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //try
            //{
            //    int ii = int.Parse(txt.Text);

            //}
            //catch (Exception exx)
            //{
            //    if (txt.Text == "")
            //        epAstra.SetError(txt, "Enter number.");
            //    else
            //        epAstra.SetError(txt, "Incorrect Format!");
            //}
        }

        private void txtX_Validating(object sender, CancelEventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //try
            //{
            //    int ii = int.Parse(txt.Text);

            //}
            //catch (Exception exx)
            //{
            //    if (txt.Text == "")
            //        epAstra.SetError(txt, "Enter number.");
            //    else
            //        epAstra.SetError(txt, "Incorrect Format!");
            //}
        }

        private void txtX_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            try
            {
                double dd = double.Parse(txt.Text);
                epAstra.SetError(txt, "");
            }
            catch (Exception exx)
            {
                if (txt.Text == "")
                    epAstra.SetError(txt, "Enter number.");
                else
                    epAstra.SetError(txt, "\'" + txt.Text + "' not a valid number.");
            }
        }

        private void chkIncrOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncrOn.Checked)
            {
                txtNodeIncr.Enabled = true;
                txtXIncr.Enabled = true;
                txtYIncr.Enabled = true;
                txtZIncr.Enabled = true;

            }
            else
            {
                txtNodeIncr.Enabled = false;
                txtXIncr.Enabled = false;
                txtYIncr.Enabled = false;
                txtZIncr.Enabled = false;
            }
        }

        private void txtX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtY.Focus();
            }
        }

        private void txtY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtZ.Focus();
            }
        }

        private void txtZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnNext.Focus();
            }
        }

        private void txtNodeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtX.Focus();
            }
        }
    }
}
