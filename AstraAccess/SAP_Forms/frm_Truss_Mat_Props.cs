using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public partial class frm_Truss_Mat_Props : Form
    {

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

        public frm_Truss_Mat_Props(DataGridView dgv, bool isEdit)
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

                if(r != -1)
                {
                    DGV[c++, r].Value = txt_mat_no.Text;
                    DGV[c++, r].Value = txt_mod.Text;
                    DGV[c++, r].Value = txt_therm_coeff.Text;
                    DGV[c++, r].Value = txt_mass_den.Text;
                    DGV[c++, r].Value = txt_area.Text;
                    DGV[c++, r].Value = txt_weight_den.Text;
                }
                this.Close();
            }
            else
            {
                //if (row_index == -1)
                //{
                DGV.Rows.Add(txt_mat_no.Text, txt_mod.Text,
                    txt_therm_coeff.Text, txt_mass_den.Text,
                    txt_area.Text, txt_weight_den.Text);
                //}
                //else
                //{
                //    DGV.Rows.Insert(row_index, txt_mat_no.Text, txt_mod.Text,
                //        txt_therm_coeff.Text, txt_mass_den.Text,
                //        txt_area.Text, txt_weight_den.Text);
                //}
                txt_mat_no.Text = (DGV.RowCount + 1).ToString();
            }
        }

        private void frm_Truss_Mat_Props_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;
                btn_add_mem.Text = "Change";
                if (r != -1)
                {
                    txt_mat_no.Text = DGV[c++, r].Value.ToString();
                    txt_mod.Text = DGV[c++, r].Value.ToString();
                    txt_therm_coeff.Text = DGV[c++, r].Value.ToString();
                    txt_mass_den.Text = DGV[c++, r].Value.ToString();
                    txt_area.Text = DGV[c++, r].Value.ToString();
                    txt_weight_den.Text = DGV[c++, r].Value.ToString();
                }
            }
            else
            {
                txt_mat_no.Text = (DGV.RowCount + 1).ToString();

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
