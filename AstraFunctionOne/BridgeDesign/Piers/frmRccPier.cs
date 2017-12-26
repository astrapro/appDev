using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace AstraFunctionOne.BridgeDesign.Piers
{
    public partial class frmRccPier : Form
    {
        IApplication iapp = null;

        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        bool is_process = false;

        public frmRccPier(IApplication app)
        {
            InitializeComponent();
            this.iapp = app;
        }
        public void Calculate_Program()
        {
            double L1, W1, W2, W3, W4, W5, total_vehicle_load;
            double w1, w2, w3;
            double D1, D2, D3;
            double RL6, RL5, RL4, RL3, RL2, RL1;
            double H1, H2, H3, H4, H5, H6, H7, H8;
            double B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, B14, B15, B16;
            double NR, NP, gama_c, MX1, MY1;


            L1 = 0.0d;
            W1 = 0.0d;
            W2 = 0.0d;
            W3 = 0.0d;
            W4 = 0.0d;
            W5 = 0.0d;
            total_vehicle_load = 0.0d;
            D1 = 0.0d;
            D2 = 0.0d; 
            D3 = 0.0d;

            RL6 = 0.0d;
            RL5 = 0.0d;
            RL4 = 0.0d;
            RL3 = 0.0d;
            RL2 = 0.0d;
            RL1 = 0.0d;
            H1 = 0.0d;
            H2 = 0.0d;
            H3 = 0.0d;
            H4 = 0.0d;
            H5 = 0.0d;
            H6 = 0.0d;
            H7 = 0.0d;
            H8 = 0.0d;
            B1 = 0.0d;
            B2 = 0.0d;
            B3 = 0.0d;
            B4 = 0.0d;
            B5 = 0.0d;
            B6 = 0.0d;
            B7 = 0.0d;
            B8 = 0.0d;
            B9 = 0.0d;
            B10 = 0.0d;
            B11 = 0.0d;
            B12 = 0.0d;
            B13 = 0.0d;
            B14 = 0.0d;
            B15 = 0.0d;
            B16 = 0.0d;
            NR = 0.0d;
            NP = 0.0d;
            gama_c = 0.0d;
            MX1 = 0.0d;
            MY1 = 0.0d;


            L1 = MyList.StringToDouble(txt_L1.Text, 0.0);
            w1 = MyList.StringToDouble(txt_w1.Text, 0.0);
            w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            w3 = MyList.StringToDouble(txt_w3.Text, 0.0);

            double a1, NB, fy1, fy2,p1,p2,d_dash, Pu,Mux,Muy;
            double d1, d2,fck1,fck2,perm_flex_stress;

           
            a1 = MyList.StringToDouble(txt_a1.Text, 0.0);
            NB = MyList.StringToDouble(txt_NB.Text, 0.0);
            d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            gama_c = MyList.StringToDouble(txt_gama_c.Text, 0.0);
            B1 = MyList.StringToDouble(txt_B1.Text, 0.0);
            B2 = MyList.StringToDouble(txt_B2.Text, 0.0);
            H1 = MyList.StringToDouble(txt_H1.Text, 0.0);
            B3 = MyList.StringToDouble(txt_B3.Text, 0.0);
            B4 = MyList.StringToDouble(txt_B4.Text, 0.0);
            H2 = MyList.StringToDouble(txt_H2.Text, 0.0);
            B5 = MyList.StringToDouble(txt_B5.Text, 0.0);
            B6 = MyList.StringToDouble(txt_B6.Text, 0.0);
            RL1 = MyList.StringToDouble(txt_RL1.Text, 0.0);
            RL2 = MyList.StringToDouble(txt_RL2.Text, 0.0);
            RL3 = MyList.StringToDouble(txt_RL3.Text, 0.0);
            RL4 = MyList.StringToDouble(txt_RL4.Text, 0.0);
            RL5 = MyList.StringToDouble(txt_RL5.Text, 0.0);
            double form_lev = MyList.StringToDouble(txt_Form_Lev.Text, 0.0);
            B7 = MyList.StringToDouble(txt_B7.Text, 0.0);
            H3 = MyList.StringToDouble(txt_H3.Text, 0.0);
            H4 = MyList.StringToDouble(txt_H4.Text, 0.0);
            B8 = MyList.StringToDouble(txt_B8.Text, 0.0);
            //fck1 = MyList.StringToDouble(txt_fck_1.Text, 0.0);
            //perm_flex_stress = MyList.StringToDouble(txt_permis_flex_stress.Text, 0.0);
            //fy1 = MyList.StringToDouble(txt_fy1.Text, 0.0);
            H5 = MyList.StringToDouble(txt_H5.Text, 0.0);
            H6 = MyList.StringToDouble(txt_H6.Text, 0.0);
            H7 = MyList.StringToDouble(txt_H7.Text, 0.0);
            B9 = MyList.StringToDouble(txt_B9.Text, 0.0);
            B10 = MyList.StringToDouble(txt_B10.Text, 0.0);
            B11 = MyList.StringToDouble(txt_B11.Text, 0.0);
            B12 = MyList.StringToDouble(txt_B12.Text, 0.0);
            B13 = MyList.StringToDouble(txt_B13.Text, 0.0);
            B14 = MyList.StringToDouble(txt_B14.Text, 0.0);
            double over_all = H7 + H5 + H6;
            B15 = MyList.StringToDouble(txt_B15.Text, 0.0);
            B16 = MyList.StringToDouble(txt_B16.Text, 0.0);
            p1 = MyList.StringToDouble(txt_p1.Text, 0.0);
            p2 = MyList.StringToDouble(txt_p2.Text, 0.0);
            d_dash = MyList.StringToDouble(txt_d_dash.Text, 0.0);
            //fck2 = MyList.StringToDouble(txt_fck_2.Text, 0.0);
            //fy2 = MyList.StringToDouble(txt_fy2.Text, 0.0);
            double D = MyList.StringToDouble(txt_D.Text, 0.0);
            double b = MyList.StringToDouble(txt_b.Text, 0.0);

            //Pu = MyList.StringToDouble(txt_Pu.Text, 0.0);
            //Mux = MyList.StringToDouble(txt_Mux.Text, 0.0);
            //Muy = MyList.StringToDouble(txt_Muy.Text, 0.0);
            NP = MyList.StringToDouble(txt_NP.Text, 0.0);
            NR = MyList.StringToDouble(txt_NR.Text, 0.0);
            MX1 = MyList.StringToDouble(txt_Mx1.Text, 0.0);
            MY1 = MyList.StringToDouble(txt_Mz1.Text, 0.0);
            total_vehicle_load = MyList.StringToDouble(txt_vehi_load.Text, 0.0);
            W1 = MyList.StringToDouble(txt_W1_supp_reac.Text, 0.0);





            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*                  ASTRA Pro                  *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*           DESIGN  OF  RCC PIERS             *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion

            #region User Input

            list.Add(string.Format("USER'S DATA"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Support Reaction on the Pier for (DL+SIDL+LL) = W1 = {0} kN", W1));
            list.Add(string.Format("Moment at Supports in Longitudinal Direction [Mx1] = {0} kN-m", MX1));
            list.Add(string.Format("Moment at Supports in Transverse Direction [My1] = {0} kN-m", MY1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("C/C Distance between Piers = L1 = {0} m.", L1));
            list.Add(string.Format("Carriageway width = w1 = {0} m.", w1));
            list.Add(string.Format("Overall width of Deck = w2 = {0} m.",w2));
            list.Add(string.Format("Width of Crash Barrier = w3 = {0} m.",w3));
            list.Add(string.Format("Height of Crash Barrier = a1 = {0} m.",a1));
            list.Add(string.Format("Number of Bearings = NB = {0}",NB ));
            list.Add(string.Format("Depth of Girder = d1 = {0} m.", d1));
            list.Add(string.Format("Depth of Deck Slab = d2 = {0} m.", d2));
            list.Add(string.Format("Size of Pedestals = B1 x B2 x H1 = {0} m. x {1} m. x {2} m.", B1, B2, H1));
            list.Add(string.Format("Size of Bearings = B3 x B4 x H2 = {0} m. x {1} m. x {2} m.", B3, B4, H2));
            list.Add(string.Format("Distance Between Girders = B5 = {0} m.",B5));
            list.Add(string.Format("Length of Footing = B6 = {0} m.",B6));


            list.Add(string.Format("R.L. at Pier Cap Top = RL1 = {0} m.",RL1));
            list.Add(string.Format("High Flood Level (HFL) = RL2 = {0} m.",RL2));
            list.Add(string.Format("Existing Ground Level = RL3 = {0} m.",RL3));
            list.Add(string.Format("R.L. at Footing Top = RL4 = {0} m.",RL4));
            list.Add(string.Format("R.L. at Footing Bottom = RL5 = {0} m.",RL5));
            list.Add(string.Format("Formation Level=RL1+d1+d2+H1+H2={0} m.", form_lev));

            list.Add(string.Format("Width of Footing = B7 = {0} m.",B7));
            list.Add(string.Format("Straight Depth of Footing = H3 = {0} m.",H3));
            list.Add(string.Format("Varying Depth of Footing = H4 = {0} m.",H4));
            list.Add(string.Format("P.C.C. Projection under Footing on either side = B8 = {0} m.",B8));
            //list.Add(string.Format("Grade of Concrete = M{0} ",fck1));
            //list.Add(string.Format("Permissible Flexural Stress = {0}  N/Sq.mm.",perm_flex_stress));
            //list.Add(string.Format("Grade of Steel = Fe{0} ",fy1));
            list.Add(string.Format("Straight depth of Pier Cap = H5 = {0} m.",H5));
            list.Add(string.Format("Varying Depth of Pier Cap = H6 = {0} m.",H6));

            list.Add(string.Format("Longitudinal width of Pier at Base = B9 = {0} m.",B9));
            list.Add(string.Format("Transverse width of Pier at Base = B10 = {0} m.",B10));

            list.Add(string.Format("Longitudinal width of Pier at Top = B11 = {0} m.",B11));
            list.Add(string.Format("Transverse width of Pier at Top = B12 = {0} m.",B12));

            list.Add(string.Format("Pier Cap width in longitudinal direction = B13 = {0} m.",B13));
            list.Add(string.Format("Pier Cap width in transverse direction = B14 = {0} m.",B14));

            list.Add(string.Format("Overall Height of Substructure = H7 + H5 + H6 = {0} + {1} + {2} = {3} m. ", H7, H5, H6, over_all));
            //list.Add(string.Format("                               = {0} + {1} + {2} ",H7,H5,H6));
            //list.Add(string.Format("                              "));

            list.Add(string.Format(""));
            list.Add(string.Format("Distance B15 = {0} m.", B15));
            list.Add(string.Format("Distance B16 = {0} m.",B16));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            #endregion


            list.Add(string.Format("DESIGN CALCULTIONS"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double volume = B1 * B2 * H1;
            list.Add(string.Format("Pedestals : Volume = B1 * B2 * H1"));
            list.Add(string.Format("                   = {0} * {1} * {2}", B1, B2, H1));
            list.Add(string.Format("                   = {0} cu.m.", volume));
            list.Add(string.Format(""));

            double total_volume = NR * NP * volume;

            list.Add(string.Format("      Total Volume = NR * NP * {0}", volume));
            list.Add(string.Format("                   = {0} cu.m.", total_volume));
            list.Add(string.Format(""));
            double total_weight = total_volume * gama_c;
            W2 = total_weight;

            list.Add(string.Format("Total Weight = W2 =  {0} * γ_c", total_volume));
            list.Add(string.Format("                  = {0} * {1}", total_volume, gama_c));
            list.Add(string.Format("                  = {0} kN", W2));
            list.Add(string.Format(""));

            double bed_block_volume = ((B14 * (H5 + H6)) - (B16 * H6 * 2 / 2.0)) * B13;
            list.Add(string.Format("Pier Cap Bed Block :"));
            list.Add(string.Format(""));
            list.Add(string.Format("   Volume = ((B14 * (H5 + H6)) - (B16 * H6 * 2 / 2)) * B13"));
            list.Add(string.Format("          = (({0} * ({1} + {2})) - ({3} * {2} * 2 / 2)) * {4}",
                B14, H5, H6, B16, B13));
            list.Add(string.Format("          = {0:f3} cu.m.", bed_block_volume));
            list.Add(string.Format(""));
            total_weight = bed_block_volume * gama_c;
            W3 = total_weight;
            list.Add(string.Format("Total Weight = W3  =  {0} * γ_c", bed_block_volume));
            list.Add(string.Format("                   = {0} * {1}", bed_block_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pier :"));
            list.Add(string.Format("------"));
            list.Add(string.Format(""));
            double pier_volume = ((B9 * B10 + B11 * B12) / 2.0) * H7;
            list.Add(string.Format("Volume = ((B9 * B10 + B11 * B12)/2) * H7"));
            list.Add(string.Format("       = (({0} * {1} + {2} * {3})/2) * {4}", B9, B10, B11, B12, H7));
            list.Add(string.Format("       = {0} cu.m.", pier_volume));
            list.Add(string.Format(""));
            total_weight = pier_volume * gama_c;
            W4 = total_weight;
            list.Add(string.Format("Total Weight = W4  =  {0} * γ_c", pier_volume));
            list.Add(string.Format("                   = {0} * {1}", pier_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W4));
            list.Add(string.Format(""));


            list.Add(string.Format("Footing :"));
            list.Add(string.Format("---------"));
            W5 = total_weight;
            double footing_volume = ((B6 * B7 * H4) + ((B6 * B7 + B11 * B12) / 2.0) * H3);
            list.Add(string.Format("Volume = (B6 * B7 * H4) + ((B6 * B7 + B11 * B12) / 2.0) * H3"));
            list.Add(string.Format("       = ({0} * {1} * {2}) + (({0} * {1} + {3} * {4}) / 2.0) * {5}", B6, B7, H4, B11, B12, H3));
            list.Add(string.Format("       = {0} cu.m.", footing_volume));
            list.Add(string.Format(""));
            total_weight = footing_volume * gama_c;
            W5 = total_weight;
            list.Add(string.Format("Total Weight = W5  =  {0} * γ_c", footing_volume));
            list.Add(string.Format("                   = {0} * {1}", footing_volume, gama_c));
            list.Add(string.Format("                   = {0} kN", W5));
            list.Add(string.Format(""));
            double top_vert_load = W1 + W2 + W3 + W4;
            Pu = top_vert_load;
            list.Add(string.Format("Total Vertical Load at Top of Footing = W1 + W2 + W3 + W4"));
            list.Add(string.Format("                                      = {0} + {1} + {2} + {3}", W1, W2, W3, W4));
            list.Add(string.Format("                                      = {0} kN", top_vert_load));
            list.Add(string.Format(""));
            double bott_vert_load = W1 + W2 + W3 + W4 + W5;
            list.Add(string.Format("Total Vertical Load at Bottom of Footing = W1 + W2 + W3 + W4 + W5"));
            list.Add(string.Format("                                      = {0} + {1} + {2} + {3} + {4}", W1, W2, W3, W4, W5));
            list.Add(string.Format("                                      = {0} kN", bott_vert_load));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment at Supports in Longitudinal Direction = Mx1 = {0} kN-m", MX1));
            list.Add(string.Format("Moment at Supports in Transverse Direction = My1 = {0} kN-m", MY1));
            list.Add(string.Format(""));
            list.Add(string.Format("Breaking Force :"));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Vehicle Load = {0} kN", total_vehicle_load));
            double Fh = total_vehicle_load * 0.2d;
            list.Add(string.Format(""));
            list.Add(string.Format("  Fh = Breaking Force 20% = {0} kN", Fh));
            list.Add(string.Format(""));
            list.Add(string.Format("Temparature & Shrinkage Force on Bearings"));
            double Vr = 2.5d;
            double Itc = 4.21d;
            list.Add(string.Format(""));
            list.Add(string.Format("   Vr = {0} kN/mm", Vr));
            list.Add(string.Format("  Itc = 4.21 mm ", Itc));
            list.Add(string.Format(""));
            double force_each_bearing = (Fh / 2.0) + (Vr * Itc);
            list.Add(string.Format("Force at each Bearing = (Fh/2) + (Vr * Itc)"));
            list.Add(string.Format("                      = ({0}/2) + ({1} * {2})", Fh, Vr, Itc));
            list.Add(string.Format("                      = {0} kN", Fh, Vr, Itc));
            list.Add(string.Format(""));
            double total_force = NP * force_each_bearing;
            list.Add(string.Format("Total Force for Four Bearings = {0} * {1}", NP, force_each_bearing));
            list.Add(string.Format("                              = {0} kN", total_force));
            list.Add(string.Format(""));
            double HF1 = H1 + H2 + H5 + H6 + H7;
            list.Add(string.Format("Height from Top of Footing to Top of Bearing = HF1 = H1 + H2 + H5 + H6 + H7"));
            list.Add(string.Format("      HF1 = {0} + {1} + {2} + {3} + {4}", H1, H2, H5, H6, H7));
            list.Add(string.Format("          = {0} m", HF1));
            list.Add(string.Format(""));
            double HF2 = H1 + H2 + H3 + H4 + H5 + H6 + H7;
            list.Add(string.Format("Height from Top of Footing to Top of Bearing = HF2 = H1 + H2 + H3 + H4 + H5 + H6 + H7"));
            list.Add(string.Format("      HF2 = {0} + {1} + {2} + {3} + {4} + {5} + {6}", H1, H2, H3, H4, H5, H6, H7));
            list.Add(string.Format("          = {0} m", HF2));
            list.Add(string.Format(""));
            double M1 = total_force * HF1;
            list.Add(string.Format("Moment at Top of Footing = M1 = {0} * {1}", total_force, HF1));
            list.Add(string.Format("                         = {0} kN-m", M1));
            list.Add(string.Format(""));
            double M2 = total_force * HF2;
            list.Add(string.Format("Moment at Bottom of Footing   = M2 = {0} * {1}", total_force, HF2));
            list.Add(string.Format("                              = {0} kN-m", M2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("At Top of Footing,"));
            double Mx = MX1 + M1;
            list.Add(string.Format("Total Moment in Longitudinal Direction = Mx = Mx1 + M1"));
            list.Add(string.Format("                                            = {0} + {1}", MX1, M1));
            list.Add(string.Format("                                            = {0} kN-m", Mx));
            list.Add(string.Format(""));
            double Mz = MY1 + M1;
            list.Add(string.Format("Total Moment in Transverse Direction = My = My1 + M1"));
            list.Add(string.Format("                                           = {0} + {1}", MY1, M1));
            list.Add(string.Format("                                           = {0} kN-m", Mz));
            list.Add(string.Format(""));
            list.Add(string.Format("At Bottom of Footing,"));
            list.Add(string.Format(""));
            Mx = MX1 + M2;
            Mux = Mx;
            list.Add(string.Format("Total Moment in Longitudinal Direction = Mux = Mx1 + M2"));
            list.Add(string.Format("                                       = {0} + {1}", MX1, M2));
            list.Add(string.Format("                                       = {0} kN-m", Mx));
            list.Add(string.Format(""));
            Mz = MY1 + M2;
            Muy = Mz;
            list.Add(string.Format("Total Moment in Transverse Direction = Muy = My1 + M2"));
            list.Add(string.Format("                                       = {0} + {1}", MY1, M2));
            list.Add(string.Format("                                       = {0} kN-m", Muy));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Reinforcements:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Standard Minimum Reinforcement = p1 = {0} %",p1));
            list.Add(string.Format("Design Trial Reinforcement = p2 = {0} %",p2));
            list.Add(string.Format("Reinforcement Cover = d’ = {0} mm.",d_dash));
            //list.Add(string.Format("Concrete grade = M{0} fck={0} N/Sq.mm",fck1));
            //list.Add(string.Format("Steel grade=Fe415 fy={0}  N/Sq.mm",fy1));
            list.Add(string.Format("Width of Pier in Transverse direction = D = {0} m. = {1} mm.",D/1000,D));
            list.Add(string.Format("Width of Pier in Longitudinal direction = b ={0} m. = {1} mm.",b/1000,b));
            //D = D*1000;
            //b = b*1000;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("DESIGN FORCES:"));
            list.Add(string.Format("--------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pu={0} kN.",Pu));
            list.Add(string.Format("Mux={0} kN-m.",Mux));
            list.Add(string.Format("Muy={0} kN-m.",Muy));
            list.Add(string.Format(""));

            fck2 = 0;
            double p = p1 * p2;
            list.Add(string.Format("Reinforcement Percentage = p = p1 x p2 = {0:f2} x {1:f2} = {2:f3} %",p1,p2,p));
            list.Add(string.Format(""));
            double val1 = p / fck2;
            list.Add(string.Format("p/fck = {0:f3}/{1} = {2:f3}", p, fck2, val1));
            list.Add(string.Format(""));

            list.Add(string.Format("Uniaxial Moment capacity of the section about X_X Axis"));
            list.Add(string.Format(""));

            double val2 = d_dash / D;
            list.Add(string.Format("d’/D = {0}/{1} = {2:f4}", d_dash,D,val2));
            list.Add(string.Format(""));

            //list.Add(string.Format("From Interaction diagram chart for Fe 415,   and for  d’/D = 0.0067, refer for d’/D = 0.05 is to be used,"));
            if (val2 < 0.05)
            {
                list.Add(string.Format("From Interaction diagram chart for Fe {0},   and for  d’/D = {1:f4}, refer for d’/D = 0.05 is to be used,", fy2 = 0, val2));
                val2 = 0.05;
            }
            else
            {
                list.Add(string.Format("From Interaction diagram chart for Fe {0},   and for  d’/D = {1:f4}", fy2 =415, val2));
            }
            list.Add(string.Format(""));

            double val3 = (Pu * 1000.0) / (fck2 * b * D);
            //list.Add(string.Format("For        Pu/(fck x b x D) = 5918 x 1000 / (30 x 1000 x 6000) = 0.0329 and p/fck = 0.042"));
            list.Add(string.Format("For        Pu/(fck x b x D)"));
            list.Add(string.Format("         = ({0} x 1000) / ({1} x {2} x {3})", Pu, fck2, b, D));
            list.Add(string.Format("         = {0:f4} and p/fck = {1:f4}", val3, val1));
            list.Add(string.Format(""));


            frmUserInput f_us_inp = new frmUserInput();
            f_us_inp.SetText1 = string.Format("Refer to Interaction diagram chart for Fe 415   and   for  d’/D = {0}", val2);
            f_us_inp.SetText2 = string.Format("For values of: Pu/(fck x b x D) = {0:f4} and the value of: p/fck = {1:f4}", val3, val1);
            f_us_inp.SetText3 = string.Format("We get the value of: Mux1 / (fck x b x D x D)");
            f_us_inp.InputValue = 0.075;
            f_us_inp.ShowDialog();
            double inp_val1 = f_us_inp.InputValue;


            double Mux1 = inp_val1 * (fck2 * b * D * D);
            list.Add(string.Format("Mux1 / (fck x b x D x D) = {0}", inp_val1));
            list.Add(string.Format("Mux1 = {0} x fck x b x D x D ", inp_val1));
            list.Add(string.Format("     = {0} x {1} x {2} x {2} x {3} ", inp_val1, fck2, b, D));
            list.Add(string.Format("     = {0:E3} N-mm.", Mux1));
            Mux1 = Mux1 / 10E5;
            list.Add(string.Format("     = {0:E3} kN-m.",Mux1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Uniaxial Moment capacity of the section about Z_Z Axis"));

            double val4 = d_dash / b;

            list.Add(string.Format("d’/D = d’/b = {0}/{1} = {2:f3}", d_dash, b, val4));
            list.Add(string.Format(""));

            //User Input
            //f_us_inp = new frmUserInput();
            //f_us_inp.SetText1 = "Refer to Interaction diagram chart for Fe 415   and   for  d’/D = ";
            //f_us_inp.InputValue = 0.05;
            //f_us_inp.ShowDialog();
            //double inp_val1 = f_us_inp.InputValue;
            list.Add(string.Format("From Interaction diagram chart for Fe {0},", fy2));
            list.Add(string.Format("and for  d’/D = {0}, refer for d’/D = {0} is to be used,", inp_val1));
            list.Add(string.Format(""));


            //For values of: Pu/(fck x b x D) = 0.0329 and the value of: p/fck = 0.042
            double val5 = (Pu * 1000) / (fck2 * b * D);
            list.Add(string.Format("For     Pu/(fck x b x D)"));
            list.Add(string.Format("      = {0} x 1000 / ({1} x {2} x {3})", Pu, fck2, b, D));
            list.Add(string.Format("      = {0:f4} and p/fck = {1:f4}", val5, val1));
            list.Add(string.Format(""));

            //f_us_inp = new frmUserInput();
            //f_us_inp.SetText1 = "We get the value of:  Mux1 / (fck x b x D x D) = ";
            //f_us_inp.InputValue = 0.075;
            //f_us_inp.ShowDialog();
            //double inp_val3 = f_us_inp.InputValue;

            double Muy1 = inp_val1 * (fck2 * b * D * D);
            list.Add(string.Format("Muy1 / (fck x b x D x D) = {0}", inp_val1));
            list.Add(string.Format("Muy1 = {0} x fck x b x D x D ", inp_val1));
            list.Add(string.Format("     = {0} x {1} x {2} x {2} x {3} ", inp_val1, fck2, b, D));
            list.Add(string.Format("     = {0:E1} N-mm.", Muy1));
            Muy1 = Muy1 / 10E5;
            list.Add(string.Format("     = {0:f3} kN-m.", Muy1));
            list.Add(string.Format(""));
            list.Add(string.Format("Same as about X_X Axis,"));

            list.Add(string.Format(""));
            list.Add(string.Format("For     p = {0}%, fy = {1}, fck = {2}   N/Sq.mm", p, fy2, fck2));

            list.Add(string.Format(""));
            //User Input
            f_us_inp = new frmUserInput();
            f_us_inp.SetText1 = "";
            f_us_inp.SetText2 = string.Format("Refer to Interaction diagram chart for p = {0}%, fy = {1}, fck={2} N/Sq.mm", p, fy2, fck2);
            f_us_inp.SetText3 = string.Format("We get the value of:   Puz / Ag");
            f_us_inp.InputValue = 16.5;
            f_us_inp.ShowDialog();
            //double inp_val1 = f_us_inp.InputValue;
            inp_val1 = f_us_inp.InputValue;
            list.Add(string.Format("From Interaction diagram chart   Puz / Ag = {0}   N/Sq.mm.", inp_val1));
            double Puz = inp_val1 * b * D;
            list.Add(string.Format("Puz = {0} x {1} x {2}",inp_val1, b, D));
            list.Add(string.Format("    = {0} N", Puz));
            Puz = Puz / 1000.0;
            list.Add(string.Format("    = {0} kN", Puz));

            list.Add(string.Format(""));

            double val6 = Pu / Puz;
            list.Add(string.Format("Pu / Puz = {0} / {1} = {2:f4}", Pu, Puz, val6));
            double val7 = Mux / Mux1;
            list.Add(string.Format("Mux / Mux1 = {0:f4} / {1:f4} = {2:f4}", Mux, Mux1, val7));
            double val8 = Muy / Muy1;
            list.Add(string.Format("Muy / Muy1 = {0:f3} / {1:f3} = {2:f3}", Muy, Muy1, val8));

            list.Add(string.Format(""));
            //User Input
           
            list.Add(string.Format("From Interaction diagram chart   "));
            double val9 = Pu / Puz;
            double val10 = Muy / Muy1;
            double val11 = Mux / Mux1;
            list.Add(string.Format("For   Pu / Puz = {0:f4} = {0:f2}",val9));
            list.Add(string.Format("      Muy / Muy1 = {0:f4} = {0:f2}", val10));
            list.Add(string.Format("      Mux / Mux1 = {0:f4} = {0:f2}", val11));

            f_us_inp = new frmUserInput();
            f_us_inp.SetText1 = "";
            f_us_inp.SetText2 = string.Format("Refer to Interaction diagram chart for Pu / Puz = {0:f4} and Muy / Muy1 = {1:f4}", val9, val10);
            f_us_inp.SetText3 = string.Format("We get the value of: Mux / Mux1");
            f_us_inp.InputValue = 0.94;
            f_us_inp.ShowDialog();
            //double inp_val1 = f_us_inp.InputValue;
            inp_val1 = f_us_inp.InputValue;
           

            list.Add(string.Format(""));
            if (val11 > inp_val1)
            {
                list.Add(string.Format("This value is higher than actual values of Mux / Mux1 = {0},    So, OK", inp_val1));
            }
            else if (val11 < inp_val1)
            {
                list.Add(string.Format("This value is less than actual values of Mux / Mux1 = {0},    So, OK", inp_val1));
            }


            list.Add(string.Format(""));
            double req_area = p * b * D / 100.0;
            list.Add(string.Format("Required area for Steel Reinforcement = As = p x b x D / 100 "));
            list.Add(string.Format("                                      = {0} x {1} x {2} / 100", p, b, D));
            list.Add(string.Format("                                      = {0:f3} Sq.mm.", req_area));
            list.Add(string.Format(""));

            double d_d1 = 32;
            val3 = Math.PI * d_d1 * d_d1 / 4.0;

            double nos_bars = req_area / val3;
            //double nos_bars = 94;
            list.Add(string.Format("Provide {0:f0} numbers 32 mm. diameter steel reinforcement vertical bars ", nos_bars));
            list.Add(string.Format("all around the perimeter of the Pier,"));
            list.Add(string.Format(""));
            val1 = 2 * (b + D);
            list.Add(string.Format("Perimeter of the Pier = 2 x ({0} + {1}) = {2:f3} mm.", b, D, val1));
            val2 = val1 / nos_bars;
            list.Add(string.Format("Spacing of the bars = {0:f3} / {1:f0} = {2:f0} mm. marked as (A) in the drawing.", val1, nos_bars, val2));

            list.Add(string.Format(""));
            list.Add(string.Format("Provide 12 mm. diameter lateral ties in zig zug manner marked as (B) in the drawing."));
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            
            
            File.WriteAllLines(rep_file_name, list.ToArray());
            list.Clear();
            list = null;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            double MX1, MY1, W1;

            MX1 = MY1 = W1 = 0.0;

            MX1 = MyList.StringToDouble(txt_Mx1.Text, 0.0);
            MY1 = MyList.StringToDouble(txt_Mz1.Text, 0.0);
            W1 = MyList.StringToDouble(txt_W1_supp_reac.Text, 0.0);

            if (MX1 == 0.0 && MY1 == 0.0 && W1 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : W1  = 6101.1 kN\n";
                msg += "            : MX1 = 274.8 kN-m\n";
                msg += "            : MZ1 = 603.1 kN-m\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                Calculate_Program();
                Write_User_Input();
                Write_Drawing_File();
                if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iapp.View_Result(rep_file_name); }
                is_process = true;
                FilePath = user_path;
            }
        }
        void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                //sw.WriteLine("w1 = {0:f3}", w1);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_File()
        {
            string f_name = Path.Combine(system_path, "PIER_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(f_name, FileMode.Create));
            try
            {
                //sw.WriteLine("_G={0}", _G);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            iapp.View_Result(rep_file_name);
            //frmViewResult f_v_r = new frmViewResult(rep_file_name);
            //f_v_r.ShowDialog();
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                    Load_Analysis_Data(fbd.SelectedPath);
                }
            }
        }

        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Bridge Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Abutment & Pier");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of RCC Pier");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }

        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF RCC PierS : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "BRIDGE_PIERS");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_Pier.TXT");
                user_input_file = Path.Combine(system_path, "RCC_PIERS.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_From_File();
                }


            }
        }

        public void Read_From_File()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        //case "w1":
                        //    w1 = mList.GetDouble(1);
                        //    txt_w1.Text = w1.ToString();
                        //    break;

                        
                    }
                    #endregion
                    #region USER INPUT DATA

                    //sw.WriteLine(" = {0:f3} ", w1);
                    //sw.WriteLine(" = {0:f3} ", w2);
                    //sw.WriteLine(" = {0:f3} ", e);
                    //sw.WriteLine(" = {0:f3} ", b1);
                    //sw.WriteLine(" = {0:f3} ", b2);
                    //sw.WriteLine(" = {0:f3} ", l);
                    //sw.WriteLine(" = {0:f3} ", h);
                    //sw.WriteLine(" = {0:f3} ", );
                    //sw.WriteLine(" = {0:f3} ", w3);
                    //sw.WriteLine(" = {0:f3} ", gamma_c);
                    //sw.WriteLine(" = {0:f3} ", f1);
                    //sw.WriteLine(" = {0:f3} ", f2);
                    //sw.WriteLine(" = {0:f3} ", A);
                    //sw.WriteLine(" = {0:f3} ", F);
                    //sw.WriteLine(" = {0:f3} ", V);

                    #endregion
                }


            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        private void Load_Analysis_Data(string working_path)
        {
            //string f_path = Environment.GetEnvironmentVariable("PIER");
            string f_path = Path.Combine(working_path, "Forces.fil");
            if (File.Exists(f_path))
            {
                List<string> list_arr = new List<string>(File.ReadAllLines(f_path));
                MyList mlist = null;

                for (int i = 0; i < list_arr.Count; i++)
                {
                    mlist = new MyList(list_arr[i].ToUpper(), '=');
                    switch (mlist.StringList[0])
                    {
                        case "W1":
                            {
                                txt_W1_supp_reac.Text = mlist.StringList[1];
                                txt_W1_supp_reac.ForeColor = Color.Red;
                            }
                            break;
                        case "MX1":
                            txt_Mx1.Text = mlist.StringList[1];
                            txt_Mx1.ForeColor = Color.Red;
                            break;
                        case "MZ1":
                            txt_Mz1.Text = mlist.StringList[1];
                            txt_Mz1.ForeColor = Color.Red;
                            break;
                    }
                }

            }
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iapp.RunViewer(user_path, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRccPier_Load(object sender, EventArgs e)
        {
            //string f_path = Environment.GetEnvironmentVariable("PIER");
            //if (File.Exist(f_path ))
            //{
            //    List<string> list_arr = new List<string>(File.ReadAllLines(f_path));
            //    MyList mlist = null;

            //    for (int i = 0; i < list_arr.Count; i++)
            //    {
            //        mlist = new MyList(list_arr[i].ToUpper(), '=');
            //        switch (mlist.StringList[0])
            //        {
            //            case "W1":
            //                {
            //                    txt_W1_supp_reac.Text = mlist.StringList[1];
            //                    txt_W1_supp_reac.ForeColor = Color.Red;
            //                }
            //                break;
            //            case "MX1":
            //                txt_Mx1.Text = mlist.StringList[1];
            //                txt_Mx1.ForeColor = Color.Red;
            //                break;
            //            case "MZ1":
            //                txt_Mz1.Text = mlist.StringList[1];
            //                txt_Mz1.ForeColor = Color.Red;
            //                break;
            //        }
            //    }

            //}
        }
    }
}
