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
    public delegate void deleDrawPlates();
    public partial class frm_Plate_Elements : Form
    {

        public DataGridView DGV { get; set; }
        public deleDrawPlates DrawPlates { get; set; }


        public bool IsEdit { get; set; }
        public bool IsInsert { get; set; }

        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return 0;
            }
        }

        public List<int> Selected_Nodes { get; set; }

        public frm_Plate_Elements(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {
            if(IsEdit)
            {
                int c = 0;
                int r = row_index;

                DGV[c++, r].Value = txt_no.Text;
                DGV[c++, r].Value = txt_node_I.Text;
                DGV[c++, r].Value = txt_node_J.Text;
                DGV[c++, r].Value = txt_node_K.Text;
                DGV[c++, r].Value = txt_node_L.Text;
                DGV[c++, r].Value = txt_node_O.Text;
                DGV[c++, r].Value = txt_mat_prop_no.Text;
                DGV[c++, r].Value = txt_data_gen_no.Text;
                DGV[c++, r].Value = txt_thickness.Text;
                DGV[c++, r].Value = txt_pressure.Text;
                DGV[c++, r].Value = txt_temp_variation.Text;
                DGV[c++, r].Value = txt_temp_grad.Text;
                this.Close();
            }
            else
            {
                if (IsInsert)
                {

                    DGV.Rows.Insert(row_index, txt_no.Text,
                                    txt_node_I.Text,
                                    txt_node_J.Text,
                                    txt_node_K.Text,
                                    txt_node_L.Text,
                                    txt_node_O.Text,
                                    txt_mat_prop_no.Text,
                                    txt_data_gen_no.Text,
                                    txt_thickness.Text,
                                    txt_pressure.Text,
                                    txt_temp_variation.Text,
                                    txt_temp_grad.Text);
                }
                else
                {
                    DGV.Rows.Add(txt_no.Text,
                                    txt_node_I.Text,
                                    txt_node_J.Text,
                                    txt_node_K.Text,
                                    txt_node_L.Text,
                                    txt_node_O.Text,
                                    txt_mat_prop_no.Text,
                                    txt_data_gen_no.Text,
                                    txt_thickness.Text,
                                    txt_pressure.Text,
                                    txt_temp_variation.Text,
                                    txt_temp_grad.Text);
                }
                txt_no.Text = (MyList.StringToInt(txt_no.Text, 1) + 1).ToString();

                DrawPlates();
            }
        }

        private void frm_Plate_Elements_Load(object sender, EventArgs e)
        {
            if(IsEdit || IsInsert)
            {
                int c = 0;
                int r = 0;
                if(IsInsert)
                    btn_add_mem.Text = "INSERT";
                else
                    btn_add_mem.Text = "Change";
                if(DGV.SelectedCells.Count > 0)
                    r = DGV.SelectedCells[0].RowIndex;

                txt_no.Text = DGV[c++, r].Value.ToString();
                txt_node_I.Text = DGV[c++, r].Value.ToString();
                txt_node_J.Text = DGV[c++, r].Value.ToString();
                txt_node_K.Text = DGV[c++, r].Value.ToString();
                txt_node_L.Text = DGV[c++, r].Value.ToString();
                txt_node_O.Text = DGV[c++, r].Value.ToString();
                txt_mat_prop_no.Text = DGV[c++, r].Value.ToString();
                txt_data_gen_no.Text = DGV[c++, r].Value.ToString();
                txt_thickness.Text = DGV[c++, r].Value.ToString();
                txt_pressure.Text = DGV[c++, r].Value.ToString();
                txt_temp_variation.Text = DGV[c++, r].Value.ToString();
                txt_temp_grad.Text = DGV[c++, r].Value.ToString();

            }
            else
            {
                txt_no.Text = (DGV.RowCount + 1).ToString();
                if (Selected_Nodes != null)
                {
                    if (Selected_Nodes.Count > 3)
                    {
                        int c = 0;

                        txt_node_I.Text = Selected_Nodes[c++].ToString();
                        txt_node_J.Text = Selected_Nodes[c++].ToString();
                        txt_node_K.Text = Selected_Nodes[c++].ToString();
                        txt_node_L.Text = Selected_Nodes[c++].ToString();
                    }
                }

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
