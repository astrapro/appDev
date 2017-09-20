using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using AstraInterface.DataStructure;

namespace AstraAccess.SAP_Forms
{
    public delegate void deleDrawJoints(DataGridView DGV);

    public partial class frm_SAP_Joints : Form
    {

        public deleDrawJoints DrawJoints { get; set; }

        List<int> list_joint_No = null;
        public List<string> ASTRA_Data { get; set; }

        public DataGridView DGV { get; set; }

        public bool IsEdit { get; set; }
        public bool IsInsert { get; set; }

        public int JointNo
        {
            get
            {
                return MyList.StringToInt(txtNodeNo.Text, 1);
            }
            set
            {
                txtNodeNo.Text = value.ToString("f0");
            }
        }
        public double X
        {
            get
            {
                return MyList.StringToDouble(txtX.Text, 0.0);
            }
            set
            {
                txtX.Text = value.ToString("f4");
            }
        }
        public double Y
        {
            get
            {
                return MyList.StringToDouble(txtY.Text, 0.0);
            }
            set
            {
                txtY.Text = value.ToString("f4");
            }
        }
        public double Z
        {
            get
            {
                return MyList.StringToDouble(txtZ.Text, 0.0);
            }
            set
            {
                txtZ.Text = value.ToString("f4");
            }
        }

        public int Incr_JointNo
        {
            get
            {
                return MyList.StringToInt(txtNodeIncr.Text, 1);
            }
            set
            {
                txtNodeIncr.Text = value.ToString("f0");
            }
        }
        public double Incr_X
        {
            get
            {
                return MyList.StringToDouble(txtXIncr.Text, 0.0);
            }
            set
            {
                txtXIncr.Text = value.ToString("f4");
            }
        }
        public double Incr_Y
        {
            get
            {
                return MyList.StringToDouble(txtYIncr.Text, 0.0);
            }
            set
            {
                txtYIncr.Text = value.ToString("f4");
            }
        }
        public double Incr_Z
        {
            get
            {
                return MyList.StringToDouble(txtZIncr.Text, 0.0);
            }
            set
            {
                txtZIncr.Text = value.ToString("f4");
            }
        }

        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return -1;
            }
        }

        public frm_SAP_Joints(DataGridView dgv_joints, bool isEdit)
        {
            InitializeComponent();
            IsEdit = isEdit;
            DGV = dgv_joints;
            ASTRA_Data = new List<string>();
            list_joint_No = new List<int>();

            IsInsert = false;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int c = 0;
            if (btn.Name == btn_add.Name)
            {
                if (IsEdit)
                {
                    c = 0;
                    int r = -1;
                    if (DGV.SelectedCells.Count > 0)
                        r = DGV.SelectedCells[0].RowIndex;

                    DGV[0, r].Value = txtNodeNo.Text;
                    DGV[1, r].Value = txtX.Text;
                    DGV[2, r].Value = txtY.Text;
                    DGV[3, r].Value = txtZ.Text;
                    DGV[4, r].Value = chkXt.Checked;
                    DGV[5, r].Value = chkYt.Checked;
                    DGV[6, r].Value = chkZt.Checked;
                    DGV[7, r].Value = chkXr.Checked;
                    DGV[8, r].Value = chkYr.Checked;
                    DGV[9, r].Value = chkZr.Checked;
                    this.Close();
                }
                else
                {

                    if(IsInsert)
                    {

                        DGV.Rows.Insert(row_index, JointNo,
                             X.ToString("f4")
                            , Y.ToString("f4")
                            , Z.ToString("f4")
                            , chkXt.Checked
                            , chkYt.Checked
                            , chkZt.Checked
                            , chkXr.Checked
                            , chkYr.Checked
                            , chkZr.Checked
                            );

                    }
                    else
                    {

                        DGV.Rows.Add(JointNo,
                             X.ToString("f4")
                            , Y.ToString("f4")
                            , Z.ToString("f4")
                            , chkXt.Checked
                            , chkYt.Checked
                            , chkZt.Checked
                            , chkXr.Checked
                            , chkYr.Checked
                            , chkZr.Checked
                            );

                    }
                    if (chkIncrOn.Checked)
                    {
                        JointNo = JointNo + Incr_JointNo;
                        X = X + Incr_X;
                        Y = Y + Incr_Y;
                        Z = Z + Incr_Z;
                    }

                    DrawJoints(DGV);
                }
            }
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_SAP_Joints_Load(object sender, EventArgs e)
        {
            if (IsEdit || IsInsert)
            {
                if(IsInsert)
                    btn_add.Text = "INSERT";
                else
                    btn_add.Text = "Change";
                int r = -1;

                if (DGV.SelectedCells.Count > 0)
                    r = DGV.SelectedCells[0].RowIndex;


                if (r != -1)
                {
                    txtNodeNo.Text = DGV[0, r].Value.ToString();
                    txtX.Text = DGV[1, r].Value.ToString();
                    txtY.Text = DGV[2, r].Value.ToString();
                    txtZ.Text = DGV[3, r].Value.ToString();
                    chkXt.Checked = (bool)DGV[4, r].Value;
                    chkYt.Checked = (bool)DGV[5, r].Value;
                    chkZt.Checked = (bool)DGV[6, r].Value;
                    chkXr.Checked = (bool)DGV[7, r].Value;
                    chkYr.Checked = (bool)DGV[8, r].Value;
                    chkZr.Checked = (bool)DGV[9, r].Value;
                }
            }
            else
            {
                txtNodeNo.Text = (DGV.RowCount + 1) + "";
                txtNodeIncr.Text = "1";
            }
        }
    }
   
}
