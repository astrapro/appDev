using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using System.Threading;
namespace AstraFunctionOne
{
    internal partial class frmPlateAndShell : Form
    {
        IApplication app;
        List<CMaterialPropertyInformation> MatInfo = new List<CMaterialPropertyInformation>();
        List<CElementMultiplier> ElMult = new List<CElementMultiplier>();
        List<CElementData> ElData = new List<CElementData>();

        public frmPlateAndShell(IApplication app)
        {
            InitializeComponent();
            this.app = app;
        }

        public int ElementNo
        {
            get
            {
                return SpFuncs.getInt(txtElementNo.Text);
            }
            set
            {
                if (value > 0)
                    txtElementNo.Text = value.ToString();
            }
        }
        public int Node1
        {
            get
            {
                return SpFuncs.getInt(txtNode1.Text);
            }
            set
            {
                    txtNode1.Text = value.ToString();
            }
        }
        public int Node2
        {
            get
            {
                return SpFuncs.getInt(txtNode2.Text);
            }
            set
            {
                    txtNode2.Text = value.ToString();
            }
        }
        public int Node3
        {
            get
            {
                return SpFuncs.getInt(txtNode3.Text);
            }
            set
            {
                txtNode3.Text = value.ToString();
            }
        }
        public int Node4
        {
            get
            {
                return SpFuncs.getInt(txtNode4.Text);
            }
            set
            {
                txtNode4.Text = value.ToString();
            }
        }
        public double ElementThickness
        {
            get
            {
                return SpFuncs.getDouble(txtElementThick.Text);
            }
            set
            {
                txtElementThick.Text = value.ToString("0.0");
            }
        }




