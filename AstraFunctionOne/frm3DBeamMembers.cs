using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using System.Threading;
using System.IO;
namespace AstraFunctionOne
{

    internal partial class frm3DBeamMembers : Form
    {
        IApplication app;
        CBeamConnectivityRelease bcr = new CBeamConnectivityRelease();
        CMemberConnectivityCollection bcc = new CMemberConnectivityCollection();
        int i, start, end;
        int connCount = 1,matCount = 1,secCount = 1,loadCount = 1;
        List<CMaterialProperty> MatList = new List<CMaterialProperty>();
        List<CSectionProperty> SecList = new List<CSectionProperty>();
        List<CJointNodalLoad> JointLoadList = new List<CJointNodalLoad>();
        List<CBeamConnectivityRelease> MemberReleaseList = new List<CBeamConnectivityRelease>();
        List<CLoadGeneration> LoadGen = new List<CLoadGeneration>();
        CAreaLoad area;
        CLoadGeneration lgn;
        CMovingLoad mload;
        LoadComb loadComb = new LoadComb();
        Thread thd;

        #region Public Property

        public int MemberNo
        {
            get
            {
                return SpFuncs.getInt(txtMemberNumber.Text, -1);
            }
            set
            {
                if (value > 0)
                    txtMemberNumber.Text = value.ToString();
            }
        }
        public int MemberIncr
        {
            get
            {
                return SpFuncs.getInt(txtMemberNoIncr.Text);
            }
        }
        public int StartNodeIncr
        {
            get
            {
                return SpFuncs.getInt(txtSrtNdIncr.Text);
            }
        }
        public int EndNodeIncr
        {
            get
            {
                return SpFuncs.getInt(txtEdIncr.Text);
            }
        }
        public int EndNode
        {
            get
            {
                return SpFuncs.getInt(txtEndNode.Text);
            }
            set
            {
                txtEndNode.Text = value.ToString();
            }
        }
        public int StartNode
        {
            get
            {
                return SpFuncs.getInt(txtStartNode.Text);
            }
            set
            {
                txtStartNode.Text = value.ToString();
            }
        }
        public int SectionID
        {
            get
            {
                return SpFuncs.getInt(txtS_P_N.Text);
            }
            set
            {
                if (value > 0)
                    txtS_P_N.Text = value.ToString();
            }
        }
        public int JointLoadCase
        {
            get 
            {
                return SpFuncs.getInt(txtJointLoadCase.Text); 
            }
            set
            {
                if (value > 0)
                    txtJointLoadCase.Text = value.ToString();
            }
        }
        public int JointLoadNo
        {
            get
            {
                return SpFuncs.getInt(txtJointLoadNo.Text);
            }
            set
            {
                if (value > 0)
                    txtJointLoadNo.Text = value.ToString();
            }
        }
        public int JointLoadCaseNo
        {
            get
            {
                return SpFuncs.getInt(txtLoadCaseNo.Text);
            }
            set
            {
                if (value > 0)
                    txtLoadCaseNo.Text = value.ToString();
            }

        }
        public double MultiplyingFactor
        {
            get
            {
                return SpFuncs.getDouble(txtMultiplyinorgFact.Text);
            }
            set
            {
                txtMultiplyinorgFact.Text = value.ToString();
            }
        }
        public int MovingSerialNo
        {
            get
            {
                return SpFuncs.getInt(txtMovingSerialNo.Text);
            }
            set
            {
                if (value > 0)
                {
                    txtMovingSerialNo.Text = "";
                    txtMovingSerialNo.Text = value.ToString();
                }
            }
        }

        public int AreaLoadNo
        {
            get
            {
                return SpFuncs.getInt(txtAreaLoadNo.Text);
            }
            set
            {
                if (value > 0 && value <= app.AppDocument.AreaLoad.MaxLoadNo(AreaLoadCase))
                    txtAreaLoadNo.Text = value.ToString();
            }
        }
        public int AreaLoadCase
        {
            get
            {
                return SpFuncs.getInt(txtAreaLoadcase.Text);
            }
            set
            {
                if (value > 0)
                    txtAreaLoadcase.Text = value.ToString();
            }
        }

        #endregion

        #region Work Functions

        public void SetLoadGeneration(string sType)
        {
            LoadGen.Clear();
            int slno = 1;
            for (int i = 0; i < app.AppDocument.LoadGeneration.Count; i++)
            {
                if (app.AppDocument.LoadGeneration[i].Type == sType)
                {
                    app.AppDocument.LoadGeneration[i].SerialNo = slno;
                    LoadGen.Add(app.AppDocument.LoadGeneration[i]);
                    slno++;
                }
           }
        }
        public void SetLocalDirection()
        {
            txtLocalDirection.Text = getJointSequenceLCS(JointLoadCase,JointLoadNo);
            txtGlobalDirection.Text = getJointSequenceGCS(JointLoadCase, JointLoadNo);
            setJointLoad(JointLoadCase,JointLoadNo);           
        }
        private void addBeamConnRelease()
        {
            int index = this.app.AppDocument.BeamConnectivityRelease.IndexOf(SpFuncs.getInt(txtMemberNumber.Text));

            if (chkStartTx.Checked == false && chkStartTy.Checked == false &&
                chkStartTz.Checked == false && chkStartRx.Checked == false &&
                chkStartRy.Checked == false && chkStartRz.Checked == false &&
                chkEndTx.Checked == false && chkEndTy.Checked == false &&
                chkEndTz.Checked == false && chkEndRx.Checked == false
                && chkEndRy.Checked == false && chkEndRz.Checked == false)
            {
                if (index != -1)
                {
                    this.app.AppDocument.BeamConnectivityRelease.RemoveAt(index);
                }
                return;
            }
            else
            {
                bcr = new CBeamConnectivityRelease();
                bcr.member = SpFuncs.getInt(txtMemberNumber.Text);

                bcr.N1 = "N1";
                bcr.FX1 = chkStartTx.Checked;
                bcr.FY1 = chkStartTy.Checked;
                bcr.FZ1 = chkStartTz.Checked;
                bcr.MX1 = chkStartRx.Checked;
                bcr.MY1 = chkStartRy.Checked;
                bcr.MZ1 = chkStartRz.Checked;

                bcr.N2 = "N2";
                bcr.FX2 = chkEndTx.Checked;
                bcr.FY2 = chkEndTy.Checked;
                bcr.FZ2 = chkEndTz.Checked;
                bcr.MX2 = chkEndRx.Checked;
                bcr.MY2 = chkEndRy.Checked;
                bcr.MZ2 = chkEndRz.Checked;

                if (index != -1)
                {
                    this.app.AppDocument.BeamConnectivityRelease[index] = bcr;
                }
                else
                {
                    this.app.AppDocument.BeamConnectivityRelease.Add(bcr);
                }
            }
        }
        private CMemberConnectivity GetBeamConnectivity(int Node1, int Node2, int elementNo)
        {
            int i = 0;
            CMemberConnectivity bc = new CMemberConnectivity();

            for (i = 0; i < app.AppDocument.NodeData.Count; i++)
            {
                if (app.AppDocument.NodeData[i].NodeNo == Node1)
                {
                    bc.Node1 = Node1;
                    bc.X1 = app.AppDocument.NodeData[i].X;
                    bc.Y1 = app.AppDocument.NodeData[i].Y;
                    bc.Z1 = app.AppDocument.NodeData[i].Z;
                    break;
                }
            }
            for (i = 0; i < app.AppDocument.NodeData.Count; i++)
            {
                if (app.AppDocument.NodeData[i].NodeNo == Node2)
                {
                    bc.Node2 = Node2;
                    bc.X2 = app.AppDocument.NodeData[i].X;
                    bc.Y2 = app.AppDocument.NodeData[i].Y;
                    bc.Z2 = app.AppDocument.NodeData[i].Z;
                    break;
                }
            }
            bc.memberNo = elementNo;
            return bc;
        }
        private CSectionProperty getCSectionProperty(string memberNo)
        {

            return getCSectionProperty(SpFuncs.getInt(memberNo));
        }
        private CSectionProperty getCSectionProperty(int memberNo)
        {
            setArea(); // if area is not defind then it is calculated area by using B,D ,DO and DI
            CSectionProperty sp = new CSectionProperty();
            sp.memberNo = memberNo;
            sp.sectionId = SpFuncs.getInt(txtS_P_N.Text);
            sp.B = SpFuncs.getDouble(txtWidth.Text);
            sp.D = SpFuncs.getDouble(txtDepth.Text);
            sp.outerD = SpFuncs.getDouble(txtOuterDiameter.Text);
            sp.innerD = SpFuncs.getDouble(txtInnerDiameter.Text);
            sp.area = SpFuncs.getDouble(txtCrossSectionArea.Text);
            sp.ix = SpFuncs.getDouble(txtMomentOfInertiaIx.Text);
            sp.iy = SpFuncs.getDouble(txtMomentOfInertiaIy.Text);
            sp.iz = SpFuncs.getDouble(txtMomentOfInertiaIz.Text);
            return sp;
        }
        private CMaterialProperty getCMaterialProperty(int memberNo)
        {
            CMaterialProperty mp = new CMaterialProperty();
            mp.memberNo = memberNo;
            mp.matID = SpFuncs.getInt(txtM_I_N.Text);
            mp.emod = SpFuncs.getDouble(txtElasticModulus.Text);
            mp.pr = SpFuncs.getDouble(txtPositionRatio.Text);
            mp.mden = SpFuncs.getDouble(txtMassDensity.Text);
            mp.wden = SpFuncs.getDouble(txtWeightDensity.Text);
            mp.alpha = SpFuncs.getDouble(txtAlpha.Text);
            mp.beta = 0.0;
            return mp;
        }
        private CMaterialProperty getCMaterialProperty(string elNo)
        {
            return getCMaterialProperty(SpFuncs.getInt(elNo));
        }
        private CJointNodalLoad getCJointNodalLoad(string elNo)
        {
            return getCJointNodalLoad(SpFuncs.getInt(elNo));
        }
        private CJointNodalLoad getCJointNodalLoad(int ElementNo)
        {
            CJointNodalLoad jnl = new CJointNodalLoad();
            jnl.loadcase = SpFuncs.getInt(txtJointLoadCase.Text);
            jnl.memberNo = ElementNo;

            if (cmbLoadType.SelectedIndex == 0)
            {
                if (cmbLoadDir.SelectedIndex == 0)
                {
                    jnl.udlx = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 1)
                {
                    jnl.udly = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 2)
                {
                    jnl.udlz = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 3)
                {
                    jnl.udlx = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "GCS";
                }
                else if (cmbLoadDir.SelectedIndex == 4)
                {
                    jnl.udly = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "GCS";
                }
                else if (cmbLoadDir.SelectedIndex == 5)
                {
                    jnl.udlz = SpFuncs.getDouble(txtLoad.Text);
                    jnl.mark = "GCS";
                }
            }
            else
            {
                if (cmbLoadDir.SelectedIndex == 0)
                {
                    jnl.px = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d1 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 1)
                {
                    jnl.py = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d2 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 2)
                {
                    jnl.pz = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d3 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "LCS";
                }
                else if (cmbLoadDir.SelectedIndex == 3)
                {
                    jnl.px = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d1 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "GCS";
                }
                else if (cmbLoadDir.SelectedIndex == 4)
                {
                    jnl.py = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d2 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "GCS";
                }
                else if (cmbLoadDir.SelectedIndex == 5)
                {
                    jnl.pz = SpFuncs.getDouble(txtLoad.Text);
                    jnl.d3 = SpFuncs.getDouble(txtDisFrmStrNd.Text);
                    jnl.mark = "GCS";
                }
            }
            return jnl;

        }

