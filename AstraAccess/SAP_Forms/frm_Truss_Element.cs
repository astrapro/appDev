using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess.SAP_Forms
{
    public delegate void deleDrawTrusses(DataGridView DGV);

    public partial class frm_Truss_Element : Form
    {
        //public delegate void deleDrawBeams(DataGridView DGV);

        public deleDrawTrusses DrawTrusses { get; set; }
        public List<int> Selected_Nodes { get; set; }

        public DataGridView DGV { get; set; }

        public bool IsEdit { get; set; }
        public bool IsInsert { get; set; }


        public int Member_No { get { return int.Parse(txt_mbr_start_no.Text); } }
        public int Start_Joint { get { return int.Parse(txt_mbr_start_jnt.Text); } }
        public int End_Joint { get { return int.Parse(txt_mbr_end_jnt.Text); } }
        public int Member_No_Incr { get { return int.Parse(txt_incr_start_no.Text); } }
        public int Start_Joint_Incr { get { return int.Parse(txt_incr_start_jnt.Text); } }
        public int End_Joint_Incr { get { return int.Parse(txt_incr_end_jnt.Text); } }


        public int Material_Props_No { get { return int.Parse(txt_Mat_Prop_No.Text); } }

        public int row_index
        {
            get
            {
                if (DGV.SelectedCells.Count > 0)
                    return DGV.SelectedCells[0].RowIndex;
                return -1;
            }
        }

        public frm_Truss_Element(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;

            IsInsert = false;
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                int r = row_index;

                if (r != -1)
                {
                    DGV[0, r].Value = Member_No;
                    DGV[1, r].Value = Start_Joint;
                    DGV[2, r].Value = End_Joint;
                    DGV[3, r].Value = Material_Props_No;
                    DrawTrusses(DGV);
                }
                this.Close();
            }
            else
            {
                if (IsInsert)
                {
                    DGV.Rows.Insert(row_index, Member_No,
                    Start_Joint, End_Joint, Material_Props_No);
                
                }
                else
                {
                    DGV.Rows.Add(Member_No,
                        Start_Joint, End_Joint, Material_Props_No);
                }
                txt_mbr_start_no.Text = (Member_No + Member_No_Incr).ToString();
                txt_mbr_start_jnt.Text = (Start_Joint + Start_Joint_Incr).ToString();
                txt_mbr_end_jnt.Text = (End_Joint + End_Joint_Incr).ToString();
       
                DrawTrusses(DGV);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Truss_Element_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                btn_add_mem.Text = "Change";
                int r = -1;

                if (DGV.SelectedCells.Count > 0)
                    r = DGV.SelectedCells[0].RowIndex;

                if (r != -1)
                {
                    grb_incr.Enabled = false;

                    txt_mbr_start_no.Text = DGV[0, r].Value.ToString();
                    txt_mbr_start_jnt.Text = DGV[1, r].Value.ToString();
                    txt_mbr_end_jnt.Text = DGV[2, r].Value.ToString();
                    txt_Mat_Prop_No.Text = DGV[3, r].Value.ToString();
                }
            }
            else
            {

                txt_mbr_start_no.Text = (DGV.RowCount + 1) + "";
                if (DGV.RowCount == 0)
                {
                    txt_incr_start_no.Text = "1";
                    txt_incr_start_jnt.Text = "1";
                    txt_incr_end_jnt.Text = "1";
                }
                if (Selected_Nodes != null)
                {
                    if (Selected_Nodes.Count > 1)
                    {
                        int c = 0;

                        txt_mbr_start_jnt.Text = Selected_Nodes[c++].ToString();
                        txt_mbr_end_jnt.Text = Selected_Nodes[c++].ToString();
                    }
                }
            }
        }
    }
}
