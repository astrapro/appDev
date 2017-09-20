using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_Cont_Box_End_Cable_Profile : Form
    {
        public frm_Cont_Box_End_Cable_Profile()
        {
            InitializeComponent();
        }

        
        Continuous_Box_Girder_Design CBGD;
        Cross_Section_Box_Girder CS;
        public End_Span_Cable_Profile_Data PD { get; set; }
        public List<string> Results { get; set; }
        public List<string> Calculation_Details_Friction { get; set; }
        public List<string> Calculation_Details_Slip { get; set; }

        int Cable_No = 1;
        public frm_Cont_Box_End_Cable_Profile(Continuous_Box_Girder_Design cnt, Cross_Section_Box_Girder _CS, int cable_no)
        {
            CBGD = cnt;
            CS = _CS;
            InitializeComponent();
            Cable_No = cable_no;

            lbl_cbl.Text = "CABLE NO. " + cable_no;

            btn_Process.Text = string.Format("PROCESS {0} of 4", cable_no);
            Default_Input_Data(Cable_No);

        }
        public void Set_User_Input()
        {
            PD = new End_Span_Cable_Profile_Data();


            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B1.Text, 0.0));
            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B2.Text, 0.0));
            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B3.Text, 0.0));
            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B4.Text, 0.0));
            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B5.Text, 0.0));
            PD.Profile_1_L.Add(MyList.StringToDouble(txt_cab_pro1_B6.Text, 0.0));
            
            
            PD.Profile_1_D.Add(MyList.StringToDouble(txt_cab_pro1_D1.Text, 0.0));
            PD.Profile_1_D.Add(MyList.StringToDouble(txt_cab_pro1_D2.Text, 0.0));
            PD.Profile_1_D.Add(MyList.StringToDouble(txt_cab_pro1_D3.Text, 0.0));
            PD.Profile_1_D.Add(MyList.StringToDouble(txt_cab_pro1_D4.Text, 0.0));





            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B1.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B2.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B3.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B4.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B5.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B6.Text, 0.0));



            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B7.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B8.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B9.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B10.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B11.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B12.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B13.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B14.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B15.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B16.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B17.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B18.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B19.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B20.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B21.Text, 0.0));
            PD.Profile_2_L.Add(MyList.StringToDouble(txt_cab_pro2_B22.Text, 0.0));



            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D1.Text, 0.0));
            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D2.Text, 0.0));
            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D3.Text, 0.0));
            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D4.Text, 0.0));
            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D5.Text, 0.0));
            PD.Profile_2_D.Add(MyList.StringToDouble(txt_cab_pro2_D6.Text, 0.0));

            //PD.NC.Add(MyList.StringToInt(txt_Nc_0.Text, 2);
            PD.NC.Clear();
            PD.NC.Add(MyList.StringToInt(txt_Nc_0.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_01.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_1.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_2.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_3.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_4.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_5.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_6.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_7.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_8.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_9.Text, 2));
            PD.NC.Add(MyList.StringToInt(txt_Nc_10.Text, 2));


        }
        public void Default_Input_Data(int cable_no)
        {
            txt_Nc_0.Text = "2";
            switch (cable_no)
            {
                case 1:
                    #region Cable 1
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "14.3";
                    txt_cab_pro1_B3.Text = "5.0";
                    txt_cab_pro1_B4.Text = "14.3";
                    txt_cab_pro1_B5.Text = "1.0";
                    txt_cab_pro1_B6.Text = "35.6";

                    txt_cab_pro1_D1.Text = "0.202";
                    txt_cab_pro1_D2.Text = "1.448";
                    txt_cab_pro1_D3.Text = "1.60";
                    txt_cab_pro1_D4.Text = "1.65";


                    txt_cab_pro2_B1.Text = "35.6";
                    txt_cab_pro2_B2.Text = "17.8";
                    txt_cab_pro2_B3.Text = "4.35";
                    txt_cab_pro2_B4.Text = "3.0";
                    txt_cab_pro2_B5.Text = "3.0";
                    txt_cab_pro2_B6.Text = "3.0";
                    txt_cab_pro2_B7.Text = "4.45";
                    txt_cab_pro2_B8.Text = "5.55";
                    txt_cab_pro2_B9.Text = "3.0";
                    txt_cab_pro2_B10.Text = "3.0";
                    txt_cab_pro2_B11.Text = "3.0";
                    txt_cab_pro2_B12.Text = "3.25";
                    txt_cab_pro2_B13.Text = "10.0";
                    txt_cab_pro2_B14.Text = "3.0";
                    txt_cab_pro2_B15.Text = "3.0";
                    txt_cab_pro2_B16.Text = "3.0";
                    txt_cab_pro2_B17.Text = "3.0";
                    txt_cab_pro2_B18.Text = "4.35";
                    txt_cab_pro2_B19.Text = "6.0";
                    txt_cab_pro2_B20.Text = "16.0";
                    txt_cab_pro2_B21.Text = "6.0";
                    txt_cab_pro2_B22.Text = "3.25";
                  


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.1125";
                    txt_cab_pro2_D4.Text = "0.1125";

                    txt_cab_pro2_D5.Text = "0.150";
                    txt_cab_pro2_D6.Text = "0.150";
                    #endregion Cable 1
                    break;
                case 2:
                    #region Cable 2
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "14.3";
                    txt_cab_pro1_B3.Text = "5.0";
                    txt_cab_pro1_B4.Text = "14.3";
                    txt_cab_pro1_B5.Text = "1.0";
                    txt_cab_pro1_B6.Text = "35.6";

                    txt_cab_pro1_D1.Text = "0.172";
                    txt_cab_pro1_D2.Text = "1.228";
                    txt_cab_pro1_D3.Text = "1.400";
                    txt_cab_pro1_D4.Text = "1.40";


                    txt_cab_pro2_B1.Text = "35.6";
                    txt_cab_pro2_B2.Text = "17.8";
                    txt_cab_pro2_B3.Text = "4.35";
                    txt_cab_pro2_B4.Text = "3.0";
                    txt_cab_pro2_B5.Text = "3.0";
                    txt_cab_pro2_B6.Text = "3.0";
                    txt_cab_pro2_B7.Text = "4.45";
                    txt_cab_pro2_B8.Text = "5.55";
                    txt_cab_pro2_B9.Text = "3.0";
                    txt_cab_pro2_B10.Text = "3.0";
                    txt_cab_pro2_B11.Text = "3.0";
                    txt_cab_pro2_B12.Text = "3.25";
                    txt_cab_pro2_B13.Text = "10.0";
                    txt_cab_pro2_B14.Text = "3.0";
                    txt_cab_pro2_B15.Text = "3.0";
                    txt_cab_pro2_B16.Text = "3.0";
                    txt_cab_pro2_B17.Text = "3.0";
                    txt_cab_pro2_B18.Text = "4.35";
                    txt_cab_pro2_B19.Text = "6.0";
                    txt_cab_pro2_B20.Text = "16.0";
                    txt_cab_pro2_B21.Text = "6.0";
                    txt_cab_pro2_B22.Text = "3.25";
                  


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.1125";
                    txt_cab_pro2_D4.Text = "0.1125";

                    txt_cab_pro2_D5.Text = "0.150";
                    txt_cab_pro2_D6.Text = "0.150";
                    #endregion Cable 2

                    break;
                case 3:
                     #region Cable 3
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "14.3";
                    txt_cab_pro1_B3.Text = "5.0";
                    txt_cab_pro1_B4.Text = "14.3";
                    txt_cab_pro1_B5.Text = "1.0";
                    txt_cab_pro1_B6.Text = "35.6";

                    txt_cab_pro1_D1.Text = "0.141";
                    txt_cab_pro1_D2.Text = "1.009";
                    txt_cab_pro1_D3.Text = "1.200";
                    txt_cab_pro1_D4.Text = "1.15";


                    txt_cab_pro2_B1.Text = "35.6";
                    txt_cab_pro2_B2.Text = "17.8";
                    txt_cab_pro2_B3.Text = "4.35";
                    txt_cab_pro2_B4.Text = "3.0";
                    txt_cab_pro2_B5.Text = "3.0";
                    txt_cab_pro2_B6.Text = "3.0";
                    txt_cab_pro2_B7.Text = "4.45";
                    txt_cab_pro2_B8.Text = "5.55";
                    txt_cab_pro2_B9.Text = "3.0";
                    txt_cab_pro2_B10.Text = "3.0";
                    txt_cab_pro2_B11.Text = "3.0";
                    txt_cab_pro2_B12.Text = "3.25";
                    txt_cab_pro2_B13.Text = "10.0";
                    txt_cab_pro2_B14.Text = "3.0";
                    txt_cab_pro2_B15.Text = "3.0";
                    txt_cab_pro2_B16.Text = "3.0";
                    txt_cab_pro2_B17.Text = "3.0";
                    txt_cab_pro2_B18.Text = "4.35";
                    txt_cab_pro2_B19.Text = "6.0";
                    txt_cab_pro2_B20.Text = "16.0";
                    txt_cab_pro2_B21.Text = "6.0";
                    txt_cab_pro2_B22.Text = "3.25";
                  


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.1125";
                    txt_cab_pro2_D4.Text = "0.1125";

                    txt_cab_pro2_D5.Text = "0.150";
                    txt_cab_pro2_D6.Text = "0.150";
                    #endregion Cable 3
                    break;
                case 4:
                    #region Cable 4
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "14.3";
                    txt_cab_pro1_B3.Text = "5.0";
                    txt_cab_pro1_B4.Text = "14.3";
                    txt_cab_pro1_B5.Text = "1.0";
                    txt_cab_pro1_B6.Text = "35.6";

                    txt_cab_pro1_D1.Text = "0.110";
                    txt_cab_pro1_D2.Text = "0.790";
                    txt_cab_pro1_D3.Text = "1.00";
                    txt_cab_pro1_D4.Text = "0.90";


                    txt_cab_pro2_B1.Text = "35.6";
                    txt_cab_pro2_B2.Text = "17.8";
                    txt_cab_pro2_B3.Text = "4.35";
                    txt_cab_pro2_B4.Text = "3.0";
                    txt_cab_pro2_B5.Text = "3.0";
                    txt_cab_pro2_B6.Text = "3.0";
                    txt_cab_pro2_B7.Text = "4.45";
                    txt_cab_pro2_B8.Text = "5.55";
                    txt_cab_pro2_B9.Text = "3.0";
                    txt_cab_pro2_B10.Text = "3.0";
                    txt_cab_pro2_B11.Text = "3.0";
                    txt_cab_pro2_B12.Text = "3.25";
                    txt_cab_pro2_B13.Text = "10.0";
                    txt_cab_pro2_B14.Text = "3.0";
                    txt_cab_pro2_B15.Text = "3.0";
                    txt_cab_pro2_B16.Text = "3.0";
                    txt_cab_pro2_B17.Text = "3.0";
                    txt_cab_pro2_B18.Text = "4.35";
                    txt_cab_pro2_B19.Text = "6.0";
                    txt_cab_pro2_B20.Text = "16.0";
                    txt_cab_pro2_B21.Text = "6.0";
                    txt_cab_pro2_B22.Text = "3.25";
                  


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.1125";
                    txt_cab_pro2_D4.Text = "0.1125";

                    txt_cab_pro2_D5.Text = "0.150";
                    txt_cab_pro2_D6.Text = "0.150";
                    #endregion Cable 4
                    break;
            }
            
        }


        private void frm_Cont_Box_Cable_Profile_Load(object sender, EventArgs e)
        {
            dgv_friction_loss.Rows.Clear();
            dgv_slip_loss.Rows.Clear();

            //if (Cable_No == 1)
            Update_Datagrid();
            //else if (Cable_No == 2)
            //    Update_Datagrid();
            //else if (Cable_No == 3)
            //    Update_Datagrid_3();
            //else if (Cable_No == 4)
            //    Update_Datagrid_4();
        }

        private void Update_Datagrid()
        {
            List<string> list = new List<string>();
            List<string> output = new List<string>();


            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("Support 1"));
            list.Add(string.Format("Face of Diaphragm near Support 1"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L1/10"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("2 L1/10"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("3 L1/10"));
            list.Add(string.Format("4 L1/10"));
            list.Add(string.Format("Start of parabola in elevation"));
            list.Add(string.Format("5 L1/10"));
            list.Add(string.Format("Start of pareabola in elevation"));
            list.Add(string.Format("6 L1/10"));
            list.Add(string.Format("7 L1/10"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("8 L1/10"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("Start of second parabola in Plan"));
            list.Add(string.Format("9 L1/10"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("Dead End / Face of Diaphragm near support 2"));

            List<double> lst_x = new List<double>();
            List<double> lst_Y_Ord = new List<double>();
            List<double> lst_Incln_Elevn = new List<double>();
            List<double> lst_theta_1 = new List<double>();
            List<double> lst_Z_Ord = new List<double>();
            List<double> lst_Incln_Plan = new List<double>();
            List<double> lst_theta_2 = new List<double>();
            List<double> lst_theta = new List<double>();
            List<double> lst_mu_theta = new List<double>();
            List<double> lst_Kx = new List<double>();
            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();

            int i = 0;


            Set_User_Input();

            output.Add(string.Format(""));

            lst_x.Add(CBGD.End_Dist_Supp[0] - CBGD.End_Dist_Supp[0]);
            lst_x.Add(CBGD.End_Dist_Supp[1]);
            lst_x.Add(CBGD.End_Dist_Supp[2]);
            lst_x.Add(PD.Profile_1_L[0]);
            lst_x.Add(CBGD.End_Dist_Supp[3]);

            lst_x.Add(PD.Profile_2_L[17]);
            lst_x.Add(PD.Profile_2_L[17] + PD.Profile_2_L[13]);
            lst_x.Add(CBGD.End_Dist_Supp[4]);

            lst_x.Add(lst_x[6] + PD.Profile_2_L[14]);
            lst_x.Add(CBGD.End_Dist_Supp[5]);
            lst_x.Add(CBGD.End_Dist_Supp[6]);

            lst_x.Add(PD.Profile_2_L[1] - PD.Profile_1_L[2] / 2);

            lst_x.Add(CBGD.End_Dist_Supp[7]);
            lst_x.Add(PD.Profile_2_L[1] + PD.Profile_1_L[2] / 2);


            lst_x.Add(CBGD.End_Dist_Supp[8]);
            lst_x.Add(CBGD.End_Dist_Supp[9]);


            double D268 = CBGD.End_Dist_Supp[12];
            double D267 = D268 - PD.Profile_1_L[4];

            double D265 = D268 - PD.Profile_2_L[11];


            double D264 = D265 - PD.Profile_2_L[16];


            lst_x.Add(D264 - PD.Profile_2_L[15]);
            lst_x.Add(CBGD.End_Dist_Supp[10]);
            lst_x.Add(D264);
            lst_x.Add(D265);
            lst_x.Add(CBGD.End_Dist_Supp[11]);
            lst_x.Add(D267);
            lst_x.Add(D268);



            output.Add(string.Format(""));
            lst_Y_Ord.Clear();

            lst_Y_Ord.Add(PD.Profile_1_D[2] + PD.Profile_1_D[3]);


            double E249 = PD.Profile_1_D[1] + PD.Profile_1_D[2];

            //=E249+((E246-E249)*(D249-D247)/(D249-D246))
            double E247 = E249 + ((lst_Y_Ord[0] - E249) * (lst_x[3] - lst_x[1]) / (lst_x[3] - lst_x[0]));
            lst_Y_Ord.Add(E247);

            double E248 = E249 + ((lst_Y_Ord[0] - E249) * (lst_x[3] - lst_x[2]) / (lst_x[3] - lst_x[0]));
            lst_Y_Ord.Add(E248);
            lst_Y_Ord.Add(E249);



            double E257 = PD.Profile_1_D[2];

            double val = 0.0;

            //val = E257 + (((E249 - E257) / (D257 - D249) ^ 2) * (D257 - D250) ^ 2);

            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[4]), 2));
            lst_Y_Ord.Add(val);


            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[5]), 2));
            lst_Y_Ord.Add(val);


            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[6]), 2));
            lst_Y_Ord.Add(val);



            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[7]), 2));
            lst_Y_Ord.Add(val);


            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[8]), 2));
            lst_Y_Ord.Add(val);



            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[9]), 2));
            lst_Y_Ord.Add(val);


            val = E257 + (((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * Math.Pow((lst_x[11] - lst_x[10]), 2));
            lst_Y_Ord.Add(val);

            lst_Y_Ord.Add(E257);
            lst_Y_Ord.Add(E257);
            lst_Y_Ord.Add(E257);

            double E267 = E249;

            //val = E259 + (((E267 - E259) / (D267 - D259) ^ 2) * (D260 - D259) ^ 2);
            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[14] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);



            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[15] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);


            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[16] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);


            //1.619
            //1.795
            //1.859
            //2.154
            //2.180
            //2.628
            //2.697


            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[17] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);



            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[18] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);


            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[19] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);

            val = E257 + (((E267 - E257) / Math.Pow((D267 - lst_x[13]), 2)) * Math.Pow((lst_x[20] - lst_x[13]), 2));

            lst_Y_Ord.Add(val);



            lst_Y_Ord.Add(E267);
            lst_Y_Ord.Add(lst_Y_Ord[0]);



            lst_Incln_Elevn.Clear();

            //val = ATAN((E246 - E249) / (D249 - D246));

            val = Math.Atan((lst_Y_Ord[0] - E249) / (lst_x[3] - lst_x[0]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan((lst_Y_Ord[1] - E249) / (lst_x[3] - lst_x[1]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan((lst_Y_Ord[2] - E249) / (lst_x[3] - lst_x[2]));
            lst_Incln_Elevn.Add(val);



            //=ATAN(2*(($E$249-$E$257)/($D$257-$D$249)^2)*($D$257-D249))
            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[3]));
            lst_Incln_Elevn.Add(val);



            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[4]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[5]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[6]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[7]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[8]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[9]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[10]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E249 - E257) / Math.Pow((lst_x[11] - lst_x[3]), 2)) * (lst_x[11] - lst_x[11]));
            lst_Incln_Elevn.Add(val);

            lst_Incln_Elevn.Add(val);

            //=ATAN(2*((E267-E259)/(D267-D259)^2)*(D259-D259))

            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[13] - lst_x[13]));
            lst_Incln_Elevn.Add(val);


            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[14] - lst_x[13]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[15] - lst_x[13]));
            lst_Incln_Elevn.Add(val);

            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[16] - lst_x[13]));
            lst_Incln_Elevn.Add(val);


            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[17] - lst_x[13]));
            lst_Incln_Elevn.Add(val);



            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[18] - lst_x[13]));
            lst_Incln_Elevn.Add(val);



            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[19] - lst_x[13]));
            lst_Incln_Elevn.Add(val);




            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[20] - lst_x[13]));
            lst_Incln_Elevn.Add(val);


            val = Math.Atan(2 * ((E267 - lst_Y_Ord[13]) / Math.Pow((D267 - lst_x[13]), 2)) * (lst_x[21] - lst_x[13]));
            lst_Incln_Elevn.Add(val);

            //=ATAN((E268-E267)/(D268-D267))

            val = Math.Atan((lst_Y_Ord[22] - E267) / (D268 - D267));
            lst_Incln_Elevn.Add(val);

            //0.274
            //0.285
            //0.324
            //0.327
            //0.369
            //0.374
            //0.400
            //0.400

            lst_theta_1.Clear();


            for (i = 0; i < list.Count; i++)
            {
                if (i < 14)
                {
                    val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                }
                else
                {
                    //=$G$259+(F260-$F$259)
                    val = lst_theta_1[13] + lst_Incln_Elevn[i] - lst_Incln_Elevn[13];

                }
                lst_theta_1.Add(val);
            }




            double H251 = CS.CS_B7 / 1000 - PD.Profile_2_D[1];
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);

            double H252 = H251 + PD.Profile_2_D[2];
            lst_Z_Ord.Add(H252);


            double H254 = H252 + PD.Profile_2_D[3];

            //=H252+(((H254-H252)/(D254-D252)^2)*(D254-D253)^2)

            val = H252 + (((H254 - H252) / Math.Pow((lst_x[8] - lst_x[6]), 2)) * Math.Pow((lst_x[8] - lst_x[7]), 2));
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);
            lst_Z_Ord.Add(H254);

            //val =H264+(((H262-H264)/(D264-D262)^2)*(D263-D262)^2)
            val = H252 + (((H254 - H252) / Math.Pow((D264 - lst_x[16]), 2)) * Math.Pow((lst_x[17] - lst_x[16]), 2));
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(H252);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);
            lst_Z_Ord.Add(H251);


            lst_Incln_Plan.Add(0.0);
            lst_Incln_Plan.Add(0.0);
            lst_Incln_Plan.Add(0.0);
            lst_Incln_Plan.Add(0.0);
            lst_Incln_Plan.Add(0.0);
            lst_Incln_Plan.Add(0.0);

            //val = Math.Atan(2*((H602-H600)/(D602-D600)^2)*(D602-D600))
            val = Math.Atan(2 * ((lst_Z_Ord[8] - lst_Z_Ord[6]) / Math.Pow((lst_x[8] - lst_x[6]), 2)) * (lst_x[8] - lst_x[6]));

            lst_Incln_Plan.Add(val);


            //val = Math.Atan(2*((H602-H600)/(D602-D600)^2)*(D602-D600))
            val = Math.Atan(2 * ((lst_Z_Ord[8] - lst_Z_Ord[6]) / Math.Pow((lst_x[8] - lst_x[6]), 2)) * (lst_x[8] - lst_x[7]));

            lst_Incln_Plan.Add(val);

            //val = Math.Atan(2*((H602-H600)/(D602-D600)^2)*(D602-D600))
            val = Math.Atan(2 * ((lst_Z_Ord[8] - lst_Z_Ord[6]) / Math.Pow((lst_x[8] - lst_x[6]), 2)) * (lst_x[8] - lst_x[8]));

            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);



            //=ATAN(2*((H262-H264)/(D264-D262)^2)*(D262-$D$262))
            double I262 = Math.Atan(2 * ((lst_Z_Ord[16] - lst_Z_Ord[18]) / Math.Pow((lst_x[18] - lst_x[16]), 2)) * (lst_x[18] - lst_x[18]));
            lst_Incln_Plan.Add(I262);
            lst_Incln_Plan.Add(I262);
            lst_Incln_Plan.Add(I262);
            lst_Incln_Plan.Add(I262);

            val = Math.Atan(2 * ((lst_Z_Ord[16] - lst_Z_Ord[18]) / Math.Pow((lst_x[18] - lst_x[16]), 2)) * (lst_x[19] - lst_x[18]));
            lst_Incln_Plan.Add(val);

            val = Math.Atan(2 * ((lst_Z_Ord[16] - lst_Z_Ord[18]) / Math.Pow((lst_x[18] - lst_x[16]), 2)) * (lst_x[20] - lst_x[18]));
            lst_Incln_Plan.Add(val);
            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);


            lst_theta_2.Clear();


            for (i = 0; i < list.Count; i++)
            {
                if (i < 7)
                {
                    val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
                }
                else if (i < 9)
                {
                    //=$G$259+(F260-$F$259)
                    val = lst_theta_2[6] + lst_Incln_Plan[6] - lst_Incln_Plan[i];

                }
                else if (i < 17)
                {
                    val = lst_theta_2[8];

                }
                else if (i < 19)
                {
                    //=J262+(I263-I262)
                    val = lst_theta_2[16] + lst_Incln_Plan[i] - lst_Incln_Plan[16];

                }
                else if (i < 20)
                {
                    //=J264+(I264-I265)
                    val = lst_theta_2[18] + lst_Incln_Plan[18] - lst_Incln_Plan[i];

                }
                else
                {
                    //=J264+(I264-I265)
                    val = lst_theta_2[19];

                }
                lst_theta_2.Add(val);
            }

            for (i = 0; i < list.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);


                lst_mu_theta.Add(val * CBGD.Mu);

                lst_Kx.Add(lst_x[i] * CBGD.k);
            }


            double F = (CBGD.UTS * CBGD.Tendon_Area / 1000) * (CBGD.End_Box_Pj / 100) / 9.80665;


            for (i = 0; i < lst_x.Count; i++)
            {
                val = F * Math.Pow(Math.E, (-(lst_mu_theta[i] + lst_Kx[i])));
                lst_F.Add(val);
                lst_Fh.Add(val * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv.Add(val * Math.Sin(lst_Incln_Elevn[i]));
            }




            for (i = 0; i < list.Count; i++)
            {
                dgv_friction_loss.Rows.Add(list[i],
                    lst_x[i],
                    lst_Y_Ord[i],
                    lst_Incln_Elevn[i],
                    lst_theta_1[i],
                    lst_Z_Ord[i],
                    lst_Incln_Plan[i],
                    lst_theta_2[i],
                    lst_theta[i],
                    lst_mu_theta[i],
                    lst_Kx[i],
                    lst_F[i],
                    lst_Fh[i],
                    lst_Fv[i]);
            }



            for (i = 0; i < dgv_friction_loss.RowCount; i++)
            {

                for (int j = 1; j < dgv_friction_loss.ColumnCount; j++)
                {
                    if (j > 1 && j < 11)
                    {
                        val = MyList.StringToDouble(dgv_friction_loss[j, i].Value.ToString(), 0.0);
                        dgv_friction_loss[j, i].Value = val.ToString("f4");
                    }
                    else
                    {
                        val = MyList.StringToDouble(dgv_friction_loss[j, i].Value.ToString(), 0.0);
                        dgv_friction_loss[j, i].Value = val.ToString("f2");
                    }
                }
            }

            output.Clear();
            output.Add(string.Format(""));
            //Weighted average force
            List<double> lst_avg_frc = new List<double>();

            output.Add(string.Format("Weighted average force = (1 / {0:f3})", lst_x[lst_x.Count - 1]));

            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);

                if (i == 1)
                    output.Add(string.Format("                              x  ((({0:f3} + {1:f3}) / 2) x {2:f3}", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));
                else if (i == list.Count - 1)
                    output.Add(string.Format("                              +  (({0:f3} + {1:f3}) / 2) x {2:f3}))", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));
                else
                    output.Add(string.Format("                              +  (({0:f3} + {1:f3}) / 2) x {2:f3})", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));

                lst_avg_frc.Add(val);
            }

            output.Add(string.Format(""));
            output.Add(string.Format(""));


            val = 1 / lst_x[lst_x.Count - 1];

            output.Add(string.Format("Weighted average force =   {0:f3} x ({1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                                                                val, lst_avg_frc[0], lst_avg_frc[1], lst_avg_frc[2], lst_avg_frc[3], lst_avg_frc[4], lst_avg_frc[5]));


            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                lst_avg_frc[6], lst_avg_frc[7], lst_avg_frc[8], lst_avg_frc[9], lst_avg_frc[10], lst_avg_frc[11]));

            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                         lst_avg_frc[12], lst_avg_frc[13], lst_avg_frc[14], lst_avg_frc[15], lst_avg_frc[16], lst_avg_frc[17]));


            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3})",
                                                                         lst_avg_frc[18], lst_avg_frc[19], lst_avg_frc[20], lst_avg_frc[21]));



            //val = 0.91;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);
            output.Add(string.Format(""));
            output.Add(string.Format("                    P  =   {0:f3} T", P));
            output.Add(string.Format(""));
            output.Add(string.Format(""));





            double len_parabola = val;

            //=(D267-D259)*(1+(2*(E267-E259)^2/(3*(D267-D259)^2)))
            //val = (D267 - D259) * (1 + (2 * (E267 - E259) ^ 2 / (3 * (D267 - D259) ^ 2)));
            
            double x = lst_x[11] - lst_x[3];
            double Y = lst_Y_Ord[3] - lst_Y_Ord[11];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));

            //output.Add(string.Format("Length of first Parabola in elevation = X (1 + 2 Y^2/ 3 X^2 )= 14.398 m"));


            output.Add(string.Format(""));
            output.Add(string.Format(" X = {0:f3} - {1:f3} = {2:f3}", lst_x[11], lst_x[3], x));
            output.Add(string.Format(" Y = {0:f3} - {1:f3} = {2:f3}", lst_Y_Ord[3], lst_Y_Ord[11], Y));
            output.Add(string.Format(""));

            output.Add(string.Format("Length of first Parabola in elevation = X (1 + 2 Y^2 / 3 * X^2 )"));
            output.Add(string.Format(""));
            output.Add(string.Format("                                      = {0:f3} * (1 + 2 {1:f3}^2 / 3 * {0:f3}^2 )", x, Y));
            output.Add(string.Format("                     len_parabola     = {0:f3} m", len_parabola));
            output.Add(string.Format(""));


            //=(D267-D259)*(1+(2*(E267-E259)^2/(3*(D267-D259)^2)))
            x = lst_x[21] - lst_x[13];
            Y = lst_Y_Ord[21] - lst_Y_Ord[13];

            output.Add(string.Format(""));
            output.Add(string.Format(" X = {0:f3} - {1:f3} = {2:f3}", lst_x[21], lst_x[13], x));
            output.Add(string.Format(" Y = {0:f3} - {1:f3} = {2:f3}", lst_Y_Ord[21], lst_Y_Ord[13], Y));
            output.Add(string.Format(""));

            double len_parabola2 = x * (1 + 2 * Y * Y / (3 * x * x));


            output.Add(string.Format("Length of second Parabola in elevation  = X (1 + 2 Y^2/ 3 X^2 )"));
            output.Add(string.Format(""));
            output.Add(string.Format("                                        = {0:f3} * (1 + 2 {1:f3}^2 / 3 * {0:f3}^2 )", x, Y));
            output.Add(string.Format("                       len_parabola2    = {0:f3} m", len_parabola2));
            output.Add(string.Format(""));


            //=(2*(D254-D252)*(2*(H254-H252)^2/(3*(D254-D252)^2)))*2
            x = lst_x[8] - lst_x[6];
            Y = lst_Z_Ord[8] - lst_Z_Ord[6];

            output.Add(string.Format(""));
            output.Add(string.Format(" X = {0:f3} - {1:f3} = {2:f3}", lst_x[21], lst_x[13], x));
            output.Add(string.Format(" Y = {0:f3} - {1:f3} = {2:f3}", lst_Y_Ord[21], lst_Y_Ord[13], Y));
            output.Add(string.Format(""));


            //=(2*(D254-D252)*(2*(H254-H252)^2/(3*(D254-D252)^2)))*2

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));

            output.Add(string.Format(""));
            output.Add(string.Format("Additional Length of Parabola in plan  = 2*X ( 2 Y^2/ 3 X^2 )"));
            output.Add(string.Format("                                       = 2*{0:f3} ( 2 {1:f3}^2/ 3 * {0:f3}^2 )", x, Y));
            output.Add(string.Format("                   len_add_parabola    = {0:f3} m", len_add_parabola));
            output.Add(string.Format(""));
            output.Add(string.Format(""));

            //=(SQRT((D249-D246)^2+(E246-E249)^2))*2
            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[3] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[0] - lst_Y_Ord[3], 2)))) * 2;

            output.Add(string.Format(""));
            output.Add(string.Format("Length of Inclined Straight Line = SQRT(({0:f3} - {1:f3}) + ({2:f3} - {3:f3})) * 2", lst_x[3], lst_x[0], lst_Y_Ord[0], lst_Y_Ord[3]));
            output.Add(string.Format(""));
            output.Add(string.Format("                      len_inclnd =  {0:f3} m", len_inclnd));
            output.Add(string.Format(""));
            output.Add(string.Format(""));



            double len_cable = len_parabola + len_parabola2 + len_add_parabola + len_inclnd + PD.Profile_1_L[2];


            output.Add(string.Format("Total cable length = len_parabola + len_add_parabola + len_inclnd + len_inclnd"));
            output.Add(string.Format(""));
            output.Add(string.Format("                   = {0:f3} + {1:f3} + {2:f3} + {3:f3}", len_parabola, len_parabola2, len_add_parabola, len_inclnd, PD.Profile_1_L[2]));
            output.Add(string.Format("       len_cable   = {0:f3} m", len_cable));
            output.Add(string.Format(""));
            output.Add(string.Format(""));

            double extention_end = P * 9.80665 * len_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);

            output.Add(string.Format(""));
            output.Add(string.Format("Extention at each end = (P x len_cable x 10^6)/(A x Es)"));
            output.Add(string.Format(""));
            output.Add(string.Format("                      = (({0:f3} x 9.80665) x {1:f3} x 10^6)/({2:f3} x {3:f3})", P, len_cable, CBGD.Tendon_Area, CBGD.Es));
            output.Add(string.Format("       extention_end  = {0:f3} mm", P, len_cable, CBGD.Tendon_Area, CBGD.Es));
            output.Add(string.Format(""));
            double S = CBGD.S;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;

            output.Add(string.Format("Loss due slip = Es * Tendon_Area * S / 2"));
            output.Add(string.Format("              = {0:f3} * {1:f3} * {2} / 2", CBGD.Es, CBGD.Tendon_Area, S));
            output.Add(string.Format("              = {0:f3} N-mm", loss_due_slip));
            output.Add(string.Format(""));
            output.Add(string.Format("              = {0:f3} / 9.80665 / 1000 / 1000", loss_due_slip));


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;

            output.Add(string.Format(""));
            output.Add(string.Format("              = {0:f3} T-m", loss_due_slip));
            output.Add(string.Format(""));
            output.Add(string.Format(""));
            output.Add(string.Format("Let the Slip travels upto a distance 'Ls' beyond point 2 L1 / 10"));
            output.Add(string.Format(""));
            
            double drop_force = (lst_F[6] - lst_F[8]) / (lst_x[8] - lst_x[6]);
            output.Add(string.Format(""));
            output.Add(string.Format("Drop in Force  from 2 L1 / 10 to  Null point  =  (({0:f3} - {1:f3})/({2:f3} - {3:f3})) x Ls", lst_F[6], lst_F[8], lst_x[8], lst_x[6]));
            output.Add(string.Format(""));
            output.Add(string.Format("                                              =  {0:f3} x Ls", drop_force));

            output.Add(string.Format(""));
            output.Add(string.Format("Half of area of shaded portion"));

            List<double> lst_half_area = new List<double>();
            List<double> lst_dsa = new List<double>();

