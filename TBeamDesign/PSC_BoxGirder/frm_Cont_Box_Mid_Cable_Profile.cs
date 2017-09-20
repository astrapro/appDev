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
    public partial class frm_Cont_Box_Mid_Cable_Profile : Form
    {
        Continuous_Box_Girder_Design CBGD;
        Cross_Section_Box_Girder CS;
        public Mid_Span_Cable_Profile_Data PD { get; set; }
        public List<string> Results { get; set; }
        int Cable_No = 1;
        public frm_Cont_Box_Mid_Cable_Profile(Continuous_Box_Girder_Design cnt, Cross_Section_Box_Girder _CS, int cable_no)
        {
            CBGD = cnt;
            CS = _CS;
            InitializeComponent();
            Cable_No = cable_no;
            if (cable_no == 7)
                lbl_cbl.Text = "CABLE NO. 7 && 9";
            else if (cable_no == 8)
                lbl_cbl.Text = "CABLE NO. 8 && 10";
            else    
                lbl_cbl.Text = "CABLE NO. " + cable_no;

            btn_Process.Text = string.Format("PROCESS {0} of 8", cable_no);

            Default_Input_Data(Cable_No);

        }
        public void Set_User_Input()
        {
            PD = new Mid_Span_Cable_Profile_Data();


            PD.Profile_1_L1 = MyList.StringToDouble(txt_cab_pro1_B1.Text, 0.0);
            PD.Profile_1_L2 = MyList.StringToDouble(txt_cab_pro1_B2.Text, 0.0);
            PD.Profile_1_L3 = MyList.StringToDouble(txt_cab_pro1_B3.Text, 0.0);
            PD.Profile_1_L4 = MyList.StringToDouble(txt_cab_pro1_B4.Text, 0.0);
            PD.Profile_1_D1 = MyList.StringToDouble(txt_cab_pro1_D1.Text, 0.0);
            PD.Profile_1_D2 = MyList.StringToDouble(txt_cab_pro1_D2.Text, 0.0);
            PD.Profile_1_D3 = MyList.StringToDouble(txt_cab_pro1_D3.Text, 0.0);
            PD.Profile_1_D4 = MyList.StringToDouble(txt_cab_pro1_D4.Text, 0.0);

            PD.Profile_2_L1 = MyList.StringToDouble(txt_cab_pro2_B1.Text, 0.0);
            PD.Profile_2_L2 = MyList.StringToDouble(txt_cab_pro2_B2.Text, 0.0);
            PD.Profile_2_L3 = MyList.StringToDouble(txt_cab_pro2_B3.Text, 0.0);
            PD.Profile_2_L4 = MyList.StringToDouble(txt_cab_pro2_B4.Text, 0.0);
            PD.Profile_2_L5 = MyList.StringToDouble(txt_cab_pro2_B5.Text, 0.0);
            PD.Profile_2_L6 = MyList.StringToDouble(txt_cab_pro2_B6.Text, 0.0);


            PD.Profile_2_D1 = MyList.StringToDouble(txt_cab_pro2_D1.Text, 0.0);
            PD.Profile_2_D2 = MyList.StringToDouble(txt_cab_pro2_D2.Text, 0.0);
            PD.Profile_2_D3 = MyList.StringToDouble(txt_cab_pro2_D3.Text, 0.0);
            PD.Profile_2_D4 = MyList.StringToDouble(txt_cab_pro2_D4.Text, 0.0);

            //PD.NC = MyList.StringToInt(txt_Nc_0.Text, 2);
            PD.NC.Clear();
            PD.NC.Add(MyList.StringToInt(txt_Nc_0.Text, 2));
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
                    txt_cab_pro1_B2.Text = "32.0";
                    txt_cab_pro1_B3.Text = "2.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.17";
                    txt_cab_pro1_D2.Text = "2.75";
                    txt_cab_pro1_D3.Text = "0.325";
                    txt_cab_pro1_D4.Text = "2.925";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.150";
                    #endregion Cable 1
                    break;
                case 2:
                    #region Cable 2
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "32.0";
                    txt_cab_pro1_B3.Text = "2.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.16";
                    txt_cab_pro1_D2.Text = "2.52";
                    txt_cab_pro1_D3.Text = "0.125";
                    txt_cab_pro1_D4.Text = "2.675";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.150";
                    #endregion Cable 2
                    break;
                case 3:
                    #region Cable 3
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "16.1";
                    txt_cab_pro1_B3.Text = "18.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.22";
                    txt_cab_pro1_D2.Text = "1.80";
                    txt_cab_pro1_D3.Text = "0.325";
                    txt_cab_pro1_D4.Text = "2.025";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.350";
                    #endregion Cable 3
                    break;
                case 4:
                    #region Cable 4
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "16.1";
                    txt_cab_pro1_B3.Text = "18.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.20";
                    txt_cab_pro1_D2.Text = "1.58";
                    txt_cab_pro1_D3.Text = "0.125";
                    txt_cab_pro1_D4.Text = "1.775";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.375";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.350";
                    #endregion Cable 4
                    break;

                case 5:
                    #region Cable 5
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "6.1";
                    txt_cab_pro1_B3.Text = "28.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.28";
                    txt_cab_pro1_D2.Text = "0.847";
                    txt_cab_pro1_D3.Text = "0.325";
                    txt_cab_pro1_D4.Text = "1.125";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.175";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.550";
                    #endregion Cable 5
                    break;

                case 6:
                    #region Cable 6
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "6.1";
                    txt_cab_pro1_B3.Text = "28.0";
                    txt_cab_pro1_B4.Text = "35.1";

                    txt_cab_pro1_D1.Text = "0.22";
                    txt_cab_pro1_D2.Text = "0.660";
                    txt_cab_pro1_D3.Text = "0.125";
                    txt_cab_pro1_D4.Text = "0.875";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.375";
                    txt_cab_pro2_D2.Text = "0.175";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.550";
                    #endregion Cable 6
                    break;
                case 7:
                    #region Cable 7
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "3.0";
                    txt_cab_pro1_B3.Text = "22.0";
                    txt_cab_pro1_B4.Text = "26.0";

                    txt_cab_pro1_D1.Text = "0.170";
                    txt_cab_pro1_D2.Text = "0.260";
                    txt_cab_pro1_D3.Text = "0.125";
                    txt_cab_pro1_D4.Text = "0.425";


                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";


                    txt_cab_pro2_D1.Text = "0.750";
                    txt_cab_pro2_D2.Text = "0.225";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.750";
                    #endregion Cable 7
                    txt_Nc_0.Text = "0";
                    txt_Nc_1.Text = "0";
                    txt_Nc_2.Text = "0";
                    txt_Nc_3.Text = "4";
                    txt_Nc_4.Text = "4";
                    txt_Nc_5.Text = "4";
                    txt_Nc_6.Text = "4";
                    txt_Nc_7.Text = "4";
                    txt_Nc_8.Text = "4";
                    txt_Nc_9.Text = "4";
                    txt_Nc_10.Text = "4";


                    break;

                case 8:
                    #region Cable 8
                    txt_cab_pro1_B1.Text = "1.0";
                    txt_cab_pro1_B2.Text = "3.0";
                    txt_cab_pro1_B3.Text = "18.5";
                    txt_cab_pro1_B4.Text = "22.50";

                    txt_cab_pro1_D1.Text = "0.170";
                    txt_cab_pro1_D2.Text = "0.260";
                    txt_cab_pro1_D3.Text = "0.125";
                    txt_cab_pro1_D4.Text = "0.425";

                    txt_cab_pro2_B1.Text = "4.35";
                    txt_cab_pro2_B2.Text = "6.0";
                    txt_cab_pro2_B3.Text = "6.0";
                    txt_cab_pro2_B4.Text = "4.0";
                    txt_cab_pro2_B5.Text = "14.75";
                    txt_cab_pro2_B6.Text = "35.1";

                    txt_cab_pro2_D1.Text = "0.950";
                    txt_cab_pro2_D2.Text = "0.125";
                    txt_cab_pro2_D3.Text = "0.525";
                    txt_cab_pro2_D4.Text = "0.950";
                    #endregion Cable 8
                    
                    txt_Nc_0.Text = "0";
                    txt_Nc_1.Text = "0";
                    txt_Nc_2.Text = "0";
                    txt_Nc_3.Text = "0";
                    txt_Nc_4.Text = "2";
                    txt_Nc_5.Text = "2";
                    txt_Nc_6.Text = "2";
                    txt_Nc_7.Text = "2";
                    txt_Nc_8.Text = "2";
                    txt_Nc_9.Text = "2";
                    txt_Nc_10.Text = "2";

                    break;
            }
            
        }


        private void frm_Cont_Box_Cable_Profile_Load(object sender, EventArgs e)
        {
            dgv_friction_loss.Rows.Clear();
            dgv_slip_loss.Rows.Clear();

            if (Cable_No == 1)
                Update_Datagrid_1_2();
            else if (Cable_No == 2)
                Update_Datagrid_1_2();
            else if (Cable_No == 3)
                Update_Datagrid_3();
            else if (Cable_No == 4)
                Update_Datagrid_4();
            else if (Cable_No == 5)
                Update_Datagrid_5();
            else if (Cable_No == 6)
                Update_Datagrid_6();
            else if (Cable_No == 7)
                Update_Datagrid_7_9();
            else if (Cable_No == 8)
                Update_Datagrid_8_10();

        }

        private void Update_Datagrid_1_2()
        {
            List<string> output = new List<string>();

            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            lst_x.Add(PD.Profile_1_L1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            val = 4.5 - CBGD.dist_face;
            lst_x.Add(val);

            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[3] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);


            val = lst_x[4] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[5] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);

            val = lst_x[6] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);



            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[13] + CBGD.Mid_cc_supp / 20.0;


            lst_x.Add(val - PD.Profile_1_L3);

            lst_x.Add(val);


            List<double> lst_Y_Ord = new List<double>();



            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3 + PD.Profile_1_D2;
            lst_Y_Ord.Add(val);





            val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[14] - lst_x[1]), 2.0))) * Math.Pow((lst_x[14] - lst_x[2]), 2.0));

            //lst_Y_Ord.Add(val);

            for (i = 2; i <= 13; i++)
            {

                val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[14] - lst_x[1]), 2.0))) * Math.Pow((lst_x[14] - lst_x[i]), 2.0));

                lst_Y_Ord.Add(val);
            }


            val = PD.Profile_1_D3;
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            val = (lst_Y_Ord[0] - lst_Y_Ord[1]) / (lst_x[1] - lst_x[0]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);



            val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[15]) / Math.Pow((lst_x[14] - lst_x[1]), 2.0)) * (lst_x[14] - lst_x[1]);

            val = Math.Atan(val);
            //lst_Incln_Elevn.Add(val);


            for (i = 1; i <= 13; i++)
            {
                val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[15]) / Math.Pow((lst_x[14] - lst_x[1]), 2.0)) * (lst_x[14] - lst_x[i]);

                val = Math.Atan(val);
                lst_Incln_Elevn.Add(val);
            }
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);



            List<double> lst_theta_1 = new List<double>();
            for (i = 0; i < 16; i++)
            {
                val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 - CS.CS_Anc_B4) / 1000.0;
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;


            double H240 = val2 - ((PD.Profile_2_D1 - PD.Profile_2_D4) / 2);

            double H239 = val + (((H240 - val) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));


            lst_Z_Ord.Add(H239);
            lst_Z_Ord.Add(H240);

            val = val2 - (((val2 - H240) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[7] - lst_x[6]), 2));
            lst_Z_Ord.Add(val);

            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);
            lst_Z_Ord.Add(val2);



            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);

            val = (2 * ((lst_Z_Ord[5] - lst_Z_Ord[3]) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * (lst_x[4] - lst_x[3]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            //=ATAN(2*((H240-H238)/(D240-D238)^2)*(D239-D238))
            //=ATAN(2*((H242-H240)/(D242-D240)^2)*(D242-D240))

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[5]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[6]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);



            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 6; i++)
            {
                val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
                lst_theta_2.Add(val);
            }

            for (i = 6; i < list.Count; i++)
            {
                val = lst_Incln_Plan[5] + (lst_Incln_Plan[5] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = (CBGD.UTS * CBGD.Tendon_Area / 1000) * (CBGD.Mid_Box_Pj / 100) / 9.80665;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


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

            output.Add(string.Format("Weighted average force = (1 / {0:f3})", lst_x[lst_x.Count - 1]));
             
            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i-1]);


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

            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = val;

            x = lst_x[14] - lst_x[1];
            double Y = lst_Y_Ord[1] - lst_Y_Ord[14];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[7] - lst_x[5];
            Y = lst_Z_Ord[7] - lst_Z_Ord[5];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            val = len_add_parabola;

            

            double len_half_cable = len_parabola + len_add_parabola + len_inclnd + (lst_x[lst_x.Count - 1] - lst_x[lst_x.Count - 2]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = CBGD.S;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[7] - lst_F[8]) / (lst_x[8] - lst_x[7]);




            List<double> lst_half_area = new List<double>();




            for (i = 1; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 2; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[7];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2/val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;

       
            double frc_at_null_point = lst_F[7] - (drop_force * Ls);
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

        
        }

        private void Update_Datagrid_3()
        {
            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            //list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            lst_x.Add(PD.Profile_1_L1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            val = 4.5 - CBGD.dist_face;
            lst_x.Add(val);

            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[3] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);


            val = lst_x[4] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[5] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);

            val = lst_x[6] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);



            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[13] + CBGD.Mid_cc_supp / 20.0;


            //lst_x.Add(val - PD.Profile_1_B3);

            lst_x.Add(val);


            List<double> lst_Y_Ord = new List<double>();



            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3 + PD.Profile_1_D2;
            lst_Y_Ord.Add(val);





            val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[9] - lst_x[1]), 2.0))) * Math.Pow((lst_x[9] - lst_x[2]), 2.0));

            //lst_Y_Ord.Add(val);

            for (i = 2; i <= 9; i++)
            {

                val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[9] - lst_x[1]), 2.0))) * Math.Pow((lst_x[9] - lst_x[i]), 2.0));

                lst_Y_Ord.Add(val);
            }


            val = PD.Profile_1_D3;
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            val = (lst_Y_Ord[0] - lst_Y_Ord[1]) / (lst_x[1] - lst_x[0]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);



            val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[9] - lst_x[1]), 2.0)) * (lst_x[9] - lst_x[1]);

            val = Math.Atan(val);
            //lst_Incln_Elevn.Add(val);


            for (i = 1; i <= 9; i++)
            {
                val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[9] - lst_x[1]), 2.0)) * (lst_x[9] - lst_x[i]);

                val = Math.Atan(val);
                lst_Incln_Elevn.Add(val);
            }
            //lst_Incln_Elevn.Add(0);
            //lst_Incln_Elevn.Add(0);

            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);

            List<double> lst_theta_1 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 - CS.CS_Anc_B4) / 1000.0;
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;


            double H780 = val;
            double H784 = CS.CS_Anc_B13 / 1000 - PD.Profile_2_D4; ;
            double H782 = (H780 + H784) / 2.0;

            //double H239 = val + (((H240 - val) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((D782 - D780), 2)) * Math.Pow((D781 - D780), 2));
            val = H780 + (((H782 - H780) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            lst_Z_Ord.Add(val);

            lst_Z_Ord.Add(H782);


            //val = H784 - (((H784 - H782) / (D784 - D782) ^ 2) * (D784 - D783) ^ 2);
            val2 = H784 + (((H784 - H782) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[6] - lst_x[5]), 2));
            lst_Z_Ord.Add(val2);

            //lst_Z_Ord.Add(H239);
            //lst_Z_Ord.Add(H240);

            //val = val2 - (((val2 - H240) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[7] - lst_x[6]), 2));
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);




            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);

            val = (2 * ((lst_Z_Ord[5] - lst_Z_Ord[3]) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * (lst_x[4] - lst_x[3]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            //=ATAN(2*((H240-H238)/(D240-D238)^2)*(D239-D238))
            //=ATAN(2*((H242-H240)/(D242-D240)^2)*(D242-D240))

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[5]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[6]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            //lst_Incln_Plan.Add(val);



            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 6; i++)
            {
                val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
                lst_theta_2.Add(val);
            }

            for (i = 6; i < list.Count; i++)
            {
                val = lst_Incln_Plan[5] + (lst_Incln_Plan[5] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


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



            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = val;

            x = lst_x[14] - lst_x[1];
            double Y = lst_Y_Ord[1] - lst_Y_Ord[14];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[7] - lst_x[5];
            Y = lst_Z_Ord[7] - lst_Z_Ord[5];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_add_parabola + len_inclnd + (lst_x[lst_x.Count - 1] - lst_x[lst_x.Count - 2]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[7] - lst_F[8]) / (lst_x[8] - lst_x[7]);




            List<double> lst_half_area = new List<double>();




            for (i = 1; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 2; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[7];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[7] - (drop_force * Ls);
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


        }

        private void Update_Datagrid_4()
        {
            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            //list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            lst_x.Add(PD.Profile_1_L1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            val = 4.5 - CBGD.dist_face;
            lst_x.Add(val);

            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[3] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);


            val = lst_x[4] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[5] + PD.Profile_2_L2 / 2.0;
            lst_x.Add(val);

            val = lst_x[6] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);



            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[13] + CBGD.Mid_cc_supp / 20.0;


            //lst_x.Add(val - PD.Profile_1_B3);

            lst_x.Add(val);


            List<double> lst_Y_Ord = new List<double>();



            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3 + PD.Profile_1_D2;
            lst_Y_Ord.Add(val);





            val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[9] - lst_x[1]), 2.0))) * Math.Pow((lst_x[9] - lst_x[2]), 2.0));

            //lst_Y_Ord.Add(val);

            for (i = 2; i <= 9; i++)
            {

                val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[9] - lst_x[1]), 2.0))) * Math.Pow((lst_x[9] - lst_x[i]), 2.0));

                lst_Y_Ord.Add(val);
            }


            val = PD.Profile_1_D3;
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            val = (lst_Y_Ord[0] - lst_Y_Ord[1]) / (lst_x[1] - lst_x[0]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);



            val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[9] - lst_x[1]), 2.0)) * (lst_x[9] - lst_x[1]);

            val = Math.Atan(val);
            //lst_Incln_Elevn.Add(val);


            for (i = 1; i <= 9; i++)
            {
                val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[9] - lst_x[1]), 2.0)) * (lst_x[9] - lst_x[i]);

                val = Math.Atan(val);
                lst_Incln_Elevn.Add(val);
            }
            //lst_Incln_Elevn.Add(0);
            //lst_Incln_Elevn.Add(0);

            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);

            List<double> lst_theta_1 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                if (val < 0) val = 0.0;  
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 - CS.CS_Anc_B4) / 1000.0;
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;


            double H780 = val;
            double H784 = CS.CS_Anc_B13 / 1000 - PD.Profile_2_D4; ;
            double H782 = (H780 + H784) / 2.0;

            //double H239 = val + (((H240 - val) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((D782 - D780), 2)) * Math.Pow((D781 - D780), 2));
            val = H780 + (((H782 - H780) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            lst_Z_Ord.Add(val);

            lst_Z_Ord.Add(H782);


            //val = H784 - (((H784 - H782) / (D784 - D782) ^ 2) * (D784 - D783) ^ 2);
            val2 = H784 + (((H784 - H782) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[6] - lst_x[5]), 2));
            lst_Z_Ord.Add(val2);

            //lst_Z_Ord.Add(H239);
            //lst_Z_Ord.Add(H240);

            //val = val2 - (((val2 - H240) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[7] - lst_x[6]), 2));
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);




            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);

            val = (2 * ((lst_Z_Ord[5] - lst_Z_Ord[3]) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * (lst_x[4] - lst_x[3]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            //=ATAN(2*((H240-H238)/(D240-D238)^2)*(D239-D238))
            //=ATAN(2*((H242-H240)/(D242-D240)^2)*(D242-D240))

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[5]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = (2 * ((lst_Z_Ord[7] - lst_Z_Ord[5]) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * (lst_x[7] - lst_x[6]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);

            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            //lst_Incln_Plan.Add(val);



            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 6; i++)
            {
                val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
                lst_theta_2.Add(val);
            }

            for (i = 6; i < list.Count; i++)
            {
                val = lst_Incln_Plan[5] + (lst_Incln_Plan[5] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


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



            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = val;

            x = lst_x[14] - lst_x[1];
            double Y = lst_Y_Ord[1] - lst_Y_Ord[14];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[7] - lst_x[5];
            Y = lst_Z_Ord[7] - lst_Z_Ord[5];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_add_parabola + len_inclnd + (lst_x[lst_x.Count - 1] - lst_x[lst_x.Count - 2]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[7] - lst_F[8]) / (lst_x[8] - lst_x[7]);




            List<double> lst_half_area = new List<double>();




            for (i = 1; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 2; i < 8; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[7];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[7] - (drop_force * Ls);
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


        }

        private void Update_Datagrid_5()
        {

            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();

            #region Section

            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            //list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("10 L2/20"));

            #endregion Section

            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            #region Calculaion X

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            lst_x.Add(PD.Profile_1_L1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            val = 4.5 - CBGD.dist_face;
            lst_x.Add(val);


            double tmp = 0.0;

            tmp = PD.Profile_2_L5 + PD.Profile_2_L4 + PD.Profile_2_L3 + PD.Profile_2_L2 - PD.Profile_1_L3;

            val = val + tmp / 2;
            lst_x.Add(val);

 

            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[3] + tmp;
            lst_x.Add(val);

 


            val = lst_x[5] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[7] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);



            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[13] + CBGD.Mid_cc_supp / 20.0;


            //lst_x.Add(val - PD.Profile_1_B3);

            lst_x.Add(val);
            #endregion X


            List<double> lst_Y_Ord = new List<double>();

            #region Y_Ord


            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3 + PD.Profile_1_D2;
            lst_Y_Ord.Add(val);





            val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[6] - lst_x[1]), 2.0))) * Math.Pow((lst_x[6] - lst_x[2]), 2.0));

            //lst_Y_Ord.Add(val);

            for (i = 2; i <= 6; i++)
            {

                val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[6] - lst_x[1]), 2.0))) * Math.Pow((lst_x[6] - lst_x[i]), 2.0));

                lst_Y_Ord.Add(val);
            }


            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            //lst_Y_Ord.Add(val);


            #endregion Y_Ord


            List<double> lst_Incln_Elevn = new List<double>();

            #region Incln_Elevn

            val = (lst_Y_Ord[0] - lst_Y_Ord[1]) / (lst_x[1] - lst_x[0]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);



            val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[6] - lst_x[1]), 2.0)) * (lst_x[6] - lst_x[1]);

            val = Math.Atan(val);
            //lst_Incln_Elevn.Add(val);


            for (i = 1; i <= 6; i++)
            {
                val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[6] - lst_x[1]), 2.0)) * (lst_x[6] - lst_x[i]);

                val = Math.Atan(val);
                lst_Incln_Elevn.Add(val);
            }
            //lst_Incln_Elevn.Add(0);
            //lst_Incln_Elevn.Add(0);

            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            #endregion Incln_Elevn

            List<double> lst_theta_1 = new List<double>();


            #region theta_1

            for (i = 0; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                if (val < 0) val = 0.0;
                lst_theta_1.Add(val);
            }
            #endregion theta_1

            List<double> lst_Z_Ord = new List<double>();
            #region Z_Ord

            val = (CS.CS_Anc_B13 - CS.CS_Anc_B4) / 1000.0;
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;


            double H780 = val;
            double H784 = CS.CS_Anc_B13 / 1000 - PD.Profile_2_D4; ;
            double H782 = (H780 + H784) / 2.0;

            //double H239 = val + (((H240 - val) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((D782 - D780), 2)) * Math.Pow((D781 - D780), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((lst_x[6] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //lst_Z_Ord.Add(val);

            lst_Z_Ord.Add(H782);


            //val = H784 - (((H784 - H782) / (D784 - D782) ^ 2) * (D784 - D783) ^ 2);
            val2 = H784 + (((H782 - H784) / Math.Pow((lst_x[6] - lst_x[4]), 2)) * Math.Pow((lst_x[6] - lst_x[5]), 2));
            lst_Z_Ord.Add(val2);

            //lst_Z_Ord.Add(H239);
            //lst_Z_Ord.Add(H240);

            //val = val2 - (((val2 - H240) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[7] - lst_x[6]), 2));
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);


            #endregion Z_Ord


            List<double> lst_Incln_Plan = new List<double>();

            #region Incln_Plan

            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);

            val = (2 * ((lst_Z_Ord[3] - lst_Z_Ord[4]) / Math.Pow((lst_x[4] - lst_x[3]), 2)) * (lst_x[4] - lst_x[3]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            //=ATAN(2*((H240-H238)/(D240-D238)^2)*(D239-D238))
            //=ATAN(2*((H242-H240)/(D242-D240)^2)*(D242-D240))

            val = (2 * ((lst_Z_Ord[4] - lst_Z_Ord[6]) / Math.Pow((lst_x[6] - lst_x[4]), 2)) * (lst_x[6] - lst_x[5]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);

            #endregion Incln_Plan


            List<double> lst_theta_2 = new List<double>();
            #region theta_2

            for (i = 0; i < 5; i++)
            {
                if(i == 0)
                    val = lst_Incln_Plan[i];
                else
                    val = lst_Incln_Plan[i] - lst_Incln_Plan[i - 1];
                lst_theta_2.Add(val);
            }

            for (i = 5; i < list.Count; i++)
            {
                val = lst_theta_2[4] + (lst_Incln_Plan[4] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }
            #endregion theta_2

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


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

            #region Friction after slip loss

            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = val;

            x = lst_x[6] - lst_x[1];
            double Y = lst_Y_Ord[1] - lst_Y_Ord[6];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[6] - lst_x[4];
            Y = lst_Z_Ord[4] - lst_Z_Ord[6];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_add_parabola + len_inclnd + (lst_x[14] - lst_x[6]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[4] - lst_F[5]) / (lst_x[5] - lst_x[4]);




            List<double> lst_half_area = new List<double>();




            for (i = 1; i < 4; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 2; i < 5; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[7];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[4] - (drop_force * Ls);
            val = Ls;

            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                if (i < 4)
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
            #endregion Friction after slip loss


        }

        private void Update_Datagrid_6()
        {

            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("Start of second para in plan"));
            list.Add(string.Format("End of first / second para in plan"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("Start of first para in plan"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            //list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            lst_x.Add(PD.Profile_1_L1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            val = 4.5 - CBGD.dist_face;
            lst_x.Add(val);


            double tmp = 0.0;

            tmp = PD.Profile_2_L5 + PD.Profile_2_L4 + PD.Profile_2_L3 + PD.Profile_2_L2 - PD.Profile_1_L3;

            val = val + tmp / 2;
            lst_x.Add(val);



            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[3] + tmp;
            lst_x.Add(val);




            val = lst_x[5] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[7] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);



            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[13] + CBGD.Mid_cc_supp / 20.0;


            //lst_x.Add(val - PD.Profile_1_B3);

            lst_x.Add(val);


            List<double> lst_Y_Ord = new List<double>();



            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3 + PD.Profile_1_D2;
            lst_Y_Ord.Add(val);





            val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[6] - lst_x[1]), 2.0))) * Math.Pow((lst_x[6] - lst_x[2]), 2.0));

            //lst_Y_Ord.Add(val);

            for (i = 2; i <= 6; i++)
            {

                val = PD.Profile_1_D3 + (((lst_Y_Ord[1] - PD.Profile_1_D3) / (Math.Pow((lst_x[6] - lst_x[1]), 2.0))) * Math.Pow((lst_x[6] - lst_x[i]), 2.0));

                lst_Y_Ord.Add(val);
            }


            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            //lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            val = (lst_Y_Ord[0] - lst_Y_Ord[1]) / (lst_x[1] - lst_x[0]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);



            val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[6] - lst_x[1]), 2.0)) * (lst_x[6] - lst_x[1]);

            val = Math.Atan(val);
            //lst_Incln_Elevn.Add(val);


            for (i = 1; i <= 6; i++)
            {
                val = 2 * ((lst_Y_Ord[1] - lst_Y_Ord[9]) / Math.Pow((lst_x[6] - lst_x[1]), 2.0)) * (lst_x[6] - lst_x[i]);

                val = Math.Atan(val);
                lst_Incln_Elevn.Add(val);
            }
            //lst_Incln_Elevn.Add(0);
            //lst_Incln_Elevn.Add(0);

            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(val);

            List<double> lst_theta_1 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[0] - lst_Incln_Elevn[i];
                if (val < 0) val = 0.0;
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 - CS.CS_Anc_B4) / 1000.0;
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;


            double H780 = val;
            double H784 = CS.CS_Anc_B13 / 1000 - PD.Profile_2_D4; ;
            double H782 = (H780 + H784) / 2.0;

            //double H239 = val + (((H240 - val) / Math.Pow((lst_x[5] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((D782 - D780), 2)) * Math.Pow((D781 - D780), 2));
            //val = H780 + (((H782 - H780) / Math.Pow((lst_x[6] - lst_x[3]), 2)) * Math.Pow((lst_x[4] - lst_x[3]), 2));
            //lst_Z_Ord.Add(val);

            lst_Z_Ord.Add(H782);


            //val = H784 - (((H784 - H782) / (D784 - D782) ^ 2) * (D784 - D783) ^ 2);
            val2 = H784 + (((H782 - H784) / Math.Pow((lst_x[6] - lst_x[4]), 2)) * Math.Pow((lst_x[6] - lst_x[5]), 2));
            lst_Z_Ord.Add(val2);

            //lst_Z_Ord.Add(H239);
            //lst_Z_Ord.Add(H240);

            //val = val2 - (((val2 - H240) / Math.Pow((lst_x[7] - lst_x[5]), 2)) * Math.Pow((lst_x[7] - lst_x[6]), 2));
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);
            lst_Z_Ord.Add(H784);




            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);

            val = (2 * ((lst_Z_Ord[3] - lst_Z_Ord[4]) / Math.Pow((lst_x[4] - lst_x[3]), 2)) * (lst_x[4] - lst_x[3]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            //=ATAN(2*((H240-H238)/(D240-D238)^2)*(D239-D238))
            //=ATAN(2*((H242-H240)/(D242-D240)^2)*(D242-D240))

            val = (2 * ((lst_Z_Ord[4] - lst_Z_Ord[6]) / Math.Pow((lst_x[6] - lst_x[4]), 2)) * (lst_x[6] - lst_x[5]));

            val = Math.Atan(val);


            lst_Incln_Plan.Add(val);


            val = 0;
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);
            lst_Incln_Plan.Add(val);



            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 5; i++)
            {
                if (i == 0)
                    val = lst_Incln_Plan[i];
                else
                    val = lst_Incln_Plan[i] - lst_Incln_Plan[i - 1];
                lst_theta_2.Add(val);
            }

            for (i = 5; i < list.Count; i++)
            {
                val = lst_theta_2[4] + (lst_Incln_Plan[4] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


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

            #region Friction after slip loss

            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            for (i = 1; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = 0.0;

            x = lst_x[6] - lst_x[1];
            double Y = lst_Y_Ord[1] - lst_Y_Ord[6];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[6] - lst_x[4];
            Y = lst_Z_Ord[4] - lst_Z_Ord[6];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_add_parabola + len_inclnd + (lst_x[14] - lst_x[6]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[5] - lst_F[6]) / (lst_x[6] - lst_x[5]);




            List<double> lst_half_area = new List<double>();




            for (i = 1; i < 6; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 2; i < 6; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[5];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[5] - (drop_force * Ls);
            val = Ls;

            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();
            for (i = 0; i < list.Count; i++)
            {
                if (i < 4)
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
            #endregion Friction after slip loss

        }

        private void Update_Datagrid_7_9()
        {

            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Face of Diaphragm"));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            //lst_x.Add(PD.Profile_1_B1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            //val = 4.5 - CBGD.dist_face;
            val = val + (CBGD.Mid_cc_supp / 20.0);
            lst_x.Add(val);


            double tmp = 0.0;

            val = PD.Profile_2_L6 - PD.Profile_1_L4;
            lst_x.Add(val);

            //val = val + tmp / 2;
            //lst_x.Add(val);



            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            //val = lst_x[3] + tmp;
            //lst_x.Add(val);




            val = lst_x[3] + PD.Profile_1_L1;
            lst_x.Add(val);


            val = lst_x[5] + PD.Profile_1_L2;
            lst_x.Add(val);



            val = lst_x[4] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[7] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);
             
            List<double> lst_Y_Ord = new List<double>();

            lst_Y_Ord.Add(0);
            lst_Y_Ord.Add(0);
            lst_Y_Ord.Add(0);


            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);




            tmp = PD.Profile_1_D3 + PD.Profile_1_D2;




            //val = PD.Profile_1_D3 + PD.Profile_1_D2;


            val = tmp + ((val - tmp) * (lst_x[5] - lst_x[4]) / (lst_x[5] - lst_x[3]));
            lst_Y_Ord.Add(val);


            val = tmp;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3;
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            //lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            val = (lst_Y_Ord[3] - lst_Y_Ord[4]) / (lst_x[4] - lst_x[3]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);


            val = (lst_Y_Ord[4] - lst_Y_Ord[5]) / (lst_x[5] - lst_x[4]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);


            val = 2 * ((lst_Y_Ord[5] - lst_Y_Ord[6]) / Math.Pow((lst_x[6] - lst_x[5]), 2.0)) * (lst_x[6] - lst_x[5]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(0);

            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);

           
            List<double> lst_theta_1 = new List<double>();

            lst_theta_1.Add(0);
            lst_theta_1.Add(0);
            lst_theta_1.Add(0);

            for (i = 3; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[3] - lst_Incln_Elevn[i];
                if (val < 0) val = 0.0;
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 / 1000.0 - PD.Profile_2_D1);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;
             


            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);


            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 5; i++)
            {
                if (i == 0)
                    val = lst_Incln_Plan[i];
                else
                    val = lst_Incln_Plan[i] - lst_Incln_Plan[i - 1];
                lst_theta_2.Add(val);
            }

            for (i = 5; i < list.Count; i++)
            {
                val = lst_theta_2[4] + (lst_Incln_Plan[4] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {

                if (i < 3)
                    val = 0;
                else
                    val = F * Math.Pow(Math.E, (-(lst_mu_theta[i] + lst_Kx[i])));
                lst_F.Add(val);
                lst_Fh.Add(val * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv.Add(val * Math.Sin(lst_Incln_Elevn[i]));
            }


            dgv_friction_loss.Rows.Clear();

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


            #region Friction after slip loss

            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            lst_avg_frc.Add(0);
            lst_avg_frc.Add(0);
            lst_avg_frc.Add(0);
            for (i = 4; i < list.Count; i++)
            {
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = 0.0;

            x = lst_x[6] - lst_x[5];
            double Y = lst_Y_Ord[5] - lst_Y_Ord[6];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[5] - lst_x[3];
            Y = lst_Y_Ord[3] - lst_Y_Ord[5];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            //double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            double len_inclnd = Math.Sqrt((Math.Pow(x, 2) + (Math.Pow(Y, 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_inclnd + (lst_x[13] - lst_x[6]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[10] - lst_F[11]) / (lst_x[11] - lst_x[10]);




            List<double> lst_half_area = new List<double>();


            lst_half_area.Add(0);
            lst_half_area.Add(0);
            lst_half_area.Add(0);

          
            for (i = 4; i < 11; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }
            for (i = 5; i < 10; i++)
            {

                if(i == 6)
                    val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[4] - lst_x[3]);
                else
                    val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i] - lst_x[3]);

                lst_half_area.Add(val);
            }

            val = drop_force / 2;
            val2 = drop_force * lst_x[10] - lst_x[3];
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[10] - (drop_force * Ls);
            val = Ls;

            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();

            lst_F2.Add(0);
            lst_F2.Add(0);
            lst_F2.Add(0);


            lst_Fh2.Add(0);
            lst_Fh2.Add(0);
            lst_Fh2.Add(0);

            lst_Fv2.Add(0);
            lst_Fv2.Add(0);
            lst_Fv2.Add(0);
            for (i = 3; i < list.Count; i++)
            {
                if (i < 11)
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
            #endregion Friction after slip loss

        }

        private void Update_Datagrid_8_10()
        {

            Set_User_Input();
            int i = 0;

            List<string> list = new List<string>();


            list.Add(string.Format("Face of Diaphragm"));
            list.Add(string.Format("1 L2/20"));
            list.Add(string.Format("2 L2/20"));
            list.Add(string.Format("3 L2/20"));
            list.Add(string.Format("Jacking End"));
            list.Add(string.Format("End of para in elevn."));
            list.Add(string.Format("Start of para in elevn."));
            list.Add(string.Format("4 L2/20"));
            list.Add(string.Format("5 L2/20"));
            list.Add(string.Format("6 L2/20"));
            list.Add(string.Format("7 L2/20"));
            list.Add(string.Format("8 L2/20"));
            list.Add(string.Format("9 L2/20"));
            list.Add(string.Format("10 L2/20"));


            double tot_len = CBGD.Mid_cc_temp_supp + (2 * CBGD.Mid_over_supp);
            double dist_supp = (CBGD.Mid_cc_supp - CBGD.Mid_cc_temp_supp) / 2.0 - CBGD.Mid_over_supp + CBGD.dist_face;

            List<double> lst_x = new List<double>();

            double x = 0.0;
            double val = 0.0;

            lst_x.Add(x);
            //lst_x.Add(PD.Profile_1_B1);



            val = x + (CBGD.Mid_cc_supp / 20.0 - dist_supp);
            lst_x.Add(val);


            //val = 4.5 - CBGD.dist_face;
            val = val + (CBGD.Mid_cc_supp / 20.0);
            lst_x.Add(val);


            double tmp = 0.0;

            //val = val + tmp / 2;
            //lst_x.Add(val);



            val = lst_x[2] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = PD.Profile_2_L6 - PD.Profile_1_L4;
            lst_x.Add(val);


            //val = lst_x[3] + tmp;
            //lst_x.Add(val);




            val = lst_x[3] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[4] + PD.Profile_1_L1;
            lst_x.Add(val);


            val = lst_x[6] + PD.Profile_1_L2;
            lst_x.Add(val);


            val = lst_x[5] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            val = lst_x[8] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[9] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[10] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[11] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);


            val = lst_x[12] + CBGD.Mid_cc_supp / 20.0;
            lst_x.Add(val);

            List<double> lst_Y_Ord = new List<double>();

            lst_Y_Ord.Add(0);
            lst_Y_Ord.Add(0);
            lst_Y_Ord.Add(0);
            lst_Y_Ord.Add(0);


            val = PD.Profile_1_D4 + PD.Profile_1_D3;
            lst_Y_Ord.Add(val);




            tmp = PD.Profile_1_D3 + PD.Profile_1_D2;


            //val = PD.Profile_1_D3 + PD.Profile_1_D2;


            val = tmp + ((val - tmp) * (lst_x[6] - lst_x[5]) / (lst_x[6] - lst_x[4]));
            lst_Y_Ord.Add(val);


            val = tmp;
            lst_Y_Ord.Add(val);



            val = PD.Profile_1_D3;
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            lst_Y_Ord.Add(val);
            //lst_Y_Ord.Add(val);
            //lst_Y_Ord.Add(val);




            List<double> lst_Incln_Elevn = new List<double>();

            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            val = (lst_Y_Ord[4] - lst_Y_Ord[6]) / (lst_x[6] - lst_x[4]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);


            val = (lst_Y_Ord[5] - lst_Y_Ord[6]) / (lst_x[6] - lst_x[5]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);


            val = 2 * ((lst_Y_Ord[6] - lst_Y_Ord[7]) / Math.Pow((lst_x[7] - lst_x[6]), 2.0)) * (lst_x[7] - lst_x[6]);

            val = Math.Atan(val);
            lst_Incln_Elevn.Add(val);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);
            lst_Incln_Elevn.Add(0);


            List<double> lst_theta_1 = new List<double>();

            lst_theta_1.Add(0);
            lst_theta_1.Add(0);
            lst_theta_1.Add(0);
            lst_theta_1.Add(0);

            for (i = 4; i < list.Count; i++)
            {
                val = lst_Incln_Elevn[4] - lst_Incln_Elevn[i];
                if (val < 0) val = 0.0;
                lst_theta_1.Add(val);
            }

            List<double> lst_Z_Ord = new List<double>();

            val = (CS.CS_Anc_B13 / 1000.0 - PD.Profile_2_D1);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);
            lst_Z_Ord.Add(val);



            double val2 = (CS.CS_B7 - CS.CS_B5 / 2.0) / 1000.0;



            List<double> lst_Incln_Plan = new List<double>();


            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);
            lst_Incln_Plan.Add(0);


            List<double> lst_theta_2 = new List<double>();

            for (i = 0; i < 5; i++)
            {
                if (i == 0)
                    val = lst_Incln_Plan[i];
                else
                    val = lst_Incln_Plan[i] - lst_Incln_Plan[i - 1];
                lst_theta_2.Add(val);
            }

            for (i = 5; i < list.Count; i++)
            {
                val = lst_theta_2[4] + (lst_Incln_Plan[4] - lst_Incln_Plan[i]);
                lst_theta_2.Add(val);
            }

            //val = lst_Incln_Plan[i] - lst_Incln_Plan[0];
            //lst_theta_2.Add(val);

            List<double> lst_theta = new List<double>();

            List<double> lst_mu_theta = new List<double>();


            for (i = 0; i < lst_theta_2.Count; i++)
            {
                val = lst_theta_1[i] + lst_theta_2[i];
                lst_theta.Add(val);
                lst_mu_theta.Add(val * CBGD.Mu);
            }

            List<double> lst_Kx = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                val = lst_x[i] * CBGD.k;
                lst_Kx.Add(val);
            }



            double F = CBGD.UTS * CBGD.Tendon_Area * 0.75 / 10000;

            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();


            for (i = 0; i < lst_x.Count; i++)
            {
                if (i < 4)
                    val = 0.0;
                else
                    val = F * Math.Pow(Math.E, (-(lst_mu_theta[i] + lst_Kx[i])));



                lst_F.Add(val);
                lst_Fh.Add(val * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv.Add(val * Math.Sin(lst_Incln_Elevn[i]));
            }


            dgv_friction_loss.Rows.Clear();

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

            #region Friction after slip loss

            //Weighted average force
            List<double> lst_avg_frc = new List<double>();
            //val = 1 / PD.Profile_1_B4;
            lst_avg_frc.Add(0);
            lst_avg_frc.Add(0);
            lst_avg_frc.Add(0);
            lst_avg_frc.Add(0);

            List<double> lst_fact = new List<double>();


            lst_fact.Add(0);
            lst_fact.Add(0);
            lst_fact.Add(0);
            lst_fact.Add(0);
            lst_fact.Add(lst_x[6] - lst_x[4]);
            lst_fact.Add(lst_x[3] - lst_x[6]);
            lst_fact.Add(lst_x[7] - lst_x[3]);
            lst_fact.Add(lst_x[5] - lst_x[7]);
            lst_fact.Add(lst_x[8] - lst_x[5]);
            lst_fact.Add(lst_x[9] - lst_x[8]);
            lst_fact.Add(lst_x[10] - lst_x[9]);
            lst_fact.Add(lst_x[11] - lst_x[10]);
            lst_fact.Add(lst_x[12] - lst_x[11]);
            lst_fact.Add(lst_x[13] - lst_x[12]);

            for (i = 5; i < list.Count; i++)
            {
                //val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                val = ((lst_F[i - 1] + lst_F[i]) / 2.0) * (lst_fact[i-1]);
                lst_avg_frc.Add(val);
            }
            val = 1 / PD.Profile_1_L4;
            double P = val * MyList.Get_Array_Sum(lst_avg_frc);


            double len_parabola = 0.0;

            x = lst_x[7] - lst_x[6];
            double Y = lst_Y_Ord[6] - lst_Y_Ord[7];


            len_parabola = x * (1 + 2 * Y * Y / (3 * x * x));


            x = lst_x[6] - lst_x[4];
            Y = lst_Y_Ord[4] - lst_Y_Ord[6];

            double len_add_parabola = 2 * x * (2 * Y * Y / (3 * x * x));


            //double len_inclnd = Math.Sqrt((Math.Pow(lst_x[1] - lst_x[0], 2) + (Math.Pow(lst_Y_Ord[1] - lst_Y_Ord[0], 2))));
            double len_inclnd = Math.Sqrt((Math.Pow(x, 2) + (Math.Pow(Y, 2))));
            val = len_add_parabola;



            double len_half_cable = len_parabola + len_inclnd + (lst_x[13] - lst_x[7]);



            double extention_end = P * 9.80665 * len_half_cable * 10E5 / (CBGD.Tendon_Area * CBGD.Es);


            double S = 6;

            double loss_due_slip = CBGD.Es * CBGD.Tendon_Area * S / 2;


            val = loss_due_slip / 9.80665 / 1000 / 1000;

            loss_due_slip = val;


            double drop_force = (lst_F[10] - lst_F[11]) / (lst_x[11] - lst_x[10]);




            List<double> lst_half_area = new List<double>();


            lst_half_area.Add(0);
            lst_half_area.Add(0);
            lst_half_area.Add(0);
            lst_half_area.Add(0);


            for (i = 5; i < 12; i++)
            {
                val = ((lst_F[i - 1] - lst_F[i]) / 2.0) * (lst_x[i] - lst_x[i - 1]);
                lst_half_area.Add(val);
            }

            tmp = 0;
            for (i = 6; i < 12; i++)
            {
                tmp += (lst_x[i-1] - lst_x[i-2]);
                //if (i == 6)
                //    val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[4] - lst_x[3]);
                //else
                //    val = ((lst_F[i - 1] - lst_F[i])) * (lst_x[i] - lst_x[3]);

                val = ((lst_F[i - 1] - lst_F[i])) * tmp;



                lst_half_area.Add(val);
            }
            tmp += (lst_x[i - 1] - lst_x[i - 2]);

            val = drop_force / 2;
            val2 = drop_force * tmp;
            double a = MyList.Get_Array_Sum(lst_half_area);



            double c = a - loss_due_slip;

            a = 1;

            double b = val2 / val;


            c = c / val;

            double Ls = ((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            double drop_in_frc = drop_force * Ls;


            double frc_at_null_point = lst_F[10] - (drop_force * Ls);
            val = Ls;

            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();

            lst_F2.Add(0);
            lst_F2.Add(0);
            lst_F2.Add(0);
            lst_F2.Add(0);


            lst_Fh2.Add(0);
            lst_Fh2.Add(0);
            lst_Fh2.Add(0);
            lst_Fh2.Add(0);

            lst_Fv2.Add(0);
            lst_Fv2.Add(0);
            lst_Fv2.Add(0);
            lst_Fv2.Add(0);
            for (i = 4; i < list.Count; i++)
            {
                if (i < 11)
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
            #endregion Friction after slip loss

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
            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f2} {13,9:f2}";

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
            Results.Add(string.Format("Section                                  Dist from    Y Ord     Incln      θ1       Z Ord    Incln      θ2       θ=θ1+θ2     µθ        Kx        F        Fh         Fv"));
            Results.Add(string.Format("                                         Jacking,x              Elevn.                       Plan                                                                            "));
            Results.Add(string.Format("                                            (m)        (m)       (rad)    (rad)       (m)    (rad)     (rad)       (rad)               (m)      (T)       (T)        (T)"));
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

            format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f2} {5,9:f2} {6,9:f2}";

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION AND SLIP LOSS"));
            Results.Add(string.Format("--------------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                  Dist from    Y Ord     Incln      F        Fh         Fv"));
            Results.Add(string.Format("                                         Jacking,x              Elevn.                           "));
            Results.Add(string.Format("                                            (m)        (m)       (rad)    (T)       (T)        (T)"));
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
        }
    }

    public class Mid_Span_Cable_Profile_Data
    {
        #region Propeties

        /// <summary>
        /// Number of Cables
        /// </summary>
        public List<int> NC { get; set; }

        public double Profile_1_L1 { get; set; }
        public double Profile_1_L2 { get; set; }
        public double Profile_1_L3 { get; set; }
        public double Profile_1_L4 { get; set; }

        public double Profile_1_D1 { get; set; }
        public double Profile_1_D2 { get; set; }
        public double Profile_1_D3 { get; set; }
        public double Profile_1_D4 { get; set; }

        public double Profile_2_L1 { get; set; }
        public double Profile_2_L2 { get; set; }
        public double Profile_2_L3 { get; set; }
        public double Profile_2_L4 { get; set; }
        public double Profile_2_L5 { get; set; }
        public double Profile_2_L6 { get; set; }

        public double Profile_2_D1 { get; set; }
        public double Profile_2_D2 { get; set; }
        public double Profile_2_D3 { get; set; }
        public double Profile_2_D4 { get; set; }
        #endregion Propeties

        public Force_After_Friction_Loss_Table Table_Friction_Loss { get; set; }
        public Force_After_Friction_Slip_Loss_Table Table_Friction_Slip_Loss { get; set; }

        public Mid_Span_Cable_Profile_Data()
        {

            NC = new List<int>();

            Profile_1_L1 = 0.0;
            Profile_1_L2 = 0.0;
            Profile_1_L3 = 0.0;
            Profile_1_L4 = 0.0;

            Profile_1_D1 = 0.0;
            Profile_1_D2 = 0.0;
            Profile_1_D3 = 0.0;
            Profile_1_D4 = 0.0;

            Profile_2_L1 = 0.0;
            Profile_2_L2 = 0.0;
            Profile_2_L3 = 0.0;
            Profile_2_L4 = 0.0;
            Profile_2_L5 = 0.0;
            Profile_2_L6 = 0.0;

            Profile_2_D1 = 0.0;
            Profile_2_D2 = 0.0;
            Profile_2_D3 = 0.0;
            Profile_2_D4 = 0.0;

            Table_Friction_Loss = new Force_After_Friction_Loss_Table();
            Table_Friction_Slip_Loss = new Force_After_Friction_Slip_Loss_Table();
        }
    }
    public class Force_After_Friction_Loss_Table : List<Force_After_Friction_Loss_Data>
    {
        public Force_After_Friction_Loss_Table()
            : base()
        {

        }

        public void Data_Retrieve_from_Grid(DataGridView dgv_friction_loass)
        {
            int column = 0;
            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f2} {13,9:f2}";

            Force_After_Friction_Loss_Data data = null;
            this.Clear();
            for (int i = 0; i < dgv_friction_loass.RowCount; i++)
            {
                column = 0;
                data = new Force_After_Friction_Loss_Data();

                data.Section = dgv_friction_loass[column++, i].Value.ToString();
                data.X = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Y_Ord = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Incln_Elevn = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Theta_1 = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Z_Ord = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Incln_Plan = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Theta_2 = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Theta = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Mu_Theta = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Kx = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.F = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Fh = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Fv = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);

                this.Add(data);
            }
        }
        
        //Chiranjit [2013 08 04]
        public List<string> Results { get; set; }
        public List<string> Get_Table_Result()
        {
            Results = new List<string>();

            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f2} {13,9:f2}";
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION LOSS"));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                  Dist from    Y Ord     Incln      θ1       Z Ord    Incln      θ2       θ=θ1+θ2     µθ        Kx        F        Fh         Fv"));
            Results.Add(string.Format("                                         Jacking,x              Elevn.                       Plan                                                                            "));
            Results.Add(string.Format("                                            (m)        (m)       (rad)    (rad)       (m)    (rad)     (rad)       (rad)               (m)      (T)       (T)        (T)"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
        
            for (int i = 0; i < Count; i++)
            {
                Results.Add(string.Format(format,
                    this[i].Section,
                    this[i].X,
                    this[i].Y_Ord,
                    this[i].Incln_Elevn,
                    this[i].Theta_1,
                    this[i].Z_Ord,
                    this[i].Incln_Plan,
                    this[i].Theta_2,
                    this[i].Theta,
                    this[i].Mu_Theta,
                    this[i].Kx,
                    this[i].F,
                    this[i].Fh,
                    this[i].Fv));
            }
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));

            return Results;

        }
        public Force_After_Friction_Loss_Data Get_Friction_Data(string section)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Section.TrimEnd().TrimStart() == section.Trim().TrimEnd().TrimStart())
                {
                    return this[i];
                }
            }

            return null;
        }
    }
    public class Force_After_Friction_Slip_Loss_Table : List<Force_After_Friction_Slip_Loss_Data>
    {
        public Force_After_Friction_Slip_Loss_Table()
            : base()
        {

        }
        public void Data_Retrieve_from_Grid(DataGridView dgv_friction_loass)
        {
            int column = 0;
         
            Force_After_Friction_Slip_Loss_Data data = null;
            this.Clear();
            for (int i = 0; i < dgv_friction_loass.RowCount; i++)
            {
                column = 0;
                data = new Force_After_Friction_Slip_Loss_Data();

                data.Section = dgv_friction_loass[column++, i].Value.ToString();
                data.X = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Y_Ord = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Incln_Elevn = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.F = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Fh = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Fv = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);

                this.Add(data);
            }
        }

        public List<string> Results { get; set; }
        public List<string> Get_Table_Result()
        {
            Results = new List<string>();

            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f2} {5,9:f2} {6,9:f2}";
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION AND SLIP LOSS"));
            Results.Add(string.Format("--------------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                  Dist from    Y Ord     Incln      F        Fh         Fv"));
            Results.Add(string.Format("                                         Jacking,x              Elevn.                           "));
            Results.Add(string.Format("                                            (m)        (m)       (rad)    (T)       (T)        (T)"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            for (int i = 0; i < Count; i++)
            {
                Results.Add(string.Format(format,
                    this[i].Section,
                    this[i].X,
                    this[i].Y_Ord,
                    this[i].Incln_Elevn,
                    this[i].F,
                    this[i].Fh,
                    this[i].Fv));
            }
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));

            return Results;

        }

        public Force_After_Friction_Slip_Loss_Data Get_Slip_Loss_Data(string section)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Section.TrimEnd().TrimStart() == section.Trim().TrimEnd().TrimStart())
                {
                    return this[i];
                }
            }

            return null;
        }
    }
    public class PSF_after_FS_Loss_Table : List<PSF_after_FS_Loss>
    {
        public PSF_after_FS_Loss_Table()
            : base()
        {

        }

        public double Yb { get; set; }
        public string Section { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
        public double e { get; set; }
        public double Fh_x_e { get; set; }



        public List<string> Get_Results()
        {

            List<string> output = new List<string>();

            string format = "";

            output.Add(string.Format("----------------------------------------------------------------------------------"));
            output.Add(string.Format("SECTION AT {0} , Yb = {1:f4}", Section, Yb));
            output.Add(string.Format("----------------------------------------------------------------------------------"));
            output.Add(string.Format(""));
            output.Add(string.Format(""));
            output.Add(string.Format("----------------------------------------------------------------------------------"));
            output.Add(string.Format("     Cable    No. of      F       Fh         Fv       Ordinate      e       Fh x e"));
            output.Add(string.Format("      No.     Cables     (T)      (T)        (T)        (m)        (m)       (T-m)"));
            output.Add(string.Format("----------------------------------------------------------------------------------"));

            format = "{0,9:f0} {1,9:f0} {2,9:f2} {3,9:f2} {4,9:f2}  {5,9:f3}  {6,9:f4} {7,9:f2}";

            F = 0;
            Fh = 0;
            Fv = 0;
            e = 0;
            Fh_x_e = 0;
            int no_cbl = 0;

            for (int j = 0; j < this.Count; j++)
            {
                output.Add(string.Format(format, this[j].CableNo, this[j].Nos_of_Cables, this[j].F,
                    this[j].Fh, this[j].Fv, this[j].Ordinate, this[j].e, this[j].Fh_x_e));

                no_cbl += this[j].Nos_of_Cables;
                F = F + this[j].F;
                Fh = Fh + this[j].Fh;
                Fv = Fv + this[j].Fv;
                Fh_x_e = Fh_x_e + this[j].Fh_x_e;
            }
            output.Add(string.Format("----------------------------------------------------------------------------------"));
            output.Add(string.Format(format, "", no_cbl, F, Fh, Fv, "", "", Fh_x_e));
            output.Add(string.Format("----------------------------------------------------------------------------------"));
            
            output.Add(string.Format(""));

            if (Fh == 0.0)
                e = 0.0;
            else
                e = Fh_x_e / Fh;

            output.Add(string.Format("Eccy.  Of Cable Group   = {0:f3} / {1:f3} = {2:f3}", Fh_x_e, Fh, e));
            output.Add(string.Format(""));
            output.Add(string.Format(""));
            output.Add(string.Format("      F  = {0:f3} Ton", F));
            output.Add(string.Format("      Fh = {0:f3} Ton", Fh));
            output.Add(string.Format("      Fv = {0:f3} Ton", Fv));
            output.Add(string.Format("      e  = {0:f3} m", e));
            output.Add(string.Format(""));
            output.Add(string.Format("----------------------------------------------------------------------------------"));
            output.Add(string.Format(""));

            return output;
        }


    }


    public class Force_After_Friction_Loss_Data
    {
        public Force_After_Friction_Loss_Data()
        {
            Section = "";
            X = 0.0;
            Y_Ord = 0.0;
            Incln_Elevn = 0.0;
            Theta_1 = 0.0;
            Z_Ord = 0.0;
            Incln_Plan = 0.0;
            Theta_2 = 0.0;
            Mu_Theta = 0.0;
            Kx = 0.0;
            F = 0.0;
            Fh = 0.0;
            Fv = 0.0;
        }
        public string Section { get; set; }
        public double X { get; set; }
        public double Y_Ord { get; set; }
        public double Incln_Elevn { get; set; }
        public double Theta_1 { get; set; }
        public double Z_Ord { get; set; }
        public double Incln_Plan { get; set; }
        public double Theta_2 { get; set; }
        public double Theta { get { return (Theta_1 + Theta_2); } set { } }
        public double Mu_Theta { get; set; }
        public double Kx { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
    }
    public class Force_After_Friction_Slip_Loss_Data
    {
        public Force_After_Friction_Slip_Loss_Data()
        {
            Section = "";
            X = 0.0;
            Y_Ord = 0.0;
            Incln_Elevn = 0.0;
            F = 0.0;
            Fh = 0.0;
            Fv = 0.0;
        }
        public string Section { get; set; }
        public double X { get; set; }
        public double Y_Ord { get; set; }
        public double Incln_Elevn { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
    }

    //Pre_Stress_Force_after_Friction_and_Slip_Loss
    public class PSF_after_FS_Loss
    {
        public string CableNo { get; set; }
        public int Nos_of_Cables { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
        public double Ordinate { get; set; }
        public double e { get; set; }
        public double Fh_x_e { get; set; }

        public PSF_after_FS_Loss()
        {
            CableNo = "";
            Nos_of_Cables = 0;
            F = 0.0;
            Fh = 0.0;
            Fv = 0.0;
            Ordinate = 0.0;
            e = 0.0;
            Fh_x_e = 0.0;

        }
    }
}
