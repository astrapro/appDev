using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
//using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.PSC_I_Girder;


using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.LS_Progress;


using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;


//using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace LimitStateMethod.DeckSlab
{
    public partial class UC_Deckslab_BS : UserControl
    {
        public IApplication iApp;
        List<bool> flags { get; set; }


        const string Title = "Deckslab Analysis [BS 5400]";
            //string user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            //if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);

            //string INPUT_FILE = Path.Combine(user_path, "INPUT_DATA.TXT");


        public UC_Deckslab_BS(IApplication app)
        {
            InitializeComponent();
            iApp = app;

            user_path = Path.Combine(iApp.LastDesignWorkingFolder, "DESIGN OF DECKSLAB [BS 5400]");
        }
        public UC_Deckslab_BS()
        {
            InitializeComponent();

            OnCreateData += UC_Deckslab_BS_OnCreateData;
            OnButtonClick += UC_Deckslab_BS_OnCreateData;

            flags = new List<bool>();
            Set_Flag(0);
            
        }
        void Set_Flag(int flag_no)
        {
            if(flags.Count == 0)
            {
                flags.Add(false);
                flags.Add(false);
                flags.Add(false);
                flags.Add(false);

                flags.Add(false);
                flags.Add(false);

                flags.Add(false);
                flags.Add(false);
                flags.Add(false);
                flags.Add(false);
            }
            if (flag_no > 0)
                flags[flag_no - 1] = true;

            for(int i = flag_no; i < flags.Count; i++)
            {
                flags[i] = false;
            }
        }

        bool Show_Flag(int flag_no)
        {

            string kStr = "";
            int count = 0;
            for (int i = 0; i < flag_no-1; i++)
            {
                if (!flags[i])
                {
                    count++;
                    kStr += "Process " + (i + 1) + ", ";
                }
            }

            if (kStr != "")
            {
                kStr = kStr.Substring(0, kStr.Length - 2);
                if (flag_no == 2)
                {
                    MessageBox.Show("This Process 2 [Design] can not be done as the Process 1 [Analysis] is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (flag_no == 11)
                {
                    if(count == 1)
                        MessageBox.Show("The Process [" + kStr + "] is not done/updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("The Processes [" + kStr + "] are not done/updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show("This Process " + flag_no + " [Design] can not be done as the Process " + (flag_no - 1) + " [Design] is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (count == 1)
                        MessageBox.Show("This Process " + flag_no + " [Design] can not be done as the Process " + (flag_no - 1) + " [Design] is not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("This Process " + flag_no + " [Design] can not be done as the Processes [" + kStr + "] are not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }
            return true;
        }

        void UC_Deckslab_BS_OnCreateData(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        // The delegate procedure we are assigning to our object
        public delegate void ButtonClick(object sender,
                                             EventArgs e);

        public event ButtonClick OnButtonClick;

        public delegate void CreateData(object sender,
                                         EventArgs e);

        public event CreateData OnCreateData;



        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string YD
        {
            get
            {
                return txt_sp_YD.Text;
            }
            set
            {
                txt_sp_YD.Text = value;

            }
        }


        public double  d_YD
        {
            get
            {
                return MyList.StringToDouble(txt_sp_YD.Text, 0.0);
            }
            set
            {
                txt_sp_YD.Text = value.ToString();

            }
        }


        public string ZD
        {
            get
            {
                return txt_sp_ZD.Text;
            }
            set
            {
                txt_sp_ZD.Text = value;

            }
        }



        #endregion Properties



        #region Properties

        public double b { get { return MyList.StringToDouble(txt_ds_b.Text, 0.0); } set { txt_ds_b.Text = value.ToString(); } }
        public double c_span { get { return MyList.StringToDouble(txt_ds_cs.Text, 0.0); } set { txt_ds_cs.Text = value.ToString(); } }
        public double cl { get { return MyList.StringToDouble(txt_ds_cl.Text, 0.0); } set { txt_ds_cl.Text = value.ToString(); } }
        public double cr { get { return MyList.StringToDouble(txt_ds_cr.Text, 0.0); } set { txt_ds_cr.Text = value.ToString(); } }
        public int c_sides { get { return ((cl == 0.0 || cr == 0.0) ? 1 : 2); } }
        public double girder_no { get { return MyList.StringToDouble(txt_ds_girder_no.Text, 0.0); } set { txt_ds_girder_no.Text = value.ToString(); } }



        public double width_girderweb { get { return MyList.StringToDouble(txt_ds_width_girderweb.Text, 0.0); } set { txt_ds_width_girderweb.Text = value.ToString(); } }
        public double width_girderflange { get { return MyList.StringToDouble(txt_ds_width_girderflange.Text, 0.0); } set { txt_ds_width_girderflange.Text = value.ToString(); } }

        public double h { get { return MyList.StringToDouble(txt_ds_h.Text, 0.0); } set { txt_ds_h.Text = value.ToString(); } }
        public double d { get { return MyList.StringToDouble(txt_ds_d.Text, 0.0); } set { txt_ds_d.Text = value.ToString(); } }
        public double d1 { get { return MyList.StringToDouble(txt_ds_d1.Text, 0.0); } set { txt_ds_d1.Text = value.ToString(); } }
        public double d_total { get { return MyList.StringToDouble(txt_ds_d_total.Text, 0.0); } set { txt_ds_d_total.Text = value.ToString(); } }
        public double thickness_surfacing { get { return MyList.StringToDouble(txt_ds_thickness_surfacing.Text, 0.0); } set { txt_ds_thickness_surfacing.Text = value.ToString(); } }
        public double cover { get { return MyList.StringToDouble(txt_ds_cover.Text, 0.0); } set { txt_ds_cover.Text = value.ToString(); } }
        public double bar_dia { get { return MyList.StringToDouble(txt_ds_bar_dia.Text, 0.0); } set { txt_ds_bar_dia.Text = value.ToString(); } }
        public double bar_spacing { get { return MyList.StringToDouble(txt_ds_bar_spacing.Text, 0.0); } set { txt_ds_bar_spacing.Text = value.ToString(); } }
        public double bar_no { get { return MyList.StringToDouble(txt_ds_bar_no.Text, 0.0); } set { txt_ds_bar_no.Text = value.ToString(); } }
        public double unitwt_concrete { get { return MyList.StringToDouble(txt_ds_unitwt_concrete.Text, 0.0); } set { txt_ds_unitwt_concrete.Text = value.ToString(); } }
        public double unitwt_surfacing { get { return MyList.StringToDouble(txt_ds_unitwt_surfacing.Text, 0.0); } set { txt_ds_unitwt_surfacing.Text = value.ToString(); } }
        public double Fck { get { return MyList.StringToDouble(txt_ds_Fck.Text, 0.0); } set { txt_ds_Fck.Text = value.ToString(); } }
        public double Fy { get { return MyList.StringToDouble(txt_ds_Fy.Text, 0.0); } set { txt_ds_Fy.Text = value.ToString(); } }
        public double wheel_a1 { get { return MyList.StringToDouble(txt_ds_wheel_a1.Text, 0.0); } set { txt_ds_wheel_a1.Text = value.ToString(); } }
        public double wheel_a2 { get { return MyList.StringToDouble(txt_ds_wheel_a2.Text, 0.0); } set { txt_ds_wheel_a2.Text = value.ToString(); } }
        public double wload_unit { get { return MyList.StringToDouble(txt_ds_wload_unit.Text, 0.0); } set { txt_ds_wload_unit.Text = value.ToString(); } }
        public double axle_unit { get { return MyList.StringToDouble(txt_ds_axle_unit.Text, 0.0); } set { txt_ds_axle_unit.Text = value.ToString(); } }
        public double load_wheel { get { return MyList.StringToDouble(txt_ds_load_wheel.Text, 0.0); } set { txt_ds_load_wheel.Text = value.ToString(); } }



        public double gf1_sls_swt { get { return MyList.StringToDouble(txt_ds_gf1_sls_swt.Text, 0.0); } set { txt_ds_gf1_sls_swt.Text = value.ToString(); } }
        public double gf3_sls_swt { get { return MyList.StringToDouble(txt_ds_gf3_sls_swt.Text, 0.0); } set { txt_ds_gf3_sls_swt.Text = value.ToString(); } }


        public double gf1_uls_swt { get { return MyList.StringToDouble(txt_ds_gf1_uls_swt.Text, 0.0); } set { txt_ds_gf1_uls_swt.Text = value.ToString(); } }
        public double gf3_uls_swt { get { return MyList.StringToDouble(txt_ds_gf3_uls_swt.Text, 0.0); } set { txt_ds_gf3_uls_swt.Text = value.ToString(); } }



        public double gf1_sls_sur { get { return MyList.StringToDouble(txt_ds_gf1_sls_sur.Text, 0.0); } set { txt_ds_gf1_sls_sur.Text = value.ToString(); } }
        public double gf3_sls_sur { get { return MyList.StringToDouble(txt_ds_gf3_sls_sur.Text, 0.0); } set { txt_ds_gf3_sls_sur.Text = value.ToString(); } }


        public double gf1_uls_sur { get { return MyList.StringToDouble(txt_ds_gf1_uls_sur.Text, 0.0); } set { txt_ds_gf1_uls_sur.Text = value.ToString(); } }
        public double gf3_uls_sur { get { return MyList.StringToDouble(txt_ds_gf3_uls_sur.Text, 0.0); } set { txt_ds_gf3_uls_sur.Text = value.ToString(); } }


        public double gf1_sls_Mhog { get { return MyList.StringToDouble(txt_ds_gf1_sls_Mhog.Text, 0.0); } set { txt_ds_gf1_sls_Mhog.Text = value.ToString(); } }
        public double gf3_sls_Mhog { get { return MyList.StringToDouble(txt_ds_gf3_sls_Mhog.Text, 0.0); } set { txt_ds_gf3_sls_Mhog.Text = value.ToString(); } }


        public double gf1_uls_Mhog { get { return MyList.StringToDouble(txt_ds_gf1_uls_Mhog.Text, 0.0); } set { txt_ds_gf1_uls_Mhog.Text = value.ToString(); } }
        public double gf3_uls_Mhog { get { return MyList.StringToDouble(txt_ds_gf3_uls_Mhog.Text, 0.0); } set { txt_ds_gf3_uls_Mhog.Text = value.ToString(); } }


        public double b1 { get { return MyList.StringToDouble(txt_ds_b1.Text, 0.0); } set { txt_ds_b1.Text = value.ToString(); } }
        public double b2 { get { return MyList.StringToDouble(txt_ds_b2.Text, 0.0); } set { txt_ds_b2.Text = value.ToString(); } }
        public double span_deckslab { get { return MyList.StringToDouble(txt_ds_span_deckslab.Text, 0.0); } set { txt_ds_span_deckslab.Text = value.ToString(); } }


        public double Mhog { get { return MyList.StringToDouble(txt_Mhog.Text, 0.0); } set { txt_Mhog.Text = value.ToString(); } }
        public double Msag { get { return MyList.StringToDouble(txt_Msag.Text, 0.0); } set { txt_Msag.Text = value.ToString(); } }
        public double V { get { return MyList.StringToDouble(txt_V.Text, 0.0); } set { txt_V.Text = value.ToString(); } }




        #endregion Properties

        public string INPUT_FILE
        {
            get
            {
                if (user_path == "") return "";
                if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);

                return Path.Combine(user_path, "INPUT_DATA.TXT");
            }
        }



        #region Deckslab Design

        public string Deckslab_Report_File
        {
            get
            {
                if (user_path == "") return "";
                if (!Directory.Exists(user_path)) Directory.CreateDirectory(user_path);
                return Path.Combine(user_path, "DESIGN_OF_DECKSLAB.TXT");
            }
        }

        string working_folder = "";
        public string user_path
        {
            get
            {
                return working_folder;
            }
            set
            {
                if(value != null)
                if (Path.GetFileName(value) != Title)
                {
                    working_folder = Path.Combine(value, Title);
                    if (!Directory.Exists(working_folder))
                        Directory.CreateDirectory(working_folder);
                }
                else
                    working_folder = value;
                Button_Enabled_Disabled();
            }
        }

        //{
        //    get
        //    {
        //        return Path.Combine(iApp.LastDesignWorkingFolder, "Deckslab Analysis [as per BS5400]");
        //    }
        //}

        public void Calculate_Program()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("----------------------------"));
            list.Add(string.Format("User's General Design Data"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall width of Deck Slab = b = {0:f3} m.", b));
            list.Add(string.Format("Cantilever Span = cs = {0:f3} m", c_span));
            list.Add(string.Format("Left Cantilever width of Deck Slab = cl = {0:f3} m", cl));
            list.Add(string.Format("Right Cantilever width of Deck Slab = cr = {0:f3} m", cr));


            list.Add(string.Format("Total Number of Main Long Girders = girder_no = {0} Nos.", girder_no));


            list.Add(string.Format("Width of Web of Long Main Girders = width_girderweb = {0:f3} m.", width_girderweb));
            list.Add(string.Format("Width of Flange of Long Main Girders = width_girderflange = {0:f3} m.", width_girderflange));
            list.Add(string.Format("Overall Thickness of Deck Slab = h = {0} mm.", h));
            list.Add(string.Format("Effective Depth of Deck Slab = d = {0} - {1} - {2}/2 = {3} mm.", h, cover, bar_dia, d));
            list.Add(string.Format("Thickness of Permanent Formwork = d1 = {0} mm.", d1));
            list.Add(string.Format("Total Thickness of Deck Slab = d_total = h + d1 = {0} + {1} = {2} mm.", h, d1, d_total));
            list.Add(string.Format("Thickness of surfacing by wearing course =  thickness_surfacing = {0} mm.", thickness_surfacing));
            list.Add(string.Format("Cover to Reinforcements = cover = {0} mm.", cover));
            list.Add(string.Format("Diameter of Reinforcement Steel Bars = bar_dia = {0} mm.", bar_dia));
            list.Add(string.Format("Spacing of Reinforcement Steel Bars = bar_spacing = {0} mm.", bar_spacing));
            list.Add(string.Format("Total Number of Reinforcement Steel Bars per metre = bar_no = 1000/{0} = {1} Nos.", bar_spacing, bar_no));
            list.Add(string.Format(""));
            list.Add(string.Format("Unit Weight of Concrete  = unitwt_concrete = {0} KN/Cu.M.", unitwt_concrete));
            list.Add(string.Format("Unit Weight of surfacing  = unitwt_surfacing = {0} KN/Cu.M", unitwt_surfacing));
            list.Add(string.Format(""));
            list.Add(string.Format("Concrete = Fck = {0} N/Sq.mm.", Fck));
            list.Add(string.Format("Steel  = Fy = {0} N/Sq.mm.", Fy));
            list.Add(string.Format(""));
            list.Add(string.Format("Wheel size = wheel_a1 x wheel_a2 = {0} x {1}  Units wheel Load = wload_units = {2}  ", wheel_a1, wheel_a2, wload_unit));
            list.Add(string.Format(""));
            list.Add(string.Format("One unit of axle = axle_unit =  {0} kN", axle_unit));
            list.Add(string.Format(""));
            list.Add(string.Format("Load per wheel (For single unit)= load_wheel = axle_unit/4= {0} kN", load_wheel));
            list.Add(string.Format(""));
            if (girder_no > 1)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------"));
                list.Add(string.Format("STEP 1.0 : Design of Deck Slab"));
                list.Add(string.Format("-------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("Width of Deck Slab between Centre to Centre of two Girders = b1 = ({0:f3} - {1:f3} - {2:f3}) / ({3}+1) = {4:f3} m.", b, cl, cr, girder_no, b1));
                list.Add(string.Format("Clear Width of Deck Slab between two Girders = b2 = {0:f3} - 2 x {1:f3} / 2 = {2:f3} m.", b1, width_girderflange, b2));
                list.Add(string.Format("Effective Span of Deck Slab = span_deckslab = {0:f3} + {1:f3} = {2:f3} m.", b2, (d / 1000), span_deckslab));
                list.Add(string.Format(""));

                list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("                                          SLS                                        ULS        "));
                list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("Load  calculation:              γf1                   γf3                  γf1                 γf3       "));
                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("Self weight               γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_swt, gf3_sls_swt, gf1_uls_swt, gf3_uls_swt));
                list.Add(string.Format("Surfacing                 γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_sur, gf3_sls_sur, gf1_uls_sur, gf3_uls_sur));
                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("Live load"));
                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format(" Mhog                     γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
                list.Add(string.Format(" Msag                     γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
                list.Add(string.Format("  V                       γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                //d_total /= 1000;
                //thickness_surfacing /= 1000;

                list.Add(string.Format("STEP 1.1 : Load Calculation"));
                list.Add(string.Format("----------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("STEP 1.1.1 : Dead Load"));
                list.Add(string.Format("-----------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("Deck self weight:"));
                list.Add(string.Format("------------------"));
                list.Add(string.Format(""));
                double Nom_dsw = (d_total / 1000) * unitwt_concrete;
                list.Add(string.Format("Nominal Deck self weight = Nom_dsw = d_total   x   unitwt_concrete  "));
                list.Add(string.Format("                                   = {0:f3}  x  {1}  =  {2:f3}  kN/Sq.m", d_total, unitwt_concrete, Nom_dsw));
                list.Add(string.Format(""));

                double Nom_dsw_sls = Nom_dsw * gf1_sls_swt * gf3_sls_swt;
                list.Add(string.Format("SLS  =  Nom_dsw_sls  = Nom_dsw  x γf1_sls_swt  x  γf3_sls_swt"));
                list.Add(string.Format("                     = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_sls_swt, gf3_sls_swt, Nom_dsw_sls));
                list.Add(string.Format(""));


                double Nom_dsw_uls = Nom_dsw * gf1_uls_swt * gf3_uls_swt;

                list.Add(string.Format("ULS =  Nom_dsw_uls  =  Nom_dsw  x γf1_uls_swt  x  γf3_uls_swt"));
                list.Add(string.Format("                     = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_uls_swt, gf3_uls_swt, Nom_dsw_uls));
                list.Add(string.Format(""));
                list.Add(string.Format("----------"));
                list.Add(string.Format("Surfacing:"));
                list.Add(string.Format("----------"));
                list.Add(string.Format(""));

                double Nom_surfacing = thickness_surfacing / 1000 * unitwt_surfacing;

                list.Add(string.Format("Nominal Surfacing = Nom_surfacing = thickness_surfacing  x   unitwt_surfacing"));
                list.Add(string.Format("                                  =  {0:f3}  x  {1}  =  {2:f3}  kN/Sq.m", thickness_surfacing, unitwt_surfacing, Nom_surfacing));

                list.Add(string.Format(""));



                double Nom_surfacing_sls = Nom_surfacing * gf1_sls_sur * gf3_sls_sur;
                list.Add(string.Format("SLS  =  Nom_surfacing_sls   =  Nom_surfacing  x  γf1_sls_sur  x  γf3_sls_sur"));
                list.Add(string.Format("                            = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_sls_sur, gf3_sls_sur, Nom_surfacing_sls));

                list.Add(string.Format(""));

                double Nom_surfacing_uls = Nom_surfacing * gf1_uls_sur * gf3_uls_sur;

                list.Add(string.Format("ULS =  Nom_surfacing_uls =  Nom_surfacing  x  γf1_uls_sur  x  γf3_uls_sur"));
                list.Add(string.Format("                         = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_uls_sur, gf3_uls_sur, Nom_surfacing_uls));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                list.Add(string.Format("Dead Load Effect for the Unit Weight :"));
                list.Add(string.Format("---------------------------------------"));
                list.Add(string.Format(""));

                double dle_mhog_sls = (Nom_dsw_sls + Nom_surfacing_sls) * span_deckslab * span_deckslab / 12;

                list.Add(string.Format("Hog BM  =  dle_mhog_sls  =  (Nom_dsw_sls  + Nom_surfacing_sls) x span_deckslab x span_deckslab /12  "));
                list.Add(string.Format("                          = ({0:f3} + {1:f3}) x {2:f3} x {2:f3} / 12  =  {3:f3}  kNm", Nom_dsw_sls, Nom_surfacing_sls, span_deckslab, dle_mhog_sls));
                list.Add(string.Format(""));


                double dle_msag_sls = dle_mhog_sls / 2;
                list.Add(string.Format("Sag BM  =  dle_msag_sls = dle_mhog_sls  / 2  = {0:f3} / 2  =  {1:f3}  kNm", dle_mhog_sls, dle_msag_sls));
                list.Add(string.Format(""));

                double dle_V_sls = (Nom_dsw_sls + Nom_surfacing_sls) * span_deckslab / 2;
                list.Add(string.Format("Shear  = dle_V_sls  =  (Nom_dsw_sls  + Nom_surfacing_sls) x span_deckslab /2  "));
                list.Add(string.Format("                    = ({0:f3} + {1:f3}) x {2:f3}/2  =  {3:f3}  kN", Nom_dsw_sls, Nom_surfacing_sls, span_deckslab, dle_V_sls));
                list.Add(string.Format(""));
                double dle_mhog_uls = (Nom_dsw_uls + Nom_surfacing_uls) * span_deckslab * span_deckslab / 12;

                list.Add(string.Format("Hog BM  = dle_mhog_uls = (Nom_dsw_uls + Nom_surfacing_uls) x span_deckslab x span_deckslab /12  "));
                list.Add(string.Format("                       = ({0:f3}  + {1:f3}) x {2:f3} x {2:f3}/12  =  {3:f3} kNm", Nom_dsw_uls, Nom_surfacing_uls, span_deckslab, dle_mhog_uls));
                list.Add(string.Format(""));

                double dle_msag_uls = dle_mhog_uls / 2;
                list.Add(string.Format("Sag BM  =  dle_msag_uls = dle_mhog_uls  / 2  = {0:f3}/2  =  {1:f3}  kNm", dle_mhog_uls, dle_msag_uls));
                list.Add(string.Format(""));

                double dle_V_uls = (Nom_dsw_uls + Nom_surfacing_uls) * span_deckslab / 2;

                list.Add(string.Format("Shear  = dle_V_uls = (Nom_dsw_uls  + Nom_surfacing_uls) x span_deckslab /2  "));
                list.Add(string.Format("                   = ({0:f3} + {1:f3}) x {2:f3}/2  =  {3:f3}  kN", Nom_dsw_uls, Nom_surfacing_uls, span_deckslab, dle_V_uls));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format("STEP 1.1.2 : Live Load"));
                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("HA & HB Loading"));
                list.Add(string.Format(""));
                list.Add(string.Format("{0} units of HB Loading is to be used for Bending Effect", wload_unit));
                list.Add(string.Format(""));
                list.Add(string.Format(" "));
                //list.Add(string.Format("Figure 2"));
                list.Add(string.Format(""));
                list.Add(string.Format("One unit of axle = axle_unit = {0} kN", axle_unit));
                list.Add(string.Format("Load per wheel =  load_wheel = {0} kN (For single unit)", load_wheel));
                double wheel_load = wload_unit * load_wheel;
                list.Add(string.Format(""));
                list.Add(string.Format("For {0} unit wheel Load =  wheel_load  =  wload_units x load_wheel =  {0} x {1}  = {2}  kN", wload_unit, load_wheel, wheel_load));
                list.Add(string.Format(""));
                double wheel_area = wheel_a1 * wheel_a2;
                list.Add(string.Format("Wheel Load Contact Area  =  wheel_a1 x wheel_a2   =  {0}  x  {1} = {2:f3} Sq.mm", wheel_a1, wheel_a2, wheel_area));
                list.Add(string.Format(""));
                list.Add(string.Format("Dispersal through surfacing  =  1H : 1V and considering the load acting on the top of slab"));
                list.Add(string.Format(""));
                double width_dispersion = span_deckslab - wheel_a1 / 1000.0 - 2 * d / 1000.0;
                //list.Add(string.Format("Dispersion width per wheel = width_dispersion  =  0.5911 m."));


                list.Add(string.Format("Dispersion width per wheel = width_dispersion  =  {0:f4} m.", width_dispersion));
                list.Add(string.Format(""));

                double udl_on_slab = wheel_load / width_dispersion;
                list.Add(string.Format("Therefore, UDL on Slab  =  wheel_load  / width_dispersion  =  {0:f3}  / {1:f3}  =  {2:f3} kN/m", wheel_load, width_dispersion, udl_on_slab));
                list.Add(string.Format(""));
                list.Add(string.Format("As the intensity of HB UDL on slab is same as HA Loading so HB Loading will be considered for Live Load."));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("-----------------------------------------------------------------"));
                list.Add(string.Format("STEP 1.2 : Calculation for Bending Moments and Shear Forces"));
                list.Add(string.Format("-----------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("Mhog  =  MAB  =  {0:f3} kNm", Mhog));
                list.Add(string.Format("Msag  =  {0:f3} kNm", Msag));
                list.Add(string.Format("VAB  =  RA  =  {0:f3} kN", V));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double LL_mhog_sls = Mhog * gf1_sls_Mhog * gf3_sls_Mhog * 25 / wload_unit;
                list.Add(string.Format("Live Load BM (hog) for SLS Mhog  = LL_mhog_sls  =Mhog  x γf1_sls_Mhog  x  γf3_sls_Mhog  x 25/ wload_units "));
                list.Add(string.Format("                                 = {0:f3} x {1:f2} x  {2:f2}  x  25 / {3}          = {4:f3}  kNm", Mhog, gf1_sls_Mhog, gf3_sls_Mhog, wload_unit, LL_mhog_sls));
                list.Add(string.Format(""));

                double gf1_sls_Msag = gf1_sls_Mhog;
                double gf3_sls_Msag = gf3_sls_Mhog;

                double LL_msag_sls = Msag * gf1_sls_Msag * gf3_sls_Msag * 25 / wload_unit;
                list.Add(string.Format("Live Load BM (sag) for SLS Msag = LL_msag_sls = Msag  x γf1_sls_Msag  x  γf3_sls_Msag x 25 / wload_units "));
                list.Add(string.Format("                                =  {0:f3}  x {1:f2}  x  {2:f2}  x  25/{3} = {4:f3}  kNm", Msag, gf1_sls_Msag, gf3_sls_Msag, wload_unit, LL_msag_sls));
                list.Add(string.Format(""));



                double gf1_sls_V = gf1_sls_Mhog;
                double gf3_sls_V = gf3_sls_Mhog;

                double LL_VAB_sls = V * gf1_sls_V * gf3_sls_V * 25 / wload_unit;
                list.Add(string.Format("Live Load Shear for SLS = LL_VAB_sls =  VAB  x γf1_sls_V  x  γf3_sls_V  x 25/ wload_units "));
                list.Add(string.Format("                                     =  {0:f3} x {1:f2}  x  {2:f2}  x  25/{3} =  {4:f2} kN", V, gf1_sls_V, gf3_sls_V, wload_unit, LL_VAB_sls));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double LL_mhog_uls = Mhog * gf1_uls_Mhog * gf3_uls_Mhog * 30 / wload_unit;
                list.Add(string.Format("Live Load BM (hog) for ULS Mhog  = LL_mhog_uls = Mhog  x γf1_uls_Mhog  x  γf3_uls_Mhog  x 30 / wload_units "));
                list.Add(string.Format("                                                = {0:f3} x {1:f2}  x  {2:f2}  x  25 / {3}  = {4:f3}  kNm", Mhog, gf1_uls_Mhog, gf3_uls_Mhog, wload_unit, LL_mhog_uls));
                list.Add(string.Format(""));



                double gf1_uls_Msag = gf1_uls_Mhog;
                double gf3_uls_Msag = gf3_uls_Mhog;


                double LL_msag_uls = Msag * gf1_uls_Msag * gf3_uls_Msag * 25 / wload_unit;
                list.Add(string.Format("Live Load BM (sag) for ULS Msag = LL_msag_uls = Msag  x γf1_uls_Msag  x  γf3_uls_Msag    x 25/ wload_units"));
                list.Add(string.Format("                                               = {0:f3}  x {1:f2}  x  {2:f2}  x  25/{3} =  {4:f3}  kNm", Msag, gf1_uls_Msag, gf3_uls_Msag, wload_unit, LL_msag_uls));
                list.Add(string.Format(""));

                double gf1_uls_V = gf1_uls_Mhog;
                double gf3_uls_V = gf3_uls_Mhog;

                double LL_VAB_uls = V * gf1_uls_V * gf3_uls_V * 25 / wload_unit;

                list.Add(string.Format("Live Load Shear for ULS = LL_VAB_uls =  VAB  x γf1_uls_V  x  γf3_uls_V  x 25 / wload_units "));
                list.Add(string.Format("                                     =  {0:f3} x {1:f2} x {1:f2} x  25/{3} =  {4:f3} kN", V, gf1_uls_V, gf3_uls_V, wload_unit, LL_VAB_uls));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("------------------------"));
                list.Add(string.Format("Maximum Sagging Moment"));
                list.Add(string.Format("------------------------"));
                list.Add(string.Format(""));



                double MDL_LL_uls_Sag = dle_msag_uls + LL_msag_uls;
                double MDL_sls_Sag = dle_msag_sls;
                double MLL_sls_Sag = LL_msag_sls;

                list.Add(string.Format("MDL + LL [ULS] =  {0:f3} + {1:f3} = {2:f3}  kNm.", dle_msag_uls, LL_msag_uls, MDL_LL_uls_Sag));
                list.Add(string.Format("MDL [SLS]  =  {0:f3} kNm.", MDL_sls_Sag));
                list.Add(string.Format("MLL [SLS]  =  {0:f3} kNm.", MLL_sls_Sag));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Maximum Hogging Moment"));
                list.Add(string.Format("-----------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                double MDL_LL_uls_Hog = dle_mhog_uls + LL_mhog_uls;
                double MDL_sls_Hog = dle_mhog_sls;
                double MLL_sls_Hog = LL_mhog_sls;


                list.Add(string.Format("MDL+LL  [ULS]  =  {0:f3} + {1:f3} = {2:f3}  kNm.", dle_mhog_uls, LL_mhog_uls, MDL_LL_uls_Hog));
                list.Add(string.Format("MDL [SLS]  =  {0:f3} kNm.", MDL_sls_Hog));
                list.Add(string.Format("MLL [SLS]  =  {0:f3}  kNm.", MLL_sls_Hog));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Maximum Shear Force"));
                list.Add(string.Format("----------------------"));
                list.Add(string.Format(""));

                double VDL_LL_uls = dle_V_uls + LL_VAB_uls;
                double VDL_sls = dle_V_sls;
                double VLL_sls = LL_VAB_sls;

                list.Add(string.Format("VDL+LL  [ULS] = {0:f3} + {1:f3} = {2:f3} kN.", dle_V_uls, LL_VAB_uls, VDL_LL_uls));
                list.Add(string.Format("VDL [SLS]  =  {0:f3} kN.", VDL_sls));
                list.Add(string.Format("VLL [SLS]  =  {0:f3} kN.", VLL_sls));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------------------------------------------"));
                list.Add(string.Format("STEP 1.3 : Check for Bending Resistance in Transverse Direction"));
                list.Add(string.Format("-------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("   (i) Check for Moment of Resistance in Transverse Direction"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = 1000 mm "));
                list.Add(string.Format("Cover = {0} mm", cover));
                list.Add(string.Format("h = {0} mm ", h));
                list.Add(string.Format("Reinforcement Bar Diameters = bar_dia = {0} mm.", bar_dia));
                list.Add(string.Format("Spacing = bar_spacing = {0} mm", bar_spacing));
                list.Add(string.Format("Bar Nos. = bar_nos = 1000/ bar_spacing = 1000/{0}  = {1} ", bar_spacing, bar_no));
                list.Add(string.Format("d = {0} mm", d));
                list.Add(string.Format("fcu = {0}  N/Sq.mm", Fck));
                list.Add(string.Format("fy = {0}  N/Sq.mm", Fy));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                //double MDL_LL_uls_Hog = dle_mhog_uls + LL_mhog_uls;
                //double MDL_sls_Hog = dle_mhog_sls;
                //double MLL_sls_Hog = LL_mhog_sls;


                double des_BM = Math.Max(Math.Max(MDL_LL_uls_Hog, MDL_sls_Hog), MLL_sls_Hog);
                double des_V = Math.Max(Math.Max(VDL_LL_uls, VDL_sls), VLL_sls);

                list.Add(string.Format("Max.Design Bending Moment = {0:f3}  kNm (Maximum Hogging Moment)", des_BM));
                list.Add(string.Format(""));
                list.Add(string.Format("Provide Reinforcement = {0} Nos.  {1} mm dia. bars", bar_no, bar_dia));
                list.Add(string.Format(""));

                double req_Ast = des_V * 1000 / (2.0 * 0.87 * Fy); // ?????


                list.Add(string.Format("Reqd. Area of steel for shear  = {0:f3} Sq.mm ", req_Ast));
                list.Add(string.Format(""));

                double Ast = bar_no * (Math.PI * bar_dia * bar_dia) / 4; // ?????
                list.Add(string.Format("Area of steel provided,     As   = bar_no x (π x bar_dia x bar_dia) / 4"));
                list.Add(string.Format("                                 = {0} x ({1:f3} x {2} x {2}) / 4", bar_no, Math.PI, bar_dia));
                list.Add(string.Format("                                 = {0:f3} Sq.mm", Ast));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double z = (1 - (1.1 * Fy * Ast) / (Fck * 1000 * d)) * d;
                list.Add(string.Format("z =   [1 -   (1.1 x fy x As)  /  (fcu x b x d) ] x d"));
                list.Add(string.Format("  =   [1 -   (1.1 x {0} x {1:f3})  /  ({2} x 1000 x {3}) ] x {3}", Fy, Ast, Fck, d));
                list.Add(string.Format(""));
                if (z < d * 0.95)
                    list.Add(string.Format("  =   {0:f3}  m. <  0.95 x d = 0.95 x {1} = {2:f3} m. OK", z, d, d * 0.95));
                else
                    list.Add(string.Format("  =   {0:f3}  m. >  0.95 x d = 0.95 x {1} = {2:f3} m. NOT OK", z, d, d * 0.95));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double Mu = 0.87 * Fy * Ast * z / 1000000;

                list.Add(string.Format("Moment of Resistance,  Mu = 0.87 x fy x As x z "));
                list.Add(string.Format("                          = 0.87 x {0} x {1:f3} x {2:f3}/10^6", Fy, Ast, z));
                list.Add(string.Format(""));
                if (Mu > des_BM)
                    list.Add(string.Format("                          = {0:f3} kNm  >  {1:f3}  kNm.  Section OK", Mu, des_BM));
                else
                    list.Add(string.Format("                          = {0:f3} kNm  <=  {1:f3}  kNm.  Section NOT OK", Mu, des_BM));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("------------------------------------"));
                list.Add(string.Format("STEP 1.4 : Check Flexural Cracking"));
                list.Add(string.Format("------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Breadth of section = b = 1000 mm"));
                list.Add(string.Format("Depth of section = h = {0} mm", h));

                double Cnom = cover;
                double Cmin = cover - 10;
                list.Add(string.Format("Reinforcement Cover (Nominal) = Cnom = {0} mm", Cnom));
                list.Add(string.Format("Reinforcement Cover (Minimum) = Cmin = {0} mm", Cmin));
                list.Add(string.Format("Reinforcement bars = bar_dia = {0}  mm", bar_dia));
                list.Add(string.Format("Spacing = spacing = {0} mm", bar_spacing));
                list.Add(string.Format("No of bars = bar_nos = b / spacing = 1000 / {0} = {1} Nos", bar_spacing, bar_no));
                list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4"));
                list.Add(string.Format(""));

                Ast = bar_no * Math.PI * bar_dia * bar_dia / 4;
                list.Add(string.Format("                   = {0} x (3.1416 x {1} x {1})/4 = {2:f3} Sq.mm", bar_no, bar_dia, Ast));

                d = h - Cnom - bar_dia / 2;

                list.Add(string.Format("Effective depth, d = h - Cnom - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", h, Cnom, bar_dia, d));
                list.Add(string.Format(""));

                double a_dash = 180.0;
                double M = 8.0;


                list.Add(string.Format("Distance from compression face to point at which crack is calculated, a', = {0} mm", a_dash));
                list.Add(string.Format("Service Moment = M = {0} kNm", M));

                double Ec = 34;
                list.Add(string.Format("Instantaneous modulus of elasticity = Ec = {0} kN/Sq.mm", Ec));

                double Es = 200;
                list.Add(string.Format("Modulus of Elasticity of Steel = Es = {0} kN/Sq.mm", Es));

                double flx_crk = 0.25;
                list.Add(string.Format("Flexural Crack width aimed for = {0} mm", flx_crk));

                double a_cr = 89.95;
                list.Add(string.Format("Distance to surface of nearest rebar, a_cr = {0} mm", a_cr));

                double ae = Es * 2 / Ec;

                list.Add(string.Format("Modular Ratio, = αe = Es x 2 / Ec = {0} x 2 / {1} = {2:f3} = (Ec long term = Ec /2)", Es, Ec, ae));

                double Mq = MLL_sls_Hog;
                double Mg = MDL_sls_Hog;
                list.Add(string.Format("Bending Moment for Live load = {0:f3} kN-m", MLL_sls_Hog));

                list.Add(string.Format("Bending Moment for Dead Load = {0:f3} kN-m", MDL_sls_Hog));
                list.Add(string.Format(""));


                double _b = 1000;
                double val1 = -((ae * Ast) + Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * d))) / _b;
                double val2 = -((ae * Ast) - Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * d))) / _b;
                //double val1 = - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b



                //double dc  =  - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b;
                double dc = Math.Max(val1, val2);

                list.Add(string.Format("Depth of neutral axis, = dc  = - [(αe x As) +/- √(((αe x As) x (αe x As)) + (z x b x αe x As x d))] / b"));
                list.Add(string.Format("Depth of neutral axis, = dc  = - [({0:f3} x {1:f3}) +/- √(({0:f3} x {1:f3}) x ({0:f3} x {1:f3})) + ({2:f3} x {3:f3} x {0:f3} x {1:f3} x {4:f3}))] / {3:f3}", ae, Ast, z, _b, d));
                list.Add(string.Format("                             = {0:f3} mm.", dc));
                list.Add(string.Format(""));

                double fs = M / (Ast * (d - (dc / 3)) * 1000 * 1000);
                list.Add(string.Format("Reinforcement tensile stress = fs = M / (As x (d - (dc/3)) x 1000 x 1000)"));
                list.Add(string.Format("                                  = {0:f3} / ({1:f3} x ({2} - ({3:f3}/3)) x 1000 x 1000)", M, Ast, d, dc));
                list.Add(string.Format("                                  = {0:f3}  N / Sq. mm", fs));
                list.Add(string.Format(""));

                double e1 = ((a_dash - dc) * fs) / ((d - dc) * Es);
                list.Add(string.Format("Flexural Strain = ((a’ - dc) x fs) / ((d - dc) x Es)"));
                list.Add(string.Format("                = (({0:f3} - {1:f3}) x {2:f3}) / (({3} - {1:f3}) x {4:f3})", a_dash, dc, fs, d, Es));
                list.Add(string.Format("                =  {0:f4}", e1));
                list.Add(string.Format(""));


                list.Add(string.Format("Thus total strain = e1 = {0:f4}", e1));
                list.Add(string.Format(""));
                double xi_s = iApp.Tables.Depth_Factor_BS5400_Table_9(d);
                list.Add(string.Format("ξs = Depth Factor =  {0:f4}    (BS 5400,table 9)", xi_s));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double em = e1 - ((3.8 * _b * h * (a_dash - dc)) / (xi_s * Ast * (h - dc))) * ((1 - (Mq / Mg)) / 1000000000.0);

                list.Add(string.Format("em   = e1 - [3.8 x bt x h x (a’ - dc) ] / [es x As x (h-dc)] x [1 - (Mq / Mg)] / 1,000,000,000 "));
                list.Add(string.Format("                 (but not greater than e1)"));
                list.Add(string.Format(""));
                list.Add(string.Format("     = {0:E4}", em));
                list.Add(string.Format(""));

                double Wmax = (3 * a_cr * em) / (1 + 2 * (a_cr - Cmin) / (h - dc));

                list.Add(string.Format("Crack width = Wmax = (3 x acr x em) / [1 + 2 x (acr - cmin) / (h - dc)]"));
                list.Add(string.Format("                   = (3 x {0:f3} x {1:E3}) / [1 + 2 x ({0:f3} - {2:f3}) / ({3} - {4:f3})]", a_cr, em, Cmin, h, dc));
                if (Wmax < 0.25)
                    list.Add(string.Format("                   = {0:f3}  mm < 0.25 mm.   OK", Wmax));
                else
                    list.Add(string.Format("                   = {0:f3}  mm >= 0.25 mm.   NOT OK", Wmax));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                list.Add(string.Format("-----------------------------------"));
                list.Add(string.Format("STEP 1.5 : Shear Calculation"));
                list.Add(string.Format("-----------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("(ii) Check for Shear Reinforcement"));
                list.Add(string.Format(""));

                double Vmax = VDL_LL_uls;
                list.Add(string.Format("Max. Shear Force, Vmax.= {0:f3} kN", Vmax));
                list.Add(string.Format(""));
                list.Add(string.Format("b = 1000 mm "));
                list.Add(string.Format("Reinforcement Bar Dia  = bar_dia = {0}", bar_dia));
                list.Add(string.Format("Bar Nos = bar_nos = {0}", bar_no));
                list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4 "));
                list.Add(string.Format("                   = {0} x 3.1416 x {1} x {1} / 4 ", bar_no, bar_dia));
                list.Add(string.Format("                   = {0:f3} Sq.mm ", Ast));
                list.Add(string.Format(""));
                list.Add(string.Format("Reinforcement Cover = cover = {0} mm", cover));
                list.Add(string.Format("Overall Slab Thickness = h = {0} mm", h));
                list.Add(string.Format("Effective Depth = d = h - cover - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", h, cover, bar_dia, d));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("fcu = {0} N / Sq.mm", Fck));
                list.Add(string.Format("allowable  fcu 40 = N / Sq.mm (for shear only)"));
                list.Add(string.Format("fyv= {0} N / Sq.mm", Fy));
                list.Add(string.Format(""));


                xi_s = iApp.Tables.Depth_Factor_BS5400_Table_9(d);
                list.Add(string.Format("ξs = Depth Factor =  {0:f4}    (BS 5400,table 9)", xi_s));
                list.Add(string.Format(""));
                list.Add(string.Format("Provide Reinforcement = {0} Nos. {0} ɸ bars", bar_no, bar_dia));
                list.Add(string.Format(""));
                list.Add(string.Format("Area of steel provided, As = {0:f3} Sq.mm", Ast));
                list.Add(string.Format(""));

                double p = 100 * Ast / (_b * d);
                list.Add(string.Format("Percentage = 100 x As / (b x d) = 100 x {0:f3} / (1000 x {1}) = {2:f3} %", Ast, _b, d));
                list.Add(string.Format(""));

                
            CONCRETE_GRADE cgrd = (CONCRETE_GRADE)(int)Fck;

            string ref_str = "";
            double vc = iApp.Tables.Permissible_Shear_Stress(p, cgrd, ref ref_str);


                //double vc = 0.77;
                list.Add(string.Format("vc = Ultimate shear stress in concrete = {0:f3} N / Sq.mm ( BS 5400,table 8)", vc));
                list.Add(string.Format(""));

                double v = V / (_b * d);

                //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
                if (v < (xi_s * vc))
                    list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:f3} N / Sq.mm   <   ξs x vc = {1:f3} N / Sq.mm     Hence, OK.", v, (xi_s * vc)));
                else
                    list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:f3} N / Sq.mm   >   ξs x vc = {1:f3} N / Sq.mm     Hence, NOT OK.", v, (xi_s * vc)));

                //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Longitudinal steel for shear fyv = {0} N/Sq.mm", Fy));
                list.Add(string.Format(""));

                list.Add(string.Format("V = Vmax.= {0:f3} kN", Vmax));

                double Asa = Vmax * 1000 / (2.0 * 0.87 * Fy);

                list.Add(string.Format("Asa  >=  V x 1000 / (2 x 0.87 x fyv) = {0:f3}  Sq.mm", Asa));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            File.WriteAllLines(Deckslab_Report_File, list.ToArray());
            iApp.View_Result(Deckslab_Report_File);
        }

        public void Design_of_Deck_Slab()
        {
            List<string> list = new List<string>();
            if (girder_no > 1)
            {
                Design_of_Deck_Slab(list);
                txt_DS_results.Lines = list.ToArray();
            }
            else
                txt_DS_results.Text = "";

        }

        public void Deckslab_Transverse_Bending_Resistance()
        {
            List<string> list = new List<string>();


            if (girder_no > 1)
            {

                //d_total /= 1000;
                //thickness_surfacing /= 1000;

                //Design_of_Deck_Slab(list, out dle_mhog_sls, out dle_mhog_uls, out LL_mhog_sls, out LL_mhog_uls, out VDL_LL_uls, out VDL_sls, out VLL_sls);


                #region TBR
                double _b = MyList.StringToDouble(txt_TBR_b.Text, 1000);
                double _Cover = MyList.StringToDouble(txt_TBR_cover.Text, 0.0);
                double _h = MyList.StringToDouble(txt_TBR_h.Text, 0.0);
                double _Spacing = MyList.StringToDouble(txt_TBR_spacing.Text, 0.0);
                double _d = MyList.StringToDouble(txt_TBR_d.Text, 0.0);
                double _fcu = MyList.StringToDouble(txt_TBR_fcu.Text, 0.0);
                double _fy = MyList.StringToDouble(txt_TBR_fy.Text, 0.0);
                #endregion TBR

                list.Clear();



                list.Add(string.Format("-------------------------------------------------------------------"));
                list.Add(string.Format("STEP 1.3 : Check for Bending Resistance in Transverse Direction"));
                list.Add(string.Format("-------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("   (i) Check for Moment of Resistance in Transverse Direction"));
                list.Add(string.Format(""));
                list.Add(string.Format("b = {0} mm ", _b));
                list.Add(string.Format("Cover = {0} mm", _Cover));
                list.Add(string.Format("h = {0} mm ", _h));
                list.Add(string.Format("Reinforcement Bar Diameters = bar_dia = {0} mm.", bar_dia));
                list.Add(string.Format("Spacing = bar_spacing = {0} mm", bar_spacing));
                list.Add(string.Format("Bar Nos. = bar_nos = 1000/ Spacing = 1000/{0}  = {1:f3} = {2}", _Spacing, (1000 / _Spacing), bar_no));
                list.Add(string.Format("d = {0} mm", _d));
                list.Add(string.Format("fcu = {0}  N/Sq.mm", _fcu));
                list.Add(string.Format("fy = {0}  N/Sq.mm", _fy));
                list.Add(string.Format(""));
                list.Add(string.Format(""));



                txt_FC_Mg.Text = MDL_sls_Hog.ToString("f3");
                txt_FC_Mq.Text = MLL_sls_Hog.ToString("f3");



                double des_BM = Math.Max(Math.Max(MDL_LL_uls_Hog, MDL_sls_Hog), MLL_sls_Hog);
                double des_V = Math.Max(Math.Max(VDL_LL_uls, VDL_sls), VLL_sls);

                list.Add(string.Format("Max.Design Bending Moment = {0:f3}  kNm (Maximum Hogging Moment)", des_BM));
                list.Add(string.Format(""));
                list.Add(string.Format("Provide Reinforcement = {0} Nos.  {1} mm dia. bars", bar_no, bar_dia));
                list.Add(string.Format(""));

                double req_Ast = des_V * 1000 / (2.0 * 0.87 * _fy); // ?????



                list.Add(string.Format("Reqd. Area of steel for shear  = {0:f3} Sq.mm ", req_Ast));
                list.Add(string.Format(""));

                double Ast = bar_no * (Math.PI * bar_dia * bar_dia) / 4; // ?????
                list.Add(string.Format("Area of steel provided,     As   = bar_no x (π x bar_dia x bar_dia) / 4"));
                list.Add(string.Format("                                 = {0} x ({1:f3} x {2} x {2}) / 4", bar_no, Math.PI, bar_dia));
                list.Add(string.Format("                                 = {0:f3} Sq.mm", Ast));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                z = (1 - (1.1 * _fy * Ast) / (_fcu * _b * _d)) * _d;
                list.Add(string.Format("z =   [1 -   (1.1 x fy x As)  /  (fcu x b x d) ] x d"));
                list.Add(string.Format("  =   [1 -   (1.1 x {0} x {1:f3})  /  ({2} x 1000 x {3}) ] x {3}", _fy, Ast, _fcu, _d));
                list.Add(string.Format(""));

                if (z < _d * 0.95)
                    list.Add(string.Format("  =   {0:f3}  m. <  0.95 x d = 0.95 x {1} = {2:f3} m. OK", z, _d, _d * 0.95));
                else
                    list.Add(string.Format("  =   {0:f3}  m. >  0.95 x d = 0.95 x {1} = {2:f3} m. NOT OK", z, _d, _d * 0.95));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                double Mu = 0.87 * _fy * Ast * z / 1000000;

                list.Add(string.Format("Moment of Resistance,  Mu = 0.87 x fy x As x z "));
                list.Add(string.Format("                          = 0.87 x {0} x {1:f3} x {2:f3}/10^6", _fy, Ast, z));
                list.Add(string.Format(""));
                if (Mu > des_BM)
                    list.Add(string.Format("                          = {0:f3} kNm  >  {1:f3}  kNm.  Section OK", Mu, des_BM));
                else
                    list.Add(string.Format("                          = {0:f3} kNm  <=  {1:f3}  kNm.  Section NOT OK", Mu, des_BM));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                txt_TBR_results.Lines = list.ToArray();
                //MessageBox.Show("Data Processed Successfull.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                txt_TBR_results.Text = "";

        }

        double dle_mhog_sls;
        double dle_mhog_uls;
        double LL_mhog_sls;
        double LL_mhog_uls;
        double VDL_LL_uls;
        double VDL_sls;
        double VLL_sls;


        double MDL_LL_uls_Hog;
        double MDL_sls_Hog;
        double MLL_sls_Hog;
        double Ast;
        double z;


        private void Design_of_Deck_Slab(List<string> list)
        {

            #region Design of Deckslab
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format("User's General Design Data"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall width of Deck Slab = b = {0:f3} m.", b));
            list.Add(string.Format("Cantilever Span = cs = {0:f3} m", c_span));
            list.Add(string.Format("Left Cantilever width of Deck Slab = cl = {0:f3} m", cl));
            list.Add(string.Format("Right Cantilever width of Deck Slab = cr = {0:f3} m", cr));


            list.Add(string.Format("Total Number of Main Long Girders = girder_no = {0} Nos.", girder_no));


            list.Add(string.Format("Width of Web of Long Main Girders = width_girderweb = {0:f3} m.", width_girderweb));
            list.Add(string.Format("Width of Flange of Long Main Girders = width_girderflange = {0:f3} m.", width_girderflange));
            list.Add(string.Format("Overall Thickness of Deck Slab = h = {0} mm.", h));
            list.Add(string.Format("Effective Depth of Deck Slab = d = {0} - {1} - {2}/2 = {3} mm.", h, cover, bar_dia, d));
            list.Add(string.Format("Thickness of Permanent Formwork = d1 = {0} mm.", d1));
            list.Add(string.Format("Total Thickness of Deck Slab = d_total = h + d1 = {0} + {1} = {2} mm.", h, d1, d_total));
            list.Add(string.Format("Thickness of surfacing by wearing course =  thickness_surfacing = {0} mm.", thickness_surfacing));
            list.Add(string.Format("Cover to Reinforcements = cover = {0} mm.", cover));
            list.Add(string.Format("Diameter of Reinforcement Steel Bars = bar_dia = {0} mm.", bar_dia));
            list.Add(string.Format("Spacing of Reinforcement Steel Bars = bar_spacing = {0} mm.", bar_spacing));
            list.Add(string.Format("Total Number of Reinforcement Steel Bars per metre = bar_no = 1000/{0} = {1} Nos.", bar_spacing, bar_no));
            list.Add(string.Format(""));
            list.Add(string.Format("Unit Weight of Concrete  = unitwt_concrete = {0} KN/Cu.M.", unitwt_concrete));
            list.Add(string.Format("Unit Weight of surfacing  = unitwt_surfacing = {0} KN/Cu.M", unitwt_surfacing));
            list.Add(string.Format(""));
            list.Add(string.Format("Concrete = Fck = {0} N/Sq.mm.", Fck));
            list.Add(string.Format("Steel  = Fy = {0} N/Sq.mm.", Fy));
            list.Add(string.Format(""));
            list.Add(string.Format("Wheel size = wheel_a1 x wheel_a2 = {0} x {1}  Units wheel Load = wload_units = {2}  ", wheel_a1, wheel_a2, wload_unit));
            list.Add(string.Format(""));
            list.Add(string.Format("One unit of axle = axle_unit =  {0} kN", axle_unit));
            list.Add(string.Format(""));
            list.Add(string.Format("Load per wheel (For single unit)= load_wheel = axle_unit/4= {0} kN", load_wheel));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format("STEP 1.0 : Design of Deck Slab"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of Deck Slab between Centre to Centre of two Girders = b1 = ({0:f3} - {1:f3} - {2:f3}) / ({3}+1) = {4:f3} m.", b, cl, cr, girder_no, b1));
            list.Add(string.Format("Clear Width of Deck Slab between two Girders = b2 = {0:f3} - 2 x {1:f3} / 2 = {2:f3} m.", b1, width_girderflange, b2));
            list.Add(string.Format("Effective Span of Deck Slab = span_deckslab = {0:f3} + {1:f3} = {2:f3} m.", b2, (d / 1000), span_deckslab));
            list.Add(string.Format(""));

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                          SLS                                        ULS        "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Load  calculation:              γf1                   γf3                  γf1                 γf3       "));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Self weight               γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_swt, gf3_sls_swt, gf1_uls_swt, gf3_uls_swt));
            list.Add(string.Format("Surfacing                 γf1_sls_swt={0:f2}         γf3_sls_swt={1:f2}    γf1_uls_swt={2:f2}    γf3_uls_swt={3:f2}", gf1_sls_sur, gf3_sls_sur, gf1_uls_sur, gf3_uls_sur));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Live load"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(" Mhog                     γf1_sls_Mhog={0:f2}        γf3_sls_Mho={1:f2}    γf1_uls_Mho={2:f2}    γf3_uls_Mho={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
            list.Add(string.Format(" Msag                     γf1_sls_Msag={0:f2}        γf3_sls_Msa={1:f2}    γf1_uls_Msa={2:f2}    γf3_uls_Msa={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
            list.Add(string.Format("  V                       γf1_sls_V   ={0:f2}        γf3_sls_V  ={1:f2}    γf1_uls_V  ={2:f2}    γf3_uls_V  ={3:f2}", gf1_sls_Mhog, gf3_sls_Mhog, gf1_uls_Mhog, gf3_uls_Mhog));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 1.1 : Load Calculation"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1.1.1 : Dead Load"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Deck self weight:"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            double Nom_dsw = (d_total / 1000) * unitwt_concrete;
            list.Add(string.Format("Nominal Deck self weight = Nom_dsw = d_total   x   unitwt_concrete  "));
            list.Add(string.Format("                                   = {0:f3}  x  {1}  =  {2:f3}  kN/Sq.m", d_total / 1000, unitwt_concrete, Nom_dsw));
            list.Add(string.Format(""));

            double Nom_dsw_sls = Nom_dsw * gf1_sls_swt * gf3_sls_swt;
            list.Add(string.Format("SLS  =  Nom_dsw_sls  = Nom_dsw  x γf1_sls_swt  x  γf3_sls_swt"));
            list.Add(string.Format("                     = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_sls_swt, gf3_sls_swt, Nom_dsw_sls));
            list.Add(string.Format(""));


            double Nom_dsw_uls = Nom_dsw * gf1_uls_swt * gf3_uls_swt;

            list.Add(string.Format("ULS =  Nom_dsw_uls  =  Nom_dsw  x γf1_uls_swt  x  γf3_uls_swt"));
            list.Add(string.Format("                     = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_dsw, gf1_uls_swt, gf3_uls_swt, Nom_dsw_uls));
            list.Add(string.Format(""));
            list.Add(string.Format("----------"));
            list.Add(string.Format("Surfacing:"));
            list.Add(string.Format("----------"));
            list.Add(string.Format(""));

            double Nom_surfacing = thickness_surfacing / 1000 * unitwt_surfacing;

            list.Add(string.Format("Nominal Surfacing = Nom_surfacing = thickness_surfacing  x   unitwt_surfacing"));
            list.Add(string.Format("                                  =  {0:f3}  x  {1}  =  {2:f3}  kN/Sq.m", thickness_surfacing/1000, unitwt_surfacing, Nom_surfacing));

            list.Add(string.Format(""));



            double Nom_surfacing_sls = Nom_surfacing * gf1_sls_sur * gf3_sls_sur;
            list.Add(string.Format("SLS  =  Nom_surfacing_sls   =  Nom_surfacing  x  γf1_sls_sur  x  γf3_sls_sur"));
            list.Add(string.Format("                            = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_surfacing, gf1_sls_sur, gf3_sls_sur, Nom_surfacing_sls));

            list.Add(string.Format(""));

            double Nom_surfacing_uls = Nom_surfacing * gf1_uls_sur * gf3_uls_sur;

            list.Add(string.Format("ULS =  Nom_surfacing_uls =  Nom_surfacing  x  γf1_uls_sur  x  γf3_uls_sur"));
            list.Add(string.Format("                         = {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}   kN/Sq.m", Nom_surfacing, gf1_uls_sur, gf3_uls_sur, Nom_surfacing_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Dead Load Effect for the Unit Weight :"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 1 from Dialog Box"));
            list.Add(string.Format(""));

            dle_mhog_sls = (Nom_dsw_sls + Nom_surfacing_sls) * span_deckslab * span_deckslab / 12;

            list.Add(string.Format("Hog BM  =  dle_mhog_sls  =  (Nom_dsw_sls  + Nom_surfacing_sls) x span_deckslab x span_deckslab /12  "));
            list.Add(string.Format("                          = ({0:f3} + {1:f3}) x {2:f3} x {2:f3} / 12  =  {3:f3}  kNm", Nom_dsw_sls, Nom_surfacing_sls, span_deckslab, dle_mhog_sls));
            list.Add(string.Format(""));


            double dle_msag_sls = dle_mhog_sls / 2;
            list.Add(string.Format("Sag BM  =  dle_msag_sls = dle_mhog_sls  / 2  = {0:f3} / 2  =  {1:f3}  kNm", dle_mhog_sls, dle_msag_sls));
            list.Add(string.Format(""));

            double dle_V_sls = (Nom_dsw_sls + Nom_surfacing_sls) * span_deckslab / 2;
            list.Add(string.Format("Shear  = dle_V_sls  =  (Nom_dsw_sls  + Nom_surfacing_sls) x span_deckslab /2  "));
            list.Add(string.Format("                    = ({0:f3} + {1:f3}) x {2:f3}/2  =  {3:f3}  kN", Nom_dsw_sls, Nom_surfacing_sls, span_deckslab, dle_V_sls));
            list.Add(string.Format(""));

            dle_mhog_uls = (Nom_dsw_uls + Nom_surfacing_uls) * span_deckslab * span_deckslab / 12;

            list.Add(string.Format("Hog BM  = dle_mhog_uls = (Nom_dsw_uls + Nom_surfacing_uls) x span_deckslab x span_deckslab /12  "));
            list.Add(string.Format("                       = ({0:f3}  + {1:f3}) x {2:f3} x {2:f3}/12  =  {3:f3} kNm", Nom_dsw_uls, Nom_surfacing_uls, span_deckslab, dle_mhog_uls));
            list.Add(string.Format(""));

            double dle_msag_uls = dle_mhog_uls / 2;
            list.Add(string.Format("Sag BM  =  dle_msag_uls = dle_mhog_uls  / 2  = {0:f3}/2  =  {1:f3}  kNm", dle_mhog_uls, dle_msag_uls));
            list.Add(string.Format(""));

            double dle_V_uls = (Nom_dsw_uls + Nom_surfacing_uls) * span_deckslab / 2;

            list.Add(string.Format("Shear  = dle_V_uls = (Nom_dsw_uls  + Nom_surfacing_uls) x span_deckslab /2  "));
            list.Add(string.Format("                   = ({0:f3} + {1:f3}) x {2:f3}/2  =  {3:f3}  kN", Nom_dsw_uls, Nom_surfacing_uls, span_deckslab, dle_V_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format("STEP 1.1.2 : Live Load"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("HA & HB Loading"));
            list.Add(string.Format(""));
            list.Add(string.Format("{0} units of HB Loading is to be used for Bending Effect", wload_unit));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 2 from Dialog Box"));
            list.Add(string.Format(""));

            list.Add(string.Format(" "));
            //list.Add(string.Format("Figure 2"));
            list.Add(string.Format(""));
            list.Add(string.Format("One unit of axle = axle_unit = {0} kN", axle_unit));
            list.Add(string.Format("Load per wheel =  load_wheel = {0} kN (For single unit)", load_wheel));
            double wheel_load = wload_unit * load_wheel;
            list.Add(string.Format(""));
            list.Add(string.Format("For {0} unit wheel Load =  wheel_load  =  wload_units x load_wheel =  {0} x {1}  = {2}  kN", wload_unit, load_wheel, wheel_load));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 3 from Dialog Box"));
            list.Add(string.Format(""));

            double wheel_area = wheel_a1 * wheel_a2;
            list.Add(string.Format("Wheel Load Contact Area  =  wheel_a1 x wheel_a2   =  {0}  x  {1} = {2:f3} Sq.mm", wheel_a1, wheel_a2, wheel_area));
            list.Add(string.Format(""));
            list.Add(string.Format("Dispersal through surfacing  =  1H : 1V and considering the load acting on the top of slab"));
            list.Add(string.Format(""));
            double width_dispersion = span_deckslab - wheel_a1 / 1000.0 - 2 * d / 1000.0;
            //list.Add(string.Format("Dispersion width per wheel = width_dispersion  =  0.5911 m."));


            list.Add(string.Format("Dispersion width per wheel = width_dispersion  =  {0:f4} m.", width_dispersion));
            list.Add(string.Format(""));

            double udl_on_slab = wheel_load / width_dispersion;
            list.Add(string.Format("Therefore, UDL on Slab  =  wheel_load  / width_dispersion  =  {0:f3}  / {1:f3}  =  {2:f3} kN/m", wheel_load, width_dispersion, udl_on_slab));
            list.Add(string.Format(""));
            list.Add(string.Format("As the intensity of HB UDL on slab is same as HA Loading so HB Loading will be considered for Live Load."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 4 from Dialog Box"));
            list.Add(string.Format(""));

            list.Add(string.Format(""));

            list.Add(string.Format("-----------------------------------------------------------------"));
            list.Add(string.Format("STEP 1.2 : Calculation for Bending Moments and Shear Forces"));
            list.Add(string.Format("-----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Mhog  =  MAB  =  {0:f3} kNm", Mhog));
            list.Add(string.Format("Msag  =  {0:f3} kNm", Msag));
            list.Add(string.Format("VAB  =  RA  =  {0:f3} kN", V));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            LL_mhog_sls = Mhog * gf1_sls_Mhog * gf3_sls_Mhog * 25 / wload_unit;
            list.Add(string.Format("Live Load BM (hog) for SLS Mhog  = LL_mhog_sls  =Mhog  x γf1_sls_Mhog  x  γf3_sls_Mhog  x 25/ wload_units "));
            list.Add(string.Format("                                 = {0:f3} x {1:f2} x  {2:f2}  x  25 / {3}          = {4:f3}  kNm", Mhog, gf1_sls_Mhog, gf3_sls_Mhog, wload_unit, LL_mhog_sls));
            list.Add(string.Format(""));

            double gf1_sls_Msag = gf1_sls_Mhog;
            double gf3_sls_Msag = gf3_sls_Mhog;

            double LL_msag_sls = Msag * gf1_sls_Msag * gf3_sls_Msag * 25 / wload_unit;
            list.Add(string.Format("Live Load BM (sag) for SLS Msag = LL_msag_sls = Msag  x γf1_sls_Msag  x  γf3_sls_Msag x 25 / wload_units "));
            list.Add(string.Format("                                =  {0:f3}  x {1:f2}  x  {2:f2}  x  25/{3} = {4:f3}  kNm", Msag, gf1_sls_Msag, gf3_sls_Msag, wload_unit, LL_msag_sls));
            list.Add(string.Format(""));



            double gf1_sls_V = gf1_sls_Mhog;
            double gf3_sls_V = gf3_sls_Mhog;

            double LL_VAB_sls = V * gf1_sls_V * gf3_sls_V * 25 / wload_unit;
            list.Add(string.Format("Live Load Shear for SLS = LL_VAB_sls =  VAB  x γf1_sls_V  x  γf3_sls_V  x 25/ wload_units "));
            list.Add(string.Format("                                     =  {0:f3} x {1:f2}  x  {2:f2}  x  25/{3} =  {4:f2} kN", V, gf1_sls_V, gf3_sls_V, wload_unit, LL_VAB_sls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            LL_mhog_uls = Mhog * gf1_uls_Mhog * gf3_uls_Mhog * 30 / wload_unit;
            list.Add(string.Format("Live Load BM (hog) for ULS Mhog  = LL_mhog_uls = Mhog  x γf1_uls_Mhog  x  γf3_uls_Mhog  x 30 / wload_units "));
            list.Add(string.Format("                                                = {0:f3} x {1:f2}  x  {2:f2}  x  25 / {3}  = {4:f3}  kNm", Mhog, gf1_uls_Mhog, gf3_uls_Mhog, wload_unit, LL_mhog_uls));
            list.Add(string.Format(""));



            double gf1_uls_Msag = gf1_uls_Mhog;
            double gf3_uls_Msag = gf3_uls_Mhog;


            double LL_msag_uls = Msag * gf1_uls_Msag * gf3_uls_Msag * 25 / wload_unit;
            list.Add(string.Format("Live Load BM (sag) for ULS Msag = LL_msag_uls = Msag  x γf1_uls_Msag  x  γf3_uls_Msag    x 25/ wload_units"));
            list.Add(string.Format("                                               = {0:f3}  x {1:f2}  x  {2:f2}  x  25/{3} =  {4:f3}  kNm", Msag, gf1_uls_Msag, gf3_uls_Msag, wload_unit, LL_msag_uls));
            list.Add(string.Format(""));

            double gf1_uls_V = gf1_uls_Mhog;
            double gf3_uls_V = gf3_uls_Mhog;

            double LL_VAB_uls = V * gf1_uls_V * gf3_uls_V * 25 / wload_unit;

            list.Add(string.Format("Live Load Shear for ULS = LL_VAB_uls =  VAB  x γf1_uls_V  x  γf3_uls_V  x 25 / wload_units "));
            list.Add(string.Format("                                     =  {0:f3} x {1:f2} x {1:f2} x  25/{3} =  {4:f3} kN", V, gf1_uls_V, gf3_uls_V, wload_unit, LL_VAB_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format("Maximum Sagging Moment"));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format(""));



            double MDL_LL_uls_Sag = dle_msag_uls + LL_msag_uls;
            double MDL_sls_Sag = dle_msag_sls;
            double MLL_sls_Sag = LL_msag_sls;

            list.Add(string.Format("MDL + LL [ULS] =  {0:f3} + {1:f3} = {2:f3}  kNm.", dle_msag_uls, LL_msag_uls, MDL_LL_uls_Sag));
            list.Add(string.Format("MDL [SLS]  =  {0:f3} kNm.", MDL_sls_Sag));
            list.Add(string.Format("MLL [SLS]  =  {0:f3} kNm.", MLL_sls_Sag));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Hogging Moment"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            MDL_LL_uls_Hog = dle_mhog_uls + LL_mhog_uls;
            MDL_sls_Hog = dle_mhog_sls;
            MLL_sls_Hog = LL_mhog_sls;


            list.Add(string.Format("MDL+LL  [ULS]  =  {0:f3} + {1:f3} = {2:f3}  kNm.", dle_mhog_uls, LL_mhog_uls, MDL_LL_uls_Hog));
            list.Add(string.Format("MDL [SLS]  =  {0:f3} kNm.", MDL_sls_Hog));
            list.Add(string.Format("MLL [SLS]  =  {0:f3}  kNm.", MLL_sls_Hog));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Shear Force"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));

            VDL_LL_uls = dle_V_uls + LL_VAB_uls;
            VDL_sls = dle_V_sls;
            VLL_sls = LL_VAB_sls;
            list.Add(string.Format("VDL+LL  [ULS] = {0:f3} + {1:f3} = {2:f3} kN.", dle_V_uls, LL_VAB_uls, VDL_LL_uls));
            list.Add(string.Format("VDL [SLS]  =  {0:f3} kN.", VDL_sls));
            list.Add(string.Format("VLL [SLS]  =  {0:f3} kN.", VLL_sls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            txt_SC_VMax.Text = Math.Max(Math.Max(VDL_LL_uls, VDL_sls), VLL_sls).ToString("f3");
            #endregion DS

        }

        public void Deckslab_Flexural_Cracking()
        {
            List<string> list = new List<string>();


            if (girder_no > 1)
            {
                list.Clear();
                Deckslab_Flexural_Cracking(list);
                txt_FC_results.Lines = list.ToArray();
                //MessageBox.Show("Data Processed Successfull.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                txt_FC_results.Text = "";

        }

        private void Deckslab_Flexural_Cracking(List<string> list)
        {


            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("STEP 1.4 : Check Flexural Cracking"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _b = MyList.StringToDouble(txt_FC_b.Text, 0.0);
            double _h = MyList.StringToDouble(txt_FC_h.Text, 0.0);
            double _Cnom = MyList.StringToDouble(txt_FC_cnom.Text, 0.0);
            double _Cmin = MyList.StringToDouble(txt_FC_cmin.Text, 0.0);



            list.Add(string.Format("Breadth of section = b = {0} mm", _b));
            list.Add(string.Format("Depth of section = h = {0} mm", _h));



            list.Add(string.Format("Reinforcement Cover (Nominal) = Cnom = {0} mm", _Cnom));
            list.Add(string.Format("Reinforcement Cover (Minimum) = Cmin = {0} mm", _Cmin));
            list.Add(string.Format("Reinforcement bars = bar_dia = {0}  mm", bar_dia));
            list.Add(string.Format("Spacing = spacing = {0} mm", bar_spacing));
            list.Add(string.Format("No of bars = bar_nos = b / spacing = {0} / {1} = {2:f3} = {3} Nos", _b, bar_spacing, (_b / bar_spacing), bar_no));
            list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4"));
            list.Add(string.Format(""));

            Ast = bar_no * Math.PI * bar_dia * bar_dia / 4;
            list.Add(string.Format("                   = {0} x (3.1416 x {1} x {1})/4 = {2:f3} Sq.mm", bar_no, bar_dia, Ast));

            double _d = _h - _Cnom - bar_dia / 2;

            list.Add(string.Format("Effective depth, d = h - Cnom - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", _h, _Cnom, bar_dia, _d));
            list.Add(string.Format(""));


            double _a_dash = MyList.StringToDouble(txt_FC_a_dash.Text, 0.0);
            double _M = MyList.StringToDouble(txt_FC_M.Text, 0.0);



            list.Add(string.Format("Distance from compression face to point at which crack is calculated, a', = {0} mm", _a_dash));
            list.Add(string.Format("Service Moment = M = {0} kNm", _M));

            double _Ec = MyList.StringToDouble(txt_FC_Ec.Text, 0.0);
            list.Add(string.Format("Instantaneous modulus of elasticity = Ec = {0} kN/Sq.mm", _Ec));

            double _Es = MyList.StringToDouble(txt_FC_Es.Text, 0.0);
            list.Add(string.Format("Modulus of Elasticity of Steel = Es = {0} kN/Sq.mm", _Es));

            double _flx_crk = MyList.StringToDouble(txt_FC_flx_crk.Text, 0.0);
            list.Add(string.Format("Flexural Crack width aimed for = {0} mm", _flx_crk));

            double _a_cr = MyList.StringToDouble(txt_FC_a_cr.Text, 0.0);
            list.Add(string.Format("Distance to surface of nearest rebar, a_cr = {0} mm", _a_cr));

            double ae = _Es * 2 / _Ec;

            list.Add(string.Format("Modular Ratio, = αe = Es x 2 / Ec = {0} x 2 / {1} = {2:f3} = (Ec long term = Ec /2)", _Es, _Ec, ae));

            //double Mq = MLL_sls_Hog;
            //double Mg = MDL_sls_Hog;



            double Mq = MyList.StringToDouble(txt_FC_Mq.Text, 0.0);

            
            double Mg = MyList.StringToDouble(txt_FC_Mg.Text, 0.0);



            list.Add(string.Format("Bending Moment for Live load = {0:f3} kN-m", MLL_sls_Hog));

            list.Add(string.Format("Bending Moment for Dead Load = {0:f3} kN-m", MDL_sls_Hog));
            list.Add(string.Format(""));


            //double val1 = -((ae * Ast) + Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * _d))) / _b;
            //double val2 = -((ae * Ast) - Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * _d))) / _b;

            double val1 = -((ae * Ast) + Math.Sqrt(((ae * Ast) * (ae * Ast)) + (2 * _b * ae * Ast * _d))) / _b;
            double val2 = -((ae * Ast) - Math.Sqrt(((ae * Ast) * (ae * Ast)) + (2 * _b * ae * Ast * _d))) / _b;



            //double val1 = - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b



            //double dc  =  - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b;
            double dc = Math.Max(val1, val2);

            list.Add(string.Format("Depth of neutral axis, = dc  = - [(αe x As) ± √(((αe x As) x (αe x As)) + (2 x b x αe x As x d))] / b"));
            //list.Add(string.Format("                             = - [({0:f3} x {1:f3}) ± √(({0:f3} x {1:f3}) x ({0:f3} x {1:f3})) + ({2:f3} x {3:f3} x {0:f3} x {1:f3} x {4:f3}))] / {3:f3}", ae, Ast, z, _b, d));

            list.Add(string.Format("                             = - [({0:f3} x {1:f3}) ± √(({0:f3} x {1:f3}) x ({0:f3} x {1:f3}))", ae, Ast));
            list.Add(string.Format("                               + ({0:f3} x {1:f3} x {2:f3} x {3:f3} x {4:f3}))] / {1:f3}", 2, _b, ae, Ast, d));

            list.Add(string.Format("                             = {0:f3} mm.", dc));
            list.Add(string.Format(""));

            double fs = Math.Abs(_M / (Ast * (d - (dc / 3))) * 1000 * 1000);


            list.Add(string.Format("Reinforcement tensile stress = fs = M x 1000 x 1000 / (As x (d - (dc/3)))"));
            list.Add(string.Format("                                  = {0:f3} x 1000 x 1000 / ({1:f3} x ({2} - ({3:f3}/3)))", _M, Ast, d, dc));
            list.Add(string.Format("                                  = {0:f5}  N / Sq. mm", fs));
            list.Add(string.Format(""));

            double e1 = ((_a_dash - dc) * fs) / ((d - dc) * _Es);
            list.Add(string.Format("Flexural Strain = ((a’ - dc) x fs) / ((d - dc) x Es)"));
            list.Add(string.Format("                = (({0:f3} - {1:f3}) x {2:f3}) / (({3} - {1:f3}) x {4:f3})", _a_dash, dc, fs, d, _Es));
            list.Add(string.Format("                =  {0:f4}", e1));
            list.Add(string.Format(""));

            if (e1 > 0.001) e1 = 0.001;
            list.Add(string.Format("Thus total strain = e1 = {0:f4}", e1));
            list.Add(string.Format(""));

            double es = iApp.Tables.Depth_Factor_BS5400_Table_9(d);
            //list.Add(string.Format("es = Depth Factor = 1.438 (BS 5400,table 9)"));
            list.Add(string.Format("es = Depth Factor = {0:f4} (BS 5400,table 9)", es));
            list.Add(string.Format(""));

            //double em = e1 - ((3.8 * _b * _h * (_a_dash - dc)) / (_Es * Ast * (_h - dc))) * ((1 - (Mq / Mg)) / 1000000000.0);
            double em = e1 - ((3.8 * _b * _h * (_a_dash - dc)) / (es * Ast * (_h - dc))) * ((1 - (Mg / Mq)) / 1000000000.0);

            list.Add(string.Format("em   = e1 - [3.8 x bt x h x (a’ - dc) ] / [es x As x (h-dc)] x [1 - (Mq / Mg)] / 1,000,000,000 "));
            list.Add(string.Format("                 (but not greater than e1)"));
            list.Add(string.Format(""));
            list.Add(string.Format("     = {0:f6}", em));
            list.Add(string.Format(""));

            double Wmax = (3 * _a_cr * em) / (1 + 2 * (_a_cr - _Cmin) / (_h - dc));

            list.Add(string.Format("Crack width = Wmax = (3 x acr x em) / [1 + 2 x (acr - cmin) / (h - dc)]"));
            list.Add(string.Format("                   = (3 x {0:f3} x {1:f5}) / [1 + 2 x ({0:f3} - {2:f3}) / ({3} - {4:f3})]", _a_cr, em, _Cmin, _h, dc));
            if (Wmax < 0.25)
                list.Add(string.Format("                   = {0:f3}  mm < 0.25 mm.   OK", Wmax));
            else
                list.Add(string.Format("                   = {0:f3}  mm >= 0.25 mm.   NOT OK", Wmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

        }

        public void Deckslab_Shear_Calculation()
        {
            List<string> list = new List<string>();


            if (girder_no > 1)
            {
                list.Clear();
                Deckslab_Shear_Calculation(list);
                txt_SC_results.Lines = list.ToArray();
                //MessageBox.Show("Data Processed Successfull.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                txt_SC_results.Text = "";


        }

        private void Deckslab_Shear_Calculation(List<string> list)
        {

            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format("STEP 1.5 : Shear Calculation"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Check for Shear Reinforcement"));
            list.Add(string.Format(""));

            //double Vmax = VDL_LL_uls;
            double Vmax = MyList.StringToDouble(txt_SC_VMax.Text, 0.0);

            list.Add(string.Format("Max. Shear Force, Vmax.= {0:f3} kN", Vmax));
            list.Add(string.Format(""));
            double _b = MyList.StringToDouble(txt_SC_b.Text, 0.0);

            list.Add(string.Format("b = {0} mm ", _b));
            list.Add(string.Format("Reinforcement Bar Dia  = bar_dia = {0}", bar_dia));
            list.Add(string.Format("Bar Nos = bar_nos = {0}", bar_no));
            list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4 "));
            list.Add(string.Format("                   = {0} x 3.1416 x {1} x {1} / 4 ", bar_no, bar_dia));
            list.Add(string.Format("                   = {0:f3} Sq.mm ", Ast));
            list.Add(string.Format(""));

            double _cover = MyList.StringToDouble(txt_SC_Cover.Text, 0.0);
            double _h = MyList.StringToDouble(txt_SC_h.Text, 0.0);
            double _d = MyList.StringToDouble(txt_SC_d.Text, 0.0);

            list.Add(string.Format("Reinforcement Cover = cover = {0} mm", _cover));
            list.Add(string.Format("Overall Slab Thickness = h = {0} mm", _h));
            list.Add(string.Format("Effective Depth = d = h - cover - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", _h, _cover, bar_dia, _d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _fcu = MyList.StringToDouble(txt_SC_fcu.Text, 0.0);
            double _fcu_allow = MyList.StringToDouble(txt_SC_fcu_allow.Text, 0.0);
            double _fyv = MyList.StringToDouble(txt_SC_fyv.Text, 0.0);

            list.Add(string.Format("fcu = {0} N / Sq.mm", _fcu));
            list.Add(string.Format("allowable  fcu {0} = N / Sq.mm (for shear only)", _fcu_allow));
            list.Add(string.Format("fyv= {0} N / Sq.mm", _fyv));
            list.Add(string.Format(""));


            double xi_s = iApp.Tables.Depth_Factor_BS5400_Table_9(d);
            list.Add(string.Format("ξs = Depth Factor =  {0:f4}    (BS 5400,table 9)", xi_s));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide Reinforcement = {0} Nos. {1} ɸ bars", bar_no, bar_dia));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of steel provided, As = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));

            double p = 100 * Ast / (_b * _d);
            list.Add(string.Format("Percentage = 100 x As / (b x d) = 100 x {0:f3} / (1000 x {1}) = {2:f3} %", Ast, _b, _d));
            list.Add(string.Format(""));

            //double vc = 0.77;
            
            CONCRETE_GRADE cgrd = (CONCRETE_GRADE)(int)Fck;

            string ref_str = "";
            double vc = iApp.Tables.Permissible_Shear_Stress(p, cgrd, ref ref_str);

            list.Add(string.Format("vc = Ultimate shear stress in concrete = {0:f3} N / Sq.mm ( BS 5400,table 8)", vc));
            list.Add(string.Format(""));

            double v = V / (_b * _d);

            //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
            if (v < (xi_s * vc))
                list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:E3} N / Sq.mm   <   ξs x vc = {1:f3} N / Sq.mm     Hence, OK.", v, (xi_s * vc)));
            else
                list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:E3} N / Sq.mm   >   ξs x vc = {1:f3} N / Sq.mm     Hence, NOT OK.", v, (xi_s * vc)));

            //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal steel for shear fyv = {0} N/Sq.mm", _fyv));
            list.Add(string.Format(""));

            list.Add(string.Format("V = Vmax.= {0:f3} kN", Vmax));

            double Asa = Vmax * 1000 / (2.0 * 0.87 * _fyv);

            list.Add(string.Format("Asa  >=  V x 1000 / (2 x 0.87 x fyv)", Asa));
            list.Add(string.Format("      =  {0:f3} x 1000 / (2 x 0.87 x {1}) = {2:f3}  Sq.mm", Vmax, _fyv, Asa));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
        }
        public void Change_Color(TextBox txt)
        {
            if (txt.Text.Contains("NOT OK"))
                txt.BackColor = Color.Azure;
            else
                txt.BackColor = Color.White;
        }


        private void btn_process_Click(object sender, EventArgs e)
        {
            OnCreateData(sender, e);



            Button btn = sender as Button;
            if (btn.Name == btn_process_analysis.Name)
            {
                //if (Show_Flag(1))
                Process_Analysis();
                Set_Flag(1);
                goto _button_click;
                //return;
            }
            else if (btn.Name == btn_view_data.Name)
            {
                Create_Analysis_Data();

                if (File.Exists(INPUT_FILE))
                    iApp.RunExe(INPUT_FILE);

                goto _button_click;
            }
            else if (btn.Name == btn_view_ana_report.Name)
            {

                if (File.Exists(MyList.Get_Analysis_Report_File(INPUT_FILE)))
                    iApp.RunExe(MyList.Get_Analysis_Report_File(INPUT_FILE));
                goto _button_click;
            }
            else if (btn.Name == btn_view_ana_model.Name)
            {

                if (File.Exists(INPUT_FILE))
                    iApp.OpenWork(INPUT_FILE, false);
                goto _button_click;
            }
            else if (btn.Name == btn_view_report_txt.Name)
            {
                //Calculate_Program();
                if (File.Exists(Deckslab_Report_File))
                    iApp.RunExe(Deckslab_Report_File);

                goto _button_click;
                //return;
            }
            else if (btn.Name == btn_process_design.Name)
            {
                //Calculate_Program();
                if (!Show_Flag(11)) return;
                Process_Design(Deckslab_Report_File);

                iApp.View_Result(Deckslab_Report_File);

                goto _button_click;
                //return;
            }
            else if (btn.Name == btn_DS_process.Name)
            {

                if (!Show_Flag(2)) return;
                Design_of_Deck_Slab();
                Set_Flag(2);
                Change_Color(txt_DS_results);
            }
            else if (btn.Name == btn_TBR_process.Name)
            {
                if (!Show_Flag(3)) return;
                Deckslab_Transverse_Bending_Resistance();
                Set_Flag(3);
                Change_Color(txt_TBR_results);
            }
            else if (btn.Name == btn_FC_process.Name)
            {
                if (!Show_Flag(4)) return;
                Deckslab_Flexural_Cracking();
                Set_Flag(4);
                Change_Color(txt_FC_results);
            }
            else if (btn.Name == btn_SC_process.Name)
            {
                if (!Show_Flag(5)) return;
                Deckslab_Shear_Calculation();
                Set_Flag(5);
                Change_Color(txt_SC_results);
            }

            if (girder_no == 1)
            {
                MessageBox.Show("For Single Beam Deckslab Design is not applicable. User may derectly go to Cantilever Design.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Processed.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        _button_click:
            OnButtonClick(sender, e);

        Button_Enabled_Disabled();

        }


        public void Process_Analysis()
        {
            if (!File.Exists(INPUT_FILE))
                Create_Analysis_Data();

            string flPath = INPUT_FILE;

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (File.Exists(prs.StartInfo.FileName))
            {
                if (prs.Start())
                {
                    prs.WaitForExit();


                    string ana_rep = MyList.Get_Analysis_Report_File(flPath);
                    if (File.Exists(ana_rep))
                    {
                        BridgeMemberAnalysis bma = new BridgeMemberAnalysis(iApp, ana_rep);
                        List<int> ls_joints = new List<int>();
                        List<int> ls_load = new List<int>();

                        ls_joints.Add(1);
                        ls_joints.Add(2);
                        ls_joints.Add(3);
                        ls_joints.Add(4);

                        txt_Mhog.Text = bma.GetJoint_Max_Hogging(ls_joints, true).ToString();
                        txt_Msag.Text = Math.Abs(bma.GetJoint_Max_Sagging(ls_joints, true)).ToString();
                        txt_V.Text = bma.GetJoint_ShearForce(ls_joints, true).ToString();

                    }
                }
            }
            else
            {
                MessageBox.Show(prs.StartInfo.FileName + " not found."); return;
            }

        }

        public void Button_Enabled_Disabled()
        {
            //btn_process_analysis.Enabled = File.Exists(INPUT_FILE);
            btn_view_ana_model.Enabled = File.Exists(INPUT_FILE);
            btn_view_ana_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(INPUT_FILE));
            btn_view_report_txt.Enabled = File.Exists(Deckslab_Report_File);
        }
        private void Create_Analysis_Data()
        {

            List<string> list = new List<string>();


            double mem_len = MyList.StringToDouble(txt_ld1_mem_len.Text, 0.0);
            double ld1_ld = MyList.StringToDouble(txt_ld1_ld.Text, 0.0);
            double ld1_ld_start = MyList.StringToDouble(txt_ld1_ld_start.Text, 0.0);
            double ld1_ld_end = MyList.StringToDouble(txt_ld1_ld_end.Text, 0.0);

            //double mem_len = MyList.StringToDouble(txt_ld1_mem_len.Text, 0.0);
            double ld2_ld = MyList.StringToDouble(txt_ld2_ld.Text, 0.0);
            double ld2_ld_start = MyList.StringToDouble(txt_ld2_ld_start.Text, 0.0);
            double ld2_ld_end = MyList.StringToDouble(txt_ld2_ld_end.Text, 0.0);



            list.Add(string.Format("ASTRA SPACE ANALYSIS FOR BRIDGE DECK SLAB IN BS 5400"));
            list.Add(string.Format("UNIT METER KN"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1 0 0 0; "));
            list.Add(string.Format("2 {0:f3} 0 0;", (ld1_ld_start + ld1_ld_end) / 2.0));
            //list.Add(string.Format("2 0.2955 0 0;"));
            //list.Add(string.Format("3 0.489 0 0;"));
            list.Add(string.Format("3 {0:f3} 0 0;", (ld2_ld_start + ld2_ld_end) / 2.0));
            list.Add(string.Format("4 {0:f3} 0 0;", mem_len));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1 1 2;"));
            list.Add(string.Format("2 2 3;"));
            list.Add(string.Format("3 3 4;"));
            list.Add(string.Format("SECTION PROPERTIES"));
            //list.Add(string.Format("1 TO 3 PRIS YD 0.18 ZD 1"));
            list.Add(string.Format("1 TO 3 PRIS YD {0} ZD {1}", txt_sp_YD.Text, txt_sp_ZD.Text));
            list.Add(string.Format("MATERIAL CONSTANTS"));
            list.Add(string.Format("E {0} ALL", txt_sp_emod.Text));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));
            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("1 4 FIXED"));
            list.Add(string.Format("*4 PINNED"));
            list.Add(string.Format("LOAD 1 TITLE LOAD CASE 1"));
            list.Add(string.Format("*MEMBER LOAD"));
            //list.Add(string.Format("*1 UNI GY -126.88 0 0.5911"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*   ({0:f3} * ({1:f3} - {2:f3}))", ld1_ld , ld1_ld_end , ld1_ld_start));
            list.Add(string.Format("2 FY {0:f3}", (ld1_ld * ((ld1_ld_end - ld1_ld_start)))));
            list.Add(string.Format("LOAD 2 TITLE LOAD CASE 2"));



            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*   ({0:f3} * ({1:f3} - {2:f3}))", ld2_ld, ld2_ld_end, ld2_ld_start));
            list.Add(string.Format("3 FY {0:f3}", (ld2_ld * ((ld2_ld_end - ld2_ld_start)))));
            //list.Add(string.Format("3 FY -62.055"));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));


            File.WriteAllLines(INPUT_FILE, list.ToArray());

        }
        public void Process_Design(string file_name)
        {
            List<string> list = new List<string>();

            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*                  ASTRA Pro                  *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*  DESIGN OF DECK SLAB AS PER BS 5400 CODE    *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion
            if (txt_DS_results.Lines.Length == 0)
                Design_of_Deck_Slab();
            if (txt_TBR_results.Lines.Length == 0)
                Deckslab_Transverse_Bending_Resistance();
            if (txt_FC_results.Lines.Length == 0)
                Deckslab_Flexural_Cracking();
            if (txt_SC_results.Lines.Length == 0)
                Deckslab_Shear_Calculation();

            if (rtb_cant_DC_results.Lines.Length == 0)
                Design_Of_Cantilever_Slab();
            if (txt_cant_TBR_results.Lines.Length == 0)
                Cantilever_Transverse_Bending_Resistance();
            if (txt_cant_FC_results.Lines.Length == 0)
                Cantilever_Flexural_Cracking();
            if (txt_cant_SC_results.Lines.Length == 0)
                Cantilever_Shear_Calculation();
            if (txt_cant_LBR_results.Lines.Length == 0)
                Cantilever_Longitudinal_Bending_Resistance();



            list.AddRange(txt_DS_results.Lines);
            list.AddRange(txt_TBR_results.Lines);
            list.AddRange(txt_FC_results.Lines);
            list.AddRange(txt_SC_results.Lines);

            list.AddRange(rtb_cant_DC_results.Lines);
            list.AddRange(txt_cant_TBR_results.Lines);
            list.AddRange(txt_cant_FC_results.Lines);
            list.AddRange(txt_cant_SC_results.Lines);
            list.AddRange(txt_cant_LBR_results.Lines);

            list.AddRange(iApp.Tables.Get_Permissible_Shear_Stress());
            list.Add(string.Format(""));
            list.AddRange(iApp.Tables.Get_Depth_Factor_BS5400_Table_9());
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("          END OF REPORT "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));

            File.WriteAllLines(file_name, list.ToArray());
        }

        private void txt_Deckslab_TextChanged(object sender, EventArgs e)
        {
            Deckslab_Text_Changed();
        }

        private void Deckslab_Text_Changed()
        {
            d = h - cover - bar_dia / 2;
            d_total = h + d1;

            bar_no = (int)((1000 / bar_spacing) + 1);
            load_wheel = axle_unit / 4;

            b1 = (b - cl - cr) / (girder_no + 1);
            b2 = (b1 - 2 * width_girderflange / 2);
            span_deckslab = b2 + d / 1000.0;

            txt_sp_YD.Text = (h / 1000).ToString();
            txt_ld1_mem_len.Text = span_deckslab.ToString("f3");
            txt_ld2_mem_len.Text = span_deckslab.ToString("f3");




            double width_dispersion = span_deckslab - wheel_a1 / 1000.0 - 2 * d / 1000.0;
            //list.Add(string.Format("Dispersion width per wheel = width_dispersion  =  0.5911 m."));


            double wheel_load = wload_unit * load_wheel;

            double udl_on_slab = -wheel_load / width_dispersion;


            txt_ld1_ld.Text = udl_on_slab.ToString("f2");
            txt_ld1_ld_end.Text = width_dispersion.ToString("f3");
            txt_ld2_ld_start.Text = ((span_deckslab - width_dispersion) / 2).ToString("f3");
            txt_ld2_ld_end.Text = (((span_deckslab - width_dispersion) / 2) + width_dispersion).ToString("f3");


            #region Design of Deckslab

            txt_DS_b1_.Text = txt_ds_b1.Text;
            txt_DS_b2_.Text = txt_ds_b2.Text;
            txt_DS_span_deckslab_.Text = txt_ds_span_deckslab.Text;

            #endregion Design of Deckslab

            #region TBR

            txt_TBR_cover.Text = txt_ds_cover.Text;
            txt_TBR_h.Text = txt_ds_h.Text;
            txt_TBR_spacing.Text = txt_ds_bar_spacing.Text;
            txt_TBR_d.Text = txt_ds_d.Text;
            txt_TBR_fcu.Text = txt_ds_Fck.Text;
            txt_TBR_fy.Text = txt_ds_Fy.Text;

            #endregion TBR


            #region FC

            //txt_FC_b.Text = txt_ds_cover.Text;
            txt_FC_h.Text = txt_ds_h.Text;
            txt_FC_cnom.Text = txt_ds_cover.Text;
            txt_FC_bar_dia.Text = txt_ds_bar_dia.Text;
            txt_FC_spacing.Text = txt_ds_bar_spacing.Text;
            txt_FC_bar_nos.Text = txt_ds_bar_no.Text;
            txt_FC_d.Text = txt_ds_d.Text;

            #endregion FC


            #region SC

            //txt_FC_b.Text = txt_ds_cover.Text;
            txt_SC_Cover.Text = txt_ds_cover.Text;
            txt_SC_h.Text = txt_ds_h.Text;
            txt_SC_spacing.Text = txt_ds_bar_spacing.Text;
            txt_SC_d.Text = txt_ds_d.Text;
            txt_SC_fcu.Text = txt_ds_Fck.Text;
            txt_SC_fyv.Text = txt_ds_Fy.Text;


            #endregion SC
        }

        private void frm_Deckslab_LS_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            list.Add(string.Format("Edge Beam,1.0,1.0,,1.2,1.1"));
            list.Add(string.Format("Verge Fill,1.2,1.0,,1.75,1.1"));
            list.Add(string.Format("Self Weight,1.0,1.0,,1.2,1.1"));
            list.Add(string.Format("Parapet Load,1.0,1.0,,1.0,1.0"));
            list.Add(string.Format("Footpath Live Load,1.5,1.1,,1.0,1.0"));
            list.Add(string.Format("Accidental Wheel Load,1.2,1.0,,1.3,1.1"));
            MyList mlist;
            foreach (var item in list)
            {
                mlist = new MyList(item, ',');

                dgv_cant_DC_sls_uls_factor.Rows.Add(mlist.StringList.ToArray());
                
            }
         
            Deckslab_Text_Changed();
        }


        #endregion Deckslab Design

        #region Design of Cantiver Slab
        private void btn_cant_DC_process_Click(object sender, EventArgs e)
        {

            OnCreateData(sender, e);

            Button btn = sender as Button;

            if (btn.Name == btn_cant_DC_process.Name)
            {
                if (!Show_Flag(6)) return;
                    Design_Of_Cantilever_Slab();
                Set_Flag(6);
            }
            else if (btn.Name == btn_cant_TBR_process.Name)
            {
                if (!Show_Flag(7)) return;
                    Cantilever_Transverse_Bending_Resistance();
                Set_Flag(7);
            }
            else if (btn.Name == btn_cant_FC_process.Name)
            {
                if (!Show_Flag(8)) return;
                    Cantilever_Flexural_Cracking();
                Set_Flag(8);
            }
            else if (btn.Name == btn_cant_SC_process.Name)
            {
                if (!Show_Flag(9)) return;
                    Cantilever_Shear_Calculation();
                Set_Flag(9);
            }
            else if (btn.Name == btn_cant_LBR_process.Name)
            {
                if (!Show_Flag(10)) return;
                    Cantilever_Longitudinal_Bending_Resistance();
                Set_Flag(10);
            }
            MessageBox.Show("Data Processed.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OnButtonClick(sender, e);

        }

        private void Design_Of_Cantilever_Slab()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format("STEP 2.0 : Design of Cantilever Slab"));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2.1 : Cantilever Slab Design Data"));
            list.Add(string.Format("----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            double _h = MyList.StringToDouble(txt_cant_DC_h.Text, 0.0);
            double _c_width = MyList.StringToDouble(txt_cant_DC_c_width.Text, 0.0);
            double _C_Length = MyList.StringToDouble(txt_cant_DC_C_Length.Text, 0.0);
            double _C_Thick = MyList.StringToDouble(txt_cant_DC_C_Thick.Text, 0.0);
            double _depth_infill = MyList.StringToDouble(txt_cant_DC_depth_infill.Text, 0.0);
            double _pload_sls = MyList.StringToDouble(txt_cant_DC_pload_sls.Text, 0.0);
            double _pload_uls = MyList.StringToDouble(txt_cant_DC_pload_uls.Text, 0.0);
            double _fpll = MyList.StringToDouble(txt_cant_DC_fpll.Text, 0.0);
            double _awl = MyList.StringToDouble(txt_cant_DC_awl.Text, 0.0);
            double _awll = MyList.StringToDouble(txt_cant_DC_awll.Text, 0.0);
            double _ada = MyList.StringToDouble(txt_cant_DC_ada.Text, 0.0);
            double _adb = MyList.StringToDouble(txt_cant_DC_adb.Text, 0.0);
            double _dal = MyList.StringToDouble(txt_cant_DC_dal.Text, 0.0);


            //Chiranjit [2014 10 05]

            #region Extra Inputs
            double _fp_thick = MyList.StringToDouble(txt_cant_DC_fp_Thick.Text, 0.0);
            double _cover = MyList.StringToDouble(txt_cant_DC_Cover.Text, 0.0);
            double _bar_dia = MyList.StringToDouble(txt_cant_DC_bar_dia.Text, 0.0);

            #endregion Extra Inputs


            double _gf1_sls_dl = MyList.StringToDouble(txt_cant_DC_gf1_sls_dl.Text, 0.0);
            double _gf3_sls_dl = MyList.StringToDouble(txt_cant_DC_gf3_sls_dl.Text, 0.0);
            double _gf1_uls_dl = MyList.StringToDouble(txt_cant_DC_gf1_uls_dl.Text, 0.0);
            double _gf3_uls_dl = MyList.StringToDouble(txt_cant_DC_gf3_uls_dl.Text, 0.0);


            double _gf1_sls_vf = MyList.StringToDouble(txt_cant_DC_gf1_sls_vf.Text, 0.0);
            double _gf3_sls_vf = MyList.StringToDouble(txt_cant_DC_gf3_sls_vf.Text, 0.0);
            double _gf1_uls_vf = MyList.StringToDouble(txt_cant_DC_gf1_uls_vf.Text, 0.0);
            double _gf3_uls_vf = MyList.StringToDouble(txt_cant_DC_gf3_uls_vf.Text, 0.0);



            double _gf1_sls_csw = MyList.StringToDouble(txt_cant_DC_gf1_sls_csw.Text, 0.0);
            double _gf3_sls_csw = MyList.StringToDouble(txt_cant_DC_gf3_sls_csw.Text, 0.0);
            double _gf1_uls_csw = MyList.StringToDouble(txt_cant_DC_gf1_uls_csw.Text, 0.0);
            double _gf3_uls_csw = MyList.StringToDouble(txt_cant_DC_gf3_uls_csw.Text, 0.0);




            double _gf1_sls_pl = MyList.StringToDouble(txt_cant_DC_gf1_sls_pl.Text, 0.0);
            double _gf3_sls_pl = MyList.StringToDouble(txt_cant_DC_gf3_sls_pl.Text, 0.0);
            double _gf1_uls_pl = MyList.StringToDouble(txt_cant_DC_gf1_uls_pl.Text, 0.0);
            double _gf3_uls_pl = MyList.StringToDouble(txt_cant_DC_gf3_uls_pl.Text, 0.0);




            double _gf1_sls_fpll = MyList.StringToDouble(txt_cant_DC_gf1_sls_fpll.Text, 0.0);
            double _gf3_sls_fpll = MyList.StringToDouble(txt_cant_DC_gf3_sls_fpll.Text, 0.0);
            double _gf1_uls_fpll = MyList.StringToDouble(txt_cant_DC_gf1_uls_fpll.Text, 0.0);
            double _gf3_uls_fpll = MyList.StringToDouble(txt_cant_DC_gf3_uls_fpll.Text, 0.0);



            double _gf1_sls_awl = MyList.StringToDouble(txt_cant_DC_gf1_sls_awl.Text, 0.0);
            double _gf3_sls_awl = MyList.StringToDouble(txt_cant_DC_gf3_sls_awl.Text, 0.0);
            double _gf1_uls_awl = MyList.StringToDouble(txt_cant_DC_gf1_uls_awl.Text, 0.0);
            double _gf3_uls_awl = MyList.StringToDouble(txt_cant_DC_gf3_uls_awl.Text, 0.0);



            list.Add(string.Format("Thickness of Cantilever slab = h = {0} m", _h));
            list.Add(string.Format("Total cantilever = c_width = {0} m", _c_width));
            list.Add(string.Format(""));
            list.Add(string.Format("For Cantilever Self weight calculation :"));
            list.Add(string.Format("Length = C_Length = {0} m. Thickness = C_Thick = {1} m.", _C_Length, _C_Thick));
            list.Add(string.Format(""));
            list.Add(string.Format("Depth of the infill = depth_infill = {0} mm", _depth_infill));
            list.Add(string.Format("Parapet Load for SLS = pload_sls = {0} kN/m", _pload_sls));
            list.Add(string.Format("Parapet Load for ULS = pload_uls = {0} kN/m", _pload_uls));
            list.Add(string.Format("Footpath Live Load =  fpll = {0}  kN/Sq.m        (BD 37/01  Cl. 6.5.1)", _fpll));
            list.Add(string.Format("Accidental Wheel Load = awl = {0} kN", _awl));
            list.Add(string.Format("Accidental Wheel Load Length = awll = {0} m.", _awll));
            list.Add(string.Format("Accidental Wheel Load Dispersal Area = (ada = {0} m) x (adb = {1} m)", _ada, _adb));
            list.Add(string.Format("Distance between accidental l oad axle = dal = {0} m", _dal));
            list.Add(string.Format(""));
            list.Add(string.Format("Footpath Thickness = fp_thick = {0} m", _fp_thick));
            list.Add(string.Format("Reinforcement Cover = cover = {0} mm", _cover));
            list.Add(string.Format("Reinforcement Bar Dia = bar_dia = {0} mm", _bar_dia));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.2 : Cantilever Load Calculation"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                          SLS                                        ULS        "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Load  calculation:              γf1                   γf3                  γf1                 γf3       "));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Edge Beam                 γf1_sls_dl ={0:f2}         γf3_sls_dl ={1:f2}    γf1_uls_dl ={2:f2}    γf3_uls_dl ={3:f2}", _gf1_sls_dl, _gf3_sls_dl, _gf1_uls_dl, _gf3_uls_dl));
            list.Add(string.Format("Verge Fill                γf1_sls_vf ={0:f2}         γf3_sls_vf ={1:f2}    γf1_uls_vf ={2:f2}    γf3_uls_vf ={3:f2}", _gf1_sls_vf, _gf3_sls_vf, _gf1_uls_vf, _gf3_uls_vf));
            list.Add(string.Format("Self Weight               γf1_sls_csw={0:f2}         γf3_sls_csw={1:f2}    γf1_uls_csw={2:f2}    γf3_uls_csw={3:f2}", _gf1_sls_csw, _gf3_sls_csw, _gf1_uls_csw, _gf3_uls_csw));
            list.Add(string.Format("Parapet Load              γf1_sls_pl ={0:f2}         γf3_sls_pl ={1:f2}    γf1_uls_pl ={2:f2}    γf3_uls_pl ={3:f2}", _gf1_sls_pl, _gf3_sls_pl, _gf1_uls_pl, _gf3_uls_pl));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Live load"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Footpath Live Load        γf1_sls_fpl={0:f2}         γf3_sls_fpl={1:f2}    γf1_uls_fpl={2:f2}    γf3_uls_fpl={3:f2}", _gf1_sls_fpll, _gf3_sls_fpll, _gf1_uls_fpll, _gf3_uls_fpll));
            list.Add(string.Format("Accidental Wheel Load     γf1_sls_awl={0:f2}         γf3_sls_awl={1:f2}    γf1_uls_awl={2:f2}    γf3_uls_awl={3:f2}", _gf1_sls_awl, _gf3_sls_awl, _gf1_uls_awl, _gf3_uls_awl));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format("STEP 2.2.1 : Dead Load"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1)     Dead  Load of Edge Beam                (BD 37/01  Cl. 5.1.1)"));
            list.Add(string.Format(""));

            double dleb = width_girderweb * unitwt_concrete;

            list.Add(string.Format("Dead Load of Edge Beam  = dleb "));
            list.Add(string.Format("                        = width_girderweb  x   unitwt_concrete"));
            list.Add(string.Format("                        = {0:f3}  x  {1} = {2:f3}  kN/m", width_girderweb, unitwt_concrete, dleb));
            list.Add(string.Format(""));

            double dleb_sls = dleb * _gf1_sls_dl * _gf3_sls_dl;

            list.Add(string.Format("At SLS  = dleb_sls = dleb x γf1_sls_dl  x  γf3_sls_dl"));
            list.Add(string.Format("                   = {0:f3}  x  {1:f2}  x  {2:f2} = {3:f3}  kN/m", dleb, _gf1_sls_dl, _gf3_sls_dl, dleb_sls));
            list.Add(string.Format(""));

            double dleb_uls = dleb * _gf1_uls_dl * _gf3_uls_dl;

            list.Add(string.Format("At ULS = dleb_uls = dleb x γf1_uls_dl  x  γf3_uls_dl"));
            list.Add(string.Format("                 = {0:f3}  x  {1:f2}  x  {2:f2} = {3:f3}  kN/m", dleb, _gf1_uls_dl, _gf3_uls_dl, dleb_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2) Dead Load of Verge Infill    (BS 648-1964, BD 37/01  Table 1)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Depth of the Infill = depth_infill = {0} mm.", _depth_infill));
            list.Add(string.Format(""));

            double nom_dlif = (_depth_infill / 1000) * unitwt_concrete;
            list.Add(string.Format("Nominal Infill Weight = nom_dlif "));
            list.Add(string.Format("                      = depth_infill   x   unitwt_concrete"));
            list.Add(string.Format("                      = {0:f3}  x  {1}  =  {2:f3}  kN/Sq.m", (_depth_infill / 1000), unitwt_concrete, nom_dlif));
            list.Add(string.Format(""));

            double dlvf_sls = nom_dlif * _gf1_sls_vf * _gf3_sls_vf;

            list.Add(string.Format("At SLS  =  dlvf_sls =  nom_dlif x γf1_sls_vf  x  γf3_sls_vf"));
            list.Add(string.Format("                    =  {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}  kN/Sq.m", nom_dlif, _gf1_sls_vf, _gf3_sls_vf, dlvf_sls));
            list.Add(string.Format(""));

            double dlvf_uls = nom_dlif * _gf1_uls_vf * _gf3_uls_vf;

            list.Add(string.Format("At ULS =  dlvf_uls  =  nom_dlif x γf1_uls_vf  x  γf3_uls_vf"));
            list.Add(string.Format("                    =  {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}  kN/Sq.m", nom_dlif, _gf1_uls_vf, _gf3_uls_vf, dlvf_uls));
            //list.Add(string.Format("                    =  6.000  x  1.75  x  1.1 = 11.550  kN/Sq.m"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("3)   Cantilever Self Weight:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double nom_csw = _C_Length * _C_Thick * unitwt_concrete;

            list.Add(string.Format("Nominal Cantilever self weight = nom_csw "));
            list.Add(string.Format("                               =  C_Length x C_Thick  x  unitwt_concrete"));
            list.Add(string.Format("                               =  {0:f3}  x {1:f3} x  {2} = {3:f3}  kN/m", _C_Length, _C_Thick, unitwt_concrete, nom_csw));
            list.Add(string.Format(""));

            double nom_csw_sls = nom_csw * _gf1_sls_csw * _gf3_sls_csw;

            list.Add(string.Format("At SLS  = nom_csw_sls =  nom_csw  x γf1_sls_csw x  γf3_sls_csw"));
            list.Add(string.Format("                      =  {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}  kN/Sq.m", nom_csw, _gf1_sls_csw, _gf3_sls_csw, nom_csw_sls));
            //list.Add(string.Format("                      =  12.096  x  1.0  x  1.0  =  12.096  kN/m"));
            list.Add(string.Format(""));
            double nom_csw_uls = nom_csw * _gf1_uls_csw * _gf3_uls_csw;
            list.Add(string.Format("At ULS =  nom_csw_uls  =  nom_csw  x γf1_uls_csw  x  γf3_uls_csw "));
            list.Add(string.Format("                      =  {0:f3}  x  {1:f2}  x  {2:f2}  =  {3:f3}  kN/Sq.m", nom_csw, _gf1_uls_csw, _gf3_uls_csw, nom_csw_uls));
            //list.Add(string.Format("                       = 12.096  x  1.20  x  1.1  =  15.966  kN/m"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("4)        Parapet Load:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("At SLS  =  pload_sls =  {0}  kN/m", _pload_sls));
            list.Add(string.Format("At ULS =  pload_uls =  {0}  kN/m", _pload_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("STEP 2.2.2 : Live Load"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2.2.2.1 : Footpath Live Load =  fpll = {0}  kN/Sq.m    (BD 37/01  Cl. 6.5.1)", _fpll));
            list.Add(string.Format("----------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Load on Beam  (Ref. BD 37/01 Table 1)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double fpll_uls = _fpll * _gf1_uls_fpll * _gf3_uls_fpll * _C_Length;

            list.Add(string.Format("At ULS = fpll_uls = fpll x γf1_uls_fpll x  γf3_uls_fpll x   C_Length  "));
            list.Add(string.Format("                  = {0}  x  {1}  x  {2} x   {3} =  {4:f3}  kN/Sq.m", _fpll, _gf1_uls_fpll, _gf3_uls_fpll, _C_Length, fpll_uls));
            list.Add(string.Format(""));

            double fpll_sls = _fpll * _gf1_sls_fpll * _gf3_sls_fpll * _C_Length;

            list.Add(string.Format("At   SLS  =  fpll_sls = fpll x γf1_sls_fpll x  γf3_sls_fpll x   C_Length  "));
            list.Add(string.Format("                  = {0}  x  {1}  x  {2} x   {3} =  {4:f3}  kN/Sq.m", _fpll, _gf1_sls_fpll, _gf3_sls_fpll, _C_Length, fpll_uls));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format("STEP 2.2.2.2 : Accidental Wheel Load"));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 1 from Dialog Box"));
            list.Add(string.Format(""));

            double ovl_thick = _fp_thick + _C_Thick;

            list.Add(string.Format("Overall Thickness = ovl_thick = fp_thick + C_Thick = {0} + {1} = {2} m = {3} mm", _fp_thick, _C_Thick, ovl_thick, ovl_thick *= 1000));



            list.Add(string.Format(""));
            double eff_thick = ovl_thick - _cover - _bar_dia / 2;
            list.Add(string.Format("Effective Thickness (Depth)= eff_thick = ovl_thick - cover - bar_dia/2 "));
            list.Add(string.Format("                                = {0} - {1} - {2} / 2 = {3} mm = {4} m", ovl_thick, _cover, _bar_dia, eff_thick, eff_thick /= 1000));
            list.Add(string.Format(""));
            list.Add(string.Format("One Side Load Dispersal Width = {0:f3} / 2   m", eff_thick));
            list.Add(string.Format(""));
            list.Add(string.Format("Both Side Load Dispersal Width = 2 x {0:f3} / 2 = {0:f3} m", eff_thick));
            list.Add(string.Format(""));

            double disp_width = Math.Sqrt(100 * axle_unit * axle_unit * axle_unit / 1.1);
            list.Add(string.Format(""));
            list.Add(string.Format("Dispersal Width = (100 x {0}^3 / 1.1)^(1/2)  = {1:f2} mm.  =  {2:f4}  m.   (BD 37/01  Cl. 6.6.3)", axle_unit, disp_width, (disp_width /= 1000)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            
            double tot_disp_width = disp_width + eff_thick;
            list.Add(string.Format("Total Dispersed width of Load at Effective Depth = {0:f4} + {1:f4} = {2:f4} m", disp_width, eff_thick, tot_disp_width));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Cantilever Slab Thickness = {0:f3} m", _C_Thick));

            double eff_thick2 = _C_Thick * 1000 - _cover - (_bar_dia / 2);
            list.Add(string.Format("Effective Thickness = {0} - {1}  - {2}/2 = {3} mm = {4} m", _C_Thick * 1000, _cover, _bar_dia, eff_thick2, eff_thick2 /= 1000));
            list.Add(string.Format(""));
            list.Add(string.Format("One Side Load Dispersal Width = ({0}/2) m", eff_thick2));
            list.Add(string.Format("Both Side Load Dispersal Width = 2 x ({0}/2) = {0} m", eff_thick2));
            list.Add(string.Format("Width of Load at Top of Cantilever Slab = = {0:f4} m", disp_width));
            list.Add(string.Format(""));

            double tot_disp_width2 = disp_width + eff_thick2;
            list.Add(string.Format("Total Dispersed width of Load at Effective Depth = {0:f4} + {1:f4} = {2:f4} m", disp_width, eff_thick2, tot_disp_width2));
        
            list.Add(string.Format(""));
            double awa = _ada;
            double awb = _adb;

            list.Add(string.Format("Hence, {0} kN wheel load will be dispersed on area of  = awa x awb = {1} x {2} Sq.m", _awl, _ada, _adb));
            list.Add(string.Format(""));

            //double dis_fact = 0.4640 / _ada;
            double dis_fact = tot_disp_width2 / _ada;
            list.Add(string.Format("Dispersal factor in Longitudinal Direction = dis_fact = {0:f4} / {1:f4} = {2:f4} ", tot_disp_width2, _ada, dis_fact));
            list.Add(string.Format(""));
            list.Add(string.Format("(Ref.  BD 37/01  Table 1)"));
            list.Add(string.Format(""));


            double awll_sls = (_awl / awa) * _gf1_sls_awl * _gf3_sls_awl * dis_fact;
            list.Add(string.Format("Maximum intensity of UDL over Slab for SLS  = awll_sls "));
            list.Add(string.Format(""));
            list.Add(string.Format("        = (awl / awa) x γf1_sls_awl x γf3_sls_awl x dis_fact"));
            list.Add(string.Format("        = ({0} / {1}) x {2} x {3} x {4:f4} = {5:f4}  kN/m", _awl, awa, _gf1_sls_awl, _gf3_sls_awl, dis_fact, awll_sls));
            list.Add(string.Format(""));

            list.Add(string.Format("Maximum intensity of UDL over Slab for ULS = awll_uls"));
            list.Add(string.Format(""));
            double awll_uls = (_awl / awa) * _gf1_uls_awl * _gf3_uls_awl * dis_fact;
            list.Add(string.Format("     = (awl / awa) x γf1_uls_awl x γf3_uls_awl x dis_fact"));
            list.Add(string.Format("     = ({0} / {1}) x {2} x {3} x {4:f4} = {5:f4}  kN/m", _awl, awa, _gf1_uls_awl, _gf3_uls_awl, dis_fact, awll_uls));
            //list.Add(string.Format("     = ({0} / 0.7315) x 1.3 x 1.1 x 0.6343 = 216.995  kN/m"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Cantilever deck is checked for the width equals to distance between accidental load axle = dal = {0} m", _dal));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1)      Dead  Load of Edge Beam"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 2 from Dialog Box"));
            list.Add(string.Format(""));

            double _w_parapet = 0.50;
            double L = _c_width - (_w_parapet / 2);

            list.Add(string.Format("L = c_width - (w_parapet / 2) = {0} - ({1:f3} / 2) = {2:f3} m", _c_width, _w_parapet, L));
            list.Add(string.Format(""));

            double P_uls = dleb_uls * _dal;
            list.Add(string.Format("Dead  Load  of Edge Beam at ULS = P_uls = dleb_uls x dal = {0:f3} x {1} = {2:f3} kN", dleb_uls, _dal, P_uls));
            list.Add(string.Format(""));

            double MA_uls1 = P_uls * L;
            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = P_uls x L = {0:f3} x {1:f3} = {2:f3} kN.m", P_uls, L, MA_uls1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double P_sls = dleb_sls * _dal;
            list.Add(string.Format("Dead  Load of Edge Beam at SLS = P_sls = dleb_sls x dal = {0:f3} x {1} = P = {2:f3} kN", dleb_sls, _dal, P_sls));
            list.Add(string.Format(""));
            double MA_sls1 = P_sls * L;
            list.Add(string.Format("Bending Moment at A at SLS = MA_sls  = P_sls x L = {0:f3} x {1:f4} = {2:f3} kN.m", P_sls, L, MA_sls1));
            list.Add(string.Format(""));

            double RA_ULS1 = P_uls;
            double RA_SLS1 = P_sls;
            list.Add(string.Format("Reaction at A at ULS = ULS_RA  = P_uls = {0:f3} kN", RA_ULS1));
            list.Add(string.Format("Reaction at A at SLS = SLS_RA  = P_sls = {0:f3} kN", RA_SLS1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2)    Dead Load of Verge Fill"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 3 from Dialog Box"));
            list.Add(string.Format(""));
            L = c_span;
            list.Add(string.Format("L = c_span = {0:f3} m", L));
            list.Add(string.Format(""));

            double W_uls = dlvf_uls * _dal;
            list.Add(string.Format("Dead  Load of Verge Fill at ULS = W_uls = dlvf_uls  x dal = {0:f3} x {1} = W = {2:f3} kN/m", dlvf_uls, _dal, W_uls));
            list.Add(string.Format(""));

            double MA_uls2 = W_uls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = W x L^2 / 2 = {0:f3} x {1}^2 / 2 = {2:f3} kN.m", W_uls, L, MA_uls2));
            list.Add(string.Format(""));

            double W_sls = dlvf_sls * _dal;
            list.Add(string.Format("Dead  Load of Verge Fill at SLS = W_sls = dlvf_sls  x dal = {0:f3} x {1} = W = {2:f3} kN/m", dlvf_sls, _dal, W_sls));
            list.Add(string.Format(""));

            double MA_sls2 = W_sls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at SLS = MA_sls  = W x L^2 / 2 = {0:f3} x {1}^2 / 2 = {2:f3} kN.m", W_sls, L, MA_sls2));
            list.Add(string.Format(""));

            double RA_ULS2 = W_uls * L;
            list.Add(string.Format("Reaction at A at ULS = RA_ULS  = W_uls x L = {0:f3} x {1} = {2:f3} kN", W_uls, L, RA_ULS2));
            list.Add(string.Format(""));

            double RA_SLS2 = W_sls * L;
            list.Add(string.Format("Reaction at A at SLS = RA_SLS  = W_sls x L = {0:f3} x {1} = {2:f3} kN", W_sls, L, RA_SLS2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("3)    Parapet Load"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 4 from Dialog Box"));
            list.Add(string.Format(""));
        
            list.Add(string.Format(""));

            L = _c_width - (_w_parapet / 2);
            list.Add(string.Format("L = c_width - (w_parapet / 2) = {0:f3} - ({1:f3} / 2) = {2:f3} m", _c_width, _w_parapet, L));
            list.Add(string.Format(""));

            double W_uls2 = _pload_uls * _dal;
            list.Add(string.Format("Dead  Load of Parapet at ULS = W_uls = pload_uls x dal = {0:f3} x {1} = W = {2:f3} kN", _pload_uls, _dal, W_uls2));
            list.Add(string.Format(""));

            double MA_uls3 = W_uls2 * L * L / 2.0;


            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = W_uls x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", W_uls2, L, MA_uls3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double W_sls2 = _pload_sls * _dal;
            list.Add(string.Format("Dead  Load of Parapet at SLS = W_sls = pload_sls x dal = {0:f3} x {1} = W = {2:f3} kN", _pload_sls, _dal, W_sls2));
            list.Add(string.Format(""));

            double MA_sls3 = W_sls2 * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at SLS = MA_sls  = W_sls x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", W_sls2, L, MA_sls3));
            list.Add(string.Format(""));

            double RA_ULS3 = W_uls2;
            double RA_SLS3 = W_sls2;
            list.Add(string.Format("Reaction at A at ULS = RA_ULS  = W_uls = {0:f3} kN", RA_ULS3));
            list.Add(string.Format("Reaction at A at SLS = RA_SLS  = W_sls =  {0:f3} kN", RA_SLS3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("4) Footpath Live Load"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 5 from Dialog Box"));
            list.Add(string.Format("")); 
            L = c_span;

            list.Add(string.Format("L = c_span = {0} m", L));
            list.Add(string.Format("W  (at ULS) = fpll_uls  = {0:f3} kN/m", fpll_uls));
            list.Add(string.Format("W  (at SLS) = fpll_sls  = {0:f3} kN/m", fpll_sls));
            list.Add(string.Format(""));

            double MA_uls4 = fpll_uls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = fpll_uls  x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", fpll_uls, L, MA_uls4));
            list.Add(string.Format(""));

            double MA_sls4 = fpll_sls * L * L / 2;
            list.Add(string.Format("Bending Moment at A at SLS =  MA_sls  = fpll_sls  x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", fpll_sls, L, MA_sls4));
            list.Add(string.Format(""));

            double RA_ULS4 = fpll_uls * L;
            list.Add(string.Format("Reaction at A at ULS = RA_ULS  = fpll_uls  x L = {0:f3} x {1:f3} = {2:f3} kN", fpll_uls, L, RA_ULS4));
            list.Add(string.Format(""));
            double RA_SLS4 = fpll_sls * L;
            list.Add(string.Format("Reaction at A at SLS  = RA_SLS  = fpll_sls  x L =  {0:f3} x {1:f3} = {2:f3} kN", fpll_sls, L, RA_SLS4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("5)     Accidental Live Load"));
            list.Add(string.Format("")); 
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 6 from Dialog Box"));
            list.Add(string.Format(""));
        

            L = _awll;
            list.Add(string.Format("L = Accident Wheel Load length = awll = {0:f3} m", L));
            list.Add(string.Format("W  (at ULS) = fpll_uls  = {0:f3} kN/m", fpll_uls));
            list.Add(string.Format("W  (at SLS) =  fpll_sls  = {0:f3} kN/m", fpll_sls));
            list.Add(string.Format(""));

            double MA_uls5 = awll_uls * L * L / 2.0;

            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = awll_uls  x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", awll_uls, L, MA_uls5));
            list.Add(string.Format(""));
            double MA_sls5 = awll_sls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at SLS =  MA_sls  = awll_sls  x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", awll_sls, L, MA_sls5));
            list.Add(string.Format(""));

            double RA_uls5 = awll_uls * L;
            list.Add(string.Format("Reaction at A at ULS = RA_ULS  = awll_uls  x L = {0:f3} x {1:f3} = {2:f3} kN", awll_uls, L, RA_uls5));
            list.Add(string.Format(""));

            double RA_sls5 = awll_sls * L;
            list.Add(string.Format("Reaction at A at SLS  = RA_SLS  = awll_sls  x L =  {0:f3} x {1:f3} = {2:f3} kN", awll_sls, L, RA_sls5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("6)  Collision with Parapet Load"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 7 from Dialog Box"));
            list.Add(string.Format(""));
        
            //double _M = 67.8;
            double _M = MA_uls5;

            //double MA_uls6 = _M * _gf1_uls_fpll * _gf3_uls_fpll + 103.69 * 0.475 * _gf1_uls_fpll * _gf1_uls_fpll;
            double MA_uls6 = _M * _gf1_uls_fpll * _gf3_uls_fpll + RA_uls5 * (_awll / 2) * _gf1_uls_fpll * _gf1_uls_fpll;

            list.Add(string.Format("As plastic moment is used no other factor will be used"));
            list.Add(string.Format(""));
            //list.Add(string.Format("MA_uls  = 67.80 x γf1_sls_fpll  x γf3_sls_fpll  +  103.69 x 0.475 x γf1_sls_fpll x γf1_sls_fpll "));
            list.Add(string.Format("MA_uls  = {0:f3} x γf1_uls_fpll  x γf3_uls_fpll  +  {1:f3} x {2:f3} x γf1_uls_fpll x γf1_uls_fpll ", _M, RA_uls5, (_awll / 2)));
            list.Add(string.Format("        = {0:f3} x {1:f2} x {2:f2}  +  {3:f3} x {4:f3} x {1:f2} x {2:f2} ", _M, _gf1_uls_fpll, _gf3_uls_fpll, RA_uls5, (_awll / 2)));

            list.Add(string.Format("        = {0:f3} kN-m", MA_uls6));
            list.Add(string.Format(""));

            double MA_sls6 = 0;
            double RA_uls6 = 0;
            double RA_sls6 = 0;

            list.Add(string.Format("MA_sls = 0.0"));
            list.Add(string.Format("RA_uls = RA_sls = 0.0"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("7)   Self Weight"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 8 from Dialog Box"));
            list.Add(string.Format(""));
        
            list.Add(string.Format(""));
            L = _awll;
            list.Add(string.Format("L = Accident Wheel Load length = awll = {0} m", L));
            list.Add(string.Format("W  (at ULS) = fpll_uls  = {0:f3} kN/m", fpll_uls));
            list.Add(string.Format("W  (at SLS) =  fpll_sls  = {0:f3} kN/m", fpll_sls));
            list.Add(string.Format(""));

            double MA_uls7 = awll_uls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at ULS = MA_uls  = awll_uls x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", awll_uls, L, MA_uls7));
            list.Add(string.Format(""));
            double MA_sls7 = awll_sls * L * L / 2.0;
            list.Add(string.Format("Bending Moment at A at SLS =  MA_sls  = awll_sls x L^2 / 2 = {0:f3} x {1:f3}^2 / 2 = {2:f3} kN.m", awll_sls, L, MA_sls7));
            list.Add(string.Format(""));

            double RA_uls7 = awll_uls * L;
            list.Add(string.Format("Reaction at A at ULS = RA_ULS  = awll_uls  x L = {0:f3} x {1:f3} = {2:f3} kN", awll_uls, L, RA_uls7));
            list.Add(string.Format(""));
            double RA_sls7 = awll_sls * L;
            list.Add(string.Format("Reaction at A at SLS  = RA_SLS  = awll_sls  x L = {0:f3} x {1:f3} = {2:f3} kN", awll_uls, L, RA_sls7));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------------------"));
            list.Add(string.Format("DESIGN MOMENTS and SHEARS"));
            list.Add(string.Format("---------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("MDL + LL at ULS = M_uls = ULS Load Combination (1 + 2 + 3 + 4 + 5 + 6 + 7)"));
            list.Add(string.Format(""));

            double M_uls = MA_uls1 + MA_uls2 + MA_uls3 + MA_uls4 + MA_uls5 + MA_uls6 + MA_uls7;
            list.Add(string.Format("                = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                MA_uls1, MA_uls2, MA_uls3, MA_uls4, MA_uls5, MA_uls6, MA_uls7));

            list.Add(string.Format("                = {0:f3} kN.m", M_uls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("MDL + LL at SLS = M_sls = SLS Load Combination (1 + 2 + 3 + 4 + 5 + 6 + 7)"));
            list.Add(string.Format(""));
            double M_sls = MA_sls1 + MA_sls2 + MA_sls3 + MA_sls4 + MA_sls5 + MA_sls6 + MA_sls7;

            list.Add(string.Format("                = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                MA_sls1, MA_sls2, MA_sls3, MA_sls4, MA_sls5, MA_sls6, MA_sls7));

            list.Add(string.Format("                = {0:f3} kN.m", M_sls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double V_uls = RA_ULS1 + RA_ULS2 + RA_ULS3 + RA_ULS4 + RA_uls5 + RA_uls6 + RA_uls7;
            list.Add(string.Format("VDL + LL   at ULS = V_uls = ULS Load Combination (1 + 2 + 3 + 4 + 5 + 6 + 7)"));
            list.Add(string.Format(""));

            list.Add(string.Format("                  = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                RA_ULS1, RA_ULS2, RA_ULS3, RA_ULS4, RA_uls5, RA_uls6, RA_uls7));

            list.Add(string.Format("                = {0:f3} kN", V_uls));
            list.Add(string.Format(""));

            double V_sls = RA_SLS1 + RA_SLS2 + RA_SLS3 + RA_SLS4 + RA_sls5 + RA_sls6 + RA_sls7;

            list.Add(string.Format("VDL + LL   at SLS = V_sls = SLS Load Combination (1 + 2 + 3 + 4 + 5 + 6 + 7)"));
            list.Add(string.Format(""));
            list.Add(string.Format("                  = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                 RA_SLS1, RA_SLS2, RA_SLS3, RA_SLS4, RA_sls5, RA_sls6, RA_sls7));

            list.Add(string.Format("                = {0:f3} kN", V_sls));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            txt_cant_TBR_M.Text = Math.Max(M_uls, M_sls).ToString("f3");
            //txt_cant_FC_Mg.Text = Math.Max(M_uls, M_sls).ToString("f3");



            txt_cant_FC_Mg.Text = Math.Max(Math.Max(MA_sls1, MA_sls2), MA_sls3).ToString("f3");


            txt_cant_FC_Mq.Text = Math.Max(Math.Max(MA_sls4, MA_sls5), MA_sls7).ToString("f3");


            txt_cant_SC_Vmax.Text = Math.Max(V_sls, V_uls).ToString("f3");

            txt_cant_LBR_M.Text = Math.Max(Math.Max(MA_uls1, MA_uls2), Math.Max(MA_uls3, MA_uls4)).ToString("f3");


            list.Add(string.Format(""));
            rtb_cant_DC_results.Lines = list.ToArray();
        }

        public void Cantilever_Transverse_Bending_Resistance()
        {

            List<string> list = new List<string>();

            //d_total /= 1000;
            //thickness_surfacing /= 1000;

            //Design_of_Deck_Slab(list, out dle_mhog_sls, out dle_mhog_uls, out LL_mhog_sls, out LL_mhog_uls, out VDL_LL_uls, out VDL_sls, out VLL_sls);


            #region TBR
            double _b = MyList.StringToDouble(txt_cant_TBR_b.Text, 1000);
            double _Cover = MyList.StringToDouble(txt_cant_TBR_cover.Text, 0.0);
            double _h = MyList.StringToDouble(txt_cant_TBR_h.Text, 0.0);
            double _bar_dia = MyList.StringToDouble(txt_cant_TBR_bar_dia.Text, 0.0);
            double _bar_spacing = MyList.StringToDouble(txt_cant_TBR_bar_spacing.Text, 0.0);
            double _bar_nos = MyList.StringToDouble(txt_cant_TBR_bar_nos.Text, 0.0);
            double _d = MyList.StringToDouble(txt_cant_TBR_d.Text, 0.0);
            double _fcu = MyList.StringToDouble(txt_cant_TBR_fcu.Text, 0.0);
            double _fy = MyList.StringToDouble(txt_cant_TBR_fy.Text, 0.0);
            double _M = MyList.StringToDouble(txt_cant_TBR_M.Text, 0.0);
            #endregion TBR

            list.Clear();



            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.3 : Check for Bending Resistance in Transverse Direction"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("   (i) Check for Moment of Resistance in Transverse Direction"));
            list.Add(string.Format(""));
            list.Add(string.Format("b = {0} mm ", _b));
            list.Add(string.Format("Cover = {0} mm", _Cover));
            list.Add(string.Format("h = {0} mm ", _h));
            list.Add(string.Format("Reinforcement Bar Diameters = bar_dia = {0} mm.", _bar_dia));
            list.Add(string.Format("Spacing = bar_spacing = {0} mm", _bar_spacing));

            _bar_nos = (int)(_b / _bar_spacing);
            _bar_nos += 1;
            list.Add(string.Format("Bar Nos. = bar_nos = 1000/ Spacing = {0}/{1}  = {2:f3} = {3} nos", _b, _bar_spacing, (_b / _bar_spacing), _bar_nos));

            _d = _h - _Cover - (_bar_dia / 2.0);
            list.Add(string.Format("d = h – cover – (Bar_dia/2)  = {0} – {1}  – {2:f3} = {3:f3} mm", _h, _Cover, _bar_dia / 2, _d));


            list.Add(string.Format("fcu = {0}  N/Sq.mm", _fcu));
            list.Add(string.Format("fy = {0}  N/Sq.mm", _fy));
            list.Add(string.Format(""));
            list.Add(string.Format(""));





            double des_BM = _M;
            double des_V = Math.Max(Math.Max(VDL_LL_uls, VDL_sls), VLL_sls);

            list.Add(string.Format("Maximum Design Bending Moment = {0:f3}  kNm ", des_BM));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide Reinforcement = {0} Nos.  {1} mm dia. bars", _bar_nos, _bar_dia));
            list.Add(string.Format(""));

            double req_Ast = des_V * 1000 / (2.0 * 0.87 * _fy); // ?????



            list.Add(string.Format("Reqd. Area of steel for shear  = {0:f3} Sq.mm ", req_Ast));
            list.Add(string.Format(""));

            double _As = _bar_nos * (Math.PI * _bar_dia * _bar_dia) / 4; // ?????
            list.Add(string.Format("Area of steel provided,     As   = bar_no x (π x bar_dia x bar_dia) / 4"));
            list.Add(string.Format("                                 = {0} x ({1:f3} x {2} x {2}) / 4", _bar_nos, Math.PI, _bar_dia));
            list.Add(string.Format("                                 = {0:f3} Sq.mm", _As));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double cant_z = (1 - (1.1 * _fy * _As) / (_fcu * _b * _d)) * _d;
            list.Add(string.Format("z =   [1 -   (1.1 x fy x As)  /  (fcu x b x d) ] x d"));
            list.Add(string.Format("  =   [1 -   (1.1 x {0} x {1:f3})  /  ({2} x 1000 x {3}) ] x {3}", _fy, _As, _fcu, _d));
            list.Add(string.Format(""));

            if (cant_z < _d * 0.95)
                list.Add(string.Format("  =   {0:f3}  m. <  0.95 x d = 0.95 x {1} = {2:f3} m. OK", cant_z, _d, _d * 0.95));
            else
                list.Add(string.Format("  =   {0:f3}  m. >  0.95 x d = 0.95 x {1} = {2:f3} m. NOT OK", cant_z, _d, _d * 0.95));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Mu = 0.87 * _fy * _As * cant_z / 1000000;

            list.Add(string.Format("Moment of Resistance,  Mu = 0.87 x fy x As x z "));
            list.Add(string.Format("                          = 0.87 x {0} x {1:f3} x {2:f3}/10^6", _fy, _As, cant_z));
            list.Add(string.Format(""));
            if (Mu > des_BM)
                list.Add(string.Format("                          = {0:f3} kNm  >  {1:f3}  kNm.  Section OK", Mu, des_BM));
            else
                list.Add(string.Format("                          = {0:f3} kNm  <=  {1:f3}  kNm.  Section NOT OK", Mu, des_BM));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            txt_cant_TBR_results.Lines = list.ToArray();
        }

        private void Cantilever_Flexural_Cracking()
        {
            List<string> list = new List<string>();


            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("STEP 2.4 : Check Flexural Cracking"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _b = MyList.StringToDouble(txt_cant_FC_b.Text, 0.0);
            double _bar_dia = MyList.StringToDouble(txt_cant_FC_bar_dia.Text, 0.0);
            double _spacing = MyList.StringToDouble(txt_cant_FC_spacing.Text, 0.0);


            double _bar_nos = (int)(_b / _spacing);

            double _h = MyList.StringToDouble(txt_cant_FC_h.Text, 0.0);
            double _Cnom = MyList.StringToDouble(txt_cant_FC_Cnom.Text, 0.0);
            double _Cmin = MyList.StringToDouble(txt_cant_FC_Cmin.Text, 0.0);

            double _d = _h - _Cmin - _bar_dia / 2;



            list.Add(string.Format("Breadth of section = b = {0} mm", _b));
            list.Add(string.Format("Depth of section = h = {0} mm", _h));



            list.Add(string.Format("Reinforcement Cover (Nominal) = Cnom = {0} mm", _Cnom));
            list.Add(string.Format("Reinforcement Cover (Minimum) = Cmin = {0} mm", _Cmin));
            list.Add(string.Format("Reinforcement bars = bar_dia = {0}  mm", bar_dia));
            list.Add(string.Format("Spacing = spacing = {0} mm", bar_spacing));
            list.Add(string.Format("No of bars = bar_nos = b / spacing = {0} / {1} = {2:f3} = {3} Nos", _b, bar_spacing, (_b / bar_spacing), bar_no));
            list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4"));
            list.Add(string.Format(""));

            double _As = _bar_nos * Math.PI * _bar_dia * _bar_dia / 4;

            list.Add(string.Format("                   = {0} x (3.1416 x {1} x {1})/4 = {2:f3} Sq.mm", _bar_nos, _bar_dia, _As));


            list.Add(string.Format("Effective depth, d = h - Cnom - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", _h, _Cnom, bar_dia, _d));
            list.Add(string.Format(""));


            double _a_dash = MyList.StringToDouble(txt_cant_FC_a_dash.Text, 0.0);
            double _M = MyList.StringToDouble(txt_FC_M.Text, 0.0);



            list.Add(string.Format("Distance from compression face to point at which crack is calculated, a', = {0} mm", _a_dash));
            list.Add(string.Format("Service Moment = M = {0} kNm", _M));

            double _Ec = MyList.StringToDouble(txt_cant_FC_Ec.Text, 0.0);
            list.Add(string.Format("Instantaneous modulus of elasticity = Ec = {0} kN/Sq.mm", _Ec));

            double _Es = MyList.StringToDouble(txt_cant_FC_Es.Text, 0.0);
            list.Add(string.Format("Modulus of Elasticity of Steel = Es = {0} kN/Sq.mm", _Es));

            double _flx_crk = MyList.StringToDouble(txt_cant_FC_flx_crk.Text, 0.0);
            list.Add(string.Format("Flexural Crack width aimed for = {0} mm", _flx_crk));

            double _a_cr = MyList.StringToDouble(txt_cant_FC_acr.Text, 0.0);
            list.Add(string.Format("Distance to surface of nearest rebar, a_cr = {0} mm", _a_cr));

            double ae = _Es * 2 / _Ec;

            list.Add(string.Format("Modular Ratio, = αe = Es x 2 / Ec = {0} x 2 / {1} = {2:f3} = (Ec long term = Ec /2)", _Es, _Ec, ae));

            //double Mq = MLL_sls_Hog;
            //double Mg = MDL_sls_Hog;



            double Mq = MyList.StringToDouble(txt_cant_FC_Mq.Text, 0.0);
            double Mg = MyList.StringToDouble(txt_cant_FC_Mg.Text, 0.0);



            list.Add(string.Format("Bending Moment for Live load = {0:f3} kN-m", Mq));

            list.Add(string.Format("Bending Moment for Dead Load = {0:f3} kN-m", Mg));
            list.Add(string.Format(""));


            //double val1 = -((ae * Ast) + Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * _d))) / _b;
            //double val2 = -((ae * Ast) - Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * _b * ae * Ast * _d))) / _b;

            double val1 = -((ae * _As) + Math.Sqrt(((ae * _As) * (ae * _As)) + (2 * _b * ae * _As * _d))) / _b;
            double val2 = -((ae * _As) - Math.Sqrt(((ae * _As) * (ae * _As)) + (2 * _b * ae * _As * _d))) / _b;



            //double val1 = - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b



            //double dc  =  - ((ae * Ast) +/- Math.Sqrt(((ae * Ast) * (ae * Ast)) + (z * b * ae * Ast * d))) / b;
            double dc = Math.Max(val1, val2);

            list.Add(string.Format("Depth of neutral axis, = dc  = - [(αe x As) ± √(((αe x As) x (αe x As)) + (2 x b x αe x As x d))] / b"));
            //list.Add(string.Format("                             = - [({0:f3} x {1:f3}) ± √(({0:f3} x {1:f3}) x ({0:f3} x {1:f3})) + ({2:f3} x {3:f3} x {0:f3} x {1:f3} x {4:f3}))] / {3:f3}", ae, Ast, z, _b, d));

            list.Add(string.Format("                             = - [({0:f3} x {1:f3}) ± √(({0:f3} x {1:f3}) x ({0:f3} x {1:f3}))", ae, _As));
            list.Add(string.Format("                               + ({0:f3} x {1:f3} x {2:f3} x {3:f3} x {4:f3}))] / {1:f3}", 2, _b, ae, _As, d));

            list.Add(string.Format("                             = {0:f3} mm.", dc));
            list.Add(string.Format(""));

            double fs = Math.Abs(_M / (_As * (d - (dc / 3))) * 1000 * 1000);


            list.Add(string.Format("Reinforcement tensile stress = fs = M x 1000 x 1000 / (As x (d - (dc/3)))"));
            list.Add(string.Format("                                  = {0:f3} x 1000 x 1000 / ({1:f3} x ({2} - ({3:f3}/3)))", _M, _As, d, dc));
            list.Add(string.Format("                                  = {0:f5}  N / Sq. mm", fs));
            list.Add(string.Format(""));

            double e1 = ((_a_dash - dc) * fs) / ((d - dc) * _Es);
            list.Add(string.Format("Flexural Strain = ((a’ - dc) x fs) / ((d - dc) x Es)"));
            list.Add(string.Format("                = (({0:f3} - {1:f3}) x {2:f3}) / (({3} - {1:f3}) x {4:f3})", _a_dash, dc, fs, d, _Es));
            list.Add(string.Format("                =  {0:f4}", e1));
            list.Add(string.Format(""));

            if (e1 > 0.001) e1 = 0.001;
            list.Add(string.Format("Thus total strain = e1 = {0:f4}", e1));
            list.Add(string.Format(""));

            double xi_s = iApp.Tables.Depth_Factor_BS5400_Table_9(d);
            list.Add(string.Format("ξs = Depth Factor =  {0:f4}    (BS 5400,table 9)", xi_s));
            list.Add(string.Format(""));

            //double em = e1 - ((3.8 * _b * _h * (_a_dash - dc)) / (_Es * Ast * (_h - dc))) * ((1 - (Mq / Mg)) / 1000000000.0);
            double em = e1 - ((3.8 * _b * _h * (_a_dash - dc)) / (xi_s * _As * (_h - dc))) * ((1 - (Mg / Mq)) / 1000000000.0);

            list.Add(string.Format("em   = e1 - [3.8 x bt x h x (a’ - dc) ] / [ξs x As x (h-dc)] x [1 - (Mq / Mg)] / 1,000,000,000 "));
            list.Add(string.Format("                 (but not greater than e1)"));
            list.Add(string.Format(""));
            list.Add(string.Format("     = {0:E4}", em));
            list.Add(string.Format(""));

            double Wmax = (3 * _a_cr * em) / (1 + 2 * (_a_cr - _Cmin) / (_h - dc));

            list.Add(string.Format("Crack width = Wmax = (3 x acr x em) / [1 + 2 x (acr - cmin) / (h - dc)]"));
            list.Add(string.Format("                   = (3 x {0:f3} x {1:E3}) / [1 + 2 x ({0:f3} - {2:f3}) / ({3} - {4:f3})]", _a_cr, em, _Cmin, _h, dc));
            if (Wmax < 0.25)
                list.Add(string.Format("                   = {0:f3}  mm < 0.25 mm.   OK", Wmax));
            else
                list.Add(string.Format("                   = {0:f3}  mm >= 0.25 mm.   NOT OK", Wmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            txt_cant_FC_results.Lines = list.ToArray();
        }

        private void Cantilever_Shear_Calculation()
        {

            List<string> list = new List<string>();
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format("STEP 2.5 : Shear Calculation"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Check for Shear Reinforcement"));
            list.Add(string.Format(""));

            //double Vmax = VDL_LL_uls;
            double _Vmax = MyList.StringToDouble(txt_cant_SC_Vmax.Text, 0.0);

            list.Add(string.Format("Max. Shear Force, Vmax.= {0:f3} kN", _Vmax));
            list.Add(string.Format(""));
            double _b = MyList.StringToDouble(txt_cant_SC_b.Text, 0.0);
            double _bar_dia = MyList.StringToDouble(txt_cant_SC_bar_dia.Text, 0.0);
            double _spacing = MyList.StringToDouble(txt_cant_SC_spacing.Text, 0.0);
            double _bar_nos = (int)(_b / _spacing);

            double _As = _bar_nos * Math.PI * _bar_dia * _bar_dia / 4;

            list.Add(string.Format("b = {0} mm ", _b));
            list.Add(string.Format("Reinforcement Bar Dia  = bar_dia = {0}", bar_dia));
            list.Add(string.Format("Bar Nos = bar_nos = {0}", bar_no));
            list.Add(string.Format("Area of Steel = As = bar_nos x π x bar_dia x bar_dia / 4 "));
            list.Add(string.Format("                   = {0} x 3.1416 x {1} x {1} / 4 ", _bar_nos, _bar_dia));
            list.Add(string.Format("                   = {0:f3} Sq.mm ", _As));
            list.Add(string.Format(""));

            double _cover = MyList.StringToDouble(txt_cant_SC_cover.Text, 0.0);
            double _h = MyList.StringToDouble(txt_cant_SC_h.Text, 0.0);
            double _d = _h - _cover - _bar_dia / 2;

            list.Add(string.Format("Reinforcement Cover = cover = {0} mm", _cover));
            list.Add(string.Format("Overall Slab Thickness = h = {0} mm", _h));
            list.Add(string.Format("Effective Depth = d = h - cover - bar_dia/2 = {0} - {1} - {2}/2 = {3} mm", _h, _cover, _bar_dia, _d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _fcu = MyList.StringToDouble(txt_cant_SC_fcu.Text, 0.0);
            double _fcu_allow = MyList.StringToDouble(txt_cant_SC_fcu_allow.Text, 0.0);
            double _fyv = MyList.StringToDouble(txt_cant_SC_fyv.Text, 0.0);

            list.Add(string.Format("fcu = {0} N / Sq.mm", _fcu));
            list.Add(string.Format("allowable  fcu {0} = N / Sq.mm (for shear only)", _fcu_allow));
            list.Add(string.Format("fyv= {0} N / Sq.mm", _fyv));
            list.Add(string.Format(""));


            //double xi_s = 1.319;
            double xi_s = iApp.Tables.Depth_Factor_BS5400_Table_9(_d);
            list.Add(string.Format("ξs = Depth Factor =  {0:f4}    (BS 5400,table 9)", xi_s));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide Reinforcement = {0} Nos. {1} ɸ bars", _bar_nos, _bar_dia));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of steel provided, As = {0:f3} Sq.mm", _As));
            list.Add(string.Format(""));

            double p = 100 * _As / (_b * _d);
            list.Add(string.Format("Percentage = 100 x As / (b x d) = 100 x {0:f3} / (1000 x {1}) = {2:f3} %", _As, _b, _d));
            list.Add(string.Format(""));

            //double vc = 0.85;

            CONCRETE_GRADE cgrd = (CONCRETE_GRADE)(int)Fck;

            string ref_str = "";
            double vc = iApp.Tables.Permissible_Shear_Stress(p, cgrd, ref ref_str);
            list.Add(string.Format("vc = Ultimate shear stress in concrete = {0:f3} N / Sq.mm ( BS 5400,table 8)", vc));
            list.Add(string.Format(""));

            double v = _Vmax / (_b * _d);

            //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
            if (v < (xi_s * vc))
                list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:E3} N / Sq.mm   <   ξs x vc = {1:f3} N / Sq.mm     Hence, OK.", v, (xi_s * vc)));
            else
                list.Add(string.Format("Shear Stress, v = V / (b x d) = {0:E3} N / Sq.mm   >   ξs x vc = {1:f3} N / Sq.mm     Hence, NOT OK.", v, (xi_s * vc)));

            //list.Add(string.Format("Shear Stress, v = V / (b x d) = 0.74 N / Sq.mm   <   ξs x vc = 1.11 N / Sq.mm     Hence, OK."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal steel for shear fyv = {0} N/Sq.mm", _fyv));
            list.Add(string.Format(""));

            list.Add(string.Format("V = Vmax.= {0:f3} kN", _Vmax));

            double Asa = _Vmax * 1000 / (2.0 * 0.87 * _fyv);

            list.Add(string.Format("Asa  >=  V x 1000 / (2 x 0.87 x fyv)", Asa));
            list.Add(string.Format("      =  {0:f3} x 1000 / (2 x 0.87 x {1}) = {2:f3}  Sq.mm", _Vmax, _fyv, Asa));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            txt_cant_SC_results.Lines = list.ToArray();

        }

        public void Cantilever_Longitudinal_Bending_Resistance()
        {

            List<string> list = new List<string>();

            //d_total /= 1000;
            //thickness_surfacing /= 1000;

            //Design_of_Deck_Slab(list, out dle_mhog_sls, out dle_mhog_uls, out LL_mhog_sls, out LL_mhog_uls, out VDL_LL_uls, out VDL_sls, out VLL_sls);


            #region LBR
            double _b = MyList.StringToDouble(txt_cant_LBR_b.Text, 1000);
            double _Cover = MyList.StringToDouble(txt_cant_LBR_cover.Text, 0.0);
            double _h = MyList.StringToDouble(txt_cant_LBR_h.Text, 0.0);
            double _bar_dia = MyList.StringToDouble(txt_cant_LBR_bar_dia.Text, 0.0);
            double _bar_spacing = MyList.StringToDouble(txt_cant_LBR_spacing.Text, 0.0);
            double _bar_nos = MyList.StringToDouble(txt_cant_LBR_bar_nos.Text, 0.0);
            double _d = MyList.StringToDouble(txt_cant_LBR_d.Text, 0.0);
            double _fcu = MyList.StringToDouble(txt_cant_LBR_fcu.Text, 0.0);
            double _fy = MyList.StringToDouble(txt_cant_LBR_fy.Text, 0.0);
            double _M = MyList.StringToDouble(txt_cant_LBR_M.Text, 0.0);
            #endregion LBR

            list.Clear();



            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.6 : Check for Moment of Resistance for longitudinal bending"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("   (i) Check for Moment of Resistance in Transverse Direction"));
            list.Add(string.Format(""));
            list.Add(string.Format("b = {0} mm ", _b));
            list.Add(string.Format("Cover = {0} mm", _Cover));
            list.Add(string.Format("h = {0} mm ", _h));
            list.Add(string.Format("Reinforcement Bar Diameters = bar_dia = {0} mm.", _bar_dia));
            list.Add(string.Format("Spacing = bar_spacing = {0} mm", _bar_spacing));

            _bar_nos = (int)(_b / _bar_spacing);
            _bar_nos += 1;
            list.Add(string.Format("Bar Nos. = bar_nos = 1000/ Spacing = {0}/{1}  = {2:f3} = {3} nos", _b, _bar_spacing, (_b / _bar_spacing), _bar_nos));

            _d = _h - _Cover - (_bar_dia / 2.0);
            list.Add(string.Format("d = h – cover – (Bar_dia/2)  = {0} – {1}  – {2:f3} = {3:f3} mm", _h, _Cover, _bar_dia / 2, _d));


            list.Add(string.Format("fcu = {0}  N/Sq.mm", _fcu));
            list.Add(string.Format("fy = {0}  N/Sq.mm", _fy));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Refer to Figure 9 from Dialog Box"));
            list.Add(string.Format(""));
        
            list.Add(string.Format(""));


            list.Add(string.Format("Considering  the four side simply supported "));
            list.Add(string.Format("case with the b=2a "));
            list.Add(string.Format(""));
            list.Add(string.Format("Dispersion width per wheel = 0.591 m "));
            list.Add(string.Format("u = v = 0.591 m"));
            list.Add(string.Format("a = 0.846 m"));
            list.Add(string.Format("b = 1.692 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("β1 = (Refer table-22 from theory of Plates & Sheels, Timoshenko)"));
            list.Add(string.Format(""));
            list.Add(string.Format("u/a = 0.699  "));
            list.Add(string.Format("v/a = 0.699"));
            list.Add(string.Format(""));

            double des_BM = _M;
            double des_V = Math.Max(Math.Max(VDL_LL_uls, VDL_sls), VLL_sls);

            list.Add(string.Format("Maximum Design Bending Moment = {0:f3}  kNm ", des_BM));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide Reinforcement = {0} Nos.  {1} mm dia. bars", _bar_nos, _bar_dia));
            list.Add(string.Format(""));

            double req_Ast = des_V * 1000 / (2.0 * 0.87 * _fy); // ?????



            list.Add(string.Format("Reqd. Area of steel for shear  = {0:f3} Sq.mm ", req_Ast));
            list.Add(string.Format(""));

            double _As = _bar_nos * (Math.PI * _bar_dia * _bar_dia) / 4; // ?????
            list.Add(string.Format("Area of steel provided,     As   = bar_no x (π x bar_dia x bar_dia) / 4"));
            list.Add(string.Format("                                 = {0} x ({1:f3} x {2} x {2}) / 4", _bar_nos, Math.PI, _bar_dia));
            list.Add(string.Format("                                 = {0:f3} Sq.mm", _As));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double cant_z = (1 - (1.1 * _fy * _As) / (_fcu * _b * _d)) * _d;
            list.Add(string.Format("z =   [1 -   (1.1 x fy x As)  /  (fcu x b x d) ] x d"));
            list.Add(string.Format("  =   [1 -   (1.1 x {0} x {1:f3})  /  ({2} x 1000 x {3}) ] x {3}", _fy, _As, _fcu, _d));
            list.Add(string.Format(""));

            if (cant_z < _d * 0.95)
                list.Add(string.Format("  =   {0:f3}  m. <  0.95 x d = 0.95 x {1} = {2:f3} m. OK", cant_z, _d, _d * 0.95));
            else
                list.Add(string.Format("  =   {0:f3}  m. >  0.95 x d = 0.95 x {1} = {2:f3} m. NOT OK", cant_z, _d, _d * 0.95));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Mu = 0.87 * _fy * _As * cant_z / 1000000;

            list.Add(string.Format("Moment of Resistance,  Mu = 0.87 x fy x As x z "));
            list.Add(string.Format("                          = 0.87 x {0} x {1:f3} x {2:f3}/10^6", _fy, _As, cant_z));
            list.Add(string.Format(""));
            if (Mu > des_BM)
                list.Add(string.Format("                          = {0:f3} kNm  >  {1:f3}  kNm.  Section OK", Mu, des_BM));
            else
                list.Add(string.Format("                          = {0:f3} kNm  <=  {1:f3}  kNm.  Section NOT OK", Mu, des_BM));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            txt_cant_LBR_results.Lines = list.ToArray();
        }
        #endregion Design of Cantiver Slab

        public void Save_All_Data()
        {
            //Save_FormRecord
        }

        private void dgv_cant_DC_sls_uls_factor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int row_index = 0;

            try
            {

                //Edge Beam
                txt_cant_DC_gf1_sls_dl.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_dl.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_dl.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_dl.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();



                row_index++;
                //Verge Fill
                txt_cant_DC_gf1_sls_vf.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_vf.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_vf.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_vf.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();



                row_index++;
                //Self Weight
                txt_cant_DC_gf1_sls_csw.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_csw.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_csw.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_csw.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();



                row_index++;
                //Parapet Load
                txt_cant_DC_gf1_sls_pl.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_pl.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_pl.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_pl.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();


                row_index++;
                //Footpath Live Load
                txt_cant_DC_gf1_sls_fpll.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_fpll.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_fpll.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_fpll.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();


                row_index++;
                //Accidental Wheel Load
                txt_cant_DC_gf1_sls_awl.Text = dgv_cant_DC_sls_uls_factor[1, row_index].Value.ToString();
                txt_cant_DC_gf3_sls_awl.Text = dgv_cant_DC_sls_uls_factor[2, row_index].Value.ToString();


                txt_cant_DC_gf1_uls_awl.Text = dgv_cant_DC_sls_uls_factor[4, row_index].Value.ToString();
                txt_cant_DC_gf3_uls_awl.Text = dgv_cant_DC_sls_uls_factor[5, row_index].Value.ToString();


            }
            catch (Exception ex) { }

        }

     


    }
}
