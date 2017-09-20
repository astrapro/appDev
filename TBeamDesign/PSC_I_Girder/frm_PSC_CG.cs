using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BridgeAnalysisDesign.PSC_I_Girder
{
    public partial class frm_PSC_CG : Form
    {
        public PSC_Force_Data Cable_A;
        public PSC_Force_Data Cable_B; 
        public PSC_Force_Data Cable_C; 
        public PSC_Force_Data Cable_D;
        public PSC_Force_Data Cable_E;
        public PSC_Force_Data Cable_CG;

       

        public double L { get; set; }
        public double L1 { get { return 0.0; } }
        public double L2 { get; set; }
        //public double L2 { get { return deff; } }
        public double L3 { get { return L / 5.3; } }
        public double L4 { get { return L / 3.65; } }
        public double L5 { get { return L / 3.05; } }
        public double L6 { get { return L / 2.61; } }
        public double L7 { get { return L / 2.28; } }
        public double L8 { get { return L / 2.03; } }
        public double L9 { get { return L / 2.00; } }



        public frm_PSC_CG(double Length, double deff, PSC_Force_Data Cable_a, PSC_Force_Data Cable_b,
            PSC_Force_Data Cable_c,PSC_Force_Data Cable_d,PSC_Force_Data Cable_e)
        {
            InitializeComponent();
            L = Length;
            L2 = deff;
            Cable_A = Cable_a;
            Cable_B = Cable_b;
            Cable_C = Cable_c;
            Cable_D = Cable_d;
            Cable_E = Cable_e;

        }

        private void frm_PSC_CG_Load(object sender, EventArgs e)
        {
            dgv.Rows.Clear();

            dgv.Rows.Add("Distance (m)",
                L1.ToString("f3"),
                L2.ToString("f3"),
                L3.ToString("f3"),
                L4.ToString("f3"),
                L5.ToString("f3"),
                L6.ToString("f3"),
                L7.ToString("f3"),
                L8.ToString("f3"),
                L9.ToString("f3"));

            dgv.Rows.Add("Cable 'e' (mm)",
                Cable_E.F1.ToString("f3"),
                Cable_E.F2.ToString("f3"),
                Cable_E.F3.ToString("f3"),
                Cable_E.F4.ToString("f3"),
                Cable_E.F5.ToString("f3"),
                Cable_E.F6.ToString("f3"),
                Cable_E.F7.ToString("f3"),
                Cable_E.F8.ToString("f3"),
                Cable_E.F9.ToString("f3"));
            dgv.Rows.Add("Cable 'd' (mm)",
                Cable_D.F1.ToString("f3"),
                Cable_D.F2.ToString("f3"),
                Cable_D.F3.ToString("f3"),
                Cable_D.F4.ToString("f3"),
                Cable_D.F5.ToString("f3"),
                Cable_D.F6.ToString("f3"),
                Cable_D.F7.ToString("f3"),
                Cable_D.F8.ToString("f3"),
                Cable_D.F9.ToString("f3"));
            dgv.Rows.Add("Cable 'c' (mm)",
                Cable_C.F1.ToString("f3"),
                Cable_C.F2.ToString("f3"),
                Cable_C.F3.ToString("f3"),
                Cable_C.F4.ToString("f3"),
                Cable_C.F5.ToString("f3"),
                Cable_C.F6.ToString("f3"),
                Cable_C.F7.ToString("f3"),
                Cable_C.F8.ToString("f3"),
                Cable_C.F9.ToString("f3"));


            dgv.Rows.Add("Cable 'b' (mm)",
                Cable_B.F1.ToString("f3"),
                Cable_B.F2.ToString("f3"),
                Cable_B.F3.ToString("f3"),
                Cable_B.F4.ToString("f3"),
                Cable_B.F5.ToString("f3"),
                Cable_B.F6.ToString("f3"),
                Cable_B.F7.ToString("f3"),
                Cable_B.F8.ToString("f3"),
                Cable_B.F9.ToString("f3"));
            dgv.Rows.Add("Cable 'a' (mm)",
                Cable_A.F1.ToString("f3"),
                Cable_A.F2.ToString("f3"),
                Cable_A.F3.ToString("f3"),
                Cable_A.F4.ToString("f3"),
                Cable_A.F5.ToString("f3"),
                Cable_A.F6.ToString("f3"),
                Cable_A.F7.ToString("f3"),
                Cable_A.F8.ToString("f3"),
                Cable_A.F9.ToString("f3"));

            //Average 
            Cable_CG = ((Cable_A + Cable_B + Cable_C + Cable_D + Cable_E) / 5.0);


            dgv.Rows.Add("CG",
                Cable_CG.F1.ToString("f3"),
                Cable_CG.F2.ToString("f3"),
                Cable_CG.F3.ToString("f3"),
                Cable_CG.F4.ToString("f3"),
                Cable_CG.F5.ToString("f3"),
                Cable_CG.F6.ToString("f3"),
                Cable_CG.F7.ToString("f3"),
                Cable_CG.F8.ToString("f3"),
                Cable_CG.F9.ToString("f3"));


            dgv.Rows[0].ReadOnly = true;
            dgv.Rows[0].Frozen = true;
            dgv.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;

            dgv.Rows[6].ReadOnly = true;
            dgv.Rows[6].Frozen = true;
            dgv.Rows[6].DefaultCellStyle.ForeColor = Color.Blue;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

            for (int c = 1; c < dgv.ColumnCount; c++)
            {
                Cable_E[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 1].Value.ToString(), 0.0);
                Cable_D[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 2].Value.ToString(), 0.0);
                Cable_C[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 3].Value.ToString(), 0.0);
                Cable_B[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 4].Value.ToString(), 0.0);
                Cable_A[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 5].Value.ToString(), 0.0);
            }
            Cable_CG = ((Cable_A + Cable_B + Cable_C + Cable_D + Cable_E) / 5.0);

            this.Close();
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int c = 1; c < dgv.ColumnCount; c++)
                {
                    Cable_E[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 1].Value.ToString(), 0.0);
                    Cable_D[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 2].Value.ToString(), 0.0);
                    Cable_C[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 3].Value.ToString(), 0.0);
                    Cable_B[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 4].Value.ToString(), 0.0);
                    Cable_A[c - 1] = AstraInterface.DataStructure.MyList.StringToDouble(dgv[c, 5].Value.ToString(), 0.0);
                }
                Cable_CG = ((Cable_A + Cable_B + Cable_C + Cable_D + Cable_E) / 5.0);
                for (int c = 1; c < dgv.ColumnCount; c++)
                {
                    dgv[c, 1].Value = Cable_E[c - 1].ToString("f3");
                    dgv[c, 2].Value = Cable_D[c - 1].ToString("f3");
                    dgv[c, 3].Value = Cable_C[c - 1].ToString("f3");
                    dgv[c, 4].Value = Cable_B[c - 1].ToString("f3");
                    dgv[c, 5].Value = Cable_A[c - 1].ToString("f3");
                    dgv[c, 6].Value = Cable_CG[c - 1].ToString("f3");
                }
            }
            catch (Exception ex) { }
        }
    }
}
