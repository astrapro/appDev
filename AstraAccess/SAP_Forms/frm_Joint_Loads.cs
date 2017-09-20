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
    public partial class frm_Joint_Loads : Form
    {

        public List<int> Selected_Nodes { get; set; }
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

        public frm_Joint_Loads(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
        }

        private void frm_Joint_Loads_Load(object sender, EventArgs e)
        {
            if(IsEdit)
            {
                btn_jload_add.Text = "Change";
                int c = 0;
                int r = row_index;
                if (r != -1)
                {
                    txt_joint_number.Text = DGV[c++, r].Value.ToString();
                    txt_load_case.Text = DGV[c++, r].Value.ToString();
                    txt_fx.Text = DGV[c++, r].Value.ToString();
                    txt_fy.Text = DGV[c++, r].Value.ToString();
                    txt_fz.Text = DGV[c++, r].Value.ToString();
                    txt_mx.Text = DGV[c++, r].Value.ToString();
                    txt_my.Text = DGV[c++, r].Value.ToString();
                    txt_mz.Text = DGV[c++, r].Value.ToString();
                }
            }
            else
            {
                if(Selected_Nodes != null)
                {
                    if(Selected_Nodes.Count > 1)
                    {
                        txt_joint_number.Text = MyList.Get_Array_Text(Selected_Nodes);
                    }
                }
            }
        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {

            int c = 0;
            int r = row_index;

            if (IsEdit)
            {
                c = 0;
                DGV[c++, r].Value = txt_joint_number.Text;
                DGV[c++, r].Value = txt_load_case.Text;
                DGV[c++, r].Value = txt_fx.Text;
                DGV[c++, r].Value = txt_fy.Text;
                DGV[c++, r].Value = txt_fz.Text;
                DGV[c++, r].Value = txt_mx.Text;
                DGV[c++, r].Value = txt_my.Text;
                DGV[c++, r].Value = txt_mz.Text;

                this.Close();
            }
            else
            {
                List<int> joints = MyList.Get_Array_Intiger(txt_joint_number.Text);

                foreach (var item in joints)
                {
                    DGV.Rows.Add(item,
                        txt_load_case.Text,
                        MyList.StringToDouble(txt_fx.Text, 0.0).ToString("F3"),
                        MyList.StringToDouble(txt_fy.Text, 0.0).ToString("F3"),
                        MyList.StringToDouble(txt_fz.Text, 0.0).ToString("F3"),
                        MyList.StringToDouble(txt_mx.Text, 0.0).ToString("F3"),
                        MyList.StringToDouble(txt_my.Text, 0.0).ToString("F3"),
                        MyList.StringToDouble(txt_mz.Text, 0.0).ToString("F3")
                        );
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
