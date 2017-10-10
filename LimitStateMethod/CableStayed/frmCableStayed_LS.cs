using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraInterface.TrussBridge;
using AstraFunctionOne.BridgeDesign;

using BridgeAnalysisDesign;
using AstraAccess.SAP_Classes;

using BridgeAnalysisDesign.CableStayed;
using LimitStateMethod.SuspensionBridge;


namespace LimitStateMethod.CableStayed
{
    public partial class frmCableStayed_LS : Form
    {
        IApplication iApp = null;

        CABLE_STAYED_LS_Analysis CS_Analysis { get; set; }
        eDesignStandard DesignStandard
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0) return eDesignStandard.BritishStandard;

                return eDesignStandard.IndianStandard;
            }
        }

        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelBeams;
                      
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelBeams;
                   
                }
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelChannels;
                        break;
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelChannels;
                        break;
                }
                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                switch (DesignStandard)
                {
                    case eDesignStandard.IndianStandard:
                        return iApp.Tables.IS_SteelAngles;
                        break;
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelAngles;
                        break;
                }
                return iApp.Tables.IS_SteelAngles;
            }
        }

        List<MemberSectionProperty> SectionProperty { get; set; }
        List<LoadData> LoadList = new List<LoadData>();



        CableStayedDeckSlab Deck = null;



        List<string> list_member_group = new List<string>();
        //string Title = "ANALYSIS OF CABLE STAYED BRIDGE";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "CABLE STAYED BRIDGE [BS]";
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return "CABLE STAYED BRIDGE [LRFD]";
                return "CABLE STAYED BRIDGE [IRC]";
            }
        }



        BridgeMemberAnalysis Bridge_Analysis = null;
        bool IsCreateData = true;
        public int img_counter = 0;

        public List<string> Results { get; set; }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(user_path, "LL.TXT");
            }
        }
        List<LoadData> Live_Load_List { get; set; }
        public string CSB_DATA_File
        {
            get
            {
                return Path.Combine(user_path, "CSB_DATA.TXT");
            }
        }

        #region Properties
        public double L1
        {
            get { return MyList.StringToDouble(txt_L1.Text, 0.0); }
            set { txt_L1.Text = value.ToString(); }
        }
        public double L2
        {
            get { return MyList.StringToDouble(txt_L2.Text, 0.0); }
            set { txt_L2.Text = value.ToString(); }
        }
        public double L3
        {
            get { return MyList.StringToDouble(txt_L3.Text, 0.0); }
            set { txt_L3.Text = value.ToString(); }
        }

        public double B
        {
            get { return MyList.StringToDouble(txt_B.Text, 0.0); }
            set { txt_B.Text = value.ToString(); }
        }
        public double a1
        {
            get { return MyList.StringToDouble(txt_a1.Text, 0.0); }
            set { txt_a1.Text = value.ToString(); }
        }
        public double a2
        {
            get { return MyList.StringToDouble(txt_a2.Text, 0.0); }
            set { txt_a2.Text = value.ToString(); }
        }
        public double a3
        {
            get { return MyList.StringToDouble(txt_a3.Text, 0.0); }
            set { txt_a3.Text = value.ToString(); }
        }
        public double a4
        {
            get { return MyList.StringToDouble(txt_a4.Text, 0.0); }
            set { txt_a4.Text = value.ToString(); }
        }

        public double h1
        {
            get { return MyList.StringToDouble(txt_h1.Text, 0.0); }
            set { txt_h1.Text = value.ToString(); }
        }
        public double h2
        {
            get { return MyList.StringToDouble(txt_h2.Text, 0.0); }
            set { txt_h2.Text = value.ToString(); }
        }
        public double h3
        {
            get { return MyList.StringToDouble(txt_h3.Text, 0.0); }
            set { txt_h3.Text = value.ToString(); }
        }
        public double d1
        {
            get { return MyList.StringToDouble(txt_d1.Text, 0.0); }
            set { txt_d1.Text = value.ToString(); }
        }
        public double d2
        {
            get { return MyList.StringToDouble(txt_d2.Text, 0.0); }
            set { txt_d2.Text = value.ToString(); }
        }
        public double d3
        {
            get { return MyList.StringToDouble(txt_d3.Text, 0.0); }
            set { txt_d3.Text = value.ToString(); }
        }
        public int Total_Side_Cables
        {
            get { return MyList.StringToInt(txt_nos_Side_cable.Text, 0); }
            set
            {
                txt_nos_Side_cable.Text = value.ToString();
                txt_nos_centre_cable.Text = value.ToString();
            }
        }
        public int nl
        {
            get { return MyList.StringToInt(txt_n.Text, 0); }
            set { txt_n.Text = value.ToString(); }
        }
        public int Total_Cables
        {
            get { return 0; }
        }
        public int Nos_Center_Cables_Cross_Girder
        {
            get { return MyList.StringToInt(txt_cno_center_cables_cross_dist.Text, 0); }
            set { txt_cno_center_cables_cross_dist.Text = value.ToString(); }
        }
        public int Nos_Side_Cables_Cross_Girder
        {
            get { return MyList.StringToInt(txt_sno_side_cables_cross_dist.Text, 0); }
            set { txt_sno_side_cables_cross_dist.Text = value.ToString(); }
        }

        public string Input_Data
        {
            get
            {
                return txt_Ana_analysis_file.Text;
            }
            set
            {
                txt_Ana_analysis_file.Text = value;
            }
        }

        public string Input_Data_Linear
        {
            get
            {
                if (Input_Data == "") return "";

                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Linear Analysis");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_LINEAR_ANALYSIS.TXT");
            }
        }

        public string Input_Data_2D
        {
            get
            {
                if (Input_Data == "") return "";

                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Analysis_2D");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_2D.TXT");
            }
            set
            {
                txt_Ana_analysis_file.Text = value;
            }
        }

        public string Input_Data_2D_Left
        {
            get
            {
                if (Input_Data == "") return "";

                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Analysis_2D_Left");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_2D_Left.TXT");
            }
            set
            {
                //txt_Ana_analysis_file.Text = value;
            }
        }

        public string Input_Data_2D_Right
        {
            get
            {
                if (Input_Data == "") return "";

                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Analysis_2D_Right");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_2D_Right.TXT");
            }
            set
            {
                //txt_Ana_analysis_file.Text = value;
            }
        }

        public string Input_Data_Bridge_Deck
        {
            get
            {
                if (Input_Data == "") return "";

                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Analysis_Bridge_Deck");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_Bridge_Deck.TXT");
            }
            set
            {
                //txt_Ana_analysis_file.Text = value;
            }
        }



        public string Input_Data_Nonlinear
        {
            get
            {



                string fld = Path.GetDirectoryName(Input_Data);

                fld = Path.Combine(fld, "Nonlinear Analysis");
                if (!Directory.Exists(fld)) Directory.CreateDirectory(fld);

                string fl = Path.GetFileNameWithoutExtension(Input_Data);

                return Path.Combine(fld, fl + "_Nonlinear_Analysis.TXT");


            }
            set
            {
                //txt_Ana_analysis_file.Text = value;
            }
        }

        public string Analysis_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "ANALYSIS_REP.TXT");
                return "";
            }

        }
        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "CSB_DRAWINGS");
                return "";
            }

        }
        public string Cable_Design_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "CABLE_DESIGN_REP.TXT");
                return "";
            }

        }
        public string Analysis_Result_Report
        {
            get
            {
                if (Directory.Exists(user_path))
                    return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
                return "";
            }

        }
        public string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
            }
        }


        public double Cable_D
        {
            get { return MyList.StringToDouble(txt_cbl_des_d.Text, 0.0); }
            set { txt_cbl_des_d.Text = value.ToString("E3"); }
        }
        public double Cable_Ax
        {
            get { return (Math.PI * Cable_D * Cable_D) / 4.0; }
        }
        public double Cable_Gamma
        {
            get { return MyList.StringToDouble(txt_cbl_des_gamma.Text, 0.0); }
            set { txt_cbl_des_gamma.Text = value.ToString("E3"); }
        }
        public double Cable_E
        {
            get { return MyList.StringToDouble(txt_cbl_des_E.Text, 0.0); }
            set { txt_cbl_des_E.Text = value.ToString("E3"); }
        }
        public double Cable_f
        {
            get { return MyList.StringToDouble(txt_cbl_des_f.Text, 0.0); }
            set { txt_cbl_des_f.Text = value.ToString("f3"); }
        }


        public double Section_TotalNos
        {
            get { return MyList.StringToDouble(txt_sec_total_nos.Text, 0.0); }
            set { txt_sec_total_nos.Text = value.ToString("F0"); }
        }
        public double Section_Thickness
        {
            get { return MyList.StringToDouble(txt_sec_thickness.Text, 0.0); }
            set { txt_sec_thickness.Text = value.ToString(); }
        }
        public double Section_Diameter
        {
            get { return MyList.StringToDouble(txt_sec_dia.Text, 0.0); }
            set { txt_sec_dia.Text = value.ToString(); }
        }
        public double Section_Depth
        {
            get { return MyList.StringToDouble(txt_sec_D.Text, 0.0); }
            set { txt_sec_D.Text = value.ToString(); }
        }
        public double Section_Breadth
        {
            get { return MyList.StringToDouble(txt_sec_B.Text, 0.0); }
            set { txt_sec_B.Text = value.ToString(); }
        }
        public double Section_Length
        {
            get { return MyList.StringToDouble(txt_sec_L.Text, 0.0); }
            set { txt_sec_L.Text = value.ToString(""); }
        }

        public double Section_AX
        {
            get { return MyList.StringToDouble(txt_sec_AX.Text, 0.0); }
            set { txt_sec_AX.Text = value.ToString("E3"); }
        }
        public double Section_IX
        {
            get { return MyList.StringToDouble(txt_sec_IX.Text, 0.0); }
            set { txt_sec_IX.Text = value.ToString("E3"); }
        }
        public double Section_IY
        {
            get { return MyList.StringToDouble(txt_sec_IY.Text, 0.0); }
            set { txt_sec_IY.Text = value.ToString("E3"); }
        }
        public double Section_IZ
        {
            get { return MyList.StringToDouble(txt_sec_IZ.Text, 0.0); }
            set { txt_sec_IZ.Text = value.ToString("E3"); }
        }
        public double Section_UnitWeight
        {
            get { return MyList.StringToDouble(txt_sec_unit_weight.Text, 0.0); }
            set { txt_sec_unit_weight.Text = value.ToString(); }
        }
        public double Section_Weight
        {
            get { return MyList.StringToDouble(txt_sec_weight.Text, 0.0); }
            set { txt_sec_weight.Text = value.ToString("E3"); }
        }
        public eAppliedSection Section_Applied
        {
            get
            {
                if (cmb_applied_section.SelectedIndex != -1)
                {
                    return (eAppliedSection)(cmb_applied_section.SelectedIndex);
                }
                return eAppliedSection.None;
            }
            set
            {
                if (cmb_applied_section.Items.Count > 0)
                {
                    cmb_applied_section.SelectedIndex = (int)value;
                }
            }
        }
        public double Section_AngleThickness
        {
            get { return MyList.StringToDouble(cmb_sec_thk.Text, 0.0); }
            set { cmb_sec_thk.SelectedItem = value.ToString(); }
        }
        public string Section_Code
        {
            get { return cmb_code1.Text; }
            set { cmb_code1.SelectedItem = value.ToString(); }
        }
        public string Section_Name
        {
            get { return cmb_section_name.Text; }
            set { cmb_section_name.SelectedItem = value.ToString(); }
        }
        public double Top_Plate_Width
        {
            get { return MyList.StringToDouble(txt_tp_width.Text, 0.0); }
            set { txt_tp_width.Text = value.ToString(); }
        }
        public double Top_Plate_Thickness
        {
            get { return MyList.StringToDouble(txt_tp_thk.Text, 0.0); }
            set { txt_tp_thk.Text = value.ToString(); }
        }
        public double Side_Plate_Width
        {
            get { return MyList.StringToDouble(txt_sp_wd.Text, 0.0); }
            set { txt_sp_wd.Text = value.ToString(); }
        }
        public double Side_Plate_Thickness
        {
            get { return MyList.StringToDouble(txt_sp_thk.Text, 0.0); }
            set { txt_sp_thk.Text = value.ToString(); }
        }
        public double Vertical_Stiffener_Plate_Width
        {
            get { return MyList.StringToDouble(txt_vsp_wd.Text, 0.0); }
            set { txt_vsp_wd.Text = value.ToString(); }
        }
        public double Vertical_Stiffener_Plate_Thickness
        {
            get { return MyList.StringToDouble(txt_vsp_thk.Text, 0.0); }
            set { txt_vsp_thk.Text = value.ToString(); }
        }
        public double Bottom_Plate_Width
        {
            get { return MyList.StringToDouble(txt_bp_wd.Text, 0.0); }
            set { txt_bp_wd.Text = value.ToString(); }
        }
        public double Bottom_Plate_Thickness
        {
            get { return MyList.StringToDouble(txt_bp_thk.Text, 0.0); }
            set { txt_bp_thk.Text = value.ToString(); }
        }
        public double Nos_Of_Sections
        {
            get { return MyList.StringToDouble(txt_no_ele.Text, 0.0); }
            set { txt_no_ele.Text = value.ToString(); }
        }

        //public double 
        public double Weight_Factor
        {
            get { return (1.0 + (MyList.StringToDouble(txt_weight_factor.Text, 0.0) / 100.0)); }
            set { txt_weight_factor.Text = value.ToString(); }
        }
        public double Section_Lateral_Spacing
        {
            get { return MyList.StringToDouble(txt_sec_lat_spac.Text, 0.0); }
            set { txt_sec_lat_spac.Text = value.ToString(); }
        }

        #endregion Properties

        #region Members
        MemberCollection long_girders = null;
        MemberCollection cross_girders = null;
        MemberCollection cables = null;
        MemberCollection pylon = null;
        MemberCollection tie_beam = null;

        CableMemberCollection Cable_Members { get; set; }
        #endregion Members

        #region User Events
        public frmCableStayed_LS(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = app.LastDesignWorkingFolder;
            SectionProperty = new List<MemberSectionProperty>();
            long_girders = new MemberCollection();
            cross_girders = new MemberCollection();
            cables = new MemberCollection();
            pylon = new MemberCollection();
            tie_beam = new MemberCollection();

            Cable_Members = new CableMemberCollection();

            Deck = new CableStayedDeckSlab(iApp);
        }

        void CreateData()
        {

            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members

            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;
            list_z_values.Add(z);
            list_z_values.Add(d1);

            z = d1 + spacing;

            while (z <= W)
            {
                z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
                list_z_values.Add(z);
                z += spacing;
                if ((z + mergin_value) > W)
                {
                    break;
                }
            }
            list_z_values.Add(B - d2);
            list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = d1;
                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = (B - d1);
                Joints.Add(jn);
            }
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = d2;
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = (B - d2);
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            for (i = 0; i < list_x_values.Count; i++)
            {
                for (j = 1; j < list_z_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j - 1];

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cross_girders.Add(mbr);
                }
            }
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            #endregion Cross Girder

            #region Left Pylons 1/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Left Pylons
            //#region Right Pylons 2/1

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Pylons
            //#region Right Pylons 2/2

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons



            #region Left Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                try
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables


            #region Right Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1 + L2;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1 + L2;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA SPACE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");
            foreach (var item in Joints.JointNodes) list_data.Add(item.ToString());
            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members) list_data.Add(item.ToString());
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");
            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty) list_data.Add(item.ToString());
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8                ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                List<int> supports = new List<int>();
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    supports.Add(cables[c].StartNode.NodeNo);
                }
                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                supports.Clear();
                sup.Clear();
            }

            list_data.Add("LOAD                1 ");
            list_data.Add("JOINT LOAD");
            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");
            list_data.Add("LOAD                1");
            list_data.Add("JOINT LOAD");
            list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            list_data.Add("1 TO 36 FY  -9.454");
            list_data.Add("172 TO 207 FY  -9.454");
            list_data.Add("343 TO 378 FY  -9.454");
            list_data.Add("514 TO 549 FY  -9.454");
            list_data.Add("685 TO 720 FY  -9.454");
            list_data.Add("856 TO 891 FY  -9.454");
            list_data.Add("1027 TO 1062 FY  -9.454");
            list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            list_data.Add("36 TO 135 FY  -11.970");
            list_data.Add("207 TO 306 FY  -11.970");
            list_data.Add("378 TO 477 FY  -11.970");
            list_data.Add("549 TO 648  FY  -11.970");
            list_data.Add("720 TO 819 FY  -11.970");
            list_data.Add("891 TO 990 FY  -11.970");
            list_data.Add("1062 TO 1161 FY  -11.970");
            list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            list_data.Add("136 TO 171 FY  -9.454");
            list_data.Add("307 TO 342 FY  -9.454");
            list_data.Add("478 TO 513 FY  -9.454");
            list_data.Add("649 TO 684 FY  -9.454");
            list_data.Add("820 TO 855 FY  -9.454");
            list_data.Add("991 TO 1026 FY  -9.454");
            list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data, list_data.ToArray());
            //iApp.RunExe(Input_Data);
        }

        List<int> HA_Loading_Members = new List<int>();
        List<double> HA_Dists = new List<double>();

        void CreateData_Total_Structure()
        {




            List<int> supports = new List<int>();

            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members




            #region British Loads

            HA_Loading_Members.Clear();
            HA_Dists.Clear();
            if (HA_Lanes != null)
            {
                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
                }
            }

            #endregion British Loads




            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;
            list_z_values.Add(z);
            list_z_values.Add(d1);

            z = d1 + spacing;

            while (z <= W)
            {
                z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
                list_z_values.Add(z);
                z += spacing;
                if ((z + mergin_value) > W)
                {
                    break;
                }
            }
            list_z_values.Add(B - d2);
            list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1, list_y_values[i], d1);
                //else
                {

                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1;
                    jn.Y = list_y_values[i];
                    jn.Z = d1;
                }

                if (i < 2)
                    supports.Add(jn.NodeNo);


                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1, list_y_values[i], (B - d1));
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1;
                    jn.Y = list_y_values[i];
                    jn.Z = (B - d1);
                }


                if (i < 2)  supports.Add(jn.NodeNo);

                Joints.Add(jn);

            }
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1 + L2, list_y_values[i], d2);
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1 + L2;
                    jn.Y = list_y_values[i];
                    jn.Z = d2;
                }
                Joints.Add(jn);

                if (i < 2) supports.Add(jn.NodeNo);

            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1 + L2, list_y_values[i], (B - d2));
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1 + L2;
                    jn.Y = list_y_values[i];
                    jn.Z = (B - d2);
                }
                Joints.Add(jn);

                if (i < 2) supports.Add(jn.NodeNo);

            }
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);



                    //if (HA_Dists.Contains(mbr.EndNode.Z) && HA_Dists.Contains(mbr.StartNode.Z))

                    foreach (var item in HA_Dists)
                    {

                        if ((mbr.StartNode.Z > item) && (mbr.EndNode.Z > item))
                        {
                            if (!HA_Loading_Members.Contains(mbr.MemberNo))
                                HA_Loading_Members.Add(mbr.MemberNo);
                        }
                    }
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            for (i = 0; i < list_x_values.Count; i++)
            {
                for (j = 1; j < list_z_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j - 1];

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cross_girders.Add(mbr);
                }
            }
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            #endregion Cross Girder

            #region Left Pylons 1/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Left Pylons
            //#region Right Pylons 2/1

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Pylons
            //#region Right Pylons 2/2

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons



            #region Left Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                try
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables


            #region Right Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1 + L2;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1 + L2;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA SPACE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");

            #region Modified Camber
            List<double> lst_y_incr = new List<double>();
            double camber = MyList.StringToDouble(txt_camber.Text, 0.0)/100;
            for (i = 0; i < list_x_values.Count; i++)
			{
                if (i < (list_x_values.Count / 2))
                    lst_y_incr.Add(list_x_values[i] * camber);
			}
            if (list_x_values.Count % 2 != 0)
            {
                lst_y_incr.Add(lst_y_incr[lst_y_incr.Count - 1]);
                for (i = lst_y_incr.Count - 2; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }
            else
            {

                for (i = lst_y_incr.Count - 1; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }


            int indx = -1;
            foreach (var item in Joints.JointNodes)
            {
                indx = Get_Index(list_x_values, item.X);
                if (indx != -1)
                {
                    jn = new JointNode();
                    jn.NodeNo = item.NodeNo;
                    jn.X = item.X;
                    jn.Y = item.Y + lst_y_incr[indx];
                    jn.Z = item.Z;

                    list_data.Add(jn.ToString());
                }
                else
                {
                    list_data.Add(item.ToString());
                }
            }

            #endregion Modified Camber


            List<int> cbl_nos = new List<int>();

            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members)
            {
                cbl_nos.Add(item.MemberNo);
                list_data.Add(item.ToString());
            }
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");


            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty) list_data.Add(item.ToString());
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8 ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    //supports.Add(cables[c].StartNode.NodeNo);
                }

                supports.Add(nd1);
                supports.Add(nd2);
                supports.Add(nd3);
                supports.Add(nd4);



                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            //list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            list_data.Add(MyList.Get_Array_Text(sup) + " FIXED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                {
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                    //list_data.Add(MyList.Get_Array_Text(supports) + " FIXED");
                }
                supports.Clear();
                sup.Clear();
            }

            //list_data.Add("LOAD                1 ");
            //list_data.Add("JOINT LOAD");
            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");
            list_data.Add("LOAD 1");
            list_data.Add("JOINT LOAD");
            list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            list_data.Add("1 TO 36 FY  -9.454");
            list_data.Add("172 TO 207 FY  -9.454");
            list_data.Add("343 TO 378 FY  -9.454");
            list_data.Add("514 TO 549 FY  -9.454");
            list_data.Add("685 TO 720 FY  -9.454");
            list_data.Add("856 TO 891 FY  -9.454");
            list_data.Add("1027 TO 1062 FY  -9.454");
            list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            list_data.Add("36 TO 135 FY  -11.970");
            list_data.Add("207 TO 306 FY  -11.970");
            list_data.Add("378 TO 477 FY  -11.970");
            list_data.Add("549 TO 648  FY  -11.970");
            list_data.Add("720 TO 819 FY  -11.970");
            list_data.Add("891 TO 990 FY  -11.970");
            list_data.Add("1062 TO 1161 FY  -11.970");
            list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            list_data.Add("136 TO 171 FY  -9.454");
            list_data.Add("307 TO 342 FY  -9.454");
            list_data.Add("478 TO 513 FY  -9.454");
            list_data.Add("649 TO 684 FY  -9.454");
            list_data.Add("820 TO 855 FY  -9.454");
            list_data.Add("991 TO 1026 FY  -9.454");
            list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data


            txt_cbl_des_mem_nos.Text = MyList.Get_Array_Text(cbl_nos);
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            //iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data, list_data.ToArray());

            File.WriteAllLines(Input_Data_Linear, list_data.ToArray());


            if (CS_Analysis == null)
                CS_Analysis = new CABLE_STAYED_LS_Analysis(iApp);
            CS_Analysis.Input_File = Input_Data_Linear;

            File.WriteAllLines(Input_Data, list_data.ToArray());



            //File.WriteAllLines(CS_Analysis.TotalAnalysis_Input_File, list_data.ToArray());
            File.WriteAllLines(CS_Analysis.DeadLoadAnalysis_Input_File, list_data.ToArray());
            //File.WriteAllLines(CS_Analysis.Get_LL_Analysis_Input_File(1), list_data.ToArray());



            //for (i = 0; i < all_loads.Count; i++)
            //{
            //    File.WriteAllLines(CS_Analysis.Get_LL_Analysis_Input_File(i + 1), list_data.ToArray());
            //}




             //uC_Deckslab_BS1.user_path = user_path;

                //    Long_Girder_Analysis.HA_Lanes = HA_Lanes;

                //    Long_Girder_Analysis.CreateData_British();

                //    Long_Girder_Analysis.WriteData_Total_Analysis(Long_Girder_Analysis.Input_File, true);
                //    Long_Girder_Analysis.WriteData_Total_Analysis(inp_file, true);

                //    Calculate_Load_Computation(Long_Girder_Analysis._Outer_Girder_Mid,
                //        Long_Girder_Analysis._Inner_Girder_Mid,
                //         Long_Girder_Analysis.joints_list_for_load);

                //    Long_Girder_Analysis.WriteData_Total_Analysis(Long_Girder_Analysis.TotalAnalysis_Input_File);

                //    Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.TotalAnalysis_Input_File, long_ll);
                //    Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, long_ll);
                //    Long_Girder_Analysis.WriteData_DeadLoad_Analysis(Long_Girder_Analysis.DeadLoadAnalysis_Input_File);

                //    Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.Input_File, true, true);




                //    if (rbtn_HA.Checked == false)
                //    {
                //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_1_Input_File, long_ll);
                //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_2_Input_File, long_ll);
                //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_3_Input_File, long_ll);
                //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_4_Input_File, long_ll);
                //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_5_Input_File, long_ll);


                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.TotalAnalysis_Input_File, true, true, 5);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, true, false, 5);

                //        Chiranjit [2013 09 24] Without Dead Load
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_1_Input_File, true, false, 1);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_2_Input_File, true, false, 2);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_3_Input_File, true, false, 3);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_4_Input_File, true, false, 4);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_5_Input_File, true, false, 5);


                //    }
                //    else
                //    {
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.TotalAnalysis_Input_File, true, true, 1);
                //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, false, false, 1);
                //    }

                //}


                //Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.DeadLoadAnalysis_Input_File, false, true);

                //Long_Girder_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.TotalAnalysis_Input_File);

                //string ll_txt = Long_Girder_Analysis.LiveLoad_File;

                //Long_Girder_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                //if (Long_Girder_Analysis.Live_Load_List == null) return;

                //cmb_long_open_file.SelectedIndex = 0;
                //Button_Enable_Disable();

                //MessageBox.Show(this, MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
                //  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);


           

            //iApp.RunExe(Input_Data);
        }

        void CreateData_Total_Structure_LL()
        {




            List<int> supports = new List<int>();

            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members




            #region British Loads

            HA_Loading_Members.Clear();
            HA_Dists.Clear();
            if (HA_Lanes != null)
            {
                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
                }
            }

            #endregion British Loads




            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;
            list_z_values.Add(z);
            list_z_values.Add(d1);

            z = d1 + spacing;

            while (z <= W)
            {
                z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
                list_z_values.Add(z);
                z += spacing;
                if ((z + mergin_value) > W)
                {
                    break;
                }
            }
            list_z_values.Add(B - d2);
            list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1, list_y_values[i], d1);
                //else
                {

                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1;
                    jn.Y = list_y_values[i];
                    jn.Z = d1;
                }

                if (i < 2)
                    supports.Add(jn.NodeNo);


                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1, list_y_values[i], (B - d1));
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1;
                    jn.Y = list_y_values[i];
                    jn.Z = (B - d1);
                }


                if (i < 2) supports.Add(jn.NodeNo);

                Joints.Add(jn);

            }
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1 + L2, list_y_values[i], d2);
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1 + L2;
                    jn.Y = list_y_values[i];
                    jn.Z = d2;
                }
                Joints.Add(jn);

                if (i < 2) supports.Add(jn.NodeNo);

            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {

                //if (i == 1)
                //    jn = Joints.GetJoints(L1 + L2, list_y_values[i], (B - d2));
                //else
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = L1 + L2;
                    jn.Y = list_y_values[i];
                    jn.Z = (B - d2);
                }
                Joints.Add(jn);

                if (i < 2) supports.Add(jn.NodeNo);

            }
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);



                    //if (HA_Dists.Contains(mbr.EndNode.Z) && HA_Dists.Contains(mbr.StartNode.Z))

                    foreach (var item in HA_Dists)
                    {

                        if ((mbr.StartNode.Z > item) && (mbr.EndNode.Z > item))
                        {
                            if (!HA_Loading_Members.Contains(mbr.MemberNo))
                                HA_Loading_Members.Add(mbr.MemberNo);
                        }
                    }
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            for (i = 0; i < list_x_values.Count; i++)
            {
                for (j = 1; j < list_z_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j - 1];

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cross_girders.Add(mbr);
                }
            }
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            #endregion Cross Girder

            #region Left Pylons 1/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Left Pylons
            //#region Right Pylons 2/1

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Pylons
            //#region Right Pylons 2/2

            //start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons



            #region Left Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                try
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables


            #region Right Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Side Cables 2
            //start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Centre Cables 2
            //start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            start_mbr_no = mem_count;
            mbr = new Member();
            mbr.MemberNo = mem_count++;

            x = L1 + L2;
            y = h1 + h3;
            z = d1;

            mbr.StartNode = Joints.GetJoints(x, y, z);

            x = L1 + L2;
            y = h1 + h3;
            z = B - d1;

            mbr.EndNode = Joints.GetJoints(x, y, z);

            tie_beam.Add(mbr);
            list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA SPACE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");

            #region Modified Camber
            List<double> lst_y_incr = new List<double>();
            double camber = MyList.StringToDouble(txt_camber.Text, 0.0) / 100;
            for (i = 0; i < list_x_values.Count; i++)
            {
                if (i < (list_x_values.Count / 2))
                    lst_y_incr.Add(list_x_values[i] * camber);
            }
            if (list_x_values.Count % 2 != 0)
            {
                lst_y_incr.Add(lst_y_incr[lst_y_incr.Count - 1]);
                for (i = lst_y_incr.Count - 2; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }
            else
            {

                for (i = lst_y_incr.Count - 1; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }


            int indx = -1;
            foreach (var item in Joints.JointNodes)
            {
                indx = Get_Index(list_x_values, item.X);
                if (indx != -1)
                {
                    jn = new JointNode();
                    jn.NodeNo = item.NodeNo;
                    jn.X = item.X;
                    //jn.Y = item.Y + lst_y_incr[indx];
                    jn.Y = item.Y - h1;
                    jn.Z = item.Z;

                    list_data.Add(jn.ToString());
                }
                else
                {
                    list_data.Add(item.ToString());
                }
            }

            #endregion Modified Camber


            List<int> cbl_nos = new List<int>();

            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members)
            {
                cbl_nos.Add(item.MemberNo);
                list_data.Add(item.ToString());
            }
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");


            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty) list_data.Add(item.ToString());
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8 ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    //supports.Add(cables[c].StartNode.NodeNo);
                }

                supports.Add(nd1);
                supports.Add(nd2);
                supports.Add(nd3);
                supports.Add(nd4);



                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            //list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            list_data.Add(MyList.Get_Array_Text(sup) + " FIXED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                {
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                    //list_data.Add(MyList.Get_Array_Text(supports) + " FIXED");
                }
                supports.Clear();
                sup.Clear();
            }

            //list_data.Add("LOAD                1 ");
            //list_data.Add("JOINT LOAD");
            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");
            list_data.Add("LOAD 1");
            list_data.Add("JOINT LOAD");
            list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            list_data.Add("1 TO 36 FY  -9.454");
            list_data.Add("172 TO 207 FY  -9.454");
            list_data.Add("343 TO 378 FY  -9.454");
            list_data.Add("514 TO 549 FY  -9.454");
            list_data.Add("685 TO 720 FY  -9.454");
            list_data.Add("856 TO 891 FY  -9.454");
            list_data.Add("1027 TO 1062 FY  -9.454");
            list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            list_data.Add("36 TO 135 FY  -11.970");
            list_data.Add("207 TO 306 FY  -11.970");
            list_data.Add("378 TO 477 FY  -11.970");
            list_data.Add("549 TO 648  FY  -11.970");
            list_data.Add("720 TO 819 FY  -11.970");
            list_data.Add("891 TO 990 FY  -11.970");
            list_data.Add("1062 TO 1161 FY  -11.970");
            list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            list_data.Add("136 TO 171 FY  -9.454");
            list_data.Add("307 TO 342 FY  -9.454");
            list_data.Add("478 TO 513 FY  -9.454");
            list_data.Add("649 TO 684 FY  -9.454");
            list_data.Add("820 TO 855 FY  -9.454");
            list_data.Add("991 TO 1026 FY  -9.454");
            list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data


            txt_cbl_des_mem_nos.Text = MyList.Get_Array_Text(cbl_nos);
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data, list_data.ToArray());

            File.WriteAllLines(Input_Data_Linear, list_data.ToArray());


            if (CS_Analysis == null)
                CS_Analysis = new CABLE_STAYED_LS_Analysis(iApp);
            CS_Analysis.Input_File = Input_Data_Linear;

            File.WriteAllLines(Input_Data, list_data.ToArray());



            File.WriteAllLines(CS_Analysis.TotalAnalysis_Input_File, list_data.ToArray());
            //File.WriteAllLines(CS_Analysis.DeadLoadAnalysis_Input_File, list_data.ToArray());
            File.WriteAllLines(CS_Analysis.Get_LL_Analysis_Input_File(1), list_data.ToArray());


            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                LONG_GIRDER_LL_TXT();
            else
                LONG_GIRDER_BRITISH_LL_TXT();


            for (i = 0; i < all_loads.Count; i++)
            {
                File.WriteAllLines(CS_Analysis.Get_LL_Analysis_Input_File(i + 1), list_data.ToArray());
            }




            //uC_Deckslab_BS1.user_path = user_path;

            //    Long_Girder_Analysis.HA_Lanes = HA_Lanes;

            //    Long_Girder_Analysis.CreateData_British();

            //    Long_Girder_Analysis.WriteData_Total_Analysis(Long_Girder_Analysis.Input_File, true);
            //    Long_Girder_Analysis.WriteData_Total_Analysis(inp_file, true);

            //    Calculate_Load_Computation(Long_Girder_Analysis._Outer_Girder_Mid,
            //        Long_Girder_Analysis._Inner_Girder_Mid,
            //         Long_Girder_Analysis.joints_list_for_load);

            //    Long_Girder_Analysis.WriteData_Total_Analysis(Long_Girder_Analysis.TotalAnalysis_Input_File);

            //    Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.TotalAnalysis_Input_File, long_ll);
            //    Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, long_ll);
            //    Long_Girder_Analysis.WriteData_DeadLoad_Analysis(Long_Girder_Analysis.DeadLoadAnalysis_Input_File);

            //    Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.Input_File, true, true);




            //    if (rbtn_HA.Checked == false)
            //    {
            //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_1_Input_File, long_ll);
            //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_2_Input_File, long_ll);
            //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_3_Input_File, long_ll);
            //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_4_Input_File, long_ll);
            //        Long_Girder_Analysis.WriteData_LiveLoad_Analysis(Long_Girder_Analysis.LL_Analysis_5_Input_File, long_ll);


            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.TotalAnalysis_Input_File, true, true, 5);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, true, false, 5);

            //        Chiranjit [2013 09 24] Without Dead Load
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_1_Input_File, true, false, 1);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_2_Input_File, true, false, 2);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_3_Input_File, true, false, 3);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_4_Input_File, true, false, 4);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LL_Analysis_5_Input_File, true, false, 5);


            //    }
            //    else
            //    {
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.TotalAnalysis_Input_File, true, true, 1);
            //        Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, false, false, 1);
            //    }

            //}


            //Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.DeadLoadAnalysis_Input_File, false, true);

            //Long_Girder_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.TotalAnalysis_Input_File);

            //string ll_txt = Long_Girder_Analysis.LiveLoad_File;

            //Long_Girder_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Long_Girder_Analysis.Live_Load_List == null) return;

            //cmb_long_open_file.SelectedIndex = 0;
            //Button_Enable_Disable();

            //MessageBox.Show(this, MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
            //  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);




            //iApp.RunExe(Input_Data);
        }

        public int Get_Index(List<double> list, double val)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs(list[i] - val) < 0.9) return i;
                if (list[i].ToString("f2") == val.ToString("f2")) return i;
            }
            return -1;
        }
        void CreateData_2D()
        {

            double d1 = 0;
            double d2 = 0;
            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();


            List<double> list_y_incr = new List<double>();




            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members

            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            #endregion X Values

            list_y_incr.Clear();


            double mid_point = L1 + L2 / 2.0;
            foreach (var item in list_x_values)
            {
                if (item < mid_point)
                list_y_incr.Add(item * 0.05);
            }

            list_y_incr.Add(list_y_incr[list_y_incr.Count - 1]);

            for (i  = list_y_incr.Count - 2; i  >= 0; i--)
            {
                list_y_incr.Add(list_y_incr[i]);
            }

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;

            list_z_values.Add(z);
            //list_z_values.Add(d1);

            z = d1 + spacing;

            //while (z <= W)
            //{
            //    z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
            //    list_z_values.Add(z);
            //    z += spacing;
            //    if ((z + mergin_value) > W)
            //    {
            //        break;
            //    }
            //}
            //list_z_values.Add(B - d2);
            //list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];

                    //jn.Y = h1;
                    jn.Y = h1 + list_y_incr[j];




                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = d1;
                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1;
            //    jn.Y = list_y_values[i];
            //    jn.Z = (B - d1);
            //    Joints.Add(jn);
            //}
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = d2;
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1 + L2;
            //    jn.Y = list_y_values[i];
            //    jn.Z = (B - d2);
            //    Joints.Add(jn);
            //}
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];

                    //y = h1;
                    y = h1 + list_y_incr[j - 1];

                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    //y = h1;
                    //y = h1 + (list_x_values[j] - list_x_values[j - 1]) * 0.05;
                    y = h1 + list_y_incr[j];
                    
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length >= a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            //for (i = 0; i < list_x_values.Count; i++)
            //{
            //    for (j = 1; j < list_z_values.Count; j++)
            //    {
            //        mbr = new Member();
            //        mbr.MemberNo = mem_count++;

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j - 1];

            //        mbr.StartNode = Joints.GetJoints(x, y, z);

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j];

            //        mbr.EndNode = Joints.GetJoints(x, y, z);

            //        cross_girders.Add(mbr);
            //    }
            //}
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
                kStr = MyList.Get_Array_Text(list_side_mem_no);
                list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

                kStr = MyList.Get_Array_Text(list_center_mem_no);
                list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            }
            #endregion Cross Girder

            #region Left Pylons 1/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Left Pylons
            //#region Right Pylons 2/1

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1;
            //    y = list_y_values[i - 1];
            //    z = (B - d2);

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = list_y_values[i];
            //    z = (B - d2);

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Pylons
            //#region Right Pylons 2/2

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1 + L2;
            //    y = list_y_values[i - 1];
            //    z = (B - d2);

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = list_y_values[i];
            //    z = (B - d2);

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            int indx = -1;

            #region Left Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                try
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;

                    indx = Get_Index(list_x_values, x);
                    if (indx != -1) y = y + list_y_incr[indx];
                   

                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];

                    indx = Get_Index(list_x_values, x);
                    //if (indx != -1)   y = y + list_y_incr[indx];
                  

                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Side Cables 2
            //start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = i * Nos_Side_Cables_Cross_Girder * a1;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = pylon_y[i];
            //    z = B - d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;

                indx = Get_Index(list_x_values, x);
                if (indx != -1) y = y + list_y_incr[indx];
               

                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];


                indx = Get_Index(list_x_values, x);
                //if (indx != -1)  y = y + list_y_incr[indx];
              

                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Centre Cables 2
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = B - d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables


            #region Right Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a3;
                y = h1;
                //z = list_z_values[0];

                indx = Get_Index(list_x_values, x);
                if (indx != -1)  y = y + list_y_incr[indx];

                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];


                indx = Get_Index(list_x_values, x);
                //if (indx != -1) y = y + list_y_incr[indx];
               
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Side Cables 2
            //start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L - i * Nos_Side_Cables_Cross_Girder * a1;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[i];
            //    z = B - d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;

                indx = Get_Index(list_x_values, x);
                if (indx != -1)
                    y = y + list_y_incr[indx];
              


                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];


                indx = Get_Index(list_x_values, x);
                //if (indx != -1) y = y + list_y_incr[indx];
                


                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Centre Cables 2
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = B - d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1 + L2;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1 + L2;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA PLANE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");
            foreach (var item in Joints.JointNodes) list_data.Add(item.ToString());
            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members) list_data.Add(item.ToString());
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");
            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty)
                {
                    if (!item.GroupName.Contains("_XGIRDER") && !item.GroupName.Contains("_TIEBEAM"))
                        list_data.Add(item.ToString());
                }
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8                ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                List<int> supports = new List<int>();
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    supports.Add(cables[c].StartNode.NodeNo);
                }
                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                supports.Clear();
                sup.Clear();
            }

            list_data.Add("LOAD 1 DL + SIDL");
            list_data.Add("JOINT LOAD");

            MemberGroup mgr1 = MemberGroup.Parse(list_member_group[0]);
            MemberGroup mgr2 = MemberGroup.Parse(list_member_group[1]);

            double side_span1 = 0.0;
            double centre_span = 0.0;
            //string kStr = "";
            for (i = 0; i < dgv_section_property.RowCount; i++)
            {
                try
                {
                    kStr = dgv_section_property[1, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }

            for (i = 0; i < dgv_SIDL.RowCount; i++)
            {
                try
                {
                    kStr = dgv_SIDL[0, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }


            mgr1.SetMemNos();
            mgr2.SetMemNos();
            int centre_count = mgr2.MemberNos.Count;
            int side_count = mgr1.MemberNos.Count;



            list_data.Add(string.Format("{0} FY -{1:f3}", mgr1.MemberNosText, centre_span / centre_count));
            list_data.Add(string.Format("{0} FY -{1:f3}", mgr2.MemberNosText, side_span1 / side_count));

            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");


            //list_data.Add("LOAD 1");
            //list_data.Add("JOINT LOAD");


            //list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            //list_data.Add("1 TO 36 FY  -9.454");
            //list_data.Add("172 TO 207 FY  -9.454");
            //list_data.Add("343 TO 378 FY  -9.454");
            //list_data.Add("514 TO 549 FY  -9.454");
            //list_data.Add("685 TO 720 FY  -9.454");
            //list_data.Add("856 TO 891 FY  -9.454");
            //list_data.Add("1027 TO 1062 FY  -9.454");
            //list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            //list_data.Add("36 TO 135 FY  -11.970");
            //list_data.Add("207 TO 306 FY  -11.970");
            //list_data.Add("378 TO 477 FY  -11.970");
            //list_data.Add("549 TO 648  FY  -11.970");
            //list_data.Add("720 TO 819 FY  -11.970");
            //list_data.Add("891 TO 990 FY  -11.970");
            //list_data.Add("1062 TO 1161 FY  -11.970");
            //list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            //list_data.Add("136 TO 171 FY  -9.454");
            //list_data.Add("307 TO 342 FY  -9.454");
            //list_data.Add("478 TO 513 FY  -9.454");
            //list_data.Add("649 TO 684 FY  -9.454");
            //list_data.Add("820 TO 855 FY  -9.454");
            //list_data.Add("991 TO 1026 FY  -9.454");
            //list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data_2D, list_data.ToArray());
            //iApp.RunExe(Input_Data);
        }

        void CreateData_2D_Left()
        {

            double d1 = 0;
            double d2 = 0;
            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members

            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;

            list_z_values.Add(z);
            //list_z_values.Add(d1);

            z = d1 + spacing;

            //while (z <= W)
            //{
            //    z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
            //    list_z_values.Add(z);
            //    z += spacing;
            //    if ((z + mergin_value) > W)
            //    {
            //        break;
            //    }
            //}
            //list_z_values.Add(B - d2);
            //list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = d1;
                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1;
            //    jn.Y = list_y_values[i];
            //    jn.Z = (B - d1);
            //    Joints.Add(jn);
            //}
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = d2;
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1 + L2;
            //    jn.Y = list_y_values[i];
            //    jn.Z = (B - d2);
            //    Joints.Add(jn);
            //}
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            //for (i = 0; i < list_x_values.Count; i++)
            //{
            //    for (j = 1; j < list_z_values.Count; j++)
            //    {
            //        mbr = new Member();
            //        mbr.MemberNo = mem_count++;

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j - 1];

            //        mbr.StartNode = Joints.GetJoints(x, y, z);

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j];

            //        mbr.EndNode = Joints.GetJoints(x, y, z);

            //        cross_girders.Add(mbr);
            //    }
            //}
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
                kStr = MyList.Get_Array_Text(list_side_mem_no);
                list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

                kStr = MyList.Get_Array_Text(list_center_mem_no);
                list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            }
            #endregion Cross Girder

            #region Left Pylons 1/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Left Pylons
            //#region Right Pylons 2/1

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1;
            //    y = list_y_values[i - 1];
            //    z = (B - d2);

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = list_y_values[i];
            //    z = (B - d2);

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Pylons
            //#region Right Pylons 2/2

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1 + L2;
            //    y = list_y_values[i - 1];
            //    z = (B - d2);

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = list_y_values[i];
            //    z = (B - d2);

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons



            #region Left Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                try
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Side Cables 2
            //start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = i * Nos_Side_Cables_Cross_Girder * a1;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = pylon_y[i];
            //    z = B - d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Left Centre Cables 2
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = B - d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables


            #region Right Side Cables 1
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Side Cables 2
            //start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L - i * Nos_Side_Cables_Cross_Girder * a1;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[i];
            //    z = B - d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[0];
                z = d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables
            //#region Right Centre Cables 2
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[list_z_values.Count - 1];
            //    z = B - d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = B - d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1 + L2;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1 + L2;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA PLANE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");

            //foreach (var item in Joints.JointNodes) list_data.Add(item.ToString());
            #region Modified Camber
            List<double> lst_y_incr = new List<double>();
            double camber = MyList.StringToDouble(txt_camber.Text, 0.0) / 100;
            for (i = 0; i < list_x_values.Count; i++)
            {
                if (i < (list_x_values.Count / 2))
                    lst_y_incr.Add(list_x_values[i] * camber);
            }
            if (list_x_values.Count % 2 != 0)
            {
                lst_y_incr.Add(lst_y_incr[lst_y_incr.Count - 1]);
                for (i = lst_y_incr.Count - 2; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }
            else
            {

                for (i = lst_y_incr.Count - 1; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }


            int indx = -1;
            foreach (var item in Joints.JointNodes)
            {
                indx = Get_Index(list_x_values, item.X);
                if (indx != -1)
                {
                    jn = new JointNode();
                    jn.NodeNo = item.NodeNo;
                    jn.X = item.X;
                    jn.Y = item.Y + lst_y_incr[indx];
                    jn.Z = item.Z;

                    list_data.Add(jn.ToString());
                }
                else
                {
                    list_data.Add(item.ToString());
                }
            }

            #endregion Modified Camber




            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members) list_data.Add(item.ToString());
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");
            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty)
                {
                    if (!item.GroupName.Contains("_XGIRDER") && !item.GroupName.Contains("_TIEBEAM"))
                        list_data.Add(item.ToString());
                }
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8                ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                List<int> supports = new List<int>();
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    supports.Add(cables[c].StartNode.NodeNo);
                }
                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                supports.Clear();
                sup.Clear();
            }

            list_data.Add("LOAD 1 DL + SIDL");
            list_data.Add("JOINT LOAD");

            MemberGroup mgr1 = MemberGroup.Parse(list_member_group[0]);
            MemberGroup mgr2 = MemberGroup.Parse(list_member_group[1]);

            double side_span1 = 0.0;
            double centre_span = 0.0;
            //string kStr = "";
            for (i = 0; i < dgv_section_property.RowCount; i++)
            {
                try
                {
                    kStr = dgv_section_property[1, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }

            for (i = 0; i < dgv_SIDL.RowCount; i++)
            {
                try
                {
                    kStr = dgv_SIDL[0, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }


            mgr1.SetMemNos();
            mgr2.SetMemNos();
            int centre_count = mgr2.MemberNos.Count;
            int side_count = mgr1.MemberNos.Count;



            list_data.Add(string.Format("{0} FY -{1:f3}", mgr1.MemberNosText, centre_span / centre_count));
            list_data.Add(string.Format("{0} FY -{1:f3}", mgr2.MemberNosText, side_span1 / side_count));

            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");


            //list_data.Add("LOAD 1");
            //list_data.Add("JOINT LOAD");


            //list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            //list_data.Add("1 TO 36 FY  -9.454");
            //list_data.Add("172 TO 207 FY  -9.454");
            //list_data.Add("343 TO 378 FY  -9.454");
            //list_data.Add("514 TO 549 FY  -9.454");
            //list_data.Add("685 TO 720 FY  -9.454");
            //list_data.Add("856 TO 891 FY  -9.454");
            //list_data.Add("1027 TO 1062 FY  -9.454");
            //list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            //list_data.Add("36 TO 135 FY  -11.970");
            //list_data.Add("207 TO 306 FY  -11.970");
            //list_data.Add("378 TO 477 FY  -11.970");
            //list_data.Add("549 TO 648  FY  -11.970");
            //list_data.Add("720 TO 819 FY  -11.970");
            //list_data.Add("891 TO 990 FY  -11.970");
            //list_data.Add("1062 TO 1161 FY  -11.970");
            //list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            //list_data.Add("136 TO 171 FY  -9.454");
            //list_data.Add("307 TO 342 FY  -9.454");
            //list_data.Add("478 TO 513 FY  -9.454");
            //list_data.Add("649 TO 684 FY  -9.454");
            //list_data.Add("820 TO 855 FY  -9.454");
            //list_data.Add("991 TO 1026 FY  -9.454");
            //list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data_2D_Left, list_data.ToArray());
            //iApp.RunExe(Input_Data);
        }

        void CreateData_2D_Right()
        {

            double d1 = 0;
            double d2 = 0;
            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members

            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;

            //list_z_values.Add(z);
            //list_z_values.Add(d1);

            z = d1 + spacing;

            //while (z <= W)
            //{
            //    z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
            //    list_z_values.Add(z);
            //    z += spacing;
            //    if ((z + mergin_value) > W)
            //    {
            //        break;
            //    }
            //}
            list_z_values.Add(B - d2);
            //list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1;
            //    jn.Y = list_y_values[i];
            //    jn.Z = d1;
            //    Joints.Add(jn);
            //}
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = (B - d1);
                Joints.Add(jn);
            }
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            //for (i = 0; i < list_y_values.Count; i++)
            //{
            //    jn = new JointNode();
            //    jn.NodeNo = joint_counter++;
            //    jn.X = L1 + L2;
            //    jn.Y = list_y_values[i];
            //    jn.Z = d2;
            //    Joints.Add(jn);
            //}
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = (B - d2);
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            //for (i = 0; i < list_x_values.Count; i++)
            //{
            //    for (j = 1; j < list_z_values.Count; j++)
            //    {
            //        mbr = new Member();
            //        mbr.MemberNo = mem_count++;

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j - 1];

            //        mbr.StartNode = Joints.GetJoints(x, y, z);

            //        x = list_x_values[i];
            //        y = h1;
            //        z = list_z_values[j];

            //        mbr.EndNode = Joints.GetJoints(x, y, z);

            //        cross_girders.Add(mbr);
            //    }
            //}
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
                kStr = MyList.Get_Array_Text(list_side_mem_no);
                list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

                kStr = MyList.Get_Array_Text(list_center_mem_no);
                list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            }
            #endregion Cross Girder

            #region Left Pylons 1/1

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1;
            //    y = list_y_values[i - 1];
            //    z = d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = list_y_values[i];
            //    z = d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Left Pylons

            #region Right Pylons 2/1

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right  Pylons 1/2

            //start_mbr_no = mem_count;
            //for (i = 1; i < list_y_values.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;
            //    x = L1 + L2;
            //    y = list_y_values[i - 1];
            //    z = d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = list_y_values[i];
            //    z = d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    pylon.Add(mbr);
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Right Pylons 2/2

            start_mbr_no = mem_count;
            for (i = 1; i < list_y_values.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;
                x = L1 + L2;
                y = list_y_values[i - 1];
                z = (B - d2);

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = list_y_values[i];
                z = (B - d2);

                mbr.EndNode = Joints.GetJoints(x, y, z);

                pylon.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Pylons

            #region Left Side Cables 1
            start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    try
            //    {
            //        mbr = new Member();
            //        mbr.MemberNo = mem_count++;

            //        x = i * Nos_Side_Cables_Cross_Girder * a1;
            //        y = h1;
            //        //z = list_z_values[0];
            //        z = d1;

            //        mbr.StartNode = Joints.GetJoints(x, y, z);

            //        x = L1;
            //        y = pylon_y[i];
            //        z = d1;

            //        mbr.EndNode = Joints.GetJoints(x, y, z);

            //        cables.Add(mbr);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            //#endregion Cables

            //#region Left Side Cables 2
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 1
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[0];
            //    z = d1;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = d1;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Centre Cables 2
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = pylon_y[pylon_y.Count - i];
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Side Cables 1
            //start_mbr_no = mem_count;
            //for (i = 0; i < pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L - i * Nos_Side_Cables_Cross_Girder * a1;
            //    y = h1;
            //    //z = list_z_values[0];
            //    z = d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[i];
            //    z = d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Side Cables 2
            start_mbr_no = mem_count;
            for (i = 0; i < pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 1
            //start_mbr_no = mem_count;
            //for (i = 1; i <= pylon_y.Count; i++)
            //{
            //    mbr = new Member();
            //    mbr.MemberNo = mem_count++;

            //    x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
            //    y = h1;
            //    //z = list_z_values[0];
            //    z = d2;

            //    mbr.StartNode = Joints.GetJoints(x, y, z);

            //    x = L1 + L2;
            //    y = pylon_y[pylon_y.Count - i];
            //    z = d2;

            //    mbr.EndNode = Joints.GetJoints(x, y, z);

            //    cables.Add(mbr);
            //}

            //end_mbr_no = mem_count - 1;
            //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Right Centre Cables 2
            start_mbr_no = mem_count;
            for (i = 1; i <= pylon_y.Count; i++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                y = h1;
                //z = list_z_values[list_z_values.Count - 1];
                z = B - d2;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = pylon_y[pylon_y.Count - i];
                z = B - d2;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                cables.Add(mbr);
            }

            end_mbr_no = mem_count - 1;
            list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
            #endregion Cables

            #region Left Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
            #endregion Left Tie Beam

            #region Right Tie Beam
            //start_mbr_no = mem_count;
            //mbr = new Member();
            //mbr.MemberNo = mem_count++;

            //x = L1 + L2;
            //y = h1 + h3;
            //z = d1;

            //mbr.StartNode = Joints.GetJoints(x, y, z);

            //x = L1 + L2;
            //y = h1 + h3;
            //z = B - d1;

            //mbr.EndNode = Joints.GetJoints(x, y, z);

            //tie_beam.Add(mbr);
            //list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
            #endregion Right Tie Beam

            #region Write Data

            list_data.Add("ASTRA PLANE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");
            //foreach (var item in Joints.JointNodes) list_data.Add(item.ToString());
            #region Modified Camber
            List<double> lst_y_incr = new List<double>();
            double camber = MyList.StringToDouble(txt_camber.Text, 0.0) / 100;
            for (i = 0; i < list_x_values.Count; i++)
            {
                if (i < (list_x_values.Count / 2))
                    lst_y_incr.Add(list_x_values[i] * camber);
            }
            if (list_x_values.Count % 2 != 0)
            {
                lst_y_incr.Add(lst_y_incr[lst_y_incr.Count - 1]);
                for (i = lst_y_incr.Count - 2; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }
            else
            {

                for (i = lst_y_incr.Count - 1; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }


            int indx = -1;
            foreach (var item in Joints.JointNodes)
            {
                indx = Get_Index(list_x_values, item.X);
                if (indx != -1)
                {
                    jn = new JointNode();
                    jn.NodeNo = item.NodeNo;
                    jn.X = item.X;
                    jn.Y = item.Y + lst_y_incr[indx];
                    jn.Z = item.Z;

                    list_data.Add(jn.ToString());
                }
                else
                {
                    list_data.Add(item.ToString());
                }
            }

            #endregion Modified Camber




            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members) list_data.Add(item.ToString());
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");
            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty)
                {
                    if (!item.GroupName.Contains("_XGIRDER") && !item.GroupName.Contains("_TIEBEAM"))
                        list_data.Add(item.ToString());
                }
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8                ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                List<int> supports = new List<int>();
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    supports.Add(cables[c].StartNode.NodeNo);
                }
                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                supports.Clear();
                sup.Clear();
            }

            list_data.Add("LOAD 1 DL + SIDL");
            list_data.Add("JOINT LOAD");

            MemberGroup mgr1 = MemberGroup.Parse(list_member_group[0]);
            MemberGroup mgr2 = MemberGroup.Parse(list_member_group[1]);

            double side_span1 = 0.0;
            double centre_span = 0.0;
            //string kStr = "";
            for (i = 0; i < dgv_section_property.RowCount; i++)
            {
                try
                {
                    kStr = dgv_section_property[1, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }

            for (i = 0; i < dgv_SIDL.RowCount; i++)
            {
                try
                {
                    kStr = dgv_SIDL[0, i].Value.ToString();
                    if (kStr.Contains("1"))
                        side_span1 += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                    if (kStr.Contains("2"))
                        centre_span += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                }
                catch (Exception ex)
                {

                }
            }


            mgr1.SetMemNos();
            mgr2.SetMemNos();
            int centre_count = mgr2.MemberNos.Count;
            int side_count = mgr1.MemberNos.Count;



            list_data.Add(string.Format("{0} FY -{1:f3}", mgr1.MemberNosText, centre_span / centre_count));
            list_data.Add(string.Format("{0} FY -{1:f3}", mgr2.MemberNosText, side_span1 / side_count));

            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");


            //list_data.Add("LOAD 1");
            //list_data.Add("JOINT LOAD");


            //list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            //list_data.Add("1 TO 36 FY  -9.454");
            //list_data.Add("172 TO 207 FY  -9.454");
            //list_data.Add("343 TO 378 FY  -9.454");
            //list_data.Add("514 TO 549 FY  -9.454");
            //list_data.Add("685 TO 720 FY  -9.454");
            //list_data.Add("856 TO 891 FY  -9.454");
            //list_data.Add("1027 TO 1062 FY  -9.454");
            //list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            //list_data.Add("36 TO 135 FY  -11.970");
            //list_data.Add("207 TO 306 FY  -11.970");
            //list_data.Add("378 TO 477 FY  -11.970");
            //list_data.Add("549 TO 648  FY  -11.970");
            //list_data.Add("720 TO 819 FY  -11.970");
            //list_data.Add("891 TO 990 FY  -11.970");
            //list_data.Add("1062 TO 1161 FY  -11.970");
            //list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            //list_data.Add("136 TO 171 FY  -9.454");
            //list_data.Add("307 TO 342 FY  -9.454");
            //list_data.Add("478 TO 513 FY  -9.454");
            //list_data.Add("649 TO 684 FY  -9.454");
            //list_data.Add("820 TO 855 FY  -9.454");
            //list_data.Add("991 TO 1026 FY  -9.454");
            //list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data_2D_Right, list_data.ToArray());
            //iApp.RunExe(Input_Data);
        }

        void CreateData_Bridge_Deck()
        {


            #region Variables for Members

            JointNodeCollection Joints = new JointNodeCollection();
            JointNode jn = new JointNode();
            List<double> list_x_values = new List<double>();
            List<double> list_y_values = new List<double>();
            List<double> list_z_values = new List<double>();
            List<string> list_data = new List<string>();
            List<double> pylon_y = new List<double>();

            int i = 0; int j = 0;
            double x = 0;
            double y = h1;
            double z = 0.0;
            string kStr = "";

            List<int> list_side_mem_no = new List<int>();
            List<int> list_center_mem_no = new List<int>();
            Member mbr = new Member();
            //MemberCollection long_girders = new MemberCollection();
            //MemberCollection cross_girders = new MemberCollection();
            //MemberCollection cables = new MemberCollection();
            //MemberCollection pylon = new MemberCollection();
            //MemberCollection tie_beam = new MemberCollection();

            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            int mem_count = 1;
            int start_mbr_no = 0;
            int end_mbr_no = 0;
            int joint_counter = 1;
            double mergin_value = 0.9;
            double L = 0.0;
            #endregion Variables for Members

            x = 0;
            y = h1;
            z = 0;

            Joints.Clear();
            L = L1;

            #region X Values

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a1;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers


            x = L1 + a2;
            y = h1;
            z = 0;
            L = L1 + L2;

            #region  Stringers

            while (x <= L)
            {
                list_x_values.Add(x);
                x += a2;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            x = L + a3;
            y = h1;
            z = 0;
            L = L1 + L2 + L3;

            #region  Stringers


            while (x <= L)
            {
                list_x_values.Add(x);
                x += a3;
                if ((x + mergin_value) > L)
                {
                    list_x_values.Add(L);
                    break;
                }
            }
            #endregion  Stringers
            #endregion X Values

            #region Y Values
            list_y_values.Clear();
            list_y_values.Add(0);
            list_y_values.Add(h1);
            list_y_values.Add(h1 + h2);
            list_y_values.Add(h1 + h3);
            //list_y_values.Add(((h1 + h2) - d3));

            //pylon_y.Add(h1 + h2 - d3);

            for (i = 0; i < Total_Side_Cables; i++)
            {
                y = ((h1 + h2) - d3) - i * a4;

                if (!list_y_values.Contains(y))
                {
                    list_y_values.Add(y);
                    pylon_y.Add(y);
                }
            }
            list_y_values.Sort();
            pylon_y.Sort();
            pylon_y.Reverse();
            #endregion Y Values

            #region Z Values
            double W = 0.0;
            W = (B - d1 - d2);
            double spacing = (W) / (nl + 1);

            W += d1;
            x = 0;
            y = h1;
            list_z_values.Add(z);
            list_z_values.Add(d1);

            z = d1 + spacing;

            while (z <= W)
            {
                z = MyList.StringToDouble(z.ToString("0.000"), 0.0);
                list_z_values.Add(z);
                z += spacing;
                if ((z + mergin_value) > W)
                {
                    break;
                }
            }
            list_z_values.Add(B - d2);
            list_z_values.Add(B);
            #endregion Z Values


            #region Joint Coordinate

            Joints.Clear();
            joint_counter = 1;

            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 0; j < list_x_values.Count; j++)
                {
                    jn = new JointNode();
                    jn.NodeNo = joint_counter++;
                    jn.X = list_x_values[j];
                    jn.Y = h1;
                    jn.Z = list_z_values[i];
                    Joints.Add(jn);
                }
            }
            #endregion Joint Coordinate


            #region Pylon 1/1  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = d1;
                Joints.Add(jn);
            }
            #endregion Pylon 1/1 PYLON AT 121 MTR

            #region Pylon 1/2  PYLON AT 121 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1;
                jn.Y = list_y_values[i];
                jn.Z = (B - d1);
                Joints.Add(jn);
            }
            #endregion Pylon 1/2 PYLON AT 121 MTR

            #region Pylon 2/1 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = d2;
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR

            #region Pylon 2/2 PYLON AT 471 MTR
            for (i = 0; i < list_y_values.Count; i++)
            {
                jn = new JointNode();
                jn.NodeNo = joint_counter++;
                jn.X = L1 + L2;
                jn.Y = list_y_values[i];
                jn.Z = (B - d2);
                Joints.Add(jn);
            }
            #endregion Pylon PYLON AT 471 MTR



            #region Write MEMBER CONNECTIVITY

            #endregion Write MEMBER CONNECTIVITY



            long_girders.Clear();
            cross_girders.Clear();
            cables.Clear();
            pylon.Clear();
            tie_beam.Clear();

            #region Long Girders
            for (i = 0; i < list_z_values.Count; i++)
            {
                for (j = 1; j < list_x_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = list_x_values[j - 1];
                    y = h1;
                    z = list_z_values[i];


                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[j];
                    y = h1;
                    z = list_z_values[i];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    long_girders.Add(mbr);
                }
            }


            if (long_girders.Count > 0)
            {
                foreach (var item in long_girders.Members)
                {
                    if (item.Length == a2)
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            list_member_group.Clear();
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_LGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_LGIRDER2 {0}", kStr));

            #endregion Long Girders

            #region Cross Girder
            for (i = 0; i < list_x_values.Count; i++)
            {
                for (j = 1; j < list_z_values.Count; j++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j - 1];

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = list_x_values[i];
                    y = h1;
                    z = list_z_values[j];

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cross_girders.Add(mbr);
                }
            }
            list_side_mem_no.Clear();
            list_center_mem_no.Clear();
            if (cross_girders.Count > 0)
            {
                foreach (var item in cross_girders.Members)
                {
                    if (item.EndNode.X >= L1 && item.EndNode.X <= (L1 + L2))
                    {
                        list_center_mem_no.Add(item.MemberNo);
                    }
                    else
                    {
                        list_side_mem_no.Add(item.MemberNo);
                    }
                }
            }
            kStr = MyList.Get_Array_Text(list_side_mem_no);
            list_member_group.Add(string.Format("_XGIRDER1 {0}", kStr));

            kStr = MyList.Get_Array_Text(list_center_mem_no);
            list_member_group.Add(string.Format("_XGIRDER2 {0}", kStr));
            #endregion Cross Girder
            if (false)
            {
                #region Left Pylons 1/1

                start_mbr_no = mem_count;
                for (i = 1; i < list_y_values.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = L1;
                    y = list_y_values[i - 1];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = list_y_values[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    pylon.Add(mbr);
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_LPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Left Pylons
                //#region Right Pylons 2/1

                //start_mbr_no = mem_count;
                for (i = 1; i < list_y_values.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = L1;
                    y = list_y_values[i - 1];
                    z = (B - d2);

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = list_y_values[i];
                    z = (B - d2);

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    pylon.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_RPYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
                list_member_group.Add(string.Format("_PYLON1 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Pylons

                #region Right  Pylons 1/2

                start_mbr_no = mem_count;
                for (i = 1; i < list_y_values.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = L1 + L2;
                    y = list_y_values[i - 1];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = list_y_values[i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    pylon.Add(mbr);
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_LPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Pylons
                //#region Right Pylons 2/2

                //start_mbr_no = mem_count;
                for (i = 1; i < list_y_values.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;
                    x = L1 + L2;
                    y = list_y_values[i - 1];
                    z = (B - d2);

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = list_y_values[i];
                    z = (B - d2);

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    pylon.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_RPYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
                list_member_group.Add(string.Format("_PYLON2 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Pylons



                #region Left Side Cables 1
                start_mbr_no = mem_count;
                for (i = 0; i < pylon_y.Count; i++)
                {
                    try
                    {
                        mbr = new Member();
                        mbr.MemberNo = mem_count++;

                        x = i * Nos_Side_Cables_Cross_Girder * a1;
                        y = h1;
                        //z = list_z_values[0];
                        z = d1;

                        mbr.StartNode = Joints.GetJoints(x, y, z);

                        x = L1;
                        y = pylon_y[i];
                        z = d1;

                        mbr.EndNode = Joints.GetJoints(x, y, z);

                        cables.Add(mbr);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_LCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Cables
                //#region Left Side Cables 2
                //start_mbr_no = mem_count;
                for (i = 0; i < pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[list_z_values.Count - 1];
                    z = B - d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[i];
                    z = B - d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                list_member_group.Add(string.Format("_CABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Cables

                #region Left Centre Cables 1
                start_mbr_no = mem_count;
                for (i = 1; i <= pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                    y = h1;
                    //z = list_z_values[0];
                    z = d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[pylon_y.Count - i];
                    z = d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_LCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Cables
                //#region Left Centre Cables 2
                //start_mbr_no = mem_count;
                for (i = 1; i <= pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L1 + i * Nos_Center_Cables_Cross_Girder * a2;
                    y = h1;
                    //z = list_z_values[list_z_values.Count - 1];
                    z = B - d1;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1;
                    y = pylon_y[pylon_y.Count - i];
                    z = B - d1;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                list_member_group.Add(string.Format("_CABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Cables


                #region Right Side Cables 1
                start_mbr_no = mem_count;
                for (i = 0; i < pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[0];
                    z = d2;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = pylon_y[i];
                    z = d2;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_RCABLE1 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Cables
                //#region Right Side Cables 2
                //start_mbr_no = mem_count;
                for (i = 0; i < pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L - i * Nos_Side_Cables_Cross_Girder * a1;
                    y = h1;
                    //z = list_z_values[list_z_values.Count - 1];
                    z = B - d2;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = pylon_y[i];
                    z = B - d2;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                list_member_group.Add(string.Format("_CABLE3 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Cables

                #region Right Centre Cables 1
                start_mbr_no = mem_count;
                for (i = 1; i <= pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                    y = h1;
                    //z = list_z_values[0];
                    z = d2;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = pylon_y[pylon_y.Count - i];
                    z = d2;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                //end_mbr_no = mem_count - 1;
                //list_member_group.Add(string.Format("_RCABLE2 {0} TO {1}", start_mbr_no, end_mbr_no));
                //#endregion Cables
                //#region Right Centre Cables 2
                //start_mbr_no = mem_count;
                for (i = 1; i <= pylon_y.Count; i++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_count++;

                    x = L1 + L2 - i * Nos_Center_Cables_Cross_Girder * a2;
                    y = h1;
                    //z = list_z_values[list_z_values.Count - 1];
                    z = B - d2;

                    mbr.StartNode = Joints.GetJoints(x, y, z);

                    x = L1 + L2;
                    y = pylon_y[pylon_y.Count - i];
                    z = B - d2;

                    mbr.EndNode = Joints.GetJoints(x, y, z);

                    cables.Add(mbr);
                }

                end_mbr_no = mem_count - 1;
                list_member_group.Add(string.Format("_CABLE4 {0} TO {1}", start_mbr_no, end_mbr_no));
                #endregion Cables

                #region Left Tie Beam
                start_mbr_no = mem_count;
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1;
                y = h1 + h3;
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1;
                y = h1 + h3;
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                tie_beam.Add(mbr);
                list_member_group.Add(string.Format("_TIEBEAM1 {0}", start_mbr_no));
                #endregion Left Tie Beam

                #region Right Tie Beam
                start_mbr_no = mem_count;
                mbr = new Member();
                mbr.MemberNo = mem_count++;

                x = L1 + L2;
                y = h1 + h3;
                z = d1;

                mbr.StartNode = Joints.GetJoints(x, y, z);

                x = L1 + L2;
                y = h1 + h3;
                z = B - d1;

                mbr.EndNode = Joints.GetJoints(x, y, z);

                tie_beam.Add(mbr);
                list_member_group.Add(string.Format("_TIEBEAM2 {0}", start_mbr_no));
                #endregion Right Tie Beam
            }
            #region Write Data

            list_data.Add("ASTRA SPACE CABLES STAYED BRIDGE");
            list_data.Add("UNIT TON MET");
            list_data.Add("JOINT COORDINATES");
            //foreach (var item in Joints.JointNodes) list_data.Add(item.ToString());
            #region Modified Camber
            List<double> lst_y_incr = new List<double>();
            double camber = MyList.StringToDouble(txt_camber.Text, 0.0) / 100;
            for (i = 0; i < list_x_values.Count; i++)
            {
                if (i < (list_x_values.Count / 2))
                    lst_y_incr.Add(list_x_values[i] * camber);
            }
            if (list_x_values.Count % 2 != 0)
            {
                lst_y_incr.Add(lst_y_incr[lst_y_incr.Count - 1]);
                for (i = lst_y_incr.Count - 2; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }
            else
            {

                for (i = lst_y_incr.Count - 1; i >= 0; i--)
                {
                    lst_y_incr.Add(lst_y_incr[i]);
                }
            }


            int indx = -1;
            foreach (var item in Joints.JointNodes)
            {
                indx = Get_Index(list_x_values, item.X);
                if (indx != -1)
                {
                    jn = new JointNode();
                    jn.NodeNo = item.NodeNo;
                    jn.X = item.X;
                    jn.Y = item.Y + lst_y_incr[indx];
                    jn.Z = item.Z;

                    list_data.Add(jn.ToString());
                }
                else
                {
                    list_data.Add(item.ToString());
                }
            }

            #endregion Modified Camber



            //list_data.Add("MEMBER CONNECTIVITY INCIDENCE");
            list_data.Add("MEMBER INCIDENCE");
            list_data.Add("*******     LONG GIRDERS    *******");
            foreach (var item in long_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CROSS GIRDERS    *******");
            foreach (var item in cross_girders.Members) list_data.Add(item.ToString());
            list_data.Add("*******    PYLONS    *******");
            foreach (var item in pylon.Members) list_data.Add(item.ToString());
            list_data.Add("*******    CABLES   *********");
            foreach (var item in cables.Members) list_data.Add(item.ToString());
            list_data.Add("*******  TIE BEAMS    *******");
            foreach (var item in tie_beam.Members) list_data.Add(item.ToString());
            list_data.Add("START GROUP DEFINITION");
            foreach (var item in list_member_group) list_data.Add(item.ToString());
            list_data.Add("END GROUP DEFINITION");
            //Set_Section_Properties();
            list_data.Add("MEMBER PROPERTY");
            if (SectionProperty.Count > 0)
                foreach (var item in SectionProperty) list_data.Add(item.ToString());
            else
            {

                list_data.Add("_LGIRDER1   PRI    AX 0.22410   IX 0.20220   IY 0.20810   IZ 0.41030");
                list_data.Add("_LGIRDER2   PRI    AX 0.21130   IX 0.18810   IY 0.17480   IZ 0.36290");
                list_data.Add("_XGIRDER1   PRI    AX 0.03951   IX 0.01291   IY 0.00021   IZ 0.01312");
                list_data.Add("_XGIRDER2   PRI    AX 0.03951   IX 0.01301   IY 0.00033   IZ 0.01334");
                list_data.Add("_CABLE1   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE2   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE3   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_CABLE4   PRI    AX 0.01767   IX 0.00002   IY 0.00002   IZ 0.00005");
                list_data.Add("_PYLON1   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_PYLON2   PRI    AX 6.20000   IX 16.52000   IY 3.86100   IZ 20.38100");
                list_data.Add("_TIEBEAM1   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
                list_data.Add("_TIEBEAM2   PRI    AX 6.00000   IX 4.50000   IY 2.00000   IZ 6.50000");
            }
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE1");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE2");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE3");
            list_data.Add("MEMBER CABLE");
            list_data.Add("_CABLE4");
            list_data.Add("CONSTANT");
            list_data.Add("E  2.110E8                ALL");
            list_data.Add("DENSITY STEEL ALL");
            list_data.Add("POISSON STEEL ALL");
            list_data.Add("SUPPORT");
            // list_data.Add("172 170 FIXED BUT MZ");
            //list_data.Add("173 441 FIXED BUT MX MZ");


            int nd1 = 0, nd2 = 0, nd3 = 0, nd4 = 0;

            if (cables.Count != 0)
            {
                List<int> supports = new List<int>();
                for (int c = 0; c < cables.Count; c++)
                {
                    if (cables[c].StartNode.X == 0)
                    {
                        if (nd1 == 0)
                            nd1 = cables[c].StartNode.NodeNo;
                        else if (nd2 == 0)
                            nd2 = cables[c].StartNode.NodeNo;
                    }
                    if (cables[c].StartNode.X == L)
                    {
                        if (nd3 == 0)
                            nd3 = cables[c].StartNode.NodeNo;
                        else if (nd4 == 0)
                            nd4 = cables[c].StartNode.NodeNo;
                    }
                    supports.Add(cables[c].StartNode.NodeNo);
                }
                supports.Sort();
                List<int> sup = new List<int>();

                for (int c = 0; c < supports.Count; c++)
                {
                    if (supports[c] == nd1)
                    {
                        list_data.Add(nd1 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd2)
                    {
                        list_data.Add(nd2 + " FIXED BUT MZ");
                    }
                    else if (supports[c] == nd3)
                    {
                        list_data.Add(nd3 + " FIXED BUT MX MZ");
                    }
                    else if (supports[c] == nd4)
                    {
                        list_data.Add(nd4 + " FIXED BUT MX MZ");
                    }
                    else
                    {
                        sup.Add(supports[c]);
                        if (sup.Count > 1)
                        {
                            list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                            sup.Clear();
                        }
                    }
                }
                if (sup.Count > 0)
                    list_data.Add(MyList.Get_Array_Text(sup) + " PINNED");
                supports.Clear();
                sup.Clear();
            }

            list_data.Add("LOAD                1 ");
            list_data.Add("JOINT LOAD");
            //list_data.Add("172   TO  342  856   TO  1026      FY  -34.0");
            //list_data.Add("343   TO  855      FY  -17.0");
            list_data.Add("LOAD                1");
            list_data.Add("JOINT LOAD");
            list_data.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
            list_data.Add("1 TO 36 FY  -9.454");
            list_data.Add("172 TO 207 FY  -9.454");
            list_data.Add("343 TO 378 FY  -9.454");
            list_data.Add("514 TO 549 FY  -9.454");
            list_data.Add("685 TO 720 FY  -9.454");
            list_data.Add("856 TO 891 FY  -9.454");
            list_data.Add("1027 TO 1062 FY  -9.454");
            list_data.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
            list_data.Add("36 TO 135 FY  -11.970");
            list_data.Add("207 TO 306 FY  -11.970");
            list_data.Add("378 TO 477 FY  -11.970");
            list_data.Add("549 TO 648  FY  -11.970");
            list_data.Add("720 TO 819 FY  -11.970");
            list_data.Add("891 TO 990 FY  -11.970");
            list_data.Add("1062 TO 1161 FY  -11.970");
            list_data.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
            list_data.Add("136 TO 171 FY  -9.454");
            list_data.Add("307 TO 342 FY  -9.454");
            list_data.Add("478 TO 513 FY  -9.454");
            list_data.Add("649 TO 684 FY  -9.454");
            list_data.Add("820 TO 855 FY  -9.454");
            list_data.Add("991 TO 1026 FY  -9.454");
            list_data.Add("1162 TO 1197 FY  -9.454");
            //list_data.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list_data.Add("TYPE 1 CLA 1.179");
            //list_data.Add("TYPE 2 CLB 1.188");
            //list_data.Add("TYPE 3 A70RT 1.10");
            //list_data.Add("TYPE 4 CLAR 1.179");
            //list_data.Add("TYPE 5 A70RR 1.188");
            //list_data.Add("TYPE 6 IRC24RTRACK 1.188");
            //list_data.Add("TYPE 7 RAILBG 1.25");
            //list_data.Add("LOAD GENERATION 100");
            //list_data.Add("TYPE 2 60.000 0 1.500 XINC 0.5");
            //list_data.Add("TYPE 2 60.000 0 4.500 XINC 0.5");
            list_data.Add("PERFORM ANALYSIS");
            //FINISH



            list_data.Add("FINISH");


            #endregion Write Data
            //iApp.Write_LiveLoad_LL_TXT(user_path, true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data_Bridge_Deck, list_data.ToArray());
            //iApp.RunExe(Input_Data);
        }


        public void WriteData()
        {
            iApp.Save_Form_Record(this, user_path);

            try
            {
                List<string> list = new List<string>();
                string kStr = "";
                string k2 = "";

                #region General Input
                //list.Add(string.Format("",));
                list.Add(string.Format("GENERAL INPUT DATA"));
                list.Add(string.Format("INPUT_DATA={0}", Path.GetFileName(Input_Data)));
                list.Add(string.Format("L1={0}", L1));
                list.Add(string.Format("L2={0}", L2));
                list.Add(string.Format("L3={0}", L3));
                list.Add(string.Format("B={0}", B));
                list.Add(string.Format("A1={0}", a1));
                list.Add(string.Format("A2={0}", a2));
                list.Add(string.Format("A3={0}", a3));
                list.Add(string.Format("h1={0}", h1));
                list.Add(string.Format("h2={0}", h2));
                list.Add(string.Format("h3={0}", h3));
                list.Add(string.Format("d3={0}", d3));
                list.Add(string.Format("a4={0}", a4));
                list.Add(string.Format("sno={0}", Nos_Side_Cables_Cross_Girder));
                list.Add(string.Format("cno={0}", Nos_Center_Cables_Cross_Girder));
                list.Add(string.Format("d1={0}", d1));
                list.Add(string.Format("d2={0}", d2));
                list.Add(string.Format("nl={0}", nl));
                list.Add(string.Format("nc={0}", Total_Side_Cables));
                #endregion General Input

                #region SECTION PROPERTIES
                list.Add(string.Format("SECTION PROPERTIES"));
                foreach (var item in SectionProperty)
                {
                    list.Add(item.ToString("ALL"));
                }
                //for (int i = 0; i < dgv_section_property.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dgv_section_property.Columns.Count; j++)
                //    {
                //        //LOAD_CSB.TXT

                //        k2 = (dgv_section_property[j, i].Value.ToString() == "") ? "0.0" : dgv_section_property[j, i].Value.ToString();
                //        k2 = k2.Trim().TrimEnd().TrimStart();

                //        if (k2 == "")
                //            k2 = "0.0";
                //        if (dgv_section_property[j, i].Value != null)
                //            kStr += string.Format("{0,-16}", k2);
                //    }
                //    list.Add(kStr);
                //    kStr = "";
                //}
                #endregion SECTION PROPERTIES

                #region ANALYSIS RESULT
                list.Add(string.Format("ANALYSIS RESULT"));
                for (int i = 0; i < dgv_member_Result.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv_member_Result.Columns.Count; j++)
                    {
                        //LOAD_CSB.TXT

                        k2 = (dgv_member_Result[j, i].Value.ToString() == "") ? "0.0" : dgv_member_Result[j, i].Value.ToString();
                        k2 = k2.Trim().TrimEnd().TrimStart();
                        if (k2 == "")
                            k2 = "0.0";
                        if (dgv_member_Result[j, i].Value != null)
                            kStr += string.Format("{0,-16}", k2);
                    }
                    list.Add(kStr);
                    kStr = "";
                }
                #endregion ANALYSIS RESULT

                #region CABLE ANALYSIS
                list.Add(string.Format("CABLE ANALYSIS"));
                for (int i = 0; i < dgv_cable_design.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv_cable_design.Columns.Count; j++)
                    {
                        //LOAD_CSB.TXT
                        try
                        {
                            k2 = (dgv_cable_design[j, i].Value.ToString() == "") ? "0.0" : dgv_cable_design[j, i].Value.ToString();
                            k2 = k2.Trim().TrimEnd().TrimStart();
                            if (k2 == "")
                                k2 = "0.0";
                            if (dgv_cable_design[j, i].Value != null)
                                kStr += string.Format("{0,-16}", k2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    list.Add(kStr);
                    kStr = "";
                }
                #endregion CABLE ANALYSIS
                #region SIDL
                list.Add(string.Format("SIDL"));
                //list.RemoveAt(list.Count - 1);
                for (int i = 0; i < dgv_SIDL.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgv_SIDL.Columns.Count; j++)
                    {
                        //LOAD_CSB.TXT
                        try
                        {
                            if (dgv_SIDL[j, i].Value == null)
                                dgv_SIDL[j, i].Value = "";


                            k2 = (dgv_SIDL[j, i].Value.ToString() == "") ? "0.0" : dgv_SIDL[j, i].Value.ToString();
                            k2 = k2.Trim().TrimEnd().TrimStart();
                            if (k2 == "")
                                k2 = "0.0";
                            //if (dgv_SIDL[j, i].Value != null)
                            kStr += string.Format("{0,-16}", k2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    list.Add(kStr);
                    kStr = "";
                }
                #endregion SIDL

                File.WriteAllLines(CSB_DATA_File, list.ToArray());

            }
            catch (Exception ex) { }
        }
        public void ReadData()
        {
            if (!File.Exists(CSB_DATA_File))
            {
                string sf = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Cable Stayed Bridge\" + Path.GetFileName(CSB_DATA_File));

                if (File.Exists(sf))
                    File.Copy(sf, CSB_DATA_File);
            }

            if (!File.Exists(CSB_DATA_File)) return;
            List<string> list = new List<string>(File.ReadAllLines(CSB_DATA_File));
            string kStr = "";
            string k2 = "";

            MyList mlist = null;

            bool flag_gen = false;
            bool flag_sec = false;
            bool flag_ana_res = false;
            bool flag_CBL_ana = false;
            bool flag_SIDL = false;
            for (int i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i].Trim().TrimStart().TrimEnd().ToUpper());
                mlist = new MyList(kStr, '=');


                if (mlist.StringList[0].StartsWith("GEN") && !IsCreateData)
                {
                    flag_gen = true;
                    flag_sec = false;
                    flag_ana_res = false;
                    flag_CBL_ana = false;
                    flag_SIDL = false;
                    continue;
                }
                else if (mlist.StringList[0].StartsWith("SECT"))
                {
                    flag_gen = false;
                    flag_sec = true;
                    flag_ana_res = false;
                    flag_CBL_ana = false;
                    SectionProperty.Clear();

                    dgv_section_property.Rows.Clear();
                    flag_SIDL = false;
                    continue;
                }
                else if (mlist.StringList[0].StartsWith("ANALY"))
                {
                    flag_gen = false;
                    flag_sec = false;
                    flag_ana_res = true;
                    flag_CBL_ana = false;
                    flag_SIDL = false;
                    dgv_member_Result.Rows.Clear();
                    continue;
                }
                else if (mlist.StringList[0].StartsWith("CABLE ANALY"))
                {
                    flag_gen = false;
                    flag_sec = false;
                    flag_ana_res = false;
                    flag_CBL_ana = true;
                    flag_SIDL = false;
                    dgv_cable_design.Rows.Clear();
                }
                else if (mlist.StringList[0].StartsWith("SIDL"))
                {
                    flag_gen = false;
                    flag_sec = false;
                    flag_ana_res = false;
                    flag_CBL_ana = false;
                    flag_SIDL = true;
                    dgv_SIDL.Rows.Clear();
                }
                #region General Data
                if (flag_gen)
                {
                    switch (mlist.StringList[0])
                    {
                        case "INPUT_DATA":
                            Input_Data = Path.Combine(Path.GetDirectoryName(CSB_DATA_File), mlist.StringList[1]);
                            break;
                        case "L1":
                            L1 = mlist.GetDouble(1);
                            break;
                        case "L2":
                            L2 = mlist.GetDouble(1);
                            break;
                        case "L3":
                            L3 = mlist.GetDouble(1);
                            break;
                        case "B":
                            B = mlist.GetDouble(1);
                            break;
                        case "A1":
                            a1 = mlist.GetDouble(1);
                            break;
                        case "A2":
                            a2 = mlist.GetDouble(1);
                            break;
                        case "A3":
                            a3 = mlist.GetDouble(1);
                            break;
                        case "A4":
                            a4 = mlist.GetDouble(1);
                            break;
                        case "H1":
                            h1 = mlist.GetDouble(1);
                            break;
                        case "H2":
                            h2 = mlist.GetDouble(1);
                            break;
                        case "H3":
                            h3 = mlist.GetDouble(1);
                            break;
                        case "SNO":
                            Nos_Side_Cables_Cross_Girder = (int)mlist.GetDouble(1);
                            break;
                        case "CNO":
                            Nos_Center_Cables_Cross_Girder = (int)mlist.GetDouble(1);
                            break;
                        case "D1":
                            d1 = mlist.GetDouble(1);
                            break;
                        case "D2":
                            d2 = mlist.GetDouble(1);
                            break;
                        case "D3":
                            d3 = mlist.GetDouble(1);
                            break;
                        case "NL":
                            nl = (int)mlist.GetDouble(1);
                            break;
                        case "NC":
                            Total_Side_Cables = (int)mlist.GetDouble(1);
                            break;
                    }
                }
                #endregion General Data

                #region SECTION PROPERTIES
                if (flag_sec)
                {
                    //mlist = new MyList(kStr, ' ');
                    //if (mlist.Count == dgv_section_property.Columns.Count)
                    //    dgv_section_property.Rows.Add(mlist.StringList.ToArray());
                    MemberSectionProperty msec = MemberSectionProperty.Parse(kStr);
                    if (msec != null)
                    {
                        SectionProperty.Add(msec);
                        dgv_section_property.Rows.Add(msec.ToArray());
                    }

                }
                #endregion SECTION PROPERTIES

                #region Analysis Result
                if (flag_ana_res)
                {
                    mlist = new MyList(kStr, ' ');
                    if (mlist.Count == dgv_member_Result.Columns.Count)
                        dgv_member_Result.Rows.Add(mlist.StringList.ToArray());
                }
                #endregion Analysis Result


                #region SIDL
                if (flag_SIDL)
                {
                    mlist = new MyList(kStr, ' ');
                    if (mlist.Count == dgv_SIDL.Columns.Count)
                        dgv_SIDL.Rows.Add(mlist.StringList.ToArray());
                }
                #endregion SIDL


                #region Cable Analysis Result
                if (flag_CBL_ana)
                {
                    mlist = new MyList(kStr, ' ');
                    if (mlist.Count == dgv_cable_design.Columns.Count)
                        dgv_cable_design.Rows.Add(mlist.StringList.ToArray());
                }
                #endregion Cable Analysis Result
            }
        }

        void Calculate_Total_Weight()
        {
            try
            {
                double side_span1 = 0.0;
                double centre_span = 0.0;
                string kStr = "";
                for (int i = 0; i < dgv_section_property.RowCount; i++)
                {
                    try
                    {
                        kStr = dgv_section_property[1, i].Value.ToString();
                        if (kStr.Contains("1"))
                            side_span1 += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                        if (kStr.Contains("2"))
                            centre_span += MyList.StringToDouble(dgv_section_property[13, i].Value.ToString(), 0.0);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                for (int i = 0; i < dgv_SIDL.RowCount; i++)
                {
                    try
                    {
                        kStr = dgv_SIDL[0, i].Value.ToString();
                        if (kStr.Contains("1"))
                            side_span1 += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                        if (kStr.Contains("2"))
                            centre_span += MyList.StringToDouble(dgv_SIDL[7, i].Value.ToString(), 0.0);
                    }
                    catch (Exception ex)
                    {

                    }
                }



                int centre_count = 0;
                int side_count = 0;

                MemberGroup mgr1 = null;
                MemberGroup mgr2 = null;

                MemberGroup cbl1 = null;

                MemberGroup cbl2 = null;

                MemberGroup cbl3 = null;

                MemberGroup cbl4 = null;

                List<int> spa1 = new List<int>();
                List<int> spa2 = new List<int>();

                foreach (var item in Bridge_Analysis.Analysis.MemberGroups.GroupCollection)
                {

                    if (item.GroupName.ToUpper() == "_LGIRDER1")
                    {
                        //side_count = MyList.Get_Array_Intiger(item.MemberNosText).Count;
                        mgr1 = item;
                    }
                    if (item.GroupName.ToUpper() == "_LGIRDER2")
                    {
                        //centre_count = MyList.Get_Array_Intiger(item.MemberNosText).Count;
                        mgr2 = item;
                    }

                    if (item.GroupName.ToUpper() == "_CABLE1")
                    {
                        //side_count = MyList.Get_Array_Intiger(item.MemberNosText).Count;
                        cbl1 = item;

                        spa1.AddRange(MyList.Get_Array_Intiger(cbl1.MemberNosText));
                    }
                    if (item.GroupName.ToUpper() == "_CABLE2")
                    {
                        cbl2 = item;
                        spa2.AddRange(MyList.Get_Array_Intiger(cbl2.MemberNosText));
                    }
                    if (item.GroupName.ToUpper() == "_CABLE3")
                    {
                        cbl3 = item;
                        spa1.AddRange(MyList.Get_Array_Intiger(cbl3.MemberNosText));
                    }
                    if (item.GroupName.ToUpper() == "_CABLE4")
                    {
                        cbl4 = item;
                        spa2.AddRange(MyList.Get_Array_Intiger(cbl4.MemberNosText));
                    }
                }


                List<int> side_jnts = new List<int>();

                List<int> cen_jnts = new List<int>();

                foreach (var item in spa1)
                {
                    side_jnts.Add(Bridge_Analysis.Analysis.Members.GetMember(item).StartNode.NodeNo);
                }

                foreach (var item in spa2)
                {
                    cen_jnts.Add(Bridge_Analysis.Analysis.Members.GetMember(item).StartNode.NodeNo);
                }


                centre_span = centre_span * Weight_Factor;
                side_span1 = side_span1 * Weight_Factor;

                side_span1 = side_span1 / 2.0;


                side_count = side_jnts.Count;
                centre_count = cen_jnts.Count;


                double side_span2 = side_span1;
                txt_sec_total_weight_centre_span.Text = centre_span.ToString("E3");
                txt_sec_total_weight_Side_span1.Text = (side_span1).ToString("E3");
                txt_sec_total_weight_side_span2.Text = (side_span2).ToString("E3");


                List<string> lis = new List<string>();

                //lis.Add("LOAD 1 CABLE TENSION");
                //lis.Add("MEMBER LOAD");


                //lis.Add(string.Format("{0} UNI X  {1:f3}", cbl1.MemberNosText, 10.0));
                //lis.Add(string.Format("{0} UNI X  {1:f3}", cbl2.MemberNosText, 10.0));
                //lis.Add(string.Format("{0} UNI X  {1:f3}", cbl3.MemberNosText, 10.0));
                //lis.Add(string.Format("{0} UNI X  {1:f3}", cbl4.MemberNosText, 10.0));



                lis.Add("LOAD 1 DL+SIDL");
                lis.Add("JOINT LOAD");

                foreach (var item in side_jnts)
                {
                    lis.Add(string.Format("{0} FY  -{1:f3}", item, side_span1 / side_count));

                }
                foreach (var item in cen_jnts)
                {
                    lis.Add(string.Format("{0} FY  -{1:f3}", item, centre_span / side_count));
                }
                //lis.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(side_jnts), side_span1 / side_count));
                //lis.Add(string.Format("{0} FY  -{1:f3}", MyList.Get_Array_Text(cen_jnts), centre_span / centre_count));


                ////Side Span1
                //lis.Add("** Total weight of Side Span1 distributed over 490 Joint nodes");
                //lis.Add(string.Format("1 TO 36 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("172 TO 207 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("343 TO 378 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("514 TO 549 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("685 TO 720 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("856 TO 891 FY  -{0:f3}", side_span1 / side_count));
                //lis.Add(string.Format("1027 TO 1062 FY  -{0:f3}", side_span1 / side_count));

                ////Centre Span
                //lis.Add("** Total weight of Centre Span distributed over 700 Joint nodes");
                //lis.Add(string.Format("36 TO 135 FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("207 TO 306 FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("378 TO 477 FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("549 TO 648  FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("720 TO 819 FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("891 TO 990 FY  -{0:f3}", centre_span / centre_count));
                //lis.Add(string.Format("1062 TO 1161 FY  -{0:f3}", centre_span / centre_count));

                ////Side Span2
                //lis.Add("** Total weight of Side Span2 distributed over 490 Joint nodes");
                //lis.Add(string.Format("136 TO 171 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("307 TO 342 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("478 TO 513 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("649 TO 684 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("820 TO 855 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("991 TO 1026 FY  -{0:f3}", side_span2 / side_count));
                //lis.Add(string.Format("1162 TO 1197 FY  -{0:f3}", side_span2 / side_count));
                txt_member_load.Lines = lis.ToArray();
            }
            catch (Exception ex) { }
        }

        #region SECTION PROPERTIES
        void Set_Default_Section_Property()
        {
            //dgv_section_property.Rows.Add(1, "_LGIRDER1", "PRIS", 1.2, 2.1, 0, 0);
            //dgv_section_property.Rows.Add(1, "_LGIRDER2", "PRIS", 1.0, 2.0, 0, 0);
            //dgv_section_property.Rows.Add(1, "_XGIRDER1", "PRIS", 1.2, 2.1, 0, 0);
            //dgv_section_property.Rows.Add(1, "_XGIRDER2", "PRIS", 1.0, 2.0, 0, 0);
            //dgv_section_property.Rows.Add(1, "_PYLON1", "PRIS", 2.2, 5.0, 0, 0.5);
            ////dgv_section_property.Rows.Add(1, "_LPYLON2", "PRIS", 2.2, 5.0, 0, 0.5);
            //dgv_section_property.Rows.Add(1, "_PYLON2", "PRIS", 2.2, 5.0, 0, 0.5);
            ////dgv_section_property.Rows.Add(1, "_RPYLON2", "PRIS", 2.2, 5.0, 0, 0.5);
            //dgv_section_property.Rows.Add(1, "_CABLE1", "PRIS", 0.0, 0.0, 0.15, 0.0);
            //dgv_section_property.Rows.Add(1, "_CABLE2", "PRIS", 0.0, 0.0, 0.15, 0.0);
            ////dgv_section_property.Rows.Add(1, "_LCABLE3", "PRIS", 0.0, 0.0, 0.15, 0.0);
            ////dgv_section_property.Rows.Add(1, "_LCABLE4", "PRIS", 0.0, 0.0, 0.15, 0.0);
            //dgv_section_property.Rows.Add(1, "_CABLE3", "PRIS", 0.0, 0.0, 0.15, 0.0);
            //dgv_section_property.Rows.Add(1, "_CABLE4", "PRIS", 0.0, 0.0, 0.15, 0.0);
            ////dgv_section_property.Rows.Add(1, "_RCABLE3", "PRIS", 0.0, 0.0, 0.15, 0.0);
            ////dgv_section_property.Rows.Add(1, "_RCABLE4", "PRIS", 0.0, 0.0, 0.15, 0.0);
            //dgv_section_property.Rows.Add(1, "_TIEBEAM1", "PRIS", 2.0, 3.0, 0.0, 0.0);
            //dgv_section_property.Rows.Add(1, "_TIEBEAM2", "PRIS", 2.0, 3.0, 0.0, 0.0);
            //Format_Section_Properties_Grid();
        }
        void Format_Section_Properties_Grid()
        {
            MemberSectionProperty ss = new MemberSectionProperty();
            for (int i = 0; i < dgv_section_property.RowCount; i++)
            {
                try
                {
                    ss.GroupName = dgv_section_property[1, i].Value.ToString();
                    ss.PropertyName = dgv_section_property[2, i].Value.ToString();
                    ss.Breadth = MyList.StringToDouble(dgv_section_property[3, i].Value.ToString(), 0.0);
                    if (ss.Breadth != 0.0)
                        ss.Diameter = 0.0;
                    ss.Depth = MyList.StringToDouble(dgv_section_property[4, i].Value.ToString(), 0.0);
                    if (ss.Depth != 0.0)
                        ss.Diameter = 0.0;



                    ss.Diameter = MyList.StringToDouble(dgv_section_property[5, i].Value.ToString(), 0.0);
                    if (ss.Diameter != 0.0)
                        ss.Breadth = ss.Depth = 0.0;


                    ss.Thickness = MyList.StringToDouble(dgv_section_property[6, i].Value.ToString(), 0.0);

                    dgv_section_property[0, i].Value = i + 1;
                    dgv_section_property[1, i].Value = ss.GroupName.ToUpper();
                    dgv_section_property[2, i].Value = ss.PropertyName.ToUpper();
                    dgv_section_property[3, i].Value = ss.Breadth.ToString("f3");
                    dgv_section_property[4, i].Value = ss.Depth.ToString("f3");
                    dgv_section_property[5, i].Value = ss.Diameter.ToString("f3");
                    dgv_section_property[6, i].Value = ss.Thickness.ToString("f3");
                    dgv_section_property[7, i].Value = ss.AX_Area.ToString("f5");
                    dgv_section_property[8, i].Value = ss.IX.ToString("E3");
                    dgv_section_property[9, i].Value = ss.IY.ToString("E3");
                    dgv_section_property[10, i].Value = ss.IZ.ToString("E3");
                }
                catch (Exception ex)
                {

                }
            }
        }
        void Set_Section_Properties()
        {
            SectionProperty.Clear();
            MemberSectionProperty ss = null;
            for (int i = 0; i < dgv_section_property.RowCount; i++)
            {
                try
                {
                    ss = new MemberSectionProperty();
                    ss.GroupName = dgv_section_property[1, i].Value.ToString();
                    ss.Length = MyList.StringToDouble(dgv_section_property[2, i].Value.ToString(), 0.0);
                    ss.Breadth = MyList.StringToDouble(dgv_section_property[3, i].Value.ToString(), 0.0);
                    ss.Depth = MyList.StringToDouble(dgv_section_property[4, i].Value.ToString(), 0.0);
                    ss.Diameter = MyList.StringToDouble(dgv_section_property[5, i].Value.ToString(), 0.0);
                    ss.Thickness = MyList.StringToDouble(dgv_section_property[6, i].Value.ToString(), 0.0);
                    ss.UnitWeight = MyList.StringToDouble(dgv_section_property[7, i].Value.ToString(), 0.0);
                    ss.TotalNos = MyList.StringToInt(dgv_section_property[8, i].Value.ToString(), 1);

                    if (!ss.GroupName.ToUpper().Contains("DECK_SLAB"))
                    {
                        if (!ss.GroupName.ToUpper().Contains("WEARING"))
                            SectionProperty.Add(ss);
                    }
                }
                catch (Exception ex) { }
            }


        }
        public MemberSectionProperty GetSectionProperty()
        {
            MemberSectionProperty msec = new MemberSectionProperty();
            try
            {
                msec.AppliedSection = Section_Applied;
                msec.GroupName = cmb_group_name.Text;
                switch (msec.AppliedSection)
                {
                    case eAppliedSection.Angle_Section1:
                    case eAppliedSection.Angle_Section2:
                    case eAppliedSection.Angle_Section3:
                    case eAppliedSection.Beam_Section1:
                    case eAppliedSection.Beam_Section2:
                    case eAppliedSection.Channel_Section1:
                    case eAppliedSection.Channel_Section2:
                    case eAppliedSection.Builtup_LongGirder:
                    case eAppliedSection.Builtup_CrossGirder:
                        msec.SectionDetails.SectionName = cmb_section_name.Text;
                        msec.SectionDetails.SectionCode = cmb_code1.Text.Replace("x", "");

                        if (cmb_sec_thk.Visible)
                        {
                            msec.SectionDetails.AngleThickness = MyList.StringToDouble(cmb_sec_thk.Text, 0.0);
                        }

                        msec.SectionDetails.LateralSpacing = Section_Lateral_Spacing;
                        msec.SectionDetails.NoOfElements = Nos_Of_Sections;
                        msec.SectionDetails.TopPlate.Width = Top_Plate_Width;
                        msec.SectionDetails.TopPlate.Thickness = Top_Plate_Thickness;

                        msec.SectionDetails.BottomPlate.Width = Bottom_Plate_Width;
                        msec.SectionDetails.BottomPlate.Thickness = Bottom_Plate_Thickness;

                        msec.SectionDetails.SidePlate.Width = Side_Plate_Width;
                        msec.SectionDetails.SidePlate.Thickness = Side_Plate_Thickness;

                        msec.SectionDetails.VerticalStiffenerPlate.Width = Vertical_Stiffener_Plate_Width;
                        msec.SectionDetails.VerticalStiffenerPlate.Thickness = Vertical_Stiffener_Plate_Thickness;
                        break;

                    case eAppliedSection.Reactangular_Section:
                    case eAppliedSection.Circular_Section:
                        msec.Breadth = Section_Breadth;
                        msec.Depth = Section_Depth;
                        msec.Diameter = Section_Diameter;
                        break;
                }

                msec.Length = Section_Length;
                msec.Thickness = Section_Thickness;
                msec.UnitWeight = Section_UnitWeight;
                msec.TotalNos = (int)Section_TotalNos;

                msec.AX_Area = Section_AX;
                msec.IX = Section_IX;
                msec.IY = Section_IY;
                msec.Weight = Section_Weight;
                return msec;
            }
            catch (Exception ex) { }
            return null;
        }
        void Add_Section_Property()
        {
            MemberSectionProperty msec = GetSectionProperty();

            for (int i = 0; i < SectionProperty.Count; i++)
            {
                if (SectionProperty[i].GroupName == msec.GroupName)
                {
                    SectionProperty[i] = msec;
                    return;
                }
            }
            //msec.Weight = msec.Length * msec.UnitWeight;
            SectionProperty.Add(msec);
        }
        void Delete_Section_Property()
        {
            MemberSectionProperty msec = GetSectionProperty();

            for (int i = 0; i < SectionProperty.Count; i++)
            {
                if (SectionProperty[i].GroupName == msec.GroupName)
                {
                    SectionProperty.RemoveAt(i);
                    return;
                }
            }
        }
        private void Select_Section_Prop1(int row_indx)
        {
            try
            {
                int col_indx = 1;

                cmb_group_name.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;

                txt_sec_L.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_B.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_D.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_dia.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_thickness.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_unit_weight.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_total_nos.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_AX.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_IX.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_IY.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_IZ.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;
                txt_sec_weight.Text = dgv_section_property[col_indx, row_indx].Value.ToString(); col_indx++;

                txt_sec_group_mems.Text = "";
                if (Bridge_Analysis != null)
                    txt_sec_group_mems.Text = Bridge_Analysis.Analysis.MemberGroups.GetMemberGroup(cmb_group_name.Text).MemberNosText;
            }
            catch (Exception ex) { }
        }
        private void Select_Section_Prop(string group_name)
        {
            try
            {
                if (SectionProperty.Count == 0) return;

                MemberSectionProperty msec = null;
                foreach (var item in SectionProperty)
                {
                    if (item.GroupName == group_name)
                    {
                        msec = item;
                    }
                }

                if (msec == null) return;


                int col_indx = 1;

                cmb_group_name.Text = msec.GroupName;
                Section_Applied = msec.AppliedSection;

                if (msec.AppliedSection != eAppliedSection.Circular_Section &&
                    msec.AppliedSection != eAppliedSection.Reactangular_Section)
                {
                    Nos_Of_Sections = msec.SectionDetails.NoOfElements;
                    Section_Name = msec.SectionDetails.SectionName;
                    Section_Code = msec.SectionDetails.SectionCode;
                    Section_AngleThickness = msec.SectionDetails.AngleThickness;
                    Top_Plate_Width = msec.SectionDetails.TopPlate.Width;
                    Top_Plate_Thickness = msec.SectionDetails.TopPlate.Thickness;
                    Bottom_Plate_Width = msec.SectionDetails.BottomPlate.Width;
                    Bottom_Plate_Thickness = msec.SectionDetails.BottomPlate.Thickness;

                    Side_Plate_Width = msec.SectionDetails.SidePlate.Width;
                    Side_Plate_Thickness = msec.SectionDetails.SidePlate.Thickness;

                    Vertical_Stiffener_Plate_Width = msec.SectionDetails.VerticalStiffenerPlate.Width;
                    Vertical_Stiffener_Plate_Thickness = msec.SectionDetails.VerticalStiffenerPlate.Thickness;
                    Section_Lateral_Spacing = msec.SectionDetails.LateralSpacing;
                }

                Section_Length = msec.Length;
                Section_Breadth = msec.Breadth;
                Section_Depth = msec.Depth;
                Section_Diameter = msec.Diameter;
                Section_Thickness = msec.Thickness;
                Section_UnitWeight = msec.UnitWeight;
                Section_TotalNos = msec.TotalNos;
                Section_AX = msec.AX_Area;
                Section_IX = msec.IX;
                Section_IY = msec.IY;
                Section_IZ = msec.IZ;
                Section_Weight = msec.Weight;


                txt_sec_group_mems.Text = "";
                if (Bridge_Analysis != null)
                    txt_sec_group_mems.Text = Bridge_Analysis.Analysis.MemberGroups.GetMemberGroup(cmb_group_name.Text).MemberNosText;
            }
            catch (Exception ex) { }
        }

        #endregion SECTION PROPERTIES

        public void Button_Enable_Disable()
        {
            btn_Ana_view_data.Enabled = File.Exists(Input_Data);
            btn_Ana_view_structure.Enabled = File.Exists(Input_Data);
            btn_Ana_View_Moving_Load.Enabled = File.Exists(Analysis_Report);
            btn_Ana_view_report.Enabled = (File.Exists(Analysis_Report) || File.Exists(Analysis_Result_Report) || File.Exists(Cable_Design_Report));
            btn_Ana_process_analysis.Enabled = File.Exists(Input_Data);
        }
        public void FillMemberResult()
        {
            try
            {

                dgv_member_Result.Rows.Clear();

                Results.Clear();
                List<BridgeMemberAnalysis> lst_ana = new List<BridgeMemberAnalysis>();

                if (CS_Analysis.DeadLoad_Analysis != null) lst_ana.Add(CS_Analysis.DeadLoad_Analysis);

                lst_ana.AddRange(CS_Analysis.All_LL_Analysis);

                if (CS_Analysis.TotalLoad_Analysis != null) lst_ana.Add(CS_Analysis.TotalLoad_Analysis);


                for (int i = 0; i < lst_ana.Count; i++)
                {
                    Bridge_Analysis = lst_ana[i];

                    string rest = Path.GetFileName(Path.GetDirectoryName(Bridge_Analysis.Analysis_File).ToUpper());

                    Results.Add("RESULTS FROM : " + rest);

                    dgv_member_Result.Rows.Add(rest, "", "", "", "");


                    dgv_member_Result.Rows[dgv_member_Result.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;



                    List<CMember> list_frc_mem = new List<CMember>();
                    //foreach)< Bridge_Analysis.Analysis.MemberGroups.GroupCollection
                    CMember m = new CMember();
                    foreach (var item in Bridge_Analysis.Analysis.MemberGroups.GroupCollection)
                    {

                        m = new CMember();
                        m.Group = item;
                        list_frc_mem.Add(m);

                        if (item.GroupName.Contains("_LGIRDER"))
                        {
                            List<int> jnts = MyList.Get_Array_Intiger(item.MemberNosText);
                            List<int> mems = new List<int>();
                            for (int im = 0; im < jnts.Count; im++)
                            {
                                Member mb = Bridge_Analysis.Analysis.Members.GetMember(jnts[im]);

                                if ((mb.StartNode.Z == d1) ||
                                    (mb.StartNode.Z == (B - d1)))
                                {
                                    mems.Add(mb.MemberNo);
                                }
                            }

                            m.Group.MemberNosText = MyList.Get_Array_Text(mems);
                        }

                        m.Force = Bridge_Analysis.GetForce(ref m);

                        m.Capacity_TensionForce = ((m.MaxBendingMoment.Force == 0.0) && m.MaxShearForce.Force == 0.0) ? 1770.0 : 0.0;

                        dgv_member_Result.Rows.Add(m.Group.GroupName,
                                ((m.MaxBendingMoment.Force != 0.0) ? 0.0 : (m.MaxTensionForce.Force == 0.0 ? m.MaxCompForce.Force : m.MaxTensionForce.Force)).ToString("E3"),
                                m.Capacity_TensionForce.ToString("E3"),
                                m.MaxBendingMoment.Force.ToString("E3"),
                                m.MaxShearForce.Force.ToString("E3"));

                        //txt_


                        //Results.AddRange(m.MaxCompForce.GetDetails(m.Group.GroupName + " : Max Comp Force", m.Group.MemberNos, "unit"));
                        Results.Add("");
                        Results.Add("");
                        if (m.Group.GroupName.Contains("CABLE"))
                        {
                            if (m.MaxTensionForce.Force != 0)
                            {
                                Results.AddRange(m.MaxTensionForce.GetDetails(m.Group.GroupName + " : Max Tension Force", m.Group.MemberNos, "Ton"));
                            }
                            if (m.MaxCompForce.Force != 0)
                            {
                                Results.AddRange(m.MaxCompForce.GetDetails(m.Group.GroupName + " : Max Tension Force", m.Group.MemberNos, "Ton"));
                            }

                        }
                        else
                        {
                            Results.AddRange(m.MaxBendingMoment.GetDetails(m.Group.GroupName + " : Max Bending Moment", m.Group.MemberNos, "Ton-m"));
                            Results.AddRange(m.MaxShearForce.GetDetails(m.Group.GroupName + " : Max Shear Force", m.Group.MemberNos, "Ton"));

                        }
                        Results.Add("");

                    }

                    double dd = 0.0;
                    for (int r = 0; r < dgv_member_Result.RowCount; r++)
                    {
                        try
                        {
                            for (int col = 0; col < dgv_member_Result.Columns.Count; col++)
                            {
                                dd = MyList.StringToDouble(dgv_member_Result[col, r].Value.ToString(), -999999.0);
                                if (dd == 0.0)
                                    dgv_member_Result[col, r].Value = "";
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch (Exception ex) { }
            Deck_Load_Analysis_Data();
            File.WriteAllLines(Analysis_Result_Report, Results.ToArray());
            Results.Clear();
            //iApp.RunExe(Path.Combine(user_path, "ANALYSIS_RESULT.TXT"));
        }
        private void Write_Ana_Load_Data()
        {
            string file_name = Input_Data;

            if (!File.Exists(file_name)) return;

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    list_member_load.Add(inp_file_cont[i]);
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";
            //s += (chk_Ana_active_SIDL.Checked ? " + SIDL " : "");
            //s += (chk_Ana_active_LL.Checked ? " + LL " : "");

            //if (!load_lst.Contains("LOAD    1   " + s)) load_lst.Add("LOAD    1   " + s);
            //if (!load_lst.Contains("MEMBER LOAD")) load_lst.Add("MEMBER LOAD");

            List<string> lst_mll = new List<string>();
            if (chk_Ana_active_SIDL.Checked)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (dgv_Ana_live_load.RowCount != 0)
                {
                    if (!File.Exists(LiveLoad_File))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    }
                    //Bridge_Analysis.LoadReadFromGrid(dgv_Ana_live_load);
                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }
            //Chiranjit [2011 09 23]
            //Do not write Moving Load Data wheather user Remove all the data from the Data Grid Box
            if (File.Exists(LiveLoad_File))
            {
                Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
                if (dgv_Ana_live_load.RowCount != 0)
                {
                    //load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
                    lst_mll.AddRange(Get_MovingLoad_Data(Live_Load_List));
                }
            }

            load_lst.Remove("");
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );

            //File.WriteAllLines(Input_Data_Nonlinear, inp_file_cont.ToArray());

            indx = indx + load_lst.Count;

            inp_file_cont.InsertRange(indx, lst_mll);


            while (inp_file_cont.Contains("")) inp_file_cont.Remove("");


            File.WriteAllLines(file_name, inp_file_cont.ToArray());




        }
        public void Show_ReadMemberLoad(string file_name)
        {

            if (!File.Exists(file_name)) return;
            List<LoadData> lds = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "LL.txt"));

            List<string> list_member_load = new List<string>();
            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;
            bool mov_flag = false;
            bool isMoving_load = false;

            chk_Ana_active_LL.Checked = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                {
                    chk_Ana_active_LL.Checked = true;
                    isMoving_load = true;
                    if (mlist.Count == 3) txt_Ana_LL_load_gen.Text = mlist.StringList[2];
                    dgv_Ana_live_load.Rows.Clear();
                    continue;
                }

                if (kStr.Contains("DEFINE MOV"))
                {
                    mov_flag = false;
                    //continue;
                }


                if (isMoving_load)
                {
                    try
                    {
                        LoadData ld = LoadData.Parse(kStr);
                        for (int c = 0; c < lds.Count; c++)
                        {
                            if (lds[c].TypeNo == ld.TypeNo)
                            {
                                ld.Code = lds[c].Code;
                                break;
                            }
                        }
                        dgv_Ana_live_load.Rows.Add(ld.TypeNo + " : " + ld.Code,
                            ld.X.ToString("0.000"), ld.Y.ToString("0.000"), ld.Z.ToString("0.000"), ld.XINC.ToString("0.000"));

                    }
                    catch (Exception ex) { }
                }

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    //i++;
                    //continue;
                }
                if (mlist.StringList[0].StartsWith("MEMBER LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                    mov_flag = true;
                    //continue;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                    mov_flag = false;
                }
                if (flag)
                {
                    if (mov_flag)
                    {
                        list_member_load.Add(inp_file_cont[i]);
                    }
                    inp_file_cont.RemoveAt(i);
                    i--;
                }
            }
            txt_member_load.Lines = list_member_load.ToArray();
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList = new List<LoadData>();
            //LoadList.Clear();
            MyList mlist = null;
            for (i = 0; i < dgv_live_load.RowCount; i++)
            {
                try
                {
                    ld = new LoadData();
                    mlist = new MyList(MyList.RemoveAllSpaces(dgv_live_load[0, i].Value.ToString().ToUpper()), ':');
                    ld.TypeNo = mlist.StringList[0];
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);

                    for (int j = 0; j < Live_Load_List.Count; j++)
                    {
                        if (Live_Load_List[j].TypeNo == ld.TypeNo)
                        {
                            ld.LoadWidth = Live_Load_List[j].LoadWidth;
                            break;
                        }
                    }
                    if ((ld.Z + ld.LoadWidth) > B)
                    {
                        throw new Exception("Width of Bridge Deck is insufficient to accommodate \ngiven numbers of Lanes of Vehicle Load. \n\nBridge Width = " + B + " <  Load Width (" + ld.Z + " + " + ld.LoadWidth + ") = " + (ld.Z + ld.LoadWidth));
                    }
                    else
                    {
                        ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                        LoadList.Add(ld);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT KN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (chk_Ana_active_LL.Checked)
            {
                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT")) load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                if (!load_lst.Contains("TYPE 1 CLA 1.179")) load_lst.Add("TYPE 1 CLA 1.179");
                if (!load_lst.Contains("TYPE 2 CLB 1.188")) load_lst.Add("TYPE 2 CLB 1.188");
                if (!load_lst.Contains("TYPE 3 A70RT 1.10")) load_lst.Add("TYPE 3 A70RT 1.10");
                if (!load_lst.Contains("TYPE 4 CLAR 1.179")) load_lst.Add("TYPE 4 CLAR 1.179");
                if (!load_lst.Contains("TYPE 5 A70RR 1.188")) load_lst.Add("TYPE 5 A70RR 1.188");
                if (!load_lst.Contains("TYPE 6 IRC24RTRACK 1.188")) load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                if (!load_lst.Contains("TYPE 7 RAILBG 1.25")) load_lst.Add("TYPE 7 RAILBG 1.25");
                if (!load_lst.Contains("")) load_lst.Add("");

                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");


                load_lst.Add("LOAD GENERATION " + txt_Ana_LL_load_gen.Text);

                LoadReadFromGrid(dgv_Ana_live_load);
                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            try
            {
                if (calc_width > Bridge_Analysis.Analysis.Width)
                {
                    string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Bridge_Analysis.Analysis.Width;

                    str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                    MessageBox.Show(str, "ASTRA");
                    return null;
                }
            }
            catch (Exception ex) { }

            return load_lst.ToArray();
        }

        public void Open_AnalysisFile()
        {
            //string In = file_name;


            try
            {
                ReadData();
                string s = File.Exists(Analysis_Report) ? Analysis_Report : Input_Data;
                if (File.Exists(s))
                {
                    Bridge_Analysis = null;
                    Bridge_Analysis = new BridgeMemberAnalysis(iApp, s);
                    FillMemberResult();
                }

                //MessageBox.Show(this, "File opened successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex) { }


            //string ll_txt = Path.Combine(user_path, "LL.txt");

            //Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            //if (Bridge_Analysis.Live_Load_List == null) return;

            //cmb_Ana_load_type.Items.Clear();
        }


        public void Design_Cables(ref CableMember mem)
        {
            double Ax = Cable_Ax;
            double gamma = Cable_Gamma;
            double alpha_x = mem.InclinationAngle * (Math.PI / 180);
            double na = 0.0;
            double n = 0.0;
            double a = 0.0;
            double E = Cable_E;
            double Fx = 0.0;
            double f = Cable_f;

            double x1, y1, z1;
            double x2, y2, z2;
            double x3, y3, z3;

            Fx = mem.AnalysisForce.Force;

            x1 = mem.MemberDetails.StartNode.X;
            y1 = mem.MemberDetails.StartNode.Y;
            z1 = mem.MemberDetails.StartNode.Z;

            x2 = mem.MemberDetails.EndNode.X;
            y2 = mem.MemberDetails.EndNode.Y;
            z2 = mem.MemberDetails.EndNode.Z;


            x3 = x1;
            y3 = y2;
            z3 = z2;

            List<string> list = new List<string>();
            //list.Add(string.Format("Cross Sectional Area of Cable [Ax] = {0} sq.mm", Ax));
            //list.Add(string.Format("Angle of Inclination of Cable [alpha_x] = {0} ", alpha_x));
            //list.Add(string.Format("Horizontal Projection of Cable Length [na] = {0} ", na));
            //list.Add(string.Format("Number of Panels up to the Cable = {0} ", n));
            //list.Add(string.Format("Length of Each Panel = {0} "));
            //list.Add(string.Format("Elastic Modulus of Cable Material = {0} "));
            //list.Add(string.Format("Force in the Cable [Fx] = {0} ", Fx));

            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User's Cable No : {0}", mem.User_MemberNo));
            list.Add(string.Format("ASTRA's Cable No : {0} ", mem.ASTRA_MemberNo));
            list.Add(string.Format("Joint Coorinate at Upper End : {0} [x1 : {1:f3}, y1 : {2:f3}, z1 : {3:f3}]", mem.StartJointNo, mem.StartJoint_X, mem.StartJoint_Y, mem.StartJoint_Z));
            list.Add(string.Format("Joint Coorinate at Lower End : {0} [x2 : {1:f3}, y2 : {2:f3}, z2 : {3:f3}]", mem.EndJointNo, mem.EndJoint_X, mem.EndJoint_Y, mem.EndJoint_Z));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Diameter = d = {0:f3} m", Cable_D));
            list.Add(string.Format("Provided Cable Cross Section Area     = π*d^2/4 = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Permissible Shear Stress [f] = {0} N/sq.mm = {1} Ton/sq.m", f, (f = f * 100)));
            list.Add(string.Format("Specific Weight of Cable Material [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));


            list.Add(string.Format("Specific Weight of Cable [γ] = {0} Ton/cu.m", gamma));
            list.Add(string.Format(""));
            list.Add(string.Format("Analysis Force :  {0:E3} Ton           [Member No :{1}, LoadCase : {2}]", mem.AnalysisForce, mem.AnalysisForce.MemberNo, mem.AnalysisForce.Loadcase));
            list.Add(string.Format("Analysis Stress : {0:E3} Ton/sq.m      [Member No :{1}, LoadCase : {2}]", mem.AnalysisStress, mem.AnalysisStress.MemberNo, mem.AnalysisStress.Loadcase));
            list.Add(string.Format(""));
            list.Add(string.Format("Pylon Coordinate at Deck : {0} m", mem.StartJoint_Y));
            list.Add(string.Format(""));
            double Lx = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
            double Lh = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3) + (z2 - z3) * (z2 - z3));
            double Lv = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3) + (z1 - z3) * (z1 - z3));
            //list.Add(string.Format("Length of Cable = Lx = SQRT((x1 - x2)^2 + (y1 - y2)^2 + (z1 - z2)^2) = {0:f3} m", Lx));
            list.Add(string.Format("Length of Cable = Lx = {0:f3} m", Lx));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lh = SQRT((x2 - x3)^2 + (y2 - y3)^2 + (z2 - z3)^2) = {0:f3} m", Lh));
            list.Add(string.Format("Length of Horizontal Projection = Lh = {0:f3} m", Lh));
            list.Add(string.Format(""));
            //list.Add(string.Format("Length of Horizontal Projection = Lv = SQRT((x1 - x3)^2 + (y1 - y3)^2 + (z1 - z3)^2) = {0:f3} m", Lv));
            list.Add(string.Format("Length of Vertical Projection = Lv = {0:f3} m", Lv));
            list.Add(string.Format(""));
            alpha_x = mem.MemberDetails.InclinationAngle_in_Radian;
            list.Add(string.Format("Inclination Angle of Cable = α_x"));
            list.Add(string.Format("                           = tan^(-1)[(y2-y1)/(x2-x1)]", Lv));
            list.Add(string.Format("                           = {0:f0}° (degree)", mem.InclinationAngle));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Pu = Fx * Math.Sin(alpha_x);
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Component of Force = Pu = Fx * Sin α_x"));
            list.Add(string.Format(""));
            list.Add(string.Format("                                 = {0:E3} * {1:F3}", Fx, Math.Sin(alpha_x)));
            list.Add(string.Format("                                 = {0:E3} Ton", Pu));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Ax = Fx / f;
            list.Add(string.Format("Required Cross Section Area = Ax = Fx / f"));
            list.Add(string.Format("                                 = {0:E3} / {1}", Fx, f));
            list.Add(string.Format("                                 = {0:E3} sq.m", Ax));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided Cable Cross Section Area = {0:E3} sq.m", Cable_Ax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double W = Cable_Ax * Lx * gamma;
            list.Add(string.Format("Weight of Cable = W = Ax * Lx * γ "));
            list.Add(string.Format("                = {0:E3} * {1:f3} * {2}", Cable_Ax, Lx, gamma));
            list.Add(string.Format("                = {0:E3} Ton", W));
            list.Add(string.Format(""));

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Calculated Force = Fx"));
            //list.Add(string.Format("                 = Pu/Sin α_x"));
            //list.Add(string.Format("                 = {0:E3}/{1:f3}", Pu, Math.Sin(alpha_x)));
            //list.Add(string.Format("                 = {0:E3} Ton", Fx));
            //list.Add(string.Format(""));

            mem.CalculatedStress = Fx / Ax;
            //list.Add(string.Format("Calculated Stress = Fx / Ax "));
            //list.Add(string.Format("                  =  Pu/(Sin α_x * Ax)"));
            //list.Add(string.Format("                  =  {0:E3}/({1:F3} * {2:E3})", Pu, Math.Sin(alpha_x), Ax));
            //list.Add(string.Format("                  =  {0:E3} Ton/sq.m", mem.CalculatedStress));
            list.Add(string.Format(""));
            list.Add(string.Format("E = Stress / Strain"));
            mem.Strain = (Fx / Cable_Ax) / E;
            list.Add(string.Format("Strain = Stress / E  "));
            list.Add(string.Format("       = (Fx / Ax) / E  "));
            list.Add(string.Format("       = ({0:E3}/{1:E3}) / {2:E3}", Fx, Cable_Ax, E));
            list.Add(string.Format("       = {0:E3} ", mem.Strain));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Strain  = Elongation / Lx"));

            mem.Elongation = mem.Strain * Lx;
            list.Add(string.Format(""));

            list.Add(string.Format("Elongation in Cable = δx = Strain * Lx"));
            list.Add(string.Format("                         = {0:E3} * {1:E3} ", mem.Strain, Lx));
            list.Add(string.Format("                         = {0:E3}  m", mem.Elongation));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Deflection of Deck at joint x2 = δhn = δx / Sin α_x"));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Sin(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Vertical_Deflection_at_Deck));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Deflection at Pylon Top      = δhx = δx/Cos α_x "));
            list.Add(string.Format("                                              = {0:E3} / {1:f3}", mem.Elongation, Math.Cos(alpha_x)));
            list.Add(string.Format("                                              = {0:E3} m", mem.Horizontal_Deflection_at_Pylon_Top));
            list.Add("");
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add("");
            list.Add("");

            mem.DesignResult.Clear();
            mem.DesignResult.AddRange(list.ToArray());

        }

        public void Read_Cable_Member()
        {
                //Input_Data

            //Results.Clear();
            List<BridgeMemberAnalysis> lst_ana = new List<BridgeMemberAnalysis>();

            if (CS_Analysis.DeadLoad_Analysis != null) lst_ana.Add(CS_Analysis.DeadLoad_Analysis);

            lst_ana.AddRange(CS_Analysis.All_LL_Analysis);

            if (CS_Analysis.TotalLoad_Analysis != null) lst_ana.Add(CS_Analysis.TotalLoad_Analysis);


            Bridge_Analysis = CS_Analysis.TotalLoad_Analysis;


            if(Bridge_Analysis.MemberAnalysis == null)
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            List<int> list_mem = null;

            string kStr = MyList.RemoveAllSpaces(txt_cbl_des_mem_nos.Text.Trim().ToUpper());
            List<int> non_cable_mem = new List<int>();
            if (kStr == "ALL")
            {
                kStr = "1 TO " + Bridge_Analysis.Analysis.Members.Count;

            }
            list_mem = MyList.Get_Array_Intiger(kStr);

            CableMember cbl = new CableMember(); dgv_cable_design.Rows.Clear();
            Cable_Members.Clear();
            foreach (var item in list_mem)
            {
                cbl = new CableMember();
                cbl.User_MemberNo = item;

                AnalysisData ana = (AnalysisData)Bridge_Analysis.MemberAnalysis[item];
                if (ana != null)
                {
                    cbl.ASTRA_MemberNo = ana.AstraMemberNo;
                }

                cbl.MemberDetails = Bridge_Analysis.Analysis.Members.GetMember(item);

                List<int> l_int = new List<int>();

                l_int.Add(cbl.StartJointNo);
                l_int.Add(cbl.EndJointNo);


                CMember mem = new CMember();

                mem.Group.MemberNos.Add(item);

                mem.Result = Bridge_Analysis.GetForce(ref mem);
                if (mem.MaxTensionForce != null)
                {
                    cbl.AnalysisForce = (mem.MaxTensionForce.Force == 0.0) ? mem.MaxCompForce : mem.MaxTensionForce;
                }
                if (mem.MaxStress != null)
                {
                    cbl.AnalysisStress = mem.MaxStress;
                }
                if (cbl.MemberDetails != null)
                {
                    if (cbl.InclinationAngle > 0 && cbl.InclinationAngle < 90)
                    {
                        Cable_Members.Add(cbl);
                        dgv_cable_design.Rows.Add(cbl.ToArray());

                    }
                    else
                        non_cable_mem.Add(item);
                }
            }
            if (kStr == "")
            {
                MessageBox.Show(this, "Please put member nos.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_cbl_des_mem_nos.Focus();
            }
            else if (non_cable_mem.Count != 0)
            {
                kStr = MyList.Get_Array_Text(non_cable_mem);


                //kStr = kStr.Replace(" ", ", ");
                MessageBox.Show(this, "Member No(s) : " + kStr + " are not Cable Member", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dgv_cable_design.RowCount == 0)
            {
                MessageBox.Show(this, "Member No(s) : " + kStr + " are not Cable Member", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            grb_members.Text = "Total " + dgv_cable_design.RowCount + " Members in the List.";
        }
        void Format_SIDL()
        {
            int indx = 0;

            //double L, B, D, N, vol, gama, to_wgt;
            SuperInposedDeadLoad dl = new SuperInposedDeadLoad("LL");
            try
            {
                for (int i = 0; i < dgv_SIDL.RowCount; i++)
                {
                    indx = i;
                    dl.Length = MyList.StringToDouble(dgv_SIDL[1, indx].Value.ToString(), 0.0);
                    dl.Breadth = MyList.StringToDouble(dgv_SIDL[2, indx].Value.ToString(), 0.0);
                    dl.Depth = MyList.StringToDouble(dgv_SIDL[3, indx].Value.ToString(), 0.0);
                    dl.TotalNo = MyList.StringToInt(dgv_SIDL[4, indx].Value.ToString(), 0);
                    dl.Gamma = MyList.StringToDouble(dgv_SIDL[6, indx].Value.ToString(), 0);

                    dgv_SIDL[1, indx].Value = dl.Length.ToString("0.00");
                    dgv_SIDL[2, indx].Value = dl.Breadth.ToString("0.000");
                    dgv_SIDL[3, indx].Value = dl.Depth.ToString("0.000");
                    dgv_SIDL[4, indx].Value = dl.TotalNo.ToString();
                    dgv_SIDL[5, indx].Value = dl.Volume.ToString("0.000");
                    dgv_SIDL[6, indx].Value = dl.Gamma.ToString("0.000");
                    dgv_SIDL[7, indx].Value = dl.Weight.ToString("0.000");
                }
            }
            catch (Exception ex) { }
        }

        public void Calculate_Moment_Of_Inertia()
        {
            double ax, ix, iy, iz;
            double I, I1, I2, I3, I4;
            double unit_wt = 0.0;
            double B1, t1, B2, t2, B3, t3, B4, t4;
            int n1, n2, n3, n4, n;
            double D, S, Cxx, Cyy, B, ang_L1, ang_L2;
            double t = 0.0;
            double Wt = 0.0;

            B = 0.0; ang_L1 = 0.0; ang_L2 = 0.0;
            ax = 0.0; ix = 0.0; iy = 0.0; iz = 0.0;
            I = 0.0; I1 = 0.0; I2 = 0.0; I3 = 0.0; I4 = 0.0;

            S = Section_Lateral_Spacing;
            D = 0.0;
            Cxx = 0.0;
            Cyy = 0.0;

            B1 = Top_Plate_Width;
            t1 = Top_Plate_Thickness;
            n1 = 1;

            B3 = Side_Plate_Width;
            t3 = Side_Plate_Thickness;
            n3 = 2;

            B4 = Vertical_Stiffener_Plate_Width;
            t4 = Vertical_Stiffener_Plate_Thickness;
            n4 = 2;

            B2 = Bottom_Plate_Width;
            t2 = Bottom_Plate_Thickness;
            n2 = 1;

            n = (int)Nos_Of_Sections;
            #region Calculate_Moment_Of_Inertia
            if (cmb_section_name.Text.Contains("A"))
            {
                #region Angle
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName == cmb_section_name.Text &&
                        item.SectionSize == cmb_code1.Text &&
                        item.Thickness.ToString() == cmb_sec_thk.Text)
                    {

                        try
                        {
                            MyList mlist = new MyList(item.SectionSize.ToLower(), 'x');
                            ang_L1 = mlist.GetDouble(0);
                            ang_L2 = mlist.GetDouble(1);

                            Cxx = item.Cxx;
                            Cyy = item.Cxx;
                            ax = (item.Area * 100);
                            ix = item.Ixx * (1.0E+8);
                            iy = item.Iyy * (1.0E+8);
                            unit_wt = item.Weight;
                        }
                        catch (Exception ex) { }
                        break;
                    }
                }
                #endregion Angle
            }
            else if (cmb_section_name.Text.Contains("C"))
            {
                #region Channel
                foreach (var item in tbl_rolledSteelChannels.List_Table)
                {
                    if (item.SectionName == cmb_section_name.Text &&
                        item.SectionCode == cmb_code1.Text)
                    {
                        //tab_item = item;
                        ax = (item.Area * 100);
                        ix = item.Ixx * (1.0E+8);
                        iy = item.Iyy * (1.0E+8);
                        unit_wt = item.Weight;

                        D = item.Depth;
                        t = item.WebThickness;
                        B = item.FlangeWidth;
                        Cyy = item.CentreOfGravity * 10;


                        //ax += (np * tp * Bp) + (nbp * tbp * Bbp) + (ns * ts * Bs) + (nss * tss * Bss);

                        break;
                    }
                }
                #endregion Channel
            }
            else if (cmb_section_name.Text.Contains("B"))
            {
                #region Beam
                foreach (var item in tbl_rolledSteelBeams.List_Table)
                {
                    if (item.SectionName == cmb_section_name.Text &&
                        item.SectionCode == cmb_code1.Text)
                    {
                        D = item.Depth;
                        B = item.FlangeWidth;
                        t = item.WebThickness;

                        ax = (item.Area * 100);
                        ix = item.Ixx * (1.0E+8);
                        iy = item.Iyy * (1.0E+8);
                        unit_wt = item.Weight;
                        break;
                    }
                }
                #endregion Beam
            }
            #endregion



            switch (Section_Applied)
            {
                case eAppliedSection.Angle_Section1:
                    #region Calculate IX
                    I = ix + ax * Math.Pow(((B3 / 2.0) - Cxx), 2.0);
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((B3 / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((B3 / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy + ax * Math.Pow(((B1 / 2.0) - Cyy), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((B1 / 2.0) + (t3 / 2.0)), 2.0));

                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((B1 / 2.0) - (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY
                    break;
                case eAppliedSection.Angle_Section2:
                    #region Calculate IX
                    I = ix + ax * Math.Pow(((B3 / 2.0) - Cxx), 2.0);
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((B3 / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((B3 / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy + ax * Math.Pow(((B1 / 2.0) + Cyy - ang_L1), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((B1 / 2.0) - ang_L1 - (t3 / 2.0)), 2.0));

                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((B1 / 2.0) - ang_L1 - (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    break;
                case eAppliedSection.Angle_Section3:
                    break;
                case eAppliedSection.Beam_Section1:
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((D / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((D / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy + ax * Math.Pow(((B1 / 2.0) - (B / 2)), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((B1 / 2.0) - (B / 2.0) + (t / 2.0) + (t3 / 2.0)), 2.0));

                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((B1 / 2.0) - (B / 2.0) - (t / 2.0) - (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY
                    break;
                case eAppliedSection.Beam_Section2:
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((D / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((D / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy;

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((t / 2.0) + (t3 / 2.0)), 2.0));

                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((t / 2.0) + t3 + (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    break;
                case eAppliedSection.Channel_Section1:
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((D / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((D / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy + ax * Math.Pow(((B1 / 2.0) - Cyy), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((B1 / 2.0) + (t3 / 2.0)), 2.0));
                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((B1 / 2.0) - t - (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    break;
                case eAppliedSection.Channel_Section2:
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((D / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((D / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY
                    I = iy + ax * Math.Pow(((B1 / 2.0) - B + Cyy), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((B1 / 2.0) - B + t + (t3 / 2.0)), 2.0));
                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((B1 / 2.0) - B - (t4 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    break;
                case eAppliedSection.Builtup_LongGirder:
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((B3 / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((B3 / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY

                    I = iy + ax * Math.Pow(((B1 / 2.0) - B + Cyy), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = ((B3 * t3 * t3 * t3 / 12.0) + (B3 * t3) * Math.Pow(((t3 / 2.0) + (S / 2.0)), 2.0));
                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((t4 / 2.0) + (S / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    ax += (n1 * t1 * B1) + (n2 * t2 * B2) + (n3 * t3 * B3) + (n4 * t4 * B4);

                    Section_AX = (ax / 1.0E6);
                    Section_IX = (ix / 1.0E12);
                    Section_IY = (iy / 1.0E12);
                    Section_IZ = (Section_IX + Section_IY);
                    Section_Weight = Section_AX * Section_UnitWeight * Section_Length * Section_TotalNos;
                    return;

                    //Wt = B1*t1*n1 + B2*t2*n2 + B3*t3*n3
                    break;
                case eAppliedSection.Builtup_CrossGirder:

                    n3 = 1;
                    #region Calculate IX
                    I = ix;
                    I1 = (B1 * t1 * t1 * t1 / 12.0) + (B1 * t1) * Math.Pow(((B3 / 2.0) + (t1 / 2.0)), 2.0);
                    I2 = (B2 * t2 * t2 * t2 / 12.0) + (B2 * t2) * Math.Pow(((B3 / 2.0) + (t2 / 2.0)), 2.0);
                    I3 = (t3 * B3 * B3 * B3 / 12.0);
                    I4 = (t4 * B4 * B4 * B4 / 12.0);

                    ix = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IX

                    #region Calculate IY

                    I = iy + ax * Math.Pow(((B1 / 2.0) - B + Cyy), 2.0);

                    I1 = (t1 * B1 * B1 * B1 / 12.0);
                    I2 = (t2 * B2 * B2 * B2 / 12.0);
                    I3 = (B3 * t3 * t3 * t3 / 12.0);
                    I4 = ((B4 * t4 * t4 * t4 / 12.0) + (t4 * B4) * Math.Pow(((t4 / 2.0) + (t3 / 2.0)), 2.0));

                    iy = I * n + I1 * n1 + I2 * n2 + I3 * n3 + I4 * n4;
                    #endregion Calculate IY

                    ax += (n1 * t1 * B1) + (n2 * t2 * B2) + (n3 * t3 * B3) + (n4 * t4 * B4);

                    Section_AX = (ax / 1.0E6);
                    Section_IX = (ix / 1.0E12);
                    Section_IY = (iy / 1.0E12);
                    Section_IZ = (Section_IX + Section_IY);
                    Section_Weight = Section_AX * Section_UnitWeight * Section_Length * Section_TotalNos;
                    return;

                    break;
            }

            ax += ((n1 * t1 * B1) + (n2 * t2 * B2) + (n3 * t3 * B3) + (n4 * t4 * B4));

            Section_AX = (ax / 1.0E6);
            Section_IX = (ix / 1.0E12);
            Section_IY = (iy / 1.0E12);
            Section_IZ = (Section_IX + Section_IY);
            Section_UnitWeight = (unit_wt / 1.0E4);
            Section_Weight = Section_UnitWeight * Section_Length * Section_TotalNos;
        }

        public void Read_Angle_Sections(ComboBox cmb)
        {
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
        public void Read_Angle_Sections()
        {
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                cmb_section_name.Items.Clear();
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb_section_name.Items.Contains(item.SectionName) == false)
                            cmb_section_name.Items.Add(item.SectionName);
                }
            }
        }
        public void Read_Beam_Sections()
        {
            if (tbl_rolledSteelBeams.List_Table.Count > 0)
            {
                cmb_section_name.Items.Clear();
                foreach (var item in tbl_rolledSteelBeams.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb_section_name.Items.Contains(item.SectionName) == false)
                            cmb_section_name.Items.Add(item.SectionName);
                }

            }
        }
        public void Read_Channel_Sections()
        {
            if (tbl_rolledSteelChannels.List_Table.Count > 0)
            {
                cmb_section_name.Items.Clear();
                foreach (var item in tbl_rolledSteelChannels.List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb_section_name.Items.Contains(item.SectionName) == false)
                            cmb_section_name.Items.Add(item.SectionName);
                }
            }
        }

        #endregion User Events


        #region Deck Slab + Steel Girder Form Events
        private void btn_Deck_Process_Click(object sender, EventArgs e)
        {
            DeckSlab_Initialize_InputData();
            Deck.FilePath = user_path;
            Deck.Write_User_Input();
            Deck.Calculate_Program();
            Deck.Write_Drawing_File();
            if (File.Exists(Deck.rep_file_name)) { MessageBox.Show(this, "Report file written in " + Deck.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(Deck.rep_file_name); }
            Deck.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_Deck_Report_Click(object sender, EventArgs e)
        {

            iApp.RunExe(Deck.rep_file_name);
        }


        private void txt_Deck_fck_TextChanged(object sender, EventArgs e)
        {

        }
        private void Deck_rbtn_inner_girder_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                Deck_Load_Analysis_Data();
                Deck_Initialize_InputData();
                Deck.FilePath = user_path;
                btnReport.Enabled = (File.Exists(Deck.rep_file_name));
            }
            catch (Exception ex) { }

        }
        #endregion Deck Slab + Steel Girder Form Events

        #region Deck Slab + Steel Girder Methods
        public void Deck_Load_Analysis_Data()
        {

            double dval = 0.0;
            try
            {
                for (int i = 0; i < dgv_member_Result.RowCount; i++)
                {
                    if (dgv_member_Result[0, i].Value.ToString().ToUpper() == "_LGIRDER2")
                    {
                        dval = MyList.StringToDouble(dgv_member_Result[3, i].Value.ToString(), 0.0);
                        txt_deck_long_Moment.Text = (dval * 10.0).ToString();
                        dval = MyList.StringToDouble(dgv_member_Result[4, i].Value.ToString(), 0.0);
                        txt_deck_long_Shear.Text = (dval * 10.0).ToString();
                    }
                    if (dgv_member_Result[0, i].Value.ToString().ToUpper() == "_XGIRDER2")
                    {
                        dval = MyList.StringToDouble(dgv_member_Result[3, i].Value.ToString(), 0.0);
                        txt_deck_cross_Moment.Text = (dval * 10.0).ToString();
                        dval = MyList.StringToDouble(dgv_member_Result[4, i].Value.ToString(), 0.0);
                        txt_deck_cross_Shear.Text = (dval * 10.0).ToString();
                    }
                }
            }
            catch (Exception ex) { }

            //txt_deck_long_Moment.Text = MyList.StringToDouble(txt_Ana_inner_long_L2_moment.Text, 0.0) * 10.0 + "";
            //deck_inner_L4_Shear.Text = MyList.StringToDouble(txt_Ana_inner_long_L4_shear.Text, 0.0) * 10.0 + "";
            //txt_deck_long_Shear.Text = MyList.StringToDouble(txt_Ana_inner_long_L2_shear.Text, 0.0) * 10.0 + "";

            //txt_deck_cross_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_L2_moment.Text, 0.0) * 10.0 + "";
            //txt_deck_outer_L4_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_L4_moment.Text, 0.0) * 10.0 + "";
            //txt_deck_outer_Deff_Moment.Text = MyList.StringToDouble(txt_Ana_outer_long_deff_moment.Text, 0.0) * 10.0 + "";
            //txt_deck_outer_Deff_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_deff_shear.Text, 0.0) * 10.0 + "";
            //txt_deck_outer_L4_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_L4_shear.Text, 0.0) * 10.0 + "";
            //txt_deck_cross_Shear.Text = MyList.StringToDouble(txt_Ana_outer_long_L2_shear.Text, 0.0) * 10.0 + "";
        }
        private void Deck_Initialize_InputData()
        {
            //Deck.tbl_rolledSteelAngles = new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES"));

            string kStr = "";
            try
            {
                if (cmb_Long_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_Long_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }

                if (cmb_cross_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_cross_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void DeckSlab_Initialize_InputData()
        {
            try
            {
                Deck.S = MyList.StringToDouble(txt_S.Text, 0.0);
                Deck.B1 = MyList.StringToDouble(txt_B1.Text, 0.0);
                Deck.B2 = MyList.StringToDouble(txt_B2.Text, 0.0);
                Deck.B = MyList.StringToDouble(txt_B.Text, 0.0);
                Deck.fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                Deck.fy = MyList.StringToDouble(txt_fy.Text, 0.0);
                Deck.m = MyList.StringToDouble(txt_m.Text, 0.0);
                Deck.YS = MyList.StringToDouble(txt_YS.Text, 0.0);
                Deck.D = MyList.StringToDouble(txt_D.Text, 0.0);
                Deck.L = MyList.StringToDouble(txt_L.Text, 0.0);
                Deck.Dwc = MyList.StringToDouble(txt_Dwc.Text, 0.0);

                Deck.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);

                Deck.gamma_wc = MyList.StringToDouble(txt_gamma_wc.Text, 0.0);
                Deck.WL = MyList.StringToDouble(txt_WL.Text, 0.0);
                Deck.v = MyList.StringToDouble(txt_v.Text, 0.0);
                Deck.u = MyList.StringToDouble(txt_u.Text, 0.0);
                Deck.IF = MyList.StringToDouble(txt_IF.Text, 0.0);
                Deck.CF = MyList.StringToDouble(txt_CF.Text, 0.0);
                Deck.j = MyList.StringToDouble(txt_j.Text, 0.0);
                Deck.Q = MyList.StringToDouble(txt_Q.Text, 0.0);

                Deck.sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
                Deck.sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                Deck.tau = MyList.StringToDouble(txt_tau.Text, 0.0);
                Deck.sigma_tf = MyList.StringToDouble(txt_sigma_tf.Text, 0.0);

                Deck.K = MyList.StringToDouble(txt_K.Text, 0.0);
                Deck.sigma_p = MyList.StringToDouble(txt_sigma_p.Text, 0.0);


                Deck.dw = MyList.StringToDouble(txt_Dwc.Text, 0.0);
                Deck.tw = MyList.StringToDouble(txt_Long_tw.Text, 0.0);
                Deck.bf1 = MyList.StringToDouble(txt_Long_bf1.Text, 0.0);
                Deck.tf1 = MyList.StringToDouble(txt_Long_tf1.Text, 0.0);
                Deck.bf2 = MyList.StringToDouble(txt_Long_bf2.Text, 0.0);
                Deck.tf2 = MyList.StringToDouble(txt_Long_tf2.Text, 0.0);


                Deck.LongGirder_ang = cmb_Long_ang_section_code.Text;
                Deck.LongGirder_ang_name = cmb_Long_ang_section_name.Text;
                Deck.LongGirder_ang_thk = MyList.StringToDouble(cmb_Long_ang_thk.Text, 0.0);
                Deck.off = MyList.StringToDouble(txt_off.Text, 0.0);

                //Deck.des_moment = MyList.StringToDouble(txt_des_mom.Text, 0.0);
                //Deck.des_shear = MyList.StringToDouble(txt_des_shr.Text, 0.0);


                Deck.nw = MyList.StringToInt(txt_Long_nw.Text, 0);
                Deck.nf = 1;
                Deck.na = MyList.StringToInt(cmb_Long_nos_ang.Text, 0);



                //Chiranjit [2011 11 25]
                //Add Extra Property like Nos of Web PLates at L/2
                Deck.LongGirder_nw = MyList.StringToDouble(txt_Long_nw.Text, 0);
                Deck.LongGirder_dw = MyList.StringToDouble(txt_Long_dw.Text, 0);
                Deck.LongGirder_bf1 = MyList.StringToDouble(txt_Long_bf1.Text, 0);
                Deck.LongGirder_bf2 = MyList.StringToDouble(txt_Long_bf2.Text, 0);
                Deck.LongGirder_nf = 1;
                Deck.LongGirder_tf1 = MyList.StringToDouble(txt_Long_tf1.Text, 0);
                Deck.LongGirder_tf2 = MyList.StringToDouble(txt_Long_tf2.Text, 0);
                Deck.LongGirder_tw = MyList.StringToDouble(txt_Long_tw.Text, 0);
                Deck.LongGirder_ang_thk = MyList.StringToDouble(cmb_Long_ang_thk.Text, 0);
                Deck.LongGirder_nos_ang = MyList.StringToDouble(cmb_Long_nos_ang.Text, 0);
                Deck.LongGirder_ang = cmb_Long_ang_section_code.Text;

                //Chiranjit [2011 11 25]
                //Add Extra Property like Nos of Web PLates at L/2
                Deck.CrossGirder_nw = MyList.StringToDouble(txt_cross_nw.Text, 0);
                Deck.CrossGirder_dw = MyList.StringToDouble(txt_cross_dw.Text, 0);
                Deck.CrossGirder_bf1 = MyList.StringToDouble(txt_cross_bf1.Text, 0);
                Deck.CrossGirder_bf2 = MyList.StringToDouble(txt_cross_bf2.Text, 0);
                Deck.CrossGirder_nf = 1;
                Deck.CrossGirder_tf1 = MyList.StringToDouble(txt_cross_tf1.Text, 0);
                Deck.CrossGirder_tf2 = MyList.StringToDouble(txt_cross_tf2.Text, 0);
                Deck.CrossGirder_tw = MyList.StringToDouble(txt_cross_tw.Text, 0);
                Deck.CrossGirder_ang_thk = MyList.StringToDouble(cmb_cross_ang_thk.Text, 0);
                Deck.CrossGirder_nos_ang = MyList.StringToDouble(cmb_cross_nos_ang.Text, 0);
                Deck.CrossGirder_ang = cmb_cross_ang_section_code.Text;
                Deck.CrossGirder_ang_name = cmb_cross_ang_section_name.Text;




                Deck.IsPlateArrangement = false;

                Deck.deff = Deck.dw / 1000.0;

                Deck.LongGirder_Moment = MyList.StringToDouble(txt_deck_long_Moment.Text, 0.0);
                Deck.CrossGirder_Moment = MyList.StringToDouble(txt_deck_cross_Moment.Text, 0.0);

                Deck.LongGirder_Shear = MyList.StringToDouble(txt_deck_long_Shear.Text, 0.0);
                Deck.CrossGirder_Shear = MyList.StringToDouble(txt_deck_cross_Shear.Text, 0.0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }
        }
        public void DeckSlab_Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(Deck.user_input_file));
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
                        case "S":
                            Deck.S = mList.GetDouble(1);
                            txt_S.Text = Deck.S.ToString();
                            break;
                        case "B1":
                            Deck.B1 = mList.GetDouble(1);
                            txt_B1.Text = Deck.B1.ToString();
                            break;
                        case "B2":
                            Deck.B2 = mList.GetDouble(1);
                            txt_B2.Text = Deck.B2.ToString();
                            break;
                        case "B":
                            Deck.B = mList.GetDouble(1);
                            txt_B.Text = Deck.B.ToString();
                            break;
                        case "fck":
                            Deck.fck = mList.GetDouble(1);
                            txt_fck.Text = Deck.fck.ToString();
                            break;
                        case "fy":
                            Deck.fy = mList.GetDouble(1);
                            txt_fy.Text = Deck.fy.ToString();
                            break;
                        case "m":
                            Deck.m = mList.GetDouble(1);
                            txt_m.Text = Deck.m.ToString();
                            break;
                        case "YS":
                            Deck.YS = mList.GetDouble(1);
                            txt_YS.Text = Deck.YS.ToString();
                            break;
                        case "D":
                            Deck.D = mList.GetDouble(1);
                            txt_D.Text = Deck.D.ToString();
                            break;
                        case "L":
                            Deck.L = mList.GetDouble(1);
                            txt_L.Text = Deck.L.ToString();
                            break;
                        case "Dwc":
                            Deck.Dwc = mList.GetDouble(1);
                            txt_Dwc.Text = Deck.Dwc.ToString();
                            break;
                        case "gamma_c":
                            Deck.gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = Deck.gamma_c.ToString();
                            break;
                        case "gamma_wc":
                            Deck.gamma_wc = mList.GetDouble(1);
                            txt_gamma_wc.Text = Deck.gamma_wc.ToString();
                            break;
                        case "WL":
                            Deck.WL = mList.GetDouble(1);
                            txt_WL.Text = Deck.WL.ToString();
                            break;
                        case "v":
                            Deck.v = mList.GetDouble(1);
                            txt_v.Text = Deck.v.ToString();
                            break;
                        case "u":
                            Deck.u = mList.GetDouble(1);
                            txt_u.Text = Deck.u.ToString();
                            break;
                        case "IF":
                            Deck.IF = mList.GetDouble(1);
                            txt_IF.Text = Deck.IF.ToString();
                            break;
                        case "CF":
                            Deck.CF = mList.GetDouble(1);
                            txt_CF.Text = Deck.CF.ToString();
                            break;
                        case "Q":
                            Deck.Q = mList.GetDouble(1);
                            txt_Q.Text = Deck.Q.ToString();
                            break;
                        case "j":
                            Deck.j = mList.GetDouble(1);
                            txt_j.Text = Deck.j.ToString();
                            break;
                        case "sigma_st":
                            Deck.sigma_st = mList.GetDouble(1);
                            txt_sigma_st.Text = Deck.sigma_st.ToString();
                            break;
                        case "sigma_b":
                            Deck.sigma_b = mList.GetDouble(1);
                            txt_sigma_b.Text = Deck.sigma_b.ToString();
                            break;
                        case "tau":
                            Deck.tau = mList.GetDouble(1);
                            txt_tau.Text = Deck.tau.ToString();
                            break;
                        case "sigma_tf":
                            Deck.sigma_tf = mList.GetDouble(1);
                            txt_sigma_tf.Text = Deck.sigma_tf.ToString();
                            break;
                        case "K":
                            Deck.K = mList.GetDouble(1);
                            txt_K.Text = Deck.K.ToString();
                            break;
                        case "sigma_p":
                            Deck.sigma_p = mList.GetDouble(1);
                            txt_sigma_p.Text = Deck.sigma_p.ToString();
                            break;
                        case "dw":
                            txt_Long_dw.Text = mList.StringList[1];
                            break;
                        case "tw":
                            //txt_tw.Text = mList.StringList[1];
                            break;
                        case "nw":
                            //txt_nw.Text = mList.StringList[1];
                            break;
                        case "bf":
                            //txt_bf.Text = mList.StringList[1];
                            break;
                        case "tf":
                            //txt_tf.Text = mList.StringList[1];
                            break;
                        case "nf":
                            //txt_nf.Text = mList.StringList[1];
                            break;
                        case "ang":
                            //cmb_ang.SelectedItem = mList.StringList[1];
                            break;
                        case "ang_thk":
                            //cmb_ang_thk.Text = mList.StringList[1];
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        #endregion Deck Slab + Steel Girder Methods


        #region Form Events

        private void btn_Ana_add_load_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_Ana_live_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Ana_Y.Text, txt_Ana_Z.Text, txt_Ana_XINCR.Text);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_Ana_live_load.Rows.RemoveAt(dgv_Ana_live_load.CurrentRow.Index);
                chk_Ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_Ana_live_load.Rows.Clear();
            chk_Ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);

        }



        private bool Check_Project_Folder()
        {

            if (Path.GetFileName(user_path) != Project_Name)
            {
                MessageBox.Show(this, "New Project is not created. Please create New Project.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }


        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;



            if (!Check_Project_Folder()) return;
            if (Path.GetFileName(user_path) != Project_Name)
            {

                Create_Project();

            }
            if (!Directory.Exists(user_path))
                Directory.CreateDirectory(user_path);

            Input_Data = Path.Combine(user_path, "INPUT_DATA.TXT");

            if (!File.Exists(CSB_DATA_File))
            {
                string sf = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Cable Stayed Bridge\" + Path.GetFileName(CSB_DATA_File));

                if (File.Exists(sf))
                    File.Copy(sf, CSB_DATA_File);
            }
            if (dgv_section_property.RowCount < 2)
                ReadData();

            //CreateData();
            try
            {



                //if (iApp.DesignStandard == eDesignStandard.IndianStandard ||
                //    iApp.DesignStandard == eDesignStandard.LRFDStandard
                //    )
                //    LONG_GIRDER_LL_TXT();
                //else
                //    LONG_GIRDER_BRITISH_LL_TXT();

                //CreateData_2D();
                //CreateData_2D_Left();
                //CreateData_2D_Right();
                //CreateData_Bridge_Deck();
                CreateData_Total_Structure();
                CreateData_Total_Structure_LL();

                Bridge_Analysis = new BridgeMemberAnalysis(iApp, Input_Data);
                Calculate_Total_Weight();



                ////CS_Analysis = new CABLE_STAYED_LS_Analysis(iApp);
                ////CS_Analysis.Input_File = Input_Data_Linear;


                    //LONG_GIRDER_LL_TXT();



                //CS_Analysis.TotalAnalysis_Input_File;



                Ana_Write_Long_Girder_Load_Data(CS_Analysis.DeadLoadAnalysis_Input_File, false, true, 1);
                Ana_Write_Long_Girder_Load_Data(CS_Analysis.TotalAnalysis_Input_File, true, true, 1);



                //File.Copy(CS_Analysis.DeadLoadAnalysis_Input_File, Input_Data_Nonlinear);


                File.WriteAllLines(Input_Data_Nonlinear, File.ReadAllLines(CS_Analysis.DeadLoadAnalysis_Input_File));

                //CS_Analysis.WriteData_DeadLoad_Analysis(CS_Analysis.DeadLoadAnalysis_Input_File);

                Ana_Write_Long_Girder_Load_Data(CS_Analysis.Input_File, true, true);
                Ana_Write_Long_Girder_Load_Data(CS_Analysis.TotalAnalysis_Input_File, true, true);

                for (int i = 0; i < all_loads.Count; i++)
                {
                    Ana_Write_Long_Girder_Load_Data(CS_Analysis.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
                       
                }
                //Ana_Write_Long_Girder_Load_Data(Long_Girder_Analysis.LiveLoadAnalysis_Input_File, true, false);



                #region Chiranjit [2014 10 22]

                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add("DEAD LOAD ANALYSIS");
                //cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS");
                for (int i = 0; i < all_loads.Count; i++)
                {
                    Ana_Write_Long_Girder_Load_Data(CS_Analysis.Get_LL_Analysis_Input_File(i + 1), true, false, i + 1);
                    //cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                    if (ll_comb.Count == all_loads.Count)
                    {
                        cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1) + " (" + ll_comb[i] + ")");
                    }
                    else
                        cmb_long_open_file.Items.Add("LIVE LOAD ANALYSIS " + (i + 1));
                }
                cmb_long_open_file.Items.Add("DL + LL ANALYSIS");
                Ana_Write_Long_Girder_Load_Data(CS_Analysis.TotalAnalysis_Input_File, true, true, DL_LL_Comb_Load_No);


                #endregion Chiranjit [2014 10 22]

                //CS_Analysis.LoadReadFromGrid(dgv_long_liveloads);

            }
            catch(Exception exx)
            {
                MessageBox.Show("Wrong Input Data", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Write_Ana_Load_Data();



            string ll_txt = LiveLoad_File;
            if (Live_Load_List == null) Live_Load_List = new List<LoadData>();

            Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Live_Load_List == null) return;

            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);

            Button_Enable_Disable();

            //if (!File.Exists(CSB_DATA_File))
            //{
            //    string sf = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Cable Stayed Bridge\" + Path.GetFileName(CSB_DATA_File));

            //    if (File.Exists(sf))
            //        File.Copy(sf, CSB_DATA_File);
            //    ReadData();
            //}

            WriteData();

            cmb_structure_file.SelectedIndex = 0;
            cmb_long_open_file.SelectedIndex = 0;


           MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
              "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if (File.Exists(CSB_DATA_File))
            //{
            //    ReadData();
            //    Calculate_Total_Weight();
            //}
        }
        public int DL_LL_Comb_Load_No
        {
            get
            {
                return MyList.StringToInt(txt_dl_ll_comb.Text, 1);
            }
        }

        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            int opt = 0;
            string fl = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
            if (btn.Name == btn_Ana_view_structure.Name)
            {
                opt = 0;
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                opt = cmb_structure_file.SelectedIndex;
            }

            if (opt == 0)
            {
                if (File.Exists(fl)) iApp.OpenWork(fl, false);
            }
            //else if (opt == 1)
            //{
            //    if (File.Exists(Input_Data_2D_Left))
            //    {
            //        iApp.OpenWork(Input_Data_2D_Left, false);
            //    }
            //}
            //else if (opt == 2)
            //{
            //    if (File.Exists(Input_Data_2D_Right))
            //    {
            //        iApp.OpenWork(Input_Data_2D_Right, false);
            //    }
            //}
            //else if (opt == 3)
            //{
            //    if (File.Exists(Input_Data_Bridge_Deck))
            //    {
            //        iApp.OpenWork(Input_Data_Bridge_Deck, false);
            //    }
            //}
        }



        private void txt_Section_Property_TextChanged(object sender, EventArgs e)
        {

        }
        private void frm_Cable_Stayed_Load(object sender, EventArgs e)
        {
            try
            {
                Set_Project_Name();
                //tbl_rolledSteelBeams = new TableRolledSteelBeams();
                //tbl_rolledSteelChannels = new TableRolledSteelChannels();
                //tbl_rolledSteelAngles = new TableRolledSteelAngles();
                cmb_select_standard.SelectedIndex = 1;
                //Read_Angle_Sections(cmb_Long_ang_section_name);
                //Read_Angle_Sections(cmb_cross_ang_section_name);

                uC_Sections1.iApp = iApp;
                uC_Sections2.iApp = iApp;

                txt_XINCR.Text = "10.0";
                if (iApp.Tables.DesignStandard == eDesignStandard.BritishStandard)
                {
                    iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_Long_ang_section_name, true);
                    iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_Long_ang_section_name, false);
                    iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_cross_ang_section_name, true);
                    iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_cross_ang_section_name, false);
                }
                else
                {
                    iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_Long_ang_section_name, true);
                    iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_Long_ang_section_name, false);

                    iApp.Tables.IS_SteelAngles.Read_Angle_Sections(ref cmb_cross_ang_section_name, true);
                    iApp.Tables.BS_SteelAngles.Read_Angle_Sections(ref cmb_cross_ang_section_name, false);
                }




                cmb_Long_ang_section_name.SelectedIndex = 0;
                cmb_Long_nos_ang.SelectedIndex = 1;

                cmb_cross_ang_section_name.SelectedIndex = 0;
                cmb_cross_nos_ang.SelectedIndex = 1;






                tc_analysis.TabPages.Remove(tab_cable_design);
                tc_analysis.TabPages.Remove(tab_deck_Structure_Design);
                tc_analysis.TabPages.Remove(tab_drawing);
                tc_analysis.TabPages.Remove(tab_abutment_Design);
                tc_analysis.TabPages.Remove(tab_deck_slab_BS);
                tc_analysis.TabPages.Remove(tab_deck_slab_IS);
                tc_analysis.TabPages.Remove(tab_moving_load);

                tc_main.TabPages.Add(tab_cable_design);
                tc_main.TabPages.Add(tab_deck_Structure_Design);


                //if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    tc_main.TabPages.Add(tab_deck_slab_BS);
                //else
                //    tc_main.TabPages.Add(tab_deck_slab_IS);

                //tc_main.TabPages.Add(tab_abutment_Design);
                tc_main.TabPages.Add(tab_drawing);



                //tc_main.TabPages.Remove(tab_tower_ana);


                if (iApp.DesignStandard == eDesignStandard.IndianStandard || iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    tc_analysis.TabPages.Remove(tab_moving_load_BS);
                else
                    tc_analysis.TabPages.Remove(tab_moving_load_IS);


                tabControl2.TabPages.Remove(tabPage4);


                uC_RCC_Abut1.iApp = iApp;
                uC_Superstructure1.Loads_Suspension_Sections(iApp);

                #region Add Limit State Method Live Loads

                Default_Moving_LoadData(dgv_long_liveloads);
                Default_Moving_Type_LoadData(dgv_long_loads);



                cmb_HB.SelectedIndex = 0;
                British_Interactive();

                //tc_limit_design.TabPages.Remove(tab_deck_slab);

                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    cmb_long_open_file.Items.Clear();
                    cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                    cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                    cmb_long_open_file.Items.Add(string.Format("DL + LL COMBINE ANALYSIS"));
                    //cmb_long_open_file.Items.Add(string.Format("LONGITUDINAL GIRDER ANALYSIS RESULTS"));

                    //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
                    //tc_limit_design.TabPages.Remove(tab_deck_slab_IS);

                }
                #endregion Add Limit State Method Live Loads
    


                #region Deckslab
                uC_Deckslab_BS1.iApp = iApp;
                #endregion Deckslab
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ASTRA", MessageBoxButtons.OK);
            }


            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            IsCreateData = true;
            Results = new List<string>();
            Button_Enable_Disable();
            Set_Default_Section_Property();
            dgv_Ana_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -600.0, 0, 2.0, 10.0);
            dgv_Ana_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -600.0, 0, 6.0, 10.0);

            txt_Ana_analysis_file.Text = "";
            txt_cbl_des_Ax.Text = Cable_Ax.ToString("F3");



            dgv_SIDL.Rows.Add("DECK_SLAB1", 121, 13.15, 0.4, 2, 0.0, 2.4);
            dgv_SIDL.Rows.Add("DECK_SLAB2", 350, 13.15, 0.225, 1, 0.0, 2.4);

            dgv_SIDL.Rows.Add("WEARING_COURSE1", 121, 13.15, 0.4, 2, 0.0, 2.3);
            dgv_SIDL.Rows.Add("WEARING_COURSE2", 350, 13.15, 0.225, 1, 0.0, 2.3);
            Format_SIDL();
            cmb_applied_section.SelectedIndex = 0;


            Change_Cable_numbers();



            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                //    //IsCreate_Data = false;

                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                //    string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");
                //    //Chiranjit [2013 07 28]
                //    iApp.Read_Form_Record(this, user_path);


                //    Input_Data = chk_file;
                //    user_path = Path.GetDirectoryName(Input_Data);
                //    Open_AnalysisFile();
                //    IsCreateData = false;
                //    Show_ReadMemberLoad(Input_Data);
                //    MessageBox.Show(this, "File opened succesfully.", "ASTRA", MessageBoxButtons.OK);

                //}

                Button_Enable_Disable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option




        }
        private void dgv_section_property_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgv_section_property.RowCount; i++)
                {
                    dgv_section_property[0, i].Value = (i + 1);
                }
            }
            catch (Exception ex) { }

            Calculate_Total_Weight();
            if (dgv_section_property.RowCount > 0)
                Select_Section_Prop(dgv_section_property[1, e.RowIndex].Value.ToString());
        }

        private string Get_LongGirder_File(int index)
        {
            string file_name = "";

            if (iApp.DesignStandard == eDesignStandard.IndianStandard ||
                iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                if (index == 0)
                {
                    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                }
                else if (index == all_loads.Count + 1)
                {
                    file_name = CS_Analysis.TotalAnalysis_Input_File;
                }
                else
                {
                    file_name = CS_Analysis.Get_LL_Analysis_Input_File(index);
                }
            }
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                if (index == 0)
                {
                    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                }
                else if (index == all_loads.Count + 1)
                {
                    file_name = CS_Analysis.TotalAnalysis_Input_File;
                }
                else
                {
                    file_name = CS_Analysis.Get_LL_Analysis_Input_File(index);
                }

                //if (index == -1) return "";
                //string item = cmb_long_open_file.Items[index].ToString();

                //if (item.StartsWith("DL + LL") || item.StartsWith("TOTAL"))
                //{
                //    file_name = CS_Analysis.TotalAnalysis_Input_File;
                //}
                //else if (item.StartsWith("DEAD LOAD"))
                //{
                //    file_name = CS_Analysis.DeadLoadAnalysis_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 1"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_1_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 2"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_2_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 3"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_3_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 4"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_4_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS 5"))
                //{
                //    file_name = CS_Analysis.LL_Analysis_5_Input_File;
                //}
                //else if (item.StartsWith("LIVE LOAD ANALYSIS"))
                //{
                //    file_name = CS_Analysis.LiveLoadAnalysis_Input_File;
                //}
                //else if (item.StartsWith("LONGITUDINAL GIRDER ANALYSIS RESULTS"))
                //{
                //    file_name = File_Long_Girder_Results;
                //}
            }
            return file_name;
        }

        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {


            #region [2016 06 27]

            try
            {
                #region Process
                int i = 0;
                //Write_All_Data(true);



                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = CS_Analysis.Input_File;
                iApp.Progress_Works.Clear();
                do
                {
                    flPath = Get_LongGirder_File(i);

                    if (File.Exists(flPath))
                    {
                        pd = new ProcessData();
                        pd.Process_File_Name = flPath;
                        pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                        pcol.Add(pd);
                        iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                    }


                    i++;
                }
                while (i < ((iApp.DesignStandard == eDesignStandard.IndianStandard || iApp.DesignStandard == eDesignStandard.LRFDStandard) ? (all_loads.Count + 2) : (all_loads.Count + 3)));
                //while (i < 3) ;




                //frm_LS_Process ff = new frm_LS_Process(pcol);
                //ff.Owner = this;
                ////MessageBox.Show(ff.ShowDialog().ToString());
                //if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;



                //string ana_rep_file = CS_Analysis.Analysis_Report;
                string ana_rep_file =  Analysis_Report;
                if (iApp.Show_and_Run_Process_List(pcol))
                {
                    //iApp.Progress_Works.Clear();
                    //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 1 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 2 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 3 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 4 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 5 Report File (ANALYSIS_REP.TXT)");

                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 6 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");

                    CS_Analysis.TotalLoad_Analysis = null;


                    //CS_Analysis.LiveLoad_Analysis = new BridgeMemberAnalysis(iApp, CS_Analysis.LiveLoad_Analysis_Report);


                    //CS_Analysis.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, CS_Analysis.Dead);

                    if (rbtn_HA.Checked == false)
                    {
                        CS_Analysis.All_LL_Analysis = new List<BridgeMemberAnalysis>();

                        CS_Analysis.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(CS_Analysis.DeadLoadAnalysis_Input_File));

                        for (i = 0; i < all_loads.Count; i++)
                        {
                            string fn = MyList.Get_Analysis_Report_File(CS_Analysis.Get_LL_Analysis_Input_File(i + 1));
                            if (File.Exists(fn))
                            {
                                CS_Analysis.All_LL_Analysis.Add(new BridgeMemberAnalysis(iApp, fn));
                            }
                        }

                        //CS_Analysis.LiveLoad_1_Analysis = new BridgeMemberAnalysis(iApp,
                        //        CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_1_Input_File));

                        //CS_Analysis.LiveLoad_2_Analysis = new BridgeMemberAnalysis(iApp,
                        //    CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_2_Input_File));

                        //CS_Analysis.LiveLoad_3_Analysis = new BridgeMemberAnalysis(iApp,
                        //    CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_3_Input_File));

                        //CS_Analysis.LiveLoad_4_Analysis = new BridgeMemberAnalysis(iApp,
                        //    CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_4_Input_File));


                        //CS_Analysis.LiveLoad_5_Analysis = new BridgeMemberAnalysis(iApp,
                        //    CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_5_Input_File));
                    }
                    CS_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, MyList.Get_Analysis_Report_File(CS_Analysis.TotalAnalysis_Input_File));

                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    CS_Analysis.LiveLoad_6_Analysis = new BridgeMemberAnalysis(iApp,
                    //    CS_Analysis.Get_Analysis_Report_File(CS_Analysis.LL_Analysis_6_Input_File));

                    CS_Analysis.LiveLoad_Analysis = CS_Analysis.All_LL_Analysis[DL_LL_Comb_Load_No - 1];

                    if (!iApp.Is_Progress_Cancel)
                    {
                        //Show_Long_Girder_Moment_Shear();
                        if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        {
                            //Show_Long_Girder_Moment_Shear();
                        }
                        else
                        {
                            CS_Analysis.LiveLoad_1_Analysis = CS_Analysis.All_LL_Analysis[0];
                            CS_Analysis.LiveLoad_2_Analysis = CS_Analysis.All_LL_Analysis[1];
                            CS_Analysis.LiveLoad_3_Analysis = CS_Analysis.All_LL_Analysis[2];
                            CS_Analysis.LiveLoad_4_Analysis = CS_Analysis.All_LL_Analysis[3];
                            CS_Analysis.LiveLoad_5_Analysis = CS_Analysis.All_LL_Analysis[4];
                            //Show_British_Long_Girder_Moment_Shear();
                        }
                    }
                    else
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;
                    }

                }

                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                //grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                //grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;




                iApp.Progress_Works.Clear();
                iApp.Progress_OFF();

                Button_Enable_Disable();
                //Write_All_Data(false);

                FillMemberResult();

                #endregion Process

                if (File.Exists(Analysis_Report))
                {
                    try
                    {
                        Bridge_Analysis = null;
                        Bridge_Analysis = new BridgeMemberAnalysis(iApp, Analysis_Report);
                        FillMemberResult();

                        WriteData();
                        Button_Enable_Disable();
                        MessageBox.Show(this, "Analysis Result created in file " + Analysis_Result_Report, "ASTRA", MessageBoxButtons.OK);
                        iApp.RunExe(Analysis_Result_Report);
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex) { }

            return;

            #endregion [2016 06 27]







            //string flPath = Input_Data;
            //File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            ////System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            //System.Diagnostics.Process prs = new System.Diagnostics.Process();

            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            //System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");

            //if (!File.Exists(prs.StartInfo.FileName))
            //{
            //    MessageBox.Show("AST001cable.EXE module not found in the Application Folder \n\r\n\r \"" + Application.StartupPath + "\".", "ASTRA", MessageBoxButtons.OK);
            //    return;
            //}
            //if (prs.Start())
            //    prs.WaitForExit();
            Process_Analysis();
            if (File.Exists(Analysis_Report))
            {
                try
                {
                    Bridge_Analysis = null;
                    Bridge_Analysis = new BridgeMemberAnalysis(iApp, Analysis_Report);
                    FillMemberResult();

                    WriteData();
                    Button_Enable_Disable();
                    MessageBox.Show(this, "Analysis Result created in file " + Analysis_Result_Report, "ASTRA", MessageBoxButtons.OK);
                    iApp.RunExe(Analysis_Result_Report);
                }
                catch (Exception ex) { }
            }
        }

        private bool Process_Analysis()
        {
            try
            {
                #region Process

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = Input_Data;
                iApp.Progress_Works.Clear();

                flPath = Input_Data;

                if (File.Exists(flPath))
                {
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                }

                flPath = Input_Data_2D;

                if (File.Exists(flPath))
                {
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                }

                flPath = Input_Data_2D_Left;

                if (File.Exists(flPath))
                {
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                }

                flPath = Input_Data_2D_Right;

                if (File.Exists(flPath))
                {
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                }


                flPath = Input_Data_Bridge_Deck;

                if (File.Exists(flPath))
                {
                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);
                    //iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                }


                if (iApp.Show_and_Run_Process_List(pcol))
                {
                    //if (!iApp.Is_Progress_Cancel)
                    //{
                    //}
                    //else
                    //{
                    //}
                }

                iApp.Progress_Works.Clear();
                iApp.Progress_OFF();

                #endregion Process
            }
            catch (Exception ex) { }


            return true;
        }

        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {
            string inp_file = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
            if (File.Exists(inp_file))
            {
                iApp.View_Input_File(inp_file);
            }
            return;

            if (cmb_long_open_file.SelectedIndex == 0)
            {
                if (File.Exists(inp_file))
                {
                    iApp.View_Input_File(inp_file);
                }
            }
            else if (cmb_long_open_file.SelectedIndex == 1)
            {
                if (File.Exists(Input_Data_2D_Left))
                {
                    iApp.View_Input_File(Input_Data_2D_Left);
                }
            }
            else if (cmb_long_open_file.SelectedIndex == 2)
            {
                if (File.Exists(Input_Data_2D_Right))
                {
                    iApp.View_Input_File(Input_Data_2D_Right);
                }
            }
            else if (cmb_long_open_file.SelectedIndex == 3)
            {
                if (File.Exists(Input_Data_Bridge_Deck))
                {
                    iApp.View_Input_File(Input_Data_Bridge_Deck);
                }
            }
        }

        private void btn_Ana_view_report_Click(object sender, EventArgs e)
        {
            frm_CSB_Result_option frm = new frm_CSB_Result_option();


            string fileName = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);

            string rep = MyList.Get_Analysis_Report_File(fileName);


            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.Is_Full_Analysis_Report)
                {
                    iApp.RunExe(rep);
                }
                else if (frm.Is_Analysis_Result)
                    iApp.RunExe(Analysis_Result_Report);
                else if (frm.Is_Cable_Analysis_Result)
                    iApp.RunExe(Cable_Design_Report);

            }


        }
        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {

            MemberSectionProperty msec = new MemberSectionProperty();
            try
            {
                msec = GetSectionProperty();
                //msec.AppliedSection = Section_Applied;
                if (Section_Applied == eAppliedSection.Reactangular_Section ||
                    Section_Applied == eAppliedSection.Circular_Section)
                {
                    msec.Calculate_Moment_Of_Inertia();

                    Section_AX = msec.AX_Area;
                    Section_IX = msec.IX;
                    Section_IY = msec.IY;
                    Section_IZ = msec.IZ;
                    Section_Weight = msec.Weight;
                }
                else
                {
                    Calculate_Moment_Of_Inertia();
                }
                Calculate_Total_Weight();
            }
            catch (Exception ex) { }
        }

        private void btnSectionApply_Click(object sender, EventArgs e)
        {
            try
            {
                Calculate();
                Add_Section_Property();

                string kStr = "";
                for (int i = 0; i < dgv_section_property.Rows.Count; i++)
                {
                    kStr = dgv_section_property[1, i].Value.ToString();

                    if (kStr.ToUpper() == cmb_group_name.Text)
                    {
                        int indx = 2;
                        dgv_section_property[indx, i].Value = txt_sec_L.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_B.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_D.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_dia.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_thickness.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_unit_weight.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_total_nos.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_AX.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_IX.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_IY.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_IZ.Text; indx++;
                        dgv_section_property[indx, i].Value = txt_sec_weight.Text; indx++;
                        return;
                    }
                }

                dgv_section_property.Rows.Add(dgv_section_property.Rows.Count + 1,
                    cmb_group_name.Text,
                    txt_sec_L.Text == "" ? "0.0" : txt_sec_L.Text,
                    txt_sec_B.Text == "" ? "0.0" : txt_sec_B.Text,
                    txt_sec_D.Text == "" ? "0.0" : txt_sec_D.Text,
                    txt_sec_dia.Text == "" ? "0.0" : txt_sec_dia.Text,
                    txt_sec_thickness.Text == "" ? "0.0" : txt_sec_thickness.Text,
                    txt_sec_unit_weight.Text == "" ? "0.0" : txt_sec_unit_weight.Text,
                    txt_sec_total_nos.Text == "" ? "0.0" : txt_sec_total_nos.Text,
                    txt_sec_AX.Text == "" ? "0.0" : txt_sec_AX.Text,
                    txt_sec_IX.Text == "" ? "0.0" : txt_sec_IX.Text,
                    txt_sec_IY.Text == "" ? "0.0" : txt_sec_IY.Text,
                    txt_sec_IZ.Text == "" ? "0.0" : txt_sec_IZ.Text,
                    txt_sec_weight.Text == "" ? "0.0" : txt_sec_weight.Text);

                Calculate_Total_Weight();
            }
            catch (Exception ex) { }
        }
        private void txt_sec_radius_Leave(object sender, EventArgs e)
        {
            double dval = MyList.StringToDouble(((TextBox)sender).Text, 0.0);


            if (((TextBox)sender).Name == txt_sec_B.Name)
            {
                if (dval != 0)
                {
                    txt_sec_dia.Text = "0.0";
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.BridgeImages.Rectangle;
                }
            }
            if (((TextBox)sender).Name == txt_sec_D.Name)
            {
                if (dval != 0)
                {
                    txt_sec_dia.Text = "0.0";
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.BridgeImages.Rectangle;
                }
            }
            if (((TextBox)sender).Name == txt_sec_dia.Name)
            {
                if (dval != 0)
                {
                    txt_sec_B.Text = "0.0";
                    txt_sec_D.Text = "0.0";
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.BridgeImages.Circular;
                }
            }
            //if (((TextBox)sender).Name == txt_sec_thickness.Name)
            //{
            //    if (dval != 0)
            //    {
            //        txt_sec_B.Text = "0.0";
            //        txt_sec_D.Text = "0.0";
            //        pb_sections.BackgroundImage = global::BridgeAnalysisDesign.BridgeImages.Pylon;
            //    }
            //}
        }
        private void btn_sec_remove_Click(object sender, EventArgs e)
        {
            try
            {

                if (((Button)sender).Name == btn_sec_remove.Name)
                {
                    dgv_section_property.Rows.RemoveAt(dgv_section_property.CurrentCell.RowIndex);
                    Delete_Section_Property();
                }
                else
                {
                    dgv_section_property.Rows.Clear();
                    SectionProperty.Clear();
                }
            }
            catch (Exception ex) { }
        }

        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt"; ofd.InitialDirectory = user_path;
                    ofd.InitialDirectory = user_path;
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        Input_Data = ofd.FileName;
                        user_path = Path.GetDirectoryName(Input_Data);
                        Open_AnalysisFile();
                        IsCreateData = false;
                        Show_ReadMemberLoad(Input_Data);
                        MessageBox.Show(this, "File opened succesfully.", "ASTRA", MessageBoxButtons.OK);
                    }
                }
                Button_Enable_Disable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
        }
        private void rbtn_create_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;
        }
        private void btn_mem_ana_Click(object sender, EventArgs e)
        {
            CableMember cbl = null;

            Results.Clear();
            #region TechSOFT Banner
            Results.Add("");
            Results.Add("");
            Results.Add("\t\t**********************************************");
            Results.Add("\t\t*            ASTRA Pro Release 21            *");
            Results.Add("\t\t*        TechSOFT Engineering Services       *");
            Results.Add("\t\t*                                            *");
            Results.Add("\t\t*        DESIGN OF STAY CABLE MEMBERS        *");
            Results.Add("\t\t*                                            *");
            Results.Add("\t\t**********************************************");
            Results.Add("\t\t----------------------------------------------");
            Results.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            Results.Add("\t\t----------------------------------------------");

            #endregion
            Read_Cable_Member();
            dgv_cable_design.Rows.Clear();
            for (int i = 0; i < Cable_Members.Count; i++)
            {
                cbl = Cable_Members[i];
                Design_Cables(ref cbl);
                Results.AddRange(cbl.DesignResult.ToArray());
                dgv_cable_design.Rows.Add(cbl.ToArray());
            }
            Results.Add("\t\t----------------------------------------------");
            Results.Add("\t\tTHIS RESULT ENDED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            Results.Add("\t\t----------------------------------------------");

            File.WriteAllLines(Cable_Design_Report, Results.ToArray());
            Results.Clear();

            iApp.View_Result(Cable_Design_Report);
            //try
            //{
            WriteData();
            Button_Enable_Disable();

        }
        private void btn_cbl_des_read_data_Click(object sender, EventArgs e)
        {
            Read_Cable_Member();
            WriteData();
        }
        private void txt_cbl_des_mem_nos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Read_Cable_Member();
        }

        private void dgv_cable_design_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string s = dgv_cable_design[1, e.RowIndex].Value.ToString();
                int i = MyList.StringToInt(s, -1);
                if (i != -1)
                {
                    int indx = Cable_Members.IndexOf(i);

                    if (indx != -1)
                    {
                        if (Cable_Members[indx].DesignResult.Count > 0)
                        {
                            File.WriteAllLines(Path.Combine(user_path, "cbl.tmp"), Cable_Members[indx].DesignResult.ToArray());
                            iApp.View_Result(Path.Combine(user_path, "cbl.tmp"));
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            timer1.Interval = 2799;
            switch (img_counter)
            {
                case 0:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Cable_Stayed_Bridge;
                    img_counter++;
                    break;
                case 1:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.BridgeImages.CSB1;
                    img_counter++;
                    break;
                case 2:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.BridgeImages.CSB2;
                    img_counter++;
                    break;
                case 3:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.BridgeImages.CSB3;
                    img_counter = 0;
                    break;
            }

        }

        private void txt_cbl_des_d_TextChanged(object sender, EventArgs e)
        {
            txt_cbl_des_Ax.Text = Cable_Ax.ToString("E3");
        }

        private void cmb_section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                if (cmb.Name == cmb_section_name.Name)
                {
                    cmb_code1.Items.Clear();
                    if (cmb.Text.Contains("B"))
                    {
                        cmb_sec_thk.Visible = false;

                        foreach (var item in tbl_rolledSteelBeams.List_Table)
                        {
                            if (item.SectionName == cmb.Text)
                            {
                                if (!cmb_code1.Items.Contains(item.SectionCode))
                                    cmb_code1.Items.Add(item.SectionCode);
                            }
                        }
                    }
                    else if (cmb.Text.Contains("C"))
                    {
                        cmb_sec_thk.Visible = false;

                        foreach (var item in tbl_rolledSteelChannels.List_Table)
                        {
                            if (item.SectionName == cmb.Text)
                            {
                                if (!cmb_code1.Items.Contains(item.SectionCode))
                                    cmb_code1.Items.Add(item.SectionCode);
                            }
                        }
                    }
                    else if (cmb.Text.Contains("A"))
                    {
                        cmb_sec_thk.Visible = true;

                        foreach (var item in tbl_rolledSteelAngles.List_Table)
                        {
                            if (item.SectionName == cmb.Text)
                            {
                                if (!cmb_code1.Items.Contains(item.SectionSize))
                                    cmb_code1.Items.Add(item.SectionSize);
                            }
                        }
                    }
                    cmb_code1.SelectedIndex = cmb_code1.Items.Count > 0 ? 0 : -1;
                }
                else if (cmb.Name == cmb_code1.Name)
                {
                    if (cmb_section_name.Text.Contains("A"))
                    {
                        cmb_sec_thk.Items.Clear();
                        cmb_sec_thk.Visible = true;

                        foreach (var item in tbl_rolledSteelAngles.List_Table)
                        {
                            if (item.SectionName == cmb_section_name.Text &&
                                item.SectionSize == cmb_code1.Text)
                            {
                                cmb_sec_thk.Items.Add(item.Thickness);
                            }
                        }
                        cmb_sec_thk.SelectedIndex = cmb_sec_thk.Items.Count > 0 ? 1 : -1;
                    }
                }
            }
            catch (Exception ex) { }
            Calculate_Moment_Of_Inertia();


        }
        private void cmb_section_name_SelectedIndexChanged_OLD(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.Name == cmb_section_name.Name)
            {
                cmb_code1.Items.Clear();
                if (cmb.Text.Contains("B"))
                {
                    cmb_sec_thk.Visible = false;

                    foreach (var item in tbl_rolledSteelBeams.List_Table)
                    {
                        if (item.SectionName == cmb.Text)
                        {
                            if (!cmb_code1.Items.Contains(item.SectionCode))
                                cmb_code1.Items.Add(item.SectionCode);
                        }
                    }
                }
                else if (cmb.Text.Contains("C"))
                {
                    cmb_sec_thk.Visible = false;

                    foreach (var item in tbl_rolledSteelChannels.List_Table)
                    {
                        if (item.SectionName == cmb.Text)
                        {
                            if (!cmb_code1.Items.Contains(item.SectionCode))
                                cmb_code1.Items.Add(item.SectionCode);
                        }
                    }
                }
                else if (cmb.Text.Contains("A"))
                {
                    cmb_sec_thk.Visible = true;

                    foreach (var item in tbl_rolledSteelAngles.List_Table)
                    {
                        if (item.SectionName == cmb.Text)
                        {
                            if (!cmb_code1.Items.Contains(item.SectionSize))
                                cmb_code1.Items.Add(item.SectionSize);
                        }
                    }
                }
                cmb_code1.SelectedIndex = cmb_code1.Items.Count > 0 ? 0 : -1;
            }
            else if (cmb.Name == cmb_code1.Name)
            {
                if (cmb_section_name.Text.Contains("A"))
                {
                    cmb_sec_thk.Items.Clear();
                    cmb_sec_thk.Visible = true;

                    foreach (var item in tbl_rolledSteelAngles.List_Table)
                    {
                        if (item.SectionName == cmb_section_name.Text &&
                            item.SectionSize == cmb_code1.Text)
                        {
                            cmb_sec_thk.Items.Add(item.Thickness);
                        }
                    }
                    cmb_sec_thk.SelectedIndex = cmb_sec_thk.Items.Count > 0 ? 1 : -1;
                }
                else if (cmb_section_name.Text.Contains("C"))
                {
                    //cmb_sec_thk.Items.Clear();
                    //cmb_sec_thk.Visible = true;

                    foreach (var item in tbl_rolledSteelChannels.List_Table)
                    {
                        if (item.SectionName == cmb_section_name.Text &&
                            item.SectionCode == cmb_code1.Text)
                        {
                            txt_sec_AX.Text = item.Area.ToString();
                            txt_sec_IX.Text = item.Ixx.ToString();
                            txt_sec_IY.Text = item.Iyy.ToString();
                            txt_sec_IZ.Text = (item.Ixx + item.Iyy).ToString();
                            txt_sec_unit_weight.Text = item.Weight.ToString();
                            break;
                        }
                    }
                }
                else if (cmb_section_name.Text.Contains("B"))
                {
                    //cmb_sec_thk.Items.Clear();
                    //cmb_sec_thk.Visible = true;

                    foreach (var item in tbl_rolledSteelBeams.List_Table)
                    {
                        if (item.SectionName == cmb_section_name.Text &&
                            item.SectionCode == cmb_code1.Text)
                        {
                            txt_sec_AX.Text = item.Area.ToString();
                            txt_sec_IX.Text = item.Ixx.ToString();
                            txt_sec_IY.Text = item.Iyy.ToString();
                            txt_sec_IZ.Text = (item.Ixx + item.Iyy).ToString();
                            txt_sec_unit_weight.Text = item.Weight.ToString();
                            break;
                        }
                    }
                }
            }
            else if (cmb.Name == cmb_sec_thk.Name)
            {
                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionName == cmb_section_name.Text &&
                        item.SectionSize == cmb_code1.Text &&
                        item.Thickness.ToString() == cmb_sec_thk.Text)
                    {
                        txt_sec_AX.Text = item.Area.ToString();
                        txt_sec_IX.Text = item.Ixx.ToString();
                        txt_sec_IY.Text = item.Iyy.ToString();
                        txt_sec_IZ.Text = (item.Ixx + item.Iyy).ToString();
                        txt_sec_unit_weight.Text = item.Weight.ToString();
                        break;
                    }
                }
            }

        }
        private void cmb_applied_section_SelectedIndexChanged(object sender, EventArgs e)
        {

            lbl_gamma_unit.Text = (cmb_applied_section.SelectedIndex == 0) ? "Ton/m" : "Ton/m^3";

            grb_sec_rectangular.Visible = false;
            grb_sec_steel.Visible = false;

            //grb_sec_rectangular.Visible = true;
            //grb_sec_steel.Visible = true;

            if (Section_Applied == eAppliedSection.Reactangular_Section)
            {
                grb_sec_rectangular.Visible = true;
                grb_sec_steel.Visible = false;

                Section_Depth = 0;
                Section_Breadth = 0;
                Section_Diameter = 0;

                txt_sec_dia.Enabled = false;
                txt_sec_B.Enabled = true;
                txt_sec_D.Enabled = true;
            }
            if (Section_Applied == eAppliedSection.Circular_Section)
            {
                Section_Depth = 0;
                Section_Breadth = 0;
                Section_Diameter = 0;
                txt_sec_dia.Enabled = true;
                txt_sec_B.Enabled = false;
                txt_sec_D.Enabled = false;

                grb_sec_rectangular.Visible = true;
                grb_sec_steel.Visible = false;
            }
            grb_sec_steel.Visible = !grb_sec_rectangular.Visible;

            pnl_section.Visible = true;
            switch (Section_Applied)
            {
                case eAppliedSection.Angle_Section1:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Angle_Section1;
                    Read_Angle_Sections();
                    txt_no_ele.Text = "4";
                    break;
                case eAppliedSection.Angle_Section2:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Angle_Section2;
                    Read_Angle_Sections();
                    txt_no_ele.Text = "4";
                    break;
                case eAppliedSection.Angle_Section3:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Bottom_Chord_Bracing;
                    Read_Angle_Sections();
                    txt_no_ele.Text = "4";
                    break;
                case eAppliedSection.Beam_Section1:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Beam_Section1;
                    Read_Beam_Sections();
                    txt_no_ele.Text = "2";
                    break;
                case eAppliedSection.Beam_Section2:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Beam_Section2;
                    Read_Beam_Sections();
                    txt_no_ele.Text = "1";
                    break;
                case eAppliedSection.Channel_Section1:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Channel_Section1;
                    Read_Channel_Sections();
                    txt_no_ele.Text = "2";
                    break;
                case eAppliedSection.Channel_Section2:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Channel_Section2;
                    Read_Channel_Sections();
                    txt_no_ele.Text = "2";
                    break;
                case eAppliedSection.Reactangular_Section:
                    //pb_sections.BackgroundImage = Properties.Resources.Rectangle;
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Pylon;
                    break;
                case eAppliedSection.Circular_Section:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Circular;
                    break;
                case eAppliedSection.Builtup_LongGirder:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Builtup_Long;
                    pnl_section.Visible = false;
                    Read_Angle_Sections();
                    break;
                case eAppliedSection.Builtup_CrossGirder:
                    pb_sections.BackgroundImage = BridgeAnalysisDesign.BridgeImages.Builtup_Cross;
                    pnl_section.Visible = false;
                    break;
            }
            if (cmb_section_name.Items.Count > 0)
            {
                cmb_section_name.SelectedIndex = 0;
            }

            cmb_code1.Width = (cmb_sec_thk.Visible) ? 104 : 154;
        }

        private void dgv_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Format_SIDL();
            Calculate_Total_Weight();
        }

        private void txt_weight_factor_TextChanged(object sender, EventArgs e)
        {
            Calculate_Total_Weight();
        }

        private void btn_drawing_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Drawing_Folder, "Cable_Stayed_Bridge");
        }
        #endregion Form Events


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_remove_all_Click(object sender, EventArgs e)
        {
            dgv_SIDL.Rows.Clear();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (dgv_SIDL.CurrentCell.RowIndex >= 0)
                dgv_SIDL.Rows.RemoveAt(dgv_SIDL.CurrentCell.RowIndex);
        }

        private void cmb_Long_ang_section_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = sender as ComboBox;
            TableRolledSteelAngles tbl = iApp.Tables.IS_SteelAngles;
            if (cmb.Name == cmb_Long_ang_section_code.Name)
            {
                cmb_Long_ang_thk.Items.Clear();
                cmb_Long_ang_thk.Visible = true;


                tbl = cmb_Long_ang_section_name.Items.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;


                foreach (var item in tbl.List_Table)
                {
                    if (item.SectionSize == cmb_Long_ang_section_code.Text)
                    {
                        cmb_Long_ang_thk.Items.Add(item.Thickness);
                    }
                }
                cmb_Long_ang_thk.SelectedIndex = cmb_Long_ang_thk.Items.Count > 0 ? 0 : -1;
            }

            if (cmb.Name == cmb_cross_ang_section_code.Name)
            {
                cmb_Long_ang_thk.Items.Clear();
                cmb_Long_ang_thk.Visible = true;
                tbl = cmb_cross_ang_section_name.Items.Contains("IS") ? iApp.Tables.IS_SteelAngles : iApp.Tables.BS_SteelAngles;

                foreach (var item in tbl_rolledSteelAngles.List_Table)
                {
                    if (item.SectionSize == cmb_cross_ang_section_code.Text)
                    {
                        cmb_cross_ang_thk.Items.Add(item.Thickness);
                    }
                }
                cmb_cross_ang_thk.SelectedIndex = cmb_cross_ang_thk.Items.Count > 0 ? 0 : -1;
            }
        }

        private void cmb_convert_standard_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_convert_standard.SelectedIndex == 0)
            {
                for (int i = 0; i < iApp.Tables.Steel_Convert.Count; i++)
                {
                    if (iApp.Tables.Steel_Convert[i].IS_Section_Name == cmb_section_name.Text)
                    {
                        if (iApp.Tables.Steel_Convert[i].IS_Section_Code.Replace("X", "") == cmb_code1.Text.ToUpper().Replace("X", ""))
                        {
                            if (cmb_section_name.Text.EndsWith("A"))
                            {
                                if (iApp.Tables.Steel_Convert[i].IS_Angle_Thickness.ToString("0") == cmb_sec_thk.Text)
                                {
                                    try
                                    {
                                        cmb_select_standard.SelectedIndex = 0;

                                        cmb_section_name.SelectedItem = iApp.Tables.Steel_Convert[i].BS_Section_Name;
                                        cmb_code1.SelectedItem = iApp.Tables.Steel_Convert[i].BS_Section_Code;
                                        cmb_sec_thk.SelectedItem = iApp.Tables.Steel_Convert[i].BS_Angle_Thickness;
                                        return;
                                    }
                                    catch (Exception ex) { }
                                }
                            }
                            else
                            {

                                try
                                {
                                    cmb_select_standard.SelectedIndex = 0;
                                    cmb_section_name.SelectedItem = iApp.Tables.Steel_Convert[i].BS_Section_Name;
                                    cmb_code1.SelectedItem = iApp.Tables.Steel_Convert[i].BS_Section_Code;
                                    return;
                                }
                                catch (Exception ex) { }
                            }
                        }
                    }
                }
            }
            if (cmb_convert_standard.SelectedIndex == 1)
            {
                for (int i = 0; i < iApp.Tables.Steel_Convert.Count; i++)
                {
                    if (iApp.Tables.Steel_Convert[i].BS_Section_Name == cmb_section_name.Text)
                    {
                        if (iApp.Tables.Steel_Convert[i].BS_Section_Code.Replace("X", "").ToUpper() == cmb_code1.Text.ToUpper().Replace("X", "").ToUpper())
                        {
                            if (cmb_section_name.Text.EndsWith("A"))
                            {
                                if (iApp.Tables.Steel_Convert[i].BS_Angle_Thickness.ToString("0") == cmb_sec_thk.Text)
                                {
                                    try
                                    {
                                        cmb_select_standard.SelectedIndex = 1;

                                        cmb_section_name.SelectedItem = iApp.Tables.Steel_Convert[i].IS_Section_Name;
                                        cmb_code1.SelectedItem = MyList.GetSectionSize(iApp.Tables.Steel_Convert[i].IS_Section_Code);
                                        cmb_sec_thk.SelectedItem = iApp.Tables.Steel_Convert[i].IS_Angle_Thickness;
                                        return;
                                    }
                                    catch (Exception ex) { }
                                }
                            }
                            else
                            {

                                try
                                {
                                    cmb_select_standard.SelectedIndex = 1;
                                    cmb_section_name.SelectedItem = iApp.Tables.Steel_Convert[i].IS_Section_Name;
                                    cmb_code1.SelectedItem = iApp.Tables.Steel_Convert[i].IS_Section_Code;
                                    //cmb_code1.SelectedItem = iApp.Tables.Steel_Convert[i].IS_Section_Code;
                                    return;
                                }
                                catch (Exception ex) { }
                            }
                        }
                    }
                }
            }

        }

        private void cmb_ang_section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_Long_ang_section_name.Name)
            {
                cmb_Long_ang_section_code.Items.Clear();
                if (cmb_Long_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_Long_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_Long_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_Long_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_Long_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_Long_ang_section_code.Items.Count > 0)
                {
                    cmb_Long_ang_section_code.SelectedIndex = 0;
                    cmb_Long_ang_section_code.SelectedItem = "100X100";
                    cmb_Long_ang_thk.SelectedIndex = cmb_Long_ang_thk.Items.Contains(10.0) ? cmb_Long_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_Long_nos_ang.SelectedIndex = 1;
                }

            }

            if (cmb.Name == cmb_cross_ang_section_name.Name)
            {
                cmb_cross_ang_section_code.Items.Clear();
                if (cmb_cross_ang_section_name.Text.Contains("UK"))
                {
                    foreach (var item in iApp.Tables.BS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                else if (cmb_cross_ang_section_name.Text.Contains("IS"))
                {
                    foreach (var item in iApp.Tables.IS_SteelAngles.List_Table)
                    {
                        if (cmb_cross_ang_section_name.Text == item.SectionName)
                        {
                            if (cmb_cross_ang_section_code.Items.Contains(item.SectionSize) == false)
                                cmb_cross_ang_section_code.Items.Add(item.SectionSize);
                        }
                    }
                }
                if (cmb_cross_ang_section_code.Items.Count > 0)
                {
                    cmb_cross_ang_section_code.SelectedIndex = 0;
                    cmb_cross_ang_section_code.SelectedItem = "100X100";
                    cmb_cross_ang_thk.SelectedIndex = cmb_cross_ang_thk.Items.Contains(10.0) ? cmb_cross_ang_thk.Items.IndexOf(10.0) : 0;
                    cmb_cross_nos_ang.SelectedIndex = 1;
                }

            }
        }

        private void cmb_Ana_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (iApp.LiveLoads.Count > 0)
                {
                    txt_Ana_X.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Distance.ToString("f4");
                }
            }
            catch (Exception ex) { }
        }

        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = chk_M2.Checked;
            rft.M3 = chk_M3.Checked;
            rft.R3 = chk_R3.Checked;
            rft.R2 = chk_R2.Checked;
            return rft;
        }

        private void btn_update_force_Click(object sender, EventArgs e)
        {
            if (File.Exists(Analysis_Report))
            {
                Bridge_Analysis = null;
                Bridge_Analysis = new BridgeMemberAnalysis(iApp, Analysis_Report, GetForceType());
                FillMemberResult();
            }
            try
            {
                WriteData();
            }
            catch (Exception ex) { }
            Button_Enable_Disable();
        }

        private void btn__Click(object sender, EventArgs e)
        {
            frm_ProblemDescription f = new frm_ProblemDescription();
            f.Owner = this;
            f.Show();
        }

        private void btn_Ana_View_Moving_Load_Click(object sender, EventArgs e)
        {
            string fileName = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
            if (File.Exists(fileName))
                iApp.OpenWork(fileName, true);
        }

        private void txt_L1_TextChanged(object sender, EventArgs e)
        {
            double len = L1 + L2 + L3;

            uC_Superstructure1.Length = len;
            uC_Superstructure1.Width = B;

            double xinc = MyList.StringToDouble(txt_Ana_XINCR.Text, 10.0);


            txt_Ana_LL_load_gen.Text = (len / xinc).ToString("f0");


            dgv_SIDL[1, 0].Value = L1;
            dgv_SIDL[1, 1].Value = L2;
            dgv_SIDL[1, 2].Value = L1;
            dgv_SIDL[1, 3].Value = L2;


            Change_Cable_numbers();

        }

        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {

            iApp.Show_LL_Dialog();
            iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
        }

        private void btn_NLNR_create_Click(object sender, EventArgs e)
        {

            if (!File.Exists(Input_Data_2D))
            {
                MessageBox.Show("Main Analysis is not done, go to tab .... and create analysis input data and process Analysis.", "ASTRA", MessageBoxButtons.OK);

                return;
            }
            Process_Data(Input_Data_2D);
            Create_Nonlinear_Data_2D();

        }
        private void Create_Nonlinear_Data_2D()
        {

            string sap_data = "";


            sap_data = Path.Combine(Path.GetDirectoryName(Input_Data_2D), "SAP_Input_Data.txt");



            SAP_Document sp_doc = new SAP_Document();

            CBridgeStructure astra_doc = new CBridgeStructure(Input_Data_2D);




            if (File.Exists(sap_data))
            {
                sp_doc.Read_SAP_Data(sap_data);




                List<string> list = new List<string>();


                #region Sample Data
                //                list.Add(string.Format("Input file format"));
                //list.Add(string.Format("=================="));
                //list.Add(string.Format("Title - Up to 80 characters (identifies the problem title)"));
                //list.Add(string.Format("Structure data 9 Ints 1 Dec 1 Int (NJ,NM,NSUP,NC,NLJ,NLM,NSTEP1,NSTEP2,LIMIT,AWUN,ISTIF)"));
                //list.Add(string.Format("The Text "JOINT COORDINATES""));
                //list.Add(string.Format("For each Joint a line with 1 int and 2 dec - N,X,Y (Joint Number, X-coord, Y-coord)"));
                //list.Add(string.Format("The Text "MEMBER DATA" "));
                //list.Add(string.Format("For each member a line with 3 ints and 3 dec - N,IS,IE,A,ZI,E (Member Number, Joint # at start, Joint # at end, cross section area of member, Moment of Inertia of cross section, Modulus of elasticity)"));
                //list.Add(string.Format("The Text "SUPPORT RESTRAINTS""));
                //list.Add(string.Format("For each Support Joint a line with 4 ints - N,JX,JY,JZ"));
                //list.Add(string.Format("The Text "CABLE DATA""));
                //list.Add(string.Format("For each cable a line with 1 int and 2 dec - N,CIT,UWC"));
                //list.Add(string.Format("The Text "MEMBER DEAD WEIGHTS""));
                //list.Add(string.Format("For each member other than cables a line with 1 int and 1 dec = N,WDL"));
                //list.Add(string.Format("The Text "JOINT LOADS""));
                //list.Add(string.Format("For each loaded joint a line with 1 int and 3 dec - N,WX,WY,WZ"));
                //list.Add(string.Format("The Text "MEMBER LOADS""));
                //list.Add(string.Format("For each loaded member(other than dead load) 1 int and 4 dec - N,WSX,WEX,WSY,WEY"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("Notes:"));
                //list.Add(string.Format("** Input data can be separated by either a comma or whitespace "));
                //list.Add(string.Format("** Data counts start at 0 - ie when inputing the joint number the count goes 0...1...2...3..."));
                //list.Add(string.Format("** Data counts do not need to be in order - ie when input the joint number the first line could be JN=2 the next JN=1, the next JN=3, etc"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("example input file (not valid values):"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("BRIDGE STRUCTURE EXAMPLE FILE"));
                //list.Add(string.Format("4,2,2,2,2,2,1,2,3,34.78574,0"));
                //list.Add(string.Format("JOINT COORDINATES"));
                //list.Add(string.Format("0,23.4563,21.2311"));
                //list.Add(string.Format("1,32.3232,12.343"));
                //list.Add(string.Format("2,5.223,3.2134"));
                //list.Add(string.Format("3,45.3434,23.5445"));
                //list.Add(string.Format("MEMBER DATA"));
                //list.Add(string.Format("1,0,2,23.23,34.234,23.545"));
                //list.Add(string.Format("0,3,1,234.33,23.22,85.333"));
                //list.Add(string.Format("SUPPORT RESTRAINTS"));
                //list.Add(string.Format("0,1,1,0"));
                //list.Add(string.Format("2,0,1,0"));
                //list.Add(string.Format("CABLE DATA"));
                //list.Add(string.Format("0,23.44,44.334"));
                //list.Add(string.Format("1,23.44,44.323"));
                //list.Add(string.Format("MEMBER DEAD WEIGHTS"));
                //list.Add(string.Format("4,45.44"));
                //list.Add(string.Format("5,67.88"));
                //list.Add(string.Format("JOINT LOADS"));
                //list.Add(string.Format("0,32.33,23.55,32.66"));
                //list.Add(string.Format("1,43.32,76.66,45.56"));
                //list.Add(string.Format("MEMBER LOADS"));
                //list.Add(string.Format("0,454.334,564.43,345.33,456.342"));
                //list.Add(string.Format("1,323.44,233.44,754.43,545.334"));
                #endregion

                #region DATA 1 PROBLEM TITLE

                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                list.Add(string.Format("PROBLEM TITLE"));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format(""));
                //list.Add(string.Format("2 DIMENSIONAL PLANE CABLE STAYED BRIDGE", sp_doc.HED));
                list.Add(string.Format("2 DIMENSIONAL {0}", sp_doc.HED));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));


                #endregion DATA 1 PROBLEM TITLE

                #region DATA 2 STRUCTURE DATA CARD

                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 2 STRUCTURE DATA CARD"));
                list.Add(string.Format("STRUCTURE DATA"));
                list.Add(string.Format("=============="));
                list.Add(string.Format("NJ = Total Joints = {0}", sp_doc.Joints.Count));
                list.Add(string.Format("NM = TOTAL MEMBERS = {0}", (sp_doc.Beams.Count + sp_doc.Trusses.Count)));
                list.Add(string.Format("NSUP = NUMBER OF SUPPORT JOINTS = {0}", astra_doc.Supports.Count));
                list.Add(string.Format("NC = NUMBER OF CABLES = {0}", sp_doc.Trusses.Count));
                list.Add(string.Format("NLJ = NUMBER OF LOADED JOINTS = {0}", sp_doc.Joint_Loads.Count));
                list.Add(string.Format("NLM = NUMBER OF LOADED MEMBERS = 0"));
                list.Add(string.Format("NSTEP1 = 0"));
                list.Add(string.Format("NSTEP2 = 0"));
                list.Add(string.Format(""));

                #endregion DATA 2 STRUCTURE DATA CARD

                #region DATA 3 JOINT COORDINATES
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                list.Add(string.Format("JOINT COORDINATES "));
                //list.Add(string.Format("N (JOINT NoS.)   X-Coord   Y-Coord"));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("1            0.000     10.000       "));
                //list.Add(string.Format("2            3.433     10.000       "));

                foreach (var item in astra_doc.Joints)
                {
                    list.Add(string.Format("{0,-7} {1,10:f3} {2,10:f3}", item.NodeNo, item.X, item.Y));
                }


                #endregion DATA 3 JOINT COORDINATES

                #region DATA 4 MEMBER DATA


                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 4 MEMBER DATA"));
                //list.Add(string.Format("MEMBER NoS.  START &  END JOINT Nos.  A    E    IZ"));
                //list.Add(string.Format("==============  "));
                //list.Add(string.Format(""));
                list.Add(string.Format("MEMBER DATA"));

                //list.Add(string.Format("    1    1    2    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    2    2    3    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    3    3    4    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    4    4    5    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    5    5    6    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    6    6    7    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    7    7    8    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                System.Collections.Hashtable ht_sec_props = new System.Collections.Hashtable();
                System.Collections.Hashtable ht_bm_mat_props = new System.Collections.Hashtable();
                System.Collections.Hashtable ht_tr_mat_props = new System.Collections.Hashtable();



                foreach (var item in sp_doc.Beam_Sect_Properties)
                {
                    ht_sec_props.Add(item.Property_No, item);
                }
                foreach (var item in sp_doc.Beam_Mat_Properties)
                {
                    ht_bm_mat_props.Add(item.Material_No, item);
                }
                foreach (var item in sp_doc.Truss_Mat_Properties)
                {
                    ht_tr_mat_props.Add(item.Material_No, item);
                }

                foreach (var item in sp_doc.Beams)
                {
                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                    Beam_Material_Property bmp = ht_bm_mat_props[item.Material_Property_No] as Beam_Material_Property;
                    Beam_Section_Property bsp = ht_sec_props[item.Element_Property_No] as Beam_Section_Property;


                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));
                    //list.Add(string.Format("{0,5} {1,5} {2,5}{3,12:f6){4,12:E3){5,12:E3)",
                    //list.Add(string.Format("{0,5} {1,5} {2,5}{3:f6,12){4:E3,12){5:E3,12)",
                    //    item.Element_No,
                    //    item.Node_I,
                    //    item.Node_J,
                    //    bsp.AX, 
                    //    bmp.Youngs_Modulus,
                    //    bsp.IZ
                    //    ));
                    list.Add(string.Format("{0,5} {1,5} {2,5}{3,12:f6}{4,12:E3}{5,12:E3}",
                       item.Element_No,
                       item.Node_I,
                       item.Node_J,
                       bsp.AX,
                       bmp.Youngs_Modulus,
                       bsp.IZ
                       ));





                }

                int beam_count = sp_doc.Beams.Count;
                foreach (var item in sp_doc.Trusses)
                {
                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                    Truss_Material_Property tmp = ht_tr_mat_props[item.Material_Property_No] as Truss_Material_Property;
                    //list.Add(string.Format("  201    1  186    1.767E-002    2.11E+008     0.000"));
                    list.Add(string.Format("{0,5}{1,5}{2,5}{3,12:E3}{4,12:E3}{5,12:F3}",
                        (item.Element_No + beam_count)
                        , (item.Node_I)
                        , (item.Node_J)
                        , (tmp.Cross_Sectional_Area)
                        , (tmp.Modulus_of_Elasticity)
                        , 0.0
                        ));



                }
                #endregion DATA 4 MEMBER DAT

                #region DATA 5 SUPPORT DATA

                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 5 SUPPORT DATA"));
                //list.Add(string.Format("N (JOINT NoS.) JX  JY  JZ  "));
                //list.Add(string.Format("==============  "));
                //list.Add(string.Format(""));
                //list.Add(string.Format("1        1        1        1"));
                //list.Add(string.Format("2        0        0        0"));
                list.Add(string.Format("SUPPORT RESTRAINTS"));

                foreach (var item in sp_doc.Joints)
                {
                    //list.Add(string.Format("2        0        0        0"));
                    list.Add(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}", item.NodeNo
                        , item.Tx ? 1 : 0
                        , item.Ty ? 1 : 0
                        , item.Tz ? 1 : 0));
                }

                #endregion DATA 5 SUPPORT DATA

                #region DATA 6 CABLE DATA

                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 6 CABLE DATA"));
                //list.Add(string.Format("CABLES ARE OF GIVEN MEMBER Nos."));
                //list.Add(string.Format("N (MEMBER(CABLE) Nos.)  CIT  (INITIAL TENSION (TONS))  UWC (UNIT WEIGHT (SPECIFIC GRAVITY))"));
                //list.Add(string.Format("==============  "));
                //list.Add(string.Format(""));
                //list.Add(string.Format("201  100 7.8"));
                //list.Add(string.Format("202  100 7.8"));
                list.Add(string.Format("CABLE DATA"));

                double init_ten = 10.0;
                foreach (var item in sp_doc.Trusses)
                {
                    //list.Add(string.Format("202  100 7.8"));

                    Truss_Material_Property tmp = ht_tr_mat_props[item.Material_Property_No] as Truss_Material_Property;
                    //list.Add(string.Format("  201    1  186    1.767E-002    2.11E+008     0.000"));
                    list.Add(string.Format("{0,-5} {1,-5} {2,-5}",
                        (item.Element_No + beam_count)
                        , init_ten
                        , (tmp.Mass_Density)
                        ));
                }



                #endregion DATA 6 CABLE DATA

                #region DATA 7 MEMBER DEAD WEIGHT DATA


                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 7 MEMBER DEAD WEIGHT DATA"));
                list.Add(string.Format("N (MEMBER Nos.) WDL (WEIGHT (TONS) PER UNIT LENGTH)"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("1  5"));
                //list.Add(string.Format("2  5"));
                //list.Add(string.Format("3  5"));
                list.Add(string.Format("MEMBER DEAD WEIGHTS"));

                double mem_self = 5.0;
                foreach (var item in astra_doc.Members)
                {
                    list.Add(string.Format("{0,-5} {1,-10:f3}", item.MemberNo, mem_self));

                }

                #endregion DATA 7 MEMBER DEAD WEIGHT DATA

                #region DATA 8 JOINT LOAD DATA

                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 8 JOINT LOAD DATA"));
                //list.Add(string.Format("N (JOINT Nos.) WX (TONS)  WY (TONS)  WZ (TONS)"));
                //list.Add(string.Format("==============  "));
                //list.Add(string.Format(""));
                //list.Add(string.Format("1  0.0 -64.455  0.0"));
                //list.Add(string.Format("2  0.0 -64.455  0.0"));
                //list.Add(string.Format("3  0.0 -64.455  0.0"));
                //list.Add(string.Format("4  0.0 -64.455  0.0"));
                list.Add(string.Format("JOINT LOADS"));

                foreach (var item in sp_doc.Joint_Loads)
                {
                    //list.Add(string.Format("1  0.0 -64.455  0.0"));
                    list.Add(string.Format("{0} {1:f3} {2:f3} {3:f3}", item.Joint_No, item.FX
                        , item.FY
                        , item.FZ));
                }
                #endregion DATA 8 JOINT LOAD DATA

                #region DATA 9 MEMBER DISTRIBUTED LOAD DATA



                //list.Add(string.Format(""));
                //list.Add(string.Format("=============="));
                //list.Add(string.Format("DATA 9 MEMBER DISTRIBUTED LOAD DATA"));
                //list.Add(string.Format("MEMBER Nos. WSX (TONS)  WEX (TONS)  WSY (TONS)  WEY (TONS)"));
                //list.Add(string.Format("==============  "));
                //list.Add(string.Format(""));
                //list.Add(string.Format("NOT APPLICABLE AS MENTIONED IN DATA (2)"));
                list.Add(string.Format("MEMBER LOADS"));
                //list.Add(string.Format("NOT APPLICABLE AS MENTIONED IN DATA (2)"));

                #endregion DATA 9 MEMBER DISTRIBUTED LOAD DATA



                File.WriteAllLines(Input_Data_Nonlinear, list.ToArray());

                MessageBox.Show("Non Linear Analysis Data File Created as " + Input_Data_Nonlinear, "ASTRA", MessageBoxButtons.OK);

                //System.Diagnostics.Process.Start(Input_Data_Nonlinear);

            }
        }

        private void Create_Nonlinear_Data()
        {

            string sap_data = "";


            sap_data = Path.Combine(Path.GetDirectoryName(Input_Data_2D), "SAP_Input_Data.txt");



            SAP_Document sp_doc = new SAP_Document();

            CBridgeStructure astra_doc = new CBridgeStructure(Input_Data_2D);




            if (File.Exists(sap_data))
            {
                sp_doc.Read_SAP_Data(sap_data);


                List<string> list = new List<string>();


                #region DATA 1 PROBLEM TITLE

                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 1 PROBLEM TITLE"));
                list.Add(string.Format("=============="));
                list.Add(string.Format(""));
                //list.Add(string.Format("2 DIMENSIONAL PLANE CABLE STAYED BRIDGE", sp_doc.HED));
                list.Add(string.Format("2 DIMENSIONAL {0}", sp_doc.HED));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                #endregion DATA 1 PROBLEM TITLE

                #region DATA 2 STRUCTURE DATA CARD

                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 2 STRUCTURE DATA CARD"));
                list.Add(string.Format("=============="));
                list.Add(string.Format("NJ = Total Joints = {0}", sp_doc.Joints.Count));
                list.Add(string.Format("NM = TOTAL MEMBERS = {0}", (sp_doc.Beams.Count + sp_doc.Trusses.Count)));
                list.Add(string.Format("NSUP = NUMBER OF SUPPORT JOINTS = {0}", astra_doc.Supports.Count));
                list.Add(string.Format("NC = NUMBER OF CABLES = {0}", sp_doc.Trusses.Count));
                list.Add(string.Format("NLJ = NUMBER OF LOADED JOINTS = {0}", sp_doc.Joint_Loads.Count));
                list.Add(string.Format("NLM = NUMBER OF LOADED MEMBERS = 0"));
                list.Add(string.Format("NSTEP1 = ?"));
                list.Add(string.Format("NSTEP2 = ?"));
                list.Add(string.Format(""));

                #endregion DATA 2 STRUCTURE DATA CARD

                #region DATA 3 JOINT COORDINATES
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 3 JOINT COORDINATES "));
                list.Add(string.Format("N (JOINT NoS.)   X-Coord   Y-Coord"));
                list.Add(string.Format("=============="));
                //list.Add(string.Format("1            0.000     10.000       "));
                //list.Add(string.Format("2            3.433     10.000       "));

                foreach (var item in astra_doc.Joints)
                {
                    list.Add(string.Format("{0,-7} {1,10:f3} {2,10:f3}", item.NodeNo, item.X, item.Y));
                }


                #endregion DATA 3 JOINT COORDINATES

                #region DATA 4 MEMBER DAT


                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 4 MEMBER DATA"));
                list.Add(string.Format("MEMBER NoS.  START &  END JOINT Nos.  A    E    IZ"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("    1    1    2    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    2    2    3    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    3    3    4    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    4    4    5    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    5    5    6    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    6    6    7    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    7    7    8    0.224100    2.11E+008     4.10E-001"));
                //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                System.Collections.Hashtable ht_sec_props = new System.Collections.Hashtable();
                System.Collections.Hashtable ht_bm_mat_props = new System.Collections.Hashtable();
                System.Collections.Hashtable ht_tr_mat_props = new System.Collections.Hashtable();



                foreach (var item in sp_doc.Beam_Sect_Properties)
                {
                    ht_sec_props.Add(item.Property_No, item);
                }
                foreach (var item in sp_doc.Beam_Mat_Properties)
                {
                    ht_bm_mat_props.Add(item.Material_No, item);
                }
                foreach (var item in sp_doc.Truss_Mat_Properties)
                {
                    ht_tr_mat_props.Add(item.Material_No, item);
                }

                foreach (var item in sp_doc.Beams)
                {
                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                    Beam_Material_Property bmp = ht_bm_mat_props[item.Material_Property_No] as Beam_Material_Property;
                    Beam_Section_Property bsp = ht_sec_props[item.Element_Property_No] as Beam_Section_Property;


                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));
                    //list.Add(string.Format("{0,5} {1,5} {2,5}{3,12:f6){4,12:E3){5,12:E3)",
                    //list.Add(string.Format("{0,5} {1,5} {2,5}{3:f6,12){4:E3,12){5:E3,12)",
                    //    item.Element_No,
                    //    item.Node_I,
                    //    item.Node_J,
                    //    bsp.AX, 
                    //    bmp.Youngs_Modulus,
                    //    bsp.IZ
                    //    ));
                    list.Add(string.Format("{0,5} {1,5} {2,5}{3,12:f6}{4,12:E3}{5,12:E3}",
                       item.Element_No,
                       item.Node_I,
                       item.Node_J,
                       bsp.AX,
                       bmp.Youngs_Modulus,
                       bsp.IZ
                       ));





                }

                int beam_count = sp_doc.Beams.Count;
                foreach (var item in sp_doc.Trusses)
                {
                    //list.Add(string.Format("    8    8    9    0.224100    2.11E+008     4.10E-001"));

                    Truss_Material_Property tmp = ht_tr_mat_props[item.Material_Property_No] as Truss_Material_Property;
                    //list.Add(string.Format("  201    1  186    1.767E-002    2.11E+008     0.000"));
                    list.Add(string.Format("{0,5}{1,5}{2,5}{3,12:E3}{4,12:E3}{5,12:F3}",
                        (item.Element_No + beam_count)
                        , (item.Node_I)
                        , (item.Node_J)
                        , (tmp.Cross_Sectional_Area)
                        , (tmp.Modulus_of_Elasticity)
                        , 0.0
                        ));



                }
                #endregion DATA 4 MEMBER DAT

                #region DATA 5 SUPPORT DATA

                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 5 SUPPORT DATA"));
                list.Add(string.Format("N (JOINT NoS.) JX  JY  JZ  "));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("1        1        1        1"));
                //list.Add(string.Format("2        0        0        0"));

                foreach (var item in sp_doc.Joints)
                {
                    //list.Add(string.Format("2        0        0        0"));
                    list.Add(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}", item.NodeNo
                        , item.Tx ? 1 : 0
                        , item.Ty ? 1 : 0
                        , item.Tz ? 1 : 0));
                }

                #endregion DATA 5 SUPPORT DATA

                #region DATA 6 CABLE DATA

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 6 CABLE DATA"));
                list.Add(string.Format("CABLES ARE OF GIVEN MEMBER Nos."));
                list.Add(string.Format("N (MEMBER(CABLE) Nos.)  CIT  (INITIAL TENSION (TONS))  UWC (UNIT WEIGHT (SPECIFIC GRAVITY))"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("201  100 7.8"));
                //list.Add(string.Format("202  100 7.8"));

                double init_ten = 10.0;
                foreach (var item in sp_doc.Trusses)
                {
                    //list.Add(string.Format("202  100 7.8"));

                    Truss_Material_Property tmp = ht_tr_mat_props[item.Material_Property_No] as Truss_Material_Property;
                    //list.Add(string.Format("  201    1  186    1.767E-002    2.11E+008     0.000"));
                    list.Add(string.Format("{0,-5} {1,-5} {2,-5}",
                        (item.Element_No + beam_count)
                        , init_ten
                        , (tmp.Mass_Density)
                        ));
                }



                #endregion DATA 6 CABLE DATA

                #region DATA 7 MEMBER DEAD WEIGHT DATA


                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 7 MEMBER DEAD WEIGHT DATA"));
                list.Add(string.Format("N (MEMBER Nos.) WDL (WEIGHT (TONS) PER UNIT LENGTH)"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("1  5"));
                //list.Add(string.Format("2  5"));
                //list.Add(string.Format("3  5"));

                double mem_self = 5.0;
                foreach (var item in astra_doc.Members)
                {
                    list.Add(string.Format("{0,-5} {1,-10}", item.MemberNo, mem_self));

                }

                #endregion DATA 7 MEMBER DEAD WEIGHT DATA

                #region DATA 8 JOINT LOAD DATA

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 8 JOINT LOAD DATA"));
                list.Add(string.Format("N (JOINT Nos.) WX (TONS)  WY (TONS)  WZ (TONS)"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                //list.Add(string.Format("1  0.0 -64.455  0.0"));
                //list.Add(string.Format("2  0.0 -64.455  0.0"));
                //list.Add(string.Format("3  0.0 -64.455  0.0"));
                //list.Add(string.Format("4  0.0 -64.455  0.0"));

                foreach (var item in sp_doc.Joint_Loads)
                {
                    //list.Add(string.Format("1  0.0 -64.455  0.0"));
                    list.Add(string.Format("{0} {1:f3} {2:f3} {3:f3}", item.Joint_No, item.FX
                        , item.FY
                        , item.FZ));
                }
                #endregion DATA 8 JOINT LOAD DATA

                #region DATA 9 MEMBER DISTRIBUTED LOAD DATA



                list.Add(string.Format(""));
                list.Add(string.Format("=============="));
                list.Add(string.Format("DATA 9 MEMBER DISTRIBUTED LOAD DATA"));
                list.Add(string.Format("MEMBER Nos. WSX (TONS)  WEX (TONS)  WSY (TONS)  WEY (TONS)"));
                list.Add(string.Format("==============  "));
                list.Add(string.Format(""));
                list.Add(string.Format("NOT APPLICABLE AS MENTIONED IN DATA (2)"));
                list.Add(string.Format(""));

                #endregion DATA 9 MEMBER DISTRIBUTED LOAD DATA



                File.WriteAllLines(Input_Data_Nonlinear, list.ToArray());

                MessageBox.Show("Non Linear Analysis Data File Created as " + Input_Data_Nonlinear, "ASTRA", MessageBoxButtons.OK);

                //System.Diagnostics.Process.Start(Input_Data_Nonlinear);

            }
        }
        public bool Process_Data(string flPath)
        {
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");

            if (!File.Exists(prs.StartInfo.FileName))
            {
                MessageBox.Show("AST001.EXE module not found in the Application Folder \n\r\n\r \"" + Application.StartupPath + "\".", "ASTRA", MessageBoxButtons.OK);
                return false;
            }
            if (prs.Start())
                prs.WaitForExit();

            return true;
        }

        private void btn_NLNR_view_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            if (!File.Exists(Input_Data))
            {
                MessageBox.Show(this, "File not created.");
                return;
            }

            if (btn.Name == btn_NLNR_open.Name)
                System.Diagnostics.Process.Start(Input_Data);
            else if (btn.Name == btn_NLNR_view.Name)
            {
                iApp.OpenWork(Input_Data, false);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_CSB_Technical ft = new frm_CSB_Technical();
            ft.Show();
        }

        private void txt_sno_side_cables_cross_dist_TextChanged(object sender, EventArgs e)
        {
            Change_Cable_numbers();
        }

        private void Change_Cable_numbers()
        {

            double nc = MyList.StringToDouble(txt_nos_Side_cable.Text, 1);

            txt_nos_centre_cable.Text = txt_nos_Side_cable.Text;

            double sno = MyList.StringToDouble(txt_sno_side_cables_cross_dist.Text, 1);
            double cno = MyList.StringToDouble(txt_cno_center_cables_cross_dist.Text, 1);

            double d1 = (L1 / nc);

            txt_a1.Text = (d1 / sno).ToString("f3");
            txt_a3.Text = (d1 / sno).ToString("f3");

            d1 = (L2 / (nc * 2 + 1));

            txt_a2.Text = (d1 / cno).ToString("f3");
        }

        private void btn_NLNR_process_Click(object sender, EventArgs e)
        {
            iApp.Form_Stage_Analysis(Input_Data_Nonlinear).Show();
        }
        private void uC_Sections1_Changed(object sender, EventArgs e)
        {
            UserControl uc = sender as UserControl;
            if (uc == null) return;

            if (uc.Name == uC_Sections1.Name)
            {
                txt_TWA_sec_vs_A.Text = uC_Sections1.Area.ToString("f3");
                txt_TWA_sec_vs_IX.Text = uC_Sections1.Ixx.ToString("f3");
                txt_TWA_sec_vs_IZ.Text = uC_Sections1.Izz.ToString("f3");
            }

            else if (uc.Name == uC_Sections2.Name)
            {
                txt_TWA_sec_bs_AX.Text = uC_Sections2.Area.ToString("f3");
                txt_TWA_sec_bs_IX.Text = uC_Sections2.Ixx.ToString("f3");
                txt_TWA_sec_bs_IZ.Text = uC_Sections2.Izz.ToString("f3");
            }

        }


        public string Working_Folder
        {
            get
            {
                //return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return iApp.user_path;
            }

        }

        public string Analysis_File
        {
            get
            {

                if (!Directory.Exists(Working_Folder)) Directory.CreateDirectory(Working_Folder);

                string fpath = Path.Combine(Working_Folder, "Tower Analysis");

                if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

                return Path.Combine(fpath, "Tower_Analysis.txt");


            }
        }

        public string Seismic_File
        {
            get
            {
                string seis_inp_file = Path.Combine(Path.GetDirectoryName(Analysis_File), @"Seismic_Analysis\Seismic_Analysis.txt");
                return seis_inp_file;
            }
        }


        private void btn_create_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_create_data.Name)
            {
                Suspension_Bridge sps = new Suspension_Bridge();


                sps.Tower_Height = MyList.StringToDouble(txt_TWA_hgt.Text, 0.0);
                sps.Bracing_Panel_Height = MyList.StringToDouble(txt_TWA_brc_hgt.Text, 0.0);
                sps.Tower_Base_Width = MyList.StringToDouble(txt_TWA_base_wd.Text, 0.0);
                sps.Tower_Top_Width = MyList.StringToDouble(txt_TWA_top_wd.Text, 0.0);
                sps.Tower_Lower_Connector_Width = MyList.StringToDouble(txt_TWA_lower_cntr.Text, 0.0);
                sps.Tower_Upper_Connector_Width = MyList.StringToDouble(txt_TWA_upper_cntr.Text, 0.0);
                sps.Tower_Clear_Distance = MyList.StringToDouble(txt_TWA_clear_distance.Text, 0.0);


                sps.Tower_Dead_Load = MyList.StringToDouble(txt_TWA_dead_load.Text, 0.0);
                sps.Tower_Live_Load = MyList.StringToDouble(txt_TWA_live_load.Text, 0.0);
                sps.Tower_Seismic_Coefficient = MyList.StringToDouble(txt_TWA_seismic_coefficient.Text, 0.0);

                sps.Tower_SEC_VS_AX = MyList.StringToDouble(txt_TWA_sec_vs_A.Text, 0.0);
                sps.Tower_SEC_VS_IX = MyList.StringToDouble(txt_TWA_sec_vs_IX.Text, 0.0);
                sps.Tower_SEC_VS_IZ = MyList.StringToDouble(txt_TWA_sec_vs_IZ.Text, 0.0);


                sps.Tower_SEC_BS_AX = MyList.StringToDouble(txt_TWA_sec_bs_AX.Text, 0.0);
                sps.Tower_SEC_BS_IX = MyList.StringToDouble(txt_TWA_sec_bs_IX.Text, 0.0);
                sps.Tower_SEC_BS_IZ = MyList.StringToDouble(txt_TWA_sec_bs_IZ.Text, 0.0);

                //if (sps.Tower_Height == 30.0 &&

                //    sps.Bracing_Panel_Height == 2.0 &&
                //    sps.Tower_Base_Width == 3.0 &&
                //    sps.Tower_Top_Width == 1.5 &&
                //    sps.Tower_Lower_Connector_Width == 10.0 &&
                //    sps.Tower_Upper_Connector_Width == 24.0 &&
                //    sps.Tower_Clear_Distance == 4.5

                //    )
                //{
                //    Load_Example();
                //}
                //else
                //{
                if (sps.Create_Data(Analysis_File))
                {

                    MessageBox.Show("Data file Created as file " + Analysis_File);
                }
                //}
            }
            else if (btn.Name == btn_view_data.Name)
            {
                if (File.Exists(Analysis_File))
                    System.Diagnostics.Process.Start(Analysis_File);
                else
                {
                    MessageBox.Show("Analysis Input Data file not found.");
                }
            }

        }
        private void btn_open_analysis_Click(object sender, EventArgs e)
        {

            string rep_file = MyList.Get_Analysis_Report_File(Analysis_File);
            string seis_rep_file = MyList.Get_Analysis_Report_File(Seismic_File);

            int opt = 0;
            frm_OpenInput fin = new frm_OpenInput();


            Button btn = sender as Button;

            if (btn.Name == btn_open_analysis_report.Name)
            {
                if (File.Exists(rep_file) && File.Exists(seis_rep_file))
                {
                    if (fin.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) opt = fin.Option;

                    if (opt == 1) System.Diagnostics.Process.Start(rep_file);
                    if (opt == 2) System.Diagnostics.Process.Start(seis_rep_file);
                }
                else
                {
                    if (File.Exists(rep_file))
                        System.Diagnostics.Process.Start(rep_file);
                }
            }
            else if (btn.Name == btn_view_data.Name)
            {
                if (File.Exists(Analysis_File) && File.Exists(Seismic_File))
                {
                    if (fin.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) opt = fin.Option;

                    if (opt == 1) System.Diagnostics.Process.Start(Analysis_File);
                    if (opt == 2) System.Diagnostics.Process.Start(Seismic_File);
                }
                else
                {
                    if (File.Exists(Analysis_File))
                        System.Diagnostics.Process.Start(Analysis_File);
                }
            }
            else
            {
                MessageBox.Show("Analysis Input Data file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }
        }
        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (File.Exists(Analysis_File))
            {
                iApp.Form_ASTRA_TEXT_Data(Analysis_File, false).ShowDialog();
            }
            else
            {
                MessageBox.Show("Analysis Input Data file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            string rep_file = MyList.Get_Analysis_Report_File(Analysis_File);
            string seis_rep_file = MyList.Get_Analysis_Report_File(Seismic_File);

            BridgeMemberAnalysis dbd = null;

            if (File.Exists(seis_rep_file) && File.Exists(rep_file))
            {
                frm_OpenInput fin = new frm_OpenInput();
                if (fin.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                switch (fin.Option)
                {
                    case 1:
                        if (File.Exists(rep_file)) dbd = new BridgeMemberAnalysis(iApp, rep_file);
                        break;
                    case 2:
                        if (File.Exists(seis_rep_file)) dbd = new BridgeMemberAnalysis(iApp, seis_rep_file);
                        break;
                }
            }
            else
            {
                if (File.Exists(rep_file)) dbd = new BridgeMemberAnalysis(iApp, rep_file);
            }


            if (dbd != null)
            {
                //CMember cm = new CMember(iApp);
                //cm.Group = new MemberGroup();
                //foreach (var item in dbd.Analysis.Members)
                //{
                //    cm.Group.MemberNos.Add(item.MemberNo);
                //}

                //dbd.GetForce(ref cm);

                //txt_inp_moto_H21.Text = Math.Abs(cm.MaxShearForce).ToString();
                //txt_inp_moto_H22.Text = Math.Abs(cm.MaxTorsion).ToString();
            }
        }
        private void btn_edit_load_combs_Click(object sender, EventArgs e)
        {

            LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
            ff.Owner = this;
            ff.ShowDialog();
        }

        private void btn_calculate_weight_Click(object sender, EventArgs e)
        {
            Calculate_Total_Weight();
        }
        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                list.Add(string.Format("TYPE 1, LRFD_HTL57"));
                list.Add(string.Format("AXLE LOAD IN TONS,10.5,10.5,10.5,10.5,10.5,4.5 "));
                list.Add(string.Format("AXLE SPACING IN METRES,1.6,4.572,4.572,1.6,4.572"));
                list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 2, LRFD_HL93_HS20"));
                list.Add(string.Format("AXLE LOAD IN TONS,4.0,16.0,16.0"));
                list.Add(string.Format("AXLE SPACING IN METRES,4.2672,4.2672 "));
                list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 3, LRFD_HL93_H20"));
                list.Add(string.Format("AXLE LOAD IN TONS,4.0,16.0 "));
                list.Add(string.Format("AXLE SPACING IN METRES,4.2672 "));
                list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 4, LRFD_H30S24"));
                list.Add(string.Format("AXLE LOAD IN TONS,6.0,24.0,24.0"));
                list.Add(string.Format("AXLE SPACING IN METRES,4.25,8.0"));
                list.Add(string.Format("AXLE WIDTH IN METRES,1.800"));
                list.Add(string.Format("IMPACT FACTOR,1.10"));
                list.Add(string.Format(""));
            }
            else
            {
                list.Clear();
                list.Add(string.Format("TYPE 1, IRCCLASSA"));
                list.Add(string.Format("AXLE LOAD IN TONS , 2.7,2.7,11.4,11.4,6.8,6.8,6.8,6.8"));
                list.Add(string.Format("AXLE SPACING IN METRES, 1.10,3.20,1.20,4.30,3.00,3.00,3.00"));
                list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
                list.Add(string.Format("IMPACT FACTOR, 1.179"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 2, IRC70RTRACK"));
                list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
                list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
                list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
                list.Add(string.Format("IMPACT FACTOR, 1.25"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
                list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
                list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
                list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
                list.Add(string.Format("IMPACT FACTOR, 1.25"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
                list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
                list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
                list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
                list.Add(string.Format("IMPACT FACTOR, 1.10"));
                list.Add(string.Format(""));
                list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
                list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
                list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
                list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
                list.Add(string.Format("IMPACT FACTOR, 1.10"));
                list.Add(string.Format(""));
            }


            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    #region Long Girder
                    list.Clear();
                    list.Add(string.Format("LOAD 1,TYPE 3"));
                    list.Add(string.Format("X,-13.4"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 2,TYPE 1"));
                    list.Add(string.Format("X,-18.8"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 3,TYPE 1,TYPE 1"));
                    list.Add(string.Format("X,-18.8,-18.8"));
                    list.Add(string.Format("Z,1.5,4.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 4,TYPE 1,TYPE 1,TYPE 1"));
                    list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                    list.Add(string.Format("Z,1.5,4.5,7.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 5,TYPE 1,TYPE 3"));
                    list.Add(string.Format("X,-18.8,-13.4"));
                    list.Add(string.Format("Z,1.5,4.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 6,TYPE 3,TYPE 1"));
                    list.Add(string.Format("X,-13.4,-18.8"));
                    list.Add(string.Format("Z,1.5,4.5"));
                    list.Add(string.Format(""));
                    //list.Add(string.Format("TOTAL LOAD,TYPE 1,TYPE 1,TYPE 1"));
                    //list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                    //list.Add(string.Format("Z,1.5,4.5,7.5"));
                    //list.Add(string.Format(""));
                    #endregion
                }
                else
                {
                    #region Long Girder
                    list.Clear();
                    list.Add(string.Format("LOAD 1,TYPE 4"));
                    list.Add(string.Format("X,0"));
                    list.Add(string.Format("Z,1.5"));
                    list.Add(string.Format(""));
                    list.Add(string.Format("LOAD 2,TYPE 4,TYPE 4,"));
                    list.Add(string.Format("X,0,0,"));
                    list.Add(string.Format("Z,1.5,4.5,"));
                    list.Add(string.Format(""));
                    #endregion
                }
            }

            for (i = 0; i < list.Count; i++)
            {

                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                if (list[i] == "")
                {
                    dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                }
                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }


        }


        //private void btn_edit_load_combs_Click(object sender, EventArgs e)
        //{
        //    LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);
        //    ff.Owner = this;
        //    ff.ShowDialog();
        //}


        private void btn_restore_ll_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_long_restore_ll.Name) Default_Moving_LoadData(dgv_long_liveloads);
            }
        }

        private void txt_Ana_length_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
        }

        void Text_Changed()
        {
            double L = L1 + L2 + L3;

            txt_LL_load_gen.Text = ((L) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");

            uC_RCC_Abut1.Length = L;
            uC_RCC_Abut1.Width = B;
        }

        private void txt_LL_load_gen_TextChanged(object sender, EventArgs e)
        {
            return;
            double L = L1 + L2 + L3;

            txt_XINCR.Text = ((L) / (MyList.StringToDouble(txt_LL_load_gen.Text, 0.0))).ToString("f0");

        }



        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();


        List<string> load_list_1 = new List<string>();
        List<string> load_list_2 = new List<string>();
        List<string> load_list_3 = new List<string>();
        List<string> load_list_4 = new List<string>();
        List<string> load_list_5 = new List<string>();
        List<string> load_list_6 = new List<string>();
        List<string> load_total_7 = new List<string>();


        #region Chiranjit [2016 07 11]
        List<string> ll_comb = new List<string>();

        public void Store_LL_Combinations(DataGridView dgv_live_loads, DataGridView dgv_loads)
        {
            ll_comb.Clear();
            List<string> com = new List<string>();
            Hashtable ht_cmb = new Hashtable();

            string kStr = "";
            string txt = "";
            int i = 0;
            for (i = 0; i < dgv_live_loads.RowCount; i++)
            {
                kStr = dgv_live_loads[0, i].Value.ToString();
                txt = dgv_live_loads[1, i].Value.ToString();

                if (kStr.StartsWith("TYPE"))
                {
                    try
                    {
                        ht_cmb.Add(kStr, txt);
                    }
                    catch (Exception exx) { }
                }
            }

            dgv_live_loads.Tag = ht_cmb;


            com.Clear();


            for (i = 0; i < dgv_loads.RowCount; i++)
            {

                kStr = dgv_loads[0, i].Value.ToString();


                if (kStr.StartsWith("LOAD"))
                {
                    com.Clear();

                    for (int c = 1; c < dgv_loads.ColumnCount; c++)
                    {
                        txt = dgv_loads[c, i].Value.ToString();

                        if (txt == "") break;
                        kStr = ht_cmb[txt] as string;
                        com.Add(kStr);
                    }

                    //List<string> lst_lane1 = new List<string>();
                    //List<string> lst_lane2 = new List<string>();


                    //lst_lane1.AddRange(com);


                    //lst_lane2.Add(lst_lane1[0]);


                    //lst_lane1.RemoveAt(0);


                    //for(int k = 0; )




                    if (com.Count > 0)
                    {
                        txt = com[0];

                        int lane = 1;

                        for (int j = 1; j < com.Count; j++)
                        {
                            if (txt == com[j]) lane++;
                        }
                        if (lane > 1)
                        {
                            txt = lane + " Lane " + txt;
                        }
                        else
                        {
                            txt = lane + " Lane " + txt;

                            for (int j = 1; j < com.Count; j++)
                            {
                                txt += " + 1 Lane " + com[j];
                            }
                        }
                        ll_comb.Add(txt);
                    }

                }
            }
            dgv_loads.Tag = ll_comb;
        }

        #endregion Chiranjit [2016 07 11]

        public void LONG_GIRDER_LL_TXT()
        {
            Store_LL_Combinations(dgv_long_liveloads, dgv_long_loads);

            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();

            bool flag = false;
            for (i = 0; i < dgv_long_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_long_liveloads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        if (long_ll_impact.Contains(kStr) == false)
                            long_ll_impact.Add(kStr);

                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            List<string> files = new List<string>();


            load_list_1 = new List<string>();
            load_list_2 = new List<string>();
            load_list_3 = new List<string>();
            load_list_4 = new List<string>();
            load_list_5 = new List<string>();
            load_list_6 = new List<string>();
            load_total_7 = new List<string>();

            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);
            double imp_fact = 1.179;



            int count = 0;
            for (i = 0; i < dgv_long_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_long_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                //txt = string.Format("{0} {1:f3}", long_ll_types[f], imp_fact);
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (!list.Contains(txt))
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

                    
                    string fn = "";
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} {2:f3} {3:f3} XINC {4}", def_load[j], def_x[j], 0.0, def_z[j], xinc);
                        list.Add(txt);

                        fn = fn + " " + def_load[j];
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_long_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }


        void Ana_Write_Long_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";
            bool fl = false;

            if (add_DeadLoad)
            {

                foreach (var item in txt_member_load.Lines)
                {
                    if (add_LiveLoad)
                    {
                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }
                    else
                        load_lst.Add(item);

                }


            }
            else
            {
                //load_lst.Add("LOAD 1 DEAD LOAD");
                //load_lst.Add("MEMBER LOAD");
                //load_lst.Add("1 TO " + Long_Girder_Analysis.MemColls.Count + " UNI GY -0.0001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            CS_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                //Chiranjit [2013 10 07]
                //if (dgv_live_load.RowCount != 0)
                //load_lst.AddRange(Ana_Get_MovingLoad_Data(Long_Girder_Analysis.Live_Load_List));
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                load_lst.AddRange(load_total_7.ToArray());

            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        void Ana_Write_Long_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";
            bool fl = false;
            if (add_DeadLoad)
            {

                if (add_LiveLoad)
                {
                    foreach (var item in txt_member_load.Lines)
                    {

                        if (item.ToUpper().StartsWith("LOAD"))
                        {
                            if (fl == false)
                            {
                                fl = true;
                                load_lst.Add(item);
                            }
                            else
                                load_lst.Add("*" + item);
                        }
                        else
                        {
                            if (!load_lst.Contains(item))
                                load_lst.Add(item);
                            else
                                load_lst.Add("*" + item);
                        }
                    }
                    if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (HA_Lanes.Count > 0)
                        {
                            //load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("*HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                            load_lst.Add("MEMBER LOAD");

                            load_lst.Add(string.Format("{0} UNI GY -{1}", MyList.Get_Array_Text(HA_Loading_Members), txt_HA_UDL.Text));


                            foreach (var item in  HA_Loading_Members)
                            {

                                //load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, CS_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                            }
                        }
                    }

                }
                else
                    load_lst.AddRange(txt_member_load.Lines);
            }
            else
            {

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    //if (chk_self_indian.Checked)
                    //    load_lst.Add("SELFWEIGHT Y -1");
                }
                else if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    if (HA_Lanes.Count > 0)
                    {
                        load_lst.Add("LOAD 1 HA LOADINGS AS PER [BS 5400, Part 2, BD 37/01]");
                        load_lst.Add("MEMBER LOAD");

                        load_lst.Add(string.Format("{0} UNI GY -{1}", MyList.Get_Array_Text(HA_Loading_Members), txt_HA_UDL.Text));



                        //foreach (var item in MyList.Get_Array_Intiger(Long_Girder_Analysis.HA_Loading_Members))
                        //{
                        //    load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Long_Girder_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                        //}
                    }
                }
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            CS_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                if ((rbtn_HB.Checked || rbtn_HA_HB.Checked)
                    || iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                    load_lst.AddRange(all_loads[load_no - 1].ToArray());

                }

            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if(add_LiveLoad)
            {

                File.WriteAllLines(MyList.Get_LL_TXT_File(file_name), long_ll.ToArray());
            }
        }

        #region British Standard Loading
        private void txt_deck_width_TextChanged(object sender, EventArgs e)
        {
            British_Interactive();

        }


        private void rbtn_HA_HB_CheckedChanged(object sender, EventArgs e)
        {
            British_Interactive();


            if (rbtn_HA_HB.Checked || rbtn_HB.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
                cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LONGITUDINAL GIRDER ANALYSIS RESULTS"));
                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            else if (rbtn_HA.Checked)
            {
                cmb_long_open_file.Items.Clear();
                cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
                cmb_long_open_file.Items.Add(string.Format("LONGITUDINAL GIRDER ANALYSIS RESULTS"));
                //tabCtrl.TabPages.Remove(tab_mov_data_Indian);
            }
            sp_hb.Visible = !rbtn_HA.Checked;

        }
        public bool IsRead = false;
        public void British_Interactive()
        {
            if (IsRead) return;

            double d, lane_width, impf, lf;

            double incr, lgen;

            double L = L1 + L2 + L3;

            //txt_ll_british_incr
            int nos_lane, i;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);
            incr = MyList.StringToDouble(txt_ll_british_incr.Text, 0.0);

            if (incr == 0) incr = 1;
            lgen = ((int)(L / incr)) + 1;

            nos_lane = (int)(d / lane_width);

            txt_no_lanes.Text = nos_lane.ToString();
            txt_no_lanes.Enabled = false;

            txt_ll_british_lgen.Text = lgen.ToString();
            txt_ll_british_lgen.Enabled = false;


            chk_HA_1L.Enabled = (nos_lane >= 1);
            chk_HA_2L.Enabled = (nos_lane >= 2);
            chk_HA_3L.Enabled = (nos_lane >= 3);
            chk_HA_4L.Enabled = (nos_lane >= 4);
            chk_HA_5L.Enabled = (nos_lane >= 5);
            chk_HA_6L.Enabled = (nos_lane >= 6);
            chk_HA_7L.Enabled = (nos_lane >= 7);
            chk_HA_8L.Enabled = (nos_lane >= 8);
            chk_HA_9L.Enabled = (nos_lane >= 9);
            chk_HA_10L.Enabled = (nos_lane >= 10);


            chk_HB_1L.Enabled = (nos_lane >= 1);
            chk_HB_2L.Enabled = (nos_lane >= 2);
            chk_HB_3L.Enabled = (nos_lane >= 3);
            chk_HB_4L.Enabled = (nos_lane >= 4);
            chk_HB_5L.Enabled = (nos_lane >= 5);
            chk_HB_6L.Enabled = (nos_lane >= 6);
            chk_HB_7L.Enabled = (nos_lane >= 7);
            chk_HB_8L.Enabled = (nos_lane >= 8);
            chk_HB_9L.Enabled = (nos_lane >= 9);
            chk_HB_10L.Enabled = (nos_lane >= 10);


            grb_ha.Enabled = (rbtn_HA.Checked || rbtn_HA_HB.Checked);
            grb_hb.Enabled = (rbtn_HB.Checked || rbtn_HA_HB.Checked);

            if (rbtn_HA.Checked)
            {
                chk_HA_1L.Checked = chk_HA_1L.Enabled;
                chk_HA_2L.Checked = chk_HA_2L.Enabled;
                chk_HA_3L.Checked = chk_HA_3L.Enabled;
                chk_HA_4L.Checked = chk_HA_4L.Enabled;
                chk_HA_5L.Checked = chk_HA_5L.Enabled;
                chk_HA_6L.Checked = chk_HA_6L.Enabled;
                chk_HA_7L.Checked = chk_HA_7L.Enabled;
                chk_HA_8L.Checked = chk_HA_8L.Enabled;
                chk_HA_9L.Checked = chk_HA_9L.Enabled;
                chk_HA_10L.Checked = chk_HA_10L.Enabled;
            }

            if (rbtn_HB.Checked)
            {
                chk_HB_1L.Checked = chk_HB_1L.Enabled;
                chk_HB_2L.Checked = chk_HB_2L.Enabled;
                chk_HB_3L.Checked = chk_HB_3L.Enabled;
                chk_HB_4L.Checked = chk_HB_4L.Enabled;
                chk_HB_5L.Checked = chk_HB_5L.Enabled;
                chk_HB_6L.Checked = chk_HB_6L.Enabled;
                chk_HB_7L.Checked = chk_HB_7L.Enabled;
                chk_HB_8L.Checked = chk_HB_8L.Enabled;
                chk_HB_9L.Checked = chk_HB_9L.Enabled;
                chk_HB_10L.Checked = chk_HB_10L.Enabled;
            }

            //if(rbtn_HA_HB.Checked)
            //{

            //    chk_HB_1L.Checked = !chk_HA_1L.Checked;
            //    chk_HB_2L.Checked = !chk_HA_2L.Checked;
            //    chk_HB_3L.Checked = !chk_HA_3L.Checked;
            //    chk_HB_4L.Checked = !chk_HA_4L.Checked;
            //    chk_HB_5L.Checked = !chk_HA_5L.Checked;
            //    chk_HB_6L.Checked = !chk_HA_6L.Checked;
            //    chk_HB_7L.Checked = !chk_HA_7L.Checked;
            //    chk_HB_8L.Checked = !chk_HA_8L.Checked;
            //    chk_HB_9L.Checked = !chk_HA_9L.Checked;
            //    chk_HB_10L.Checked = !chk_HA_10L.Checked;
            //}


        }

        public void Default_British_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();
            int typ_no = 1;


            double ll = MyList.StringToDouble(load.Replace("HB_", ""), 1.0);


            for (i = 6; i <= 26; i += 5)
            {
                list.Add(string.Format("TYPE {0}, {1}_{2}", typ_no++, load, i));
                list.Add(string.Format("AXLE LOAD IN TONS ,{0:f1}, {0:f1}, {0:f1}, {0:f1}", ll));
                list.Add(string.Format("AXLE SPACING IN METRES, 1.8,{0:f1},1.8", i));
                list.Add(string.Format("AXLE WIDTH IN METRES, 1.0"));
                list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
                list.Add(string.Format(""));
            }

            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                //for (int j = 0; j < mlist.Count; j++)
                //{
                //    dgv_live_load[j, i].Value = mlist[j];
                //}

                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            if (chk_HB_1L.Checked) lanes.Add(1);
            if (chk_HB_2L.Checked) lanes.Add(2);
            if (chk_HB_3L.Checked) lanes.Add(3);
            if (chk_HB_4L.Checked) lanes.Add(4);
            if (chk_HB_5L.Checked) lanes.Add(5);
            if (chk_HB_6L.Checked) lanes.Add(6);
            if (chk_HB_7L.Checked) lanes.Add(7);
            if (chk_HB_8L.Checked) lanes.Add(8);
            if (chk_HB_9L.Checked) lanes.Add(9);
            if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);



            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 5; ld++)
            {
                load = "LOAD " + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25);

                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            //list.Add(string.Format("LOAD 1,TYPE 1"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 2,TYPE 2"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 3,TYPE 3"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,5.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 4,TYPE 4"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 5,TYPE 5"));
            //list.Add(string.Format("X,0,0"));
            //list.Add(string.Format("Z,1.5,4.5"));
            //list.Add(string.Format(""));
            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count * 2; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        private void cmb_HB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRead) return;

            Default_British_LoadData(dgv_long_british_loads);
            Default_British_Type_LoadData(dgv_british_loads);
        }

        private void chk_HA_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HB_1L.Checked = (!chk_HA_1L.Checked && chk_HB_1L.Enabled);
            chk_HB_2L.Checked = !chk_HA_2L.Checked && chk_HB_2L.Enabled;
            chk_HB_3L.Checked = !chk_HA_3L.Checked && chk_HB_3L.Enabled;
            chk_HB_4L.Checked = !chk_HA_4L.Checked && chk_HB_4L.Enabled;
            chk_HB_5L.Checked = !chk_HA_5L.Checked && chk_HB_5L.Enabled;
            chk_HB_6L.Checked = !chk_HA_6L.Checked && chk_HB_6L.Enabled;
            chk_HB_7L.Checked = !chk_HA_7L.Checked && chk_HB_7L.Enabled;
            chk_HB_8L.Checked = !chk_HA_8L.Checked && chk_HB_8L.Enabled;
            chk_HB_9L.Checked = !chk_HA_9L.Checked && chk_HB_9L.Enabled;
            chk_HB_10L.Checked = !chk_HA_10L.Checked && chk_HB_10L.Enabled;
            Default_British_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;

        }

        private void chk_HB_1L_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            chk_HA_1L.Checked = !chk_HB_1L.Checked && chk_HB_1L.Enabled;
            chk_HA_2L.Checked = !chk_HB_2L.Checked && chk_HB_2L.Enabled;
            chk_HA_3L.Checked = !chk_HB_3L.Checked && chk_HB_3L.Enabled;
            chk_HA_4L.Checked = !chk_HB_4L.Checked && chk_HB_4L.Enabled;
            chk_HA_5L.Checked = !chk_HB_5L.Checked && chk_HB_5L.Enabled;
            chk_HA_6L.Checked = !chk_HB_6L.Checked && chk_HB_6L.Enabled;
            chk_HA_7L.Checked = !chk_HB_7L.Checked && chk_HB_7L.Enabled;
            chk_HA_8L.Checked = !chk_HB_8L.Checked && chk_HB_8L.Enabled;
            chk_HA_9L.Checked = !chk_HB_9L.Checked && chk_HB_9L.Enabled;
            chk_HA_10L.Checked = !chk_HB_10L.Checked && chk_HB_10L.Enabled;

            Default_British_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;
        }

        private void chk_HA_3L_EnabledChanged(object sender, EventArgs e)
        {
            if (IsRead) return;
            CheckBox chk = sender as CheckBox;
            if (!chk.Enabled) chk.Checked = false;
        }
        public void LONG_GIRDER_BRITISH_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();

            if (rbtn_HA.Checked) return;

            List<string> long_ll_impact = new List<string>();


            bool flag = false;
            for (i = 0; i < dgv_long_british_loads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_british_loads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            load_list_1 = new List<string>();
            load_list_2 = new List<string>();
            load_list_3 = new List<string>();
            load_list_4 = new List<string>();
            load_list_5 = new List<string>();
            load_list_6 = new List<string>();
            load_total_7 = new List<string>();



            int fl = 0;
            double xinc = MyList.StringToDouble(txt_ll_british_incr.Text, 0.5);
            //double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_british_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_british_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_ll_british_lgen.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_british_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }

        public List<int> HA_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HA_1L.Checked) lanes.Add(1);
                if (chk_HA_2L.Checked) lanes.Add(2);
                if (chk_HA_3L.Checked) lanes.Add(3);
                if (chk_HA_4L.Checked) lanes.Add(4);
                if (chk_HA_5L.Checked) lanes.Add(5);
                if (chk_HA_6L.Checked) lanes.Add(6);
                if (chk_HA_7L.Checked) lanes.Add(7);
                if (chk_HA_8L.Checked) lanes.Add(8);
                if (chk_HA_9L.Checked) lanes.Add(9);
                if (chk_HA_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }
        public List<int> HB_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HB_1L.Checked) lanes.Add(1);
                if (chk_HB_2L.Checked) lanes.Add(2);
                if (chk_HB_3L.Checked) lanes.Add(3);
                if (chk_HB_4L.Checked) lanes.Add(4);
                if (chk_HB_5L.Checked) lanes.Add(5);
                if (chk_HB_6L.Checked) lanes.Add(6);
                if (chk_HB_7L.Checked) lanes.Add(7);
                if (chk_HB_8L.Checked) lanes.Add(8);
                if (chk_HB_9L.Checked) lanes.Add(9);
                if (chk_HB_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }



        #endregion British Standard Loading


        #region Deck slab

        private void uC_Deckslab_BS1_OnButtonClick(object sender, EventArgs e)
        {
            iApp.Save_Form_Record(this, user_path);
        }

        private void uC_Deckslab_IS1_OnCreateData(object sender, EventArgs e)
        {

            //Write_All_Data(true);

            uC_Deckslab_IS1.iApp = iApp;

            uC_Deckslab_IS1.user_path = user_path;





            //Calculate_Load_Computation(Long_Girder_Analysis._Outer_Girder_Mid,
            //    Long_Girder_Analysis._Inner_Girder_Mid,
            //     Long_Girder_Analysis.joints_list_for_load);

            //uC_Deckslab_IS1.deck_member_load = deck_member_load;


            //uC_Deckslab_IS1.L = L;
            //uC_Deckslab_IS1.NMG = NMG;
            //uC_Deckslab_IS1.SMG = SMG;
            //uC_Deckslab_IS1.os = os;
            //uC_Deckslab_IS1.CL = CL;
            //uC_Deckslab_IS1.Ds = Ds;
            //uC_Deckslab_IS1.B = B;


            //uC_Deckslab_IS1.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            //uC_Deckslab_IS1.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            //uC_Deckslab_IS1.Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 5].Value.ToString(), 0.0);
            //uC_Deckslab_IS1.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
            //uC_Deckslab_IS1.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);
            //uC_Deckslab_IS1.WidthBridge = L / (NCG - 1);


        }

        private void uC_Deckslab_BS1_OnCreateData(object sender, EventArgs e)
        {
            uC_Deckslab_BS1.iApp = iApp;
            uC_Deckslab_BS1.user_path = user_path;
        }

        #endregion  Deck slab

       
        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Set File Name

            string file_name = "";
            if (CS_Analysis != null)
            {
                file_name = Get_LongGirder_File(cmb_long_open_file.SelectedIndex);
            }
            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_Ana_View_Moving_Load.Enabled = File.Exists(MyList.Get_LL_TXT_File(file_name)) && File.Exists(MyList.Get_Analysis_Report_File(file_name));
            btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file.SelectedIndex != 9;
            btn_Ana_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
        }

        private void btn_cable_new_design_Click(object sender, EventArgs e)
        {

        }
        #region Chiranjit [2016 09 07]
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }

        public void All_Button_Enable(bool flag)
        {
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Cable_Stayed_Bridge;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]

        private void btn_susp_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                   
                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As


                    IsCreateData = false;

                    Input_Data = Path.Combine(user_path, "INPUT_DATA.TXT");
                    Open_AnalysisFile();
                    Show_ReadMemberLoad(Input_Data);
                    MessageBox.Show(this, "File opened succesfully.", "ASTRA", MessageBoxButtons.OK);


                    txt_project_name.Text = Path.GetFileName(user_path);

                    Write_All_Data();

                }
            }
            else if (btn.Name == btn_susp_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreateData = true;
                //}
                IsCreateData = true;
                Create_Project();
            }
        }

    }

    public class CABLE_STAYED_LS_Analysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
       
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;

        public List<BridgeMemberAnalysis> All_LL_Analysis = null;





        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        public int _Columns = 0, _Rows = 0;

        double span_length = 0.0;


        List<MemberSectionProperty> SectionProperty { get; set; }



        //Chiranjit [2013 06 06] Kolkata

        public string List_Envelop_Inner { get; set; }
        public string List_Envelop_Outer { get; set; }

        public string Start_Support { get; set; }
        public string End_Support { get; set; }


        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Working_Folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }
        #region Analysis Input File
        #endregion Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                if (File.Exists(value))
                    user_path = Path.GetDirectoryName(input_file);
            }
        }
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "DL + LL Combine Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DL_LL_Combine_Input_File.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2014 10 22]
        public string Get_LL_Analysis_Input_File(int AnalysisNo)
        {
            if (AnalysisNo <= 0) return "";

            if (Directory.Exists(Working_Folder))
            {
                string pd = Path.Combine(Working_Folder, "LL Analysis Load " + AnalysisNo);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_Load_" + AnalysisNo + "_Input_File.txt");
            }
            return "";
        }
        public string Working_Folder
        {
            get
            {
                if (File.Exists(Input_File))
                    return Path.GetDirectoryName(Input_File);
                return "";
            }
        }


        string input_file, user_path;
        public CABLE_STAYED_LS_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            //Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            //Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            List_Envelop_Inner = "";
            List_Envelop_Outer = "";
        }


        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();

        #region Chiranjit [2014 09 02] For British Standard

        public List<int> HA_Lanes;

        public string HA_Loading_Members;
        public void CreateData_British()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();


            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data


            List<double> HA_distances = new List<double>();
            if (HA_Lanes.Count > 0)
            {
                double ha = 0.0;

                for (int i = 0; i < HA_Lanes.Count; i++)
                {
                    ha = 1.75 + (HA_Lanes[i] - 1) * 3.5;
                    if (!list_z.Contains(ha))
                    {
                        list_z.Add(ha);
                        HA_distances.Add(ha);
                    }
                }
            }

            list_z.Sort();

            #endregion Chiranjit [2011 09 23] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

            //int i = 0;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows]);
                }
                z_table.Add(list_z[iRows], list);
            }

            int nodeNo = 0;
            Joints.Clear();


            #region Chiranjit [2013 05 30]
          
            #endregion Chiranjit [2013 05 30]

            #region Chiranjit [2013 06 06]

            #endregion Chiranjit [2013 06 06]
        }

        #endregion Chiranjit [2014 09 02] For British Standard

        public void Clear()
        {
            MemColls.Clear();
            MemColls = null;
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList_1 = new List<LoadData>();
            //LoadList.Clear();
            MyList mlist = null;
            for (i = 0; i < dgv_live_load.RowCount; i++)
            {
                try
                {
                    ld = new LoadData();
                    mlist = new MyList(MyList.RemoveAllSpaces(dgv_live_load[0, i].Value.ToString().ToUpper()), ':');
                    ld.TypeNo = mlist.StringList[0];
                    ld.Code = mlist.StringList[1];
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    for (int j = 0; j < Live_Load_List.Count; j++)
                    {
                        if (Live_Load_List[j].TypeNo == ld.TypeNo)
                        {
                            ld.LoadWidth = Live_Load_List[j].LoadWidth;
                            break;
                        }
                    }
                    if ((ld.Z + ld.LoadWidth) > WidthBridge)
                    {
                        throw new Exception("Width of Bridge Deck is insufficient to accommodate \ngiven numbers of Lanes of Vehicle Load. \n\nBridge Width = " + WidthBridge + " <  Load Width (" + ld.Z + " + " + ld.LoadWidth + ") = " + (ld.Z + ld.LoadWidth));
                    }
                    else
                    {
                        ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                        ld.ImpactFactor = MyList.StringToDouble(dgv_live_load[5, i].Value.ToString(), 0.5);
                        LoadList_1.Add(ld);
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #region Set Loads

            List<LoadData> LoadList_tmp = new List<LoadData>(LoadList_1.ToArray());

            LoadList_2 = new List<LoadData>();
            LoadList_3 = new List<LoadData>();
            LoadList_4 = new List<LoadData>();
            LoadList_5 = new List<LoadData>();
            LoadList_6 = new List<LoadData>();

            LoadList_1.Clear();

            //70 R Wheel
            ld = new LoadData(Live_Load_List[2]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_1.Add(ld);


            //1 Lane Class A
            ld = new LoadData(Live_Load_List[0]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_2.Add(ld);



            //2 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);



            //3 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[2].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);


            //1 Lane Class A + 70 RW
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);

            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            #endregion Set Loads

        }

        public double WidthBridge { get; set; }
    }


}
