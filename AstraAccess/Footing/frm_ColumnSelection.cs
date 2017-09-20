using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.Footing
{
    public partial class frm_ColumnSelection : Form
    {
        public frm_ColumnSelection()
        {
            InitializeComponent();
        }
        public string Coordinates
        {
            get
            {
                return txt_coordinates.Text;
            }
            set
            {
                txt_coordinates.Text = value;
            }
        }
        public string GridLength
        {
            get
            {
                return txt_grid_lengths.Text;
            }
            set
            {
                txt_grid_lengths.Text = value;
            }
        }
        public string L
        {
            get
            {
                return txt_L.Text;
            }
            set
            {
                txt_L.Text = value;
            }
        }
        public string B
        {
            get
            {
                return txt_B.Text;
            }
            set
            {
                txt_B.Text = value;
            }
        }

        public string Load
        {
            get
            {
                return txt_Load.Text;
            }
            set
            {
                txt_Load.Text = value;
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btn_finish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
