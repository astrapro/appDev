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
    public partial class frm_Time_Nodal : Form
    {
        
        public List<int> Selected_Nodes { get; set; }

        public deleDrawJoints DrawJoints { get; set; }

        List<int> list_joint_No = null;
        public List<string> ASTRA_Data { get; set; }

        public DataGridView DGV { get; set; }

        public bool IsEdit { get; set; }

        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return -1;
            }
        }

        public frm_Time_Nodal(DataGridView dgv_joints, bool isEdit)
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            int c = 0;
            int r = row_index;

            if (IsEdit)
            {
                c = 0;
                DGV[c++, r].Value = txt_joint_nos.Text;
                DGV[c++, r].Value = chk_Tx.Checked;
                DGV[c++, r].Value = chk_Ty.Checked;
                DGV[c++, r].Value = chk_Tz.Checked;
                DGV[c++, r].Value = chk_Rx.Checked;
                DGV[c++, r].Value = chk_Ry.Checked;
                DGV[c++, r].Value = chk_Rz.Checked;

                this.Close();
            }
            else
            {
                List<int> joints = MyList.Get_Array_Intiger(txt_joint_nos.Text);

                foreach (var item in joints)
                {
                    DGV.Rows.Add(item,
                        chk_Tx.Checked,
                        chk_Ty.Checked,
                        chk_Tz.Checked,
                        chk_Rx.Checked,
                        chk_Ry.Checked,
                        chk_Rz.Checked
                        );
                }
            }
        }


        private void frm_Time_Nodal_Load(object sender, EventArgs e)
        {

            if (IsEdit)
            {
                btnAddData.Text = "Change";
                int c = 0;
                int r = row_index;
                if (r != -1)
                {
                    txt_joint_nos.Text = DGV[c++, r].Value.ToString();
                    chk_Tx.Checked = (bool)DGV[c++, r].Value;
                    chk_Ty.Checked = (bool)DGV[c++, r].Value;
                    chk_Tz.Checked = (bool)DGV[c++, r].Value;
                    chk_Rx.Checked = (bool)DGV[c++, r].Value;
                    chk_Ry.Checked = (bool)DGV[c++, r].Value;
                    chk_Rz.Checked = (bool)DGV[c++, r].Value;
                }
            }
            else
            {
                if (Selected_Nodes != null)
                {
                    if (Selected_Nodes.Count > 0)
                    {
                        txt_joint_nos.Text = MyList.Get_Array_Text(Selected_Nodes);
                    }
                }
            }

        }

    }
}
