using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
namespace AstraAccess.SAP_Forms
{
    public partial class frm_Supports : Form
    {
        public List<int> Selected_Nodes { get; set; }

        public deleDrawJoints DrawJoints { get; set; }

        List<int> list_joint_No = null;
        public List<string> ASTRA_Data { get; set; }

        public DataGridView DGV { get; set; }
        public DataGridView DGV_Boundary { get; set; }


        public bool IsEdit { get; set; }

        public frm_Supports(DataGridView dgv_joints, bool isEdit)
        {
            InitializeComponent();
            IsEdit = isEdit;
            DGV = dgv_joints;
            ASTRA_Data = new List<string>();
            list_joint_No = new List<int>();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            list_joint_No = MyList.Get_Array_Intiger(txt_joint_nos.Text);

            int nn = 0;
            int c = 0;

            double kVal = MyList.StringToDouble(txt_kVal.Text, 0.0);
            double x = 0.0;
            double y = 0.0;
            double z = 0.0;

            double fact = MyList.StringToDouble(txt_displacement.Text, 1);


            if (kVal != 0.0)
            {
                fact = kVal > 0 ? -1 * fact : 1 * fact;
                if (rbtn_kFX.Checked)
                {
                    x = fact;
                }
                if (rbtn_kFY.Checked)
                {
                    y = fact;
                }
                if (rbtn_kFZ.Checked)
                {
                    z = fact;
                }
            }
            for (int i = 0; i < DGV.RowCount; i++)
            {
                nn = MyList.StringToInt(DGV[0, i].Value.ToString(), -1);

                c = 4;
                if(list_joint_No.Contains(nn))
                {
                    DGV[c++, i].Value = chk_fx.Checked;
                    DGV[c++, i].Value = chk_fy.Checked;
                    DGV[c++, i].Value = chk_fz.Checked;
                    DGV[c++, i].Value = chk_mx.Checked;
                    DGV[c++, i].Value = chk_my.Checked;
                    DGV[c++, i].Value = chk_mz.Checked;

                    if(kVal != 0.0)
                    {

                        double _xc = MyList.StringToDouble(DGV[1,i].Value.ToString(), 0.0);
                        double _yc = MyList.StringToDouble(DGV[2,i].Value.ToString(), 0.0);
                        double _zc = MyList.StringToDouble(DGV[3,i].Value.ToString(), 0.0);
                        int N = DGV.RowCount + 1;
                        Set_Boundary(N, nn);

                        DGV.Rows.Add(N,
                            (_xc + x).ToString("f4"),
                            (_yc + y).ToString("f4"),
                            (_zc + z).ToString("f4"),
                            chk_fx.Checked,
                            chk_fy.Checked,
                            chk_fz.Checked,
                            chk_mx.Checked,
                            chk_my.Checked,
                            chk_mz.Checked
                            );

                    }
                }
            }
            DrawJoints(DGV);
        }
        public void Set_Boundary(int Node_N, int Node_I)
        {
            if (DGV_Boundary == null) return;
            for (int i = 0; i < DGV_Boundary.RowCount; i++)
            {
                if (DGV_Boundary[0, i].Value.ToString() == Node_I.ToString())
                {
                    int c = 0;
                    DGV_Boundary[c++, i].Value = Node_I;
                    DGV_Boundary[c++, i].Value = Node_N;
                    c++;
                    c++;
                    c++;
                    c++;
                    c++;
                    c++;
                    DGV_Boundary[c++, i].Value = txt_displacement.Text;
                    c++;
                    DGV_Boundary[c++, i].Value = txt_kVal.Text;
                    return;
                }
            }

            //1   17    0    0    0    1    0    0     1.000     0.000  2000.000
            //2   18    0    0    0    1    0    0     1.000     0.000  2000.000
            DGV_Boundary.Rows.Add(Node_I, Node_N, 0, 0, 0, 1, 0, 0, txt_displacement.Text, 0, txt_kVal.Text);

        }
        private void cmb_support_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_support_type.SelectedIndex == 1)
            {
                chk_fx.Checked = true;
                chk_fy.Checked = true;
                chk_fz.Checked = true;

                chk_mx.Checked = false;
                chk_my.Checked = false;
                chk_mz.Checked = false;
            }
            else
            {
                chk_fx.Checked = true;
                chk_fy.Checked = true;
                chk_fz.Checked = true;

                chk_mx.Checked = true;
                chk_my.Checked = true;
                chk_mz.Checked = true;
            }
        }

        private void frm_Supports_Load(object sender, EventArgs e)
        {
            cmb_support_type.SelectedIndex = 0;

            if(Selected_Nodes != null)
            {
                if(Selected_Nodes.Count > 0)
                {
                    txt_joint_nos.Text = MyList.Get_Array_Text(Selected_Nodes);
                }
            }
        }

    }
}