        #region Working Function
        private CElementData GetCElementData()
        {
            CElementData ced = new CElementData();
            ced.elementNo = SpFuncs.getInt(txtElementNo.Text);
            ced.node1 = SpFuncs.getInt(txtNode1.Text);
            ced.node2 = SpFuncs.getInt(txtNode2.Text);
            ced.node3 = SpFuncs.getInt(txtNode3.Text);
            ced.node4 = SpFuncs.getInt(txtNode4.Text);
            ced.matId = SpFuncs.getInt(txtMatIdRef.Text);
            ced.elementThickness = SpFuncs.getDouble(txtElementThick.Text);
            ced.distLateralPressure = SpFuncs.getDouble(txtDistLatPressure.Text);
            ced.meanTempVar = SpFuncs.getDouble(txtMeanTempVar.Text);
            ced.meanTempGrad = SpFuncs.getDouble(txtMeanTempGrad.Text);
            return ced;
        }
        private CMaterialPropertyInformation GetCMaterialPropertyInformation()
        {
            CMaterialPropertyInformation cmpi = new CMaterialPropertyInformation();
            cmpi.matIdNo = SpFuncs.getInt(txtMatId.Text);
            cmpi.massDen = SpFuncs.getDouble(txtMassDensity.Text);
            cmpi.alphaX = SpFuncs.getDouble(txtAlphaX.Text);
            cmpi.alphaY = SpFuncs.getDouble(txtAlphaY.Text);
            cmpi.alphaZ = SpFuncs.getDouble(txtAlphaZ.Text);
            cmpi.Exx = SpFuncs.getDouble(txtExx.Text);
            cmpi.Exy = SpFuncs.getDouble(txtExy.Text);
            cmpi.Exs = SpFuncs.getDouble(txtExs.Text);
            cmpi.Eyy = SpFuncs.getDouble(txtEyy.Text);
            cmpi.Eys = SpFuncs.getDouble(txtEys.Text);
            cmpi.Gxy = SpFuncs.getDouble(txtGxy.Text);

            return cmpi;
        }
        public void ShowMatInfo(int matIdNo)
        {
            int indx = this.app.AppDocument.MatPropertyInfo.IndexOf(matIdNo);
            if (indx != -1)
            {
                txtAlphaX.Text = this.app.AppDocument.MatPropertyInfo[indx].alphaX.ToString();
                txtAlphaY.Text = this.app.AppDocument.MatPropertyInfo[indx].alphaY.ToString();
                txtAlphaZ.Text = this.app.AppDocument.MatPropertyInfo[indx].alphaZ.ToString();
                txtExx.Text = this.app.AppDocument.MatPropertyInfo[indx].Exx.ToString();
                txtExy.Text = this.app.AppDocument.MatPropertyInfo[indx].Exy.ToString();
                txtExs.Text = this.app.AppDocument.MatPropertyInfo[indx].Exs.ToString();
                txtEyy.Text = this.app.AppDocument.MatPropertyInfo[indx].Eyy.ToString();
                txtEys.Text = this.app.AppDocument.MatPropertyInfo[indx].Eys.ToString();
                txtGxy.Text = this.app.AppDocument.MatPropertyInfo[indx].Gxy.ToString();
            }
        }
        public void UpdateElementMultiplier(int selectedIndex)
        {
            CElementMultiplier cem = new CElementMultiplier();
            cem.Pressure = SpFuncs.getDouble(txtPressure.Text);
            cem.thermalEffects = SpFuncs.getDouble(txtThermalEffects.Text);
            cem.XAcceleration = SpFuncs.getDouble(txtXAcceleration.Text);
            cem.YAcceleration = SpFuncs.getDouble(txtYAcceleration.Text);
            cem.ZAcceleration = SpFuncs.getDouble(txtZAcceleration.Text);
            this.app.AppDocument.ElementMultiplier[selectedIndex] = cem;
        }
        public void ShowElementData(int ElementNo)
        {
            int indx = this.app.AppDocument.ElementData.IndexOf(ElementNo);
            if (indx != -1)
            {
                txtNode1.Text = this.app.AppDocument.ElementData[indx].node1.ToString();
                txtNode2.Text = this.app.AppDocument.ElementData[indx].node2.ToString();
                txtNode3.Text = this.app.AppDocument.ElementData[indx].node3.ToString();
                txtNode4.Text = this.app.AppDocument.ElementData[indx].node4.ToString();
                txtMatIdRef.Text = this.app.AppDocument.ElementData[indx].matId.ToString();
                txtElementThick.Text = this.app.AppDocument.ElementData[indx].elementThickness.ToString();
                txtDistLatPressure.Text = this.app.AppDocument.ElementData[indx].distLateralPressure.ToString();
                txtMeanTempVar.Text = this.app.AppDocument.ElementData[indx].meanTempVar.ToString();
                txtMeanTempGrad.Text = this.app.AppDocument.ElementData[indx].meanTempGrad.ToString();
            }
            else
            {
                txtNode1.Text = "";
                txtNode2.Text = "";
                txtNode3.Text = "";
                txtNode4.Text = "";
                txtMatIdRef.Text = "";
                txtElementThick.Text = "";
                txtDistLatPressure.Text = "";
                txtMeanTempVar.Text = "";
                txtMeanTempGrad.Text = "";
            }
        }
        public void ShowElementMultiplier(int index)
        {
            txtPressure.Text = this.app.AppDocument.ElementMultiplier[index].Pressure.ToString();
            txtThermalEffects.Text = this.app.AppDocument.ElementMultiplier[index].thermalEffects.ToString();
            txtXAcceleration.Text = this.app.AppDocument.ElementMultiplier[index].XAcceleration.ToString();
            txtYAcceleration.Text = this.app.AppDocument.ElementMultiplier[index].YAcceleration.ToString();
            txtZAcceleration.Text = this.app.AppDocument.ElementMultiplier[index].ZAcceleration.ToString();
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.app.AppDocument.ElementData.Clear();
            this.app.AppDocument.ElementMultiplier.Clear();
            this.app.AppDocument.MatPropertyInfo.Clear();

            int i = 0;
            for (i = 0; i < MatInfo.Count; i++)
            {
                this.app.AppDocument.MatPropertyInfo.Add(MatInfo[i]);
            }
            for (i = 0; i < ElData.Count; i++)
            {
                this.app.AppDocument.ElementData.Add(ElData[i]);
            }
            for (i = 0; i < ElMult.Count; i++)
            {
                this.app.AppDocument.ElementMultiplier.Add(ElMult[i]);
            }
            MatInfo.Clear();
            ElData.Clear();
            ElMult.Clear();
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.app.AppDocument.MatPropertyInfo.MassFactor = CAstraUnits.GetFact(cmbMassUnit.Text);
            this.app.AppDocument.MatPropertyInfo.LengthFactor = CAstraUnits.GetFact(cmbLengthUnit.Text);
            MatInfo.Clear();
            ElData.Clear();
            ElMult.Clear();
            this.Close();
        }
        private void btnNextProp_Click(object sender, EventArgs e)
        {
            int indx = this.app.AppDocument.MatPropertyInfo.IndexOf(SpFuncs.getInt(txtMatId.Text));
            if (indx == -1)
            {
                this.app.AppDocument.MatPropertyInfo.Add(GetCMaterialPropertyInformation());
            }
            else
            {
                this.app.AppDocument.MatPropertyInfo[indx] =  GetCMaterialPropertyInformation();
            }
            txtMatId.Text = (SpFuncs.getInt(txtMatId.Text) + 1) + "";
            txtMassDensity.Focus();
        }
        private void btnNextElement_Click(object sender, EventArgs e)
        {
            CElementData eldata = GetCElementData();
            int index = this.app.AppDocument.ElementData.IndexOf(ElementNo);
            if (SpFuncs.getInt(txtElementNo.Text) > 0)
            {

                if (index == -1)
                {
                    this.app.AppDocument.ElementData.Add(GetCElementData());
                }
                else
                {
                    this.app.AppDocument.ElementData[index] = GetCElementData();
                }
            }
            txtElementNo.Text = (SpFuncs.getInt(txtElementNo.Text) + 1) + "";
            txtElementNo.Focus();
        }
        private void frmPlateAndShell_Load(object sender, EventArgs e)
        {
            cmbLengthUnit.SelectedIndex = CAstraUnits.GetLengthUnitIndex(this.app.AppDocument.MatPropertyInfo.LengthFactor);
            cmbMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(this.app.AppDocument.MatPropertyInfo.MassFactor);
            if (this.app.AppDocument.ElementMultiplier.Count != 4)
            {
                for (byte b = 1; b <= 4; b++)
                {
                    CElementMultiplier em = new CElementMultiplier();
                    em.loadcase = b;
                    this.app.AppDocument.ElementMultiplier.Add(em);
                }
            }

            int i = 0;
            MatInfo.Clear();
            ElMult.Clear();
            ElData.Clear();

            for (i = 0; i < this.app.AppDocument.MatPropertyInfo.Count; i++)
            {
                MatInfo.Add(this.app.AppDocument.MatPropertyInfo[i]);
            }
            for (i = 0; i < this.app.AppDocument.ElementMultiplier.Count; i++)
            {
                ElMult.Add(this.app.AppDocument.ElementMultiplier[i]);
            }
            for (i = 0; i < this.app.AppDocument.ElementData.Count; i++)
            {
                ElData.Add(this.app.AppDocument.ElementData[i]);
            }
            cmbLoadCase.SelectedIndex = 0;
            ShowMatInfo(SpFuncs.getInt(txtMatId.Text));
            txtMatId.Focus();

        }
        private void btnElNext_Click(object sender, EventArgs e)
        {
            UpdateElementMultiplier(cmbLoadCase.SelectedIndex);
            if (cmbLoadCase.SelectedIndex > -1 && cmbLoadCase.SelectedIndex < 3)
            {
                cmbLoadCase.SelectedIndex += 1;
            }
            txtPressure.Focus();
        }
        private void btnElPrev_Click(object sender, EventArgs e)
        {
            UpdateElementMultiplier(cmbLoadCase.SelectedIndex);
            if (cmbLoadCase.SelectedIndex > 0 && cmbLoadCase.SelectedIndex < 4)
                cmbLoadCase.SelectedIndex -= 1;
        }
        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowElementMultiplier(cmbLoadCase.SelectedIndex);
        }
        private void txtMatId_TextChanged(object sender, EventArgs e)
        {
            ShowMatInfo(SpFuncs.getInt(txtMatId.Text));
        }
        private void btnPrevProp_Click(object sender, EventArgs e)
        {
            int indx = this.app.AppDocument.MatPropertyInfo.IndexOf(SpFuncs.getInt(txtMatId.Text));
            if (indx != -1)
            {
                this.app.AppDocument.MatPropertyInfo[indx] = GetCMaterialPropertyInformation();
            } 
            if (SpFuncs.getInt(txtMatId.Text) > 1)
            {
                txtMatId.Text = (SpFuncs.getInt(txtMatId.Text) - 1) + "";
                
            }
        }
        private void txtElementNo_TextChanged(object sender, EventArgs e)
        {
            ShowElementData(SpFuncs.getInt(txtElementNo.Text));
        }
        private void btnPrevElement_Click(object sender, EventArgs e)
        {
            if (SpFuncs.getInt(txtElementNo.Text) > 1)
            {
                int indx = this.app.AppDocument.ElementData.IndexOf(SpFuncs.getInt(txtElementNo.Text));
                if (indx != -1)
                {
                    this.app.AppDocument.ElementData[indx] = GetCElementData();
                }
                else
                {
                    indx = this.app.AppDocument.ElementData.Count - 1;
                    txtElementNo.Text = (this.app.AppDocument.ElementData[indx].elementNo) + "";
                    return;
                }
                if (indx > 0)
                {
                    txtElementNo.Text = (this.app.AppDocument.ElementData[indx - 1].elementNo) + "";
                }
                if (indx == 0)
                {
                    txtElementNo.Text = (this.app.AppDocument.ElementData[0].elementNo) + "";
                }
            }
        }
        private void btnDelProp_Click(object sender, EventArgs e)
        {
            //lblStatus.Text = (this.app.AppDocument.MatPropertyInfo.DeleteMatId(SpFuncs.getInt(txtMatId.Text)) + "");
            ShowMatInfo(SpFuncs.getInt(txtMatId.Text));
        }
        private void btnDelElement_Click(object sender, EventArgs e)
        {
            int index = this.app.AppDocument.ElementData.IndexOf(SpFuncs.getInt(txtElementNo.Text));
            if (index != -1)
            {
                this.app.AppDocument.ElementData.RemoveAt(index);
                if (index < this.app.AppDocument.ElementData.Count)
                {
                    txtElementNo.Text = this.app.AppDocument.ElementData[index].elementNo.ToString();
                }
            }
        }

