using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public partial class frm_Boundary_Elements : Form
    {
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

        public frm_Boundary_Elements(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
            IsInsert = false;
        }

        private void frm_Boundary_Elements_Load(object sender, EventArgs e)
        {
            if(IsEdit)
            {
                int c = 0;
                int r = row_index;
                btn_add_mem.Text = "Change";
                
                txt_node_N.Text = DGV[c++, r].Value.ToString();
                txt_node_I.Text = DGV[c++, r].Value.ToString();
                txt_node_J.Text = DGV[c++, r].Value.ToString();
                txt_node_K.Text = DGV[c++, r].Value.ToString();
                txt_node_L.Text = DGV[c++, r].Value.ToString();
                txt_disp_code.Text = DGV[c++, r].Value.ToString();
                txt_rot_code.Text = DGV[c++, r].Value.ToString();
                txt_data_gen.Text = DGV[c++, r].Value.ToString();
                txt_disp.Text = DGV[c++, r].Value.ToString();
                txt_rot.Text = DGV[c++, r].Value.ToString();
                txt_stiff.Text = DGV[c++, r].Value.ToString();
            }
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;

                DGV[c++, r].Value = txt_node_N.Text;
                DGV[c++, r].Value = txt_node_I.Text;
                DGV[c++, r].Value = txt_node_J.Text;
                DGV[c++, r].Value = txt_node_K.Text;
                DGV[c++, r].Value = txt_disp_code.Text;
                DGV[c++, r].Value = txt_rot_code.Text;
                DGV[c++, r].Value = txt_data_gen.Text;
                DGV[c++, r].Value = txt_disp.Text;
                DGV[c++, r].Value = txt_rot.Text;
                DGV[c++, r].Value = txt_stiff.Text;

                this.Close();
            }
            else
            {
                if (IsInsert)
                {

                    DGV.Rows.Insert(row_index, txt_node_N.Text
                        , txt_node_I.Text
                        , txt_node_J.Text
                        , txt_node_K.Text
                        , txt_disp_code.Text
                        , txt_rot_code.Text
                        , txt_data_gen.Text
                        , txt_disp.Text
                        , txt_rot.Text, txt_stiff.Text);
                }
                else
                {
                    DGV.Rows.Add(txt_node_N.Text
                        , txt_node_I.Text
                        , txt_node_J.Text
                        , txt_node_K.Text
                        , txt_disp_code.Text
                        , txt_rot_code.Text
                        , txt_data_gen.Text
                        , txt_disp.Text
                        , txt_rot.Text, txt_stiff.Text);
                }

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
