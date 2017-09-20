using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraAccess.SAP_Classes;

namespace AstraAccess.SAP_Forms
{
    public delegate void deleDrawMembers();

    public partial class frm_Beam_Elements : Form
    {
        public deleDrawMembers DrawBeams { get; set; }

        public DataGridView DGV { get; set; }

        public bool IsEdit { get; set; }
        public bool IsInsert { get; set; }

        public List<int> Selected_Nodes { get; set; }

        public int Member_No { get { return int.Parse(txt_mbr_No.Text); } }
        public int NODE_I { get { return int.Parse(txt_node_I.Text); } }
        public int NODE_J { get { return int.Parse(txt_node_J.Text); } }
        public int NODE_K { get { return int.Parse(txt_node_K.Text); } }
        public int Member_No_Incr { get { return int.Parse(txt_incr_no.Text); } }
        public int Start_Joint_Incr { get { return int.Parse(txt_incr_start_jnt.Text); } }
        public int End_Joint_Incr { get { return int.Parse(txt_incr_end_jnt.Text); } }

        public int Material_No { get { return int.Parse(txt_mat_prop_no.Text); } }
        public int Section_No { get { return int.Parse(txt_sec_prop_no.Text); } }

        public kString NODE_I_Release
        {
            get
            {
                string str = "";

                str = "";
                str += chk_I_Fx.Checked ? "1" : "0";
                str += chk_I_Fy.Checked ? "1" : "0";
                str += chk_I_Fz.Checked ? "1" : "0";
                str += chk_I_Mx.Checked ? "1" : "0";
                str += chk_I_My.Checked ? "1" : "0";
                str += chk_I_Mz.Checked ? "1" : "0";
                return str;
            }
            set
            {
                chk_I_Fx.Checked = value.Get_Int(1, 1) == 1;
                chk_I_Fy.Checked = value.Get_Int(2, 2) == 1;
                chk_I_Fz.Checked = value.Get_Int(3, 3) == 1;
                chk_I_Mx.Checked = value.Get_Int(4, 4) == 1;
                chk_I_My.Checked = value.Get_Int(5, 5) == 1;
                chk_I_Mz.Checked = value.Get_Int(6, 6) == 1;
            }
        }

        public kString NODE_J_Release
        {
            get
            {
                string str = "";

                str = "";
                str += chk_J_Fx.Checked ? "1" : "0";
                str += chk_J_Fy.Checked ? "1" : "0";
                str += chk_J_Fz.Checked ? "1" : "0";
                str += chk_J_Mx.Checked ? "1" : "0";
                str += chk_J_My.Checked ? "1" : "0";
                str += chk_J_Mz.Checked ? "1" : "0";
                return str;
            }
            set
            {
                chk_J_Fx.Checked = value.Get_Int(1, 1) == 1;
                chk_J_Fy.Checked = value.Get_Int(2, 2) == 1;
                chk_J_Fz.Checked = value.Get_Int(3, 3) == 1;
                chk_J_Mx.Checked = value.Get_Int(4, 4) == 1;
                chk_J_My.Checked = value.Get_Int(5, 5) == 1;
                chk_J_Mz.Checked = value.Get_Int(6, 6) == 1;
            }
        }

        public frm_Beam_Elements(DataGridView dgv, bool isEdit)
        {
            InitializeComponent();
            DGV = dgv;
            IsEdit = isEdit;
            IsInsert = false;
        }