        private void PlateAndShellElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = sender as Control;
                if (ctrl.Name == txtMatId.Name)
                {
                    cmbMassUnit.Focus();
                }
                else if (ctrl.Name == cmbMassUnit.Name)
                {
                    cmbLengthUnit.Focus();
                }
                else if (ctrl.Name == cmbLengthUnit.Name)
                {
                    txtMassDensity.Focus();
                }
                else if (ctrl.Name == txtMassDensity.Name)
                {
                    txtAlphaX.Focus();
                }
                else if (ctrl.Name == txtAlphaX.Name)
                {
                    txtAlphaY.Focus();
                }
                else if (ctrl.Name == txtAlphaY.Name)
                {
                    txtAlphaZ.Focus();
                }
                else if (ctrl.Name == txtAlphaZ.Name)
                {
                    txtExx.Focus();
                }
                else if (ctrl.Name == txtExx.Name)
                {
                    txtExy.Focus();
                }
                else if (ctrl.Name == txtExy.Name)
                {
                    txtExs.Focus();
                }
                else if (ctrl.Name == txtExs.Name)
                {
                    txtEyy.Focus();
                }
                else if (ctrl.Name == txtEyy.Name)
                {
                    txtEys.Focus();
                }
                else if (ctrl.Name == txtEys.Name)
                {
                    txtGxy.Focus();
                }
                else if (ctrl.Name == txtGxy.Name)
                {
                    btnNextProp.Focus();
                }
                else if (ctrl.Name == txtPressure.Name)
                {
                    txtThermalEffects.Focus();
                }
                else if (ctrl.Name == txtThermalEffects.Name)
                {
                    txtXAcceleration.Focus();
                }
                else if (ctrl.Name == txtXAcceleration.Name)
                {
                    txtYAcceleration.Focus();
                }
                else if (ctrl.Name == txtYAcceleration.Name)
                {
                    txtZAcceleration.Focus();
                }
                else if (ctrl.Name == txtZAcceleration.Name)
                {
                    btnElNext.Focus();
                }
                else if (ctrl.Name == txtElementNo.Name)
                {
                    txtNode1.Focus();
                }
                else if (ctrl.Name == txtNode1.Name)
                {
                    txtNode2.Focus();
                }
                else if (ctrl.Name == txtNode2.Name)
                {
                    txtNode3.Focus();
                }
                else if (ctrl.Name == txtNode3.Name)
                {
                    txtNode4.Focus();
                }
                else if (ctrl.Name == txtNode4.Name)
                {
                    txtMatIdRef.Focus();
                }
                else if (ctrl.Name == txtMatIdRef.Name)
                {
                    txtElementThick.Focus();
                }
                else if (ctrl.Name == txtElementThick.Name)
                {
                    txtDistLatPressure.Focus();
                }
                else if (ctrl.Name == txtDistLatPressure.Name)
                {
                    txtMeanTempVar.Focus();
                }
                else if (ctrl.Name == txtMeanTempVar.Name)
                {
                    txtMeanTempGrad.Focus();
                }
                else if (ctrl.Name == txtMeanTempGrad.Name)
                {
                    btnNextElement.Focus();
                }

                
            }
        }
    }

   
}