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
    public partial class frm_Member_Release : Form
    {

        List<int> list_mem_No = null;
        public List<string> ASTRA_Data { get; set; }

        public DataGridView DGV { get; set; }
        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return -1;
            }
        }

        public bool IsEdit { get; set; }

        public frm_Member_Release(DataGridView dgv_members, bool isEdit)
        {
            InitializeComponent();
            IsEdit = isEdit;
            DGV = dgv_members;
            ASTRA_Data = new List<string>();
        }
        private void frm_Member_Release_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {

            list_mem_No = MyList.Get_Array_Intiger(txt_mem_nos.Text);

            int nn = 0;
            int c = 0;

            string str = "";
            for (int i = 0; i < DGV.RowCount; i++)
            {
                nn = MyList.StringToInt(DGV[0, i].Value.ToString(), -1);


                c = 7;
                if (list_mem_No.Contains(nn))
                {
                    str = "";
                    str += chk_I_Fx.Checked ? "1" : "0";
                    str += chk_I_Fy.Checked ? "1" : "0";
                    str += chk_I_Fz.Checked ? "1" : "0";
                    str += chk_I_Mx.Checked ? "1" : "0";
                    str += chk_I_My.Checked ? "1" : "0";
                    str += chk_I_Mz.Checked ? "1" : "0";

                    if(str.Contains("1"))
                        DGV[6, i].Value = nn;

                    DGV[c++, i].Value = str;

                    str = "";
                    str += chk_J_Fx.Checked ? "1" : "0";
                    str += chk_J_Fy.Checked ? "1" : "0";
                    str += chk_J_Fz.Checked ? "1" : "0";
                    str += chk_J_Mx.Checked ? "1" : "0";
                    str += chk_J_My.Checked ? "1" : "0";
                    str += chk_J_Mz.Checked ? "1" : "0";
                    DGV[c++, i].Value = str;

                    if (str.Contains("1"))
                        DGV[6, i].Value = nn;

                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
