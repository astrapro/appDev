using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using BridgeAnalysisDesign.PSC_I_Girder;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_Box_EachCable : Form
    {
        public Average_Forces_after_friction_and_Slip CableData { get; set; }
        public List<string> Cable_Calculations { get; set; }
        public double Effective_Depth;
        public double Length;


        public int Total_Cables { get; set; }
        public int Cable_No { get; set; }
        public frm_Box_EachCable(double lenth, double deff_dist, int total_cables, int cable_no)
        {
            InitializeComponent();
            CableData = new Average_Forces_after_friction_and_Slip();
            Cable_Calculations = new List<string>();

            Length = lenth;
            Effective_Depth = deff_dist;

            Total_Cables = total_cables;
            Cable_No = cable_no;

            lbl_cable.Text = "CABLE   " + cable_no + "  of   " + total_cables; 
        }
        private void frm_Box_Cable_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("CABLE 1"));
            if (Cable_No == 1)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;1.00;4.00;22.71;22.71;24.21"));
                list.Add(string.Format("ΣL;0.00;1.00;4.00;22.71;22.71;24.21"));
                list.Add(string.Format("θ;0.00;0.00;0.193;0.00;0.00;0.067"));
                txt_cab1_lc.Text = Length.ToString("f3");
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 2"));
            if (Cable_No == 2)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;1.00;4.00;22.71;22.71;24.21"));
                list.Add(string.Format("ΣL;0.00;1.00;4.00;22.71;22.71;24.21"));
                list.Add(string.Format("θ;0.00;0.00;0.193;0.00;0.00;0.067"));
                txt_cab1_lc.Text = Length.ToString("f3");
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 3"));
            if (Cable_No == 3)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;1.02;3.50;5.50;8.50;8.50"));
                list.Add(string.Format("ΣL;0.00;1.02;3.50;5.50;8.50;8.50"));
                list.Add(string.Format("θ;0.00;0.00;0.200;0.132;0.00;0.000"));
                txt_cab1_lc.Text = (Length * (17.0 / 48.41)).ToString("f3");

            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 4"));
            if (Cable_No == 4)
            {
                list.Add(string.Format("Segment Lengths = d =;0.000;1.02;3.50;7.50;13.50;13.50"));
                list.Add(string.Format("ΣL;0.000;1.02;3.50;7.50;13.50;13.50"));
                list.Add(string.Format("θ;0.000;0.000;0.200;0.286;0.000;0.000"));
                txt_cab1_lc.Text = (Length * (27.0/48.41)).ToString("f3");

            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 5"));
            if (Cable_No == 5)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;4.25;5.75;14.21;24.21;24.21"));
                list.Add(string.Format("ΣL;0.00;4.25;5.75;14.21;24.21;24.21"));
                list.Add(string.Format("θ;0.00;0.00;0.084;0.344;0.00;0.00"));
                txt_cab1_lc.Text = Length.ToString("f3");
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 6"));
            if (Cable_No == 6)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;1.00;2.50;8.50;24.21;24.21"));
                list.Add(string.Format("ΣL;0.00;1.00;2.50;8.50;24.21;24.21"));
                list.Add(string.Format("θ;0.00;0.00;0.083;0.253;0.00;0.00"));
                txt_cab1_lc.Text = Length.ToString("f3");
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CABLE 7"));
            if (Cable_No == 7)
            {
                list.Add(string.Format("Segment Lengths = d =;0.00;1.00;2.50;24.21;24.21; 24.21"));
                list.Add(string.Format("ΣL;0.00;1.00;2.50;24.21;24.21; 24.21"));
                list.Add(string.Format("θ;0.00;0.00;0.083;0.00;0.00; 0.00"));
                txt_cab1_lc.Text = Length.ToString("f3");
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Segment Lengths = d =;0.00;1.00;4.00;22.71;22.71;24.21"));
            //list.Add(string.Format("ΣL;0.00;1.00;4.00;22.71;22.71;24.21"));
            //list.Add(string.Format("θ;0.00;0.00;0.193;0.00;0.00;0.067"));
            list.Add(string.Format("Σθ;Σθ1= θ1;Σθ2= Σθ1+θ2;Σθ3= Σθ2+θ3;Σθ4= Σθ3+θ4;Σθ5= Σθ4+θ5;Σθ6= Σθ5+θ6"));
            list.Add(string.Format(";=0.00;=0.00;=0.193;=0.193;=0.193;=0.260"));
            list.Add(string.Format("µ x Σθ;=0.00;=0.00;=0.0328;=0.0328;=0.0328;=0.0442"));
            list.Add(string.Format("k x ΣL;=0.00;=0.002;=0.008;=0.045;=0.045;=0.048"));
            list.Add(string.Format("A= µxΣθ+kxΣL;=0.00;=0.002;=0.0408;=0.0778;=0.0778;=0.0922"));
            list.Add(string.Format("Z=1 - A;=1.000;=0.998;=0.960;=0.922;=0.922;=0.910"));
            list.Add(string.Format("P;378.9;378.9;378.9;378.9;378.9;378.9"));
            list.Add(string.Format("PZ=P x Z;=378.9;=378.1;=363.7;=349.3;=349.3;=344.8"));
            list.Add(string.Format("P_Slip;=325.976;=326.776;=341.176;=350.4;=350.4;=345.4"));
            list.Add(string.Format("Force after Friction;=378.500;=370.900;=356.500;=349.300;=347.050;=345.0"));
            list.Add(string.Format("Force after Slip;=326.376;=333.976;=345.788;=350.400;=347.900;=345.0"));



            //Chiranjit [2013 06 20]
            if (Cable_No == 1)
                list.Add(string.Format("Height from Sofit;1.675;1.500;1.275;0.875;0.475;0.130"));
            if (Cable_No == 2)
                list.Add(string.Format("Height from Sofit;1.100;0.975;0.775;0.475;0.150;0.130"));
            if (Cable_No == 3)
                list.Add(string.Format("Height from Sofit;0.000;0.000;0.000;0.000;0.175;0.130"));
            if (Cable_No == 4)
                list.Add(string.Format("Height from Sofit;0.000;0.000;0.000;0.290;0.130;0.130"));
            if (Cable_No == 5)
                list.Add(string.Format("Height from Sofit;0.525;0.325;0.130;0.130;0.130;0.130"));
            if (Cable_No == 6)
                list.Add(string.Format("Height from Sofit;0.248;0.130;0.130;0.130;0.130;0.130"));
            if (Cable_No == 7)
                list.Add(string.Format("Height from Sofit;0.248;0.130;0.130;0.130;0.130;0.130"));


            chk_L2.Checked = true;
            chk_L4.Checked = true;
            chk_L8.Checked = true;
            chk_3L8.Checked = true;
            chk_deff.Checked = true;
            chk_support.Checked = true;

            //list.Add(string.Format("ELONGATION in Cable 2 (in mm):  156.0"));

            Set_Cable_Data(dgv_cab1, list);


            //txt_cab1_lc.Text = Length.ToString("f3");




            Set_L2_Values();


            //dgv_cab1[2, 0].Value = Effective_Depth.ToString("f3");
            //dgv_cab1[2, 1].Value = Effective_Depth.ToString("f3");

            Format_Grid(dgv_cab1);


        }

        public void Set_Cable_Data(DataGridView dgv, List<string> list)
        {
            dgv.Rows.Clear();

            MyList mlist = null;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ';');
                dgv.Rows.Add(mlist.StringList.ToArray());

                if (i > 2 && i != 9 && i != 14 || i == 1)
                {
                    dgv.Rows[i].ReadOnly = true;
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else
                {
                    //dgv.Rows[i].ReadOnly = true;
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            Format_Grid(dgv);
        }
        private void Format_Grid(DataGridView dgv)
        {
            if (dgv.Name == dgv_cab1.Name)
            {
                CableData.Cable_Length = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
                CableData.Strand_Area = MyList.StringToDouble(txt_cab1_a.Text, 0.0);
                CableData.Elasticity_Modulus = MyList.StringToDouble(txt_cab1_Es.Text, 0.0);
                CableData.Strands_Number = MyList.StringToInt(txt_cab1_Ns.Text, 0);
                CableData.Jacking_End_Slip = MyList.StringToDouble(txt_cab1_Sl.Text, 0.0);
                CableData.Friction_Coefficient = MyList.StringToDouble(txt_cab1_mu.Text, 0.0);
                CableData.Wobble_Coefficient = MyList.StringToDouble(txt_cab1_k.Text, 0.0);
            }
            CableData.Read_Data_From_Grid(dgv);


            try
            {
                chk_support.Checked = CableData.Height_from_sofit.F1 != 0.0;
                chk_deff.Checked = CableData.Height_from_sofit.F2 != 0.0;
                chk_L8.Checked = CableData.Height_from_sofit.F3 != 0.0;
                chk_L4.Checked = CableData.Height_from_sofit.F4 != 0.0;
                chk_3L8.Checked = CableData.Height_from_sofit.F5 != 0.0;
                chk_L2.Checked = CableData.Height_from_sofit.F6 != 0.0;
            }
            catch (Exception ex) { }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            //Average_Forces_after_friction_and_Slip avf = new Average_Forces_after_friction_and_Slip();


            CableData.Cable_Length = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
            CableData.Strand_Area = MyList.StringToDouble(txt_cab1_a.Text, 0.0);
            CableData.Elasticity_Modulus = MyList.StringToDouble(txt_cab1_Es.Text, 0.0);
            CableData.Strands_Number = MyList.StringToInt(txt_cab1_Ns.Text, 0);
            CableData.Jacking_End_Slip = MyList.StringToDouble(txt_cab1_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
            CableData.Friction_Coefficient = MyList.StringToDouble(txt_cab1_mu.Text, 0.0);
            CableData.Wobble_Coefficient = MyList.StringToDouble(txt_cab1_k.Text, 0.0);


            #region Chiranjit [2013 06 20]
            CableData.PresenseOfCable.F1 = chk_support.Checked ? (Cable_No == 7) ? 1.052 :  2.0 : 0.0;
            CableData.PresenseOfCable.F2 = chk_deff.Checked ? (Cable_No == 7) ? 1.052 :  2.0 :0.0;
            CableData.PresenseOfCable.F3 = chk_L8.Checked ? (Cable_No == 7) ? 1.052 : 2.0 : 0.0;
            CableData.PresenseOfCable.F4 = chk_L4.Checked ? (Cable_No == 7) ? 1.052 :  2.0 :0.0;
            CableData.PresenseOfCable.F5 = chk_3L8.Checked ? (Cable_No == 7) ? 1.052 :  2.0 :0.0;
            CableData.PresenseOfCable.F6 = chk_L2.Checked ? (Cable_No == 7) ? 1.052 :  2.0 :0.0;
            #endregion Chiranjit [2013 06 20]



            CableData.Read_Data_From_Grid(dgv_cab1);


            Cable_Calculations.Add(string.Format("Cable {0}", Cable_No));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", CableData.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", CableData.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", CableData.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0}", CableData.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", CableData.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", CableData.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", CableData.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add(string.Format("TABLE 9.{0}: SUMMARY of Average Forces after friction and Slip in Cable {0}:", Cable_No));
            Cable_Calculations.Add(string.Format("-----------------------------------------------------------------------------"));
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(CableData.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add(string.Format("Calculation Details for TABLE {0}, for Cable {1}:", (8 + Cable_No), Cable_No));
            Cable_Calculations.Add("---------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(CableData.list);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            try
            {
                Format_Grid(dgv);
                Set_L2_Values();
            }
            catch (Exception ex) { }
        }

        private void txt_lc_TextChanged(object sender, EventArgs e)
        {
            //Set_L2_Values();
        }

        private void Set_L2_Values()
        {
            double lc1 = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);

            //dgv_cab1[6, 0].Value = (Length / 8.0).ToString("f3");
            //dgv_cab1[6, 1].Value = (Length / 8.0).ToString("f3");

            //dgv_cab1[5, 0].Value = (Length / 8.0).ToString("f3");
            //dgv_cab1[5, 1].Value = (Length / 8.0).ToString("f3");

            //dgv_cab1[4, 0].Value = (Length / 8.0).ToString("f3");
            //dgv_cab1[4, 1].Value = (Length / 8.0).ToString("f3");

            //dgv_cab1[3, 0].Value = (Length / 8.0).ToString("f3");
            //dgv_cab1[3, 1].Value = (Length / 8.0).ToString("f3");

            Format_Grid(dgv_cab1);
 
        }

    }
}
