using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne.BeamDesign
{
    public partial class frmGridTable : Form
    {
        public frmGridTable()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            //Set_Bending_Moment();
            //Set_Shear_Force();

            //dgv_bending_moment.Rows[2].ReadOnly = true;
            //dgv_shear_force.Rows[2].ReadOnly = true;

            //frmReport_Viewer f_rep = new frmReport_Viewer();



            //f_rep.crv_report_viewer.ReportSource = new CR_Beam_01();

            //CR_Beam_01 cr = new CR_Beam_01();
            //cr.ParameterFields[0].PromptText = "Hare Krishna";
            //f_rep.Show();
        }

        void Set_Bending_Moment()
        {
            double at_deff_1, span_L_4_1, span_L_2_1;
            double at_deff_2, span_L_4_2, span_L_2_2;
            double at_deff_3, span_L_4_3, span_L_2_3;
            double at_deff_4, span_L_4_4, span_L_2_4;

            at_deff_1 = 34.62;
            at_deff_2 = 39.32;
            at_deff_3 = 22.70;
            at_deff_4 = 30.60;

            span_L_4_1 = 74.41;
            span_L_4_2 = 84.58;
            span_L_4_3 = 52.55;
            span_L_4_4 = 70.71;

            span_L_2_1 = 90.29;
            span_L_2_2 = 111.05;
            span_L_2_3 = 71.71;
            span_L_2_4 = 93.74;

            dgv_bending_moment.Rows.Add("DESIGN LIVE LOAD BENDING MOMENT", at_deff_1, span_L_4_1, span_L_2_1, at_deff_2, span_L_4_2, span_L_2_2);
            dgv_bending_moment.Rows.Add("DEAD LOAD + SIDL", at_deff_3, span_L_4_3, span_L_2_3, at_deff_4, span_L_4_4, span_L_2_4);


            dgv_bending_moment.Rows.Add("DESIGN BENDING MOMENT ( t-m )",
                (at_deff_1 + at_deff_3),
                (span_L_4_1 + span_L_4_3),
                (span_L_2_1 + span_L_2_3),
                (at_deff_2 + at_deff_4),
                (span_L_4_2 + span_L_4_4),
                (span_L_2_2 + span_L_2_4));


        }
        void Set_Shear_Force()
        {
            double at_deff_1, span_L_4_1, span_L_2_1;
            double at_deff_2, span_L_4_2, span_L_2_2;
            double at_deff_3, span_L_4_3, span_L_2_3;
            double at_deff_4, span_L_4_4, span_L_2_4;

            at_deff_1 = 33.91;
            at_deff_2 = 34.04;
            at_deff_3 = 20.74;
            at_deff_4 = 28.15;

            span_L_4_1 = 26.33;
            span_L_4_2 = 29.90;
            span_L_4_3 = 16.09;
            span_L_4_4 = 21.15;

            span_L_2_1 = 19.13;
            span_L_2_2 = 17.92;
            span_L_2_3 = 5.78;
            span_L_2_4 = 6.60;

            dgv_shear_force.Rows.Add("DESIGN LIVE LOAD BENDING MOMENT", at_deff_1, span_L_4_1, span_L_2_1, at_deff_2, span_L_4_2, span_L_2_2);
            dgv_shear_force.Rows.Add("DEAD LOAD + SIDL", at_deff_3, span_L_4_3, span_L_2_3, at_deff_4, span_L_4_4, span_L_2_4);


            dgv_shear_force.Rows.Add("DESIGN BENDING MOMENT ( t-m )",
                (at_deff_1 + at_deff_3), 
                (span_L_4_1 + span_L_4_3), 
                (span_L_2_1 + span_L_2_3), 
                (at_deff_2 + at_deff_4), 
                (span_L_4_2 + span_L_4_4), 
                (span_L_2_2 + span_L_2_4));


            double g1, g2, g3, g4;

            g1 = 36.1;
            g2 = 18.36;
            g3 = 18.36;
            g4 = 36.1;

            dataGridView1.Rows.Add("",
                (g1),
                (g2),
                (g3),
                (g4));

        }

        private void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            double d1, d2, d3;

            try
            {

                d1 = double.Parse(dgv[e.ColumnIndex, 0].Value.ToString());
                d2 = double.Parse(dgv[e.ColumnIndex, 1].Value.ToString());


                dgv[e.ColumnIndex, 2].Value = (d1 + d2).ToString();

                dgv[e.ColumnIndex, 0].Value = d1.ToString("0.00");
                dgv[e.ColumnIndex, 1].Value = d2.ToString("0.00");
            }
            catch (Exception ex) { }
        }

        private void frmGridTable_Load(object sender, EventArgs e)
        {
            Set_Bending_Moment();
            Set_Shear_Force();

            dgv_bending_moment.Rows[2].ReadOnly = true;
            dgv_shear_force.Rows[2].ReadOnly = true;
        }
    }
}
