 // Jay Radhey Govinda Hare Krishna Krishna
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
namespace AstraFunctionOne
{
    internal partial class frmNodalLoadData : Form
    {
        IApplication iApp;
        CMemberBeamLoading mbl;
        int ndCount = 1;
        List<int> lst = new List<int>();

        CMemberBeamLoadingCollection mblcCancel;
        public frmNodalLoadData()
        {
            InitializeComponent();
        }

        public frmNodalLoadData(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }

        #region Public Property
        public int LoadCase
        {
            get
            {
                return SpFuncs.getInt(txtLoadCase.Text);
            }
            set
            {
                if (value > 0)
                    txtLoadCase.Text = value.ToString();
            }
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

        #endregion

        private void btnNextLoadCase_Click(object sender, EventArgs e)
        {
            txtLoadCase.Text = (SpFuncs.getInt(txtLoadCase.Text) + 1) + "";
            showStatus();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            //EMassUnits EME = EMassUnits.KN;
            //EMassUnits EME = (EMassUnits)cmbMassUnit.SelectedIndex;
            //MessageBox.Show((int)EME + EME.ToString());

            //ELengthUnits EMES = (ELengthUnits)cmbLengthUnits.SelectedIndex;
            //MessageBox.Show((int)EMES + EMES.ToString());
            iApp.AppDocument.MemberBeamLoad.MassUnit = (EMassUnits)cmbMassUnit.SelectedIndex;
            iApp.AppDocument.MemberBeamLoad.LengthUnit = (eLengthUnits)cmbLengthUnits.SelectedIndex;



            iApp.AppDocument.MemberBeamLoad.LengthFactor = CAstraUnits.GetFact(cmbLengthUnits.Text);
            iApp.AppDocument.MemberBeamLoad.MassFactor = CAstraUnits.GetFact(cmbMassUnit.Text);
            this.Close();
        }

        private void btnNextNodalData_Click(object sender, EventArgs e)
        {
            if (txtFx.Text == "" && txtFy.Text == "" && txtFz.Text == "" &&
                txtMx.Text == "" && txtMy.Text == "" && txtMz.Text == "")
                return;
            if (txtNodeNo.Text == "" || txtLoadCase.Text == "")
                return;

            if (isPresent(txtLoadCase.Text, txtNodeNo.Text))
            {
                ndCount = lst.IndexOf(SpFuncs.getInt(txtNodeNo.Text));
                if (ndCount == lst.Count - 1) { txtNodeNo.Text = (lst[lst.Count - 1] + 1) + ""; return; }

                if (lst.Count > 0)
                {
                    if (ndCount >= 0 && ndCount < lst.Count - 1)
                    {
                        txtNodeNo.Text = lst[ndCount + 1].ToString();
                        setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
                    }
                    else
                    {
                        txtNodeNo.Text = lst[0].ToString();
                        setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
                    }
                }
            }
            else
            {
                addMemberLoad();
                txtNodeNo.Focus();
            }

            showStatus();
        }
        private void addMemberLoad()
        {
            mbl = new CMemberBeamLoading();
            mbl.Node = SpFuncs.getInt(txtNodeNo.Text);
            mbl.loadcase = SpFuncs.getInt(txtLoadCase.Text);
            mbl.fx = SpFuncs.getDouble(txtFx.Text);
            mbl.fy = SpFuncs.getDouble(txtFy.Text);
            mbl.fz = SpFuncs.getDouble(txtFz.Text);
            mbl.mx = SpFuncs.getDouble(txtMx.Text);
            mbl.my = SpFuncs.getDouble(txtMy.Text);
            mbl.mz = SpFuncs.getDouble(txtMz.Text);

            lst.Add(mbl.Node);
            iApp.AppDocument.MemberBeamLoad.Add(mbl);
            NodeNo = mbl.Node + 1;
            //txtNodeNo.Text = () + "";
        }

        private void frmNodalLoadData_Load(object sender, EventArgs e)
        {
            mblcCancel = new CMemberBeamLoadingCollection();
            for (int i = 0; i < iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                mblcCancel.Add(iApp.AppDocument.MemberBeamLoad[i]);
            }


            cmbMassUnit.SelectedIndex = (int)iApp.AppDocument.MemberBeamLoad.MassUnit;
            cmbLengthUnits.SelectedIndex = (int)iApp.AppDocument.MemberBeamLoad.LengthUnit;
            //cmbMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(iApp.AppDocument.MemberBeamLoad.MassFactor);
            //cmbLengthUnits.SelectedIndex = CAstraUnits.GetLengthUnitIndex(iApp.AppDocument.MemberBeamLoad.LenthFactor);

            if (iApp.AppDocument.MemberBeamLoad.Count > 0)
            {
                txtLoadCase.Text = iApp.AppDocument.MemberBeamLoad[0].loadcase.ToString();
            }
            showStatus();
        }
        private void showStatus()
        {
            ndCount = iApp.AppDocument.MemberBeamLoad.IndexOf(LoadCase, NodeNo);
            
            
            lblStatus.Text = "Total Data = " + iApp.AppDocument.MemberBeamLoad.Count + ", Current Position = " + ((ndCount == -1) ? iApp.AppDocument.MemberBeamLoad.Count + 1: ndCount + 1);
        }
        private void showData(int index)
        {
            //txtLoadCase.Text = iApp.AppDocument.MemberBeamLoad[index].loadcase.ToString();
            //txtNodeNo.Text = iApp.AppDocument.MemberBeamLoad[index].Node.ToString();
            //txtFx.Text = iApp.AppDocument.MemberBeamLoad[index].fx.ToString();
            //txtFy.Text = iApp.AppDocument.MemberBeamLoad[index].fy.ToString();
            //txtFz.Text = iApp.AppDocument.MemberBeamLoad[index].fz.ToString();
            //txtMx.Text = iApp.AppDocument.MemberBeamLoad[index].mx.ToString();
            //txtMy.Text = iApp.AppDocument.MemberBeamLoad[index].my.ToString();
            //txtMz.Text = iApp.AppDocument.MemberBeamLoad[index].mz.ToString();
        }

        private void btnPrevLoadCase_Click(object sender, EventArgs e)
        {
            txtLoadCase.Text = (SpFuncs.getInt(txtLoadCase.Text) - 1) + "";
            showStatus();
        }

        private void btnDeleteLoadCase_Click(object sender, EventArgs e)
        {
            deleteData(SpFuncs.getInt(txtLoadCase.Text));
            showStatus();
        }

        private void btnDeleteNodalData_Click(object sender, EventArgs e)
        {
            deleteData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
            showStatus();
        }

        private void btnPrevNodalData_Click(object sender, EventArgs e)
        {
            if (!isPresent(txtLoadCase.Text, txtNodeNo.Text))
            {
                txtNodeNo.Text = lst[lst.Count - 1].ToString();
                return;
            }
            if (lst.Count > 0)
            {
                ndCount = lst.IndexOf(SpFuncs.getInt(txtNodeNo.Text));
                if (ndCount > 0 && ndCount < lst.Count)
                {
                    txtNodeNo.Text = lst[ndCount - 1].ToString();
                    setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
                }
                else
                {
                    txtNodeNo.Text = lst[0].ToString();
                    setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
                }
            }
            showStatus();
        }

        private void txtNodeNo_TextChanged(object sender, EventArgs e)
        {
            setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
            showStatus();
        }
        private void setNodalLoadData(int loadcase, int nodeNo)
        {
            txtFx.Text = "";
            txtFy.Text = "";
            txtFz.Text = "";
            txtMx.Text = "";
            txtMy.Text = "";
            txtMz.Text = "";
            for (int i = 0; i < this.iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                if (this.iApp.AppDocument.MemberBeamLoad[i].loadcase == loadcase && iApp.AppDocument.MemberBeamLoad[i].Node == nodeNo)
                {
                    txtFx.Text = iApp.AppDocument.MemberBeamLoad[i].fx.ToString();
                    txtFy.Text = iApp.AppDocument.MemberBeamLoad[i].fy.ToString();
                    txtFz.Text = iApp.AppDocument.MemberBeamLoad[i].fz.ToString();
                    txtMx.Text = iApp.AppDocument.MemberBeamLoad[i].mx.ToString();
                    txtMy.Text = iApp.AppDocument.MemberBeamLoad[i].my.ToString();
                    txtMz.Text = iApp.AppDocument.MemberBeamLoad[i].mz.ToString();
                    return;
                }
            }
        }

        private bool isPresent(string loadcase, string nodeNo)
        {

            for (int i = 0; i < this.iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                if (this.iApp.AppDocument.MemberBeamLoad[i].loadcase == SpFuncs.getInt(loadcase) && 
                    iApp.AppDocument.MemberBeamLoad[i].Node == SpFuncs.getInt(nodeNo))
                {
                    return true;
                }
            }
            return false;
        }

        private void txtLoadCase_TextChanged(object sender, EventArgs e)
        {
            setList(SpFuncs.getInt(txtLoadCase.Text));
            if (lst.Count > 0 && txtNodeNo.Text == "")
            {
                txtNodeNo.Text = lst[0].ToString();
            }
            setNodalLoadData(SpFuncs.getInt(txtLoadCase.Text), SpFuncs.getInt(txtNodeNo.Text));
            
        }
        private void setList(int loadcase)
        {
            lst.Clear();
            for (int i = 0; i < this.iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                if (this.iApp.AppDocument.MemberBeamLoad[i].loadcase == loadcase)
                {
                    lst.Add(iApp.AppDocument.MemberBeamLoad[i].Node);
                }
            }
            lst.Sort();
        }

        private void deleteData(int loadcase, int nodeNo)
        {
            for (int i = 0; i < this.iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                if (this.iApp.AppDocument.MemberBeamLoad[i].loadcase == loadcase &&
                    this.iApp.AppDocument.MemberBeamLoad[i].Node == nodeNo)
                {
                    this.iApp.AppDocument.MemberBeamLoad.RemoveAt(i);
                    lst.Remove(nodeNo);
                    txtNodeNo.Text = "";
                }
            }
        }
        private void deleteData(int loadcase)
        {
            int i = 0;

            for ( i = 0; i < this.iApp.AppDocument.MemberBeamLoad.Count; i++)
            {
                if (this.iApp.AppDocument.MemberBeamLoad[i].loadcase == loadcase)
                {
                    this.iApp.AppDocument.MemberBeamLoad.RemoveAt(i);
                    i = -1;
                    txtLoadCase.Text = "";
                    txtNodeNo.Text = "";
                }
            }
        }

        private void txtNodeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox txt = (TextBox)sender;

                if (txt.Name == "txtNodeNo")
                    txtFx.Focus();
                else if (txt.Name == "txtFx")
                    txtFy.Focus();
                else if (txt.Name == "txtFy")
                    txtFz.Focus();
                else if (txt.Name == "txtFz")
                    txtMx.Focus();
                else if (txt.Name == "txtMx")
                    txtMy.Focus();
                else if (txt.Name == "txtMy")
                    txtMz.Focus();
                else if (txt.Name == "txtMz")
                    btnNextNodalData.Focus();
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iApp.AppDocument.MemberBeamLoad = mblcCancel;
            this.Close();
        }
    }
}