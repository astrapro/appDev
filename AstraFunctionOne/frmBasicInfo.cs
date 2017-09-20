using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace AstraFunctionOne
{
    internal partial class frmBasicInfo : Form
    {
        IApplication app;

        public frmBasicInfo(IApplication aApp)
        {
            InitializeComponent();
            this.app = aApp;


        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            app.AppDocument.BasicInfo.UsersTitle = this.txtProjectTitle.Text;
            app.AppDocument.BasicInfo.Type = (StructureType)(lbStructureType.SelectedIndex + 1);
            app.AppDocument.BasicInfo.RunningOption = (short)((rbtnProblemSolution.Checked) ? 0 : 1);
            app.AppDocument.BasicInfo.LengthUnit = (eLengthUnits)cmbLengthUnit.SelectedIndex;
            app.AppDocument.BasicInfo.MassUnit = (EMassUnits)cmbMassUnit.SelectedIndex;


            //this.app.AppDocument.ProjectTitle = txtAst.Text + " " + this.txtProjectTitile.Text;
            //switch (lbStructureType.SelectedIndex)
            //{
            //    case 0:
            //        this.app.AppDocument.StructureType = AstraInterface.DataStructure.StructureType.Space;
            //        break;
            //    case 1:
            //        this.app.AppDocument.StructureType = AstraInterface.DataStructure.StructureType.Floor;
            //        break;
            //    case 2:
            //        this.app.AppDocument.StructureType = AstraInterface.DataStructure.StructureType.Plane;
            //        break;
            //}
            //this.app.AppDocument.Modex = (rbtnProblemSolution.Checked ? (short)0 : (short)1);
            //SetUnit();
            this.Close();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTitle_Load(object sender, EventArgs e)
        {
            lbStructureType.SelectedIndex = ((int)app.AppDocument.BasicInfo.Type -1);
            txtProjectTitle.Text = app.AppDocument.BasicInfo.UsersTitle;
            if (app.AppDocument.BasicInfo.RunningOption == 0) rbtnProblemSolution.Checked = true;
            else if (app.AppDocument.BasicInfo.RunningOption == 1) rbtnDataCheakOnly.Checked = true;
            cmbMassUnit.SelectedIndex = (int)app.AppDocument.BasicInfo.MassUnit;
            cmbLengthUnit.SelectedIndex = (int)app.AppDocument.BasicInfo.LengthUnit;


            //if (this.app.AppDocument.ProjectTitle.Length > 12)
            //    this.txtProjectTitile.Text = this.app.AppDocument.ProjectTitle.Remove(0, 12);

            //if (this.app.AppDocument.StructureType == AstraInterface.DataStructure.StructureType.Space)
            //{
            //    lbStructureType.SelectedIndex = 0;
            //}
            //else if (this.app.AppDocument.StructureType == AstraInterface.DataStructure.StructureType.Floor)
            //{
            //    lbStructureType.SelectedIndex = 1;
            //}
            //else if (this.app.AppDocument.StructureType == AstraInterface.DataStructure.StructureType.Plane)
            //{
            //    lbStructureType.SelectedIndex = 2;
            //}
            //if (this.app.AppDocument.Modex == 0) rbtnProblemSolution.Checked = true;
            //else if (this.app.AppDocument.Modex == 1) rbtnDataCheakOnly.Checked = true;

            //cmbLengthUnit.SelectedIndex = CAstraUnits.GetLengthUnitIndex(CAstraUnits.GetFact(this.app.AppDocument.LUnit));
            //cmbMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(CAstraUnits.GetFact(this.app.AppDocument.MUnit));

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lbStructureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAst.Text = "ASTRA " + lbStructureType.SelectedItem.ToString();
        }
        private void SetUnit()
        {
            if ((this.app.AppDocument.wunit_flag == 0 && this.app.AppDocument.lunit_flag == 0) ||
                ((this.app.AppDocument.MUnit == cmbMassUnit.Text && this.app.AppDocument.LUnit == cmbLengthUnit.Text)))
            {
                this.app.AppDocument.MUnit = cmbMassUnit.Text;
                this.app.AppDocument.LUnit = cmbLengthUnit.Text;

                short fl = this.app.AppDocument.wunit_flag;
                this.app.AppDocument.WFact = CAstraUnits.GetMassFact(cmbMassUnit.Text, ref fl);
                this.app.AppDocument.wunit_flag = fl;
                fl = this.app.AppDocument.lunit_flag;
                this.app.AppDocument.LFact = CAstraUnits.GetLengthFact(cmbLengthUnit.Text, ref fl);
                this.app.AppDocument.lunit_flag = fl;

                this.app.AppDocument.NodeData.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.NodeData.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.MatPropertyInfo.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.MatPropertyInfo.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.AreaLoad.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.AreaLoad.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.JointNodalLoad.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.JointNodalLoad.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.MemberBeamLoad.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.MemberBeamLoad.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.MaterialProperty.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.MaterialProperty.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.SectionProperty.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.SectionProperty.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);

                this.app.AppDocument.BeamConnectivity.LengthFactor = CAstraUnits.GetFact(this.app.AppDocument.LUnit);
                this.app.AppDocument.BeamConnectivity.MassFactor = CAstraUnits.GetFact(this.app.AppDocument.MUnit);
            }
            else if (!(this.app.AppDocument.MUnit == cmbMassUnit.Text && this.app.AppDocument.LUnit == cmbLengthUnit.Text))
            {
                MessageBox.Show(this, "You can't change the unit, unit has already taken.\n" +
                    "\n       Length Unit = " + this.app.AppDocument.LUnit + ", Mass Unit = " + this.app.AppDocument.MUnit, "ASTRA pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtProjectTitile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbMassUnit.Focus();
            }
        }

        private void cmbMassUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbLengthUnit.Focus();
            }
        }

        private void cmbLengthUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnOK.Focus();
            }
        }
    }
}