//0.350
//0.500
//0.150
//2.950
//0.400
//3.000
//0.200


            for (i = 1; i < 8; i++)
            {
                output.Add(string.Format("(({0:f3}- {1:f3}) / 2.0) x ({2:f3} - {3:f3}) + ", lst_F[i - 1], lst_F[i], lst_x[i], lst_x[i - 1]));

                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_dsa.Add((lst_x[i] - lst_x[i - 1]));
                lst_half_area.Add(val);
            }
            output.Add(string.Format(""));
            output.Add(string.Format("{0:f3}  x  LsxLs/2 +", drop_force));
            output.Add(string.Format(""));
            lst_half_area.Add(val);



            for (i = 2; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                output.Add(string.Format("({0:f3} - {1:f3}) x {2:f3} +", lst_F[i - 1], lst_F[i], lst_x[i - 1]));
                lst_half_area.Add(val);
            }

            output.Add(string.Format("{0:f3} x Ls x {1:f3}", drop_force, lst_x[7]));


            val = drop_force / 2;
            double val2 = drop_force * lst_x[7];

            output.Add(string.Format(""));
            output.Add(string.Format(""));
            output.Add(string.Format("{0:f3} x Ls^2 + {1:f3} Ls + {2:f3} + {3:f3} + {4:f3} + {5:f3} +", val, val2, lst_half_area[0], lst_half_area[1], lst_half_area[2], lst_half_area[3]));
            output.Add(string.Format("{0:f3} + {1:f3} Ls + {2:f3} + {3:f3} + {4:f3} + {5:f3} +", lst_half_area[4], lst_half_area[5], lst_half_area[6], lst_half_area[7], lst_half_area[8], lst_half_area[9]));
            output.Add(string.Format("{0:f3} + {1:f3} Ls + {2:f3} + {3:f3}", lst_half_area[10], lst_half_area[11], lst_half_area[12], lst_half_area[13]));
            output.Add(string.Format(""));

            double a = MyList.Get_Array_Sum(lst_half_area);


            output.Add(string.Format(""));
            output.Add(string.Format("from above equation,"));
            output.Add(string.Format(""));
            output.Add(string.Format("=> {0:f3} x Ls^2 + {1:f3} x Ls + {2:f3} = {3:f3}", val, val2, a, loss_due_slip));
            output.Add(string.Format(""));

            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;
            output.Add(string.Format("=> {0} x Ls^2 + {1:f3} x Ls + ({2:f3}) = 0", a, b, c));
            output.Add(string.Format(""));

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));
            output.Add(string.Format(""));
            output.Add(string.Format("Ls = ((-({0:f3}) + SQRT(({0:f3}^2 - 4 * {1:f3} * {2:f3}))) / (2 * {1:f3}))", b, a, c));
            output.Add(string.Format(""));
            output.Add(string.Format("   = {0:f3} m", Ls));
            output.Add(string.Format(""));

            double drop_in_frc = drop_force * Ls;
            output.Add(string.Format(""));
            output.Add(string.Format("Drop in Force  from 2L1 / 10 to  Null point  = drop_force x Ls = {0:f3} x {1:f3} = {2:f3} T", drop_force, Ls, drop_in_frc));
            output.Add(string.Format(""));


            double frc_at_null_point = lst_F[7] - (drop_force * Ls);
            output.Add(string.Format("Therefore Force at Null Point  = {0:f3} - (drop_force * Ls)  = {0:f3} - ({1:f3} * {2:f3}) = {3:f3} T", lst_F[7], drop_force, Ls, frc_at_null_point));
            val = Ls;

            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                if (i < 8)
                    val = lst_F[i] - 2 * (lst_F[i] - frc_at_null_point);
                else
                    val = lst_F[i];

                lst_F2.Add(val);

                lst_Fh2.Add(val * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv2.Add(val * Math.Sin(lst_Incln_Elevn[i]));
            }

            for (i = 0; i < list.Count; i++)
            {
                dgv_slip_loss.Rows.Add(list[i],
                    lst_x[i],
                    lst_Y_Ord[i],
                    lst_Incln_Elevn[i],
                    lst_F2[i],
                    lst_Fh2[i],
                    lst_Fv2[i]);
            }


            for (i = 0; i < dgv_slip_loss.RowCount; i++)
            {

                for (int j = 1; j < dgv_slip_loss.ColumnCount; j++)
                {
                    if (j > 1 && j < 4)
                    {
                        val = MyList.StringToDouble(dgv_slip_loss[j, i].Value.ToString(), 0.0);
                        dgv_slip_loss[j, i].Value = val.ToString("f4");
                    }
                    else
                    {
                        val = MyList.StringToDouble(dgv_slip_loss[j, i].Value.ToString(), 0.0);
                        dgv_slip_loss[j, i].Value = val.ToString("f2");
                    }
                }
            }

            lst_avg_frc.Clear();


            Calculation_Details_Friction = new List<string>(output.ToArray());
            output.Clear();

            output.Add(string.Format("Weighted average force = (1 / {0:f3})", lst_x[lst_x.Count - 1]));

            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F2[i - 1] + lst_F2[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);

                if (i == 1)
                    output.Add(string.Format("                             x  ((({0:f3} + {1:f3}) / 2) x {2:f3}", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));
                else if (i == list.Count - 1)
                    output.Add(string.Format("                            +  (({0:f3} + {1:f3}) / 2) x {2:f3}", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));
                else
                    output.Add(string.Format("                            +  (({0:f3} + {1:f3}) / 2) x {2:f3})", lst_F[i - 1], lst_F[i], lst_x[i] - lst_x[i - 1]));

                lst_avg_frc.Add(val);
            }

            output.Add(string.Format(""));
            output.Add(string.Format(""));

            val = 1 / lst_x[lst_x.Count - 1];

            output.Add(string.Format("Weighted average force =   {0:f3} x ({1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                                                                val, lst_avg_frc[0], lst_avg_frc[1], lst_avg_frc[2], lst_avg_frc[3], lst_avg_frc[4], lst_avg_frc[5]));


            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                lst_avg_frc[6], lst_avg_frc[7], lst_avg_frc[8], lst_avg_frc[9], lst_avg_frc[10], lst_avg_frc[11]));

            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                         lst_avg_frc[12], lst_avg_frc[13], lst_avg_frc[14], lst_avg_frc[15], lst_avg_frc[16], lst_avg_frc[17]));


            output.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3})",
                                                                         lst_avg_frc[18], lst_avg_frc[19], lst_avg_frc[20], lst_avg_frc[21]));


            //val = 0.91;
            P = val * MyList.Get_Array_Sum(lst_avg_frc);
            output.Add(string.Format(""));
            output.Add(string.Format("                    P  =   {0:f3} T", P));
            output.Add(string.Format(""));
            output.Add(string.Format(""));






            Calculation_Details_Slip = new List<string>(output.ToArray());

        }


        private void btn_Process_Click(object sender, EventArgs e)
        {
            Set_Results();

            PD.Table_Friction_Loss.Data_Retrieve_from_Grid(dgv_friction_loss);
            PD.Table_Friction_Slip_Loss.Data_Retrieve_from_Grid(dgv_slip_loss);
            this.Close();

        }

        private void Set_Results()
        {
            string format = "{0,-43} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f2} {13,9:f2}";

            int column = 0;
            Results = new List<string>();
            Results.Add(string.Format(""));
            Results.Add(string.Format("--------------------------------------------------------------------------"));
            Results.Add(string.Format(lbl_cbl.Text.Replace("&&", "&")));
            Results.Add(string.Format("--------------------------------------------------------------------------"));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION LOSS"));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                     Dist from    Y Ord     Incln      θ1       Z Ord    Incln      θ2       θ=θ1+θ2     µθ        Kx        F        Fh         Fv"));
            Results.Add(string.Format("                                            Jacking,x              Elevn.                       Plan                                                                            "));
            Results.Add(string.Format("                                               (m)        (m)       (rad)    (rad)       (m)    (rad)     (rad)       (rad)               (m)      (T)       (T)        (T)"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            for (int i = 0; i < dgv_friction_loss.RowCount; i++)
            {
                column = 0;
                Results.Add(string.Format(format,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value,
                    dgv_friction_loss[column++, i].Value));
            }
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));


            Results.AddRange(Calculation_Details_Friction.ToArray());
            format = "{0,-43} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f2} {5,9:f2} {6,9:f2}";

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION AND SLIP LOSS"));
            Results.Add(string.Format("--------------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                     Dist from    Y Ord     Incln      F        Fh         Fv"));
            Results.Add(string.Format("                                            Jacking,x              Elevn.                           "));
            Results.Add(string.Format("                                               (m)        (m)       (rad)    (T)       (T)        (T)"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            for (int i = 0; i < dgv_slip_loss.RowCount; i++)
            {
                column = 0;

                Results.Add(string.Format(format,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value));
            }
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.AddRange(Calculation_Details_Slip.ToArray());
        }
    }

    public class End_Span_Cable_Profile_Data
    {

        #region Propeties

        /// <summary>
        /// Number of Cables
        /// </summary>
        public List<int> NC { get; set; }

        public List<double> Profile_1_L { get; set; }
        public List<double> Profile_1_D { get; set; }

        public List<double> Profile_2_L { get; set; }
        public List<double> Profile_2_D { get; set; }

        #endregion Propeties

        public Force_After_Friction_Loss_Table Table_Friction_Loss { get; set; }
        public Force_After_Friction_Slip_Loss_Table Table_Friction_Slip_Loss { get; set; }

        public End_Span_Cable_Profile_Data()
        {

            NC = new List<int>();
            Profile_1_L = new List<double>();
            Profile_1_D = new List<double>();
            Profile_2_L = new List<double>();
            Profile_2_D = new List<double>();


            Table_Friction_Loss = new Force_After_Friction_Loss_Table();
            Table_Friction_Slip_Loss = new Force_After_Friction_Slip_Loss_Table();
        }
    }

}
