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
    public partial class frm_Cap_Cables : Form
    {
        public List<string> Results { get; set; }
        Continuous_Box_Girder_Design Design;

        public Cap_Cable_Profile_Data PD { get; set; }


        public List<string> Calculation_Details_Friction { get; set; }
        public List<string> Calculation_Details_Slip { get; set; }
        public List<string> Input_Details { get; set; }
        #region  Poperties
        public double Adopt_Cable
        {
            get
            {
                return MyList.StringToDouble(txt_adp_cbl.Text, 0.0);
            }
        }
        public int NC
        {
            get
            {
                return MyList.StringToInt(txt_unc.Text, 0);
            }
        }
        public double Duct_Dia
        {
            get
            {
                return MyList.StringToDouble(txt_duct_dia.Text, 0.0);
            }
        }
        public double UTS
        {
            get
            {
                return MyList.StringToDouble(txt_UTS.Text, 0.0);
            }
        }
        public double Area
        {
            get
            {
                return MyList.StringToDouble(txt_Area.Text, 0.0);
            }
        }


        public double P
        {
            get
            {
                return MyList.StringToDouble(txt_P.Text, 0.0);
            }
        }
        public double mpf
        {
            get
            {
                return MyList.StringToDouble(txt_mpf.Text, 0.0);
            }
        }


        public List<double> _L { get; set; }
        public List<double> _D { get; set; }
        #endregion  Poperties
        int CableNo = 1;
        public frm_Cap_Cables(Continuous_Box_Girder_Design CBGD, int cable_no)
        {
            InitializeComponent();
            this.Design = CBGD;
            Results = new List<string>();
            Calculation_Details_Friction = new List<string>();
            Calculation_Details_Slip = new List<string>();
            Input_Details = new List<string>();
            _L = new List<double>();
            _D = new List<double>();
            CableNo = cable_no;

            PD = new Cap_Cable_Profile_Data();

        }

        private void frm_Cap_Cables_Load(object sender, EventArgs e)
        {
            //Set_Input_Data();

            #region Set Default Data
            
            switch(CableNo)
            {
                case 1:
                    txt_cap_D1.Text = "0.525";
                    txt_cap_D2.Text = "0.400";
                    txt_cap_D3.Text = "0.125";
                    txt_cap_D4.Text = "0.352";
                    txt_cap_D5.Text = "0.048";
                    txt_cap_D6.Text = "0.337";
                    txt_cap_D7.Text = "0.063";

                    txt_cap_L1.Text = "1.000";
                    txt_cap_L2.Text = "14.75";
                    txt_cap_L3.Text = "0.250";
                    txt_cap_L4.Text = "10.75";
                    txt_cap_L5.Text = "15.750";
                    txt_cap_L6.Text = "11.750";
                    txt_cap_L7.Text = "16.000";
                    txt_cap_L8.Text = "12.000";


                    txt_unc.Text = "4";

                    lbl_span1.Text = Design.End_cc_supp.ToString() + "   m    Span.";
                    lbl_span2.Text = Design.Mid_cc_supp.ToString() + "   m    Span.";

                    break;
                case 2:
                    txt_cap_D1.Text = "0.525";
                    txt_cap_D2.Text = "0.400";
                    txt_cap_D3.Text = "0.125";
                    txt_cap_D4.Text = "0.357";
                    txt_cap_D5.Text = "0.043";
                    txt_cap_D6.Text = "0.352";
                    txt_cap_D7.Text = "0.048";

                    txt_cap_L1.Text = "1.000";
                    txt_cap_L2.Text = "16.75";
                    txt_cap_L3.Text = "0.250";
                    txt_cap_L4.Text = "14.75";
                    txt_cap_L5.Text = "17.750";
                    txt_cap_L6.Text = "15.750";
                    txt_cap_L7.Text = "18.000";
                    txt_cap_L8.Text = "16.000";

                    txt_unc.Text = "4";
                    
                    lbl_span1.Text = Design.End_cc_supp.ToString() + "   m    Span.";
                    lbl_span2.Text = Design.Mid_cc_supp.ToString() + "   m    Span.";

                    break;
                case 3:
                    txt_cap_D1.Text = "0.525";
                    txt_cap_D2.Text = "0.400";
                    txt_cap_D3.Text = "0.125";
                    txt_cap_D4.Text = "0.337";
                    txt_cap_D5.Text = "0.063";
                    txt_cap_D6.Text = "0.337";
                    txt_cap_D7.Text = "0.063";

                    txt_cap_L1.Text = "1.000";
                    txt_cap_L2.Text = "10.75";
                    txt_cap_L3.Text = "0.250";
                    txt_cap_L4.Text = "10.75";
                    txt_cap_L5.Text = "11.750";
                    txt_cap_L6.Text = "11.750";
                    txt_cap_L7.Text = "12.000";
                    txt_cap_L8.Text = "12.000";

                    txt_unc.Text = "6";

                    
                    lbl_span2.Text = Design.End_cc_supp.ToString() + "   m    Span.";
                    lbl_span1.Text = Design.Mid_cc_supp.ToString() + "   m    Span.";



                    break;
        }
            #endregion Set Default Data

            Calculate_Results();

            lbl_cap.Text = "CABLE CAP " + CableNo;
            btn_process.Text = string.Format("PROCESS {0} of 3", CableNo);
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            Calculate_Results();

            PD.Table_Friction_Loss.Data_Retrieve_from_Grid(dgv_friction_loss);
            PD.Table_Friction_Slip_Loss.Data_Retrieve_from_Grid(dgv_slip_loss);
            this.Close();
        }
        void Set_Input_Data()
        {
            _L.Clear();
            _D.Clear();


            _L.Add(MyList.StringToDouble(txt_cap_L1.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L2.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L3.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L4.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L5.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L6.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L7.Text, 0.0));
            _L.Add(MyList.StringToDouble(txt_cap_L8.Text, 0.0));

            _D.Add(MyList.StringToDouble(txt_cap_D1.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D2.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D3.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D4.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D5.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D6.Text, 0.0));
            _D.Add(MyList.StringToDouble(txt_cap_D7.Text, 0.0));

            PD = new Cap_Cable_Profile_Data();

            PD._L = _L;
            PD._D = _D;

            PD.Adopt_Cable = Adopt_Cable;
            PD.NC = NC;
            PD.Duct_Dia = Duct_Dia;
            PD.Area = Area;
            PD.P = P;
            PD.mpf = mpf;
            PD.UTS = UTS;
        }
        public void Calculate_Results()
        {
            Set_Input_Data();
            Results.Clear();

            #region User Inputs
            Results.Add(string.Format(""));
            Results.Add(string.Format("Cable containing of 12 Nos of 12.7mm dia 7 ply Low Relaxation Class 2"));
            Results.Add(string.Format("strand as per IS: 6006-1983 shall be used for prestressing"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Adopt Cables of {0}  T 13     {1} Nos", Adopt_Cable, NC));
            Results.Add(string.Format("Duct dia   =  {0} mm ", Duct_Dia));
            Results.Add(string.Format("U T S = {0} Mpa ", UTS));

            double _UTS = UTS * 100 / 9.80655;
            Results.Add(string.Format("        = {0:f3} Kg/cm2", _UTS));
            Results.Add(string.Format("Area of one strand  = {0:f3} mm^2", Area));
            Results.Add(string.Format(""));
            double tendon_area = Area * Adopt_Cable;
            Results.Add(string.Format("Area of tendon = {0} x {1:f3} = {2:f3} mm^2", Area, Adopt_Cable, tendon_area));


            double ult_tens_stngth = tendon_area * UTS / 1000.0;
            Results.Add(string.Format("Ultimate tensile strength of cable = {0:f3} x {1:f3} / 1000 = {2:f3} kN ", tendon_area, UTS, ult_tens_stngth));

            double max_force = (mpf / 100.0) * ult_tens_stngth;

            Results.Add(string.Format("Maximum permissible force in cable = {0:f3} x {1:f3} = ", (mpf / 100.0), ult_tens_stngth));
            Results.Add(string.Format(""));
            Results.Add(string.Format("                                   = {0:f3} kN", max_force));
            max_force = max_force / 9.80665;
            Results.Add(string.Format("                                   = {0:f3} T", max_force));
            Results.Add(string.Format(""));

            Results.Add(string.Format("Let  (F1 - F2) be the drop in force due to slip in length  \"dx\" at a distance \"x\""));
            Results.Add(string.Format("from jacking end,  Stress loss = (F1-F2) /  {0:f3}", tendon_area));

            Results.Add(string.Format("                   x"));
            Results.Add(string.Format("Total slip loss = ∫ strain loss  =  {0} mm  ", Design.S));
            Results.Add(string.Format("                  0"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("                    x"));
            Results.Add(string.Format("                =  ∫(F1-F2) dx /{0} x {1}", Design.Tendon_Area, Design.Es));
            Results.Add(string.Format("                  0"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Modulus of Elasticity of steel = {0} MPa = {1} Kg/cm^2 ", Design.Es, (Design.Es * 10)));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Additional length of cable for attatching the jack is assumed = 1.000 m"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("At any distance x from jacking end Prestressing Force after friction, F = F0 x e^(-(Kx + mq))"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Wooble co-efficient, K = {0:f5}", Design.k));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Friction co-efficient, m = {0:f5}", Design.Mu));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Grade of Concrete  =  M {0}", Design.Fcu));
            Results.Add(string.Format(""));


            Input_Details.Clear();
            Input_Details.AddRange(Results.ToArray());
            Results.Clear();

            #endregion User Inputs

            double val1, val2, temp;

            List<string> sections = new List<string>();

            #region Sections
            sections.Add(string.Format("Dead End"));
            sections.Add(string.Format("1 L1 /10"));
            sections.Add(string.Format("End of parabola in elevation"));
            sections.Add(string.Format("2 L1 / 10"));
            sections.Add(string.Format("3 L1 / 10"));
            sections.Add(string.Format("4 L1 / 10"));
            sections.Add(string.Format("5 L1 / 10"));
            sections.Add(string.Format("6 L1 /10"));
            sections.Add(string.Format("7 L1 /10"));
            sections.Add(string.Format("8 L1 /10"));
            sections.Add(string.Format("9 L1 /10"));
            sections.Add(string.Format("Face of diaphragm"));
            sections.Add(string.Format("Start of parabola in elevation"));
            sections.Add(string.Format("Support 2"));
            sections.Add(string.Format("Start of parabola in elevation"));
            sections.Add(string.Format("Face of diaphragm"));
            sections.Add(string.Format("1 L2 / 20"));
            sections.Add(string.Format("2 L2 / 20"));
            sections.Add(string.Format("3 L2 / 20"));
            sections.Add(string.Format("4 L2 / 20"));
            sections.Add(string.Format("5 L2 / 20"));
            sections.Add(string.Format("6 L2 / 20"));
            sections.Add(string.Format("7 L2 / 20"));
            sections.Add(string.Format("8 L2 / 20"));
            sections.Add(string.Format("End of parabola in elevation"));
            sections.Add(string.Format("9 L2 / 20"));
            sections.Add(string.Format("Jacking End"));

            #endregion Sections

            List<double> lst_x = new List<double>();

            #region Distance from Jacking End

            val1 = _L[7] + _L[6];

            lst_x.Add(val1);


            double D84 = _L[7];
            double H43 = Design.End_cc_supp;

            if (D84 + (9 * H43 / 10) > lst_x[0])
                val1 = lst_x[0];
            else
                val1 = D84 + (9 * H43 / 10);

            lst_x.Add(val1);

            //=D71-E57
            val1 = lst_x[0] - _L[0];
            lst_x.Add(val1);


            for (int i = 8; i > 0; i--)
            {
                if (D84 + (i * H43 / 10) > lst_x[2])
                    val1 = lst_x[2];
                else
                    val1 = D84 + (i * H43 / 10);
                lst_x.Add(val1);
            }

            val1 = D84 + (1.5 / 2.0);
            lst_x.Add(val1);


            val1 = D84 + _L[2];
            lst_x.Add(val1);


            lst_x.Add(D84);




            val1 = D84 - _L[2];
            lst_x.Add(val1);


            val1 = D84 - (1.5 / 2.0);
            lst_x.Add(val1);



            double P43 = Design.Mid_cc_supp;
            double D95 = _L[0];
            double D97 = 0.0;


            for (int i = 1; i < 9; i++)
            {
                //=IF(D84-(3*P43/20)>D95,D84-(3*P43/20),IF(D84-(3*P43/20)>D97,D84-(3*P43/20),D95))
                if (D84 - (i * P43 / 20) > D95)
                    val1 = D84 - (i * P43 / 20);
                else if (D84 - (i * P43 / 20) > D97)
                    val1 = D84 - (i * P43 / 20);
                else
                    val1 = D95;
                lst_x.Add(val1);
            }

            lst_x.Add(D95);

            //=IF(D84-(9*P43/20)>D95,D84-(9*P43/20),IF(D84-(9*P43/20)>D97,D84-(9*P43/20),D97))
            if (D84 - (9 * P43 / 20) > D95)
                val1 = D84 - (9 * P43 / 20);
            else if (D84 - (9 * P43 / 20) > D97)
                val1 = D84 - (9 * P43 / 20);
            else
                val1 = D97;
            lst_x.Add(val1);

            lst_x.Add(D97);


            #endregion Distance from Jacking End

            List<double> lst_Y_ord_Top = new List<double>();

            #region Y Ord from Top


            val1 = _D[0];
            lst_Y_ord_Top.Add(val1);



            double D71 = lst_x[0];
            double D72 = lst_x[1];
            double D73 = lst_x[2];
            double D83 = lst_x[12];

            double E71 = lst_Y_ord_Top[0];
            double E73 = _D[0] - _D[4];
            double E83 = _D[2];

            if (D72 > D73)
                val1 = E73 + ((E71 - E73) * (D72 - D73) / (D71 - D73));
            else
                val1 = E83 + (((E73 - E83) / Math.Pow((D73 - D83), 2)) * Math.Pow((D72 - D83), 2));
            lst_Y_ord_Top.Add(val1);



            val1 = E73;
            lst_Y_ord_Top.Add(val1);



            //=IF(D74>D73,E73+((E71-E73)*(D74-D73)/(D71-D73)),E83+(((E73-E83)/(D73-D83)^2)*(D74-D83)^2))

            for (int i = 3; i <= 11; i++)
            {
                if (lst_x[i] > D73)
                    val1 = E73 + ((E71 - E73) * (lst_x[i] - D73) / (D71 - D73));
                else
                    val1 = E83 + (((E73 - E83) / Math.Pow((D73 - D83), 2)) * Math.Pow((lst_x[i] - D83), 2));

                lst_Y_ord_Top.Add(val1);
            }



            val1 = _D[2];
            lst_Y_ord_Top.Add(val1);

            lst_Y_ord_Top.Add(val1);
            lst_Y_ord_Top.Add(val1);




            //=IF(D86<D95,E95+((E97-E95)*(D95-D86)/(D95-D97)),E85+(((E95-E85)/(D85-D95)^2)*(D85-D86)^2))
            double D85 = lst_x[14];
            double D86 = lst_x[15];
            //double D95 = _L[0];
            double E95 = _D[0] - _D[6];
            double E97 = _D[0];
            double E85 = _D[2];

            for (int i = 15; i <= 23; i++)
            {
                D86 = lst_x[i];
                if (D86 < D95)
                    val1 = E95 + ((E97 - E95) * (D95 - D86) / (D95 - D97));
                else
                    val1 = E85 + (((E95 - E85) / Math.Pow((D85 - D95), 2)) * Math.Pow((D85 - D86), 2));
                lst_Y_ord_Top.Add(val1);
            }


            lst_Y_ord_Top.Add(E95);


            D86 = lst_x[25];
            if (D86 < D95)
                val1 = E95 + ((E97 - E95) * (D95 - D86) / (D95 - D97));
            else
                val1 = E85 + (((E95 - E85) / Math.Pow((D85 - D95), 2)) * Math.Pow((D85 - D86), 2));
            lst_Y_ord_Top.Add(val1);
            lst_Y_ord_Top.Add(E97);
            #endregion Y Ord from Top

            List<double> lst_Incln_Elevn = new List<double>();
            #region Incln. Elevn. (rad.)
            for (int i = 0; i < lst_Y_ord_Top.Count; i++)
            {

                //=IF(D71>D73,ATAN((E71-E73)/(D71-D73)),ATAN(2*((E73-E83)/(D73-D83)^2)*(D71-D83)))

                //=IF(D72>D73,ATAN((E72-E73)/(D72-D73)),ATAN(2*((E73-E83)/(D73-D83)^2)*(D72-D83)))

                if (i < 13)
                {
                    D71 = lst_x[i];
                    E71 = lst_Y_ord_Top[i];

                    if (D71 > D73)
                        val1 = Math.Atan((E71 - E73) / (D71 - D73));
                    else
                        val1 = Math.Atan(2 * ((E73 - E83) / Math.Pow((D73 - D83), 2) * (D71 - D83)));

                }
                else
                {
                    //=IF(D87<D95,ATAN((E87-E95)/(D95-D87)),ATAN(2*((E95-E85)/(D85-D95)^2)*(D85-D87)))
                    //=IF(D90<D95,ATAN((E90-E95)/(D95-D90)),ATAN(2*((E95-E85)/(D85-D95)^2)*(D85-D90)))
                    if (lst_x[i] < D95)
                        val1 = Math.Atan((lst_Y_ord_Top[i] - E95) / (D95 - lst_x[i]));
                    else
                        val1 = Math.Atan(2 * ((E95 - E85) / Math.Pow((D85 - D95), 2) * (D85 - lst_x[i])));
                }
                lst_Incln_Elevn.Add(val1);
            }
            #endregion Incln. Elevn. (rad.)


            List<double> lst_Theta = new List<double>();
            List<double> lst_Mu_Theta = new List<double>();
            List<double> lst_Kx = new List<double>();
            List<double> lst_F = new List<double>();
            List<double> lst_Fh = new List<double>();
            List<double> lst_Fv = new List<double>();

            #region Theta
            double G83 = lst_Incln_Elevn[26] - lst_Incln_Elevn[12];

            for (int i = 0; i < lst_Incln_Elevn.Count; i++)
            {
                if (i < 13)
                {
                    //=G83+(F71-F83)
                    val1 = G83 + (lst_Incln_Elevn[0] - lst_Incln_Elevn[12]);
                }
                else
                {
                    //=F97-F88
                    val1 = (lst_Incln_Elevn[26] - lst_Incln_Elevn[i]);

                }
                lst_Theta.Add(val1);

                lst_Mu_Theta.Add(val1 * Design.Mu);
                lst_Kx.Add(lst_x[i] * Design.k);


                val1 = max_force * Math.Pow(Math.E, (-(lst_Mu_Theta[i] + lst_Kx[i])));
                lst_F.Add(val1);
                lst_Fh.Add(val1 * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv.Add(val1 * Math.Sin(lst_Incln_Elevn[i]));
            }


            val1 = max_force;



            dgv_friction_loss.Rows.Clear();


            for (int i = 0; i < lst_x.Count; i++)
            {
                dgv_friction_loss.Rows.Add(sections[i],
                    lst_x[i],
                    lst_Y_ord_Top[i],
                    lst_Incln_Elevn[i],
                    lst_Theta[i],
                    lst_Mu_Theta[i],
                    lst_Kx[i],
                    lst_F[i],
                    lst_Fh[i],
                    lst_Fv[i]);
            }

            double val = 0;

            for (int i = 0; i < dgv_friction_loss.RowCount; i++)
            {

                for (int j = 1; j < dgv_friction_loss.ColumnCount; j++)
                {
                    if (j > 1 && j < 7)
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

            val = 0;

            #endregion Theta



            //Weighted average force

            List<double> lst_avg_frcs = new List<double>();

            //Results.Add(string.Format("Weighted average force  = 1 / {0:f3} x  ", lst_x[0]));

            for (int i = 1; i < lst_Fh.Count; i++)
            {
                val = ((lst_Fh[i - 1] + lst_Fh[i]) / 2.0);

                if (lst_x[i - 1] > lst_x[i])
                {
                    val1 = (lst_x[i - 1] - lst_x[i]);
                }
                else
                {
                    val1 = 0;
                }

                if (i == 1)
                {
                    Results.Add(string.Format("Weighted average force  = 1 / {0:f3} x (({1:f3} + {2:f3}) / 2) x {3:f3} ", lst_x[0], lst_Fh[i - 1], lst_Fh[i], val1));
                }
                else
                {
                    Results.Add(string.Format("                                     + (({0:f3} + {1:f3}) / 2) x {2:f3} ", lst_Fh[i - 1], lst_Fh[i], val1));
                }
                lst_avg_frcs.Add(val * val1);

            }

            val1 = 0;
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            val = 1 / lst_x[0];
            val1 = MyList.Get_Array_Sum(lst_avg_frcs);
            double avg_wgt_frc = val * val1;
            Results.Add(string.Format("Weighted average force  = {0:f3} x ({1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                val,
                lst_avg_frcs[0],
                lst_avg_frcs[1],
                lst_avg_frcs[2],
                lst_avg_frcs[3],
                lst_avg_frcs[4],
                lst_avg_frcs[5]));

            Results.Add(string.Format("                                 + {0:f3} x {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                          lst_avg_frcs[6],
                          lst_avg_frcs[7],
                          lst_avg_frcs[8],
                          lst_avg_frcs[9],
                          lst_avg_frcs[10],
                          lst_avg_frcs[11],
                          lst_avg_frcs[12]));

            Results.Add(string.Format("                                 + {0:f3} x {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                          lst_avg_frcs[13],
                          lst_avg_frcs[14],
                          lst_avg_frcs[15],
                          lst_avg_frcs[16],
                          lst_avg_frcs[17],
                          lst_avg_frcs[18],
                          lst_avg_frcs[19]));

            Results.Add(string.Format("                                 + {0:f3} x {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3})",
                          lst_avg_frcs[20],
                          lst_avg_frcs[21],
                          lst_avg_frcs[22],
                          lst_avg_frcs[23],
                          lst_avg_frcs[24],
                          lst_avg_frcs[25]));



            //Results.Add(string.Format("                                 + {0:f3} +{1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}", val));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Weighted average force = P  = {0:f3} x {1:f3} = {2:f3} Ton", val, val1, avg_wgt_frc));

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            double X = (lst_x[2] - lst_x[12]);
            double Y = (lst_Y_ord_Top[2] - lst_Y_ord_Top[12]);


            double len_parabola_1 = X * (1 + 2 * Y * Y / (3 * X * X));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("X = {0:f3} - {1:f3} = {2:f3}", lst_x[2], lst_x[12], X));
            Results.Add(string.Format("Y = {0:f3} - {1:f3} = {2:f3}", lst_Y_ord_Top[2], lst_Y_ord_Top[12], Y));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Length of first Parabola in elevation = X (1 + 2 Y^2/ 3 X^2 )"));
            Results.Add(string.Format("                                      = {0:f3} * (1 + 2 x {1:f3} ^ 2 / (3 * {0:f3}^2))", X, Y));
            Results.Add(string.Format("                      len_parabola_1  = {0:f3} m", len_parabola_1));





            //=(D85-D95)*(1+(2*(E95-E85)^2/(3*(D85-D95)^2)))

            //14 - 24

            X = (lst_x[14] - lst_x[24]);
            Y = (lst_Y_ord_Top[24] - lst_Y_ord_Top[14]);


            double len_parabola_2 = X * (1 + 2 * Y * Y / (3 * X * X));

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("X = {0:f3} - {1:f3} = {2:f3}", lst_x[14], lst_x[24], X));
            Results.Add(string.Format("Y = {0:f3} - {1:f3} = {2:f3}", lst_Y_ord_Top[24], lst_Y_ord_Top[14], Y));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            Results.Add(string.Format(""));
            Results.Add(string.Format("Length of second Parabola in elevation = X (1 + 2 Y^2/ 3 X^2 )"));
            Results.Add(string.Format("                                       = {0:f3} * (1 + 2 x {1:f3} ^ 2 / (3 * {0:f3}^2))", X, Y));
            Results.Add(string.Format("                       len_parabola_2  = {0:f3} m", len_parabola_1));


            double len_incl_ln = (Math.Sqrt(Math.Pow((lst_x[0] - lst_x[2]), 2) + Math.Pow((lst_Y_ord_Top[0] - lst_Y_ord_Top[2]), 2))) * 2;
            //=(SQRT((D71-D73)^2+(E71-E73)^2))*2
            Results.Add(string.Format("Length of Inclined Straight Line = (SQRT(({0:f3} - {1:f3})^2 + ({2:f3} - {3:f3})^2))*2", lst_x[0], lst_x[2], lst_Y_ord_Top[0], lst_Y_ord_Top[2]));
            Results.Add(string.Format("                  len_incl_ln    = {0:f3} m", len_incl_ln));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            double tot_cbl_len = len_parabola_1 + len_parabola_2 + len_incl_ln + 2 * _L[2];
            Results.Add(string.Format("Total cable length = {0:f3} + {1:f3} + {2:f3} + {3:f3}", len_parabola_1,
                len_parabola_2, len_incl_ln, 2 * _L[2]));
            Results.Add(string.Format("      tot_cbl_len  = {0:f3} m", tot_cbl_len));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            double P = avg_wgt_frc;

            double ext_each = (P * 9.80665 * tot_cbl_len * 1000000) / (tendon_area * Design.Es);
            Results.Add(string.Format("Extention at each end = (P x l) / (A x Es)"));
            Results.Add(string.Format("                      = ({0:f3}*9.80665 x {1:f3} x 10^6) / ({2:f3} x {3:f3})", P, tot_cbl_len, tendon_area, Design.Es));
            Results.Add(string.Format("                      = {0:f3} ", ext_each));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            double loss_due_slip = Design.Es * tendon_area * Design.S / 2;
            Results.Add(string.Format("Loss due slip = Es x A x S / 2 "));
            Results.Add(string.Format("              = {0:f3} x {1:f3} x {2} / 2 ", Design.Es, tendon_area, Design.S));
            Results.Add(string.Format("              = {0:f3} N-mm", loss_due_slip));
            Results.Add(string.Format(""));
            loss_due_slip = loss_due_slip / 9.80665 / 1000 / 1000;
            Results.Add(string.Format("              = {0:f3} T-m", loss_due_slip));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("'Let the Slip travels upto a distance \"Ls\" beyond point {0} ", sections[11]));
            Results.Add(string.Format("in Span L1 i.e. at distance of {0:f3} m from jacking end", lst_x[11]));


            Results.Add(string.Format(""));
            //Results.Add(string.Format("Drop in Force from    Face of diaphragm in Span L1 i.e. at distance of 12.750 m from jacking end to  Null point"));
            Results.Add(string.Format("Drop in Force from  {0} in Span L1 i.e. at distance ", sections[11]));
            Results.Add(string.Format("of {0}  m from jacking end to  Null point", lst_x[11]));
            Results.Add(string.Format(""));

            //(J82 - J81)/(D81 - D82)
            double drop_force = (lst_F[11] - lst_F[10]) / (lst_x[10] - lst_x[11]);
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Half of area of shaded portion"));
            Results.Add(string.Format(""));

            //Half of area of shaded portion

            List<double> lst_half_area = new List<double>();
            List<double> lst_vals = new List<double>();


            for (int i = lst_F.Count - 1; i > 11; i--)
            {
                val1 = lst_F[i];
                val2 = lst_F[i - 1];


                if (val1 == val2)
                    val = 0;
                else
                    val = lst_x[i - 1] - lst_x[lst_F.Count - 1];


                if (val2 == 0.0)
                {
                    val1 = 0.0;
                    val = 0.0;
                }
                else if (i >= 14 && i <= 24)
                {
                    //=IF(F222=0,0,IF(D94=D95,D94-D95,D94-D95))
                    val = 0.0;
                    if (lst_x[i - 1] == lst_x[24])
                        val = lst_x[i - 1] - lst_x[24];
                    else
                        val = lst_x[i - 1] - lst_x[i];
                }
                else if (i >= 11 && i <= 13)
                {

                    //=IF(F255=0,0,IF(D83=$D$73,$D$73-D84,D83-D84))
                    val = 0.0;
                    if (lst_x[i - 1] == lst_x[2])
                        val = lst_x[2] - lst_x[i];
                    else
                        val = lst_x[i - 1] - lst_x[i];
                }


                if (i == lst_F.Count - 1)
                {
                    Results.Add(string.Format("      = (({0:f3} - {1:f3}) / 2) x {2:f3} ", val1, val2, val));
                }
                else
                {
                    Results.Add(string.Format("        + (({0:f3} - {1:f3}) / 2) x {2:f3} ", val1, val2, val));
                }


                lst_vals.Add((val1 - val2));
                lst_half_area.Add(((val1 - val2) / 2) * val);

            }
            Results.Add(string.Format(""));
            Results.Add(string.Format("{0:f3}  x  LsxLs/2 +", drop_force));

            val2 = 0;
            for (int i = lst_F.Count - 1; i > 10; i--)
            {

                val1 = lst_F[i];
                val2 = lst_F[i - 1];

                //if (val1 == val2)
                //    val = 0;
                //else
                val = lst_x[i];


                if (val2 == 0.0)
                {
                    val1 = 0.0;
                    val = 0.0;
                }
                lst_half_area.Add((val1 - val2) * val);

                Results.Add(string.Format("        + (({0:f3} - {1:f3})) x {2:f3} ", val1, val2, val));
            }

            Results.Add(string.Format(""));
            Results.Add(string.Format("          + {0:f3} x Ls x {1:f3}", drop_force, lst_x[11]));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            val1 = drop_force / 2.0;
            val2 = drop_force * lst_x[11];


            Results.Add(string.Format("    {0:f3} x Ls^2 + {1:f3} x Ls + {2:f3}", val1, val2, lst_half_area[0]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[1], lst_half_area[2], lst_half_area[3]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[4], lst_half_area[5], lst_half_area[6]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[7], lst_half_area[8], lst_half_area[9]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[10], lst_half_area[11], lst_half_area[12]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[13], lst_half_area[14], lst_half_area[15]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[16], lst_half_area[17], lst_half_area[18]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[19], lst_half_area[20], lst_half_area[21]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[22], lst_half_area[23], lst_half_area[24]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3}", lst_half_area[25], lst_half_area[26], lst_half_area[27]));
            Results.Add(string.Format("                  + {0:f3} + {1:f3} + {2:f3} ", lst_half_area[28], lst_half_area[29], lst_half_area[30]));
            Results.Add(string.Format(""));
            Results.Add(string.Format("                  = {0:f3}", loss_due_slip));
            Results.Add(string.Format(""));

            val = MyList.Get_Array_Sum(lst_half_area);
            Results.Add(string.Format(""));

            Results.Add(string.Format("=>   {0:f3} x Ls^2 + {1:f3} x Ls + {2:f3} = {3:f3}", val1, val2, val, loss_due_slip));
            Results.Add(string.Format(""));

            
            double a = val1;
            double b = val2;
            double c = val - loss_due_slip;
            Results.Add(string.Format(""));
            Results.Add(string.Format("=>   {0:f3} x Ls^2 + {1:f3} x Ls + ({2:f3}) = 0", a, b, c));


            b = b / a;
            c = c / a;
            a = 1;
            Results.Add(string.Format("=>   {0:f3} x Ls^2 + {1:f3} x Ls + ({2:f3}) = 0", a, b, c));

            double Ls = Math.Abs((-b + Math.Pow((b * b - 4 * a * c), 0.5)) / (2 * a));

            Results.Add(string.Format("Ls = ((-({0:f3}) + SQRT(({0:f3}^2 - 4 * {1:f3} * {2:f3}))) / (2 * {1:f3}))", b, a, c));
            Results.Add(string.Format(""));
            Results.Add(string.Format("   = {0:f3} m", Ls));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            double drp_frc = drop_force * Ls;

            Results.Add(string.Format("Drop in Force from 9L1/10 to Null Point = {0:f3} x {1:f3} = {2:f3} Ton", drop_force, Ls, drp_frc));
            Results.Add(string.Format(""));

            val = lst_F[11] - (drop_force * Ls);
            double frc_null_pnt = val;

            Results.Add(string.Format("Therefore, Force at Null Point  =  {0:f3} - ({1:f3} * {2:f3}) = {3:f3} Ton", lst_F[11], drop_force, Ls, val));

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));



            List<double> lst_F2 = new List<double>();
            List<double> lst_Fh2 = new List<double>();
            List<double> lst_Fv2 = new List<double>();

            List<double> lst_Y_Ord_bottom = new List<double>();

            
            for (int i = 0; i < lst_x.Count ; i++)
            {
                if (i == 0)
                {
                    val = lst_F[i];
                }
                else if (i == 1)
                {
                    if (lst_x[1] == lst_x[0])
                        val = 0;
                    else
                        val = lst_F[i];
                }
                else if (i == 2)
                {
                    val = lst_F[i];
                }
                else if (i >= 3 && i <= 10)
                {

                    if (lst_x[i] == lst_x[2])
                        val = 0;
                    else
                        val = lst_F[i];
                }
                else if (i >= 11 && i <= 13)
                {
                    if (lst_x[i] == lst_x[2])
                        val = 0;
                    else
                        val = lst_F[i] - 2 * (lst_F[i] - frc_null_pnt);
                }
                else if (i >= 14 && i <= 23)
                {
                    if (lst_x[i] == lst_x[24])
                        val = 0;
                    else
                        val = lst_F[i] - 2 * (lst_F[i] - frc_null_pnt);
                }
                else if (i == 24)
                {
                    val = lst_F[i] - 2 * (lst_F[i] - frc_null_pnt);
                }
                else if (i == 25)
                {
                    if (lst_x[25] == lst_x[26])
                        val = 0;
                    else
                        val = lst_F[i] - 2 * (lst_F[i] - frc_null_pnt);
                }
                else if (i == 26)
                {
                    val = lst_F[i] - 2 * (lst_F[i] - frc_null_pnt);
                }
                else
                    val = lst_F[i];

                if (i > 23)
                    val1 = 0.0;


                lst_F2.Add(val);
                lst_Fh2.Add(val * Math.Cos(lst_Incln_Elevn[i]));
                lst_Fv2.Add(val * Math.Sin(lst_Incln_Elevn[i]));


                lst_Y_Ord_bottom.Add(Design.Mid_Cross_Section.CS_D9/1000.0 - lst_Y_ord_Top[i]);

            }

            Results.Add(string.Format(""));


            dgv_slip_loss.Rows.Clear();

            for (int i = 0; i < sections.Count; i++)
            {
                dgv_slip_loss.Rows.Add(sections[i],
                    lst_x[i],
                    lst_Y_ord_Top[i],
                    lst_Y_Ord_bottom[i],
                    lst_Incln_Elevn[i],
                    lst_F2[i],
                    lst_Fh2[i],
                    lst_Fv2[i]);
            }


            for (int i = 0; i < dgv_slip_loss.RowCount; i++)
            {

                for (int j = 1; j < dgv_slip_loss.ColumnCount; j++)
                {
                    if (j > 1 && j < 5)
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

            lst_avg_frcs.Clear();

            Calculation_Details_Friction = new List<string>(Results.ToArray());
            Results.Clear();

            Results.Add(string.Format("Weighted average force = (1 / {0:f3})", lst_x[0]));

            for (int i = 1; i < sections.Count; i++)
            {
                val = ((lst_F2[i - 1] + lst_F2[i]) / 2.0) * (lst_x[i - 1] - lst_x[i]);

                if (i == 1)
                    Results.Add(string.Format("                             x  ((({0:f3} + {1:f3}) / 2) x {2:f3}", lst_F[i - 1], lst_F[i], lst_x[i - 1] - lst_x[i]));
                else if (i == sections.Count - 1)
                    Results.Add(string.Format("                            +  (({0:f3} + {1:f3}) / 2) x {2:f3}", lst_F[i - 1], lst_F[i], lst_x[i - 1] - lst_x[i]));
                else
                    Results.Add(string.Format("                            +  (({0:f3} + {1:f3}) / 2) x {2:f3})", lst_F[i - 1], lst_F[i], lst_x[i - 1] - lst_x[i]));

                lst_avg_frcs.Add(val);
            }

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            val = 1 / lst_x[0];

            Results.Add(string.Format("Weighted average force =   {0:f3} x ({1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                                                                val, lst_avg_frcs[0], lst_avg_frcs[1], lst_avg_frcs[2], lst_avg_frcs[3], lst_avg_frcs[4], lst_avg_frcs[5]));


            Results.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                lst_avg_frcs[6], lst_avg_frcs[7], lst_avg_frcs[8], lst_avg_frcs[9], lst_avg_frcs[10], lst_avg_frcs[11]));
            
            Results.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                                                                         lst_avg_frcs[12], lst_avg_frcs[13], lst_avg_frcs[14], lst_avg_frcs[15], lst_avg_frcs[16], lst_avg_frcs[17]));


            Results.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3}",
                                                                         lst_avg_frcs[18], lst_avg_frcs[19], lst_avg_frcs[20], lst_avg_frcs[21], lst_avg_frcs[22]));


            Results.Add(string.Format("                                  + {0:f3} + {1:f3} + {2:f3})", lst_avg_frcs[23], lst_avg_frcs[24], lst_avg_frcs[25]));


            P = val * MyList.Get_Array_Sum(lst_avg_frcs);

            Results.Add(string.Format(""));
            Results.Add(string.Format("                    P  =   {0:f3} T", P));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            Calculation_Details_Slip = new List<string>(Results.ToArray());

            Set_Results();
        }
        private void Set_Results()
        {
            string format = "{0,-43} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4}";

            int column = 0;
            Results.Clear();
            Results.Add(string.Format(""));
            Results.Add(string.Format("---------------------------------------------------"));
            Results.Add(string.Format("CALCULATION OF LOSSES IN CAP CABLE {0} ", CableNo));
            Results.Add(string.Format("---------------------------------------------------"));
            Results.Add(string.Format(""));
            Results.AddRange(Input_Details.ToArray());
            Results.Add(string.Format(""));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION LOSS"));
            Results.Add(string.Format("---------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                     Dist from    Y Ord     Incln    θ=θ1+θ2      µθ        Kx        F        Fh         Fv"));
            Results.Add(string.Format("                                            Jacking,x              Elevn.                         Plan"));
            Results.Add(string.Format("                                               (m)        (m)       (rad)     (rad)                (m)      (T)       (T)        (T)"));
            Results.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("")); for (int i = 0; i < dgv_friction_loss.RowCount; i++)
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
                    dgv_friction_loss[column++, i].Value));
            }
            Results.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------"));


            Results.AddRange(Calculation_Details_Friction.ToArray());
            format = "{0,-43} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f2} {6,9:f2} {7,9:f2}";

            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("FORCE AFTER FRICTION AND SLIP LOSS"));
            Results.Add(string.Format("--------------------------------------"));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("Section                                     Dist from    Y Ord     Y Ord       Incln     F         Fh         Fv"));
            Results.Add(string.Format("                                            Jacking,x  from Top  from Bottom   Elevn.                           "));
            Results.Add(string.Format("                                               (m)        (m)       (m)        (rad)       (T)     (T)        (T)"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("")); for (int i = 0; i < dgv_slip_loss.RowCount; i++)
            {
                column = 0;

                Results.Add(string.Format(format,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value,
                    dgv_slip_loss[column++, i].Value));
            }
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------------------"));
            Results.AddRange(Calculation_Details_Slip.ToArray());
        }

    }

    public class Force_After_Friction_Loss_Cap_Cable_Table : List<Force_After_Friction_Loss_Cap_Cable>
    {
        public Force_After_Friction_Loss_Cap_Cable_Table()
            : base()
        {

        }

        public void Data_Retrieve_from_Grid(DataGridView dgv_friction_loass)
        {
            int column = 0;
            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f4} {5,9:f4} {6,9:f4} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f2} {13,9:f2}";

            Force_After_Friction_Loss_Cap_Cable data = null;
            this.Clear();
            for (int i = 0; i < dgv_friction_loass.RowCount; i++)
            {
                column = 0;
                data = new Force_After_Friction_Loss_Cap_Cable();

                data.Section = dgv_friction_loass[column++, i].Value.ToString();
                data.X = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Y_Ord = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Incln_Elevn = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
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
        public Force_After_Friction_Loss_Cap_Cable Get_Friction_Data(string section)
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
    public class Force_After_Friction_Slip_Loss_Cap_Cable_Table : List<Force_After_Friction_Slip_Loss_Cap_Cable>
    {
        public Force_After_Friction_Slip_Loss_Cap_Cable_Table()
            : base()
        {

        }
        public void Data_Retrieve_from_Grid(DataGridView dgv_friction_loass)
        {
            int column = 0;

            Force_After_Friction_Slip_Loss_Cap_Cable data = null;
            this.Clear();
            for (int i = 0; i < dgv_friction_loass.RowCount; i++)
            {
                column = 0;
                data = new Force_After_Friction_Slip_Loss_Cap_Cable();

                data.Section = dgv_friction_loass[column++, i].Value.ToString();
                data.X = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Y_Ord_Top = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
                data.Y_Ord_Bottom = MyList.StringToDouble(dgv_friction_loass[column++, i].Value.ToString(), 0.0);
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

            string format = "{0,-40} {1,9:f3} {2,9:f4} {3,9:f4} {4,9:f2} {5,9:f2} {6,9:f2} {7,9:f2}";
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
                    this[i].Y_Ord_Top,
                    this[i].Y_Ord_Bottom,
                    this[i].Incln_Elevn,
                    this[i].F,
                    this[i].Fh,
                    this[i].Fv));
            }
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            Results.Add(string.Format("------------------------------------------------------------------------------------------------------"));

            return Results;

        }

        public Force_After_Friction_Slip_Loss_Cap_Cable Get_Slip_Loss_Data(string section)
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


    public class Force_After_Friction_Loss_Cap_Cable
    {
        public Force_After_Friction_Loss_Cap_Cable()
        {
            Section = "";
            X = 0.0;
            Y_Ord = 0.0;
            Incln_Elevn = 0.0;
            Theta = 0.0;
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
        public double Theta { get; set; }
        public double Mu_Theta { get; set; }
        public double Kx { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
    }
    public class Force_After_Friction_Slip_Loss_Cap_Cable
    {
        public Force_After_Friction_Slip_Loss_Cap_Cable()
        {
            Section = "";
            X = 0.0;
            Y_Ord_Top = 0.0;
            Y_Ord_Bottom = 0.0;
            Incln_Elevn = 0.0;
            F = 0.0;
            Fh = 0.0;
            Fv = 0.0;
        }
        public string Section { get; set; }
        public double X { get; set; }
        public double Y_Ord_Top { get; set; }
        public double Y_Ord_Bottom { get; set; }
        public double Incln_Elevn { get; set; }
        public double F { get; set; }
        public double Fh { get; set; }
        public double Fv { get; set; }
    }

    public class Cap_Cable_Profile_Data
    {
        #region Propeties

        /// <summary>
        /// Number of Cables
        /// </summary>
        //public List<int> NC { get; set; }

        public double Adopt_Cable { get; set; }
        public int NC { get; set; }
        public double Duct_Dia { get; set; }
        public double UTS { get; set; }
        public double Area { get; set; }
        public double P { get; set; }
        public double mpf { get; set; }

        public List<double> _L { get; set; }
        public List<double> _D { get; set; }
        #endregion Propeties

        public Force_After_Friction_Loss_Cap_Cable_Table Table_Friction_Loss { get; set; }
        public Force_After_Friction_Slip_Loss_Cap_Cable_Table Table_Friction_Slip_Loss { get; set; }

        public Cap_Cable_Profile_Data()
        {
            //NC = new List<int>();
            NC = 0;

            Table_Friction_Loss = new Force_After_Friction_Loss_Cap_Cable_Table();
            Table_Friction_Slip_Loss = new Force_After_Friction_Slip_Loss_Cap_Cable_Table();
        }
    }


}