        public void SetLoadComb()
        {
            //loadComb = new LoadComb();
            loadComb.loadColl.Clear();
            loadComb.multFactColl.Clear();
            loadComb.loadCase = SpFuncs.getInt(txtJointLoadCase.Text);
            for (int i = 0; i < app.AppDocument.LoadCombination.Count; i++)
            {
                if (app.AppDocument.LoadCombination[i].loadCase == loadComb.loadCase)
                {
                    loadComb.loadColl.Add(app.AppDocument.LoadCombination[i].load);
                    loadComb.multFactColl.Add(app.AppDocument.LoadCombination[i].factor);
                }
            }
        }
        public void ShowStatus()
        {
            connCount = this.app.AppDocument.BeamConnectivity.IndexOf(SpFuncs.getInt(txtMemberNumber.Text));

            if (connCount == -1)
            {
                connCount = app.AppDocument.BeamConnectivity.Count;
            }
            lblConnStatus.Text = " Total Element = " + app.AppDocument.BeamConnectivity.Count +
                ", Current Position = " + (connCount + 1);

            lblLoadStatus.Text = " Total Element = " + app.AppDocument.JointNodalLoad.Count + ", Current Position = " + loadCount;
            lblMatStatus.Text = " Total Element = " + app.AppDocument.MaterialProperty.Count + ", Current Position = " + matCount;
            lblSecStatus.Text = " Total Element = " + app.AppDocument.SectionProperty.Count + ", Current Position = " + secCount;
        }
        public void showConnData(int index)
        {
            if (index != -1)
            {
                txtMemberNumber.Text = app.AppDocument.BeamConnectivity[index].memberNo.ToString();
                txtStartNode.Text = app.AppDocument.BeamConnectivity[index].Node1.ToString();
                txtEndNode.Text = app.AppDocument.BeamConnectivity[index].Node2.ToString();
            }
        }
        private void showMaterialData(int index)
        {
            txtM_I_N.Text = app.AppDocument.MaterialProperty[index].matID.ToString();
            txtElasticModulus.Text = app.AppDocument.MaterialProperty[index].emod.ToString();
            txtPositionRatio.Text = app.AppDocument.MaterialProperty[index].pr.ToString();
            txtAlpha.Text = app.AppDocument.MaterialProperty[index].alpha.ToString();
            txtMassDensity.Text = app.AppDocument.MaterialProperty[index].mden.ToString();
            txtWeightDensity.Text = app.AppDocument.MaterialProperty[index].wden.ToString();
            txtMatMemberNos.Text = app.AppDocument.MaterialProperty[index].memberNo.ToString();
        }
        public void firstLoadConn()
        {
            connCount = app.AppDocument.BeamConnectivity.Count;
            if (connCount > 0)
            {
                showConnData(0);
            }
            else if (connCount == 0)
            {
                connCount = 1;
            }
        }
        private string getJointSequenceLCS(int LoadCase,int LoadNo)
        {
            int notseq = 0;
            string sequence = " ";
            for (i = 0; i < this.app.AppDocument.JointNodalLoad.Count; i++)
            {
                if (this.app.AppDocument.JointNodalLoad[i].loadcase == LoadCase && 
                    this.app.AppDocument.JointNodalLoad[i].mark == "LCS" &&
                    this.app.AppDocument.JointNodalLoad[i].loadNo == LoadNo)
                {
                    notseq = this.app.AppDocument.JointNodalLoad[i].memberNo;
                    sequence += notseq + ",";
                }
            }
            return sequence.Substring(0, sequence.Length - 1);
        }
        private string getJointSequenceGCS(int LoadCase,int loadNo)
        {
            int notseq = 0;
            string sequence = " ";
            for (i = 0; i < this.app.AppDocument.JointNodalLoad.Count; i++)
            {
                if (this.app.AppDocument.JointNodalLoad[i].loadcase == LoadCase && 
                    this.app.AppDocument.JointNodalLoad[i].mark == "GCS" &&
                    this.app.AppDocument.JointNodalLoad[i].loadNo == loadNo)
                {
                    notseq = this.app.AppDocument.JointNodalLoad[i].memberNo;
                    sequence += notseq + ",";
                }
            }
            return sequence.Substring(0, sequence.Length - 1);
        }
        private string getMaterialSequence(int matId)
        {
            int notseq = 0;
            string sequence = " ";

            for (i = 0; i < this.app.AppDocument.MaterialProperty.Count; i++)
            {
                if (this.app.AppDocument.MaterialProperty[i].matID == matId)
                {
                    notseq = this.app.AppDocument.MaterialProperty[i].memberNo;
                    sequence += notseq + ",";
                }
            }
            return sequence.Substring(0, sequence.Length - 1);
        }
        private void setMaterialProperty(int matId)
        {
            for (i = 0; i < this.app.AppDocument.MaterialProperty.Count; i++)
            {
                if (this.app.AppDocument.MaterialProperty[i].matID == matId)
                {
                    txtElasticModulus.Text = this.app.AppDocument.MaterialProperty[i].emod.ToString();
                    txtPositionRatio.Text = this.app.AppDocument.MaterialProperty[i].pr.ToString();
                    txtAlpha.Text = this.app.AppDocument.MaterialProperty[i].alpha.ToString("0.000000");
                    txtMassDensity.Text = this.app.AppDocument.MaterialProperty[i].mden.ToString();
                    txtWeightDensity.Text = this.app.AppDocument.MaterialProperty[i].wden.ToString();
                    return;
                }
            }
        }
        private string getSectionSequence(int secId)
        {
            int seq = 0, notseq = 0;
            string sequence = " ";

            for (i = 0; i < this.app.AppDocument.SectionProperty.Count; i++)
            {
                if (this.app.AppDocument.SectionProperty[i].sectionId == secId)
                {
                    notseq = this.app.AppDocument.SectionProperty[i].memberNo;
                    sequence += notseq + ",";
                }
            }
            return sequence.Substring(0, sequence.Length - 1);
        }
        public void setSectionProperty(int secId)
        {
            for (i = 0; i < this.app.AppDocument.SectionProperty.Count; i++)
            {
                if (this.app.AppDocument.SectionProperty[i].sectionId == secId)
                {
                    txtWidth.Text = this.app.AppDocument.SectionProperty[i].B.ToString();
                    txtDepth.Text = this.app.AppDocument.SectionProperty[i].D.ToString();
                    txtOuterDiameter.Text = this.app.AppDocument.SectionProperty[i].outerD.ToString();
                    txtInnerDiameter.Text = this.app.AppDocument.SectionProperty[i].innerD.ToString();
                    txtCrossSectionArea.Text = this.app.AppDocument.SectionProperty[i].area.ToString();
                    txtMomentOfInertiaIx.Text = this.app.AppDocument.SectionProperty[i].ix.ToString();
                    txtMomentOfInertiaIy.Text = this.app.AppDocument.SectionProperty[i].iy.ToString();
                    txtMomentOfInertiaIz.Text = this.app.AppDocument.SectionProperty[i].iz.ToString();
                    return;
                }
            }
        }
        private void setJointLoad(int loadcase,int LoadNo)
        {
            for (i = 0; i < this.app.AppDocument.JointNodalLoad.Count; i++)
            {
                if (this.app.AppDocument.JointNodalLoad[i].loadcase == loadcase &&
                    this.app.AppDocument.JointNodalLoad[i].loadNo == LoadNo)
                {
                    if (this.app.AppDocument.JointNodalLoad[i].udlx != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].udlx.ToString();
                        cmbLoadType.SelectedIndex = 0;
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 3;
                        else
                            cmbLoadDir.SelectedIndex = 0;
                    }
                    else if (this.app.AppDocument.JointNodalLoad[i].udly != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].udly.ToString();
                        cmbLoadType.SelectedIndex = 0;
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 4;
                        else
                            cmbLoadDir.SelectedIndex = 1;
                    }
                    else if (this.app.AppDocument.JointNodalLoad[i].udlz != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].udlz.ToString();
                        cmbLoadType.SelectedIndex = 0;
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 5;
                        else
                            cmbLoadDir.SelectedIndex = 2;
                    }
                    else if (this.app.AppDocument.JointNodalLoad[i].px != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].px.ToString();
                        cmbLoadType.SelectedIndex = 1;
                        txtDisFrmStrNd.Text = this.app.AppDocument.JointNodalLoad[i].d1.ToString();
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 3;
                        else
                            cmbLoadDir.SelectedIndex = 0;
                    }
                    else if (this.app.AppDocument.JointNodalLoad[i].py != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].py.ToString();
                        cmbLoadType.SelectedIndex = 1;
                        txtDisFrmStrNd.Text = this.app.AppDocument.JointNodalLoad[i].d2.ToString();
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 4;
                        else
                            cmbLoadDir.SelectedIndex = 1;
                    }
                    else if (this.app.AppDocument.JointNodalLoad[i].pz != 0)
                    {
                        txtLoad.Text = this.app.AppDocument.JointNodalLoad[i].pz.ToString();
                        cmbLoadType.SelectedIndex = 1;
                        txtDisFrmStrNd.Text = this.app.AppDocument.JointNodalLoad[i].d3.ToString();
                        if (this.app.AppDocument.JointNodalLoad[i].mark == "GCS")
                            cmbLoadDir.SelectedIndex = 5;
                        else
                            cmbLoadDir.SelectedIndex = 2;
                    }
                    return;
                }
            }
        }
        private string getTrussElements()
        {
            string kStr = " ";
            if (this.app.AppDocument.MemberTruss.Count > 0)
            {
                for (i = 0; i < this.app.AppDocument.MemberTruss.Count; i++)
                {
                    kStr += this.app.AppDocument.MemberTruss[i].TRUSS_MEMB + ",";
                }
            }
            return kStr.Substring(0, kStr.Length - 1);
        }
        public void setArea()
        {
            if (rbtnDimentionSection.Checked)
            {
                double b, d, ix, iy, iz, area, id, od;
                area = b = d = ix = iy = iz = id = od = 0;
                b = SpFuncs.getDouble(txtWidth.Text);
                d = SpFuncs.getDouble(txtDepth.Text);

                id = SpFuncs.getDouble(txtInnerDiameter.Text);
                od = SpFuncs.getDouble(txtOuterDiameter.Text);

                if (b != 0.0 && d != 0.0)
                {
                    area = b * d;
                    ix = (b * d * d * d) / 12;
                    iy = (d * b * b * b) / 12;
                    iz = ix + iy;
                }
                else
                {
                    area = (((Math.PI * od * od) / 4) - ((Math.PI * id * id) / 4));
                    ix = ((Math.PI / 64) * ((od * od * od * od) - (id * id * id * id)));
                    iy = ix;
                    iz = ix + iy;
                }
                txtCrossSectionArea.Text = SpFuncs.setNumber(area, 7);
                txtMomentOfInertiaIx.Text = SpFuncs.setNumber(ix, 7);
                txtMomentOfInertiaIy.Text = SpFuncs.setNumber(iy, 7);
                txtMomentOfInertiaIz.Text = SpFuncs.setNumber(iz, 7);
            }
        }
        private void setElementConnectivity(int elementNo)
        {
            //txtStartNode.Text = "";
            //txtEndNode.Text = "";
            chkStartTx.Checked = false;
            chkStartTy.Checked = false;
            chkStartTz.Checked = false;
            chkStartRx.Checked = false;
            chkStartRy.Checked = false;
            chkStartRz.Checked = false;

            chkEndTx.Checked = false;
            chkEndTy.Checked = false;
            chkEndTz.Checked = false;
            chkEndRx.Checked = false;
            chkEndRy.Checked = false;
            chkEndRz.Checked = false;
            for (i = 0; i < this.app.AppDocument.BeamConnectivity.Count; i++)
            {
                if (this.app.AppDocument.BeamConnectivity[i].memberNo == elementNo)
                {
                    txtStartNode.Text = this.app.AppDocument.BeamConnectivity[i].Node1.ToString();
                    txtEndNode.Text = this.app.AppDocument.BeamConnectivity[i].Node2.ToString();
                    break;
                }
            }
            for (i = 0; i < this.app.AppDocument.BeamConnectivityRelease.Count; i++)
            {
                if (this.app.AppDocument.BeamConnectivityRelease[i].member == elementNo)
                {
                    chkStartTx.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FX1;
                    chkStartTy.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FY1;
                    chkStartTz.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FZ1;
                    chkStartRx.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MX1;
                    chkStartRy.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MY1;
                    chkStartRz.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MZ1;

                    chkEndTx.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FX2;
                    chkEndTy.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FY2;
                    chkEndTz.Checked = this.app.AppDocument.BeamConnectivityRelease[i].FZ2;
                    chkEndRx.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MX2;
                    chkEndRy.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MY2;
                    chkEndRz.Checked = this.app.AppDocument.BeamConnectivityRelease[i].MZ2;
                    return;
                }
            }
        }
        private bool deleteSection(int sectionId)
        {
            bool fl = false;
            for (i = 0; i < this.app.AppDocument.SectionProperty.Count; i++)
            {
                if (this.app.AppDocument.SectionProperty[i].sectionId == sectionId)
                {
                    this.app.AppDocument.SectionProperty.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            return fl;
        }
        private void matCancelWork()
        {
            this.app.AppDocument.MaterialProperty.Clear();
            for (i = 0; i < MatList.Count; i++)
            {
                this.app.AppDocument.MaterialProperty.Add(MatList[i]);
            }
            MatList.Clear();
        }
        private void secCencelWork()
        {
            this.app.AppDocument.SectionProperty.Clear();
            for (i = 0; i < SecList.Count; i++)
            {
                this.app.AppDocument.SectionProperty.Add(SecList[i]);
            }
        }
        private void JointCencelWork()
        {
            this.app.AppDocument.JointNodalLoad.Clear();
            for (i = 0; i < JointLoadList.Count; i++)
            {
                this.app.AppDocument.JointNodalLoad.Add(JointLoadList[i]);
            }
            JointLoadList.Clear();
        }
        private void elementCancelWork()
        {
            this.app.AppDocument.BeamConnectivity.Clear();
            for (i = 0; i < bcc.Count; i++)
            {
                this.app.AppDocument.BeamConnectivity.Add(bcc[i]);
            }
            bcc.Clear();

            this.app.AppDocument.BeamConnectivityRelease.Clear();
            for (i = 0; i < MemberReleaseList.Count; i++)
            {
                this.app.AppDocument.BeamConnectivityRelease.Add(MemberReleaseList[i]);
            }
            MemberReleaseList.Clear();

        }
        private void ElementLoad()
        {
            bcc.Clear();
            connCount = this.app.AppDocument.BeamConnectivity.Count - 1;
            for (i = 0; i < this.app.AppDocument.BeamConnectivity.Count; i++)
            {
                bcc.Add(this.app.AppDocument.BeamConnectivity[i]);
            }
            MemberReleaseList.Clear();
            for (i = 0; i < this.app.AppDocument.BeamConnectivityRelease.Count; i++)
            {
                MemberReleaseList.Add(this.app.AppDocument.BeamConnectivityRelease[i]);
            }
        }
        private void MaterialLoad()
        {
            MatList.Clear();
            for (i = 0; i < this.app.AppDocument.MaterialProperty.Count; i++)
            {
                MatList.Add(this.app.AppDocument.MaterialProperty[i]);
            }
        }
        private void SectionLoad()
        {
            SecList.Clear();
            for (int i = 0; i < this.app.AppDocument.SectionProperty.Count; i++)
            {
                SecList.Add(this.app.AppDocument.SectionProperty[i]);
            }
        }
        private void JointLoad()
        {
            JointLoadList.Clear();
            for (int i = 0; i < this.app.AppDocument.JointNodalLoad.Count; i++)
            {
                JointLoadList.Add(this.app.AppDocument.JointNodalLoad[i]);
            }
        }

        #endregion



        #region SET INDEX
        private void SectionProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = (Control)sender;
                if (ctrl.Name == txtS_P_N.Name)
                    rbtnDimentionSection.Focus();
                else if (ctrl.Name == rbtnDimentionSection.Name)
                    rbtnPrismatic.Focus();
                else if (ctrl.Name == rbtnPrismatic.Name)
                    txtWidth.Focus();
                else if (ctrl.Name == txtWidth.Name)
                    txtDepth.Focus();
                else if (ctrl.Name == txtDepth.Name)
                    rbtnSecAllMember.Focus();

                else if (ctrl.Name == rbtnCircular.Name)
                    txtOuterDiameter.Focus();
                else if (ctrl.Name == txtOuterDiameter.Name)
                    txtInnerDiameter.Focus();
                else if (ctrl.Name == txtInnerDiameter.Name)
                    rbtnSecAllMember.Focus();

                else if (ctrl.Name == rbtnCalculatedSection.Name)
                    txtCrossSectionArea.Focus();
                else if (ctrl.Name == txtCrossSectionArea.Name)
                    txtMomentOfInertiaIx.Focus();
                else if (ctrl.Name == txtMomentOfInertiaIx.Name)
                    txtMomentOfInertiaIy.Focus();
                else if (ctrl.Name == txtMomentOfInertiaIy.Name)
                    txtMomentOfInertiaIz.Focus();
                else if (ctrl.Name == txtMomentOfInertiaIz.Name)
                    rbtnSecAllMember.Focus();

                else if (ctrl.Name == rbtnSecAllMember.Name)
                    btnSectionApply.Focus();
                else if (ctrl.Name == rbtnSecMemberNo.Name)
                    txtSecMemberNos.Focus();
                else if (ctrl.Name == txtSecMemberNos.Name)
                    btnSectionApply.Focus();
                else if (ctrl.Name == rbtnSecMemberRange.Name)
                    txtSecStartMember.Focus();
                else if (ctrl.Name == txtSecStartMember.Name)
                    txtSecEndMember.Focus();

            }
        }
        private void AreaLoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = (Control)sender;
                if (ctrl.Name == txtAreaLoadcase.Name)
                {
                    txtAreaFy.Focus();
                }
                else if (ctrl.Name == txtAreaFy.Name)
                {
                    rbtnAreaAllMem.Focus();
                }
                else if (ctrl.Name == rbtnAreaAllMem.Name)
                {
                    btnAreaMemberApply.Focus();
                }
                else if (ctrl.Name == rbtnAreaMemNos.Name)
                {
                    txtAreaElementNos.Focus();
                }
                else if (ctrl.Name == txtAreaElementNos.Name)
                {
                    btnAreaMemberApply.Focus();
                }
                else if (ctrl.Name == rbtnAreaMemRabge.Name)
                {
                    txtAreaStartMember.Focus();
                }
                else if (ctrl.Name == txtAreaStartMember.Name)
                {
                    txtAreaEndMember.Focus();
                }
                else if (ctrl.Name == txtAreaEndMember.Name)
                {
                    btnAreaMemberApply.Focus();
                }
                

            }
        }
        private void MovingLoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = sender as Control;
                if (ctrl.Name == cmbMovLoadType.Name)
                {
                    cmbMovLoadName.Focus();
                }
                else if (ctrl.Name == cmbMovLoadName.Name)
                {
                    cmbMovImpactFactor.Focus();
                }
                else if (ctrl.Name == cmbMovImpactFactor.Name)
                {
                    btnMovNext.Focus();
                }
                else if (ctrl.Name == cmbMovMassUnit.Name)
                {
                    cmbMovLengthUnit.Focus();
                }
                else if (ctrl.Name == cmbMovLengthUnit.Name)
                {
                    txtMovLoadFileName.Focus();
                }
                else if (ctrl.Name == txtMovLoadFileName.Name)
                {
                    btnCreateTXTFile.Focus();
                }
                else if (ctrl.Name == cmbMovGenLoadType.Name)
                {
                    txtMovGenX.Focus();
                }
                else if (ctrl.Name == txtMovGenX.Name)
                {
                    txtMovGenZ.Focus();
                }
                else if (ctrl.Name == txtMovGenZ.Name)
                {
                    txtMovGenXInc.Focus();
                }
                else if (ctrl.Name == txtMovGenXInc.Name)
                {
                    txtMovGenRepeatTimes.Focus();
                }
                else if (ctrl.Name == txtMovGenRepeatTimes.Name)
                {
                    btnMovGenNext.Focus();
                }
            }

        }
        private void JointLoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control ctrl = sender as Control;
                if (ctrl.Name == txtJointLoadCase.Name)
                {
                    txtJointLoadNo.Focus();
                }
                else if (ctrl.Name == txtJointLoadNo.Name)
                {
                    cmbLoadType.Focus();
                }
                else if (ctrl.Name == cmbLoadType.Name)
                {
                    txtLoad.Focus();
                }
                else if (ctrl.Name == txtLoad.Name)
                {
                    cmbLoadDir.Focus();
                }
                else if (ctrl.Name == cmbLoadDir.Name)
                {
                    if (txtDisFrmStrNd.Enabled)
                        txtDisFrmStrNd.Focus();
                    else
                        rbtnLoadAllMember.Focus();

                }
                else if (ctrl.Name == txtDisFrmStrNd.Name)
                {
                    rbtnLoadAllMember.Focus();
                }
                else if (ctrl.Name == rbtnLoadAllMember.Name)
                {
                    btnJointLoadApply.Focus();
                }
                else if (ctrl.Name == rbtnLoadMemberNos.Name)
                {
                    if (txtLocalDirection.Enabled)
                        txtLocalDirection.Focus();
                    else
                        txtGlobalDirection.Focus();
                }
                else if (ctrl.Name == rbtnLoadMemberRange.Name)
                {
                    txtLoadStartMember.Focus();
                }
                else if (ctrl.Name == txtLoadStartMember.Name)
                {
                    txtLoadEndMember.Focus();
                }
                else if (ctrl.Name == txtLocalDirection.Name)
                {
                    btnJointLoadApply.Focus();
                }
                else if (ctrl.Name == txtGlobalDirection.Name)
                {
                    btnJointLoadApply.Focus();
                }
                else if (ctrl.Name == txtLoadEndMember.Name)
                {
                    btnJointLoadApply.Focus();
                }
                else if (ctrl.Name == rbtnAppliedLoad.Name)
                {
                    cmbLoadType.Focus();
                }


                else if (ctrl.Name == rbtnLoadCombination.Name)
                {
                    txtLoadCaseNo.Focus();
                }
                else if (ctrl.Name == txtLoadCaseNo.Name)
                {
                    txtMultiplyinorgFact.Focus();
                }
                else if (ctrl.Name == txtMultiplyinorgFact.Name)
                {
                    btnAddNextLoad.Focus();
                }
            }
        }
        #endregion
        #region MyRegion


        public frm3DBeamMembers()
        {
            InitializeComponent();
        }
        public frm3DBeamMembers(IApplication iApp)
        {
            InitializeComponent();
            this.app = iApp;
        }
       
        //private void ShowElement
        private void btnNextElement_Click(object sender, EventArgs e)
        {
            connCount = this.app.AppDocument.BeamConnectivity.IndexOf(txtMemberNumber.Text);
            int mbIndex = this.app.AppDocument.BeamConnectivity.IndexOf(txtMemberNumber.Text);
            int n1, n2, mbNo;

            n1 = SpFuncs.getInt(txtStartNode.Text);
            n2 = SpFuncs.getInt(txtEndNode.Text);
            mbNo = SpFuncs.getInt(txtMemberNumber.Text);

            if (mbIndex != -1)
            {
                this.app.AppDocument.BeamConnectivity[mbIndex] = GetBeamConnectivity(StartNode, EndNode, MemberNo);
                addBeamConnRelease();
                if (mbIndex < this.app.AppDocument.BeamConnectivity.Count - 1)
                {
                    showConnData(mbIndex + 1);
                }
                else if (mbIndex == this.app.AppDocument.BeamConnectivity.Count - 1)
                {
                    if (chkIncrOn.Checked)
                    {
                        StartNode += StartNodeIncr;
                        EndNode += EndNodeIncr;
                    }
                    else
                    {
                        txtStartNode.Text = "";
                        txtEndNode.Text = "";
                        txtStartNode.Focus();
                    }
                }
            }
            else if (mbIndex == -1)
            {
                if (txtStartNode.Text == "" || txtEndNode.Text == "")
                    return;

                this.app.AppDocument.BeamConnectivity.Add(GetBeamConnectivity(n1, n2, mbNo));
                addBeamConnRelease();

                if (chkIncrOn.Checked)
                {
                    StartNode += StartNodeIncr;
                    EndNode += EndNodeIncr;
                    MemberNo += MemberIncr;
                }
                else
                {
                    MemberNo += 1;
                    txtStartNode.Text = "";
                    txtEndNode.Text = "";
                    txtStartNode.Focus();
                }
            }
            if (mbIndex == this.app.AppDocument.BeamConnectivity.Count - 1)
            {
                MemberNo += MemberIncr;
                //txtMemberNumber.Text = (SpFuncs.getInt(txtMemberNumber.Text) + SpFuncs.getInt(txtMemberNoIncr.Text)) + "";
            }
            ShowStatus();
        }

        private void btnSecNextProperty_Click(object sender, EventArgs e)
        {
            txtS_P_N.Text = (SpFuncs.getInt(txtS_P_N.Text) + 1) + "";
            ShowStatus();
        }
        private void rbtnDimentionSection_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDimentionSection.Checked)
            {
                grbDimentionsOfSection.Enabled = true;
                txtCrossSectionArea.Text = "0.0";
                txtMomentOfInertiaIx.Text = "0.0";
                txtMomentOfInertiaIy.Text = "0.0";
                txtMomentOfInertiaIz.Text = "0.0";
            }
            else
                grbDimentionsOfSection.Enabled = false;
        }

        private void rbtnCalculatedSection_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCalculatedSection.Checked)
            {
                grbCalculatedSection.Enabled = true;
                txtWidth.Text = "0.0";
                txtDepth.Text = "0.0";
                txtInnerDiameter.Text = "0.0";
                txtOuterDiameter.Text = "0.0";
            }
            else
                grbCalculatedSection.Enabled = false;
        }

        private void btnNextProperty_Click(object sender, EventArgs e)
        {

            this.txtM_I_N.Text = (SpFuncs.getInt(this.txtM_I_N.Text) + 1) + "";
            ShowStatus();
        }

        private void btnTrussNext_Click(object sender, EventArgs e)
        {
            if (rbtnTrussAllMember.Checked)
                TrussAllMember();
            else if (rbtnTrussMemberNos.Checked)
                TrussMemberNos();
            else if (rbtnTrussMemberRange.Checked)
                TrussMemberRange();

            txtTrussMemberNos.Text = getTrussElements();
        }
        private void TrussMemberNos()
        {
            try
            {
                string[] values = txtTrussMemberNos.Text.Split(new char[] { ',' });
                app.AppDocument.MemberTruss.Clear();
                foreach (string val in values)
                {
                    CMemberTruss memTrs = new CMemberTruss();
                    memTrs.TRUSS_MEMB = SpFuncs.getInt(val, -1);
                    if (memTrs.TRUSS_MEMB != -1)
                    {
                        app.AppDocument.MemberTruss.Add(memTrs);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void TrussMemberRange()
        {
            CMemberTruss cmt;
            int start, end;
            start = SpFuncs.getInt(txtTrussStartMember.Text);
            end = SpFuncs.getInt(txtTrussEndElmt.Text);
            for (i = start; i <= end; i++)
            {
                cmt = new CMemberTruss();
                cmt.TRUSS_MEMB = i;
                app.AppDocument.MemberTruss.Add(cmt);
            }
        }
        private void TrussAllMember()
        {
            CMemberTruss cmt;
            //int start, end;
            //start = SpFuncs.getInt(txtTrussStartMember.Text);
            //end = SpFuncs.getInt(txtTrussEndElmt.Text);
            app.AppDocument.MemberTruss.Clear();
            for (i = 0; i < app.AppDocument.BeamConnectivity.Count; i++)
            {
                cmt = new CMemberTruss();
                cmt.TRUSS_MEMB = i + 1;
                app.AppDocument.MemberTruss.Add(cmt);
            }
        }



        private void cmbLoadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoadType.SelectedIndex == 0 || cmbLoadType.SelectedIndex == 2)
                txtDisFrmStrNd.Enabled = false;
            else
                txtDisFrmStrNd.Enabled = true;
        }

        private void btnNextLoad_Click(object sender, EventArgs e)
        {
            txtJointLoadNo.Text = (SpFuncs.getInt(txtJointLoadNo.Text) + 1) + "";
            ShowStatus();
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            elementCancelWork();
            matCancelWork();
            secCencelWork();
            JointCencelWork();

            MatList.Clear();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            app.AppDocument.MaterialProperty.MassUnit = (EMassUnits) cmbMaterialMassUnit.SelectedIndex;
            app.AppDocument.MaterialProperty.LengthUnit = (eLengthUnits)cmbMaterialLengthUnits.SelectedIndex;
            //app.AppDocument.MaterialProperty.MassFactor = CAstraUnits.GetFact(cmbMaterialMassUnit.Text);
            //app.AppDocument.MaterialProperty.LenthFactor = CAstraUnits.GetFact(cmbMaterialLengthUnits.Text);

            app.AppDocument.SectionProperty.MassUnit = (EMassUnits)(cmbSecMassUnit.SelectedIndex);
            app.AppDocument.SectionProperty.LengthUnit =(eLengthUnits)cmbSecLengthUnit.SelectedIndex;
            //app.AppDocument.SectionProperty.MassFactor = CAstraUnits.GetFact(cmbSecMassUnit.Text);
            //app.AppDocument.SectionProperty.LenthFactor = CAstraUnits.GetFact(cmbSecLengthUnit.Text);

            app.AppDocument.JointNodalLoad.MassUnit = (EMassUnits)(cmbLoadMassUnit.SelectedIndex);
            app.AppDocument.JointNodalLoad.LengthUnit = (eLengthUnits)(cmbLoadLengthUnits.SelectedIndex);

            this.app.AppDocument.MovingLoad.FileName = txtMovLoadFileName.Text;
            this.app.AppDocument.MovingLoad.MassUnit = (EMassUnits)(cmbMovMassUnit.SelectedIndex);
            this.app.AppDocument.MovingLoad.LengthUnit = (eLengthUnits)(cmbMovLengthUnit.SelectedIndex);

            this.app.AppDocument.AreaLoad.MassUnit = (EMassUnits)(cmbAreaMassUnit.SelectedIndex);
            this.app.AppDocument.AreaLoad.LengthUnit = (eLengthUnits)(cmbAreaLengthUnit.SelectedIndex);

            bcc.Clear();
            MatList.Clear();
            SecList.Clear();
            JointLoadList.Clear();
            MemberReleaseList.Clear();
            ////app.RunViewer();
            this.Close();
        }

        //public void RunViewer()
        //{
        //    app.AppDocument.Write(app.WorkingFile);
        //    //System.Environment.SetEnvironmentVariable("ASTRA", app.WorkingFile);
        //    string chkFilePath = Path.Combine(app.AppFolder, "Viewer.exe");
        //    if (File.Exists(chkFilePath))
        //        System.Diagnostics.Process.Start(chkFilePath);
        //    else
        //        MessageBox.Show(this, "Viewer.exe" + " not found.");
        //}
        private void rbtnSecElmtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSecMemberRange.Checked)
            {
                txtSecStartMember.Enabled = true;
                txtSecEndMember.Enabled = true;
                //btnSectionApply.Enabled = true;
            }
            else
            {
                txtSecStartMember.Enabled = false;
                txtSecEndMember.Enabled = false;
                //btnSectionApply.Enabled = false;
            }
        }

        private void rbtnSecElmtNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSecMemberNo.Checked)
            {
                txtSecMemberNos.Enabled = true;
                //btnSecElmtNext.Enabled = true;
            }
            else
            {
                txtSecMemberNos.Enabled = false;
                //btnSecElmtNext.Enabled = false;
            }

        }

        private void rbtnSecAllElmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSecAllMember.Checked)
            {
                txtSecStartMember.Enabled = false;
                txtSecEndMember.Enabled = false;
                //btnSectionApply.Enabled = false;
                txtSecMemberNos.Enabled = false;
                //btnSecElmtNext.Enabled = false;
            }
        }

        private void rbtnMatAllElmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMatAllMember.Checked)
            {
                txtMatMemberNos.Enabled = false;

                txtMatStartMember.Enabled = false;
                txtMatEndMember.Enabled = false;
            }
        }

        private void rbtnElmtNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMatMemberNos.Checked)
            {
                txtMatMemberNos.Enabled = true;
            }
            else
            {
                txtMatMemberNos.Enabled = false;
            }
        }

        private void rbtnMatElmtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMatMemberRange.Checked)
            {
                txtMatStartMember.Enabled = true;
                txtMatEndMember.Enabled = true;
            }
            else
            {
                txtMatStartMember.Enabled = false;
                txtMatEndMember.Enabled = false;
            }
        }

        private void rbtnLoadAllElmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoadAllMember.Checked)
            {
                txtLocalDirection.Enabled = false;
                txtGlobalDirection.Enabled = false;
                //btnLoadElmtNoNext.Enabled = false;
                txtLoadStartMember.Enabled = false;
                txtLoadEndMember.Enabled = false;
                //btnJointLoadApply.Enabled = false;
            }
        }

        private void rbtnLoadElmtNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoadMemberNos.Checked)
            {
                if (cmbLoadDir.SelectedIndex > 2)
                {
                    txtGlobalDirection.Enabled = true;
                    txtLocalDirection.Enabled = false;
                }
                else
                {
                    txtLocalDirection.Enabled = true;
                    txtGlobalDirection.Enabled = false;
                }
                //btnLoadElmtNoNext.Enabled = true;
            }
            else
            {
                txtLocalDirection.Enabled = false;
                //btnLoadElmtNoNext.Enabled = false;
            }
        }

        private void rbtnLoadElmtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoadMemberRange.Checked)
            {
                txtLoadStartMember.Enabled = true;
                txtLoadEndMember.Enabled = true;
                //btnJointLoadApply.Enabled = true;
            }
            else
            {
                txtLoadStartMember.Enabled = false;
                txtLoadEndMember.Enabled = false;
                //btnJointLoadApply.Enabled = false;
            }
        }

        private void rbtnTrussAllElmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTrussAllMember.Checked)
            {
                txtTrussMemberNos.Enabled = false;
                //btnLoadElmtNoNext.Enabled = false;
                txtTrussStartMember.Enabled = false;
                txtTrussEndElmt.Enabled = false;
                //btnTrussApply.Enabled = false;
            }
        }

        private void rbtnTrussElmtNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTrussMemberNos.Checked)
            {
                txtTrussMemberNos.Enabled = true;
                //btnTrussElmtNoNext.Enabled = true;

                //txtTrussStartMember.Enabled = false;
                //txtTrussEndElmt.Enabled = false;
                //btnTrussNext.Enabled = false;
            }
            else
            {
                txtTrussMemberNos.Enabled = false;
                //btnTrussElmtNoNext.Enabled = false;
            }
        }

        private void rbtnTrussElmtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTrussMemberRange.Checked)
            {
                txtTrussStartMember.Enabled = true;
                txtTrussEndElmt.Enabled = true;
                //btnTrussApply.Enabled = true;
            }
            else
            {
                txtTrussStartMember.Enabled = false;
                txtTrussEndElmt.Enabled = false;
                //btnTrussApply.Enabled = false;
            }
        }

        private void btnNextLoadCase_Click(object sender, EventArgs e)
        {
            txtJointLoadCase.Text = (SpFuncs.getInt(txtJointLoadCase.Text) + 1) + "";
            ShowStatus();
        }
        private void frm3DBeamElements_Load(object sender, EventArgs e)
        {
            cmbMaterialLengthUnits.SelectedIndex = (int)app.AppDocument.MaterialProperty.LengthUnit;
            cmbMaterialMassUnit.SelectedIndex = (int)app.AppDocument.MaterialProperty.MassUnit;
            //cmbMaterialLengthUnits.SelectedIndex = CAstraUnits.GetLengthUnitIndex(app.AppDocument.MaterialProperty.LenthFactor);
            //cmbMaterialMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(app.AppDocument.MaterialProperty.MassFactor);

            cmbSecLengthUnit.SelectedIndex = (int)app.AppDocument.SectionProperty.LengthUnit;
            cmbSecMassUnit.SelectedIndex =(int) app.AppDocument.SectionProperty.MassUnit;
            //cmbSecLengthUnit.SelectedIndex = CAstraUnits.GetLengthUnitIndex(app.AppDocument.SectionProperty.LenthFactor);
            //cmbSecMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(app.AppDocument.SectionProperty.MassFactor);

            cmbLoadLengthUnits.SelectedIndex =(int) app.AppDocument.JointNodalLoad.LengthUnit;
            cmbLoadMassUnit.SelectedIndex =(int)app.AppDocument.JointNodalLoad.MassUnit;
            //cmbLoadLengthUnits.SelectedIndex = CAstraUnits.GetLengthUnitIndex(app.AppDocument.JointNodalLoad.LenthFactor);
            //cmbLoadMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(app.AppDocument.JointNodalLoad.MassFactor);

            cmbMovLengthUnit.SelectedIndex = CAstraUnits.GetLengthUnitIndex(this.app.AppDocument.MovingLoad.LengthFactor);
            cmbMovMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(this.app.AppDocument.MovingLoad.MassFactor);

            cmbAreaMassUnit.SelectedIndex =(int) this.app.AppDocument.AreaLoad.MassUnit;
            cmbAreaLengthUnit.SelectedIndex =(int)  this.app.AppDocument.AreaLoad.LengthUnit;
            //cmbAreaMassUnit.SelectedIndex = CAstraUnits.GetWeightUnitIndex(this.app.AppDocument.AreaLoad.MassFactor);
            //cmbAreaLengthUnit.SelectedIndex = CAstraUnits.GetLengthUnitIndex(this.app.AppDocument.AreaLoad.LenthFactor);

            cmbMovLoadType.SelectedIndex = 0;
            if (this.app.AppDocument.LoadGeneration.Count > 0)
            {
                grbLoadGeneration.Enabled = true;
                cmbMovGenLoadType.Items.Clear();
                for (i = 0; i < this.app.AppDocument.MovingLoad.Count; i++)
                {
                    if (!cmbMovGenLoadType.Items.Contains(this.app.AppDocument.MovingLoad[i].Type))
                    {
                        cmbMovGenLoadType.Items.Add(this.app.AppDocument.MovingLoad[i].Type);
                        cmbMovGenLoadType.SelectedIndex = 0;
                    }
                }
                AreaLoadCase = 1;
                AreaLoadNo = 1;
            }
            firstLoadConn();
            txtTrussMemberNos.Text = getTrussElements();
            if (txtTrussMemberNos.Text.Length > 0)
                rbtnTrussMemberNos.Checked = true;

            ElementLoad();
            MaterialLoad();
            SectionLoad();
            JointLoad();
            txtM_I_N.Text = "1";
            txtS_P_N.Text = "1";
            txtJointLoadCase.Text = "1";
            txtAreaLoadcase.Text = "1";
            
            ShowStatus();
        }

        private void btnAddNextLoad_Click(object sender, EventArgs e)
        {
            
            CLoadCombination clc = new CLoadCombination();
            clc.loadCase = SpFuncs.getInt(txtJointLoadCase.Text, -1);
            clc.load = SpFuncs.getInt(txtLoadCaseNo.Text, -1);
            clc.factor = SpFuncs.getDouble(txtMultiplyinorgFact.Text, -1);
            int indx = app.AppDocument.LoadCombination.IndexOf(clc);
            if (indx == -1)
            {
                app.AppDocument.LoadCombination.Add(clc);
            }
            else
            {
                app.AppDocument.LoadCombination[indx] = clc;
            }
            SetLoadComb();

            
            indx = loadComb.IndexOf(JointLoadCaseNo);
            if (indx != -1)
            {
                if (indx == loadComb.loadColl.Count - 1)
                {
                    JointLoadCaseNo += 1;
                }
                else
                {
                    JointLoadCaseNo = loadComb.loadColl[indx + 1];
                }
            }
            else
            {
                JointLoadCaseNo += 1;
                txtMultiplyinorgFact.Text = "";
            }

            //JointLoadCaseNo += 1;
            txtLoadCaseNo.Focus();
        }

        private void rbtnAppliedLoad_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAppliedLoad.Checked)
                grbAppliedLoad.Enabled = true;
            else
                grbAppliedLoad.Enabled = false;
        }

        private void rbtnLoadCombination_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoadCombination.Checked)
                grbLoadComb.Enabled = true;
            else
                grbLoadComb.Enabled = false;
        }

        private void btnPrevElement_Click(object sender, EventArgs e)
        {
            connCount = this.app.AppDocument.BeamConnectivity.IndexOf(txtMemberNumber.Text);
            int mbIndex = this.app.AppDocument.BeamConnectivity.IndexOf(txtMemberNumber.Text);
            if (connCount != -1)
            {
                this.app.AppDocument.BeamConnectivity[mbIndex] = GetBeamConnectivity(StartNode,EndNode,MemberNo);
                addBeamConnRelease();
                if (connCount > 0)
                    showConnData(connCount - 1);
                else
                    showConnData(app.AppDocument.BeamConnectivity.Count -1);
            }
            else if (connCount == -1 && this.app.AppDocument.BeamConnectivity.Count > 0)
            {
                showConnData(this.app.AppDocument.BeamConnectivity.Count - 1);
                
            }
            ShowStatus();
        } 
        
        private void btnPreviousProperty_Click(object sender, EventArgs e)
        {
            if (SpFuncs.getInt(this.txtM_I_N.Text) > 1)
            {
                this.txtM_I_N.Text = (SpFuncs.getInt(this.txtM_I_N.Text) - 1) + "";
            }
            ShowStatus();
        }

        private void btnSecElmtNext_Click(object sender, EventArgs e)
        {
            SectionMemberNos();
        }

        private void SectionMemberNos()
        {
            app.AppDocument.SectionProperty.DeleteSection(SectionID);
            string[] kStr = txtSecMemberNos.Text.Split(new char[] { ',' });
            foreach (string s in kStr)
            {
                this.app.AppDocument.SectionProperty.Add(getCSectionProperty(s));
            }
        }
        private void SectionMemberRange()
        {
            start = SpFuncs.getInt(txtSecStartMember.Text);
            end = SpFuncs.getInt(txtSecEndMember.Text);
            for (i = start; i <= end; i++)
            {
                this.app.AppDocument.SectionProperty.Add(getCSectionProperty(i));
            }
        }
        private void SectionAllMember()
        {
            app.AppDocument.SectionProperty.DeleteSection(SectionID);
            for (i = 0; i < app.AppDocument.BeamConnectivity.Count; i++)
            {
                this.app.AppDocument.SectionProperty.Add(getCSectionProperty(app.AppDocument.BeamConnectivity[i].memberNo));
            } 
        }

        private void btnSecElmtRangeNext_Click(object sender, EventArgs e)
        {
            if (rbtnSecAllMember.Checked)
                SectionAllMember();
            else if (rbtnSecMemberNo.Checked)
                SectionMemberNos();
            else if (rbtnSecMemberRange.Checked)
                SectionMemberRange();

            string st = txtS_P_N.Text;
            txtS_P_N.Text = "";
            txtS_P_N.Text = st;
        }

        public void WorkMaterialMemberNos()
        {
            app.AppDocument.MaterialProperty.DeleteMaterial(SpFuncs.getInt(txtM_I_N.Text));
            string[] kStr = txtMatMemberNos.Text.Split(new char[] { ',' });
            foreach (string s in kStr)
            {
                this.app.AppDocument.MaterialProperty.Add(getCMaterialProperty(s));
            }
        }
        public void WorkMaterialAllMenbers()
        {
            app.AppDocument.MaterialProperty.DeleteMaterial(SpFuncs.getInt(txtM_I_N.Text));
            for(int i = 0; i < app.AppDocument.BeamConnectivity.Count;i++)
            {
                this.app.AppDocument.MaterialProperty.Add(getCMaterialProperty(app.AppDocument.BeamConnectivity[i].memberNo));
            }
        }
        public void WorkMaterialMemberRange()
        {
            start = SpFuncs.getInt(txtMatStartMember.Text);
            end = SpFuncs.getInt(txtMatEndMember.Text);
            for (i = start; i <= end; i++)
            {
                this.app.AppDocument.MaterialProperty.Add(getCMaterialProperty(i));
            }
        }
        private void btnMaterialMemberApply_Click(object sender, EventArgs e)
        {
            if (rbtnMatAllMember.Checked)
                WorkMaterialAllMenbers();
            else if (rbtnMatMemberNos.Checked)
                WorkMaterialMemberNos();
            else if (rbtnMatMemberRange.Checked)
                WorkMaterialMemberRange();

            MessageBox.Show(this, "Members added.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            string ss = txtM_I_N.Text;
            txtM_I_N.Text = "";
            txtM_I_N.Text = ss;
            txtElasticModulus.Focus();
        }

        private void JointMemberNos()
        {
            if (txtLocalDirection.Enabled)
            {
                string[] kStr = txtLocalDirection.Text.Split(new char[] { ',' });
                if (kStr.Length > 0)
                {
                    CJointNodalLoad jnd = getCJointNodalLoad(kStr[0]);
                    jnd.loadNo = JointLoadNo;
                    app.AppDocument.JointNodalLoad.DeleteAllElement(jnd);
                }
                foreach (string s in kStr)
                {
                    CJointNodalLoad jnd = getCJointNodalLoad(s);
                    jnd.loadNo = JointLoadNo;
                    this.app.AppDocument.JointNodalLoad.Add(jnd);
                }
            }
            else
            {
                string[] kStr = txtGlobalDirection.Text.Split(new char[] { ',' });
                //app.AppDocument.JointNodalLoad.DeleteLoadcase(JointLoadCase, JointLoadNo);
                foreach (string s in kStr)
                {
                    this.app.AppDocument.JointNodalLoad.Add(getCJointNodalLoad(s));
                }
            }
        }
        private void JointMemberRange()
        {
            start = SpFuncs.getInt(txtLoadStartMember.Text);
            end = SpFuncs.getInt(txtLoadEndMember.Text);
            //app.AppDocument.JointNodalLoad.DeleteLoadcase(JointLoadCase, JointLoadNo);
            for (i = start; i <= end; i++)
            {
                this.app.AppDocument.JointNodalLoad.Add(getCJointNodalLoad(i));
            }
        }
        private void JointAllMember()
        {
            //if (txtLocalDirection.Enabled)
            //{
            //string[] kStr = txtLocalDirection.Text.Split(new char[] { ',' });
            if (app.AppDocument.BeamConnectivity.Count > 0)
            {
                CJointNodalLoad jnd = getCJointNodalLoad(app.AppDocument.BeamConnectivity[0].memberNo);
                jnd.loadNo = JointLoadNo;
                app.AppDocument.JointNodalLoad.DeleteAllElement(jnd);
            }
            for (int i = 0; i < app.AppDocument.BeamConnectivity.Count; i++)
            {
                CJointNodalLoad jnd = getCJointNodalLoad(app.AppDocument.BeamConnectivity[i].memberNo);
                jnd.loadNo = JointLoadNo;
                this.app.AppDocument.JointNodalLoad.Add(jnd);
            }
            //}
            //else
            //{
            //    string[] kStr = txtGlobalDirection.Text.Split(new char[] { ',' });
            //    //app.AppDocument.JointNodalLoad.DeleteLoadcase(JointLoadCase, JointLoadNo);
            //    foreach (string s in kStr)
            //    {
            //        this.app.AppDocument.JointNodalLoad.Add(getCJointNodalLoad(s));
            //    }
            //}
        }


        private void btnLoadElmtNoNext_Click(object sender, EventArgs e)
        {
            JointMemberNos();
        }

        private void btnJointLoadApply_Click(object sender, EventArgs e)
        {
            if (rbtnLoadAllMember.Checked)
                JointAllMember();
            else if (rbtnLoadMemberNos.Checked)
                JointMemberNos();
            else if (rbtnLoadMemberRange.Checked)
                JointMemberRange();

            string kstr = txtJointLoadCase.Text;
            txtJointLoadCase.Text = "";
            txtJointLoadCase.Text = kstr;
            cmbLoadType.Focus();
        }

        private void txtLoadCase_Leave(object sender, EventArgs e)
        {
            txtLocalDirection.Text = this.getJointSequenceLCS(SpFuncs.getInt(txtJointLoadCase.Text), SpFuncs.getInt(txtJointLoadNo.Text));
        }

        private void txtS_P_N_Leave(object sender, EventArgs e)
        {
            txtSecMemberNos.Text = this.getSectionSequence(SpFuncs.getInt(txtS_P_N.Text));
        }

        private void txtM_I_N_Leave(object sender, EventArgs e)
        {
            txtMatMemberNos.Text = this.getMaterialSequence(SpFuncs.getInt(txtM_I_N.Text));
        }

        private void txtM_I_N_TextChanged(object sender, EventArgs e)
        {
            txtMatMemberNos.Text = this.getMaterialSequence(SpFuncs.getInt(txtM_I_N.Text));
            if (txtMatMemberNos.Text.Length > 0)
                rbtnMatMemberNos.Checked = true;
            setMaterialProperty(SpFuncs.getInt(txtM_I_N.Text));
        }

        private void txtS_P_N_TextChanged(object sender, EventArgs e)
        {
            setSectionProperty(SpFuncs.getInt(txtS_P_N.Text));
            txtSecMemberNos.Text = this.getSectionSequence(SpFuncs.getInt(txtS_P_N.Text));
            if (txtSecMemberNos.Text.Length > 0)
                rbtnSecMemberNo.Checked = true;
            if (SpFuncs.getDouble(txtWidth.Text) == 0.0 &&
                SpFuncs.getDouble(txtDepth.Text) == 0.0 &&
                SpFuncs.getDouble(txtInnerDiameter.Text) == 0.0 &&
                SpFuncs.getDouble(txtOuterDiameter.Text) == 0.0)
            {
                rbtnCalculatedSection.Checked = true;
            }

        }

        private void txtLoadCase_TextChanged(object sender, EventArgs e)
        {

            SetLoadComb();
            if (loadComb.loadColl.Count > 0)
            {
                rbtnLoadCombination.Checked = true;
                txtLoadCaseNo.Text = loadComb.loadColl[0].ToString();
                txtMultiplyinorgFact.Text = loadComb.multFactColl[0].ToString();
            }
            else
            {
                rbtnAppliedLoad.Checked = true;
                txtLoadCaseNo.Text = "";
                txtMultiplyinorgFact.Text = "";
            }
            SetLocalDirection();
        }
       
        private void txtElementNumber_TextChanged(object sender, EventArgs e)
        {
            setElementConnectivity(MemberNo);
            ShowStatus();
            
        }

        private void btnPreviousLoadCase_Click(object sender, EventArgs e)
        {
            txtJointLoadCase.Text = (SpFuncs.getInt(txtJointLoadCase.Text) - 1) + "";

        }

        private void btnGeoPrevProp_Click(object sender, EventArgs e)
        {
            if (SpFuncs.getInt(txtS_P_N.Text) > 1)
            {
                txtS_P_N.Text = (SpFuncs.getInt(txtS_P_N.Text) - 1) + "";
            }
        }

        private void cmbLoadDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnLoadMemberNos.Checked = true;
            if (cmbLoadDir.SelectedIndex >= 0 && cmbLoadDir.SelectedIndex <= 2)
            {
                txtLocalDirection.Enabled = true;
                txtGlobalDirection.Enabled = false;
            }
            else
            {
                txtLocalDirection.Enabled = false;
                txtGlobalDirection.Enabled = true;
            }
        }

        private void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            if (this.app.AppDocument.MaterialProperty.DeleteMaterial(SpFuncs.getInt(txtM_I_N.Text)))
            {
                txtM_I_N.Text = "";
                txtMatMemberNos.Text = "";
                txtElasticModulus.Text = "";
                txtMassDensity.Text = "";
                txtWeightDensity.Text = "";
            }
        }
        
        private void btnSecDelProp_Click(object sender, EventArgs e)
        {
            if (this.app.AppDocument.SectionProperty.DeleteSection(SpFuncs.getInt(txtS_P_N.Text)))
            {
                txtS_P_N.Text = "";
                txtDepth.Text = "";
                txtWidth.Text = "";
                txtOuterDiameter.Text = "";
                txtInnerDiameter.Text = "";
                txtCrossSectionArea.Text = "";
                txtMomentOfInertiaIx.Text = "";
                txtMomentOfInertiaIy.Text = "";
                txtMomentOfInertiaIz.Text = "";
                txtSecMemberNos.Text = "";
                txtSecStartMember.Text = "";
                txtSecEndMember.Text = "";
            }
        }

        private void btnDeleteElement_Click(object sender, EventArgs e)
        {
            string elNo = txtMemberNumber.Text;
            if (this.app.AppDocument.BeamConnectivity.IndexOf(elNo) > 0)
            {
                connCount = this.app.AppDocument.BeamConnectivity.IndexOf(elNo);
                showConnData(connCount - 1);
                this.app.AppDocument.BeamConnectivity.deleteElement(elNo);
            }
            else if(this.app.AppDocument.BeamConnectivity.IndexOf(elNo) == 0)
            {
                this.app.AppDocument.BeamConnectivity.deleteElement(0);
                txtMemberNumber.Text = "1";
                txtStartNode.Text = "1";
                txtEndNode.Text = "1";
            }
            ShowStatus();
        }
        private void btnDeleteLoadCase_Click(object sender, EventArgs e)
        {
            if(this.app.AppDocument.JointNodalLoad.DeleteLoadcase(JointLoadCase,JointLoadNo))
            {
                txtJointLoadCase.Text = "";
                txtJointLoadNo.Text = "";
                txtLoad.Text = "";
                txtDisFrmStrNd.Text = "";
                txtLocalDirection.Text = "";
                txtGlobalDirection.Text = "";
                txtLoadStartMember.Text = "";
                txtLoadEndMember.Text = "";
            }
            if (app.AppDocument.LoadCombination.DeleteLoadCase(JointLoadCase))
            {
                SetLoadComb();
                txtJointLoadCase.Text = "";
                JointLoadCaseNo = 1;
            }
        }

        private void btnMovNext_Click(object sender, EventArgs e)
        {
            mload = new CMovingLoad();
            mload.Type = cmbMovLoadType.Text;
            mload.LoadName = cmbMovLoadName.Text;
            mload.impactFactor = SpFuncs.getDouble(cmbMovImpactFactor.Text);

            int index = this.app.AppDocument.MovingLoad.IndexOf(cmbMovLoadType.Text);

            if (index != -1)
            {
                this.app.AppDocument.MovingLoad[index] = mload;
            }
            else
                this.app.AppDocument.MovingLoad.Add(mload);

            if (!cmbMovGenLoadType.Items.Contains(cmbMovLoadType.Text) && cmbMovLoadType.Text != "")
            {
                cmbMovGenLoadType.Items.Add(cmbMovLoadType.Text);
                if (cmbMovGenLoadType.Items.Count > 0)
                    grbLoadGeneration.Enabled = true;
                else
                    grbLoadGeneration.Enabled = false;
            }
            if (cmbMovLoadType.SelectedIndex > -1 && cmbMovLoadType.SelectedIndex < cmbMovLoadType.Items.Count - 1)
            {
                cmbMovLoadType.SelectedIndex += 1;
            }
            cmbMovLoadType.Focus();
        }

        private void btnMovGenNext_Click(object sender, EventArgs e)
        {

            lgn = new CLoadGeneration();
            lgn.Type = cmbMovGenLoadType.Text;
            lgn.SerialNo = MovingSerialNo;
            lgn.X = SpFuncs.getDouble(txtMovGenX.Text);
            lgn.Y = SpFuncs.getDouble(txtMovGenY.Text);
            lgn.Z = SpFuncs.getDouble(txtMovGenZ.Text);
            lgn.XINC = SpFuncs.getDouble(txtMovGenXInc.Text, 0.2).ToString();

            int indx = app.AppDocument.LoadGeneration.IndexOf(lgn.Type, lgn.SerialNo);
            if (indx == -1)
            {
                app.AppDocument.LoadGeneration.Add(lgn);
                SetLoadGeneration(lgn.Type);
            }
            else
                app.AppDocument.LoadGeneration[indx] = lgn;

            app.AppDocument.LoadGeneration.repeatTime = SpFuncs.getInt(txtMovGenRepeatTimes.Text);
            MovingSerialNo += 1;
            cmbMovGenLoadType.Focus();
        }

        private void cmbMovLoadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kStr = cmbMovLoadType.SelectedItem.ToString();
            for (int i = 0; i < this.app.AppDocument.MovingLoad.Count; i++)
            {
                if (this.app.AppDocument.MovingLoad[i].Type == kStr)
                {
                    cmbMovLoadName.Text = (this.app.AppDocument.MovingLoad[i].LoadName);
                    cmbMovImpactFactor.Text = this.app.AppDocument.MovingLoad[i].impactFactor.ToString();
                    return;
                }
            }
        }

        private void rbtnAreaAllMem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAreaAllMem.Checked)
            {
                txtAreaEndMember.Enabled = false;
                txtAreaStartMember.Enabled = false;
                txtAreaEndMember.Enabled = false;

                //btnAreaMemberNos.Enabled = false;
                //btnAreaMemberApply.Enabled = false;
            }
        }

        private void rbtnAreaMemNos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAreaMemNos.Checked)
            {
                txtAreaElementNos.Enabled = true;
            }
            else
            {
                txtAreaElementNos.Enabled = false;
            }
        }

        private void rbtnAreaMemRabge_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAreaMemRabge.Checked)
            {
                txtAreaStartMember.Enabled = true;
                txtAreaEndMember.Enabled = true;

            }
            else
            {
                txtAreaStartMember.Enabled = false;
                txtAreaEndMember.Enabled = false;
            }
        }

        private void btnAreaNext_Click(object sender, EventArgs e)
        {
            AreaLoadNo += 1;

            //txtAreaLoadcase.Text = (SpFuncs.getInt(txtAreaLoadcase.Text) + 1) + "";
        }

        private void btnAreaPrevious_Click(object sender, EventArgs e)
        {
            AreaLoadNo -= 1;
            //if (SpFuncs.getInt(txtAreaLoadcase.Text) > 1)
            //{
            //    txtAreaLoadcase.Text = (SpFuncs.getInt(txtAreaLoadcase.Text) - 1) + "";
            //}
        }

        private void btnAreaElmtRangeApply_Click(object sender, EventArgs e)
        {
            if (rbtnAreaAllMem.Checked)
            {
                AreaLoadAllMember();
            }
            else if (rbtnAreaMemNos.Checked)
            {
                AreaLoadMemberNos();
            }
            else if (rbtnAreaMemRabge.Checked)
            {
                AreaLoadMemberRange();
            }

            string ss = txtAreaLoadcase.Text;
            txtAreaLoadcase.Text = "";
            txtAreaLoadcase.Text = ss;

            app.AppDocument.AreaLoad.SetLoadNo(AreaLoadCase);
            txtAreaLoadNo.Text = "";
            AreaLoadNo = this.app.AppDocument.AreaLoad.MaxLoadNo(AreaLoadCase);
            btnAreaNextLoadNo.Focus();

        }
        private void AreaLoadMemberRange()
        {
            start = SpFuncs.getInt(txtAreaStartMember.Text);
            end = SpFuncs.getInt(txtAreaEndMember.Text);
            for (i = start; i <= end; i++)
            {
                area = new CAreaLoad();
                area.loadcase = AreaLoadCase;
                area.memberNo = i;
                area.fy = SpFuncs.getDouble(txtAreaFy.Text);
                this.app.AppDocument.AreaLoad.Add(area);
            }
        }
        
        public void AreaLoadAllMember()
        {
            //start = SpFuncs.getInt(txtAreaStartElement.Text);
            //end = SpFuncs.getInt(txtAreaEndElement.Text);

            for (i = 0; i < app.AppDocument.BeamConnectivity.Count; i++)
            {
                area = new CAreaLoad();
                area.loadcase = SpFuncs.getInt(txtAreaLoadcase.Text);
                area.memberNo = app.AppDocument.BeamConnectivity[i].memberNo;
                area.fy = SpFuncs.getDouble(txtAreaFy.Text);
                this.app.AppDocument.AreaLoad.Add(area);
            }
        }

        private void btnAreaElmtApply_Click(object sender, EventArgs e)
        {
            //AreaLoadMemberNos();
        }
        private void AreaLoadMemberNos()
        {
            string[] values = txtAreaElementNos.Text.Split(new char[] { ',' });
            this.app.AppDocument.AreaLoad.DeleteLoadcase(SpFuncs.getInt(txtAreaLoadcase.Text));
            for (i = 0; i < values.Length; i++)
            {
                if (values[i] == "")
                    continue;
                area = new CAreaLoad();
                area.loadcase = SpFuncs.getInt(txtAreaLoadcase.Text);
                area.memberNo = SpFuncs.getInt(values[i]);
                area.fy = SpFuncs.getDouble(txtAreaFy.Text);
                this.app.AppDocument.AreaLoad.Add(area);

            }
        }
        private string GetAreaLoadMember(int loadcase)
        {
            string str = " ",kStr = "";
            for (i = 0; i < this.app.AppDocument.AreaLoad.Count; i++)
            {
                if (this.app.AppDocument.AreaLoad[i].loadcase == loadcase &&
                    this.app.AppDocument.AreaLoad[i].loadNo == AreaLoadNo)
                {
                    str += this.app.AppDocument.AreaLoad[i].memberNo + ",";
                    kStr = this.app.AppDocument.AreaLoad[i].fy.ToString();
                }
            }
            //////AreaLoadNo = this.app.AppDocument.AreaLoad.MaxLoadNo(loadcase);
            txtAreaFy.Text = kStr;
            return str.Substring(0, str.Length - 1).TrimStart().TrimEnd();
        }
        #endregion

        private void Area_txt_TextChanged(object sender, EventArgs e)
        {
            setArea();
        }

        private void btnMovPrev_Click(object sender, EventArgs e)
        {
            int index = this.app.AppDocument.MovingLoad.IndexOf(cmbMovLoadType.Text);
            if (index != -1)
            {
                mload = new CMovingLoad();
                mload.Type = cmbMovLoadType.Text;
                mload.LoadName = cmbMovLoadName.Text;
                mload.impactFactor = SpFuncs.getDouble(cmbMovImpactFactor.Text);
                this.app.AppDocument.MovingLoad[index] = mload;
            }
            if (cmbMovLoadType.SelectedIndex > 0)
            {
                cmbMovLoadType.SelectedIndex -= 1;
            }
        }

        private void txtAreaLoadcase_TextChanged(object sender, EventArgs e)
        {
            txtAreaElementNos.Text = GetAreaLoadMember(SpFuncs.getInt(txtAreaLoadcase.Text));
            
            if (txtAreaElementNos.Text.Length > 0)
                rbtnAreaMemNos.Checked = true;
            app.AppDocument.AreaLoad.SetLoadNo(AreaLoadCase);
        }

        private void btnAreaElmtRangePrevious_Click(object sender, EventArgs e)
        {
            thd.Abort();
        }

        private void txtBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            toolTip1.SetToolTip(txt, txt.Text);
            
        }

        private void btnPreviousLoad_Click(object sender, EventArgs e)
        {
            JointLoadNo -= 1;
        }

        private void chkIncrOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncrOn.Checked)
            {
                txtMemberNoIncr.Enabled = true;
                txtSrtNdIncr.Enabled = true;
                txtEdIncr.Enabled = true;
            }
            else
            {
                txtMemberNoIncr.Enabled = false;
                txtSrtNdIncr.Enabled = false;
                txtEdIncr.Enabled = false;
            }
        }

        private void txtStartNode_Leave(object sender, EventArgs e)
        {
            bool bIsPresent = false;
            TextBox txt = (TextBox) sender;
            for (int i = 0; i < app.AppDocument.NodeData.Count; i++)
            {
                if (app.AppDocument.NodeData[i].NodeNo == SpFuncs.getInt(txt.Text))
                    bIsPresent = true;
            }
            if (!bIsPresent)
            {
                if (txt.Text != "")
                    epAstra.SetError(txt, "Node No '" + txt.Text + "' is not present.");
                else
                {
                    epAstra.SetError(txt, "");
                }
            }
            else
            {
                epAstra.SetError(txt, "");
            }
           

        }

        private void rbtnCylindrical_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton chk = (RadioButton)sender;
            if (chk.Checked)
            {
                if (chk.Name == "rbtnPrismatic")
                {
                    txtWidth.Enabled = true;
                    txtDepth.Enabled = true;

                    txtOuterDiameter.Enabled = false;
                    txtInnerDiameter.Enabled = false;
                    txtOuterDiameter.Text = "0";
                    txtInnerDiameter.Text = "0";
                }
                else if (chk.Name == "rbtnCircular")
                {
                    txtWidth.Enabled = false;
                    txtDepth.Enabled = false;
                    txtWidth.Text = "0";
                    txtDepth.Text = "0";

                    txtOuterDiameter.Enabled = true;
                    txtInnerDiameter.Enabled = true;
                }
            }

        }
        private void btnTrussElmtNoNext_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAreaDelete_Click(object sender, EventArgs e)
        {
            app.AppDocument.AreaLoad.DeleteLoadNo(AreaLoadCase, AreaLoadNo);
            AreaLoadNo = app.AppDocument.AreaLoad.MaxLoadNo(AreaLoadCase);
            txtAreaElementNos.Text = GetAreaLoadMember(AreaLoadCase);
        }

        private void btnDeleteLoad_Click(object sender, EventArgs e)
        {
            string[] values = txtLocalDirection.Text.Split(new char[]{','});

            if(values.Length > 0)
            {
                CJointNodalLoad cjnd = getCJointNodalLoad(values[0]);
                cjnd.loadNo = JointLoadNo;
                app.AppDocument.JointNodalLoad.DeleteAllElement(cjnd);
                txtLocalDirection.Text = "";
                txtGlobalDirection.Text = "";
            }
        }

        private void btnCreateTXTFile_Click(object sender, EventArgs e)
        {
            string src,dest;
            src = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                @"Astra Examples\[07] Moving Load Analysis for Bridge Deck\Data\LL.TXT");
            dest = Path.Combine(app.WorkingFolder,"LL.TXT");
            if (File.Exists(src))
            {
                File.Copy(src, dest);
                MessageBox.Show(this, "Load File is created in " + dest + ".", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cmbMovGenLoadType.Focus();
        }

        private void btnLoadCombPrevLoad_Click(object sender, EventArgs e)
        {
            CLoadCombination clc = new CLoadCombination();
            clc.loadCase = SpFuncs.getInt(txtJointLoadCase.Text, -1);
            clc.load = SpFuncs.getInt(txtLoadCaseNo.Text, -1);
            clc.factor = SpFuncs.getDouble(txtMultiplyinorgFact.Text, -1);
            int indx = app.AppDocument.LoadCombination.IndexOf(clc);
            if (indx != -1)
            {
                app.AppDocument.LoadCombination[indx] = clc;
            }
            SetLoadComb();

            indx = loadComb.IndexOf(JointLoadCaseNo);
            if (indx != -1)
            {
                if (indx > 0)
                {
                    JointLoadCaseNo = loadComb.loadColl[indx - 1];
                    MultiplyingFactor = loadComb.multFactColl[indx - 1];
                }
            }
            else
            {
                if (loadComb.loadColl.Count > 0)
                {
                    JointLoadCaseNo = loadComb.loadColl[loadComb.loadColl.Count -1];
                }
            }
        }

        private void txtLoadCaseNo_TextChanged(object sender, EventArgs e)
        {
            int indx = loadComb.IndexOf(JointLoadCaseNo);
            if (indx != -1)
            {
                MultiplyingFactor = loadComb.multFactColl[indx];
            }
            else
            {
                txtMultiplyinorgFact.Text = "";
            }
            
        }

        private void btnLoadCombDelLode_Click(object sender, EventArgs e)
        {
            CLoadCombination clc = new CLoadCombination();
            clc.loadCase = SpFuncs.getInt(txtJointLoadCase.Text, -1);
            clc.load = SpFuncs.getInt(txtLoadCaseNo.Text, -1);
            clc.factor = SpFuncs.getDouble(txtMultiplyinorgFact.Text, -1);
            int indx = app.AppDocument.LoadCombination.IndexOf(clc);
            if (indx != -1)
            {
                app.AppDocument.LoadCombination.RemoveAt(indx);
                SetLoadComb();
                if (loadComb.loadColl.Count > 0)
                {
                    JointLoadCaseNo = loadComb.loadColl[0];
                }
                else
                    JointLoadCaseNo = 1;
            }
        }

        private void grbLoadGeneration_Enter(object sender, EventArgs e)
        {

        }

        private void cmbMovGenLoadType_TextChanged(object sender, EventArgs e)
        {
            SetLoadGeneration(cmbMovGenLoadType.Text);
            MovingSerialNo = 1;
        }

        private void txtMovingSerialNo_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < LoadGen.Count; i++)
            {
                if (LoadGen[i].SerialNo == MovingSerialNo)
                {
                    txtMovGenX.Text = LoadGen[i].X.ToString("0.00");
                    txtMovGenY.Text = LoadGen[i].Y.ToString("0.00");
                    txtMovGenZ.Text = LoadGen[i].Z.ToString("0.00");
                    txtMovGenXInc.Text = LoadGen[i].XINC;
                    txtMovGenRepeatTimes.Text = app.AppDocument.LoadGeneration.repeatTime.ToString();
                    return;
                }
            }
            txtMovGenX.Text = "0.0";
            txtMovGenY.Text = "0.0";
            txtMovGenZ.Text = "0.0";
            txtXIncr.Text = "0.2";
            txtRepeatTime.Text = app.AppDocument.LoadGeneration.repeatTime.ToString();
        }

        private void btnMovGenPrev_Click(object sender, EventArgs e)
        {
            MovingSerialNo -= 1;
        }

        private void txtMemberNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (txt.Name == "txtMemberNumber")
                    txtStartNode.Focus();
                else if (txt.Name == "txtStartNode")
                    txtEndNode.Focus();
                else if (txt.Name == "txtEndNode")
                    btnNextMember.Focus();
            }
        }

        private void materialProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            Control ctrl = sender as Control;
            
            if (ctrl.Name == txtM_I_N.Name)
                txtElasticModulus.Focus();
            else if (ctrl.Name == txtElasticModulus.Name)
                txtPositionRatio.Focus();
            else if (ctrl.Name == txtPositionRatio.Name)
                txtAlpha.Focus();
            else if (ctrl.Name == txtAlpha.Name)
                txtMassDensity.Focus();
            else if (ctrl.Name == txtMassDensity.Name)
                txtWeightDensity.Focus();
            else if (ctrl.Name == txtWeightDensity.Name)
                rbtnMatAllMember.Focus();
            else if (ctrl.Name == rbtnMatAllMember.Name)
                btnMaterialMemberApply.Focus();

            else if (ctrl.Name == rbtnMatMemberNos.Name)
                txtMatMemberNos.Focus();

            else if (ctrl.Name == txtMatMemberNos.Name)
            {
                if (txtMatMemberNos.Text == "")
                    rbtnMatMemberRange.Focus();
                else
                    btnMaterialMemberApply.Focus();
            }
            else if (ctrl.Name == rbtnMatMemberRange.Name)
                txtMatStartMember.Focus();

            else if (ctrl.Name == txtMatStartMember.Name)
                txtMatEndMember.Focus();
            else if (ctrl.Name == txtMatEndMember.Name)
                btnMaterialMemberApply.Focus();
            else if (ctrl.Name == btnMaterialMemberApply.Name)
                btnNextProperty.Focus();
            else if (ctrl.Name == btnNextProperty.Name)
                txtElasticModulus.Focus();
        }

        private void btnAreaNextLoadCase_Click(object sender, EventArgs e)
        {
            AreaLoadCase += 1;
            txtAreaLoadNo.Text = "";
            AreaLoadNo = 1;
        }

        private void btnAreaPrevLoadCase_Click(object sender, EventArgs e)
        {
            AreaLoadCase -= 1;
            txtAreaLoadNo.Text = "";
            AreaLoadNo = 1;
        }

        private void btnAreaDelLoadCase_Click(object sender, EventArgs e)
        {
            app.AppDocument.AreaLoad.DeleteLoadcase(AreaLoadCase);
            AreaLoadNo = app.AppDocument.AreaLoad.MaxLoadNo(AreaLoadCase);
            txtAreaElementNos.Text = GetAreaLoadMember(AreaLoadCase);

        }

    }
    class LoadComb
    {
        public int loadCase;
        public List<int> loadColl;
        public List<double> multFactColl;
        public LoadComb()
        {
            loadCase = 0;
            loadColl = new List<int>();
            multFactColl = new List<double>();
        }
        public int IndexOf(int load)
        {
            for (int i = 0; i < loadColl.Count; i++)
            {
                if (loadColl[i] == load)
                    return i;
            }

            return -1;
        }
    }
}
