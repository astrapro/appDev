using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public partial class frm_Solid_Loads : Form
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

        public frm_Solid_Loads(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
        }
        private void frm_Solid_Loads_Load(object sender, EventArgs e)
        {

            if (IsEdit)
            {
                btn_add.Text = "Change";

                int c = 0;
                int r = row_index;
                if (r != -1)
                {
                    txt_load_no.Text = DGV[c++, r].Value.ToString();
                    cmb_LT.SelectedItem = DGV[c++, r].Value.ToString();
                    txt_P.Text = DGV[c++, r].Value.ToString();
                    txt_Y.Text = DGV[c++, r].Value.ToString();
                    cmb_face_no.SelectedItem = DGV[c++, r].Value.ToString();
                }
            }
            else
            {
                txt_load_no.Text = (DGV.RowCount + 1).ToString();
                cmb_LT.SelectedIndex = 0;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            if (IsEdit)
            {
                int c = 0;
                int r = row_index;

                DGV[c++, r].Value = txt_load_no.Text;
                DGV[c++, r].Value = cmb_LT.Text;
                DGV[c++, r].Value = txt_P.Text;
                DGV[c++, r].Value = txt_Y.Text;
                DGV[c++, r].Value = cmb_face_no.Text;

                this.Close();

            }
            else
            {

                DGV.Rows.Add(txt_load_no.Text,
                    cmb_LT.Text,
                    txt_P.Text,
                    txt_Y.Text,
                    cmb_face_no.Text
                    );
                txt_load_no.Text = (DGV.RowCount + 1).ToString();
            }
        }
    }
}