        private void btn_add_mem_Click(object sender, EventArgs e)
        {

            int r = -1;

            if (DGV.SelectedCells.Count > 0)
                r = DGV.SelectedCells[0].RowIndex;

            if (IsEdit)
            {
                int c = -1;
                if (r != -1)
                {
                    //DGV.Rows.Add(Member_No,
                    //NODE_I, NODE_J, Material_No);

                    //txt_mbr_No.Text = (Member_No + Member_No_Incr).ToString();
                    //txt_node_I.Text = (NODE_I + Start_Joint_Incr).ToString();
                    //txt_node_J.Text = (NODE_J + End_Joint_Incr).ToString();
                    c = 0;

                    DGV[c++, r].Value = Member_No;
                    DGV[c++, r].Value = NODE_I;
                    DGV[c++, r].Value = NODE_J;
                    DGV[c++, r].Value = NODE_K;
                    DGV[c++, r].Value = Material_No;
                    DGV[c++, r].Value = Section_No;
                    DGV[c++, r].Value = txt_end_frc_no.Text;
                    DGV[c++, r].Value = NODE_I_Release;
                    DGV[c++, r].Value = NODE_J_Release;

                    DrawBeams();
                    this.Close();
                }
            }
            else
            {
                if (IsInsert)
                {
                    DGV.Rows.Insert(r, Member_No,
                               NODE_I, NODE_J, NODE_K, Material_No, Section_No, 
                               txt_end_frc_no.Text, NODE_I_Release, NODE_J_Release);

                }
                else
                {
                    DGV.Rows.Add(Member_No,
                        NODE_I, NODE_J, NODE_K, Material_No, Section_No, txt_end_frc_no.Text, NODE_I_Release, NODE_J_Release);
                }
                txt_mbr_No.Text = (Member_No + Member_No_Incr).ToString();
                txt_node_I.Text = (NODE_I + Start_Joint_Incr).ToString();
                txt_node_J.Text = (NODE_J + End_Joint_Incr).ToString();
                DrawBeams();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Beam_Elements_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                btn_add_mem.Text = "Change";
                int c = 0;
                int r = -1;

                if (DGV.SelectedCells.Count > 0)
                    r = DGV.SelectedCells[0].RowIndex;

                if (r != -1)
                {
                    grb_incr.Enabled = false;
                    txt_mbr_No.Text = DGV[c++, r].Value.ToString();
                    txt_node_I.Text = DGV[c++, r].Value.ToString();
                    txt_node_J.Text = DGV[c++, r].Value.ToString();
                    txt_node_K.Text = DGV[c++, r].Value.ToString();
                    txt_mat_prop_no.Text = DGV[c++, r].Value.ToString();
                    txt_sec_prop_no.Text = DGV[c++, r].Value.ToString();
                    txt_end_frc_no.Text = DGV[c++, r].Value.ToString();
                    NODE_I_Release = DGV[c++, r].Value.ToString();
                    NODE_J_Release = DGV[c++, r].Value.ToString();
                }
            }
            else
            {

                if (IsInsert)
                {
                    btn_add_mem.Text = "INSERT";
                    int c = 0;
                    int r = -1;

                    if (DGV.SelectedCells.Count > 0)
                        r = DGV.SelectedCells[0].RowIndex;

                    if (r != -1)
                    {
                        grb_incr.Enabled = false;
                        txt_mbr_No.Text = DGV[c++, r].Value.ToString();
                        txt_node_I.Text = DGV[c++, r].Value.ToString();
                        txt_node_J.Text = DGV[c++, r].Value.ToString();
                        txt_node_K.Text = DGV[c++, r].Value.ToString();
                        txt_mat_prop_no.Text = DGV[c++, r].Value.ToString();
                        txt_sec_prop_no.Text = DGV[c++, r].Value.ToString();
                        txt_end_frc_no.Text = DGV[c++, r].Value.ToString();
                        NODE_I_Release = DGV[c++, r].Value.ToString();
                        NODE_J_Release = DGV[c++, r].Value.ToString();
                    }
                }
                else
                {
                    txt_mbr_No.Text = (DGV.RowCount + 1) + "";
                    if (DGV.RowCount == 0)
                    {
                        txt_incr_no.Text = "1";
                        txt_incr_start_jnt.Text = "1";
                        txt_incr_end_jnt.Text = "1";
                    }
                    if (Selected_Nodes != null)
                    {
                        if (Selected_Nodes.Count > 1)
                        {
                            int c = 0;

                            txt_node_I.Text = Selected_Nodes[c++].ToString();
                            txt_node_J.Text = Selected_Nodes[c++].ToString();
                        }
                    }
                }

            }
        }
    }
}
