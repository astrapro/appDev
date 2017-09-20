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
    public partial class frm_Solid_Elements : Form
    {
        public List<int> Selected_Nodes { get; set; }
        public deleDrawMembers DrawSolids { get; set; }

        public DataGridView DGV { get; set; }

        public bool IsEdit { get; set; }
        public bool IsInsert { get; set; }

        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return -1;
            }
        }

        public frm_Solid_Elements(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
            IsInsert = false;
        }
        private void chk_incr_CheckedChanged(object sender, EventArgs e)
        {
            grb_incr.Enabled = chk_incr.Checked;
        }

        private void frm_Solid_Elements_Load(object sender, EventArgs e)
        {
            txt_inte_order.SelectedIndex = 0;
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;
                btn_add_mem.Text = "Change";

                txt_elem_no.Text = DGV[c++, r].Value.ToString();
                txt_node_1.Text = DGV[c++, r].Value.ToString();
                txt_node_2.Text = DGV[c++, r].Value.ToString();
                txt_node_3.Text = DGV[c++, r].Value.ToString();
                txt_node_4.Text = DGV[c++, r].Value.ToString();
                txt_node_5.Text = DGV[c++, r].Value.ToString();
                txt_node_6.Text = DGV[c++, r].Value.ToString();
                txt_node_7.Text = DGV[c++, r].Value.ToString();
                txt_node_8.Text = DGV[c++, r].Value.ToString();
                txt_inte_order.SelectedItem = DGV[c++, r].Value.ToString();
                txt_mat_no.Text = DGV[c++, r].Value.ToString();
                txt_gen_param.Text = DGV[c++, r].Value.ToString();
                txt_LSA.Text = DGV[c++, r].Value.ToString();
                txt_LSB.Text = DGV[c++, r].Value.ToString();
                txt_LSC.Text = DGV[c++, r].Value.ToString();
                txt_LSD.Text = DGV[c++, r].Value.ToString();
                txt_face_no.Text = DGV[c++, r].Value.ToString();
                txt_stress_temp.Text = DGV[c++, r].Value.ToString();
            }
            else
            {
                txt_elem_no.Text = (DGV.RowCount + 1).ToString();
                if(Selected_Nodes != null)
                {
                    if(Selected_Nodes.Count > 7)
                    {
                        int c = 0;

                        txt_node_1.Text = Selected_Nodes[c++].ToString();
                        txt_node_2.Text = Selected_Nodes[c++].ToString();
                        txt_node_3.Text = Selected_Nodes[c++].ToString();
                        txt_node_4.Text = Selected_Nodes[c++].ToString();
                        txt_node_5.Text = Selected_Nodes[c++].ToString();
                        txt_node_6.Text = Selected_Nodes[c++].ToString();
                        txt_node_7.Text = Selected_Nodes[c++].ToString();
                        txt_node_8.Text = Selected_Nodes[c++].ToString();
                    }
                }


            }
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {

            chk_incr.Checked = !IsEdit;
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;

                DGV[c++, r].Value = txt_elem_no.Text;
                DGV[c++, r].Value = txt_node_1.Text;
                DGV[c++, r].Value = txt_node_2.Text;
                DGV[c++, r].Value = txt_node_3.Text;
                DGV[c++, r].Value = txt_node_4.Text;
                DGV[c++, r].Value = txt_node_5.Text;
                DGV[c++, r].Value = txt_node_6.Text;
                DGV[c++, r].Value = txt_node_7.Text;
                DGV[c++, r].Value = txt_node_8.Text;
                DGV[c++, r].Value = txt_inte_order.Text;
                DGV[c++, r].Value = txt_mat_no.Text;
                DGV[c++, r].Value = txt_gen_param.Text;
                DGV[c++, r].Value = txt_LSA.Text;
                DGV[c++, r].Value = txt_LSB.Text;
                DGV[c++, r].Value = txt_LSC.Text;
                DGV[c++, r].Value = txt_LSD.Text;
                DGV[c++, r].Value = txt_face_no.Text;
                DGV[c++, r].Value = txt_stress_temp.Text;

                this.Close();
            }
            else
            {
                if (IsInsert)
                {

                    DGV.Rows.Insert(row_index,
                                    txt_elem_no.Text,
                                    txt_node_1.Text,
                                    txt_node_2.Text,
                                    txt_node_3.Text,
                                    txt_node_4.Text,
                                    txt_node_5.Text,
                                    txt_node_6.Text,
                                    txt_node_7.Text,
                                    txt_node_8.Text,
                                    txt_inte_order.Text,
                                    txt_mat_no.Text,
                                    txt_gen_param.Text,
                                    txt_LSA.Text,
                                    txt_LSB.Text,
                                    txt_LSC.Text,
                                    txt_LSD.Text,
                                    txt_face_no.Text,
                                    txt_stress_temp.Text);
                }
                else
                {

                    DGV.Rows.Add(
                            txt_elem_no.Text,
                            txt_node_1.Text,
                            txt_node_2.Text,
                            txt_node_3.Text,
                            txt_node_4.Text,
                            txt_node_5.Text,
                            txt_node_6.Text,
                            txt_node_7.Text,
                            txt_node_8.Text,
                            txt_inte_order.Text,
                            txt_mat_no.Text,
                            txt_gen_param.Text,
                            txt_LSA.Text,
                            txt_LSB.Text,
                            txt_LSC.Text,
                            txt_LSD.Text,
                            txt_face_no.Text,
                            txt_stress_temp.Text);
                    DrawSolids();
                }
                if (chk_incr.Checked)
                {

                    txt_elem_no.Text = (MyList.StringToInt(txt_elem_no.Text, 0) + MyList.StringToInt(txt_elem_no_incr.Text, 0)).ToString();
                    txt_node_1.Text = (MyList.StringToInt(txt_node_1.Text, 0) + MyList.StringToInt(txt_node_1_incr.Text, 0)).ToString();
                    txt_node_2.Text = (MyList.StringToInt(txt_node_2.Text, 0) + MyList.StringToInt(txt_node_2_incr.Text, 0)).ToString();

                    txt_node_3.Text = (MyList.StringToInt(txt_node_3.Text, 0) + MyList.StringToInt(txt_node_3_incr.Text, 0)).ToString();
                    txt_node_4.Text = (MyList.StringToInt(txt_node_4.Text, 0) + MyList.StringToInt(txt_node_4_incr.Text, 0)).ToString();
                    txt_node_5.Text = (MyList.StringToInt(txt_node_5.Text, 0) + MyList.StringToInt(txt_node_5_incr.Text, 0)).ToString();

                    txt_node_6.Text = (MyList.StringToInt(txt_node_6.Text, 0) + MyList.StringToInt(txt_node_6_incr.Text, 0)).ToString();
                    txt_node_7.Text = (MyList.StringToInt(txt_node_7.Text, 0) + MyList.StringToInt(txt_node_7_incr.Text, 0)).ToString();
                    txt_node_8.Text = (MyList.StringToInt(txt_node_8.Text, 0) + MyList.StringToInt(txt_node_8_incr.Text, 0)).ToString();
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
