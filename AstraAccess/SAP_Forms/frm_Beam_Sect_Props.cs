using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public partial class frm_Beam_Sect_Props : Form
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

        public frm_Beam_Sect_Props(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
        }

        private void frm_Beam_Sect_Props_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;
                btn_add_mem.Text = "Change";
                if(r != -1)
                {
                    txt_sec_no.Text = DGV[c++, r].Value.ToString();
                    txt_AX.Text = DGV[c++, r].Value.ToString();
                    txt_AY.Text = DGV[c++, r].Value.ToString();
                    txt_AZ.Text = DGV[c++, r].Value.ToString();
                    txt_IX.Text = DGV[c++, r].Value.ToString();
                    txt_IY.Text = DGV[c++, r].Value.ToString();
                    txt_IZ.Text = DGV[c++, r].Value.ToString();
                }

            }
            else
            {
                txt_sec_no.Text = (DGV.RowCount + 1).ToString();
            }

            txt_AX.Focus();
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;
                DGV[c++, r].Value = txt_sec_no.Text;
                DGV[c++, r].Value = txt_AX.Text;
                DGV[c++, r].Value = txt_AY.Text;
                DGV[c++, r].Value = txt_AZ.Text;
                DGV[c++, r].Value = txt_IX.Text;
                DGV[c++, r].Value = txt_IY.Text;
                DGV[c++, r].Value = txt_IZ.Text;
                this.Close();
            }
            else
            {
                DGV.Rows.Add(
                txt_sec_no.Text,
                txt_AX.Text,
                txt_AY.Text,
                txt_AZ.Text,
                txt_IX.Text,
                txt_IY.Text,
                txt_IZ.Text);

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
