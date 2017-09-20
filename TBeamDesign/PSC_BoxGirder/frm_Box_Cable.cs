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
    public partial class frm_Box_Cable : Form
    {
        public PSC_BOX_Cable_Data CABLES { get; set; }

        public List<string> Cable_Calculations { get; set; }
        public double Effective_Depth;
        public double Length;
        public frm_Box_Cable(double lenth, double deff_dist)
        {
            InitializeComponent();
            Cable_Calculations = new List<string>();

            Length = lenth;
            Effective_Depth = deff_dist;
        }

        public void Set_Cable_Data(DataGridView dgv, List<string> list)
        {
            dgv.Rows.Clear();

            MyList mlist = null;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ';');
                dgv.Rows.Add(mlist.StringList.ToArray());

                if (i > 2 && i != 9)
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

        private void frm_Box_Cable_Load(object sender, EventArgs e)
        {

            CABLES = new PSC_BOX_Cable_Data();

            List<string> list = new List<string>();

            list.Add(string.Format("Distance = d =;0.00;1.00;4.00;22.71;22.71;24.21"));
            list.Add(string.Format("ΣL;0.00;1.00;4.00;22.71;22.71;24.21"));
            list.Add(string.Format("θ;0.00;0.00;0.193;0.00;0.00;0.067"));
            list.Add(string.Format("Σθ;Σθ1= θ1;Σθ2= Σθ1+θ2;Σθ3= Σθ2+θ3;Σθ4= Σθ3+θ4;Σθ5= Σθ4+θ5;Σθ6= Σθ5+θ6"));
            list.Add(string.Format(";=0.00;=0.00;=0.193;=0.193;=0.193;=0.260"));
            list.Add(string.Format("µ x Σθ;=0.00;=0.00;=0.0328;=0.0328;=0.0328;=0.0442"));
            list.Add(string.Format("k x ΣL;=0.00;=0.002;=0.008;=0.045;=0.045;=0.048"));
            list.Add(string.Format("A= µxΣθ+kxΣL;=0.00;=0.002;=0.0408;=0.0778;=0.0778;=0.0922"));
            list.Add(string.Format("Z=1 - A;=1.000;=0.998;=0.960;=0.922;=0.922;=0.910"));
            list.Add(string.Format("P;378.9;378.9;378.9;378.9;378.9;378.9"));
            list.Add(string.Format("PZ=P x Z;=378.9;=378.1;=363.7;=349.3;=349.3;=344.8"));
            list.Add(string.Format("P_Slip;=325.976;=326.776;=341.176;=350.4;=350.4;=345.4"));
            list.Add(string.Format("Force after Friction;=378.500;=370.900;=356.500;=349.300;=347.050;=0.0"));
            list.Add(string.Format("Force after Slip;=326.376;=333.976;=345.788;=350.400;=347.900;=0.0"));

            //list.Add(string.Format("ELONGATION in Cable 2 (in mm):  156.0"));

            Set_Cable_Data(dgv_cab1, list);
            Set_Cable_Data(dgv_cab2, list);
            Set_Cable_Data(dgv_cab3, list);
            Set_Cable_Data(dgv_cab4, list);
            Set_Cable_Data(dgv_cab5, list);
            Set_Cable_Data(dgv_cab6, list);
            Set_Cable_Data(dgv_cab7, list);



            txt_cab1_lc.Text = Length.ToString("f3");
            txt_cab2_lc.Text = Length.ToString("f3");
            txt_cab3_lc.Text = Length.ToString("f3");
            txt_cab4_lc.Text = Length.ToString("f3");
            txt_cab5_lc.Text = Length.ToString("f3");
            txt_cab6_lc.Text = Length.ToString("f3");
            txt_cab7_lc.Text = Length.ToString("f3");



            Set_L2_Values();


            dgv_cab1[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab1[2, 1].Value = Effective_Depth.ToString("f3");

            dgv_cab2[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab2[2, 1].Value = Effective_Depth.ToString("f3");


            dgv_cab3[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab3[2, 1].Value = Effective_Depth.ToString("f3");



            dgv_cab4[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab4[2, 1].Value = Effective_Depth.ToString("f3");


            dgv_cab5[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab5[2, 1].Value = Effective_Depth.ToString("f3");


            dgv_cab6[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab6[2, 1].Value = Effective_Depth.ToString("f3");


            dgv_cab7[2, 0].Value = Effective_Depth.ToString("f3");
            dgv_cab7[2, 1].Value = Effective_Depth.ToString("f3");




        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Average_Forces_after_friction_and_Slip avf = new Average_Forces_after_friction_and_Slip();


            avf.Cable_Length = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab1_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab1_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab1_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab1_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab1_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab1_k.Text, 0.0);

            avf.Read_Data_From_Grid(dgv_cab1);

            
            Cable_Calculations.Add(string.Format("Cable 1"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm",avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa",avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}",avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.",avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 9: SUMMARY of Average Forces after friction and Slip in Cable 1:");
            Cable_Calculations.Add("----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 9, for Cable 1:");
            Cable_Calculations.Add("--------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);

            CABLES.CABLE_1 = avf;


            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab2_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab2_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab2_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab2_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab2_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab2_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab2_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab2_k.Text, 0.0);


            avf.Read_Data_From_Grid(dgv_cab2);

            Cable_Calculations.Add(string.Format("Cable 2"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 10: SUMMARY of Average Forces after friction and Slip in Cable 2:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 10, for Cable 2:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);

            CABLES.CABLE_2 = avf;


            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab3_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab3_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab3_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab3_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab3_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab3_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab3_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab3_k.Text, 0.0);


            avf.Read_Data_From_Grid(dgv_cab3);


            Cable_Calculations.Add(string.Format("Cable 3"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 11: SUMMARY of Average Forces after friction and Slip in Cable 3:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 11, for Cable 3:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);

            CABLES.CABLE_3 = avf;



            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab4_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab4_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab4_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab4_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab4_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab4_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab4_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab4_k.Text, 0.0);


            avf.Read_Data_From_Grid(dgv_cab4);


            Cable_Calculations.Add(string.Format("Cable 4"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 12: SUMMARY of Average Forces after friction and Slip in Cable 4:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 12, for Cable 4:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);
            CABLES.CABLE_4 = avf;




            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab5_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab5_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab5_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab5_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab5_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab5_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab5_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab5_k.Text, 0.0);


            avf.Read_Data_From_Grid(dgv_cab5);


            Cable_Calculations.Add(string.Format("Cable 5"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 13: SUMMARY of Average Forces after friction and Slip in Cable 5:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 13, for Cable 5:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);
            CABLES.CABLE_5 = avf;



            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab6_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab6_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab6_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab6_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab6_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab6_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab6_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab6_k.Text, 0.0);
            
            avf.Read_Data_From_Grid(dgv_cab6);





            Cable_Calculations.Add(string.Format("Cable 6"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 14: SUMMARY of Average Forces after friction and Slip in Cable 6:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 14, for Cable 6:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);
            CABLES.CABLE_6 = avf;


            avf = new Average_Forces_after_friction_and_Slip();
            avf.Cable_Length = MyList.StringToDouble(txt_cab7_lc.Text, 0.0);
            avf.Strand_Area = MyList.StringToDouble(txt_cab7_a.Text, 0.0);
            avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab7_Es.Text, 0.0);
            avf.Strands_Number = MyList.StringToInt(txt_cab7_Ns.Text, 0);
            avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab7_Sl.Text, 0.0);
            //avf.Sheathing_Type = MyList.StringToDouble(txt_cab7_lc.Text, 0.0);
            avf.Friction_Coefficient = MyList.StringToDouble(txt_cab7_mu.Text, 0.0);
            avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab7_k.Text, 0.0);

            avf.Read_Data_From_Grid(dgv_cab7);




            Cable_Calculations.Add(string.Format("Cable 7"));
            Cable_Calculations.Add(string.Format("-------"));
            Cable_Calculations.Add(string.Format(""));
            Cable_Calculations.Add(string.Format("Total Length of Cable = {0:f3} m.", avf.Cable_Length));
            Cable_Calculations.Add(string.Format("Area of the Strand = a = {0:f3} Sq.mm", avf.Strand_Area));
            Cable_Calculations.Add(string.Format("Modulus of Elasticity = Es = {0:f3} Gpa", avf.Elasticity_Modulus));
            Cable_Calculations.Add(string.Format("Number of Strands = Ns = {0:f3}", avf.Strands_Number));
            Cable_Calculations.Add(string.Format("Slip at Jacking End = Sl = {0:f3} mm.", avf.Jacking_End_Slip));
            Cable_Calculations.Add(string.Format("Type of Sheathing = HDPE"));
            Cable_Calculations.Add(string.Format("Coefficient of Friction (µ) = {0:f3}", avf.Friction_Coefficient));
            Cable_Calculations.Add(string.Format("Wobble Coefficient (k) = {0:f3}", avf.Wobble_Coefficient));
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("TABLE 15: SUMMARY of Average Forces after friction and Slip in Cable 7:");
            Cable_Calculations.Add("-----------------------------------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.Get_Table_Data());
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("Calculation Details for TABLE 15, for Cable 7:");
            Cable_Calculations.Add("----------------------------------------------");
            Cable_Calculations.Add("");
            Cable_Calculations.Add("");
            Cable_Calculations.AddRange(avf.list);


            CABLES.CABLE_7 = avf;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgv_cab1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;
            //Format_Grid(dgv);
        }

        private void Format_Grid(DataGridView dgv)
        {

            Average_Forces_after_friction_and_Slip avf = new Average_Forces_after_friction_and_Slip();


            if (dgv.Name == dgv_cab1.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab1_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab1_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab1_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab1_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab1_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab1_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab2.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab2_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab2_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab2_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab2_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab2_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab2_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab2_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab2_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab3.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab3_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab3_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab3_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab3_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab3_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab3_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab3_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab3_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab4.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab4_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab4_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab4_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab4_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab4_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab4_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab4_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab4_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab5.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab5_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab5_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab5_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab5_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab5_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab5_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab5_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab5_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab6.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab6_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab6_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab6_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab6_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab6_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab6_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab6_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab6_k.Text, 0.0);
            }
            else if (dgv.Name == dgv_cab7.Name)
            {
                avf.Cable_Length = MyList.StringToDouble(txt_cab7_lc.Text, 0.0);
                avf.Strand_Area = MyList.StringToDouble(txt_cab7_a.Text, 0.0);
                avf.Elasticity_Modulus = MyList.StringToDouble(txt_cab7_Es.Text, 0.0);
                avf.Strands_Number = MyList.StringToInt(txt_cab7_Ns.Text, 0);
                avf.Jacking_End_Slip = MyList.StringToDouble(txt_cab7_Sl.Text, 0.0);
                //avf.Sheathing_Type = MyList.StringToDouble(txt_cab7_lc.Text, 0.0);
                avf.Friction_Coefficient = MyList.StringToDouble(txt_cab7_mu.Text, 0.0);
                avf.Wobble_Coefficient = MyList.StringToDouble(txt_cab7_k.Text, 0.0);
            }
            avf.Read_Data_From_Grid(dgv);

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
            Set_L2_Values();
        }

        private void Set_L2_Values()
        {

            double lc1 = MyList.StringToDouble(txt_cab1_lc.Text, 0.0);
            double lc2 = MyList.StringToDouble(txt_cab2_lc.Text, 0.0);
            double lc3 = MyList.StringToDouble(txt_cab3_lc.Text, 0.0);
            double lc4 = MyList.StringToDouble(txt_cab4_lc.Text, 0.0);
            double lc5 = MyList.StringToDouble(txt_cab5_lc.Text, 0.0);
            double lc6 = MyList.StringToDouble(txt_cab6_lc.Text, 0.0);
            double lc7 = MyList.StringToDouble(txt_cab7_lc.Text, 0.0);


            dgv_cab1[6, 0].Value = (lc1 / 2.0).ToString("f3");
            dgv_cab1[6, 1].Value = (lc1 / 2.0).ToString("f3");

            dgv_cab1[5, 0].Value = (3 * lc1 / 8.0).ToString("f3");
            dgv_cab1[5, 1].Value = (3 * lc1 / 8.0).ToString("f3");

            dgv_cab1[4, 0].Value = (lc1 / 4.0).ToString("f3");
            dgv_cab1[4, 1].Value = (lc1 / 4.0).ToString("f3");

            dgv_cab1[3, 0].Value = (lc1 / 8.0).ToString("f3");
            dgv_cab1[3, 1].Value = (lc1 / 8.0).ToString("f3");

            Format_Grid(dgv_cab1);

            dgv_cab2[6, 0].Value = (lc2 / 2.0).ToString("f3");
            dgv_cab2[6, 1].Value = (lc2 / 2.0).ToString("f3");

            dgv_cab2[5, 0].Value = (3 * lc2 / 8.0).ToString("f3");
            dgv_cab2[5, 1].Value = (3 * lc2 / 8.0).ToString("f3");

            dgv_cab2[4, 0].Value = (lc2 / 4.0).ToString("f3");
            dgv_cab2[4, 1].Value = (lc2 / 4.0).ToString("f3");

            dgv_cab2[3, 0].Value = (lc2 / 8.0).ToString("f3");
            dgv_cab2[3, 1].Value = (lc2 / 8.0).ToString("f3");

            Format_Grid(dgv_cab2);

            dgv_cab3[6, 0].Value = (lc3 / 2.0).ToString("f3");
            dgv_cab3[6, 1].Value = (lc3 / 2.0).ToString("f3");

            dgv_cab3[5, 0].Value = (3 * lc3 / 8.0).ToString("f3");
            dgv_cab3[5, 1].Value = (3 * lc3 / 8.0).ToString("f3");

            dgv_cab3[4, 0].Value = (lc3 / 4.0).ToString("f3");
            dgv_cab3[4, 1].Value = (lc3 / 4.0).ToString("f3");

            dgv_cab3[3, 0].Value = (lc3 / 8.0).ToString("f3");
            dgv_cab3[3, 1].Value = (lc3 / 8.0).ToString("f3");

            Format_Grid(dgv_cab3);

            dgv_cab4[6, 0].Value = (lc4 / 2.0).ToString("f3");
            dgv_cab4[6, 1].Value = (lc4 / 2.0).ToString("f3");
            dgv_cab4[5, 0].Value = (3 * lc4 / 8.0).ToString("f3");
            dgv_cab4[5, 1].Value = (3 * lc4 / 8.0).ToString("f3");

            dgv_cab4[4, 0].Value = (lc4 / 4.0).ToString("f3");
            dgv_cab4[4, 1].Value = (lc4 / 4.0).ToString("f3");

            dgv_cab4[3, 0].Value = (lc4 / 8.0).ToString("f3");
            dgv_cab4[3, 1].Value = (lc4 / 8.0).ToString("f3");

            Format_Grid(dgv_cab4);

            dgv_cab5[6, 0].Value = (lc5 / 2.0).ToString("f3");
            dgv_cab5[6, 1].Value = (lc5 / 2.0).ToString("f3");

            dgv_cab5[5, 0].Value = (3 * lc5 / 8.0).ToString("f3");
            dgv_cab5[5, 1].Value = (3 * lc5 / 8.0).ToString("f3");

            dgv_cab5[4, 0].Value = (lc5 / 4.0).ToString("f3");
            dgv_cab5[4, 1].Value = (lc5 / 4.0).ToString("f3");

            dgv_cab5[3, 0].Value = (lc5 / 8.0).ToString("f3");
            dgv_cab5[3, 1].Value = (lc5 / 8.0).ToString("f3");

            Format_Grid(dgv_cab5);

            dgv_cab6[6, 0].Value = (lc6 / 2.0).ToString("f3");
            dgv_cab6[6, 1].Value = (lc6 / 2.0).ToString("f3");

            dgv_cab6[5, 0].Value = (3 * lc6 / 8.0).ToString("f3");
            dgv_cab6[5, 1].Value = (3 * lc6 / 8.0).ToString("f3");

            dgv_cab6[4, 0].Value = (lc6 / 4.0).ToString("f3");
            dgv_cab6[4, 1].Value = (lc6 / 4.0).ToString("f3");

            dgv_cab6[3, 0].Value = (lc6 / 8.0).ToString("f3");
            dgv_cab6[3, 1].Value = (lc6 / 8.0).ToString("f3");

            Format_Grid(dgv_cab6);

            dgv_cab7[6, 0].Value = (lc7 / 2.0).ToString("f3");
            dgv_cab7[6, 1].Value = (lc7 / 2.0).ToString("f3");

            dgv_cab7[5, 0].Value = (3 * lc7 / 8.0).ToString("f3");
            dgv_cab7[5, 1].Value = (3 * lc7 / 8.0).ToString("f3");

            dgv_cab7[4, 0].Value = (lc7 / 4.0).ToString("f3");
            dgv_cab7[4, 1].Value = (lc7 / 4.0).ToString("f3");

            dgv_cab7[3, 0].Value = (lc7 / 8.0).ToString("f3");
            dgv_cab7[3, 1].Value = (lc7 / 8.0).ToString("f3");

            Format_Grid(dgv_cab7);
        }
    }

    public class PSC_BOX_Cable_Data : List<Average_Forces_after_friction_and_Slip>
    {
        public Average_Forces_after_friction_and_Slip CABLE_1 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_2 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_3 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_4 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_5 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_6 { get; set; }
        public Average_Forces_after_friction_and_Slip CABLE_7 { get; set; }

        public PSC_BOX_Cable_Data() : base()
        {
            CABLE_1 = new Average_Forces_after_friction_and_Slip();
            CABLE_2 = new Average_Forces_after_friction_and_Slip();
            CABLE_3 = new Average_Forces_after_friction_and_Slip();
            CABLE_4 = new Average_Forces_after_friction_and_Slip();
            CABLE_5 = new Average_Forces_after_friction_and_Slip();
            CABLE_6 = new Average_Forces_after_friction_and_Slip();
            CABLE_7 = new Average_Forces_after_friction_and_Slip();
        }

    }
    public class CablePresense
    {
        public double At_Support { get; set; }
        public double At_Deff { get; set; }
        public double At_L8 { get; set; }
        public double At_L4 { get; set; }
        public double At_3L8 { get; set; }
        public double At_L2 { get; set; }

        public CablePresense()
        {
            At_Support = 2.0;
            At_Deff = 2.0;
            At_L8 = 2.0;
            At_L4 = 2.0;
            At_3L8 = 2.0;
            At_L2 = 2.0;
        }
    }
    public class Average_Forces_after_friction_and_Slip
    {
        public double lc = 0;
        public double a = 0;
        public double Es = 0;
        public int Ns = 0;
        public double Sl = 0.0;
        public double mu = 0.0;
        public double k = 0.0;



        public double dy1 = 0;
        public double Sdy1 = 0;
        public double dy2 = 0;
        public double Sdy2 = 0;
        public double dy3 = 0;
        public double Sdy3 = 0;

        public double dy4 = 0;
        public double Sdy4 = 0;

        public double dy5 = 0;
        public double Sdy5 = 0;


        public double Calc1 = 0.0;
        public double Calc2 = 0.0;
        public double Calc3 = 0.0;
        public double X = 0.0;


        public double Lx = 0.0;
        public double Px = 0.0;
        public PSC_Force_Data F_friction = new PSC_Force_Data(6);
        public PSC_Force_Data F_Slip = new PSC_Force_Data(6);
        public double Elongation = 0.0;

        public List<string> list { get; set; }


        public double Cable_Length
        {
            get
            {
                return lc;
            }
            set
            {
                lc = value;
            }
        }
        public double Elasticity_Modulus
        {
            get
            {
                return Es;
            }
            set
            {
                Es = value;
            }
        }
        public double Strand_Area
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public int Strands_Number
        {
            get
            {
                return Ns;
            }
            set
            {
                Ns = value;
            }
        }
        public double Jacking_End_Slip
        {
            get
            {
                return Sl;
            }
            set
            {
                Sl = value;
            }
        }

        public string Sheathing_Type { get; set; }
        public double Friction_Coefficient
        {
            get
            {
                return mu;
            }
            set
            {
                mu = value;
            }
        }
        public double Wobble_Coefficient
        {
            get
            {
                return k;
            }
            set
            {
                k = value;
            }
        }

        //Total Length of Cable = lc = 48.41 m.
        //Area of the Strand = a = 150 Sq.mm
        //Modulus of Elasticity = Es = 195 Gpa
        //Number of Strands = Ns = 19
        //Slip at Jacking End = Sl = 6 mm.
        //Type of Sheathing = HDPE
        //Coefficient of Friction (µ) = 0.17
        //Wobble Coefficient (k) = 0.002



        public PSC_Force_Data L { get; set; }
        public PSC_Force_Data Distance { get { return L; } set { L = value; } }
        public PSC_Force_Data SUM_L { get; set; }
        public PSC_Force_Data theta { get; set; }
        public PSC_Force_Data SUM_theta { get; set; }
        public PSC_Force_Data mu_SUM_theta { get; set; }
        public PSC_Force_Data k_SUM_L { get; set; }
        public PSC_Force_Data A { get; set; }
        public PSC_Force_Data Z { get; set; }
        public PSC_Force_Data P { get; set; }
        public PSC_Force_Data PZ { get; set; }
        public PSC_Force_Data P_Slip { get; set; }
        public PSC_Force_Data Force_after_Friction { get { return F_friction; } }
        public PSC_Force_Data Force_after_Slip { get { return F_Slip; } }

        public PSC_Force_Data Height_from_sofit { get; set; } //Chiranjit [2013 06 20]
        public PSC_Force_Data PresenseOfCable { get; set; } //Chiranjit [2013 06 20]


        public Average_Forces_after_friction_and_Slip()
        {
            Distance = new PSC_Force_Data(6);
            SUM_L = new PSC_Force_Data(6);
            theta = new PSC_Force_Data(6);
            SUM_theta = new PSC_Force_Data(6);
            mu_SUM_theta = new PSC_Force_Data(6);
            k_SUM_L = new PSC_Force_Data(6);
            A = new PSC_Force_Data(6);
            Z = new PSC_Force_Data(6);
            P = new PSC_Force_Data(6);
            PZ = new PSC_Force_Data(6);
            P_Slip = new PSC_Force_Data(6);
            Height_from_sofit = new PSC_Force_Data(6);
            PresenseOfCable = new PSC_Force_Data(6);
            //Force_after_Friction = new PSC_Force_Data(6);
            //Force_after_Slip = new PSC_Force_Data(6);

            lc = 0;
            Es = 0;
            a = 0;
            Sl = 0.0;
            mu = 0.0;
            k = 0.0;
        }

        //Point:                                 1 (At Support)                 2                                  3                                  4                                  5                                  6 (At Mid Span)
        //Distance = d =                0.00                                1.00                                4.00                                22.71                                22.71                      d6=lc/2=24.21
        //ΣL                                0.00                                1.00                                4.00                                22.71                                22.71                                24.21
        //θ                                0.00                                0.00                                0.193                                0.00                                0.00                                0.067
        //Σθ                                Σθ1= θ1                                Σθ2= Σθ1+θ2                  Σθ3= Σθ2+θ3                Σθ4= Σθ3+θ4                Σθ5= Σθ4+θ5                Σθ6= Σθ5+θ6
        //        =0.00                                =0.00                                =0.193                                =0.193                                =0.193                                =0.260
        //µ x Σθ                                =0.00                                =0.00                                =0.0328                                =0.0328                                =0.0328                                =0.0442
        //k x ΣL                                =0.00                                =0.002                                =0.008                                =0.045                                =0.045                                =0.048
        //A= µxΣθ+kxΣL                =0.00                                =0.002                                =0.0408                                =0.0778                                =0.0778                                =0.0922
        //Z=1 – A                                =1.000                                =0.998                                =0.960                                =0.922                                =0.922                                =0.910
        //P                                378.9                                378.9                                378.9                                378.9                                378.9                                378.9
        //PZ=P x Z                                =378.9                                =378.1                                =363.7                                =349.3                                =349.3                                =344.8
        //P_Slip                                =325.976                                =326.776                                =341.176                                =350.4                                =350.4                                =345.4
        //Force after Friction                =378.500                                =370.900                                =356.500                                =349.300                                =347.050                                =0.0
        //Force after Slip                =326.376                                =333.976                                =345.788                                =350.400                                =347.900                                =0.0


        public void Calculation()
        {
            list = new List<string>();

            F_friction = new PSC_Force_Data(6);
            F_Slip = new PSC_Force_Data(6);

            //list.Add(string.Format("Calculation Details for TABLE 9, for Cable 1:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //= (378.9-378.1)x(1.0-0.0)x10000/150.0/19/195 = 0.014 
            dy1 = (PZ.F1 - PZ.F2) * (SUM_L.F2 - SUM_L.F1) * (10000.0 / a / Ns / Es);


            list.Add(string.Format("dy1   = (PZ1 - PZ2)X(ΣL2 - ΣL1)x10000/a/Ns/Es "));
            list.Add(string.Format("      = ({0:f3}-{1:f3})x({2:f3}-{3:f3})x10000/{4:f3}/{5}/{6:f3} = {7:f4} mm.",
                                           PZ.F1, PZ.F2, SUM_L.F2, SUM_L.F1, a, Ns, Es, dy1));
            list.Add(string.Format(""));


            Sdy1 = Sl - dy1;
            list.Add(string.Format("Sdy1  = Sl - dy1 = {0:f0} - {1:f4} = {2:f4} mm.", Sl, dy1, Sdy1));
            list.Add(string.Format(""));

            dy2 = dy1 + ((PZ.F2 - PZ.F3) * SUM_L.F2 * 2 + (PZ.F2 - PZ.F3) * (SUM_L.F3 - SUM_L.F2)) * (10000 / a / Ns / Es);

            list.Add(string.Format("dy2   = dy1 + ((PZ2 - PZ3) X ΣL2 x 2 + (PZ2 - PZ3) x (ΣL3 - ΣL2)) x 10000/a/Ns/Es "));
            list.Add(string.Format("      = {0:f4} + (({1:f4}-{2:f4}) x {3:f4} x 2 + ({1:f4}-{2:f4}) x ({4:f4}-{3:f4})) x 10000/{5:f4}/{6}/{7:f4}",
                                            dy1, PZ.F2, PZ.F3, SUM_L.F2, SUM_L.F3, a, Ns, Es));
            list.Add(string.Format("      = {0:f4} mm.", dy2));
            list.Add(string.Format(""));

            Sdy2 = Sl - dy2;
            list.Add(string.Format("Sdy2  = Sl - dy2 = {0:f3} - {1:f4} = {2:f4} mm.", Sl, dy2, Sdy2));
            list.Add(string.Format(""));

            dy3 = dy2 + ((PZ.F3 - PZ.F4) * SUM_L.F3 * 2 + (PZ.F3 - PZ.F4) * (SUM_L.F4 - SUM_L.F3)) * (10000 / a / Ns / Es);

            list.Add(string.Format("dy3   = dy2 + ((PZ3 - PZ4) X ΣL3 x 2 + (PZ3 - PZ4) x (ΣL4 - ΣL3)) x 10000/a/Ns/Es "));
            list.Add(string.Format("      = {0:f3} + (({1:f3} - {2:f3}) x {3:f3} x 2 + ({1:f3} - {2:f3}) x ({3:f3}-{4:f3})) x 10000/{5:f3}/{6:f3}/{7:f3}",
                                    dy2, PZ.F3, PZ.F4, SUM_L.F3, SUM_L.F4, a, Ns, Es));
            list.Add(string.Format("      = {0:f3}  mm.", dy3));
            list.Add(string.Format(""));

            Sdy3 = Sl - dy3;
            list.Add(string.Format("Sdy3  = Sl - dy3 = {0:f3} - {1:f4} = {2:f4} mm", Sl, dy3, Sdy3));
            list.Add(string.Format(""));

            dy4 = dy3 + ((PZ.F4 - PZ.F5) * SUM_L.F4 * 2 + (PZ.F4 - PZ.F5) * (SUM_L.F5 - SUM_L.F4)) * (10000 / a / Ns / Es);


            list.Add(string.Format("dy4   = dy3 + ((PZ4 - PZ5) X ΣL4 x 2 + (PZ4 - PZ5) x (ΣL5 - ΣL4)) x 10000/a/Ns/Es "));
            list.Add(string.Format("      = {0:f3}  + (({1:f3}-{2:f3}) x {3:f3} x 2 + ({1:f3}-{2:f3}) x ({4:f3}-{3:f3})) x 10000/{5:f3}/{6}/{7:f3}",
                dy3, PZ.F4, PZ.F5, SUM_L.F4, SUM_L.F5, a, Ns, Es));
            list.Add(string.Format("      = {0:f4} mm.", dy4));
            list.Add(string.Format(""));

            Sdy4 = Sl - dy4;
            list.Add(string.Format("Sdy4  = Sl - dy4 = {0:f3} - {1:f4} = {2:f4} mm", Sl, dy4, Sdy4));
            list.Add(string.Format(""));

            dy5 = dy4 + ((PZ.F5 - PZ.F6) * SUM_L.F5 * 2 + (PZ.F5 - PZ.F6) * (SUM_L.F6 - SUM_L.F5)) * (10000 / a / Ns / Es);


            list.Add(string.Format("dy5   = dy4 + ((PZ5 - PZ6) X ΣL5 x 2 + (PZ5- PZ6) x (ΣL6 - ΣL5)) x 10000/a/Ns/Es "));
            list.Add(string.Format("      = {0:f3} + (({1:f3}-{2:f3}) x {3:f3} x 2 + ({1:f3}-{2:f3}) x ({4:f3}-{3:f3})) x 10000/{5:f3}/{6}/{7:f3}",
                                             dy4, PZ.F5, PZ.F6, SUM_L.F5, SUM_L.F6, a, Ns, Es));
            list.Add(string.Format("      = {0:f3} mm.", dy5));
            list.Add(string.Format(""));
            Sdy5 = Sl - dy5;
            list.Add(string.Format("Sdy5  = Sl - dy5 = {0:f3} - {1:f4} = {2:f4} mm", Sl, dy5, Sdy5));
            list.Add(string.Format(""));



            Calc1 = (L.F4 - L.F3) / (PZ.F3 - PZ.F4) / 10.0;
            Calc2 = L.F3 * 2;
            Calc3 = Sdy2 * Ns * a * Es / 1000.0;

            list.Add(string.Format("Calc1 = (L4-L3)/(PZ3-PZ4) = ({0:f3}-{1:f3})/({2:f3} - {3:f3}) /10= {4:f3}",
                                            L.F4, L.F3, PZ.F3, PZ.F4, Calc1));
            list.Add(string.Format(""));


            list.Add(string.Format("Calc2 = L3 x 2 = {0:f3} x 2 = {1:f3}", L.F3, Calc2));
            list.Add(string.Format(""));
            list.Add(string.Format("Calc3 = Sdy2 x Ns x a x Es / 1000.0 = {0:f3} x {1} x {2:f3} x {3:f3} / 1000.0 = {4:f3} ", Sdy2, Ns, a, Es, Calc3));
            list.Add(string.Format("     "));
            list.Add(string.Format(""));

            list.Add(string.Format(""));


            X = (-Calc2 + Math.Sqrt(Calc2 * Calc2 + 4 * Calc1 * Calc3)) / 2 / Calc1;


            list.Add(string.Format("X    = (- Calc2 + Sqrt (Calc2 x  Calc2 + 4 x Calc1 x Calc3)) / 2 / Calc1"));
            list.Add(string.Format("     = (- 8 + Sqrt({0:f3} x {0:f3} + 4 x {1:f3} x {2:f3})) / 2 / {1:f3}",
                                        Calc1, Calc2, Calc3));
            list.Add(string.Format("     = {0:f3}", X));
            list.Add(string.Format(""));


            Lx = Calc1 * X + SUM_L.F3;


            list.Add(string.Format("Lx = Calc1 x X + ΣL3 = ({0:f4} x {1:f4}) + {2:f4} = {3:f4}", Calc1, X, SUM_L.F3, Lx));
            list.Add(string.Format(""));

            Px = (PZ.F3 * 10) - X;

            list.Add(string.Format("Px = (PZ3 x 10) - X = {0:f3} x 10 - {1:f3} = {2:f3} kN = {3:f3} Ton.",
                PZ.F3, X, Px, (Px = Px / 10.0)));

            list.Add(string.Format(""));
            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ1 = {2:f3},  ", Px, PZ.F6, PZ.F1));
            list.Add(string.Format(""));


            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                list.Add(string.Format(""));
                if (Px > PZ.F1)
                {
                    P_Slip.F1 = PZ.F1;
                    list.Add(string.Format("IF (Px > PZ1) P_slip1 = PZ1 = {0:f3}", P_Slip.F1));
                    list.Add(string.Format("ELSE   P_slip1  = PZ1 - (PZ1- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ1- PZ6)"));
                    list.Add(string.Format(""));
                }
                else
                {
                    P_Slip.F1 = PZ.F1 - (PZ.F1 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ1) P_slip1 = PZ1"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ1 - (PZ1- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F1, Px, P_Slip.F1));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ1- PZ6)"));
                    list.Add(string.Format(""));
                }
            }
            else
            {
                P_Slip.F1 = Px - (PZ.F1 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ1) P_slip1 = PZ1"));
                list.Add(string.Format("ELSE   P_slip1  = PZ1 - (PZ1- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ1- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F1, PZ.F6));
                list.Add(string.Format(""));
            }

            //list.Add(string.Format("IF (Px > PZ1) P_slip1 = PZ1"));
            //list.Add(string.Format("ELSE   P_slip1  = PZ1 - (PZ1- Px)x2 = 378.9 - (378.9 - 352.684)x2 = 326.468"));
            //list.Add("}");
            //list.Add(string.Format("ELSE P_slip1 = Px - (PZ1- PZ6)"));







            list.Add(string.Format(""));
            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ2 = {2:f3},  ", Px, PZ.F6, PZ.F2));
            //list.Add(string.Format("IF (Px > PZ6)"));
            //list.Add("{");
            //list.Add(string.Format("IF (Px > PZ2) P_slip2 = PZ2"));
            //list.Add(string.Format("ELSE P_slip2         = PZ2 - (PZ2- Px)x2 = 378.1 - (378.1 - 352.684)x2 = 327.268"));
            //list.Add("}");
            //list.Add(string.Format("ELSE         P_slip2 = Px -(PZ2- PZ6)"));


            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                if (Px > PZ.F2)
                {
                    P_Slip.F2 = PZ.F2;
                    list.Add(string.Format("IF (Px > PZ2) P_slip1 = PZ2 = {0:f3}", P_Slip.F2));
                    list.Add(string.Format("ELSE   P_slip1  = PZ2 - (PZ2- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ2- PZ6)"));
                }
                else
                {
                    P_Slip.F2 = PZ.F2 - (PZ.F2 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ2) P_slip1 = PZ2"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ2 - (PZ2- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F2, Px, P_Slip.F2));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ2- PZ6)"));
                    list.Add(string.Format(""));
                }
                list.Add(string.Format(""));
            }
            else
            {
                P_Slip.F2 = Px - (PZ.F2 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ2) P_slip1 = PZ2"));
                list.Add(string.Format("ELSE   P_slip1  = PZ2 - (PZ2- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ2- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F2, PZ.F6));
                list.Add(string.Format(""));
            }


            list.Add(string.Format(""));
            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ3 = {2:f3},  ", Px, PZ.F6, PZ.F3));
            //list.Add(string.Format("Px = 352.684,         PZ6 = 345.4,         PZ3 = 363.7,  "));
            //list.Add(string.Format("IF (Px > PZ6)"));
            //list.Add("{");
            //list.Add(string.Format("IF (Px > PZ3) P_slip3 = PZ3"));
            //list.Add(string.Format("ELSE P_slip3         = PZ3 - (PZ3- Px)x2 = 363.7- (363.7- 352.684)x2 = 341.668"));
            //list.Add("}");
            //list.Add(string.Format("ELSE         P_slip3 = Px -( PZ3- PZ6)"));
            //list.Add(string.Format(""));


            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                if (Px > PZ.F3)
                {
                    P_Slip.F3 = PZ.F3;
                    list.Add(string.Format("IF (Px > PZ3) P_slip1 = PZ3 = {0:f3}", P_Slip.F3));
                    list.Add(string.Format("ELSE   P_slip1  = PZ3 - (PZ3- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ3- PZ6)"));
                }
                else
                {
                    P_Slip.F3 = PZ.F3 - (PZ.F3 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ3) P_slip1 = PZ3"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ3 - (PZ3- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F3, Px, P_Slip.F3));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ3- PZ6)"));
                    list.Add(string.Format(""));
                }
                list.Add(string.Format(""));
            }
            else
            {
                P_Slip.F3 = Px - (PZ.F3 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ3) P_slip1 = PZ3"));
                list.Add(string.Format("ELSE   P_slip1  = PZ3 - (PZ3- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ3- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F3, PZ.F6));
                list.Add(string.Format(""));
            }

            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ4 = {2:f3},  ", Px, PZ.F6, PZ.F4));
            //list.Add(string.Format("Px = 352.684,         PZ6 = 345.4,         PZ4 = 350.4,  "));
            //list.Add(string.Format("IF (Px > PZ6)"));
            //list.Add("{");
            //list.Add(string.Format("IF (Px > PZ4) P_slip4 = PZ4= 350.4"));
            //list.Add(string.Format("ELSE P_slip4         = PZ4 - (PZ4- Px)x2"));
            //list.Add("}");
            //list.Add(string.Format("ELSE         P_slip4 = Px-( PZ4- PZ6)"));
            list.Add(string.Format(""));

            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                if (Px > PZ.F4)
                {
                    P_Slip.F4 = PZ.F4;
                    list.Add(string.Format("IF (Px > PZ4) P_slip1 = PZ4 = {0:f3}", P_Slip.F4));
                    list.Add(string.Format("ELSE   P_slip1  = PZ4 - (PZ4- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ4- PZ6)"));
                    list.Add(string.Format(""));
                }
                else
                {
                    P_Slip.F4 = PZ.F4 - (PZ.F4 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ4) P_slip1 = PZ4"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ4 - (PZ4- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F4, Px, P_Slip.F4));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ4- PZ6)"));
                }
                list.Add(string.Format(""));
            }
            else
            {
                P_Slip.F4 = Px - (PZ.F4 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ4) P_slip1 = PZ4"));
                list.Add(string.Format("ELSE   P_slip1  = PZ4 - (PZ4- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ4- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F4, PZ.F6));
                list.Add(string.Format(""));
            }

            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ5 = {2:f3},  ", Px, PZ.F6, PZ.F5));
            //list.Add(string.Format("Px = 352.684,         PZ6 = 345.4,         PZ5 = 350.4,  "));
            //list.Add(string.Format("IF (Px > PZ6)"));
            //list.Add("{");
            //list.Add(string.Format("IF (Px > PZ5) P_slip5 = PZ5 = 350.4"));
            //list.Add(string.Format("ELSE P_slip5         = PZ5- (PZ5- Px)x2 "));
            //list.Add("}");
            //list.Add(string.Format("ELSE         P_slip5 = Px-( PZ5- PZ6)"));
            //list.Add(string.Format(""));


            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                if (Px > PZ.F5)
                {
                    P_Slip.F5 = PZ.F5;
                    list.Add(string.Format("IF (Px > PZ5) P_slip1 = PZ5 = {0:f3}", P_Slip.F5));
                    list.Add(string.Format("ELSE   P_slip1  = PZ5 - (PZ5- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ5- PZ6)"));
                }
                else
                {
                    P_Slip.F5 = PZ.F5 - (PZ.F5 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ5) P_slip1 = PZ5"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ5 - (PZ5- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F5, Px, P_Slip.F5));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ5- PZ6)"));
                }
                list.Add(string.Format(""));
            }
            else
            {
                P_Slip.F5 = Px - (PZ.F5 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ5) P_slip1 = PZ5"));
                list.Add(string.Format("ELSE   P_slip1  = PZ5 - (PZ5- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ5- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F5, PZ.F6));
                list.Add(string.Format(""));
            }

            list.Add(string.Format("Px = {0:f3},         PZ6 = {1:f3},         PZ6 = {2:f3},  ", Px, PZ.F6, PZ.F6));
            //list.Add(string.Format("Px = 352.684,         PZ6 = 345.4,         PZ6 = 345.4,  "));
            //list.Add(string.Format("IF (Px > PZ6)"));
            //list.Add("{");
            //list.Add(string.Format("IF (Px > PZ6) P_slip6 = PZ6 = 345.4"));
            //list.Add(string.Format("ELSE P_slip6         = PZ6 - (PZ6- Px)x2 "));
            //list.Add("}");
            //list.Add(string.Format("ELSE         P_slip6 = Px - (PZ6- PZ6)"));
            list.Add(string.Format(""));

            list.Add(string.Format("IF (Px > PZ6)"));
            list.Add("{");
            list.Add(string.Format(""));
            if (Px > PZ.F6)
            {
                if (Px > PZ.F6)
                {
                    P_Slip.F6 = PZ.F6;
                    list.Add(string.Format("IF (Px > PZ6) P_slip1 = PZ6 = {0:f3}", P_Slip.F6));
                    list.Add(string.Format("ELSE   P_slip1  = PZ6 - (PZ6- Px) x 2"));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ6- PZ6)"));
                }
                else
                {
                    P_Slip.F6 = PZ.F6 - (PZ.F6 - Px) * 2;
                    list.Add(string.Format("IF (Px > PZ6) P_slip1 = PZ6"));
                    list.Add(string.Format("ELSE   P_slip1  = PZ6 - (PZ6- Px)x2 = {0:f3} - ({0:f3} - {1:f3}) x 2 = {2:f3}", PZ.F6, Px, P_Slip.F6));
                    list.Add(string.Format(""));
                    list.Add("}");
                    list.Add(string.Format(""));
                    list.Add(string.Format("ELSE P_slip1 = Px - (PZ6- PZ6)"));
                }
                list.Add(string.Format(""));
            }
            else
            {
                P_Slip.F6 = Px - (PZ.F6 - PZ.F6);

                list.Add(string.Format(""));
                list.Add(string.Format("IF (Px > PZ6) P_slip1 = PZ6"));
                list.Add(string.Format("ELSE   P_slip1  = PZ6 - (PZ6- Px) x 2"));
                list.Add(string.Format(""));
                list.Add("}");
                list.Add(string.Format(""));
                list.Add(string.Format("ELSE P_slip1 = Px - (PZ6- PZ6) = {0:f4} - ({1:f4} - {2:f3})", Px, PZ.F6, PZ.F6));
                list.Add(string.Format(""));
            }

            list.Add(string.Format("Average Force after Friction in Cable :"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format("PZ1 = {0:f3} ", PZ.F1));
            list.Add(string.Format("PZ2 = {0:f3} ", PZ.F2));
            list.Add(string.Format("PZ3 = {0:f3} ", PZ.F3));
            list.Add(string.Format("PZ4 = {0:f3} ", PZ.F4));
            list.Add(string.Format("PZ5 = {0:f3} ", PZ.F5));
            list.Add(string.Format("PZ6 = {0:f3} ", PZ.F6));
            //list.Add(string.Format("     = 378.9            = 378.1                               =349.3                =349.3                =344.8"));
            list.Add(string.Format(""));



            F_friction.F1 = (PZ.F1 + PZ.F2) / 2.0;
            F_friction.F2 = (PZ.F2 + PZ.F3) / 2.0;
            F_friction.F3 = (PZ.F3 + PZ.F4) / 2.0;
            F_friction.F4 = (PZ.F4 + PZ.F5) / 2.0;
            F_friction.F5 = (PZ.F5 + PZ.F6) / 2.0;
            //F_friction.F6 = PZ.F6; //Chiranjit [2013 06 20]

            list.Add(string.Format("F_friction1 = (PZ1+ PZ2)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                PZ.F1, PZ.F2, F_friction.F1));
            list.Add(string.Format("F_friction2 = (PZ2+ PZ3)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                PZ.F2, PZ.F3, F_friction.F2));
            list.Add(string.Format("F_friction3 = (PZ3+ PZ4)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                PZ.F3, PZ.F4, F_friction.F3));
            list.Add(string.Format("F_friction4 = (PZ4+ PZ5)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                PZ.F4, PZ.F5, F_friction.F4));
            list.Add(string.Format("F_friction5 = (PZ5+ PZ6)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                PZ.F5, PZ.F6, F_friction.F5));
            //list.Add(string.Format("F_friction6 =  PZ6  = {0:f3}", F_friction.F6)); //Chiranjit [2013 06 20]
            list.Add(string.Format(""));




            list.Add(string.Format("Average Force after Slip in Cable :"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("P_Slip1 = {0:f3}", P_Slip.F1));
            list.Add(string.Format("P_Slip2 = {0:f3}", P_Slip.F2));
            list.Add(string.Format("P_Slip3 = {0:f3}", P_Slip.F3));
            list.Add(string.Format("P_Slip4 = {0:f3}", P_Slip.F4));
            list.Add(string.Format("P_Slip5 = {0:f3}", P_Slip.F5));
            list.Add(string.Format("P_Slip6 = {0:f3}", P_Slip.F6));


            //list.Add(string.Format("=326.468        =327.268        =341.668        =350.400        =350.400        =345.400"));
            list.Add(string.Format(""));



            F_Slip.F1 = (P_Slip.F1 + P_Slip.F2) / 2;
            F_Slip.F2 = (P_Slip.F2 + P_Slip.F3) / 2;
            F_Slip.F3 = (P_Slip.F3 + P_Slip.F4) / 2;
            F_Slip.F4 = (P_Slip.F4 + P_Slip.F5) / 2;
            F_Slip.F5 = (P_Slip.F5 + P_Slip.F6) / 2;
            F_Slip.F6 = P_Slip.F6; //Chiranjit [2013 06 24]

            list.Add(string.Format("F_slip1 = (P_Slip1+ P_Slip2)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                P_Slip.F1, P_Slip.F2, F_Slip.F1));

            list.Add(string.Format("F_slip2 = (P_Slip2+ P_Slip3)/2 = ({0:f3} + {1:f3})/2 = {2:f3}",
                P_Slip.F2, P_Slip.F3, F_Slip.F2));

            list.Add(string.Format("F_slip3 = (P_Slip3+ P_Slip4)/2 = ({0:f3} + {1:f3})/2 = {2:f3}",
                P_Slip.F3, P_Slip.F4, F_Slip.F3));

            list.Add(string.Format("F_slip4 = (P_Slip4+ P_Slip5)/2 =  ({0:f3} + {1:f3})/2 = {2:f3}",
                P_Slip.F4, P_Slip.F5, F_Slip.F4));

            list.Add(string.Format("F_slip5 = (P_Slip5+ P_Slip6)/2 = ({0:f3} + {1:f3})/2 = {2:f3}",
                P_Slip.F5, P_Slip.F6, F_Slip.F5));
            //list.Add(string.Format("F_slip6 = P_Slip6 = {0:f3} ", F_Slip.F6)); //Chiranjit [2013 06 24]
            list.Add(string.Format(""));
            list.Add(string.Format("ELONGATION in Cable :"));
            list.Add(string.Format("--------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("ΣL =   ΣL1 = {0:f3}   ΣL2 = {1:f3}   ΣL3 = {2:f3}  ΣL4 = {3:f3}  ΣL5= {4:f3}   ΣL6 = {5:f3}",
                SUM_L.F1, SUM_L.F2, SUM_L.F3, SUM_L.F4, SUM_L.F5, SUM_L.F6));



            Elongation = (F_friction.F1 * (L.F2 - L.F1) + F_friction.F2 * (L.F3 - L.F2) + F_friction.F3 * (L.F4 - L.F3)
              + F_friction.F4 * (L.F5 - L.F4) + F_friction.F5 * (L.F6 - L.F5)) * (10000 / (a * Es * Ns));


            list.Add(string.Format(""));
            list.Add(string.Format("Elongation = [F_friction1x(L2-L1)+F_friction2x(L3-L2)+F_friction3x(L4-L3)"));
            list.Add(string.Format("             +F_friction4x(L5-L4)+F_friction5x(L6-L5)]x10000 /(axEsxNs)"));

            list.Add(string.Format(""));

            list.Add(string.Format("           = [{0:f3}x({1:f3}-{2:f3})+{3:f3}x({4:f3}-{1:f3})+{5:f3}x({6:f3}-{4:f3})",
                                                F_friction.F1, L.F2, L.F1, F_friction.F2, L.F3, F_friction.F3, L.F4));
            list.Add(string.Format("             +{0:f3}x({1:f3}-{2:f3})+{3:f3}x({4:f3}-{2:f3})]x10000 /({5:f3}x{6:f3}x{7:f3})",
                F_friction.F4, L.F5, L.F4, F_friction.F5, L.F6, a, Es, Ns));


            list.Add(string.Format(""));
            //list.Add(string.Format("           =[378.500x(1-0)+ 370.900x(4-1)+356.5x(22.71-4)+349.3x(22.71-22.71)+347.05x(24.21-22.71)]x10000/(150x195x19)"));
            list.Add(string.Format("           = {0:f3} mm.", Elongation));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
        }

        public void Read_Data_From_Grid(DataGridView dgv)
        {
            string kStr = "";
            int row = 0;
            for (int col = 1; col < dgv.ColumnCount; col++)
            {

                //L
                kStr = dgv[col, 0].Value.ToString().Replace("=", "");
                L[col - 1] = MyList.StringToDouble(kStr, 0.0);

                //Chiranjit [2013 06 23]
                dgv[col, 1].Value = dgv[col, 0].Value;

                //SUM_L
                kStr = dgv[col, 1].Value.ToString().Replace("=", "");
                SUM_L[col - 1] = MyList.StringToDouble(kStr, 0.0);


                //theta
                kStr = dgv[col, 2].Value.ToString().Replace("=", "");
                theta[col - 1] = MyList.StringToDouble(kStr, 0.0);



                //P
                kStr = dgv[col, 9].Value.ToString().Replace("=", "");
                P[col - 1] = MyList.StringToDouble(kStr, 0.0);
                
                //SUM_Theta
                //mu_SUM_theta
                //k_SUM_theta
                //A
                //Z
                //P
                //PZ
                //P_Slip
                //Force_after_Friction
                //Force_after_Slip



                //Height from Sofit
                kStr = dgv[col, 14].Value.ToString().Replace("=", "");
                Height_from_sofit[col - 1] = MyList.StringToDouble(kStr, 0.0);
                

            }


            SUM_theta.F1 = theta.F1;
            SUM_theta.F2 = SUM_theta.F1 + theta.F2;
            SUM_theta.F3 = SUM_theta.F2 + theta.F3;
            SUM_theta.F4 = SUM_theta.F3 + theta.F4;
            SUM_theta.F5 = SUM_theta.F4 + theta.F5;
            SUM_theta.F6 = SUM_theta.F5 + theta.F6;



            mu_SUM_theta = mu * SUM_theta;
            k_SUM_L = k * SUM_L;
            A = mu_SUM_theta + k_SUM_L;
            Z = 1 - A;
            PZ = P * Z;

            Calculation();


            for (int col = 1; col < dgv.ColumnCount; col++)
            {

                //L
                //kStr = dgv[col, 0].Value.ToString().Replace("=", "");
                //L[col - 1] = MyList.StringToDouble(kStr, 0.0);


                //SUM_L
                //kStr = dgv[col, 1].Value.ToString().Replace("=", "");
                //SUM_L[col - 1] = MyList.StringToDouble(kStr, 0.0);


                //theta
                //kStr = dgv[col, 2].Value.ToString().Replace("=", "");
                //theta[col - 1] = MyList.StringToDouble(kStr, 0.0);


                try
                {
                    dgv[col, 4].Value = "=" + SUM_theta[col - 1].ToString("f3");
                    dgv[col, 5].Value = "=" + mu_SUM_theta[col - 1].ToString("f3");
                    dgv[col, 6].Value = "=" + k_SUM_L[col - 1].ToString("f3");
                    dgv[col, 7].Value = "=" + A[col - 1].ToString("f3");
                    dgv[col, 8].Value = "=" + Z[col - 1].ToString("f3");
                    //dgv[col, 9].Value = "=" + P[col - 1].ToString("f3");
                    dgv[col, 10].Value = "=" + PZ[col - 1].ToString("f3");
                    dgv[col, 11].Value = "=" + P_Slip[col - 1].ToString("f3");
                    dgv[col, 12].Value = "=" + F_friction[col - 1].ToString("f3");
                    dgv[col, 13].Value = "=" + F_Slip[col - 1].ToString("f3");
                }
                catch (Exception ex) { }

                //P
                kStr = dgv[col, 9].Value.ToString().Replace("=", "");
                P[col - 1] = MyList.StringToDouble(kStr, 0.0);

                //SUM_Theta
                //mu_SUM_theta
                //k_SUM_L
                //A
                //Z
                //P
                //PZ
                //P_Slip
                //Force_after_Friction
                //Force_after_Slip
            }
        }

        public List<string> Get_Table_Data()
        {
            List<string> list_table = new List<string>();
            string format1 = "{0,-24} {1,-15:f3} {2,-15:f3} {3,-15:f3} {4,-15:f3} {5,-15:f3} {6,-15:f3}";
            string format2 = "{0,-24}={1,-15:f3}={2,-15:f3}={3,-15:f3}={4,-15:f3}={5,-15:f3}={6,-15:f3}";

            list_table.Add("".PadLeft(130, '-'));
            list_table.Add(string.Format(format1,
                "Point:", "1 (At Support)", "2", "3", "4", "5", "6 (At Mid Span)"));
            list_table.Add("".PadLeft(130, '-'));
            list_table.Add(string.Format(format1,
                         "Distance = d", Distance.F1, Distance.F2, Distance.F3,
                         Distance.F4, Distance.F5, Distance.F6));

            list_table.Add(string.Format(format1,
                                   "ΣL", SUM_L.F1, SUM_L.F2, SUM_L.F3,
                                   SUM_L.F4, SUM_L.F5, SUM_L.F6));

            list_table.Add(string.Format(format1,
                                   "θ", theta.F1, theta.F2, theta.F3,
                                   theta.F4, theta.F5, theta.F6));
            list_table.Add("");

            list_table.Add(string.Format(format1.Replace("24","20"),
              "Σθ", "Σθ1= θ1", "Σθ2= Σθ1+θ2", "Σθ3= Σθ2+θ3", "Σθ4= Σθ3+θ4", "Σθ5= Σθ4+θ5", "Σθ6= Σθ5+θ6"));


            list_table.Add(string.Format(format2,
                                   "", SUM_theta.F1, SUM_theta.F2, SUM_theta.F3,
                                   SUM_theta.F4, SUM_theta.F5, SUM_theta.F6));
            list_table.Add("");

            list_table.Add(string.Format(format2,
                                   "µ x Σθ", mu_SUM_theta.F1, mu_SUM_theta.F2, mu_SUM_theta.F3,
                                   mu_SUM_theta.F4, mu_SUM_theta.F5, mu_SUM_theta.F6));

            list_table.Add(string.Format(format2,
                                   "k x ΣL", k_SUM_L.F1, k_SUM_L.F2, k_SUM_L.F3,
                                   k_SUM_L.F4, k_SUM_L.F5, k_SUM_L.F6));

            list_table.Add(string.Format(format2,
                                   "A= µxΣθ+kxΣL", A.F1, A.F2, A.F3,
                                   A.F4, A.F5, A.F6));

            list_table.Add(string.Format(format2,
                                   "Z=1 – A", Z.F1, Z.F2, Z.F3,
                                   Z.F4, Z.F5, Z.F6));
            list_table.Add("");

            list_table.Add(string.Format(format1,
                                   "P", P.F1, P.F2, P.F3,
                                   P.F4, P.F5, P.F6));

            list_table.Add("");
            list_table.Add(string.Format(format2,
                                   "PZ=P x Z", PZ.F1, PZ.F2, PZ.F3,
                                   PZ.F4, PZ.F5, PZ.F6));
            list_table.Add(string.Format(format2,
                                   "P_Slip", P_Slip.F1, P_Slip.F2, P_Slip.F3,
                                   P_Slip.F4, P_Slip.F5, P_Slip.F6));
            list_table.Add(string.Format(format2,
                                   "Force after Friction", F_friction.F1, F_friction.F2, F_friction.F3,
                                   F_friction.F4, F_friction.F5, F_friction.F6));

            list_table.Add(string.Format(format2,
                                           "Force after Slip", F_Slip.F1, F_Slip.F2, F_Slip.F3,
                                           F_Slip.F4, F_Slip.F5, F_Slip.F6));
            //list_table.Add("".PadLeft(130, '-'));

            //Chiranjit [2013 06 26]
            list_table.Add(string.Format(format2,
                                           "Height from Sofit", Height_from_sofit.F1, Height_from_sofit.F2, Height_from_sofit.F3,
                                           Height_from_sofit.F4, Height_from_sofit.F5, Height_from_sofit.F6));
            list_table.Add("".PadLeft(130, '-'));

            list_table.Add("");
            list_table.Add("");


            list_table.Add(string.Format("ELONGATION in Cable :  {0:f4} mm", Elongation));


            return list_table;

            //Point:                                 1 (At Support)                 2                                  3                                  4                                  5                                  6 (At Mid Span)
            //Distance = d =                0.00                                1.00                                4.00                                22.71                                22.71                      d6=lc/2=24.21
            //ΣL                                0.00                                1.00                                4.00                                22.71                                22.71                                24.21
            //θ                                0.00                                0.00                                0.193                                0.00                                0.00                                0.067
            //Σθ                                Σθ1= θ1                                Σθ2= Σθ1+θ2                  Σθ3= Σθ2+θ3                Σθ4= Σθ3+θ4                Σθ5= Σθ4+θ5                Σθ6= Σθ5+θ6
            //        =0.00                                =0.00                                =0.193                                =0.193                                =0.193                                =0.260
            //µ x Σθ                                =0.00                                =0.00                                =0.0328                                =0.0328                                =0.0328                                =0.0442
            //k x ΣL                                =0.00                                =0.002                                =0.008                                =0.045                                =0.045                                =0.048
            //A= µxΣθ+kxΣL                =0.00                                =0.002                                =0.0408                                =0.0778                                =0.0778                                =0.0922
            //Z=1 – A                                =1.000                                =0.998                                =0.960                                =0.922                                =0.922                                =0.910
            //P                                378.9                                378.9                                378.9                                378.9                                378.9                                378.9
            //PZ=P x Z                                =378.9                                =378.1                                =363.7                                =349.3                                =349.3                                =344.8
            //P_Slip                                =325.976                                =326.776                                =341.176                                =350.4                                =350.4                                =345.4
            //Force after Friction                =378.500                                =370.900                                =356.500                                =349.300                                =347.050                                =0.0
            //Force after Slip                =326.376                                =333.976                                =345.788                                =350.400                                =347.900                                =0.0
          
            //ELONGATION in Cable 2 (in mm):  156.0

        }
    }


}
