﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public partial class frm_Plate_Mat_Props : Form
    {
        public DataGridView DGV { get; set; }
        public deleDrawPlates DrawPlates { get; set; }


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

        public frm_Plate_Mat_Props(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
        }

        private void frm_Plate_Mat_Props_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;

                btn_add_mem.Text = "Change";

                txt_mat_no.Text = DGV[c++, r].Value.ToString();
                txt_mass_den.Text = DGV[c++, r].Value.ToString();
                txt_Ax.Text = DGV[c++, r].Value.ToString();
                txt_Ay.Text = DGV[c++, r].Value.ToString();
                txt_Axy.Text = DGV[c++, r].Value.ToString();
                txt_Cxx.Text = DGV[c++, r].Value.ToString();
                txt_Cxy.Text = DGV[c++, r].Value.ToString();
                txt_Cxs.Text = DGV[c++, r].Value.ToString();
                txt_Cyy.Text = DGV[c++, r].Value.ToString();
                txt_Cys.Text = DGV[c++, r].Value.ToString();
                txt_Gxy.Text = DGV[c++, r].Value.ToString();
            }
            else
            {
                txt_mat_no.Text = (DGV.RowCount + 1).ToString();

            }
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int c = 0;
                int r = row_index;

                if(r != -1)
                {
                    DGV[c++, r].Value = txt_mat_no.Text;
                    DGV[c++, r].Value = txt_mass_den.Text;
                    DGV[c++, r].Value = txt_Ax.Text;
                    DGV[c++, r].Value = txt_Ay.Text;
                    DGV[c++, r].Value = txt_Axy.Text;
                    DGV[c++, r].Value = txt_Cxx.Text;
                    DGV[c++, r].Value = txt_Cxy.Text;
                    DGV[c++, r].Value = txt_Cxs.Text;
                    DGV[c++, r].Value = txt_Cyy.Text;
                    DGV[c++, r].Value = txt_Cys.Text;
                    DGV[c++, r].Value = txt_Gxy.Text; 
                }
                this.Close();
            }
            else
            {
                DGV.Rows.Add(txt_mat_no.Text, txt_mass_den.Text, txt_Ax.Text,
                    txt_Ay.Text, txt_Axy.Text, txt_Cxx.Text, txt_Cxy.Text, txt_Cxs.Text,
                    txt_Cyy.Text, txt_Cys.Text, txt_Gxy.Text);

                txt_mat_no.Text = (DGV.RowCount + 1).ToString();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
